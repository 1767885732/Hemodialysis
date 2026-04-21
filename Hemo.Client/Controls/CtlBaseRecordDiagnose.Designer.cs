namespace Hemo.Client.Controls
{
    partial class CtlBaseRecordDiagnose
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.gcRecord = new DevExpress.XtraGrid.GridControl();
            this.gvRecord = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIN_HOSPITAL_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLEAVE_HOSPITAL_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDIAGNOSE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCREATEBY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.txtLEAVE_HOSPITAL_DATE = new DevExpress.XtraEditors.DateEdit();
            this.lblLEAVE_HOSPITAL_DATE = new DevExpress.XtraEditors.LabelControl();
            this.lblDIAGNOSE = new DevExpress.XtraEditors.LabelControl();
            this.lupCREATEBY = new DevExpress.XtraEditors.LookUpEdit();
            this.lblCREATEBY = new DevExpress.XtraEditors.LabelControl();
            this.txtIN_HOSPITAL_DATE = new DevExpress.XtraEditors.DateEdit();
            this.txtDIAGNOSE = new DevExpress.XtraEditors.TextEdit();
            this.lblIN_HOSPITAL_DATE = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLEAVE_HOSPITAL_DATE.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLEAVE_HOSPITAL_DATE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupCREATEBY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIN_HOSPITAL_DATE.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIN_HOSPITAL_DATE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDIAGNOSE.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.gcRecord);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(660, 358);
            this.panelControl3.TabIndex = 2;
            // 
            // gcRecord
            // 
            this.gcRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcRecord.Location = new System.Drawing.Point(2, 2);
            this.gcRecord.MainView = this.gvRecord;
            this.gcRecord.Name = "gcRecord";
            this.gcRecord.Size = new System.Drawing.Size(656, 354);
            this.gcRecord.TabIndex = 0;
            this.gcRecord.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRecord});
            // 
            // gvRecord
            // 
            this.gvRecord.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvRecord.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvRecord.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvRecord.Appearance.Row.Options.UseFont = true;
            this.gvRecord.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIN_HOSPITAL_DATE,
            this.colLEAVE_HOSPITAL_DATE,
            this.colDIAGNOSE,
            this.colCREATEBY});
            this.gvRecord.GridControl = this.gcRecord;
            this.gvRecord.Name = "gvRecord";
            this.gvRecord.OptionsBehavior.Editable = false;
            this.gvRecord.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvRecord.OptionsView.ShowGroupPanel = false;
            this.gvRecord.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvRecord_RowClick);
            // 
            // colIN_HOSPITAL_DATE
            // 
            this.colIN_HOSPITAL_DATE.Caption = "住院日期";
            this.colIN_HOSPITAL_DATE.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.colIN_HOSPITAL_DATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colIN_HOSPITAL_DATE.FieldName = "IN_HOSPITAL_DATE";
            this.colIN_HOSPITAL_DATE.Name = "colIN_HOSPITAL_DATE";
            this.colIN_HOSPITAL_DATE.Visible = true;
            this.colIN_HOSPITAL_DATE.VisibleIndex = 0;
            // 
            // colLEAVE_HOSPITAL_DATE
            // 
            this.colLEAVE_HOSPITAL_DATE.Caption = "出院日期";
            this.colLEAVE_HOSPITAL_DATE.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.colLEAVE_HOSPITAL_DATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colLEAVE_HOSPITAL_DATE.FieldName = "LEAVE_HOSPITAL_DATE";
            this.colLEAVE_HOSPITAL_DATE.Name = "colLEAVE_HOSPITAL_DATE";
            this.colLEAVE_HOSPITAL_DATE.Visible = true;
            this.colLEAVE_HOSPITAL_DATE.VisibleIndex = 1;
            // 
            // colDIAGNOSE
            // 
            this.colDIAGNOSE.Caption = "住院诊断";
            this.colDIAGNOSE.FieldName = "DIAGNOSE";
            this.colDIAGNOSE.Name = "colDIAGNOSE";
            this.colDIAGNOSE.Visible = true;
            this.colDIAGNOSE.VisibleIndex = 2;
            // 
            // colCREATEBY
            // 
            this.colCREATEBY.Caption = "记录人";
            this.colCREATEBY.FieldName = "DOCTOR_NAME";
            this.colCREATEBY.Name = "colCREATEBY";
            this.colCREATEBY.Visible = true;
            this.colCREATEBY.VisibleIndex = 3;
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.txtLEAVE_HOSPITAL_DATE);
            this.panelControl4.Controls.Add(this.lblLEAVE_HOSPITAL_DATE);
            this.panelControl4.Controls.Add(this.lblDIAGNOSE);
            this.panelControl4.Controls.Add(this.lupCREATEBY);
            this.panelControl4.Controls.Add(this.lblCREATEBY);
            this.panelControl4.Controls.Add(this.txtIN_HOSPITAL_DATE);
            this.panelControl4.Controls.Add(this.txtDIAGNOSE);
            this.panelControl4.Controls.Add(this.lblIN_HOSPITAL_DATE);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl4.Location = new System.Drawing.Point(0, 358);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(660, 77);
            this.panelControl4.TabIndex = 4;
            // 
            // txtLEAVE_HOSPITAL_DATE
            // 
            this.txtLEAVE_HOSPITAL_DATE.EditValue = null;
            this.txtLEAVE_HOSPITAL_DATE.Enabled = false;
            this.txtLEAVE_HOSPITAL_DATE.EnterMoveNextControl = true;
            this.txtLEAVE_HOSPITAL_DATE.Location = new System.Drawing.Point(228, 8);
            this.txtLEAVE_HOSPITAL_DATE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLEAVE_HOSPITAL_DATE.Name = "txtLEAVE_HOSPITAL_DATE";
            this.txtLEAVE_HOSPITAL_DATE.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLEAVE_HOSPITAL_DATE.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtLEAVE_HOSPITAL_DATE.Properties.Appearance.Options.UseFont = true;
            this.txtLEAVE_HOSPITAL_DATE.Properties.Appearance.Options.UseForeColor = true;
            this.txtLEAVE_HOSPITAL_DATE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtLEAVE_HOSPITAL_DATE.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtLEAVE_HOSPITAL_DATE.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.txtLEAVE_HOSPITAL_DATE.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtLEAVE_HOSPITAL_DATE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtLEAVE_HOSPITAL_DATE.Size = new System.Drawing.Size(88, 26);
            this.txtLEAVE_HOSPITAL_DATE.TabIndex = 403;
            // 
            // lblLEAVE_HOSPITAL_DATE
            // 
            this.lblLEAVE_HOSPITAL_DATE.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLEAVE_HOSPITAL_DATE.Location = new System.Drawing.Point(170, 11);
            this.lblLEAVE_HOSPITAL_DATE.Name = "lblLEAVE_HOSPITAL_DATE";
            this.lblLEAVE_HOSPITAL_DATE.Size = new System.Drawing.Size(56, 20);
            this.lblLEAVE_HOSPITAL_DATE.TabIndex = 402;
            this.lblLEAVE_HOSPITAL_DATE.Text = "出院日期";
            // 
            // lblDIAGNOSE
            // 
            this.lblDIAGNOSE.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDIAGNOSE.Location = new System.Drawing.Point(13, 48);
            this.lblDIAGNOSE.Name = "lblDIAGNOSE";
            this.lblDIAGNOSE.Size = new System.Drawing.Size(28, 20);
            this.lblDIAGNOSE.TabIndex = 399;
            this.lblDIAGNOSE.Text = "诊断";
            // 
            // lupCREATEBY
            // 
            this.lupCREATEBY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lupCREATEBY.Enabled = false;
            this.lupCREATEBY.EnterMoveNextControl = true;
            this.lupCREATEBY.Location = new System.Drawing.Point(391, 8);
            this.lupCREATEBY.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lupCREATEBY.Name = "lupCREATEBY";
            this.lupCREATEBY.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lupCREATEBY.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lupCREATEBY.Properties.Appearance.Options.UseFont = true;
            this.lupCREATEBY.Properties.Appearance.Options.UseForeColor = true;
            this.lupCREATEBY.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupCREATEBY.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.lupCREATEBY.Properties.NullText = "";
            this.lupCREATEBY.Size = new System.Drawing.Size(92, 26);
            this.lupCREATEBY.TabIndex = 397;
            // 
            // lblCREATEBY
            // 
            this.lblCREATEBY.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCREATEBY.Location = new System.Drawing.Point(333, 11);
            this.lblCREATEBY.Name = "lblCREATEBY";
            this.lblCREATEBY.Size = new System.Drawing.Size(56, 20);
            this.lblCREATEBY.TabIndex = 398;
            this.lblCREATEBY.Text = "记录医生";
            // 
            // txtIN_HOSPITAL_DATE
            // 
            this.txtIN_HOSPITAL_DATE.EditValue = null;
            this.txtIN_HOSPITAL_DATE.Enabled = false;
            this.txtIN_HOSPITAL_DATE.EnterMoveNextControl = true;
            this.txtIN_HOSPITAL_DATE.Location = new System.Drawing.Point(71, 8);
            this.txtIN_HOSPITAL_DATE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtIN_HOSPITAL_DATE.Name = "txtIN_HOSPITAL_DATE";
            this.txtIN_HOSPITAL_DATE.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIN_HOSPITAL_DATE.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtIN_HOSPITAL_DATE.Properties.Appearance.Options.UseFont = true;
            this.txtIN_HOSPITAL_DATE.Properties.Appearance.Options.UseForeColor = true;
            this.txtIN_HOSPITAL_DATE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtIN_HOSPITAL_DATE.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtIN_HOSPITAL_DATE.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.txtIN_HOSPITAL_DATE.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtIN_HOSPITAL_DATE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtIN_HOSPITAL_DATE.Size = new System.Drawing.Size(88, 26);
            this.txtIN_HOSPITAL_DATE.TabIndex = 385;
            // 
            // txtDIAGNOSE
            // 
            this.txtDIAGNOSE.Enabled = false;
            this.txtDIAGNOSE.EnterMoveNextControl = true;
            this.txtDIAGNOSE.Location = new System.Drawing.Point(71, 45);
            this.txtDIAGNOSE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDIAGNOSE.Name = "txtDIAGNOSE";
            this.txtDIAGNOSE.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDIAGNOSE.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtDIAGNOSE.Properties.Appearance.Options.UseFont = true;
            this.txtDIAGNOSE.Properties.Appearance.Options.UseForeColor = true;
            this.txtDIAGNOSE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtDIAGNOSE.Size = new System.Drawing.Size(249, 26);
            this.txtDIAGNOSE.TabIndex = 388;
            // 
            // lblIN_HOSPITAL_DATE
            // 
            this.lblIN_HOSPITAL_DATE.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIN_HOSPITAL_DATE.Location = new System.Drawing.Point(13, 11);
            this.lblIN_HOSPITAL_DATE.Name = "lblIN_HOSPITAL_DATE";
            this.lblIN_HOSPITAL_DATE.Size = new System.Drawing.Size(56, 20);
            this.lblIN_HOSPITAL_DATE.TabIndex = 391;
            this.lblIN_HOSPITAL_DATE.Text = "住院日期";
            // 
            // CtlBaseRecordDiagnose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl4);
            this.Controls.Add(this.panelControl3);
            this.Name = "CtlBaseRecordDiagnose";
            this.Size = new System.Drawing.Size(660, 435);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.panelControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLEAVE_HOSPITAL_DATE.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLEAVE_HOSPITAL_DATE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupCREATEBY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIN_HOSPITAL_DATE.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIN_HOSPITAL_DATE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDIAGNOSE.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraGrid.GridControl gcRecord;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRecord;
        private DevExpress.XtraGrid.Columns.GridColumn colIN_HOSPITAL_DATE;
        private DevExpress.XtraGrid.Columns.GridColumn colLEAVE_HOSPITAL_DATE;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.LookUpEdit lupCREATEBY;
        private DevExpress.XtraEditors.LabelControl lblCREATEBY;
        private DevExpress.XtraEditors.DateEdit txtIN_HOSPITAL_DATE;
        private DevExpress.XtraEditors.TextEdit txtDIAGNOSE;
        private DevExpress.XtraEditors.LabelControl lblIN_HOSPITAL_DATE;
        private DevExpress.XtraGrid.Columns.GridColumn colDIAGNOSE;
        private DevExpress.XtraGrid.Columns.GridColumn colCREATEBY;
        private DevExpress.XtraEditors.LabelControl lblDIAGNOSE;
        private DevExpress.XtraEditors.LabelControl lblLEAVE_HOSPITAL_DATE;
        private DevExpress.XtraEditors.DateEdit txtLEAVE_HOSPITAL_DATE;
    }
}
