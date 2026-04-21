/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：工作量统计报表
// 创建时间：2018-1-29
// 创建者：贺建操
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
    public partial class WorkloadReportSingle : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="date"></param>
        /// <param name="workloadTable"></param>
        public WorkloadReportSingle(string title, HemoModel.MED_WORKLOADDataTable workloadTable)
        {
            InitializeComponent();
            foreach(var row in workloadTable)
            {
                row.AREANAME = (row.LSQYTX + row.XTLS + row.XLLS + row.XYGL + row.WGSXT + row.GTLTX + row.DHSXF + row.DGHLLS + row.DGHLCQ + row.NLCC + row.LSZGSCSY + row.CQZGSCSY + row.QJRC + row.XTJC + row.XDJH + row.XJZH + row.BW).ToString();
            }
            this.xrLabel23.Text = title;
            this.DataSource = workloadTable;
        }
        #endregion
    }
}
