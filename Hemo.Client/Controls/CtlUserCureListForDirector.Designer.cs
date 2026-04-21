namespace Hemo.Client.Controls
{
    partial class CtlUserCureListForDirector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtlUserCureListForDirector));
            //DevExpress.XtraBars.Alerter.AlertButton alertButton1 = new DevExpress.XtraBars.Alerter.AlertButton();
            this.documentContainerHost = new System.Windows.Forms.Integration.ElementHost();
            this.smallimageList = new System.Windows.Forms.ImageList(this.components);
            this.largeImageList = new System.Windows.Forms.ImageList(this.components);
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.pnlPatientTreat = new System.Windows.Forms.FlowLayoutPanel();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.navBarGroup2 = new DevExpress.XtraNavBar.NavBarGroup();
            this.largeImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            //this.timerSyncTime = new System.Windows.Forms.Timer(this.components);
            //this.alertControl1 = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            //this.timerSaveMessage = new System.Windows.Forms.Timer(this.components);
            //this.timerShowMsg = new System.Windows.Forms.Timer(this.components);
            //this.timerShowOrders = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.largeImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // documentContainerHost
            // 
            this.documentContainerHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentContainerHost.Location = new System.Drawing.Point(2, 23);
            this.documentContainerHost.Name = "documentContainerHost";
            this.documentContainerHost.Size = new System.Drawing.Size(740, 565);
            this.documentContainerHost.TabIndex = 2;
            this.documentContainerHost.Text = "elementHost1";
            this.documentContainerHost.Child = null;
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
            // largeImageList
            // 
            this.largeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("largeImageList.ImageStream")));
            this.largeImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.largeImageList.Images.SetKeyName(0, "4934_hire-me.png");
            this.largeImageList.Images.SetKeyName(1, "4962_sitemap.png");
            this.largeImageList.Images.SetKeyName(2, "4974_plus.png");
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
            this.dockPanel1.ID = new System.Guid("7a8498e1-62da-4638-9f2b-78589e672fc3");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Options.ShowCloseButton = false;
            this.dockPanel1.OriginalSize = new System.Drawing.Size(236, 200);
            this.dockPanel1.Size = new System.Drawing.Size(236, 590);
            this.dockPanel1.Text = "病患列表";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.pnlPatientTreat);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(230, 562);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // pnlPatientTreat
            // 
            this.pnlPatientTreat.AutoScroll = true;
            this.pnlPatientTreat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPatientTreat.Location = new System.Drawing.Point(0, 0);
            this.pnlPatientTreat.Name = "pnlPatientTreat";
            this.pnlPatientTreat.Size = new System.Drawing.Size(230, 562);
            this.pnlPatientTreat.TabIndex = 6;
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(192, 137);
            this.navBarGroupControlContainer2.TabIndex = 1;
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(192, 137);
            this.panelControl2.TabIndex = 1;
            // 
            // navBarGroup2
            // 
            this.navBarGroup2.Caption = "知情同意书";
            this.navBarGroup2.ControlContainer = this.navBarGroupControlContainer2;
            this.navBarGroup2.Expanded = true;
            this.navBarGroup2.GroupClientHeight = 138;
            this.navBarGroup2.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup2.Name = "navBarGroup2";
            this.navBarGroup2.SmallImageIndex = 2;
            // 
            // largeImageCollection
            // 
            this.largeImageCollection.ImageSize = new System.Drawing.Size(32, 32);
            this.largeImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("largeImageCollection.ImageStream")));
            this.largeImageCollection.Images.SetKeyName(10, "DatabaseCopyDatabaseFile.png");
            this.largeImageCollection.Images.SetKeyName(11, "DatabaseSqlServer.png");
            this.largeImageCollection.Images.SetKeyName(12, "GroupInsertTables.png");
            this.largeImageCollection.Images.SetKeyName(13, "CellsDeleteSmart.png");
            this.largeImageCollection.Images.SetKeyName(14, "CellsInsertSmart.png");
            this.largeImageCollection.Images.SetKeyName(15, "FileEmailAsPdfEmailAttachment.png");
            this.largeImageCollection.Images.SetKeyName(16, "FooterInsertGallery.png");
            this.largeImageCollection.Images.SetKeyName(17, "FormatPainter.png");
            this.largeImageCollection.Images.SetKeyName(18, "GoToFooter.png");
            this.largeImageCollection.Images.SetKeyName(19, "GoToHeader.png");
            this.largeImageCollection.Images.SetKeyName(20, "GroupHeaderFooter.png");
            this.largeImageCollection.Images.SetKeyName(21, "GroupProtect.png");
            this.largeImageCollection.Images.SetKeyName(22, "GroupTableStylesExcel.png");
            this.largeImageCollection.Images.SetKeyName(23, "ReviewDeleteCommentPowerPoint.png");
            this.largeImageCollection.Images.SetKeyName(24, "ReviewNewComment.png");
            this.largeImageCollection.Images.SetKeyName(25, "ReviewNextCommentPowerPoint.png");
            this.largeImageCollection.Images.SetKeyName(26, "ReviewPreviousComment.png");
            this.largeImageCollection.Images.SetKeyName(27, "ReviewTrackChangesMenu.png");
            this.largeImageCollection.Images.SetKeyName(28, "Spelling.png");
            this.largeImageCollection.Images.SetKeyName(29, "AcceptChanges.png");
            this.largeImageCollection.Images.SetKeyName(30, "DesignMode.png");
            this.largeImageCollection.Images.SetKeyName(31, "DropCapInsertGallery.png");
            this.largeImageCollection.Images.SetKeyName(32, "FileMarkAsFinal.png");
            this.largeImageCollection.Images.SetKeyName(33, "ReviewRejectChange.png");
            this.largeImageCollection.Images.SetKeyName(34, "ViewNormalViewPowerPoint.png");
            this.largeImageCollection.Images.SetKeyName(35, "WordArtEditTextClassic.png");
            this.largeImageCollection.Images.SetKeyName(36, "ExchangeFolder.png");
            this.largeImageCollection.Images.SetKeyName(37, "ZoomClassic.png");
            this.largeImageCollection.Images.SetKeyName(38, "ViewsFormView.png");
            this.largeImageCollection.Images.SetKeyName(39, "BookmarkInsert.png");
            this.largeImageCollection.Images.SetKeyName(40, "CreateFormInDesignView.png");
            this.largeImageCollection.Images.SetKeyName(41, "CreateFormSplitForm.png");
            this.largeImageCollection.Images.SetKeyName(42, "ViewsFormView.png");
            this.largeImageCollection.Images.SetKeyName(43, "CreateFormBlankForm.png");
            this.largeImageCollection.Images.SetKeyName(44, "CreateForm.png");
            this.largeImageCollection.Images.SetKeyName(45, "CreateReport.png");
            this.largeImageCollection.Images.SetKeyName(46, "PageNambersInMarginsInsertGallery.png");
            this.largeImageCollection.Images.SetKeyName(47, "WindowNew.png");
            this.largeImageCollection.Images.SetKeyName(48, "WindowsSwitch.png");
            this.largeImageCollection.Images.SetKeyName(49, "tableselect.png");
            this.largeImageCollection.Images.SetKeyName(50, "CustomTableOfContentsGallery.png");
            this.largeImageCollection.Images.SetKeyName(51, "zoom-.png");
            this.largeImageCollection.Images.SetKeyName(52, "zoom+.png");
            this.largeImageCollection.Images.SetKeyName(53, "ControlProperties.png");
            this.largeImageCollection.Images.SetKeyName(54, "DocumentPanelTemplate.png");
            this.largeImageCollection.Images.SetKeyName(55, "Play.png");
            this.largeImageCollection.Images.SetKeyName(56, "Add Green Button.png");
            this.largeImageCollection.Images.SetKeyName(57, "Add.png");
            this.largeImageCollection.Images.SetKeyName(58, "Clear Green Button.png");
            this.largeImageCollection.Images.SetKeyName(59, "Import Document.png");
            this.largeImageCollection.Images.SetKeyName(60, "Pre.png");
            this.largeImageCollection.Images.SetKeyName(61, "Play Blue Button.png");
            this.largeImageCollection.Images.SetKeyName(62, "Play Green Button.png");
            this.largeImageCollection.Images.SetKeyName(63, "Red Ball.png");
            this.largeImageCollection.Images.SetKeyName(64, "Stop All.png");
            this.largeImageCollection.Images.SetKeyName(65, "Stop Green Button.png");
            this.largeImageCollection.Images.SetKeyName(66, "Stop Red Button.png");
            this.largeImageCollection.Images.SetKeyName(67, "Stop.png");
            this.largeImageCollection.Images.SetKeyName(68, "RefreshAll.png");
            this.largeImageCollection.Images.SetKeyName(69, "Refresh.png");
            this.largeImageCollection.Images.SetKeyName(70, "AddOrRemoveAttendees.png");
            this.largeImageCollection.Images.SetKeyName(71, "AddressBook.png");
            this.largeImageCollection.Images.SetKeyName(72, "1.png");
            this.largeImageCollection.Images.SetKeyName(73, "2.png");
            this.largeImageCollection.Images.SetKeyName(74, "3.png");
            this.largeImageCollection.Images.SetKeyName(75, "4.png");
            this.largeImageCollection.Images.SetKeyName(76, "5.png");
            this.largeImageCollection.Images.SetKeyName(77, "6.png");
            this.largeImageCollection.Images.SetKeyName(78, "7.png");
            this.largeImageCollection.Images.SetKeyName(79, "8.png");
            this.largeImageCollection.Images.SetKeyName(80, "9.png");
            this.largeImageCollection.Images.SetKeyName(81, "10.png");
            this.largeImageCollection.Images.SetKeyName(82, "11.png");
            this.largeImageCollection.Images.SetKeyName(83, "12.png");
            this.largeImageCollection.Images.SetKeyName(84, "13.png");
            this.largeImageCollection.Images.SetKeyName(85, "14.png");
            this.largeImageCollection.Images.SetKeyName(86, "15.png");
            this.largeImageCollection.Images.SetKeyName(87, "16.png");
            this.largeImageCollection.Images.SetKeyName(88, "17.png");
            this.largeImageCollection.Images.SetKeyName(89, "18.png");
            this.largeImageCollection.Images.SetKeyName(90, "19.png");
            this.largeImageCollection.Images.SetKeyName(91, "20.png");
            this.largeImageCollection.Images.SetKeyName(92, "21.png");
            this.largeImageCollection.Images.SetKeyName(93, "22.png");
            this.largeImageCollection.Images.SetKeyName(94, "23.png");
            this.largeImageCollection.Images.SetKeyName(95, "24.png");
            this.largeImageCollection.Images.SetKeyName(96, "25.png");
            this.largeImageCollection.Images.SetKeyName(97, "26.png");
            this.largeImageCollection.Images.SetKeyName(98, "27.png");
            this.largeImageCollection.Images.SetKeyName(99, "28.png");
            this.largeImageCollection.Images.SetKeyName(100, "29.png");
            this.largeImageCollection.Images.SetKeyName(101, "30.png");
            this.largeImageCollection.Images.SetKeyName(102, "31.png");
            this.largeImageCollection.Images.SetKeyName(103, "32.png");
            this.largeImageCollection.Images.SetKeyName(104, "Action_Save_32x32.png");
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.documentContainerHost);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(236, 0);
            this.groupControl2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(744, 590);
            this.groupControl2.TabIndex = 6;
            // 
            // timerSyncTime
            // 
            //this.timerSyncTime.Interval = 1000;
            //this.timerSyncTime.Tick += new System.EventHandler(this.timerSyncTime_Tick);
            // 
            // alertControl1
            // 
            //this.alertControl1.AppearanceText.ForeColor = System.Drawing.Color.Red;
            //this.alertControl1.AppearanceText.Options.UseForeColor = true;
            //this.alertControl1.AutoFormDelay = 10000;
            //alertButton1.Hint = "已读";
            //alertButton1.ImageIndex = 6;
            //alertButton1.Name = "btnSave";
            //this.alertControl1.Buttons.Add(alertButton1);
            //this.alertControl1.Images = this.smallimageList;
            //this.alertControl1.ButtonClick += new DevExpress.XtraBars.Alerter.AlertButtonClickEventHandler(this.alertControl1_ButtonClick);
            // 
            // timerSaveMessage
            // 
            //this.timerSaveMessage.Interval = 1000;
            //this.timerSaveMessage.Tick += new System.EventHandler(this.timerSaveMessage_Tick);
            // 
            // timerShowMsg
            // 
            //this.timerShowMsg.Interval = 10000;
            //this.timerShowMsg.Tick += new System.EventHandler(this.timerShowMsg_Tick);
            // 
            // timerShowOrders
            // 
            //this.timerShowOrders.Enabled = true;
            //this.timerShowOrders.Interval = 180000;
            //this.timerShowOrders.Tick += new System.EventHandler(this.timerShowOrders_Tick);
            // 
            // CtlUserCureListForDirector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.dockPanel1);
            this.Name = "CtlUserCureListForDirector";
            this.Size = new System.Drawing.Size(980, 590);
            this.Load += new System.EventHandler(this.CtlUserCureList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.largeImageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost documentContainerHost;
        private CtlMedicalDocumentContainer documentContainerHost1;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private System.Windows.Forms.ImageList smallimageList;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup2;
        private System.Windows.Forms.ImageList largeImageList;
        private System.Windows.Forms.FlowLayoutPanel pnlPatientTreat;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.Utils.ImageCollection largeImageCollection;
        //private System.Windows.Forms.Timer timerSyncTime;
        //private DevExpress.XtraBars.Alerter.AlertControl alertControl1;
        //private System.Windows.Forms.Timer timerSaveMessage;
        //private System.Windows.Forms.Timer timerShowMsg;
        //private System.Windows.Forms.Timer timerShowOrders;
    }
}
