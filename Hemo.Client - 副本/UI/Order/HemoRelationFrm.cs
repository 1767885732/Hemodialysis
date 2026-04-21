/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：透析器药品默认值维护用户控件类
// 创建时间：2014-04-07
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
using Hemo.Client.UI.Machine;

namespace Hemo.Client.UI.Order
{
    public partial class HemoRelationFrm : ViewBase
    {
        #region 类变量

        private ConfigModel.MED_COMMON_RELATIONDataTable _relations = new ConfigModel.MED_COMMON_RELATIONDataTable();

        private ConfigModel.MED_COMMON_RELATIONDataTable _relationshift = new ConfigModel.MED_COMMON_RELATIONDataTable();

        private IDrug objDrug = ServiceManager.Instance.DrugService;

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

        public HemoRelationFrm()
        {
            InitializeComponent();
            dt = Utility.CDate(patientScheduleService.GetServerDate()).Date; //当前时间
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
            if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                this.bindingSource1.AddNew();
                this.HasDirty = true;
                this.iSaveButton.Enabled = true;
            }
            else if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                this.bindingSource2.AddNew();
                this.HasDirty = true;
                this.iSaveButton.Enabled = true;

            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iDeleteButton_Click(object sender, EventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                if (this.bindingSource1.Current != null)
                {
                    if (XtraMessageBox.Show("你确定要删除选中的项吗？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.bindingSource1.EndEdit();
                        var current = (this.bindingSource1.Current as DataRowView).Row as ConfigModel.MED_COMMON_RELATIONRow;
                        this._configService.DeleteCommonRelationById(current.ID);

                        _relations.RemoveMED_COMMON_RELATIONRow(current);
                        this.HasDirty = false;
                        var rows = _relations.Where(i => i.RowState != DataRowState.Deleted);
                        foreach (var row in rows)
                        {
                            if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                                this.HasDirty = true;
                        }
                        this.iSaveButton.Enabled = this.HasDirty;
                    }

                }
            }
            else if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                if (this.bindingSource2.Current != null)
                {
                    if (XtraMessageBox.Show("你确定要删除选中的项吗？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.bindingSource1.EndEdit();
                        var current = (this.bindingSource2.Current as DataRowView).Row as ConfigModel.MED_COMMON_RELATIONRow;
                        this._configService.DeleteCommonRelationById(current.ID);

                        _relationshift.RemoveMED_COMMON_RELATIONRow(current);
                        this.HasDirty = false;
                        var rows = _relations.Where(i => i.RowState != DataRowState.Deleted);
                        foreach (var row in rows)
                        {
                            if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                                this.HasDirty = true;
                        }
                        this.iSaveButton.Enabled = this.HasDirty;
                    }

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
            #region MyRegion
            if (xtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                #region MyRegion
                if (_relations == null)
                    return;

                this.gridView1.ClearColumnErrors();
                this.bindingSource1.EndEdit();
                this.bindingSource1.CurrencyManager.EndCurrentEdit();

                //数据验证
                for (int i = 0; i < this.gridView1.DataRowCount; i++)
                {
                    //名称不能为空
                    string ITEMNAME = this.gridView1.GetRowCellDisplayText(i, "ITEMNAME");
                    if (string.IsNullOrEmpty(ITEMNAME) || string.IsNullOrEmpty(ITEMNAME.Trim()))
                    {
                        this.gridView1.FocusedRowHandle = i;
                        this.gridView1.SelectCell(i, this.gridView1.Columns["ITEMNAME"]);
                        this.gridView1.SetColumnError(this.gridView1.Columns["ITEMNAME"], "请输入名称");
                        return;
                    }

                    string DOSAGE = this.gridView1.GetRowCellDisplayText(i, "DOSAGE");
                    if (string.IsNullOrEmpty(DOSAGE) || string.IsNullOrEmpty(DOSAGE.Trim()))
                    {
                        this.gridView1.FocusedRowHandle = i;
                        this.gridView1.SelectCell(i, this.gridView1.Columns["DOSAGE"]);
                        this.gridView1.SetColumnError(this.gridView1.Columns["DOSAGE"], "请输入剂量");
                        return;
                    }

                    string UNIT = this.gridView1.GetRowCellDisplayText(i, "UNIT");
                    if (string.IsNullOrEmpty(UNIT) || string.IsNullOrEmpty(UNIT.Trim()))
                    {
                        this.gridView1.FocusedRowHandle = i;
                        this.gridView1.SelectCell(i, this.gridView1.Columns["UNIT"]);
                        this.gridView1.SetColumnError(this.gridView1.Columns["UNIT"], "请输入单位");
                        return;
                    }
                    string DRUGMODE = this.gridView1.GetRowCellDisplayText(i, "DRUGMODE");
                    if (string.IsNullOrEmpty(DRUGMODE) || string.IsNullOrEmpty(DRUGMODE.Trim()))
                    {
                        this.gridView1.FocusedRowHandle = i;
                        this.gridView1.SelectCell(i, this.gridView1.Columns["DRUGMODE"]);
                        this.gridView1.SetColumnError(this.gridView1.Columns["DRUGMODE"], "请输入给药途径");
                        return;
                    }
                }
                //保存
                if (this._configService.SaveCommonRelation(_relations) > 0)
                {

                    AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);
                    //XtraMessageBox.Show("保存成功!");
                }

                #endregion
            }
            else if (xtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                #region MyRegion
                if (_relationshift == null)
                    return;

                this.gridView2.ClearColumnErrors();
                this.bindingSource2.EndEdit();
                this.bindingSource2.CurrencyManager.EndCurrentEdit();



                //数据验证
                for (int i = 0; i < this.gridView2.DataRowCount; i++)
                {
                    //名称不能为空
                    string RELATIONNAME = this.gridView2.GetRowCellDisplayText(i, "RELATIONNAME");
                    if (string.IsNullOrEmpty(RELATIONNAME) || string.IsNullOrEmpty(RELATIONNAME.Trim()))
                    {
                        this.gridView2.FocusedRowHandle = i;
                        this.gridView2.SelectCell(i, this.gridView2.Columns["RELATIONNAME"]);
                        this.gridView2.SetColumnError(this.gridView2.Columns["RELATIONNAME"], "请输入透析器类型");
                        return;
                    }

                    string ITEMNAME = this.gridView2.GetRowCellDisplayText(i, "ITEMNAME");
                    if (string.IsNullOrEmpty(ITEMNAME) || string.IsNullOrEmpty(ITEMNAME.Trim()))
                    {
                        this.gridView2.FocusedRowHandle = i;
                        this.gridView2.SelectCell(i, this.gridView2.Columns["ITEMNAME"]);
                        this.gridView2.SetColumnError(this.gridView2.Columns["ITEMNAME"], "请输入透析膜");
                        return;
                    }

                    string DOSAGE = this.gridView2.GetRowCellDisplayText(i, "DOSAGE");
                    if (string.IsNullOrEmpty(DOSAGE) || string.IsNullOrEmpty(DOSAGE.Trim()))
                    {
                        this.gridView2.FocusedRowHandle = i;
                        this.gridView2.SelectCell(i, this.gridView2.Columns["DOSAGE"]);
                        this.gridView2.SetColumnError(this.gridView2.Columns["DOSAGE"], "请输入M2");
                        return;
                    }
                    //string UNIT = this.gridView2.GetRowCellDisplayText(i, "UNIT");
                    //if (string.IsNullOrEmpty(UNIT) || string.IsNullOrEmpty(UNIT.Trim()))
                    //{
                    //    this.gridView2.FocusedRowHandle = i;
                    //    this.gridView2.SelectCell(i, this.gridView2.Columns["UNIT"]);
                    //    this.gridView2.SetColumnError(this.gridView2.Columns["UNIT"], "请输入KOA");
                    //    return;
                    //}
                    //string DRUGMODE = this.gridView2.GetRowCellDisplayText(i, "DRUGMODE");
                    //if (string.IsNullOrEmpty(DRUGMODE) || string.IsNullOrEmpty(DRUGMODE.Trim()))
                    //{
                    //    this.gridView2.FocusedRowHandle = i;
                    //    this.gridView2.SelectCell(i, this.gridView2.Columns["DRUGMODE"]);
                    //    this.gridView2.SetColumnError(this.gridView2.Columns["DRUGMODE"], "请输入KUF");
                    //    return;
                    //}
                }
                //保存
                if (this._configService.SaveCommonRelation(_relationshift) > 0)
                {

                    AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);
                    //XtraMessageBox.Show("保存成功!");
                }

                #endregion
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
            this.CloseView();
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
            row["RELATIONTYPE"] = "0";
            row["CREATEBY"] = HemoApplicationContext.Current.CurrentUser.USER_ID;
            row["CREATEDATE"] = dt;
            this.gridView1.IsCellSelected(this.gridView1.FocusedRowHandle, this.gridColumn3);

        }
        private void gridView2_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var row = this.gridView2.GetDataRow(e.RowHandle);
            if (row == null)
                return;
            row["ID"] = Guid.NewGuid().ToString();
            row["RELATIONTYPE"] = "1";
            row["CREATEBY"] = HemoApplicationContext.Current.CurrentUser.USER_ID;
            row["CREATEDATE"] = dt;
            this.gridView1.IsCellSelected(this.gridView2.FocusedRowHandle, this.gridColumn11);
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                this.iDeleteButton.Enabled = this.bindingSource1.Current != null;
            }
            else if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                this.iDeleteButton.Enabled = this.bindingSource2.Current != null;
            }

        }
        /// <summary>
        /// 显示过虑行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iFilterCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            this.gridView1.OptionsView.ShowAutoFilterRow = this.iFilterCheckEdit.Checked;
            this.gridView2.OptionsView.ShowAutoFilterRow = this.iFilterCheckEdit.Checked;
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

        private void HemoRelationFrm_Load(object sender, EventArgs e)
        {
            InitalizeData();
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
            var FIRST_PURIFIER_MODEL = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
            var FIRST_PURIFIER_MODELNAME = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
            var THERAPEUTIC_METHOD = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
            var FIRST_DRUG_UNIT = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
            var FIRST_DRUG_MODE = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
            var _relationData = new ConfigModel.MED_COMMON_RELATIONDataTable();
            var items = new DrugModel.MED_DRUG_MASTERDataTable();

            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    _relationData = this._configService.GetCommRelation();
                    FIRST_PURIFIER_MODEL = this._configService.GetConfigList(string.Empty, string.Empty, "净化器类型", "1");
                    FIRST_PURIFIER_MODELNAME = this._configService.GetConfigList(string.Empty, string.Empty, "透析膜", "1");
                    THERAPEUTIC_METHOD = this._configService.GetConfigList(string.Empty, string.Empty, "治疗方法", "1");
                    FIRST_DRUG_UNIT = this._configService.GetConfigList(string.Empty, string.Empty, "药品单位", "1");
                    items = objDrug.GetDrugMasterList();
                    FIRST_DRUG_MODE = this._configService.GetConfigList(string.Empty, string.Empty, "注射方式", "1");
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.repositoryItemLookUpEdit1.DataSource = items;
                    this.repositoryItemLookUpEdit2.DataSource = FIRST_DRUG_UNIT;
                    this.repositoryItemLookUpEdit3.DataSource = FIRST_DRUG_MODE;
                    this.repositoryItemLookUpEdit4.DataSource = FIRST_PURIFIER_MODEL;
                    this.repositoryItemLookUpEdit5.DataSource = FIRST_PURIFIER_MODELNAME;
                    _relationData.Where(i => i.RELATIONTYPE == "0").CopyToDataTable<ConfigModel.MED_COMMON_RELATIONRow>(_relations, LoadOption.PreserveChanges);
                    _relationData.Where(i => i.RELATIONTYPE == "1").CopyToDataTable<ConfigModel.MED_COMMON_RELATIONRow>(_relationshift, LoadOption.PreserveChanges);

                    this.bindingSource1.DataSource = _relations;
                    this.bindingSource2.DataSource = _relationshift;

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
    }
}
