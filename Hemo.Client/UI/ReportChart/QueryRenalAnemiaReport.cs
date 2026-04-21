/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:肾性贫血统计查询类
 * 创建标识:吕志强-2017年5月17日
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
    public partial class QueryRenalAnemiaReport : ViewBase
    {
        #region 成员变量

        ILab labService = ServiceManager.Instance.LabService;

        private DataTable dtResult = null;

        private DataTable dtReport = null;

        #endregion

        #region 构造函数

        public QueryRenalAnemiaReport()
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
        private void QueryRenalAnemiaReport_Load(object sender, EventArgs e)
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
            this.ctlYearQuery.ExportExcel(this.gcRenalAnemia, "肾性贫血纠正例数统计");
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlYearQuery_PrintEvent(object sender, EventArgs e)
        {
            this.ctlYearQuery.Print(this.gcRenalAnemia);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            int sum = 0;
            dtResult = labService.GetRenalAnemiaCountList(this.ctlYearQuery.Year);
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
                sum += int.Parse(row["RENALANEMIA_COUNT"].ToString());
            });

            DataRow rowSum = dtReport.NewRow();
            rowSum["DATE_MONTH"] = "合计";
            rowSum["RENALANEMIA_COUNT"] = sum;
            dtReport.Rows.Add(rowSum);

            this.gcRenalAnemia.DataSource = dtReport;
        }

        /// <summary>
        /// 加载报表图样
        /// </summary>
        private void LoadReportChart()
        {
            this.cclRenalAnemia.Series.Clear();
            if (dtReport != null && dtReport.Rows.Count > 0)
            {
                DataTable dtSub = dtReport.Clone();
                dtReport.AsEnumerable().ToList().ForEach(row =>
                {
                    dtSub.ImportRow(row);
                });
                dtSub.Rows.RemoveAt(dtSub.Rows.Count - 1);

                Series seriesRenalAnemia = new Series("RenalAnemia", ViewType.Bar);
                seriesRenalAnemia.DataSource = dtSub;
                seriesRenalAnemia.ArgumentDataMember = "DATE_MONTH";
                seriesRenalAnemia.ArgumentScaleType = ScaleType.Qualitative;
                seriesRenalAnemia.ValueDataMembers.AddRange(new string[] { "RENALANEMIA_COUNT" });
                seriesRenalAnemia.ValueScaleType = ScaleType.Numerical;
                seriesRenalAnemia.LegendText = "肾性贫血纠正例数";
                this.cclRenalAnemia.Series.Add(seriesRenalAnemia);

                ((XYDiagram)this.cclRenalAnemia.Diagram).AxisY.Title.Text = "人数";
                ((XYDiagram)this.cclRenalAnemia.Diagram).AxisY.Title.Font = new Font("Tahoma", 10);
                ((XYDiagram)this.cclRenalAnemia.Diagram).AxisY.Title.TextColor = Color.Red;
                ((XYDiagram)this.cclRenalAnemia.Diagram).AxisY.Title.Visible = true;

                ChartTitle title = new ChartTitle();
                title.Text = "肾性贫血纠正例数统计";
                title.Font = new Font("Tahoma", 12);
                title.Dock = ChartTitleDockStyle.Top;
                this.cclRenalAnemia.Titles.Clear();
                this.cclRenalAnemia.Titles.Add(title);

                this.cclRenalAnemia.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
                this.cclRenalAnemia.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
                this.cclRenalAnemia.Legend.Direction = LegendDirection.TopToBottom;
            }
        }

        #endregion
    }
}
