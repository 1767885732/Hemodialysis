namespace Hemo.Client.UI.Patient {
    partial class PatientBillRecordFrm
    {
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
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.patientInfo = new Hemo.Client.UI.Hemodialysis.CtlUserInfo();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.gcRecord = new DevExpress.XtraGrid.GridControl();
            this.gvRecord = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colHemoId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWEIGHT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl103 = new DevExpress.XtraEditors.LabelControl();
            this.lupCHECK_NURSE = new DevExpress.XtraEditors.LookUpEdit();
            this.txtNote = new DevExpress.XtraEditors.TextEdit();
            this.btnAdd = new Hemo.Client.Controls.DXSimpleButton();
            this.lupUSE_TYPE = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.spnDRUG_TIMES = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtDRUG_NAME = new DevExpress.XtraEditors.LookUpEdit();
            this.txtRastCount = new DevExpress.XtraEditors.TextEdit();
            this.txtRastFee = new DevExpress.XtraEditors.TextEdit();
            this.txtAllFee = new DevExpress.XtraEditors.TextEdit();
            this.btnHistory = new Hemo.Client.Controls.DXSimpleButton();
            this.btnDel = new Hemo.Client.Controls.DXSimpleButton();
            this.dxSimpleButton1 = new Hemo.Client.Controls.DXSimpleButton();
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupCHECK_NURSE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupUSE_TYPE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnDRUG_TIMES.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDRUG_NAME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRastCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRastFee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllFee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(384, 581);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 25);
            this.btnSave.TabIndex = 309;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // patientInfo
            // 
            this.patientInfo.FormContainer = null;
            this.patientInfo.HEMODIALYSIS_ID = "";
            this.patientInfo.Location = new System.Drawing.Point(-2, -2);
            this.patientInfo.Name = "patientInfo";
            this.patientInfo.Size = new System.Drawing.Size(570, 102);
            this.patientInfo.TabIndex = 310;
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Controls.Add(this.xtraTabControl1);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.labelControl103);
            this.groupControl2.Controls.Add(this.lupCHECK_NURSE);
            this.groupControl2.Controls.Add(this.txtNote);
            this.groupControl2.Controls.Add(this.btnAdd);
            this.groupControl2.Controls.Add(this.lupUSE_TYPE);
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.spnDRUG_TIMES);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.txtDRUG_NAME);
            this.groupControl2.Controls.Add(this.txtRastCount);
            this.groupControl2.Controls.Add(this.txtRastFee);
            this.groupControl2.Controls.Add(this.txtAllFee);
            this.groupControl2.Location = new System.Drawing.Point(6, 104);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(563, 471);
            this.groupControl2.TabIndex = 312;
            this.groupControl2.Text = "记账项目项目";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xtraTabControl1.Location = new System.Drawing.Point(2, 127);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(559, 342);
            this.xtraTabControl1.TabIndex = 806;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.gcRecord);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(552, 312);
            this.xtraTabPage1.Text = "记账";
            // 
            // gcRecord
            // 
            this.gcRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcRecord.Location = new System.Drawing.Point(0, 0);
            this.gcRecord.MainView = this.gvRecord;
            this.gcRecord.Name = "gcRecord";
            this.gcRecord.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gcRecord.Size = new System.Drawing.Size(552, 312);
            this.gcRecord.TabIndex = 312;
            this.gcRecord.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRecord});
            // 
            // gvRecord
            // 
            this.gvRecord.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvRecord.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvRecord.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvRecord.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colHemoId,
            this.colCreateDate,
            this.colPrice,
            this.colWEIGHT,
            this.gridColumn1});
            this.gvRecord.GridControl = this.gcRecord;
            this.gvRecord.Name = "gvRecord";
            this.gvRecord.OptionsBehavior.Editable = false;
            this.gvRecord.OptionsBehavior.ReadOnly = true;
            this.gvRecord.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvRecord.OptionsView.ColumnAutoWidth = false;
            this.gvRecord.OptionsView.ShowGroupPanel = false;
            // 
            // colHemoId
            // 
            this.colHemoId.AppearanceHeader.Options.UseTextOptions = true;
            this.colHemoId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHemoId.Caption = "项目名称";
            this.colHemoId.FieldName = "BILL_ITEM_NAME";
            this.colHemoId.Name = "colHemoId";
            this.colHemoId.OptionsColumn.AllowEdit = false;
            this.colHemoId.OptionsColumn.ReadOnly = true;
            this.colHemoId.OptionsFilter.AllowAutoFilter = false;
            this.colHemoId.OptionsFilter.AllowFilter = false;
            this.colHemoId.Visible = true;
            this.colHemoId.VisibleIndex = 0;
            this.colHemoId.Width = 260;
            // 
            // colCreateDate
            // 
            this.colCreateDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colCreateDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreateDate.Caption = "使用数量";
            this.colCreateDate.FieldName = "BILL_COUNT";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.OptionsFilter.AllowAutoFilter = false;
            this.colCreateDate.OptionsFilter.AllowFilter = false;
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 1;
            this.colCreateDate.Width = 80;
            // 
            // colPrice
            // 
            this.colPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPrice.Caption = "费用";
            this.colPrice.FieldName = "BILL_PRICE";
            this.colPrice.Name = "colPrice";
            this.colPrice.Visible = true;
            this.colPrice.VisibleIndex = 2;
            // 
            // colWEIGHT
            // 
            this.colWEIGHT.AppearanceHeader.Options.UseTextOptions = true;
            this.colWEIGHT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colWEIGHT.Caption = "是否医保";
            this.colWEIGHT.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colWEIGHT.FieldName = "BILL_TYPE";
            this.colWEIGHT.Name = "colWEIGHT";
            this.colWEIGHT.OptionsColumn.AllowEdit = false;
            this.colWEIGHT.OptionsColumn.ReadOnly = true;
            this.colWEIGHT.OptionsFilter.AllowAutoFilter = false;
            this.colWEIGHT.OptionsFilter.AllowFilter = false;
            this.colWEIGHT.Width = 90;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repositoryItemCheckEdit1.ValueChecked = "21";
            this.repositoryItemCheckEdit1.ValueGrayed = "20";
            this.repositoryItemCheckEdit1.ValueUnchecked = "20";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "备注";
            this.gridColumn1.FieldName = "NOTE_INFO";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            this.gridColumn1.Width = 134;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.gridControl1);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(552, 312);
            this.xtraTabPage2.Text = "剩余项目";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit2});
            this.gridControl1.Size = new System.Drawing.Size(552, 312);
            this.gridControl1.TabIndex = 313;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "项目名称";
            this.gridColumn2.FieldName = "ITEM_NAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn2.OptionsFilter.AllowFilter = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 260;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "剩余数量";
            this.gridColumn3.FieldName = "ITEM_COUNT";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn3.OptionsFilter.AllowFilter = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 90;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "费用";
            this.gridColumn4.FieldName = "PREPAYCOST";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 90;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repositoryItemCheckEdit2.ValueChecked = "21";
            this.repositoryItemCheckEdit2.ValueGrayed = "20";
            this.repositoryItemCheckEdit2.ValueUnchecked = "20";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(39, 100);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(27, 17);
            this.labelControl3.TabIndex = 805;
            this.labelControl3.Text = "备注:";
            // 
            // labelControl103
            // 
            this.labelControl103.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl103.Appearance.Options.UseFont = true;
            this.labelControl103.Location = new System.Drawing.Point(28, 69);
            this.labelControl103.Name = "labelControl103";
            this.labelControl103.Size = new System.Drawing.Size(39, 17);
            this.labelControl103.TabIndex = 799;
            this.labelControl103.Text = "操作人:";
            // 
            // lupCHECK_NURSE
            // 
            this.lupCHECK_NURSE.EnterMoveNextControl = true;
            this.lupCHECK_NURSE.Location = new System.Drawing.Point(87, 65);
            this.lupCHECK_NURSE.Name = "lupCHECK_NURSE";
            this.lupCHECK_NURSE.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lupCHECK_NURSE.Properties.Appearance.Options.UseFont = true;
            this.lupCHECK_NURSE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupCHECK_NURSE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.lupCHECK_NURSE.Properties.NullText = "";
            this.lupCHECK_NURSE.Size = new System.Drawing.Size(108, 23);
            this.lupCHECK_NURSE.TabIndex = 798;
            // 
            // txtNote
            // 
            this.txtNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNote.EnterMoveNextControl = true;
            this.txtNote.Location = new System.Drawing.Point(87, 98);
            this.txtNote.Name = "txtNote";
            this.txtNote.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtNote.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtNote.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtNote.Properties.Appearance.Options.UseBackColor = true;
            this.txtNote.Properties.Appearance.Options.UseFont = true;
            this.txtNote.Properties.Appearance.Options.UseForeColor = true;
            this.txtNote.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtNote.Size = new System.Drawing.Size(327, 23);
            this.txtNote.TabIndex = 804;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.ImageIndex = 0;
            this.btnAdd.Location = new System.Drawing.Point(447, 96);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(86, 25);
            this.btnAdd.TabIndex = 800;
            this.btnAdd.Text = "添加(&A)";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lupUSE_TYPE
            // 
            this.lupUSE_TYPE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lupUSE_TYPE.EditValue = "20";
            this.lupUSE_TYPE.Enabled = false;
            this.lupUSE_TYPE.Location = new System.Drawing.Point(212, 64);
            this.lupUSE_TYPE.Name = "lupUSE_TYPE";
            this.lupUSE_TYPE.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lupUSE_TYPE.Properties.Appearance.Options.UseBackColor = true;
            this.lupUSE_TYPE.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("21", "医保"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("20", "自费")});
            this.lupUSE_TYPE.Size = new System.Drawing.Size(35, 27);
            this.lupUSE_TYPE.TabIndex = 803;
            this.lupUSE_TYPE.Visible = false;
            // 
            // labelControl6
            // 
            this.labelControl6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl6.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(386, 66);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(51, 17);
            this.labelControl6.TabIndex = 802;
            this.labelControl6.Text = "剩余费用:";
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl4.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(386, 35);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(51, 17);
            this.labelControl4.TabIndex = 802;
            this.labelControl4.Text = "记账费用:";
            // 
            // labelControl5
            // 
            this.labelControl5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl5.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(263, 66);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(51, 17);
            this.labelControl5.TabIndex = 802;
            this.labelControl5.Text = "剩余数量:";
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(263, 35);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(51, 17);
            this.labelControl2.TabIndex = 802;
            this.labelControl2.Text = "记账数量:";
            // 
            // spnDRUG_TIMES
            // 
            this.spnDRUG_TIMES.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.spnDRUG_TIMES.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spnDRUG_TIMES.EnterMoveNextControl = true;
            this.spnDRUG_TIMES.Location = new System.Drawing.Point(327, 32);
            this.spnDRUG_TIMES.Name = "spnDRUG_TIMES";
            this.spnDRUG_TIMES.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.spnDRUG_TIMES.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.spnDRUG_TIMES.Properties.Appearance.Options.UseFont = true;
            this.spnDRUG_TIMES.Properties.Appearance.Options.UseForeColor = true;
            this.spnDRUG_TIMES.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spnDRUG_TIMES.Size = new System.Drawing.Size(57, 23);
            this.spnDRUG_TIMES.TabIndex = 801;
            this.spnDRUG_TIMES.EditValueChanged += new System.EventHandler(this.spnDRUG_TIMES_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(16, 35);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(51, 17);
            this.labelControl1.TabIndex = 800;
            this.labelControl1.Text = "记账项目:";
            // 
            // txtDRUG_NAME
            // 
            this.txtDRUG_NAME.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDRUG_NAME.EnterMoveNextControl = true;
            this.txtDRUG_NAME.Location = new System.Drawing.Point(87, 32);
            this.txtDRUG_NAME.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDRUG_NAME.Name = "txtDRUG_NAME";
            this.txtDRUG_NAME.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtDRUG_NAME.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtDRUG_NAME.Properties.Appearance.Options.UseFont = true;
            this.txtDRUG_NAME.Properties.Appearance.Options.UseForeColor = true;
            this.txtDRUG_NAME.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDRUG_NAME.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ITEM_ID", "编号", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ITEM_NAME", "项目名称")});
            this.txtDRUG_NAME.Properties.DisplayMember = "ITEM_NAME";
            this.txtDRUG_NAME.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtDRUG_NAME.Properties.NullText = "";
            this.txtDRUG_NAME.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.txtDRUG_NAME.Properties.ValueMember = "ITEM_ID";
            this.txtDRUG_NAME.Size = new System.Drawing.Size(169, 23);
            this.txtDRUG_NAME.TabIndex = 313;
            this.txtDRUG_NAME.EditValueChanged += new System.EventHandler(this.txtDRUG_NAME_EditValueChanged);
            // 
            // txtRastCount
            // 
            this.txtRastCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRastCount.EditValue = "";
            this.txtRastCount.Enabled = false;
            this.txtRastCount.Location = new System.Drawing.Point(327, 64);
            this.txtRastCount.Name = "txtRastCount";
            this.txtRastCount.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRastCount.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtRastCount.Properties.Appearance.Options.UseFont = true;
            this.txtRastCount.Properties.Appearance.Options.UseForeColor = true;
            this.txtRastCount.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered;
            this.txtRastCount.Properties.Mask.EditMask = "d";
            this.txtRastCount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtRastCount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtRastCount.Size = new System.Drawing.Size(57, 23);
            this.txtRastCount.TabIndex = 801;
            this.txtRastCount.EditValueChanged += new System.EventHandler(this.spnDRUG_TIMES_EditValueChanged);
            // 
            // txtRastFee
            // 
            this.txtRastFee.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRastFee.EditValue = "";
            this.txtRastFee.Enabled = false;
            this.txtRastFee.Location = new System.Drawing.Point(447, 64);
            this.txtRastFee.Name = "txtRastFee";
            this.txtRastFee.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRastFee.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtRastFee.Properties.Appearance.Options.UseFont = true;
            this.txtRastFee.Properties.Appearance.Options.UseForeColor = true;
            this.txtRastFee.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered;
            this.txtRastFee.Properties.Mask.EditMask = "d";
            this.txtRastFee.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtRastFee.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtRastFee.Size = new System.Drawing.Size(109, 23);
            this.txtRastFee.TabIndex = 801;
            this.txtRastFee.EditValueChanged += new System.EventHandler(this.spnDRUG_TIMES_EditValueChanged);
            // 
            // txtAllFee
            // 
            this.txtAllFee.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAllFee.EditValue = "";
            this.txtAllFee.Enabled = false;
            this.txtAllFee.Location = new System.Drawing.Point(447, 32);
            this.txtAllFee.Name = "txtAllFee";
            this.txtAllFee.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAllFee.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtAllFee.Properties.Appearance.Options.UseFont = true;
            this.txtAllFee.Properties.Appearance.Options.UseForeColor = true;
            this.txtAllFee.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered;
            this.txtAllFee.Properties.Mask.EditMask = "d";
            this.txtAllFee.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtAllFee.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtAllFee.Size = new System.Drawing.Size(109, 23);
            this.txtAllFee.TabIndex = 801;
            this.txtAllFee.EditValueChanged += new System.EventHandler(this.spnDRUG_TIMES_EditValueChanged);
            // 
            // btnHistory
            // 
            this.btnHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHistory.ImageIndex = 8;
            this.btnHistory.Location = new System.Drawing.Point(292, 581);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(86, 25);
            this.btnHistory.TabIndex = 801;
            this.btnHistory.Text = "记账明细";
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDel.ImageIndex = 1;
            this.btnDel.Location = new System.Drawing.Point(200, 581);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(86, 25);
            this.btnDel.TabIndex = 802;
            this.btnDel.Text = "删除(&D)";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // dxSimpleButton1
            // 
            this.dxSimpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dxSimpleButton1.ImageIndex = 3;
            this.dxSimpleButton1.Location = new System.Drawing.Point(476, 581);
            this.dxSimpleButton1.Name = "dxSimpleButton1";
            this.dxSimpleButton1.Size = new System.Drawing.Size(86, 25);
            this.dxSimpleButton1.TabIndex = 804;
            this.dxSimpleButton1.Text = "关闭(&C)";
            this.dxSimpleButton1.Click += new System.EventHandler(this.dxSimpleButton1_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // PatientBillRecordFrm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(581, 612);
            this.Controls.Add(this.dxSimpleButton1);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnHistory);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.patientInfo);
            this.Controls.Add(this.btnSave);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(597, 650);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(597, 650);
            this.Name = "PatientBillRecordFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "患者费用记账";
            this.Load += new System.EventHandler(this.PatientRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupCHECK_NURSE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupUSE_TYPE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnDRUG_TIMES.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDRUG_NAME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRastCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRastFee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllFee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Hemo.Client.Controls.DXSimpleButton btnSave;
        private Hemodialysis.CtlUserInfo patientInfo;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gcRecord;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRecord;
        private DevExpress.XtraGrid.Columns.GridColumn colHemoId;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colWEIGHT;
        private DevExpress.XtraEditors.LookUpEdit lupCHECK_NURSE;
        private DevExpress.XtraEditors.LabelControl labelControl103;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit txtDRUG_NAME;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SpinEdit spnDRUG_TIMES;
        private DevExpress.XtraEditors.RadioGroup lupUSE_TYPE;
        private Hemo.Client.Controls.DXSimpleButton btnAdd;
        private DevExpress.XtraEditors.TextEdit txtNote;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private Hemo.Client.Controls.DXSimpleButton btnHistory;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private Hemo.Client.Controls.DXSimpleButton btnDel;
        private DevExpress.XtraGrid.Columns.GridColumn colPrice;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtAllFee;
        private Controls.DXSimpleButton dxSimpleButton1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtRastFee;
        private DevExpress.XtraEditors.TextEdit txtRastCount;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;

    }
}