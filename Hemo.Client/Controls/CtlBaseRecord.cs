/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：病历基本资料用户控件类
// 创建时间：2015-07-31
// 创建者：吕志强
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/
using System;
using System.Data;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.Client.Core;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Client.UI.Machine;

namespace Hemo.Client.Controls
{
    public partial class CtlBaseRecord : ViewBase
    {
        #region 类变量

        private string hemoId = string.Empty;

        private DateTime beginDate;

        private PatientModel.MED_BASE_RECORDDataTable baseRecord = null;

        private IHemodialysis hemodialysisService = ServiceManager.Instance.HemodialysisService;

        #endregion

        #region 属性

        /// <summary>
        /// 透析编号
        /// </summary>
        public string HemoId
        {
            get { return hemoId; }
            set { hemoId = value; }
        }

        /// <summary>
        /// 透析开始时间
        /// </summary>
        public DateTime BeginDate
        {
            get { return beginDate; }
            set { beginDate = value; }
        }

        /// <summary>
        /// 病历基本资料记录
        /// </summary>
        public PatientModel.MED_BASE_RECORDDataTable BaseRecord
        {
            get { return baseRecord; }
            set { baseRecord = value; }
        }

        #endregion

        #region 构造函数

        public CtlBaseRecord()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 抽烟选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radSMOKE_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtSMOKE_NUM.Enabled = this.radSMOKE.SelectedIndex == 1 ? true : false;
            //this.txtSMOKE_NUM.Enabled = this.radSMOKE.SelectedIndex == 1 ? true : false;
            this.txtSMOKE_NUM.Text = this.radSMOKE.SelectedIndex == 0 ? string.Empty : this.txtSMOKE_NUM.Text;
            //this.txtSMOKE_NUM.Text = this.radSMOKE.SelectedIndex == 0 ? string.Empty : this.txtSMOKE_NUM.Text;
        }

        /// <summary>
        /// 腹膜透析选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radPD_EXIST_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtPD_YEAR.Enabled = this.radPD_EXIST.SelectedIndex == 1 ? true : false;
            this.txtPD_YEAR.Text = this.radPD_EXIST.SelectedIndex == 0 ? string.Empty : this.txtPD_YEAR.Text;
        }

        /// <summary>
        /// 肾移植选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radRENAL_TRANSPLANT_EXIST_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtRENAL_TRANSPLANT_YEAR.Enabled = this.radRENAL_TRANSPLANT_EXIST.SelectedIndex == 1 ? true : false;
            this.txtRENAL_TRANSPLANT_YEAR.Text = this.radRENAL_TRANSPLANT_EXIST.SelectedIndex == 0 ? string.Empty : this.txtRENAL_TRANSPLANT_YEAR.Text;
        }

        /// <summary>
        /// 酗酒选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radXUJIU_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtXUJIU_DESC.Enabled = this.radXUJIU.SelectedIndex == 1 ? true : false;
            this.txtXUJIU_DESC.Text = this.radXUJIU.SelectedIndex == 0 ? string.Empty : this.txtXUJIU_DESC.Text;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载患者病历基本资料
        /// </summary>
        public void LoadBaseRecord()
        {
            this.txtDIALYSIS_BEGIN.EditValue = beginDate;
            this.txtINTO_DATE.EditValue = null;
            this.txtDIALYSIS_END.EditValue = null;
            this.txtDEAD_DATE.EditValue = null;
            //this.txtDIALYSIS_BEGIN.Enabled = false;
            HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable progressNodeDataTable = null;
            progressNodeDataTable = hemodialysisService.GetPatientProgressNoteByHemoId(hemoId);
            if (progressNodeDataTable.Rows.Count > 0 && progressNodeDataTable != null)
            {
                foreach (HemodialysisModel.MED_PATIENT_PROGRESS_NOTERow row in progressNodeDataTable.Rows)
                {
                    this.txtPROGRESS_NODE.Text = "记录时间：" + row.CREATE_DATE.ToString("yyyy-MM-dd") + " 用药记录：" + (row["PROGRESS_NODE"] ==DBNull.Value ? "无" : row.PROGRESS_NODE.ToString()) + "\r\n" + this.txtPROGRESS_NODE.Text;
                }
            }

            ResetBaseRecord();

            if (baseRecord != null && baseRecord.Rows.Count > 0)
            {
                this.chkIsUp.Checked = baseRecord.Rows[0]["IS_UP"] != DBNull.Value ? (baseRecord.Rows[0]["IS_UP"].ToString() == "1" ? true : false) : false;
                this.chkCGN.Checked = baseRecord.Rows[0]["CGN"] != DBNull.Value ? (baseRecord.Rows[0]["CGN"].ToString() == "1" ? true : false) : false;
                this.chkCIN.Checked = baseRecord.Rows[0]["CIN"] != DBNull.Value ? (baseRecord.Rows[0]["CIN"].ToString() == "1" ? true : false) : false;
                this.chkDN.Checked = baseRecord.Rows[0]["DN"] != DBNull.Value ? (baseRecord.Rows[0]["DN"].ToString() == "1" ? true : false) : false;
                this.chkHRD.Checked = baseRecord.Rows[0]["HRD"] != DBNull.Value ? (baseRecord.Rows[0]["HRD"].ToString() == "1" ? true : false) : false;
                this.chkPCKD.Checked = baseRecord.Rows[0]["PCKD"] != DBNull.Value ? (baseRecord.Rows[0]["PCKD"].ToString() == "1" ? true : false) : false;
                this.chkUUO.Checked = baseRecord.Rows[0]["UUO"] != DBNull.Value ? (baseRecord.Rows[0]["UUO"].ToString() == "1" ? true : false) : false;
                this.chkRENAL_TUMOR.Checked = baseRecord.Rows[0]["RENAL_TUMOR"] != DBNull.Value ? (baseRecord.Rows[0]["RENAL_TUMOR"].ToString() == "1" ? true : false) : false;
                this.chkOTHER_PROTOPATHY.Checked = baseRecord.Rows[0]["OTHER_PROTOPATHY"] != DBNull.Value ? (baseRecord.Rows[0]["OTHER_PROTOPATHY"].ToString() == "1" ? true : false) : false;
                this.txtOTHER_PROTOPATHY.Text = baseRecord.Rows[0]["OTHER_PROTOPATHY_TEXT"].ToString();
                // add by pagi at 20170105
                this.chkIsFirstDialysis.Checked = baseRecord.Rows[0]["IsFirstDialysis"] != DBNull.Value ? (baseRecord.Rows[0]["IsFirstDialysis"].ToString() == "1" ? true : false) : false;
                this.radDIALYSIS_YEARS.EditValue = baseRecord.Rows[0]["DIALYSIS_YEARS"].ToString();

                this.chkTFSB.Checked = baseRecord.Rows[0]["TFSB"] != DBNull.Value ? (baseRecord.Rows[0]["TFSB"].ToString() == "1" ? true : false) : false;
                this.chkBYBX.Checked = baseRecord.Rows[0]["BYBX"] != DBNull.Value ? (baseRecord.Rows[0]["BYBX"].ToString() == "1" ? true : false) : false;
                this.chkHTN.Checked = baseRecord.Rows[0]["HTN"] != DBNull.Value ? (baseRecord.Rows[0]["HTN"].ToString() == "1" ? true : false) : false;
                this.chkDM.Checked = baseRecord.Rows[0]["DM"] != DBNull.Value ? (baseRecord.Rows[0]["DM"].ToString() == "1" ? true : false) : false;
                this.chkCAD.Checked = baseRecord.Rows[0]["CAD"] != DBNull.Value ? (baseRecord.Rows[0]["CAD"].ToString() == "1" ? true : false) : false;
                this.chkCHF.Checked = baseRecord.Rows[0]["CHF"] != DBNull.Value ? (baseRecord.Rows[0]["CHF"].ToString() == "1" ? true : false) : false;
                this.chkCVA.Checked = baseRecord.Rows[0]["CVA"] != DBNull.Value ? (baseRecord.Rows[0]["CVA"].ToString() == "1" ? true : false) : false;
                this.chkPAOD.Checked = baseRecord.Rows[0]["PAOD"] != DBNull.Value ? (baseRecord.Rows[0]["PAOD"].ToString() == "1" ? true : false) : false;
                this.chkCOPD.Checked = baseRecord.Rows[0]["COPD"] != DBNull.Value ? (baseRecord.Rows[0]["COPD"].ToString() == "1" ? true : false) : false;
                this.chkANOIA.Checked = baseRecord.Rows[0]["ANOIA"] != DBNull.Value ? (baseRecord.Rows[0]["ANOIA"].ToString() == "1" ? true : false) : false;
                this.chkOTHER_COMORBIDITY.Checked = baseRecord.Rows[0]["OTHER_COMORBIDITY"] != DBNull.Value ? (baseRecord.Rows[0]["OTHER_COMORBIDITY"].ToString() == "1" ? true : false) : false;
                this.txtOTHER_COMORBIDITY.Text = baseRecord.Rows[0]["OTHER_COMORBIDITY_TEXT"].ToString();

                this.txtFAMILY_HISTORY.Text = baseRecord.Rows[0]["FAMILY_HISTORY"].ToString();
                this.txtOPERATION_HISTORY.Text = baseRecord.Rows[0]["OPERATION_HISTORY"].ToString();
                this.radSMOKE.EditValue = baseRecord.Rows[0]["SMOKE"].ToString();
                this.txtSMOKE_NUM.Text = baseRecord.Rows[0]["SMOKE_NUM"].ToString();
                //this.txtSMOKE_NUM.Text = baseRecord.Rows[0]["SMOKE_NUM"].ToString();
                this.txtDRUG_ALLERGY.Text = baseRecord.Rows[0]["DRUG_ALLERGY"].ToString();
                this.txtFOOD_ALLERGY.Text = baseRecord.Rows[0]["FOOD_ALLERGY"].ToString();
                this.txtDIALYZER_ALLERGY.Text = baseRecord.Rows[0]["DIALYZER_ALLERGY"].ToString();
                this.radXUJIU.EditValue = baseRecord.Rows[0]["XUJIU"].ToString();
                this.txtXUJIU_DESC.Text = baseRecord.Rows[0]["XUJIU_DESC"].ToString();

                this.txtDIALYSIS_BEGIN.EditValue = baseRecord.Rows[0]["DIALYSIS_BEGIN"] != DBNull.Value ? Utility.CDate(baseRecord.Rows[0]["DIALYSIS_BEGIN"].ToString()) : this.txtDIALYSIS_BEGIN.EditValue;
                this.radPD_EXIST.EditValue = baseRecord.Rows[0]["PD_EXIST"].ToString();
                this.txtPD_YEAR.Text = baseRecord.Rows[0]["PD_YEAR"].ToString();
                this.radRENAL_TRANSPLANT_EXIST.EditValue = baseRecord.Rows[0]["RENAL_TRANSPLANT_EXIST"].ToString();
                this.txtRENAL_TRANSPLANT_YEAR.Text = baseRecord.Rows[0]["RENAL_TRANSPLANT_YEAR"].ToString();
                this.txtINTO_DATE.EditValue = baseRecord.Rows[0]["INTO_DATE"] != DBNull.Value ? Utility.CDate(baseRecord.Rows[0]["INTO_DATE"].ToString()) : this.txtINTO_DATE.EditValue;
                this.txtINTO_HOSPITAL.Text = baseRecord.Rows[0]["INTO_HOSPITAL"].ToString();
                this.txtDIALYSIS_END.EditValue = baseRecord.Rows[0]["DIALYSIS_END"] != DBNull.Value ? Utility.CDate(baseRecord.Rows[0]["DIALYSIS_END"].ToString()) : this.txtDIALYSIS_END.EditValue;
                this.radDIALYSIS_END_REASON.EditValue = baseRecord.Rows[0]["DIALYSIS_END_REASON"].ToString();
                this.txtDEAD_DATE.EditValue = baseRecord.Rows[0]["DEAD_DATE"] != DBNull.Value ? Utility.CDate(baseRecord.Rows[0]["DEAD_DATE"].ToString()) : this.txtDEAD_DATE.EditValue;
                this.txtDEAD_REASON.Text = baseRecord.Rows[0]["DEAD_REASON"].ToString();
                this.txtONSET_PASS.Text = baseRecord.Rows[0]["ONSET_PASS"].ToString();
            }
        }

        public PatientModel.MED_BASE_RECORDDataTable GetBaseRecordDataTable()
        {
            baseRecord = baseRecord ?? new PatientModel.MED_BASE_RECORDDataTable();
            if (baseRecord.Rows.Count == 0)
            {
                var newRow = baseRecord.NewMED_BASE_RECORDRow();
                newRow.ID = Guid.NewGuid().ToString();
                newRow.HEMODIALYSIS_ID = hemoId;
                newRow.CREATEBY = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                newRow.CREATE_DATE = DateTime.Now;
                baseRecord.AddMED_BASE_RECORDRow(newRow);
            }
            else
            {
                baseRecord[0].UPDATEBY = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                baseRecord[0].UPDATE_DATE = DateTime.Now;
            }

            var row = baseRecord[0];
            row.IS_UP = this.chkIsUp.Checked ? "1" : "0";
            row.CGN = this.chkCGN.Checked ? "1" : "0";
            row.CIN = this.chkCIN.Checked ? "1" : "0";
            row.DN = this.chkDN.Checked ? "1" : "0";
            row.HRD = this.chkHRD.Checked ? "1" : "0";
            row.PCKD = this.chkPCKD.Checked ? "1" : "0";
            row.UUO = this.chkUUO.Checked ? "1" : "0";
            row.RENAL_TUMOR = this.chkRENAL_TUMOR.Checked ? "1" : "0";
            row.OTHER_PROTOPATHY = this.chkOTHER_PROTOPATHY.Checked ? "1" : "0";
            row.OTHER_PROTOPATHY_TEXT = this.txtOTHER_PROTOPATHY.Text.Trim();
            row.ISFIRSTDIALYSIS = this.chkIsFirstDialysis.Checked?"1":"0";
            row.DIALYSIS_YEARS = this.radDIALYSIS_YEARS.EditValue.ToString();
            row.TFSB = this.chkTFSB.Checked ? "1" : "0";
            row.BYBX = this.chkBYBX.Checked ? "1" : "0";
            row.HTN = this.chkHTN.Checked ? "1" : "0";
            row.DM = this.chkDM.Checked ? "1" : "0";
            row.CAD = this.chkCAD.Checked ? "1" : "0";
            row.CHF = this.chkCHF.Checked ? "1" : "0";
            row.CVA = this.chkCVA.Checked ? "1" : "0";
            row.PAOD = this.chkPAOD.Checked ? "1" : "0";
            row.COPD = this.chkCOPD.Checked ? "1" : "0";
            row.ANOIA = this.chkANOIA.Checked ? "1" : "0";
            row.OTHER_COMORBIDITY = this.chkOTHER_COMORBIDITY.Checked ? "1" : "0";
            row.OTHER_COMORBIDITY_TEXT = this.txtOTHER_COMORBIDITY.Text.Trim();

            row.FAMILY_HISTORY = this.txtFAMILY_HISTORY.Text.Trim();
            row.OPERATION_HISTORY = this.txtOPERATION_HISTORY.Text.Trim();
            row.SMOKE = this.radSMOKE.EditValue.ToString();
            row["SMOKE_NUM"] = this.txtSMOKE_NUM.Text.Trim() != string.Empty ? Utility.CDecimal(this.txtSMOKE_NUM.Text.Trim()) : row["SMOKE_NUM"];
            //row["SMOKE_NUM"] = this.txtSMOKE_NUM.Text.Trim() != string.Empty ? Utility.CDecimal(this.txtSMOKE_NUM.Text.Trim()) : row["SMOKE_NUM"];
            row.DRUG_ALLERGY = this.txtDRUG_ALLERGY.Text.Trim();
            row.FOOD_ALLERGY = this.txtFOOD_ALLERGY.Text.Trim();
            row.DIALYZER_ALLERGY = this.txtDIALYZER_ALLERGY.Text.Trim();
            row.XUJIU = this.radXUJIU.EditValue.ToString();
            row["XUJIU_DESC"] = this.txtXUJIU_DESC.Text.Trim();

            row["DIALYSIS_BEGIN"] = this.txtDIALYSIS_BEGIN.EditValue != null ? Utility.CDate(this.txtDIALYSIS_BEGIN.EditValue.ToString()) : row["DIALYSIS_BEGIN"];
            row.PD_EXIST = this.radPD_EXIST.EditValue.ToString();
            row["PD_YEAR"] = this.txtPD_YEAR.Text.Trim() != string.Empty ? Utility.CDecimal(this.txtPD_YEAR.Text.Trim()) : row["PD_YEAR"];
            row.RENAL_TRANSPLANT_EXIST = this.radRENAL_TRANSPLANT_EXIST.EditValue.ToString();
            row["RENAL_TRANSPLANT_YEAR"] = this.txtRENAL_TRANSPLANT_YEAR.Text.Trim() != string.Empty ? Utility.CDecimal(this.txtRENAL_TRANSPLANT_YEAR.Text.Trim()) : row["RENAL_TRANSPLANT_YEAR"];
            row["INTO_DATE"] = this.txtINTO_DATE.EditValue != null ? Utility.CDate(this.txtINTO_DATE.EditValue.ToString()) : row["INTO_DATE"];
            row.INTO_HOSPITAL = this.txtINTO_HOSPITAL.Text.Trim();
            row["DIALYSIS_END"] = this.txtDIALYSIS_END.EditValue != null ? Utility.CDate(this.txtDIALYSIS_END.EditValue.ToString()) : row["DIALYSIS_END"];
            row.DIALYSIS_END_REASON = this.radDIALYSIS_END_REASON.EditValue.ToString();
            row["DEAD_DATE"] = this.txtDEAD_DATE.EditValue != null ? Utility.CDate(this.txtDEAD_DATE.EditValue.ToString()) : row["DEAD_DATE"];
            row.DEAD_REASON = this.txtDEAD_REASON.Text.Trim();
            row.ONSET_PASS = this.txtONSET_PASS.Text.Trim();

            return baseRecord;
        }

        /// <summary>
        /// 重置患者病历基本资料
        /// </summary>
        private void ResetBaseRecord()
        {
            this.chkIsUp.Checked = false;
            this.chkCGN.Checked = false;
            this.chkCIN.Checked = false;
            this.chkDN.Checked = false;
            this.chkHRD.Checked = false;
            this.chkPCKD.Checked = false;
            this.chkUUO.Checked = false;
            this.chkRENAL_TUMOR.Checked = false;
            this.chkTFSB.Checked = false;
            this.chkOTHER_PROTOPATHY.Checked = false;
            this.txtOTHER_PROTOPATHY.Text = string.Empty;

            this.chkHTN.Checked = false;
            this.chkDM.Checked = false;
            this.chkCAD.Checked = false;
            this.chkCHF.Checked = false;
            this.chkCVA.Checked = false;
            this.chkPAOD.Checked = false;
            this.chkCOPD.Checked = false;
            this.chkANOIA.Checked = false;
            this.chkOTHER_COMORBIDITY.Checked = false;
            this.txtOTHER_COMORBIDITY.Text = string.Empty;

            this.txtFAMILY_HISTORY.Text = string.Empty;
            this.txtOPERATION_HISTORY.Text = string.Empty;
            this.radSMOKE.EditValue = 0;
            this.txtSMOKE_NUM.Text = string.Empty;
            this.radXUJIU.EditValue = 0;
            this.txtXUJIU_DESC.Text = string.Empty;
            this.txtDRUG_ALLERGY.Text = string.Empty;
            this.txtFOOD_ALLERGY.Text = string.Empty;
            this.txtDIALYZER_ALLERGY.Text = string.Empty;

            this.txtDIALYSIS_BEGIN.EditValue = null;
            this.radPD_EXIST.EditValue = 0;
            this.txtPD_YEAR.Text = string.Empty;
            this.radRENAL_TRANSPLANT_EXIST.EditValue = 0;
            this.txtRENAL_TRANSPLANT_YEAR.Text = string.Empty;
            this.chkIsFirstDialysis.Checked = false;
            this.radDIALYSIS_YEARS.EditValue = 0;
            this.txtINTO_DATE.EditValue = null;
            this.txtINTO_HOSPITAL.Text = string.Empty;
            this.txtDIALYSIS_END.EditValue = null;
            this.radDIALYSIS_END_REASON.EditValue = 0;
            this.txtDEAD_DATE.EditValue = null;
            this.txtDEAD_REASON.Text = string.Empty;
            this.txtONSET_PASS.Text = string.Empty;
            this.txtPROGRESS_NODE.Text = string.Empty;
        }

        #endregion
    }
}
