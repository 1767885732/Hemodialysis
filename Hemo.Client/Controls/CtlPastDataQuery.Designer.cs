namespace Hemo.Client.Controls
{
    partial class CtlPastDataQuery
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
            this.customGridLookUpEdit1 = new Hemo.Utilities.CustomGridLookUpEdit();
            this.customGridLookUpEdit1View = new Hemo.Utilities.CustomGridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customGridLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.customGridLookUpEdit1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(167, 23);
            this.panelControl1.TabIndex = 0;
            // 
            // customGridLookUpEdit1
            // 
            this.customGridLookUpEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridLookUpEdit1.Location = new System.Drawing.Point(2, 2);
            this.customGridLookUpEdit1.Name = "customGridLookUpEdit1";
            this.customGridLookUpEdit1.Properties.AutoComplete = false;
            this.customGridLookUpEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.customGridLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.customGridLookUpEdit1.Properties.NullText = "";
            this.customGridLookUpEdit1.Properties.PopupFormSize = new System.Drawing.Size(200, 0);
            this.customGridLookUpEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.customGridLookUpEdit1.Properties.View = this.customGridLookUpEdit1View;
            this.customGridLookUpEdit1.Size = new System.Drawing.Size(163, 18);
            this.customGridLookUpEdit1.TabIndex = 1;
            // 
            // customGridLookUpEdit1View
            // 
            this.customGridLookUpEdit1View.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.customGridLookUpEdit1View.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.customGridLookUpEdit1View.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.customGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.customGridLookUpEdit1View.Name = "customGridLookUpEdit1View";
            this.customGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.customGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // CtlPastDataQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "CtlPastDataQuery";
            this.Size = new System.Drawing.Size(167, 23);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customGridLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Utilities.CustomGridLookUpEdit customGridLookUpEdit1;
        private Utilities.CustomGridView customGridLookUpEdit1View;
        private DevExpress.XtraEditors.PanelControl panelControl1;

    }
}
