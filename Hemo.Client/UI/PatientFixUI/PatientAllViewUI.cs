/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：透析充分性评估窗体类
// 创建时间：2017-05-18
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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.IService;
using Hemo.Service;
using Hemo.IService.Config;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.Client.Doc;
using Hemo.Client.UI.Machine;


namespace Hemo.Client.UI.Patient
{
    public partial class PatientAllViewUI : ViewBase
    {
        #region 类变量

        public string CurrentHemoId { get; set; }

        #endregion

        #region 构造函数

        public PatientAllViewUI()
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
        private void PatientAllViewUI_Load(object sender, EventArgs e)
        {
            //InzationData();
        }

        public void InzationData()
        {
            this.ctlFirstPageView1.InzationData(CurrentHemoId);            
        }
        #endregion

    }
}