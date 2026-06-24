/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:病人手术操作控件
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Client.Modules.Patient;
using DevExpress.XtraEditors;

namespace Hemo.Client.Controls.Schedule
{
    public partial class FormPatientOperatorUI : XtraUserControl
    {
        public FormPatientOperatorUI()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 加载 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormPatientOperatorUI_Load(object sender, EventArgs e)
        {
            PatientOperatorUI f = new PatientOperatorUI();
            f.Dock = DockStyle.Fill;
            this.panelControl2.Controls.Add(f);
        }
    }
}
