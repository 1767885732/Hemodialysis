/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者透析卡管理类
// 创建时间：2016-04-23
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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Service;
using Hemo.Model;
using Hemo.Utilities;
using DevExpress.XtraEditors;

namespace Hemo.Client.UI.Patient
{
    public partial class PatientCardOperator : HemoBaseFrm
    {
        #region 属性

        /// <summary>
        /// 当前患者
        /// </summary>
        public string currentHemoId { get; set; }

        #endregion

        #region 构造函数

        public PatientCardOperator()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void PatientCardOperator_Load(object sender, EventArgs e)
        {
            this.patientCardOperatorUI1.currentHemoId = currentHemoId;
            this.patientCardOperatorUI1.InzationData();
        }

        #endregion
    }
}
