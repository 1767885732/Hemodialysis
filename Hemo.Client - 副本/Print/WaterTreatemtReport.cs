/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：水处理消毒记录报表
// 创建时间：2016-03-18
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
    public partial class WaterTreatemtReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        public WaterTreatemtReport(MachineModel.MED_MACHINE_MIXBARRELDataTable _data, string _quarter)
        {
            InitializeComponent();
            this.lb_quarter.Text = _quarter;
            this.DataSource = _data;
        }

        #endregion
    }
}
