namespace Hemo.Client.UI.Patient
{
    partial class PatientPictureAction
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
            this.getPatientPic1 = new Hemo.Client.UI.Patient.GetPatientPic();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new Hemo.Client.Controls.DXSimpleButton();
            this.btnCaptiure = new Hemo.Client.Controls.DXSimpleButton();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // getPatientPic1
            // 
            this.getPatientPic1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.getPatientPic1.HasDirty = false;
            this.getPatientPic1.Location = new System.Drawing.Point(0, 0);
            this.getPatientPic1.Name = "getPatientPic1";
            this.getPatientPic1.PatientPic = null;
            this.getPatientPic1.Size = new System.Drawing.Size(760, 388);
            this.getPatientPic1.TabIndex = 0;
            this.getPatientPic1.Load += new System.EventHandler(this.getPatientPic1_Load);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.btnCaptiure);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 388);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(760, 34);
            this.panelControl1.TabIndex = 42;
            // 
            // btnClose
            // 
            this.btnClose.ImageIndex = 3;
            this.btnClose.Location = new System.Drawing.Point(663, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 292;
            this.btnClose.Text = "关闭(&D)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCaptiure
            // 
            this.btnCaptiure.ImageIndex = 17;
            this.btnCaptiure.Location = new System.Drawing.Point(501, 6);
            this.btnCaptiure.Name = "btnCaptiure";
            this.btnCaptiure.Size = new System.Drawing.Size(75, 23);
            this.btnCaptiure.TabIndex = 27;
            this.btnCaptiure.Text = "拍照(&P)";
            this.btnCaptiure.Click += new System.EventHandler(this.btnCaptiure_Click);
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 11;
            this.btnSave.Location = new System.Drawing.Point(582, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 27;
            this.btnSave.Text = "确定(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // PatientPictureAction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 422);
            this.Controls.Add(this.getPatientPic1);
            this.Controls.Add(this.panelControl1);
            this.Name = "PatientPictureAction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "患者拍照";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GetPatientPic getPatientPic1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Controls.DXSimpleButton btnClose;
        private Controls.DXSimpleButton btnCaptiure;
        private Controls.DXSimpleButton btnSave;
    }
}