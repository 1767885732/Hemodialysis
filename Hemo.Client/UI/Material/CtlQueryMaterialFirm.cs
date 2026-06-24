/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：耗材厂商查询窗体
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
    public partial class CtlQueryMaterialFirm : DevExpress.XtraEditors.XtraUserControl {
       
         #region 私有成员
        /// <summary>
        /// 药厂信息表
        /// </summary>
        private DrugModel.MED_DRUG_FIRMDataTable _firmDataTable;
        /// <summary>
        /// 药厂信息数据服务对象
        /// </summary>
        // private DrugService objDrug = new DrugService();
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
        public CtlQueryMaterialFirm() {
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
          //  this.Close();
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
                MessageBox.Show(ex.Message, "耗材厂商");
            }
        }

        private void showEditForm(bool pIsAdd, string pFirmID) {
            EditMaterialFirm frm = new EditMaterialFirm(pIsAdd, pFirmID);
            //frm.Owner = this;
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

        #region 事件
        private void chkFilter_CheckedChanged(object sender, EventArgs e) {
            this.gridView1.OptionsView.ShowAutoFilterRow = this.chkFilter.Checked;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var dr = gridView1.GetFocusedDataRow() as DrugModel.MED_DRUG_FIRMRow;
            if (dr != null)
            {
                if (XtraMessageBox.Show("是否确认删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (objDrug.DeleteDrugFirmInfo(dr.FIRM_ID.ToString()) > 0)
                    {
                        loadDrugMasterListByParas();
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
        #endregion
    }
}
