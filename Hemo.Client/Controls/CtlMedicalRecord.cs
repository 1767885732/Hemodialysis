/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司股份有限公司
// 文件名：CtlMedicalRecord.cs
// 文件功能描述： 用户原发病管理
// 创建标识：刘超 2013-07-22
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.Client.Core;

namespace Hemo.Client.Controls
{
    public partial class CtlMedicalRecord : DevExpress.XtraEditors.XtraUserControl
    {
        #region 成员变量

        private PatientScheduleModel.MED_PATIENTRECORDRow currentRecordRow = null;

        private string currentHemoId = string.Empty;

        #endregion

        #region

        public string CurrentHemoId
        {
            get { return currentHemoId; }
            set { currentHemoId = value; }
        }

        public PatientScheduleModel.MED_PATIENTRECORDRow CurrentRecordRow
        {
            get { return currentRecordRow; }
            set { currentRecordRow = value; }
        }

        #endregion

        #region 构造函数

        public CtlMedicalRecord()
        {
            InitializeComponent();
        }

        #endregion

        #region 方法

        public void LoadMedicalRecord()
        {
            if (currentRecordRow != null)
            {
                this.txtAction.Text = currentRecordRow["ACTION"].ToString();
                this.txtPresentIllness.Text = currentRecordRow["PRESENTILLNESS"].ToString();
                this.chkSugar_Diabetes.Checked = currentRecordRow["SUGAR_DIABETES"].ToString() == "1" ? true : false;
                this.chkHigh_Blood_Pressure.Checked = currentRecordRow["HIGH_BLOOD_PRESSURE"].ToString() == "1" ? true : false;
                this.chkChronic_Nephritis.Checked = currentRecordRow["CHRONIC_NEPHRITIS"].ToString() == "1" ? true : false;
                this.chkPolycystic_Kidney.Checked = currentRecordRow["POLYCYSTIC_KIDNEY"].ToString() == "1" ? true : false;
                this.chkUrinary_Calculus.Checked = currentRecordRow["URINARY_CALCULUS"].ToString() == "1" ? true : false;
                this.txtOther_Protopathy.Text = currentRecordRow["OTHER_PROTOPATHY"].ToString();
                this.chkDrug_Allergy_History.Checked = currentRecordRow["DRUG_ALLERGY_HISTORY"].ToString() == "1" ? true : false;
                this.txtOther_Past_History.Text = currentRecordRow["OTHER_PAST_HISTORY"].ToString();
                this.txtT.Text = currentRecordRow["T"].ToString();
                this.txtP.Text = currentRecordRow["P"].ToString();
                this.txtR.Text = currentRecordRow["R"].ToString();
                this.txtBP.Text = currentRecordRow["BP"].ToString();
                this.txtConsciousness.Text = currentRecordRow["CONSCIOUSNESS"].ToString();
                this.radBody_Position.EditValue = currentRecordRow["BODY_POSITION"].ToString();
                this.radCheck_Up.EditValue = currentRecordRow["CHECK_UP"].ToString();
                this.radFull_Body_Skin_1.EditValue = currentRecordRow["FULL_BODY_SKIN_1"].ToString();
                this.radFull_Body_Skin_2.EditValue = currentRecordRow["FULL_BODY_SKIN_2"].ToString();
                this.radSickly_Look.EditValue = currentRecordRow["SICKLY_LOOK"].ToString();
                this.radAnemia.EditValue = currentRecordRow["ANEMIA"].ToString();
                this.radAscites.EditValue = currentRecordRow["ASCITES"].ToString();
                this.radCardiacdullness.EditValue = currentRecordRow["CARDIACDULLNESS"].ToString();
                this.radHeart_Rate.EditValue = currentRecordRow["HEART_RATE"].ToString();
                this.radEdema.EditValue = currentRecordRow["EDEMA"].ToString();
                this.radNoise.EditValue = currentRecordRow["NOISE"].ToString();
                this.txtDouble_Lung_Auscultation.Text = currentRecordRow["DOUBLE_LUNG_AUSCULTATION"].ToString();
                this.txtWeight.Text = currentRecordRow["WEIGHT"].ToString();
                this.txtRabat.Text = currentRecordRow["RABAT"].ToString();
                this.radRenal_Size.EditValue = currentRecordRow["RENAL_SIZE"].ToString();
                this.radStructure.EditValue = currentRecordRow["STRUCTURE"].ToString();
                this.txtOther_B_Ultrasonic.Text = currentRecordRow["OTHER_B_ULTRASONIC"].ToString();
                this.txtRBC.Text = currentRecordRow["RBC"].ToString();
                this.txtWBC.Text = currentRecordRow["WBC"].ToString();
                this.txtHGB.Text = currentRecordRow["HGB"].ToString();
                this.txtPLT.Text = currentRecordRow["PLT"].ToString();
                this.txtHCT.Text = currentRecordRow["HCT"].ToString();
                this.txtBUN.Text = currentRecordRow["BUN"].ToString();
                this.txtSCR.Text = currentRecordRow["SCR"].ToString();
                this.txtUA.Text = currentRecordRow["UA"].ToString();
                this.txtK.Text = currentRecordRow["K"].ToString();
                this.txtCa.Text = currentRecordRow["CA"].ToString();
                this.txtPh.Text = currentRecordRow["PH"].ToString();
                this.chkIMX.Checked = currentRecordRow["IMX"].ToString() == "1" ? true : false;
                this.chkHCV.Checked = currentRecordRow["HCV"].ToString() == "1" ? true : false;
                this.chkTPPA.Checked = currentRecordRow["TPPA"].ToString() == "1" ? true : false;
                this.chkAIDS.Checked = currentRecordRow["AIDS"].ToString() == "1" ? true : false;
                this.txtOther_Infection.Text = currentRecordRow["OTHER_INFECTION"].ToString();
                this.radClotting_Mechanism.EditValue = currentRecordRow["CLOTTING_MECHANISM"].ToString();
                this.txtPT.Text = currentRecordRow["PT"].ToString();
                this.txtAPTT.Text = currentRecordRow["APTT"].ToString();
                this.txtINR.Text = currentRecordRow["INR"].ToString();
                this.txtSerum_Iron.Text = currentRecordRow["SERUM_IRON"].ToString();
                this.txtTibc.Text = currentRecordRow["TIBC"].ToString();
                this.txtFerroprotein.Text = currentRecordRow["FERROPROTEIN"].ToString();
                this.txtPTH.Text = currentRecordRow["PTH"].ToString();
                this.txtDrug_Application.Text = currentRecordRow["DRUG_APPLICATION"].ToString();
                this.txtDiagnosis.Text = currentRecordRow["DIAGNOSIS"].ToString();
                this.txtTreatment_Plan.Text = currentRecordRow["TREATMENT_PLAN"].ToString();
                this.txtDrug_Use.Text = currentRecordRow["DRUG_USE"].ToString();
            }
        }

        public PatientScheduleModel.MED_PATIENTRECORDDataTable GetPatientRecordDataTable(PatientScheduleModel.MED_PATIENTRECORDDataTable dtRecord)
        {
            if (dtRecord == null)
            {
                //新增
                dtRecord = new PatientScheduleModel.MED_PATIENTRECORDDataTable();
                var row = dtRecord.NewMED_PATIENTRECORDRow();
                row.ID = Guid.NewGuid().ToString().Trim();
                row.HEMODIALYSIS_ID = currentHemoId;
                //row.PASTILLNESS = this.memoEdit_bottom.Text.Trim();
                row.CREATEBY = HemoApplicationContext.Current.CurrentUser.USER_ID;
                row.CREATEDATE = DateTime.Now.Date;
                row.LASTUPDATEBY = HemoApplicationContext.Current.CurrentUser.USER_ID;
                row.LASTUPDATEDATE = System.DateTime.Now;
                row.ACTION = this.txtAction.Text;
                row.PRESENTILLNESS = this.txtPresentIllness.Text;
                row.SUGAR_DIABETES = this.chkSugar_Diabetes.Checked ? "1" : "0";
                row.HIGH_BLOOD_PRESSURE = this.chkHigh_Blood_Pressure.Checked ? "1" : "0";
                row.CHRONIC_NEPHRITIS = this.chkChronic_Nephritis.Checked ? "1" : "0";
                row.POLYCYSTIC_KIDNEY = this.chkPolycystic_Kidney.Checked ? "1" : "0";
                row.URINARY_CALCULUS = this.chkUrinary_Calculus.Checked ? "1" : "0";
                row.OTHER_PROTOPATHY = this.txtOther_Protopathy.Text;
                row.DRUG_ALLERGY_HISTORY = this.chkDrug_Allergy_History.Checked ? "1" : "0";
                row.OTHER_PAST_HISTORY = this.txtOther_Past_History.Text;
                row.PASTILLNESS = this.txtOther_Past_History.Text;
                row.T = Utility.CDecimal(this.txtT.Text);
                row.P = Utility.CDecimal(this.txtP.Text);
                row.R = Utility.CDecimal(this.txtR.Text);
                row.BP = Utility.CDecimal(this.txtBP.Text);
                row.CONSCIOUSNESS = this.txtConsciousness.Text;
                row.BODY_POSITION = this.radBody_Position.EditValue.ToString();
                row.CHECK_UP = this.radCheck_Up.EditValue.ToString();
                row.FULL_BODY_SKIN_1 = this.radFull_Body_Skin_1.EditValue.ToString();
                row.FULL_BODY_SKIN_2 = this.radFull_Body_Skin_2.EditValue.ToString();
                row.SICKLY_LOOK = this.radSickly_Look.EditValue.ToString();
                row.ANEMIA = this.radAnemia.EditValue.ToString();
                row.ASCITES = this.radAscites.EditValue.ToString();
                row.CARDIACDULLNESS = this.radCardiacdullness.EditValue.ToString();
                row.HEART_RATE = this.radHeart_Rate.EditValue.ToString();
                row.EDEMA = this.radEdema.EditValue.ToString();
                row.NOISE = this.radNoise.EditValue.ToString();
                row.DOUBLE_LUNG_AUSCULTATION = this.txtDouble_Lung_Auscultation.Text;
                row.WEIGHT = Utility.CDecimal(this.txtWeight.Text);
                row.RABAT = this.txtRabat.Text;
                row.RENAL_SIZE = this.radRenal_Size.EditValue.ToString();
                row.STRUCTURE = this.radStructure.EditValue.ToString();
                row.OTHER_B_ULTRASONIC = this.txtOther_B_Ultrasonic.Text;
                row.RBC = Utility.CDecimal(this.txtRBC.Text);
                row.WBC = Utility.CDecimal(this.txtWBC.Text);
                row.HGB = Utility.CDecimal(this.txtHGB.Text);
                row.PLT = Utility.CDecimal(this.txtPLT.Text);
                row.HCT = Utility.CDecimal(this.txtHCT.Text);
                row.BUN = Utility.CDecimal(this.txtBUN.Text);
                row.SCR = Utility.CDecimal(this.txtSCR.Text);
                row.UA = Utility.CDecimal(this.txtUA.Text);
                row.K = Utility.CDecimal(this.txtK.Text);
                row.CA = Utility.CDecimal(this.txtCa.Text);
                row.PH = Utility.CDecimal(this.txtPh.Text);
                row.IMX = this.chkIMX.Checked ? "1" : "0";
                row.HCV = this.chkHCV.Checked ? "1" : "0";
                row.TPPA = this.chkTPPA.Checked ? "1" : "0";
                row.AIDS = this.chkAIDS.Checked ? "1" : "0";
                row.OTHER_INFECTION = this.txtOther_Infection.Text;
                row.CLOTTING_MECHANISM = this.radClotting_Mechanism.EditValue.ToString();
                row.PT = Utility.CDecimal(this.txtPT.Text);
                row.APTT = Utility.CDecimal(this.txtAPTT.Text);
                row.INR = Utility.CDecimal(this.txtINR.Text);
                row.SERUM_IRON = Utility.CDecimal(this.txtSerum_Iron.Text);
                row.TIBC = Utility.CDecimal(this.txtTibc.Text);
                row.FERROPROTEIN = Utility.CDecimal(this.txtFerroprotein.Text);
                row.PTH = Utility.CDecimal(this.txtPTH.Text);
                row.DRUG_APPLICATION = this.txtDrug_Application.Text;
                row.DIAGNOSIS = this.txtDiagnosis.Text;
                row.TREATMENT_PLAN = Utility.CDecimal(this.txtTreatment_Plan.Text);
                row.DRUG_USE = Utility.CDecimal(this.txtDrug_Use.Text);
                dtRecord.AddMED_PATIENTRECORDRow(row);
            }
            else
            {
                //编辑
                //dtRecord[0].PASTILLNESS = this.memoEdit_bottom.Text.Trim();
                dtRecord[0].LASTUPDATEBY = HemoApplicationContext.Current.CurrentUser.USER_ID;
                dtRecord[0].LASTUPDATEDATE = System.DateTime.Now;
                dtRecord[0].ACTION = this.txtAction.Text;
                dtRecord[0].PRESENTILLNESS = this.txtPresentIllness.Text;
                dtRecord[0].SUGAR_DIABETES = this.chkSugar_Diabetes.Checked ? "1" : "0";
                dtRecord[0].HIGH_BLOOD_PRESSURE = this.chkHigh_Blood_Pressure.Checked ? "1" : "0";
                dtRecord[0].CHRONIC_NEPHRITIS = this.chkChronic_Nephritis.Checked ? "1" : "0";
                dtRecord[0].POLYCYSTIC_KIDNEY = this.chkPolycystic_Kidney.Checked ? "1" : "0";
                dtRecord[0].URINARY_CALCULUS = this.chkUrinary_Calculus.Checked ? "1" : "0";
                dtRecord[0].OTHER_PROTOPATHY = this.txtOther_Protopathy.Text;
                dtRecord[0].DRUG_ALLERGY_HISTORY = this.chkDrug_Allergy_History.Checked ? "1" : "0";
                dtRecord[0].OTHER_PAST_HISTORY = this.txtOther_Past_History.Text;
                dtRecord[0].PASTILLNESS = this.txtOther_Past_History.Text;
                dtRecord[0].T = Utility.CDecimal(this.txtT.Text);
                dtRecord[0].P = Utility.CDecimal(this.txtP.Text);
                dtRecord[0].R = Utility.CDecimal(this.txtR.Text);
                dtRecord[0].BP = Utility.CDecimal(this.txtBP.Text);
                dtRecord[0].CONSCIOUSNESS = this.txtConsciousness.Text;
                dtRecord[0].BODY_POSITION = this.radBody_Position.EditValue.ToString();
                dtRecord[0].CHECK_UP = this.radCheck_Up.EditValue.ToString();
                dtRecord[0].FULL_BODY_SKIN_1 = this.radFull_Body_Skin_1.EditValue.ToString();
                dtRecord[0].FULL_BODY_SKIN_2 = this.radFull_Body_Skin_2.EditValue.ToString();
                dtRecord[0].SICKLY_LOOK = this.radSickly_Look.EditValue.ToString();
                dtRecord[0].ANEMIA = this.radAnemia.EditValue.ToString();
                dtRecord[0].ASCITES = this.radAscites.EditValue.ToString();
                dtRecord[0].CARDIACDULLNESS = this.radCardiacdullness.EditValue.ToString();
                dtRecord[0].HEART_RATE = this.radHeart_Rate.EditValue.ToString();
                dtRecord[0].EDEMA = this.radEdema.EditValue.ToString();
                dtRecord[0].NOISE = this.radNoise.EditValue.ToString();
                dtRecord[0].DOUBLE_LUNG_AUSCULTATION = this.txtDouble_Lung_Auscultation.Text;
                dtRecord[0].WEIGHT = Utility.CDecimal(this.txtWeight.Text);
                dtRecord[0].RABAT = this.txtRabat.Text;
                dtRecord[0].RENAL_SIZE = this.radRenal_Size.EditValue.ToString();
                dtRecord[0].STRUCTURE = this.radStructure.EditValue.ToString();
                dtRecord[0].OTHER_B_ULTRASONIC = this.txtOther_B_Ultrasonic.Text;
                dtRecord[0].RBC = Utility.CDecimal(this.txtRBC.Text);
                dtRecord[0].WBC = Utility.CDecimal(this.txtWBC.Text);
                dtRecord[0].HGB = Utility.CDecimal(this.txtHGB.Text);
                dtRecord[0].PLT = Utility.CDecimal(this.txtPLT.Text);
                dtRecord[0].HCT = Utility.CDecimal(this.txtHCT.Text);
                dtRecord[0].BUN = Utility.CDecimal(this.txtBUN.Text);
                dtRecord[0].SCR = Utility.CDecimal(this.txtSCR.Text);
                dtRecord[0].UA = Utility.CDecimal(this.txtUA.Text);
                dtRecord[0].K = Utility.CDecimal(this.txtK.Text);
                dtRecord[0].CA = Utility.CDecimal(this.txtCa.Text);
                dtRecord[0].PH = Utility.CDecimal(this.txtPh.Text);
                dtRecord[0].IMX = this.chkIMX.Checked ? "1" : "0";
                dtRecord[0].HCV = this.chkHCV.Checked ? "1" : "0";
                dtRecord[0].TPPA = this.chkTPPA.Checked ? "1" : "0";
                dtRecord[0].AIDS = this.chkAIDS.Checked ? "1" : "0";
                dtRecord[0].OTHER_INFECTION = this.txtOther_Infection.Text;
                dtRecord[0].CLOTTING_MECHANISM = this.radClotting_Mechanism.EditValue.ToString();
                dtRecord[0].PT = Utility.CDecimal(this.txtPT.Text);
                dtRecord[0].APTT = Utility.CDecimal(this.txtAPTT.Text);
                dtRecord[0].INR = Utility.CDecimal(this.txtINR.Text);
                dtRecord[0].SERUM_IRON = Utility.CDecimal(this.txtSerum_Iron.Text);
                dtRecord[0].TIBC = Utility.CDecimal(this.txtTibc.Text);
                dtRecord[0].FERROPROTEIN = Utility.CDecimal(this.txtFerroprotein.Text);
                dtRecord[0].PTH = Utility.CDecimal(this.txtPTH.Text);
                dtRecord[0].DRUG_APPLICATION = this.txtDrug_Application.Text;
                dtRecord[0].DIAGNOSIS = this.txtDiagnosis.Text;
                dtRecord[0].TREATMENT_PLAN = Utility.CDecimal(this.txtTreatment_Plan.Text);
                dtRecord[0].DRUG_USE = Utility.CDecimal(this.txtDrug_Use.Text);
            }
            return dtRecord;
        }

        #endregion
    }
}
