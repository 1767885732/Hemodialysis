namespace Hemo.Client.UI.Hemodialysis
{
    partial class ShowRescueRecord
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
            this.txtRescueRecord = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRescueRecord.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtRescueRecord
            // 
            this.txtRescueRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRescueRecord.Location = new System.Drawing.Point(0, 0);
            this.txtRescueRecord.Name = "txtRescueRecord";
            this.txtRescueRecord.Properties.MaxLength = 1400;
            this.txtRescueRecord.Size = new System.Drawing.Size(542, 353);
            this.txtRescueRecord.TabIndex = 2;
            // 
            // ShowRescueRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 353);
            this.Controls.Add(this.txtRescueRecord);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ShowRescueRecord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "抢救记录";
            ((System.ComponentModel.ISupportInitialize)(this.txtRescueRecord.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit txtRescueRecord;

    }
}