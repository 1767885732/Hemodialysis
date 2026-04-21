/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：主任工作站模块对应窗体类
// 创建时间：2014-07-01
// 创建者：吕志强
//  
// 修改时间：
// 修改人：
// 修改描述：
//
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTab;
using Hemo.Client.Controls;
using Hemo.IService;
using Hemo.IService.Config;
using Hemo.IService.Machine;
using Hemo.IService.PatientSchedule;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.Client.UI.PatientSchedule;
using Hemo.WinForm;
using Hemo.Client.UI.ReportChart;
using Hemo.Client.Core;
using Hemo.Client.UI.User;
using Hemo.Client.UI.Machine;
using Hemo.Client.UI.Hemodialysis;

namespace Hemo.Client.UI.Patient
{
    public partial class PatientTreantmentForDirectorFrm :HemoBaseFrm
    {
        #region 成员变量

        private IConfig _configService = ServiceManager.Instance.ConfigService;

        private IMachine _machineService = ServiceManager.Instance.MachineService;

        private IPatient _patientService = ServiceManager.Instance.PatientService;

        //private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        //private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;

        private CtlUserCureListForDirector ctlUserCureList = new CtlUserCureListForDirector();

        //当前点击的Tab
        //private DevExpress.XtraTab.ViewInfo.BaseTabHitInfo CurrentSelectingTab = null;

        private ConfigModel.MED_COMMON_ITEMLISTDataTable _bedDataTable;

        private ConfigModel.MED_COMMON_ITEMLISTDataTable _purifierDataTable;

        private MachineModel.MED_DIALYSIS_MACHINEDataTable _machineDataTable;

        private PatientModel.MED_PATIENTSDataTable _patientDataTable;

        //private HemodialysisModel.MED_HEMO_RECIPEDataTable _recipeDataTable;

        //private HemodialysisModel.MED_CURE_MAINDataTable _cureMainDatatable;

        //private HemodialysisModel.MED_CURE_DRUGDataTable _cureDrugDatatable;

        private string strDate = string.Empty;

        #endregion

        #region 构造函数

        public PatientTreantmentForDirectorFrm()
        {
            this.InitializeComponent();
        }

        #endregion

        #region 事件

        private void PatientTreantmentFrm_Load(object sender, EventArgs e)
        {
            this.InitializeControls();

            this.LoadPatientTreatMainData();
        }

        private void beiAREA_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadPatientTreatMainData();
        }

        private void beiBANCI_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadPatientTreatMainData();
        }

        private void beiDate_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadPatientTreatMainData();
        }

        /// <summary>
        /// 透析记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Books_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ctlUserCureList.CurrentSelectedCtl == null)
            {
                AutoClosedMsgBox.ShowForm("请先选择病患信息！", "主任工作站", 1000, MessageBoxIcon.Warning);
                return;
            }

            PatientKnowBooks FRM = new PatientKnowBooks();
            FRM.BindDocTree(ctlUserCureList.CurrentSelectedCtl.PatientRow);
            FRM.ShowDialog();
        }

        /// <summary>
        /// 点击开始，根据选中的病人生成一条治疗单记录
        /// </summary>
        /// <param name="pHemoID">病人透析号</param>
        /// <returns></returns>
        //private bool insertCureAllInfoByHemoID(string pHemodialysisID)
        //{
        //    bool result = false;
        //    DataSet ds = new DataSet();
        //    DataTable dtRecipt = _hemodialysisService.GetRecipeByHemodialysisIDAndDate(pHemodialysisID, Utility.CDate(strDate));
        //    int Clean_Up_Times = _hemodialysisService.GetCleanUpTimes(pHemodialysisID);
        //    if (dtRecipt != null && dtRecipt.Rows.Count > 0)
        //    {
        //        //从排班表中确认治疗方式，得到处方ID.
        //        if (dtRecipt.Rows.Count > 1)
        //        {
        //            string strRecipe = _patientScheduleService.GetPatientScheduleRecipeIDByStartTime(pHemodialysisID, Utility.CDate(strDate));
        //            dtRecipt = Utility.GetSubTable(dtRecipt, "RECIPE_ID = '" + strRecipe + "'");
        //        }
        //        if (dtRecipt != null && dtRecipt.Rows.Count > 0)
        //        {
        //            //将处方内容插入到治疗单主表
        //            _cureMainDatatable = new HemodialysisModel.MED_CURE_MAINDataTable();
        //            DataRow drCureMain = _cureMainDatatable.NewRow();
        //            //_cureMainDatatable.Rows.Add(drCureMain);                   
        //            //治疗单编号   
        //            drCureMain["CURE_ID"] = _hemodialysisService.GetNewCureID();
        //            //病人透析号
        //            drCureMain["HEMODIALYSIS_ID"] = pHemodialysisID;
        //            //处方号
        //            drCureMain["RECIPE_ID"] = dtRecipt.Rows[0]["RECIPE_ID"].ToString();
        //            //治疗方式
        //            drCureMain["PURIFICATION_MODE"] = dtRecipt.Rows[0]["PURIFICATION_MODE"].ToString();
        //            //净化时间 
        //            drCureMain["FREQUENCY_HOURS"] = dtRecipt.Rows[0]["FREQUENCY_HOURS"].ToString();
        //            //血管通路
        //            drCureMain["VASCULAR_ACCESS_ID"] = dtRecipt.Rows[0]["VASCULAR_ACCESS_ID"].ToString();
        //            //净化器类型
        //            drCureMain["MACHINE_TYPE"] = dtRecipt.Rows[0]["FIRST_PURIFIER_MODEL"].ToString();
        //            //膜材质
        //            drCureMain["PURIFIER_NAME"] = dtRecipt.Rows[0]["FIRST_PURIFIER_NAME"].ToString();
        //            //面积 
        //            drCureMain["PURIFIER_M2"] = dtRecipt.Rows[0]["FIRST_PURIFIER_M2"].ToString();
        //            //钠离子 
        //            drCureMain["SODION"] = dtRecipt.Rows[0]["SODION"].ToString();
        //            //钾离子
        //            drCureMain["POTASSIUM_ION"] = dtRecipt.Rows[0]["POTASSIUM_ION"].ToString();
        //            //钙离子
        //            drCureMain["CALCIUM_ION"] = dtRecipt.Rows[0]["CALCIUM_ION"].ToString();
        //            //碳酸氢盐
        //            drCureMain["BIRCARBONATE"] = dtRecipt.Rows[0]["BICARBONATE_RADICAL"].ToString();
        //            //治疗单生成时间
        //            drCureMain["CURE_CREATE_DATE"] = Utility.CDate(strDate);
        //            //透析机
        //            drCureMain["MACHINE_ID"] = dtRecipt.Rows[0]["MACHINE_ID"].ToString();
        //            //肝素种类=抗凝剂
        //            drCureMain["HEPARIN_SPECIES"] = dtRecipt.Rows[0]["FIRST_DRUG_NAME"].ToString();
        //            //首剂药品用量
        //            drCureMain["FIRST_HEPARIN"] = Utility.CDecimal(dtRecipt.Rows[0]["FIRST_DRUG_DOSAGE"].ToString());
        //            //血流量
        //            drCureMain["BLOOW_FLOW"] = Utility.CDecimal(dtRecipt.Rows[0]["BLOOW_FLOW"].ToString());
        //            //治疗单状态
        //            drCureMain["CURE_STATUS"] = "3";
        //            //干体重
        //            drCureMain["BEFORE_DRY_WEIGHT"] = Utility.CDecimal(dtRecipt.Rows[0]["DRY_WEIGHT"].ToString());
        //            //责任医生
        //            drCureMain["PRIMARY_DOCTOR"] = dtRecipt.Rows[0]["USER_ID"].ToString();
        //            //超滤率
        //            drCureMain["DRY_WATER_VALUE"] = Utility.CDecimal(dtRecipt.Rows[0]["UFR"].ToString());
        //            //抗凝治疗方法用量及追加药品剂量
        //            drCureMain["HEPARIN_SPECIES"] = dtRecipt.Rows[0]["THERAPEUTIC_METHOD"].ToString();
        //            drCureMain["DOSIS_SUSTENTATIVA"] = Utility.CDecimal(dtRecipt.Rows[0]["SECOND_DRUG_DOSAGE"].ToString());
        //            //首剂药品单位
        //            drCureMain["FIRST_DRUG_UNIT"] = dtRecipt.Rows[0]["FIRST_DRUG_UNIT"].ToString();
        //            //追加药品单位
        //            drCureMain["SECOND_DRUG_UNIT"] = dtRecipt.Rows[0]["SECOND_DRUG_UNIT"].ToString();
        //            //置换液量
        //            drCureMain["FILTRATION_DISPLACEMENT_LIQUID"] = dtRecipt.Rows[0]["DISPLACEMENT_LIQUID"].ToString();
        //            drCureMain["CLEAN_UP_TIMES"] = Clean_Up_Times;
        //            //治疗单状态　
        //            //添加数据到Datatable
        //            _cureMainDatatable.Rows.Add(drCureMain);

        //            ////插入到给药表
        //            //_cureDrugDatatable = new HemodialysisModel.MED_CURE_DRUGDataTable();
        //            //DataRow drDrug = _cureDrugDatatable.NewRow();
        //            ////给药ID
        //            //drDrug["CURE_DRUG_ID"] = System.Guid.NewGuid().ToString();
        //            ////治疗单编号
        //            //drDrug["CURE_ID"] = _cureMainDatatable.Rows[0]["CURE_ID"].ToString();
        //            ////处方编号
        //            //drDrug["RECIPE_ID"] = dtRecipt.Rows[0]["RECIPE_ID"].ToString();
        //            ////药品名称
        //            //drDrug["DRUG_NAME"] = dtRecipt.Rows[0]["FIRST_DRUG_NAME"].ToString();
        //            ////药品单位
        //            //drDrug["DOSAGE_UNITS"] = dtRecipt.Rows[0]["FIRST_DRUG_UNIT"].ToString();
        //            ////药品使用方式
        //            //drDrug["DRUG_MODE"] = dtRecipt.Rows[0]["FIRST_DRUG_MODE"].ToString();
        //            ////药品用量
        //            //drDrug["DOSAGE"] = dtRecipt.Rows[0]["FIRST_DRUG_DOSAGE"].ToString();
        //            ////开药时间，处方中默认的开药
        //            //drDrug["CREATE_DATE"] = dtRecipt.Rows[0]["RECIPE_DATE"].ToString();
        //            ////开方医生
        //            //drDrug["DOCTOR_ID"] = dtRecipt.Rows[0]["USER_ID"].ToString();

        //            //_cureDrugDatatable.Rows.Add(drDrug);
        //        }

        //        if (_cureMainDatatable != null && _cureMainDatatable.Rows.Count > 0)
        //        {
        //            ds.Tables.Add(_cureMainDatatable);
        //        }
        //        if (_cureDrugDatatable != null && _cureDrugDatatable.Rows.Count > 0)
        //        {
        //            ds.Tables.Add(_cureDrugDatatable);
        //        }
        //        //保存治疗单、给药表
        //        if (ds != null && ds.Tables.Count > 0)
        //        {
        //            result = _hemodialysisService.SaveAllCure(ds);
        //        }
        //    }
        //    return result;
        //}

        /// <summary>
        /// 治疗结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btnStop_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    this.SavePatientTreatMainData(this.btnStop.Name);
        //}

        //private void tabPatientToday_CloseButtonClick(object sender, EventArgs e)
        //{
        //    if (CurrentSelectingTab != null)
        //    {
        //        XtraTabPage page = (XtraTabPage)CurrentSelectingTab.Page;
        //        //this.tabPatientToday.TabPages.Remove((XtraTabPage)CurrentSelectingTab.Page);
        //    }
        //}

        //private void tabPatientToday_MouseDown(object sender, MouseEventArgs e)
        //{
        //    //CurrentSelectingTab = tabPatientToday.CalcHitInfo(new Point(e.X, e.Y));
        //}

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 刷新病人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.LoadPatientTreatMainData();
        }

        //private void btbtBegin_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    startCure();
        //}

        //private void btnbStop_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    this.SavePatientTreatMainData(this.btnStop.Name);
        //}

        //private void PatientTreantmentFrm_FormClosed(object sender, FormClosedEventArgs e)
        //{

        //}

        private void barButtonItem5_ItemClick(object senderr, ItemClickEventArgs er)
        {
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

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            ChangPwdFrm frm = new ChangPwdFrm(HemoApplicationContext.Current.CurrentUser.USER_ID);
            frm.ShowDialog();
        }

        private void PatientTreantmentFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (XtraMessageBox.Show("您确定退出当前系统吗？", "提示", MessageBoxButtons.OKCancel) !=
                System.Windows.Forms.DialogResult.OK)
            {

                e.Cancel = true;
            }
            else
            {
                Program.HideClose = true;
            }
        }

        //private void barAir_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    AirPurgeFrm frm = new AirPurgeFrm();
        //    frm.RoomID = this.beiAREA.EditValue.ToString();
        //    frm.ShowDialog();
        //}

        //private void barUse_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    QueryMachineUseRecord frm = new QueryMachineUseRecord();
        //    frm.Tag = beiAREA.EditValue;
        //    frm.ShowDialog();
        //}

        //private void barBtn_UseFee_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    QueryMachineUseFeeFrm frm = new QueryMachineUseFeeFrm();
        //    frm.Tag = beiAREA.EditValue;
        //    frm.ShowDialog();
        //}

        //private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    QueryPrintCureList frm = new QueryPrintCureList();
        //    frm.Show();
        //}

        #endregion

        #region 方法

        private void InitializeControls()
        {
            this.barBtn_User.Caption = string.IsNullOrEmpty(HemoApplicationContext.Current.CurrentUser.USER_NAME) ? "用户" : "当前用户:" + HemoApplicationContext.Current.CurrentUser.USER_NAME;
            this.barBtn_Date.Caption = DateTime.Today.ToString("yyyy年MM月dd日");
            this.barBtn_IP.Caption = HemoApplicationContext.Current.IpAddress;
            this.barBtn_Version.Caption = HemoApplicationContext.Current.versionAddress;

            this._bedDataTable = this._configService.GetConfigList(string.Empty, string.Empty, "床位", "1");
            this._purifierDataTable = this._configService.GetConfigList(string.Empty, string.Empty, "净化器类型", "1");
            this._machineDataTable = this._machineService.GetMachineList();
            this._patientDataTable = this._patientService.GetPatientList();

            //班次
            DataTable dtBANCI = new DataTable();
            dtBANCI.Columns.Add(new DataColumn("ITEM_ID"));
            dtBANCI.Columns.Add(new DataColumn("ITEM_NAME"));

            DataRow row = dtBANCI.NewRow();
            row["ITEM_ID"] = "1";
            row["ITEM_NAME"] = "上午";
            dtBANCI.Rows.Add(row);

            row = dtBANCI.NewRow();
            row["ITEM_ID"] = "2";
            row["ITEM_NAME"] = "下午";
            dtBANCI.Rows.Add(row);

            //row = dtBANCI.NewRow();
            //row["ITEM_ID"] = "3";
            //row["ITEM_NAME"] = "晚班";
            //dtBANCI.Rows.Add(row);

            row = dtBANCI.NewRow();
            row["ITEM_ID"] = "4";
            row["ITEM_NAME"] = "急诊";
            dtBANCI.Rows.Add(row);

            Utility.BindLookUpEdit(this.beiBANCI.Edit as RepositoryItemLookUpEdit, "ITEM_ID", "ITEM_NAME", dtBANCI, "ITEM_NAME", "班次");

            this.beiBANCI.EditValue = "1";

            this.beiBANCI.EditValueChanged += new EventHandler(beiBANCI_EditValueChanged);

            //病室
            DataTable dtAREA = this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1");

            Utility.BindLookUpEdit(this.beiAREA.Edit as RepositoryItemLookUpEdit, "ITEM_ID", "ITEM_NAME", dtAREA, "ITEM_NAME", "病室");


            string areaStr = System.Configuration.ConfigurationManager.AppSettings["CurrentOffice"].ToString();
            if (string.IsNullOrEmpty(areaStr))
            {
                if (dtAREA.Rows.Count > 0)
                    this.beiAREA.EditValue = dtAREA.Rows[0]["ITEM_ID"].ToString();
            }
            else
            {
                this.beiAREA.EditValue = areaStr;
            }

            this.beiAREA.EditValueChanged += new EventHandler(beiAREA_EditValueChanged);

            //签到日期
            (this.beiDate.Edit as RepositoryItemDateEdit).NullText = DateTime.Now.ToString("yyyy/MM/dd");

            this.beiDate.EditValueChanged += new EventHandler(beiDate_EditValueChanged);

            //设置填充区域样式
            //tabPatientToday.Dock = DockStyle.Fill;
        }

        private void LoadPatientTreatMainData()
        {
            DateTime dt = this.beiDate.EditValue == null ? DateTime.Now.Date : Utility.CDate(this.beiDate.EditValue.ToString()).Date;

            ctlUserCureList.TreamentDate = dt.ToString();
            ctlUserCureList.Classes = beiBANCI.EditValue.ToString();
            ctlUserCureList.Area = beiAREA.EditValue.ToString();
            ctlUserCureList.Dock = DockStyle.Fill;

            ctlUserCureList.LoadPatientTreatMainData();

            pnlPatientTreat.Controls.Add(ctlUserCureList);
        }

        //private void SavePatientTreatMainData(string btnName)
        //{
        //    if (ctlUserCureList.CurrentSelectedCtl == null)
        //    {
        //        XtraMessageBox.Show("请先选择数据，再开始治疗！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

        //        return;
        //    }

        //    if (beiDate.EditValue == null)
        //        this.strDate = Utility.CDate((beiDate.Edit).NullText).ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ":" + DateTime.Now.Second;
        //    else
        //        this.strDate = Utility.CDate(beiDate.EditValue.ToString()).ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ":" + DateTime.Now.Second;

        //    switch (btnName.ToLower())
        //    {
        //        case "btnbegin": //开始治疗
        //            if (!this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.IsEND_TIMENull())
        //            {
        //                XtraMessageBox.Show("已经治疗结束，无法再次开始治疗！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return;
        //            }

        //            if (!ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.IsSTART_TIMENull())
        //            {
        //                XtraMessageBox.Show("已经开始治疗，无法重复执行！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return;
        //            }

        //            if (XtraMessageBox.Show("确定开始治疗吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
        //                return;

        //            this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.START_TIME = Utility.CDate(strDate); //DateTime.Now;
        //            break;

        //        case "btnstop": //治疗结束
        //            if (!this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.IsEND_TIMENull())
        //            {
        //                XtraMessageBox.Show("已经治疗结束，无法重复执行！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return;
        //            }
        //            if (this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.IsSTART_TIMENull())
        //            {
        //                XtraMessageBox.Show("没有开始治疗，无法结束！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return;
        //            }
        //            if (XtraMessageBox.Show("确定结束治疗吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
        //                return;

        //            this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.END_TIME = Utility.CDate(strDate); //DateTime.Now;

        //            //保存系统消息为已读
        //            this._hemodialysisService.SaveMsgInfoToMarkRead(this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.PATIENT_SCHEDULE_ID);
        //            break;

        //        default:
        //            break;
        //    }

        //    this._patientScheduleService.SavePatientScheduleInfo(this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.Table as PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable);

        //    this.ctlUserCureList.CurrentSelectedCtl.SetTimeInfo();

        //    XtraMessageBox.Show("操作成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}

        /// <summary>
        /// 开始治疗
        /// </summary>
        //private void startCure()
        //{
        //    if (ctlUserCureList.CurrentSelectedCtl == null)
        //    {
        //        XtraMessageBox.Show("请先选择数据，再开始治疗！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    if (ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.IsRECIPE_IDNull())
        //    {
        //        XtraMessageBox.Show("该病人尚没有确认透析处方，不能进行治疗。", "主任工作站");
        //        return;
        //    }

        //    string strHemoID = ctlUserCureList.CurrentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
        //    DateTime startDate = new DateTime();
        //    if (beiDate.EditValue != null)
        //    {
        //        startDate = Utility.CDate(beiDate.EditValue.ToString());
        //    }
        //    else
        //    {
        //        startDate = System.DateTime.Now;
        //    }
        //    //2014-03-05 刘超 修改将处方日期通过窗体传入，之前使用的为默认值sysdate，只传入透析号。
        //    /// HemodialysisModel.MED_HEMO_RECIPEDataTable recipeTable = this._hemodialysisService.GetRecipeByHemodialysisID(strHemoID);
        //    HemodialysisModel.MED_HEMO_RECIPEDataTable recipeTable = this._hemodialysisService.GetRecipeByHemodialysisIDAndDate(strHemoID, startDate);
        //    //recipeTable = Utility.GetSubTable(recipeTable, "status = 1") as HemodialysisModel.MED_HEMO_RECIPEDataTable;

        //    //if (recipeTable.Rows.Count == 0) {
        //    //    XtraMessageBox.Show("该病人尚未确认透析处方，不能进行治疗。", "主任工作站");
        //    //    return;
        //    //}

        //    if (recipeTable.Rows.Count > 1)
        //    {
        //        XtraMessageBox.Show("该病人当天有多张处方，请确认只有一张有效的处方。", "主任工作站");
        //        return;
        //    }

        //    this.SavePatientTreatMainData(this.btnBegin.Name);

        //    //在开始的同时，添加一条治疗单记录
        //    bool result = false;
        //    int iCureCount = 0;

        //    if (ctlUserCureList.CurrentSelectedCtl != null)
        //    {
        //        if (ctlUserCureList.CurrentSelectedCtl != null && ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow != null)
        //        {
        //            //if ((beiDate.Edit).NullText.Length == 0) {
        //            //    strDate = beiDate.EditValue.ToString();
        //            //}
        //            //else {
        //            //    strDate = (beiDate.Edit).NullText;
        //            //}

        //            //if (beiDate.EditValue.ToString().Length == 0) {
        //            //    strDate = Utility.CDate((beiDate.Edit).NullText) + " " + System.DateTime.Now.ToShortTimeString();
        //            if (ctlUserCureList.CurrentSelectedCtl != null && ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow != null)
        //            {
        //                iCureCount = _hemodialysisService.GetMainCureCountByCreateDate(strHemoID, Utility.CDate(strDate));
        //                if (iCureCount == 0)
        //                {
        //                    result = insertCureAllInfoByHemoID(strHemoID);
        //                    ctlUserCureList.LoadPatientCure(ctlUserCureList.CurrentSelectedCtl);
        //                }
        //            }
        //        }
        //    }
        //}

        #endregion
    }
}
