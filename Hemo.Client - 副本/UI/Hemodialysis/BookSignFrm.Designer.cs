namespace Hemo.Client.UI.Hemodialysis
{
    partial class BookSignFrm
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
            this.ctlPatientSign = new Hemo.Client.Controls.CtlPatientSign();
            this.SuspendLayout();
            // 
            // ctlPatientSign
            // 
            this.ctlPatientSign.BookNames = null;
            this.ctlPatientSign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlPatientSign.Location = new System.Drawing.Point(0, 0);
            this.ctlPatientSign.Name = "ctlPatientSign";
            this.ctlPatientSign.Patient = null;
            this.ctlPatientSign.Size = new System.Drawing.Size(694, 362);
            this.ctlPatientSign.TabIndex = 0;
            // 
            // BookSignFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 362);
            this.Controls.Add(this.ctlPatientSign);
            this.Name = "BookSignFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "患者签名";
            this.Load += new System.EventHandler(this.BookSignFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CtlPatientSign ctlPatientSign;
    }
}