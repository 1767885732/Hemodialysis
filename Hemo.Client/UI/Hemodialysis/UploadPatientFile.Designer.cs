namespace Hemo.Client.UI.Hemodialysis
{
    partial class UploadPatientFile
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
            this.ctlUserLongInfo = new Hemo.Client.Controls.CtlUserLongInfo();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnUpload = new Hemo.Client.Controls.DXSimpleButton();
            this.btnLoadFile = new Hemo.Client.Controls.DXSimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtFile = new DevExpress.XtraEditors.TextEdit();
            this.lupBook = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.picView = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupBook.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picView.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlUserLongInfo
            // 
            this.ctlUserLongInfo.DIAGNOSE = "";
            this.ctlUserLongInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlUserLongInfo.FormContainer = null;
            this.ctlUserLongInfo.HEMODIALYSIS_ID = "";
            this.ctlUserLongInfo.Location = new System.Drawing.Point(0, 0);
            this.ctlUserLongInfo.Name = "ctlUserLongInfo";
            this.ctlUserLongInfo.PanBorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.ctlUserLongInfo.PanelWidth = 894;
            this.ctlUserLongInfo.PatientType = "";
            this.ctlUserLongInfo.PatientTypeEnabled = true;
            this.ctlUserLongInfo.Size = new System.Drawing.Size(894, 39);
            this.ctlUserLongInfo.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.btnUpload);
            this.panelControl1.Controls.Add(this.btnLoadFile);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.txtFile);
            this.panelControl1.Controls.Add(this.lupBook);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 512);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(894, 60);
            this.panelControl1.TabIndex = 1;
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl4.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl4.Location = new System.Drawing.Point(12, 35);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(238, 19);
            this.labelControl4.TabIndex = 409;
            this.labelControl4.Text = "注意：上传的患者资料需和同意书符合";
            // 
            // btnUpload
            // 
            this.btnUpload.ImageIndex = 9;
            this.btnUpload.Location = new System.Drawing.Point(720, 5);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(90, 23);
            this.btnUpload.TabIndex = 408;
            this.btnUpload.Text = "上传";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.ImageIndex = 11;
            this.btnLoadFile.Location = new System.Drawing.Point(654, 5);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(50, 23);
            this.btnLoadFile.TabIndex = 407;
            this.btnLoadFile.Text = "选择";
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl2.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl2.Location = new System.Drawing.Point(344, 8);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 17);
            this.labelControl2.TabIndex = 406;
            this.labelControl2.Text = "患者资料";
            // 
            // txtFile
            // 
            this.txtFile.EnterMoveNextControl = true;
            this.txtFile.Location = new System.Drawing.Point(398, 5);
            this.txtFile.Name = "txtFile";
            this.txtFile.Properties.AutoHeight = false;
            this.txtFile.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtFile.Properties.MaxLength = 100;
            this.txtFile.Size = new System.Drawing.Size(250, 23);
            this.txtFile.TabIndex = 405;
            // 
            // lupBook
            // 
            this.lupBook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lupBook.EnterMoveNextControl = true;
            this.lupBook.Location = new System.Drawing.Point(78, 5);
            this.lupBook.Name = "lupBook";
            this.lupBook.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lupBook.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lupBook.Properties.Appearance.Options.UseFont = true;
            this.lupBook.Properties.Appearance.Options.UseForeColor = true;
            this.lupBook.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupBook.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ITEM_VALUE", 40, "编号"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ITEM_NAME", 240, "名称")});
            this.lupBook.Properties.DisplayMember = "ITEM_NAME";
            this.lupBook.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.lupBook.Properties.NullText = "";
            this.lupBook.Properties.PopupWidth = 280;
            this.lupBook.Properties.ValueMember = "ITEM_VALUE";
            this.lupBook.Size = new System.Drawing.Size(250, 24);
            this.lupBook.TabIndex = 402;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl1.Location = new System.Drawing.Point(12, 8);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 17);
            this.labelControl1.TabIndex = 403;
            this.labelControl1.Text = "同意书类型";
            // 
            // picView
            // 
            this.picView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picView.Location = new System.Drawing.Point(0, 39);
            this.picView.Name = "picView";
            this.picView.Properties.ShowMenu = false;
            this.picView.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picView.Size = new System.Drawing.Size(894, 473);
            this.picView.TabIndex = 410;
            this.picView.TabStop = true;
            // 
            // UploadPatientFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 572);
            this.Controls.Add(this.picView);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ctlUserLongInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UploadPatientFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "上传患者资料";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UploadPatientFile_FormClosed);
            this.Load += new System.EventHandler(this.UploadPatientFile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupBook.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picView.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CtlUserLongInfo ctlUserLongInfo;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PictureEdit picView;
        private DevExpress.XtraEditors.LookUpEdit lupBook;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Controls.DXSimpleButton btnLoadFile;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtFile;
        private Controls.DXSimpleButton btnUpload;
        private DevExpress.XtraEditors.LabelControl labelControl4;

    }
}