namespace Hemo.Client.UI.Patient
{
    partial class PatientSufficiencyUI
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
            this.btnDelete = new Hemo.Client.Controls.DXSimpleButton();
            this.btnQuery = new Hemo.Client.Controls.DXSimpleButton();
            this.txtToDate = new DevExpress.XtraEditors.DateEdit();
            this.lblToDate = new DevExpress.XtraEditors.LabelControl();
            this.txtFromDate = new DevExpress.XtraEditors.DateEdit();
            this.lblFromDate = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnPrint = new Hemo.Client.Controls.DXSimpleButton();
            this.btnExit = new Hemo.Client.Controls.DXSimpleButton();
            this.btnAdd = new Hemo.Client.Controls.DXSimpleButton();
            this.gcRecord = new DevExpress.XtraGrid.GridControl();
            this.gvRecord = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCustomGridLookUpEdit2 = new Hemo.Utilities.RepositoryItemCustomGridLookUpEdit();
            this.repositoryItemCustomGridLookUpEdit2View = new Hemo.Utilities.CustomGridView();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCustomGridLookUpEdit1 = new Hemo.Utilities.RepositoryItemCustomGridLookUpEdit();
            this.repositoryItemCustomGridLookUpEdit1View = new Hemo.Utilities.CustomGridView();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCustomGridLookUpEdit3 = new Hemo.Utilities.RepositoryItemCustomGridLookUpEdit();
            this.repositoryItemCustomGridLookUpEdit3View = new Hemo.Utilities.CustomGridView();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCustomGridLookUpEdit4 = new Hemo.Utilities.RepositoryItemCustomGridLookUpEdit();
            this.repositoryItemCustomGridLookUpEdit4View = new Hemo.Utilities.CustomGridView();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCustomGridLookUpEdit5 = new Hemo.Utilities.RepositoryItemCustomGridLookUpEdit();
            this.repositoryItemCustomGridLookUpEdit5View = new Hemo.Utilities.CustomGridView();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.dxSimpleButton1 = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit3View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit4View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit5View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.ImageIndex = 1;
            this.btnDelete.Location = new System.Drawing.Point(632, 6);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(86, 32);
            this.btnDelete.TabIndex = 376;
            this.btnDelete.Text = "删除(&D)";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.ImageIndex = 8;
            this.btnQuery.Location = new System.Drawing.Point(606, 6);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(86, 32);
            this.btnQuery.TabIndex = 313;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtToDate
            // 
            this.txtToDate.EditValue = null;
            this.txtToDate.Location = new System.Drawing.Point(297, 6);
            this.txtToDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToDate.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtToDate.Properties.Appearance.Options.UseFont = true;
            this.txtToDate.Properties.Appearance.Options.UseForeColor = true;
            this.txtToDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtToDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtToDate.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtToDate.Size = new System.Drawing.Size(134, 26);
            this.txtToDate.TabIndex = 311;
            // 
            // lblToDate
            // 
            this.lblToDate.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDate.Location = new System.Drawing.Point(218, 10);
            this.lblToDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(60, 20);
            this.lblToDate.TabIndex = 312;
            this.lblToDate.Text = "结束日期";
            // 
            // txtFromDate
            // 
            this.txtFromDate.EditValue = null;
            this.txtFromDate.Location = new System.Drawing.Point(78, 6);
            this.txtFromDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromDate.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtFromDate.Properties.Appearance.Options.UseFont = true;
            this.txtFromDate.Properties.Appearance.Options.UseForeColor = true;
            this.txtFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFromDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtFromDate.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtFromDate.Size = new System.Drawing.Size(134, 26);
            this.txtFromDate.TabIndex = 309;
            // 
            // lblFromDate
            // 
            this.lblFromDate.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDate.Location = new System.Drawing.Point(6, 12);
            this.lblFromDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(60, 20);
            this.lblFromDate.TabIndex = 310;
            this.lblFromDate.Text = "开始日期";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnPrint);
            this.panelControl2.Controls.Add(this.btnQuery);
            this.panelControl2.Controls.Add(this.txtToDate);
            this.panelControl2.Controls.Add(this.lblToDate);
            this.panelControl2.Controls.Add(this.txtFromDate);
            this.panelControl2.Controls.Add(this.lblFromDate);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(831, 46);
            this.panelControl2.TabIndex = 375;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.ImageIndex = 6;
            this.btnPrint.Location = new System.Drawing.Point(698, 6);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(86, 32);
            this.btnPrint.TabIndex = 314;
            this.btnPrint.Text = "打印(&P)";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.ImageIndex = 3;
            this.btnExit.Location = new System.Drawing.Point(725, 6);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(86, 32);
            this.btnExit.TabIndex = 374;
            this.btnExit.Text = "关闭(&Q)";
            this.btnExit.Visible = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.ImageIndex = 0;
            this.btnAdd.Location = new System.Drawing.Point(539, 6);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(86, 32);
            this.btnAdd.TabIndex = 373;
            this.btnAdd.Text = "新增(&A)";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // gcRecord
            // 
            this.gcRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcRecord.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcRecord.Location = new System.Drawing.Point(0, 46);
            this.gcRecord.MainView = this.gvRecord;
            this.gcRecord.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcRecord.Name = "gcRecord";
            this.gcRecord.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCustomGridLookUpEdit1,
            this.repositoryItemCustomGridLookUpEdit2,
            this.repositoryItemCustomGridLookUpEdit3,
            this.repositoryItemCustomGridLookUpEdit4,
            this.repositoryItemCustomGridLookUpEdit5});
            this.gcRecord.Size = new System.Drawing.Size(831, 461);
            this.gcRecord.TabIndex = 377;
            this.gcRecord.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRecord});
            // 
            // gvRecord
            // 
            this.gvRecord.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCreateDate,
            this.colCreateBy,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gvRecord.GridControl = this.gcRecord;
            this.gvRecord.Name = "gvRecord";
            this.gvRecord.OptionsBehavior.Editable = false;
            this.gvRecord.OptionsBehavior.ReadOnly = true;
            this.gvRecord.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvRecord.OptionsView.ShowGroupPanel = false;
            this.gvRecord.DoubleClick += new System.EventHandler(this.gvRecord_DoubleClick);
            // 
            // colCreateDate
            // 
            this.colCreateDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colCreateDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreateDate.Caption = "录入日期";
            this.colCreateDate.FieldName = "CREATEDATE";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.OptionsColumn.AllowEdit = false;
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 0;
            // 
            // colCreateBy
            // 
            this.colCreateBy.AppearanceHeader.Options.UseTextOptions = true;
            this.colCreateBy.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreateBy.Caption = "录入人员";
            this.colCreateBy.FieldName = "USERNAME";
            this.colCreateBy.Name = "colCreateBy";
            this.colCreateBy.OptionsColumn.AllowEdit = false;
            this.colCreateBy.Visible = true;
            this.colCreateBy.VisibleIndex = 1;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "透析中血压";
            this.gridColumn1.ColumnEdit = this.repositoryItemCustomGridLookUpEdit2;
            this.gridColumn1.FieldName = "INDIALYSISBP";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            // 
            // repositoryItemCustomGridLookUpEdit2
            // 
            this.repositoryItemCustomGridLookUpEdit2.AutoComplete = false;
            this.repositoryItemCustomGridLookUpEdit2.AutoHeight = false;
            this.repositoryItemCustomGridLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCustomGridLookUpEdit2.DisplayMember = "ITEM_NAME";
            this.repositoryItemCustomGridLookUpEdit2.Name = "repositoryItemCustomGridLookUpEdit2";
            this.repositoryItemCustomGridLookUpEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.repositoryItemCustomGridLookUpEdit2.ValueMember = "ITEM_VALUE";
            this.repositoryItemCustomGridLookUpEdit2.View = this.repositoryItemCustomGridLookUpEdit2View;
            // 
            // repositoryItemCustomGridLookUpEdit2View
            // 
            this.repositoryItemCustomGridLookUpEdit2View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn6,
            this.gridColumn7});
            this.repositoryItemCustomGridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemCustomGridLookUpEdit2View.Name = "repositoryItemCustomGridLookUpEdit2View";
            this.repositoryItemCustomGridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemCustomGridLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "ITEM_VALUE";
            this.gridColumn6.FieldName = "ITEM_VALUE";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "ITEM_NAME";
            this.gridColumn7.FieldName = "ITEM_NAME";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "体重增长";
            this.gridColumn2.ColumnEdit = this.repositoryItemCustomGridLookUpEdit1;
            this.gridColumn2.FieldName = "WEIGHTADD";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            // 
            // repositoryItemCustomGridLookUpEdit1
            // 
            this.repositoryItemCustomGridLookUpEdit1.AutoComplete = false;
            this.repositoryItemCustomGridLookUpEdit1.AutoHeight = false;
            this.repositoryItemCustomGridLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCustomGridLookUpEdit1.DisplayMember = "ITEM_NAME";
            this.repositoryItemCustomGridLookUpEdit1.Name = "repositoryItemCustomGridLookUpEdit1";
            this.repositoryItemCustomGridLookUpEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.repositoryItemCustomGridLookUpEdit1.ValueMember = "ITEM_VALUE";
            this.repositoryItemCustomGridLookUpEdit1.View = this.repositoryItemCustomGridLookUpEdit1View;
            // 
            // repositoryItemCustomGridLookUpEdit1View
            // 
            this.repositoryItemCustomGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn8,
            this.gridColumn9});
            this.repositoryItemCustomGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemCustomGridLookUpEdit1View.Name = "repositoryItemCustomGridLookUpEdit1View";
            this.repositoryItemCustomGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemCustomGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "ITEM_VALUE";
            this.gridColumn8.FieldName = "ITEM_VALUE";
            this.gridColumn8.Name = "gridColumn8";
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "ITEM_NAME";
            this.gridColumn9.FieldName = "ITEM_NAME";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "营养状态";
            this.gridColumn3.ColumnEdit = this.repositoryItemCustomGridLookUpEdit3;
            this.gridColumn3.FieldName = "NUTRITION";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 4;
            // 
            // repositoryItemCustomGridLookUpEdit3
            // 
            this.repositoryItemCustomGridLookUpEdit3.AutoComplete = false;
            this.repositoryItemCustomGridLookUpEdit3.AutoHeight = false;
            this.repositoryItemCustomGridLookUpEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCustomGridLookUpEdit3.DisplayMember = "ITEM_NAME";
            this.repositoryItemCustomGridLookUpEdit3.Name = "repositoryItemCustomGridLookUpEdit3";
            this.repositoryItemCustomGridLookUpEdit3.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.repositoryItemCustomGridLookUpEdit3.ValueMember = "ITEM_VALUE";
            this.repositoryItemCustomGridLookUpEdit3.View = this.repositoryItemCustomGridLookUpEdit3View;
            // 
            // repositoryItemCustomGridLookUpEdit3View
            // 
            this.repositoryItemCustomGridLookUpEdit3View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn12,
            this.gridColumn13});
            this.repositoryItemCustomGridLookUpEdit3View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemCustomGridLookUpEdit3View.Name = "repositoryItemCustomGridLookUpEdit3View";
            this.repositoryItemCustomGridLookUpEdit3View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemCustomGridLookUpEdit3View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "ITEM_VALUE";
            this.gridColumn12.FieldName = "ITEM_VALUE";
            this.gridColumn12.Name = "gridColumn12";
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "ITEM_NAME";
            this.gridColumn13.FieldName = "ITEM_NAME";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 0;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "KT/V";
            this.gridColumn4.ColumnEdit = this.repositoryItemCustomGridLookUpEdit4;
            this.gridColumn4.FieldName = "KTV";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            // 
            // repositoryItemCustomGridLookUpEdit4
            // 
            this.repositoryItemCustomGridLookUpEdit4.AutoComplete = false;
            this.repositoryItemCustomGridLookUpEdit4.AutoHeight = false;
            this.repositoryItemCustomGridLookUpEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCustomGridLookUpEdit4.DisplayMember = "ITEM_NAME";
            this.repositoryItemCustomGridLookUpEdit4.Name = "repositoryItemCustomGridLookUpEdit4";
            this.repositoryItemCustomGridLookUpEdit4.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.repositoryItemCustomGridLookUpEdit4.ValueMember = "ITEM_VALUE";
            this.repositoryItemCustomGridLookUpEdit4.View = this.repositoryItemCustomGridLookUpEdit4View;
            // 
            // repositoryItemCustomGridLookUpEdit4View
            // 
            this.repositoryItemCustomGridLookUpEdit4View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn10,
            this.gridColumn11});
            this.repositoryItemCustomGridLookUpEdit4View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemCustomGridLookUpEdit4View.Name = "repositoryItemCustomGridLookUpEdit4View";
            this.repositoryItemCustomGridLookUpEdit4View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemCustomGridLookUpEdit4View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "ITEM_VALUE";
            this.gridColumn10.FieldName = "ITEM_VALUE";
            this.gridColumn10.Name = "gridColumn10";
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "ITEM_NAME";
            this.gridColumn11.FieldName = "ITEM_NAME";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 0;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "URR";
            this.gridColumn5.ColumnEdit = this.repositoryItemCustomGridLookUpEdit5;
            this.gridColumn5.FieldName = "URR";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 6;
            // 
            // repositoryItemCustomGridLookUpEdit5
            // 
            this.repositoryItemCustomGridLookUpEdit5.AutoComplete = false;
            this.repositoryItemCustomGridLookUpEdit5.AutoHeight = false;
            this.repositoryItemCustomGridLookUpEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCustomGridLookUpEdit5.DisplayMember = "ITEM_NAME";
            this.repositoryItemCustomGridLookUpEdit5.Name = "repositoryItemCustomGridLookUpEdit5";
            this.repositoryItemCustomGridLookUpEdit5.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.repositoryItemCustomGridLookUpEdit5.ValueMember = "ITEM_VALUE";
            this.repositoryItemCustomGridLookUpEdit5.View = this.repositoryItemCustomGridLookUpEdit5View;
            // 
            // repositoryItemCustomGridLookUpEdit5View
            // 
            this.repositoryItemCustomGridLookUpEdit5View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemCustomGridLookUpEdit5View.Name = "repositoryItemCustomGridLookUpEdit5View";
            this.repositoryItemCustomGridLookUpEdit5View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemCustomGridLookUpEdit5View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "ITEM_VALUE";
            this.gridColumn14.FieldName = "ITEM_VALUE";
            this.gridColumn14.Name = "gridColumn14";
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "ITEM_NAME";
            this.gridColumn15.FieldName = "ITEM_NAME";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.dxSimpleButton1);
            this.panelControl1.Controls.Add(this.btnExit);
            this.panelControl1.Controls.Add(this.btnAdd);
            this.panelControl1.Controls.Add(this.btnDelete);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 463);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(831, 44);
            this.panelControl1.TabIndex = 378;
            // 
            // dxSimpleButton1
            // 
            this.dxSimpleButton1.ImageIndex = 14;
            this.dxSimpleButton1.Location = new System.Drawing.Point(443, 5);
            this.dxSimpleButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dxSimpleButton1.Name = "dxSimpleButton1";
            this.dxSimpleButton1.Size = new System.Drawing.Size(86, 32);
            this.dxSimpleButton1.TabIndex = 379;
            this.dxSimpleButton1.Text = "检验数据";
            this.dxSimpleButton1.Visible = false;
            this.dxSimpleButton1.Click += new System.EventHandler(this.dxSimpleButton1_Click);
            // 
            // PatientSufficiencyUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.gcRecord);
            this.Controls.Add(this.panelControl2);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PatientSufficiencyUI";
            this.Size = new System.Drawing.Size(831, 507);
            this.Load += new System.EventHandler(this.PatientSufficiency_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit3View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit4View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit5View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.DXSimpleButton btnDelete;
        private Controls.DXSimpleButton btnQuery;
        private DevExpress.XtraEditors.DateEdit txtToDate;
        private DevExpress.XtraEditors.LabelControl lblToDate;
        private DevExpress.XtraEditors.DateEdit txtFromDate;
        private DevExpress.XtraEditors.LabelControl lblFromDate;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private Controls.DXSimpleButton btnExit;
        private Controls.DXSimpleButton btnAdd;
        private DevExpress.XtraGrid.GridControl gcRecord;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRecord;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateBy;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private Utilities.RepositoryItemCustomGridLookUpEdit repositoryItemCustomGridLookUpEdit2;
        private Utilities.CustomGridView repositoryItemCustomGridLookUpEdit2View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private Utilities.RepositoryItemCustomGridLookUpEdit repositoryItemCustomGridLookUpEdit1;
        private Utilities.CustomGridView repositoryItemCustomGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private Utilities.RepositoryItemCustomGridLookUpEdit repositoryItemCustomGridLookUpEdit3;
        private Utilities.CustomGridView repositoryItemCustomGridLookUpEdit3View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private Utilities.RepositoryItemCustomGridLookUpEdit repositoryItemCustomGridLookUpEdit4;
        private Utilities.CustomGridView repositoryItemCustomGridLookUpEdit4View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private Utilities.RepositoryItemCustomGridLookUpEdit repositoryItemCustomGridLookUpEdit5;
        private Utilities.CustomGridView repositoryItemCustomGridLookUpEdit5View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private Controls.DXSimpleButton btnPrint;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Controls.DXSimpleButton dxSimpleButton1;
    }
}