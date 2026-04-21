/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：设备信息管理主窗体
// 创建时间：2015-05-21
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
using DevExpress.XtraBars;
using Hemo.Client.Core;
using DevExpress.XtraTab;
using DevExpress.XtraEditors;
using Hemo.WinForm;

namespace Hemo.Client.UI.Machine
{
    public partial class EqumentInfoManager : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region 类变量

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public EqumentInfoManager()
        {
            InitializeComponent();
            this.barBtn_User.Caption = string.IsNullOrEmpty(HemoApplicationContext.Current.CurrentUser.USER_NAME) ? "用户" : HemoApplicationContext.Current.CurrentUser.USER_NAME;
            this.barBtn_Date.Caption = DateTime.Today.ToString("yyyy年MM月dd日");
            this.barBtn_IP.Caption = HemoApplicationContext.Current.IpAddress;
            this.barBtn_Version.Caption = HemoApplicationContext.Current.versionAddress;

        }

        #endregion

        #region 事件

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

        private void viewHostTabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            //如果不是嵌入到医生站中,设置编辑器组是否可见

            if (e.Page == null)
                return;
            var view = e.Page.Controls[0] as ViewBase;
            if (view != null)
                view.OnTabPageViewSelectedHandler();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("设备维修"))
            {
                ShowViewInWorkspace(new RepairRecordManager(), "设备维修");
            }
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("内毒素检测"))
            {
                ShowViewInWorkspace(new WaterHemoManager(), "内毒素检测");
            }
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("配液桶消毒"))
            {
                ShowViewInWorkspace(new MahineMixManager(), "配液桶消毒");
            }
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("水处理消毒"))
            {
                ShowViewInWorkspace(new WaterTreatmentManager(), "水处理消毒");
            }
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("设备保养"))
            {
                ShowViewInWorkspace(new HosEquipmentManager(), "设备保养");
            }
        }

        private void barManageIn_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("设备登记"))
            {
                ShowViewInWorkspace(new QueryMainframe(), "设备登记");
            }
        }

        private void btnClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

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

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("水处理机运行记录"))
            {
                ShowViewInWorkspace(new WaterProcessorUseRecord(), "水处理机运行记录");
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
    }
}