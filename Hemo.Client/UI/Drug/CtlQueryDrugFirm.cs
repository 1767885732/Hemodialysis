/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：药厂查询窗体
// 创建时间：2013-03-21
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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Hemo.Service;
using Hemo.Model;
using Hemo.Utilities;

namespace Hemo.Client.UI.Drug {
    public partial class CtlQueryDrugFirm : DevExpress.XtraEditors.XtraUserControl {

        #region 私有成员
        /// <summary>
        /// 药厂信息表
        /// </summary>
        private DrugModel.MED_DRUG_FIRMDataTable _firmDataTable;
        /// <summary>
        /// 药厂信息数据服务对象
        /// </summary>
        private DrugService objDrug = new DrugService();
        #endregion

        #region  共有成员
        /// <summary>
        /// 是否为新增
        /// </summary>
        private bool _isAdd = true;
        public bool IsAdd {
            get {
                return _isAdd;
            }
            set {
                _isAdd = value;
            }
        }

        /// <summary>
        /// 选择的行数据药厂编号
        /// </summary>
        private string _firmID = string.Empty;
        public string FirmID {
            get {
                return _firmID;
            }
            set {
                _firmID = value;
            }
        }
        #endregion

        #region 初始化方法
        public CtlQueryDrugFirm() {
            InitializeComponent();
            loadDrugMasterListByParas();
        }
        #endregion

        #region 各种事件
        private void btnQuery_Click(object sender, EventArgs e) {
            loadDrugMasterListByParas();
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            IsAdd = true;
            showEditForm(IsAdd, string.Empty);
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            // this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e) {
            DataRow dr = gridView1.GetFocusedDataRow();
            FirmID = dr["FIRM_ID"].ToString();
            IsAdd = false;
            showEditForm(IsAdd, FirmID);
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e) {
            try {
                DataRow dr = gridView1.GetFocusedDataRow();
                if (dr != null) {
                    btnEdit.Enabled = true;
                }
                else {
                    btnEdit.Enabled = false;
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "透析记录单");
            }
        }

        private void showEditForm(bool pIsAdd, string pFirmID) {
            EditDrugFirm frm = new EditDrugFirm(pIsAdd, pFirmID);
            // frm.Owner = this;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.Yes) {
                loadDrugMasterListByParas();
            }
        }

        //private void chkFilter_CheckedChanged(object sender, EventArgs e) {
        //    this.gridView1.OptionsView.ShowAutoFilterRow = this.chkFilter.Checked;
        //}

        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            this.gridView1.OptionsView.ShowAutoFilterRow = this.chkFilter.Checked;
        }
        #endregion

        #region 数据方法
        /// <summary>
        /// 使用DataTable收集查询条件并返回数据集
        /// </summary>
        private void loadDrugMasterListByParas() {
            if (txtFIRM_NAME.Text.Length == 0 && txtFIRM_ID.Text.Length == 0 && txtFIRM_ADDRESS.Text.Length == 0 && txtTELEPHONE.Text.Length == 0 && txtMOBILE_PHONE.Text.Length == 0 && txtCONTECT_PEOPLE.Text.Length == 0) {
                _firmDataTable = objDrug.GetDrugFirmListByFirmType(txtFIRM_TYPE.Text);
                gridDrugFirm.DataSource = _firmDataTable;
            }
            else {
                DrugModel.MED_DRUG_FIRMDataTable tmpTable = new DrugModel.MED_DRUG_FIRMDataTable();
                DataRow dr = tmpTable.NewRow();
                
                dr["FIRM_NAME"] = txtFIRM_NAME.Text.Trim();
                dr["FIRM_ID"] = txtFIRM_ID.Text.Trim();
                dr["FIRM_ADDRESS"] = txtFIRM_ADDRESS.Text.Trim();
                dr["CONTECT_PEOPLE"] = txtCONTECT_PEOPLE.Text.Trim();
                dr["TELEPHONE"] = txtTELEPHONE.Text.Trim();
                dr["MOBILE_PHONE"] = txtMOBILE_PHONE.Text.Trim();
                dr["FIRM_TYPE"] = txtFIRM_TYPE.Text.Trim();

                tmpTable.Rows.Add(dr);
                // DataTable dt = BaseControlInfo.GetDataTableByPanel(tmpTable, pnlControls, true);
                if (tmpTable != null && tmpTable.Rows.Count > 0) {
                    _firmDataTable = objDrug.GetDrugFirmListByParams(tmpTable);
                    gridDrugFirm.DataSource = _firmDataTable;
                }
            }
        }
        #endregion
    }
}
