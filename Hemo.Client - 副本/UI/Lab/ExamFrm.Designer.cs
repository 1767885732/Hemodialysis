namespace Hemo.Client.UI.Lab
{
    partial class ExamFrm
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
            this.ctlUserInfo = new Hemo.Client.UI.Hemodialysis.CtlUserInfo();
            this.lblPicture = new System.Windows.Forms.PictureBox();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnExport = new Hemo.Client.Controls.DXSimpleButton();
            this.btnClose = new Hemo.Client.Controls.DXSimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.busyIndicator = new Hemo.Client.Controls.BusyIndicator();
            this.gcLabMain = new DevExpress.XtraGrid.GridControl();
            this.gvLabMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcLabDetail = new DevExpress.XtraGrid.GridControl();
            this.gvLabDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new Hemo.Client.Controls.DXSimpleButton();
            this.btnPrint = new Hemo.Client.Controls.DXSimpleButton();
            this.labelControl33 = new DevExpress.XtraEditors.LabelControl();
            this.cmbSTART_DATE = new DevExpress.XtraEditors.DateEdit();
            this.labelControl62 = new DevExpress.XtraEditors.LabelControl();
            this.cmbEND_DATE = new DevExpress.XtraEditors.DateEdit();
            this.btnSyncExamInfo = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lblPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcLabMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLabMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLabDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLabDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_DATE.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_DATE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_DATE.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_DATE.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlUserInfo
            // 
            this.ctlUserInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlUserInfo.FormContainer = null;
            this.ctlUserInfo.HEMODIALYSIS_ID = "";
            this.ctlUserInfo.Location = new System.Drawing.Point(0, 0);
            this.ctlUserInfo.Name = "ctlUserInfo";
            this.ctlUserInfo.Size = new System.Drawing.Size(919, 102);
            this.ctlUserInfo.TabIndex = 1;
            // 
            // lblPicture
            // 
            this.lblPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.lblPicture.Location = new System.Drawing.Point(820, 7);
            this.lblPicture.Name = "lblPicture";
            this.lblPicture.Size = new System.Drawing.Size(91, 92);
            this.lblPicture.TabIndex = 315;
            this.lblPicture.TabStop = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnSyncExamInfo);
            this.panelControl1.Controls.Add(this.btnExport);
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 505);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(919, 35);
            this.panelControl1.TabIndex = 316;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.ImageIndex = 9;
            this.btnExport.Location = new System.Drawing.Point(612, 4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(73, 27);
            this.btnExport.TabIndex = 403;
            this.btnExport.Text = "导出(&O)";
            this.btnExport.Visible = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.ImageIndex = 3;
            this.btnClose.Location = new System.Drawing.Point(821, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(73, 27);
            this.btnClose.TabIndex = 402;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.splitContainerControl1);
            this.panelControl2.Controls.Add(this.panelControl3);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 102);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(919, 403);
            this.panelControl2.TabIndex = 317;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(2, 37);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.busyIndicator);
            this.splitContainerControl1.Panel1.Controls.Add(this.gcLabMain);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gcLabDetail);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(915, 364);
            this.splitContainerControl1.SplitterPosition = 457;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // busyIndicator
            // 
            this.busyIndicator.Location = new System.Drawing.Point(142, 133);
            this.busyIndicator.Name = "busyIndicator";
            this.busyIndicator.Size = new System.Drawing.Size(185, 75);
            this.busyIndicator.TabIndex = 401;
            this.busyIndicator.Visible = false;
            // 
            // gcLabMain
            // 
            this.gcLabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcLabMain.Location = new System.Drawing.Point(0, 0);
            this.gcLabMain.MainView = this.gvLabMain;
            this.gcLabMain.Name = "gcLabMain";
            this.gcLabMain.Size = new System.Drawing.Size(457, 364);
            this.gcLabMain.TabIndex = 394;
            this.gcLabMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLabMain});
            // 
            // gvLabMain
            // 
            this.gvLabMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13});
            this.gvLabMain.GridControl = this.gcLabMain;
            this.gvLabMain.Name = "gvLabMain";
            this.gvLabMain.OptionsBehavior.Editable = false;
            this.gvLabMain.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gvLabMain.OptionsView.ShowGroupPanel = false;
            this.gvLabMain.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvLabMain_RowClick);
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "检查序号";
            this.gridColumn10.FieldName = "EXAM_NO";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 0;
            this.gridColumn10.Width = 70;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "检查类别";
            this.gridColumn11.FieldName = "EXAM_CLASS";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 1;
            this.gridColumn11.Width = 90;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "检验子类";
            this.gridColumn12.FieldName = "EXAM_SUB_CLASS";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 2;
            this.gridColumn12.Width = 90;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "检查日期";
            this.gridColumn13.FieldName = "EXAM_DATE_TIME";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 3;
            this.gridColumn13.Width = 129;
            // 
            // gcLabDetail
            // 
            this.gcLabDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcLabDetail.Location = new System.Drawing.Point(0, 0);
            this.gcLabDetail.MainView = this.gvLabDetail;
            this.gcLabDetail.Name = "gcLabDetail";
            this.gcLabDetail.Size = new System.Drawing.Size(453, 364);
            this.gcLabDetail.TabIndex = 395;
            this.gcLabDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLabDetail});
            // 
            // gvLabDetail
            // 
            this.gvLabDetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn7,
            this.gridColumn8});
            this.gvLabDetail.GridControl = this.gcLabDetail;
            this.gvLabDetail.Name = "gvLabDetail";
            this.gvLabDetail.OptionsBehavior.Editable = false;
            this.gvLabDetail.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gvLabDetail.OptionsView.ColumnAutoWidth = false;
            this.gvLabDetail.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "项目名称";
            this.gridColumn1.FieldName = "EXAM_ITEM";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 200;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "检查结果";
            this.gridColumn7.FieldName = "IMPRESSION";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            this.gridColumn7.Width = 500;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "是否阳性";
            this.gridColumn8.FieldName = "IS_ABNORMAL";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 2;
            this.gridColumn8.Width = 70;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.btnSearch);
            this.panelControl3.Controls.Add(this.btnPrint);
            this.panelControl3.Controls.Add(this.labelControl33);
            this.panelControl3.Controls.Add(this.cmbSTART_DATE);
            this.panelControl3.Controls.Add(this.labelControl62);
            this.panelControl3.Controls.Add(this.cmbEND_DATE);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(2, 2);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(915, 35);
            this.panelControl3.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.ImageIndex = 8;
            this.btnSearch.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(733, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 27);
            this.btnSearch.TabIndex = 398;
            this.btnSearch.Text = "查询(&Q)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.ImageIndex = 6;
            this.btnPrint.Location = new System.Drawing.Point(816, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 27);
            this.btnPrint.TabIndex = 399;
            this.btnPrint.Text = "打印(&P)";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // labelControl33
            // 
            this.labelControl33.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl33.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.labelControl33.Location = new System.Drawing.Point(38, 9);
            this.labelControl33.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl33.Name = "labelControl33";
            this.labelControl33.Size = new System.Drawing.Size(48, 17);
            this.labelControl33.TabIndex = 394;
            this.labelControl33.Text = "检验日期";
            // 
            // cmbSTART_DATE
            // 
            this.cmbSTART_DATE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSTART_DATE.EditValue = null;
            this.cmbSTART_DATE.EnterMoveNextControl = true;
            this.cmbSTART_DATE.Location = new System.Drawing.Point(94, 6);
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
            this.labelControl62.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl62.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl62.Location = new System.Drawing.Point(187, 8);
            this.labelControl62.Name = "labelControl62";
            this.labelControl62.Size = new System.Drawing.Size(9, 17);
            this.labelControl62.TabIndex = 392;
            this.labelControl62.Text = "~";
            // 
            // cmbEND_DATE
            // 
            this.cmbEND_DATE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEND_DATE.EditValue = null;
            this.cmbEND_DATE.EnterMoveNextControl = true;
            this.cmbEND_DATE.Location = new System.Drawing.Point(201, 6);
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
            // btnSyncExamInfo
            // 
            this.btnSyncExamInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSyncExamInfo.ImageIndex = 22;
            this.btnSyncExamInfo.Location = new System.Drawing.Point(691, 4);
            this.btnSyncExamInfo.Name = "btnSyncExamInfo";
            this.btnSyncExamInfo.Size = new System.Drawing.Size(124, 27);
            this.btnSyncExamInfo.TabIndex = 405;
            this.btnSyncExamInfo.Text = "同步检查记录(&E)";
            this.btnSyncExamInfo.Click += new System.EventHandler(this.btnSyncExamInfo_Click);
            // 
            // ExamFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 540);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.lblPicture);
            this.Controls.Add(this.ctlUserInfo);
            this.MinimumSize = new System.Drawing.Size(830, 400);
            this.Name = "ExamFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "检查记录";
            this.Load += new System.EventHandler(this.ExamFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lblPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcLabMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLabMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLabDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLabDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_DATE.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_DATE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_DATE.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_DATE.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Hemodialysis.CtlUserInfo ctlUserInfo;
        private System.Windows.Forms.PictureBox lblPicture;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.LabelControl labelControl33;
        private DevExpress.XtraEditors.DateEdit cmbSTART_DATE;
        private DevExpress.XtraEditors.LabelControl labelControl62;
        private DevExpress.XtraEditors.DateEdit cmbEND_DATE;
        private Controls.DXSimpleButton btnSearch;
        private Controls.DXSimpleButton btnPrint;
        private Controls.DXSimpleButton btnExport;
        private Controls.DXSimpleButton btnClose;
        private DevExpress.XtraGrid.GridControl gcLabMain;
        public DevExpress.XtraGrid.Views.Grid.GridView gvLabMain;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.GridControl gcLabDetail;
        public DevExpress.XtraGrid.Views.Grid.GridView gvLabDetail;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private Controls.BusyIndicator busyIndicator;
        private Controls.DXSimpleButton btnSyncExamInfo;
    }
}