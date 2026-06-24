/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：医护人员维护用户控件
// 创建时间：2014-04-13
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Hemo.IService.Dict;
using Hemo.Model;
using Hemo.Service;
using System.Data;

namespace Hemo.Client.UI.Dict {
    public partial class CtlStaffDictList : DevExpress.XtraEditors.XtraUserControl {
        #region 变量

        private DictModel.MED_STAFF_DICTDataTable _staffDictDataTable;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private DataTable dt = new DataTable();
        #endregion

        #region 构造函数

        string strType = string.Empty;
        public CtlStaffDictList(string pType) {
            this.InitializeComponent();
            this.BindGrid(pType);
            strType = pType;
        }

        #endregion

        #region 方法

        private void BindGrid(string pType) {
            this._staffDictDataTable = this._staffDictService.GetAllStaffDictList();
            dt = _staffDictDataTable;
            dt = Utilities.Utility.GetSubTable(dt, "ZYNAME='" + pType + "'");
            this.gcStaffDictList.DataSource = dt;
        }

        #endregion

        #region 事件

        private void StaffDictList_Load(object sender, EventArgs e) {
            //  this.BindGrid();
        }

        private void gvStaffDictList_RowClick(object sender, RowClickEventArgs e) {
            this.btnEdit.Enabled = this.gvStaffDictList.GetFocusedDataRow() != null;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e) {
            this.BindGrid(strType);
        }

        /// <summary>
        /// 操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpt_Click(object sender, EventArgs e) {
            bool isAdd = string.Compare(((SimpleButton)sender).Name, "btnAdd", true) == 0;
            _staffDictDataTable = (DictModel.MED_STAFF_DICTDataTable)dt;
            EditStaffDict frm = new EditStaffDict(this._staffDictDataTable, isAdd ? null : this.gvStaffDictList.GetFocusedDataRow() as DictModel.MED_STAFF_DICTRow, strType);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Yes)
                this.BindGrid(strType);
        }

        private void chkFilter_CheckedChanged(object sender, EventArgs e) {
            this.gvStaffDictList.OptionsView.ShowAutoFilterRow = this.chkFilter.Checked;
        }

        #endregion
    }
}

