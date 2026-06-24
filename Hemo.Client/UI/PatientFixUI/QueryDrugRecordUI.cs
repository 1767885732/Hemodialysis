/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：用药记录查询类
// 创建时间：2017-03-10
// 创建者：刘配齐
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/
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
using Hemo.Utilities;
using Hemo.Client.Print;
using Hemo.Client.UI.Machine;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.UI.Hemodialysis {
    public partial class QueryDrugRecordUI : ViewBase
    {
        #region 类变量

        private string pHemodialysisID;
        private string pCureID;
        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;
        private DataTable drugRecord;

        #endregion

        #region 属性

        public string HemodialysisID
        {
            get
            {
                return pHemodialysisID;
            }
            set
            {
                pHemodialysisID = value;
            }
        }

        #endregion

        #region 构造函数

        public QueryDrugRecordUI()
        {
            InitializeComponent();

            this.pHemodialysisID = HemodialysisID;

            this.cmbSTART_DATE.EditValue = DateTime.Now.AddDays(-1).ToShortDateString();
            this.cmbSTART_Time.EditValue = DateTime.Now.AddDays(-1).ToShortTimeString();
            this.cmbEND_DATE.EditValue = DateTime.Now.ToShortDateString();
            this.cmbEND_Time.EditValue = DateTime.Now.ToShortTimeString();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryDrugRecord_Load(object sender, EventArgs e)
        {
            //this.ctlUserLongInfo1.HEMODIALYSIS_ID = this.pHemodialysisID;
            //this.ctlUserLongInfo1.LoadPatientInfo();
            //this.ctlUserLongInfo1.PatientTypeEnabled = true;

            //    this.SearchAndBind();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SearchAndBind();
        }
        
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopy_Click(object sender, EventArgs e)
        {
            DrugRecordReport report = new DrugRecordReport(this.pHemodialysisID, this.drugRecord);
            ReportPrintTool pt = new ReportPrintTool(report);
            pt.ShowPreviewDialog();
        }

        #endregion

        #region 方法

        public void SearchAndBind()
        {
            this.drugRecord = this.objHemodialysisService.GetDrugRecord(pHemodialysisID,
                    Utility.CDate(this.cmbSTART_DATE.Text + " " + this.cmbSTART_Time.Text),
                    Utility.CDate(this.cmbEND_DATE.Text + " " + this.cmbEND_Time.Text));

            this.gridControl1.DataSource = this.drugRecord;
        }

        #endregion
    }
}