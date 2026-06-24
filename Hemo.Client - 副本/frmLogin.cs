/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：用户登录窗体
// 创建时间：2016-04-12
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.Core;
using Hemo.IService.Permission;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.Client;

namespace Hemo.WinForm
{
    public partial class frmLogin :HemoBaseFrm
    {
        #region 类变量

        private IUser _userService = ServiceManager.Instance.UserService;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public frmLogin()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

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

        #endregion

        #region 方法

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
                       Utility.Encrypto(devtxtPwd.Text.ToUpper()));

                    if (loginUser.Rows.Count > 0)
                    {
                        LoginUser.SetUserInfo(loginUser.Rows[0] as PermissionModel.MED_USERSRow);

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

        #endregion
    }
}