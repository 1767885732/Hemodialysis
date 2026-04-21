namespace Hemo.Client.UI.HemodialysisApply
{
    partial class EditHemodialysisApply
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
            this.btnClose = new Hemo.Client.Controls.DXSimpleButton();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.lab3 = new DevExpress.XtraEditors.LabelControl();
            this.lab2 = new DevExpress.XtraEditors.LabelControl();
            this.lab1 = new DevExpress.XtraEditors.LabelControl();
            this.txtAPPLY_COMMENT = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtSTART_TIME = new DevExpress.XtraEditors.TimeEdit();
            this.txtEND_TIME = new DevExpress.XtraEditors.TimeEdit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAPPLY_COMMENT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTART_TIME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEND_TIME.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImageIndex = 3;
            this.btnClose.Location = new System.Drawing.Point(203, 174);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(101, 31);
            this.btnClose.TabIndex = 355;
            this.btnClose.Text = "取消";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(96, 174);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(101, 31);
            this.btnSave.TabIndex = 353;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // lab3
            // 
            this.lab3.Location = new System.Drawing.Point(12, 69);
            this.lab3.Name = "lab3";
            this.lab3.Size = new System.Drawing.Size(24, 14);
            this.lab3.TabIndex = 346;
            this.lab3.Text = "备注";
            // 
            // lab2
            // 
            this.lab2.Location = new System.Drawing.Point(12, 42);
            this.lab2.Name = "lab2";
            this.lab2.Size = new System.Drawing.Size(48, 14);
            this.lab2.TabIndex = 344;
            this.lab2.Text = "结束时间";
            // 
            // lab1
            // 
            this.lab1.Location = new System.Drawing.Point(12, 15);
            this.lab1.Name = "lab1";
            this.lab1.Size = new System.Drawing.Size(48, 14);
            this.lab1.TabIndex = 341;
            this.lab1.Text = "开始时间";
            // 
            // txtAPPLY_COMMENT
            // 
            this.txtAPPLY_COMMENT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAPPLY_COMMENT.Location = new System.Drawing.Point(66, 66);
            this.txtAPPLY_COMMENT.Name = "txtAPPLY_COMMENT";
            this.txtAPPLY_COMMENT.Properties.MaxLength = 72;
            this.txtAPPLY_COMMENT.Size = new System.Drawing.Size(238, 90);
            this.txtAPPLY_COMMENT.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 341;
            this.labelControl1.Text = "开始时间";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 69);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 14);
            this.labelControl2.TabIndex = 346;
            this.labelControl2.Text = "备注";
            // 
            // txtSTART_TIME
            // 
            this.txtSTART_TIME.EditValue = null;
            this.txtSTART_TIME.Location = new System.Drawing.Point(66, 12);
            this.txtSTART_TIME.Name = "txtSTART_TIME";
            this.txtSTART_TIME.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtSTART_TIME.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtSTART_TIME.Properties.Mask.EditMask = "t";
            this.txtSTART_TIME.Properties.MaxLength = 20;
            this.txtSTART_TIME.Size = new System.Drawing.Size(76, 21);
            this.txtSTART_TIME.TabIndex = 1;
            // 
            // txtEND_TIME
            // 
            this.txtEND_TIME.EditValue = null;
            this.txtEND_TIME.Location = new System.Drawing.Point(66, 39);
            this.txtEND_TIME.Name = "txtEND_TIME";
            this.txtEND_TIME.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtEND_TIME.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtEND_TIME.Properties.Mask.EditMask = "t";
            this.txtEND_TIME.Properties.MaxLength = 20;
            this.txtEND_TIME.Size = new System.Drawing.Size(76, 21);
            this.txtEND_TIME.TabIndex = 360;
            // 
            // EditHemodialysisApply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 217);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.lab3);
            this.Controls.Add(this.lab2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lab1);
            this.Controls.Add(this.txtAPPLY_COMMENT);
            this.Controls.Add(this.txtSTART_TIME);
            this.Controls.Add(this.txtEND_TIME);
            this.MinimumSize = new System.Drawing.Size(333, 255);
            this.Name = "EditHemodialysisApply";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "预约申请";
            this.Load += new System.EventHandler(this.EditMachine_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAPPLY_COMMENT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTART_TIME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEND_TIME.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Hemo.Client.Controls.DXSimpleButton btnClose;
        private Hemo.Client.Controls.DXSimpleButton btnSave;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider;
        private DevExpress.XtraEditors.LabelControl lab3;
        private DevExpress.XtraEditors.LabelControl lab2;
        private DevExpress.XtraEditors.LabelControl lab1;
        private DevExpress.XtraEditors.MemoEdit txtAPPLY_COMMENT;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TimeEdit txtSTART_TIME;
        private DevExpress.XtraEditors.TimeEdit txtEND_TIME;
    }
}