namespace Hemo.Client.UI.Machine
{
    partial class ctlMachineUI
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnChose = new DevExpress.XtraEditors.SimpleButton();
            this.txtDocName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl87 = new DevExpress.XtraEditors.LabelControl();
            this.txtFileName = new DevExpress.XtraEditors.TextEdit();
            this.pictureBox1 = new DevExpress.XtraEditors.PictureEdit();
            this.dxSimpleButton1 = new Hemo.Client.Controls.DXSimpleButton();
            this.btnClose = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "图片文件|*.bmp;*.jpg;*.jpeg;*.png";
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.dxSimpleButton1);
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.btnChose);
            this.panelControl1.Controls.Add(this.txtDocName);
            this.panelControl1.Controls.Add(this.labelControl87);
            this.panelControl1.Controls.Add(this.txtFileName);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(752, 38);
            this.panelControl1.TabIndex = 318;
            // 
            // btnChose
            // 
            this.btnChose.Location = new System.Drawing.Point(5, 5);
            this.btnChose.Name = "btnChose";
            this.btnChose.Size = new System.Drawing.Size(53, 23);
            this.btnChose.TabIndex = 321;
            this.btnChose.Text = "文件(&C)";
            this.btnChose.Click += new System.EventHandler(this.btnChose_Click);
            // 
            // txtDocName
            // 
            this.txtDocName.EditValue = "";
            this.txtDocName.Location = new System.Drawing.Point(418, 8);
            this.txtDocName.Name = "txtDocName";
            this.txtDocName.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtDocName.Properties.Appearance.Options.UseForeColor = true;
            this.txtDocName.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtDocName.Size = new System.Drawing.Size(163, 20);
            this.txtDocName.TabIndex = 320;
            // 
            // labelControl87
            // 
            this.labelControl87.Location = new System.Drawing.Point(343, 10);
            this.labelControl87.Name = "labelControl87";
            this.labelControl87.Size = new System.Drawing.Size(60, 14);
            this.labelControl87.TabIndex = 319;
            this.labelControl87.Text = "文件名称：";
            // 
            // txtFileName
            // 
            this.txtFileName.EditValue = "";
            this.txtFileName.Location = new System.Drawing.Point(64, 7);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtFileName.Properties.Appearance.Options.UseForeColor = true;
            this.txtFileName.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtFileName.Size = new System.Drawing.Size(273, 20);
            this.txtFileName.TabIndex = 318;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 38);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureBox1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.pictureBox1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureBox1.Size = new System.Drawing.Size(752, 389);
            this.pictureBox1.TabIndex = 319;
            // 
            // dxSimpleButton1
            // 
            this.dxSimpleButton1.ImageIndex = 9;
            this.dxSimpleButton1.Location = new System.Drawing.Point(584, 7);
            this.dxSimpleButton1.Name = "dxSimpleButton1";
            this.dxSimpleButton1.Size = new System.Drawing.Size(75, 23);
            this.dxSimpleButton1.TabIndex = 322;
            this.dxSimpleButton1.Text = "上传(&U)";
            this.dxSimpleButton1.Click += new System.EventHandler(this.dxSimpleButton1_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnClose.ImageIndex = 3;
            this.btnClose.Location = new System.Drawing.Point(665, 7);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 323;
            this.btnClose.Text = "关闭(&D)";
            // 
            // ctlMachineUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panelControl1);
            this.Name = "ctlMachineUI";
            this.Size = new System.Drawing.Size(752, 427);
            this.Load += new System.EventHandler(this.ctlMachineUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Controls.DXSimpleButton dxSimpleButton1;
        private Controls.DXSimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnChose;
        private DevExpress.XtraEditors.TextEdit txtDocName;
        private DevExpress.XtraEditors.LabelControl labelControl87;
        private DevExpress.XtraEditors.TextEdit txtFileName;
        private DevExpress.XtraEditors.PictureEdit pictureBox1;
    }
}
