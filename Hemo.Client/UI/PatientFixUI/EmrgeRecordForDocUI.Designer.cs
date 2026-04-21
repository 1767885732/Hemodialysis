namespace Hemo.Client.Controls
{
    partial class EmrgeRecordForDocUI
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtRecord = new DevExpress.XtraEditors.MemoEdit();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.dxSimpleButton2 = new Hemo.Client.Controls.DXSimpleButton();
            this.dxSimpleButton1 = new Hemo.Client.Controls.DXSimpleButton();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecord.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtRecord);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(591, 265);
            this.panelControl1.TabIndex = 347;
            // 
            // txtRecord
            // 
            this.txtRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRecord.EnterMoveNextControl = true;
            this.txtRecord.Location = new System.Drawing.Point(2, 2);
            this.txtRecord.Name = "txtRecord";
            this.txtRecord.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtRecord.Properties.Appearance.Options.UseForeColor = true;
            this.txtRecord.Size = new System.Drawing.Size(587, 261);
            this.txtRecord.TabIndex = 349;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.dxSimpleButton2);
            this.panelControl3.Controls.Add(this.dxSimpleButton1);
            this.panelControl3.Controls.Add(this.btnSave);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(0, 265);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(591, 41);
            this.panelControl3.TabIndex = 346;
            // 
            // dxSimpleButton2
            // 
            this.dxSimpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dxSimpleButton2.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.dxSimpleButton2.Appearance.Options.UseFont = true;
            this.dxSimpleButton2.ImageIndex = 4;
            this.dxSimpleButton2.Location = new System.Drawing.Point(360, 9);
            this.dxSimpleButton2.Name = "dxSimpleButton2";
            this.dxSimpleButton2.Size = new System.Drawing.Size(126, 25);
            this.dxSimpleButton2.TabIndex = 305;
            this.dxSimpleButton2.Text = "护士抢救记录(&N)";
            this.dxSimpleButton2.Click += new System.EventHandler(this.btn_NurseRecord_Click);
            // 
            // dxSimpleButton1
            // 
            this.dxSimpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dxSimpleButton1.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.dxSimpleButton1.Appearance.Options.UseFont = true;
            this.dxSimpleButton1.ImageIndex = 3;
            this.dxSimpleButton1.Location = new System.Drawing.Point(492, 9);
            this.dxSimpleButton1.Name = "dxSimpleButton1";
            this.dxSimpleButton1.Size = new System.Drawing.Size(75, 25);
            this.dxSimpleButton1.TabIndex = 304;
            this.dxSimpleButton1.Text = "关闭(&D)";
            this.dxSimpleButton1.Visible = false;
            this.dxSimpleButton1.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(279, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 303;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // EmrgeRecordForDocUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl3);
            this.Name = "EmrgeRecordForDocUI";
            this.Size = new System.Drawing.Size(591, 306);
            this.Load += new System.EventHandler(this.EmrgeRecordForDoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRecord.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.MemoEdit txtRecord;
        private DXSimpleButton dxSimpleButton2;
        private DXSimpleButton dxSimpleButton1;
        private DXSimpleButton btnSave;
    }
}
