/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:处方确认类
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.Service;
using Hemo.IService;
using Hemo.IService.Config;
using Hemo.Client.Properties;
using Hemo.IService.PatientSchedule;
using Hemo.Client.UI.Lab;
using DevExpress.XtraGrid.Views.Card;
using Hemo.Client.UI.Patient;
using Hemo.Client.Core;
using Hemo.Client.Controls;

using System.Linq;

using Hemo.Client.UI.FollowUp;
using Hemo.Client.UI.Assessment;
using System.Diagnostics;
using System.Security;
using System.Configuration;
using Hemo.Client.UI.PatientFixUI;


namespace Hemo.Client.UI.Hemodialysis {
    public partial class EditRecipeConfirmListNew : DevExpress.XtraEditors.XtraUserControl
    {
        #region 变量
        public event EventHandler<ConfirmClickEventArgs> ConfirmDoubleClick;
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        DataTable patientDataTable = null;

        public CardView LayoutViewConfirmPatient {
            get {
                return this.cardView1;
            }
        }
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        public EditRecipeConfirmListNew() {
            InitializeComponent();
            txtStartDate.EditValue = System.DateTime.Now.ToShortDateString();
            //  InitList();
        }
        #region 事件
        /// <summary>
        /// 根据当前患者排班患者插入当天的处方数据
        /// </summary>
        private void insertPatientTodayRecipe() {
            int result = _hemodialysisService.CreatePatientRecipeBydate(Utility.CDate(this.txtStartDate.EditValue.ToString()));
        }

        private void btnQuery_Click(object sender, EventArgs e) {
            CalculateWeek();
        }

        public void InitList() {
            ConfigModel.MED_COMMON_ITEMLISTDataTable _banChiDateTable = this._configService.GetConfigList(string.Empty, string.Empty, "班次", "1");
            Hemo.Utilities.Utility.BindLookUpEdit(ediBanCi, "ITEM_VALUE", "ITEM_NAME", _banChiDateTable, "ITEM_NAME", "班次");
            ediBanCi.EditValue = _banChiDateTable[0].ITEM_VALUE;// "全部";

            ConfigModel.MED_COMMON_ITEMLISTDataTable config = this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1");
            if (config != null && config.Rows.Count > 0) {
                DataRow SickAreaRow = config.NewRow();
                SickAreaRow["ITEM_NAME"] = "全部";
                SickAreaRow["ITEM_ID"] = "c570d95c-76a2-4af4-893a-1357065623bf";
                SickAreaRow["ORDER_NUMBER"] = 0;
                config.Rows.InsertAt(SickAreaRow, 0);
                Hemo.Utilities.Utility.BindLookUpEdit(ediSickArea, "ITEM_ID", "ITEM_NAME", (DataTable)config, "ITEM_NAME", "区域");
            }
            ediSickArea.EditValue = config[1].ITEM_ID;// "c570d95c-76a2-4af4-893a-1357065623bf";

            DataTable dtRecipeStatus = new DataTable();
            dtRecipeStatus.Columns.Add("ITEM_ID", typeof(System.String));
            dtRecipeStatus.Columns.Add("ITEM_NAME", typeof(System.String));
            dtRecipeStatus.Columns.Add("ORDER_NUMBER", typeof(System.Int32));
            DataRow rowStatus = dtRecipeStatus.NewRow();
            rowStatus["ITEM_ID"] = "1";
            rowStatus["ITEM_NAME"] = "全部";
            rowStatus["ORDER_NUMBER"] = 1;
            dtRecipeStatus.Rows.Add(rowStatus);
            rowStatus = dtRecipeStatus.NewRow();
            rowStatus["ITEM_ID"] = "2";
            rowStatus["ITEM_NAME"] = "处方未确定";
            rowStatus["ORDER_NUMBER"] = 2;
            dtRecipeStatus.Rows.Add(rowStatus);
            rowStatus = dtRecipeStatus.NewRow();
            rowStatus["ITEM_ID"] = "3";
            rowStatus["ITEM_NAME"] = "处方已确定";
            rowStatus["ORDER_NUMBER"] = 3;
            dtRecipeStatus.Rows.Add(rowStatus);
            rowStatus = dtRecipeStatus.NewRow();
            rowStatus["ITEM_ID"] = "4";
            rowStatus["ITEM_NAME"] = "处方未开";
            rowStatus["ORDER_NUMBER"] = 4;
            dtRecipeStatus.Rows.Add(rowStatus);
            Utility.BindLookUpEdit(this.ediRecipe, "ITEM_ID", "ITEM_NAME", dtRecipeStatus, "ITEM_NAME", "处方状态");

            txtStartDate.EditValue = System.DateTime.Now.ToShortDateString();
            this.pictureEdit1.EditValue = global::Hemo.Client.Properties.Resources.重点;
            this.pictureEdit2.EditValue = global::Hemo.Client.Properties.Resources.一般;
            this.pictureEdit3.EditValue = global::Hemo.Client.Properties.Resources.处方确定;
            this.pictureEdit4.EditValue = global::Hemo.Client.Properties.Resources.处方未确定;
            this.pictureEdit5.EditValue = global::Hemo.Client.Properties.Resources.处方未开;

        }

        /// <summary>
        /// 根据选择条件查询对应的排班病人数据
        /// </summary>
        public void CalculateWeek() {
            patientDataTable = new DataTable();
            gridPatientList.DataSource = null;
            busyIndicator1.Visible = true;
            busyIndicator1.ShowLoadingScreenFor(this);
            using (BackgroundWorker worker = new BackgroundWorker()) {
                //生成当日临时处方
                insertPatientTodayRecipe();
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    if (txtStartDate.EditValue != null) {
                        DateTime startDate = Utility.CDate(txtStartDate.EditValue.ToString());
                        string strBanCi = string.Empty;
                        if (ediBanCi.EditValue == null || ediBanCi.EditValue.ToString() == "0") {
                            strBanCi = string.Empty;
                        }
                        else {
                            strBanCi = ediBanCi.EditValue.ToString();
                        }
                        string strSickArea = string.Empty;
                        if (ediSickArea.EditValue == null || ediSickArea.EditValue.ToString() == "c570d95c-76a2-4af4-893a-1357065623bf") {
                            strSickArea = string.Empty;
                        }
                        else {
                            strSickArea = ediSickArea.EditValue.ToString();
                        }

                        if (string.IsNullOrEmpty(this.txtName.Text)) {
                            patientDataTable = objPatient.GetPatientListBySchedule(startDate, strSickArea, strBanCi);
                        }
                        else {
                            patientDataTable = objPatient.GetPatientListBySchedule(this.txtName.Text.Trim(), startDate, strSickArea, strBanCi);
                        }

                        patientDataTable.Columns.Add("PATIENT_HEAD_PORTRAIT", System.Type.GetType("System.Byte[]"));
                        patientDataTable.Columns.Add("PATIENT_TAG", System.Type.GetType("System.Byte[]"));
                        patientDataTable.Columns.Add("STATUS_TAG", System.Type.GetType("System.Byte[]"));
                        foreach (DataRow patientRow in patientDataTable.Rows) {
                            if (patientRow["SEX"].ToString() == "男") {
                                patientRow["PATIENT_HEAD_PORTRAIT"] = Utility.BitmapToBytes(Resources.boy);
                            }
                            else {
                                patientRow["PATIENT_HEAD_PORTRAIT"] = Utility.BitmapToBytes(Resources.gril);
                            }
                            if (patientRow["FOCUS_LEVEL"].ToString() == "1") {
                                patientRow["PATIENT_TAG"] = Utility.BitmapToBytes(Resources.重点);
                            }
                            else if (patientRow["FOCUS_LEVEL"].ToString() == "0") {
                                patientRow["PATIENT_TAG"] = Utility.BitmapToBytes(Resources.一般);
                            }
                            else {
                                patientRow["PATIENT_TAG"] = Utility.BitmapToBytes(Resources.关注1);
                            }
                            //有效的一条处方尚未确认
                            if (patientRow["purifier_model_id"].ToString().Length == 0) {
                                //patientRow["STATUS_TAG"] = "透析处方尚未确定!";
                                patientRow["STATUS_TAG"] = Utility.BitmapToBytes(Resources.处方未确定);
                            }
                            else {
                                //patientRow["STATUS_TAG"] = "透析处方已经确定!";
                                patientRow["STATUS_TAG"] = Utility.BitmapToBytes(Resources.处方确定);
                            }
                            if (patientRow["purification_mode_name"].ToString().Length == 0) {
                                // patientRow["STATUS_TAG"] = "有效处方尚未确定!";
                                patientRow["STATUS_TAG"] = Utility.BitmapToBytes(Resources.处方未开);
                            }
                        }
                    }
                    else {
                        if (txtStartDate.EditValue == null) {
                            XtraMessageBox.Show("请选择病人透析排班日期后，查询病人列表信息。", "病患管理");
                            return;
                        }
                    }
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {

                    if (patientDataTable != null && patientDataTable.Rows.Count > 0) {
                        DataTable dtSub = patientDataTable.Clone();
                        if (this.ediRecipe.EditValue == "1") {
                            patientDataTable.AsEnumerable().CopyToDataTable(dtSub, LoadOption.PreserveChanges);
                        }
                        else if (this.ediRecipe.EditValue == "2") {
                            patientDataTable.AsEnumerable().Where(row => row["STATUS_TAG"].Equals(Utility.BitmapToBytes(Resources.处方未确定))).CopyToDataTable(dtSub, LoadOption.PreserveChanges);
                        }
                        else if (this.ediRecipe.EditValue == "3") {
                            patientDataTable.AsEnumerable().Where(row => row["STATUS_TAG"].Equals(Utility.BitmapToBytes(Resources.处方确定))).CopyToDataTable(dtSub, LoadOption.PreserveChanges);
                        }
                        else if (this.ediRecipe.EditValue == "4") {
                            patientDataTable.AsEnumerable().Where(row => row["STATUS_TAG"].Equals(Utility.BitmapToBytes(Resources.处方未开))).CopyToDataTable(dtSub, LoadOption.PreserveChanges);
                        }
                        gridPatientList.DataSource = dtSub;
                        string HD = patientDataTable.Select("PURIFICATION_MODE_NAME='HD'").Length.ToString();
                        string HDF = patientDataTable.Select("PURIFICATION_MODE_NAME='HDF'").Length.ToString();
                        string HF = patientDataTable.Select("PURIFICATION_MODE_NAME='HF'").Length.ToString();
                        string HP = patientDataTable.Select("PURIFICATION_MODE_NAME='HP'").Length.ToString();
                        string HDHP = patientDataTable.Select("PURIFICATION_MODE_NAME='HD+HP'").Length.ToString();
                        string CRRT = patientDataTable.Select("PURIFICATION_MODE_NAME='CRRT'").Length.ToString();
                        string PE = patientDataTable.Select("PURIFICATION_MODE_NAME='PE'").Length.ToString();
                        this.lb_count.Text = string.Format("当前班次.病区的总排班人数为:<Color=blue> {0} </Color=blue>人 HD:<Color=blue> {1} </Color=blue>人 HDF:<Color=blue> {2} </Color=blue>人 HF:<Color=blue> {3} </Color=blue>人 HP:<Color=blue> {4} </Color=blue>人 HD+HP:<Color=blue> {5} </Color=blue>人 CRRT:<Color=blue> {6} </Color=blue>人 PE:<Color=blue> {7} </Color=blue>人", patientDataTable.Rows.Count.ToString(), HD, HDF, HF, HP, HDHP, CRRT, PE);
                    }
                    else {
                        gridPatientList.DataSource = null;
                    }
                    // _ctlStartMain.LoadPatientListSchedule(patientDataTable);
                    // this._ctlStartMain.Dock = DockStyle.Fill;
                    // this.panelControl1.Controls.Add(this._ctlStartMain);
                    this.busyIndicator1.HideLoadingScreen();
                };
                worker.RunWorkerAsync();
            }

        }

        private void cardView1_CustomCardCaptionImage(object sender, DevExpress.XtraGrid.Views.Card.CardCaptionImageEventArgs e) {
            var row = cardView1.GetDataRow(e.RowHandle) as DataRow;
            if (row["SEX"].ToString() == "男") {
                e.ImageIndex = 0;
            }
            else {
                e.ImageIndex = 1;
            }
        }

        private void cardView1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                contextMenuStrip1.Show(MousePosition);
            }
        }

        private void cardView1_CustomDrawCardCaption(object sender, DevExpress.XtraGrid.Views.Card.CardCaptionCustomDrawEventArgs e) {
            if (this.patientDataTable.Rows[e.RowHandle]["purifier_model_name"].ToString().Length != 0) {
                e.Appearance.ForeColor = Color.Blue;
            }
        }

        private void EditRecipeConfirmListNew_Load(object sender, EventArgs e) {
            CalculateWeek();
            // insertPatientTodayRecipe();
            this.cardView1.DoubleClick += new EventHandler(cardView1_DoubleClick);
        }

        /// <summary>
        /// 确认或取消病人处方列表
        /// </summary>
        /// <param name="pStatus">1=确认 0=取消</param>
        private void ConfirmOrCancelPatientList(bool pStatus) {

            string strStautsName = string.Empty;
            if (pStatus) {
                strStautsName = "确认";
            }
            else {
                strStautsName = "取消";
            }

            if (cardView1.RowCount == 0) {


                AutoClosedMsgBox.ShowForm("请先安排病人透析排班信息，再" + strStautsName + "处方信息。", "病患管理", 1000, MessageBoxIcon.Warning);
                return;
            }
            if (txtStartDate.EditValue == null) {
                AutoClosedMsgBox.ShowForm("请先安排病人透析排班信息，再" + strStautsName + "处方信息。", "病患管理", 1000, MessageBoxIcon.Warning);

                return;
            }

            if (XtraMessageBox.Show("确定" + strStautsName + "当前病人列表透析处方吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //确认确认的处方信息，更新排班表数据
            DataTable dt = gridPatientList.DataSource as DataTable;
            string HemoID = string.Empty;
            DataTable dtTemp = new DataTable();
            if (dt != null && dt.Rows.Count > 0) {

                #region 添加验证传染病与对应透析室关系的逻辑
                string strRoom = string.Empty;
                string strInf = string.Empty;
                string strResult = string.Empty;
                int infCount = 0;
                int intBCount = 0;
                if (ediSickArea != null) {
                    strRoom = ediSickArea.Text;
                }
                for (int i = 0; i < dt.Rows.Count; i++) {
                    if (dt.Rows[i]["INFECTIOUS_CHECK_RESULT"] != null) {
                        strInf = dt.Rows[i]["INFECTIOUS_CHECK_RESULT"].ToString();
                        if (strInf.Contains("乙肝") || strInf.Contains("乙型肝炎")) {
                            infCount += 1;
                        }
                        if (strInf.Contains("丙肝") || strInf.Contains("丙型肝炎")) {
                            intBCount += 1;
                        }
                    }
                }

                var rooms = _configService.GetConfigList("", "", "隔离病区", "1");
                if (rooms.Count(wh => wh.ITEM_VALUE == strRoom) > 0)//strRoom == "第五透析室"
                {
                    if (infCount < dt.Rows.Count) {

                        strResult = "该患者是非传染性疾病患者，是否" + strStautsName + "在隔离病区治疗？";
                        if (XtraMessageBox.Show(strResult, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;
                    }
                }


                //else if (strRoom == "第五透析室")
                //{
                //    if (intBCount < dt.Rows.Count)
                //    {
                //        strResult = "该病区有非丙肝病人，是否" + strStautsName + "第五透析室丙肝病人区域治疗计划？";
                //        if (XtraMessageBox.Show(strResult, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                //            return;
                //    }
                //}
                #endregion

                DateTime startDate = Utility.CDate(txtStartDate.EditValue.ToString());
                string strBanCi = string.Empty;
                if (ediBanCi.EditValue == null || ediBanCi.EditValue.ToString() == "0") {
                    strBanCi = string.Empty;
                }
                else {
                    strBanCi = ediBanCi.EditValue.ToString();
                }
                string strSickArea = string.Empty;
                if (ediSickArea.EditValue == null || ediSickArea.EditValue.ToString() == "c570d95c-76a2-4af4-893a-1357065623bf") {
                    strSickArea = string.Empty;
                }
                else {
                    strSickArea = ediSickArea.EditValue.ToString();
                }

                PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable patientScheduleDataTable = _patientScheduleService.GetPatientScheduleByParames(startDate, startDate, strBanCi, strSickArea);
                StringBuilder sbRecipeID = new StringBuilder();

                #region 是否加入质控校验相关判断
                //读取系统质控校验选项
                DataTable dtQualityCheck = this._configService.GetConfigList(string.Empty, string.Empty, "质控校验", "1");
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

                string pName = string.Empty;
                foreach (PatientScheduleModel.MED_PATIENT_SCHEDULERow rowSchedule in patientScheduleDataTable.Rows) {
                    HemoID = rowSchedule["HEMODIALYSIS_ID"].ToString();
                    pName = rowSchedule["NAME"].ToString();
                    //rowSchedule.PATIENTNAME
                    // 2014-03-05 刘超 修改将处方日期通过窗体传入，之前使用的为默认值sysdate，只传入透析号。
                    HemodialysisModel.MED_HEMO_RECIPEDataTable recipeTable = this._hemodialysisService.GetRecipeByHemodialysisIDAndDate(HemoID, startDate);
                    // dtTemp = Utility.GetSubTable(recipeTable, "TRUNC(recipe_date) = TRUNC(" + txtStartDate.EditValue.ToString() + ")") as HemodialysisModel.MED_HEMO_RECIPEDataTable;

                    if (recipeTable != null && recipeTable.Rows.Count > 0) {
                        #region 判断预计脱水、透前体重、诊断是否录入，没有录入强制用户录入否则不能确认处方,达到质控要求。
                        if (diagnose != null && diagnose.Rows.Count > 0) {
                            if (rowSchedule["DIAGNOSE"].ToString().Length == 0) {
                                XtraMessageBox.Show("患者【" + pName + "】的诊断未录入，不能确定透析处方。", "病患管理", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                return;
                            }
                        }

                        if (dtQualityCheck != null && dtQualityCheck.Rows.Count > 0) {
                            if (todayWeight != null && todayWeight.Rows.Count > 0) {
                                if (recipeTable.Rows[0]["TODAY_WEIGHT"].ToString().Length == 0 || recipeTable.Rows[0]["TODAY_WEIGHT"].ToString() == "0") {
                                    XtraMessageBox.Show("患者【" + pName + "】的透前体重未测量，不能确定透析处方。", "病患管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }

                            if (todayUfR != null && todayUfR.Rows.Count > 0) {
                                if (recipeTable.Rows[0]["UFR"].ToString().Length == 0 || recipeTable.Rows[0]["UFR"].ToString() == "0") {
                                    XtraMessageBox.Show("患者【" + pName + "】的当日预计脱水量未确定，不能确定透析处方。", "病患管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                        #endregion

                        sbRecipeID.Append("'").Append(recipeTable.Rows[0]["RECIPE_ID"].ToString()).Append("',");
                        HemoDWHApplication hemodwApp = new HemoDWHApplication();
                        if (pStatus) {
                            if (!string.IsNullOrEmpty(rowSchedule["RECIPE_ID"].ToString())) {
                                continue;
                            }
                            rowSchedule["RECIPE_ID"] = recipeTable.Rows[0]["RECIPE_ID"].ToString();
                            rowSchedule["PURIFIER_MODEL_ID"] = recipeTable.Rows[0]["FIRST_PURIFIER_MODEL"].ToString();
                            rowSchedule["PURIFICATION_MODE"] = recipeTable.Rows[0]["PURIFICATION_MODE"].ToString();

                            //进行出库申请
                            hemodwApp.ConfirmHemoDWApply(HemoID, recipeTable.Rows[0]["RECIPE_ID"].ToString(), recipeTable.Rows[0]["PURIFICATION_MODE"].ToString());
                        }
                        else {
                            rowSchedule["RECIPE_ID"] = string.Empty;
                            rowSchedule["PURIFIER_MODEL_ID"] = string.Empty;
                            hemodwApp.CancleConfirHemoDwApply(recipeTable.Rows[0]["RECIPE_ID"].ToString());

                        }
                    }
                    else {
                        AutoClosedMsgBox.ShowForm("患者【" + pName + "】的临时透析处方尚未创建,请先创建临时透析处方。", "病患管理", 1000, MessageBoxIcon.Warning);

                        return;
                    }
                }


                int result = _patientScheduleService.SavePatientScheduleInfo(patientScheduleDataTable);

                //更新处方的开放医生为当前登录的医生账号，根据当前排班病人列表

                string strRecipeIDList = string.Empty;
                if (sbRecipeID.ToString().Length > 0) {
                    strRecipeIDList = sbRecipeID.ToString().Substring(0, sbRecipeID.ToString().Length - 1);
                }
                result = _hemodialysisService.SaveRecipeUserIDByRecipeIDList(strRecipeIDList, HemoApplicationContext.Current.CurrentUser.EMP_NO);
                if (result > 0) {
                    AutoClosedMsgBox.ShowForm("当前病人列表中处方信息已经" + strStautsName + "。", "病患管理", 1000, MessageBoxIcon.Warning);

                    CalculateWeek();
                }
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e) {
            ConfirmOrCancelPatientList(true);
        }

        private void 患者处方ToolStripMenuItem_Click(object sender, EventArgs e) {
            DataRow row = cardView1.GetFocusedDataRow();

            if (row != null) {
                using (QueryRecipeList frm = new QueryRecipeList(row["HEMODIALYSIS_ID"].ToString(), 0)) {
                    frm.currentRecipeIdStr = row["RECIPE_ID"].ToString();
                    frm.CurrentDt = txtStartDate.DateTime;
                    if (frm.ShowDialog() == DialogResult.Yes) { CalculateWeek(); }
                }
            }
            else {
                AutoClosedMsgBox.ShowForm("请先选择一个病人！", "病患管理", 1000, MessageBoxIcon.Warning);


            }
        }

        private void 促红素ToolStripMenuItem_Click(object sender, EventArgs e) {
            //PatientModel.MED_PATIENTSRow patientRow = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            //ErythropoietinFrm erythropoietinFrm = new ErythropoietinFrm(patientRow, false);
            //erythropoietinFrm.ShowDialog();
        }

        private void 药品医嘱ToolStripMenuItem_Click(object sender, EventArgs e) {
            DataRow row = cardView1.GetFocusedDataRow();
            if (row != null) {
                using (QueryRecipeList frm = new QueryRecipeList(row["HEMODIALYSIS_ID"].ToString(), 1)) {
                    frm.currentRecipeIdStr = row["RECIPE_ID"].ToString();
                    frm.CurrentDt = txtStartDate.DateTime;
                    frm.ShowDialog();
                }
            }
            else {
                AutoClosedMsgBox.ShowForm("请先选择一个病人！", "病患管理", 1000, MessageBoxIcon.Warning);
            }
        }

        private void 血管通路ToolStripMenuItem_Click(object sender, EventArgs e) {

            DataRow row = cardView1.GetFocusedDataRow();
            if (row != null) {
                using (EditVascularAccess frmEditVascularAccess = new EditVascularAccess(row["HEMODIALYSIS_ID"].ToString())) {
                    frmEditVascularAccess.ShowDialog();
                }
            }
            else {
                AutoClosedMsgBox.ShowForm("请先选择一个病人！", "病患管理", 1000, MessageBoxIcon.Warning);
            }
        }

        private void 检查检验ToolStripMenuItem_Click(object sender, EventArgs e) {
            //DataRow dr = cardView1.GetFocusedDataRow() as DataRow;
            //PatientModel.MED_PATIENTSDataTable patientRowTable = new PatientModel.MED_PATIENTSDataTable();
            //PatientModel.MED_PATIENTSRow patientRow = patientRowTable.NewMED_PATIENTSRow();
            //patientRow.VISIT_ID = Utility.CDecimal(dr["VISIT_ID"].ToString());
            //patientRow.HEMODIALYSIS_ID = dr["HEMODIALYSIS_ID"].ToString();
            //patientRow.PATIENT_ID = dr["PATIENT_ID"].ToString();
            DataRow row = cardView1.GetFocusedDataRow();
            if (row != null) {
                using (LabFrm labFrm = new LabFrm(GetPatientRow(row))) {
                    labFrm.ShowDialog();
                }
            }
        }

        private void 补录治疗记录单ToolStripMenuItem_Click(object sender, EventArgs e) {
            DataRow row = cardView1.GetFocusedDataRow();
            if (row != null) {
                //string _strCureID = _hemodialysisService.GetCureID(System.DateTime.Now.ToString(), row["HEMODIALYSIS_ID"].ToString());
                //EditTreatment frmTreatment = new EditTreatment(row["HEMODIALYSIS_ID"].ToString(), _strCureID, 0);
                //frmTreatment.IsReplenishTreat = true;
                //frmTreatment.ShowDialog();

                PatientModel.MED_PATIENTSRow PatientDocRow;
                PatientDocRow = GetPatientRow(row);
                if (row != null) {
                    using (PatientFixInfos FRM = new PatientFixInfos()) {
                        FRM.AreaName = GetAreaName(this.ediSickArea.Text);
                        FRM.currentDt = Utility.CDate(this.txtStartDate.EditValue.ToString());

                        FRM.PatientDocRow = PatientDocRow;
                        FRM.ShowDialog();
                    }
                }
            }
            else {
                AutoClosedMsgBox.ShowForm("请先选择一个病人！", "病患管理", 1000, MessageBoxIcon.Warning);
            }
        }

        private void setPatientFocusLevel(string pLevel) {
            DataRow row = cardView1.GetFocusedDataRow();
            int result = 0;
            if (row != null) {
                DateTime startDate = Utility.CDate(txtStartDate.EditValue.ToString());
                PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable dataTableSchedule = _patientScheduleService.GetPatientScheduleSignle(startDate, row["HEMODIALYSIS_ID"].ToString());
                if (dataTableSchedule != null && dataTableSchedule.Rows.Count > 0) {
                    PatientScheduleModel.MED_PATIENT_SCHEDULERow drSchedule = dataTableSchedule.Rows[0] as PatientScheduleModel.MED_PATIENT_SCHEDULERow;
                    drSchedule.FOCUS_LEVEL = pLevel;
                    result = _patientScheduleService.SavePatientScheduleInfo(dataTableSchedule);
                    if (result > 0) {
                        AutoClosedMsgBox.ShowForm("病人状态更新成功！", "病患管理", 1000, MessageBoxIcon.Warning);

                        CalculateWeek();
                    }
                }
            }
            else {
                AutoClosedMsgBox.ShowForm("请先选择一个病人！", "病患管理", 1000, MessageBoxIcon.Warning);
            }
        }

        private void 标注为重点ToolStripMenuItem_Click(object sender, EventArgs e) {
        }

        private void 标注为一般ToolStripMenuItem_Click(object sender, EventArgs e) {
        }

        private void 清空标注ToolStripMenuItem_Click(object sender, EventArgs e) {
        }

        private string CheckArea(string strRoom, string strInf) {
            var rooms = _configService.GetConfigList("", "", "隔离病区", "1");
            if (rooms.Count(wh => wh.ITEM_VALUE == strRoom) > 0)//strRoom == "第五透析室"
            {
                if (strInf == "全阴" || string.IsNullOrEmpty(strInf)) {
                    return "该病人没有标记为【乙肝或丙肝】,是否确认在隔离区域治疗？";

                }
                //if (strInf.Length == 0 || !strInf.Contains("乙肝") || !strInf.Contains("乙型肝炎")) {
                //    return "该病人没有标记为【乙肝或丙肝】,是否确认在隔离区域治疗？";

                //}
                //if (strInf.Length == 0 || !strInf.Contains("丙肝") || !strInf.Contains("丙型肝炎")) {
                //    return "该病人没有标记为【乙肝或丙肝】,是否确认在隔离区域治疗？";

                //}
            }
            return "";

        }

        private void ConfirmOrCancelOnePatient(bool pStatus) {

            DataRow row = cardView1.GetFocusedDataRow();
            string strStautsName = string.Empty;
            string strRoom = string.Empty;
            string strInf = string.Empty;
            string strResult = string.Empty;

            if (pStatus) {
                strStautsName = "确认";
            }
            else {
                strStautsName = "取消";
            }

            if (ediSickArea != null) {
                strRoom = ediSickArea.Text;
            }

            if (row["INFECTIOUS_CHECK_RESULT"] != null) {
                strInf = row["INFECTIOUS_CHECK_RESULT"].ToString();
            }
            strResult = CheckArea(strRoom, strInf).Trim();
            if (strResult != "") {
                if (XtraMessageBox.Show(strResult, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            if (XtraMessageBox.Show("确定" + strStautsName + "该病人透析处方吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;


            #region 读取系统质控校验选项
            DataTable dtQualityCheck = this._configService.GetConfigList(string.Empty, string.Empty, "质控校验", "1");
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
            if (row != null) {
                DateTime startDate = Utility.CDate(txtStartDate.EditValue.ToString());
                PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable dataTableSchedule = _patientScheduleService.GetPatientScheduleSignle(startDate, row["HEMODIALYSIS_ID"].ToString());


                if (dataTableSchedule != null && dataTableSchedule.Rows.Count > 0) {
                    PatientScheduleModel.MED_PATIENT_SCHEDULERow drSchedule = dataTableSchedule.Rows[0] as PatientScheduleModel.MED_PATIENT_SCHEDULERow;
                    //2014-03-05 刘超 修改将处方日期通过窗体传入，之前使用的为默认值sysdate，只传入透析号。
                    //HemodialysisModel.MED_HEMO_RECIPEDataTable recipeTable = this._hemodialysisService.GetRecipeByHemodialysisID(row["HEMODIALYSIS_ID"].ToString());
                    HemodialysisModel.MED_HEMO_RECIPEDataTable recipeTable = this._hemodialysisService.GetRecipeByHemodialysisIDAndDate(row["HEMODIALYSIS_ID"].ToString(), startDate);
                    string strRecipeIDList = string.Empty;
                    string pName = drSchedule.PATIENTNAME;
                    if (recipeTable != null && recipeTable.Rows.Count > 0) {



                        #region 判断预计脱水、透前体重、诊断是否录入，没有录入强制用户录入否则不能确认处方,达到质控要求。
                        if (diagnose != null && diagnose.Rows.Count > 0) {
                            if (drSchedule["DIAGNOSE"].ToString().Length == 0) {
                                AutoClosedMsgBox.ShowForm("患者【" + pName + "】的诊断未录入，不能确定透析处方。", "病患管理", 2000, MessageBoxIcon.Information);
                                return;
                            }
                        }

                        if (dtQualityCheck != null && dtQualityCheck.Rows.Count > 0) {
                            if (todayWeight != null && todayWeight.Rows.Count > 0) {
                                if (recipeTable.Rows[0]["TODAY_WEIGHT"].ToString().Length == 0 || recipeTable.Rows[0]["TODAY_WEIGHT"].ToString() == "0") {
                                    AutoClosedMsgBox.ShowForm("患者【" + pName + "】的透前体重未测量，不能确定透析处方。", "病患管理", 2000, MessageBoxIcon.Information);
                                    return;
                                }
                            }

                            if (todayUfR != null && todayUfR.Rows.Count > 0) {
                                if (recipeTable.Rows[0]["UFR"].ToString().Length == 0 || recipeTable.Rows[0]["UFR"].ToString() == "0") {
                                    AutoClosedMsgBox.ShowForm("患者【" + pName + "】的当日预计脱水量未确定，不能确定透析处方。", "病患管理", 2000, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                        #endregion

                        strRecipeIDList = "'" + recipeTable.Rows[0]["RECIPE_ID"].ToString() + "'";
                        HemoDWHApplication hemodwApp = new HemoDWHApplication();

                        if (pStatus) {
                            if (drSchedule.IsRECIPE_IDNull() ? false : !string.IsNullOrEmpty(drSchedule.RECIPE_ID)) {
                                AutoClosedMsgBox.ShowForm("该病人处方已进行过确认!", "病患管理", 2000, MessageBoxIcon.Warning);
                                return;
                            }
                            drSchedule.RECIPE_ID = recipeTable.Rows[0]["RECIPE_ID"].ToString();
                            drSchedule.PURIFIER_MODEL_ID = recipeTable.Rows[0]["FIRST_PURIFIER_MODEL"].ToString();
                            drSchedule.PURIFICATION_MODE = recipeTable.Rows[0]["PURIFICATION_MODE"].ToString();
                            //进行出库申请
                            hemodwApp.ConfirmHemoDWApply(recipeTable.Rows[0]["HEMODIALYSIS_ID"].ToString(), recipeTable.Rows[0]["RECIPE_ID"].ToString(), recipeTable.Rows[0]["PURIFICATION_MODE"].ToString());
                        }
                        else {
                            drSchedule.RECIPE_ID = string.Empty;
                            drSchedule.PURIFIER_MODEL_ID = string.Empty;
                            hemodwApp.CancleConfirHemoDwApply(recipeTable.Rows[0]["RECIPE_ID"].ToString());

                        }
                    }
                    else {
                        AutoClosedMsgBox.ShowForm("病人透析ID:" + row["HEMODIALYSIS_ID"].ToString() + "的临时处方尚未创建,请先创建临时处方。", "病患管理", 2000, MessageBoxIcon.Warning);
                        return;
                    }

                    //根据处方编号取透析单号，判断患者该处方是否已经执行，如果已经执行不能取消。
                    HemodialysisModel.MED_CURE_MAINDataTable dtCure = this._hemodialysisService.GetMainCureByRecipeId(recipeTable.Rows[0]["RECIPE_ID"].ToString());
                    if (dtCure != null && dtCure.Rows.Count > 0 && strStautsName == "取消") {
                        AutoClosedMsgBox.ShowForm("该病人处方已执行不能取消!", "病患管理", 2000, MessageBoxIcon.Warning);
                        return;
                    }

                    result = _patientScheduleService.SavePatientScheduleInfo(dataTableSchedule);
                    result = _hemodialysisService.SaveRecipeUserIDByRecipeIDList(strRecipeIDList, HemoApplicationContext.Current.CurrentUser.EMP_NO);
                    if (result > 0) {
                        AutoClosedMsgBox.ShowForm("该病人处方已" + strStautsName + "。", "病患管理", 2000, MessageBoxIcon.Warning);
                        CalculateWeek();
                    }
                }
            }
            else {
                AutoClosedMsgBox.ShowForm("请先选择一个病人！", "病患管理", 2000, MessageBoxIcon.Warning);
            }
        }

        private void 确认处方ToolStripMenuItem_Click(object sender, EventArgs e) {
            ConfirmOrCancelOnePatient(true);
        }

        private void 取消处方ToolStripMenuItem_Click(object sender, EventArgs e) {
            ConfirmOrCancelOnePatient(false);
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            ConfirmOrCancelPatientList(false);
        }

        private void 超滤率ToolStripMenuItem_Click(object sender, EventArgs e) {
            DataRow row = cardView1.GetFocusedDataRow();
            if (row != null) {
                using (var frm = new EditUFR(row["HEMODIALYSIS_ID"].ToString(), Utility.CDate(this.txtStartDate.EditValue.ToString()))) {
                    if (frm.IsShowFrm) {
                        frm.AreaName = this.ediSickArea.Text;
                        frm.CurrentPatient = row;
                        DialogResult result = frm.ShowDialog();
                        if (result == DialogResult.OK) {
                            CalculateWeek();
                        }
                    }
                }
            }
        }

        private void cardView1_DoubleClick(object sender, EventArgs e) {
            DataRow dr = this.cardView1.GetFocusedDataRow();

            PatientModel.MED_PATIENTSRow PatientDocRow;
            PatientDocRow = GetPatientRow(dr);
            if (dr != null) {
                using (PatientFixInfos FRM = new PatientFixInfos()) {
                    FRM.PatientDocRow = PatientDocRow;
                    FRM.currentDt = Utility.CDate(this.txtStartDate.EditValue.ToString());
                    FRM.ShowDialog();
                }
            }
        }

        private void 删除患者ToolStripMenuItem1_Click(object sender, EventArgs e) {
            if (XtraMessageBox.Show("是否确定删除当前患者？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            DataRow row = cardView1.GetFocusedDataRow();
            if (row != null) {
                if (objPatient.DeletePatientByPatient_id(row["HEMODIALYSIS_ID"].ToString()) > 0) { CalculateWeek(); }

            }
        }

        private PatientModel.MED_PATIENTSRow GetPatientRow(DataRow confirmRow) {
            PatientModel.MED_PATIENTSDataTable dtt = new PatientModel.MED_PATIENTSDataTable();
            PatientModel.MED_PATIENTSRow row = dtt.NewMED_PATIENTSRow();
            row.HEMODIALYSIS_ID = confirmRow["HEMODIALYSIS_ID"].ToString();
            row.PATIENT_ID = confirmRow["PATIENT_ID"].ToString();
            row.NAME = confirmRow["NAME"].ToString();
            row.SEX = confirmRow["SEX"].ToString();
            if (!string.IsNullOrEmpty(confirmRow["BIRTHDAY"].ToString()))
                row.BIRTHDAY = Convert.ToDateTime(confirmRow["BIRTHDAY"].ToString());

            row.AGE = Convert.ToDecimal(confirmRow["AGE"].ToString());
            row.NATIVEPLACE = confirmRow["NATIVEPLACE"].ToString();
            row.JOB = confirmRow["JOB"].ToString();
            row.MARITAL = confirmRow["MARITAL"].ToString();
            row.CREDENTIALS_TYPE = confirmRow["CREDENTIALS_TYPE"].ToString();
            row.CREDENTIALS_NUMBER = confirmRow["CREDENTIALS_NUMBER"].ToString();
            row.EDUCATION = confirmRow["EDUCATION"].ToString();
            row.NATION = confirmRow["NATION"].ToString();
            row.WORK_TELEPHONE = confirmRow["WORK_TELEPHONE"].ToString();
            row.ADDRESS = confirmRow["ADDRESS"].ToString();
            row.MEDICAL_TYPE = confirmRow["MEDICAL_TYPE"].ToString();
            row.TELEPHONE = confirmRow["TELEPHONE"].ToString();
            row.TIME_TYPE = confirmRow["TIME_TYPE"].ToString();
            row.SPECIFIC_TIME = Convert.ToDateTime(confirmRow["SPECIFIC_TIME"].ToString());
            row.ADMISSION_NUMBER = confirmRow["ADMISSION_NUMBER"].ToString();
            row.IS_NEW = confirmRow["IS_NEW"].ToString();
            row.WHAT_HOSPITAL_IN = confirmRow["WHAT_HOSPITAL_IN"].ToString();
            row.WHAT_DEPARTMENT_IN = confirmRow["WHAT_DEPARTMENT_IN"].ToString();
            row.FIRST_VISIT = confirmRow["FIRST_VISIT"].ToString();
            row.DIAGNOSE = confirmRow["DIAGNOSE"].ToString();
            if (!string.IsNullOrEmpty(confirmRow["LEAVE_HOSPITAL_TIME"].ToString()))
                row.LEAVE_HOSPITAL_TIME = Convert.ToDateTime(confirmRow["LEAVE_HOSPITAL_TIME"]);
            row.INFECTIOUS_CHECK_RESULT = confirmRow["INFECTIOUS_CHECK_RESULT"].ToString();
            row.INPUT_CODE = confirmRow["INPUT_CODE"].ToString();
            row.WARD_CODE = confirmRow["WARD_CODE"].ToString();
            row.BED_NO = confirmRow["BED_NO"].ToString();
            return row;
        }

        private string GetAreaName(string areaName) {
            switch (areaName) {
                case "第一透析室":
                    return "1室";
                case "第二透析室":
                    return "2室";
                case "第三透析室":
                    return "3室";
                case "第四透析室":
                    return "4室";
                case "第五透析室":
                    return "5室";
                case "第六透析室":
                    return "6室";
                case "第七透析室":
                    return "7室";
                case "第八透析室":
                    return "8室";
                case "第九透析室":
                    return "9室";
                case "CRRT":
                    return "CRRT室";
            }
            return string.Empty;
        }

        private void 修改患者ToolStripMenuItem_Click(object sender, EventArgs e) {
            using (EditPatientNew frmEditPatient = new EditPatientNew()) {
                DataRow confirmRow = this.cardView1.GetFocusedDataRow();
                frmEditPatient.Current = GetPatientRow(confirmRow);
                frmEditPatient.ShowDialog();
            }
            CalculateWeek();
        }

        private void 一般关注ToolStripMenuItem_Click(object sender, EventArgs e) {
            setPatientFocusLevel("0");
        }

        private void 重点关注ToolStripMenuItem_Click(object sender, EventArgs e) {
            setPatientFocusLevel("1");
        }

        private void 取消关注ToolStripMenuItem_Click(object sender, EventArgs e) {
            setPatientFocusLevel("");
        }

        private void ToINJECTTIPToolStripMenuItem_Click(object sender, EventArgs e) {
            using (InfectiousForm frm = new InfectiousForm()) {

                #region
                DataRow confirmRow = this.cardView1.GetFocusedDataRow();
                PatientModel.MED_PATIENTSDataTable dtt = new PatientModel.MED_PATIENTSDataTable();

                var row = dtt.NewMED_PATIENTSRow();
                row.HEMODIALYSIS_ID = confirmRow["HEMODIALYSIS_ID"].ToString();
                row.PATIENT_ID = confirmRow["PATIENT_ID"].ToString();
                row.NAME = confirmRow["NAME"].ToString();
                row.SEX = confirmRow["SEX"].ToString();
                row.BIRTHDAY = Convert.ToDateTime(confirmRow["BIRTHDAY"].ToString());
                row.AGE = Convert.ToDecimal(confirmRow["AGE"].ToString());
                row.NATIVEPLACE = confirmRow["NATIVEPLACE"].ToString();
                row.JOB = confirmRow["JOB"].ToString();
                row.MARITAL = confirmRow["MARITAL"].ToString();
                row.CREDENTIALS_TYPE = confirmRow["CREDENTIALS_TYPE"].ToString();
                row.CREDENTIALS_NUMBER = confirmRow["CREDENTIALS_NUMBER"].ToString();
                row.EDUCATION = confirmRow["EDUCATION"].ToString();
                row.NATION = confirmRow["NATION"].ToString();
                row.WORK_TELEPHONE = confirmRow["WORK_TELEPHONE"].ToString();
                row.ADDRESS = confirmRow["ADDRESS"].ToString();
                row.MEDICAL_TYPE = confirmRow["MEDICAL_TYPE"].ToString();
                row.TELEPHONE = confirmRow["TELEPHONE"].ToString();
                row.TIME_TYPE = confirmRow["TIME_TYPE"].ToString();
                row.SPECIFIC_TIME = Convert.ToDateTime(confirmRow["SPECIFIC_TIME"].ToString());
                row.ADMISSION_NUMBER = confirmRow["ADMISSION_NUMBER"].ToString();
                row.IS_NEW = confirmRow["IS_NEW"].ToString();
                row.WHAT_HOSPITAL_IN = confirmRow["WHAT_HOSPITAL_IN"].ToString();
                row.WHAT_DEPARTMENT_IN = confirmRow["WHAT_DEPARTMENT_IN"].ToString();
                row.FIRST_VISIT = confirmRow["FIRST_VISIT"].ToString();
                row.DIAGNOSE = confirmRow["DIAGNOSE"].ToString();
                if (!string.IsNullOrEmpty(confirmRow["LEAVE_HOSPITAL_TIME"].ToString()))
                    row.LEAVE_HOSPITAL_TIME = Convert.ToDateTime(confirmRow["LEAVE_HOSPITAL_TIME"]);
                row.INFECTIOUS_CHECK_RESULT = confirmRow["INFECTIOUS_CHECK_RESULT"].ToString();
                row.INPUT_CODE = confirmRow["INPUT_CODE"].ToString();
                row.WARD_CODE = confirmRow["WARD_CODE"].ToString();
                row.BED_NO = confirmRow["BED_NO"].ToString();
                frm.Current = row;

                #endregion

                frm.ShowDialog();
            }
            CalculateWeek();
        }

        private void cardView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e) {
            var rowCurrent = cardView1.GetDataRow(e.ListSourceRowIndex);
            if (rowCurrent != null && rowCurrent["INFECTIOUS_CHECK_RESULT"].ToString().Length > 0) {
                if (e.Column == gridColumn14) {
                    e.Column.AppearanceCell.ForeColor = Color.Red;
                }
            }
            else {
                //e.Column.AppearanceCell.ForeColor = Color.White;
            }
        }

        private void 患者病历ToolStripMenuItem_Click(object sender, EventArgs e) {
            DataRow row = cardView1.GetFocusedDataRow();
            if (row != null) {

                //begin 更新后代码
                using (var record = new PatientBaseRecord()) {
                    record.HemoId = row["HEMODIALYSIS_ID"].ToString();
                    record.ShowDialog();
                }
            }
        }

        private void 病程记录ToolStripMenuItem_Click(object sender, EventArgs e) {
            DataRow row = cardView1.GetFocusedDataRow();
            if (row != null) {
                using (PatientProgressNote frmNote = new PatientProgressNote(row["HEMODIALYSIS_ID"].ToString())) {
                    frmNote.ShowDialog();
                }
            }
        }

        private void 已入科ToolStripMenuItem_Click(object sender, EventArgs e) {
            DataRow row = cardView1.GetFocusedDataRow();
            if (row != null) {
                PatientModel.MED_PATIENTSDataTable _patientDataTable = objPatient.GetPatientListByPatientID(row["PATIENT_ID"].ToString());
                if (_patientDataTable != null && _patientDataTable.Rows.Count > 0) {
                    _patientDataTable.Rows[0]["IS_NEW"] = "1";
                    objPatient.SavePatientInfo(_patientDataTable);
                    AutoClosedMsgBox.ShowForm("患者状态已更新为入科!", "病患管理", 1000, MessageBoxIcon.Warning);
                }
            }
        }

        private void 已出科ToolStripMenuItem_Click(object sender, EventArgs e) {
            DataRow row = cardView1.GetFocusedDataRow();
            if (row != null) {
                PatientModel.MED_PATIENTSDataTable _patientDataTable = objPatient.GetPatientListByPatientID(row["PATIENT_ID"].ToString());
                if (_patientDataTable != null && _patientDataTable.Rows.Count > 0) {
                    _patientDataTable.Rows[0]["IS_NEW"] = "0";
                    objPatient.SavePatientInfo(_patientDataTable);
                    AutoClosedMsgBox.ShowForm("患者状态已更新为出科!", "病患管理", 1000, MessageBoxIcon.Warning);
                }
            }
        }

        private void uRRKtV评估ToolStripMenuItem_Click(object sender, EventArgs e) {
            var row = cardView1.GetFocusedDataRow();
            if (row != null) {
                using (FrmSufficiencyURR frmSufficiencyURR = new FrmSufficiencyURR()) {
                    frmSufficiencyURR.HemoId = row["HEMODIALYSIS_ID"].ToString();
                    frmSufficiencyURR.ShowDialog();
                }
            }
        }

        private void 透析充分性评估ToolStripMenuItem_Click(object sender, EventArgs e) {
            var row = cardView1.GetFocusedDataRow();
            if (row != null) {
                using (PatientSufficiency frm = new PatientSufficiency()) {
                    frm.CurrentHemoId = row["HEMODIALYSIS_ID"].ToString();
                    frm.CurrentHemoName = row["NAME"].ToString();
                    frm.ShowDialog();
                }
            }
        }

        private void 风险评估ToolStripMenuItem_Click(object sender, EventArgs e) {
            using (RiskAssessFrm frm = new RiskAssessFrm()) {
                frm.ShowDialog();
            }
        }

        private void 营养评估ToolStripMenuItem_Click(object sender, EventArgs e) {
            var row = cardView1.GetFocusedDataRow();
            if (row != null) {
                using (NutritionSGAFrm frm = new NutritionSGAFrm()) {
                    frm.CurrentHemoId = row["HEMODIALYSIS_ID"].ToString();
                    frm.ShowDialog();
                }
            }
        }

        private void 随访评估ToolStripMenuItem_Click(object sender, EventArgs e) {
            DataRow row = cardView1.GetFocusedDataRow();
            if (row != null) {
                using (FollowUpQuery frm = new FollowUpQuery()) {
                    frm.PatientRow = GetPatientRow(row);
                    frm.ShowDialog();
                }
            }
        }

        private void 评估查询ToolStripMenuItem_Click(object sender, EventArgs e) {
            //var row = cardView1.GetFocusedDataRow();
            //if (row != null)
            //{
            //    using (QueryEstimateSufficiency frm = new QueryEstimateSufficiency())
            //    {
            //        frm.HemoId = row["HEMODIALYSIS_ID"].ToString();
            //        frm.ShowDialog();
            //    }
            //}
        }

        private void 抢救记录ToolStripMenuItem_Click(object sender, EventArgs e) {
            var row = cardView1.GetFocusedDataRow() as DataRow;
            if (row == null) return;
            string _strCureID = _hemodialysisService.GetCureID(this.txtStartDate.EditValue.ToString(), row["HEMODIALYSIS_ID"].ToString());
            if (string.IsNullOrEmpty(_strCureID)) {
                MessageBox.Show("患者没有开始治疗,不可以录入抢救记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var prow = GetPatientRow(row);

            EmrgeRecordForDoc frm = new EmrgeRecordForDoc();

            frm.lbl_PatientInfo.Text = string.Format(@"姓名:{0} 性别:{1} 年龄:{2} 床号:{3} 诊断:{4}", prow.NAME, prow.SEX.ToString(), prow.AGE, prow.BED_NO, prow.DIAGNOSE);

            if (!string.IsNullOrEmpty(_strCureID)) {
                var date = _hemodialysisService.GetMainCureByCureID(_strCureID);
                frm._cureRow = date[0];
            }
            frm.patientHemoId = row["HEMODIALYSIS_ID"].ToString();
            frm.ShowDialog();
        }

        /// <summary>
        /// 打开住院医生工作站程序，传入相关参数，打开历次就诊界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 历次就诊ToolStripMenuItem_Click(object sender, EventArgs e) {
            var row = cardView1.GetFocusedDataRow();
            if (row != null) {
                string file = ConfigurationManager.AppSettings["EMRS_Path"].ToString();

                //SecureString password = new SecureString();
                //string pass = "SUPCON";
                //pass.ToCharArray().ToList().ForEach(c => password.AppendChar(c));

                ProcessStartInfo info = new ProcessStartInfo(file);
                //info.UserName = "SUPCON";
                //info.Password = password;
                info.Arguments = string.Format("{0} {1} {2}", "SUPCON", "SUPCON" + "#" + row["PATIENT_ID"].ToString() + "#" + row["VISIT_ID"].ToString(), "SUPCON" + "#" + row["PATIENT_ID"].ToString() + "#" + row["VISIT_ID"].ToString());

                Process process = new Process();
                process.StartInfo = info;
                process.Start();
                this.ParentForm.WindowState = (this.ParentForm.WindowState != FormWindowState.Minimized) ? FormWindowState.Minimized : this.ParentForm.WindowState;
            }
        }

        private void 用药记录ToolStripMenuItem_Click(object sender, EventArgs e) {
            DataRow row = cardView1.GetFocusedDataRow();
            if (row != null) {
                PatientModel.MED_PATIENTSDataTable _patientDataTable = objPatient.GetPatientListByPatientID(row["PATIENT_ID"].ToString());
                if (_patientDataTable != null && _patientDataTable.Rows.Count > 0) {
                    string _strCureID = _hemodialysisService.GetCureID(this.txtStartDate.EditValue.ToString(), _patientDataTable.Rows[0]["HEMODIALYSIS_ID"].ToString());

                    QueryDrugRecord frm = new QueryDrugRecord(_patientDataTable.Rows[0]["HEMODIALYSIS_ID"].ToString());
                    frm.ShowDialog();
                }
            }
            else {
                XtraMessageBox.Show("请先选择一个病人！", "病患管理");
            }
        }

        private void 透析血压ToolStripMenuItem_Click(object sender, EventArgs e) {
            DataRow row = cardView1.GetFocusedDataRow();
            if (row != null) {
                PatientCurePressureFrm frm = new PatientCurePressureFrm();
                frm.currentHemoId = row["HEMODIALYSIS_ID"].ToString();
                frm.currentPatientName = row["NAME"].ToString();
                frm.ShowDialog();
            }
        }

        private void 患者检查ToolStripMenuItem_Click(object sender, EventArgs e) {
            DataRow row = cardView1.GetFocusedDataRow();
            if (row != null) {
                using (ExamFrm examFrm = new ExamFrm()) {
                    examFrm.PatientRow = GetPatientRow(row);
                    examFrm.ShowDialog();
                }
            }
        }
    }
        #endregion
    /// <summary>
    /// 事件类
    /// </summary>
    public class ConfirmClickEventArgs : EventArgs {
        public DataRow confirmRow { get; set; }
    }
}