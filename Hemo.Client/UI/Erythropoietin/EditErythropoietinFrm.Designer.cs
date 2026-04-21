namespace Hemo.Client.UI.Erythropoietin
{
    partial class EditErythropoietinFrm
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
            this.cbxTIME_TYPE = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lab3 = new DevExpress.XtraEditors.LabelControl();
            this.lab2 = new DevExpress.XtraEditors.LabelControl();
            this.lab1 = new DevExpress.XtraEditors.LabelControl();
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.btnClose = new Hemo.Client.Controls.DXSimpleButton();
            this.lab4 = new DevExpress.XtraEditors.LabelControl();
            this.txtFREQUENCY = new DevExpress.XtraEditors.TextEdit();
            this.cbxDRUG_NAME = new DevExpress.XtraEditors.LookUpEdit();
            this.cbxQW = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbxHEMOGLOBIN_TYPE = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lab5 = new DevExpress.XtraEditors.LabelControl();
            this.lab6 = new DevExpress.XtraEditors.LabelControl();
            this.cbxDRUG_MODE = new DevExpress.XtraEditors.LookUpEdit();
            this.txtDOSAGE = new DevExpress.XtraEditors.TextEdit();
            this.lab7 = new DevExpress.XtraEditors.LabelControl();
            this.cbxUNIT = new DevExpress.XtraEditors.LookUpEdit();
            this.lab8 = new DevExpress.XtraEditors.LabelControl();
            this.lab9 = new DevExpress.XtraEditors.LabelControl();
            this.txtREMARK = new DevExpress.XtraEditors.MemoEdit();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxTIME_TYPE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFREQUENCY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxDRUG_NAME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxQW.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxHEMOGLOBIN_TYPE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxDRUG_MODE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDOSAGE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxUNIT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtREMARK.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxTIME_TYPE
            // 
            this.cbxTIME_TYPE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxTIME_TYPE.EditValue = "普通";
            this.cbxTIME_TYPE.EnterMoveNextControl = true;
            this.cbxTIME_TYPE.Location = new System.Drawing.Point(62, 70);
            this.cbxTIME_TYPE.Name = "cbxTIME_TYPE";
            this.cbxTIME_TYPE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxTIME_TYPE.Properties.Items.AddRange(new object[] {
            "普通",
            "单周",
            "双周"});
            this.cbxTIME_TYPE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxTIME_TYPE.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxTIME_TYPE.Size = new System.Drawing.Size(204, 21);
            this.cbxTIME_TYPE.TabIndex = 2;
            // 
            // lab3
            // 
            this.lab3.Location = new System.Drawing.Point(10, 73);
            this.lab3.Name = "lab3";
            this.lab3.Size = new System.Drawing.Size(48, 14);
            this.lab3.TabIndex = 304;
            this.lab3.Text = "时间类型";
            // 
            // lab2
            // 
            this.lab2.Location = new System.Drawing.Point(10, 46);
            this.lab2.Name = "lab2";
            this.lab2.Size = new System.Drawing.Size(21, 14);
            this.lab2.TabIndex = 301;
            this.lab2.Text = "QW";
            // 
            // lab1
            // 
            this.lab1.Location = new System.Drawing.Point(10, 17);
            this.lab1.Name = "lab1";
            this.lab1.Size = new System.Drawing.Size(24, 14);
            this.lab1.TabIndex = 300;
            this.lab1.Text = "药品";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(86, 324);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 27);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImageIndex = 3;
            this.btnClose.Location = new System.Drawing.Point(179, 324);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 27);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lab4
            // 
            this.lab4.Location = new System.Drawing.Point(10, 100);
            this.lab4.Name = "lab4";
            this.lab4.Size = new System.Drawing.Size(24, 14);
            this.lab4.TabIndex = 308;
            this.lab4.Text = "频次";
            // 
            // txtFREQUENCY
            // 
            this.txtFREQUENCY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFREQUENCY.EnterMoveNextControl = true;
            this.txtFREQUENCY.Location = new System.Drawing.Point(62, 97);
            this.txtFREQUENCY.Name = "txtFREQUENCY";
            this.txtFREQUENCY.Properties.Mask.EditMask = "n";
            this.txtFREQUENCY.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtFREQUENCY.Properties.MaxLength = 50;
            this.txtFREQUENCY.Size = new System.Drawing.Size(204, 21);
            this.txtFREQUENCY.TabIndex = 3;
            // 
            // cbxDRUG_NAME
            // 
            this.cbxDRUG_NAME.Location = new System.Drawing.Point(62, 13);
            this.cbxDRUG_NAME.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbxDRUG_NAME.Name = "cbxDRUG_NAME";
            this.cbxDRUG_NAME.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cbxDRUG_NAME.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cbxDRUG_NAME.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.cbxDRUG_NAME.Properties.Appearance.Options.UseBackColor = true;
            this.cbxDRUG_NAME.Properties.Appearance.Options.UseFont = true;
            this.cbxDRUG_NAME.Properties.Appearance.Options.UseForeColor = true;
            this.cbxDRUG_NAME.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxDRUG_NAME.Properties.NullText = "";
            this.cbxDRUG_NAME.Size = new System.Drawing.Size(204, 23);
            this.cbxDRUG_NAME.TabIndex = 0;
            // 
            // cbxQW
            // 
            this.cbxQW.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxQW.EditValue = "qw";
            this.cbxQW.EnterMoveNextControl = true;
            this.cbxQW.Location = new System.Drawing.Point(62, 43);
            this.cbxQW.Name = "cbxQW";
            this.cbxQW.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxQW.Properties.Items.AddRange(new object[] {
            "qw",
            "qow"});
            this.cbxQW.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxQW.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxQW.Size = new System.Drawing.Size(204, 21);
            this.cbxQW.TabIndex = 320;
            // 
            // cbxHEMOGLOBIN_TYPE
            // 
            this.cbxHEMOGLOBIN_TYPE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxHEMOGLOBIN_TYPE.EditValue = "长期";
            this.cbxHEMOGLOBIN_TYPE.EnterMoveNextControl = true;
            this.cbxHEMOGLOBIN_TYPE.Location = new System.Drawing.Point(62, 124);
            this.cbxHEMOGLOBIN_TYPE.Name = "cbxHEMOGLOBIN_TYPE";
            this.cbxHEMOGLOBIN_TYPE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxHEMOGLOBIN_TYPE.Properties.Items.AddRange(new object[] {
            "长期",
            "临时"});
            this.cbxHEMOGLOBIN_TYPE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxHEMOGLOBIN_TYPE.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxHEMOGLOBIN_TYPE.Size = new System.Drawing.Size(204, 21);
            this.cbxHEMOGLOBIN_TYPE.TabIndex = 4;
            // 
            // lab5
            // 
            this.lab5.Location = new System.Drawing.Point(10, 127);
            this.lab5.Name = "lab5";
            this.lab5.Size = new System.Drawing.Size(24, 14);
            this.lab5.TabIndex = 322;
            this.lab5.Text = "类型";
            // 
            // lab6
            // 
            this.lab6.Location = new System.Drawing.Point(10, 156);
            this.lab6.Name = "lab6";
            this.lab6.Size = new System.Drawing.Size(48, 14);
            this.lab6.TabIndex = 324;
            this.lab6.Text = "用药途径";
            // 
            // cbxDRUG_MODE
            // 
            this.cbxDRUG_MODE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxDRUG_MODE.EditValue = "";
            this.cbxDRUG_MODE.EnterMoveNextControl = true;
            this.cbxDRUG_MODE.Location = new System.Drawing.Point(62, 152);
            this.cbxDRUG_MODE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbxDRUG_MODE.Name = "cbxDRUG_MODE";
            this.cbxDRUG_MODE.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cbxDRUG_MODE.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.cbxDRUG_MODE.Properties.Appearance.Options.UseFont = true;
            this.cbxDRUG_MODE.Properties.Appearance.Options.UseForeColor = true;
            this.cbxDRUG_MODE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxDRUG_MODE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxDRUG_MODE.Properties.NullText = "";
            this.cbxDRUG_MODE.Size = new System.Drawing.Size(204, 23);
            this.cbxDRUG_MODE.TabIndex = 5;
            // 
            // txtDOSAGE
            // 
            this.txtDOSAGE.EditValue = "";
            this.txtDOSAGE.EnterMoveNextControl = true;
            this.txtDOSAGE.Location = new System.Drawing.Point(62, 183);
            this.txtDOSAGE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDOSAGE.Name = "txtDOSAGE";
            this.txtDOSAGE.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtDOSAGE.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtDOSAGE.Properties.Appearance.Options.UseFont = true;
            this.txtDOSAGE.Properties.Appearance.Options.UseForeColor = true;
            this.txtDOSAGE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtDOSAGE.Properties.MaxLength = 50;
            this.txtDOSAGE.Size = new System.Drawing.Size(204, 23);
            this.txtDOSAGE.TabIndex = 6;
            // 
            // lab7
            // 
            this.lab7.Location = new System.Drawing.Point(10, 187);
            this.lab7.Name = "lab7";
            this.lab7.Size = new System.Drawing.Size(24, 14);
            this.lab7.TabIndex = 327;
            this.lab7.Text = "剂量";
            // 
            // cbxUNIT
            // 
            this.cbxUNIT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxUNIT.EditValue = "";
            this.cbxUNIT.EnterMoveNextControl = true;
            this.cbxUNIT.Location = new System.Drawing.Point(62, 214);
            this.cbxUNIT.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbxUNIT.Name = "cbxUNIT";
            this.cbxUNIT.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cbxUNIT.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.cbxUNIT.Properties.Appearance.Options.UseFont = true;
            this.cbxUNIT.Properties.Appearance.Options.UseForeColor = true;
            this.cbxUNIT.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxUNIT.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxUNIT.Properties.NullText = "";
            this.cbxUNIT.Size = new System.Drawing.Size(204, 23);
            this.cbxUNIT.TabIndex = 7;
            // 
            // lab8
            // 
            this.lab8.Location = new System.Drawing.Point(10, 218);
            this.lab8.Name = "lab8";
            this.lab8.Size = new System.Drawing.Size(24, 14);
            this.lab8.TabIndex = 329;
            this.lab8.Text = "单位";
            // 
            // lab9
            // 
            this.lab9.Location = new System.Drawing.Point(10, 249);
            this.lab9.Name = "lab9";
            this.lab9.Size = new System.Drawing.Size(24, 14);
            this.lab9.TabIndex = 331;
            this.lab9.Text = "备注";
            // 
            // txtREMARK
            // 
            this.txtREMARK.EditValue = "";
            this.txtREMARK.Location = new System.Drawing.Point(62, 245);
            this.txtREMARK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtREMARK.Name = "txtREMARK";
            this.txtREMARK.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtREMARK.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtREMARK.Properties.Appearance.Options.UseFont = true;
            this.txtREMARK.Properties.Appearance.Options.UseForeColor = true;
            this.txtREMARK.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtREMARK.Properties.MaxLength = 500;
            this.txtREMARK.Size = new System.Drawing.Size(204, 57);
            this.txtREMARK.TabIndex = 8;
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEdit1.EditValue = "qw";
            this.comboBoxEdit1.EnterMoveNextControl = true;
            this.comboBoxEdit1.Location = new System.Drawing.Point(62, 43);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Properties.Items.AddRange(new object[] {
            "qw",
            "qow"});
            this.comboBoxEdit1.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit1.Size = new System.Drawing.Size(204, 21);
            this.comboBoxEdit1.TabIndex = 1;
            // 
            // EditErythropoietinFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 363);
            this.Controls.Add(this.lab9);
            this.Controls.Add(this.lab8);
            this.Controls.Add(this.cbxUNIT);
            this.Controls.Add(this.lab7);
            this.Controls.Add(this.txtDOSAGE);
            this.Controls.Add(this.cbxDRUG_MODE);
            this.Controls.Add(this.lab6);
            this.Controls.Add(this.cbxHEMOGLOBIN_TYPE);
            this.Controls.Add(this.lab5);
            this.Controls.Add(this.comboBoxEdit1);
            this.Controls.Add(this.cbxQW);
            this.Controls.Add(this.cbxDRUG_NAME);
            this.Controls.Add(this.txtFREQUENCY);
            this.Controls.Add(this.lab4);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbxTIME_TYPE);
            this.Controls.Add(this.lab3);
            this.Controls.Add(this.lab2);
            this.Controls.Add(this.lab1);
            this.Controls.Add(this.txtREMARK);
            this.MinimumSize = new System.Drawing.Size(294, 401);
            this.Name = "EditErythropoietinFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "维护促红素信息";
            this.Load += new System.EventHandler(this.EditErythropoietinFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cbxTIME_TYPE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFREQUENCY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxDRUG_NAME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxQW.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxHEMOGLOBIN_TYPE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxDRUG_MODE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDOSAGE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxUNIT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtREMARK.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit cbxTIME_TYPE;
        private DevExpress.XtraEditors.LabelControl lab3;
        private DevExpress.XtraEditors.LabelControl lab2;
        private DevExpress.XtraEditors.LabelControl lab1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider;
        private Hemo.Client.Controls.DXSimpleButton btnSave;
        private Hemo.Client.Controls.DXSimpleButton btnClose;
        private DevExpress.XtraEditors.TextEdit txtFREQUENCY;
        private DevExpress.XtraEditors.LabelControl lab4;
        private DevExpress.XtraEditors.LookUpEdit cbxDRUG_NAME;
        private DevExpress.XtraEditors.ComboBoxEdit cbxQW;
        private DevExpress.XtraEditors.ComboBoxEdit cbxHEMOGLOBIN_TYPE;
        private DevExpress.XtraEditors.LabelControl lab5;
        private DevExpress.XtraEditors.LabelControl lab6;
        private DevExpress.XtraEditors.LookUpEdit cbxDRUG_MODE;
        private DevExpress.XtraEditors.LabelControl lab7;
        private DevExpress.XtraEditors.TextEdit txtDOSAGE;
        private DevExpress.XtraEditors.LabelControl lab8;
        private DevExpress.XtraEditors.LookUpEdit cbxUNIT;
        private DevExpress.XtraEditors.LabelControl lab9;
        private DevExpress.XtraEditors.MemoEdit txtREMARK;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;

    }
}