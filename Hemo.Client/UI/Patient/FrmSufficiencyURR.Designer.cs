namespace Hemo.Client.UI.Patient
{
    partial class FrmSufficiencyURR
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
            this.ctlUserLongInfo = new Hemo.Client.Controls.CtlUserLongInfo();
            this.ctlPatientSufficiency = new Hemo.Client.Controls.CtlPatientSufficiency();
            this.SuspendLayout();
            // 
            // ctlUserLongInfo
            // 
            this.ctlUserLongInfo.DIAGNOSE = "";
            this.ctlUserLongInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlUserLongInfo.FormContainer = null;
            this.ctlUserLongInfo.HEMODIALYSIS_ID = "";
            this.ctlUserLongInfo.Location = new System.Drawing.Point(0, 0);
            this.ctlUserLongInfo.Name = "ctlUserLongInfo";
            this.ctlUserLongInfo.PanBorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.ctlUserLongInfo.PanelWidth = 774;
            this.ctlUserLongInfo.PatientType = "";
            this.ctlUserLongInfo.PatientTypeEnabled = true;
            this.ctlUserLongInfo.Size = new System.Drawing.Size(774, 36);
            this.ctlUserLongInfo.TabIndex = 2;
            // 
            // ctlPatientSufficiency
            // 
            this.ctlPatientSufficiency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlPatientSufficiency.HemoId = "";
            this.ctlPatientSufficiency.Location = new System.Drawing.Point(0, 36);
            this.ctlPatientSufficiency.Name = "ctlPatientSufficiency";
            this.ctlPatientSufficiency.Size = new System.Drawing.Size(774, 426);
            this.ctlPatientSufficiency.TabIndex = 3;
            this.ctlPatientSufficiency.Load += new System.EventHandler(this.ctlPatientSufficiency_Load);
            // 
            // FrmSufficiencyURR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 462);
            this.Controls.Add(this.ctlPatientSufficiency);
            this.Controls.Add(this.ctlUserLongInfo);
            this.Name = "FrmSufficiencyURR";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "URR|Kt/V|TS|MDRD评估列表";
            this.Load += new System.EventHandler(this.FrmSufficiencyURR_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CtlUserLongInfo ctlUserLongInfo;
        private Controls.CtlPatientSufficiency ctlPatientSufficiency;
    }
}