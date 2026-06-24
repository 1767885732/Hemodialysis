/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：设备列表户控件类
// 创建时间：2016-02-28
// 创建者：贺建操
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
using System.Data;
using Hemo.Service;
using Hemo.Client.Core;
using Medicalsystem.Auth.Client;
using Hemo.Utilities;

namespace Hemo.Client.UI.Machine
{
    public partial class CtlMachineList : XtraUserControl
    {
        #region 变量

        private MachineModel.MED_DIALYSIS_MACHINEDataTable _machineDataTable;

        private IMachine _machineService = ServiceManager.Instance.MachineService;

        #endregion

        #region 构造函数

        public CtlMachineList()
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
            validDAuth();

            BindGrid();
        }
        private void validDAuth()
        {
            if (!string.IsNullOrEmpty(HemoApplicationContext.Current.IsPassDogValid))
            {
                this.lbTitle.Visible = true;
                this.btnAdd.Enabled = false;
                this.btnSet.Enabled = false;
                this.lbTitle.Text = HemoApplicationContext.Current.IsPassDogValid;
            }
            else
            {
                this.lbTitle.Visible = false;

                this.btnAdd.Enabled = true;
                this.btnSet.Enabled = true;
            }
        }
        /// <summary>
        /// 列表行点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMachine_RowClick(object sender, RowClickEventArgs e)
        {
            this.btnEdit.Enabled = this.btnSet.Enabled = this.gvMachine.GetFocusedDataRow() != null;
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
            int index = this.gvMachine.GetFocusedDataSourceRowIndex();

            EditMachine frm = new EditMachine(_machineDataTable, isAdd ? null : this.gvMachine.GetFocusedDataRow() as MachineModel.MED_DIALYSIS_MACHINERow);
            frm.IsMachine = true;
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Yes)
            {
                BindGrid();
                if (!isAdd) { this.gvMachine.FocusedRowHandle = index; }
            }
        }

        /// <summary>
        /// 床位、区域设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSet_Click(object sender, EventArgs e)
        {
            var row = this.gvMachine.GetFocusedDataRow() as MachineModel.MED_DIALYSIS_MACHINERow;
            if (row.IsAREA_IDNull() && row.IsBED_IDNull())
            {
                //var _machineDataTable = _machineService.GetNewMachineList();
                var data = new MachineModel.MED_DIALYSIS_MACHINEDataTable();
                _machineDataTable.Where(i => !string.IsNullOrEmpty(i.AREA_ID) && !string.IsNullOrEmpty(i.BED_ID)).CopyToDataTable<MachineModel.MED_DIALYSIS_MACHINERow>(data, LoadOption.PreserveChanges);
                if (data != null && data.Rows.Count > 0)
                {
                    int bedCount = data.Rows.Count + 1;
                    int dogCount = Utilities.Utility.GetHospitalBedCount();
                    if (bedCount > dogCount)
                    {
                        if (DAuthContext.Current.HospitalID != 1)
                        {
                            AutoClosedMsgBox.ShowForm(string.Format(Utilities.Utility.dogTipStr1, dogCount), "系统提示", 2000, MessageBoxIcon.Stop);
                        }
                        else
                        {
                            AutoClosedMsgBox.ShowForm(Utilities.Utility.dogTipStr, "系统提示", 2000, MessageBoxIcon.Stop);
                        }
                        return;
                    }
                }
            }
            AreaAndBedSet frm = new AreaAndBedSet(_machineDataTable, row);
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
            this.gcMachine.DataSource = _machineDataTable = _machineService.GetNewMachineList();
            validDAuth();

        }

        #endregion
    }
}
