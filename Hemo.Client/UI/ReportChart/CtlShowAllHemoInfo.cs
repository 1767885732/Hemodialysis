/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:透析人次种类报表类
 * 创建标识:吕志强-2017年4月20日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using Hemo.IService.Config;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.Client.UI.Machine;
using System.Linq;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace Hemo.Client.UI.ReportChart
{
    public partial class CtlShowAllHemoInfo : ViewBase
    {
        #region 类变量

        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        private DataTable dtResult = null;

        private bool isFirstPage = false;

        private bool isConstant = true;

        #endregion

        #region 属性

        /// <summary>
        /// 是否首页加载
        /// </summary>
        public bool IsFirstPage
        {
            get { return isFirstPage; }
            set { isFirstPage = value; }
        }

        #endregion

        #region 构造函数

        public CtlShowAllHemoInfo()
        {
            InitializeComponent();
            this.beginTime.DateTime = DateTime.Now.AddDays(-DateTime.Now.Day + 1).AddMonths(-DateTime.Now.Month + 1);
            this.endTime.DateTime = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.Day);
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CtlShowAllHemoInfo_Load(object sender, EventArgs e)
        {
            RunBackgroundWorker(true);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Query_Click(object sender, EventArgs e)
        {
            isConstant = this.chkConstant.Checked;
            RunBackgroundWorker(false);
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ExpExcel_Click(object sender, EventArgs e)
        {
            if (this.gvCount.RowCount > 0)
            {
                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.Title = "导出Excel";
                fileDialog.Filter = "Excel文件(*.xls)|*.xls";
                fileDialog.FileName = this.beginTime.DateTime.ToString("yyyyMMdd") + "-" + this.endTime.DateTime.ToString("yyyyMMdd") + "透析人次统计";
                fileDialog.RestoreDirectory = true;
                DialogResult dialogResult = fileDialog.ShowDialog(this);
                if (dialogResult == DialogResult.OK)
                {
                    DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                    options.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
                    this.gvCount.ExportToXls(fileDialog.FileName, options);
                    DevExpress.XtraEditors.XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// 报表说明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNote_Click(object sender, EventArgs e)
        {
            ReportNoteFrm reportNote = new ReportNoteFrm();
            reportNote.ReportType = ReportTypeEnum.数据汇总;
            reportNote.ShowDialog();
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_print_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "导出图片";
            fileDialog.Filter = "图片文件(*.jpg;*.jpeg;*.gif;*.png)|*.jpg;*.jpeg;*.gif;*.png";
            fileDialog.FileName = this.beginTime.DateTime.ToString("yyyyMMdd") + "-" + this.endTime.DateTime.ToString("yyyyMMdd") + "透析人次统计";
            fileDialog.RestoreDirectory = true;
            DialogResult dialogResult = fileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                if (this.xtraTabControl1.SelectedTabPageIndex == 0)
                {
                    chartCureCount.ExportToImage(fileDialog.FileName, ImageFormat.Jpeg);
                }
                else
                {
                    chartShiYang.ExportToImage(fileDialog.FileName, ImageFormat.Jpeg);
                }
            }
        }

        private void work_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = e.Argument;
            LoadChartData();
        }

        private void work_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((bool)e.Result)
            {
                if (isFirstPage)
                {
                    LoadBarData();
                }
                else
                {
                    LoadBarData();
                    LoadPieData();
                }
            }
            else
            {
                if (this.xtraTabControl1.SelectedTabPageIndex == 0) { LoadBarData(); }
                else { LoadPieData(); }
            }
            this.gcCount.DataSource = dtResult;

            if (this.xtraTabControl1.SelectedTabPageIndex == 0)
            {
                this.busyIndicator.HideLoadingScreen();
            }
            else
            {
                this.busyIndicator1.HideLoadingScreen();
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 首页调用时隐藏检索条件
        /// </summary>
        public void SetSearchBarDisplayNone()
        {
            this.gcCount.Visible = false;
            panelControl3.Visible = false;
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            xtraTabControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tableLayoutPanel1.SetRowSpan(this.xtraTabControl1, 3);
        }

        /// <summary>
        /// 启动后台线程
        /// </summary>
        /// <param name="isFirstLoad"></param>
        private void RunBackgroundWorker(bool isFirstLoad)
        {
            if (this.xtraTabControl1.SelectedTabPageIndex == 0)
            {
                this.busyIndicator.ShowLoadingScreenFor(this.chartCureCount);
            }
            else
            {
                this.busyIndicator1.ShowLoadingScreenFor(this.chartShiYang);
            }

            using (BackgroundWorker work = new BackgroundWorker())
            {
                work.DoWork += new DoWorkEventHandler(work_DoWork);
                work.RunWorkerCompleted += new RunWorkerCompletedEventHandler(work_RunWorkerCompleted);
                work.RunWorkerAsync(isFirstLoad);
            }
        }

        /// <summary>
        /// 加载报表数据
        /// </summary>
        private void LoadChartData()
        {
            dtResult = null;
            DataTable dtPatient = isConstant ? _hemodialysisService.GetHemoIdInLastWeekAndThreeMonthsByDate(this.beginTime.DateTime, this.endTime.DateTime) : _hemodialysisService.GetHemoIdByDate(this.beginTime.DateTime, this.endTime.DateTime);
            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                DataTable dtCount = null;
                dtPatient.AsEnumerable().ToList().ForEach(row =>
                {
                    DataTable dtSubCount = _hemodialysisService.GetCureCountByHemoIdAndDate(row["HEMODIALYSIS_ID"].ToString(), this.beginTime.DateTime, this.endTime.DateTime);
                    dtCount = dtCount ?? (dtSubCount != null ? dtSubCount.Clone() : dtCount);
                    if (dtSubCount != null && dtSubCount.Rows.Count > 0)
                    {
                        dtSubCount.AsEnumerable().ToList().ForEach(r =>
                        {
                            var findRow = dtCount.AsEnumerable().FirstOrDefault(i => i["CURE_MONTH"].ToString().Equals(r["CURE_MONTH"].ToString()));
                            if (findRow == null)
                            {
                                dtCount.ImportRow(r);
                            }
                            else
                            {
                                findRow["SUB_COUNT"] = Utility.CInt(findRow["SUB_COUNT"].ToString()) + Utility.CInt(r["SUB_COUNT"].ToString());
                                findRow["HD_COUNT"] = Utility.CInt(findRow["HD_COUNT"].ToString()) + Utility.CInt(r["HD_COUNT"].ToString());
                                findRow["HDF_COUNT"] = Utility.CInt(findRow["HDF_COUNT"].ToString()) + Utility.CInt(r["HDF_COUNT"].ToString());
                                findRow["HF_COUNT"] = Utility.CInt(findRow["HF_COUNT"].ToString()) + Utility.CInt(r["HF_COUNT"].ToString());
                                findRow["HP_COUNT"] = Utility.CInt(findRow["HP_COUNT"].ToString()) + Utility.CInt(r["HP_COUNT"].ToString());
                                findRow["HDHP_COUNT"] = Utility.CInt(findRow["HDHP_COUNT"].ToString()) + Utility.CInt(r["HDHP_COUNT"].ToString());
                                findRow["CRRT_COUNT"] = Utility.CInt(findRow["CRRT_COUNT"].ToString()) + Utility.CInt(r["CRRT_COUNT"].ToString());
                            }
                        });
                    }
                });

                if (dtCount != null && dtCount.Rows.Count > 0)
                {
                    dtResult = dtCount.Clone();
                    dtCount.AsEnumerable().OrderBy(row => row["CURE_MONTH"].ToString()).CopyToDataTable(dtResult, LoadOption.OverwriteChanges);
                    var r = dtResult.NewRow();
                    r["CURE_MONTH"] = "合计";
                    r["SUB_COUNT"] = dtResult.Compute("Sum(SUB_COUNT)", string.Empty);
                    r["HD_COUNT"] = dtResult.Compute("Sum(HD_COUNT)", string.Empty);
                    r["HDF_COUNT"] = dtResult.Compute("Sum(HDF_COUNT)", string.Empty);
                    r["HF_COUNT"] = dtResult.Compute("Sum(HF_COUNT)", string.Empty);
                    r["HP_COUNT"] = dtResult.Compute("Sum(HP_COUNT)", string.Empty);
                    r["HDHP_COUNT"] = dtResult.Compute("Sum(HDHP_COUNT)", string.Empty);
                    r["CRRT_COUNT"] = dtResult.Compute("Sum(CRRT_COUNT)", string.Empty);
                    dtResult.Rows.Add(r);
                }
            }
        }

        /// <summary>
        /// 加载饼图数据
        /// </summary>
        private void LoadPieData()
        {
            this.chartShiYang.Series.Clear();
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                var findRow = dtResult.AsEnumerable().FirstOrDefault(row => row["CURE_MONTH"].ToString().Equals("合计"));
                if (findRow != null)
                {
                    DataTable dtSource = new DataTable();
                    dtSource.Columns.Add("PURIFICATION_MODE", typeof(string));
                    dtSource.Columns.Add("SUB_COUNT", typeof(int));
                    foreach (DataColumn column in dtResult.Columns)
                    {
                        if (column.ColumnName.Equals("CURE_MONTH") || column.ColumnName.Equals("SUB_COUNT"))
                        {
                            continue;
                        }
                        var row = dtSource.NewRow();
                        string mode = column.ColumnName.Substring(0, column.ColumnName.IndexOf("_"));
                        row["PURIFICATION_MODE"] = mode.Equals("HDHP") ? "HD+HP" : mode;
                        row["SUB_COUNT"] = findRow[column.ColumnName];
                        dtSource.Rows.Add(row);
                    }

                    //新建一个大饼
                    Series serShiYang = new Series(string.Empty, ViewType.Pie);//往控件中新增一个饼图
                    serShiYang.DataSource = dtSource;//设置数据源
                    serShiYang.PointOptions.PointView = PointView.ArgumentAndValues;//设置饼图上的lable显示 文字/比率值 
                    serShiYang.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;//用百分比表示
                    serShiYang.PointOptions.ValueNumericOptions.Precision = 1;//百分号前面的数字不跟小数点
                    PieSeriesLabel label = serShiYang.Label as PieSeriesLabel;
                    label.Position = PieSeriesLabelPosition.TwoColumns; //设置饼图上lable的显示方式，此方式将独立出一个列显示lable
                    (serShiYang.View as DevExpress.XtraCharts.PieSeriesView).ExplodeMode = PieExplodeMode.MaxValue; //突出显示最大的饼块。
                    (serShiYang.View as DevExpress.XtraCharts.PieSeriesView).RuntimeExploding = true;

                    //饼图的数据
                    serShiYang.ValueDataMembers.AddRange(new string[] { "SUB_COUNT" });//绑定值的字段
                    serShiYang.ValueScaleType = ScaleType.Numerical;//值的类型
                    serShiYang.ArgumentDataMember = "PURIFICATION_MODE";//绑定饼块的描述文字
                    serShiYang.ArgumentScaleType = ScaleType.Qualitative;
                    //数据以百分比显示时只能是Default和None
                    ((PieSeriesLabel)serShiYang.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

                    chartShiYang.Series.Add(serShiYang);

                    //图例位置
                    chartShiYang.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right; //靠右
                    chartShiYang.Legend.AlignmentVertical = LegendAlignmentVertical.Top; //顶部

                    //文字汇总信息
                    LoadSummary(dtSource);
                }
            }
        }

        /// <summary>
        /// 加载柱状图数据
        /// </summary>
        private void LoadBarData()
        {
            this.chartCureCount.Series.Clear();
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                //建立柱子
                Series serRenCi = new Series(string.Empty, ViewType.Bar);
                serRenCi.DataSource = dtResult.AsEnumerable().Where(row => !row["CURE_MONTH"].Equals("合计")).CopyToDataTable();
                //显示在下面的字段
                serRenCi.ArgumentDataMember = "CURE_MONTH";
                serRenCi.ArgumentScaleType = ScaleType.Qualitative;
                //柱状图的数据
                serRenCi.ValueDataMembers.AddRange(new string[] { "SUB_COUNT" });
                serRenCi.ValueScaleType = ScaleType.Numerical;
                //serRenCi.LegendText = "人次";
                chartCureCount.Series.Add(serRenCi);

                //设置纵坐标、横坐标的标题
                ((XYDiagram)chartCureCount.Diagram).AxisY.Title.Font = new Font("Tahoma", 10);
                ((XYDiagram)chartCureCount.Diagram).AxisY.Title.TextColor = System.Drawing.Color.Red;
                ((XYDiagram)chartCureCount.Diagram).AxisY.Title.Text = "透析人次";
                ((XYDiagram)chartCureCount.Diagram).AxisY.Title.Visible = true;
                ((XYDiagram)chartCureCount.Diagram).AxisX.Title.Text = "年度透析人次统计";
                ((XYDiagram)chartCureCount.Diagram).AxisX.Title.Visible = true;

                //图例位置
                //chartCureCount.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
                //chartCureCount.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
            }
        }

        /// <summary>
        /// 加载文字汇总信息
        /// </summary>
        /// <param name="dtSource"></param>
        private void LoadSummary(DataTable dtSource)
        {
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                string strMsg = string.Empty;
                int total = Utility.CInt(dtSource.Compute("Sum(SUB_COUNT)", string.Empty).ToString());
                dtSource.AsEnumerable().ToList().ForEach(row =>
                {
                    strMsg += string.Format("{0} <Color=blue>{1}</Color> 人/次，", row["PURIFICATION_MODE"].ToString(), row["SUB_COUNT"].ToString());
                });
                strMsg = strMsg.Substring(0, strMsg.Length - 1);

                this.lblInfo.Text = string.Format("当前时间段，透析人次共计 <Color=blue>{0}</Color> 人/次，{1}", total, strMsg);
            }
        }

        #endregion
    }
}
