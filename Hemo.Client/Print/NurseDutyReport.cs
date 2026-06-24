/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:护士值班信息报表
 * 创建标识:吕志强-2016年7月10日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Hemo.Model;

namespace Hemo.Client.Print
{
    public partial class NurseDutyReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        public NurseDutyReport(ReportRelationModel.MED_PATIENTDUTYDataTable dt,string timeduty)
        {
            InitializeComponent();

            this.xrLabel2.Text = string.Format("值班时间:{0}",timeduty);
            this.DataSource = dt;
            this.DataMember = "";
        }

        #endregion
    }
}
