/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:列表
 * 创建标识:贺建操-2013年7月16日
 * 
 * 修改时间:2013年10月24日
 * 修改人:顾伟伟
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年2月1日
 * 修改人:吕志强
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年5月12日
 * 修改人:刘超
 * 修改描述:新增方法SQL
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

namespace Hemo.Client.UI.Hemodialysis {
    public partial class FastRecipeList : HemoBaseFrm {
        #region 类变量

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        private IPatient patientService = ServiceManager.Instance.PatientService;

        private IPatientSchedule scheduleService = ServiceManager.Instance.PatientSchedule;

        private IConfig configService = ServiceManager.Instance.ConfigService;

        private DataTable dtPatient = null;

        private HemodialysisModel.MED_HEMO_RECIPEDataTable dtRecipe = null;

        private HemodialysisModel.MED_HEMO_RECIPEDataTable dtLongRecipe = null;

        private HemodialysisModel.MED_CURE_MAINDataTable dtCure = null;

        private HemodialysisModel.MED_CURE_MAINDataTable cureDt = null;

        private ConfigModel.MED_COMMON_ITEMLISTDataTable dtAccess = null;

        private HemoModel.MED_VASCULAR_ACCESSDataTable dtVasularAccess = null;


        private DataTable dtPastCure = null;

        private DataTable dtPastPressure = null;

        private DataTable dtPastCureAndPressure = null;

        private decimal dryWeight = 0;

        private decimal todayWeight = 0;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public FastRecipeList() {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FastRecipeList_Load(object sender, EventArgs e) {
            this.Text = "快捷处方";
            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountFrm(this);

            BindLookUpEdit();
            this.txtStartDate.DateTime = DateTime.Now;
            this.lupClass.EditValue = (this.lupClass.Properties.DataSource as DataTable).Rows[1]["ITEM_VALUE"];
            this.lupArea.EditValue = (this.lupArea.Properties.DataSource as DataTable).Rows[1]["ITEM_ID"];
            this.pictureEdit1.EditValue = global::Hemo.Client.Properties.Resources.处方未确定;
            this.pictureEdit2.EditValue = global::Hemo.Client.Properties.Resources.处方确定;
            this.pictureEdit3.EditValue = global::Hemo.Client.Properties.Resources.处方未开;
            Query();

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e) {
            Query();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e) {
            if (!IsDataValid()) {
                return;
            }

            var row = this.gvPatientList.GetFocusedDataRow();
            if (row != null) {
                if (dtRecipe != null && dtRecipe.Rows.Count > 0) {
                    int result = 0;
                    dtRecipe.Rows[0]["DRY_WEIGHT"] = this.spnDRY_WEIGHT.EditValue;
                    dtRecipe.Rows[0]["TODAY_WEIGHT"] = this.spnTODAY_WEIGHT.EditValue;
                    dtRecipe.Rows[0]["UFR"] = this.spnUFR.EditValue;
                    dtRecipe.Rows[0]["VASCULAR_ACCESS_ID"] = this.cmbVASCULAR_ACCESS_ID.EditValue;
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

                    result = hemoService.SaveRecipe(dtRecipe);

                    dtCure = hemoService.GetMainCureByRecipeId(dtRecipe[0].RECIPE_ID);
                    if (dtCure != null && dtCure.Rows.Count > 0) {
                        dtCure.Rows[0]["DRY_WEIGHT"] = this.spnDRY_WEIGHT.EditValue;
                        dtCure.Rows[0]["BEFORE_DRY_WEIGHT"] = this.spnTODAY_WEIGHT.EditValue;
                        dtCure.Rows[0]["UFR"] = this.spnUFR.EditValue;
                        dtCure.Rows[0]["FREQUENCY_HOURS"] = this.spnFREQUENCY_HOURS.EditValue;
                        dtCure.Rows[0]["AFTER_DRY_WEIGHT"] = this.spnAFTER_DRY_WEIGHT.EditValue;
                        dtCure.Rows[0]["VASCULAR_ACCESS_ID"] = this.cmbVASCULAR_ACCESS_ID.EditValue;
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

                        result = hemoService.SaveCureMain(dtCure);
                    }

                    if (result == 1) {
                        AutoClosedMsgBox.ShowForm("保存成功！", "提示", 1000, MessageBoxIcon.Information);
                    }
                }
                else {
                    AutoClosedMsgBox.ShowForm("患者临时处方尚未创建，请先创建临时处方。", "提示", 1000, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// 确认处方
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dxSimpleButton1_Click(object sender, EventArgs e) {
            if (!IsDataValid()) {
                return;
            }

            var row = this.gvPatientList.GetFocusedDataRow();
            if (row != null) {
                if (dtRecipe != null && dtRecipe.Rows.Count > 0) {
                    int result = 0;
                    dtRecipe.Rows[0]["DRY_WEIGHT"] = this.spnDRY_WEIGHT.EditValue;
                    dtRecipe.Rows[0]["TODAY_WEIGHT"] = this.spnTODAY_WEIGHT.EditValue;
                    dtRecipe.Rows[0]["UFR"] = this.spnUFR.EditValue;
                    dtRecipe.Rows[0]["VASCULAR_ACCESS_ID"] = this.cmbVASCULAR_ACCESS_ID.EditValue;
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

                    result = hemoService.SaveRecipe(dtRecipe);

                    dtCure = hemoService.GetMainCureByRecipeId(dtRecipe[0].RECIPE_ID);
                    if (dtCure != null && dtCure.Rows.Count > 0) {
                        dtCure.Rows[0]["DRY_WEIGHT"] = this.spnDRY_WEIGHT.EditValue;
                        dtCure.Rows[0]["BEFORE_DRY_WEIGHT"] = this.spnTODAY_WEIGHT.EditValue;
                        dtCure.Rows[0]["UFR"] = this.spnUFR.EditValue;
                        dtCure.Rows[0]["FREQUENCY_HOURS"] = this.spnFREQUENCY_HOURS.EditValue;
                        dtCure.Rows[0]["AFTER_DRY_WEIGHT"] = this.spnAFTER_DRY_WEIGHT.EditValue;
                        dtCure.Rows[0]["VASCULAR_ACCESS_ID"] = this.cmbVASCULAR_ACCESS_ID.EditValue;
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

                        result = hemoService.SaveCureMain(dtCure);
                    }

                    if (result == 1) {
                        var index = this.gvPatientList.FocusedRowHandle;
                        var r = this.gvPatientList.GetDataRow(index);
                        ConfirmOrCancelOnePatient(true, r);
                        Query();
                    }
                }
                else {
                    AutoClosedMsgBox.ShowForm("患者临时处方尚未创建，请先创建临时处方。", "提示", 1000, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// 取消确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dxSimpleButton2_Click(object sender, EventArgs e) {
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
        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
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
        private void repositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
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
        private void btnClose_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 点击列表行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPatientList_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e) {
            var row = this.gvPatientList.GetFocusedDataRow();
            if (row != null) {
                using (BackgroundWorker worker = new BackgroundWorker()) {
                    worker.DoWork += new DoWorkEventHandler(worker2_DoWork);
                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker2_RunWorkerCompleted);
                    worker.RunWorkerAsync(row);
                }

                using (BackgroundWorker worker = new BackgroundWorker()) {
                    worker.DoWork += new DoWorkEventHandler(worker3_DoWork);
                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker3_RunWorkerCompleted);
                    worker.RunWorkerAsync(row);
                }
            }
        }

        /// <summary>
        /// 姓名改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPatientName_TextChanged(object sender, EventArgs e) {
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
        private void spnDRY_WEIGHT_EditValueChanged(object sender, EventArgs e) {
            ChangeValue(spnDRY_WEIGHT);
        }

        /// <summary>
        /// 透前体重值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnTODAY_WEIGHT_EditValueChanged(object sender, EventArgs e) {
            ChangeValue(spnTODAY_WEIGHT);
        }

        /// <summary>
        /// 预计脱水值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnUFR_EditValueChanged(object sender, EventArgs e) {
            CheckNumberValue(spnUFR);
        }

        /// <summary>
        /// 面积值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnFIRST_PURIFIER_M2_EditValueChanged(object sender, EventArgs e) {
            CheckNumberValue(spnFIRST_PURIFIER_M2);
        }

        /// <summary>
        /// Koa值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnFIRST_PURIFIER_KOA_EditValueChanged(object sender, EventArgs e) {
            CheckNumberValue(spnFIRST_PURIFIER_KOA);
        }

        /// <summary>
        /// Kuf值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnFIRST_PURIFIER_KUF_EditValueChanged(object sender, EventArgs e) {
            CheckNumberValue(spnFIRST_PURIFIER_KUF);
        }

        /// <summary>
        /// 面积2值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnSECOND_PURIFIER_M2_EditValueChanged(object sender, EventArgs e) {
            CheckNumberValue(spnSECOND_PURIFIER_M2);
        }

        /// <summary>
        /// Koa2值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnSECOND_PURIFIER_KOA_EditValueChanged(object sender, EventArgs e) {
            CheckNumberValue(spnSECOND_PURIFIER_KOA);
        }

        /// <summary>
        /// Kuf2值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnSECOND_PURIFIER_KUF_EditValueChanged(object sender, EventArgs e) {
            CheckNumberValue(spnSECOND_PURIFIER_KUF);
        }

        /// <summary>
        /// 净化方式选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPURIFICATION_MODE_EditValueChanged(object sender, EventArgs e) {
            if (this.cmbPURIFICATION_MODE.Text == "HD") {
                //this.cmbFIRST_PURIFIER_MODEL.EditValue = "90150299-e0a1-40b8-b9e8-82d504c88f21";//F6HPS
                this.cmbFIRST_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";//聚砜膜
                this.spnFIRST_PURIFIER_M2.EditValue = 1.3;
                this.cmbSECOND_PURIFIER_MODEL.EditValue = null;
                this.cmbSECOND_PURIFIER_NAME.EditValue = null;
                this.spnSECOND_PURIFIER_M2.EditValue = 0;
            }
            else if (this.cmbPURIFICATION_MODE.Text == "HDF") {
                this.cmbFIRST_PURIFIER_MODEL.EditValue = "59b5a10f-5a1f-4192-bc70-6be6b40efdb1";//FX80
                this.cmbFIRST_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";//聚砜膜
                this.spnFIRST_PURIFIER_M2.EditValue = 1.8;
                this.cmbSECOND_PURIFIER_MODEL.EditValue = null;
                this.cmbSECOND_PURIFIER_NAME.EditValue = null;
                this.spnSECOND_PURIFIER_M2.EditValue = 0;
            }
            else if (this.cmbPURIFICATION_MODE.Text == "HD+HP") {
                //this.cmbFIRST_PURIFIER_MODEL.EditValue = "90150299-e0a1-40b8-b9e8-82d504c88f21";//F6HPS
                this.cmbFIRST_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";//聚砜膜
                this.spnFIRST_PURIFIER_M2.EditValue = 1.3;
                this.cmbSECOND_PURIFIER_MODEL.EditValue = "61c16922-87a4-4176-8e6c-726b04baea7b";//HP130
                this.cmbSECOND_PURIFIER_NAME.EditValue = "92f926c3-cb00-43cf-b4b5-7011597d51e7";//树脂
                this.spnSECOND_PURIFIER_M2.EditValue = 0;
            }
            else {
                this.cmbFIRST_PURIFIER_MODEL.EditValue = null;
                this.cmbFIRST_PURIFIER_NAME.EditValue = null;
                this.spnFIRST_PURIFIER_M2.EditValue = 0;
                this.cmbSECOND_PURIFIER_MODEL.EditValue = null;
                this.cmbSECOND_PURIFIER_NAME.EditValue = null;
                this.spnSECOND_PURIFIER_M2.EditValue = 0;
            }
        }

        /// <summary>
        /// 净化器型号选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFIRST_PURIFIER_MODEL_EditValueChanged(object sender, EventArgs e) {
            if (this.cmbFIRST_PURIFIER_MODEL.Text.Equals("威高F15"))
            {
                this.cmbFIRST_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";
                this.spnFIRST_PURIFIER_M2.EditValue = 1.5;
            }
            else if (this.cmbFIRST_PURIFIER_MODEL.Text.Equals("FX80")) {
                this.cmbFIRST_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";
                this.spnFIRST_PURIFIER_M2.EditValue = 1.8;
            }
            else if (this.cmbFIRST_PURIFIER_MODEL.Text.Equals("金宝Polyflux 14L"))
            {
                this.cmbFIRST_PURIFIER_NAME.EditValue = "52269ca7-b9ba-490a-9164-8199220ec61b";
                this.spnFIRST_PURIFIER_M2.EditValue = 1.8;
            }
            else if (this.cmbFIRST_PURIFIER_MODEL.Text.Equals("HP130"))
            {
                this.cmbFIRST_PURIFIER_NAME.EditValue = "92f926c3-cb00-43cf-b4b5-7011597d51e7";
            }
        }

        /// <summary>
        /// 净化器型号2选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSECOND_PURIFIER_MODEL_EditValueChanged(object sender, EventArgs e) {
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
                this.spnSECOND_PURIFIER_M2.EditValue = 1.8;
            }
            else if (this.cmbSECOND_PURIFIER_MODEL.Text.Equals("HP130"))
            {
                this.cmbSECOND_PURIFIER_NAME.EditValue = "92f926c3-cb00-43cf-b4b5-7011597d51e7";
            }
        }

        /// <summary>
        /// 抗凝治疗方法选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTHERAPEUTIC_METHOD_EditValueChanged(object sender, EventArgs e) {
            if (this.cmbTHERAPEUTIC_METHOD.Text.Equals("低分子肝素抗凝")) {
                cmbFIRST_DRUG_UNIT.EditValue = "74e7e438-7f88-4e51-b8f1-376023e803c1";
                cmbFIRST_DRUG_MODE.EditValue = "d18bc2cf-a2f6-4544-ad1c-2dfe93b82d7d";
                cmbSECOND_DRUG_UNIT.EditValue = "74e7e438-7f88-4e51-b8f1-376023e803c1";
                cmbSECOND_DRUG_MODE.EditValue = "d18bc2cf-a2f6-4544-ad1c-2dfe93b82d7d";
            }
            else if (this.cmbTHERAPEUTIC_METHOD.Text.Equals("普通肝素抗凝") || this.cmbTHERAPEUTIC_METHOD.Text.Equals("阿加曲班")) {
                this.cmbFIRST_DRUG_UNIT.EditValue = "a0a7c82b-78d8-431c-b9f9-fd78f34c8a4c";
                this.cmbFIRST_DRUG_MODE.EditValue = "d18bc2cf-a2f6-4544-ad1c-2dfe93b82d7d";
                this.cmbSECOND_DRUG_UNIT.EditValue = "a0a7c82b-78d8-431c-b9f9-fd78f34c8a4c";
                this.cmbSECOND_DRUG_MODE.EditValue = "d18bc2cf-a2f6-4544-ad1c-2dfe93b82d7d";
            }
            else {
                this.cmbFIRST_DRUG_UNIT.EditValue = string.Empty;
                this.cmbFIRST_DRUG_MODE.EditValue = string.Empty;
                this.cmbSECOND_DRUG_UNIT.EditValue = string.Empty;
                this.cmbSECOND_DRUG_MODE.EditValue = string.Empty;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 下拉项绑定
        /// </summary>
        private void BindLookUpEdit() {
            //班次
            ConfigModel.MED_COMMON_ITEMLISTDataTable dtClass = configService.GetConfigList(string.Empty, string.Empty, "班次", "1");
            if (dtClass != null && dtClass.Rows.Count > 0) {
                DataRow row = dtClass.NewRow();
                row["ITEM_NAME"] = "全部";
                row["ITEM_VALUE"] = "0";
                row["ITEM_ID"] = "c5540d95c-76a2-4af4-893a-13df547kj3s";
                row["ORDER_NUMBER"] = 0;
                dtClass.Rows.InsertAt(row, 0);
                Utility.BindLookUpEdit(this.lupClass, "ITEM_VALUE", "ITEM_NAME", (DataTable)dtClass, "ITEM_NAME", "班次");
            }

            //区域
            ConfigModel.MED_COMMON_ITEMLISTDataTable dtArea = configService.GetConfigList(string.Empty, string.Empty, "区域", "1");
            if (dtArea != null && dtArea.Rows.Count > 0) {
                DataRow row = dtArea.NewRow();
                row["ITEM_NAME"] = "全部";
                row["ITEM_ID"] = "c5540d95c-76a2-4af4-893a-13tg43gdf34f";
                row["ORDER_NUMBER"] = 0;
                dtArea.Rows.InsertAt(row, 0);
                Utility.BindLookUpEdit(this.lupArea, "ITEM_ID", "ITEM_NAME", (DataTable)dtArea, "ITEM_NAME", "区域");
            }

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
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void Query() {
            using (BackgroundWorker worker = new BackgroundWorker()) {
                worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                worker.RunWorkerAsync();
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e) {
            string area = this.lupArea.Text == "全部" ? string.Empty : this.lupArea.EditValue.ToString();
            string banci = this.lupClass.Text == "全部" ? string.Empty : this.lupClass.EditValue.ToString();
            dtPatient = patientService.GetPatientListBySchedule(this.txtStartDate.DateTime, area, banci);
            dtPatient = Utility.GetSubTable(dtPatient, string.Empty, "AREA_VALUE,NAME ASC");
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (!dtPatient.Columns.Contains("STATUS_TAG")) {
                dtPatient.Columns.Add("STATUS_TAG", System.Type.GetType("System.Byte[]"));
            }
            dtPatient.AsEnumerable().ToList().ForEach(row => {
                if (row["PURIFIER_MODEL_ID"].ToString().Length == 0) {
                    row["STATUS_TAG"] = Utility.BitmapToBytes(Resources.处方未确定);
                }
                else {
                    row["STATUS_TAG"] = Utility.BitmapToBytes(Resources.处方确定);
                }
                if (row["PURIFICATION_MODE_NAME"].ToString().Length == 0) {
                    row["STATUS_TAG"] = Utility.BitmapToBytes(Resources.处方未开);
                }
            });

            DataTable dtResult = dtPatient.Clone();
            dtPatient.AsEnumerable().CopyToDataTable(dtResult, LoadOption.PreserveChanges);
            if (!string.IsNullOrEmpty(this.txtPatientName.Text)) {
                dtResult.Clear();
                dtPatient.AsEnumerable().Where(r => r["NAME"].ToString().Contains(txtPatientName.Text.Trim()) || PinYinConverter.GetPYString(r["NAME"].ToString()).ToUpper().Contains(txtPatientName.Text.Trim().ToUpper())).CopyToDataTable(dtResult, LoadOption.PreserveChanges);
            }

            this.gcPatientList.DataSource = dtResult;
        }

        private void worker2_DoWork(object sender, DoWorkEventArgs e) {
            DataRow row = e.Argument as DataRow;
            //LoadPastCureInfo(row["HEMODIALYSIS_ID"].ToString());
            //dtAccess = hemoService.GetItemListByHemoIDandItemType(row["HEMODIALYSIS_ID"].ToString(), "血管通路");

            dtVasularAccess = hemoService.GetPatientVasular_AccessDt(row["HEMODIALYSIS_ID"].ToString());


            dtRecipe = hemoService.GetRecipeByHemodialysisIDAndDate(row["HEMODIALYSIS_ID"].ToString(), this.txtStartDate.DateTime);
            cureDt = (dtRecipe != null && dtRecipe.Rows.Count > 0) ? hemoService.GetMainCureByRecipeId(dtRecipe[0].RECIPE_ID) : null;
            e.Result = row;
        }

        private void worker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            DataRow row = e.Result as DataRow;
            this.txtName.Text = row["NAME"].ToString();
            this.txtID.Text = row["PATIENT_ID"].ToString();
            this.txtCardNo.Text = row["CREDENTIALS_NUMBER"].ToString();
            //历次透前体重
            //this.lupPastBeforeWeight.Properties.DataSource = (dtPastCure != null && dtPastCure.Rows.Count > 0) ? dtPastCure : null;
            //历次预计脱水
            //this.lupPastUFR.Properties.DataSource = (dtPastCure != null && dtPastCure.Rows.Count > 0) ? dtPastCure : null;
            //历次透后体重
            //this.lupPastAfterWeight.Properties.DataSource = (dtPastCure != null && dtPastCure.Rows.Count > 0) ? dtPastCure : null;
            //历次收缩压
            //this.lupPastSSY.Properties.DataSource = (dtPastPressure != null && dtPastPressure.Rows.Count > 0) ? dtPastPressure : null;
            //历次舒张压
            //this.lupPastSZY.Properties.DataSource = (dtPastPressure != null && dtPastPressure.Rows.Count > 0) ? dtPastPressure : null;
            //历次脉搏
            //this.lupPastMB.Properties.DataSource = (dtPastPressure != null && dtPastPressure.Rows.Count > 0) ? dtPastPressure : null;
            //BaseControlInfo.BindLookUpEdit(this.cmbVASCULAR_ACCESS_ID, "ITEM_ID", "ITEM_NAME", dtAccess, "ITEM_NAME", "血管通路");
            //this.cmbVASCULAR_ACCESS_ID.Properties.DropDownRows = dtAccess.Rows.Count == 0 ? 1 : dtAccess.Rows.Count;

            BaseControlInfo.BindLookUpEdit(this.cmbVASCULAR_ACCESS_ID, "VASCULAR_ACCESS_ID", "PATIENT_ID", dtVasularAccess, "PATIENT_ID", "血管通路");
            if (dtVasularAccess.Rows.Count > 0)
                this.cmbVASCULAR_ACCESS_ID.EditValue = dtVasularAccess[0].VASCULAR_ACCESS_ID;
            else
                this.cmbVASCULAR_ACCESS_ID.EditValue = null;

            if (dtRecipe != null && dtRecipe.Rows.Count > 0) {
                this.spnDRY_WEIGHT.EditValue = dryWeight = Utility.CDecimal(dtRecipe.Rows[0]["DRY_WEIGHT"].ToString());
                this.spnTODAY_WEIGHT.EditValue = todayWeight = Utility.CDecimal(dtRecipe.Rows[0]["TODAY_WEIGHT"].ToString());
                this.spnUFR.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["UFR"].ToString());

                if (this.spnUFR.EditValue.ToString() == "0") {
                    if (dryWeight != 0 && todayWeight != 0) {
                        this.spnUFR.EditValue = todayWeight - dryWeight;
                    }
                }

                //患者已经开始治疗，预计脱水禁止修改
                if (cureDt != null && cureDt.Rows.Count > 0) { this.spnUFR.ReadOnly = true; }
                else { this.spnUFR.ReadOnly = false; }

                this.spnTODAY_BLOODA.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["TODAY_BLOODA"].ToString());
                this.spnTODAY_BLOODB.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["TODAY_BLOODB"].ToString());
                this.spnBLOODP.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["TODAY_BLOODP"].ToString());
                this.cmbVASCULAR_ACCESS_ID.EditValue = dtRecipe.Rows[0]["VASCULAR_ACCESS_ID"].ToString();
                this.spnFREQUENCY_HOURS.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["FREQUENCY_HOURS"].ToString());
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
                this.txtREMARK.Text = dtRecipe.Rows[0]["REMARK"].ToString();
                this.txtFIRST_DRUG_DOSAGE.Text = dtRecipe.Rows[0]["FIRST_DRUG_DOSAGE"].ToString();
                this.cmbFIRST_DRUG_UNIT.EditValue = dtRecipe.Rows[0]["FIRST_DRUG_UNIT"].ToString();
                this.cmbFIRST_DRUG_MODE.EditValue = dtRecipe.Rows[0]["FIRST_DRUG_MODE"].ToString();
                this.txtSECOND_DRUG_DOSAGE.Text = dtRecipe.Rows[0]["SECOND_DRUG_DOSAGE"].ToString();
                this.cmbSECOND_DRUG_UNIT.EditValue = dtRecipe.Rows[0]["SECOND_DRUG_UNIT"].ToString();
                this.cmbSECOND_DRUG_MODE.EditValue = dtRecipe.Rows[0]["SECOND_DRUG_MODE"].ToString();

                if (!dtRecipe[0].IsVASCULAR_ACCESS_IDNull())
                    this.cmbVASCULAR_ACCESS_ID.EditValue = dtRecipe[0].VASCULAR_ACCESS_ID;
                else
                    this.cmbVASCULAR_ACCESS_ID.EditValue = dtVasularAccess[0].VASCULAR_ACCESS_ID;

                //if (this.cmbVASCULAR_ACCESS_ID.Properties.DropDownRows > 1)
                //{
                //    this.cmbVASCULAR_ACCESS_ID.EditValue = dtAccess[0].ITEM_ID;
                //}

                if (cureDt != null && cureDt.Rows.Count > 0)
                    this.spnAFTER_DRY_WEIGHT.EditValue = cureDt[0].AFTER_DRY_WEIGHT;
                else
                    this.spnAFTER_DRY_WEIGHT.EditValue = 0;

                //净化方式默认值
                if (this.cmbPURIFICATION_MODE.EditValue == null || this.cmbPURIFICATION_MODE.EditValue == string.Empty) {
                    this.cmbPURIFICATION_MODE.EditValue = "9c01f053-ad09-4873-b68f-b96d03b8572f";//HD
                }

                //净化器型号默认值
                if (this.cmbFIRST_PURIFIER_MODEL.EditValue == null || this.cmbFIRST_PURIFIER_MODEL.EditValue == string.Empty) {
                    if (this.cmbPURIFICATION_MODE.Text == "HD" || this.cmbPURIFICATION_MODE.Text == "HD+HP") {
                        //this.cmbFIRST_PURIFIER_MODEL.EditValue = "90150299-e0a1-40b8-b9e8-82d504c88f21";//F6HPS
                    }
                    else if (this.cmbPURIFICATION_MODE.Text == "HDF") {
                        this.cmbFIRST_PURIFIER_MODEL.EditValue = "59b5a10f-5a1f-4192-bc70-6be6b40efdb1";//FX80
                    }
                }

                //净化器型号2默认值
                if (this.cmbSECOND_PURIFIER_MODEL.EditValue == null || this.cmbSECOND_PURIFIER_MODEL.EditValue == string.Empty) {
                    if (this.cmbPURIFICATION_MODE.Text == "HD+HP") {
                        this.cmbSECOND_PURIFIER_MODEL.EditValue = "61c16922-87a4-4176-8e6c-726b04baea7b";//HP130
                    }
                }

                //透析膜默认值
                if (this.cmbFIRST_PURIFIER_NAME.EditValue == null || this.cmbFIRST_PURIFIER_NAME.EditValue == string.Empty) {
                    if (this.cmbPURIFICATION_MODE.Text == "HD" || this.cmbPURIFICATION_MODE.Text == "HDF" || this.cmbPURIFICATION_MODE.Text == "HD+HP") {
                        this.cmbFIRST_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";//聚砜膜
                    }
                }

                //透析膜2默认值
                if (this.cmbSECOND_PURIFIER_NAME.EditValue == null || this.cmbSECOND_PURIFIER_NAME.EditValue == string.Empty) {
                    if (this.cmbPURIFICATION_MODE.Text == "HD+HP") {
                        this.cmbSECOND_PURIFIER_NAME.EditValue = "92f926c3-cb00-43cf-b4b5-7011597d51e7";//树脂
                    }
                }

                //面积默认值
                if (this.spnFIRST_PURIFIER_M2.EditValue == null || this.spnFIRST_PURIFIER_M2.EditValue.ToString() == "0") {
                    if (this.cmbPURIFICATION_MODE.Text == "HD" || this.cmbPURIFICATION_MODE.Text == "HD+HP") {
                        this.spnFIRST_PURIFIER_M2.EditValue = 1.3;
                    }
                    else if (this.cmbPURIFICATION_MODE.Text == "HDF") {
                        this.spnFIRST_PURIFIER_M2.EditValue = 1.8;
                    }
                }
            }
        }

        private void worker3_DoWork(object sender, DoWorkEventArgs e) {
            DataRow row = e.Argument as DataRow;
            dtLongRecipe = hemoService.GetLongRecipeByHemodialysisID(row["HEMODIALYSIS_ID"].ToString());
            LoadPastCureInfo(row["HEMODIALYSIS_ID"].ToString());
        }

        private void worker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (dtPastCureAndPressure != null && dtPastCureAndPressure.Rows.Count > 0) {
                gridControl1.DataSource = dtPastCureAndPressure;
                //干体重若为0，默认取上次的值
                if (this.spnDRY_WEIGHT.EditValue.ToString() == "0")
                {
                    this.spnDRY_WEIGHT.EditValue = dtPastCureAndPressure.Rows[0]["DRY_WEIGHT"];
                    this.spnDRY_WEIGHT.EditValue = this.spnDRY_WEIGHT.EditValue != null ? this.spnDRY_WEIGHT.EditValue : 0;
                }

                //干体重若依然为0，默认取长期处方的值
                if (this.spnDRY_WEIGHT.EditValue.ToString() == "0")
                {
                    this.spnDRY_WEIGHT.EditValue = dtLongRecipe.Rows[0]["DRY_WEIGHT"];
                    this.spnDRY_WEIGHT.EditValue = this.spnDRY_WEIGHT.EditValue != null ? this.spnDRY_WEIGHT.EditValue : 0;
                }
            }
        }

        /// <summary>
        /// 加载历次治疗单信息
        /// </summary>
        private void LoadPastCureInfo(string hemoId) {
            //dtPastCure = hemoService.GetPastCureInfoByHemoId(hemoId);
            //dtPastPressure = hemoService.GetPastPressureByHemoId(hemoId);
            dtPastCureAndPressure = hemoService.GetPatientCureAndPastPressureByHemoId(hemoId);
        }

        /// <summary>
        /// 确认、取消确认处方
        /// </summary>
        /// <param name="pStatus"></param>
        /// <param name="currentPatient"></param>
        private void ConfirmOrCancelOnePatient(bool pStatus, DataRow currentPatient) {
            string strStautsName = string.Empty;
            string strInf = string.Empty;
            string strResult = string.Empty;

            if (pStatus) {
                strStautsName = "确认";
            }
            else {
                strStautsName = "取消";
            }

            if (currentPatient["INFECTIOUS_CHECK_RESULT"] != null) {
                strInf = currentPatient["INFECTIOUS_CHECK_RESULT"].ToString();
            }
            strResult = CheckArea(this.lupArea.Text, strInf).Trim();
            if (strResult != string.Empty) {
                if (XtraMessageBox.Show(strResult, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            //if (XtraMessageBox.Show("确定" + strStautsName + "该患者透析处方吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //    return;
            string HemoID = string.Empty;

            #region 读取系统质控校验选项

            DataTable dtQualityCheck = configService.GetConfigList(string.Empty, string.Empty, "质控校验", "1");
            DataTable todayWeight = new DataTable();
            DataTable todayUfR = new DataTable();
            DataTable diagnose = new DataTable();

            if (dtQualityCheck != null && dtQualityCheck.Rows.Count > 0) {
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
            if (currentPatient != null) {
                PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable dataTableSchedule = scheduleService.GetPatientScheduleSignle(this.txtStartDate.DateTime, currentPatient["HEMODIALYSIS_ID"].ToString());
                if (dataTableSchedule != null && dataTableSchedule.Rows.Count > 0) {
                    PatientScheduleModel.MED_PATIENT_SCHEDULERow drSchedule = dataTableSchedule.Rows[0] as PatientScheduleModel.MED_PATIENT_SCHEDULERow;
                    HemodialysisModel.MED_HEMO_RECIPEDataTable recipeTable = hemoService.GetRecipeByHemodialysisIDAndDate(currentPatient["HEMODIALYSIS_ID"].ToString(), this.txtStartDate.DateTime);
                    string strRecipeIDList = string.Empty;
                    string pName = drSchedule.PATIENTNAME;
                    HemoID = drSchedule.HEMODIALYSIS_ID;

                    if (recipeTable != null && recipeTable.Rows.Count > 0) {
                        #region 判断预计脱水、透前体重、诊断是否录入，没有录入强制用户录入否则不能确认处方,达到质控要求。

                        if (diagnose != null && diagnose.Rows.Count > 0) {
                            if (drSchedule["DIAGNOSE"].ToString().Length == 0) {
                                AutoClosedMsgBox.ShowForm("患者【" + pName + "】的诊断未录入，不能确定透析处方。", this.Text, 2000, MessageBoxIcon.Information);
                                return;
                            }
                        }

                        if (dtQualityCheck != null && dtQualityCheck.Rows.Count > 0) {
                            if (todayWeight != null && todayWeight.Rows.Count > 0) {
                                if (recipeTable.Rows[0]["TODAY_WEIGHT"].ToString().Length == 0 || recipeTable.Rows[0]["TODAY_WEIGHT"].ToString() == "0") {
                                    AutoClosedMsgBox.ShowForm("患者【" + pName + "】的透前体重未测量，不能确定透析处方。", this.Text, 2000, MessageBoxIcon.Information);
                                    return;
                                }
                            }

                            if (todayUfR != null && todayUfR.Rows.Count > 0) {
                                if (recipeTable.Rows[0]["UFR"].ToString().Length == 0 || recipeTable.Rows[0]["UFR"].ToString() == "0") {
                                    AutoClosedMsgBox.ShowForm("患者【" + pName + "】的当日预计脱水量未确定，不能确定透析处方。", this.Text, 2000, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }

                        #endregion
                        HemoDWHApplication hemodwApp = new HemoDWHApplication();
                        strRecipeIDList = "'" + recipeTable.Rows[0]["RECIPE_ID"].ToString() + "'";
                        if (pStatus) {
                            drSchedule.RECIPE_ID = recipeTable.Rows[0]["RECIPE_ID"].ToString();
                            drSchedule.PURIFIER_MODEL_ID = recipeTable.Rows[0]["FIRST_PURIFIER_MODEL"].ToString();
                            drSchedule.PURIFICATION_MODE = recipeTable.Rows[0]["PURIFICATION_MODE"].ToString();

                            //进行出库申请
                            hemodwApp.ConfirmHemoDWApply(HemoID, recipeTable.Rows[0]["RECIPE_ID"].ToString(), recipeTable.Rows[0]["PURIFICATION_MODE"].ToString());
                        }
                        else {
                            drSchedule.RECIPE_ID = string.Empty;
                            drSchedule.PURIFIER_MODEL_ID = string.Empty;
                            hemodwApp.CancleConfirHemoDwApply(recipeTable.Rows[0]["RECIPE_ID"].ToString());

                        }
                    }
                    else {
                        AutoClosedMsgBox.ShowForm("患者透析ID:" + currentPatient["HEMODIALYSIS_ID"].ToString() + "的临时处方尚未创建,请先创建临时处方。", "提示", 2000, MessageBoxIcon.Information);
                        return;
                    }

                    //根据处方编号取透析单号，判断患者该处方是否已经执行，如果已经执行不能取消。
                    HemodialysisModel.MED_CURE_MAINDataTable dtCure = hemoService.GetMainCureByRecipeId(recipeTable.Rows[0]["RECIPE_ID"].ToString());
                    if (dtCure != null && dtCure.Rows.Count > 0 && strStautsName == "取消") {
                        AutoClosedMsgBox.ShowForm("该患者处方已执行不能取消!", "提示", 2000, MessageBoxIcon.Information);
                        return;
                    }

                    result = scheduleService.SavePatientScheduleInfo(dataTableSchedule);
                    result = hemoService.SaveRecipeUserIDByRecipeIDList(strRecipeIDList, HemoApplicationContext.Current.CurrentUser.EMP_NO);
                    if (result > 0) {
                        AutoClosedMsgBox.ShowForm("该患者处方已" + strStautsName + "。", "提示", 2000, MessageBoxIcon.Information);
                    }
                }
            }
            else {
                AutoClosedMsgBox.ShowForm("请先选择一个患者！", "提示", 2000, MessageBoxIcon.Information);
            }
        }

        private void ChangeValue(SpinEdit pEdit) {
            CheckNumberValue(pEdit);
            dryWeight = Utility.CDecimal(this.spnDRY_WEIGHT.EditValue != null ? this.spnDRY_WEIGHT.EditValue.ToString() : "0");
            todayWeight = Utility.CDecimal(spnTODAY_WEIGHT.EditValue != null ? spnTODAY_WEIGHT.EditValue.ToString() : "0");
            this.spnUFR.EditValue = (dryWeight != 0 && todayWeight != 0) ? todayWeight - dryWeight : 0;
        }

        /// <summary>
        /// 过滤符号
        /// </summary>
        /// <param name="pEdit"></param>
        private void CheckNumberValue(SpinEdit pEdit) {
            if (pEdit.Text.IndexOf("-") > -1) {
                pEdit.Text = pEdit.Text.Replace("-", "");
            }
        }

        private bool IsDataValid() {
            bool result = true;
            result = BaseControlInfo.CheckSpinEdit(this.spnUFR, "请录入预计脱水！", this.Text);
            if (result == false)
            {
                return result;
            }
            result = BaseControlInfo.CheckpLookUpEdit(this.cmbPURIFICATION_MODE, "请选择净化方式！", this.Text);
            if (result == false) {
                return result;
            }
            result = BaseControlInfo.CheckpLookUpEdit(this.cmbFIRST_PURIFIER_MODEL, "请选择净化器型号！", this.Text);
            if (result == false) {
                return result;
            }
            return result;
        }

        private string CheckArea(string strRoom, string strInf) {
            var rooms = configService.GetConfigList(string.Empty, string.Empty, "隔离病区", "1");
            if (rooms.Count(wh => wh.ITEM_VALUE == strRoom) > 0)//strRoom == "第五透析室"
            {
                if (strInf == "全阴" || string.IsNullOrEmpty(strInf)) {
                    return "该患者没有标记为【乙肝或丙肝】,是否确认在隔离区域治疗？";
                }
            }
            return string.Empty;
        }

        #endregion
    }
}