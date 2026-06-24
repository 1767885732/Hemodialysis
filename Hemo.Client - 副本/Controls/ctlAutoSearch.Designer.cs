namespace Hemo.Client.Controls
{
    partial class ctlAutoSearch
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.chkConstant = new DevExpress.XtraEditors.CheckEdit();
            this.btnReport = new Hemo.Client.Controls.DXSimpleButton();
            this.btnSearch = new Hemo.Client.Controls.DXSimpleButton();
            this.txtInfo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lupItmes = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.beginTime = new DevExpress.XtraEditors.DateEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.endTime = new DevExpress.XtraEditors.DateEdit();
            this.btnControlP = new Hemo.Client.Controls.DXSimpleButton();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnLoad = new Hemo.Client.Controls.DXSimpleButton();
            this.btnClear = new Hemo.Client.Controls.DXSimpleButton();
            this.btnExpExcel = new Hemo.Client.Controls.DXSimpleButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkConstant.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInfo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupItmes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.chkConstant);
            this.panelControl1.Controls.Add(this.btnReport);
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Controls.Add(this.txtInfo);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.lupItmes);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.beginTime);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.endTime);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1006, 40);
            this.panelControl1.TabIndex = 0;
            // 
            // chkConstant
            // 
            this.chkConstant.EditValue = true;
            this.chkConstant.Location = new System.Drawing.Point(753, 7);
            this.chkConstant.Name = "chkConstant";
            this.chkConstant.Properties.Caption = "维持性透析";
            this.chkConstant.Size = new System.Drawing.Size(80, 19);
            this.chkConstant.TabIndex = 817;
            // 
            // btnReport
            // 
            this.btnReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReport.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.btnReport.Appearance.Options.UseFont = true;
            this.btnReport.ImageIndex = 4;
            this.btnReport.Location = new System.Drawing.Point(930, 5);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(70, 26);
            this.btnReport.TabIndex = 43;
            this.btnReport.Text = "说明";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.btnSearch.Appearance.Options.UseFont = true;
            this.btnSearch.ImageIndex = 8;
            this.btnSearch.Location = new System.Drawing.Point(855, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(70, 26);
            this.btnSearch.TabIndex = 41;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtInfo
            // 
            this.txtInfo.Location = new System.Drawing.Point(601, 5);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtInfo.Properties.Appearance.Options.UseFont = true;
            this.txtInfo.Properties.NullValuePrompt = " 姓名/透析号/拼音";
            this.txtInfo.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtInfo.Size = new System.Drawing.Size(140, 24);
            this.txtInfo.TabIndex = 40;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labelControl2.Location = new System.Drawing.Point(535, 8);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 18);
            this.labelControl2.TabIndex = 39;
            this.labelControl2.Text = "患者搜索";
            // 
            // lupItmes
            // 
            this.lupItmes.Location = new System.Drawing.Point(401, 5);
            this.lupItmes.Name = "lupItmes";
            this.lupItmes.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.lupItmes.Properties.Appearance.Options.UseFont = true;
            this.lupItmes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupItmes.Size = new System.Drawing.Size(129, 24);
            this.lupItmes.TabIndex = 38;
            this.lupItmes.EditValueChanged += new System.EventHandler(this.lupItmes_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labelControl1.Location = new System.Drawing.Point(365, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(30, 18);
            this.labelControl1.TabIndex = 37;
            this.labelControl1.Text = "项目";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labelControl5.Location = new System.Drawing.Point(5, 8);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(60, 18);
            this.labelControl5.TabIndex = 35;
            this.labelControl5.Text = "开始日期";
            // 
            // beginTime
            // 
            this.beginTime.EditValue = null;
            this.beginTime.Location = new System.Drawing.Point(68, 5);
            this.beginTime.Name = "beginTime";
            this.beginTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.beginTime.Properties.Appearance.Options.UseFont = true;
            this.beginTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.beginTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beginTime.Size = new System.Drawing.Size(108, 24);
            this.beginTime.TabIndex = 34;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labelControl4.Location = new System.Drawing.Point(182, 8);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 18);
            this.labelControl4.TabIndex = 36;
            this.labelControl4.Text = "结束日期";
            // 
            // endTime
            // 
            this.endTime.EditValue = null;
            this.endTime.Location = new System.Drawing.Point(244, 5);
            this.endTime.Name = "endTime";
            this.endTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.endTime.Properties.Appearance.Options.UseFont = true;
            this.endTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.endTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.endTime.Size = new System.Drawing.Size(117, 24);
            this.endTime.TabIndex = 33;
            // 
            // btnControlP
            // 
            this.btnControlP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnControlP.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.btnControlP.Appearance.Options.UseFont = true;
            this.btnControlP.ImageIndex = 2;
            this.btnControlP.Location = new System.Drawing.Point(778, 3);
            this.btnControlP.Name = "btnControlP";
            this.btnControlP.Size = new System.Drawing.Size(70, 26);
            this.btnControlP.TabIndex = 42;
            this.btnControlP.Text = "收起";
            this.btnControlP.Click += new System.EventHandler(this.btnControlP_Click);
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.lblInfo);
            this.panelControl3.Controls.Add(this.btnLoad);
            this.panelControl3.Controls.Add(this.btnClear);
            this.panelControl3.Controls.Add(this.btnControlP);
            this.panelControl3.Controls.Add(this.btnExpExcel);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 75);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1006, 31);
            this.panelControl3.TabIndex = 13;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(5, 8);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 14);
            this.lblInfo.TabIndex = 45;
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.btnLoad.Appearance.Options.UseFont = true;
            this.btnLoad.ImageIndex = 4;
            this.btnLoad.Location = new System.Drawing.Point(702, 3);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(70, 26);
            this.btnLoad.TabIndex = 44;
            this.btnLoad.Text = "导数据";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageIndex = 18;
            this.btnClear.Location = new System.Drawing.Point(854, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 26);
            this.btnClear.TabIndex = 43;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExpExcel
            // 
            this.btnExpExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExpExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.btnExpExcel.Appearance.Options.UseFont = true;
            this.btnExpExcel.ImageIndex = 4;
            this.btnExpExcel.Location = new System.Drawing.Point(930, 3);
            this.btnExpExcel.Name = "btnExpExcel";
            this.btnExpExcel.Size = new System.Drawing.Size(70, 26);
            this.btnExpExcel.TabIndex = 35;
            this.btnExpExcel.Text = "导出";
            this.btnExpExcel.Click += new System.EventHandler(this.btnExpExcel_Click_1);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 40);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.MaximumSize = new System.Drawing.Size(0, 190);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1006, 35);
            this.flowLayoutPanel1.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 375);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1006, 45);
            this.panel1.TabIndex = 15;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 106);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1006, 269);
            this.gridControl1.TabIndex = 16;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // ctlAutoSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panelControl1);
            this.Name = "ctlAutoSearch";
            this.Size = new System.Drawing.Size(1006, 420);
            this.Load += new System.EventHandler(this.ctlAutoSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkConstant.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInfo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupItmes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtInfo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit lupItmes;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.DateEdit beginTime;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.DateEdit endTime;
        private DXSimpleButton btnSearch;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DXSimpleButton btnExpExcel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DXSimpleButton btnControlP;
        private DXSimpleButton btnReport;
        private DXSimpleButton btnClear;
        private DXSimpleButton btnLoad;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.CheckEdit chkConstant;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
