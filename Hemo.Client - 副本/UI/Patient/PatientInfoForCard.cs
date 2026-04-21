/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者卡片信息类
// 创建时间：2016-03-14
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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Service;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.Client.UI.Machine;


namespace Hemo.Client.UI.Patient
{
    /// <summary>
    /// 界面的基类
    /// </summary>
    [ToolboxItem(true)]
    public partial class PatientInfoForCard : ViewBase
    {
        #region 类变量

        public event EventHandler GetHemoEventHandler;

        private PatientService objPatient = new PatientService();

        private PatientModel.MED_PATIENTSDataTable _patientDataTable;

        #endregion

        #region 属性

        public string hemoID { get; set; }

        #endregion

        #region 构造函数

        public PatientInfoForCard()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void txtHEMODIALYSIS_ID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string hemoIds = string.IsNullOrEmpty(this.txtHEMODIALYSIS_ID.Text.Trim()) ? this.txtPATIENT_ID.Text : this.txtHEMODIALYSIS_ID.Text;

                string patientName = this.txtNAME.Text.Trim();
                _patientDataTable = objPatient.GetPatientListByParams(patientName, hemoIds);
                BaseControlInfo.SetControlDataByDataTable(_patientDataTable, panControl);
                this.hemoID = hemoIds;
                if (GetHemoEventHandler != null)
                    GetHemoEventHandler(sender, e);
            }
        }

        #endregion

        #region 方法

        public void InzationData(string hemoID)
        {
            _patientDataTable = objPatient.GetPatientListByParams(string.Empty, hemoID);
            BaseControlInfo.SetControlDataByDataTable(_patientDataTable, panControl);
            this.hemoID = hemoID;
            if (GetHemoEventHandler != null)
                GetHemoEventHandler(null, null);
        }

        #endregion

        private void txtHEMODIALYSIS_ID_EditValueChanged(object sender, EventArgs e)
        {
            this.txtNAME.Text = string.Empty;
        }
    }
}
