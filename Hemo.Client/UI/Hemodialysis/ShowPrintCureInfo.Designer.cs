namespace Hemo.Client.UI.Hemodialysis {
    partial class ShowPrintCureInfo {
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
            this.documentContainerHost = new System.Windows.Forms.Integration.ElementHost();
            this.SuspendLayout();
            // 
            // documentContainerHost
            // 
            this.documentContainerHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentContainerHost.Location = new System.Drawing.Point(0, 0);
            this.documentContainerHost.Name = "documentContainerHost";
            this.documentContainerHost.Size = new System.Drawing.Size(874, 741);
            this.documentContainerHost.TabIndex = 3;
            this.documentContainerHost.Text = "elementHost1";
            this.documentContainerHost.Child = null;
            // 
            // ShowPrintCureInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 741);
            this.Controls.Add(this.documentContainerHost);
            this.Name = "ShowPrintCureInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost documentContainerHost;
    }
}