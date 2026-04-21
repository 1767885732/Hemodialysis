/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:报表
 * 创建标识:贺建操-2013年8月3日
 * 
 * 修改时间:2013年11月11日
 * 修改人:刘超
 * 修改描述:新增方法
 * 
 * 修改时间:2014年2月19日
 * 修改人:吕志强
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年5月30日
 * 修改人:贺建操
 * 修改描述:修改方法SQL
 * ----------------------------------------------------------------*/
using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Utilities;
using Hemo.Client.Print;
using Hemo.Model;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class EstimateInBasketReport :HemoBaseFrm
    {
        #region 成员变量

        private bool isTemp;

        private string patientName;

        private string hemoID;

        private DataTable inBasketTable;

        #endregion

        #region 属性

        /// <summary>
        /// 病人姓名
        /// </summary>
        public string PatientName
        {
            get { return patientName; }
            set { patientName = value; }
        }

        /// <summary>
        /// 透析号
        /// </summary>
        public string HemoID
        {
            get { return hemoID; }
            set { hemoID = value; }
        }

        public DataTable InBasketTable
        {
            get
            {
                return inBasketTable;
            }
            set
            {
                inBasketTable = value;
            }
        }

        #endregion

        #region 构造函数

        public EstimateInBasketReport()
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
            this.barEditReportDate.EditValue = DateTime.Now.Date;
            LoadReport();
        }

        /// <summary>
        /// 日期改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEditReportDate_EditValueChanged(object sender, EventArgs e)
        {
            if (this.barEditReportDate.EditValue == null)
            {
                XtraMessageBox.Show("请选择月份！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            LoadReport();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载报表
        /// </summary>
        private void LoadReport()
        {
            //EstimateInBasket report = new EstimateInBasket();
            //report.PatientName = patientName;
            //report.HemoID = HemoID;
            //report.CreateDate = Utility.CDate(this.barEditReportDate.EditValue.ToString());
            //if (InBasketTable != null && InBasketTable.Rows.Count > 0)
            //{
            //    report.InBasketTable = InBasketTable;
            //}
            //this.pcReport.PrintingSystem = report.PrintingSystem;
            //report.CreateDocument();
        }
        #endregion
    }
}