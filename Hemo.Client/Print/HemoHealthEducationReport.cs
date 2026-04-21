/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技股份有限公司
 * 文件功能描述:透析器复用情况记录单报表
 * 创建标识:刘超-2016年5月16日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.Print {
    public partial class HemoHealthEducationReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        public HemoHealthEducationReport() {
            InitializeComponent();
            xrLabel1.Text = Utilities.Utility.GetHospitalName() + "透析器复用情况记录单";
        }

        #endregion
    }
}
