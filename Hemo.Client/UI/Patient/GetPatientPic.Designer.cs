namespace Hemo.Client.UI.Patient
{
    partial class GetPatientPic
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
            this.tabPicture = new DevExpress.XtraTab.XtraTabControl();
            this.tabPageMain = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnPreview = new DevExpress.XtraEditors.SimpleButton();
            this.btnLoadPicture = new DevExpress.XtraEditors.SimpleButton();
            this.btnClearPicture = new DevExpress.XtraEditors.SimpleButton();
            this.picMain = new DevExpress.XtraEditors.PictureEdit();
            this.tabPageVideo = new DevExpress.XtraTab.XtraTabPage();
            this.picVideo = new DevExpress.XtraEditors.PictureEdit();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.tabPicture)).BeginInit();
            this.tabPicture.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMain.Properties)).BeginInit();
            this.tabPageVideo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picVideo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPicture
            // 
            this.tabPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPicture.Location = new System.Drawing.Point(0, 0);
            this.tabPicture.Name = "tabPicture";
            this.tabPicture.SelectedTabPage = this.tabPageMain;
            this.tabPicture.Size = new System.Drawing.Size(603, 364);
            this.tabPicture.TabIndex = 31;
            this.tabPicture.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageMain,
            this.tabPageVideo});
            this.tabPicture.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabPicture_SelectedPageChanged);
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.panelControl1);
            this.tabPageMain.Controls.Add(this.picMain);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Size = new System.Drawing.Size(596, 334);
            this.tabPageMain.Text = "读取";
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnPreview);
            this.panelControl1.Controls.Add(this.btnLoadPicture);
            this.panelControl1.Controls.Add(this.btnClearPicture);
            this.panelControl1.Location = new System.Drawing.Point(138, 299);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(309, 32);
            this.panelControl1.TabIndex = 27;
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(193, 4);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(87, 25);
            this.btnPreview.TabIndex = 4;
            this.btnPreview.Text = "预览图片";
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnLoadPicture
            // 
            this.btnLoadPicture.Location = new System.Drawing.Point(6, 4);
            this.btnLoadPicture.Name = "btnLoadPicture";
            this.btnLoadPicture.Size = new System.Drawing.Size(87, 25);
            this.btnLoadPicture.TabIndex = 4;
            this.btnLoadPicture.Text = "载入图片";
            this.btnLoadPicture.Click += new System.EventHandler(this.btnLoadPicture_Click);
            // 
            // btnClearPicture
            // 
            this.btnClearPicture.Location = new System.Drawing.Point(100, 4);
            this.btnClearPicture.Name = "btnClearPicture";
            this.btnClearPicture.Size = new System.Drawing.Size(87, 25);
            this.btnClearPicture.TabIndex = 3;
            this.btnClearPicture.Text = "清空";
            this.btnClearPicture.Click += new System.EventHandler(this.btnClearPicture_Click);
            // 
            // picMain
            // 
            this.picMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.picMain.Location = new System.Drawing.Point(15, 11);
            this.picMain.Name = "picMain";
            this.picMain.Properties.ShowMenu = false;
            this.picMain.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.picMain.Size = new System.Drawing.Size(565, 283);
            this.picMain.TabIndex = 26;
            this.picMain.TabStop = true;
            // 
            // tabPageVideo
            // 
            this.tabPageVideo.Controls.Add(this.picVideo);
            this.tabPageVideo.Name = "tabPageVideo";
            this.tabPageVideo.Size = new System.Drawing.Size(596, 334);
            this.tabPageVideo.Text = "拍照";
            // 
            // picVideo
            // 
            this.picVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.picVideo.Location = new System.Drawing.Point(68, 0);
            this.picVideo.Name = "picVideo";
            this.picVideo.Properties.ShowMenu = false;
            this.picVideo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.picVideo.Size = new System.Drawing.Size(432, 331);
            this.picVideo.TabIndex = 29;
            this.picVideo.TabStop = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "All files (*.*)|*.*|BMP (*.bmp)|JPG (*.jpg;*.jpeg)|GIF (*.gif)|PNG (*.png)";
            this.openFileDialog.RestoreDirectory = true;
            // 
            // GetPatientPic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabPicture);
            this.Name = "GetPatientPic";
            this.Size = new System.Drawing.Size(603, 364);
            ((System.ComponentModel.ISupportInitialize)(this.tabPicture)).EndInit();
            this.tabPicture.ResumeLayout(false);
            this.tabPageMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMain.Properties)).EndInit();
            this.tabPageVideo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picVideo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tabPicture;
        private DevExpress.XtraTab.XtraTabPage tabPageMain;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnPreview;
        private DevExpress.XtraEditors.SimpleButton btnLoadPicture;
        private DevExpress.XtraEditors.SimpleButton btnClearPicture;
        private DevExpress.XtraEditors.PictureEdit picMain;
        private DevExpress.XtraTab.XtraTabPage tabPageVideo;
        private DevExpress.XtraEditors.PictureEdit picVideo;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}
