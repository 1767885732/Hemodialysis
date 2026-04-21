/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者选择控件类
// 创建时间：2017-03-12
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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Client.UI.Machine;
using Hemo.Model;
using Hemo.Service;
using Hemo.Client.Controls.ScheduleNew;
using Hemo.Utilities;
using Hemo.Client.Core;
using System.Configuration;
using Hemo.IService.Config;

namespace Hemo.Client.UI.Patient
{
    [ToolboxItem(true)]
    public partial class PatientInfoCheck : DevExpress.XtraEditors.XtraUserControl
    {
        #region 类变量

        private string strWardCode = string.Empty;

        public event EventHandler patientPickEvent;

        private PatientModel.MED_PATIENTSRow _patient = null;

        private PatientService objPatient = new PatientService();

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        public PatientModel.MED_PATIENTSDataTable _patientDataTable = new PatientModel.MED_PATIENTSDataTable();

        public bool HasDirty { get; set; }



        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public PatientInfoCheck()
        {
            InitializeComponent();
            this.btnPick.Image = global::Hemo.Client.Properties.Resources.add;
            if (!DesignMode)
            {
            }

        }

        #endregion

        #region 事件

        private void btnPick_Click(object sender, EventArgs e)
        {
            var frm = new PatientScheduleInputCtl();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                _patientDataTable = new PatientModel.MED_PATIENTSDataTable();
                this._patient = frm._PatientRow;
                this.txtHemoId.Text = this._patient.HEMODIALYSIS_ID;
                this._patientDataTable.Rows.Add(this._patient.ItemArray);
                if (patientPickEvent != null)
                    patientPickEvent(this, e);
            }
        }

        private void txtHemoId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _patientDataTable = objPatient.GetPatientListByParams(string.Empty, this.txtHemoId.Text);
                if (_patientDataTable != null && _patientDataTable.Rows.Count > 0)
                {
                    this.txtHemoId.Text = _patientDataTable[0].HEMODIALYSIS_ID;
                    if (patientPickEvent != null)
                        patientPickEvent(this, e);
                }
                else
                {
                    //患者不在血透数据库患者表，执行患者信息同步操作
                    SyncPatientByPatientId(this.txtHemoId.Text);
                }
            }
        }

        #endregion

        #region 方法

        public void SetTxtHemoIdText(string hemoid)
        {
            this.txtHemoId.Text = hemoid;
            if (string.IsNullOrEmpty(hemoid))
            {
                this._patient = null;
                this._patientDataTable = new PatientModel.MED_PATIENTSDataTable();
            }
        }

        public void SetFocuse()
        {
            this.txtHemoId.Focus();
        }

        /// <summary>
        /// 根据患者ID同步患者
        /// </summary>
        /// <param name="patientId"></param>
        private void SyncPatientByPatientId(string patientId)
        {
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender, DoWorkEventArgs e)
                {
                    string msg = string.Empty;
                    msg = InterfaceUtility.SynchronizePatientsByPatientId(patientId, HemoApplicationContext.Current.InterFaceDate.FirstOrDefault(i => i.ITEM_NAME == "血透同步数据接口").ITEM_VALUE.ToString());

                    if (msg.Length == 0)
                    {
                        UpdatePatient(patientId, false);
                    }
                    else
                    {
                        MessageBox.Show(msg, "同步患者失败！");
                    }
                };
                worker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
                {
                    _patientDataTable = objPatient.GetPatientListByParams(string.Empty, patientId);
                    if (_patientDataTable != null && _patientDataTable.Rows.Count > 0)
                    {
                        this.txtHemoId.Text = _patientDataTable[0].HEMODIALYSIS_ID;
                        if (patientPickEvent != null)
                            patientPickEvent(this, e);
                    }
                };
                worker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// 更新患者
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private void UpdatePatient(string patientId, bool flag)
        {
            bool isNew = false;
            HemodialysisModel.MED_PAT_MASTER_INDEXDataTable pMaster;
            if (string.IsNullOrEmpty(strWardCode))
                strWardCode = ConfigurationManager.AppSettings["WardCode"].ToString();


            if (patientId.Length > 0 && flag == false)
            {
                pMaster = hemoService.GetPatientMasterIndexByPatientID(patientId, strWardCode);
            }
            else
            {
                pMaster = hemoService.GetPatientMasterIndexList(strWardCode);
            }

            PatientModel.MED_PATIENTSDataTable dtPatient = objPatient.GetPatientList();
            PatientModel.MED_PATIENTSRow drPatient = null;

            if (pMaster != null && pMaster.Rows.Count > 0)
            {
                foreach (HemodialysisModel.MED_PAT_MASTER_INDEXRow patIndexRow in pMaster.Rows)
                {
                    DataRow[] rows = dtPatient.Select("PATIENT_ID = '" + patIndexRow.PATIENT_ID + "'");
                    if (rows.Length > 0)
                        drPatient = rows[0] as PatientModel.MED_PATIENTSRow;

                    if (drPatient == null)
                    {
                        drPatient = dtPatient.NewMED_PATIENTSRow();
                        drPatient.PATIENT_ID = patIndexRow.PATIENT_ID;
                        drPatient.HEMODIALYSIS_ID = objPatient.GetNewHemoID();
                        dtPatient.AddMED_PATIENTSRow(drPatient);
                        isNew = true;
                    }
                    else
                    {
                        isNew = false;
                    }

                    if (!patIndexRow.IsNAMENull())
                    {
                        drPatient.NAME = patIndexRow.NAME;
                    }
                    if (!patIndexRow.IsNAME_PHONETICNull())
                    {
                        drPatient.INPUT_CODE = PinYinConverter.GetPYString(patIndexRow.NAME);
                    }
                    if (!patIndexRow.IsSEXNull())
                    {
                        drPatient.SEX = patIndexRow.SEX;
                    }
                    if (!patIndexRow.IsDATE_OF_BIRTHNull())
                    {
                        drPatient.BIRTHDAY = patIndexRow.DATE_OF_BIRTH;
                        drPatient.AGE = Hemo.Utilities.Utility.GetAge(drPatient.BIRTHDAY.ToShortDateString());
                    }
                    if (!patIndexRow.IsID_NONull())
                    {
                        drPatient.CREDENTIALS_TYPE = "居民身份证";
                        drPatient.CREDENTIALS_NUMBER = patIndexRow.ID_NO;
                    }
                    if (!patIndexRow.IsNATIONNull())
                    {
                        drPatient.NATION = patIndexRow.NATION;
                    }
                    if (!patIndexRow.IsPHONE_NUMBER_HOMENull())
                    {
                        drPatient.WORK_TELEPHONE = patIndexRow.PHONE_NUMBER_HOME;
                    }
                    if (!patIndexRow.IsMAILING_ADDRESSNull())
                    {
                        drPatient.ADDRESS = patIndexRow.MAILING_ADDRESS;
                    }
                    if (!patIndexRow.IsCHARGE_TYPENull())
                    {
                        drPatient.MEDICAL_TYPE = patIndexRow.CHARGE_TYPE;
                    }
                    if (!patIndexRow.IsNEXT_OF_KIN_PHONENull())
                    {
                        drPatient.TELEPHONE = patIndexRow.NEXT_OF_KIN_PHONE;
                    }
                    if (!patIndexRow.IsVISIT_IDNull())
                    {
                        drPatient.VISIT_ID = patIndexRow.VISIT_ID;
                    }
                    if (!patIndexRow.IsBED_NONull())
                    {
                        drPatient.BED_NO = patIndexRow.BED_NO.ToString();
                    }
                    if (!patIndexRow.IsADMISSION_DATE_TIMENull())
                    {
                        drPatient.SPECIFIC_TIME = DateTime.Now;
                    }
                    if (!patIndexRow.IsWARD_NAMENull())
                    {
                        drPatient.WARD_CODE = patIndexRow.WARD_NAME;
                        drPatient.TIME_TYPE = "住院";
                    }
                    else
                    {
                        drPatient.TIME_TYPE = "门诊";
                    }

                    if (!patIndexRow.IsDEPT_NAMENull())
                    {
                        drPatient.WHAT_DEPARTMENT_IN = patIndexRow.DEPT_NAME;
                    }
                    if (!patIndexRow.IsDIAGNOSISNull())
                    {
                        drPatient.DIAGNOSE = patIndexRow.DIAGNOSIS;
                    }
                    if (!patIndexRow.IsINP_NONull())
                    {
                        drPatient.ADMISSION_NUMBER = patIndexRow.INP_NO;
                    }
                    if (!patIndexRow.IsCREATE_DATENull())
                    {
                        drPatient.CREATE_DATE = patIndexRow.CREATE_DATE;
                    }
                    drPatient.IS_NEW = "0";
                    objPatient.SavePatientInfo(dtPatient);
                }
            }
        }

        #endregion
    }
}
