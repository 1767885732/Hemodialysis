namespace Hemo.Client.UI.Assessment
{
    partial class NutritionAssessment
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
            this.ctlNurtritionSga1 = new Hemo.Client.UI.Assessment.CtlNurtritionSga();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.tcRecord = new DevExpress.XtraTab.XtraTabControl();
            this.ctlUserLongInfo1 = new Hemo.Client.Controls.CtlUserLongInfo();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnPrint = new Hemo.Client.Controls.DXSimpleButton();
            this.btnExit = new Hemo.Client.Controls.DXSimpleButton();
            this.dxSimpleButton1 = new Hemo.Client.Controls.DXSimpleButton();
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
            this.tpLeft.Controls.Add(this.ctlNurtritionSga1);
            this.tpLeft.Name = "tpLeft";
            this.tpLeft.Size = new System.Drawing.Size(780, 503);
            this.tpLeft.Text = "评估内容";
            // 
            // ctlNurtritionSga1
            // 
            this.ctlNurtritionSga1.AssessmentID = null;
            this.ctlNurtritionSga1.CurrentHemoId = "";
            this.ctlNurtritionSga1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlNurtritionSga1.Location = new System.Drawing.Point(0, 0);
            this.ctlNurtritionSga1.MedAssessRow = null;
            this.ctlNurtritionSga1.MedAssessTable = null;
            this.ctlNurtritionSga1.Name = "ctlNurtritionSga1";
            this.ctlNurtritionSga1.Size = new System.Drawing.Size(763, 784);
            this.ctlNurtritionSga1.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.tcRecord);
            this.panelControl2.Controls.Add(this.ctlUserLongInfo1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(790, 575);
            this.panelControl2.TabIndex = 313;
            // 
            // tcRecord
            // 
            this.tcRecord.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.tcRecord.Appearance.Options.UseBackColor = true;
            this.tcRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcRecord.Location = new System.Drawing.Point(2, 41);
            this.tcRecord.Name = "tcRecord";
            this.tcRecord.SelectedTabPage = this.tpLeft;
            this.tcRecord.Size = new System.Drawing.Size(786, 532);
            this.tcRecord.TabIndex = 0;
            this.tcRecord.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpLeft});
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
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.dxSimpleButton1);
            this.panelControl1.Controls.Add(this.btnPrint);
            this.panelControl1.Controls.Add(this.btnExit);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 575);
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
            this.btnPrint.Visible = false;
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
            // dxSimpleButton1
            // 
            this.dxSimpleButton1.ImageIndex = 14;
            this.dxSimpleButton1.Location = new System.Drawing.Point(451, 8);
            this.dxSimpleButton1.Name = "dxSimpleButton1";
            this.dxSimpleButton1.Size = new System.Drawing.Size(75, 25);
            this.dxSimpleButton1.TabIndex = 381;
            this.dxSimpleButton1.Text = "检验数据";
            this.dxSimpleButton1.Click += new System.EventHandler(this.dxSimpleButton1_Click);
            // 
            // NutritionAssessment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 612);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.MaximumSize = new System.Drawing.Size(806, 651);
            this.MinimumSize = new System.Drawing.Size(806, 651);
            this.Name = "NutritionAssessment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "患者营养评估";
            this.Load += new System.EventHandler(this.NutritionAssessment_Load);
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
        private Controls.CtlUserLongInfo ctlUserLongInfo1;
        private CtlNurtritionSga ctlNurtritionSga1;
        private Controls.DXSimpleButton dxSimpleButton1;
    }
}