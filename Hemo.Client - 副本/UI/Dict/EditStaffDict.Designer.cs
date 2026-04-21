namespace Hemo.Client.UI.Dict
{
    partial class EditStaffDict
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
            this.cbxDEPT = new DevExpress.XtraEditors.LookUpEdit();
            this.txtNAME = new DevExpress.XtraEditors.TextEdit();
            this.lab2 = new DevExpress.XtraEditors.LabelControl();
            this.txtINPUT_CODE = new DevExpress.XtraEditors.TextEdit();
            this.lab3 = new DevExpress.XtraEditors.LabelControl();
            this.cbxJOB = new DevExpress.XtraEditors.LookUpEdit();
            this.lab4 = new DevExpress.XtraEditors.LabelControl();
            this.cbxTITLE = new DevExpress.XtraEditors.LookUpEdit();
            this.lab5 = new DevExpress.XtraEditors.LabelControl();
            this.cbxUSER = new DevExpress.XtraEditors.LookUpEdit();
            this.lab6 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.btnExit = new Hemo.Client.Controls.DXSimpleButton();
            this.cbxIS_DELETE = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.cbxDEPT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNAME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtINPUT_CODE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxJOB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxTITLE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxUSER.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxIS_DELETE.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lab1
            // 
            this.lab1.Location = new System.Drawing.Point(12, 15);
            this.lab1.Name = "lab1";
            this.lab1.Size = new System.Drawing.Size(96, 14);
            this.lab1.TabIndex = 327;
            this.lab1.Text = "工作人员所在科室";
            // 
            // cbxDEPT
            // 
            this.cbxDEPT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxDEPT.EditValue = "";
            this.cbxDEPT.Location = new System.Drawing.Point(114, 12);
            this.cbxDEPT.Name = "cbxDEPT";
            this.cbxDEPT.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxDEPT.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxDEPT.Size = new System.Drawing.Size(238, 21);
            this.cbxDEPT.TabIndex = 328;
            // 
            // txtNAME
            // 
            this.txtNAME.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNAME.Location = new System.Drawing.Point(114, 39);
            this.txtNAME.Name = "txtNAME";
            this.txtNAME.Properties.MaxLength = 10;
            this.txtNAME.Size = new System.Drawing.Size(238, 21);
            this.txtNAME.TabIndex = 329;
            this.txtNAME.Leave += new System.EventHandler(this.txtNAME_Leave);
            // 
            // lab2
            // 
            this.lab2.Location = new System.Drawing.Point(12, 42);
            this.lab2.Name = "lab2";
            this.lab2.Size = new System.Drawing.Size(24, 14);
            this.lab2.TabIndex = 330;
            this.lab2.Text = "姓名";
            // 
            // txtINPUT_CODE
            // 
            this.txtINPUT_CODE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtINPUT_CODE.Location = new System.Drawing.Point(114, 66);
            this.txtINPUT_CODE.Name = "txtINPUT_CODE";
            this.txtINPUT_CODE.Properties.MaxLength = 5;
            this.txtINPUT_CODE.Size = new System.Drawing.Size(238, 21);
            this.txtINPUT_CODE.TabIndex = 331;
            // 
            // lab3
            // 
            this.lab3.Location = new System.Drawing.Point(12, 69);
            this.lab3.Name = "lab3";
            this.lab3.Size = new System.Drawing.Size(72, 14);
            this.lab3.TabIndex = 332;
            this.lab3.Text = "姓名的输入码";
            // 
            // cbxJOB
            // 
            this.cbxJOB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxJOB.EditValue = "";
            this.cbxJOB.Location = new System.Drawing.Point(114, 93);
            this.cbxJOB.Name = "cbxJOB";
            this.cbxJOB.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxJOB.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxJOB.Size = new System.Drawing.Size(238, 21);
            this.cbxJOB.TabIndex = 334;
            // 
            // lab4
            // 
            this.lab4.Location = new System.Drawing.Point(12, 96);
            this.lab4.Name = "lab4";
            this.lab4.Size = new System.Drawing.Size(24, 14);
            this.lab4.TabIndex = 333;
            this.lab4.Text = "职业";
            // 
            // cbxTITLE
            // 
            this.cbxTITLE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxTITLE.EditValue = "";
            this.cbxTITLE.Location = new System.Drawing.Point(114, 120);
            this.cbxTITLE.Name = "cbxTITLE";
            this.cbxTITLE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxTITLE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxTITLE.Size = new System.Drawing.Size(238, 21);
            this.cbxTITLE.TabIndex = 336;
            // 
            // lab5
            // 
            this.lab5.Location = new System.Drawing.Point(12, 123);
            this.lab5.Name = "lab5";
            this.lab5.Size = new System.Drawing.Size(24, 14);
            this.lab5.TabIndex = 335;
            this.lab5.Text = "职称";
            // 
            // cbxUSER
            // 
            this.cbxUSER.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxUSER.EditValue = "";
            this.cbxUSER.Location = new System.Drawing.Point(114, 147);
            this.cbxUSER.Name = "cbxUSER";
            this.cbxUSER.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxUSER.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxUSER.Size = new System.Drawing.Size(238, 21);
            this.cbxUSER.TabIndex = 338;
            // 
            // lab6
            // 
            this.lab6.Location = new System.Drawing.Point(12, 150);
            this.lab6.Name = "lab6";
            this.lab6.Size = new System.Drawing.Size(72, 14);
            this.lab6.TabIndex = 337;
            this.lab6.Text = "本系统用户名";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(114, 225);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 339;
            this.btnSave.Text = "保存(&S) ";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.ImageIndex = 3;
            this.btnExit.Location = new System.Drawing.Point(229, 225);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.TabIndex = 340;
            this.btnExit.Text = "取消(&C) ";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cbxIS_DELETE
            // 
            this.cbxIS_DELETE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxIS_DELETE.EditValue = "启用";
            this.cbxIS_DELETE.EnterMoveNextControl = true;
            this.cbxIS_DELETE.Location = new System.Drawing.Point(114, 174);
            this.cbxIS_DELETE.Margin = new System.Windows.Forms.Padding(0);
            this.cbxIS_DELETE.Name = "cbxIS_DELETE";
            this.cbxIS_DELETE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxIS_DELETE.Properties.Items.AddRange(new object[] {
            "停用",
            "启用"});
            this.cbxIS_DELETE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxIS_DELETE.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxIS_DELETE.Size = new System.Drawing.Size(238, 21);
            this.cbxIS_DELETE.TabIndex = 341;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 177);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 342;
            this.labelControl1.Text = "状态";
            // 
            // EditStaffDict
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 268);
            this.Controls.Add(this.cbxIS_DELETE);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbxUSER);
            this.Controls.Add(this.lab6);
            this.Controls.Add(this.cbxTITLE);
            this.Controls.Add(this.lab5);
            this.Controls.Add(this.cbxJOB);
            this.Controls.Add(this.lab4);
            this.Controls.Add(this.txtINPUT_CODE);
            this.Controls.Add(this.lab3);
            this.Controls.Add(this.txtNAME);
            this.Controls.Add(this.lab2);
            this.Controls.Add(this.cbxDEPT);
            this.Controls.Add(this.lab1);
            this.MinimumSize = new System.Drawing.Size(383, 274);
            this.Name = "EditStaffDict";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "维护资料设定信息";
            this.Load += new System.EventHandler(this.EditStaffDict_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cbxDEPT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNAME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtINPUT_CODE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxJOB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxTITLE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxUSER.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxIS_DELETE.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lab1;
        private DevExpress.XtraEditors.LookUpEdit cbxDEPT;
        private DevExpress.XtraEditors.TextEdit txtNAME;
        private DevExpress.XtraEditors.LabelControl lab2;
        private DevExpress.XtraEditors.TextEdit txtINPUT_CODE;
        private DevExpress.XtraEditors.LabelControl lab3;
        private DevExpress.XtraEditors.LookUpEdit cbxJOB;
        private DevExpress.XtraEditors.LabelControl lab4;
        private DevExpress.XtraEditors.LookUpEdit cbxTITLE;
        private DevExpress.XtraEditors.LabelControl lab5;
        private DevExpress.XtraEditors.LookUpEdit cbxUSER;
        private DevExpress.XtraEditors.LabelControl lab6;
        private Hemo.Client.Controls.DXSimpleButton btnSave;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider;
        private Hemo.Client.Controls.DXSimpleButton btnExit;
        private DevExpress.XtraEditors.ComboBoxEdit cbxIS_DELETE;
        private DevExpress.XtraEditors.LabelControl labelControl1;

    }
}