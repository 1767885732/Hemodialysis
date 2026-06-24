/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者费用记账查询类
// 创建时间：2014-06-25
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
using Hemo.IService.Dict;
using System.Linq;
namespace Hemo.Client.UI.Patient
{

    public partial class PatientBillListFrm : HemoBaseFrm
    {
        #region 类变量

        private IPatient _patientService = ServiceManager.Instance.PatientService;

        private HemodialysisModel.MED_CURE_MAIN_BILLDataTable _patientBill = new HemodialysisModel.MED_CURE_MAIN_BILLDataTable();
        private HemoModel.MED_PATIENT_PREPAYDataTable _patientRastBill = new HemoModel.MED_PATIENT_PREPAYDataTable();

        public string currentHemoID = string.Empty;

        #endregion

        #region 属性

        public string DefaultHemoID { get; set; }

        #endregion

        #region 构造函数

        public PatientBillListFrm()
        {
            InitializeComponent();
            txtCreateDate.DateTime = DateTime.Now.AddDays(-30);
            txtEndDate.DateTime = DateTime.Now.AddDays(1);
        }

        #endregion

        #region 事件

        private void PatientRecord_Load(object sender, EventArgs e)
        {
            InzationData();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage1)
                gridControlDtl.ShowPrintPreview();
            else
                gridControl1.ShowPrintPreview();
        }

        private void btnQuey_Click(object sender, EventArgs e)
        {
            InzationData();
        }

        #endregion

        #region 方法

        private void InzationData()
        {
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                _patientBill = new HemodialysisModel.MED_CURE_MAIN_BILLDataTable();
                _patientRastBill = new HemoModel.MED_PATIENT_PREPAYDataTable();

                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    _patientBill = _patientService.GetPatientBillByHemoID(DefaultHemoID, txtCreateDate.DateTime, txtEndDate.DateTime);
                    _patientRastBill = _patientService.GetRastBillByHemoID(DefaultHemoID);
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {

                    gridControlDtl.DataSource = _patientBill;
                    this.gridControl1.DataSource = _patientRastBill;
                };
                worker.RunWorkerAsync();
            }
        }

        #endregion
    }
}