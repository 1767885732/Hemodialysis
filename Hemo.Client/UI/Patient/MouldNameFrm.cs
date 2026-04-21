/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：模板名称维护类
// 创建时间：2017-03-15
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
    public partial class MouldNameFrm :HemoBaseFrm
    {
        #region 类变量

        private string _mouldName;

        #endregion

        #region 属性

        public string MouldName
        {
            get { return this.textEdit_MouldName.Text.Trim(); }
        }

        #endregion

        #region 构造函数

        public MouldNameFrm()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        #endregion
    }
}