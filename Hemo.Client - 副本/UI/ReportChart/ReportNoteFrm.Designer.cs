namespace Hemo.Client.UI.ReportChart
{
    partial class ReportNoteFrm
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblReportName = new DevExpress.XtraEditors.LabelControl();
            this.meNote = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.meNote.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(202, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 18);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "报表名称：";
            // 
            // lblReportName
            // 
            this.lblReportName.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblReportName.Appearance.Options.UseFont = true;
            this.lblReportName.Location = new System.Drawing.Point(288, 25);
            this.lblReportName.Name = "lblReportName";
            this.lblReportName.Size = new System.Drawing.Size(64, 18);
            this.lblReportName.TabIndex = 1;
            this.lblReportName.Text = "治疗数据";
            // 
            // meNote
            // 
            this.meNote.Location = new System.Drawing.Point(12, 62);
            this.meNote.Name = "meNote";
            this.meNote.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.meNote.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.meNote.Properties.Appearance.Options.UseFont = true;
            this.meNote.Properties.Appearance.Options.UseForeColor = true;
            this.meNote.Size = new System.Drawing.Size(559, 272);
            this.meNote.TabIndex = 2;
            // 
            // ReportNoteFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 346);
            this.Controls.Add(this.meNote);
            this.Controls.Add(this.lblReportName);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ReportNoteFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "报表说明";
            this.Load += new System.EventHandler(this.ReportNoteFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.meNote.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblReportName;
        private DevExpress.XtraEditors.MemoEdit meNote;
    }
}