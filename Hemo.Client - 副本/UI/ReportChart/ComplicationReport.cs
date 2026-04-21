/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:工作量报表类
 * 创建标识:吕志强-2016年5月16日
 * ----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Utilities;
using Hemo.Client.Print;
using Hemo.Model;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Hemo.Client.UI.ReportChart
{
    public partial class ComplicationReport :HemoBaseFrm
    {
        #region 成员变量
        /// <summary>
        /// 日期
        /// </summary>
        private DateTime date;

        /// <summary>
        /// 并发症数据表
        /// </summary>
        private HemoModel.MED_COMPLICATION_OTHERDataTable complicationTable;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ComplicationReport(DateTime date, HemoModel.MED_COMPLICATION_OTHERDataTable complicationTable)
        {
            this.InitializeComponent();
            this.date = date;
            this.complicationTable = complicationTable;
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
        #endregion

        #region 方法

        /// <summary>
        /// 加载报表
        /// </summary>
        private void LoadReport()
        {
            Hemo.Client.Print.ComplicationReport report = new Hemo.Client.Print.ComplicationReport(this.date, this.complicationTable);
            this.pcReport.PrintingSystem = report.PrintingSystem;
            report.CreateDocument();
        }
        #endregion
    }
}