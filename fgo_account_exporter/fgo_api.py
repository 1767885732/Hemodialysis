# -*- coding: utf-8 -*-
"""
FGO 私有游戏 API 客户端。

通过 FGO 游戏服务器私有接口（/home/top）获取当前账号下拥有的从者与礼装。
所需的 authKey / secretKey / userId 需要玩家自己通过抓包工具
（Fiddler / Charles / mitmproxy 等）从游戏请求中截取，详见 GUI 帮助说明。
"""
from __future__ import annotations

import json
import urllib.parse
import urllib.request
import gzip
import io
from typing import Any, Dict, List, Optional, Tuple


# 各区服登录服务器基址（FGO 私有 API 均走 POST）
REGION_BASE_URLS: Dict[str, str] = {
    "CN": "https://line1-login.bilibili.com",
    "JP": "https://login.fate-go.jp",
    "NA": "https://login.fate-go.us",
    "TW": "https://login.fate-go.tw",
    "KR": "https://login.fate-go.kr",
}

# 请求头：模拟游戏客户端 Dalvik 请求
DEFAULT_HEADERS: Dict[str, str] = {
    "User-Agent": "Dalvik/2.1.0 (Linux; U; Android 13)",
    "Accept-Encoding": "gzip",
    "Content-Type": "application/x-www-form-urlencoded",
}


class FGOApiError(Exception):
    """FGO API 调用异常。"""


class FGOClient:
    """FGO 私有 API 客户端。"""

    def __init__(
        self,
        region: str = "CN",
        user_id: str = "",
        auth_key: str = "",
        secret_key: str = "",
        ver_code: str = "",
        data_ver: str = "",
        date_ver: str = "",
        server_id: str = "1",
        timeout: int = 30,
    ) -> None:
        self.region = region
        self.base_url = REGION_BASE_URLS.get(region, REGION_BASE_URLS["CN"])
        self.user_id = user_id
        self.auth_key = auth_key
        self.secret_key = secret_key
        self.ver_code = ver_code
        self.data_ver = data_ver
        self.date_ver = date_ver
        self.server_id = server_id
        self.timeout = timeout
        self._last_access_time = "0"

    # ------------------------------------------------------------------ #
    #  HTTP
    # ------------------------------------------------------------------ #
    def _build_params(self, extra: Optional[Dict[str, str]] = None) -> Dict[str, str]:
        params = {
            "verCode": self.ver_code,
            "dataVer": self.data_ver,
            "dateVer": self.date_ver,
            "id": self.server_id,
            "lastAccessTime": self._last_access_time,
            "userId": self.user_id,
            "authKey": self.auth_key,
            "secretKey": self.secret_key,
        }
        if extra:
            params.update(extra)
        return params

    def _post(self, path: str, extra: Optional[Dict[str, str]] = None) -> Any:
        url = self.base_url.rstrip("/") + path
        params = self._build_params(extra)
        body = urllib.parse.urlencode(params).encode("utf-8")
        req = urllib.request.Request(url, data=body, headers=DEFAULT_HEADERS, method="POST")
        try:
            with urllib.request.urlopen(req, timeout=self.timeout) as resp:
                raw = resp.read()
                if resp.headers.get("Content-Encoding") == "gzip":
                    raw = gzip.decompress(raw)
                text = raw.decode("utf-8", errors="replace")
        except urllib.error.HTTPError as e:
            raise FGOApiError(f"HTTP {e.code} 错误：{e.reason}（路径 {path}）") from e
        except urllib.error.URLError as e:
            raise FGOApiError(f"网络请求失败：{e.reason}") from e

        try:
            data = json.loads(text)
        except json.JSONDecodeError as e:
            raise FGOApiError(f"响应不是合法 JSON：{e}\n前 200 字：{text[:200]}") from e

        # FGO API 失败时通常 resCode != "00"
        if isinstance(data, dict) and "response" in data:
            resp_block = data["response"]
            if isinstance(resp_block, list) and resp_block:
                first = resp_block[0]
                res_code = str(first.get("resCode", ""))
                if res_code and res_code != "00":
                    res_msg = first.get("resMsg", "未知错误")
                    raise FGOApiError(f"FGO 服务器返回错误（resCode={res_code}）：{res_msg}")
        return data

    # ------------------------------------------------------------------ #
    #  高层
    # ------------------------------------------------------------------ #
    def fetch_home_top(self) -> Dict[str, Any]:
        """请求 /home/top，返回完整 JSON 响应（含玩家持有数据与主数据缓存）。"""
        return self._post("/home/top", extra={"verCode": self.ver_code})

    # ------------------------------------------------------------------ #
    #  解析
    # ------------------------------------------------------------------ #
    @staticmethod
    def _updated_block(response: Dict[str, Any]) -> Dict[str, Any]:
        """从响应中定位 cache.updated 数据块（兼容多种结构）。"""
        block: Dict[str, Any] = {}
        resp = response.get("response")
        if isinstance(resp, list) and resp:
            first = resp[0] if resp else {}
            cache = first.get("cache") if isinstance(first, dict) else None
            if isinstance(cache, dict):
                updated = cache.get("updated")
                if isinstance(updated, dict):
                    block = updated
                # 部分版本直接把数据挂在 cache 下
                if not block:
                    block = {k: v for k, v in cache.items() if isinstance(v, list)}
            if not block and isinstance(first, dict):
                # 极少数情况下数据直接在 response[0] 顶层
                block = {k: v for k, v in first.items() if isinstance(v, list)}
        return block

    def build_name_maps(self, response: Dict[str, Any]) -> Tuple[Dict[int, str], Dict[int, str]]:
        """根据响应中的主数据构建 {id: name} 映射：从者、礼装。"""
        updated = self._updated_block(response)
        svt_names: Dict[int, str] = {}
        equip_names: Dict[int, str] = {}

        for item in updated.get("mstSvt", []) or []:
            try:
                svt_names[int(item["id"])] = item.get("name", "")
            except (KeyError, ValueError, TypeError):
                continue

        # 礼装主数据可能叫 mstEquip 或 mstSvtEquip
        for key in ("mstEquip", "mstSvtEquip"):
            for item in updated.get(key, []) or []:
                try:
                    equip_names[int(item["id"])] = item.get("name", "")
                except (KeyError, ValueError, TypeError):
                    continue
        return svt_names, equip_names

    def parse_servants(self, response: Dict[str, Any]) -> List[Dict[str, Any]]:
        """解析出当前账号持有的从者列表。

        优先取 userServant（含等级/技能/宝具等明细），否则退回 userSvtCollection（仅持有数）。
        """
        updated = self._updated_block(response)
        svt_names, _ = self.build_name_maps(response)
        svt_class = _CLASS_NAME_MAP  # 职阶名

        raw_list = updated.get("userServant") or updated.get("userSvtCollection") or []
        results: List[Dict[str, Any]] = []
        for item in raw_list:
            if not isinstance(item, dict):
                continue
            svt_id = _to_int(item.get("svtId"))
            if svt_id is None:
                continue
            status = item.get("status") if isinstance(item.get("status"), dict) else item
            name = svt_names.get(svt_id, f"未知从者({svt_id})")
            cls_id = _to_int(item.get("classId") or status.get("classId"))
            results.append(
                {
                    "id": svt_id,
                    "name": name,
                    "class": svt_class.get(cls_id, "—"),
                    "level": _to_int(status.get("lv")) or _to_int(item.get("lv")),
                    "skill1": _to_int(status.get("skillLv1")) or _to_int(item.get("skillLv1")),
                    "skill2": _to_int(status.get("skillLv2")) or _to_int(item.get("skillLv2")),
                    "skill3": _to_int(status.get("skillLv3")) or _to_int(item.get("skillLv3")),
                    "np_level": (
                        _to_int(status.get("treasureDeviceLv"))
                        or _to_int(item.get("treasureDeviceLv"))
                    ),
                    "bond": _to_int(status.get("friendshipRank"))
                    or _to_int(item.get("friendshipRank")),
                    "count": _to_int(item.get("num")) or 1,
                }
            )
        return results

    def parse_equips(self, response: Dict[str, Any]) -> List[Dict[str, Any]]:
        """解析出当前账号持有的礼装列表。

        优先取 userEquip（含等级明细），否则退回 userEquipCollection（仅持有数）。
        """
        updated = self._updated_block(response)
        _, equip_names = self.build_name_maps(response)

        # 合并明细表与收藏表，按礼装 id 聚合数量
        detail_list = updated.get("userEquip") or []
        coll_list = updated.get("userEquipCollection") or []

        merged: Dict[int, Dict[str, Any]] = {}
        for item in detail_list:
            if not isinstance(item, dict):
                continue
            equip_id = _to_int(item.get("svtId") or item.get("equipId"))
            if equip_id is None:
                continue
            status = item.get("status") if isinstance(item.get("status"), dict) else item
            rec = merged.setdefault(
                equip_id,
                {
                    "id": equip_id,
                    "name": equip_names.get(equip_id, f"未知礼装({equip_id})"),
                    "level": None,
                    "count": 0,
                },
            )
            rec["level"] = _to_int(status.get("lv")) or rec["level"]
            rec["count"] += 1

        for item in coll_list:
            if not isinstance(item, dict):
                continue
            equip_id = _to_int(item.get("svtId") or item.get("equipId"))
            if equip_id is None:
                continue
            num = _to_int(item.get("num")) or 1
            rec = merged.setdefault(
                equip_id,
                {
                    "id": equip_id,
                    "name": equip_names.get(equip_id, f"未知礼装({equip_id})"),
                    "level": None,
                    "count": 0,
                },
            )
            rec["count"] = max(rec["count"], num) if rec["count"] else num

        return list(merged.values())


# ---------------------------------------------------------------------- #
#  辅助
# ---------------------------------------------------------------------- #
def _to_int(value: Any) -> Optional[int]:
    try:
        if value is None:
            return None
        return int(value)
    except (ValueError, TypeError):
        return None


# 职阶 id -> 中文名（FGO 通用职阶编号）
_CLASS_NAME_MAP: Dict[int, str] = {
    1: "Saber", 2: "Archer", 3: "Lancer", 4: "Rider", 5: "Caster",
    6: "Assassin", 7: "Berserker", 8: "Shielder", 9: "Ruler", 10: "Avenger",
    11: "BeastⅡ", 12: "BeastⅠ", 13: "Alterego", 14: "Foreigner", 15: "Pretender",
    17: "Beast?", 18: "MoonCancer",
}
