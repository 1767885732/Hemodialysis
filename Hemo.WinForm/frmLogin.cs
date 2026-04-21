using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Permission;
using Hemo.Service;
using Hemo.Model;
using Hemo.Utilities;

namespace Hemo.WinForm
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        private IUser _userService = ServiceManager.Instance.UserService;

        public frmLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 使控件不可点击
        /// </summary>
        /// <param name="isEnabled"></param>
        private void EnabledControl(bool isEnabled)
        {
            btnLogin.Enabled = isEnabled;
            btnCancel.Enabled = isEnabled;
            devtxtLoginName.Enabled = isEnabled;
            devtxtPwd.Enabled = isEnabled;

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(devtxtLoginName.Text.Trim()))
            {
                devtxtLoginName.Focus();
                errorProvider1.SetError(devtxtLoginName, "请输入用户名");
                return;
            }

            if (string.IsNullOrEmpty(devtxtPwd.Text.Trim()))
            {
                devtxtPwd.Focus();
                errorProvider1.SetError(devtxtPwd, "请输入密码");
                return;
            }
            this.picLoading.Visible = true;
            EnabledControl(false);
            InitalizeData();
        }
        /// <summary>
        /// 登录初始化
        /// </summary>
        private void InitalizeData()
        {
            bool isLogin = false;
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    PermissionModel.MED_USERSDataTable loginUser = _userService.VerifyUserLogin(this.devtxtLoginName.Text.Trim().ToUpper(), 
                       Utility.Encrypto(devtxtPwd.Text.Trim().ToUpper()));
                    if (loginUser.Rows.Count > 0)
                    {
                        isLogin = true;
                    }
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    if (isLogin)
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.picLoading.Visible = false;
                    }
                    else
                    {
                        this.picLoading.Visible = false;
                        EnabledControl(true);
                        XtraMessageBox.Show("用户名或者密码错误！");
                    }
                };
                worker.RunWorkerAsync();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void devtxtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnLogin.PerformClick();
            }
        }
    }
}