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

namespace Hemo.Client.UI.PatientSchedule
{
    public partial class PatientScheduleReport : XtraForm
    {
        #region 构造函数

        public PatientScheduleReport()
        {
            this.InitializeComponent();

            this.barEditReportDate.EditValue = DateTime.Now.Date;

            this.barEditReportDate.EditValueChanged += new EventHandler(barEditReportDate_EditValueChanged);
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

            PatientScheduleReportList report = new PatientScheduleReportList(Utility.CDate(this.barEditReportDate.EditValue.ToString()));
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
        private void PatientScheduleReport_Load(object sender, EventArgs e)
        {
            this.InitReport();
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

        #endregion
    }
}