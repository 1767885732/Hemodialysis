namespace Hemo.Client.UI.Hemodialysis {
    partial class EditCureList {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.panControl = new DevExpress.XtraEditors.PanelControl();
            this.documentContainerHost = new System.Windows.Forms.Integration.ElementHost();
            this.ctlMedicalDocumentContainer1 = new Hemo.Client.Controls.CtlMedicalDocumentContainer();
            ((System.ComponentModel.ISupportInitialize)(this.panControl)).BeginInit();
            this.panControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // panControl
            // 
            this.panControl.Controls.Add(this.documentContainerHost);
            this.panControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panControl.Location = new System.Drawing.Point(0, 0);
            this.panControl.Name = "panControl";
            this.panControl.Size = new System.Drawing.Size(981, 541);
            this.panControl.TabIndex = 1;
            // 
            // documentContainerHost
            // 
            this.documentContainerHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentContainerHost.Location = new System.Drawing.Point(2, 2);
            this.documentContainerHost.Name = "documentContainerHost";
            this.documentContainerHost.Size = new System.Drawing.Size(977, 537);
            this.documentContainerHost.TabIndex = 3;
            this.documentContainerHost.Text = "elementHost1";
            this.documentContainerHost.Child = this.ctlMedicalDocumentContainer1;
            // 
            // EditCureList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 541);
            this.Controls.Add(this.panControl);
            this.Name = "EditCureList";
            this.Text = "透析记录单列表";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.panControl)).EndInit();
            this.panControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panControl;
        private System.Windows.Forms.Integration.ElementHost documentContainerHost;
        private Controls.CtlMedicalDocumentContainer ctlMedicalDocumentContainer1;
    }
}