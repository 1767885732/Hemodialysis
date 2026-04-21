/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技股份有限公司
 * 文件功能描述:用药记录报表
 * 创建标识:吕志强-2016年6月24日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Hemo.Model;
using Hemo.Service;
using Hemo.IService.PatientSchedule;
using Hemo.IService;
using System.Data;

namespace Hemo.Client.Print {
    public partial class DrugRecordReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 类变量

        private IPatient objPatient = ServiceManager.Instance.PatientService;

        private PatientModel.MED_PATIENTSDataTable _patientDataTable;

        #endregion

        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pHemoID"></param>
        /// <param name="drugRecord"></param>
        public DrugRecordReport(string pHemoID, DataTable drugRecord) {
            InitializeComponent();

            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CREATE_DATE", "{0:yyyy/MM/dd HH:mm:ss}")});
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "NAME")});
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CLINICAL_MANIFESTATION")});

            loadData(pHemoID, drugRecord);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 载入报表数据
        /// </summary>
        /// <param name="pHemoID">透析号 </param>
        /// <param name="drugRecord">用药记录表</param>
        private void loadData(string pHemoID, DataTable drugRecord) {
            _patientDataTable = objPatient.GetPatientListByParams("", pHemoID);
            if (_patientDataTable != null && _patientDataTable.Rows.Count > 0) {
                lblHemoID.Text = pHemoID;
                lblName.Text = _patientDataTable.Rows[0]["NAME"].ToString();
                lblSex.Text = _patientDataTable.Rows[0]["SEX"].ToString();
                lblAge.Text = _patientDataTable.Rows[0]["AGE"].ToString();
                lblDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            }

            this.DataSource = drugRecord;
        }

        #endregion
    }
}
