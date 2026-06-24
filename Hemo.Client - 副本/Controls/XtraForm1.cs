/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:测试控件
 * 创建标识:吕志强-2017年1月30日
 * 
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hemo.Client.Controls
{
    /// <summary>
    /// 用于系统 测试
    /// </summary>
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm1()
        {
            InitializeComponent();
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ctlFirstPageView1.InzationData("10000022");

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.flyoutPanel1.ShowPopup();
        }
    }
}