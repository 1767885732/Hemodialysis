/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：登录欢迎用户控件
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
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Navigation;
using Hemo.Client.UI.User;
using Hemo.Client.Core;
using DevExpress.XtraBars.Docking2010.Customization;

namespace Hemo.Client
{
    public partial class LoginFrmDtl : DevExpress.XtraEditors.XtraUserControl
    {
        #region 类变量

        private string _username;

        #endregion

        #region 属性

        public static string DayOfWeek
        {
            get
            {
                switch (DateTime.Now.DayOfWeek.ToString("D"))
                {
                    case "0":
                        return "星期日 ";
                    case "1":
                        return "星期一 ";
                    case "2":
                        return "星期二 ";
                    case "3":
                        return "星期三 ";
                    case "4":
                        return "星期四 ";
                    case "5":
                        return "星期五 ";
                    case "6":
                        return "星期六 ";
                    default:
                        return "星期日";
                }
            }
        }

        #endregion

        #region 构造函数

        #endregion

        #region 事件

        private void labelControl3_Click(object sender, EventArgs e)
        {

            var y = (LoginFrm)this.Parent.Parent.Parent.Parent.Parent.Parent.Parent;
            y.transitionManager1.StartTransition(y.ctlT);
            y.AllBtnEnabled(false);

            var x = (NavigationFrame)this.Parent.Parent;
            y.transitionManager1.EndTransition();
            x.Parent = null;

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void LoginFrmDtl_Load(object sender, EventArgs e)
        {
            this.labelControl1.Text = string.Format("你好，{0} 欢迎登陆！", _username);
            this.labelControl2.Text = string.Format("{0}  {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm"), DayOfWeek);
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            ChangePassWord();
        }

        private void labelControl4_Click(object sender, EventArgs e)
        {
            ChangePassWord();
        }

        #endregion

        #region 方法

        public LoginFrmDtl(string username)
        {
            InitializeComponent();
            _username = username;
        }

        private void ChangePassWord()
        {
            FlyoutDialog.Show(this.FindForm(), new ctlChangPwdFrm(HemoApplicationContext.Current.CurrentUser.USER_ID));

            //ChangPwdFrm frm = new ChangPwdFrm(HemoApplicationContext.Current.CurrentUser.USER_ID);
            //frm.ShowDialog();
        }

        #endregion
    }
}
