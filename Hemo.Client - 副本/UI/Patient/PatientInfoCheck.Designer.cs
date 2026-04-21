namespace Hemo.Client.UI.Patient
{
    partial class PatientInfoCheck
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
            this.components = new System.ComponentModel.Container();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtHemoId = new DevExpress.XtraEditors.TextEdit();
            this.btnPick = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtHemoId.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelControl1.Location = new System.Drawing.Point(0, 0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = " 透析号/卡号: ";
            // 
            // txtHemoId
            // 
            this.txtHemoId.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtHemoId.Location = new System.Drawing.Point(78, 0);
            this.txtHemoId.Name = "txtHemoId";
            this.txtHemoId.Properties.AutoHeight = false;
            this.txtHemoId.Size = new System.Drawing.Size(83, 23);
            this.txtHemoId.TabIndex = 1;
            this.txtHemoId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHemoId_KeyDown);
            // 
            // btnPick
            // 
            this.btnPick.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPick.ImageIndex = 11;
            this.btnPick.Location = new System.Drawing.Point(161, 0);
            this.btnPick.Name = "btnPick";
            this.btnPick.Size = new System.Drawing.Size(65, 23);
            this.btnPick.TabIndex = 4;
            this.btnPick.Text = "选择";
            this.btnPick.Click += new System.EventHandler(this.btnPick_Click);
            // 
            // PatientInfoCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnPick);
            this.Controls.Add(this.txtHemoId);
            this.Controls.Add(this.labelControl1);
            this.Name = "PatientInfoCheck";
            this.Size = new System.Drawing.Size(230, 23);
            ((System.ComponentModel.ISupportInitialize)(this.txtHemoId.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtHemoId;
        private Controls.DXSimpleButton btnPick;
    }
}
