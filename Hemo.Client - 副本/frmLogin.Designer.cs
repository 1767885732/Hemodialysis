namespace Hemo.WinForm
{
    partial class frmLogin
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new Hemo.Client.Controls.DXSimpleButton();
            this.btnLogin = new Hemo.Client.Controls.DXSimpleButton();
            this.devtxtPwd = new DevExpress.XtraEditors.TextEdit();
            this.devtxtLoginName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.picLoading = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.devtxtPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.devtxtLoginName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl2.Location = new System.Drawing.Point(179, 176);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 17;
            this.labelControl2.Text = "密   码：";
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl1.Location = new System.Drawing.Point(179, 145);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 18;
            this.labelControl1.Text = "用户名：";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.Location = new System.Drawing.Point(320, 211);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLogin.Location = new System.Drawing.Point(235, 211);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 14;
            this.btnLogin.Text = "登录";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // devtxtPwd
            // 
            this.devtxtPwd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.devtxtPwd.EditValue = "";
            this.devtxtPwd.Location = new System.Drawing.Point(235, 172);
            this.devtxtPwd.Name = "devtxtPwd";
            this.devtxtPwd.Properties.PasswordChar = '*';
            this.devtxtPwd.Size = new System.Drawing.Size(160, 21);
            this.devtxtPwd.TabIndex = 13;
            this.devtxtPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.devtxtPwd_KeyPress);
            // 
            // devtxtLoginName
            // 
            this.devtxtLoginName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.devtxtLoginName.EditValue = "";
            this.devtxtLoginName.Location = new System.Drawing.Point(235, 145);
            this.devtxtLoginName.Name = "devtxtLoginName";
            this.devtxtLoginName.Size = new System.Drawing.Size(160, 21);
            this.devtxtLoginName.TabIndex = 12;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("黑体", 16F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.Location = new System.Drawing.Point(120, 79);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(326, 29);
            this.labelControl3.TabIndex = 19;
            this.labelControl3.Text = "血液净化信息系统V3.0";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Hemo.Client.Properties.Resources.logo;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.Location = new System.Drawing.Point(13, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(159, 55);
            this.panel1.TabIndex = 20;
            // 
            // picLoading
            // 
            this.picLoading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picLoading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picLoading.EditValue = global::Hemo.Client.Properties.Resources._257;
            this.picLoading.Location = new System.Drawing.Point(401, 153);
            this.picLoading.Name = "picLoading";
            this.picLoading.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picLoading.Properties.Appearance.Options.UseBackColor = true;
            this.picLoading.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picLoading.Size = new System.Drawing.Size(44, 40);
            this.picLoading.TabIndex = 16;
            this.picLoading.Visible = false;
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Stretch;
            this.BackgroundImageStore = global::Hemo.Client.Properties.Resources.home11;
            this.ClientSize = new System.Drawing.Size(590, 330);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.picLoading);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.devtxtPwd);
            this.Controls.Add(this.devtxtLoginName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(590, 330);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(590, 330);
            this.Name = "frmLogin";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.devtxtPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.devtxtLoginName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PictureEdit picLoading;
        private Hemo.Client.Controls.DXSimpleButton btnCancel;
        private Hemo.Client.Controls.DXSimpleButton btnLogin;
        private DevExpress.XtraEditors.TextEdit devtxtPwd;
        private DevExpress.XtraEditors.TextEdit devtxtLoginName;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}