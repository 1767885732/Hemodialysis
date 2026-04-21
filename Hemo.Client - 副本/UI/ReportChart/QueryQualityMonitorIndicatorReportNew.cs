/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:最新质量监测指标统计查询类
 * 创建标识:吕志强-2017年7月24日
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
using DevExpress.XtraSplashScreen;
using Hemo.Client.Controls;
using System.Threading;
using Hemo.Client.UI.Hemodialysis;
using DevExpress.XtraBars.Docking2010.Customization;

namespace Hemo.Client.UI.ReportChart
{
    [ToolboxItem(true)]
    public partial class QueryQualityMonitorIndicatorReportNew : ViewBase
    {
        #region 成员变量

        private ILab labService = ServiceManager.Instance.LabService;

        private DataTable dtResult = null;

        private DataTable dtReport = null;

        #endregion

        #region 构造函数

        public QueryQualityMonitorIndicatorReportNew()
        {
            InitializeComponent();
            this.ctlYearQuery.IsRptVisble = true;
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
            this.ctlYearQuery.RptInstruction += new EventHandler(ctlInstruction_Click);

            //SetGroupLayout();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlYearQuery_QueryEvent(object sender, EventArgs e)
        {
            ShowMessage();
            Query();
            LoadReportChart();
            HideMessage();
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlYearQuery_ExpExcelEvent(object sender, EventArgs e)
        {
            this.ctlYearQuery.ExportExcel(this.gcQualityTest, "质量检测指标统计");
            this.ctlYearQuery.ExportExcel(this.gcBloodAccess, "血管通路指标统计");
            this.ctlYearQuery.ExportExcel(this.gcDialysisTime, "透析时间指标统计");
            this.ctlYearQuery.ExportExcel(this.gcEvaluate, "舒适度评价及其它指标统计");
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

            QualityMonitorIndicatorReportNew qualityMonitorIndicatorReport = new QualityMonitorIndicatorReportNew(dtReport, title);
            ReportPrintTool pt = new ReportPrintTool(qualityMonitorIndicatorReport);
            pt.ShowPreviewDialog();
        }

        private void ctlInstruction_Click(object sender, EventArgs e)
        {
            FlyoutDialog.Show(this.FindForm(), new QualityControlRptInstruct("监测指标"));
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="kind"></param>
        void InsertMedReportData(string kind)
        {
            if (kind == "日期")
            {
                int months = (this.ctlYearQuery.ToDate.Year - this.ctlYearQuery.ToDate.Year) * 12 + (this.ctlYearQuery.ToDate.Month - this.ctlYearQuery.ToDate.Month);
                for (int i = 0; i <= months; i++)
                {
                    this.labService.Save_MED_REPORT_DATA(this.ctlYearQuery.FromDate.AddMonths(i));
                }
            }
            else if (kind =="年份")
            {
                DateTime t = Convert.ToDateTime(this.ctlYearQuery.Year + "-01-01");
                for (int i = 0; i < 12; i++)
                {
                    this.labService.Save_MED_REPORT_DATA(t.AddMonths(i));
                }
            }
        }

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
                if (this.ctlYearQuery.ToDate < this.ctlYearQuery.FromDate)
                {
                    MessageBox.Show("结束日期不能小于开始日期！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                InsertMedReportData("日期");
                dtReport = labService.GetQualityMonitorIndicatorByDate(this.ctlYearQuery.FromDate, this.ctlYearQuery.ToDate);
            }
            else
            {
                InsertMedReportData("年份");
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

            this.gcQualityTest.DataSource = dtReport;
            this.gcBloodAccess.DataSource = dtReport;
            this.gcDialysisTime.DataSource = dtReport;
            this.gcEvaluate.DataSource = dtReport;
            this.gvQualityTest.BestFitColumns();
            this.gvBloodAccess.BestFitColumns();
            this.gvDialysisTime.BestFitColumns();
            this.gvEvaluate.BestFitColumns();
        }

        /// <summary>
        /// 加载报表图样
        /// </summary>
        private void LoadReportChart()
        {
            this.cclQualityTest.Series.Clear();
            this.cclBloodAccess.Series.Clear();
            this.cclDialysisTime.Series.Clear();
            this.cclEvaluate.Series.Clear();

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
                cclQualityTest.Series.Add(seriesUreaRemove);

                Series seriesRenalAnemia = new Series("RenalAnemia", ViewType.Bar);
                seriesRenalAnemia.DataSource = dtSub;
                seriesRenalAnemia.ArgumentDataMember = "DATE_MONTH";
                seriesRenalAnemia.ArgumentScaleType = ScaleType.Qualitative;
                seriesRenalAnemia.ValueDataMembers.AddRange(new string[] { "RENAL_ANEMIA" });
                seriesRenalAnemia.ValueScaleType = ScaleType.Numerical;
                seriesRenalAnemia.LegendText = "纠正肾性贫血";
                cclQualityTest.Series.Add(seriesRenalAnemia);

                Series seriesCa_P_Metabolism = new Series("Ca_P_Metabolism", ViewType.Bar);
                seriesCa_P_Metabolism.DataSource = dtSub;
                seriesCa_P_Metabolism.ArgumentDataMember = "DATE_MONTH";
                seriesCa_P_Metabolism.ArgumentScaleType = ScaleType.Qualitative;
                seriesCa_P_Metabolism.ValueDataMembers.AddRange(new string[] { "CA_P_METABOLISM" });
                seriesCa_P_Metabolism.ValueScaleType = ScaleType.Numerical;
                seriesCa_P_Metabolism.LegendText = "钙磷代谢";
                cclQualityTest.Series.Add(seriesCa_P_Metabolism);

                Series seriesSecondaryShpt = new Series("SecondaryShpt", ViewType.Bar);
                seriesSecondaryShpt.DataSource = dtSub;
                seriesSecondaryShpt.ArgumentDataMember = "DATE_MONTH";
                seriesSecondaryShpt.ArgumentScaleType = ScaleType.Qualitative;
                seriesSecondaryShpt.ValueDataMembers.AddRange(new string[] { "SECONDARY_SHPT" });
                seriesSecondaryShpt.ValueScaleType = ScaleType.Numerical;
                seriesSecondaryShpt.LegendText = "甲状旁腺功能亢进";
                cclQualityTest.Series.Add(seriesSecondaryShpt);

                Series seriesPressureControl = new Series("PressureControl", ViewType.Bar);
                seriesPressureControl.DataSource = dtSub;
                seriesPressureControl.ArgumentDataMember = "DATE_MONTH";
                seriesPressureControl.ArgumentScaleType = ScaleType.Qualitative;
                seriesPressureControl.ValueDataMembers.AddRange(new string[] { "PRESSURE_CONTROL" });
                seriesPressureControl.ValueScaleType = ScaleType.Numerical;
                seriesPressureControl.LegendText = "血压控制";
                cclQualityTest.Series.Add(seriesPressureControl);

                //Series seriesVenousCatheter = new Series("VenousCatheter", ViewType.Bar);
                //seriesVenousCatheter.DataSource = dtSub;
                //seriesVenousCatheter.ArgumentDataMember = "DATE_MONTH";
                //seriesVenousCatheter.ArgumentScaleType = ScaleType.Qualitative;
                //seriesVenousCatheter.ValueDataMembers.AddRange(new string[] { "VENOUS_CATHETER" });
                //seriesVenousCatheter.ValueScaleType = ScaleType.Numerical;
                //seriesVenousCatheter.LegendText = "静脉留置导管";
                //cclBloodAccess.Series.Add(seriesVenousCatheter);

                //Series seriesAutologousFistula = new Series("AutologousFistula", ViewType.Bar);
                //seriesAutologousFistula.DataSource = dtSub;
                //seriesAutologousFistula.ArgumentDataMember = "DATE_MONTH";
                //seriesAutologousFistula.ArgumentScaleType = ScaleType.Qualitative;
                //seriesAutologousFistula.ValueDataMembers.AddRange(new string[] { "AUTOLOGOUS_FISTULA" });
                //seriesAutologousFistula.ValueScaleType = ScaleType.Numerical;
                //seriesAutologousFistula.LegendText = "自体内瘘";
                //cclBloodAccess.Series.Add(seriesAutologousFistula);

                //Series seriesTempVenousCatheter = new Series("TempVenousCatheter", ViewType.Bar);
                //seriesTempVenousCatheter.DataSource = dtSub;
                //seriesTempVenousCatheter.ArgumentDataMember = "DATE_MONTH";
                //seriesTempVenousCatheter.ArgumentScaleType = ScaleType.Qualitative;
                //seriesTempVenousCatheter.ValueDataMembers.AddRange(new string[] { "TEMP_VENOUS_CATHETER" });
                //seriesTempVenousCatheter.ValueScaleType = ScaleType.Numerical;
                //seriesTempVenousCatheter.LegendText = "临时静脉留置导管";
                //cclBloodAccess.Series.Add(seriesTempVenousCatheter);

                //Series seriesArtificialVessel = new Series("ArtificialVessel", ViewType.Bar);
                //seriesArtificialVessel.DataSource = dtSub;
                //seriesArtificialVessel.ArgumentDataMember = "DATE_MONTH";
                //seriesArtificialVessel.ArgumentScaleType = ScaleType.Qualitative;
                //seriesArtificialVessel.ValueDataMembers.AddRange(new string[] { "ARTIFICIAL_VESSEL" });
                //seriesArtificialVessel.ValueScaleType = ScaleType.Numerical;
                //seriesArtificialVessel.LegendText = "人造血管";
                //cclBloodAccess.Series.Add(seriesArtificialVessel);

                //Series seriesDoubleVein = new Series("DoubleVein", ViewType.Bar);
                //seriesDoubleVein.DataSource = dtSub;
                //seriesDoubleVein.ArgumentDataMember = "DATE_MONTH";
                //seriesDoubleVein.ArgumentScaleType = ScaleType.Qualitative;
                //seriesDoubleVein.ValueDataMembers.AddRange(new string[] { "DOUBLE_VEIN" });
                //seriesDoubleVein.ValueScaleType = ScaleType.Numerical;
                //seriesDoubleVein.LegendText = "双静脉";
                //cclBloodAccess.Series.Add(seriesDoubleVein);

                //Series seriesHighAvf = new Series("HighAvf", ViewType.Bar);
                //seriesHighAvf.DataSource = dtSub;
                //seriesHighAvf.ArgumentDataMember = "DATE_MONTH";
                //seriesHighAvf.ArgumentScaleType = ScaleType.Qualitative;
                //seriesHighAvf.ValueDataMembers.AddRange(new string[] { "HIGH_AVF" });
                //seriesHighAvf.ValueScaleType = ScaleType.Numerical;
                //seriesHighAvf.LegendText = "高位动静脉内瘘";
                //cclBloodAccess.Series.Add(seriesHighAvf);

                //Series seriesJugularVenousCatheter = new Series("JugularVenousCatheter", ViewType.Bar);
                //seriesJugularVenousCatheter.DataSource = dtSub;
                //seriesJugularVenousCatheter.ArgumentDataMember = "DATE_MONTH";
                //seriesJugularVenousCatheter.ArgumentScaleType = ScaleType.Qualitative;
                //seriesJugularVenousCatheter.ValueDataMembers.AddRange(new string[] { "JUGULAR_VENOUS_CATHETER" });
                //seriesJugularVenousCatheter.ValueScaleType = ScaleType.Numerical;
                //seriesJugularVenousCatheter.LegendText = "颈内静脉置管";
                //cclBloodAccess.Series.Add(seriesJugularVenousCatheter);

                //Series seriesSubclavianVenousCatheter = new Series("SubclavianVenousCatheter", ViewType.Bar);
                //seriesSubclavianVenousCatheter.DataSource = dtSub;
                //seriesSubclavianVenousCatheter.ArgumentDataMember = "DATE_MONTH";
                //seriesSubclavianVenousCatheter.ArgumentScaleType = ScaleType.Qualitative;
                //seriesSubclavianVenousCatheter.ValueDataMembers.AddRange(new string[] { "SUBCLAVIAN_VENOUS_CATHETER" });
                //seriesSubclavianVenousCatheter.ValueScaleType = ScaleType.Numerical;
                //seriesSubclavianVenousCatheter.LegendText = "锁骨下静脉置管";
                //cclBloodAccess.Series.Add(seriesSubclavianVenousCatheter);

                //Series seriesFemoralVenousCatheter = new Series("FemoralVenousCatheter", ViewType.Bar);
                //seriesFemoralVenousCatheter.DataSource = dtSub;
                //seriesFemoralVenousCatheter.ArgumentDataMember = "DATE_MONTH";
                //seriesFemoralVenousCatheter.ArgumentScaleType = ScaleType.Qualitative;
                //seriesFemoralVenousCatheter.ValueDataMembers.AddRange(new string[] { "FEMORAL_VENOUS_CATHETER" });
                //seriesFemoralVenousCatheter.ValueScaleType = ScaleType.Numerical;
                //seriesFemoralVenousCatheter.LegendText = "股静脉置管";
                //cclBloodAccess.Series.Add(seriesFemoralVenousCatheter);


                Series seriesHighAvf = new Series("动静脉内瘘术", ViewType.Bar);
                seriesHighAvf.DataSource = dtSub;
                seriesHighAvf.ArgumentDataMember = "DATE_MONTH";
                seriesHighAvf.ArgumentScaleType = ScaleType.Qualitative;
                seriesHighAvf.ValueDataMembers.AddRange(new string[] { "DJMNL" });
                seriesHighAvf.ValueScaleType = ScaleType.Numerical;
                seriesHighAvf.LegendText = "动静脉内瘘术";
                cclBloodAccess.Series.Add(seriesHighAvf);

                Series seriesJugularVenousCatheter = new Series("导管深静脉置入术", ViewType.Bar);
                seriesJugularVenousCatheter.DataSource = dtSub;
                seriesJugularVenousCatheter.ArgumentDataMember = "DATE_MONTH";
                seriesJugularVenousCatheter.ArgumentScaleType = ScaleType.Qualitative;
                seriesJugularVenousCatheter.ValueDataMembers.AddRange(new string[] { "DGSJM" });
                seriesJugularVenousCatheter.ValueScaleType = ScaleType.Numerical;
                seriesJugularVenousCatheter.LegendText = "导管深静脉置入术";
                cclBloodAccess.Series.Add(seriesJugularVenousCatheter);

                Series seriesSubclavianVenousCatheter = new Series("动静脉人工血管术", ViewType.Bar);
                seriesSubclavianVenousCatheter.DataSource = dtSub;
                seriesSubclavianVenousCatheter.ArgumentDataMember = "DATE_MONTH";
                seriesSubclavianVenousCatheter.ArgumentScaleType = ScaleType.Qualitative;
                seriesSubclavianVenousCatheter.ValueDataMembers.AddRange(new string[] { "DJMRG" });
                seriesSubclavianVenousCatheter.ValueScaleType = ScaleType.Numerical;
                seriesSubclavianVenousCatheter.LegendText = "动静脉人工血管术";
                cclBloodAccess.Series.Add(seriesSubclavianVenousCatheter);

                Series seriesFemoralVenousCatheter = new Series("其他血管通路", ViewType.Bar);
                seriesFemoralVenousCatheter.DataSource = dtSub;
                seriesFemoralVenousCatheter.ArgumentDataMember = "DATE_MONTH";
                seriesFemoralVenousCatheter.ArgumentScaleType = ScaleType.Qualitative;
                seriesFemoralVenousCatheter.ValueDataMembers.AddRange(new string[] { "QTXGTL" });
                seriesFemoralVenousCatheter.ValueScaleType = ScaleType.Numerical;
                seriesFemoralVenousCatheter.LegendText = "其他血管通路";
                cclBloodAccess.Series.Add(seriesFemoralVenousCatheter);

                Series seriesTimeLess8 = new Series("TimeLess8", ViewType.Bar);
                seriesTimeLess8.DataSource = dtSub;
                seriesTimeLess8.ArgumentDataMember = "DATE_MONTH";
                seriesTimeLess8.ArgumentScaleType = ScaleType.Qualitative;
                seriesTimeLess8.ValueDataMembers.AddRange(new string[] { "TIME_LESS_8" });
                seriesTimeLess8.ValueScaleType = ScaleType.Numerical;
                seriesTimeLess8.LegendText = "平均透析时间<8h";
                cclDialysisTime.Series.Add(seriesTimeLess8);

                Series seriesTime8_9 = new Series("Time8_9", ViewType.Bar);
                seriesTime8_9.DataSource = dtSub;
                seriesTime8_9.ArgumentDataMember = "DATE_MONTH";
                seriesTime8_9.ArgumentScaleType = ScaleType.Qualitative;
                seriesTime8_9.ValueDataMembers.AddRange(new string[] { "TIME_8_9" });
                seriesTime8_9.ValueScaleType = ScaleType.Numerical;
                seriesTime8_9.LegendText = "平均透析时间8~9h";
                cclDialysisTime.Series.Add(seriesTime8_9);

                Series seriesTime9_10 = new Series("Time9_10", ViewType.Bar);
                seriesTime9_10.DataSource = dtSub;
                seriesTime9_10.ArgumentDataMember = "DATE_MONTH";
                seriesTime9_10.ArgumentScaleType = ScaleType.Qualitative;
                seriesTime9_10.ValueDataMembers.AddRange(new string[] { "TIME_9_10" });
                seriesTime9_10.ValueScaleType = ScaleType.Numerical;
                seriesTime9_10.LegendText = "平均透析时间9~10h";
                cclDialysisTime.Series.Add(seriesTime9_10);

                Series seriesTime10_11 = new Series("Time10_11", ViewType.Bar);
                seriesTime10_11.DataSource = dtSub;
                seriesTime10_11.ArgumentDataMember = "DATE_MONTH";
                seriesTime10_11.ArgumentScaleType = ScaleType.Qualitative;
                seriesTime10_11.ValueDataMembers.AddRange(new string[] { "TIME_10_11" });
                seriesTime10_11.ValueScaleType = ScaleType.Numerical;
                seriesTime10_11.LegendText = "平均透析时间10~11h";
                cclDialysisTime.Series.Add(seriesTime10_11);

                Series seriesTime11_12 = new Series("Time11_12", ViewType.Bar);
                seriesTime11_12.DataSource = dtSub;
                seriesTime11_12.ArgumentDataMember = "DATE_MONTH";
                seriesTime11_12.ArgumentScaleType = ScaleType.Qualitative;
                seriesTime11_12.ValueDataMembers.AddRange(new string[] { "TIME_11_12" });
                seriesTime11_12.ValueScaleType = ScaleType.Numerical;
                seriesTime11_12.LegendText = "平均透析时间11~12h";
                cclDialysisTime.Series.Add(seriesTime11_12);

                Series seriesTimeMore12 = new Series("TimeMore12", ViewType.Bar);
                seriesTimeMore12.DataSource = dtSub;
                seriesTimeMore12.ArgumentDataMember = "DATE_MONTH";
                seriesTimeMore12.ArgumentScaleType = ScaleType.Qualitative;
                seriesTimeMore12.ValueDataMembers.AddRange(new string[] { "TIME_MORE_12" });
                seriesTimeMore12.ValueScaleType = ScaleType.Numerical;
                seriesTimeMore12.LegendText = "平均透析时间>12h";
                cclDialysisTime.Series.Add(seriesTimeMore12);

                Series seriesComfort = new Series("Comfort", ViewType.Bar);
                seriesComfort.DataSource = dtSub;
                seriesComfort.ArgumentDataMember = "DATE_MONTH";
                seriesComfort.ArgumentScaleType = ScaleType.Qualitative;
                seriesComfort.ValueDataMembers.AddRange(new string[] { "COMFORT" });
                seriesComfort.ValueScaleType = ScaleType.Numerical;
                seriesComfort.LegendText = "舒适度评价-舒适";
                cclEvaluate.Series.Add(seriesComfort);

                Series seriesMildDiscomfort = new Series("MildDiscomfort", ViewType.Bar);
                seriesMildDiscomfort.DataSource = dtSub;
                seriesMildDiscomfort.ArgumentDataMember = "DATE_MONTH";
                seriesMildDiscomfort.ArgumentScaleType = ScaleType.Qualitative;
                seriesMildDiscomfort.ValueDataMembers.AddRange(new string[] { "MILD_DISCOMFORT" });
                seriesMildDiscomfort.ValueScaleType = ScaleType.Numerical;
                seriesMildDiscomfort.LegendText = "舒适度评价-轻度不适";
                cclEvaluate.Series.Add(seriesMildDiscomfort);

                Series seriesSevereDiscomfort = new Series("SevereDiscomfort", ViewType.Bar);
                seriesSevereDiscomfort.DataSource = dtSub;
                seriesSevereDiscomfort.ArgumentDataMember = "DATE_MONTH";
                seriesSevereDiscomfort.ArgumentScaleType = ScaleType.Qualitative;
                seriesSevereDiscomfort.ValueDataMembers.AddRange(new string[] { "SEVERE_DISCOMFORT" });
                seriesSevereDiscomfort.ValueScaleType = ScaleType.Numerical;
                seriesSevereDiscomfort.LegendText = "舒适度评价-重度不适";
                cclEvaluate.Series.Add(seriesSevereDiscomfort);

                Series seriesPeritonealDialysis = new Series("PeritonealDialysis", ViewType.Bar);
                seriesPeritonealDialysis.DataSource = dtSub;
                seriesPeritonealDialysis.ArgumentDataMember = "DATE_MONTH";
                seriesPeritonealDialysis.ArgumentScaleType = ScaleType.Qualitative;
                seriesPeritonealDialysis.ValueDataMembers.AddRange(new string[] { "PERITONEAL_DIALYSIS" });
                seriesPeritonealDialysis.ValueScaleType = ScaleType.Numerical;
                seriesPeritonealDialysis.LegendText = "腹膜透析";
                cclEvaluate.Series.Add(seriesPeritonealDialysis);

                SetChartSurface(this.cclQualityTest, "人数", "质量检测指标统计");
                SetChartSurface(this.cclBloodAccess, "人数", "血管通路指标统计");
                SetChartSurface(this.cclDialysisTime, "人数", "透析时间指标统计");
                SetChartSurface(this.cclEvaluate, "人数", "舒适度评价及其它指标统计");
            }
        }

        /// <summary>
        /// 设置GroupControl布局
        /// </summary>
        private void SetGroupLayout()
        {
            int tpGridWidth = this.tpQualityMonitorIndicatorGrid.Width;
            tpGridWidth -= 10;

            this.groupControl1.Location = new Point(3, 3);
            this.groupControl1.Width = tpGridWidth / 2;
            this.groupControl2.Location = new Point(3 + this.groupControl1.Width + 4, 3);
            this.groupControl2.Width = tpGridWidth / 2;
            this.groupControl3.Location = new Point(3, 3 + this.groupControl1.Height + 4);
            this.groupControl3.Width = tpGridWidth / 2;
            this.groupControl4.Location = new Point(3 + this.groupControl3.Width + 4, 3 + this.groupControl2.Height + 4);
            this.groupControl4.Width = tpGridWidth / 2;

            int tpChartWidth = this.tpQualityMonitorIndicatorChart.Width;
            tpChartWidth -= 10;

            this.groupControl5.Location = new Point(3, 3);
            this.groupControl5.Width = tpChartWidth / 2;
            this.groupControl6.Location = new Point(3 + this.groupControl5.Width + 4, 3);
            this.groupControl6.Width = tpChartWidth / 2;
            this.groupControl7.Location = new Point(3, 3 + this.groupControl5.Height + 4);
            this.groupControl7.Width = tpChartWidth / 2;
            this.groupControl8.Location = new Point(3 + this.groupControl7.Width + 4, 3 + this.groupControl6.Height + 4);
            this.groupControl8.Width = tpChartWidth / 2;
        }

        /// <summary>
        /// 设置报表外观
        /// </summary>
        /// <param name="chart"></param>
        /// <param name="text"></param>
        /// <param name="title"></param>
        private void SetChartSurface(ChartControl chart, string text, string title)
        {
            ((XYDiagram)chart.Diagram).AxisY.Title.Text = text;
            ((XYDiagram)chart.Diagram).AxisY.Title.Font = new Font("Tahoma", 10);
            ((XYDiagram)chart.Diagram).AxisY.Title.TextColor = Color.Red;
            ((XYDiagram)chart.Diagram).AxisY.Title.Visible = true;

            ChartTitle ctTitle = new ChartTitle();
            ctTitle.Text = title;
            ctTitle.Font = new Font("Tahoma", 12);
            ctTitle.Dock = ChartTitleDockStyle.Top;
            chart.Titles.Clear();
            chart.Titles.Add(ctTitle);

            chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
            chart.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
            chart.Legend.Direction = LegendDirection.TopToBottom;
        }

        public void SetShowInOthers()
        {
            this.ctlYearQuery.Visible = false;
            this.tpQualityMonitorIndicatorGrid.PageVisible = false;
            this.tpQualityMonitorIndicatorChart.PageVisible = true;
            this.tcQualityMonitorIndicator.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            this.groupControl6.Visible = false;
            this.groupControl7.Visible = false;
            this.groupControl8.Visible = false;
            this.groupControl5.Visible = true;
            this.groupControl5.Dock = DockStyle.Fill;
            this.ctlYearQuery.InitMUIOperator();
            Query();
            LoadReportChart();
        }

        #region SplashScreenManager

        private SplashScreenManager _loadForm;
        /// <summary>
        /// 等待窗体管理对象
        /// </summary>
        protected SplashScreenManager LoadForm
        {
            get
            {
                if (_loadForm == null)
                {
                    this._loadForm = new SplashScreenManager(this.FindForm(), typeof(FrmWaitForm), true, true);
                    //this._loadForm.CloseWaitForm();.ClosingDelay = 0;
                }
                return _loadForm;
            }
        }
        /// <summary>
        /// 显示等待窗体
        /// </summary>
        public void ShowMessage()
        {
            bool flag = !this.LoadForm.IsSplashFormVisible;
            if (flag)
            {
                this.LoadForm.ShowWaitForm();
            }
        }
        /// <summary>
        /// 关闭等待窗体
        /// </summary>
        public void HideMessage()
        {
            bool isSplashFormVisible = this.LoadForm.IsSplashFormVisible;
            if (isSplashFormVisible)
            {
                this.LoadForm.CloseWaitForm();
            }
        }

        #endregion

        #endregion
    }
}
