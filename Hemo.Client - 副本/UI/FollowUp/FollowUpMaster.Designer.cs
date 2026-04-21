namespace Hemo.Client.UI.FollowUp
{
    partial class FollowUpMaster {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FollowUpMaster));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Cancle = new Hemo.Client.Controls.DXSimpleButton();
            this.btn_Print = new Hemo.Client.Controls.DXSimpleButton();
            this.btn_Save = new Hemo.Client.Controls.DXSimpleButton();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.followUpControl1 = new Hemo.Client.UI.FollowUp.FollowUpControl();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.panel1.SuspendLayout();
            this.xtraScrollableControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_Cancle);
            this.panel1.Controls.Add(this.btn_Print);
            this.panel1.Controls.Add(this.btn_Save);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 605);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(624, 37);
            this.panel1.TabIndex = 0;
            // 
            // btn_Cancle
            // 
            this.btn_Cancle.ImageIndex = 3;
            this.btn_Cancle.Location = new System.Drawing.Point(523, 6);
            this.btn_Cancle.Name = "btn_Cancle";
            this.btn_Cancle.Size = new System.Drawing.Size(75, 25);
            this.btn_Cancle.TabIndex = 0;
            this.btn_Cancle.Text = "关闭(&C)";
            this.btn_Cancle.Click += new System.EventHandler(this.btn_Cancle_Click);
            // 
            // btn_Print
            // 
            this.btn_Print.ImageIndex = 6;
            this.btn_Print.Location = new System.Drawing.Point(441, 6);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(75, 25);
            this.btn_Print.TabIndex = 0;
            this.btn_Print.Text = "打印(&P)";
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.ImageIndex = 7;
            this.btn_Save.Location = new System.Drawing.Point(360, 6);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 25);
            this.btn_Save.TabIndex = 0;
            this.btn_Save.Text = "保存(&S)";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Controls.Add(this.followUpControl1);
            this.xtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.xtraScrollableControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(624, 605);
            this.xtraScrollableControl1.TabIndex = 1;
            // 
            // followUpControl1
            // 
            this.followUpControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.followUpControl1.Appearance.Options.UseBackColor = true;
            this.followUpControl1.CurrentData = null;
            this.followUpControl1.HasDirty = false;
            this.followUpControl1.Location = new System.Drawing.Point(4, 3);
            this.followUpControl1.MaximumSize = new System.Drawing.Size(595, 842);
            this.followUpControl1.MinimumSize = new System.Drawing.Size(595, 842);
            this.followUpControl1.Name = "followUpControl1";
            this.followUpControl1.Size = new System.Drawing.Size(595, 842);
            this.followUpControl1.TabIndex = 0;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // FollowUpMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 642);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.xtraScrollableControl1);
            this.Name = "FollowUpMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "随访单";
            this.Load += new System.EventHandler(this.FollowUpMaster_Load);
            this.panel1.ResumeLayout(false);
            this.xtraScrollableControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Hemo.Client.Controls.DXSimpleButton btn_Cancle;
        private Hemo.Client.Controls.DXSimpleButton btn_Print;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        public Hemo.Client.Controls.DXSimpleButton btn_Save;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        public FollowUpControl followUpControl1;






    }
}