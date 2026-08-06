# -*- coding: utf-8 -*-
"""FGO 账号从者/礼装导出工具入口。

运行：
    python main.py
"""
from __future__ import annotations

import sys

from gui import main as gui_main


if __name__ == "__main__":
    try:
        gui_main()
    except KeyboardInterrupt:
        sys.exit(0)
