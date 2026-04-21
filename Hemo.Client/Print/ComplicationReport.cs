/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技股份有限公司
 * 文件功能描述:并发症及其它统计报表
 * 创建标识:刘超-2016年4月3日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Hemo.Model;
using Hemo.IService.Machine;
using Hemo.Service;
using Hemo.IService.Config;
using System.Data;

namespace Hemo.Client.Print
{
    public partial class ComplicationReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="date"></param>
        /// <param name="workloadTable"></param>
        public ComplicationReport(DateTime date, HemoModel.MED_COMPLICATION_OTHERDataTable complicationTable)
        {
            InitializeComponent();

            this.xrLabel23.Text = date.Year + "-" + date.Month;
            this.DataSource = complicationTable;
        }

        #endregion
    }
}
