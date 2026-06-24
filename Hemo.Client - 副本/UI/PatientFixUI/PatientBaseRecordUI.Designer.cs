namespace Hemo.Client.UI.PatientFixUI
{
    partial class PatientBaseRecordUI
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnUpToWeb = new Hemo.Client.Controls.DXSimpleButton();
            this.btnDelete = new Hemo.Client.Controls.DXSimpleButton();
            this.btnNew = new Hemo.Client.Controls.DXSimpleButton();
            this.btnPrint = new Hemo.Client.Controls.DXSimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.tcRecord = new DevExpress.XtraTab.XtraTabControl();
            this.tpLeft = new DevExpress.XtraTab.XtraTabPage();
            this.ctlBaseRecord = new Hemo.Client.Controls.CtlBaseRecord();
            this.tpMiddle = new DevExpress.XtraTab.XtraTabPage();
            this.ctlBaseRecordEvent = new Hemo.Client.Controls.CtlBaseRecordEvent();
            this.tpRight = new DevExpress.XtraTab.XtraTabPage();
            this.ctlBaseRecordDiagnose = new Hemo.Client.Controls.CtlBaseRecordDiagnose();
            this.tpreport = new DevExpress.XtraTab.XtraTabPage();
            this.patientRecipeFrm1 = new Hemo.Client.UI.ReportChart.PatientRecipeFrm();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcRecord)).BeginInit();
            this.tcRecord.SuspendLayout();
            this.tpLeft.SuspendLayout();
            this.tpMiddle.SuspendLayout();
            this.tpRight.SuspendLayout();
            this.tpreport.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(445, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 25);
            this.btnSave.TabIndex = 309;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnUpToWeb);
            this.panelControl1.Controls.Add(this.btnDelete);
            this.panelControl1.Controls.Add(this.btnNew);
            this.panelControl1.Controls.Add(this.btnPrint);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 586);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(699, 36);
            this.panelControl1.TabIndex = 310;
            // 
            // btnUpToWeb
            // 
            this.btnUpToWeb.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpToWeb.Appearance.Options.UseFont = true;
            this.btnUpToWeb.ImageIndex = 19;
            this.btnUpToWeb.Location = new System.Drawing.Point(288, 5);
            this.btnUpToWeb.Name = "btnUpToWeb";
            this.btnUpToWeb.Size = new System.Drawing.Size(75, 25);
            this.btnUpToWeb.TabIndex = 320;
            this.btnUpToWeb.Text = "上传(&U)";
            this.btnUpToWeb.Click += new System.EventHandler(this.btnUpToWeb_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.ImageIndex = 1;
            this.btnDelete.Location = new System.Drawing.Point(521, 5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 25);
            this.btnDelete.TabIndex = 319;
            this.btnDelete.Text = "删除(&D)";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNew
            // 
            this.btnNew.Enabled = false;
            this.btnNew.ImageIndex = 0;
            this.btnNew.Location = new System.Drawing.Point(369, 5);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(70, 25);
            this.btnNew.TabIndex = 318;
            this.btnNew.Text = "新增(&A)";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.ImageIndex = 6;
            this.btnPrint.Location = new System.Drawing.Point(597, 5);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(95, 25);
            this.btnPrint.TabIndex = 312;
            this.btnPrint.Text = "打印病历(&P)";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.tcRecord);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(699, 586);
            this.panelControl2.TabIndex = 313;
            // 
            // tcRecord
            // 
            this.tcRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcRecord.HeaderButtons = ((DevExpress.XtraTab.TabButtons)((DevExpress.XtraTab.TabButtons.Next | DevExpress.XtraTab.TabButtons.Default)));
            this.tcRecord.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.tcRecord.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            this.tcRecord.Location = new System.Drawing.Point(2, 2);
            this.tcRecord.Name = "tcRecord";
            this.tcRecord.RightToLeftLayout = DevExpress.Utils.DefaultBoolean.True;
            this.tcRecord.SelectedTabPage = this.tpLeft;
            this.tcRecord.Size = new System.Drawing.Size(695, 582);
            this.tcRecord.TabIndex = 0;
            this.tcRecord.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpLeft,
            this.tpMiddle,
            this.tpRight,
            this.tpreport});
            this.tcRecord.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tcRecord_SelectedPageChanged);
            // 
            // tpLeft
            // 
            this.tpLeft.Appearance.Header.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpLeft.Appearance.Header.Options.UseFont = true;
            this.tpLeft.AutoScroll = true;
            this.tpLeft.Controls.Add(this.ctlBaseRecord);
            this.tpLeft.Name = "tpLeft";
            this.tpLeft.Size = new System.Drawing.Size(605, 576);
            this.tpLeft.Text = "病历内容";
            // 
            // ctlBaseRecord
            // 
            this.ctlBaseRecord.AutoScroll = true;
            this.ctlBaseRecord.BaseRecord = null;
            this.ctlBaseRecord.BeginDate = new System.DateTime(((long)(0)));
            this.ctlBaseRecord.HasDirty = false;
            this.ctlBaseRecord.hemoId = null;
            this.ctlBaseRecord.HemoId = "";
            this.ctlBaseRecord.Location = new System.Drawing.Point(3, 0);
            this.ctlBaseRecord.Name = "ctlBaseRecord";
            this.ctlBaseRecord.Size = new System.Drawing.Size(727, 1100);
            this.ctlBaseRecord.TabIndex = 0;
            // 
            // tpMiddle
            // 
            this.tpMiddle.Appearance.Header.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpMiddle.Appearance.Header.Options.UseFont = true;
            this.tpMiddle.Controls.Add(this.ctlBaseRecordEvent);
            this.tpMiddle.Name = "tpMiddle";
            this.tpMiddle.Size = new System.Drawing.Size(605, 576);
            this.tpMiddle.Text = "病历事件";
            // 
            // ctlBaseRecordEvent
            // 
            this.ctlBaseRecordEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlBaseRecordEvent.HemoId = "";
            this.ctlBaseRecordEvent.Location = new System.Drawing.Point(0, 0);
            this.ctlBaseRecordEvent.Name = "ctlBaseRecordEvent";
            this.ctlBaseRecordEvent.RecordEvent = null;
            this.ctlBaseRecordEvent.Size = new System.Drawing.Size(605, 576);
            this.ctlBaseRecordEvent.TabIndex = 0;
            // 
            // tpRight
            // 
            this.tpRight.Appearance.Header.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpRight.Appearance.Header.Options.UseFont = true;
            this.tpRight.Controls.Add(this.ctlBaseRecordDiagnose);
            this.tpRight.Name = "tpRight";
            this.tpRight.Size = new System.Drawing.Size(605, 576);
            this.tpRight.Text = "病历诊断";
            // 
            // ctlBaseRecordDiagnose
            // 
            this.ctlBaseRecordDiagnose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlBaseRecordDiagnose.HemoId = "";
            this.ctlBaseRecordDiagnose.Location = new System.Drawing.Point(0, 0);
            this.ctlBaseRecordDiagnose.Name = "ctlBaseRecordDiagnose";
            this.ctlBaseRecordDiagnose.RecordDiagnose = null;
            this.ctlBaseRecordDiagnose.Size = new System.Drawing.Size(605, 576);
            this.ctlBaseRecordDiagnose.TabIndex = 0;
            // 
            // tpreport
            // 
            this.tpreport.Appearance.Header.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpreport.Appearance.Header.Options.UseFont = true;
            this.tpreport.Controls.Add(this.patientRecipeFrm1);
            this.tpreport.Name = "tpreport";
            this.tpreport.Size = new System.Drawing.Size(605, 576);
            this.tpreport.Text = "透析趋势图";
            // 
            // patientRecipeFrm1
            // 
            this.patientRecipeFrm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientRecipeFrm1.HasDirty = false;
            this.patientRecipeFrm1.hemoId = null;
            this.patientRecipeFrm1.HemoId = "";
            this.patientRecipeFrm1.Location = new System.Drawing.Point(0, 0);
            this.patientRecipeFrm1.Name = "patientRecipeFrm1";
            this.patientRecipeFrm1.Size = new System.Drawing.Size(605, 576);
            this.patientRecipeFrm1.TabIndex = 0;
            // 
            // PatientBaseRecordUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.MinimumSize = new System.Drawing.Size(680, 500);
            this.Name = "PatientBaseRecordUI";
            this.Size = new System.Drawing.Size(699, 622);
            this.Load += new System.EventHandler(this.PatientBaseRecordUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tcRecord)).EndInit();
            this.tcRecord.ResumeLayout(false);
            this.tpLeft.ResumeLayout(false);
            this.tpMiddle.ResumeLayout(false);
            this.tpRight.ResumeLayout(false);
            this.tpreport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Hemo.Client.Controls.DXSimpleButton btnSave;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Hemo.Client.Controls.DXSimpleButton btnPrint;
        private Controls.DXSimpleButton btnNew;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraTab.XtraTabControl tcRecord;
        private DevExpress.XtraTab.XtraTabPage tpLeft;
        private DevExpress.XtraTab.XtraTabPage tpMiddle;
        private DevExpress.XtraTab.XtraTabPage tpRight;
        private Controls.CtlBaseRecord ctlBaseRecord;
        private Controls.CtlBaseRecordEvent ctlBaseRecordEvent;
        private Controls.CtlBaseRecordDiagnose ctlBaseRecordDiagnose;
        private Controls.DXSimpleButton btnDelete;
        private DevExpress.XtraTab.XtraTabPage tpreport;
        private ReportChart.PatientRecipeFrm patientRecipeFrm1;
        private Controls.DXSimpleButton btnUpToWeb;

    }
}