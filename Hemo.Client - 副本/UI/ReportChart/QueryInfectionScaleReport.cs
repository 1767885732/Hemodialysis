/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:男女性别统计查询类
 * 创建标识:吕志强-2017年4月26日
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
using System.Drawing.Imaging;
using Hemo.IService;
using Hemo.IService.PatientSchedule;
using Hemo.Model;

namespace Hemo.Client.UI.ReportChart
{
    public partial class QueryInfectionScaleReport : ViewBase
    {
        #region 类变量

        private IPatient _patientService = ServiceManager.Instance.PatientService;

        private DataTable dtResult = null;

        private IPatientSchedule patientScheduleService = ServiceManager.Instance.PatientSchedule;

        #endregion

        #region 构造函数

        public QueryInfectionScaleReport()
        {
            InitializeComponent();

            var dt = Utility.CDate(patientScheduleService.GetServerDate()).Date; //当前时间  

            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初 
            DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);  //本月月末 

            this.dtMonth.DateTime = dt;

        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryInfectionScaleReport_Load(object sender, EventArgs e)
        {
            CreateResultTable();
            InzationData();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Query_Click(object sender, EventArgs e)
        {
            InzationData();
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
                string name = "213213213213213";
                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.Title = "导出Excel";
                fileDialog.Filter = "Excel文件(*.xls)|*.xls";
                fileDialog.FileName = name;
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



        #endregion

        #region 方法
        /// <summary>
        /// 创建表
        /// </summary>
        private void CreateResultTable()
        {
            dtResult = new DataTable("dtResult");
            dtResult.Columns.Add("CREATETIME", Type.GetType("System.String"));
            dtResult.Columns.Add("ITEMNAME", Type.GetType("System.String"));
            dtResult.Columns.Add("ITEMCOUNT", Type.GetType("System.String"));
            dtResult.Columns.Add("PATIENTCOUNT", Type.GetType("System.String"));
            dtResult.Columns.Add("ITEMSCALE", Type.GetType("System.String"));
        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        private void InzationData()
        {
            using (BackgroundWorker work = new BackgroundWorker())
            {
                this.busyIndicator.ShowLoadingScreenFor(this.chartControl1);
                var dtCureCount = new DataTable();
                var dtEventCount = new PatientModel.MED_HEMO_EVENTINFODataTable();

                DateTime startMonth = dtMonth.DateTime.Date.AddDays(1 - dtMonth.DateTime.Day);  //本月月初 
                DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);  //本月月末 

                work.DoWork += (o, e) =>
                {
                    dtEventCount = _patientService.GetHemoEventInfoByBetweenDt(startMonth, endMonth, "1");
                    dtCureCount = _patientService.GetCureCountByDt(startMonth, endMonth);
                };
                work.RunWorkerCompleted += (o1, e1) =>
                {
                    if (dtEventCount.Rows.Count > 0 && dtCureCount.Rows.Count > 0)
                    {
                        #region 数据封装

                        var patientCount = dtCureCount.Rows[0][0].ToString();

                        if (!patientCount.Equals("0"))
                        {
                            DataRow dr = dtResult.NewRow();
                            dr["CREATETIME"] = dtMonth.DateTime.ToString("yyyy年MM月");
                            dr["ITEMNAME"] = "血透事件发生率";
                            dr["ITEMCOUNT"] = dtEventCount.Rows.Count.ToString();
                            dr["PATIENTCOUNT"] = patientCount.ToString();
                            dr["ITEMSCALE"] = string.Format("{0}%", Math.Round(Convert.ToDecimal(dtEventCount.Rows.Count) / Convert.ToDecimal(patientCount) * 100, 2).ToString());
                            dtResult.Rows.Add(dr);
                            //(口服+肌注+静脉)例数
                            var concretevent = dtEventCount.Where(i => i.CONCRETEEVENT.Equals("1"));
                            var concretevent1 = dtEventCount.Where(i => i.CONCRETEEVENT1.Equals("1"));
                            var concretevent2 = dtEventCount.Where(i => i.CONCRETEEVENT2.Equals("1"));

                            var concreteventAll = concretevent.Count() + concretevent1.Count() + concretevent2.Count();

                            if (concreteventAll > 0)
                            {
                                DataRow dr1 = dtResult.NewRow();
                                dr1["CREATETIME"] = dtMonth.DateTime.ToString("yyyy年MM月");
                                dr1["ITEMNAME"] = "抗菌药物使用率";
                                dr1["ITEMCOUNT"] = concreteventAll.ToString();
                                dr1["PATIENTCOUNT"] = patientCount.ToString();
                                dr1["ITEMSCALE"] = string.Format("{0}%", Math.Round(Convert.ToDecimal(concreteventAll) / Convert.ToDecimal(patientCount) * 100, 2).ToString());
                                dtResult.Rows.Add(dr1);
                            }
                            //血培养例数
                            var xpyevent = dtEventCount.Where(i => i.XQYYX.Equals("1"));
                            if (xpyevent != null && xpyevent.Count() > 0)
                            {
                                DataRow dr2 = dtResult.NewRow();
                                dr2["CREATETIME"] = dtMonth.DateTime.ToString("yyyy年MM月");
                                dr2["ITEMNAME"] = "血培养阳性率";
                                dr2["ITEMCOUNT"] = xpyevent.Count().ToString();
                                dr2["PATIENTCOUNT"] = patientCount.ToString();
                                dr2["ITEMSCALE"] = string.Format("{0}%", Math.Round(Convert.ToDecimal(xpyevent.Count()) / Convert.ToDecimal(patientCount) * 100, 2).ToString());
                                dtResult.Rows.Add(dr2);
                            }

                            //穿刺部位感染率
                            var xgccevent = dtEventCount.Where(i => i.XGCCGR.Equals("1"));
                            if (xgccevent != null && xgccevent.Count() > 0)
                            {
                                DataRow dr3 = dtResult.NewRow();
                                dr3["CREATETIME"] = dtMonth.DateTime.ToString("yyyy年MM月");
                                dr3["ITEMNAME"] = "血透穿刺部位感染率";
                                dr3["ITEMCOUNT"] = xgccevent.Count().ToString();
                                dr3["PATIENTCOUNT"] = patientCount.ToString();
                                dr3["ITEMSCALE"] = string.Format("{0}%", Math.Round(Convert.ToDecimal(xgccevent.Count()) / Convert.ToDecimal(patientCount) * 100, 2).ToString());
                                dtResult.Rows.Add(dr3);
                            }

                            //血流感染
                            var xlevent = dtEventCount.Where(i => i.XLGR.Equals("1"));
                            if (xlevent != null && xlevent.Count() > 0)
                            {
                                DataRow dr4 = dtResult.NewRow();
                                dr4["CREATETIME"] = dtMonth.DateTime.ToString("yyyy年MM月");
                                dr4["ITEMNAME"] = "血流感染率";
                                dr4["ITEMCOUNT"] = xlevent.Count().ToString();
                                dr4["PATIENTCOUNT"] = patientCount.ToString();
                                dr4["ITEMSCALE"] = string.Format("{0}%", Math.Round(Convert.ToDecimal(xlevent.Count()) / Convert.ToDecimal(patientCount) * 100, 2).ToString());
                                dtResult.Rows.Add(dr4);
                            }
                            //血透通路相关性血流感染率
                            var tlxgxevent = dtEventCount.Where(i => i.TLXGXGR.Equals("1"));
                            if (tlxgxevent != null && tlxgxevent.Count() > 0)
                            {
                                DataRow dr5 = dtResult.NewRow();
                                dr5["CREATETIME"] = dtMonth.DateTime.ToString("yyyy年MM月");
                                dr5["ITEMNAME"] = "血管通路相关性血流感染率";
                                dr5["ITEMCOUNT"] = tlxgxevent.Count().ToString();
                                dr5["PATIENTCOUNT"] = patientCount.ToString();
                                dr5["ITEMSCALE"] = string.Format("{0}%", Math.Round(Convert.ToDecimal(tlxgxevent.Count()) / Convert.ToDecimal(patientCount) * 100, 2).ToString());
                                dtResult.Rows.Add(dr5);
                            }
                            //血透通路感染率
                            var tlgrevent = dtEventCount.Where(i => i.TLGR.Equals("1"));
                            if (tlgrevent != null && tlgrevent.Count() > 0)
                            {
                                DataRow dr6 = dtResult.NewRow();
                                dr6["CREATETIME"] = dtMonth.DateTime.ToString("yyyy年MM月");
                                dr6["ITEMNAME"] = "血管通路感染率";
                                dr6["ITEMCOUNT"] = tlgrevent.Count().ToString();
                                dr6["PATIENTCOUNT"] = patientCount.ToString();
                                dr6["ITEMSCALE"] = string.Format("{0}%", Math.Round(Convert.ToDecimal(tlgrevent.Count()) / Convert.ToDecimal(patientCount) * 100, 2).ToString());
                                dtResult.Rows.Add(dr6);
                            }
                        }
                        #endregion
                    }
                    this.gcCount.DataSource = dtResult;

                    this.busyIndicator.HideLoadingScreen();
                };

                work.RunWorkerAsync();
            }
        }


        #endregion

        private void gvCount_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var ItemRow = gvCount.GetFocusedDataRow() as DataRow;

            DataTable dtSource = new DataTable();
            dtSource.Columns.Add("NAME", typeof(string));
            dtSource.Columns.Add("COUNT", typeof(decimal));

            var row = dtSource.NewRow();

            row["NAME"] = ItemRow["ITEMNAME"].ToString().Replace("率", "例数");
            row["COUNT"] = Convert.ToDecimal(ItemRow["ITEMCOUNT"].ToString());
            dtSource.Rows.Add(row);

            var row1 = dtSource.NewRow();

            row1["NAME"] = "其他患者数";
            row1["COUNT"] = Convert.ToDecimal(ItemRow["PATIENTCOUNT"].ToString()) - Convert.ToDecimal(ItemRow["ITEMCOUNT"].ToString());
            dtSource.Rows.Add(row1);


            this.chartControl1.Series.Clear();
            this.chartControl1.Titles.Clear();


            Series serSex = new Series(string.Empty, ViewType.Pie);
            serSex.DataSource = dtSource;
            ChartTitle ct = new ChartTitle();
            ct.Text = ItemRow["ITEMNAME"].ToString();
            ct.TextColor = Color.Black;//颜色
            ct.Font = new Font("Tahoma", 12);//字体
            ct.Dock = ChartTitleDockStyle.Top;//停靠在上方
            ct.Alignment = StringAlignment.Center;//居中显示

            this.chartControl1.Titles.Add(ct);

            DevExpress.XtraCharts.PieSeriesView pieSeriesView = new DevExpress.XtraCharts.PieSeriesView();
            pieSeriesView.Rotation = 90;
            pieSeriesView.ExplodeMode = PieExplodeMode.MaxValue;//突出显示最大的饼块。
            pieSeriesView.RuntimeExploding = true;//设置了
            serSex.View = pieSeriesView;

            serSex.PointOptions.PointView = PointView.ArgumentAndValues;//显示表示的信息和数据  
            serSex.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;//NumericFormat.Percent;//用百分比表示
            serSex.PointOptions.ValueNumericOptions.Precision = 1;//百分号前面的数字不跟小数点
            serSex.ValueDataMembers.AddRange(new string[] { "COUNT" });//绑定值的字段
            serSex.ValueScaleType = ScaleType.Numerical;//数字类型
            serSex.ArgumentDataMember = "NAME";//绑定饼块的描述文字
            serSex.ArgumentScaleType = ScaleType.Qualitative;//定性的
            this.chartControl1.Series.Add(serSex);

            //图例位置
            this.chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right; //靠右
            this.chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Top; //顶部
        }
    }
}
