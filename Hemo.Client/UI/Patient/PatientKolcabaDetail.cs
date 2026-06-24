/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者Kolcaba详细信息编辑类
// 创建时间：2016-07-5
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
using Hemo.Model;
using Hemo.IService;
using Hemo.Service;
using Hemo.IService.Config;
using DevExpress.XtraEditors;
using Hemo.Client.Doc;
using Hemo.Client.UI.Machine;

namespace Hemo.Client.UI.Patient
{
    public partial class PatientKolcabaDetail : HemoBaseFrm
    {
        #region 类变量

        private IPatient patientService = ServiceManager.Instance.PatientService;

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        private PatientKolcabaModel.MED_PATIENT_KOLCABADataTable dtRecord = null;

        private PatientKolcabaModel.MED_PATIENT_KOLCABARow currentRecordRow = null;

        private string currentHemoId = string.Empty;
        private string currentHemoName = string.Empty;

        #endregion

        #region 属性

        public string CurrentHemoName
        {
            get { return currentHemoName; }
            set { currentHemoName = value; }
        }
        public string CurrentHemoId
        {
            get { return currentHemoId; }
            set { currentHemoId = value; }
        }
        public PatientKolcabaModel.MED_PATIENT_KOLCABARow CurrentRecordRow
        {
            get { return currentRecordRow; }
            set { currentRecordRow = value; }
        }

        #endregion

        #region 构造函数

        public PatientKolcabaDetail()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ctlKolcabaRecord1.checkFill())
            {
                dtRecord = this.ctlKolcabaRecord1.GetPatientKolcabaDataTable(dtRecord);
                int result = patientService.SavePatientKolcaba(dtRecord);
                if (result > 0)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    XtraMessageBox.Show("保存病历记录成功！");
                }
                else
                {
                    XtraMessageBox.Show("保存病历记录失败！");
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            if (currentRecordRow != null)
            {
                var doc = new Kolcaba的舒适状况量表();
                doc.PatientRow = currentRecordRow;
                doc.Pname = currentHemoName;
                doc.LoadDocumentInfo();
                var frm = new ShowPrintForm(doc);
                frm.ShowDialog();
            }
        }

        private void PatientKolcabaDetail_Load(object sender, EventArgs e)
        {
            lbKolcaba.Visible = false;
            if (currentRecordRow != null)
            {
                this.ctlKolcabaRecord1.CurrentRecordRow = currentRecordRow;
                this.ctlKolcabaRecord1.LoadMedicalRecord();
                lbKolcaba.Visible = true;

                dtRecord = patientService.GetPatientKolcabaByHemoIDandDate(currentRecordRow.HEMODIALYSIS_ID, currentRecordRow.CREATEDATE);
                lbKolcaba.Text = "Kolcaba的舒适状况量：" + dtRecord[0].TOTALSCORE.ToString();
            }
            this.ctlKolcabaRecord1.CurrentHemoId = currentHemoId;
        }

        #endregion
    }
}
