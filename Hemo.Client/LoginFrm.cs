/*----------------------------------------------------------------
// Copyright (C) 2005 苏州医疗科技股份有限公司有限公司
// 描述：登录窗体
// 创建时间：2017-03-08
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
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Navigation;
using DevExpress.Utils;
using Hemo.Client.Core;
using System.Windows.Media.Animation;
using Hemo.IService.Permission;
using Hemo.Service;
using System.Windows;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.Client.Base.XtraBaseInfo;
using System.Runtime.InteropServices;
using DevExpress.XtraSplashScreen;
using Hemo.Client.Controls;
using System.IO;

namespace Hemo.Client
{
    public partial class LoginFrm : DevExpress.XtraEditors.XtraForm
    {
        #region 类变量

        private NavigationFrame navigationFrame;
        public DefaultBoolean IsLogin = DefaultBoolean.Default;

        public EventHandler<LoginEventArgs> LoginEvent;

        private Storyboard _loadingStoryboard = null;

        private IUser _userService = ServiceManager.Instance.UserService;

        private bool _isEnter = false;

        private IDictionary<string, int> _formDict = new Dictionary<string, int>();

        private string ToRunAppNames = string.Empty;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public LoginFrm()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {

            var userName = this.userName.Text.Trim().ToUpper();
            var password = this.password.Text.Trim().ToUpper();
            this.lblMessage.Visible = false;
            DataTable office = new DataTable();
            if (string.IsNullOrEmpty(userName))
            {
                ShowMessage("请输入用户名!");
                this.userName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                ShowMessage("请输入密码!");
                this.password.Focus();
                return;
            }

            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    ShowMessage();//显示等待窗口
                    try
                    {
                        PermissionModel.MED_USERSDataTable loginUser = _userService.VerifyUserLogin(userName.Trim().ToUpper(),
                        Utility.Encrypto(password.ToUpper()));
                        if (loginUser.Rows.Count > 0)
                        {
                            LoginUser.SetUserInfo(loginUser.Rows[0] as PermissionModel.MED_USERSRow);
                            IsLogin = DefaultBoolean.True;
                            HemoApplicationContext.Current.CurrentUser = loginUser.Rows[0] as PermissionModel.MED_USERSRow;
                        }
                        else { IsLogin = DefaultBoolean.False; }
                        if (IsLogin == DefaultBoolean.True)
                        {
                            AppendLoginNamesToTxt(userName);
                            office = this._userService.GetPermissionListByUserID(LoginUser.User.USER_ID);
                            HemoApplicationContext.Current.RolesOffices = office;
                            HemoApplicationContext.Current.CurrentUser = LoginUser.User;
                            //执行每日排班的存储过程、写入所在星期的一周数据
                            this._userService.ExecuteScheduleProceduce();
                        }
                    }
                    catch (Exception ex)
                    {
                        HideMessage();
                        IsLogin = DefaultBoolean.Default;
                        Logger.WriteErrorLog(ex);

                    }
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    if (IsLogin == DefaultBoolean.True)
                    {
                        if (office.Rows.Count >= 1) //有一个权限以上的权限
                        {
                            MainFrm.DtRoleControl = office;
                            if (office.Rows.Count == 1) //只有一个权限点直接进入模块
                            {
                                _isEnter = true;
                                LoginMoudel(ChangeToM(office.Rows[0]["PERMISSIONNAME"].ToString()));
                            }
                            else  //有多个模块增加动画效果
                            {
                                DisplyOffice(office, userName);
                            }
                            this.password.Text = null;
                            this.lblMessage.Visible = false;
                        }
                        else   //没有权限
                        {
                            HideMessage();
                            ShowMessage("此用户未分配登陆模块,登陆失败!");
                        }
                    }
                    else if (IsLogin == DefaultBoolean.False)
                    {
                        HideMessage();
                        ShowMessage("用户名 / 密码错误!");
                    }
                    else if (IsLogin == DefaultBoolean.Default)
                    {
                        HideMessage();
                        InvokeLoginEvent(false);
                        this.Dispose();
                    }
                    HideMessage();
                };
                worker.RunWorkerAsync();

            }
        }

        private void LoginFrm_Load(object sender, EventArgs e)
        {
            //this.userName.Properties.NullValuePromptShowForEmptyValue = true;
            //this.userName.Properties.NullValuePrompt = "登陆名称";
            LoadLoginNamesFromTxt();
            LoadCurrentPath();
            //  this.userName.SetWatermark("登陆名称");
            this.password.Properties.NullValuePromptShowForEmptyValue = true;
            this.password.Properties.NullValuePrompt = "登陆密码";

            AllBtnEnabled(false);
        }

        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            { button9_Click_1(this, e); }
        }

        private void userName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                password.Focus();
            }
        }

        private void password_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void transitionManager1_BeforeTransitionStarts(DevExpress.Utils.Animation.ITransition transition, CancelEventArgs e)
        {
            _isEnter = false;
        }

        private void transitionManager1_AfterTransitionEnds(DevExpress.Utils.Animation.ITransition transition, EventArgs e)
        {
            _isEnter = true;
        }

        #region 按钮跳转

        public void AllBtnEnabled(bool isClick)
        {
            this.button1.Enabled = isClick;
            this.button2.Enabled = isClick;
            this.button3.Enabled = isClick;
            this.button7.Enabled = isClick;
            this.button5.Enabled = isClick;
            this.button6.Enabled = isClick;
            this.button8.Enabled = isClick;
            this.button4.Enabled = false;
        }

        void BtnEnabled(ModuleType mt)
        {
            switch (mt)
            {
                case ModuleType.PatientMgr:
                    button1.Enabled = true;
                    break;
                case ModuleType.PatientTreantmentFrmNew:
                    button2.Enabled = true;
                    break;
                case ModuleType.PatientScheduleFrmN:
                    button3.Enabled = true;
                    break;
                case ModuleType.EqumentInfoMgr:
                    button7.Enabled = true;
                    break;
                case ModuleType.ReportMainMgr:
                    button5.Enabled = true;
                    break;
                case ModuleType.CtlSystemSet:
                    button6.Enabled = true;
                    break;
                case ModuleType.DataReportManagerMgr:
                    button8.Enabled = true;
                    break;
                default:
                    break;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginMoudel(ModuleType.PatientMgr);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            LoginMoudel(ModuleType.PatientTreantmentFrmNew);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoginMoudel(ModuleType.PatientScheduleFrmN);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LoginMoudel(ModuleType.EqumentInfoMgr);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoginMoudel(ModuleType.ReportMainMgr);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoginMoudel(ModuleType.CtlSystemSet);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            LoginMoudel(ModuleType.DataReportManagerMgr);
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

        #endregion

        #endregion

        #region 方法

        private void ShowMessage(string message)
        {
            this.lblMessage.Visible = !lblMessage.Visible;
            this.lblMessage.Text = message;
        }

        /// <summary>
        /// 从本地文本文件加载登陆用户名称
        /// </summary>
        private void LoadLoginNamesFromTxt()
        {
            var txt = GetTxtPath();
            if (!System.IO.File.Exists(txt))
                return;
            try
            {
                var source = new AutoCompleteStringCollection();
                source.AddRange(System.IO.File.ReadAllLines(txt));
                userName.AutoCompleteCustomSource = source;
                userName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                userName.AutoCompleteSource = AutoCompleteSource.CustomSource;

                // this.userName.ItemsSource = System.IO.File.ReadAllLines(txt);
            }
            catch
            {

            }
        }
        private void LoadCurrentPath()
        {
            var dir = string.Format(@"{0}\configs", System.Windows.Forms.Application.StartupPath);
            if (!System.IO.Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);
            var pathVersion = string.Format(@"{0}\version.config", dir);
            if (!System.IO.File.Exists(pathVersion))
            {
                var verConfig = File.CreateText(pathVersion);
                verConfig.WriteLine(System.DateTime.Now.Date.ToString("yyyy-mm-dd"));
                verConfig.Close();
            }
            var pathText = System.IO.File.ReadAllLines(pathVersion);
            this.lbPath.Text = string.Format("当前版本：{0}", pathText[0].Replace("patch", "V3.0").Replace("-", "").Trim());
            HemoApplicationContext.Current.versionAddress = this.lbPath.Text;

        }
        /// <summary>
        /// 记录登陆用户名到本地文本文件
        /// </summary>
        private void AppendLoginNamesToTxt(string name)
        {
            var txt = GetTxtPath();
            try
            {
                if (!System.IO.File.Exists(txt))
                {
                    System.IO.File.CreateText(Text);
                }
                var items = System.IO.File.ReadAllLines(txt);
                if (items != null && items.Contains(name))
                    return;
                if (items.Length > 0)
                    name = Environment.NewLine + name;
                System.IO.File.AppendAllText(txt, name);
            }
            catch
            {
                //eat  
            }
        }

        /// <summary>
        /// 登陆名本地保存路径
        /// </summary>
        /// <returns></returns>
        private string GetTxtPath()
        {
            var dir = string.Format(@"{0}\Log", System.Windows.Forms.Application.StartupPath);
            if (!System.IO.Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);
            return string.Format(@"{0}\LoginNames.txt", dir);
        }

        /// <summary>
        /// 显示功能模块列表  -- add by pagi.liu at 20170302  重写该方法
        /// </summary>
        private void DisplyOffice(DataTable dt, string username)
        {
            transitionManager1.StartTransition(this.ctlT);
            {
                navigationFrame = new NavigationFrame() { Dock = DockStyle.Fill, Parent = this.ctlT };
                navigationFrame.AllowTransitionAnimation = DevExpress.Utils.DefaultBoolean.False;
                NavigationPage page1 = new NavigationPage() { Caption = "成功登陆" };
                var pageContent1 = new LoginFrmDtl(username);
                pageContent1.Parent = page1;
                pageContent1.Dock = DockStyle.Fill;
                navigationFrame.Pages.AddRange(new NavigationPage[] { page1 });
                navigationFrame.BringToFront();
                this.navigationFrame.SelectedPageIndex = 0;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var mt = ChangeToM(dt.Rows[i]["PERMISSIONNAME"].ToString());
                BtnEnabled(mt);
            }
            transitionManager1.EndTransition();
        }

        /// <summary>
        /// 登陆事件
        /// </summary>
        /// <param name="runApp"></param>
        private void InvokeLoginEvent(bool runApp)
        {
            if (LoginEvent != null)
                LoginEvent(this, new LoginEventArgs { RunApp = runApp, RunAppNames = ToRunAppNames });
        }

        private void LoginMoudel(ModuleType mt)
        {
            if (!_isEnter)
            {
                return;
            }

            Hemo.WinForm.Program.CurrentModuleType = mt;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        void SetFormMax(MainFrm frm)
        {
            frm.Top = 0;
            frm.Left = 0;
            frm.Width = Screen.PrimaryScreen.WorkingArea.Width;
            frm.Height = Screen.PrimaryScreen.WorkingArea.Height;
        }

        #endregion

        #region SplashScreenManager

        private SplashScreenManager _loadForm;
        /// <summary>
        /// 等待窗体管理对象
        /// </summary>
        protected SplashScreenManager LoadForm
        {
            get
            {
                if (_loadForm == null)
                {
                    this._loadForm = new SplashScreenManager(this.FindForm(), typeof(FrmWaitForm), true, true);
                    //this._loadForm.CloseWaitForm();.ClosingDelay = 0;
                }
                return _loadForm;
            }
        }
        /// <summary>
        /// 显示等待窗体
        /// </summary>
        public void ShowMessage()
        {
            bool flag = !this.LoadForm.IsSplashFormVisible;
            if (flag)
            {
                this.LoadForm.ShowWaitForm();
            }
        }
        /// <summary>
        /// 关闭等待窗体
        /// </summary>
        public void HideMessage()
        {
            bool isSplashFormVisible = this.LoadForm.IsSplashFormVisible;
            if (isSplashFormVisible)
            {
                this.LoadForm.CloseWaitForm();
            }
        }

        #endregion
    }

    public static class DoWaterTextBox
    {
        private const int EM_SETCUEBANNER = 0x1501;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]

        private static extern Int32 SendMessage
         (IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        /// <summary>
        /// 为TextBox设置水印文字
        /// </summary>
        /// <param name="textBox">TextBox</param>
        /// <param name="watermark">水印文字</param>
        public static void SetWatermark(this TextBox textBox, string watermark)
        {
            SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, watermark);
        }
        /// <summary>
        /// 清除水印文字
        /// </summary>
        /// <param name="textBox">TextBox</param>
        public static void ClearWatermark(this TextBox textBox)
        {
            SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, string.Empty);
        }
    }
}