namespace Hemo.Client.Controls
{
    partial class CtlDrugUseRecord
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
            this.colDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRecord = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.lopDoctorId = new DevExpress.XtraEditors.LookUpEdit();
            this.lblDoctorId = new DevExpress.XtraEditors.LabelControl();
            this.lblDrugTimeType = new DevExpress.XtraEditors.LabelControl();
            this.lupDrugTimeType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtDrugName = new Hemo.Utilities.CustomGridLookUpEdit();
            this.customGridLookUpEdit2View = new Hemo.Utilities.CustomGridView();
            this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbCreateTime = new DevExpress.XtraEditors.TimeEdit();
            this.txtCreateDate = new DevExpress.XtraEditors.DateEdit();
            this.cmbDrugMode = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbDosageUnits = new DevExpress.XtraEditors.LookUpEdit();
            this.txtDosage = new DevExpress.XtraEditors.TextEdit();
            this.lblDrugName = new DevExpress.XtraEditors.LabelControl();
            this.lblCreateDate = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lopDoctorId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDrugTimeType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDrugName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCreateTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDrugMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDosageUnits.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDosage.Properties)).BeginInit();
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
            this.gvRecord.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDate,
            this.colRecord});
            this.gvRecord.GridControl = this.gcRecord;
            this.gvRecord.Name = "gvRecord";
            this.gvRecord.OptionsView.ShowGroupPanel = false;
            // 
            // colDate
            // 
            this.colDate.Caption = "日期";
            this.colDate.FieldName = "CREATE_DATE";
            this.colDate.Name = "colDate";
            this.colDate.Visible = true;
            this.colDate.VisibleIndex = 0;
            this.colDate.Width = 65;
            // 
            // colRecord
            // 
            this.colRecord.Caption = "项目记录";
            this.colRecord.FieldName = "DRUG_RECORD";
            this.colRecord.Name = "colRecord";
            this.colRecord.Visible = true;
            this.colRecord.VisibleIndex = 1;
            this.colRecord.Width = 300;
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.lopDoctorId);
            this.panelControl4.Controls.Add(this.lblDoctorId);
            this.panelControl4.Controls.Add(this.lblDrugTimeType);
            this.panelControl4.Controls.Add(this.lupDrugTimeType);
            this.panelControl4.Controls.Add(this.txtDrugName);
            this.panelControl4.Controls.Add(this.cmbCreateTime);
            this.panelControl4.Controls.Add(this.txtCreateDate);
            this.panelControl4.Controls.Add(this.cmbDrugMode);
            this.panelControl4.Controls.Add(this.cmbDosageUnits);
            this.panelControl4.Controls.Add(this.txtDosage);
            this.panelControl4.Controls.Add(this.lblDrugName);
            this.panelControl4.Controls.Add(this.lblCreateDate);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl4.Location = new System.Drawing.Point(0, 358);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(660, 77);
            this.panelControl4.TabIndex = 4;
            // 
            // lopDoctorId
            // 
            this.lopDoctorId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lopDoctorId.EnterMoveNextControl = true;
            this.lopDoctorId.Location = new System.Drawing.Point(523, 45);
            this.lopDoctorId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lopDoctorId.Name = "lopDoctorId";
            this.lopDoctorId.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lopDoctorId.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lopDoctorId.Properties.Appearance.Options.UseFont = true;
            this.lopDoctorId.Properties.Appearance.Options.UseForeColor = true;
            this.lopDoctorId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lopDoctorId.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.lopDoctorId.Size = new System.Drawing.Size(92, 23);
            this.lopDoctorId.TabIndex = 397;
            // 
            // lblDoctorId
            // 
            this.lblDoctorId.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDoctorId.Appearance.Options.UseFont = true;
            this.lblDoctorId.Location = new System.Drawing.Point(459, 48);
            this.lblDoctorId.Name = "lblDoctorId";
            this.lblDoctorId.Size = new System.Drawing.Size(48, 17);
            this.lblDoctorId.TabIndex = 398;
            this.lblDoctorId.Text = "开药医生";
            // 
            // lblDrugTimeType
            // 
            this.lblDrugTimeType.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDrugTimeType.Appearance.Options.UseFont = true;
            this.lblDrugTimeType.Location = new System.Drawing.Point(273, 48);
            this.lblDrugTimeType.Name = "lblDrugTimeType";
            this.lblDrugTimeType.Size = new System.Drawing.Size(48, 17);
            this.lblDrugTimeType.TabIndex = 394;
            this.lblDrugTimeType.Text = "给药阶段";
            // 
            // lupDrugTimeType
            // 
            this.lupDrugTimeType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lupDrugTimeType.EditValue = "透析前";
            this.lupDrugTimeType.EnterMoveNextControl = true;
            this.lupDrugTimeType.Location = new System.Drawing.Point(327, 45);
            this.lupDrugTimeType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lupDrugTimeType.Name = "lupDrugTimeType";
            this.lupDrugTimeType.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lupDrugTimeType.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lupDrugTimeType.Properties.Appearance.Options.UseFont = true;
            this.lupDrugTimeType.Properties.Appearance.Options.UseForeColor = true;
            this.lupDrugTimeType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDrugTimeType.Properties.Items.AddRange(new object[] {
            "透析前",
            "透析中",
            "透析后"});
            this.lupDrugTimeType.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.lupDrugTimeType.Properties.NullText = "[EditValue is null]";
            this.lupDrugTimeType.Properties.PopupSizeable = true;
            this.lupDrugTimeType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.lupDrugTimeType.Size = new System.Drawing.Size(92, 23);
            this.lupDrugTimeType.TabIndex = 393;
            // 
            // txtDrugName
            // 
            this.txtDrugName.EnterMoveNextControl = true;
            this.txtDrugName.Location = new System.Drawing.Point(327, 9);
            this.txtDrugName.Name = "txtDrugName";
            this.txtDrugName.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtDrugName.Properties.Appearance.Options.UseForeColor = true;
            this.txtDrugName.Properties.AutoComplete = false;
            this.txtDrugName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDrugName.Properties.DisplayMember = "DRUG_NAME";
            this.txtDrugName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.txtDrugName.Properties.ValueMember = "DRUG_CODE";
            this.txtDrugName.Properties.View = this.customGridLookUpEdit2View;
            this.txtDrugName.Size = new System.Drawing.Size(180, 21);
            this.txtDrugName.TabIndex = 387;
            // 
            // customGridLookUpEdit2View
            // 
            this.customGridLookUpEdit2View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn28,
            this.gridColumn30,
            this.gridColumn31});
            this.customGridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.customGridLookUpEdit2View.Name = "customGridLookUpEdit2View";
            this.customGridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.customGridLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn28
            // 
            this.gridColumn28.Caption = "编号";
            this.gridColumn28.FieldName = "DRUG_CODE";
            this.gridColumn28.Name = "gridColumn28";
            this.gridColumn28.Visible = true;
            this.gridColumn28.VisibleIndex = 0;
            this.gridColumn28.Width = 95;
            // 
            // gridColumn30
            // 
            this.gridColumn30.Caption = "名称";
            this.gridColumn30.FieldName = "DRUG_NAME";
            this.gridColumn30.Name = "gridColumn30";
            this.gridColumn30.Visible = true;
            this.gridColumn30.VisibleIndex = 1;
            this.gridColumn30.Width = 150;
            // 
            // gridColumn31
            // 
            this.gridColumn31.Caption = "拼音码";
            this.gridColumn31.FieldName = "DRUG_PINYIN";
            this.gridColumn31.Name = "gridColumn31";
            this.gridColumn31.Visible = true;
            this.gridColumn31.VisibleIndex = 2;
            this.gridColumn31.Width = 67;
            // 
            // cmbCreateTime
            // 
            this.cmbCreateTime.EditValue = new System.DateTime(2013, 4, 8, 0, 0, 0, 0);
            this.cmbCreateTime.Enabled = false;
            this.cmbCreateTime.EnterMoveNextControl = true;
            this.cmbCreateTime.Location = new System.Drawing.Point(170, 9);
            this.cmbCreateTime.Name = "cmbCreateTime";
            this.cmbCreateTime.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.cmbCreateTime.Properties.Appearance.Options.UseForeColor = true;
            this.cmbCreateTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbCreateTime.Size = new System.Drawing.Size(88, 21);
            this.cmbCreateTime.TabIndex = 386;
            // 
            // txtCreateDate
            // 
            this.txtCreateDate.EditValue = null;
            this.txtCreateDate.EnterMoveNextControl = true;
            this.txtCreateDate.Location = new System.Drawing.Point(67, 8);
            this.txtCreateDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCreateDate.Name = "txtCreateDate";
            this.txtCreateDate.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCreateDate.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtCreateDate.Properties.Appearance.Options.UseFont = true;
            this.txtCreateDate.Properties.Appearance.Options.UseForeColor = true;
            this.txtCreateDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtCreateDate.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtCreateDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCreateDate.Size = new System.Drawing.Size(88, 23);
            this.txtCreateDate.TabIndex = 385;
            // 
            // cmbDrugMode
            // 
            this.cmbDrugMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDrugMode.EnterMoveNextControl = true;
            this.cmbDrugMode.Location = new System.Drawing.Point(170, 45);
            this.cmbDrugMode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbDrugMode.Name = "cmbDrugMode";
            this.cmbDrugMode.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cmbDrugMode.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.cmbDrugMode.Properties.Appearance.Options.UseFont = true;
            this.cmbDrugMode.Properties.Appearance.Options.UseForeColor = true;
            this.cmbDrugMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDrugMode.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cmbDrugMode.Size = new System.Drawing.Size(92, 23);
            this.cmbDrugMode.TabIndex = 390;
            // 
            // cmbDosageUnits
            // 
            this.cmbDosageUnits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDosageUnits.EnterMoveNextControl = true;
            this.cmbDosageUnits.Location = new System.Drawing.Point(67, 45);
            this.cmbDosageUnits.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbDosageUnits.Name = "cmbDosageUnits";
            this.cmbDosageUnits.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cmbDosageUnits.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.cmbDosageUnits.Properties.Appearance.Options.UseFont = true;
            this.cmbDosageUnits.Properties.Appearance.Options.UseForeColor = true;
            this.cmbDosageUnits.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDosageUnits.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cmbDosageUnits.Size = new System.Drawing.Size(92, 23);
            this.cmbDosageUnits.TabIndex = 389;
            // 
            // txtDosage
            // 
            this.txtDosage.EnterMoveNextControl = true;
            this.txtDosage.Location = new System.Drawing.Point(523, 8);
            this.txtDosage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDosage.Name = "txtDosage";
            this.txtDosage.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtDosage.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtDosage.Properties.Appearance.Options.UseFont = true;
            this.txtDosage.Properties.Appearance.Options.UseForeColor = true;
            this.txtDosage.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtDosage.Size = new System.Drawing.Size(88, 23);
            this.txtDosage.TabIndex = 388;
            // 
            // lblDrugName
            // 
            this.lblDrugName.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblDrugName.Appearance.Options.UseFont = true;
            this.lblDrugName.Location = new System.Drawing.Point(273, 11);
            this.lblDrugName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblDrugName.Name = "lblDrugName";
            this.lblDrugName.Size = new System.Drawing.Size(48, 17);
            this.lblDrugName.TabIndex = 392;
            this.lblDrugName.Text = "项目名称";
            // 
            // lblCreateDate
            // 
            this.lblCreateDate.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateDate.Appearance.Options.UseFont = true;
            this.lblCreateDate.Location = new System.Drawing.Point(13, 11);
            this.lblCreateDate.Name = "lblCreateDate";
            this.lblCreateDate.Size = new System.Drawing.Size(48, 17);
            this.lblCreateDate.TabIndex = 391;
            this.lblCreateDate.Text = "录入时间";
            // 
            // CtlDrugUseRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl4);
            this.Controls.Add(this.panelControl3);
            this.Name = "CtlDrugUseRecord";
            this.Size = new System.Drawing.Size(660, 435);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.panelControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lopDoctorId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDrugTimeType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDrugName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCreateTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDrugMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDosageUnits.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDosage.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraGrid.GridControl gcRecord;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRecord;
        private DevExpress.XtraGrid.Columns.GridColumn colDate;
        private DevExpress.XtraGrid.Columns.GridColumn colRecord;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.LookUpEdit lopDoctorId;
        private DevExpress.XtraEditors.LabelControl lblDoctorId;
        private DevExpress.XtraEditors.LabelControl lblDrugTimeType;
        private DevExpress.XtraEditors.ComboBoxEdit lupDrugTimeType;
        private Utilities.CustomGridLookUpEdit txtDrugName;
        private Utilities.CustomGridView customGridLookUpEdit2View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn28;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn30;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn31;
        private DevExpress.XtraEditors.TimeEdit cmbCreateTime;
        private DevExpress.XtraEditors.DateEdit txtCreateDate;
        private DevExpress.XtraEditors.LookUpEdit cmbDrugMode;
        private DevExpress.XtraEditors.LookUpEdit cmbDosageUnits;
        private DevExpress.XtraEditors.TextEdit txtDosage;
        private DevExpress.XtraEditors.LabelControl lblDrugName;
        private DevExpress.XtraEditors.LabelControl lblCreateDate;
    }
}
