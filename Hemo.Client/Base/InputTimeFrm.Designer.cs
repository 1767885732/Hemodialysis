namespace Hemo.Client.Base
{
    partial class InputTimeFrm
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
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtTimeHourse = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtTimeMinutes = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnNo = new Hemo.Client.Controls.DXSimpleButton();
            this.btnYes = new Hemo.Client.Controls.DXSimpleButton();
            this.labelControlStar = new DevExpress.XtraEditors.LabelControl();
            this.labelControlEnd = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimeHourse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimeMinutes.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(96, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "确定结束治疗吗？";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(17, 94);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 14);
            this.labelControl2.TabIndex = 452;
            this.labelControl2.Text = "实际治疗时长";
            // 
            // txtTimeHourse
            // 
            this.txtTimeHourse.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTimeHourse.Location = new System.Drawing.Point(96, 89);
            this.txtTimeHourse.Name = "txtTimeHourse";
            this.txtTimeHourse.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtTimeHourse.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtTimeHourse.Properties.Appearance.Options.UseBackColor = true;
            this.txtTimeHourse.Properties.Appearance.Options.UseForeColor = true;
            this.txtTimeHourse.Properties.AutoHeight = false;
            this.txtTimeHourse.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtTimeHourse.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtTimeHourse.Size = new System.Drawing.Size(73, 24);
            this.txtTimeHourse.TabIndex = 451;
            this.txtTimeHourse.EditValueChanged += new System.EventHandler(this.txtTimeHourse_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(175, 94);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(12, 14);
            this.labelControl3.TabIndex = 452;
            this.labelControl3.Text = "时";
            // 
            // txtTimeMinutes
            // 
            this.txtTimeMinutes.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTimeMinutes.Location = new System.Drawing.Point(193, 89);
            this.txtTimeMinutes.Name = "txtTimeMinutes";
            this.txtTimeMinutes.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtTimeMinutes.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtTimeMinutes.Properties.Appearance.Options.UseBackColor = true;
            this.txtTimeMinutes.Properties.Appearance.Options.UseForeColor = true;
            this.txtTimeMinutes.Properties.AutoHeight = false;
            this.txtTimeMinutes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtTimeMinutes.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtTimeMinutes.Size = new System.Drawing.Size(73, 24);
            this.txtTimeMinutes.TabIndex = 451;
            this.txtTimeMinutes.EditValueChanged += new System.EventHandler(this.txtTimeMinutes_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(272, 94);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(12, 14);
            this.labelControl4.TabIndex = 452;
            this.labelControl4.Text = "分";
            // 
            // btnNo
            // 
            this.btnNo.ImageIndex = 13;
            this.btnNo.Location = new System.Drawing.Point(180, 143);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(82, 23);
            this.btnNo.TabIndex = 454;
            this.btnNo.Text = "否(&N)";
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // btnYes
            // 
            this.btnYes.ImageIndex = 11;
            this.btnYes.Location = new System.Drawing.Point(87, 143);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(82, 23);
            this.btnYes.TabIndex = 453;
            this.btnYes.Text = "是(&Y)";
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // labelControlStar
            // 
            this.labelControlStar.Location = new System.Drawing.Point(69, 36);
            this.labelControlStar.Name = "labelControlStar";
            this.labelControlStar.Size = new System.Drawing.Size(48, 14);
            this.labelControlStar.TabIndex = 452;
            this.labelControlStar.Text = "开始时间";
            // 
            // labelControlEnd
            // 
            this.labelControlEnd.Location = new System.Drawing.Point(69, 61);
            this.labelControlEnd.Name = "labelControlEnd";
            this.labelControlEnd.Size = new System.Drawing.Size(48, 14);
            this.labelControlEnd.TabIndex = 452;
            this.labelControlEnd.Text = "结束时间";
            // 
            // InputTimeFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 183);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControlEnd);
            this.Controls.Add(this.labelControlStar);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtTimeMinutes);
            this.Controls.Add(this.txtTimeHourse);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputTimeFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "系统提示";
            this.Load += new System.EventHandler(this.InputTimeFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtTimeHourse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimeMinutes.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private Controls.DXSimpleButton btnYes;
        private Controls.DXSimpleButton btnNo;
        private DevExpress.XtraEditors.SpinEdit txtTimeHourse;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SpinEdit txtTimeMinutes;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControlStar;
        private DevExpress.XtraEditors.LabelControl labelControlEnd;
    }
}