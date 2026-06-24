/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：根据排班与病人信息查询透析单
// 创建时间：2013-07-25
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
//
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Service;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.IService.Config;
using Hemo.Client.Controls;
using Hemo.Client.UI.Machine;
using Hemo.IService;
using Hemo.Client.UI.ReportChart;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class CtlQueryCureList : ViewBase
    {
        #region 类变量

        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;

        private IPatient objPatient = ServiceManager.Instance.PatientService;

        #endregion

        #region 构造函数

        public CtlQueryCureList()
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
        private void QueryPrintCureList_Load(object sender, EventArgs e)
        {
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
        /// 透析记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecord_Click(object sender, EventArgs e)
        {
            ShowCureList();
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExpExcel_Click(object sender, EventArgs e)
        {
            if (this.gridView1.RowCount > 0)
            {
                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.Title = "导出Excel";
                fileDialog.Filter = "Excel文件(*.xls)|*.xls";
                fileDialog.FileName = DateTime.Now.ToString("yyyyMMdd") + "血透患者记录";
                fileDialog.RestoreDirectory = true;
                DialogResult dialogResult = fileDialog.ShowDialog(this);
                if (dialogResult == DialogResult.OK)
                {
                    DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                    options.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
                    this.gridView1.ExportToXls(fileDialog.FileName, options);
                    DevExpress.XtraEditors.XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// 报表说明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNote_Click(object sender, EventArgs e)
        {
            ReportNoteFrm reportNote = new ReportNoteFrm();
            reportNote.ReportType = ReportTypeEnum.血透患者记录;
            reportNote.ShowDialog();
        }

        /// <summary>
        /// 点击列表行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            DataRow row = gridView1.GetFocusedDataRow();
            btnRecord.Enabled = row != null ? true : false;
        }

        /// <summary>
        /// 双击列表行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            ShowCureList();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            string pHemoID = string.Empty;
            string pName = string.Empty;
            if (txtHemoID.Text.Length > 0)
            {
                pHemoID = txtHemoID.Text;
            }
            if (txtName.Text.Length > 0)
            {
                pName = txtName.Text;
            }

            DataTable dt = objPatient.GetPatientListByParams(pName, pHemoID);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdCureList.DataSource = dt;
            }
            else
            {
                grdCureList.DataSource = null;
            }
        }

        /// <summary>
        /// 显示透析记录
        /// </summary>
        private void ShowCureList()
        {
            DataRow dr = this.gridView1.GetFocusedDataRow();
            if (dr != null)
            {
                PatientModel.MED_PATIENTSRow PatientDocRow;
                PatientDocRow = gridView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
                PatientKnowBooks FRM = new PatientKnowBooks();
                FRM.BindDocTree(PatientDocRow);
                FRM.Show();
            }
            else
            {
                XtraMessageBox.Show("请选择一个患者，然后查询透析记录信息。", "血透患者记录");
            }
        }

        #endregion
    }
}
