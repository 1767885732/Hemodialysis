/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 文件名：LoginScreen.cs
// 文件功能描述：用户登录窗体
// 创建标识：顾伟伟-2013-07-10
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Data;
//using Medicalsystem.EMR.BusinessEntity;
//using Medicalsystem.EMR.Library;
using System.ComponentModel;
//using Medicalsystem.Docare.Security;
//using Medicalsystem.Docare.Common.Principal;
//using Medicalsystem.EMR.ServiceProxies;
//using Medicalsystem.Docare.ExceptionHandling;
using DevExpress.Utils;
//using Medicalsystem.Docare.SmartEditor;
using System.Windows.Controls.Primitives;
using Hemo.IService.Permission;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.Client.Core;
using Hemo.Client.UI.ReportChart;
using Hemo.WinForm;
using Hemo.Client.UI.PatientSchedule;
using Hemo.Client.UI.Patient;

namespace Hemo.Client {
    /// <summary>
    ///登陆界面
    /// </summary>
    public partial class LoginScreen : Window
    {
        #region 类变量

        public DefaultBoolean IsLogin = DefaultBoolean.Default;

        public EventHandler<LoginEventArgs> LoginEvent;

        private Storyboard _loadingStoryboard = null;

        private IUser _userService = ServiceManager.Instance.UserService;

        private IDictionary<string, int> _formDict = new Dictionary<string, int>();

        private string ToRunAppNames = string.Empty;

        private DataTable _shiftRoles = new DataTable();

        #endregion

        #region 属性

        public DataTable ShiftRoles
        {
            get { return _shiftRoles; }
            set
            {
                _shiftRoles = value;
                if (_shiftRoles != null)
                {
                    DisplyOffice();
                    this.offices.ItemsSource = _shiftRoles.DefaultView;
                }
            }
        }

        #endregion

        #region 构造函数

        public LoginScreen()
        {
            InitializeComponent();
            this._formDict.Add("病患管理", 1);
            this._formDict.Add("病患治疗", 2);
            this._formDict.Add("病患排班", 3);
            this._formDict.Add("程序设置", 4);
            this._formDict.Add("统计报表", 5);
            this._formDict.Add("设备管理", 6);
            this._formDict.Add("患者上报管理", 7);
            this._formDict.Add("主任工作站", 8);

        }

        #endregion

        #region 事件

        /// <summary>
        /// 位移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsLogin = DefaultBoolean.Default;
            this.Close();
        }

        /// <summary>
        /// 动画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLoginNamesFromTxt();
            this.userName.Focus();
            this.Topmost = true;
        }

        /// <summary>
        /// 用户名水印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userName_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.userNameWatermark.Visibility = string.IsNullOrEmpty(this.userName.Text) ? Visibility.Visible : Visibility.Collapsed;
            this.messageView.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        ///  密码水印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            this.passwordWatermark.Visibility = string.IsNullOrEmpty(this.password.Password) ? Visibility.Visible : Visibility.Collapsed;
            this.messageView.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var userName = this.userName.Text.Trim().ToUpper();
            var password = this.password.Password.Trim().ToUpper();
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

            this.logView.Visibility = Visibility.Collapsed;
            this.logining.Visibility = Visibility.Visible;

            if (_loadingStoryboard == null)
            {
                _loadingStoryboard = this.FindResource("loading") as Storyboard;
                _loadingStoryboard.Begin();
            }

            ReportMessage(false);

            if (!string.IsNullOrEmpty(HemoApplicationContext.Current.IsPassDogValid))
            {
                MessageBox.Show(HemoApplicationContext.Current.IsPassDogValid, "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            //     LookupDataSet.MED_DEPT_DICTDataTable office = null;

            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    try
                    {

                        //      this.IsLogin = Authentication.ValidateUser(userName, password) ? DefaultBoolean.True : DefaultBoolean.False;

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

                            //EmrApplicationContext.Current.UserIdentity = Medicalsystem.Docare.Common.ApplicationContext.Current.UserIdentity;
                            ////管理员或者实施用户
                            //if (EmrApplicationContext.Current.UserIdentity.AuthenticationType == AuthenticationType.Administrator
                            //    || EmrApplicationContext.Current.UserIdentity.AuthenticationType == AuthenticationType.Medicalsystemer) {
                            //    using (var proxy = new EmrProxy()) {
                            //        office = proxy.GetOffices();
                            //    }
                            //}
                            //else {
                            //    using (var proxy = new EmrProxy()) {
                            //        office = proxy.GetOfficeInfoByUserId(EmrApplicationContext.Current.UserIdentity.UserId);
                            //    }
                            //}

                        }
                    }
                    catch (Exception ex)
                    {
                        IsLogin = DefaultBoolean.Default;
                        //ExceptionManager.Handle(ex);
                        Logger.WriteErrorLog(ex);
                    }

                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    if (IsLogin == DefaultBoolean.True)
                    {
                        if (office.Rows.Count == 1)
                        {
                            ShowForm(office.Rows[0]["PERMISSIONNAME"].ToString());
                            InvokeLoginEvent(true);
                        }
                        else if (office.Rows.Count > 1)
                        {

                            this.offices.ItemsSource = office.DefaultView;
                            DisplyOffice();
                        }
                        else
                        {
                            ShowMessage("此用户未分配登陆模块,登陆失败!");
                            DisplayLoginView();
                        }
                    }
                    else if (IsLogin == DefaultBoolean.False)
                    {
                        ShowMessage("用户名 / 密码错误!");
                        DisplayLoginView();
                    }
                    else if (IsLogin == DefaultBoolean.Default)
                    {
                        //发生异常，关闭界面
                        InvokeLoginEvent(false);
                        this.Dispose();
                    }
                };

                worker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// 选择登陆科室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void offices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.offices.SelectedItem == null)
                return;
            var office = (this.offices.SelectedItem as DataRowView).Row;
            int iCount = office.ItemArray.Count() - 1;
            ShowForm(office.ItemArray[iCount].ToString());
            offices.Text = office.ItemArray[iCount].ToString();
            ReportMessage(true);
        }

        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minimizeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// 确定登陆科室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ok_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(this.offices.Text))
            {
                this.offices.Focus();
                return;
            }

            ShowForm(this.offices.Text.ToString());
            ReportMessage(true);
        }

        /// <summary>
        /// 选择科室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void officeCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ok_MouseLeftButtonDown(this, null);
        }

        /// <summary>
        /// 科室水印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void offices_TextChanged(object sender, TextChangedEventArgs e)
        {
            //   this.officeWatermark.Visibility = string.IsNullOrEmpty(this.offices.Text) ? Visibility.Visible : Visibility.Collapsed;
            //    ShowForm(this.offices.SelectionBoxItem.ToString());
        }

        /// <summary>
        /// 选择登陆用户名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems == null || e.AddedItems.Count == 0)
                return;
            if (e.AddedItems[0] != null)
            {
                if (userName.IsDropDownOpen)
                    this.password.Focus();
            }
        }

        #endregion

        #region 方法

        public void LoginTest()
        {
            var userName = "admin";
            var password = "medhemo";
            DataTable office = new DataTable();


            if (!string.IsNullOrEmpty(HemoApplicationContext.Current.IsPassDogValid))
            {
                MessageBox.Show(HemoApplicationContext.Current.IsPassDogValid, "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            //     LookupDataSet.MED_DEPT_DICTDataTable office = null;

            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    try
                    {

                        //      this.IsLogin = Authentication.ValidateUser(userName, password) ? DefaultBoolean.True : DefaultBoolean.False;

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

                            //EmrApplicationContext.Current.UserIdentity = Medicalsystem.Docare.Common.ApplicationContext.Current.UserIdentity;
                            ////管理员或者实施用户
                            //if (EmrApplicationContext.Current.UserIdentity.AuthenticationType == AuthenticationType.Administrator
                            //    || EmrApplicationContext.Current.UserIdentity.AuthenticationType == AuthenticationType.Medicalsystemer) {
                            //    using (var proxy = new EmrProxy()) {
                            //        office = proxy.GetOffices();
                            //    }
                            //}
                            //else {
                            //    using (var proxy = new EmrProxy()) {
                            //        office = proxy.GetOfficeInfoByUserId(EmrApplicationContext.Current.UserIdentity.UserId);
                            //    }
                            //}

                        }
                    }
                    catch (Exception ex)
                    {
                        IsLogin = DefaultBoolean.Default;
                        //ExceptionManager.Handle(ex);
                        Logger.WriteErrorLog(ex);
                    }

                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    if (IsLogin == DefaultBoolean.True)
                    {
                        if (office.Rows.Count == 1)
                        {
                            ShowForm(office.Rows[0]["PERMISSIONNAME"].ToString());
                            InvokeLoginEvent(true);
                        }
                        else if (office.Rows.Count > 1)
                        {

                            this.offices.ItemsSource = office.DefaultView;
                            DisplyOffice();
                        }
                        else
                        {
                            ShowMessage("此用户未分配登陆模块,登陆失败!");
                            DisplayLoginView();
                        }
                    }
                    else if (IsLogin == DefaultBoolean.False)
                    {
                        ShowMessage("用户名 / 密码错误!");
                        DisplayLoginView();
                    }
                    else if (IsLogin == DefaultBoolean.Default)
                    {
                        //发生异常，关闭界面
                        InvokeLoginEvent(false);
                        this.Dispose();
                    }
                };

                worker.RunWorkerAsync();
            }

        }

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="message"></param>
        private void ShowMessage(string message)
        {
            this.messageView.Visibility = Visibility.Visible;
            this.message.Text = message;
        }

        /// <summary>
        /// 显示登陆框
        /// </summary>
        private void DisplayLoginView()
        {
            this.logining.Visibility = Visibility.Collapsed;
            this.logView.Visibility = Visibility.Visible;
            this.password.Focus();
        }

        /// <summary>
        /// 显示功能模块列表 
        /// </summary>
        private void DisplyOffice()
        {
            this.messageView.Visibility = Visibility.Collapsed;
            this.officeView.Visibility = Visibility.Visible;
            this.logView.Visibility = Visibility.Collapsed;
            this.logining.Text = "选择登陆模块。";
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

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                loginButton_MouseLeftButtonDown(this, null);
            }
        }

        /// <summary>
        /// 清空资源，并关闭界面
        /// </summary>
        public void Dispose()
        {
            if (_loadingStoryboard != null)
                _loadingStoryboard.Stop();

            this.Close();
        }

        /// <summary>
        /// 暂停动画
        /// </summary>
        public void Pause()
        {
            if (_loadingStoryboard != null)
                _loadingStoryboard.Pause();
        }

        private void ShowForm(string key)
        {
            if (this._formDict.ContainsKey(key))
            {
                ToRunAppNames = key.Trim();

                #region MyRegion

                /*
                switch (key) {
                    case "病患管理":
                        using (BackgroundWorker worker = new BackgroundWorker()) {
                            worker.DoWork += (o1, e1) => {
                                var frm = new PatientMgrFrm();
                                frm.Show();
                            };
                            worker.RunWorkerCompleted += (o2, e2) => { this.Close(); };
                        }

                        break;
                    case "病患治疗":
                        using (BackgroundWorker worker = new BackgroundWorker()) {
                            worker.DoWork += (o1, e1) => {
                                var frm = new PatientTreantmentFrm();
                                frm.Show();
                            };
                            worker.RunWorkerCompleted += (o2, e2) => { this.Close(); };
                        }
                        break;
                    case "病患排班":
                        using (BackgroundWorker worker = new BackgroundWorker()) {
                            worker.DoWork += (o1, e1) => {
                                var frm = new PatientScheduleFrm();
                                frm.Show();
                            };
                            worker.RunWorkerCompleted += (o2, e2) => { this.Close(); };
                        }


                        break;
                    case "程序设置":
                        using (BackgroundWorker worker = new BackgroundWorker()) {
                            worker.DoWork += (o1, e1) => {
                                var frm = new frmMain();
                                frm.Show();
                            };
                            worker.RunWorkerCompleted += (o2, e2) => { this.Close(); };
                        }


                        break;
                    case "统计报表":
                        using (BackgroundWorker worker = new BackgroundWorker()) {
                            worker.DoWork += (o1, e1) => {
                                var frm = new ReportMainFrm();
                                frm.Show();
                            };
                            worker.RunWorkerCompleted += (o2, e2) => { this.Close(); };
                        }
                        break;
                    default:
                        break;
                }
               * */
                #endregion
            }
            else
            {
                this.logining.Text = "模块功能与权限名称不匹配！";
            }
        }

        /// <summary>
        /// 报靠消息
        /// </summary>
        /// <param name="loadingOffice"></param>
        private void ReportMessage(bool loadingOffice)
        {
            if (loadingOffice)
            {
                this.officeView.Visibility = Visibility.Collapsed;
                this.logining.Text = string.Format("欢迎您 {0}。", LoginUser.User.USER_NAME);
            }

            this.logining.Visibility = Visibility.Visible;

            var translateAnimation = new DoubleAnimation();
            translateAnimation.To = -5;
            translateAnimation.From = 0;
            translateAnimation.Duration = TimeSpan.FromSeconds(.20);
            translateAnimation.AccelerationRatio = 0.3;
            translateAnimation.DecelerationRatio = 0.7;
            translateAnimation.FillBehavior = FillBehavior.Stop;
            translateAnimation.Completed += delegate
            {
                this.logining.Text += "。";
                loggingTranslateTransform.Y = -5;
                if (loadingOffice)
                {
                    Pause();
                    InvokeLoginEvent(true);
                }
            };

            loggingTranslateTransform.BeginAnimation(TranslateTransform.YProperty, translateAnimation, HandoffBehavior.SnapshotAndReplace);
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
                this.userName.ItemsSource = System.IO.File.ReadAllLines(txt);
            }
            catch
            {
                // eat 
            }
        }

        /// <summary>
        /// 记录登陆用户名到本地文本文件
        /// </summary>
        private void AppendLoginNamesToTxt(string name)
        {
            var txt = GetTxtPath();
            try
            {
                if (System.IO.File.Exists(txt))
                {
                    var items = System.IO.File.ReadAllLines(txt);
                    if (items != null && items.Contains(name))
                        return;
                    if (items.Length > 0)
                        name = Environment.NewLine + name;
                }
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

        #endregion
    }
}
