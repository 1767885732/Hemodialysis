/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:修改对外公开的方法
 * 创建标识:贺建操-2016年12月1日
 * 
 * 修改时间:2017年4月18日
 * 修改人:顾伟伟
 * 修改描述:用户控件
 * 
 * 修改时间:2017年5月20日
 * 修改人:顾伟伟
 * 修改描述:增加窗体控件值的方法
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
using Hemo.Client.Base;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using Hemo.Client.Base.XtraBaseInfo;
using DevExpress.XtraBars.Docking;
using Hemo.Model;
using Hemo.IService.PatientSchedule;
using Hemo.Service;
using Hemo.IService.Config;
using Hemo.Client.UI.Hemodialysis;
using Hemo.Client.UI.PatientFixUI;
using DevExpress.XtraBars.Docking2010.Customization;


namespace Hemo.Client.Modules
{
    /// <summary>
    /// 病人基本信息
    /// </summary>
    public partial class PatientDetail : XtraUserControl
    {
        #region 变量
        private static PatientDetail pd = null;
        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        private DateTime BeginDate { set; get; }
        private string SickArea { set; get; }
        private string AreaName { set; get; }
        private string Banci { set; get; }

        private PatientModel.MED_PATIENTSRow PatientRow { set; get; }

        public static PatientDetail PdInstance()
        {
            if (pd == null)
            {
                pd = new PatientDetail();
            }
            return pd;
        }


        #endregion

        #region 构造函数

        private PatientDetail()
        {
            InitializeComponent();
        }

        #endregion

        #region 方法

        public void InitializeButtonPanel()
        {
            var listBI = new List<ButtonInfo>();
            listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "扫描件", Name = "4", Image = Hemo.Client.Base.XtraBaseInfo.ImageHelper.GetImageFromToolbarResource("UpdateBtn"), mouseEventHandler = PatientDocImageUpdate });
            listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "快捷处方", Name = "5", Image = Hemo.Client.Base.XtraBaseInfo.ImageHelper.GetImageFromToolbarResource("QuekRecipe"), mouseEventHandler = 快捷处方ToolStripMenuItem_Click });
            BottomPanel.InitializeButtons(listBI, false);
        }

        string strPantientNew = string.Empty;
        DataTable dtPatientNew = new DataTable();
        public void ChangeTitleInfo(object sender, EventArgs e)
        {
            MyEventArgs args = (MyEventArgs)e;
            BeginDate = args.BeginDate;
            SickArea = args.SickArea;
            Banci = args.Banci;
            PatientRow = args.Get;
            AreaName = args.AreaName;
            var method = new MethodInvoker(() =>
            {
                InitializeButtonPanel();
                this.lblAge.Text = args.Get.AGE.ToString();
                this.lblName.Text = args.Get.NAME;
                this.lblHemoID.Text = args.Get.HEMODIALYSIS_ID;
                this.lblSex.Text = args.Get.SEX;
                this.lblINFECTIOUS_CHECK_RESULT.Text = args.Get.INFECTIOUS_CHECK_RESULT;
                this.lblFrom.Text = args.Get.TIME_TYPE;
                this.lblPatientSrc.Text = _patientScheduleService.GetCurrentScheduleInfoByHemoId(args.Get.HEMODIALYSIS_ID);
                dtPatientNew = _hemodialysisService.GetPatientTypeIsNew(args.Get.HEMODIALYSIS_ID);
                if (dtPatientNew != null && dtPatientNew.Rows.Count > 0)
                {
                    strPantientNew = dtPatientNew.Rows[0][0].ToString() == "1" ? "新入" : string.Empty;
                }

                lblFrom.Text = strPantientNew + _hemodialysisService.GetCureTypeByHemoId(args.Get.HEMODIALYSIS_ID);
            });
            if (InvokeRequired) BeginInvoke(method);
            else method();
        }


        public void Pget(object sender, EventArgs e)
        {
            MyEventArgs args = (MyEventArgs)e;
            BeginDate = args.BeginDate;
            SickArea = args.SickArea;
            Banci = args.Banci;
            PatientRow = args.Get;
            AreaName = args.AreaName;
            var method = new MethodInvoker(() =>
            {
                this.lblAge.Text = args.Get.AGE.ToString();
                this.lblName.Text = args.Get.NAME;
                this.lblHemoID.Text = args.Get.HEMODIALYSIS_ID;
                this.lblSex.Text = args.Get.SEX;
                this.lblINFECTIOUS_CHECK_RESULT.Text = args.Get.INFECTIOUS_CHECK_RESULT;
                this.lblFrom.Text = args.Get.TIME_TYPE;
                this.lblPatientSrc.Text = _patientScheduleService.GetCurrentScheduleInfoByHemoId(args.Get.HEMODIALYSIS_ID);
                dtPatientNew = _hemodialysisService.GetPatientTypeIsNew(args.Get.HEMODIALYSIS_ID);
                if (dtPatientNew != null && dtPatientNew.Rows.Count > 0)
                {
                    strPantientNew = dtPatientNew.Rows[0][0].ToString() == "1" ? "新入" : string.Empty;
                }

                lblFrom.Text = strPantientNew + _hemodialysisService.GetCureTypeByHemoId(args.Get.HEMODIALYSIS_ID);
                if (pd != null)
                {
                    PatientDtlEditMain a1 = new PatientDtlEditMain(BeginDate, SickArea, AreaName, Banci, PatientRow);
                    a1.Width = panelControl5.Width;
                    a1.Height = panelControl5.Height;
                    a1.CurrentUI = args.CurrentUI;
                    a1.Parent = panelControl5;
                    a1.Dock = DockStyle.Fill;
                    this.panelControl5.Controls.Clear();
                    this.panelControl5.Controls.Add(a1);
                }
            });
            if (InvokeRequired) BeginInvoke(method);
            else method();
        }

        public BottomPanelBase BottomPanel
        {
            get
            {
                if (Parent == null || Parent.Parent == null)
                {
                    return null;
                }
                var mainForm = Parent.Parent as MainFrm;
                if (mainForm != null)
                {
                    return mainForm.bottomPanelBase1;
                }
                mainForm = Parent.Parent.Parent as MainFrm;
                if (mainForm != null)
                {
                    return mainForm.bottomPanelBase1;
                }
                return null;
            }
        }

        #endregion
        #region 事件


        private void tabbedView1_QueryControl(object sender, DevExpress.XtraBars.Docking2010.Views.QueryControlEventArgs e)
        {
            XtraUserControl xtr1 = new XtraUserControl();
            e.Control = xtr1;
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            BottomPanel.Visible = true;
            MainFrm.viewModel.SelectModule(ModuleType.PatientMgr);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // a1.dockPanel1.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            BottomPanel.Visible = true;
            MainFrm.viewModel.SelectModule(ModuleType.PatientMgr);
        }

        /// <summary>
        /// 病人扫描件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientDocImageUpdate(object sender, EventArgs e)
        {
            if (PatientRow != null)
            {
                var ctl = new PatientDocImageUI(PatientRow);
                FlyoutDialog.Show(this.FindForm(), ctl);
            }
            else
            {
                XtraMessageBox.Show("请先选择一个病人,然后上传电子扫描件！", "病患管理");
            }
        }

        private void 快捷处方ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var main = MainFrm.viewModel;
            //(this.FindForm() as MainFrm).IsShowPopupFlyOutPanel(false);

            //main.SelectModule(ModuleType.AllPatientList, (x) =>
            //{
            //    ViewModelHelper.EnsureModuleViewModel(main.SelectedModule, main, null);
            //    if (dr != null)
            //        ((AllPatientList)main.SelectedModule).currentPatientsRow = PatientRow;
            //    ((AllPatientList)main.SelectedModule).currentCureDate =BeginDate;
            //    ((AllPatientList)main.SelectedModule).InzationControl("FASTRECIPELISTMODLES");

            //    ((AllPatientList)main.SelectedModule).Refresh();
            //});


            FastRecipeListNew frm = new FastRecipeListNew();
            frm.currentDt = BeginDate;
            frm._currentPatientRow = PatientRow;
            //frm.InitalizeData();
            FlyoutDialog.Show(this.FindForm(), frm);
        }

        private void PatientDetail_Load(object sender, EventArgs e)
        {
            // InitializeButtonPanel();
        }

        #endregion

    }
}
