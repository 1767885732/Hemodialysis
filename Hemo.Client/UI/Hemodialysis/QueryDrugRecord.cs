/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:记录查询
 * 创建标识:贺建操-2013年7月9日
 * 
 * 修改时间:2013年10月17日
 * 修改人:顾伟伟
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年1月25日
 * 修改人:贺建操
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年5月5日
 * 修改人:顾伟伟
 * 修改描述:新增方法
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
using Hemo.Utilities;
using Hemo.Client.Print;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class QueryDrugRecord : HemoBaseFrm
    {
        #region 变量
        private string pHemodialysisID;
        private string pCureID;
        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;
        private DataTable drugRecord;
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pHemodialysisID"></param>
        public QueryDrugRecord(string pHemodialysisID)
        {
            InitializeComponent();

            this.pHemodialysisID = pHemodialysisID;
            this.pCureID = pCureID;

            this.cmbSTART_DATE.EditValue = DateTime.Now.AddDays(-1).ToShortDateString();
            this.cmbSTART_Time.EditValue = DateTime.Now.AddDays(-1).ToShortTimeString();
            this.cmbEND_DATE.EditValue = DateTime.Now.ToShortDateString();
            this.cmbEND_Time.EditValue = DateTime.Now.ToShortTimeString();
        }
        #region 事件
        private void QueryDrugRecord_Load(object sender, EventArgs e)
        {
            this.ctlUserLongInfo1.HEMODIALYSIS_ID = this.pHemodialysisID;
            this.ctlUserLongInfo1.LoadPatientInfo();
            this.ctlUserLongInfo1.PatientTypeEnabled = true;

            this.SearchAndBind();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SearchAndBind();
        }

        private void SearchAndBind()
        {
            this.drugRecord = this.objHemodialysisService.GetDrugRecord(pHemodialysisID,
                    Utility.CDate(this.cmbSTART_DATE.Text + " " + this.cmbSTART_Time.Text),
                    Utility.CDate(this.cmbEND_DATE.Text + " " + this.cmbEND_Time.Text));

            this.gridControl1.DataSource = this.drugRecord;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            DrugRecordReport report = new DrugRecordReport(this.pHemodialysisID, this.drugRecord);
            ReportPrintTool pt = new ReportPrintTool(report);
            pt.ShowPreviewDialog();
        }
        #endregion
    }
}