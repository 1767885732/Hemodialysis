/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：透析用水内毒素检测报表
// 创建时间：2015-12-27
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

namespace Hemo.Client.Print
{
    public partial class WaterHemoReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        public WaterHemoReport(MachineModel.MED_MACHINE_MIXBARRELDataTable _data, string _quarter)
        {
            InitializeComponent();
            this.lb_quarter.Text = _quarter;
            this.DataSource = _data;
        }

        #endregion
    }
}
