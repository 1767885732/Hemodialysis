/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：根据病人透析单信息打印透析单
// 创建时间：2013-07-25
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
//
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.Controls;
using Hemo.Client.Doc;
using Hemo.Service;
using Hemo.IService.Config;
using Hemo.Model;
using Hemo.IService;

namespace Hemo.Client.UI.Hemodialysis {
    public partial class ShowPrintCureInfo :HemoBaseFrm
    {
        #region 变量
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        public ShowPrintCureInfo() {
            InitializeComponent();
        }
        #region 事件
        private CtlMedicalDocumentContainer _medicalDocContainer = new CtlMedicalDocumentContainer();

        public void LoadDocumentGrid(DataSet ds) {
            CtlMedicalDocument document = new CtlMedicalDocument(ds, 0, 0);
            document.IsShowGrid(true);
            _medicalDocContainer.CurrentMedicalDocument = document;
            documentContainerHost.Child = _medicalDocContainer;
        }


        public void LoadHealthEducation(string pHemoID,string pId) {
            健康宣教指导表 document = new 健康宣教指导表();
            PatientModel.MED_PATIENTSDataTable _patientDataTable = objPatient.GetPatientListByParams("", pHemoID);
            if (pHemoID.Length >0) {
                DataTable dt = objHemodialysisService.GetHealthEducationReportByHemoID(pHemoID, pId);
               // PatientModel.MED_PATIENTSRow PatientRow 
                document.PatientRow = _patientDataTable.Rows[0] as PatientModel.MED_PATIENTSRow;
                if (dt != null && dt.Rows.Count > 0) {
                    document.HealthDataTable = dt;
                }
                document.LoadDocumentInfo();
            }
            _medicalDocContainer.CurrentMedicalDocument = document;
            documentContainerHost.Child = _medicalDocContainer;
        }
        #endregion
    }
}