namespace Hemo.Client.UI.Machine
{
    partial class MixMachineEdit
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
            this.lb_id = new DevExpress.XtraEditors.LabelControl();
            this.mEDMACHINEMIXBARRELDataTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dateEdit_Disinfect = new DevExpress.XtraEditors.DateEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btn_Save = new Hemo.Client.Controls.DXSimpleButton();
            this.btn_Cancle = new Hemo.Client.Controls.DXSimpleButton();
            this.lop_sign = new DevExpress.XtraEditors.LookUpEdit();
            this.txt_eventext = new DevExpress.XtraEditors.MemoEdit();
            this.txt_result = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.mEDMACHINEMIXBARRELDataTableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_Disinfect.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_Disinfect.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lop_sign.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_eventext.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_result.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_id
            // 
            this.lb_id.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.mEDMACHINEMIXBARRELDataTableBindingSource, "ID", true));
            this.lb_id.Location = new System.Drawing.Point(40, 311);
            this.lb_id.Name = "lb_id";
            this.lb_id.Size = new System.Drawing.Size(0, 14);
            this.lb_id.TabIndex = 0;
            // 
            // mEDMACHINEMIXBARRELDataTableBindingSource
            // 
            this.mEDMACHINEMIXBARRELDataTableBindingSource.DataSource = typeof(Hemo.Model.MachineModel.MED_MACHINE_MIXBARRELDataTable);
            // 
            // dateEdit_Disinfect
            // 
            this.dateEdit_Disinfect.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.mEDMACHINEMIXBARRELDataTableBindingSource, "DISINFECTDATE", true));
            this.dateEdit_Disinfect.EditValue = null;
            this.dateEdit_Disinfect.Location = new System.Drawing.Point(130, 21);
            this.dateEdit_Disinfect.Name = "dateEdit_Disinfect";
            this.dateEdit_Disinfect.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_Disinfect.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit_Disinfect.Size = new System.Drawing.Size(119, 21);
            this.dateEdit_Disinfect.TabIndex = 1;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(40, 67);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 14);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "事件：";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(40, 274);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(36, 14);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "签名：";
            // 
            // btn_Save
            // 
            this.btn_Save.ImageIndex = 7;
            this.btn_Save.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_Save.Location = new System.Drawing.Point(268, 302);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 25);
            this.btn_Save.TabIndex = 10;
            this.btn_Save.Text = "保存(&S)";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Cancle
            // 
            this.btn_Cancle.ImageIndex = 3;
            this.btn_Cancle.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_Cancle.Location = new System.Drawing.Point(362, 302);
            this.btn_Cancle.Name = "btn_Cancle";
            this.btn_Cancle.Size = new System.Drawing.Size(75, 25);
            this.btn_Cancle.TabIndex = 11;
            this.btn_Cancle.Text = "取消(&C)";
            this.btn_Cancle.Click += new System.EventHandler(this.btn_Cancle_Click);
            // 
            // lop_sign
            // 
            this.lop_sign.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.mEDMACHINEMIXBARRELDataTableBindingSource, "SIGN", true));
            this.lop_sign.EditValue = "";
            this.lop_sign.EnterMoveNextControl = true;
            this.lop_sign.Location = new System.Drawing.Point(130, 270);
            this.lop_sign.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lop_sign.Name = "lop_sign";
            this.lop_sign.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lop_sign.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lop_sign.Properties.Appearance.Options.UseFont = true;
            this.lop_sign.Properties.Appearance.Options.UseForeColor = true;
            this.lop_sign.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lop_sign.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.lop_sign.Properties.NullText = "";
            this.lop_sign.Size = new System.Drawing.Size(137, 23);
            this.lop_sign.TabIndex = 9;
            // 
            // txt_eventext
            // 
            this.txt_eventext.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.mEDMACHINEMIXBARRELDataTableBindingSource, "EVENTTEXT", true));
            this.txt_eventext.EditValue = "将反渗水注入配液桶容器至二分之一刻度处，然后加入已配好的14%过氧乙酸原液7.92L，再注满反渗水稀释至浓度为0.2%；悬挂“消毒中”警示牌；浸泡1小时后排空。";
            this.txt_eventext.Location = new System.Drawing.Point(130, 65);
            this.txt_eventext.Name = "txt_eventext";
            this.txt_eventext.Size = new System.Drawing.Size(300, 80);
            this.txt_eventext.TabIndex = 7;
            // 
            // txt_result
            // 
            this.txt_result.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.mEDMACHINEMIXBARRELDataTableBindingSource, "RESULT", true));
            this.txt_result.EditValue = "1.用反渗水反复冲洗，直至用过氧乙酸残余量试纸检测<1ppm，合格；2.更换滤芯，反渗水冲洗滤芯即可。";
            this.txt_result.Location = new System.Drawing.Point(130, 166);
            this.txt_result.Name = "txt_result";
            this.txt_result.Size = new System.Drawing.Size(300, 80);
            this.txt_result.TabIndex = 7;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(40, 169);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 14);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "结果：";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(40, 24);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "日期：";
            // 
            // MixMachineEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 337);
            this.Controls.Add(this.lop_sign);
            this.Controls.Add(this.btn_Cancle);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.dateEdit_Disinfect);
            this.Controls.Add(this.lb_id);
            this.Controls.Add(this.txt_result);
            this.Controls.Add(this.txt_eventext);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(488, 295);
            this.Name = "MixMachineEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "配制液消毒记录";
            this.Load += new System.EventHandler(this.MixMachineEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mEDMACHINEMIXBARRELDataTableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_Disinfect.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_Disinfect.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lop_sign.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_eventext.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_result.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lb_id;
        private DevExpress.XtraEditors.DateEdit dateEdit_Disinfect;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private Hemo.Client.Controls.DXSimpleButton btn_Save;
        private Hemo.Client.Controls.DXSimpleButton btn_Cancle;
        private DevExpress.XtraEditors.LookUpEdit lop_sign;
        private DevExpress.XtraEditors.MemoEdit txt_eventext;
        private DevExpress.XtraEditors.MemoEdit txt_result;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.BindingSource mEDMACHINEMIXBARRELDataTableBindingSource;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider;
        private DevExpress.XtraEditors.LabelControl labelControl1;

    }
}