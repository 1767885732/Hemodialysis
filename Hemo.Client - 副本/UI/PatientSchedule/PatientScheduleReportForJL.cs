/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:患者排班打印类
 * 创建标识:贺建操-2016年4月23日
 * ----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Utilities;
using Hemo.Client.Print;
using Hemo.Model;
using Hemo.IService.Config;
using Hemo.Service;

namespace Hemo.Client.UI.PatientSchedule
{
    public partial class PatientScheduleReportForJL : HemoBaseFrm
    {
        #region 构造函数
        private IConfig _configService = ServiceManager.Instance.ConfigService;

        public PatientScheduleReportForJL()
        {
            this.InitializeComponent();

            this.barEditReportDate.EditValue = DateTime.Now.Date;

            this.barEditReportDate.EditValueChanged += new EventHandler(barEditReportDate_EditValueChanged);

            ConfigModel.MED_COMMON_ITEMLISTDataTable _banChiDateTable = this._configService.GetConfigList(string.Empty, string.Empty, "班次", "1");
            this.repositoryItemLookUpEdit1.DataSource = _banChiDateTable;
            this.barEditItem_BanChi.EditValue = _banChiDateTable[0].ITEM_VALUE;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化报表
        /// </summary>
        private void InitReport()
        {
            if (this.barEditReportDate.EditValue == null)
            {
                XtraMessageBox.Show("请选择日期！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            if (this.barEditItem_BanChi.EditValue == null)
            {
                XtraMessageBox.Show("请选择班次！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            DateTime date = Utility.CDate(this.barEditReportDate.EditValue.ToString());
            int num = ((int)date.DayOfWeek - (int)DayOfWeek.Monday) != -1 ? (int)date.DayOfWeek - (int)DayOfWeek.Monday : -6;
            num = num > 0 ? -num : num;

            DateTime beginDate = date.AddDays(num);
            DateTime endDate = beginDate.AddDays(5);

            string banChi = this.barEditItem_BanChi.EditValue.ToString();
            PatientScheduleReportForJl report = new PatientScheduleReportForJl(beginDate, endDate, banChi, date);
            this.printControl1.PrintingSystem = report.PrintingSystem;
            report.CreateDocument();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientScheduleReportForJL_Load(object sender, EventArgs e)
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

        #endregion
    }
}