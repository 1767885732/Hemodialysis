namespace Hemo.Client.UI.Hemodialysis {
    partial class CtlQueryCureList {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.txtFIRM_TYPE = new DevExpress.XtraEditors.TextEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.grdCureList = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlControls = new DevExpress.XtraEditors.PanelControl();
            this.btnNote = new Hemo.Client.Controls.DXSimpleButton();
            this.btnExpExcel = new Hemo.Client.Controls.DXSimpleButton();
            this.btnRecord = new Hemo.Client.Controls.DXSimpleButton();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtHemoID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnQuery = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtFIRM_TYPE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCureList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlControls)).BeginInit();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHemoID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFIRM_TYPE
            // 
            this.txtFIRM_TYPE.EditValue = "1";
            this.txtFIRM_TYPE.Enabled = false;
            this.txtFIRM_TYPE.Location = new System.Drawing.Point(594, 54);
            this.txtFIRM_TYPE.Name = "txtFIRM_TYPE";
            this.txtFIRM_TYPE.Size = new System.Drawing.Size(17, 20);
            this.txtFIRM_TYPE.TabIndex = 32;
            this.txtFIRM_TYPE.Visible = false;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.grdCureList);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelControl2.Size = new System.Drawing.Size(878, 482);
            this.panelControl2.TabIndex = 9;
            // 
            // grdCureList
            // 
            this.grdCureList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCureList.Location = new System.Drawing.Point(0, 8);
            this.grdCureList.MainView = this.gridView1;
            this.grdCureList.Name = "grdCureList";
            this.grdCureList.Size = new System.Drawing.Size(878, 474);
            this.grdCureList.TabIndex = 51;
            this.grdCureList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn6,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn7,
            this.gridColumn1});
            this.gridView1.GridControl = this.grdCureList;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView1_RowClick);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "透析号";
            this.gridColumn2.FieldName = "HEMODIALYSIS_ID";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 88;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "ID号";
            this.gridColumn6.FieldName = "PATIENT_ID";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 1;
            this.gridColumn6.Width = 92;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "姓名";
            this.gridColumn3.FieldName = "NAME";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 91;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "性别";
            this.gridColumn4.FieldName = "SEX";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 103;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "年龄";
            this.gridColumn5.FieldName = "AGE";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "诊断";
            this.gridColumn7.FieldName = "DIAGNOSE";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 5;
            this.gridColumn7.Width = 190;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "地址";
            this.gridColumn1.FieldName = "ADDRESS";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 6;
            this.gridColumn1.Width = 218;
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.btnNote);
            this.pnlControls.Controls.Add(this.btnExpExcel);
            this.pnlControls.Controls.Add(this.btnRecord);
            this.pnlControls.Controls.Add(this.labelControl5);
            this.pnlControls.Controls.Add(this.txtName);
            this.pnlControls.Controls.Add(this.txtHemoID);
            this.pnlControls.Controls.Add(this.labelControl3);
            this.pnlControls.Controls.Add(this.btnQuery);
            this.pnlControls.Controls.Add(this.txtFIRM_TYPE);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControls.Location = new System.Drawing.Point(0, 482);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(878, 54);
            this.pnlControls.TabIndex = 8;
            // 
            // btnNote
            // 
            this.btnNote.ImageIndex = 4;
            this.btnNote.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnNote.Location = new System.Drawing.Point(749, 17);
            this.btnNote.Name = "btnNote";
            this.btnNote.Size = new System.Drawing.Size(84, 22);
            this.btnNote.TabIndex = 813;
            this.btnNote.Text = "报表说明";
            this.btnNote.Click += new System.EventHandler(this.btnNote_Click);
            // 
            // btnExpExcel
            // 
            this.btnExpExcel.ImageIndex = 4;
            this.btnExpExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnExpExcel.Location = new System.Drawing.Point(659, 17);
            this.btnExpExcel.Name = "btnExpExcel";
            this.btnExpExcel.Size = new System.Drawing.Size(84, 22);
            this.btnExpExcel.TabIndex = 812;
            this.btnExpExcel.Text = "导出Excel";
            this.btnExpExcel.Click += new System.EventHandler(this.btnExpExcel_Click);
            // 
            // btnRecord
            // 
            this.btnRecord.Enabled = false;
            this.btnRecord.ImageIndex = 4;
            this.btnRecord.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnRecord.Location = new System.Drawing.Point(569, 17);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(84, 22);
            this.btnRecord.TabIndex = 45;
            this.btnRecord.Text = "透析记录";
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(176, 21);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(24, 14);
            this.labelControl5.TabIndex = 43;
            this.labelControl5.Text = "姓名";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(206, 18);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 20);
            this.txtName.TabIndex = 44;
            // 
            // txtHemoID
            // 
            this.txtHemoID.Location = new System.Drawing.Point(51, 18);
            this.txtHemoID.Name = "txtHemoID";
            this.txtHemoID.Size = new System.Drawing.Size(100, 20);
            this.txtHemoID.TabIndex = 42;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(9, 21);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 14);
            this.labelControl3.TabIndex = 41;
            this.labelControl3.Text = "透析号";
            // 
            // btnQuery
            // 
            this.btnQuery.ImageIndex = 8;
            this.btnQuery.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnQuery.Location = new System.Drawing.Point(479, 17);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(84, 22);
            this.btnQuery.TabIndex = 36;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // CtlQueryCureList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.pnlControls);
            this.Name = "CtlQueryCureList";
            this.Size = new System.Drawing.Size(878, 536);
            this.Load += new System.EventHandler(this.QueryPrintCureList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtFIRM_TYPE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCureList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlControls)).EndInit();
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHemoID.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtFIRM_TYPE;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl pnlControls;
        private DevExpress.XtraGrid.GridControl grdCureList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private Hemo.Client.Controls.DXSimpleButton btnRecord;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtHemoID;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private Hemo.Client.Controls.DXSimpleButton btnQuery;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private Controls.DXSimpleButton btnNote;
        private Controls.DXSimpleButton btnExpExcel;
    }
}
