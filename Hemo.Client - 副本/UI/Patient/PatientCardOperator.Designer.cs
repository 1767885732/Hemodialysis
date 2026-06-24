namespace Hemo.Client.UI.Patient
{
    partial class PatientCardOperator
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
            this.patientCardOperatorUI1 = new Hemo.Client.UI.Patient.PatientCardOperatorUI();
            this.SuspendLayout();
            // 
            // patientCardOperatorUI1
            // 
            this.patientCardOperatorUI1.currentHemoId = null;
            this.patientCardOperatorUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientCardOperatorUI1.HasDirty = false;
            this.patientCardOperatorUI1.Location = new System.Drawing.Point(0, 0);
            this.patientCardOperatorUI1.Name = "patientCardOperatorUI1";
            this.patientCardOperatorUI1.Size = new System.Drawing.Size(449, 455);
            this.patientCardOperatorUI1.TabIndex = 0;
            // 
            // PatientCardOperator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 455);
            this.Controls.Add(this.patientCardOperatorUI1);
            this.Name = "PatientCardOperator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "透析卡管理";
            this.Load += new System.EventHandler(this.PatientCardOperator_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private PatientCardOperatorUI patientCardOperatorUI1;

    }
}