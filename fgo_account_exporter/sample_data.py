# -*- coding: utf-8 -*-
"""演示用样本数据，用于在未填写真实凭证时展示与测试导出功能。"""
from __future__ import annotations

from typing import Any, Dict, List


def sample_servants() -> List[Dict[str, Any]]:
    return [
        {"id": 100100, "name": "阿尔托莉雅·潘德拉贡", "class": "Saber", "level": 90,
         "skill1": 10, "skill2": 10, "skill3": 10, "np_level": 5, "bond": 15, "count": 1},
        {"id": 101800, "name": "冲田总司", "class": "Saber", "level": 90,
         "skill1": 10, "skill2": 9, "skill3": 10, "np_level": 5, "bond": 12, "count": 1},
        {"id": 200100, "name": "埃米ya(卫宫)", "class": "Archer", "level": 80,
         "skill1": 8, "skill2": 8, "skill3": 8, "np_level": 3, "bond": 8, "count": 1},
        {"id": 201300, "name": "吉尔伽美什", "class": "Archer", "level": 90,
         "skill1": 10, "skill2": 10, "skill3": 10, "np_level": 5, "bond": 14, "count": 1},
        {"id": 301300, "name": "阿尔托莉雅·潘德拉贡(Alter)(Lancer)", "class": "Lancer",
         "level": 90, "skill1": 9, "skill2": 10, "skill3": 10, "np_level": 4, "bond": 10, "count": 1},
        {"id": 401700, "name": "阿尔托莉雅·潘德拉贡(Rider)", "class": "Rider",
         "level": 90, "skill1": 10, "skill2": 10, "skill3": 10, "np_level": 5, "bond": 15, "count": 1},
        {"id": 501900, "name": "诸葛孔明〔埃尔梅罗Ⅱ世〕", "class": "Caster",
         "level": 90, "skill1": 10, "skill2": 10, "skill3": 10, "np_level": 2, "bond": 15, "count": 1},
        {"id": 601200, "name": "冈田以藏", "class": "Assassin", "level": 80,
         "skill1": 8, "skill2": 8, "skill3": 8, "np_level": 3, "bond": 7, "count": 1},
        {"id": 701300, "name": "赫拉克勒斯", "class": "Berserker", "level": 80,
         "skill1": 10, "skill2": 9, "skill3": 9, "np_level": 4, "bond": 11, "count": 1},
        {"id": 900100, "name": "贞德·达尔克", "class": "Ruler", "level": 90,
         "skill1": 10, "skill2": 9, "skill3": 10, "np_level": 5, "bond": 13, "count": 1},
        {"id": 1100200, "name": "岩窟王 爱德蒙·唐泰斯", "class": "Avenger",
         "level": 90, "skill1": 10, "skill2": 10, "skill3": 10, "np_level": 5, "bond": 14, "count": 1},
        {"id": 1500100, "name": "奥伯龙", "class": "Pretender", "level": 90,
         "skill1": 10, "skill2": 10, "skill3": 10, "np_level": 5, "bond": 10, "count": 1},
    ]


def sample_equips() -> List[Dict[str, Any]]:
    return [
        {"id": 9400190, "name": "万华镜", "level": 100, "count": 2},
        {"id": 9400460, "name": "有限/无坌之梦", "level": 100, "count": 1},
        {"id": 9400490, "name": "如月之夜·苍月之雪", "level": 100, "count": 1},
        {"id": 9400270, "name": "天鬼姫", "level": 80, "count": 1},
        {"id": 9400510, "name": "甜蜜水晶", "level": 100, "count": 1},
        {"id": 9400340, "name": "黑之圣杯", "level": 100, "count": 1},
        {"id": 9400450, "name": "棱镜宇宙", "level": 100, "count": 1},
        {"id": 9400280, "name": "圣女の教示", "level": 80, "count": 3},
        {"id": 9400330, "name": "骑士の矜持", "level": 100, "count": 1},
        {"id": 9400550, "name": "魔法少女の体験", "level": 100, "count": 1},
        {"id": 9402710, "name": "五星·夺取圣杯的少女", "level": 1, "count": 1},
        {"id": 9403180, "name": "天使の诗", "level": 100, "count": 1},
    ]
