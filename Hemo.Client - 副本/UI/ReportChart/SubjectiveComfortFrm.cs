/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:综合统计报表类
 * 创建标识:刘超-2017年5月14日
 * ----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Utilities;
using Hemo.Client.Print;
using Hemo.Model;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Text;

namespace Hemo.Client.UI.ReportChart
{
    public partial class SubjectiveComfortFrm :HemoBaseFrm
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public SubjectiveComfortFrm()
        {
            this.InitializeComponent();

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
            txtDateEdit.EditValue = DateTime.Now;
            LoadReport();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
            var report = new Hemo.Client.Print.SubjectiveComfortList();
            report.BindData((DateTime)txtDateEdit.EditValue);
            this.pcReport.PrintingSystem = report.PrintingSystem;
            report.CreateDocument();
        }

        #endregion
    }
}