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
using Hemo.Model;
using Hemo.IService;
using Hemo.Client.UI.Patient;

namespace Hemo.Client.UI.ReportChart
{
    public partial class InfectiousCountReport : ViewBase
    {
        #region 类变量

        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        private PatientModel.MED_PATIENTSDataTable _patientsYG = new PatientModel.MED_PATIENTSDataTable();


        private PatientModel.MED_PATIENTSDataTable _patientsBG = new PatientModel.MED_PATIENTSDataTable();
        private IPatient _patientService = ServiceManager.Instance.PatientService;

        private DataTable dtResult = null;

        private bool isConstant = true;

        #endregion

        #region 构造函数

        public InfectiousCountReport()
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
        private void InfectiousCountReport_Load(object sender, EventArgs e)
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
            reportNote.ReportType = ReportTypeEnum.乙肝丙肝转阳;
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
            this.gcCount.DataSource = dtResult;
            LoadPieData();

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
                DataTable dtSubCount = new DataTable();
                dtSubCount.Columns.Add("TYPE", typeof(string));
                dtSubCount.Columns.Add("COUNT", typeof(int));
                dtPatient.AsEnumerable().ToList().ForEach(row =>
                {
                    dtSubCount.Rows.Clear();
                    DataTable dtInfection = _hemodialysisService.GetInfectiousCountByParams(row["HEMODIALYSIS_ID"].ToString(), this.beginTime.DateTime, this.endTime.DateTime, "乙肝", string.Empty);
                    DataTable dtInfectionBin = _hemodialysisService.GetInfectiousCountByParams(row["HEMODIALYSIS_ID"].ToString(), this.beginTime.DateTime, this.endTime.DateTime, "丙肝", string.Empty);

                    #region 乙肝
                    var Negative = false;//阳性
                    var posetive = false;//阴性
                    var dtimeNow = string.Empty;
                    foreach (DataRow dr in dtInfection.Rows)
                    {
                        if (dr["RESULT"].ToString().Equals("阴性"))
                        {
                            posetive = true;
                        }
                        else if (dr["RESULT"].ToString().Equals("阳性"))
                        {
                            dtimeNow = dr["RESULT_DATE_TIME"].ToString();
                            Negative = true;
                        }
                        else
                        {
                            if (Utility.CDecimal(dr["RESULT"].ToString()) > 0.05m)
                            {
                                Negative = true;
                            }
                            else
                            {
                                posetive = true;
                            }
                        }
                    }
                    var rowInfect = dtSubCount.NewRow();
                    rowInfect["TYPE"] = "乙肝";
                    if (Negative && posetive)
                    {
                        var pati = _patientService.GetPatientListByHemoIds(row["HEMODIALYSIS_ID"].ToString());
                        var patiRow = pati[0];
                        patiRow.CREATE_DATE = Utility.CDate(dtimeNow);
                        patiRow.CREDENTIALS_TYPE = "阳性";
                        _patientsYG.LoadDataRow(patiRow.ItemArray, true);

                        rowInfect["COUNT"] = 1;
                    }
                    else
                        rowInfect["COUNT"] = 0;
                    dtSubCount.Rows.Add(rowInfect);
                    #endregion

                    #region 丙肝
                    var NegativeBin = false;//阳性
                    var posetiveBin = false;//阴性
                    dtimeNow = string.Empty;
                    foreach (DataRow dr in dtInfectionBin.Rows)
                    {
                        if (dr["RESULT"].ToString().Equals("阴性"))
                        {
                            posetiveBin = true;
                        }
                        else if (dr["RESULT"].ToString().Equals("阳性"))
                        {
                            dtimeNow = dr["RESULT_DATE_TIME"].ToString();
                            NegativeBin = true;
                        }
                        else
                        {
                            if (Utility.CDecimal(dr["RESULT"].ToString()) > 1)
                            {
                                Negative = true;
                            }
                            else
                            {
                                posetive = true;
                            }
                        }
                    }
                    var rowInfectBin = dtSubCount.NewRow();
                    rowInfectBin["TYPE"] = "丙肝";
                    if (NegativeBin && posetiveBin)
                    {
                        rowInfectBin["COUNT"] = 1;

                        var pati = _patientService.GetPatientListByHemoIds(row["HEMODIALYSIS_ID"].ToString());
                        var patiRow = pati[0];
                        patiRow.CREATE_DATE = Utility.CDate(dtimeNow);
                        patiRow.CREDENTIALS_TYPE = "阳性";
                        _patientsBG.LoadDataRow(patiRow.ItemArray, true);
                    }
                    else
                        rowInfectBin["COUNT"] = 0;
                    dtSubCount.Rows.Add(rowInfectBin);
                    #endregion

                    dtCount = dtCount ?? (dtSubCount != null ? dtSubCount.Clone() : dtCount);
                    if (dtSubCount != null && dtSubCount.Rows.Count > 0)
                    {
                        dtSubCount.AsEnumerable().ToList().ForEach(r =>
                        {
                            var findRow = dtCount.AsEnumerable().FirstOrDefault(i => i["TYPE"].ToString().Equals(r["TYPE"].ToString()));
                            if (findRow == null)
                            {
                                dtCount.ImportRow(r);
                            }
                            else
                            {
                                findRow["COUNT"] = Utility.CInt(findRow["COUNT"].ToString()) + Utility.CInt(r["COUNT"].ToString());
                            }
                        });
                    }

                });

                if (dtCount != null && dtCount.Rows.Count > 0)
                {
                    dtResult = dtCount.Clone();
                    dtCount.AsEnumerable().OrderBy(row => row["TYPE"].ToString()).CopyToDataTable(dtResult, LoadOption.OverwriteChanges);
                    var r = dtResult.NewRow();
                    r["TYPE"] = "合计";
                    r["COUNT"] = dtResult.Compute("Sum(COUNT)", string.Empty);
                    dtResult.Rows.Add(r);
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


        /// <summary>
        /// 加载饼图数据
        /// </summary>
        private void LoadPieData()
        {
            this.chartControl1.Series.Clear();
            this.chartControl1.Titles.Clear();

            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                var findRow = dtResult.AsEnumerable().FirstOrDefault(row => row["TYPE"].ToString().Equals("合计"));
                if (findRow != null)
                {
                    dtResult.Rows.Remove(findRow);

                    Series serAccess = new Series(string.Empty, ViewType.Pie);
                    serAccess.DataSource = dtResult;
                    ChartTitle ct = new ChartTitle();
                    ct.Text = string.Empty;
                    ct.Text = "乙肝、丙肝转阳例数";
                    ct.TextColor = Color.Black;//颜色
                    ct.Font = new Font("Tahoma", 12);//字体
                    ct.Dock = ChartTitleDockStyle.Top;//停靠在上方
                    ct.Alignment = StringAlignment.Center;//居中显示

                    this.chartControl1.Titles.Add(ct);

                    DevExpress.XtraCharts.PieSeriesView pieSeriesView = new DevExpress.XtraCharts.PieSeriesView();
                    pieSeriesView.Rotation = 90;
                    pieSeriesView.ExplodeMode = PieExplodeMode.MaxValue;//突出显示最大的饼块。
                    pieSeriesView.RuntimeExploding = true;//设置了
                    serAccess.View = pieSeriesView;

                    serAccess.PointOptions.PointView = PointView.ArgumentAndValues;//显示表示的信息和数据
                    serAccess.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;//用百分比表示
                    serAccess.PointOptions.ValueNumericOptions.Precision = 1;//百分号前面的数字不跟小数点
                    serAccess.ValueDataMembers.AddRange(new string[] { "COUNT" });//绑定值的字段
                    serAccess.ValueScaleType = ScaleType.Numerical;//数字类型
                    serAccess.ArgumentDataMember = "TYPE";//绑定饼块的描述文字
                    serAccess.ArgumentScaleType = ScaleType.Qualitative;//定性的
                    this.chartControl1.Series.Add(serAccess);

                    //图例位置
                    this.chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right; //靠右
                    this.chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Top; //顶部
                }
            }
        }



        /// <summary>
        /// 创建图形对你
        /// </summary>
        /// <param name="dt"></param>
        private void CreateChart(DataTable dt)
        {
            if (dt == null) return;
            dt.Columns.Remove("SUB_COUNT");

            #region 数据整理适合线性显示
            var lineDtResult = new DataTable("ChartData");
            lineDtResult.Columns.Add(new DataColumn("类型"));
            List<string> listTime = new List<string>();

            foreach (DataRow dr in dt.Rows)
            {
                //加入list
                if (!listTime.Contains(dr[0].ToString()))
                {
                    listTime.Add(dr[0].ToString());
                }

            }
            //排序后加入到table列
            List<string> listTime1 = listTime.OrderBy(i => i).ToList<string>();
            foreach (var colunname in listTime1)
            {
                //动态添加列
                if (!lineDtResult.Columns.Contains(colunname))
                {
                    lineDtResult.Columns.Add(new DataColumn(colunname, typeof(string)));
                }
            }
            //加行
            for (var i = 1; i < dt.Columns.Count; i++)
            {
                var row = lineDtResult.NewRow();
                row["类型"] = dt.Columns[i].ToString().Contains("NL") ? "内瘘例数" : "中心静脉导管例数";
                lineDtResult.Rows.Add(row);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString().Contains("合计"))
                {
                    continue;
                }
                for (int j = 1; j < lineDtResult.Columns.Count; j++)
                {
                    if (dt.Rows[i][0].ToString().Equals(lineDtResult.Columns[j].ColumnName))
                    {
                        lineDtResult.Rows[0][j] = dt.Rows[i][1].ToString();
                    }
                    if (dt.Rows[i][0].ToString().Equals(lineDtResult.Columns[j].ColumnName))
                    {
                        lineDtResult.Rows[1][j] = dt.Rows[i][2].ToString();
                    }
                }
            }

            #endregion

            #region Series
            ////创建几个图形的对象
            List<Series> list = new List<Series>();
            for (int i = 0; i < lineDtResult.Rows.Count; i++)
            {
                list.Add(CreateSeries(lineDtResult.Rows[i][0].ToString(), ViewType.Line, lineDtResult, i));
            }
            #endregion

            chartControl1.Series.Clear();
            chartControl1.Series.AddRange(list.ToArray());
            chartControl1.Legend.Visible = false;
            chartControl1.SeriesTemplate.Label.Visible = true;// DefaultBoolean.True;//LabelsVisibility
            ((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Clear();
            if (lineDtResult.Columns.Count > 12)
            {
                ((XYDiagram)this.chartControl1.Diagram).AxisX.VisualRange.MinValue = lineDtResult.Columns[1].ColumnName;
                ((XYDiagram)this.chartControl1.Diagram).AxisX.VisualRange.MaxValue = lineDtResult.Columns[11].ColumnName;
            }

            for (int i = 0; i < list.Count; i++)
            {
                //设置图表线型颜色
                //list[i].View.Color = colorList[i];
                //创建图表的第二坐标系
                //CreateAxisY(list[i]);
            }
        }

        /// <summary>
        /// 根据数据创建一个图形展现
        /// </summary>
        /// <param name="caption">图形标题</param>
        /// <param name="viewType">图形类型</param>
        /// <param name="dt">数据DataTable</param>
        /// <param name="rowIndex">图形数据的行序号</param>
        /// <returns></returns>
        private Series CreateSeries(string caption, ViewType viewType, DataTable dt, int rowIndex)
        {
            Series series = new Series(caption, viewType);
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                string argument = dt.Columns[i].ColumnName;//参数名称
                //decimal value = (decimal)dt.Rows[rowIndex][i];//参数值
                if (string.IsNullOrEmpty(dt.Rows[rowIndex][i].ToString()))
                    continue;
                string value = dt.Rows[rowIndex][i].ToString();//参数值

                series.Points.Add(new SeriesPoint(argument, value));
            }

            //必须设置ArgumentScaleType的类型，否则显示会转换为日期格式，导致不是希望的格式显示
            //也就是说，显示字符串的参数，必须设置类型为ScaleType.Qualitative
            series.ArgumentScaleType = ScaleType.Qualitative;
            series.Label.Visible = true;//.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;//显示标注标签
            series.Label.ResolveOverlappingMode = ResolveOverlappingMode.JustifyAllAroundPoint;
            return series;
        }


        /// <summary>
        /// 创建图表的第二坐标系
        /// </summary>
        /// <param name="series">Series对象</param>
        /// <returns></returns>
        private SecondaryAxisY CreateAxisY(Series series)
        {
            SecondaryAxisY myAxis = new SecondaryAxisY(series.Name);
            ((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Add(myAxis);
            ((LineSeriesView)series.View).AxisY = myAxis;
            myAxis.Title.Text = series.Name;
            myAxis.Title.Alignment = StringAlignment.Far; //顶部对齐
            myAxis.Title.Visible = true; //显示标题
            myAxis.Title.Font = new Font("宋体", 9.0f);

            Color color = series.View.Color;//设置坐标的颜色和图表线条颜色一致

            myAxis.Title.TextColor = color;
            myAxis.Label.TextColor = color;
            myAxis.Color = color;

            return myAxis;
        }



        #endregion

        private void gvCount_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            DataRowView dr = this.gvCount.GetFocusedRow() as DataRowView;
            if (e.Clicks == 2 && dr["TYPE"].ToString().Equals("乙肝"))
            {
                PatientsListForIntegrated frm = new PatientsListForIntegrated();
                frm.SetColumnVisitble(true);
                frm._patient = _patientsYG;
                frm.Text = "乙肝转阳患者列表";
                frm.ShowDialog();
            }
            else if (e.Clicks == 2 && dr["TYPE"].ToString().Equals("丙肝"))
            {
                PatientsListForIntegrated frm = new PatientsListForIntegrated();
                frm.SetColumnVisitble(true);
                frm._patient = _patientsBG;
                frm.Text = "丙肝转阳患者列表";
                frm.ShowDialog();
            }
        }


    }
}
