/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：血透风险评估表查询维护窗体
// 创建时间：2015-04-08
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client;
using Hemo.Model;
using Hemo.IService.Dict;
using Hemo.Service;
using Hemo.IService.Config;
using Hemo.IService;
using Hemo.Utilities;
using Hemo.Client.Core;
using Hemo.IService.PatientSchedule;
using Hemo.Client.Print;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class RiskAssessFrm : HemoBaseFrm
    {
        #region 变量
        
        private HemoModel.MED_PATIENTS_ASSESSMENT_SCOREDataTable _patientAssessScore = null;
       
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;

        private PatientModel.MED_PATIENTSDataTable _patientDataTable;
        private IPatient _patientService = ServiceManager.Instance.PatientService;

        private IPatientSchedule patientScheduleService = ServiceManager.Instance.PatientSchedule;

        private DataTable dtStaffSict = new DataTable();
        private bool HasDirty = false;

        private DateTime dt = new DateTime();

        #endregion

        #region 构造函数

        public RiskAssessFrm()
        {
            InitializeComponent();
            this.Text = "血透风险评估";
            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountFrm(this);
            #region 设置开始时间结束时间
            dt = Utility.CDate(patientScheduleService.GetServerDate()).Date; //当前服务器时间  
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初 
            DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);  //本月月末 
            this.beginTime.DateTime = startMonth;
            this.endTime.DateTime = endMonth;
            #endregion
        }

        #endregion

        #region 方法
        
        /// <summary>
        /// 初使化数据
        /// </summary>
        private void InitalizeData()
        {
            SetButtonStates(false, false, false, false);
            this.busyIndicator.ShowLoadingScreenFor(this.gridControl1);
            
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                DataTable dtStaffSict = new DataTable();
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    _patientDataTable = this._patientService.GetPatientList();
                    dtStaffSict = _staffDictService.GetStaffDictList();
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.customGridLookUpEdit1.Properties.DataSource = dtStaffSict;
                    this.repositoryItemCustomGridLookUpEdit1.DataSource = dtStaffSict;
                    this.repositoryItemCustomGridLookUpEdit2.DataSource = _patientDataTable;
                    this.busyIndicator.HideLoadingScreen();
                    SetButtonStates(true, this.bindingSource1.Current != null, false, true);

                    this.iFilterCheckEdit.Enabled = true;
                };
                worker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            SetButtonStates(false, false, false, false);
            this.busyIndicator.ShowLoadingScreenFor(this.gridControl1);

            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    _patientAssessScore = this._patientService.GetPatientAssessScoreByDate(this.beginTime.DateTime, this.endTime.DateTime, this.txtPatientName.Text, this.customGridLookUpEdit1.EditValue != null ? this.customGridLookUpEdit1.EditValue.ToString() : string.Empty);
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.bindingSource1.DataSource = _patientAssessScore;
                    this.busyIndicator.HideLoadingScreen();
                    SetButtonStates(true, this.bindingSource1.Current != null, false, true);

                    this.iFilterCheckEdit.Enabled = true;
                };
                worker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// 设置按纽的状态
        /// </summary>
        /// <param name="add"></param>
        /// <param name="delete"></param>
        /// <param name="cancel"></param>
        /// <param name="save"></param>
        /// <param name="close"></param>
        private void SetButtonStates(bool add, bool delete, bool save, bool refresh)
        {
            this.iNewButton.Enabled = add;
            this.iDeleteButton.Enabled = delete;
            this.iSaveButton.Enabled = save;
            this.iRefreshButton.Enabled = refresh;
        }

        #endregion

        #region 事件
        
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iNewButton_Click(object sender, EventArgs e)
        {
            this.bindingSource1.AddNew();
            this.HasDirty = true;
            this.iSaveButton.Enabled = true;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iDeleteButton_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                if (XtraMessageBox.Show("你确定要删除选中的项吗？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.bindingSource1.EndEdit();
                    var current = (this.bindingSource1.Current as DataRowView).Row as HemoModel.MED_PATIENTS_ASSESSMENT_SCORERow;
                    this._patientService.DeletePatientAssessScoreById(current.ID);

                    _patientAssessScore.RemoveMED_PATIENTS_ASSESSMENT_SCORERow(current);
                    this.HasDirty = false;
                    var rows = _patientAssessScore.Where(i => i.RowState != DataRowState.Deleted);
                    foreach (var row in rows)
                    {
                        if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                            this.HasDirty = true;
                    }
                    this.iSaveButton.Enabled = this.HasDirty;
                }

            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iSaveButton_Click(object sender, EventArgs e)
        {
            if (_patientAssessScore == null)
                return;

            this.gridView1.ClearColumnErrors();
            this.bindingSource1.EndEdit();
            this.bindingSource1.CurrencyManager.EndCurrentEdit();

            #region MyRegion

            //数据验证
            for (int i = 0; i < this.gridView1.DataRowCount; i++)
            {
                //名称不能为空
                string ASSENEMENT = this.gridView1.GetRowCellDisplayText(i, "ASSENEMENT");
                if (string.IsNullOrEmpty(ASSENEMENT) || string.IsNullOrEmpty(ASSENEMENT.Trim()))
                {
                    this.gridView1.FocusedRowHandle = i;
                    this.gridView1.SelectCell(i, this.gridView1.Columns["ASSENEMENT"]);
                    this.gridView1.SetColumnError(this.gridView1.Columns["ASSENEMENT"], "请输入评分日期");
                    return;
                }

                string HEMODIALYSIS_ID = this.gridView1.GetRowCellDisplayText(i, "HEMODIALYSIS_ID");
                if (string.IsNullOrEmpty(HEMODIALYSIS_ID) || string.IsNullOrEmpty(HEMODIALYSIS_ID.Trim()))
                {
                    this.gridView1.FocusedRowHandle = i;
                    this.gridView1.SelectCell(i, this.gridView1.Columns["HEMODIALYSIS_ID"]);
                    this.gridView1.SetColumnError(this.gridView1.Columns["HEMODIALYSIS_ID"], "请输入对应患者");
                    return;
                }

                string CANAL = this.gridView1.GetRowCellDisplayText(i, "CANAL");
                if (string.IsNullOrEmpty(CANAL) || string.IsNullOrEmpty(CANAL.Trim()))
                {
                    this.gridView1.FocusedRowHandle = i;
                    this.gridView1.SelectCell(i, this.gridView1.Columns["CANAL"]);
                    this.gridView1.SetColumnError(this.gridView1.Columns["CANAL"], "请输入患者导管评分");
                    return;
                }

                string PRESSURE = this.gridView1.GetRowCellDisplayText(i, "PRESSURE");
                if (string.IsNullOrEmpty(PRESSURE) || string.IsNullOrEmpty(PRESSURE.Trim()))
                {
                    this.gridView1.FocusedRowHandle = i;
                    this.gridView1.SelectCell(i, this.gridView1.Columns["PRESSURE"]);
                    this.gridView1.SetColumnError(this.gridView1.Columns["PRESSURE"], "请输入患者压疮评分");
                    return;
                }

                string FALL = this.gridView1.GetRowCellDisplayText(i, "FALL");
                if (string.IsNullOrEmpty(FALL) || string.IsNullOrEmpty(FALL.Trim()))
                {
                    this.gridView1.FocusedRowHandle = i;
                    this.gridView1.SelectCell(i, this.gridView1.Columns["FALL"]);
                    this.gridView1.SetColumnError(this.gridView1.Columns["FALL"], "请输入患者跌/坠评分");
                    return;
                }
                //string NURSERECORD = this.gridView1.GetRowCellDisplayText(i, "NURSERECORD");
                //if (string.IsNullOrEmpty(NURSERECORD) || string.IsNullOrEmpty(NURSERECORD.Trim()))
                //{
                //    this.gridView1.FocusedRowHandle = i;
                //    this.gridView1.SelectCell(i, this.gridView1.Columns["NURSERECORD"]);
                //    this.gridView1.SetColumnError(this.gridView1.Columns["NURSERECORD"], "护理记录");
                //    return;
                //}                

            }
            //保存
            if (this._patientService.SavePatientAssessScore(_patientAssessScore) > 0)
            {

                AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);
                //XtraMessageBox.Show("保存成功!");
            }

            #endregion

            Query();

            this.HasDirty = false;

            this.iSaveButton.Enabled = false;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iCloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 初使化新行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var row = this.gridView1.GetDataRow(e.RowHandle);
            if (row == null)
                return;
            row["ID"] = Guid.NewGuid().ToString();
            row["ASSENEMENT"] = dt.Date;       
            row["CREATEBY"] = HemoApplicationContext.Current.CurrentUser.USER_ID;
            row["CREATEDATE"] = dt;
            this.gridView1.IsCellSelected(this.gridView1.FocusedRowHandle, this.gridPATIENT);

        }
        /// <summary>
        /// 选择行时间的按钮状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.iDeleteButton.Enabled = this.bindingSource1.Current != null;

        }
        /// <summary>
        /// 显示过虑行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iFilterCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            this.gridView1.OptionsView.ShowAutoFilterRow = this.iFilterCheckEdit.Checked;
        }
        /// <summary>
        /// 值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            this.iSaveButton.Enabled = true;
            this.HasDirty = true;
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iRefreshButton_Click(object sender, EventArgs e)
        {
            if (this.HasDirty)
            {
                if (XtraMessageBox.Show("数据未保存,是否刷新?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }
            Query();
        }
        /// <summary>
        /// 开始的时候去初始化数据和界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RiskAssessFrm_Load(object sender, EventArgs e)
        {
            InitalizeData();
            Query();
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Print_Click(object sender, EventArgs e)
        {
            string title = string.Format("{0}~{1}", this.beginTime.DateTime.ToString("yyyy-MM-dd"),this.endTime.DateTime.ToString("yyyy-MM-dd"));
            RiskAssessReport frm = new RiskAssessReport(_patientAssessScore, title);
            ReportPrintTool pt = new ReportPrintTool(frm);
            pt.ShowPreviewDialog();


        }

        private void labelControl76_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
