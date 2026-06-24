using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Hemo.Model;
using Hemo.IService.Machine;
using Hemo.Service;
using Hemo.IService.Config;
using System.Data;

namespace Hemo.Client.Print
{
    public partial class PrintDrugUserReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="date"></param>
        /// <param name="workloadTable"></param>
        public PrintDrugUserReport(DateTime dt, string banchiID, HemodialysisModel.MED_CURE_DRUGDataTable workloadTable)
        {
            InitializeComponent();
            var bancID = banchiID == "1" ? "上午" : banchiID == "2" ? "下午" : banchiID == "3" ? "晚班" : "急诊";
            this.xrLabel23.Text = string.Format("{0}     {1}     {2}", (workloadTable.Rows.Count > 0) ? workloadTable[0].ROOMNAME : string.Empty, bancID, dt.ToString("yyyy-MM-dd"));
            this.DataSource = workloadTable;
            this.DataMember = "";

        }
        #endregion
    }
}
