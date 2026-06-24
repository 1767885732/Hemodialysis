/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技股份有限公司
 * 文件功能描述:交接班记录报表
 * 创建标识:刘超-2016年4月10日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.Print {
    public partial class HemoChangeWorkReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        public HemoChangeWorkReport() {
            InitializeComponent();
            xrLabel8.Text = Utilities.Utility.GetHospitalName();
        }

        #endregion
    }
}
