/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:血透患者排班表报表
 * 创建标识:贺建操-2016年4月15日
 * ----------------------------------------------------------------*/

using System;
using System.Linq;
using DevExpress.XtraReports.UI;
using Hemo.IService.PatientSchedule;
using Hemo.Model;
using Hemo.Service;

namespace Hemo.Client.Print
{
    public partial class HemoScheduleReport : XtraReport
    {
        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportDate"></param>
        public HemoScheduleReport(DateTime reportDate)
        {
            this.InitializeComponent();

            xrLabel8.Text = Utilities.Utility.GetHospitalName() + "血透患者排班表";

            #region Variable

            this.ShowPrintMarginsWarning = false;

            IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;
            PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable patientScheduleDataTable = _patientScheduleService.GetPatientScheduleList4Report(reportDate, reportDate);
            PatientScheduleModel.MED_PATIENT_SCHEDULE_REPORTDataTable patientScheduleReportDataTable = this.patientScheduleModel1.MED_PATIENT_SCHEDULE_REPORT;

            #endregion

            #region Date

            int index = 1;
            var groupList = from r in patientScheduleDataTable.AsEnumerable()
                            group r by new
                            {
                                r.DIALYSIS_ROOM_ID,
                                r.BED_NUMBER
                            } into g
                            select g.Key;

            foreach (var item in groupList)
            {
                PatientScheduleModel.MED_PATIENT_SCHEDULERow[] patientScheduleRows = patientScheduleDataTable.Select(string.Format("DIALYSIS_ROOM_ID = '{0}' AND BED_NUMBER = '{1}'", item.DIALYSIS_ROOM_ID, item.BED_NUMBER)) as PatientScheduleModel.MED_PATIENT_SCHEDULERow[];
                PatientScheduleModel.MED_PATIENT_SCHEDULE_REPORTRow patientScheduleReportRow = patientScheduleReportDataTable.NewMED_PATIENT_SCHEDULE_REPORTRow();

                for (int i = 0; i < patientScheduleRows.Length; i++)
                {
                    if (i == 0)
                    {
                        patientScheduleReportRow.NUMBER = (index++).ToString();
                        patientScheduleReportRow.AREANAME = patientScheduleRows[i]["AREANAME"].ToString();
                    }

                    string temp = string.Format("{0} {1} {2}", patientScheduleRows[i]["PATIENTNAME"], patientScheduleRows[i]["MACHINE_NAME"], patientScheduleRows[i].IsREMARKNull() ? string.Empty : patientScheduleRows[i].REMARK);

                    switch (patientScheduleRows[i].BANCI_ID)
                    {
                        case "1":
                            patientScheduleReportRow.SW = temp;
                            break;

                        case "2":
                            patientScheduleReportRow.XW = temp;
                            break;

                        case "3":
                            patientScheduleReportRow.WS = temp;
                            break;

                        default:
                            break;
                    }
                }

                patientScheduleReportDataTable.AddMED_PATIENT_SCHEDULE_REPORTRow(patientScheduleReportRow);
            }

            #endregion

            #region Label

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
