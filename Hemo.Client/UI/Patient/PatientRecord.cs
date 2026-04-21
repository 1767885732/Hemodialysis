/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司
// 描述：患者病历窗体
// 创建时间：2015-6-8
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
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.IService;
using DevExpress.XtraEditors.Controls;
using Hemo.IService.Config;
using Hemo.Client.UI.Hemodialysis;
using System.Configuration;
using Hemo.Client.Core;

namespace Hemo.Client.UI.Patient 
{
    public partial class PatientRecord :HemoBaseFrm
    {
        #region 类变量

        public string currentHemoID = string.Empty;
        private PatientScheduleModel.MED_PATIENTRECORDDataTable _patientrecord = new PatientScheduleModel.MED_PATIENTRECORDDataTable();
        private IPatient objPatient = ServiceManager.Instance.PatientService;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public PatientRecord()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void PatientRecord_Load(object sender, EventArgs e)
        {
            this.recoredDateEdit.EditValue = System.DateTime.Now.Date;
            this.ctlUserLongInfo1.HEMODIALYSIS_ID = currentHemoID;
            this.ctlUserLongInfo1.LoadPatientInfo();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            GetValueByUserControls();

            objPatient.SavePatientRecord(_patientrecord);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void txtBIRTHDAY_EditValueChanged(object sender, EventArgs e)
        {
            //根据患者信息和日期去加载病人的病历信息。。
            _patientrecord = objPatient.GetPatientRecordByHemoIDandDate(currentHemoID, Convert.ToDateTime(this.recoredDateEdit.EditValue));
            if (_patientrecord.Rows.Count > 0)
            {
                this.memoEdit_Top.Text = string.Format("          {0}", _patientrecord[0].ACTION.ToString());
                this.memoEdit_Fill.Text = string.Format("             {0}", _patientrecord[0].PRESENTILLNESS.ToString());
                this.memoEdit_bottom.Text = string.Format("             {0}", _patientrecord[0].PASTILLNESS.ToString());
            }
            else
            {
                this.memoEdit_Top.Text = "          ";
                this.memoEdit_Fill.Text = "             ";
                this.memoEdit_bottom.Text = "             ";
            }
        }

        private void btn_Mould_Click(object sender, EventArgs e)
        {
            MouldNameFrm frm = new MouldNameFrm();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PatientScheduleModel.MED_RECORDMOULDDataTable recordMould = new PatientScheduleModel.MED_RECORDMOULDDataTable();
                var row = recordMould.NewMED_RECORDMOULDRow();
                row.ID = Guid.NewGuid().ToString();
                row.NAME = frm.MouldName;
                row.ACTION = this.memoEdit_Top.Text.Trim();
                row.PRESENTILLNESS = this.memoEdit_Fill.Text.Trim();
                row.PASTILLNESS = this.memoEdit_bottom.Text.Trim();
                row.CREATEBY = HemoApplicationContext.Current.CurrentUser.USER_ID;
                row.CREATEDATE = Convert.ToDateTime(recoredDateEdit.EditValue);
                row.LASTUPDATEBY = HemoApplicationContext.Current.CurrentUser.USER_ID;
                row.LASTUPDATEDATE = System.DateTime.Now;
                recordMould.AddMED_RECORDMOULDRow(row);
                objPatient.SavePatientRecordMoule(recordMould);
                AutoClosedMsgBox.ShowForm("保存病历模版成功!", this.Text, 1000, MessageBoxIcon.Question);
            }
        }

        private void btn_call_Click(object sender, EventArgs e)
        {
            RecordMouldFrm frm = new RecordMouldFrm();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.memoEdit_Top.Text = "          " + frm.Action;
                this.memoEdit_Fill.Text = "             " + frm.Presentliness;
                this.memoEdit_bottom.Text = "             " + frm.Pastliness;
            }
        }

        #endregion

        #region 方法

        private void GetValueByUserControls()
        {
            if (_patientrecord.Rows.Count <= 0)
            {
                var row = _patientrecord.NewMED_PATIENTRECORDRow();
                row.ID = Guid.NewGuid().ToString().Trim();
                row.HEMODIALYSIS_ID = this.ctlUserLongInfo1.HEMODIALYSIS_ID;
                row.ACTION = this.memoEdit_Top.Text.Trim();
                row.PRESENTILLNESS = this.memoEdit_Fill.Text.Trim();
                row.PASTILLNESS = this.memoEdit_bottom.Text.Trim();
                row.CREATEBY = HemoApplicationContext.Current.CurrentUser.USER_ID;
                row.CREATEDATE = Convert.ToDateTime(recoredDateEdit.EditValue);
                row.LASTUPDATEBY = HemoApplicationContext.Current.CurrentUser.USER_ID;
                row.LASTUPDATEDATE = System.DateTime.Now;
                _patientrecord.AddMED_PATIENTRECORDRow(row);
            }
            else
            {
                _patientrecord[0].ACTION = this.memoEdit_Top.Text.Trim();
                _patientrecord[0].PRESENTILLNESS = this.memoEdit_Fill.Text.Trim();
                _patientrecord[0].PASTILLNESS = this.memoEdit_bottom.Text.Trim();
                _patientrecord[0].CREATEBY = HemoApplicationContext.Current.CurrentUser.USER_ID;
                _patientrecord[0].CREATEDATE = Convert.ToDateTime(recoredDateEdit.EditValue);
                _patientrecord[0].LASTUPDATEBY = HemoApplicationContext.Current.CurrentUser.USER_ID;
                _patientrecord[0].LASTUPDATEDATE = System.DateTime.Now;
            }
        }

        #endregion
    }
}