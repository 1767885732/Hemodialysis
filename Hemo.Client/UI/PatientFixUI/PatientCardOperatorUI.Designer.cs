namespace Hemo.Client.UI.Patient
{
    partial class PatientCardOperatorUI
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
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new Hemo.Client.Controls.DXSimpleButton();
            this.btnDELETE = new Hemo.Client.Controls.DXSimpleButton();
            this.btnLock = new Hemo.Client.Controls.DXSimpleButton();
            this.btnUnLock = new Hemo.Client.Controls.DXSimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.downpanel = new DevExpress.XtraEditors.PanelControl();
            this.txtSERIALNUMBER = new DevExpress.XtraEditors.TextEdit();
            this.txtCARDNO = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new DevExpress.XtraEditors.LabelControl();
            this.txtSEC = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new DevExpress.XtraEditors.LabelControl();
            this.label7 = new DevExpress.XtraEditors.LabelControl();
            this.txtCARDKEY = new DevExpress.XtraEditors.TextEdit();
            this.label6 = new DevExpress.XtraEditors.LabelControl();
            this.patientInfoForCard1 = new Hemo.Client.UI.Patient.PatientInfoForCard();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnClose1 = new Hemo.Client.Controls.DXSimpleButton();
            this.btn_Sup = new Hemo.Client.Controls.DXSimpleButton();
            this.patientInfoForCard2 = new Hemo.Client.UI.Patient.PatientInfoForCard();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.patientGridCtl1 = new Hemo.Client.UI.Patient.PatientGridCtl();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.patientGridCtl2 = new Hemo.Client.UI.Patient.PatientGridCtl();
            this.xtraTabPage5 = new DevExpress.XtraTab.XtraTabPage();
            this.patientGridCtl3 = new Hemo.Client.UI.Patient.PatientGridCtl();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.downpanel)).BeginInit();
            this.downpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSERIALNUMBER.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCARDNO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSEC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCARDKEY.Properties)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.xtraTabPage3.SuspendLayout();
            this.xtraTabPage4.SuspendLayout();
            this.xtraTabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(461, 455);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3,
            this.xtraTabPage4,
            this.xtraTabPage5});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            this.xtraTabControl1.TabIndexChanged += new System.EventHandler(this.xtraTabControl1_TabIndexChanged);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.panelControl1);
            this.xtraTabPage1.Controls.Add(this.groupControl1);
            this.xtraTabPage1.Controls.Add(this.patientInfoForCard1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(455, 426);
            this.xtraTabPage1.Text = "锁定/回收";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.btnDELETE);
            this.panelControl1.Controls.Add(this.btnLock);
            this.panelControl1.Controls.Add(this.btnUnLock);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 391);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(455, 35);
            this.panelControl1.TabIndex = 313;
            // 
            // btnClose
            // 
            this.btnClose.ImageIndex = 3;
            this.btnClose.Location = new System.Drawing.Point(314, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 27);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDELETE
            // 
            this.btnDELETE.ImageIndex = 25;
            this.btnDELETE.Location = new System.Drawing.Point(242, 6);
            this.btnDELETE.Name = "btnDELETE";
            this.btnDELETE.Size = new System.Drawing.Size(66, 27);
            this.btnDELETE.TabIndex = 0;
            this.btnDELETE.Text = "回收";
            this.btnDELETE.Click += new System.EventHandler(this.btnDELETE_Click);
            // 
            // btnLock
            // 
            this.btnLock.ImageIndex = 24;
            this.btnLock.Location = new System.Drawing.Point(15, 5);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(87, 27);
            this.btnLock.TabIndex = 0;
            this.btnLock.Text = "锁定";
            this.btnLock.Visible = false;
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
            // 
            // btnUnLock
            // 
            this.btnUnLock.ImageIndex = 26;
            this.btnUnLock.Location = new System.Drawing.Point(108, 5);
            this.btnUnLock.Name = "btnUnLock";
            this.btnUnLock.Size = new System.Drawing.Size(87, 27);
            this.btnUnLock.TabIndex = 0;
            this.btnUnLock.Text = "解锁";
            this.btnUnLock.Visible = false;
            this.btnUnLock.Click += new System.EventHandler(this.btnUnLock_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.downpanel);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 186);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(455, 240);
            this.groupControl1.TabIndex = 307;
            this.groupControl1.Text = "卡信息";
            // 
            // downpanel
            // 
            this.downpanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.downpanel.Controls.Add(this.txtSERIALNUMBER);
            this.downpanel.Controls.Add(this.txtCARDNO);
            this.downpanel.Controls.Add(this.label4);
            this.downpanel.Controls.Add(this.txtSEC);
            this.downpanel.Controls.Add(this.label5);
            this.downpanel.Controls.Add(this.label7);
            this.downpanel.Controls.Add(this.txtCARDKEY);
            this.downpanel.Controls.Add(this.label6);
            this.downpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.downpanel.Location = new System.Drawing.Point(2, 21);
            this.downpanel.Name = "downpanel";
            this.downpanel.Size = new System.Drawing.Size(451, 217);
            this.downpanel.TabIndex = 27;
            // 
            // txtSERIALNUMBER
            // 
            this.txtSERIALNUMBER.Enabled = false;
            this.txtSERIALNUMBER.Location = new System.Drawing.Point(121, 90);
            this.txtSERIALNUMBER.Name = "txtSERIALNUMBER";
            this.txtSERIALNUMBER.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtSERIALNUMBER.Properties.Appearance.Options.UseBackColor = true;
            this.txtSERIALNUMBER.Size = new System.Drawing.Size(233, 20);
            this.txtSERIALNUMBER.TabIndex = 26;
            // 
            // txtCARDNO
            // 
            this.txtCARDNO.Enabled = false;
            this.txtCARDNO.Location = new System.Drawing.Point(121, 128);
            this.txtCARDNO.Name = "txtCARDNO";
            this.txtCARDNO.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtCARDNO.Properties.Appearance.Options.UseBackColor = true;
            this.txtCARDNO.Size = new System.Drawing.Size(233, 20);
            this.txtCARDNO.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(13, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 14);
            this.label4.TabIndex = 24;
            this.label4.Text = "卡片序列号";
            // 
            // txtSEC
            // 
            this.txtSEC.Enabled = false;
            this.txtSEC.Location = new System.Drawing.Point(121, 18);
            this.txtSEC.Name = "txtSEC";
            this.txtSEC.Size = new System.Drawing.Size(233, 20);
            this.txtSEC.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(19, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 14);
            this.label5.TabIndex = 19;
            this.label5.Text = "密码";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(19, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 14);
            this.label7.TabIndex = 22;
            this.label7.Text = "扇区号";
            // 
            // txtCARDKEY
            // 
            this.txtCARDKEY.Enabled = false;
            this.txtCARDKEY.Location = new System.Drawing.Point(121, 55);
            this.txtCARDKEY.Name = "txtCARDKEY";
            this.txtCARDKEY.Size = new System.Drawing.Size(233, 20);
            this.txtCARDKEY.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(13, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 14);
            this.label6.TabIndex = 21;
            this.label6.Text = "数据";
            // 
            // patientInfoForCard1
            // 
            this.patientInfoForCard1.Dock = System.Windows.Forms.DockStyle.Top;
            this.patientInfoForCard1.HasDirty = false;
            this.patientInfoForCard1.hemoID = null;
            this.patientInfoForCard1.Location = new System.Drawing.Point(0, 0);
            this.patientInfoForCard1.Name = "patientInfoForCard1";
            this.patientInfoForCard1.Size = new System.Drawing.Size(455, 186);
            this.patientInfoForCard1.TabIndex = 314;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.panelControl2);
            this.xtraTabPage2.Controls.Add(this.patientInfoForCard2);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(455, 426);
            this.xtraTabPage2.Text = "补卡";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnClose1);
            this.panelControl2.Controls.Add(this.btn_Sup);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 391);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(455, 35);
            this.panelControl2.TabIndex = 316;
            // 
            // btnClose1
            // 
            this.btnClose1.ImageIndex = 3;
            this.btnClose1.Location = new System.Drawing.Point(314, 6);
            this.btnClose1.Name = "btnClose1";
            this.btnClose1.Size = new System.Drawing.Size(87, 27);
            this.btnClose1.TabIndex = 0;
            this.btnClose1.Text = "关闭";
            this.btnClose1.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btn_Sup
            // 
            this.btn_Sup.ImageIndex = 21;
            this.btn_Sup.Location = new System.Drawing.Point(217, 6);
            this.btn_Sup.Name = "btn_Sup";
            this.btn_Sup.Size = new System.Drawing.Size(87, 27);
            this.btn_Sup.TabIndex = 0;
            this.btn_Sup.Text = "补卡";
            this.btn_Sup.Click += new System.EventHandler(this.btn_Sup_Click);
            // 
            // patientInfoForCard2
            // 
            this.patientInfoForCard2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientInfoForCard2.HasDirty = false;
            this.patientInfoForCard2.hemoID = null;
            this.patientInfoForCard2.Location = new System.Drawing.Point(0, 0);
            this.patientInfoForCard2.Name = "patientInfoForCard2";
            this.patientInfoForCard2.Size = new System.Drawing.Size(455, 426);
            this.patientInfoForCard2.TabIndex = 317;
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.patientGridCtl1);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(455, 426);
            this.xtraTabPage3.Text = "制卡患者列表";
            // 
            // patientGridCtl1
            // 
            this.patientGridCtl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientGridCtl1.HasDirty = false;
            this.patientGridCtl1.Location = new System.Drawing.Point(0, 0);
            this.patientGridCtl1.Name = "patientGridCtl1";
            this.patientGridCtl1.Size = new System.Drawing.Size(455, 426);
            this.patientGridCtl1.TabIndex = 0;
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.patientGridCtl2);
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.PageVisible = false;
            this.xtraTabPage4.Size = new System.Drawing.Size(455, 426);
            this.xtraTabPage4.Text = "卡片锁定";
            // 
            // patientGridCtl2
            // 
            this.patientGridCtl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientGridCtl2.HasDirty = false;
            this.patientGridCtl2.Location = new System.Drawing.Point(0, 0);
            this.patientGridCtl2.Name = "patientGridCtl2";
            this.patientGridCtl2.Size = new System.Drawing.Size(455, 426);
            this.patientGridCtl2.TabIndex = 0;
            // 
            // xtraTabPage5
            // 
            this.xtraTabPage5.Controls.Add(this.patientGridCtl3);
            this.xtraTabPage5.Name = "xtraTabPage5";
            this.xtraTabPage5.Size = new System.Drawing.Size(455, 426);
            this.xtraTabPage5.Text = "作废患者列表";
            // 
            // patientGridCtl3
            // 
            this.patientGridCtl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientGridCtl3.HasDirty = false;
            this.patientGridCtl3.Location = new System.Drawing.Point(0, 0);
            this.patientGridCtl3.Name = "patientGridCtl3";
            this.patientGridCtl3.Size = new System.Drawing.Size(455, 426);
            this.patientGridCtl3.TabIndex = 0;
            // 
            // PatientCardOperatorUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "PatientCardOperatorUI";
            this.Size = new System.Drawing.Size(461, 455);
            this.Load += new System.EventHandler(this.PatientCardOperatorUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.downpanel)).EndInit();
            this.downpanel.ResumeLayout(false);
            this.downpanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSERIALNUMBER.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCARDNO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSEC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCARDKEY.Properties)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.xtraTabPage3.ResumeLayout(false);
            this.xtraTabPage4.ResumeLayout(false);
            this.xtraTabPage5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtCARDNO;
        private DevExpress.XtraEditors.TextEdit txtSEC;
        private DevExpress.XtraEditors.LabelControl label7;
        private DevExpress.XtraEditors.LabelControl label6;
        private DevExpress.XtraEditors.TextEdit txtCARDKEY;
        private DevExpress.XtraEditors.LabelControl label5;
        private DevExpress.XtraEditors.LabelControl label4;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Controls.DXSimpleButton btnClose;
        private Controls.DXSimpleButton btnLock;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private Controls.DXSimpleButton btnClose1;
        private Controls.DXSimpleButton btn_Sup;
        private DevExpress.XtraEditors.PanelControl downpanel;
        private DevExpress.XtraEditors.TextEdit txtSERIALNUMBER;
        private PatientInfoForCard patientInfoForCard1;
        private PatientInfoForCard patientInfoForCard2;
        private Controls.DXSimpleButton btnUnLock;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage5;
        private PatientGridCtl patientGridCtl1;
        private PatientGridCtl patientGridCtl2;
        private PatientGridCtl patientGridCtl3;
        private Controls.DXSimpleButton btnDELETE;
    }
}