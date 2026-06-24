namespace Hemo.Client.UI.ReportChart
{
    partial class QueryRenalAnemiaReport
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
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel2 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            this.ctlYearQuery = new Hemo.Client.Controls.CtlYearQuery();
            this.tcRenalAnemia = new DevExpress.XtraTab.XtraTabControl();
            this.tpRenalAnemiaGrid = new DevExpress.XtraTab.XtraTabPage();
            this.gcRenalAnemia = new DevExpress.XtraGrid.GridControl();
            this.gvRenalAnemia = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tpRenalAnemiaChart = new DevExpress.XtraTab.XtraTabPage();
            this.cclRenalAnemia = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.tcRenalAnemia)).BeginInit();
            this.tcRenalAnemia.SuspendLayout();
            this.tpRenalAnemiaGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcRenalAnemia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRenalAnemia)).BeginInit();
            this.tpRenalAnemiaChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cclRenalAnemia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlYearQuery
            // 
            this.ctlYearQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlYearQuery.Location = new System.Drawing.Point(0, 0);
            this.ctlYearQuery.Name = "ctlYearQuery";
            this.ctlYearQuery.Size = new System.Drawing.Size(840, 48);
            this.ctlYearQuery.TabIndex = 0;
            // 
            // tcRenalAnemia
            // 
            this.tcRenalAnemia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcRenalAnemia.Location = new System.Drawing.Point(0, 48);
            this.tcRenalAnemia.Name = "tcRenalAnemia";
            this.tcRenalAnemia.SelectedTabPage = this.tpRenalAnemiaGrid;
            this.tcRenalAnemia.Size = new System.Drawing.Size(840, 462);
            this.tcRenalAnemia.TabIndex = 1;
            this.tcRenalAnemia.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpRenalAnemiaGrid,
            this.tpRenalAnemiaChart});
            // 
            // tpRenalAnemiaGrid
            // 
            this.tpRenalAnemiaGrid.Controls.Add(this.gcRenalAnemia);
            this.tpRenalAnemiaGrid.Name = "tpRenalAnemiaGrid";
            this.tpRenalAnemiaGrid.Size = new System.Drawing.Size(833, 432);
            this.tpRenalAnemiaGrid.Text = "肾性贫血列表";
            // 
            // gcRenalAnemia
            // 
            this.gcRenalAnemia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcRenalAnemia.Location = new System.Drawing.Point(0, 0);
            this.gcRenalAnemia.MainView = this.gvRenalAnemia;
            this.gcRenalAnemia.Name = "gcRenalAnemia";
            this.gcRenalAnemia.Size = new System.Drawing.Size(833, 432);
            this.gcRenalAnemia.TabIndex = 0;
            this.gcRenalAnemia.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRenalAnemia});
            // 
            // gvRenalAnemia
            // 
            this.gvRenalAnemia.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gvRenalAnemia.GridControl = this.gcRenalAnemia;
            this.gvRenalAnemia.Name = "gvRenalAnemia";
            this.gvRenalAnemia.OptionsBehavior.Editable = false;
            this.gvRenalAnemia.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvRenalAnemia.OptionsView.ShowGroupPanel = false;
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
            this.gridColumn2.Caption = "肾性贫血纠正例数";
            this.gridColumn2.FieldName = "RENALANEMIA_COUNT";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // tpRenalAnemiaChart
            // 
            this.tpRenalAnemiaChart.Controls.Add(this.cclRenalAnemia);
            this.tpRenalAnemiaChart.Name = "tpRenalAnemiaChart";
            this.tpRenalAnemiaChart.Size = new System.Drawing.Size(833, 432);
            this.tpRenalAnemiaChart.Text = "肾性贫血报表";
            // 
            // cclRenalAnemia
            // 
            xyDiagram1.AxisX.Range.ScrollingRange.SideMarginsEnabled = true;
            xyDiagram1.AxisX.Range.SideMarginsEnabled = true;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.Range.ScrollingRange.SideMarginsEnabled = true;
            xyDiagram1.AxisY.Range.SideMarginsEnabled = true;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.cclRenalAnemia.Diagram = xyDiagram1;
            this.cclRenalAnemia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cclRenalAnemia.Location = new System.Drawing.Point(0, 0);
            this.cclRenalAnemia.Name = "cclRenalAnemia";
            sideBySideBarSeriesLabel1.LineVisible = true;
            series1.Label = sideBySideBarSeriesLabel1;
            series1.Name = "Series 1";
            this.cclRenalAnemia.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            sideBySideBarSeriesLabel2.LineVisible = true;
            this.cclRenalAnemia.SeriesTemplate.Label = sideBySideBarSeriesLabel2;
            this.cclRenalAnemia.Size = new System.Drawing.Size(833, 432);
            this.cclRenalAnemia.TabIndex = 0;
            // 
            // QueryRenalAnemiaReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcRenalAnemia);
            this.Controls.Add(this.ctlYearQuery);
            this.Name = "QueryRenalAnemiaReport";
            this.Size = new System.Drawing.Size(840, 510);
            this.Load += new System.EventHandler(this.QueryRenalAnemiaReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tcRenalAnemia)).EndInit();
            this.tcRenalAnemia.ResumeLayout(false);
            this.tpRenalAnemiaGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcRenalAnemia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRenalAnemia)).EndInit();
            this.tpRenalAnemiaChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cclRenalAnemia)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CtlYearQuery ctlYearQuery;
        private DevExpress.XtraTab.XtraTabControl tcRenalAnemia;
        private DevExpress.XtraTab.XtraTabPage tpRenalAnemiaGrid;
        private DevExpress.XtraTab.XtraTabPage tpRenalAnemiaChart;
        private DevExpress.XtraGrid.GridControl gcRenalAnemia;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRenalAnemia;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraCharts.ChartControl cclRenalAnemia;
    }
}
