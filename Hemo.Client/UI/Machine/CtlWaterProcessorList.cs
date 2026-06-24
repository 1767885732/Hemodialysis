/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：水处理机列表户控件类
// 创建时间：2016-06-08
// 创建者：吕志强
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Hemo.IService.Machine;
using Hemo.Model;
using Hemo.Service;

namespace Hemo.Client.UI.Machine
{
    public partial class CtlWaterProcessorList : XtraUserControl
    {
        #region 变量

        private MachineModel.MED_DIALYSIS_MACHINEDataTable _machineDataTable;

        private IMachine _machineService = ServiceManager.Instance.MachineService;

        #endregion

        #region 构造函数

        public CtlWaterProcessorList()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MachineList_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 列表行点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMachine_RowClick(object sender, RowClickEventArgs e)
        {
            this.btnEdit.Enabled = this.gvMachine.GetFocusedDataRow() != null;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpt_Click(object sender, EventArgs e)
        {
            bool isAdd = string.Compare(((SimpleButton)sender).Name, "btnAdd", true) == 0;

            EditMachine frm = new EditMachine(_machineDataTable, isAdd ? null : this.gvMachine.GetFocusedDataRow() as MachineModel.MED_DIALYSIS_MACHINERow);
            frm.IsMachine = false;
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Yes)
                BindGrid();
        }

        /// <summary>
        /// 是否显示过滤行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            this.gvMachine.OptionsView.ShowAutoFilterRow = this.chkFilter.Checked;
        }

        #endregion

        #region 方法

        private void BindGrid()
        {
            this.gcMachine.DataSource = _machineDataTable = _machineService.GetWaterProcessorList();
        }

        #endregion
    }
}
