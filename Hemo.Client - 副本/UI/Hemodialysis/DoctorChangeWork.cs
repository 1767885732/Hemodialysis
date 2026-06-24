/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:医生改变
 * 创建标识:贺建操-2013年7月3日
 * 
 * 修改时间:2013年10月11日
 * 修改人:顾伟伟
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年1月19日
 * 修改人:吕志强
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年4月29日
 * 修改人:顾伟伟
 * 修改描述:修改方法
 * ----------------------------------------------------------------*/
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
    public partial class DoctorChangeWork : HemoBaseFrm
    {

        #region 变量
        private HemoModel.MED_HEMO_CHAGEWORKDataTable _docChangeWork = null;

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
        public DoctorChangeWork()
        {
            InitializeComponent();
            this.Text = "医生交班记录";
            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountFrm(this);
            #region 设置开始时间结束时间
            dt = Utility.CDate(patientScheduleService.GetServerDate()).Date; //当前时间  
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初 
            DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);  //本月月末 
            this.beginTime.DateTime = startMonth;
            this.endTime.DateTime = endMonth;


            #endregion
        }
        #endregion

        #region 事件

        /// <summary>
        /// 初使化数据
        /// </summary>
        private void InitalizeData()
        {
            SetButtonStates(false, false, false, false);
            this.busyIndicator.ShowLoadingScreenFor(this.gridControl1);
            
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    this._patientDataTable = this._patientService.GetPatientList();
                    _docChangeWork = this._patientService.GetChangeWorkByDate(this.beginTime.DateTime, this.endTime.DateTime);
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.repositoryItemCustomGridLookUpEdit2.DataSource = _patientDataTable;
                    this.bindingSource1.DataSource = _docChangeWork;
                    this.busyIndicator.HideLoadingScreen();
                    SetButtonStates(true, this.bindingSource1.Current != null, false, true);

                    this.iFilterCheckEdit.Enabled = true;
                };
                worker.RunWorkerAsync();
            }
        }

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
                    var current = (this.bindingSource1.Current as DataRowView).Row as HemoModel.MED_HEMO_CHAGEWORKRow;
                    this._patientService.DeleteChangeWorkById(current.ID);

                    _docChangeWork.RemoveMED_HEMO_CHAGEWORKRow(current);
                    this.HasDirty = false;
                    var rows = _docChangeWork.Where(i => i.RowState != DataRowState.Deleted);
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
            if (_docChangeWork == null)
                return;

            this.gridView1.ClearColumnErrors();
            this.bindingSource1.EndEdit();
            this.bindingSource1.CurrencyManager.EndCurrentEdit();

            #region MyRegion

            //数据验证
            for (int i = 0; i < this.gridView1.DataRowCount; i++)
            {
                //名称不能为空
                string CHANGETIME = this.gridView1.GetRowCellDisplayText(i, "CHANGETIME");
                if (string.IsNullOrEmpty(CHANGETIME) || string.IsNullOrEmpty(CHANGETIME.Trim()))
                {
                    this.gridView1.FocusedRowHandle = i;
                    this.gridView1.SelectCell(i, this.gridView1.Columns["CHANGETIME"]);
                    this.gridView1.SetColumnError(this.gridView1.Columns["CHANGETIME"], "请输入交班日期");
                    return;
                }

                string PATIENT = this.gridView1.GetRowCellDisplayText(i, "PATIENT");
                if (string.IsNullOrEmpty(PATIENT) || string.IsNullOrEmpty(PATIENT.Trim()))
                {
                    this.gridView1.FocusedRowHandle = i;
                    this.gridView1.SelectCell(i, this.gridView1.Columns["PATIENT"]);
                    this.gridView1.SetColumnError(this.gridView1.Columns["PATIENT"], "请输入对应患者");
                    return;
                }

                string CONTENT = this.gridView1.GetRowCellDisplayText(i, "CONTENT");
                if (string.IsNullOrEmpty(CONTENT) || string.IsNullOrEmpty(CONTENT.Trim()))
                {
                    this.gridView1.FocusedRowHandle = i;
                    this.gridView1.SelectCell(i, this.gridView1.Columns["CONTENT"]);
                    this.gridView1.SetColumnError(this.gridView1.Columns["CONTENT"], "交班内容");
                    return;
                }                

            }
            //保存
            if (this._patientService.SaveChageWork(_docChangeWork) > 0)
            {

                AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);
                //XtraMessageBox.Show("保存成功!");
            }

            #endregion


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
            row["CHANGETIME"] = dt.Date;
            row["TYPE"] = "1";
            row["CHANGEUSER"] = HemoApplicationContext.Current.CurrentUser.USER_ID;          
            row["CREATEBY"] = HemoApplicationContext.Current.CurrentUser.USER_ID;
            row["CREATEDATE"] = dt;
            this.gridView1.IsCellSelected(this.gridView1.FocusedRowHandle, this.PATIENT);

        }

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
            if (e.Column == this.PATIENT)
            {
                var disname = this.gridView1.GetFocusedRowCellDisplayText(PATIENT);
                var username = this._patientDataTable.FirstOrDefault(i => i.NAME == disname);
                if (username != null)
                {
                    this.gridView1.SetFocusedRowCellValue(HEMODIALYSIS_ID, username.HEMODIALYSIS_ID);
                    this.gridView1.SetFocusedRowCellValue(SEX, username.SEX);
                    this.gridView1.SetFocusedRowCellValue(AGE, username.AGE);
                    this.gridView1.SetFocusedRowCellValue(DIAGNOSE, username.DIAGNOSE);

                }
                this.gridView1.IsCellSelected(this.gridView1.FocusedRowHandle, this.CONTENT); 
            }


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
            InitalizeData();
        }

        private void DoctorChangeWork_Load(object sender, EventArgs e)
        {
            InitalizeData();
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            string title = string.Format("{0}~{1}", this.beginTime.DateTime.ToString("yyyy-MM-dd"),this.endTime.DateTime.ToString("yyyy-MM-dd"));
            DocChangeWorkReport frm = new DocChangeWorkReport(_docChangeWork, title);
            ReportPrintTool pt = new ReportPrintTool(frm);
            pt.ShowPreviewDialog();
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.PATIENT)
            {
                var disname = this.gridView1.GetFocusedRowCellDisplayText(PATIENT);
                var username = this._patientDataTable.FirstOrDefault(i => i.NAME == disname);
                if (username != null)
                {
                    this.gridView1.SetFocusedRowCellValue(HEMODIALYSIS_ID, username.HEMODIALYSIS_ID);                    
                    this.gridView1.SetFocusedRowCellValue(SEX, username.SEX);
                    this.gridView1.SetFocusedRowCellValue(AGE, username.AGE);
                    this.gridView1.SetFocusedRowCellValue(DIAGNOSE, username.DIAGNOSE);        
                
                }
            }
        }
         #endregion
    }
}
