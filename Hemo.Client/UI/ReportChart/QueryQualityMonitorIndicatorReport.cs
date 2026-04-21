/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:质量监测指标统计查询类
 * 创建标识:吕志强-2017年4月26日
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
using Hemo.IService.Config;
using Hemo.Service;
using System.Linq;
using DevExpress.XtraCharts;
using Hemo.IService.Lab;
using Hemo.Client.Print;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.UI.ReportChart
{
    public partial class QueryQualityMonitorIndicatorReport : ViewBase
    {
        #region 成员变量

        private ILab labService = ServiceManager.Instance.LabService;

        private DataTable dtResult = null;

        private DataTable dtReport = null;

        #endregion

        #region 构造函数

        public QueryQualityMonitorIndicatorReport()
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
        private void QueryQualityControlReport_Load(object sender, EventArgs e)
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
            this.ctlYearQuery.ExportExcel(this.gcQualityMonitorIndicator, "维持性血透患者质量监测指标统计");
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
            title += "维持性血透患者质量监测指标统计";

            QualityMonitorIndicatorReport qualityMonitorIndicatorReport = new QualityMonitorIndicatorReport(dtReport, title);
            ReportPrintTool pt = new ReportPrintTool(qualityMonitorIndicatorReport);
            pt.ShowPreviewDialog();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            int sumUreaRemove = 0;
            int sumRenalAnemia = 0;
            int sumCa_P_Metabolism = 0;
            int sumSecondaryShpt = 0;
            int sumVenousCatheter = 0;
            int sumAutologousFistula = 0;
            int sumTempVenousCatheter = 0;
            int sumArtificialVessel = 0;
            int sumDoubleVein = 0;
            int sumHighAvf = 0;
            int sumJugularVenousCatheter = 0;
            int sumSubclavianVenousCatheter = 0;
            int sumFemoralVenousCatheter = 0;
            int sumPressureControl = 0;
            int sumTimeLess8 = 0;
            int sumTime8_9 = 0;
            int sumTime9_10 = 0;
            int sumTime10_11 = 0;
            int sumTime11_12 = 0;
            int sumTimeMore12 = 0;
            int sumComfort = 0;
            int sumMildDiscomfort = 0;
            int sumSevereDiscomfort = 0;
            int sumPeritonealDialysis = 0;

            if (this.ctlYearQuery.PageIndex == 0)
            {
                dtReport = labService.GetQualityMonitorIndicatorByDate(this.ctlYearQuery.FromDate, this.ctlYearQuery.ToDate);
            }
            else
            {
                dtResult = labService.GetQualityMonitorIndicatorByYear(this.ctlYearQuery.Year);
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
            }

            dtReport.AsEnumerable().ToList().ForEach(row =>
            {
                sumUreaRemove += int.Parse(row["UREA_REMOVE"].ToString());
                sumRenalAnemia += int.Parse(row["RENAL_ANEMIA"].ToString());
                sumCa_P_Metabolism += int.Parse(row["CA_P_METABOLISM"].ToString());
                sumSecondaryShpt += int.Parse(row["SECONDARY_SHPT"].ToString());
                sumVenousCatheter += int.Parse(row["VENOUS_CATHETER"].ToString());
                sumAutologousFistula += int.Parse(row["AUTOLOGOUS_FISTULA"].ToString());
                sumTempVenousCatheter += int.Parse(row["TEMP_VENOUS_CATHETER"].ToString());
                sumArtificialVessel += int.Parse(row["ARTIFICIAL_VESSEL"].ToString());
                sumDoubleVein += int.Parse(row["DOUBLE_VEIN"].ToString());
                sumHighAvf += int.Parse(row["HIGH_AVF"].ToString());
                sumJugularVenousCatheter += int.Parse(row["JUGULAR_VENOUS_CATHETER"].ToString());
                sumSubclavianVenousCatheter += int.Parse(row["SUBCLAVIAN_VENOUS_CATHETER"].ToString());
                sumFemoralVenousCatheter += int.Parse(row["FEMORAL_VENOUS_CATHETER"].ToString());
                sumPressureControl += int.Parse(row["PRESSURE_CONTROL"].ToString());
                sumTimeLess8 += int.Parse(row["TIME_LESS_8"].ToString());
                sumTime8_9 += int.Parse(row["TIME_8_9"].ToString());
                sumTime9_10 += int.Parse(row["TIME_9_10"].ToString());
                sumTime10_11 += int.Parse(row["TIME_10_11"].ToString());
                sumTime11_12 += int.Parse(row["TIME_11_12"].ToString());
                sumTimeMore12 += int.Parse(row["TIME_MORE_12"].ToString());
                sumComfort += int.Parse(row["COMFORT"].ToString());
                sumMildDiscomfort += int.Parse(row["MILD_DISCOMFORT"].ToString());
                sumSevereDiscomfort += int.Parse(row["SEVERE_DISCOMFORT"].ToString());
                sumPeritonealDialysis += int.Parse(row["PERITONEAL_DIALYSIS"].ToString());
            });

            DataRow rowSum = dtReport.NewRow();
            rowSum["DATE_MONTH"] = "合计";
            rowSum["UREA_REMOVE"] = sumUreaRemove;
            rowSum["RENAL_ANEMIA"] = sumRenalAnemia;
            rowSum["CA_P_METABOLISM"] = sumCa_P_Metabolism;
            rowSum["SECONDARY_SHPT"] = sumSecondaryShpt;
            rowSum["VENOUS_CATHETER"] = sumVenousCatheter;
            rowSum["AUTOLOGOUS_FISTULA"] = sumAutologousFistula;
            rowSum["TEMP_VENOUS_CATHETER"] = sumTempVenousCatheter;
            rowSum["ARTIFICIAL_VESSEL"] = sumArtificialVessel;
            rowSum["DOUBLE_VEIN"] = sumDoubleVein;
            rowSum["HIGH_AVF"] = sumHighAvf;
            rowSum["JUGULAR_VENOUS_CATHETER"] = sumJugularVenousCatheter;
            rowSum["SUBCLAVIAN_VENOUS_CATHETER"] = sumSubclavianVenousCatheter;
            rowSum["FEMORAL_VENOUS_CATHETER"] = sumFemoralVenousCatheter;
            rowSum["PRESSURE_CONTROL"] = sumPressureControl;
            rowSum["TIME_LESS_8"] = sumTimeLess8;
            rowSum["TIME_8_9"] = sumTime8_9;
            rowSum["TIME_9_10"] = sumTime9_10;
            rowSum["TIME_10_11"] = sumTime10_11;
            rowSum["TIME_11_12"] = sumTime11_12;
            rowSum["TIME_MORE_12"] = sumTimeMore12;
            rowSum["COMFORT"] = sumComfort;
            rowSum["MILD_DISCOMFORT"] = sumMildDiscomfort;
            rowSum["SEVERE_DISCOMFORT"] = sumSevereDiscomfort;
            rowSum["PERITONEAL_DIALYSIS"] = sumPeritonealDialysis;
            dtReport.Rows.Add(rowSum);

            this.gcQualityMonitorIndicator.DataSource = dtReport;
        }

        /// <summary>
        /// 加载报表图样
        /// </summary>
        private void LoadReportChart()
        {
            this.cclQualityMonitorIndicator.Series.Clear();
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
                seriesUreaRemove.ValueDataMembers.AddRange(new string[] { "UREA_REMOVE" });
                seriesUreaRemove.ValueScaleType = ScaleType.Numerical;
                seriesUreaRemove.LegendText = "溶质清除";
                cclQualityMonitorIndicator.Series.Add(seriesUreaRemove);

                Series seriesRenalAnemia = new Series("RenalAnemia", ViewType.Bar);
                seriesRenalAnemia.DataSource = dtSub;
                seriesRenalAnemia.ArgumentDataMember = "DATE_MONTH";
                seriesRenalAnemia.ArgumentScaleType = ScaleType.Qualitative;
                seriesRenalAnemia.ValueDataMembers.AddRange(new string[] { "RENAL_ANEMIA" });
                seriesRenalAnemia.ValueScaleType = ScaleType.Numerical;
                seriesRenalAnemia.LegendText = "纠正肾性贫血";
                cclQualityMonitorIndicator.Series.Add(seriesRenalAnemia);

                Series seriesCa_P_Metabolism = new Series("Ca_P_Metabolism", ViewType.Bar);
                seriesCa_P_Metabolism.DataSource = dtSub;
                seriesCa_P_Metabolism.ArgumentDataMember = "DATE_MONTH";
                seriesCa_P_Metabolism.ArgumentScaleType = ScaleType.Qualitative;
                seriesCa_P_Metabolism.ValueDataMembers.AddRange(new string[] { "CA_P_METABOLISM" });
                seriesCa_P_Metabolism.ValueScaleType = ScaleType.Numerical;
                seriesCa_P_Metabolism.LegendText = "钙磷代谢";
                cclQualityMonitorIndicator.Series.Add(seriesCa_P_Metabolism);

                Series seriesSecondaryShpt = new Series("SecondaryShpt", ViewType.Bar);
                seriesSecondaryShpt.DataSource = dtSub;
                seriesSecondaryShpt.ArgumentDataMember = "DATE_MONTH";
                seriesSecondaryShpt.ArgumentScaleType = ScaleType.Qualitative;
                seriesSecondaryShpt.ValueDataMembers.AddRange(new string[] { "SECONDARY_SHPT" });
                seriesSecondaryShpt.ValueScaleType = ScaleType.Numerical;
                seriesSecondaryShpt.LegendText = "甲状旁腺功能亢进";
                cclQualityMonitorIndicator.Series.Add(seriesSecondaryShpt);

                Series seriesVenousCatheter = new Series("VenousCatheter", ViewType.Bar);
                seriesVenousCatheter.DataSource = dtSub;
                seriesVenousCatheter.ArgumentDataMember = "DATE_MONTH";
                seriesVenousCatheter.ArgumentScaleType = ScaleType.Qualitative;
                seriesVenousCatheter.ValueDataMembers.AddRange(new string[] { "VENOUS_CATHETER" });
                seriesVenousCatheter.ValueScaleType = ScaleType.Numerical;
                seriesVenousCatheter.LegendText = "静脉留置导管";
                cclQualityMonitorIndicator.Series.Add(seriesVenousCatheter);

                Series seriesAutologousFistula = new Series("AutologousFistula", ViewType.Bar);
                seriesAutologousFistula.DataSource = dtSub;
                seriesAutologousFistula.ArgumentDataMember = "DATE_MONTH";
                seriesAutologousFistula.ArgumentScaleType = ScaleType.Qualitative;
                seriesAutologousFistula.ValueDataMembers.AddRange(new string[] { "AUTOLOGOUS_FISTULA" });
                seriesAutologousFistula.ValueScaleType = ScaleType.Numerical;
                seriesAutologousFistula.LegendText = "自体内瘘";
                cclQualityMonitorIndicator.Series.Add(seriesAutologousFistula);

                Series seriesTempVenousCatheter = new Series("TempVenousCatheter", ViewType.Bar);
                seriesTempVenousCatheter.DataSource = dtSub;
                seriesTempVenousCatheter.ArgumentDataMember = "DATE_MONTH";
                seriesTempVenousCatheter.ArgumentScaleType = ScaleType.Qualitative;
                seriesTempVenousCatheter.ValueDataMembers.AddRange(new string[] { "TEMP_VENOUS_CATHETER" });
                seriesTempVenousCatheter.ValueScaleType = ScaleType.Numerical;
                seriesTempVenousCatheter.LegendText = "临时静脉留置导管";
                cclQualityMonitorIndicator.Series.Add(seriesTempVenousCatheter);

                Series seriesArtificialVessel = new Series("ArtificialVessel", ViewType.Bar);
                seriesArtificialVessel.DataSource = dtSub;
                seriesArtificialVessel.ArgumentDataMember = "DATE_MONTH";
                seriesArtificialVessel.ArgumentScaleType = ScaleType.Qualitative;
                seriesArtificialVessel.ValueDataMembers.AddRange(new string[] { "ARTIFICIAL_VESSEL" });
                seriesArtificialVessel.ValueScaleType = ScaleType.Numerical;
                seriesArtificialVessel.LegendText = "人造血管";
                cclQualityMonitorIndicator.Series.Add(seriesArtificialVessel);

                Series seriesDoubleVein = new Series("DoubleVein", ViewType.Bar);
                seriesDoubleVein.DataSource = dtSub;
                seriesDoubleVein.ArgumentDataMember = "DATE_MONTH";
                seriesDoubleVein.ArgumentScaleType = ScaleType.Qualitative;
                seriesDoubleVein.ValueDataMembers.AddRange(new string[] { "DOUBLE_VEIN" });
                seriesDoubleVein.ValueScaleType = ScaleType.Numerical;
                seriesDoubleVein.LegendText = "双静脉";
                cclQualityMonitorIndicator.Series.Add(seriesDoubleVein);

                Series seriesHighAvf = new Series("HighAvf", ViewType.Bar);
                seriesHighAvf.DataSource = dtSub;
                seriesHighAvf.ArgumentDataMember = "DATE_MONTH";
                seriesHighAvf.ArgumentScaleType = ScaleType.Qualitative;
                seriesHighAvf.ValueDataMembers.AddRange(new string[] { "HIGH_AVF" });
                seriesHighAvf.ValueScaleType = ScaleType.Numerical;
                seriesHighAvf.LegendText = "高位动静脉内瘘";
                cclQualityMonitorIndicator.Series.Add(seriesHighAvf);

                Series seriesJugularVenousCatheter = new Series("JugularVenousCatheter", ViewType.Bar);
                seriesJugularVenousCatheter.DataSource = dtSub;
                seriesJugularVenousCatheter.ArgumentDataMember = "DATE_MONTH";
                seriesJugularVenousCatheter.ArgumentScaleType = ScaleType.Qualitative;
                seriesJugularVenousCatheter.ValueDataMembers.AddRange(new string[] { "JUGULAR_VENOUS_CATHETER" });
                seriesJugularVenousCatheter.ValueScaleType = ScaleType.Numerical;
                seriesJugularVenousCatheter.LegendText = "颈内静脉置管";
                cclQualityMonitorIndicator.Series.Add(seriesJugularVenousCatheter);

                Series seriesSubclavianVenousCatheter = new Series("SubclavianVenousCatheter", ViewType.Bar);
                seriesSubclavianVenousCatheter.DataSource = dtSub;
                seriesSubclavianVenousCatheter.ArgumentDataMember = "DATE_MONTH";
                seriesSubclavianVenousCatheter.ArgumentScaleType = ScaleType.Qualitative;
                seriesSubclavianVenousCatheter.ValueDataMembers.AddRange(new string[] { "SUBCLAVIAN_VENOUS_CATHETER" });
                seriesSubclavianVenousCatheter.ValueScaleType = ScaleType.Numerical;
                seriesSubclavianVenousCatheter.LegendText = "锁骨下静脉置管";
                cclQualityMonitorIndicator.Series.Add(seriesSubclavianVenousCatheter);

                Series seriesFemoralVenousCatheter = new Series("FemoralVenousCatheter", ViewType.Bar);
                seriesFemoralVenousCatheter.DataSource = dtSub;
                seriesFemoralVenousCatheter.ArgumentDataMember = "DATE_MONTH";
                seriesFemoralVenousCatheter.ArgumentScaleType = ScaleType.Qualitative;
                seriesFemoralVenousCatheter.ValueDataMembers.AddRange(new string[] { "FEMORAL_VENOUS_CATHETER" });
                seriesFemoralVenousCatheter.ValueScaleType = ScaleType.Numerical;
                seriesFemoralVenousCatheter.LegendText = "股静脉置管";
                cclQualityMonitorIndicator.Series.Add(seriesFemoralVenousCatheter);

                Series seriesPressureControl = new Series("PressureControl", ViewType.Bar);
                seriesPressureControl.DataSource = dtSub;
                seriesPressureControl.ArgumentDataMember = "DATE_MONTH";
                seriesPressureControl.ArgumentScaleType = ScaleType.Qualitative;
                seriesPressureControl.ValueDataMembers.AddRange(new string[] { "PRESSURE_CONTROL" });
                seriesPressureControl.ValueScaleType = ScaleType.Numerical;
                seriesPressureControl.LegendText = "血压控制";
                cclQualityMonitorIndicator.Series.Add(seriesPressureControl);

                Series seriesTimeLess8 = new Series("TimeLess8", ViewType.Bar);
                seriesTimeLess8.DataSource = dtSub;
                seriesTimeLess8.ArgumentDataMember = "DATE_MONTH";
                seriesTimeLess8.ArgumentScaleType = ScaleType.Qualitative;
                seriesTimeLess8.ValueDataMembers.AddRange(new string[] { "TIME_LESS_8" });
                seriesTimeLess8.ValueScaleType = ScaleType.Numerical;
                seriesTimeLess8.LegendText = "平均透析时间<8h";
                cclQualityMonitorIndicator.Series.Add(seriesTimeLess8);

                Series seriesTime8_9 = new Series("Time8_9", ViewType.Bar);
                seriesTime8_9.DataSource = dtSub;
                seriesTime8_9.ArgumentDataMember = "DATE_MONTH";
                seriesTime8_9.ArgumentScaleType = ScaleType.Qualitative;
                seriesTime8_9.ValueDataMembers.AddRange(new string[] { "TIME_8_9" });
                seriesTime8_9.ValueScaleType = ScaleType.Numerical;
                seriesTime8_9.LegendText = "平均透析时间8~9h";
                cclQualityMonitorIndicator.Series.Add(seriesTime8_9);

                Series seriesTime9_10 = new Series("Time9_10", ViewType.Bar);
                seriesTime9_10.DataSource = dtSub;
                seriesTime9_10.ArgumentDataMember = "DATE_MONTH";
                seriesTime9_10.ArgumentScaleType = ScaleType.Qualitative;
                seriesTime9_10.ValueDataMembers.AddRange(new string[] { "TIME_9_10" });
                seriesTime9_10.ValueScaleType = ScaleType.Numerical;
                seriesTime9_10.LegendText = "平均透析时间9~10h";
                cclQualityMonitorIndicator.Series.Add(seriesTime9_10);

                Series seriesTime10_11 = new Series("Time10_11", ViewType.Bar);
                seriesTime10_11.DataSource = dtSub;
                seriesTime10_11.ArgumentDataMember = "DATE_MONTH";
                seriesTime10_11.ArgumentScaleType = ScaleType.Qualitative;
                seriesTime10_11.ValueDataMembers.AddRange(new string[] { "TIME_10_11" });
                seriesTime10_11.ValueScaleType = ScaleType.Numerical;
                seriesTime10_11.LegendText = "平均透析时间10~11h";
                cclQualityMonitorIndicator.Series.Add(seriesTime10_11);

                Series seriesTime11_12 = new Series("Time11_12", ViewType.Bar);
                seriesTime11_12.DataSource = dtSub;
                seriesTime11_12.ArgumentDataMember = "DATE_MONTH";
                seriesTime11_12.ArgumentScaleType = ScaleType.Qualitative;
                seriesTime11_12.ValueDataMembers.AddRange(new string[] { "TIME_11_12" });
                seriesTime11_12.ValueScaleType = ScaleType.Numerical;
                seriesTime11_12.LegendText = "平均透析时间11~12h";
                cclQualityMonitorIndicator.Series.Add(seriesTime11_12);

                Series seriesTimeMore12 = new Series("TimeMore12", ViewType.Bar);
                seriesTimeMore12.DataSource = dtSub;
                seriesTimeMore12.ArgumentDataMember = "DATE_MONTH";
                seriesTimeMore12.ArgumentScaleType = ScaleType.Qualitative;
                seriesTimeMore12.ValueDataMembers.AddRange(new string[] { "TIME_MORE_12" });
                seriesTimeMore12.ValueScaleType = ScaleType.Numerical;
                seriesTimeMore12.LegendText = "平均透析时间>12h";
                cclQualityMonitorIndicator.Series.Add(seriesTimeMore12);

                Series seriesComfort = new Series("Comfort", ViewType.Bar);
                seriesComfort.DataSource = dtSub;
                seriesComfort.ArgumentDataMember = "DATE_MONTH";
                seriesComfort.ArgumentScaleType = ScaleType.Qualitative;
                seriesComfort.ValueDataMembers.AddRange(new string[] { "COMFORT" });
                seriesComfort.ValueScaleType = ScaleType.Numerical;
                seriesComfort.LegendText = "舒适度评价-舒适";
                cclQualityMonitorIndicator.Series.Add(seriesComfort);

                Series seriesMildDiscomfort = new Series("MildDiscomfort", ViewType.Bar);
                seriesMildDiscomfort.DataSource = dtSub;
                seriesMildDiscomfort.ArgumentDataMember = "DATE_MONTH";
                seriesMildDiscomfort.ArgumentScaleType = ScaleType.Qualitative;
                seriesMildDiscomfort.ValueDataMembers.AddRange(new string[] { "MILD_DISCOMFORT" });
                seriesMildDiscomfort.ValueScaleType = ScaleType.Numerical;
                seriesMildDiscomfort.LegendText = "舒适度评价-轻度不适";
                cclQualityMonitorIndicator.Series.Add(seriesMildDiscomfort);

                Series seriesSevereDiscomfort = new Series("SevereDiscomfort", ViewType.Bar);
                seriesSevereDiscomfort.DataSource = dtSub;
                seriesSevereDiscomfort.ArgumentDataMember = "DATE_MONTH";
                seriesSevereDiscomfort.ArgumentScaleType = ScaleType.Qualitative;
                seriesSevereDiscomfort.ValueDataMembers.AddRange(new string[] { "SEVERE_DISCOMFORT" });
                seriesSevereDiscomfort.ValueScaleType = ScaleType.Numerical;
                seriesSevereDiscomfort.LegendText = "舒适度评价-重度不适";
                cclQualityMonitorIndicator.Series.Add(seriesSevereDiscomfort);

                Series seriesPeritonealDialysis = new Series("PeritonealDialysis", ViewType.Bar);
                seriesPeritonealDialysis.DataSource = dtSub;
                seriesPeritonealDialysis.ArgumentDataMember = "DATE_MONTH";
                seriesPeritonealDialysis.ArgumentScaleType = ScaleType.Qualitative;
                seriesPeritonealDialysis.ValueDataMembers.AddRange(new string[] { "PERITONEAL_DIALYSIS" });
                seriesPeritonealDialysis.ValueScaleType = ScaleType.Numerical;
                seriesPeritonealDialysis.LegendText = "腹膜透析";
                cclQualityMonitorIndicator.Series.Add(seriesPeritonealDialysis);

                ((XYDiagram)this.cclQualityMonitorIndicator.Diagram).AxisY.Title.Text = "人数";
                ((XYDiagram)this.cclQualityMonitorIndicator.Diagram).AxisY.Title.Font = new Font("Tahoma", 10);
                ((XYDiagram)this.cclQualityMonitorIndicator.Diagram).AxisY.Title.TextColor = Color.Red;
                ((XYDiagram)this.cclQualityMonitorIndicator.Diagram).AxisY.Title.Visible = true;

                ChartTitle title = new ChartTitle();
                title.Text = "维持性血透患者质量监测指标统计";
                title.Font = new Font("Tahoma", 12);
                title.Dock = ChartTitleDockStyle.Top;
                this.cclQualityMonitorIndicator.Titles.Clear();
                this.cclQualityMonitorIndicator.Titles.Add(title);

                this.cclQualityMonitorIndicator.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
                this.cclQualityMonitorIndicator.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
                this.cclQualityMonitorIndicator.Legend.Direction = LegendDirection.TopToBottom;
            }
        }

        #endregion
    }
}
