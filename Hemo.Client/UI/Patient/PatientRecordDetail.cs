/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司
// 描述：患者病历详细窗体
// 创建时间：2016-5-18
// 创建者：吕志强
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
using Hemo.Client.Print;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.UI.Patient
{
    public partial class PatientRecordDetail : HemoBaseFrm
    {
        #region 成员变量

        private IPatient patientService = ServiceManager.Instance.PatientService;

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        private PatientScheduleModel.MED_PATIENTRECORDDataTable dtRecord = null;

        private HemodialysisModel.MED_CURE_DRUGDataTable dtDrug = null;

        private PatientScheduleModel.MED_PATIENTRECORDRow currentRecordRow = null;

        private string currentHemoId = string.Empty;

        #endregion

        #region 属性

        public string CurrentHemoId
        {
            get { return currentHemoId; }
            set { currentHemoId = value; }
        }

        public PatientScheduleModel.MED_PATIENTRECORDRow CurrentRecordRow
        {
            get { return currentRecordRow; }
            set { currentRecordRow = value; }
        }

        #endregion

        #region 构造函数

        public PatientRecordDetail()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientRecordDetail_Load(object sender, EventArgs e)
        {
            if (currentRecordRow != null)
            {
                this.ctlMedicalRecord.CurrentRecordRow = currentRecordRow;
                this.ctlMedicalRecord.LoadMedicalRecord();
                dtRecord = patientService.GetPatientRecordByHemoIDandDate(currentRecordRow.HEMODIALYSIS_ID, currentRecordRow.CREATEDATE);
            }

            //dtDrug = hemoService.GetCureDrugByHemoID(currentHemoId);
            //this.ctlDrugUseRecord.DtDrug = dtDrug;
            //this.ctlDrugUseRecord.LoadDrugRecord();

            this.ctlMedicalRecord.CurrentHemoId = currentHemoId;
            this.tcRecord.SelectedTabPageIndex = 0;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_Click(object sender, EventArgs e)
        {
            dtRecord = this.ctlMedicalRecord.GetPatientRecordDataTable(dtRecord);
            int result = patientService.SavePatientRecord(dtRecord);
            if (result > 0)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                AutoClosedMsgBox.ShowForm("保存病历记录成功！", this.Text, 1000, MessageBoxIcon.Warning);
            }
            else
            {
                AutoClosedMsgBox.ShowForm("保存病历记录失败！", this.Text, 1000, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 选项卡页面改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tcRecord_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            //this.btnAdd.Enabled = (this.tcRecord.SelectedTabPageIndex == 0) ? false : true;
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            FirstHemoRecord frm = new FirstHemoRecord();
            frm.CurrentHemoId = currentHemoId;
            DateTime beginDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            if (currentRecordRow != null)
            {
                beginDate = Utilities.Utility.CDate(currentRecordRow.CREATEDATE.ToShortDateString());
                endDate = Utilities.Utility.CDate(currentRecordRow.CREATEDATE.ToShortDateString());
            }
            frm.StartTime = beginDate;
            frm.EndTime = endDate;
            ReportPrintTool pt = new ReportPrintTool(frm);
            pt.ShowPreviewDialog();
        }

        #endregion

        #region 方法

        #endregion
    }
}