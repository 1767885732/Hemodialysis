/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:用户控件类
 * 创建标识:顾伟伟-2013年5月15日
 * 
 * 修改时间:2013年8月23日
 * 修改人:吕志强
 * 修改描述:新增方法
 * 
 * 修改时间:2013年12月1日
 * 修改人:吕志强
 * 修改描述:新增方法
 * 
 * 修改时间:2014年3月11日
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
using DevExpress.XtraGrid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using Hemo.Utilities;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraPrinting;
using Hemo.Client.UI.Machine;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class QueryPatientHemoAge : ViewBase
    {

        #region 变量
        private IHemodialysis hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private bool isConstant = true;
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryPatientHemoAge()
        {
            InitializeComponent();
            this.beginTime.EditValue = Utility.CDate(System.DateTime.Now.Year + "/1/1");
            this.endTime.EditValue = System.DateTime.Now.Date;
        }
        #region 事件
        DataTable dt;
        private void queryData()
        {
            isConstant = chkConstant.Checked;
            DataTable dtPatient = isConstant ? _hemodialysisService.GetHemoIdInLastWeekAndThreeMonthsByDate(Utility.CDate(this.beginTime.DateTime.ToString()), Utility.CDate(this.endTime.DateTime.ToString())) : _hemodialysisService.GetHemoIdByDate(Utility.CDate(this.beginTime.DateTime.ToString()), Utility.CDate(this.endTime.DateTime.ToString()));
            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                dt = hemodialysisService.GetPatientHemoAge(txtName.Text, Utility.CDecimal(txtAge.Text), Utility.CDecimal(txtHemoAge.Text));

                StringBuilder sbStr = new StringBuilder();
                if (dtPatient != null && dtPatient.Rows.Count > 0)
                {
                    string strWhere = string.Empty;

                    foreach (DataRow row in dtPatient.Rows)
                    {
                        sbStr.Append("'").Append(row["HEMODIALYSIS_ID"].ToString()).Append("',");
                    }
                    strWhere = sbStr.ToString();
                    strWhere = strWhere.Substring(0, strWhere.Length - 1);
                    dt = Utility.GetSubTable(dt, "HEMODIALYSIS_ID in (" + strWhere + ")");
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.gcInBasket.DataSource = dt;
                }
                else
                {
                    this.gcInBasket.DataSource = null;
                }
            }
            else
            {
                this.gcInBasket.DataSource = null;
            }
        }

        private void btn_Query_Click(object sender, EventArgs e)
        {
            queryData();
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            //   BaseChartInfo.PrintGridViewCustom(gcInBasket, "患者透析龄列表");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //  this.Close();
        }

        private void QueryEstimateInBasket_Load(object sender, EventArgs e)
        {
            // this.beginTime.DateTime = DateTime.Now.Date.AddMonths(-DateTime.Now.Month + 1).AddDays(-DateTime.Now.Day + 1);
            //  this.endTime.DateTime = DateTime.Now.Date;
        }

        private void gvInBasket_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

        }


        private void btnOutput_Click(object sender, EventArgs e)
        {
            ExportExcel(gcInBasket, "患者透析龄");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="gcResult"></param>
        /// <param name="fileName"></param>
        public void ExportExcel(GridControl gcResult, string fileName)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "导出Excel文件";
            dialog.Filter = "Excel文件(*.xls)|*.xls";
            dialog.FileName = fileName + ".xls";
            DialogResult result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                XlsExportOptions options = new XlsExportOptions();
                options.TextExportMode = TextExportMode.Text;
                gcResult.ExportToXls(dialog.FileName, options);
                XtraMessageBox.Show("导出Excel成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
    }
}