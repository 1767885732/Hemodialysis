/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：系统基础信息
// 创建时间：2013-03-07
// 创建者：Frank
//  
// 修改时间：
// 修改人：
// 修改描述：
//
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hemo.Utilities
{
    public class BaseSystemInfo
    {
        public static string ServiceFactory = "ServiceFactory";
        public static string ServicePath = "Hemo.Service";

        /// <summary>
        /// 是否登记异常
        /// </summary>
        public static bool LogException = true;

        /// <summary>
        /// 是否登记到 Windows 系统异常中
        /// </summary>
        public static bool EventLog = false;

        /// <summary>
        /// 是否将日志写入文件中
        /// </summary>
        public static bool FileLog = false;

        /// <summary>
        /// 当前软件ID
        /// </summary>
        public static string SoftName = string.Empty;

        /// <summary>
        /// 软件的名称
        /// </summary>
        public static string SoftFullName = string.Empty;
    }
}
