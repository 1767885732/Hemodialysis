namespace Hemo.Client.UI.Config
{
    partial class EditConfig
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
            this.cbxSTATUS = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lab4 = new DevExpress.XtraEditors.LabelControl();
            this.txtITEM_VALUE = new DevExpress.XtraEditors.TextEdit();
            this.lab2 = new DevExpress.XtraEditors.LabelControl();
            this.lab1 = new DevExpress.XtraEditors.LabelControl();
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.btnClose = new Hemo.Client.Controls.DXSimpleButton();
            this.lab5 = new DevExpress.XtraEditors.LabelControl();
            this.txtORDER_NUMBER = new DevExpress.XtraEditors.TextEdit();
            this.txtITEM_NAME = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxSTATUS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_VALUE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtORDER_NUMBER.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_NAME.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxSTATUS
            // 
            this.cbxSTATUS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxSTATUS.EditValue = "";
            this.cbxSTATUS.EnterMoveNextControl = true;
            this.cbxSTATUS.Location = new System.Drawing.Point(64, 169);
            this.cbxSTATUS.Name = "cbxSTATUS";
            this.cbxSTATUS.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxSTATUS.Properties.Items.AddRange(new object[] {
            "停用",
            "启用"});
            this.cbxSTATUS.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxSTATUS.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxSTATUS.Size = new System.Drawing.Size(316, 20);
            this.cbxSTATUS.TabIndex = 3;
            // 
            // lab4
            // 
            this.lab4.Location = new System.Drawing.Point(10, 172);
            this.lab4.Name = "lab4";
            this.lab4.Size = new System.Drawing.Size(24, 14);
            this.lab4.TabIndex = 304;
            this.lab4.Text = "状态";
            // 
            // txtITEM_VALUE
            // 
            this.txtITEM_VALUE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtITEM_VALUE.EnterMoveNextControl = true;
            this.txtITEM_VALUE.Location = new System.Drawing.Point(64, 10);
            this.txtITEM_VALUE.Name = "txtITEM_VALUE";
            this.txtITEM_VALUE.Properties.MaxLength = 200;
            this.txtITEM_VALUE.Size = new System.Drawing.Size(316, 20);
            this.txtITEM_VALUE.TabIndex = 1;
            // 
            // lab2
            // 
            this.lab2.Location = new System.Drawing.Point(10, 40);
            this.lab2.Name = "lab2";
            this.lab2.Size = new System.Drawing.Size(24, 14);
            this.lab2.TabIndex = 301;
            this.lab2.Text = "名称";
            // 
            // lab1
            // 
            this.lab1.Location = new System.Drawing.Point(10, 13);
            this.lab1.Name = "lab1";
            this.lab1.Size = new System.Drawing.Size(12, 14);
            this.lab1.TabIndex = 300;
            this.lab1.Text = "值";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(200, 226);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 27);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImageIndex = 3;
            this.btnClose.Location = new System.Drawing.Point(293, 226);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 27);
            this.btnClose.TabIndex = 307;
            this.btnClose.Text = "取消(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lab5
            // 
            this.lab5.Location = new System.Drawing.Point(10, 199);
            this.lab5.Name = "lab5";
            this.lab5.Size = new System.Drawing.Size(48, 14);
            this.lab5.TabIndex = 308;
            this.lab5.Text = "排序字段";
            // 
            // txtORDER_NUMBER
            // 
            this.txtORDER_NUMBER.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtORDER_NUMBER.EnterMoveNextControl = true;
            this.txtORDER_NUMBER.Location = new System.Drawing.Point(64, 196);
            this.txtORDER_NUMBER.Name = "txtORDER_NUMBER";
            this.txtORDER_NUMBER.Properties.Mask.EditMask = "n";
            this.txtORDER_NUMBER.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtORDER_NUMBER.Properties.MaxLength = 5;
            this.txtORDER_NUMBER.Size = new System.Drawing.Size(316, 20);
            this.txtORDER_NUMBER.TabIndex = 4;
            // 
            // txtITEM_NAME
            // 
            this.txtITEM_NAME.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtITEM_NAME.Location = new System.Drawing.Point(64, 36);
            this.txtITEM_NAME.Name = "txtITEM_NAME";
            this.txtITEM_NAME.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtITEM_NAME.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtITEM_NAME.Properties.Appearance.Options.UseFont = true;
            this.txtITEM_NAME.Properties.Appearance.Options.UseForeColor = true;
            this.txtITEM_NAME.Properties.MaxLength = 2000;
            this.txtITEM_NAME.Size = new System.Drawing.Size(316, 127);
            this.txtITEM_NAME.TabIndex = 2;
            // 
            // EditConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 265);
            this.Controls.Add(this.txtORDER_NUMBER);
            this.Controls.Add(this.lab5);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbxSTATUS);
            this.Controls.Add(this.lab4);
            this.Controls.Add(this.txtITEM_VALUE);
            this.Controls.Add(this.lab2);
            this.Controls.Add(this.lab1);
            this.Controls.Add(this.txtITEM_NAME);
            this.MinimumSize = new System.Drawing.Size(294, 212);
            this.Name = "EditConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.EditConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cbxSTATUS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_VALUE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtORDER_NUMBER.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_NAME.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit cbxSTATUS;
        private DevExpress.XtraEditors.LabelControl lab4;
        private DevExpress.XtraEditors.TextEdit txtITEM_VALUE;
        private DevExpress.XtraEditors.LabelControl lab2;
        private DevExpress.XtraEditors.LabelControl lab1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider;
        private Hemo.Client.Controls.DXSimpleButton btnSave;
        private Hemo.Client.Controls.DXSimpleButton btnClose;
        private DevExpress.XtraEditors.TextEdit txtORDER_NUMBER;
        private DevExpress.XtraEditors.LabelControl lab5;
        private DevExpress.XtraEditors.MemoEdit txtITEM_NAME;

    }
}