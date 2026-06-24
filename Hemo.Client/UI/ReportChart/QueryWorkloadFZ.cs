/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:工作量统计查询类
 * 创建标识:吕志强-2017年6月21日
 * 
 * 修改时间:2012年7月3日
 * 修改人:贺建操
 * 修改描述:修改部分业务逻辑
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
using Hemo.Utilities;
using Hemo.IService.Dict;
using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting;

namespace Hemo.Client.UI.ReportChart
{
    public partial class QueryWorkloadFZ : ViewBase
    {
        #region 成员变量
        /// <summary>
        /// 工作量数据表
        /// </summary>
        private HemoModel.MED_WORKLOADDataTable workloadTable = new HemoModel.MED_WORKLOADDataTable();

        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        /// <summary>
        /// 数据服务层
        /// </summary>
        private IHemodialysis hemodialysisService = ServiceManager.Instance.HemodialysisService;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryWorkloadFZ()
        {
            InitializeComponent();
            DataTable dtStaffSict = _staffDictService.GetStaffDictList();
            DataTable dtPunctureNurseList = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'", "name");

            if (dtPunctureNurseList != null && dtPunctureNurseList.Rows.Count > 0)
            {
                BaseControlInfo.BindLookUpEdit(txtTJR, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "记录护士");
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void QueryWorkloadFZ_Load(object sender, EventArgs e)
        {
            DateTime beginDt = Utility.CDate(string.Format("{0}/{1}/{2}", DateTime.Now.Year, DateTime.Now.Month, "1"));
            DateTime endDt = beginDt.AddMonths(1).AddDays(-1);
            this.deBeginTime.DateTime = beginDt;
            this.deEndTime.DateTime = endDt;
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

            DevExpress.XtraPrinting.PrintingSystem printingSystem1 = new DevExpress.XtraPrinting.PrintingSystem();
            PrintableComponentLink printableComponentLink1 = new PrintableComponentLink();
            // Add the link to the printing system's collection of links.
            printingSystem1.Links.AddRange(new object[] { printableComponentLink1 });
            // Assign a control to be printed by this link.
            printableComponentLink1.Component = this.gcWorkload;
            // Set the paper orientation to Landscape.
            printableComponentLink1.Landscape = false;
            printableComponentLink1.PaperKind = System.Drawing.Printing.PaperKind.A4;
            string _PrintHeader = string.Format("{0}-{1}工作量报表", this.deBeginTime.DateTime.ToString("yyyy-MM-dd"), this.deEndTime.DateTime.ToString("yyyy-MM-dd"));
            PageHeaderFooter phf = printableComponentLink1.PageHeaderFooter as PageHeaderFooter;
            phf.Header.Content.Clear();
            phf.Header.Content.AddRange(new string[] { "", _PrintHeader, "" });
            phf.Header.Font = new System.Drawing.Font("宋体", 14, System.Drawing.FontStyle.Bold);
            phf.Header.LineAlignment = BrickAlignment.Center;

            //显示打印预览
            printableComponentLink1.ShowPreview();
        }
        /// <summary>
        /// 给Grid加上序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvWorkload_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
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
                this.workloadTable = new HemoModel.MED_WORKLOADDataTable();
                var date = new HemoModel.MED_WORKLOADDataTable();
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    date = this.hemodialysisService.GetWorkloadByDateFZ(this.deBeginTime.DateTime, this.deEndTime.DateTime);

                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    if (txtTJR.EditValue != null && !string.IsNullOrEmpty(txtTJR.Text))
                    {
                        date.Where(i => i.NURSE_ID == this.txtTJR.EditValue.ToString()).CopyToDataTable<HemoModel.MED_WORKLOADRow>(this.workloadTable, LoadOption.PreserveChanges);

                    }
                    else
                    {
                        date.CopyToDataTable<HemoModel.MED_WORKLOADRow>(this.workloadTable, LoadOption.PreserveChanges);

                    }
                    this.gcWorkload.DataSource = this.workloadTable;


                };
                worker.RunWorkerAsync();
            }
        }
        #endregion


    }
}