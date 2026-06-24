/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：工作量统计报表
// 创建时间：2016-04-04
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

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
    public partial class WorkloadReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="date"></param>
        /// <param name="workloadTable"></param>
        public WorkloadReport(string title, HemoModel.MED_WORKLOADDataTable workloadTable)
        {
            InitializeComponent();
             
            this.xrLabel23.Text = title;
            this.DataSource = workloadTable;
        }
        #endregion
    }
}
