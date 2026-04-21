/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:修改对外公开的方法
 * 创建标识:贺建操-2016年12月16日
 * 
 * 修改时间:2017年5月3日
 * 修改人:刘超
 * 修改描述:增加窗体控件值的方法
 * 
 * 修改时间:2017年6月4日
 * 修改人:贺建操
 * 修改描述:修改对外公开的方法
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using Hemo.Client.Core;
using DevExpress.XtraTab;
using DevExpress.XtraEditors;
using Hemo.WinForm;
using Hemo.Client.UI.Machine;

namespace Hemo.Client.Modules
{
    public partial class EqumentInfoMgr : XtraUserControl
    {
        #region 构造函数
        public EqumentInfoMgr()
        {
            InitializeComponent();
            if (!IsViewInWorkspace("设备维修"))
            {
                ShowViewInWorkspace(new RepairRecordManager(), "设备维修");
            }
        }


        #endregion
        #region 方法
        /// <summary>
        /// 页面是否已经存在
        /// </summary>
        /// <param name="caption"></param>
        /// <returns></returns>
        private bool IsViewInWorkspace(string caption)
        {
            foreach (XtraTabPage tabPage in this.viewHostTabControl.TabPages)
            {
                if (tabPage.Text == caption)
                {
                    this.viewHostTabControl.SelectedTabPage = tabPage;
                    return true;
                }

            }
            return false;
        }
        /// <summary>
        /// 将子界面以TabPage的方式显示，如果子界面已存在，则激活此子界面
        /// </summary>
        /// <param name="view"></param>
        /// <param name="viewName"></param>
        private void ShowViewInWorkspace(ViewBase view, string caption)
        {
            XtraTabPage page = new XtraTabPage();
            page.Text = caption;

            view.Dock = DockStyle.Fill;
            page.Controls.Add(view);
            page.Padding = new Padding(2);

            this.viewHostTabControl.TabPages.Add(page);
            this.viewHostTabControl.SelectedTabPage = page;

            view.CloseButtonClicked += delegate
            {
                viewHostTabControl_CloseButtonClick(view, EventArgs.Empty);
            };
        }


        #endregion
        #region 事件

        /// <summary>
        /// viewHostTabControl_CloseButtonClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewHostTabControl_CloseButtonClick(object sender, EventArgs e)
        {

            if (this.viewHostTabControl.SelectedTabPage != null)
            {
                var current = this.viewHostTabControl.SelectedTabPage.Controls[0] as ViewBase;
                if (current != null)
                {
                    if (current.HasDirty)
                    {
                        if (XtraMessageBox.Show("当前界面含有未保存的数据,是否确定关闭?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;
                    }
                    var tabPage = this.viewHostTabControl.SelectedTabPage;
                    this.viewHostTabControl.TabPages.Remove(tabPage);
                    if (tabPage != null)
                        tabPage.Dispose();
                    current.Dispose();
                }
            }
        }
        /// <summary>
        /// viewHostTabControl_SelectedPageChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewHostTabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            //如果不是嵌入到医生站中,设置编辑器组是否可见

            if (e.Page == null)
                return;
            var view = e.Page.Controls[0] as ViewBase;
            if (view != null)
                view.OnTabPageViewSelectedHandler();
        }
        /// <summary>
        /// 设备维修
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("设备维修"))
            {
                ShowViewInWorkspace(new RepairRecordManager(), "设备维修");
            }
        }
        /// <summary>
        /// 内毒素检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("内毒素检测"))
            {
                ShowViewInWorkspace(new WaterHemoManager(), "内毒素检测");
            }
        }
        /// <summary>
        /// 配液桶消毒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("配液桶消毒"))
            {
                ShowViewInWorkspace(new MahineMixManager(), "配液桶消毒");
            }
        }
        /// <summary>
        /// 水处理消毒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("水处理消毒"))
            {
                ShowViewInWorkspace(new WaterTreatmentManager(), "水处理消毒");
            }
        }
        /// <summary>
        /// 设备保养
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("设备保养"))
            {
                ShowViewInWorkspace(new HosEquipmentManager(), "设备保养");
            }
        }
        /// <summary>
        /// 设备登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barManageIn_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("设备登记"))
            {
                ShowViewInWorkspace(new QueryMainframe(), "设备登记");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            // this.Close();
        }
        /// <summary>
        /// exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EqumentInfoManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (XtraMessageBox.Show("您确定退出当前系统吗？", "提示", MessageBoxButtons.OKCancel) !=
            System.Windows.Forms.DialogResult.OK)
            {

                e.Cancel = true;
            }
            else
            {
                Program.HideClose = true;
            }
        }
        /// <summary>
        /// barBtn_ShiftRole_ItemClick
        /// </summary>
        /// <param name="senderr"></param>
        /// <param name="ee"></param>
        private void barBtn_ShiftRole_ItemClick(object senderr, ItemClickEventArgs ee)
        {
            LoginScreen frm = new LoginScreen();
            frm.ShiftRoles = HemoApplicationContext.Current.RolesOffices;
            frm.LoginEvent += delegate(object sender, LoginEventArgs e)
            {
                frm.Dispose();
                this.Dispose();
                Program.Show(e.RunApp, e.RunAppNames);

            };
            frm.ShowDialog();
        }
        /// <summary>
        /// 水处理机运行记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("水处理机运行记录"))
            {
                ShowViewInWorkspace(new WaterProcessorUseRecord(), "水处理机运行记录");
            }
        }
        /// <summary>
        /// 血透机运行记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("血透机运行记录"))
            {
                ShowViewInWorkspace(new MachineUseRecordNew(), "血透机运行记录");
            }
        }
        /// <summary>
        /// 设备信息管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEquipmentMgr_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("设备信息管理"))
            {
                ShowViewInWorkspace(new ctlEquipmentMgr(), "设备信息管理");
            }
        }
        /// <summary>
        /// 水处理机管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barWaterEquipmentMgr_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("水处理机管理"))
            {
                ShowViewInWorkspace(new ctlWaterEquipmentMgr(), "水处理机管理");
            }
        }
        /// <summary>
        /// 集中液管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barTogetherMgr_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("集中液管理"))
            {
                ShowViewInWorkspace(new ctlTogetherMgr(), "集中液管理");
            }
        }

        #endregion
    }
}