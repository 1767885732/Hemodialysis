/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:参数
 * 创建标识:吕志强-2013年7月9日
 * 
 * 修改时间:2013年10月17日
 * 修改人:贺建操
 * 修改描述:修改方法
 * 
 * 修改时间:2014年1月25日
 * 修改人:顾伟伟
 * 修改描述:新增方法
 * 
 * 修改时间:2014年5月5日
 * 修改人:顾伟伟
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2017年9月29日
 * 修改人:刘配齐
 * 修改描述:修改查询报错
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using Hemo.IService.Config;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.Client.UI.Machine;
using System.Collections;
using Hemo.IService;
using System.Windows.Forms;
using Hemo.Client.UI.ReportChart;
using System.ComponentModel;
namespace Hemo.Client.UI.Hemodialysis
{
    public partial class CtlHemoParametersChart : ViewBase
    {
        #region 变量

        private const char SEPARATOR = '/';

        private Color[] _colorArray = new Color[] { Color.Red, Color.Yellow, Color.Blue, Color.LightBlue, Color.Green, Color.LightGreen, Color.Orange, Color.Violet, Color.Purple, Color.Lavender };

        private HemoModel.MED_HEMODIALYSIS_PARAMS_TYPEDataTable _paramsTypeDataTable;

        private HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable dtParam;

        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        private IPatient _patientService = ServiceManager.Instance.PatientService;

        private bool isConstant = true;

        #endregion

        #region 构造函数

        public CtlHemoParametersChart()
        {
            this.InitializeComponent();
        }

        #endregion

        #region 事件

        private void HemoParametersChart_Load(object sender, EventArgs e)
        {
            InitializeControls();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.errorProvider.ClearErrors();
            if (string.IsNullOrEmpty(this.txtHemoID.Text))
            {
                this.txtHemoID.Focus();

                this.errorProvider.SetError(this.txtHemoID, "请选择患者！");

                return;
            }

            isConstant = this.chkConstant.Checked;
            RunBackgroundWorker();
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExpExcel_Click(object sender, EventArgs e)
        {
            if (this.gvParams.RowCount > 0)
            {
                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.Title = "导出Excel";
                fileDialog.Filter = "Excel文件(*.xls)|*.xls";
                fileDialog.FileName = this.deBeginCreateTime.DateTime.ToString("yyyyMMdd") + "-" + this.deEndCreateTime.DateTime.ToString("yyyyMMdd") + this.txtHemoID.Text + "治疗数据";
                fileDialog.RestoreDirectory = true;
                DialogResult dialogResult = fileDialog.ShowDialog(this);
                if (dialogResult == DialogResult.OK)
                {
                    DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                    options.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
                    this.gvParams.ExportToXls(fileDialog.FileName, options);
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
            reportNote.ReportType = ReportTypeEnum.治疗数据;
            reportNote.ShowDialog();
        }

        private void gvParams_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Column.ColumnHandle >= 0)
                e.Appearance.ForeColor = this.chartControl1.Series[this.gvParams.GetDataRow(e.RowHandle).ItemArray[0].ToString()].View.Color;
        }

        private void work_DoWork(object sender, DoWorkEventArgs e)
        {
            //以当前时间为基准，检查患者上周是否透析过、三个月内是否连续透析过
            DataTable dtRecord = isConstant ? _hemodialysisService.GetHemoIdInLastWeekAndThreeMonthsByDate(this.deBeginCreateTime.DateTime, this.deEndCreateTime.DateTime) : _hemodialysisService.GetHemoIdByDate(this.deBeginCreateTime.DateTime, this.deEndCreateTime.DateTime);
            if (dtRecord != null && dtRecord.Rows.Count > 0)
            {
                var row = dtRecord.AsEnumerable().FirstOrDefault(r => r["HEMODIALYSIS_ID"].ToString().Equals(this.txtHemoID.EditValue.ToString()));
                if (row == null)
                {
                    e.Cancel = true;
                    return;
                }
                dtParam = _hemodialysisService.GetHemoParameters(this.txtHemoID.EditValue.ToString(), this.deBeginCreateTime.DateTime, this.deEndCreateTime.DateTime);
            }
            else
            {
                e.Cancel = true;
                return;
            }
        }

        private void work_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                if (isConstant)
                {
                    XtraMessageBox.Show("该患者不符合维持性透析条件！", "治疗数据");
                }
                else
                {
                    XtraMessageBox.Show("该患者在指定时间范围内未做过透析！", "治疗数据");
                }
                ResetBindData();
                this.busyIndicator.HideLoadingScreen();
                return;
            }

            BindData();
            this.busyIndicator.HideLoadingScreen();
        }

        #endregion

        #region 方法

        private void InitializeControls()
        {
            _paramsTypeDataTable = _hemodialysisService.GetHemoParametersType();

            var result = from r in _paramsTypeDataTable.AsEnumerable()
                         group r by r.GROUPID into g
                         select new
                         {
                             IdArray = (from r in _paramsTypeDataTable where r.GROUPID == g.Key select r.ID).ToArray()
                         };

            DataTable dtBind = new DataTable();

            dtBind.Columns.Add(new DataColumn("id", typeof(string)));
            dtBind.Columns.Add(new DataColumn("text", typeof(string)));

            foreach (var item in result)
            {
                DataRow rowNew = dtBind.NewRow();

                rowNew["id"] = string.Join(SEPARATOR.ToString(), item.IdArray);
                rowNew["text"] = string.Join(SEPARATOR.ToString(), (from id in item.IdArray select _paramsTypeDataTable.FindByID(id).DISPLAYNAME).ToArray());

                dtBind.Rows.Add(rowNew);
            }

            this.deBeginCreateTime.DateTime = DateTime.Now.AddDays(-DateTime.Now.Day + 1).AddMonths(-DateTime.Now.Month + 1);
            this.deEndCreateTime.DateTime = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.Day);

            this.lueType.Properties.DataSource = dtBind;
            var _patientDataTable = _patientService.GetPatientList();
            this.txtHemoID.Properties.DataSource = _patientDataTable;
        }

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

        private void BindData()
        {
            //创建报表Series
            this.chartControl1.Series.Clear();
            string[] param = Utility.Split(this.lueType.EditValue.ToString().Replace(" ", "").Replace(',', SEPARATOR), SEPARATOR);
            if (param.Length == 0)
            {
                XtraMessageBox.Show("请选择治疗参数！");
                return;
            };


            param.AsEnumerable().ToList().ForEach(p =>
            {
                HemoModel.MED_HEMODIALYSIS_PARAMS_TYPERow row = _paramsTypeDataTable.First(r => r.ID.Equals(p));
                Series series = new Series(row.DISPLAYNAME, ViewType.Line);
                series.View.Color = _colorArray[_paramsTypeDataTable.Rows.IndexOf(row)];
                series.ArgumentScaleType = ScaleType.Qualitative;

                //创建报表Points
                dtParam.AsEnumerable().ToList().ForEach(r =>
                {
                    SeriesPoint point = new SeriesPoint(r.CREATE_DATE, r[row.FILEDNAME]);
                    series.Points.Add(point);
                });
                this.chartControl1.Series.Add(series);
            });
            this.chartControl1.DataSource = this.gcParams.DataSource = dtParam;

            if (dtParam != null && dtParam.Rows.Count > 5)
            {
                ((XYDiagram)this.chartControl1.Diagram).AxisX.VisualRange.MinValue = dtParam[0].CREATE_DATE;
                ((XYDiagram)this.chartControl1.Diagram).AxisX.VisualRange.MaxValue = dtParam[4].CREATE_DATE;
            }

            #region 后台代码实现设计界面报表设置

            //((XYDiagram)this.chartControl1.Diagram).EnableAxisXScrolling = true;
            //((XYDiagram)this.chartControl1.Diagram).AxisX.Range.Auto = false; //要开启滚动条必须将其设置为false
            //((XYDiagram)this.chartControl1.Diagram).AxisX.Range.ScrollingRange.Auto = false;
            //((XYDiagram)this.chartControl1.Diagram).AxisX.MinorCount = 9; //显示X轴间隔数量
            //((XYDiagram)this.chartControl1.Diagram).AxisX.Tickmarks.MinorVisible = true;//是否显示X轴间隔
            //((XYDiagram)this.chartControl1.Diagram).AxisX.Range.ScrollingRange.MaxValueSerializable = (0 + 1).ToString();//整个X轴最多显示多多少个值
            //((XYDiagram)this.chartControl1.Diagram).AxisX.Range.ScrollingRange.MinValueSerializable = "0";
            //((XYDiagram)this.chartControl1.Diagram).AxisX.DateTimeMeasureUnit = DateTimeMeasurementUnit.Second;
            //((XYDiagram)this.chartControl1.Diagram).AxisX.DateTimeOptions.Format = DateTimeFormat.Custom;
            //((XYDiagram)this.chartControl1.Diagram).AxisX.DateTimeOptions.FormatString = "yyyy:MM:HH";
            //((XYDiagram)this.chartControl1.Diagram).AxisX.Range.ScrollingRange.SideMarginsEnabled = true;//是否从X轴原点开始显示
            //((XYDiagram)this.chartControl1.Diagram).AxisX.Range.SideMarginsEnabled = false;
            //((XYDiagram)this.chartControl1.Diagram).AxisX.VisibleInPanesSerializable = "-1";

            #endregion
        }

        private void ResetBindData()
        {
            this.chartControl1.Series.Clear();
            this.chartControl1.DataSource = this.gcParams.DataSource = null;
        }

        #endregion
    }
}
