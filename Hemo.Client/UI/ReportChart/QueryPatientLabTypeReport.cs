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
    public partial class QueryPatientLabTypeReport : ViewBase
    {
        #region 类变量

        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IPatient _patientService = ServiceManager.Instance.PatientService;

        private DataTable dtResult = null;
        private DataTable _dtPatients = new DataTable();
        private bool isConstant = true;

        #endregion

        #region 构造函数

        public QueryPatientLabTypeReport()
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
        private void QueryPatientLabTypeReport_Load(object sender, EventArgs e)
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
            if (xtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                if (this.gvCount.RowCount > 0)
                {
                    SaveFileDialog fileDialog = new SaveFileDialog();
                    fileDialog.Title = "导出Excel";
                    fileDialog.Filter = "Excel文件(*.xls)|*.xls";
                    fileDialog.FileName = this.beginTime.DateTime.ToString("yyyyMMdd") + "-" + this.endTime.DateTime.ToString("yyyyMMdd") + this.combType.EditValue.ToString();
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
                fileDialog.FileName = this.beginTime.DateTime.ToString("yyyyMMdd") + "-" + this.endTime.DateTime.ToString("yyyyMMdd") + this.combType.EditValue.ToString() + "未达标人员";
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

            if (this.combType.EditValue.ToString() == "溶质清除患者比例")
            {
                reportNote.ReportType = ReportTypeEnum.溶质清除患者比例;
            }
            else if (this.combType.EditValue == "血红蛋白达标率")
            {
                reportNote.ReportType = ReportTypeEnum.血红蛋白达标率;
            }
            else if (this.combType.EditValue == "钙磷代谢例数")
            {
                reportNote.ReportType = ReportTypeEnum.钙磷代谢例数;
            }
            else if (this.combType.EditValue == "甲状旁腺功能亢进")
            {
                reportNote.ReportType = ReportTypeEnum.甲状旁腺功能亢进;
            }
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
            this.gridControl1.DataSource = _dtPatients;
            if (_dtPatients.Rows.Count > 0)
            {
                this.xtraTabPage2.Text = string.Format("未达标人员：{0} 人", _dtPatients.Rows.Count);
            }
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
            _dtPatients = new DataTable();
            _dtPatients.Columns.Add("患者姓名", typeof(string));
            _dtPatients.Columns.Add("检验日期", typeof(string));
            _dtPatients.Columns.Add("结果", typeof(string));
            _dtPatients.Columns.Add("类型", typeof(string));
            dtResult = null;
            DataTable dtPatient = isConstant ? _hemodialysisService.GetHemoIdInLastWeekAndThreeMonthsByDate(this.beginTime.DateTime, this.endTime.DateTime) : _hemodialysisService.GetHemoIdByDate(this.beginTime.DateTime, this.endTime.DateTime);
            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                DataTable dtCount = null;
                //全部溶质清除患者>65%的人数
                var rzqchz = 0;
                var Rzqcdt = new DataTable();
                Rzqcdt.Columns.Add("TYPENAME", typeof(string));
                Rzqcdt.Columns.Add("RZQCHZS", typeof(string));
                Rzqcdt.Columns.Add("ALLHZS", typeof(string));
                Rzqcdt.Columns.Add("RATE", typeof(string));
                dtPatient.AsEnumerable().ToList().ForEach(row =>
                {
                    #region 溶质清除患者比例
                    if (this.combType.EditValue.ToString() == "溶质清除患者比例")
                    {
                        DataTable dtSubCount = _hemodialysisService.GetInfectiousCountByParams(row["HEMODIALYSIS_ID"].ToString(), this.beginTime.DateTime, this.endTime.DateTime, "生化全套", string.Empty);
                        DataTable dtSubCountExt = _hemodialysisService.GetInfectiousCountByParams(row["HEMODIALYSIS_ID"].ToString(), this.beginTime.DateTime, this.endTime.DateTime, "肾功电解质", string.Empty);
                        if (dtSubCount.Rows.Count <= 0)
                        {
                            var prow = _dtPatients.NewRow();
                            prow["患者姓名"] = _patientService.GetPatientListByHemoIds(row["HEMODIALYSIS_ID"].ToString())[0].NAME;
                            prow["检验日期"] = "无";
                            prow["结果"] = "【无检验记录】";
                            prow["类型"] = "溶质清除患者比例算法： (透前尿素 - 透后尿素）* 透前尿素 100%  然后算值是否大于65%";
                            _dtPatients.Rows.Add(prow);
                            return;
                        }
                        if (dtSubCountExt.Rows.Count <= 0)
                        {
                            var prow = _dtPatients.NewRow();
                            prow["患者姓名"] = _patientService.GetPatientListByHemoIds(row["HEMODIALYSIS_ID"].ToString())[0].NAME;
                            prow["检验日期"] = "无";
                            prow["结果"] = "【无检验记录】";
                            prow["类型"] = "溶质清除患者比例算法： (透前尿素 - 透后尿素）* 透前尿素 100%  然后算值是否大于65%";
                            _dtPatients.Rows.Add(prow);
                            return;
                        }
                        //算法：透前尿素是生化全套里的尿素，透后尿素是肾功电解质里的尿素  (透前尿素 - 透后尿素）* 透前尿素 100%  然后算值是否大于65%   【最后一次检验数据】
                        //透前尿素
                        var tqns = Utility.CDecimal(dtSubCount.Rows[dtSubCount.Rows.Count - 1]["RESULT"].ToString());
                        //透后尿素
                        var thns = Utility.CDecimal(dtSubCountExt.Rows[dtSubCountExt.Rows.Count - 1]["RESULT"].ToString());
                        //进行计算 
                        var resultItem = (tqns - thns) * tqns;
                        if (resultItem > 0.65m)
                        {
                            rzqchz++;
                        }
                        else
                        {
                            var prow = _dtPatients.NewRow();
                            prow["患者姓名"] = _patientService.GetPatientListByHemoIds(row["HEMODIALYSIS_ID"].ToString())[0].NAME;
                            prow["检验日期"] = dtSubCount.Rows[dtSubCount.Rows.Count - 1]["RESULT_DATE_TIME"].ToString();
                            prow["结果"] = string.Format("透前尿素:{0} 透后尿素{1}", tqns.ToString(), thns.ToString());
                            prow["类型"] = "溶质清除患者比例算法： (透前尿素 - 透后尿素）* 透前尿素 100%  然后算值是否大于65%";
                            _dtPatients.Rows.Add(prow);
                        }
                    }
                    #endregion
                    #region 血红蛋白达标率
                    else if (this.combType.EditValue.ToString() == "血红蛋白达标率")
                    {
                        DataTable dtSubCount = _hemodialysisService.GetInfectiousCountByParams(row["HEMODIALYSIS_ID"].ToString(), this.beginTime.DateTime, this.endTime.DateTime, "血红蛋白", "HGB");
                        if (dtSubCount.Rows.Count <= 0)
                        {
                            var prow = _dtPatients.NewRow();
                            prow["患者姓名"] = _patientService.GetPatientListByHemoIds(row["HEMODIALYSIS_ID"].ToString())[0].NAME;
                            prow["检验日期"] = "无";
                            prow["结果"] = "【无检验记录】";
                            prow["类型"] = "血红蛋白达标率算法：标准为：值在110到130之间为达标，否则为不达标";
                            _dtPatients.Rows.Add(prow);
                            return;
                        }
                        //得到数据后去看他是否达标。标准为：值在110到130之间就是达标，要不然就不算达标 【最后一次检验数据】
                        var hgbHi = Utility.CDecimal(dtSubCount.Rows[dtSubCount.Rows.Count - 1]["RESULT"].ToString());
                        if (hgbHi > 110 && hgbHi < 130)
                        {
                            rzqchz++;
                        }
                        else
                        {
                            var prow = _dtPatients.NewRow();
                            prow["患者姓名"] = _patientService.GetPatientListByHemoIds(row["HEMODIALYSIS_ID"].ToString())[0].NAME;
                            prow["检验日期"] = dtSubCount.Rows[dtSubCount.Rows.Count - 1]["RESULT_DATE_TIME"].ToString();
                            prow["结果"] = string.Format("血红蛋白:{0}", hgbHi.ToString());
                            prow["类型"] = "血红蛋白达标率算法：标准为：值在110到130之间为达标，否则为不达标";
                            _dtPatients.Rows.Add(prow);
                        }
                    }
                    #endregion
                    #region 甲状旁腺功能亢进

                    else if (this.combType.EditValue.ToString() == "甲状旁腺功能亢进")
                    {
                        DataTable dtSubCount = _hemodialysisService.GetInfectiousCountByParams(row["HEMODIALYSIS_ID"].ToString(), this.beginTime.DateTime, this.endTime.DateTime, "全段甲状旁腺激素", "iPTH");
                        if (dtSubCount.Rows.Count <= 0)
                        {
                            var prow = _dtPatients.NewRow();
                            prow["患者姓名"] = _patientService.GetPatientListByHemoIds(row["HEMODIALYSIS_ID"].ToString())[0].NAME;
                            prow["检验日期"] = "无";
                            prow["结果"] = "【无检验记录】";
                            prow["类型"] = "甲状旁腺功能亢进算法：标准为：值在100到300之间为达标，否则为不达标";
                            _dtPatients.Rows.Add(prow);
                            return;
                        }
                        //得到数据后去看他是否达标。标准为：值在100到300之间就是达标，要不然就不算达标 【最后一次检验数据】100-300ng/dl
                        var hgbHi = Utility.CDecimal(dtSubCount.Rows[dtSubCount.Rows.Count - 1]["RESULT"].ToString());
                        if (hgbHi > 100 && hgbHi < 300)
                        {
                            rzqchz++;
                        }
                        else
                        {
                            var prow = _dtPatients.NewRow();
                            prow["患者姓名"] = _patientService.GetPatientListByHemoIds(row["HEMODIALYSIS_ID"].ToString())[0].NAME;
                            prow["检验日期"] = dtSubCount.Rows[dtSubCount.Rows.Count - 1]["RESULT_DATE_TIME"].ToString();
                            prow["结果"] = string.Format("全段甲状旁腺激素:{0}", hgbHi);
                            prow["类型"] = "甲状旁腺功能亢进算法：标准为：值在100到300之间为达标，否则为不达标";
                            _dtPatients.Rows.Add(prow);
                        }
                    }

                    #endregion

                    #region 钙磷代谢例数

                    else if (this.combType.EditValue.ToString() == "钙磷代谢例数")
                    {
                        DataTable dtSubCount = _hemodialysisService.GetInfectiousCountByParams(row["HEMODIALYSIS_ID"].ToString(), this.beginTime.DateTime, this.endTime.DateTime, "钙", "Ca");
                        DataTable dtSubCountExt = _hemodialysisService.GetInfectiousCountByParams(row["HEMODIALYSIS_ID"].ToString(), this.beginTime.DateTime, this.endTime.DateTime, "磷", "P");
                        if (dtSubCount.Rows.Count <= 0)
                        {
                            var prow = _dtPatients.NewRow();
                            prow["患者姓名"] = _patientService.GetPatientListByHemoIds(row["HEMODIALYSIS_ID"].ToString())[0].NAME;
                            prow["检验日期"] = "无";
                            prow["结果"] = "【无检验记录】";
                            prow["类型"] = "钙磷代谢例数算法：钙磷乘积 < 55";
                            _dtPatients.Rows.Add(prow);
                            return;
                        }
                        if (dtSubCountExt.Rows.Count <= 0)
                        {
                            var prow = _dtPatients.NewRow();
                            prow["患者姓名"] = _patientService.GetPatientListByHemoIds(row["HEMODIALYSIS_ID"].ToString())[0].NAME;
                            prow["检验日期"] = "无";
                            prow["结果"] = "【无检验记录】";
                            prow["类型"] = "钙磷代谢例数算法：钙磷乘积 < 55";

                            _dtPatients.Rows.Add(prow);
                            return;
                        }
                        //算法：钙磷代谢例数   钙磷乘积 < 55   【最后一次检验数据】
                        //钙
                        var tqns = Utility.CDecimal(dtSubCount.Rows[dtSubCount.Rows.Count - 1]["RESULT"].ToString());
                        //磷
                        var thns = Utility.CDecimal(dtSubCountExt.Rows[dtSubCountExt.Rows.Count - 1]["RESULT"].ToString());
                        //进行计算 
                        var resultItem = tqns * thns;
                        if (resultItem < 55)
                        {
                            rzqchz++;
                        }
                        else
                        {
                            var prow = _dtPatients.NewRow();
                            prow["患者姓名"] = _patientService.GetPatientListByHemoIds(row["HEMODIALYSIS_ID"].ToString())[0].NAME;
                            prow["检验日期"] = dtSubCount.Rows[dtSubCount.Rows.Count - 1]["RESULT_DATE_TIME"].ToString();
                            prow["结果"] = string.Format("全段甲状旁腺激素:钙 :{0}  磷:{1}", tqns.ToString(), thns.ToString());
                            prow["类型"] = "钙磷代谢例数算法：钙磷乘积 < 55";

                            _dtPatients.Rows.Add(prow);
                        }
                    }

                    #endregion
                });


                #region 溶质清除患者比例
                if (this.combType.EditValue.ToString() == "溶质清除患者比例")
                {
                    var row = Rzqcdt.NewRow();
                    row["TYPENAME"] = "溶质清除患者比例";
                    row["RZQCHZS"] = rzqchz;
                    row["ALLHZS"] = dtPatient.Rows.Count;
                    row["RATE"] = Math.Round((Utility.CDecimal(rzqchz.ToString()) / Utility.CDecimal(dtPatient.Rows.Count.ToString())), 3) * 100 + "%";
                    Rzqcdt.Rows.Add(row);
                    dtResult = Rzqcdt.Copy();
                }
                #endregion
                #region 血红蛋白达标率

                else if (this.combType.EditValue.ToString() == "血红蛋白达标率")
                {
                    var row = Rzqcdt.NewRow();
                    row["TYPENAME"] = "血红蛋白达标率";
                    row["RZQCHZS"] = rzqchz;
                    row["ALLHZS"] = dtPatient.Rows.Count;
                    row["RATE"] = Math.Round((Utility.CDecimal(rzqchz.ToString()) / Utility.CDecimal(dtPatient.Rows.Count.ToString())), 3) * 100 + "%";
                    Rzqcdt.Rows.Add(row);
                    dtResult = Rzqcdt.Copy();
                }

                #endregion
                #region 甲状旁腺功能亢进

                else if (this.combType.EditValue.ToString() == "甲状旁腺功能亢进")
                {
                    var row = Rzqcdt.NewRow();
                    row["TYPENAME"] = "甲状旁腺功能亢进";
                    row["RZQCHZS"] = rzqchz;
                    row["ALLHZS"] = dtPatient.Rows.Count;
                    row["RATE"] = Math.Round((Utility.CDecimal(rzqchz.ToString()) / Utility.CDecimal(dtPatient.Rows.Count.ToString())), 3) * 100 + "%";
                    Rzqcdt.Rows.Add(row);
                    dtResult = Rzqcdt.Copy();
                }

                #endregion
                #region 钙磷代谢例数

                else if (this.combType.EditValue.ToString() == "钙磷代谢例数")
                {
                    var row = Rzqcdt.NewRow();
                    row["TYPENAME"] = "钙磷代谢例数";
                    row["RZQCHZS"] = rzqchz;
                    row["ALLHZS"] = dtPatient.Rows.Count;
                    row["RATE"] = Math.Round((Utility.CDecimal(rzqchz.ToString()) / Utility.CDecimal(dtPatient.Rows.Count.ToString())), 3) * 100 + "%";
                    Rzqcdt.Rows.Add(row);
                    dtResult = Rzqcdt.Copy();
                }

                #endregion

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
                var findRow = dtResult.AsEnumerable().FirstOrDefault(row => row["TYPENAME"].ToString().Equals(this.combType.EditValue.ToString()));
                if (findRow != null)
                {
                    DataTable dtSource = new DataTable();
                    dtSource.Columns.Add("ACCESS_TYPE", typeof(string));
                    dtSource.Columns.Add("SUB_COUNT", typeof(int));
                    foreach (DataColumn column in dtResult.Columns)
                    {
                        if (column.ColumnName.Equals("TYPENAME") || column.ColumnName.Equals("RATE"))
                        {
                            continue;
                        }
                        var row = dtSource.NewRow();
                        row["ACCESS_TYPE"] = column.ColumnName.Equals("RZQCHZS") ? "达标" : "未达标";
                        row["SUB_COUNT"] = column.ColumnName.Equals("RZQCHZS") ? Utility.CInt(findRow[column.ColumnName].ToString()) : Utility.CInt(findRow["ALLHZS"].ToString()) - Utility.CInt(findRow["RZQCHZS"].ToString());
                        dtSource.Rows.Add(row);
                    }

                    Series serAccess = new Series(string.Empty, ViewType.Pie);
                    serAccess.DataSource = dtSource;
                    ChartTitle ct = new ChartTitle();

                    ct.Text = this.combType.EditValue.ToString();
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


    }
}
