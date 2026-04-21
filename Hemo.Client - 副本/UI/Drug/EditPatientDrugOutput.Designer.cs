namespace Hemo.Client.UI.Drug {
    partial class EditPatientDrugOutput {
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lbDrugInfo = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new Hemo.Client.Controls.DXSimpleButton();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.ctlUserLongInfo1 = new Hemo.Client.Controls.CtlUserLongInfo();
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.cbxDRUG_TYPE = new DevExpress.XtraEditors.LookUpEdit();
            this.cbxUNITS = new DevExpress.XtraEditors.LookUpEdit();
            this.txtFIRM_NAME = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtFIRM_ID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txt_COUNT = new DevExpress.XtraEditors.TextEdit();
            this.txtDRUG_NAME = new Hemo.Utilities.CustomGridLookUpEdit();
            this.customGridLookUpEdit2View = new Hemo.Utilities.CustomGridView();
            this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spiPRICE = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtDRUG_SPEC = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.lab1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxDRUG_TYPE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxUNITS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFIRM_NAME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFIRM_ID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_COUNT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDRUG_NAME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spiPRICE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDRUG_SPEC.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lbDrugInfo);
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Location = new System.Drawing.Point(9, 245);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(515, 38);
            this.panelControl1.TabIndex = 350;
            // 
            // lbDrugInfo
            // 
            this.lbDrugInfo.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lbDrugInfo.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lbDrugInfo.Appearance.Options.UseFont = true;
            this.lbDrugInfo.Appearance.Options.UseForeColor = true;
            this.lbDrugInfo.Location = new System.Drawing.Point(9, 11);
            this.lbDrugInfo.Name = "lbDrugInfo";
            this.lbDrugInfo.Size = new System.Drawing.Size(0, 14);
            this.lbDrugInfo.TabIndex = 351;
            // 
            // btnCancel
            // 
            this.btnCancel.ImageIndex = 3;
            this.btnCancel.Location = new System.Drawing.Point(436, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 25);
            this.btnCancel.TabIndex = 350;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(360, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 25);
            this.btnSave.TabIndex = 349;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ctlUserLongInfo1
            // 
            this.ctlUserLongInfo1.DIAGNOSE = "";
            this.ctlUserLongInfo1.FormContainer = null;
            this.ctlUserLongInfo1.HEMODIALYSIS_ID = "";
            this.ctlUserLongInfo1.Location = new System.Drawing.Point(22, 10);
            this.ctlUserLongInfo1.Name = "ctlUserLongInfo1";
            this.ctlUserLongInfo1.PanBorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.ctlUserLongInfo1.PanelWidth = 493;
            this.ctlUserLongInfo1.PatientType = "";
            this.ctlUserLongInfo1.PatientTypeEnabled = true;
            this.ctlUserLongInfo1.Size = new System.Drawing.Size(493, 39);
            this.ctlUserLongInfo1.TabIndex = 349;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.cbxDRUG_TYPE);
            this.panelControl2.Controls.Add(this.cbxUNITS);
            this.panelControl2.Controls.Add(this.txtFIRM_NAME);
            this.panelControl2.Controls.Add(this.labelControl5);
            this.panelControl2.Controls.Add(this.txtFIRM_ID);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Controls.Add(this.txt_COUNT);
            this.panelControl2.Controls.Add(this.txtDRUG_NAME);
            this.panelControl2.Controls.Add(this.spiPRICE);
            this.panelControl2.Controls.Add(this.labelControl6);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.txtDRUG_SPEC);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Controls.Add(this.labelControl8);
            this.panelControl2.Controls.Add(this.lab1);
            this.panelControl2.Location = new System.Drawing.Point(9, 55);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(515, 181);
            this.panelControl2.TabIndex = 352;
            // 
            // cbxDRUG_TYPE
            // 
            this.cbxDRUG_TYPE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxDRUG_TYPE.EditValue = "";
            this.cbxDRUG_TYPE.EnterMoveNextControl = true;
            this.cbxDRUG_TYPE.Location = new System.Drawing.Point(327, 131);
            this.cbxDRUG_TYPE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbxDRUG_TYPE.Name = "cbxDRUG_TYPE";
            this.cbxDRUG_TYPE.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cbxDRUG_TYPE.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.cbxDRUG_TYPE.Properties.Appearance.Options.UseFont = true;
            this.cbxDRUG_TYPE.Properties.Appearance.Options.UseForeColor = true;
            this.cbxDRUG_TYPE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxDRUG_TYPE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxDRUG_TYPE.Properties.NullText = "";
            this.cbxDRUG_TYPE.Size = new System.Drawing.Size(150, 23);
            this.cbxDRUG_TYPE.TabIndex = 369;
            this.cbxDRUG_TYPE.Visible = false;
            // 
            // cbxUNITS
            // 
            this.cbxUNITS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxUNITS.EditValue = "";
            this.cbxUNITS.Enabled = false;
            this.cbxUNITS.EnterMoveNextControl = true;
            this.cbxUNITS.Location = new System.Drawing.Point(92, 61);
            this.cbxUNITS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbxUNITS.Name = "cbxUNITS";
            this.cbxUNITS.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cbxUNITS.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.cbxUNITS.Properties.Appearance.Options.UseFont = true;
            this.cbxUNITS.Properties.Appearance.Options.UseForeColor = true;
            this.cbxUNITS.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxUNITS.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxUNITS.Properties.NullText = "";
            this.cbxUNITS.Properties.PopupSizeable = false;
            this.cbxUNITS.Properties.ReadOnly = true;
            this.cbxUNITS.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
            this.cbxUNITS.Properties.ShowFooter = false;
            this.cbxUNITS.Properties.ShowHeader = false;
            this.cbxUNITS.Properties.ShowLines = false;
            this.cbxUNITS.Properties.ShowPopupShadow = false;
            this.cbxUNITS.Size = new System.Drawing.Size(150, 23);
            this.cbxUNITS.TabIndex = 368;
            // 
            // txtFIRM_NAME
            // 
            this.txtFIRM_NAME.Enabled = false;
            this.txtFIRM_NAME.EnterMoveNextControl = true;
            this.txtFIRM_NAME.Location = new System.Drawing.Point(327, 96);
            this.txtFIRM_NAME.Name = "txtFIRM_NAME";
            this.txtFIRM_NAME.Size = new System.Drawing.Size(150, 21);
            this.txtFIRM_NAME.TabIndex = 364;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(270, 103);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.TabIndex = 367;
            this.labelControl5.Text = "厂商名称";
            // 
            // txtFIRM_ID
            // 
            this.txtFIRM_ID.Enabled = false;
            this.txtFIRM_ID.EnterMoveNextControl = true;
            this.txtFIRM_ID.Location = new System.Drawing.Point(92, 100);
            this.txtFIRM_ID.Name = "txtFIRM_ID";
            this.txtFIRM_ID.Size = new System.Drawing.Size(150, 21);
            this.txtFIRM_ID.TabIndex = 365;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(32, 103);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 366;
            this.labelControl3.Text = "厂商编号";
            // 
            // txt_COUNT
            // 
            this.txt_COUNT.EnterMoveNextControl = true;
            this.txt_COUNT.Location = new System.Drawing.Point(92, 137);
            this.txt_COUNT.Name = "txt_COUNT";
            this.txt_COUNT.Size = new System.Drawing.Size(150, 21);
            this.txt_COUNT.TabIndex = 2;
            // 
            // txtDRUG_NAME
            // 
            this.txtDRUG_NAME.EditValue = "";
            this.txtDRUG_NAME.EnterMoveNextControl = true;
            this.txtDRUG_NAME.Location = new System.Drawing.Point(92, 19);
            this.txtDRUG_NAME.Name = "txtDRUG_NAME";
            this.txtDRUG_NAME.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtDRUG_NAME.Properties.Appearance.Options.UseForeColor = true;
            this.txtDRUG_NAME.Properties.AutoComplete = false;
            this.txtDRUG_NAME.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDRUG_NAME.Properties.DisplayMember = "DRUG_NAME";
            this.txtDRUG_NAME.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.txtDRUG_NAME.Properties.ValueMember = "DRUG_CODE";
            this.txtDRUG_NAME.Properties.View = this.customGridLookUpEdit2View;
            this.txtDRUG_NAME.Size = new System.Drawing.Size(150, 21);
            this.txtDRUG_NAME.TabIndex = 1;
            this.txtDRUG_NAME.EditValueChanged += new System.EventHandler(this.txtDRUG_NAME_EditValueChanged);
            // 
            // customGridLookUpEdit2View
            // 
            this.customGridLookUpEdit2View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn28,
            this.gridColumn30,
            this.gridColumn31});
            this.customGridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.customGridLookUpEdit2View.Name = "customGridLookUpEdit2View";
            this.customGridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.customGridLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn28
            // 
            this.gridColumn28.Caption = "编号";
            this.gridColumn28.FieldName = "DRUG_CODE";
            this.gridColumn28.Name = "gridColumn28";
            this.gridColumn28.Visible = true;
            this.gridColumn28.VisibleIndex = 0;
            this.gridColumn28.Width = 95;
            // 
            // gridColumn30
            // 
            this.gridColumn30.Caption = "名称";
            this.gridColumn30.FieldName = "DRUG_NAME";
            this.gridColumn30.Name = "gridColumn30";
            this.gridColumn30.Visible = true;
            this.gridColumn30.VisibleIndex = 1;
            this.gridColumn30.Width = 150;
            // 
            // gridColumn31
            // 
            this.gridColumn31.Caption = "拼音码";
            this.gridColumn31.FieldName = "DRUG_PINYIN";
            this.gridColumn31.Name = "gridColumn31";
            this.gridColumn31.Visible = true;
            this.gridColumn31.VisibleIndex = 2;
            this.gridColumn31.Width = 67;
            // 
            // spiPRICE
            // 
            this.spiPRICE.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spiPRICE.Enabled = false;
            this.spiPRICE.EnterMoveNextControl = true;
            this.spiPRICE.Location = new System.Drawing.Point(327, 57);
            this.spiPRICE.Name = "spiPRICE";
            this.spiPRICE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spiPRICE.Properties.Mask.EditMask = "c2";
            this.spiPRICE.Size = new System.Drawing.Size(150, 21);
            this.spiPRICE.TabIndex = 355;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(287, 60);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(24, 14);
            this.labelControl6.TabIndex = 359;
            this.labelControl6.Text = "单价";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(35, 64);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 358;
            this.labelControl1.Text = "单位";
            // 
            // txtDRUG_SPEC
            // 
            this.txtDRUG_SPEC.Enabled = false;
            this.txtDRUG_SPEC.EnterMoveNextControl = true;
            this.txtDRUG_SPEC.Location = new System.Drawing.Point(327, 19);
            this.txtDRUG_SPEC.Name = "txtDRUG_SPEC";
            this.txtDRUG_SPEC.Size = new System.Drawing.Size(150, 21);
            this.txtDRUG_SPEC.TabIndex = 352;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(287, 22);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 14);
            this.labelControl2.TabIndex = 357;
            this.labelControl2.Text = "规格";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(270, 135);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 356;
            this.labelControl4.Text = "药品分类";
            this.labelControl4.Visible = false;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(35, 140);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(48, 14);
            this.labelControl8.TabIndex = 350;
            this.labelControl8.Text = "使用数量";
            // 
            // lab1
            // 
            this.lab1.Location = new System.Drawing.Point(35, 22);
            this.lab1.Name = "lab1";
            this.lab1.Size = new System.Drawing.Size(24, 14);
            this.lab1.TabIndex = 348;
            this.lab1.Text = "药品";
            // 
            // EditPatientDrugOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 293);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ctlUserLongInfo1);
            this.Name = "EditPatientDrugOutput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "患者药品使用";
            this.Load += new System.EventHandler(this.EditPatientDrugOutput_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxDRUG_TYPE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxUNITS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFIRM_NAME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFIRM_ID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_COUNT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDRUG_NAME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spiPRICE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDRUG_SPEC.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Controls.DXSimpleButton btnCancel;
        private Controls.DXSimpleButton btnSave;
        private Controls.CtlUserLongInfo ctlUserLongInfo1;
        private DevExpress.XtraEditors.LabelControl lbDrugInfo;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LookUpEdit cbxDRUG_TYPE;
        private DevExpress.XtraEditors.LookUpEdit cbxUNITS;
        private DevExpress.XtraEditors.TextEdit txtFIRM_NAME;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtFIRM_ID;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txt_COUNT;
        private Utilities.CustomGridLookUpEdit txtDRUG_NAME;
        private Utilities.CustomGridView customGridLookUpEdit2View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn28;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn30;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn31;
        private DevExpress.XtraEditors.SpinEdit spiPRICE;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtDRUG_SPEC;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl lab1;
    }
}