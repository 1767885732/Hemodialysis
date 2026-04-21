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
using System.ComponentModel;
using System.Threading;
using System.Web.Script.Serialization;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using DevExpress.Utils;
using Hemo.Utilities;
using System.Data;
using Hemo.Model;
using Hemo.IService.Permission;
using Hemo.Service;
using Hemo.Client.Core;

namespace Hemo.Client.Login
{
    /// <summary>
    /// 登陆
    /// </summary>
    public partial class LoginForm : Window
    {
        Storyboard _storyboard = null;
        public EventHandler<LoginEventArgs> LoginEvent;

        ListCollectionView _collectionView = null;

        public static DefaultBoolean IsLogin = DefaultBoolean.Default;
        private IUser _userService = ServiceManager.Instance.UserService;

        public LoginForm()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLoginNamesFromTxt();
            this.userName.Focus();
            //this.Topmost = true;
            this.passwordWatermark.Visibility = Visibility.Collapsed;
            this.messageView.Visibility = Visibility.Collapsed;



        }
       


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Application.Current.Shutdown();
            else if (e.Key == Key.Enter)
            {
                if (loginView.Visibility == Visibility.Visible)
                    Login();
                else if (this.officeView.Visibility == Visibility.Visible)
                    ok_MouseLeftButtonDown(this, null);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
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
        /// 登陆
        /// </summary>
        private void Login()
        {
            this.message.Text = string.Empty;

            var userName = this.userName.Text.ToUpper();
            var password = this.password.Password.ToUpper();

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

            //运行动画
            RunSplashScreen();


           System.Data.DataTable office = null;
            //AppContext.Current.UserIdentify = null;

            HemoApplicationContext.Current.CurrentUser = null;
            ReportProgress("正在验证帐号 . . .");
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                       //进行普通登陆验证
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
                        #region 验证成功后去获取用户的所有登陆科室
                        if (IsLogin == DefaultBoolean.True)
                        {
                            AppendLoginNamesToTxt(userName);
                            office = this._userService.GetPermissionListByUserID(LoginUser.User.USER_ID);
                            HemoApplicationContext.Current.RolesOffices = office;
                            HemoApplicationContext.Current.CurrentUser = LoginUser.User;
                            //执行每日排班的存储过程、写入所在星期的一周数据
                            //this._userService.ExecuteScheduleProceduce();

                        }
                    }
                    catch (Exception ex)
                    {
                        IsLogin = DefaultBoolean.Default;
                        Logger.WriteErrorLog(ex);
                    }
                    #endregion

                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    if (e2.Error != null)
                    {
                        IsLogin = DefaultBoolean.Default;
                        this.Topmost = false;
                        Logger.WriteErrorLog(e2.Error);
                    }
                    if (IsLogin == DefaultBoolean.True)
                    {
                        if (office.Rows.Count == 1)
                        {
                            var officeRow = office.Rows[0];
                            OfficeSelected(officeRow);

                        }
                        else if (office.Rows.Count > 1)
                        {
                            this.officeView.Visibility = Visibility.Visible;
                            this.officeView.IsHitTestVisible = true;
                            _collectionView = new ListCollectionView(office.AsEnumerable().ToList());
                            _collectionView.Filter = new Predicate<object>(AutoComplete);
                            this.officeItems.ItemsSource = _collectionView;
                            //this.officeItems.ItemsSource = office.DefaultView;
                            this.officeTextBox.Focus();
                            //暂停动画
                            if (_storyboard != null)
                                _storyboard.Pause(this);
                            this.loadingView.Visibility = Visibility.Collapsed;
                            ReportProgress("选择登陆科室 . . .");
                        }
                        else
                        {
                            ShowMessage("您未分配登陆科室,登陆失败!");
                            this.loginView.Visibility = Visibility.Visible;
                            this.loginView.IsHitTestVisible = true;
                            //停止动画
                            if (_storyboard != null)
                                _storyboard.Stop();
                            this.loadingView.Visibility = Visibility.Collapsed;
                        }


                    }
                    else if (IsLogin == DefaultBoolean.False)
                    {
                        ShowMessage("用户名/密码错误!");
                        this.loginView.Visibility = Visibility.Visible;
                        this.loginView.IsHitTestVisible = true;
                        //停止动画
                        if (_storyboard != null)
                            _storyboard.Stop(this);
                        this.loadingView.Visibility = Visibility.Collapsed;
                        this.password.Focus();
                        //this.userName.Focusable = true;
                    }
                    else if (IsLogin == DefaultBoolean.Default)
                    {
                        //发生异常，关闭界面
                        if (LoginEvent != null)
                            LoginEvent(this, new LoginEventArgs { RunApp = false,RunAppNames ="NONO" });

                        Dispose();
                    }
                };

                worker.RunWorkerAsync();
            }

        }
        private bool AutoComplete(object item)
        {
            if (string.IsNullOrEmpty(this.officeTextBox.Text))
                return true;
            DataRow current = item as DataRow;
            return current["PERMISSIONNAME"].ToString().StartsWith(this.officeTextBox.Text);
            //|| current.DEPT_NAME.StartsWith(this.officeTextBox.Text)
            //    || current.INPUT_CODE.StartsWith(this.officeTextBox.Text);
        }
        /// <summary>
        /// 运行登陆动画
        /// </summary>
        private void RunSplashScreen()
        {
            this.loadingView.Visibility = Visibility.Visible;
            this.loginView.Visibility = Visibility.Collapsed;

            if (_storyboard != null && _storyboard.GetIsPaused(this))
            {
                _storyboard.Resume();
                return;
            }

            DoubleAnimationUsingKeyFrames timelines1 = new DoubleAnimationUsingKeyFrames();
            timelines1.Duration = TimeSpan.FromSeconds(2.00);
            timelines1.BeginTime = TimeSpan.FromSeconds(0);
            timelines1.RepeatBehavior = RepeatBehavior.Forever;
            timelines1.FillBehavior = FillBehavior.Stop;
            timelines1.SpeedRatio = 0.5;
            timelines1.AccelerationRatio = 0.3;
            timelines1.DecelerationRatio = 0.7;
            timelines1.KeyFrames.Add(new LinearDoubleKeyFrame(this.ActualWidth * 0.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.0))));
            timelines1.KeyFrames.Add(new LinearDoubleKeyFrame(this.ActualWidth, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.0))));

            DoubleAnimationUsingKeyFrames timelines2 = new DoubleAnimationUsingKeyFrames();
            timelines2.Duration = TimeSpan.FromSeconds(2.00);
            timelines2.BeginTime = TimeSpan.FromSeconds(0);
            timelines2.RepeatBehavior = RepeatBehavior.Forever;
            timelines2.FillBehavior = FillBehavior.Stop;
            timelines2.SpeedRatio = 0.5;
            timelines2.AccelerationRatio = 0.3;
            timelines2.DecelerationRatio = 0.7;
            timelines2.KeyFrames.Add(new LinearDoubleKeyFrame(this.ActualWidth * 0.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.3))));
            timelines2.KeyFrames.Add(new LinearDoubleKeyFrame(this.ActualWidth, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.0))));

            DoubleAnimationUsingKeyFrames timelines3 = new DoubleAnimationUsingKeyFrames();
            timelines3.Duration = TimeSpan.FromSeconds(2.00);
            timelines3.BeginTime = TimeSpan.FromSeconds(0);
            timelines3.RepeatBehavior = RepeatBehavior.Forever;
            timelines3.FillBehavior = FillBehavior.Stop;
            timelines3.SpeedRatio = 0.5;
            timelines3.AccelerationRatio = 0.3;
            timelines3.DecelerationRatio = 0.7;
            timelines3.KeyFrames.Add(new LinearDoubleKeyFrame(this.ActualWidth * 0.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.6))));
            timelines3.KeyFrames.Add(new LinearDoubleKeyFrame(this.ActualWidth, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.0))));

            NameScope.SetNameScope(this, new NameScope());

            this.RegisterName("dot1", this.dot1);
            this.RegisterName("dot2", this.dot2);
            this.RegisterName("dot3", this.dot3);

            Storyboard.SetTargetName(timelines1, "dot1");
            Storyboard.SetTargetProperty(timelines1, new PropertyPath(TranslateTransform.XProperty));

            Storyboard.SetTargetName(timelines2, "dot2");
            Storyboard.SetTargetProperty(timelines2, new PropertyPath(TranslateTransform.XProperty));

            Storyboard.SetTargetName(timelines3, "dot3");
            Storyboard.SetTargetProperty(timelines3, new PropertyPath(TranslateTransform.XProperty));

            _storyboard = new Storyboard();
            _storyboard.Children.Add(timelines1);
            _storyboard.Children.Add(timelines2);
            _storyboard.Children.Add(timelines3);

            _storyboard.Begin(this, true);

        }
        /// <summary>
        /// 释放资源并关闭动画
        /// </summary>
        public void Dispose()
        {
            if (_storyboard != null)
                _storyboard.Stop(this);

            this.Close();
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
        /// 密码水印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                this.passwordWatermark.Visibility = string.IsNullOrEmpty(this.password.Password) ? Visibility.Visible : Visibility.Collapsed;
                this.messageView.Visibility = Visibility.Collapsed;
            }
            catch { }
        }
        /// <summary>
        /// 科室水印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void offices_TextChanged(object sender, TextChangedEventArgs e)
        {

            this.officeWatermark.Visibility = string.IsNullOrEmpty(this.officeTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
            _collectionView.Refresh();
            if (!this.popup.IsOpen)
            { this.popup.IsOpen = true; }

            officeItems.SelectedIndex = 0;

            if (officeTextBox.Tag != null)
            {
                this.officeView.IsEnabled = false;
                this.officeView.IsHitTestVisible = false;
                using (BackgroundWorker worker = new BackgroundWorker())
                {
                    worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                    {
                        System.Threading.Thread.Sleep(100);
                    };
                    worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                    {
                        OfficeSelected(officeTextBox.Tag as DataRow);

                    };
                    worker.RunWorkerAsync();
                };
            }
        }

        /// <summary>
        /// 确定登陆科室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ok_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(this.officeTextBox.Text))
            {
                this.officeTextBox.Focus();
                return;
            }

            var office = this.officeTextBox.Tag as DataRow;

            if (office == null)
                return;

            OfficeSelected(office);

        }

        /// <summary>
        /// 选择科室
        /// </summary>
        /// <param name="office"></param>
        private void OfficeSelected(System.Data.DataRow office)
        {
            this.officeView.Visibility = Visibility.Collapsed;

            this.loadingView.Visibility = System.Windows.Visibility.Visible;
            if (_storyboard != null && _storyboard.GetIsPaused(this))
                _storyboard.Resume(this);
            this.popup.IsOpen = false;


            ReportProgress(string.Format("{0} . . .", office["PERMISSIONNAME"].ToString()));

            if (LoginEvent != null)
            {

                LoginEvent(this, new LoginEventArgs { RunApp = true, RunAppNames = office["PERMISSIONNAME"].ToString() });

            }

            //this.officeView.IsHitTestVisible = true;
            //this.officeView.IsEnabled = true;
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Login();
        }

        private void minimizeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void closeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Dispose();
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
                //this.userName.Focusable = false;
                this.userName.IsEditable = true;
            }
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

            var path = string.Format(@"{0}\LoginNames.txt", dir);

            if (!System.IO.File.Exists(path))
                System.IO.File.Create(path);
            return path;
        }
        /// <summary>
        /// 关闭下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closePopup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.popup.IsOpen = false;
        }
        /// <summary>
        /// 报告进度
        /// </summary>
        /// <param name="message"></param>
        public void ReportProgress(string message)
        {
            this.messageView.Visibility = Visibility.Visible;
            this.message.Visibility = Visibility.Visible;
            this.message.Text = message;
        }
        /// <summary>
        /// 获得焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void officeTextBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!this.popup.IsOpen)
                this.popup.IsOpen = true;
        }
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (this.popup.IsOpen)
            {
                if (e.Key == Key.Down && officeItems.SelectedIndex < officeItems.Items.Count)
                {
                    officeItems.SelectedIndex++;

                    e.Handled = true;
                }

                if (e.Key == Key.Up && officeItems.SelectedIndex > 0)
                {
                    officeItems.SelectedIndex--;

                    e.Handled = true;
                }

                if (officeItems.SelectedItem != null)
                {
                    officeItems.ScrollIntoView(officeItems.SelectedItem);
                }

            }
            if (e.Key == Key.Enter && officeItems.SelectedItem != null)
            {

                var item = this.officeItems.SelectedItem as DataRow;
                this.officeTextBox.Tag = item;
                this.officeTextBox.Text = item["PERMISSIONNAME"].ToString();
                this.popup.IsOpen = false;
                //     OfficeSelected(item);
                e.Handled = true;

            }
            base.OnPreviewKeyDown(e);
        }

        private void officeTextBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            this.popup.IsOpen = !this.popup.IsOpen;
        }

        private void officeItems_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = this.officeItems.SelectedItem as DataRow;
            this.officeTextBox.Tag = item;
            this.officeTextBox.Text = item["PERMISSIONNAME"].ToString();
            this.popup.IsOpen = false;
            //    OfficeSelected(item);
        }

    }
}
