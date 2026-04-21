/*----------------------------------------------------------------
// Copyright (C) 2013 苏州麦迪斯顿医疗科技股份有限公司有限公司
// 描述：药品耗材入库信息列表窗体
// 创建时间：2013-07-30
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
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
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Hemo.Service;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.IService;
using Hemo.Client.Core;
using Hemo.Client.Print;


namespace Hemo.Client.UI.Drug {
    public partial class QueryMaterialCheckForm : HemoBaseFrm {

        private DrugService objDrug = new DrugService();
        private IMaterial objMaterial = ServiceManager.Instance.MaterialService;

        private DrugModel.MED_MATERIAL_CHECKDataTable dt = null;
        private DrugModel.MED_MATERIAL_INPUTDataTable dtInputDetail = null;
        private DrugModel.MED_MATERIAL_OUTPUTDataTable dtOutputDetail = null;

        public QueryMaterialCheckForm() {
            InitializeComponent();
            loadMaterialInputList();

        }

        private void chkFilter_CheckedChanged(object sender, EventArgs e) {
            this.gridView1.OptionsView.ShowAutoFilterRow = this.chkFilter.Checked;
        }

        /// <summary>
        /// 使用DataTable收集查询条件并返回数据集
        /// </summary>
        private void loadMaterialInputList() {
            busyIndicator1.Visible = true;
            busyIndicator1.ShowLoadingScreenFor(gridMaterialInput);
            dt = new DrugModel.MED_MATERIAL_CHECKDataTable();
            var dtHisList = new DrugModel.MED_MATERIAL_CHECKDataTable();

            using (BackgroundWorker worker = new BackgroundWorker()) {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    //dt = objDrug.GetMedMaterialCheckList(DateTime.Now, string.Empty);
                    dtHisList = objDrug.GetMedMaterialCheckHisList();
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    var dtMaster = new DrugModel.MED_MATERIAL_CHECKDataTable();
                    lupMaterialCheckList.Properties.DataSource = dtHisList;
                    if (dtHisList.Rows.Count > 0)
                        this.lupMaterialCheckList.EditValue = dtHisList[0].ID;
                    ////如果还用批号的话要改成0
                    //if (chkNullCount.Checked)
                    //{
                    //    dt.Where(i => i.CHECKTYPE == "1" && i.F_COUNT > 0).CopyToDataTable(dtMaster, LoadOption.PreserveChanges);
                    //}
                    //else
                    //{
                    //    dt.Where(i => i.CHECKTYPE == "1").CopyToDataTable(dtMaster, LoadOption.PreserveChanges);
                    //}

                    //if (dtMaster != null && dtMaster.Rows.Count > 0)
                    //{
                    //    gridMaterialInput.DataSource = dtMaster;
                    //}
                    //else
                    //{
                    //    gridMaterialInput.DataSource = null;
                    //}
                    this.busyIndicator1.HideLoadingScreen();
                };
                worker.RunWorkerAsync();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e) {
            loadMaterialInputList();
        }

        private void btn_Check_Click(object sender, EventArgs e) {
            if (objMaterial.CheckMaterialInOutStore(LoginUser.User.USER_ID) > 0) {
                XtraMessageBox.Show("盘点成功！", "盘点");
                loadMaterialInputList();
            }
            else {
                XtraMessageBox.Show("盘点失败！", "盘点");

            }

        }

        private void btn_Print_Click(object sender, EventArgs e) {
            MaterialCheckReport report = new MaterialCheckReport(Convert.ToDateTime(lupMaterialCheckList.Text), dt);

            report.ShowPreviewDialog();
        }

        private void gridView1_RowClick(object sender, RowClickEventArgs e) {
            var rowCurrent = this.gridView1.GetFocusedDataRow() as DrugModel.MED_MATERIAL_CHECKRow;

            if (rowCurrent == null || gridView1.FocusedColumn == null)
                return;

            //打开患者录入界面
            if (e.Button == MouseButtons.Left && !this.gridView1.IsGroupRow(e.RowHandle)) {
                #region 双击去获取明细数据。
                if (e.Clicks == 2) {
                    dtInputDetail = new DrugModel.MED_MATERIAL_INPUTDataTable();
                    dtOutputDetail = new DrugModel.MED_MATERIAL_OUTPUTDataTable();

                    objMaterial.GetMedMaterialInputDetailByCodeAndBatchNum(Convert.ToDateTime(lupMaterialCheckList.Text), rowCurrent.CODE, rowCurrent.BATCH_NUMBER).Where(i => i.UNITS == rowCurrent.UNITS).CopyToDataTable(dtInputDetail, LoadOption.PreserveChanges);
                    objMaterial.GetMedMaterialOutputDetailByCodeAndBatchNum(Convert.ToDateTime(lupMaterialCheckList.Text), rowCurrent.CODE, rowCurrent.BATCH_NUMBER).Where(i => i.UNITS == rowCurrent.UNITSNAME).CopyToDataTable(dtOutputDetail, LoadOption.PreserveChanges);

                    gridControl2.DataSource = dtInputDetail;
                    gridControl3.DataSource = dtOutputDetail;

                    this.xtraTabControl1.SelectedTabPage = this.xtraTabPage3;

                    #region 老的，不再按批号去搞。。，去掉

                    //var dtDetail = new DrugModel.MED_MATERIAL_CHECKDataTable();
                    //dt.Where(i => i.CHECKTYPE == "1" && i.CODE == rowCurrent.CODE && i.UNITS == rowCurrent.UNITS).CopyToDataTable(dtDetail, LoadOption.PreserveChanges);
                    //if (dtDetail != null && dtDetail.Rows.Count > 0)
                    //    this.gridControl1.DataSource = dtDetail;
                    //else
                    //    this.gridControl1.DataSource = null;
                    //this.xtraTabControl1.SelectedTabPage = this.xtraTabPage2;

                    #endregion
                }
                #endregion
            }
        }

        private void btnDetail_Click(object sender, EventArgs e) {
            var rowCurrent = this.gridView1.GetFocusedDataRow() as DrugModel.MED_MATERIAL_CHECKRow;

            if (rowCurrent == null || gridView1.FocusedColumn == null)
                return;


            dtInputDetail = new DrugModel.MED_MATERIAL_INPUTDataTable();
            dtOutputDetail = new DrugModel.MED_MATERIAL_OUTPUTDataTable();

            objMaterial.GetMedMaterialInputDetailByCodeAndBatchNum(Convert.ToDateTime(lupMaterialCheckList.Text), rowCurrent.CODE, rowCurrent.BATCH_NUMBER).Where(i => i.UNITS == rowCurrent.UNITS).CopyToDataTable(dtInputDetail, LoadOption.PreserveChanges);
            objMaterial.GetMedMaterialOutputDetailByCodeAndBatchNum(Convert.ToDateTime(lupMaterialCheckList.Text), rowCurrent.CODE, rowCurrent.BATCH_NUMBER).Where(i => i.UNITS == rowCurrent.UNITSNAME).CopyToDataTable(dtOutputDetail, LoadOption.PreserveChanges);

            gridControl2.DataSource = dtInputDetail;
            gridControl3.DataSource = dtOutputDetail;

            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage3;


            #region 老的，不再按批号去搞。。，去掉

            //var dtDetail = new DrugModel.MED_MATERIAL_CHECKDataTable();
            //dt.Where(i => i.CHECKTYPE == "1" && i.CODE == rowCurrent.CODE).CopyToDataTable(dtDetail, LoadOption.PreserveChanges);
            //if (dtDetail != null && dtDetail.Rows.Count > 0)
            //    this.gridControl1.DataSource = dtDetail;
            //else
            //    this.gridControl1.DataSource = null;
            //this.xtraTabControl1.SelectedTabPage = this.xtraTabPage2;
            #endregion
        }

        private void gridView2_RowClick(object sender, RowClickEventArgs e) {
            var rowCurrent = this.gridView2.GetFocusedDataRow() as DrugModel.MED_MATERIAL_CHECKRow;
            if (rowCurrent == null || gridView2.FocusedColumn == null)
                return;
            if (e.Button == MouseButtons.Left && !this.gridView1.IsGroupRow(e.RowHandle)) {
                if (e.Clicks == 2) {
                    dtInputDetail = new DrugModel.MED_MATERIAL_INPUTDataTable();
                    dtOutputDetail = new DrugModel.MED_MATERIAL_OUTPUTDataTable();

                    objMaterial.GetMedMaterialInputDetailByCodeAndBatchNum(Utility.CDate(lupMaterialCheckList.Text), rowCurrent.CODE, rowCurrent.BATCH_NUMBER).Where(i => i.UNITS == rowCurrent.UNITS).CopyToDataTable(dtInputDetail, LoadOption.PreserveChanges);
                    objMaterial.GetMedMaterialOutputDetailByCodeAndBatchNum(Utility.CDate(lupMaterialCheckList.Text), rowCurrent.CODE, rowCurrent.BATCH_NUMBER).Where(i => i.UNITS == rowCurrent.UNITSNAME).CopyToDataTable(dtOutputDetail, LoadOption.PreserveChanges);

                    gridControl2.DataSource = dtInputDetail;
                    gridControl3.DataSource = dtOutputDetail;

                    this.xtraTabControl1.SelectedTabPage = this.xtraTabPage3;
                }
            }
        }

        private void chkNullCount_CheckedChanged(object sender, EventArgs e) {
            loadMaterialInputList();
        }

        private void lupMaterialCheckList_EditValueChanged(object sender, EventArgs e) {
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            if (this.lupMaterialCheckList.EditValue != null && !string.IsNullOrEmpty(this.lupMaterialCheckList.EditValue.ToString())) {
                var data = this.lupMaterialCheckList.Properties.DataSource as DrugModel.MED_MATERIAL_CHECKDataTable;

                var row = data.FindByID(this.lupMaterialCheckList.EditValue.ToString());
                var dtMaster = new DrugModel.MED_MATERIAL_CHECKDataTable();
                dt = new DrugModel.MED_MATERIAL_CHECKDataTable();

                dt = objDrug.GetMedMaterialCheckList(Utility.CDate(lupMaterialCheckList.Text), row.CHECKER);
                //如果还用批号的话要改成0
                if (chkNullCount.Checked) {
                    dt.Where(i => i.CHECKTYPE == "1" && i.F_COUNT > 0).CopyToDataTable(dtMaster, LoadOption.PreserveChanges);
                }
                else {
                    dt.Where(i => i.CHECKTYPE == "1").CopyToDataTable(dtMaster, LoadOption.PreserveChanges);
                }

                if (dtMaster != null && dtMaster.Rows.Count > 0) {
                    gridMaterialInput.DataSource = dtMaster;
                }
                else {
                    gridMaterialInput.DataSource = null;
                }
            }
        }
    }
}
