/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:溶质清除统计查询类
 * 创建标识:吕志强-2017年3月26日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.UI.Machine;
using Hemo.IService.Lab;
using Hemo.Service;
using System.Linq;
using DevExpress.XtraCharts;

namespace Hemo.Client.UI.ReportChart
{
    public partial class QueryUreaRemoveReport : ViewBase
    {
        #region 成员变量

        ILab labService = ServiceManager.Instance.LabService;

        private DataTable dtResult = null;

        private DataTable dtReport = null;

        #endregion

        #region 构造函数

        public QueryUreaRemoveReport()
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
        private void QueryUreaRemoveReport_Load(object sender, EventArgs e)
        {
            this.ctlYearQuery.QueryEvent += new EventHandler(ctlYearQuery_QueryEvent);
            this.ctlYearQuery.ExpExcelEvent += new EventHandler(ctlYearQuery_ExpExcelEvent);
            this.ctlYearQuery.PrintEvent += new EventHandler(ctlYearQuery_PrintEvent);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlYearQuery_QueryEvent(object sender, EventArgs e)
        {
            Query();
            LoadReportChart();
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlYearQuery_ExpExcelEvent(object sender, EventArgs e)
        {
            this.ctlYearQuery.ExportExcel(this.gcUreaRemove, "溶质清除例数统计");
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlYearQuery_PrintEvent(object sender, EventArgs e)
        {
            this.ctlYearQuery.Print(this.gcUreaRemove);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            int sum = 0;
            dtResult = labService.GetUreaRemoveCountList(this.ctlYearQuery.Year);
            dtReport = this.ctlYearQuery.IsYear ? dtResult : dtResult.Clone();

            if (this.ctlYearQuery.MonthValue != null)
            {
                //月度
                DataRow[] rows = dtResult.Select("DATE_MONTH='" + this.ctlYearQuery.Year + "-" + this.ctlYearQuery.Month + "'");
                rows.AsEnumerable().ToList().ForEach(row =>
                {
                    dtReport.ImportRow(row);
                });
            }
            else if (this.ctlYearQuery.QuarterValue != null)
            {
                //季度
                DataRow[] rows = dtResult.Select("DATE_MONTH>='" + this.ctlYearQuery.From + "'" + " and " + "DATE_MONTH<='" + this.ctlYearQuery.To + "'");
                rows.AsEnumerable().ToList().ForEach(row =>
                {
                    dtReport.ImportRow(row);
                });
            }

            dtReport.AsEnumerable().ToList().ForEach(row =>
            {
                sum += int.Parse(row["UREA_COUNT"].ToString());
            });

            DataRow rowSum = dtReport.NewRow();
            rowSum["DATE_MONTH"] = "合计";
            rowSum["UREA_COUNT"] = sum;
            dtReport.Rows.Add(rowSum);

            this.gcUreaRemove.DataSource = dtReport;
        }

        /// <summary>
        /// 加载报表图样
        /// </summary>
        private void LoadReportChart()
        {
            this.cclUreaRemove.Series.Clear();
            if (dtReport != null && dtReport.Rows.Count > 0)
            {
                DataTable dtSub = dtReport.Clone();
                dtReport.AsEnumerable().ToList().ForEach(row =>
                {
                    dtSub.ImportRow(row);
                });
                dtSub.Rows.RemoveAt(dtSub.Rows.Count - 1);

                Series seriesUreaRemove = new Series("UreaRemove", ViewType.Bar);
                seriesUreaRemove.DataSource = dtSub;
                seriesUreaRemove.ArgumentDataMember = "DATE_MONTH";
                seriesUreaRemove.ArgumentScaleType = ScaleType.Qualitative;
                seriesUreaRemove.ValueDataMembers.AddRange(new string[] { "UREA_COUNT" });
                seriesUreaRemove.ValueScaleType = ScaleType.Numerical;
                seriesUreaRemove.LegendText = "溶质清除";
                this.cclUreaRemove.Series.Add(seriesUreaRemove);

                ((XYDiagram)this.cclUreaRemove.Diagram).AxisY.Title.Text = "人数";
                ((XYDiagram)this.cclUreaRemove.Diagram).AxisY.Title.Font = new Font("Tahoma", 10);
                ((XYDiagram)this.cclUreaRemove.Diagram).AxisY.Title.TextColor = Color.Red;
                ((XYDiagram)this.cclUreaRemove.Diagram).AxisY.Title.Visible = true;

                ChartTitle title = new ChartTitle();
                title.Text = "溶质清除例数统计";
                title.Font = new Font("Tahoma", 12);
                title.Dock = ChartTitleDockStyle.Top;
                this.cclUreaRemove.Titles.Clear();
                this.cclUreaRemove.Titles.Add(title);

                this.cclUreaRemove.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
                this.cclUreaRemove.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
                this.cclUreaRemove.Legend.Direction = LegendDirection.TopToBottom;
            }
        }

        #endregion
    }
}
