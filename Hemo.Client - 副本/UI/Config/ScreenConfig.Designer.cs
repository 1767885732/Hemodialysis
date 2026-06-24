namespace Hemo.Client.UI.Config
{
    partial class ScreenConfig
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
            this.lab1 = new DevExpress.XtraEditors.LabelControl();
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.btnClose = new Hemo.Client.Controls.DXSimpleButton();
            this.txtFileDictionary = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.txtDirFolder = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileDictionary.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDirFolder.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lab1
            // 
            this.lab1.Location = new System.Drawing.Point(14, 43);
            this.lab1.Name = "lab1";
            this.lab1.Size = new System.Drawing.Size(48, 14);
            this.lab1.TabIndex = 300;
            this.lab1.Text = "选择文件";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(77, 135);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 27);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "保存到大屏";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImageIndex = 3;
            this.btnClose.Location = new System.Drawing.Point(201, 135);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 27);
            this.btnClose.TabIndex = 307;
            this.btnClose.Text = "取消";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtFileDictionary
            // 
            this.txtFileDictionary.Location = new System.Drawing.Point(68, 40);
            this.txtFileDictionary.Name = "txtFileDictionary";
            this.txtFileDictionary.Size = new System.Drawing.Size(234, 21);
            this.txtFileDictionary.TabIndex = 308;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(306, 38);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(53, 23);
            this.simpleButton1.TabIndex = 309;
            this.simpleButton1.Text = "选择";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // txtDirFolder
            // 
            this.txtDirFolder.EditValue = "\\\\\\\\WENJUND-PC\\hehehe\\\\aa.ppt";
            this.txtDirFolder.Location = new System.Drawing.Point(68, 79);
            this.txtDirFolder.Name = "txtDirFolder";
            this.txtDirFolder.Size = new System.Drawing.Size(234, 21);
            this.txtDirFolder.TabIndex = 310;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 82);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 300;
            this.labelControl1.Text = "目标路径";
            // 
            // ScreenConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 174);
            this.Controls.Add(this.txtDirFolder);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.txtFileDictionary);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lab1);
            this.MinimumSize = new System.Drawing.Size(294, 212);
            this.Name = "ScreenConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "上传";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileDictionary.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDirFolder.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lab1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider;
        private Hemo.Client.Controls.DXSimpleButton btnSave;
        private Hemo.Client.Controls.DXSimpleButton btnClose;
        private DevExpress.XtraEditors.TextEdit txtFileDictionary;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.TextEdit txtDirFolder;
        private DevExpress.XtraEditors.LabelControl labelControl1;

    }
}