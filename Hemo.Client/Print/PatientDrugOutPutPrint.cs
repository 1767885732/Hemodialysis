/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:血液净化中心透析用药记录单报表
 * 创建标识:贺建操-2016年9月5日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Hemo.Model;

namespace Hemo.Client.Print
{
    public partial class PatientDrugOutPutPrint : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="patientName"></param>
        public PatientDrugOutPutPrint(ReportRelationModel.PatientDrugOutPutPrintDataTable data,string patientName)
        {
            InitializeComponent();

            this.xrLabel4.Text = patientName;
            this.DataSource = data;
            this.DataMember = "";
        }

        #endregion
    }
}
