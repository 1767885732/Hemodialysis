/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:传染病统计查询类
 * 创建标识:刘超-2017年3月15日
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
using Hemo.Utilities;
using Hemo.IService.Lab;
using Hemo.Service;
using System.Linq;
using DevExpress.XtraCharts;
using DevExpress.XtraPrinting;
using Hemo.Client.Print;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.UI.ReportChart
{
    public partial class QueryInfectionCheckReport : ViewBase
    {
        #region 成员变量

        private ILab labService = ServiceManager.Instance.LabService;

        private DataTable dtResult = null;

        private DataTable dtReport = null;

        #endregion

        #region 构造函数

        public QueryInfectionCheckReport()
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
        private void QueryInfectionCheckReport_Load(object sender, EventArgs e)
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
            this.ctlYearQuery.ExportExcel(this.gcCheck, "院感检查统计");
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlYearQuery_PrintEvent(object sender, EventArgs e)
        {
            string title = string.Empty;
            if (this.ctlYearQuery.PageIndex == 0)
            {
                title += this.ctlYearQuery.FromDate.ToString("yyyy年MM月dd日") + "~" + this.ctlYearQuery.ToDate.ToString("yyyy年MM月dd日");
            }
            else
            {
                if (this.ctlYearQuery.MonthValue != null)
                {
                    title += this.ctlYearQuery.Year + "年" + this.ctlYearQuery.Month + "月";
                }
                else if (this.ctlYearQuery.QuarterValue != null)
                {
                    title += this.ctlYearQuery.Year + "年" + this.ctlYearQuery.QuarterValue.ToString();
                }
                else
                {
                    title += this.ctlYearQuery.Year + "年度";
                }
            }
            title += "院感检查统计";

            InfectionCheckReport infectionCheckReport = new InfectionCheckReport(dtReport, title);
            ReportPrintTool pt = new ReportPrintTool(infectionCheckReport);
            pt.ShowPreviewDialog();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            int sumNegative = 0;
            int sumHBsAg_Positive = 0;
            int sumHBeAg_Positive = 0;
            int sumAnti_HCV_Positive = 0;
            int sumAnti_TP_Positive = 0;
            int sumHIV_Positive = 0;
            int sumPositive = 0;

            if (this.ctlYearQuery.PageIndex == 0)
            {
                dtReport = labService.GetInfectionCheckListByDate(this.ctlYearQuery.FromDate, this.ctlYearQuery.ToDate);
            }
            else
            {
                dtResult = labService.GetInfectionCheckListByYear(this.ctlYearQuery.Year);
                dtReport = ctlYearQuery.IsYear ? dtResult : dtResult.Clone();

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
            }

            dtReport.AsEnumerable().ToList().ForEach(row =>
            {
                sumNegative += int.Parse(row["NEGATIVE"].ToString());
                sumHBsAg_Positive += int.Parse(row["HBSAG_POSITIVE"].ToString());
                sumHBeAg_Positive += int.Parse(row["HBEAG_POSITIVE"].ToString());
                sumAnti_HCV_Positive += int.Parse(row["ANTI_HCV_POSITIVE"].ToString());
                sumAnti_TP_Positive += int.Parse(row["ANTI_TP_POSITIVE"].ToString());
                sumHIV_Positive += int.Parse(row["HIV_POSITIVE"].ToString());
                sumPositive += int.Parse(row["POSITIVE"].ToString());
            });

            DataRow rowSum = dtReport.NewRow();
            rowSum["DATE_MONTH"] = "合计";
            rowSum["NEGATIVE"] = sumNegative;
            rowSum["HBSAG_POSITIVE_DESC"] = sumHBsAg_Positive;
            rowSum["HBEAG_POSITIVE"] = sumHBeAg_Positive;
            rowSum["ANTI_HCV_POSITIVE"] = sumAnti_HCV_Positive;
            rowSum["ANTI_TP_POSITIVE"] = sumAnti_TP_Positive;
            rowSum["HIV_POSITIVE"] = sumHIV_Positive;
            rowSum["POSITIVE"] = sumPositive;
            dtReport.Rows.Add(rowSum);

            this.gcCheck.DataSource = dtReport;
        }

        /// <summary>
        /// 加载报表图样
        /// </summary>
        private void LoadReportChart()
        {
            this.cclCheck.Series.Clear();
            if (dtReport != null && dtReport.Rows.Count > 0)
            {
                DataTable dtSub = dtReport.Clone();
                dtReport.AsEnumerable().ToList().ForEach(row =>
                {
                    dtSub.ImportRow(row);
                });
                dtSub.Rows.RemoveAt(dtSub.Rows.Count - 1);

                Series seriesNegative = new Series("Negative", ViewType.Bar);
                seriesNegative.DataSource = dtSub;
                seriesNegative.ArgumentDataMember = "DATE_MONTH";
                seriesNegative.ArgumentScaleType = ScaleType.Qualitative;
                seriesNegative.ValueDataMembers.AddRange(new string[] { "NEGATIVE" });
                seriesNegative.ValueScaleType = ScaleType.Numerical;
                seriesNegative.LegendText = "全阴";
                cclCheck.Series.Add(seriesNegative);

                Series seriesHBsAg_Positive = new Series("HBsAg_Positive", ViewType.Bar);
                seriesHBsAg_Positive.DataSource = dtSub;
                seriesHBsAg_Positive.ArgumentDataMember = "DATE_MONTH";
                seriesHBsAg_Positive.ArgumentScaleType = ScaleType.Qualitative;
                seriesHBsAg_Positive.ValueDataMembers.AddRange(new string[] { "HBSAG_POSITIVE" });
                seriesHBsAg_Positive.ValueScaleType = ScaleType.Numerical;
                seriesHBsAg_Positive.LegendText = "乙肝表面抗原阳性";
                cclCheck.Series.Add(seriesHBsAg_Positive);

                Series seriesHBeAg_Positive = new Series("HBeAg_Positive", ViewType.Bar);
                seriesHBeAg_Positive.DataSource = dtSub;
                seriesHBeAg_Positive.ArgumentDataMember = "DATE_MONTH";
                seriesHBeAg_Positive.ArgumentScaleType = ScaleType.Qualitative;
                seriesHBeAg_Positive.ValueDataMembers.AddRange(new string[] { "HBEAG_POSITIVE" });
                seriesHBeAg_Positive.ValueScaleType = ScaleType.Numerical;
                seriesHBeAg_Positive.LegendText = "乙肝E抗原阳性";
                cclCheck.Series.Add(seriesHBeAg_Positive);

                Series seriesAnti_HCV_Positive = new Series("Anti_HCV_Positive", ViewType.Bar);
                seriesAnti_HCV_Positive.DataSource = dtSub;
                seriesAnti_HCV_Positive.ArgumentDataMember = "DATE_MONTH";
                seriesAnti_HCV_Positive.ArgumentScaleType = ScaleType.Qualitative;
                seriesAnti_HCV_Positive.ValueDataMembers.AddRange(new string[] { "ANTI_HCV_POSITIVE" });
                seriesAnti_HCV_Positive.ValueScaleType = ScaleType.Numerical;
                seriesAnti_HCV_Positive.LegendText = "丙肝病毒抗体阳性";
                cclCheck.Series.Add(seriesAnti_HCV_Positive);

                Series seriesAnti_TP_Positive = new Series("Anti_TP_Positive", ViewType.Bar);
                seriesAnti_TP_Positive.DataSource = dtSub;
                seriesAnti_TP_Positive.ArgumentDataMember = "DATE_MONTH";
                seriesAnti_TP_Positive.ArgumentScaleType = ScaleType.Qualitative;
                seriesAnti_TP_Positive.ValueDataMembers.AddRange(new string[] { "ANTI_TP_POSITIVE" });
                seriesAnti_TP_Positive.ValueScaleType = ScaleType.Numerical;
                seriesAnti_TP_Positive.LegendText = "梅毒抗体阳性";
                cclCheck.Series.Add(seriesAnti_TP_Positive);

                Series seriesHIV_Positive = new Series("HIV_Positive", ViewType.Bar);
                seriesHIV_Positive.DataSource = dtSub;
                seriesHIV_Positive.ArgumentDataMember = "DATE_MONTH";
                seriesHIV_Positive.ArgumentScaleType = ScaleType.Qualitative;
                seriesHIV_Positive.ValueDataMembers.AddRange(new string[] { "HIV_POSITIVE" });
                seriesHIV_Positive.ValueScaleType = ScaleType.Numerical;
                seriesHIV_Positive.LegendText = "HIV阳性";
                cclCheck.Series.Add(seriesHIV_Positive);

                Series seriesPositive = new Series("Positive", ViewType.Bar);
                seriesPositive.DataSource = dtSub;
                seriesPositive.ArgumentDataMember = "DATE_MONTH";
                seriesPositive.ArgumentScaleType = ScaleType.Qualitative;
                seriesPositive.ValueDataMembers.AddRange(new string[] { "POSITIVE" });
                seriesPositive.ValueScaleType = ScaleType.Numerical;
                seriesPositive.LegendText = "转阳";
                cclCheck.Series.Add(seriesPositive);

                ((XYDiagram)this.cclCheck.Diagram).AxisY.Title.Text = "人数";
                ((XYDiagram)this.cclCheck.Diagram).AxisY.Title.Font = new Font("Tahoma", 10);
                ((XYDiagram)this.cclCheck.Diagram).AxisY.Title.TextColor = Color.Red;
                ((XYDiagram)this.cclCheck.Diagram).AxisY.Title.Visible = true;

                ChartTitle title = new ChartTitle();
                title.Text = "感染检查统计";
                title.Font = new Font("Tahoma", 12);
                title.Dock = ChartTitleDockStyle.Top;
                this.cclCheck.Titles.Clear();
                this.cclCheck.Titles.Add(title);

                this.cclCheck.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
                this.cclCheck.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
                this.cclCheck.Legend.Direction = LegendDirection.TopToBottom;
            }
        }

        #endregion
    }
}
