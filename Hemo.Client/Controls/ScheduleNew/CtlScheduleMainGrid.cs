/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:提班控件
 * 创建标识:贺建操-2014年8月2日
 * 
 * 修改时间:2017年9月26日
 * 修改人:吕志强
 * 修改描述:更新CRRT透析室显示名称
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Hemo.Client.Controls.Schedule;
using Hemo.Client.Core;
using Hemo.Client.UI.PatientSchedule;
using Hemo.IService;
using Hemo.IService.Config;
using Hemo.IService.Machine;
using Hemo.IService.PatientSchedule;
using Hemo.IService.Permission;
using Hemo.Model;
using Hemo.Service;
using System.ComponentModel;
using Hemo.Client.UI.Patient;
using Hemo.Client.UI.Lab;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Hemo.Client.Controls.ScheduleNew;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using Hemo.Utilities;
using System.Collections;
using Hemo.Client.UI.Hemodialysis;
using System.Configuration;

namespace Hemo.Client.Controls.Schedule
{
    public partial class CtlScheduleMainGrid : XtraUserControl
    {
        #region 变量
        private PermissionModel.MED_DIALYSIS_MACHINEEXTENDDataTable _machineDataTable;

        private PermissionModel.MED_HEMO_SCHEDULEMASTERDataTable _ScheduleDataTableMain;

        public PermissionModel.MED_HEMO_SCHEDULEMASTERDataTable ScheduleDataTableMain
        {
            get { return _ScheduleDataTableMain; }
            set
            {
                _ScheduleDataTableMain = value;
                dateIsFromTemplate = true;
                #region 对于已经开始治疗的患者不能进行覆盖替换

                if (this.patientScheduleDataTable != null && this.patientScheduleDataTable.Rows.Count > 0)
                {
                    Dictionary<string, string> al = new Dictionary<string, string>();
                    foreach (PermissionModel.MED_HEMO_SCHEDULEMASTERRow row in _ScheduleDataTableMain.Rows)
                    {
                        foreach (PatientScheduleModel.MED_PATIENT_SCHEDULERow scheduleRow in this.patientScheduleDataTable.Rows)
                        {
                            try
                            {
                                if (row.TIMETABLEVALUE == scheduleRow.BANCI_ID && row.AREA_ID == scheduleRow.DIALYSIS_ROOM_ID && row.BED_ID == scheduleRow.BED_NUMBER)
                                {
                                    string weekstr = scheduleRow.DIALYSIS_DATE.DayOfWeek.ToString().ToUpper();
                                    if (weekstr == "SATURDAY")
                                    {
                                        weekstr = "STATURDAY";
                                    }
                                    if (!scheduleRow.IsSTART_TIMENull() || !scheduleRow.IsEND_TIMENull() || !scheduleRow.IsRECIPE_IDNull())
                                    {
                                        row[string.Format("{0}PATIENT", weekstr)] = scheduleRow.IsPATIENT_IDNull() ? string.Empty : scheduleRow.PATIENT_ID.ToString();
                                        row[string.Format("{0}REMARK", weekstr)] = scheduleRow.IsREMARKNull() ? string.Empty : scheduleRow.REMARK.ToString();
                                        row[string.Format("{0}HEMOID", weekstr)] = scheduleRow.HEMODIALYSIS_ID.ToString();
                                        row[string.Format("{0}_SCHEDULE_ID", weekstr)] = scheduleRow.PATIENT_SCHEDULE_ID.ToString();
                                        row[string.Format("{0}_START_TIME", weekstr)] = scheduleRow.IsSTART_TIMENull() ? string.Empty : scheduleRow.START_TIME.ToString();
                                        row[string.Format("{0}_END_TIME", weekstr)] = scheduleRow.IsEND_TIMENull() ? string.Empty : scheduleRow.END_TIME.ToString();
                                        row[string.Format("{0}_RECIPE_ID", weekstr)] = scheduleRow.IsRECIPE_IDNull() ? string.Empty : scheduleRow.RECIPE_ID;
                                        row[string.Format("{0}_PURIFIER_MODEL_ID", weekstr)] = scheduleRow.IsPURIFIER_MODEL_IDNull() ? string.Empty : scheduleRow.PURIFIER_MODEL_ID;
                                        row[string.Format("{0}_PURIFIER_ID", weekstr)] = scheduleRow.IsPURIFICATION_MODENull() ? string.Empty : scheduleRow.PURIFICATION_MODE;
                                        row[string.Format("{0}_IS_CRRT", weekstr)] = scheduleRow.IsIS_CRRTNull() ? string.Empty : scheduleRow.IS_CRRT;
                                        var rowPur = _purificationMondel.FindByITEM_ID(row[string.Format("{0}_PURIFIER_ID", weekstr)].ToString());
                                        string purString = "";
                                        if (rowPur != null)
                                        {
                                            purString = string.Format("{0}", rowPur["ITEM_NAME"]);
                                        }
                                        row[weekstr] = string.Format("{0} {1} {2}", scheduleRow.IsPATIENTNAMENull() ? string.Empty : scheduleRow.PATIENTNAME.ToString(), purString, scheduleRow.IsREMARKNull() ? string.Empty : scheduleRow.REMARK);
                                        al.Add(scheduleRow.HEMODIALYSIS_ID + weekstr, weekstr);

                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    if (al.Count > 0)
                    {
                        foreach (var item in al)
                        {
                            foreach (PermissionModel.MED_HEMO_SCHEDULEMASTERRow row in _ScheduleDataTableMain.Rows)
                            {
                                if (row[string.Format("{0}HEMOID", item.Value.ToString())].ToString() + item.Value == item.Key.ToString() && string.IsNullOrEmpty(row[string.Format("{0}_START_TIME", item.Value.ToString())].ToString()) && string.IsNullOrEmpty(row[string.Format("{0}_RECIPE_ID", item.Value.ToString())].ToString()))
                                {
                                    row[string.Format("{0}", item.Value.ToString())] = string.Empty;
                                    row[string.Format("{0}PATIENT", item.Value.ToString())] = string.Empty;
                                    row[string.Format("{0}REMARK", item.Value.ToString())] = string.Empty;
                                    row[string.Format("{0}HEMOID", item.Value.ToString())] = string.Empty;
                                    row[string.Format("{0}_SCHEDULE_ID", item.Value.ToString())] = string.Empty;
                                    row[string.Format("{0}_START_TIME", item.Value.ToString())] = string.Empty;
                                    row[string.Format("{0}_END_TIME", item.Value.ToString())] = string.Empty;
                                    row[string.Format("{0}_RECIPE_ID", item.Value.ToString())] = string.Empty;
                                    row[string.Format("{0}_PURIFIER_MODEL_ID", item.Value.ToString())] = string.Empty;
                                    row[string.Format("{0}_PURIFIER_ID", item.Value.ToString())] = string.Empty;
                                }
                            }
                        }
                    }


                }
                #endregion
                this.gridControlForEmerger.DataSource = _ScheduleDataTableMain;
                for (int i = -_areaDict.Count; i < 0; i++)
                {
                    this.gridView1.ExpandGroupRow(i);
                }
            }
        }
        private bool dateIsFromTemplate = false;
        public PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATERow _patientScheduleTemplateRow;
        private IUser _userService = ServiceManager.Instance.UserService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IMachine _machineService = ServiceManager.Instance.MachineService;
        private IPatient _patientService = ServiceManager.Instance.PatientService;
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;

        private Dictionary<ConfigModel.MED_COMMON_ITEMLISTRow, int> _areaDict;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _bedDataTable;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _banChiDateTable;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _purificationMondel;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _insulateArea;
        private PatientModel.MED_PATIENTSDataTable _patientDataTable;
        private HemodialysisModel.MED_HEMO_RECIPEDataTable _recipeDataTable;


        private ConfigModel.MED_COMMON_ITEMLISTDataTable _purifierModelDataTable;
        private PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable patientScheduleDataTable;
        private PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable _patientScheduleTemplateDataTable;


        /// <summary>
        /// 开始日期
        /// </summary>  
        private DateTime _beginDate;
        /// <summary>
        /// 结束日期
        /// </summary>
        private DateTime _endDate;
        private GridHitInfo hitInfo = null;


        public string currentOpenTemplateID { get; set; }
        public string currentOpenTemplateName { get; set; }

        /// <summary>
        /// 是否有脏数据。
        /// </summary>
        private bool _isDirty = false;
        public bool IsDirty
        {
            get { return _isDirty; }
            set { _isDirty = value; }
        }

        private static readonly string Blood_Hemo_Room = ConfigurationManager.AppSettings["Blood_Hemo_Room"].ToString();

        private static readonly string Head_Nurse = "10000107";

        #endregion

        #region 构造函数

        public CtlScheduleMainGrid()
        {
            this.InitializeComponent();
            this.Text = "患者排班";
            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountUI(this);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 数据初始化
        /// </summary>
        public void InizationData(bool isFromTemplate)
        {
            dateIsFromTemplate = false;

            BusyIndicatorHelp busyIndicatorHelp = new BusyIndicatorHelp();
            busyIndicatorHelp.ShowMessage();
            busyIndicatorHelp.SetWaitFormCaption("数据加载中....");


            this.gridView1.Columns["TIMETABLEVALUE"].Group();
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    //获取基本数据
                    //科室
                    this._areaDict = this._userService.GetAreaList(LoginUser.User.USER_ID).OrderByDescending(r => r.ORDER_NUMBER).ToDictionary(r => r, r => 200);
                    //床位
                    this._bedDataTable = this._configService.GetConfigList(string.Empty, string.Empty, "床位", "1");
                    //班次
                    this._banChiDateTable = this._configService.GetConfigList(string.Empty, string.Empty, "班次", "1");
                    //
                    this._purificationMondel = this._configService.GetConfigList(string.Empty, string.Empty, "净化方式", "1");
                    //隔离病区
                    this._insulateArea = this._configService.GetConfigList(string.Empty, string.Empty, "隔离病区", "1");
                    //已提班的人
                    if (!isFromTemplate)
                    {
                        var dtSchedule = this._patientScheduleService.GetPatientScheduleListByPara(LoginUser.User.USER_ID, this._beginDate, this._endDate);
                        var dtSubSchedule = dtSchedule.Where(s => 1 == 1);
                        //if (!HemoApplicationContext.Current.CurrentUser.EMP_NO.Equals(Head_Nurse))
                        //{
                        //    dtSubSchedule = Blood_Hemo_Room.Equals("5") ? dtSchedule.Where(s => s.AREANAME.Equals("透析室E区") || s.AREANAME.Equals("透析室F区") || s.AREANAME.Equals("透析室G区")) : dtSchedule.Where(s => !s.AREANAME.Equals("透析室E区") && !s.AREANAME.Equals("透析室F区") && !s.AREANAME.Equals("透析室G区"));
                        //}
                        if (dtSubSchedule.Any())
                        {
                            patientScheduleDataTable = dtSchedule.Clone() as PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable;
                            dtSubSchedule.CopyToDataTable(patientScheduleDataTable, LoadOption.OverwriteChanges);
                        }
                        else
                        {
                            patientScheduleDataTable = new PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable();
                        }
                    }
                    else { patientScheduleDataTable = new PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable(); }
                    this._machineDataTable = this._machineService.GetMachineListByUserID(LoginUser.User.USER_ID);
                    //this._patientDataTable = this._patientService.GetPatientList();
                    //处方信息
                    //this._recipeDataTable = this._hemodialysisService.GetAllRecipe();
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    try
                    {
                        #region 获取数据后对数据进行相应的转换处理后进行赋值
                        var _ScheduleDataTable = new PermissionModel.MED_HEMO_SCHEDULEMASTERDataTable();
                        foreach (ConfigModel.MED_COMMON_ITEMLISTRow row in this._banChiDateTable.Rows)
                        {
                            foreach (PermissionModel.MED_DIALYSIS_MACHINEEXTENDRow _machineRow in _machineDataTable.Rows)
                            {
                                //if (!HemoApplicationContext.Current.CurrentUser.EMP_NO.Equals(Head_Nurse))
                                //{
                                //    if (Blood_Hemo_Room.Equals("5"))
                                //    {
                                //        if (!_machineRow.QYNAME.Equals("透析室E区") && !_machineRow.QYNAME.Equals("透析室F区") && !_machineRow.QYNAME.Equals("透析室G区"))
                                //        {
                                //            continue;
                                //        }
                                //    }
                                //    else
                                //    {
                                //        if (_machineRow.QYNAME.Equals("透析室E区") || _machineRow.QYNAME.Equals("透析室F区") || _machineRow.QYNAME.Equals("透析室G区"))
                                //        {
                                //            continue;
                                //        }
                                //    }
                                //}
                                var scheduleRow = _ScheduleDataTable.NewMED_HEMO_SCHEDULEMASTERRow();
                                scheduleRow.TIMETABLEID = row.ITEM_ID;
                                scheduleRow.TIMETABLEVALUE = row.ITEM_VALUE;
                                scheduleRow.TIMETABLENAME = row.ITEM_NAME;
                                scheduleRow.MACHINE_ID = _machineRow.MACHINE_ID;
                                scheduleRow.MACHINE_NAME = _machineRow.MACHINE_NAME;
                                scheduleRow.MACHINE_MODEL = _machineRow.MACHINE_MODEL;
                                scheduleRow.TYPE = _machineRow.TYPE;
                                scheduleRow.THERAPEUTIC_PROPERTIES = _machineRow.THERAPEUTIC_PROPERTIES;
                                scheduleRow.OTHER_THERAPEUTIC = ConvertToString(_machineRow.OTHER_THERAPEUTIC);
                                scheduleRow.SUPPLIER = ConvertToString(_machineRow.SUPPLIER);
                                scheduleRow.AREA_ID = _machineRow.AREA_ID;
                                scheduleRow.BED_ID = _machineRow.BED_ID;
                                scheduleRow.FLNAME = _machineRow.FLNAME;
                                scheduleRow.QYNAME = _machineRow.QYNAME;
                                scheduleRow.QYVALUE = _machineRow.QYVALUE;
                                scheduleRow.CWNAME = _machineRow.CWNAME;
                                scheduleRow.CWVALUE = _machineRow.CWVALUE;
                                if (scheduleRow.QYNAME.Equals("CRRT"))
                                {
                                    if (row.ITEM_VALUE.Equals("1"))
                                    {
                                        scheduleRow.QYNAME = scheduleRow.QYNAME + "白天";
                                    }
                                    else if (row.ITEM_VALUE.Equals("2"))
                                    {
                                        scheduleRow.QYNAME = scheduleRow.QYNAME + "小夜";
                                    }
                                    else if (row.ITEM_VALUE.Equals("3"))
                                    {
                                        scheduleRow.QYNAME = scheduleRow.QYNAME + "大夜";
                                    }
                                }
                                _ScheduleDataTable.AddMED_HEMO_SCHEDULEMASTERRow(scheduleRow);
                            }
                        }
                        //复制排序
                        _ScheduleDataTableMain = new PermissionModel.MED_HEMO_SCHEDULEMASTERDataTable();
                        _ScheduleDataTable.OrderBy(i => i.TIMETABLEVALUE).ThenBy(i => i.QYVALUE).ThenBy(i => int.Parse(i.CWVALUE)).CopyToDataTable<PermissionModel.MED_HEMO_SCHEDULEMASTERRow>(_ScheduleDataTableMain, LoadOption.OverwriteChanges);

                        #endregion

                        #region 对于已排班的人员加载到排班显示中
                        foreach (PermissionModel.MED_HEMO_SCHEDULEMASTERRow row in _ScheduleDataTableMain.Rows)
                        {
                            foreach (PatientScheduleModel.MED_PATIENT_SCHEDULERow scheduleRow in this.patientScheduleDataTable.Rows)
                            {
                                if (row.AREA_ID == scheduleRow.DIALYSIS_ROOM_ID && row.BED_ID == scheduleRow.BED_NUMBER && row.TIMETABLEVALUE == scheduleRow.BANCI_ID)
                                {

                                    #region new code jg
                                    string weekstr = scheduleRow.DIALYSIS_DATE.DayOfWeek.ToString().ToUpper();
                                    if (weekstr == "SATURDAY")
                                    {
                                        weekstr = "STATURDAY";
                                    }
                                    row[string.Format("{0}PATIENT", weekstr)] = scheduleRow.PATIENT_ID.ToString();
                                    row[string.Format("{0}REMARK", weekstr)] = scheduleRow.REMARK.ToString();
                                    row[string.Format("{0}HEMOID", weekstr)] = scheduleRow.HEMODIALYSIS_ID;
                                    row[string.Format("{0}_SCHEDULE_ID", weekstr)] = scheduleRow.PATIENT_SCHEDULE_ID;
                                    row[string.Format("{0}_START_TIME", weekstr)] = scheduleRow.IsSTART_TIMENull() ? string.Empty : scheduleRow.START_TIME.ToString();
                                    row[string.Format("{0}_END_TIME", weekstr)] = scheduleRow.IsEND_TIMENull() ? string.Empty : scheduleRow.END_TIME.ToString();
                                    row[string.Format("{0}_RECIPE_ID", weekstr)] = scheduleRow.IsRECIPE_IDNull() ? string.Empty : scheduleRow.RECIPE_ID;
                                    row[string.Format("{0}_PURIFIER_ID", weekstr)] = scheduleRow.IsPURIFICATION_MODENull() ? string.Empty : scheduleRow.PURIFICATION_MODE;
                                    row[string.Format("{0}_IS_CRRT", weekstr)] = scheduleRow.IsIS_CRRTNull() ? string.Empty : scheduleRow.IS_CRRT;
                                    var rowPur = _purificationMondel.FindByITEM_ID(row[string.Format("{0}_PURIFIER_ID", weekstr)].ToString());
                                    string purString = "";
                                    if (rowPur != null)
                                    {
                                        purString = string.Format("{0}", rowPur["ITEM_NAME"]);
                                    }
                                    row[weekstr] = string.Format("{0} {1} {2}", scheduleRow.PATIENTNAME.ToString(), purString, scheduleRow.REMARK);

                                    #endregion

                                }
                            }
                        }
                        #endregion
                        this.gridControlForEmerger.DataSource = _ScheduleDataTableMain;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    for (int i = -_areaDict.Count; i < 0; i++)
                    {
                        this.gridView1.ExpandGroupRow(i);
                    }
                    //this.gridView1.ExpandGroupRow(-1);
                    //this.gridView1.ExpandGroupRow(-2);
                    //this.gridView1.ExpandGroupRow(-3);
                    //this.gridView1.ExpandGroupRow(-4);
                    busyIndicatorHelp.HideMessage();
                };
                worker.RunWorkerAsync();
            }

        }
        /// <summary>
        /// 对像转换
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private string ConvertToString(object o)
        {
            if (o == null)
                return string.Empty;
            if (o == DBNull.Value || o is DBNull)
                return string.Empty;
            return o.ToString();
        }
        /// <summary>
        /// 加载当前所在星期的时间段
        /// </summary>
        /// <param name="beginDate">星期一</param>
        /// <param name="endDate">星期日</param>
        public void LoadPatientScheduleData(DateTime beginDate, DateTime endDate)
        {
            this._beginDate = beginDate;
            this._endDate = endDate;
            #region 赋值日期，gridView的columnsName
            //对于控件的列头进行变换赋值
            this.gridMonday.Caption = string.Format("星期一（{0}）", beginDate.ToString("yyyy-MM-dd"));
            this.gridMonday.Tag = beginDate;
            DateTime dialysisDate = beginDate.AddDays(1);
            this.gridTuesday.Caption = string.Format("星期二（{0}）", dialysisDate.ToString("yyyy-MM-dd"));
            this.gridTuesday.Tag = dialysisDate;
            dialysisDate = beginDate.AddDays(2);
            this.gridWednesday.Caption = string.Format("星期三（{0}）", dialysisDate.ToString("yyyy-MM-dd"));
            this.gridWednesday.Tag = dialysisDate;
            dialysisDate = beginDate.AddDays(3);
            this.gridThursday.Caption = string.Format("星期四（{0}）", dialysisDate.ToString("yyyy-MM-dd"));
            this.gridThursday.Tag = dialysisDate;
            dialysisDate = beginDate.AddDays(4);
            this.gridFriday.Caption = string.Format("星期五（{0}）", dialysisDate.ToString("yyyy-MM-dd"));
            this.gridFriday.Tag = dialysisDate;
            dialysisDate = beginDate.AddDays(5);
            this.gridSaturday.Caption = string.Format("星期六（{0}）", dialysisDate.ToString("yyyy-MM-dd"));
            this.gridSaturday.Tag = dialysisDate;

            this.gridSunday.Caption = string.Format("星期日（{0}）", endDate.ToString("yyyy-MM-dd"));
            this.gridSunday.Tag = endDate;

            #endregion

        }

        /// <summary>
        /// 保存模板数据
        /// </summary>
        public void SaveToScheduleTemplate(bool isRename)
        {
            _ScheduleDataTableMain.AcceptChanges();

            //模板的ID
            string PATIENT_SCHEDULE_TEMPLATE_ID = Guid.NewGuid().ToString();
            //保存要保存的数表
            _patientScheduleTemplateDataTable = new PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable();
            foreach (PermissionModel.MED_HEMO_SCHEDULEMASTERRow row in _ScheduleDataTableMain.Rows)
            {
                #region 进行显示与数据的行列转换,写入新表进行保存
                //0~7表示星期一到星期日
                for (int i = 0; i < 7; i++)
                {
                    switch (i.ToString())
                    {
                        case "0":
                            {
                                if (row.IsMONDAYHEMOIDNull() || string.IsNullOrEmpty(row.MONDAYHEMOID))
                                    continue;
                                var patientScheduleTemplateDataRow = _patientScheduleTemplateDataTable.NewMED_PATIENT_SCHEDULE_TEMP_DATARow();
                                patientScheduleTemplateDataRow.MED_SCHEDULE_TEMPLATE_DATA_ID = Guid.NewGuid().ToString();
                                patientScheduleTemplateDataRow.PATIENT_SCHEDULE_TEMPLATE_ID = PATIENT_SCHEDULE_TEMPLATE_ID;
                                patientScheduleTemplateDataRow.MONITOR_LABEL = ConvertToString(row.MACHINE_ID);
                                patientScheduleTemplateDataRow.BANCI_ID = row.TIMETABLEVALUE;
                                patientScheduleTemplateDataRow.DIALYSIS_ROOM_ID = ConvertToString(row.AREA_ID);
                                patientScheduleTemplateDataRow.BED_NUMBER = ConvertToString(row.BED_ID);
                                patientScheduleTemplateDataRow.STATUS = "1";
                                patientScheduleTemplateDataRow.PATIENT_ID = row.MONDAYPATIENT;
                                patientScheduleTemplateDataRow.PURIFICATION_MODE = row.MONDAY_PURIFIER_ID;
                                patientScheduleTemplateDataRow.DIALYSIS_DATE = DateTime.Parse(this.gridMonday.Tag.ToString());
                                patientScheduleTemplateDataRow.HEMODIALYSIS_ID = ConvertToString(row.MONDAYHEMOID);
                                patientScheduleTemplateDataRow.REMARK = ConvertToString(row.MONDAYREMARK);
                                _patientScheduleTemplateDataTable.AddMED_PATIENT_SCHEDULE_TEMP_DATARow(patientScheduleTemplateDataRow);
                                break;
                            }
                        case "1":
                            {
                                if (row.IsTUESDAYHEMOIDNull() || string.IsNullOrEmpty(row.TUESDAYHEMOID))
                                    continue;
                                var patientScheduleTemplateDataRow = _patientScheduleTemplateDataTable.NewMED_PATIENT_SCHEDULE_TEMP_DATARow();
                                patientScheduleTemplateDataRow.MED_SCHEDULE_TEMPLATE_DATA_ID = Guid.NewGuid().ToString();
                                patientScheduleTemplateDataRow.PATIENT_SCHEDULE_TEMPLATE_ID = PATIENT_SCHEDULE_TEMPLATE_ID;
                                patientScheduleTemplateDataRow.MONITOR_LABEL = ConvertToString(row.MACHINE_ID);
                                patientScheduleTemplateDataRow.BANCI_ID = row.TIMETABLEVALUE;
                                patientScheduleTemplateDataRow.DIALYSIS_ROOM_ID = ConvertToString(row.AREA_ID);
                                patientScheduleTemplateDataRow.BED_NUMBER = ConvertToString(row.BED_ID);
                                patientScheduleTemplateDataRow.STATUS = "1";
                                patientScheduleTemplateDataRow.PURIFICATION_MODE = row.TUESDAY_PURIFIER_ID;
                                patientScheduleTemplateDataRow.PATIENT_ID = row.TUESDAYPATIENT;
                                patientScheduleTemplateDataRow.DIALYSIS_DATE = DateTime.Parse(this.gridTuesday.Tag.ToString());
                                patientScheduleTemplateDataRow.HEMODIALYSIS_ID = ConvertToString(row.TUESDAYHEMOID);
                                patientScheduleTemplateDataRow.REMARK = ConvertToString(row.TUESDAYREMARK);
                                _patientScheduleTemplateDataTable.AddMED_PATIENT_SCHEDULE_TEMP_DATARow(patientScheduleTemplateDataRow);
                                break;
                            }
                        case "2":
                            {
                                if (row.IsWEDNESDAYHEMOIDNull() || string.IsNullOrEmpty(row.WEDNESDAYHEMOID))
                                    continue;
                                var patientScheduleTemplateDataRow = _patientScheduleTemplateDataTable.NewMED_PATIENT_SCHEDULE_TEMP_DATARow();
                                patientScheduleTemplateDataRow.MED_SCHEDULE_TEMPLATE_DATA_ID = Guid.NewGuid().ToString();
                                patientScheduleTemplateDataRow.PATIENT_SCHEDULE_TEMPLATE_ID = PATIENT_SCHEDULE_TEMPLATE_ID;
                                patientScheduleTemplateDataRow.MONITOR_LABEL = ConvertToString(row.MACHINE_ID);
                                patientScheduleTemplateDataRow.BANCI_ID = row.TIMETABLEVALUE;
                                patientScheduleTemplateDataRow.DIALYSIS_ROOM_ID = ConvertToString(row.AREA_ID);
                                patientScheduleTemplateDataRow.BED_NUMBER = ConvertToString(row.BED_ID);
                                patientScheduleTemplateDataRow.STATUS = "1";
                                patientScheduleTemplateDataRow.PURIFICATION_MODE = row.WEDNESDAY_PURIFIER_ID;
                                patientScheduleTemplateDataRow.PATIENT_ID = row.WEDNESDAYPATIENT;
                                patientScheduleTemplateDataRow.DIALYSIS_DATE = DateTime.Parse(this.gridWednesday.Tag.ToString());
                                patientScheduleTemplateDataRow.HEMODIALYSIS_ID = ConvertToString(row.WEDNESDAYHEMOID);
                                patientScheduleTemplateDataRow.REMARK = ConvertToString(row.WEDNESDAYREMARK);
                                _patientScheduleTemplateDataTable.AddMED_PATIENT_SCHEDULE_TEMP_DATARow(patientScheduleTemplateDataRow);
                                break;
                            }
                        case "3":
                            {
                                if (row.IsTHURSDAYHEMOIDNull() || string.IsNullOrEmpty(row.THURSDAYHEMOID))
                                    continue;
                                var patientScheduleTemplateDataRow = _patientScheduleTemplateDataTable.NewMED_PATIENT_SCHEDULE_TEMP_DATARow();
                                patientScheduleTemplateDataRow.MED_SCHEDULE_TEMPLATE_DATA_ID = Guid.NewGuid().ToString();
                                patientScheduleTemplateDataRow.PATIENT_SCHEDULE_TEMPLATE_ID = PATIENT_SCHEDULE_TEMPLATE_ID;
                                patientScheduleTemplateDataRow.MONITOR_LABEL = ConvertToString(row.MACHINE_ID);
                                patientScheduleTemplateDataRow.BANCI_ID = row.TIMETABLEVALUE;
                                patientScheduleTemplateDataRow.DIALYSIS_ROOM_ID = ConvertToString(row.AREA_ID);
                                patientScheduleTemplateDataRow.BED_NUMBER = ConvertToString(row.BED_ID);
                                patientScheduleTemplateDataRow.STATUS = "1";
                                patientScheduleTemplateDataRow.PURIFICATION_MODE = row.THURSDAY_PURIFIER_ID;
                                patientScheduleTemplateDataRow.PATIENT_ID = row.THURSDAYPATIENT;
                                patientScheduleTemplateDataRow.DIALYSIS_DATE = DateTime.Parse(this.gridThursday.Tag.ToString());
                                patientScheduleTemplateDataRow.HEMODIALYSIS_ID = ConvertToString(row.THURSDAYHEMOID);
                                patientScheduleTemplateDataRow.REMARK = ConvertToString(row.THURSDAYREMARK);
                                _patientScheduleTemplateDataTable.AddMED_PATIENT_SCHEDULE_TEMP_DATARow(patientScheduleTemplateDataRow);
                                break;
                            }
                        case "4":
                            {
                                if (row.IsFRIDAYHEMOIDNull() || string.IsNullOrEmpty(row.FRIDAYHEMOID))
                                    continue;
                                var patientScheduleTemplateDataRow = _patientScheduleTemplateDataTable.NewMED_PATIENT_SCHEDULE_TEMP_DATARow();
                                patientScheduleTemplateDataRow.MED_SCHEDULE_TEMPLATE_DATA_ID = Guid.NewGuid().ToString();
                                patientScheduleTemplateDataRow.PATIENT_SCHEDULE_TEMPLATE_ID = PATIENT_SCHEDULE_TEMPLATE_ID;
                                patientScheduleTemplateDataRow.MONITOR_LABEL = ConvertToString(row.MACHINE_ID);
                                patientScheduleTemplateDataRow.BANCI_ID = row.TIMETABLEVALUE;
                                patientScheduleTemplateDataRow.DIALYSIS_ROOM_ID = ConvertToString(row.AREA_ID);
                                patientScheduleTemplateDataRow.BED_NUMBER = ConvertToString(row.BED_ID);
                                patientScheduleTemplateDataRow.STATUS = "1";
                                patientScheduleTemplateDataRow.PURIFICATION_MODE = row.FRIDAY_PURIFIER_ID;
                                patientScheduleTemplateDataRow.PATIENT_ID = row.FRIDAYPATIENT;
                                patientScheduleTemplateDataRow.DIALYSIS_DATE = DateTime.Parse(this.gridFriday.Tag.ToString());
                                patientScheduleTemplateDataRow.HEMODIALYSIS_ID = ConvertToString(row.FRIDAYHEMOID);
                                patientScheduleTemplateDataRow.REMARK = ConvertToString(row.FRIDAYREMARK);
                                _patientScheduleTemplateDataTable.AddMED_PATIENT_SCHEDULE_TEMP_DATARow(patientScheduleTemplateDataRow);
                                break;
                            }
                        case "5":
                            {
                                if (row.IsSTATURDAYHEMOIDNull() || string.IsNullOrEmpty(row.STATURDAYHEMOID))
                                    continue;
                                var patientScheduleTemplateDataRow = _patientScheduleTemplateDataTable.NewMED_PATIENT_SCHEDULE_TEMP_DATARow();
                                patientScheduleTemplateDataRow.MED_SCHEDULE_TEMPLATE_DATA_ID = Guid.NewGuid().ToString();
                                patientScheduleTemplateDataRow.PATIENT_SCHEDULE_TEMPLATE_ID = PATIENT_SCHEDULE_TEMPLATE_ID;
                                patientScheduleTemplateDataRow.MONITOR_LABEL = ConvertToString(row.MACHINE_ID);
                                patientScheduleTemplateDataRow.BANCI_ID = row.TIMETABLEVALUE;
                                patientScheduleTemplateDataRow.DIALYSIS_ROOM_ID = ConvertToString(row.AREA_ID);
                                patientScheduleTemplateDataRow.BED_NUMBER = ConvertToString(row.BED_ID);
                                patientScheduleTemplateDataRow.STATUS = "1";
                                patientScheduleTemplateDataRow.PURIFICATION_MODE = row.STATURDAY_PURIFIER_ID;
                                patientScheduleTemplateDataRow.PATIENT_ID = row.STATURDAYPATIENT;
                                patientScheduleTemplateDataRow.DIALYSIS_DATE = DateTime.Parse(this.gridSaturday.Tag.ToString());
                                patientScheduleTemplateDataRow.HEMODIALYSIS_ID = ConvertToString(row.STATURDAYHEMOID);
                                patientScheduleTemplateDataRow.REMARK = ConvertToString(row.STATURDAYREMARK);
                                _patientScheduleTemplateDataTable.AddMED_PATIENT_SCHEDULE_TEMP_DATARow(patientScheduleTemplateDataRow);
                                break;
                            }
                        case "6":
                            {
                                if (row.IsSUNDAYHEMOIDNull() || string.IsNullOrEmpty(row.SUNDAYHEMOID))
                                    continue;
                                var patientScheduleTemplateDataRow = _patientScheduleTemplateDataTable.NewMED_PATIENT_SCHEDULE_TEMP_DATARow();
                                patientScheduleTemplateDataRow.MED_SCHEDULE_TEMPLATE_DATA_ID = Guid.NewGuid().ToString();
                                patientScheduleTemplateDataRow.PATIENT_SCHEDULE_TEMPLATE_ID = PATIENT_SCHEDULE_TEMPLATE_ID;
                                patientScheduleTemplateDataRow.MONITOR_LABEL = ConvertToString(row.MACHINE_ID);
                                patientScheduleTemplateDataRow.BANCI_ID = row.TIMETABLEVALUE;
                                patientScheduleTemplateDataRow.DIALYSIS_ROOM_ID = ConvertToString(row.AREA_ID);
                                patientScheduleTemplateDataRow.BED_NUMBER = ConvertToString(row.BED_ID);
                                patientScheduleTemplateDataRow.STATUS = "1";
                                patientScheduleTemplateDataRow.PURIFICATION_MODE = row.SUNDAY_PURIFIER_ID;
                                patientScheduleTemplateDataRow.PATIENT_ID = row.SUNDAYPATIENT;
                                patientScheduleTemplateDataRow.DIALYSIS_DATE = DateTime.Parse(this.gridSunday.Tag.ToString());
                                patientScheduleTemplateDataRow.HEMODIALYSIS_ID = ConvertToString(row.SUNDAYHEMOID);
                                patientScheduleTemplateDataRow.REMARK = ConvertToString(row.SUNDAYREMARK);
                                _patientScheduleTemplateDataTable.AddMED_PATIENT_SCHEDULE_TEMP_DATARow(patientScheduleTemplateDataRow);
                                break;
                            }

                    }

                }
                //保存数据
                #endregion
            }

            if (_patientScheduleTemplateDataTable.Rows.Count <= 0)
            {
                AutoClosedMsgBox.ShowForm("保存失败！\r\n没有排班信息无法保存为模板？", "保存模板", 1500, MessageBoxIcon.Warning);
                return;
            }

            EditTemplate frm = new EditTemplate();
            frm.btn_edit.Visible = isRename;
            frm.currentTemplateName = currentOpenTemplateName;
            frm.currentTemplateID = currentOpenTemplateID;
            frm._patientScheduleTemplateRow = _patientScheduleTemplateRow;
            frm.txtPATIENT_SCHEDULE_TEMPLATE_NAME.Text = currentOpenTemplateName;
            frm.Tag = currentOpenTemplateID;
            frm.Blood_Hemo_Room = Blood_Hemo_Room;
            frm.IsHeadNurse = HemoApplicationContext.Current.CurrentUser.EMP_NO.Equals(Head_Nurse) ? true : false;
            if (DialogResult.Cancel == frm.ShowDialog())
                return;
            //模板的ID
            PATIENT_SCHEDULE_TEMPLATE_ID = frm.Tag.ToString();
            //保存要保存的数表
            _patientScheduleTemplateDataTable = new PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable();
            foreach (PermissionModel.MED_HEMO_SCHEDULEMASTERRow row in _ScheduleDataTableMain.Rows)
            {
                #region 进行显示与数据的行列转换,写入新表进行保存
                //0~7表示星期一到星期日
                for (int i = 0; i < 7; i++)
                {
                    switch (i.ToString())
                    {
                        case "0":
                            {
                                if (row.IsMONDAYHEMOIDNull() || string.IsNullOrEmpty(row.MONDAYHEMOID))
                                    continue;
                                var patientScheduleTemplateDataRow = _patientScheduleTemplateDataTable.NewMED_PATIENT_SCHEDULE_TEMP_DATARow();
                                patientScheduleTemplateDataRow.MED_SCHEDULE_TEMPLATE_DATA_ID = Guid.NewGuid().ToString();
                                patientScheduleTemplateDataRow.PATIENT_SCHEDULE_TEMPLATE_ID = PATIENT_SCHEDULE_TEMPLATE_ID;
                                patientScheduleTemplateDataRow.MONITOR_LABEL = ConvertToString(row.MACHINE_ID);
                                patientScheduleTemplateDataRow.BANCI_ID = row.TIMETABLEVALUE;
                                patientScheduleTemplateDataRow.DIALYSIS_ROOM_ID = ConvertToString(row.AREA_ID);
                                patientScheduleTemplateDataRow.BED_NUMBER = ConvertToString(row.BED_ID);
                                patientScheduleTemplateDataRow.STATUS = "1";
                                patientScheduleTemplateDataRow.PATIENT_ID = row.MONDAYPATIENT;
                                patientScheduleTemplateDataRow.PURIFICATION_MODE = row.MONDAY_PURIFIER_ID;
                                patientScheduleTemplateDataRow.DIALYSIS_DATE = DateTime.Parse(this.gridMonday.Tag.ToString());
                                patientScheduleTemplateDataRow.HEMODIALYSIS_ID = ConvertToString(row.MONDAYHEMOID);
                                patientScheduleTemplateDataRow.REMARK = ConvertToString(row.MONDAYREMARK);
                                patientScheduleTemplateDataRow.IS_CRRT = row.MONDAY_IS_CRRT;
                                _patientScheduleTemplateDataTable.AddMED_PATIENT_SCHEDULE_TEMP_DATARow(patientScheduleTemplateDataRow);
                                break;
                            }
                        case "1":
                            {
                                if (row.IsTUESDAYHEMOIDNull() || string.IsNullOrEmpty(row.TUESDAYHEMOID))
                                    continue;
                                var patientScheduleTemplateDataRow = _patientScheduleTemplateDataTable.NewMED_PATIENT_SCHEDULE_TEMP_DATARow();
                                patientScheduleTemplateDataRow.MED_SCHEDULE_TEMPLATE_DATA_ID = Guid.NewGuid().ToString();
                                patientScheduleTemplateDataRow.PATIENT_SCHEDULE_TEMPLATE_ID = PATIENT_SCHEDULE_TEMPLATE_ID;
                                patientScheduleTemplateDataRow.MONITOR_LABEL = ConvertToString(row.MACHINE_ID);
                                patientScheduleTemplateDataRow.BANCI_ID = row.TIMETABLEVALUE;
                                patientScheduleTemplateDataRow.DIALYSIS_ROOM_ID = ConvertToString(row.AREA_ID);
                                patientScheduleTemplateDataRow.BED_NUMBER = ConvertToString(row.BED_ID);
                                patientScheduleTemplateDataRow.STATUS = "1";
                                patientScheduleTemplateDataRow.PURIFICATION_MODE = row.TUESDAY_PURIFIER_ID;
                                patientScheduleTemplateDataRow.PATIENT_ID = row.TUESDAYPATIENT;
                                patientScheduleTemplateDataRow.DIALYSIS_DATE = DateTime.Parse(this.gridTuesday.Tag.ToString());
                                patientScheduleTemplateDataRow.HEMODIALYSIS_ID = ConvertToString(row.TUESDAYHEMOID);
                                patientScheduleTemplateDataRow.REMARK = ConvertToString(row.TUESDAYREMARK);
                                patientScheduleTemplateDataRow.IS_CRRT = row.TUESDAY_IS_CRRT;
                                _patientScheduleTemplateDataTable.AddMED_PATIENT_SCHEDULE_TEMP_DATARow(patientScheduleTemplateDataRow);
                                break;
                            }
                        case "2":
                            {
                                if (row.IsWEDNESDAYHEMOIDNull() || string.IsNullOrEmpty(row.WEDNESDAYHEMOID))
                                    continue;
                                var patientScheduleTemplateDataRow = _patientScheduleTemplateDataTable.NewMED_PATIENT_SCHEDULE_TEMP_DATARow();
                                patientScheduleTemplateDataRow.MED_SCHEDULE_TEMPLATE_DATA_ID = Guid.NewGuid().ToString();
                                patientScheduleTemplateDataRow.PATIENT_SCHEDULE_TEMPLATE_ID = PATIENT_SCHEDULE_TEMPLATE_ID;
                                patientScheduleTemplateDataRow.MONITOR_LABEL = ConvertToString(row.MACHINE_ID);
                                patientScheduleTemplateDataRow.BANCI_ID = row.TIMETABLEVALUE;
                                patientScheduleTemplateDataRow.DIALYSIS_ROOM_ID = ConvertToString(row.AREA_ID);
                                patientScheduleTemplateDataRow.BED_NUMBER = ConvertToString(row.BED_ID);
                                patientScheduleTemplateDataRow.STATUS = "1";
                                patientScheduleTemplateDataRow.PURIFICATION_MODE = row.WEDNESDAY_PURIFIER_ID;
                                patientScheduleTemplateDataRow.PATIENT_ID = row.WEDNESDAYPATIENT;
                                patientScheduleTemplateDataRow.DIALYSIS_DATE = DateTime.Parse(this.gridWednesday.Tag.ToString());
                                patientScheduleTemplateDataRow.HEMODIALYSIS_ID = ConvertToString(row.WEDNESDAYHEMOID);
                                patientScheduleTemplateDataRow.REMARK = ConvertToString(row.WEDNESDAYREMARK);
                                patientScheduleTemplateDataRow.IS_CRRT = row.WEDNESDAY_IS_CRRT;
                                _patientScheduleTemplateDataTable.AddMED_PATIENT_SCHEDULE_TEMP_DATARow(patientScheduleTemplateDataRow);
                                break;
                            }
                        case "3":
                            {
                                if (row.IsTHURSDAYHEMOIDNull() || string.IsNullOrEmpty(row.THURSDAYHEMOID))
                                    continue;
                                var patientScheduleTemplateDataRow = _patientScheduleTemplateDataTable.NewMED_PATIENT_SCHEDULE_TEMP_DATARow();
                                patientScheduleTemplateDataRow.MED_SCHEDULE_TEMPLATE_DATA_ID = Guid.NewGuid().ToString();
                                patientScheduleTemplateDataRow.PATIENT_SCHEDULE_TEMPLATE_ID = PATIENT_SCHEDULE_TEMPLATE_ID;
                                patientScheduleTemplateDataRow.MONITOR_LABEL = ConvertToString(row.MACHINE_ID);
                                patientScheduleTemplateDataRow.BANCI_ID = row.TIMETABLEVALUE;
                                patientScheduleTemplateDataRow.DIALYSIS_ROOM_ID = ConvertToString(row.AREA_ID);
                                patientScheduleTemplateDataRow.BED_NUMBER = ConvertToString(row.BED_ID);
                                patientScheduleTemplateDataRow.STATUS = "1";
                                patientScheduleTemplateDataRow.PURIFICATION_MODE = row.THURSDAY_PURIFIER_ID;
                                patientScheduleTemplateDataRow.PATIENT_ID = row.THURSDAYPATIENT;
                                patientScheduleTemplateDataRow.DIALYSIS_DATE = DateTime.Parse(this.gridThursday.Tag.ToString());
                                patientScheduleTemplateDataRow.HEMODIALYSIS_ID = ConvertToString(row.THURSDAYHEMOID);
                                patientScheduleTemplateDataRow.REMARK = ConvertToString(row.THURSDAYREMARK);
                                patientScheduleTemplateDataRow.IS_CRRT = row.THURSDAY_IS_CRRT;
                                _patientScheduleTemplateDataTable.AddMED_PATIENT_SCHEDULE_TEMP_DATARow(patientScheduleTemplateDataRow);
                                break;
                            }
                        case "4":
                            {
                                if (row.IsFRIDAYHEMOIDNull() || string.IsNullOrEmpty(row.FRIDAYHEMOID))
                                    continue;
                                var patientScheduleTemplateDataRow = _patientScheduleTemplateDataTable.NewMED_PATIENT_SCHEDULE_TEMP_DATARow();
                                patientScheduleTemplateDataRow.MED_SCHEDULE_TEMPLATE_DATA_ID = Guid.NewGuid().ToString();
                                patientScheduleTemplateDataRow.PATIENT_SCHEDULE_TEMPLATE_ID = PATIENT_SCHEDULE_TEMPLATE_ID;
                                patientScheduleTemplateDataRow.MONITOR_LABEL = ConvertToString(row.MACHINE_ID);
                                patientScheduleTemplateDataRow.BANCI_ID = row.TIMETABLEVALUE;
                                patientScheduleTemplateDataRow.DIALYSIS_ROOM_ID = ConvertToString(row.AREA_ID);
                                patientScheduleTemplateDataRow.BED_NUMBER = ConvertToString(row.BED_ID);
                                patientScheduleTemplateDataRow.STATUS = "1";
                                patientScheduleTemplateDataRow.PURIFICATION_MODE = row.FRIDAY_PURIFIER_ID;
                                patientScheduleTemplateDataRow.PATIENT_ID = row.FRIDAYPATIENT;
                                patientScheduleTemplateDataRow.DIALYSIS_DATE = DateTime.Parse(this.gridFriday.Tag.ToString());
                                patientScheduleTemplateDataRow.HEMODIALYSIS_ID = ConvertToString(row.FRIDAYHEMOID);
                                patientScheduleTemplateDataRow.REMARK = ConvertToString(row.FRIDAYREMARK);
                                patientScheduleTemplateDataRow.IS_CRRT = row.FRIDAY_IS_CRRT;
                                _patientScheduleTemplateDataTable.AddMED_PATIENT_SCHEDULE_TEMP_DATARow(patientScheduleTemplateDataRow);
                                break;
                            }
                        case "5":
                            {
                                if (row.IsSTATURDAYHEMOIDNull() || string.IsNullOrEmpty(row.STATURDAYHEMOID))
                                    continue;
                                var patientScheduleTemplateDataRow = _patientScheduleTemplateDataTable.NewMED_PATIENT_SCHEDULE_TEMP_DATARow();
                                patientScheduleTemplateDataRow.MED_SCHEDULE_TEMPLATE_DATA_ID = Guid.NewGuid().ToString();
                                patientScheduleTemplateDataRow.PATIENT_SCHEDULE_TEMPLATE_ID = PATIENT_SCHEDULE_TEMPLATE_ID;
                                patientScheduleTemplateDataRow.MONITOR_LABEL = ConvertToString(row.MACHINE_ID);
                                patientScheduleTemplateDataRow.BANCI_ID = row.TIMETABLEVALUE;
                                patientScheduleTemplateDataRow.DIALYSIS_ROOM_ID = ConvertToString(row.AREA_ID);
                                patientScheduleTemplateDataRow.BED_NUMBER = ConvertToString(row.BED_ID);
                                patientScheduleTemplateDataRow.STATUS = "1";
                                patientScheduleTemplateDataRow.PURIFICATION_MODE = row.STATURDAY_PURIFIER_ID;
                                patientScheduleTemplateDataRow.PATIENT_ID = row.STATURDAYPATIENT;
                                patientScheduleTemplateDataRow.DIALYSIS_DATE = DateTime.Parse(this.gridSaturday.Tag.ToString());
                                patientScheduleTemplateDataRow.HEMODIALYSIS_ID = ConvertToString(row.STATURDAYHEMOID);
                                patientScheduleTemplateDataRow.REMARK = ConvertToString(row.STATURDAYREMARK);
                                patientScheduleTemplateDataRow.IS_CRRT = row.STATURDAY_IS_CRRT;
                                _patientScheduleTemplateDataTable.AddMED_PATIENT_SCHEDULE_TEMP_DATARow(patientScheduleTemplateDataRow);
                                break;
                            }
                        case "6":
                            {
                                if (row.IsSUNDAYHEMOIDNull() || string.IsNullOrEmpty(row.SUNDAYHEMOID))
                                    continue;
                                var patientScheduleTemplateDataRow = _patientScheduleTemplateDataTable.NewMED_PATIENT_SCHEDULE_TEMP_DATARow();
                                patientScheduleTemplateDataRow.MED_SCHEDULE_TEMPLATE_DATA_ID = Guid.NewGuid().ToString();
                                patientScheduleTemplateDataRow.PATIENT_SCHEDULE_TEMPLATE_ID = PATIENT_SCHEDULE_TEMPLATE_ID;
                                patientScheduleTemplateDataRow.MONITOR_LABEL = ConvertToString(row.MACHINE_ID);
                                patientScheduleTemplateDataRow.BANCI_ID = row.TIMETABLEVALUE;
                                patientScheduleTemplateDataRow.DIALYSIS_ROOM_ID = ConvertToString(row.AREA_ID);
                                patientScheduleTemplateDataRow.BED_NUMBER = ConvertToString(row.BED_ID);
                                patientScheduleTemplateDataRow.STATUS = "1";
                                patientScheduleTemplateDataRow.PURIFICATION_MODE = row.SUNDAY_PURIFIER_ID;
                                patientScheduleTemplateDataRow.PATIENT_ID = row.SUNDAYPATIENT;
                                patientScheduleTemplateDataRow.DIALYSIS_DATE = DateTime.Parse(this.gridSunday.Tag.ToString());
                                patientScheduleTemplateDataRow.HEMODIALYSIS_ID = ConvertToString(row.SUNDAYHEMOID);
                                patientScheduleTemplateDataRow.REMARK = ConvertToString(row.SUNDAYREMARK);
                                patientScheduleTemplateDataRow.IS_CRRT = row.SUNDAY_IS_CRRT;
                                _patientScheduleTemplateDataTable.AddMED_PATIENT_SCHEDULE_TEMP_DATARow(patientScheduleTemplateDataRow);
                                break;
                            }

                    }

                }
                //保存数据
                #endregion
            }

            if (XtraMessageBox.Show("确定保存为模版信息吗？", "保存模板", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            if (this._patientScheduleService.SavePatientScheduleTemplateDataInfoNew(_patientScheduleTemplateDataTable) > 0)
                AutoClosedMsgBox.ShowForm("保存模版信息成功！", "提示", 1000, MessageBoxIcon.Information);
            else
                AutoClosedMsgBox.ShowForm("保存模版信息失败！", "错误", 1000, MessageBoxIcon.Error);
            _isDirty = false;
        }

        private void SetRowData(PermissionModel.MED_HEMO_SCHEDULEMASTERRow row, GridColumn gridCol)
        {

            if (row[gridCol.FieldName + "HEMOID"] == null || string.IsNullOrEmpty(row[gridCol.FieldName + "HEMOID"].ToString()))
            {
                return;
            }
            var patientScheduleDataRow = patientScheduleDataTable.NewMED_PATIENT_SCHEDULERow();
            patientScheduleDataRow["PATIENT_SCHEDULE_ID"] = row[gridCol.FieldName + "_SCHEDULE_ID"];
            patientScheduleDataRow.USER_ID = LoginUser.User.USER_ID;
            patientScheduleDataRow.MONITOR_LABEL = ConvertToString(row.MACHINE_ID);
            patientScheduleDataRow.BANCI_ID = row.TIMETABLEVALUE;
            patientScheduleDataRow.DIALYSIS_ROOM_ID = ConvertToString(row.AREA_ID);
            patientScheduleDataRow.BED_NUMBER = ConvertToString(row.BED_ID);
            patientScheduleDataRow.STATUS = "1";
            patientScheduleDataRow.FOCUS_LEVEL = "0";
            patientScheduleDataRow["PURIFICATION_MODE"] = row[gridCol.FieldName + "_PURIFIER_ID"];// row.MONDAY_PURIFIER_ID;
            patientScheduleDataRow["PATIENT_ID"] = row[gridCol.FieldName + "PATIENT"];// row.MONDAYPATIENT;
            patientScheduleDataRow.DIALYSIS_DATE = DateTime.Parse(gridCol.Tag.ToString());
            patientScheduleDataRow["HEMODIALYSIS_ID"] = row[gridCol.FieldName + "HEMOID"];// ConvertToString(row.MONDAYHEMOID);
            patientScheduleDataRow["REMARK"] = row[gridCol.FieldName + "REMARK"];//ConvertToString(row.MONDAYREMARK);
            patientScheduleDataRow["IS_CRRT"] = row.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
            patientScheduleDataTable.AddMED_PATIENT_SCHEDULERow(patientScheduleDataRow);
        }
        /// <summary>
        /// 保存排班数据
        /// </summary>
        public void SaveToScheduleData()
        {
            patientScheduleDataTable = new PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable();
            foreach (PermissionModel.MED_HEMO_SCHEDULEMASTERRow row in _ScheduleDataTableMain.Rows)
            {
                #region 进行显示与数据的行列转换,写入新表进行保存[正式库同样 出现了程序过大的问题，但本机未出现，同样采取分数据组方式进行保存测试。]

                SetRowData(row, gridMonday);
                SetRowData(row, gridSunday);
                SetRowData(row, gridSaturday);
                SetRowData(row, gridThursday);
                SetRowData(row, gridTuesday);
                SetRowData(row, gridWednesday);
                SetRowData(row, gridFriday);
                #endregion
            }
            if (patientScheduleDataTable.Rows.Count <= 0)
            { AutoClosedMsgBox.ShowForm("无排班信息！", "警告", 1000, MessageBoxIcon.Warning); return; }

            if (XtraMessageBox.Show("确定保存排班信息吗？", "保存排班", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            int returnSave = 0;
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                BusyIndicatorHelp busyIndicatorHelp = new BusyIndicatorHelp();

                busyIndicatorHelp.ShowMessage();
                busyIndicatorHelp.SetWaitFormCaption("数据保存中....");

                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    bool isHeadNurse = HemoApplicationContext.Current.CurrentUser.EMP_NO.Equals(Head_Nurse) ? true : false;
                    returnSave = this._patientScheduleService.SavePatientScheduleInfoNew(patientScheduleDataTable, _beginDate, _endDate, LoginUser.User.USER_ID, dateIsFromTemplate, Blood_Hemo_Room, isHeadNurse);
                    if (returnSave > 0)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            //生成处方
                            _hemodialysisService.CreatePatientRecipeBydate(_beginDate.AddDays(i));
                            //更新透析方式
                            _hemodialysisService.UpdatePatientRecipePurificationModeBydate(_beginDate.AddDays(i));
                        }
                    }
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    busyIndicatorHelp.HideMessage();
                    if (returnSave > 0)
                    {
                        InizationData(false);
                        AutoClosedMsgBox.ShowForm("保存成功！", "提示", 1000, MessageBoxIcon.Information);
                    }
                    else
                    {
                        AutoClosedMsgBox.ShowForm("保存排班信息失败！", "错误", 1000, MessageBoxIcon.Error);
                    }
                };
                worker.RunWorkerAsync();
            }

            //if (this._patientScheduleService.SavePatientScheduleInfoNew(patientScheduleDataTable, _beginDate, _endDate, LoginUser.User.USER_ID, dateIsFromTemplate) > 0)
            //{
            //    AutoClosedMsgBox.ShowForm("保存成功！", "提示", 1000, MessageBoxIcon.Information);
            //    for (int i = 0; i < 7; i++)
            //    {
            //        //生成处方
            //        _hemodialysisService.CreatePatientRecipeBydate(_beginDate.AddDays(i));
            //        //更新透析方式
            //        _hemodialysisService.UpdatePatientRecipePurificationModeBydate(_beginDate.AddDays(i));
            //    }
            //    InizationData(false);
            //}
            //else
            //    AutoClosedMsgBox.ShowForm("保存排班信息失败！", "错误", 1000, MessageBoxIcon.Error);
            dateIsFromTemplate = false;
            _isDirty = false;
        }

        /// <summary>
        /// 导入排班模板数据至当前界面进行排班
        /// </summary>
        public void ImportTemplateScheduleData(string templateID)
        {
            _isDirty = true;

            this.busyIndicator1.ShowLoadingScreenFor(this.gridControlForEmerger);
            this.gridView1.Columns["TIMETABLEVALUE"].Group();
            var patientScheduleTemplateDt = new PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable();
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    //getData
                    this._areaDict = this._userService.GetAreaList(LoginUser.User.USER_ID).OrderByDescending(r => r.ORDER_NUMBER).ToDictionary(r => r, r => 200);
                    this._bedDataTable = this._configService.GetConfigList(string.Empty, string.Empty, "床位", "1");
                    this._banChiDateTable = this._configService.GetConfigList(string.Empty, string.Empty, "班次", "1");
                    this._machineDataTable = this._machineService.GetMachineListByUserID(LoginUser.User.USER_ID);
                    //this._patientDataTable = this._patientService.GetPatientList();
                    //this._recipeDataTable = this._hemodialysisService.GetAllRecipe();
                    //根据模板号加载模板的数据
                    patientScheduleTemplateDt = this._patientScheduleService.GetPatientScheduleTempDataListNew(templateID);
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    #region 获取数据后对数据进行相应的转换处理后进行赋值
                    var _ScheduleDataTable = new PermissionModel.MED_HEMO_SCHEDULEMASTERDataTable();
                    foreach (ConfigModel.MED_COMMON_ITEMLISTRow row in this._banChiDateTable.Rows)
                    {
                        foreach (PermissionModel.MED_DIALYSIS_MACHINEEXTENDRow _machineRow in _machineDataTable.Rows)
                        {
                            //if (!HemoApplicationContext.Current.CurrentUser.EMP_NO.Equals(Head_Nurse))
                            //{
                            //    if (Blood_Hemo_Room.Equals("5"))
                            //    {
                            //        if (!_machineRow.QYNAME.Equals("透析室E区") && !_machineRow.QYNAME.Equals("透析室F区") && !_machineRow.QYNAME.Equals("透析室G区"))
                            //        {
                            //            continue;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        if (_machineRow.QYNAME.Equals("透析室E区") || _machineRow.QYNAME.Equals("透析室F区") || _machineRow.QYNAME.Equals("透析室G区"))
                            //        {
                            //            continue;
                            //        }
                            //    }
                            //}
                            var scheduleRow = _ScheduleDataTable.NewMED_HEMO_SCHEDULEMASTERRow();
                            scheduleRow.TIMETABLEID = row.ITEM_ID;
                            scheduleRow.TIMETABLEVALUE = row.ITEM_VALUE;
                            scheduleRow.TIMETABLENAME = row.ITEM_NAME;
                            scheduleRow.MACHINE_ID = _machineRow.MACHINE_ID;
                            scheduleRow.MACHINE_NAME = _machineRow.MACHINE_NAME;
                            scheduleRow.MACHINE_MODEL = _machineRow.MACHINE_MODEL;
                            scheduleRow.TYPE = _machineRow.TYPE;
                            scheduleRow.THERAPEUTIC_PROPERTIES = _machineRow.THERAPEUTIC_PROPERTIES;
                            scheduleRow.OTHER_THERAPEUTIC = ConvertToString(_machineRow.OTHER_THERAPEUTIC);
                            scheduleRow.SUPPLIER = ConvertToString(_machineRow.SUPPLIER);
                            scheduleRow.AREA_ID = _machineRow.AREA_ID;
                            scheduleRow.BED_ID = _machineRow.BED_ID;
                            scheduleRow.FLNAME = _machineRow.FLNAME;
                            scheduleRow.QYNAME = _machineRow.QYNAME;
                            scheduleRow.QYVALUE = _machineRow.QYVALUE;
                            scheduleRow.CWNAME = _machineRow.CWNAME;
                            scheduleRow.CWVALUE = _machineRow.CWVALUE;
                            _ScheduleDataTable.AddMED_HEMO_SCHEDULEMASTERRow(scheduleRow);
                        }
                    }
                    _ScheduleDataTableMain = new PermissionModel.MED_HEMO_SCHEDULEMASTERDataTable();
                    _ScheduleDataTable.OrderByDescending(i => i.TIMETABLEVALUE).ThenBy(i => i.QYVALUE).ThenBy(i => int.Parse(i.CWVALUE)).CopyToDataTable<PermissionModel.MED_HEMO_SCHEDULEMASTERRow>(_ScheduleDataTableMain, LoadOption.OverwriteChanges);

                    #endregion

                    #region 对于已排班的人员加载到排班显示中
                    foreach (PermissionModel.MED_HEMO_SCHEDULEMASTERRow row in _ScheduleDataTableMain.Rows)
                    {
                        foreach (PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATARow scheduleRow in patientScheduleTemplateDt.Rows)
                        {
                            string _purificationMondelID = string.Empty;
                            if (row.AREA_ID == scheduleRow.DIALYSIS_ROOM_ID && row.BED_ID == scheduleRow.BED_NUMBER && row.TIMETABLEVALUE == scheduleRow.BANCI_ID)
                            {
                                if (!scheduleRow.IsPURIFICATION_MODENull() && !string.IsNullOrEmpty(scheduleRow.PURIFICATION_MODE))
                                    _purificationMondelID = scheduleRow.PURIFICATION_MODE;
                                string weekstr = scheduleRow.DIALYSIS_DATE.DayOfWeek.ToString().ToUpper();
                                if (weekstr == "SATURDAY")
                                {
                                    weekstr = "STATURDAY";
                                }
                                if (!string.IsNullOrEmpty(_purificationMondelID))
                                {
                                    if (this._purificationMondel.FindByITEM_ID(_purificationMondelID) != null)
                                    {
                                        row[weekstr] = string.Format("{0} {1} {2}", scheduleRow.PATIENTNAME.ToString(), this._purificationMondel.FindByITEM_ID(_purificationMondelID)["ITEM_NAME"].ToString(), scheduleRow.REMARK);
                                    }
                                }
                                else
                                {
                                    row[weekstr] = string.Format("{0} {1}", scheduleRow.PATIENTNAME.ToString(), scheduleRow.REMARK);
                                }
                                row[weekstr + "PATIENT"] = scheduleRow.PATIENT_ID.ToString();
                                row[weekstr + "REMARK"] = scheduleRow.REMARK.ToString();
                                row[weekstr + "HEMOID"] = scheduleRow.HEMODIALYSIS_ID;
                                row[weekstr + "_SCHEDULE_ID"] = Guid.NewGuid().ToString();
                                row[string.Format("{0}_PURIFIER_ID", weekstr)] = scheduleRow.IsPURIFICATION_MODENull() ? string.Empty : scheduleRow.PURIFICATION_MODE;
                                row[string.Format("{0}_IS_CRRT", weekstr)] = scheduleRow.IsIS_CRRTNull() ? string.Empty : scheduleRow.IS_CRRT;
                                var rowPur = _purificationMondel.FindByITEM_ID(row[string.Format("{0}_PURIFIER_ID", weekstr)].ToString());
                                string purString = "";
                                if (rowPur != null)
                                {
                                    purString = string.Format("{0}", rowPur["ITEM_NAME"]);
                                }
                                row[weekstr] = string.Format("{0} {1} {2}", scheduleRow.PATIENTNAME.ToString(), purString, scheduleRow.REMARK);

                            }
                        }
                    }
                    #endregion
                    this.gridControlForEmerger.DataSource = _ScheduleDataTableMain;
                    //this.gridView1.ExpandGroupRow(-1);
                    for (int i = -_areaDict.Count; i < 0; i++)
                    {
                        this.gridView1.ExpandGroupRow(i);
                    }
                    this.busyIndicator1.HideLoadingScreen();
                };
                worker.RunWorkerAsync();
            }

        }
        /// <summary>
        /// 验证是否已存在该病人!
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        //private bool ValidInputPatientSchedule(string hemoId, string columnName)
        //{
        //    PermissionModel.MED_HEMO_SCHEDULEMASTERRow rows = null;
        //    var pName = "";
        //    switch (columnName)
        //    {
        //        case "gridMonday":
        //            {
        //                rows = _ScheduleDataTableMain.FirstOrDefault(i => i.MONDAYHEMOID == hemoId);
        //                if (rows!=null)
        //                {
        //                    pName = rows.MONDAY;
        //                }
        //                break;
        //            }
        //        case "gridTuesday":
        //            {
        //                rows = _ScheduleDataTableMain.FirstOrDefault(i => i.TUESDAYHEMOID == hemoId);
        //                if (rows != null)
        //                {
        //                    pName = rows.TUESDAY;
        //                }
        //                break;
        //            }
        //        case "gridWednesday":
        //            {
        //                rows = _ScheduleDataTableMain.FirstOrDefault(i => i.WEDNESDAYHEMOID == hemoId);
        //                if (rows != null)
        //                {
        //                    pName = rows.WEDNESDAY;
        //                }
        //                break;
        //            }
        //        case "gridThursday":
        //            {
        //                rows = _ScheduleDataTableMain.FirstOrDefault(i => i.THURSDAYHEMOID == hemoId);
        //                if (rows != null)
        //                {
        //                    pName = rows.THURSDAY;
        //                }
        //                break;
        //            }
        //        case "gridFriday":
        //            {
        //                rows = _ScheduleDataTableMain.FirstOrDefault(i => i.FRIDAYHEMOID == hemoId);
        //                if (rows != null)
        //                {
        //                    pName = rows.FRIDAY;
        //                }
        //                break;
        //            }
        //        case "gridSaturday":
        //            {
        //                rows = _ScheduleDataTableMain.FirstOrDefault(i => i.STATURDAYHEMOID == hemoId);
        //                if (rows != null)
        //                {
        //                    pName = rows.STATURDAY;
        //                }
        //                break;
        //            }
        //        case "gridSunday":
        //            {
        //                rows = _ScheduleDataTableMain.FirstOrDefault(i => i.SUNDAYHEMOID == hemoId);
        //                if (rows != null)
        //                {
        //                    pName = rows.SUNDAY;
        //                }
        //                break;
        //            }
        //    }
        //    if (rows!=null)
        //    {
        //        var msg = string.Format("患者【{0}】已在【{1}】的【{2}班】进行了排班,是否继续？", pName, rows.QYNAME, rows.TIMETABLENAME);
        //        if (DialogResult.No == MessageBox.Show(msg, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))

        //            return true;
        //    }
        //    return false;

        //}

        /// <summary>
        /// 应用模板数据
        /// </summary>
        /// <param name="data"></param>
        public void UseTemplateDataFromSky(PermissionModel.MED_HEMO_SCHEDULEMASTERDataTable data)
        {
            this._ScheduleDataTableMain = data;
        }

        /// <summary>
        /// 显示患者信息
        /// </summary>
        public void ShowPatientInfo()
        {
            _patientDataTable = GetCurrentPatient();
            if (_patientDataTable != null && _patientDataTable.Rows.Count > 0)
            {
                using (EditPatientNew editPatient = new EditPatientNew())
                {
                    editPatient.Current = _patientDataTable[0];
                    editPatient.ShowDialog();
                }
            }
        }

        public PatientModel.MED_PATIENTSDataTable GetCurrentPatient()
        {
            var rowCurrent = this.gridView1.GetFocusedDataRow() as PermissionModel.MED_HEMO_SCHEDULEMASTERRow;

            if (rowCurrent == null || gridView1.FocusedColumn == null || gridView1.FocusedColumn.Name.ToString() == this.gridBanchi.Name.ToString() ||
                gridView1.FocusedColumn.Name.ToString() == this.gridColumn1.Name.ToString() ||
                gridView1.FocusedColumn.Name.ToString() == this.gridOffice.Name.ToString() ||
                gridView1.FocusedColumn.Name.ToString() == this.gridBedNo.Name.ToString())
                return null;

            var fileName = this.gridView1.FocusedColumn.FieldName;
            var hemoId = rowCurrent[string.Format("{0}HEMOID", fileName)];
            if (hemoId != null && hemoId.ToString() != string.Empty)
            {
                _patientDataTable = _patientService.GetPatientListByParams(string.Empty, hemoId.ToString());
            }
            return _patientDataTable;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        public void ExportExcel()
        {
            if (this.gridView1.RowCount > 0)
            {
                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.Title = "导出Excel";
                fileDialog.Filter = "Excel文件(*.xls)|*.xls";
                fileDialog.FileName = this._beginDate.ToString("yyyyMMdd") + "-" + this._endDate.ToString("yyyyMMdd") + "血透患者排班记录";
                fileDialog.RestoreDirectory = true;
                DialogResult dialogResult = fileDialog.ShowDialog(this);
                if (dialogResult == DialogResult.OK)
                {
                    DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                    options.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
                    this.gridView1.ExportToXls(fileDialog.FileName, options);
                    DevExpress.XtraEditors.XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #endregion

        #region 事件
        private void CtlScheduleMainGrid_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 对于分组的内容表头进行重新绘制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            var row = gridView1.GetDataRow(e.RowHandle) as PermissionModel.MED_HEMO_SCHEDULEMASTERRow;
            GridGroupRowInfo GridGroupRowInfo = e.Info as GridGroupRowInfo;
            if (GridGroupRowInfo.Column.GroupIndex == 0)
            {
                GridGroupRowInfo.GroupText = "班次:" + row.TIMETABLENAME;// +"-" + GridGroupRowInfo.EditValue.ToString();
            }
            else if (GridGroupRowInfo.Column.GroupIndex == 1)
            {
                GridGroupRowInfo.GroupText = row.QYNAME;// +"-" + GridGroupRowInfo.EditValue.ToString();
                if (row.QYNAME.Equals("CRRT"))
                {
                    if (row.TIMETABLENAME.Equals("上午"))
                    {
                        GridGroupRowInfo.GroupText = row.QYNAME + "白天";
                    }
                    else if (row.TIMETABLENAME.Equals("下午"))
                    {
                        GridGroupRowInfo.GroupText = row.QYNAME + "小夜";
                    }
                    else if (row.TIMETABLENAME.Equals("晚班"))
                    {
                        GridGroupRowInfo.GroupText = row.QYNAME + "大夜";
                    }
                }
            }
        }

        /// <summary>
        /// 双击的时候录入排班信息右击出现菜单进行删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this.gridView1.IsGroupRow(e.RowHandle))
            {
                if (this.gridView1.GetRowLevel(e.RowHandle) == 0)
                {
                    for (int i = -_areaDict.Count + e.RowHandle; i < e.RowHandle; i++)
                    {
                        this.gridView1.ExpandGroupRow(i);
                    }
                }
            }

            var rowCurrent = this.gridView1.GetFocusedDataRow() as PermissionModel.MED_HEMO_SCHEDULEMASTERRow;

            if (rowCurrent == null || gridView1.FocusedColumn == null || gridView1.FocusedColumn.Name.ToString() == this.gridBanchi.Name.ToString() ||
                gridView1.FocusedColumn.Name.ToString() == this.gridColumn1.Name.ToString() ||
                gridView1.FocusedColumn.Name.ToString() == this.gridOffice.Name.ToString() ||
                gridView1.FocusedColumn.Name.ToString() == this.gridBedNo.Name.ToString())
                return;

            // this.gridView1.FocusedColumn.AppearanceCell.Font = new Font("Tahoma", 9, FontStyle.Bold);

            var fileName = this.gridView1.FocusedColumn.FieldName;

            //打开患者录入界面
            if (e.Button == MouseButtons.Left && !this.gridView1.IsGroupRow(e.RowHandle))
            {
                #region

                this.contextMenuStrip1.Visible = false;

                if (e.Clicks == 2)
                {
                    using (PatientScheduleInputCtl frm = new PatientScheduleInputCtl())
                    {
                        var rooms = _configService.GetConfigList("", "", "隔离病区", "1");
                        if (rooms.Count(wh => wh.ITEM_VALUE == rowCurrent.QYNAME.ToString()) > 0)
                            frm.IsQuarantineArea = true;
                        else
                            frm.IsQuarantineArea = false;
                        frm.StartPosition = FormStartPosition.CenterParent;
                        frm.patientValue = string.Format("{0}", rowCurrent[this.gridView1.FocusedColumn.FieldName]);
                        frm.patientRemark = rowCurrent[string.Format("{0}REMARK", fileName)].ToString();
                        frm.scheduleMode = rowCurrent[string.Format("{0}_PURIFIER_ID", fileName)].ToString();

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            string strResult = CheckCRRTCureStatus(rowCurrent);
                            if (!string.IsNullOrEmpty(strResult))
                            {
                                if (XtraMessageBox.Show(strResult, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Question) == DialogResult.OK)
                                    return;
                            }

                            strResult = string.Format(CheckArea(rowCurrent.QYNAME.ToString(), frm.infectious_check_result, rowCurrent.CWNAME).Trim(), frm.patientName);
                            if (!string.IsNullOrEmpty(strResult))
                            {
                                if (XtraMessageBox.Show(strResult, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Question) == DialogResult.OK)
                                    return;
                            }
                            var rowExisit = _ScheduleDataTableMain.FirstOrDefault(i => i[string.Format("{0}HEMOID", fileName)].ToString() == frm.hemoID && i != rowCurrent);
                            if (rowExisit != null)
                            {
                                //九龙医院有时会出现个别上午下午都会透析的患者，要求改动！
                                //var msg = string.Format("患者【{0}】已在【{1}】的【{2}班】进行了排班,是否继续?", rowExisit[fileName], rowExisit.QYNAME, rowExisit.TIMETABLENAME);
                                //if (XtraMessageBox.Show(msg, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                                //    return;


                                #region 九龙医院有时会出现个别上午下午都会透析的患者，要求改动！

                                var msg = string.Format("患者【{0}】已在【{1}】的【{2}班】进行了排班,不允许重复排班!", rowExisit[fileName], rowExisit.QYNAME, rowExisit.TIMETABLENAME);
                                AutoClosedMsgBox.ShowForm(msg, "提示", 3000, MessageBoxIcon.Question);
                                return;

                                #endregion

                            }
                            if (rowCurrent[string.Format("{0}_SCHEDULE_ID", fileName)] == null || string.IsNullOrEmpty(rowCurrent[string.Format("{0}_SCHEDULE_ID", fileName)].ToString()))
                            {
                                rowCurrent[string.Format("{0}_SCHEDULE_ID", fileName)] = Guid.NewGuid().ToString();

                            }
                            if (frm.patientValue.Trim() == string.Format("{0}", rowCurrent[this.gridView1.FocusedColumn.FieldName].ToString().Trim()) && frm.patientRemark.Trim() == rowCurrent[string.Format("{0}REMARK", fileName)].ToString().Trim() && frm.scheduleMode.Trim() == rowCurrent[string.Format("{0}_PURIFIER_ID", fileName)].ToString().Trim())
                                _isDirty = false;
                            else
                                _isDirty = true;

                            rowCurrent[string.Format("{0}", fileName)] = frm.patientValue;
                            rowCurrent[string.Format("{0}PATIENT", fileName)] = frm.patientID;
                            rowCurrent[string.Format("{0}REMARK", fileName)] = frm.patientRemark;
                            rowCurrent[string.Format("{0}HEMOID", fileName)] = frm.hemoID;
                            rowCurrent[string.Format("{0}_PURIFIER_ID", fileName)] = frm.scheduleMode;
                            rowCurrent[string.Format("{0}_INFECTIOUS_CHECK_RESULT", fileName)] = frm.infectious_check_result;
                            rowCurrent[string.Format("{0}_IS_CRRT", fileName)] = rowCurrent.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                            return;
                        }
                    }
                }
                else if (e.Clicks == 1)
                {

                }

                #endregion
            }
            //右击菜单
            else if (e.Button == MouseButtons.Right && e.Clicks == 1)
            {
                this.contextMenuStrip1.Visible = true;
                this.contextMenuStrip1.Left = MousePosition.X;
                this.contextMenuStrip1.Top = MousePosition.Y;
            }
        }

        /// <summary>
        /// 判断是否为传染病病区
        /// </summary>
        /// <param name="strRoom"></param>
        /// <param name="strInf"></param>
        /// <param name="strBed"></param>
        /// <returns></returns>
        private string CheckArea(string strRoom, string strInf, string strBed)
        {
            var rooms = _configService.GetConfigList("", "", "隔离病区", "1");
            strInf = strInf.Replace(" ", "");
            //第九透析室急诊床位排全阴、待查患者，其它床位排普通患者；CRRT室无传染病限制
            if (rooms.Count(wh => wh.ITEM_VALUE == strRoom) > 0)
            {
                var AntiAreaName = rooms.FirstOrDefault(i => i.ITEM_VALUE == strRoom).ITEM_NAME;
                if (!AntiAreaName.Contains(strInf) && !string.IsNullOrEmpty(strInf))
                {
                    return "{0}" + string.Format("为{0}患者，不能在{1}区域进行治疗！", strInf, AntiAreaName);
                }
                else if (string.IsNullOrEmpty(strInf) && !strRoom.Equals("第九透析室"))
                {
                    return "{0}" + string.Format("为普通患者，不能在{0}区域进行治疗！", AntiAreaName);
                }
                else if (string.IsNullOrEmpty(strInf) && strRoom.Equals("第九透析室"))
                {
                    if (strBed.Equals("急诊机"))
                    {
                        return "{0}" + "为普通患者，九室只能排在普通床位进行治疗！";
                    }
                }
                else if (AntiAreaName.Contains(strInf) && !string.IsNullOrEmpty(strInf) && strRoom.Equals("第九透析室"))
                {
                    if (!strBed.Equals("急诊机"))
                    {
                        return "{0}" + string.Format("为{0}患者，九室只能排在急诊床位进行治疗！", strInf);
                    }
                }
            }
            else if (!strInf.Equals("全阴") && !string.IsNullOrEmpty(strInf) && !strRoom.Substring(0, 4).Equals("CRRT"))
            {
                return "{0}" + string.Format("为{0}患者，不能在普通区域进行治疗！", strInf);
            }
            return "";
        }

        /// <summary>
        /// 检查CRRT治疗状态
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string CheckCRRTCureStatus(PermissionModel.MED_HEMO_SCHEDULEMASTERRow row)
        {
            string result = string.Empty;
            bool flag = true;
            if (row.QYNAME.Substring(0, 4).Equals("CRRT"))
            {
                var dtSchedule = this.gridControlForEmerger.DataSource as PermissionModel.MED_HEMO_SCHEDULEMASTERDataTable;
                var schedule = dtSchedule.Where(r => r.AREA_ID.Equals(row.AREA_ID) && r.BED_ID.Equals(row.BED_ID));
                foreach (PermissionModel.MED_HEMO_SCHEDULEMASTERRow r in schedule)
                {
                    if (!string.IsNullOrEmpty(r.SUNDAY))
                    {
                        if (r.SUNDAY_IS_CRRT.Equals("1") && string.IsNullOrEmpty(r.SUNDAY_END_TIME))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (!string.IsNullOrEmpty(r.STATURDAY))
                    {
                        if (r.STATURDAY_IS_CRRT.Equals("1") && string.IsNullOrEmpty(r.STATURDAY_END_TIME))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (!string.IsNullOrEmpty(r.FRIDAY))
                    {
                        if (r.FRIDAY_IS_CRRT.Equals("1") && string.IsNullOrEmpty(r.FRIDAY_END_TIME))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (!string.IsNullOrEmpty(r.THURSDAY))
                    {
                        if (r.THURSDAY_IS_CRRT.Equals("1") && string.IsNullOrEmpty(r.THURSDAY_END_TIME))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (!string.IsNullOrEmpty(r.WEDNESDAY))
                    {
                        if (r.WEDNESDAY_IS_CRRT.Equals("1") && string.IsNullOrEmpty(r.WEDNESDAY_END_TIME))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (!string.IsNullOrEmpty(r.TUESDAY))
                    {
                        if (r.TUESDAY_IS_CRRT.Equals("1") && string.IsNullOrEmpty(r.TUESDAY_END_TIME))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (!string.IsNullOrEmpty(r.MONDAY))
                    {
                        if (r.MONDAY_IS_CRRT.Equals("1") && string.IsNullOrEmpty(r.MONDAY_END_TIME))
                        {
                            flag = false;
                            break;
                        }
                    }
                }

                if (!flag)
                {
                    result = row.CWNAME + "床已安排患者，结束治疗前不能安排新的CRRT患者！";
                }
            }
            return result;
        }

        //删除
        private void ToolStripMenuItem_Delete_Click(object sender, EventArgs e)
        {
            var row = this.gridView1.GetFocusedDataRow() as PermissionModel.MED_HEMO_SCHEDULEMASTERRow;
            if (row == null)
                return;
            if (DialogResult.Cancel == MessageBox.Show("是否删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                return;
            #region 删除


            switch (this.gridView1.FocusedColumn.Name)
            {
                case "gridMonday":
                    {
                        if (!row.IsMONDAY_START_TIMENull() && !string.IsNullOrEmpty(row.MONDAY_START_TIME))
                        {
                            if (DialogResult.OK == MessageBox.Show("患者开始治疗无法删除!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning))
                                break;
                        }
                        if (!row.IsMONDAY_RECIPE_IDNull() && !string.IsNullOrEmpty(row.MONDAY_RECIPE_ID))
                        {
                            if (DialogResult.OK == MessageBox.Show("患者已确认处方无法删除,请先取消处方确认再删除患者。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning))
                                break;
                        }
                        this._patientScheduleService.DeletePatientScheduleDateByID(row.MONDAY_SCHEDULE_ID.Trim());
                        row.MONDAY = string.Empty;
                        row.MONDAYPATIENT = string.Empty;
                        row.MONDAYREMARK = string.Empty;
                        row.MONDAYHEMOID = string.Empty;
                        row.MONDAY_SCHEDULE_ID = string.Empty;
                        ShowForm("删除成功！");

                        break;
                    }
                case "gridTuesday":
                    {
                        if (!row.IsTUESDAY_START_TIMENull() && !string.IsNullOrEmpty(row.TUESDAY_START_TIME))
                        {
                            if (DialogResult.OK == MessageBox.Show("患者开始治疗无法删除!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning))
                                break;
                        }
                        if (!row.IsTUESDAY_RECIPE_IDNull() && !string.IsNullOrEmpty(row.TUESDAY_RECIPE_ID))
                        {
                            if (DialogResult.OK == MessageBox.Show("患者已确认处方无法删除,请先取消处方确认再删除患者。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning))
                                break;
                        }
                        this._patientScheduleService.DeletePatientScheduleDateByID(row.TUESDAY_SCHEDULE_ID.Trim());

                        row.TUESDAY = string.Empty;
                        row.TUESDAYPATIENT = string.Empty;
                        row.TUESDAYREMARK = string.Empty;
                        row.TUESDAYHEMOID = string.Empty;
                        row.TUESDAY_SCHEDULE_ID = string.Empty;
                        ShowForm("删除成功！");

                        break;
                    }
                case "gridWednesday":
                    {
                        if (!row.IsWEDNESDAY_START_TIMENull() && !string.IsNullOrEmpty(row.WEDNESDAY_START_TIME))
                        {
                            if (DialogResult.OK == MessageBox.Show("患者开始治疗无法删除!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning))
                                break;
                        }
                        if (!row.IsWEDNESDAY_RECIPE_IDNull() && !string.IsNullOrEmpty(row.WEDNESDAY_RECIPE_ID))
                        {
                            if (DialogResult.OK == MessageBox.Show("患者已确认处方无法删除,请先取消处方确认再删除患者。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning))
                                break;
                        }
                        this._patientScheduleService.DeletePatientScheduleDateByID(row.WEDNESDAY_SCHEDULE_ID.Trim());

                        row.WEDNESDAY = string.Empty;
                        row.WEDNESDAYPATIENT = string.Empty;
                        row.WEDNESDAYREMARK = string.Empty;
                        row.WEDNESDAYHEMOID = string.Empty;
                        row.WEDNESDAY_SCHEDULE_ID = string.Empty;
                        ShowForm("删除成功！");

                        break;
                    }
                case "gridThursday":
                    {
                        if (!row.IsTHURSDAY_START_TIMENull() && !string.IsNullOrEmpty(row.THURSDAY_START_TIME))
                        {
                            if (DialogResult.OK == MessageBox.Show("患者开始治疗无法删除!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning))
                                break;
                        }
                        if (!row.IsTHURSDAY_RECIPE_IDNull() && !string.IsNullOrEmpty(row.THURSDAY_RECIPE_ID))
                        {
                            if (DialogResult.OK == MessageBox.Show("患者已确认处方无法删除,请先取消处方确认再删除患者。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning))
                                break;
                        }
                        this._patientScheduleService.DeletePatientScheduleDateByID(row.THURSDAY_SCHEDULE_ID.Trim());

                        row.THURSDAY = string.Empty;
                        row.THURSDAYPATIENT = string.Empty;
                        row.THURSDAYREMARK = string.Empty;
                        row.THURSDAYHEMOID = string.Empty;
                        row.THURSDAY_SCHEDULE_ID = string.Empty;
                        ShowForm("删除成功！");

                        break;
                    }
                case "gridFriday":
                    {
                        if (!row.IsFRIDAY_START_TIMENull() && !string.IsNullOrEmpty(row.FRIDAY_START_TIME))
                        {
                            if (DialogResult.OK == MessageBox.Show("患者开始治疗无法删除!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning))
                                break;
                        }
                        if (!row.IsFRIDAY_RECIPE_IDNull() && !string.IsNullOrEmpty(row.FRIDAY_RECIPE_ID))
                        {
                            if (DialogResult.OK == MessageBox.Show("患者已确认处方无法删除,请先取消处方确认再删除患者。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning))
                                break;
                        }
                        this._patientScheduleService.DeletePatientScheduleDateByID(row.FRIDAY_SCHEDULE_ID.Trim());

                        row.FRIDAY = string.Empty;
                        row.FRIDAYPATIENT = string.Empty;
                        row.FRIDAYREMARK = string.Empty;
                        row.FRIDAYHEMOID = string.Empty;
                        row.FRIDAY_SCHEDULE_ID = string.Empty;
                        ShowForm("删除成功！");

                        break;
                    }
                case "gridSaturday":
                    {
                        if (!row.IsSTATURDAY_START_TIMENull() && !string.IsNullOrEmpty(row.STATURDAY_START_TIME))
                        {
                            if (DialogResult.OK == MessageBox.Show("患者开始治疗无法删除!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning))
                                break;
                        }
                        if (!row.IsSTATURDAY_RECIPE_IDNull() && !string.IsNullOrEmpty(row.STATURDAY_RECIPE_ID))
                        {
                            if (DialogResult.OK == MessageBox.Show("患者已确认处方无法删除,请先取消处方确认再删除患者。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning))
                                break;
                        }
                        this._patientScheduleService.DeletePatientScheduleDateByID(row.STATURDAY_SCHEDULE_ID.Trim());

                        row.STATURDAY = string.Empty;
                        row.STATURDAYPATIENT = string.Empty;
                        row.STATURDAYREMARK = string.Empty;
                        row.STATURDAYHEMOID = string.Empty;
                        row.STATURDAY_SCHEDULE_ID = string.Empty;
                        ShowForm("删除成功！");

                        break;
                    }
                case "gridSunday":
                    {
                        if (!row.IsSUNDAY_START_TIMENull() && !string.IsNullOrEmpty(row.SUNDAY_START_TIME))
                        {
                            if (DialogResult.OK == MessageBox.Show("患者开始治疗无法删除!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning))
                                break;
                        }
                        if (!row.IsSUNDAY_RECIPE_IDNull() && !string.IsNullOrEmpty(row.SUNDAY_RECIPE_ID))
                        {
                            if (DialogResult.OK == MessageBox.Show("患者已确认处方无法删除,请先取消处方确认再删除患者。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning))
                                break;
                        }
                        this._patientScheduleService.DeletePatientScheduleDateByID(row.SUNDAY_SCHEDULE_ID.Trim());

                        row.SUNDAY = string.Empty;
                        row.SUNDAYPATIENT = string.Empty;
                        row.SUNDAYREMARK = string.Empty;
                        row.SUNDAYHEMOID = string.Empty;
                        row.SUNDAY_SCHEDULE_ID = string.Empty;
                        ShowForm("删除成功！");

                        break;
                    }
            }

            _isDirty = true;
            #endregion
        }

        private void ShowForm(string caption)
        {
            AutoClosedMsgBox.ShowForm(caption, "提示", 1000, MessageBoxIcon.Information);

        }

        /// <summary>
        /// 变色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            var row = gridView1.GetDataRow(e.RowHandle) as Hemo.Model.PermissionModel.MED_HEMO_SCHEDULEMASTERRow;
            if (row == null)
            {
                return;
            }
            if (e.RowHandle >= 0 && (e.Column == gridBedNo || e.Column == gridMonday || e.Column == gridFriday || e.Column == gridSunday || e.Column == gridThursday || e.Column == gridTuesday || e.Column == gridWednesday || e.Column == gridSaturday))
            {
                if (e.RowHandle == gridView1.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.FromArgb(49, 106, 197);
                    e.Appearance.ForeColor = Color.White;
                    return;

                }
                else if (e.RowHandle % 2 == 0)
                {

                    //e.Appearance.BackColor = Color.FromArgb(234, 247, 255);
                }
                if (e.Column != gridBedNo)
                {
                    var patientName = string.Format("{0}", row[e.Column.FieldName]);
                    if (string.IsNullOrEmpty(patientName))
                    {
                        return;
                    }
                    var starttime = string.Format("{0}", row[e.Column.FieldName + "_START_TIME"]);
                    var endtime = string.Format("{0}", row[e.Column.FieldName + "_END_TIME"]);
                    var recipet = string.Format("{0}", row[e.Column.FieldName + "_RECIPE_ID"]);
                    if (!string.IsNullOrEmpty(starttime) && string.IsNullOrEmpty(endtime))
                    {
                        e.Appearance.ForeColor = Color.Blue;
                    }
                    else if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                    else if (string.IsNullOrEmpty(starttime) && string.IsNullOrEmpty(endtime) && !string.IsNullOrEmpty(recipet))
                    {
                        e.Appearance.ForeColor = Color.Green;
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.Black;
                    }
                }
            }
        }

        /// <summary>
        /// 合并单元格...？
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.Column.FieldName == "QYNAME")
            {
                //GridView view = sender as GridView;
                //var val1 = view.GetRowCellValue(e.RowHandle1, e.Column);
                //var val2 = view.GetRowCellValue(e.RowHandle2, e.Column);
                //e.Merge = val1 == val2;
                //e.Handled = true;
            }
            else
            {
                //e.Merge = false;
                //e.Handled = false;
            }
        }

        private void gridControlForEmerger_MouseDown(object sender, MouseEventArgs e)
        {
            hitInfo = gridView1.CalcHitInfo(new Point(e.X, e.Y));
            if (hitInfo.RowHandle < 0) hitInfo = null;
        }

        /// <summary>
        /// 点击移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControlForEmerger_MouseMove(object sender, MouseEventArgs e)
        {
            if (hitInfo == null) return;
            if (e.Button != MouseButtons.Left) return;
            Rectangle dragRect = new Rectangle(new Point(
                hitInfo.HitPoint.X - SystemInformation.DragSize.Width / 2,
                hitInfo.HitPoint.Y - SystemInformation.DragSize.Height / 2), SystemInformation.DragSize);
            if (!dragRect.Contains(new Point(e.X, e.Y)))
            {
                if (hitInfo.InRowCell)
                {
                    var data = gridView1.GetDataRow(hitInfo.RowHandle) as PermissionModel.MED_HEMO_SCHEDULEMASTERRow;
                    #region 拖动的时候进行数据传递，三个参数【行、列名、行数据】

                    switch (hitInfo.Column.Name)
                    {
                        case "gridMonday":
                            {
                                if (!data.IsMONDAYNull() && !string.IsNullOrEmpty(data.MONDAY))
                                {
                                    if (!string.IsNullOrEmpty(data.MONDAY_START_TIME))
                                    {
                                        MessageBox.Show("已经开始治疗的患者不能进行移动！");
                                        break;
                                    }
                                    if (!string.IsNullOrEmpty(data.MONDAY_RECIPE_ID))
                                    {
                                        MessageBox.Show("已经确认处方的患者不能进行移动！");
                                        break;
                                    }
                                    gridControlForEmerger.DoDragDrop(new object[] { hitInfo.RowHandle, hitInfo.Column.Name, data }, DragDropEffects.Copy);
                                }
                                break;
                            }
                        case "gridTuesday":
                            {
                                if (!data.IsTUESDAYNull() && !string.IsNullOrEmpty(data.TUESDAY))
                                {
                                    if (!string.IsNullOrEmpty(data.TUESDAY_START_TIME))
                                    {
                                        MessageBox.Show("已经开始治疗的患者不能进行移动！");
                                        break;
                                    }
                                    if (!string.IsNullOrEmpty(data.TUESDAY_RECIPE_ID))
                                    {
                                        MessageBox.Show("已经确认处方的患者不能进行移动！");
                                        break;
                                    }
                                    gridControlForEmerger.DoDragDrop(new object[] { hitInfo.RowHandle, hitInfo.Column.Name, data }, DragDropEffects.Copy);
                                }
                                break;
                            }
                        case "gridWednesday":
                            {
                                if (!data.IsWEDNESDAYNull() && !string.IsNullOrEmpty(data.WEDNESDAY))
                                {
                                    if (!string.IsNullOrEmpty(data.WEDNESDAY_START_TIME))
                                    {
                                        MessageBox.Show("已经开始治疗的患者不能进行移动！");
                                        break;
                                    }
                                    if (!string.IsNullOrEmpty(data.WEDNESDAY_RECIPE_ID))
                                    {
                                        MessageBox.Show("已经确认处方的患者不能进行移动！");
                                        break;
                                    }
                                    gridControlForEmerger.DoDragDrop(new object[] { hitInfo.RowHandle, hitInfo.Column.Name, data }, DragDropEffects.Copy);
                                }
                                break;
                            }
                        case "gridThursday":
                            {
                                if (!data.IsTHURSDAYNull() && !string.IsNullOrEmpty(data.THURSDAY))
                                {
                                    if (!string.IsNullOrEmpty(data.THURSDAY_START_TIME))
                                    {
                                        MessageBox.Show("已经开始治疗的患者不能进行移动！");
                                        break;
                                    }
                                    if (!string.IsNullOrEmpty(data.THURSDAY_RECIPE_ID))
                                    {
                                        MessageBox.Show("已经确认处方的患者不能进行移动！");
                                        break;
                                    }
                                    gridControlForEmerger.DoDragDrop(new object[] { hitInfo.RowHandle, hitInfo.Column.Name, data }, DragDropEffects.Copy);
                                }
                                break;
                            }
                        case "gridFriday":
                            {
                                if (!data.IsFRIDAYNull() && !string.IsNullOrEmpty(data.FRIDAY))
                                {
                                    if (!string.IsNullOrEmpty(data.FRIDAY_START_TIME))
                                    {
                                        MessageBox.Show("已经开始治疗的患者不能进行移动！");
                                        break;
                                    }
                                    if (!string.IsNullOrEmpty(data.FRIDAY_RECIPE_ID))
                                    {
                                        MessageBox.Show("已经确认处方的患者不能进行移动！");
                                        break;
                                    }
                                    gridControlForEmerger.DoDragDrop(new object[] { hitInfo.RowHandle, hitInfo.Column.Name, data }, DragDropEffects.Copy);
                                }
                                break;
                            }
                        case "gridSaturday":
                            {
                                if (!data.IsSTATURDAYNull() && !string.IsNullOrEmpty(data.STATURDAY))
                                {
                                    if (!string.IsNullOrEmpty(data.STATURDAY_START_TIME))
                                    {
                                        MessageBox.Show("已经开始治疗的患者不能进行移动！");
                                        break;
                                    }
                                    gridControlForEmerger.DoDragDrop(new object[] { hitInfo.RowHandle, hitInfo.Column.Name, data }, DragDropEffects.Copy);
                                }
                                if (!string.IsNullOrEmpty(data.STATURDAY_RECIPE_ID))
                                {
                                    MessageBox.Show("已经确认处方的患者不能进行移动！");
                                    break;
                                }
                                break;
                            }
                        case "gridSunday":
                            {
                                if (!data.IsSUNDAYNull() && !string.IsNullOrEmpty(data.SUNDAY))
                                {
                                    if (!string.IsNullOrEmpty(data.SUNDAY_START_TIME))
                                    {
                                        MessageBox.Show("已经开始治疗的患者不能进行移动！");
                                        break;
                                    }
                                    if (!string.IsNullOrEmpty(data.SUNDAY_RECIPE_ID))
                                    {
                                        MessageBox.Show("已经确认处方的患者不能进行移动！");
                                        break;
                                    }
                                    gridControlForEmerger.DoDragDrop(new object[] { hitInfo.RowHandle, hitInfo.Column.Name, data }, DragDropEffects.Copy);
                                }
                                break;
                            }
                    }

                    #endregion

                }

            }
        }

        /// <summary>
        /// 获取拖动时传过来的第一个参数【行】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private int GetDragObject(object sender, IDataObject data)
        {
            object[] obj = data.GetData(typeof(object[])) as object[];//获取数据
            if (obj == null || !sender.Equals(gridControlForEmerger)) return -2;
            if (obj[0] is Int32)//返回第一个值
                return (int)obj[0];
            else return -1;
        }
        /// <summary>
        /// 获取拖动时传过来的第一个参数【行】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private int GetDragObjectRowHander(object sender, IDataObject data)
        {
            object[] obj = data.GetData(typeof(object[])) as object[];
            if (obj == null || !sender.Equals(gridControlForEmerger)) return -2;
            if (obj[0] is Int32)
                return (int)obj[0];
            else return -1;
        }
        /// <summary>
        /// 获取拖动时传过来的第二个参数【原列名】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string GetDragObjectColumnHander(object sender, IDataObject data)
        {
            object[] obj = data.GetData(typeof(object[])) as object[];
            if (obj == null || !sender.Equals(gridControlForEmerger)) return string.Empty;
            if (obj[1] is string)
                return (string)obj[1];
            else return string.Empty;
        }
        /// <summary>
        /// 拖至目标位置进行列数据值的互换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControlForEmerger_DragDrop(object sender, DragEventArgs e)
        {
            GridHitInfo hi = gridView1.CalcHitInfo(gridControlForEmerger.PointToClient(new Point(e.X, e.Y)));
            int rhandle = GetDragObjectRowHander(sender, e.Data);//取原行
            string chandle = GetDragObjectColumnHander(sender, e.Data);//取原列名
            if (rhandle < 0 || string.IsNullOrEmpty(chandle)) return;
            if (hi.Column == null || hi.RowHandle < 0) return;
            if (chandle == hi.Column.Name && rhandle == hi.RowHandle) return;
            if (hi.InRowCell)
            {
                var row = gridView1.GetDataRow(rhandle) as PermissionModel.MED_HEMO_SCHEDULEMASTERRow;      //获取原行的行数据        

                int newHandle = hi.RowHandle;//目标行
                if (newHandle < 0)
                    return;
                var rowReciever = gridView1.GetDataRow(newHandle) as PermissionModel.MED_HEMO_SCHEDULEMASTERRow;//目标行数据

                if (hi.Column.Name.ToString().Length > 4)
                {
                    if (!string.IsNullOrEmpty(rowReciever[string.Format("{0}", hi.Column.Name.Substring(4) == "Saturday" ? "Staturday" : hi.Column.Name.Substring(4))].ToString()))
                    {
                        if (!string.IsNullOrEmpty(rowReciever[string.Format("{0}_START_TIME", hi.Column.Name.Substring(4) == "Saturday" ? "Staturday" : hi.Column.Name.Substring(4))].ToString()))
                        {
                            var msg = string.Format("患者【{0}】已开始治疗不能交换!", rowReciever[string.Format("{0}", hi.Column.Name.Substring(4) == "Saturday" ? "Staturday" : hi.Column.Name.Substring(4))].ToString());
                            AutoClosedMsgBox.ShowForm(msg, "提示", 3000, MessageBoxIcon.Question);
                            return;
                        }

                        if (!string.IsNullOrEmpty(rowReciever[string.Format("{0}_RECIPE_ID", hi.Column.Name.Substring(4) == "Saturday" ? "Staturday" : hi.Column.Name.Substring(4))].ToString()))
                        {
                            var msg = string.Format("患者【{0}】已确认处方不能交换!", rowReciever[string.Format("{0}", hi.Column.Name.Substring(4) == "Saturday" ? "Staturday" : hi.Column.Name.Substring(4))].ToString());
                            AutoClosedMsgBox.ShowForm(msg, "提示", 3000, MessageBoxIcon.Question);
                            return;
                        }
                    }
                }

                string strResult = string.Empty;
                if (string.IsNullOrEmpty(rowReciever[string.Format("{0}", hi.Column.Name.Substring(4) == "Saturday" ? "Staturday" : hi.Column.Name.Substring(4))].ToString()))
                {
                    strResult = CheckCRRTCureStatus(rowReciever);
                    if (!string.IsNullOrEmpty(strResult))
                    {
                        if (XtraMessageBox.Show(strResult, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Question) == DialogResult.OK)
                            return;
                    }
                }

                var value = this._insulateArea.FirstOrDefault(i => i.ITEM_VALUE == rowReciever.QYNAME);
                var value1 = this._insulateArea.FirstOrDefault(i => i.ITEM_VALUE == row.QYNAME);
                if (rowReciever.QYNAME != row.QYNAME)
                {
                    if (value != null)
                    {
                        var fileName = this.gridView1.FocusedColumn.FieldName;
                        strResult = string.Format(CheckArea(rowReciever.QYNAME.ToString(), row[string.Format("{0}_INFECTIOUS_CHECK_RESULT", fileName)].ToString(), rowReciever.CWNAME).Trim(), row[string.Format("{0}", fileName)].ToString().Substring(0, 3));

                        if (strResult != "")
                        {
                            if (XtraMessageBox.Show(strResult, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                return;
                        }
                    }

                    var fileName1 = this.gridView1.FocusedColumn.FieldName;
                    if (value1 != null && !string.IsNullOrEmpty(rowReciever[string.Format("{0}", fileName1)].ToString()))
                    {
                        strResult = string.Format(CheckArea(row.QYNAME.ToString(), rowReciever[string.Format("{0}_INFECTIOUS_CHECK_RESULT", fileName1)].ToString(), row.CWNAME).Trim(), rowReciever[string.Format("{0}", fileName1)].ToString().Substring(0, 3));

                        if (strResult != "")
                        {
                            if (XtraMessageBox.Show(strResult, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                return;
                        }
                    }
                }

                var fileName2 = this.gridView1.FocusedColumn.FieldName;
                var hiColumnName = hi.Column.Name.Substring(4) == "Saturday" ? "Staturday" : hi.Column.Name.Substring(4);
                var rowExisit = _ScheduleDataTableMain.FirstOrDefault(i => i[string.Format("{0}HEMOID", hiColumnName.ToUpper())].ToString() == row[string.Format("{0}HEMOID", fileName2)].ToString() && hiColumnName.ToUpper() != fileName2);
                if (rowExisit != null)
                {
                    #region 九龙医院有时会出现个别上午下午都会透析的患者，要求改动！

                    var msg = string.Format("患者【{0}】已在【{1}】的【{2}班】进行了排班,不允许重复排班!", rowExisit[hiColumnName.ToUpper()], rowExisit.QYNAME, rowExisit.TIMETABLENAME);
                    AutoClosedMsgBox.ShowForm(msg, "提示", 3000, MessageBoxIcon.Question);
                    return;

                    #endregion
                }
                else
                {
                    if (!string.IsNullOrEmpty(rowReciever[string.Format("{0}HEMOID", hiColumnName)].ToString()))
                    {
                        var rowExisit2 = _ScheduleDataTableMain.FirstOrDefault(i => i[string.Format("{0}HEMOID", fileName2.ToUpper())].ToString() == rowReciever[string.Format("{0}HEMOID", hiColumnName)].ToString() && hiColumnName.ToUpper() != fileName2);
                        if (rowExisit2 != null)
                        {
                            #region 九龙医院有时会出现个别上午下午都会透析的患者，要求改动！

                            var msg = string.Format("患者【{0}】已在【{1}】的【{2}班】进行了排班,不允许重复排班!", rowExisit2[fileName2.ToUpper()], rowExisit2.QYNAME, rowExisit2.TIMETABLENAME);
                            AutoClosedMsgBox.ShowForm(msg, "提示", 3000, MessageBoxIcon.Question);
                            return;

                            #endregion
                        }
                    }
                }

                //PermissionModel.MED_HEMO_SCHEDULEMASTERRow newRow = null;
                if (e.Effect == DragDropEffects.Copy)
                {
                    #region 原值与目标值 进行数据互换

                    switch (hi.Column.Name)//取目标列名
                    {
                        case "gridMonday":
                            {
                                //目标列和原列进行比较，然后进行值的互换
                                switch (chandle)
                                {
                                    case "gridMonday":
                                        {
                                            if (!rowReciever.IsMONDAYNull() && !string.IsNullOrEmpty(rowReciever.MONDAY) && !rowReciever.MONDAYHEMOID.Equals(row.MONDAYHEMOID))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.MONDAY;
                                                    rowReciever.MONDAY = row.MONDAY;
                                                    row.MONDAY = tranStr;

                                                    tranStr = rowReciever.MONDAYPATIENT;
                                                    rowReciever.MONDAYPATIENT = row.MONDAYPATIENT;
                                                    row.MONDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.MONDAYREMARK;
                                                    rowReciever.MONDAYREMARK = row.MONDAYREMARK;
                                                    row.MONDAYREMARK = tranStr;

                                                    tranStr = rowReciever.MONDAYHEMOID;
                                                    rowReciever.MONDAYHEMOID = row.MONDAYHEMOID;
                                                    row.MONDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.MONDAY_SCHEDULE_ID;
                                                    rowReciever.MONDAY_SCHEDULE_ID = row.MONDAY_SCHEDULE_ID;
                                                    row.MONDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.MONDAY_PURIFIER_ID;
                                                    rowReciever.MONDAY_PURIFIER_ID = row.MONDAY_PURIFIER_ID;
                                                    row.MONDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.MONDAY = row.MONDAY;
                                                rowReciever.MONDAYPATIENT = row.MONDAYPATIENT;
                                                rowReciever.MONDAYREMARK = row.MONDAYREMARK;
                                                rowReciever.MONDAYHEMOID = row.MONDAYHEMOID;
                                                rowReciever.MONDAY_SCHEDULE_ID = row.MONDAY_SCHEDULE_ID;
                                                rowReciever.MONDAY_PURIFIER_ID = row.MONDAY_PURIFIER_ID;
                                                rowReciever.MONDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.MONDAY = string.Empty;
                                                row.MONDAYPATIENT = string.Empty;
                                                row.MONDAYREMARK = string.Empty;
                                                row.MONDAYHEMOID = string.Empty;
                                                row.MONDAY_SCHEDULE_ID = string.Empty;
                                                row.MONDAY_PURIFIER_ID = string.Empty;
                                                row.MONDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridTuesday":
                                        {
                                            if (!rowReciever.IsMONDAYNull() && !string.IsNullOrEmpty(rowReciever.MONDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.MONDAY;
                                                    rowReciever.MONDAY = row.TUESDAY;
                                                    row.TUESDAY = tranStr;

                                                    tranStr = rowReciever.MONDAYPATIENT;
                                                    rowReciever.MONDAYPATIENT = row.TUESDAYPATIENT;
                                                    row.TUESDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.MONDAYREMARK;
                                                    rowReciever.MONDAYREMARK = row.TUESDAYREMARK;
                                                    row.TUESDAYREMARK = tranStr;

                                                    tranStr = rowReciever.MONDAYHEMOID;
                                                    rowReciever.MONDAYHEMOID = row.TUESDAYHEMOID;
                                                    row.TUESDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.MONDAY_SCHEDULE_ID;
                                                    rowReciever.MONDAY_SCHEDULE_ID = row.TUESDAY_SCHEDULE_ID;
                                                    row.TUESDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.MONDAY_PURIFIER_ID;
                                                    rowReciever.MONDAY_PURIFIER_ID = row.TUESDAY_PURIFIER_ID;
                                                    row.TUESDAY_PURIFIER_ID = tranStr;

                                                }
                                            }
                                            else
                                            {
                                                rowReciever.MONDAY = row.TUESDAY;
                                                rowReciever.MONDAYPATIENT = row.TUESDAYPATIENT;
                                                rowReciever.MONDAYREMARK = row.TUESDAYREMARK;
                                                rowReciever.MONDAYHEMOID = row.TUESDAYHEMOID;
                                                rowReciever.MONDAY_SCHEDULE_ID = row.TUESDAY_SCHEDULE_ID;
                                                rowReciever.MONDAY_PURIFIER_ID = row.TUESDAY_PURIFIER_ID;
                                                rowReciever.MONDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.TUESDAY = string.Empty;
                                                row.TUESDAYPATIENT = string.Empty;
                                                row.TUESDAYREMARK = string.Empty;
                                                row.TUESDAYHEMOID = string.Empty;
                                                row.TUESDAY_SCHEDULE_ID = string.Empty;
                                                row.TUESDAY_PURIFIER_ID = string.Empty;
                                                row.TUESDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridWednesday":
                                        {
                                            if (!rowReciever.IsMONDAYNull() && !string.IsNullOrEmpty(rowReciever.MONDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.MONDAY;
                                                    rowReciever.MONDAY = row.WEDNESDAY;
                                                    row.WEDNESDAY = tranStr;

                                                    tranStr = rowReciever.MONDAYPATIENT;
                                                    rowReciever.MONDAYPATIENT = row.WEDNESDAYPATIENT;
                                                    row.WEDNESDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.MONDAYREMARK;
                                                    rowReciever.MONDAYREMARK = row.WEDNESDAYREMARK;
                                                    row.WEDNESDAYREMARK = tranStr;

                                                    tranStr = rowReciever.MONDAYHEMOID;
                                                    rowReciever.MONDAYHEMOID = row.WEDNESDAYHEMOID;
                                                    row.WEDNESDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.MONDAY_SCHEDULE_ID;
                                                    rowReciever.MONDAY_SCHEDULE_ID = row.WEDNESDAY_SCHEDULE_ID;
                                                    row.WEDNESDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.MONDAY_PURIFIER_ID;
                                                    rowReciever.MONDAY_PURIFIER_ID = row.WEDNESDAY_PURIFIER_ID;
                                                    row.WEDNESDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.MONDAY = row.WEDNESDAY;
                                                rowReciever.MONDAYPATIENT = row.WEDNESDAYPATIENT;
                                                rowReciever.MONDAYREMARK = row.WEDNESDAYREMARK;
                                                rowReciever.MONDAYHEMOID = row.WEDNESDAYHEMOID;
                                                rowReciever.MONDAY_SCHEDULE_ID = row.WEDNESDAY_SCHEDULE_ID;
                                                rowReciever.MONDAY_PURIFIER_ID = row.WEDNESDAY_PURIFIER_ID;
                                                rowReciever.MONDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.WEDNESDAY = string.Empty;
                                                row.WEDNESDAYPATIENT = string.Empty;
                                                row.WEDNESDAYREMARK = string.Empty;
                                                row.WEDNESDAYHEMOID = string.Empty;
                                                row.WEDNESDAY_PURIFIER_ID = string.Empty;
                                                row.WEDNESDAY_SCHEDULE_ID = string.Empty;
                                                row.WEDNESDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridThursday":
                                        {
                                            if (!rowReciever.IsMONDAYNull() && !string.IsNullOrEmpty(rowReciever.MONDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.MONDAY;
                                                    rowReciever.MONDAY = row.THURSDAY;
                                                    row.THURSDAY = tranStr;

                                                    tranStr = rowReciever.MONDAYPATIENT;
                                                    rowReciever.MONDAYPATIENT = row.THURSDAYPATIENT;
                                                    row.THURSDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.MONDAYREMARK;
                                                    rowReciever.MONDAYREMARK = row.THURSDAYREMARK;
                                                    row.THURSDAYREMARK = tranStr;

                                                    tranStr = rowReciever.MONDAYHEMOID;
                                                    rowReciever.MONDAYHEMOID = row.THURSDAYHEMOID;
                                                    row.THURSDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.MONDAY_SCHEDULE_ID;
                                                    rowReciever.MONDAY_SCHEDULE_ID = row.THURSDAY_SCHEDULE_ID;
                                                    row.THURSDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.MONDAY_PURIFIER_ID;
                                                    rowReciever.MONDAY_PURIFIER_ID = row.THURSDAY_PURIFIER_ID;
                                                    row.THURSDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.MONDAY = row.THURSDAY;
                                                rowReciever.MONDAYPATIENT = row.THURSDAYPATIENT;
                                                rowReciever.MONDAYREMARK = row.THURSDAYREMARK;
                                                rowReciever.MONDAYHEMOID = row.THURSDAYHEMOID;
                                                rowReciever.MONDAY_SCHEDULE_ID = row.THURSDAY_SCHEDULE_ID;
                                                rowReciever.MONDAY_PURIFIER_ID = row.THURSDAY_PURIFIER_ID;
                                                rowReciever.MONDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.THURSDAY = string.Empty;
                                                row.THURSDAYPATIENT = string.Empty;
                                                row.THURSDAYREMARK = string.Empty;
                                                row.THURSDAYHEMOID = string.Empty;
                                                row.THURSDAY_SCHEDULE_ID = string.Empty;
                                                row.THURSDAY_PURIFIER_ID = string.Empty;
                                                row.THURSDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridFriday":
                                        {
                                            if (!rowReciever.IsMONDAYNull() && !string.IsNullOrEmpty(rowReciever.MONDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.MONDAY;
                                                    rowReciever.MONDAY = row.FRIDAY;
                                                    row.FRIDAY = tranStr;

                                                    tranStr = rowReciever.MONDAYPATIENT;
                                                    rowReciever.MONDAYPATIENT = row.FRIDAYPATIENT;
                                                    row.FRIDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.MONDAYREMARK;
                                                    rowReciever.MONDAYREMARK = row.FRIDAYREMARK;
                                                    row.FRIDAYREMARK = tranStr;

                                                    tranStr = rowReciever.MONDAYHEMOID;
                                                    rowReciever.MONDAYHEMOID = row.FRIDAYHEMOID;
                                                    row.FRIDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.MONDAY_SCHEDULE_ID;
                                                    rowReciever.MONDAY_SCHEDULE_ID = row.FRIDAY_SCHEDULE_ID;
                                                    row.FRIDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.MONDAY_PURIFIER_ID;
                                                    rowReciever.MONDAY_PURIFIER_ID = row.FRIDAY_PURIFIER_ID;
                                                    row.FRIDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.MONDAY = row.FRIDAY;
                                                rowReciever.MONDAYPATIENT = row.FRIDAYPATIENT;
                                                rowReciever.MONDAYREMARK = row.FRIDAYREMARK;
                                                rowReciever.MONDAYHEMOID = row.FRIDAYHEMOID;
                                                rowReciever.MONDAY_SCHEDULE_ID = row.FRIDAY_SCHEDULE_ID;
                                                rowReciever.MONDAY_PURIFIER_ID = row.FRIDAY_PURIFIER_ID;
                                                rowReciever.MONDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.FRIDAY = string.Empty;
                                                row.FRIDAYPATIENT = string.Empty;
                                                row.FRIDAYREMARK = string.Empty;
                                                row.FRIDAYHEMOID = string.Empty;
                                                row.FRIDAY_SCHEDULE_ID = string.Empty;
                                                row.FRIDAY_PURIFIER_ID = string.Empty;
                                                row.FRIDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridSaturday":
                                        {
                                            if (!rowReciever.IsMONDAYNull() && !string.IsNullOrEmpty(rowReciever.MONDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.MONDAY;
                                                    rowReciever.MONDAY = row.STATURDAY;
                                                    row.STATURDAY = tranStr;

                                                    tranStr = rowReciever.MONDAYPATIENT;
                                                    rowReciever.MONDAYPATIENT = row.STATURDAYPATIENT;
                                                    row.STATURDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.MONDAYREMARK;
                                                    rowReciever.MONDAYREMARK = row.STATURDAYREMARK;
                                                    row.STATURDAYREMARK = tranStr;

                                                    tranStr = rowReciever.MONDAYHEMOID;
                                                    rowReciever.MONDAYHEMOID = row.STATURDAYHEMOID;
                                                    row.STATURDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.MONDAY_SCHEDULE_ID;
                                                    rowReciever.MONDAY_SCHEDULE_ID = row.STATURDAY_SCHEDULE_ID;
                                                    row.STATURDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.MONDAY_PURIFIER_ID;
                                                    rowReciever.MONDAY_PURIFIER_ID = row.STATURDAY_PURIFIER_ID;
                                                    row.STATURDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.MONDAY = row.STATURDAY;
                                                rowReciever.MONDAYPATIENT = row.STATURDAYPATIENT;
                                                rowReciever.MONDAYREMARK = row.STATURDAYREMARK;
                                                rowReciever.MONDAYHEMOID = row.STATURDAYHEMOID;
                                                rowReciever.MONDAY_SCHEDULE_ID = row.STATURDAY_SCHEDULE_ID;
                                                rowReciever.MONDAY_PURIFIER_ID = row.STATURDAY_PURIFIER_ID;
                                                rowReciever.MONDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.STATURDAY = string.Empty;
                                                row.STATURDAYPATIENT = string.Empty;
                                                row.STATURDAYREMARK = string.Empty;
                                                row.STATURDAYHEMOID = string.Empty;
                                                row.STATURDAY_SCHEDULE_ID = string.Empty;
                                                row.STATURDAY_PURIFIER_ID = string.Empty;
                                                row.STATURDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridSunday":
                                        {
                                            if (!rowReciever.IsMONDAYNull() && !string.IsNullOrEmpty(rowReciever.MONDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.MONDAY;
                                                    rowReciever.MONDAY = row.SUNDAY;
                                                    row.SUNDAY = tranStr;

                                                    tranStr = rowReciever.MONDAYPATIENT;
                                                    rowReciever.MONDAYPATIENT = row.SUNDAYPATIENT;
                                                    row.SUNDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.MONDAYREMARK;
                                                    rowReciever.MONDAYREMARK = row.SUNDAYREMARK;
                                                    row.SUNDAYREMARK = tranStr;

                                                    tranStr = rowReciever.MONDAYHEMOID;
                                                    rowReciever.MONDAYHEMOID = row.SUNDAYHEMOID;
                                                    row.SUNDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.MONDAY_SCHEDULE_ID;
                                                    rowReciever.MONDAY_SCHEDULE_ID = row.SUNDAY_SCHEDULE_ID;
                                                    row.SUNDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.MONDAY_PURIFIER_ID;
                                                    rowReciever.MONDAY_PURIFIER_ID = row.SUNDAY_PURIFIER_ID;
                                                    row.SUNDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.MONDAY = row.SUNDAY;
                                                rowReciever.MONDAYPATIENT = row.SUNDAYPATIENT;
                                                rowReciever.MONDAYREMARK = row.SUNDAYREMARK;
                                                rowReciever.MONDAYHEMOID = row.SUNDAYHEMOID;
                                                rowReciever.MONDAY_SCHEDULE_ID = row.SUNDAY_SCHEDULE_ID;
                                                rowReciever.MONDAY_PURIFIER_ID = row.SUNDAY_PURIFIER_ID;
                                                rowReciever.MONDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.SUNDAY = string.Empty;
                                                row.SUNDAYPATIENT = string.Empty;
                                                row.SUNDAYREMARK = string.Empty;
                                                row.SUNDAYHEMOID = string.Empty;
                                                row.SUNDAY_SCHEDULE_ID = string.Empty;
                                                row.SUNDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                }
                                break;
                            }
                        case "gridTuesday":
                            {
                                switch (chandle)
                                {
                                    case "gridMonday":
                                        {
                                            if (!rowReciever.IsTUESDAYHEMOIDNull() && !string.IsNullOrEmpty(rowReciever.TUESDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.TUESDAY;
                                                    rowReciever.TUESDAY = row.MONDAY;
                                                    row.MONDAY = tranStr;

                                                    tranStr = rowReciever.TUESDAYPATIENT;
                                                    rowReciever.TUESDAYPATIENT = row.MONDAYPATIENT;
                                                    row.MONDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.TUESDAYREMARK;
                                                    rowReciever.TUESDAYREMARK = row.MONDAYREMARK;
                                                    row.MONDAYREMARK = tranStr;

                                                    tranStr = rowReciever.TUESDAYHEMOID;
                                                    rowReciever.TUESDAYHEMOID = row.MONDAYHEMOID;
                                                    row.MONDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.TUESDAY_SCHEDULE_ID;
                                                    rowReciever.TUESDAY_SCHEDULE_ID = row.MONDAY_SCHEDULE_ID;
                                                    row.MONDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.TUESDAY_PURIFIER_ID;
                                                    rowReciever.TUESDAY_PURIFIER_ID = row.MONDAY_PURIFIER_ID;
                                                    row.MONDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.TUESDAY = row.MONDAY;
                                                rowReciever.TUESDAYPATIENT = row.MONDAYPATIENT;
                                                rowReciever.TUESDAYREMARK = row.MONDAYREMARK;
                                                rowReciever.TUESDAYHEMOID = row.MONDAYHEMOID;
                                                rowReciever.TUESDAY_SCHEDULE_ID = row.MONDAY_SCHEDULE_ID;
                                                rowReciever.TUESDAY_PURIFIER_ID = row.MONDAY_PURIFIER_ID;
                                                rowReciever.TUESDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.MONDAY = string.Empty;
                                                row.MONDAYPATIENT = string.Empty;
                                                row.MONDAYREMARK = string.Empty;
                                                row.MONDAYHEMOID = string.Empty;
                                                row.MONDAY_SCHEDULE_ID = string.Empty;
                                                row.MONDAY_PURIFIER_ID = string.Empty;
                                                row.MONDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridTuesday":
                                        {
                                            if (!rowReciever.IsTUESDAYHEMOIDNull() && !string.IsNullOrEmpty(rowReciever.TUESDAY) && !rowReciever.TUESDAYHEMOID.Equals(row.TUESDAYHEMOID))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.TUESDAY;
                                                    rowReciever.TUESDAY = row.TUESDAY;
                                                    row.TUESDAY = tranStr;

                                                    tranStr = rowReciever.TUESDAYPATIENT;
                                                    rowReciever.TUESDAYPATIENT = row.TUESDAYPATIENT;
                                                    row.TUESDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.TUESDAYREMARK;
                                                    rowReciever.TUESDAYREMARK = row.TUESDAYREMARK;
                                                    row.TUESDAYREMARK = tranStr;

                                                    tranStr = rowReciever.TUESDAYHEMOID;
                                                    rowReciever.TUESDAYHEMOID = row.TUESDAYHEMOID;
                                                    row.TUESDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.TUESDAY_SCHEDULE_ID;
                                                    rowReciever.TUESDAY_SCHEDULE_ID = row.TUESDAY_SCHEDULE_ID;
                                                    row.TUESDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.TUESDAY_PURIFIER_ID;
                                                    rowReciever.TUESDAY_PURIFIER_ID = row.MONDAY_PURIFIER_ID;
                                                    row.MONDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.TUESDAY = row.TUESDAY;
                                                rowReciever.TUESDAYPATIENT = row.TUESDAYPATIENT;
                                                rowReciever.TUESDAYREMARK = row.TUESDAYREMARK;
                                                rowReciever.TUESDAYHEMOID = row.TUESDAYHEMOID;
                                                rowReciever.TUESDAY_SCHEDULE_ID = row.TUESDAY_SCHEDULE_ID;
                                                rowReciever.TUESDAY_PURIFIER_ID = row.MONDAY_PURIFIER_ID;
                                                rowReciever.TUESDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.TUESDAY = string.Empty;
                                                row.TUESDAYPATIENT = string.Empty;
                                                row.TUESDAYREMARK = string.Empty;
                                                row.TUESDAYHEMOID = string.Empty;
                                                row.TUESDAY_SCHEDULE_ID = string.Empty;
                                                row.TUESDAY_PURIFIER_ID = string.Empty;
                                                row.TUESDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridWednesday":
                                        {
                                            if (!rowReciever.IsTUESDAYHEMOIDNull() && !string.IsNullOrEmpty(rowReciever.TUESDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.TUESDAY;
                                                    rowReciever.TUESDAY = row.WEDNESDAY;
                                                    row.WEDNESDAY = tranStr;

                                                    tranStr = rowReciever.TUESDAYPATIENT;
                                                    rowReciever.TUESDAYPATIENT = row.WEDNESDAYPATIENT;
                                                    row.WEDNESDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.TUESDAYREMARK;
                                                    rowReciever.TUESDAYREMARK = row.WEDNESDAYREMARK;
                                                    row.WEDNESDAYREMARK = tranStr;

                                                    tranStr = rowReciever.TUESDAYHEMOID;
                                                    rowReciever.TUESDAYHEMOID = row.WEDNESDAYHEMOID;
                                                    row.WEDNESDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.TUESDAY_SCHEDULE_ID;
                                                    rowReciever.TUESDAY_SCHEDULE_ID = row.WEDNESDAY_SCHEDULE_ID;
                                                    row.WEDNESDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.TUESDAY_PURIFIER_ID;
                                                    rowReciever.TUESDAY_PURIFIER_ID = row.WEDNESDAY_PURIFIER_ID;
                                                    row.WEDNESDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.TUESDAY = row.WEDNESDAY;
                                                rowReciever.TUESDAYPATIENT = row.WEDNESDAYPATIENT;
                                                rowReciever.TUESDAYREMARK = row.WEDNESDAYREMARK;
                                                rowReciever.TUESDAYHEMOID = row.WEDNESDAYHEMOID;
                                                rowReciever.TUESDAY_SCHEDULE_ID = row.WEDNESDAY_SCHEDULE_ID;
                                                rowReciever.TUESDAY_PURIFIER_ID = row.WEDNESDAY_PURIFIER_ID;
                                                rowReciever.TUESDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.WEDNESDAY = string.Empty;
                                                row.WEDNESDAYPATIENT = string.Empty;
                                                row.WEDNESDAYREMARK = string.Empty;
                                                row.WEDNESDAYHEMOID = string.Empty;
                                                row.WEDNESDAY_SCHEDULE_ID = string.Empty;
                                                row.WEDNESDAY_PURIFIER_ID = string.Empty;
                                                row.WEDNESDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridThursday":
                                        {
                                            if (!rowReciever.IsTUESDAYHEMOIDNull() && !string.IsNullOrEmpty(rowReciever.TUESDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.TUESDAY;
                                                    rowReciever.TUESDAY = row.THURSDAY;
                                                    row.THURSDAY = tranStr;

                                                    tranStr = rowReciever.TUESDAYPATIENT;
                                                    rowReciever.TUESDAYPATIENT = row.THURSDAYPATIENT;
                                                    row.THURSDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.TUESDAYREMARK;
                                                    rowReciever.TUESDAYREMARK = row.THURSDAYREMARK;
                                                    row.THURSDAYREMARK = tranStr;

                                                    tranStr = rowReciever.TUESDAYHEMOID;
                                                    rowReciever.TUESDAYHEMOID = row.THURSDAYHEMOID;
                                                    row.THURSDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.TUESDAY_SCHEDULE_ID;
                                                    rowReciever.TUESDAY_SCHEDULE_ID = row.THURSDAY_SCHEDULE_ID;
                                                    row.THURSDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.TUESDAY_PURIFIER_ID;
                                                    rowReciever.TUESDAY_PURIFIER_ID = row.THURSDAY_PURIFIER_ID;
                                                    row.THURSDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.TUESDAY = row.THURSDAY;
                                                rowReciever.TUESDAYPATIENT = row.THURSDAYPATIENT;
                                                rowReciever.TUESDAYREMARK = row.THURSDAYREMARK;
                                                rowReciever.TUESDAYHEMOID = row.THURSDAYHEMOID;
                                                rowReciever.TUESDAY_SCHEDULE_ID = row.THURSDAY_SCHEDULE_ID;
                                                rowReciever.TUESDAY_PURIFIER_ID = row.THURSDAY_PURIFIER_ID;
                                                rowReciever.TUESDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.THURSDAY = string.Empty;
                                                row.THURSDAYPATIENT = string.Empty;
                                                row.THURSDAYREMARK = string.Empty;
                                                row.THURSDAYHEMOID = string.Empty;
                                                row.THURSDAY_SCHEDULE_ID = string.Empty;
                                                row.THURSDAY_PURIFIER_ID = string.Empty;
                                                row.THURSDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridFriday":
                                        {
                                            if (!rowReciever.IsTUESDAYHEMOIDNull() && !string.IsNullOrEmpty(rowReciever.TUESDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.TUESDAY;
                                                    rowReciever.TUESDAY = row.FRIDAY;
                                                    row.FRIDAY = tranStr;

                                                    tranStr = rowReciever.TUESDAYPATIENT;
                                                    rowReciever.TUESDAYPATIENT = row.FRIDAYPATIENT;
                                                    row.FRIDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.TUESDAYREMARK;
                                                    rowReciever.TUESDAYREMARK = row.FRIDAYREMARK;
                                                    row.FRIDAYREMARK = tranStr;

                                                    tranStr = rowReciever.TUESDAYHEMOID;
                                                    rowReciever.TUESDAYHEMOID = row.FRIDAYHEMOID;
                                                    row.FRIDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.TUESDAY_SCHEDULE_ID;
                                                    rowReciever.TUESDAY_SCHEDULE_ID = row.FRIDAY_SCHEDULE_ID;
                                                    row.FRIDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.TUESDAY_PURIFIER_ID;
                                                    rowReciever.TUESDAY_PURIFIER_ID = row.FRIDAY_PURIFIER_ID;
                                                    row.FRIDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.TUESDAY = row.FRIDAY;
                                                rowReciever.TUESDAYPATIENT = row.FRIDAYPATIENT;
                                                rowReciever.TUESDAYREMARK = row.FRIDAYREMARK;
                                                rowReciever.TUESDAYHEMOID = row.FRIDAYHEMOID;
                                                rowReciever.TUESDAY_SCHEDULE_ID = row.FRIDAY_SCHEDULE_ID;
                                                rowReciever.TUESDAY_PURIFIER_ID = row.FRIDAY_PURIFIER_ID;
                                                rowReciever.TUESDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.FRIDAY = string.Empty;
                                                row.FRIDAYPATIENT = string.Empty;
                                                row.FRIDAYREMARK = string.Empty;
                                                row.FRIDAYHEMOID = string.Empty;
                                                row.FRIDAY_SCHEDULE_ID = string.Empty;
                                                row.FRIDAY_PURIFIER_ID = string.Empty;
                                                row.FRIDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridSaturday":
                                        {
                                            if (!rowReciever.IsTUESDAYHEMOIDNull() && !string.IsNullOrEmpty(rowReciever.TUESDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.TUESDAY;
                                                    rowReciever.TUESDAY = row.STATURDAY;
                                                    row.STATURDAY = tranStr;

                                                    tranStr = rowReciever.TUESDAYPATIENT;
                                                    rowReciever.TUESDAYPATIENT = row.STATURDAYPATIENT;
                                                    row.STATURDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.TUESDAYREMARK;
                                                    rowReciever.TUESDAYREMARK = row.STATURDAYREMARK;
                                                    row.STATURDAYREMARK = tranStr;

                                                    tranStr = rowReciever.TUESDAYHEMOID;
                                                    rowReciever.TUESDAYHEMOID = row.STATURDAYHEMOID;
                                                    row.STATURDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.TUESDAY_SCHEDULE_ID;
                                                    rowReciever.TUESDAY_SCHEDULE_ID = row.STATURDAY_SCHEDULE_ID;
                                                    row.STATURDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.TUESDAY_PURIFIER_ID;
                                                    rowReciever.TUESDAY_PURIFIER_ID = row.STATURDAY_PURIFIER_ID;
                                                    row.STATURDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.TUESDAY = row.STATURDAY;
                                                rowReciever.TUESDAYPATIENT = row.STATURDAYPATIENT;
                                                rowReciever.TUESDAYREMARK = row.STATURDAYREMARK;
                                                rowReciever.TUESDAYHEMOID = row.STATURDAYHEMOID;
                                                rowReciever.TUESDAY_SCHEDULE_ID = row.STATURDAY_SCHEDULE_ID;
                                                rowReciever.TUESDAY_PURIFIER_ID = row.STATURDAY_PURIFIER_ID;
                                                rowReciever.TUESDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.STATURDAY = string.Empty;
                                                row.STATURDAYPATIENT = string.Empty;
                                                row.STATURDAYREMARK = string.Empty;
                                                row.STATURDAYHEMOID = string.Empty;
                                                row.STATURDAY_SCHEDULE_ID = string.Empty;
                                                row.STATURDAY_PURIFIER_ID = string.Empty;
                                                row.STATURDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridSunday":
                                        {
                                            if (!rowReciever.IsTUESDAYHEMOIDNull() && !string.IsNullOrEmpty(rowReciever.TUESDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.TUESDAY;
                                                    rowReciever.TUESDAY = row.SUNDAY;
                                                    row.SUNDAY = tranStr;

                                                    tranStr = rowReciever.TUESDAYPATIENT;
                                                    rowReciever.TUESDAYPATIENT = row.SUNDAYPATIENT;
                                                    row.SUNDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.TUESDAYREMARK;
                                                    rowReciever.TUESDAYREMARK = row.SUNDAYREMARK;
                                                    row.SUNDAYREMARK = tranStr;

                                                    tranStr = rowReciever.TUESDAYHEMOID;
                                                    rowReciever.TUESDAYHEMOID = row.SUNDAYHEMOID;
                                                    row.SUNDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.TUESDAY_SCHEDULE_ID;
                                                    rowReciever.TUESDAY_SCHEDULE_ID = row.SUNDAY_SCHEDULE_ID;
                                                    row.SUNDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.TUESDAY_PURIFIER_ID;
                                                    rowReciever.TUESDAY_PURIFIER_ID = row.SUNDAY_PURIFIER_ID;
                                                    row.SUNDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.TUESDAY = row.SUNDAY;
                                                rowReciever.TUESDAYPATIENT = row.SUNDAYPATIENT;
                                                rowReciever.TUESDAYREMARK = row.SUNDAYREMARK;
                                                rowReciever.TUESDAYHEMOID = row.SUNDAYHEMOID;
                                                rowReciever.TUESDAY_SCHEDULE_ID = row.SUNDAY_SCHEDULE_ID;
                                                rowReciever.TUESDAY_PURIFIER_ID = row.SUNDAY_PURIFIER_ID;
                                                rowReciever.TUESDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.SUNDAY = string.Empty;
                                                row.SUNDAYPATIENT = string.Empty;
                                                row.SUNDAYREMARK = string.Empty;
                                                row.SUNDAYHEMOID = string.Empty;
                                                row.SUNDAY_SCHEDULE_ID = string.Empty;
                                                row.SUNDAY_PURIFIER_ID = string.Empty;
                                                row.SUNDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                }
                                break;
                            }
                        case "gridWednesday":
                            {
                                switch (chandle)
                                {
                                    case "gridMonday":
                                        {
                                            if (!rowReciever.IsWEDNESDAYNull() && !string.IsNullOrEmpty(rowReciever.WEDNESDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.WEDNESDAY;
                                                    rowReciever.WEDNESDAY = row.MONDAY;
                                                    row.MONDAY = tranStr;

                                                    tranStr = rowReciever.WEDNESDAYPATIENT;
                                                    rowReciever.WEDNESDAYPATIENT = row.MONDAYPATIENT;
                                                    row.MONDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.WEDNESDAYREMARK;
                                                    rowReciever.WEDNESDAYREMARK = row.MONDAYREMARK;
                                                    row.MONDAYREMARK = tranStr;

                                                    tranStr = rowReciever.WEDNESDAYHEMOID;
                                                    rowReciever.WEDNESDAYHEMOID = row.MONDAYHEMOID;
                                                    row.MONDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.WEDNESDAY_SCHEDULE_ID;
                                                    rowReciever.WEDNESDAY_SCHEDULE_ID = row.MONDAY_SCHEDULE_ID;
                                                    row.MONDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.WEDNESDAY_PURIFIER_ID;
                                                    rowReciever.WEDNESDAY_PURIFIER_ID = row.MONDAY_PURIFIER_ID;
                                                    row.MONDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.WEDNESDAY = row.MONDAY;
                                                rowReciever.WEDNESDAYPATIENT = row.MONDAYPATIENT;
                                                rowReciever.WEDNESDAYREMARK = row.MONDAYREMARK;
                                                rowReciever.WEDNESDAYHEMOID = row.MONDAYHEMOID;
                                                rowReciever.WEDNESDAY_SCHEDULE_ID = row.MONDAY_SCHEDULE_ID;
                                                rowReciever.WEDNESDAY_PURIFIER_ID = row.MONDAY_PURIFIER_ID;
                                                rowReciever.WEDNESDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.MONDAY = string.Empty;
                                                row.MONDAYPATIENT = string.Empty;
                                                row.MONDAYREMARK = string.Empty;
                                                row.MONDAYHEMOID = string.Empty;
                                                row.MONDAY_SCHEDULE_ID = string.Empty;
                                                row.MONDAY_PURIFIER_ID = string.Empty;
                                                row.MONDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridTuesday":
                                        {
                                            if (!rowReciever.IsWEDNESDAYNull() && !string.IsNullOrEmpty(rowReciever.WEDNESDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.WEDNESDAY;
                                                    rowReciever.WEDNESDAY = row.TUESDAY;
                                                    row.TUESDAY = tranStr;

                                                    tranStr = rowReciever.WEDNESDAYPATIENT;
                                                    rowReciever.WEDNESDAYPATIENT = row.TUESDAYPATIENT;
                                                    row.TUESDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.WEDNESDAYREMARK;
                                                    rowReciever.WEDNESDAYREMARK = row.TUESDAYREMARK;
                                                    row.TUESDAYREMARK = tranStr;

                                                    tranStr = rowReciever.WEDNESDAYHEMOID;
                                                    rowReciever.WEDNESDAYHEMOID = row.TUESDAYHEMOID;
                                                    row.TUESDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.WEDNESDAY_SCHEDULE_ID;
                                                    rowReciever.WEDNESDAY_SCHEDULE_ID = row.TUESDAY_SCHEDULE_ID;
                                                    row.TUESDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.WEDNESDAY_PURIFIER_ID;
                                                    rowReciever.WEDNESDAY_PURIFIER_ID = row.TUESDAY_PURIFIER_ID;
                                                    row.TUESDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.WEDNESDAY = row.TUESDAY;
                                                rowReciever.WEDNESDAYPATIENT = row.TUESDAYPATIENT;
                                                rowReciever.WEDNESDAYREMARK = row.TUESDAYREMARK;
                                                rowReciever.WEDNESDAYHEMOID = row.TUESDAYHEMOID;
                                                rowReciever.WEDNESDAY_SCHEDULE_ID = row.TUESDAY_SCHEDULE_ID;
                                                rowReciever.WEDNESDAY_PURIFIER_ID = row.TUESDAY_PURIFIER_ID;
                                                rowReciever.WEDNESDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.TUESDAY = string.Empty;
                                                row.TUESDAYPATIENT = string.Empty;
                                                row.TUESDAYREMARK = string.Empty;
                                                row.TUESDAYHEMOID = string.Empty;
                                                row.TUESDAY_SCHEDULE_ID = string.Empty;
                                                row.TUESDAY_PURIFIER_ID = string.Empty;
                                                row.TUESDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridWednesday":
                                        {
                                            if (!rowReciever.IsWEDNESDAYNull() && !string.IsNullOrEmpty(rowReciever.WEDNESDAY) && !rowReciever.WEDNESDAYHEMOID.Equals(row.WEDNESDAYHEMOID))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.WEDNESDAY;
                                                    rowReciever.WEDNESDAY = row.WEDNESDAY;
                                                    row.WEDNESDAY = tranStr;

                                                    tranStr = rowReciever.WEDNESDAYPATIENT;
                                                    rowReciever.WEDNESDAYPATIENT = row.WEDNESDAYPATIENT;
                                                    row.WEDNESDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.WEDNESDAYREMARK;
                                                    rowReciever.WEDNESDAYREMARK = row.WEDNESDAYREMARK;
                                                    row.WEDNESDAYREMARK = tranStr;

                                                    tranStr = rowReciever.WEDNESDAYHEMOID;
                                                    rowReciever.WEDNESDAYHEMOID = row.WEDNESDAYHEMOID;
                                                    row.WEDNESDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.WEDNESDAY_SCHEDULE_ID;
                                                    rowReciever.WEDNESDAY_SCHEDULE_ID = row.WEDNESDAY_SCHEDULE_ID;
                                                    row.WEDNESDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.WEDNESDAY_PURIFIER_ID;
                                                    rowReciever.WEDNESDAY_PURIFIER_ID = row.WEDNESDAY_PURIFIER_ID;
                                                    row.WEDNESDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.WEDNESDAY = row.WEDNESDAY;
                                                rowReciever.WEDNESDAYPATIENT = row.WEDNESDAYPATIENT;
                                                rowReciever.WEDNESDAYREMARK = row.WEDNESDAYREMARK;
                                                rowReciever.WEDNESDAYHEMOID = row.WEDNESDAYHEMOID;
                                                rowReciever.WEDNESDAY_SCHEDULE_ID = row.WEDNESDAY_SCHEDULE_ID;
                                                rowReciever.WEDNESDAY_PURIFIER_ID = row.WEDNESDAY_PURIFIER_ID;
                                                rowReciever.WEDNESDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.WEDNESDAY = string.Empty;
                                                row.WEDNESDAYPATIENT = string.Empty;
                                                row.WEDNESDAYREMARK = string.Empty;
                                                row.WEDNESDAYHEMOID = string.Empty;
                                                row.WEDNESDAY_SCHEDULE_ID = string.Empty;
                                                row.WEDNESDAY_PURIFIER_ID = string.Empty;
                                                row.WEDNESDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridThursday":
                                        {
                                            if (!rowReciever.IsWEDNESDAYNull() && !string.IsNullOrEmpty(rowReciever.WEDNESDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.WEDNESDAY;
                                                    rowReciever.WEDNESDAY = row.THURSDAY;
                                                    row.THURSDAY = tranStr;

                                                    tranStr = rowReciever.WEDNESDAYPATIENT;
                                                    rowReciever.WEDNESDAYPATIENT = row.THURSDAYPATIENT;
                                                    row.THURSDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.WEDNESDAYREMARK;
                                                    rowReciever.WEDNESDAYREMARK = row.THURSDAYREMARK;
                                                    row.THURSDAYREMARK = tranStr;

                                                    tranStr = rowReciever.WEDNESDAYHEMOID;
                                                    rowReciever.WEDNESDAYHEMOID = row.THURSDAYHEMOID;
                                                    row.THURSDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.WEDNESDAY_SCHEDULE_ID;
                                                    rowReciever.WEDNESDAY_SCHEDULE_ID = row.THURSDAY_SCHEDULE_ID;
                                                    row.THURSDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.WEDNESDAY_PURIFIER_ID;
                                                    rowReciever.WEDNESDAY_PURIFIER_ID = row.THURSDAY_PURIFIER_ID;
                                                    row.THURSDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.WEDNESDAY = row.THURSDAY;
                                                rowReciever.WEDNESDAYPATIENT = row.THURSDAYPATIENT;
                                                rowReciever.WEDNESDAYREMARK = row.THURSDAYREMARK;
                                                rowReciever.WEDNESDAYHEMOID = row.THURSDAYHEMOID;
                                                rowReciever.WEDNESDAY_SCHEDULE_ID = row.THURSDAY_SCHEDULE_ID;
                                                rowReciever.WEDNESDAY_PURIFIER_ID = row.THURSDAY_PURIFIER_ID;
                                                rowReciever.WEDNESDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.THURSDAY = string.Empty;
                                                row.THURSDAYPATIENT = string.Empty;
                                                row.THURSDAYREMARK = string.Empty;
                                                row.THURSDAYHEMOID = string.Empty;
                                                row.THURSDAY_SCHEDULE_ID = string.Empty;
                                                row.THURSDAY_PURIFIER_ID = string.Empty;
                                                row.THURSDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridFriday":
                                        {
                                            if (!rowReciever.IsWEDNESDAYNull() && !string.IsNullOrEmpty(rowReciever.WEDNESDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.WEDNESDAY;
                                                    rowReciever.WEDNESDAY = row.FRIDAY;
                                                    row.FRIDAY = tranStr;

                                                    tranStr = rowReciever.WEDNESDAYPATIENT;
                                                    rowReciever.WEDNESDAYPATIENT = row.FRIDAYPATIENT;
                                                    row.FRIDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.WEDNESDAYREMARK;
                                                    rowReciever.WEDNESDAYREMARK = row.FRIDAYREMARK;
                                                    row.FRIDAYREMARK = tranStr;

                                                    tranStr = rowReciever.WEDNESDAYHEMOID;
                                                    rowReciever.WEDNESDAYHEMOID = row.FRIDAYHEMOID;
                                                    row.FRIDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.WEDNESDAY_SCHEDULE_ID;
                                                    rowReciever.WEDNESDAY_SCHEDULE_ID = row.FRIDAY_SCHEDULE_ID;
                                                    row.FRIDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.WEDNESDAY_PURIFIER_ID;
                                                    rowReciever.WEDNESDAY_PURIFIER_ID = row.FRIDAY_PURIFIER_ID;
                                                    row.FRIDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.WEDNESDAY = row.FRIDAY;
                                                rowReciever.WEDNESDAYPATIENT = row.FRIDAYPATIENT;
                                                rowReciever.WEDNESDAYREMARK = row.FRIDAYREMARK;
                                                rowReciever.WEDNESDAYHEMOID = row.FRIDAYHEMOID;
                                                rowReciever.WEDNESDAY_SCHEDULE_ID = row.FRIDAY_SCHEDULE_ID;
                                                rowReciever.WEDNESDAY_PURIFIER_ID = row.FRIDAY_PURIFIER_ID;
                                                rowReciever.WEDNESDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.FRIDAY = string.Empty;
                                                row.FRIDAYPATIENT = string.Empty;
                                                row.FRIDAYREMARK = string.Empty;
                                                row.FRIDAYHEMOID = string.Empty;
                                                row.FRIDAY_SCHEDULE_ID = string.Empty;
                                                row.FRIDAY_PURIFIER_ID = string.Empty;
                                                row.FRIDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridSaturday":
                                        {
                                            if (!rowReciever.IsWEDNESDAYNull() && !string.IsNullOrEmpty(rowReciever.WEDNESDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.WEDNESDAY;
                                                    rowReciever.WEDNESDAY = row.STATURDAY;
                                                    row.STATURDAY = tranStr;

                                                    tranStr = rowReciever.WEDNESDAYPATIENT;
                                                    rowReciever.WEDNESDAYPATIENT = row.STATURDAYPATIENT;
                                                    row.STATURDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.WEDNESDAYREMARK;
                                                    rowReciever.WEDNESDAYREMARK = row.STATURDAYREMARK;
                                                    row.STATURDAYREMARK = tranStr;

                                                    tranStr = rowReciever.WEDNESDAYHEMOID;
                                                    rowReciever.WEDNESDAYHEMOID = row.STATURDAYHEMOID;
                                                    row.STATURDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.WEDNESDAY_SCHEDULE_ID;
                                                    rowReciever.WEDNESDAY_SCHEDULE_ID = row.STATURDAY_SCHEDULE_ID;
                                                    row.STATURDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.WEDNESDAY_PURIFIER_ID;
                                                    rowReciever.WEDNESDAY_PURIFIER_ID = row.STATURDAY_PURIFIER_ID;
                                                    row.STATURDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.WEDNESDAY = row.STATURDAY;
                                                rowReciever.WEDNESDAYPATIENT = row.STATURDAYPATIENT;
                                                rowReciever.WEDNESDAYREMARK = row.STATURDAYREMARK;
                                                rowReciever.WEDNESDAYHEMOID = row.STATURDAYHEMOID;
                                                rowReciever.WEDNESDAY_SCHEDULE_ID = row.STATURDAY_SCHEDULE_ID;
                                                rowReciever.WEDNESDAY_PURIFIER_ID = row.STATURDAY_PURIFIER_ID;
                                                rowReciever.WEDNESDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.STATURDAY = string.Empty;
                                                row.STATURDAYPATIENT = string.Empty;
                                                row.STATURDAYREMARK = string.Empty;
                                                row.STATURDAYHEMOID = string.Empty;
                                                row.STATURDAY_SCHEDULE_ID = string.Empty;
                                                row.STATURDAY_PURIFIER_ID = string.Empty;
                                                row.STATURDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridSunday":
                                        {
                                            if (!rowReciever.IsWEDNESDAYNull() && !string.IsNullOrEmpty(rowReciever.WEDNESDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.WEDNESDAY;
                                                    rowReciever.WEDNESDAY = row.SUNDAY;
                                                    row.SUNDAY = tranStr;

                                                    tranStr = rowReciever.WEDNESDAYPATIENT;
                                                    rowReciever.WEDNESDAYPATIENT = row.SUNDAYPATIENT;
                                                    row.SUNDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.WEDNESDAYREMARK;
                                                    rowReciever.WEDNESDAYREMARK = row.SUNDAYREMARK;
                                                    row.SUNDAYREMARK = tranStr;

                                                    tranStr = rowReciever.WEDNESDAYHEMOID;
                                                    rowReciever.WEDNESDAYHEMOID = row.SUNDAYHEMOID;
                                                    row.SUNDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.WEDNESDAY_SCHEDULE_ID;
                                                    rowReciever.WEDNESDAY_SCHEDULE_ID = row.SUNDAY_SCHEDULE_ID;
                                                    row.SUNDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.WEDNESDAY_PURIFIER_ID;
                                                    rowReciever.WEDNESDAY_PURIFIER_ID = row.SUNDAY_PURIFIER_ID;
                                                    row.SUNDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.WEDNESDAY = row.SUNDAY;
                                                rowReciever.WEDNESDAYPATIENT = row.SUNDAYPATIENT;
                                                rowReciever.WEDNESDAYREMARK = row.SUNDAYREMARK;
                                                rowReciever.WEDNESDAYHEMOID = row.SUNDAYHEMOID;
                                                rowReciever.WEDNESDAY_SCHEDULE_ID = row.SUNDAY_SCHEDULE_ID;
                                                rowReciever.WEDNESDAY_PURIFIER_ID = row.SUNDAY_PURIFIER_ID;
                                                rowReciever.WEDNESDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.SUNDAY = string.Empty;
                                                row.SUNDAYPATIENT = string.Empty;
                                                row.SUNDAYREMARK = string.Empty;
                                                row.SUNDAYHEMOID = string.Empty;
                                                row.SUNDAY_SCHEDULE_ID = string.Empty;
                                                row.SUNDAY_PURIFIER_ID = string.Empty;
                                                row.SUNDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                }
                                break;
                            }
                        case "gridThursday":
                            {
                                switch (chandle)
                                {
                                    case "gridMonday":
                                        {
                                            if (!rowReciever.IsTHURSDAYNull() && !string.IsNullOrEmpty(rowReciever.THURSDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.THURSDAY;
                                                    rowReciever.THURSDAY = row.MONDAY;
                                                    row.MONDAY = tranStr;

                                                    tranStr = rowReciever.THURSDAYPATIENT;
                                                    rowReciever.THURSDAYPATIENT = row.MONDAYPATIENT;
                                                    row.MONDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.THURSDAYREMARK;
                                                    rowReciever.THURSDAYREMARK = row.MONDAYREMARK;
                                                    row.MONDAYREMARK = tranStr;

                                                    tranStr = rowReciever.THURSDAYHEMOID;
                                                    rowReciever.THURSDAYHEMOID = row.MONDAYHEMOID;
                                                    row.MONDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.THURSDAY_SCHEDULE_ID;
                                                    rowReciever.THURSDAY_SCHEDULE_ID = row.MONDAY_SCHEDULE_ID;
                                                    row.MONDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.THURSDAY_PURIFIER_ID;
                                                    rowReciever.THURSDAY_PURIFIER_ID = row.MONDAY_PURIFIER_ID;
                                                    row.MONDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.THURSDAY = row.MONDAY;
                                                rowReciever.THURSDAYPATIENT = row.MONDAYPATIENT;
                                                rowReciever.THURSDAYREMARK = row.MONDAYREMARK;
                                                rowReciever.THURSDAYHEMOID = row.MONDAYHEMOID;
                                                rowReciever.THURSDAY_SCHEDULE_ID = row.MONDAY_SCHEDULE_ID;
                                                rowReciever.THURSDAY_PURIFIER_ID = row.MONDAY_PURIFIER_ID;
                                                rowReciever.THURSDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.MONDAY = string.Empty;
                                                row.MONDAYPATIENT = string.Empty;
                                                row.MONDAYREMARK = string.Empty;
                                                row.MONDAYHEMOID = string.Empty;
                                                row.MONDAY_SCHEDULE_ID = string.Empty;
                                                row.MONDAY_PURIFIER_ID = string.Empty;
                                                row.MONDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridTuesday":
                                        {
                                            if (!rowReciever.IsTHURSDAYNull() && !string.IsNullOrEmpty(rowReciever.THURSDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.THURSDAY;
                                                    rowReciever.THURSDAY = row.TUESDAY;
                                                    row.TUESDAY = tranStr;

                                                    tranStr = rowReciever.THURSDAYPATIENT;
                                                    rowReciever.THURSDAYPATIENT = row.TUESDAYPATIENT;
                                                    row.TUESDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.THURSDAYREMARK;
                                                    rowReciever.THURSDAYREMARK = row.TUESDAYREMARK;
                                                    row.TUESDAYREMARK = tranStr;

                                                    tranStr = rowReciever.THURSDAYHEMOID;
                                                    rowReciever.THURSDAYHEMOID = row.TUESDAYHEMOID;
                                                    row.TUESDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.THURSDAY_SCHEDULE_ID;
                                                    rowReciever.THURSDAY_SCHEDULE_ID = row.TUESDAY_SCHEDULE_ID;
                                                    row.TUESDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.THURSDAY_PURIFIER_ID;
                                                    rowReciever.THURSDAY_PURIFIER_ID = row.TUESDAY_PURIFIER_ID;
                                                    row.TUESDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.THURSDAY = row.TUESDAY;
                                                rowReciever.THURSDAYPATIENT = row.TUESDAYPATIENT;
                                                rowReciever.THURSDAYREMARK = row.TUESDAYREMARK;
                                                rowReciever.THURSDAYHEMOID = row.TUESDAYHEMOID;
                                                rowReciever.THURSDAY_SCHEDULE_ID = row.TUESDAY_SCHEDULE_ID;
                                                rowReciever.THURSDAY_PURIFIER_ID = row.TUESDAY_PURIFIER_ID;
                                                rowReciever.THURSDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.TUESDAY = string.Empty;
                                                row.TUESDAYPATIENT = string.Empty;
                                                row.TUESDAYREMARK = string.Empty;
                                                row.TUESDAYHEMOID = string.Empty;
                                                row.TUESDAY_SCHEDULE_ID = string.Empty;
                                                row.TUESDAY_PURIFIER_ID = string.Empty;
                                                row.TUESDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridWednesday":
                                        {
                                            if (!rowReciever.IsTHURSDAYNull() && !string.IsNullOrEmpty(rowReciever.THURSDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.THURSDAY;
                                                    rowReciever.THURSDAY = row.WEDNESDAY;
                                                    row.WEDNESDAY = tranStr;

                                                    tranStr = rowReciever.THURSDAYPATIENT;
                                                    rowReciever.THURSDAYPATIENT = row.WEDNESDAYPATIENT;
                                                    row.WEDNESDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.THURSDAYREMARK;
                                                    rowReciever.THURSDAYREMARK = row.WEDNESDAYREMARK;
                                                    row.WEDNESDAYREMARK = tranStr;

                                                    tranStr = rowReciever.THURSDAYHEMOID;
                                                    rowReciever.THURSDAYHEMOID = row.WEDNESDAYHEMOID;
                                                    row.WEDNESDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.THURSDAY_SCHEDULE_ID;
                                                    rowReciever.THURSDAY_SCHEDULE_ID = row.WEDNESDAY_SCHEDULE_ID;
                                                    row.WEDNESDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.THURSDAY_PURIFIER_ID;
                                                    rowReciever.THURSDAY_PURIFIER_ID = row.WEDNESDAY_PURIFIER_ID;
                                                    row.WEDNESDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.THURSDAY = row.WEDNESDAY;
                                                rowReciever.THURSDAYPATIENT = row.WEDNESDAYPATIENT;
                                                rowReciever.THURSDAYREMARK = row.WEDNESDAYREMARK;
                                                rowReciever.THURSDAYHEMOID = row.WEDNESDAYHEMOID;
                                                rowReciever.THURSDAY_SCHEDULE_ID = row.WEDNESDAY_SCHEDULE_ID;
                                                rowReciever.THURSDAY_PURIFIER_ID = row.WEDNESDAY_PURIFIER_ID;
                                                rowReciever.THURSDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.WEDNESDAY = string.Empty;
                                                row.WEDNESDAYPATIENT = string.Empty;
                                                row.WEDNESDAYREMARK = string.Empty;
                                                row.WEDNESDAYHEMOID = string.Empty;
                                                row.WEDNESDAY_SCHEDULE_ID = string.Empty;
                                                row.WEDNESDAY_PURIFIER_ID = string.Empty;
                                                row.WEDNESDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridThursday":
                                        {
                                            if (!rowReciever.IsTHURSDAYNull() && !string.IsNullOrEmpty(rowReciever.THURSDAY) && !rowReciever.THURSDAYHEMOID.Equals(row.THURSDAYHEMOID))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.THURSDAY;
                                                    rowReciever.THURSDAY = row.THURSDAY;
                                                    row.THURSDAY = tranStr;

                                                    tranStr = rowReciever.THURSDAYPATIENT;
                                                    rowReciever.THURSDAYPATIENT = row.THURSDAYPATIENT;
                                                    row.THURSDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.THURSDAYREMARK;
                                                    rowReciever.THURSDAYREMARK = row.THURSDAYREMARK;
                                                    row.THURSDAYREMARK = tranStr;

                                                    tranStr = rowReciever.THURSDAYHEMOID;
                                                    rowReciever.THURSDAYHEMOID = row.THURSDAYHEMOID;
                                                    row.THURSDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.THURSDAY_SCHEDULE_ID;
                                                    rowReciever.THURSDAY_SCHEDULE_ID = row.THURSDAY_SCHEDULE_ID;
                                                    row.THURSDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.THURSDAY_PURIFIER_ID;
                                                    rowReciever.THURSDAY_PURIFIER_ID = row.THURSDAY_PURIFIER_ID;
                                                    row.THURSDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.THURSDAY = row.THURSDAY;
                                                rowReciever.THURSDAYPATIENT = row.THURSDAYPATIENT;
                                                rowReciever.THURSDAYREMARK = row.THURSDAYREMARK;
                                                rowReciever.THURSDAYHEMOID = row.THURSDAYHEMOID;
                                                rowReciever.THURSDAY_SCHEDULE_ID = row.THURSDAY_SCHEDULE_ID;
                                                rowReciever.THURSDAY_PURIFIER_ID = row.THURSDAY_PURIFIER_ID;
                                                rowReciever.THURSDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.THURSDAY = string.Empty;
                                                row.THURSDAYPATIENT = string.Empty;
                                                row.THURSDAYREMARK = string.Empty;
                                                row.THURSDAYHEMOID = string.Empty;
                                                row.THURSDAY_SCHEDULE_ID = string.Empty;
                                                row.THURSDAY_PURIFIER_ID = string.Empty;
                                                row.THURSDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridFriday":
                                        {
                                            if (!rowReciever.IsTHURSDAYNull() && !string.IsNullOrEmpty(rowReciever.THURSDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.THURSDAY;
                                                    rowReciever.THURSDAY = row.FRIDAY;
                                                    row.FRIDAY = tranStr;

                                                    tranStr = rowReciever.THURSDAYPATIENT;
                                                    rowReciever.THURSDAYPATIENT = row.FRIDAYPATIENT;
                                                    row.FRIDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.THURSDAYREMARK;
                                                    rowReciever.THURSDAYREMARK = row.FRIDAYREMARK;
                                                    row.FRIDAYREMARK = tranStr;

                                                    tranStr = rowReciever.THURSDAYHEMOID;
                                                    rowReciever.THURSDAYHEMOID = row.FRIDAYHEMOID;
                                                    row.FRIDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.THURSDAY_SCHEDULE_ID;
                                                    rowReciever.THURSDAY_SCHEDULE_ID = row.FRIDAY_SCHEDULE_ID;
                                                    row.FRIDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.THURSDAY_PURIFIER_ID;
                                                    rowReciever.THURSDAY_PURIFIER_ID = row.FRIDAY_PURIFIER_ID;
                                                    row.FRIDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.THURSDAY = row.FRIDAY;
                                                rowReciever.THURSDAYPATIENT = row.FRIDAYPATIENT;
                                                rowReciever.THURSDAYREMARK = row.FRIDAYREMARK;
                                                rowReciever.THURSDAYHEMOID = row.FRIDAYHEMOID;
                                                rowReciever.THURSDAY_SCHEDULE_ID = row.FRIDAY_SCHEDULE_ID;
                                                rowReciever.THURSDAY_PURIFIER_ID = row.FRIDAY_PURIFIER_ID;
                                                rowReciever.THURSDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.FRIDAY = string.Empty;
                                                row.FRIDAYPATIENT = string.Empty;
                                                row.FRIDAYREMARK = string.Empty;
                                                row.FRIDAYHEMOID = string.Empty;
                                                row.FRIDAY_SCHEDULE_ID = string.Empty;
                                                row.FRIDAY_PURIFIER_ID = string.Empty;
                                                row.FRIDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridSaturday":
                                        {
                                            if (!rowReciever.IsTHURSDAYNull() && !string.IsNullOrEmpty(rowReciever.THURSDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.THURSDAY;
                                                    rowReciever.THURSDAY = row.STATURDAY;
                                                    row.STATURDAY = tranStr;

                                                    tranStr = rowReciever.THURSDAYPATIENT;
                                                    rowReciever.THURSDAYPATIENT = row.STATURDAYPATIENT;
                                                    row.STATURDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.THURSDAYREMARK;
                                                    rowReciever.THURSDAYREMARK = row.STATURDAYREMARK;
                                                    row.STATURDAYREMARK = tranStr;

                                                    tranStr = rowReciever.THURSDAYHEMOID;
                                                    rowReciever.THURSDAYHEMOID = row.STATURDAYHEMOID;
                                                    row.STATURDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.THURSDAY_SCHEDULE_ID;
                                                    rowReciever.THURSDAY_SCHEDULE_ID = row.STATURDAY_SCHEDULE_ID;
                                                    row.STATURDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.THURSDAY_PURIFIER_ID;
                                                    rowReciever.THURSDAY_PURIFIER_ID = row.STATURDAY_PURIFIER_ID;
                                                    row.STATURDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.THURSDAY = row.STATURDAY;
                                                rowReciever.THURSDAYPATIENT = row.STATURDAYPATIENT;
                                                rowReciever.THURSDAYREMARK = row.STATURDAYREMARK;
                                                rowReciever.THURSDAYHEMOID = row.STATURDAYHEMOID;
                                                rowReciever.THURSDAY_SCHEDULE_ID = row.STATURDAY_SCHEDULE_ID;
                                                rowReciever.THURSDAY_PURIFIER_ID = row.STATURDAY_PURIFIER_ID;
                                                rowReciever.THURSDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.STATURDAY = string.Empty;
                                                row.STATURDAYPATIENT = string.Empty;
                                                row.STATURDAYREMARK = string.Empty;
                                                row.STATURDAYHEMOID = string.Empty;
                                                row.STATURDAY_SCHEDULE_ID = string.Empty;
                                                row.STATURDAY_PURIFIER_ID = string.Empty;
                                                row.STATURDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridSunday":
                                        {
                                            if (!rowReciever.IsTHURSDAYNull() && !string.IsNullOrEmpty(rowReciever.THURSDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.THURSDAY;
                                                    rowReciever.THURSDAY = row.SUNDAY;
                                                    row.SUNDAY = tranStr;

                                                    tranStr = rowReciever.THURSDAYPATIENT;
                                                    rowReciever.THURSDAYPATIENT = row.SUNDAYPATIENT;
                                                    row.SUNDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.THURSDAYREMARK;
                                                    rowReciever.THURSDAYREMARK = row.SUNDAYREMARK;
                                                    row.SUNDAYREMARK = tranStr;

                                                    tranStr = rowReciever.THURSDAYHEMOID;
                                                    rowReciever.THURSDAYHEMOID = row.SUNDAYHEMOID;
                                                    row.SUNDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.THURSDAY_SCHEDULE_ID;
                                                    rowReciever.THURSDAY_SCHEDULE_ID = row.SUNDAY_SCHEDULE_ID;
                                                    row.SUNDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.THURSDAY_PURIFIER_ID;
                                                    rowReciever.THURSDAY_PURIFIER_ID = row.SUNDAY_PURIFIER_ID;
                                                    row.SUNDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.THURSDAY = row.SUNDAY;
                                                rowReciever.THURSDAYPATIENT = row.SUNDAYPATIENT;
                                                rowReciever.THURSDAYREMARK = row.SUNDAYREMARK;
                                                rowReciever.THURSDAYHEMOID = row.SUNDAYHEMOID;
                                                rowReciever.THURSDAY_SCHEDULE_ID = row.SUNDAY_SCHEDULE_ID;
                                                rowReciever.THURSDAY_PURIFIER_ID = row.SUNDAY_PURIFIER_ID;
                                                rowReciever.THURSDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.SUNDAY = string.Empty;
                                                row.SUNDAYPATIENT = string.Empty;
                                                row.SUNDAYREMARK = string.Empty;
                                                row.SUNDAYHEMOID = string.Empty;
                                                row.SUNDAY_SCHEDULE_ID = string.Empty;
                                                row.SUNDAY_PURIFIER_ID = string.Empty;
                                                row.SUNDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                }
                                break;
                            }
                        case "gridFriday":
                            {
                                switch (chandle)
                                {
                                    case "gridMonday":
                                        {
                                            if (!rowReciever.IsFRIDAYNull() && !string.IsNullOrEmpty(rowReciever.FRIDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.FRIDAY;
                                                    rowReciever.FRIDAY = row.MONDAY;
                                                    row.MONDAY = tranStr;

                                                    tranStr = rowReciever.FRIDAYPATIENT;
                                                    rowReciever.FRIDAYPATIENT = row.MONDAYPATIENT;
                                                    row.MONDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.FRIDAYREMARK;
                                                    rowReciever.FRIDAYREMARK = row.MONDAYREMARK;
                                                    row.MONDAYREMARK = tranStr;

                                                    tranStr = rowReciever.FRIDAYHEMOID;
                                                    rowReciever.FRIDAYHEMOID = row.MONDAYHEMOID;
                                                    row.MONDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.FRIDAY_SCHEDULE_ID;
                                                    rowReciever.FRIDAY_SCHEDULE_ID = row.MONDAY_SCHEDULE_ID;
                                                    row.MONDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.FRIDAY_PURIFIER_ID;
                                                    rowReciever.FRIDAY_PURIFIER_ID = row.MONDAY_PURIFIER_ID;
                                                    row.MONDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.FRIDAY = row.MONDAY;
                                                rowReciever.FRIDAYPATIENT = row.MONDAYPATIENT;
                                                rowReciever.FRIDAYREMARK = row.MONDAYREMARK;
                                                rowReciever.FRIDAYHEMOID = row.MONDAYHEMOID;
                                                rowReciever.FRIDAY_SCHEDULE_ID = row.MONDAY_SCHEDULE_ID;
                                                rowReciever.FRIDAY_PURIFIER_ID = row.MONDAY_PURIFIER_ID;
                                                rowReciever.FRIDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.MONDAY = string.Empty;
                                                row.MONDAYPATIENT = string.Empty;
                                                row.MONDAYREMARK = string.Empty;
                                                row.MONDAYHEMOID = string.Empty;
                                                row.MONDAY_SCHEDULE_ID = string.Empty;
                                                row.MONDAY_PURIFIER_ID = string.Empty;
                                                row.MONDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridTuesday":
                                        {
                                            if (!rowReciever.IsFRIDAYNull() && !string.IsNullOrEmpty(rowReciever.FRIDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.FRIDAY;
                                                    rowReciever.FRIDAY = row.TUESDAY;
                                                    row.TUESDAY = tranStr;

                                                    tranStr = rowReciever.FRIDAYPATIENT;
                                                    rowReciever.FRIDAYPATIENT = row.TUESDAYPATIENT;
                                                    row.TUESDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.FRIDAYREMARK;
                                                    rowReciever.FRIDAYREMARK = row.TUESDAYREMARK;
                                                    row.TUESDAYREMARK = tranStr;

                                                    tranStr = rowReciever.FRIDAYHEMOID;
                                                    rowReciever.FRIDAYHEMOID = row.TUESDAYHEMOID;
                                                    row.TUESDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.FRIDAY_SCHEDULE_ID;
                                                    rowReciever.FRIDAY_SCHEDULE_ID = row.TUESDAY_SCHEDULE_ID;
                                                    row.TUESDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.FRIDAY_PURIFIER_ID;
                                                    rowReciever.FRIDAY_PURIFIER_ID = row.TUESDAY_PURIFIER_ID;
                                                    row.TUESDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.FRIDAY = row.TUESDAY;
                                                rowReciever.FRIDAYPATIENT = row.TUESDAYPATIENT;
                                                rowReciever.FRIDAYREMARK = row.TUESDAYREMARK;
                                                rowReciever.FRIDAYHEMOID = row.TUESDAYHEMOID;
                                                rowReciever.FRIDAY_SCHEDULE_ID = row.TUESDAY_SCHEDULE_ID;
                                                rowReciever.FRIDAY_PURIFIER_ID = row.TUESDAY_PURIFIER_ID;
                                                rowReciever.FRIDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.TUESDAY = string.Empty;
                                                row.TUESDAYPATIENT = string.Empty;
                                                row.TUESDAYREMARK = string.Empty;
                                                row.TUESDAYHEMOID = string.Empty;
                                                row.TUESDAY_SCHEDULE_ID = string.Empty;
                                                row.TUESDAY_PURIFIER_ID = string.Empty;
                                                row.TUESDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridWednesday":
                                        {
                                            if (!rowReciever.IsFRIDAYNull() && !string.IsNullOrEmpty(rowReciever.FRIDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.FRIDAY;
                                                    rowReciever.FRIDAY = row.WEDNESDAY;
                                                    row.WEDNESDAY = tranStr;

                                                    tranStr = rowReciever.FRIDAYPATIENT;
                                                    rowReciever.FRIDAYPATIENT = row.WEDNESDAYPATIENT;
                                                    row.WEDNESDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.FRIDAYREMARK;
                                                    rowReciever.FRIDAYREMARK = row.WEDNESDAYREMARK;
                                                    row.WEDNESDAYREMARK = tranStr;

                                                    tranStr = rowReciever.FRIDAYHEMOID;
                                                    rowReciever.FRIDAYHEMOID = row.WEDNESDAYHEMOID;
                                                    row.WEDNESDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.FRIDAY_SCHEDULE_ID;
                                                    rowReciever.FRIDAY_SCHEDULE_ID = row.WEDNESDAY_SCHEDULE_ID;
                                                    row.WEDNESDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.FRIDAY_PURIFIER_ID;
                                                    rowReciever.FRIDAY_PURIFIER_ID = row.WEDNESDAY_PURIFIER_ID;
                                                    row.WEDNESDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.FRIDAY = row.WEDNESDAY;
                                                rowReciever.FRIDAYPATIENT = row.WEDNESDAYPATIENT;
                                                rowReciever.FRIDAYREMARK = row.WEDNESDAYREMARK;
                                                rowReciever.FRIDAYHEMOID = row.WEDNESDAYHEMOID;
                                                rowReciever.FRIDAY_SCHEDULE_ID = row.WEDNESDAY_SCHEDULE_ID;
                                                rowReciever.FRIDAY_PURIFIER_ID = row.WEDNESDAY_PURIFIER_ID;
                                                rowReciever.FRIDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.WEDNESDAY = string.Empty;
                                                row.WEDNESDAYPATIENT = string.Empty;
                                                row.WEDNESDAYREMARK = string.Empty;
                                                row.WEDNESDAYHEMOID = string.Empty;
                                                row.WEDNESDAY_SCHEDULE_ID = string.Empty;
                                                row.WEDNESDAY_PURIFIER_ID = string.Empty;
                                                row.WEDNESDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridThursday":
                                        {
                                            if (!rowReciever.IsFRIDAYNull() && !string.IsNullOrEmpty(rowReciever.FRIDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.FRIDAY;
                                                    rowReciever.FRIDAY = row.THURSDAY;
                                                    row.THURSDAY = tranStr;

                                                    tranStr = rowReciever.FRIDAYPATIENT;
                                                    rowReciever.FRIDAYPATIENT = row.THURSDAYPATIENT;
                                                    row.THURSDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.FRIDAYREMARK;
                                                    rowReciever.FRIDAYREMARK = row.THURSDAYREMARK;
                                                    row.THURSDAYREMARK = tranStr;

                                                    tranStr = rowReciever.FRIDAYHEMOID;
                                                    rowReciever.FRIDAYHEMOID = row.THURSDAYHEMOID;
                                                    row.THURSDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.FRIDAY_SCHEDULE_ID;
                                                    rowReciever.FRIDAY_SCHEDULE_ID = row.THURSDAY_SCHEDULE_ID;
                                                    row.THURSDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.FRIDAY_PURIFIER_ID;
                                                    rowReciever.FRIDAY_PURIFIER_ID = row.THURSDAY_PURIFIER_ID;
                                                    row.THURSDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.FRIDAY = row.THURSDAY;
                                                rowReciever.FRIDAYPATIENT = row.THURSDAYPATIENT;
                                                rowReciever.FRIDAYREMARK = row.THURSDAYREMARK;
                                                rowReciever.FRIDAYHEMOID = row.THURSDAYHEMOID;
                                                rowReciever.FRIDAY_SCHEDULE_ID = row.THURSDAY_SCHEDULE_ID;
                                                rowReciever.FRIDAY_PURIFIER_ID = row.THURSDAY_PURIFIER_ID;
                                                rowReciever.FRIDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.THURSDAY = string.Empty;
                                                row.THURSDAYPATIENT = string.Empty;
                                                row.THURSDAYREMARK = string.Empty;
                                                row.THURSDAYHEMOID = string.Empty;
                                                row.THURSDAY_SCHEDULE_ID = string.Empty;
                                                row.THURSDAY_PURIFIER_ID = string.Empty;
                                                row.THURSDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridFriday":
                                        {
                                            if (!rowReciever.IsFRIDAYNull() && !string.IsNullOrEmpty(rowReciever.FRIDAY) && !rowReciever.FRIDAYHEMOID.Equals(row.FRIDAYHEMOID))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.FRIDAY;
                                                    rowReciever.FRIDAY = row.FRIDAY;
                                                    row.FRIDAY = tranStr;

                                                    tranStr = rowReciever.FRIDAYPATIENT;
                                                    rowReciever.FRIDAYPATIENT = row.FRIDAYPATIENT;
                                                    row.FRIDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.FRIDAYREMARK;
                                                    rowReciever.FRIDAYREMARK = row.FRIDAYREMARK;
                                                    row.FRIDAYREMARK = tranStr;

                                                    tranStr = rowReciever.FRIDAYHEMOID;
                                                    rowReciever.FRIDAYHEMOID = row.FRIDAYHEMOID;
                                                    row.FRIDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.FRIDAY_SCHEDULE_ID;
                                                    rowReciever.FRIDAY_SCHEDULE_ID = row.FRIDAY_SCHEDULE_ID;
                                                    row.FRIDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.FRIDAY_PURIFIER_ID;
                                                    rowReciever.FRIDAY_PURIFIER_ID = row.FRIDAY_PURIFIER_ID;
                                                    row.FRIDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.FRIDAY = row.FRIDAY;
                                                rowReciever.FRIDAYPATIENT = row.FRIDAYPATIENT;
                                                rowReciever.FRIDAYREMARK = row.FRIDAYREMARK;
                                                rowReciever.FRIDAYHEMOID = row.FRIDAYHEMOID;
                                                rowReciever.FRIDAY_SCHEDULE_ID = row.FRIDAY_SCHEDULE_ID;
                                                rowReciever.FRIDAY_PURIFIER_ID = row.FRIDAY_PURIFIER_ID;
                                                rowReciever.FRIDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.FRIDAY = string.Empty;
                                                row.FRIDAYPATIENT = string.Empty;
                                                row.FRIDAYREMARK = string.Empty;
                                                row.FRIDAYHEMOID = string.Empty;
                                                row.FRIDAY_SCHEDULE_ID = string.Empty;
                                                row.FRIDAY_PURIFIER_ID = string.Empty;
                                                row.FRIDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridSaturday":
                                        {
                                            if (!rowReciever.IsFRIDAYNull() && !string.IsNullOrEmpty(rowReciever.FRIDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.FRIDAY;
                                                    rowReciever.FRIDAY = row.STATURDAY;
                                                    row.STATURDAY = tranStr;

                                                    tranStr = rowReciever.FRIDAYPATIENT;
                                                    rowReciever.FRIDAYPATIENT = row.STATURDAYPATIENT;
                                                    row.STATURDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.FRIDAYREMARK;
                                                    rowReciever.FRIDAYREMARK = row.STATURDAYREMARK;
                                                    row.STATURDAYREMARK = tranStr;

                                                    tranStr = rowReciever.FRIDAYHEMOID;
                                                    rowReciever.FRIDAYHEMOID = row.STATURDAYHEMOID;
                                                    row.STATURDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.FRIDAY_SCHEDULE_ID;
                                                    rowReciever.FRIDAY_SCHEDULE_ID = row.STATURDAY_SCHEDULE_ID;
                                                    row.STATURDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.FRIDAY_PURIFIER_ID;
                                                    rowReciever.FRIDAY_PURIFIER_ID = row.STATURDAY_PURIFIER_ID;
                                                    row.STATURDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.FRIDAY = row.STATURDAY;
                                                rowReciever.FRIDAYPATIENT = row.STATURDAYPATIENT;
                                                rowReciever.FRIDAYREMARK = row.STATURDAYREMARK;
                                                rowReciever.FRIDAYHEMOID = row.STATURDAYHEMOID;
                                                rowReciever.FRIDAY_SCHEDULE_ID = row.STATURDAY_SCHEDULE_ID;
                                                rowReciever.FRIDAY_PURIFIER_ID = row.STATURDAY_PURIFIER_ID;
                                                rowReciever.FRIDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.STATURDAY = string.Empty;
                                                row.STATURDAYPATIENT = string.Empty;
                                                row.STATURDAYREMARK = string.Empty;
                                                row.STATURDAYHEMOID = string.Empty;
                                                row.STATURDAY_SCHEDULE_ID = string.Empty;
                                                row.STATURDAY_PURIFIER_ID = string.Empty;
                                                row.STATURDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridSunday":
                                        {
                                            if (!rowReciever.IsFRIDAYNull() && !string.IsNullOrEmpty(rowReciever.FRIDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.FRIDAY;
                                                    rowReciever.FRIDAY = row.SUNDAY;
                                                    row.SUNDAY = tranStr;

                                                    tranStr = rowReciever.FRIDAYPATIENT;
                                                    rowReciever.FRIDAYPATIENT = row.SUNDAYPATIENT;
                                                    row.SUNDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.FRIDAYREMARK;
                                                    rowReciever.FRIDAYREMARK = row.SUNDAYREMARK;
                                                    row.SUNDAYREMARK = tranStr;

                                                    tranStr = rowReciever.FRIDAYHEMOID;
                                                    rowReciever.FRIDAYHEMOID = row.SUNDAYHEMOID;
                                                    row.SUNDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.FRIDAY_SCHEDULE_ID;
                                                    rowReciever.FRIDAY_SCHEDULE_ID = row.SUNDAY_SCHEDULE_ID;
                                                    row.SUNDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.FRIDAY_PURIFIER_ID;
                                                    rowReciever.FRIDAY_PURIFIER_ID = row.SUNDAY_PURIFIER_ID;
                                                    row.SUNDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.FRIDAY = row.SUNDAY;
                                                rowReciever.FRIDAYPATIENT = row.SUNDAYPATIENT;
                                                rowReciever.FRIDAYREMARK = row.SUNDAYREMARK;
                                                rowReciever.FRIDAYHEMOID = row.SUNDAYHEMOID;
                                                rowReciever.FRIDAY_SCHEDULE_ID = row.SUNDAY_SCHEDULE_ID;
                                                rowReciever.FRIDAY_PURIFIER_ID = row.SUNDAY_PURIFIER_ID;
                                                rowReciever.FRIDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.SUNDAY = string.Empty;
                                                row.SUNDAYPATIENT = string.Empty;
                                                row.SUNDAYREMARK = string.Empty;
                                                row.SUNDAYHEMOID = string.Empty;
                                                row.SUNDAY_SCHEDULE_ID = string.Empty;
                                                row.SUNDAY_PURIFIER_ID = string.Empty;
                                                row.SUNDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                }
                                break;
                            }
                        case "gridSaturday":
                            {
                                switch (chandle)
                                {
                                    case "gridMonday":
                                        {
                                            if (!rowReciever.IsSTATURDAYNull() && !string.IsNullOrEmpty(rowReciever.STATURDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.STATURDAY;
                                                    rowReciever.STATURDAY = row.MONDAY;
                                                    row.MONDAY = tranStr;

                                                    tranStr = rowReciever.STATURDAYPATIENT;
                                                    rowReciever.STATURDAYPATIENT = row.MONDAYPATIENT;
                                                    row.MONDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.STATURDAYREMARK;
                                                    rowReciever.STATURDAYREMARK = row.MONDAYREMARK;
                                                    row.MONDAYREMARK = tranStr;

                                                    tranStr = rowReciever.STATURDAYHEMOID;
                                                    rowReciever.STATURDAYHEMOID = row.MONDAYHEMOID;
                                                    row.MONDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.STATURDAY_SCHEDULE_ID;
                                                    rowReciever.STATURDAY_SCHEDULE_ID = row.MONDAY_SCHEDULE_ID;
                                                    row.MONDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.STATURDAY_PURIFIER_ID;
                                                    rowReciever.STATURDAY_PURIFIER_ID = row.MONDAY_PURIFIER_ID;
                                                    row.MONDAY_PURIFIER_ID = tranStr;

                                                }
                                            }
                                            else
                                            {
                                                rowReciever.STATURDAY = row.MONDAY;
                                                rowReciever.STATURDAYPATIENT = row.MONDAYPATIENT;
                                                rowReciever.STATURDAYREMARK = row.MONDAYREMARK;
                                                rowReciever.STATURDAYHEMOID = row.MONDAYHEMOID;
                                                rowReciever.STATURDAY_SCHEDULE_ID = row.MONDAY_SCHEDULE_ID;
                                                rowReciever.STATURDAY_PURIFIER_ID = row.MONDAY_PURIFIER_ID;
                                                rowReciever.STATURDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.MONDAY = string.Empty;
                                                row.MONDAYPATIENT = string.Empty;
                                                row.MONDAYREMARK = string.Empty;
                                                row.MONDAYHEMOID = string.Empty;
                                                row.MONDAY_SCHEDULE_ID = string.Empty;
                                                row.MONDAY_PURIFIER_ID = string.Empty;
                                                row.MONDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridTuesday":
                                        {
                                            if (!rowReciever.IsSTATURDAYNull() && !string.IsNullOrEmpty(rowReciever.STATURDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.STATURDAY;
                                                    rowReciever.STATURDAY = row.TUESDAY;
                                                    row.TUESDAY = tranStr;

                                                    tranStr = rowReciever.STATURDAYPATIENT;
                                                    rowReciever.STATURDAYPATIENT = row.TUESDAYPATIENT;
                                                    row.TUESDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.STATURDAYREMARK;
                                                    rowReciever.STATURDAYREMARK = row.TUESDAYREMARK;
                                                    row.TUESDAYREMARK = tranStr;

                                                    tranStr = rowReciever.STATURDAYHEMOID;
                                                    rowReciever.STATURDAYHEMOID = row.TUESDAYHEMOID;
                                                    row.TUESDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.STATURDAY_SCHEDULE_ID;
                                                    rowReciever.STATURDAY_SCHEDULE_ID = row.TUESDAY_SCHEDULE_ID;
                                                    row.TUESDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.STATURDAY_PURIFIER_ID;
                                                    rowReciever.STATURDAY_PURIFIER_ID = row.TUESDAY_PURIFIER_ID;
                                                    row.TUESDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.STATURDAY = row.TUESDAY;
                                                rowReciever.STATURDAYPATIENT = row.TUESDAYPATIENT;
                                                rowReciever.STATURDAYREMARK = row.TUESDAYREMARK;
                                                rowReciever.STATURDAYHEMOID = row.TUESDAYHEMOID;
                                                rowReciever.STATURDAY_SCHEDULE_ID = row.TUESDAY_SCHEDULE_ID;
                                                rowReciever.STATURDAY_PURIFIER_ID = row.TUESDAY_PURIFIER_ID;
                                                rowReciever.STATURDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.TUESDAY = string.Empty;
                                                row.TUESDAYPATIENT = string.Empty;
                                                row.TUESDAYREMARK = string.Empty;
                                                row.TUESDAYHEMOID = string.Empty;
                                                row.TUESDAY_SCHEDULE_ID = string.Empty;
                                                row.TUESDAY_PURIFIER_ID = string.Empty;
                                                row.TUESDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridWednesday":
                                        {
                                            if (!rowReciever.IsSTATURDAYNull() && !string.IsNullOrEmpty(rowReciever.STATURDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.STATURDAY;
                                                    rowReciever.STATURDAY = row.WEDNESDAY;
                                                    row.WEDNESDAY = tranStr;

                                                    tranStr = rowReciever.STATURDAYPATIENT;
                                                    rowReciever.STATURDAYPATIENT = row.WEDNESDAYPATIENT;
                                                    row.WEDNESDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.STATURDAYREMARK;
                                                    rowReciever.STATURDAYREMARK = row.WEDNESDAYREMARK;
                                                    row.WEDNESDAYREMARK = tranStr;

                                                    tranStr = rowReciever.STATURDAYHEMOID;
                                                    rowReciever.STATURDAYHEMOID = row.WEDNESDAYHEMOID;
                                                    row.WEDNESDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.STATURDAY_SCHEDULE_ID;
                                                    rowReciever.STATURDAY_SCHEDULE_ID = row.WEDNESDAY_SCHEDULE_ID;
                                                    row.WEDNESDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.STATURDAY_PURIFIER_ID;
                                                    rowReciever.STATURDAY_PURIFIER_ID = row.WEDNESDAY_PURIFIER_ID;
                                                    row.WEDNESDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.STATURDAY = row.WEDNESDAY;
                                                rowReciever.STATURDAYPATIENT = row.WEDNESDAYPATIENT;
                                                rowReciever.STATURDAYREMARK = row.WEDNESDAYREMARK;
                                                rowReciever.STATURDAYHEMOID = row.WEDNESDAYHEMOID;
                                                rowReciever.STATURDAY_SCHEDULE_ID = row.WEDNESDAY_SCHEDULE_ID;
                                                rowReciever.STATURDAY_PURIFIER_ID = row.WEDNESDAY_PURIFIER_ID;
                                                rowReciever.STATURDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.WEDNESDAY = string.Empty;
                                                row.WEDNESDAYPATIENT = string.Empty;
                                                row.WEDNESDAYREMARK = string.Empty;
                                                row.WEDNESDAYHEMOID = string.Empty;
                                                row.WEDNESDAY_SCHEDULE_ID = string.Empty;
                                                row.WEDNESDAY_PURIFIER_ID = string.Empty;
                                                row.WEDNESDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridThursday":
                                        {
                                            if (!rowReciever.IsSTATURDAYNull() && !string.IsNullOrEmpty(rowReciever.STATURDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.STATURDAY;
                                                    rowReciever.STATURDAY = row.THURSDAY;
                                                    row.THURSDAY = tranStr;

                                                    tranStr = rowReciever.STATURDAYPATIENT;
                                                    rowReciever.STATURDAYPATIENT = row.THURSDAYPATIENT;
                                                    row.THURSDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.STATURDAYREMARK;
                                                    rowReciever.STATURDAYREMARK = row.THURSDAYREMARK;
                                                    row.THURSDAYREMARK = tranStr;

                                                    tranStr = rowReciever.STATURDAYHEMOID;
                                                    rowReciever.STATURDAYHEMOID = row.THURSDAYHEMOID;
                                                    row.THURSDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.STATURDAY_SCHEDULE_ID;
                                                    rowReciever.STATURDAY_SCHEDULE_ID = row.THURSDAY_SCHEDULE_ID;
                                                    row.THURSDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.STATURDAY_PURIFIER_ID;
                                                    rowReciever.STATURDAY_PURIFIER_ID = row.THURSDAY_PURIFIER_ID;
                                                    row.THURSDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.STATURDAY = row.THURSDAY;
                                                rowReciever.STATURDAYPATIENT = row.THURSDAYPATIENT;
                                                rowReciever.STATURDAYREMARK = row.THURSDAYREMARK;
                                                rowReciever.STATURDAYHEMOID = row.THURSDAYHEMOID;
                                                rowReciever.STATURDAY_SCHEDULE_ID = row.THURSDAY_SCHEDULE_ID;
                                                rowReciever.STATURDAY_PURIFIER_ID = row.THURSDAY_PURIFIER_ID;
                                                rowReciever.STATURDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.THURSDAY = string.Empty;
                                                row.THURSDAYPATIENT = string.Empty;
                                                row.THURSDAYREMARK = string.Empty;
                                                row.THURSDAYHEMOID = string.Empty;
                                                row.THURSDAY_SCHEDULE_ID = string.Empty;
                                                row.THURSDAY_PURIFIER_ID = string.Empty;
                                                row.THURSDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridFriday":
                                        {
                                            if (!rowReciever.IsSTATURDAYNull() && !string.IsNullOrEmpty(rowReciever.STATURDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.STATURDAY;
                                                    rowReciever.STATURDAY = row.FRIDAY;
                                                    row.FRIDAY = tranStr;

                                                    tranStr = rowReciever.STATURDAYPATIENT;
                                                    rowReciever.STATURDAYPATIENT = row.FRIDAYPATIENT;
                                                    row.FRIDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.STATURDAYREMARK;
                                                    rowReciever.STATURDAYREMARK = row.FRIDAYREMARK;
                                                    row.FRIDAYREMARK = tranStr;

                                                    tranStr = rowReciever.STATURDAYHEMOID;
                                                    rowReciever.STATURDAYHEMOID = row.FRIDAYHEMOID;
                                                    row.FRIDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.STATURDAY_SCHEDULE_ID;
                                                    rowReciever.STATURDAY_SCHEDULE_ID = row.FRIDAY_SCHEDULE_ID;
                                                    row.FRIDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.STATURDAY_PURIFIER_ID;
                                                    rowReciever.STATURDAY_PURIFIER_ID = row.FRIDAY_PURIFIER_ID;
                                                    row.FRIDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.STATURDAY = row.FRIDAY;
                                                rowReciever.STATURDAYPATIENT = row.FRIDAYPATIENT;
                                                rowReciever.STATURDAYREMARK = row.FRIDAYREMARK;
                                                rowReciever.STATURDAYHEMOID = row.FRIDAYHEMOID;
                                                rowReciever.STATURDAY_SCHEDULE_ID = row.FRIDAY_SCHEDULE_ID;
                                                rowReciever.STATURDAY_PURIFIER_ID = row.FRIDAY_PURIFIER_ID;
                                                rowReciever.STATURDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.FRIDAY = string.Empty;
                                                row.FRIDAYPATIENT = string.Empty;
                                                row.FRIDAYREMARK = string.Empty;
                                                row.FRIDAYHEMOID = string.Empty;
                                                row.FRIDAY_SCHEDULE_ID = string.Empty;
                                                row.FRIDAY_PURIFIER_ID = string.Empty;
                                                row.FRIDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridSaturday":
                                        {
                                            if (!rowReciever.IsSTATURDAYNull() && !string.IsNullOrEmpty(rowReciever.STATURDAY) && !rowReciever.STATURDAYHEMOID.Equals(row.STATURDAYHEMOID))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.STATURDAY;
                                                    rowReciever.STATURDAY = row.STATURDAY;
                                                    row.STATURDAY = tranStr;

                                                    tranStr = rowReciever.STATURDAYPATIENT;
                                                    rowReciever.STATURDAYPATIENT = row.STATURDAYPATIENT;
                                                    row.STATURDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.STATURDAYREMARK;
                                                    rowReciever.STATURDAYREMARK = row.STATURDAYREMARK;
                                                    row.STATURDAYREMARK = tranStr;

                                                    tranStr = rowReciever.STATURDAYHEMOID;
                                                    rowReciever.STATURDAYHEMOID = row.STATURDAYHEMOID;
                                                    row.STATURDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.STATURDAY_SCHEDULE_ID;
                                                    rowReciever.STATURDAY_SCHEDULE_ID = row.STATURDAY_SCHEDULE_ID;
                                                    row.STATURDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.STATURDAY_PURIFIER_ID;
                                                    rowReciever.STATURDAY_PURIFIER_ID = row.STATURDAY_PURIFIER_ID;
                                                    row.STATURDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.STATURDAY = row.STATURDAY;
                                                rowReciever.STATURDAYPATIENT = row.STATURDAYPATIENT;
                                                rowReciever.STATURDAYREMARK = row.STATURDAYREMARK;
                                                rowReciever.STATURDAYHEMOID = row.STATURDAYHEMOID;
                                                rowReciever.STATURDAY_SCHEDULE_ID = row.STATURDAY_SCHEDULE_ID;
                                                rowReciever.STATURDAY_PURIFIER_ID = row.STATURDAY_PURIFIER_ID;
                                                rowReciever.STATURDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.STATURDAY = string.Empty;
                                                row.STATURDAYPATIENT = string.Empty;
                                                row.STATURDAYREMARK = string.Empty;
                                                row.STATURDAYHEMOID = string.Empty;
                                                row.STATURDAY_SCHEDULE_ID = string.Empty;
                                                row.STATURDAY_PURIFIER_ID = string.Empty;
                                                row.STATURDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridSunday":
                                        {
                                            if (!rowReciever.IsSTATURDAYNull() && !string.IsNullOrEmpty(rowReciever.STATURDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.STATURDAY;
                                                    rowReciever.STATURDAY = row.SUNDAY;
                                                    row.SUNDAY = tranStr;

                                                    tranStr = rowReciever.STATURDAYPATIENT;
                                                    rowReciever.STATURDAYPATIENT = row.SUNDAYPATIENT;
                                                    row.SUNDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.STATURDAYREMARK;
                                                    rowReciever.STATURDAYREMARK = row.SUNDAYREMARK;
                                                    row.SUNDAYREMARK = tranStr;

                                                    tranStr = rowReciever.STATURDAYHEMOID;
                                                    rowReciever.STATURDAYHEMOID = row.SUNDAYHEMOID;
                                                    row.SUNDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.STATURDAY_SCHEDULE_ID;
                                                    rowReciever.STATURDAY_SCHEDULE_ID = row.SUNDAY_SCHEDULE_ID;
                                                    row.SUNDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.STATURDAY_PURIFIER_ID;
                                                    rowReciever.STATURDAY_PURIFIER_ID = row.SUNDAY_PURIFIER_ID;
                                                    row.SUNDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.STATURDAY = row.SUNDAY;
                                                rowReciever.STATURDAYPATIENT = row.SUNDAYPATIENT;
                                                rowReciever.STATURDAYREMARK = row.SUNDAYREMARK;
                                                rowReciever.STATURDAYHEMOID = row.SUNDAYHEMOID;
                                                rowReciever.STATURDAY_SCHEDULE_ID = row.SUNDAY_SCHEDULE_ID;
                                                rowReciever.STATURDAY_PURIFIER_ID = row.SUNDAY_PURIFIER_ID;
                                                rowReciever.STATURDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.SUNDAY = string.Empty;
                                                row.SUNDAYPATIENT = string.Empty;
                                                row.SUNDAYREMARK = string.Empty;
                                                row.SUNDAYHEMOID = string.Empty;
                                                row.SUNDAY_SCHEDULE_ID = string.Empty;
                                                row.SUNDAY_PURIFIER_ID = string.Empty;
                                                row.SUNDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                }
                                break;
                            }
                        case "gridSunday":
                            {
                                switch (chandle)
                                {
                                    case "gridMonday":
                                        {
                                            if (!rowReciever.IsSUNDAYNull() && !string.IsNullOrEmpty(rowReciever.SUNDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.SUNDAY;
                                                    rowReciever.SUNDAY = row.MONDAY;
                                                    row.MONDAY = tranStr;

                                                    tranStr = rowReciever.SUNDAYPATIENT;
                                                    rowReciever.SUNDAYPATIENT = row.MONDAYPATIENT;
                                                    row.MONDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.SUNDAYREMARK;
                                                    rowReciever.SUNDAYREMARK = row.MONDAYREMARK;
                                                    row.MONDAYREMARK = tranStr;

                                                    tranStr = rowReciever.SUNDAYHEMOID;
                                                    rowReciever.SUNDAYHEMOID = row.MONDAYHEMOID;
                                                    row.MONDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.SUNDAY_SCHEDULE_ID;
                                                    rowReciever.SUNDAY_SCHEDULE_ID = row.MONDAY_SCHEDULE_ID;
                                                    row.MONDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.SUNDAY_PURIFIER_ID;
                                                    rowReciever.SUNDAY_PURIFIER_ID = row.MONDAY_PURIFIER_ID;
                                                    row.MONDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.SUNDAY = row.MONDAY;
                                                rowReciever.SUNDAYPATIENT = row.MONDAYPATIENT;
                                                rowReciever.SUNDAYREMARK = row.MONDAYREMARK;
                                                rowReciever.SUNDAYHEMOID = row.MONDAYHEMOID;
                                                rowReciever.SUNDAY_SCHEDULE_ID = row.MONDAY_SCHEDULE_ID;
                                                rowReciever.SUNDAY_PURIFIER_ID = row.MONDAY_PURIFIER_ID;
                                                rowReciever.SUNDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.MONDAY = string.Empty;
                                                row.MONDAYPATIENT = string.Empty;
                                                row.MONDAYREMARK = string.Empty;
                                                row.MONDAYHEMOID = string.Empty;
                                                row.MONDAY_SCHEDULE_ID = string.Empty;
                                                row.MONDAY_PURIFIER_ID = string.Empty;
                                                row.MONDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridTuesday":
                                        {
                                            if (!rowReciever.IsSUNDAYNull() && !string.IsNullOrEmpty(rowReciever.SUNDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.SUNDAY;
                                                    rowReciever.SUNDAY = row.TUESDAY;
                                                    row.TUESDAY = tranStr;

                                                    tranStr = rowReciever.SUNDAYPATIENT;
                                                    rowReciever.SUNDAYPATIENT = row.TUESDAYPATIENT;
                                                    row.TUESDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.SUNDAYREMARK;
                                                    rowReciever.SUNDAYREMARK = row.TUESDAYREMARK;
                                                    row.TUESDAYREMARK = tranStr;

                                                    tranStr = rowReciever.SUNDAYHEMOID;
                                                    rowReciever.SUNDAYHEMOID = row.TUESDAYHEMOID;
                                                    row.TUESDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.SUNDAY_SCHEDULE_ID;
                                                    rowReciever.SUNDAY_SCHEDULE_ID = row.TUESDAY_SCHEDULE_ID;
                                                    row.TUESDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.SUNDAY_PURIFIER_ID;
                                                    rowReciever.SUNDAY_PURIFIER_ID = row.TUESDAY_PURIFIER_ID;
                                                    row.TUESDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.SUNDAY = row.TUESDAY;
                                                rowReciever.SUNDAYPATIENT = row.TUESDAYPATIENT;
                                                rowReciever.SUNDAYREMARK = row.TUESDAYREMARK;
                                                rowReciever.SUNDAYHEMOID = row.TUESDAYHEMOID;
                                                rowReciever.SUNDAY_SCHEDULE_ID = row.TUESDAY_SCHEDULE_ID;
                                                rowReciever.SUNDAY_PURIFIER_ID = row.TUESDAY_PURIFIER_ID;
                                                rowReciever.SUNDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.TUESDAY = string.Empty;
                                                row.TUESDAYPATIENT = string.Empty;
                                                row.TUESDAYREMARK = string.Empty;
                                                row.TUESDAYHEMOID = string.Empty;
                                                row.TUESDAY_SCHEDULE_ID = string.Empty;
                                                row.TUESDAY_PURIFIER_ID = string.Empty;
                                                row.TUESDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridWednesday":
                                        {
                                            if (!rowReciever.IsSUNDAYNull() && !string.IsNullOrEmpty(rowReciever.SUNDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.SUNDAY;
                                                    rowReciever.SUNDAY = row.WEDNESDAY;
                                                    row.WEDNESDAY = tranStr;

                                                    tranStr = rowReciever.SUNDAYPATIENT;
                                                    rowReciever.SUNDAYPATIENT = row.WEDNESDAYPATIENT;
                                                    row.WEDNESDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.SUNDAYREMARK;
                                                    rowReciever.SUNDAYREMARK = row.WEDNESDAYREMARK;
                                                    row.WEDNESDAYREMARK = tranStr;

                                                    tranStr = rowReciever.SUNDAYHEMOID;
                                                    rowReciever.SUNDAYHEMOID = row.WEDNESDAYHEMOID;
                                                    row.WEDNESDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.SUNDAY_SCHEDULE_ID;
                                                    rowReciever.SUNDAY_SCHEDULE_ID = row.WEDNESDAY_SCHEDULE_ID;
                                                    row.WEDNESDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.SUNDAY_PURIFIER_ID;
                                                    rowReciever.SUNDAY_PURIFIER_ID = row.WEDNESDAY_PURIFIER_ID;
                                                    row.WEDNESDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.SUNDAY = row.WEDNESDAY;
                                                rowReciever.SUNDAYPATIENT = row.WEDNESDAYPATIENT;
                                                rowReciever.SUNDAYREMARK = row.WEDNESDAYREMARK;
                                                rowReciever.SUNDAYHEMOID = row.WEDNESDAYHEMOID;
                                                rowReciever.SUNDAY_SCHEDULE_ID = row.WEDNESDAY_SCHEDULE_ID;
                                                rowReciever.SUNDAY_PURIFIER_ID = row.WEDNESDAY_PURIFIER_ID;
                                                rowReciever.SUNDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.WEDNESDAY = string.Empty;
                                                row.WEDNESDAYPATIENT = string.Empty;
                                                row.WEDNESDAYREMARK = string.Empty;
                                                row.WEDNESDAYHEMOID = string.Empty;
                                                row.WEDNESDAY_SCHEDULE_ID = string.Empty;
                                                row.WEDNESDAY_PURIFIER_ID = string.Empty;
                                                row.WEDNESDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridThursday":
                                        {
                                            if (!rowReciever.IsSUNDAYNull() && !string.IsNullOrEmpty(rowReciever.SUNDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.SUNDAY;
                                                    rowReciever.SUNDAY = row.THURSDAY;
                                                    row.THURSDAY = tranStr;

                                                    tranStr = rowReciever.SUNDAYPATIENT;
                                                    rowReciever.SUNDAYPATIENT = row.THURSDAYPATIENT;
                                                    row.THURSDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.SUNDAYREMARK;
                                                    rowReciever.SUNDAYREMARK = row.THURSDAYREMARK;
                                                    row.THURSDAYREMARK = tranStr;

                                                    tranStr = rowReciever.SUNDAYHEMOID;
                                                    rowReciever.SUNDAYHEMOID = row.THURSDAYHEMOID;
                                                    row.THURSDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.SUNDAY_SCHEDULE_ID;
                                                    rowReciever.SUNDAY_SCHEDULE_ID = row.THURSDAY_SCHEDULE_ID;
                                                    row.THURSDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.SUNDAY_PURIFIER_ID;
                                                    rowReciever.SUNDAY_PURIFIER_ID = row.THURSDAY_PURIFIER_ID;
                                                    row.THURSDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.SUNDAY = row.THURSDAY;
                                                rowReciever.SUNDAYPATIENT = row.THURSDAYPATIENT;
                                                rowReciever.SUNDAYREMARK = row.THURSDAYREMARK;
                                                rowReciever.SUNDAYHEMOID = row.THURSDAYHEMOID;
                                                rowReciever.SUNDAY_SCHEDULE_ID = row.THURSDAY_SCHEDULE_ID;
                                                rowReciever.SUNDAY_PURIFIER_ID = row.THURSDAY_PURIFIER_ID;
                                                rowReciever.SUNDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.THURSDAY = string.Empty;
                                                row.THURSDAYPATIENT = string.Empty;
                                                row.THURSDAYREMARK = string.Empty;
                                                row.THURSDAYHEMOID = string.Empty;
                                                row.THURSDAY_SCHEDULE_ID = string.Empty;
                                                row.THURSDAY_PURIFIER_ID = string.Empty;
                                                row.THURSDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridFriday":
                                        {
                                            if (!rowReciever.IsSUNDAYNull() && !string.IsNullOrEmpty(rowReciever.SUNDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.SUNDAY;
                                                    rowReciever.SUNDAY = row.FRIDAY;
                                                    row.FRIDAY = tranStr;

                                                    tranStr = rowReciever.SUNDAYPATIENT;
                                                    rowReciever.SUNDAYPATIENT = row.FRIDAYPATIENT;
                                                    row.FRIDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.SUNDAYREMARK;
                                                    rowReciever.SUNDAYREMARK = row.FRIDAYREMARK;
                                                    row.FRIDAYREMARK = tranStr;

                                                    tranStr = rowReciever.SUNDAYHEMOID;
                                                    rowReciever.SUNDAYHEMOID = row.FRIDAYHEMOID;
                                                    row.FRIDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.SUNDAY_SCHEDULE_ID;
                                                    rowReciever.SUNDAY_SCHEDULE_ID = row.FRIDAY_SCHEDULE_ID;
                                                    row.FRIDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.SUNDAY_PURIFIER_ID;
                                                    rowReciever.SUNDAY_PURIFIER_ID = row.FRIDAY_PURIFIER_ID;
                                                    row.FRIDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.SUNDAY = row.FRIDAY;
                                                rowReciever.SUNDAYPATIENT = row.FRIDAYPATIENT;
                                                rowReciever.SUNDAYREMARK = row.FRIDAYREMARK;
                                                rowReciever.SUNDAYHEMOID = row.FRIDAYHEMOID;
                                                rowReciever.SUNDAY_SCHEDULE_ID = row.FRIDAY_SCHEDULE_ID;
                                                rowReciever.SUNDAY_PURIFIER_ID = row.FRIDAY_PURIFIER_ID;
                                                rowReciever.SUNDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.FRIDAY = string.Empty;
                                                row.FRIDAYPATIENT = string.Empty;
                                                row.FRIDAYREMARK = string.Empty;
                                                row.FRIDAYHEMOID = string.Empty;
                                                row.FRIDAY_SCHEDULE_ID = string.Empty;
                                                row.FRIDAY_PURIFIER_ID = string.Empty;
                                                row.FRIDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridSaturday":
                                        {
                                            if (!rowReciever.IsSUNDAYNull() && !string.IsNullOrEmpty(rowReciever.SUNDAY))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.SUNDAY;
                                                    rowReciever.SUNDAY = row.STATURDAY;
                                                    row.STATURDAY = tranStr;

                                                    tranStr = rowReciever.SUNDAYPATIENT;
                                                    rowReciever.SUNDAYPATIENT = row.STATURDAYPATIENT;
                                                    row.STATURDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.STATURDAYREMARK;
                                                    rowReciever.STATURDAYREMARK = row.SUNDAYREMARK;
                                                    row.SUNDAYREMARK = tranStr;

                                                    tranStr = rowReciever.SUNDAYHEMOID;
                                                    rowReciever.SUNDAYHEMOID = row.STATURDAYHEMOID;
                                                    row.STATURDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.SUNDAY_SCHEDULE_ID;
                                                    rowReciever.SUNDAY_SCHEDULE_ID = row.STATURDAY_SCHEDULE_ID;
                                                    row.STATURDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.SUNDAY_PURIFIER_ID;
                                                    rowReciever.SUNDAY_PURIFIER_ID = row.STATURDAY_PURIFIER_ID;
                                                    row.STATURDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.SUNDAY = row.STATURDAY;
                                                rowReciever.SUNDAYPATIENT = row.STATURDAYPATIENT;
                                                rowReciever.SUNDAYREMARK = row.STATURDAYREMARK;
                                                rowReciever.SUNDAYHEMOID = row.STATURDAYHEMOID;
                                                rowReciever.SUNDAY_SCHEDULE_ID = row.STATURDAY_SCHEDULE_ID;
                                                rowReciever.SUNDAY_PURIFIER_ID = row.STATURDAY_PURIFIER_ID;
                                                rowReciever.SUNDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.STATURDAY = string.Empty;
                                                row.STATURDAYPATIENT = string.Empty;
                                                row.STATURDAYREMARK = string.Empty;
                                                row.STATURDAYHEMOID = string.Empty;
                                                row.STATURDAY_SCHEDULE_ID = string.Empty;
                                                row.STATURDAY_PURIFIER_ID = string.Empty;
                                                row.STATURDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                    case "gridSunday":
                                        {
                                            if (!rowReciever.IsSUNDAYNull() && !string.IsNullOrEmpty(rowReciever.SUNDAY) && !rowReciever.SUNDAYHEMOID.Equals(row.SUNDAYHEMOID))
                                            {
                                                if (DialogResult.OK == XtraMessageBox.Show("是否确认交换？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                                                {
                                                    string tranStr = rowReciever.SUNDAY;
                                                    rowReciever.SUNDAY = row.SUNDAY;
                                                    row.SUNDAY = tranStr;

                                                    tranStr = rowReciever.SUNDAYPATIENT;
                                                    rowReciever.SUNDAYPATIENT = row.SUNDAYPATIENT;
                                                    row.SUNDAYPATIENT = tranStr;

                                                    tranStr = rowReciever.STATURDAYREMARK;
                                                    rowReciever.STATURDAYREMARK = row.STATURDAYREMARK;
                                                    row.STATURDAYREMARK = tranStr;

                                                    tranStr = rowReciever.SUNDAYHEMOID;
                                                    rowReciever.SUNDAYHEMOID = row.SUNDAYHEMOID;
                                                    row.SUNDAYHEMOID = tranStr;

                                                    tranStr = rowReciever.SUNDAY_SCHEDULE_ID;
                                                    rowReciever.SUNDAY_SCHEDULE_ID = row.SUNDAY_SCHEDULE_ID;
                                                    row.SUNDAY_SCHEDULE_ID = tranStr;

                                                    tranStr = rowReciever.SUNDAY_PURIFIER_ID;
                                                    rowReciever.SUNDAY_PURIFIER_ID = row.SUNDAY_PURIFIER_ID; ;
                                                    row.SUNDAY_PURIFIER_ID = tranStr;
                                                }
                                            }
                                            else
                                            {
                                                rowReciever.SUNDAY = row.SUNDAY;
                                                rowReciever.SUNDAYPATIENT = row.SUNDAYPATIENT;
                                                rowReciever.SUNDAYREMARK = row.SUNDAYREMARK;
                                                rowReciever.SUNDAYHEMOID = row.SUNDAYHEMOID;
                                                rowReciever.SUNDAY_SCHEDULE_ID = row.SUNDAY_SCHEDULE_ID;
                                                rowReciever.SUNDAY_PURIFIER_ID = row.SUNDAY_PURIFIER_ID;
                                                rowReciever.SUNDAY_IS_CRRT = rowReciever.QYNAME.Substring(0, 4).Equals("CRRT") ? "1" : "0";
                                                row.SUNDAY = string.Empty;
                                                row.SUNDAYPATIENT = string.Empty;
                                                row.SUNDAYREMARK = string.Empty;
                                                row.SUNDAYHEMOID = string.Empty;
                                                row.SUNDAY_SCHEDULE_ID = string.Empty;
                                                row.SUNDAY_PURIFIER_ID = string.Empty;
                                                row.SUNDAY_IS_CRRT = string.Empty;
                                            }
                                            break;
                                        }
                                }
                                break;
                            }
                    }
                    _isDirty = true;
                    #endregion
                }
                row.EndEdit();
            }
            _isDirty = true;
        }
        /// <summary>
        /// 拖动的时候进行复制。以便有效果。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControlForEmerger_DragOver(object sender, DragEventArgs e)
        {
            GridHitInfo hi = gridView1.CalcHitInfo(gridControlForEmerger.PointToClient(new Point(e.X, e.Y)));
            int handle = GetDragObject(sender, e.Data);
            if (hi.InRow && handle >= 0)// && handle != hi.RowHandle)
            {
                e.Effect = DragDropEffects.Copy;
                //if ((e.KeyState & 8) != 0)
                //    e.Effect = DragDropEffects.Copy;
                //else
                //    e.Effect = DragDropEffects.Move;
            }
            else e.Effect = DragDropEffects.None;
        }

        private void gridControlForEmerger_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void gridView1_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {

        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var rowCurrent = this.gridView1.GetFocusedDataRow() as PermissionModel.MED_HEMO_SCHEDULEMASTERRow;

            if (rowCurrent == null || e.CellValue == null) return;

            var value = this._insulateArea.FirstOrDefault(i => i.ITEM_VALUE == e.CellValue.ToString());
            if (e.Column.Name == this.gridColumn1.Name)
            {
                var aaStr = "asdgasdfasa";
            }
            if (e.Column.Name == this.gridOffice.Name)
            {
                var bestr = "com on!";
            }

            if (value != null && e.Column.Name == this.gridOffice.Name)
            {
                if (!e.CellValue.ToString().Equals("第九透析室"))
                {
                    e.Appearance.Font = new Font("Tahoma", 11, FontStyle.Bold);
                    e.Appearance.BackColor = Color.Red;
                }
            }


            if (rowCurrent == null || gridView1.FocusedColumn == null || gridView1.FocusedColumn.Name.ToString() == this.gridBanchi.Name.ToString() ||
                gridView1.FocusedColumn.Name.ToString() == this.gridColumn1.Name.ToString() ||
                gridView1.FocusedColumn.Name.ToString() == this.gridOffice.Name.ToString() ||
                gridView1.FocusedColumn.Name.ToString() == this.gridBedNo.Name.ToString())
                return;
            if (e.RowHandle >= 0 && (e.Column == gridBedNo || e.Column == gridMonday || e.Column == gridFriday || e.Column == gridSunday || e.Column == gridThursday || e.Column == gridTuesday || e.Column == gridWednesday || e.Column == gridSaturday))
            {
                if (e.RowHandle == this.gridView1.FocusedRowHandle && e.Column == this.gridView1.FocusedColumn)
                {
                    var cellValue = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.FocusedColumn);
                    if (cellValue != null)
                        e.Appearance.Font = new Font("Tahoma", 11, FontStyle.Bold);
                }
            }
        }
        #endregion
    }
}


