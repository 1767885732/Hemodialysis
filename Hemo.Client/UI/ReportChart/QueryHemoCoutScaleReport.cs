/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:规律透析统计查询类
 * 创建标识:吕志强-2017年4月24日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Client.UI.Machine;
using Hemo.IService.Config;
using Hemo.Service;
using DevExpress.XtraCharts;
using Hemo.Utilities;
using Hemo.IService.PatientSchedule;
using System.Drawing.Imaging;

namespace Hemo.Client.UI.ReportChart
{
    public partial class QueryHemoCoutScaleReport : ViewBase
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

        public QueryHemoCoutScaleReport()
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
        private void QueryHemoCoutScaleReport_Load(object sender, EventArgs e)
        {
            RunBackgroundWorker();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Query_Click(object sender, EventArgs e)
        {
            isConstant = this.chkConstant.Checked;
            RunBackgroundWorker();
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
                fileDialog.FileName = this.beginTime.DateTime.ToString("yyyyMMdd") + "-" + this.endTime.DateTime.ToString("yyyyMMdd") + "规律透析人数统计";
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
            reportNote.ReportType = ReportTypeEnum.规律透析比例;
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
            fileDialog.FileName = this.beginTime.DateTime.ToString("yyyyMMdd") + "-" + this.endTime.DateTime.ToString("yyyyMMdd") + "规律透析人数统计";
            fileDialog.RestoreDirectory = true;
            DialogResult dialogResult = fileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                this.chartRegular.ExportToImage(fileDialog.FileName, ImageFormat.Jpeg);
            }
        }

        private void work_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadChartData();
        }

        private void work_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadPieData();
            this.gcCount.DataSource = dtResult;
            busyIndicator.HideLoadingScreen();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 首页调用时隐藏检索条件
        /// </summary>
        public void SetSearchBarDisplayNone()
        {
            this.gcCount.Visible = false;
            this.panelControl1.Visible = false;
            this.tableLayoutPanel1.SetRowSpan(this.chartRegular, 3);
        }

        /// <summary>
        /// 启动后台线程
        /// </summary>
        private void RunBackgroundWorker()
        {
            if (isFirstPage)
            {
                busyIndicator.ShowLoadingScreenFor(this.Parent);
            }
            else
            {
                busyIndicator.ShowLoadingScreenFor(this.chartRegular);
            }

            using (BackgroundWorker work = new BackgroundWorker())
            {
                work.DoWork += new DoWorkEventHandler(work_DoWork);
                work.RunWorkerCompleted += new RunWorkerCompletedEventHandler(work_RunWorkerCompleted);
                work.RunWorkerAsync();
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
                    DataTable dtSubCount = _hemodialysisService.GetRegularCountByHemoIdAndDate(row["HEMODIALYSIS_ID"].ToString(), this.beginTime.DateTime, this.endTime.DateTime);
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
                                findRow["TWO_TIME"] = Utility.CInt(findRow["TWO_TIME"].ToString()) + Utility.CInt(r["TWO_TIME"].ToString());
                                findRow["THREE_TIME"] = Utility.CInt(findRow["THREE_TIME"].ToString()) + Utility.CInt(r["THREE_TIME"].ToString());
                                findRow["FOUR_TIME"] = Utility.CInt(findRow["FOUR_TIME"].ToString()) + Utility.CInt(r["FOUR_TIME"].ToString());
                                findRow["FIVE_TIME"] = Utility.CInt(findRow["FIVE_TIME"].ToString()) + Utility.CInt(r["FIVE_TIME"].ToString());
                            }
                        });
                    }
                });

                if (dtCount != null && dtCount.Rows.Count > 0)
                {
                    dtResult = dtCount.Clone();
                    dtCount.AsEnumerable().OrderBy(row => row["CURE_MONTH"].ToString()).CopyToDataTable(dtResult, LoadOption.OverwriteChanges);
                    dtResult.Rows[0]["SUB_COUNT"] = dtPatient.Rows.Count;
                    dtResult.Rows[0]["UNREGULAR"] = Utility.CInt(dtResult.Rows[0]["SUB_COUNT"].ToString()) - Utility.CInt(dtResult.Rows[0]["TWO_TIME"].ToString()) - Utility.CInt(dtResult.Rows[0]["THREE_TIME"].ToString()) - Utility.CInt(dtResult.Rows[0]["FOUR_TIME"].ToString()) - Utility.CInt(dtResult.Rows[0]["FIVE_TIME"].ToString());
                    var r = dtResult.NewRow();
                    r["CURE_MONTH"] = "合计";
                    r["SUB_COUNT"] = dtResult.Compute("Sum(SUB_COUNT)", string.Empty);
                    r["TWO_TIME"] = dtResult.Compute("Sum(TWO_TIME)", string.Empty);
                    r["THREE_TIME"] = dtResult.Compute("Sum(THREE_TIME)", string.Empty);
                    r["FOUR_TIME"] = dtResult.Compute("Sum(FOUR_TIME)", string.Empty);
                    r["FIVE_TIME"] = dtResult.Compute("Sum(FIVE_TIME)", string.Empty);
                    r["UNREGULAR"] = dtResult.Compute("Sum(UNREGULAR)", string.Empty);
                    dtResult.Rows.Add(r);
                }
            }
        }

        /// <summary>
        /// 加载饼图数据
        /// </summary>
        private void LoadPieData()
        {
            this.chartRegular.Series.Clear();
            this.chartRegular.Titles.Clear();
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                var findRow = dtResult.AsEnumerable().FirstOrDefault(row => row["CURE_MONTH"].ToString().Equals("合计"));
                if (findRow != null)
                {
                    DataTable dtSource = new DataTable();
                    dtSource.Columns.Add("REGULAR_TYPE", typeof(string));
                    dtSource.Columns.Add("SUB_COUNT", typeof(int));
                    foreach (DataColumn column in dtResult.Columns)
                    {
                        if (column.ColumnName.Equals("CURE_MONTH") || column.ColumnName.Equals("SUB_COUNT"))
                        {
                            continue;
                        }
                        var row = dtSource.NewRow();
                        string type = column.ColumnName.Equals("UNREGULAR") ? "UNREGULAR" : column.ColumnName.Substring(0, column.ColumnName.IndexOf("_"));
                        row["REGULAR_TYPE"] = type.Equals("UNREGULAR") ? "无规律" : (type.Equals("TWO") ? "每周2次" : (type.Equals("THREE") ? "每周3次" : (type.Equals("FOUR") ? "每周4次" : "每周5次")));
                        row["SUB_COUNT"] = findRow[column.ColumnName];
                        dtSource.Rows.Add(row);
                    }

                    //新建一个大饼
                    Series serRegular = new Series(string.Empty, ViewType.Pie);//往控件中新增一个饼图
                    serRegular.DataSource = dtSource;//设置数据源
                    ChartTitle ct = new ChartTitle();
                    ct.Text = this.beginTime.DateTime.ToShortDateString() + "～" + this.endTime.DateTime.ToShortDateString() + " ";
                    ct.TextColor = Color.Black;//颜色
                    ct.Font = new Font("Tahoma", 12);//字体
                    ct.Dock = ChartTitleDockStyle.Top;//停靠在上方
                    ct.Alignment = StringAlignment.Center;//居中显示
                    dtSource.AsEnumerable().ToList().ForEach(row =>
                    {
                        ct.Text += string.Format("{0}：{1}人 ", row["REGULAR_TYPE"].ToString(), row["SUB_COUNT"].ToString());
                    });
                    this.chartRegular.Titles.Add(ct);

                    DevExpress.XtraCharts.PieSeriesView pieSeriesView = new DevExpress.XtraCharts.PieSeriesView();
                    pieSeriesView.Rotation = 90;
                    pieSeriesView.ExplodeMode = PieExplodeMode.MaxValue;// //突出显示最大的饼块。
                    pieSeriesView.RuntimeExploding = true;//设置了
                    serRegular.View = pieSeriesView;

                    serRegular.PointOptions.PointView = PointView.ArgumentAndValues;//设置饼图上的lable显示 文字/比率值 
                    serRegular.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;//用百分比表示
                    serRegular.PointOptions.ValueNumericOptions.Precision = 1;//百分号前面的数字不跟小数点
                    PieSeriesLabel label = serRegular.Label as PieSeriesLabel;
                    label.Position = PieSeriesLabelPosition.TwoColumns; //设置饼图上lable的显示方式，此方式将独立出一个列显示lable

                    //饼图的数据
                    serRegular.ValueDataMembers.AddRange(new string[] { "SUB_COUNT" });//绑定值的字段
                    serRegular.ValueScaleType = ScaleType.Numerical;//值的类型
                    serRegular.ArgumentDataMember = "REGULAR_TYPE";//绑定饼块的描述文字
                    serRegular.ArgumentScaleType = ScaleType.Qualitative;
                    //数据以百分比显示时只能是Default和None
                    ((PieSeriesLabel)serRegular.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    chartRegular.Series.Add(serRegular);

                    //图例位置
                    chartRegular.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right; //靠右
                    chartRegular.Legend.AlignmentVertical = LegendAlignmentVertical.Top; //顶部
                }
            }
        }

        #endregion
    }
}
