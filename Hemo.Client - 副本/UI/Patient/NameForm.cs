/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：名称设置类
// 创建时间：2015-07-10
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hemo.Client.UI.Patient
{
    public partial class NameForm : DevExpress.XtraEditors.XtraForm
    {
        #region 属性

        public string EditValue
        {
            get
            {
                return this.textEdit1.Text.Trim();
            }
            set
            {
                this.textEdit1.Text = value;
                this.textEdit1.Focus();
                this.textEdit1.SelectAll();
            }
        }

        #endregion

        #region 构造函数

        public NameForm()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void iCancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void iOkButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textEdit1.Text) || string.IsNullOrEmpty(this.textEdit1.Text.Trim()))
            {
                this.dxErrorProvider1.SetError(this.textEdit1, "请输入名称");
                this.textEdit1.Focus();
                return; ;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void NameForm_Load(object sender, EventArgs e)
        {
            this.textEdit1.Focus();
            this.textEdit1.SelectAll();
        }

        #endregion
    }
}