/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:修改方法SQL
 * 创建标识:刘超-2013年7月8日
 * 
 * 修改时间:2013年10月16日
 * 修改人:吕志强
 * 修改描述:新增方法
 * 
 * 修改时间:2014年1月24日
 * 修改人:顾伟伟
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年5月4日
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

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class CRRTFastRecipeList : HemoBaseFrm
    {
        #region 类变量

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        private IPatient patientService = ServiceManager.Instance.PatientService;

        private IPatientSchedule scheduleService = ServiceManager.Instance.PatientSchedule;

        private IConfig configService = ServiceManager.Instance.ConfigService;

        private DataTable dtPatient = null;

        private HemodialysisModel.MED_HEMO_RECIPEDataTable dtRecipe = null;

        private HemodialysisModel.MED_CURE_MAINDataTable dtCure = null;

        //private HemodialysisModel.MED_CURE_MAINDataTable cureDt = null;

        private ConfigModel.MED_COMMON_ITEMLISTDataTable dtAccess = null;

        private DataTable dtPastCure = null;

        #endregion

        #region 构造函数

        public CRRTFastRecipeList()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CRRTFastRecipeList_Load(object sender, EventArgs e)
        {
            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountFrm(this);
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
                    dtRecipe.Rows[0]["DRY_WEIGHT"] = this.spnDISPLACEMENT_LIQUID.EditValue;
                    dtRecipe.Rows[0]["TODAY_WEIGHT"] = this.spnBLOOW_FLOW.EditValue;
                    dtRecipe.Rows[0]["UFR"] = this.spnUFR.EditValue;
                    dtRecipe.Rows[0]["VASCULAR_ACCESS_ID"] = this.lupVASCULAR_ACCESS_ID.EditValue;
                    dtRecipe.Rows[0]["FREQUENCY_HOURS"] = this.spnFREQUENCY_HOURS.EditValue;
                    dtRecipe.Rows[0]["PURIFICATION_MODE"] = this.lupPURIFICATION_MODE.EditValue;
                    dtRecipe.Rows[0]["REMARK"] = this.txtDISPLACEMENT_SPECIAL_ADJUST.Text;

                    result = hemoService.SaveRecipe(dtRecipe);

                    dtCure = hemoService.GetMainCureByRecipeId(dtRecipe[0].RECIPE_ID);
                    if (dtCure != null && dtCure.Rows.Count > 0)
                    {
                        dtCure.Rows[0]["DRY_WEIGHT"] = this.spnDISPLACEMENT_LIQUID.EditValue;
                        dtCure.Rows[0]["BEFORE_DRY_WEIGHT"] = this.spnBLOOW_FLOW.EditValue;
                        dtCure.Rows[0]["UFR"] = this.spnUFR.EditValue;
                        dtCure.Rows[0]["FREQUENCY_HOURS"] = this.spnFREQUENCY_HOURS.EditValue;
                        dtCure.Rows[0]["VASCULAR_ACCESS_ID"] = this.lupVASCULAR_ACCESS_ID.EditValue;
                        dtCure.Rows[0]["PURIFICATION_MODE"] = this.lupPURIFICATION_MODE.EditValue;

                        result = hemoService.SaveCureMain(dtCure);
                    }

                    if (result == 1)
                    {
                        AutoClosedMsgBox.ShowForm("保存成功！", "提示", 1000, MessageBoxIcon.Information);
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
        private void dxSimpleButton1_Click(object sender, EventArgs e)
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
                    dtRecipe.Rows[0]["PURIFICATION_MODE"] = this.lupPURIFICATION_MODE.EditValue;
                    dtRecipe.Rows[0]["FREQUENCY_HOURS"] = this.spnFREQUENCY_HOURS.EditValue;
                    dtRecipe.Rows[0]["DISPLACEMENT_LIQUID"] = this.spnDISPLACEMENT_LIQUID.EditValue;
                    dtRecipe.Rows[0]["DISPLACEMENT_MODE"] = this.lupDISPLACEMENT_MODE.EditValue;
                    dtRecipe.Rows[0]["BLOOW_FLOW"] = this.spnBLOOW_FLOW.EditValue;
                    dtRecipe.Rows[0]["UFR"] = this.spnUFR.EditValue;
                    dtRecipe.Rows[0]["VASCULAR_ACCESS_ID"] = this.lupVASCULAR_ACCESS_ID.EditValue;
                    dtRecipe.Rows[0]["DISPLACEMENT_RECIPE"] = this.lupDISPLACEMENT_RECIPE.EditValue;
                    dtRecipe.Rows[0]["DISPLACEMENT_SPECIAL_ADJUST"] = this.txtDISPLACEMENT_SPECIAL_ADJUST.Text;
                    dtRecipe.Rows[0]["ANTICOAGULANT_USE"] = this.txtANTICOAGULANT_USE.Text;
                    dtRecipe.Rows[0]["SPECIAL_MATTER"] = this.txtSPECIAL_MATTER.Text;

                    result = hemoService.SaveRecipe(dtRecipe);

                    dtCure = hemoService.GetMainCureByRecipeId(dtRecipe[0].RECIPE_ID);
                    if (dtCure != null && dtCure.Rows.Count > 0)
                    {
                        dtCure.Rows[0]["PURIFICATION_MODE"] = this.lupPURIFICATION_MODE.EditValue;
                        dtCure.Rows[0]["FREQUENCY_HOURS"] = this.spnFREQUENCY_HOURS.EditValue;
                        dtCure.Rows[0]["DISPLACEMENT_LIQUID"] = this.spnDISPLACEMENT_LIQUID.EditValue;
                        dtCure.Rows[0]["DISPLACEMENT_MODE"] = this.lupDISPLACEMENT_MODE.EditValue;
                        dtCure.Rows[0]["BLOOW_FLOW"] = this.spnBLOOW_FLOW.EditValue;
                        dtCure.Rows[0]["UFR"] = this.spnUFR.EditValue;
                        dtCure.Rows[0]["VASCULAR_ACCESS_ID"] = this.lupVASCULAR_ACCESS_ID.EditValue;
                        dtCure.Rows[0]["DISPLACEMENT_RECIPE"] = this.lupDISPLACEMENT_RECIPE.EditValue;
                        dtCure.Rows[0]["DISPLACEMENT_SPECIAL_ADJUST"] = this.txtDISPLACEMENT_SPECIAL_ADJUST.Text;
                        dtCure.Rows[0]["ANTICOAGULANT_USE"] = this.txtANTICOAGULANT_USE.Text;
                        dtCure.Rows[0]["SPECIAL_MATTER"] = this.txtSPECIAL_MATTER.Text;

                        result = hemoService.SaveCureMain(dtCure);
                    }

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
        /// 取消确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dxSimpleButton2_Click(object sender, EventArgs e)
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
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 点击列表行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPatientList_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var row = this.gvPatientList.GetFocusedDataRow();
            if (row != null)
            {
                using (BackgroundWorker worker = new BackgroundWorker())
                {
                    worker.DoWork += new DoWorkEventHandler(worker2_DoWork);
                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker2_RunWorkerCompleted);
                    worker.RunWorkerAsync(row);
                }

                using (BackgroundWorker worker = new BackgroundWorker())
                {
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
        private void txtPatientName_TextChanged(object sender, EventArgs e)
        {
            var dtResult = new DataTable();
            dtResult = dtPatient.Clone();
            dtResult.Clear();
            dtPatient.AsEnumerable().Where(i => i["NAME"].ToString().Contains(txtPatientName.Text.Trim()) || PinYinConverter.GetPYString(i["NAME"].ToString()).ToUpper().Contains(txtPatientName.Text.ToUpper())).CopyToDataTable(dtResult, LoadOption.PreserveChanges);
            this.gcPatientList.DataSource = dtResult;
        }

        /// <summary>
        /// 超滤率改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnUFR_EditValueChanged(object sender, EventArgs e)
        {
            CheckNumberValue(spnUFR);
        }

        /// <summary>
        /// 置换液流速改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnDISPLACEMENT_LIQUID_EditValueChanged(object sender, EventArgs e)
        {
            CheckNumberValue(spnDISPLACEMENT_LIQUID);
        }

        /// <summary>
        /// 血流速度改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnBLOOW_FLOW_EditValueChanged(object sender, EventArgs e)
        {
            CheckNumberValue(spnBLOOW_FLOW);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载默认设置
        /// </summary>
        private void LoadDefaultSet()
        {
            this.txtStartDate.DateTime = DateTime.Now;
            this.lupClass.EditValue = (this.lupClass.Properties.DataSource as DataTable).Rows[1]["ITEM_VALUE"];
            this.picUnsure.EditValue = global::Hemo.Client.Properties.Resources.处方未确定;
            this.picSure.EditValue = global::Hemo.Client.Properties.Resources.处方确定;
            this.picNoOpen.EditValue = global::Hemo.Client.Properties.Resources.处方未开;
        }

        /// <summary>
        /// 下拉项绑定
        /// </summary>
        private void BindLookUpEdit()
        {
            ConfigModel.MED_COMMON_ITEMLISTDataTable dtClass = configService.GetConfigList(string.Empty, string.Empty, "班次", "1");
            if (dtClass != null && dtClass.Rows.Count > 0)
            {
                DataRow row = dtClass.NewRow();
                row["ITEM_NAME"] = "全部";
                row["ITEM_VALUE"] = "0";
                row["ITEM_ID"] = "c5540d95c-76a2-4af4-893a-13df547kj3s";
                row["ORDER_NUMBER"] = 0;
                dtClass.Rows.InsertAt(row, 0);
                Utility.BindLookUpEdit(this.lupClass, "ITEM_VALUE", "ITEM_NAME", (DataTable)dtClass, "ITEM_NAME", "班次");
            }

            BaseControlInfo.BindLookUpEdit(this.lupPURIFICATION_MODE, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "CRRT净化方式", "1"), "ITEM_NAME", "CRRT净化方式");
            BaseControlInfo.BindLookUpEdit(this.lupDISPLACEMENT_MODE, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "置换方式", "1"), "ITEM_NAME", "置换方式");
            BaseControlInfo.BindLookUpEdit(this.lupDISPLACEMENT_RECIPE, "ITEM_ID", "ITEM_NAME", configService.GetConfigList(string.Empty, string.Empty, "置换液配方", "1"), "ITEM_NAME", "置换液配方");
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                worker.RunWorkerAsync();
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string banci = this.lupClass.Text == "全部" ? string.Empty : this.lupClass.EditValue.ToString();
            string area = string.Empty;
            ConfigModel.MED_COMMON_ITEMLISTDataTable dtArea = configService.GetConfigList(string.Empty, string.Empty, "区域", "1");
            if (dtArea != null && dtArea.Rows.Count > 0)
            {
                area = dtArea.FirstOrDefault(row => row.ITEM_NAME.Equals("CRRT")).ITEM_ID;
            }
            dtPatient = patientService.GetPatientListBySchedule(this.txtStartDate.DateTime, area, banci);
            dtPatient = Utility.GetSubTable(dtPatient, string.Empty, "NAME ASC");
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!dtPatient.Columns.Contains("STATUS_TAG"))
            {
                dtPatient.Columns.Add("STATUS_TAG", System.Type.GetType("System.Byte[]"));
            }
            dtPatient.AsEnumerable().ToList().ForEach(row =>
            {
                if (row["RECIPE_ID"].ToString().Length == 0)
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

            DataTable dtResult = dtPatient.Clone();
            dtPatient.AsEnumerable().CopyToDataTable(dtResult, LoadOption.PreserveChanges);
            if (!string.IsNullOrEmpty(this.txtPatientName.Text))
            {
                dtResult.Clear();
                dtPatient.AsEnumerable().Where(r => r["NAME"].ToString().Contains(txtPatientName.Text.Trim()) || PinYinConverter.GetPYString(r["NAME"].ToString()).ToUpper().Contains(txtPatientName.Text.Trim().ToUpper())).CopyToDataTable(dtResult, LoadOption.PreserveChanges);
            }

            this.gcPatientList.DataSource = dtResult;
        }

        private void worker2_DoWork(object sender, DoWorkEventArgs e)
        {
            DataRow row = e.Argument as DataRow;
            dtAccess = hemoService.GetItemListByHemoIDandItemType(row["HEMODIALYSIS_ID"].ToString(), "血管通路");
            dtRecipe = hemoService.GetRecipeByHemodialysisIDAndDate(row["HEMODIALYSIS_ID"].ToString(), this.txtStartDate.DateTime);
            //cureDt = (dtRecipe != null && dtRecipe.Rows.Count > 0) ? hemoService.GetMainCureByRecipeId(dtRecipe[0].RECIPE_ID) : null;
            e.Result = row;
        }

        private void worker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DataRow row = e.Result as DataRow;
            this.txtName.Text = row["NAME"].ToString();
            this.txtID.Text = row["PATIENT_ID"].ToString();
            this.txtCardNo.Text = row["CREDENTIALS_NUMBER"].ToString();
            BaseControlInfo.BindLookUpEdit(this.lupVASCULAR_ACCESS_ID, "ITEM_ID", "ITEM_NAME", dtAccess, "ITEM_NAME", "血管通路");
            this.lupVASCULAR_ACCESS_ID.Properties.DropDownRows = dtAccess.Rows.Count == 0 ? 1 : dtAccess.Rows.Count;

            if (dtRecipe != null && dtRecipe.Rows.Count > 0)
            {
                this.lupPURIFICATION_MODE.EditValue = dtRecipe.Rows[0]["PURIFICATION_MODE"].ToString();
                this.spnFREQUENCY_HOURS.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["FREQUENCY_HOURS"].ToString());
                this.spnDISPLACEMENT_LIQUID.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["DISPLACEMENT_LIQUID"].ToString());
                this.lupDISPLACEMENT_MODE.EditValue = dtRecipe.Rows[0]["DISPLACEMENT_MODE"].ToString();
                this.spnBLOOW_FLOW.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["BLOOW_FLOW"].ToString());
                this.spnUFR.EditValue = Utility.CDecimal(dtRecipe.Rows[0]["UFR"].ToString());
                this.lupVASCULAR_ACCESS_ID.EditValue = dtRecipe.Rows[0]["VASCULAR_ACCESS_ID"].ToString();
                this.lupDISPLACEMENT_RECIPE.EditValue = dtRecipe.Rows[0]["DISPLACEMENT_RECIPE"].ToString();
                this.txtDISPLACEMENT_SPECIAL_ADJUST.Text = dtRecipe.Rows[0]["DISPLACEMENT_SPECIAL_ADJUST"].ToString();
                this.txtANTICOAGULANT_USE.Text = dtRecipe.Rows[0]["ANTICOAGULANT_USE"].ToString();
                this.txtSPECIAL_MATTER.Text = dtRecipe.Rows[0]["SPECIAL_MATTER"].ToString();

                //患者已经开始治疗，超滤率禁止修改
                //if (cureDt != null && cureDt.Rows.Count > 0) { this.spnUFR.ReadOnly = true; }
                //else { this.spnUFR.ReadOnly = false; }

                if (this.lupVASCULAR_ACCESS_ID.Properties.DropDownRows > 1)
                {
                    this.lupVASCULAR_ACCESS_ID.EditValue = dtAccess[0].ITEM_ID;
                }

                //净化方式默认值
                if (this.lupPURIFICATION_MODE.EditValue == null || this.lupPURIFICATION_MODE.EditValue == string.Empty)
                {
                    this.lupPURIFICATION_MODE.EditValue = "9E5A7415339E4812A70F5E353E7A7D7B";//CVVH
                }
            }
        }

        private void worker3_DoWork(object sender, DoWorkEventArgs e)
        {
            DataRow row = e.Argument as DataRow;
            dtPastCure = hemoService.GetPastCureInfoByHemoId(row["HEMODIALYSIS_ID"].ToString());
        }

        private void worker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            gcRecentInfo.DataSource = dtPastCure;
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
            strResult = CheckArea("CRRT", strInf).Trim();
            if (strResult != string.Empty)
            {
                if (XtraMessageBox.Show(strResult, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            //读取系统质控校验选项
            DataTable dtQualityCheck = configService.GetConfigList(string.Empty, string.Empty, "质控校验", "1");
            DataTable diagnose = new DataTable();

            if (dtQualityCheck != null && dtQualityCheck.Rows.Count > 0)
            {
                //读取系统是否需要验证诊断结果
                diagnose = Utility.GetSubTable(dtQualityCheck, "item_name='诊断结果' and status='1'");
            }

            int result = 0;
            string hemoId = string.Empty;
            if (currentPatient != null)
            {
                PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable dataTableSchedule = scheduleService.GetPatientScheduleSignle(this.txtStartDate.DateTime, currentPatient["HEMODIALYSIS_ID"].ToString());
                if (dataTableSchedule != null && dataTableSchedule.Rows.Count > 0)
                {
                    PatientScheduleModel.MED_PATIENT_SCHEDULERow drSchedule = dataTableSchedule.Rows[0] as PatientScheduleModel.MED_PATIENT_SCHEDULERow;
                    HemodialysisModel.MED_HEMO_RECIPEDataTable recipeTable = hemoService.GetRecipeByHemodialysisIDAndDate(currentPatient["HEMODIALYSIS_ID"].ToString(), this.txtStartDate.DateTime);
                    string strRecipeIDList = string.Empty;
                    string pName = drSchedule.PATIENTNAME;
                    hemoId = drSchedule.HEMODIALYSIS_ID;

                    if (recipeTable != null && recipeTable.Rows.Count > 0)
                    {
                        //判断诊断是否录入，没有录入强制用户录入否则不能确认处方,达到质控要求。
                        if (diagnose != null && diagnose.Rows.Count > 0)
                        {
                            if (drSchedule["DIAGNOSE"].ToString().Length == 0)
                            {
                                AutoClosedMsgBox.ShowForm("患者【" + pName + "】的诊断未录入，不能确定透析处方。", this.Text, 2000, MessageBoxIcon.Information);
                                return;
                            }
                        }

                        HemoDWHApplication hemodwApp = new HemoDWHApplication();
                        strRecipeIDList = "'" + recipeTable.Rows[0]["RECIPE_ID"].ToString() + "'";
                        if (pStatus)
                        {
                            drSchedule.RECIPE_ID = recipeTable.Rows[0]["RECIPE_ID"].ToString();
                            drSchedule.PURIFICATION_MODE = recipeTable.Rows[0]["PURIFICATION_MODE"].ToString();
                            //进行出库申请
                            hemodwApp.ConfirmHemoDWApply(hemoId, recipeTable.Rows[0]["RECIPE_ID"].ToString(), recipeTable.Rows[0]["PURIFICATION_MODE"].ToString());
                        }
                        else
                        {
                            drSchedule.RECIPE_ID = string.Empty;
                            drSchedule.PURIFICATION_MODE = string.Empty;
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
            //result = BaseControlInfo.CheckSpinEdit(this.spnUFR, "请录入超滤率！", this.Text);
            //if (result == false)
            //{
            //    return result;
            //}
            result = BaseControlInfo.CheckpLookUpEdit(this.lupPURIFICATION_MODE, "请选择净化方式！", this.Text);
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

        #endregion
    }
}