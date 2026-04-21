namespace Hemo.Client.UI.Config
{
    partial class ScreenItemConfigConfigEdit
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
            this.txtID = new DevExpress.XtraEditors.TextEdit();
            this.cbxSTATUS = new DevExpress.XtraEditors.RadioGroup();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnChose = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.busyIndicator1 = new Hemo.Client.Controls.BusyIndicator();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtFileDictionary = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtORDER_NUMBER.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_VALUE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_NAME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxSTATUS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileDictionary.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtORDER_NUMBER
            // 
            this.txtORDER_NUMBER.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtORDER_NUMBER.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ORDER_NUMBER", true));
            this.txtORDER_NUMBER.EnterMoveNextControl = true;
            this.txtORDER_NUMBER.Location = new System.Drawing.Point(77, 183);
            this.txtORDER_NUMBER.Name = "txtORDER_NUMBER";
            this.txtORDER_NUMBER.Properties.Mask.EditMask = "n";
            this.txtORDER_NUMBER.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtORDER_NUMBER.Properties.MaxLength = 5;
            this.txtORDER_NUMBER.Size = new System.Drawing.Size(251, 20);
            this.txtORDER_NUMBER.TabIndex = 5;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Hemo.Model.ConfigModel.MED_COMMON_ITEMLISTRow);
            // 
            // lab5
            // 
            this.lab5.Location = new System.Drawing.Point(13, 9);
            this.lab5.Name = "lab5";
            this.lab5.Size = new System.Drawing.Size(48, 14);
            this.lab5.TabIndex = 318;
            this.lab5.Text = "上传文件";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImageIndex = 3;
            this.btnClose.Location = new System.Drawing.Point(384, 11);
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
            this.btnSave.Location = new System.Drawing.Point(303, 11);
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
            this.lab4.Location = new System.Drawing.Point(14, 150);
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
            this.txtITEM_VALUE.Location = new System.Drawing.Point(77, 12);
            this.txtITEM_VALUE.Name = "txtITEM_VALUE";
            this.txtITEM_VALUE.Properties.MaxLength = 25;
            this.txtITEM_VALUE.Size = new System.Drawing.Size(161, 20);
            this.txtITEM_VALUE.TabIndex = 1;
            this.txtITEM_VALUE.EditValueChanged += new System.EventHandler(this.txtITEM_VALUE_EditValueChanged);
            // 
            // lab2
            // 
            this.lab2.Location = new System.Drawing.Point(24, 43);
            this.lab2.Name = "lab2";
            this.lab2.Size = new System.Drawing.Size(24, 14);
            this.lab2.TabIndex = 315;
            this.lab2.Text = "名称";
            // 
            // lab1
            // 
            this.lab1.Location = new System.Drawing.Point(24, 12);
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
            this.txtITEM_NAME.Enabled = false;
            this.txtITEM_NAME.Location = new System.Drawing.Point(77, 39);
            this.txtITEM_NAME.Name = "txtITEM_NAME";
            this.txtITEM_NAME.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtITEM_NAME.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtITEM_NAME.Properties.Appearance.Options.UseFont = true;
            this.txtITEM_NAME.Properties.Appearance.Options.UseForeColor = true;
            this.txtITEM_NAME.Properties.MaxLength = 2000;
            this.txtITEM_NAME.Size = new System.Drawing.Size(382, 100);
            this.txtITEM_NAME.TabIndex = 2;
            // 
            // txtID
            // 
            this.txtID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ITEM_ID", true));
            this.txtID.Location = new System.Drawing.Point(0, 125);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(0, 20);
            this.txtID.TabIndex = 319;
            // 
            // cbxSTATUS
            // 
            this.cbxSTATUS.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "STATUS", true));
            this.cbxSTATUS.EditValue = "1";
            this.cbxSTATUS.Enabled = false;
            this.cbxSTATUS.Location = new System.Drawing.Point(77, 145);
            this.cbxSTATUS.Name = "cbxSTATUS";
            this.cbxSTATUS.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "启用"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "停用")});
            this.cbxSTATUS.Size = new System.Drawing.Size(180, 29);
            this.cbxSTATUS.TabIndex = 4;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 346);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(471, 41);
            this.panelControl1.TabIndex = 320;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnChose);
            this.panelControl2.Controls.Add(this.lab5);
            this.panelControl2.Controls.Add(this.txtFileDictionary);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 229);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(471, 117);
            this.panelControl2.TabIndex = 321;
            // 
            // btnChose
            // 
            this.btnChose.Location = new System.Drawing.Point(337, 81);
            this.btnChose.Name = "btnChose";
            this.btnChose.Size = new System.Drawing.Size(60, 23);
            this.btnChose.TabIndex = 319;
            this.btnChose.Text = "选择";
            this.btnChose.Click += new System.EventHandler(this.btnChose_Click);
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.busyIndicator1);
            this.panelControl3.Controls.Add(this.txtORDER_NUMBER);
            this.panelControl3.Controls.Add(this.labelControl2);
            this.panelControl3.Controls.Add(this.lab4);
            this.panelControl3.Controls.Add(this.cbxSTATUS);
            this.panelControl3.Controls.Add(this.lab1);
            this.panelControl3.Controls.Add(this.txtITEM_VALUE);
            this.panelControl3.Controls.Add(this.txtITEM_NAME);
            this.panelControl3.Controls.Add(this.lab2);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(471, 229);
            this.panelControl3.TabIndex = 322;
            // 
            // busyIndicator1
            // 
            this.busyIndicator1.Location = new System.Drawing.Point(198, 99);
            this.busyIndicator1.Name = "busyIndicator1";
            this.busyIndicator1.Size = new System.Drawing.Size(100, 40);
            this.busyIndicator1.TabIndex = 321;
            this.busyIndicator1.Visible = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(14, 186);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 320;
            this.labelControl2.Text = "排序字段";
            // 
            // txtFileDictionary
            // 
            this.txtFileDictionary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileDictionary.Location = new System.Drawing.Point(77, 7);
            this.txtFileDictionary.Name = "txtFileDictionary";
            this.txtFileDictionary.Properties.MaxLength = 5;
            this.txtFileDictionary.Size = new System.Drawing.Size(241, 97);
            this.txtFileDictionary.TabIndex = 319;
            // 
            // ScreenItemConfigConfigEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 387);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.txtID);
            this.MinimumSize = new System.Drawing.Size(487, 332);
            this.Name = "ScreenItemConfigConfigEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "大屏配置项维护";
            this.Load += new System.EventHandler(this.ScreenItemConfigConfigEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtORDER_NUMBER.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_VALUE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_NAME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxSTATUS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileDictionary.Properties)).EndInit();
            this.ResumeLayout(false);

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
        private DevExpress.XtraEditors.TextEdit txtID;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.RadioGroup cbxSTATUS;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnChose;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private Controls.BusyIndicator busyIndicator1;
        private DevExpress.XtraEditors.MemoEdit txtFileDictionary;
    }
}