namespace Hemo.Client.Controls
{
    partial class ctlAuoSearchCondition
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lupChoice = new DevExpress.XtraEditors.LookUpEdit();
            this.txtRange = new DevExpress.XtraEditors.TextEdit();
            this.lupCondition = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutRange = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutCondition = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutChoice = new DevExpress.XtraLayout.LayoutControlItem();
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lupChoice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRange.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupCondition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutChoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lupChoice);
            this.layoutControl1.Controls.Add(this.txtRange);
            this.layoutControl1.Controls.Add(this.lupCondition);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(372, 156, 725, 485);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(525, 26);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lupChoice
            // 
            this.lupChoice.Location = new System.Drawing.Point(378, 3);
            this.lupChoice.Name = "lupChoice";
            this.lupChoice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupChoice.Size = new System.Drawing.Size(144, 20);
            this.lupChoice.StyleController = this.layoutControl1;
            this.lupChoice.TabIndex = 7;
            // 
            // txtRange
            // 
            this.txtRange.Location = new System.Drawing.Point(190, 3);
            this.txtRange.Name = "txtRange";
            this.txtRange.Properties.NullValuePrompt = "请输入";
            this.txtRange.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtRange.Size = new System.Drawing.Size(133, 20);
            this.txtRange.StyleController = this.layoutControl1;
            this.txtRange.TabIndex = 5;
            // 
            // lupCondition
            // 
            this.lupCondition.Location = new System.Drawing.Point(54, 3);
            this.lupCondition.Name = "lupCondition";
            this.lupCondition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupCondition.Size = new System.Drawing.Size(123, 20);
            this.lupCondition.StyleController = this.layoutControl1;
            this.lupCondition.TabIndex = 4;
            this.lupCondition.EditValueChanged += new System.EventHandler(this.lupCondition_EditValueChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutRange,
            this.layoutCondition,
            this.layoutChoice});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(525, 26);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutRange
            // 
            this.layoutRange.Control = this.txtRange;
            this.layoutRange.Location = new System.Drawing.Point(178, 0);
            this.layoutRange.MaxSize = new System.Drawing.Size(146, 24);
            this.layoutRange.MinSize = new System.Drawing.Size(146, 24);
            this.layoutRange.Name = "layoutRange";
            this.layoutRange.Size = new System.Drawing.Size(146, 24);
            this.layoutRange.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutRange.Text = ":";
            this.layoutRange.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutRange.TextSize = new System.Drawing.Size(4, 14);
            this.layoutRange.TextToControlDistance = 5;
            // 
            // layoutCondition
            // 
            this.layoutCondition.Control = this.lupCondition;
            this.layoutCondition.Location = new System.Drawing.Point(0, 0);
            this.layoutCondition.Name = "layoutCondition";
            this.layoutCondition.Size = new System.Drawing.Size(178, 24);
            this.layoutCondition.Text = "条件选择";
            this.layoutCondition.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutChoice
            // 
            this.layoutChoice.Control = this.lupChoice;
            this.layoutChoice.Location = new System.Drawing.Point(324, 0);
            this.layoutChoice.MinSize = new System.Drawing.Size(120, 24);
            this.layoutChoice.Name = "layoutChoice";
            this.layoutChoice.Size = new System.Drawing.Size(199, 24);
            this.layoutChoice.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutChoice.Text = "阴性阳性";
            this.layoutChoice.TextSize = new System.Drawing.Size(48, 14);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ctlAuoSearchCondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ctlAuoSearchCondition";
            this.Size = new System.Drawing.Size(525, 26);
            this.Load += new System.EventHandler(this.ctlAuoSearchCondition_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lupChoice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRange.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupCondition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutChoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.LookUpEdit lupChoice;
        private DevExpress.XtraEditors.TextEdit txtRange;
        private DevExpress.XtraEditors.LookUpEdit lupCondition;
        private DevExpress.XtraLayout.LayoutControlItem layoutRange;
        private DevExpress.XtraLayout.LayoutControlItem layoutCondition;
        private DevExpress.XtraLayout.LayoutControlItem layoutChoice;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider;
    }
}
