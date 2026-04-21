/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：血透患者数据上报管理中心窗体
// 创建时间：2015-04-15
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
using Hemo.Client.UI.Machine;
using Hemo.Client.UI.DataReport.HemoInfos;
using Hemo.Model;
using Hemo.Client.UI.DataReport.Diagnose;
using Hemo.Client.UI.DataReport.LabInfos;

namespace Hemo.Client.UI.DataReport
{
    public partial class DataReportManager : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region 类变量

        #endregion

        #region 属性

        public DataReportModel.MED_PATIENTSRow _currentPatientRow { get; set; }

        #endregion

        #region 构造函数

        public DataReportManager()
        {
            InitializeComponent();
            this.barBtn_User.Caption = string.IsNullOrEmpty(HemoApplicationContext.Current.CurrentUser.USER_NAME) ? "用户" : HemoApplicationContext.Current.CurrentUser.USER_NAME;
            this.barBtn_Date.Caption = DateTime.Today.ToString("yyyy年MM月dd日");
            this.barBtn_IP.Caption = HemoApplicationContext.Current.IpAddress;
            this.barBtn_Version.Caption = HemoApplicationContext.Current.versionAddress;

            if (!IsViewInWorkspace("患者基本信息上传"))
            {
                ShowViewInWorkspace(new UploadPateientInfo(), "患者基本信息上传");
            }
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
            if (!IsViewInWorkspace("患者基本信息上传"))
            {
                ShowViewInWorkspace(new UploadPateientInfo(), "患者基本信息上传");
            }
        }
        /// <summary>
        /// 诊断信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("诊断信息"))
            {
                ShowViewInWorkspace(new PatientDiagnoseInfo(_currentPatientRow), "诊断信息");
            }
        }

        private void btnClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void DataReportManager_FormClosing(object sender, FormClosingEventArgs e)
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

        /// <summary>
        /// 血透信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("血透信息"))
            {
                ShowViewInWorkspace(new PatientHemoInfo(_currentPatientRow), "血透信息");
            }
        }
        /// <summary>
        /// 实验室及辅助检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("实验室及辅助检查"))
            {
                ShowViewInWorkspace(new LabAndCheckInfo(_currentPatientRow), "实验室及辅助检查");
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

        /// <summary>
        /// 设置显示
        /// </summary>
        /// <param name="isVisble"></param>
        public void SetMenuVisble(bool isVisble)
        {
            this.barButtonItem5.Visibility = isVisble ? BarItemVisibility.Always : BarItemVisibility.Never;
            this.barButtonItem2.Visibility = isVisble ? BarItemVisibility.Always : BarItemVisibility.Never;
            this.barButtonItem4.Visibility = isVisble ? BarItemVisibility.Always : BarItemVisibility.Never;
            this.barButtonItem3.Visibility = isVisble ? BarItemVisibility.Always : BarItemVisibility.Never;

        }

        #endregion  
    }
}