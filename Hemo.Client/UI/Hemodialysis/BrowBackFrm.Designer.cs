namespace Hemo.Client.UI.Hemodialysis
{
    partial class BrowBackFrm
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
            this.btnQuery = new Hemo.Client.Controls.DXSimpleButton();
            this.txtBorrowDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnAdd = new Hemo.Client.Controls.DXSimpleButton();
            this.busyIndicator1 = new Hemo.Client.Controls.BusyIndicator();
            this.labelControl103 = new DevExpress.XtraEditors.LabelControl();
            this.lupCHECK_NURSE = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtPatientName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textBackInfo = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBorrowDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBorrowDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupCHECK_NURSE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPatientName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBackInfo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.ImageIndex = 7;
            this.btnQuery.Location = new System.Drawing.Point(71, 175);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 25);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "确定(&S)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtBorrowDate
            // 
            this.txtBorrowDate.EditValue = null;
            this.txtBorrowDate.EnterMoveNextControl = true;
            this.txtBorrowDate.Location = new System.Drawing.Point(94, 110);
            this.txtBorrowDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBorrowDate.Name = "txtBorrowDate";
            this.txtBorrowDate.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtBorrowDate.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtBorrowDate.Properties.Appearance.Options.UseFont = true;
            this.txtBorrowDate.Properties.Appearance.Options.UseForeColor = true;
            this.txtBorrowDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtBorrowDate.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtBorrowDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtBorrowDate.Size = new System.Drawing.Size(198, 23);
            this.txtBorrowDate.TabIndex = 575;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 114);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 14);
            this.labelControl1.TabIndex = 576;
            this.labelControl1.Text = "归还日期:";
            // 
            // btnAdd
            // 
            this.btnAdd.ImageIndex = 3;
            this.btnAdd.Location = new System.Drawing.Point(167, 175);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 25);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "取消(&C)";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // busyIndicator1
            // 
            this.busyIndicator1.Location = new System.Drawing.Point(105, 88);
            this.busyIndicator1.Name = "busyIndicator1";
            this.busyIndicator1.Size = new System.Drawing.Size(100, 40);
            this.busyIndicator1.TabIndex = 583;
            // 
            // labelControl103
            // 
            this.labelControl103.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl103.Appearance.Options.UseFont = true;
            this.labelControl103.Location = new System.Drawing.Point(25, 147);
            this.labelControl103.Name = "labelControl103";
            this.labelControl103.Size = new System.Drawing.Size(39, 17);
            this.labelControl103.TabIndex = 798;
            this.labelControl103.Text = "归还人:";
            // 
            // lupCHECK_NURSE
            // 
            this.lupCHECK_NURSE.EnterMoveNextControl = true;
            this.lupCHECK_NURSE.Location = new System.Drawing.Point(94, 141);
            this.lupCHECK_NURSE.Name = "lupCHECK_NURSE";
            this.lupCHECK_NURSE.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lupCHECK_NURSE.Properties.Appearance.Options.UseFont = true;
            this.lupCHECK_NURSE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupCHECK_NURSE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.lupCHECK_NURSE.Properties.NullText = "";
            this.lupCHECK_NURSE.Size = new System.Drawing.Size(198, 23);
            this.lupCHECK_NURSE.TabIndex = 797;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 29);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 14);
            this.labelControl3.TabIndex = 799;
            this.labelControl3.Text = "患者姓名:";
            // 
            // txtPatientName
            // 
            this.txtPatientName.Enabled = false;
            this.txtPatientName.Location = new System.Drawing.Point(94, 26);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.Size = new System.Drawing.Size(198, 21);
            this.txtPatientName.TabIndex = 800;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 65);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(76, 14);
            this.labelControl2.TabIndex = 802;
            this.labelControl2.Text = "归还药品信息:";
            // 
            // textBackInfo
            // 
            this.textBackInfo.Location = new System.Drawing.Point(94, 62);
            this.textBackInfo.Name = "textBackInfo";
            this.textBackInfo.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.textBackInfo.Properties.Appearance.Options.UseForeColor = true;
            this.textBackInfo.Size = new System.Drawing.Size(198, 41);
            this.textBackInfo.TabIndex = 801;
            // 
            // BrowBackFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 210);
            this.Controls.Add(this.busyIndicator1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtPatientName);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl103);
            this.Controls.Add(this.lupCHECK_NURSE);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.txtBorrowDate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.textBackInfo);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(324, 249);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(324, 248);
            this.Name = "BrowBackFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "患者借还药管理";
            this.Load += new System.EventHandler(this.ShowSummary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtBorrowDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBorrowDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupCHECK_NURSE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPatientName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBackInfo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Hemo.Client.Controls.DXSimpleButton btnQuery;
        private DevExpress.XtraEditors.DateEdit txtBorrowDate;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Hemo.Client.Controls.DXSimpleButton btnAdd;
        private Controls.BusyIndicator busyIndicator1;
        private DevExpress.XtraEditors.LabelControl labelControl103;
        private DevExpress.XtraEditors.LookUpEdit lupCHECK_NURSE;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtPatientName;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.MemoEdit textBackInfo;
    }
}