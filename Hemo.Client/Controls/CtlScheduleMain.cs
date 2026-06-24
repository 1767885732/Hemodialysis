/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：用户排班控件
// 创建时间：2015-08-21
// 创建者：吕志强
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/
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
using Hemo.Utilities;

namespace Hemo.Client.Controls {
    public partial class CtlScheduleMain : XtraUserControl {
        #region 变量

        /// <summary>
        /// 排班容器总宽
        /// </summary>
        //private int _pnlScheduleContainerWidth = 1524;
        private int _pnlScheduleContainerWidth = 1867;
        /// <summary>
        /// 开始日期
        /// </summary>  
        private DateTime _beginDate;
        /// <summary>
        /// 结束日期
        /// </summary>
        private DateTime _endDate;
        private GroupControl[] _groupControls;

        private IUser _userService = ServiceManager.Instance.UserService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IMachine _machineService = ServiceManager.Instance.MachineService;
        private IPatient _patientService = ServiceManager.Instance.PatientService;
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;

        private Dictionary<ConfigModel.MED_COMMON_ITEMLISTRow, int> _areaDict;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _bedDataTable;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _purifierModelDataTable;
        private MachineModel.MED_DIALYSIS_MACHINEDataTable _machineDataTable;
        private PatientModel.MED_PATIENTSDataTable _patientDataTable;
        private HemodialysisModel.MED_HEMO_RECIPEDataTable _recipeDataTable;

        private PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable _patientScheduleTemplateDataTable;
        private PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATERow _patientScheduleTemplateRow;

        public RadioGroup RgpBanCi {
            get {
                return this.rgpBANCI;
            }
        }


        //public PatientModel.MED_PATIENTSRow PatientRow {
        //    get {
        //        return this._patientDataTable.FindByHEMODIALYSIS_ID(this.treeListPatient.FocusedNode.GetValue("HEMODIALYSIS_ID").ToString());
        //    }
        //}


        #endregion

        #region 构造函数

        public CtlScheduleMain() {
            this.InitializeComponent();
        }

        #endregion

        #region 方法

        private PatientScheduleModel.MED_PATIENT_SCHEDULERow GetPatientScheduleRow(PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable patientScheduleDataTable, CtlSchedulePerson ctlSchedulePerson, DateTime dialysisDate) {
            PatientScheduleModel.MED_PATIENT_SCHEDULERow[] patientScheduleRows = patientScheduleDataTable.Select(string.Format("MONITOR_LABEL = '{0}' AND DIALYSIS_DATE = '{1}' AND BANCI_ID = '{2}' AND DIALYSIS_ROOM_ID = '{3}' AND BED_NUMBER = '{4}'", ctlSchedulePerson.MachineRow.MACHINE_ID, dialysisDate, this.rgpBANCI.EditValue, ctlSchedulePerson.AreaRow.ITEM_ID, ctlSchedulePerson.BedRow.ITEM_ID)) as PatientScheduleModel.MED_PATIENT_SCHEDULERow[];

            return patientScheduleRows.Length == 0 ? null : patientScheduleRows[0];
        }

        private PatientScheduleModel.MED_PATIENT_SCHEDULERow GetPatientScheduleRow(PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable patientScheduleDataTable, CtlSchedulePerson ctlSchedulePerson, DayOfWeek dayOfWeek) {
            PatientScheduleModel.MED_PATIENT_SCHEDULERow[] patientScheduleRows = patientScheduleDataTable.Select(string.Format("MONITOR_LABEL = '{0}' AND BANCI_ID = '{1}' AND DIALYSIS_ROOM_ID = '{2}' AND BED_NUMBER = '{3}'", ctlSchedulePerson.MachineRow.MACHINE_ID, this.rgpBANCI.EditValue, ctlSchedulePerson.AreaRow.ITEM_ID, ctlSchedulePerson.BedRow.ITEM_ID)) as PatientScheduleModel.MED_PATIENT_SCHEDULERow[];

            foreach (var row in patientScheduleRows) {
                if (row.DIALYSIS_DATE.DayOfWeek == dayOfWeek)
                    return row;
            }

            return null;
        }

        /// <summary>
        /// 绑定患者列表数据
        /// </summary>
        private void LoadPatientTreeData() {
            this.busyIndicator1.ShowLoadingScreenFor(this.treeListPatient);
            DataTable dtMainCure = new DataTable();
            this.treeListPatient.BeginUnboundLoad();
            this.treeListPatient.Nodes.Clear();

            using (BackgroundWorker worker = new BackgroundWorker()) {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    DateTime dtBeginMonth = DateTime.Parse(string.Format("{0}-{1}-{2}", this._beginDate.Year, this._beginDate.Month, "1")).Date;
                    DateTime dtEndMonth = dtBeginMonth.AddMonths(1).AddDays(-1).Date;
                    dtMainCure = this._hemodialysisService.GetMainCureGroupByHemoIDAndPurificationMode(dtBeginMonth, dtEndMonth);
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    foreach (PatientModel.MED_PATIENTSRow patientRow in this._patientDataTable.Rows) {
                        DataRow[] rowMainCures = dtMainCure.Select(string.Format("HEMODIALYSIS_ID = '{0}'", patientRow.HEMODIALYSIS_ID));
                        StringBuilder text = new StringBuilder();

                        if (rowMainCures.Length > 0) {
                            text.Append("(");

                            for (int i = 0; i < rowMainCures.Length; i++) {
                                text.AppendFormat("{0}{1}:{2}", i == 0 ? string.Empty : " ", rowMainCures[i]["PURIFICATION_MODE_NAME"], rowMainCures[i]["COUNT"]);
                            }

                            text.Append(")");
                        }

                        this.treeListPatient.AppendNode(new object[] { string.Format("{0} {1}", patientRow.NAME, text.ToString()), patientRow.SEX, patientRow.HEMODIALYSIS_ID }, -1);
                    }

                    foreach (TreeListNode node in this.treeListPatient.Nodes) {
                        if (string.Compare(node.GetValue("SEX").ToString(), "男", true) == 0) {
                            node.StateImageIndex = 0;
                            node.ImageIndex = 0;
                        }
                        else {
                            node.StateImageIndex = 1;
                            node.ImageIndex = 1;
                        }
                    }

                    this.busyIndicator1.HideLoadingScreen();

                };
                worker.RunWorkerAsync();
            }

            this.treeListPatient.EndUnboundLoad();
        }

        /// <summary>
        /// 创建病患排班控件
        /// </summary>
        private void CreatePatientScheduleControls() {
            int height = 0;

            this.pnlScheduleContainer.Left = 0;
            this.pnlScheduleContainer.Top = 0;
            this.pnlScheduleContainer.Width = this._pnlScheduleContainerWidth;

            //创建病区显示列表
            foreach (KeyValuePair<ConfigModel.MED_COMMON_ITEMLISTRow, int> areaItem in this._areaDict) {
                LabelControl lblRange = new LabelControl();
                lblRange.BorderStyle = BorderStyles.Simple;
                lblRange.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                lblRange.Appearance.TextOptions.VAlignment = VertAlignment.Center;
                lblRange.AutoSizeMode = LabelAutoSizeMode.None;
                lblRange.Height = areaItem.Value + 20;
                lblRange.Dock = DockStyle.Top;
                lblRange.Text = areaItem.Key.ITEM_NAME;

                this.grpRange.Controls.Add(lblRange);

                height += areaItem.Value + 20;
            }

            this.pnlScheduleContainer.Height = height + 50;

            for (int i = 0; i < this._groupControls.Length; i++) {
                SchedulePanel ctlSchedulePanel = new SchedulePanel(i, this._areaDict, this._bedDataTable, this._machineDataTable);
                ctlSchedulePanel.Dock = DockStyle.Fill;

                this._groupControls[i].Controls.Add(ctlSchedulePanel);
            }
        }

        /// <summary>
        /// 加载病患排班数据
        /// </summary>
        /// <param name="templateID"></param>
        private void LoadPatientScheduleData(string templateID) {
            if (this._beginDate == DateTime.MinValue)
                return;

            SchedulePersonDragManager.Instance.InitHemoIDDict();

            PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable patientScheduleDataTable, tempPatientScheduleDataTable = null;

            if (string.IsNullOrEmpty(templateID))
                patientScheduleDataTable = this._patientScheduleService.GetPatientScheduleList(LoginUser.User.USER_ID, this._beginDate, this._endDate, this.rgpBANCI.EditValue.ToString());
            else {//载入模板
                patientScheduleDataTable = this._patientScheduleService.GetPatientScheduleListByTemplateID(templateID);

                tempPatientScheduleDataTable = this._patientScheduleService.GetPatientScheduleList(LoginUser.User.USER_ID, this._beginDate, this._endDate, this.rgpBANCI.EditValue.ToString());

            }

            foreach (var item in SchedulePersonDragManager.Instance.SchedulePersonControlDict) {
                int i = item.Key;
                DateTime dialysisDate = this._beginDate.AddDays(i);

                this._groupControls[i].Text = string.Format("{0}（{1}）", this._groupControls[i].Tag, dialysisDate.ToString("yyyy-MM-dd"));

                foreach (var ctlSchedulePerson in item.Value) {
                    //病患排班
                    PatientScheduleModel.MED_PATIENT_SCHEDULERow patientScheduleRow = null;

                    if (string.IsNullOrEmpty(templateID))
                        patientScheduleRow = this.GetPatientScheduleRow(patientScheduleDataTable, ctlSchedulePerson, dialysisDate);
                    else
                        patientScheduleRow = this.GetPatientScheduleRow(patientScheduleDataTable, ctlSchedulePerson, dialysisDate.DayOfWeek);

                    ctlSchedulePerson.ClearInfo();

                    ctlSchedulePerson.SetBaseInfo(dialysisDate, this._purifierModelDataTable, this._recipeDataTable);

                    if (patientScheduleRow != null) {
                        ctlSchedulePerson.SetPatientInfo(
                            this._patientDataTable.FindByHEMODIALYSIS_ID(patientScheduleRow.HEMODIALYSIS_ID),
                            patientScheduleRow);

                        ctlSchedulePerson.SetOtherInfo(
                            patientScheduleRow.IsREMARKNull() ? string.Empty : patientScheduleRow.REMARK,
                            patientScheduleRow.IsRECIPE_IDNull() ? string.Empty : patientScheduleRow.RECIPE_ID,
                            patientScheduleRow.IsPURIFIER_MODEL_IDNull() ? string.Empty : patientScheduleRow.PURIFIER_MODEL_ID);
                    }
                    //因为排班模板有时候会和排班数据混，所以拿下来可以进行独立。 2013-12-25 horace.jc
                    if (tempPatientScheduleDataTable != null) {
                        PatientScheduleModel.MED_PATIENT_SCHEDULERow tempPatientScheduleRow = this.GetPatientScheduleRow(tempPatientScheduleDataTable, ctlSchedulePerson, dialysisDate);
                        //加载的为模板排班数据
                        ctlSchedulePerson.ScheduleType = "Temp";
                        if (tempPatientScheduleRow != null) {
                            if (!tempPatientScheduleRow.IsSTART_TIMENull() || !tempPatientScheduleRow.IsEND_TIMENull()) {
                                SchedulePersonDragManager.Instance.AddHemoID(ctlSchedulePerson.DayOfWeek, tempPatientScheduleRow.HEMODIALYSIS_ID);

                                continue;
                            }
                        }
                    }


                }
            }
        }

        private bool CheckValue() {
            //foreach (var item in SchedulePersonDragManager.Instance.SchedulePersonControlDict)
            //{
            //    foreach (var ctlSchedulePerson in item.Value)
            //    {
            //        if (ctlSchedulePerson.PatientRow != null)
            //            if (string.IsNullOrEmpty(ctlSchedulePerson.RECIPE_ID) || string.IsNullOrEmpty(ctlSchedulePerson.PURIFIER_MODEL_ID))
            //                return false;
            //    }
            //}

            return true;
        }

        /// <summary>
        /// 保存病患排班数据
        /// </summary>
        private void SavePatientScheduleData() {
            #region 之前的保存方法，当数据量过大时报错：ORA-06550: 第 1 行, 第 1 列: PLS-00123: 程序太大 (Diana nodes)
            //所以去掉老的保存方法,采用临时表数据进行保存,以避免这种错误的发生.

            this._patientScheduleService.DeletePatientSchedule(this._beginDate, this._endDate, this.rgpBANCI.EditValue.ToString(), LoginUser.User.USER_ID);

            PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable patientScheduleDataTable = new PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable();

            foreach (var item in SchedulePersonDragManager.Instance.SchedulePersonControlDict) {
                DateTime dialysisDate = this._beginDate.AddDays(item.Key);

                foreach (var ctlSchedulePerson in item.Value) {

                    //这个判断方法有误，并发时因为数据已经发生了改变所以再以这个判断就不准了。
                    //if (ctlSchedulePerson.IsPatientTreated)
                    //    continue;
                    PatientModel.MED_PATIENTSRow patientRow = ctlSchedulePerson.PatientRow;


                    if (patientRow != null) {
                        PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable dttmp = this._patientScheduleService.GetPatientScheduleSignle(dialysisDate, patientRow.HEMODIALYSIS_ID);
                        if (dttmp != null && dttmp.Rows.Count > 0) {

                            //var dttmpRow = dttmp.FirstOrDefault(i => i.BANCI_ID == this.rgpBANCI.EditValue.ToString());
                            //if (!dttmpRow.IsSTART_TIMENull() && !dttmpRow.IsEND_TIMENull())
                            //   continue;
                            //2013-12-16 刘超 修改保存排班数据重复方法
                            var dttmpRow = dttmp.Count(i => i.BANCI_ID == this.rgpBANCI.EditValue.ToString());
                            if (dttmpRow > 0) {
                                continue;
                            }
                        }
                    }


                    if (patientRow != null) {
                        PatientScheduleModel.MED_PATIENT_SCHEDULERow patientScheduleRow = patientScheduleDataTable.NewMED_PATIENT_SCHEDULERow();

                        patientScheduleRow.PATIENT_SCHEDULE_ID = Guid.NewGuid().ToString();
                        patientScheduleRow.PATIENT_ID = patientRow.PATIENT_ID;
                        patientScheduleRow.MONITOR_LABEL = ctlSchedulePerson.MachineRow.MACHINE_ID;
                        patientScheduleRow.DIALYSIS_DATE = dialysisDate;
                        patientScheduleRow.BANCI_ID = this.rgpBANCI.EditValue.ToString();
                        patientScheduleRow.DIALYSIS_ROOM_ID = ctlSchedulePerson.AreaRow.ITEM_ID;
                        patientScheduleRow.BED_NUMBER = ctlSchedulePerson.BedRow.ITEM_ID;
                        patientScheduleRow.STATUS = "1";
                        patientScheduleRow.HEMODIALYSIS_ID = patientRow.HEMODIALYSIS_ID;
                        if (ctlSchedulePerson.Comments != null) {
                            patientScheduleRow.REMARK = ctlSchedulePerson.Comments.Trim();
                        }
                        patientScheduleRow.RECIPE_ID = ctlSchedulePerson.RECIPE_ID;
                        patientScheduleRow.PURIFIER_MODEL_ID = ctlSchedulePerson.PURIFIER_MODEL_ID;
                        patientScheduleRow.USER_ID = LoginUser.User.USER_ID;

                        patientScheduleDataTable.AddMED_PATIENT_SCHEDULERow(patientScheduleRow);
                    }
                    //采取分数据组方式进行保存，以避免大数据问题。但经过实际测试无法进行。
                    if (patientScheduleDataTable.Rows.Count > 200) {
                        this._patientScheduleService.SavePatientScheduleInfo(patientScheduleDataTable);
                        patientScheduleDataTable = new PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable();
                    }
                }
            }

            this._patientScheduleService.SavePatientScheduleInfo(patientScheduleDataTable);

            #endregion


            #region 避免以上错误的的保存方法 。。。。经过测试依旧无法解决问题。。。所以保留方法 以观后尤
            //PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable patientScheduleDataTableTemp = new PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable();

            //PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable patientScheduleDataTable = this._patientScheduleService.GetPatientScheduleList(LoginUser.User.USER_ID, this._beginDate, this._endDate, this.rgpBANCI.EditValue.ToString());

            //foreach (var item in SchedulePersonDragManager.Instance.SchedulePersonControlDict)
            //{
            //    DateTime dialysisDate = this._beginDate.AddDays(item.Key);


            //    foreach (var ctlSchedulePerson in item.Value)
            //    {
            //        if (ctlSchedulePerson.IsPatientTreated)
            //            continue;

            //        PatientModel.MED_PATIENTSRow patientRow = ctlSchedulePerson.PatientRow;
            //        if (patientRow != null)
            //        {
            //            var row = patientScheduleDataTable.FirstOrDefault(k => k.PATIENT_ID == patientRow.PATIENT_ID && k.BANCI_ID == this.rgpBANCI.EditValue.ToString() && k.HEMODIALYSIS_ID == patientRow.HEMODIALYSIS_ID && k.PATIENT_SCHEDULE_ID == ctlSchedulePerson.PatientScheduleRow.PATIENT_SCHEDULE_ID && k.DIALYSIS_DATE == dialysisDate);
            //            if (row == null)
            //            {
            //                PatientScheduleModel.MED_PATIENT_SCHEDULERow patientScheduleDataRow = patientScheduleDataTable.NewMED_PATIENT_SCHEDULERow();

            //                patientScheduleDataRow.PATIENT_SCHEDULE_ID = Guid.NewGuid().ToString();
            //                patientScheduleDataRow.PATIENT_ID = ConvertToString(patientRow.PATIENT_ID);
            //                patientScheduleDataRow.MONITOR_LABEL = ConvertToString(ctlSchedulePerson.MachineRow.MACHINE_ID);
            //                patientScheduleDataRow.DIALYSIS_DATE = dialysisDate;
            //                patientScheduleDataRow.BANCI_ID = this.rgpBANCI.EditValue.ToString();
            //                patientScheduleDataRow.DIALYSIS_ROOM_ID = ConvertToString(ctlSchedulePerson.AreaRow.ITEM_ID);
            //                patientScheduleDataRow.BED_NUMBER = ConvertToString(ctlSchedulePerson.BedRow.ITEM_ID);
            //                patientScheduleDataRow.STATUS = "1";
            //                patientScheduleDataRow.HEMODIALYSIS_ID = ConvertToString(patientRow.HEMODIALYSIS_ID);
            //                patientScheduleDataRow.REMARK = ConvertToString(ctlSchedulePerson.Comments);
            //                patientScheduleDataTable.AddMED_PATIENT_SCHEDULERow(patientScheduleDataRow);


            //                var rowTemp = patientScheduleDataTableTemp.NewMED_PATIENT_SCHEDULERow();
            //                rowTemp.PATIENT_SCHEDULE_ID = patientScheduleDataRow.PATIENT_SCHEDULE_ID;
            //                rowTemp.HEMODIALYSIS_ID = patientScheduleDataRow.HEMODIALYSIS_ID;
            //                rowTemp.PATIENT_ID = patientScheduleDataRow.PATIENT_ID;
            //                patientScheduleDataTableTemp.AddMED_PATIENT_SCHEDULERow(rowTemp);

            //            }
            //            else
            //            {
            //                row.MONITOR_LABEL = ConvertToString(ctlSchedulePerson.MachineRow.MACHINE_ID);
            //                row.DIALYSIS_DATE = dialysisDate;
            //                row.DIALYSIS_ROOM_ID = ConvertToString(ctlSchedulePerson.AreaRow.ITEM_ID);
            //                row.BED_NUMBER = ConvertToString(ctlSchedulePerson.BedRow.ITEM_ID);
            //                row.REMARK = ConvertToString(ctlSchedulePerson.Comments);


            //                var rowTemp = patientScheduleDataTableTemp.NewMED_PATIENT_SCHEDULERow();
            //                rowTemp.PATIENT_SCHEDULE_ID = row.PATIENT_SCHEDULE_ID;
            //                rowTemp.HEMODIALYSIS_ID = row.HEMODIALYSIS_ID;
            //                rowTemp.PATIENT_ID = row.PATIENT_ID;
            //                patientScheduleDataTableTemp.AddMED_PATIENT_SCHEDULERow(rowTemp);

            //            }
            //        }
            //    }
            //}
            //foreach (var row in patientScheduleDataTable)
            //{
            //    var dRow = patientScheduleDataTableTemp.FirstOrDefault(k => k.PATIENT_SCHEDULE_ID == row.PATIENT_SCHEDULE_ID);
            //    if (dRow == null)
            //    {
            //        row.Delete();

            //    }

            //}


            //this._patientScheduleService.SavePatientScheduleInfo(patientScheduleDataTable);
            #endregion



        }

        /// <summary>
        /// 保存病患排班模板数据
        /// </summary>
        /// <param name="templateID"></param>
        private void SavePatientScheduleTemplateData(string banciID) {
            string PATIENT_SCHEDULE_TEMPLATE_ID = string.Empty;
            this._patientScheduleTemplateDataTable = this._patientScheduleService.GetPatientScheduleTemplateList(banciID);
            if (this._patientScheduleTemplateDataTable.Rows.Count <= 0) {
                this._patientScheduleTemplateRow = this._patientScheduleTemplateDataTable.NewMED_PATIENT_SCHEDULE_TEMPLATERow();

                this._patientScheduleTemplateRow.PATIENT_SCHEDULE_TEMPLATE_ID = Guid.NewGuid().ToString();
                this._patientScheduleTemplateRow.PATIENT_SCHEDULE_TEMPLATE_NAME = banciID;
                this._patientScheduleTemplateRow.PATIENT_SCHEDULE_TEMPLATE_DATE = DateTime.Now;

                this._patientScheduleTemplateDataTable.AddMED_PATIENT_SCHEDULE_TEMPLATERow(this._patientScheduleTemplateRow);
                PATIENT_SCHEDULE_TEMPLATE_ID = this._patientScheduleTemplateRow.PATIENT_SCHEDULE_TEMPLATE_ID;
                this._patientScheduleService.SavePatientScheduleTemplateInfo(this._patientScheduleTemplateDataTable);

            }
            else {
                PATIENT_SCHEDULE_TEMPLATE_ID = this._patientScheduleTemplateDataTable.Rows[0][0].ToString();
            }

            #region 正式库同样 出现了程序过大的问题，但本机未出现，同样采取分数据组方式进行保存测试。
            PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable patientScheduleTemplateDataTable = new PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable();
            //删除已存在的
            this._patientScheduleService.DeletePatientScheduleDateTemp(PATIENT_SCHEDULE_TEMPLATE_ID);
            //
            foreach (var item in SchedulePersonDragManager.Instance.SchedulePersonControlDict) {
                DateTime dialysisDate = this._beginDate.AddDays(item.Key);

                foreach (var ctlSchedulePerson in item.Value) {
                    PatientModel.MED_PATIENTSRow patientRow = ctlSchedulePerson.PatientRow;
                    if (patientRow != null) {
                        PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATARow patientScheduleTemplateDataRow = patientScheduleTemplateDataTable.NewMED_PATIENT_SCHEDULE_TEMP_DATARow();

                        patientScheduleTemplateDataRow.MED_SCHEDULE_TEMPLATE_DATA_ID = Guid.NewGuid().ToString();
                        patientScheduleTemplateDataRow.PATIENT_SCHEDULE_TEMPLATE_ID = PATIENT_SCHEDULE_TEMPLATE_ID;
                        patientScheduleTemplateDataRow.PATIENT_ID = ConvertToString(patientRow.PATIENT_ID);
                        patientScheduleTemplateDataRow.MONITOR_LABEL = ConvertToString(ctlSchedulePerson.MachineRow.MACHINE_ID);
                        patientScheduleTemplateDataRow.DIALYSIS_DATE = dialysisDate;
                        patientScheduleTemplateDataRow.BANCI_ID = this.rgpBANCI.EditValue.ToString();
                        patientScheduleTemplateDataRow.DIALYSIS_ROOM_ID = ConvertToString(ctlSchedulePerson.AreaRow.ITEM_ID);
                        patientScheduleTemplateDataRow.BED_NUMBER = ConvertToString(ctlSchedulePerson.BedRow.ITEM_ID);
                        patientScheduleTemplateDataRow.STATUS = "1";
                        patientScheduleTemplateDataRow.HEMODIALYSIS_ID = ConvertToString(patientRow.HEMODIALYSIS_ID);
                        patientScheduleTemplateDataRow.REMARK = ConvertToString(ctlSchedulePerson.Comments);
                        patientScheduleTemplateDataTable.AddMED_PATIENT_SCHEDULE_TEMP_DATARow(patientScheduleTemplateDataRow);

                        if (patientScheduleTemplateDataTable.Rows.Count > 200) {
                            //保存数据
                            this._patientScheduleService.SavePatientScheduleTemplateDataInfo(patientScheduleTemplateDataTable);
                            //保存数据 完成后再时行 
                            patientScheduleTemplateDataTable = new PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable();
                        }
                    }
                }

            }
            //保存数据
            this._patientScheduleService.SavePatientScheduleTemplateDataInfo(patientScheduleTemplateDataTable);
            #endregion




            #region 保存方法
            /*
            PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable patientScheduleTemplateDataTableTemp = new PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable();

            PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable patientScheduleTemplateDataTable = this._patientScheduleService.GetPatientScheduleTempDataList(PATIENT_SCHEDULE_TEMPLATE_ID);

            foreach (var item in SchedulePersonDragManager.Instance.SchedulePersonControlDict) {
                DateTime dialysisDate = this._beginDate.AddDays(item.Key);

                foreach (var ctlSchedulePerson in item.Value) {
                    PatientModel.MED_PATIENTSRow patientRow = ctlSchedulePerson.PatientRow;
                    if (patientRow != null)                    
                    {
                        var row = patientScheduleTemplateDataTable.FirstOrDefault(k => k.PATIENT_ID == patientRow.PATIENT_ID && k.BANCI_ID == this.rgpBANCI.EditValue.ToString() && k.HEMODIALYSIS_ID == patientRow.HEMODIALYSIS_ID && k.PATIENT_SCHEDULE_TEMPLATE_ID == PATIENT_SCHEDULE_TEMPLATE_ID && k.DIALYSIS_DATE == dialysisDate);
                        if (row == null) {
                            PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATARow patientScheduleTemplateDataRow = patientScheduleTemplateDataTable.NewMED_PATIENT_SCHEDULE_TEMP_DATARow();

                            patientScheduleTemplateDataRow.MED_SCHEDULE_TEMPLATE_DATA_ID = Guid.NewGuid().ToString();
                            patientScheduleTemplateDataRow.PATIENT_SCHEDULE_TEMPLATE_ID = PATIENT_SCHEDULE_TEMPLATE_ID;
                            patientScheduleTemplateDataRow.PATIENT_ID = ConvertToString(patientRow.PATIENT_ID);
                            patientScheduleTemplateDataRow.MONITOR_LABEL = ConvertToString(ctlSchedulePerson.MachineRow.MACHINE_ID);
                            patientScheduleTemplateDataRow.DIALYSIS_DATE = dialysisDate;
                            patientScheduleTemplateDataRow.BANCI_ID = this.rgpBANCI.EditValue.ToString();
                            patientScheduleTemplateDataRow.DIALYSIS_ROOM_ID = ConvertToString(ctlSchedulePerson.AreaRow.ITEM_ID);
                            patientScheduleTemplateDataRow.BED_NUMBER = ConvertToString(ctlSchedulePerson.BedRow.ITEM_ID);
                            patientScheduleTemplateDataRow.STATUS = "1";
                            patientScheduleTemplateDataRow.HEMODIALYSIS_ID = ConvertToString(patientRow.HEMODIALYSIS_ID);
                            patientScheduleTemplateDataRow.REMARK = ConvertToString(ctlSchedulePerson.Comments);
                            patientScheduleTemplateDataTable.AddMED_PATIENT_SCHEDULE_TEMP_DATARow(patientScheduleTemplateDataRow);


                            var rowTemp = patientScheduleTemplateDataTableTemp.NewMED_PATIENT_SCHEDULE_TEMP_DATARow();
                            rowTemp.MED_SCHEDULE_TEMPLATE_DATA_ID = patientScheduleTemplateDataRow.MED_SCHEDULE_TEMPLATE_DATA_ID;
                            rowTemp.HEMODIALYSIS_ID = patientScheduleTemplateDataRow.HEMODIALYSIS_ID;
                            rowTemp.PATIENT_ID = patientScheduleTemplateDataRow.PATIENT_ID;
                            patientScheduleTemplateDataTableTemp.AddMED_PATIENT_SCHEDULE_TEMP_DATARow(rowTemp);

                        }
                        else {
                            row.MONITOR_LABEL = ConvertToString(ctlSchedulePerson.MachineRow.MACHINE_ID);
                            row.DIALYSIS_DATE = dialysisDate;
                            row.DIALYSIS_ROOM_ID = ConvertToString(ctlSchedulePerson.AreaRow.ITEM_ID);
                            row.BED_NUMBER = ConvertToString(ctlSchedulePerson.BedRow.ITEM_ID);
                            row.REMARK = ConvertToString(ctlSchedulePerson.Comments);


                            var rowTemp = patientScheduleTemplateDataTableTemp.NewMED_PATIENT_SCHEDULE_TEMP_DATARow();
                            rowTemp.MED_SCHEDULE_TEMPLATE_DATA_ID = row.MED_SCHEDULE_TEMPLATE_DATA_ID;
                            rowTemp.HEMODIALYSIS_ID = row.HEMODIALYSIS_ID;
                            rowTemp.PATIENT_ID = row.PATIENT_ID;
                            patientScheduleTemplateDataTableTemp.AddMED_PATIENT_SCHEDULE_TEMP_DATARow(rowTemp);

                        }
                    }
                }
            }
            foreach (var row in patientScheduleTemplateDataTable) {
                var dRow = patientScheduleTemplateDataTableTemp.FirstOrDefault(k => k.MED_SCHEDULE_TEMPLATE_DATA_ID == row.MED_SCHEDULE_TEMPLATE_DATA_ID);
                if (dRow == null) {
                    row.Delete();

                }

            }
                 
                this._patientScheduleService.SavePatientScheduleTemplateDataInfo(patientScheduleTemplateDataTable);
                 * */
            #endregion



        }

        /// <summary>
        /// 加载病患排班数据
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        public void LoadPatientScheduleData(DateTime beginDate, DateTime endDate) {
            this._beginDate = beginDate;
            this._endDate = endDate;
            CheckDataMethds();
            this.LoadPatientTreeData();

            this.LoadPatientScheduleData(string.Empty);
        }

        private void CheckDataMethds() {
            PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable templateScheduleTable = this._patientScheduleService.GetPatientScheduleTemplateList(this.rgpBANCI.EditValue.ToString());
            if (templateScheduleTable.Rows.Count <= 0)
                return;
            string tempID = templateScheduleTable.Rows[0][0].ToString();
            PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable patientScheduleDataTable = this._patientScheduleService.GetPatientScheduleList(LoginUser.User.USER_ID, this._beginDate, this._endDate, this.rgpBANCI.EditValue.ToString());
            if (patientScheduleDataTable.Rows.Count <= 0) {
                PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable tempDataScheduleTable = this._patientScheduleService.GetPatientScheduleTempDataList(tempID);

                if (tempDataScheduleTable.Rows.Count <= 0) return;

                DateTime tempData = Convert.ToDateTime(tempDataScheduleTable.Rows[0]["DIALYSIS_DATE"].ToString());

                int difWeek = DifWeek(tempData, this._beginDate);

                var row = tempDataScheduleTable.FirstOrDefault(k => k.DIALYSIS_DATE.Date < this._beginDate.Date);
                if (row != null) {
                    //更新模版表日期为新日期，否则不去更新。。。
                    foreach (var prwo in tempDataScheduleTable) {
                        prwo.DIALYSIS_DATE = prwo.DIALYSIS_DATE.AddDays(7 * difWeek);
                    }
                }
                //去往排班表中写入模版表中数据。。。
                foreach (var prow in tempDataScheduleTable) {
                    var psrow = patientScheduleDataTable.NewMED_PATIENT_SCHEDULERow();
                    psrow.PATIENT_SCHEDULE_ID = Guid.NewGuid().ToString();
                    psrow.PATIENT_ID = ConvertToString(prow.PATIENT_ID);
                    psrow.MONITOR_LABEL = ConvertToString(prow.MONITOR_LABEL);
                    psrow.DIALYSIS_DATE = row == null ? prow.DIALYSIS_DATE.AddDays(7 * difWeek) : prow.DIALYSIS_DATE;
                    psrow.BANCI_ID = ConvertToString(prow.BANCI_ID);
                    psrow.DIALYSIS_ROOM_ID = ConvertToString(prow.DIALYSIS_ROOM_ID);
                    psrow.BED_NUMBER = ConvertToString(prow.BED_NUMBER);
                    psrow.STATUS = ConvertToString(prow.STATUS);
                    psrow.HEMODIALYSIS_ID = ConvertToString(prow.HEMODIALYSIS_ID);
                    psrow.REMARK = ConvertToString(prow.REMARK);
                    psrow.USER_ID = "5d744a87-ca60-40c3-bc7e-1f50a844aa5b";
                    patientScheduleDataTable.AddMED_PATIENT_SCHEDULERow(psrow);
                }
                //保存数据
                this._patientScheduleService.SavePatientScheduleTemplateDataInfo(tempDataScheduleTable);//patientScheduleTemplateDataTable);
                this._patientScheduleService.SavePatientScheduleInfo(patientScheduleDataTable);
                this._patientScheduleService.InSertExecProcLog(Guid.NewGuid().ToString(), GetWeekOfYear(this._beginDate).ToString());
            }

        }

        private int DifWeek(DateTime tempData, DateTime currentData) {
            int tempInt = GetWeekOfYear(tempData);
            int currentInt = GetWeekOfYear(currentData);

            return currentInt - tempInt;

        }
        private int GetWeekOfYear(DateTime dt) {
            //一.找到第一周的最后一天（先获取1月1日是星期几，从而得知第一周周末是几）
            int firstWeekend = 7 - Convert.ToInt32(DateTime.Parse(dt.Year + "-1-1").DayOfWeek);

            //二.获取今天是一年当中的第几天
            int currentDay = dt.DayOfYear;
            //三.（今天 减去 第一周周末）/7 等于 距第一周有多少周 再加上第一周的1 就是今天是今年的第几周了
            //    刚好考虑了惟一的特殊情况就是，今天刚好在第一周内，那么距第一周就是0 再加上第一周的1 最后还是1
            return Convert.ToInt32(Math.Ceiling((currentDay - firstWeekend) / 7.0)) + 1;
        }
        private string ConvertToString(object o) {
            if (o == null)
                return string.Empty;
            if (o == DBNull.Value || o is DBNull)
                return string.Empty;
            return o.ToString();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CtlScheduleMain_Load(object sender, EventArgs e) {
            SchedulePersonDragManager.Instance.InitSchedulePersonControlDict();

            this._groupControls = new GroupControl[] { this.grpMonday, this.grpTuesday, this.grpWednesday, this.grpThursday, this.grpFriday, this.grpSaturday, this.grpSunday };

            this._areaDict = this._userService.GetAreaList(LoginUser.User.USER_ID).OrderByDescending(r => r.ORDER_NUMBER).ToDictionary(r => r, r => 300);
            this._bedDataTable = this._configService.GetConfigList(string.Empty, string.Empty, "床位", "1");
            this._purifierModelDataTable = this._configService.GetConfigList(string.Empty, string.Empty, "净化器类型", "1");
            this._machineDataTable = this._machineService.GetMachineList();
            this._patientDataTable = this._patientService.GetPatientList();
            this._recipeDataTable = this._hemodialysisService.GetAllRecipe();

            this.CreatePatientScheduleControls();

            this.rdoGroup.MouseWheel += new MouseEventHandler(rdoGroup_MouseWheel);
        }

        void rdoGroup_MouseWheel(object sender, MouseEventArgs e) {
            HandledMouseEventArgs h = e as HandledMouseEventArgs;

            if (h != null)
                h.Handled = true;
        }

        /// <summary>
        /// 查询检索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearchPatient_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == 13) {
                string condtion = this.txtSearchPatient.Text.Trim().ToUpper();

                foreach (TreeListNode node in this.treeListPatient.Nodes) {
                    if (node.GetValue("NAME").ToString() == condtion ||
                        (node.GetValue("INPUT_CODE") != null && node.GetValue("INPUT_CODE").ToString() == condtion) ||
                        node.GetValue("HEMODIALYSIS_ID").ToString() == condtion)
                        this.treeListPatient.FocusedNode = node;
                }
            }
        }

        private void treeListPatient_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                TreeListHitInfo hInfo = this.treeListPatient.CalcHitInfo(new Point(e.X, e.Y));

                if (hInfo.HitInfoType == HitInfoType.Cell)
                    this.treeListPatient.DoDragDrop(
                        new SchedulePersonDragInfo() {
                            SourceCtlSchedulePerson = null,
                            PatientRow = this._patientDataTable.FindByHEMODIALYSIS_ID(hInfo.Node[2].ToString())
                        }, DragDropEffects.Copy | DragDropEffects.Move);
            }

            if (e.Button == MouseButtons.Right) {
                contextMenuStrip2.Show(MousePosition);
            }
        }

        public void rgpBANCI_SelectedIndexChanged(object sender, EventArgs e) {
            this.LoadPatientScheduleData(string.Empty);
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnSave_Click(object sender, EventArgs e) 
        {
            DevExpress.XtraBars.ItemClickEventArgs er = e as DevExpress.XtraBars.ItemClickEventArgs;
            if (Convert.ToBoolean(er.Item.Caption.ToString())) {
                if (XtraMessageBox.Show("确定保存为模版信息吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                this.SavePatientScheduleTemplateData(sender.ToString());
                AutoClosedMsgBox.ShowForm("保存模版信息成功！", "提示", 1000, MessageBoxIcon.Warning);
               
            }
            else {

                if (!this.CheckValue())
                     AutoClosedMsgBox.ShowForm("保存排班信息前，必须设置治疗方式和透析器！！", "提示", 1000, MessageBoxIcon.Warning);                 
                else {
                    if (XtraMessageBox.Show("确定保存排班信息吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;

                    this.SavePatientScheduleData();

                    AutoClosedMsgBox.ShowForm("提交成功！", "提示", 1000, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// 保存为模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnSaveTemplate_Click(object sender, EventArgs e) {
            if (XtraMessageBox.Show("确定保存排班信息吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            this.SavePatientScheduleTemplateData(sender.ToString());



            #region 老的保存方法


            //EditTemplate editTemplate = new EditTemplate();

            //if (editTemplate.ShowDialog() == DialogResult.Yes && editTemplate.Tag != null) {
            //    this.SavePatientScheduleTemplateData(editTemplate.Tag.ToString());

            //    XtraMessageBox.Show("保存模板成功！");
            //}
            #endregion
        }

        /// <summary>
        /// 打开模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnOpenTemplate_Click(object sender, EventArgs e) {
            // txtSearchPatient.Text = string.Empty;
            // loadPatients();
            PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable TablerSchedule = this._patientScheduleService.GetPatientScheduleTemplateList(sender.ToString());
            if (TablerSchedule.Rows.Count > 0)
                this.LoadPatientScheduleData(TablerSchedule.Rows[0][0].ToString());

            //TemplateList templateList = new TemplateList(this.rgpBANCI.EditValue.ToString());

            //if (templateList.ShowDialog() == DialogResult.Yes && templateList.Tag != null)
            //    this.LoadPatientScheduleData(templateList.Tag.ToString());
        }

        /// <summary>
        /// 查询病人树数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e) {
            loadPatients();
        }

        private void loadPatients() {
            this.busyIndicator1.ShowLoadingScreenFor(this.treeListPatient);

            using (BackgroundWorker worker = new BackgroundWorker()) {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    if (txtSearchPatient.Text.Length == 0) {
                        this._patientDataTable = this._patientService.GetPatientList();
                    }
                    else {
                        this._patientDataTable = this._patientService.GetPatientListByParams(txtSearchPatient.Text.Trim(), "");
                    }
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.LoadPatientTreeData();
                    this.busyIndicator1.HideLoadingScreen();

                };
                worker.RunWorkerAsync();
            }
        }

        private void rdoGroup_SelectedIndexChanged(object sender, EventArgs e) {
            switch (rdoGroup.SelectedIndex.ToString()) {
                case "0"://全部
                    LoadPatientList("全部");
                    break;
                case "1"://CRRT
                    LoadPatientList("门诊");
                    break;
                case "2"://急诊
                    LoadPatientList("住院");
                    break;
            }
        }

        /// <summary>
        /// 加载病人信息
        /// </summary>
        private void LoadPatientList(string pType) {
            if (pType == "全部") {
                this._patientDataTable = this._patientService.GetPatientList();
            }
            else if (pType == "住院" || pType == "门诊") {
                this._patientDataTable = this._patientService.GetPatientListByType(pType);
            }

            this.LoadPatientTreeData();
        }


        private void treeListPatient_MouseUp(object sender, MouseEventArgs e) {

        }

        private void treeListPatient_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e) {
            if (this.treeListPatient.FocusedNode != null) {
                PatientModel.MED_PATIENTSRow PatientRow = this._patientDataTable.FindByHEMODIALYSIS_ID(this.treeListPatient.FocusedNode.GetValue("HEMODIALYSIS_ID").ToString());
                if (PatientRow != null) {
                    ctlShowGuide1.SetImageStatus(PatientRow);
                }
            }
        }

        private void txtSearchPatient_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                loadPatients();
            }
        }

        private void 添加给药记录ToolStripMenuItem_Click(object sender, EventArgs e) {
            EditPatientNew frmEditPatient = new EditPatientNew();
            frmEditPatient.Current = null;
            frmEditPatient.ShowDialog();
        }

        private void 修改患者ToolStripMenuItem_Click(object sender, EventArgs e) {
            if (this.treeListPatient.FocusedNode != null) {
                PatientModel.MED_PATIENTSRow PatientRow = this._patientDataTable.FindByHEMODIALYSIS_ID(this.treeListPatient.FocusedNode.GetValue("HEMODIALYSIS_ID").ToString());
                EditPatientNew frmEditPatient = new EditPatientNew();
                frmEditPatient.Current = PatientRow;
                frmEditPatient.ShowDialog();
            }
        }

        private void labListRecord_Click(object sender, EventArgs e) {
            if (this.treeListPatient.FocusedNode != null) {
                PatientModel.MED_PATIENTSRow PatientRow = this._patientDataTable.FindByHEMODIALYSIS_ID(this.treeListPatient.FocusedNode.GetValue("HEMODIALYSIS_ID").ToString());
                LabFrm labFrm = new LabFrm(PatientRow);
                labFrm.ShowDialog();
            }
        }

        #endregion
    }
}
