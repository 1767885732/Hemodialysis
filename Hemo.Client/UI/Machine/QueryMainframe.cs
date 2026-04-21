/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：医疗设备主机登记用户控件类
// 创建时间：2015-03-15
// 创建者：刘超
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
using Hemo.Service;
using Hemo.Utilities;
using Hemo.Client.Print;
using Hemo.IService.Dict;
using Hemo.IService.Machine;
using Hemo.Client.Controls;
using Hemo.Client.Doc;

namespace Hemo.Client.UI.Machine 
{
    public partial class QueryMainframe : ViewBase
    {
        #region 字段
        /// <summary>
        /// 当前主机表
        /// </summary>
        private MachineModel.MED_MACHINE_MAINFRAMEDataTable _currentMainFrameData;

        /// <summary>
        /// 当前主机
        /// </summary>
        private MachineModel.MED_MACHINE_MAINFRAMERow _currentMainFrame;

        /// <summary>
        /// 服务
        /// </summary>
        private IMachine _machineService = ServiceManager.Instance.MachineService;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryMainframe()
        {
            InitializeComponent();

            this.repositoryItemButtonEdit1.ButtonClick+=new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit1_ButtonClick);
        }
        #endregion

        #region 事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryMainframe_Load(object sender, EventArgs e)
        {
            this.GetData();
        }
        
        /// <summary>
        /// 数据表鼠标按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gc_Mainframe_MouseDown(object sender, MouseEventArgs e)
        {
            this._currentMainFrame = this.gvMainframe.GetFocusedDataRow() as MachineModel.MED_MACHINE_MAINFRAMERow;
            if (this._currentMainFrame == null)
            {
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(MousePosition);
            }
        }

        /// <summary>
        /// 数据行单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMainframe_DoubleClick(object sender, EventArgs e)
        {
            this._currentMainFrame = this.gvMainframe.GetFocusedDataRow() as MachineModel.MED_MACHINE_MAINFRAMERow;
            if (this._currentMainFrame == null)
            {
                return;
            }

            var frm = new EditMainframe(this._currentMainFrameData, this._currentMainFrame);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.GetData();
            }
        }

        /// <summary>
        /// 新增设备按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Entering_Click(object sender, EventArgs e)
        {
            var frm = new EditMainframe(null, null);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.GetData();
            }
        }

        /// <summary>
        /// 打印按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Print_Click(object sender, EventArgs e)
        {
            this._currentMainFrame = this.gvMainframe.GetFocusedDataRow() as MachineModel.MED_MACHINE_MAINFRAMERow;
            if (this._currentMainFrame == null)
            {
                XtraMessageBox.Show("请选择要打印的设备！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var doc = new 医疗设备登记表();
            doc.CurrentMainFrame = this._currentMainFrame;

            var accessoryEquips = this._machineService.GetAccessoryEquipData(this._currentMainFrame.ID);
            doc.CurrentAccessoryEquips = accessoryEquips;

            var frm = new ShowPrintForm(doc);
            frm.ShowDialog();
        }

        /// <summary>
        /// 删除菜单项点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolDelete_Click(object sender, EventArgs e)
        {
            this._currentMainFrame = this.gvMainframe.GetFocusedDataRow() as MachineModel.MED_MACHINE_MAINFRAMERow;
            if (this._currentMainFrame == null)
            {
                return;
            }

            if (XtraMessageBox.Show("是否确定删除当前行数据？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            this._currentMainFrame.ISDELETE = "1";
            try
            {
                this._machineService.SaveMainframeData(this._currentMainFrameData);
            }
            catch (Exception ex)
            { 
                //日志
            }

            this.GetData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void item_AccessoryEquip_Click(object sender, EventArgs e)
        {
            this._currentMainFrame = this.gvMainframe.GetFocusedDataRow() as MachineModel.MED_MACHINE_MAINFRAMERow;
            var frm = new QueryAccessoryEquip(this._currentMainFrame);
            frm.ShowDialog();
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, EventArgs e)
        {
            this._currentMainFrame = this.gvMainframe.GetFocusedDataRow() as MachineModel.MED_MACHINE_MAINFRAMERow;
            var frm = new QueryAccessoryEquip(this._currentMainFrame);
            frm.ShowDialog();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            this._currentMainFrame = this.gvMainframe.GetFocusedDataRow() as MachineModel.MED_MACHINE_MAINFRAMERow;
            if (this._currentMainFrame == null)
            {
                return;
            }

            if (XtraMessageBox.Show("是否确定删除当前行数据？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            this._currentMainFrame.ISDELETE = "1";
            try
            {
                this._machineService.SaveMainframeData(this._currentMainFrameData);
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
            try
            {
                this._currentMainFrame = null;
                this._currentMainFrameData = this._machineService.GetMainframeData();
                this.gc_Mainframe.DataSource = this._currentMainFrameData;
            }
            catch (Exception ex)
            {
                //日志
            }
        }
        #endregion
    }
}