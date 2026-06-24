/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:维持性人数统计查询类
 * 创建标识:刘超-2017年4月24日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.UI.Machine;
using DevExpress.XtraCharts;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Model;
using DevExpress.XtraSplashScreen;
using Hemo.Client.Controls;
using Hemo.Utilities;
using LinqExtensions = DevExpress.Mvvm.Native.LinqExtensions;

namespace Hemo.Client.UI.ReportChart
{
    public partial class QueryQualityMonitorLabReport : ViewBase
    {
        #region 类变量

        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        private IConfig _configService = ServiceManager.Instance.ConfigService;

        public ConfigModel.MED_COMMON_ITEMLISTDataTable commData = new ConfigModel.MED_COMMON_ITEMLISTDataTable();

        private bool IsNeed = false;

        #endregion

        #region 属性

        public DateTime dtStar { get; set; }

        public DateTime dtEnd { get; set; }

        #endregion

        #region 构造函数

        public QueryQualityMonitorLabReport()
        {
            InitializeComponent();
            //this.lbHide.Visible = this.panelChart.Visible = true;
        }

        #endregion

        #region 事件

        /// <summary>
        /// 列表行点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                var dtRow = this.gridView1.GetFocusedDataRow() as ConfigModel.MED_COMMON_ITEMLISTRow;

                //DrawChartPie(dtRow);
            }
        }

        /// <summary>
        /// lbHide点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbHide_Click(object sender, EventArgs e)
        {
            if (this.panelChart.Visible)
            {
                this.panelChart.Visible = false;
                this.lbHide.Appearance.Image = global::Hemo.Client.Properties.Resources.left2;
            }
            else
            {
                this.panelChart.Visible = true;
                this.lbHide.Appearance.Image = global::Hemo.Client.Properties.Resources.right2;
                if (IsNeed)
                {
                    if (commData.Rows.Count > 0)
                    {
                        DrawChartPie(commData);
                        IsNeed = false;
                    }
                }
            }
        }

        /// <summary>
        /// lbHide MouseHover
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbHide_MouseHover(object sender, EventArgs e)
        {
            this.lbHide.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(243)))), ((int)(((byte)(183)))));
        }

        /// <summary>
        /// lbHide MouseLeave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbHide_MouseLeave(object sender, EventArgs e)
        {
            this.lbHide.Appearance.BackColor = System.Drawing.Color.Transparent;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InzationData()
        {
            IsNeed = true;
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                ShowMessage();
                var dtPatient = new DataTable();
                var commonItemlist = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
                var conditionList = new ConfigModel.MED_COMMON_ITEMLISTDataTable(); 
                commData = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    dtPatient = _hemodialysisService.GetHemoIdInLastWeekAndThreeMonthsByDate(dtStar, dtEnd);
                    commonItemlist = _configService.GetConfigList("", "", "检验统计", "1");
                    conditionList = _configService.GetConfigList("", "", "检验统计条件", "1");
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    if (dtPatient.Rows.Count <= 0)
                    {
                        HideMessage();
                        return;
                    }
                    var holdPatientCount = dtPatient.Rows.Count;
                    this.lbPatients.Text = this.labelControl1.Text = string.Format("维持性总人数：{0}", holdPatientCount);
                    string hemoIds = string.Empty;
                    dtPatient.AsEnumerable().ToList().ForEach(row =>
                    {
                        hemoIds += string.Format("'{0}',", row["HEMODIALYSIS_ID"].ToString());
                    });
                    hemoIds = hemoIds.Substring(0, hemoIds.Length - 1);
                    foreach (ConfigModel.MED_COMMON_ITEMLISTRow itemlistRow in commonItemlist.Rows)
                    {
                        var condition = string.Empty;
                        if (conditionList.AsEnumerable().FirstOrDefault(i => i.ITEM_NAME.Equals(itemlistRow.ITEM_NAME)) != null)
                        {
                            condition = conditionList.AsEnumerable().FirstOrDefault(i => i.ITEM_NAME.Equals(itemlistRow.ITEM_NAME)).ITEM_VALUE.ToString();
                        }
                        else
                        {
                            condition = " 1=1 ";
                        }
                        var dt = _hemodialysisService.GetHoldLabItemDt(hemoIds, itemlistRow.ITEM_NAME,
                               itemlistRow.ITEM_VALUE, dtStar, dtEnd, condition);
                        int holdLabcount = dt.Rows.Count;

                        #region MyRegion

                        ////dtPatient.AsEnumerable().ToList().ForEach(row =>
                        ////{
                        ////    //if (row["HEMODIALYSIS_ID"].ToString() == "10000022" && itemlistRow.ITEM_NAME == "白蛋白")
                        ////    //{
                        ////    //    string a = "ggog";
                        ////    //}
                        ////    var dt = _hemodialysisService.GetHoldLabItemDt(row["HEMODIALYSIS_ID"].ToString(), itemlistRow.ITEM_NAME,
                        ////        itemlistRow.ITEM_VALUE, dtStar, dtEnd);
                        ////    if (dt.Rows.Count > 0)
                        ////    {
                        ////        holdLabcount++;
                        ////    }
                        ////});

                        #endregion
                        var dataRow = commData.NewMED_COMMON_ITEMLISTRow();
                        dataRow.ITEM_ID = itemlistRow.ITEM_ID;
                        dataRow.ITEM_NAME = itemlistRow.ITEM_NAME;
                        dataRow.ITEM_VALUE = itemlistRow.ITEM_VALUE;
                        dataRow.ITEM_TYPE = itemlistRow.ITEM_NAME + itemlistRow.ITEM_VALUE;
                        dataRow.STATUS = holdPatientCount.ToString();
                        dataRow.PARENT = holdLabcount.ToString();
                        dataRow.PRICE = Math.Round(Utilities.Utility.CDecimal(holdLabcount.ToString()) / Utilities.Utility.CDecimal(holdPatientCount.ToString()) * 100, 2).ToString() + "%";
                        commData.AddMED_COMMON_ITEMLISTRow(dataRow);
                    }

                    this.gridControl1.DataSource = commData;

                    HideMessage();

                };
                worker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// 绘制饼图
        /// </summary>
        /// <param name="commData"></param>
        private void DrawChartPie(ConfigModel.MED_COMMON_ITEMLISTDataTable commData)
        {
            if (commData != null && commData.Rows.Count > 0)
            {
                #region MyReg克隆表结构ion

                DataTable dtResult = new DataTable();
                //克隆表结构
                dtResult = commData.Clone();
                chartControl.Series.Clear();
                foreach (DataColumn dataColumn in dtResult.Columns)
                {
                    if (dataColumn.ColumnName == "STATUS" || dataColumn.ColumnName == "PARENT")
                    {
                        dataColumn.DefaultValue = 0;
                        dataColumn.DataType = typeof(decimal);
                    }
                }
                dtResult.AcceptChanges();
                foreach (ConfigModel.MED_COMMON_ITEMLISTRow itemlistRow in commData.Rows)
                {
                    DataRow rowNew = dtResult.NewRow();
                    rowNew["ITEM_ID"] = itemlistRow.ITEM_ID;
                    rowNew["ITEM_NAME"] = itemlistRow.ITEM_NAME;
                    rowNew["ITEM_VALUE"] = itemlistRow.ITEM_VALUE;
                    rowNew["ITEM_TYPE"] = itemlistRow.ITEM_TYPE;
                    rowNew["STATUS"] = itemlistRow.STATUS;
                    rowNew["PARENT"] = itemlistRow.PARENT;
                    rowNew["PRICE"] = itemlistRow.PRICE.Replace('%', ' ');
                    dtResult.Rows.Add(rowNew);
                }

                #endregion

                // 柱状图里的第一个柱
                //Series Series1 = new Series("维护性总人数", ViewType.Bar);
                //Series1.DataSource = dtResult;
                //Series1.ArgumentScaleType = ScaleType.Qualitative;

                // 以哪个字段进行显示
                //Series1.ArgumentDataMember = "ITEM_TYPE";//itemlistRow.ITEM_TYPE;
                //Series1.ValueScaleType = ScaleType.Numerical;

                //// 柱状图里的柱的取值字段
                //Series1.ValueDataMembers.AddRange(new string[] { "STATUS" });

                // 柱状图里的第二柱
                Series Series2 = new Series("检验数据", ViewType.Bar);
                Series2.DataSource = dtResult;
                Series2.ArgumentScaleType = ScaleType.Qualitative;
                Series2.ArgumentDataMember = "ITEM_TYPE";//itemlistRow.ITEM_NAME;
                Series2.ValueScaleType = ScaleType.Numerical;
                Series2.ValueDataMembers.AddRange(new string[] { "PARENT" });
                //chartControl.Series.Add(Series1);
                chartControl.Series.Add(Series2);
            }
        }

        /// <summary>
        /// 绘制图表
        /// </summary>
        /// <param name="control"></param>
        /// <param name="seriesName"></param>
        /// <param name="type"></param>
        /// <param name="dt"></param>
        /// <param name="column1"></param>
        /// <param name="column2"></param>
        public static void DrawChart(DevExpress.XtraCharts.ChartControl control, string seriesName, ViewType type,
            DataTable dt, string column1, string column2)
        {
            Series series = new Series(seriesName, type);
            DataTable table = dt;
            SeriesPoint point = null;

            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (column1.ToUpper() == "CURE_CREATE_DATE" || column1.ToUpper() == "CREATE_DATE")
                {
                    point = new SeriesPoint(Utility.CDate(table.Rows[i][column1].ToString()).ToShortDateString(),
                        Utility.CDouble(table.Rows[i][column2].ToString()));
                }
                else
                {
                    point = new SeriesPoint(table.Rows[i][column1].ToString(),
                        Utility.CDouble(table.Rows[i][column2].ToString()));
                }
                series.Points.Add(point);
            }

            if (type == ViewType.Line && (seriesName.Contains("透前收缩压") || seriesName.Contains("透前舒张压")))
            {
                (series.Label as PointSeriesLabel).Angle = 120;
            }

            control.Series.Add(series);
        }

        /// <summary>
        /// 获取检验结果
        /// </summary>
        /// <returns></returns>
        public ConfigModel.MED_COMMON_ITEMLISTDataTable GetLabResult()
        {
            return this.gridControl1.DataSource as ConfigModel.MED_COMMON_ITEMLISTDataTable;
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
