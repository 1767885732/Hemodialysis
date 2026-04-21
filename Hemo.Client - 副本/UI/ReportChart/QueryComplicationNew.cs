/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:最新并发症统计查询类
 * 创建标识:吕志强-2017年5月8日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Client.UI.Machine;
using Hemo.Model;
using Hemo.Client.Print;
using DevExpress.XtraReports.UI;


namespace Hemo.Client.UI.ReportChart
{
    public partial class QueryComplicationNew : HemoBaseFrm
    {
        #region 成员变量
        /// <summary>
        /// 工作量数据表
        /// </summary>
        private HemoModel.MED_COMPLICATION_OTHERDataTable complicationTable = new HemoModel.MED_COMPLICATION_OTHERDataTable();

        /// <summary>
        /// 数据服务层
        /// </summary>
        private IHemodialysis hemodialysisService = ServiceManager.Instance.HemodialysisService;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryComplicationNew()
        {
            InitializeComponent();
        }
        #endregion

        #region 事件
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void QueryWorkload_Load(object sender, EventArgs e)
        {
            this.deBeginTime.DateTime = DateTime.Now.Date;
            Query();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            ComplicationReportNew report = new ComplicationReportNew(this.deBeginTime.DateTime, this.complicationTable);
            ReportPrintTool pt = new ReportPrintTool(report);
            pt.ShowPreviewDialog();
        }

        #endregion

        #region 方法
        /// <summary>
        /// 查询数据
        /// </summary>
        private void Query()
        {
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    this.complicationTable = this.hemodialysisService.GetComplicationByDate(this.deBeginTime.DateTime);

                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.gcComplication.DataSource = this.complicationTable;

                };
                worker.RunWorkerAsync();
            }
        }
        #endregion
    }
}