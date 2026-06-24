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
using Hemo.IService.Config;
using System.Data;
using Hemo.Utilities;
using Hemo.IService.Machine;
using Hemo.Client.Core;

namespace Hemo.Client.Print
{
    public partial class PatientScheduleReportForJl : XtraReport
    {
        #region 成员变量

        private IConfig configService = ServiceManager.Instance.ConfigService;

        IPatientSchedule patientScheduleService = ServiceManager.Instance.PatientSchedule;
        private IMachine _machineService = ServiceManager.Instance.MachineService;

        ReportRelationModel.MED_PATIENT_SCHEDULEDataTable _data = new ReportRelationModel.MED_PATIENT_SCHEDULEDataTable();
        #endregion

        #region 构造函数

        public PatientScheduleReportForJl(DateTime beginDate, DateTime endDate, string banChi, DateTime date)
        {
            InitializeComponent();
            //this.lb_User.Text = string.Format("今日值班护士：{0}", patientScheduleService.GetCurrentDateNurseDuty(date));
            xrLabel8.Text = Utilities.Utility.GetHospitalName() + "血透患者排班表";
            // this.labDate.Text = "日期：" + beginDate.ToShortDateString() + "至" + beginDate.AddDays(5).ToShortDateString();
            ConfigModel.MED_COMMON_ITEMLISTDataTable _banChiDateTable = this.configService.GetConfigList(string.Empty, string.Empty, "班次", "1");

            PermissionModel.MED_DIALYSIS_MACHINEEXTENDDataTable _machineDataTable = this._machineService.GetMachineListByUserID(LoginUser.User.USER_ID);
            // this.LabBanChi.Text = string.Format("班次{0}", _banChiDateTable.First(i => i.ITEM_VALUE == banChi).ITEM_NAME);
            this.lblRoom.Text = string.Format("班次     {0}", _banChiDateTable.First(i => i.ITEM_VALUE == banChi).ITEM_NAME);
            this.lblAM1.Text = string.Format("{0}", beginDate.ToString("MM/dd"));
            this.lblAM2.Text = string.Format("{0}", beginDate.AddDays(1).ToString("MM/dd"));
            this.lblAM3.Text = string.Format("{0}", beginDate.AddDays(2).ToString("MM/dd"));
            this.lblAM4.Text = string.Format("{0}", beginDate.AddDays(3).ToString("MM/dd"));
            this.lblAM5.Text = string.Format("{0}", beginDate.AddDays(4).ToString("MM/dd"));
            this.lblAM6.Text = string.Format("{0}", beginDate.AddDays(5).ToString("MM/dd"));

            var data = patientScheduleService.GetPatientScheduleListReportForJL(beginDate, endDate, banChi);


            foreach (PermissionModel.MED_DIALYSIS_MACHINEEXTENDRow itemRow in _machineDataTable.Rows)
            {
                var row = data.FirstOrDefault(i => i.BEDVALUE == itemRow.CWVALUE);
                if (row == null)
                {
                    var newRow = data.NewMED_PATIENT_SCHEDULERow();
                    newRow.AREANAME = itemRow.QYNAME;
                    newRow.AREAVALUE = itemRow.QYVALUE;
                    newRow.BEDNAME = itemRow.CWNAME;
                    newRow.BEDVALUE = itemRow.CWVALUE;
                    data.AddMED_PATIENT_SCHEDULERow(newRow);
                }
            }
            data.OrderBy(i => int.Parse(i.AREAVALUE)).ThenBy(i => int.Parse(i.BEDVALUE)).CopyToDataTable<ReportRelationModel.MED_PATIENT_SCHEDULERow>(_data, LoadOption.PreserveChanges);
            this.DataSource = _data;
            this.DataMember = "";
           
        }
        #endregion
        
        #region 事件

        private void xrTable1_AfterPrint(object sender, EventArgs e)
        {


        }

        private void xrTable1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }

        private void PatientScheduleReportForJl_AfterPrint(object sender, EventArgs e)
        {
            return;
            var aa = new DevExpress.XtraReports.UI.XRLabel();
            aa.Borders = DevExpress.XtraPrinting.BorderSide.All;
            aa.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            aa.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            aa.Name = "aa";
            aa.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            aa.SizeF = new System.Drawing.SizeF(200, 23F);
            aa.StylePriority.UseBorders = false;
            aa.StylePriority.UseFont = false;
            aa.StylePriority.UseTextAlignment = false;
            aa.Text = "";
            aa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.TopMargin.Controls.Add(aa);


            var height = 0F;
            foreach (ReportRelationModel.MED_PATIENT_SCHEDULERow row in _data.Rows)
            {
                height += 25F;
            }
            var xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            xrLine1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            xrLine1.BorderWidth = 0;
            xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, height);
            xrLine1.Name = "xrLine1";
            xrLine1.SizeF = new System.Drawing.SizeF(770.96F, 2F);
            xrLine1.StylePriority.UseBorders = false;
            xrLine1.StylePriority.UseBorderWidth = false;
            this.Detail.Controls.Add(xrLine1);
            //this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            //xrLine1});
        }

        private void PatientScheduleReportForJl_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }

        private void Detail_AfterPrint(object sender, EventArgs e)
        {
        }

        private void PatientScheduleReportForJl_DesignerLoaded(object sender, DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs e)
        {
        }

        #endregion
    }
}
