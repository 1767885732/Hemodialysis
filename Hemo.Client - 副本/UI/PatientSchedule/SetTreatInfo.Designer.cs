namespace Hemo.Client.UI.PatientSchedule
{
    partial class SetTreatInfo
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
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.lab1 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.cbxPURIFICATION_MODE = new DevExpress.XtraEditors.LookUpEdit();
            this.cbxPURIFIER_MODEL = new DevExpress.XtraEditors.LookUpEdit();
            this.lab2 = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxPURIFICATION_MODE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxPURIFIER_MODEL.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // lab1
            // 
            this.lab1.Location = new System.Drawing.Point(12, 15);
            this.lab1.Name = "lab1";
            this.lab1.Size = new System.Drawing.Size(48, 14);
            this.lab1.TabIndex = 343;
            this.lab1.Text = "净化方式";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(108, 84);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(101, 31);
            this.btnSave.TabIndex = 356;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbxPURIFICATION_MODE
            // 
            this.cbxPURIFICATION_MODE.EditValue = "";
            this.cbxPURIFICATION_MODE.Location = new System.Drawing.Point(78, 12);
            this.cbxPURIFICATION_MODE.Name = "cbxPURIFICATION_MODE";
            this.cbxPURIFICATION_MODE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxPURIFICATION_MODE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxPURIFICATION_MODE.Size = new System.Drawing.Size(238, 21);
            this.cbxPURIFICATION_MODE.TabIndex = 358;
            this.cbxPURIFICATION_MODE.EditValueChanged += new System.EventHandler(this.cbxPURIFICATION_MODE_EditValueChanged);
            // 
            // cbxPURIFIER_MODEL
            // 
            this.cbxPURIFIER_MODEL.EditValue = "";
            this.cbxPURIFIER_MODEL.Location = new System.Drawing.Point(78, 39);
            this.cbxPURIFIER_MODEL.Name = "cbxPURIFIER_MODEL";
            this.cbxPURIFIER_MODEL.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxPURIFIER_MODEL.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxPURIFIER_MODEL.Size = new System.Drawing.Size(238, 21);
            this.cbxPURIFIER_MODEL.TabIndex = 360;
            // 
            // lab2
            // 
            this.lab2.Location = new System.Drawing.Point(12, 42);
            this.lab2.Name = "lab2";
            this.lab2.Size = new System.Drawing.Size(60, 14);
            this.lab2.TabIndex = 359;
            this.lab2.Text = "净化器型号";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImageIndex = 3;
            this.btnClose.Location = new System.Drawing.Point(215, 84);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(101, 31);
            this.btnClose.TabIndex = 357;
            this.btnClose.Text = "取消(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // SetTreatInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 127);
            this.Controls.Add(this.cbxPURIFIER_MODEL);
            this.Controls.Add(this.lab2);
            this.Controls.Add(this.cbxPURIFICATION_MODE);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lab1);
            this.MinimumSize = new System.Drawing.Size(343, 165);
            this.Name = "SetTreatInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置治疗方式和透析器";
            this.Load += new System.EventHandler(this.SetTreatInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxPURIFICATION_MODE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxPURIFIER_MODEL.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider;
        private DevExpress.XtraEditors.LabelControl lab1;
        private Hemo.Client.Controls.DXSimpleButton btnSave;
        private DevExpress.XtraEditors.LookUpEdit cbxPURIFICATION_MODE;
        private DevExpress.XtraEditors.LookUpEdit cbxPURIFIER_MODEL;
        private DevExpress.XtraEditors.LabelControl lab2;
        private Hemo.Client.Controls.DXSimpleButton btnClose;
    }
}