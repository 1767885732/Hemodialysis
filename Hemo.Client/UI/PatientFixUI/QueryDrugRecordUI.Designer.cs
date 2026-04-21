namespace Hemo.Client.UI.Hemodialysis
{
    partial class QueryDrugRecordUI
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbSTART_Time = new DevExpress.XtraEditors.TimeEdit();
            this.cmbSTART_DATE = new DevExpress.XtraEditors.DateEdit();
            this.labelControl62 = new DevExpress.XtraEditors.LabelControl();
            this.cmbEND_Time = new DevExpress.XtraEditors.TimeEdit();
            this.cmbEND_DATE = new DevExpress.XtraEditors.DateEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnPrint = new Hemo.Client.Controls.DXSimpleButton();
            this.btn_Query = new Hemo.Client.Controls.DXSimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_Time.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_DATE.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_DATE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_Time.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_DATE.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_DATE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 2);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(787, 344);
            this.gridControl1.TabIndex = 259;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Appearance.RowSeparator.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.gridView1.Appearance.RowSeparator.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "时间";
            this.gridColumn4.DisplayFormat.FormatString = "HH:mm";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn4.FieldName = "CREATE_DATE";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 100;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "记录护士";
            this.gridColumn5.FieldName = "NAME";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 100;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "用药";
            this.gridColumn6.FieldName = "CLINICAL_MANIFESTATION";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 500;
            // 
            // cmbSTART_Time
            // 
            this.cmbSTART_Time.EditValue = new System.DateTime(2013, 4, 8, 0, 0, 0, 0);
            this.cmbSTART_Time.Location = new System.Drawing.Point(107, 11);
            this.cmbSTART_Time.Name = "cmbSTART_Time";
            this.cmbSTART_Time.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbSTART_Time.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.cmbSTART_Time.Properties.Appearance.Options.UseBackColor = true;
            this.cmbSTART_Time.Properties.Appearance.Options.UseForeColor = true;
            this.cmbSTART_Time.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbSTART_Time.Size = new System.Drawing.Size(81, 21);
            this.cmbSTART_Time.TabIndex = 261;
            // 
            // cmbSTART_DATE
            // 
            this.cmbSTART_DATE.EditValue = null;
            this.cmbSTART_DATE.EnterMoveNextControl = true;
            this.cmbSTART_DATE.Location = new System.Drawing.Point(17, 10);
            this.cmbSTART_DATE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbSTART_DATE.Name = "cmbSTART_DATE";
            this.cmbSTART_DATE.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbSTART_DATE.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.cmbSTART_DATE.Properties.Appearance.Options.UseFont = true;
            this.cmbSTART_DATE.Properties.Appearance.Options.UseForeColor = true;
            this.cmbSTART_DATE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSTART_DATE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cmbSTART_DATE.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbSTART_DATE.Size = new System.Drawing.Size(88, 23);
            this.cmbSTART_DATE.TabIndex = 260;
            // 
            // labelControl62
            // 
            this.labelControl62.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl62.Appearance.Options.UseFont = true;
            this.labelControl62.Location = new System.Drawing.Point(194, 14);
            this.labelControl62.Name = "labelControl62";
            this.labelControl62.Size = new System.Drawing.Size(9, 17);
            this.labelControl62.TabIndex = 381;
            this.labelControl62.Text = "~";
            // 
            // cmbEND_Time
            // 
            this.cmbEND_Time.EditValue = new System.DateTime(2013, 4, 8, 0, 0, 0, 0);
            this.cmbEND_Time.Location = new System.Drawing.Point(299, 12);
            this.cmbEND_Time.Name = "cmbEND_Time";
            this.cmbEND_Time.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbEND_Time.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.cmbEND_Time.Properties.Appearance.Options.UseBackColor = true;
            this.cmbEND_Time.Properties.Appearance.Options.UseForeColor = true;
            this.cmbEND_Time.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbEND_Time.Size = new System.Drawing.Size(81, 21);
            this.cmbEND_Time.TabIndex = 383;
            // 
            // cmbEND_DATE
            // 
            this.cmbEND_DATE.EditValue = null;
            this.cmbEND_DATE.EnterMoveNextControl = true;
            this.cmbEND_DATE.Location = new System.Drawing.Point(209, 11);
            this.cmbEND_DATE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbEND_DATE.Name = "cmbEND_DATE";
            this.cmbEND_DATE.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbEND_DATE.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.cmbEND_DATE.Properties.Appearance.Options.UseFont = true;
            this.cmbEND_DATE.Properties.Appearance.Options.UseForeColor = true;
            this.cmbEND_DATE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbEND_DATE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cmbEND_DATE.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbEND_DATE.Size = new System.Drawing.Size(88, 23);
            this.cmbEND_DATE.TabIndex = 382;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnPrint);
            this.panelControl1.Controls.Add(this.btn_Query);
            this.panelControl1.Controls.Add(this.cmbSTART_DATE);
            this.panelControl1.Controls.Add(this.cmbEND_Time);
            this.panelControl1.Controls.Add(this.cmbSTART_Time);
            this.panelControl1.Controls.Add(this.cmbEND_DATE);
            this.panelControl1.Controls.Add(this.labelControl62);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(791, 41);
            this.panelControl1.TabIndex = 386;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.ImageIndex = 6;
            this.btnPrint.Location = new System.Drawing.Point(702, 7);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(73, 27);
            this.btnPrint.TabIndex = 387;
            this.btnPrint.Text = "打印(&P)";
            this.btnPrint.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btn_Query
            // 
            this.btn_Query.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Query.ImageIndex = 8;
            this.btn_Query.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_Query.Location = new System.Drawing.Point(623, 7);
            this.btn_Query.Name = "btn_Query";
            this.btn_Query.Size = new System.Drawing.Size(73, 27);
            this.btn_Query.TabIndex = 386;
            this.btn_Query.Text = "查询(&Q)";
            this.btn_Query.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gridControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 41);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(791, 348);
            this.panelControl2.TabIndex = 387;
            // 
            // QueryDrugRecordUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "QueryDrugRecordUI";
            this.Size = new System.Drawing.Size(791, 389);
            this.Load += new System.EventHandler(this.QueryDrugRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_Time.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_DATE.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_DATE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_Time.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_DATE.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_DATE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.TimeEdit cmbSTART_Time;
        private DevExpress.XtraEditors.DateEdit cmbSTART_DATE;
        private DevExpress.XtraEditors.LabelControl labelControl62;
        private DevExpress.XtraEditors.TimeEdit cmbEND_Time;
        private DevExpress.XtraEditors.DateEdit cmbEND_DATE;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Controls.DXSimpleButton btn_Query;
        private Controls.DXSimpleButton btnPrint;
        private DevExpress.XtraEditors.PanelControl panelControl2;
    }
}