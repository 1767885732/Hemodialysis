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
using System.Drawing.Imaging;
using Hemo.Model;

namespace Hemo.Client.UI.ReportChart
{
    public partial class QueryTouQianTiZhongDaBiaoReport : ViewBase
    {
        #region 类变量

        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        private DataTable dtResult = null;

        private bool isConstant = true;

        #endregion

        #region 构造函数

        public QueryTouQianTiZhongDaBiaoReport()
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
        private void QueryInfectousScaleReport_Load(object sender, EventArgs e)
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
                fileDialog.FileName = this.beginTime.DateTime.ToString("yyyyMMdd") + "-" + this.endTime.DateTime.ToString("yyyyMMdd") + "透前体重达标";
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
            reportNote.ReportType = ReportTypeEnum.透前体重达标;
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
            fileDialog.FileName = this.beginTime.DateTime.ToString("yyyyMMdd") + "-" + this.endTime.DateTime.ToString("yyyyMMdd") + "透前体重达标";
            fileDialog.RestoreDirectory = true;
            DialogResult dialogResult = fileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                this.chartControl1.ExportToImage(fileDialog.FileName, ImageFormat.Jpeg);
            }
        }

        private void work_DoWork(object sender, DoWorkEventArgs e)
        {
            InitDictData();

            LoadChartData();
        }

        private ConfigModel.MED_COMMON_ITEMLISTDataTable dtCommonItemList;
        private void InitDictData()
        {
            string itemType = "净化方式";
            dtCommonItemList = _hemodialysisService.GetCommonItemListByItemType(itemType);
        }


        private void work_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadPieData(null);
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

        private float ConvertToFloat(object obj)
        {
            float result;
            float.TryParse(obj.ToString(), out result);
            return result;
        }

        private float ConvertToInt(object obj)
        {
            float result;
            float.TryParse(obj.ToString(), out result);
            return result;
        }

        private DataTable InitDataTable()
        {
            DataTable pDtResult = new DataTable();
            DataColumn dcDate = new DataColumn("Date", typeof(string));
            pDtResult.Columns.Add(dcDate);
            DataColumn dcName = new DataColumn("Name", typeof(string));
            pDtResult.Columns.Add(dcName);
            DataColumn dcHemoID = new DataColumn("HemoID", typeof(string));
            pDtResult.Columns.Add(dcHemoID);
            DataColumn dcTotalCount = new DataColumn("TotalCount", typeof(int));
            dcTotalCount.DefaultValue = 0;
            pDtResult.Columns.Add(dcTotalCount);

            //未限制空以及重复值
            foreach (ConfigModel.MED_COMMON_ITEMLISTRow dr in dtCommonItemList.Rows)
            {
                DataColumn dcType = new DataColumn(dr.ITEM_NAME.ToString() + "Count", typeof(int));
                dcType.DefaultValue = 0;
                pDtResult.Columns.Add(dcType);
            }
            DataColumn dcDaBiaoCount = new DataColumn("DaBiaoCount", typeof(int));
            dcDaBiaoCount.DefaultValue = 0;
            pDtResult.Columns.Add(dcDaBiaoCount);

            return pDtResult;
        }


        private int GetDaBiaoValue(object dryWeight, object beforeDryWeight, object dryWeightTag)
        {
            float daBiaoLvResult = ConvertToFloat(this.teDaBiaoLv.Text) * ((float)0.01);

            float dryWeightT = ConvertToFloat(dryWeight);
            float StandardWeight = dryWeightT * daBiaoLvResult;
            float perWeight = ConvertToFloat(beforeDryWeight) - ConvertToFloat(dryWeightTag);
            int daBiao = (StandardWeight - perWeight) < 0 ? 0 : 1;//0：不达标；1：达标；
            return daBiao;
        }


        private void GetGridData(string searchHemoID)
        {
            DataTable dtSubCount = _hemodialysisService.GetCureMainByHemoIdAndDate(searchHemoID, this.beginTime.DateTime, this.endTime.DateTime);

            float daBiaoLvResult = ConvertToFloat(this.teDaBiaoLv.Text);

            //患者—>根据净化方式分组，计算各分组治疗次数
            var resultPart1 = dtSubCount.AsEnumerable().ToList().GroupBy(row => { return new { hemoID = row["hemodialysis_id"], name = row["name"], mode = row["purification_mode"], modeName = row["item_name"] }; })
                 .Select(row => { return new { hemoID = row.Key.hemoID, name = row.Key.name, mode = row.Key.mode, modeName = row.Key.modeName, count = row.Count(), totalCount = dtSubCount.Rows.Count }; });

            //患者—>根据达标率计算各次治疗是否达标—>得出总达标次数
            int daBiaoCount = dtSubCount.AsEnumerable().ToList().Select(row => new
            {
                hemoID = row["hemodialysis_id"],
                mode = row["purification_mode"],
                daBiao = GetDaBiaoValue(row["dry_weight"], row["before_dry_weight"], row["dry_weight_tag"])
            }).Sum(row => row.daBiao);

            DataRow dr = dtResult.NewRow();
            dr["Date"] = this.beginTime.Text.ToString() + "-" + this.endTime.Text.ToString();
            DataTable currentPatient = _hemodialysisService.GetPatientByHemoId(searchHemoID);
            dr["Name"] = currentPatient.AsEnumerable().First()["NAME"];
            dr["HemoID"] = searchHemoID;
            dr["DaBiaoCount"] = daBiaoCount;

            dtResult.Rows.Add(dr);

            foreach (var item in resultPart1)
            {
                dr["TotalCount"] = item.totalCount.ToString();
                dr[item.modeName.ToString() + "Count"] = item.count.ToString();
            }
        }

        /// <summary>
        /// 加载报表数据
        /// </summary>
        private void LoadChartData()
        {
            dtResult = null;
            DataTable dtPatient = isConstant ? _hemodialysisService.GetHemoIdInLastWeekAndThreeMonthsByDate(this.beginTime.DateTime, this.endTime.DateTime) : _hemodialysisService.GetHemoIdByDate(this.beginTime.DateTime, this.endTime.DateTime);

            string searchHemoID = this.teHemodialysisID.Text;
            string searchName = this.tePatientName.Text;

            var dtPatientSelected = dtPatient.AsEnumerable().Where(row =>
            {
                bool notExistSearchHemoID = string.IsNullOrWhiteSpace(searchHemoID);
                bool notExistSearchPatientName = string.IsNullOrWhiteSpace(searchName);
                if (notExistSearchHemoID && notExistSearchPatientName)
                {
                    return true;
                }
                string hemoID = row["HEMODIALYSIS_ID"].ToString();
                if (!notExistSearchHemoID)
                {
                    if (hemoID == searchHemoID)
                    {
                        return true;
                    }
                }
                if (!notExistSearchPatientName)
                {
                    DataTable patientTemp = _hemodialysisService.GetPatientByHemoId(hemoID);
                    var existName = patientTemp.AsEnumerable().First().Field<string>("NAME").Contains(searchName);
                    if (existName)
                    {
                        return true;
                    }
                }
                return false;
            });

            if (dtPatientSelected != null && dtPatientSelected.Count() > 0)
            {
                //创建结果
                dtResult = InitDataTable();
                dtPatientSelected.AsEnumerable().ToList().ForEach(row => { GetGridData(row["HEMODIALYSIS_ID"].ToString()); });
            }
        }

        /// <summary>
        /// 加载饼图数据
        /// </summary>
        private void LoadPieData(DataRow dr)
        {
            this.chartControl1.Series.Clear();
            this.chartControl1.Titles.Clear();

            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                if (dr == null)
                {
                    dr = dtResult.AsEnumerable().First();
                }
                DataTable dtPie = new DataTable();
                dtPie.Columns.Add("PieName", typeof(string));
                dtPie.Columns.Add("PieValue", typeof(int));
                DataRow drPie1 = dtPie.NewRow();
                drPie1["PieName"] = "治疗总次数";
                drPie1["PieValue"] = dr["TotalCount"];
                dtPie.Rows.Add(drPie1);
                DataRow drPie2 = dtPie.NewRow();
                drPie2["PieName"] = "达标次数";
                drPie2["PieValue"] = dr["DaBiaoCount"];
                dtPie.Rows.Add(drPie2);

                Series seriousPie = new Series(string.Empty, ViewType.Pie);
                seriousPie.DataSource = dtPie;
                ChartTitle cTitle = new ChartTitle();
                string title = dr["name"].ToString() + "（" + dr["hemoID"].ToString() + "）" + dr["Date"].ToString() + "—" + "透前体重达标：总次数（" + dr["TotalCount"].ToString() + "）次，达标次数（" + dr["DaBiaoCount"].ToString() + "）次 ,达标率：" + (Utility.CDecimal(dr["DaBiaoCount"].ToString()) / Utility.CDecimal(dr["TotalCount"].ToString()) *100).ToString() + "%";
                cTitle.Text = title;
                cTitle.TextColor = Color.Black;
                cTitle.Font = new Font("Tahoma", 12);
                cTitle.Dock = ChartTitleDockStyle.Top;
                cTitle.Alignment = StringAlignment.Center;
                this.chartControl1.Titles.Add(cTitle);
                PieSeriesView pieSeriesView = new PieSeriesView();
                pieSeriesView.Rotation = 90;
                pieSeriesView.ExplodeMode = PieExplodeMode.MaxValue;
                pieSeriesView.RuntimeExploding = true;
                seriousPie.View = pieSeriesView;
                seriousPie.PointOptions.PointView = PointView.ArgumentAndValues;
                seriousPie.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                seriousPie.PointOptions.ValueNumericOptions.Precision = 1;
                seriousPie.ArgumentDataMember = "PieName";
                seriousPie.ValueDataMembers.AddRange(new string[] { "PieValue" });
                seriousPie.ValueScaleType = ScaleType.Numerical;
                seriousPie.ArgumentScaleType = ScaleType.Qualitative;
                this.chartControl1.Series.Add(seriousPie);

                //图例位置
                this.chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
                this.chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
            }
        }

        private void gvCount_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                DataRow drClicked = this.gvCount.GetDataRow(e.RowHandle);
                LoadPieData(drClicked);
            }
        }

        #endregion


    }
}
