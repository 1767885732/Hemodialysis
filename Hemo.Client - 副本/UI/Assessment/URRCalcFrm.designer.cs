namespace Hemo.Client.UI.Assessment
{
    partial class URRCalcFrm
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtURR = new DevExpress.XtraEditors.TextEdit();
            this.lblURR = new DevExpress.XtraEditors.LabelControl();
            this.txtAFTER_BUN = new DevExpress.XtraEditors.TextEdit();
            this.lblAFTER_BUN = new DevExpress.XtraEditors.LabelControl();
            this.txtBEFORE_BUN = new DevExpress.XtraEditors.TextEdit();
            this.lblBEFORE_BUN = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new Hemo.Client.Controls.DXSimpleButton();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.ctlUserLongInfo = new Hemo.Client.Controls.CtlUserLongInfo();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtURR.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAFTER_BUN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBEFORE_BUN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.panelControl3);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Controls.Add(this.ctlUserLongInfo);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(774, 212);
            this.panelControl1.TabIndex = 0;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.labelControl1);
            this.panelControl3.Controls.Add(this.txtURR);
            this.panelControl3.Controls.Add(this.lblURR);
            this.panelControl3.Controls.Add(this.txtAFTER_BUN);
            this.panelControl3.Controls.Add(this.lblAFTER_BUN);
            this.panelControl3.Controls.Add(this.txtBEFORE_BUN);
            this.panelControl3.Controls.Add(this.lblBEFORE_BUN);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(2, 38);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(770, 140);
            this.panelControl3.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 78);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(291, 14);
            this.labelControl1.TabIndex = 30;
            this.labelControl1.Text = "备注：URR=（透前BUN-透后BUN）÷透前BUN ×100%";
            // 
            // txtURR
            // 
            this.txtURR.Location = new System.Drawing.Point(424, 16);
            this.txtURR.Name = "txtURR";
            this.txtURR.Properties.ReadOnly = true;
            this.txtURR.Size = new System.Drawing.Size(100, 21);
            this.txtURR.TabIndex = 29;
            // 
            // lblURR
            // 
            this.lblURR.Location = new System.Drawing.Point(371, 19);
            this.lblURR.Name = "lblURR";
            this.lblURR.Size = new System.Drawing.Size(46, 14);
            this.lblURR.TabIndex = 28;
            this.lblURR.Text = "URR结果";
            // 
            // txtAFTER_BUN
            // 
            this.txtAFTER_BUN.Location = new System.Drawing.Point(243, 16);
            this.txtAFTER_BUN.Name = "txtAFTER_BUN";
            this.txtAFTER_BUN.Size = new System.Drawing.Size(100, 21);
            this.txtAFTER_BUN.TabIndex = 27;
            this.txtAFTER_BUN.EditValueChanged += new System.EventHandler(this.txtAFTER_BUN_EditValueChanged);
            this.txtAFTER_BUN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAFTER_BUN_KeyPress);
            // 
            // lblAFTER_BUN
            // 
            this.lblAFTER_BUN.Location = new System.Drawing.Point(190, 19);
            this.lblAFTER_BUN.Name = "lblAFTER_BUN";
            this.lblAFTER_BUN.Size = new System.Drawing.Size(47, 14);
            this.lblAFTER_BUN.TabIndex = 26;
            this.lblAFTER_BUN.Text = "透后BUN";
            // 
            // txtBEFORE_BUN
            // 
            this.txtBEFORE_BUN.Location = new System.Drawing.Point(63, 16);
            this.txtBEFORE_BUN.Name = "txtBEFORE_BUN";
            this.txtBEFORE_BUN.Size = new System.Drawing.Size(100, 21);
            this.txtBEFORE_BUN.TabIndex = 25;
            this.txtBEFORE_BUN.EditValueChanged += new System.EventHandler(this.txtBEFORE_BUN_EditValueChanged);
            this.txtBEFORE_BUN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBEFORE_BUN_KeyPress);
            // 
            // lblBEFORE_BUN
            // 
            this.lblBEFORE_BUN.Location = new System.Drawing.Point(10, 19);
            this.lblBEFORE_BUN.Name = "lblBEFORE_BUN";
            this.lblBEFORE_BUN.Size = new System.Drawing.Size(47, 14);
            this.lblBEFORE_BUN.TabIndex = 24;
            this.lblBEFORE_BUN.Text = "透前BUN";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnClose);
            this.panelControl2.Controls.Add(this.btnSave);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(2, 178);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(770, 32);
            this.panelControl2.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.ImageIndex = 3;
            this.btnClose.Location = new System.Drawing.Point(673, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 25);
            this.btnClose.TabIndex = 303;
            this.btnClose.Text = "关闭(&D)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(592, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 302;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ctlUserLongInfo
            // 
            this.ctlUserLongInfo.DIAGNOSE = "";
            this.ctlUserLongInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlUserLongInfo.FormContainer = null;
            this.ctlUserLongInfo.HEMODIALYSIS_ID = "";
            this.ctlUserLongInfo.Location = new System.Drawing.Point(2, 2);
            this.ctlUserLongInfo.Name = "ctlUserLongInfo";
            this.ctlUserLongInfo.PanBorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.ctlUserLongInfo.PanelWidth = 770;
            this.ctlUserLongInfo.PatientType = "";
            this.ctlUserLongInfo.PatientTypeEnabled = true;
            this.ctlUserLongInfo.Size = new System.Drawing.Size(770, 36);
            this.ctlUserLongInfo.TabIndex = 1;
            // 
            // URRCalcFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 212);
            this.Controls.Add(this.panelControl1);
            this.Name = "URRCalcFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "URR计算统计";
            this.Load += new System.EventHandler(this.URRCalcFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtURR.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAFTER_BUN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBEFORE_BUN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Controls.CtlUserLongInfo ctlUserLongInfo;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private Controls.DXSimpleButton btnClose;
        private Controls.DXSimpleButton btnSave;
        private DevExpress.XtraEditors.TextEdit txtBEFORE_BUN;
        private DevExpress.XtraEditors.LabelControl lblBEFORE_BUN;
        private DevExpress.XtraEditors.TextEdit txtAFTER_BUN;
        private DevExpress.XtraEditors.LabelControl lblAFTER_BUN;
        private DevExpress.XtraEditors.TextEdit txtURR;
        private DevExpress.XtraEditors.LabelControl lblURR;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}