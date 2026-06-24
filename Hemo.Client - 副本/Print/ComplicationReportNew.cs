/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技股份有限公司
 * 文件功能描述:新并发症及其它统计报表
 * 创建标识:刘超-2016年7月22日
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
    public partial class ComplicationReportNew : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="date"></param>
        /// <param name="workloadTable"></param>
        public ComplicationReportNew(DateTime date, HemoModel.MED_COMPLICATION_OTHERDataTable complicationTable)
        {
            InitializeComponent();

            foreach (HemoModel.MED_COMPLICATION_OTHERRow dataRow in complicationTable.Rows)
            {
                foreach (DataColumn dc in complicationTable.Columns)
                {
                    if (dataRow[dc].ToString() == "是")
                    {
                        dataRow[dc] = "1";
                    }
                    else if (dataRow[dc].ToString() == "否")
                    {
                        dataRow[dc] = "0"; 
                    }
                }
            }

            this.xrLabel23.Text = "统计月份:" + date.Year + "-" + date.Month;
            this.DataSource = complicationTable;
        }

        #endregion
    }
}
