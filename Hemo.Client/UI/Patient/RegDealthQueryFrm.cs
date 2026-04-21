/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：患者事件查询窗体
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
using System.Data;
using Hemo.IService;
using Hemo.Service;
using Hemo.Model;
using DevExpress.XtraReports.UI;
using Hemo.Client.UI.Machine;

namespace Hemo.Client.UI.Patient {
    public partial class RegDealthQueryFrm : ViewBase
    {
        #region 类变量

        private IPatient patientService = ServiceManager.Instance.PatientService;
        private PatientModel.MED_PATIENTREGDEALTHDataTable _data = new PatientModel.MED_PATIENTREGDEALTHDataTable();

        #endregion

        #region 属性

        public string currentHemoId { get; set; }

        #endregion

        #region 构造函数

        public RegDealthQueryFrm()
        {
            InitializeComponent();

            this.beginDataEdit.DateTime = DateTime.Now.AddDays(-365);
            this.endDataEdit.DateTime = DateTime.Now;
        }

        #endregion

        #region 事件

        private void RegDealthQueryFrm_Load(object sender, EventArgs e)
        {
            InzationData();
        }

        private void btn_Query_Click(object sender, EventArgs e)
        {
            InzationData();
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            Hemo.Client.Print.RegDealthReport report = new Hemo.Client.Print.RegDealthReport(_data);
            ReportPrintTool pt = new ReportPrintTool(report);
            pt.ShowPreviewDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //RegOfDealthFrm frm = new RegOfDealthFrm();
            //frm.currentHemoId = currentHemoId;
            //frm.ShowDialog();
        }

        #endregion

        #region 方法

        public void InzationData()
        {
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                var _dataTemp = new PatientModel.MED_PATIENTREGDEALTHDataTable();
                _data = new PatientModel.MED_PATIENTREGDEALTHDataTable();
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    _dataTemp = patientService.GetPatientRegDealthByData(this.beginDataEdit.DateTime.Date, this.endDataEdit.DateTime.Date);
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    if (!string.IsNullOrEmpty(this.txtHemoID.Text.Trim()))
                    {
                        _dataTemp.Where(i => i.HEMODIALYSIS_ID == this.txtHemoID.Text).CopyToDataTable<PatientModel.MED_PATIENTREGDEALTHRow>(_data, LoadOption.PreserveChanges);

                    }
                    else
                    {
                        _dataTemp.CopyToDataTable<PatientModel.MED_PATIENTREGDEALTHRow>(_data, LoadOption.PreserveChanges);
                    }
                    this.gridControl1.DataSource = _data;
                };
                worker.RunWorkerAsync();
            }
        }

        #endregion
    }
}