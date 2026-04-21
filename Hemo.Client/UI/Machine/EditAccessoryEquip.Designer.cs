namespace Hemo.Client.UI.Machine
{
    partial class EditAccessoryEquip
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lbl_MainFrame = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txt_Name = new DevExpress.XtraEditors.TextEdit();
            this.txt_Type = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txt_Count = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txt_FactoryNumber = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btn_Cancle = new Hemo.Client.Controls.DXSimpleButton();
            this.btn_Save = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Name.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Type.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Count.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_FactoryNumber.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "所属主机名称:";
            // 
            // lbl_MainFrame
            // 
            this.lbl_MainFrame.Location = new System.Drawing.Point(94, 12);
            this.lbl_MainFrame.Name = "lbl_MainFrame";
            this.lbl_MainFrame.Size = new System.Drawing.Size(6, 14);
            this.lbl_MainFrame.TabIndex = 1;
            this.lbl_MainFrame.Text = "?";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 47);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(28, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "名称:";
            // 
            // txt_Name
            // 
            this.txt_Name.Location = new System.Drawing.Point(94, 44);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(178, 21);
            this.txt_Name.TabIndex = 3;
            // 
            // txt_Type
            // 
            this.txt_Type.Location = new System.Drawing.Point(94, 71);
            this.txt_Type.Name = "txt_Type";
            this.txt_Type.Size = new System.Drawing.Size(178, 21);
            this.txt_Type.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 74);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 14);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "型号规格:";
            // 
            // txt_Count
            // 
            this.txt_Count.Location = new System.Drawing.Point(94, 98);
            this.txt_Count.Name = "txt_Count";
            this.txt_Count.Size = new System.Drawing.Size(178, 21);
            this.txt_Count.TabIndex = 7;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 101);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(28, 14);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "数量:";
            // 
            // txt_FactoryNumber
            // 
            this.txt_FactoryNumber.Location = new System.Drawing.Point(94, 125);
            this.txt_FactoryNumber.Name = "txt_FactoryNumber";
            this.txt_FactoryNumber.Size = new System.Drawing.Size(178, 21);
            this.txt_FactoryNumber.TabIndex = 9;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 128);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(52, 14);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "出厂编号:";
            // 
            // btn_Cancle
            // 
            this.btn_Cancle.ImageIndex = 3;
            this.btn_Cancle.Location = new System.Drawing.Point(197, 166);
            this.btn_Cancle.Name = "btn_Cancle";
            this.btn_Cancle.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancle.TabIndex = 21;
            this.btn_Cancle.Text = "取消(&C)";
            this.btn_Cancle.Click += new System.EventHandler(this.btn_Cancle_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.ImageIndex = 7;
            this.btn_Save.Location = new System.Drawing.Point(116, 166);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 20;
            this.btn_Save.Text = "保存(&S)";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // EditAccessoryEquip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 201);
            this.Controls.Add(this.btn_Cancle);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.txt_FactoryNumber);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.txt_Count);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.txt_Type);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txt_Name);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.lbl_MainFrame);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditAccessoryEquip";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "主要附属设备";
            this.Load += new System.EventHandler(this.EditAccessoryEquip_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txt_Name.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Type.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Count.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_FactoryNumber.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lbl_MainFrame;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txt_Name;
        private DevExpress.XtraEditors.TextEdit txt_Type;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txt_Count;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txt_FactoryNumber;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private Hemo.Client.Controls.DXSimpleButton btn_Cancle;
        private Hemo.Client.Controls.DXSimpleButton btn_Save;
    }
}