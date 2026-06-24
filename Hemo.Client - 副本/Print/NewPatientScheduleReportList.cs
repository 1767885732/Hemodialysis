/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:大屏排班报表
 * 创建标识:吕志强-2016年6月18日
 * ----------------------------------------------------------------*/

using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Hemo.IService.PatientSchedule;
using Hemo.Service;
using Hemo.Model;
using Hemo.IService.Config;
using System.Data;
using Hemo.Utilities;

namespace Hemo.Client.Print
{
    public partial class NewPatientScheduleReportList : XtraReport
    {
        #region 成员变量

        private IConfig configService = ServiceManager.Instance.ConfigService;

        IPatientSchedule patientScheduleService = ServiceManager.Instance.PatientSchedule;

        #endregion

        #region 构造函数

        public NewPatientScheduleReportList(DateTime beginDate, DateTime endDate)
        {
            InitializeComponent();
            xrLabel8.Text = Utilities.Utility.GetHospitalName() + "血透患者排班表";
            this.Detail.Controls.Clear();
            LoadReportHead(beginDate);
            CreateTable();
            LoadReportDetail(beginDate, endDate);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载报表头部
        /// </summary>
        /// <param name="beginDate"></param>
        private void LoadReportHead(DateTime beginDate)
        {
            this.labDate.Text = beginDate.ToShortDateString() + "至" + beginDate.AddDays(5).ToShortDateString();

            this.lblDay1.Text = "[" + GetDayOfWeek((int)beginDate.DayOfWeek) + "]" + "(" + beginDate.ToShortDateString() + ")";
            this.lblDay2.Text = "[" + GetDayOfWeek((int)beginDate.AddDays(1).DayOfWeek) + "]" + "(" + beginDate.AddDays(1).ToShortDateString() + ")";
            this.lblDay3.Text = "[" + GetDayOfWeek((int)beginDate.AddDays(2).DayOfWeek) + "]" + "(" + beginDate.AddDays(2).ToShortDateString() + ")";
            this.lblDay4.Text = "[" + GetDayOfWeek((int)beginDate.AddDays(3).DayOfWeek) + "]" + "(" + beginDate.AddDays(3).ToShortDateString() + ")";
            this.lblDay5.Text = "[" + GetDayOfWeek((int)beginDate.AddDays(4).DayOfWeek) + "]" + "(" + beginDate.AddDays(4).ToShortDateString() + ")";
            this.lblDay6.Text = "[" + GetDayOfWeek((int)beginDate.AddDays(5).DayOfWeek) + "]" + "(" + beginDate.AddDays(5).ToShortDateString() + ")";
        }

        /// <summary>
        /// 加载报表明细
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        private void LoadReportDetail(DateTime beginDate, DateTime endDate)
        {
            //病区
            ConfigModel.MED_COMMON_ITEMLISTDataTable dtConfig = configService.GetConfigList(string.Empty, string.Empty, "区域", "1");
            if (dtConfig != null && dtConfig.Rows.Count > 0)
            {
                //float x = 0F;
                //float y = 0F;

                DataTable dtPatientSchedule = patientScheduleService.GetPatientScheduleList4Report(beginDate, endDate);
                DataTable dtPatient = new DataTable("SchedulePatient");

                dtPatient.Columns.Add(new DataColumn("Room_Name"));

                dtPatient.Columns.Add(new DataColumn("AM1_Name"));
                dtPatient.Columns.Add(new DataColumn("PM1_Name"));
                dtPatient.Columns.Add(new DataColumn("AM2_Name"));
                dtPatient.Columns.Add(new DataColumn("PM2_Name"));
                dtPatient.Columns.Add(new DataColumn("AM3_Name"));
                dtPatient.Columns.Add(new DataColumn("PM3_Name"));
                dtPatient.Columns.Add(new DataColumn("AM4_Name"));
                dtPatient.Columns.Add(new DataColumn("PM4_Name"));
                dtPatient.Columns.Add(new DataColumn("AM5_Name"));
                dtPatient.Columns.Add(new DataColumn("PM5_Name"));
                dtPatient.Columns.Add(new DataColumn("AM6_Name"));
                dtPatient.Columns.Add(new DataColumn("PM6_Name"));

                foreach (ConfigModel.MED_COMMON_ITEMLISTRow config in dtConfig.Rows)
                {
                    var rowsAM1 = dtPatientSchedule.Select("DIALYSIS_ROOM_ID='" + config.ITEM_ID + "' and BANCI_ID='" + "1" + "' and DIALYSIS_DATE='" + beginDate.ToShortDateString() + "'");

                    var rowsAM2 = dtPatientSchedule.Select("DIALYSIS_ROOM_ID='" + config.ITEM_ID + "' and BANCI_ID='" + "1" + "' and DIALYSIS_DATE='" + beginDate.AddDays(1).ToShortDateString() + "'");

                    var rowsAM3 = dtPatientSchedule.Select("DIALYSIS_ROOM_ID='" + config.ITEM_ID + "' and BANCI_ID='" + "1" + "' and DIALYSIS_DATE='" + beginDate.AddDays(2).ToShortDateString() + "'");

                    var rowsAM4 = dtPatientSchedule.Select("DIALYSIS_ROOM_ID='" + config.ITEM_ID + "' and BANCI_ID='" + "1" + "' and DIALYSIS_DATE='" + beginDate.AddDays(3).ToShortDateString() + "'");

                    var rowsAM5 = dtPatientSchedule.Select("DIALYSIS_ROOM_ID='" + config.ITEM_ID + "' and BANCI_ID='" + "1" + "' and DIALYSIS_DATE='" + beginDate.AddDays(4).ToShortDateString() + "'");

                    var rowsAM6 = dtPatientSchedule.Select("DIALYSIS_ROOM_ID='" + config.ITEM_ID + "' and BANCI_ID='" + "1" + "' and DIALYSIS_DATE='" + beginDate.AddDays(5).ToShortDateString() + "'");

                    var rowsPM1 = dtPatientSchedule.Select("DIALYSIS_ROOM_ID='" + config.ITEM_ID + "' and BANCI_ID='" + "2" + "' and DIALYSIS_DATE='" + beginDate.ToShortDateString() + "'");

                    var rowsPM2 = dtPatientSchedule.Select("DIALYSIS_ROOM_ID='" + config.ITEM_ID + "' and BANCI_ID='" + "2" + "' and DIALYSIS_DATE='" + beginDate.AddDays(1).ToShortDateString() + "'");

                    var rowsPM3 = dtPatientSchedule.Select("DIALYSIS_ROOM_ID='" + config.ITEM_ID + "' and BANCI_ID='" + "2" + "' and DIALYSIS_DATE='" + beginDate.AddDays(2).ToShortDateString() + "'");

                    var rowsPM4 = dtPatientSchedule.Select("DIALYSIS_ROOM_ID='" + config.ITEM_ID + "' and BANCI_ID='" + "2" + "' and DIALYSIS_DATE='" + beginDate.AddDays(3).ToShortDateString() + "'");

                    var rowsPM5 = dtPatientSchedule.Select("DIALYSIS_ROOM_ID='" + config.ITEM_ID + "' and BANCI_ID='" + "2" + "' and DIALYSIS_DATE='" + beginDate.AddDays(4).ToShortDateString() + "'");

                    var rowsPM6 = dtPatientSchedule.Select("DIALYSIS_ROOM_ID='" + config.ITEM_ID + "' and BANCI_ID='" + "2" + "' and DIALYSIS_DATE='" + beginDate.AddDays(5).ToShortDateString() + "'");

                    int rowNum = 0;
                    rowNum = rowNum < rowsAM1.Length ? rowsAM1.Length : rowNum;
                    rowNum = rowNum < rowsAM2.Length ? rowsAM2.Length : rowNum;
                    rowNum = rowNum < rowsAM3.Length ? rowsAM3.Length : rowNum;
                    rowNum = rowNum < rowsAM4.Length ? rowsAM4.Length : rowNum;
                    rowNum = rowNum < rowsAM5.Length ? rowsAM5.Length : rowNum;
                    rowNum = rowNum < rowsAM6.Length ? rowsAM6.Length : rowNum;

                    rowNum = rowNum < rowsPM1.Length ? rowsPM1.Length : rowNum;
                    rowNum = rowNum < rowsPM2.Length ? rowsPM2.Length : rowNum;
                    rowNum = rowNum < rowsPM3.Length ? rowsPM3.Length : rowNum;
                    rowNum = rowNum < rowsPM4.Length ? rowsPM4.Length : rowNum;
                    rowNum = rowNum < rowsPM5.Length ? rowsPM5.Length : rowNum;
                    rowNum = rowNum < rowsPM6.Length ? rowsPM6.Length : rowNum;

                    for (int j = 0; j < rowNum; j++)
                    {
                        var r = dtPatient.NewRow();

                        r["Room_Name"] = config.ITEM_NAME;

                        if (j < rowsAM1.Length)
                        {
                            r["AM1_Name"] = rowsAM1[j]["PATIENTNAME"];
                        }
                        if (j < rowsAM2.Length)
                        {
                            r["AM2_Name"] = rowsAM2[j]["PATIENTNAME"];
                        }
                        if (j < rowsAM3.Length)
                        {
                            r["AM3_Name"] = rowsAM3[j]["PATIENTNAME"];
                        }
                        if (j < rowsAM4.Length)
                        {
                            r["AM4_Name"] = rowsAM4[j]["PATIENTNAME"];
                        }
                        if (j < rowsAM5.Length)
                        {
                            r["AM5_Name"] = rowsAM5[j]["PATIENTNAME"];
                        }
                        if (j < rowsAM6.Length)
                        {
                            r["AM6_Name"] = rowsAM6[j]["PATIENTNAME"];
                        }

                        if (j < rowsPM1.Length)
                        {
                            r["PM1_Name"] = rowsPM1[j]["PATIENTNAME"];
                        }
                        if (j < rowsPM2.Length)
                        {
                            r["PM2_Name"] = rowsPM2[j]["PATIENTNAME"];
                        }
                        if (j < rowsPM3.Length)
                        {
                            r["PM3_Name"] = rowsPM3[j]["PATIENTNAME"];
                        }
                        if (j < rowsPM4.Length)
                        {
                            r["PM4_Name"] = rowsPM4[j]["PATIENTNAME"];
                        }
                        if (j < rowsPM5.Length)
                        {
                            r["PM5_Name"] = rowsPM5[j]["PATIENTNAME"];
                        }
                        if (j < rowsPM6.Length)
                        {
                            r["PM6_Name"] = rowsPM6[j]["PATIENTNAME"];
                        }

                        dtPatient.Rows.Add(r);
                    }

                    if (rowNum == 0)
                    {
                        var r = dtPatient.NewRow();
                        r["Room_Name"] = config.ITEM_NAME;
                        dtPatient.Rows.Add(r);
                        //rowNum = 1;
                    }

                    //XRLabel label = new XRLabel();
                    //label.LocationFloat = new DevExpress.Utils.PointFloat(x, y);
                    //label.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
                    //label.Borders = (DevExpress.XtraPrinting.BorderSide)DevExpress.XtraPrinting.BorderSide.All;
                    //label.WidthF = 75F;
                    //label.HeightF = 25F * rowNum;
                    //label.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular);
                    //label.StylePriority.UseBorders = false;
                    //label.StylePriority.UseTextAlignment = false;
                    //label.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    //label.Text = config.ITEM_NAME;

                    //this.Detail.Controls.AddRange(new XRControl[] { label });
                    //y += label.HeightF;
                }

                this.DataSource = dtPatient;
            }
        }

        /// <summary>
        /// 创建表
        /// </summary>
        private void CreateTable()
        {
            XRTable table = new XRTable();
            table.Borders = (DevExpress.XtraPrinting.BorderSide)DevExpress.XtraPrinting.BorderSide.All;
            table.BorderWidth = 1;
            table.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            table.Name = "xrTable";
            table.SizeF = new SizeF(1040F, 25F);
            table.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular);
            table.StylePriority.UseBorders = false;
            table.StylePriority.UseTextAlignment = false;
            table.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            XRTableRow row = new XRTableRow();
            row.Name = "xrTableRow";
            row.Weight = 1D;

            table.Rows.AddRange(new XRTableRow[] { row });

            XRTableCell cell_Room = new XRTableCell();
            cell_Room.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell_Room.Name = "xrTableCell0";
            cell_Room.Weight = 0.75D;
            cell_Room.StylePriority.UseBorders = false;
            cell_Room.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "Room_Name") });

            XRTableCell cell_AM1 = new XRTableCell();
            cell_AM1.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell_AM1.Name = "xrTableCell1";
            cell_AM1.Weight = 0.75D;
            cell_AM1.StylePriority.UseBorders = false;
            cell_AM1.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "AM1_Name") });

            XRTableCell cell_PM1 = new XRTableCell();
            cell_PM1.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell_PM1.Name = "xrTableCell7";
            cell_PM1.Weight = 0.75D;
            cell_PM1.StylePriority.UseBorders = false;
            cell_PM1.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "PM1_Name") });

            XRTableCell cell_AM2 = new XRTableCell();
            cell_AM2.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell_AM2.Name = "xrTableCell2";
            cell_AM2.Weight = 0.75D;
            cell_AM2.StylePriority.UseBorders = false;
            cell_AM2.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "AM2_Name") });

            XRTableCell cell_PM2 = new XRTableCell();
            cell_PM2.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell_PM2.Name = "xrTableCell8";
            cell_PM2.Weight = 0.75D;
            cell_PM2.StylePriority.UseBorders = false;
            cell_PM2.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "PM2_Name") });

            XRTableCell cell_AM3 = new XRTableCell();
            cell_AM3.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell_AM3.Name = "xrTableCell3";
            cell_AM3.Weight = 0.75D;
            cell_AM3.StylePriority.UseBorders = false;
            cell_AM3.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "AM3_Name") });

            XRTableCell cell_PM3 = new XRTableCell();
            cell_PM3.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell_PM3.Name = "xrTableCell9";
            cell_PM3.Weight = 0.75D;
            cell_PM3.StylePriority.UseBorders = false;
            cell_PM3.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "PM3_Name") });

            XRTableCell cell_AM4 = new XRTableCell();
            cell_AM4.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell_AM4.Name = "xrTableCell4";
            cell_AM4.Weight = 0.75D;
            cell_AM4.StylePriority.UseBorders = false;
            cell_AM4.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "AM4_Name") });

            XRTableCell cell_PM4 = new XRTableCell();
            cell_PM4.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell_PM4.Name = "xrTableCell10";
            cell_PM4.Weight = 0.75D;
            cell_PM4.StylePriority.UseBorders = false;
            cell_PM4.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "PM4_Name") });

            XRTableCell cell_AM5 = new XRTableCell();
            cell_AM5.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell_AM5.Name = "xrTableCell5";
            cell_AM5.Weight = 0.75D;
            cell_AM5.StylePriority.UseBorders = false;
            cell_AM5.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "AM5_Name") });

            XRTableCell cell_PM5 = new XRTableCell();
            cell_PM5.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell_PM5.Name = "xrTableCell11";
            cell_PM5.Weight = 0.75D;
            cell_PM5.StylePriority.UseBorders = false;
            cell_PM5.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "PM5_Name") });

            XRTableCell cell_AM6 = new XRTableCell();
            cell_AM6.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell_AM6.Name = "xrTableCell6";
            cell_AM6.Weight = 0.75D;
            cell_AM6.StylePriority.UseBorders = false;
            cell_AM6.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "AM6_Name") });

            XRTableCell cell_PM6 = new XRTableCell();
            cell_PM6.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell_PM6.Name = "xrTableCell12";
            cell_PM6.Weight = 0.75D;
            cell_PM6.StylePriority.UseBorders = false;
            cell_PM6.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "PM6_Name") });

            row.Cells.AddRange(new XRTableCell[] { cell_Room, cell_AM1, cell_PM1, cell_AM2, cell_PM2, cell_AM3, cell_PM3, cell_AM4, cell_PM4, cell_AM5, cell_PM5, cell_AM6, cell_PM6 });
            this.Detail.Controls.AddRange(new XRControl[] { table });
        }

        /// <summary>
        /// 获取日期对应于一周内的周几
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        private string GetDayOfWeek(int day)
        {
            string result = "周一";

            switch (day)
            {
                case 1:
                    result = "周一";
                    break;
                case 2:
                    result = "周二";
                    break;
                case 3:
                    result = "周三";
                    break;
                case 4:
                    result = "周四";
                    break;
                case 5:
                    result = "周五";
                    break;
                case 6:
                    result = "周六";
                    break;
                default:
                    result = "周日";
                    break;
            }

            return result;
        }

        #endregion
    }
}
