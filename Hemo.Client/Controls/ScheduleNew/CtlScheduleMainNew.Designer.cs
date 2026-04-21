namespace Hemo.Client.Controls.Schedule
{
    partial class CtlScheduleMainNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtlScheduleMainNew));
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.pnlOperation = new DevExpress.XtraEditors.PanelControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.busyIndicator1 = new Hemo.Client.Controls.BusyIndicator();
            this.treeListPatient = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.smallimageList = new System.Windows.Forms.ImageList(this.components);
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.btnQuery = new Hemo.Client.Controls.DXSimpleButton();
            this.txtSearchPatient = new DevExpress.XtraEditors.TextEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupControl_office = new DevExpress.XtraEditors.GroupControl();
            this.groupControl_banChi = new DevExpress.XtraEditors.GroupControl();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.新增病人ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改患者ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labListRecord = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOperation)).BeginInit();
            this.pnlOperation.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListPatient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchPatient.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl_office)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl_banChi)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.ID = new System.Guid("9d28504b-822e-4142-98e8-2e6d4ac38d1c");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Options.ShowCloseButton = false;
            this.dockPanel1.OriginalSize = new System.Drawing.Size(196, 200);
            this.dockPanel1.Size = new System.Drawing.Size(196, 522);
            this.dockPanel1.Text = "常用操作";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.pnlOperation);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(190, 494);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // pnlOperation
            // 
            this.pnlOperation.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlOperation.Controls.Add(this.tableLayoutPanel1);
            this.pnlOperation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOperation.Location = new System.Drawing.Point(0, 0);
            this.pnlOperation.Name = "pnlOperation";
            this.pnlOperation.Size = new System.Drawing.Size(190, 494);
            this.pnlOperation.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelControl2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelControl3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(190, 494);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.groupControl2);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(3, 41);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(184, 450);
            this.panelControl2.TabIndex = 3;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.busyIndicator1);
            this.groupControl2.Controls.Add(this.treeListPatient);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(184, 450);
            this.groupControl2.TabIndex = 3;
            this.groupControl2.Text = "患者列表";
            // 
            // busyIndicator1
            // 
            this.busyIndicator1.Location = new System.Drawing.Point(36, 184);
            this.busyIndicator1.Name = "busyIndicator1";
            this.busyIndicator1.Size = new System.Drawing.Size(100, 40);
            this.busyIndicator1.TabIndex = 0;
            // 
            // treeListPatient
            // 
            this.treeListPatient.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.treeListPatient.Appearance.FocusedCell.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.treeListPatient.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeListPatient.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.treeListPatient.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3});
            this.treeListPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListPatient.Location = new System.Drawing.Point(2, 23);
            this.treeListPatient.Name = "treeListPatient";
            this.treeListPatient.OptionsSelection.InvertSelection = true;
            this.treeListPatient.OptionsView.ShowColumns = false;
            this.treeListPatient.OptionsView.ShowIndicator = false;
            this.treeListPatient.ParentFieldName = "HEMODIALYSIS_ID";
            this.treeListPatient.Size = new System.Drawing.Size(180, 425);
            this.treeListPatient.StateImageList = this.smallimageList;
            this.treeListPatient.TabIndex = 1;
            this.treeListPatient.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeListPatient_FocusedNodeChanged);
            this.treeListPatient.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeListPatient_MouseDown);
            this.treeListPatient.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeListPatient_MouseUp);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "姓名";
            this.treeListColumn1.FieldName = "姓名";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.AllowEdit = false;
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            this.treeListColumn1.Width = 92;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "SEX";
            this.treeListColumn2.FieldName = "SEX";
            this.treeListColumn2.Name = "treeListColumn2";
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "HEMODIALYSIS_ID";
            this.treeListColumn3.FieldName = "HEMODIALYSIS_ID";
            this.treeListColumn3.Name = "treeListColumn3";
            // 
            // smallimageList
            // 
            this.smallimageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("smallimageList.ImageStream")));
            this.smallimageList.TransparentColor = System.Drawing.Color.Transparent;
            this.smallimageList.Images.SetKeyName(0, "boy16.png");
            this.smallimageList.Images.SetKeyName(1, "gril16.png");
            this.smallimageList.Images.SetKeyName(2, "DatabaseSwitchboardManager.png");
            this.smallimageList.Images.SetKeyName(3, "OPENSTEP_EUI Write Document.ico");
            this.smallimageList.Images.SetKeyName(4, "FileSaveAsWord97_2003.png");
            this.smallimageList.Images.SetKeyName(5, "PrintAreaMenu.png");
            this.smallimageList.Images.SetKeyName(6, "MailSelectNames.png");
            this.smallimageList.Images.SetKeyName(7, "1279369174_user_business_boss.png");
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.btnQuery);
            this.panelControl3.Controls.Add(this.txtSearchPatient);
            this.panelControl3.Location = new System.Drawing.Point(3, 3);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(184, 32);
            this.panelControl3.TabIndex = 4;
            // 
            // btnQuery
            // 
            this.btnQuery.ImageIndex = 8;
            this.btnQuery.Location = new System.Drawing.Point(126, 4);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(55, 23);
            this.btnQuery.TabIndex = 6;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtSearchPatient
            // 
            this.txtSearchPatient.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtSearchPatient.Location = new System.Drawing.Point(2, 5);
            this.txtSearchPatient.Name = "txtSearchPatient";
            this.txtSearchPatient.Size = new System.Drawing.Size(118, 21);
            this.txtSearchPatient.TabIndex = 5;
            this.txtSearchPatient.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchPatient_KeyDown);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.xtraScrollableControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(196, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(837, 522);
            this.panelControl1.TabIndex = 1;
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Controls.Add(this.panel1);
            this.xtraScrollableControl1.Controls.Add(this.groupControl_office);
            this.xtraScrollableControl1.Controls.Add(this.groupControl_banChi);
            this.xtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(837, 522);
            this.xtraScrollableControl1.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(41, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(409, 347);
            this.panel1.TabIndex = 12;
            // 
            // groupControl_office
            // 
            this.groupControl_office.Appearance.BackColor = System.Drawing.Color.AliceBlue;
            this.groupControl_office.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.groupControl_office.Appearance.Options.UseBackColor = true;
            this.groupControl_office.Appearance.Options.UseBorderColor = true;
            this.groupControl_office.AppearanceCaption.Options.UseImage = true;
            this.groupControl_office.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl_office.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl_office.AppearanceCaption.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
            this.groupControl_office.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupControl_office.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.groupControl_office.Location = new System.Drawing.Point(39, 0);
            this.groupControl_office.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            this.groupControl_office.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControl_office.Name = "groupControl_office";
            this.groupControl_office.Size = new System.Drawing.Size(798, 50);
            this.groupControl_office.TabIndex = 11;
            this.groupControl_office.Text = "透析室";
            // 
            // groupControl_banChi
            // 
            this.groupControl_banChi.Appearance.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupControl_banChi.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.groupControl_banChi.Appearance.Options.UseBackColor = true;
            this.groupControl_banChi.Appearance.Options.UseBorderColor = true;
            this.groupControl_banChi.AppearanceCaption.Options.UseImage = true;
            this.groupControl_banChi.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl_banChi.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl_banChi.AppearanceCaption.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
            this.groupControl_banChi.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupControl_banChi.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.groupControl_banChi.Location = new System.Drawing.Point(0, 0);
            this.groupControl_banChi.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            this.groupControl_banChi.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControl_banChi.Name = "groupControl_banChi";
            this.groupControl_banChi.Size = new System.Drawing.Size(40, 522);
            this.groupControl_banChi.TabIndex = 10;
            this.groupControl_banChi.Text = "班次";
            // 
            // elementHost1
            // 
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(200, 100);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新增病人ToolStripMenuItem,
            this.修改患者ToolStripMenuItem,
            this.labListRecord});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(125, 70);
            // 
            // 新增病人ToolStripMenuItem
            // 
            this.新增病人ToolStripMenuItem.Name = "新增病人ToolStripMenuItem";
            this.新增病人ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.新增病人ToolStripMenuItem.Text = "新增病人";
            this.新增病人ToolStripMenuItem.Click += new System.EventHandler(this.添加给药记录ToolStripMenuItem_Click);
            // 
            // 修改患者ToolStripMenuItem
            // 
            this.修改患者ToolStripMenuItem.Name = "修改患者ToolStripMenuItem";
            this.修改患者ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.修改患者ToolStripMenuItem.Text = "修改病人";
            this.修改患者ToolStripMenuItem.Click += new System.EventHandler(this.修改患者ToolStripMenuItem_Click);
            // 
            // labListRecord
            // 
            this.labListRecord.Name = "labListRecord";
            this.labListRecord.Size = new System.Drawing.Size(124, 22);
            this.labListRecord.Text = "检验记录";
            this.labListRecord.Click += new System.EventHandler(this.labListRecord_Click);
            // 
            // CtlScheduleMainNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.dockPanel1);
            this.Name = "CtlScheduleMainNew";
            this.Size = new System.Drawing.Size(1033, 522);
            this.Load += new System.EventHandler(this.CtlScheduleMainNew_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlOperation)).EndInit();
            this.pnlOperation.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListPatient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchPatient.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.xtraScrollableControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl_office)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl_banChi)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraEditors.PanelControl pnlOperation;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraTreeList.TreeList treeListPatient;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.ImageList smallimageList;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private Doc.动静脉内瘘血管吻合术同意书 动静脉内瘘血管吻合术同意书1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private Hemo.Client.Controls.DXSimpleButton btnQuery;
        private DevExpress.XtraEditors.TextEdit txtSearchPatient;
        private BusyIndicator busyIndicator1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 新增病人ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改患者ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem labListRecord;
        private DevExpress.XtraEditors.GroupControl groupControl_office;
        private DevExpress.XtraEditors.GroupControl groupControl_banChi;
        private System.Windows.Forms.Panel panel1;
    }
}
