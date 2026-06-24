/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：主要附属设备录入户控件类
// 创建时间：2016-07-02
// 创建者：贺建操
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
using Hemo.Model;
using Hemo.IService.Machine;
using Hemo.Service;

namespace Hemo.Client.UI.Machine
{
    public partial class EditAccessoryEquip : PureTextEditForm
    {
        #region 字段
        /// <summary>
        /// 所属主机
        /// </summary>
        private MachineModel.MED_MACHINE_MAINFRAMERow _mainFrame;

        /// <summary>
        /// 当前附属设备表
        /// </summary>
        private MachineModel.MED_MACHINE_ACCESSORYEQUIPDataTable _currentTable;

        /// <summary>
        /// 当前附属设备
        /// </summary>
        private MachineModel.MED_MACHINE_ACCESSORYEQUIPRow _currentRow;

        /// <summary>
        /// 服务
        /// </summary>
        private IMachine _machineService = ServiceManager.Instance.MachineService;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mainFrame"></param>
        /// <param name="currentTable"></param>
        /// <param name="currentRow"></param>
        public EditAccessoryEquip(MachineModel.MED_MACHINE_MAINFRAMERow mainFrame, MachineModel.MED_MACHINE_ACCESSORYEQUIPDataTable currentTable, MachineModel.MED_MACHINE_ACCESSORYEQUIPRow currentRow)
        {
            InitializeComponent();

            this._mainFrame = mainFrame;
            this._currentTable = currentTable;
            this._currentRow = currentRow;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditAccessoryEquip_Load(object sender, EventArgs e)
        {
            if (this._mainFrame != null)
            {
                this.lbl_MainFrame.Text = this._mainFrame["CHINESENAME"] != DBNull.Value ? this._mainFrame.CHINESENAME : string.Empty;
            }

            this.FillUiDataByDataRow(this._currentRow);
        }

        /// <summary>
        /// 点击保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (this._mainFrame == null)
            {
                XtraMessageBox.Show("请确定该附属设备所属主机！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this._currentTable == null)
            {
                this._currentTable = new MachineModel.MED_MACHINE_ACCESSORYEQUIPDataTable();
            }

            if (!this.CheckInputData())
            {
                return;
            }

            if (this._currentRow == null)
            {
                //新增
                this._currentRow = this._currentTable.NewMED_MACHINE_ACCESSORYEQUIPRow();
                this.FillDataRowByUi(this._currentRow);
                this._currentRow.ID = Guid.NewGuid().ToString();
                this._currentRow.MAINFRAMEID = this._mainFrame["ID"] != DBNull.Value ? this._mainFrame.ID : string.Empty;
                this._currentTable.AddMED_MACHINE_ACCESSORYEQUIPRow(this._currentRow);
            }
            else
            { 
                //修改
                this.FillDataRowByUi(this._currentRow);
                this._currentRow.MAINFRAMEID = this._mainFrame["ID"] != DBNull.Value ? this._mainFrame.ID : string.Empty;
            }

            try
            {
                this._currentRow.ISDELETE = "0";
                this._machineService.SaveAccessoryEquipData(this._currentTable);
            }
            catch (Exception ex)
            {
                //日志
                XtraMessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// 点击取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 验证输入的数据合法性
        /// </summary>
        /// <returns></returns>
        private bool CheckInputData()
        {
            decimal d;
            if (!string.IsNullOrEmpty(this.txt_Count.Text.Trim()) && !decimal.TryParse(this.txt_Count.Text.Trim(), out d))
            {
                XtraMessageBox.Show("'数量'请填入正确的数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
        #endregion
    }
}