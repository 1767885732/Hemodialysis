/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:工作量报表类
 * 创建标识:刘超-2017年5月14日
 * ----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Utilities;
using System.Linq;
using System.Data;
using Hemo.Client.Print;
using Hemo.Model;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Text;
using Hemo.IService.Config;
using Hemo.Service;

namespace Hemo.Client.UI.ReportChart
{
    public partial class WorkloadReport : HemoBaseFrm
    {
        #region 成员变量
        /// <summary>
        /// 日期
        /// </summary>
        private DateTime beginDt;
        private DateTime endDt;
        private IHemodialysis hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private bool IsDetal = false;
        /// <summary>
        /// 工作量数据表
        /// </summary>
        private HemoModel.MED_WORKLOADDataTable workloadTable;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkloadReport(DateTime dateStart, DateTime dateEnd)
        {
            this.InitializeComponent();
            this.beginDt = dateStart;
            this.endDt = dateEnd;
            this.barDateTimeStart.EditValue = dateStart.Date;
            this.barDateTimeEnd.EditValue = dateEnd.Date;
        }
        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UseRecordReport_Load(object sender, EventArgs e)
        {
            LoadReport();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadReport();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载报表
        /// </summary>
        private void LoadReport()
        {
            if (this.barDateTimeStart.EditValue == null || this.barDateTimeEnd.EditValue == null)
            {
                XtraMessageBox.Show("请选择开始时间和结束时间！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            if (Utility.CDate(this.barDateTimeStart.EditValue.ToString()) > Utility.CDate(this.barDateTimeEnd.EditValue.ToString()))
            {
                XtraMessageBox.Show("开始时间不能大于结束时间！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            DateTime dateBegin = Utility.CDate(this.barDateTimeStart.EditValue.ToString()).Date;
            DateTime dateEnd = Utility.CDate(this.barDateTimeEnd.EditValue.ToString()).Date;
            string title = string.Format("统计时间:{0}到{1}", dateBegin.ToString("yyyy/MM/dd"), dateEnd.ToString("yyyy/MM/dd"));
            if (IsDetal)
            {
                this.workloadTable = this.hemodialysisService.GetWorkLoadNurseCountByDate(dateBegin, dateEnd);

                Hemo.Client.Print.WorkloadReportSingle report = new Hemo.Client.Print.WorkloadReportSingle(title, workloadTable);
                this.pcReport.PrintingSystem = report.PrintingSystem;
                report.CreateDocument();
            }
            else
            {
                this.workloadTable = this.hemodialysisService.GetWorkLoadCountByDate(dateBegin, dateEnd);

                Hemo.Client.Print.WorkloadReport report = new Hemo.Client.Print.WorkloadReport(title, workloadTable);
                this.pcReport.PrintingSystem = report.PrintingSystem;
                report.CreateDocument();
            }
        }

        #endregion

        private void btnAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IsDetal = false;
            LoadReport();

        }

        private void btnDetal_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IsDetal = true;
            LoadReport();
        }
    }
}