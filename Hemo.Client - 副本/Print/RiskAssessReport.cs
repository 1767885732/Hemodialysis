/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：血透患者风险评分表报表
// 创建时间：2015-10-25
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
    public partial class RiskAssessReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        public RiskAssessReport(HemoModel.MED_PATIENTS_ASSESSMENT_SCOREDataTable data, string title)
        {
            InitializeComponent();

            this.lbTitle.Text = string.Format("日期:{0}", title);
            this.DataSource = data;
            this.DataMember = "";
        }

        #endregion
    }
}
