namespace Hemo.Client.UI.Hemodialysis
{
    partial class BorrowMedicineFrm
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
            this.gridBrowList = new DevExpress.XtraGrid.GridControl();
            this.mnuGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolBack = new System.Windows.Forms.ToolStripMenuItem();
            this.gridBrowListView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPatientName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBrowDay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDrugName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDrugUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBrowOut = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colISBack = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colBackUser = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnQuery = new Hemo.Client.Controls.DXSimpleButton();
            this.btnPrint = new Hemo.Client.Controls.DXSimpleButton();
            this.labelControl30 = new DevExpress.XtraEditors.LabelControl();
            this.txtStart = new DevExpress.XtraEditors.DateEdit();
            this.txtEnd = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtPatientName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.btnAdd = new Hemo.Client.Controls.DXSimpleButton();
            this.btn_Cancle = new Hemo.Client.Controls.DXSimpleButton();
            this.btnClose = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridBrowList)).BeginInit();
            this.mnuGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBrowListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStart.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEnd.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPatientName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridBrowList
            // 
            this.gridBrowList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridBrowList.ContextMenuStrip = this.mnuGrid;
            this.gridBrowList.Location = new System.Drawing.Point(2, 75);
            this.gridBrowList.MainView = this.gridBrowListView;
            this.gridBrowList.Name = "gridBrowList";
            this.gridBrowList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gridBrowList.Size = new System.Drawing.Size(864, 363);
            this.gridBrowList.TabIndex = 3;
            this.gridBrowList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridBrowListView});
            // 
            // mnuGrid
            // 
            this.mnuGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBack});
            this.mnuGrid.Name = "mnuGrid";
            this.mnuGrid.Size = new System.Drawing.Size(125, 26);
            // 
            // toolBack
            // 
            this.toolBack.Name = "toolBack";
            this.toolBack.Size = new System.Drawing.Size(124, 22);
            this.toolBack.Text = "药品归还";
            this.toolBack.Click += new System.EventHandler(this.toolBack_Click);
            // 
            // gridBrowListView
            // 
            this.gridBrowListView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPatientName,
            this.colBrowDay,
            this.colDrugName,
            this.colCount,
            this.colDrugUnit,
            this.colBrowOut,
            this.colISBack,
            this.colBackUser});
            this.gridBrowListView.GridControl = this.gridBrowList;
            this.gridBrowListView.Name = "gridBrowListView";
            this.gridBrowListView.OptionsView.ColumnAutoWidth = false;
            this.gridBrowListView.OptionsView.ShowGroupPanel = false;
            // 
            // colPatientName
            // 
            this.colPatientName.AppearanceHeader.Options.UseTextOptions = true;
            this.colPatientName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPatientName.Caption = "姓  名";
            this.colPatientName.FieldName = "PATIENTNAME";
            this.colPatientName.Name = "colPatientName";
            this.colPatientName.OptionsColumn.AllowEdit = false;
            this.colPatientName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPatientName.OptionsColumn.ReadOnly = true;
            this.colPatientName.OptionsFilter.AllowAutoFilter = false;
            this.colPatientName.OptionsFilter.AllowFilter = false;
            this.colPatientName.Visible = true;
            this.colPatientName.VisibleIndex = 0;
            this.colPatientName.Width = 89;
            // 
            // colBrowDay
            // 
            this.colBrowDay.AppearanceHeader.Options.UseTextOptions = true;
            this.colBrowDay.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBrowDay.Caption = "借药日期";
            this.colBrowDay.FieldName = "BORROW_DAY";
            this.colBrowDay.Name = "colBrowDay";
            this.colBrowDay.OptionsColumn.AllowEdit = false;
            this.colBrowDay.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colBrowDay.OptionsColumn.ReadOnly = true;
            this.colBrowDay.OptionsFilter.AllowAutoFilter = false;
            this.colBrowDay.OptionsFilter.AllowFilter = false;
            this.colBrowDay.Visible = true;
            this.colBrowDay.VisibleIndex = 1;
            this.colBrowDay.Width = 109;
            // 
            // colDrugName
            // 
            this.colDrugName.AppearanceHeader.Options.UseTextOptions = true;
            this.colDrugName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDrugName.Caption = "药品名称";
            this.colDrugName.FieldName = "MEDICINE_NAME";
            this.colDrugName.Name = "colDrugName";
            this.colDrugName.OptionsColumn.AllowEdit = false;
            this.colDrugName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDrugName.OptionsColumn.ReadOnly = true;
            this.colDrugName.OptionsFilter.AllowAutoFilter = false;
            this.colDrugName.OptionsFilter.AllowFilter = false;
            this.colDrugName.Visible = true;
            this.colDrugName.VisibleIndex = 2;
            this.colDrugName.Width = 276;
            // 
            // colCount
            // 
            this.colCount.AppearanceHeader.Options.UseTextOptions = true;
            this.colCount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCount.Caption = "数量";
            this.colCount.FieldName = "MEDICINE_COUNT";
            this.colCount.Name = "colCount";
            this.colCount.OptionsColumn.AllowEdit = false;
            this.colCount.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colCount.OptionsColumn.ReadOnly = true;
            this.colCount.OptionsFilter.AllowAutoFilter = false;
            this.colCount.OptionsFilter.AllowFilter = false;
            this.colCount.Visible = true;
            this.colCount.VisibleIndex = 3;
            // 
            // colDrugUnit
            // 
            this.colDrugUnit.AppearanceHeader.Options.UseTextOptions = true;
            this.colDrugUnit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDrugUnit.Caption = "药品单位";
            this.colDrugUnit.FieldName = "MEDICINE_UNIT";
            this.colDrugUnit.Name = "colDrugUnit";
            this.colDrugUnit.OptionsColumn.AllowEdit = false;
            this.colDrugUnit.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDrugUnit.OptionsColumn.ReadOnly = true;
            this.colDrugUnit.OptionsFilter.AllowAutoFilter = false;
            this.colDrugUnit.OptionsFilter.AllowFilter = false;
            this.colDrugUnit.Visible = true;
            this.colDrugUnit.VisibleIndex = 4;
            this.colDrugUnit.Width = 61;
            // 
            // colBrowOut
            // 
            this.colBrowOut.AppearanceHeader.Options.UseTextOptions = true;
            this.colBrowOut.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBrowOut.Caption = "借出操作人";
            this.colBrowOut.FieldName = "BORROW_USER";
            this.colBrowOut.Name = "colBrowOut";
            this.colBrowOut.OptionsColumn.AllowEdit = false;
            this.colBrowOut.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colBrowOut.OptionsColumn.ReadOnly = true;
            this.colBrowOut.OptionsFilter.AllowAutoFilter = false;
            this.colBrowOut.OptionsFilter.AllowFilter = false;
            this.colBrowOut.Visible = true;
            this.colBrowOut.VisibleIndex = 5;
            this.colBrowOut.Width = 79;
            // 
            // colISBack
            // 
            this.colISBack.AppearanceHeader.Options.UseTextOptions = true;
            this.colISBack.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colISBack.Caption = "是否归还";
            this.colISBack.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colISBack.FieldName = "ISBACK";
            this.colISBack.Name = "colISBack";
            this.colISBack.OptionsColumn.AllowEdit = false;
            this.colISBack.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colISBack.OptionsColumn.ReadOnly = true;
            this.colISBack.OptionsFilter.AllowAutoFilter = false;
            this.colISBack.OptionsFilter.AllowFilter = false;
            this.colISBack.Visible = true;
            this.colISBack.VisibleIndex = 6;
            this.colISBack.Width = 76;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repositoryItemCheckEdit1.ValueChecked = "1";
            this.repositoryItemCheckEdit1.ValueGrayed = "0";
            this.repositoryItemCheckEdit1.ValueUnchecked = "0";
            // 
            // colBackUser
            // 
            this.colBackUser.AppearanceHeader.Options.UseTextOptions = true;
            this.colBackUser.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBackUser.Caption = "归还操作人";
            this.colBackUser.FieldName = "BBORROW_USER";
            this.colBackUser.Name = "colBackUser";
            this.colBackUser.OptionsColumn.AllowEdit = false;
            this.colBackUser.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colBackUser.OptionsColumn.ReadOnly = true;
            this.colBackUser.OptionsFilter.AllowAutoFilter = false;
            this.colBackUser.OptionsFilter.AllowFilter = false;
            this.colBackUser.Visible = true;
            this.colBackUser.VisibleIndex = 7;
            // 
            // btnQuery
            // 
            this.btnQuery.ImageIndex = 8;
            this.btnQuery.Location = new System.Drawing.Point(339, 46);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 25);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.ImageIndex = 6;
            this.btnPrint.Location = new System.Drawing.Point(582, 46);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 25);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.Text = "打印(&P)";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // labelControl30
            // 
            this.labelControl30.Location = new System.Drawing.Point(8, 15);
            this.labelControl30.Name = "labelControl30";
            this.labelControl30.Size = new System.Drawing.Size(52, 14);
            this.labelControl30.TabIndex = 576;
            this.labelControl30.Text = "开始日期:";
            // 
            // txtStart
            // 
            this.txtStart.EditValue = null;
            this.txtStart.EnterMoveNextControl = true;
            this.txtStart.Location = new System.Drawing.Point(71, 11);
            this.txtStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtStart.Name = "txtStart";
            this.txtStart.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtStart.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtStart.Properties.Appearance.Options.UseFont = true;
            this.txtStart.Properties.Appearance.Options.UseForeColor = true;
            this.txtStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtStart.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtStart.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtStart.Size = new System.Drawing.Size(208, 23);
            this.txtStart.TabIndex = 575;
            // 
            // txtEnd
            // 
            this.txtEnd.EditValue = null;
            this.txtEnd.EnterMoveNextControl = true;
            this.txtEnd.Location = new System.Drawing.Point(384, 11);
            this.txtEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtEnd.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtEnd.Properties.Appearance.Options.UseFont = true;
            this.txtEnd.Properties.Appearance.Options.UseForeColor = true;
            this.txtEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtEnd.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtEnd.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtEnd.Size = new System.Drawing.Size(201, 23);
            this.txtEnd.TabIndex = 575;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(321, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 14);
            this.labelControl1.TabIndex = 576;
            this.labelControl1.Text = "结束日期:";
            // 
            // txtPatientName
            // 
            this.txtPatientName.Location = new System.Drawing.Point(700, 11);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtPatientName.Properties.Appearance.Options.UseForeColor = true;
            this.txtPatientName.Size = new System.Drawing.Size(156, 21);
            this.txtPatientName.TabIndex = 577;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(637, 14);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 14);
            this.labelControl2.TabIndex = 578;
            this.labelControl2.Text = "患者姓名:";
            // 
            // radioGroup1
            // 
            this.radioGroup1.EditValue = "0";
            this.radioGroup1.Location = new System.Drawing.Point(12, 41);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "全部"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "已还"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("2", "未还")});
            this.radioGroup1.Size = new System.Drawing.Size(267, 28);
            this.radioGroup1.TabIndex = 579;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.ImageIndex = 0;
            this.btnAdd.Location = new System.Drawing.Point(420, 46);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 25);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "新增(&A)";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btn_Cancle
            // 
            this.btn_Cancle.ImageIndex = 5;
            this.btn_Cancle.Location = new System.Drawing.Point(501, 46);
            this.btn_Cancle.Name = "btn_Cancle";
            this.btn_Cancle.Size = new System.Drawing.Size(75, 25);
            this.btn_Cancle.TabIndex = 580;
            this.btn_Cancle.Text = "归还(&G)";
            this.btn_Cancle.Click += new System.EventHandler(this.btn_Cancle_Click);
            // 
            // btnClose
            // 
            this.btnClose.ImageIndex = 3;
            this.btnClose.Location = new System.Drawing.Point(663, 46);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 25);
            this.btnClose.TabIndex = 581;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // BorrowMedicineFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 439);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btn_Cancle);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.labelControl30);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtPatientName);
            this.Controls.Add(this.txtStart);
            this.Controls.Add(this.txtEnd);
            this.Controls.Add(this.gridBrowList);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnPrint);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(884, 478);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(884, 477);
            this.Name = "BorrowMedicineFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "患者借还药管理";
            this.Load += new System.EventHandler(this.ShowSummary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridBrowList)).EndInit();
            this.mnuGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridBrowListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStart.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEnd.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPatientName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridBrowList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridBrowListView;
        private DevExpress.XtraGrid.Columns.GridColumn colPatientName;
        private DevExpress.XtraGrid.Columns.GridColumn colBrowDay;
        private DevExpress.XtraGrid.Columns.GridColumn colDrugName;
        private DevExpress.XtraGrid.Columns.GridColumn colDrugUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colBrowOut;
        private DevExpress.XtraGrid.Columns.GridColumn colISBack;
        private DevExpress.XtraGrid.Columns.GridColumn colBackUser;
        private Hemo.Client.Controls.DXSimpleButton btnQuery;
        private Hemo.Client.Controls.DXSimpleButton btnPrint;
        private DevExpress.XtraEditors.LabelControl labelControl30;
        private DevExpress.XtraEditors.DateEdit txtStart;
        private DevExpress.XtraEditors.DateEdit txtEnd;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtPatientName;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private Hemo.Client.Controls.DXSimpleButton btnAdd;
        private System.Windows.Forms.ContextMenuStrip mnuGrid;
        private System.Windows.Forms.ToolStripMenuItem toolBack;
        private DevExpress.XtraGrid.Columns.GridColumn colCount;
        private Hemo.Client.Controls.DXSimpleButton  btn_Cancle;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private Controls.DXSimpleButton btnClose;
    }
}