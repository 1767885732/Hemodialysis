/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：患者排班记录报表
// 创建时间：2016-05-16
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Hemo.IService.PatientSchedule;
using Hemo.Service;
using Hemo.Model;

namespace Hemo.Client.Print
{
    public partial class PatientScheduleReportList : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        public PatientScheduleReportList(DateTime reportDate)
        {
            InitializeComponent();

            #region GetAllDatas

            this.ShowPrintMarginsWarning = false;

            IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;
            PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable patientScheduleDataTable = _patientScheduleService.GetPatientScheduleList4Report(reportDate, reportDate);

            #endregion

            #region 根据获取的数据进行数据表格填充

            #region 早班

            var groupList = from r in patientScheduleDataTable.AsEnumerable()
                            where r.BANCI_ID == "1"
                            group r by new
                            {
                                r.DIALYSIS_ROOM_ID,
                                r.BED_NUMBER,
                                r.BANCI_ID
                            } into g
                            select g.Key;

            foreach (var item in groupList)
            {
                PatientScheduleModel.MED_PATIENT_SCHEDULERow[] patientScheduleRows = patientScheduleDataTable.Select(string.Format("DIALYSIS_ROOM_ID = '{0}' AND BED_NUMBER = '{1}' AND BANCI_ID='{2}'", item.DIALYSIS_ROOM_ID, item.BED_NUMBER, item.BANCI_ID)) as PatientScheduleModel.MED_PATIENT_SCHEDULERow[];
                if (patientScheduleRows.Length <= 0)
                    continue;
                for (int i = 0; i < patientScheduleRows.Length; i++)
                {
                    switch (patientScheduleRows[0].AREANAME.ToString())
                    {
                        case "透析室A区":
                            int BedNumOneRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedNumOneRoom % 2 == 0)
                            {
                                this.xrTable1.Rows[(BedNumOneRoom - 2) / 2].Cells[1].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable1.Rows[(BedNumOneRoom - 1) / 2].Cells[0].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室B区":
                            int BedNumTwoRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);

                            if (BedNumTwoRoom % 2 == 0)
                            {
                                this.xrTable1.Rows[(BedNumTwoRoom - 2) / 2].Cells[3].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable1.Rows[(BedNumTwoRoom - 1) / 2].Cells[2].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室C区":
                            int BedNumThreeRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedNumThreeRoom % 2 == 0)
                            {
                                this.xrTable1.Rows[(BedNumThreeRoom - 2) / 2].Cells[5].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable1.Rows[(BedNumThreeRoom - 1) / 2].Cells[4].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室D区":
                            int BedNumFourRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedNumFourRoom % 2 == 0)
                            {
                                this.xrTable1.Rows[(BedNumFourRoom - 2) / 2].Cells[7].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable1.Rows[(BedNumFourRoom - 1) / 2].Cells[6].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室E区":
                            int BedFiveRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedFiveRoom % 2 == 0)
                            {
                                this.xrTable1.Rows[(BedFiveRoom - 2) / 2].Cells[9].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable1.Rows[(BedFiveRoom - 1) / 2].Cells[8].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室F区":
                            int BedSixRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedSixRoom % 2 == 0)
                            {
                                this.xrTable1.Rows[(BedSixRoom - 2) / 2].Cells[11].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable1.Rows[(BedSixRoom - 1) / 2].Cells[10].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室G区":
                            int BedSevenRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedSevenRoom % 2 == 0)
                            {
                                this.xrTable1.Rows[(BedSevenRoom - 2) / 2].Cells[13].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable1.Rows[(BedSevenRoom - 1) / 2].Cells[12].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "CRRT":
                            int BedNumCRRTRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            this.xrTable1.Rows[BedNumCRRTRoom - 1].Cells[14].Text = patientScheduleRows[i].PATIENTNAME;
                            break;
                        default:
                            break;
                    }
                }
            }

            #endregion

            #region 中班

            var groupList1 = from r in patientScheduleDataTable.AsEnumerable()
                             where r.BANCI_ID == "2"
                             group r by new
                             {
                                 r.DIALYSIS_ROOM_ID,
                                 r.BED_NUMBER,
                                 r.BANCI_ID
                             } into g
                             select g.Key;

            foreach (var item in groupList1)
            {
                PatientScheduleModel.MED_PATIENT_SCHEDULERow[] patientScheduleRows = patientScheduleDataTable.Select(string.Format("DIALYSIS_ROOM_ID = '{0}' AND BED_NUMBER = '{1}' AND BANCI_ID='{2}'", item.DIALYSIS_ROOM_ID, item.BED_NUMBER, item.BANCI_ID)) as PatientScheduleModel.MED_PATIENT_SCHEDULERow[];
                if (patientScheduleRows.Length <= 0)
                    continue;
                for (int i = 0; i < patientScheduleRows.Length; i++)
                {
                    switch (patientScheduleRows[0].AREANAME.ToString())
                    {
                        case "透析室A区":
                            int BedNumOneRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedNumOneRoom % 2 == 0)
                            {
                                this.xrTable2.Rows[(BedNumOneRoom - 2) / 2].Cells[1].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable2.Rows[(BedNumOneRoom - 1) / 2].Cells[0].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室B区":
                            int BedNumTwoRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);

                            if (BedNumTwoRoom % 2 == 0)
                            {
                                this.xrTable2.Rows[(BedNumTwoRoom - 2) / 2].Cells[3].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable2.Rows[(BedNumTwoRoom - 1) / 2].Cells[2].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室C区":
                            int BedNumThreeRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedNumThreeRoom % 2 == 0)
                            {
                                this.xrTable2.Rows[(BedNumThreeRoom - 2) / 2].Cells[5].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable2.Rows[(BedNumThreeRoom - 1) / 2].Cells[4].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室D区":
                            int BedNumFourRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedNumFourRoom % 2 == 0)
                            {
                                this.xrTable2.Rows[(BedNumFourRoom - 2) / 2].Cells[7].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable2.Rows[(BedNumFourRoom - 1) / 2].Cells[6].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室E区":
                            int BedFiveRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedFiveRoom % 2 == 0)
                            {
                                this.xrTable2.Rows[(BedFiveRoom - 2) / 2].Cells[9].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable2.Rows[(BedFiveRoom - 1) / 2].Cells[8].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室F区":
                            int BedSixRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedSixRoom % 2 == 0)
                            {
                                this.xrTable2.Rows[(BedSixRoom - 2) / 2].Cells[11].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable2.Rows[(BedSixRoom - 1) / 2].Cells[10].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室G区":
                            int BedSevenRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedSevenRoom % 2 == 0)
                            {
                                this.xrTable2.Rows[(BedSevenRoom - 2) / 2].Cells[13].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable2.Rows[(BedSevenRoom - 1) / 2].Cells[12].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "CRRT":
                            int BedNumCRRTRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            this.xrTable2.Rows[BedNumCRRTRoom - 1].Cells[14].Text = patientScheduleRows[i].PATIENTNAME;
                            break;
                        default:
                            break;
                    }
                }
            }

            #endregion

            #region 晚班

            var groupList2 = from r in patientScheduleDataTable.AsEnumerable()
                             where r.BANCI_ID == "3"
                             group r by new
                             {
                                 r.DIALYSIS_ROOM_ID,
                                 r.BED_NUMBER,
                                 r.BANCI_ID
                             } into g
                             select g.Key;

            foreach (var item in groupList2)
            {
                PatientScheduleModel.MED_PATIENT_SCHEDULERow[] patientScheduleRows = patientScheduleDataTable.Select(string.Format("DIALYSIS_ROOM_ID = '{0}' AND BED_NUMBER = '{1}' AND BANCI_ID='{2}'", item.DIALYSIS_ROOM_ID, item.BED_NUMBER, item.BANCI_ID)) as PatientScheduleModel.MED_PATIENT_SCHEDULERow[];
                if (patientScheduleRows.Length <= 0)
                    continue;
                for (int i = 0; i < patientScheduleRows.Length; i++)
                {
                    switch (patientScheduleRows[0].AREANAME.ToString())
                    {
                        case "透析室A区":
                            int BedNumOneRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedNumOneRoom % 2 == 0)
                            {
                                this.xrTable3.Rows[(BedNumOneRoom - 2) / 2].Cells[1].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable3.Rows[(BedNumOneRoom - 1) / 2].Cells[0].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室B区":
                            int BedNumTwoRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);

                            if (BedNumTwoRoom % 2 == 0)
                            {
                                this.xrTable3.Rows[(BedNumTwoRoom - 2) / 2].Cells[3].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable3.Rows[(BedNumTwoRoom - 1) / 2].Cells[2].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室C区":
                            int BedNumThreeRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedNumThreeRoom % 2 == 0)
                            {
                                this.xrTable3.Rows[(BedNumThreeRoom - 2) / 2].Cells[5].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable3.Rows[(BedNumThreeRoom - 1) / 2].Cells[4].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室D区":
                            int BedNumFourRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedNumFourRoom % 2 == 0)
                            {
                                this.xrTable3.Rows[(BedNumFourRoom - 2) / 2].Cells[7].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable3.Rows[(BedNumFourRoom - 1) / 2].Cells[6].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室E区":
                            int BedFiveRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedFiveRoom % 2 == 0)
                            {
                                this.xrTable3.Rows[(BedFiveRoom - 2) / 2].Cells[9].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable3.Rows[(BedFiveRoom - 1) / 2].Cells[8].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室F区":
                            int BedSixRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedSixRoom % 2 == 0)
                            {
                                this.xrTable3.Rows[(BedSixRoom - 2) / 2].Cells[11].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable3.Rows[(BedSixRoom - 1) / 2].Cells[10].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室G区":
                            int BedSevenRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedSevenRoom % 2 == 0)
                            {
                                this.xrTable3.Rows[(BedSevenRoom - 2) / 2].Cells[13].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable3.Rows[(BedSevenRoom - 1) / 2].Cells[12].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "CRRT":
                            int BedNumCRRTRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            this.xrTable3.Rows[BedNumCRRTRoom - 1].Cells[14].Text = patientScheduleRows[i].PATIENTNAME;
                            break;
                        default:
                            break;
                    }
                }
            }

            #endregion

            #region 急诊

            var groupList3 = from r in patientScheduleDataTable.AsEnumerable()
                             where r.BANCI_ID == "4"
                             group r by new
                             {
                                 r.DIALYSIS_ROOM_ID,
                                 r.BED_NUMBER,
                                 r.BANCI_ID
                             } into g
                             select g.Key;

            foreach (var item in groupList3)
            {
                PatientScheduleModel.MED_PATIENT_SCHEDULERow[] patientScheduleRows = patientScheduleDataTable.Select(string.Format("DIALYSIS_ROOM_ID = '{0}' AND BED_NUMBER = '{1}' AND BANCI_ID='{2}'", item.DIALYSIS_ROOM_ID, item.BED_NUMBER, item.BANCI_ID)) as PatientScheduleModel.MED_PATIENT_SCHEDULERow[];
                if (patientScheduleRows.Length <= 0)
                    continue;
                for (int i = 0; i < patientScheduleRows.Length; i++)
                {
                    switch (patientScheduleRows[0].AREANAME.ToString())
                    {
                        case "透析室A区":
                            int BedNumOneRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedNumOneRoom % 2 == 0)
                            {
                                this.xrTable5.Rows[(BedNumOneRoom - 2) / 2].Cells[1].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable5.Rows[(BedNumOneRoom - 1) / 2].Cells[0].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室B区":
                            int BedNumTwoRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);

                            if (BedNumTwoRoom % 2 == 0)
                            {
                                this.xrTable5.Rows[(BedNumTwoRoom - 2) / 2].Cells[3].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable5.Rows[(BedNumTwoRoom - 1) / 2].Cells[2].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室C区":
                            int BedNumThreeRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedNumThreeRoom % 2 == 0)
                            {
                                this.xrTable5.Rows[(BedNumThreeRoom - 2) / 2].Cells[5].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable5.Rows[(BedNumThreeRoom - 1) / 2].Cells[4].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室D区":
                            int BedNumFourRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedNumFourRoom % 2 == 0)
                            {
                                this.xrTable5.Rows[(BedNumFourRoom - 2) / 2].Cells[7].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable5.Rows[(BedNumFourRoom - 1) / 2].Cells[6].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室E区":
                            int BedFiveRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedFiveRoom % 2 == 0)
                            {
                                this.xrTable5.Rows[(BedFiveRoom - 2) / 2].Cells[9].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable5.Rows[(BedFiveRoom - 1) / 2].Cells[8].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室F区":
                            int BedSixRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedSixRoom % 2 == 0)
                            {
                                this.xrTable5.Rows[(BedSixRoom - 2) / 2].Cells[11].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable5.Rows[(BedSixRoom - 1) / 2].Cells[10].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "透析室G区":
                            int BedSevenRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            if (BedSevenRoom % 2 == 0)
                            {
                                this.xrTable5.Rows[(BedSevenRoom - 2) / 2].Cells[13].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            else
                            {
                                this.xrTable5.Rows[(BedSevenRoom - 1) / 2].Cells[12].Text = patientScheduleRows[i].PATIENTNAME;
                            }
                            break;
                        case "CRRT":
                            int BedNumCRRTRoom = Convert.ToInt32(patientScheduleRows[i].BEDNAME);
                            this.xrTable5.Rows[BedNumCRRTRoom - 1].Cells[14].Text = patientScheduleRows[i].PATIENTNAME;
                            break;
                        default:
                            break;
                    }
                }
            }

            #endregion

            #endregion

            #region 日期、星期显示

            this.labDate.Text = reportDate.ToString("yyyy年MM月dd日");

            switch (reportDate.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    this.labWeek.Text = "星期五";
                    break;

                case DayOfWeek.Monday:
                    this.labWeek.Text = "星期一";
                    break;

                case DayOfWeek.Saturday:
                    this.labWeek.Text = "星期六";
                    break;

                case DayOfWeek.Sunday:
                    this.labWeek.Text = "星期日";
                    break;

                case DayOfWeek.Thursday:
                    this.labWeek.Text = "星期四";
                    break;

                case DayOfWeek.Tuesday:
                    this.labWeek.Text = "星期二";
                    break;

                case DayOfWeek.Wednesday:
                    this.labWeek.Text = "星期三";
                    break;

                default:
                    break;
            }

            #endregion
        }

        #endregion
    }
}
