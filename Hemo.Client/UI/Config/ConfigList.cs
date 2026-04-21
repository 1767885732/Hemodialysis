/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：配置列表窗体
// 创建时间：2014-03-06
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
using Hemo.IService.Config;
using Hemo.Model;
using Hemo.Service;

namespace Hemo.Client.UI.Config {
    public partial class ConfigList :HemoBaseFrm{
        #region 变量

        private string _itemType;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _configDataTable;
        private IConfig _configService = ServiceManager.Instance.ConfigService;

        #endregion

        #region 构造函数

        public ConfigList(string itemType) {
            this.InitializeComponent();

            this.Text = this._itemType = itemType;
        }

        #endregion

        #region 方法

        private void InitializeControls() {
            this.cbxSTATUS.Properties.Items.Insert(0, "请选择");

            this.cbxSTATUS.SelectedIndex = 0;

            this.BindGrid();
        }

        private void BindGrid() {
            this.gcConfig.DataSource = this._configDataTable = this._configService.GetConfigList(this.txtITEM_VALUE.Text, this.txtITEM_NAME.Text, this._itemType, this.cbxSTATUS.SelectedIndex > 0 ? (this.cbxSTATUS.SelectedIndex - 1).ToString() : string.Empty);
        }

        #endregion

        #region 事件

        private void ConfigList_Load(object sender, EventArgs e) {
            this.InitializeControls();
        }

        private void gvConfig_RowClick(object sender, RowClickEventArgs e) {
            this.btnEdit.Enabled = this.gvConfig.GetFocusedDataRow() != null;
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

            EditConfig frm = new EditConfig(this._itemType, this._configDataTable, isAdd ? null : this.gvConfig.GetFocusedDataRow() as ConfigModel.MED_COMMON_ITEMLISTRow);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Yes)
                this.BindGrid();
        }
        #endregion
    }
}
