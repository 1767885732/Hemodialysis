/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技股份有限公司
 * 文件功能描述:长期留置静脉导管报表
 * 创建标识:吕志强-2016年4月25日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.IService;
using Hemo.Model;

namespace Hemo.Client.Print
{
    public partial class EstimateLongVenousReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 成员变量

        private string hemoId;

        private string patientNo;

        private DateTime createDate;

        private IHemodialysis hemodialysisService = ServiceManager.Instance.HemodialysisService;

        private IPatient patientService = ServiceManager.Instance.PatientService;

        #endregion

        #region 构造函数

        public EstimateLongVenousReport(string hemoId, string patientNo, DateTime createDate)
        {
            this.hemoId = hemoId;
            this.patientNo = patientNo;
            this.createDate = createDate;

            InitializeComponent();
            LoadReportHead();
            LoadReportDetail();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载报表头部
        /// </summary>
        private void LoadReportHead()
        {
            PatientModel.MED_PATIENTSDataTable dtPatient = patientService.GetPatientListByParams(string.Empty, hemoId);
            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                this.lblName.Text = dtPatient[0].NAME;
            }
            this.lblPatientNo.Text = patientNo;
            this.lblYear.Text = createDate.Year.ToString();
            this.lblMonth.Text = createDate.Month.ToString();
        }

        /// <summary>
        /// 加载报表明细
        /// </summary>
        private void LoadReportDetail()
        {
            this.DataSource = hemodialysisService.GetEstimateLongVenousByHemoIdAndSingleDate(hemoId, createDate);
        }

        #endregion
    }
}
