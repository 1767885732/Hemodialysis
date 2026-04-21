namespace Hemo.Client.UI.DataReport.HemoInfos
{
    partial class PatientHemoInfo
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.treeViewInfo = new DevExpress.XtraTreeList.TreeList();
            this.hemoColumn = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnUploadAll = new DevExpress.XtraEditors.SimpleButton();
            this.panelContainers = new DevExpress.XtraEditors.PanelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnUpLoad = new DevExpress.XtraEditors.SimpleButton();
            this.ctlUserLongInfo1 = new Hemo.Client.Controls.CtlUserLongInfo();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeViewInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelContainers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
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
            this.hemoColumn});
            this.treeViewInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewInfo.Location = new System.Drawing.Point(0, 0);
            this.treeViewInfo.Name = "treeViewInfo";
            this.treeViewInfo.BeginUnboundLoad();
            this.treeViewInfo.AppendNode(new object[] {
            "血管通路"}, -1);
            this.treeViewInfo.AppendNode(new object[] {
            "透析处方"}, -1);
            this.treeViewInfo.AppendNode(new object[] {
            "血压测量"}, -1);
            this.treeViewInfo.AppendNode(new object[] {
            "透析充分性"}, -1);
            this.treeViewInfo.AppendNode(new object[] {
            "抗凝剂"}, -1);
            this.treeViewInfo.AppendNode(new object[] {
            "干体重"}, -1);
            this.treeViewInfo.AppendNode(new object[] {
            "合用其它透析模式"}, -1);
            this.treeViewInfo.EndUnboundLoad();
            this.treeViewInfo.OptionsBehavior.AllowExpandOnDblClick = false;
            this.treeViewInfo.OptionsBehavior.AutoChangeParent = false;
            this.treeViewInfo.OptionsBehavior.Editable = false;
            this.treeViewInfo.OptionsBehavior.MoveOnEdit = false;
            this.treeViewInfo.OptionsBehavior.ResizeNodes = false;
            this.treeViewInfo.OptionsLayout.AddNewColumns = false;
            this.treeViewInfo.OptionsMenu.EnableColumnMenu = false;
            this.treeViewInfo.OptionsMenu.EnableFooterMenu = false;
            this.treeViewInfo.OptionsView.ShowRoot = false;
            this.treeViewInfo.Size = new System.Drawing.Size(150, 457);
            this.treeViewInfo.TabIndex = 0;
            this.treeViewInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewInfo_MouseDown);
            // 
            // hemoColumn
            // 
            this.hemoColumn.Caption = "血透信息";
            this.hemoColumn.FieldName = "血透信息";
            this.hemoColumn.Name = "hemoColumn";
            this.hemoColumn.Visible = true;
            this.hemoColumn.VisibleIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnUploadAll);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 457);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(150, 35);
            this.panelControl2.TabIndex = 0;
            // 
            // btnUploadAll
            // 
            this.btnUploadAll.Location = new System.Drawing.Point(27, 6);
            this.btnUploadAll.Name = "btnUploadAll";
            this.btnUploadAll.Size = new System.Drawing.Size(75, 23);
            this.btnUploadAll.TabIndex = 0;
            this.btnUploadAll.Text = "一键上传";
            this.btnUploadAll.Visible = false;
            this.btnUploadAll.Click += new System.EventHandler(this.btnUploadAll_Click);
            // 
            // panelContainers
            // 
            this.panelContainers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainers.Location = new System.Drawing.Point(0, 43);
            this.panelContainers.Name = "panelContainers";
            this.panelContainers.Size = new System.Drawing.Size(780, 414);
            this.panelContainers.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnUpLoad);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 457);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(780, 35);
            this.panelControl1.TabIndex = 1;
            // 
            // btnUpLoad
            // 
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
            this.ctlUserLongInfo1.PanelWidth = 780;
            this.ctlUserLongInfo1.PatientType = "";
            this.ctlUserLongInfo1.PatientTypeEnabled = true;
            this.ctlUserLongInfo1.Size = new System.Drawing.Size(780, 43);
            this.ctlUserLongInfo1.TabIndex = 0;
            // 
            // PatientHemoInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "PatientHemoInfo";
            this.Size = new System.Drawing.Size(936, 492);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeViewInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelContainers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTreeList.TreeList treeViewInfo;
        private DevExpress.XtraTreeList.Columns.TreeListColumn hemoColumn;
        private DevExpress.XtraEditors.PanelControl panelContainers;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnUploadAll;
        private DevExpress.XtraEditors.SimpleButton btnUpLoad;
        private Controls.CtlUserLongInfo ctlUserLongInfo1;
    }
}
