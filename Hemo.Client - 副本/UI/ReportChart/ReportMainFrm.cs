/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:综合统计报表类
 * 创建标识:刘超-2017年5月12日
 * 
 *  修改时间:2017年5月24日
 * 修改人:吕志强
 * 修改描述:修改部分业务逻辑及界面
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraTab;
using Hemo.Client.UI.Hemodialysis;
using DevExpress.XtraEditors;
using Hemo.Client.UI.Machine;
using Hemo.WinForm;
using Hemo.Client.Core;

namespace Hemo.Client.UI.ReportChart
{
    public partial class ReportMainFrm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region 构造函数

        public ReportMainFrm()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 治疗数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barCureData_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("治疗数据"))
            {
                ShowViewInWorkSpace(new CtlHemoParametersChart(), "治疗数据");
            }
        }

        /// <summary>
        /// 页面是否已经存在
        /// </summary>
        /// <param name="caption"></param>
        /// <returns></returns>
        private bool IsViewInWorkspace(string caption)
        {
            this.workSpaceControl.Visible = true;
            foreach (XtraTabPage tabPage in this.workSpaceControl.TabPages)
            {
                if (tabPage.Text == caption)
                {
                    this.workSpaceControl.SelectedTabPage = tabPage;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 将指定的界面添加到工作区域中,并设置Tab标题
        /// </summary>
        /// <param name="view"></param>
        /// <param name="caption"></param>
        private void ShowViewInWorkSpace(ViewBase view, string name)
        {
            XtraTabPage page = new XtraTabPage();
            page.Text = name;
            if (name.Equals("首页"))
                page.ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
            view.Dock = DockStyle.Fill;
            page.Controls.Add(view);
            page.Padding = new Padding(2);

            this.workSpaceControl.TabPages.Add(page);
            this.workSpaceControl.SelectedTabPage = page;

            view.CloseButtonClicked += delegate
            {
                workSpaceControl_CloseButtonClick(view, EventArgs.Empty);
            };
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportMainFrm_Load(object sender, EventArgs e)
        {
            if (!IsViewInWorkspace("首页"))
            {
                ShowViewInWorkSpace(new IntegratedQuery(), "首页");
            }
        }

        /// <summary>
        /// 数据汇总
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSumData_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("数据汇总"))
            {
                ShowViewInWorkSpace(new CtlShowAllHemoInfo(), "数据汇总");
            }
        }

        /// <summary>
        /// 血透患者记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barCureList_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("血透患者记录"))
            {
                ShowViewInWorkSpace(new CtlQueryCureList(), "血透患者记录");
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportMainFrm_FormClosing(object sender, FormClosingEventArgs e)
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
        /// 导管手术例数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barVasular_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("导管手术例数"))
            {
                ShowViewInWorkSpace(new QueryVascular(), "导管手术例数");
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workSpaceControl_CloseButtonClick(object sender, EventArgs e)
        {
            if (this.workSpaceControl.SelectedTabPage != null)
            {
                var current = this.workSpaceControl.SelectedTabPage.Controls[0] as ViewBase;
                if (current != null)
                {
                    if (current.HasDirty)
                    {
                        if (XtraMessageBox.Show("当前界面含有未保存的数据,是否确定关闭?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;
                    }
                    var tabPage = this.workSpaceControl.SelectedTabPage;
                    this.workSpaceControl.TabPages.Remove(tabPage);
                    if (tabPage != null)
                        tabPage.Dispose();
                    current.Dispose();
                }
            }
        }

        /// <summary>
        /// 透析男女比例
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("透析男女比例"))
            {
                ShowViewInWorkSpace(new QuerySexScaleReport(), "透析男女比例");
            }
        }

        /// <summary>
        /// 透析年龄段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("透析年龄段"))
            {
                ShowViewInWorkSpace(new QueryAgeScaleReport(), "透析年龄段");
            }
        }

        /// <summary>
        /// 传染病
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("传染病"))
            {
                ShowViewInWorkSpace(new QueryInfectousScaleReport(), "传染病");
            }
        }

        /// <summary>
        /// 规律透析比例
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("规律透析比例"))
            {
                var frm = new QueryHemoCoutScaleReport();
                ShowViewInWorkSpace(frm, "规律透析比例");
            }
        }

        /// <summary>
        /// 工作量统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("工作量统计"))
            {
                ShowViewInWorkSpace(new QueryWorkload(), "工作量统计");
            }
        }

        /// <summary>
        /// 并发症及其他
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("并发症及其他"))
            {
                ShowViewInWorkSpace(new QueryComplication(), "并发症及其他");
            }
        }

        /// <summary>
        /// 感染检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("感染检查"))
            {
                ShowViewInWorkSpace(new QueryInfectionCheckReport(), "感染检查");
            }
        }

        /// <summary>
        /// 质量管理基础数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem19_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("质量管理基础数据"))
            {
                ShowViewInWorkSpace(new QueryQualityControlReport(), "质量管理基础数据");
            }
        }

        /// <summary>
        /// 维持性患者质量监测指标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem20_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("维持性患者质量监测指标"))
            {
                ShowViewInWorkSpace(new QueryQualityMonitorIndicatorReportNew(), "维持性患者质量监测指标");
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 切换登录
        /// </summary>
        /// <param name="senderr"></param>
        /// <param name="ee"></param>
        private void barButtonItem16_ItemClick(object senderr, ItemClickEventArgs ee)
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
        /// 维持性血透患者质量监测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("维持性血透患者质量监测"))
            {
                ShowViewInWorkSpace(new QueryQualityPatientData(), "维持性血透患者质量监测");
            }
        }

        #endregion 
    }

    /// <summary>
    /// 报表类型枚举类
    /// </summary>
    public enum ReportTypeEnum
    {
        治疗数据 = 1,
        血透患者记录 = 2,
        数据汇总 = 3,
        导管手术例数 = 4,
        透析男女比例 = 5,
        透析年龄段 = 6,
        传染病 = 7,
        规律透析比例 = 8,
        工作量统计 = 9,
        通路类别人数 = 10,
        死亡登记 = 11,
        血压控制例数 = 12,
        透前体重达标 = 13,
        溶质清除患者比例 = 14,
        血红蛋白达标率 = 15,
        钙磷代谢例数 = 16,
        甲状旁腺功能亢进 = 17,
        检验导出数据 = 18,
        乙肝丙肝转阳 = 19,
    }
}