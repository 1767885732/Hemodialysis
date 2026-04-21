namespace Hemo.Client.UI.Patient {
    partial class PatientRecord {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.mEDPATIENTSRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.memoEdit_Fill = new DevExpress.XtraEditors.MemoEdit();
            this.memoEdit_Top = new DevExpress.XtraEditors.MemoEdit();
            this.memoEdit_bottom = new DevExpress.XtraEditors.MemoEdit();
            this.recoredDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.labelControl76 = new DevExpress.XtraEditors.LabelControl();
            this.btn_Save = new Hemo.Client.Controls.DXSimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_call = new Hemo.Client.Controls.DXSimpleButton();
            this.btn_Mould = new Hemo.Client.Controls.DXSimpleButton();
            this.ctlUserLongInfo1 = new Hemo.Client.Controls.CtlUserLongInfo();
            ((System.ComponentModel.ISupportInitialize)(this.mEDPATIENTSRowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_Fill.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_Top.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_bottom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recoredDateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recoredDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mEDPATIENTSRowBindingSource
            // 
            this.mEDPATIENTSRowBindingSource.AllowNew = true;
            this.mEDPATIENTSRowBindingSource.DataSource = typeof(Hemo.Model.PatientModel.MED_PATIENTSRow);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.memoEdit_Fill);
            this.groupControl1.Controls.Add(this.memoEdit_Top);
            this.groupControl1.Controls.Add(this.memoEdit_bottom);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 38);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1012, 466);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "病历内容";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseBackColor = true;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(6, 321);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 14);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "既往史：";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseBackColor = true;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(5, 173);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 14);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "现病史：";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl1.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseBackColor = true;
            this.labelControl1.Appearance.Options.UseBorderColor = true;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(5, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(39, 14);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "主诉：";
            // 
            // memoEdit_Fill
            // 
            this.memoEdit_Fill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoEdit_Fill.EditValue = "";
            this.memoEdit_Fill.Location = new System.Drawing.Point(2, 170);
            this.memoEdit_Fill.Name = "memoEdit_Fill";
            this.memoEdit_Fill.Size = new System.Drawing.Size(1008, 147);
            this.memoEdit_Fill.TabIndex = 3;
            // 
            // memoEdit_Top
            // 
            this.memoEdit_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.memoEdit_Top.EditValue = "";
            this.memoEdit_Top.Location = new System.Drawing.Point(2, 23);
            this.memoEdit_Top.Name = "memoEdit_Top";
            this.memoEdit_Top.Size = new System.Drawing.Size(1008, 147);
            this.memoEdit_Top.TabIndex = 2;
            // 
            // memoEdit_bottom
            // 
            this.memoEdit_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.memoEdit_bottom.EditValue = "";
            this.memoEdit_bottom.Location = new System.Drawing.Point(2, 317);
            this.memoEdit_bottom.Name = "memoEdit_bottom";
            this.memoEdit_bottom.Size = new System.Drawing.Size(1008, 147);
            this.memoEdit_bottom.TabIndex = 4;
            // 
            // recoredDateEdit
            // 
            this.recoredDateEdit.EditValue = null;
            this.recoredDateEdit.Location = new System.Drawing.Point(825, 9);
            this.recoredDateEdit.Name = "recoredDateEdit";
            this.recoredDateEdit.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recoredDateEdit.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.recoredDateEdit.Properties.Appearance.Options.UseFont = true;
            this.recoredDateEdit.Properties.Appearance.Options.UseForeColor = true;
            this.recoredDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.recoredDateEdit.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.recoredDateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.recoredDateEdit.Size = new System.Drawing.Size(90, 21);
            this.recoredDateEdit.TabIndex = 307;
            this.recoredDateEdit.EditValueChanged += new System.EventHandler(this.txtBIRTHDAY_EditValueChanged);
            // 
            // labelControl76
            // 
            this.labelControl76.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl76.Appearance.Options.UseFont = true;
            this.labelControl76.Location = new System.Drawing.Point(771, 12);
            this.labelControl76.Name = "labelControl76";
            this.labelControl76.Size = new System.Drawing.Size(48, 17);
            this.labelControl76.TabIndex = 308;
            this.labelControl76.Text = "病历日期";
            // 
            // btn_Save
            // 
            this.btn_Save.ImageIndex = 7;
            this.btn_Save.Location = new System.Drawing.Point(876, 8);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(70, 25);
            this.btn_Save.TabIndex = 309;
            this.btn_Save.Text = "保存";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_call);
            this.panelControl1.Controls.Add(this.btn_Mould);
            this.panelControl1.Controls.Add(this.btn_Save);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 504);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1012, 40);
            this.panelControl1.TabIndex = 310;
            // 
            // btn_call
            // 
            this.btn_call.ImageIndex = 10;
            this.btn_call.Location = new System.Drawing.Point(673, 8);
            this.btn_call.Name = "btn_call";
            this.btn_call.Size = new System.Drawing.Size(96, 25);
            this.btn_call.TabIndex = 309;
            this.btn_call.Text = "调用模板";
            this.btn_call.Visible = false;
            this.btn_call.Click += new System.EventHandler(this.btn_call_Click);
            // 
            // btn_Mould
            // 
            this.btn_Mould.ImageIndex = 5;
            this.btn_Mould.Location = new System.Drawing.Point(775, 8);
            this.btn_Mould.Name = "btn_Mould";
            this.btn_Mould.Size = new System.Drawing.Size(93, 25);
            this.btn_Mould.TabIndex = 309;
            this.btn_Mould.Text = "保存为模板";
            this.btn_Mould.Visible = false;
            this.btn_Mould.Click += new System.EventHandler(this.btn_Mould_Click);
            // 
            // ctlUserLongInfo1
            // 
            this.ctlUserLongInfo1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline) 
                | System.Drawing.FontStyle.Strikeout))), System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.ctlUserLongInfo1.Appearance.Options.UseFont = true;
            this.ctlUserLongInfo1.DIAGNOSE = "";
            this.ctlUserLongInfo1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlUserLongInfo1.FormContainer = null;
            this.ctlUserLongInfo1.HEMODIALYSIS_ID = "";
            this.ctlUserLongInfo1.Location = new System.Drawing.Point(0, 0);
            this.ctlUserLongInfo1.Name = "ctlUserLongInfo1";
            this.ctlUserLongInfo1.PanBorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.ctlUserLongInfo1.PanelWidth = 1012;
            this.ctlUserLongInfo1.PatientType = "";
            this.ctlUserLongInfo1.PatientTypeEnabled = true;
            this.ctlUserLongInfo1.Size = new System.Drawing.Size(1012, 38);
            this.ctlUserLongInfo1.TabIndex = 0;
            // 
            // PatientRecord
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1012, 544);
            this.Controls.Add(this.recoredDateEdit);
            this.Controls.Add(this.labelControl76);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.ctlUserLongInfo1);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1028, 500);
            this.Name = "PatientRecord";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "病人病历";
            this.Load += new System.EventHandler(this.PatientRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mEDPATIENTSRowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_Fill.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_Top.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_bottom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recoredDateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recoredDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider;
        private System.Windows.Forms.BindingSource mEDPATIENTSRowBindingSource;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.MemoEdit memoEdit_Top;
        private Hemo.Client.Controls.DXSimpleButton btn_Save;
        private DevExpress.XtraEditors.DateEdit recoredDateEdit;
        private DevExpress.XtraEditors.LabelControl labelControl76;
        public Controls.CtlUserLongInfo ctlUserLongInfo1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.MemoEdit memoEdit_Fill;
        private DevExpress.XtraEditors.MemoEdit memoEdit_bottom;
        private Hemo.Client.Controls.DXSimpleButton btn_call;
        private Hemo.Client.Controls.DXSimpleButton btn_Mould;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;

    }
}