/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:用户控件
 * 创建标识:顾伟伟-2016年11月24日
 * 
 * 修改时间:2017年4月11日
 * 修改人:贺建操
 * 修改描述:修改对外公开的方法
 * 
 * 修改时间:2017年5月13日
 * 修改人:吕志强
 * 修改描述:用户控件
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
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraLayout;
using Hemo.Client.Base;
using Hemo.Client.Base.XtraBaseInfo;
using DevExpress.Data.Filtering;
using DevExpress.Mvvm.POCO;
using System.Reflection;
using Hemo.Service;
using Hemo.IService.Config;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.IService;
using Hemo.IService.PatientSchedule;
using Hemo.Client.Properties;
using Hemo.Client.Core;
using Hemo.Client.UI.Hemodialysis;
using Hemo.Client.UI.Patient;
using Hemo.Client.Base.Services;
using Hemo.Client.UI.Lab;
using DevExpress.Utils.Animation;
using DevExpress.Utils;
using System.Configuration;
using System.Diagnostics;
using Hemo.Client.Modules.Patient;
using Hemo.Client.UI.PatientFixUI;
using DevExpress.XtraBars.Docking2010.Customization;


namespace Hemo.Client.Modules
{
    public partial class PatientMgr : BaseMoudleControl
    {
        #region 变量
        BaseItemCollection hideItemCollection = new BaseItemCollection();

        //public event EventHandler<ConfirmClickEventArgs> ConfirmDoubleClick;
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        public static PatientModel.MED_PATIENTSRow PatientDocRow = null;
        public event EventHandler RowsChange;
        DataTable patientDataTable = null;
        private DateTime BeginDate { set; get; }
        private string SickArea { set; get; }
        private string Banci { set; get; }



        #endregion

        #region 构造函数
        public PatientMgr()
        {
            InitializeComponent();
            InitializeData();
            base.viewModelCore = CreateViewModel<TaskViewModel>();
            base.InitServices();
        }


        #endregion

        #region 方法
        /// <summary>
        /// 加载菜单项
        /// </summary>
        public void InitializeButtonPanel()
        {
            var listBI = new List<ButtonInfo>();
            listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "新增", Name = "1", Image = Hemo.Client.Base.XtraBaseInfo.ImageHelper.GetImageFromToolbarResource("NewBtn"), mouseEventHandler = PatientAddMehtond });
            listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "修改", Name = "2", Image = Hemo.Client.Base.XtraBaseInfo.ImageHelper.GetImageFromToolbarResource("EditBtn"), mouseEventHandler = PatientEditMehtond });
            listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "透析卡管理", Name = "3", Image = Hemo.Client.Base.XtraBaseInfo.ImageHelper.GetImageFromToolbarResource("CardManager"), mouseEventHandler = PatientCardMgrMehtond });
            listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "扫描件", Name = "4", Image = Hemo.Client.Base.XtraBaseInfo.ImageHelper.GetImageFromToolbarResource("UpdateBtn"), mouseEventHandler = PatientDocImageUpdate });
            listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "快捷处方", Name = "5", Image = Hemo.Client.Base.XtraBaseInfo.ImageHelper.GetImageFromToolbarResource("QuekRecipe"), mouseEventHandler = 快捷处方ToolStripMenuItem_Click });
            listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "所有患者", Name = "6", Image = Hemo.Client.Base.XtraBaseInfo.ImageHelper.GetImageFromToolbarResource("AllPatient"), mouseEventHandler = ShowAllPatients });
            listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "患者检验导出", Name = "7", Image = Hemo.Client.Base.XtraBaseInfo.ImageHelper.GetImageFromToolbarResource("AllPatient"), mouseEventHandler = ShowAllPatientsExam });
            listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "患者手术", Name = "8", Image = Hemo.Client.Base.XtraBaseInfo.ImageHelper.GetImageFromToolbarResource("IComb"), mouseEventHandler = PatientOperatorUpdate });
            //listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "事件监测", Name = "8", Image = Hemo.Client.Base.XtraBaseInfo.ImageHelper.GetImageFromToolbarResource("IComb"), mouseEventHandler = EventInfoManager });
            //listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "事件监测2", Name = "8", Image = Hemo.Client.Base.XtraBaseInfo.ImageHelper.GetImageFromToolbarResource("IComb"), mouseEventHandler = EventInfoExtManager });

            //listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "监测日志", Name = "8", Image = Hemo.Client.Base.XtraBaseInfo.ImageHelper.GetImageFromToolbarResource("IComb"), mouseEventHandler = HemoOtherLogManager });

            BottomPanel.InitializeButtons(listBI, false);
        }
        /// <summary>
        /// 事件监测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventInfoExtManager(object sender, EventArgs e)
        {
            //var main = MainFrm.viewModel;
            //(this.FindForm() as MainFrm).IsShowPopupFlyOutPanel(false);

            //main.SelectModule(ModuleType.AllPatientList, (x) =>
            //{
            //    ViewModelHelper.EnsureModuleViewModel(main.SelectedModule, main, null);
            //    ((AllPatientList)main.SelectedModule).InzationControl("EventInfoExtManager");

            //    ((AllPatientList)main.SelectedModule).Refresh();
            //});
        }
        /// <summary>
        /// 事件监测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventInfoManager(object sender, EventArgs e)
        {
            //var main = MainFrm.viewModel;
            //(this.FindForm() as MainFrm).IsShowPopupFlyOutPanel(false);

            //main.SelectModule(ModuleType.AllPatientList, (x) =>
            //{
            //    ViewModelHelper.EnsureModuleViewModel(main.SelectedModule, main, null);
            //    ((AllPatientList)main.SelectedModule).InzationControl("EventInfoManager");

            //    ((AllPatientList)main.SelectedModule).Refresh();
            //});
        }
        /// <summary>
        /// 监测日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HemoOtherLogManager(object sender, EventArgs e)
        {
            //var main = MainFrm.viewModel;
            //(this.FindForm() as MainFrm).IsShowPopupFlyOutPanel(false);

            //main.SelectModule(ModuleType.AllPatientList, (x) =>
            //{
            //    ViewModelHelper.EnsureModuleViewModel(main.SelectedModule, main, null);
            //    ((AllPatientList)main.SelectedModule).InzationControl("HemoOtherLogManager");

            //    ((AllPatientList)main.SelectedModule).Refresh();
            //});
        }
        public TaskViewModel ViewModel
        {
            get { return GetViewModel<TaskViewModel>(); }
        }
        private void MyFaloyoutTest(object sender, EventArgs e)
        {
            ViewModel.ShowExistDocument(ModuleType.EditTask);
        }

        private void tileControl1_ItemClick(object sender, TileItemEventArgs e)
        {
            TileItem item = e.Item;

            var filter = item.Name;

            if (filter == "TliALL")
            {
                this.tileView1.ActiveFilter.Clear();
            }
            else if (filter == "TliHD")
            {
                //  this.tileView1.ActiveFilterCriteria = CriteriaOperator.Parse("PURIFICATION_MODE_NAME=?", HD);
                this.tileView1.ActiveFilterCriteria = CriteriaOperator.Parse("PURIFICATION_MODE_NAME='HD'");
            }
            else if (filter == "TliHDF")
            {
                this.tileView1.ActiveFilterCriteria = CriteriaOperator.Parse("PURIFICATION_MODE_NAME='HDF'");
            }
            else if (filter == "TliCHUANRAN")
            {
                this.tileView1.ActiveFilterCriteria = CriteriaOperator.Parse("INFECTIOUS_CHECK_RESULT!='全阴'");
            }
        }

        protected internal override void OnTransitionCompleted()
        {
            base.OnTransitionCompleted();
            InitializeButtonPanel();
            CalculateWeek();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitList()
        {
            ConfigModel.MED_COMMON_ITEMLISTDataTable config = this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1");
            if (config != null && config.Rows.Count > 0)
            {
                DataRow SickAreaRow = config.NewRow();
                SickAreaRow["ITEM_NAME"] = "全部";
                SickAreaRow["ITEM_ID"] = "c570d95c-76a2-4af4-893a-1357065623bf";
                SickAreaRow["ORDER_NUMBER"] = 0;
                config.Rows.InsertAt(SickAreaRow, 0);
                Hemo.Utilities.Utility.BindLookUpEdit(ediSickArea, "ITEM_ID", "ITEM_NAME", (DataTable)config, "ITEM_NAME", "区域");
            }
            if (config.Rows.Count > 0)
            {
                ediSickArea.EditValue = config[1].ITEM_ID;
            }

            BindBanci(this.ediSickArea.Text);

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
            if (dtRecipeStatus.Rows.Count > 0)
            {
                this.ediRecipe.EditValue = dtRecipeStatus.Rows[0]["ITEM_ID"];

            }
            this.txtStartDate.EditValue = DateTime.Now;
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

            string currentBanci = this.ediBanCi.EditValue != null ? this.ediBanCi.EditValue.ToString() : "1";
            Utility.BindLookUpEdit(this.ediBanCi, "ITEM_VALUE", "ITEM_NAME", dtBanci, "ITEM_NAME", "班次");
            this.ediBanCi.EditValue = areaName.Equals("CRRT") ? "1" : currentBanci;
        }

        /// <summary>
        /// 重新加载 
        /// </summary>
        public void CalculateWeek()
        {
            patientDataTable = new DataTable();
            gridPatientList.DataSource = null;
            // busyIndicator1.Visible = true;
            // busyIndicator1.ShowLoadingScreenFor(this);
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                //生成当日临时处方
                insertPatientTodayRecipe();
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    if (txtStartDate.EditValue != null)
                    {
                        DateTime startDate = Utility.CDate(txtStartDate.EditValue.ToString());
                        string strBanCi = string.Empty;
                        if (ediBanCi.EditValue == null || ediBanCi.EditValue.ToString() == "0")
                        {
                            strBanCi = string.Empty;
                        }
                        else
                        {
                            strBanCi = ediBanCi.EditValue.ToString();
                        }
                        string strSickArea = string.Empty;
                        if (ediSickArea.EditValue == null || ediSickArea.EditValue.ToString() == "c570d95c-76a2-4af4-893a-1357065623bf")
                        {
                            strSickArea = string.Empty;
                        }
                        else
                        {
                            strSickArea = ediSickArea.EditValue.ToString();
                        }

                        if (string.IsNullOrEmpty(this.txtName.Text))
                        {
                            patientDataTable = objPatient.GetPatientListBySchedule(startDate, strSickArea, strBanCi);
                        }
                        else
                        {
                            patientDataTable = objPatient.GetPatientListBySchedule(this.txtName.Text.Trim(), startDate, strSickArea, strBanCi);
                        }
                        BeginDate = startDate;
                        Banci = strBanCi;
                        SickArea = strSickArea;


                        patientDataTable.Columns.Add("PATIENT_HEAD_PORTRAIT", System.Type.GetType("System.Byte[]"));
                        patientDataTable.Columns.Add("PATIENT_TAG", System.Type.GetType("System.Byte[]"));
                        patientDataTable.Columns.Add("STATUS_TAG", System.Type.GetType("System.Byte[]"));
                        patientDataTable.Columns.Add("zhaopian", System.Type.GetType("System.Byte[]"));
                        foreach (DataRow patientRow in patientDataTable.Rows)
                        {
                            if (patientRow["SEX"].ToString() == "男")
                            {
                                patientRow["PATIENT_HEAD_PORTRAIT"] = Utility.BitmapToBytes(Resources.boy);
                            }
                            else
                            {
                                patientRow["PATIENT_HEAD_PORTRAIT"] = Utility.BitmapToBytes(Resources.gril);
                            }
                            if (patientRow["FOCUS_LEVEL"].ToString() == "1")
                            {
                                patientRow["PATIENT_TAG"] = Utility.BitmapToBytes(Resources.重点);
                            }
                            else if (patientRow["FOCUS_LEVEL"].ToString() == "0")
                            {
                                patientRow["PATIENT_TAG"] = Utility.BitmapToBytes(Resources.一般);
                            }
                            else
                            {
                                patientRow["PATIENT_TAG"] = Utility.BitmapToBytes(Resources.关注1);
                            }
                            //有效的一条处方尚未确认
                            if (patientRow["purifier_model_id"].ToString().Length == 0)
                            {
                                patientRow["STATUS_TAG"] = Utility.BitmapToBytes(Resources.处方未确定);
                            }
                            else
                            {
                                patientRow["STATUS_TAG"] = Utility.BitmapToBytes(Resources.处方确定);
                            }
                            if (patientRow["purification_mode_name"].ToString().Length == 0)
                            {
                                patientRow["STATUS_TAG"] = Utility.BitmapToBytes(Resources.处方未开);
                            }

                            var patientPicDt = objPatient.GetPatientPicByHemoId(patientRow["HEMODIALYSIS_ID"].ToString());
                            if (patientPicDt != null && patientPicDt.Rows.Count > 0)
                            {
                                patientRow["zhaopian"] = patientPicDt[0].PAT_PIC;
                            }
                            else
                            {
                                patientRow["zhaopian"] = Utility.BitmapToBytes(Resources.默认显示照片);
                            }
                        }
                    }
                    else
                    {
                        if (txtStartDate.EditValue == null)
                        {
                            XtraMessageBox.Show("请选择病人透析排班日期后，查询病人列表信息。", "病患管理");
                            return;
                        }
                    }
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {

                    if (patientDataTable != null && patientDataTable.Rows.Count > 0)
                    {
                        DataTable dtSub = patientDataTable.Clone();
                        if (this.ediRecipe.EditValue == "1")
                        {
                            patientDataTable.AsEnumerable().CopyToDataTable(dtSub, LoadOption.PreserveChanges);
                        }
                        else if (this.ediRecipe.EditValue == "2")
                        {
                            patientDataTable.AsEnumerable().Where(row => row["STATUS_TAG"].Equals(Utility.BitmapToBytes(Resources.处方未确定))).CopyToDataTable(dtSub, LoadOption.PreserveChanges);
                        }
                        else if (this.ediRecipe.EditValue == "3")
                        {
                            patientDataTable.AsEnumerable().Where(row => row["STATUS_TAG"].Equals(Utility.BitmapToBytes(Resources.处方确定))).CopyToDataTable(dtSub, LoadOption.PreserveChanges);
                        }
                        else if (this.ediRecipe.EditValue == "4")
                        {
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
                        string TCOUNT = patientDataTable.Rows.Count.ToString();
                        string CHUANRAN = patientDataTable.Select("INFECTIOUS_CHECK_RESULT <> '全阴'").Length.ToString();

                        TliALL.Text = TCOUNT;
                        TliHD.Text = HD;
                        TliHDF.Text = HDF;
                        TliCHUANRAN.Text = CHUANRAN;
                        //this.lb_count.Text = string.Format("当前班次.病区的总排班人数为:<Color=blue> {0} </Color=blue>人 HD:<Color=blue> {1} </Color=blue>人 HDF:<Color=blue> {2} </Color=blue>人 HF:<Color=blue> {3} </Color=blue>人 HP:<Color=blue> {4} </Color=blue>人 HD+HP:<Color=blue> {5} </Color=blue>人 CRRT:<Color=blue> {6} </Color=blue>人 PE:<Color=blue> {7} </Color=blue>人", patientDataTable.Rows.Count.ToString(), HD, HDF, HF, HP, HDHP, CRRT, PE);
                    }
                    else
                    {
                        gridPatientList.DataSource = null;
                    }
                    //this.busyIndicator1.HideLoadingScreen();
                };
                worker.RunWorkerAsync();
            }
        }
        /// <summary>
        /// 根据当前患者排班患者插入当天的处方数据
        /// </summary>
        private void insertPatientTodayRecipe()
        {
            int result = _hemodialysisService.CreatePatientRecipeBydate(Utility.CDate(this.txtStartDate.EditValue.ToString()));
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            ConfirmOrCancelPatientList(true);
        }

        /// <summary>
        /// 确认或取消病人处方列表
        /// </summary>
        /// <param name="pStatus">1=确认 0=取消</param>

        private void ConfirmOrCancelPatientList(bool pStatus)
        {

            string strStautsName = string.Empty;
            if (pStatus)
            {
                strStautsName = "确认";
            }
            else
            {
                strStautsName = "取消";
            }

            if (tileView1.RowCount == 0)
            {


                AutoClosedMsgBox.ShowForm("请先安排病人透析排班信息，再" + strStautsName + "处方信息。", "病患管理", 1000, MessageBoxIcon.Warning);
                return;
            }
            if (txtStartDate.EditValue == null)
            {
                AutoClosedMsgBox.ShowForm("请先安排病人透析排班信息，再" + strStautsName + "处方信息。", "病患管理", 1000, MessageBoxIcon.Warning);

                return;
            }

            if (XtraMessageBox.Show("确定" + strStautsName + "当前病人列表透析处方吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //确认确认的处方信息，更新排班表数据
            string filter = this.tileView1.ActiveFilterString;
            DataTable dt = gridPatientList.DataSource as DataTable;
            if (filter != null && filter.Length > 0)
            {
                dt = Utility.GetSubTable(dt, filter);
            }
            string HemoID = string.Empty;
            DataTable dtTemp = new DataTable();
            if (dt != null && dt.Rows.Count > 0)
            {

                #region 添加验证传染病与对应透析室关系的逻辑
                string strRoom = string.Empty;
                string strInf = string.Empty;
                string strResult = string.Empty;
                int infCount = 0;
                int intBCount = 0;
                if (ediSickArea != null)
                {
                    strRoom = ediSickArea.Text;
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["INFECTIOUS_CHECK_RESULT"] != null)
                    {
                        strInf = dt.Rows[i]["INFECTIOUS_CHECK_RESULT"].ToString();
                        if (strInf.Contains("乙肝") || strInf.Contains("乙型肝炎"))
                        {
                            infCount += 1;
                        }
                        if (strInf.Contains("丙肝") || strInf.Contains("丙型肝炎"))
                        {
                            intBCount += 1;
                        }
                    }
                }

                var rooms = _configService.GetConfigList("", "", "隔离病区", "1");
                if (rooms.Count(wh => wh.ITEM_VALUE == strRoom) > 0)//strRoom == "第五透析室"
                {
                    if (infCount < dt.Rows.Count)
                    {

                        strResult = "该患者是非传染性疾病患者，是否" + strStautsName + "在隔离病区治疗？";
                        if (XtraMessageBox.Show(strResult, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;
                    }
                }

                #endregion

                DateTime startDate = Utility.CDate(Utility.CDate(txtStartDate.EditValue.ToString()).ToShortDateString());
                string strBanCi = string.Empty;
                if (ediBanCi.EditValue == null || ediBanCi.EditValue.ToString() == "0")
                {
                    strBanCi = string.Empty;
                }
                else
                {
                    strBanCi = ediBanCi.EditValue.ToString();
                }
                string strSickArea = string.Empty;
                if (ediSickArea.EditValue == null || ediSickArea.EditValue.ToString() == "c570d95c-76a2-4af4-893a-1357065623bf")
                {
                    strSickArea = string.Empty;
                }
                else
                {
                    strSickArea = ediSickArea.EditValue.ToString();
                }
                BeginDate = startDate;
                Banci = strBanCi;
                SickArea = strSickArea;
                PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable patientScheduleDataTable = _patientScheduleService.GetPatientScheduleByParames(startDate, startDate, strBanCi, strSickArea);

                //筛选需要确认处方或者取消处方的病人 add by Pagi.Liu at 2017.2.28
                int index = 0;
                foreach (PatientScheduleModel.MED_PATIENT_SCHEDULERow thisrow in patientScheduleDataTable)
                {
                    int jk = 0;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["HEMODIALYSIS_ID"].ToString() == thisrow.HEMODIALYSIS_ID.ToString())
                        {
                            jk = 1;
                            break;
                        }
                        else
                        { jk = 0; continue; }
                    }
                    if (jk == 0)
                    {
                        patientScheduleDataTable.Rows[index].Delete();
                    }
                    index++;

                }
                patientScheduleDataTable.AcceptChanges();



                //因后加入阳性患者等筛选条件，所以对应排班的患者也需要筛选               
                StringBuilder sbRecipeID = new StringBuilder();

                #region 是否加入质控校验相关判断
                //读取系统质控校验选项
                DataTable dtQualityCheck = this._configService.GetConfigList(string.Empty, string.Empty, "质控校验", "1");
                DataTable todayWeight = new DataTable();
                DataTable todayUfR = new DataTable();
                DataTable diagnose = new DataTable();

                if (dtQualityCheck != null && dtQualityCheck.Rows.Count > 0)
                {
                    //读取系统是否需要验证透前体重设置
                    todayWeight = Utility.GetSubTable(dtQualityCheck, "item_name='透前体重' and status='1'");
                    //读取系统是否需要验证诊断设置
                    diagnose = Utility.GetSubTable(dtQualityCheck, "item_name='诊断结果' and status='1'");
                    //读取系统是否需要验证预计脱水设置
                    todayUfR = Utility.GetSubTable(dtQualityCheck, "item_name='预计脱水' and status='1'");
                }


                #endregion

                string pName = string.Empty;
                foreach (PatientScheduleModel.MED_PATIENT_SCHEDULERow rowSchedule in patientScheduleDataTable.Rows)
                {


                    HemoID = rowSchedule["HEMODIALYSIS_ID"].ToString();
                    pName = rowSchedule["NAME"].ToString();
                    //rowSchedule.PATIENTNAME
                    // 2014-03-05 刘超 修改将处方日期通过窗体传入，之前使用的为默认值sysdate，只传入透析号。
                    HemodialysisModel.MED_HEMO_RECIPEDataTable recipeTable = this._hemodialysisService.GetRecipeByHemodialysisIDAndDate(HemoID, startDate);
                    // dtTemp = Utility.GetSubTable(recipeTable, "TRUNC(recipe_date) = TRUNC(" + txtStartDate.EditValue.ToString() + ")") as HemodialysisModel.MED_HEMO_RECIPEDataTable;

                    if (recipeTable != null && recipeTable.Rows.Count > 0)
                    {
                        #region 判断预计脱水、透前体重、诊断是否录入，没有录入强制用户录入否则不能确认处方,达到质控要求。
                        if (diagnose != null && diagnose.Rows.Count > 0)
                        {
                            if (rowSchedule["DIAGNOSE"].ToString().Length == 0)
                            {
                                XtraMessageBox.Show("患者【" + pName + "】的诊断未录入，不能确定透析处方。", "病患管理", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                return;
                            }
                        }

                        if (dtQualityCheck != null && dtQualityCheck.Rows.Count > 0)
                        {
                            if (todayWeight != null && todayWeight.Rows.Count > 0)
                            {
                                if (recipeTable.Rows[0]["TODAY_WEIGHT"].ToString().Length == 0 || recipeTable.Rows[0]["TODAY_WEIGHT"].ToString() == "0")
                                {
                                    XtraMessageBox.Show("患者【" + pName + "】的透前体重未测量，不能确定透析处方。", "病患管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }

                            if (todayUfR != null && todayUfR.Rows.Count > 0)
                            {
                                if (recipeTable.Rows[0]["UFR"].ToString().Length == 0 || recipeTable.Rows[0]["UFR"].ToString() == "0")
                                {
                                    XtraMessageBox.Show("患者【" + pName + "】的当日预计脱水量未确定，不能确定透析处方。", "病患管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                        #endregion

                        sbRecipeID.Append("'").Append(recipeTable.Rows[0]["RECIPE_ID"].ToString()).Append("',");
                        HemoDWHApplication hemodwApp = new HemoDWHApplication();
                        if (pStatus)
                        {
                            if (!string.IsNullOrEmpty(rowSchedule["RECIPE_ID"].ToString()))
                            {
                                continue;
                            }
                            rowSchedule["RECIPE_ID"] = recipeTable.Rows[0]["RECIPE_ID"].ToString();
                            rowSchedule["PURIFIER_MODEL_ID"] = recipeTable.Rows[0]["FIRST_PURIFIER_MODEL"].ToString();
                            rowSchedule["PURIFICATION_MODE"] = recipeTable.Rows[0]["PURIFICATION_MODE"].ToString();

                            //进行出库申请
                            hemodwApp.ConfirmHemoDWApply(HemoID, recipeTable.Rows[0]["RECIPE_ID"].ToString(), recipeTable.Rows[0]["PURIFICATION_MODE"].ToString());
                        }
                        else
                        {
                            if (!rowSchedule.IsSTART_TIMENull())
                                continue;
                            rowSchedule["RECIPE_ID"] = string.Empty;
                            rowSchedule["PURIFIER_MODEL_ID"] = string.Empty;
                            hemodwApp.CancleConfirHemoDwApply(recipeTable.Rows[0]["RECIPE_ID"].ToString());

                        }
                    }
                    else
                    {
                        AutoClosedMsgBox.ShowForm("患者【" + pName + "】的临时透析处方尚未创建,请先创建临时透析处方。", "病患管理", 1000, MessageBoxIcon.Warning);
                        return;
                    }
                }


                int result = _patientScheduleService.SavePatientScheduleInfo(patientScheduleDataTable);

                //更新处方的开放医生为当前登录的医生账号，根据当前排班病人列表

                string strRecipeIDList = string.Empty;
                if (sbRecipeID.ToString().Length > 0)
                {
                    strRecipeIDList = sbRecipeID.ToString().Substring(0, sbRecipeID.ToString().Length - 1);
                }
                result = _hemodialysisService.SaveRecipeUserIDByRecipeIDList(strRecipeIDList, HemoApplicationContext.Current.CurrentUser.EMP_NO);
                if (result > 0)
                {
                    AutoClosedMsgBox.ShowForm("当前病人列表中处方信息已经" + strStautsName + "。", "病患管理", 1000, MessageBoxIcon.Warning);

                    CalculateWeek();
                }
            }
        }


        private string CheckArea(string strRoom, string strInf)
        {
            var rooms = _configService.GetConfigList("", "", "隔离病区", "1");
            if (rooms.Count(wh => wh.ITEM_VALUE == strRoom) > 0)//strRoom == "第五透析室"
            {
                if (strInf == "全阴" || string.IsNullOrEmpty(strInf))
                {
                    return "该病人没有标记为【乙肝或丙肝】,是否确认在隔离区域治疗？";

                }
            }
            return "";

        }
        /// <summary>
        /// 取消确认
        /// </summary>
        /// <param name="pStatus"></param>
        private void ConfirmOrCancelOnePatient(bool pStatus)
        {

            DataRow row = this.tileView1.GetFocusedDataRow();
            string strStautsName = string.Empty;
            string strRoom = string.Empty;
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

            if (ediSickArea != null)
            {
                strRoom = ediSickArea.Text;
            }

            if (row["INFECTIOUS_CHECK_RESULT"] != null)
            {
                strInf = row["INFECTIOUS_CHECK_RESULT"].ToString();
            }
            strResult = CheckArea(strRoom, strInf).Trim();
            if (strResult != "")
            {
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

            if (dtQualityCheck != null && dtQualityCheck.Rows.Count > 0)
            {
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
            if (row != null)
            {
                DateTime startDate = Utility.CDate(txtStartDate.EditValue.ToString());
                PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable dataTableSchedule = _patientScheduleService.GetPatientScheduleSignle(startDate, row["HEMODIALYSIS_ID"].ToString());


                if (dataTableSchedule != null && dataTableSchedule.Rows.Count > 0)
                {
                    PatientScheduleModel.MED_PATIENT_SCHEDULERow drSchedule = dataTableSchedule.Rows[0] as PatientScheduleModel.MED_PATIENT_SCHEDULERow;
                    //2014-03-05 刘超 修改将处方日期通过窗体传入，之前使用的为默认值sysdate，只传入透析号。
                    //HemodialysisModel.MED_HEMO_RECIPEDataTable recipeTable = this._hemodialysisService.GetRecipeByHemodialysisID(row["HEMODIALYSIS_ID"].ToString());
                    HemodialysisModel.MED_HEMO_RECIPEDataTable recipeTable = this._hemodialysisService.GetRecipeByHemodialysisIDAndDate(row["HEMODIALYSIS_ID"].ToString(), startDate);
                    string strRecipeIDList = string.Empty;
                    string pName = drSchedule.PATIENTNAME;
                    if (recipeTable != null && recipeTable.Rows.Count > 0)
                    {



                        #region 判断预计脱水、透前体重、诊断是否录入，没有录入强制用户录入否则不能确认处方,达到质控要求。
                        if (diagnose != null && diagnose.Rows.Count > 0)
                        {
                            if (drSchedule["DIAGNOSE"].ToString().Length == 0)
                            {
                                AutoClosedMsgBox.ShowForm("患者【" + pName + "】的诊断未录入，不能确定透析处方。", "病患管理", 2000, MessageBoxIcon.Information);
                                return;
                            }
                        }

                        if (dtQualityCheck != null && dtQualityCheck.Rows.Count > 0)
                        {
                            if (todayWeight != null && todayWeight.Rows.Count > 0)
                            {
                                if (recipeTable.Rows[0]["TODAY_WEIGHT"].ToString().Length == 0 || recipeTable.Rows[0]["TODAY_WEIGHT"].ToString() == "0")
                                {
                                    AutoClosedMsgBox.ShowForm("患者【" + pName + "】的透前体重未测量，不能确定透析处方。", "病患管理", 2000, MessageBoxIcon.Information);
                                    return;
                                }
                            }

                            if (todayUfR != null && todayUfR.Rows.Count > 0)
                            {
                                if (recipeTable.Rows[0]["UFR"].ToString().Length == 0 || recipeTable.Rows[0]["UFR"].ToString() == "0")
                                {
                                    AutoClosedMsgBox.ShowForm("患者【" + pName + "】的当日预计脱水量未确定，不能确定透析处方。", "病患管理", 2000, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                        #endregion

                        strRecipeIDList = "'" + recipeTable.Rows[0]["RECIPE_ID"].ToString() + "'";
                        HemoDWHApplication hemodwApp = new HemoDWHApplication();

                        if (pStatus)
                        {
                            if (drSchedule.IsRECIPE_IDNull() ? false : !string.IsNullOrEmpty(drSchedule.RECIPE_ID))
                            {
                                AutoClosedMsgBox.ShowForm("该病人处方已进行过确认!", "病患管理", 2000, MessageBoxIcon.Warning);
                                return;
                            }
                            drSchedule.RECIPE_ID = recipeTable.Rows[0]["RECIPE_ID"].ToString();
                            drSchedule.PURIFIER_MODEL_ID = recipeTable.Rows[0]["FIRST_PURIFIER_MODEL"].ToString();
                            drSchedule.PURIFICATION_MODE = recipeTable.Rows[0]["PURIFICATION_MODE"].ToString();
                            //进行出库申请
                            hemodwApp.ConfirmHemoDWApply(recipeTable.Rows[0]["HEMODIALYSIS_ID"].ToString(), recipeTable.Rows[0]["RECIPE_ID"].ToString(), recipeTable.Rows[0]["PURIFICATION_MODE"].ToString());
                        }
                        else
                        {
                            drSchedule.RECIPE_ID = string.Empty;
                            drSchedule.PURIFIER_MODEL_ID = string.Empty;
                            hemodwApp.CancleConfirHemoDwApply(recipeTable.Rows[0]["RECIPE_ID"].ToString());

                        }
                    }
                    else
                    {
                        AutoClosedMsgBox.ShowForm("病人透析ID:" + row["HEMODIALYSIS_ID"].ToString() + "的临时处方尚未创建,请先创建临时处方。", "病患管理", 2000, MessageBoxIcon.Warning);
                        return;
                    }

                    //根据处方编号取透析单号，判断患者该处方是否已经执行，如果已经执行不能取消。
                    HemodialysisModel.MED_CURE_MAINDataTable dtCure = this._hemodialysisService.GetMainCureByRecipeId(recipeTable.Rows[0]["RECIPE_ID"].ToString());
                    if (dtCure != null && dtCure.Rows.Count > 0 && strStautsName == "取消")
                    {
                        AutoClosedMsgBox.ShowForm("该病人处方已执行不能取消!", "病患管理", 2000, MessageBoxIcon.Warning);
                        return;
                    }
                    //更新临时医嘱记录处方编号
                    HemodialysisModel.MED_CURE_DRUGDataTable drugDt = _hemodialysisService.GetValidCureDrugByHemoID(row["HEMODIALYSIS_ID"].ToString(), this.txtStartDate.DateTime);
                    if (drugDt != null && drugDt.Rows.Count > 0)
                    {
                        drugDt.AsEnumerable().ToList().ForEach(drug =>
                        {
                            drug.RECIPE_ID = string.IsNullOrEmpty(drug.RECIPE_ID) ? strRecipeIDList.Replace("'", "") : drug.RECIPE_ID;
                        });
                        _hemodialysisService.SaveCureDrug(drugDt);
                    }
                    result = _patientScheduleService.SavePatientScheduleInfo(dataTableSchedule);
                    result = _hemodialysisService.SaveRecipeUserIDByRecipeIDList(strRecipeIDList, HemoApplicationContext.Current.CurrentUser.EMP_NO);
                    if (result > 0)
                    {
                        AutoClosedMsgBox.ShowForm("该病人处方已" + strStautsName + "。", "病患管理", 2000, MessageBoxIcon.Warning);
                        CalculateWeek();
                    }
                }
            }
            else
            {
                AutoClosedMsgBox.ShowForm("请先选择一个病人！", "病患管理", 2000, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <param name="confirmRow"></param>
        /// <returns></returns>
        private PatientModel.MED_PATIENTSRow GetPatientRow(DataRow confirmRow)
        {
            PatientModel.MED_PATIENTSDataTable dtt = new PatientModel.MED_PATIENTSDataTable();
            PatientModel.MED_PATIENTSRow row = dtt.NewMED_PATIENTSRow();
            row.HEMODIALYSIS_ID = confirmRow["HEMODIALYSIS_ID"].ToString();
            row.PATIENT_ID = confirmRow["PATIENT_ID"].ToString();
            row.NAME = confirmRow["NAME"].ToString();
            row.SEX = confirmRow["SEX"].ToString();
            if (!string.IsNullOrEmpty(confirmRow["BIRTHDAY"].ToString()))
                row.BIRTHDAY = Convert.ToDateTime(confirmRow["BIRTHDAY"].ToString());
            if (!string.IsNullOrEmpty(confirmRow["AGE"].ToString()))
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
            row.ROOMID = confirmRow["AREA_ID"].ToString();
            row.UPSTATE = confirmRow["BANCI_ID"].ToString();

            row.BED_NO = confirmRow["BED_NO"].ToString();
            return row;
        }
        /// <summary>
        /// 病人扫描件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientDocImageUpdate(object sender, EventArgs e)
        {
            DataRow dr = this.tileView1.GetFocusedDataRow();
            if (dr != null)
            {
                PatientDocRow = GetPatientRow(dr);
                //根据ModultType获取到ModuleView，然后传入参数以供后边使用
                ((PatientDocImageMgr)MainFrm.viewModel.GetModule(ModuleType.PatientDocImageMgr)).LoadPatientInfoMehtno(PatientDocRow);
                //显示模态窗口
                ViewModel.ShowExistDocument(ModuleType.PatientDocImageMgr);


            }
            else
            {
                XtraMessageBox.Show("请先选择一个病人,然后上传电子扫描件！", "病患管理");
            }
        }

        /// <summary>
        /// 病人基本信息，新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientAddMehtond(object sender, EventArgs e)
        {
            ShowEditPatient(null);

            //using (AddNewPatientInfos newPatient = new AddNewPatientInfos())
            //{
            //    DialogResult result = newPatient.ShowDialog();
            //}
        }
        /// <summary>
        /// 病人基本信息，编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientEditMehtond(object sender, EventArgs e)
        {
            DataRow dr = this.tileView1.GetFocusedDataRow();
            if (dr != null)
            {
                PatientDocRow = GetPatientRow(dr);
                ShowEditPatient(PatientDocRow);
            }
        }
        /// <summary>
        /// 透析卡管理MainModule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientCardMgrMehtond(object sender, EventArgs e)
        {
            DataRow dr = this.tileView1.GetFocusedDataRow();
            if (dr != null)
            {
                PatientDocRow = GetPatientRow(dr);
                ShowPatientCardMgr(PatientDocRow);
            }
        }
        /// <summary>
        /// 新增与编辑患者事件与参数
        /// </summary>
        /// <param name="patientsRow">传入参数</param>
        private void ShowEditPatient(PatientModel.MED_PATIENTSRow patientsRow)
        {
            //根据ModultType获取到ModuleView，然后传入参数以供后边使用
            ((PatientEdit)MainFrm.viewModel.GetModule(ModuleType.PatientEdit)).LoadPatientInfoMehtno(patientsRow);
            //显示模态窗口
            ViewModel.ShowExistDocument(ModuleType.PatientEdit);

            CalculateWeek();


            #region Mian显示去掉，换成上面的弹框显示

            //var main = MainFrm.viewModel;
            //(this.FindForm() as MainFrm).IsShowPopupFlyOutPanel(false);


            //main.SelectModule(ModuleType.PatientEdit, (x) =>
            //{
            //    ViewModelHelper.EnsureModuleViewModel(main.SelectedModule, main, null);

            //    ((PatientEdit)main.SelectedModule).LoadPatientInfoMehtno(patientsRow);
            //    ((PatientEdit)main.SelectedModule).Refresh();
            //});

            #endregion

        }
        /// <summary>
        /// 透析卡管理
        /// </summary>
        /// <param name="patientsRow"></param>
        private void ShowPatientCardMgr(PatientModel.MED_PATIENTSRow patientsRow)
        {
            var main = MainFrm.viewModel;
            (this.FindForm() as MainFrm).IsShowPopupFlyOutPanel(false);


            main.SelectModule(ModuleType.PatientCardMgr, (x) =>
            {
                ViewModelHelper.EnsureModuleViewModel(main.SelectedModule, main, null);
                ((PatientCardMgr)main.SelectedModule).LoadPatientInfoMehtno(patientsRow);
                ((PatientCardMgr)main.SelectedModule).Refresh();
            });
        }
        /// <summary>
        /// 显示全部病人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowAllPatients(object sender, EventArgs e)
        {


            var main = MainFrm.viewModel;
            (this.FindForm() as MainFrm).IsShowPopupFlyOutPanel(false);

            main.SelectModule(ModuleType.AllPatientList, (x) =>
            {
                ViewModelHelper.EnsureModuleViewModel(main.SelectedModule, main, null);
                ((AllPatientList)main.SelectedModule).InzationControl("ALLPATIENT");

                ((AllPatientList)main.SelectedModule).Refresh();
            });
        }
        /// <summary>
        /// 患者手术
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientOperatorUpdate(object sender, EventArgs e)
        {
            var main = MainFrm.viewModel;
            (this.FindForm() as MainFrm).IsShowPopupFlyOutPanel(false);

            main.SelectModule(ModuleType.AllPatientList, (x) =>
            {
                ViewModelHelper.EnsureModuleViewModel(main.SelectedModule, main, null);
                ((AllPatientList)main.SelectedModule).InzationControl("PATIENTOPERATOR");

                ((AllPatientList)main.SelectedModule).Refresh();
            });
        }
        /// <summary>
        /// 患者检验导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ShowAllPatientsExam(object sender, EventArgs e)
        {
            //DateTime startTime = Utility.GetMonday(DateTime.Now).AddDays(0).Date;
            //DateTime endTime = startTime.AddDays(6).Date;
            //string strBanci = "1";
            //if (this.ediBanCi.EditValue != null)
            //{
            //    strBanci = this.ediBanCi.EditValue.ToString();
            //}
            //SchedulePatientLabReportNew frm = new SchedulePatientLabReportNew(startTime, endTime, strBanci);
            //frm.ShowDialog();


            var main = MainFrm.viewModel;
            (this.FindForm() as MainFrm).IsShowPopupFlyOutPanel(false);

            main.SelectModule(ModuleType.AllPatientList, (x) =>
            {
                ViewModelHelper.EnsureModuleViewModel(main.SelectedModule, main, null);
                ((AllPatientList)main.SelectedModule).InzationControl("AllPatientLabRpt");

                ((AllPatientList)main.SelectedModule).Refresh();
            });

        }

        #endregion

        #region 事件

        /// <summary>
        /// 如果有模态框方式进行显示时必须注册显示UI的ModuleType，有多少注册多少。
        /// </summary>
        /// <param name="serviceContainer"></param>
        protected override void OnInitServices(DevExpress.Mvvm.IServiceContainer serviceContainer)
        {
            base.OnInitServices(serviceContainer);
            serviceContainer.RegisterService(new FlyoutDetailFormDocumentManagerService(new ModuleType[] { ModuleType.PatientEdit, ModuleType.PatientDocImageMgr }));
        }

        void InitializeData()
        {
            hideItemCollection.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1 });
            InitList();
        }
        /// <summary>
        /// 左侧边显示隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleLabelItem2_MouseDown(object sender, MouseEventArgs e)
        {


            //Transition transiton = new Transition();
            //transiton.ShowWaitingIndicator = DefaultBoolean.False;
            //transiton.Control = this;
            //transiton.TransitionType = new FadeTransition();
            //TransitionManager manager = new TransitionManager();
            //manager.Transitions.Add(transiton);
            //this.layoutControl1.Parent = this;
            //Random r = new Random();
            //manager.StartTransition(this);

            if (layoutControlItem1.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                ItemsHideHelper.Hide(hideItemCollection, buttonHide);

                //return;
            }
            else if (layoutControlItem1.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            {
                ItemsHideHelper.Expand(hideItemCollection, buttonHide);

                //return;
            }
            // manager.EndTransition();


        }

        private void PatientMgr_Load(object sender, EventArgs e)
        {
            CalculateWeek();
            InitializeButtonPanel();
            RowsChange += PatientDetail.PdInstance().Pget;
        }

        private void gridControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip1.Show(MousePosition);
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            CalculateWeek();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ConfirmOrCancelPatientList(false);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ConfirmOrCancelOnePatient(true);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ConfirmOrCancelOnePatient(false);
        }
        /// <summary>
        /// 双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tileView1_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = this.tileView1.GetFocusedDataRow();
            PatientDocRow = GetPatientRow(dr);
            if (dr != null)
            {
                if (RowsChange != null)
                {
                    RowsChange(this, new MyEventArgs() { Get = PatientDocRow, BeginDate = BeginDate, Banci = Banci, SickArea = SickArea, AreaName = this.ediSickArea.Text });
                    var main = MainFrm.viewModel;
                    BottomPanel.Visible = false;
                    main.SelectModule(ModuleType.PatientDetail, (x) =>
                    {
                        ViewModelHelper.EnsureModuleViewModel(main.SelectedModule, main, null);
                        ((PatientDetail)main.SelectedModule).InitializeButtonPanel();
                        ((PatientDetail)main.SelectedModule).Refresh();
                    });
                }
            }
        }

        private void 快捷处方ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow dr = this.tileView1.GetFocusedDataRow();

            //FastRecipeListNew frm = new FastRecipeListNew();
            //frm.currentDt = this.txtStartDate.DateTime.Date;
            //frm._currentPatientRow = GetPatientRow(dr);
            ////frm.InitalizeData();
            //FlyoutDialog.Show(this.FindForm(), frm);




            var main = MainFrm.viewModel;
            (this.FindForm() as MainFrm).IsShowPopupFlyOutPanel(false);

            main.SelectModule(ModuleType.AllPatientList, (x) =>
            {
                ViewModelHelper.EnsureModuleViewModel(main.SelectedModule, main, null);
                if (dr != null)
                    ((AllPatientList)main.SelectedModule).currentPatientsRow = GetPatientRow(dr);
                ((AllPatientList)main.SelectedModule).currentCureDate = this.txtStartDate.DateTime.Date;
                ((AllPatientList)main.SelectedModule).InzationControl("FASTRECIPELISTMODLES");

                ((AllPatientList)main.SelectedModule).Refresh();
            });


        }


        private void tileView1_ItemCustomize(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemCustomizeEventArgs e)
        {
            if (e.Item == null || e.Item.Elements.Count == 0)
                return;
            string str = tileView1.GetRowCellValue(e.RowHandle, tileView1.Columns["INFECTIOUS_CHECK_RESULT"]).ToString();
            if (str.ToString().Length > 0)
            {
                e.Item.Elements[8].Appearance.Normal.ForeColor = Color.Red;
            }
        }

        private void tileView1_ItemClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e)
        {
            //  e.Item.Elements[8].Appearance.Normal.BackColor = Color.Red;
        }
        /// <summary>
        /// 检验数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            var dr = this.tileView1.GetFocusedDataRow();
            if (dr != null)
            {
                var patientRow = GetPatientRow(this.tileView1.GetFocusedDataRow());
                if (patientRow != null)
                {
                    LabFrm labFrm = new LabFrm(patientRow);
                    labFrm.ShowDialog();
                }

            }

        }
        /// <summary>
        /// 检查数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            var dr = this.tileView1.GetFocusedDataRow();
            if (dr != null)
            {
                var patientRow = GetPatientRow(this.tileView1.GetFocusedDataRow());
                if (patientRow != null)
                {
                    using (ExamFrm examFrm = new ExamFrm())
                    {
                        examFrm.PatientRow = patientRow;
                        examFrm.ShowDialog();
                    }
                }
            }
        }

        private void 简易处方ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dr = this.tileView1.GetFocusedDataRow();
            if (dr != null)
            {
                using (var frm = new EditUFR(dr["HEMODIALYSIS_ID"].ToString(), Utility.CDate(this.txtStartDate.EditValue.ToString())))
                {
                    if (frm.IsShowFrm)
                    {
                        frm.AreaName = this.ediSickArea.Text;
                        frm.CurrentPatient = dr;
                        DialogResult result = frm.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CalculateWeek();
                        }
                    }
                }
            }
        }

        private void 历次就诊ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var row = this.tileView1.GetFocusedDataRow();
            if (row != null)
            {
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

        private void cRRT快捷处方ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (CRRTFastRecipeList fastRecipe = new CRRTFastRecipeList())
            {
                DialogResult result = fastRecipe.ShowDialog();
                CalculateWeek();
            }
        }

        /// <summary>
        /// 病区改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ediSickArea_EditValueChanged(object sender, EventArgs e)
        {
            BindBanci(this.ediSickArea.Text);
        }

        #endregion

        private void 创建处方ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dr = this.tileView1.GetFocusedDataRow();
            if (dr != null)
            {
                using (var frm = new EditPrescribe(dr["HEMODIALYSIS_ID"].ToString(), ""))
                {
                    frm.ShowDialog();
                }
            }
        }

        private void 临时医嘱ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow dr = this.tileView1.GetFocusedDataRow();
            PatientDocRow = GetPatientRow(dr);
            if (dr != null)
            {
                if (RowsChange != null)
                {
                    RowsChange(this, new MyEventArgs() { Get = PatientDocRow, BeginDate = BeginDate, Banci = Banci, SickArea = SickArea, AreaName = this.ediSickArea.Text, CurrentUI = ModuleType.TempDrug });
                    var main = MainFrm.viewModel;
                    BottomPanel.Visible = false;
                    main.SelectModule(ModuleType.PatientDetail, (x) =>
                    {
                        ViewModelHelper.EnsureModuleViewModel(main.SelectedModule, main, null);
                        ((PatientDetail)main.SelectedModule).InitializeButtonPanel();
                        ((PatientDetail)main.SelectedModule).Refresh();
                    });
                }
            }
        }
    }

    #region 事件参数
    /// <summary>
    /// 事件参数
    /// </summary>
    public class MyEventArgs : EventArgs
    {
        public PatientModel.MED_PATIENTSRow Get
        {
            set;
            get;
        }

        public DateTime BeginDate
        {
            set;
            get;
        }
        public string Banci
        {
            set;
            get;
        }
        public string SickArea
        {
            set;
            get;
        }
        public string AreaName
        {
            set;
            get;
        }
        public ModuleType CurrentUI
        {
            set;
            get;
        }
    }


    #endregion
}


