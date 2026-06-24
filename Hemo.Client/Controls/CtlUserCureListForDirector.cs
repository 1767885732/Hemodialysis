/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：针对主任工作站的患者治疗单列表用户控件类
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
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraEditors;
using Hemo.Client.Controls.Treatment;
using Hemo.Client.Core;
using Hemo.Client.UI.Erythropoietin;
using Hemo.Client.UI.Hemodialysis;
using Hemo.Client.UI.Lab;
using Hemo.IService;
using Hemo.IService.Config;
using Hemo.IService.Machine;
using Hemo.IService.PatientSchedule;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.Client.UI.Machine;

namespace Hemo.Client.Controls
{
    public partial class CtlUserCureListForDirector : XtraUserControl
    {
        //class MessageInfo
        //{
        //    public string PatientScheduleID
        //    {
        //        set;
        //        get;
        //    }

        //    public string Message
        //    {
        //        set;
        //        get;
        //    }
        //}

        #region 变量

        //private int _hoursPoint4SaveMessage;
        //private int _minutesPoint4SaveMessage;
        //private int _secondsPoint4SaveMessage;
        private string _strCureID = string.Empty;
        private DataTable _cureTable = new DataTable();

        private CtlMedicalDocumentContainer _medicalDocContainer = new CtlMedicalDocumentContainer();
        private CtlTreatmentPerson _currentSelectedCtl;

        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IMachine _machineService = ServiceManager.Instance.MachineService;
        private IPatient _patientService = ServiceManager.Instance.PatientService;
        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;

        private PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable _patientScheduleDataTable;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _bedDataTable;
        private MachineModel.MED_DIALYSIS_MACHINEDataTable _machineDataTable;
        private PatientModel.MED_PATIENTSDataTable _patientDataTable;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _purifierDataTable;
        //private ConfigModel.MED_COMMON_ITEMLISTDataTable _alertParamsDataTable;

        private IList<CtlTreatmentPerson> _currentSelectedCtls = new List<CtlTreatmentPerson>();
        //private IList<MessageInfo> _messageInfos = new List<MessageInfo>();

        private HemodialysisModel.MED_CURE_MAINDataTable _CureMainDatatable;

        #endregion

        #region 属性

        /// <summary>
        /// 透析号
        /// </summary>
        private string _hemodialysisID = string.Empty;
        public string HEMODIALYSIS_ID
        {
            set
            {
                _hemodialysisID = value;
            }
            get
            {
                return _hemodialysisID;
            }
        }

        /// <summary>
        /// 病人姓名 
        /// </summary>
        private string _patientName = string.Empty;
        public string PatientName
        {
            set
            {
                _patientName = value;
            }
            get
            {
                return _patientName;
            }
        }

        /// <summary>
        /// 治疗日期
        /// </summary>
        public string TreamentDate
        {
            get;
            set;
        }

        /// <summary>
        /// 班次
        /// </summary>
        public string Classes
        {
            get;
            set;
        }

        /// <summary>
        /// 区域
        /// </summary>
        public string Area
        {
            get;
            set;
        }

        public CtlTreatmentPerson CurrentSelectedCtl
        {
            get
            {
                return this._currentSelectedCtl;
            }
        }

        #endregion

        #region 构造函数

        public CtlUserCureListForDirector()
        {
            this.InitializeComponent();

            this._bedDataTable = this._configService.GetConfigList(string.Empty, string.Empty, "床位", "1");
            this._purifierDataTable = this._configService.GetConfigList(string.Empty, string.Empty, "净化器类型", "1");
            this._machineDataTable = this._machineService.GetMachineList();
            this._patientDataTable = this._patientService.GetPatientList();
            //this._alertParamsDataTable = this._configService.GetConfigList(string.Empty, string.Empty, "提醒参数设定", "1");
        }

        #endregion

        #region 事件

        private void CtlUserCureList_Load(object sender, EventArgs e)
        {
            //this._hoursPoint4SaveMessage = this.GetValueFromConfig("保存系统消息的时点", 0);
            //this._minutesPoint4SaveMessage = this.GetValueFromConfig("保存系统消息的分点", 10);
            //this._secondsPoint4SaveMessage = this.GetValueFromConfig("保存系统消息的秒点", 0);

            //载入透析单文档
            this._medicalDocContainer = new CtlMedicalDocumentContainer();

            //this.timerSyncTime.Interval = this.GetValueFromConfig("治疗时间倒计时间隔（单位：毫秒）", 1000);
            //this.timerShowMsg.Interval = this.GetValueFromConfig("显示系统消息间隔（单位：毫秒）", 10000);

            //this.timerSyncTime.Enabled = this.timerShowMsg.Enabled = true;
        }

        /// <summary>
        /// 点击弹出治疗单透析处方选项卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //void itmNarBarMain_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        //{
        //    _strCureID = ((DevExpress.XtraNavBar.NavBarItem)(sender)).Tag.ToString();
        //    EditTreatment frmTreatment = new EditTreatment(HEMODIALYSIS_ID, _strCureID, 0);
        //    frmTreatment.IsReplenishTreat = false;
        //    frmTreatment.ShowDialog();
        //}

        /// <summary>
        /// 病人卡片单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ctlTreatmentPerson_ContainerPanelClick(object sender, ContainerPanelEventArgs args)
        {
            if (this._currentSelectedCtl != null)
                this._currentSelectedCtl.ClearSelectedEffect();

            this._currentSelectedCtl = (CtlTreatmentPerson)sender;

            this._currentSelectedCtl.SetSelectedEffect();
            //if (!string.IsNullOrEmpty(_strCureID) && !this._currentSelectedCtl.PatientScheduleRow.IsEND_TIMENull())
            //{
            //    HemodialysisModel.MED_CURE_MAINDataTable _CureMainDatatableTemp = _hemodialysisService.GetMainCureByCureID(_strCureID);
            //    if (_CureMainDatatableTemp.Rows[0]["PRIMARY_NURSE"].ToString() == Hemo.Client.Core.HemoApplicationContext.Current.CurrentUser.EMP_NO || _CureMainDatatableTemp.Rows[0]["PRIMARY_NURSE"].ToString().Length == 0)
            //    {
            //        this.btnRecipe.Enabled = true;
            //        this.btnParams.Enabled = true;
            //        this.btnOrder.Enabled = true;
            //        this.btnSummary.Enabled = true;
            //    }
            //    else
            //    {
            //        this.btnRecipe.Enabled = false;
            //        this.btnParams.Enabled = false;
            //        this.btnOrder.Enabled = false;
            //        this.btnSummary.Enabled = false;
            //    }
            //}
        }

        /// <summary>
        /// 病人卡片双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ctlTreatmentPerson_ContainerPanelDoubleClick(object sender, ContainerPanelEventArgs args)
        {
            this._currentSelectedCtl = (CtlTreatmentPerson)sender;

            //if (_currentSelectedCtl.PatientScheduleRow.IsRECIPE_IDNull())
            //{
            //    XtraMessageBox.Show("该病人尚没有透析处方，不能进行治疗。", "主任工作站");
            //    return;
            //}

            LoadPatientCure(_currentSelectedCtl);

            //if (!string.IsNullOrEmpty(_strCureID) && !this._currentSelectedCtl.PatientScheduleRow.IsEND_TIMENull())
            //{
            //    HemodialysisModel.MED_CURE_MAINDataTable _CureMainDatatableTemp = _hemodialysisService.GetMainCureByCureID(_strCureID);
            //    if (_CureMainDatatableTemp.Rows[0]["PRIMARY_NURSE"].ToString() == Hemo.Client.Core.HemoApplicationContext.Current.CurrentUser.EMP_NO)
            //    {
            //        this.btnRecipe.Enabled = true;
            //        this.btnParams.Enabled = true;
            //        this.btnOrder.Enabled = true;
            //        this.btnSummary.Enabled = true;
            //    }
            //    else
            //    {
            //        this.btnRecipe.Enabled = false;
            //        this.btnParams.Enabled = false;
            //        this.btnOrder.Enabled = false;
            //        this.btnSummary.Enabled = false;
            //    }

            //}
            //else
            //{
            //    this.btnRecipe.Enabled = true;
            //    this.btnParams.Enabled = true;
            //    this.btnOrder.Enabled = true;
            //    this.btnSummary.Enabled = true;
            //}
        }

        /// <summary>
        /// 透析处方
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btnRecipe_Click(object sender, EventArgs e)
        //{
        //    if (!this.CheckCurrentSelectedCtlSeleted())
        //        return;

        //    if (checkBeginCure())
        //    {
        //        return;
        //    }

        //    EditTreatment frmTreatment = new EditTreatment(this._currentSelectedCtl.PatientRow.HEMODIALYSIS_ID, _strCureID, 0);
        //    frmTreatment.PatientScheduleRow = this._currentSelectedCtl.PatientScheduleRow;
        //    frmTreatment.IsReplenishTreat = false;
        //    frmTreatment.CureDate = Utility.CDate(TreamentDate).Date;
        //    frmTreatment.ShowDialog();
        //    if (frmTreatment.DialogResult == DialogResult.Yes)
        //    {
        //        LoadPatientCure(_currentSelectedCtl);
        //    }
        //}

        /// <summary>
        /// 临时医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btnOrder_Click(object sender, EventArgs e)
        //{
        //    if (!this.CheckCurrentSelectedCtlSeleted())
        //        return;

        //    if (checkBeginCure())
        //    {
        //        return;
        //    }

        //    EditTreatment frmTreatment = new EditTreatment(this._currentSelectedCtl.PatientRow.HEMODIALYSIS_ID, _strCureID, 1);
        //    frmTreatment.PatientScheduleRow = this._currentSelectedCtl.PatientScheduleRow;
        //    frmTreatment.IsReplenishTreat = false;
        //    frmTreatment.CureDate = Utility.CDate(TreamentDate).Date;
        //    frmTreatment.ShowDialog();
        //    if (frmTreatment.DialogResult == DialogResult.Yes)
        //    {
        //        LoadPatientCure(_currentSelectedCtl);
        //    }
        //}

        /// <summary>
        /// 检查化验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btnLab_Click(object sender, EventArgs e)
        //{
        //    if (!this.CheckCurrentSelectedCtlSeleted())
        //        return;

        //    LabFrm labFrm = new LabFrm(this._currentSelectedCtl.PatientRow);

        //    labFrm.ShowDialog();
        //}

        /// <summary>
        /// 促红素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btnErythropoietin_Click(object sender, EventArgs e)
        //{
        //    if (!this.CheckCurrentSelectedCtlSeleted())
        //        return;

        //    if (checkBeginCure())
        //    {
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(_strCureID))
        //        return;
        //    _CureMainDatatable = _hemodialysisService.GetMainCureByCureID(_strCureID);
        //    var row = _CureMainDatatable.Rows[0] as HemodialysisModel.MED_CURE_MAINRow;
        //    this._currentSelectedCtl.PatientRow.INPUT_CODE = row.REUSE_TIMES.ToString();
        //    this._currentSelectedCtl.PatientRow.EDUCATION = row.MACHINE_TYPE.ToString();
        //    ReUsableRecord _reUsableRecord = new ReUsableRecord(this._currentSelectedCtl.PatientRow);
        //    _reUsableRecord.ShowDialog();
        //}

        /// <summary>
        /// 透析小结
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btnSummary_Click(object sender, EventArgs e)
        //{

        //    if (!this.CheckCurrentSelectedCtlSeleted())
        //        return;

        //    if (checkBeginCure())
        //    {
        //        return;
        //    }

        //    EditTreatment frmTreatment = new EditTreatment(this._currentSelectedCtl.PatientRow.HEMODIALYSIS_ID, _strCureID, 3);
        //    frmTreatment.PatientScheduleRow = this._currentSelectedCtl.PatientScheduleRow;
        //    frmTreatment.IsReplenishTreat = false;
        //    frmTreatment.CureDate = Utility.CDate(TreamentDate).Date;
        //    frmTreatment.ShowDialog();
        //    if (frmTreatment.DialogResult == DialogResult.Yes)
        //    {
        //        LoadPatientCure(_currentSelectedCtl);
        //    }
        //}

        //private void btnParams_Click(object sender, EventArgs e)
        //{
        //    if (!this.CheckCurrentSelectedCtlSeleted())
        //        return;

        //    if (checkBeginCure())
        //    {
        //        return;
        //    }

        //    EditTreatment frmTreatment = new EditTreatment(this._currentSelectedCtl.PatientRow.HEMODIALYSIS_ID, _strCureID, 2);
        //    frmTreatment.PatientScheduleRow = this._currentSelectedCtl.PatientScheduleRow;
        //    frmTreatment.IsReplenishTreat = false;
        //    frmTreatment.CureDate = Utility.CDate(TreamentDate).Date;
        //    frmTreatment.ShowDialog();
        //    if (frmTreatment.DialogResult == DialogResult.Yes)
        //    {
        //        LoadPatientCure(_currentSelectedCtl);
        //    }
        //}

        //private void timerSyncTime_Tick(object sender, EventArgs e)
        //{
        //    var currentSelectedCtls = this._currentSelectedCtls.Where(c => c.IsAllowSyncTime).ToList();

        //    foreach (var currentSelectedCtl in currentSelectedCtls)
        //    {
        //        TimeSpan ts;

        //        currentSelectedCtl.SyncTime(out ts);

        //        if (ts.Hours == this._hoursPoint4SaveMessage
        //            && ts.Minutes == this._minutesPoint4SaveMessage
        //            && ts.Seconds == this._secondsPoint4SaveMessage)
        //        {
        //            HemodialysisModel.MED_COMMON_MESSAGEDataTable messageDataTable = new HemodialysisModel.MED_COMMON_MESSAGEDataTable();
        //            HemodialysisModel.MED_COMMON_MESSAGERow messageRow = messageDataTable.NewMED_COMMON_MESSAGERow();

        //            messageRow.MSG_ID = currentSelectedCtl.PatientScheduleRow.PATIENT_SCHEDULE_ID;
        //            messageRow.MESSAGE = string.Format("{0} 距离透析结束还有不到 {1} 分钟！", currentSelectedCtl.PatientRow.NAME, ts.Minutes);
        //            messageRow.TYPE = 1;
        //            messageRow.STATUS = "1"; //1：未读；2：已读
        //            messageRow.CREATETIME = DateTime.Now;

        //            messageDataTable.AddMED_COMMON_MESSAGERow(messageRow);
        //        }
        //    }
        //}

        //private void timerSaveMessage_Tick(object sender, EventArgs e)
        //{
        //    if (this._messageInfos.Count == 0)
        //        return;

        //    HemodialysisModel.MED_COMMON_MESSAGEDataTable messageDataTable = new HemodialysisModel.MED_COMMON_MESSAGEDataTable();

        //    for (int i = 0; i < this._messageInfos.Count; i++)
        //    {
        //        HemodialysisModel.MED_COMMON_MESSAGERow messageRow = messageDataTable.NewMED_COMMON_MESSAGERow();

        //        messageRow.MSG_ID = this._messageInfos[i].PatientScheduleID;
        //        messageRow.MESSAGE = this._messageInfos[i].Message;
        //        messageRow.TYPE = 1;
        //        messageRow.STATUS = "1"; //1：未读；2：已读
        //        messageRow.CREATETIME = DateTime.Now;

        //        messageDataTable.AddMED_COMMON_MESSAGERow(messageRow);

        //        this._messageInfos.RemoveAt(i);
        //    }

        //    if (messageDataTable.Rows.Count > 0)
        //        this._hemodialysisService.SaveMsgInfo(messageDataTable);
        //}

        //private void timerShowMsg_Tick(object sender, EventArgs e)
        //{
        //    var msgIDs = (from f in this.alertControl1.AlertFormList
        //                  let tag = f.AlertInfo.Tag
        //                  select tag == null ? string.Empty : tag.ToString()).ToList();
        //    HemodialysisModel.MED_COMMON_MESSAGEDataTable messageDataTable = this._hemodialysisService.GetAllMessage(1);

        //    foreach (HemodialysisModel.MED_COMMON_MESSAGERow messageRow in messageDataTable.Rows)
        //    {
        //        if (msgIDs.Contains(messageRow.MSG_ID))
        //            continue;

        //        AlertInfo info = new AlertInfo("透析提醒", messageRow.MESSAGE, messageRow.MESSAGE, null, messageRow.MSG_ID);

        //        this.alertControl1.Show(this.ParentForm, info);
        //    }
        //}

        //private void alertControl1_ButtonClick(object sender, AlertButtonClickEventArgs e)
        //{
        //    if (e.Info.Tag == null)
        //        return;

        //    switch (e.ButtonName.ToLower())
        //    {
        //        case "btnsave": //保存系统消息为已读
        //            this._hemodialysisService.SaveMsgInfoToMarkRead(e.Info.Tag.ToString());

        //            e.AlertForm.Close();
        //            break;

        //        default:
        //            e.AlertForm.Close();
        //            break;
        //    }
        //}

        //private void timerShowOrders_Tick(object sender, EventArgs e)
        //{
        //    this.alertControl1.Dispose();
        //    DataTable dt = this._hemodialysisService.GetUNExcuteOrdersbyData(TreamentDate == "" ? DateTime.Now.Date : Utility.CDate(TreamentDate).Date);
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        string strCaption = string.Empty;
        //        foreach (DataRow msgRow in dt.Rows)
        //        {
        //            strCaption += string.Format("{0};", msgRow.ItemArray[0].ToString());
        //        }
        //        AlertInfo info = new AlertInfo("以下人员有未执行处方", strCaption, string.Empty);

        //        this.alertControl1.Show(this.ParentForm, info);
        //    }
        //}

        //private void btn_Books_Click(object sender, EventArgs e)
        //{
        //    if (!this.CheckCurrentSelectedCtlSeleted())
        //        return;

        //    PatientKnowBooks FRM = new PatientKnowBooks();
        //    FRM.BindDocTree(this._currentSelectedCtl.PatientRow);
        //    FRM.ShowDialog();
        //}

        //private void btnHealth_Click(object sender, EventArgs e)
        //{
        //    if (!this.CheckCurrentSelectedCtlSeleted())
        //        return;

        //    EditHealthEducation frm = new EditHealthEducation();
        //    frm.HEMODIALYSIS_ID = this._currentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
        //    frm.ShowDialog();
        //}

        #endregion

        #region 方法

        #region 载入治疗单方法

        /// <summary>
        /// 加载病人治疗数据
        /// </summary>
        public void LoadPatientTreatMainData()
        {
            this._currentSelectedCtl = null;

            DateTime treamentDate = TreamentDate == string.Empty ? DateTime.Now.Date : Utility.CDate(TreamentDate).Date;
            this._patientScheduleDataTable = Utility.GetSubTable(this._patientScheduleService.GetPatientScheduleList(LoginUser.User.USER_ID, treamentDate, treamentDate, Classes), string.Format("DIALYSIS_ROOM_ID = '{0}'", Area)) as PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable;

            this.pnlPatientTreat.Controls.Clear();

            foreach (PatientScheduleModel.MED_PATIENT_SCHEDULERow row in this._patientScheduleDataTable.Rows)
            {
                ConfigModel.MED_COMMON_ITEMLISTRow bedRow = this._bedDataTable.FindByITEM_ID(row.BED_NUMBER);
                MachineModel.MED_DIALYSIS_MACHINERow machineRow = this._machineDataTable.FindByMACHINE_ID(row.MONITOR_LABEL);
                PatientModel.MED_PATIENTSRow patientRow = this._patientDataTable.FindByHEMODIALYSIS_ID(row.HEMODIALYSIS_ID);
                HemodialysisModel.MED_HEMO_RECIPEDataTable recipeDataTable = null;
                HemodialysisModel.MED_HEMO_RECIPERow recipeRow = null;
                ConfigModel.MED_COMMON_ITEMLISTRow purifierRow = null;
                HemodialysisModel.MED_CURE_MAINDataTable _cureMainDataTable = null;
                HemodialysisModel.MED_CURE_MAINRow _cureMainRow = null;

                if (!row.IsRECIPE_IDNull())
                {
                    recipeDataTable = this._hemodialysisService.GetRecipeByRecipeID(row.RECIPE_ID);
                    _cureMainDataTable = this._hemodialysisService.GetMainCureByRecipeId(row.RECIPE_ID);
                }

                if (recipeDataTable != null && recipeDataTable.Rows.Count > 0)
                    recipeRow = recipeDataTable.Rows[0] as HemodialysisModel.MED_HEMO_RECIPERow;

                if (_cureMainDataTable != null && _cureMainDataTable.Rows.Count > 0)
                    _cureMainRow = _cureMainDataTable.Rows[0] as HemodialysisModel.MED_CURE_MAINRow;

                if (!row.IsPURIFIER_MODEL_IDNull())
                    purifierRow = this._purifierDataTable.FindByITEM_ID(row.PURIFIER_MODEL_ID);

                CtlTreatmentPerson ctlTreatmentPerson = new CtlTreatmentPerson(bedRow, purifierRow, machineRow, patientRow, _cureMainRow, recipeRow, row, treamentDate);
                ctlTreatmentPerson.Name = Guid.NewGuid().ToString();
                ctlTreatmentPerson.ContainerPanelClick += new CtlTreatmentPerson.ContainerPanelClickEventHandler(ctlTreatmentPerson_ContainerPanelClick);
                ctlTreatmentPerson.ContainerPanelDoubleClick += new CtlTreatmentPerson.ContainerPanelDoubleClickEventHandler(ctlTreatmentPerson_ContainerPanelDoubleClick);

                this.pnlPatientTreat.Controls.Add(ctlTreatmentPerson);

                this._currentSelectedCtls.Add(ctlTreatmentPerson);
            }
        }

        #endregion

        //private int GetValueFromConfig(string name, int defaultValue)
        //{
        //    int result = defaultValue;

        //    if (this._alertParamsDataTable.Rows.Count > 0)
        //    {
        //        DataRow[] alertParamsRows = this._alertParamsDataTable.Select(string.Format("ITEM_NAME = '{0}'", name));

        //        if (alertParamsRows.Length > 0)
        //            result = Utility.CInt(alertParamsRows[0]["ITEM_VALUE"].ToString());
        //    }

        //    return result;
        //}

        //private bool CheckCurrentSelectedCtlSeleted()
        //{
        //    if (this._currentSelectedCtl == null)
        //    {
        //        XtraMessageBox.Show("请先选择病患信息！", "病患治疗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        //        return false;
        //    }
        //    else
        //        return true;
        //}

        /// <summary>
        /// 载入病人透析治疗单信息
        /// </summary>
        /// <param name="pHemoID"></param>
        public void LoadPatientCure(CtlTreatmentPerson pPerson)
        {
            int result = 0;
            DataSet ds = new DataSet();
            result = GetCureCountByCreateDate();
            string strName = string.Empty;
            string strHemoID = string.Empty;
            string strRecipeID = string.Empty;

            if (result != 0)
            {
                if (_currentSelectedCtl != null)
                {
                    strHemoID = _currentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
                    _cureTable = _hemodialysisService.GetCureList(TreamentDate, string.Empty, string.Empty, strHemoID);

                    if (_cureTable != null && _cureTable.Rows.Count > 0)
                    {
                        _strCureID = _cureTable.Rows[0]["CURE_ID"].ToString();
                        if (_strCureID.Length > 0)
                        {
                            ds = _hemodialysisService.GetAllCure(_strCureID);
                            loadDocumentGrid(ds, true);
                        }
                    }
                }
            }
            //治疗单无记录，加载处方信息。
            else
            {
                if (_currentSelectedCtl != null)
                {
                    if (!_currentSelectedCtl.PatientScheduleRow.IsRECIPE_IDNull() && _currentSelectedCtl.PatientScheduleRow.RECIPE_ID.ToString().Length > 0)
                    {
                        strRecipeID = _currentSelectedCtl.PatientScheduleRow.RECIPE_ID;
                        strHemoID = _currentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
                        ds = _hemodialysisService.GetRecipeAndPatientInfo(strHemoID, strRecipeID);
                        loadDocumentGrid(ds, false);
                    }
                    else
                    {
                        documentContainerHost.Child = null;
                        XtraMessageBox.Show("暂无患者的透析记录。", "主任工作站");
                    }
                }
            }
        }

        private void loadDocumentGrid(DataSet ds, bool isShow) {
            _medicalDocContainer.HaveNextPage = false;

            int countNum = 0;
            if (ds.Tables["MED_HEMODIALYSIS_PARAMETERS"] != null)
                countNum = ds.Tables["MED_HEMODIALYSIS_PARAMETERS"].Rows.Count;
            int countParam;
            string[] records = null;

            if (ds.Tables["MED_CURE_MAIN"] != null) {
                DataTable dtCureMain = ds.Tables["MED_CURE_MAIN"];
                records = dtCureMain.Rows[0]["SUMMARY2"].ToString().Split("|".ToCharArray());
            }

            if (countNum > 90) {
                CtlMedicalDocument document = new CtlMedicalDocument(ds, 90, 10);
                document.IsShowGrid(isShow);
                _medicalDocContainer.Add(document);
            }
            else {
                //CtlMedicalDocument document = new CtlMedicalDocument(ds,0,0);
                CtlMedicalDocument document = new CtlMedicalDocument(ds, countNum, 10);
                document.IsShowGrid(isShow);
                _medicalDocContainer.Add(document);
            }

            _medicalDocContainer.Remove("2");
            if (countNum > 10) {
                if (countNum < 31) {
                    CtlMedicalDocument3 document1 = new CtlMedicalDocument3(ds, countNum, countNum - 10, "", 2, string.Empty);
                    _medicalDocContainer.Add("2", document1);
                }
                else {
                    if (countNum > 90) {
                        countParam = 80;
                    }
                    else {
                        countParam = countNum - 10;
                    }

                    CtlMedicalDocument3 document1 = new CtlMedicalDocument3(ds, countParam, 20, "sqlByParams", 2, string.Empty);
                    _medicalDocContainer.Add("2", document1);
                }
            }
            else {
                if (records != null && records.Length >= 1 && records[0].Trim().Length > 0) {
                    CtlMedicalDocument3 document1 = new CtlMedicalDocument3(ds, countNum, countNum - 10, "", 2, string.Empty);
                    _medicalDocContainer.Add("2", document1);
                }
            }

            _medicalDocContainer.Remove("3");
            if (ds.Tables["MED_HEMODIALYSIS_PARAMETERS"] != null && ds.Tables["MED_HEMODIALYSIS_PARAMETERS"].Rows.Count > 30) {
                if (countNum < 51) {
                    CtlMedicalDocument3 document2 = new CtlMedicalDocument3(ds, countNum, countNum - 30, "", 3, string.Empty);
                    _medicalDocContainer.Add("3", document2);
                }
                else {
                    if (countNum > 90) {
                        countParam = 60;
                    }
                    else {
                        countParam = countNum - 30;
                    }
                    CtlMedicalDocument3 document2 = new CtlMedicalDocument3(ds, countParam, 20, "sqlByParams", 3, string.Empty);
                    _medicalDocContainer.Add("3", document2);
                }
            }
            else {
                if (records != null && records.Length >= 2 && records[1].Trim().Length > 0) {
                    CtlMedicalDocument3 document2 = new CtlMedicalDocument3(ds, countNum, countNum - 30, "", 3, string.Empty);
                    _medicalDocContainer.Add("3", document2);
                }
            }

            _medicalDocContainer.Remove("4");
            if (ds.Tables["MED_HEMODIALYSIS_PARAMETERS"] != null && ds.Tables["MED_HEMODIALYSIS_PARAMETERS"].Rows.Count > 50) {
                if (countNum < 71) {
                    CtlMedicalDocument3 document3 = new CtlMedicalDocument3(ds, countNum, countNum - 50, "", 4, string.Empty);
                    _medicalDocContainer.Add("4", document3);
                }
                else {
                    if (countNum > 90) {
                        countParam = 40;
                    }
                    else {
                        countParam = countNum - 50;
                    }
                    CtlMedicalDocument3 document3 = new CtlMedicalDocument3(ds, countParam, 20, "sqlByParams", 4, string.Empty);
                    _medicalDocContainer.Add("4", document3);
                }
            }
            else {
                if (records != null && records.Length >= 3 && records[2].Trim().Length > 0) {
                    CtlMedicalDocument3 document3 = new CtlMedicalDocument3(ds, countNum, countNum - 50, "", 4, string.Empty);
                    _medicalDocContainer.Add("4", document3);
                }
            }

            _medicalDocContainer.Remove("5");
            if (ds.Tables["MED_HEMODIALYSIS_PARAMETERS"] != null && ds.Tables["MED_HEMODIALYSIS_PARAMETERS"].Rows.Count > 70) {
                if (countNum < 91) {
                    CtlMedicalDocument3 document4 = new CtlMedicalDocument3(ds, countNum, countNum - 70, "", 5, string.Empty);
                    _medicalDocContainer.Add("5", document4);
                }
                else {
                    if (countNum > 90) {
                        countParam = 20;
                    }
                    else {
                        countParam = countNum - 70;
                    }
                    CtlMedicalDocument3 document4 = new CtlMedicalDocument3(ds, countParam, 20, "sqlByParams", 5, string.Empty);
                    _medicalDocContainer.Add("5", document4);
                }
            }
            else {
                if (records != null && records.Length >= 4 && records[3].Trim().Length > 0) {
                    CtlMedicalDocument3 document4 = new CtlMedicalDocument3(ds, countNum, countNum - 70, "", 5, string.Empty);
                    _medicalDocContainer.Add("5", document4);
                }
            }

            documentContainerHost.Child = _medicalDocContainer;
        }

        private string ConvertToString(object o)
        {
            if (o == null)
                return string.Empty;
            if (o == DBNull.Value || o is DBNull)
                return string.Empty;
            return o.ToString();
        }

        /// <summary>
        /// 根据病人透析号获取在治疗单是否存病人信息
        /// </summary>
        /// <returns></returns>
        public int GetCureCountByCreateDate()
        {
            int result = 0;
            string strHemoID = string.Empty;
            if (_currentSelectedCtl != null)
            {
                strHemoID = _currentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
                result = _hemodialysisService.GetMainCureCountByCreateDate(strHemoID, Utility.CDate(TreamentDate));
            }
            return result;
        }

        /// <summary>
        /// 验证是否开始治疗 
        /// </summary>
        //private bool checkBeginCure()
        //{
        //    int iCureCount = GetCureCountByCreateDate();
        //    if (iCureCount == 0)
        //    {
        //        XtraMessageBox.Show("请先开始治疗，在编辑治疗数据。", "主任工作站");
        //        return true;
        //    }
        //    return false;
        //}

        #endregion
    }
}