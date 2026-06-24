/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司

 * 
 * 修改时间:2017年6月17日
 * 修改人:贺建操
 * 修改描述:读取Config配置项类
 * 
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Hemo.Client.Core
{
    public class AppSettingsConfig
    {
        /// <summary>
        /// 返回获取访问令牌WebApi地址
        /// </summary>
        public static string GetTokenApi
        {
            get { return ConfigurationManager.AppSettings["GetTokenApi"]; }
        }

        /// <summary>
        /// 返回保存科室信息WebApi地址
        /// </summary>
        public static string SaveHospitalMgrInfo
        {
            get { return ConfigurationManager.AppSettings["SaveHospitalMgrInfo"]; }
        }

        /// <summary>
        /// 返回获取科室信息WebApi地址
        /// </summary>
        public static string GetHospitalMgrInfo
        {
            get { return ConfigurationManager.AppSettings["GetHospitalMgrInfo"]; }
        }
    }
}
