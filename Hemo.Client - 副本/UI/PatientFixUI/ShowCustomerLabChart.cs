using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;

namespace Hemo.Client.UI.PatientFixUI
{
    public partial class ShowCustomerLabChart : DevExpress.XtraEditors.XtraForm
    {
        public ShowCustomerLabChart()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 图表数据来源 
        /// </summary>
        private DataTable _chartDataTable;
        public DataTable ChartDataTable
        {
            set
            {
                _chartDataTable = value;
            }
            get
            {
                return _chartDataTable;
            }
        }

        /// <summary>
        /// 图标标题
        /// </summary>
        private string _chartTitle = string.Empty;
        public string ChartTitle
        {
            set
            {
                _chartTitle = value;
            }
            get
            {
                return _chartTitle;
            }
        }

        /// <summary>
        /// 轴标题
        /// </summary>
        private string _seriesTitle = string.Empty;
        public string SeriesTitle
        {
            set
            {
                _seriesTitle = value;
            }
            get
            {
                return _seriesTitle;
            }
        }

        /// <summary>
        /// 列一
        /// </summary>
        private string _column1 = string.Empty;
        public string Column1
        {
            set
            {
                _column1 = value;
            }
            get
            {
                return _column1;
            }
        }

        /// <summary>
        /// 列二
        /// </summary>
        private string _column2 = string.Empty;
        public string Column2
        {
            set
            {
                _column2 = value;
            }
            get
            {
                return _column2;
            }
        }

        public void ShowChart()
        {
            if (beginTime.EditValue != null && beginTime.EditValue != null)
            {
                ChartDataTable = Utilities.Utility.GetSubTable(ChartDataTable, "检验日期 >='" + Utilities.Utility.CDate(beginTime.EditValue.ToString()) + "' AND 检验日期 <='" + Utilities.Utility.CDate(endTime.EditValue.ToString()) + "'");
            }
            if (ChartDataTable != null && ChartDataTable.Rows.Count > 0)
            {
                ctlSignChart1.DrawCustomerChart(ChartDataTable, ChartTitle, SeriesTitle, ViewType.Line, Column1, Column2);
            }
        }

        private void ShowCustomerLabChart_Load(object sender, EventArgs e)
        {
            ShowChart();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (ChartDataTable != null && ChartDataTable.Rows.Count > 0)
            {
                ShowChart();
            }
        }

        private void btnExpExcel_Click(object sender, EventArgs e)
        {
            ctlSignChart1.ExportToImage();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}