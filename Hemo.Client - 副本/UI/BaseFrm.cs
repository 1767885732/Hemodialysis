using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hemo.Client.UI {
    public partial class BaseFrm : Form {
        public BaseFrm() {
            InitializeComponent();
        }

        protected DevExpress.XtraGrid.Columns.GridColumn InitializeGridColumn(string colName, string name, string fieldName) {
            return InitializeGridColumn(colName, name, fieldName, false, 91);
        }

        protected DevExpress.XtraGrid.Columns.GridColumn InitializeGridColumn(string colName, string name, string fieldName, bool allowEdit) {
            return InitializeGridColumn(colName, name, fieldName, allowEdit, 91);
        }

        protected DevExpress.XtraGrid.Columns.GridColumn InitializeGridColumn(string colName, string name, string fieldName, bool allowEdit, int width) {
            DevExpress.XtraGrid.Columns.GridColumn gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridColumn1.Caption = colName;
            gridColumn1.FieldName = fieldName;
            gridColumn1.Name = name;
            gridColumn1.OptionsColumn.AllowEdit = allowEdit;
            gridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            gridColumn1.OptionsColumn.ReadOnly = !allowEdit;
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 0;
            gridColumn1.Width = width;
            return gridColumn1;
        }

        private void BaseFrm_Resize(object sender, EventArgs e) {
            if (this.WindowState == FormWindowState.Maximized) {
                this.FormBorderStyle = FormBorderStyle.None;
            }
            else {
                this.FormBorderStyle = FormBorderStyle.Sizable;
            }
        }




        ///// <summary>
        ///// 并发处理数据校验
        ///// </summary>
        ///// <param name="dataTable"></param>
        //public void VerifyConcurrentProcessing(DataTable dataTable) {
        //    foreach (DataRow row in dataTable.Rows) {
        //        if (row.RowState == DataRowState.Modified) {
        //            if (DataOperator.CheckRowChanged(row) == -1) {
        //                throw new Exception("数据被其他人更改，请刷新页面重新提交。");
        //            }
        //        }
        //    }
        //}
    }
}
