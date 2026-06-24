namespace Hemo.Client.UI.PatientFixUI
{
    partial class PatientDocImageUI
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("患者电子扫描件");
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtFileName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl87 = new DevExpress.XtraEditors.LabelControl();
            this.txtDocName = new DevExpress.XtraEditors.TextEdit();
            this.tlDocments = new System.Windows.Forms.TreeView();
            this.pictureBox1 = new DevExpress.XtraEditors.PictureEdit();
            this.btnChose = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new Hemo.Client.Controls.DXSimpleButton();
            this.dxSimpleButton1 = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "图片文件|*.bmp;*.jpg;*.jpeg;*.gif;*.png";
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // txtFileName
            // 
            this.txtFileName.EditValue = "";
            this.txtFileName.Location = new System.Drawing.Point(71, 14);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtFileName.Properties.Appearance.Options.UseForeColor = true;
            this.txtFileName.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtFileName.Size = new System.Drawing.Size(382, 20);
            this.txtFileName.TabIndex = 304;
            // 
            // labelControl87
            // 
            this.labelControl87.Location = new System.Drawing.Point(460, 17);
            this.labelControl87.Name = "labelControl87";
            this.labelControl87.Size = new System.Drawing.Size(60, 14);
            this.labelControl87.TabIndex = 305;
            this.labelControl87.Text = "文件名称：";
            // 
            // txtDocName
            // 
            this.txtDocName.EditValue = "";
            this.txtDocName.Location = new System.Drawing.Point(538, 14);
            this.txtDocName.Name = "txtDocName";
            this.txtDocName.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtDocName.Properties.Appearance.Options.UseForeColor = true;
            this.txtDocName.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtDocName.Size = new System.Drawing.Size(211, 20);
            this.txtDocName.TabIndex = 306;
            // 
            // tlDocments
            // 
            this.tlDocments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tlDocments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlDocments.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tlDocments.Location = new System.Drawing.Point(12, 41);
            this.tlDocments.Name = "tlDocments";
            treeNode1.Name = "节点0";
            treeNode1.Text = "患者电子扫描件";
            this.tlDocments.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.tlDocments.ShowRootLines = false;
            this.tlDocments.Size = new System.Drawing.Size(275, 436);
            this.tlDocments.TabIndex = 308;
            this.tlDocments.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tlDocments_MouseDoubleClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(293, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureBox1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.pictureBox1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureBox1.Size = new System.Drawing.Size(625, 436);
            this.pictureBox1.TabIndex = 309;
            // 
            // btnChose
            // 
            this.btnChose.Location = new System.Drawing.Point(12, 12);
            this.btnChose.Name = "btnChose";
            this.btnChose.Size = new System.Drawing.Size(53, 23);
            this.btnChose.TabIndex = 310;
            this.btnChose.Text = "文件(&C)";
            this.btnChose.Click += new System.EventHandler(this.dxSimpleButton1_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnClose.ImageIndex = 3;
            this.btnClose.Location = new System.Drawing.Point(836, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 311;
            this.btnClose.Text = "关闭(&D)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dxSimpleButton1
            // 
            this.dxSimpleButton1.ImageIndex = 9;
            this.dxSimpleButton1.Location = new System.Drawing.Point(755, 11);
            this.dxSimpleButton1.Name = "dxSimpleButton1";
            this.dxSimpleButton1.Size = new System.Drawing.Size(75, 23);
            this.dxSimpleButton1.TabIndex = 311;
            this.dxSimpleButton1.Text = "上传(&U)";
            this.dxSimpleButton1.Click += new System.EventHandler(this.dxSimpleButton2_Click);
            // 
            // PatientDocImageUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dxSimpleButton1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnChose);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tlDocments);
            this.Controls.Add(this.txtDocName);
            this.Controls.Add(this.labelControl87);
            this.Controls.Add(this.txtFileName);
            this.Name = "PatientDocImageUI";
            this.Size = new System.Drawing.Size(921, 489);
            this.Load += new System.EventHandler(this.PatientDocImageUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevExpress.XtraEditors.TextEdit txtFileName;
        private DevExpress.XtraEditors.LabelControl labelControl87;
        private DevExpress.XtraEditors.TextEdit txtDocName;
        private System.Windows.Forms.TreeView tlDocments;
        private DevExpress.XtraEditors.PictureEdit pictureBox1;
        private DevExpress.XtraEditors.SimpleButton btnChose;
        private Controls.DXSimpleButton btnClose;
        private Controls.DXSimpleButton dxSimpleButton1;
    }
}