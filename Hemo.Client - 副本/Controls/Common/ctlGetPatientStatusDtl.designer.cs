namespace Hemo.Client.Controls.Common
{
    partial class ctlGetPatientStatusDtl
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
            this.pic1 = new DevExpress.XtraEditors.PictureEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pic1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pic1
            // 
            this.pic1.Location = new System.Drawing.Point(0, 6);
            this.pic1.Name = "pic1";
            this.pic1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pic1.Properties.Appearance.Options.UseBackColor = true;
            this.pic1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pic1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pic1.Size = new System.Drawing.Size(28, 28);
            this.pic1.TabIndex = 1;
            this.pic1.Click += new System.EventHandler(this.panelControl1_Click);
            this.pic1.DoubleClick += new System.EventHandler(this.panelControl1_DoubleClick);
            this.pic1.MouseHover += new System.EventHandler(this.panelControl1_MouseHover);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(29, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.panelControl1_Click);
            this.label1.DoubleClick += new System.EventHandler(this.panelControl1_DoubleClick);
            this.label1.MouseHover += new System.EventHandler(this.panelControl1_MouseHover);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.pic1);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(117, 37);
            this.panelControl1.TabIndex = 6;
            this.panelControl1.Click += new System.EventHandler(this.panelControl1_Click);
            this.panelControl1.DoubleClick += new System.EventHandler(this.panelControl1_DoubleClick);
            this.panelControl1.MouseHover += new System.EventHandler(this.panelControl1_MouseHover);
            // 
            // ctlGetPatientStatusDtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "ctlGetPatientStatusDtl";
            this.Size = new System.Drawing.Size(117, 37);
            this.Load += new System.EventHandler(this.ctlGetPatientStatusDtl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit pic1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}
