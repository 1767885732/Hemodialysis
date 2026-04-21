namespace Hemo.Client.UI.Machine
{
    partial class EditMachine
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
            this.components = new System.ComponentModel.Container();
            this.btnClose = new Hemo.Client.Controls.DXSimpleButton();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.lab2 = new DevExpress.XtraEditors.LabelControl();
            this.lab1 = new DevExpress.XtraEditors.LabelControl();
            this.txtMACHINE_NAME = new DevExpress.XtraEditors.TextEdit();
            this.cbxTYPE = new DevExpress.XtraEditors.LookUpEdit();
            this.cbxTHERAPEUTIC_PROPERTIES = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.lab5 = new DevExpress.XtraEditors.LabelControl();
            this.lab6 = new DevExpress.XtraEditors.LabelControl();
            this.txtOTHER_THERAPEUTIC = new DevExpress.XtraEditors.TextEdit();
            this.lab3 = new DevExpress.XtraEditors.LabelControl();
            this.txtMACHINE_MODEL = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtSERVICE_ENGINEER = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtPHONE_NO = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.deSETUP_DATE = new DevExpress.XtraEditors.DateEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtMACHINE_SERIAL_NO = new DevExpress.XtraEditors.SpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMACHINE_NAME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxTYPE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxTHERAPEUTIC_PROPERTIES.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOTHER_THERAPEUTIC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMACHINE_MODEL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSERVICE_ENGINEER.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPHONE_NO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deSETUP_DATE.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deSETUP_DATE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMACHINE_SERIAL_NO.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImageIndex = 3;
            this.btnClose.Location = new System.Drawing.Point(268, 272);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 27);
            this.btnClose.TabIndex = 355;
            this.btnClose.Text = "取消(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(177, 272);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 27);
            this.btnSave.TabIndex = 353;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // lab2
            // 
            this.lab2.Location = new System.Drawing.Point(12, 42);
            this.lab2.Name = "lab2";
            this.lab2.Size = new System.Drawing.Size(60, 14);
            this.lab2.TabIndex = 344;
            this.lab2.Text = "透析机分类";
            // 
            // lab1
            // 
            this.lab1.Location = new System.Drawing.Point(12, 15);
            this.lab1.Name = "lab1";
            this.lab1.Size = new System.Drawing.Size(60, 14);
            this.lab1.TabIndex = 341;
            this.lab1.Text = "透析机编号";
            // 
            // txtMACHINE_NAME
            // 
            this.txtMACHINE_NAME.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMACHINE_NAME.Location = new System.Drawing.Point(115, 12);
            this.txtMACHINE_NAME.Name = "txtMACHINE_NAME";
            this.txtMACHINE_NAME.Properties.MaxLength = 20;
            this.txtMACHINE_NAME.Size = new System.Drawing.Size(238, 20);
            this.txtMACHINE_NAME.TabIndex = 1;
            // 
            // cbxTYPE
            // 
            this.cbxTYPE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxTYPE.EditValue = "";
            this.cbxTYPE.Location = new System.Drawing.Point(115, 39);
            this.cbxTYPE.Name = "cbxTYPE";
            this.cbxTYPE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxTYPE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxTYPE.Properties.NullText = "";
            this.cbxTYPE.Size = new System.Drawing.Size(238, 20);
            this.cbxTYPE.TabIndex = 2;
            // 
            // cbxTHERAPEUTIC_PROPERTIES
            // 
            this.cbxTHERAPEUTIC_PROPERTIES.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxTHERAPEUTIC_PROPERTIES.EditValue = "";
            this.cbxTHERAPEUTIC_PROPERTIES.Location = new System.Drawing.Point(115, 93);
            this.cbxTHERAPEUTIC_PROPERTIES.Name = "cbxTHERAPEUTIC_PROPERTIES";
            this.cbxTHERAPEUTIC_PROPERTIES.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxTHERAPEUTIC_PROPERTIES.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("HD", "HD"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("HDF", "HDF"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("HF", "HF"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("HP", "HP"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("HD+HP", "HD+HP"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("CRRT", "CRRT")});
            this.cbxTHERAPEUTIC_PROPERTIES.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxTHERAPEUTIC_PROPERTIES.Properties.NullText = "[EditValue is null]";
            this.cbxTHERAPEUTIC_PROPERTIES.Size = new System.Drawing.Size(238, 20);
            this.cbxTHERAPEUTIC_PROPERTIES.TabIndex = 356;
            // 
            // lab5
            // 
            this.lab5.Location = new System.Drawing.Point(12, 96);
            this.lab5.Name = "lab5";
            this.lab5.Size = new System.Drawing.Size(48, 14);
            this.lab5.TabIndex = 357;
            this.lab5.Text = "治疗特征";
            // 
            // lab6
            // 
            this.lab6.Location = new System.Drawing.Point(12, 123);
            this.lab6.Name = "lab6";
            this.lab6.Size = new System.Drawing.Size(72, 14);
            this.lab6.TabIndex = 358;
            this.lab6.Text = "采集绑定标示";
            // 
            // txtOTHER_THERAPEUTIC
            // 
            this.txtOTHER_THERAPEUTIC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOTHER_THERAPEUTIC.Location = new System.Drawing.Point(115, 120);
            this.txtOTHER_THERAPEUTIC.Name = "txtOTHER_THERAPEUTIC";
            this.txtOTHER_THERAPEUTIC.Properties.MaxLength = 20;
            this.txtOTHER_THERAPEUTIC.Size = new System.Drawing.Size(238, 20);
            this.txtOTHER_THERAPEUTIC.TabIndex = 359;
            // 
            // lab3
            // 
            this.lab3.Location = new System.Drawing.Point(12, 69);
            this.lab3.Name = "lab3";
            this.lab3.Size = new System.Drawing.Size(60, 14);
            this.lab3.TabIndex = 346;
            this.lab3.Text = "透析机型号";
            // 
            // txtMACHINE_MODEL
            // 
            this.txtMACHINE_MODEL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMACHINE_MODEL.Location = new System.Drawing.Point(115, 66);
            this.txtMACHINE_MODEL.Name = "txtMACHINE_MODEL";
            this.txtMACHINE_MODEL.Properties.MaxLength = 20;
            this.txtMACHINE_MODEL.Size = new System.Drawing.Size(238, 20);
            this.txtMACHINE_MODEL.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 150);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 360;
            this.labelControl1.Text = "安装日期";
            // 
            // txtSERVICE_ENGINEER
            // 
            this.txtSERVICE_ENGINEER.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSERVICE_ENGINEER.Location = new System.Drawing.Point(115, 174);
            this.txtSERVICE_ENGINEER.Name = "txtSERVICE_ENGINEER";
            this.txtSERVICE_ENGINEER.Properties.MaxLength = 20;
            this.txtSERVICE_ENGINEER.Size = new System.Drawing.Size(238, 20);
            this.txtSERVICE_ENGINEER.TabIndex = 363;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 177);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 362;
            this.labelControl2.Text = "维修工程师";
            // 
            // txtPHONE_NO
            // 
            this.txtPHONE_NO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPHONE_NO.Location = new System.Drawing.Point(115, 201);
            this.txtPHONE_NO.Name = "txtPHONE_NO";
            this.txtPHONE_NO.Properties.MaxLength = 20;
            this.txtPHONE_NO.Size = new System.Drawing.Size(238, 20);
            this.txtPHONE_NO.TabIndex = 365;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 204);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 14);
            this.labelControl3.TabIndex = 364;
            this.labelControl3.Text = "电话";
            // 
            // deSETUP_DATE
            // 
            this.deSETUP_DATE.EditValue = null;
            this.deSETUP_DATE.Location = new System.Drawing.Point(115, 147);
            this.deSETUP_DATE.Name = "deSETUP_DATE";
            this.deSETUP_DATE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deSETUP_DATE.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deSETUP_DATE.Size = new System.Drawing.Size(238, 20);
            this.deSETUP_DATE.TabIndex = 366;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 236);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 364;
            this.labelControl4.Text = "机器序号";
            // 
            // txtMACHINE_SERIAL_NO
            // 
            this.txtMACHINE_SERIAL_NO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMACHINE_SERIAL_NO.Location = new System.Drawing.Point(115, 233);
            this.txtMACHINE_SERIAL_NO.Name = "txtMACHINE_SERIAL_NO";
            this.txtMACHINE_SERIAL_NO.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtMACHINE_SERIAL_NO.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtMACHINE_SERIAL_NO.Properties.Mask.EditMask = "n";
            this.txtMACHINE_SERIAL_NO.Properties.MaxLength = 10;
            this.txtMACHINE_SERIAL_NO.Size = new System.Drawing.Size(238, 20);
            this.txtMACHINE_SERIAL_NO.TabIndex = 367;
            // 
            // EditMachine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 311);
            this.Controls.Add(this.deSETUP_DATE);
            this.Controls.Add(this.txtPHONE_NO);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtSERVICE_ENGINEER);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtOTHER_THERAPEUTIC);
            this.Controls.Add(this.lab6);
            this.Controls.Add(this.lab5);
            this.Controls.Add(this.cbxTYPE);
            this.Controls.Add(this.txtMACHINE_NAME);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtMACHINE_MODEL);
            this.Controls.Add(this.lab3);
            this.Controls.Add(this.lab2);
            this.Controls.Add(this.lab1);
            this.Controls.Add(this.cbxTHERAPEUTIC_PROPERTIES);
            this.Controls.Add(this.txtMACHINE_SERIAL_NO);
            this.Name = "EditMachine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "维护血透机信息";
            this.Load += new System.EventHandler(this.EditMachine_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMACHINE_NAME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxTYPE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxTHERAPEUTIC_PROPERTIES.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOTHER_THERAPEUTIC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMACHINE_MODEL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSERVICE_ENGINEER.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPHONE_NO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deSETUP_DATE.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deSETUP_DATE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMACHINE_SERIAL_NO.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Hemo.Client.Controls.DXSimpleButton btnClose;
        private Hemo.Client.Controls.DXSimpleButton btnSave;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider;
        private DevExpress.XtraEditors.LabelControl lab2;
        private DevExpress.XtraEditors.LabelControl lab1;
        private DevExpress.XtraEditors.TextEdit txtMACHINE_NAME;
        private DevExpress.XtraEditors.LookUpEdit cbxTYPE;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cbxTHERAPEUTIC_PROPERTIES;
        private DevExpress.XtraEditors.LabelControl lab5;
        private DevExpress.XtraEditors.TextEdit txtOTHER_THERAPEUTIC;
        private DevExpress.XtraEditors.LabelControl lab6;
        private DevExpress.XtraEditors.TextEdit txtMACHINE_MODEL;
        private DevExpress.XtraEditors.LabelControl lab3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtSERVICE_ENGINEER;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtPHONE_NO;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit deSETUP_DATE;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SpinEdit txtMACHINE_SERIAL_NO;
    }
}