/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：自定义控价
// 创建时间：2015-08-21
// 创建者：吕志强
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Utilities;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;

namespace Hemo.Client.Controls
{
    public partial class CtlYearQuery : DevExpress.XtraEditors.XtraUserControl
    {
        #region 成员变量

        public event EventHandler QueryEvent;//查询事件

        public event EventHandler ExpExcelEvent;//导出Excel事件

        public event EventHandler PrintEvent;//打印事件

        public event EventHandler RptInstruction; // 报表说明

        #endregion

        #region 属性

        public bool IsRptVisble
        {
            set;
            get;

        }


        public object MonthValue
        {
            get
            {
                return this.lookUpMonth.EditValue;
            }
        }

        public object QuarterValue
        {
            get
            {
                return this.lookUpQuarter.EditValue;
            }
        }

        public string Year
        {
            get
            {
                return this.lookUpYearFrom.EditValue != null ? this.lookUpYearFrom.EditValue.ToString().Substring(0, this.lookUpYearFrom.EditValue.ToString().Length - 1) : DateTime.Now.Year.ToString();
            }
        }


        public string FromYear
        {
            get
            {
                return this.lookUpYearFrom.EditValue != null ? this.lookUpYearFrom.EditValue.ToString().Substring(0, this.lookUpYearFrom.EditValue.ToString().Length - 1) : DateTime.Now.Year.ToString();
            }
        }

        public string ToYear
        {
            get
            {
                return this.lookUpYearTo.EditValue != null ? this.lookUpYearTo.EditValue.ToString().Substring(0, this.lookUpYearTo.EditValue.ToString().Length - 1) : DateTime.Now.Year.ToString();
            }
        }

        public string Month
        {
            get
            {
                string month = this.lookUpMonth.EditValue.ToString().Substring(0, this.lookUpMonth.EditValue.ToString().Length - 1);
                month = (month.Length == 1) ? "0" + month : month;
                return month;
            }
        }

        public string From
        {
            get
            {
                string from = Year + "-" + "01";

                if (this.lookUpQuarter.EditValue.ToString() == "第2季度")
                {
                    from = Year + "-" + "04";
                }
                else if (this.lookUpQuarter.EditValue.ToString() == "第3季度")
                {
                    from = Year + "-" + "07";
                }
                else if (this.lookUpQuarter.EditValue.ToString() == "第4季度")
                {
                    from = Year + "-" + "10";
                }

                return from;
            }
        }

        public String SelectedTab
        {
            get
            {
                string result = string.Empty;
                if (tcDateOp.SelectedTabPageIndex == 0)
                {
                    result = "1";
                }
                else
                {
                    result = "2";
                }
                return result;
            }
        }

        public string To
        {
            get
            {
                string to = Year + "-" + "03";

                if (this.lookUpQuarter.EditValue.ToString() == "第2季度")
                {
                    to = Year + "-" + "06";
                }
                else if (this.lookUpQuarter.EditValue.ToString() == "第3季度")
                {
                    to = Year + "-" + "09";
                }
                else if (this.lookUpQuarter.EditValue.ToString() == "第4季度")
                {
                    to = Year + "-" + "12";
                }

                return to;
            }
        }

        public bool IsYear
        {
            get
            {
                return this.chkIsYear.Checked;
            }
        }

        public DateTime FromDate
        {
            get
            {
                return this.beginTime.DateTime;
            }
        }

        public DateTime ToDate
        {
            get
            {
                return this.endTime.DateTime;
            }
        }

        public int PageIndex
        {
            get
            {
                return this.tcDateOp.SelectedTabPageIndex;
            }
        }

        #endregion

        #region 构造函数

        public CtlYearQuery()
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
        private void CtlYearQuery_Load(object sender, EventArgs e)
        {
            InitLookUpEdit();
            this.btnInstruction.Visible = IsRptVisble;
            this.btnInstruction1.Visible = IsRptVisble;
        }

        /// <summary>
        /// 月度选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpMonth_EditValueChanged(object sender, EventArgs e)
        {
            if (this.lookUpMonth.EditValue != null)
            {
                this.lookUpQuarter.EditValue = null;
            }
        }

        /// <summary>
        /// 季度选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpQuarter_EditValueChanged(object sender, EventArgs e)
        {
            if (this.lookUpQuarter.EditValue != null)
            {
                this.lookUpMonth.EditValue = null;
            }
        }

        /// <summary>
        /// 是否年度选中改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsYear_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkIsYear.Checked)
            {
                this.lookUpMonth.EditValue = null;
                this.lookUpQuarter.EditValue = null;
                this.lookUpMonth.Enabled = false;
                this.lookUpQuarter.Enabled = false;
            }
            else
            {
                this.lookUpMonth.Enabled = true;
                this.lookUpQuarter.Enabled = true;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (this.lookUpYearFrom.EditValue == null || this.lookUpYearFrom.EditValue.ToString() == string.Empty)
            {
                XtraMessageBox.Show("请选择年份！");
                return;
            }

            if (!this.chkIsYear.Checked)
            {
                if ((this.lookUpMonth.EditValue == null || this.lookUpMonth.EditValue.ToString() == string.Empty) && (this.lookUpQuarter.EditValue == null || this.lookUpQuarter.EditValue.ToString() == string.Empty))
                {
                    XtraMessageBox.Show("请选择月度或者季度！");
                    return;
                }
            }

            if (QueryEvent != null)
            {
                QueryEvent(sender, e);
            }
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExpExcel_Click(object sender, EventArgs e)
        {
            if (ExpExcelEvent != null)
            {
                ExpExcelEvent(sender, e);
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (PrintEvent != null)
            {
                PrintEvent(sender, e);
            }
        }
        /// <summary>
        /// 报表说明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInstruction1_Click(object sender, EventArgs e)
        {
            if (RptInstruction != null)
            {
                RptInstruction(sender, e);
            }
        }
        /// <summary>
        /// 报表说明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInstruction_Click(object sender, EventArgs e)
        {
            if (RptInstruction != null)
            {
                RptInstruction(sender, e);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery1_Click(object sender, EventArgs e)
        {
            if (this.beginTime.EditValue == null)
            {
                XtraMessageBox.Show("请选择开始日期！");
                return;
            }

            if (this.endTime.EditValue == null)
            {
                XtraMessageBox.Show("请选择结束日期！");
                return;
            }

            if (QueryEvent != null)
            {
                QueryEvent(sender, e);
            }
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExpExcel1_Click(object sender, EventArgs e)
        {
            if (ExpExcelEvent != null)
            {
                ExpExcelEvent(sender, e);
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint1_Click(object sender, EventArgs e)
        {
            if (PrintEvent != null)
            {
                PrintEvent(sender, e);
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化下拉选项
        /// </summary>
        private void InitLookUpEdit()
        {
            DataTable dtYear = new DataTable();
            dtYear.Columns.Add(new DataColumn("Year"));
            DataRow rowYear;

            for (int i = 2014; i <= Utility.CInt(DateTime.Now.Year.ToString()); i++)
            {
                rowYear = dtYear.NewRow();
                rowYear["Year"] = i.ToString() + "年";
                dtYear.Rows.Add(rowYear);
            }

            BaseControlInfo.BindLookUpEdit(this.lookUpYearFrom, "Year", "Year", dtYear, "Year", "年份");
            BaseControlInfo.BindLookUpEdit(this.lookUpYearTo, "Year", "Year", dtYear, "Year", "年份");


            DataTable dtMonth = new DataTable();
            dtMonth.Columns.Add(new DataColumn("Month"));
            DataRow rowMonth;

            for (int i = 1; i <= 12; i++)
            {
                rowMonth = dtMonth.NewRow();
                rowMonth["Month"] = i.ToString() + "月";
                dtMonth.Rows.Add(rowMonth);
            }

            BaseControlInfo.BindLookUpEdit(this.lookUpMonth, "Month", "Month", dtMonth, "Month", "月度");

            DataTable dtQuarter = new DataTable();
            dtQuarter.Columns.Add(new DataColumn("Quarter"));

            DataRow rowQuarter = dtQuarter.NewRow();
            rowQuarter["Quarter"] = "第1季度";
            dtQuarter.Rows.Add(rowQuarter);

            rowQuarter = dtQuarter.NewRow();
            rowQuarter["Quarter"] = "第2季度";
            dtQuarter.Rows.Add(rowQuarter);

            rowQuarter = dtQuarter.NewRow();
            rowQuarter["Quarter"] = "第3季度";
            dtQuarter.Rows.Add(rowQuarter);

            rowQuarter = dtQuarter.NewRow();
            rowQuarter["Quarter"] = "第4季度";
            dtQuarter.Rows.Add(rowQuarter);

            BaseControlInfo.BindLookUpEdit(this.lookUpQuarter, "Quarter", "Quarter", dtQuarter, "Quarter", "季度");
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

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="gcResult"></param>
        public void Print(GridControl gcResult)
        {
            gcResult.ShowPrintPreview();
        }
        public void InitMUIOperator()
        {
            this.tcDateOp.SelectedTabPageIndex = 1;
            chkIsYear.CheckState = CheckState.Checked;
            //this.lookUpYear.EditValue = System.DateTime.Now.Year.ToString() ;


        }
        #endregion




    }
}
