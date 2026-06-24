/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：程序启动文件
// 创建时间：2013-07-08
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.UI.Config;
using Hemo.Client.UI.Drug;
using Hemo.Client.UI.Hemodialysis;
using Hemo.Client.UI.Dict;
using Hemo.Client.UI.Patient;
using Hemo.Client.UI.Material;
using Hemo.Client.UI.Machine;
using Hemo.Client.UI.HemodialysisApply;
using Hemo.Client.UI.PatientSchedule;
using Hemo.Client;
using Hemo.Client.UI.ReportChart;
using Hemo.Client.Core;
using Medicalsystem.Docare.Updater.Connection;
using Medicalsystem.Auth.Client;
using Hemo.Client.UI.DataReport;
using Hemo.Client.Base.XtraBaseInfo;

namespace Hemo.WinForm {
    static class Program
    {
        #region 属性

        public static ModuleType CurrentModuleType { get; set; }

        public static MainFrm MainForm { get; set; }

        #endregion

        #region 构造函数

        #endregion

        #region 事件

        /// <summary>
        /// 应用程序退出时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ApplicationExit(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 应用程序域未处理的异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            MessageBox.Show(exception.Message, "系统提醒", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        /// <summary>
        /// UI线程的异常处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {

            MessageBox.Show(string.Format("请输入正确内容，详情如下：\r\n" + e.Exception.Message), "系统提醒", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CHS");
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Office 2013");
            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            //try
            //{
            //    DAuthContext.Init(8);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            string appId = "53beb378-30de-4788-bf1f-334a9fe669c8";
            AutoUpdater.Run(appId);

            var login = new LoginFrm();
            SetFormMax(login);
            login.ShowDialog();

            if (login.DialogResult == DialogResult.OK)
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(SplashScreen1));
                var mainfrom = new MainFrm();
                SetFormMax(mainfrom);
                MainFrm.viewModel.SelectModule(CurrentModuleType);
                Application.Run(mainfrom);

            }
            else
            {
                System.Windows.Forms.Application.ExitThread();
            }


            #region// 上一个版本的登录方式

            ////LoginScreen frm = new LoginScreen();
            ////frm.LoginEvent += delegate(object sender, LoginEventArgs e)
            ////{
            ////    EditTreatment frme = new EditTreatment(string.Empty, string.Empty, 0, 0);
            ////    frme.IsCahLoad = true;
            ////    frme.Show();
            ////    frm.Dispose();
            ////    Show(e.RunApp, e.RunAppNames);
            ////};
            ////frm.ShowDialog();

            #endregion
        }

        public static void SetFormMax(XtraForm frm)
        {
            frm.Top = 0;
            frm.Left = 0;
            frm.Width = Screen.PrimaryScreen.WorkingArea.Width;
            frm.Height = Screen.PrimaryScreen.WorkingArea.Height;
        }

        #region 上一个版本的登录模块切换

        public static bool HideClose = false;
        public static void Show(bool RunApp, string RunAppNames)
        {
            if (RunApp && RunAppNames.Length > 0)
            {
                switch (RunAppNames)
                {
                    case "病患管理":
                        var frmRun = new PatientMgrFrm();
                        frmRun.ShowDialog();
                        break;
                    case "病患治疗":
                        var frmRun1 = new PatientTreantmentFrm();
                        frmRun1.ShowDialog();

                        break;
                    case "病患排班":
                        var frmRun2 = new PatientScheduleFrmNew();
                        frmRun2.ShowDialog();
                        break;
                    case "程序设置":
                        var frmRun3 = new frmMain();
                        frmRun3.ShowDialog();
                        break;
                    case "统计报表":
                        var frmRun4 = new ReportMainFrm();
                        frmRun4.ShowDialog();
                        break;
                    case "设备管理":
                        var frmRun5 = new EqumentInfoManager();
                        frmRun5.ShowDialog();
                        break;
                    case "主任工作站":
                        var frmRun6 = new PatientTreantmentForDirectorFrm();
                        frmRun6.ShowDialog();
                        break;
                    case "患者上报管理":
                        var frmRun7 = new DataReportManager();
                        frmRun7.ShowDialog();
                        break;
                    default:
                        Application.Exit();
                        break;
                }
                if (HideClose)
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                //异常出错，退出系统
                Environment.Exit(0);
            }
        }

        #endregion

        #endregion
    }
}
