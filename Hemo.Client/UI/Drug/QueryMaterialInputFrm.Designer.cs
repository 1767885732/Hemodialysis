namespace Hemo.Client.UI.Drug {
    partial class QueryMaterialInputFrm {
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
            this.ctlQueryMaterialInput1 = new Hemo.Client.UI.Drug.CtlQueryMaterialInput();
            this.SuspendLayout();
            // 
            // ctlQueryMaterialInput1
            // 
            this.ctlQueryMaterialInput1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlQueryMaterialInput1.Location = new System.Drawing.Point(0, 0);
            this.ctlQueryMaterialInput1.Name = "ctlQueryMaterialInput1";
            this.ctlQueryMaterialInput1.Size = new System.Drawing.Size(1056, 498);
            this.ctlQueryMaterialInput1.TabIndex = 0;
            // 
            // QueryMaterialInputFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 498);
            this.Controls.Add(this.ctlQueryMaterialInput1);
            this.Name = "QueryMaterialInputFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "耗材入库";
            this.ResumeLayout(false);

        }

        #endregion

        private CtlQueryMaterialInput ctlQueryMaterialInput1;

    }
}
