/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：血液净化中心终止透析患者登记报表
// 创建时间：2015-11-24
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
using Hemo.IService.Machine;
using Hemo.Service;
using Hemo.Model;
using Hemo.Utilities;

namespace Hemo.Client.Print
{
    public partial class RegDealthReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        public RegDealthReport(PatientModel.MED_PATIENTREGDEALTHDataTable _repairDate)
        {
            InitializeComponent();
            this.DataSource = _repairDate;
            this.DataMember = "";
        }

        #endregion
    }
}
