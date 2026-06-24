namespace Hemo.Client.UI.Machine
{
    partial class EditProcess
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.btnClose = new Hemo.Client.Controls.DXSimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lpPOCESSID = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtSort = new DevExpress.XtraEditors.TextEdit();
            this.lab4 = new DevExpress.XtraEditors.LabelControl();
            this.cbxSTATUS = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.lpPOCESSID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxSTATUS.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(64, 111);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 27);
            this.btnSave.TabIndex = 354;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImageIndex = 3;
            this.btnClose.Location = new System.Drawing.Point(155, 111);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 27);
            this.btnClose.TabIndex = 356;
            this.btnClose.Text = "取消(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(3, 34);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 357;
            this.labelControl1.Text = "流程节点";
            // 
            // lpPOCESSID
            // 
            this.lpPOCESSID.Location = new System.Drawing.Point(56, 33);
            this.lpPOCESSID.Name = "lpPOCESSID";
            this.lpPOCESSID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lpPOCESSID.Size = new System.Drawing.Size(136, 20);
            this.lpPOCESSID.TabIndex = 358;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(23, 9);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 14);
            this.labelControl2.TabIndex = 359;
            this.labelControl2.Text = "名称";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(26, 86);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 14);
            this.labelControl3.TabIndex = 361;
            this.labelControl3.Text = "排序";
            // 
            // txtSort
            // 
            this.txtSort.Location = new System.Drawing.Point(56, 83);
            this.txtSort.Name = "txtSort";
            this.txtSort.Size = new System.Drawing.Size(100, 20);
            this.txtSort.TabIndex = 362;
            // 
            // lab4
            // 
            this.lab4.Location = new System.Drawing.Point(25, 58);
            this.lab4.Name = "lab4";
            this.lab4.Size = new System.Drawing.Size(24, 14);
            this.lab4.TabIndex = 363;
            this.lab4.Text = "状态";
            // 
            // cbxSTATUS
            // 
            this.cbxSTATUS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxSTATUS.EditValue = "";
            this.cbxSTATUS.EnterMoveNextControl = true;
            this.cbxSTATUS.Location = new System.Drawing.Point(56, 57);
            this.cbxSTATUS.Name = "cbxSTATUS";
            this.cbxSTATUS.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxSTATUS.Properties.Items.AddRange(new object[] {
            "启用",
            "停用"});
            this.cbxSTATUS.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxSTATUS.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxSTATUS.Size = new System.Drawing.Size(102, 20);
            this.cbxSTATUS.TabIndex = 364;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(56, 6);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 22);
            this.txtName.TabIndex = 365;
            // 
            // EditProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 141);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.cbxSTATUS);
            this.Controls.Add(this.lab4);
            this.Controls.Add(this.txtSort);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.lpPOCESSID);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Name = "EditProcess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "绑定流程节点";
            this.Load += new System.EventHandler(this.EditProcess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lpPOCESSID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxSTATUS.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.DXSimpleButton btnSave;
        private Controls.DXSimpleButton btnClose;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lpPOCESSID;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtSort;
        private DevExpress.XtraEditors.LabelControl lab4;
        private DevExpress.XtraEditors.ComboBoxEdit cbxSTATUS;
        private System.Windows.Forms.TextBox txtName;
    }
}
