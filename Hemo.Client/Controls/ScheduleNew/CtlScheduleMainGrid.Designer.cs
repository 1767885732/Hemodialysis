namespace Hemo.Client.Controls.Schedule
{
    partial class CtlScheduleMainGrid
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
            this.gridControlForEmerger = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridBanchi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridoffice1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridOffice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridBedNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridMonday = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridTuesday = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridWednesday = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridThursday = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridFriday = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridSaturday = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridSunday = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemRichTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.ToolStripMenuItem_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.busyIndicator1 = new Hemo.Client.Controls.BusyIndicator();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlForEmerger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichTextEdit1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.gridControlForEmerger.Size = new System.Drawing.Size(1245, 522);
            this.gridControlForEmerger.TabIndex = 0;
            this.gridControlForEmerger.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControlForEmerger.DragDrop += new System.Windows.Forms.DragEventHandler(this.gridControlForEmerger_DragDrop);
            this.gridControlForEmerger.DragEnter += new System.Windows.Forms.DragEventHandler(this.gridControlForEmerger_DragEnter);
            this.gridControlForEmerger.DragOver += new System.Windows.Forms.DragEventHandler(this.gridControlForEmerger_DragOver);
            this.gridControlForEmerger.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridControlForEmerger_MouseDown);
            this.gridControlForEmerger.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridControlForEmerger_MouseMove);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridView1.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridView1.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(247)))));
            this.gridView1.Appearance.FooterPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(247)))));
            this.gridView1.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gridView1.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(247)))));
            this.gridView1.Appearance.GroupFooter.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(247)))));
            this.gridView1.Appearance.GroupFooter.Options.UseBackColor = true;
            this.gridView1.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(247)))));
            this.gridView1.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(247)))));
            this.gridView1.Appearance.GroupPanel.Options.UseBackColor = true;
            this.gridView1.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(247)))));
            this.gridView1.Appearance.GroupRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(247)))));
            this.gridView1.Appearance.GroupRow.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.gridView1.Appearance.GroupRow.Options.UseBackColor = true;
            this.gridView1.Appearance.GroupRow.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(247)))));
            this.gridView1.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(247)))));
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Appearance.Row.Options.UseTextOptions = true;
            this.gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView1.Appearance.RowSeparator.ForeColor = System.Drawing.Color.Red;
            this.gridView1.Appearance.RowSeparator.Options.UseForeColor = true;
            this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridView1.ColumnPanelRowHeight = 40;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridBanchi,
            this.gridColumn1,
            this.gridoffice1,
            this.gridOffice,
            this.gridBedNo,
            this.gridMonday,
            this.gridTuesday,
            this.gridWednesday,
            this.gridThursday,
            this.gridFriday,
            this.gridSaturday,
            this.gridSunday});
            this.gridView1.GridControl = this.gridControlForEmerger;
            this.gridView1.GroupCount = 2;
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
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridBanchi, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridoffice1, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView1_RowClick);
            this.gridView1.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(this.gridView1_CellMerge);
            this.gridView1.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.gridView1_DragObjectDrop);
            this.gridView1.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView1_CustomDrawCell);
            this.gridView1.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.gridView1_CustomDrawGroupRow);
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            // 
            // gridBanchi
            // 
            this.gridBanchi.Caption = "班次";
            this.gridBanchi.FieldName = "TIMETABLEVALUE";
            this.gridBanchi.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.DisplayText;
            this.gridBanchi.Name = "gridBanchi";
            this.gridBanchi.OptionsColumn.AllowEdit = false;
            this.gridBanchi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridBanchi.Width = 100;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 80;
            // 
            // gridoffice1
            // 
            this.gridoffice1.Caption = "科室ID";
            this.gridoffice1.FieldName = "QYVALUE";
            this.gridoffice1.Name = "gridoffice1";
            // 
            // gridOffice
            // 
            this.gridOffice.Caption = "病室";
            this.gridOffice.FieldName = "QYNAME";
            this.gridOffice.Name = "gridOffice";
            this.gridOffice.OptionsColumn.AllowEdit = false;
            this.gridOffice.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.gridOffice.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridOffice.Visible = true;
            this.gridOffice.VisibleIndex = 1;
            this.gridOffice.Width = 120;
            // 
            // gridBedNo
            // 
            this.gridBedNo.Caption = "床位";
            this.gridBedNo.FieldName = "CWNAME";
            this.gridBedNo.Name = "gridBedNo";
            this.gridBedNo.OptionsColumn.AllowEdit = false;
            this.gridBedNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridBedNo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridBedNo.Visible = true;
            this.gridBedNo.VisibleIndex = 2;
            this.gridBedNo.Width = 60;
            // 
            // gridMonday
            // 
            this.gridMonday.Caption = "星期一<2014-11-17>";
            this.gridMonday.FieldName = "MONDAY";
            this.gridMonday.Name = "gridMonday";
            this.gridMonday.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridMonday.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridMonday.Tag = "星期一";
            this.gridMonday.Visible = true;
            this.gridMonday.VisibleIndex = 3;
            this.gridMonday.Width = 140;
            // 
            // gridTuesday
            // 
            this.gridTuesday.Caption = "[星期二]<2014-11-18>";
            this.gridTuesday.FieldName = "TUESDAY";
            this.gridTuesday.Name = "gridTuesday";
            this.gridTuesday.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridTuesday.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridTuesday.Tag = "星期二";
            this.gridTuesday.Visible = true;
            this.gridTuesday.VisibleIndex = 4;
            this.gridTuesday.Width = 140;
            // 
            // gridWednesday
            // 
            this.gridWednesday.Caption = "[星期三]<2014-11-19>";
            this.gridWednesday.FieldName = "WEDNESDAY";
            this.gridWednesday.Name = "gridWednesday";
            this.gridWednesday.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridWednesday.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridWednesday.Tag = "星期三";
            this.gridWednesday.Visible = true;
            this.gridWednesday.VisibleIndex = 5;
            this.gridWednesday.Width = 140;
            // 
            // gridThursday
            // 
            this.gridThursday.Caption = "[星期四]<2014-11-20>";
            this.gridThursday.FieldName = "THURSDAY";
            this.gridThursday.Name = "gridThursday";
            this.gridThursday.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridThursday.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridThursday.Tag = "星期四";
            this.gridThursday.Visible = true;
            this.gridThursday.VisibleIndex = 6;
            this.gridThursday.Width = 140;
            // 
            // gridFriday
            // 
            this.gridFriday.Caption = "[星期五]<2014-11-21>";
            this.gridFriday.FieldName = "FRIDAY";
            this.gridFriday.Name = "gridFriday";
            this.gridFriday.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridFriday.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridFriday.Tag = "星期五";
            this.gridFriday.Visible = true;
            this.gridFriday.VisibleIndex = 7;
            this.gridFriday.Width = 140;
            // 
            // gridSaturday
            // 
            this.gridSaturday.Caption = "[星期六]<2014-11-22>";
            this.gridSaturday.FieldName = "STATURDAY";
            this.gridSaturday.Name = "gridSaturday";
            this.gridSaturday.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridSaturday.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridSaturday.Tag = "星期六";
            this.gridSaturday.Visible = true;
            this.gridSaturday.VisibleIndex = 8;
            this.gridSaturday.Width = 140;
            // 
            // gridSunday
            // 
            this.gridSunday.Caption = "[星期日]<2014-11-23>";
            this.gridSunday.FieldName = "SUNDAY";
            this.gridSunday.Name = "gridSunday";
            this.gridSunday.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridSunday.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridSunday.Tag = "星期日";
            this.gridSunday.Visible = true;
            this.gridSunday.VisibleIndex = 9;
            this.gridSunday.Width = 144;
            // 
            // repositoryItemRichTextEdit1
            // 
            this.repositoryItemRichTextEdit1.DocumentFormat = DevExpress.XtraRichEdit.DocumentFormat.Html;
            this.repositoryItemRichTextEdit1.Name = "repositoryItemRichTextEdit1";
            this.repositoryItemRichTextEdit1.OptionsExport.PlainText.ExportFinalParagraphMark = DevExpress.XtraRichEdit.Export.PlainText.ExportFinalParagraphMark.Never;
            this.repositoryItemRichTextEdit1.ShowCaretInReadOnly = false;
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
            // busyIndicator1
            // 
            this.busyIndicator1.Location = new System.Drawing.Point(579, 239);
            this.busyIndicator1.Name = "busyIndicator1";
            this.busyIndicator1.Size = new System.Drawing.Size(100, 40);
            this.busyIndicator1.TabIndex = 1;
            this.busyIndicator1.Visible = false;
            // 
            // CtlScheduleMainGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.busyIndicator1);
            this.Controls.Add(this.gridControlForEmerger);
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            this.Name = "CtlScheduleMainGrid";
            this.Size = new System.Drawing.Size(1245, 522);
            this.Load += new System.EventHandler(this.CtlScheduleMainGrid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlForEmerger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichTextEdit1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Doc.动静脉内瘘血管吻合术同意书 动静脉内瘘血管吻合术同意书1;
        private DevExpress.XtraGrid.GridControl gridControlForEmerger;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridOffice;
        private DevExpress.XtraGrid.Columns.GridColumn gridBedNo;
        private DevExpress.XtraGrid.Columns.GridColumn gridMonday;
        private DevExpress.XtraGrid.Columns.GridColumn gridTuesday;
        private DevExpress.XtraGrid.Columns.GridColumn gridWednesday;
        private DevExpress.XtraGrid.Columns.GridColumn gridThursday;
        private DevExpress.XtraGrid.Columns.GridColumn gridFriday;
        private DevExpress.XtraGrid.Columns.GridColumn gridSaturday;
        private DevExpress.XtraGrid.Columns.GridColumn gridSunday;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Delete;
        private DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit repositoryItemRichTextEdit1;
        private BusyIndicator busyIndicator1;
        private DevExpress.XtraGrid.Columns.GridColumn gridoffice1;
        public DevExpress.XtraGrid.Columns.GridColumn gridBanchi;
    }
}
