/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:列表
 * 创建标识:顾伟伟-2013年7月8日
 * 
 * 修改时间:2013年10月16日
 * 修改人:刘超
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年1月24日
 * 修改人:顾伟伟
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年5月4日
 * 修改人:吕志强
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2017年10月11日
 * 修改人:刘配齐
 * 修改描述:治疗方法增加达肝素钠注射液（法安明）；净化器金宝有1.8改为1.4
 * ----------------------------------------------------------------*/
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.IService;
using System.Linq;
using Hemo.Client.Properties;
using Hemo.IService.PatientSchedule;
using Hemo.Client.Core;
using Hemo.IService.Lab;
using System.Drawing;
using Hemo.Client.Controls;
using Hemo.Client.UI.Drug;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class FastRecipeListNew : XtraUserControl
    {
        #region 类变量
        BusyIndicatorHelp busyIndicatorHelp = new BusyIndicatorHelp();

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        private IPatient patientService = ServiceManager.Instance.PatientService;

        private IPatientSchedule scheduleService = ServiceManager.Instance.PatientSchedule;

        private IConfig configService = ServiceManager.Instance.ConfigService;

        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IDrug objDrug = ServiceManager.Instance.DrugService;

        private string HEMODIALYSIS_ID = string.Empty;

        private ILab _labService = ServiceManager.Instance.LabService;

        private DataTable dtPatient = null;

        private HemodialysisModel.MED_HEMO_RECIPEDataTable dtRecipe = null;

        private HemodialysisModel.MED_HEMO_RECIPEDataTable dtLongRecipe = null;

        private HemodialysisModel.MED_CURE_MAINDataTable dtCure = null;

        private HemodialysisModel.MED_CURE_MAINDataTable cureDt = null;
        private HemodialysisModel.MED_CURE_DRUGDataTable cureDrugDt = null;
        private HemodialysisModel.MED_CURE_DRUGDataTable drugDt = new HemodialysisModel.MED_CURE_DRUGDataTable();

        private ConfigModel.MED_COMMON_RELATIONDataTable _relationData = new ConfigModel.MED_COMMON_RELATIONDataTable();

        private ConfigModel.MED_COMMON_ITEMLISTDataTable dtAccess = null;

        private HemoModel.MED_VASCULAR_ACCESSDataTable dtVasularAccess = null;

        private DataTable dtInfectResent = null;

        private DataTable dtPastCure = null;

        private DataTable dtPastPressure = null;

        private DataTable dtPastCureAndPressure = null;

        private decimal dryWeight = 0;

        private decimal todayWeight = 0;
        public PatientModel.MED_PATIENTSRow _currentPatientRow = null;

        public DateTime currentDt { get; set; }

        private bool IsToComputeUFR = false;

        /// <summary>
        /// 是否加透医嘱
        /// </summary>
        private bool IsOverOrder = false;
        /// <summary>
        /// 加透医嘱治疗单
        /// </summary>
        private string overOrderCureId = string.Empty;

        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public FastRecipeListNew()
        {
            InitializeComponent();
        }

        public void SetBtnCloseFalse()
        {
            dxSimpleButton1.Visible = false;
            dxSimpleButton2.Visible = false;
        }
        #endregion

        #region 事件

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FastRecipeList_Load(object sender, EventArgs e)
        {
            //this.Text = "快捷处方";
            //ProFunctionCount pfc = new ProFunctionCount();
            //pfc.SaveFunctionCountFrm(this);

            if (currentDt == null || currentDt == DateTime.MinValue || currentDt == DateTime.MaxValue)
            {
                currentDt = DateTime.Now.Date;
            }

            BindLookUpEdit();
            LoadDefaultSet();
            Query();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveRecipeMethond();

        }

        private void SaveRecipeMethond()
        {
            if (!IsDataValid())
            {
                return;
            }

            var row = this.gvPatientList.GetFocusedDataRow();
            if (row != null)
            {
                if (dtRecipe != null && dtRecipe.Rows.Count > 0)
                {
                    int result = 0;
                    dtRecipe.Rows[0]["DRY_WEIGHT"] = this.spnDRY_WEIGHT.EditValue;
                    dtRecipe.Rows[0]["TODAY_WEIGHT"] = this.spnTODAY_WEIGHT.EditValue;
                    dtRecipe.Rows[0]["UFR"] = this.spnUFR.EditValue;
                    dtRecipe.Rows[0]["VASCULAR_ACCESS_ID"] = this.lupVASCULAR_ACCESS_ID.EditValue;
                    dtRecipe.Rows[0]["TODAY_BLOODA"] = this.spnTODAY_BLOODA.EditValue;
                    dtRecipe.Rows[0]["TODAY_BLOODB"] = this.spnTODAY_BLOODB.EditValue;
                    dtRecipe.Rows[0]["TODAY_BLOODP"] = this.spnBLOODP.EditValue;
                    dtRecipe.Rows[0]["FREQUENCY_HOURS"] = this.spnFREQUENCY_HOURS.EditValue;
                    dtRecipe.Rows[0]["FREQUENCY_MINUTE"] = this.spnFREQUENCY_MINUTE.EditValue;

                    dtRecipe.Rows[0]["PURIFICATION_MODE"] = this.cmbPURIFICATION_MODE.EditValue;
                    dtRecipe.Rows[0]["FIRST_PURIFIER_MODEL"] = this.cmbFIRST_PURIFIER_MODEL.EditValue;
                    dtRecipe.Rows[0]["FIRST_PURIFIER_NAME"] = this.cmbFIRST_PURIFIER_NAME.EditValue;
                    dtRecipe.Rows[0]["FIRST_PURIFIER_M2"] = this.spnFIRST_PURIFIER_M2.EditValue;
                    dtRecipe.Rows[0]["FIRST_PURIFIER_KOA"] = this.spnFIRST_PURIFIER_KOA.EditValue;
                    dtRecipe.Rows[0]["FIRST_PURIFIER_KUF"] = this.spnFIRST_PURIFIER_KUF.EditValue;
                    dtRecipe.Rows[0]["SECOND_PURIFIER_MODEL"] = this.cmbSECOND_PURIFIER_MODEL.EditValue;
                    dtRecipe.Rows[0]["SECOND_PURIFIER_NAME"] = this.cmbSECOND_PURIFIER_NAME.EditValue;
                    dtRecipe.Rows[0]["SECOND_PURIFIER_M2"] = this.spnSECOND_PURIFIER_M2.EditValue;
                    dtRecipe.Rows[0]["SECOND_PURIFIER_KOA"] = this.spnSECOND_PURIFIER_KOA.EditValue;
                    dtRecipe.Rows[0]["SECOND_PURIFIER_KUF"] = this.spnSECOND_PURIFIER_KUF.EditValue;
                    dtRecipe.Rows[0]["THERAPEUTIC_METHOD"] = this.cmbTHERAPEUTIC_METHOD.EditValue;
                    dtRecipe.Rows[0]["FIRST_DRUG_DOSAGE"] = this.txtFIRST_DRUG_DOSAGE.Text;
                    dtRecipe.Rows[0]["FIRST_DRUG_UNIT"] = this.cmbFIRST_DRUG_UNIT.EditValue;
                    dtRecipe.Rows[0]["FIRST_DRUG_MODE"] = this.cmbFIRST_DRUG_MODE.EditValue;
                    dtRecipe.Rows[0]["SECOND_DRUG_DOSAGE"] = this.txtSECOND_DRUG_DOSAGE.Text;
                    dtRecipe.Rows[0]["SECOND_DRUG_UNIT"] = this.cmbSECOND_DRUG_UNIT.EditValue;
                    dtRecipe.Rows[0]["SECOND_DRUG_MODE"] = this.cmbSECOND_DRUG_MODE.EditValue;
                    dtRecipe.Rows[0]["DRY_WEIGHT_REMARK"] = txtDRY_WEIGHT_REMARK.Value;
                    dtRecipe.Rows[0]["FOCUS_LEVEL"] = this.lupFOCUS_LEVEL.EditValue;
                    dtRecipe.Rows[0]["SENSES"] = this.lupSENSES.EditValue;
                    dtRecipe.Rows[0]["ALLERGIC"] = this.lupALLERGIC.Text;
                    dtRecipe.Rows[0]["BR"] = this.txtBR.Value.ToString();
                    dtRecipe.Rows[0]["BEFORE_TEMPERATURE"] = this.txtBEFORE_TEMPERATURE.Value.ToString();

                    //dtRecipe.Rows[0]["REMARK"] = this.txtREMARK.Text;
                    dtRecipe.Rows[0]["REMARK"] = this.checkBed.Checked.ToString().ToUpper(); //卧床选项

                    dtRecipe.Rows[0]["DISPLACEMENT_MODE"] = this.lupDISPLACEMENT_MODE.EditValue;
                    dtRecipe.Rows[0]["DISPLACEMENT_FLOW"] = this.spnDISPLACEMENT_FLOW.EditValue;
                    dtRecipe.Rows[0]["DISPLACEMENT_RECIPE"] = this.lupDISPLACEMENT_RECIPE.EditValue;
                    dtRecipe.Rows[0]["BLOOW_FLOW"] = this.spnBLOOW_FLOW.EditValue;
                    dtRecipe.Rows[0]["UFR2"] = this.spnUFR2.EditValue;
                    dtRecipe.Rows[0]["DISPLACEMENT_SPECIAL_ADJUST"] = this.txtDISPLACEMENT_SPECIAL_ADJUST.Text;
                    dtRecipe.Rows[0]["ANTICOAGULANT_USE"] = this.txtANTICOAGULANT_USE.Text;
                    dtRecipe.Rows[0]["SPECIAL_MATTER"] = this.txtSPECIAL_MATTER.Text;



                    result = hemoService.SaveRecipe(dtRecipe);

                    dtCure = hemoService.GetMainCureByRecipeId(dtRecipe[0].RECIPE_ID);
                    if (dtCure != null && dtCure.Rows.Count > 0)
                    {
                        dtCure.Rows[0]["DRY_WEIGHT"] = this.spnDRY_WEIGHT.EditValue;
                        dtCure.Rows[0]["BEFORE_DRY_WEIGHT"] = this.spnTODAY_WEIGHT.EditValue;
                        dtCure.Rows[0]["UFR"] = this.spnUFR.EditValue;
                        dtCure.Rows[0]["FREQUENCY_HOURS"] = this.spnFREQUENCY_HOURS.EditValue;
                        dtCure.Rows[0]["FREQUENCY_MINUTE"] = this.spnFREQUENCY_MINUTE.EditValue;
                        dtCure.Rows[0]["AFTER_DRY_WEIGHT"] = this.spnAFTER_DRY_WEIGHT.EditValue;
                        dtCure.Rows[0]["VASCULAR_ACCESS_ID"] = this.lupVASCULAR_ACCESS_ID.EditValue;
                        dtCure.Rows[0]["BEFORE_SYSTOLIC_PRESSURE"] = this.spnTODAY_BLOODA.EditValue;
                        dtCure.Rows[0]["BEFORE_DIASTOLIC_PRESSURE"] = this.spnTODAY_BLOODB.EditValue;
                        dtCure.Rows[0]["BEFORE_BP"] = this.spnBLOODP.EditValue;

                        dtCure.Rows[0]["PURIFICATION_MODE"] = this.cmbPURIFICATION_MODE.EditValue;
                        dtCure.Rows[0]["MACHINE_TYPE"] = this.cmbFIRST_PURIFIER_MODEL.EditValue;
                        dtCure.Rows[0]["PURIFIER_NAME"] = this.cmbFIRST_PURIFIER_NAME.EditValue;
                        dtCure.Rows[0]["PURIFIER_M2"] = this.spnFIRST_PURIFIER_M2.EditValue;
                        dtCure.Rows[0]["HEPARIN_SPECIES"] = this.cmbTHERAPEUTIC_METHOD.EditValue;
                        dtCure.Rows[0]["FIRST_HEPARIN"] = Utility.CDecimal(this.txtFIRST_DRUG_DOSAGE.Text);
                        dtCure.Rows[0]["FIRST_DRUG_UNIT"] = this.cmbFIRST_DRUG_UNIT.EditValue;
                        dtCure.Rows[0]["DOSIS_SUSTENTATIVA"] = Utility.CDecimal(this.txtSECOND_DRUG_DOSAGE.Text);
                        dtCure.Rows[0]["SECOND_DRUG_UNIT"] = this.cmbSECOND_DRUG_UNIT.EditValue;
                        dtCure.Rows[0]["DRY_WEIGHT_TAG"] = txtDRY_WEIGHT_REMARK.Value;

                        dtCure.Rows[0]["VASCULAR_ACCESS_TYPE"] = this.checkBed.Checked.ToString().ToUpper();
                        dtCure.Rows[0]["BR"] = this.txtBR.Value.ToString();
                        dtCure.Rows[0]["BEFORE_TEMPERATURE"] = this.txtBEFORE_TEMPERATURE.Value.ToString();

                        dtCure.Rows[0]["FOCUS_LEVEL"] = this.lupFOCUS_LEVEL.EditValue;
                        dtCure.Rows[0]["SENSES"] = this.lupSENSES.EditValue;
                        dtCure.Rows[0]["ALLERGIC"] = this.lupALLERGIC.Text;

                        dtCure.Rows[0]["DISPLACEMENT_MODE"] = this.lupDISPLACEMENT_MODE.EditValue;
                        dtCure.Rows[0]["DISPLACEMENT_FLOW"] = this.spnDISPLACEMENT_FLOW.EditValue;
                        dtCure.Rows[0]["DISPLACEMENT_RECIPE"] = this.lupDISPLACEMENT_RECIPE.EditValue;
                        dtCure.Rows[0]["BLOOW_FLOW"] = this.spnBLOOW_FLOW.EditValue;
                        dtCure.Rows[0]["UFR2"] = this.spnUFR2.EditValue;
                        dtCure.Rows[0]["DISPLACEMENT_SPECIAL_ADJUST"] = this.txtDISPLACEMENT_SPECIAL_ADJUST.Text;
                        dtCure.Rows[0]["ANTICOAGULANT_USE"] = this.txtANTICOAGULANT_USE.Text;
                        dtCure.Rows[0]["SPECIAL_MATTER"] = this.txtSPECIAL_MATTER.Text;

                        result = hemoService.SaveCureMain(dtCure);
                    }
                    HEMODIALYSIS_ID = this.txtHemoID.Text;

                    if (result == 1)
                    {
                        var index = this.gvPatientList.FocusedRowHandle;
                        var r = this.gvPatientList.GetDataRow(index);
                        ConfirmOrCancelOnePatient(true, r);
                        Query();

                    }
                }
                else
                {
                    AutoClosedMsgBox.ShowForm("患者临时处方尚未创建，请先创建临时处方。", "提示", 1000, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// 确认处方
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSure_Click(object sender, EventArgs e)
        {
            SaveRecipeMethond();

            #region 方法进行提取 SveRecipeMethond
            /*
            if (!IsDataValid())
            {
                return;
            }

            var row = this.gvPatientList.GetFocusedDataRow();
            if (row != null)
            {
                if (dtRecipe != null && dtRecipe.Rows.Count > 0)
                {
                    int result = 0;
                    dtRecipe.Rows[0]["DRY_WEIGHT"] = this.spnDRY_WEIGHT.EditValue;
                    dtRecipe.Rows[0]["TODAY_WEIGHT"] = this.spnTODAY_WEIGHT.EditValue;
                    dtRecipe.Rows[0]["UFR"] = this.spnUFR.EditValue;
                    dtRecipe.Rows[0]["VASCULAR_ACCESS_ID"] = this.lupVASCULAR_ACCESS_ID.EditValue;
                    dtRecipe.Rows[0]["TODAY_BLOODA"] = this.spnTODAY_BLOODA.EditValue;
                    dtRecipe.Rows[0]["TODAY_BLOODB"] = this.spnTODAY_BLOODB.EditValue;
                    dtRecipe.Rows[0]["TODAY_BLOODP"] = this.spnBLOODP.EditValue;
                    dtRecipe.Rows[0]["FREQUENCY_HOURS"] = this.spnFREQUENCY_HOURS.EditValue;
                    dtRecipe.Rows[0]["PURIFICATION_MODE"] = this.cmbPURIFICATION_MODE.EditValue;
                    dtRecipe.Rows[0]["FIRST_PURIFIER_MODEL"] = this.cmbFIRST_PURIFIER_MODEL.EditValue;
                    dtRecipe.Rows[0]["FIRST_PURIFIER_NAME"] = this.cmbFIRST_PURIFIER_NAME.EditValue;
                    dtRecipe.Rows[0]["FIRST_PURIFIER_M2"] = this.spnFIRST_PURIFIER_M2.EditValue;
                    dtRecipe.Rows[0]["FIRST_PURIFIER_KOA"] = this.spnFIRST_PURIFIER_KOA.EditValue;
                    dtRecipe.Rows[0]["FIRST_PURIFIER_KUF"] = this.spnFIRST_PURIFIER_KUF.EditValue;
                    dtRecipe.Rows[0]["SECOND_PURIFIER_MODEL"] = this.cmbSECOND_PURIFIER_MODEL.EditValue;
                    dtRecipe.Rows[0]["SECOND_PURIFIER_NAME"] = this.cmbSECOND_PURIFIER_NAME.EditValue;
                    dtRecipe.Rows[0]["SECOND_PURIFIER_M2"] = this.spnSECOND_PURIFIER_M2.EditValue;
                    dtRecipe.Rows[0]["SECOND_PURIFIER_KOA"] = this.spnSECOND_PURIFIER_KOA.EditValue;
                    dtRecipe.Rows[0]["SECOND_PURIFIER_KUF"] = this.spnSECOND_PURIFIER_KUF.EditValue;
                    dtRecipe.Rows[0]["THERAPEUTIC_METHOD"] = this.cmbTHERAPEUTIC_METHOD.EditValue;
                    dtRecipe.Rows[0]["REMARK"] = this.txtREMARK.Text;
                    dtRecipe.Rows[0]["FIRST_DRUG_DOSAGE"] = this.txtFIRST_DRUG_DOSAGE.Text;
                    dtRecipe.Rows[0]["FIRST_DRUG_UNIT"] = this.cmbFIRST_DRUG_UNIT.EditValue;
                    dtRecipe.Rows[0]["FIRST_DRUG_MODE"] = this.cmbFIRST_DRUG_MODE.EditValue;
                    dtRecipe.Rows[0]["SECOND_DRUG_DOSAGE"] = this.txtSECOND_DRUG_DOSAGE.Text;
                    dtRecipe.Rows[0]["SECOND_DRUG_UNIT"] = this.cmbSECOND_DRUG_UNIT.EditValue;
                    dtRecipe.Rows[0]["SECOND_DRUG_MODE"] = this.cmbSECOND_DRUG_MODE.EditValue;
                    dtRecipe.Rows[0]["DISPLACEMENT_MODE"] = this.lupDISPLACEMENT_MODE.EditValue;
                    dtRecipe.Rows[0]["DISPLACEMENT_FLOW"] = this.spnDISPLACEMENT_FLOW.EditValue;
                    dtRecipe.Rows[0]["DISPLACEMENT_RECIPE"] = this.lupDISPLACEMENT_RECIPE.EditValue;
                    dtRecipe.Rows[0]["BLOOW_FLOW"] = this.spnBLOOW_FLOW.EditValue;
                    dtRecipe.Rows[0]["UFR2"] = this.spnUFR2.EditValue;
                    dtRecipe.Rows[0]["DISPLACEMENT_SPECIAL_ADJUST"] = this.txtDISPLACEMENT_SPECIAL_ADJUST.Text;
                    dtRecipe.Rows[0]["ANTICOAGULANT_USE"] = this.txtANTICOAGULANT_USE.Text;
                    dtRecipe.Rows[0]["SPECIAL_MATTER"] = this.txtSPECIAL_MATTER.Text;

                    result = hemoService.SaveRecipe(dtRecipe);

                    dtCure = hemoService.GetMainCureByRecipeId(dtRecipe[0].RECIPE_ID);
                    if (dtCure != null && dtCure.Rows.Count > 0)
                    {
                        dtCure.Rows[0]["DRY_WEIGHT"] = this.spnDRY_WEIGHT.EditValue;
                        dtCure.Rows[0]["BEFORE_DRY_WEIGHT"] = this.spnTODAY_WEIGHT.EditValue;
                        dtCure.Rows[0]["UFR"] = this.spnUFR.EditValue;
                        dtCure.Rows[0]["FREQUENCY_HOURS"] = this.spnFREQUENCY_HOURS.EditValue;
                        dtCure.Rows[0]["AFTER_DRY_WEIGHT"] = this.spnAFTER_DRY_WEIGHT.EditValue;
                        dtCure.Rows[0]["VASCULAR_ACCESS_ID"] = this.lupVASCULAR_ACCESS_ID.EditValue;
                        dtCure.Rows[0]["BEFORE_SYSTOLIC_PRESSURE"] = this.spnTODAY_BLOODA.EditValue;
                        dtCure.Rows[0]["BEFORE_DIASTOLIC_PRESSURE"] = this.spnTODAY_BLOODB.EditValue;
                        dtCure.Rows[0]["BEFORE_BP"] = this.spnBLOODP.EditValue;
                        dtCure.Rows[0]["PURIFICATION_MODE"] = this.cmbPURIFICATION_MODE.EditValue;
                        dtCure.Rows[0]["MACHINE_TYPE"] = this.cmbFIRST_PURIFIER_MODEL.EditValue;
                        dtCure.Rows[0]["PURIFIER_NAME"] = this.cmbFIRST_PURIFIER_NAME.EditValue;
                        dtCure.Rows[0]["PURIFIER_M2"] = this.spnFIRST_PURIFIER_M2.EditValue;
                        dtCure.Rows[0]["HEPARIN_SPECIES"] = this.cmbTHERAPEUTIC_METHOD.EditValue;
                        dtCure.Rows[0]["FIRST_HEPARIN"] = Utility.CDecimal(this.txtFIRST_DRUG_DOSAGE.Text);
                        dtCure.Rows[0]["FIRST_DRUG_UNIT"] = this.cmbFIRST_DRUG_UNIT.EditValue;
                        dtCure.Rows[0]["DOSIS_SUSTENTATIVA"] = Utility.CDecimal(this.txtSECOND_DRUG_DOSAGE.Text);
                        dtCure.Rows[0]["SECOND_DRUG_UNIT"] = this.cmbSECOND_DRUG_UNIT.EditValue;
                        dtCure.Rows[0]["DOCTOR_ADVICE"] = this.txtREMARK.Text;

                        dtCure.Rows[0]["DISPLACEMENT_MODE"] = this.lupDISPLACEMENT_MODE.EditValue;
                        dtCure.Rows[0]["DISPLACEMENT_FLOW"] = this.spnDISPLACEMENT_FLOW.EditValue;
                        dtCure.Rows[0]["DISPLACEMENT_RECIPE"] = this.lupDISPLACEMENT_RECIPE.EditValue;
                        dtCure.Rows[0]["BLOOW_FLOW"] = this.spnBLOOW_FLOW.EditValue;
                        dtCure.Rows[0]["UFR2"] = this.spnUFR2.EditValue;
                        dtCure.Rows[0]["DISPLACEMENT_SPECIAL_ADJUST"] = this.txtDISPLACEMENT_SPECIAL_ADJUST.Text;
                        dtCure.Rows[0]["ANTICOAGULANT_USE"] = this.txtANTICOAGULANT_USE.Text;
                        dtCure.Rows[0]["SPECIAL_MATTER"] = this.txtSPECIAL_MATTER.Text;

                        result = hemoService.SaveCureMain(dtCure);
                    }

               
                }
                else
                {
                    AutoClosedMsgBox.ShowForm("患者临时处方尚未创建，请先创建临时处方。", "提示", 1000, MessageBoxIcon.Information);
                }
            }
            */
            #endregion
        }

        /// <summary>
        /// 取消确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            var index = this.gvPatientList.FocusedRowHandle;
            var row = this.gvPatientList.GetDataRow(index);
            ConfirmOrCancelOnePatient(false, row);
            Query();
        }

        /// <summary>
        /// 确认处方
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var index = this.gvPatientList.FocusedRowHandle;
            var row = this.gvPatientList.GetDataRow(index);
            ConfirmOrCancelOnePatient(true, row);
            Query();
        }

        /// <summary>
        /// 取消确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var index = this.gvPatientList.FocusedRowHandle;
            var row = this.gvPatientList.GetDataRow(index);
            ConfirmOrCancelOnePatient(false, row);
            Query();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        /// <summary>
        /// 点击列表行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPatientList_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {



            xtraTabControl1.SelectedTabPage = this.xtraTabPageRecipe;
            var row = this.gvPatientList.GetFocusedDataRow();
            if (row != null)
            {
                groupControl4.Text = string.Format("{0}-{1}近期透析信息", row["AREA_NAME"].ToString(), row["NAME"].ToString());
                btn_UseTemplate.Tag = row["HEMODIALYSIS_ID"].ToString();
                //using (BackgroundWorker worker = new BackgroundWorker())
                //{
                //    worker.DoWork += new DoWorkEventHandler(worker3_DoWork);
                //    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker3_RunWorkerCompleted);
                //    worker.RunWorkerAsync(row);
                //}

                using (BackgroundWorker worker = new BackgroundWorker())
                {
                    busyIndicatorHelp.ShowMessage();
                    busyIndicatorHelp.SetWaitFormCaption("数据加载中....");
                    worker.DoWork += new DoWorkEventHandler(worker2_DoWork);
                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker2_RunWorkerCompleted);
                    worker.RunWorkerAsync(row);
                }


                this.btnNew.Enabled = true;

            }
        }

        /// <summary>
        /// 透析室选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lupArea_EditValueChanged(object sender, EventArgs e)
        {
            string type = this.lupArea.Text.Equals("CRRT") ? "CRRT净化方式" : "净化方式";
            BindBanci(this.lupArea.Text);
            BaseControlInfo.BindLookUpEdit(this.cmbPURIFICATION_MODE, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, type, "1"), "ITEM_NAME", "净化方式");
        }

        /// <summary>
        /// 姓名改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPatientName_TextChanged(object sender, EventArgs e)
        {
            var dt = new DataTable();
            dt = dtPatient.Clone();
            dt.Clear();
            dtPatient.AsEnumerable().Where(i => i["NAME"].ToString().Contains(txtPatientName.Text.Trim()) || PinYinConverter.GetPYString(i["NAME"].ToString()).ToUpper().Contains(txtPatientName.Text.ToUpper())).CopyToDataTable(dt, LoadOption.PreserveChanges);
            this.gcPatientList.DataSource = dt;
        }

        /// <summary>
        /// 干体重值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnDRY_WEIGHT_EditValueChanged(object sender, EventArgs e)
        {
            ChangeValue(spnDRY_WEIGHT);
        }

        /// <summary>
        /// 透前体重值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnTODAY_WEIGHT_EditValueChanged(object sender, EventArgs e)
        {
            ChangeValue(spnTODAY_WEIGHT);
        }

        /// <summary>
        /// 预计脱水值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnUFR_EditValueChanged(object sender, EventArgs e)
        {
            CheckNumberValue(spnUFR);

            var QzValue = spnFREQUENCY_HOURS.Value + spnFREQUENCY_MINUTE.Value / 60;
            this.spnUFR2.Value = Math.Floor((spnUFR.Value * 1000 + 300) / QzValue);
        }

        /// <summary>
        /// 面积值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnFIRST_PURIFIER_M2_EditValueChanged(object sender, EventArgs e)
        {
            CheckNumberValue(spnFIRST_PURIFIER_M2);
        }

        /// <summary>
        /// Koa值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnFIRST_PURIFIER_KOA_EditValueChanged(object sender, EventArgs e)
        {
            CheckNumberValue(spnFIRST_PURIFIER_KOA);
        }

        /// <summary>
        /// Kuf值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnFIRST_PURIFIER_KUF_EditValueChanged(object sender, EventArgs e)
        {
            CheckNumberValue(spnFIRST_PURIFIER_KUF);
        }

        /// <summary>
        /// 面积2值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnSECOND_PURIFIER_M2_EditValueChanged(object sender, EventArgs e)
        {
            CheckNumberValue(spnSECOND_PURIFIER_M2);
        }

        /// <summary>
        /// Koa2值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnSECOND_PURIFIER_KOA_EditValueChanged(object sender, EventArgs e)
        {
            CheckNumberValue(spnSECOND_PURIFIER_KOA);
        }

        /// <summary>
        /// Kuf2值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnSECOND_PURIFIER_KUF_EditValueChanged(object sender, EventArgs e)
        {
            CheckNumberValue(spnSECOND_PURIFIER_KUF);
        }

        /// <summary>
        /// 超滤率值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnUFR2_EditValueChanged(object sender, EventArgs e)
        {
            CheckNumberValue(spnUFR2);
        }

        /// <summary>
        /// 置换液流速值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnDISPLACEMENT_FLOW_EditValueChanged(object sender, EventArgs e)
        {
            CheckNumberValue(spnDISPLACEMENT_FLOW);
        }

        /// <summary>
        /// 血流速度值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnBLOOW_FLOW_EditValueChanged(object sender, EventArgs e)
        {
            CheckNumberValue(spnBLOOW_FLOW);
        }

        /// <summary>
        /// 净化方式选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPURIFICATION_MODE_EditValueChanged(object sender, EventArgs e)
        {
            if (this.cmbPURIFICATION_MODE.Text == "HD")
            {
                this.cmbFIRST_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";//聚砜膜
                this.spnFIRST_PURIFIER_M2.EditValue = 1.3;
                this.cmbSECOND_PURIFIER_MODEL.EditValue = null;
                this.cmbSECOND_PURIFIER_NAME.EditValue = null;
                this.spnSECOND_PURIFIER_M2.EditValue = 0;
            }
            else if (this.cmbPURIFICATION_MODE.Text == "HDF")
            {
                this.cmbFIRST_PURIFIER_MODEL.EditValue = "59b5a10f-5a1f-4192-bc70-6be6b40efdb1";//FX80
                this.cmbFIRST_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";//聚砜膜
                this.spnFIRST_PURIFIER_M2.EditValue = 1.8;
                this.cmbSECOND_PURIFIER_MODEL.EditValue = null;
                this.cmbSECOND_PURIFIER_NAME.EditValue = null;
                this.spnSECOND_PURIFIER_M2.EditValue = 0;
            }
            else if (this.cmbPURIFICATION_MODE.Text == "HD+HP")
            {
                this.cmbFIRST_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";//聚砜膜
                this.spnFIRST_PURIFIER_M2.EditValue = 1.3;
                this.cmbSECOND_PURIFIER_MODEL.EditValue = "61c16922-87a4-4176-8e6c-726b04baea7b";//HP130
                this.cmbSECOND_PURIFIER_NAME.EditValue = "92f926c3-cb00-43cf-b4b5-7011597d51e7";//树脂
                this.spnSECOND_PURIFIER_M2.EditValue = 0;
            }
            else
            {
                //this.cmbFIRST_PURIFIER_MODEL.EditValue = null;
                //this.cmbFIRST_PURIFIER_NAME.EditValue = null;
                //this.spnFIRST_PURIFIER_M2.EditValue = 0;
                //this.cmbSECOND_PURIFIER_MODEL.EditValue = null;
                //this.cmbSECOND_PURIFIER_NAME.EditValue = null;
                //this.spnSECOND_PURIFIER_M2.EditValue = 0;
            }
        }

        /// <summary>
        /// 净化器型号选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFIRST_PURIFIER_MODEL_EditValueChanged(object sender, EventArgs e)
        {
            if (this.cmbFIRST_PURIFIER_MODEL.EditValue == null) return;
            var _relationshift = new ConfigModel.MED_COMMON_RELATIONDataTable();
            _relationData.Where(i => i.RELATIONTYPE == "1").CopyToDataTable<ConfigModel.MED_COMMON_RELATIONRow>(_relationshift, LoadOption.PreserveChanges);

            var row = _relationshift.FirstOrDefault(i => i.RELATIONNAME == this.cmbFIRST_PURIFIER_MODEL.EditValue.ToString());

            if (row != null)
            {
                cmbFIRST_PURIFIER_NAME.EditValue = row.ITEMNAME;
                spnFIRST_PURIFIER_M2.Text = row.DOSAGE;
                spnFIRST_PURIFIER_KOA.Text = row.UNIT;
                spnFIRST_PURIFIER_KUF.Text = row.DRUGMODE;
            }
            else
            {
                if (this.cmbFIRST_PURIFIER_MODEL.Text.Equals("威高F15"))
                {
                    this.cmbFIRST_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";
                    this.spnFIRST_PURIFIER_M2.EditValue = 1.5;
                }
                else if (this.cmbFIRST_PURIFIER_MODEL.Text.Equals("FX80"))
                {
                    this.cmbFIRST_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";
                    this.spnFIRST_PURIFIER_M2.EditValue = 1.8;
                }
                else if (this.cmbFIRST_PURIFIER_MODEL.Text.Equals("金宝Polyflux 14L"))
                {
                    this.cmbFIRST_PURIFIER_NAME.EditValue = "52269ca7-b9ba-490a-9164-8199220ec61b";
                    this.spnFIRST_PURIFIER_M2.EditValue = 1.4;
                }
                else if (this.cmbFIRST_PURIFIER_MODEL.Text.Equals("HP130"))
                {
                    this.cmbFIRST_PURIFIER_NAME.EditValue = "92f926c3-cb00-43cf-b4b5-7011597d51e7";
                }
            }


        }

        /// <summary>
        /// 净化器型号2选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSECOND_PURIFIER_MODEL_EditValueChanged(object sender, EventArgs e)
        {
            if (this.cmbSECOND_PURIFIER_MODEL.EditValue == null) return;
            var _relationshift = new ConfigModel.MED_COMMON_RELATIONDataTable();
            _relationData.Where(i => i.RELATIONTYPE == "1").CopyToDataTable<ConfigModel.MED_COMMON_RELATIONRow>(_relationshift, LoadOption.PreserveChanges);

            var row = _relationshift.FirstOrDefault(i => i.RELATIONNAME == this.cmbSECOND_PURIFIER_MODEL.EditValue.ToString());

            if (row != null)
            {
                cmbFIRST_PURIFIER_NAME.EditValue = row.ITEMNAME;
                spnFIRST_PURIFIER_M2.Text = row.DOSAGE;
                spnFIRST_PURIFIER_KOA.Text = row.UNIT;
                spnFIRST_PURIFIER_KUF.Text = row.DRUGMODE;
            }
            else
            {
                if (this.cmbSECOND_PURIFIER_MODEL.Text.Equals("威高F15"))
                {
                    this.cmbSECOND_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";
                    this.spnSECOND_PURIFIER_M2.EditValue = 1.5;
                }
                else if (this.cmbSECOND_PURIFIER_MODEL.Text.Equals("FX80"))
                {
                    this.cmbSECOND_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";
                    this.spnSECOND_PURIFIER_M2.EditValue = 1.8;
                }
                else if (this.cmbSECOND_PURIFIER_MODEL.Text.Equals("金宝Polyflux 14L"))
                {
                    this.cmbSECOND_PURIFIER_NAME.EditValue = "52269ca7-b9ba-490a-9164-8199220ec61b";
                    this.spnSECOND_PURIFIER_M2.EditValue = 1.4;
                }
                else if (this.cmbSECOND_PURIFIER_MODEL.Text.Equals("HP130"))
                {
                    this.cmbSECOND_PURIFIER_NAME.EditValue = "92f926c3-cb00-43cf-b4b5-7011597d51e7";
                }
            }


        }

        /// <summary>
        /// 抗凝治疗方法选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTHERAPEUTIC_METHOD_EditValueChanged(object sender, EventArgs e)
        {
            if (this.cmbTHERAPEUTIC_METHOD.Text.Equals("低分子肝素抗凝"))
            {
                cmbFIRST_DRUG_UNIT.EditValue = "74e7e438-7f88-4e51-b8f1-376023e803c1";
                cmbFIRST_DRUG_MODE.EditValue = "d18bc2cf-a2f6-4544-ad1c-2dfe93b82d7d";
                cmbSECOND_DRUG_UNIT.EditValue = "74e7e438-7f88-4e51-b8f1-376023e803c1";
                cmbSECOND_DRUG_MODE.EditValue = "d18bc2cf-a2f6-4544-ad1c-2dfe93b82d7d";
            }
            else if (this.cmbTHERAPEUTIC_METHOD.Text.Equals("普通肝素抗凝") || this.cmbTHERAPEUTIC_METHOD.Text.Equals("阿加曲班"))
            {
                this.cmbFIRST_DRUG_UNIT.EditValue = "a0a7c82b-78d8-431c-b9f9-fd78f34c8a4c";
                this.cmbFIRST_DRUG_MODE.EditValue = "d18bc2cf-a2f6-4544-ad1c-2dfe93b82d7d";
                this.cmbSECOND_DRUG_UNIT.EditValue = "a0a7c82b-78d8-431c-b9f9-fd78f34c8a4c";
                this.cmbSECOND_DRUG_MODE.EditValue = "d18bc2cf-a2f6-4544-ad1c-2dfe93b82d7d";
            }
            else if (this.cmbTHERAPEUTIC_METHOD.Text.Equals("枸橼酸2.5抗凝"))
            {
                this.txtSECOND_DRUG_DOSAGE.Text = "165";
                this.cmbSECOND_DRUG_UNIT.EditValue = "D70CA3B1796E46358F7CE06220E6A25F";
            }
            else if (this.cmbTHERAPEUTIC_METHOD.Text.Equals("枸橼酸2.8抗凝"))
            {
                this.txtSECOND_DRUG_DOSAGE.Text = "185";
                this.cmbSECOND_DRUG_UNIT.EditValue = "D70CA3B1796E46358F7CE06220E6A25F";
            }
            else if (this.cmbTHERAPEUTIC_METHOD.Text.Equals("枸橼酸3.0抗凝"))
            {
                this.txtSECOND_DRUG_DOSAGE.Text = "198";
                this.cmbSECOND_DRUG_UNIT.EditValue = "D70CA3B1796E46358F7CE06220E6A25F";
            }
            else
            {
                this.cmbFIRST_DRUG_UNIT.EditValue = string.Empty;
                this.cmbFIRST_DRUG_MODE.EditValue = string.Empty;
                this.cmbSECOND_DRUG_UNIT.EditValue = string.Empty;
                this.cmbSECOND_DRUG_MODE.EditValue = string.Empty;
            }
        }

        /// <summary>
        /// 追加单位选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSECOND_DRUG_UNIT_EditValueChanged(object sender, EventArgs e)
        {
            //if (this.cmbSECOND_DRUG_UNIT.Text.Equals("mg") || this.cmbSECOND_DRUG_UNIT.Text.Equals("μg") || this.cmbSECOND_DRUG_UNIT.Text.Equals("ml") || this.cmbSECOND_DRUG_UNIT.Text.Equals("u"))
            //{
            //    this.labelControl46.Text = "/h";
            //}
            //else
            //{
            //    this.labelControl46.Text = "/小时";
            //}
        }

        /// <summary>
        /// 双击历史记录列表行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            var row = this.gridView1.GetFocusedDataRow() as DataRow;
            if (row != null)
            {
                ShowCureInfo cureInfo = new ShowCureInfo();
                cureInfo.CureId = row["CURE_ID"].ToString();
                cureInfo.RECIPE_ID = row["RECIPE_ID"].ToString();
                cureInfo.AreaName = row["AREA_NAME"].ToString();
                cureInfo.ShowDialog(this);
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载默认设置
        /// </summary>
        private void LoadDefaultSet()
        {
            this.txtStartDate.DateTime = currentDt;

            if (_currentPatientRow != null)
            {
                this.lupClass.EditValue = _currentPatientRow.UPSTATE.ToString();
                this.lupArea.EditValue = _currentPatientRow.ROOMID.ToString();
            }
            else
            {

                this.lupClass.EditValue = (this.lupClass.Properties.DataSource as DataTable).Rows[1]["ITEM_VALUE"];
                this.lupArea.EditValue = (this.lupArea.Properties.DataSource as DataTable).Rows[1]["ITEM_ID"];
            }

            this.picUnsure.EditValue = global::Hemo.Client.Properties.Resources.处方未确定;
            this.picSure.EditValue = global::Hemo.Client.Properties.Resources.处方确定;
            this.picNoOpen.EditValue = global::Hemo.Client.Properties.Resources.处方未开;
        }

        /// <summary>
        /// 下拉项绑定
        /// </summary>
        private void BindLookUpEdit()
        {
            //区域
            ConfigModel.MED_COMMON_ITEMLISTDataTable dtArea = configService.GetConfigList(string.Empty, string.Empty, "区域", "1");
            if (dtArea != null && dtArea.Rows.Count > 0)
            {
                DataRow row = dtArea.NewRow();
                row["ITEM_NAME"] = "全部";
                row["ITEM_ID"] = "c5540d95c-76a2-4af4-893a-13tg43gdf34f";
                row["ORDER_NUMBER"] = 0;
                dtArea.Rows.InsertAt(row, 0);
                Utility.BindLookUpEdit(this.lupArea, "ITEM_ID", "ITEM_NAME", (DataTable)dtArea, "ITEM_NAME", "区域");
            }

            //班次
            BindBanci(this.lupArea.Text);

            //净化方式
            BaseControlInfo.BindLookUpEdit(this.cmbPURIFICATION_MODE, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "净化方式", "1"), "ITEM_NAME", "净化方式");
            //净化器类型
            BaseControlInfo.BindLookUpEdit(this.cmbFIRST_PURIFIER_MODEL, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "净化器类型", "1"), "ITEM_NAME", "净化器类型");
            BaseControlInfo.BindLookUpEdit(this.cmbSECOND_PURIFIER_MODEL, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "净化器类型", "1"), "ITEM_NAME", "净化器类型");
            //透析膜
            BaseControlInfo.BindLookUpEdit(this.cmbFIRST_PURIFIER_NAME, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "透析膜", "1"), "ITEM_NAME", "透析膜");
            BaseControlInfo.BindLookUpEdit(this.cmbSECOND_PURIFIER_NAME, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "透析膜", "1"), "ITEM_NAME", "透析膜");
            //治疗方法
            BaseControlInfo.BindLookUpEdit(this.cmbTHERAPEUTIC_METHOD, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "治疗方法", "1"), "ITEM_NAME", "治疗方法");
            //药品单位
            BaseControlInfo.BindLookUpEdit(this.cmbFIRST_DRUG_UNIT, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "药品单位", "1"), "ITEM_NAME", "药品单位");
            BaseControlInfo.BindLookUpEdit(this.cmbSECOND_DRUG_UNIT, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "药品单位", "1"), "ITEM_NAME", "药品单位");
            //注射方式
            BaseControlInfo.BindLookUpEdit(cmbFIRST_DRUG_MODE, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "注射方式", "1"), "ITEM_NAME", "注射方式");
            BaseControlInfo.BindLookUpEdit(cmbSECOND_DRUG_MODE, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "注射方式", "1"), "ITEM_NAME", "注射方式");
            //置换方式
            BaseControlInfo.BindLookUpEdit(this.lupDISPLACEMENT_MODE, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "置换方式", "1"), "ITEM_NAME", "置换方式");
            //置换液配方
            BaseControlInfo.BindLookUpEdit(this.lupDISPLACEMENT_RECIPE, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "置换液配方", "1"), "ITEM_NAME", "置换液配方");

            BaseControlInfo.BindLookUpEdit(lupFOCUS_LEVEL, "ITEM_ID", "ITEM_NAME", this.configService.GetConfigList(string.Empty, string.Empty, "病情", "1"), "ITEM_NAME", "病情");
            BaseControlInfo.BindLookUpEdit(lupSENSES, "ITEM_ID", "ITEM_NAME", this.configService.GetConfigList(string.Empty, string.Empty, "神志", "1"), "ITEM_NAME", "神志");
            this.lupFOCUS_LEVEL.EditValue = this.configService.GetConfigList(string.Empty, string.Empty, "病情", "1").First().ITEM_ID;
            this.lupSENSES.EditValue = this.configService.GetConfigList(string.Empty, string.Empty, "神志", "1").First().ITEM_ID;

            //药品绑定
            txtDRUG_NAME.Properties.DataSource = objDrug.GetDrugMasterList();
            txtDRUG_NAME.Properties.PopupFormSize = new Size(400, 230);
            txtDRUG_NAME.Properties.DisplayMember = "DRUG_NAME";//要显示的字段,Text获得
            txtDRUG_NAME.Properties.ValueMember = "DRUG_CODE";//实际值的字段,EditValue获得 // DeptID
            BaseControlInfo.BindLookUpEdit(cmbDOSAGE_UNITS, "ITEM_ID", "ITEM_NAME", this.configService.GetConfigList(string.Empty, string.Empty, "药品单位", "1"), "ITEM_NAME", "药品单位");
            BaseControlInfo.BindLookUpEdit(cmbDRUG_MODE, "ITEM_ID", "ITEM_NAME", this.configService.GetConfigList(string.Empty, string.Empty, "注射方式", "1"), "ITEM_NAME", "使用方式");


        }

        /// <summary>
        /// 绑定班次
        /// </summary>
        /// <param name="areaName"></param>
        private void BindBanci(string areaName)
        {
            ConfigModel.MED_COMMON_ITEMLISTDataTable dtBanci = null;
            if (!areaName.Equals("CRRT"))
            {
                dtBanci = configService.GetConfigList(string.Empty, string.Empty, "班次", "1");
            }
            else
            {
                dtBanci = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
                var row = dtBanci.NewMED_COMMON_ITEMLISTRow();
                row.ITEM_ID = Guid.NewGuid().ToString();
                row.ITEM_NAME = "白天";
                row.ITEM_VALUE = "1";
                dtBanci.AddMED_COMMON_ITEMLISTRow(row);

                row = dtBanci.NewMED_COMMON_ITEMLISTRow();
                row.ITEM_ID = Guid.NewGuid().ToString();
                row.ITEM_NAME = "小夜";
                row.ITEM_VALUE = "2";
                dtBanci.AddMED_COMMON_ITEMLISTRow(row);

                row = dtBanci.NewMED_COMMON_ITEMLISTRow();
                row.ITEM_ID = Guid.NewGuid().ToString();
                row.ITEM_NAME = "大夜";
                row.ITEM_VALUE = "3";
                dtBanci.AddMED_COMMON_ITEMLISTRow(row);
            }

            if (dtBanci != null && dtBanci.Rows.Count > 0)
            {
                DataRow row = dtBanci.NewRow();
                row["ITEM_NAME"] = "全部";
                row["ITEM_VALUE"] = "0";
                row["ITEM_ID"] = "c5540d95c-76a2-4af4-893a-13df547kj3s";
                row["ORDER_NUMBER"] = 0;
                dtBanci.Rows.InsertAt(row, 0);
            }

            string currentBanci = this.lupClass.EditValue != null ? this.lupClass.EditValue.ToString() : "1";
            Utility.BindLookUpEdit(this.lupClass, "ITEM_VALUE", "ITEM_NAME", dtBanci, "ITEM_NAME", "班次");
            this.lupClass.EditValue = areaName.Equals("CRRT") ? "1" : currentBanci;
        }

        /// <summary>
        /// 查询
        /// </summary>
        public void Query()
        {

            using (BackgroundWorker worker = new BackgroundWorker())
            {
                _relationData = new ConfigModel.MED_COMMON_RELATIONDataTable();
                worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                worker.RunWorkerAsync();
            }
            this.btnNew.Enabled = true;
        }

        string strPantientNew = string.Empty;
        DataTable dtPatientNew = new DataTable();
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string area = this.lupArea.Text == "全部" ? string.Empty : this.lupArea.EditValue.ToString();
            string banci = this.lupClass.Text == "全部" ? string.Empty : this.lupClass.EditValue.ToString();
            dtPatient = patientService.GetPatientListBySchedule(this.txtStartDate.DateTime, area, banci);
            dtPatient = Utility.GetSubTable(dtPatient, string.Empty, "AREA_VALUE,NAME ASC");
            _relationData = this.configService.GetCommRelation();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!dtPatient.Columns.Contains("STATUS_TAG"))
            {
                dtPatient.Columns.Add("STATUS_TAG", System.Type.GetType("System.Byte[]"));
            }
            dtPatient.AsEnumerable().ToList().ForEach(row =>
            {
                if (row["PURIFIER_MODEL_ID"].ToString().Length == 0)
                {
                    row["STATUS_TAG"] = Utility.BitmapToBytes(Resources.处方未确定);
                }
                else
                {
                    row["STATUS_TAG"] = Utility.BitmapToBytes(Resources.处方确定);
                }
                if (row["PURIFICATION_MODE_NAME"].ToString().Length == 0)
                {
                    row["STATUS_TAG"] = Utility.BitmapToBytes(Resources.处方未开);
                }
            });
            if (_currentPatientRow != null)
            {
                this.txtPatientName.Text = _currentPatientRow.NAME;
                HEMODIALYSIS_ID = _currentPatientRow.HEMODIALYSIS_ID;
                _currentPatientRow = null;
            }

            DataTable dtResult = dtPatient.Clone();
            dtPatient.AsEnumerable().CopyToDataTable(dtResult, LoadOption.PreserveChanges);
            if (!string.IsNullOrEmpty(this.txtPatientName.Text))
            {
                dtResult.Clear();
                dtPatient.AsEnumerable().Where(r => r["NAME"].ToString().Contains(txtPatientName.Text.Trim()) || PinYinConverter.GetPYString(r["NAME"].ToString()).ToUpper().Contains(txtPatientName.Text.Trim().ToUpper())).CopyToDataTable(dtResult, LoadOption.PreserveChanges);
            }
            int rowHandler = 0;
            if (!string.IsNullOrEmpty(HEMODIALYSIS_ID))
            {
                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    if (dtResult.Rows[i]["HEMODIALYSIS_ID"].ToString() == HEMODIALYSIS_ID)
                    {
                        rowHandler = i;
                    }
                }
            }

            this.gcPatientList.DataSource = dtResult;

            this.gvPatientList.FocusedRowHandle = rowHandler;

            this.gvPatientList.SelectRow(rowHandler);

            this.gvPatientList_RowClick(null, null);
        }

        string InfectionStr = string.Empty;
        string YIGAN = string.Empty;
        string BINGGAN = string.Empty;
        string MEIDU = string.Empty;
        string AIZIBING = string.Empty;
        private void worker2_DoWork(object sender, DoWorkEventArgs e)
        {
            DataRow row = e.Argument as DataRow;
            //dtAccess = hemoService.GetItemListByHemoIDandItemType(row["HEMODIALYSIS_ID"].ToString(), "血管通路");
            dtVasularAccess = hemoService.GetPatientVasular_AccessDt(row["HEMODIALYSIS_ID"].ToString());
            dtRecipe = hemoService.GetRecipeByHemodialysisIDAndDate(row["HEMODIALYSIS_ID"].ToString(), this.txtStartDate.DateTime);
            cureDt = (dtRecipe != null && dtRecipe.Rows.Count > 0) ? hemoService.GetMainCureByRecipeId(dtRecipe[0].RECIPE_ID) : null;
            dtInfectResent = _labService.GetSixMonthsCommonLabListByHemoID(row["HEMODIALYSIS_ID"].ToString());

            this.txtHemoID.Text = row["HEMODIALYSIS_ID"].ToString();
            this.txtRECIPE_ID.Text = (dtRecipe != null && dtRecipe.Rows.Count > 0) ? dtRecipe[0].RECIPE_ID : string.Empty;

            dtPatientNew = hemoService.GetPatientTypeIsNew(row["HEMODIALYSIS_ID"].ToString());
            if (dtPatientNew != null && dtPatientNew.Rows.Count > 0)
            {
                strPantientNew = dtPatientNew.Rows[0][0].ToString() == "1" ? "新入" : string.Empty;
            }
            strPantientNew += hemoService.GetCureTypeByHemoId(row["HEMODIALYSIS_ID"].ToString());

            dtLongRecipe = hemoService.GetLongRecipeByHemodialysisID(row["HEMODIALYSIS_ID"].ToString());
            LoadPastCureInfo(row["HEMODIALYSIS_ID"].ToString());

            e.Result = row;
        }

        private void worker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {

                DataRow row = e.Result as DataRow;

                gridControl1.DataSource = dtPastCureAndPressure;


                this.txtName.Text = row["NAME"].ToString();
                this.txtID.Text = row["PATIENT_ID"].ToString();
                this.txtCardNo.Text = row["CREDENTIALS_NUMBER"].ToString();
                lblINFECTIOUS_CHECK_RESULT.Text = row["INFECTIOUS_CHECK_RESULT"].ToString();
                BaseControlInfo.BindLookUpEdit(this.lupVASCULAR_ACCESS_ID, "VASCULAR_ACCESS_ID", "PATIENT_ID", dtVasularAccess, "PATIENT_ID", "血管通路");
                if (dtVasularAccess.Rows.Count > 0)
                    this.lupVASCULAR_ACCESS_ID.EditValue = dtVasularAccess[0].VASCULAR_ACCESS_ID;
                else
                    this.lupVASCULAR_ACCESS_ID.EditValue = null;
                //this.lupVASCULAR_ACCESS_ID.Properties.DropDownRows = dtAccess.Rows.Count == 0 ? 1 : dtAccess.Rows.Count;


                lblFrom.Text = strPantientNew;

                //检验提醒 
                lblLabTip.Text = string.Empty;
                if (dtInfectResent.Rows.Count > 0)
                {
                    InfectionStr = "【传染病】项目已超过六个月未监测";

                    foreach (DataRow vRow in dtInfectResent.Rows)
                    {
                        if (vRow["YIGAN"].ToString().Equals("是"))
                        {
                            YIGAN = string.Empty;
                        }
                        if (vRow["BINGGAN"].ToString().Equals("是"))
                        {
                            BINGGAN = string.Empty;
                        }
                        if (vRow["MEIDU"].ToString().Equals("是"))
                        {
                            MEIDU = string.Empty;
                        }
                        if (vRow["AIZIBING"].ToString().Equals("是"))
                        {
                            AIZIBING = string.Empty;
                        }
                    }
                    if (string.IsNullOrEmpty(YIGAN) && string.IsNullOrEmpty(BINGGAN) && string.IsNullOrEmpty(MEIDU) &&
                        string.IsNullOrEmpty(AIZIBING))
                    {
                        InfectionStr = "已超过六个月没有做【传染病】常规监测项目检验";
                    }
                    else
                    {
                        InfectionStr = string.Format("{0}:{1} {2} {3} {4}", InfectionStr, YIGAN, BINGGAN, MEIDU,
                            AIZIBING);
                    }
                }
                else
                {
                    InfectionStr = "已超过六个月没有做【传染病】常规监测项目检验";
                }

                lblLabTip.Text = "患者【" + txtName.Text + "】" + InfectionStr;

                if (dtRecipe != null && dtRecipe.Rows.Count > 0)
                {
                    this.groupControl1.Text = string.Format("患者{0}透析方案。", this.txtName.Text);
                    this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.Black;


                    this.spnDRY_WEIGHT.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["DRY_WEIGHT"].ToString());
                    this.spnTODAY_WEIGHT.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["TODAY_WEIGHT"].ToString());
                    this.spnUFR.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["UFR"].ToString());
                    var weightRemark = Utility.CDecimal(dtRecipe.Rows[0]["DRY_WEIGHT_REMARK"].ToString());

                    ResetDryWeight();

                    if (row["PURIFIER_MODEL_ID"].ToString().Length == 0)
                    {
                        dryWeight = this.spnDRY_WEIGHT.Value;
                        todayWeight = this.spnTODAY_WEIGHT.Value;
                        IsToComputeUFR = true;
                        if (dryWeight != 0 && todayWeight != 0)
                        {
                            this.spnUFR.EditValue = todayWeight - weightRemark - dryWeight;
                            if (todayWeight - weightRemark - dryWeight < 0)
                                this.spnUFR.Value = 0;
                        }
                    }
                    else
                    {
                        IsToComputeUFR = false;

                    }


                    //if (this.spnUFR.EditValue.ToString() == "0")
                    //{
                    //if (dryWeight != 0 && todayWeight != 0)
                    //{
                    //    this.spnUFR.EditValue = todayWeight - dryWeight;
                    //}
                    //}

                    //患者已经开始治疗，预计脱水禁止修改
                    //if (cureDt != null && cureDt.Rows.Count > 0) { this.spnUFR.ReadOnly = true; }
                    //else { this.spnUFR.ReadOnly = false; }

                    this.spnTODAY_BLOODA.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["TODAY_BLOODA"].ToString());
                    this.spnTODAY_BLOODB.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["TODAY_BLOODB"].ToString());
                    this.spnBLOODP.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["TODAY_BLOODP"].ToString());
                    this.lupVASCULAR_ACCESS_ID.EditValue = dtRecipe.Rows[0]["VASCULAR_ACCESS_ID"].ToString();
                    this.spnFREQUENCY_HOURS.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["FREQUENCY_HOURS"].ToString());
                    this.spnFREQUENCY_MINUTE.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["FREQUENCY_MINUTE"].ToString());

                    this.cmbPURIFICATION_MODE.EditValue = dtRecipe.Rows[0]["PURIFICATION_MODE"].ToString();
                    this.cmbFIRST_PURIFIER_MODEL.EditValue = dtRecipe.Rows[0]["FIRST_PURIFIER_MODEL"].ToString();
                    this.cmbFIRST_PURIFIER_NAME.EditValue = dtRecipe.Rows[0]["FIRST_PURIFIER_NAME"].ToString();
                    this.spnFIRST_PURIFIER_M2.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["FIRST_PURIFIER_M2"].ToString());
                    this.spnFIRST_PURIFIER_KOA.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["FIRST_PURIFIER_KOA"].ToString());
                    this.spnFIRST_PURIFIER_KUF.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["FIRST_PURIFIER_KUF"].ToString());
                    this.cmbSECOND_PURIFIER_MODEL.EditValue = dtRecipe.Rows[0]["SECOND_PURIFIER_MODEL"].ToString();
                    this.cmbSECOND_PURIFIER_NAME.EditValue = dtRecipe.Rows[0]["SECOND_PURIFIER_NAME"].ToString();
                    this.spnSECOND_PURIFIER_M2.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["SECOND_PURIFIER_M2"].ToString());
                    this.spnSECOND_PURIFIER_KOA.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["SECOND_PURIFIER_KOA"].ToString());
                    this.spnSECOND_PURIFIER_KUF.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["SECOND_PURIFIER_KUF"].ToString());
                    this.cmbTHERAPEUTIC_METHOD.EditValue = dtRecipe.Rows[0]["THERAPEUTIC_METHOD"].ToString();
                    //this.txtREMARK.Text = dtRecipe.Rows[0]["REMARK"].ToString();
                    this.txtFIRST_DRUG_DOSAGE.Text = dtRecipe.Rows[0]["FIRST_DRUG_DOSAGE"].ToString();
                    this.cmbFIRST_DRUG_UNIT.EditValue = dtRecipe.Rows[0]["FIRST_DRUG_UNIT"].ToString();
                    this.cmbFIRST_DRUG_MODE.EditValue = dtRecipe.Rows[0]["FIRST_DRUG_MODE"].ToString();
                    this.txtSECOND_DRUG_DOSAGE.Text = dtRecipe.Rows[0]["SECOND_DRUG_DOSAGE"].ToString();
                    this.cmbSECOND_DRUG_UNIT.EditValue = dtRecipe.Rows[0]["SECOND_DRUG_UNIT"].ToString();
                    this.cmbSECOND_DRUG_MODE.EditValue = dtRecipe.Rows[0]["SECOND_DRUG_MODE"].ToString();
                    this.spnDISPLACEMENT_FLOW.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["DISPLACEMENT_FLOW"].ToString());
                    this.lupDISPLACEMENT_MODE.EditValue = dtRecipe.Rows[0]["DISPLACEMENT_MODE"].ToString();
                    this.lupDISPLACEMENT_RECIPE.EditValue = dtRecipe.Rows[0]["DISPLACEMENT_RECIPE"].ToString();
                    this.spnBLOOW_FLOW.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["BLOOW_FLOW"].ToString());
                    this.spnUFR2.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["UFR2"].ToString());
                    this.txtDISPLACEMENT_SPECIAL_ADJUST.Text = dtRecipe.Rows[0]["DISPLACEMENT_SPECIAL_ADJUST"].ToString();
                    this.txtANTICOAGULANT_USE.Text = dtRecipe.Rows[0]["ANTICOAGULANT_USE"].ToString();
                    this.txtSPECIAL_MATTER.Text = dtRecipe.Rows[0]["SPECIAL_MATTER"].ToString();
                    this.txtDRY_WEIGHT_REMARK.Value = Utility.CDecimal(dtRecipe.Rows[0]["DRY_WEIGHT_REMARK"].ToString());
                    //是否卧床
                    this.checkBed.Checked = dtRecipe.Rows[0]["REMARK"].ToString().ToUpper() == "TRUE" ? true : false;

                    this.txtBR.Value = Utility.CDecimal((string.IsNullOrEmpty(dtRecipe.Rows[0]["BR"].ToString()) || dtRecipe.Rows[0]["BR"].ToString() == "0") ? "19" : dtRecipe.Rows[0]["BR"].ToString());
                    this.txtBEFORE_TEMPERATURE.Value = Utility.CDecimal((string.IsNullOrEmpty(dtRecipe.Rows[0]["BEFORE_TEMPERATURE"].ToString()) || dtRecipe.Rows[0]["BEFORE_TEMPERATURE"].ToString() == "0") ? "36.5" : dtRecipe.Rows[0]["BEFORE_TEMPERATURE"].ToString());



                    this.lupFOCUS_LEVEL.EditValue = string.IsNullOrEmpty(dtRecipe.Rows[0]["FOCUS_LEVEL"].ToString()) ? this.configService.GetConfigList(string.Empty, string.Empty, "病情", "1").First().ITEM_ID : dtRecipe.Rows[0]["FOCUS_LEVEL"].ToString();
                    this.lupSENSES.EditValue = string.IsNullOrEmpty(dtRecipe.Rows[0]["SENSES"].ToString()) ? this.configService.GetConfigList(string.Empty, string.Empty, "神志", "1").First().ITEM_ID : dtRecipe.Rows[0]["SENSES"].ToString();
                    this.lupALLERGIC.Text = string.IsNullOrEmpty(dtRecipe.Rows[0]["ALLERGIC"].ToString()) ? "无" : dtRecipe.Rows[0]["ALLERGIC"].ToString();


                    //if (this.lupVASCULAR_ACCESS_ID.Properties.DropDownRows > 1)
                    //{
                    //    this.lupVASCULAR_ACCESS_ID.EditValue = dtAccess[0].ITEM_ID;
                    //}

                    if (cureDt != null && cureDt.Rows.Count > 0)
                    {
                        this.spnAFTER_DRY_WEIGHT.EditValue = cureDt[0].AFTER_DRY_WEIGHT;
                        this.spnTODAY_WEIGHT.EditValue = cureDt[0].BEFORE_DRY_WEIGHT;
                        this.spnBLOODP.EditValue = Utility.CDecimal(cureDt[0].BEFORE_BP.ToString());
                    }

                    else
                        this.spnAFTER_DRY_WEIGHT.EditValue = 0;



                    //净化方式默认值
                    if (this.cmbPURIFICATION_MODE.EditValue == null || this.cmbPURIFICATION_MODE.EditValue == string.Empty)
                    {
                        this.cmbPURIFICATION_MODE.EditValue = "9c01f053-ad09-4873-b68f-b96d03b8572f";//HD
                    }

                    //净化器型号默认值
                    if (this.cmbFIRST_PURIFIER_MODEL.EditValue == null || this.cmbFIRST_PURIFIER_MODEL.EditValue == string.Empty)
                    {
                        if (this.cmbPURIFICATION_MODE.Text == "HD" || this.cmbPURIFICATION_MODE.Text == "HD+HP")
                        {
                            //this.cmbFIRST_PURIFIER_MODEL.EditValue = "90150299-e0a1-40b8-b9e8-82d504c88f21";//F6HPS
                        }
                        else if (this.cmbPURIFICATION_MODE.Text == "HDF")
                        {
                            this.cmbFIRST_PURIFIER_MODEL.EditValue = "59b5a10f-5a1f-4192-bc70-6be6b40efdb1";//FX80
                        }
                    }

                    //净化器型号2默认值
                    if (this.cmbSECOND_PURIFIER_MODEL.EditValue == null || this.cmbSECOND_PURIFIER_MODEL.EditValue == string.Empty)
                    {
                        if (this.cmbPURIFICATION_MODE.Text == "HD+HP")
                        {
                            this.cmbSECOND_PURIFIER_MODEL.EditValue = "61c16922-87a4-4176-8e6c-726b04baea7b";//HP130
                        }
                    }

                    //透析膜默认值
                    if (this.cmbFIRST_PURIFIER_NAME.EditValue == null || this.cmbFIRST_PURIFIER_NAME.EditValue == string.Empty)
                    {
                        if (this.cmbPURIFICATION_MODE.Text == "HD" || this.cmbPURIFICATION_MODE.Text == "HDF" || this.cmbPURIFICATION_MODE.Text == "HD+HP")
                        {
                            this.cmbFIRST_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";//聚砜膜
                        }
                    }

                    //透析膜2默认值
                    if (this.cmbSECOND_PURIFIER_NAME.EditValue == null || this.cmbSECOND_PURIFIER_NAME.EditValue == string.Empty)
                    {
                        if (this.cmbPURIFICATION_MODE.Text == "HD+HP")
                        {
                            this.cmbSECOND_PURIFIER_NAME.EditValue = "92f926c3-cb00-43cf-b4b5-7011597d51e7";//树脂
                        }
                    }

                    //面积默认值
                    if (this.spnFIRST_PURIFIER_M2.EditValue == null || this.spnFIRST_PURIFIER_M2.EditValue.ToString() == "0")
                    {
                        if (this.cmbPURIFICATION_MODE.Text == "HD" || this.cmbPURIFICATION_MODE.Text == "HD+HP")
                        {
                            this.spnFIRST_PURIFIER_M2.EditValue = 1.3;
                        }
                        else if (this.cmbPURIFICATION_MODE.Text == "HDF")
                        {
                            this.spnFIRST_PURIFIER_M2.EditValue = 1.8;
                        }
                    }
                }
                else
                {
                    this.groupControl1.Text = string.Format("患者{0}无透析方案，请先建立透析方案。", this.txtName.Text);
                    this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.Red;
                }
                busyIndicatorHelp.HideMessage();

            }
            catch
            {
                busyIndicatorHelp.HideMessage();

            }
        }

        private void worker3_DoWork(object sender, DoWorkEventArgs e)
        {
            DataRow row = e.Argument as DataRow;
            dtLongRecipe = hemoService.GetLongRecipeByHemodialysisID(row["HEMODIALYSIS_ID"].ToString());
            LoadPastCureInfo(row["HEMODIALYSIS_ID"].ToString());
        }

        private void worker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                gridControl1.DataSource = dtPastCureAndPressure;
            }
            catch
            {
                busyIndicatorHelp.HideMessage();
            }
            //if (dtPastCureAndPressure != null && dtPastCureAndPressure.Rows.Count > 0)
            //{
            //    gridControl1.DataSource = dtPastCureAndPressure;
            //    //ResetDryWeight();

            //}
            //else
            //{
            //    gridControl1.DataSource = dtPastCureAndPressure;
            //}
        }

        /// <summary>
        /// 加载历次治疗单信息
        /// </summary>
        private void LoadPastCureInfo(string hemoId)
        {
            //dtPastCure = hemoService.GetPastCureInfoByHemoId(hemoId);
            //dtPastPressure = hemoService.GetPastPressureByHemoId(hemoId);
            dtPastCureAndPressure = hemoService.GetPatientCureAndPastPressureByHemoId(hemoId);
        }

        /// <summary>
        /// 确认、取消确认处方
        /// </summary>
        /// <param name="pStatus"></param>
        /// <param name="currentPatient"></param>
        private void ConfirmOrCancelOnePatient(bool pStatus, DataRow currentPatient)
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
            strResult = CheckArea(this.lupArea.Text, strInf).Trim();
            if (strResult != string.Empty)
            {
                if (XtraMessageBox.Show(strResult, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            string HemoID = string.Empty;

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
                PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable dataTableSchedule = scheduleService.GetPatientScheduleSignle(this.txtStartDate.DateTime, currentPatient["HEMODIALYSIS_ID"].ToString());
                if (dataTableSchedule != null && dataTableSchedule.Rows.Count > 0)
                {
                    PatientScheduleModel.MED_PATIENT_SCHEDULERow drSchedule = dataTableSchedule.Rows[0] as PatientScheduleModel.MED_PATIENT_SCHEDULERow;
                    HemodialysisModel.MED_HEMO_RECIPEDataTable recipeTable = hemoService.GetRecipeByHemodialysisIDAndDate(currentPatient["HEMODIALYSIS_ID"].ToString(), this.txtStartDate.DateTime);
                    string strRecipeIDList = string.Empty;
                    string pName = drSchedule.PATIENTNAME;
                    HemoID = drSchedule.HEMODIALYSIS_ID;

                    if (recipeTable != null && recipeTable.Rows.Count > 0)
                    {
                        #region 判断预计脱水、透前体重、诊断是否录入，没有录入强制用户录入否则不能确认处方,达到质控要求。

                        if (diagnose != null && diagnose.Rows.Count > 0)
                        {
                            if (drSchedule["DIAGNOSE"].ToString().Length == 0)
                            {
                                AutoClosedMsgBox.ShowForm("患者【" + pName + "】的诊断未录入，不能确定透析处方。", this.Text, 2000, MessageBoxIcon.Information);
                                return;
                            }
                        }

                        if (dtQualityCheck != null && dtQualityCheck.Rows.Count > 0)
                        {
                            if (todayWeight != null && todayWeight.Rows.Count > 0)
                            {
                                if (recipeTable.Rows[0]["TODAY_WEIGHT"].ToString().Length == 0 || recipeTable.Rows[0]["TODAY_WEIGHT"].ToString() == "0")
                                {
                                    AutoClosedMsgBox.ShowForm("患者【" + pName + "】的透前体重未测量，不能确定透析处方。", this.Text, 2000, MessageBoxIcon.Information);
                                    return;
                                }
                            }

                            if (todayUfR != null && todayUfR.Rows.Count > 0)
                            {
                                if (recipeTable.Rows[0]["UFR"].ToString().Length == 0 || recipeTable.Rows[0]["UFR"].ToString() == "0")
                                {
                                    AutoClosedMsgBox.ShowForm("患者【" + pName + "】的当日预计脱水量未确定，不能确定透析处方。", this.Text, 2000, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }

                        #endregion

                        HemoDWHApplication hemodwApp = new HemoDWHApplication();
                        strRecipeIDList = "'" + recipeTable.Rows[0]["RECIPE_ID"].ToString() + "'";
                        if (pStatus)
                        {
                            drSchedule.RECIPE_ID = recipeTable.Rows[0]["RECIPE_ID"].ToString();
                            drSchedule.PURIFIER_MODEL_ID = recipeTable.Rows[0]["FIRST_PURIFIER_MODEL"].ToString();
                            drSchedule.PURIFICATION_MODE = recipeTable.Rows[0]["PURIFICATION_MODE"].ToString();

                            //进行出库申请
                            hemodwApp.ConfirmHemoDWApply(HemoID, recipeTable.Rows[0]["RECIPE_ID"].ToString(), recipeTable.Rows[0]["PURIFICATION_MODE"].ToString());
                        }
                        else
                        {
                            drSchedule.RECIPE_ID = string.Empty;
                            drSchedule.PURIFIER_MODEL_ID = string.Empty;
                            hemodwApp.CancleConfirHemoDwApply(recipeTable.Rows[0]["RECIPE_ID"].ToString());
                        }
                    }
                    else
                    {
                        AutoClosedMsgBox.ShowForm("患者透析ID:" + currentPatient["HEMODIALYSIS_ID"].ToString() + "的临时处方尚未创建,请先创建临时处方。", "提示", 2000, MessageBoxIcon.Information);
                        return;
                    }

                    //根据处方编号取透析单号，判断患者该处方是否已经执行，如果已经执行不能取消。
                    HemodialysisModel.MED_CURE_MAINDataTable dtCure = hemoService.GetMainCureByRecipeId(recipeTable.Rows[0]["RECIPE_ID"].ToString());
                    if (dtCure != null && dtCure.Rows.Count > 0 && strStautsName == "取消")
                    {
                        AutoClosedMsgBox.ShowForm("该患者处方已执行不能取消!", "提示", 2000, MessageBoxIcon.Information);
                        return;
                    }
                    //更新临时医嘱记录处方编号
                    HemodialysisModel.MED_CURE_DRUGDataTable drugDt = hemoService.GetValidCureDrugByHemoID(currentPatient["HEMODIALYSIS_ID"].ToString(), this.txtStartDate.DateTime);
                    if (drugDt != null && drugDt.Rows.Count > 0)
                    {
                        drugDt.AsEnumerable().ToList().ForEach(drug =>
                        {
                            drug.RECIPE_ID = string.IsNullOrEmpty(drug.RECIPE_ID) ? strRecipeIDList.Replace("'", "") : drug.RECIPE_ID;
                        });
                        hemoService.SaveCureDrug(drugDt);
                    }
                    result = scheduleService.SavePatientScheduleInfo(dataTableSchedule);
                    result = hemoService.SaveRecipeUserIDByRecipeIDList(strRecipeIDList, HemoApplicationContext.Current.CurrentUser.EMP_NO);
                    if (result > 0)
                    {
                        AutoClosedMsgBox.ShowForm("该患者处方已" + strStautsName + "。", "提示", 2000, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                AutoClosedMsgBox.ShowForm("请先选择一个患者！", "提示", 2000, MessageBoxIcon.Information);
            }
        }

        private void ChangeValue(SpinEdit pEdit)
        {
            if (IsToComputeUFR)
            {
                CheckNumberValue(pEdit);
                dryWeight = Utility.CDecimal(this.spnDRY_WEIGHT.EditValue != null ? this.spnDRY_WEIGHT.EditValue.ToString() : "0");
                todayWeight = Utility.CDecimal(spnTODAY_WEIGHT.EditValue != null ? spnTODAY_WEIGHT.EditValue.ToString() : "0");

                var weightRemark = Utility.CDecimal(txtDRY_WEIGHT_REMARK.EditValue != null ? txtDRY_WEIGHT_REMARK.EditValue.ToString() : "0");
                this.spnUFR.EditValue = (dryWeight != 0 && todayWeight != 0) ? todayWeight - weightRemark - dryWeight : 0;
                if (todayWeight - weightRemark - dryWeight < 0)
                    this.spnUFR.Value = 0;
            }
        }

        /// <summary>
        /// 过滤符号
        /// </summary>
        /// <param name="pEdit"></param>
        private void CheckNumberValue(SpinEdit pEdit)
        {
            if (pEdit.Text.IndexOf("-") > -1)
            {
                pEdit.Text = pEdit.Text.Replace("-", "");
            }
        }

        private bool IsDataValid()
        {
            bool result = true;
            //result = BaseControlInfo.CheckSpinEdit(this.spnUFR, "请录入预计脱水！", this.Text);
            //if (result == false)
            //{
            //    return result;
            //}
            result = BaseControlInfo.CheckpLookUpEdit(this.cmbPURIFICATION_MODE, "请选择净化方式！", this.Text);
            if (result == false)
            {
                return result;
            }
            result = BaseControlInfo.CheckpLookUpEdit(this.cmbFIRST_PURIFIER_MODEL, "请选择净化器型号！", this.Text);
            if (result == false)
            {
                return result;
            }
            return result;
        }

        private string CheckArea(string strRoom, string strInf)
        {
            var rooms = configService.GetConfigList(string.Empty, string.Empty, "隔离病区", "1");
            if (rooms.Count(wh => wh.ITEM_VALUE == strRoom) > 0)
            {
                if (strInf == "全阴" || string.IsNullOrEmpty(strInf))
                {
                    return "该患者没有标记为【乙肝或丙肝】,是否确认在隔离区域治疗？";
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 重新设置干体重
        /// </summary>
        private void ResetDryWeight()
        {
            try
            {
                if (dtPastCureAndPressure != null)
                    lock (dtPastCureAndPressure)
                    {
                        //干体重若为0，默认取上次的值
                        if (this.spnDRY_WEIGHT.EditValue.ToString() == "0")
                        {
                            if (dtPastCureAndPressure != null && dtPastCureAndPressure.Rows.Count > 0)
                            {
                                if (dtPastCureAndPressure.Rows[0]["DRY_WEIGHT"] != DBNull.Value)
                                {
                                    this.spnDRY_WEIGHT.EditValue = dtPastCureAndPressure.Rows[0]["DRY_WEIGHT"];
                                }
                            }
                        }
                    }

                //干体重若依然为0，默认取长期处方的值
                if (this.spnDRY_WEIGHT.EditValue.ToString() == "0")
                {
                    if (dtLongRecipe != null && dtLongRecipe.Rows.Count > 0)
                    {
                        if (dtLongRecipe.Rows[0]["DRY_WEIGHT"] != DBNull.Value)
                        {
                            this.spnDRY_WEIGHT.EditValue = dtLongRecipe.Rows[0]["DRY_WEIGHT"];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 新建医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            this.errorProvider.ClearErrors();
            btnSaveDrug.Enabled = true;

            this.btnNew.Enabled = false;
            ClearDrugItems();
            cureDrugDt = new HemodialysisModel.MED_CURE_DRUGDataTable();
            var row = cureDrugDt.NewMED_CURE_DRUGRow();
            row.CURE_DRUG_ID = Guid.NewGuid().ToString();
            cureDrugDt.AddMED_CURE_DRUGRow(row);
            Utilities.BaseControlInfo.SetControlEnabled(panDrug, true);

            this.btnEdit.Enabled = false;

        }
        /// <summary>
        /// 编辑医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.errorProvider.ClearErrors();

            this.btnSaveDrug.Enabled = true;
            this.btnEdit.Enabled = false;
            this.btnNew.Enabled = true;
            Utilities.BaseControlInfo.SetControlEnabled(panDrug, true);

        }
        private void ClearDrugItems()
        {
            this.txtDRUG_NAME.EditValue = string.Empty;
            this.txtDRUG_NAME.Text = string.Empty;
            this.txtDOSAGE.Text = string.Empty;
            this.txtDOSAGE.EditValue = string.Empty;
            this.cmbDOSAGE_UNITS.EditValue = string.Empty;
            this.cmbDOSAGE_UNITS.Text = string.Empty;
            this.cmbDRUG_MODE.EditValue = string.Empty;
            this.cmbDRUG_MODE.Text = string.Empty;
            this.lupDRUG_TIMETYPE.Text = string.Empty;
            this.memoEditDrug.Text = string.Empty;
            this.txtCREATE_DATE.DateTime = this.txtStartDate.DateTime.Date;
            this.txtCREATE_DATE.Enabled = false;
            this.cmbCreate_Time.EditValue = DateTime.Now.ToShortTimeString();
        }
        /// <summary>
        /// 加载用药信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPageDrug)
            {
                this.errorProvider.ClearErrors();
                IsOverOrder = false;
                overOrderCureId = string.Empty;
                this.btnSaveDrug.Enabled = false;
                var row = this.gvPatientList.GetFocusedDataRow();
                if (row != null)
                {
                    InzationDrugDt(row);

                }
            }
        }
        private void InzationDrugDt(DataRow dr)
        {
            drugDt = new HemodialysisModel.MED_CURE_DRUGDataTable();
            var drugDtTemp = new HemodialysisModel.MED_CURE_DRUGDataTable();

            using (var _worker = new BackgroundWorker())
            {
                _worker.DoWork += (s1, e1) =>
               {
                   drugDtTemp = !string.IsNullOrEmpty(this.txtRECIPE_ID.Text) ? objHemodialysisService.GetCureDrugByHemoIDAndRecipeId(this.txtHemoID.Text.Trim(), this.txtRECIPE_ID.Text) : objHemodialysisService.GetValidCureDrugByHemoID(this.txtHemoID.Text.Trim(), this.txtStartDate.DateTime);
                   if (drugDtTemp == null || drugDtTemp.Rows.Count <= 0)
                   {
                       drugDtTemp = objHemodialysisService.GetValidCureDrugByHemoID(this.txtHemoID.Text.Trim(), this.txtStartDate.DateTime);
                   }
                   if (IsOverOrder)
                   {
                       drugDtTemp.Where(i => !i.IsEXECUTE_STATUSNull() && i.EXECUTE_STATUS == "1").CopyToDataTable<HemodialysisModel.MED_CURE_DRUGRow>(drugDt, LoadOption.PreserveChanges);
                   }
                   else
                   {
                       drugDtTemp.Where(i => i.IsEXECUTE_STATUSNull() || i.EXECUTE_STATUS != "1").CopyToDataTable<HemodialysisModel.MED_CURE_DRUGRow>(drugDt, LoadOption.PreserveChanges);
                   }

               };
                _worker.RunWorkerCompleted += (s2, e2) =>
                {
                    gridDrugList.DataSource = drugDt;
                    ClearDrugItems();
                    Utilities.BaseControlInfo.SetControlEnabled(panDrug, false);
                    this.btnSaveDrug.Enabled = false;


                };
                _worker.RunWorkerAsync();

            }
        }
        /// <summary>
        /// 保存用药信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveDrug_Click(object sender, EventArgs e)
        {
            SaveDrugMethond();

        }

        private void SaveDrugMethond()
        {
            var rowr = this.gvPatientList.GetFocusedDataRow();
            if (rowr != null)
            {
                if (dtRecipe != null && dtRecipe.Rows.Count > 0)
                {

                    if (cureDrugDt != null && cureDrugDt.Rows.Count > 0)
                    {
                        if (!DrugValidate()) return;
                        cureDrugDt[0].RECIPE_ID = this.txtRECIPE_ID.Text;
                        cureDrugDt[0].DRUG_CODE = this.txtDRUG_NAME.EditValue.ToString();
                        cureDrugDt[0].DRUG_NAME = this.txtDRUG_NAME.Text.ToString();
                        cureDrugDt[0].CREATE_DATE = Utility.CDate(txtCREATE_DATE.Text + " " + cmbCreate_Time.Text);// this.txtStartDate.DateTime.Date;
                        cureDrugDt[0].HEMODIALYSIS_ID = this.txtHemoID.Text;
                        cureDrugDt[0].PATIENT_ID = txtID.Text.Trim();
                        cureDrugDt[0].COM_NO = objHemodialysisService.GetOrderComNo();
                        cureDrugDt[0].COM_SUB_NO = "1";
                        cureDrugDt[0].STATUS = "0";//临时用药新开立状态
                        cureDrugDt[0].EXECUTE_STATUS = IsOverOrder ? "1" : "0";
                        cureDrugDt[0].DOSAGE = this.txtDOSAGE.Text;
                        cureDrugDt[0].DOSAGE_UNITS = this.cmbDOSAGE_UNITS.EditValue.ToString();
                        cureDrugDt[0].DOCTOR_ID = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                        cureDrugDt[0].ADMINISTRATION = this.cmbDRUG_MODE.Text.ToString();
                        cureDrugDt[0].DRUG_MODE = this.cmbDRUG_MODE.EditValue.ToString();
                        cureDrugDt[0].DRUG_TIMETYPE = this.lupDRUG_TIMETYPE.Text.ToString();
                        cureDrugDt[0].REMARK = this.memoEditDrug.Text.ToString();
                        if (IsOverOrder)
                        {
                            cureDrugDt[0].CURE_ID = overOrderCureId;
                        }


                        var result = objHemodialysisService.SaveCureDrug((HemodialysisModel.MED_CURE_DRUGDataTable)cureDrugDt);
                        if (result > 0)
                        {
                            cureDrugDt = null;
                            XtraMessageBox.Show("信息保存成功.", "快捷处方信息");
                        }
                    }
                    else
                    {
                        var drow = gridView5.GetFocusedDataRow() as HemodialysisModel.MED_CURE_DRUGRow;
                        if (drow == null) return;
                        if (!DrugValidate()) return;
                        var dr = drugDt.FindByCURE_DRUG_ID(drow.CURE_DRUG_ID);
                        dr.DRUG_CODE = this.txtDRUG_NAME.EditValue.ToString();
                        dr.DRUG_NAME = this.txtDRUG_NAME.Text.ToString();
                        dr.CREATE_DATE = Utility.CDate(txtCREATE_DATE.Text + " " + cmbCreate_Time.Text);// this.txtStartDate.DateTime.Date;
                        dr.DOSAGE = this.txtDOSAGE.Text;
                        dr.DOCTOR_ID = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                        dr.DOSAGE_UNITS = this.cmbDOSAGE_UNITS.EditValue.ToString();
                        dr.ADMINISTRATION = this.cmbDRUG_MODE.Text.ToString();
                        dr.DRUG_MODE = this.cmbDRUG_MODE.EditValue.ToString();
                        dr.DRUG_TIMETYPE = this.lupDRUG_TIMETYPE.Text.ToString();
                        dr.REMARK = this.memoEditDrug.Text.ToString();
                        var result = objHemodialysisService.SaveCureDrug((HemodialysisModel.MED_CURE_DRUGDataTable)drugDt);
                        if (result > 0)
                        {
                            cureDrugDt = null;
                            XtraMessageBox.Show("信息保存成功.", "快捷处方信息");
                        }
                    }
                    this.btnSaveDrug.Enabled = false;
                    var row = this.gvPatientList.GetFocusedDataRow();
                    if (row != null)
                    {
                        InzationDrugDt(row);
                        Utilities.BaseControlInfo.SetControlEnabled(panDrug, false);
                        btnNew.Enabled = true;
                    }
                }
                else
                {
                    AutoClosedMsgBox.ShowForm("患者临时处方尚未创建，请先创建临时处方。", "提示", 1000, MessageBoxIcon.Information);
                }
            }
        }
        private bool DrugValidate()
        {
            this.errorProvider.ClearErrors();
            bool result = true;
            if (string.IsNullOrEmpty(this.txtCREATE_DATE.DateTime.ToString()))
            {
                errorProvider.SetError(txtCREATE_DATE, "请输入日期");
                result = false;
            }
            if (string.IsNullOrEmpty(cmbCreate_Time.EditValue.ToString()))
            {
                errorProvider.SetError(cmbCreate_Time, "请输入时间");
                result = false;
            }
            if (string.IsNullOrEmpty(txtDOSAGE.EditValue.ToString()))
            {
                errorProvider.SetError(txtDOSAGE, "请输入剂量");
                result = false;
            }
            if (string.IsNullOrEmpty(cmbDOSAGE_UNITS.EditValue.ToString()))
            {
                errorProvider.SetError(cmbDOSAGE_UNITS, "请输入单位");
                result = false;
            }
            //if (string.IsNullOrEmpty(cmbDRUG_MODE.EditValue.ToString()))
            //{
            //    errorProvider.SetError(cmbDRUG_MODE, "请输入用法");
            //    result = false;
            //}
            return result;
        }

        #endregion

        private void gridView5_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var dr = gridView5.GetFocusedDataRow() as HemodialysisModel.MED_CURE_DRUGRow;
            if (dr != null && (dr.STATUS == "0" || dr.STATUS == "2"))
            {
                HemodialysisModel.MED_CURE_DRUGDataTable tmpCrue = new HemodialysisModel.MED_CURE_DRUGDataTable();
                tmpCrue.Rows.Add(dr.ItemArray);
                BaseControlInfo.SetControlDataByDataTable(tmpCrue, panDrug);
                txtDRUG_NAME.EditValue = dr.IsDRUG_CODENull() ? "" : dr.DRUG_CODE.Trim();
                txtDOSAGE.Text = dr.IsDOSAGENull() ? string.Empty : dr.DOSAGE;
                cmbDOSAGE_UNITS.EditValue = dr.IsDOSAGE_UNITSNull() ? string.Empty : dr.DOSAGE_UNITS;
                cmbDRUG_MODE.EditValue = dr.IsDRUG_MODENull() ? string.Empty : dr.DRUG_MODE;
                memoEditDrug.Text = dr.IsREMARKNull() ? string.Empty : dr.REMARK;
                tmpCrue = null;
                this.btnSaveDrug.Enabled = true;
                Utilities.BaseControlInfo.SetControlEnabled(panDrug, false);

            }
            else
            {
                HemodialysisModel.MED_CURE_DRUGDataTable tmpCrue = new HemodialysisModel.MED_CURE_DRUGDataTable();
                tmpCrue.Rows.Add(dr.ItemArray);
                BaseControlInfo.SetControlDataByDataTable(tmpCrue, panDrug);
                txtDRUG_NAME.EditValue = dr.IsDRUG_CODENull() ? "" : dr.DRUG_CODE.Trim();
                txtDOSAGE.Text = dr.IsDOSAGENull() ? string.Empty : dr.DOSAGE;
                cmbDOSAGE_UNITS.EditValue = dr.IsDOSAGE_UNITSNull() ? string.Empty : dr.DOSAGE_UNITS;
                cmbDRUG_MODE.EditValue = dr.IsDRUG_MODENull() ? string.Empty : dr.DRUG_MODE;
                memoEditDrug.Text = dr.IsREMARKNull() ? string.Empty : dr.REMARK;

                this.btnSaveDrug.Enabled = false;
                Utilities.BaseControlInfo.SetControlEnabled(panDrug, false);
            }
            btnNew.Enabled = true;
            btnEdit.Enabled = true;
        }

        private void txtDRUG_NAME_EditValueChanged(object sender, EventArgs e)
        {

            var _relationshift = new ConfigModel.MED_COMMON_RELATIONDataTable();
            _relationData.Where(i => i.RELATIONTYPE == "0").CopyToDataTable<ConfigModel.MED_COMMON_RELATIONRow>(_relationshift, LoadOption.PreserveChanges);
            if (txtDRUG_NAME.EditValue != null)
            {
                var row = _relationshift.FirstOrDefault(i => i.ITEMNAME.Trim() == this.txtDRUG_NAME.EditValue.ToString().Trim());

                if (row != null)
                {
                    txtDOSAGE.Text = row.DOSAGE;
                    cmbDOSAGE_UNITS.EditValue = row.UNIT;
                    cmbDRUG_MODE.EditValue = row.DRUGMODE;
                }
                else
                {
                    txtDOSAGE.Text = string.Empty;
                    cmbDOSAGE_UNITS.EditValue = string.Empty;
                    cmbDRUG_MODE.EditValue = string.Empty;
                }
            }
        }

        private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            var dr = gridView5.GetFocusedDataRow() as HemodialysisModel.MED_CURE_DRUGRow;
            if (XtraMessageBox.Show("是否确认删除当前临时医嘱？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            objHemodialysisService.DeleteCureORLongDrugByID("short", dr.CURE_DRUG_ID);

            var row = this.gvPatientList.GetFocusedDataRow();
            if (row != null)
            {
                InzationDrugDt(row);

            }
        }

        private void ToolStripMenuItemForCom_Click(object sender, EventArgs e)
        {
            int result = -1;

            string COM_NOS = string.Empty;
            string COM_SUB_NOS = string.Empty;
            string drugmode = string.Empty;
            var rows = gridView5.GetSelectedRows();
            for (int i = 0; i < rows.Length; i++)
            {
                var row = drugDt.Rows[rows[i]] as HemodialysisModel.MED_CURE_DRUGRow;
                if (row.IsDRUG_MODENull())
                {
                    MessageBox.Show("组合医嘱用法不能为空！");
                    return;
                }
                if (string.IsNullOrEmpty(drugmode) && !string.IsNullOrEmpty(row.DRUG_MODE))
                {
                    drugmode = row.DRUG_MODE;
                }
                if (COM_NOS.Length <= 0)
                {
                    COM_NOS = row.COM_NO;
                    COM_SUB_NOS = row.COM_SUB_NO;
                }
                row.COM_NO = COM_NOS;
                row.COM_SUB_NO = Convert.ToString(int.Parse(COM_SUB_NOS) + i);
            }
            for (int i = 0; i < rows.Length; i++)
            {
                var row = drugDt.Rows[rows[i]] as HemodialysisModel.MED_CURE_DRUGRow;
                row.DRUG_MODE = drugmode;
            }
            result = objHemodialysisService.SaveCureDrug(drugDt);




            if (result >= 1)
            {
                XtraMessageBox.Show("药品信息保存成功.", "用药信息");
            }
            else
            {
                XtraMessageBox.Show("药品信息保存失败.", "用药信息");
            }
            var row1 = this.gvPatientList.GetFocusedDataRow();
            if (row1 != null)
            {
                InzationDrugDt(row1);

            }
        }

        private void ToolStripMenuItemForComCancle_Click(object sender, EventArgs e)
        {
            int result = -1;


            var rows = gridView5.GetSelectedRows();
            for (int i = 0; i < rows.Length; i++)
            {
                var row = drugDt.Rows[rows[i]] as HemodialysisModel.MED_CURE_DRUGRow;

                row.COM_NO = objHemodialysisService.GetOrderComNo();
                row.COM_SUB_NO = "1";
            }
            result = objHemodialysisService.SaveCureDrug(drugDt);


            if (result >= 1)
            {
                XtraMessageBox.Show("药品信息保存成功.", "用药信息");
            }
            else
            {
                XtraMessageBox.Show("药品信息保存失败.", "用药信息");
            }
            var row1 = this.gvPatientList.GetFocusedDataRow();
            if (row1 != null)
            {
                InzationDrugDt(row1);

            }
        }

        private void gridDrugList_MouseDown(object sender, MouseEventArgs e)
        {
            var rows = gridView5.GetSelectedRows();
            if (rows.Length > 1)
            {
                bool isShow = true;
                for (int i = 0; i < rows.Length; i++)
                {
                    if (gridView5.GetRowCellDisplayText(int.Parse(rows[i].ToString()), COM_gridColumn).Length > 0)
                    {
                        isShow = false;
                        break;
                    }
                }

                if (isShow && e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    this.contextMenuStrip2.Show(MousePosition);
                    this.ToolStripMenuItemDelete.Visible = false;
                    this.ToolStripMenuItemForCom.Visible = true;
                    this.ToolStripMenuItemForComCancle.Visible = false;
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    this.contextMenuStrip2.Show(MousePosition);
                    this.ToolStripMenuItemDelete.Visible = false;
                    this.ToolStripMenuItemForCom.Visible = false;
                    this.ToolStripMenuItemForComCancle.Visible = true;
                }
                else
                {
                    this.contextMenuStrip2.Hide();
                }

            }
            else
            {

                var dr = gridView5.GetFocusedDataRow() as HemodialysisModel.MED_CURE_DRUGRow;
                if (dr == null)
                {
                    this.contextMenuStrip2.Hide();
                    return;
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Right && (dr.STATUS == "2"))
                {
                    this.contextMenuStrip2.Show(MousePosition);
                    this.ToolStripMenuItemDelete.Visible = false;
                    this.ToolStripMenuItemForCom.Visible = false;
                    this.ToolStripMenuItemForComCancle.Visible = false;

                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Right && (dr.STATUS == "0"))
                {
                    this.contextMenuStrip2.Show(MousePosition);
                    this.ToolStripMenuItemDelete.Visible = true;
                    this.ToolStripMenuItemForCom.Visible = false;
                    this.ToolStripMenuItemForComCancle.Visible = false;
                }
                else
                {
                    this.contextMenuStrip2.Hide();
                }
            }
        }

        private void txtDRY_WEIGHT_REMARK_EditValueChanged(object sender, EventArgs e)
        {
            ChangeValue(txtDRY_WEIGHT_REMARK);
        }

        private void checkBed_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBed.Checked)
            {
                spnTODAY_WEIGHT.Enabled = false;
                spnTODAY_WEIGHT.Value = 0;
            }
            else
            {
                spnTODAY_WEIGHT.Enabled = true;

            }

        }

        private void btn_UseTemplate_Click(object sender, EventArgs e)
        {
            CureDrugTemplateList frm = new CureDrugTemplateList();
            frm.CurrentHemoID = this.txtHemoID.Text.ToString();
            frm.CurrentPatientID = this.txtID.Text.ToString();
            frm.RecipeId = this.txtRECIPE_ID.Text.ToString();
            frm.IsLong = false;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var row = this.gvPatientList.GetFocusedDataRow();
                if (row != null)
                {
                    InzationDrugDt(row);

                }
            }
        }

        private void dxSimpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void dxSimpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void xtraScrollableControl1_Click(object sender, EventArgs e)
        {

        }

        private void dxSimpleButton3_Click(object sender, EventArgs e)
        {
            QueryDrugExectFrm frm = new QueryDrugExectFrm();
            frm.banchiId = this.lupClass.EditValue.ToString();
            frm.roomId = this.lupArea.EditValue.ToString();
            frm.currentDt = Utility.CDate(txtStartDate.DateTime.ToString()).Date;
            frm.ShowDialog();
        }

        private void btnMaintain_Click(object sender, EventArgs e)
        {
            EditDrugMaster frm = new EditDrugMaster(true, string.Empty);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.Yes)
            {
                txtDRUG_NAME.Properties.DataSource = objDrug.GetDrugMasterList();
                txtDRUG_NAME.Properties.PopupFormSize = new Size(400, 230);
                txtDRUG_NAME.Properties.DisplayMember = "DRUG_NAME";//要显示的字段,Text获得
                txtDRUG_NAME.Properties.ValueMember = "DRUG_CODE";//实际值的字段,EditValue获得 // DeptID
            }
        }

        private void gridView5_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            var rowCurrent = gridView5.GetDataRow(e.ListSourceRowIndex) as HemodialysisModel.MED_CURE_DRUGRow;
            if (rowCurrent == null)
            {
                return;
            }

            if (e.Column == COM_gridColumn)
            {
                var exitrows = drugDt.Count(wh => wh.COM_NO == rowCurrent.COM_NO);
                var smalCount = drugDt.Count(wh => wh.COM_NO == rowCurrent.COM_NO && Convert.ToInt32(wh.COM_SUB_NO) < Convert.ToInt32(rowCurrent.COM_SUB_NO));
                var bigCount = drugDt.Count(wh => wh.COM_NO == rowCurrent.COM_NO && Convert.ToInt32(wh.COM_SUB_NO) > Convert.ToInt32(rowCurrent.COM_SUB_NO));
                if (exitrows == 1)
                {
                    e.DisplayText = "";
                    return;
                }

                if (smalCount == 0)
                {
                    e.DisplayText = "┏";
                    return;
                }
                if (bigCount == 0)
                {
                    e.DisplayText = "┗";
                    return;
                }
                e.DisplayText = "┃";
                return;

            }
        }
        /// <summary>
        /// 患者加透医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOverOrder_Click(object sender, EventArgs e)
        {
            //根据透析号去获取治疗单相关信息。得到 加透治疗单相关信息
            var _cureMain = _hemodialysisService.GetMainCureByRecipeId(this.txtRECIPE_ID.Text);
            if (_cureMain.Count > 1)
            {
                //获取加透治疗单
                var _cureMainRow = _cureMain.FirstOrDefault(i => !i.IsRECIPE_TYPENull() && i.RECIPE_TYPE == "1");
                overOrderCureId = _cureMainRow.CURE_ID;
                IsOverOrder = true;
                var row = this.gvPatientList.GetFocusedDataRow();
                if (row != null)
                {
                    InzationDrugDt(row);
                }
            }
            else
            {
                overOrderCureId = string.Empty;
                IsOverOrder = false;
                MessageBox.Show("患者无加透数据，无法开立加透医嘱！");
            }
            SetTabPageText();

        }

        private void btnCustom_Click(object sender, EventArgs e)
        {
            overOrderCureId = string.Empty;
            IsOverOrder = false;
            var row = this.gvPatientList.GetFocusedDataRow();
            if (row != null)
            {
                InzationDrugDt(row);
            }
            SetTabPageText();
        }
        private void SetTabPageText()
        {
            if (IsOverOrder)
            {
                this.xtraTabPageDrug.Text = "加透医嘱";
                this.xtraTabPageDrug.Appearance.Header.ForeColor = Color.Red;
                this.btn_UseTemplate.Visible = false;
            }
            else
            {
                this.xtraTabPageDrug.Text = "普通医嘱";
                this.xtraTabPageDrug.Appearance.Header.ForeColor = Color.Black;
                this.btn_UseTemplate.Visible = true;


            }
        }





    }
}