namespace Hemo.Client.UI.Patient
{
    partial class PatientKolcabaDetail
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatientKolcabaDetail));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lbKolcaba = new DevExpress.XtraEditors.LabelControl();
            this.btnPrint = new Hemo.Client.Controls.DXSimpleButton();
            this.btnExit = new Hemo.Client.Controls.DXSimpleButton();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.tcRecord = new DevExpress.XtraTab.XtraTabControl();
            this.tpLeft = new DevExpress.XtraTab.XtraTabPage();
            this.ctlKolcabaRecord1 = new Hemo.Client.Controls.CtlKolcabaRecord();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcRecord)).BeginInit();
            this.tcRecord.SuspendLayout();
            this.tpLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lbKolcaba);
            this.panelControl1.Controls.Add(this.btnPrint);
            this.panelControl1.Controls.Add(this.btnExit);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 562);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(710, 34);
            this.panelControl1.TabIndex = 312;
            // 
            // lbKolcaba
            // 
            this.lbKolcaba.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lbKolcaba.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbKolcaba.Appearance.Options.UseBackColor = true;
            this.lbKolcaba.Appearance.Options.UseFont = true;
            this.lbKolcaba.Location = new System.Drawing.Point(42, 5);
            this.lbKolcaba.Name = "lbKolcaba";
            this.lbKolcaba.Size = new System.Drawing.Size(166, 19);
            this.lbKolcaba.TabIndex = 313;
            this.lbKolcaba.Text = "Kolcaba的舒适状况量：";
            // 
            // btnPrint
            // 
            this.btnPrint.ImageIndex = 6;
            this.btnPrint.Location = new System.Drawing.Point(527, 5);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(70, 25);
            this.btnPrint.TabIndex = 312;
            this.btnPrint.Text = "打印(&P)";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.ImageIndex = 3;
            this.btnExit.Location = new System.Drawing.Point(602, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(70, 25);
            this.btnExit.TabIndex = 310;
            this.btnExit.Text = "关闭(&Q)";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(452, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 25);
            this.btnSave.TabIndex = 309;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tcRecord
            // 
            this.tcRecord.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.tcRecord.Appearance.Options.UseBackColor = true;
            this.tcRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcRecord.Location = new System.Drawing.Point(0, 0);
            this.tcRecord.Name = "tcRecord";
            this.tcRecord.SelectedTabPage = this.tpLeft;
            this.tcRecord.Size = new System.Drawing.Size(710, 596);
            this.tcRecord.TabIndex = 311;
            this.tcRecord.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpLeft});
            // 
            // tpLeft
            // 
            this.tpLeft.AutoScroll = true;
            this.tpLeft.Controls.Add(this.ctlKolcabaRecord1);
            this.tpLeft.Name = "tpLeft";
            this.tpLeft.Size = new System.Drawing.Size(703, 566);
            this.tpLeft.Text = "Kolcaba内容";
            // 
            // ctlKolcabaRecord1
            // 
            this.ctlKolcabaRecord1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.ctlKolcabaRecord1.Appearance.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ctlKolcabaRecord1.Appearance.Options.UseBackColor = true;
            this.ctlKolcabaRecord1.Appearance.Options.UseFont = true;
            this.ctlKolcabaRecord1.CurrentHemoId = "";
            this.ctlKolcabaRecord1.CurrentRecordRow = null;
            this.ctlKolcabaRecord1.Location = new System.Drawing.Point(0, 0);
            this.ctlKolcabaRecord1.Margin = new System.Windows.Forms.Padding(6);
            this.ctlKolcabaRecord1.Name = "ctlKolcabaRecord1";
            this.ctlKolcabaRecord1.Size = new System.Drawing.Size(681, 936);
            this.ctlKolcabaRecord1.TabIndex = 0;
            // 
            // PatientKolcabaDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 596);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.tcRecord);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PatientKolcabaDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Kolcaba的舒适状况量表";
            this.Load += new System.EventHandler(this.PatientKolcabaDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcRecord)).EndInit();
            this.tcRecord.ResumeLayout(false);
            this.tpLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Controls.DXSimpleButton btnPrint;
        private Controls.DXSimpleButton btnExit;
        private Controls.DXSimpleButton btnSave;
        private DevExpress.XtraTab.XtraTabControl tcRecord;
        private DevExpress.XtraTab.XtraTabPage tpLeft;
        private Controls.CtlKolcabaRecord ctlKolcabaRecord1;
        private DevExpress.XtraEditors.LabelControl lbKolcaba;



    }
}