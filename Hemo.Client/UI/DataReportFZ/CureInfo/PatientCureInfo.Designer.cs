namespace Hemo.Client.UI.DataReportFZ.CureInfo
{
    partial class PatientCureInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatientCureInfo));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.treeViewInfo = new DevExpress.XtraTreeList.TreeList();
            this.LabAndCheckColumn = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.panelContainers = new DevExpress.XtraEditors.PanelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.checkAll = new DevExpress.XtraEditors.CheckEdit();
            this.btnSearch = new Hemo.Client.Controls.DXSimpleButton();
            this.labelControl33 = new DevExpress.XtraEditors.LabelControl();
            this.cmbSTART_DATE = new DevExpress.XtraEditors.DateEdit();
            this.labelControl62 = new DevExpress.XtraEditors.LabelControl();
            this.cmbEND_DATE = new DevExpress.XtraEditors.DateEdit();
            this.btnUpLoad = new DevExpress.XtraEditors.SimpleButton();
            this.ctlUserLongInfo1 = new Hemo.Client.Controls.CtlUserLongInfo();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeViewInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelContainers)).BeginInit();
            this.panelContainers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_DATE.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_DATE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_DATE.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_DATE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.treeViewInfo);
            this.splitContainerControl1.Panel1.Controls.Add(this.panelControl2);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.panelContainers);
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl1);
            this.splitContainerControl1.Panel2.Controls.Add(this.ctlUserLongInfo1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(936, 492);
            this.splitContainerControl1.SplitterPosition = 150;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // treeViewInfo
            // 
            this.treeViewInfo.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.LabAndCheckColumn});
            this.treeViewInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewInfo.Location = new System.Drawing.Point(0, 0);
            this.treeViewInfo.Name = "treeViewInfo";
            this.treeViewInfo.BeginUnboundLoad();
            this.treeViewInfo.AppendNode(new object[] {
            "患者病历"}, -1);
            this.treeViewInfo.AppendNode(new object[] {
            "患者病程"}, -1);
            this.treeViewInfo.AppendNode(new object[] {
            "透析记录"}, -1);
            this.treeViewInfo.EndUnboundLoad();
            this.treeViewInfo.OptionsBehavior.AllowExpandOnDblClick = false;
            this.treeViewInfo.OptionsBehavior.AutoChangeParent = false;
            this.treeViewInfo.OptionsBehavior.Editable = false;
            this.treeViewInfo.OptionsBehavior.ResizeNodes = false;
            this.treeViewInfo.OptionsLayout.AddNewColumns = false;
            this.treeViewInfo.OptionsMenu.EnableColumnMenu = false;
            this.treeViewInfo.OptionsMenu.EnableFooterMenu = false;
            this.treeViewInfo.OptionsNavigation.MoveOnEdit = false;
            this.treeViewInfo.OptionsView.ShowRoot = false;
            this.treeViewInfo.Size = new System.Drawing.Size(150, 457);
            this.treeViewInfo.TabIndex = 2;
            this.treeViewInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewInfo_MouseDown);
            // 
            // LabAndCheckColumn
            // 
            this.LabAndCheckColumn.Caption = "患者治疗信息";
            this.LabAndCheckColumn.FieldName = "患者治疗信息";
            this.LabAndCheckColumn.Name = "LabAndCheckColumn";
            this.LabAndCheckColumn.Visible = true;
            this.LabAndCheckColumn.VisibleIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.simpleButton1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 457);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(150, 35);
            this.panelControl2.TabIndex = 0;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(27, 6);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "一键上传";
            this.simpleButton1.Visible = false;
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // panelContainers
            // 
            this.panelContainers.AllowTouchScroll = true;
            this.panelContainers.Controls.Add(this.xtraTabControl1);
            this.panelContainers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainers.FireScrollEventOnMouseWheel = true;
            this.panelContainers.Location = new System.Drawing.Point(0, 43);
            this.panelContainers.Name = "panelContainers";
            this.panelContainers.Size = new System.Drawing.Size(781, 414);
            this.panelContainers.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.checkAll);
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Controls.Add(this.labelControl33);
            this.panelControl1.Controls.Add(this.cmbSTART_DATE);
            this.panelControl1.Controls.Add(this.labelControl62);
            this.panelControl1.Controls.Add(this.cmbEND_DATE);
            this.panelControl1.Controls.Add(this.btnUpLoad);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 457);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(781, 35);
            this.panelControl1.TabIndex = 1;
            // 
            // checkAll
            // 
            this.checkAll.Location = new System.Drawing.Point(349, 8);
            this.checkAll.Name = "checkAll";
            this.checkAll.Properties.Caption = "全选";
            this.checkAll.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.checkAll.Size = new System.Drawing.Size(60, 19);
            this.checkAll.TabIndex = 396;
            this.checkAll.CheckedChanged += new System.EventHandler(this.checkAll_CheckedChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.ImageIndex = 4;
            this.btnSearch.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(268, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 22);
            this.btnSearch.TabIndex = 394;
            this.btnSearch.Text = "查询(&Q)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // labelControl33
            // 
            this.labelControl33.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl33.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.labelControl33.Location = new System.Drawing.Point(14, 10);
            this.labelControl33.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl33.Name = "labelControl33";
            this.labelControl33.Size = new System.Drawing.Size(24, 17);
            this.labelControl33.TabIndex = 395;
            this.labelControl33.Text = "日期";
            // 
            // cmbSTART_DATE
            // 
            this.cmbSTART_DATE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbSTART_DATE.EditValue = null;
            this.cmbSTART_DATE.EnterMoveNextControl = true;
            this.cmbSTART_DATE.Location = new System.Drawing.Point(67, 7);
            this.cmbSTART_DATE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbSTART_DATE.Name = "cmbSTART_DATE";
            this.cmbSTART_DATE.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbSTART_DATE.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.cmbSTART_DATE.Properties.Appearance.Options.UseFont = true;
            this.cmbSTART_DATE.Properties.Appearance.Options.UseForeColor = true;
            this.cmbSTART_DATE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSTART_DATE.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbSTART_DATE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cmbSTART_DATE.Size = new System.Drawing.Size(88, 24);
            this.cmbSTART_DATE.TabIndex = 391;
            // 
            // labelControl62
            // 
            this.labelControl62.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl62.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl62.Location = new System.Drawing.Point(160, 9);
            this.labelControl62.Name = "labelControl62";
            this.labelControl62.Size = new System.Drawing.Size(9, 17);
            this.labelControl62.TabIndex = 392;
            this.labelControl62.Text = "~";
            // 
            // cmbEND_DATE
            // 
            this.cmbEND_DATE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbEND_DATE.EditValue = null;
            this.cmbEND_DATE.EnterMoveNextControl = true;
            this.cmbEND_DATE.Location = new System.Drawing.Point(174, 7);
            this.cmbEND_DATE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbEND_DATE.Name = "cmbEND_DATE";
            this.cmbEND_DATE.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbEND_DATE.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.cmbEND_DATE.Properties.Appearance.Options.UseFont = true;
            this.cmbEND_DATE.Properties.Appearance.Options.UseForeColor = true;
            this.cmbEND_DATE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbEND_DATE.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbEND_DATE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cmbEND_DATE.Size = new System.Drawing.Size(88, 24);
            this.cmbEND_DATE.TabIndex = 393;
            // 
            // btnUpLoad
            // 
            this.btnUpLoad.Image = ((System.Drawing.Image)(resources.GetObject("btnUpLoad.Image")));
            this.btnUpLoad.Location = new System.Drawing.Point(687, 7);
            this.btnUpLoad.Name = "btnUpLoad";
            this.btnUpLoad.Size = new System.Drawing.Size(75, 23);
            this.btnUpLoad.TabIndex = 0;
            this.btnUpLoad.Text = "上传";
            this.btnUpLoad.Click += new System.EventHandler(this.btnUpLoad_Click);
            // 
            // ctlUserLongInfo1
            // 
            this.ctlUserLongInfo1.DIAGNOSE = "";
            this.ctlUserLongInfo1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlUserLongInfo1.FormContainer = null;
            this.ctlUserLongInfo1.HEMODIALYSIS_ID = "";
            this.ctlUserLongInfo1.Location = new System.Drawing.Point(0, 0);
            this.ctlUserLongInfo1.Name = "ctlUserLongInfo1";
            this.ctlUserLongInfo1.PanBorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.ctlUserLongInfo1.PanelWidth = 781;
            this.ctlUserLongInfo1.PatientType = "";
            this.ctlUserLongInfo1.PatientTypeEnabled = true;
            this.ctlUserLongInfo1.Size = new System.Drawing.Size(781, 43);
            this.ctlUserLongInfo1.TabIndex = 0;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(2, 2);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            this.xtraTabControl1.Size = new System.Drawing.Size(777, 410);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.AutoScroll = true;
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(771, 404);
            this.xtraTabPage1.Text = "xtraTabPage1";
            // 
            // PatientCureInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "PatientCureInfo";
            this.Size = new System.Drawing.Size(936, 492);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeViewInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelContainers)).EndInit();
            this.panelContainers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_DATE.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_DATE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_DATE.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_DATE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl panelContainers;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btnUpLoad;
        private Controls.CtlUserLongInfo ctlUserLongInfo1;
        private DevExpress.XtraTreeList.TreeList treeViewInfo;
        private DevExpress.XtraTreeList.Columns.TreeListColumn LabAndCheckColumn;
        private Controls.DXSimpleButton btnSearch;
        private DevExpress.XtraEditors.LabelControl labelControl33;
        private DevExpress.XtraEditors.DateEdit cmbSTART_DATE;
        private DevExpress.XtraEditors.LabelControl labelControl62;
        private DevExpress.XtraEditors.DateEdit cmbEND_DATE;
        private DevExpress.XtraEditors.CheckEdit checkAll;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
    }
}
