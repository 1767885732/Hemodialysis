namespace Hemo.Client.UI.PatientSchedule
{
    partial class PatientDutyFrm
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
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.TabPageNurse = new DevExpress.XtraTab.XtraTabPage();
            this.busyIndicator1 = new Hemo.Client.Controls.BusyIndicator();
            this.gridControlForEmerger = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridUSER = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridMonday = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridTuesday = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridWednesday = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridThursday = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridFriday = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridSaturday = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridSunday = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemRichTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit();
            this.TabPageDoctor = new DevExpress.XtraTab.XtraTabPage();
            this.dutyDoctor1 = new Hemo.Client.UI.PatientSchedule.DutyDoctor();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_last = new Hemo.Client.Controls.DXSimpleButton();
            this.btn_Next = new Hemo.Client.Controls.DXSimpleButton();
            this.btnClose = new Hemo.Client.Controls.DXSimpleButton();
            this.btn_Print = new Hemo.Client.Controls.DXSimpleButton();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_Delete = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.TabPageNurse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlForEmerger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichTextEdit1)).BeginInit();
            this.TabPageDoctor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.TabPageNurse;
            this.xtraTabControl1.Size = new System.Drawing.Size(913, 408);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.TabPageNurse,
            this.TabPageDoctor});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            this.xtraTabControl1.TabIndexChanged += new System.EventHandler(this.xtraTabControl1_TabIndexChanged);
            // 
            // TabPageNurse
            // 
            this.TabPageNurse.Controls.Add(this.busyIndicator1);
            this.TabPageNurse.Controls.Add(this.gridControlForEmerger);
            this.TabPageNurse.Name = "TabPageNurse";
            this.TabPageNurse.Size = new System.Drawing.Size(906, 378);
            this.TabPageNurse.Text = "护士值班";
            // 
            // busyIndicator1
            // 
            this.busyIndicator1.Location = new System.Drawing.Point(419, 187);
            this.busyIndicator1.Name = "busyIndicator1";
            this.busyIndicator1.Size = new System.Drawing.Size(100, 40);
            this.busyIndicator1.TabIndex = 2;
            // 
            // gridControlForEmerger
            // 
            this.gridControlForEmerger.AllowDrop = true;
            this.gridControlForEmerger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlForEmerger.Location = new System.Drawing.Point(0, 0);
            this.gridControlForEmerger.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            this.gridControlForEmerger.MainView = this.gridView1;
            this.gridControlForEmerger.Name = "gridControlForEmerger";
            this.gridControlForEmerger.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemRichTextEdit1});
            this.gridControlForEmerger.Size = new System.Drawing.Size(906, 378);
            this.gridControlForEmerger.TabIndex = 1;
            this.gridControlForEmerger.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridView1.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView1.Appearance.Row.Options.UseTextOptions = true;
            this.gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView1.Appearance.RowSeparator.ForeColor = System.Drawing.Color.Red;
            this.gridView1.Appearance.RowSeparator.Options.UseForeColor = true;
            this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.gridView1.ColumnPanelRowHeight = 40;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridUSER,
            this.gridMonday,
            this.gridTuesday,
            this.gridWednesday,
            this.gridThursday,
            this.gridFriday,
            this.gridSaturday,
            this.gridSunday});
            this.gridView1.GridControl = this.gridControlForEmerger;
            this.gridView1.GroupFormat = "";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AutoPopulateColumns = false;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsDetail.ShowDetailTabs = false;
            this.gridView1.OptionsMenu.EnableColumnMenu = false;
            this.gridView1.OptionsView.AllowCellMerge = true;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView1_RowClick);
            // 
            // gridUSER
            // 
            this.gridUSER.Caption = "值班人员";
            this.gridUSER.FieldName = "USER_NAME";
            this.gridUSER.Name = "gridUSER";
            this.gridUSER.OptionsColumn.AllowEdit = false;
            this.gridUSER.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridUSER.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridUSER.Visible = true;
            this.gridUSER.VisibleIndex = 0;
            this.gridUSER.Width = 90;
            // 
            // gridMonday
            // 
            this.gridMonday.AppearanceHeader.Options.UseTextOptions = true;
            this.gridMonday.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridMonday.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridMonday.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridMonday.Caption = "星期一<2014-11-17>";
            this.gridMonday.FieldName = "MONDAYOFFICENAME";
            this.gridMonday.Name = "gridMonday";
            this.gridMonday.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridMonday.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridMonday.Tag = "星期一";
            this.gridMonday.Visible = true;
            this.gridMonday.VisibleIndex = 1;
            this.gridMonday.Width = 117;
            // 
            // gridTuesday
            // 
            this.gridTuesday.AppearanceHeader.Options.UseTextOptions = true;
            this.gridTuesday.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridTuesday.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridTuesday.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridTuesday.Caption = "[星期二]<2014-11-18>";
            this.gridTuesday.FieldName = "TUESDAYOFFICENAME";
            this.gridTuesday.Name = "gridTuesday";
            this.gridTuesday.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridTuesday.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridTuesday.Tag = "星期二";
            this.gridTuesday.Visible = true;
            this.gridTuesday.VisibleIndex = 2;
            this.gridTuesday.Width = 117;
            // 
            // gridWednesday
            // 
            this.gridWednesday.AppearanceHeader.Options.UseTextOptions = true;
            this.gridWednesday.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridWednesday.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridWednesday.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridWednesday.Caption = "[星期三]<2014-11-19>";
            this.gridWednesday.FieldName = "WEDNESDAYOFFICENAME";
            this.gridWednesday.Name = "gridWednesday";
            this.gridWednesday.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridWednesday.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridWednesday.Tag = "星期三";
            this.gridWednesday.Visible = true;
            this.gridWednesday.VisibleIndex = 3;
            this.gridWednesday.Width = 117;
            // 
            // gridThursday
            // 
            this.gridThursday.AppearanceHeader.Options.UseTextOptions = true;
            this.gridThursday.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridThursday.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridThursday.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridThursday.Caption = "[星期四]<2014-11-20>";
            this.gridThursday.FieldName = "THURSDAYOFFICENAME";
            this.gridThursday.Name = "gridThursday";
            this.gridThursday.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridThursday.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridThursday.Tag = "星期四";
            this.gridThursday.Visible = true;
            this.gridThursday.VisibleIndex = 4;
            this.gridThursday.Width = 117;
            // 
            // gridFriday
            // 
            this.gridFriday.AppearanceHeader.Options.UseTextOptions = true;
            this.gridFriday.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridFriday.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridFriday.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridFriday.Caption = "[星期五]<2014-11-21>";
            this.gridFriday.FieldName = "FRIDAYOFFICENAME";
            this.gridFriday.Name = "gridFriday";
            this.gridFriday.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridFriday.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridFriday.Tag = "星期五";
            this.gridFriday.Visible = true;
            this.gridFriday.VisibleIndex = 5;
            this.gridFriday.Width = 117;
            // 
            // gridSaturday
            // 
            this.gridSaturday.AppearanceHeader.Options.UseTextOptions = true;
            this.gridSaturday.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridSaturday.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridSaturday.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridSaturday.Caption = "[星期六]<2014-11-22>";
            this.gridSaturday.FieldName = "SATURDAYOFFICENAME";
            this.gridSaturday.Name = "gridSaturday";
            this.gridSaturday.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridSaturday.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridSaturday.Tag = "星期六";
            this.gridSaturday.Visible = true;
            this.gridSaturday.VisibleIndex = 6;
            this.gridSaturday.Width = 117;
            // 
            // gridSunday
            // 
            this.gridSunday.AppearanceHeader.Options.UseTextOptions = true;
            this.gridSunday.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridSunday.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridSunday.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridSunday.Caption = "[星期日]<2014-11-23>";
            this.gridSunday.FieldName = "SUNDAYOFFICENAME";
            this.gridSunday.Name = "gridSunday";
            this.gridSunday.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridSunday.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridSunday.Tag = "星期日";
            this.gridSunday.Visible = true;
            this.gridSunday.VisibleIndex = 7;
            this.gridSunday.Width = 130;
            // 
            // repositoryItemRichTextEdit1
            // 
            this.repositoryItemRichTextEdit1.DocumentFormat = DevExpress.XtraRichEdit.DocumentFormat.Html;
            this.repositoryItemRichTextEdit1.Name = "repositoryItemRichTextEdit1";
            // 
            // TabPageDoctor
            // 
            this.TabPageDoctor.Appearance.PageClient.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.TabPageDoctor.Appearance.PageClient.Options.UseBackColor = true;
            this.TabPageDoctor.Controls.Add(this.dutyDoctor1);
            this.TabPageDoctor.Name = "TabPageDoctor";
            this.TabPageDoctor.Size = new System.Drawing.Size(906, 378);
            this.TabPageDoctor.Text = "医生值班";
            // 
            // dutyDoctor1
            // 
            this.dutyDoctor1._date = new System.DateTime(((long)(0)));
            this.dutyDoctor1.Appearance.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dutyDoctor1.Appearance.Options.UseBackColor = true;
            this.dutyDoctor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dutyDoctor1.HasDirty = false;
            this.dutyDoctor1.Location = new System.Drawing.Point(0, 0);
            this.dutyDoctor1.Name = "dutyDoctor1";
            this.dutyDoctor1.Size = new System.Drawing.Size(906, 378);
            this.dutyDoctor1.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_last);
            this.panelControl1.Controls.Add(this.btn_Next);
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.btn_Print);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 408);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(913, 36);
            this.panelControl1.TabIndex = 1;
            // 
            // btn_last
            // 
            this.btn_last.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_last.ImageIndex = 16;
            this.btn_last.Location = new System.Drawing.Point(406, 8);
            this.btn_last.Name = "btn_last";
            this.btn_last.Size = new System.Drawing.Size(75, 23);
            this.btn_last.TabIndex = 356;
            this.btn_last.Text = "上周(&L)";
            this.btn_last.Click += new System.EventHandler(this.btn_last_Click);
            // 
            // btn_Next
            // 
            this.btn_Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Next.ImageIndex = 17;
            this.btn_Next.Location = new System.Drawing.Point(525, 8);
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.Size = new System.Drawing.Size(75, 23);
            this.btn_Next.TabIndex = 357;
            this.btn_Next.Text = "下周(&N)";
            this.btn_Next.Click += new System.EventHandler(this.btn_Next_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.ImageIndex = 3;
            this.btnClose.Location = new System.Drawing.Point(826, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 355;
            this.btnClose.Text = "关闭(&D)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btn_Print
            // 
            this.btn_Print.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Print.ImageIndex = 6;
            this.btn_Print.Location = new System.Drawing.Point(745, 8);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(75, 23);
            this.btn_Print.TabIndex = 354;
            this.btn_Print.Text = "打印(&P)";
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(664, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 354;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Delete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // ToolStripMenuItem_Delete
            // 
            this.ToolStripMenuItem_Delete.Name = "ToolStripMenuItem_Delete";
            this.ToolStripMenuItem_Delete.Size = new System.Drawing.Size(100, 22);
            this.ToolStripMenuItem_Delete.Text = "删除";
            this.ToolStripMenuItem_Delete.Click += new System.EventHandler(this.ToolStripMenuItem_Delete_Click);
            // 
            // PatientDutyFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 444);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "PatientDutyFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "当周值班人员";
            this.Load += new System.EventHandler(this.PatientDutyFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.TabPageNurse.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlForEmerger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichTextEdit1)).EndInit();
            this.TabPageDoctor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage TabPageNurse;
        private DevExpress.XtraTab.XtraTabPage TabPageDoctor;
        private DutyDoctor dutyDoctor1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Controls.DXSimpleButton btn_last;
        private Controls.DXSimpleButton btn_Next;
        private Controls.DXSimpleButton btnClose;
        private Controls.DXSimpleButton btnSave;
        private DevExpress.XtraGrid.GridControl gridControlForEmerger;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridUSER;
        private DevExpress.XtraGrid.Columns.GridColumn gridMonday;
        private DevExpress.XtraGrid.Columns.GridColumn gridTuesday;
        private DevExpress.XtraGrid.Columns.GridColumn gridWednesday;
        private DevExpress.XtraGrid.Columns.GridColumn gridThursday;
        private DevExpress.XtraGrid.Columns.GridColumn gridFriday;
        private DevExpress.XtraGrid.Columns.GridColumn gridSaturday;
        private DevExpress.XtraGrid.Columns.GridColumn gridSunday;
        private DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit repositoryItemRichTextEdit1;
        private Controls.BusyIndicator busyIndicator1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Delete;
        private Controls.DXSimpleButton btn_Print;

    }
}