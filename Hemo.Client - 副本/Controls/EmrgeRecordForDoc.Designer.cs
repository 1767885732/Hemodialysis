namespace Hemo.Client.Controls
{
    partial class EmrgeRecordForDoc
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.btn_NurseRecord = new DevExpress.XtraEditors.SimpleButton();
            this.btn_save = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lbl_PatientInfo = new DevExpress.XtraEditors.LabelControl();
            this.txtRecord = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecord.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.btn_NurseRecord);
            this.panelControl3.Controls.Add(this.btn_save);
            this.panelControl3.Controls.Add(this.btnClose);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(0, 265);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(591, 41);
            this.panelControl3.TabIndex = 346;
            // 
            // btn_NurseRecord
            // 
            this.btn_NurseRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_NurseRecord.Location = new System.Drawing.Point(395, 13);
            this.btn_NurseRecord.Name = "btn_NurseRecord";
            this.btn_NurseRecord.Size = new System.Drawing.Size(90, 23);
            this.btn_NurseRecord.TabIndex = 302;
            this.btn_NurseRecord.Text = "护士抢救记录";
            this.btn_NurseRecord.Click += new System.EventHandler(this.btn_NurseRecord_Click);
            // 
            // btn_save
            // 
            this.btn_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_save.Location = new System.Drawing.Point(308, 13);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(81, 23);
            this.btn_save.TabIndex = 302;
            this.btn_save.Text = "保存(&S)";
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(491, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 23);
            this.btnClose.TabIndex = 299;
            this.btnClose.Text = "关闭(&D)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lbl_PatientInfo);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(591, 41);
            this.panelControl1.TabIndex = 347;
            // 
            // lbl_PatientInfo
            // 
            this.lbl_PatientInfo.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PatientInfo.Appearance.Options.UseFont = true;
            this.lbl_PatientInfo.Location = new System.Drawing.Point(104, 12);
            this.lbl_PatientInfo.Name = "lbl_PatientInfo";
            this.lbl_PatientInfo.Size = new System.Drawing.Size(48, 17);
            this.lbl_PatientInfo.TabIndex = 381;
            this.lbl_PatientInfo.Text = "病人信息";
            // 
            // txtRecord
            // 
            this.txtRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRecord.EnterMoveNextControl = true;
            this.txtRecord.Location = new System.Drawing.Point(0, 41);
            this.txtRecord.Name = "txtRecord";
            this.txtRecord.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtRecord.Properties.Appearance.Options.UseForeColor = true;
            this.txtRecord.Size = new System.Drawing.Size(591, 224);
            this.txtRecord.TabIndex = 348;
            // 
            // EmrgeRecordForDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 306);
            this.Controls.Add(this.txtRecord);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl3);
            this.Name = "EmrgeRecordForDoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "抢救记录";
            this.Load += new System.EventHandler(this.EmrgeRecordForDoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecord.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton btn_save;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.MemoEdit txtRecord;
        private DevExpress.XtraEditors.SimpleButton btn_NurseRecord;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        public DevExpress.XtraEditors.LabelControl lbl_PatientInfo;
    }
}
