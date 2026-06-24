namespace Hemo.Client.UI.Hemodialysis
{
    partial class QueryDrugRecord
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
            this.ctlUserLongInfo1 = new Hemo.Client.Controls.CtlUserLongInfo();
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
            this.btnCopy = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_Time.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_DATE.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_DATE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_Time.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_DATE.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_DATE.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlUserLongInfo1
            // 
            this.ctlUserLongInfo1.FormContainer = null;
            this.ctlUserLongInfo1.HEMODIALYSIS_ID = "";
            this.ctlUserLongInfo1.Location = new System.Drawing.Point(8, 8);
            this.ctlUserLongInfo1.Name = "ctlUserLongInfo1";
            this.ctlUserLongInfo1.PanBorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.ctlUserLongInfo1.PanelWidth = 776;
            this.ctlUserLongInfo1.PatientType = "";
            this.ctlUserLongInfo1.PatientTypeEnabled = true;
            this.ctlUserLongInfo1.Size = new System.Drawing.Size(776, 44);
            this.ctlUserLongInfo1.TabIndex = 258;
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(8, 93);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(776, 247);
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
            this.cmbSTART_Time.Location = new System.Drawing.Point(99, 63);
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
            this.cmbSTART_DATE.Location = new System.Drawing.Point(9, 62);
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
            this.labelControl62.Location = new System.Drawing.Point(186, 66);
            this.labelControl62.Name = "labelControl62";
            this.labelControl62.Size = new System.Drawing.Size(9, 17);
            this.labelControl62.TabIndex = 381;
            this.labelControl62.Text = "~";
            // 
            // cmbEND_Time
            // 
            this.cmbEND_Time.EditValue = new System.DateTime(2013, 4, 8, 0, 0, 0, 0);
            this.cmbEND_Time.Location = new System.Drawing.Point(291, 64);
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
            this.cmbEND_DATE.Location = new System.Drawing.Point(201, 63);
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
            // btnCopy
            // 
            this.btnCopy.ImageIndex = 4;
            this.btnCopy.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnCopy.Location = new System.Drawing.Point(709, 354);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 384;
            this.btnCopy.Text = "打印(&P)";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.ImageIndex = 4;
            this.btnSearch.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(378, 63);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 385;
            this.btnSearch.Text = "查询(&Q)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // QueryDrugRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 389);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.cmbEND_Time);
            this.Controls.Add(this.cmbEND_DATE);
            this.Controls.Add(this.labelControl62);
            this.Controls.Add(this.cmbSTART_Time);
            this.Controls.Add(this.cmbSTART_DATE);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.ctlUserLongInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "QueryDrugRecord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "用药记录";
            this.Load += new System.EventHandler(this.QueryDrugRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_Time.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_DATE.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_DATE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_Time.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_DATE.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_DATE.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CtlUserLongInfo ctlUserLongInfo1;
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
        private DevExpress.XtraEditors.SimpleButton btnCopy;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
    }
}