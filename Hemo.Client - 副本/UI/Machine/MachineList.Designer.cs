namespace Hemo.Client.UI.Machine
{
    partial class MachineList
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
            this.pnlRecordList = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlRecordList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlRecordList
            // 
            this.pnlRecordList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRecordList.Location = new System.Drawing.Point(0, 0);
            this.pnlRecordList.Name = "pnlRecordList";
            this.pnlRecordList.Size = new System.Drawing.Size(1200, 542);
            this.pnlRecordList.TabIndex = 2;
            // 
            // MachineList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 542);
            this.Controls.Add(this.pnlRecordList);
            this.Name = "MachineList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "血透机列表";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MachineList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlRecordList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlRecordList;


    }
}