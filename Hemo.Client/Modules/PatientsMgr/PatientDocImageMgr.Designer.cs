namespace Hemo.Client.Modules
{
    partial class PatientDocImageMgr
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
            this.panelMain = new DevExpress.XtraEditors.PanelControl();
            this.LbTitle = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelMain)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelMain.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.panelMain.Appearance.Options.UseBackColor = true;
            this.panelMain.Appearance.Options.UseBorderColor = true;
            this.panelMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 44);
            this.panelMain.Margin = new System.Windows.Forms.Padding(0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.panelMain.Size = new System.Drawing.Size(989, 503);
            this.panelMain.TabIndex = 2;
            // 
            // LbTitle
            // 
            this.LbTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.LbTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.LbTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.LbTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.LbTitle.Location = new System.Drawing.Point(0, 0);
            this.LbTitle.Name = "LbTitle";
            this.LbTitle.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.LbTitle.Size = new System.Drawing.Size(989, 44);
            this.LbTitle.TabIndex = 3;
            this.LbTitle.Text = "labelControl1";
            // 
            // PatientDocImageMgr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.LbTitle);
            this.Name = "PatientDocImageMgr";
            this.Size = new System.Drawing.Size(989, 547);
            this.Load += new System.EventHandler(this.PatientDocImageMgr_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelMain;
        private DevExpress.XtraEditors.LabelControl LbTitle;


    }
}
