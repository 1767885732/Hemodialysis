namespace Hemo.Client.UI.Hemodialysis
{
    partial class EditAppraisalRule
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
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtITEM_NAME = new DevExpress.XtraEditors.MemoEdit();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtITEM_DESC = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.lupSTATUS = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtORDER_NUMBER = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.btnCancel = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_NAME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_DESC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupSTATUS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtORDER_NUMBER.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(30, 21);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "名称";
            // 
            // txtITEM_NAME
            // 
            this.txtITEM_NAME.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtITEM_NAME.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "ITEM_NAME", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtITEM_NAME.Location = new System.Drawing.Point(84, 20);
            this.txtITEM_NAME.Name = "txtITEM_NAME";
            this.txtITEM_NAME.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtITEM_NAME.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtITEM_NAME.Properties.Appearance.Options.UseFont = true;
            this.txtITEM_NAME.Properties.Appearance.Options.UseForeColor = true;
            this.txtITEM_NAME.Properties.MaxLength = 2000;
            this.txtITEM_NAME.Size = new System.Drawing.Size(315, 65);
            this.txtITEM_NAME.TabIndex = 3;
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(Hemo.Model.HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULERow);
            // 
            // txtITEM_DESC
            // 
            this.txtITEM_DESC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtITEM_DESC.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "ITEM_DESC", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtITEM_DESC.Location = new System.Drawing.Point(84, 110);
            this.txtITEM_DESC.Name = "txtITEM_DESC";
            this.txtITEM_DESC.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtITEM_DESC.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtITEM_DESC.Properties.Appearance.Options.UseFont = true;
            this.txtITEM_DESC.Properties.Appearance.Options.UseForeColor = true;
            this.txtITEM_DESC.Properties.MaxLength = 2000;
            this.txtITEM_DESC.Size = new System.Drawing.Size(315, 65);
            this.txtITEM_DESC.TabIndex = 9;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(30, 111);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(24, 14);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "描述";
            // 
            // lupSTATUS
            // 
            this.lupSTATUS.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource, "STATUS", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lupSTATUS.Location = new System.Drawing.Point(84, 201);
            this.lupSTATUS.Name = "lupSTATUS";
            this.lupSTATUS.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupSTATUS.Properties.NullText = "";
            this.lupSTATUS.Size = new System.Drawing.Size(316, 20);
            this.lupSTATUS.TabIndex = 11;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(30, 204);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(48, 14);
            this.labelControl6.TabIndex = 10;
            this.labelControl6.Text = "是否启用";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(30, 250);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(24, 14);
            this.labelControl7.TabIndex = 12;
            this.labelControl7.Text = "顺序";
            // 
            // txtORDER_NUMBER
            // 
            this.txtORDER_NUMBER.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "ORDER_NUMBER", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtORDER_NUMBER.Location = new System.Drawing.Point(84, 247);
            this.txtORDER_NUMBER.Name = "txtORDER_NUMBER";
            this.txtORDER_NUMBER.Size = new System.Drawing.Size(316, 20);
            this.txtORDER_NUMBER.TabIndex = 13;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(232, 289);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 25);
            this.btnSave.TabIndex = 410;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageIndex = 3;
            this.btnCancel.Location = new System.Drawing.Point(319, 289);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 25);
            this.btnCancel.TabIndex = 411;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EditAppraisalRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 332);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtORDER_NUMBER);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.lupSTATUS);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.txtITEM_DESC);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.txtITEM_NAME);
            this.Controls.Add(this.labelControl2);
            this.Name = "EditAppraisalRule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "绩效考核规则类别";
            this.Load += new System.EventHandler(this.EditAppraisalRule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_NAME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_DESC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupSTATUS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtORDER_NUMBER.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.MemoEdit txtITEM_NAME;
        private DevExpress.XtraEditors.MemoEdit txtITEM_DESC;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LookUpEdit lupSTATUS;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtORDER_NUMBER;
        private Controls.DXSimpleButton btnSave;
        private Controls.DXSimpleButton btnCancel;
        private System.Windows.Forms.BindingSource bindingSource;
    }
}