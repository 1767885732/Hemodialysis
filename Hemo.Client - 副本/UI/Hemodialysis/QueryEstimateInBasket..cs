/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:用户控件
 * 创建标识:吕志强-2013年7月31日
 * 
 * 修改时间:2013年11月8日
 * 修改人:顾伟伟
 * 修改描述:新增方法
 * 
 * 修改时间:2014年2月16日
 * 修改人:吕志强
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年5月27日
 * 修改人:贺建操
 * 修改描述:修改方法SQL
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
using Hemo.Client.Print;
using Hemo.IService;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.UI.Hemodialysis {
    public partial class QueryEstimateInBasket :HemoBaseFrm
    {

        #region 变量
        private IHemodialysis hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        #endregion
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pHemoID"></param>
        public QueryEstimateInBasket(string pHemoID) {
            InitializeComponent();
            HemoID = pHemoID;
          
        }

        /// <summary>
        /// 透析ID
        /// </summary>
        private string hemoID;
        public string HemoID {
            get {
                return hemoID;
            }
            set {
                hemoID = value;
            }
        }

        /// <summary>
        /// 内瘘评估ID
        /// </summary>
        private string id;
        public string ID {
            get {
                return id;
            }
            set {
                id = value;
            }
        }

        #region 事件
        DataTable dt;
        private void queryData(string pHemoID) {
   

            dt = hemodialysisService.GetEstimateInBasketByParams(pHemoID, txtName.Text, beginTime.DateTime, endTime.DateTime);
            if (dt != null && dt.Rows.Count > 0) {
                this.gcInBasket.DataSource = dt;
            }
            else {
                this.gcInBasket.DataSource = null;
            }
        }

        private void btn_Query_Click(object sender, EventArgs e) {
            queryData(HemoID);
        }

        private void btn_Entering_Click(object sender, EventArgs e) {
            EditEstimateInBasket frm = new EditEstimateInBasket(HemoID);
            frm.HemoID = HemoID;

            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK) {
                queryData(HemoID);
            }
        }

        private void btn_Print_Click(object sender, EventArgs e) {
            DataTable dtPatient = objPatient.GetPatientListByParams("", HemoID);
            string patientName = string.Empty;
            if (dtPatient != null && dtPatient.Rows.Count > 0) {
                patientName = dtPatient.Rows[0]["NAME"].ToString();
            }
            if (dt != null && dt.Rows.Count > 0) {
                EstimateInBasket report = new EstimateInBasket(dt, Utilities.Utility.CDate(System.DateTime.Now.ToShortDateString()), dt.Rows[0]["HEMODIALYSIS_ID"].ToString(), patientName);
                ReportPrintTool pt = new ReportPrintTool(report);
                pt.ShowPreviewDialog();
            }
            else {
                EstimateInBasket report = new EstimateInBasket(null, Utilities.Utility.CDate(System.DateTime.Now.ToShortDateString()), "", patientName);
                ReportPrintTool pt = new ReportPrintTool(report);
                pt.ShowPreviewDialog();
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void gvInBasket_DoubleClick(object sender, EventArgs e) {
            DataRow dr = gvInBasket.GetFocusedDataRow() as DataRow;
            if (dr != null) {
                EditEstimateInBasket frm = new EditEstimateInBasket(HemoID);
                frm.HemoID = HemoID;
                frm.ID = dr["ID"].ToString();
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK) {
                    queryData(HemoID);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if (this.gvInBasket.GetFocusedDataRow() == null) {
                XtraMessageBox.Show("请选择一行要删除的记录！");
                return;
            }

            if (XtraMessageBox.Show("是否确认删除当前选择记录？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
                return;
            }

            string id = this.gvInBasket.GetFocusedDataRow()["ID"].ToString();
            int result = hemodialysisService.DeleteEstimateInBasketById(id);

            if (result > 0) {
                XtraMessageBox.Show("删除记录成功！");
                queryData(HemoID);
            }
        }

        private void QueryEstimateInBasket_Load(object sender, EventArgs e)
        {
            this.beginTime.DateTime = DateTime.Now.Date.AddMonths(-DateTime.Now.Month + 1).AddDays(-DateTime.Now.Day + 1);
            this.endTime.DateTime = DateTime.Now.Date;
            DataTable dtPatient = objPatient.GetPatientListByParams("", HemoID);
            string patientName = string.Empty;
            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                patientName = dtPatient.Rows[0]["NAME"].ToString();
            }
            this.txtName.Text = patientName;
            this.txtHemoID.Text = HemoID;
            queryData(HemoID);
        }
        #endregion
    }
}