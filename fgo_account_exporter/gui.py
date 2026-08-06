# -*- coding: utf-8 -*-
"""FGO 账号从者/礼装导出工具 —— tkinter 图形界面。"""
from __future__ import annotations

import os
import threading
import tkinter as tk
from tkinter import filedialog, messagebox, ttk
from typing import Any, Dict, List, Optional

from excel_export import export_to_excel
from fgo_api import FGOApiError, FGOClient, REGION_BASE_URLS
from sample_data import sample_equips, sample_servants


APP_TITLE = "FGO 账号从者/礼装导出工具"


class App(tk.Tk):
    def __init__(self) -> None:
        super().__init__()
        self.title(APP_TITLE)
        self.geometry("1080x720")
        self.minsize(900, 600)

        self.servants: List[Dict[str, Any]] = []
        self.equips: List[Dict[str, Any]] = []

        self._build_styles()
        self._build_credential_frame()
        self._build_table_frame()
        self._build_status_bar()
        self._set_actions_state(False)

    # ------------------------------------------------------------------ #
    #  布局
    # ------------------------------------------------------------------ #
    def _build_styles(self) -> None:
        style = ttk.Style(self)
        try:
            style.theme_use("clam")
        except tk.TclError:
            pass
        style.configure("Header.TLabel", font=("Microsoft YaHei", 11, "bold"))
        style.configure("Tip.TLabel", foreground="#888888", font=("Microsoft YaHei", 9))
        style.configure("Accent.TButton", font=("Microsoft YaHei", 10, "bold"))

    def _build_credential_frame(self) -> None:
        frame = ttk.LabelFrame(self, text="账号凭证（从游戏抓包获取，详见「帮助」）")
        frame.pack(fill="x", padx=10, pady=(10, 6))

        ttk.Label(frame, text="区服:").grid(row=0, column=0, sticky="e", padx=4, pady=6)
        self.region_var = tk.StringVar(value="CN")
        ttk.Combobox(
            frame,
            textvariable=self.region_var,
            values=list(REGION_BASE_URLS.keys()),
            width=6,
            state="readonly",
        ).grid(row=0, column=1, sticky="w", padx=4)

        labels = [
            ("userId:", "user_id"), ("authKey:", "auth_key"),
            ("secretKey:", "secret_key"),
            ("verCode:", "ver_code"), ("dataVer:", "data_ver"),
            ("dateVer:", "date_ver"),
        ]
        self.entries: Dict[str, ttk.Entry] = {}
        col = 2
        for text, key in labels:
            ttk.Label(frame, text=text).grid(row=0, column=col, sticky="e", padx=4)
            entry = ttk.Entry(frame, width=18 if key in ("auth_key", "secret_key") else 12)
            entry.grid(row=0, column=col + 1, sticky="w", padx=4)
            self.entries[key] = entry
            col += 2

        btn_box = ttk.Frame(frame)
        btn_box.grid(row=0, column=col, padx=(10, 6))
        self.fetch_btn = ttk.Button(btn_box, text="获取账号数据", style="Accent.TButton",
                                    command=self.on_fetch)
        self.fetch_btn.pack(side="left", padx=3)
        self.demo_btn = ttk.Button(btn_box, text="载入演示数据", command=self.on_load_demo)
        self.demo_btn.pack(side="left", padx=3)
        self.help_btn = ttk.Button(btn_box, text="帮助", command=self.show_help)
        self.help_btn.pack(side="left", padx=3)

        ttk.Label(
            frame,
            text="提示：authKey/secretKey/userId 为必填；verCode/dataVer/dateVer 可填可不填（部分版本校验）。",
            style="Tip.TLabel",
        ).grid(row=1, column=0, columnspan=col + 1, sticky="w", padx=6, pady=(0, 6))

    def _build_table_frame(self) -> None:
        nb = ttk.Notebook(self)
        nb.pack(fill="both", expand=True, padx=10, pady=6)

        svt_cols = ("id", "name", "class", "level", "skill1", "skill2", "skill3",
                    "np_level", "bond", "count")
        svt_headers = ("从者ID", "名称", "职阶", "等级", "技能1", "技能2", "技能3",
                       "宝具等级", "羁绊", "持有数")
        self.svt_tree = self._make_tree(nb, "从者", svt_cols, svt_headers)

        eq_cols = ("id", "name", "level", "count")
        eq_headers = ("礼装ID", "名称", "等级", "持有数")
        self.eq_tree = self._make_tree(nb, "礼装", eq_cols, eq_headers)

        export_box = ttk.Frame(self)
        export_box.pack(fill="x", padx=10, pady=(0, 6))
        self.export_btn = ttk.Button(export_box, text="导出 Excel", style="Accent.TButton",
                                     command=self.on_export)
        self.export_btn.pack(side="left")
        self.summary_var = tk.StringVar(value="尚未加载数据")
        ttk.Label(export_box, textvariable=self.summary_var).pack(side="left", padx=12)

    def _make_tree(self, parent, title, cols, headers) -> ttk.Treeview:
        container = ttk.Frame(parent)
        parent.add(container, text=title)
        tree = ttk.Treeview(container, columns=cols, show="headings", height=18)
        for col, header in zip(cols, headers):
            tree.heading(col, text=header)
            tree.column(col, width=120, anchor="center")
        tree.column("name", width=240, anchor="w")
        ysb = ttk.Scrollbar(container, orient="vertical", command=tree.yview)
        xsb = ttk.Scrollbar(container, orient="horizontal", command=tree.xview)
        tree.configure(yscrollcommand=ysb.set, xscrollcommand=xsb.set)
        tree.grid(row=0, column=0, sticky="nsew")
        ysb.grid(row=0, column=1, sticky="ns")
        xsb.grid(row=1, column=0, sticky="ew")
        container.rowconfigure(0, weight=1)
        container.columnconfigure(0, weight=1)
        return tree

    def _build_status_bar(self) -> None:
        bar = ttk.Frame(self, relief="sunken")
        bar.pack(fill="x", side="bottom")
        self.status_var = tk.StringVar(value="就绪")
        ttk.Label(bar, textvariable=self.status_var, anchor="w").pack(
            side="left", padx=8, pady=3
        )

    # ------------------------------------------------------------------ #
    #  行为
    # ------------------------------------------------------------------ #
    def _set_actions_state(self, has_data: bool) -> None:
        self.export_btn.config(state="normal" if has_data else "disabled")

    def _set_busy(self, busy: bool) -> None:
        state = "disabled" if busy else "normal"
        for w in (self.fetch_btn, self.demo_btn, self.help_btn, self.export_btn):
            w.config(state=state)

    def _status(self, text: str) -> None:
        self.status_var.set(text)

    def on_load_demo(self) -> None:
        self.servants = sample_servants()
        self.equips = sample_equips()
        self._populate_trees()
        self._set_actions_state(True)
        self.summary_var.set(
            f"演示数据：从者 {len(self.servants)} 位，礼装 {len(self.equips)} 种"
        )
        self._status("已载入演示数据（非真实账号）")

    def on_fetch(self) -> None:
        user_id = self.entries["user_id"].get().strip()
        auth_key = self.entries["auth_key"].get().strip()
        secret_key = self.entries["secret_key"].get().strip()
        if not (user_id and auth_key and secret_key):
            messagebox.showwarning("缺少凭证", "请至少填写 userId、authKey、secretKey。")
            return

        self._set_busy(True)
        self._status("正在连接 FGO 服务器获取数据……")

        def work() -> None:
            try:
                client = FGOClient(
                    region=self.region_var.get(),
                    user_id=user_id,
                    auth_key=auth_key,
                    secret_key=secret_key,
                    ver_code=self.entries["ver_code"].get().strip(),
                    data_ver=self.entries["data_ver"].get().strip(),
                    date_ver=self.entries["date_ver"].get().strip(),
                )
                response = client.fetch_home_top()
                servants = client.parse_servants(response)
                equips = client.parse_equips(response)
                self.after(0, lambda: self._on_fetch_done(servants, equips, None))
            except FGOApiError as e:
                self.after(0, lambda: self._on_fetch_done([], [], e))
            except Exception as e:  # noqa: BLE001
                self.after(0, lambda: self._on_fetch_done([], [], e))

        threading.Thread(target=work, daemon=True).start()

    def _on_fetch_done(self, servants, equips, error: Optional[BaseException]) -> None:
        self._set_busy(False)
        if error is not None:
            self._status("获取失败")
            messagebox.showerror(
                "获取失败",
                f"{type(error).__name__}: {error}\n\n"
                "请检查凭证是否正确、是否过期，以及区服/版本号是否匹配。",
            )
            return
        self.servants = servants
        self.equips = equips
        self._populate_trees()
        self._set_actions_state(True)
        self.summary_var.set(
            f"账号数据：从者 {len(servants)} 位，礼装 {len(equips)} 种"
        )
        self._status("获取成功")

    def _populate_trees(self) -> None:
        self.svt_tree.delete(*self.svt_tree.get_children())
        for s in sorted(self.servants, key=lambda x: (x.get("class", ""), -(x.get("level") or 0))):
            self.svt_tree.insert(
                "", "end",
                values=[s.get(k, "") for k in
                        ("id", "name", "class", "level", "skill1", "skill2",
                         "skill3", "np_level", "bond", "count")],
            )
        self.eq_tree.delete(*self.eq_tree.get_children())
        for e in sorted(self.equips, key=lambda x: -(x.get("count") or 0)):
            self.eq_tree.insert(
                "", "end",
                values=[e.get(k, "") for k in ("id", "name", "level", "count")],
            )

    def on_export(self) -> None:
        if not self.servants and not self.equips:
            messagebox.showinfo("无数据", "当前没有可导出的数据。")
            return
        initial = f"FGO_account_{self.region_var.get()}.xlsx"
        path = filedialog.asksaveasfilename(
            title="导出 Excel",
            defaultextension=".xlsx",
            filetypes=[("Excel 工作簿", "*.xlsx"), ("所有文件", "*.*")],
            initialfile=initial,
        )
        if not path:
            return
        try:
            export_to_excel(self.servants, self.equips, path,
                            account_label=self.region_var.get())
        except Exception as e:  # noqa: BLE001
            messagebox.showerror("导出失败", f"{type(e).__name__}: {e}")
            return
        self._status(f"已导出：{path}")
        if messagebox.askyesno("导出成功", f"已导出到：\n{path}\n\n是否打开所在文件夹？"):
            try:
                _open_folder(os.path.dirname(os.path.abspath(path)))
            except Exception:
                pass

    def show_help(self) -> None:
        help_text = HELP_TEXT
        win = tk.Toplevel(self)
        win.title("使用帮助")
        win.geometry("720x560")
        txt = tk.Text(win, wrap="word", padx=12, pady=12, font=("Microsoft YaHei", 10))
        txt.pack(fill="both", expand=True)
        txt.insert("1.0", help_text)
        txt.config(state="disabled")
        ttk.Button(win, text="关闭", command=win.destroy).pack(pady=8)


HELP_TEXT = """\
FGO 账号从者/礼装导出工具 —— 使用说明

【本工具用途】
连接 FGO 游戏服务器私有接口，读取当前账号拥有的从者与礼装，
并一键导出为 Excel（.xlsx），包含「从者」「礼装」两个工作表。

【如何获取凭证（userId / authKey / secretKey）】
FGO 没有公开 API，需要从游戏请求中抓包获取这三项参数：

1. 在电脑上安装抓包代理工具：Fiddler、Charles 或 mitmproxy。
2. 配置手机走电脑代理，并安装代理的 HTTPS 根证书。
3. 打开 FGO 客户端正常进入游戏主界面（触发一次网络请求）。
4. 在抓包工具中找到请求域名：
     国服：line1-login.bilibili.com / line2-login.bilibili.com
     日服：login.fate-go.jp
     美服：login.fate-go.us
     台服：login.fate-go.tw
     韩服：login.fate-go.kr
   任意一个 POST 请求（如 /home/top、/login/top）的请求体里就含有：
     userId、authKey、secretKey，以及 verCode / dataVer / dateVer。
5. 把这些值填入本工具对应输入框，选择区服后点击「获取账号数据」。

【关于 verCode / dataVer / dateVer】
这是游戏版本号，会随版本更新变化。多数情况下留空也能请求成功；
若服务器返回版本错误，请从抓包请求体里复制最新值填入。

【安全说明】
- 本工具仅在本地运行，凭证不会上传任何第三方服务器。
- authKey/secretKey 有时效，过期后需重新抓包获取。
- 仅用于读取自己账号的数据，请勿用于他人账号。

【演示数据】
未抓包时点击「载入演示数据」可预览界面与导出效果（数据为虚构）。

【导出】
获取/载入数据后点击「导出 Excel」选择保存路径即可。
"""


def _open_folder(path: str) -> None:
    import subprocess
    import sys
    if sys.platform.startswith("win"):
        os.startfile(path)  # type: ignore[attr-defined]
    elif sys.platform == "darwin":
        subprocess.Popen(["open", path])
    else:
        subprocess.Popen(["xdg-open", path])


def main() -> None:
    app = App()
    app.mainloop()


if __name__ == "__main__":
    main()
