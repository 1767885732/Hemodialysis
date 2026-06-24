namespace Hemo.Client.UI.Machine
{
    partial class HosEquipmentEdit
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
            this.dateEdit_Disinfect = new DevExpress.XtraEditors.DateEdit();
            this.mEDMACHINEMIXBARRELDataTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btn_Save = new Hemo.Client.Controls.DXSimpleButton();
            this.btn_Cancle = new Hemo.Client.Controls.DXSimpleButton();
            this.txt_eventext = new DevExpress.XtraEditors.MemoEdit();
            this.txt_result = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lb_id = new DevExpress.XtraEditors.LabelControl();
            this.txt_machine = new DevExpress.XtraEditors.TextEdit();
            this.dateEdit_check = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_Disinfect.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_Disinfect.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mEDMACHINEMIXBARRELDataTableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_eventext.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_result.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_machine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_check.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_check.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(16, 31);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "启用日期：";
            // 
            // dateEdit_Disinfect
            // 
            this.dateEdit_Disinfect.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.mEDMACHINEMIXBARRELDataTableBindingSource, "DISINFECTDATE", true));
            this.dateEdit_Disinfect.EditValue = null;
            this.dateEdit_Disinfect.Location = new System.Drawing.Point(82, 28);
            this.dateEdit_Disinfect.Name = "dateEdit_Disinfect";
            this.dateEdit_Disinfect.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_Disinfect.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit_Disinfect.Size = new System.Drawing.Size(119, 21);
            this.dateEdit_Disinfect.TabIndex = 1;
            // 
            // mEDMACHINEMIXBARRELDataTableBindingSource
            // 
            this.mEDMACHINEMIXBARRELDataTableBindingSource.DataSource = typeof(Hemo.Model.MachineModel.MED_MACHINE_MIXBARRELDataTable);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(40, 67);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 14);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "维修：";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(24, 267);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(52, 14);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "效验日期:";
            // 
            // btn_Save
            // 
            this.btn_Save.ImageIndex = 7;
            this.btn_Save.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_Save.Location = new System.Drawing.Point(207, 298);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(85, 27);
            this.btn_Save.TabIndex = 10;
            this.btn_Save.Text = "保存(&S)";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Cancle
            // 
            this.btn_Cancle.ImageIndex = 3;
            this.btn_Cancle.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_Cancle.Location = new System.Drawing.Point(297, 298);
            this.btn_Cancle.Name = "btn_Cancle";
            this.btn_Cancle.Size = new System.Drawing.Size(85, 27);
            this.btn_Cancle.TabIndex = 11;
            this.btn_Cancle.Text = "取消(&C)";
            this.btn_Cancle.Click += new System.EventHandler(this.btn_Cancle_Click);
            // 
            // txt_eventext
            // 
            this.txt_eventext.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.mEDMACHINEMIXBARRELDataTableBindingSource, "EVENTTEXT", true));
            this.txt_eventext.EditValue = "";
            this.txt_eventext.Location = new System.Drawing.Point(82, 64);
            this.txt_eventext.Name = "txt_eventext";
            this.txt_eventext.Size = new System.Drawing.Size(300, 80);
            this.txt_eventext.TabIndex = 7;
            // 
            // txt_result
            // 
            this.txt_result.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.mEDMACHINEMIXBARRELDataTableBindingSource, "RESULT", true));
            this.txt_result.EditValue = "";
            this.txt_result.Location = new System.Drawing.Point(82, 165);
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
            this.labelControl2.Text = "保养：";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(207, 32);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 12;
            this.labelControl3.Text = "贵重设备：";
            // 
            // lb_id
            // 
            this.lb_id.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.mEDMACHINEMIXBARRELDataTableBindingSource, "ID", true));
            this.lb_id.Location = new System.Drawing.Point(64, 311);
            this.lb_id.Name = "lb_id";
            this.lb_id.Size = new System.Drawing.Size(0, 14);
            this.lb_id.TabIndex = 18;
            // 
            // txt_machine
            // 
            this.txt_machine.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.mEDMACHINEMIXBARRELDataTableBindingSource, "MACHINE", true));
            this.txt_machine.Location = new System.Drawing.Point(273, 28);
            this.txt_machine.Name = "txt_machine";
            this.txt_machine.Size = new System.Drawing.Size(109, 21);
            this.txt_machine.TabIndex = 19;
            // 
            // dateEdit_check
            // 
            this.dateEdit_check.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.mEDMACHINEMIXBARRELDataTableBindingSource, "CHECKDATE", true));
            this.dateEdit_check.EditValue = null;
            this.dateEdit_check.Location = new System.Drawing.Point(82, 264);
            this.dateEdit_check.Name = "dateEdit_check";
            this.dateEdit_check.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_check.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit_check.Size = new System.Drawing.Size(119, 21);
            this.dateEdit_check.TabIndex = 1;
            // 
            // HosEquipmentEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 337);
            this.Controls.Add(this.txt_machine);
            this.Controls.Add(this.lb_id);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.btn_Cancle);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.dateEdit_check);
            this.Controls.Add(this.dateEdit_Disinfect);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txt_result);
            this.Controls.Add(this.txt_eventext);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(440, 376);
            this.MinimumSize = new System.Drawing.Size(440, 375);
            this.Name = "HosEquipmentEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "贵重医疗设备保养及检测登记";
            this.Load += new System.EventHandler(this.HosEquipmentEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_Disinfect.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_Disinfect.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mEDMACHINEMIXBARRELDataTableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_eventext.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_result.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_machine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_check.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_check.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dateEdit_Disinfect;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private Hemo.Client.Controls.DXSimpleButton btn_Save;
        private Hemo.Client.Controls.DXSimpleButton btn_Cancle;
        private DevExpress.XtraEditors.MemoEdit txt_eventext;
        private DevExpress.XtraEditors.MemoEdit txt_result;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl lb_id;
        private System.Windows.Forms.BindingSource mEDMACHINEMIXBARRELDataTableBindingSource;
        private DevExpress.XtraEditors.TextEdit txt_machine;
        private DevExpress.XtraEditors.DateEdit dateEdit_check;

    }
}