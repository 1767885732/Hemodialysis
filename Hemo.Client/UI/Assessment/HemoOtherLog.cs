/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:医生改变
 * 创建标识:贺建操-2013年7月3日

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
using DevExpress.XtraPrinting;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class HemoOtherLog : UserControl
    {

        #region 变量
        private PatientModel.MED_HEMO_OHTERLOGDataTable _hemoOtherLog = null;



        private IPatient _patientService = ServiceManager.Instance.PatientService;

        private IPatientSchedule patientScheduleService = ServiceManager.Instance.PatientSchedule;

        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;

        private bool HasDirty = false;

        private DateTime dt = new DateTime();
        private DateTime startMonth = new DateTime();


        #endregion
        #region 构造函数
        public HemoOtherLog()
        {
            InitializeComponent();

            dt = Utility.CDate(patientScheduleService.GetServerDate()).Date; //当前时间  
            startMonth = dt.AddDays(1 - dt.Day);  //本月月初 
            DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);  //本月月末 

            this.dtMonth.DateTime = dt;

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
                var dtStaffSict = new DictModel.MED_STAFF_DICTDataTable();
                _hemoOtherLog = new PatientModel.MED_HEMO_OHTERLOGDataTable();
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    dtStaffSict = _staffDictService.GetStaffDictList();
                    _hemoOtherLog = _patientService.GetHemoOhterLogByDt(this.dtMonth.DateTime);
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    DataTable dtPunctureNurseList = Utility.GetSubTable(dtStaffSict, "ZYNAME='医生'", "name");
                    this.repositoryItemCustomGridLookUpEdit2.DataSource = dtPunctureNurseList;

                    this.bindingSource1.DataSource = _hemoOtherLog;
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
                    var current = (this.bindingSource1.Current as DataRowView).Row as PatientModel.MED_HEMO_OHTERLOGRow;
                    this._patientService.DeleteHemoOtherLogById(current.ID);

                    _hemoOtherLog.RemoveMED_HEMO_OHTERLOGRow(current);
                    this.HasDirty = false;
                    var rows = _hemoOtherLog.Where(i => i.RowState != DataRowState.Deleted);
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
            if (_hemoOtherLog == null)
                return;

            this.bandedGridView1.ClearColumnErrors();
            this.bindingSource1.EndEdit();
            this.bindingSource1.CurrencyManager.EndCurrentEdit();

            #region 数据验证

            //数据验证
            for (int i = 0; i < this.bandedGridView1.DataRowCount; i++)
            {
                //名称不能为空
                string gridColumnDtTX = this.bandedGridView1.GetRowCellDisplayText(i, "LOGDAY");
                if (string.IsNullOrEmpty(gridColumnDtTX) || string.IsNullOrEmpty(gridColumnDtTX.Trim()))
                {
                    this.bandedGridView1.FocusedRowHandle = i;
                    this.bandedGridView1.SelectCell(i, gridColumnDt);
                    this.bandedGridView1.SetColumnError(gridColumnDt, "请输入日期");
                    return;
                }

                string gridColumnHemoCountTX = this.bandedGridView1.GetRowCellDisplayText(i, "HEMOCOUNT");
                if (string.IsNullOrEmpty(gridColumnHemoCountTX) || string.IsNullOrEmpty(gridColumnHemoCountTX.Trim()))
                {
                    this.bandedGridView1.FocusedRowHandle = i;
                    this.bandedGridView1.SelectCell(i, gridColumnHemoCount);
                    this.bandedGridView1.SetColumnError(this.bandedGridView1.Columns["gridColumnHemoCount"], "请输入患者人数");
                    return;
                }

                string gridColumn7Tx = this.bandedGridView1.GetRowCellDisplayText(i, "CREATER");
                if (string.IsNullOrEmpty(gridColumn7Tx) || string.IsNullOrEmpty(gridColumn7Tx.Trim()))
                {
                    this.bandedGridView1.FocusedRowHandle = i;
                    this.bandedGridView1.SelectCell(i, gridColumn7);
                    this.bandedGridView1.SetColumnError(gridColumn7, "请输入填表人");
                    return;
                }

            }
            //保存
            if (this._patientService.SaveHemoOtherLogInfo(_hemoOtherLog) > 0)
            {

                AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);
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
        private void bandedGridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var row = this.bandedGridView1.GetDataRow(e.RowHandle);
            if (row == null)
                return;
            row["ID"] = Guid.NewGuid().ToString();
            if (_hemoOtherLog != null && _hemoOtherLog.Rows.Count > 0)
            {
                var itemDt = _hemoOtherLog.Select(t => t.Field<DateTime>("LOGDAY")).Max();
                row["LOGDAY"] = itemDt.AddDays(1);
            }
            else
            {
                row["LOGDAY"] = DateTime.Now;
            }
            row["CREATER"] = HemoApplicationContext.Current.CurrentUser.EMP_NO;
            row["CREATED"] = dt;
            this.bandedGridView1.IsCellSelected(this.bandedGridView1.FocusedRowHandle, this.gridColumnHemoCount);

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
            this.bandedGridView1.OptionsView.ShowAutoFilterRow = this.iFilterCheckEdit.Checked;
        }
        /// <summary>
        /// 值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bandedGridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.gridColumnDt)
            {
                var currentDt = this.bandedGridView1.GetFocusedRowCellDisplayText(gridColumnDt);
                var currentDtValue = this.bandedGridView1.GetFocusedRowCellValue(gridColumnDt);

                if (!string.IsNullOrEmpty(currentDt))
                {
                    //根据当前日，去获取数据看是否存在，存在直接 复制不然就为空
                    var singleOhterLogDt = _patientService.GetHemoSingleOhterLogByDt(Convert.ToDateTime(currentDt));
                    if (singleOhterLogDt != null && singleOhterLogDt.Rows.Count > 0)
                    {
                        XtraMessageBox.Show("此日期已存在数据，请重新选择日期！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Question);

                        var row = this.bandedGridView1.GetDataRow(e.RowHandle);

                        var itemDt = _hemoOtherLog.Select(t => t.Field<DateTime>("LOGDAY")).Max();
                        //row["LOGDAY"] = itemDt.AddDays(1);
                        this.bandedGridView1.SetFocusedRowCellValue(gridColumnDt, itemDt.AddDays(1));
                        var ZX = this.bandedGridView1.GetFocusedRowCellValue(gridColumnDt);

                        //bindingSource1
                        this.bandedGridView1.IsCellSelected(this.bandedGridView1.FocusedRowHandle, this.gridColumnDt);

                        return;
                    }
                    else
                    {
                        //去取他的值，得到这一天的透析人数等 信息
                        var dtCure = _patientService.GetHemoOtherLogCureDtByTime(Convert.ToDateTime(currentDt));
                        int NLCOUNT = 0;
                        int LONGCOUNT = 0;
                        int TEMPCOUNT = 0;
                        int CureCount = 0;
                        //得到 治疗信息
                        foreach (DataRow dr in dtCure.Rows)
                        {
                            if (dr["NAME"].ToString().Contains("内瘘"))
                            {
                                NLCOUNT += int.Parse(dr["COUNT"].ToString());
                            }
                            else if (dr["NAME"].ToString().Contains("永久"))
                            {
                                LONGCOUNT += int.Parse(dr["COUNT"].ToString());
                            }
                            else if (dr["NAME"].ToString().Contains("半永久") || dr["NAME"].ToString().Contains("临时"))
                            {
                                TEMPCOUNT += int.Parse(dr["COUNT"].ToString());
                            }
                        }
                        CureCount = NLCOUNT + LONGCOUNT + TEMPCOUNT;

                        this.bandedGridView1.SetFocusedRowCellValue(gridColumnHemoCount, CureCount);
                        this.bandedGridView1.SetFocusedRowCellValue(gridColumn1, NLCOUNT);
                        this.bandedGridView1.SetFocusedRowCellValue(gridColumn2, LONGCOUNT);
                        this.bandedGridView1.SetFocusedRowCellValue(gridColumn3, TEMPCOUNT);
                    }
                }

                this.bandedGridView1.IsCellSelected(this.bandedGridView1.FocusedRowHandle, this.gridColumnHemoCount);
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

        private void HemoOtherLog_Load(object sender, EventArgs e)
        {
            InitalizeData();
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
            link.Component = this.gridControl1;
            link.Landscape = false;
            link.PaperKind = System.Drawing.Printing.PaperKind.A4;

            var margins = new System.Drawing.Printing.Margins(10, 10, 100, 50);
            link.Margins = margins;

            link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
            link.CreateDocument();
            link.ShowPreview();
        }
        private void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            string title = string.Format("{0}血液透析相关目标性监测日志", Utility.GetHospitalName());
            PageInfoBrick brick = e.Graph.DrawPageInfo(PageInfo.None, title, Color.Black,
               new RectangleF(0, 0, 100, 30), BorderSide.None);

            brick.LineAlignment = BrickAlignment.Center;
            brick.Alignment = BrickAlignment.Center;
            brick.AutoWidth = true;

            brick.Font = new System.Drawing.Font("宋体", 11f, FontStyle.Bold);
        }

        private void bandedGridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.gridColumnDt)
            {
                var currentDt = this.bandedGridView1.GetFocusedRowCellDisplayText(gridColumnDt);
                var currentDtValue = this.bandedGridView1.GetFocusedRowCellValue(gridColumnDt);

                if (!string.IsNullOrEmpty(currentDt))
                {
                    //根据当前日，去获取数据看是否存在，存在直接 复制不然就为空
                    var singleOhterLogDt = _patientService.GetHemoSingleOhterLogByDt(Convert.ToDateTime(currentDt));
                    if (singleOhterLogDt != null && singleOhterLogDt.Rows.Count > 0)
                    {
                        XtraMessageBox.Show("此日期已存在数据，请重新选择日期！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Question);

                        var row = this.bandedGridView1.GetDataRow(e.RowHandle);

                        var itemDt = _hemoOtherLog.Select(t => t.Field<DateTime>("LOGDAY")).Max();
                        //row["LOGDAY"] = itemDt.AddDays(1);
                        this.bandedGridView1.SetFocusedRowCellValue(gridColumnDt, itemDt.AddDays(1));
                        var ZX = this.bandedGridView1.GetFocusedRowCellValue(gridColumnDt);

                        //bindingSource1
                        this.bandedGridView1.IsCellSelected(this.bandedGridView1.FocusedRowHandle, this.gridColumnDt);

                        return;
                    }
                    else
                    {
                        //去取他的值，得到这一天的透析人数等 信息
                        var dtCure = _patientService.GetHemoOtherLogCureDtByTime(Convert.ToDateTime(currentDt));
                        int NLCOUNT = 0;
                        int LONGCOUNT = 0;
                        int TEMPCOUNT = 0;
                        int CureCount = 0;
                        //得到 治疗信息
                        foreach (DataRow dr in dtCure.Rows)
                        {
                            if (dr["NAME"].ToString().Contains("内瘘"))
                            {
                                NLCOUNT += int.Parse(dr["COUNT"].ToString());
                            }
                            else if (dr["NAME"].ToString().Contains("永久"))
                            {
                                LONGCOUNT += int.Parse(dr["COUNT"].ToString());
                            }
                            else if (dr["NAME"].ToString().Contains("半永久") || dr["NAME"].ToString().Contains("临时"))
                            {
                                TEMPCOUNT += int.Parse(dr["COUNT"].ToString());
                            }
                        }
                        CureCount = NLCOUNT + LONGCOUNT + TEMPCOUNT;

                        this.bandedGridView1.SetFocusedRowCellValue(gridColumnHemoCount, CureCount);
                        this.bandedGridView1.SetFocusedRowCellValue(gridColumn1, NLCOUNT);
                        this.bandedGridView1.SetFocusedRowCellValue(gridColumn2, LONGCOUNT);
                        this.bandedGridView1.SetFocusedRowCellValue(gridColumn3, TEMPCOUNT);
                    }
                }
            }
        }
        #endregion
    }
}
