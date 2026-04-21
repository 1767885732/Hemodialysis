/*----------------------------------------------------------------
 * Copyright (C) 2005 麦迪斯顿(苏州)医疗科技发展有限公司
 * 文件功能描述:死亡率统计查询类
 * 创建标识:吕志强-2017年4月23日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using Hemo.Client.UI.Machine;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.Model;
using Hemo.IService;

namespace Hemo.Client.UI.ReportChart
{
    public partial class QueryDeathRate : ViewBase
    {
        #region 类变量

        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        private bool isConstant = false;

        private DataTable dtResult = null;
        private DataTable dtCount = null;
        private PatientModel.MED_PATIENTSDataTable patientDt = null;
        private IPatient _patientService = ServiceManager.Instance.PatientService;

        private DataTable dtBarReport = null;
        #endregion

        #region 构造函数

        public QueryDeathRate()
        {
            InitializeComponent();
            BindType();
            this.lupType.EditValue = "1";

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
        private void QuerySexScaleReport_Load(object sender, EventArgs e)
        {
            dtBarReport = new DataTable();
            dtBarReport.Columns.Add(new DataColumn("ALLCOUNT", typeof(decimal)));
            dtBarReport.Columns.Add(new DataColumn("DIECOUNT", typeof(decimal)));
            dtBarReport.Columns.Add(new DataColumn("RESULT", typeof(string)));
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
                string name = "透析死亡例数";
                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.Title = "导出Excel";
                fileDialog.Filter = "Excel文件(*.xls)|*.xls";
                fileDialog.FileName = this.beginTime.DateTime.ToString("yyyyMMdd") + "-" + this.endTime.DateTime.ToString("yyyyMMdd") + name;
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
            reportNote.ReportType = ReportTypeEnum.死亡登记;
            reportNote.ShowDialog();
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_print_Click(object sender, EventArgs e)
        {
            // this.chartControl1.Print();
        }

        private void work_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadChartData();
        }

        private void work_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //   LoadPieData();
            this.gcCount.DataSource = dtBarReport;
            this.gridControlMan.DataSource = patientDt;
            LoadPieData();
            this.busyIndicator.HideLoadingScreen();
        }

        #endregion

        #region 方法

        private void beginTime_EditValueChanged(object sender, EventArgs e)
        {
            this.endTime.DateTime = this.beginTime.DateTime.AddYears(1);
        }
        /// <summary>
        /// 绑定统计类型
        /// </summary>
        private void BindType()
        {
            DataTable dtType = new DataTable();
            dtType.Columns.Add("NAME", typeof(string));
            dtType.Columns.Add("VALUE", typeof(string));

            var row = dtType.NewRow();
            row["NAME"] = "按照人数";
            row["VALUE"] = "1";
            dtType.Rows.Add(row);

            row = dtType.NewRow();
            row["NAME"] = "按照人次";
            row["VALUE"] = "2";
            dtType.Rows.Add(row);

            this.lupType.Properties.DataSource = dtType;
        }

        /// <summary>
        /// 启动后台线程
        /// </summary>
        private void RunBackgroundWorker()
        {
            //  this.busyIndicator.ShowLoadingScreenFor(this.chartControl1);

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
            string HemoIds = string.Empty;

            dtResult = null;
            DataTable dtPatient = isConstant ? _hemodialysisService.GetHemoIdInLastWeekAndThreeMonthsByDate(this.beginTime.DateTime, this.endTime.DateTime) : _hemodialysisService.GetHemoIdByDate(this.beginTime.DateTime, this.endTime.DateTime);
            //  DataTable dtPatient = _hemodialysisService.GetHemoIdInLastWeekAndThreeMonthsByDate(this.beginTime.DateTime, this.endTime.DateTime);
            string strHemoIDLsit = string.Empty;
            var hCount = 0;
            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                StringBuilder sbHemoID = new StringBuilder();
                sbHemoID.Append("AND (");
                for (int i = 0; i < dtPatient.Rows.Count; i++)
                {
                    sbHemoID.Append("HEMODIALYSIS_ID ='").Append(dtPatient.Rows[i][0].ToString()).Append("'").Append(" OR ");
                }
                strHemoIDLsit = sbHemoID.ToString();
                strHemoIDLsit = strHemoIDLsit.Substring(0, strHemoIDLsit.Length - 3);
                strHemoIDLsit += ")";
                hCount = dtPatient.Rows.Count;
            }

            dtCount = _hemodialysisService.GetDeathRate(this.beginTime.DateTime, this.endTime.DateTime, strHemoIDLsit);
            dtCount.AsEnumerable().ToList().ForEach(row =>
            {
                HemoIds = HemoIds + string.Format("','{0}", row["HEMODIALYSIS_ID"].ToString());
            });
            var dCount = dtCount.Rows.Count;
            var newRow = dtBarReport.NewRow();
            newRow["ALLCOUNT"] = hCount;
            newRow["DIECOUNT"] = dCount;
            if (hCount > 0 && dCount > 0)
            {
                var result = Math.Round(dCount / Utility.CDecimal(hCount.ToString()), 4) * 100;
                newRow["RESULT"] = "死亡率 (" + result.ToString() + "%) ";
            }
            else
            {
                newRow["RESULT"] = "";
            }
            dtBarReport.Rows.Add(newRow);
            patientDt = _patientService.GetPatientListByHemoIds(HemoIds);


            dtResult = dtBarReport.Copy();

            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                dtResult.Rows.Clear();

                var r = dtResult.NewRow();
                r["ALLCOUNT"] = hCount - dCount;
                r["DIECOUNT"] = dCount;
                r["RESULT"] = newRow["RESULT"].ToString();


                dtResult.Rows.Add(r);
            }
        }

        /// <summary>
        /// 加载饼图数据
        /// </summary>
        private void LoadPieData()
        {


            ctlSignChart1.DrawDeadCountChart(dtResult);
        }

        private void labelControl3_DoubleClick(object sender, EventArgs e)
        {
            this.endTime.Enabled = !this.endTime.Enabled;
        }
        #endregion


    }
}
