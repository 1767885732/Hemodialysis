namespace Hemo.Client.Controls
{
    partial class CtlPatientSign
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtlPatientSign));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.btnSign = new Hemo.Client.Controls.DXSimpleButton();
            this.axHWPenSign = new AxHWPenSignLib.AxHWPenSign();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axHWPenSign)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Controls.Add(this.btnSign);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 355);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(700, 35);
            this.panelControl1.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(354, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 25);
            this.btnSave.TabIndex = 347;
            this.btnSave.Text = "保存签名(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSign
            // 
            this.btnSign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSign.ImageIndex = 2;
            this.btnSign.Location = new System.Drawing.Point(248, 5);
            this.btnSign.Name = "btnSign";
            this.btnSign.Size = new System.Drawing.Size(100, 25);
            this.btnSign.TabIndex = 346;
            this.btnSign.Text = "重新签名(&W)";
            this.btnSign.Click += new System.EventHandler(this.btnSign_Click);
            // 
            // axHWPenSign
            // 
            this.axHWPenSign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axHWPenSign.Enabled = true;
            this.axHWPenSign.Location = new System.Drawing.Point(0, 0);
            this.axHWPenSign.Name = "axHWPenSign";
            this.axHWPenSign.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axHWPenSign.OcxState")));
            this.axHWPenSign.Size = new System.Drawing.Size(700, 355);
            this.axHWPenSign.TabIndex = 1;
            // 
            // CtlPatientSign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.axHWPenSign);
            this.Controls.Add(this.panelControl1);
            this.Name = "CtlPatientSign";
            this.Size = new System.Drawing.Size(700, 390);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axHWPenSign)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private AxHWPenSignLib.AxHWPenSign axHWPenSign;
        private DXSimpleButton btnSign;
        private DXSimpleButton btnSave;
    }
}
