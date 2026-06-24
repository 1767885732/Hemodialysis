/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:报表基础控件柱图
 * 创建标识:贺建操-2014年8月2日
 * 传入参数，使用自定义图表功能（DrawCustomerChart）。
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Utilities;
using DevExpress.XtraCharts;
using Hemo.Model;
using System.Drawing.Imaging;

namespace Hemo.Client.Controls.SignChart
{
    public partial class CtlSignChart : DevExpress.XtraEditors.XtraUserControl
    {
        #region 变量

        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        /// <summary>
        /// 透析号
        /// </summary>
        private string _hemodialysisID = string.Empty;
        public string HEMODIALYSIS_ID
        {
            set
            {
                _hemodialysisID = value;
            }
            get
            {
                return _hemodialysisID;
            }
        }

        /// <summary>
        /// 图表标题
        /// </summary>
        private string _chartTitle = string.Empty;
        public string ChartTitle
        {
            set
            {
                _chartTitle = value;
            }
            get
            {
                return _chartTitle;
            }
        }

        #endregion

        #region 构造函数

        public CtlSignChart()
        {
            InitializeComponent();
        }

        #endregion

        #region 方法



        /// <summary>
        /// 透前/后体重趋势图
        /// </summary>
        public void DrawWeightChart(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                BaseChartInfo.SetChartTitle(chartCureCount, true, "肝素趋势图", true, 2, StringAlignment.Center, ChartTitleDockStyle.Top, true, new Font("宋体", 12, FontStyle.Bold), Color.Red, 10);
                //普通肝素抗凝
                DataTable dtCommon = Hemo.Utilities.Utility.GetSubTable(dt, "HEPARIN_SPECIES='fc3020ed-7fe4-4798-8982-3a4a9cf64fe1'");
                //低分子肝素抗凝
                DataTable dtLow = Hemo.Utilities.Utility.GetSubTable(dt, "HEPARIN_SPECIES='6c128a35-2a54-4efa-84ff-b7b88cd6bc21'");
                if (dtLow != null && dtLow.Rows.Count > 0)
                {
                    BaseChartInfo.DrawChart(chartCureCount, "低分子肝素", ViewType.Line, dtLow, "CURE_CREATE_DATE", "FIRST_HEPARIN");
                }
                if (dtCommon != null && dtCommon.Rows.Count > 0)
                {
                    BaseChartInfo.DrawChart(chartCureCount, "普通肝素", ViewType.Line, dtCommon, "CURE_CREATE_DATE", "FIRST_HEPARIN");
                }
                BaseChartInfo.SetAxisX(chartCureCount, true, StringAlignment.Center, "", Color.Red, true, new Font("宋体", 12, FontStyle.Bold));
                //   BaseChartInfo.SetAxisY(chartCureCount, true, StringAlignment.Center, "单位：mmHg", Color.Red, true, new Font("宋体", 12, FontStyle.Bold));
            }
        }
        public void SetCear()
        {
            this.chartCureCount.ClearCache();

        }
        /// <summary>
        /// 干体重趋势图
        /// </summary>
        /// <param name="dt"></param>
        public void DrawDryWeight(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                BaseChartInfo.SetChartTitle(chartCureCount, true, "体重趋势图", true, 2, StringAlignment.Center, ChartTitleDockStyle.Top, true, new Font("宋体", 12, FontStyle.Bold), Color.Red, 10);
                BaseChartInfo.DrawChart(chartCureCount, "干体重", ViewType.Line, dt, "CURE_CREATE_DATE", "DRY_WEIGHT");
                BaseChartInfo.DrawChart(chartCureCount, "透前体重", ViewType.Line, dt, "CURE_CREATE_DATE", "BEFORE_DRY_WEIGHT");
                BaseChartInfo.DrawChart(chartCureCount, "透后体重", ViewType.Line, dt, "CURE_CREATE_DATE", "AFTER_DRY_WEIGHT");

                #region  刘超，2015-10-8，为了达到更好的展现效果，需要动态计算Y轴的最大与最小值才能看清每个节点的数据,在最大值的基础上加5个点。
                decimal minValue = 0;
                decimal maxValue = 0;
                decimal tempDryWeight = 0;
                decimal tempBeforeWeight = 0;
                decimal tempAfterWeight = 0;
                decimal temp = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tempDryWeight = Utility.CDecimal(dt.Rows[i]["DRY_WEIGHT"].ToString());
                    tempBeforeWeight = Utility.CDecimal(dt.Rows[i]["BEFORE_DRY_WEIGHT"].ToString());
                    tempAfterWeight = Utility.CDecimal(dt.Rows[i]["AFTER_DRY_WEIGHT"].ToString());

                    //取最大值
                    temp = tempDryWeight > tempBeforeWeight ? tempDryWeight : tempBeforeWeight;
                    temp = temp > tempAfterWeight ? temp : tempAfterWeight;
                    maxValue = temp + 3;

                    //取最小值
                    temp = tempDryWeight < tempBeforeWeight ? tempDryWeight : tempBeforeWeight;
                    if (tempAfterWeight != 0)
                    {
                        temp = temp < tempAfterWeight ? temp : tempAfterWeight;
                        minValue = temp;
                    }
                    else
                    {
                        minValue = temp;
                    }
                }
                #endregion

                BaseChartInfo.SetAxisX(chartCureCount, true, StringAlignment.Center, "", Color.Red, true, new Font("宋体", 12, FontStyle.Bold));
            }
        }


        /// <summary>
        /// 预计脱水趋势图
        /// </summary>
        public void DrawUFRChart(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                BaseChartInfo.SetChartTitle(chartCureCount, true, "预计脱水趋势图", true, 2, StringAlignment.Center, ChartTitleDockStyle.Top, true, new Font("宋体", 12, FontStyle.Bold), Color.Red, 10);
                BaseChartInfo.DrawChart(chartCureCount, "预计脱水", ViewType.Line, dt, "CURE_CREATE_DATE", "UFR");

                BaseChartInfo.SetAxisX(chartCureCount, true, StringAlignment.Center, "", Color.Red, true, new Font("宋体", 12, FontStyle.Bold));
                BaseChartInfo.SetAxisY(chartCureCount, true, StringAlignment.Center, "", Color.Red, true, new Font("宋体", 12, FontStyle.Bold));
            }
        }

        /// <summary>
        /// 自定义图表 
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="chartTitle">图表标题</param>
        /// <param name="chartSeriesTitel">数据轴标题</param>
        /// <param name="cType">图表类型</param>
        /// <param name="column1">显示列1</param>
        /// <param name="column2">显示列2</param>
        public void DrawCustomerChart(DataTable dt, string chartTitle, string chartSeriesTitel, ViewType cType, string column1, string column2)
        {
            this.chartCureCount.Titles.Clear();
            this.chartCureCount.Series.Clear();
            if (dt != null && dt.Rows.Count > 0)
            {
                BaseChartInfo.SetChartTitle(chartCureCount, true, chartTitle, true, 2, StringAlignment.Center, ChartTitleDockStyle.Top, true, new Font("宋体", 12, FontStyle.Bold), Color.Red, 10);
                BaseChartInfo.DrawChart(chartCureCount, chartSeriesTitel, cType, dt, column1, column2);

                BaseChartInfo.SetAxisX(chartCureCount, true, StringAlignment.Center, "", Color.Red, true, new Font("宋体", 12, FontStyle.Bold));
                BaseChartInfo.SetAxisY(chartCureCount, true, StringAlignment.Center, "", Color.Red, true, new Font("宋体", 12, FontStyle.Bold));
            }
        }

        /// <summary>
        /// 实际脱水趋势图
        /// </summary>
        public void DrawDryWaterChart(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                BaseChartInfo.SetChartTitle(chartCureCount, true, "实际脱水趋势图", true, 2, StringAlignment.Center, ChartTitleDockStyle.Top, true, new Font("宋体", 12, FontStyle.Bold), Color.Red, 10);
                BaseChartInfo.DrawChart(chartCureCount, "实际脱水", ViewType.Line, dt, "CURE_CREATE_DATE", "DRY_WATER_VALUE");

                BaseChartInfo.SetAxisX(chartCureCount, true, StringAlignment.Center, "", Color.Red, true, new Font("宋体", 12, FontStyle.Bold));
                BaseChartInfo.SetAxisY(chartCureCount, true, StringAlignment.Center, "", Color.Red, true, new Font("宋体", 12, FontStyle.Bold));
            }
        }

        /// <summary>
        /// 血流量趋势图
        /// </summary>
        public void DrawBloowFlowChart(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                BaseChartInfo.SetChartTitle(chartCureCount, true, "血流量趋势图", true, 2, StringAlignment.Center, ChartTitleDockStyle.Top, true, new Font("宋体", 12, FontStyle.Bold), Color.Red, 10);
                BaseChartInfo.DrawChart(chartCureCount, "血流量", ViewType.Line, dt, "CURE_CREATE_DATE", "BLOOW_FLOW");

                BaseChartInfo.SetAxisX(chartCureCount, true, StringAlignment.Center, "", Color.Red, true, new Font("宋体", 12, FontStyle.Bold));
                BaseChartInfo.SetAxisY(chartCureCount, true, StringAlignment.Center, "", Color.Red, true, new Font("宋体", 12, FontStyle.Bold));
            }
        }

        /// <summary>
        /// 静脉压/跨膜压趋势图 SYSTOLIC_PRESSURE/DIASTOLIC_PRESSURE
        /// </summary>
        /// <param name="dt"></param>
        public void DrawPressureChart(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                DataTable dtSub = Utility.GetSubTable(dt, string.Empty, "CREATE_DATE DESC");
                List<string> listDate = new List<string>();
                List<DataRow> listBefore = new List<DataRow>();
                List<DataRow> listAfter = new List<DataRow>();

                dtSub.AsEnumerable().ToList().ForEach(row =>
                {
                    var strDate = Utility.CDate(row["CREATE_DATE"].ToString()).ToShortDateString();
                    if (!listDate.Contains(strDate))
                    {
                        listDate.Add(strDate);
                    }
                });

                listDate.ForEach(item =>
                {
                    var beforeRow = dtSub.AsEnumerable().ToList().Last(row => Utility.CDate(row["CREATE_DATE"].ToString()).ToShortDateString() == item);
                    var afterRow = dtSub.AsEnumerable().ToList().First(row => Utility.CDate(row["CREATE_DATE"].ToString()).ToShortDateString() == item);
                    listBefore.Add(beforeRow);
                    listAfter.Add(afterRow);
                });

                DataTable dtBefore = Utility.GetDataTableFromDataRow(listBefore.ToArray());
                DataTable dtAfter = Utility.GetDataTableFromDataRow(listAfter.ToArray());

                BaseChartInfo.SetChartTitle(chartCureCount, true, "血压趋势图", true, 2, StringAlignment.Center, ChartTitleDockStyle.Top, true, new Font("宋体", 12, FontStyle.Bold), Color.Red, 10);
                BaseChartInfo.DrawChart(chartCureCount, "透前收缩压", ViewType.Line, dtBefore, "CREATE_DATE", "SYSTOLIC_PRESSURE");
                BaseChartInfo.DrawChart(chartCureCount, "透前舒张压", ViewType.Line, dtBefore, "CREATE_DATE", "DIASTOLIC_PRESSURE");
                BaseChartInfo.DrawChart(chartCureCount, "透后收缩压", ViewType.Line, dtAfter, "CREATE_DATE", "SYSTOLIC_PRESSURE");
                BaseChartInfo.DrawChart(chartCureCount, "透后舒张压", ViewType.Line, dtAfter, "CREATE_DATE", "DIASTOLIC_PRESSURE");

                BaseChartInfo.SetAxisX(chartCureCount, true, StringAlignment.Center, "", Color.Red, true, new Font("宋体", 12, FontStyle.Bold));
                //    BaseChartInfo.SetAxisY(chartCureCount, true, StringAlignment.Center, "单位：mmHg", Color.Red, true, new Font("宋体", 12, FontStyle.Bold));
            }
        }
        /// <summary>
        /// 静脉压/跨膜压趋势图 SYSTOLIC_PRESSURE/DIASTOLIC_PRESSURE
        /// </summary>
        /// <param name="dt"></param>
        public void DrawPressureChartDetail(HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable dt)
        {
            this.chartCureCount.Titles.Clear();
            this.chartCureCount.Series.Clear();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataTable dtSub = Utility.GetSubTable(dt, string.Empty, "CREATE_DATE DESC");
                List<string> listDate = new List<string>();
                List<DataRow> listBefore = new List<DataRow>();
                List<DataRow> listAfter = new List<DataRow>();

                dtSub.AsEnumerable().ToList().ForEach(row =>
                {
                    var strDate = Utility.CDate(row["CREATE_DATE"].ToString()).ToShortTimeString();
                    if (!listDate.Contains(strDate))
                    {
                        listDate.Add(strDate);
                    }
                });

                listDate.ForEach(item =>
                {
                    var beforeRow = dtSub.AsEnumerable().ToList().Last(row => Utility.CDate(row["CREATE_DATE"].ToString()).ToShortTimeString() == item);
                    var afterRow = dtSub.AsEnumerable().ToList().First(row => Utility.CDate(row["CREATE_DATE"].ToString()).ToShortTimeString() == item);
                    listBefore.Add(beforeRow);
                    listAfter.Add(afterRow);
                });

                DataTable dtBefore = Utility.GetDataTableFromDataRow(listBefore.ToArray());
                DataTable dtAfter = Utility.GetDataTableFromDataRow(listAfter.ToArray());

                BaseChartInfo.SetChartTitle(chartCureCount, true, "血压趋势图", true, 2, StringAlignment.Center, ChartTitleDockStyle.Top, true, new Font("宋体", 12, FontStyle.Bold), Color.Red, 10);
                BaseChartInfo.DrawChartDetail(chartCureCount, "透前收缩压", ViewType.Line, dtBefore, "CREATE_DATE", "SYSTOLIC_PRESSURE");
                BaseChartInfo.DrawChartDetail(chartCureCount, "透前舒张压", ViewType.Line, dtBefore, "CREATE_DATE", "DIASTOLIC_PRESSURE");
                BaseChartInfo.DrawChartDetail(chartCureCount, "透后收缩压", ViewType.Line, dtAfter, "CREATE_DATE", "SYSTOLIC_PRESSURE");
                BaseChartInfo.DrawChartDetail(chartCureCount, "透后舒张压", ViewType.Line, dtAfter, "CREATE_DATE", "DIASTOLIC_PRESSURE");

                BaseChartInfo.SetAxisX(chartCureCount, true, StringAlignment.Center, "", Color.Red, true, new Font("宋体", 12, FontStyle.Bold));
                //    BaseChartInfo.SetAxisY(chartCureCount, true, StringAlignment.Center, "单位：mmHg", Color.Red, true, new Font("宋体", 12, FontStyle.Bold));
            }
        }
        public void DrawRecentPressureChart(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                BaseChartInfo.SetChartTitle(chartCureCount, true, "血压趋势图", true, 2, StringAlignment.Center, ChartTitleDockStyle.Top, true, new Font("宋体", 12, FontStyle.Bold), Color.Red, 10);
                BaseChartInfo.DrawChart(chartCureCount, "收缩压", ViewType.Line, dt, "CREATE_DATE", "SYSTOLIC_PRESSURE");
                BaseChartInfo.DrawChart(chartCureCount, "舒张压", ViewType.Line, dt, "CREATE_DATE", "DIASTOLIC_PRESSURE");

                BaseChartInfo.SetAxisX(chartCureCount, true, StringAlignment.Center, "", Color.Red, true, new Font("宋体", 12, FontStyle.Bold));
            }
        }

        /// <summary>
        /// 血红蛋白趋势图
        /// </summary>
        public void DrawHBTrendChart(DataTable dt)
        {
            chartCureCount.Titles.Clear();
            chartCureCount.Series.Clear();
            if (dt != null && dt.Rows.Count > 0)
            {
                BaseChartInfo.SetChartTitle(chartCureCount, true, "血红蛋白浓度变化趋势图", true, 2, StringAlignment.Center, ChartTitleDockStyle.Top, true, new Font("宋体", 12, FontStyle.Bold), Color.Red, 10);
                BaseChartInfo.DrawChart(chartCureCount, "血红蛋白浓度", ViewType.Bar, dt, "RESULTS_RPT_DATE_TIME", "RESULT");
                BaseChartInfo.SetAxisX(chartCureCount, true, StringAlignment.Center, "", Color.Red, true, new Font("宋体", 12, FontStyle.Bold));
                BaseChartInfo.SetAxisY(chartCureCount, true, StringAlignment.Center, "单位：g/l", Color.Red, true, new Font("宋体", 12, FontStyle.Bold));
                this.chartCureCount.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            }
        }


        /// <summary>
        /// 透析死亡人数
        /// </summary>
        public void DrawDeadCountChart(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
                return;
            var strTitle = dt.Rows[0]["RESULT"].ToString();
            chartCureCount.Titles.Clear();
            chartCureCount.Series.Clear();
            if (dt != null && dt.Rows.Count > 0)
            {
                BaseChartInfo.SetChartTitle(chartCureCount, true, strTitle, true, 2, StringAlignment.Center, ChartTitleDockStyle.Top, true, new Font("宋体", 12, FontStyle.Bold), Color.Red, 10);
                BaseChartInfo.DrawChart(chartCureCount, "死亡人数", ViewType.Pie, dt, "ALLCOUNT", "DIECOUNT");


            }
            //chartCureCount.Titles.Clear();
            //chartCureCount.Series.Clear();
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    BaseChartInfo.SetChartTitle(chartCureCount, true, "透析死亡人数", true, 2, StringAlignment.Center, ChartTitleDockStyle.Top, true, new Font("宋体", 12, FontStyle.Bold), Color.Red, 10);
            //    BaseChartInfo.DrawChart(chartCureCount, "死亡人数", ViewType.Line, dt, "DEAD_DATE", "DEAD_COUNT");

            //    BaseChartInfo.SetAxisX(chartCureCount, true, StringAlignment.Center, "", Color.Red, true, new Font("宋体", 12, FontStyle.Bold));
            //    BaseChartInfo.SetAxisY(chartCureCount, true, StringAlignment.Center, "", Color.Red, true, new Font("宋体", 12, FontStyle.Bold));
            //}
        }

        public void ExportToImage()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "导出图片";
            fileDialog.Filter = "图片文件(*.jpg;*.jpeg;*.gif;*.png)|*.jpg;*.jpeg;*.gif;*.png";
            fileDialog.FileName = System.DateTime.Now.ToShortDateString() + ChartTitle;
            fileDialog.RestoreDirectory = true;
            DialogResult dialogResult = fileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                this.chartCureCount.ExportToImage(fileDialog.FileName, ImageFormat.Jpeg);
            }
        }
        #endregion
    }
}
