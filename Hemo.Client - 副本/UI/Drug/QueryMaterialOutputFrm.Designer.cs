namespace Hemo.Client.UI.Drug {
    partial class QueryMaterialOutputFrm {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.ctlQueryMaterialOutput1 = new Hemo.Client.UI.Drug.CtlQueryMaterialOutput();
            this.SuspendLayout();
            // 
            // ctlQueryMaterialOutput1
            // 
            this.ctlQueryMaterialOutput1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlQueryMaterialOutput1.Location = new System.Drawing.Point(0, 0);
            this.ctlQueryMaterialOutput1.Name = "ctlQueryMaterialOutput1";
            this.ctlQueryMaterialOutput1.Size = new System.Drawing.Size(1022, 463);
            this.ctlQueryMaterialOutput1.TabIndex = 0;
            // 
            // QueryMaterialOutputFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 463);
            this.Controls.Add(this.ctlQueryMaterialOutput1);
            this.Name = "QueryMaterialOutputFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "耗材出库";
            this.ResumeLayout(false);

        }

        #endregion

        private CtlQueryMaterialOutput ctlQueryMaterialOutput1;

    }
}
