namespace Hemo.Client.UI.Patient
{
    partial class PatientMaterialDetail
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
            this.patientMaterialDetailUI1 = new Hemo.Client.UI.Patient.PatientMaterialDetailUI();
            this.SuspendLayout();
            // 
            // patientMaterialDetailUI1
            // 
            this.patientMaterialDetailUI1.CurrentHemoId = "";
            this.patientMaterialDetailUI1.CurrentRecordRow = null;
            this.patientMaterialDetailUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientMaterialDetailUI1.HasDirty = false;
            this.patientMaterialDetailUI1.Location = new System.Drawing.Point(0, 0);
            this.patientMaterialDetailUI1.Name = "patientMaterialDetailUI1";
            this.patientMaterialDetailUI1.PackageCode = "";
            this.patientMaterialDetailUI1.Size = new System.Drawing.Size(931, 617);
            this.patientMaterialDetailUI1.TabIndex = 0;
            // 
            // PatientMaterialDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 617);
            this.Controls.Add(this.patientMaterialDetailUI1);
            this.Name = "PatientMaterialDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "患者出库明细";
            this.Load += new System.EventHandler(this.PatientMaterialDetail_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private PatientMaterialDetailUI patientMaterialDetailUI1;


    }
}