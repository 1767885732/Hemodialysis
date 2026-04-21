/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:护士工作记录
 * 创建标识:吕志强-2013年6月5日
 * 
 * 修改时间:2013年9月13日
 * 修改人:刘超
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2013年12月22日
 * 修改人:刘超
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年4月1日
 * 修改人:贺建操
 * 修改描述:修改方法SQL
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
    public partial class NurseWorkTimeRecord : HemoBaseFrm
    {
        #region 私有变量
        private HemoModel.MED_HEMO_WORKOVERTIMEDataTable _workOverTime = null;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IHemodialysis _hemoService = ServiceManager.Instance.HemodialysisService;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _cureTypes;
        private PatientModel.MED_PATIENTSDataTable _patientDataTable;
        private IPatient _patientService = ServiceManager.Instance.PatientService;
        private IPatientSchedule patientScheduleService = ServiceManager.Instance.PatientSchedule;

        private DataTable dtStaffSict = new DataTable();
        private bool HasDirty = false;
        #endregion
        #region 构造函数
        public NurseWorkTimeRecord()
        {
            InitializeComponent();
            DateTime dt = Utility.CDate(patientScheduleService.GetServerDate()).Date; //当前时间  
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初 
            DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);  //本月月末 
            this.beginTime.DateTime = startMonth;
            this.endTime.DateTime = endMonth;

            this.Text = "护士加班工作量录入 ";

            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountFrm(this);
            
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
                    dtStaffSict = _staffDictService.GetStaffDictList();
                    this._cureTypes = this._configService.GetConfigList(string.Empty, string.Empty, "净化方式", "1");
                    this._patientDataTable = this._patientService.GetPatientList();
                    _workOverTime = _hemoService.GetNurseWorkOverTimeRecordByDate(this.beginTime.DateTime, this.endTime.DateTime);
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    DataTable dtPunctureNurseList = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'", "name");
                    this.repositoryItemCustomGridLookUpEdit1.DataSource = dtPunctureNurseList;
                    this.APPItemLookUpEdit.DataSource = _cureTypes;
                    this.repositoryItemCustomGridLookUpEdit2.DataSource = _patientDataTable;
                    this.bindingSource1.DataSource = _workOverTime;
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
                    var current = (this.bindingSource1.Current as DataRowView).Row as HemoModel.MED_HEMO_WORKOVERTIMERow;
                    _hemoService.DeleteNurseWorkOverTimeByID(current.ID);
                    _workOverTime.RemoveMED_HEMO_WORKOVERTIMERow(current);
                    this.HasDirty = false;
                    var rows = _workOverTime.Where(i => i.RowState != DataRowState.Deleted);
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
            if (_workOverTime == null)
                return;

            this.gridView1.ClearColumnErrors();
            this.bindingSource1.EndEdit();
            this.bindingSource1.CurrencyManager.EndCurrentEdit();

            #region MyRegion

            //数据验证
            for (int i = 0; i < this.gridView1.DataRowCount; i++)
            {
                //名称不能为空
                string WORKDATE = this.gridView1.GetRowCellDisplayText(i, "WORKDATE");
                if (string.IsNullOrEmpty(WORKDATE) || string.IsNullOrEmpty(WORKDATE.Trim()))
                {
                    this.gridView1.FocusedRowHandle = i;
                    this.gridView1.SelectCell(i, this.gridView1.Columns["WORKDATE"]);
                    this.gridView1.SetColumnError(this.gridView1.Columns["WORKDATE"], "请输入加班日期");
                    return;
                }

                string USERID = this.gridView1.GetRowCellDisplayText(i, "USERID");
                if (string.IsNullOrEmpty(USERID) || string.IsNullOrEmpty(USERID.Trim()))
                {
                    this.gridView1.FocusedRowHandle = i;
                    this.gridView1.SelectCell(i, this.gridView1.Columns["USERID"]);
                    this.gridView1.SetColumnError(this.gridView1.Columns["USERID"], "请输入加班人员");
                    return;
                }

                string CURETYPE = this.gridView1.GetRowCellDisplayText(i, "CURETYPE");
                if (string.IsNullOrEmpty(CURETYPE) || string.IsNullOrEmpty(CURETYPE.Trim()))
                {
                    this.gridView1.FocusedRowHandle = i;
                    this.gridView1.SelectCell(i, this.gridView1.Columns["CURETYPE"]);
                    this.gridView1.SetColumnError(this.gridView1.Columns["CURETYPE"], "治疗方式");
                    return;
                }

                string HEMODIALYSIS_ID = this.gridView1.GetRowCellDisplayText(i, "HEMODIALYSIS_ID");
                if (string.IsNullOrEmpty(HEMODIALYSIS_ID) || string.IsNullOrEmpty(HEMODIALYSIS_ID.Trim()))
                {
                    this.gridView1.FocusedRowHandle = i;
                    this.gridView1.SelectCell(i, this.gridView1.Columns["HEMODIALYSIS_ID"]);
                    this.gridView1.SetColumnError(this.gridView1.Columns["HEMODIALYSIS_ID"], "对应患者");
                    return;
                }

                string WORKTIME = this.gridView1.GetRowCellDisplayText(i, "WORKTIME");
                if (string.IsNullOrEmpty(WORKTIME) || string.IsNullOrEmpty(WORKTIME.Trim()))
                {
                    this.gridView1.FocusedRowHandle = i;
                    this.gridView1.SelectCell(i, this.gridView1.Columns["WORKTIME"]);
                    this.gridView1.SetColumnError(this.gridView1.Columns["WORKTIME"], "工作时长");
                    return;
                }
                //if (_workOverTime.Where(k => (k.RowState != DataRowState.Deleted)).Count(j => j.WORKDATE == Convert.ToDateTime(WORKDATE)) > 1)
                //{
                //    this.gridView1.FocusedRowHandle = i;
                //    this.gridView1.SelectCell(i, this.gridView1.Columns["NAME"]);
                //    this.gridView1.SetColumnError(this.gridView1.Columns["NAME"], "角色名称不能重复");
                //    return;
                //}

            }
            //保存
            if (_hemoService.SaveNurseWorkOverTime(_workOverTime) > 0)
            {
                AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);
              //  XtraMessageBox.Show("保存成功!");
            }
            InitalizeData();

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
            row["CREATEDATE"] = DateTime.Now;
            row["CREATEBY"] = HemoApplicationContext.Current.CurrentUser.USER_ID;
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

        private void NurseWorkTimeRecord_Load(object sender, EventArgs e)
        {
            InitalizeData();
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            string title = string.Format("{0}~{1}", this.beginTime.DateTime.ToString("yyyy-MM-dd"),this.endTime.DateTime.ToString("yyyy-MM-dd"));

            NurseWorkOverTimePrint _doc = new NurseWorkOverTimePrint(_workOverTime,title);
            ReportPrintTool pt = new ReportPrintTool(_doc);
            pt.ShowPreviewDialog();

        }
        #endregion
    }
}
