/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:用户控件类
 * 创建标识:贺建操-2013年6月17日
 * 
 * 修改时间:2013年9月25日
 * 修改人:吕志强
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年1月3日
 * 修改人:贺建操
 * 修改描述:修改方法
 * 
 * 修改时间:2014年4月13日
 * 修改人:顾伟伟
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
using Hemo.Client.UI.Machine;
using Hemo.Model;
using Hemo.IService;
using Hemo.Client.UI.Lab;

namespace Hemo.Client.UI.Hemodialysis
{
    [ToolboxItem(true)]
    public partial class QueryEstimateVenousListUI : ViewBase
    {
        #region 成员变量

        private bool isTemp = false;

        private string hemoId;

        private IHemodialysis hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IPatient patientService = ServiceManager.Instance.PatientService;

        #endregion

        #region 属性

        /// <summary>
        /// 是否临时
        /// </summary>
        public bool IsTemp
        {
            get { return isTemp; }
            set { isTemp = value; }
        }

        /// <summary>
        /// 透析编号
        /// </summary>
        public string HemoId
        {
            get { return hemoId; }
            set { hemoId = value; }
        }

        #endregion

        #region 构造函数

        public QueryEstimateVenousListUI() {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryEstimateVenousListUI_Load(object sender, EventArgs e) {
            this.Text = "查询患者长期留置静脉导管评估记录";


            this.deBeginTime.DateTime = DateTime.Now.Date.AddMonths(-DateTime.Now.Month + 1).AddDays(-DateTime.Now.Day + 1);
            this.deEndTime.DateTime = DateTime.Now.Date;
            //ProFunctionCount pfc = new ProFunctionCount();
            //pfc.SaveFunctionCountUI(this);

            QueryData();
        }
        public void InizationDate() {
            this.Text = isTemp ? "查询患者临时留置静脉导管评估记录" : "查询患者长期留置静脉导管评估记录";

            this.gvEstimateVenous.Columns["SUTURE"].Visible = isTemp;

        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e) {
            QueryData();
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e) {
            var estimateVenousReport = new EstimateVenousReport();
            estimateVenousReport.IsTemp = isTemp;
            estimateVenousReport.HemoId = ((this.gcEstimateVenous.DataSource as DataTable != null) && (this.gcEstimateVenous.DataSource as DataTable).Rows.Count > 0) ? (this.gcEstimateVenous.DataSource as DataTable).Rows[0]["HEMODIALYSIS_ID"].ToString() : hemoId;
            estimateVenousReport.PatientNo = estimateVenousReport.HemoId;
            estimateVenousReport.ShowDialog();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e) {
            var frmEditEstimateVenous = new EditEstimateVenous();
            frmEditEstimateVenous.IsTemp = isTemp;
            frmEditEstimateVenous.HemoId = ((this.gcEstimateVenous.DataSource as DataTable != null) && (this.gcEstimateVenous.DataSource as DataTable).Rows.Count > 0) ? (this.gcEstimateVenous.DataSource as DataTable).Rows[0]["HEMODIALYSIS_ID"].ToString() : hemoId;

            DialogResult result = frmEditEstimateVenous.ShowDialog();
            if (result == DialogResult.OK) {
                QueryData();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e) {
            if (this.gvEstimateVenous.GetFocusedDataRow() == null) {
                XtraMessageBox.Show("请选择一行要删除的记录！");
                return;
            }

            if (XtraMessageBox.Show("是否确认删除当前选择记录？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
                return;
            }

            string id = this.gvEstimateVenous.GetFocusedDataRow()["ID"].ToString();
            int result = isTemp ? hemodialysisService.DeleteEstimateVenousCatheterById(id) : hemodialysisService.DeleteEstimateLongVenousById(id);

            if (result > 0) {
                XtraMessageBox.Show("删除记录成功！");
                QueryData();
            }
        }

        /// <summary>
        /// 双击列表行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcEstimateVenous_DoubleClick(object sender, EventArgs e) {
            if (this.gvEstimateVenous.GetFocusedDataRow() != null) {
                var frmEditEstimateVenous = new EditEstimateVenous();
                frmEditEstimateVenous.IsTemp = isTemp;
                frmEditEstimateVenous.HemoId = ((this.gcEstimateVenous.DataSource as DataTable != null) && (this.gcEstimateVenous.DataSource as DataTable).Rows.Count > 0) ? (this.gcEstimateVenous.DataSource as DataTable).Rows[0]["HEMODIALYSIS_ID"].ToString() : hemoId;
                frmEditEstimateVenous.CurrentRow = this.gvEstimateVenous.GetFocusedDataRow();

                DialogResult result = frmEditEstimateVenous.ShowDialog();
                if (result == DialogResult.OK) {
                    QueryData();
                }
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e) {

        }

        #endregion

        #region 方法

        public void QueryData() {
            if (!string.IsNullOrEmpty(this.txtName.Text)) {
                this.gcEstimateVenous.DataSource = isTemp ? hemodialysisService.GetEstimateVenousCatheterByNameAndDate(this.txtName.Text, this.deBeginTime.DateTime, this.deEndTime.DateTime) as DataTable : hemodialysisService.GetEstimateLongVenousByNameAndDate(this.txtName.Text, this.deBeginTime.DateTime, this.deEndTime.DateTime) as DataTable;
            }
            else {
                this.gcEstimateVenous.DataSource = isTemp ? hemodialysisService.GetEstimateVenousCatheterByHemoIdAndDate(hemoId, this.deBeginTime.DateTime, this.deEndTime.DateTime) as DataTable : hemodialysisService.GetEstimateLongVenousByHemoIdAndDate(hemoId, this.deBeginTime.DateTime, this.deEndTime.DateTime) as DataTable;
            }
        }

        #endregion

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click_1(object sender, EventArgs e) {
            this.FindForm().Close();
        }

        private void dxSimpleButton1_Click(object sender, EventArgs e) {
            PatientModel.MED_PATIENTSRow _patientDocRow;
            _patientDocRow = patientService.GetPatientListByParams(string.Empty, HemoId)[0];
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