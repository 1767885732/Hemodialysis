/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:排班患者信息打印类
 * 创建标识:贺建操-2016年4月29日
 * ----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Utilities;
using Hemo.Client.Print;
using System.Linq;
using Hemo.Model;
using System.Data;
using Hemo.IService.Config;
using Hemo.Service;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.UI.PatientSchedule
{
    public partial class SchedulePatientInfostForJL : HemoBaseFrm
    {
        #region 类变量

        private IConfig _configService = ServiceManager.Instance.ConfigService;

        private DateTime dt = new DateTime();
        private ReportRelationModel.SCHEDULEPATIENTINFODataTable date = null;

        #endregion

        #region 构造函数

        public SchedulePatientInfostForJL(DateTime MoonDate, ReportRelationModel.SCHEDULEPATIENTINFODataTable dateAll)
        {
            this.InitializeComponent();
            dt = MoonDate;
            date = dateAll;

            ConfigModel.MED_COMMON_ITEMLISTDataTable _banChiDateTable = this._configService.GetConfigList(string.Empty, string.Empty, "班次", "1");
            ConfigModel.MED_COMMON_ITEMLISTDataTable _areaDateTable = this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1");


            this.repositoryItemLookUpEdit1.DataSource = _banChiDateTable;
            this.repositoryItemLookUpEdit2.DataSource = _areaDateTable;
            this.barEditItem_Area.EditValue = _areaDateTable[0].ITEM_VALUE;

            this.barEditItem_BanChi.EditValue = _banChiDateTable[0].ITEM_VALUE;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化报表
        /// </summary>
        private void InitReport()
        {
            if (this.barEditItem_BanChi.EditValue == null)
            {
                XtraMessageBox.Show("请选择班次！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            if (this.barEditItem_Area.EditValue == null)
            {
                XtraMessageBox.Show("请选择组别！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            string banChi = this.barEditItem_BanChi.EditValue.ToString();
            string areaName = this.barEditItem_Area.EditValue.ToString();
            var dateFilter = new ReportRelationModel.SCHEDULEPATIENTINFODataTable();
            date.Where(i => i.BANCHIID == banChi && i.AREAID == areaName).CopyToDataTable<ReportRelationModel.SCHEDULEPATIENTINFORow>(dateFilter, LoadOption.PreserveChanges);
            if (dateFilter.Rows.Count <= 0)
            {
                XtraMessageBox.Show("无数据，请重新选择！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            SchedulePatientInfoReport report = new SchedulePatientInfoReport(dt, dateFilter);
           // ReportPrintTool tool = new ReportPrintTool(report);
          //  this.printControl1.PrintingSystem = tool.PrintingSystem;
           // tool.ShowRibbonPreview();

            this.printControl1.PrintingSystem = report.PrintingSystem;
            report.DataSource = dateFilter;
            report.CreateDocument();
            
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SchedulePatientInfostForJL_Load(object sender, EventArgs e)
        {
            //this.InitReport();
        }

        /// <summary>
        /// 日期改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barEditReportDate_EditValueChanged(object sender, EventArgs e)
        {
            this.InitReport();
        }

        /// <summary>
        /// 班次改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEditItem_BanChi_EditValueChanged(object sender, EventArgs e)
        {
            this.InitReport();
        }

        /// <summary>
        /// 病区改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEditItem_Area_EditValueChanged(object sender, EventArgs e)
        {
            if (this.barEditItem_BanChi.EditValue == null)
            {

                return;
            }
            this.InitReport();

        }

        #endregion
    }
}