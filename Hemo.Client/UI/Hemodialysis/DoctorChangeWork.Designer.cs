using Hemo.Client.Controls;
namespace Hemo.Client.UI.Hemodialysis
{
    partial class DoctorChangeWork
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
            this.components = new System.ComponentModel.Container();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.endTime = new DevExpress.XtraEditors.DateEdit();
            this.beginTime = new DevExpress.XtraEditors.DateEdit();
            this.iFilterCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.iNewButton = new Hemo.Client.Controls.DXSimpleButton();
            this.iDeleteButton = new Hemo.Client.Controls.DXSimpleButton();
            this.iSaveButton = new Hemo.Client.Controls.DXSimpleButton();
            this.btn_Print = new Hemo.Client.Controls.DXSimpleButton();
            this.iRefreshButton = new Hemo.Client.Controls.DXSimpleButton();
            this.iCloseButton = new Hemo.Client.Controls.DXSimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.busyIndicator = new Hemo.Client.Controls.BusyIndicator();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.CHANGETIME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.PATIENT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCustomGridLookUpEdit2 = new Hemo.Utilities.RepositoryItemCustomGridLookUpEdit();
            this.repositoryItemCustomGridLookUpEdit2View = new Hemo.Utilities.CustomGridView();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HEMODIALYSIS_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SEX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.AGE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DIAGNOSE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CONTENT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iFilterCheckEdit.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.iFilterCheckEdit);
            this.panelControl2.Controls.Add(this.iNewButton);
            this.panelControl2.Controls.Add(this.iDeleteButton);
            this.panelControl2.Controls.Add(this.iSaveButton);
            this.panelControl2.Controls.Add(this.iCloseButton);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 411);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(875, 40);
            this.panelControl2.TabIndex = 4;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(153, 13);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(9, 14);
            this.labelControl3.TabIndex = 23;
            this.labelControl3.Text = "~";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 24;
            this.labelControl1.Text = "日期范围";
            // 
            // endTime
            // 
            this.endTime.EditValue = null;
            this.endTime.Location = new System.Drawing.Point(168, 10);
            this.endTime.Name = "endTime";
            this.endTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.endTime.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.endTime.Size = new System.Drawing.Size(91, 21);
            this.endTime.TabIndex = 21;
            // 
            // beginTime
            // 
            this.beginTime.EditValue = null;
            this.beginTime.Location = new System.Drawing.Point(60, 10);
            this.beginTime.Name = "beginTime";
            this.beginTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.beginTime.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beginTime.Size = new System.Drawing.Size(91, 21);
            this.beginTime.TabIndex = 22;
            // 
            // iFilterCheckEdit
            // 
            this.iFilterCheckEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iFilterCheckEdit.Location = new System.Drawing.Point(5, 13);
            this.iFilterCheckEdit.Name = "iFilterCheckEdit";
            this.iFilterCheckEdit.Properties.Caption = "显示过虑行";
            this.iFilterCheckEdit.Size = new System.Drawing.Size(97, 19);
            this.iFilterCheckEdit.TabIndex = 20;
            this.iFilterCheckEdit.CheckedChanged += new System.EventHandler(this.iFilterCheckEdit_CheckedChanged);
            // 
            // iNewButton
            // 
            this.iNewButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.iNewButton.ImageIndex = 0;
            this.iNewButton.Location = new System.Drawing.Point(550, 10);
            this.iNewButton.Name = "iNewButton";
            this.iNewButton.Size = new System.Drawing.Size(75, 25);
            this.iNewButton.TabIndex = 19;
            this.iNewButton.Text = "新增(&A) ";
            this.iNewButton.Click += new System.EventHandler(this.iNewButton_Click);
            // 
            // iDeleteButton
            // 
            this.iDeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.iDeleteButton.ImageIndex = 1;
            this.iDeleteButton.Location = new System.Drawing.Point(631, 10);
            this.iDeleteButton.Name = "iDeleteButton";
            this.iDeleteButton.Size = new System.Drawing.Size(75, 25);
            this.iDeleteButton.TabIndex = 18;
            this.iDeleteButton.Text = " 删除(&D) ";
            this.iDeleteButton.Click += new System.EventHandler(this.iDeleteButton_Click);
            // 
            // iSaveButton
            // 
            this.iSaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.iSaveButton.ImageIndex = 7;
            this.iSaveButton.Location = new System.Drawing.Point(712, 9);
            this.iSaveButton.Name = "iSaveButton";
            this.iSaveButton.Size = new System.Drawing.Size(75, 25);
            this.iSaveButton.TabIndex = 17;
            this.iSaveButton.Text = "保存(&S) ";
            this.iSaveButton.Click += new System.EventHandler(this.iSaveButton_Click);
            // 
            // btn_Print
            // 
            this.btn_Print.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Print.ImageIndex = 6;
            this.btn_Print.Location = new System.Drawing.Point(793, 9);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(75, 25);
            this.btn_Print.TabIndex = 7;
            this.btn_Print.Text = "打印(&P) ";
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // iRefreshButton
            // 
            this.iRefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.iRefreshButton.ImageIndex = 8;
            this.iRefreshButton.Location = new System.Drawing.Point(712, 9);
            this.iRefreshButton.Name = "iRefreshButton";
            this.iRefreshButton.Size = new System.Drawing.Size(75, 25);
            this.iRefreshButton.TabIndex = 7;
            this.iRefreshButton.Text = "查询(&P) ";
            this.iRefreshButton.Click += new System.EventHandler(this.iRefreshButton_Click);
            // 
            // iCloseButton
            // 
            this.iCloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.iCloseButton.ImageIndex = 3;
            this.iCloseButton.Location = new System.Drawing.Point(793, 9);
            this.iCloseButton.Name = "iCloseButton";
            this.iCloseButton.Size = new System.Drawing.Size(75, 25);
            this.iCloseButton.TabIndex = 6;
            this.iCloseButton.Text = " 关闭(&C)";
            this.iCloseButton.Click += new System.EventHandler(this.iCloseButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.busyIndicator);
            this.panel1.Controls.Add(this.gridControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panel1.Size = new System.Drawing.Size(875, 371);
            this.panel1.TabIndex = 6;
            // 
            // busyIndicator
            // 
            this.busyIndicator.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.busyIndicator.BackColor = System.Drawing.Color.Transparent;
            this.busyIndicator.Location = new System.Drawing.Point(326, 166);
            this.busyIndicator.Name = "busyIndicator";
            this.busyIndicator.Size = new System.Drawing.Size(116, 43);
            this.busyIndicator.TabIndex = 8;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 2);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEdit1,
            this.repositoryItemCustomGridLookUpEdit2});
            this.gridControl1.Size = new System.Drawing.Size(875, 367);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.CHANGETIME,
            this.PATIENT,
            this.HEMODIALYSIS_ID,
            this.SEX,
            this.AGE,
            this.DIAGNOSE,
            this.CONTENT,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn1});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "CAPTION", null, "(未审核:{0})")});
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridView1_InitNewRow);
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            this.gridView1.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanging);
            // 
            // CHANGETIME
            // 
            this.CHANGETIME.AppearanceHeader.Options.UseTextOptions = true;
            this.CHANGETIME.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CHANGETIME.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CHANGETIME.Caption = "日期";
            this.CHANGETIME.ColumnEdit = this.repositoryItemDateEdit1;
            this.CHANGETIME.FieldName = "CHANGETIME";
            this.CHANGETIME.Name = "CHANGETIME";
            this.CHANGETIME.Visible = true;
            this.CHANGETIME.VisibleIndex = 0;
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            this.repositoryItemDateEdit1.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // PATIENT
            // 
            this.PATIENT.AppearanceHeader.Options.UseTextOptions = true;
            this.PATIENT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PATIENT.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.PATIENT.Caption = "对应患者";
            this.PATIENT.ColumnEdit = this.repositoryItemCustomGridLookUpEdit2;
            this.PATIENT.FieldName = "PATIENT";
            this.PATIENT.Name = "PATIENT";
            this.PATIENT.Visible = true;
            this.PATIENT.VisibleIndex = 1;
            this.PATIENT.Width = 67;
            // 
            // repositoryItemCustomGridLookUpEdit2
            // 
            this.repositoryItemCustomGridLookUpEdit2.AutoComplete = false;
            this.repositoryItemCustomGridLookUpEdit2.AutoHeight = false;
            this.repositoryItemCustomGridLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCustomGridLookUpEdit2.DisplayMember = "NAME";
            this.repositoryItemCustomGridLookUpEdit2.Name = "repositoryItemCustomGridLookUpEdit2";
            this.repositoryItemCustomGridLookUpEdit2.NullText = "「请选择」";
            this.repositoryItemCustomGridLookUpEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.repositoryItemCustomGridLookUpEdit2.ValueMember = "HEMODIALYSIS_ID";
            this.repositoryItemCustomGridLookUpEdit2.View = this.repositoryItemCustomGridLookUpEdit2View;
            // 
            // repositoryItemCustomGridLookUpEdit2View
            // 
            this.repositoryItemCustomGridLookUpEdit2View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17});
            this.repositoryItemCustomGridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemCustomGridLookUpEdit2View.Name = "repositoryItemCustomGridLookUpEdit2View";
            this.repositoryItemCustomGridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemCustomGridLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "透析号";
            this.gridColumn9.FieldName = "HEMODIALYSIS_ID";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 0;
            this.gridColumn9.Width = 86;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "病人号";
            this.gridColumn10.FieldName = "PATIENT_ID";
            this.gridColumn10.Name = "gridColumn10";
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "名称";
            this.gridColumn11.FieldName = "NAME";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 1;
            this.gridColumn11.Width = 74;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "性别";
            this.gridColumn12.FieldName = "SEX";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 2;
            this.gridColumn12.Width = 66;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "年龄";
            this.gridColumn13.FieldName = "AGE";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 3;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "类别";
            this.gridColumn14.FieldName = "ITEM_TYPE";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 4;
            this.gridColumn14.Width = 80;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "民族";
            this.gridColumn15.FieldName = "NATION";
            this.gridColumn15.Name = "gridColumn15";
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "证件类型";
            this.gridColumn16.FieldName = "CREDENTIALS_TYPE";
            this.gridColumn16.Name = "gridColumn16";
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "证件号";
            this.gridColumn17.FieldName = "CREDENTIALS_NUMBER";
            this.gridColumn17.Name = "gridColumn17";
            // 
            // HEMODIALYSIS_ID
            // 
            this.HEMODIALYSIS_ID.AppearanceHeader.Options.UseTextOptions = true;
            this.HEMODIALYSIS_ID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.HEMODIALYSIS_ID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.HEMODIALYSIS_ID.Caption = "透析号";
            this.HEMODIALYSIS_ID.FieldName = "HEMODIALYSIS_ID";
            this.HEMODIALYSIS_ID.Name = "HEMODIALYSIS_ID";
            this.HEMODIALYSIS_ID.OptionsColumn.AllowEdit = false;
            this.HEMODIALYSIS_ID.OptionsColumn.AllowFocus = false;
            this.HEMODIALYSIS_ID.Visible = true;
            this.HEMODIALYSIS_ID.VisibleIndex = 2;
            this.HEMODIALYSIS_ID.Width = 67;
            // 
            // SEX
            // 
            this.SEX.Caption = "性别";
            this.SEX.FieldName = "SEX";
            this.SEX.Name = "SEX";
            this.SEX.OptionsColumn.AllowEdit = false;
            this.SEX.OptionsColumn.AllowFocus = false;
            this.SEX.Visible = true;
            this.SEX.VisibleIndex = 3;
            this.SEX.Width = 60;
            // 
            // AGE
            // 
            this.AGE.Caption = "年龄";
            this.AGE.FieldName = "AGE";
            this.AGE.Name = "AGE";
            this.AGE.OptionsColumn.AllowEdit = false;
            this.AGE.OptionsColumn.AllowFocus = false;
            this.AGE.Visible = true;
            this.AGE.VisibleIndex = 4;
            this.AGE.Width = 60;
            // 
            // DIAGNOSE
            // 
            this.DIAGNOSE.Caption = "诊断";
            this.DIAGNOSE.FieldName = "DIAGNOSE";
            this.DIAGNOSE.Name = "DIAGNOSE";
            this.DIAGNOSE.OptionsColumn.AllowEdit = false;
            this.DIAGNOSE.OptionsColumn.AllowFocus = false;
            this.DIAGNOSE.Visible = true;
            this.DIAGNOSE.VisibleIndex = 5;
            this.DIAGNOSE.Width = 67;
            // 
            // CONTENT
            // 
            this.CONTENT.Caption = "交班内容";
            this.CONTENT.FieldName = "CONTENT";
            this.CONTENT.Name = "CONTENT";
            this.CONTENT.Visible = true;
            this.CONTENT.VisibleIndex = 6;
            this.CONTENT.Width = 475;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "创建人";
            this.gridColumn7.FieldName = "CREATEBY";
            this.gridColumn7.Name = "gridColumn7";
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "创建时间";
            this.gridColumn8.FieldName = "CREATEDATE";
            this.gridColumn8.Name = "gridColumn8";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.FieldName = "NAME";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.endTime);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.beginTime);
            this.panelControl1.Controls.Add(this.btn_Print);
            this.panelControl1.Controls.Add(this.iRefreshButton);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(875, 40);
            this.panelControl1.TabIndex = 7;
            // 
            // DoctorChangeWork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 451);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "DoctorChangeWork";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "医生交班记录";
            this.Load += new System.EventHandler(this.DoctorChangeWork_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iFilterCheckEdit.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private Hemo.Client.Controls.DXSimpleButton iRefreshButton;
        private Hemo.Client.Controls.DXSimpleButton iCloseButton;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.CheckEdit iFilterCheckEdit;
        private Hemo.Client.Controls.DXSimpleButton iNewButton;
        private Hemo.Client.Controls.DXSimpleButton iDeleteButton;
        private Hemo.Client.Controls.DXSimpleButton iSaveButton;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn CHANGETIME;
        private DevExpress.XtraGrid.Columns.GridColumn PATIENT;
        private DevExpress.XtraGrid.Columns.GridColumn HEMODIALYSIS_ID;
        private BusyIndicator busyIndicator;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private Utilities.RepositoryItemCustomGridLookUpEdit repositoryItemCustomGridLookUpEdit2;
        private Utilities.CustomGridView repositoryItemCustomGridLookUpEdit2View;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit endTime;
        private DevExpress.XtraEditors.DateEdit beginTime;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DXSimpleButton btn_Print;
        private DevExpress.XtraGrid.Columns.GridColumn SEX;
        private DevExpress.XtraGrid.Columns.GridColumn AGE;
        private DevExpress.XtraGrid.Columns.GridColumn DIAGNOSE;
        private DevExpress.XtraGrid.Columns.GridColumn CONTENT;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}
