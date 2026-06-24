/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:感染统计查询类
 * 创建标识:吕志强-2017年4月24日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.UI.Machine;
using Hemo.IService.Config;
using Hemo.Service;
using System.Linq;
using Hemo.Utilities;

namespace Hemo.Client.UI.ReportChart
{
    public partial class QueryQualityMonitorInfectReport : ViewBase
    {
        #region 类变量

        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        private DataTable dtResult = null;

        private DateTime beginTime;

        private DateTime endTime;

        public DataTable dtMonitorInfect = new DataTable();

        #endregion

        #region 属性

        public DateTime BeginTime
        {
            get { return beginTime; }
            set { beginTime = value; }
        }

        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        #endregion

        #region 构造函数

        public QueryQualityMonitorInfectReport()
        {
            InitializeComponent();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载数据
        /// </summary>
        public void LoadData()
        {
            this.busyIndicator.ShowLoadingScreenFor(this.gcInfect);
            using (BackgroundWorker work = new BackgroundWorker())
            {
                work.DoWork += new DoWorkEventHandler(work_DoWork);
                work.RunWorkerCompleted += new RunWorkerCompletedEventHandler(work_RunWorkerCompleted);
                work.RunWorkerAsync();
            }
        }

        private void work_DoWork(object sender, DoWorkEventArgs e)
        {
            dtResult = null;
            DataTable dtPatient = _hemodialysisService.GetHemoIdInLastWeekAndThreeMonthsByDate(beginTime, endTime);
            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                DataTable dtCount = null;
                dtPatient.AsEnumerable().ToList().ForEach(row =>
                {
                    DataTable dtSubCount = _hemodialysisService.GetInfectousCountByHemoIdAndDate(row["HEMODIALYSIS_ID"].ToString(), beginTime, endTime);
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
                                findRow["YXGY_COUNT"] = Utility.CInt(findRow["YXGY_COUNT"].ToString()) + Utility.CInt(r["YXGY_COUNT"].ToString());
                                findRow["BXGY_COUNT"] = Utility.CInt(findRow["BXGY_COUNT"].ToString()) + Utility.CInt(r["BXGY_COUNT"].ToString());
                                findRow["AZB_COUNT"] = Utility.CInt(findRow["AZB_COUNT"].ToString()) + Utility.CInt(r["AZB_COUNT"].ToString());
                                findRow["MD_COUNT"] = Utility.CInt(findRow["MD_COUNT"].ToString()) + Utility.CInt(r["MD_COUNT"].ToString());
                                findRow["QY_COUNT"] = Utility.CInt(findRow["QY_COUNT"].ToString()) + Utility.CInt(r["QY_COUNT"].ToString());
                                findRow["DC_COUNT"] = Utility.CInt(findRow["DC_COUNT"].ToString()) + Utility.CInt(r["DC_COUNT"].ToString());
                                findRow["NORMAL_COUNT"] = Utility.CInt(findRow["NORMAL_COUNT"].ToString()) + Utility.CInt(r["NORMAL_COUNT"].ToString());
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
                    r["YXGY_COUNT"] = dtResult.Compute("Sum(YXGY_COUNT)", string.Empty);
                    r["BXGY_COUNT"] = dtResult.Compute("Sum(BXGY_COUNT)", string.Empty);
                    r["AZB_COUNT"] = dtResult.Compute("Sum(AZB_COUNT)", string.Empty);
                    r["MD_COUNT"] = dtResult.Compute("Sum(MD_COUNT)", string.Empty);
                    r["QY_COUNT"] = dtResult.Compute("Sum(QY_COUNT)", string.Empty);
                    r["DC_COUNT"] = dtResult.Compute("Sum(DC_COUNT)", string.Empty);
                    r["NORMAL_COUNT"] = dtResult.Compute("Sum(NORMAL_COUNT)", string.Empty);
                    r["SUB_COUNT"] = dtResult.Compute("Sum(SUB_COUNT)", string.Empty);
                    dtResult.Rows.Add(r);
                }
            }
        }

        private void work_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DataTable dtInfect = null;
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                dtInfect = new DataTable();
                dtInfect.Columns.Add("INFECT_NAME", typeof(System.String));
                dtInfect.Columns.Add("INFECT_CONDITION", typeof(System.String));
                dtInfect.Columns.Add("INFECT_COUNT", typeof(System.Int32));
                dtInfect.Columns.Add("INFECT_RATIO", typeof(System.String));

                foreach (DataColumn column in dtResult.Columns)
                {
                    if (column.ColumnName.Equals("CREATE_MONTH") || column.ColumnName.Equals("SUB_COUNT"))
                    {
                        continue;
                    }

                    var row = dtInfect.NewRow();
                    string name = column.ColumnName.Substring(0, column.ColumnName.IndexOf("_"));
                    Double value = Utility.CDouble(dtResult.Rows[0][column.ColumnName].ToString()) * 100 / Utility.CDouble(dtResult.Rows[0]["SUB_COUNT"].ToString());
                    row["INFECT_NAME"] = "传染病例数";
                    row["INFECT_CONDITION"] = name.Equals("YXGY") ? "乙型肝炎" : (name.Equals("BXGY") ? "丙型肝炎" : (name.Equals("AZB") ? "艾滋病" : (name.Equals("MD") ? "梅毒" : (name.Equals("QY") ? "全阴" : (name.Equals("DC") ? "待查" : "无传染病")))));
                    row["INFECT_COUNT"] = dtResult.Rows[0][column.ColumnName];
                    row["INFECT_RATIO"] = Math.Round(value, 1).ToString() + "%";
                    dtInfect.Rows.Add(row);
                }

                var rowt = dtInfect.NewRow();
                rowt["INFECT_NAME"] = "传染病例数";
                rowt["INFECT_CONDITION"] = "除乙肝丙肝其他传染病";
                string count = string.Empty;
                string ratio = string.Empty;
                dtResult.AsEnumerable().ToList().ForEach(row =>
                {
                    if (row["CREATE_MONTH"].ToString() == "合计")
                    {
                        count = (Utility.CDouble(row["AZB_COUNT"].ToString()) + Utility.CDouble((row["MD_COUNT"].ToString()))).ToString();
                        ratio = Math.Round(Utility.CDouble(count) * 100 / Utility.CDouble(row["SUB_COUNT"].ToString()), 1).ToString() + "%";
                    }
                });

                rowt["INFECT_COUNT"] = count;
                rowt["INFECT_RATIO"] = ratio;
                dtInfect.Rows.Add(rowt);

            }

            this.gcInfect.DataSource = dtInfect;
            dtMonitorInfect = dtInfect;
            this.busyIndicator.HideLoadingScreen();
        }

        /// <summary>
        /// 获取院感结果
        /// </summary>
        /// <returns></returns>
        public DataTable GetInfectionResult()
        {
            return this.gcInfect.DataSource as DataTable;
        }

        #endregion
    }
}
