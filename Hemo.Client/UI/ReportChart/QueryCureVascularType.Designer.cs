namespace Hemo.Client.UI.ReportChart
{
    partial class QueryCureVascularType
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel1 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel2 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView2 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel3 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView3 = new DevExpress.XtraCharts.LineSeriesView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.chkConstant = new DevExpress.XtraEditors.CheckEdit();
            this.btnNote = new Hemo.Client.Controls.DXSimpleButton();
            this.btn_print = new Hemo.Client.Controls.DXSimpleButton();
            this.btn_ExpExcel = new Hemo.Client.Controls.DXSimpleButton();
            this.dxSimpleButton2 = new Hemo.Client.Controls.DXSimpleButton();
            this.dxSimpleButton1 = new Hemo.Client.Controls.DXSimpleButton();
            this.btn_Query = new Hemo.Client.Controls.DXSimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.endTime = new DevExpress.XtraEditors.DateEdit();
            this.beginTime = new DevExpress.XtraEditors.DateEdit();
            this.gcCount = new DevExpress.XtraGrid.GridControl();
            this.gvCount = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.busyIndicator = new Hemo.Client.Controls.BusyIndicator();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkConstant.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView3)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.chkConstant);
            this.panelControl1.Controls.Add(this.btnNote);
            this.panelControl1.Controls.Add(this.btn_print);
            this.panelControl1.Controls.Add(this.btn_ExpExcel);
            this.panelControl1.Controls.Add(this.dxSimpleButton2);
            this.panelControl1.Controls.Add(this.dxSimpleButton1);
            this.panelControl1.Controls.Add(this.btn_Query);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.endTime);
            this.panelControl1.Controls.Add(this.beginTime);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 472);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1120, 38);
            this.panelControl1.TabIndex = 4;
            // 
            // chkConstant
            // 
            this.chkConstant.EditValue = true;
            this.chkConstant.Location = new System.Drawing.Point(367, 8);
            this.chkConstant.Name = "chkConstant";
            this.chkConstant.Properties.Caption = "维持性透析";
            this.chkConstant.Size = new System.Drawing.Size(80, 19);
            this.chkConstant.TabIndex = 814;
            // 
            // btnNote
            // 
            this.btnNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNote.ImageIndex = 4;
            this.btnNote.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnNote.Location = new System.Drawing.Point(900, 6);
            this.btnNote.Name = "btnNote";
            this.btnNote.Size = new System.Drawing.Size(85, 25);
            this.btnNote.TabIndex = 813;
            this.btnNote.Text = "报表说明";
            this.btnNote.Click += new System.EventHandler(this.btnNote_Click);
            // 
            // btn_print
            // 
            this.btn_print.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_print.ImageIndex = 6;
            this.btn_print.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_print.Location = new System.Drawing.Point(991, 6);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(85, 25);
            this.btn_print.TabIndex = 30;
            this.btn_print.Text = "导出图片";
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // btn_ExpExcel
            // 
            this.btn_ExpExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ExpExcel.ImageIndex = 9;
            this.btn_ExpExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_ExpExcel.Location = new System.Drawing.Point(809, 6);
            this.btn_ExpExcel.Name = "btn_ExpExcel";
            this.btn_ExpExcel.Size = new System.Drawing.Size(85, 25);
            this.btn_ExpExcel.TabIndex = 30;
            this.btn_ExpExcel.Text = "导出Excel";
            this.btn_ExpExcel.Click += new System.EventHandler(this.btn_ExpExcel_Click);
            // 
            // dxSimpleButton2
            // 
            this.dxSimpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dxSimpleButton2.ImageIndex = 8;
            this.dxSimpleButton2.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.dxSimpleButton2.Location = new System.Drawing.Point(544, 6);
            this.dxSimpleButton2.Name = "dxSimpleButton2";
            this.dxSimpleButton2.Size = new System.Drawing.Size(85, 25);
            this.dxSimpleButton2.TabIndex = 30;
            this.dxSimpleButton2.Text = "导管患者";
            this.dxSimpleButton2.Click += new System.EventHandler(this.dxSimpleButton2_Click);
            // 
            // dxSimpleButton1
            // 
            this.dxSimpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dxSimpleButton1.ImageIndex = 8;
            this.dxSimpleButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.dxSimpleButton1.Location = new System.Drawing.Point(453, 6);
            this.dxSimpleButton1.Name = "dxSimpleButton1";
            this.dxSimpleButton1.Size = new System.Drawing.Size(85, 25);
            this.dxSimpleButton1.TabIndex = 30;
            this.dxSimpleButton1.Text = "内瘘患者";
            this.dxSimpleButton1.Click += new System.EventHandler(this.dxSimpleButton1_Click);
            // 
            // btn_Query
            // 
            this.btn_Query.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Query.ImageIndex = 8;
            this.btn_Query.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_Query.Location = new System.Drawing.Point(718, 6);
            this.btn_Query.Name = "btn_Query";
            this.btn_Query.Size = new System.Drawing.Size(85, 25);
            this.btn_Query.TabIndex = 30;
            this.btn_Query.Text = "查    询";
            this.btn_Query.Click += new System.EventHandler(this.btn_Query_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(186, 11);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 14);
            this.labelControl3.TabIndex = 28;
            this.labelControl3.Text = "结束日期:";
            this.labelControl3.ToolTip = "双击可以修改结束日期";
            this.labelControl3.DoubleClick += new System.EventHandler(this.labelControl3_DoubleClick);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 14);
            this.labelControl1.TabIndex = 27;
            this.labelControl1.Text = "开始日期:";
            // 
            // endTime
            // 
            this.endTime.EditValue = null;
            this.endTime.Enabled = false;
            this.endTime.Location = new System.Drawing.Point(244, 8);
            this.endTime.Name = "endTime";
            this.endTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.endTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.endTime.Size = new System.Drawing.Size(108, 20);
            this.endTime.TabIndex = 25;
            // 
            // beginTime
            // 
            this.beginTime.EditValue = null;
            this.beginTime.Location = new System.Drawing.Point(63, 8);
            this.beginTime.Name = "beginTime";
            this.beginTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.beginTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beginTime.Size = new System.Drawing.Size(108, 20);
            this.beginTime.TabIndex = 26;
            this.beginTime.EditValueChanged += new System.EventHandler(this.beginTime_EditValueChanged);
            // 
            // gcCount
            // 
            this.gcCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcCount.Location = new System.Drawing.Point(0, 293);
            this.gcCount.MainView = this.gvCount;
            this.gcCount.Name = "gcCount";
            this.gcCount.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1,
            this.repositoryItemButtonEdit2});
            this.gcCount.Size = new System.Drawing.Size(1120, 179);
            this.gcCount.TabIndex = 82;
            this.gcCount.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCount});
            this.gcCount.Click += new System.EventHandler(this.gcCount_Click);
            // 
            // gvCount
            // 
            this.gvCount.ActiveFilterEnabled = false;
            this.gvCount.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn6,
            this.gridColumn2});
            this.gvCount.GridControl = this.gcCount;
            this.gvCount.Name = "gvCount";
            this.gvCount.OptionsBehavior.Editable = false;
            this.gvCount.OptionsCustomization.AllowColumnMoving = false;
            this.gvCount.OptionsCustomization.AllowColumnResizing = false;
            this.gvCount.OptionsCustomization.AllowFilter = false;
            this.gvCount.OptionsCustomization.AllowGroup = false;
            this.gvCount.OptionsCustomization.AllowQuickHideColumns = false;
            this.gvCount.OptionsView.ShowGroupPanel = false;
            this.gvCount.DoubleClick += new System.EventHandler(this.gvCount_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "日期";
            this.gridColumn1.FieldName = "CREATE_MONTH";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "内瘘人数";
            this.gridColumn3.FieldName = "NL_COUNT";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "导管人数";
            this.gridColumn6.FieldName = "ZXJM_COUNT";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "汇总（人数）";
            this.gridColumn2.FieldName = "SUB_COUNT";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // repositoryItemButtonEdit2
            // 
            this.repositoryItemButtonEdit2.AutoHeight = false;
            this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
            this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // busyIndicator
            // 
            this.busyIndicator.Location = new System.Drawing.Point(489, 174);
            this.busyIndicator.Name = "busyIndicator";
            this.busyIndicator.Size = new System.Drawing.Size(100, 40);
            this.busyIndicator.TabIndex = 84;
            this.busyIndicator.Visible = false;
            // 
            // chartControl1
            // 
            this.chartControl1.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisX.VisualRange.Auto = false;
            xyDiagram1.AxisX.VisualRange.MaxValueSerializable = "4";
            xyDiagram1.AxisX.VisualRange.MinValueSerializable = "0";
            xyDiagram1.AxisX.WholeRange.Auto = false;
            xyDiagram1.AxisX.WholeRange.MaxValueSerializable = "4";
            xyDiagram1.AxisX.WholeRange.MinValueSerializable = "0";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.EnableAxisXScrolling = true;
            xyDiagram1.Margins.Top = 0;
            this.chartControl1.Diagram = xyDiagram1;
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.chartControl1.Location = new System.Drawing.Point(0, 0);
            this.chartControl1.Name = "chartControl1";
            pointSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
            pointSeriesLabel1.ResolveOverlappingMode = DevExpress.XtraCharts.ResolveOverlappingMode.JustifyAllAroundPoint;
            series1.Label = pointSeriesLabel1;
            series1.Name = "Series 1";
            series1.View = lineSeriesView1;
            pointSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
            pointSeriesLabel2.ResolveOverlappingMode = DevExpress.XtraCharts.ResolveOverlappingMode.JustifyAllAroundPoint;
            series2.Label = pointSeriesLabel2;
            series2.Name = "Series 2";
            series2.View = lineSeriesView2;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2};
            pointSeriesLabel3.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartControl1.SeriesTemplate.Label = pointSeriesLabel3;
            this.chartControl1.SeriesTemplate.View = lineSeriesView3;
            this.chartControl1.Size = new System.Drawing.Size(1120, 293);
            this.chartControl1.TabIndex = 85;
            // 
            // QueryCureVascularType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcCount);
            this.Controls.Add(this.chartControl1);
            this.Controls.Add(this.busyIndicator);
            this.Controls.Add(this.panelControl1);
            this.Name = "QueryCureVascularType";
            this.Size = new System.Drawing.Size(1120, 510);
            this.Load += new System.EventHandler(this.QueryCureVascularType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkConstant.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Controls.DXSimpleButton btn_print;
        private Controls.DXSimpleButton btn_ExpExcel;
        private Controls.DXSimpleButton btn_Query;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit endTime;
        private DevExpress.XtraEditors.DateEdit beginTime;
        private DevExpress.XtraGrid.GridControl gcCount;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCount;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private Controls.DXSimpleButton btnNote;
        private Controls.BusyIndicator busyIndicator;
        private DevExpress.XtraEditors.CheckEdit chkConstant;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private Controls.DXSimpleButton dxSimpleButton2;
        private Controls.DXSimpleButton dxSimpleButton1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit2;


    }
}
