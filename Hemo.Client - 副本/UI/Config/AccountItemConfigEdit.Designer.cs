namespace Hemo.Client.UI.Config
{
    partial class AccountItemConfigEdit
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
            this.txtORDER_NUMBER = new DevExpress.XtraEditors.TextEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.lab5 = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new Hemo.Client.Controls.DXSimpleButton();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.lab4 = new DevExpress.XtraEditors.LabelControl();
            this.txtITEM_VALUE = new DevExpress.XtraEditors.TextEdit();
            this.lab2 = new DevExpress.XtraEditors.LabelControl();
            this.lab1 = new DevExpress.XtraEditors.LabelControl();
            this.txtITEM_NAME = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtPrice = new DevExpress.XtraEditors.SpinEdit();
            this.txtID = new DevExpress.XtraEditors.TextEdit();
            this.cbxSTATUS = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.txtORDER_NUMBER.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_VALUE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_NAME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxSTATUS.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtORDER_NUMBER
            // 
            this.txtORDER_NUMBER.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtORDER_NUMBER.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ORDER_NUMBER", true));
            this.txtORDER_NUMBER.EnterMoveNextControl = true;
            this.txtORDER_NUMBER.Location = new System.Drawing.Point(76, 297);
            this.txtORDER_NUMBER.Name = "txtORDER_NUMBER";
            this.txtORDER_NUMBER.Properties.Mask.EditMask = "n";
            this.txtORDER_NUMBER.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtORDER_NUMBER.Properties.MaxLength = 5;
            this.txtORDER_NUMBER.Size = new System.Drawing.Size(369, 21);
            this.txtORDER_NUMBER.TabIndex = 5;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Hemo.Model.ConfigModel.MED_COMMON_ITEMLISTRow);
            // 
            // lab5
            // 
            this.lab5.Location = new System.Drawing.Point(13, 301);
            this.lab5.Name = "lab5";
            this.lab5.Size = new System.Drawing.Size(48, 14);
            this.lab5.TabIndex = 318;
            this.lab5.Text = "排序字段";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImageIndex = 3;
            this.btnClose.Location = new System.Drawing.Point(370, 337);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 25);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "取消(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(289, 337);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // lab4
            // 
            this.lab4.Location = new System.Drawing.Point(13, 269);
            this.lab4.Name = "lab4";
            this.lab4.Size = new System.Drawing.Size(24, 14);
            this.lab4.TabIndex = 316;
            this.lab4.Text = "状态";
            // 
            // txtITEM_VALUE
            // 
            this.txtITEM_VALUE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtITEM_VALUE.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ITEM_VALUE", true));
            this.txtITEM_VALUE.EnterMoveNextControl = true;
            this.txtITEM_VALUE.Location = new System.Drawing.Point(76, 36);
            this.txtITEM_VALUE.Name = "txtITEM_VALUE";
            this.txtITEM_VALUE.Properties.MaxLength = 25;
            this.txtITEM_VALUE.Size = new System.Drawing.Size(369, 21);
            this.txtITEM_VALUE.TabIndex = 1;
            // 
            // lab2
            // 
            this.lab2.Location = new System.Drawing.Point(13, 71);
            this.lab2.Name = "lab2";
            this.lab2.Size = new System.Drawing.Size(24, 14);
            this.lab2.TabIndex = 315;
            this.lab2.Text = "名称";
            // 
            // lab1
            // 
            this.lab1.Location = new System.Drawing.Point(13, 33);
            this.lab1.Name = "lab1";
            this.lab1.Size = new System.Drawing.Size(12, 14);
            this.lab1.TabIndex = 314;
            this.lab1.Text = "值";
            // 
            // txtITEM_NAME
            // 
            this.txtITEM_NAME.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtITEM_NAME.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ITEM_NAME", true));
            this.txtITEM_NAME.Location = new System.Drawing.Point(76, 66);
            this.txtITEM_NAME.Name = "txtITEM_NAME";
            this.txtITEM_NAME.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtITEM_NAME.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtITEM_NAME.Properties.Appearance.Options.UseFont = true;
            this.txtITEM_NAME.Properties.Appearance.Options.UseForeColor = true;
            this.txtITEM_NAME.Properties.MaxLength = 2000;
            this.txtITEM_NAME.Size = new System.Drawing.Size(369, 148);
            this.txtITEM_NAME.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 230);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 316;
            this.labelControl1.Text = "费用";
            // 
            // txtPrice
            // 
            this.txtPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrice.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "PRICE", true));
            this.txtPrice.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPrice.Location = new System.Drawing.Point(76, 234);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtPrice.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtPrice.Properties.Mask.EditMask = "d";
            this.txtPrice.Properties.MaxLength = 25;
            this.txtPrice.Size = new System.Drawing.Size(369, 21);
            this.txtPrice.TabIndex = 3;
            // 
            // txtID
            // 
            this.txtID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ITEM_ID", true));
            this.txtID.Location = new System.Drawing.Point(0, 125);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(0, 21);
            this.txtID.TabIndex = 319;
            // 
            // cbxSTATUS
            // 
            this.cbxSTATUS.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "STATUS", true));
            this.cbxSTATUS.EditValue = "1";
            this.cbxSTATUS.Location = new System.Drawing.Point(76, 264);
            this.cbxSTATUS.Name = "cbxSTATUS";
            this.cbxSTATUS.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "启用"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "停用")});
            this.cbxSTATUS.Size = new System.Drawing.Size(180, 24);
            this.cbxSTATUS.TabIndex = 4;
            // 
            // AccountItemConfigEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 374);
            this.Controls.Add(this.cbxSTATUS);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.txtORDER_NUMBER);
            this.Controls.Add(this.lab5);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lab4);
            this.Controls.Add(this.txtITEM_VALUE);
            this.Controls.Add(this.lab2);
            this.Controls.Add(this.lab1);
            this.Controls.Add(this.txtITEM_NAME);
            this.Controls.Add(this.txtPrice);
            this.Name = "AccountItemConfigEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "记帐项目维护";
            this.Load += new System.EventHandler(this.AccountItemConfigEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtORDER_NUMBER.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_VALUE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_NAME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxSTATUS.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtORDER_NUMBER;
        private DevExpress.XtraEditors.LabelControl lab5;
        private Controls.DXSimpleButton btnClose;
        private Controls.DXSimpleButton btnSave;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider;
        private DevExpress.XtraEditors.LabelControl lab4;
        private DevExpress.XtraEditors.TextEdit txtITEM_VALUE;
        private DevExpress.XtraEditors.LabelControl lab2;
        private DevExpress.XtraEditors.LabelControl lab1;
        private DevExpress.XtraEditors.MemoEdit txtITEM_NAME;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SpinEdit txtPrice;
        private DevExpress.XtraEditors.TextEdit txtID;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.RadioGroup cbxSTATUS;
    }
}