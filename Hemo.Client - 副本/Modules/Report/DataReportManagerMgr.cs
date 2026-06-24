/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:增加窗体控件值的方法
 * 创建标识:刘超-2016年12月17日
 * 
 * 修改时间:2017年5月4日
 * 修改人:吕志强
 * 修改描述:用户控件
 * 
 * 修改时间:2017年6月5日
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
using Hemo.Client.UI.DataReport.HemoInfos;
using Hemo.Model;
using Hemo.Client.UI.DataReport.Diagnose;
using Hemo.Client.UI.DataReport.LabInfos;
using Hemo.Client.UI.DataReport;
using Hemo.Client.UI.DataReportFZ;
using Hemo.Client.UI.DataReportFZ.CureInfo;

namespace Hemo.Client.Modules
{
    public partial class DataReportManagerMgr : DevExpress.XtraEditors.XtraUserControl
    {
        #region 构造函数

        public DataReportManagerMgr()
        {
            InitializeComponent();
            this.barBtn_User.Caption = string.IsNullOrEmpty(HemoApplicationContext.Current.CurrentUser.USER_NAME) ? "用户" : HemoApplicationContext.Current.CurrentUser.USER_NAME;
            this.barBtn_Date.Caption = DateTime.Today.ToString("yyyy年MM月dd日");
            this.barBtn_IP.Caption = HemoApplicationContext.Current.IpAddress;

            //if (!IsViewInWorkspace("患者基本信息上传"))
            //{
            //    ShowViewInWorkspace(new UploadPateientInfo(), "患者基本信息上传");
            //}
        }
        #endregion
        #region 变量

        /// <summary>
        /// 病人数据
        /// </summary>
        public DataReportModel.MED_PATIENTSRow _currentPatientRow { get; set; }
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
        /// 关闭
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
        /// 患者基本信息上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            //this.Close();
        }
        /// <summary>
        /// EXIT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataReportManagerMgr_FormClosing(object sender, FormClosingEventArgs e)
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
        public void Close(string tabViewName)
        {
            foreach (XtraTabPage item in viewHostTabControl.TabPages)
            {
                if (item.Text.Equals(tabViewName))
                {
                    viewHostTabControl.TabPages.Remove(item);
                    break;
                }
            }
        }
        /// <summary>
        /// 设置显示
        /// </summary>
        /// <param name="isVisble"></param>
        public void SetMenuVisbleFZ(bool isVisble)
        {
            this.barButtonItem8.Visibility = isVisble ? BarItemVisibility.Always : BarItemVisibility.Never;
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
        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("患者信息上报"))
            {
                ShowViewInWorkspace(new UploadPateientInfoFZ(), "患者信息上报");
            }
        }
        public void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("患者治疗信息"))
            {
                ShowViewInWorkspace(new PatientCureInfo(_currentPatientRow), "患者治疗信息");
            }
        }
        #endregion

     

       

    }
}