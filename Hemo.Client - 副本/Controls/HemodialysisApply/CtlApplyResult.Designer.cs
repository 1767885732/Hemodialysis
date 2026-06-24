namespace Hemo.Client.Controls.HemodialysisApply
{
    partial class CtlApplyResult
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labTime = new System.Windows.Forms.Label();
            this.labMsg = new System.Windows.Forms.Label();
            this.labRemark = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labTime
            // 
            this.labTime.AutoSize = true;
            this.labTime.Location = new System.Drawing.Point(0, 9);
            this.labTime.Name = "labTime";
            this.labTime.Size = new System.Drawing.Size(0, 14);
            this.labTime.TabIndex = 0;
            // 
            // labMsg
            // 
            this.labMsg.AutoSize = true;
            this.labMsg.Location = new System.Drawing.Point(70, 9);
            this.labMsg.Name = "labMsg";
            this.labMsg.Size = new System.Drawing.Size(0, 14);
            this.labMsg.TabIndex = 1;
            // 
            // labRemark
            // 
            this.labRemark.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labRemark.Location = new System.Drawing.Point(0, 32);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(142, 103);
            this.labRemark.TabIndex = 2;
            // 
            // CtlApplyResult
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labRemark);
            this.Controls.Add(this.labMsg);
            this.Controls.Add(this.labTime);
            this.Name = "CtlApplyResult";
            this.Size = new System.Drawing.Size(142, 135);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labTime;
        private System.Windows.Forms.Label labMsg;
        private System.Windows.Forms.Label labRemark;
    }
}
