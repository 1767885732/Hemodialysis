/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:耗材记录报表
 * 创建标识:贺建操-2016年5月8日
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

namespace Hemo.Client.Print {
    public partial class HemoMaterialReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 类变量

        private IPatient objPatient = ServiceManager.Instance.PatientService;
        private IMaterial objMaterial = ServiceManager.Instance.MaterialService;
        private PatientModel.MED_PATIENTSDataTable _patientDataTable;
        private MaterialModel.MED_HEMO_MATERIAL_REPORTDataTable _materialReportDataTable;

        #endregion

        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pHemoID"></param>
        /// <param name="pUserMaterialID"></param>
        public HemoMaterialReport(string pHemoID, string pUserMaterialID) {
            // MaterialModel.MED_HEMO_MATERIAL_REPORTDataTable _materialReportDataTable = this.materialModel1.MED_HEMO_MATERIAL_REPORT;
          //  _materialReportDataTable = objMaterial.GetMaterialReport(pUserMaterialID);
            //use_material_id
            InitializeComponent();
            //xrTable1.data
            loadData(pHemoID, "");
        }

        #endregion

        #region 方法

        /// <summary>
        /// 载入报表数据
        /// </summary>
        /// <param name="pHemoID">透析号 </param>
        /// <param name="pUserMaterialID">耗材领用单编号</param>
        private void loadData(string pHemoID, string pUserMaterialID) {
            _patientDataTable = objPatient.GetPatientListByParams("", pHemoID);
            if (_patientDataTable != null && _patientDataTable.Rows.Count > 0) {
                lblHemoID.Text = pHemoID;
                lblName.Text = _patientDataTable.Rows[0]["NAME"].ToString();
                lblSex.Text = _patientDataTable.Rows[0]["SEX"].ToString();
                lblAge.Text = _patientDataTable.Rows[0]["AGE"].ToString();
                lblDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        #endregion
    }
}
