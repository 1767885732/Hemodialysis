/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:患者治疗统计查询类
 * 创建标识:吕志强-2017年2月28日
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

namespace Hemo.Client.UI.ReportChart
{
    public partial class QueryHemoCureReport : ViewBase
    {
        #region 成员变量

        IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        private DataTable dtResult = null;

        private DataTable dtReport = null;

        #endregion

        #region 构造函数

        public QueryHemoCureReport()
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
        private void QueryHemoCureReport_Load(object sender, EventArgs e)
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
            this.ctlYearQuery.ExportExcel(this.gcHemoCure, "血透治疗例数统计");
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlYearQuery_PrintEvent(object sender, EventArgs e)
        {
            this.ctlYearQuery.Print(this.gcHemoCure);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            int sumHD = 0;
            int sumHDF = 0;
            int sumHF = 0;
            int sumHP = 0;
            int sumHD_HP = 0;

            if (ctlYearQuery.SelectedTab == "1")
            {
                dtResult = hemoService.GetHemoCureCountList(this.ctlYearQuery.Year);

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
                    sumHD += int.Parse(row["HD_COUNT"].ToString());
                    sumHDF += int.Parse(row["HDF_COUNT"].ToString());
                    sumHF += int.Parse(row["HF_COUNT"].ToString());
                    sumHP += int.Parse(row["HP_COUNT"].ToString());
                    sumHD_HP += int.Parse(row["HD_HP_COUNT"].ToString());
                });

                DataRow rowSum = dtReport.NewRow();
                rowSum["DATE_MONTH"] = "合计";
                rowSum["HD_COUNT"] = sumHD;
                rowSum["HDF_COUNT"] = sumHDF;
                rowSum["HF_COUNT"] = sumHF;
                rowSum["HP_COUNT"] = sumHP;
                rowSum["HD_HP_COUNT"] = sumHD_HP;
                dtReport.Rows.Add(rowSum);
                this.gcHemoCure.DataSource = dtReport;
            }
            else
            {
                if (this.ctlYearQuery.FromDate != null)
                {
                    dtResult = hemoService.GetHemoCureCountList(this.ctlYearQuery.FromDate.Year.ToString());

                    if (dtResult != null && dtResult.Rows.Count > 0)
                    {
                        this.gcHemoCure.DataSource = dtResult;
                    }
                }
            }
        }

        /// <summary>
        /// 加载报表图样
        /// </summary>
        private void LoadReportChart()
        {
            this.cclHemoCure.Series.Clear();
            if (dtReport != null && dtReport.Rows.Count > 0)
            {
                DataTable dtSub = dtReport.Clone();
                dtReport.AsEnumerable().ToList().ForEach(row =>
                {
                    dtSub.ImportRow(row);
                });
                dtSub.Rows.RemoveAt(dtSub.Rows.Count - 1);

                Series seriesHD = new Series("HD", ViewType.Bar);
                seriesHD.DataSource = dtSub;
                seriesHD.ArgumentDataMember = "DATE_MONTH";
                seriesHD.ArgumentScaleType = ScaleType.Qualitative;
                seriesHD.ValueDataMembers.AddRange(new string[] { "HD_COUNT" });
                seriesHD.ValueScaleType = ScaleType.Numerical;
                seriesHD.LegendText = "HD";
                cclHemoCure.Series.Add(seriesHD);

                Series seriesHDF = new Series("HDF", ViewType.Bar);
                seriesHDF.DataSource = dtSub;
                seriesHDF.ArgumentDataMember = "DATE_MONTH";
                seriesHDF.ArgumentScaleType = ScaleType.Qualitative;
                seriesHDF.ValueDataMembers.AddRange(new string[] { "HDF_COUNT" });
                seriesHDF.ValueScaleType = ScaleType.Numerical;
                seriesHDF.LegendText = "HDF";
                cclHemoCure.Series.Add(seriesHDF);

                Series seriesHF = new Series("HF", ViewType.Bar);
                seriesHF.DataSource = dtSub;
                seriesHF.ArgumentDataMember = "DATE_MONTH";
                seriesHF.ArgumentScaleType = ScaleType.Qualitative;
                seriesHF.ValueDataMembers.AddRange(new string[] { "HF_COUNT" });
                seriesHF.ValueScaleType = ScaleType.Numerical;
                seriesHF.LegendText = "HF";
                cclHemoCure.Series.Add(seriesHF);

                Series seriesHP = new Series("HP", ViewType.Bar);
                seriesHP.DataSource = dtSub;
                seriesHP.ArgumentDataMember = "DATE_MONTH";
                seriesHP.ArgumentScaleType = ScaleType.Qualitative;
                seriesHP.ValueDataMembers.AddRange(new string[] { "HP_COUNT" });
                seriesHP.ValueScaleType = ScaleType.Numerical;
                seriesHP.LegendText = "HP";
                cclHemoCure.Series.Add(seriesHP);

                Series seriesHD_HP = new Series("HD|HP", ViewType.Bar);
                seriesHD_HP.DataSource = dtSub;
                seriesHD_HP.ArgumentDataMember = "DATE_MONTH";
                seriesHD_HP.ArgumentScaleType = ScaleType.Qualitative;
                seriesHD_HP.ValueDataMembers.AddRange(new string[] { "HD_HP_COUNT" });
                seriesHD_HP.ValueScaleType = ScaleType.Numerical;
                seriesHD_HP.LegendText = "HD|HP";
                cclHemoCure.Series.Add(seriesHD_HP);

                ((XYDiagram)this.cclHemoCure.Diagram).AxisY.Title.Text = "人数";
                ((XYDiagram)this.cclHemoCure.Diagram).AxisY.Title.Font = new Font("Tahoma", 10);
                ((XYDiagram)this.cclHemoCure.Diagram).AxisY.Title.TextColor = Color.Red;
                ((XYDiagram)this.cclHemoCure.Diagram).AxisY.Title.Visible = true;

                ChartTitle title = new ChartTitle();
                title.Text = "血透治疗例数统计";
                title.Font = new Font("Tahoma", 12);
                title.Dock = ChartTitleDockStyle.Top;
                this.cclHemoCure.Titles.Clear();
                this.cclHemoCure.Titles.Add(title);

                this.cclHemoCure.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
                this.cclHemoCure.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
                this.cclHemoCure.Legend.Direction = LegendDirection.TopToBottom;
            }
        }

        #endregion
    }
}
