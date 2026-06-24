namespace Hemo.Client.UI.Machine
{
    partial class ReUsableRecord
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReUsableRecord));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_Query = new Hemo.Client.Controls.DXSimpleButton();
            this.btn_Print = new Hemo.Client.Controls.DXSimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEdit_machine = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.endTime = new DevExpress.XtraEditors.DateEdit();
            this.beginTime = new DevExpress.XtraEditors.DateEdit();
            this.btn_Entering = new Hemo.Client.Controls.DXSimpleButton();
            this.largeImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.gcMachine = new DevExpress.XtraGrid.GridControl();
            this.gvMachine = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.busyIndicator1 = new Hemo.Client.Controls.BusyIndicator();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolDelete = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit_machine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.largeImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMachine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMachine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_Query);
            this.panelControl1.Controls.Add(this.btn_Print);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.lookUpEdit_machine);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.endTime);
            this.panelControl1.Controls.Add(this.beginTime);
            this.panelControl1.Controls.Add(this.btn_Entering);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 361);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(783, 37);
            this.panelControl1.TabIndex = 0;
            // 
            // btn_Query
            // 
            this.btn_Query.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Query.ImageIndex = 8;
            this.btn_Query.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_Query.Location = new System.Drawing.Point(545, 6);
            this.btn_Query.Name = "btn_Query";
            this.btn_Query.Size = new System.Drawing.Size(73, 25);
            this.btn_Query.TabIndex = 19;
            this.btn_Query.Text = "查询(&Q)";
            this.btn_Query.Click += new System.EventHandler(this.btn_Query_Click);
            // 
            // btn_Print
            // 
            this.btn_Print.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Print.ImageIndex = 6;
            this.btn_Print.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_Print.Location = new System.Drawing.Point(703, 6);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(73, 25);
            this.btn_Print.TabIndex = 18;
            this.btn_Print.Text = "打印(&P)";
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(7, 10);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 14);
            this.labelControl2.TabIndex = 17;
            this.labelControl2.Text = "透析器型号：";
            // 
            // lookUpEdit_machine
            // 
            this.lookUpEdit_machine.EditValue = "";
            this.lookUpEdit_machine.EnterMoveNextControl = true;
            this.lookUpEdit_machine.Location = new System.Drawing.Point(83, 6);
            this.lookUpEdit_machine.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lookUpEdit_machine.Name = "lookUpEdit_machine";
            this.lookUpEdit_machine.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lookUpEdit_machine.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lookUpEdit_machine.Properties.Appearance.Options.UseFont = true;
            this.lookUpEdit_machine.Properties.Appearance.Options.UseForeColor = true;
            this.lookUpEdit_machine.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit_machine.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MACHINE_NAME", "名称", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FLNAME", "分类"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MACHINE_MODEL", "型号"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("THERAPEUTIC_PROPERTIES", "治疗方式"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MACHINE_ID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("QYNAME", "区域"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CWNAME", "床位")});
            this.lookUpEdit_machine.Properties.DisplayMember = "MACHINE_MODEL";
            this.lookUpEdit_machine.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.lookUpEdit_machine.Properties.NullText = "";
            this.lookUpEdit_machine.Properties.ValueMember = "MACHINE_ID";
            this.lookUpEdit_machine.Size = new System.Drawing.Size(116, 23);
            this.lookUpEdit_machine.TabIndex = 16;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(374, 10);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "结束日期：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(211, 9);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "开始日期：";
            // 
            // endTime
            // 
            this.endTime.EditValue = null;
            this.endTime.Location = new System.Drawing.Point(434, 7);
            this.endTime.Name = "endTime";
            this.endTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.endTime.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.endTime.Size = new System.Drawing.Size(100, 21);
            this.endTime.TabIndex = 2;
            // 
            // beginTime
            // 
            this.beginTime.EditValue = null;
            this.beginTime.Location = new System.Drawing.Point(272, 6);
            this.beginTime.Name = "beginTime";
            this.beginTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.beginTime.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beginTime.Size = new System.Drawing.Size(91, 21);
            this.beginTime.TabIndex = 2;
            // 
            // btn_Entering
            // 
            this.btn_Entering.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Entering.ImageIndex = 0;
            this.btn_Entering.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_Entering.Location = new System.Drawing.Point(624, 6);
            this.btn_Entering.Name = "btn_Entering";
            this.btn_Entering.Size = new System.Drawing.Size(73, 25);
            this.btn_Entering.TabIndex = 1;
            this.btn_Entering.Text = "新增(&A)";
            this.btn_Entering.Click += new System.EventHandler(this.btn_Entering_Click);
            // 
            // largeImageCollection
            // 
            this.largeImageCollection.ImageSize = new System.Drawing.Size(32, 32);
            this.largeImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("largeImageCollection.ImageStream")));
            this.largeImageCollection.Images.SetKeyName(10, "DatabaseCopyDatabaseFile.png");
            this.largeImageCollection.Images.SetKeyName(11, "DatabaseSqlServer.png");
            this.largeImageCollection.Images.SetKeyName(12, "GroupInsertTables.png");
            this.largeImageCollection.Images.SetKeyName(13, "CellsDeleteSmart.png");
            this.largeImageCollection.Images.SetKeyName(14, "CellsInsertSmart.png");
            this.largeImageCollection.Images.SetKeyName(15, "FileEmailAsPdfEmailAttachment.png");
            this.largeImageCollection.Images.SetKeyName(16, "FooterInsertGallery.png");
            this.largeImageCollection.Images.SetKeyName(17, "FormatPainter.png");
            this.largeImageCollection.Images.SetKeyName(18, "GoToFooter.png");
            this.largeImageCollection.Images.SetKeyName(19, "GoToHeader.png");
            this.largeImageCollection.Images.SetKeyName(20, "GroupHeaderFooter.png");
            this.largeImageCollection.Images.SetKeyName(21, "GroupProtect.png");
            this.largeImageCollection.Images.SetKeyName(22, "GroupTableStylesExcel.png");
            this.largeImageCollection.Images.SetKeyName(23, "ReviewDeleteCommentPowerPoint.png");
            this.largeImageCollection.Images.SetKeyName(24, "ReviewNewComment.png");
            this.largeImageCollection.Images.SetKeyName(25, "ReviewNextCommentPowerPoint.png");
            this.largeImageCollection.Images.SetKeyName(26, "ReviewPreviousComment.png");
            this.largeImageCollection.Images.SetKeyName(27, "ReviewTrackChangesMenu.png");
            this.largeImageCollection.Images.SetKeyName(28, "Spelling.png");
            this.largeImageCollection.Images.SetKeyName(29, "AcceptChanges.png");
            this.largeImageCollection.Images.SetKeyName(30, "DesignMode.png");
            this.largeImageCollection.Images.SetKeyName(31, "DropCapInsertGallery.png");
            this.largeImageCollection.Images.SetKeyName(32, "FileMarkAsFinal.png");
            this.largeImageCollection.Images.SetKeyName(33, "ReviewRejectChange.png");
            this.largeImageCollection.Images.SetKeyName(34, "ViewNormalViewPowerPoint.png");
            this.largeImageCollection.Images.SetKeyName(35, "WordArtEditTextClassic.png");
            this.largeImageCollection.Images.SetKeyName(36, "ExchangeFolder.png");
            this.largeImageCollection.Images.SetKeyName(37, "ZoomClassic.png");
            this.largeImageCollection.Images.SetKeyName(38, "ViewsFormView.png");
            this.largeImageCollection.Images.SetKeyName(39, "BookmarkInsert.png");
            this.largeImageCollection.Images.SetKeyName(40, "CreateFormInDesignView.png");
            this.largeImageCollection.Images.SetKeyName(41, "CreateFormSplitForm.png");
            this.largeImageCollection.Images.SetKeyName(42, "ViewsFormView.png");
            this.largeImageCollection.Images.SetKeyName(43, "CreateFormBlankForm.png");
            this.largeImageCollection.Images.SetKeyName(44, "CreateForm.png");
            this.largeImageCollection.Images.SetKeyName(45, "CreateReport.png");
            this.largeImageCollection.Images.SetKeyName(46, "PageNambersInMarginsInsertGallery.png");
            this.largeImageCollection.Images.SetKeyName(47, "WindowNew.png");
            this.largeImageCollection.Images.SetKeyName(48, "WindowsSwitch.png");
            this.largeImageCollection.Images.SetKeyName(49, "tableselect.png");
            this.largeImageCollection.Images.SetKeyName(50, "CustomTableOfContentsGallery.png");
            this.largeImageCollection.Images.SetKeyName(51, "zoom-.png");
            this.largeImageCollection.Images.SetKeyName(52, "zoom+.png");
            this.largeImageCollection.Images.SetKeyName(53, "ControlProperties.png");
            this.largeImageCollection.Images.SetKeyName(54, "DocumentPanelTemplate.png");
            this.largeImageCollection.Images.SetKeyName(55, "Play.png");
            this.largeImageCollection.Images.SetKeyName(56, "Add Green Button.png");
            this.largeImageCollection.Images.SetKeyName(57, "Add.png");
            this.largeImageCollection.Images.SetKeyName(58, "Clear Green Button.png");
            this.largeImageCollection.Images.SetKeyName(59, "Import Document.png");
            this.largeImageCollection.Images.SetKeyName(60, "Pre.png");
            this.largeImageCollection.Images.SetKeyName(61, "Play Blue Button.png");
            this.largeImageCollection.Images.SetKeyName(62, "Play Green Button.png");
            this.largeImageCollection.Images.SetKeyName(63, "Red Ball.png");
            this.largeImageCollection.Images.SetKeyName(64, "Stop All.png");
            this.largeImageCollection.Images.SetKeyName(65, "Stop Green Button.png");
            this.largeImageCollection.Images.SetKeyName(66, "Stop Red Button.png");
            this.largeImageCollection.Images.SetKeyName(67, "Stop.png");
            this.largeImageCollection.Images.SetKeyName(68, "RefreshAll.png");
            this.largeImageCollection.Images.SetKeyName(69, "Refresh.png");
            this.largeImageCollection.Images.SetKeyName(70, "AddOrRemoveAttendees.png");
            this.largeImageCollection.Images.SetKeyName(71, "AddressBook.png");
            this.largeImageCollection.Images.SetKeyName(72, "1.png");
            this.largeImageCollection.Images.SetKeyName(73, "2.png");
            this.largeImageCollection.Images.SetKeyName(74, "3.png");
            this.largeImageCollection.Images.SetKeyName(75, "4.png");
            this.largeImageCollection.Images.SetKeyName(76, "5.png");
            this.largeImageCollection.Images.SetKeyName(77, "6.png");
            this.largeImageCollection.Images.SetKeyName(78, "7.png");
            this.largeImageCollection.Images.SetKeyName(79, "8.png");
            this.largeImageCollection.Images.SetKeyName(80, "9.png");
            this.largeImageCollection.Images.SetKeyName(81, "10.png");
            this.largeImageCollection.Images.SetKeyName(82, "11.png");
            this.largeImageCollection.Images.SetKeyName(83, "12.png");
            this.largeImageCollection.Images.SetKeyName(84, "13.png");
            this.largeImageCollection.Images.SetKeyName(85, "14.png");
            this.largeImageCollection.Images.SetKeyName(86, "15.png");
            this.largeImageCollection.Images.SetKeyName(87, "16.png");
            this.largeImageCollection.Images.SetKeyName(88, "17.png");
            this.largeImageCollection.Images.SetKeyName(89, "18.png");
            this.largeImageCollection.Images.SetKeyName(90, "19.png");
            this.largeImageCollection.Images.SetKeyName(91, "20.png");
            this.largeImageCollection.Images.SetKeyName(92, "21.png");
            this.largeImageCollection.Images.SetKeyName(93, "22.png");
            this.largeImageCollection.Images.SetKeyName(94, "23.png");
            this.largeImageCollection.Images.SetKeyName(95, "24.png");
            this.largeImageCollection.Images.SetKeyName(96, "25.png");
            this.largeImageCollection.Images.SetKeyName(97, "26.png");
            this.largeImageCollection.Images.SetKeyName(98, "27.png");
            this.largeImageCollection.Images.SetKeyName(99, "28.png");
            this.largeImageCollection.Images.SetKeyName(100, "29.png");
            this.largeImageCollection.Images.SetKeyName(101, "30.png");
            this.largeImageCollection.Images.SetKeyName(102, "31.png");
            this.largeImageCollection.Images.SetKeyName(103, "32.png");
            this.largeImageCollection.Images.SetKeyName(104, "Action_Save_32x32.png");
            this.largeImageCollection.Images.SetKeyName(105, "add.png");
            this.largeImageCollection.Images.SetKeyName(106, "del.png");
            this.largeImageCollection.Images.SetKeyName(107, "edit.png");
            this.largeImageCollection.Images.SetKeyName(108, "exit.png");
            this.largeImageCollection.Images.SetKeyName(109, "export.png");
            this.largeImageCollection.Images.SetKeyName(110, "preview.png");
            this.largeImageCollection.Images.SetKeyName(111, "print.png");
            this.largeImageCollection.Images.SetKeyName(112, "save.png");
            this.largeImageCollection.Images.SetKeyName(113, "search_16.png");
            // 
            // gcMachine
            // 
            this.gcMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMachine.Location = new System.Drawing.Point(0, 0);
            this.gcMachine.MainView = this.gvMachine;
            this.gcMachine.Name = "gcMachine";
            this.gcMachine.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1,
            this.repositoryItemLookUpEdit2});
            this.gcMachine.Size = new System.Drawing.Size(783, 361);
            this.gcMachine.TabIndex = 308;
            this.gcMachine.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMachine});
            this.gcMachine.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gcMachine_MouseDown);
            // 
            // gvMachine
            // 
            this.gvMachine.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16});
            this.gvMachine.GridControl = this.gcMachine;
            this.gvMachine.Name = "gvMachine";
            this.gvMachine.OptionsBehavior.Editable = false;
            this.gvMachine.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gvMachine.OptionsView.ShowGroupPanel = false;
            this.gvMachine.DoubleClick += new System.EventHandler(this.gvMachine_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "HEMODIALYSIS_ID";
            this.gridColumn2.FieldName = "HEMODIALYSIS_ID";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "血液透析器型号";
            this.gridColumn3.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.gridColumn3.FieldName = "MACHINETYPE";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 4;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ITEM_ID", "ID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ITEM_NAME", "名称")});
            this.repositoryItemLookUpEdit1.DisplayMember = "ITEM_NAME";
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.NullText = "";
            this.repositoryItemLookUpEdit1.ValueMember = "ITEM_ID";
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "首次使用";
            this.gridColumn4.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn4.FieldName = "FIRSTUSETIME";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "复用日期";
            this.gridColumn5.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn5.FieldName = "REUSEDATETIME";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "复用情况次数";
            this.gridColumn6.FieldName = "REUSECOUNT";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "工作人员签名";
            this.gridColumn7.FieldName = "PRIMARY_NURSE";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "TCV容积检测";
            this.gridColumn8.FieldName = "TCVCHECK";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 5;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "DIALYZERLAB";
            this.gridColumn9.FieldName = "DIALYZERLAB";
            this.gridColumn9.Name = "gridColumn9";
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "DISINFECTANTLD";
            this.gridColumn10.FieldName = "DISINFECTANTLD";
            this.gridColumn10.Name = "gridColumn10";
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "DISINFECTANTCL";
            this.gridColumn11.FieldName = "DISINFECTANTCL";
            this.gridColumn11.Name = "gridColumn11";
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "PREDIAPPEARANCE";
            this.gridColumn12.FieldName = "PREDIAPPEARANCE";
            this.gridColumn12.Name = "gridColumn12";
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "BACKDIAPPEARANCE";
            this.gridColumn13.FieldName = "BACKDIAPPEARANCE";
            this.gridColumn13.Name = "gridColumn13";
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "PROGRAMCHECK";
            this.gridColumn14.FieldName = "PROGRAMCHECK";
            this.gridColumn14.Name = "gridColumn14";
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "FLUXCHECK";
            this.gridColumn15.FieldName = "FLUXCHECK";
            this.gridColumn15.Name = "gridColumn15";
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "CREATEDATE";
            this.gridColumn16.FieldName = "CREATEDATE";
            this.gridColumn16.Name = "gridColumn16";
            // 
            // repositoryItemLookUpEdit2
            // 
            this.repositoryItemLookUpEdit2.AutoHeight = false;
            this.repositoryItemLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit2.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("EMP_NO", "ID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAME", "姓名")});
            this.repositoryItemLookUpEdit2.DisplayMember = "NAME";
            this.repositoryItemLookUpEdit2.Name = "repositoryItemLookUpEdit2";
            this.repositoryItemLookUpEdit2.NullText = "";
            this.repositoryItemLookUpEdit2.ValueMember = "EMP_NO";
            // 
            // busyIndicator1
            // 
            this.busyIndicator1.Location = new System.Drawing.Point(286, 161);
            this.busyIndicator1.Name = "busyIndicator1";
            this.busyIndicator1.Size = new System.Drawing.Size(86, 34);
            this.busyIndicator1.TabIndex = 309;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // toolDelete
            // 
            this.toolDelete.Name = "toolDelete";
            this.toolDelete.Size = new System.Drawing.Size(100, 22);
            this.toolDelete.Text = "删除";
            this.toolDelete.Click += new System.EventHandler(this.toolDelete_Click);
            // 
            // ReUsableRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 398);
            this.Controls.Add(this.busyIndicator1);
            this.Controls.Add(this.gcMachine);
            this.Controls.Add(this.panelControl1);
            this.Name = "ReUsableRecord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "复用记录";
            this.Load += new System.EventHandler(this.ReUsableRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit_machine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.largeImageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMachine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMachine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gcMachine;
        public DevExpress.XtraGrid.Views.Grid.GridView gvMachine;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private Hemo.Client.Controls.DXSimpleButton btn_Entering;
        private DevExpress.Utils.ImageCollection largeImageCollection;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit beginTime;
        private Hemo.Client.Controls.DXSimpleButton btn_Query;
        private Hemo.Client.Controls.DXSimpleButton btn_Print;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit_machine;
        private DevExpress.XtraEditors.DateEdit endTime;
        private Controls.BusyIndicator busyIndicator1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolDelete;
        private DevExpress.XtraEditors.LabelControl labelControl3;

    }
}