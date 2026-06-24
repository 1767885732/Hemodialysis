/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者ID提示类
// 创建时间：2017-06-20
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
using Hemo.IService;
using Hemo.Model;
using Hemo.Service;

namespace Hemo.Client.UI.Patient
{
    public partial class PatientIdFrm : HemoBaseFrm
    {
        #region 类变量

        private IPatient _patientService = ServiceManager.Instance.PatientService;

        public string LastPatientId = string.Empty;

        #endregion

        #region 属性

        public PatientModel.MED_PATIENTSRow _CurrenRow { get; set; }

        #endregion

        #region 构造函数

        public PatientIdFrm()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void PatientIdFrm_Load(object sender, EventArgs e)
        {
            this.txtPatientId.Text = _CurrenRow.PATIENT_ID;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.txtPatientId.Text.Trim().ToUpper().Equals(_CurrenRow.PATIENT_ID.Trim().ToUpper()))
            {
                LastPatientId = _CurrenRow.PATIENT_ID.ToUpper().Trim();
            }
            else
            {
                LastPatientId = this.txtPatientId.Text.ToUpper();
                //var patientDt = _patientService.GetPatientListByParams(string.Empty, _CurrenRow.HEMODIALYSIS_ID);
                //patientDt[0].PATIENT_ID = this.txtPatientId.Text.Trim().ToUpper();
                //_patientService.SavePatientInfo(patientDt);

            }
            this.DialogResult = DialogResult.OK;
        }

        private void txtPatientId_EditValueChanged(object sender, EventArgs e)
        {
            this.labelControl2.Visible = true;

        }

        #endregion
    }
}
