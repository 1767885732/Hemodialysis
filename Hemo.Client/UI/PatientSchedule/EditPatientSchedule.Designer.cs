namespace Hemo.Client.UI.PatientSchedule
{
    partial class EditPatientSchedule
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
            this.simpleButton1 = new Hemo.Client.Controls.DXSimpleButton();
            this.txtCOMMENTS = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCOMMENTS.Properties)).BeginInit();
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
            this.lab1.Size = new System.Drawing.Size(24, 14);
            this.lab1.TabIndex = 343;
            this.lab1.Text = "备注";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(96, 55);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(101, 31);
            this.btnSave.TabIndex = 356;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.ImageIndex = 3;
            this.simpleButton1.Location = new System.Drawing.Point(203, 55);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(101, 31);
            this.simpleButton1.TabIndex = 357;
            this.simpleButton1.Text = "取消(&C)";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // txtCOMMENTS
            // 
            this.txtCOMMENTS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCOMMENTS.Location = new System.Drawing.Point(42, 12);
            this.txtCOMMENTS.Name = "txtCOMMENTS";
            this.txtCOMMENTS.Properties.MaxLength = 5;
            this.txtCOMMENTS.Size = new System.Drawing.Size(262, 21);
            this.txtCOMMENTS.TabIndex = 342;
            this.txtCOMMENTS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCOMMENTS_KeyDown);
            // 
            // EditPatientSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 98);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lab1);
            this.Controls.Add(this.txtCOMMENTS);
            this.MinimumSize = new System.Drawing.Size(333, 136);
            this.Name = "EditPatientSchedule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "维护备注信息";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCOMMENTS.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider;
        private DevExpress.XtraEditors.LabelControl lab1;
        private Hemo.Client.Controls.DXSimpleButton btnSave;
        private Hemo.Client.Controls.DXSimpleButton simpleButton1;
        private DevExpress.XtraEditors.TextEdit txtCOMMENTS;
    }
}