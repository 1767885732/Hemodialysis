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
using Hemo.IService;
using Hemo.Client.UI.Patient;

namespace Hemo.Client.UI.ReportChart
{
    public partial class QueryCureVascularType : ViewBase
    {
        #region 类变量

        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        private IPatient _patientService = ServiceManager.Instance.PatientService;

        private IVascuarAccess _vascularAccessService = ServiceManager.Instance.VascuarAccessService;

        private DataTable dtResult = null;

        private bool isConstant = true;

        #endregion

        #region 构造函数

        public QueryCureVascularType()
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
        private void QueryCureVascularType_Load(object sender, EventArgs e)
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
            reportNote.ReportType = ReportTypeEnum.通路类别人数;
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
            CreateChart(dtResult);
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

        private string nlhemos = string.Empty;
        private string zghemos = string.Empty;

        private string nlhemosNames = string.Empty;
        private string zghemosNames = string.Empty;
        /// <summary>
        /// 加载报表数据
        /// </summary>
        private void LoadChartData()
        {
            nlhemos = string.Empty;
            zghemos = string.Empty;
            nlhemosNames = string.Empty;
            zghemosNames = string.Empty;
            dtResult = null;
            DataTable dtPatient = isConstant ? _hemodialysisService.GetHemoIdInLastWeekAndThreeMonthsByDate(this.beginTime.DateTime, this.endTime.DateTime) : _hemodialysisService.GetHemoIdByDate(this.beginTime.DateTime, this.endTime.DateTime);
            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                DataTable dtCount = null;
                dtPatient.AsEnumerable().ToList().ForEach(row =>
                {
                    DataTable dtSubCount = _hemodialysisService.GetCureVascularTypeCountByHemoIdAndDate(row["HEMODIALYSIS_ID"].ToString(), this.beginTime.DateTime, this.endTime.DateTime);
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
                                findRow["NL_COUNT"] = Utility.CInt(findRow["NL_COUNT"].ToString()) + Utility.CInt(r["NL_COUNT"].ToString());
                                findRow["ZXJM_COUNT"] = Utility.CInt(findRow["ZXJM_COUNT"].ToString()) + Utility.CInt(r["ZXJM_COUNT"].ToString());
                                findRow["SUB_COUNT"] = Utility.CInt(findRow["SUB_COUNT"].ToString()) + Utility.CInt(r["SUB_COUNT"].ToString());
                            }
                            if (Utility.CInt(r["NL_COUNT"].ToString()) > 0)
                            {
                                nlhemos += row["HEMODIALYSIS_ID"].ToString() + "','";
                            }
                            if (Utility.CInt(r["NL_COUNT"].ToString()) > 1)
                            {
                                nlhemosNames += row["HEMODIALYSIS_ID"].ToString() + "','";
                            }

                            if (Utility.CInt(r["ZXJM_COUNT"].ToString()) > 0)
                            {
                                zghemos += row["HEMODIALYSIS_ID"].ToString() + "','";
                            }

                            if (Utility.CInt(r["ZXJM_COUNT"].ToString()) > 1)
                            {
                                zghemosNames += row["HEMODIALYSIS_ID"].ToString() + "','";
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
                    r["NL_COUNT"] = dtResult.Compute("Sum(NL_COUNT)", string.Empty);
                    r["ZXJM_COUNT"] = dtResult.Compute("Sum(ZXJM_COUNT)", string.Empty);
                    r["SUB_COUNT"] = dtResult.Compute("Sum(SUB_COUNT)", string.Empty);
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

                        row["ACCESS_TYPE"] = type.Equals("NL") ? "内瘘" : "中心静脉导管";
                        row["SUB_COUNT"] = findRow[column.ColumnName];
                        dtSource.Rows.Add(row);
                    }

                    Series serAccess = new Series(string.Empty, ViewType.Pie);
                    serAccess.DataSource = dtSource;
                    ChartTitle ct = new ChartTitle();
                    ct.Text = string.Empty;
                    ct.Text = "血管通路类别例数：";
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
        /// <summary>
        /// 创建图形对你
        /// </summary>
        /// <param name="dt"></param>
        private void CreateChart(DataTable dtResult)
        {
            if (dtResult == null) return;
            var dt = dtResult.Copy();

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

        private void gvCount_DoubleClick(object sender, EventArgs e)
        {

        }
        private void dxSimpleButton1_Click(object sender, EventArgs e)
        {
            var hightNames = _patientService.GetPatientListByHemoIds(nlhemosNames);
            var names = string.Empty;
            foreach (var item in hightNames)
            {
                names += item.NAME;
            }
            var patients = _patientService.GetPatientListByHemoIds(nlhemos);
            foreach (var row in patients)
            {
                var itemDt = _vascularAccessService.GetVascularAccessListByHEMODIALYSIS_ID(row.HEMODIALYSIS_ID);
                if (itemDt.Rows.Count <= 0) continue;
                var varscularName = string.Empty;
                foreach (var itemdtRow in itemDt)
                {
                    var itemDtName = _vascularAccessService.GetVascularAccessAllName(itemdtRow.VASCULAR_ACCESS_ID);
                    var vascualrDt = Utility.CDate(itemDtName.Rows[0]["CREATE_DATE"].ToString());
                    if (vascualrDt >= this.beginTime.DateTime && vascualrDt <= this.endTime.DateTime)
                    {
                        varscularName += string.Format("时间:{0} 名称:{1}",vascualrDt.ToString("yyyy-mm-dd"), itemDtName.Rows[0]["VA"].ToString());
                        //break;
                    }
                }
                row.CREATE_DATE = itemDt[0].CREATE_DATE;
                row.CREDENTIALS_TYPE = varscularName;
            }
            PatientsListForIntegrated frm = new PatientsListForIntegrated();
            frm.SetColumnVisitble(true);
            frm.Text = string.Format("内瘘患者：{0} --  {1}", this.beginTime.DateTime.ToString("yyyy-MM-dd"), this.endTime.DateTime.ToString("yyyy-MM-dd"));
            //frm.labTips.Text = names + "有多个通路";
            frm._patient = patients;
            frm.ShowDialog();
        }

        private void dxSimpleButton2_Click(object sender, EventArgs e)
        {
            var hightNames = _patientService.GetPatientListByHemoIds(zghemosNames);
            var names = string.Empty;
            foreach (var item in hightNames)
            {
                names += item.NAME;
            }
            var patients = _patientService.GetPatientListByHemoIds(zghemos);
            foreach (var row in patients)
            {
                var itemDt = _vascularAccessService.GetVascularAccessListByHEMODIALYSIS_ID(row.HEMODIALYSIS_ID);
                var varscularName = string.Empty;
                foreach (var itemdtRow in itemDt)
                {
                    var itemDtName = _vascularAccessService.GetVascularAccessAllName(itemdtRow.VASCULAR_ACCESS_ID);
                    var vascualrDt = Utility.CDate(itemDtName.Rows[0]["CREATE_DATE"].ToString());
                    if (vascualrDt >= this.beginTime.DateTime && vascualrDt <= this.endTime.DateTime)
                    {
                        varscularName += string.Format("时间:{0} 名称:{1}", vascualrDt.ToString("yyyy-mm-dd"), itemDtName.Rows[0]["VA"].ToString());
                        //break;
                    }
                }
                row.CREATE_DATE = itemDt[0].CREATE_DATE;
                row.CREDENTIALS_TYPE = varscularName;
            }
            PatientsListForIntegrated frm = new PatientsListForIntegrated();
            frm._patient = patients;
            frm.SetColumnVisitble(true);
            frm.Text = string.Format("导管患者：{0} --  {1}", this.beginTime.DateTime.ToString("yyyy-MM-dd"), this.endTime.DateTime.ToString("yyyy-MM-dd"));
            //frm.labTips.Text = names + "有多个通路";
            frm.ShowDialog();
        }

        private void gcCount_Click(object sender, EventArgs e)
        {

        }


    }
}
