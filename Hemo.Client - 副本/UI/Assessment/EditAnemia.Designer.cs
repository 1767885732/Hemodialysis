namespace Hemo.Client.UI.Assessment
{
    partial class EditAnemia
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
            this.txtXHDB = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtXDB = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtXQT = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtZTJHL = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.btnExit = new Hemo.Client.Controls.DXSimpleButton();
            this.dxSimpleButton1 = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtXHDB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtXDB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtXQT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtZTJHL.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtXHDB
            // 
            this.txtXHDB.Location = new System.Drawing.Point(81, 6);
            this.txtXHDB.Name = "txtXHDB";
            this.txtXHDB.Size = new System.Drawing.Size(100, 20);
            this.txtXHDB.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(30, 9);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "血红蛋白";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(187, 9);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(18, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "g/L";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(425, 10);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(31, 14);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "ng/ml";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(279, 10);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 14);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "铁蛋白";
            // 
            // txtXDB
            // 
            this.txtXDB.Location = new System.Drawing.Point(319, 7);
            this.txtXDB.Name = "txtXDB";
            this.txtXDB.Size = new System.Drawing.Size(100, 20);
            this.txtXDB.TabIndex = 3;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(187, 48);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(37, 14);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "umol/L";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(37, 47);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(36, 14);
            this.labelControl6.TabIndex = 7;
            this.labelControl6.Text = "血清铁";
            // 
            // txtXQT
            // 
            this.txtXQT.Location = new System.Drawing.Point(81, 45);
            this.txtXQT.Name = "txtXQT";
            this.txtXQT.Size = new System.Drawing.Size(100, 20);
            this.txtXQT.TabIndex = 6;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(425, 46);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(37, 14);
            this.labelControl7.TabIndex = 11;
            this.labelControl7.Text = "umol/L";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(253, 46);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(60, 14);
            this.labelControl8.TabIndex = 10;
            this.labelControl8.Text = "总铁结合力";
            // 
            // txtZTJHL
            // 
            this.txtZTJHL.Location = new System.Drawing.Point(319, 43);
            this.txtZTJHL.Name = "txtZTJHL";
            this.txtZTJHL.Size = new System.Drawing.Size(100, 20);
            this.txtZTJHL.TabIndex = 9;
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(330, 72);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(76, 23);
            this.btnSave.TabIndex = 41;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.ImageIndex = 3;
            this.btnExit.Location = new System.Drawing.Point(412, 71);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.TabIndex = 42;
            this.btnExit.Text = "关闭(&C)";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dxSimpleButton1
            // 
            this.dxSimpleButton1.ImageIndex = 14;
            this.dxSimpleButton1.Location = new System.Drawing.Point(227, 73);
            this.dxSimpleButton1.Name = "dxSimpleButton1";
            this.dxSimpleButton1.Size = new System.Drawing.Size(88, 23);
            this.dxSimpleButton1.TabIndex = 43;
            this.dxSimpleButton1.Text = "检验数据";
            this.dxSimpleButton1.Click += new System.EventHandler(this.dxSimpleButton1_Click);
            // 
            // EditAnemia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 101);
            this.Controls.Add(this.dxSimpleButton1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.txtZTJHL);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.txtXQT);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.txtXDB);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtXHDB);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditAnemia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "贫血评估";
            this.Load += new System.EventHandler(this.EditCKDMBD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtXHDB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtXDB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtXQT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtZTJHL.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtXHDB;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtXDB;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtXQT;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtZTJHL;
        private Controls.DXSimpleButton btnSave;
        private Controls.DXSimpleButton btnExit;
        private Controls.DXSimpleButton dxSimpleButton1;
    }
}