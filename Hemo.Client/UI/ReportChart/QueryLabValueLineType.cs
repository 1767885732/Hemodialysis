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

namespace Hemo.Client.UI.ReportChart
{
    public partial class QueryLabValueLineType : ViewBase
    {
        #region 类变量

        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IPatient _patientService = ServiceManager.Instance.PatientService;

        private DataTable dtResult = null;
        private DataTable dtUnminresult = null;
        private DataTable dtUnmaxresult = null;


        private bool isConstant = true;

        #endregion

        #region 构造函数

        public QueryLabValueLineType()
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
        private void QueryLabValueLineType_Load(object sender, EventArgs e)
        {
            InzationControls();
            RunBackgroundWorker();
        }
        private void InzationControls()
        {
            var dataComm = _configService.GetConfigList("", "血透检验导出", "检验项目明细配置", "1");
            this.lupItems.Properties.DataSource = dataComm;

            var patientDt = _patientService.GetPatientList();
            this.lupPatients.Properties.DataSource = patientDt;
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
            if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                if (this.gvCount.RowCount > 0)
                {
                    SaveFileDialog fileDialog = new SaveFileDialog();
                    fileDialog.Title = "导出Excel";
                    fileDialog.Filter = "Excel文件(*.xls)|*.xls";
                    fileDialog.FileName = this.beginTime.DateTime.ToString("yyyyMMdd") + "-" + this.endTime.DateTime.ToString("yyyyMMdd") + this.lupItems.EditValue.ToString();
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
            else
            {
                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.Title = "导出Excel";
                fileDialog.Filter = "Excel文件(*.xls)|*.xls";
                fileDialog.FileName = this.beginTime.DateTime.ToString("yyyyMMdd") + "-" + this.endTime.DateTime.ToString("yyyyMMdd") + this.lupItems.EditValue.ToString();
                fileDialog.RestoreDirectory = true;
                DialogResult dialogResult = fileDialog.ShowDialog(this);
                if (dialogResult == DialogResult.OK)
                {
                    DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                    options.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
                    this.gridControl1.ExportToXls(fileDialog.FileName, options);
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
            reportNote.ReportType = ReportTypeEnum.检验导出数据;
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
            fileDialog.FileName = this.beginTime.DateTime.ToString("yyyyMMdd") + "-" + this.endTime.DateTime.ToString("yyyyMMdd") + this.lupItems.EditValue.ToString();
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
            if (dtResult == null)
            {
                this.busyIndicator.HideLoadingScreen();
                return;
            }
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;

            this.gcCount.DataSource = dtResult;
            this.gridControl1.DataSource = dtUnmaxresult;
            this.gridControl2.DataSource = dtUnminresult;
            if (dtResult.Rows.Count > 0)
            {
                this.xtraTabPage1.Text = string.Format("达标人员:{0}人", dtResult.Rows.Count);
            }  
            if (dtUnmaxresult.Rows.Count > 0)
            {
                this.xtraTabPage2.Text = string.Format("高于达标范围人员:{0}人", dtUnmaxresult.Rows.Count);
            }
            if (dtUnminresult.Rows.Count > 0)
            {
                this.xtraTabPage3.Text = string.Format("低于达标范围人员:{0}人", dtUnminresult.Rows.Count);
            }
            if (dtUnmaxresult == null || dtUnmaxresult.Rows.Count <= 0)
            {
                this.xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            }
            else
            {
                this.xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
            }

            var dtPieReport = dtResult.Clone();

            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                foreach (DataRow dr in dtResult.Rows)
                {
                    DataRow newRow = dtPieReport.NewRow();
                    newRow["CREATE_MONTH"] = dr["CREATE_MONTH"];
                    newRow["INSTRUMENT_ID"] = dr["INSTRUMENT_ID"].ToString();
                    newRow["REPORT_ITEM_NAME"] = dr["REPORT_ITEM_NAME"];
                    newRow["RESULT"] = dr["RESULT"];
                    dtPieReport.Rows.Add(newRow);
                }
            }
            if (dtUnmaxresult != null && dtUnmaxresult.Rows.Count > 0)
            {
                foreach (DataRow dr in dtUnmaxresult.Rows)
                {
                    DataRow newRow = dtPieReport.NewRow();
                    newRow["CREATE_MONTH"] = dr["CREATE_MONTH"];
                    newRow["INSTRUMENT_ID"] = dr["INSTRUMENT_ID"].ToString();
                    newRow["REPORT_ITEM_NAME"] = dr["REPORT_ITEM_NAME"];
                    newRow["RESULT"] = dr["RESULT"];
                    dtPieReport.Rows.Add(newRow);
                }
            }
            if (dtUnminresult != null && dtUnminresult.Rows.Count > 0)
            {
                foreach (DataRow dr in dtUnminresult.Rows)
                {
                    DataRow newRow = dtPieReport.NewRow();
                    newRow["CREATE_MONTH"] = dr["CREATE_MONTH"];
                    newRow["INSTRUMENT_ID"] = dr["INSTRUMENT_ID"].ToString();
                    newRow["REPORT_ITEM_NAME"] = dr["REPORT_ITEM_NAME"];
                    newRow["RESULT"] = dr["RESULT"];
                    dtPieReport.Rows.Add(newRow);
                }
            }


            var queryAll = from t in dtPieReport.AsEnumerable()
                           group t by new { month = Utility.CDate(t.Field<string>("CREATE_MONTH")).ToString("yyyy-MM") } into m
                           select new
                           {
                               month = m.Key.month,
                               count = m.Count()
                           };

            var dtBarReport = new DataTable();
            dtBarReport.Columns.Add(new DataColumn("CREATE_MONTH", typeof(string)));
            dtBarReport.Columns.Add(new DataColumn("INSTRUMENT_ID", typeof(decimal)));
            dtBarReport.Columns.Add(new DataColumn("REPORT_ITEM_NAME", typeof(decimal)));
            dtBarReport.Columns.Add(new DataColumn("RESULT", typeof(decimal)));





            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                //算正常范围内的值比
                var query = from t in dtResult.AsEnumerable()
                            group t by new { month = Utility.CDate(t.Field<string>("CREATE_MONTH")).ToString("yyyy-MM") } into m
                            select new
                            {
                                month = m.Key.month,
                                count = m.Count()
                            };

                foreach (var item in query)
                {
                    var row = dtBarReport.NewRow();
                    row["CREATE_MONTH"] = item.month;
                    row["RESULT"] = Math.Round(((decimal)item.count / queryAll.FirstOrDefault(i => i.month == item.month).count), 2);
                    dtBarReport.Rows.Add(row);
                }
            }
            if (dtUnmaxresult != null && dtUnmaxresult.Rows.Count > 0)
            {
                //算高正常范围内的值比
                var query1 = from t in dtUnmaxresult.AsEnumerable()
                             group t by new { month = Utility.CDate(t.Field<string>("CREATE_MONTH")).ToString("yyyy-MM") } into m
                             select new
                             {
                                 month = m.Key.month,
                                 count = m.Count()
                             };
                foreach (var item in query1)
                {
                    var havingRow = dtBarReport.AsEnumerable().FirstOrDefault(i => i["CREATE_MONTH"].ToString().Equals(item.month));
                    if (havingRow == null)
                    {
                        var row = dtBarReport.NewRow();
                        row["CREATE_MONTH"] = item.month;
                        row["REPORT_ITEM_NAME"] = Math.Round(((decimal)item.count / queryAll.FirstOrDefault(i => i.month == item.month).count), 2);
                        dtBarReport.Rows.Add(row);
                    }
                    else
                    {
                        havingRow["REPORT_ITEM_NAME"] = Math.Round(((decimal)item.count / queryAll.FirstOrDefault(i => i.month == item.month).count), 2);
                    }
                }
            }
            if (dtUnminresult != null && dtUnminresult.Rows.Count > 0)
            {
                //算低正常范围内的值比
                var query2 = from t in dtUnminresult.AsEnumerable()
                             group t by new { month = Utility.CDate(t.Field<string>("CREATE_MONTH")).ToString("yyyy-MM") } into m
                             select new
                             {
                                 month = m.Key.month,
                                 count = m.Count()
                             };

                foreach (var item in query2)
                {
                    var havingRow = dtBarReport.AsEnumerable().FirstOrDefault(i => i["CREATE_MONTH"].ToString().Equals(item.month));
                    if (havingRow == null)
                    {
                        var row = dtBarReport.NewRow();
                        row["CREATE_MONTH"] = item.month;
                        row["INSTRUMENT_ID"] = Math.Round(((decimal)item.count / queryAll.FirstOrDefault(i => i.month == item.month).count), 2);
                        dtBarReport.Rows.Add(row);
                    }
                    else
                    {
                        havingRow["INSTRUMENT_ID"] = Math.Round(((decimal)item.count / queryAll.FirstOrDefault(i => i.month == item.month).count), 2);
                    }
                }
            }
            foreach (DataRow row in dtBarReport.Rows)
            {
                if (string.IsNullOrWhiteSpace(row["RESULT"].ToString()))
                {
                    row["RESULT"] = 0;
                }
                if (string.IsNullOrWhiteSpace(row["REPORT_ITEM_NAME"].ToString()))
                {
                    row["REPORT_ITEM_NAME"] = 0;
                }
                if (string.IsNullOrWhiteSpace(row["INSTRUMENT_ID"].ToString()))
                {
                    row["INSTRUMENT_ID"] = 0;
                }
            }
            CreateChart(dtBarReport);
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
            var fwValue = this.txtFw.Text;
            var minValue = string.Empty;
            var maxValue = string.Empty;
            if (fwValue.Contains("~"))
            {
                var items = fwValue.Split('~');
                minValue = items[0];
                maxValue = items[1];
            }
            else if (fwValue.Contains(">"))
            {
                minValue = fwValue.Replace('>', ' ').Trim();
                maxValue = "100";
            }
            else if (fwValue.Contains("<"))
            {
                minValue = "0";
                maxValue = fwValue.Replace('<', ' ').Trim();
            }
            else
            {
                minValue = string.Empty;
                maxValue = string.Empty;
            }


            dtResult = null;
            dtUnminresult = new DataTable();
            dtUnmaxresult = new DataTable();
            DataTable dtPatient = isConstant ? _hemodialysisService.GetHemoIdInLastWeekAndThreeMonthsByDate(this.beginTime.DateTime, this.endTime.DateTime) : _hemodialysisService.GetHemoIdByDate(this.beginTime.DateTime, this.endTime.DateTime);
            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                DataTable dtCount = null;
                DataTable dtUnMinCount = null;
                DataTable dtUnMaxCount = null;

                dtPatient.AsEnumerable().ToList().ForEach(row =>
                {
                    if (this.lupPatients.EditValue == null || string.IsNullOrWhiteSpace(this.lupPatients.Text) || string.IsNullOrWhiteSpace(this.lupPatients.EditValue.ToString()))
                    {
                        var patientName = _patientService.GetPatientListByHemoIds(row["HEMODIALYSIS_ID"].ToString())[0].NAME.Trim();
                      
                        var dt = _hemodialysisService.GetLabValueLineTypeByparams(row["HEMODIALYSIS_ID"].ToString(), this.lupItems.EditValue.ToString(), this.beginTime.DateTime, this.endTime.DateTime);
                        if (dtCount == null)
                            dtCount = dt.Clone();
                        if (dtUnMinCount == null)
                            dtUnMinCount = dt.Clone();
                        if (dtUnMaxCount == null)
                            dtUnMaxCount = dt.Clone();
                        //因为上面数据中同一天会有两比数据，一个是透前一个是透后的。透前是生化，透后是肾功能。
                        foreach (DataRow dtRow in dt.Rows)
                        {
                            var item = dtCount.AsEnumerable().FirstOrDefault(i => i["CREATE_MONTH"].ToString().Equals(dtRow["CREATE_MONTH"].ToString()) && i["INSTRUMENT_ID"].Equals(patientName) && i["RESULT"].Equals(dtRow["RESULT"].ToString()));
                            if (item == null)
                            {
                                if (string.IsNullOrEmpty(maxValue))
                                {
                                    var newRow = dtCount.NewRow();
                                    newRow["CREATE_MONTH"] = dtRow["CREATE_MONTH"];
                                    newRow["INSTRUMENT_ID"] = patientName;
                                    newRow["REPORT_ITEM_NAME"] = dtRow["REPORT_ITEM_NAME"];
                                    newRow["RESULT"] = dtRow["RESULT"];
                                    dtCount.Rows.Add(newRow);
                                    dtUnMaxCount.Rows.Clear();
                                    dtUnMinCount.Rows.Clear();

                                }
                                else if ((Utility.CDecimal(dtRow["RESULT"].ToString()) < Utility.CDecimal(minValue)))
                                {
                                    var newRow = dtUnMinCount.NewRow();
                                    newRow["CREATE_MONTH"] = dtRow["CREATE_MONTH"];
                                    newRow["INSTRUMENT_ID"] = patientName;
                                    newRow["REPORT_ITEM_NAME"] = dtRow["REPORT_ITEM_NAME"];
                                    newRow["RESULT"] = dtRow["RESULT"];
                                    dtUnMinCount.Rows.Add(newRow);
                                }
                                else if ((Utility.CDecimal(dtRow["RESULT"].ToString()) > Utility.CDecimal(maxValue)))
                                {
                                    var newRow = dtUnMaxCount.NewRow();
                                    newRow["CREATE_MONTH"] = dtRow["CREATE_MONTH"];
                                    newRow["INSTRUMENT_ID"] = patientName;
                                    newRow["REPORT_ITEM_NAME"] = dtRow["REPORT_ITEM_NAME"];
                                    newRow["RESULT"] = dtRow["RESULT"];
                                    dtUnMaxCount.Rows.Add(newRow);
                                }
                                else if ((Utility.CDecimal(dtRow["RESULT"].ToString()) >= Utility.CDecimal(minValue)) && (Utility.CDecimal(dtRow["RESULT"].ToString()) <= Utility.CDecimal(maxValue)))
                                {
                                    var newRow = dtCount.NewRow();
                                    newRow["CREATE_MONTH"] = dtRow["CREATE_MONTH"];
                                    newRow["INSTRUMENT_ID"] = _patientService.GetPatientListByHemoIds(dtRow["INSTRUMENT_ID"].ToString())[0].NAME;
                                    newRow["REPORT_ITEM_NAME"] = dtRow["REPORT_ITEM_NAME"];
                                    newRow["RESULT"] = dtRow["RESULT"];
                                    dtCount.Rows.Add(newRow);
                                }
                            }
                        }
                    }
                    else
                    {
                        var dt = _hemodialysisService.GetLabValueLineTypeByparams(this.lupPatients.EditValue.ToString(), this.lupItems.EditValue.ToString(), this.beginTime.DateTime, this.endTime.DateTime);
                        if (dtCount == null)
                            dtCount = dt.Clone();
                        if (dtUnMinCount == null)
                            dtUnMinCount = dt.Clone();
                        if (dtUnMaxCount == null)
                            dtUnMaxCount = dt.Clone();
                        //因为上面数据中同一天会有两比数据，一个是透前一个是透后的。透前是生化，透后是肾功能。
                        foreach (DataRow dtRow in dt.Rows)
                        {
                            var item = dtCount.AsEnumerable().FirstOrDefault(i => i["CREATE_MONTH"].ToString().Equals(dtRow["CREATE_MONTH"].ToString()));
                            if (item == null)
                            {
                                if (string.IsNullOrEmpty(maxValue))
                                {
                                    var newRow = dtCount.NewRow();
                                    newRow["CREATE_MONTH"] = dtRow["CREATE_MONTH"];
                                    newRow["INSTRUMENT_ID"] = _patientService.GetPatientListByHemoIds(dtRow["INSTRUMENT_ID"].ToString())[0].NAME;
                                    newRow["REPORT_ITEM_NAME"] = dtRow["REPORT_ITEM_NAME"];
                                    newRow["RESULT"] = dtRow["RESULT"];
                                    dtCount.Rows.Add(newRow);
                                    dtUnMaxCount.Rows.Clear();
                                    dtUnMinCount.Rows.Clear();
                                }
                                else if ((Utility.CDecimal(dtRow["RESULT"].ToString()) < Utility.CDecimal(minValue)))
                                {
                                    var newRow = dtUnMinCount.NewRow();
                                    newRow["CREATE_MONTH"] = dtRow["CREATE_MONTH"];
                                    newRow["INSTRUMENT_ID"] = _patientService.GetPatientListByHemoIds(dtRow["INSTRUMENT_ID"].ToString())[0].NAME;
                                    newRow["REPORT_ITEM_NAME"] = dtRow["REPORT_ITEM_NAME"];
                                    newRow["RESULT"] = dtRow["RESULT"];
                                    dtUnMinCount.Rows.Add(newRow);
                                }
                                else if ((Utility.CDecimal(dtRow["RESULT"].ToString()) > Utility.CDecimal(maxValue)))
                                {
                                    var newRow = dtUnMaxCount.NewRow();
                                    newRow["CREATE_MONTH"] = dtRow["CREATE_MONTH"];
                                    newRow["INSTRUMENT_ID"] = _patientService.GetPatientListByHemoIds(dtRow["INSTRUMENT_ID"].ToString())[0].NAME;
                                    newRow["REPORT_ITEM_NAME"] = dtRow["REPORT_ITEM_NAME"];
                                    newRow["RESULT"] = dtRow["RESULT"];
                                    dtUnMaxCount.Rows.Add(newRow);
                                }
                                else if ((Utility.CDecimal(dtRow["RESULT"].ToString()) >= Utility.CDecimal(minValue)) && (Utility.CDecimal(dtRow["RESULT"].ToString()) <= Utility.CDecimal(maxValue)))
                                {
                                    var newRow = dtCount.NewRow();
                                    newRow["CREATE_MONTH"] = dtRow["CREATE_MONTH"];
                                    newRow["INSTRUMENT_ID"] = _patientService.GetPatientListByHemoIds(dtRow["INSTRUMENT_ID"].ToString())[0].NAME;
                                    newRow["REPORT_ITEM_NAME"] = dtRow["REPORT_ITEM_NAME"];
                                    newRow["RESULT"] = dtRow["RESULT"];
                                    dtCount.Rows.Add(newRow);
                                }
                            }
                        }
                    }
                });

                if (dtCount != null && dtCount.Rows.Count > 0)
                {
                    dtResult = dtCount.Clone();
                    dtCount.AsEnumerable().OrderBy(row => row["CREATE_MONTH"].ToString()).CopyToDataTable(dtResult, LoadOption.OverwriteChanges);
                }
                if (dtUnMinCount != null && dtUnMinCount.Rows.Count > 0)
                {
                    dtUnminresult = dtUnMinCount.Clone();
                    dtUnMinCount.AsEnumerable().OrderBy(row => row["CREATE_MONTH"].ToString()).CopyToDataTable(dtUnminresult, LoadOption.OverwriteChanges);
                }
                if (dtUnMaxCount != null && dtUnMaxCount.Rows.Count > 0)
                {
                    dtUnmaxresult = dtUnMaxCount.Clone();
                    dtUnMaxCount.AsEnumerable().OrderBy(row => row["CREATE_MONTH"].ToString()).CopyToDataTable(dtUnmaxresult, LoadOption.OverwriteChanges);
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

        private void CreateChart(DataTable dt1)
        {
            chartControl1.Series.Clear();
            // 柱状图里的第一个柱
            Series Series1 = new Series("正常范围达标率", ViewType.Bar);
            Series1.DataSource = dt1;
            Series1.ArgumentScaleType = ScaleType.Qualitative;

            // 以哪个字段进行显示
            Series1.ArgumentDataMember = "CREATE_MONTH";
            Series1.ValueScaleType = ScaleType.Numerical;

            // 柱状图里的柱的取值字段
            Series1.ValueDataMembers.AddRange(new string[] { "RESULT" });

            // 柱状图里的第二柱
            Series Series2 = new Series("高于正常范围比率", ViewType.Bar);
            Series2.DataSource = dt1;
            Series2.ArgumentScaleType = ScaleType.Qualitative;
            Series2.ArgumentDataMember = "CREATE_MONTH";
            Series2.ValueScaleType = ScaleType.Numerical;
            Series2.ValueDataMembers.AddRange(new string[] { "REPORT_ITEM_NAME" });

            // 柱状图里的第二柱
            Series Series3 = new Series("低于正常范围比率", ViewType.Bar);
            Series3.DataSource = dt1;
            Series3.ArgumentScaleType = ScaleType.Qualitative;
            Series3.ArgumentDataMember = "CREATE_MONTH";
            Series3.ValueScaleType = ScaleType.Numerical;
            Series3.ValueDataMembers.AddRange(new string[] { "INSTRUMENT_ID" });
            chartControl1.Series.Add(Series1);
            chartControl1.Series.Add(Series2);
            chartControl1.Series.Add(Series3);

        }



        /// <summary>
        /// 创建图形对你
        /// </summary>
        /// <param name="dt"></param>
        private void CreateChart1(DataTable dt)
        {
            if (dt == null) return;

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
            for (var i = 2; i < dt.Columns.Count; i++)
            {
                var row = lineDtResult.NewRow();
                row["类型"] = dt.Rows[0][i].ToString();
                lineDtResult.Rows.Add(row);
                break;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 1; j < lineDtResult.Columns.Count; j++)
                {
                    if (dt.Rows[i][0].ToString().Equals(lineDtResult.Columns[j].ColumnName))
                    {
                        lineDtResult.Rows[0][j] = dt.Rows[i][3].ToString();
                    }
                }
            }

            //根据录入的范围值进行画线 = "1.13~1.78"  >   <  
            var fwValue = this.txtFw.Text;
            var minValue = string.Empty;
            var maxValue = string.Empty;
            if (fwValue.Contains("~"))
            {
                var items = fwValue.Split('~');
                minValue = items[0];
                maxValue = items[1];
            }
            else if (fwValue.Contains(">"))
            {
                minValue = fwValue.Replace('>', ' ').Trim();
                maxValue = "100";
            }
            else if (fwValue.Contains("<"))
            {
                minValue = "0";
                maxValue = fwValue.Replace('<', ' ').Trim();
            }
            else
            {
                minValue = string.Empty;
                maxValue = string.Empty;
            }

            if (!string.IsNullOrWhiteSpace(minValue) && !string.IsNullOrWhiteSpace(maxValue))
            {
                #region 方法

                var linRow = lineDtResult.NewRow();
                linRow["类型"] = "MIN";
                lineDtResult.Rows.Add(linRow);
                var linRow1 = lineDtResult.NewRow();
                linRow1["类型"] = "MAX";
                lineDtResult.Rows.Add(linRow1);


                var minRow = lineDtResult.AsEnumerable().FirstOrDefault(k => k["类型"].ToString().Equals("MIN"));
                var maxRow = lineDtResult.AsEnumerable().FirstOrDefault(k => k["类型"].ToString().Equals("MAX"));
                #endregion
                for (int i = 1; i < lineDtResult.Columns.Count; i++)
                {
                    minRow[i] = minValue;
                    maxRow[i] = maxValue;
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
            if (caption.ToUpper().Equals("MIN") || caption.ToUpper().Equals("MAX"))
                series.View.Color = Color.Orchid;
            else
                series.View.Color = Color.Red;
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



        private void lupItems_EditValueChanged(object sender, EventArgs e)
        {
            if (lupItems.EditValue.ToString().Contains("钙"))
            {
                this.txtFw.Text = "2.08~2.6";
            }
            else if (lupItems.EditValue.ToString().Contains("磷"))
            {
                this.txtFw.Text = "1.13~1.45";
            }
            else if (lupItems.EditValue.ToString().Contains("钾"))
            {
                this.txtFw.Text = "3.3~5.5";
            }
            else if (lupItems.EditValue.ToString().Contains("全段甲状旁腺激素"))
            {
                this.txtFw.Text = "150~300";
            }
            else if (lupItems.EditValue.ToString().Contains("白蛋白"))
            {
                this.txtFw.Text = "35~40";
            }
            else if (lupItems.EditValue.ToString().Contains("血红蛋白"))
            {
                this.txtFw.Text = "110~130";
            }
            else if (lupItems.EditValue.ToString().Contains("铁蛋白"))
            {
                this.txtFw.Text = "200~500";
            }
            else if (lupItems.EditValue.ToString().Contains("总胆固醇"))
            {
                this.txtFw.Text = "2.86~5";
            }
            else
            {
                this.txtFw.Text = string.Empty;
            }
        }

        #endregion


    }
}
