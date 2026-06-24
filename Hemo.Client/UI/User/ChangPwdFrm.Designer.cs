namespace Hemo.Client.UI.User {
    partial class ChangPwdFrm {
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
            this.labelControl4.Location = new System.Drawing.Point(59, 24);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(156, 14);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "温馨提示：密码不区分大小写";
            // 
            // Cancel
            // 
            this.Cancel.ImageIndex = 3;
            this.Cancel.Location = new System.Drawing.Point(163, 212);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 25);
            this.Cancel.TabIndex = 13;
            this.Cancel.Text = "取消(&C) ";
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Confirm
            // 
            this.Confirm.ImageIndex = 7;
            this.Confirm.Location = new System.Drawing.Point(47, 212);
            this.Confirm.Name = "Confirm";
            this.Confirm.Size = new System.Drawing.Size(75, 25);
            this.Confirm.TabIndex = 8;
            this.Confirm.Text = "确定(&S) ";
            this.Confirm.Click += new System.EventHandler(this.Confirm_Click);
            // 
            // ConfirmPwd
            // 
            this.ConfirmPwd.EnterMoveNextControl = true;
            this.ConfirmPwd.Location = new System.Drawing.Point(112, 153);
            this.ConfirmPwd.Name = "ConfirmPwd";
            this.ConfirmPwd.Size = new System.Drawing.Size(146, 20);
            this.ConfirmPwd.TabIndex = 7;
            // 
            // NewPwd
            // 
            this.NewPwd.EnterMoveNextControl = true;
            this.NewPwd.Location = new System.Drawing.Point(112, 111);
            this.NewPwd.Name = "NewPwd";
            this.NewPwd.Size = new System.Drawing.Size(146, 20);
            this.NewPwd.TabIndex = 6;
            // 
            // OldPwd
            // 
            this.OldPwd.EnterMoveNextControl = true;
            this.OldPwd.Location = new System.Drawing.Point(112, 63);
            this.OldPwd.Name = "OldPwd";
            this.OldPwd.Size = new System.Drawing.Size(146, 20);
            this.OldPwd.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(26, 156);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "确认密码：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(26, 114);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "新密码：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(26, 66);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "原密码：";
            // 
            // ChangPwdFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Confirm);
            this.Controls.Add(this.ConfirmPwd);
            this.Controls.Add(this.NewPwd);
            this.Controls.Add(this.OldPwd);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Name = "ChangPwdFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "修改用户密码";
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