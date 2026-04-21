namespace Hemo.Client.UI.ReportChart
{
    partial class QueryHemoCureReport
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
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel1 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel2 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel3 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel4 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            DevExpress.XtraCharts.Series series5 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel5 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel6 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            this.ctlYearQuery = new Hemo.Client.Controls.CtlYearQuery();
            this.tcHemoCure = new DevExpress.XtraTab.XtraTabControl();
            this.tpHemoGrid = new DevExpress.XtraTab.XtraTabPage();
            this.gcHemoCure = new DevExpress.XtraGrid.GridControl();
            this.gvHemoCure = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tpHemoChart = new DevExpress.XtraTab.XtraTabPage();
            this.cclHemoCure = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.tcHemoCure)).BeginInit();
            this.tcHemoCure.SuspendLayout();
            this.tpHemoGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcHemoCure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHemoCure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cclHemoCure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel6)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlYearQuery
            // 
            this.ctlYearQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlYearQuery.IsRptVisble = false;
            this.ctlYearQuery.Location = new System.Drawing.Point(0, 0);
            this.ctlYearQuery.Name = "ctlYearQuery";
            this.ctlYearQuery.Size = new System.Drawing.Size(840, 84);
            this.ctlYearQuery.TabIndex = 2;
            // 
            // tcHemoCure
            // 
            this.tcHemoCure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcHemoCure.Location = new System.Drawing.Point(0, 84);
            this.tcHemoCure.Name = "tcHemoCure";
            this.tcHemoCure.SelectedTabPage = this.tpHemoGrid;
            this.tcHemoCure.Size = new System.Drawing.Size(840, 426);
            this.tcHemoCure.TabIndex = 3;
            this.tcHemoCure.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpHemoGrid,
            this.tpHemoChart});
            // 
            // tpHemoGrid
            // 
            this.tpHemoGrid.Controls.Add(this.cclHemoCure);
            this.tpHemoGrid.Controls.Add(this.gcHemoCure);
            this.tpHemoGrid.Name = "tpHemoGrid";
            this.tpHemoGrid.Size = new System.Drawing.Size(834, 397);
            this.tpHemoGrid.Text = "血透治疗数据报表";
            // 
            // gcHemoCure
            // 
            this.gcHemoCure.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gcHemoCure.Location = new System.Drawing.Point(0, 199);
            this.gcHemoCure.MainView = this.gvHemoCure;
            this.gcHemoCure.Name = "gcHemoCure";
            this.gcHemoCure.Size = new System.Drawing.Size(834, 198);
            this.gcHemoCure.TabIndex = 0;
            this.gcHemoCure.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvHemoCure});
            // 
            // gvHemoCure
            // 
            this.gvHemoCure.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.gvHemoCure.GridControl = this.gcHemoCure;
            this.gvHemoCure.Name = "gvHemoCure";
            this.gvHemoCure.OptionsBehavior.Editable = false;
            this.gvHemoCure.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvHemoCure.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "日期";
            this.gridColumn1.FieldName = "DATE_MONTH";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "HD例数";
            this.gridColumn2.FieldName = "HD_COUNT";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "HDF例数";
            this.gridColumn3.FieldName = "HDF_COUNT";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "HF例数";
            this.gridColumn4.FieldName = "HF_COUNT";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "HP例数";
            this.gridColumn5.FieldName = "HP_COUNT";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "HD|HP例数";
            this.gridColumn6.FieldName = "HD_HP_COUNT";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            // 
            // tpHemoChart
            // 
            this.tpHemoChart.Name = "tpHemoChart";
            this.tpHemoChart.PageVisible = false;
            this.tpHemoChart.Size = new System.Drawing.Size(834, 397);
            this.tpHemoChart.Text = "血透治疗报表";
            // 
            // cclHemoCure
            // 
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.cclHemoCure.Diagram = xyDiagram1;
            this.cclHemoCure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cclHemoCure.Location = new System.Drawing.Point(0, 0);
            this.cclHemoCure.Name = "cclHemoCure";
            sideBySideBarSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
            series1.Label = sideBySideBarSeriesLabel1;
            series1.Name = "Series 1";
            sideBySideBarSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
            series2.Label = sideBySideBarSeriesLabel2;
            series2.Name = "Series 2";
            sideBySideBarSeriesLabel3.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
            series3.Label = sideBySideBarSeriesLabel3;
            series3.Name = "Series 3";
            sideBySideBarSeriesLabel4.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
            series4.Label = sideBySideBarSeriesLabel4;
            series4.Name = "Series 4";
            sideBySideBarSeriesLabel5.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
            series5.Label = sideBySideBarSeriesLabel5;
            series5.Name = "Series 5";
            this.cclHemoCure.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2,
        series3,
        series4,
        series5};
            sideBySideBarSeriesLabel6.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
            this.cclHemoCure.SeriesTemplate.Label = sideBySideBarSeriesLabel6;
            this.cclHemoCure.Size = new System.Drawing.Size(834, 199);
            this.cclHemoCure.TabIndex = 1;
            // 
            // QueryHemoCureReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcHemoCure);
            this.Controls.Add(this.ctlYearQuery);
            this.Name = "QueryHemoCureReport";
            this.Size = new System.Drawing.Size(840, 510);
            this.Load += new System.EventHandler(this.QueryHemoCureReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tcHemoCure)).EndInit();
            this.tcHemoCure.ResumeLayout(false);
            this.tpHemoGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcHemoCure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHemoCure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cclHemoCure)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CtlYearQuery ctlYearQuery;
        private DevExpress.XtraTab.XtraTabControl tcHemoCure;
        private DevExpress.XtraTab.XtraTabPage tpHemoGrid;
        private DevExpress.XtraTab.XtraTabPage tpHemoChart;
        private DevExpress.XtraGrid.GridControl gcHemoCure;
        private DevExpress.XtraGrid.Views.Grid.GridView gvHemoCure;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraCharts.ChartControl cclHemoCure;

    }
}
