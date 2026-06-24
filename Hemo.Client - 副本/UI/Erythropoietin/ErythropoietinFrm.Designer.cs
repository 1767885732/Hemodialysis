namespace Hemo.Client.UI.Erythropoietin
{
    partial class ErythropoietinFrm
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
            this.components = new System.ComponentModel.Container();
            this.gcMainInfo = new DevExpress.XtraGrid.GridControl();
            this.gvMainInfo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.btnAdd = new Hemo.Client.Controls.DXSimpleButton();
            this.btnEdit = new Hemo.Client.Controls.DXSimpleButton();
            this.btnExec = new Hemo.Client.Controls.DXSimpleButton();
            this.gcExecInfo = new DevExpress.XtraGrid.GridControl();
            this.gvExecInfo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemTextEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lab1 = new DevExpress.XtraEditors.LabelControl();
            this.deBeginExecDate = new DevExpress.XtraEditors.DateEdit();
            this.lab2 = new DevExpress.XtraEditors.LabelControl();
            this.deEndExecDate = new DevExpress.XtraEditors.DateEdit();
            this.btnSearchExecInfo = new Hemo.Client.Controls.DXSimpleButton();
            this.gcExec = new DevExpress.XtraEditors.GroupControl();
            this.btnClose = new Hemo.Client.Controls.DXSimpleButton();
            this.btnClose1 = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcMainInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMainInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcExecInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvExecInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBeginExecDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBeginExecDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndExecDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndExecDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcExec)).BeginInit();
            this.gcExec.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcMainInfo
            // 
            this.gcMainInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcMainInfo.Location = new System.Drawing.Point(12, 12);
            this.gcMainInfo.MainView = this.gvMainInfo;
            this.gcMainInfo.Name = "gcMainInfo";
            this.gcMainInfo.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2});
            this.gcMainInfo.Size = new System.Drawing.Size(806, 180);
            this.gcMainInfo.TabIndex = 6;
            this.gcMainInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMainInfo});
            // 
            // gvMainInfo
            // 
            this.gvMainInfo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7});
            this.gvMainInfo.GridControl = this.gcMainInfo;
            this.gvMainInfo.Name = "gvMainInfo";
            this.gvMainInfo.OptionsBehavior.Editable = false;
            this.gvMainInfo.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gvMainInfo.OptionsView.ShowGroupPanel = false;
            this.gvMainInfo.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvMainInfo_RowClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "床位";
            this.gridColumn1.FieldName = "BED_NO";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 60;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "病人";
            this.gridColumn2.FieldName = "PATIENTNAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 60;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "测定时间";
            this.gridColumn3.ColumnEdit = this.repositoryItemTextEdit2;
            this.gridColumn3.FieldName = "CREATE_TIME";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 120;
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "药品";
            this.gridColumn4.FieldName = "DRUG_NAME";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 150;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "频次";
            this.gridColumn5.FieldName = "FREQUENCYSTR";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 150;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "上次用药时间";
            this.gridColumn6.FieldName = "LAST_EXCUTE_DATESTR";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 120;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "备注";
            this.gridColumn7.FieldName = "REMARK";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 125;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.DisplayValueChecked = "true";
            this.repositoryItemCheckEdit1.DisplayValueUnchecked = "false";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.ImageIndex = 0;
            this.btnAdd.Location = new System.Drawing.Point(545, 198);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(87, 27);
            this.btnAdd.TabIndex = 19;
            this.btnAdd.Text = "新增";
            this.btnAdd.Click += new System.EventHandler(this.btnOpt_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.ImageIndex = 2;
            this.btnEdit.Location = new System.Drawing.Point(638, 198);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(87, 27);
            this.btnEdit.TabIndex = 21;
            this.btnEdit.Text = "编辑";
            this.btnEdit.Click += new System.EventHandler(this.btnOpt_Click);
            // 
            // btnExec
            // 
            this.btnExec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExec.ImageIndex = 7;
            this.btnExec.Location = new System.Drawing.Point(621, 245);
            this.btnExec.Name = "btnExec";
            this.btnExec.Size = new System.Drawing.Size(87, 27);
            this.btnExec.TabIndex = 20;
            this.btnExec.Text = "执行";
            this.btnExec.Click += new System.EventHandler(this.btnExec_Click);
            // 
            // gcExecInfo
            // 
            this.gcExecInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcExecInfo.Location = new System.Drawing.Point(5, 59);
            this.gcExecInfo.MainView = this.gvExecInfo;
            this.gcExecInfo.Name = "gcExecInfo";
            this.gcExecInfo.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit2,
            this.repositoryItemTextEdit4,
            this.repositoryItemTextEdit3,
            this.repositoryItemTextEdit5});
            this.gcExecInfo.Size = new System.Drawing.Size(796, 180);
            this.gcExecInfo.TabIndex = 21;
            this.gcExecInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvExecInfo,
            this.gridView1});
            // 
            // gvExecInfo
            // 
            this.gvExecInfo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13});
            this.gvExecInfo.GridControl = this.gcExecInfo;
            this.gvExecInfo.Name = "gvExecInfo";
            this.gvExecInfo.OptionsBehavior.Editable = false;
            this.gvExecInfo.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gvExecInfo.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "类型";
            this.gridColumn8.FieldName = "ERYTHROPOIETIN_TYPE";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 0;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "执行时间";
            this.gridColumn9.ColumnEdit = this.repositoryItemTextEdit5;
            this.gridColumn9.FieldName = "EXCUTE_DATE";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 1;
            // 
            // repositoryItemTextEdit5
            // 
            this.repositoryItemTextEdit5.AutoHeight = false;
            this.repositoryItemTextEdit5.Name = "repositoryItemTextEdit5";
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "用药途径";
            this.gridColumn10.FieldName = "DRUG_MODESTR";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 2;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "剂量";
            this.gridColumn11.FieldName = "DOSAGE";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 3;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "单位";
            this.gridColumn12.FieldName = "UNITSTR";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 4;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "备注";
            this.gridColumn13.FieldName = "REMARK";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 5;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.DisplayValueChecked = "true";
            this.repositoryItemCheckEdit2.DisplayValueUnchecked = "false";
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // repositoryItemTextEdit4
            // 
            this.repositoryItemTextEdit4.AutoHeight = false;
            this.repositoryItemTextEdit4.Name = "repositoryItemTextEdit4";
            // 
            // repositoryItemTextEdit3
            // 
            this.repositoryItemTextEdit3.AutoHeight = false;
            this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gcExecInfo;
            this.gridView1.Name = "gridView1";
            // 
            // lab1
            // 
            this.lab1.Location = new System.Drawing.Point(5, 33);
            this.lab1.Name = "lab1";
            this.lab1.Size = new System.Drawing.Size(96, 14);
            this.lab1.TabIndex = 22;
            this.lab1.Text = "促红素实际用药：";
            // 
            // deBeginExecDate
            // 
            this.deBeginExecDate.EditValue = null;
            this.deBeginExecDate.Location = new System.Drawing.Point(97, 30);
            this.deBeginExecDate.Name = "deBeginExecDate";
            this.deBeginExecDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deBeginExecDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deBeginExecDate.Size = new System.Drawing.Size(100, 21);
            this.deBeginExecDate.TabIndex = 23;
            // 
            // lab2
            // 
            this.lab2.Location = new System.Drawing.Point(203, 33);
            this.lab2.Name = "lab2";
            this.lab2.Size = new System.Drawing.Size(12, 14);
            this.lab2.TabIndex = 24;
            this.lab2.Text = "至";
            // 
            // deEndExecDate
            // 
            this.deEndExecDate.EditValue = null;
            this.deEndExecDate.Location = new System.Drawing.Point(221, 30);
            this.deEndExecDate.Name = "deEndExecDate";
            this.deEndExecDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEndExecDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deEndExecDate.Size = new System.Drawing.Size(100, 21);
            this.deEndExecDate.TabIndex = 25;
            // 
            // btnSearchExecInfo
            // 
            this.btnSearchExecInfo.ImageIndex = 8;
            this.btnSearchExecInfo.Location = new System.Drawing.Point(327, 27);
            this.btnSearchExecInfo.Name = "btnSearchExecInfo";
            this.btnSearchExecInfo.Size = new System.Drawing.Size(87, 27);
            this.btnSearchExecInfo.TabIndex = 26;
            this.btnSearchExecInfo.Text = "查询";
            this.btnSearchExecInfo.Click += new System.EventHandler(this.btnSearchExecInfo_Click);
            // 
            // gcExec
            // 
            this.gcExec.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcExec.Controls.Add(this.btnClose);
            this.gcExec.Controls.Add(this.btnSearchExecInfo);
            this.gcExec.Controls.Add(this.deEndExecDate);
            this.gcExec.Controls.Add(this.lab2);
            this.gcExec.Controls.Add(this.deBeginExecDate);
            this.gcExec.Controls.Add(this.lab1);
            this.gcExec.Controls.Add(this.gcExecInfo);
            this.gcExec.Controls.Add(this.btnExec);
            this.gcExec.Location = new System.Drawing.Point(12, 254);
            this.gcExec.Name = "gcExec";
            this.gcExec.Size = new System.Drawing.Size(806, 277);
            this.gcExec.TabIndex = 22;
            this.gcExec.Text = "执行记录";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImageIndex = 3;
            this.btnClose.Location = new System.Drawing.Point(714, 245);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 27);
            this.btnClose.TabIndex = 308;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClose1
            // 
            this.btnClose1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose1.ImageIndex = 3;
            this.btnClose1.Location = new System.Drawing.Point(731, 198);
            this.btnClose1.Name = "btnClose1";
            this.btnClose1.Size = new System.Drawing.Size(87, 27);
            this.btnClose1.TabIndex = 309;
            this.btnClose1.Text = "关闭";
            this.btnClose1.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ErythropoietinFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 543);
            this.Controls.Add(this.btnClose1);
            this.Controls.Add(this.gcExec);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.gcMainInfo);
            this.MinimumSize = new System.Drawing.Size(844, 270);
            this.Name = "ErythropoietinFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "促红素记录";
            this.Load += new System.EventHandler(this.ErythropoietinFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcMainInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMainInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcExecInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvExecInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBeginExecDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBeginExecDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndExecDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndExecDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcExec)).EndInit();
            this.gcExec.ResumeLayout(false);
            this.gcExec.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcMainInfo;
        public DevExpress.XtraGrid.Views.Grid.GridView gvMainInfo;
        private Hemo.Client.Controls.DXSimpleButton btnAdd;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private Hemo.Client.Controls.DXSimpleButton btnEdit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private Hemo.Client.Controls.DXSimpleButton btnExec;
        private DevExpress.XtraGrid.GridControl gcExecInfo;
        public DevExpress.XtraGrid.Views.Grid.GridView gvExecInfo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit4;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl lab1;
        private DevExpress.XtraEditors.DateEdit deBeginExecDate;
        private DevExpress.XtraEditors.LabelControl lab2;
        private DevExpress.XtraEditors.DateEdit deEndExecDate;
        private Hemo.Client.Controls.DXSimpleButton btnSearchExecInfo;
        private DevExpress.XtraEditors.GroupControl gcExec;
        private Hemo.Client.Controls.DXSimpleButton btnClose;
        private Hemo.Client.Controls.DXSimpleButton btnClose1;
    }
}