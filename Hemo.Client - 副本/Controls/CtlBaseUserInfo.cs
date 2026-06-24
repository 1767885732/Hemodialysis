/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：病历用户信息控件类
// 创建时间：2015-07-31
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
using Hemo.IService;
using Hemo.Service;
using Hemo.Utilities;

namespace Hemo.Client.Controls
{
    public partial class CtlBaseUserInfo : DevExpress.XtraEditors.XtraUserControl
    {
        #region 类变量

        private string hemoId = string.Empty;

        private DateTime beginDate;

        private PatientModel.MED_PATIENTSDataTable dtPatient = null;

        private IPatient patientService = ServiceManager.Instance.PatientService;

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

        /// <summary>
        /// 透析开始时间
        /// </summary>
        public DateTime BeginDate
        {
            get { return beginDate; }
        }

        #endregion

        #region 构造函数

        public CtlBaseUserInfo()
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
            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                this.txtName.Text = dtPatient[0].NAME;
                this.cboSex.EditValue = dtPatient[0].SEX;
                this.txtBirthday.DateTime = dtPatient[0].BIRTHDAY;
                this.txtHemoId.Text = dtPatient[0].HEMODIALYSIS_ID;
                this.txtJob.Text = dtPatient[0].JOB;
                this.cboMarriage.EditValue = dtPatient[0].MARITAL;
                this.cboEducate.EditValue = dtPatient[0].EDUCATION;
                this.txtWorkPhone.Text = dtPatient[0].WORK_TELEPHONE;
                this.txtAddress.Text = dtPatient[0].ADDRESS;
                this.txtPhone.Text = dtPatient[0].TELEPHONE;
                this.txtContact.Text = dtPatient[0].NATIVEPLACE;
                this.txtDiagnose.Text = dtPatient[0].DIAGNOSE;
                beginDate = dtPatient[0].SPECIFIC_TIME;
            }
            BaseControlInfo.SetControlEnabled(this.panelControl1, false);
            txtDiagnose.Enabled = true;
        }

        #endregion
    }
}
