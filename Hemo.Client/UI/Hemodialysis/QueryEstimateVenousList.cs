/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:用户控件类
 * 创建标识:刘超-2013年7月24日
 * 
 * 修改时间:2013年11月1日
 * 修改人:顾伟伟
 * 修改描述:修改方法
 * 
 * 修改时间:2014年2月9日
 * 修改人:吕志强
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年5月20日
 * 修改人:刘超
 * 修改描述:新增方法SQL
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

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class QueryEstimateVenousList :HemoBaseFrm
    {
        #region 成员变量

        private bool isTemp = false;

        private string hemoId;

        private IHemodialysis hemodialysisService = ServiceManager.Instance.HemodialysisService;

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

        public QueryEstimateVenousList()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryEstimateVenousList_Load(object sender, EventArgs e)
        {
            this.Text = isTemp ? "查询患者临时留置静脉导管评估记录" : "查询患者长期留置静脉导管评估记录";
            this.deBeginTime.DateTime = DateTime.Now.Date.AddMonths(-DateTime.Now.Month + 1).AddDays(-DateTime.Now.Day + 1);
            this.deEndTime.DateTime = DateTime.Now.Date;
            this.gvEstimateVenous.Columns["SUTURE"].Visible = isTemp;
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var frmEditEstimateVenous = new EditEstimateVenous();
            frmEditEstimateVenous.IsTemp = isTemp;
            frmEditEstimateVenous.HemoId = ((this.gcEstimateVenous.DataSource as DataTable != null) && (this.gcEstimateVenous.DataSource as DataTable).Rows.Count > 0) ? (this.gcEstimateVenous.DataSource as DataTable).Rows[0]["HEMODIALYSIS_ID"].ToString() : hemoId;

            DialogResult result = frmEditEstimateVenous.ShowDialog();
            if (result == DialogResult.OK)
            {
                Query();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.gvEstimateVenous.GetFocusedDataRow() == null)
            {
                XtraMessageBox.Show("请选择一行要删除的记录！");
                return;
            }

            if (XtraMessageBox.Show("是否确认删除当前选择记录？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            string id = this.gvEstimateVenous.GetFocusedDataRow()["ID"].ToString();
            int result = isTemp ? hemodialysisService.DeleteEstimateVenousCatheterById(id) : hemodialysisService.DeleteEstimateLongVenousById(id);

            if (result > 0)
            {
                XtraMessageBox.Show("删除记录成功！");
                Query();
            }
        }

        /// <summary>
        /// 双击列表行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcEstimateVenous_DoubleClick(object sender, EventArgs e)
        {
            if (this.gvEstimateVenous.GetFocusedDataRow() != null)
            {
                var frmEditEstimateVenous = new EditEstimateVenous();
                frmEditEstimateVenous.IsTemp = isTemp;
                frmEditEstimateVenous.HemoId = ((this.gcEstimateVenous.DataSource as DataTable != null) && (this.gcEstimateVenous.DataSource as DataTable).Rows.Count > 0) ? (this.gcEstimateVenous.DataSource as DataTable).Rows[0]["HEMODIALYSIS_ID"].ToString() : hemoId;
                frmEditEstimateVenous.CurrentRow = this.gvEstimateVenous.GetFocusedDataRow();

                DialogResult result = frmEditEstimateVenous.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Query();
                }
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 方法

        private void Query()
        {
            if (!string.IsNullOrEmpty(this.txtName.Text))
            {
                this.gcEstimateVenous.DataSource = isTemp ? hemodialysisService.GetEstimateVenousCatheterByNameAndDate(this.txtName.Text, this.deBeginTime.DateTime, this.deEndTime.DateTime) as DataTable : hemodialysisService.GetEstimateLongVenousByNameAndDate(this.txtName.Text, this.deBeginTime.DateTime, this.deEndTime.DateTime) as DataTable;
            }
            else
            {
                this.gcEstimateVenous.DataSource = isTemp ? hemodialysisService.GetEstimateVenousCatheterByHemoIdAndDate(hemoId, this.deBeginTime.DateTime, this.deEndTime.DateTime) as DataTable : hemodialysisService.GetEstimateLongVenousByHemoIdAndDate(hemoId, this.deBeginTime.DateTime, this.deEndTime.DateTime) as DataTable;
            }
        }

        #endregion
    }
}