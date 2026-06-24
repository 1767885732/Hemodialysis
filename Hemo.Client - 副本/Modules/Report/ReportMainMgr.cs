/*----------------------------------------------------------------
 * Copyright (C) 2005 麦迪斯顿医疗科技发展有限公司
 * 文件功能描述:修复系统响应速度慢的问题
 * 创建标识:贺建操-2016年11月26日
 * 
 * 修改时间:2017年4月13日
 * 修改人:贺建操
 * 修改描述:修复系统加载时数据缓存问题
 * 
 * 修改时间:2017年5月15日
 * 修改人:顾伟伟
 * 修改描述:用户控件
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
using Hemo.Client.Core;
using Hemo.Client.UI.ReportChart;
using Hemo.Client.UI.Patient;
using DevExpress.XtraTab.ViewInfo;
using Hemo.Client.UI.Assessment;
using Hemo.WinForm;
using Hemo.Client.Modules.NurseModel;

namespace Hemo.Client.Modules
{
    public partial class ReportMainMgr : ViewBase
    {

        /// <summary>
        /// 当前点击的Tab
        /// </summary>
        private BaseTabHitInfo CurrentSelectingTab = null;


        public ReportMainMgr()
        {
            InitializeComponent();
        }
        #region 事件

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
            view.Name = name;

            this.workSpaceControl.TabPages.Add(page);
            this.workSpaceControl.SelectedTabPage = page;

            view.CloseButtonClicked += delegate
            {
                workSpaceControl_CloseButtonClick(view, EventArgs.Empty);
            };





        }
        /// <summary>
        /// "首页")
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
            if (!IsViewInWorkspace("治疗例数"))
            {
                ShowViewInWorkSpace(new CtlShowAllHemoInfo(), "治疗例数");
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
        /// ReportMainFrm_FormClosing
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
            if (!IsViewInWorkspace("通路手术例数"))
            {
                ShowViewInWorkSpace(new QueryVascular(), "通路手术例数");
            }
        }
        /// <summary>
        /// workSpaceControl_CloseButtonClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workSpaceControl_CloseButtonClick(object sender, EventArgs e)
        {
            //if (this.workSpaceControl.SelectedTabPage != null)
            //{
            //    var current = this.workSpaceControl.SelectedTabPage.Controls[0] as ViewBase;
            //    if (current != null)
            //    {
            //        if (current.HasDirty)
            //        {
            //            if (XtraMessageBox.Show("当前界面含有未保存的数据,是否确定关闭?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //                return;
            //        }
            //        var tabPage = this.workSpaceControl.SelectedTabPage;
            //        this.workSpaceControl.TabPages.Remove(tabPage);
            //        if (tabPage != null)
            //            tabPage.Dispose();
            //        current.Dispose();
            //    }
            //}


            if (CurrentSelectingTab != null)
            {
                XtraTabPage page = (XtraTabPage)CurrentSelectingTab.Page;

                var current = page.Controls[0] as ViewBase;
                if (current != null)
                {
                    if (current.HasDirty)
                    {
                        if (XtraMessageBox.Show("当前界面含有未保存的数据,是否确定关闭?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;
                    }
                    current.Dispose();
                    this.workSpaceControl.TabPages.Remove((XtraTabPage)CurrentSelectingTab.Page);
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
            if (!IsViewInWorkspace("异常消息"))
            {
                //ShowViewInWorkSpace(new CommMsgMgr(), "异常消息");
            }


            //if (!IsViewInWorkspace("工作量统计"))
            //{
            //    ShowViewInWorkSpace(new QueryWorkload(), "工作量统计");
            //}
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
                ShowViewInWorkSpace(new QueryHemoCureReport(), "感染检查");
            }

            //if (!IsViewInWorkspace("感染检查"))
            //{
            //    ShowViewInWorkSpace(new QueryInfectionCheckReport(), "感染检查");
            //}
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

        private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
        {
            // this.Close();
        }

        //死亡率
        private void barButtonItem16_ItemClick(object senderr, ItemClickEventArgs ee)
        {
            //LoginScreen frm = new LoginScreen();
            //frm.ShiftRoles = HemoApplicationContext.Current.RolesOffices;
            //frm.LoginEvent += delegate(object sender, LoginEventArgs e)
            //{
            //    frm.Dispose();
            //    this.Dispose();
            //    Program.Show(e.RunApp, e.RunAppNames);
            //};
            //frm.ShowDialog();

            if (!IsViewInWorkspace("患者死亡率"))
            {
                ShowViewInWorkSpace(new QueryDeathRate(), "患者死亡率");
            }
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

        /// <summary>
        /// 透析龄统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem21_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("透析龄统计"))
            {
                ShowViewInWorkSpace(new QueryPatientHemoAge(), "透析龄统计");
            }
        }

        /// <summary>
        /// 患者综合查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem22_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("患者综合查询"))
            {
                ShowViewInWorkSpace(new QueryPatientMoreInfo(), "患者综合查询");
            }
        }
        /// <summary>
        /// 质控数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barQualityControl_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("质控数据"))
            {
                ShowViewInWorkSpace(new QualityControlData(), "质控数据");
            }
        }
        /// <summary>
        /// 质控指标查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barQualitySearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("质控指标查询"))
            {
                ShowViewInWorkSpace(new QualitySearch(), "质控指标查询");
            }
        }
        /// <summary>
        /// 院感相关性查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem24_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("院感相关性查询"))
            {
                ShowViewInWorkSpace(new QueryInfectionScaleReport(), "院感相关性查询");
            }
        }

        /// <summary>
        /// 透前体重达标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void barButtonItem33_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("透前体重达标"))
            {
                ShowViewInWorkSpace(new QueryTouQianTiZhongDaBiaoReport(), "透前体重达标");
            }
        }


        /// <summary>
        /// 监测日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem23_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("监测日志"))
            {
                ShowViewInWorkSpace(new HemoEventExtManager(), "监测日志");
            }
        }
        #endregion

        private void workSpaceControl_MouseDown(object sender, MouseEventArgs e)
        {
            CurrentSelectingTab = this.workSpaceControl.CalcHitInfo(new Point(e.X, e.Y));
        }

        private void barButtonGroup1_ItemDoubleClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonGroup1_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonGroup2_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem31_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("通路类别统计"))
            {
                ShowViewInWorkSpace(new QueryVascularType(), "通路类别统计");
            }
        }

        private void barButtonItem30_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("血压控制例数"))
            {
                ShowViewInWorkSpace(new BloodControlReport(), "血压控制例数");
            }
        }

        private void barButtonItem17_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("乙肝丙肝转阳例数"))
            {
                ShowViewInWorkSpace(new InfectiousCountReport(), "乙肝丙肝转阳例数");
            }
        }

        private void barButtonItem26_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("检验信息达标统计"))
            {
                ShowViewInWorkSpace(new QueryPatientLabTypeReport(), "检验信息达标统计");
            }

        }

        private void barButtonItem28_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem25_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("患者检验图表"))
            {
                ShowViewInWorkSpace(new QueryLabValueLineType(), "患者检验图表");
            }
        }

        private void barButtonItem27_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("患者治疗通路统计"))
            {
                ShowViewInWorkSpace(new QueryCureVascularType(), "患者治疗通路统计");
            }
        }

        private void barButtonItem28_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            if (!IsViewInWorkspace("患者相关统计"))
            {
                ShowViewInWorkSpace(new AllPtientFileter(), "患者相关统计");
            }
        }

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
        工作量统计 = 9
    }
}