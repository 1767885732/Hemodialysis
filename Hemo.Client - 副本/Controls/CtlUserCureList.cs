/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：通用治疗单列表显示控件，显示治疗单日期列表，点击弹出对应的治疗单选项卡 
// 创建时间：2013-04-10
// 创建者：刘超
//  
// 修改时间:2017年9月28日
// 修改人:吕志强
// 修改描述:添加CRRT班次切换事件
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
using System.Drawing;
using Hemo.Client.UI.PatientFixUI;
using Hemo.Client.UI.Patient;
using Hemo.Client.UI.Assessment;
using System.Text;

namespace Hemo.Client.Controls
{
    public partial class CtlUserCureList : ViewBase
    {
        public class MessageInfo
        {
            public string PatientScheduleID
            {
                set;
                get;
            }

            public string Message
            {
                set;
                get;
            }
        }

        #region 变量

        private int _hoursPoint4SaveMessage;
        private int _minutesPoint4SaveMessage;
        private int _secondsPoint4SaveMessage;
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
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _alertParamsDataTable;

        private IList<CtlTreatmentPerson> _currentSelectedCtls = new List<CtlTreatmentPerson>();
        private IList<MessageInfo> _messageInfos = new List<MessageInfo>();

        private HemodialysisModel.MED_CURE_MAINDataTable _CureMainDatatable;

        private bool isPageChanged = false;

        private string currentExamLabItem = string.Empty;

        private string labFromTime = string.Empty;

        private string labToTime = string.Empty;

        private string examFromTime = string.Empty;

        private string examToTime = string.Empty;

        public delegate void BanciChangedEventHandler(object sender, BanciChangedEventArgs e);
        public event BanciChangedEventHandler BanciChanged;

        public delegate void DoubleClickEventHandler(object sender, DoubleClickEventArgs e);
        public event DoubleClickEventHandler DoubleClick;

        #endregion

        #region 属性

        public string CurrentExamLabItem
        {
            get { return currentExamLabItem; }
            set { currentExamLabItem = value; }
        }

        public string LabFromTime
        {
            get { return labFromTime; }
            set { labFromTime = value; }
        }

        public string LabToTime
        {
            get { return labToTime; }
            set { labToTime = value; }
        }

        public string ExamFromTime
        {
            get { return examFromTime; }
            set { examFromTime = value; }
        }

        public string ExamToTime
        {
            get { return examToTime; }
            set { examToTime = value; }
        }

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

        private string areaName;

        public string AreaName
        {
            get { return areaName; }
            set { areaName = value; }
        }

        public CtlTreatmentPerson CurrentSelectedCtl
        {
            get
            {
                return this._currentSelectedCtl;
            }
        }

        #endregion

        #region 初始化

        public CtlUserCureList()
        {
            this.InitializeComponent();

            this._bedDataTable = this._configService.GetConfigList(string.Empty, string.Empty, "床位", "1");
            this._purifierDataTable = this._configService.GetConfigList(string.Empty, string.Empty, "净化器类型", "1");
            this._machineDataTable = this._machineService.GetMachineList();
            this._patientDataTable = this._patientService.GetPatientList();
            this._alertParamsDataTable = this._configService.GetConfigList(string.Empty, string.Empty, "提醒参数设定", "1");
            this.BanciChanged += new BanciChangedEventHandler(Banci_Changed);
        }

        private void CtlUserCureList_Load(object sender, EventArgs e)
        {
            //this.LoadPatientTreatMainData();
            this._hoursPoint4SaveMessage = this.GetValueFromConfig("保存系统消息的时点", 0);
            this._minutesPoint4SaveMessage = this.GetValueFromConfig("保存系统消息的分点", 10);
            this._secondsPoint4SaveMessage = this.GetValueFromConfig("保存系统消息的秒点", 0);

            //载入透析单文档
            this._medicalDocContainer = new CtlMedicalDocumentContainer();

            this.timerSyncTime.Interval = this.GetValueFromConfig("治疗时间倒计时间隔（单位：毫秒）", 1000);
            //this.timerSaveMessage.Interval = this.GetValueFromConfig("保存系统消息间隔（单位：毫秒）", 1000);
            this.timerShowMsg.Interval = this.GetValueFromConfig("显示系统消息间隔（单位：毫秒）", 10000);

            //this.timerSyncTime.Enabled = this.timerSaveMessage.Enabled = this.timerShowMsg.Enabled = true;
            this.timerSyncTime.Enabled = this.timerShowMsg.Enabled = true;
        }

        #endregion

        #region 方法

        #region 载入治疗单方法

        /// <summary>
        /// 加载病人治疗数据
        /// </summary>
        public void LoadPatientTreatMainData()
        {
            this._currentSelectedCtl = null;
            PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable dtSchedule = null;
            DateTime treamentDate = TreamentDate == "" ? DateTime.Now.Date : Utility.CDate(TreamentDate).Date;
            dtSchedule = areaName.Equals("CRRT室") ? _patientScheduleService.GetPatientScheduleListByPara2(LoginUser.User.USER_ID, treamentDate, treamentDate) : _patientScheduleService.GetPatientScheduleList(LoginUser.User.USER_ID, treamentDate, treamentDate, Classes);
            this._patientScheduleDataTable = Utility.GetSubTable(dtSchedule, string.Format("DIALYSIS_ROOM_ID = '{0}'", Area)) as PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable;

            this.pnlPatientTreat.Controls.Clear();

            foreach (PatientScheduleModel.MED_PATIENT_SCHEDULERow row in this._patientScheduleDataTable.Rows)
            {
                ConfigModel.MED_COMMON_ITEMLISTRow bedRow = this._bedDataTable.FindByITEM_ID(row.BED_NUMBER);
                MachineModel.MED_DIALYSIS_MACHINERow machineRow = this._machineDataTable.FindByMACHINE_ID(row.MONITOR_LABEL);
                PatientModel.MED_PATIENTSRow patientRow = this._patientService.GetPatientListByParams(string.Empty, row.HEMODIALYSIS_ID)[0]; // this._patientDataTable.FindByHEMODIALYSIS_ID(row.HEMODIALYSIS_ID); 
                Logger.WriteErrorLogContet($"LoadPatientTreatMainData, patient_name is {row.PATIENTNAME}, HEMODIALYSIS_ID is {row.HEMODIALYSIS_ID}");
                Logger.WriteErrorLogContet($"LoadPatientTreatMainData, patientRow is {(patientRow == null ? "null" : patientRow.NAME)}");
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
                {
                    _cureMainRow = _cureMainDataTable.FirstOrDefault(i => i.IsRECIPE_TYPENull() || i.RECIPE_TYPE != "1");//.Rows[0] as HemodialysisModel.MED_CURE_MAINRow;
                }
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

        private int GetValueFromConfig(string name, int defaultValue)
        {
            int result = defaultValue;

            if (this._alertParamsDataTable.Rows.Count > 0)
            {
                DataRow[] alertParamsRows = this._alertParamsDataTable.Select(string.Format("ITEM_NAME = '{0}'", name));

                if (alertParamsRows.Length > 0)
                    result = Utility.CInt(alertParamsRows[0]["ITEM_VALUE"].ToString());
            }

            return result;
        }

        private bool CheckCurrentSelectedCtlSeleted()
        {
            if (this._currentSelectedCtl == null)
            {
                AutoClosedMsgBox.ShowForm("请先选择病患信息！", "系统提示", 2000, MessageBoxIcon.Warning);

                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// 载入病人透析治疗单信息
        /// </summary>
        /// <param name="pHemoID"></param>
        public void LoadPatientCure(CtlTreatmentPerson pPerson)
        {
            if (xtabl_1.SelectedTabPage != this.xtraTabPage1)
            {
                xtabl_1.SelectedTabPage = this.xtraTabPage1;
            }
            int result = 0;
            DataSet ds = new DataSet();
            result = GetCureCountByCreateDate();
            string strName = string.Empty;
            string strHemoID = string.Empty;
            string strRecipeID = string.Empty;
            //开始治疗，治疗单有记录
            if (result != 0)
            {
                if (_currentSelectedCtl != null)
                {
                    strHemoID = _currentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
                    DateTime tempDate = _currentSelectedCtl.CureMainRow != null ? _currentSelectedCtl.CureMainRow.CURE_CREATE_DATE : _currentSelectedCtl.PatientScheduleRow.DIALYSIS_DATE;
                    string createDate = _currentSelectedCtl.PatientScheduleRow.AREANAME.Equals("CRRT") ? tempDate.ToShortDateString() : TreamentDate;
                    string recipeId = (_currentSelectedCtl.CurentRecipeRow != null) ? _currentSelectedCtl.CurentRecipeRow.RECIPE_ID : (_currentSelectedCtl.CureMainRow != null ? _currentSelectedCtl.CureMainRow.RECIPE_ID : string.Empty);
                    //_cureTable = _hemodialysisService.GetCureList(createDate, string.Empty, string.Empty, strHemoID);
                    _cureTable = _hemodialysisService.GetMainCureByRecipeId(recipeId);

                    if (_cureTable != null && _cureTable.Rows.Count > 0)
                    {
                        _strCureID = _cureTable.Rows[0]["CURE_ID"].ToString();
                        if (_cureTable.Rows.Count > 1)
                        {
                            var row = _cureTable.AsEnumerable().FirstOrDefault(i => string.IsNullOrEmpty(i["RECIPE_TYPE"].ToString()) || !i["RECIPE_TYPE"].ToString().Equals("1"));
                            if (row != null) { _strCureID = row["CURE_ID"].ToString(); }
                        }
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
                    if (_currentSelectedCtl.PatientScheduleRow.RECIPE_ID.ToString().Length > 0)
                    {
                        strRecipeID = _currentSelectedCtl.PatientScheduleRow.RECIPE_ID;
                        strHemoID = _currentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
                        ds = _hemodialysisService.GetRecipeAndPatientInfo(strHemoID, strRecipeID);
                        loadDocumentGrid(ds, false);
                    }
                    else
                    {
                        AutoClosedMsgBox.ShowForm("请先确认患者透析医嘱。", "系统提示", 2000, MessageBoxIcon.Stop);
                    }
                }
            }
        }
        public const int WordPixel = 408;

        /// <summary>
        /// 获取字符横向所占像素数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int GetPixelb(string str, System.Drawing.Graphics currentGps)
        {
            System.Drawing.Font font;
            System.Windows.Forms.PictureBox pb = new System.Windows.Forms.PictureBox();
            System.Drawing.Graphics g = currentGps == null ? pb.CreateGraphics() : currentGps;// pb.CreateGraphics();
            g.PageUnit = System.Drawing.GraphicsUnit.Pixel;
            int len;
            if (Encoding.Default.GetByteCount(str) == 2)
            {
                font = new System.Drawing.Font("SimSun", 12, System.Drawing.GraphicsUnit.Pixel);
                len = (int)(Math.Round(g.MeasureString(str, font).Width) * 0.75);
            }
            else
            {
                font = new System.Drawing.Font("Arial", 12, System.Drawing.GraphicsUnit.Pixel);
                len = (int)(Math.Round(g.MeasureString(str, font).Width) * 0.7);
            }
            font.Dispose();
            font = null;
            g.Dispose();
            g = null;
            pb.Dispose();
            pb = null;
            return len;
        }
        /// <summary>
        /// 加载治疗单
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="isShow"></param>
        private void loadDocumentGrid(DataSet ds, bool isShow)
        {
            _medicalDocContainer.HaveNextPage = false;
            int countNum = 0;
            int countParam;
            int pageParamCount = 10;
            string[] records = null;
            HemodialysisModel.MED_CURE_MAIN_CRRTDataTable dtCRRTCure = null;

            if (ds.Tables["MED_HEMODIALYSIS_PARAMETERS"] != null)
            {
                if (areaName.Equals("CRRT室"))
                {
                    DateTime treamentDate = Utility.CDate(TreamentDate);
                    DateTime createDate = Classes.Equals("3") ? treamentDate.Date.AddDays(1) : treamentDate.Date;
                    var rows = ds.Tables["MED_HEMODIALYSIS_PARAMETERS"].AsEnumerable().Where(r => Utility.CDate(r["CREATE_DATE"].ToString()).Date.CompareTo(createDate) == 0 && r["CRRT_CLASS"].ToString().Equals(Classes));
                    countNum = rows != null ? rows.Count() : 0;
                }
                else
                {
                    countNum = ds.Tables["MED_HEMODIALYSIS_PARAMETERS"].Rows.Count;
                }
                #region 计算这些透析参数能够显示多少行。
                var dtHemoParametersTemp1 = ds.Tables["MED_HEMODIALYSIS_PARAMETERS"] as HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable;
                var dtHemoParameters = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();
                var dtHemoParametersTemp = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();
                dtHemoParametersTemp1.CopyToDataTable<HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSRow>(dtHemoParametersTemp, LoadOption.PreserveChanges);

                var itemCollet = new List<string>();
                int p = 0;
                foreach (HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSRow item in dtHemoParametersTemp.Rows)
                {
                    p = 0;
                    if (!item.IsCLINICAL_MANIFESTATIONNull())
                    {
                        List<string> cureResultList = new List<string>();
                        string cureResult = "";
                        string cureResultTotal = string.Empty;
                        //遍历循环数据是不是超出了一行所能放的区域
                        foreach (string str in item.CLINICAL_MANIFESTATION.Split('\r'))
                        {
                            cureResultTotal = "";
                            if (string.IsNullOrEmpty(str))
                            {
                                cureResultList.Add(cureResult);
                            }
                            else
                            {
                                StringBuilder strbuilder = new StringBuilder();
                                int totalLen = 0;
                                for (int i = 0; i < str.Length; i++)
                                {
                                    totalLen += GetPixelb(str[i].ToString(), null);
                                    if (totalLen < WordPixel)
                                    {
                                        strbuilder.Append(str[i]);
                                        if (i == str.Length - 1)
                                        {
                                            totalLen = 0;
                                            cureResultList.Add(strbuilder.ToString());
                                            strbuilder.Clear();
                                        }
                                    }
                                    else
                                    {
                                        totalLen = 0;
                                        i--;
                                        cureResultList.Add(strbuilder.ToString());
                                        strbuilder.Clear();
                                    }
                                }
                                strbuilder.Clear();
                                strbuilder = null;
                            }
                        }
                        string allStr = string.Empty;
                        foreach (string itemStr in cureResultList)
                        {
                            allStr += string.Format("{0}$", itemStr);
                        }

                        item.CLINICAL_MANIFESTATION = allStr;



                        var splitItem = item.CLINICAL_MANIFESTATION.Split('$');


                        foreach (var sitem in splitItem)
                        {
                            if (!string.IsNullOrEmpty(sitem))
                            {
                                p++;
                                item.CLINICAL_MANIFESTATION = sitem;
                                if (p == 1)
                                    dtHemoParameters.LoadDataRow(item.ItemArray, LoadOption.PreserveChanges);
                                else
                                {
                                    var dr = dtHemoParameters.NewMED_HEMODIALYSIS_PARAMETERSRow();
                                    dr.HEMODIALYSIS_PARAMETERS_ID = System.Guid.NewGuid().ToString();
                                    dr.CLINICAL_MANIFESTATION = sitem;
                                    dr.CURE_ID = item.CURE_ID;
                                    dr.RECIPE_ID = item.RECIPE_ID;
                                    dtHemoParameters.AddMED_HEMODIALYSIS_PARAMETERSRow(dr);
                                }
                            }
                        }
                    }
                    else
                    {
                        dtHemoParameters.LoadDataRow(item.ItemArray, LoadOption.PreserveChanges);
                    }
                }

                countNum = dtHemoParameters.Rows.Count;
                #endregion
            }





            if (ds.Tables["MED_CURE_MAIN"] != null)
            {
                DataTable dtCureMain = ds.Tables["MED_CURE_MAIN"];
                if (areaName.Equals("CRRT室"))
                {
                    DateTime treamentDate = Utility.CDate(TreamentDate);
                    DateTime createDate = Classes.Equals("3") ? treamentDate.Date.AddDays(1) : treamentDate.Date;
                    dtCRRTCure = _hemodialysisService.GetCRRTCureByCureIdAndBanci(dtCureMain.Rows[0]["CURE_ID"].ToString(), Classes, createDate);
                    if (dtCRRTCure != null && dtCRRTCure.Rows.Count > 0)
                    {
                        records = dtCRRTCure[0].SUMMARY2.Split("|".ToCharArray());
                    }
                }
                else
                {
                    records = dtCureMain.Rows[0]["SUMMARY2"].ToString().Split("|".ToCharArray());
                }
            }

            WPF_DocumentBase document = null;
            if (areaName.Equals("CRRT室"))
            {
                document = new CtlMedicalDocumentCRRTNew(_currentSelectedCtl.PatientScheduleRow, ds, countNum, 10, Classes, Classes.Equals("3") ? Utility.CDate(TreamentDate).AddDays(1) : Utility.CDate(TreamentDate));
                //countNum = countNum - document.currentParamNoShowInt;
                pageParamCount = document.paramRowNum;
            }
            else
            {
                document = new CtlMedicalDocumentNew(_currentSelectedCtl.PatientScheduleRow, ds);
                //countNum = countNum - document.currentParamNoShowInt;
                pageParamCount = document.paramRowNum;

            }
            document.IsShowGrid(isShow);
            _medicalDocContainer.Add(document);



            //进行计算分页
            //计算可以有多少页的参数页
            int pageCount = (countNum - pageParamCount) / 24;
            int pageCountExt = (countNum - pageParamCount) % 24;
            if (pageCountExt > 0)
            {
                pageCount++;
            }
            countParam = countNum - pageParamCount;

            for (int i = 2; i < pageCount + 2; i++)
            {
                _medicalDocContainer.Remove(i.ToString());

                string area = areaName.Equals("CRRT室") ? "CRRT" : areaName;
                CtlMedicalDocument3New document1 = area.Equals("CRRT") ? new CtlMedicalDocument3New(ds, (dtCRRTCure != null && dtCRRTCure.Rows.Count > 0) ? dtCRRTCure[0] : null, countParam, 20, 1, i, area) : new CtlMedicalDocument3New(ds, pageParamCount, 20, "sqlByParams", i, area);
                pageParamCount += 24;

                _medicalDocContainer.Add(i.ToString(), document1);
                //countParam = countParam - 20;
            }

            DataTable cureMainDataTable = ds.Tables["MED_CURE_MAIN"];

            if (cureMainDataTable != null)
            {
                string strSummary = cureMainDataTable.Rows[0]["SUMMARY"].ToString();

                string strSummary1 = cureMainDataTable.Rows[0]["SUMMARY2"].ToString().Replace("|||", "");

                //病情记录
                if (strSummary.Length > 164 || strSummary1.Length > 0)
                {
                    var lastDoc = pageCount + 2;
                    CtlMedicalDocument2 document2 = new CtlMedicalDocument2(ds);
                    _medicalDocContainer.Add(lastDoc.ToString(), document2);
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
                //strHemoID = _currentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
                //DateTime tempDate = _currentSelectedCtl.CureMainRow != null ? _currentSelectedCtl.CureMainRow.CURE_CREATE_DATE : _currentSelectedCtl.PatientScheduleRow.DIALYSIS_DATE;
                //DateTime createDate = _currentSelectedCtl.PatientScheduleRow.AREANAME.Equals("CRRT") ? tempDate : Utility.CDate(TreamentDate);
                //result = _hemodialysisService.GetMainCureCountByCreateDate(strHemoID, createDate);

                string recipeId = (_currentSelectedCtl.CurentRecipeRow != null) ? _currentSelectedCtl.CurentRecipeRow.RECIPE_ID : (_currentSelectedCtl.CureMainRow != null ? _currentSelectedCtl.CureMainRow.RECIPE_ID : string.Empty);
                var cure = _hemodialysisService.GetMainCureByRecipeId(recipeId);
                result = (cure != null && cure.Rows.Count > 0) ? cure.Rows.Count : result;
            }
            return result;
        }

        /// <summary>
        /// 验证是否开始治疗 
        /// </summary>
        private bool checkBeginCure()
        {
            int iCureCount = GetCureCountByCreateDate();
            if (iCureCount == 0)
            {
                AutoClosedMsgBox.ShowForm("请先开始治疗，再编辑治疗数据。", "系统提示", 2000, MessageBoxIcon.Stop);
                return true;
            }

            if (iCureCount > 0 && _strCureID.Length == 0)
            {
                AutoClosedMsgBox.ShowForm("请双击选择一位患者", "系统提示", 2000, MessageBoxIcon.Stop);
                return true;
            }
            return false;
        }

        private bool checkBeginCureNew()
        {
            int iCureCount = GetCureCountByCreateDate();
            if (iCureCount == 0)
            {

                return true;
            }

            if (iCureCount > 0 && _strCureID.Length == 0)
            {
                return true;
            }
            return false;
        }
        public void ToSaveCureData()
        {
            if (this.xtraTabPage2.Controls.Count != 0)
            {
                (this.xtraTabPage2.Controls[0] as EditTreatment).SaveData();
            }
        }
        public void DoTabPageFreash()
        {
            //透析治疗单
            if (this.xtabl_1.SelectedTabPage == this.xtraTabPage1)
            {
                if (_currentSelectedCtl != null)
                {
                    LoadPatientCure(_currentSelectedCtl);
                }
            }

            //透析处方
            if (this.xtabl_1.SelectedTabPage == this.xtraTabPage2 && this.xtraTabPage2.Controls.Count > 0)
            {
                if (!this.CheckCurrentSelectedCtlSeleted())
                    return;
                if (checkBeginCure())
                {
                    return;
                }
                (this.xtraTabPage2.Controls[0] as EditTreatment).PatientScheduleRow = this._currentSelectedCtl.PatientScheduleRow;
                (this.xtraTabPage2.Controls[0] as EditTreatment).currentRecipeID = this._currentSelectedCtl.PatientScheduleRow.RECIPE_ID;
                (this.xtraTabPage2.Controls[0] as EditTreatment).MachineRow = this._currentSelectedCtl.MachineRow;
                (this.xtraTabPage2.Controls[0] as EditTreatment).IsReplenishTreat = false;
                (this.xtraTabPage2.Controls[0] as EditTreatment).CureDate = Utility.CDate(TreamentDate).Date;
                (this.xtraTabPage2.Controls[0] as EditTreatment).Banci = Classes;
                (this.xtraTabPage2.Controls[0] as EditTreatment).LoadInfo(this._currentSelectedCtl.PatientRow.HEMODIALYSIS_ID, _strCureID, 0, 0);
            }
            else if (this.xtabl_1.SelectedTabPage == this.xtraTabPage2)
            {
                if (!this.CheckCurrentSelectedCtlSeleted())
                    return;
                if (checkBeginCure())
                {
                    return;
                }
                EditTreatment frmTreatment = new EditTreatment(this._currentSelectedCtl.PatientRow.HEMODIALYSIS_ID, _strCureID, 0, 0);
                frmTreatment.PatientScheduleRow = this._currentSelectedCtl.PatientScheduleRow;
                frmTreatment.MachineRow = this._currentSelectedCtl.MachineRow;
                frmTreatment.currentRecipeID = this._currentSelectedCtl.PatientScheduleRow.RECIPE_ID;
                frmTreatment.IsReplenishTreat = false;
                frmTreatment.CureDate = Utility.CDate(TreamentDate).Date;
                frmTreatment.Banci = Classes;
                frmTreatment.Width = 927;
                frmTreatment.Height = 525;
                frmTreatment.Dock = DockStyle.Fill;
                this.xtraTabPage2.Controls.Add(frmTreatment);
            }
            // edit 2017
            if (this.xtabl_1.SelectedTabPage == this.xtraTabPage9 && this.xtraTabPage3.Controls.Count > 0)
            {
                (this.xtraTabPage3.Controls[0] as CtlPatientSufficiency).CurrentPatient = this._currentSelectedCtl.PatientRow;
                (this.xtraTabPage3.Controls[0] as CtlPatientSufficiency).HemoId = this._currentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
                (this.xtraTabPage3.Controls[0] as CtlPatientSufficiency).Query();
            }
            else if (this.xtabl_1.SelectedTabPage == this.xtraTabPage9)
            {
                if (!this.CheckCurrentSelectedCtlSeleted())
                    return;
                if (checkBeginCure())
                {
                    return;
                }
                CtlPatientSufficiency frm2 = new CtlPatientSufficiency();
                frm2.CurrentPatient = this._currentSelectedCtl.PatientRow;
                frm2.HemoId = this._currentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
                frm2.Dock = DockStyle.Fill;
                this.xtraTabPage3.Controls.Add(frm2);
                frm2.Query();
            }

            if (this.xtabl_1.SelectedTabPage == this.xtraTabPage5)
            {
                if (!this.CheckCurrentSelectedCtlSeleted())
                    return;
                if (isPageChanged)
                {
                    xtraTabControl3_SelectedPageChanged(this.xtraTabControl3, new DevExpress.XtraTab.TabPageChangedEventArgs(null, this.xtraTabControl3.SelectedTabPage));
                }
                else
                {
                    //患者切换，刷新界面
                    if (this.xtraTabControl3.TabPages[0].Controls.Count > 0)
                    {
                        (this.xtraTabControl3.TabPages[0].Controls[0] as ExamLabItemUINew).HemoId = this._currentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
                        (this.xtraTabControl3.TabPages[0].Controls[0] as ExamLabItemUINew).PatientName = this._currentSelectedCtl.PatientRow.NAME;
                        (this.xtraTabControl3.TabPages[0].Controls[0] as ExamLabItemUINew).LoadResultList();
                    }
                    if (this.xtraTabControl3.TabPages[1].Controls.Count > 0)
                    {
                        (this.xtraTabControl3.TabPages[1].Controls[0] as ctlLabFrm).LoadLabInfo(this._currentSelectedCtl.PatientRow);
                    }
                    if (this.xtraTabControl3.TabPages[2].Controls.Count > 0)
                    {
                        (this.xtraTabControl3.TabPages[2].Controls[0] as ctlExamFrm).PatientRow = this._currentSelectedCtl.PatientRow;
                        (this.xtraTabControl3.TabPages[2].Controls[0] as ctlExamFrm).LoadExamInfo();
                    }
                }
            }

            if (this.xtabl_1.SelectedTabPage == this.xtraTabPage6 && this.xtraTabPage6.Controls.Count > 0)
            {
                if (!this.CheckCurrentSelectedCtlSeleted())
                    return;
                this.xtraTabPage6.Controls.Clear();
                PatientKnowBooksUI frm = new PatientKnowBooksUI();
                frm.AreaName = areaName;
                frm.BindDocTree(this._currentSelectedCtl.PatientRow);
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage6.Controls.Add(frm);
            }
            else if (this.xtabl_1.SelectedTabPage == this.xtraTabPage6)
            {
                if (!this.CheckCurrentSelectedCtlSeleted())
                    return;
                PatientKnowBooksUI frm = new PatientKnowBooksUI();
                frm.AreaName = areaName;
                frm.BindDocTree(this._currentSelectedCtl.PatientRow);
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage6.Controls.Add(frm);
                return;
            }

            if (this.xtabl_1.SelectedTabPage == this.xtraTabPage9 && this.xtraTabPage9.Controls.Count > 0)
            {
                if (!this.CheckCurrentSelectedCtlSeleted())
                    return;
            }
            else if (this.xtabl_1.SelectedTabPage == this.xtraTabPage9)
            {
                if (!this.CheckCurrentSelectedCtlSeleted())
                    return;
            }

            if (this.xtabl_1.SelectedTabPage == this.xtraTabPage4 && this.xtraTabPage4.Controls.Count > 0)
            {
                if (!this.CheckCurrentSelectedCtlSeleted())
                    return;
                if (_currentSelectedCtl.CureMainRow == null)
                {
                    AutoClosedMsgBox.ShowForm("请先开始治疗！", "系统提示", 2000, MessageBoxIcon.Warning);
                    return;
                }
                var currentHemoId = this._currentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
                var RecipeId = this._currentSelectedCtl.PatientScheduleRow.RECIPE_ID;
                var RecoderId = this._currentSelectedCtl.CureMainRow.CURE_ID;
                var IsCanEdit = _currentSelectedCtl.PatientScheduleRow.IsEND_TIMENull();
                var patientMaterialUi = (this.xtraTabPage4.Controls[0] as PatientMaterialDetailUI);
                var dt = _patientService.QueryPatientMaterialByParams(currentHemoId, DateTime.Now, DateTime.Now, RecipeId);
                patientMaterialUi.CurrentHemoId = currentHemoId;
                patientMaterialUi.RecipeId = RecipeId;
                patientMaterialUi.PackageCode = RecoderId;
                if (IsCanEdit)
                {
                    patientMaterialUi.Enabled = true;
                    patientMaterialUi.MaterialInfoVisabled = false;
                }
                else
                {
                    patientMaterialUi.Enabled = false;
                    patientMaterialUi.MaterialInfoVisabled = true;
                }

                patientMaterialUi.CurrentRecordRow = (dt != null && dt.Rows.Count > 0) ? dt[0] : null;
                patientMaterialUi.InzationDataDetailUi();
            }
            else if (this.xtabl_1.SelectedTabPage == this.xtraTabPage4)
            {
                if (!this.CheckCurrentSelectedCtlSeleted())
                    return;
                var currentHemoId = this._currentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
                var RecipeId = this._currentSelectedCtl.PatientScheduleRow.RECIPE_ID;
                var RecoderId = this._currentSelectedCtl.CureMainRow.CURE_ID;
                var IsCanEdit = _currentSelectedCtl.PatientScheduleRow.IsEND_TIMENull();
                PatientMaterialDetailUI patientMaterialUi = new PatientMaterialDetailUI();
                var dt = _patientService.QueryPatientMaterialByParams(currentHemoId, DateTime.Now, DateTime.Now, RecipeId);
                patientMaterialUi.CurrentHemoId = currentHemoId;
                patientMaterialUi.RecipeId = RecipeId;
                if (IsCanEdit)
                {
                    patientMaterialUi.Enabled = true;
                    patientMaterialUi.MaterialInfoVisabled = false;
                }
                else
                {
                    patientMaterialUi.Enabled = false;
                    patientMaterialUi.MaterialInfoVisabled = true;
                }
                patientMaterialUi.PackageCode = RecoderId;
                patientMaterialUi.CurrentRecordRow = (dt != null && dt.Rows.Count > 0) ? dt[0] : null;
                patientMaterialUi.InzationDataDetailUi();
                patientMaterialUi.Dock = DockStyle.Fill;
                this.xtraTabPage4.Controls.Add(patientMaterialUi);
            }
            else if (this.xtabl_1.SelectedTabPage == this.xtraTabPage9 && xtraTabPage9.Controls.Count > 0)
            {
                if (this._currentSelectedCtl.PatientRow != null)
                {
                    ChangeAllControlByPatientInfoTab_2(this._currentSelectedCtl.PatientRow);
                }
            }
            //切换基本信息
            if (this.xtabl_1.SelectedTabPage == this.xtraTabPage7 && this.xtraTabPage7.Controls.Count > 0)
            {
                if (!this.CheckCurrentSelectedCtlSeleted())
                    return;
                if (this._currentSelectedCtl.PatientRow == null)
                {
                    AutoClosedMsgBox.ShowForm("请双击选中患者！", "系统提示", 2000, MessageBoxIcon.Warning);
                    return;
                }
                (this.xtraTabPage7.Controls[0] as PatientInfoUI).Current = this._currentSelectedCtl.PatientRow;
                (this.xtraTabPage7.Controls[0] as PatientInfoUI).SetBtnCloseHide();

                (this.xtraTabPage7.Controls[0] as PatientInfoUI).InitalizeData();
            }
            else if (this.xtabl_1.SelectedTabPage == this.xtraTabPage7)
            {
                if (!this.CheckCurrentSelectedCtlSeleted())
                    return;
                if (this._currentSelectedCtl.PatientRow == null)
                {
                    AutoClosedMsgBox.ShowForm("请双击选中患者！", "系统提示", 2000, MessageBoxIcon.Warning);
                    return;
                }
                PatientInfoUI frm = new PatientInfoUI();
                frm.SetBtnCloseHide();
                frm.Current = this._currentSelectedCtl.PatientRow;
                frm.InitalizeData();
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage7.Controls.Add(frm);
                return;
            }
        }

        /// <summary>
        /// 执行班次改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DoBanciChanged(object sender, BanciChangedEventArgs e)
        {
            this.Classes = e.Banci;
            if (this.BanciChanged != null)
            {
                BanciChanged(sender, e);
            }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 点击弹出治疗单透析处方选项卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void itmNarBarMain_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            _strCureID = ((DevExpress.XtraNavBar.NavBarItem)(sender)).Tag.ToString();
            using (EditTreatment frmTreatment = new EditTreatment(HEMODIALYSIS_ID, _strCureID, 0, 0))
            {
                frmTreatment.currentRecipeID = this._currentSelectedCtl.PatientScheduleRow.RECIPE_ID;
                frmTreatment.IsReplenishTreat = false;
                // frmTreatment.ShowDialog();
            }
        }

        /// <summary>
        /// 病人卡片单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void ctlTreatmentPerson_ContainerPanelClick(object sender, ContainerPanelEventArgs args)
        {
            if (this._currentSelectedCtl != null)
                this._currentSelectedCtl.ClearSelectedEffect();

            this._currentSelectedCtl = (CtlTreatmentPerson)sender;

            this._currentSelectedCtl.SetSelectedEffect();
            // DoTabPageFreash();

            //if (!string.IsNullOrEmpty(_strCureID) && !this._currentSelectedCtl.PatientScheduleRow.IsEND_TIMENull()) {
            //    HemodialysisModel.MED_CURE_MAINDataTable _CureMainDatatableTemp = _hemodialysisService.GetMainCureByCureID(_strCureID);
            //    if (_CureMainDatatableTemp.Rows[0]["PRIMARY_NURSE"].ToString() == Hemo.Client.Core.HemoApplicationContext.Current.CurrentUser.EMP_NO || _CureMainDatatableTemp.Rows[0]["PRIMARY_NURSE"].ToString().Length == 0) {
            //        this.btnRecipe.Enabled = true;
            //        this.btnParams.Enabled = true;
            //        this.btnOrder.Enabled = true;
            //        this.btnSummary.Enabled = true;
            //    }
            //    else {
            //        this.btnRecipe.Enabled = false;
            //        this.btnParams.Enabled = false;
            //        this.btnOrder.Enabled = false;
            //        this.btnSummary.Enabled = false;
            //    }
            //}
        }

        private string hemoStr = string.Empty;
        void ctlTreatmentPerson_ContainerPanelDoubleClick(object sender, ContainerPanelEventArgs args)
        {
            this._currentSelectedCtl = (CtlTreatmentPerson)sender;
            if (hemoStr.Equals(_currentSelectedCtl.PatientRow.HEMODIALYSIS_ID))
            {
                if (!areaName.Equals("CRRT室"))
                {
                    return;
                }
            }
            hemoStr = _currentSelectedCtl.PatientRow.HEMODIALYSIS_ID;

            if (_currentSelectedCtl.PatientScheduleRow.IsRECIPE_IDNull())
            {
                AutoClosedMsgBox.ShowForm("该病人尚没有透析处方，不能进行治疗。", "系统提示", 2000, MessageBoxIcon.Stop);
                return;
            }

            if (DoubleClick != null)
            {
                DoubleClickEventArgs arg = new DoubleClickEventArgs();
                DoubleClick(this, arg);
                Classes = arg.Banci;
            }

            LoadPatientCure(_currentSelectedCtl);
            DoTabPageFreash();
        }

        /// <summary>
        /// 透析处方
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecipe_Click(object sender, EventArgs e)
        {
            if (!this.CheckCurrentSelectedCtlSeleted())
                return;

            if (checkBeginCure())
            {
                return;
            }
            this.xtabl_1.SelectedTabPageIndex = 1;
        }

        /// <summary>
        /// 临时医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (!this.CheckCurrentSelectedCtlSeleted())
                return;
            //OrderSearchFrm orderExecFrm = new OrderSearchFrm(this._currentSelectedCtl.PatientRow);
            //HemodialysisModel.MED_CURE_MAINRow cureRowData = null;
            //if (_cureTable.Rows.Count == 0) {
            //    _cureTable = _hemodialysisService.GetCureList(TreamentDate, "", "", _currentSelectedCtl.PatientRow.HEMODIALYSIS_ID);
            //}
            //if (_cureTable != null && _cureTable.Rows.Count > 0) {
            //    cureRowData = _cureTable.Rows[0] as HemodialysisModel.MED_CURE_MAINRow;
            //    Hemo.Client.UI.Order.OrderExecFrm orderExecFrm = new Hemo.Client.UI.Order.OrderExecFrm(Utility.CDate(TreamentDate), this._currentSelectedCtl.PatientRow, cureRowData);
            //    orderExecFrm.ShowDialog();
            //}
            //else {
            //    XtraMessageBox.Show("请先确认开始治疗，然后查看执行临时医嘱！", "透析治疗");
            //}

            if (checkBeginCure())
            {
                return;
            }
            this.xtabl_1.SelectedTabPageIndex = 3;
            return;
            using (EditTreatment frmTreatment = new EditTreatment(this._currentSelectedCtl.PatientRow.HEMODIALYSIS_ID, _strCureID, 1, 0))
            {
                frmTreatment.PatientScheduleRow = this._currentSelectedCtl.PatientScheduleRow;
                frmTreatment.MachineRow = this._currentSelectedCtl.MachineRow;
                frmTreatment.currentRecipeID = this._currentSelectedCtl.PatientScheduleRow.RECIPE_ID;
                frmTreatment.IsReplenishTreat = false;
                frmTreatment.CureDate = Utility.CDate(TreamentDate).Date;
                frmTreatment.Banci = Classes;
                //frmTreatment.ShowDialog();
                //if (frmTreatment.DialogResult == DialogResult.Yes)
                //{
                //    LoadPatientCure(_currentSelectedCtl);
                //}
            }
        }

        /// <summary>
        /// 检查化验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLab_Click(object sender, EventArgs e)
        {
            if (!this.CheckCurrentSelectedCtlSeleted())
                return;
            this.xtabl_1.SelectedTabPageIndex = 4;
        }

        /// <summary>
        /// 促红素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnErythropoietin_Click(object sender, EventArgs e)
        {
            if (!this.CheckCurrentSelectedCtlSeleted())
                return;

            if (checkBeginCure())
            {
                return;
            }
            if (string.IsNullOrEmpty(_strCureID))
                return;

            this.xtabl_1.SelectedTabPageIndex = 7;
            return;
            _CureMainDatatable = _hemodialysisService.GetMainCureByCureID(_strCureID);
            var row = _CureMainDatatable.Rows[0] as HemodialysisModel.MED_CURE_MAINRow;
            this._currentSelectedCtl.PatientRow.INPUT_CODE = row.REUSE_TIMES.ToString();
            this._currentSelectedCtl.PatientRow.EDUCATION = row.MACHINE_TYPE.ToString();
            ReUsableRecord _reUsableRecord = new ReUsableRecord(this._currentSelectedCtl.PatientRow);
            _reUsableRecord.ShowDialog();
        }

        /// <summary>
        /// 透析小结
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSummary_Click(object sender, EventArgs e)
        {

            if (!this.CheckCurrentSelectedCtlSeleted())
                return;

            if (checkBeginCure())
            {
                return;
            }
            this.xtabl_1.SelectedTabPageIndex = 6;
        }

        private void btnParams_Click(object sender, EventArgs e)
        {

            if (!this.CheckCurrentSelectedCtlSeleted())
                return;

            if (checkBeginCure())
            {
                return;
            }
            this.xtabl_1.SelectedTabPageIndex = 2;
            return;
        }

        private void timerSyncTime_Tick(object sender, EventArgs e)
        {
            var currentSelectedCtls = this._currentSelectedCtls.Where(c => c.IsAllowSyncTime).ToList();

            foreach (var currentSelectedCtl in currentSelectedCtls)
            {
                TimeSpan ts;

                currentSelectedCtl.SyncTime(out ts);

                if (ts.Hours == this._hoursPoint4SaveMessage
                    && ts.Minutes == this._minutesPoint4SaveMessage
                    && ts.Seconds == this._secondsPoint4SaveMessage)
                {
                    //this._messageInfos.Add(new MessageInfo()
                    //{
                    //    PatientScheduleID = currentSelectedCtl.PatientScheduleRow.PATIENT_SCHEDULE_ID,
                    //    Message = string.Format("{0} 距离透析结束还有不到 {1} 分钟！", currentSelectedCtl.PatientRow.NAME, ts.Minutes)
                    //});

                    HemodialysisModel.MED_COMMON_MESSAGEDataTable messageDataTable = new HemodialysisModel.MED_COMMON_MESSAGEDataTable();
                    HemodialysisModel.MED_COMMON_MESSAGERow messageRow = messageDataTable.NewMED_COMMON_MESSAGERow();

                    messageRow.MSG_ID = currentSelectedCtl.PatientScheduleRow.PATIENT_SCHEDULE_ID;
                    messageRow.MESSAGE = string.Format("{0} 距离透析结束还有不到 {1} 分钟！", currentSelectedCtl.PatientRow.NAME, ts.Minutes);
                    messageRow.TYPE = 1;
                    messageRow.STATUS = "1"; //1：未读；2：已读
                    messageRow.CREATETIME = DateTime.Now;

                    messageDataTable.AddMED_COMMON_MESSAGERow(messageRow);

                    //this._hemodialysisService.SaveMsgInfo(messageDataTable);
                }
            }
        }

        private void timerSaveMessage_Tick(object sender, EventArgs e)
        {
            //if (this._messageInfos.Count == 0)
            //    return;

            //HemodialysisModel.MED_COMMON_MESSAGEDataTable messageDataTable = new HemodialysisModel.MED_COMMON_MESSAGEDataTable();

            //for (int i = 0; i < this._messageInfos.Count; i++) {
            //    HemodialysisModel.MED_COMMON_MESSAGERow messageRow = messageDataTable.NewMED_COMMON_MESSAGERow();

            //    messageRow.MSG_ID = this._messageInfos[i].PatientScheduleID;
            //    messageRow.MESSAGE = this._messageInfos[i].Message;
            //    messageRow.TYPE = 1;
            //    messageRow.STATUS = "1"; //1：未读；2：已读
            //    messageRow.CREATETIME = DateTime.Now;

            //    messageDataTable.AddMED_COMMON_MESSAGERow(messageRow);

            //    this._messageInfos.RemoveAt(i);
            //}

            //if (messageDataTable.Rows.Count > 0)
            //    this._hemodialysisService.SaveMsgInfo(messageDataTable);
        }

        private void timerShowMsg_Tick(object sender, EventArgs e)
        {
            //var msgIDs = (from f in this.alertControl1.AlertFormList
            //              let tag = f.AlertInfo.Tag
            //              select tag == null ? string.Empty : tag.ToString()).ToList();
            //HemodialysisModel.MED_COMMON_MESSAGEDataTable messageDataTable = this._hemodialysisService.GetAllMessage(1);

            //foreach (HemodialysisModel.MED_COMMON_MESSAGERow messageRow in messageDataTable.Rows) {
            //    if (msgIDs.Contains(messageRow.MSG_ID))
            //        continue;

            //    AlertInfo info = new AlertInfo("透析提醒", messageRow.MESSAGE, messageRow.MESSAGE, null, messageRow.MSG_ID);

            //    this.alertControl1.Show(this.ParentForm, info);
            //}
        }

        private void alertControl1_ButtonClick(object sender, AlertButtonClickEventArgs e)
        {
            if (e.Info.Tag == null)
                return;

            switch (e.ButtonName.ToLower())
            {
                case "btnsave": //保存系统消息为已读
                    this._hemodialysisService.SaveMsgInfoToMarkRead(e.Info.Tag.ToString());

                    e.AlertForm.Close();
                    break;

                default:
                    e.AlertForm.Close();
                    break;
            }
        }

        /// <summary>
        /// 班次改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Banci_Changed(object sender, BanciChangedEventArgs e)
        {
            LoadPatientCure(_currentSelectedCtl);
            DoTabPageFreash();
        }

        #endregion

        private void timerShowOrders_Tick(object sender, EventArgs e)
        {
            //this.alertControl1.Dispose();
            //DataTable dt = this._hemodialysisService.GetUNExcuteOrdersbyData(TreamentDate == "" ? DateTime.Now.Date : Utility.CDate(TreamentDate).Date);
            //if (dt != null && dt.Rows.Count > 0) {
            //    string strCaption = string.Empty;
            //    foreach (DataRow msgRow in dt.Rows) {
            //        strCaption += string.Format("{0};", msgRow.ItemArray[0].ToString());
            //    }
            //    AlertInfo info = new AlertInfo("以下人员有未执行处方", strCaption, string.Empty);

            //    this.alertControl1.Show(this.ParentForm, info);
            //}
        }

        private void btn_Books_Click(object sender, EventArgs e)
        {
            if (!this.CheckCurrentSelectedCtlSeleted())
                return;
            this.xtabl_1.SelectedTabPageIndex = 5;
            //PatientKnowBooks FRM = new PatientKnowBooks();
            //FRM.AreaName = areaName;
            //FRM.BindDocTree(this._currentSelectedCtl.PatientRow);
            //FRM.ShowDialog();
        }

        public void SavePdfToServer()
        {
            try
            {
                var service = new EmrHost.EmrService();
                DateTime trDate = TreamentDate == "" ? DateTime.Now.Date : Utility.CDate(TreamentDate).Date;
                if (CurrentSelectedCtl != null)
                {
                    service.SaveEmrToServer(this._medicalDocContainer.GetPdfByte(), this.CurrentSelectedCtl.PatientRow.PATIENT_ID, trDate, "HemoRecord");

                }
                else
                {
                    AutoClosedMsgBox.ShowForm("请选择患者!", "系统提示", 2000, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnHealth_Click(object sender, EventArgs e)
        {
            if (!this.CheckCurrentSelectedCtlSeleted())
                return;
            this.xtabl_1.SelectedTabPageIndex = 8;
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            isPageChanged = true;
            DoTabPageFreash();
            isPageChanged = false;
        }

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {

            if (this._currentSelectedCtl.PatientRow != null)
            {
                ChangeAllControlByPatientInfoTab_2(this._currentSelectedCtl.PatientRow);
            }

        }

        public void ChangeAllControlByPatientInfoTab_2(PatientModel.MED_PATIENTSRow PatientDocRow)
        {
            //切换评估宣教
            if (this.xtraTabPage9.Controls.Count > 0 && this.xtraTabControl2.SelectedTabPage == this.xtraTabPage9)
            {
                (this.xtraTabPage9.Controls[0] as CtlPatientSufficiency).CurrentPatient = PatientDocRow;
                (this.xtraTabPage9.Controls[0] as CtlPatientSufficiency).HemoId = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage9.Controls[0] as CtlPatientSufficiency).Query();
            }
            else if (this.xtraTabControl2.SelectedTabPage == this.xtraTabPage9)
            {

                CtlPatientSufficiency frm = new CtlPatientSufficiency();
                frm.CurrentPatient = PatientDocRow;
                frm.HemoId = PatientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage9.Controls.Add(frm);
                frm.Query();
                return;

            }
            //切换患者透析充分性评估
            if (this.xtraTabPage10.Controls.Count > 0 && this.xtraTabControl2.SelectedTabPage == this.xtraTabPage10)
            {
                (this.xtraTabPage10.Controls[0] as PatientSufficiencyUI).CurrentHemoId = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage10.Controls[0] as PatientSufficiencyUI).CurrentHemoName = PatientDocRow.NAME;
                (this.xtraTabPage10.Controls[0] as PatientSufficiencyUI).LoadInfo();
            }
            else if (this.xtraTabControl2.SelectedTabPage == this.xtraTabPage10)
            {
                PatientSufficiencyUI frm = new PatientSufficiencyUI();
                frm.CurrentHemoId = PatientDocRow.HEMODIALYSIS_ID;
                frm.CurrentHemoName = PatientDocRow.NAME;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage10.Controls.Add(frm);
                frm.LoadInfo();
                return;
            }
            //切换患者营养评估
            if (this.xtraTabPage11.Controls.Count > 0 && this.xtraTabControl2.SelectedTabPage == this.xtraTabPage11)
            {
                (this.xtraTabPage11.Controls[0] as NutritionSGAUI).CurrentHemoId = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage11.Controls[0] as NutritionSGAUI).Query();
            }
            else if (this.xtraTabControl2.SelectedTabPage == this.xtraTabPage11)
            {
                NutritionSGAUI frm = new NutritionSGAUI();
                frm.CurrentHemoId = PatientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage11.Controls.Add(frm);
                frm.Query();
                return;
            }

            //切换患者风险评估
            if (this.xtraTabPage12.Controls.Count > 0 && this.xtraTabControl2.SelectedTabPage == this.xtraTabPage12)
            {
                (this.xtraTabPage12.Controls[0] as RiskAssessUI).InitalizeData();
            }
            else if (this.xtraTabControl2.SelectedTabPage == this.xtraTabPage12)
            {
                RiskAssessUI frm = new RiskAssessUI();
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage12.Controls.Add(frm);
                frm.InitalizeData();
                return;

            }
            //切换患者内瘘评估
            if (this.xtraTabPage13.Controls.Count > 0 && this.xtraTabControl2.SelectedTabPage == this.xtraTabPage13)
            {
                (this.xtraTabPage13.Controls[0] as QueryEstimateInBasketUI).HemoID = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage13.Controls[0] as QueryEstimateInBasketUI).queryData(PatientDocRow.HEMODIALYSIS_ID);
            }
            else if (this.xtraTabControl2.SelectedTabPage == this.xtraTabPage13)
            {

                QueryEstimateInBasketUI frm = new QueryEstimateInBasketUI();
                frm.HemoID = PatientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage13.Controls.Add(frm);
                frm.queryData(PatientDocRow.HEMODIALYSIS_ID);
                return;

            }
            //切换临时导管评估
            if (this.xtraTabPage14.Controls.Count > 0 && this.xtraTabControl2.SelectedTabPage == this.xtraTabPage14)
            {
                (this.xtraTabPage14.Controls[0] as QueryEstimateVenousListUI).IsTemp = true;
                (this.xtraTabPage14.Controls[0] as QueryEstimateVenousListUI).HemoId = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage14.Controls[0] as QueryEstimateVenousListUI).QueryData();
            }
            else if (this.xtraTabControl2.SelectedTabPage == this.xtraTabPage14)
            {

                QueryEstimateVenousListUI frm = new QueryEstimateVenousListUI();
                frm.IsTemp = true;
                frm.HemoId = PatientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage14.Controls.Add(frm);
                frm.QueryData();
                return;

            }
            //切换长期导管评估
            if (this.xtraTabPage15.Controls.Count > 0 && this.xtraTabControl2.SelectedTabPage == this.xtraTabPage15)
            {
                (this.xtraTabPage15.Controls[0] as QueryEstimateVenousListUI).IsTemp = false;
                (this.xtraTabPage15.Controls[0] as QueryEstimateVenousListUI).HemoId = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage15.Controls[0] as QueryEstimateVenousListUI).QueryData();
            }
            else if (this.xtraTabControl2.SelectedTabPage == this.xtraTabPage15)
            {

                QueryEstimateVenousListUI frm = new QueryEstimateVenousListUI();
                frm.IsTemp = false;
                frm.HemoId = PatientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage15.Controls.Add(frm);
                frm.QueryData();
                return;

            }
            //切换Kolcaba评估
            if (this.xtraTabPage16.Controls.Count > 0 && this.xtraTabControl2.SelectedTabPage == this.xtraTabPage16)
            {
                (this.xtraTabPage16.Controls[0] as PatientKolcabaUI).CurrentHemoId = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage16.Controls[0] as PatientKolcabaUI).CurrentHemoName = PatientDocRow.NAME;
                (this.xtraTabPage16.Controls[0] as PatientKolcabaUI).Query();
            }
            else if (this.xtraTabControl2.SelectedTabPage == this.xtraTabPage16)
            {

                PatientKolcabaUI frm = new PatientKolcabaUI();
                frm.CurrentHemoId = PatientDocRow.HEMODIALYSIS_ID;
                frm.CurrentHemoName = PatientDocRow.NAME;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage16.Controls.Add(frm);
                frm.Query();
                return;

            }
            //切换透析综合评估
            if (this.xtraTabPage17.Controls.Count > 0 && this.xtraTabControl2.SelectedTabPage == this.xtraTabPage17)
            {
                (this.xtraTabPage17.Controls[0] as AssessmentListUI).HemoID = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage17.Controls[0] as AssessmentListUI).patient = PatientDocRow;
                (this.xtraTabPage17.Controls[0] as AssessmentListUI).LoadInfo();
            }
            else if (this.xtraTabControl2.SelectedTabPage == this.xtraTabPage17)
            {
                AssessmentListUI frm = new AssessmentListUI();
                frm.HemoID = PatientDocRow.HEMODIALYSIS_ID;
                frm.patient = PatientDocRow;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage17.Controls.Add(frm);
                frm.LoadInfo();
                return;
            }
            //切换患者健康宣教
            if (this.xtraTabPage18.Controls.Count > 0 && this.xtraTabControl2.SelectedTabPage == this.xtraTabPage18)
            {
                (this.xtraTabPage18.Controls[0] as EditHealthEducationList).HEMODIALYSIS_ID = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage18.Controls[0] as EditHealthEducationList).LoadData(PatientDocRow.HEMODIALYSIS_ID);
            }
            else if (this.xtraTabControl2.SelectedTabPage == this.xtraTabPage18)
            {
                EditHealthEducationList frm = new EditHealthEducationList();
                frm.HEMODIALYSIS_ID = PatientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage18.Controls.Add(frm);
                frm.LoadData(PatientDocRow.HEMODIALYSIS_ID);
                return;
            }
        }

        private void xtraTabControl3_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page.Text == "肾科检验")
            {
                if (this.tpSKJY.Controls.Count > 0)
                    return;
                ExamLabItemUINew frm = new ExamLabItemUINew();
                frm.HemoId = _currentSelectedCtl.PatientRow.HEMODIALYSIS_ID;
                frm.PatientName = _currentSelectedCtl.PatientRow.NAME;
                frm.CurrentParent = this;
                frm.Dock = DockStyle.Fill;
                this.tpSKJY.Controls.Add(frm);
            }
            else if (e.Page.Text == "患者检验")
            {
                if (this.tpHZJY.Controls.Count > 0)
                    return;
                ctlLabFrm labFrm = new ctlLabFrm(_currentSelectedCtl.PatientRow);
                labFrm.CurrentParent = this;
                labFrm.Dock = DockStyle.Fill;
                this.tpHZJY.Controls.Add(labFrm);
            }
            else if (e.Page.Text == "患者检查")
            {
                if (this.tpHZJC.Controls.Count > 0)
                    return;
                ctlExamFrm frm = new ctlExamFrm();
                frm.PatientRow = _currentSelectedCtl.PatientRow;
                frm.CurrentParent = this;
                frm.Dock = DockStyle.Fill;
                this.tpHZJC.Controls.Add(frm);
            }
        }
    }

    /// <summary>
    /// 班次改变事件数据
    /// </summary>
    public class BanciChangedEventArgs : EventArgs
    {
        #region 属性

        public string Banci { get; set; }

        #endregion
    }

    /// <summary>
    /// 患者卡片双击事件数据
    /// </summary>
    public class DoubleClickEventArgs : EventArgs
    {
        #region 属性

        public string Banci { get; set; }

        #endregion
    }
}