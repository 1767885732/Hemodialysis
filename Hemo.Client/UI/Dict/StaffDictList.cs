/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：医生资料设定窗体
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

namespace Hemo.Client.UI.Dict {
    public partial class StaffDictList :HemoBaseFrm{
        #region 变量

        private DictModel.MED_STAFF_DICTDataTable _staffDictDataTable;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;

        #endregion

        #region 构造函数

        public StaffDictList() {
            this.InitializeComponent();
        }

        #endregion

        #region 方法

        private void BindGrid() {
            this.gcStaffDictList.DataSource = this._staffDictDataTable = this._staffDictService.GetStaffDictList();
        }

        #endregion

        #region 事件

        private void StaffDictList_Load(object sender, EventArgs e) {
            this.BindGrid();
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
            this.BindGrid();
        }

        /// <summary>
        /// 操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpt_Click(object sender, EventArgs e) {
            bool isAdd = string.Compare(((SimpleButton)sender).Name, "btnAdd", true) == 0;

            EditStaffDict frm = new EditStaffDict(this._staffDictDataTable, isAdd ? null : this.gvStaffDictList.GetFocusedDataRow() as DictModel.MED_STAFF_DICTRow, "");
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Yes)
                this.BindGrid();
        }

        #endregion
    }
}
