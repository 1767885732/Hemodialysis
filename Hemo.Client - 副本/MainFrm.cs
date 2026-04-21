/*----------------------------------------------------------------
// Copyright (C) 2005 XX公司
// 描述：主窗体
// 创建时间：2017-04-12
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hemo.Client.Base;
using Hemo.Client.Base.XtraBaseInfo;
using DevExpress.Utils.Gesture;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils.Animation;
using Hemo.Client.Core;
using Hemo.WinForm;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraBars.Docking2010.Customization;
using Hemo.Client.UI.User;
using Hemo.Client.Controls.Common;
using System.Xml;
using DevExpress.LookAndFeel;
using Hemo.IService.Permission;
using Hemo.Service;

namespace Hemo.Client
{
    public partial class MainFrm : BaseFrm, IMainModule, ISwipeGestureClient
    {
        #region 类变量

        private IUser _userService = ServiceManager.Instance.UserService;

        public static DataTable DtRoleControl;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public MainFrm()
        {
            Program.MainForm = this;

            InitializeComponent();
            InitViewModel();
            //  PrepareUI();
            this.tileNavPane1.OptionsPrimaryDropDown.Height = 100;
        }

        #endregion

        #region 事件

        private void MainFrm_Load(object sender, EventArgs e)
        {
            InitTileBar();
            this.barBtn_User.Caption = string.IsNullOrEmpty(HemoApplicationContext.Current.CurrentUser.USER_NAME) ? "用户" : "当前用户:" + HemoApplicationContext.Current.CurrentUser.USER_NAME;
            this.barBtn_Date.Caption = DateTime.Today.ToString("yyyy年MM月dd日");
            this.barBtn_IP.Caption = HemoApplicationContext.Current.IpAddress;
            this.barBtn_Version.Caption = HemoApplicationContext.Current.versionAddress;
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();
            InitMenu();
            #region 设置皮肤
            var defaultSkinName = "Office 2013";
            var _userSkin = _userService.GetUserSkinDt();
            if (_userSkin != null)
            {
                var skinRow = _userSkin.FindByUSER_ID(HemoApplicationContext.Current.CurrentUser.USER_ID);
                if (skinRow != null)
                    defaultSkinName = skinRow.SKINSTRING;
            }

            UserLookAndFeel.Default.SetSkinStyle(defaultSkinName);//设置主题样式

            #region //去掉从XML中读取皮肤名称的。。

            //CheckFile();//检查文件
            //var defaultSkinName = GetXmlSkin();//获取xml主题
            //UserLookAndFeel.Default.SetSkinStyle(defaultSkinName);//设置主题样式

            #endregion

            #endregion
        }

        private void nBtnClose_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            this.Close();
        }

        private void tileNavPane1_SelectedElementChanged(object sender, DevExpress.XtraBars.Navigation.TileNavElementEventArgs e)
        {

            if (e.Element.Tag is ModuleType)
            {
                viewModel.SelectModule((ModuleType)e.Element.Tag);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void nBtnSmall_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void nBtnClose_ElementClick_1(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            using (ExitForm exit = new ExitForm())
            {
                var diagresult = FlyoutDialog.Show(this.FindForm(), exit);
                if (diagresult == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Close();
                }
            }
        }

        private void tileNavCategoryChangePsw_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            FlyoutDialog.Show(this.FindForm(), new ctlChangPwdFrm(HemoApplicationContext.Current.CurrentUser.USER_ID));

        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            SkinManagerFrm frm = new SkinManagerFrm();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = frm.SkinName;
            }
        }

        private void barDockControlBottom_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.modulesContainer.Controls[0].Name.ToString() == ModuleType.PatientMgr.ToString() || this.modulesContainer.Controls[0].Name.ToString() == ModuleType.PatientDetail.ToString())
            {
                IsShowPopupFlyOutPanel(true);
            }

        }

        #region 鼠标移动位置变量

        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标签是否为左键
        private void MainFrm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void MainFrm_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void MainFrm_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }

        #endregion

        #endregion

        #region 方法

        void InitMenu()
        {
            tileNavPane1.Categories.Clear();
            tileNavCategory3.Items.Clear();
            if (DtRoleControl != null)
            {
                for (int i = 0; i < DtRoleControl.Rows.Count; i++)
                {
                    var mt = ChangeToM(DtRoleControl.Rows[i]["PERMISSIONNAME"].ToString());
                    BtnEnabled(mt);
                }
            }

        }

        void BtnEnabled(ModuleType mt)
        {
            switch (mt)
            {
                case ModuleType.PatientMgr:
                    tileNavPane1.Categories.Add(TNavI_PatientMgr);
                    break;
                case ModuleType.PatientTreantmentFrmNew:
                    if (!tileNavPane1.Categories.Contains(tileNavCategory3))
                    {
                        tileNavPane1.Categories.Add(tileNavCategory3);
                    }
                    tileNavCategory3.Items.Add(TNavI_PatientTreantmentFrmNew);
                    break;
                case ModuleType.PatientScheduleFrmN:
                    tileNavPane1.Categories.Add(TNavI_PatientScheduleFrmNew);
                    break;
                case ModuleType.EqumentInfoMgr:
                    tileNavPane1.Categories.Add(TNavI_EqumentInfoMgr);
                    break;
                case ModuleType.ReportMainMgr:
                    tileNavPane1.Categories.Add(TNavI_ReportMainMgr);
                    break;
                case ModuleType.CtlSystemSet:
                    tileNavPane1.Categories.Add(TNavI_CtlSystemSet);
                    break;
                case ModuleType.DataReportManagerMgr:
                    if (!tileNavPane1.Categories.Contains(tileNavCategory3))
                    {
                        tileNavPane1.Categories.Add(tileNavCategory3);
                    }
                    tileNavCategory3.Items.Add(TNavI_DataReport);
                    break;
                default:
                    break;
            }

        }

        ModuleType ChangeToM(string str)
        {
            switch (str)
            {
                case "病患管理":
                    return ModuleType.PatientMgr;
                    break;
                case "病患治疗":
                    return ModuleType.PatientTreantmentFrmNew;
                    break;
                case "病患排班":
                    return ModuleType.PatientScheduleFrmN;
                    break;
                case "程序设置":
                    return ModuleType.CtlSystemSet;
                    break;
                case "统计报表":
                    return ModuleType.ReportMainMgr;
                    break;
                case "设备管理":
                    return ModuleType.EqumentInfoMgr;
                    break;
                case "患者上报管理":
                    return ModuleType.DataReportManagerMgr;
                    break;
                default:
                    return ModuleType.PatientMgr;
                    break;
            }
        }

        void InitTileBar()
        {
            TNavI_PatientMgr.Tag = ModuleType.PatientMgr;
            TNavI_PatientScheduleFrmNew.Tag = ModuleType.PatientScheduleFrmN;
            TNavI_PatientTreantmentFrmNew.Tag = ModuleType.PatientTreantmentFrmNew;
            TNavI_DataReport.Tag = ModuleType.DataReportManagerMgr;
            TNavI_CtlSystemSet.Tag = ModuleType.CtlSystemSet;
            TNavI_EqumentInfoMgr.Tag = ModuleType.EqumentInfoMgr;
            TNavI_ReportMainMgr.Tag = ModuleType.ReportMainMgr;
        }

        /// <summary>
        /// 主窗体中的下边工具栏是否显示。
        /// </summary>
        /// <param name="isShow"></param>
        public void IsShowPopupFlyOutPanel(bool isShow)
        {
            if (isShow)
            {
                this.flyoutPanel1.ShowPopup();

            }
            else
            {
                this.flyoutPanel1.HidePopup();

            }

        }

        #region 检查XML文件是否存在
        public void CheckFile()
        {
            try
            {
                if (System.IO.File.Exists("SkinInfo.xml") == false)
                {
                    //XtraMessageBox.Show("不存在XML");
                    CreateXml();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region 创建XML文件

        public void CreateXml()
        {
            XmlDocument doc = new XmlDocument();
            //建立xml定义声明
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(dec);

            //创建根节点
            XmlElement root = doc.CreateElement("SetSkin");
            XmlElement rootone = doc.CreateElement("Skinstring");//皮肤


            //将one，two，插入到root节点下
            doc.AppendChild(root);
            root.AppendChild(rootone);
            doc.Save("SkinInfo.xml");
        }

        #endregion

        #region 读取Xml节点内容

        public string GetXmlSkin()
        {
            var defaultSkinName = "Office 2013";
            try
            {
                XmlDocument mydoc = new XmlDocument();
                mydoc.Load("SkinInfo.xml");
                XmlNode ressNode = mydoc.SelectSingleNode("SetSkin");
                var defaultSkinName1 = ressNode.SelectSingleNode("Skinstring").InnerText;
                if (string.IsNullOrEmpty(defaultSkinName1))
                    defaultSkinName = "Office 2013";

            }
            catch (Exception ex)
            {
                return defaultSkinName;

            }
            return defaultSkinName;
        }

        #endregion
        #endregion

        #endregion

        #region IMainModule
        #region baseModel

        public static MainViewModel viewModel;
        bool allowFlyoutPanel = true;
        bool allowTransition = true;
        bool transitionEffective = false;

        void PrepareUI()
        {
            DisableBottomPanelSwipe();
        }
        void DisableBottomPanelSwipe()
        {
            bottomPanelBase1.Dock = DockStyle.Bottom;
            bottomPanelBase1.Parent = this.flyoutPanel1;
            bottomPanelBase1.SendToBack();
            allowFlyoutPanel = false;
        }
        #endregion
        private void transitionManager1_BeforeTransitionStarts(ITransition transition, CancelEventArgs e)
        {
            if (this.slideFadeTransition1 != null)
            {
                if (this.slideFadeTransition1.Parameters.EffectOptions == PushEffectOptions.FromLeft)
                    this.slideFadeTransition1.Parameters.EffectOptions = PushEffectOptions.FromRight;
                else
                    this.slideFadeTransition1.Parameters.EffectOptions = PushEffectOptions.FromLeft;
            }

            bottomPanelBase1.Enabled = true;
        }
        void transitionManager1_AfterTransitionEnds(ITransition transition, System.EventArgs e)
        {
            if (!IsHandleCreated) return;
            var method = new MethodInvoker(() =>
            {
                bottomPanelBase1.Enabled = true;
                var moduleControl = viewModel.SelectedModule as Hemo.Client.Base.BaseMoudleControl;
                if (moduleControl != null) moduleControl.OnTransitionCompleted();
            });
            if (InvokeRequired) BeginInvoke(method);
            else method();
        }
        public void StartTransition(bool effective)
        {
            this.transitionEffective = effective;
            if (!allowTransition) return;
            if (effective) transitionManager1.StartTransition(modulesContainer);
        }

        public void EndTransition(bool effective)
        {
            if (!effective || !allowTransition)
            {
                transitionManager1_AfterTransitionEnds(null, EventArgs.Empty);
                return;
            }
            transitionManager1.EndTransition();
        }

        void InitViewModel()
        {
            viewModel = ViewModelSource.Create(() => new MainViewModel(this));
            PrefetchChildModules();
            viewModel.ModuleAdded += viewModel_ModuleAdded;
            viewModel.ModuleRemoved += viewModel_ModuleRemoved;
            viewModel.ModuleTransitionCompleted += viewModel_ModuleTransitionCompleted;
        }

        private void PrefetchChildModules()
        {
            if (System.Diagnostics.Debugger.IsAttached) return;
            viewModel.GetModule(ModuleType.Opportunities);
            viewModel.GetModule(ModuleType.Tasks);
            viewModel.GetModule(ModuleType.Products);
            viewModel.GetModule(ModuleType.CustomersModule);
            viewModel.GetModule(ModuleType.Dashboard);
            viewModel.GetModule(ModuleType.Sales);
            viewModel.GetModule(ModuleType.PatientMgr);
            viewModel.GetModule(ModuleType.PatientTreantmentFrmNew);
            viewModel.GetModule(ModuleType.PatientScheduleFrmN);
            viewModel.GetModule(ModuleType.DataReportManagerMgr);
            viewModel.GetModule(ModuleType.CtlSystemSet);
            viewModel.GetModule(ModuleType.EqumentInfoMgr);
            viewModel.GetModule(ModuleType.ReportMainMgr);
        }
        void viewModel_ModuleAdded(object sender, EventArgs e)
        {
            var moduleControl = sender as Control;
            moduleControl.Dock = DockStyle.Fill;
            moduleControl.Size = modulesContainer.ClientSize;
            moduleControl.Parent = modulesContainer;
            if (moduleControl.Name.Equals("PatientScheduleFrmN"))
            {
                (moduleControl as Hemo.Client.Modules.PatientScheduleFrmN).InizationData(false);
            }
        }
        void viewModel_ModuleRemoved(object sender, EventArgs e)
        {
            var moduleControl = sender as Control;
            moduleControl.Parent = null;
        }
        void viewModel_ModuleTransitionCompleted(object sender, EventArgs e)
        {

        }

        public bool IsDocked(ModuleType type)
        {
            return true;
        }

        public void DockModule(ModuleType moduleType)
        {
            // throw new NotImplementedException();
        }

        public void ShowPeek(ModuleType moduleType)
        {
            // throw new NotImplementedException();
        }

        public void SaveLayoutToStream(System.IO.MemoryStream ms)
        {
            //  throw new NotImplementedException();
        }

        public void RestoreLayoutFromStream(System.IO.MemoryStream ms)
        {
            //  throw new NotImplementedException();
        }

        public DevExpress.Utils.Menu.IDXMenuManager MenuManager
        {
            get { return this.barManager1; }
        }

        #endregion

        #region ISwipeGestureClient

        public void OnSwipe(SwipeEventArgs args)
        {
            if (args.IsBottomEdge && allowFlyoutPanel)
            {
                flyoutPanel1.ShowPopup();
            }
        }

        #endregion
    }
}
