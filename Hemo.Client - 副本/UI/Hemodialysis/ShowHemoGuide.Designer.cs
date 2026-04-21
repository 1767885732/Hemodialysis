namespace Hemo.Client.UI.Hemodialysis {
    partial class ShowHemoGuide {
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
            this.treCatalog = new DevExpress.XtraTreeList.TreeList();
            this.treColumn = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel2 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton1 = new Hemo.Client.Controls.DXSimpleButton();
            this.txtContent = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.treCatalog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel2.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // treCatalog
            // 
            this.treCatalog.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treColumn});
            this.treCatalog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treCatalog.Location = new System.Drawing.Point(0, 0);
            this.treCatalog.Name = "treCatalog";
            this.treCatalog.BeginUnboundLoad();
            this.treCatalog.AppendNode(new object[] {
            "血液滤过(HF)"}, -1);
            this.treCatalog.AppendNode(new object[] {
            "致热原反应和败血症"}, 0);
            this.treCatalog.AppendNode(new object[] {
            "氨基酸与蛋白质丢失"}, 0);
            this.treCatalog.AppendNode(new object[] {
            "血液透析(HD)"}, -1);
            this.treCatalog.AppendNode(new object[] {
            "透析中低血压"}, 3);
            this.treCatalog.AppendNode(new object[] {
            "溶血"}, 3);
            this.treCatalog.AppendNode(new object[] {
            "空气栓塞"}, 3);
            this.treCatalog.AppendNode(new object[] {
            "发热"}, 3);
            this.treCatalog.AppendNode(new object[] {
            "透析器破膜"}, 3);
            this.treCatalog.AppendNode(new object[] {
            "体外循环凝血"}, 3);
            this.treCatalog.AppendNode(new object[] {
            "肌肉痉挛"}, 3);
            this.treCatalog.AppendNode(new object[] {
            "恶心和呕吐"}, 3);
            this.treCatalog.AppendNode(new object[] {
            "头痛"}, 3);
            this.treCatalog.AppendNode(new object[] {
            "胸痛和背痛"}, 3);
            this.treCatalog.AppendNode(new object[] {
            "皮肤搔痒"}, 3);
            this.treCatalog.AppendNode(new object[] {
            "失衡综合症"}, 3);
            this.treCatalog.AppendNode(new object[] {
            "透析器反应"}, 3);
            this.treCatalog.AppendNode(new object[] {
            "心律失常"}, 3);
            this.treCatalog.AppendNode(new object[] {
            "血液透析滤过（HDF）"}, -1);
            this.treCatalog.AppendNode(new object[] {
            "反超滤"}, 18);
            this.treCatalog.AppendNode(new object[] {
            "蛋白丢失"}, 18);
            this.treCatalog.AppendNode(new object[] {
            "缺失综合征"}, 18);
            this.treCatalog.AppendNode(new object[] {
            "连续性肾脏替代疗法(CRRT)"}, -1);
            this.treCatalog.AppendNode(new object[] {
            "单纯超滤"}, -1);
            this.treCatalog.AppendNode(new object[] {
            "滤器破膜漏血"}, 23);
            this.treCatalog.AppendNode(new object[] {
            "滤器和管路凝血"}, 23);
            this.treCatalog.AppendNode(new object[] {
            "出血"}, 23);
            this.treCatalog.AppendNode(new object[] {
            "低血压"}, 23);
            this.treCatalog.AppendNode(new object[] {
            "心律失常、猝死"}, 23);
            this.treCatalog.EndUnboundLoad();
            this.treCatalog.Size = new System.Drawing.Size(256, 587);
            this.treCatalog.TabIndex = 0;
            this.treCatalog.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treCatalog_FocusedNodeChanged);
            // 
            // treColumn
            // 
            this.treColumn.Caption = "并发症及处理";
            this.treColumn.FieldName = "并发症及处理";
            this.treColumn.MinWidth = 38;
            this.treColumn.Name = "treColumn";
            this.treColumn.OptionsColumn.AllowEdit = false;
            this.treColumn.OptionsColumn.AllowMove = false;
            this.treColumn.OptionsColumn.FixedWidth = true;
            this.treColumn.Visible = true;
            this.treColumn.VisibleIndex = 0;
            this.treColumn.Width = 124;
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel2});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dockPanel2
            // 
            this.dockPanel2.Controls.Add(this.dockPanel2_Container);
            this.dockPanel2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel2.ID = new System.Guid("72d050b8-6645-42e6-9677-bf9c270ac1f8");
            this.dockPanel2.Location = new System.Drawing.Point(0, 0);
            this.dockPanel2.Name = "dockPanel2";
            this.dockPanel2.Options.ShowCloseButton = false;
            this.dockPanel2.OriginalSize = new System.Drawing.Size(262, 200);
            this.dockPanel2.Size = new System.Drawing.Size(262, 615);
            this.dockPanel2.Text = "操作指导目录";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.treCatalog);
            this.dockPanel2_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(256, 587);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Controls.Add(this.txtContent);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(262, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(598, 615);
            this.panelControl1.TabIndex = 2;
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageIndex = 5;
            this.simpleButton1.Location = new System.Drawing.Point(496, 580);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "测试";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(2, 2);
            this.txtContent.Name = "txtContent";
            this.txtContent.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContent.Properties.Appearance.Options.UseFont = true;
            this.txtContent.Size = new System.Drawing.Size(594, 572);
            this.txtContent.TabIndex = 0;
            // 
            // ShowHemoGuide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 615);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.dockPanel2);
            this.Name = "ShowHemoGuide";
            this.Text = "血透操作指导";
            ((System.ComponentModel.ISupportInitialize)(this.treCatalog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel2.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treCatalog;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treColumn;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel2;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.MemoEdit txtContent;
        private Hemo.Client.Controls.DXSimpleButton simpleButton1;
    }
}