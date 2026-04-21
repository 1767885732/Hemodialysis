/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:护士加班工作量报表
 * 创建标识:贺建操-2016年8月15日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Hemo.Model;

namespace Hemo.Client.Print
{
    public partial class NurseWorkOverTimePrint : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="descurit"></param>
        public NurseWorkOverTimePrint(HemoModel.MED_HEMO_WORKOVERTIMEDataTable data,string descurit)
        {
            InitializeComponent();
            this.xrlabMoon.Text = string.Format("统计时间为:{0}",descurit);
            this.DataSource = data;
            this.DataMember = string.Empty;
        }

        #endregion
    }
}
