namespace Hemo.Client.UI.Patient
{
    partial class RegOfDealthFrm
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.date_create = new DevExpress.XtraEditors.DateEdit();
            this.txtEVENTANALYSIS = new DevExpress.XtraEditors.MemoEdit();
            this.txtCORRECTIVEACTIONS = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.btnTemplate = new Hemo.Client.Controls.DXSimpleButton();
            this.UserInfo = new Hemo.Client.Controls.CtlUserLongInfo();
            this.btn_Print = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.date_create.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_create.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEVENTANALYSIS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCORRECTIVEACTIONS.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(32, 52);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "登记时间";
            // 
            // date_create
            // 
            this.date_create.EditValue = null;
            this.date_create.Location = new System.Drawing.Point(141, 53);
            this.date_create.Name = "date_create";
            this.date_create.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date_create.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.date_create.Size = new System.Drawing.Size(100, 20);
            this.date_create.TabIndex = 2;
            // 
            // txtEVENTANALYSIS
            // 
            this.txtEVENTANALYSIS.Location = new System.Drawing.Point(141, 89);
            this.txtEVENTANALYSIS.Name = "txtEVENTANALYSIS";
            this.txtEVENTANALYSIS.Size = new System.Drawing.Size(598, 156);
            this.txtEVENTANALYSIS.TabIndex = 3;
            // 
            // txtCORRECTIVEACTIONS
            // 
            this.txtCORRECTIVEACTIONS.Location = new System.Drawing.Point(141, 261);
            this.txtCORRECTIVEACTIONS.Name = "txtCORRECTIVEACTIONS";
            this.txtCORRECTIVEACTIONS.Size = new System.Drawing.Size(598, 154);
            this.txtCORRECTIVEACTIONS.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(32, 92);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(84, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "事件及原因分析";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(32, 261);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(72, 28);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "针对死亡事\r\n件的改进措施";
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(664, 421);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnTemplate
            // 
            this.btnTemplate.ImageIndex = 5;
            this.btnTemplate.Location = new System.Drawing.Point(501, 421);
            this.btnTemplate.Name = "btnTemplate";
            this.btnTemplate.Size = new System.Drawing.Size(75, 25);
            this.btnTemplate.TabIndex = 4;
            this.btnTemplate.Text = "模板";
            this.btnTemplate.Visible = false;
            this.btnTemplate.Click += new System.EventHandler(this.btnTemplate_Click);
            // 
            // UserInfo
            // 
            this.UserInfo.DIAGNOSE = "";
            this.UserInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.UserInfo.FormContainer = null;
            this.UserInfo.HEMODIALYSIS_ID = "";
            this.UserInfo.Location = new System.Drawing.Point(0, 0);
            this.UserInfo.Name = "UserInfo";
            this.UserInfo.PanBorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.UserInfo.PanelWidth = 790;
            this.UserInfo.PatientType = "";
            this.UserInfo.PatientTypeEnabled = true;
            this.UserInfo.Size = new System.Drawing.Size(790, 38);
            this.UserInfo.TabIndex = 6;
            this.UserInfo.Visible = false;
            // 
            // btn_Print
            // 
            this.btn_Print.ImageIndex = 6;
            this.btn_Print.Location = new System.Drawing.Point(582, 421);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(76, 24);
            this.btn_Print.TabIndex = 7;
            this.btn_Print.Text = "打印(&P)";
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // RegOfDealthFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_Print);
            this.Controls.Add(this.UserInfo);
            this.Controls.Add(this.btnTemplate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtCORRECTIVEACTIONS);
            this.Controls.Add(this.txtEVENTANALYSIS);
            this.Controls.Add(this.date_create);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.MaximumSize = new System.Drawing.Size(790, 468);
            this.MinimumSize = new System.Drawing.Size(790, 468);
            this.Name = "RegOfDealthFrm";
            this.Size = new System.Drawing.Size(790, 468);
            this.Load += new System.EventHandler(this.RegOfDealthFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.date_create.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_create.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEVENTANALYSIS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCORRECTIVEACTIONS.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit date_create;
        private DevExpress.XtraEditors.MemoEdit txtEVENTANALYSIS;
        private DevExpress.XtraEditors.MemoEdit txtCORRECTIVEACTIONS;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private Controls.DXSimpleButton btnSave;
        private Controls.DXSimpleButton btnTemplate;
        private Controls.CtlUserLongInfo UserInfo;
        private Controls.DXSimpleButton btn_Print;
    }
}