using Hemo.Client.Controls;
namespace Hemo.Client.UI.PatientFixUI
{
    partial class PatientKnowBooksUI
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("血液净化记录单", 6, 6);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("血液透析病历首页");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("连续性肾脏替代治疗知情同意书", 5, 4);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("血液净化治疗知情同意书", 5, 4);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("授权委托书", 5, 4);
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("血液灌流知情同意书", 5, 4);
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("动静脉内瘘血管吻合术同意书", 5, 4);
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("中心静脉置管术知情同意书", 5, 4);
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("枸橼酸抗凝同意书", 5, 4);
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("急诊施行血液灌流同意书", 5, 4);
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("抗生素皮试知情同意书", 5, 4);
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("血透同意书", 5, 4);
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("术后告知", 5, 4);
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("血透病人告知书", 5, 4);
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("穿刺风险告之书", 5, 4);
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("自购药品使用知情同意书", 5, 4);
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("无肝素血液透析风险知情同意书", 5, 4);
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("危重病人急诊行床边血液净化治疗同意书", 5, 4);
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("知情同意书", 6, 6, new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode16,
            treeNode17,
            treeNode18});
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.tlDocments = new System.Windows.Forms.TreeView();
            this.smallimageList = new System.Windows.Forms.ImageList(this.components);
            this.lblCureIDList = new System.Windows.Forms.Label();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.bigimageList = new System.Windows.Forms.ImageList(this.components);
            this.busyIndicator1 = new Hemo.Client.Controls.BusyIndicator();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSign = new Hemo.Client.Controls.DXSimpleButton();
            this.btnPDSign = new Hemo.Client.Controls.DXSimpleButton();
            this.btnPNSign = new Hemo.Client.Controls.DXSimpleButton();
            this.btnCNSign = new Hemo.Client.Controls.DXSimpleButton();
            this.btnUpload = new Hemo.Client.Controls.DXSimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.lupSignType = new DevExpress.XtraEditors.LookUpEdit();
            this.documentContainerHost = new System.Windows.Forms.Integration.ElementHost();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lupSignType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.BackColor = System.Drawing.SystemColors.Window;
            this.groupControl1.Appearance.Options.UseBackColor = true;
            this.groupControl1.Controls.Add(this.xtraScrollableControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            this.groupControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(200, 518);
            this.groupControl1.TabIndex = 8;
            this.groupControl1.Text = "患者文书列表";
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Controls.Add(this.tlDocments);
            this.xtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl1.Location = new System.Drawing.Point(2, 19);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(196, 497);
            this.xtraScrollableControl1.TabIndex = 0;
            // 
            // tlDocments
            // 
            this.tlDocments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlDocments.CheckBoxes = true;
            this.tlDocments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlDocments.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tlDocments.ImageIndex = 0;
            this.tlDocments.ImageList = this.smallimageList;
            this.tlDocments.Location = new System.Drawing.Point(0, 0);
            this.tlDocments.Name = "tlDocments";
            treeNode1.ImageIndex = 6;
            treeNode1.Name = "节点1";
            treeNode1.SelectedImageIndex = 6;
            treeNode1.Text = "血液净化记录单";
            treeNode2.Name = "血液透析病历首页";
            treeNode2.Text = "血液透析病历首页";
            treeNode3.ImageIndex = 5;
            treeNode3.Name = "节点1";
            treeNode3.SelectedImageIndex = 4;
            treeNode3.Tag = "b";
            treeNode3.Text = "连续性肾脏替代治疗知情同意书";
            treeNode4.ImageIndex = 5;
            treeNode4.Name = "节点0";
            treeNode4.SelectedImageIndex = 4;
            treeNode4.Tag = "e";
            treeNode4.Text = "血液净化治疗知情同意书";
            treeNode5.ImageIndex = 5;
            treeNode5.Name = "节点1";
            treeNode5.SelectedImageIndex = 4;
            treeNode5.Text = "授权委托书";
            treeNode6.ImageIndex = 5;
            treeNode6.Name = "节点2";
            treeNode6.SelectedImageIndex = 4;
            treeNode6.Tag = "c";
            treeNode6.Text = "血液灌流知情同意书";
            treeNode7.ImageIndex = 5;
            treeNode7.Name = "节点0";
            treeNode7.SelectedImageIndex = 4;
            treeNode7.Text = "动静脉内瘘血管吻合术同意书";
            treeNode8.ImageIndex = 5;
            treeNode8.Name = "节点3";
            treeNode8.SelectedImageIndex = 4;
            treeNode8.Tag = "g";
            treeNode8.Text = "中心静脉置管术知情同意书";
            treeNode9.ImageIndex = 5;
            treeNode9.Name = "节点2";
            treeNode9.SelectedImageIndex = 4;
            treeNode9.Text = "枸橼酸抗凝同意书";
            treeNode10.ImageIndex = 5;
            treeNode10.Name = "节点3";
            treeNode10.SelectedImageIndex = 4;
            treeNode10.Text = "急诊施行血液灌流同意书";
            treeNode11.ImageIndex = 5;
            treeNode11.Name = "节点4";
            treeNode11.SelectedImageIndex = 4;
            treeNode11.Text = "抗生素皮试知情同意书";
            treeNode12.ImageIndex = 5;
            treeNode12.Name = "节点0";
            treeNode12.SelectedImageIndex = 4;
            treeNode12.Text = "血透同意书";
            treeNode13.ImageIndex = 5;
            treeNode13.Name = "节点1";
            treeNode13.SelectedImageIndex = 4;
            treeNode13.Text = "术后告知";
            treeNode14.ImageIndex = 5;
            treeNode14.Name = "血透病人告知书";
            treeNode14.SelectedImageIndex = 4;
            treeNode14.Text = "血透病人告知书";
            treeNode15.ImageIndex = 5;
            treeNode15.Name = "节点1";
            treeNode15.SelectedImageIndex = 4;
            treeNode15.Text = "穿刺风险告之书";
            treeNode16.ImageIndex = 5;
            treeNode16.Name = "节点0";
            treeNode16.SelectedImageIndex = 4;
            treeNode16.Text = "自购药品使用知情同意书";
            treeNode17.ImageIndex = 5;
            treeNode17.Name = "节点0";
            treeNode17.SelectedImageIndex = 4;
            treeNode17.Text = "无肝素血液透析风险知情同意书";
            treeNode18.ImageIndex = 5;
            treeNode18.Name = "节点0";
            treeNode18.SelectedImageIndex = 4;
            treeNode18.Text = "危重病人急诊行床边血液净化治疗同意书";
            treeNode19.ImageIndex = 6;
            treeNode19.Name = "节点0";
            treeNode19.SelectedImageIndex = 6;
            treeNode19.Text = "知情同意书";
            this.tlDocments.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode19});
            this.tlDocments.SelectedImageIndex = 0;
            this.tlDocments.ShowRootLines = false;
            this.tlDocments.Size = new System.Drawing.Size(196, 497);
            this.tlDocments.TabIndex = 1;
            this.tlDocments.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tlDocments_NodeMouseClick);
            this.tlDocments.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tlDocments_NodeMouseDoubleClick);
            // 
            // smallimageList
            // 
            this.smallimageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.smallimageList.ImageSize = new System.Drawing.Size(16, 16);
            this.smallimageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // lblCureIDList
            // 
            this.lblCureIDList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCureIDList.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCureIDList.ForeColor = System.Drawing.Color.Black;
            this.lblCureIDList.Location = new System.Drawing.Point(3, 0);
            this.lblCureIDList.Name = "lblCureIDList";
            this.lblCureIDList.Size = new System.Drawing.Size(51, 16);
            this.lblCureIDList.TabIndex = 11;
            this.lblCureIDList.Visible = false;
            // 
            // dockManager1
            // 
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // bigimageList
            // 
            this.bigimageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.bigimageList.ImageSize = new System.Drawing.Size(16, 16);
            this.bigimageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // busyIndicator1
            // 
            this.busyIndicator1.Location = new System.Drawing.Point(619, 315);
            this.busyIndicator1.Name = "busyIndicator1";
            this.busyIndicator1.Size = new System.Drawing.Size(100, 40);
            this.busyIndicator1.TabIndex = 9;
            this.busyIndicator1.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btnSign, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnPDSign, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnPNSign, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCNSign, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnUpload, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblCureIDList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelControl2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(200, 483);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(894, 35);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // btnSign
            // 
            this.btnSign.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSign.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnSign.Appearance.Options.UseFont = true;
            this.btnSign.ImageIndex = 2;
            this.btnSign.Location = new System.Drawing.Point(288, 3);
            this.btnSign.Name = "btnSign";
            this.btnSign.Size = new System.Drawing.Size(101, 29);
            this.btnSign.TabIndex = 350;
            this.btnSign.Text = "患者签名";
            this.btnSign.Click += new System.EventHandler(this.btnSign_Click);
            // 
            // btnPDSign
            // 
            this.btnPDSign.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPDSign.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnPDSign.Appearance.Options.UseFont = true;
            this.btnPDSign.ImageIndex = 2;
            this.btnPDSign.Location = new System.Drawing.Point(395, 3);
            this.btnPDSign.Name = "btnPDSign";
            this.btnPDSign.Size = new System.Drawing.Size(101, 29);
            this.btnPDSign.TabIndex = 352;
            this.btnPDSign.Text = "责任医生签名";
            this.btnPDSign.Click += new System.EventHandler(this.btnPDSign_Click);
            // 
            // btnPNSign
            // 
            this.btnPNSign.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPNSign.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnPNSign.Appearance.Options.UseFont = true;
            this.btnPNSign.ImageIndex = 2;
            this.btnPNSign.Location = new System.Drawing.Point(502, 3);
            this.btnPNSign.Name = "btnPNSign";
            this.btnPNSign.Size = new System.Drawing.Size(101, 29);
            this.btnPNSign.TabIndex = 354;
            this.btnPNSign.Text = "责任护士签名";
            this.btnPNSign.Click += new System.EventHandler(this.btnPNSign_Click);
            // 
            // btnCNSign
            // 
            this.btnCNSign.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCNSign.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnCNSign.Appearance.Options.UseFont = true;
            this.btnCNSign.ImageIndex = 2;
            this.btnCNSign.Location = new System.Drawing.Point(609, 3);
            this.btnCNSign.Name = "btnCNSign";
            this.btnCNSign.Size = new System.Drawing.Size(101, 29);
            this.btnCNSign.TabIndex = 355;
            this.btnCNSign.Text = "审核护士签名";
            this.btnCNSign.Click += new System.EventHandler(this.btnCNSign_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpload.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnUpload.Appearance.Options.UseFont = true;
            this.btnUpload.ImageIndex = 19;
            this.btnUpload.Location = new System.Drawing.Point(716, 3);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(175, 29);
            this.btnUpload.TabIndex = 356;
            this.btnUpload.Text = "上传扫描件";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.labelControl6);
            this.panelControl2.Controls.Add(this.lupSignType);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(181, 3);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(101, 29);
            this.panelControl2.TabIndex = 357;
            // 
            // labelControl6
            // 
            this.labelControl6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelControl6.Location = new System.Drawing.Point(-47, 7);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(48, 14);
            this.labelControl6.TabIndex = 389;
            this.labelControl6.Text = "签名方式";
            // 
            // lupSignType
            // 
            this.lupSignType.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lupSignType.EnterMoveNextControl = true;
            this.lupSignType.Location = new System.Drawing.Point(7, 3);
            this.lupSignType.Name = "lupSignType";
            this.lupSignType.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lupSignType.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lupSignType.Properties.Appearance.Options.UseFont = true;
            this.lupSignType.Properties.Appearance.Options.UseForeColor = true;
            this.lupSignType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupSignType.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.lupSignType.Properties.NullText = "";
            this.lupSignType.Size = new System.Drawing.Size(90, 24);
            this.lupSignType.TabIndex = 388;
            this.lupSignType.EditValueChanged += new System.EventHandler(this.lupSignType_EditValueChanged);
            // 
            // documentContainerHost
            // 
            this.documentContainerHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentContainerHost.Location = new System.Drawing.Point(200, 0);
            this.documentContainerHost.Name = "documentContainerHost";
            this.documentContainerHost.Size = new System.Drawing.Size(894, 483);
            this.documentContainerHost.TabIndex = 14;
            this.documentContainerHost.Text = "elementHost1";
            this.documentContainerHost.Child = null;
            // 
            // PatientKnowBooksUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.documentContainerHost);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.busyIndicator1);
            this.Controls.Add(this.groupControl1);
            this.Name = "PatientKnowBooksUI";
            this.Size = new System.Drawing.Size(1094, 518);
            this.Load += new System.EventHandler(this.PatientKnowBooksUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.xtraScrollableControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lupSignType.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private System.Windows.Forms.TreeView tlDocments;
     
        
    
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private System.Windows.Forms.ImageList bigimageList;
        private System.Windows.Forms.ImageList smallimageList;
        private System.Windows.Forms.Label lblCureIDList;
        private BusyIndicator busyIndicator1;
        private System.Windows.Forms.Label lblRoom;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DXSimpleButton btnSign;
        private DXSimpleButton btnPDSign;
        private DXSimpleButton btnPNSign;
        private DXSimpleButton btnCNSign;
        private System.Windows.Forms.Integration.ElementHost documentContainerHost;
        private DXSimpleButton btnUpload;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LookUpEdit lupSignType;
        private DevExpress.XtraEditors.LabelControl labelControl6;
    }
}
