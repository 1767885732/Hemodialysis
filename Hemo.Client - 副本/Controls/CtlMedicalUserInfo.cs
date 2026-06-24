/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司股份有限公司
// 文件名：CtlMedicalUserInfo.cs
// 文件功能描述： 查询条件
// 创建标识：刘超 2013-07-22
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.IService;
using Hemo.Service;
using Hemo.Utilities;

namespace Hemo.Client.Controls
{
    public partial class CtlMedicalUserInfo : DevExpress.XtraEditors.XtraUserControl
    {
        #region 成员变量

        private string hemoId = string.Empty;

        private PatientModel.MED_PATIENTSDataTable dtPatient = null;

        private IPatient patientService = ServiceManager.Instance.PatientService;

        #endregion

        #region

        public string HemoId
        {
            get { return hemoId; }
            set { hemoId = value; }
        }

        #endregion

        #region 构造函数

        public CtlMedicalUserInfo()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载用户信息
        /// </summary>
        public void LoadUserInfo()
        {
            dtPatient = patientService.GetPatientListByParams(string.Empty, hemoId);
            BaseControlInfo.SetControlDataByDataTable(dtPatient, this.panelControl1);
            BaseControlInfo.SetControlEnabled(this.panelControl1, false);
        }

        #endregion
    }
}
