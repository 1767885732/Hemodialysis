/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：促红素信息查询窗体
// 创建时间：2014-09-14
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Hemo.IService.Erythropoietin;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;

namespace Hemo.Client.UI.Erythropoietin {
    public partial class ErythropoietinFrm :HemoBaseFrm{
        #region 变量

        private PatientModel.MED_PATIENTSRow _patientRow;
        private ErythropoietinModel.MED_ERYTHROPOIETINDataTable _erythropoietinDataTable;
        private IErythropoietin _erythropoietinService = ServiceManager.Instance.ErythropoietinService;

        #endregion

        #region 属性

        private DateTime BeginExecDate {
            get {
                return Utility.CDate(this.deBeginExecDate.DateTime.ToString("yyyy-MM-dd 00:00:00"));
            }
        }

        private DateTime EndExecDate {
            get {
                return Utility.CDate(this.deEndExecDate.DateTime.ToString("yyyy-MM-dd 23:59:59"));
            }
        }

        private ErythropoietinModel.MED_ERYTHROPOIETINRow FocusedErythropoietinRow {
            get {
                return this.gvMainInfo.GetFocusedDataRow() as ErythropoietinModel.MED_ERYTHROPOIETINRow;
            }
        }

        #endregion

        #region 构造函数

        public ErythropoietinFrm(PatientModel.MED_PATIENTSRow patientRow, bool isAllowExec) {
            this.InitializeComponent();

            this._patientRow = patientRow;

            if (isAllowExec) {
                this.btnAdd.Visible = this.btnEdit.Visible = false;

                this.gcExec.Visible = true;
            }
            else {
                this.gcExec.Visible = false;

                this.Size = new Size(844, 270);
            }
        }

        #endregion

        #region 方法

        private void LoadErythropoietinData() {
            this._erythropoietinDataTable = this._erythropoietinService.GetErythropoietinList(this._patientRow.HEMODIALYSIS_ID);

            this._erythropoietinDataTable.Columns.Add(new DataColumn("LAST_EXCUTE_DATESTR", typeof(string)));

            foreach (ErythropoietinModel.MED_ERYTHROPOIETINRow erythropoietinRow in this._erythropoietinDataTable.Rows) {
                if (erythropoietinRow["LAST_EXCUTE_DATE"] == DBNull.Value)
                    continue;

                string weekStr = string.Empty;
                DateTime lastExcuteDate = Utility.CDate(erythropoietinRow["LAST_EXCUTE_DATE"].ToString());

                switch (lastExcuteDate.DayOfWeek) {
                    case DayOfWeek.Friday:
                        weekStr = "周5";
                        break;

                    case DayOfWeek.Monday:
                        weekStr = "周1";
                        break;

                    case DayOfWeek.Saturday:
                        weekStr = "周6";
                        break;

                    case DayOfWeek.Sunday:
                        weekStr = "周7";
                        break;

                    case DayOfWeek.Thursday:
                        weekStr = "周4";
                        break;

                    case DayOfWeek.Tuesday:
                        weekStr = "周2";
                        break;

                    case DayOfWeek.Wednesday:
                        weekStr = "周3";
                        break;

                    default:
                        break;
                }

                erythropoietinRow["LAST_EXCUTE_DATESTR"] = string.Format("{0} {1}", weekStr, lastExcuteDate.ToString("yyyy-MM-dd"));
            }

            this.gcMainInfo.DataSource = this._erythropoietinDataTable;

            if (this.gvMainInfo.RowCount > 0)
                this.LoadErythropoietinExecData();
        }

        private void LoadErythropoietinExecData() {
            if (this.FocusedErythropoietinRow == null) {
                XtraMessageBox.Show("请选择要查询的记录！");

                return;
            }

            this.gcExecInfo.DataSource = this._erythropoietinService.GetErythropoietinExecList(this.FocusedErythropoietinRow.ERYTHROPOIETIN_ID, this.BeginExecDate, this.EndExecDate);
        }

        #endregion

        #region 事件

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void ErythropoietinFrm_Load(object sender, EventArgs e) {
            this.deBeginExecDate.DateTime = DateTime.Now.AddDays(-7);
            this.deEndExecDate.DateTime = DateTime.Now;

            this.LoadErythropoietinData();
        }

        private void gvMainInfo_RowClick(object sender, RowClickEventArgs e) {
            this.LoadErythropoietinExecData();
        }

        /// <summary>
        /// 新增 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpt_Click(object sender, EventArgs e) {
            bool isNew = string.Compare(((Control)sender).Name, "btnadd", true) == 0;
            EditErythropoietinFrm editErythropoietinFrm = null;

            if (isNew)
                editErythropoietinFrm = new EditErythropoietinFrm(this._patientRow, this._erythropoietinDataTable, null);
            else {
                if (this.FocusedErythropoietinRow == null) {
                    XtraMessageBox.Show("请选择要编辑的记录！");

                    return;
                }

                editErythropoietinFrm = new EditErythropoietinFrm(this._patientRow, this._erythropoietinDataTable, this.FocusedErythropoietinRow);
            }

            editErythropoietinFrm.ShowDialog();

            if (editErythropoietinFrm.DialogResult == DialogResult.Yes)
                this.LoadErythropoietinData();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchExecInfo_Click(object sender, EventArgs e) {
            this.LoadErythropoietinExecData();
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExec_Click(object sender, EventArgs e) {
            if (this.FocusedErythropoietinRow == null) {
                XtraMessageBox.Show("请选择要执行的记录！");

                return;
            }

            OrderExecFrm orderExecFrm = new OrderExecFrm(this._patientRow, this.FocusedErythropoietinRow.ERYTHROPOIETIN_ID);

            orderExecFrm.ShowDialog();

            if (orderExecFrm.DialogResult == DialogResult.Yes)
                this.LoadErythropoietinData();
        }

        #endregion
    }
}
