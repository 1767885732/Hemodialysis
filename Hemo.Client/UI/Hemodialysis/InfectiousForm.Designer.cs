namespace Hemo.Client.UI.Hemodialysis
{
    partial class InfectiousForm
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
            this.btn_OK = new Hemo.Client.Controls.DXSimpleButton();
            this.btn_Cancle = new Hemo.Client.Controls.DXSimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txt_Other = new DevExpress.XtraEditors.TextEdit();
            this.checkEdit_Other = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit5 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit2 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit4 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit3 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit6 = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Other.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit_Other.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit6.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.ImageIndex = 7;
            this.btn_OK.Location = new System.Drawing.Point(119, 173);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 25);
            this.btn_OK.TabIndex = 1;
            this.btn_OK.Text = "保存(&S)";
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancle
            // 
            this.btn_Cancle.ImageIndex = 3;
            this.btn_Cancle.Location = new System.Drawing.Point(209, 173);
            this.btn_Cancle.Name = "btn_Cancle";
            this.btn_Cancle.Size = new System.Drawing.Size(75, 25);
            this.btn_Cancle.TabIndex = 1;
            this.btn_Cancle.Text = "取消(&C)";
            this.btn_Cancle.Click += new System.EventHandler(this.btn_Cancle_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.checkEdit6);
            this.panelControl1.Controls.Add(this.txt_Other);
            this.panelControl1.Controls.Add(this.checkEdit_Other);
            this.panelControl1.Controls.Add(this.checkEdit5);
            this.panelControl1.Controls.Add(this.checkEdit2);
            this.panelControl1.Controls.Add(this.checkEdit1);
            this.panelControl1.Controls.Add(this.checkEdit4);
            this.panelControl1.Controls.Add(this.checkEdit3);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(296, 167);
            this.panelControl1.TabIndex = 4;
            // 
            // txt_Other
            // 
            this.txt_Other.Location = new System.Drawing.Point(120, 132);
            this.txt_Other.Name = "txt_Other";
            this.txt_Other.Size = new System.Drawing.Size(145, 21);
            this.txt_Other.TabIndex = 9;
            this.txt_Other.Visible = false;
            // 
            // checkEdit_Other
            // 
            this.checkEdit_Other.Location = new System.Drawing.Point(38, 132);
            this.checkEdit_Other.Name = "checkEdit_Other";
            this.checkEdit_Other.Properties.Caption = "其它";
            this.checkEdit_Other.Size = new System.Drawing.Size(44, 19);
            this.checkEdit_Other.TabIndex = 5;
            this.checkEdit_Other.Visible = false;
            this.checkEdit_Other.CheckedChanged += new System.EventHandler(this.checkEdit_Other_CheckedChanged);
            // 
            // checkEdit5
            // 
            this.checkEdit5.Location = new System.Drawing.Point(38, 99);
            this.checkEdit5.Name = "checkEdit5";
            this.checkEdit5.Properties.Caption = "全阴";
            this.checkEdit5.Size = new System.Drawing.Size(75, 19);
            this.checkEdit5.TabIndex = 5;
            // 
            // checkEdit2
            // 
            this.checkEdit2.Location = new System.Drawing.Point(158, 61);
            this.checkEdit2.Name = "checkEdit2";
            this.checkEdit2.Properties.Caption = "丙肝";
            this.checkEdit2.Size = new System.Drawing.Size(75, 19);
            this.checkEdit2.TabIndex = 5;
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(158, 22);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "艾滋病";
            this.checkEdit1.Size = new System.Drawing.Size(75, 19);
            this.checkEdit1.TabIndex = 8;
            // 
            // checkEdit4
            // 
            this.checkEdit4.Location = new System.Drawing.Point(38, 61);
            this.checkEdit4.Name = "checkEdit4";
            this.checkEdit4.Properties.Caption = "梅毒";
            this.checkEdit4.Size = new System.Drawing.Size(75, 19);
            this.checkEdit4.TabIndex = 6;
            // 
            // checkEdit3
            // 
            this.checkEdit3.Location = new System.Drawing.Point(38, 22);
            this.checkEdit3.Name = "checkEdit3";
            this.checkEdit3.Properties.Caption = "乙肝";
            this.checkEdit3.Size = new System.Drawing.Size(75, 19);
            this.checkEdit3.TabIndex = 7;
            // 
            // checkEdit6
            // 
            this.checkEdit6.Location = new System.Drawing.Point(158, 99);
            this.checkEdit6.Name = "checkEdit6";
            this.checkEdit6.Properties.Caption = "待查";
            this.checkEdit6.Size = new System.Drawing.Size(75, 19);
            this.checkEdit6.TabIndex = 10;
            // 
            // InfectiousForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 203);
            this.Controls.Add(this.btn_Cancle);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InfectiousForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "传染病";
            this.Load += new System.EventHandler(this.InfectiousForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txt_Other.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit_Other.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit6.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Hemo.Client.Controls.DXSimpleButton btn_OK;
        private Hemo.Client.Controls.DXSimpleButton btn_Cancle;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CheckEdit checkEdit2;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraEditors.CheckEdit checkEdit4;
        private DevExpress.XtraEditors.CheckEdit checkEdit3;
        private DevExpress.XtraEditors.TextEdit txt_Other;
        private DevExpress.XtraEditors.CheckEdit checkEdit_Other;
        private DevExpress.XtraEditors.CheckEdit checkEdit5;
        private DevExpress.XtraEditors.CheckEdit checkEdit6;
    }
}