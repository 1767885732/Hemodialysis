/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：病人透析信息综合查询基础列表窗体
// 创建时间：2016-03-11
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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using Hemo.Service;
using Hemo.Model;
using Hemo.IService;
using Hemo.Client.UI.Hemodialysis;
using Hemo.IService.Config;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using Hemo.Client.UI.Machine;

namespace Hemo.Client.UI.Patient {
    public partial class QueryPatientMoreInfo : ViewBase {

        #region 私有成员
        /// <summary>
        /// 病人列表
        /// </summary>
        private DataTable _patientDataTable;
        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;

        #endregion

        #region 初始化方法
        public QueryPatientMoreInfo() {
            InitializeComponent();
        }
        #endregion

        #region 各种事件
        private void btnQuery_Click(object sender, EventArgs e) {
            loadGridData();
        }

        private void gridView4_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e) {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ExportExcel(gridControl1, "透析患者综合统计");

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

        #region 数据方法
        private void loadGridData() {
            loadPatientListByName();
        }

        private void loadPatientListByName() {
            _patientDataTable = objHemodialysisService.QueryPatientMoreInfoList(txtBeginDate.DateTime, txtEndDate.DateTime, txtBeginAge.Text, txtEndAge.Text, txtHemoBeginAge.Text, txtHemoEndAge.Text, txtSEX.Text.Trim());
            gridControl1.DataSource = _patientDataTable;
        }
        #endregion
    }
}