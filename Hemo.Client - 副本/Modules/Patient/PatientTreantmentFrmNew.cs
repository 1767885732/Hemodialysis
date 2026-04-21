/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:增加窗体控件值的方法
 * 创建标识:吕志强-2016年12月24日
 * 
 * 修改时间:2017年5月11日
 * 修改人:顾伟伟
 * 修改描述:用户控件
 * 
 * 修改时间:2017年6月12日
 * 修改人:刘超
 * 修改描述:修改对外公开的方法
 * 
 * 修改时间:2017年9月28日
 * 修改人:吕志强
 * 修改描述:添加CRRT班次切换事件
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Config;
using Hemo.IService.Machine;
using Hemo.IService;
using Hemo.Service;
using Hemo.IService.PatientSchedule;
using Hemo.Client.Controls;
using Hemo.Model;
using DevExpress.XtraBars;
using Hemo.Client.Controls.Treatment;
using Hemo.Utilities;
using Hemo.Client.UI.Hemodialysis;
using Hemo.Client.UI.ReportChart;
using Hemo.Client.UI.Patient;
using Hemo.Client.Core;
using Hemo.WinForm;
using Hemo.Client.UI.User;
using Hemo.Client.UI.Machine;
using Hemo.Client.UI.PatientSchedule;
using Hemo.Client.Print;
using DevExpress.XtraReports.UI;
using Hemo.Client.UI.Drug;
using Hemo.Client.UI.Config;
using Hemo.Client.UI.Assessment;
using Hemo.Client.UI.Material;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTab;
using DevExpress.XtraBars.Docking2010.Customization;
using Hemo.Client.Base;

namespace Hemo.Client.Modules
{
    public partial class PatientTreantmentFrmNew : DevExpress.XtraEditors.XtraUserControl
    {
        #region 成员变量

        private IConfig _configService = ServiceManager.Instance.ConfigService;

        private IMachine _machineService = ServiceManager.Instance.MachineService;

        private IPatient _patientService = ServiceManager.Instance.PatientService;

        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;


        private CtlUserCureList ctlUserCureList = new CtlUserCureList();

        /// <summary>
        /// 当前点击的Tab
        /// </summary>
        private DevExpress.XtraTab.ViewInfo.BaseTabHitInfo CurrentSelectingTab = null;

        private ConfigModel.MED_COMMON_ITEMLISTDataTable _bedDataTable;

        private ConfigModel.MED_COMMON_ITEMLISTDataTable _purifierDataTable;

        private MachineModel.MED_DIALYSIS_MACHINEDataTable _machineDataTable;

        private PatientModel.MED_PATIENTSDataTable _patientDataTable;

        private HemodialysisModel.MED_HEMO_RECIPEDataTable _recipeDataTable;

        private HemodialysisModel.MED_CURE_MAINDataTable _cureMainDatatable;

        private HemodialysisModel.MED_CURE_DRUGDataTable _cureDrugDatatable;

        private string strDate = string.Empty;

        #endregion

        #region 构造函数

        public PatientTreantmentFrmNew()
        {
            this.InitializeComponent();
            ctlUserCureList.DoubleClick += new CtlUserCureList.DoubleClickEventHandler(ctlUserCureList_DoubleClick);
        }

        #endregion

        #region 方法

        private void InitializeControls()
        {
            this.barBtn_User.Caption = string.IsNullOrEmpty(HemoApplicationContext.Current.CurrentUser.USER_NAME) ? "用户" : "当前用户:" + HemoApplicationContext.Current.CurrentUser.USER_NAME;
            this.barBtn_Date.Caption = DateTime.Today.ToString("yyyy年MM月dd日");
            this.barBtn_IP.Caption = HemoApplicationContext.Current.IpAddress; ;
            this._bedDataTable = this._configService.GetConfigList(string.Empty, string.Empty, "床位", "1");
            this._purifierDataTable = this._configService.GetConfigList(string.Empty, string.Empty, "净化器类型", "1");
            this._machineDataTable = this._machineService.GetMachineList();

            BindArea();
            BindBanci(this.beiAREA.Edit.GetDisplayText(this.beiAREA.EditValue));

            //签到日期
            (this.beiDate.Edit as RepositoryItemDateEdit).NullText = DateTime.Now.ToString("yyyy/MM/dd");
            beiDate.EditValue = DateTime.Now.ToString("yyyy/MM/dd");
            this.beiDate.EditValueChanged += new EventHandler(beiDate_EditValueChanged);

            //设置填充区域样式
            //tabPatientToday.Dock = DockStyle.Fill;
        }

        private void LoadPatientTreatMainData()
        {
            try
            {
                DateTime dt = this.beiDate.EditValue == null ? DateTime.Now.Date : Utility.CDate(this.beiDate.EditValue.ToString()).Date;
                ctlUserCureList.TreamentDate = dt.ToString();
                ctlUserCureList.Classes = beiBANCI.EditValue.ToString();
                ctlUserCureList.Area = beiAREA.EditValue.ToString();
                ctlUserCureList.AreaName = GetAreaName(beiAREA.Edit.GetDisplayText(beiAREA.EditValue.ToString()));
                ctlUserCureList.Dock = DockStyle.Fill;

                ctlUserCureList.LoadPatientTreatMainData();
                pnlPatientTreat.Controls.Add(ctlUserCureList);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
                Logger.WriteErrorLogContet(string.Format("LoadPatientTreatMainData:{0}", ex.InnerException.StackTrace + "\n" + ex.InnerException.Message));
                throw;
            }            
        }

        private void ShowBillFrm(CtlTreatmentPerson patientCtl)
        {
            var frm = new PatientBillRecordFrm();
            frm.PatientTreatmentInfo = patientCtl;
            frm.ShowDialog();

        }

        private bool SavePatientTreatMainData(string btnName)
        {
            if (ctlUserCureList.CurrentSelectedCtl == null)
            {
                AutoClosedMsgBox.ShowForm("请先选择数据，再开始治疗！", "系统提示", 1500, MessageBoxIcon.Warning);
                return false;
            }

            if (beiDate.EditValue == null)
            {
                this.strDate = Utility.CDate((beiDate.Edit).NullText).ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ":" + DateTime.Now.Second;
            }
            else
            {
                this.strDate = Utility.CDate(beiDate.EditValue.ToString()).ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ":" + DateTime.Now.Second;
            }

            switch (btnName.ToLower())
            {
                case "btnbegin": //开始治疗
                    if (!this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.IsEND_TIMENull())
                    {
                        AutoClosedMsgBox.ShowForm("已经开始治疗，无法再次开始治疗！", "系统提示", 1500, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (!ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.IsSTART_TIMENull())
                    {
                        AutoClosedMsgBox.ShowForm("已经开始治疗，无法重复执行！", "系统提示", 1500, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (XtraMessageBox.Show("确定开始治疗吗？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return false;
                    this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.RECIPE_ID = saveRecipe;
                    this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.START_TIME = Utility.CDate(strDate); //DateTime.Now;
                    break;

                case "btnstop": //治疗结束
                    if (this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.IsSTART_TIMENull())
                    {
                        AutoClosedMsgBox.ShowForm("没有开始治疗，无法结束！", "系统提示", 1500, MessageBoxIcon.Warning);
                        return false;
                    }
                    ConfigModel.MED_COMMON_ITEMLISTDataTable config = this._configService.GetConfigList(string.Empty, string.Empty, "系统参数", "1");

                    var dt = this._patientScheduleService.GetCureBillByCureID(this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.RECIPE_ID);
                    if (dt.Rows.Count == 0 && config.Count > 0)
                    {
                        AutoClosedMsgBox.ShowForm("患者未记账,请先记账！", "系统提示", 1500, MessageBoxIcon.Warning);
                        return false;
                    }
                    if (!this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.IsEND_TIMENull())
                    {
                        AutoClosedMsgBox.ShowForm("已经治疗结束，无法重复执行！", "系统提示", 1500, MessageBoxIcon.Warning);
                        return false;
                    }
                    var UnExcuteDt = _hemodialysisService.GetUnExcuteCureDrugByHemoRecipeId(this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.HEMODIALYSIS_ID, this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.RECIPE_ID);
                    if (UnExcuteDt != null && UnExcuteDt.Rows.Count > 0)
                    {
                        AutoClosedMsgBox.ShowForm("有未执行的医嘱，执行医嘱后才可以结束治疗！", "系统提示", 1500, MessageBoxIcon.Warning);
                        return false;
                    }
                    string area = this.beiAREA.Edit.GetDisplayText(this.beiAREA.EditValue);
                    DateTime finishTime = Utility.CDate(strDate);
                    if (area.Equals("CRRT"))
                    {
                        InputTimeFrm inputTimeFrm = new InputTimeFrm();
                        inputTimeFrm.StartTime = this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.START_TIME;
                        inputTimeFrm.EndTime = finishTime;

                        if (inputTimeFrm.ShowDialog(this) == DialogResult.Yes)
                        {
                            finishTime = inputTimeFrm.EndTime;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (XtraMessageBox.Show("确定结束治疗吗？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return false;
                    }

                    this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.END_TIME = finishTime;//Utility.CDate(strDate); //DateTime.Now;
                    //出库
                    HemoDWHApplication hemodwApp = new HemoDWHApplication();
                    hemodwApp.ConfirmHemoDWApplyOut(this.ctlUserCureList.CurrentSelectedCtl.CureMainRow.RECIPE_ID, this.ctlUserCureList.CurrentSelectedCtl.CureMainRow.CURE_ID);
                    //保存系统消息为已读
                    this._hemodialysisService.SaveMsgInfoToMarkRead(this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.PATIENT_SCHEDULE_ID);
                    break;
                case "btnback": //退回治疗
                    if (this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.IsSTART_TIMENull())
                    {
                        AutoClosedMsgBox.ShowForm("没有开始治疗，无法退回！", "系统提示", 1500, MessageBoxIcon.Warning);
                        return false;
                    }
                    if (!this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.IsEND_TIMENull())
                    {
                        if (XtraMessageBox.Show("是否取消结束治疗退回到开始治疗状态？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                            return false;
                        this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.SetEND_TIMENull();
                    }
                    else
                    {
                        if (XtraMessageBox.Show("取消治疗可能会导致治疗单数据丢失，是否确定取消？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                            return false;
                        //this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.START_TIME = DBNull.Value;
                        #region 取消治疗的数据操作

                        this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.SetSTART_TIMENull();
                        this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.SetEND_TIMENull();
                        //this.ctlUserCureList.LoadPatientCure(this.ctlUserCureList.CurrentSelectedCtl);
                        var cureMainData = _hemodialysisService.GetMainCureByRecipeId(this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.RECIPE_ID);
                        if (cureMainData != null && cureMainData.Rows.Count > 0)
                        {
                            for (int i = 0; i < cureMainData.Rows.Count; i++)
                            {
                                cureMainData[i].RECIPE_ID = cureMainData[i].RECIPE_ID + "back";
                                cureMainData[i].CURE_STATUS = "4";
                                _hemodialysisService.SaveCureMain(cureMainData);
                            }
                        }
                    }
                        #endregion
                    break;
                default:
                    break;
            }

            this._patientScheduleService.SavePatientScheduleInfo(this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.Table as PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable);

            this.ctlUserCureList.CurrentSelectedCtl.SetTimeInfo();
            this.ctlUserCureList.CurrentSelectedCtl.SetSelectedEffect();

            //this.ctlUserCureList.LoadPatientCure(this.ctlUserCureList.CurrentSelectedCtl);

            AutoClosedMsgBox.ShowForm("操作成功!", "系统提示", 1500, MessageBoxIcon.Information);
            return true;
        }

        private string saveRecipe = string.Empty;
        /// <summary>
        /// 开始治疗
        /// </summary>
        private void startCure()
        {
            if (ctlUserCureList.CurrentSelectedCtl == null)
            {
                AutoClosedMsgBox.ShowForm("请先选择数据，再开始治疗！", "系统提示", 1500, MessageBoxIcon.Warning);
                return;
            }

            if (ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.IsRECIPE_IDNull())
            {
                AutoClosedMsgBox.ShowForm("该病人尚没有确认透析处方，不能进行治疗。", "系统提示", 1500, MessageBoxIcon.Warning);
                return;
            }

            string strHemoID = ctlUserCureList.CurrentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
            DateTime startDate = new DateTime();
            if (beiDate.EditValue != null)
            {
                startDate = Utility.CDate(beiDate.EditValue.ToString());
            }
            else
            {
                startDate = System.DateTime.Now;
            }

            HemodialysisModel.MED_HEMO_RECIPEDataTable recipeTable = this._hemodialysisService.GetRecipeByHemodialysisIDAndDate(strHemoID, startDate);

            if (recipeTable.Rows.Count > 1)
            {
                AutoClosedMsgBox.ShowForm("该病人当天有多张处方，请确认只有一张有效的处方！", "系统提示", 1500, MessageBoxIcon.Warning);
                return;
            }

            //CRRT患者检查是否提前开始治疗
            string area = this.beiAREA.Edit.GetDisplayText(this.beiAREA.EditValue);
            if (area.Equals("CRRT"))
            {
                string name = ctlUserCureList.CurrentSelectedCtl.PatientRow.NAME;
                string banci = ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.BANCI_ID;
                if (banci.Equals("1"))
                {
                    if (DateTime.Now.Hour < 8)
                    {
                        AutoClosedMsgBox.ShowForm(name + "安排在白天班次，不允许提前开始治疗！", "系统提示", 1500, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (banci.Equals("2"))
                {
                    if (DateTime.Now.Hour < 16)
                    {
                        AutoClosedMsgBox.ShowForm(name + "安排在小夜班次，不允许提前开始治疗！", "系统提示", 1500, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (banci.Equals("3"))
                {
                    int day = ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.DIALYSIS_DATE.Day;
                    if (DateTime.Now.Day <= day)
                    {
                        AutoClosedMsgBox.ShowForm(name + "安排在大夜班次，不允许提前开始治疗！", "系统提示", 1500, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            if (recipeTable != null)
            {
                saveRecipe = recipeTable[0].RECIPE_ID;
                this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.RECIPE_ID = recipeTable[0].RECIPE_ID;
            }

            #region //开始治疗时对患者费用和警戒线的判断
            //decimal cost = _patientService.GetHemoAccountCostByHemoId(strHemoID);
            //if (cost < 0) {
            //    if (XtraMessageBox.Show("费用不足或低于警戒线的设定，是否继续治疗？", "系统提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No) {
            //        return;
            //    }
            //}
            #endregion

            bool result = SavePatientTreatMainData(this.btnBegin.Name);
            if (result == false) { return; }
            this.ctlUserCureList.LoadPatientCure(this.ctlUserCureList.CurrentSelectedCtl);
            //在开始的同时，添加一条治疗单记录
            int iCureCount = 0;

            if (ctlUserCureList.CurrentSelectedCtl != null)
            {
                if (ctlUserCureList.CurrentSelectedCtl != null && ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow != null)
                {
                    if (ctlUserCureList.CurrentSelectedCtl != null && ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow != null)
                    {
                        iCureCount = _hemodialysisService.GetMainCureCountByCreateDate(strHemoID, Utility.CDate(strDate));
                        if (iCureCount == 0)
                        {
                            result = insertCureAllInfoByHemoID(strHemoID, false);
                            //开始治疗班次设为排班记录对应班次
                            ctlUserCureList.Classes = ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.BANCI_ID;
                            ctlUserCureList.DoTabPageFreash();
                            //透析单加载后再设为当前显示班次
                            ctlUserCureList.Classes = this.beiBANCI.EditValue.ToString();
                        }

                        var _cureMainDataTable = this._hemodialysisService.GetMainCureByRecipeId(this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.RECIPE_ID);
                        if (_cureMainDataTable != null && _cureMainDataTable.Rows.Count > 0)
                        {
                            var _cureMainRow = _cureMainDataTable.Rows[0] as HemodialysisModel.MED_CURE_MAINRow;
                            this.ctlUserCureList.CurrentSelectedCtl.SetCureMain(_cureMainRow);
                        }
                    }
                }
            }
        }

        private string GetAreaName(string areaName)
        {
            switch (areaName)
            {
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

        /// <summary>
        /// 绑定病区
        /// </summary>
        private void BindArea()
        {
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
                dtBanci = this._configService.GetConfigList(string.Empty, string.Empty, "班次", "1");
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

            string currentBanci = this.beiBANCI.EditValue != null ? this.beiBANCI.EditValue.ToString() : "1";
            Utility.BindLookUpEdit(this.beiBANCI.Edit as RepositoryItemLookUpEdit, "ITEM_VALUE", "ITEM_NAME", dtBanci, "ITEM_NAME", "班次");
            this.beiBANCI.EditValue = areaName.Equals("CRRT") ? "1" : currentBanci;
        }

        #endregion

        #region 事件

        private void PatientTreantmentFrmNew_Load(object sender, EventArgs e)
        {
            this.InitializeControls();

            this.LoadPatientTreatMainData();
            //barButtonItem36.Visibility = BarItemVisibility.Never;
        }

        /// <summary>
        /// 病室改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void beiAREA_EditValueChanged(object sender, EventArgs e)
        {
            BindBanci(this.beiAREA.Edit.GetDisplayText(this.beiAREA.EditValue));
            this.LoadPatientTreatMainData();
        }

        private void beiBANCI_EditValueChanged(object sender, EventArgs e)
        {
            string area = this.beiAREA.Edit.GetDisplayText(this.beiAREA.EditValue);
            if (area.Equals("CRRT"))
            {
                BanciChangedEventArgs args = new BanciChangedEventArgs();
                args.Banci = this.beiBANCI.EditValue.ToString();
                this.ctlUserCureList.DoBanciChanged(sender, args);
            }
            else
            {
                this.LoadPatientTreatMainData();
            }
        }

        private void beiDate_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadPatientTreatMainData();
        }

        /// <summary>
        /// 开始治疗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBegin_ItemClick(object sender, ItemClickEventArgs e)
        {
            startCure();
        }

        /// <summary>
        /// 点击开始，根据选中的病人生成一条治疗单记录
        /// </summary>
        /// <param name="pHemoID">病人透析号</param>
        /// <param name="IsAddCure">是否加透</param>
        /// <returns></returns>
        string tmpCureID = string.Empty;
        private bool insertCureAllInfoByHemoID(string pHemodialysisID, bool IsAddCure)
        {
            bool result = false;
            DataSet ds = new DataSet();
            DataTable dtPatient = _patientService.GetPatientListByParams(string.Empty, pHemodialysisID);
            DataTable dtRecipt = _hemodialysisService.GetRecipeByHemodialysisIDAndDate(pHemodialysisID, Utility.CDate(strDate));
            int Clean_Up_Times = _hemodialysisService.GetCleanUpTimes(pHemodialysisID);
            string checkResult = (dtPatient != null && dtPatient.Rows.Count > 0) ? dtPatient.Rows[0]["INFECTIOUS_CHECK_RESULT"].ToString() : string.Empty;
            string department = (dtPatient != null && dtPatient.Rows.Count > 0) ? dtPatient.Rows[0]["WHAT_DEPARTMENT_IN"].ToString() : string.Empty;
            if (dtRecipt != null && dtRecipt.Rows.Count > 0)
            {
                //从排班表中确认治疗方式，得到处方ID.
                if (dtRecipt.Rows.Count > 1)
                {
                    string strRecipe = _patientScheduleService.GetPatientScheduleRecipeIDByStartTime(pHemodialysisID, Utility.CDate(strDate));
                    dtRecipt = Utility.GetSubTable(dtRecipt, "RECIPE_ID = '" + strRecipe + "'");
                }
                if (dtRecipt != null && dtRecipt.Rows.Count > 0)
                {
                    //将处方内容插入到治疗单主表
                    _cureMainDatatable = new HemodialysisModel.MED_CURE_MAINDataTable();
                    DataRow drCureMain = _cureMainDatatable.NewRow();
                    //治疗单编号   
                    drCureMain["CURE_ID"] = _hemodialysisService.GetNewCureID();
                    //病人透析号
                    drCureMain["HEMODIALYSIS_ID"] = pHemodialysisID;
                    //处方号
                    drCureMain["RECIPE_ID"] = dtRecipt.Rows[0]["RECIPE_ID"].ToString();
                    drCureMain["RECIPE_TYPE"] = IsAddCure ? "1" : "0";

                    tmpCureID = IsAddCure ? drCureMain["CURE_ID"].ToString() : string.Empty;

                    //治疗方式
                    drCureMain["PURIFICATION_MODE"] = (dtRecipt.Rows[0]["PURIFICATION_MODE"] == null || dtRecipt.Rows[0]["PURIFICATION_MODE"] == DBNull.Value) ? "9c01f053-ad09-4873-b68f-b96d03b8572f" : dtRecipt.Rows[0]["PURIFICATION_MODE"].ToString();
                    //净化时间 
                    drCureMain["FREQUENCY_HOURS"] = dtRecipt.Rows[0]["FREQUENCY_HOURS"].ToString();
                    //血管通路
                    drCureMain["VASCULAR_ACCESS_ID"] = dtRecipt.Rows[0]["VASCULAR_ACCESS_ID"].ToString();
                    //净化器类型
                    drCureMain["MACHINE_TYPE"] = dtRecipt.Rows[0]["FIRST_PURIFIER_MODEL"].ToString();
                    //膜材质
                    drCureMain["PURIFIER_NAME"] = dtRecipt.Rows[0]["FIRST_PURIFIER_NAME"].ToString();
                    //面积 
                    drCureMain["PURIFIER_M2"] = dtRecipt.Rows[0]["FIRST_PURIFIER_M2"].ToString();
                    //钠离子 
                    drCureMain["SODION"] = dtRecipt.Rows[0]["SODION"].ToString();
                    //钾离子
                    drCureMain["POTASSIUM_ION"] = dtRecipt.Rows[0]["POTASSIUM_ION"].ToString();
                    //钙离子
                    drCureMain["CALCIUM_ION"] = dtRecipt.Rows[0]["CALCIUM_ION"].ToString();
                    //碳酸氢盐
                    drCureMain["BIRCARBONATE"] = dtRecipt.Rows[0]["BICARBONATE_RADICAL"].ToString();
                    //治疗单生成时间
                    drCureMain["CURE_CREATE_DATE"] = Utility.CDate(strDate);
                    //透析机
                    drCureMain["MACHINE_ID"] = dtRecipt.Rows[0]["MACHINE_ID"].ToString();
                    //抗凝治疗方法
                    drCureMain["HEPARIN_SPECIES"] = dtRecipt.Rows[0]["THERAPEUTIC_METHOD"].ToString();
                    //首剂药品用量
                    drCureMain["FIRST_HEPARIN"] = Utility.CDecimal(dtRecipt.Rows[0]["FIRST_DRUG_DOSAGE"].ToString());
                    //追加药品用量
                    drCureMain["DOSIS_SUSTENTATIVA"] = Utility.CDecimal(dtRecipt.Rows[0]["SECOND_DRUG_DOSAGE"].ToString());
                    //首剂药品单位
                    drCureMain["FIRST_DRUG_UNIT"] = dtRecipt.Rows[0]["FIRST_DRUG_UNIT"].ToString();
                    //追加药品单位
                    drCureMain["SECOND_DRUG_UNIT"] = dtRecipt.Rows[0]["SECOND_DRUG_UNIT"].ToString();
                    //血流量
                    drCureMain["BLOOW_FLOW"] = Utility.CDecimal(dtRecipt.Rows[0]["BLOOW_FLOW"].ToString());
                    //治疗单状态
                    drCureMain["CURE_STATUS"] = "3";
                    //责任医生
                    drCureMain["PRIMARY_DOCTOR"] = dtRecipt.Rows[0]["USER_ID"].ToString();
                    //预计脱水
                    drCureMain["UFR"] = Utility.CDecimal(dtRecipt.Rows[0]["UFR"].ToString());
                    //置换液量
                    drCureMain["FILTRATION_DISPLACEMENT_LIQUID"] = dtRecipt.Rows[0]["DISPLACEMENT_LIQUID"].ToString();
                    //净化次数
                    drCureMain["CLEAN_UP_TIMES"] = Clean_Up_Times;
                    //透析液流量
                    drCureMain["DIALYSATE_FLOW"] = dtRecipt.Rows[0]["DIALYSATE_FLOW"].ToString();
                    //干体重
                    drCureMain["DRY_WEIGHT"] = dtRecipt.Rows[0]["DRY_WEIGHT"].ToString();
                    //透前体重
                    drCureMain["BEFORE_DRY_WEIGHT"] = dtRecipt.Rows[0]["TODAY_WEIGHT"].ToString();
                    //透前收缩压
                    drCureMain["BEFORE_SYSTOLIC_PRESSURE"] = dtRecipt.Rows[0]["TODAY_BLOODA"].ToString();
                    //透析前舒张压
                    drCureMain["BEFORE_DIASTOLIC_PRESSURE"] = dtRecipt.Rows[0]["TODAY_BLOODB"].ToString();
                    //透析前脉搏
                    drCureMain["BEFORE_BP"] = dtRecipt.Rows[0]["TODAY_BLOODP"].ToString();

                    //传染病检验结果
                    drCureMain["INFECTIOUS_CHECK_RESULT"] = checkResult;
                    //科室
                    drCureMain["WHAT_DEPARTMENT_IN"] = department;
                    //净化时间分钟
                    drCureMain["FREQUENCY_MINUTE"] = dtRecipt.Rows[0]["FREQUENCY_MINUTE"].ToString();
                    //置换方式
                    drCureMain["DISPLACEMENT_MODE"] = dtRecipt.Rows[0]["DISPLACEMENT_MODE"].ToString();
                    //置换液流量
                    drCureMain["DISPLACEMENT_FLOW"] = Utility.CDecimal(dtRecipt.Rows[0]["DISPLACEMENT_FLOW"].ToString());
                    //置换液配方
                    drCureMain["DISPLACEMENT_RECIPE"] = dtRecipt.Rows[0]["DISPLACEMENT_RECIPE"].ToString();
                    //超滤率
                    drCureMain["UFR2"] = Utility.CDecimal(dtRecipt.Rows[0]["UFR2"].ToString());
                    //置换液配方特殊调整
                    drCureMain["DISPLACEMENT_SPECIAL_ADJUST"] = dtRecipt.Rows[0]["DISPLACEMENT_SPECIAL_ADJUST"].ToString();
                    //抗凝剂使用
                    drCureMain["ANTICOAGULANT_USE"] = dtRecipt.Rows[0]["ANTICOAGULANT_USE"].ToString();
                    //特殊交代事项
                    drCureMain["SPECIAL_MATTER"] = dtRecipt.Rows[0]["SPECIAL_MATTER"].ToString();
                    //病情神志和过敏史
                    drCureMain["FOCUS_LEVEL"] = dtRecipt.Rows[0]["FOCUS_LEVEL"].ToString();
                    drCureMain["SENSES"] = dtRecipt.Rows[0]["SENSES"].ToString();
                    drCureMain["ALLERGIC"] = dtRecipt.Rows[0]["ALLERGIC"].ToString();
                    drCureMain["BR"] = dtRecipt.Rows[0]["BR"].ToString();
                    try
                    {
                        drCureMain["BEFORE_TEMPERATURE"] = Utility.CDecimal(dtRecipt.Rows[0]["BEFORE_TEMPERATURE"].ToString());
                    }
                    catch
                    { }



                    drCureMain["DRY_WEIGHT_TAG"] = dtRecipt.Rows[0]["DRY_WEIGHT_REMARK"].ToString();

                    //备注
                    //drCureMain["DOCTOR_ADVICE"] = dtRecipt.Rows[0]["REMARK"].ToString();
                    //这边存是否为卧床
                    drCureMain["VASCULAR_ACCESS_TYPE"] = dtRecipt.Rows[0]["REMARK"].ToString();

                    //治疗单状态　
                    //添加数据到Datatable
                    _cureMainDatatable.Rows.Add(drCureMain);

                    ////插入到给药表
                    //_cureDrugDatatable = new HemodialysisModel.MED_CURE_DRUGDataTable();
                    //DataRow drDrug = _cureDrugDatatable.NewRow();
                    ////给药ID
                    //drDrug["CURE_DRUG_ID"] = System.Guid.NewGuid().ToString();
                    ////治疗单编号
                    //drDrug["CURE_ID"] = _cureMainDatatable.Rows[0]["CURE_ID"].ToString();
                    ////处方编号
                    //drDrug["RECIPE_ID"] = dtRecipt.Rows[0]["RECIPE_ID"].ToString();
                    ////药品名称
                    //drDrug["DRUG_NAME"] = dtRecipt.Rows[0]["FIRST_DRUG_NAME"].ToString();
                    ////药品单位
                    //drDrug["DOSAGE_UNITS"] = dtRecipt.Rows[0]["FIRST_DRUG_UNIT"].ToString();
                    ////药品使用方式
                    //drDrug["DRUG_MODE"] = dtRecipt.Rows[0]["FIRST_DRUG_MODE"].ToString();
                    ////药品用量
                    //drDrug["DOSAGE"] = dtRecipt.Rows[0]["FIRST_DRUG_DOSAGE"].ToString();
                    ////开药时间，处方中默认的开药
                    //drDrug["CREATE_DATE"] = dtRecipt.Rows[0]["RECIPE_DATE"].ToString();
                    ////开方医生
                    //drDrug["DOCTOR_ID"] = dtRecipt.Rows[0]["USER_ID"].ToString();

                    //_cureDrugDatatable.Rows.Add(drDrug);
                }

                if (_cureMainDatatable != null && _cureMainDatatable.Rows.Count > 0)
                {
                    ds.Tables.Add(_cureMainDatatable);
                }
                if (_cureDrugDatatable != null && _cureDrugDatatable.Rows.Count > 0)
                {
                    ds.Tables.Add(_cureDrugDatatable);
                }
                //保存治疗单、给药表
                if (ds != null && ds.Tables.Count > 0)
                {
                    result = _hemodialysisService.SaveAllCure(ds);
                }
            }
            return result;
        }

        /// <summary>
        /// 治疗结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool result = SavePatientTreatMainData(this.btnStop.Name);
            //这边的判断去掉了。需求反复了。

            if (result)
            {
                try
                {
                    var dtParam = _hemodialysisService.GetHemoParametersByCureID(ctlUserCureList.CurrentSelectedCtl.CureMainRow.CURE_ID);
                    var scheduleDt = _patientScheduleService.GetPatientScheduleByRecipeId(ctlUserCureList.CurrentSelectedCtl.CureMainRow.RECIPE_ID);
                    if (dtParam != null && dtParam.Rows.Count > 1)
                    {
                        int startInt = 0;
                        //var param = dtParam.OrderByDescending(row => row.CREATE_DATE);
                        foreach (var param in dtParam)
                        {
                            if (startInt == 0)
                            {
                                if (scheduleDt != null && scheduleDt.Rows.Count > 0)
                                {
                                    scheduleDt[0].START_TIME = param.CREATE_DATE;
                                }
                            }
                            startInt++;
                            if (!param.IsCLINICAL_MANIFESTATIONNull())
                            {
                                if (param.CLINICAL_MANIFESTATION.Trim().Contains("返血0.9%NS 300ml"))
                                {
                                    if (scheduleDt != null && scheduleDt.Rows.Count > 0)
                                    {
                                        scheduleDt[0].END_TIME = param.CREATE_DATE;

                                        this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.END_TIME = param.CREATE_DATE;
                                    }
                                    break;
                                }
                                else if (param.CLINICAL_MANIFESTATION.Trim().Contains("上机"))
                                {
                                    if (scheduleDt != null && scheduleDt.Rows.Count > 0)
                                    {
                                        scheduleDt[0].START_TIME = param.CREATE_DATE;

                                        this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.START_TIME = param.CREATE_DATE;
                                    }
                                    continue;
                                }
                            }
                        }
                        this._patientScheduleService.SavePatientScheduleInfo(scheduleDt);
                    }
                    if (scheduleDt != null && scheduleDt.Rows.Count > 0)
                    {
                        this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.START_TIME = scheduleDt[0].START_TIME;
                        this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.END_TIME = scheduleDt[0].END_TIME;
                    }

                    this.ctlUserCureList.ToSaveCureData();

                    var dtCure = _hemodialysisService.GetMainCureByHemoIDAndDate(scheduleDt[0].HEMODIALYSIS_ID, scheduleDt[0].DIALYSIS_DATE, scheduleDt[0].DIALYSIS_DATE);
                    if (dtCure != null && dtCure.Rows.Count > 0)
                    {
                        DateTime begin = scheduleDt[0].START_TIME;
                        DateTime end = scheduleDt[0].END_TIME;
                        TimeSpan span = end.Subtract(begin);
                        dtCure[0].ACTUAL_CLEANUP_HOUR = span.Days * 24 + span.Hours;
                        dtCure[0].ACTUAL_CLEANUP_MINUTE = span.Minutes;
                        _hemodialysisService.SaveCureMain(dtCure);
                    }

                    this.ctlUserCureList.LoadPatientCure(this.ctlUserCureList.CurrentSelectedCtl);
                }
                catch (Exception ex)
                { }
            }
        }

        private void tabPatientToday_CloseButtonClick(object sender, EventArgs e)
        {
            if (CurrentSelectingTab != null)
            {
                XtraTabPage page = (XtraTabPage)CurrentSelectingTab.Page;
                //this.tabPatientToday.TabPages.Remove((XtraTabPage)CurrentSelectingTab.Page);
            }
        }

        private void tabPatientToday_MouseDown(object sender, MouseEventArgs e)
        {
            //CurrentSelectingTab = tabPatientToday.CalcHitInfo(new Point(e.X, e.Y));
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            // this.Close();
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

        private void btbtBegin_ItemClick(object sender, ItemClickEventArgs e)
        {
            startCure();
        }

        private void btnbStop_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool result = SavePatientTreatMainData(this.btnStop.Name);
            //这边的判断去掉了。需求反复了。

            if (result)
            {
                var dtParam = _hemodialysisService.GetHemoParametersByCureID(ctlUserCureList.CurrentSelectedCtl.CureMainRow.CURE_ID);
                if (dtParam != null && dtParam.Rows.Count > 1)
                {
                    //var param = dtParam.OrderByDescending(row => row.CREATE_DATE);
                    foreach (var param in dtParam)
                    {
                        if (param.CLINICAL_MANIFESTATION.Trim().Contains("返血0.9%NS 300ml"))
                        {
                            var scheduleDt = _patientScheduleService.GetPatientScheduleByRecipeId(ctlUserCureList.CurrentSelectedCtl.CureMainRow.RECIPE_ID);
                            if (scheduleDt != null && scheduleDt.Rows.Count > 0)
                            {
                                scheduleDt[0].END_TIME = param.CREATE_DATE;
                                this._patientScheduleService.SavePatientScheduleInfo(scheduleDt);
                                this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.END_TIME = param.CREATE_DATE;
                            }
                            break;
                        }
                    }

                }
                this.ctlUserCureList.LoadPatientCure(this.ctlUserCureList.CurrentSelectedCtl);
            }
        }

        private void PatientTreantmentFrm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

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
            //this.Close();
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            ChangPwdFrm frm = new ChangPwdFrm(HemoApplicationContext.Current.CurrentUser.USER_ID);
            frm.ShowDialog();
        }
        private void barButtonItem990_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.ctlUserCureList.SavePdfToServer();
        }
        private void PatientTreantmentFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (XtraMessageBox.Show("您确定退出当前系统吗？", "系统提示", MessageBoxButtons.OKCancel) !=
                System.Windows.Forms.DialogResult.OK)
            {

                e.Cancel = true;
            }
            else
            {
                Program.HideClose = true;
            }
        }

        private void barAir_ItemClick(object sender, ItemClickEventArgs e)
        {
            AirPurgeFrm frm = new AirPurgeFrm();
            frm.RoomID = this.beiAREA.EditValue.ToString();
            frm.ShowDialog();
        }

        /// <summary>
        /// 血透机运行记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barUse_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (this.ctlUserCureList.CurrentSelectedCtl == null) {
            //    AutoClosedMsgBox.ShowForm("请选择病患记录！", "系统提示", 1500, MessageBoxIcon.Information);
            //    return;
            //}

            MachineList machineList = new MachineList();
            machineList.AreaId = this.beiAREA.EditValue.ToString();
            machineList.IsMachineUseRecord = true;
            machineList.ShowDialog();
        }

        /// <summary>
        /// 水处理运行记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barWaterProcessor_ItemClick(object sender, ItemClickEventArgs e)
        {
            MachineList machineList = new MachineList();
            machineList.IsMachineUseRecord = false;
            machineList.ShowDialog();
        }

        private void barBtn_UseFee_ItemClick(object sender, ItemClickEventArgs e)
        {
            QueryMachineUseFeeFrm frm = new QueryMachineUseFeeFrm();
            frm.Tag = beiAREA.EditValue;
            frm.ShowDialog();
        }

        /// <summary>
        /// 开始治疗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem_BeginCure_ItemClick(object sender, ItemClickEventArgs e)
        {
            startCure();
        }

        /// <summary>
        /// 结束治疗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem_EndCure_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool result = SavePatientTreatMainData(this.btnStop.Name);
            this.ctlUserCureList.ToSaveCureData();
            this.ctlUserCureList.LoadPatientCure(this.ctlUserCureList.CurrentSelectedCtl);

        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem_Refresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadPatientTreatMainData();
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            QueryPrintCureList frm = new QueryPrintCureList();
            frm.Show();
        }

        private void barCollectionData_ItemClick(object sender, ItemClickEventArgs e)
        {
            QueryParametersCollection frm = new QueryParametersCollection();
            frm.ShowDialog();
        }

        /// <summary>
        /// 内瘘评估
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barInBasket_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (this.ctlUserCureList.CurrentSelectedCtl == null)
            {
                AutoClosedMsgBox.ShowForm("请选择病患记录！", "系统提示", 1500, MessageBoxIcon.Warning);
                return;
            }

            string strHemoID = ctlUserCureList.CurrentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
            QueryEstimateInBasket frm = new QueryEstimateInBasket(strHemoID);
            frm.ShowDialog();
        }

        /// <summary>
        /// 临时静脉置管评估
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barVenousCatheter_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.ctlUserCureList.CurrentSelectedCtl == null)
            {
                AutoClosedMsgBox.ShowForm("请选择病患记录！", "系统提示", 1500, MessageBoxIcon.Warning);

                return;
            }

            var queryEstimateVenousList = new QueryEstimateVenousList();
            queryEstimateVenousList.IsTemp = true;
            queryEstimateVenousList.HemoId = this.ctlUserCureList.CurrentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
            queryEstimateVenousList.ShowDialog();
        }

        /// <summary>
        /// 长期留置静脉导管评估
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barLongVenous_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.ctlUserCureList.CurrentSelectedCtl == null)
            {
                AutoClosedMsgBox.ShowForm("请选择病患记录！", "系统提示", 1500, MessageBoxIcon.Warning);

                return;
            }

            var queryEstimateVenousList = new QueryEstimateVenousList();
            queryEstimateVenousList.IsTemp = false;
            queryEstimateVenousList.HemoId = this.ctlUserCureList.CurrentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
            queryEstimateVenousList.ShowDialog();
        }

        private void barSubItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            //  ReportMainFrm frm = new ReportMainFrm();
            //   frm.Show();
        }

        private void barWorkLoad_ItemClick(object sender, ItemClickEventArgs e)
        {
            //QueryComplicationFrm frm = new QueryComplicationFrm();
            QueryComplicationNew frm = new QueryComplicationNew();
            frm.ShowDialog();
        }

        /// <summary>
        /// 工作量统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem10_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            QueryWorkloadFrmFZ frm = new QueryWorkloadFrmFZ();
            var control = new QueryWorkloadFZ();
            control.Dock = DockStyle.Fill;
            frm.Controls.Add(control);
            //QueryWorkloadFrm frm = new QueryWorkloadFrm();
            //frm.banChi = this.beiBANCI.EditValue.ToString();// == "1" ? "早班" : this.beiBANCI.EditValue.ToString() == "2" ? "晚班" : "其他";
            //frm.date = this.beiDate.EditValue == null ? DateTime.Now.Date : Utility.CDate(this.beiDate.EditValue.ToString()).Date;
            //frm._workArea = this.beiAREA.EditValue.ToString();
            //frm.panelControl4.Visible = true;
            frm.ShowDialog();
        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = new SubjectiveComfortFrm();
            frm.ShowDialog();
        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = new BorrowMedicineFrm();

            frm.ShowDialog();
        }

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ctlUserCureList.CurrentSelectedCtl == null)
            {
                AutoClosedMsgBox.ShowForm("请先选择数据，再开始记账！", "系统提示", 1500, MessageBoxIcon.Warning);

                return;
            }

            if (ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.IsSTART_TIMENull())
            {
                AutoClosedMsgBox.ShowForm("该患者尚未开始治疗，不能进行记账。", "系统提示", 1500, MessageBoxIcon.Warning);
                return;
            }
            ShowBillFrm(ctlUserCureList.CurrentSelectedCtl);
        }

        private void btnBillReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = new PatientBillHistoryRecordFrm();
            frm.ShowDialog();

        }

        /// <summary>
        /// 注意传参的名字改变了，否则会有冲突
        /// </summary>
        /// <param name="senderr"></param>
        /// <param name="er"></param>
        private void barBtn_ShiftRole_ItemClick(object senderr, ItemClickEventArgs er)
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

        private void barChangePWD_ItemClick(object sender, ItemClickEventArgs e)
        {
            ChangPwdFrm frm = new ChangPwdFrm(HemoApplicationContext.Current.CurrentUser.USER_ID);
            frm.ShowDialog();
        }

        private void barBtn_ReLogin_ItemClick(object sender, ItemClickEventArgs e)
        {
            // this.Close();
        }

        private void barErythropoietin_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ctlUserCureList.CurrentSelectedCtl == null)
            {
                AutoClosedMsgBox.ShowForm("请先选择病患信息！", "系统提示", 1500, MessageBoxIcon.Warning);
                return;
            }

            ReUsableRecord _reUsableRecord = new ReUsableRecord(this.ctlUserCureList.CurrentSelectedCtl.PatientRow);
            _reUsableRecord.ShowDialog();
        }

        private void barHealth_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ctlUserCureList.CurrentSelectedCtl == null)
            {
                AutoClosedMsgBox.ShowForm("请先选择病患信息！", "系统提示", 1500, MessageBoxIcon.Warning);
                return;
            }

            EditHealthEducation frm = new EditHealthEducation();
            frm.HEMODIALYSIS_ID = this.ctlUserCureList.CurrentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
            //frm.ShowDialog();
        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.ctlUserCureList.CurrentSelectedCtl == null)
            {
                AutoClosedMsgBox.ShowForm("请选择病患记录！", "系统提示", 1500, MessageBoxIcon.Warning);
                return;
            }

            string strHemoID = ctlUserCureList.CurrentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
            AssessmentListFrm frm = new AssessmentListFrm();
            frm.patient = ctlUserCureList.CurrentSelectedCtl.PatientRow;
            frm.HemoID = strHemoID;
            frm.ShowDialog();
        }

        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
        {
            NurseWorkTimeRecord frm = new NurseWorkTimeRecord();
            frm.ShowDialog();
        }

        private void barKolcaba_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.ctlUserCureList.CurrentSelectedCtl == null)
            {
                AutoClosedMsgBox.ShowForm("请选择病患记录！", "系统提示", 1500, MessageBoxIcon.Warning);
                return;
            }
            PatientKolcaba frm = new PatientKolcaba();
            frm.CurrentHemoId = ctlUserCureList.CurrentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
            frm.CurrentHemoName = ctlUserCureList.CurrentSelectedCtl.PatientRow.NAME;
            frm.ShowDialog();
        }

        private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
        {
            PatientScheduleReportForJL frmPatientScheduleReport = new PatientScheduleReportForJL();
            frmPatientScheduleReport.ShowDialog();
        }

        private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
        {
            DateTime startWeek = new DateTime();
            DateTime endWeek = new DateTime();
            DateTime date = Utility.CDate(_patientScheduleService.GetServerDate());
            int _nextWeekDays = 0;

            startWeek = Utility.GetMonday(date).AddDays(_nextWeekDays).Date;
            endWeek = startWeek.AddDays(6).Date;
            var dt = _patientScheduleService.GetWeekDutyByTime(startWeek, endWeek);

            string timer = string.Format("{0}~{1}", startWeek.ToString("yyyy-MM-dd"), endWeek.ToString("yyyy-MM-dd"));
            NurseDutyReport frm = new NurseDutyReport(dt, timer);
            ReportPrintTool pt = new ReportPrintTool(frm);
            pt.ShowPreviewDialog();
        }

        private void barButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new NurseChangeWork())
            {
                frm.currentArea = this.beiAREA.EditValue.ToString();
                frm.ShowDialog();
            }
        }

        private void barButtonItem19_ItemClick(object sender, ItemClickEventArgs e)
        {

            using (PatientPrePay frm = new PatientPrePay())
            {
                var diagresult = FlyoutDialog.Show(this.FindForm(), frm);
                if (diagresult == System.Windows.Forms.DialogResult.OK)
                {
                    //this.Close();
                }
            }
        }

        private void barButtonItem20_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.ctlUserCureList.CurrentSelectedCtl == null || this.ctlUserCureList.CurrentSelectedCtl.CureMainRow == null)
            {
                XtraMessageBox.Show("请选择开始治疗的患者病患记录！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            PatientMaterial frm = new PatientMaterial();
            frm.CurrentHemoId = ctlUserCureList.CurrentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
            frm.RecipeId = ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.RECIPE_ID;
            frm.RecoderId = ctlUserCureList.CurrentSelectedCtl.CureMainRow.CURE_ID;
            frm.IsCanEdit = ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.IsEND_TIMENull();
            frm.ShowDialog();
        }

        private void barButtonItem21_ItemClick(object sender, ItemClickEventArgs e)
        {
            SchedulePatientInfos frm = new SchedulePatientInfos();
            frm.ShowDialog();
        }

        private void barButtonItem22_ItemClick(object sender, ItemClickEventArgs e)
        {
            PatientSetCardon frm = new PatientSetCardon();
            frm.ShowDialog();
        }

        /// <summary>
        /// 耗材入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem24_ItemClick(object sender, ItemClickEventArgs e)
        {
            QueryMaterialInputFrm frm = new QueryMaterialInputFrm();
            frm.ShowDialog();

        }

        /// <summary>
        /// 耗材出库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem25_ItemClick(object sender, ItemClickEventArgs e)
        {
            QueryMaterialOutputFrm frm = new QueryMaterialOutputFrm();
            frm.ShowDialog();

        }

        /// <summary>
        /// 耗材盘点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem26_ItemClick(object sender, ItemClickEventArgs e)
        {
            QueryMaterialCheckFrm frm = new QueryMaterialCheckFrm();
            frm.ShowDialog();
        }

        /// <summary>
        /// 药品托管
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem27_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.ctlUserCureList.CurrentSelectedCtl == null)
            {
                AutoClosedMsgBox.ShowForm("请选择病患记录！", "系统提示", 1500, MessageBoxIcon.Warning);
                return;
            }
            QueryPatientDrugInput frm = new QueryPatientDrugInput();
            frm.CurrentHemoId = ctlUserCureList.CurrentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
            frm.ShowDialog();
        }

        private void barButtonItem28_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.ctlUserCureList.CurrentSelectedCtl == null)
            {
                AutoClosedMsgBox.ShowForm("请选择病患记录！", "系统提示", 1500, MessageBoxIcon.Warning);
                return;
            }
            QueryPatientDrugOutput frm = new QueryPatientDrugOutput();
            frm.CurrentHemoId = ctlUserCureList.CurrentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
            frm.ShowDialog();
        }

        /// <summary>
        /// 透析记录单模板下载包含内瘘评估
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem29_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                string AppFilePath = AppDomain.CurrentDomain.BaseDirectory + "Doc\\血液净化治疗记录单-内瘘评估.doc";
                if (System.IO.File.Exists(AppFilePath))
                {
                    System.Diagnostics.Process.Start(AppFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 透析记录单模板下载包含导管评估
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem30_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                string AppFilePath = AppDomain.CurrentDomain.BaseDirectory + "Doc\\血液净化治疗记录单-导管评估.doc";
                if (System.IO.File.Exists(AppFilePath))
                {
                    System.Diagnostics.Process.Start(AppFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void barButtonItem31_ItemClick(object sender, ItemClickEventArgs e)
        {
            ScreenConfig frm = new ScreenConfig();
            frm.ShowDialog();
        }

        private void barButtonItem32_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (this.ctlUserCureList.CurrentSelectedCtl == null) {
            //    AutoClosedMsgBox.ShowForm("请选择病患记录！", "系统提示", 1500, MessageBoxIcon.Warning);
            //    return;
            //}
            //PatientRecipeFrm frm = new PatientRecipeFrm();
            //frm.HemoId = ctlUserCureList.CurrentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
            //frm.ShowDialog();
        }

        private void barButtonItem33_ItemClick(object sender, ItemClickEventArgs e)
        {
            RiskAssessFrm frm = new RiskAssessFrm();
            frm.ShowDialog();
        }

        private void barButtonItem34_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.ctlUserCureList.CurrentSelectedCtl == null)
            {
                XtraMessageBox.Show("请选择病患记录！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            PatientSufficiency frm = new PatientSufficiency();
            frm.CurrentHemoId = ctlUserCureList.CurrentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
            frm.CurrentHemoName = ctlUserCureList.CurrentSelectedCtl.PatientRow.NAME;
            frm.ShowDialog();

        }

        private void barButtonItem35_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.ctlUserCureList.CurrentSelectedCtl == null)
            {
                XtraMessageBox.Show("请选择病患记录！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var frm = new FrmSufficiencyURR();
            frm.HemoId = ctlUserCureList.CurrentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
            frm.ShowDialog();
        }

        private void barButtonItem36_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.ctlUserCureList.CurrentSelectedCtl == null)
            {
                XtraMessageBox.Show("请选择病患记录！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var frm = new NutritionSGAFrm();
            frm.CurrentHemoId = ctlUserCureList.CurrentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
            frm.ShowDialog();
        }

        private void barButtonItem37_ItemClick(object sender, ItemClickEventArgs e)
        {
            QueryMachineUseFeeFrm frm = new QueryMachineUseFeeFrm();
            frm.Tag = beiAREA.EditValue;
            frm.ShowDialog();
        }

        private void btnBack_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool result = SavePatientTreatMainData(this.btnBack.Name);
            this.ctlUserCureList.LoadPatientCure(this.ctlUserCureList.CurrentSelectedCtl);
        }

        private void barButtonItem38_ItemClick(object sender, ItemClickEventArgs e)
        {
            MaterialQueryFrm frm = new MaterialQueryFrm();
            frm.ShowDialog();
        }

        private void barButtonItem39_ItemClick(object sender, ItemClickEventArgs e)
        {
            NursePerformanceAppraisal performanceAppraisal = new NursePerformanceAppraisal();
            performanceAppraisal.ShowDialog();
        }

        private void barButtonItem40_ItemClick(object sender, ItemClickEventArgs e)
        {
            QueryRegularDialysis queryRegularDialysis = new QueryRegularDialysis();
            queryRegularDialysis.ShowDialog();
        }

        /// <summary>
        /// 双击患者卡片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlUserCureList_DoubleClick(object sender, DoubleClickEventArgs e)
        {
            e.Banci = this.beiBANCI.EditValue.ToString();
        }

        #endregion

        private void barButtonItem41_ItemClick(object sender, ItemClickEventArgs e)
        {
            QueryDrugExectFrm frm = new QueryDrugExectFrm();
            frm.banchiId = this.beiBANCI.EditValue.ToString();
            frm.roomId = this.beiAREA.EditValue.ToString();
            frm.currentDt = Utility.CDate(this.beiDate.EditValue.ToString()).Date;
            frm.ShowDialog();
        }
        /// <summary>
        /// 透析事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem45_ItemClick(object sender, ItemClickEventArgs e)
        {
            //frmEventInfoManager frm = new frmEventInfoManager();
            //frm.currentUI = new HemoEventManager();
            //frm.ShowDialog();
        }
        /// <summary>
        /// 事件检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem44_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        /// <summary>
        /// 检测日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem43_ItemClick(object sender, ItemClickEventArgs e)
        {
            //frmEventInfoManager frm = new frmEventInfoManager();
            //frm.currentUI = new HemoOtherLog();
            //frm.ShowDialog();
        }

        private void barButtonItem46_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ctlUserCureList.CurrentSelectedCtl == null)
            {
                AutoClosedMsgBox.ShowForm("请先选择数据，之后开始加透治疗！", "系统提示", 1500, MessageBoxIcon.Warning);
                return;
            }

            //if (ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.IsRECIPE_IDNull()) {
            //    AutoClosedMsgBox.ShowForm("该病人尚没有确认透析处方，不能进行治疗。", "系统提示", 1500, MessageBoxIcon.Warning);
            //    return;
            //}

            string strHemoID = ctlUserCureList.CurrentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
            DateTime startDate = new DateTime();
            if (beiDate.EditValue != null)
            {
                startDate = Utility.CDate(beiDate.EditValue.ToString());
            }
            else
            {
                startDate = System.DateTime.Now;
            }
            //2014-03-05 刘超 修改将处方日期通过窗体传入，之前使用的为默认值sysdate，只传入透析号。
            /// HemodialysisModel.MED_HEMO_RECIPEDataTable recipeTable = this._hemodialysisService.GetRecipeByHemodialysisID(strHemoID);
            HemodialysisModel.MED_HEMO_RECIPEDataTable recipeTable = this._hemodialysisService.GetRecipeByHemodialysisIDAndDate(strHemoID, startDate);
            //recipeTable = Utility.GetSubTable(recipeTable, "status = 1") as HemodialysisModel.MED_HEMO_RECIPEDataTable;

            if (recipeTable.Rows.Count == 0)
            {
                XtraMessageBox.Show("该病人尚未确认透析处方，不能进行治疗。", "患者治疗");
                return;
            }

            if (recipeTable.Rows.Count > 1)
            {
                AutoClosedMsgBox.ShowForm("该病人当天有多张处方，请确认只有一张有效的处方！", "系统提示", 1500, MessageBoxIcon.Warning);
                return;
            }

            if (recipeTable != null)
            {
                saveRecipe = recipeTable[0].RECIPE_ID;
                this.ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow.RECIPE_ID = recipeTable[0].RECIPE_ID;
            }

            //       this.SavePatientTreatMainDataAddHemo(this.btnBegin.Name);

            //在开始的同时，添加一条治疗单记录
            bool result = false;
            int iCureCount = 0;

            if (ctlUserCureList.CurrentSelectedCtl != null)
            {
                if (ctlUserCureList.CurrentSelectedCtl != null && ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow != null)
                {
                    if (ctlUserCureList.CurrentSelectedCtl != null && ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow != null)
                    {

                        if (beiDate.EditValue == null)
                            this.strDate = Utility.CDate((beiDate.Edit).NullText).ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ":" + DateTime.Now.Second;
                        else
                            this.strDate = Utility.CDate(beiDate.EditValue.ToString()).ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ":" + DateTime.Now.Second;


                        iCureCount = _hemodialysisService.GetMainCureCountByCreateDate(strHemoID, Utility.CDate(strDate));
                        //当天没有一次透析不能开始加透治疗！
                        if (iCureCount == 0)
                        {
                            AutoClosedMsgBox.ShowForm("请确认该患者当天已经治疗过才能加透！", "系统提示", 1500, MessageBoxIcon.Warning);
                            return;
                        }
                        //有一次可以正常加透
                        if (iCureCount == 1)
                        {
                            result = insertCureAllInfoByHemoID(strHemoID, true);
                            if (result && !string.IsNullOrEmpty(tmpCureID))
                            {
                                //获取加透的治疗信息

                                #region 原点击透析处方弹出界面的逻辑
                                using (ShowEditTreatment frmTreatment = new ShowEditTreatment(strHemoID, tmpCureID, 0, 0))
                                {
                                    frmTreatment.PatientScheduleRow = ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow;
                                    // frmTreatment.MachineRow = ctlUserCureList.CurrentSelectedCtl.MachineRow;
                                    // frmTreatment.IsReplenishTreat = false;
                                    frmTreatment.CureDate = Utility.CDate(strDate).Date;
                                    frmTreatment.HemodialysisID = strHemoID;
                                    frmTreatment.CureID = tmpCureID;
                                    frmTreatment.isOverOrder = true;
                                    frmTreatment.ShowDialog();
                                    if (frmTreatment.DialogResult == DialogResult.Yes)
                                    {
                                        // LoadPatientCure(ctlUserCureList.CurrentSelectedCtl);
                                    }
                                }
                                #endregion
                            }

                        }
                        //有两次直接打开第二次的加透数据
                        if (iCureCount == 2)
                        {
                            HemodialysisModel.MED_CURE_MAINDataTable dtMain = _hemodialysisService.GetMainCureByRecipeId(recipeTable[0].RECIPE_ID);
                            if (dtMain != null && dtMain.Rows.Count > 0)
                            {
                                //取加透的治疗单的单号
                                var _cureId = dtMain.FirstOrDefault(i => !i.IsRECIPE_TYPENull() && i.RECIPE_TYPE == "1").CURE_ID;
                                #region 原点击透析处方弹出界面的逻辑
                                using (ShowEditTreatment frmTreatment = new ShowEditTreatment(strHemoID, _cureId, 0, 0))//dtMain.Rows[0]["CURE_ID"].ToString()
                                {
                                    frmTreatment.PatientScheduleRow = ctlUserCureList.CurrentSelectedCtl.PatientScheduleRow;

                                    frmTreatment.CureDate = Utility.CDate(strDate).Date;
                                    frmTreatment.HemodialysisID = strHemoID;
                                    frmTreatment.CureID = _cureId;
                                    frmTreatment.isOverOrder = true;
                                    frmTreatment.ShowDialog();
                                }
                                #endregion
                            }
                        }
                    }
                }
            }
        }
    }
}
