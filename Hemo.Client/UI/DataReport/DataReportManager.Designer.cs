namespace Hemo.Client.UI.DataReport
{
    partial class DataReportManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataReportManager));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barBtn_User = new DevExpress.XtraBars.BarButtonItem();
            this.barBtn_Date = new DevExpress.XtraBars.BarButtonItem();
            this.barBtn_IP = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barBtn_ShiftRole = new DevExpress.XtraBars.BarButtonItem();
            this.btnClose = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.largeImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup6 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.clientPanel = new DevExpress.XtraEditors.PanelControl();
            this.viewHostTabControl = new DevExpress.XtraTab.XtraTabControl();
            this.barBtn_Version = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.largeImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).BeginInit();
            this.clientPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewHostTabControl)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ApplicationButtonText = null;
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.barButtonItem1,
            this.barButtonItem5,
            this.barBtn_User,
            this.barBtn_Date,
            this.barBtn_IP,
            this.barButtonItem6,
            this.barBtn_ShiftRole,
            this.btnClose,
            this.barButtonItem2,
            this.barButtonItem3,
            this.barButtonItem4,
            this.barBtn_Version});
            this.ribbon.LargeImages = this.largeImageCollection;
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 22;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(967, 147);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "患者登记";
            this.barButtonItem1.Id = 1;
            this.barButtonItem1.LargeImageIndex = 37;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "诊断信息";
            this.barButtonItem5.Id = 5;
            this.barButtonItem5.LargeImageIndex = 15;
            this.barButtonItem5.Name = "barButtonItem5";
            this.barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barButtonItem5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem5_ItemClick);
            // 
            // barBtn_User
            // 
            this.barBtn_User.Caption = "用户";
            this.barBtn_User.Id = 6;
            this.barBtn_User.Name = "barBtn_User";
            // 
            // barBtn_Date
            // 
            this.barBtn_Date.Caption = "日期";
            this.barBtn_Date.Id = 7;
            this.barBtn_Date.Name = "barBtn_Date";
            // 
            // barBtn_IP
            // 
            this.barBtn_IP.Caption = "IP";
            this.barBtn_IP.Id = 8;
            this.barBtn_IP.Name = "barBtn_IP";
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "XX公司";
            this.barButtonItem6.Id = 9;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // barBtn_ShiftRole
            // 
            this.barBtn_ShiftRole.Caption = "切换用户";
            this.barBtn_ShiftRole.Id = 16;
            this.barBtn_ShiftRole.LargeImageIndex = 37;
            this.barBtn_ShiftRole.Name = "barBtn_ShiftRole";
            this.barBtn_ShiftRole.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtn_ShiftRole_ItemClick);
            // 
            // btnClose
            // 
            this.btnClose.Caption = "退出系统";
            this.btnClose.Id = 17;
            this.btnClose.LargeImageIndex = 38;
            this.btnClose.Name = "btnClose";
            this.btnClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClose_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "血透信息";
            this.barButtonItem2.Id = 18;
            this.barButtonItem2.LargeImageIndex = 33;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "治疗信息";
            this.barButtonItem3.Id = 19;
            this.barButtonItem3.LargeImageIndex = 34;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "实验室及辅助检查";
            this.barButtonItem4.Id = 20;
            this.barButtonItem4.LargeImageIndex = 36;
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem4_ItemClick);
            // 
            // largeImageCollection
            // 
            this.largeImageCollection.ImageSize = new System.Drawing.Size(32, 32);
            this.largeImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("largeImageCollection.ImageStream")));
            this.largeImageCollection.Images.SetKeyName(0, "ViewsFormView.png");
            this.largeImageCollection.Images.SetKeyName(1, "BookmarkInsert.png");
            this.largeImageCollection.Images.SetKeyName(2, "CreateFormInDesignView.png");
            this.largeImageCollection.Images.SetKeyName(3, "CreateFormSplitForm.png");
            this.largeImageCollection.Images.SetKeyName(4, "ViewsFormView.png");
            this.largeImageCollection.Images.SetKeyName(5, "CreateFormBlankForm.png");
            this.largeImageCollection.Images.SetKeyName(6, "CreateForm.png");
            this.largeImageCollection.Images.SetKeyName(7, "CreateReport.png");
            this.largeImageCollection.Images.SetKeyName(8, "PageNambersInMarginsInsertGallery.png");
            this.largeImageCollection.Images.SetKeyName(9, "WindowNew.png");
            this.largeImageCollection.Images.SetKeyName(10, "WindowsSwitch.png");
            this.largeImageCollection.Images.SetKeyName(11, "tableselect.png");
            this.largeImageCollection.Images.SetKeyName(12, "CustomTableOfContentsGallery.png");
            this.largeImageCollection.Images.SetKeyName(13, "zoom-.png");
            this.largeImageCollection.Images.SetKeyName(14, "zoom+.png");
            this.largeImageCollection.Images.SetKeyName(15, "ControlProperties.png");
            this.largeImageCollection.Images.SetKeyName(16, "DocumentPanelTemplate.png");
            this.largeImageCollection.Images.SetKeyName(17, "Play.png");
            this.largeImageCollection.Images.SetKeyName(18, "Add Green Button.png");
            this.largeImageCollection.Images.SetKeyName(19, "Add.png");
            this.largeImageCollection.Images.SetKeyName(20, "Clear Green Button.png");
            this.largeImageCollection.Images.SetKeyName(21, "Import Document.png");
            this.largeImageCollection.Images.SetKeyName(22, "Pre.png");
            this.largeImageCollection.Images.SetKeyName(23, "GroupOpen.png");
            this.largeImageCollection.Images.SetKeyName(24, "刷新.jpg");
            this.largeImageCollection.Images.SetKeyName(25, "Hein22.jpg");
            this.largeImageCollection.Images.SetKeyName(26, "Hein027.jpg");
            this.largeImageCollection.Images.SetKeyName(27, "Recycle Bin Empty4.ico");
            this.largeImageCollection.Images.SetKeyName(28, "Trash Can 2.ico");
            this.largeImageCollection.Images.SetKeyName(29, "Trash Can 3 Full.ico");
            this.largeImageCollection.Images.SetKeyName(30, "Hein24.jpg");
            this.largeImageCollection.Images.SetKeyName(31, "Hein84.jpg");
            this.largeImageCollection.Images.SetKeyName(32, "config.png");
            this.largeImageCollection.Images.SetKeyName(33, "medical.png");
            this.largeImageCollection.Images.SetKeyName(34, "tubes.png");
            this.largeImageCollection.Images.SetKeyName(35, "water.png");
            this.largeImageCollection.Images.SetKeyName(36, "base.png");
            this.largeImageCollection.Images.SetKeyName(37, "显示所有患者.png");
            this.largeImageCollection.Images.SetKeyName(38, "close_delete_2.png");
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup6});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "全国血液透析患者病历信息上报";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem1);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem5);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem2);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem3);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem4);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "透析患者信息上报管理";
            // 
            // ribbonPageGroup6
            // 
            this.ribbonPageGroup6.ItemLinks.Add(this.barBtn_ShiftRole);
            this.ribbonPageGroup6.ItemLinks.Add(this.btnClose);
            this.ribbonPageGroup6.Name = "ribbonPageGroup6";
            this.ribbonPageGroup6.Text = "系统管理";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.barBtn_User);
            this.ribbonStatusBar.ItemLinks.Add(this.barBtn_Date);
            this.ribbonStatusBar.ItemLinks.Add(this.barBtn_IP);
            this.ribbonStatusBar.ItemLinks.Add(this.barButtonItem6);
            this.ribbonStatusBar.ItemLinks.Add(this.barBtn_Version);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 418);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(967, 31);
            // 
            // clientPanel
            // 
            this.clientPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.clientPanel.Controls.Add(this.viewHostTabControl);
            this.clientPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clientPanel.Location = new System.Drawing.Point(0, 147);
            this.clientPanel.Name = "clientPanel";
            this.clientPanel.Size = new System.Drawing.Size(967, 271);
            this.clientPanel.TabIndex = 2;
            // 
            // viewHostTabControl
            // 
            this.viewHostTabControl.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InTabControlHeader;
            this.viewHostTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewHostTabControl.HeaderButtons = ((DevExpress.XtraTab.TabButtons)((DevExpress.XtraTab.TabButtons.Close | DevExpress.XtraTab.TabButtons.Default)));
            this.viewHostTabControl.Location = new System.Drawing.Point(0, 0);
            this.viewHostTabControl.Name = "viewHostTabControl";
            this.viewHostTabControl.Size = new System.Drawing.Size(967, 271);
            this.viewHostTabControl.TabIndex = 11;
            this.viewHostTabControl.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.viewHostTabControl_SelectedPageChanged);
            this.viewHostTabControl.CloseButtonClick += new System.EventHandler(this.viewHostTabControl_CloseButtonClick);
            // 
            // barBtn_Version
            // 
            this.barBtn_Version.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barBtn_Version.Caption = "barButtonItem7";
            this.barBtn_Version.Id = 21;
            this.barBtn_Version.Name = "barBtn_Version";
            // 
            // DataReportManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 449);
            this.Controls.Add(this.clientPanel);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "DataReportManager";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "血透患者数据上报管理中心";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataReportManager_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.largeImageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).EndInit();
            this.clientPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewHostTabControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraEditors.PanelControl clientPanel;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem barBtn_User;
        private DevExpress.XtraBars.BarButtonItem barBtn_Date;
        private DevExpress.XtraBars.BarButtonItem barBtn_IP;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.Utils.ImageCollection largeImageCollection;
        private DevExpress.XtraTab.XtraTabControl viewHostTabControl;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup6;
        private DevExpress.XtraBars.BarButtonItem barBtn_ShiftRole;
        private DevExpress.XtraBars.BarButtonItem btnClose;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barBtn_Version;
    }
}