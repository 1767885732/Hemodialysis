namespace Hemo.Client.UI.ReportChart
{
    partial class QueryWorkloadFZ
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.gcWorkload = new DevExpress.XtraGrid.GridControl();
            this.gvWorkload = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.deEndTime = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.deBeginTime = new DevExpress.XtraEditors.DateEdit();
            this.labelControl74 = new DevExpress.XtraEditors.LabelControl();
            this.txtTJR = new DevExpress.XtraEditors.LookUpEdit();
            this.btnPrint = new Hemo.Client.Controls.DXSimpleButton();
            this.btnQuery = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcWorkload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWorkload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deEndTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBeginTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBeginTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTJR.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.panelControl3);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(962, 501);
            this.panelControl1.TabIndex = 0;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.gcWorkload);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(2, 53);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(958, 446);
            this.panelControl3.TabIndex = 1;
            // 
            // gcWorkload
            // 
            this.gcWorkload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcWorkload.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcWorkload.Location = new System.Drawing.Point(2, 2);
            this.gcWorkload.MainView = this.gvWorkload;
            this.gcWorkload.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcWorkload.Name = "gcWorkload";
            this.gcWorkload.Size = new System.Drawing.Size(954, 442);
            this.gcWorkload.TabIndex = 0;
            this.gcWorkload.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvWorkload});
            // 
            // gvWorkload
            // 
            this.gvWorkload.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvWorkload.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvWorkload.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvWorkload.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn3,
            this.gridColumn6});
            this.gvWorkload.GridControl = this.gcWorkload;
            this.gvWorkload.IndicatorWidth = 40;
            this.gvWorkload.Name = "gvWorkload";
            this.gvWorkload.OptionsBehavior.Editable = false;
            this.gvWorkload.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvWorkload.OptionsView.ShowGroupPanel = false;
            this.gvWorkload.RowHeight = 40;
            this.gvWorkload.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvWorkload_CustomDrawRowIndicator);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn1.Caption = "人员";
            this.gridColumn1.FieldName = "TJR";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "NURSE_ID";
            this.gridColumn2.FieldName = "NURSE_ID";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.Caption = "HD";
            this.gridColumn4.FieldName = "WGSXT";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn5.Caption = "HDF";
            this.gridColumn5.FieldName = "GTLTX";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.Caption = "HD+HP";
            this.gridColumn3.FieldName = "GYSXYTX";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn6.Caption = "合计";
            this.gridColumn6.FieldName = "QJRC";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.deEndTime);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.deBeginTime);
            this.panelControl2.Controls.Add(this.labelControl74);
            this.panelControl2.Controls.Add(this.txtTJR);
            this.panelControl2.Controls.Add(this.btnPrint);
            this.panelControl2.Controls.Add(this.btnQuery);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(958, 51);
            this.panelControl2.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(227, 16);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(11, 18);
            this.labelControl2.TabIndex = 434;
            this.labelControl2.Text = "~";
            // 
            // deEndTime
            // 
            this.deEndTime.EditValue = null;
            this.deEndTime.Location = new System.Drawing.Point(244, 13);
            this.deEndTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.deEndTime.Name = "deEndTime";
            this.deEndTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEndTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deEndTime.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.deEndTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deEndTime.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.deEndTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deEndTime.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.deEndTime.Size = new System.Drawing.Size(104, 24);
            this.deEndTime.TabIndex = 433;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(31, 16);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(75, 18);
            this.labelControl1.TabIndex = 432;
            this.labelControl1.Text = "日期范围：";
            // 
            // deBeginTime
            // 
            this.deBeginTime.EditValue = null;
            this.deBeginTime.Location = new System.Drawing.Point(118, 13);
            this.deBeginTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.deBeginTime.Name = "deBeginTime";
            this.deBeginTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deBeginTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deBeginTime.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.deBeginTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deBeginTime.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.deBeginTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deBeginTime.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.deBeginTime.Size = new System.Drawing.Size(104, 24);
            this.deBeginTime.TabIndex = 431;
            // 
            // labelControl74
            // 
            this.labelControl74.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl74.Location = new System.Drawing.Point(369, 16);
            this.labelControl74.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl74.Name = "labelControl74";
            this.labelControl74.Size = new System.Drawing.Size(75, 20);
            this.labelControl74.TabIndex = 430;
            this.labelControl74.Text = "责任护士：";
            // 
            // txtTJR
            // 
            this.txtTJR.EnterMoveNextControl = true;
            this.txtTJR.Location = new System.Drawing.Point(456, 13);
            this.txtTJR.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTJR.Name = "txtTJR";
            this.txtTJR.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTJR.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtTJR.Properties.Appearance.Options.UseFont = true;
            this.txtTJR.Properties.Appearance.Options.UseForeColor = true;
            this.txtTJR.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtTJR.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtTJR.Properties.NullText = "";
            this.txtTJR.Size = new System.Drawing.Size(103, 26);
            this.txtTJR.TabIndex = 429;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.ImageIndex = 6;
            this.btnPrint.Location = new System.Drawing.Point(839, 9);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(86, 32);
            this.btnPrint.TabIndex = 368;
            this.btnPrint.Text = "打印(&P)";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.ImageIndex = 8;
            this.btnQuery.Location = new System.Drawing.Point(747, 9);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(86, 32);
            this.btnQuery.TabIndex = 367;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // QueryWorkloadFZ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "QueryWorkloadFZ";
            this.Size = new System.Drawing.Size(962, 501);
            this.Load += new System.EventHandler(this.QueryWorkloadFZ_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcWorkload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWorkload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deEndTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBeginTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBeginTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTJR.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private Hemo.Client.Controls.DXSimpleButton btnQuery;
        private Hemo.Client.Controls.DXSimpleButton btnPrint;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraGrid.GridControl gcWorkload;
        private DevExpress.XtraGrid.Views.Grid.GridView gvWorkload;
        private DevExpress.XtraEditors.LabelControl labelControl74;
        private DevExpress.XtraEditors.LookUpEdit txtTJR;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit deEndTime;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit deBeginTime;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
    }
}