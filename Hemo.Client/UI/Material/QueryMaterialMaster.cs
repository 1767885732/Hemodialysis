/*----------------------------------------------------------------
// Copyright (C) 2013 苏州麦迪斯顿医疗科技有限公司
// 描述：耗材资料查询列表
// 创建时间：2013-03-22
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

namespace Hemo.Client.UI.Material {
    public partial class QueryMaterialMaster : DevExpress.XtraEditors.XtraForm {
        #region 私有成员
        /// <summary>
        /// 耗材资料表
        /// </summary>
        private MaterialModel.MED_MATERIAL_MASTERDataTable _materialDataTable;
        /// <summary>
        /// 耗材资料数据服务对象
        /// </summary>
        private IMaterial objMaterial = ServiceManager.Instance.MaterialService;
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
        /// 选择的行数据耗材编号
        /// </summary>
        private string _materialID = string.Empty;
        public string MaterialID {
            get {
                return _materialID;
            }
            set {
                _materialID = value;
            }
        }
        #endregion

        #region 初始化方法
        public QueryMaterialMaster() {
            InitializeComponent();
            loadMaterialMasterList();
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
            MaterialID = dr["MATERIAL_ID"].ToString();
            IsAdd = false;
            showEditForm(_isAdd, MaterialID); ;
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

        private void showEditForm(bool pIsAdd, string pMaterialID) {
            EditMaterialMaster frm = new EditMaterialMaster(pIsAdd, pMaterialID);
            //frm.Owner = this;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.Yes) {
                loadMaterialMasterList();
            }
        }
        #endregion

        #region 数据方法
        /// <summary>
        /// 加载耗材数据列表
        /// </summary>
        private void loadMaterialMasterList() {
            _materialDataTable = objMaterial.GetMaterialMasterList();
            gridMaterialMaster.DataSource = _materialDataTable;
        }

        /// <summary>
        /// 使用DataTable收集查询条件并返回数据集
        /// </summary>
        private void loadDrugMasterListByParas() {
            if (txtMATERIAL_NAME.Text.Length == 0 && txtMATERIAL_ID.Text.Length == 0 && txtFIRM_NAME.Text.Length == 0) {
                loadMaterialMasterList();
            }
            else {
                DrugModel.MED_DRUG_MASTERDataTable tmpTable = new DrugModel.MED_DRUG_MASTERDataTable();
                DataTable dt = BaseControlInfo.GetDataTableByPanel(tmpTable, pnlControls, true);
                if (dt != null && dt.Rows.Count > 0) {
                    _materialDataTable = objMaterial.GetMaterialMasterListByParams((MaterialModel.MED_MATERIAL_MASTERDataTable)dt);
                    gridMaterialMaster.DataSource = _materialDataTable;
                }
            }
        }
        #endregion

    }
}