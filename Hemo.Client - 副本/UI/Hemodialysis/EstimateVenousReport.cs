/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:报表
 * 创建标识:刘超-2013年6月4日
 * 
 * 修改时间:2013年9月12日
 * 修改人:刘超
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2013年12月21日
 * 修改人:刘超
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年3月31日
 * 修改人:顾伟伟
 * 修改描述:修改方法
 * ----------------------------------------------------------------*/
using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Utilities;
using Hemo.Client.Print;
using Hemo.Model;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class EstimateVenousReport :HemoBaseFrm
    {
        #region 成员变量

        private bool isTemp;

        private string hemoId;

        private string patientNo;

        #endregion

        #region 属性

        /// <summary>
        /// 是否临时
        /// </summary>
        public bool IsTemp
        {
            get { return isTemp; }
            set { isTemp = value; }
        }

        /// <summary>
        /// 透析编号
        /// </summary>
        public string HemoId
        {
            get { return hemoId; }
            set { hemoId = value; }
        }

        /// <summary>
        /// 病案号
        /// </summary>
        public string PatientNo
        {
            get { return patientNo; }
            set { patientNo = value; }
        }

        #endregion

        #region 构造函数

        public EstimateVenousReport()
        {
            this.InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EstimateVenousReport_Load(object sender, EventArgs e)
        {
            this.Text = isTemp ? "临时留置静脉导管评估单" : "长期留置静脉导管评估单";
            this.barEditReportDate.EditValue = DateTime.Now.Date;
            LoadReport();
        }

        /// <summary>
        /// 日期改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEditReportDate_EditValueChanged(object sender, EventArgs e)
        {
            if (this.barEditReportDate.EditValue == null)
            {
                XtraMessageBox.Show("请选择月份！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            LoadReport();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载报表
        /// </summary>
        private void LoadReport()
        {
            if (isTemp)
            {
                EstimateVenousCatheterReport report = new EstimateVenousCatheterReport(hemoId, patientNo, Utility.CDate(this.barEditReportDate.EditValue.ToString()));
                this.pcReport.PrintingSystem = report.PrintingSystem;
                report.CreateDocument();
            }
            else
            {
                EstimateLongVenousReport report = new EstimateLongVenousReport(hemoId, patientNo, Utility.CDate(this.barEditReportDate.EditValue.ToString()));
                this.pcReport.PrintingSystem = report.PrintingSystem;
                report.CreateDocument();
            }
        }

        #endregion
    }
}