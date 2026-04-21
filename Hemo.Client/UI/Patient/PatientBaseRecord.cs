/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者病历窗体类
// 创建时间：2015-07-10
// 创建者：吕志强
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/

using System;
using System.Data;
using Hemo.Model;
using Hemo.Service;
using Hemo.IService;
using System.Windows.Forms;
using Hemo.IService.Config;
using Hemo.Utilities;
using DevExpress.XtraEditors;
using Hemo.Client.Print;
using Hemo.Client.UI.PatientFixUI;

namespace Hemo.Client.UI.Patient
{
    public partial class PatientBaseRecord : HemoBaseFrm
    {
        #region 类变量

        private string hemoId = string.Empty;

        #endregion

        #region 属性

        /// <summary>
        /// 透析编号
        /// </summary>
        public string HemoId
        {
            get { return hemoId; }
            set { hemoId = value; }
        }

        #endregion

        #region 构造函数

        public PatientBaseRecord()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientBaseRecord_Load(object sender, EventArgs e)
        {
            PatientBaseRecordUI uc = new PatientBaseRecordUI();
            uc.HemoId = this.hemoId;
            this.Controls.Add(uc);
        }

        #endregion
    }
}