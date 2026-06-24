/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:用户控件类
 * 创建标识:吕志强-2013年7月9日
 * 
 * 修改时间:2013年10月17日
 * 修改人:吕志强
 * 修改描述:修改方法
 * 
 * 修改时间:2014年1月25日
 * 修改人:贺建操
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年5月5日
 * 修改人:刘超
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
using Hemo.Client.UI.Machine;
using DevExpress.XtraReports.UI;
using Hemo.Model;
using Hemo.Client.UI.Lab;

namespace Hemo.Client.UI.Hemodialysis {
    [ToolboxItem(true)]
    public partial class QueryEstimateInBasketUI : ViewBase
    {

        #region 变量
        private IHemodialysis hemodialysisService = ServiceManager.Instance.HemodialysisService;


        private IPatient objPatient = ServiceManager.Instance.PatientService;

        private IPatient patientService = ServiceManager.Instance.PatientService;

        #endregion
        #region 构造函数
        public QueryEstimateInBasketUI() {
            InitializeComponent();

        }
        #endregion
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
        public void queryData(string pHemoID) {
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

        private void btnClose_Click_1(object sender, EventArgs e) {
            this.FindForm().Close();
        }

        private void labelControl4_Click(object sender, EventArgs e) {

        }

        private void panelControl1_Paint(object sender, PaintEventArgs e) {

        }

        private void QueryEstimateInBasketUI_Load(object sender, EventArgs e) {
            if (!this.DesignMode) {

                this.Text = "内瘘评估";

                ProFunctionCount pfc = new ProFunctionCount();
                pfc.SaveFunctionCountUI(this);

                this.beginTime.DateTime = DateTime.Now.Date.AddMonths(-DateTime.Now.Month + 1).AddDays(-DateTime.Now.Day + 1);
                this.endTime.DateTime = DateTime.Now.Date;
                DataTable dtPatient = objPatient.GetPatientListByParams("", HemoID);
                string patientName = string.Empty;
                if (dtPatient != null && dtPatient.Rows.Count > 0) {
                    patientName = dtPatient.Rows[0]["NAME"].ToString();
                }
                this.txtName.Text = patientName;
                this.txtHemoID.Text = HemoID;
            }

        }

        private void gvInBasket_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e) {
            var row = gvInBasket.GetDataRow(e.RowHandle);
            if (row != null) {
                var redhot = row["RED_HOT"].ToString();
                if (redhot == "√") {
                    e.Appearance.BackColor = Color.Red;
                }

                var swollen_pain = row["SWOLLEN_PAIN"].ToString();
                if (swollen_pain == "√") {
                    e.Appearance.BackColor = Color.Red;
                }

                var ECCHYMOSIS = row["ecchymosis"].ToString();
                if (ECCHYMOSIS == "√") {
                    e.Appearance.BackColor = Color.Red;
                }
            }
        }
        #endregion

        private void dxSimpleButton1_Click(object sender, EventArgs e) {
            PatientModel.MED_PATIENTSRow _patientDocRow;
            _patientDocRow = patientService.GetPatientListByParams(string.Empty, HemoID)[0];
            if (_patientDocRow != null) {
                XtraForm form = new XtraForm();
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Text = _patientDocRow.NAME + "的检验数据";
                ctlLabFrm labFrm = new ctlLabFrm(_patientDocRow);
                form.Size = labFrm.Size;
                labFrm.LoadLabInfo(_patientDocRow);
                labFrm.Dock = DockStyle.Fill;
                form.Controls.Add(labFrm);
                form.Show();
            }
        }
    }
}