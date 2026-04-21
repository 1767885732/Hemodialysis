namespace Hemo.Client.UI.Material {
    partial class EditMaterialMaster {
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
            this.txtCREATE_DATE = new DevExpress.XtraEditors.DateEdit();
            this.MED_MATERIAL_MASTER = new System.Windows.Forms.BindingSource(this.components);
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtMATERIAL_PINYIN = new DevExpress.XtraEditors.TextEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txtSYSTEM_BARCODE = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.pnlControls = new DevExpress.XtraEditors.PanelControl();
            this.spiPRICE = new DevExpress.XtraEditors.SpinEdit();
            this.cbxUNITS = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtFIRM_NAME = new Hemo.Utilities.CustomGridLookUpEdit();
            this.customGridView1 = new Hemo.Utilities.CustomGridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.customGridLookUpEdit_Type = new Hemo.Utilities.CustomGridLookUpEdit();
            this.customGridLookUpEdit1View = new Hemo.Utilities.CustomGridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtMATERIAL_SPEC = new DevExpress.XtraEditors.TextEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.txtMEMO = new DevExpress.XtraEditors.MemoEdit();
            this.txtPACK_BARCODE = new DevExpress.XtraEditors.TextEdit();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.txtVALID_DATE = new DevExpress.XtraEditors.DateEdit();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtMATERIAL_ID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtMATERIAL_NAME = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new Hemo.Client.Controls.DXSimpleButton();
            this.btnAdd = new Hemo.Client.Controls.DXSimpleButton();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.txtCREATE_DATE.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCREATE_DATE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MED_MATERIAL_MASTER)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMATERIAL_PINYIN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSYSTEM_BARCODE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlControls)).BeginInit();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spiPRICE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxUNITS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFIRM_NAME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridLookUpEdit_Type.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMATERIAL_SPEC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMEMO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPACK_BARCODE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVALID_DATE.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVALID_DATE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMATERIAL_ID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMATERIAL_NAME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCREATE_DATE
            // 
            this.txtCREATE_DATE.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.MED_MATERIAL_MASTER, "CREATE_DATE", true));
            this.txtCREATE_DATE.EditValue = null;
            this.txtCREATE_DATE.EnterMoveNextControl = true;
            this.txtCREATE_DATE.Location = new System.Drawing.Point(295, 13);
            this.txtCREATE_DATE.Name = "txtCREATE_DATE";
            this.txtCREATE_DATE.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCREATE_DATE.Properties.Appearance.Options.UseBackColor = true;
            this.txtCREATE_DATE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtCREATE_DATE.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCREATE_DATE.Size = new System.Drawing.Size(130, 20);
            this.txtCREATE_DATE.TabIndex = 2;
            // 
            // MED_MATERIAL_MASTER
            // 
            this.MED_MATERIAL_MASTER.AllowNew = true;
            this.MED_MATERIAL_MASTER.DataSource = typeof(Hemo.Model.MaterialModel.MED_MATERIAL_MASTERRow);
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(238, 14);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(48, 14);
            this.labelControl10.TabIndex = 27;
            this.labelControl10.Text = "录入时间";
            // 
            // txtMATERIAL_PINYIN
            // 
            this.txtMATERIAL_PINYIN.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MED_MATERIAL_MASTER, "MATERIAL_PINYIN", true));
            this.txtMATERIAL_PINYIN.Enabled = false;
            this.txtMATERIAL_PINYIN.EnterMoveNextControl = true;
            this.txtMATERIAL_PINYIN.Location = new System.Drawing.Point(295, 59);
            this.txtMATERIAL_PINYIN.Name = "txtMATERIAL_PINYIN";
            this.txtMATERIAL_PINYIN.Properties.MaxLength = 10;
            this.txtMATERIAL_PINYIN.Size = new System.Drawing.Size(130, 20);
            this.txtMATERIAL_PINYIN.TabIndex = 4;
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(239, 62);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(36, 14);
            this.labelControl11.TabIndex = 26;
            this.labelControl11.Text = "拼音码";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(32, 253);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(24, 14);
            this.labelControl9.TabIndex = 23;
            this.labelControl9.Text = "备注";
            // 
            // txtSYSTEM_BARCODE
            // 
            this.txtSYSTEM_BARCODE.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MED_MATERIAL_MASTER, "SYSTEM_BARCODE", true));
            this.txtSYSTEM_BARCODE.EnterMoveNextControl = true;
            this.txtSYSTEM_BARCODE.Location = new System.Drawing.Point(310, 172);
            this.txtSYSTEM_BARCODE.Name = "txtSYSTEM_BARCODE";
            this.txtSYSTEM_BARCODE.Properties.MaxLength = 20;
            this.txtSYSTEM_BARCODE.Size = new System.Drawing.Size(124, 20);
            this.txtSYSTEM_BARCODE.TabIndex = 9;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(254, 173);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(48, 14);
            this.labelControl8.TabIndex = 18;
            this.labelControl8.Text = "系统条码";
            // 
            // pnlControls
            // 
            this.pnlControls.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlControls.Controls.Add(this.spiPRICE);
            this.pnlControls.Controls.Add(this.cbxUNITS);
            this.pnlControls.Controls.Add(this.labelControl4);
            this.pnlControls.Controls.Add(this.labelControl7);
            this.pnlControls.Controls.Add(this.txtFIRM_NAME);
            this.pnlControls.Controls.Add(this.customGridLookUpEdit_Type);
            this.pnlControls.Controls.Add(this.txtMATERIAL_SPEC);
            this.pnlControls.Controls.Add(this.labelControl17);
            this.pnlControls.Controls.Add(this.txtMEMO);
            this.pnlControls.Controls.Add(this.txtPACK_BARCODE);
            this.pnlControls.Controls.Add(this.labelControl15);
            this.pnlControls.Controls.Add(this.txtVALID_DATE);
            this.pnlControls.Controls.Add(this.labelControl14);
            this.pnlControls.Controls.Add(this.txtCREATE_DATE);
            this.pnlControls.Controls.Add(this.labelControl10);
            this.pnlControls.Controls.Add(this.txtMATERIAL_PINYIN);
            this.pnlControls.Controls.Add(this.labelControl11);
            this.pnlControls.Controls.Add(this.labelControl9);
            this.pnlControls.Controls.Add(this.txtSYSTEM_BARCODE);
            this.pnlControls.Controls.Add(this.labelControl8);
            this.pnlControls.Controls.Add(this.labelControl5);
            this.pnlControls.Controls.Add(this.txtMATERIAL_ID);
            this.pnlControls.Controls.Add(this.labelControl3);
            this.pnlControls.Controls.Add(this.txtMATERIAL_NAME);
            this.pnlControls.Controls.Add(this.labelControl2);
            this.pnlControls.Controls.Add(this.labelControl1);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControls.Location = new System.Drawing.Point(0, 0);
            this.pnlControls.Margin = new System.Windows.Forms.Padding(5);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(456, 377);
            this.pnlControls.TabIndex = 28;
            // 
            // spiPRICE
            // 
            this.spiPRICE.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.MED_MATERIAL_MASTER, "PRICE", true));
            this.spiPRICE.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spiPRICE.EnterMoveNextControl = true;
            this.spiPRICE.Location = new System.Drawing.Point(374, 102);
            this.spiPRICE.Name = "spiPRICE";
            this.spiPRICE.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.spiPRICE.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.spiPRICE.Properties.Appearance.Options.UseBackColor = true;
            this.spiPRICE.Properties.Appearance.Options.UseForeColor = true;
            this.spiPRICE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spiPRICE.Properties.Mask.EditMask = "c2";
            this.spiPRICE.Size = new System.Drawing.Size(70, 20);
            this.spiPRICE.TabIndex = 274;
            // 
            // cbxUNITS
            // 
            this.cbxUNITS.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.MED_MATERIAL_MASTER, "UNIT", true));
            this.cbxUNITS.EditValue = "";
            this.cbxUNITS.EnterMoveNextControl = true;
            this.cbxUNITS.Location = new System.Drawing.Point(278, 101);
            this.cbxUNITS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbxUNITS.Name = "cbxUNITS";
            this.cbxUNITS.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cbxUNITS.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cbxUNITS.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.cbxUNITS.Properties.Appearance.Options.UseBackColor = true;
            this.cbxUNITS.Properties.Appearance.Options.UseFont = true;
            this.cbxUNITS.Properties.Appearance.Options.UseForeColor = true;
            this.cbxUNITS.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxUNITS.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ITEM_ID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ITEM_NAME", "名称")});
            this.cbxUNITS.Properties.DisplayMember = "ITEM_NAME";
            this.cbxUNITS.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxUNITS.Properties.NullText = "";
            this.cbxUNITS.Properties.ValueMember = "ITEM_ID";
            this.cbxUNITS.Size = new System.Drawing.Size(59, 24);
            this.cbxUNITS.TabIndex = 272;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(343, 105);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(24, 14);
            this.labelControl4.TabIndex = 273;
            this.labelControl4.Text = "价格";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(239, 105);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(24, 14);
            this.labelControl7.TabIndex = 273;
            this.labelControl7.Text = "单位";
            // 
            // txtFIRM_NAME
            // 
            this.txtFIRM_NAME.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MED_MATERIAL_MASTER, "FIRM_NAME", true));
            this.txtFIRM_NAME.EditValue = "";
            this.txtFIRM_NAME.EnterMoveNextControl = true;
            this.txtFIRM_NAME.Location = new System.Drawing.Point(76, 134);
            this.txtFIRM_NAME.Name = "txtFIRM_NAME";
            this.txtFIRM_NAME.Properties.AutoComplete = false;
            this.txtFIRM_NAME.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFIRM_NAME.Properties.DisplayMember = "FIRM_NAME";
            this.txtFIRM_NAME.Properties.NullText = "";
            this.txtFIRM_NAME.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.txtFIRM_NAME.Properties.ValueMember = "FIRM_NAME";
            this.txtFIRM_NAME.Properties.View = this.customGridView1;
            this.txtFIRM_NAME.Size = new System.Drawing.Size(358, 20);
            this.txtFIRM_NAME.TabIndex = 6;
            this.txtFIRM_NAME.EditValueChanged += new System.EventHandler(this.customGridLookUpEdit_Type_EditValueChanged);
            // 
            // customGridView1
            // 
            this.customGridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10});
            this.customGridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.customGridView1.Name = "customGridView1";
            this.customGridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.customGridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "名称";
            this.gridColumn7.FieldName = "FIRM_NAME";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            this.gridColumn7.Width = 338;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "ID";
            this.gridColumn8.FieldName = "FIRM_ID";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 0;
            this.gridColumn8.Width = 161;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "拼音";
            this.gridColumn9.FieldName = "FIRM_PINYIN";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Width = 338;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "地址";
            this.gridColumn10.FieldName = "FIRM_ADDRESS";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 2;
            this.gridColumn10.Width = 339;
            // 
            // customGridLookUpEdit_Type
            // 
            this.customGridLookUpEdit_Type.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.MED_MATERIAL_MASTER, "MATERIAL_TYPE", true));
            this.customGridLookUpEdit_Type.EditValue = "";
            this.customGridLookUpEdit_Type.EnterMoveNextControl = true;
            this.customGridLookUpEdit_Type.Location = new System.Drawing.Point(76, 102);
            this.customGridLookUpEdit_Type.Name = "customGridLookUpEdit_Type";
            this.customGridLookUpEdit_Type.Properties.AutoComplete = false;
            this.customGridLookUpEdit_Type.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.customGridLookUpEdit_Type.Properties.DisplayMember = "ITEM_NAME";
            this.customGridLookUpEdit_Type.Properties.NullText = "";
            this.customGridLookUpEdit_Type.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.customGridLookUpEdit_Type.Properties.ValueMember = "ITEM_ID";
            this.customGridLookUpEdit_Type.Properties.View = this.customGridLookUpEdit1View;
            this.customGridLookUpEdit_Type.Size = new System.Drawing.Size(136, 20);
            this.customGridLookUpEdit_Type.TabIndex = 5;
            this.customGridLookUpEdit_Type.EditValueChanged += new System.EventHandler(this.customGridLookUpEdit_Type_EditValueChanged);
            // 
            // customGridLookUpEdit1View
            // 
            this.customGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.customGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.customGridLookUpEdit1View.Name = "customGridLookUpEdit1View";
            this.customGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.customGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "gridColumn4";
            this.gridColumn4.FieldName = "ITEM_ID";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "gridColumn5";
            this.gridColumn5.FieldName = "ITEM_VALUE";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "名称";
            this.gridColumn6.FieldName = "ITEM_NAME";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            // 
            // txtMATERIAL_SPEC
            // 
            this.txtMATERIAL_SPEC.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MED_MATERIAL_MASTER, "MATERIAL_SPEC", true));
            this.txtMATERIAL_SPEC.EnterMoveNextControl = true;
            this.txtMATERIAL_SPEC.Location = new System.Drawing.Point(76, 172);
            this.txtMATERIAL_SPEC.Name = "txtMATERIAL_SPEC";
            this.txtMATERIAL_SPEC.Size = new System.Drawing.Size(130, 20);
            this.txtMATERIAL_SPEC.TabIndex = 8;
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(20, 173);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(24, 14);
            this.labelControl17.TabIndex = 38;
            this.labelControl17.Text = "规格";
            // 
            // txtMEMO
            // 
            this.txtMEMO.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MED_MATERIAL_MASTER, "MEMO", true));
            this.txtMEMO.EnterMoveNextControl = true;
            this.txtMEMO.Location = new System.Drawing.Point(76, 253);
            this.txtMEMO.Name = "txtMEMO";
            this.txtMEMO.Properties.MaxLength = 100;
            this.txtMEMO.Size = new System.Drawing.Size(358, 111);
            this.txtMEMO.TabIndex = 12;
            // 
            // txtPACK_BARCODE
            // 
            this.txtPACK_BARCODE.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MED_MATERIAL_MASTER, "PACK_BARCODE", true));
            this.txtPACK_BARCODE.EnterMoveNextControl = true;
            this.txtPACK_BARCODE.Location = new System.Drawing.Point(76, 217);
            this.txtPACK_BARCODE.Name = "txtPACK_BARCODE";
            this.txtPACK_BARCODE.Properties.MaxLength = 40;
            this.txtPACK_BARCODE.Size = new System.Drawing.Size(130, 20);
            this.txtPACK_BARCODE.TabIndex = 10;
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(20, 218);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(48, 14);
            this.labelControl15.TabIndex = 35;
            this.labelControl15.Text = "包装条码";
            // 
            // txtVALID_DATE
            // 
            this.txtVALID_DATE.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.MED_MATERIAL_MASTER, "VALID_DATE", true));
            this.txtVALID_DATE.EditValue = null;
            this.txtVALID_DATE.EnterMoveNextControl = true;
            this.txtVALID_DATE.Location = new System.Drawing.Point(310, 215);
            this.txtVALID_DATE.Name = "txtVALID_DATE";
            this.txtVALID_DATE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtVALID_DATE.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtVALID_DATE.Size = new System.Drawing.Size(124, 20);
            this.txtVALID_DATE.TabIndex = 11;
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(254, 216);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(36, 14);
            this.labelControl14.TabIndex = 33;
            this.labelControl14.Text = "有效期";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(20, 137);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.TabIndex = 16;
            this.labelControl5.Text = "厂商名称";
            // 
            // txtMATERIAL_ID
            // 
            this.txtMATERIAL_ID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MED_MATERIAL_MASTER, "MATERIAL_ID", true));
            this.txtMATERIAL_ID.Enabled = false;
            this.txtMATERIAL_ID.EnterMoveNextControl = true;
            this.txtMATERIAL_ID.Location = new System.Drawing.Point(76, 13);
            this.txtMATERIAL_ID.Name = "txtMATERIAL_ID";
            this.txtMATERIAL_ID.Size = new System.Drawing.Size(130, 20);
            this.txtMATERIAL_ID.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(20, 14);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 12;
            this.labelControl3.Text = "耗材编号";
            // 
            // txtMATERIAL_NAME
            // 
            this.txtMATERIAL_NAME.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MED_MATERIAL_MASTER, "MATERIAL_NAME", true));
            this.txtMATERIAL_NAME.EnterMoveNextControl = true;
            this.txtMATERIAL_NAME.Location = new System.Drawing.Point(76, 59);
            this.txtMATERIAL_NAME.Name = "txtMATERIAL_NAME";
            this.txtMATERIAL_NAME.Properties.MaxLength = 40;
            this.txtMATERIAL_NAME.Size = new System.Drawing.Size(130, 20);
            this.txtMATERIAL_NAME.TabIndex = 3;
            this.txtMATERIAL_NAME.ToolTip = "可输入耗材名或拼音码";
            this.txtMATERIAL_NAME.EditValueChanged += new System.EventHandler(this.txtMATERIAL_NAME_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(20, 105);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "耗材类型";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(20, 60);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "耗材名称";
            // 
            // btnCancel
            // 
            this.btnCancel.ImageIndex = 3;
            this.btnCancel.Location = new System.Drawing.Point(361, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.ImageIndex = 0;
            this.btnAdd.Location = new System.Drawing.Point(197, 10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 25);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "新增(&A)";
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.btnCancel);
            this.panelControl3.Controls.Add(this.btnAdd);
            this.panelControl3.Controls.Add(this.btnSave);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(0, 383);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(5);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(456, 45);
            this.panelControl3.TabIndex = 29;
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(278, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "ITEM_ID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "VALUE";
            this.gridColumn2.FieldName = "ITEM_VALUE";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "名称";
            this.gridColumn3.FieldName = "ITEM_NAME";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            // 
            // EditMaterialMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 428);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.panelControl3);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(472, 467);
            this.MinimumSize = new System.Drawing.Size(472, 466);
            this.Name = "EditMaterialMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "耗材资料维护";
            this.Load += new System.EventHandler(this.EditMaterialMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtCREATE_DATE.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCREATE_DATE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MED_MATERIAL_MASTER)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMATERIAL_PINYIN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSYSTEM_BARCODE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlControls)).EndInit();
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spiPRICE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxUNITS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFIRM_NAME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridLookUpEdit_Type.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMATERIAL_SPEC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMEMO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPACK_BARCODE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVALID_DATE.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVALID_DATE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMATERIAL_ID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMATERIAL_NAME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit txtCREATE_DATE;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.TextEdit txtMATERIAL_PINYIN;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.TextEdit txtSYSTEM_BARCODE;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.PanelControl pnlControls;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtMATERIAL_ID;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtMATERIAL_NAME;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Hemo.Client.Controls.DXSimpleButton btnCancel;
        private Hemo.Client.Controls.DXSimpleButton btnAdd;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private Hemo.Client.Controls.DXSimpleButton btnSave;
        private DevExpress.XtraEditors.DateEdit txtVALID_DATE;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.TextEdit txtPACK_BARCODE;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.MemoEdit txtMEMO;
        private DevExpress.XtraEditors.TextEdit txtMATERIAL_SPEC;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider;
        private Utilities.CustomGridLookUpEdit customGridLookUpEdit_Type;
        private Utilities.CustomGridView customGridLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private System.Windows.Forms.BindingSource MED_MATERIAL_MASTER;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private Utilities.CustomGridLookUpEdit txtFIRM_NAME;
        private Utilities.CustomGridView customGridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraEditors.LookUpEdit cbxUNITS;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SpinEdit spiPRICE;
    }
}