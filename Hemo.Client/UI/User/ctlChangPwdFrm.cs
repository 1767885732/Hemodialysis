/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:密码修改类
 * 创建标识:贺建操-2017年3月7日
 * ----------------------------------------------------------------*/

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
using Hemo.Client.UI.Machine;

namespace Hemo.Client.UI.User {
    public partial class ctlChangPwdFrm :ViewBase
    {
        #region 类变量

        private string _Userid;
        private IUser _userService = ServiceManager.Instance.UserService;

        #endregion

        #region 构造函数

        public ctlChangPwdFrm() {
            InitializeComponent();
        }

        public ctlChangPwdFrm(string Userid)
        {
            InitializeComponent();
            _Userid =  Userid;
        }

        #endregion

        #region 事件

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Confirm_Click(object sender, EventArgs e) {
            if (Verification())
            {
                try
                {
                    LoginValidata();
                }
                catch (Exception ex)
                {
                    AutoClosedMsgBox.ShowForm(ex.Message, "错误", 1000, MessageBoxIcon.Warning);
                }
            }
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region 方法

        /// <summary>
        /// 旧密码验证
        /// </summary>
        private void LoginValidata() {
            PermissionModel.MED_USERSDataTable _USERSDataTable = new PermissionModel.MED_USERSDataTable();
            _USERSDataTable = _userService.GetUserList();
            PermissionModel.MED_USERSRow _USERSRow = _USERSDataTable.FindByUSER_ID(_Userid);

            PermissionModel.MED_USERSDataTable loginUser = _userService.VerifyUserLogin(_USERSRow.LOGIN_NAME.Trim().ToUpper(),
                   Utility.Encrypto(OldPwd.Text.Trim().ToUpper()));

            if (loginUser.Rows.Count == 1) {
                //if (_USERSRow != null) {
                loginUser.Rows[0]["LOGIN_PWD"] = Utility.Encrypto(NewPwd.Text.Trim().ToUpper());
                //}

                int result = _userService.UpdateMedUsers(loginUser);
                if (result == 1) {
                    AutoClosedMsgBox.ShowForm("修改成功", "提示", 1000, MessageBoxIcon.Information);

               
                }
            }
            else {
                AutoClosedMsgBox.ShowForm("密码错误,请重新输入", "提示", 1000, MessageBoxIcon.Warning);

            }
           
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <returns></returns>
        private bool Verification() {
            if (string.IsNullOrEmpty(OldPwd.Text.Trim())) {
                AutoClosedMsgBox.ShowForm("请输入旧密码", "提示", 1000, MessageBoxIcon.Warning);

                OldPwd.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(NewPwd.Text.Trim())) {
                AutoClosedMsgBox.ShowForm("请输入新密码", "提示", 1000, MessageBoxIcon.Warning);

                NewPwd.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(ConfirmPwd.Text.Trim())) {

                AutoClosedMsgBox.ShowForm("请输入确认密码", "提示", 1000, MessageBoxIcon.Warning);

                ConfirmPwd.Focus();
                return false;
            }
            if (NewPwd.Text.Trim() != ConfirmPwd.Text.Trim()) {
                AutoClosedMsgBox.ShowForm("两次输入的密码不对应。", "提示", 1000, MessageBoxIcon.Warning);

                ConfirmPwd.Focus();
                return false;
            }

            return true;

        }

        #endregion
    }
}