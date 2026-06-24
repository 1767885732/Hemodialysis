/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：随访管理窗体
// 创建时间：2014-09-11
// 创建者：贺建操
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
using Hemo.Service;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.Client.Print;
using Hemo.IService;
using Hemo.IService.PatientSchedule;

namespace Hemo.Client.UI.FollowUp {
    public partial class FollowUpQuery : HemoBaseFrm
    {
        #region 类变量

        private IMaterial objMaterial = ServiceManager.Instance.MaterialService;

        private DrugModel.MED_PATIENT_FOLLOWUPDataTable _data = null;
        private IPatientSchedule patientScheduleService = ServiceManager.Instance.PatientSchedule;

        #endregion

        #region 属性

        public PatientModel.MED_PATIENTSRow PatientRow { get; set; }

        #endregion

        #region 构造函数

        public FollowUpQuery()
        {
            InitializeComponent();
            DateTime date = Utility.CDate(patientScheduleService.GetServerDate());
            DateTime startWeek = Utility.GetMonday(date).Date;
            DateTime endWeek = startWeek.AddDays(6).Date;
            this.beginTime.EditValue = startWeek;
            this.endTime.EditValue = endWeek;
        }

        #endregion

        #region 事件

        private void FollowUpQuery_Load(object sender, EventArgs e)
        {
            InzationMaterialDate();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var row = this.gridView1.GetFocusedDataRow() as DrugModel.MED_PATIENT_FOLLOWUPRow;
            if (row != null)
            {
                if (e.Button == MouseButtons.Left && e.Clicks == 2)
                {
                    FollowUpMaster frm = new FollowUpMaster();
                    frm.SetFollowUp(row);
                    frm.followUpControl1.isFromDatabase = frm.isHavingInDatabase = true;
                    frm.btn_Save.Visible = true;
                    frm.ShowDialog();
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            InzationMaterialDate();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var row = this.gridView1.GetFocusedDataRow() as DrugModel.MED_PATIENT_FOLLOWUPRow;
            if (row != null)
            {
                FollowUpMaster frm = new FollowUpMaster();
                frm.SetFollowUp(row);
                frm.followUpControl1.isFromDatabase = frm.isHavingInDatabase = true;
                frm.btn_Save.Visible = true;
                frm.ShowDialog();
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            FollowUp.FollowUpMaster frm = new FollowUp.FollowUpMaster();
            frm.PatientRow = PatientRow;
            frm.followUpControl1.isFromDatabase = false;
            frm.ShowDialog();
            InzationMaterialDate();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 方法

        public void InzationMaterialDate()
        {
            this.Enabled = false;
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                _data = new DrugModel.MED_PATIENT_FOLLOWUPDataTable();
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    _data = objMaterial.GetFollowUpByData(Convert.ToDateTime(this.beginTime.EditValue), Convert.ToDateTime(this.endTime.EditValue), PatientRow.HEMODIALYSIS_ID);
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.gridDrugMaster.DataSource = _data;
                    this.Enabled = true;
                };
                worker.RunWorkerAsync();
            }

        }

        #endregion
    }
}