/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:质量管理治疗统计查询类
 * 创建标识:吕志强-2017年4月22日
 * 
 * 修改时间:2017年5月16日
 * 修改人:刘超
 * 修改描述:修改部分业务逻辑
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
using Hemo.IService;
using Hemo.Model;

namespace Hemo.Client.UI.ReportChart
{
    public partial class QueryQualityMonitorCureReport : ViewBase
    {
        #region 类变量

        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        private IPatient _patientService = ServiceManager.Instance.PatientService;
        private DataTable dtAllResult = null;//数据汇总

        private DataTable dtVascularResult = null;//导管手术例数

        private DataTable dtSexResult = null;//透析男女比例

        private DataTable dtAgeResult = null;//透析年龄段

        private DataTable dtRegularResult = null;//规律透析比例

        private DataTable dtPatient = null;//维持性透析人数

        private DataTable dtPatientWhere = null;//去向患者

        private DataTable dtPatientProtopathy = null;//原发病人数

        private ConfigModel.MED_PATIENTS_OPERATORDataTable dtPatientOperator = null;

        public DataTable dtMonitorCure = new DataTable();

        private DateTime beginTime;

        private DateTime endTime;

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

        public QueryQualityMonitorCureReport()
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
            this.busyIndicator.ShowLoadingScreenFor(this.gcCure);
            using (BackgroundWorker work = new BackgroundWorker())
            {
                work.DoWork += new DoWorkEventHandler(work_DoWork);
                work.RunWorkerCompleted += new RunWorkerCompletedEventHandler(work_RunWorkerCompleted);
                work.RunWorkerAsync();
            }
        }
        /// 1、使用自体内瘘的患者（自体内瘘）
        /// 使用移植物内瘘的患者（人造血管）
        /// 使用双静脉作为维持血管通路的患者（双静脉）
        /// 使用带cuff中心静脉留置导管作为维持血管通路的患者（颈内静脉+永久性通路）
        /// 使用其他维持血管通路的患者以上是基本资料统计文档中的内容，如何与系统中通路方式对应
        private void work_DoWork(object sender, DoWorkEventArgs e)
        {
            dtAllResult = null;
            dtVascularResult = null;
            dtSexResult = null;
            dtAgeResult = null;
            dtRegularResult = null;
            dtPatient = null;
            dtPatientWhere = null;
            dtPatientProtopathy = null;
            //去向为死亡的病人
            dtPatientWhere = _patientService.GetPatientListByWhere("2");
            //维持性透析人数
            dtPatient = _hemodialysisService.GetHemoIdInLastWeekAndThreeMonthsByDate(beginTime, endTime);
            //原发病人数
            dtPatientProtopathy = _hemodialysisService.GetPatientBaseRecordProtopathyByDate(beginTime, endTime);

            dtPatientOperator = _patientService.GetPatientOperatorByDate(beginTime, endTime, string.Empty);

            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                DataTable dtCount = null;

                #region 数据汇总

                dtPatient.AsEnumerable().ToList().ForEach(row =>
                {
                    DataTable dtSubCount = _hemodialysisService.GetCureCountByHemoIdAndDate(row["HEMODIALYSIS_ID"].ToString(), beginTime, endTime);
                    dtCount = dtCount ?? (dtSubCount != null ? dtSubCount.Clone() : dtCount);
                    if (dtSubCount != null && dtSubCount.Rows.Count > 0)
                    {
                        dtSubCount.AsEnumerable().ToList().ForEach(r =>
                        {
                            var findRow = dtCount.AsEnumerable().FirstOrDefault(i => i["CURE_MONTH"].ToString().Equals(r["CURE_MONTH"].ToString()));
                            if (findRow == null)
                            {
                                dtCount.ImportRow(r);
                            }
                            else
                            {
                                findRow["SUB_COUNT"] = Utility.CInt(findRow["SUB_COUNT"].ToString()) + Utility.CInt(r["SUB_COUNT"].ToString());
                                findRow["HD_COUNT"] = Utility.CInt(findRow["HD_COUNT"].ToString()) + Utility.CInt(r["HD_COUNT"].ToString());
                                findRow["HDF_COUNT"] = Utility.CInt(findRow["HDF_COUNT"].ToString()) + Utility.CInt(r["HDF_COUNT"].ToString());
                                findRow["HF_COUNT"] = Utility.CInt(findRow["HF_COUNT"].ToString()) + Utility.CInt(r["HF_COUNT"].ToString());
                                findRow["HP_COUNT"] = Utility.CInt(findRow["HP_COUNT"].ToString()) + Utility.CInt(r["HP_COUNT"].ToString());
                                findRow["HDHP_COUNT"] = Utility.CInt(findRow["HDHP_COUNT"].ToString()) + Utility.CInt(r["HDHP_COUNT"].ToString());
                                findRow["CRRT_COUNT"] = Utility.CInt(findRow["CRRT_COUNT"].ToString()) + Utility.CInt(r["CRRT_COUNT"].ToString());
                            }
                        });
                    }
                });

                if (dtCount != null && dtCount.Rows.Count > 0)
                {
                    dtAllResult = dtCount.Clone();
                    dtCount.AsEnumerable().OrderBy(row => row["CURE_MONTH"].ToString()).CopyToDataTable(dtAllResult, LoadOption.OverwriteChanges);
                    var r = dtAllResult.NewRow();
                    r["CURE_MONTH"] = "合计";
                    r["SUB_COUNT"] = dtAllResult.Compute("Sum(SUB_COUNT)", string.Empty);
                    r["HD_COUNT"] = dtAllResult.Compute("Sum(HD_COUNT)", string.Empty);
                    r["HDF_COUNT"] = dtAllResult.Compute("Sum(HDF_COUNT)", string.Empty);
                    r["HF_COUNT"] = dtAllResult.Compute("Sum(HF_COUNT)", string.Empty);
                    r["HP_COUNT"] = dtAllResult.Compute("Sum(HP_COUNT)", string.Empty);
                    r["HDHP_COUNT"] = dtAllResult.Compute("Sum(HDHP_COUNT)", string.Empty);
                    r["CRRT_COUNT"] = dtAllResult.Compute("Sum(CRRT_COUNT)", string.Empty);
                    dtAllResult.Rows.Add(r);
                }

                #endregion

                #region 血管通路

                dtCount = null;
                dtPatient.AsEnumerable().ToList().ForEach(row =>
                {
                    DataTable dtSubCount = _hemodialysisService.GetAccessCountByHemoIdAndDate(row["HEMODIALYSIS_ID"].ToString(), beginTime, endTime);
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
                                findRow["SJM_COUNT"] = Utility.CInt(findRow["SJM_COUNT"].ToString()) + Utility.CInt(r["SJM_COUNT"].ToString());
                                findRow["RZXG_COUNT"] = Utility.CInt(findRow["RZXG_COUNT"].ToString()) + Utility.CInt(r["SJM_COUNT"].ToString());
                                findRow["LSG_COUNT"] = Utility.CInt(findRow["LSG_COUNT"].ToString()) + Utility.CInt(r["LSG_COUNT"].ToString());
                                findRow["BYJDG_COUNT"] = Utility.CInt(findRow["BYJDG_COUNT"].ToString()) + Utility.CInt(r["BYJDG_COUNT"].ToString());
                                findRow["YJDG_COUNT"] = Utility.CInt(findRow["YJDG_COUNT"].ToString()) + Utility.CInt(r["YJDG_COUNT"].ToString());
                                findRow["SUB_COUNT"] = Utility.CInt(findRow["SUB_COUNT"].ToString()) + Utility.CInt(r["SUB_COUNT"].ToString());
                            }
                        });
                    }
                });

                if (dtCount != null && dtCount.Rows.Count > 0)
                {
                    dtVascularResult = dtCount.Clone();
                    dtCount.AsEnumerable().OrderBy(row => row["CREATE_MONTH"].ToString()).CopyToDataTable(dtVascularResult, LoadOption.OverwriteChanges);
                    var r = dtVascularResult.NewRow();
                    r["CREATE_MONTH"] = "合计";
                    r["NL_COUNT"] = dtVascularResult.Compute("Sum(NL_COUNT)", string.Empty);
                    r["SJM_COUNT"] = dtVascularResult.Compute("Sum(SJM_COUNT)", string.Empty);
                    r["RZXG_COUNT"] = dtVascularResult.Compute("Sum(SJM_COUNT)", string.Empty);
                    r["LSG_COUNT"] = dtVascularResult.Compute("Sum(LSG_COUNT)", string.Empty);
                    r["BYJDG_COUNT"] = dtVascularResult.Compute("Sum(BYJDG_COUNT)", string.Empty);
                    r["YJDG_COUNT"] = dtVascularResult.Compute("Sum(YJDG_COUNT)", string.Empty);
                    r["SUB_COUNT"] = dtVascularResult.Compute("Sum(SUB_COUNT)", string.Empty);
                    dtVascularResult.Rows.Add(r);
                }

                #endregion

                #region 透析男女比例

                dtCount = null;
                dtPatient.AsEnumerable().ToList().ForEach(row =>
                {
                    DataTable dtSubCount = _hemodialysisService.GetSexCountByHemoIdAndDate2(row["HEMODIALYSIS_ID"].ToString(), beginTime, endTime);
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
                                findRow["MAN_COUNT"] = Utility.CInt(findRow["MAN_COUNT"].ToString()) + Utility.CInt(r["MAN_COUNT"].ToString());
                                findRow["WOMAN_COUNT"] = Utility.CInt(findRow["WOMAN_COUNT"].ToString()) + Utility.CInt(r["WOMAN_COUNT"].ToString());
                                findRow["SUB_COUNT"] = Utility.CInt(findRow["SUB_COUNT"].ToString()) + Utility.CInt(r["SUB_COUNT"].ToString());
                            }
                        });
                    }
                });

                if (dtCount != null && dtCount.Rows.Count > 0)
                {
                    dtSexResult = dtCount.Clone();
                    dtCount.AsEnumerable().OrderBy(row => row["CREATE_MONTH"].ToString()).CopyToDataTable(dtSexResult, LoadOption.OverwriteChanges);
                    var r = dtSexResult.NewRow();
                    r["CREATE_MONTH"] = "合计";
                    r["MAN_COUNT"] = dtSexResult.Compute("Sum(MAN_COUNT)", string.Empty);
                    r["WOMAN_COUNT"] = dtSexResult.Compute("Sum(WOMAN_COUNT)", string.Empty);
                    r["SUB_COUNT"] = dtSexResult.Compute("Sum(SUB_COUNT)", string.Empty);
                    dtSexResult.Rows.Add(r);
                }

                #endregion

                #region 透析年龄段

                dtCount = null;
                dtPatient.AsEnumerable().ToList().ForEach(row =>
                {
                    DataTable dtSubCount = _hemodialysisService.GetAgeCountByHemoIdAndDate(row["HEMODIALYSIS_ID"].ToString(), beginTime, endTime);
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
                                findRow["COUNT_1_20"] = Utility.CInt(findRow["COUNT_1_20"].ToString()) + Utility.CInt(r["COUNT_1_20"].ToString());
                                findRow["COUNT_20_40"] = Utility.CInt(findRow["COUNT_20_40"].ToString()) + Utility.CInt(r["COUNT_20_40"].ToString());
                                findRow["COUNT_40_60"] = Utility.CInt(findRow["COUNT_40_60"].ToString()) + Utility.CInt(r["COUNT_40_60"].ToString());
                                findRow["COUNT_60_100"] = Utility.CInt(findRow["COUNT_60_100"].ToString()) + Utility.CInt(r["COUNT_60_100"].ToString());
                                findRow["SUB_COUNT"] = Utility.CInt(findRow["SUB_COUNT"].ToString()) + Utility.CInt(r["SUB_COUNT"].ToString());
                            }
                        });
                    }
                });

                if (dtCount != null && dtCount.Rows.Count > 0)
                {
                    dtAgeResult = dtCount.Clone();
                    dtCount.AsEnumerable().OrderBy(row => row["CREATE_MONTH"].ToString()).CopyToDataTable(dtAgeResult, LoadOption.OverwriteChanges);
                    var r = dtAgeResult.NewRow();
                    r["CREATE_MONTH"] = "合计";
                    r["COUNT_1_20"] = dtAgeResult.Compute("Sum(COUNT_1_20)", string.Empty);
                    r["COUNT_20_40"] = dtAgeResult.Compute("Sum(COUNT_20_40)", string.Empty);
                    r["COUNT_40_60"] = dtAgeResult.Compute("Sum(COUNT_40_60)", string.Empty);
                    r["COUNT_60_100"] = dtAgeResult.Compute("Sum(COUNT_60_100)", string.Empty);
                    r["SUB_COUNT"] = dtAgeResult.Compute("Sum(SUB_COUNT)", string.Empty);
                    dtAgeResult.Rows.Add(r);
                }

                #endregion

                #region 规律透析比例
                //GetWeekTwoOrThirdCountPatientsHemoId
                dtCount = null;
                var dtTwoOrThird = _hemodialysisService.GetWeekTwoOrThirdCountPatientsHemoId();
                DataTable dtTwoOrThirdNew = new DataTable();
                dtTwoOrThirdNew = dtTwoOrThird.Clone();
                dtPatient.AsEnumerable().ToList().ForEach(row =>
                {
                    DataTable dtSubCount = _hemodialysisService.GetRegularCountByHemoIdAndDate(row["HEMODIALYSIS_ID"].ToString(), beginTime, endTime);
                    dtCount = dtCount ?? (dtSubCount != null ? dtSubCount.Clone() : dtCount);
                    if (dtSubCount != null && dtSubCount.Rows.Count > 0)
                    {
                        dtSubCount.AsEnumerable().ToList().ForEach(r =>
                        {
                            var findRow = dtCount.AsEnumerable().FirstOrDefault(i => i["CURE_MONTH"].ToString().Equals(r["CURE_MONTH"].ToString()));
                            if (findRow == null)
                            {
                                dtCount.ImportRow(r);
                            }
                            else
                            {
                                findRow["TWO_TIME"] = Utility.CInt(findRow["TWO_TIME"].ToString()) + Utility.CInt(r["TWO_TIME"].ToString());
                                findRow["THREE_TIME"] = Utility.CInt(findRow["THREE_TIME"].ToString()) + Utility.CInt(r["THREE_TIME"].ToString());
                                findRow["FOUR_TIME"] = Utility.CInt(findRow["FOUR_TIME"].ToString()) + Utility.CInt(r["FOUR_TIME"].ToString());
                                findRow["FIVE_TIME"] = Utility.CInt(findRow["FIVE_TIME"].ToString()) + Utility.CInt(r["FIVE_TIME"].ToString());
                            }
                        });
                    }

                    if (dtTwoOrThird != null && dtTwoOrThird.Rows.Count > 0)
                    {
                        dtTwoOrThird.AsEnumerable().ToList().ForEach(r =>
                        {
                            if (r["HEMODIALYSIS_ID"].ToString() == row["HEMODIALYSIS_ID"].ToString())
                            {
                                var nr = dtTwoOrThirdNew.NewRow();
                                nr["HEMODIALYSIS_ID"] = r["HEMODIALYSIS_ID"];
                                nr["WEEK_MONDAY"] = r["WEEK_MONDAY"];
                                nr["COUNT"] = r["COUNT"];
                                dtTwoOrThirdNew.Rows.Add(nr);
                            }
                        });
                    }
                });
                dtCount.Columns.Add("TWOTHIRD_TIME", Type.GetType("System.Int32"));
                dtCount.AsEnumerable().ToList().ForEach(row =>
                {
                    row["TWOTHIRD_TIME"] = 0;
                });
                //每个患者从最近的一周往前推十二周，最近一周可能是新的开始周也有可能是结束周
                var n = (from d in dtTwoOrThirdNew.AsEnumerable() select d["HEMODIALYSIS_ID"]).Distinct();
                DataTable dtHEMOIDs = new DataTable();
                dtHEMOIDs.Columns.Add("HEMODIALYSIS_ID", Type.GetType("System.String"));
                foreach (var item in n)
                {
                    dtHEMOIDs.Rows.Add(item);
                }
                if (dtHEMOIDs != null && dtHEMOIDs.Rows.Count > 0)
                {
                    for (var i = 0; i < dtHEMOIDs.Rows.Count; i++)
                    {
                        var dtTemp = dtTwoOrThirdNew.Clone();
                        dtTwoOrThirdNew.AsEnumerable().Where(row => row["HEMODIALYSIS_ID"].ToString().Equals(dtHEMOIDs.Rows[i]["HEMODIALYSIS_ID"].ToString())).CopyToDataTable<DataRow>(dtTemp, LoadOption.PreserveChanges);

                        if (dtTemp.AsEnumerable().Where(row => row["COUNT"].ToString().Equals("2")).Count() == 0)
                        {
                            continue;
                        }
                        if (dtTemp.AsEnumerable().Where(row => row["COUNT"].ToString().Equals("3")).Count() == 0)
                        {
                            continue;
                        }

                        if (dtTemp.Rows.Count == 12) //12周都有数据说明才是规律性透析患者
                        {
                            //对该患者按时间的倒序进行排序
                            var dtEnd = dtTemp.Clone();
                            dtTemp.AsEnumerable().OrderByDescending(row => row.Field<DateTime?>("WEEK_MONDAY")).CopyToDataTable<DataRow>(dtEnd, LoadOption.PreserveChanges);
                            var dtEnd2 = dtTemp.Clone();
                            dtTemp.AsEnumerable().OrderByDescending(row => row.Field<DateTime?>("WEEK_MONDAY")).CopyToDataTable<DataRow>(dtEnd2, LoadOption.PreserveChanges);
                            bool bl = false;
                            bl = Utility.IsTwoOrThirdPatient(dtEnd);
                            if (!bl)
                            {
                                dtEnd2.Rows[0].Delete();
                                dtEnd2.Rows[dtEnd.Rows.Count - 1].Delete();
                                dtEnd2.AcceptChanges();
                                bl = Utility.IsTwoOrThirdPatient(dtEnd2);
                            }
                            if (bl)
                            {
                                dtCount.Rows[0]["TWOTHIRD_TIME"] = Utility.CInt(dtCount.Rows[0]["TWOTHIRD_TIME"].ToString()) + 1;
                            }
                        }
                    }
                }

                if (dtCount != null && dtCount.Rows.Count > 0)
                {
                    dtRegularResult = dtCount.Clone();
                    dtCount.AsEnumerable().OrderBy(row => row["CURE_MONTH"].ToString()).CopyToDataTable(dtRegularResult, LoadOption.OverwriteChanges);
                    var r = dtRegularResult.NewRow();
                    r["CURE_MONTH"] = "合计";
                    r["SUB_COUNT"] = dtPatient.Rows.Count;
                    r["TWO_TIME"] = dtRegularResult.Compute("Sum(TWO_TIME)", string.Empty);
                    r["THREE_TIME"] = dtRegularResult.Compute("Sum(THREE_TIME)", string.Empty);
                    r["FOUR_TIME"] = dtRegularResult.Compute("Sum(FOUR_TIME)", string.Empty);
                    r["FIVE_TIME"] = dtRegularResult.Compute("Sum(FIVE_TIME)", string.Empty);
                    r["TWOTHIRD_TIME"] = dtRegularResult.Compute("Sum(TWOTHIRD_TIME)", string.Empty);
                    r["UNREGULAR"] = Utility.CInt(r["SUB_COUNT"].ToString()) - Utility.CInt(r["TWO_TIME"].ToString()) - Utility.CInt(r["THREE_TIME"].ToString()) - Utility.CInt(r["FOUR_TIME"].ToString()) - Utility.CInt(r["FIVE_TIME"].ToString()) - Utility.CInt(r["TWOTHIRD_TIME"].ToString());
                    dtRegularResult.Rows.Add(r);
                }
                #endregion
            }
        }

        private void work_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DataTable dtCure = null;

            #region 数据汇总

            if (dtAllResult != null && dtAllResult.Rows.Count > 0)
            {
                dtCure = GetDataTable(dtCure);
                var rowSum = dtAllResult.AsEnumerable().FirstOrDefault(r => r["CURE_MONTH"].ToString().Equals("合计"));
                foreach (DataColumn column in dtAllResult.Columns)
                {
                    if (column.ColumnName.Equals("CURE_MONTH") || column.ColumnName.Equals("SUB_COUNT"))
                    {
                        continue;
                    }

                    var row = dtCure.NewRow();
                    string name = column.ColumnName.Substring(0, column.ColumnName.IndexOf("_"));
                    Double value = Utility.CDouble(rowSum[column.ColumnName].ToString()) * 100 / Utility.CDouble(rowSum["SUB_COUNT"].ToString());
                    row["CURE_NAME"] = "透析人次";
                    row["CURE_CONDITION"] = name.Equals("HD") ? "HD人次" : (name.Equals("HDF") ? "HDF人次" : (name.Equals("HF") ? "HF人次" : (name.Equals("HP") ? "HP人次" : (name.Equals("HDHP") ? "HD+HP人次" : "CRRT人次"))));
                    row["CURE_COUNT"] = rowSum[column.ColumnName];
                    row["CURE_RATIO"] = Math.Round(value, 1).ToString() + "%";
                    dtCure.Rows.Add(row);
                }
            }

            #endregion

            #region 导管手术例数

            if (dtVascularResult != null && dtVascularResult.Rows.Count > 0)
            {
                dtCure = GetDataTable(dtCure);
                var rowSum = dtVascularResult.AsEnumerable().FirstOrDefault(r => r["CREATE_MONTH"].ToString().Equals("合计"));
                double smls = 0;
                foreach (DataColumn column in dtVascularResult.Columns)
                {
                    if (column.ColumnName.Equals("CREATE_MONTH") || column.ColumnName.Equals("SUB_COUNT"))
                    {
                        continue;
                    }

                    string name = column.ColumnName.Substring(0, column.ColumnName.IndexOf("_"));
                    if (name.Equals("SJM") || name.Equals("NL") || name.Equals("RZXG") || name.Equals("YJDG"))
                    {
                        var row = dtCure.NewRow();
                        Double value = Utility.CDouble(rowSum[column.ColumnName].ToString()) * 100 / Utility.CDouble(dtPatient.Rows.Count.ToString());
                        row["CURE_NAME"] = "血管通路";
                        row["CURE_CONDITION"] = name.Equals("SJM") ? "双静脉例数" : name.Equals("NL") ? "内瘘例数" : name.Equals("RZXG") ? "移植物内瘘" : name.Equals("YJDG") ? "带cuff中心静脉留置导管" : "其它";
                        row["CURE_COUNT"] = rowSum[column.ColumnName];
                        row["CURE_RATIO"] = Math.Round(value, 1).ToString() + "%";
                        dtCure.Rows.Add(row);
                        smls = smls + Utility.CDouble(rowSum[column.ColumnName].ToString());
                    }
                }

                var rowOther = dtCure.NewRow();
                var CureCount = Utility.CDouble(dtPatient.Rows.Count.ToString()) - smls;// Utility.CDouble(rowSum["SUB_COUNT"].ToString());
                Double valueOter = CureCount * 100 / Utility.CDouble(dtPatient.Rows.Count.ToString());
                rowOther["CURE_NAME"] = "血管通路";
                rowOther["CURE_CONDITION"] = "其他通路例数";
                rowOther["CURE_COUNT"] = CureCount;
                rowOther["CURE_RATIO"] = Math.Round(valueOter, 1).ToString() + "%";
                dtCure.Rows.Add(rowOther);
            }

            #endregion

            #region 透析男女比例

            if (dtSexResult != null && dtSexResult.Rows.Count > 0)
            {
                dtCure = GetDataTable(dtCure);
                var rowSum = dtSexResult.AsEnumerable().FirstOrDefault(r => r["CREATE_MONTH"].ToString().Equals("合计"));
                foreach (DataColumn column in dtSexResult.Columns)
                {
                    if (column.ColumnName.Equals("CREATE_MONTH") || column.ColumnName.Equals("SUB_COUNT"))
                    {
                        continue;
                    }

                    var row = dtCure.NewRow();
                    string name = column.ColumnName.Substring(0, column.ColumnName.IndexOf("_"));
                    Double value = Utility.CDouble(rowSum[column.ColumnName].ToString()) * 100 / Utility.CDouble(rowSum["SUB_COUNT"].ToString());
                    row["CURE_NAME"] = "透析男女比例";
                    row["CURE_CONDITION"] = name.Equals("MAN") ? "男性人数" : "女性人数";
                    row["CURE_COUNT"] = rowSum[column.ColumnName];
                    row["CURE_RATIO"] = Math.Round(value, 1).ToString() + "%";
                    dtCure.Rows.Add(row);
                }
            }

            #endregion

            #region 透析年龄段

            if (dtAgeResult != null && dtAgeResult.Rows.Count > 0)
            {
                dtCure = GetDataTable(dtCure);
                var rowSum = dtAgeResult.AsEnumerable().FirstOrDefault(r => r["CREATE_MONTH"].ToString().Equals("合计"));
                foreach (DataColumn column in dtAgeResult.Columns)
                {
                    if (column.ColumnName.Equals("CREATE_MONTH") || column.ColumnName.Equals("SUB_COUNT"))
                    {
                        continue;
                    }

                    var row = dtCure.NewRow();
                    string name = column.ColumnName.Substring(column.ColumnName.IndexOf("_") + 1);
                    Double value = Utility.CDouble(rowSum[column.ColumnName].ToString()) * 100 / Utility.CDouble(rowSum["SUB_COUNT"].ToString());
                    row["CURE_NAME"] = "透析年龄段";
                    row["CURE_CONDITION"] = name.Equals("1_20") ? "20岁以下" : (name.Equals("20_40") ? "20-40岁" : (name.Equals("40_60") ? "41-60岁" : "60岁以上"));
                    row["CURE_COUNT"] = rowSum[column.ColumnName];
                    row["CURE_RATIO"] = Math.Round(value, 1).ToString() + "%";
                    dtCure.Rows.Add(row);
                }
            }

            #endregion

            #region 规律透析比例

            if (dtRegularResult != null && dtRegularResult.Rows.Count > 0)
            {
                dtCure = GetDataTable(dtCure);
                var rowSum = dtRegularResult.AsEnumerable().FirstOrDefault(r => r["CURE_MONTH"].ToString().Equals("合计"));
                foreach (DataColumn column in dtRegularResult.Columns)
                {
                    if (column.ColumnName.Equals("CURE_MONTH") || column.ColumnName.Equals("SUB_COUNT"))
                    {
                        continue;
                    }

                    var row = dtCure.NewRow();
                    string name = column.ColumnName.Equals("UNREGULAR") ? "UNREGULAR" : column.ColumnName.Substring(0, column.ColumnName.IndexOf("_"));
                    Double value = Utility.CDouble(rowSum[column.ColumnName].ToString()) * 100 / Utility.CDouble(rowSum["SUB_COUNT"].ToString());
                    row["CURE_NAME"] = "规律透析比例";
                    row["CURE_CONDITION"] = name.Equals("TWOTHIRD") ? "2周5次" : name.Equals("TWO") ? "每周2次" : (name.Equals("THREE") ? "每周3次" : (name.Equals("FOUR") ? "每周4次" : (name.Equals("FIVE") ? "每周5次" : "无规律")));
                    row["CURE_COUNT"] = rowSum[column.ColumnName];
                    row["CURE_RATIO"] = Math.Round(value, 1).ToString() + "%";
                    dtCure.Rows.Add(row);
                }
            }

            #endregion

            #region 原发病透析患者
            if (dtPatientProtopathy != null && dtPatientProtopathy.Rows.Count >= 0)
            {
                dtCure = GetDataTable(dtCure);

                foreach (DataColumn column in dtPatientProtopathy.Columns)
                {
                    Double valueOter = Utility.CDouble(dtPatientProtopathy.Rows[0][column.ColumnName].ToString()) * 100 / Utility.CDouble(dtPatient.Rows.Count.ToString());
                    string name = column.ColumnName.ToString();
                    var rowTemp = dtCure.NewRow();
                    rowTemp["CURE_NAME"] = "原发病统计";
                    rowTemp["CURE_CONDITION"] =name.Equals("BYBX") ? "病因不详": name.Equals("CGN") ? "慢性肾小球肾炎" : name.Equals("DN") ? "糖尿病肾病" : name.Equals("PCKD") ? "多囊肾" : name.Equals("HTN") ? "高血压肾病 " : name.Equals("UUO") ? "梗阻性肾病" : name.Equals("TFSB") ? "痛风性肾病 " : "其它原发病";
                    rowTemp["CURE_COUNT"] = string.IsNullOrEmpty(dtPatientProtopathy.Rows[0][column.ColumnName].ToString()) ? 0 : Utility.CInt(dtPatientProtopathy.Rows[0][column.ColumnName].ToString());
                    rowTemp["CURE_RATIO"] = Math.Round(valueOter, 1).ToString() + "%"; ;
                    dtCure.Rows.Add(rowTemp);
                }
            }
            #endregion

            #region 其它
            if (dtPatient != null && dtPatient.Rows.Count >= 0)
            {
                dtCure = GetDataTable(dtCure);

                var rowTemp = dtCure.NewRow();
                rowTemp["CURE_NAME"] = "维持性透析人数";
                rowTemp["CURE_CONDITION"] = string.Empty;
                rowTemp["CURE_COUNT"] = dtPatient.Rows.Count;
                rowTemp["CURE_RATIO"] = string.Empty;
                dtCure.Rows.Add(rowTemp);
            }
            if (dtAllResult != null && dtAllResult.Rows.Count >= 0)
            {
                dtCure = GetDataTable(dtCure);

                decimal AllCount = 0;

                foreach (DataRow item in dtAllResult.Rows)
                {
                    AllCount = Utility.CDecimal(item["HD_COUNT"].ToString()) + Utility.CDecimal(item["HDF_COUNT"].ToString()) + Utility.CDecimal(item["HF_COUNT"].ToString()) + Utility.CDecimal(item["HP_COUNT"].ToString()) + Utility.CDecimal(item["HDHP_COUNT"].ToString()) + Utility.CDecimal(item["CRRT_COUNT"].ToString());
                }
                var rowTemp = dtCure.NewRow();
                rowTemp["CURE_NAME"] = "年度总透析例次";
                rowTemp["CURE_CONDITION"] = string.Empty;
                rowTemp["CURE_COUNT"] = AllCount.ToString();
                rowTemp["CURE_RATIO"] = string.Empty;
                dtCure.Rows.Add(rowTemp);
            }
            if (dtPatientWhere != null && dtPatientWhere.Rows.Count >= 0 && dtPatient != null && dtPatient.Rows.Count >= 0)
            {
                dtCure = GetDataTable(dtCure);

                decimal AllCount = 0;
                var rowTemp = dtCure.NewRow();
                rowTemp["CURE_NAME"] = "年死亡病人数，占维持性透析病人比例";
                rowTemp["CURE_CONDITION"] = string.Empty;
                rowTemp["CURE_COUNT"] = dtPatientWhere.Rows.Count.ToString();
                rowTemp["CURE_RATIO"] = dtPatient.Rows.Count == 0 ? "0%" : Math.Round(Utility.CDecimal(dtPatientWhere.Rows.Count.ToString()) / dtPatient.Rows.Count * 100, 1) + "%";
                dtCure.Rows.Add(rowTemp);
            }

            if (dtPatientOperator != null && dtPatientOperator.Rows.Count >= 0)
            {
                dtCure = GetDataTable(dtCure);

                var djmlns = dtPatientOperator.Where(i => i.OPE_NAME == "动静脉内瘘术" && i.VASCULARV_TYPE.Contains("临时"));
                var dgsjmzrs = dtPatientOperator.Where(i => i.OPE_NAME == "导管深静脉置入术" && i.VASCULARV_TYPE.Contains("半永久"));

                var rowTemp = dtCure.NewRow();
                rowTemp["CURE_NAME"] = "手术";
                rowTemp["CURE_CONDITION"] = "临时动静脉内瘘术";
                rowTemp["CURE_COUNT"] = djmlns.Count();
                rowTemp["CURE_RATIO"] = dtPatientOperator.Rows.Count == 0 ? "0%" : Math.Round(Utility.CDecimal(djmlns.Count().ToString()) / dtPatientOperator.Rows.Count * 100, 1) + "%";
                dtCure.Rows.Add(rowTemp);

                var rowTemp1 = dtCure.NewRow();
                rowTemp1["CURE_NAME"] = "手术";
                rowTemp1["CURE_CONDITION"] = "半永久导管深静脉置入术";
                rowTemp1["CURE_COUNT"] = dgsjmzrs.Count();
                rowTemp1["CURE_RATIO"] = dtPatientOperator.Rows.Count == 0 ? "0%" : Math.Round(Utility.CDecimal(dgsjmzrs.Count().ToString()) / dtPatientOperator.Rows.Count * 100, 1) + "%";
                dtCure.Rows.Add(rowTemp1);
 
            }

            #endregion
            this.gcCure.DataSource = dtCure;
            dtMonitorCure = dtCure;
            this.busyIndicator.HideLoadingScreen();
        }

        /// <summary>
        /// 返回数据集
        /// </summary>
        /// <param name="dtCure"></param>
        /// <returns></returns>
        private DataTable GetDataTable(DataTable dtCure)
        {
            if (dtCure == null)
            {
                dtCure = new DataTable();
                dtCure.Columns.Add("CURE_NAME", typeof(System.String));
                dtCure.Columns.Add("CURE_CONDITION", typeof(System.String));
                dtCure.Columns.Add("CURE_COUNT", typeof(System.Int32));
                dtCure.Columns.Add("CURE_RATIO", typeof(System.String));
            }
            return dtCure;
        }

        /// <summary>
        /// 获取治疗结果
        /// </summary>
        /// <returns></returns>
        public DataTable GetCureResult()
        {
            return this.gcCure.DataSource as DataTable;
        }

        #endregion
    }
}
