/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者记账查询类
// 创建时间：2014-06-15
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

    public partial class PatientBillHistoryRecordFrm : HemoBaseFrm
    {
        #region 类变量

        private IPatient _patientService = ServiceManager.Instance.PatientService;
        private System.Data.DataTable _patientBill;

        public PatientModel.MED_PATIENTSDataTable _patientDataTable;
        private HemoModel.MED_PATIENT_PREPAYDataTable _patientRastBill = new HemoModel.MED_PATIENT_PREPAYDataTable();
        public string currentHemoID = string.Empty;

        #endregion

        #region 属性

        public string DefaultHemoID { get; set; }

        #endregion

        #region 构造函数

        public PatientBillHistoryRecordFrm()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void PatientRecord_Load(object sender, EventArgs e)
        {
            if (DefaultHemoID == null)
            {
                DefaultHemoID = "";
            }
            txtCreateDate.DateTime = DateTime.Now.AddDays(-30);
            txtEndDate.DateTime = DateTime.Now.AddDays(1);
            var start = txtCreateDate.DateTime;
            var end = txtEndDate.DateTime;
            using (BackgroundWorker worker = new BackgroundWorker())
            {

                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    _patientBill = _patientService.GetUserBillListByStartEndDate(start, end, DefaultHemoID);

                    this._patientDataTable = this._patientService.GetPatientList();


                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {

                    gcRecord.DataSource = _patientBill;
                    if (_patientDataTable != null && _patientDataTable.Count > 0)
                    {
                        var orowN = _patientDataTable.NewMED_PATIENTSRow();

                        orowN.ItemArray = _patientDataTable[0].ItemArray;
                        orowN.NAME = "全部";
                        orowN.HEMODIALYSIS_ID = " ";
                        _patientDataTable.Rows.Add(orowN);
                        _patientDataTable.DefaultView.Sort = "HEMODIALYSIS_ID ASC";

                    }

                    this.gridLookPatient.Properties.DataSource = this._patientDataTable;
                    gridLookPatient.EditValue = DefaultHemoID;

                };
                worker.RunWorkerAsync();
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            if (gridLookPatient.EditValue != null)
            {
                DefaultHemoID = gridLookPatient.EditValue.ToString();
            }
            var start = txtCreateDate.DateTime;
            var end = txtEndDate.DateTime;
            _patientBill = _patientService.GetUserBillListByStartEndDate(start, end, DefaultHemoID);

            gcRecord.DataSource = _patientBill;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            gcRecord.ShowPrintPreview();
        }

        private void dxSimpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvRecord_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var rowCurrent = this.gvRecord.GetFocusedDataRow() as DataRow;

            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 2)
                {
                    this.xtraTabControl1.SelectedTabPage = this.xtraTabPage2;
                    var hemoId = rowCurrent["HEMODIALYSIS_ID"].ToString();
                    _patientRastBill = _patientService.GetRastBillByHemoID(hemoId);
                    this.gridControl1.DataSource = _patientRastBill;

                }
            }
        }

        #endregion
    }
}