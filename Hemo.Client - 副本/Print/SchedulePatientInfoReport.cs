/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：排班患者信息报表
// 创建时间：2015-10-27
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Data;
using Hemo.IService.Machine;
using Hemo.Service;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.IService.Config;

namespace Hemo.Client.Print
{
    public partial class SchedulePatientInfoReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 类变量

        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;

        #endregion

        #region 构造函数

        public SchedulePatientInfoReport(DateTime MoonDate, ReportRelationModel.SCHEDULEPATIENTINFODataTable date)
        {
            InitializeComponent();
            this.xrlabMoon.Text = MoonDate.ToString("yyyy年MM月dd日");
            this.lb_BanChi.Text = string.Format("班次:{0}", date[0].BANCHINAME);
            this.lb_AREA.Text = string.Format("组别:{0}", date[0].AREANAME);

            foreach (ReportRelationModel.SCHEDULEPATIENTINFORow row in date.Rows)
            {
                var drug = objHemodialysisService.GetValidCureDrugByHemoID(row.HEMODIALYSIS_ID, row.DIALYSIS_DATE);
                string drugName = string.Empty;
                foreach (HemodialysisModel.MED_CURE_DRUGRow drugRow in drug.Rows)
                {
                    drugName += string.Format("{0}{1}{2}\r\n", drugRow.DRUG_NAME, drugRow.DOSAGE, drugRow.UNIT_NAME);
                }
                row.DRUGNAME = drugName;
            }

            ReportRelationModel.SCHEDULEPATIENTINFODataTable desDate = new ReportRelationModel.SCHEDULEPATIENTINFODataTable();

            var Rows = date.OrderBy(i => i.BANCHIID).ThenBy(i => i.AREAID).ThenBy(i => i.BEDNAME);//.CopyToDataTable<ReportRelationModel.SCHEDULEPATIENTINFODataTable>(desDate, LoadOption.OverwriteChanges);
            foreach (var row in Rows)
            {
                desDate.Rows.Add(row.ItemArray);
            }

            this.DataSource = desDate;
            this.DataMember = "";
        }

        #endregion
    }
}
