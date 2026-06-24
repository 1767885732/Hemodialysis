/*----------------------------------------------------------------
// Copyright (C) 2005 (北京)医疗科技发展有限公司
// 文件名：PatientMgrFrm.cs
// 文件功能描述：病患管理窗体类
// 创建标识：
// 修改时间：2014-4-9
// 修改人：吕志强
// 修改描述：添加常规检验查询菜单功能
//
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Hemo.Client.Controls;
using Hemo.Client.UI.Erythropoietin;
using Hemo.Client.UI.Hemodialysis;
using Hemo.Client.UI.Lab;
using Hemo.Client.UI.Order;
using Hemo.Model;
using System;
using DevExpress.XtraEditors.Repository;
using System.Data;
using Hemo.Utilities;
using Hemo.IService;
using Hemo.Service;
using Hemo.IService.PatientSchedule;
using Hemo.IService.Config;
using System.ComponentModel;
using Hemo.Client.Core;
using Hemo.Client.UI.ReportChart;
using Hemo.WinForm;
using Hemo.Client.UI.PatientSchedule;
using Hemo.Client.UI.User;
using Hemo.Client.UI.Machine;
using Hemo.Client.UI.CommonQuery;
using Hemo.Client.UI.FollowUp;

namespace Hemo.Client.UI.Patient {
    public partial class PatientMgrFrm : HemoBaseFrm {
        #region 变量

        private CtlStartMain _ctlStartMain;
        private string strBanCi = "0";
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        public EventHandler<ShiftEventArgs> ShiftEvent;

        #endregion

        #region 属性

        private PatientModel.MED_PATIENTSRow PatientRow
        {
            get
            {
                if (_ctlStartMain.tabCtrlPatientDetail.SelectedTabPageIndex == 0)
                {
                    return _ctlStartMain.LayoutViewPatient.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
                }
                else if (_ctlStartMain.tabCtrlPatientDetail.SelectedTabPageIndex == 1)
                {
                    return _ctlStartMain.PatientDocRow;
                }
                else
                {
                    DataRow FrouROw = _ctlStartMain.layoutViewConfirmPatient.GetFocusedDataRow();
                    if (FrouROw == null)
                        return null;
                    PatientModel.MED_PATIENTSDataTable dtt = new PatientModel.MED_PATIENTSDataTable();
                    var row = dtt.NewMED_PATIENTSRow();
                    row.HEMODIALYSIS_ID = FrouROw["HEMODIALYSIS_ID"].ToString();
                    row.PATIENT_ID = FrouROw["PATIENT_ID"].ToString();
                    row.NAME = FrouROw["NAME"].ToString();
                    row.SEX = FrouROw["SEX"].ToString();
                    row.BIRTHDAY = Utility.CDate(FrouROw["BIRTHDAY"].ToString());
                    row.AGE = Utility.CDecimal(FrouROw["AGE"].ToString());
                    row.NATIVEPLACE = FrouROw["NATIVEPLACE"].ToString();
                    row.JOB = FrouROw["JOB"].ToString();
                    row.MARITAL = FrouROw["MARITAL"].ToString();
                    row.CREDENTIALS_TYPE = FrouROw["CREDENTIALS_TYPE"].ToString();
                    row.CREDENTIALS_NUMBER = FrouROw["CREDENTIALS_NUMBER"].ToString();
                    row.EDUCATION = FrouROw["EDUCATION"].ToString();
                    row.NATION = FrouROw["NATION"].ToString();
                    row.WORK_TELEPHONE = FrouROw["WORK_TELEPHONE"].ToString();
                    row.ADDRESS = FrouROw["ADDRESS"].ToString();
                    row.MEDICAL_TYPE = FrouROw["MEDICAL_TYPE"].ToString();
                    row.TELEPHONE = FrouROw["TELEPHONE"].ToString();
                    row.TIME_TYPE = FrouROw["TIME_TYPE"].ToString();
                    row.SPECIFIC_TIME = Utility.CDate(FrouROw["SPECIFIC_TIME"].ToString());
                    row.ADMISSION_NUMBER = FrouROw["ADMISSION_NUMBER"].ToString();
                    row.IS_NEW = FrouROw["IS_NEW"].ToString();
                    row.WHAT_HOSPITAL_IN = FrouROw["WHAT_HOSPITAL_IN"].ToString();
                    row.WHAT_DEPARTMENT_IN = FrouROw["WHAT_DEPARTMENT_IN"].ToString();
                    row.FIRST_VISIT = FrouROw["FIRST_VISIT"].ToString();
                    row.DIAGNOSE = FrouROw["DIAGNOSE"].ToString();
                    row.LEAVE_HOSPITAL_TIME = Utility.CDate(FrouROw["LEAVE_HOSPITAL_TIME"].ToString());
                    row.INFECTIOUS_CHECK_RESULT = FrouROw["INFECTIOUS_CHECK_RESULT"].ToString();
                    row.INPUT_CODE = FrouROw["INPUT_CODE"].ToString();
                    row.WARD_CODE = FrouROw["WARD_CODE"].ToString();
                    row.BED_NO = FrouROw["BED_NO"].ToString();

                    return row;
                    //_ctlStartMain.layoutViewConfirmPatient.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
                }

                //return this._ctlStartMain.LayoutViewPatient.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            }
        }

        #endregion

        #region 构造函数

        public PatientMgrFrm() {
            this.InitializeComponent();
            InitBar();
            this.barBtn_USER.Caption += string.IsNullOrEmpty(HemoApplicationContext.Current.CurrentUser.USER_NAME) ? "用户" : ":" + HemoApplicationContext.Current.CurrentUser.USER_NAME;
            this.barBtn_DateTime.Caption = DateTime.Today.ToString("yyyy年MM月dd日");
            this.barBtn_IP.Caption = HemoApplicationContext.Current.IpAddress;
            this.barBtn_Version.Caption = HemoApplicationContext.Current.versionAddress;

            this._ctlStartMain = new CtlStartMain();
            this._ctlStartMain.Dock = DockStyle.Fill;
            this.panelControl1.Controls.Add(this._ctlStartMain);
            //  _ctlStartMain.DockGuide.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;

            //if (this._ctlStartMain.TabPatients.SelectedTabPageIndex == 0) 
            //{
            //    barReturn.Enabled = false;
            //}
            //else 
            //{
            //    barReturn.Enabled = true;
            //}
        }

        #endregion

        #region 事件

        /// <summary>
        /// 返回到病人卡片页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barReturn_ItemClick(object sender, ItemClickEventArgs e) {
            this._ctlStartMain.TabPatients.SelectedTabPageIndex = 0;
        }

        /// <summary>
        /// 血管通路
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVascularAccess_ItemClick(object sender, ItemClickEventArgs e) {
            if (this.PatientRow != null) {
                using (EditVascularAccess frmEditVascularAccess = new EditVascularAccess(this.PatientRow.HEMODIALYSIS_ID))
                {
                    frmEditVascularAccess.ShowDialog();
                }
            }
            else {
                AutoClosedMsgBox.ShowForm("请先选择一位患者！", "病患管理", 1000, MessageBoxIcon.Warning);
              
            }
        }

        /// <summary>
        /// 检验记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExam_ItemClick(object sender, ItemClickEventArgs e) {
            if (this.PatientRow != null) {
                using (LabFrm labFrm = new LabFrm(this.PatientRow))
                {
                    labFrm.ShowDialog();
                }
            }
            else {
                AutoClosedMsgBox.ShowForm("请先选择一位患者！", "病患管理", 1000, MessageBoxIcon.Warning);
            }
        }

        private void btnClose_ItemClick(object sender, ItemClickEventArgs e) {
            this.Close();
        }

        /// <summary>
        /// 促红素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnErythropoietin_ItemClick(object sender, ItemClickEventArgs e) {

            if (this.PatientRow != null) {
                using (ErythropoietinFrm erythropoietinFrm = new ErythropoietinFrm(this.PatientRow, false))
                {
                    erythropoietinFrm.ShowDialog();
                }
            }
            else {
                AutoClosedMsgBox.ShowForm("请先选择一位患者！", "病患管理", 1000, MessageBoxIcon.Warning);
            }
        }

        private void PatientMgrFrm_FormClosed(object sender, FormClosedEventArgs e) {

        }

        private void btnPatientAdd_ItemClick(object sender, ItemClickEventArgs e) {
            using (EditPatientNew frmEditPatient = new EditPatientNew())
            {
                frmEditPatient.Current = null;
                if (frmEditPatient.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                {
                    _ctlStartMain.LoadPatientList("全部");
                    _ctlStartMain.InitRecipeConfirmData();

                    //if (MessageBox.Show("是否给此新病人添加病历？", "提醒！", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK) {
                    //    PatientRecord _patientRecord = new PatientRecord();
                    //    _patientRecord.currentHemoID = frmEditPatient.GetAddPatientHemoID;
                    //    _patientRecord.ShowDialog();
                    //}
                }
            }
        }

        private void btnPatientEdit_ItemClick(object sender, ItemClickEventArgs e) {
            using (EditPatientNew frmEditPatient = new EditPatientNew())
            {
                //frmEditPatient.Current = _ctlStartMain.PatientDataTable.FindByHEMODIALYSIS_ID((_ctlStartMain.LayoutViewPatient.GetFocusedDataRow()
                //    as PatientModel.MED_PATIENTSRow).HEMODIALYSIS_ID);
                if (_ctlStartMain.tabCtrlPatientDetail.SelectedTabPageIndex == 0)
                {
                    if (_ctlStartMain.LayoutViewPatient.GetFocusedDataRow() != null)
                    {
                        frmEditPatient.Current = _ctlStartMain.LayoutViewPatient.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
                    }
                }
                else if (_ctlStartMain.tabCtrlPatientDetail.SelectedTabPageIndex == 2)
                {
                    if (_ctlStartMain.layoutViewConfirmPatient.GetFocusedDataRow() != null)
                    {
                        DataRow FrouROw = _ctlStartMain.layoutViewConfirmPatient.GetFocusedDataRow();
                        PatientModel.MED_PATIENTSDataTable dtt = new PatientModel.MED_PATIENTSDataTable();
                        var row = dtt.NewMED_PATIENTSRow();
                        row.HEMODIALYSIS_ID = FrouROw["HEMODIALYSIS_ID"].ToString();
                        row.PATIENT_ID = FrouROw["PATIENT_ID"].ToString();
                        row.NAME = FrouROw["NAME"].ToString();
                        row.SEX = FrouROw["SEX"].ToString();
                        row.BIRTHDAY = Utility.CDate(FrouROw["BIRTHDAY"].ToString());
                        row.AGE = Utility.CDecimal(FrouROw["AGE"].ToString());
                        row.NATIVEPLACE = FrouROw["NATIVEPLACE"].ToString();
                        row.JOB = FrouROw["JOB"].ToString();
                        row.MARITAL = FrouROw["MARITAL"].ToString();
                        row.CREDENTIALS_TYPE = FrouROw["CREDENTIALS_TYPE"].ToString();
                        row.CREDENTIALS_NUMBER = FrouROw["CREDENTIALS_NUMBER"].ToString();
                        row.EDUCATION = FrouROw["EDUCATION"].ToString();
                        row.NATION = FrouROw["NATION"].ToString();
                        row.WORK_TELEPHONE = FrouROw["WORK_TELEPHONE"].ToString();
                        row.ADDRESS = FrouROw["ADDRESS"].ToString();
                        row.MEDICAL_TYPE = FrouROw["MEDICAL_TYPE"].ToString();
                        row.TELEPHONE = FrouROw["TELEPHONE"].ToString();
                        row.TIME_TYPE = FrouROw["TIME_TYPE"].ToString();
                        row.SPECIFIC_TIME = Utility.CDate(FrouROw["SPECIFIC_TIME"].ToString());
                        row.ADMISSION_NUMBER = FrouROw["ADMISSION_NUMBER"].ToString();
                        row.IS_NEW = FrouROw["IS_NEW"].ToString();
                        row.WHAT_HOSPITAL_IN = FrouROw["WHAT_HOSPITAL_IN"].ToString();
                        row.WHAT_DEPARTMENT_IN = FrouROw["WHAT_DEPARTMENT_IN"].ToString();
                        row.FIRST_VISIT = FrouROw["FIRST_VISIT"].ToString();
                        row.DIAGNOSE = FrouROw["DIAGNOSE"].ToString();
                        row.LEAVE_HOSPITAL_TIME = Utility.CDate(FrouROw["LEAVE_HOSPITAL_TIME"].ToString());
                        row.INFECTIOUS_CHECK_RESULT = FrouROw["INFECTIOUS_CHECK_RESULT"].ToString();
                        row.INPUT_CODE = FrouROw["INPUT_CODE"].ToString();
                        row.WARD_CODE = FrouROw["WARD_CODE"].ToString();
                        row.BED_NO = FrouROw["BED_NO"].ToString();
                        frmEditPatient.Current = row;
                    }
                    //frmEditPatient.Current = _ctlStartMain.layoutViewConfirmPatient.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
                }
                if (_ctlStartMain.tabCtrlPatientDetail.SelectedTabPageIndex == 1)
                {
                    if (_ctlStartMain.PatientDocRow != null)
                    {
                        frmEditPatient.Current = _ctlStartMain.PatientDocRow;
                    }
                }

                if (frmEditPatient.Current != null)
                {
                    if (frmEditPatient.ShowDialog() == System.Windows.Forms.DialogResult.Yes) { _ctlStartMain.LoadPatientList("全部");
                    _ctlStartMain.InitRecipeConfirmData();
                    }
                }
                else
                {
                    AutoClosedMsgBox.ShowForm("请先选择一位患者！", "病患管理", 1000, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// 透析处方
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecipe_ItemClick(object sender, ItemClickEventArgs e) {
            if (this.PatientRow != null) {
                using (QueryRecipeList frmEditPrescribe = new QueryRecipeList(this.PatientRow.HEMODIALYSIS_ID, 0))
                {
                    frmEditPrescribe.currentRecipeIdStr = this.PatientRow["ROOMID"].ToString();

                    frmEditPrescribe.ShowDialog();
                    if (frmEditPrescribe.DialogResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        this._ctlStartMain.LoadPatientData();
                        //this.panelControl1.Controls.Remove(this._ctlStartMain);
                        //this._ctlStartMain = new CtlStartMain();
                        //this._ctlStartMain.Dock = DockStyle.Fill;
                        //this.panelControl1.Controls.Add(this._ctlStartMain);
                    }
                }
            }
            else {
                AutoClosedMsgBox.ShowForm("请先选择一位患者！", "病患管理", 1000, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 药品医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOrder_ItemClick(object sender, ItemClickEventArgs e) {
            //OrderSearchFrm orderExecFrm = new OrderSearchFrm(this.PatientRow);
            //orderExecFrm.ShowDialog();

            var row = this.PatientRow;

            if (row != null) {
                using (QueryRecipeList frm = new QueryRecipeList(row.HEMODIALYSIS_ID, 1))
                {
                    frm.currentRecipeIdStr = row["ROOMID"].ToString();

                    frm.ShowDialog();
                }
            }
            else {
                AutoClosedMsgBox.ShowForm("请先选择一位患者！", "病患管理", 1000, MessageBoxIcon.Warning);
            }
        }

        private void barEditItemBanCi_EditValueChanged(object sender, EventArgs e) {
            //CalculateWeek();
        }

        private void PatientMgrFrm_Load(object sender, EventArgs e) {
            //CalculateWeek();
        }

        /// <summary>
        /// 确认排班后的病人处方列表信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barConfimRecipe_ItemClick(object sender, ItemClickEventArgs e) {
            //RecordQueyFrm frm = new RecordQueyFrm();
            //frm.ShowDialog();

            //begin 更新后代码
            var row = this.PatientRow;
            if (row != null) {
                //Old
                using (PatientRecordNew patientRecord = new PatientRecordNew())
                {
                    patientRecord.CurrentHemoId = row.HEMODIALYSIS_ID;
                    patientRecord.ShowDialog();
                }
                //New
                //using (PatientBaseRecord record = new PatientBaseRecord())
                //{
                //    record.HemoId = row.HEMODIALYSIS_ID;
                //    record.ShowDialog();
                //}
            }
            else {
                AutoClosedMsgBox.ShowForm("请先选择一位患者！", "病患管理", 1000, MessageBoxIcon.Warning);
            }

            //PatientScheduleFrm frm = new PatientScheduleFrm();
            //frm.Show();
            //if (_ctlStartMain.GridSource.Rows.Count == 0) {
            //    XtraMessageBox.Show("请先安排病人透析排班信息，再确认处方信息。", "病患管理");
            //    return;
            //}
            //if (barDate.EditValue == null) {
            //    XtraMessageBox.Show("请选择病人透析排班日期，再确认处方信息。", "病患管理");
            //    return;
            //}

            //if (XtraMessageBox.Show("确定确认当前病人列表透析处方吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //    return;

            ////确认确认的处方信息，更新排班表数据
            //DataTable dt = _ctlStartMain.GridSource;
            //string HemoID = string.Empty;
            //DataTable dtTemp = new DataTable();
            //if (dt != null && dt.Rows.Count > 0) {
            //    DateTime startDate = Utility.CDate(barDate.EditValue.ToString());
            //    string strBanCi = string.Empty;
            //    if (barEditItemBanCi.EditValue == null || barEditItemBanCi.EditValue.ToString() == "0") {
            //        strBanCi = string.Empty;
            //    }
            //    else {
            //        strBanCi = barEditItemBanCi.EditValue.ToString();
            //    }
            //    string strSickArea = string.Empty;
            //    if (barSickArea.EditValue == null || barSickArea.EditValue.ToString() == "c570d95c-76a2-4af4-893a-1357065623bf") {
            //        strSickArea = string.Empty;
            //    }
            //    else {
            //        strSickArea = barSickArea.EditValue.ToString();
            //    }

            //    PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable patientScheduleDataTable = _patientScheduleService.GetPatientScheduleByParames(startDate, startDate, strBanCi, strSickArea);

            //    foreach (PatientScheduleModel.MED_PATIENT_SCHEDULERow rowSchedule in patientScheduleDataTable.Rows) {
            //        HemoID = rowSchedule["HEMODIALYSIS_ID"].ToString();
            //        HemodialysisModel.MED_HEMO_RECIPEDataTable recipeTable = this._hemodialysisService.GetRecipeByHemodialysisID(HemoID);
            //        dtTemp = Utility.GetSubTable(recipeTable, "status = 1") as HemodialysisModel.MED_HEMO_RECIPEDataTable;

            //        if (dtTemp != null && dtTemp.Rows.Count > 0) {
            //            rowSchedule["RECIPE_ID"] = dtTemp.Rows[0]["RECIPE_ID"].ToString();
            //            rowSchedule["PURIFIER_MODEL_ID"] = dtTemp.Rows[0]["FIRST_PURIFIER_MODEL"].ToString();
            //        }
            //        else {
            //            XtraMessageBox.Show("病人透析ID:" + HemoID + "的处方尚未确认,请先确认透析处方。", "病患管理");
            //            return;
            //        }
            //    }
            //    int result = _patientScheduleService.SavePatientScheduleInfo(patientScheduleDataTable);
            //    if (result > 0) {
            //        XtraMessageBox.Show("当前病人列表中处方信息已经确认。", "病患管理");
            //    }
            //}
        }

        private void barQuery_ItemClick(object sender, ItemClickEventArgs e) {
            this._ctlStartMain.tabCtrlPatientDetail.SelectedTabPageIndex = 0;
            CalculateWeek();
        }

        private void barLargeButtonItem2_ItemClick(object sender, ItemClickEventArgs e) {

        }

        /// <summary>
        /// 意传参的名字改变了，否则会有冲突
        /// </summary>
        /// <param name="senderr"></param>
        /// <param name="er"></param>
        private void barBtn_ShiftRole_ItemClick(object senderr, ItemClickEventArgs er) {
            LoginScreen frm = new LoginScreen();
            frm.ShiftRoles = HemoApplicationContext.Current.RolesOffices;
            frm.LoginEvent += delegate(object sender, LoginEventArgs e)
            {
                frm.Dispose();
                this.Dispose();
                Program.Show(e.RunApp, e.RunAppNames);

            };
            frm.ShowDialog();

        }

        private void barBtn_ReLogin_ItemClick(object sender, ItemClickEventArgs e) {

            this.Close();
        }

        private void barSchedule_ItemClick(object sender, ItemClickEventArgs e) {
            //EditRecipeConfirmList frm = new EditRecipeConfirmList();
            //frm.ShowDialog();
            this._ctlStartMain.tabCtrlPatientDetail.SelectedTabPageIndex = 2;
            //this._ctlStartMain.InitRecipeConfirmData();


        }

        private void barChangePWD_ItemClick(object sender, ItemClickEventArgs e) {
            ChangPwdFrm frm = new ChangPwdFrm(HemoApplicationContext.Current.CurrentUser.USER_ID);
            frm.ShowDialog();
        }

        private void btnPatientDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            string patient_ID = string.Empty;
            if (_ctlStartMain.LayoutViewPatient.GetFocusedDataRow() != null || this.PatientRow != null)
            {
                if (_ctlStartMain.tabCtrlPatientDetail.SelectedTabPageIndex == 0)
                {
                    patient_ID = (_ctlStartMain.LayoutViewPatient.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow).HEMODIALYSIS_ID;
                }
                else if (_ctlStartMain.tabCtrlPatientDetail.SelectedTabPageIndex == 2)
                {
                    if (this.PatientRow == null)
                    {
                        AutoClosedMsgBox.ShowForm("请先选择一位患者！", "病患管理", 1000, MessageBoxIcon.Warning);

                        return;
                    }
                    patient_ID = this.PatientRow.HEMODIALYSIS_ID;
                }
                if (_ctlStartMain.tabCtrlPatientDetail.SelectedTabPageIndex == 1)
                {
                    patient_ID = _ctlStartMain.PatientDocRow.HEMODIALYSIS_ID;
                }
                //去删除ID
                if (XtraMessageBox.Show("是否确定删除当前患者？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

           
                if (objPatient.DeletePatientByPatient_id(patient_ID) > 0)
                {
                    _ctlStartMain.LoadPatientList("全部");
                    _ctlStartMain.InitRecipeConfirmData();
                }
            }
            else
            {
                AutoClosedMsgBox.ShowForm("请先选择一位患者！", "病患管理", 1000, MessageBoxIcon.Warning);
            }
        }

        private void btnRecordQuey_ItemClick(object sender, ItemClickEventArgs e) {

            this._ctlStartMain.tabCtrlPatientDetail.SelectedTabPageIndex = 0;
            //DataSet ds = new DataSet();
            //CtlMedicalDocument document = new CtlMedicalDocument(ds);
            //document.IsShowGrid(true);
            //_medicalDocContainer.CurrentMedicalDocument = document;
            //documentContainerHost.Child = _medicalDocContainer;
            //dockGuide.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;

        }

        private void ibtnRecordQuey_ItemClick(object sender, ItemClickEventArgs e) {
            using (RecordQueyFrm frm = new RecordQueyFrm())
            {
                frm.ShowDialog();
            }
        }

        private void PatientMgrFrm_FormClosing(object sender, FormClosingEventArgs e) {

            if (XtraMessageBox.Show("您确定退出当前系统吗？", "提示", MessageBoxButtons.OKCancel) !=
                System.Windows.Forms.DialogResult.OK) {

                e.Cancel = true;
            }
            else {
                Program.HideClose = true;
            }
        }

        private void ibarOutputExam_ItemClick(object sender, ItemClickEventArgs e) {
            DateTime startTime = Utility.GetMonday(DateTime.Now).AddDays(0).Date;
            DateTime endTime = startTime.AddDays(6).Date;
            string strBanci = "1";
            if (this.barEditItemBanCi.EditValue != null) {
                strBanci = this.barEditItemBanCi.EditValue.ToString();
            }
            SchedulePatientLabReport frm = new SchedulePatientLabReport(startTime, endTime, strBanci);
            frm.ShowDialog();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e) {
            using (QueryPatientList frm = new QueryPatientList())
            {

                frm.ShowDialog();
            }
        }

        /// <summary>
        /// 常规检验查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommonExamQuery_ItemClick(object sender, ItemClickEventArgs e) {
            using (FrmCommonExamQuery frmQuery = new FrmCommonExamQuery())
            {
                frmQuery.ShowDialog();
            }
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (ReportMainFrm frm = new ReportMainFrm())
            {
                frm.Show();
            }
        }

        private void barLargeButtonItem2_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            if (this.PatientRow != null)
            {
                using (PatientProgressNote frmPatientProgressNote = new PatientProgressNote(this.PatientRow.HEMODIALYSIS_ID))
                {
                    frmPatientProgressNote.ShowDialog();
                }
            }
            else
            {
                AutoClosedMsgBox.ShowForm("请先选择一位患者,然后录入记录！", "病患管理", 1000, MessageBoxIcon.Warning);

            }
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.PatientRow != null)
            {

                using (FollowUpQuery frm = new FollowUpQuery())
                {
                    frm.PatientRow = this.PatientRow;
                    frm.ShowDialog();
                }
            }
            else
            {
                AutoClosedMsgBox.ShowForm("请先选择一位患者,然后录入记录！", "病患管理", 1000, MessageBoxIcon.Warning);

            }
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.PatientRow != null)
            {

                using (PatientCardOperator frm = new PatientCardOperator())
                {
                    frm.currentHemoId = this.PatientRow.HEMODIALYSIS_ID;
                    frm.ShowDialog();
                }
            }
            else
            {
                AutoClosedMsgBox.ShowForm("请先选择一位患者,然后录入记录！", "病患管理", 1000, MessageBoxIcon.Warning);

            }
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.PatientRow != null)
            {
                using (AssessmentListFrm frm = new AssessmentListFrm())
                {
                    frm.patient = this.PatientRow;
                    frm.HemoID = this.PatientRow.HEMODIALYSIS_ID;
                    frm.ShowDialog();
                }
            }
            else
            {
                AutoClosedMsgBox.ShowForm("请先选择一位患者,然后录入记录！", "病患管理", 1000, MessageBoxIcon.Warning);

            }
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (DoctorChangeWork frm = new DoctorChangeWork())
            {
                frm.ShowDialog();
            }
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (QueryEstimateSufficiency frm = new QueryEstimateSufficiency())
            {
                frm.ShowDialog();
            }
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.PatientRow != null)
            {
                PatientDocImageManage frm = new PatientDocImageManage(this.PatientRow);
                frm.ShowDialog();
            }
            else
            {
                XtraMessageBox.Show("请先选择一个病人,然后上传电子扫描件！", "病患管理");
            }
        }

        private void btnFastRecipe_ItemClick(object sender, ItemClickEventArgs e)
        {
            //using (FastRecipeListNew fastRecipe = new FastRecipeListNew())
            //{
            //    DialogResult result = fastRecipe.ShowDialog();
            //    if (result == DialogResult.OK)
            //    {
            //        this._ctlStartMain.LoadPatientData();
            //    }
            //}
        }

        private void btnEasyRecipe_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载工具栏
        /// </summary>
        private void InitBar() {

            ConfigModel.MED_COMMON_ITEMLISTDataTable _banChiDateTable = this._configService.GetConfigList(string.Empty, string.Empty, "班次", "1");


            Hemo.Utilities.Utility.BindLookUpEdit(this.barEditItemBanCi.Edit as RepositoryItemLookUpEdit,
                "ITEM_ID", "ITEM_NAME", _banChiDateTable, "ITEM_NAME", "班次");
            ConfigModel.MED_COMMON_ITEMLISTDataTable config = this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1");
            if (config != null && config.Rows.Count > 0) {
                DataRow SickAreaRow = config.NewRow();
                SickAreaRow["ITEM_NAME"] = "全部";
                SickAreaRow["ITEM_ID"] = "c570d95c-76a2-4af4-893a-1357065623bf";
                SickAreaRow["ORDER_NUMBER"] = 0;
                config.Rows.InsertAt(SickAreaRow, 0);
                Hemo.Utilities.Utility.BindLookUpEdit(this.barSickArea.Edit as RepositoryItemLookUpEdit,
                  "ITEM_ID", "ITEM_NAME", (DataTable)config, "ITEM_NAME", "区域");
                // (this.barSickArea.Edit as RepositoryItemLookUpEdit).NullText = config.Rows[0]["ITEM_NAME"].ToString();
                (this.barSickArea.Edit as RepositoryItemLookUpEdit).NullText = "全部";

            }

            (this.barEditItemBanCi.Edit as RepositoryItemLookUpEdit).NullText = "全部";


            barDate.EditValue = System.DateTime.Now.ToShortDateString();
        }

        /// <summary>
        /// 根据选择条件查询对应的排班病人数据
        /// </summary>
        private void CalculateWeek() {
            busyIndicator1.Visible = true;
            busyIndicator1.ShowLoadingScreenFor(panelControl1);
            DataTable patientDataTable = null;
            using (BackgroundWorker worker = new BackgroundWorker()) {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {

                    if (barDate.EditValue != null) {
                        DateTime startDate = Utility.CDate(barDate.EditValue.ToString());
                        string strBanCi = string.Empty;
                        if (barEditItemBanCi.EditValue == null || barEditItemBanCi.EditValue.ToString() == "0") {
                            strBanCi = string.Empty;
                        }
                        else {
                            strBanCi = barEditItemBanCi.EditValue.ToString();
                        }
                        string strSickArea = string.Empty;
                        if (barSickArea.EditValue == null || barSickArea.EditValue.ToString() == "c570d95c-76a2-4af4-893a-1357065623bf") {
                            strSickArea = string.Empty;
                        }
                        else {
                            strSickArea = barSickArea.EditValue.ToString();
                        }
                        patientDataTable = objPatient.GetPatientListBySchedule(startDate, strSickArea, strBanCi);

                    }
                    else {
                        if (barDate.EditValue == null) {
                            AutoClosedMsgBox.ShowForm("请选择病人透析排班日期后,查询病人列表信息！", "病患管理", 1000, MessageBoxIcon.Warning);

                            return;
                        }
                    }
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    _ctlStartMain.LoadPatientListSchedule(patientDataTable as PatientModel.MED_PATIENTSDataTable);
                    this._ctlStartMain.Dock = DockStyle.Fill;
                    this.panelControl1.Controls.Add(this._ctlStartMain);
                    _ctlStartMain.DockGuide.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                    this.busyIndicator1.HideLoadingScreen();
                };
                worker.RunWorkerAsync();
            }

        }

        private void InvokeLoginEvent(bool runApp, XtraForm ToRunAppNames) {
            if (ShiftEvent != null)
                ShiftEvent(this, new ShiftEventArgs { RunApp = runApp, RunAppNames = ToRunAppNames });
        }

        #endregion 
    }
}