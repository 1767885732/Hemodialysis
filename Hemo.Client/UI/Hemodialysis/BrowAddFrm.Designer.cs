namespace Hemo.Client.UI.Hemodialysis
{
    partial class BrowAddFrm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnQuery = new Hemo.Client.Controls.DXSimpleButton();
            this.txtBorrowDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnAdd = new Hemo.Client.Controls.DXSimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.busyIndicator1 = new Hemo.Client.Controls.BusyIndicator();
            this.cmbDOSAGE_UNITS = new DevExpress.XtraEditors.LookUpEdit();
            this.spnDRUG_TIMES = new DevExpress.XtraEditors.SpinEdit();
            this.gridLookPatient = new Hemo.Utilities.CustomGridLookUpEdit();
            this.customGridView2 = new Hemo.Utilities.CustomGridView();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl103 = new DevExpress.XtraEditors.LabelControl();
            this.lupCHECK_NURSE = new DevExpress.XtraEditors.LookUpEdit();
            this.txtDRUG_NAME = new Hemo.Utilities.CustomGridLookUpEdit();
            this.customGridLookUpEdit2View = new Hemo.Utilities.CustomGridView();
            this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.txtBorrowDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBorrowDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDOSAGE_UNITS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnDRUG_TIMES.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookPatient.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupCHECK_NURSE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDRUG_NAME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridLookUpEdit2View)).BeginInit();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.ImageIndex = 7;
            this.btnQuery.Location = new System.Drawing.Point(58, 224);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 25);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "保存(&S)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtBorrowDate
            // 
            this.txtBorrowDate.EditValue = null;
            this.txtBorrowDate.EnterMoveNextControl = true;
            this.txtBorrowDate.Location = new System.Drawing.Point(75, 54);
            this.txtBorrowDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBorrowDate.Name = "txtBorrowDate";
            this.txtBorrowDate.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtBorrowDate.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtBorrowDate.Properties.Appearance.Options.UseFont = true;
            this.txtBorrowDate.Properties.Appearance.Options.UseForeColor = true;
            this.txtBorrowDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtBorrowDate.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtBorrowDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtBorrowDate.Size = new System.Drawing.Size(207, 23);
            this.txtBorrowDate.TabIndex = 575;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 58);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 14);
            this.labelControl1.TabIndex = 576;
            this.labelControl1.Text = "借药日期:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 14);
            this.labelControl2.TabIndex = 578;
            this.labelControl2.Text = "患者姓名:";
            // 
            // btnAdd
            // 
            this.btnAdd.ImageIndex = 3;
            this.btnAdd.Location = new System.Drawing.Point(167, 224);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 25);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "退出(&C)";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 102);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 14);
            this.labelControl3.TabIndex = 578;
            this.labelControl3.Text = "药品名称:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 149);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(52, 14);
            this.labelControl4.TabIndex = 582;
            this.labelControl4.Text = "药品数量:";
            // 
            // busyIndicator1
            // 
            this.busyIndicator1.Location = new System.Drawing.Point(105, 84);
            this.busyIndicator1.Name = "busyIndicator1";
            this.busyIndicator1.Size = new System.Drawing.Size(100, 40);
            this.busyIndicator1.TabIndex = 583;
            // 
            // cmbDOSAGE_UNITS
            // 
            this.cmbDOSAGE_UNITS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDOSAGE_UNITS.EnterMoveNextControl = true;
            this.cmbDOSAGE_UNITS.Location = new System.Drawing.Point(211, 146);
            this.cmbDOSAGE_UNITS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbDOSAGE_UNITS.Name = "cmbDOSAGE_UNITS";
            this.cmbDOSAGE_UNITS.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cmbDOSAGE_UNITS.Properties.Appearance.Options.UseFont = true;
            this.cmbDOSAGE_UNITS.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDOSAGE_UNITS.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cmbDOSAGE_UNITS.Properties.NullText = "";
            this.cmbDOSAGE_UNITS.Size = new System.Drawing.Size(77, 23);
            this.cmbDOSAGE_UNITS.TabIndex = 585;
            // 
            // spnDRUG_TIMES
            // 
            this.spnDRUG_TIMES.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spnDRUG_TIMES.EnterMoveNextControl = true;
            this.spnDRUG_TIMES.Location = new System.Drawing.Point(75, 146);
            this.spnDRUG_TIMES.Name = "spnDRUG_TIMES";
            this.spnDRUG_TIMES.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.spnDRUG_TIMES.Properties.Appearance.Options.UseFont = true;
            this.spnDRUG_TIMES.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spnDRUG_TIMES.Size = new System.Drawing.Size(130, 23);
            this.spnDRUG_TIMES.TabIndex = 586;
            // 
            // gridLookPatient
            // 
            this.gridLookPatient.EnterMoveNextControl = true;
            this.gridLookPatient.Location = new System.Drawing.Point(75, 12);
            this.gridLookPatient.Name = "gridLookPatient";
            this.gridLookPatient.Properties.AutoComplete = false;
            this.gridLookPatient.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gridLookPatient.Properties.DisplayMember = "NAME";
            this.gridLookPatient.Properties.NullText = "";
            this.gridLookPatient.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.gridLookPatient.Properties.ValueMember = "HEMODIALYSIS_ID";
            this.gridLookPatient.Properties.View = this.customGridView2;
            this.gridLookPatient.Size = new System.Drawing.Size(207, 21);
            this.gridLookPatient.TabIndex = 587;
            // 
            // customGridView2
            // 
            this.customGridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17});
            this.customGridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.customGridView2.Name = "customGridView2";
            this.customGridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.customGridView2.OptionsView.ColumnAutoWidth = false;
            this.customGridView2.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn12.Caption = "透析号";
            this.gridColumn12.FieldName = "HEMODIALYSIS_ID";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.OptionsColumn.ReadOnly = true;
            this.gridColumn12.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn12.OptionsFilter.AllowFilter = false;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 0;
            this.gridColumn12.Width = 124;
            // 
            // gridColumn13
            // 
            this.gridColumn13.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn13.Caption = "姓名";
            this.gridColumn13.FieldName = "NAME";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.OptionsColumn.ReadOnly = true;
            this.gridColumn13.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn13.OptionsFilter.AllowFilter = false;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 1;
            this.gridColumn13.Width = 90;
            // 
            // gridColumn14
            // 
            this.gridColumn14.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn14.Caption = "输入码";
            this.gridColumn14.FieldName = "INPUT_CODE";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.OptionsColumn.ReadOnly = true;
            this.gridColumn14.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn14.OptionsFilter.AllowFilter = false;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 2;
            this.gridColumn14.Width = 86;
            // 
            // gridColumn15
            // 
            this.gridColumn15.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn15.Caption = "性别";
            this.gridColumn15.FieldName = "SEX";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = false;
            this.gridColumn15.OptionsColumn.ReadOnly = true;
            this.gridColumn15.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn15.OptionsFilter.AllowFilter = false;
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 3;
            this.gridColumn15.Width = 54;
            // 
            // gridColumn16
            // 
            this.gridColumn16.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn16.Caption = "证件类型";
            this.gridColumn16.FieldName = "CREDENTIALS_TYPE";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsColumn.AllowEdit = false;
            this.gridColumn16.OptionsColumn.ReadOnly = true;
            this.gridColumn16.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn16.OptionsFilter.AllowFilter = false;
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 4;
            this.gridColumn16.Width = 96;
            // 
            // gridColumn17
            // 
            this.gridColumn17.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn17.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn17.Caption = "号码";
            this.gridColumn17.FieldName = "CREDENTIALS_NUMBER";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.OptionsColumn.AllowEdit = false;
            this.gridColumn17.OptionsColumn.ReadOnly = true;
            this.gridColumn17.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn17.OptionsFilter.AllowFilter = false;
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 5;
            this.gridColumn17.Width = 149;
            // 
            // labelControl103
            // 
            this.labelControl103.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl103.Appearance.Options.UseFont = true;
            this.labelControl103.Location = new System.Drawing.Point(16, 187);
            this.labelControl103.Name = "labelControl103";
            this.labelControl103.Size = new System.Drawing.Size(39, 17);
            this.labelControl103.TabIndex = 798;
            this.labelControl103.Text = "借出人:";
            // 
            // lupCHECK_NURSE
            // 
            this.lupCHECK_NURSE.EnterMoveNextControl = true;
            this.lupCHECK_NURSE.Location = new System.Drawing.Point(75, 184);
            this.lupCHECK_NURSE.Name = "lupCHECK_NURSE";
            this.lupCHECK_NURSE.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lupCHECK_NURSE.Properties.Appearance.Options.UseFont = true;
            this.lupCHECK_NURSE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupCHECK_NURSE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.lupCHECK_NURSE.Properties.NullText = "";
            this.lupCHECK_NURSE.Size = new System.Drawing.Size(95, 23);
            this.lupCHECK_NURSE.TabIndex = 797;
            // 
            // txtDRUG_NAME
            // 
            this.txtDRUG_NAME.EditValue = "";
            this.txtDRUG_NAME.EnterMoveNextControl = true;
            this.txtDRUG_NAME.Location = new System.Drawing.Point(75, 99);
            this.txtDRUG_NAME.Name = "txtDRUG_NAME";
            this.txtDRUG_NAME.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtDRUG_NAME.Properties.Appearance.Options.UseForeColor = true;
            this.txtDRUG_NAME.Properties.AutoComplete = false;
            this.txtDRUG_NAME.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDRUG_NAME.Properties.DisplayMember = "DRUG_NAME";
            this.txtDRUG_NAME.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.txtDRUG_NAME.Properties.ValueMember = "DRUG_CODE";
            this.txtDRUG_NAME.Properties.View = this.customGridLookUpEdit2View;
            this.txtDRUG_NAME.Size = new System.Drawing.Size(207, 21);
            this.txtDRUG_NAME.TabIndex = 799;
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
            // BrowAddFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 259);
            this.Controls.Add(this.busyIndicator1);
            this.Controls.Add(this.txtDRUG_NAME);
            this.Controls.Add(this.labelControl103);
            this.Controls.Add(this.lupCHECK_NURSE);
            this.Controls.Add(this.gridLookPatient);
            this.Controls.Add(this.spnDRUG_TIMES);
            this.Controls.Add(this.cmbDOSAGE_UNITS);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtBorrowDate);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.btnAdd);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(324, 298);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(324, 297);
            this.Name = "BrowAddFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "患者借还药管理";
            this.Load += new System.EventHandler(this.ShowSummary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtBorrowDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBorrowDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDOSAGE_UNITS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnDRUG_TIMES.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookPatient.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupCHECK_NURSE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDRUG_NAME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridLookUpEdit2View)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Hemo.Client.Controls.DXSimpleButton btnQuery;
        private DevExpress.XtraEditors.DateEdit txtBorrowDate;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private Hemo.Client.Controls.DXSimpleButton btnAdd;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private Controls.BusyIndicator busyIndicator1;
        private DevExpress.XtraEditors.LookUpEdit cmbDOSAGE_UNITS;
        private DevExpress.XtraEditors.SpinEdit spnDRUG_TIMES;
        private Utilities.CustomGridLookUpEdit gridLookPatient;
        private Utilities.CustomGridView customGridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraEditors.LabelControl labelControl103;
        private DevExpress.XtraEditors.LookUpEdit lupCHECK_NURSE;
        private Utilities.CustomGridLookUpEdit txtDRUG_NAME;
        private Utilities.CustomGridView customGridLookUpEdit2View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn28;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn30;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn31;
    }
}