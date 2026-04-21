namespace Hemo.Client.UI.Drug {
    partial class QueryMaterialCheckFrm {
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
            this.ctlQueryMaterialCheck1 = new Hemo.Client.UI.Drug.CtlQueryMaterialCheck();
            this.SuspendLayout();
            // 
            // ctlQueryMaterialCheck1
            // 
            this.ctlQueryMaterialCheck1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlQueryMaterialCheck1.Location = new System.Drawing.Point(0, 0);
            this.ctlQueryMaterialCheck1.Name = "ctlQueryMaterialCheck1";
            this.ctlQueryMaterialCheck1.Size = new System.Drawing.Size(981, 498);
            this.ctlQueryMaterialCheck1.TabIndex = 0;
            // 
            // QueryMaterialCheckFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 498);
            this.Controls.Add(this.ctlQueryMaterialCheck1);
            this.Name = "QueryMaterialCheckFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "耗材盘点";
            this.ResumeLayout(false);

        }

        #endregion

        private CtlQueryMaterialCheck ctlQueryMaterialCheck1;

    }
}
