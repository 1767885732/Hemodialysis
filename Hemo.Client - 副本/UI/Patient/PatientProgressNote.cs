/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司
// 描述：患者病程记录窗体类
// 创建时间：2015-7-20
// 创建者：吕志强
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
using Hemo.IService.Config;
using Hemo.Model;
using Hemo.Service;
using Hemo.IService.Dict;
using Hemo.Utilities;
using Hemo.Client.Core;
using Hemo.Client.Print;
using Hemo.IService;
using System.Linq;
using Hemo.Client.UI.Hemodialysis;
using Hemo.Client.UI.PatientFixUI;

namespace Hemo.Client.UI.Patient
{
    public partial class PatientProgressNote : HemoBaseFrm
    {
        #region 类变量

        private string currentHemoId = string.Empty;

        #endregion

        #region 构造函数

        public PatientProgressNote(string hemoId)
        {
            InitializeComponent();
            currentHemoId = hemoId;
        }

        #endregion

        #region 事件

        private void PatientProgressNote_Load(object sender, EventArgs e)
        {
            PatientProgressNoteUI frm = new PatientProgressNoteUI(currentHemoId);
            frm.Dock = DockStyle.Fill;
            this.Controls.Add(frm);
        }

        #endregion
    }
}