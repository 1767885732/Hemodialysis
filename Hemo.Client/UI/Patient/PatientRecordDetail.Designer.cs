namespace Hemo.Client.UI.Patient {
    partial class PatientRecordDetail
    {
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
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnPrint = new Hemo.Client.Controls.DXSimpleButton();
            this.btnExit = new Hemo.Client.Controls.DXSimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.tcRecord = new DevExpress.XtraTab.XtraTabControl();
            this.tpLeft = new DevExpress.XtraTab.XtraTabPage();
            this.ctlMedicalRecord = new Hemo.Client.Controls.CtlMedicalRecord();
            this.tpRight = new DevExpress.XtraTab.XtraTabPage();
            this.ctlDrugUseRecord = new Hemo.Client.Controls.CtlDrugUseRecord();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcRecord)).BeginInit();
            this.tcRecord.SuspendLayout();
            this.tpLeft.SuspendLayout();
            this.tpRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(452, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 25);
            this.btnSave.TabIndex = 309;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnPrint);
            this.panelControl1.Controls.Add(this.btnExit);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 476);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(684, 36);
            this.panelControl1.TabIndex = 310;
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
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.tcRecord);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(684, 476);
            this.panelControl2.TabIndex = 311;
            // 
            // tcRecord
            // 
            this.tcRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcRecord.Location = new System.Drawing.Point(2, 2);
            this.tcRecord.Name = "tcRecord";
            this.tcRecord.SelectedTabPage = this.tpLeft;
            this.tcRecord.Size = new System.Drawing.Size(680, 472);
            this.tcRecord.TabIndex = 0;
            this.tcRecord.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpLeft,
            this.tpRight});
            this.tcRecord.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tcRecord_SelectedPageChanged);
            // 
            // tpLeft
            // 
            this.tpLeft.AutoScroll = true;
            this.tpLeft.Controls.Add(this.ctlMedicalRecord);
            this.tpLeft.Name = "tpLeft";
            this.tpLeft.Size = new System.Drawing.Size(673, 442);
            this.tpLeft.Text = "病历内容";
            // 
            // ctlMedicalRecord
            // 
            this.ctlMedicalRecord.CurrentHemoId = "";
            this.ctlMedicalRecord.CurrentRecordRow = null;
            this.ctlMedicalRecord.Location = new System.Drawing.Point(3, 3);
            this.ctlMedicalRecord.Name = "ctlMedicalRecord";
            this.ctlMedicalRecord.Size = new System.Drawing.Size(640, 1445);
            this.ctlMedicalRecord.TabIndex = 0;
            // 
            // tpRight
            // 
            this.tpRight.Controls.Add(this.ctlDrugUseRecord);
            this.tpRight.Name = "tpRight";
            this.tpRight.PageVisible = false;
            this.tpRight.Size = new System.Drawing.Size(673, 442);
            this.tpRight.Text = "用药记录";
            // 
            // ctlDrugUseRecord
            // 
            this.ctlDrugUseRecord.DtDrug = null;
            this.ctlDrugUseRecord.Location = new System.Drawing.Point(3, 3);
            this.ctlDrugUseRecord.Name = "ctlDrugUseRecord";
            this.ctlDrugUseRecord.Size = new System.Drawing.Size(660, 435);
            this.ctlDrugUseRecord.TabIndex = 0;
            // 
            // PatientRecordDetail
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(684, 512);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "PatientRecordDetail";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "病人病历";
            this.Load += new System.EventHandler(this.PatientRecordDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tcRecord)).EndInit();
            this.tcRecord.ResumeLayout(false);
            this.tpLeft.ResumeLayout(false);
            this.tpRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Hemo.Client.Controls.DXSimpleButton btnSave;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraTab.XtraTabControl tcRecord;
        private DevExpress.XtraTab.XtraTabPage tpLeft;
        private DevExpress.XtraTab.XtraTabPage tpRight;
        private Controls.CtlDrugUseRecord ctlDrugUseRecord;
        private Controls.CtlMedicalRecord ctlMedicalRecord;
        private Hemo.Client.Controls.DXSimpleButton btnExit;
        private Hemo.Client.Controls.DXSimpleButton btnPrint;

    }
}