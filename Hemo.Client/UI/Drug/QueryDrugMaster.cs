/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：药品主档窗体
// 创建时间：2013-03-19
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
using Hemo.IService.Config;

namespace Hemo.Client.UI.Drug {
    public partial class QueryDrugMaster :HemoBaseFrm {

        #region 私有成员
        /// <summary>
        /// 药品主档表
        /// </summary>
        private DrugModel.MED_DRUG_MASTERDataTable _drugDataTable;
        private IConfig _configService = ServiceManager.Instance.ConfigService;

        /// <summary>
        /// 药品主档数据服务对象
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
        /// 选择的行数据透析号
        /// </summary>
        private string _drugCode = string.Empty;
        public string DrugCode {
            get {
                return _drugCode;
            }
            set {
                _drugCode = value;
            }
        }
        #endregion

        #region 初始化方法
        public QueryDrugMaster() {
            InitializeComponent();
            var date = this._configService.GetConfigList(string.Empty, string.Empty, "药品分类", "1");
            var dater = this._configService.GetConfigList(string.Empty, string.Empty, "托管药品分类", "1");
            date.LoadDataRow(dater.Rows[0].ItemArray, true);
            this.repositoryItemCustomGridLookUpEdit1.DataSource = date;
            loadDrugMasterList();
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
            DrugCode = dr["DRUG_CODE"].ToString();
            IsAdd = false;
            showEditForm(IsAdd, DrugCode);
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
                MessageBox.Show(ex.Message, "药品主档");
            }
        }

        private void showEditForm(bool pIsAdd, string pDrugCode) {
            //EditDrugMaster frm = new EditDrugMaster();
            EditDrugMaster frm = new EditDrugMaster(pIsAdd, pDrugCode);
           // frm.Owner = this;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.Yes) {
                loadDrugMasterList();
            }
        }
        #endregion

        #region 数据方法
        /// <summary>
        /// 加载药品主档数据
        /// </summary>
        private void loadDrugMasterList() {
            _drugDataTable = objDrug.GetDrugMasterList();
            gridDrugMaster.DataSource = _drugDataTable;

        }

        /// <summary>
        /// 使用DataTable收集查询条件并返回数据集
        /// </summary>
        private void loadDrugMasterListByParas() {
            if (txtDRUG_CODE.Text.Length == 0 && txtDRUG_NAME.Text.Length == 0 && txtFIRM_ID.Text.Length == 0) {
                loadDrugMasterList();
            }
            else {
                DrugModel.MED_DRUG_MASTERDataTable tmpTable = new DrugModel.MED_DRUG_MASTERDataTable();
                DataTable dt = BaseControlInfo.GetDataTableByPanel(tmpTable, pnlControls, true);
                if (dt != null && dt.Rows.Count > 0) {
                    _drugDataTable = objDrug.GetDrugMasterListByParams((DrugModel.MED_DRUG_MASTERDataTable)dt);
                    gridDrugMaster.DataSource = _drugDataTable;
                }
            }
        }
        #endregion
    }
}