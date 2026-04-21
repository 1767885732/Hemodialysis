/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：患者事件编辑窗体
// 创建时间：2016-03-17
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
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
using Hemo.IService;
using Hemo.Service;
using Hemo.Client.UI.Machine;
using DevExpress.XtraReports.UI;
using Hemo.Utilities;

namespace Hemo.Client.UI.Patient
{
    public partial class RegOfDealthFrm : ViewBase
    {
        #region 类变量

        private PatientModel.MED_PATIENTREGDEALTHDataTable _patientRegDealth = new PatientModel.MED_PATIENTREGDEALTHDataTable();

        private IPatient patientService = ServiceManager.Instance.PatientService;

        #endregion

        #region 属性

        public string currentHemoId { get; set; }

        #endregion

        #region 构造函数

        public RegOfDealthFrm()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!Valid())
                return;
            if (_patientRegDealth != null && _patientRegDealth.Rows.Count > 0)
            {
                _patientRegDealth[0].CREATEDATE = this.date_create.DateTime; ;
                _patientRegDealth[0].CORRECTIVEACTIONS = this.txtCORRECTIVEACTIONS.Text;
                _patientRegDealth[0].EVENTANALYSIS = this.txtEVENTANALYSIS.Text;
                _patientRegDealth[0].EXTEND1 = "0";
            }
            else
            {
                _patientRegDealth = new PatientModel.MED_PATIENTREGDEALTHDataTable();
                var row = _patientRegDealth.NewMED_PATIENTREGDEALTHRow();
                row.ID = Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = currentHemoId;
                row.EVENTANALYSIS = this.txtEVENTANALYSIS.Text;
                row.CORRECTIVEACTIONS = this.txtCORRECTIVEACTIONS.Text;
                row.TYPE = "0";
                row.EXTEND1 = "0";
                row.EXTEND2 = string.Empty;
                row.CREATEBY = Hemo.Client.Core.HemoApplicationContext.Current.CurrentUser.USER_ID;
                row.CREATEDATE = DateTime.Now;
                _patientRegDealth.AddMED_PATIENTREGDEALTHRow(row);

            }
            patientService.SavePatientRegDealth(_patientRegDealth);
            AutoClosedMsgBox.ShowForm("保存成功！", this.Text, 2000, MessageBoxIcon.Asterisk);
            //        this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            //      this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void RegOfDealthFrm_Load(object sender, EventArgs e)
        {
            UserInfo.HEMODIALYSIS_ID = currentHemoId;
            UserInfo.LoadPatientInfo();
            InzationData();
        }
        
        /// <summary>
        /// 调用模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTemplate_Click(object sender, EventArgs e)
        {
            TemplateOfDealthFrm frm = new TemplateOfDealthFrm();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (frm.SelectedDate != null)
                {
                    this.txtEVENTANALYSIS.Text = frm.SelectedDate.EVENTANALYSIS;
                    this.txtCORRECTIVEACTIONS.Text = frm.SelectedDate.CORRECTIVEACTIONS;
                }
            }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            Hemo.Client.Print.RegDealthReport report = new Hemo.Client.Print.RegDealthReport(_patientRegDealth);
            ReportPrintTool pt = new ReportPrintTool(report);
            pt.ShowPreviewDialog();
        }

        #endregion

        #region 方法

        private bool Valid()
        {
            if (string.IsNullOrEmpty(this.txtEVENTANALYSIS.Text.Trim()))
            {
                MessageBox.Show("事件及原因不能为空,请确认填写！！");
                this.txtEVENTANALYSIS.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.txtCORRECTIVEACTIONS.Text.Trim()))
            {
                MessageBox.Show("改进原因不能为空,请确认填写！");
                this.txtCORRECTIVEACTIONS.Focus();
                return false;
            }

            return true;

        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InzationData()
        {
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                _patientRegDealth = new PatientModel.MED_PATIENTREGDEALTHDataTable();

                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    _patientRegDealth = patientService.GetPatientRegDealthByHemoId(currentHemoId);

                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    if (_patientRegDealth != null && _patientRegDealth.Rows.Count > 0)
                    {
                        this.date_create.DateTime = _patientRegDealth[0].CREATEDATE;
                        this.txtEVENTANALYSIS.Text = _patientRegDealth[0].EVENTANALYSIS;
                        this.txtCORRECTIVEACTIONS.Text = _patientRegDealth[0].CORRECTIVEACTIONS;
                    }
                    else
                    {
                        this.date_create.DateTime = DateTime.Now;
                        this.txtEVENTANALYSIS.Text = string.Empty;
                        this.txtCORRECTIVEACTIONS.Text = string.Empty;
                    }
                };
                worker.RunWorkerAsync();
            }
        }

        #endregion
    }
}