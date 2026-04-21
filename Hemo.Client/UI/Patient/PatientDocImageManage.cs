/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者电子扫描件管理类
// 创建时间：2016-08-4
// 创建者：刘超
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
using System.IO;
using DevExpress.XtraEditors;
using Hemo.Client.UI.PatientFixUI;
using Hemo.IService;
using Hemo.Service;
using Hemo.Model;

namespace Hemo.Client.UI.Patient
{
    public partial class PatientDocImageManage : XtraForm
    {
        #region 变量

        private PatientModel.MED_PATIENTSRow currenPatientsRow = null;

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hemoId"></param>
        public PatientDocImageManage(PatientModel.MED_PATIENTSRow patientRow)
        {
            InitializeComponent();

            currenPatientsRow = patientRow;
            this.Text = string.Format("患者-{0}的电子扫描件管理", patientRow.NAME);

        }
        #endregion

        #region 事件

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientDocImageManage_Load(object sender, EventArgs e)
        {
            PatientDocImageUI patientDocImageUi = new PatientDocImageUI(currenPatientsRow);
            this.Controls.Add(patientDocImageUi);
        }

        #endregion
    }
}
