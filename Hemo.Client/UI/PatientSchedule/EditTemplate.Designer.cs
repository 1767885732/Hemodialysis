namespace Hemo.Client.UI.PatientSchedule
{
    partial class EditTemplate
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
            this.txtPATIENT_SCHEDULE_TEMPLATE_NAME = new DevExpress.XtraEditors.TextEdit();
            this.lab1 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.simpleButton2 = new Hemo.Client.Controls.DXSimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btn_edit = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPATIENT_SCHEDULE_TEMPLATE_NAME.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // txtPATIENT_SCHEDULE_TEMPLATE_NAME
            // 
            this.txtPATIENT_SCHEDULE_TEMPLATE_NAME.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPATIENT_SCHEDULE_TEMPLATE_NAME.Location = new System.Drawing.Point(66, 31);
            this.txtPATIENT_SCHEDULE_TEMPLATE_NAME.Name = "txtPATIENT_SCHEDULE_TEMPLATE_NAME";
            this.txtPATIENT_SCHEDULE_TEMPLATE_NAME.Properties.MaxLength = 20;
            this.txtPATIENT_SCHEDULE_TEMPLATE_NAME.Size = new System.Drawing.Size(158, 21);
            this.txtPATIENT_SCHEDULE_TEMPLATE_NAME.TabIndex = 342;
            // 
            // lab1
            // 
            this.lab1.Location = new System.Drawing.Point(12, 33);
            this.lab1.Name = "lab1";
            this.lab1.Size = new System.Drawing.Size(48, 14);
            this.lab1.TabIndex = 343;
            this.lab1.Text = "模板名称";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(97, 66);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 25);
            this.btnSave.TabIndex = 356;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton2.ImageIndex = 3;
            this.simpleButton2.Location = new System.Drawing.Point(204, 66);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(100, 25);
            this.simpleButton2.TabIndex = 357;
            this.simpleButton2.Text = "取消(&C)";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 9);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(168, 14);
            this.labelControl1.TabIndex = 343;
            this.labelControl1.Text = "提示：请输入要保存的模板名称";
            // 
            // btn_edit
            // 
            this.btn_edit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_edit.ImageIndex = 2;
            this.btn_edit.Location = new System.Drawing.Point(230, 31);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(75, 21);
            this.btn_edit.TabIndex = 357;
            this.btn_edit.Text = "重命名";
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // EditTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 98);
            this.Controls.Add(this.btn_edit);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtPATIENT_SCHEDULE_TEMPLATE_NAME);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lab1);
            this.MinimumSize = new System.Drawing.Size(333, 136);
            this.Name = "EditTemplate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "模板信息";
            this.Load += new System.EventHandler(this.EditTemplate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPATIENT_SCHEDULE_TEMPLATE_NAME.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider;
        private DevExpress.XtraEditors.LabelControl lab1;
        private Hemo.Client.Controls.DXSimpleButton btnSave;
        private Hemo.Client.Controls.DXSimpleButton simpleButton2;
        public DevExpress.XtraEditors.TextEdit txtPATIENT_SCHEDULE_TEMPLATE_NAME;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public Controls.DXSimpleButton btn_edit;
    }
}