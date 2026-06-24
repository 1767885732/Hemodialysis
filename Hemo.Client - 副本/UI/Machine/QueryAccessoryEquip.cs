/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：主要附属设备列表维护窗体
// 创建时间：2016-04-13
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
    public partial class QueryAccessoryEquip :HemoBaseFrm
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
        public QueryAccessoryEquip(MachineModel.MED_MACHINE_MAINFRAMERow mainFrame)
        {
            InitializeComponent();

            this._mainFrame = mainFrame;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryAccessoryEquip_Load(object sender, EventArgs e)
        {
            if (this._mainFrame != null)
            {
                this.lbl_MainFrame.Text = this._mainFrame["CHINESENAME"] != DBNull.Value ? this._mainFrame.CHINESENAME : string.Empty;
            }

            this.GetData();
        }

        /// <summary>
        /// 新增附属设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (this._mainFrame == null)
            {
                XtraMessageBox.Show("请确定附属设备所属主机！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var frm = new EditAccessoryEquip(this._mainFrame, this._currentTable, null);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.GetData();
            }
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 右键单击数据行显示右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gc_AccessoryEquip_MouseDown(object sender, MouseEventArgs e)
        {
            this._currentRow = this.gv_AccessoryEquip.GetFocusedDataRow() as MachineModel.MED_MACHINE_ACCESSORYEQUIPRow;
            if (this._currentRow == null)
            {
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(MousePosition);
            }
        }

        /// <summary>
        /// 双击数据行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_AccessoryEquip_DoubleClick(object sender, EventArgs e)
        {
            this._currentRow = this.gv_AccessoryEquip.GetFocusedDataRow() as MachineModel.MED_MACHINE_ACCESSORYEQUIPRow;
            if (this._currentRow == null)
            {
                return;
            }

            var frm = new EditAccessoryEquip(this._mainFrame, this._currentTable, this._currentRow);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.GetData();
            }
        }

        /// <summary>
        /// 点击删除菜单项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolDelete_Click(object sender, EventArgs e)
        {
            if (this._currentRow == null)
            {
                return;
            }

            if (XtraMessageBox.Show("是否确定删除当前行数据？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            this._currentRow.ISDELETE = "1";
            try
            {
                this._machineService.SaveAccessoryEquipData(this._currentTable);
            }
            catch (Exception ex)
            { 
                //日志
            }

            this.GetData();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取附属设备数据
        /// </summary>
        private void GetData()
        {
            if (this._mainFrame == null)
            {
                XtraMessageBox.Show("请确定附属设备所属主机！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                this._currentRow = null;
                this._currentTable = this._machineService.GetAccessoryEquipData(this._mainFrame.ID);
                this.gc_AccessoryEquip.DataSource = this._currentTable;
            }
            catch (Exception ex)
            {
                //日志
            }
        }
        #endregion
    }
}