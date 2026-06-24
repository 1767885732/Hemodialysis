/*----------------------------------------------------------------
// Copyright (C) 2005 苏州麦迪斯顿医疗科技股份有限公司
// 描述：患者列表窗体
// 创建时间：2015-9-2
// 创建者：刘超
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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Model;
using Hemo.Client.Print;
using Hemo.Utilities;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.UI.Patient
{
    public partial class PatientsListForIntegrated : HemoBaseFrm
    {
        #region 类变量

        #endregion

        #region 属性

        public PatientModel.MED_PATIENTSDataTable _patient { get; set; }

        #endregion

        #region 构造函数

        public PatientsListForIntegrated()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PatientsListForIntegrated_Load(object sender, EventArgs e)
        {
            if (this._patient != null)
            {
                this.gridControl1.DataSource = this._patient;
            }
        }
        public void SetColumnVisitble(bool isShow)
        {
           
            this.gridColumn15.VisibleIndex = 0;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn15.Width = 100;
            this.gridColumn1.Width = 90;         
            if (isShow)
            {
                this.gridColumn10.Width = 700;
                this.gridColumn15.Width = 90;
                this.gridColumn1.Width = 90;
                this.gridColumn3.Width = 80;
                this.gridColumn4.Width = 40;
            }
            this.gridColumn10.Visible = isShow;
            this.gridColumn15.Visible = isShow;
        }


        private void btn_Print_Click(object sender, EventArgs e)
        {
            PatientListQuery report = new PatientListQuery(_patient);
            ReportPrintTool pt = new ReportPrintTool(report);
            pt.ShowPreviewDialog();
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "导出Excel";
            fileDialog.Filter = "Excel文件(*.xls)|*.xls";
            fileDialog.FileName = this.Text;
            DialogResult dialogResult = fileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                options.SheetName = this.Text;
                this.gridControl1.ExportToXls(fileDialog.FileName);
                AutoClosedMsgBox.ShowForm("保存成功！", "提示", 1000, MessageBoxIcon.Information);
            }
        }

        #endregion

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                var row = this.gridView1.GetFocusedDataRow() as DataRow;
                if (row == null)
                    return;
                var hemoId = row["HEMODIALYSIS_ID"].ToString();
                //根据 ID获取每一次的记录
            }
        }
    }
}
