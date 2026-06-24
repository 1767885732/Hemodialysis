/*----------------------------------------------------------------
 * Copyright (C) 2005 麦迪斯顿(苏州)医疗科技发展有限公司
 * 文件功能描述:导管手术例数统计查询类
 * 创建标识:吕志强-2017年4月25日
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
using Hemo.Utilities;
using Hemo.IService.Config;
using Hemo.Service;
using DevExpress.XtraCharts;
using System.Drawing.Imaging;

namespace Hemo.Client.UI.ReportChart
{
    public partial class BloodControlReport : ViewBase
    {
        #region 类变量

        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        private DataTable dtResult = null;

        private bool isConstant = true;

        #endregion

        #region 构造函数

        public BloodControlReport()
        {
            InitializeComponent();
            this.beginTime.DateTime = DateTime.Now.AddYears(-1);
            //this.beginTime.DateTime = DateTime.Now.AddDays(-DateTime.Now.Day + 1).AddMonths(-DateTime.Now.Month + 1);
            //this.endTime.DateTime = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.Day);
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BloodControlReport_Load(object sender, EventArgs e)
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
                fileDialog.FileName = this.beginTime.DateTime.ToString("yyyyMMdd") + "-" + this.endTime.DateTime.ToString("yyyyMMdd") + "导管手术例数统计";
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
            reportNote.ReportType = ReportTypeEnum.血压控制例数;
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
            fileDialog.FileName = this.beginTime.DateTime.ToString("yyyyMMdd") + "-" + this.endTime.DateTime.ToString("yyyyMMdd") + "导管手术例数统计";
            fileDialog.RestoreDirectory = true;
            DialogResult dialogResult = fileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                this.chartControl1.ExportToImage(fileDialog.FileName, ImageFormat.Jpeg);
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
            this.busyIndicator.HideLoadingScreen();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 启动后台线程
        /// </summary>
        private void RunBackgroundWorker()
        {
            this.busyIndicator.ShowLoadingScreenFor(this.chartControl1);

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
                    DataTable dtSubCount = _hemodialysisService.GetBloodControlsReport(row["HEMODIALYSIS_ID"].ToString(), this.beginTime.DateTime, this.endTime.DateTime);
                    dtCount = dtCount ?? (dtSubCount != null ? dtSubCount.Clone() : dtCount);
                    if (dtSubCount != null && dtSubCount.Rows.Count > 0)
                    {
                        dtSubCount.AsEnumerable().ToList().ForEach(r =>
                        {
                            var findRow = dtCount.AsEnumerable().FirstOrDefault(i => i["CREATE_MONTH"].ToString().Equals(r["CREATE_MONTH"].ToString()));
                            if (findRow == null)
                            {
                                dtCount.ImportRow(r);
                            }
                            else
                            {
                                findRow["XYKZ_COUNT"] = Utility.CInt(findRow["XYKZ_COUNT"].ToString()) + Utility.CInt(r["XYKZ_COUNT"].ToString());
                                findRow["HIGH_COUNT"] = Utility.CInt(findRow["HIGH_COUNT"].ToString()) + Utility.CInt(r["HIGH_COUNT"].ToString());
                                findRow["LOW_COUNT"] = Utility.CInt(findRow["LOW_COUNT"].ToString()) + Utility.CInt(r["LOW_COUNT"].ToString());
                                findRow["SUB_COUNT"] = Utility.CInt(findRow["SUB_COUNT"].ToString()) + Utility.CInt(r["SUB_COUNT"].ToString());
                            }
                        });
                    }
                });

                if (dtCount != null && dtCount.Rows.Count > 0)
                {
                    dtResult = dtCount.Clone();
                    dtCount.AsEnumerable().OrderBy(row => row["CREATE_MONTH"].ToString()).CopyToDataTable(dtResult, LoadOption.OverwriteChanges);
                    var r = dtResult.NewRow();
                    r["CREATE_MONTH"] = "合计";
                    r["XYKZ_COUNT"] = dtResult.Compute("Sum(XYKZ_COUNT)", string.Empty);
                    r["HIGH_COUNT"] = dtResult.Compute("Sum(HIGH_COUNT)", string.Empty);
                    r["LOW_COUNT"] = dtResult.Compute("Sum(LOW_COUNT)", string.Empty);
                    r["SUB_COUNT"] = dtResult.Compute("Sum(SUB_COUNT)", string.Empty);
                    dtResult.Rows.Add(r);
                }
            }
        }

        /// <summary>
        /// 加载饼图数据
        /// </summary>
        private void LoadPieData()
        {
            this.chartControl1.Series.Clear();
            this.chartControl1.Titles.Clear();
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                var findRow = dtResult.AsEnumerable().FirstOrDefault(row => row["CREATE_MONTH"].ToString().Equals("合计"));
                if (findRow != null)
                {
                    DataTable dtSource = new DataTable();
                    dtSource.Columns.Add("ACCESS_TYPE", typeof(string));
                    dtSource.Columns.Add("SUB_COUNT", typeof(int));
                    foreach (DataColumn column in dtResult.Columns)
                    {
                        if (column.ColumnName.Equals("CREATE_MONTH") || column.ColumnName.Equals("SUB_COUNT"))
                        {
                            continue;
                        }
                        var row = dtSource.NewRow();
                        string type = column.ColumnName.Substring(0, column.ColumnName.IndexOf("_"));

                        row["ACCESS_TYPE"] = type.Equals("XYKZ") ? "血压控制例数" : type.Equals("HIGH") ? "高压例数" : "低压例数";
                        row["SUB_COUNT"] = findRow[column.ColumnName];
                        dtSource.Rows.Add(row);
                    }

                    Series serAccess = new Series(string.Empty, ViewType.Pie);
                    serAccess.DataSource = dtSource;
                    ChartTitle ct = new ChartTitle();
                    ct.Text = string.Empty;
                    ct.Text = "血压控制例数：";
                    ct.TextColor = Color.Black;//颜色
                    ct.Font = new Font("Tahoma", 12);//字体
                    ct.Dock = ChartTitleDockStyle.Top;//停靠在上方
                    ct.Alignment = StringAlignment.Center;//居中显示
                    dtSource.AsEnumerable().ToList().ForEach(row =>
                    {
                        ct.Text += string.Format("{0}：{1}人 ", row["ACCESS_TYPE"].ToString(), row["SUB_COUNT"].ToString());
                    });
                    this.chartControl1.Titles.Add(ct);

                    DevExpress.XtraCharts.PieSeriesView pieSeriesView = new DevExpress.XtraCharts.PieSeriesView();
                    pieSeriesView.Rotation = 90;
                    pieSeriesView.ExplodeMode = PieExplodeMode.MaxValue;//突出显示最大的饼块。
                    pieSeriesView.RuntimeExploding = true;//设置了
                    serAccess.View = pieSeriesView;

                    serAccess.PointOptions.PointView = PointView.ArgumentAndValues;//显示表示的信息和数据
                    serAccess.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;//用百分比表示
                    serAccess.PointOptions.ValueNumericOptions.Precision = 1;//百分号前面的数字不跟小数点
                    serAccess.ValueDataMembers.AddRange(new string[] { "SUB_COUNT" });//绑定值的字段
                    serAccess.ValueScaleType = ScaleType.Numerical;//数字类型
                    serAccess.ArgumentDataMember = "ACCESS_TYPE";//绑定饼块的描述文字
                    serAccess.ArgumentScaleType = ScaleType.Qualitative;//定性的
                    this.chartControl1.Series.Add(serAccess);

                    //图例位置
                    this.chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right; //靠右
                    this.chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Top; //顶部
                }
            }
        }
        private void labelControl3_DoubleClick(object sender, EventArgs e)
        {
            this.endTime.Enabled = !this.endTime.Enabled;
        }

        private void beginTime_EditValueChanged(object sender, EventArgs e)
        {
            this.endTime.DateTime = this.beginTime.DateTime.AddYears(1);
        }
        #endregion


    }
}
