/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:用户控件
 * 创建标识:刘超-2017年1月18日
 * 
 * 修改时间:2017年6月5日
 * 修改人:顾伟伟
 * 修改描述:修改对外公开的方法
 * 
 * 修改时间:2017年7月7日
 * 修改人:吕志强
 * 修改描述:修复系统响应速度慢的问题
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
using DevExpress.XtraBars.Docking;
using DevExpress.XtraNavBar;
using DevExpress.XtraBars.Navigation;
using Hemo.Model;
using Hemo.IService;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Client.Modules.PatientsMgr;
using Hemo.Client.UI.PatientFixUI;
using Hemo.Utilities;
using Hemo.Client.Properties;
using Hemo.IService.PatientSchedule;
using Hemo.Client.Core;
using Hemo.Client.UI.Hemodialysis;
using Hemo.Client.Controls.Common;
using Hemo.IService.Machine;
using Hemo.Client.UI.Lab;
using Hemo.Client.Base.XtraBaseInfo;

namespace Hemo.Client.Modules
{
    public partial class PatientDtlEditMain : DevExpress.XtraEditors.XtraUserControl
    {
        #region 变量
        private NavigationFrame navigationFrame;
        private PatientModel.MED_PATIENTSDataTable _patientDataTable;
        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IPatient _patientService = ServiceManager.Instance.PatientService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private NavigationPage page1 = new NavigationPage() { Caption = "透析前" };
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        private IMachine _machine = ServiceManager.Instance.MachineService;

        private CtlManagerPerson _currentSelectedCtl;
        private CtlManagerPerson _currentRightSelectedCtl;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _bedDataTable;
        private DateTime BeginDate { set; get; }
        private string SickArea { set; get; }
        private string Banci { set; get; }
        private string AreaName { set; get; }

        public static PatientFixInfosUI pUI;

        public static PatientModel.MED_PATIENTSRow PatientDocRow = null;

        private ctlGetPatientStatus ctl;

        public ModuleType CurrentUI { get; set; }

        #endregion

        #region 构造函数
        public PatientDtlEditMain(DateTime BeginDate, string SickArea, string AreaName, string Banci, PatientModel.MED_PATIENTSRow pPatientRow)
        {
            InitializeComponent();
            this.panelControl1.BringToFront();
            this.BeginDate = BeginDate;
            this.SickArea = SickArea;
            this.AreaName = AreaName;
            this.Banci = Banci;
            PatientDocRow = pPatientRow;
            #region 大分类
            {
                ConfigModel.MED_COMMON_ITEMLISTDataTable ctlConfig = this._configService.GetConfigList(string.Empty, string.Empty, "流程节点", "1");
                var dtDetail = _machine.GetProcessSetDataList();
                if (ctlConfig != null && ctlConfig.Rows.Count > 0)
                {
                    foreach (ConfigModel.MED_COMMON_ITEMLISTRow dr in ctlConfig)
                    {
                        ctlGetPatientStatus dtl = new ctlGetPatientStatus(dr, dtDetail);
                        dtl.ControlFly += new ctlGetPatientStatus.ControlFlyEventHandler(moveCtl);
                        this.flwpnl.Controls.Add(dtl);
                    }
                }
            }

            //OfficeNavigationBar navigationBar = new OfficeNavigationBar() { Dock = DockStyle.Top, Parent = this.panelControl1 };
            //navigationFrame中现只用到page2，其余page已废弃--dinghangcheng 2019-5-13
            navigationFrame = new NavigationFrame() { Dock = DockStyle.Fill, Parent = this.panelControl1 };

            navigationFrame.AllowTransitionAnimation = DevExpress.Utils.DefaultBoolean.False;
            page1 = new NavigationPage() { Caption = "透析前" };
            var pageContent1 = new BeforeDialysis();
            pageContent1.Parent = page1;
            pageContent1.Dock = DockStyle.Fill;

            var page2 = new NavigationPage() { Caption = "透析中" };
            page2.BorderStyle = BorderStyle.None;
            page2.BackColor = Color.Transparent;
            page2.ForeColor = Color.Transparent;
            pUI = new PatientFixInfosUI();
            pUI.CurrentDate = BeginDate;
            pUI.AreaName = this.AreaName;
            var pageContent2 = pUI;
            pageContent2.PatientDocRow = pPatientRow;
            pageContent2.LoadPatientFixInfos();
            pageContent2.Parent = page2;
            pageContent2.Dock = DockStyle.Fill;

            var page3 = new NavigationPage() { Caption = "透析后" };
            var pageContent3 = new AfterDialysis();
            pageContent3.Parent = page3;
            pageContent3.Dock = DockStyle.Fill;
            navigationFrame.Pages.AddRange(new NavigationPage[] { page1, page2, page3 });
            navigationFrame.BringToFront();
            //navigationBar.NavigationClient = navigationFrame;              
            #endregion
        }


        #endregion

        #region 方法

        private void moveCtl(object sender, MyMainEventArgs e)
        {
            if (ctl != null)
            {
                if (ctl != (ctlGetPatientStatus)sender)
                {
                    ctl.HideFly();
                }
            }
            ctl = (ctlGetPatientStatus)sender;
        }

        public void ChangeSelect()
        {
            this.navigationFrame.SelectedPageIndex = 1;
        }
        /// <summary>
        /// 加载 控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabbedView1_QueryControl(object sender, DevExpress.XtraBars.Docking2010.Views.QueryControlEventArgs e)
        {
            e.Control = new XtraUserControl2();
        }

        private void navBarItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            // this.document1.Control.Controls.Clear();
            // this.document1.Control.Controls.Add(Activator.CreateInstance(Type.GetType("Hemo.Client.Modules.XtraUserControl1"), e.Link.Item.Caption) as Control);
        }

        /// <summary>
        /// 登录时加载 内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientDtlEditMain_Load(object sender, EventArgs e)
        {
            this._bedDataTable = this._configService.GetConfigList(string.Empty, string.Empty, "床位", "1");
            InzationPatientCurrentData();

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
                ediSickArea.EditValue = this.SickArea;
            }
            this.navigationFrame.SelectedPageIndex = 1;
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage2;
            (this.navigationFrame.Pages[1].Controls[0] as PatientFixInfosUI).ChangePage(CurrentUI);
        }
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="pControl"></param>
        void AddControlClickEvent(Control pControl)
        {
            foreach (Control ctl in pControl.Controls)
            {
                ctl.MouseClick += new MouseEventHandler(ctl_Click);
                AddControlClickEvent(ctl);
            }
        }
        /// <summary>
        /// 事件内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ctl_Click(Object sender, MouseEventArgs e)
        {
            if (ctl != null)
            {
                ctl.HideFly();
            }
        }

        public static void SelectGroup()
        {
            // InzationPatientCurrentData();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InzationPatientCurrentData()
        {

            DataTable patientDataTable;
            //
            patientDataTable = new DataTable();
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                //生成当日临时处方
                worker.DoWork += delegate (object sender1, DoWorkEventArgs e1)
                {
                    if (SickArea == "c570d95c-76a2-4af4-893a-1357065623bf")
                    { SickArea = string.Empty; }

                    patientDataTable = objPatient.GetPatientListBySchedule(BeginDate, SickArea, Banci);

                    patientDataTable.Columns.Add("PATIENT_HEAD_PORTRAIT", System.Type.GetType("System.Byte[]"));
                    patientDataTable.Columns.Add("PATIENT_TAG", System.Type.GetType("System.Byte[]"));
                    patientDataTable.Columns.Add("STATUS_TAG", System.Type.GetType("System.Byte[]"));
                    patientDataTable.Columns.Add("zhaopian", System.Type.GetType("System.Byte[]"));
                    patientDataTable.Columns.Add("BED_NUMBERNAME", System.Type.GetType("System.String"));
                    patientDataTable.Columns.Add("BED_NUM", typeof(int));


                    foreach (DataRow patientRow in patientDataTable.Rows)
                    {
                        string bedNumber = this._bedDataTable.FindByITEM_ID(patientRow["BED_NUMBER"].ToString()).ITEM_VALUE;
                        patientRow["BED_NUMBERNAME"] = bedNumber;
                        int.TryParse(bedNumber, out int bed_num);
                        patientRow["BED_NUM"] = bed_num;
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
                        patientRow["zhaopian"] = Utility.BitmapToBytes(Resources.默认显示照片);
                    }
                };
                worker.RunWorkerCompleted += delegate (object sender2, RunWorkerCompletedEventArgs e2)
                {
                    patientDataTable.DefaultView.Sort = "BED_NUM ASC";
                    patientDataTable = patientDataTable.DefaultView.ToTable();
                    if (patientDataTable != null && patientDataTable.Rows.Count > 0)
                    {
                        var sortNum = patientDataTable.AsEnumerable().Select(x => x.Field<int>("BED_NUM")).OrderBy(x => x).ToList();
                        DataTable dtSub = patientDataTable.Clone();
                        patientDataTable.AsEnumerable().CopyToDataTable(dtSub, LoadOption.PreserveChanges);
                        this.flowLayoutPanel1.Controls.Clear();
                        CtlManagerPerson[] ctls = new CtlManagerPerson[dtSub.Rows.Count];
                        int index = 0;
                        //2026-05-21 刘建超 正常选择人员排序 之前的逻辑是选中人员置顶，无序
                        for (int i = 0; i < sortNum.Count; i++)
                        {
                            foreach (DataRow dr in dtSub.Rows)
                            {
                                if (!dr["BED_NUM"].Equals(sortNum[i])) continue;
                                CtlManagerPerson ctluser = new CtlManagerPerson(dr);
                                ctluser.ContainerPanelClick += new CtlManagerPerson.ContainerCtlClickEventHandler(CtlManagerPerson_ContainerPanelClick);
                                ctluser.ContainerPanelDoubleClick += new CtlManagerPerson.ContainerCtlDoubleClickEventHandler(CtlManagerPerson_ContainerPanelDoubleClick);
                                ctluser.CtlMouseRightClick += new CtlManagerPerson.CtlMouseRightClickEventHandler(ctluser_CtlMouseRightClick);
                                if (PatientDocRow != null)
                                {
                                    if (dr["HEMODIALYSIS_ID"].ToString() == PatientDocRow.HEMODIALYSIS_ID.ToString())
                                    {
                                        _currentSelectedCtl = (CtlManagerPerson)ctluser;
                                        ctluser.SetSelectedEffect();
                                    }
                                }
                                ctls[i] = ctluser;
                                index++;
                            }
                        }

                        this.flowLayoutPanel1.Controls.AddRange(ctls);
                    }
                };
                worker.RunWorkerAsync();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InzationPatientData()
        {
            try
            {
                using (BackgroundWorker worker = new BackgroundWorker())
                {
                    DrugModel.MED_PATIENTS_CARDDataTable patient = null;
                    worker.DoWork += delegate (object sender1, DoWorkEventArgs e1)
                    {
                        _patientDataTable = objPatient.GetPatientList();
                        patient = objPatient.GetCardInfoByInfo(string.Empty, "");
                    };
                    worker.RunWorkerCompleted += delegate (object sender2, RunWorkerCompletedEventArgs e2)
                    {
                        if (patient != null && patient.Rows.Count > 0)
                        {
                            var p = Hemo.Utilities.Utility.GetSubTable(_patientDataTable, "1=2");
                            _patientDataTable.Where(i => i.HEMODIALYSIS_ID == patient[0].HEMODIALYSIS_ID).CopyToDataTable(p, LoadOption.PreserveChanges);
                            if (p != null && p.Rows.Count > 0)
                            {
                                gridControlPatient.DataSource = p;
                            }
                            else
                            {
                                gridControlPatient.DataSource = null;
                            }
                        }
                        else
                        {
                            if (_patientDataTable != null && _patientDataTable.Rows.Count > 0)
                            {
                                gridControlPatient.DataSource = _patientDataTable;
                            }
                            else
                            {
                                gridControlPatient.DataSource = null;
                            }
                        }

                    };
                    worker.RunWorkerAsync();
                }
            }
            catch (Exception ee)
            {
            }


        }

        #endregion

        #region 事件
        void ctluser_CtlMouseRightClick(object sender, ContainerCtlEventArgs args)
        {
            SetOrClearBoder(sender);
            this.contextMenuStrip1.Show(MousePosition);
        }
        void CtlManagerPerson_ContainerPanelClick(object sender, ContainerCtlEventArgs args)
        {
            SetOrClearBoder(sender);
            this.flyoutPanel1.ShowPopup();
        }
        void SetOrClearBoder(object sender)
        {
            if (this._currentRightSelectedCtl != null)
                this._currentRightSelectedCtl.ClearBorder();
            this._currentRightSelectedCtl = (CtlManagerPerson)sender;
            this._currentRightSelectedCtl.SetBorder();
        }

        private string hemoStr = string.Empty;


        void CtlManagerPerson_ContainerPanelDoubleClick(object sender, ContainerCtlEventArgs args)
        {
            if (this._currentSelectedCtl != null)
            {
                this._currentSelectedCtl.ClearSelectedEffect();
                this._currentSelectedCtl.ClearBorder();
            }

            this._currentSelectedCtl = (CtlManagerPerson)sender;
            this._currentSelectedCtl.SetSelectedEffect();
            //if (hemoStr.Equals(_currentSelectedCtl.drPatientRow["HEMODIALYSIS_ID"]))
            //    return;
            hemoStr = _currentSelectedCtl.drPatientRow["HEMODIALYSIS_ID"].ToString();

            DataRow dr = _currentSelectedCtl.drPatientRow;
            PatientDocRow = GetPatientRow(dr);
            PatientDetail.PdInstance().ChangeTitleInfo(this, new MyEventArgs { Get = PatientDocRow, BeginDate = BeginDate, Banci = Banci, SickArea = SickArea, AreaName = this.AreaName });

            //重新载入新的患者信息
            (navigationFrame.Pages[1].Controls[0] as PatientFixInfosUI).PatientDocRow = PatientDocRow;
            (navigationFrame.Pages[1].Controls[0] as PatientFixInfosUI).ChangeAllControlByPatientInfoTab_1(PatientDocRow);
            (navigationFrame.Pages[1].Controls[0] as PatientFixInfosUI).ChangeAllControlByPatientInfoTab_2(PatientDocRow);


        }


        /// <summary>
        /// 隐藏左右空间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hideContainerLeft_Click(object sender, EventArgs e)
        {

        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            // navigationFrame.SelectNextPage();
            navigationFrame.SelectedPageIndex = 0;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            navigationFrame.SelectedPageIndex = 1;
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            navigationFrame.SelectedPageIndex = 2;
        }

        /// <summary>
        /// 鼠标移动上去时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tileView1_MouseMove(object sender, MouseEventArgs e)
        {
            //int x = -1;
            //int y = 202;
            //this.flyoutPanel1.Height = 35;
            //var width = this.flyoutPanel1.Size.Width;
            //var height = this.flyoutPanel1.Size.Height;
            //if ((e.X >= x && e.X <= x + width) && (e.Y <= y && e.Y >= e.Y - height))
            //{
            //    this.flyoutPanel1.ShowPopup();
            //}
        }

        private PatientModel.MED_PATIENTSRow GetPatientRow(DataRow confirmRow)
        {
            var patientNew = new PatientModel.MED_PATIENTSDataTable();

            patientNew = objPatient.GetPatientListByParams(confirmRow["NAME"].ToString(), confirmRow["HEMODIALYSIS_ID"].ToString());
            return patientNew[0];
            #region MyRegion
            //PatientModel.MED_PATIENTSDataTable dtt = new PatientModel.MED_PATIENTSDataTable();

            //PatientModel.MED_PATIENTSRow row = dtt.NewMED_PATIENTSRow();
            //row.HEMODIALYSIS_ID = confirmRow["HEMODIALYSIS_ID"].ToString();
            //row.PATIENT_ID = confirmRow["PATIENT_ID"].ToString();
            //row.NAME = confirmRow["NAME"].ToString();
            //row.SEX = confirmRow["SEX"].ToString();
            //if (!string.IsNullOrEmpty(confirmRow["BIRTHDAY"].ToString()))
            //    row.BIRTHDAY = Convert.ToDateTime(confirmRow["BIRTHDAY"].ToString());

            //row.AGE = Convert.ToDecimal(confirmRow["AGE"].ToString());
            //row.NATIVEPLACE = confirmRow["NATIVEPLACE"].ToString();
            //row.JOB = confirmRow["JOB"].ToString();
            //row.MARITAL = confirmRow["MARITAL"].ToString();
            //row.CREDENTIALS_TYPE = confirmRow["CREDENTIALS_TYPE"].ToString();
            //row.CREDENTIALS_NUMBER = confirmRow["CREDENTIALS_NUMBER"].ToString();
            //row.EDUCATION = confirmRow["EDUCATION"].ToString();
            //row.NATION = confirmRow["NATION"].ToString();
            //row.WORK_TELEPHONE = confirmRow["WORK_TELEPHONE"].ToString();
            //row.ADDRESS = confirmRow["ADDRESS"].ToString();
            //row.MEDICAL_TYPE = confirmRow["MEDICAL_TYPE"].ToString();
            //row.TELEPHONE = confirmRow["TELEPHONE"].ToString();
            //row.TIME_TYPE = confirmRow["TIME_TYPE"].ToString();
            //row.SPECIFIC_TIME = Convert.ToDateTime(confirmRow["SPECIFIC_TIME"].ToString());
            //row.ADMISSION_NUMBER = confirmRow["ADMISSION_NUMBER"].ToString();
            //row.IS_NEW = confirmRow["IS_NEW"].ToString();
            //row.WHAT_HOSPITAL_IN = confirmRow["WHAT_HOSPITAL_IN"].ToString();
            //row.WHAT_DEPARTMENT_IN = confirmRow["WHAT_DEPARTMENT_IN"].ToString();
            //row.FIRST_VISIT = confirmRow["FIRST_VISIT"].ToString();
            //row.DIAGNOSE = confirmRow["DIAGNOSE"].ToString();
            //if (!string.IsNullOrEmpty(confirmRow["LEAVE_HOSPITAL_TIME"].ToString()))
            //    row.LEAVE_HOSPITAL_TIME = Convert.ToDateTime(confirmRow["LEAVE_HOSPITAL_TIME"]);
            //row.INFECTIOUS_CHECK_RESULT = confirmRow["INFECTIOUS_CHECK_RESULT"].ToString();
            //row.INPUT_CODE = confirmRow["INPUT_CODE"].ToString();
            //row.WARD_CODE = confirmRow["WARD_CODE"].ToString();
            //row.BED_NO = confirmRow["BED_NO"].ToString();
            //return row;

            #endregion
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ConfirmOrCancelOnePatient(true);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ConfirmOrCancelOnePatient(false);
        }

        private void gridControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip1.Show(MousePosition);
            }

        }
        /// <summary>
        /// 检查区域是否感染
        /// </summary>
        /// <param name="strRoom"></param>
        /// <param name="strInf"></param>
        /// <returns></returns>
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
        /// 取消
        /// </summary>
        /// <param name="pStatus"></param>
        private void ConfirmOrCancelOnePatient(bool pStatus)
        {
            DataRow row = null;
            if (_currentRightSelectedCtl != null)
            {
                row = _currentRightSelectedCtl.drPatientRow;
            }
            else
            {
                return;
            }
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

            if (SickArea != null)
            {
                strRoom = SickArea;
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
                DateTime startDate = Utility.CDate(BeginDate.ToShortDateString());
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

                    result = _patientScheduleService.SavePatientScheduleInfo(dataTableSchedule);
                    result = _hemodialysisService.SaveRecipeUserIDByRecipeIDList(strRecipeIDList, HemoApplicationContext.Current.CurrentUser.EMP_NO);
                    if (result > 0)
                    {
                        AutoClosedMsgBox.ShowForm("该病人处方已" + strStautsName + "。", "病患管理", 2000, MessageBoxIcon.Warning);
                        InzationPatientCurrentData(); //edit by pagi;
                    }
                }
            }
            else
            {
                AutoClosedMsgBox.ShowForm("请先选择一个病人！", "病患管理", 2000, MessageBoxIcon.Warning);
            }
        }

        private void gridControlPatient_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = this.gridView1.GetFocusedDataRow();
            PatientDocRow = GetPatientRow(dr);

            PatientDetail.PdInstance().ChangeTitleInfo(this, new MyEventArgs { Get = PatientDocRow, BeginDate = BeginDate, Banci = Banci, SickArea = SickArea, AreaName = this.AreaName });

            //重新载入新的患者信息
            (navigationFrame.Pages[1].Controls[0] as PatientFixInfosUI).PatientDocRow = PatientDocRow;
            (navigationFrame.Pages[1].Controls[0] as PatientFixInfosUI).ChangeAllControlByPatientInfoTab_1(PatientDocRow);
            (navigationFrame.Pages[1].Controls[0] as PatientFixInfosUI).ChangeAllControlByPatientInfoTab_2(PatientDocRow);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.gridView1.OptionsView.ShowAutoFilterRow = this.checkBox1.Checked;
        }

        private void panelControl1_Click(object sender, EventArgs e)
        {
            if (ctl != null) { ctl.HideFly(); }
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            PatientDocRow = null;
            InzationPatientCurrentData();
            this.flyoutPanel1.HidePopup();
        }
        /// <summary>
        /// 区域选择时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ediSickArea_EditValueChanged(object sender, EventArgs e)
        {
            this.SickArea = this.ediSickArea.EditValue.ToString();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                InzationPatientData();
            }
        }

        /// <summary>
        /// 检验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DataRow row = null;
            if (_currentRightSelectedCtl != null)
            {
                row = _currentRightSelectedCtl.drPatientRow;
            }
            else
            {
                return;
            }
            if (row != null)
            {
                var patientRow = GetPatientRow(row);
                if (patientRow != null)
                {
                    LabFrm labFrm = new LabFrm(patientRow);
                    labFrm.ShowDialog();
                }

            }
        }
        /// <summary>
        /// 检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DataRow row = null;
            if (_currentRightSelectedCtl != null)
            {
                row = _currentRightSelectedCtl.drPatientRow;
            }
            else
            {
                return;
            }
            if (row != null)
            {
                var patientRow = GetPatientRow(row);
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
        /// <summary>
        /// 简易处方ToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 简易处方ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow dr = null;
            if (_currentRightSelectedCtl != null)
            {
                dr = _currentRightSelectedCtl.drPatientRow;
            }
            else
            {
                return;
            }
            if (dr != null)
            {
                using (var frm = new EditUFR(dr["HEMODIALYSIS_ID"].ToString(), Utility.CDate(BeginDate.ToString())))
                {
                    if (frm.IsShowFrm)
                    {
                        frm.AreaName = this.AreaName;
                        frm.CurrentPatient = dr;
                        DialogResult result = frm.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            InzationPatientCurrentData();
                        }
                    }
                }
            }
        }

        #endregion

    }

}
