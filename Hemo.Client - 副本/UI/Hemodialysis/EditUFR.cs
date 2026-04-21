/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:修改URF
 * 创建标识:顾伟伟-2013年5月15日
 * 
 * 修改时间:2013年8月23日
 * 修改人:贺建操
 * 修改描述:新增方法
 * 
 * 修改时间:2013年12月1日
 * 修改人:贺建操
 * 修改描述:修改方法
 * 
 * 修改时间:2014年3月11日
 * 修改人:贺建操
 * 修改描述:修改方法SQL
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.IService.Config;
using DevExpress.XtraEditors.Controls;
using Hemo.IService;
using Hemo.IService.Dict;
using System.Linq;
using Hemo.IService.PatientSchedule;
using Hemo.Client.Core;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class EditUFR : HemoBaseFrm
    {
        #region 类变量

        private HemodialysisModel.MED_HEMO_RECIPEDataTable recipeTable;

        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;

        private IPatient objPatientService = ServiceManager.Instance.PatientService;

        private IPatientSchedule scheduleService = ServiceManager.Instance.PatientSchedule;

        private IConfig configService = ServiceManager.Instance.ConfigService;

        private decimal dDRY_WEIGHT = 0;

        private decimal dTODAY_WEIGHT = 0;

        private bool isShowFrm = true;

        private DateTime cureDate = System.DateTime.Now;

        private DataTable dtCure = null;

        private DataTable dtPressure = null;

        private DataRow currentPatient = null;

        private string areaName = string.Empty;

        private BackgroundWorker worker = new BackgroundWorker();

        #endregion

        #region 属性

        public bool IsShowFrm
        {
            get { return isShowFrm; }
            set { isShowFrm = value; }
        }

        public DateTime CureDate
        {
            get { return cureDate; }
            set { this.cureDate = value; }
        }

        public DataRow CurrentPatient
        {
            get { return currentPatient; }
            set { currentPatient = value; }
        }

        public string AreaName
        {
            get { return areaName; }
            set { areaName = value; }
        }

        #endregion

        #region 构造函数

        public EditUFR(string pHemoID, DateTime pCureDate)
        {
            InitializeComponent();
            txtHemoID.EditValue = pHemoID;
            cureDate = pCureDate;

            DataTable dtPatient = objPatientService.GetPatientListByParams(string.Empty, pHemoID);
            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                this.Text = dtPatient.Rows[0]["NAME"].ToString() + " - 简易处方";
            }
        }

        #endregion

        #region 事件

        private void EditUFR_Load(object sender, EventArgs e)
        {
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.RunWorkerAsync();

            BindLookUpEdit();
            loadRecipe();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsDataValid())
            {
                return;
            }

            recipeTable = objHemodialysisService.GetRecipeByHemodialysisIDAndDate(txtHemoID.EditValue.ToString(), cureDate);
            HemodialysisModel.MED_CURE_MAINDataTable cureDt = objHemodialysisService.GetMainCureByRecipeId(recipeTable[0].RECIPE_ID);
            DataTable dtTemp = recipeTable;

            if (Utility.CDecimal(txtUFR.Text) > 5)
            {
                AutoClosedMsgBox.ShowForm("预计脱水过多，请重新确认。", "病患管理", 1000, MessageBoxIcon.Information);
                txtUFR.Focus();
                return;
            }

            if (cureDt != null && cureDt.Rows.Count > 0)
            {
                cureDt[0].UFR = Utility.CDouble(txtUFR.EditValue.ToString());
                cureDt[0].DRY_WEIGHT = Utility.CDecimal(txtDRY_WEIGHT.EditValue.ToString());
                cureDt[0].BEFORE_DRY_WEIGHT = Utility.CDouble(spnTODAY_WEIGHT.EditValue.ToString());
                cureDt[0].BEFORE_SYSTOLIC_PRESSURE = Utility.CDouble(txtTODAY_BLOODA.EditValue.ToString());
                cureDt[0].BEFORE_DIASTOLIC_PRESSURE = Utility.CDouble(txtTODAY_BLOODB.EditValue.ToString());
                cureDt[0].BEFORE_BP = Utility.CInt(txtBLOODP.EditValue.ToString());
                cureDt[0].AFTER_DRY_WEIGHT = Utility.CDouble(spnAFTER_DRY_WEIGHT.EditValue.ToString());
                cureDt[0].PURIFICATION_MODE = cmbPURIFICATION_MODE.EditValue.ToString();
                cureDt[0].HEPARIN_SPECIES = cmbTHERAPEUTIC_METHOD.EditValue.ToString();
                cureDt[0].FIRST_HEPARIN = Utility.CDouble(txtFIRST_DRUG_DOSAGE.Text);
                cureDt[0].FIRST_DRUG_UNIT = cmbFIRST_DRUG_UNIT.EditValue.ToString();
                cureDt[0].DOSIS_SUSTENTATIVA = (float)Utility.CDouble(txtSECOND_DRUG_DOSAGE.Text);
                cureDt[0].SECOND_DRUG_UNIT = cmbSECOND_DRUG_UNIT.EditValue.ToString();
                cureDt[0].MACHINE_TYPE = cmbFIRST_PURIFIER_MODEL.EditValue.ToString();
                cureDt[0].PURIFIER_NAME = cmbFIRST_PURIFIER_NAME.EditValue.ToString();
                cureDt[0].PURIFIER_M2 = Utility.CDouble(txtFIRST_PURIFIER_M2.EditValue.ToString());
                cureDt[0].DOCTOR_ADVICE = this.txtREMARK.Text;
                int result = objHemodialysisService.SaveCureMain(cureDt);
            }

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                dtTemp.Rows[0]["UFR"] = txtUFR.EditValue;
                dtTemp.Rows[0]["DRY_WEIGHT"] = txtDRY_WEIGHT.EditValue;
                dtTemp.Rows[0]["TODAY_WEIGHT"] = spnTODAY_WEIGHT.EditValue;
                dtTemp.Rows[0]["TODAY_BLOODA"] = txtTODAY_BLOODA.EditValue;
                dtTemp.Rows[0]["TODAY_BLOODB"] = txtTODAY_BLOODB.EditValue;
                dtTemp.Rows[0]["TODAY_BLOODP"] = txtBLOODP.EditValue;
                dtTemp.Rows[0]["DRY_WEIGHT_REMARK"] = txtDRY_WEIGHT_REMARK.Text;
                dtTemp.Rows[0]["REMARK"] = txtREMARK.Text;
                dtTemp.Rows[0]["PURIFICATION_MODE"] = cmbPURIFICATION_MODE.EditValue;

                dtTemp.Rows[0]["FIRST_PURIFIER_MODEL"] = cmbFIRST_PURIFIER_MODEL.EditValue;
                dtTemp.Rows[0]["FIRST_PURIFIER_NAME"] = cmbFIRST_PURIFIER_NAME.EditValue;
                dtTemp.Rows[0]["FIRST_PURIFIER_M2"] = txtFIRST_PURIFIER_M2.EditValue;
                dtTemp.Rows[0]["FIRST_PURIFIER_KOA"] = txtFIRST_PURIFIER_KOA.EditValue;
                dtTemp.Rows[0]["FIRST_PURIFIER_KUF"] = txtFIRST_PURIFIER_KUF.EditValue;

                dtTemp.Rows[0]["SECOND_PURIFIER_MODEL"] = cmbSECOND_PURIFIER_MODEL.EditValue;
                dtTemp.Rows[0]["SECOND_PURIFIER_NAME"] = cmbSECOND_PURIFIER_NAME.EditValue;
                dtTemp.Rows[0]["SECOND_PURIFIER_M2"] = txtSECOND_PURIFIER_M2.EditValue;
                dtTemp.Rows[0]["SECOND_PURIFIER_KOA"] = txtSECOND_PURIFIER_KOA.EditValue;
                dtTemp.Rows[0]["SECOND_PURIFIER_KUF"] = txtSECOND_PURIFIER_KUF.EditValue;

                dtTemp.Rows[0]["THERAPEUTIC_METHOD"] = cmbTHERAPEUTIC_METHOD.EditValue;
                dtTemp.Rows[0]["FIRST_DRUG_DOSAGE"] = txtFIRST_DRUG_DOSAGE.Text;
                dtTemp.Rows[0]["FIRST_DRUG_UNIT"] = cmbFIRST_DRUG_UNIT.EditValue;
                dtTemp.Rows[0]["FIRST_DRUG_MODE"] = cmbFIRST_DRUG_MODE.EditValue;
                dtTemp.Rows[0]["SECOND_DRUG_DOSAGE"] = txtSECOND_DRUG_DOSAGE.Text;
                dtTemp.Rows[0]["SECOND_DRUG_UNIT"] = cmbSECOND_DRUG_UNIT.EditValue;
                dtTemp.Rows[0]["SECOND_DRUG_MODE"] = cmbSECOND_DRUG_MODE.EditValue;

                int result = objHemodialysisService.SaveRecipe(dtTemp as HemodialysisModel.MED_HEMO_RECIPEDataTable);
                if (result == 1)
                {
                    AutoClosedMsgBox.ShowForm("保存成功！", "病患管理", 1000, MessageBoxIcon.Information);
                }
            }
            else
            {
                AutoClosedMsgBox.ShowForm("患者临时处方尚未创建，请先创建临时处方。", "病患管理", 1000, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 确认处方
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYes_Click(object sender, EventArgs e)
        {
            ConfirmOrCancelOnePatient(true);
        }

        /// <summary>
        /// 取消处方
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNo_Click(object sender, EventArgs e)
        {
            ConfirmOrCancelOnePatient(false);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtUFR_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(txtUFR);
        }

        private void txtDRY_WEIGHT_EditValueChanged(object sender, EventArgs e)
        {
            ChangeValue(txtDRY_WEIGHT);
        }

        private void spnTODAY_WEIGHT_EditValueChanged(object sender, EventArgs e)
        {
            ChangeValue(spnTODAY_WEIGHT);
        }

        private void cmbTHERAPEUTIC_METHOD_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbTHERAPEUTIC_METHOD.Text.Equals("低分子肝素抗凝"))
            {
                cmbFIRST_DRUG_UNIT.EditValue = "74e7e438-7f88-4e51-b8f1-376023e803c1";
                cmbFIRST_DRUG_MODE.EditValue = "d18bc2cf-a2f6-4544-ad1c-2dfe93b82d7d";
                cmbSECOND_DRUG_UNIT.EditValue = "74e7e438-7f88-4e51-b8f1-376023e803c1";
                cmbSECOND_DRUG_MODE.EditValue = "d18bc2cf-a2f6-4544-ad1c-2dfe93b82d7d";
            }
            else if (cmbTHERAPEUTIC_METHOD.Text.Equals("普通肝素抗凝") || cmbTHERAPEUTIC_METHOD.Text.Equals("阿加曲班"))
            {
                cmbFIRST_DRUG_UNIT.EditValue = "a0a7c82b-78d8-431c-b9f9-fd78f34c8a4c";
                cmbFIRST_DRUG_MODE.EditValue = "d18bc2cf-a2f6-4544-ad1c-2dfe93b82d7d";
                cmbSECOND_DRUG_UNIT.EditValue = "a0a7c82b-78d8-431c-b9f9-fd78f34c8a4c";
                cmbSECOND_DRUG_MODE.EditValue = "d18bc2cf-a2f6-4544-ad1c-2dfe93b82d7d";
            }
            else
            {
                cmbFIRST_DRUG_UNIT.EditValue = string.Empty;
                cmbFIRST_DRUG_MODE.EditValue = string.Empty;
                cmbSECOND_DRUG_UNIT.EditValue = string.Empty;
                cmbSECOND_DRUG_MODE.EditValue = string.Empty;
            }
        }

        private void txtFIRST_PURIFIER_M2_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(txtFIRST_PURIFIER_M2);
        }

        private void txtFIRST_PURIFIER_KOA_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(txtFIRST_PURIFIER_KOA);
        }

        private void txtFIRST_PURIFIER_KUF_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(txtFIRST_PURIFIER_KUF);
        }

        private void txtSECOND_PURIFIER_M2_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(txtSECOND_PURIFIER_M2);
        }

        private void txtSECOND_PURIFIER_KOA_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(txtSECOND_PURIFIER_KOA);
        }

        private void txtSECOND_PURIFIER_KUF_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(txtSECOND_PURIFIER_KUF);
        }

        private void cmbPURIFICATION_MODE_EditValueChanged(object sender, EventArgs e)
        {
            if (this.cmbPURIFICATION_MODE.Text == "HD")
            {
                //this.cmbFIRST_PURIFIER_MODEL.EditValue = "90150299-e0a1-40b8-b9e8-82d504c88f21";//F6HPS
                this.cmbFIRST_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";//聚砜膜
                this.txtFIRST_PURIFIER_M2.EditValue = 1.3;
                this.cmbSECOND_PURIFIER_MODEL.EditValue = null;
                this.cmbSECOND_PURIFIER_NAME.EditValue = null;
                this.txtSECOND_PURIFIER_M2.EditValue = 0;
            }
            else if (this.cmbPURIFICATION_MODE.Text == "HDF")
            {
                this.cmbFIRST_PURIFIER_MODEL.EditValue = "59b5a10f-5a1f-4192-bc70-6be6b40efdb1";//FX80
                this.cmbFIRST_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";//聚砜膜
                this.txtFIRST_PURIFIER_M2.EditValue = 1.8;
                this.cmbSECOND_PURIFIER_MODEL.EditValue = null;
                this.cmbSECOND_PURIFIER_NAME.EditValue = null; 
                this.txtSECOND_PURIFIER_M2.EditValue = 0;
            }
            else if (this.cmbPURIFICATION_MODE.Text == "HD+HP")
            {
                //this.cmbFIRST_PURIFIER_MODEL.EditValue = "90150299-e0a1-40b8-b9e8-82d504c88f21";//F6HPS
                this.cmbFIRST_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";//聚砜膜
                this.txtFIRST_PURIFIER_M2.EditValue = 1.3;
                this.cmbSECOND_PURIFIER_MODEL.EditValue = "61c16922-87a4-4176-8e6c-726b04baea7b";//HP130
                this.cmbSECOND_PURIFIER_NAME.EditValue = "92f926c3-cb00-43cf-b4b5-7011597d51e7";//树脂
                this.txtSECOND_PURIFIER_M2.EditValue = 0;
            }
            else
            {
                //this.cmbFIRST_PURIFIER_MODEL.EditValue = null;
                //this.cmbFIRST_PURIFIER_NAME.EditValue = null;
                //this.txtFIRST_PURIFIER_M2.EditValue = 0;
                //this.cmbSECOND_PURIFIER_MODEL.EditValue = null;
                //this.cmbSECOND_PURIFIER_NAME.EditValue = null;
                //this.txtSECOND_PURIFIER_M2.EditValue = 0;
            }
        }

        private void cmbFIRST_PURIFIER_MODEL_EditValueChanged(object sender, EventArgs e)
        {
            if (this.cmbFIRST_PURIFIER_MODEL.Text.Equals("威高F15"))
            {
                this.cmbFIRST_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";
                this.txtFIRST_PURIFIER_M2.EditValue = 1.5;
            }
            else if (this.cmbFIRST_PURIFIER_MODEL.Text.Equals("FX80"))
            {
                this.cmbFIRST_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";
                this.txtFIRST_PURIFIER_M2.EditValue = 1.8;
            }
            else if (this.cmbFIRST_PURIFIER_MODEL.Text.Equals("金宝Polyflux 14L"))
            {
                this.cmbFIRST_PURIFIER_NAME.EditValue = "52269ca7-b9ba-490a-9164-8199220ec61b";
                this.txtFIRST_PURIFIER_M2.EditValue = 1.8;
            }
            else if (this.cmbFIRST_PURIFIER_MODEL.Text.Equals("HP130"))
            {
                this.cmbFIRST_PURIFIER_NAME.EditValue = "92f926c3-cb00-43cf-b4b5-7011597d51e7";
            }
        }

        private void cmbSECOND_PURIFIER_MODEL_EditValueChanged(object sender, EventArgs e)
        {
            if (this.cmbSECOND_PURIFIER_MODEL.Text.Equals("威高F15"))
            {
                this.cmbSECOND_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";
                this.txtSECOND_PURIFIER_M2.EditValue = 1.5;
            }
            else if (this.cmbSECOND_PURIFIER_MODEL.Text.Equals("FX80"))
            {
                this.cmbSECOND_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";
                this.txtSECOND_PURIFIER_M2.EditValue = 1.8;
            }
            else if (this.cmbSECOND_PURIFIER_MODEL.Text.Equals("金宝Polyflux 14L"))
            {
                this.cmbSECOND_PURIFIER_NAME.EditValue = "52269ca7-b9ba-490a-9164-8199220ec61b";
                this.txtSECOND_PURIFIER_M2.EditValue = 1.8;
            }
            else if (this.cmbSECOND_PURIFIER_MODEL.Text.Equals("HP130"))
            {
                this.cmbSECOND_PURIFIER_NAME.EditValue = "92f926c3-cb00-43cf-b4b5-7011597d51e7";
            }
        }

        #endregion

        #region 方法

        private void BindLookUpEdit()
        {
            string type = areaName.Equals("CRRT") ? "CRRT净化方式" : "净化方式";
            BaseControlInfo.BindLookUpEdit(cmbPURIFICATION_MODE, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, type, "1"), "ITEM_NAME", "净化方式");
            BaseControlInfo.BindLookUpEdit(cmbFIRST_PURIFIER_MODEL, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "净化器类型", "1"), "ITEM_NAME", "净化器类型");
            BaseControlInfo.BindLookUpEdit(cmbSECOND_PURIFIER_MODEL, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "净化器类型", "1"), "ITEM_NAME", "净化器类型");
            BaseControlInfo.BindLookUpEdit(cmbFIRST_PURIFIER_NAME, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "透析膜", "1"), "ITEM_NAME", "透析膜");
            BaseControlInfo.BindLookUpEdit(cmbSECOND_PURIFIER_NAME, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "透析膜", "1"), "ITEM_NAME", "透析膜");
            BaseControlInfo.BindLookUpEdit(cmbTHERAPEUTIC_METHOD, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "治疗方法", "1"), "ITEM_NAME", "治疗方法");
            BaseControlInfo.BindLookUpEdit(cmbFIRST_DRUG_UNIT, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "药品单位", "1"), "ITEM_NAME", "药品单位");
            BaseControlInfo.BindLookUpEdit(cmbSECOND_DRUG_UNIT, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "药品单位", "1"), "ITEM_NAME", "药品单位");
            BaseControlInfo.BindLookUpEdit(cmbFIRST_DRUG_MODE, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "注射方式", "1"), "ITEM_NAME", "注射方式");
            BaseControlInfo.BindLookUpEdit(cmbSECOND_DRUG_MODE, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "注射方式", "1"), "ITEM_NAME", "注射方式");
        }

        private void loadRecipe()
        {
            recipeTable = objHemodialysisService.GetRecipeByHemodialysisIDAndDate(txtHemoID.EditValue.ToString(), cureDate);
            DataTable dtTemp = recipeTable;

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                txtDRY_WEIGHT.EditValue = dDRY_WEIGHT = Utility.CDecimal(dtTemp.Rows[0]["DRY_WEIGHT"].ToString());
                spnTODAY_WEIGHT.EditValue = dTODAY_WEIGHT = Utility.CDecimal(dtTemp.Rows[0]["TODAY_WEIGHT"].ToString());
                txtUFR.EditValue = Utility.CDecimal(dtTemp.Rows[0]["UFR"].ToString());

                if (txtUFR.EditValue.ToString() == "0")
                {
                    if (dDRY_WEIGHT != 0 && dTODAY_WEIGHT != 0)
                    {
                        txtUFR.EditValue = dTODAY_WEIGHT - dDRY_WEIGHT;
                    }
                }

                txtTODAY_BLOODA.Text = dtTemp.Rows[0]["TODAY_BLOODA"].ToString();
                txtTODAY_BLOODB.Text = dtTemp.Rows[0]["TODAY_BLOODB"].ToString();
                txtBLOODP.Text = dtTemp.Rows[0]["TODAY_BLOODP"].ToString();
                txtDRY_WEIGHT_REMARK.Text = dtTemp.Rows[0]["DRY_WEIGHT_REMARK"].ToString();
                cmbTHERAPEUTIC_METHOD.EditValue = dtTemp.Rows[0]["THERAPEUTIC_METHOD"].ToString();
                txtREMARK.Text = dtTemp.Rows[0]["REMARK"].ToString();
                cmbPURIFICATION_MODE.EditValue = dtTemp.Rows[0]["PURIFICATION_MODE"].ToString();
                cmbFIRST_PURIFIER_MODEL.EditValue = dtTemp.Rows[0]["FIRST_PURIFIER_MODEL"].ToString();
                cmbFIRST_PURIFIER_NAME.EditValue = dtTemp.Rows[0]["FIRST_PURIFIER_NAME"].ToString();
                txtFIRST_PURIFIER_M2.EditValue = Utility.CDecimal(dtTemp.Rows[0]["FIRST_PURIFIER_M2"].ToString());
                txtFIRST_PURIFIER_KOA.EditValue = Utility.CDecimal(dtTemp.Rows[0]["FIRST_PURIFIER_KOA"].ToString());
                txtFIRST_PURIFIER_KUF.EditValue = Utility.CDecimal(dtTemp.Rows[0]["FIRST_PURIFIER_KUF"].ToString());
                cmbSECOND_PURIFIER_MODEL.EditValue = dtTemp.Rows[0]["SECOND_PURIFIER_MODEL"].ToString();
                cmbSECOND_PURIFIER_NAME.EditValue = dtTemp.Rows[0]["SECOND_PURIFIER_NAME"].ToString();
                txtSECOND_PURIFIER_M2.EditValue = Utility.CDecimal(dtTemp.Rows[0]["SECOND_PURIFIER_M2"].ToString());
                txtSECOND_PURIFIER_KOA.EditValue = Utility.CDecimal(dtTemp.Rows[0]["SECOND_PURIFIER_KOA"].ToString());
                txtSECOND_PURIFIER_KUF.EditValue = Utility.CDecimal(dtTemp.Rows[0]["SECOND_PURIFIER_KUF"].ToString());
                txtFIRST_DRUG_DOSAGE.Text = dtTemp.Rows[0]["FIRST_DRUG_DOSAGE"].ToString();
                cmbFIRST_DRUG_UNIT.EditValue = dtTemp.Rows[0]["FIRST_DRUG_UNIT"].ToString();
                cmbFIRST_DRUG_MODE.EditValue = dtTemp.Rows[0]["FIRST_DRUG_MODE"].ToString();
                txtSECOND_DRUG_DOSAGE.Text = dtTemp.Rows[0]["SECOND_DRUG_DOSAGE"].ToString();
                cmbSECOND_DRUG_UNIT.EditValue = dtTemp.Rows[0]["SECOND_DRUG_UNIT"].ToString();
                cmbSECOND_DRUG_MODE.EditValue = dtTemp.Rows[0]["SECOND_DRUG_MODE"].ToString();
                //loadWeightList(txtHemoID.EditValue.ToString());

                if (cmbPURIFICATION_MODE.EditValue == null || cmbPURIFICATION_MODE.EditValue == string.Empty)
                {
                    cmbPURIFICATION_MODE.EditValue = "9c01f053-ad09-4873-b68f-b96d03b8572f";
                }

                if (cmbFIRST_PURIFIER_MODEL.EditValue == null || cmbFIRST_PURIFIER_MODEL.EditValue == string.Empty)
                {
                    //cmbFIRST_PURIFIER_MODEL.EditValue = "90150299-e0a1-40b8-b9e8-82d504c88f21";
                }

                if (cmbFIRST_PURIFIER_NAME.EditValue == null || cmbFIRST_PURIFIER_NAME.EditValue == string.Empty)
                {
                    cmbFIRST_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";
                }

                if (txtFIRST_PURIFIER_M2.EditValue == null || txtFIRST_PURIFIER_M2.EditValue.ToString() == "0")
                {
                    txtFIRST_PURIFIER_M2.EditValue = 1.3;
                }

                HemodialysisModel.MED_CURE_MAINDataTable cureDt = objHemodialysisService.GetMainCureByRecipeId(recipeTable[0].RECIPE_ID);
                if (cureDt != null && cureDt.Rows.Count > 0)
                {
                    txtUFR.ReadOnly = true;
                    spnAFTER_DRY_WEIGHT.EditValue = cureDt[0].AFTER_DRY_WEIGHT;
                }
                else
                {
                    txtUFR.ReadOnly = false;
                    spnAFTER_DRY_WEIGHT.EditValue = 0;
                }
            }
            else
            {
                if (XtraMessageBox.Show("患者有效处方尚未建立，请先创建处方。", "病患管理") == DialogResult.OK) { isShowFrm = false; }
            }
        }

        private void LoadChart()
        {
            DataTable dtMain = objHemodialysisService.GetRecentCureInfoByHemoId(this.txtHemoID.EditValue.ToString());
            DataTable dtPressure = objHemodialysisService.GetRecentPressureByHemoId(this.txtHemoID.EditValue.ToString());

            if (dtMain != null && dtMain.Rows.Count > 0)
            {
                this.ctlSignChart1.DrawDryWeight(dtMain);
                this.ctlSignChart3.DrawDryWaterChart(dtMain);
                this.ctlSignChart4.DrawWeightChart(dtMain);
            }

            if (dtPressure != null && dtPressure.Rows.Count > 0)
            {
                this.ctlSignChart2.DrawRecentPressureChart(dtPressure);
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadRecentInfo();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (dtCure != null && dtCure.Rows.Count > 0)
            {
                dtCure.AsEnumerable().ToList().ForEach(row => this.lblRecentWeight.Text += row["AFTERWEIGHT"].ToString() + " | ");
                this.lblRecentWeight.Text = this.lblRecentWeight.Text.Substring(0, this.lblRecentWeight.Text.Length - 3);
            }

            if (dtPressure != null && dtPressure.Rows.Count > 0)
            {
                dtPressure.AsEnumerable().ToList().ForEach(row => this.lblRecentSSY.Text += row["DISPLAY_SYSTOLIC_PRESSURE"].ToString() + " | ");
                dtPressure.AsEnumerable().ToList().ForEach(row => this.lblRecentSZY.Text += row["DISPLAY_DIASTOLIC_PRESSURE"].ToString() + " | ");
                dtPressure.AsEnumerable().ToList().ForEach(row => this.lblRecentPulse.Text += row["DISPLAY_CARDIOTACH"].ToString() + " | ");
                this.lblRecentSSY.Text = this.lblRecentSSY.Text.Substring(0, this.lblRecentSSY.Text.Length - 3);
                this.lblRecentSZY.Text = this.lblRecentSZY.Text.Substring(0, this.lblRecentSZY.Text.Length - 3);
                this.lblRecentPulse.Text = this.lblRecentPulse.Text.Substring(0, this.lblRecentPulse.Text.Length - 3);
            }

            LoadChart();
            worker.Dispose();
        }

        //载入历次干体重
        private void loadWeightList(string pHemoID)
        {
            DataTable dtWeightList = objHemodialysisService.GetDryWeightListByHemoID(pHemoID);
            if (dtWeightList != null && dtWeightList.Rows.Count > 0)
            {
                cmbWeightList.DataSource = dtWeightList;
                cmbWeightList.DisplayMember = "AHTERWEIGHT";
            }
        }

        private void ChangeValue(SpinEdit pEdit)
        {
            checkNumberValue(pEdit);
            dDRY_WEIGHT = Utility.CDecimal(txtDRY_WEIGHT.EditValue.ToString());
            dTODAY_WEIGHT = Utility.CDecimal(spnTODAY_WEIGHT.EditValue.ToString());
            txtUFR.EditValue = (dDRY_WEIGHT != 0 && dTODAY_WEIGHT != 0) ? dTODAY_WEIGHT - dDRY_WEIGHT : 0;
        }

        /// <summary>
        /// 加载近期体重、血压、脉搏信息
        /// </summary>
        private void LoadRecentInfo()
        {
            this.Invoke(new Action(() =>
            {
                this.lblRecentWeight.Text = string.Empty;
                this.lblRecentSSY.Text = string.Empty;
                this.lblRecentSZY.Text = string.Empty;
                this.lblRecentPulse.Text = string.Empty;
            }));

            dtCure = objHemodialysisService.GetRecentCureInfoByHemoId(txtHemoID.EditValue.ToString());
            dtPressure = objHemodialysisService.GetRecentPressureByHemoId(txtHemoID.EditValue.ToString());
        }

        //过滤符号
        private void checkNumberValue(SpinEdit pEdit)
        {
            if (pEdit.Text.IndexOf("-") > -1)
            {
                pEdit.Text = pEdit.Text.Replace("-", "");
            }
        }

        private bool IsDataValid()
        {
            bool result = true;
            result = BaseControlInfo.CheckSpinEdit(this.txtUFR, "请录入预计脱水！", this.Text);
            if (result == false)
            {
                return result;
            }
            result = BaseControlInfo.CheckpLookUpEdit(cmbPURIFICATION_MODE, "请选择净化方式！", "病患管理");
            if (result == false)
            {
                return result;
            }
            result = BaseControlInfo.CheckpLookUpEdit(cmbFIRST_PURIFIER_MODEL, "请选择净化器型号！", "病患管理");
            if (result == false)
            {
                return result;
            }
            return result;
        }

        private void ConfirmOrCancelOnePatient(bool pStatus)
        {
            string strStautsName = string.Empty;
            string strInf = string.Empty;
            string strResult = string.Empty;

            if (pStatus)
            {
                strStautsName = "确认";
            }
            else
            {
                strStautsName = "取消";
            }

            if (currentPatient["INFECTIOUS_CHECK_RESULT"] != null)
            {
                strInf = currentPatient["INFECTIOUS_CHECK_RESULT"].ToString();
            }
            strResult = CheckArea(areaName, strInf).Trim();
            if (strResult != string.Empty)
            {
                if (XtraMessageBox.Show(strResult, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            if (XtraMessageBox.Show("确定" + strStautsName + "该患者透析处方吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            #region 读取系统质控校验选项

            DataTable dtQualityCheck = configService.GetConfigList(string.Empty, string.Empty, "质控校验", "1");
            DataTable todayWeight = new DataTable();
            DataTable todayUfR = new DataTable();
            DataTable diagnose = new DataTable();

            if (dtQualityCheck != null && dtQualityCheck.Rows.Count > 0)
            {
                //读取系统是否需要验证透前体重设置
                todayWeight = Utility.GetSubTable(dtQualityCheck, "item_name='透前体重' and status='1'");
                //读取系统是否需要验证诊断设置
                diagnose = Utility.GetSubTable(dtQualityCheck, "item_name='诊断结果' and status='1'");
                //读取系统是否需要验证预计脱水设置
                todayUfR = Utility.GetSubTable(dtQualityCheck, "item_name='预计脱水' and status='1'");
            }

            #endregion

            DataTable dtTemp = new DataTable();
            int result = 0;
            if (currentPatient != null)
            {
                PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable dataTableSchedule = scheduleService.GetPatientScheduleSignle(cureDate, currentPatient["HEMODIALYSIS_ID"].ToString());
                if (dataTableSchedule != null && dataTableSchedule.Rows.Count > 0)
                {
                    PatientScheduleModel.MED_PATIENT_SCHEDULERow drSchedule = dataTableSchedule.Rows[0] as PatientScheduleModel.MED_PATIENT_SCHEDULERow;
                    HemodialysisModel.MED_HEMO_RECIPEDataTable recipeTable = objHemodialysisService.GetRecipeByHemodialysisIDAndDate(currentPatient["HEMODIALYSIS_ID"].ToString(), cureDate);
                    string strRecipeIDList = string.Empty;
                    string pName = drSchedule.PATIENTNAME;

                    if (recipeTable != null && recipeTable.Rows.Count > 0)
                    {
                        #region 判断预计脱水、透前体重、诊断是否录入，没有录入强制用户录入否则不能确认处方,达到质控要求。

                        if (diagnose != null && diagnose.Rows.Count > 0)
                        {
                            if (drSchedule["DIAGNOSE"].ToString().Length == 0)
                            {
                                AutoClosedMsgBox.ShowForm("患者【" + pName + "】的诊断未录入，不能确定透析处方。", "病患管理", 2000, MessageBoxIcon.Information);
                                return;
                            }
                        }

                        if (dtQualityCheck != null && dtQualityCheck.Rows.Count > 0)
                        {
                            if (todayWeight != null && todayWeight.Rows.Count > 0)
                            {
                                if (recipeTable.Rows[0]["TODAY_WEIGHT"].ToString().Length == 0 || recipeTable.Rows[0]["TODAY_WEIGHT"].ToString() == "0")
                                {
                                    AutoClosedMsgBox.ShowForm("患者【" + pName + "】的透前体重未测量，不能确定透析处方。", "病患管理", 2000, MessageBoxIcon.Information);
                                    return;
                                }
                            }

                            if (todayUfR != null && todayUfR.Rows.Count > 0)
                            {
                                if (recipeTable.Rows[0]["UFR"].ToString().Length == 0 || recipeTable.Rows[0]["UFR"].ToString() == "0")
                                {
                                    AutoClosedMsgBox.ShowForm("患者【" + pName + "】的当日预计脱水量未确定，不能确定透析处方。", "病患管理", 2000, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }

                        #endregion

                        strRecipeIDList = "'" + recipeTable.Rows[0]["RECIPE_ID"].ToString() + "'";
                        if (pStatus)
                        {
                            drSchedule.RECIPE_ID = recipeTable.Rows[0]["RECIPE_ID"].ToString();
                            drSchedule.PURIFIER_MODEL_ID = recipeTable.Rows[0]["FIRST_PURIFIER_MODEL"].ToString();
                            drSchedule.PURIFICATION_MODE = recipeTable.Rows[0]["PURIFICATION_MODE"].ToString();
                        }
                        else
                        {
                            drSchedule.RECIPE_ID = string.Empty;
                            drSchedule.PURIFIER_MODEL_ID = string.Empty;
                        }
                    }
                    else
                    {
                        AutoClosedMsgBox.ShowForm("患者透析ID:" + currentPatient["HEMODIALYSIS_ID"].ToString() + "的临时处方尚未创建,请先创建临时处方。", "病患管理", 2000, MessageBoxIcon.Warning);
                        return;
                    }

                    //根据处方编号取透析单号，判断患者该处方是否已经执行，如果已经执行不能取消。
                    HemodialysisModel.MED_CURE_MAINDataTable dtCure = objHemodialysisService.GetMainCureByRecipeId(recipeTable.Rows[0]["RECIPE_ID"].ToString());
                    if (dtCure != null && dtCure.Rows.Count > 0 && strStautsName == "取消")
                    {
                        AutoClosedMsgBox.ShowForm("该患者处方已执行不能取消!", "病患管理", 2000, MessageBoxIcon.Warning);
                        return;
                    }
                    //更新临时医嘱记录处方编号
                    HemodialysisModel.MED_CURE_DRUGDataTable drugDt = objHemodialysisService.GetValidCureDrugByHemoID(currentPatient["HEMODIALYSIS_ID"].ToString(), cureDate);
                    if (drugDt != null && drugDt.Rows.Count > 0)
                    {
                        drugDt.AsEnumerable().ToList().ForEach(drug =>
                        {
                            drug.RECIPE_ID = string.IsNullOrEmpty(drug.RECIPE_ID) ? strRecipeIDList.Replace("'", "") : drug.RECIPE_ID;
                        });
                        objHemodialysisService.SaveCureDrug(drugDt);
                    }
                    result = scheduleService.SavePatientScheduleInfo(dataTableSchedule);
                    result = objHemodialysisService.SaveRecipeUserIDByRecipeIDList(strRecipeIDList, HemoApplicationContext.Current.CurrentUser.EMP_NO);
                    if (result > 0)
                    {
                        AutoClosedMsgBox.ShowForm("该患者处方已" + strStautsName + "。", "病患管理", 2000, MessageBoxIcon.Warning);
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
            else
            {
                AutoClosedMsgBox.ShowForm("请先选择一个患者！", "病患管理", 2000, MessageBoxIcon.Warning);
            }
        }

        private string CheckArea(string strRoom, string strInf)
        {
            var rooms = configService.GetConfigList(string.Empty, string.Empty, "隔离病区", "1");
            if (rooms.Count(wh => wh.ITEM_VALUE == strRoom) > 0)//strRoom == "第五透析室"
            {
                if (strInf == "全阴" || string.IsNullOrEmpty(strInf))
                {
                    return "该患者没有标记为【乙肝或丙肝】,是否确认在隔离区域治疗？";
                }
            }
            return string.Empty;
        }

        #endregion
    }
}