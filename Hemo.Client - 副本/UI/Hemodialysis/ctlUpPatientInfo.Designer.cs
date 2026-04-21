namespace Hemo.Client.UI.Hemodialysis
{
    partial class ctlUpPatientInfo
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.xtraTabControl2 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage7 = new DevExpress.XtraTab.XtraTabPage();
            this.gridPatients = new DevExpress.XtraGrid.GridControl();
            this.gridVPatients = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn36 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn38 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn39 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn41 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn42 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn43 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn44 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn45 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn46 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn47 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn48 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn49 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn50 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl12 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtHemoDialysis = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.btnPatientsInfoLoad = new Hemo.Client.Controls.DXSimpleButton();
            this.btnPatientsInfoQuery = new Hemo.Client.Controls.DXSimpleButton();
            this.rdgStatus = new DevExpress.XtraEditors.RadioGroup();
            this.ckdCheckOrCancel = new DevExpress.XtraEditors.CheckEdit();
            this.xtraTabPage8 = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl2)).BeginInit();
            this.xtraTabControl2.SuspendLayout();
            this.xtraTabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPatients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVPatients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl12)).BeginInit();
            this.panelControl12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHemoDialysis.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdgStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckdCheckOrCancel.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl2
            // 
            this.xtraTabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl2.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl2.Name = "xtraTabControl2";
            this.xtraTabControl2.SelectedTabPage = this.xtraTabPage7;
            this.xtraTabControl2.Size = new System.Drawing.Size(781, 459);
            this.xtraTabControl2.TabIndex = 1;
            this.xtraTabControl2.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage7,
            this.xtraTabPage8});
            this.xtraTabControl2.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl2_SelectedPageChanged);
            // 
            // xtraTabPage7
            // 
            this.xtraTabPage7.Controls.Add(this.gridPatients);
            this.xtraTabPage7.Controls.Add(this.panelControl12);
            this.xtraTabPage7.Name = "xtraTabPage7";
            this.xtraTabPage7.Size = new System.Drawing.Size(775, 430);
            this.xtraTabPage7.Text = "患者信息";
            // 
            // gridPatients
            // 
            this.gridPatients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPatients.Location = new System.Drawing.Point(0, 35);
            this.gridPatients.MainView = this.gridVPatients;
            this.gridPatients.Name = "gridPatients";
            this.gridPatients.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit2});
            this.gridPatients.Size = new System.Drawing.Size(775, 395);
            this.gridPatients.TabIndex = 5;
            this.gridPatients.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVPatients});
            // 
            // gridVPatients
            // 
            this.gridVPatients.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridVPatients.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridVPatients.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridVPatients.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn35,
            this.gridColumn36,
            this.gridColumn37,
            this.gridColumn38,
            this.gridColumn39,
            this.gridColumn40,
            this.gridColumn41,
            this.gridColumn42,
            this.gridColumn43,
            this.gridColumn44,
            this.gridColumn45,
            this.gridColumn46,
            this.gridColumn47,
            this.gridColumn48,
            this.gridColumn49,
            this.gridColumn50});
            this.gridVPatients.GridControl = this.gridPatients;
            this.gridVPatients.Name = "gridVPatients";
            this.gridVPatients.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridVPatients.OptionsView.ColumnAutoWidth = false;
            this.gridVPatients.OptionsView.ShowGroupPanel = false;
            this.gridVPatients.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridVPatients_RowClick);
       
            // 
            // gridColumn35
            // 
            this.gridColumn35.Caption = "选择";
            this.gridColumn35.ColumnEdit = this.repositoryItemCheckEdit2;
            this.gridColumn35.FieldName = "IS_UP";
            this.gridColumn35.Name = "gridColumn35";
            this.gridColumn35.Visible = true;
            this.gridColumn35.VisibleIndex = 0;
            this.gridColumn35.Width = 68;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repositoryItemCheckEdit2.ValueChecked = "1";
            this.repositoryItemCheckEdit2.ValueUnchecked = "0";
            // 
            // gridColumn36
            // 
            this.gridColumn36.Caption = "上传状态";
            this.gridColumn36.FieldName = "IS_UPNAME";
            this.gridColumn36.Name = "gridColumn36";
            this.gridColumn36.OptionsColumn.AllowEdit = false;
            this.gridColumn36.Visible = true;
            this.gridColumn36.VisibleIndex = 1;
            this.gridColumn36.Width = 61;
            // 
            // gridColumn37
            // 
            this.gridColumn37.Caption = "透析号";
            this.gridColumn37.FieldName = "HEMODIALYSIS_ID";
            this.gridColumn37.Name = "gridColumn37";
            this.gridColumn37.OptionsColumn.AllowEdit = false;
            this.gridColumn37.Visible = true;
            this.gridColumn37.VisibleIndex = 2;
            this.gridColumn37.Width = 81;
            // 
            // gridColumn38
            // 
            this.gridColumn38.Caption = "病人ID";
            this.gridColumn38.FieldName = "PATIENT_ID";
            this.gridColumn38.Name = "gridColumn38";
            this.gridColumn38.OptionsColumn.AllowEdit = false;
            this.gridColumn38.Visible = true;
            this.gridColumn38.VisibleIndex = 3;
            this.gridColumn38.Width = 72;
            // 
            // gridColumn39
            // 
            this.gridColumn39.Caption = "姓名";
            this.gridColumn39.FieldName = "NAME";
            this.gridColumn39.Name = "gridColumn39";
            this.gridColumn39.OptionsColumn.AllowEdit = false;
            this.gridColumn39.Visible = true;
            this.gridColumn39.VisibleIndex = 4;
            this.gridColumn39.Width = 72;
            // 
            // gridColumn40
            // 
            this.gridColumn40.Caption = "性别";
            this.gridColumn40.FieldName = "SEX";
            this.gridColumn40.Name = "gridColumn40";
            this.gridColumn40.OptionsColumn.AllowEdit = false;
            this.gridColumn40.Visible = true;
            this.gridColumn40.VisibleIndex = 5;
            this.gridColumn40.Width = 72;
            // 
            // gridColumn41
            // 
            this.gridColumn41.Caption = "生日";
            this.gridColumn41.FieldName = "BIRTHDAY";
            this.gridColumn41.Name = "gridColumn41";
            this.gridColumn41.OptionsColumn.AllowEdit = false;
            this.gridColumn41.Visible = true;
            this.gridColumn41.VisibleIndex = 6;
            this.gridColumn41.Width = 72;
            // 
            // gridColumn42
            // 
            this.gridColumn42.Caption = "年龄";
            this.gridColumn42.FieldName = "AGE";
            this.gridColumn42.Name = "gridColumn42";
            this.gridColumn42.OptionsColumn.AllowEdit = false;
            this.gridColumn42.Visible = true;
            this.gridColumn42.VisibleIndex = 7;
            this.gridColumn42.Width = 72;
            // 
            // gridColumn43
            // 
            this.gridColumn43.Caption = "籍贯";
            this.gridColumn43.FieldName = "NATIVEPLACE";
            this.gridColumn43.Name = "gridColumn43";
            this.gridColumn43.OptionsColumn.AllowEdit = false;
            this.gridColumn43.Visible = true;
            this.gridColumn43.VisibleIndex = 8;
            this.gridColumn43.Width = 72;
            // 
            // gridColumn44
            // 
            this.gridColumn44.Caption = "工作";
            this.gridColumn44.FieldName = "JOB";
            this.gridColumn44.Name = "gridColumn44";
            this.gridColumn44.OptionsColumn.AllowEdit = false;
            this.gridColumn44.Visible = true;
            this.gridColumn44.VisibleIndex = 9;
            this.gridColumn44.Width = 72;
            // 
            // gridColumn45
            // 
            this.gridColumn45.Caption = "婚姻";
            this.gridColumn45.FieldName = "MARITAL";
            this.gridColumn45.Name = "gridColumn45";
            this.gridColumn45.OptionsColumn.AllowEdit = false;
            this.gridColumn45.Visible = true;
            this.gridColumn45.VisibleIndex = 10;
            this.gridColumn45.Width = 72;
            // 
            // gridColumn46
            // 
            this.gridColumn46.Caption = "证件类型";
            this.gridColumn46.FieldName = "CREDENTIALS_TYPE";
            this.gridColumn46.Name = "gridColumn46";
            this.gridColumn46.OptionsColumn.AllowEdit = false;
            this.gridColumn46.Visible = true;
            this.gridColumn46.VisibleIndex = 11;
            this.gridColumn46.Width = 96;
            // 
            // gridColumn47
            // 
            this.gridColumn47.Caption = "证件号码";
            this.gridColumn47.FieldName = "CREDENTIALS_NUMBER";
            this.gridColumn47.Name = "gridColumn47";
            this.gridColumn47.OptionsColumn.AllowEdit = false;
            this.gridColumn47.Visible = true;
            this.gridColumn47.VisibleIndex = 12;
            this.gridColumn47.Width = 72;
            // 
            // gridColumn48
            // 
            this.gridColumn48.Caption = "学历";
            this.gridColumn48.FieldName = "EDUCATION";
            this.gridColumn48.Name = "gridColumn48";
            this.gridColumn48.OptionsColumn.AllowEdit = false;
            this.gridColumn48.Visible = true;
            this.gridColumn48.VisibleIndex = 13;
            this.gridColumn48.Width = 72;
            // 
            // gridColumn49
            // 
            this.gridColumn49.Caption = "民族";
            this.gridColumn49.FieldName = "NATION";
            this.gridColumn49.Name = "gridColumn49";
            this.gridColumn49.OptionsColumn.AllowEdit = false;
            this.gridColumn49.Visible = true;
            this.gridColumn49.VisibleIndex = 14;
            this.gridColumn49.Width = 72;
            // 
            // gridColumn50
            // 
            this.gridColumn50.Caption = "拼音";
            this.gridColumn50.FieldName = "INPUT_CODE";
            this.gridColumn50.Name = "gridColumn50";
            this.gridColumn50.OptionsColumn.AllowEdit = false;
            this.gridColumn50.Visible = true;
            this.gridColumn50.VisibleIndex = 15;
            this.gridColumn50.Width = 78;
            // 
            // panelControl12
            // 
            this.panelControl12.Controls.Add(this.labelControl4);
            this.panelControl12.Controls.Add(this.labelControl3);
            this.panelControl12.Controls.Add(this.txtHemoDialysis);
            this.panelControl12.Controls.Add(this.txtName);
            this.panelControl12.Controls.Add(this.btnPatientsInfoLoad);
            this.panelControl12.Controls.Add(this.btnPatientsInfoQuery);
            this.panelControl12.Controls.Add(this.rdgStatus);
            this.panelControl12.Controls.Add(this.ckdCheckOrCancel);
            this.panelControl12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl12.Location = new System.Drawing.Point(0, 0);
            this.panelControl12.Name = "panelControl12";
            this.panelControl12.Size = new System.Drawing.Size(775, 35);
            this.panelControl12.TabIndex = 0;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(416, 11);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 50;
            this.labelControl4.Text = "透析编号";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(256, 11);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 49;
            this.labelControl3.Text = "患者姓名";
            // 
            // txtHemoDialysis
            // 
            this.txtHemoDialysis.Location = new System.Drawing.Point(470, 8);
            this.txtHemoDialysis.Name = "txtHemoDialysis";
            this.txtHemoDialysis.Properties.NullValuePrompt = "透析编号";
            this.txtHemoDialysis.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtHemoDialysis.Size = new System.Drawing.Size(100, 20);
            this.txtHemoDialysis.TabIndex = 48;
            // 
            // txtName
            // 
            this.txtName.EditValue = "";
            this.txtName.Location = new System.Drawing.Point(310, 8);
            this.txtName.Name = "txtName";
            this.txtName.Properties.NullValuePrompt = "姓名/首拼";
            this.txtName.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtName.Size = new System.Drawing.Size(100, 20);
            this.txtName.TabIndex = 47;
            // 
            // btnPatientsInfoLoad
            // 
            this.btnPatientsInfoLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPatientsInfoLoad.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.btnPatientsInfoLoad.Appearance.Options.UseFont = true;
            this.btnPatientsInfoLoad.ImageIndex = 19;
            this.btnPatientsInfoLoad.Location = new System.Drawing.Point(693, 4);
            this.btnPatientsInfoLoad.Name = "btnPatientsInfoLoad";
            this.btnPatientsInfoLoad.Size = new System.Drawing.Size(73, 26);
            this.btnPatientsInfoLoad.TabIndex = 46;
            this.btnPatientsInfoLoad.Text = "上传";
            // 
            // btnPatientsInfoQuery
            // 
            this.btnPatientsInfoQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPatientsInfoQuery.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.btnPatientsInfoQuery.Appearance.Options.UseFont = true;
            this.btnPatientsInfoQuery.ImageIndex = 8;
            this.btnPatientsInfoQuery.Location = new System.Drawing.Point(616, 4);
            this.btnPatientsInfoQuery.Name = "btnPatientsInfoQuery";
            this.btnPatientsInfoQuery.Size = new System.Drawing.Size(70, 26);
            this.btnPatientsInfoQuery.TabIndex = 45;
            this.btnPatientsInfoQuery.Text = "查询";
            this.btnPatientsInfoQuery.Click += new System.EventHandler(this.btnPatientsInfoQuery_Click);
            // 
            // rdgStatus
            // 
            this.rdgStatus.Location = new System.Drawing.Point(6, 5);
            this.rdgStatus.Name = "rdgStatus";
            this.rdgStatus.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("全部", "全部"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("已上传", "已上传"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("未上传", "未上传")});
            this.rdgStatus.Size = new System.Drawing.Size(192, 26);
            this.rdgStatus.TabIndex = 1;
            this.rdgStatus.SelectedIndexChanged += new System.EventHandler(this.rdgStatus_SelectedIndexChanged);
            // 
            // ckdCheckOrCancel
            // 
            this.ckdCheckOrCancel.Location = new System.Drawing.Point(204, 8);
            this.ckdCheckOrCancel.Name = "ckdCheckOrCancel";
            this.ckdCheckOrCancel.Properties.Caption = "全选";
            this.ckdCheckOrCancel.Size = new System.Drawing.Size(75, 19);
            this.ckdCheckOrCancel.TabIndex = 0;
            this.ckdCheckOrCancel.CheckedChanged += new System.EventHandler(this.ckdCheckOrCancel_CheckedChanged);
            // 
            // xtraTabPage8
            // 
            this.xtraTabPage8.Name = "xtraTabPage8";
            this.xtraTabPage8.Size = new System.Drawing.Size(775, 430);
            this.xtraTabPage8.Text = "患者病历";
            // 
            // ctlUpPatientInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl2);
            this.Name = "ctlUpPatientInfo";
            this.Size = new System.Drawing.Size(781, 459);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl2)).EndInit();
            this.xtraTabControl2.ResumeLayout(false);
            this.xtraTabPage7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridPatients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVPatients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl12)).EndInit();
            this.panelControl12.ResumeLayout(false);
            this.panelControl12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHemoDialysis.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdgStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckdCheckOrCancel.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage7;
        private DevExpress.XtraGrid.GridControl gridPatients;
        public DevExpress.XtraGrid.Views.Grid.GridView gridVPatients;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn35;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn36;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn37;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn38;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn39;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn40;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn41;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn42;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn43;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn44;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn45;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn46;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn47;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn48;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn49;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn50;
        private DevExpress.XtraEditors.PanelControl panelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtHemoDialysis;
        private DevExpress.XtraEditors.TextEdit txtName;
        private Controls.DXSimpleButton btnPatientsInfoLoad;
        private Controls.DXSimpleButton btnPatientsInfoQuery;
        private DevExpress.XtraEditors.RadioGroup rdgStatus;
        private DevExpress.XtraEditors.CheckEdit ckdCheckOrCancel;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage8;
    }
}
