namespace Hemo.Client.UI.Hemodialysis
{
    partial class EditAppraisalRuleItem
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
            this.lupITEM_TYPE = new DevExpress.XtraEditors.LookUpEdit();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtITEM_NAME = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtITEM_VALUE = new DevExpress.XtraEditors.SpinEdit();
            this.lupSCORE_TYPE = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtITEM_DESC = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.lupSTATUS = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtORDER_NUMBER = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.btnCancel = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lupITEM_TYPE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_NAME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_VALUE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupSCORE_TYPE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_DESC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupSTATUS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtORDER_NUMBER.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(30, 21);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "类别";
            // 
            // lupITEM_TYPE
            // 
            this.lupITEM_TYPE.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource, "ITEM_TYPE", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lupITEM_TYPE.Location = new System.Drawing.Point(84, 18);
            this.lupITEM_TYPE.Name = "lupITEM_TYPE";
            this.lupITEM_TYPE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupITEM_TYPE.Properties.NullText = "";
            this.lupITEM_TYPE.Size = new System.Drawing.Size(316, 20);
            this.lupITEM_TYPE.TabIndex = 1;
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(Hemo.Model.HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULERow);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(30, 55);
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
            this.txtITEM_NAME.Location = new System.Drawing.Point(84, 54);
            this.txtITEM_NAME.Name = "txtITEM_NAME";
            this.txtITEM_NAME.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtITEM_NAME.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtITEM_NAME.Properties.Appearance.Options.UseFont = true;
            this.txtITEM_NAME.Properties.Appearance.Options.UseForeColor = true;
            this.txtITEM_NAME.Properties.MaxLength = 2000;
            this.txtITEM_NAME.Size = new System.Drawing.Size(315, 65);
            this.txtITEM_NAME.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(30, 181);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 14);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "分值";
            // 
            // txtITEM_VALUE
            // 
            this.txtITEM_VALUE.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingSource, "ITEM_VALUE", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtITEM_VALUE.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtITEM_VALUE.Location = new System.Drawing.Point(84, 178);
            this.txtITEM_VALUE.Name = "txtITEM_VALUE";
            this.txtITEM_VALUE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtITEM_VALUE.Size = new System.Drawing.Size(316, 20);
            this.txtITEM_VALUE.TabIndex = 5;
            // 
            // lupSCORE_TYPE
            // 
            this.lupSCORE_TYPE.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource, "SCORE_TYPE", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lupSCORE_TYPE.Location = new System.Drawing.Point(84, 138);
            this.lupSCORE_TYPE.Name = "lupSCORE_TYPE";
            this.lupSCORE_TYPE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupSCORE_TYPE.Properties.NullText = "";
            this.lupSCORE_TYPE.Size = new System.Drawing.Size(316, 20);
            this.lupSCORE_TYPE.TabIndex = 7;
            this.lupSCORE_TYPE.EditValueChanged += new System.EventHandler(this.lupSCORE_TYPE_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(30, 141);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "得分类型";
            // 
            // txtITEM_DESC
            // 
            this.txtITEM_DESC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtITEM_DESC.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "ITEM_DESC", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtITEM_DESC.Location = new System.Drawing.Point(84, 219);
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
            this.labelControl5.Location = new System.Drawing.Point(30, 220);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(24, 14);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "描述";
            // 
            // lupSTATUS
            // 
            this.lupSTATUS.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource, "STATUS", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lupSTATUS.Location = new System.Drawing.Point(84, 303);
            this.lupSTATUS.Name = "lupSTATUS";
            this.lupSTATUS.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupSTATUS.Properties.NullText = "";
            this.lupSTATUS.Size = new System.Drawing.Size(316, 20);
            this.lupSTATUS.TabIndex = 11;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(30, 306);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(48, 14);
            this.labelControl6.TabIndex = 10;
            this.labelControl6.Text = "是否启用";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(30, 346);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(24, 14);
            this.labelControl7.TabIndex = 12;
            this.labelControl7.Text = "顺序";
            // 
            // txtORDER_NUMBER
            // 
            this.txtORDER_NUMBER.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "ORDER_NUMBER", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtORDER_NUMBER.Location = new System.Drawing.Point(84, 343);
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
            this.btnSave.Location = new System.Drawing.Point(231, 378);
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
            this.btnCancel.Location = new System.Drawing.Point(318, 378);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 25);
            this.btnCancel.TabIndex = 411;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EditAppraisalRuleItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 417);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtORDER_NUMBER);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.lupSTATUS);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.txtITEM_DESC);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.lupSCORE_TYPE);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.txtITEM_VALUE);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtITEM_NAME);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.lupITEM_TYPE);
            this.Controls.Add(this.labelControl1);
            this.Name = "EditAppraisalRuleItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "绩效考核规则条目";
            this.Load += new System.EventHandler(this.EditAppraisalRuleItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupITEM_TYPE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_NAME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_VALUE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupSCORE_TYPE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_DESC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupSTATUS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtORDER_NUMBER.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lupITEM_TYPE;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.MemoEdit txtITEM_NAME;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SpinEdit txtITEM_VALUE;
        private DevExpress.XtraEditors.LookUpEdit lupSCORE_TYPE;
        private DevExpress.XtraEditors.LabelControl labelControl4;
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