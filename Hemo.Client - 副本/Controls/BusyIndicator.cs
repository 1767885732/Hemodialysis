/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司股份有限公司
// 文件名：BusyIndicator.cs
// 文件功能描述：进度条控件 
// 创建标识：刘超 2013-07-22
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;

namespace Hemo.Client.Controls {
    public partial class BusyIndicator : UserControl {
        public BusyIndicator() {
            InitializeComponent();
        }
        /// <summary>
        /// 显示位置设置
        /// </summary>
        /// <param name="control"></param>
        public void ShowLoadingScreenFor(Control control) {
            this.Left = (control.Width - this.Width) / 2 + control.Left;
            this.Top = (control.Height - this.Height) / 2 + control.Top;
            this.Height = 40;
            this.Width = 100;
            this.Visible = true;
        }

        public void HideLoadingScreen() {
            this.Visible = false;
        }
    }
    public  class BusyIndicatorHelp
    {
        #region SplashScreenManager

        private  SplashScreenManager _loadForm;
        /// <summary>
        /// 等待窗体管理对象
        /// </summary>
        protected  SplashScreenManager LoadForm
        {
            get
            {
                if (_loadForm == null)
                {
                    this._loadForm = new SplashScreenManager(new Form(), typeof(FrmWaitForm), true, true);
                    //this._loadForm.CloseWaitForm();.ClosingDelay = 0;
                }
                return _loadForm;
            }
        }

        public void SetWaitFormCaption(string caption)
        {
            this.LoadForm.SetWaitFormCaption(caption);          
        }
        /// <summary>
        /// 显示等待窗体
        /// </summary>
        public  void ShowMessage()
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
        public  void HideMessage()
        {
            bool isSplashFormVisible = this.LoadForm.IsSplashFormVisible;
            if (isSplashFormVisible)
            {
                this.LoadForm.CloseWaitForm();
            }
        }

        #endregion
    }
}
