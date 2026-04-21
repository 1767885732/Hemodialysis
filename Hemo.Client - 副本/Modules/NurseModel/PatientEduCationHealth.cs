/*----------------------------------------------------------------
 * Copyright (C) 2005 麦迪斯顿(苏州)医疗科技发展有限公司
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
using DevExpress.XtraPrinting;

namespace Hemo.Client.Modules.NurseModel
{
    public partial class PatientEduCationHealth : XtraUserControl
    {
        #region 私有变量
        private HemodialysisModel.MED_HEALTH_EDUCATIONDataTable _EducationHeathDt = null;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IHemodialysis _hemoService = ServiceManager.Instance.HemodialysisService;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _cureTypes;
        private PatientModel.MED_PATIENTSDataTable _patientDataTable;
        private IPatient _patientService = ServiceManager.Instance.PatientService;
        private IPatientSchedule patientScheduleService = ServiceManager.Instance.PatientSchedule;
        public string pHemoID { get; set; }
        private DataTable dtStaffSict = new DataTable();
        private bool HasDirty = false;
        #endregion
        #region 构造函数
        public PatientEduCationHealth()
        {
            InitializeComponent();
            DateTime dt = Utility.CDate(patientScheduleService.GetServerDate()).Date; //当前时间  
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初 
            DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);  //本月月末 
            this.beginTime.DateTime = startMonth;
            this.endTime.DateTime = endMonth;
        }
        #endregion
        #region 事件
        /// <summary>
        /// 初使化数据
        /// </summary>
        public void InitalizeData()
        {
            SetButtonStates(false, false, false, false);
            this.busyIndicator.ShowLoadingScreenFor(this.gridControl1);
            var   _educationHeathDtTemp = new HemodialysisModel.MED_HEALTH_EDUCATIONDataTable();
            _EducationHeathDt = new HemodialysisModel.MED_HEALTH_EDUCATIONDataTable();
            var itemDt = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    itemDt = _configService.GetConfigList(string.Empty, string.Empty, "宣教评价", "1");
                    dtStaffSict = _staffDictService.GetStaffDictList();
                    _educationHeathDtTemp = _hemoService.GetHealthEducationByHemoID(pHemoID);
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    DataTable dtPunctureNurseList = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'", "name");
                    this.repositoryItemCustomGridLookUpEdit1.DataSource = dtPunctureNurseList;
                    this.repositoryItemCustomGridLookUpEdit3.DataSource = dtPunctureNurseList;
                    repositoryItemCustomGridLookUpEdit2.DataSource = itemDt;
                    this.repositoryItemCustomGridLookUpEdit4.DataSource = dtPunctureNurseList;
                    repositoryItemCustomGridLookUpEdit5.DataSource = itemDt;
                    _educationHeathDtTemp.Where(i=>i.CREATE_DATE >= this.beginTime.DateTime.Date && i.CREATE_DATE <= this.endTime.DateTime.Date).CopyToDataTable(_EducationHeathDt,LoadOption.PreserveChanges);
                    this.bindingSource1.DataSource = _EducationHeathDt;
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
            this.gridView1.OptionsBehavior.Editable = true;

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
                    var current = (this.bindingSource1.Current as DataRowView).Row as HemodialysisModel.MED_HEALTH_EDUCATIONRow;
                    _hemoService.DeleteHealthEducationByHemoIdAndId(pHemoID, current.ID);
                    _EducationHeathDt.RemoveMED_HEALTH_EDUCATIONRow(current);
                    this.HasDirty = false;
                    var rows = _EducationHeathDt.Where(i => i.RowState != DataRowState.Deleted);
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
            if (_EducationHeathDt == null)
                return;

            this.gridView1.ClearColumnErrors();
            this.bindingSource1.EndEdit();
            this.bindingSource1.CurrencyManager.EndCurrentEdit();

            #region MyRegion

            //数据验证
            for (int i = 0; i < this.gridView1.DataRowCount; i++)
            {
                //名称不能为空
                string HEALTH_NURSE_DATE = this.gridView1.GetRowCellDisplayText(i, "HEALTH_NURSE_DATE");
                if (string.IsNullOrEmpty(HEALTH_NURSE_DATE) || string.IsNullOrEmpty(HEALTH_NURSE_DATE.Trim()))
                {
                    this.gridView1.FocusedRowHandle = i;
                    this.gridView1.SelectCell(i, this.gridView1.Columns["gridcolumn1"]);
                    this.gridView1.SetColumnError(this.gridView1.Columns["gridcolumn1"], "请输入宣教日期");
                    return;
                }

                string HEALTH_NURSE_ID = this.gridView1.GetRowCellDisplayText(i, "HEALTH_NURSE_ID");
                if (string.IsNullOrEmpty(HEALTH_NURSE_ID) || string.IsNullOrEmpty(HEALTH_NURSE_ID.Trim()))
                {
                    this.gridView1.FocusedRowHandle = i;
                    this.gridView1.SelectCell(i, this.gridView1.Columns["gridcolumn2"]);
                    this.gridView1.SetColumnError(this.gridView1.Columns["gridcolumn2"], "请选择宣教人员");
                    return;
                }

                string HEALTH_VERBAL = this.gridView1.GetRowCellDisplayText(i, "HEALTH_VERBAL");
                if (string.IsNullOrEmpty(HEALTH_VERBAL) || string.IsNullOrEmpty(HEALTH_VERBAL.Trim()))
                {
                    this.gridView1.FocusedRowHandle = i;
                    this.gridView1.SelectCell(i, this.gridView1.Columns["gridcolumn3"]);
                    this.gridView1.SetColumnError(this.gridView1.Columns["gridcolumn3"], "请录入宣教内容");
                    return;
                }

                string HEALTH_HEADMAN_DATE = this.gridView1.GetRowCellDisplayText(i, "HEALTH_HEADMAN_DATE");
                if (string.IsNullOrEmpty(HEALTH_HEADMAN_DATE) || string.IsNullOrEmpty(HEALTH_HEADMAN_DATE.Trim()))
                {
                    this.gridView1.FocusedRowHandle = i;
                    this.gridView1.SelectCell(i, this.gridView1.Columns["gridColumn8"]);
                    this.gridView1.SetColumnError(this.gridView1.Columns["gridColumn8"], "请录入评价日期");
                    return;
                }

                string HEALTH_HEADMAN_APPRAISE = this.gridView1.GetRowCellDisplayText(i, "HEALTH_HEADMAN_APPRAISE");
                if (string.IsNullOrEmpty(HEALTH_HEADMAN_APPRAISE) || string.IsNullOrEmpty(HEALTH_HEADMAN_APPRAISE.Trim()))
                {
                    this.gridView1.FocusedRowHandle = i;
                    this.gridView1.SelectCell(i, this.gridView1.Columns["gridColumn4"]);
                    this.gridView1.SetColumnError(this.gridView1.Columns["gridColumn4"], "请录入评价内容");
                    return;
                }
                string HEALTH_HEADMAN_ID = this.gridView1.GetRowCellDisplayText(i, "HEALTH_HEADMAN_ID");
                if (string.IsNullOrEmpty(HEALTH_HEADMAN_ID) || string.IsNullOrEmpty(HEALTH_HEADMAN_ID.Trim()))
                {
                    this.gridView1.FocusedRowHandle = i;
                    this.gridView1.SelectCell(i, this.gridView1.Columns["gridColumn5"]);
                    this.gridView1.SetColumnError(this.gridView1.Columns["gridColumn5"], "请选择评价者");
                    return;
                }
            }
            //保存
            if (_hemoService.SaveHealthEducationInfo(_EducationHeathDt) > 0)
            {
                AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);

            }
            InitalizeData();
            this.gridView1.OptionsBehavior.Editable = false;

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
            row["HEALTH_ID"] = Guid.NewGuid().ToString();
            row["CREATE_DATE"] = DateTime.Now;
            row["HEMODIALYSIS_ID"] = pHemoID;
            row["HEALTH_NURSE_DATE"] = DateTime.Now;
            row["HEALTH_HEADMAN_DATE"] = DateTime.Now;
            //row["HEALTH_HEADNURSE_ID"] = HemoApplicationContext.Current.CurrentUser.USER_ID;
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

        private void PatientEduCationHealth_Load(object sender, EventArgs e)
        {
            InitalizeData();
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {

            PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
            link.Component = this.gridControl1;
            link.Landscape = true;
            link.PaperKind = System.Drawing.Printing.PaperKind.A4;
            link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
            link.CreateDocument();
            link.ShowPreview();
        }
        private void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            string title = string.Format("健康宣教\r\n{0}~{1}", this.beginTime.DateTime.ToString("yyyy-MM-dd"), this.endTime.DateTime.ToString("yyyy-MM-dd"));

            PageInfoBrick brick = e.Graph.DrawPageInfo(PageInfo.None, title, Color.DarkBlue,
               new RectangleF(0, 0, 100, 25), BorderSide.None);
            brick.Text = "健康宣教";

            brick.LineAlignment = BrickAlignment.Center;
            brick.Alignment = BrickAlignment.Center;
            brick.AutoWidth = true;
            brick.Font = new System.Drawing.Font("宋体", 11f, FontStyle.Bold);
        }
        #endregion

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.gridView1.OptionsBehavior.Editable = true;
        }
    }
}
