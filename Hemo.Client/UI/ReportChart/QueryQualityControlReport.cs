/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:质量管理基础数据统计查询类
 * 创建标识:吕志强-2017年4月13日
 * 
 * 修改时间:2017年5月9日
 * 修改人:刘超
 * 修改描述:修改部分业务逻辑及界面
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
using DevExpress.XtraBars.Docking2010.Customization;
using Hemo.Client.UI.Hemodialysis;
using DevExpress.XtraSplashScreen;
using Hemo.Client.Controls;

namespace Hemo.Client.UI.ReportChart
{
    [ToolboxItem(true)]
    public partial class QueryQualityControlReport : ViewBase
    {
        #region 成员变量

        private ILab labService = ServiceManager.Instance.LabService;

        private DataTable dtResult = null;

        private DataTable dtReport = null;

        private DataTable dtInfo = null;

        #endregion

        #region 构造函数

        public QueryQualityControlReport()
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
            this.ctlYearQuery.ExportExcel(this.gcQualityControl, "质量管理基础数据统计");
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
            title += "质量管理基础数据统计";

            QualityControlReport qualityControlReport = new QualityControlReport(dtInfo, dtReport, title);
            ReportPrintTool pt = new ReportPrintTool(qualityControlReport);
            pt.ShowPreviewDialog();
        }

        private void ctlInstruction_Click(object sender, EventArgs e)
        {
            FlyoutDialog.Show(this.FindForm(), new QualityControlRptInstruct("质量管理"));
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
            else if (kind == "年份")
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
            int sumHemo = 0;
            int sumHD = 0;
            int sumHDF = 0;
            int sumHF = 0;
            int sumHP = 0;
            int sumHD_HP = 0;
            int sumDeath = 0;
            int sumDeathRate = 0;
            int sumSevereComplication = 0;
            int sumHBSAG = 0;
            int sumHBEAG = 0;
            int sumANTI_HCV = 0;
            int sumPeritonealDialysis = 0;
            int sumRenalTransplant = 0;

            if (this.ctlYearQuery.PageIndex == 0)
            {
                InsertMedReportData("日期");

                dtReport = labService.GetQualityControlBaseDataByDate(this.ctlYearQuery.FromDate, this.ctlYearQuery.ToDate);
            }
            else
            {
                InsertMedReportData("年份");
                dtResult = labService.GetQualityControlBaseDataByYear(this.ctlYearQuery.Year);
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
                sumHemo += int.Parse(row["HEMO_COUNT"].ToString());
                sumHD += int.Parse(row["HD_COUNT"].ToString());
                sumHDF += int.Parse(row["HDF_COUNT"].ToString());
                sumHF += int.Parse(row["HF_COUNT"].ToString());
                sumHP += int.Parse(row["HP_COUNT"].ToString());
                sumHD_HP += int.Parse(row["HD_HP_COUNT"].ToString());
                sumDeath += int.Parse(row["DEATH_COUNT"].ToString());
                sumDeathRate += int.Parse(row["DEATH_RATE"].ToString());
                sumSevereComplication += int.Parse(row["SEVERE_COMPLICATION"].ToString());
                sumHBSAG += int.Parse(row["HBSAG_POSITIVE"].ToString());
                sumHBEAG += int.Parse(row["HBEAG_POSITIVE"].ToString());
                sumANTI_HCV += int.Parse(row["ANTI_HCV_POSITIVE"].ToString());
                sumPeritonealDialysis += int.Parse(row["PERITONEAL_DIALYSIS"].ToString());
                sumRenalTransplant += int.Parse(row["RENAL_TRANSPLANT"].ToString());
            });

            DataRow rowSum = dtReport.NewRow();
            rowSum["DATE_MONTH"] = "合计";
            rowSum["HEMO_COUNT"] = sumHemo;
            rowSum["HD_COUNT"] = sumHD;
            rowSum["HDF_COUNT"] = sumHDF;
            rowSum["HF_COUNT"] = sumHF;
            rowSum["HP_COUNT"] = sumHP;
            rowSum["HD_HP_COUNT"] = sumHD_HP;
            rowSum["DEATH_COUNT"] = sumDeath;
            rowSum["DEATH_RATE"] = sumDeathRate;
            rowSum["SEVERE_COMPLICATION"] = sumSevereComplication;
            rowSum["HBSAG_POSITIVE"] = sumHBSAG;
            rowSum["HBEAG_POSITIVE"] = sumHBEAG;
            rowSum["ANTI_HCV_POSITIVE"] = sumANTI_HCV;
            rowSum["PERITONEAL_DIALYSIS"] = sumPeritonealDialysis;
            rowSum["RENAL_TRANSPLANT"] = sumRenalTransplant;
            dtReport.Rows.Add(rowSum);
            this.gcQualityControl.DataSource = dtReport;
            this.gvQualityControl.BestFitColumns();
            dtInfo = labService.GetMachineAndSpecialistCount();
            if (dtInfo != null)
            {
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    if (dtInfo.Rows[i]["JOB"].ToString() == "008")
                    {
                        this.lblMachineCount.Text = dtInfo.Rows[i]["DCOUNT"].ToString();
                    }
                    else if (dtInfo.Rows[i]["JOB"].ToString() == "009")
                    {
                        this.lblSpecialistsCount.Text = dtInfo.Rows[i]["DCOUNT"].ToString();
                    }
                    else if (dtInfo.Rows[i]["JOB"].ToString() == "010")
                    {
                        this.lblParamedicCount.Text = dtInfo.Rows[i]["DCOUNT"].ToString();
                    }
                }
            }

            //DataTable dtPatient = isConstant ? _hemodialysisService.GetHemoIdInLastWeekAndThreeMonthsByDate(this.beginTime.DateTime, this.endTime.DateTime) : _hemodialysisService.GetHemoIdByDate(this.beginTime.DateTime, this.endTime.DateTime);

        }

        /// <summary>
        /// 加载报表图样
        /// </summary>
        private void LoadReportChart()
        {
            this.cclQualityControl.Series.Clear();
            if (dtReport != null && dtReport.Rows.Count > 0)
            {
                DataTable dtSub = dtReport.Clone();
                dtReport.AsEnumerable().ToList().ForEach(row =>
                {
                    dtSub.ImportRow(row);
                });
                dtSub.Rows.RemoveAt(dtSub.Rows.Count - 1);

                Series seriesHemo = new Series("Hemo", ViewType.Bar);
                seriesHemo.DataSource = dtSub;
                seriesHemo.ArgumentDataMember = "DATE_MONTH";
                seriesHemo.ArgumentScaleType = ScaleType.Qualitative;
                seriesHemo.ValueDataMembers.AddRange(new string[] { "HEMO_COUNT" });
                seriesHemo.ValueScaleType = ScaleType.Numerical;
                seriesHemo.LegendText = "血液透析";
                cclQualityControl.Series.Add(seriesHemo);

                Series seriesHD = new Series("HD", ViewType.Bar);
                seriesHD.DataSource = dtSub;
                seriesHD.ArgumentDataMember = "DATE_MONTH";
                seriesHD.ArgumentScaleType = ScaleType.Qualitative;
                seriesHD.ValueDataMembers.AddRange(new string[] { "HD_COUNT" });
                seriesHD.ValueScaleType = ScaleType.Numerical;
                seriesHD.LegendText = "HD";
                cclQualityControl.Series.Add(seriesHD);

                Series seriesHDF = new Series("HDF", ViewType.Bar);
                seriesHDF.DataSource = dtSub;
                seriesHDF.ArgumentDataMember = "DATE_MONTH";
                seriesHDF.ArgumentScaleType = ScaleType.Qualitative;
                seriesHDF.ValueDataMembers.AddRange(new string[] { "HDF_COUNT" });
                seriesHDF.ValueScaleType = ScaleType.Numerical;
                seriesHDF.LegendText = "HDF";
                cclQualityControl.Series.Add(seriesHDF);

                Series seriesHF = new Series("HF", ViewType.Bar);
                seriesHF.DataSource = dtSub;
                seriesHF.ArgumentDataMember = "DATE_MONTH";
                seriesHF.ArgumentScaleType = ScaleType.Qualitative;
                seriesHF.ValueDataMembers.AddRange(new string[] { "HF_COUNT" });
                seriesHF.ValueScaleType = ScaleType.Numerical;
                seriesHF.LegendText = "HF";
                cclQualityControl.Series.Add(seriesHF);

                Series seriesHP = new Series("HP", ViewType.Bar);
                seriesHP.DataSource = dtSub;
                seriesHP.ArgumentDataMember = "DATE_MONTH";
                seriesHP.ArgumentScaleType = ScaleType.Qualitative;
                seriesHP.ValueDataMembers.AddRange(new string[] { "HP_COUNT" });
                seriesHP.ValueScaleType = ScaleType.Numerical;
                seriesHP.LegendText = "HP";
                cclQualityControl.Series.Add(seriesHP);

                Series seriesHD_HP = new Series("HD|HP", ViewType.Bar);
                seriesHD_HP.DataSource = dtSub;
                seriesHD_HP.ArgumentDataMember = "DATE_MONTH";
                seriesHD_HP.ArgumentScaleType = ScaleType.Qualitative;
                seriesHD_HP.ValueDataMembers.AddRange(new string[] { "HD_HP_COUNT" });
                seriesHD_HP.ValueScaleType = ScaleType.Numerical;
                seriesHD_HP.LegendText = "HD|HP";
                cclQualityControl.Series.Add(seriesHD_HP);

                Series seriesDeath = new Series("Death", ViewType.Bar);
                seriesDeath.DataSource = dtSub;
                seriesDeath.ArgumentDataMember = "DATE_MONTH";
                seriesDeath.ArgumentScaleType = ScaleType.Qualitative;
                seriesDeath.ValueDataMembers.AddRange(new string[] { "DEATH_COUNT" });
                seriesDeath.ValueScaleType = ScaleType.Numerical;
                seriesDeath.LegendText = "死亡";
                cclQualityControl.Series.Add(seriesDeath);

                //Series seriesDeathRate = new Series("DeathRate", ViewType.Bar);
                //seriesDeathRate.DataSource = dtSub;
                //seriesDeathRate.ArgumentDataMember = "DATE_MONTH";
                //seriesDeathRate.ArgumentScaleType = ScaleType.Qualitative;
                //seriesDeathRate.ValueDataMembers.AddRange(new string[] { "DEATH_RATE" });
                //seriesDeathRate.ValueScaleType = ScaleType.Numerical;
                //seriesDeathRate.LegendText = "死亡率";
                //cclQualityControl.Series.Add(seriesDeathRate);

                Series seriesSevereComplication = new Series("SevereComplication", ViewType.Bar);
                seriesSevereComplication.DataSource = dtSub;
                seriesSevereComplication.ArgumentDataMember = "DATE_MONTH";
                seriesSevereComplication.ArgumentScaleType = ScaleType.Qualitative;
                seriesSevereComplication.ValueDataMembers.AddRange(new string[] { "SEVERE_COMPLICATION" });
                seriesSevereComplication.ValueScaleType = ScaleType.Numerical;
                seriesSevereComplication.LegendText = "并发症";
                cclQualityControl.Series.Add(seriesSevereComplication);

                Series seriesHBSAG = new Series("HBSAG", ViewType.Bar);
                seriesHBSAG.DataSource = dtSub;
                seriesHBSAG.ArgumentDataMember = "DATE_MONTH";
                seriesHBSAG.ArgumentScaleType = ScaleType.Qualitative;
                seriesHBSAG.ValueDataMembers.AddRange(new string[] { "HBSAG_POSITIVE" });
                seriesHBSAG.ValueScaleType = ScaleType.Numerical;
                seriesHBSAG.LegendText = "乙肝病毒表面抗原转阳";
                cclQualityControl.Series.Add(seriesHBSAG);

                Series seriesHBEAG = new Series("HBEAG", ViewType.Bar);
                seriesHBEAG.DataSource = dtSub;
                seriesHBEAG.ArgumentDataMember = "DATE_MONTH";
                seriesHBEAG.ArgumentScaleType = ScaleType.Qualitative;
                seriesHBEAG.ValueDataMembers.AddRange(new string[] { "HBEAG_POSITIVE" });
                seriesHBEAG.ValueScaleType = ScaleType.Numerical;
                seriesHBEAG.LegendText = "乙肝Ｅ抗原转阳";
                cclQualityControl.Series.Add(seriesHBEAG);

                Series seriesANTI_HCV = new Series("ANTI_HCV", ViewType.Bar);
                seriesANTI_HCV.DataSource = dtSub;
                seriesANTI_HCV.ArgumentDataMember = "DATE_MONTH";
                seriesANTI_HCV.ArgumentScaleType = ScaleType.Qualitative;
                seriesANTI_HCV.ValueDataMembers.AddRange(new string[] { "ANTI_HCV_POSITIVE" });
                seriesANTI_HCV.ValueScaleType = ScaleType.Numerical;
                seriesANTI_HCV.LegendText = "丙肝病毒抗体转阳";
                cclQualityControl.Series.Add(seriesANTI_HCV);

                Series seriesPeritonealDialysis = new Series("PeritonealDialysis", ViewType.Bar);
                seriesPeritonealDialysis.DataSource = dtSub;
                seriesPeritonealDialysis.ArgumentDataMember = "DATE_MONTH";
                seriesPeritonealDialysis.ArgumentScaleType = ScaleType.Qualitative;
                seriesPeritonealDialysis.ValueDataMembers.AddRange(new string[] { "PERITONEAL_DIALYSIS" });
                seriesPeritonealDialysis.ValueScaleType = ScaleType.Numerical;
                seriesPeritonealDialysis.LegendText = "血透转腹透";
                cclQualityControl.Series.Add(seriesPeritonealDialysis);

                Series seriesRenalTransplant = new Series("RenalTransplant", ViewType.Bar);
                seriesRenalTransplant.DataSource = dtSub;
                seriesRenalTransplant.ArgumentDataMember = "DATE_MONTH";
                seriesRenalTransplant.ArgumentScaleType = ScaleType.Qualitative;
                seriesRenalTransplant.ValueDataMembers.AddRange(new string[] { "RENAL_TRANSPLANT" });
                seriesRenalTransplant.ValueScaleType = ScaleType.Numerical;
                seriesRenalTransplant.LegendText = "血透转肾移植";
                cclQualityControl.Series.Add(seriesRenalTransplant);

                ((XYDiagram)this.cclQualityControl.Diagram).AxisY.Title.Text = "人数";
                ((XYDiagram)this.cclQualityControl.Diagram).AxisY.Title.Font = new Font("Tahoma", 10);
                ((XYDiagram)this.cclQualityControl.Diagram).AxisY.Title.TextColor = Color.Red;
                ((XYDiagram)this.cclQualityControl.Diagram).AxisY.Title.Visible = true;

                ChartTitle title = new ChartTitle();
                title.Text = "质量管理基础数据统计";
                title.Font = new Font("Tahoma", 12);
                title.Dock = ChartTitleDockStyle.Top;
                this.cclQualityControl.Titles.Clear();
                this.cclQualityControl.Titles.Add(title);

                this.cclQualityControl.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
                this.cclQualityControl.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
                this.cclQualityControl.Legend.Direction = LegendDirection.TopToBottom;
            }
        }

        //隐藏显示
        public void HideSomthing()
        {
            this.ctlYearQuery.Visible = false;
            this.panelControl1.Visible = false;
            this.tpQualityControlGrid.PageVisible = false;
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

