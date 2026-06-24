namespace Hemo.Client.UI.Patient
{
    partial class PreviewPictureFrm
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
            this.picPreview = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // picPreview
            // 
            this.picPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPreview.Location = new System.Drawing.Point(12, 11);
            this.picPreview.Name = "picPreview";
            this.picPreview.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.picPreview.Size = new System.Drawing.Size(760, 414);
            this.picPreview.TabIndex = 0;
            this.picPreview.DoubleClick += new System.EventHandler(this.picPreview_DoubleClick);
            // 
            // PreviewPictureFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 479);
            this.Controls.Add(this.picPreview);
            this.Name = "PreviewPictureFrm";
            this.Padding = new System.Windows.Forms.Padding(12, 11, 12, 54);
            this.Text = "图片预览";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PreviewPictureFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit picPreview;


    }
}