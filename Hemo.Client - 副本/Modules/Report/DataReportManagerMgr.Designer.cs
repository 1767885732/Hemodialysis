namespace Hemo.Client.Modules
{
    partial class DataReportManagerMgr
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
            DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataReportManagerMgr));
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barBtn_User = new DevExpress.XtraBars.BarButtonItem();
            this.barBtn_Date = new DevExpress.XtraBars.BarButtonItem();
            this.barBtn_IP = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barBtn_ShiftRole = new DevExpress.XtraBars.BarButtonItem();
            this.btnClose = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem11 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem12 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem13 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem14 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem15 = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.largeImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup6 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.clientPanel = new DevExpress.XtraEditors.PanelControl();
            this.viewHostTabControl = new DevExpress.XtraTab.XtraTabControl();
            ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.largeImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).BeginInit();
            this.clientPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewHostTabControl)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonPageGroup1
            // 
            ribbonPageGroup1.AllowMinimize = false;
            ribbonPageGroup1.AllowTextClipping = false;
            ribbonPageGroup1.ItemLinks.Add(this.barButtonItem1);
            ribbonPageGroup1.ItemLinks.Add(this.barButtonItem5);
            ribbonPageGroup1.ItemLinks.Add(this.barButtonItem2);
            ribbonPageGroup1.ItemLinks.Add(this.barButtonItem3);
            ribbonPageGroup1.ItemLinks.Add(this.barButtonItem4);
            ribbonPageGroup1.Name = "ribbonPageGroup1";
            ribbonPageGroup1.ShowCaptionButton = false;
            ribbonPageGroup1.Text = "全国数据上报管理";
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
            // ribbon
            // 
            this.ribbon.AllowMdiChildButtons = false;
            this.ribbon.AllowMinimizeRibbon = false;
            this.ribbon.AllowTrimPageText = false;
            this.ribbon.ApplicationButtonText = null;
            this.ribbon.AutoSizeItems = true;
            this.ribbon.ButtonGroupsVertAlign = DevExpress.Utils.VertAlignment.Center;
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
            this.barButtonItem7,
            this.barButtonItem8,
            this.barButtonItem11,
            this.barButtonItem12,
            this.barButtonItem13,
            this.barButtonItem14,
            this.barButtonItem15,
            this.barStaticItem1});
            this.ribbon.ItemsVertAlign = DevExpress.Utils.VertAlignment.Center;
            this.ribbon.LargeImages = this.largeImageCollection;
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 2;
            this.ribbon.Name = "ribbon";
            this.ribbon.PageCategoryAlignment = DevExpress.XtraBars.Ribbon.RibbonPageCategoryAlignment.Left;
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.ribbon.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowFullScreenButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbon.ShowQatLocationSelector = false;
            this.ribbon.ShowToolbarCustomizeItem = false;
            this.ribbon.Size = new System.Drawing.Size(1192, 98);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            this.ribbon.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
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
            // barButtonItem7
            // 
            this.barButtonItem7.Caption = "患者信息";
            this.barButtonItem7.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonItem7.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem7.Glyph")));
            this.barButtonItem7.Id = 31;
            this.barButtonItem7.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem7.LargeGlyph")));
            this.barButtonItem7.Name = "barButtonItem7";
            this.barButtonItem7.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem7_ItemClick);
            // 
            // barButtonItem8
            // 
            this.barButtonItem8.Caption = "患者治疗";
            this.barButtonItem8.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonItem8.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem8.Glyph")));
            this.barButtonItem8.Id = 32;
            this.barButtonItem8.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem8.LargeGlyph")));
            this.barButtonItem8.Name = "barButtonItem8";
            this.barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barButtonItem8.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem8_ItemClick);
            // 
            // barButtonItem11
            // 
            this.barButtonItem11.Caption = "科室信息";
            this.barButtonItem11.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonItem11.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem11.Glyph")));
            this.barButtonItem11.Id = 38;
            this.barButtonItem11.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem11.LargeGlyph")));
            this.barButtonItem11.Name = "barButtonItem11";
            // 
            // barButtonItem12
            // 
            this.barButtonItem12.Caption = "检验信息";
            this.barButtonItem12.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonItem12.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem12.Glyph")));
            this.barButtonItem12.Id = 39;
            this.barButtonItem12.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem12.LargeGlyph")));
            this.barButtonItem12.Name = "barButtonItem12";
            // 
            // barButtonItem13
            // 
            this.barButtonItem13.Caption = "院感信息";
            this.barButtonItem13.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonItem13.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem13.Glyph")));
            this.barButtonItem13.Id = 40;
            this.barButtonItem13.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem13.LargeGlyph")));
            this.barButtonItem13.Name = "barButtonItem13";
            // 
            // barButtonItem14
            // 
            this.barButtonItem14.Caption = "设备信息";
            this.barButtonItem14.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonItem14.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem14.Glyph")));
            this.barButtonItem14.Id = 41;
            this.barButtonItem14.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem14.LargeGlyph")));
            this.barButtonItem14.Name = "barButtonItem14";
            // 
            // barButtonItem15
            // 
            this.barButtonItem15.Caption = "治疗信息";
            this.barButtonItem15.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonItem15.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem15.Glyph")));
            this.barButtonItem15.Id = 42;
            this.barButtonItem15.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem15.LargeGlyph")));
            this.barButtonItem15.Name = "barButtonItem15";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barStaticItem1.Id = 1;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
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
            ribbonPageGroup1,
            this.ribbonPageGroup2,
            this.ribbonPageGroup6});
            this.ribbonPage1.Name = "ribbonPage1";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem7);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem8);
            this.ribbonPageGroup2.ItemLinks.Add(this.barStaticItem1);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem11);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem12);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem13);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem14);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem15);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "福建省数据上报管理";
            // 
            // ribbonPageGroup6
            // 
            this.ribbonPageGroup6.AllowMinimize = false;
            this.ribbonPageGroup6.AllowTextClipping = false;
            this.ribbonPageGroup6.ItemLinks.Add(this.barBtn_ShiftRole);
            this.ribbonPageGroup6.ItemLinks.Add(this.btnClose);
            this.ribbonPageGroup6.Name = "ribbonPageGroup6";
            this.ribbonPageGroup6.ShowCaptionButton = false;
            this.ribbonPageGroup6.Text = "系统管理";
            this.ribbonPageGroup6.Visible = false;
            // 
            // clientPanel
            // 
            this.clientPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.clientPanel.Controls.Add(this.viewHostTabControl);
            this.clientPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clientPanel.Location = new System.Drawing.Point(0, 98);
            this.clientPanel.Name = "clientPanel";
            this.clientPanel.Size = new System.Drawing.Size(1192, 351);
            this.clientPanel.TabIndex = 2;
            // 
            // viewHostTabControl
            // 
            this.viewHostTabControl.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InTabControlHeader;
            this.viewHostTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewHostTabControl.HeaderButtons = ((DevExpress.XtraTab.TabButtons)((DevExpress.XtraTab.TabButtons.Close | DevExpress.XtraTab.TabButtons.Default)));
            this.viewHostTabControl.Location = new System.Drawing.Point(0, 0);
            this.viewHostTabControl.Name = "viewHostTabControl";
            this.viewHostTabControl.Size = new System.Drawing.Size(1192, 351);
            this.viewHostTabControl.TabIndex = 11;
            this.viewHostTabControl.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.viewHostTabControl_SelectedPageChanged);
            this.viewHostTabControl.CloseButtonClick += new System.EventHandler(this.viewHostTabControl_CloseButtonClick);
            // 
            // DataReportManagerMgr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.clientPanel);
            this.Controls.Add(this.ribbon);
            this.Name = "DataReportManagerMgr";
            this.Size = new System.Drawing.Size(1192, 449);
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
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
        private DevExpress.XtraBars.BarButtonItem barButtonItem11;
        private DevExpress.XtraBars.BarButtonItem barButtonItem12;
        private DevExpress.XtraBars.BarButtonItem barButtonItem13;
        private DevExpress.XtraBars.BarButtonItem barButtonItem14;
        private DevExpress.XtraBars.BarButtonItem barButtonItem15;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
    }
}