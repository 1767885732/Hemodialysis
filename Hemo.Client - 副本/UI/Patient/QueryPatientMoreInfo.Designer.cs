namespace Hemo.Client.UI.Patient {
    partial class QueryPatientMoreInfo {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl86 = new DevExpress.XtraEditors.LabelControl();
            this.txtSEX = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtHemoEndAge = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtHemoBeginAge = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtEndAge = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtEndDate = new DevExpress.XtraEditors.DateEdit();
            this.txtBeginDate = new DevExpress.XtraEditors.DateEdit();
            this.btnEdit = new Hemo.Client.Controls.DXSimpleButton();
            this.txtBeginAge = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnQuery = new Hemo.Client.Controls.DXSimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSEX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHemoEndAge.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHemoBeginAge.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndAge.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBeginDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBeginDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBeginAge.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.labelControl86);
            this.panelControl2.Controls.Add(this.txtSEX);
            this.panelControl2.Controls.Add(this.txtHemoEndAge);
            this.panelControl2.Controls.Add(this.labelControl5);
            this.panelControl2.Controls.Add(this.txtHemoBeginAge);
            this.panelControl2.Controls.Add(this.labelControl6);
            this.panelControl2.Controls.Add(this.txtEndAge);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.txtEndDate);
            this.panelControl2.Controls.Add(this.txtBeginDate);
            this.panelControl2.Controls.Add(this.btnEdit);
            this.panelControl2.Controls.Add(this.txtBeginAge);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Controls.Add(this.btnQuery);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(984, 51);
            this.panelControl2.TabIndex = 45;
            // 
            // labelControl86
            // 
            this.labelControl86.Location = new System.Drawing.Point(684, 18);
            this.labelControl86.Name = "labelControl86";
            this.labelControl86.Size = new System.Drawing.Size(24, 14);
            this.labelControl86.TabIndex = 252;
            this.labelControl86.Text = "性别";
            // 
            // txtSEX
            // 
            this.txtSEX.EnterMoveNextControl = true;
            this.txtSEX.Location = new System.Drawing.Point(712, 15);
            this.txtSEX.Name = "txtSEX";
            this.txtSEX.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSEX.Properties.Items.AddRange(new object[] {
            "男",
            "女",
            "  "});
            this.txtSEX.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtSEX.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtSEX.Size = new System.Drawing.Size(60, 20);
            this.txtSEX.TabIndex = 251;
            // 
            // txtHemoEndAge
            // 
            this.txtHemoEndAge.Location = new System.Drawing.Point(603, 15);
            this.txtHemoEndAge.Name = "txtHemoEndAge";
            this.txtHemoEndAge.Size = new System.Drawing.Size(60, 20);
            this.txtHemoEndAge.TabIndex = 30;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(588, 18);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(12, 14);
            this.labelControl5.TabIndex = 29;
            this.labelControl5.Text = "至";
            // 
            // txtHemoBeginAge
            // 
            this.txtHemoBeginAge.Location = new System.Drawing.Point(523, 15);
            this.txtHemoBeginAge.Name = "txtHemoBeginAge";
            this.txtHemoBeginAge.Size = new System.Drawing.Size(60, 20);
            this.txtHemoBeginAge.TabIndex = 27;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(487, 18);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(36, 14);
            this.labelControl6.TabIndex = 28;
            this.labelControl6.Text = "透析龄";
            // 
            // txtEndAge
            // 
            this.txtEndAge.Location = new System.Drawing.Point(410, 16);
            this.txtEndAge.Name = "txtEndAge";
            this.txtEndAge.Size = new System.Drawing.Size(60, 20);
            this.txtEndAge.TabIndex = 26;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(394, 19);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(12, 14);
            this.labelControl2.TabIndex = 25;
            this.labelControl2.Text = "至";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(182, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(12, 14);
            this.labelControl1.TabIndex = 24;
            this.labelControl1.Text = "至";
            // 
            // txtEndDate
            // 
            this.txtEndDate.CausesValidation = false;
            this.txtEndDate.EditValue = null;
            this.txtEndDate.EnterMoveNextControl = true;
            this.txtEndDate.Location = new System.Drawing.Point(199, 16);
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtEndDate.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtEndDate.Size = new System.Drawing.Size(100, 20);
            this.txtEndDate.TabIndex = 23;
            // 
            // txtBeginDate
            // 
            this.txtBeginDate.CausesValidation = false;
            this.txtBeginDate.EditValue = null;
            this.txtBeginDate.EnterMoveNextControl = true;
            this.txtBeginDate.Location = new System.Drawing.Point(78, 16);
            this.txtBeginDate.Name = "txtBeginDate";
            this.txtBeginDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtBeginDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtBeginDate.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtBeginDate.Size = new System.Drawing.Size(100, 20);
            this.txtBeginDate.TabIndex = 22;
            // 
            // btnEdit
            // 
            this.btnEdit.ImageIndex = 9;
            this.btnEdit.Location = new System.Drawing.Point(868, 14);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "导出";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // txtBeginAge
            // 
            this.txtBeginAge.Location = new System.Drawing.Point(331, 16);
            this.txtBeginAge.Name = "txtBeginAge";
            this.txtBeginAge.Size = new System.Drawing.Size(60, 20);
            this.txtBeginAge.TabIndex = 0;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(25, 19);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 21;
            this.labelControl4.Text = "透析日期";
            // 
            // btnQuery
            // 
            this.btnQuery.ImageIndex = 8;
            this.btnQuery.Location = new System.Drawing.Point(787, 14);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 2;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(305, 19);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 14);
            this.labelControl3.TabIndex = 14;
            this.labelControl3.Text = "年龄";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 51);
            this.gridControl1.MainView = this.gridView4;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(984, 403);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView4});
            // 
            // gridView4
            // 
            this.gridView4.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn6,
            this.gridColumn11,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn2});
            this.gridView4.GridControl = this.gridControl1;
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsBehavior.Editable = false;
            this.gridView4.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridView4.OptionsView.ShowGroupPanel = false;
            this.gridView4.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView4_RowClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "透析号";
            this.gridColumn1.FieldName = "HEMODIALYSIS_ID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 82;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "姓名";
            this.gridColumn3.FieldName = "NAME";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 72;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "性别";
            this.gridColumn4.FieldName = "SEX";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 72;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "年龄";
            this.gridColumn6.FieldName = "PATIENT_AGE";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            this.gridColumn6.Width = 72;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "证件号码";
            this.gridColumn11.FieldName = "CREDENTIALS_NUMBER";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 8;
            this.gridColumn11.Width = 70;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "首次透析日期";
            this.gridColumn7.FieldName = "SPECIFIC_TIME";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 4;
            this.gridColumn7.Width = 84;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "诊断";
            this.gridColumn8.FieldName = "DIAGNOSE";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 5;
            this.gridColumn8.Width = 70;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "透析频率";
            this.gridColumn9.FieldName = "HEMO_TIMES";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 6;
            this.gridColumn9.Width = 70;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "促红素剂量";
            this.gridColumn10.FieldName = "FIRST_DRUG_DOSAGE";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 7;
            this.gridColumn10.Width = 95;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "抗凝剂种类";
            this.gridColumn12.FieldName = "THERAPEUTIC_METHOD";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 9;
            this.gridColumn12.Width = 70;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "透析终止日期";
            this.gridColumn13.FieldName = "END_DATE";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 10;
            this.gridColumn13.Width = 70;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "原因";
            this.gridColumn14.FieldName = "EVENTANALYSIS";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 11;
            this.gridColumn14.Width = 70;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "死亡原因";
            this.gridColumn2.FieldName = "CORRECTIVEACTIONS";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 12;
            this.gridColumn2.Width = 86;
            // 
            // QueryPatientMoreInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelControl2);
            this.Name = "QueryPatientMoreInfo";
            this.Size = new System.Drawing.Size(984, 454);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSEX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHemoEndAge.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHemoBeginAge.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndAge.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBeginDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBeginDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBeginAge.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private Hemo.Client.Controls.DXSimpleButton btnEdit;
        private DevExpress.XtraEditors.TextEdit txtBeginAge;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private Hemo.Client.Controls.DXSimpleButton btnQuery;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit txtEndDate;
        private DevExpress.XtraEditors.DateEdit txtBeginDate;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtHemoEndAge;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtHemoBeginAge;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtEndAge;
        private DevExpress.XtraEditors.LabelControl labelControl86;
        private DevExpress.XtraEditors.ComboBoxEdit txtSEX;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;


    }
}