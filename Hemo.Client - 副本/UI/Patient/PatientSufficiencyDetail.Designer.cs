namespace Hemo.Client.UI.Patient
{
    partial class PatientSufficiencyDetail
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
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.tpLeft = new DevExpress.XtraTab.XtraTabPage();
            this.ctlSufficiencyRecord1 = new Hemo.Client.Controls.CtlSufficiencyRecord();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.ctlUserLongInfo1 = new Hemo.Client.Controls.CtlUserLongInfo();
            this.tcRecord = new DevExpress.XtraTab.XtraTabControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnPrint = new Hemo.Client.Controls.DXSimpleButton();
            this.btnExit = new Hemo.Client.Controls.DXSimpleButton();
            this.tpLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcRecord)).BeginInit();
            this.tcRecord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(532, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 25);
            this.btnSave.TabIndex = 309;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tpLeft
            // 
            this.tpLeft.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.tpLeft.Appearance.PageClient.Options.UseBackColor = true;
            this.tpLeft.AutoScroll = true;
            this.tpLeft.Controls.Add(this.ctlSufficiencyRecord1);
            this.tpLeft.Name = "tpLeft";
            this.tpLeft.Size = new System.Drawing.Size(779, 571);
            this.tpLeft.Text = "评估内容";
            // 
            // ctlSufficiencyRecord1
            // 
            this.ctlSufficiencyRecord1.CurrentHemoId = "";
            this.ctlSufficiencyRecord1.CurrentRecordRow = null;
            this.ctlSufficiencyRecord1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlSufficiencyRecord1.Location = new System.Drawing.Point(0, 0);
            this.ctlSufficiencyRecord1.Name = "ctlSufficiencyRecord1";
            this.ctlSufficiencyRecord1.Size = new System.Drawing.Size(762, 1043);
            this.ctlSufficiencyRecord1.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.tcRecord);
            this.panelControl2.Controls.Add(this.ctlUserLongInfo1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(790, 644);
            this.panelControl2.TabIndex = 313;
            // 
            // ctlUserLongInfo1
            // 
            this.ctlUserLongInfo1.DIAGNOSE = "";
            this.ctlUserLongInfo1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlUserLongInfo1.FormContainer = null;
            this.ctlUserLongInfo1.HEMODIALYSIS_ID = "";
            this.ctlUserLongInfo1.Location = new System.Drawing.Point(2, 2);
            this.ctlUserLongInfo1.Name = "ctlUserLongInfo1";
            this.ctlUserLongInfo1.PanBorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.ctlUserLongInfo1.PanelWidth = 786;
            this.ctlUserLongInfo1.PatientType = "";
            this.ctlUserLongInfo1.PatientTypeEnabled = true;
            this.ctlUserLongInfo1.Size = new System.Drawing.Size(786, 39);
            this.ctlUserLongInfo1.TabIndex = 1;
            // 
            // tcRecord
            // 
            this.tcRecord.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.tcRecord.Appearance.Options.UseBackColor = true;
            this.tcRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcRecord.Location = new System.Drawing.Point(2, 41);
            this.tcRecord.Name = "tcRecord";
            this.tcRecord.SelectedTabPage = this.tpLeft;
            this.tcRecord.Size = new System.Drawing.Size(786, 601);
            this.tcRecord.TabIndex = 0;
            this.tcRecord.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpLeft});
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnPrint);
            this.panelControl1.Controls.Add(this.btnExit);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 644);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(790, 37);
            this.panelControl1.TabIndex = 312;
            // 
            // btnPrint
            // 
            this.btnPrint.ImageIndex = 6;
            this.btnPrint.Location = new System.Drawing.Point(607, 7);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(70, 25);
            this.btnPrint.TabIndex = 312;
            this.btnPrint.Text = "打印(&P)";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.ImageIndex = 3;
            this.btnExit.Location = new System.Drawing.Point(682, 7);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(70, 25);
            this.btnExit.TabIndex = 310;
            this.btnExit.Text = "关闭(&Q)";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // PatientSufficiencyDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 681);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "PatientSufficiencyDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "透析充分性评估";
            this.Load += new System.EventHandler(this.PatientSufficiencyDetail_Load);
            this.tpLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tcRecord)).EndInit();
            this.tcRecord.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.DXSimpleButton btnSave;
        private DevExpress.XtraTab.XtraTabPage tpLeft;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraTab.XtraTabControl tcRecord;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Controls.DXSimpleButton btnPrint;
        private Controls.DXSimpleButton btnExit;
        private Controls.CtlSufficiencyRecord ctlSufficiencyRecord1;
        private Controls.CtlUserLongInfo ctlUserLongInfo1;

    }
}