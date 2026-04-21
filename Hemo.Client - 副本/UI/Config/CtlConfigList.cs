/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：配置列表用户控件
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
using System.ComponentModel;

namespace Hemo.Client.UI.Config {
    public partial class CtlConfigList : XtraUserControl {
        #region 变量

        private string _itemType;

        private ConfigModel.MED_COMMON_ITEMLISTDataTable _configDataTable;

        private IConfig _configService = ServiceManager.Instance.ConfigService;

        #endregion

        #region 构造函数

        public CtlConfigList(string itemType) {
            InitializeComponent();
            this.Text = _itemType = itemType;
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CtlConfigList_Load(object sender, EventArgs e) {
            InitializeControls();
        }

        /// <summary>
        /// 是否显示过滤行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkFilter_CheckedChanged(object sender, EventArgs e) {
            this.gvConfig.OptionsView.ShowAutoFilterRow = this.chkFilter.Checked;
        }

        /// <summary>
        /// 列表行点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvConfig_RowClick(object sender, RowClickEventArgs e) {
            this.btnEdit.Enabled = this.gvConfig.GetFocusedDataRow() != null;
        }

        /// <summary>
        /// 操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpt_Click(object sender, EventArgs e) {
            bool isAdd = string.Compare(((SimpleButton)sender).Name, "btnAdd", true) == 0;

            EditConfig frm = new EditConfig(_itemType, _configDataTable, isAdd ? null : this.gvConfig.GetFocusedDataRow() as ConfigModel.MED_COMMON_ITEMLISTRow);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Yes)
                BindGrid();
        }

        private void gvConfig_DoubleClick(object sender, EventArgs e)
        {
            if (this.gvConfig.GetFocusedDataRow() != null)
            {
                EditConfig frm = new EditConfig(_itemType, _configDataTable, this.gvConfig.GetFocusedDataRow() as ConfigModel.MED_COMMON_ITEMLISTRow);
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.Yes)
                    BindGrid();
            }
        }

        #endregion

        #region 方法

        private void InitializeControls() {
            BindGrid();
        }

        /// <summary>
        /// 根据参数绑定列表数据
        /// </summary>
        private void BindGrid() {
            busyIndicator1.ShowLoadingScreenFor(gcConfig);
            using (BackgroundWorker worker = new BackgroundWorker()) {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    _configDataTable = _configService.GetConfigList(string.Empty, string.Empty, _itemType, string.Empty);
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.gcConfig.DataSource = _configDataTable;
                    this.busyIndicator1.HideLoadingScreen();
                };
                worker.RunWorkerAsync();
            }
        }

        #endregion
    }
}
