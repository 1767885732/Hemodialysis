/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司
// 描述：透析充分性评估明细窗体类
// 创建时间：2015-08-18
// 创建者：吕志强
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Service;
using Hemo.IService;
using Hemo.IService.Config;
using Hemo.Model;
using DevExpress.XtraEditors;
using Hemo.Client.Doc;
using Hemo.Client.UI.Machine;

namespace Hemo.Client.UI.Patient {
    public partial class PatientSufficiencyDetail : HemoBaseFrm {
        #region 类变量

        private string currentHemoId = string.Empty;

        private string currentHemoName = string.Empty;

        private IPatient patientService = ServiceManager.Instance.PatientService;

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        private PatientKolcabaModel.MED_PATIENT_SUFFICIENCYDataTable dtRecord = null;

        private PatientKolcabaModel.MED_PATIENT_SUFFICIENCYRow currentRecordRow = null;

        #endregion

        #region 属性

        public string CurrentHemoName {
            get { return currentHemoName; }
            set { currentHemoName = value; }
        }

        public string CurrentHemoId {
            get { return currentHemoId; }
            set { currentHemoId = value; }
        }

        public PatientKolcabaModel.MED_PATIENT_SUFFICIENCYRow CurrentRecordRow {
            get { return currentRecordRow; }
            set { currentRecordRow = value; }
        }

        #endregion

        #region 构造函数

        public PatientSufficiencyDetail() {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientSufficiencyDetail_Load(object sender, EventArgs e) {
            if (currentRecordRow != null) {
                this.ctlSufficiencyRecord1.CurrentRecordRow = currentRecordRow;
                this.ctlSufficiencyRecord1.LoadMedicalRecord();
                dtRecord = patientService.GetPatientSufficiencyByHemoIDandDate(currentRecordRow.ID);
            }
            this.ctlSufficiencyRecord1.CurrentHemoId = currentHemoId;
            ctlUserLongInfo1.HEMODIALYSIS_ID = currentHemoId;
            ctlUserLongInfo1.LoadPatientInfo();
            ctlSufficiencyRecord1.loadAllTipList();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e) {
            dtRecord = this.ctlSufficiencyRecord1.GetPatientSufficiencyDataTable(dtRecord);
            int result = patientService.SavePatientSufficiency(dtRecord);
            if (result > 0) {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                XtraMessageBox.Show("保存充分性评估记录成功！");
            }
            else {
                XtraMessageBox.Show("保存充分性评估记录失败！");
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e) {
            if (currentRecordRow != null) {
                var doc = new 透析充分性评估();
                doc.PatientRow = currentRecordRow;
                doc.Pname = currentHemoName;
                doc.LoadDocumentInfo();
                var frm = new ShowPrintForm(doc);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e) {
            this.Close();
        }

        #endregion
    }
}
