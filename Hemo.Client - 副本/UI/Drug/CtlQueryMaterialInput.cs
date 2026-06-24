/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
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
using Hemo.Client.Print;
using Hemo.IService.Config;
using Hemo.Client.UI.Hemodialysis;
using DevExpress.XtraReports.UI;


namespace Hemo.Client.UI.Drug {
    public partial class CtlQueryMaterialInput : DevExpress.XtraEditors.XtraUserControl
    {
        #region 类变量

        private IMaterial objMaterial = ServiceManager.Instance.MaterialService;
        private DrugModel.MED_MATERIAL_INPUTDataTable dt = null;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _materialTypes;
        private DrugModel.MED_MATERIAL_INPUTDataTable dtDetail = null;
        private IConfig _configService = ServiceManager.Instance.ConfigService;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public CtlQueryMaterialInput()
        {
            InitializeComponent();
            var dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day).Date;  //本月月初 

            DateTime endMonth = startMonth.AddMonths(1).AddDays(-1).Date;  //本月月末 //DateTime endMonth = startMonth.AddDays((dt.AddMonths(1) - dt).Days - 1);  //本月月末

            this.deBeginTime.EditValue = startMonth;
            this.deEndTime.EditValue = endMonth;

            loadMaterialInputList();

            this.Text = "入库记录";
            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountUI(this);

        }

        #endregion

        #region 事件

        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            this.gridView1.OptionsView.ShowAutoFilterRow = this.chkFilter.Checked;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (EditMaterialInput frm = new EditMaterialInput())
            {
                frm.CurrentData = null;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    loadMaterialInputList();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var dr = gridView2.GetFocusedDataRow() as DrugModel.MED_MATERIAL_INPUTRow;
            using (EditMaterialInput frm = new EditMaterialInput())
            {
                frm.CurrentData = dr;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    loadMaterialInputList();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            loadMaterialInputList();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            MaterialInputReport report = new MaterialInputReport(Convert.ToDateTime(this.deBeginTime.EditValue), dtDetail);
            ReportPrintTool pt = new ReportPrintTool(report);
            pt.ShowPreviewDialog();
        }

        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            var rowCurrent = this.gridView1.GetFocusedDataRow() as DrugModel.MED_MATERIAL_INPUTRow;

            if (rowCurrent == null)
                return;

            //打开患者录入界面
            if (e.Button == MouseButtons.Left && !this.gridView1.IsGroupRow(e.RowHandle))
            {
                var dtDetailFilter = new DrugModel.MED_MATERIAL_INPUTDataTable();
                dtDetail.Where(i => i.CODE == rowCurrent.CODE && i.UNITS == rowCurrent.UNITS).CopyToDataTable(dtDetailFilter, LoadOption.PreserveChanges);

                if (dtDetailFilter != null && dtDetailFilter.Rows.Count > 0)
                    this.gridControl1.DataSource = dtDetailFilter;
                else
                    this.gridControl1.DataSource = null;
                #region 双击去获取明细数据。
                if (e.Clicks == 2)
                {
                    this.xtraTabControl1.SelectedTabPage = this.xtraTabPage2;
                }
                #endregion
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var dr = gridView2.GetFocusedDataRow() as DrugModel.MED_MATERIAL_INPUTRow;
            if (dr != null)
            {
                if (XtraMessageBox.Show("是否确认删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var dtOut = objMaterial.GetMedMaterialOutputDetailByCodeAndBatchNum(Convert.ToDateTime(DateTime.Now.ToString()), dr.CODE, dr.BATCH_NUMBER);
                    if (dtOut != null && dtOut.Rows.Count > 0)
                    {
                        XtraMessageBox.Show("此耗材已存在出库记录不可以直接删除.\r\n 您可以对它进行出库已保证账面平衡.", "提示", MessageBoxButtons.OK);

                        return;

                    }
                    if (objMaterial.DeleteMaterialInPut(dr.ID.ToString()) > 0)
                    {
                        DateTime dtStar = Convert.ToDateTime(this.deBeginTime.EditValue).Date;
                        DateTime dtEnd = Convert.ToDateTime(this.deEndTime.EditValue).Date;

                        dt = objMaterial.GetMedMaterialInputMaster(dtStar, dtEnd);
                        dtDetail = objMaterial.GetMedMaterialInputDetail(dtStar, dtEnd);
                        var dtDetailFilter = new DrugModel.MED_MATERIAL_INPUTDataTable();
                        if (this.lupMaterialType.EditValue == null || string.IsNullOrEmpty(this.lupMaterialType.EditValue.ToString()))
                            dtDetail.Where(i => i.CODE == dr.CODE && i.SPACE == dr.SPACE && i.UNITS == dr.UNITS).CopyToDataTable(dtDetailFilter, LoadOption.PreserveChanges);
                        else
                            dtDetail.Where(i => i.CODE == dr.CODE && i.SPACE == dr.SPACE && i.UNITS == dr.UNITS && i.MODETYPE == this.lupMaterialType.EditValue.ToString()).CopyToDataTable(dtDetailFilter, LoadOption.PreserveChanges);



                        gridMaterialInput.DataSource = dt;

                        if (dtDetailFilter != null && dtDetailFilter.Rows.Count > 0)
                            this.gridControl1.DataSource = dtDetailFilter;
                        else
                            this.gridControl1.DataSource = null;
                        this.xtraTabControl1.SelectedTabPage = this.xtraTabPage2;
                        XtraMessageBox.Show("删除成功!");
                    }
                    else
                    { XtraMessageBox.Show("删除失败!"); }
                }
            }
            else
            {
                XtraMessageBox.Show("未选择删除数据");
            }
        }

        private void dxSimpleButton1_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 使用DataTable收集查询条件并返回数据集
        /// </summary>
        private void loadMaterialInputList()
        {
            busyIndicator1.Visible = true;
            busyIndicator1.ShowLoadingScreenFor(gridMaterialInput);
            var dtTemp = new DrugModel.MED_MATERIAL_INPUTDataTable();
            var dtDetailTemp = new DrugModel.MED_MATERIAL_INPUTDataTable();
            dt = new DrugModel.MED_MATERIAL_INPUTDataTable();
            dtDetail = new DrugModel.MED_MATERIAL_INPUTDataTable();
            DateTime dtStar = Convert.ToDateTime(this.deBeginTime.EditValue).Date;
            DateTime dtEnd = Convert.ToDateTime(this.deEndTime.EditValue).Date;
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    this._materialTypes = this._configService.GetConfigList(string.Empty, string.Empty, "辅材类型", "1");

                    dtTemp = objMaterial.GetMedMaterialInputMaster(dtStar, dtEnd);
                    dtDetailTemp = objMaterial.GetMedMaterialInputDetail(dtStar, dtEnd);
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.lupMaterialType.Properties.DataSource = this._materialTypes;
                    if (lupMaterialType.EditValue != null && !string.IsNullOrEmpty(lupMaterialType.EditValue.ToString()))
                    {
                        dtTemp.Where(i => i.MODETYPE.Trim() == this.lupMaterialType.EditValue.ToString().Trim()).CopyToDataTable<DrugModel.MED_MATERIAL_INPUTRow>(dt, LoadOption.PreserveChanges);
                        dtDetailTemp.Where(i => i.MODETYPE.Trim() == this.lupMaterialType.EditValue.ToString().Trim()).CopyToDataTable<DrugModel.MED_MATERIAL_INPUTRow>(dtDetail, LoadOption.PreserveChanges);
                    }
                    else
                    {
                        dtTemp.CopyToDataTable<DrugModel.MED_MATERIAL_INPUTRow>(dt, LoadOption.PreserveChanges);
                        dtDetailTemp.CopyToDataTable<DrugModel.MED_MATERIAL_INPUTRow>(dtDetail, LoadOption.PreserveChanges);

                    }


                    gridControl1.DataSource = null;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        gridMaterialInput.DataSource = dt;
                    }
                    else
                    {
                        gridMaterialInput.DataSource = null;
                    }
                    this.busyIndicator1.HideLoadingScreen();
                };
                worker.RunWorkerAsync();
            }
        }

        #endregion
    }
}
