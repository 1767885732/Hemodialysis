/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
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
using Hemo.Service;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.IService;

namespace Hemo.Client.UI.Drug {
    public partial class QueryDrugFirm :HemoBaseFrm {

        #region 私有成员
        /// <summary>
        /// 药厂信息表
        /// </summary>
        private DrugModel.MED_DRUG_FIRMDataTable _firmDataTable;
        /// <summary>
        /// 药厂信息数据服务对象
        /// </summary>
        //private DrugService objDrug = new DrugService();
        private IDrug objDrug = ServiceManager.Instance.DrugService;
        
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
        public QueryDrugFirm() {
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
            this.Close();
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
                MessageBox.Show(ex.Message, "药品厂商");
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
                DrugModel.MED_DRUG_MASTERDataTable tmpTable = new DrugModel.MED_DRUG_MASTERDataTable();
                DataTable dt = BaseControlInfo.GetDataTableByPanel(tmpTable, pnlControls, true);
                if (dt != null && dt.Rows.Count > 0) {
                    _firmDataTable = objDrug.GetDrugFirmListByParams((DrugModel.MED_DRUG_FIRMDataTable)dt);
                    gridDrugFirm.DataSource = _firmDataTable;
                }
            }
        }
        #endregion

    }
}