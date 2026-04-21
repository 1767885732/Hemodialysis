namespace Hemo.Client.Controls
{
    partial class CtlBaseRecordEvent
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
            this.colCREATE_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDIALYSIS_STAGE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCOMPLICATION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCHRONIC_EVENT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCREATEBY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.lblCHRONIC_EVENT = new DevExpress.XtraEditors.LabelControl();
            this.txtCHRONIC_EVENT = new DevExpress.XtraEditors.TextEdit();
            this.lblCOMPLICATION = new DevExpress.XtraEditors.LabelControl();
            this.lupCREATEBY = new DevExpress.XtraEditors.LookUpEdit();
            this.lblCREATEBY = new DevExpress.XtraEditors.LabelControl();
            this.lblDIALYSIS_STAGE = new DevExpress.XtraEditors.LabelControl();
            this.lupDIALYSIS_STAGE = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtCREATE_DATE = new DevExpress.XtraEditors.DateEdit();
            this.txtCOMPLICATION = new DevExpress.XtraEditors.TextEdit();
            this.lblCREATE_DATE = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCHRONIC_EVENT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupCREATEBY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDIALYSIS_STAGE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCREATE_DATE.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCREATE_DATE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCOMPLICATION.Properties)).BeginInit();
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
            this.gcRecord.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.colCREATE_DATE,
            this.colDIALYSIS_STAGE,
            this.colCOMPLICATION,
            this.colCHRONIC_EVENT,
            this.colCREATEBY});
            this.gvRecord.GridControl = this.gcRecord;
            this.gvRecord.Name = "gvRecord";
            this.gvRecord.OptionsBehavior.Editable = false;
            this.gvRecord.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvRecord.OptionsView.ShowGroupPanel = false;
            this.gvRecord.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvRecord_RowClick);
            // 
            // colCREATE_DATE
            // 
            this.colCREATE_DATE.Caption = "日期";
            this.colCREATE_DATE.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.colCREATE_DATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colCREATE_DATE.FieldName = "CREATE_DATE";
            this.colCREATE_DATE.Name = "colCREATE_DATE";
            this.colCREATE_DATE.Visible = true;
            this.colCREATE_DATE.VisibleIndex = 0;
            // 
            // colDIALYSIS_STAGE
            // 
            this.colDIALYSIS_STAGE.Caption = "透析阶段";
            this.colDIALYSIS_STAGE.FieldName = "DIALYSIS_STAGE";
            this.colDIALYSIS_STAGE.Name = "colDIALYSIS_STAGE";
            this.colDIALYSIS_STAGE.Visible = true;
            this.colDIALYSIS_STAGE.VisibleIndex = 1;
            // 
            // colCOMPLICATION
            // 
            this.colCOMPLICATION.Caption = "并发症";
            this.colCOMPLICATION.FieldName = "COMPLICATION";
            this.colCOMPLICATION.Name = "colCOMPLICATION";
            this.colCOMPLICATION.Visible = true;
            this.colCOMPLICATION.VisibleIndex = 2;
            // 
            // colCHRONIC_EVENT
            // 
            this.colCHRONIC_EVENT.Caption = "慢性事件";
            this.colCHRONIC_EVENT.FieldName = "CHRONIC_EVENT";
            this.colCHRONIC_EVENT.Name = "colCHRONIC_EVENT";
            this.colCHRONIC_EVENT.Visible = true;
            this.colCHRONIC_EVENT.VisibleIndex = 3;
            // 
            // colCREATEBY
            // 
            this.colCREATEBY.Caption = "记录人";
            this.colCREATEBY.FieldName = "DOCTOR_NAME";
            this.colCREATEBY.Name = "colCREATEBY";
            this.colCREATEBY.Visible = true;
            this.colCREATEBY.VisibleIndex = 4;
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.lblCHRONIC_EVENT);
            this.panelControl4.Controls.Add(this.txtCHRONIC_EVENT);
            this.panelControl4.Controls.Add(this.lblCOMPLICATION);
            this.panelControl4.Controls.Add(this.lupCREATEBY);
            this.panelControl4.Controls.Add(this.lblCREATEBY);
            this.panelControl4.Controls.Add(this.lblDIALYSIS_STAGE);
            this.panelControl4.Controls.Add(this.lupDIALYSIS_STAGE);
            this.panelControl4.Controls.Add(this.txtCREATE_DATE);
            this.panelControl4.Controls.Add(this.txtCOMPLICATION);
            this.panelControl4.Controls.Add(this.lblCREATE_DATE);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl4.Location = new System.Drawing.Point(0, 358);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(660, 77);
            this.panelControl4.TabIndex = 4;
            // 
            // lblCHRONIC_EVENT
            // 
            this.lblCHRONIC_EVENT.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCHRONIC_EVENT.Location = new System.Drawing.Point(333, 48);
            this.lblCHRONIC_EVENT.Name = "lblCHRONIC_EVENT";
            this.lblCHRONIC_EVENT.Size = new System.Drawing.Size(56, 20);
            this.lblCHRONIC_EVENT.TabIndex = 401;
            this.lblCHRONIC_EVENT.Text = "慢性事件";
            // 
            // txtCHRONIC_EVENT
            // 
            this.txtCHRONIC_EVENT.Enabled = false;
            this.txtCHRONIC_EVENT.EnterMoveNextControl = true;
            this.txtCHRONIC_EVENT.Location = new System.Drawing.Point(393, 45);
            this.txtCHRONIC_EVENT.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCHRONIC_EVENT.Name = "txtCHRONIC_EVENT";
            this.txtCHRONIC_EVENT.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCHRONIC_EVENT.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtCHRONIC_EVENT.Properties.Appearance.Options.UseFont = true;
            this.txtCHRONIC_EVENT.Properties.Appearance.Options.UseForeColor = true;
            this.txtCHRONIC_EVENT.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtCHRONIC_EVENT.Size = new System.Drawing.Size(249, 26);
            this.txtCHRONIC_EVENT.TabIndex = 400;
            // 
            // lblCOMPLICATION
            // 
            this.lblCOMPLICATION.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCOMPLICATION.Location = new System.Drawing.Point(13, 48);
            this.lblCOMPLICATION.Name = "lblCOMPLICATION";
            this.lblCOMPLICATION.Size = new System.Drawing.Size(42, 20);
            this.lblCOMPLICATION.TabIndex = 399;
            this.lblCOMPLICATION.Text = "并发症";
            // 
            // lupCREATEBY
            // 
            this.lupCREATEBY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lupCREATEBY.Enabled = false;
            this.lupCREATEBY.EnterMoveNextControl = true;
            this.lupCREATEBY.Location = new System.Drawing.Point(392, 8);
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
            // lblDIALYSIS_STAGE
            // 
            this.lblDIALYSIS_STAGE.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDIALYSIS_STAGE.Location = new System.Drawing.Point(170, 11);
            this.lblDIALYSIS_STAGE.Name = "lblDIALYSIS_STAGE";
            this.lblDIALYSIS_STAGE.Size = new System.Drawing.Size(56, 20);
            this.lblDIALYSIS_STAGE.TabIndex = 394;
            this.lblDIALYSIS_STAGE.Text = "透析阶段";
            // 
            // lupDIALYSIS_STAGE
            // 
            this.lupDIALYSIS_STAGE.EditValue = "";
            this.lupDIALYSIS_STAGE.Enabled = false;
            this.lupDIALYSIS_STAGE.EnterMoveNextControl = true;
            this.lupDIALYSIS_STAGE.Location = new System.Drawing.Point(230, 8);
            this.lupDIALYSIS_STAGE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lupDIALYSIS_STAGE.Name = "lupDIALYSIS_STAGE";
            this.lupDIALYSIS_STAGE.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lupDIALYSIS_STAGE.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lupDIALYSIS_STAGE.Properties.Appearance.Options.UseFont = true;
            this.lupDIALYSIS_STAGE.Properties.Appearance.Options.UseForeColor = true;
            this.lupDIALYSIS_STAGE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDIALYSIS_STAGE.Properties.Items.AddRange(new object[] {
            "",
            "透析前",
            "透析中",
            "透析后"});
            this.lupDIALYSIS_STAGE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.lupDIALYSIS_STAGE.Properties.NullText = "[EditValue is null]";
            this.lupDIALYSIS_STAGE.Properties.PopupSizeable = true;
            this.lupDIALYSIS_STAGE.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.lupDIALYSIS_STAGE.Size = new System.Drawing.Size(92, 26);
            this.lupDIALYSIS_STAGE.TabIndex = 393;
            // 
            // txtCREATE_DATE
            // 
            this.txtCREATE_DATE.EditValue = null;
            this.txtCREATE_DATE.Enabled = false;
            this.txtCREATE_DATE.EnterMoveNextControl = true;
            this.txtCREATE_DATE.Location = new System.Drawing.Point(75, 8);
            this.txtCREATE_DATE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCREATE_DATE.Name = "txtCREATE_DATE";
            this.txtCREATE_DATE.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCREATE_DATE.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtCREATE_DATE.Properties.Appearance.Options.UseFont = true;
            this.txtCREATE_DATE.Properties.Appearance.Options.UseForeColor = true;
            this.txtCREATE_DATE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtCREATE_DATE.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCREATE_DATE.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.txtCREATE_DATE.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtCREATE_DATE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtCREATE_DATE.Size = new System.Drawing.Size(88, 26);
            this.txtCREATE_DATE.TabIndex = 385;
            // 
            // txtCOMPLICATION
            // 
            this.txtCOMPLICATION.Enabled = false;
            this.txtCOMPLICATION.EnterMoveNextControl = true;
            this.txtCOMPLICATION.Location = new System.Drawing.Point(74, 45);
            this.txtCOMPLICATION.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCOMPLICATION.Name = "txtCOMPLICATION";
            this.txtCOMPLICATION.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCOMPLICATION.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtCOMPLICATION.Properties.Appearance.Options.UseFont = true;
            this.txtCOMPLICATION.Properties.Appearance.Options.UseForeColor = true;
            this.txtCOMPLICATION.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtCOMPLICATION.Size = new System.Drawing.Size(248, 26);
            this.txtCOMPLICATION.TabIndex = 388;
            // 
            // lblCREATE_DATE
            // 
            this.lblCREATE_DATE.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCREATE_DATE.Location = new System.Drawing.Point(13, 11);
            this.lblCREATE_DATE.Name = "lblCREATE_DATE";
            this.lblCREATE_DATE.Size = new System.Drawing.Size(56, 20);
            this.lblCREATE_DATE.TabIndex = 391;
            this.lblCREATE_DATE.Text = "录入日期";
            // 
            // CtlBaseRecordEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl4);
            this.Controls.Add(this.panelControl3);
            this.Name = "CtlBaseRecordEvent";
            this.Size = new System.Drawing.Size(660, 435);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.panelControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCHRONIC_EVENT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupCREATEBY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDIALYSIS_STAGE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCREATE_DATE.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCREATE_DATE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCOMPLICATION.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraGrid.GridControl gcRecord;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRecord;
        private DevExpress.XtraGrid.Columns.GridColumn colCREATE_DATE;
        private DevExpress.XtraGrid.Columns.GridColumn colDIALYSIS_STAGE;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.LookUpEdit lupCREATEBY;
        private DevExpress.XtraEditors.LabelControl lblCREATEBY;
        private DevExpress.XtraEditors.LabelControl lblDIALYSIS_STAGE;
        private DevExpress.XtraEditors.ComboBoxEdit lupDIALYSIS_STAGE;
        private DevExpress.XtraEditors.DateEdit txtCREATE_DATE;
        private DevExpress.XtraEditors.TextEdit txtCOMPLICATION;
        private DevExpress.XtraEditors.LabelControl lblCREATE_DATE;
        private DevExpress.XtraGrid.Columns.GridColumn colCOMPLICATION;
        private DevExpress.XtraGrid.Columns.GridColumn colCHRONIC_EVENT;
        private DevExpress.XtraGrid.Columns.GridColumn colCREATEBY;
        private DevExpress.XtraEditors.LabelControl lblCOMPLICATION;
        private DevExpress.XtraEditors.LabelControl lblCHRONIC_EVENT;
        private DevExpress.XtraEditors.TextEdit txtCHRONIC_EVENT;
    }
}
