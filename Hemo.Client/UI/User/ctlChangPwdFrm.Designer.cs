namespace Hemo.Client.UI.User {
    partial class ctlChangPwdFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.Cancel = new Hemo.Client.Controls.DXSimpleButton();
            this.Confirm = new Hemo.Client.Controls.DXSimpleButton();
            this.ConfirmPwd = new DevExpress.XtraEditors.TextEdit();
            this.NewPwd = new DevExpress.XtraEditors.TextEdit();
            this.OldPwd = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ConfirmPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OldPwd.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labelControl4.Location = new System.Drawing.Point(42, 17);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(195, 18);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "温馨提示：密码不区分大小写";
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Cancel.ImageIndex = 3;
            this.Cancel.Location = new System.Drawing.Point(162, 179);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 28);
            this.Cancel.TabIndex = 13;
            this.Cancel.Text = "关闭(&C) ";
            // 
            // Confirm
            // 
            this.Confirm.ImageIndex = 7;
            this.Confirm.Location = new System.Drawing.Point(47, 179);
            this.Confirm.Name = "Confirm";
            this.Confirm.Size = new System.Drawing.Size(75, 28);
            this.Confirm.TabIndex = 8;
            this.Confirm.Text = "确定(&S) ";
            this.Confirm.Click += new System.EventHandler(this.Confirm_Click);
            // 
            // ConfirmPwd
            // 
            this.ConfirmPwd.EnterMoveNextControl = true;
            this.ConfirmPwd.Location = new System.Drawing.Point(112, 136);
            this.ConfirmPwd.Name = "ConfirmPwd";
            this.ConfirmPwd.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ConfirmPwd.Properties.Appearance.Options.UseFont = true;
            this.ConfirmPwd.Size = new System.Drawing.Size(146, 22);
            this.ConfirmPwd.TabIndex = 7;
            // 
            // NewPwd
            // 
            this.NewPwd.EnterMoveNextControl = true;
            this.NewPwd.Location = new System.Drawing.Point(111, 100);
            this.NewPwd.Name = "NewPwd";
            this.NewPwd.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.NewPwd.Properties.Appearance.Options.UseFont = true;
            this.NewPwd.Size = new System.Drawing.Size(146, 22);
            this.NewPwd.TabIndex = 6;
            // 
            // OldPwd
            // 
            this.OldPwd.EnterMoveNextControl = true;
            this.OldPwd.Location = new System.Drawing.Point(111, 65);
            this.OldPwd.Name = "OldPwd";
            this.OldPwd.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.OldPwd.Properties.Appearance.Options.UseFont = true;
            this.OldPwd.Size = new System.Drawing.Size(146, 22);
            this.OldPwd.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl3.Location = new System.Drawing.Point(26, 138);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(70, 17);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "确认密码：";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl2.Location = new System.Drawing.Point(26, 101);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(56, 17);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "新密码：";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl1.Location = new System.Drawing.Point(26, 66);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(56, 17);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "原密码：";
            this.labelControl1.Click += new System.EventHandler(this.labelControl1_Click);
            // 
            // ctlChangPwdFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Confirm);
            this.Controls.Add(this.ConfirmPwd);
            this.Controls.Add(this.NewPwd);
            this.Controls.Add(this.OldPwd);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Name = "ctlChangPwdFrm";
            this.Size = new System.Drawing.Size(279, 215);
            ((System.ComponentModel.ISupportInitialize)(this.ConfirmPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OldPwd.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl4;
        private Hemo.Client.Controls.DXSimpleButton Cancel;
        private Hemo.Client.Controls.DXSimpleButton Confirm;
        private DevExpress.XtraEditors.TextEdit ConfirmPwd;
        private DevExpress.XtraEditors.TextEdit NewPwd;
        private DevExpress.XtraEditors.TextEdit OldPwd;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}