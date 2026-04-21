namespace Hemo.Client.UI.PatientSchedule
{
    partial class TemplateList
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
            this.lab1 = new DevExpress.XtraEditors.LabelControl();
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.cbxTemplate = new DevExpress.XtraEditors.LookUpEdit();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.btn_Delete = new Hemo.Client.Controls.DXSimpleButton();
            this.simpleButton3 = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxTemplate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lab1
            // 
            this.lab1.Location = new System.Drawing.Point(12, 34);
            this.lab1.Name = "lab1";
            this.lab1.Size = new System.Drawing.Size(24, 14);
            this.lab1.TabIndex = 344;
            this.lab1.Text = "模板";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // cbxTemplate
            // 
            this.cbxTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxTemplate.EditValue = "";
            this.cbxTemplate.Location = new System.Drawing.Point(42, 31);
            this.cbxTemplate.Name = "cbxTemplate";
            this.cbxTemplate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxTemplate.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxTemplate.Size = new System.Drawing.Size(305, 20);
            this.cbxTemplate.TabIndex = 345;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImageIndex = 11;
            this.btnSave.Location = new System.Drawing.Point(95, 87);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 358;
            this.btnSave.Text = "选择(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Delete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Delete.ImageIndex = 1;
            this.btn_Delete.Location = new System.Drawing.Point(181, 87);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(80, 25);
            this.btn_Delete.TabIndex = 359;
            this.btn_Delete.Text = "删除(&D)";
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton3.ImageIndex = 3;
            this.simpleButton3.Location = new System.Drawing.Point(267, 87);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(80, 25);
            this.simpleButton3.TabIndex = 359;
            this.simpleButton3.Text = "取消(&C)";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // TemplateList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 119);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbxTemplate);
            this.Controls.Add(this.lab1);
            this.MinimumSize = new System.Drawing.Size(333, 136);
            this.Name = "TemplateList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "模板列表";
            this.Load += new System.EventHandler(this.TemplateList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxTemplate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lab1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider;
        private DevExpress.XtraEditors.LookUpEdit cbxTemplate;
        private Hemo.Client.Controls.DXSimpleButton btnSave;
        private Hemo.Client.Controls.DXSimpleButton btn_Delete;
        private Hemo.Client.Controls.DXSimpleButton simpleButton3;
    }
}