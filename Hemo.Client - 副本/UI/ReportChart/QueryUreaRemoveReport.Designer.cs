namespace Hemo.Client.UI.ReportChart
{
    partial class QueryUreaRemoveReport
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
            DevExpress.XtraCharts.XYDiagram xyDiagram5 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series5 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel9 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel10 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            this.ctlYearQuery = new Hemo.Client.Controls.CtlYearQuery();
            this.tcUreaRemove = new DevExpress.XtraTab.XtraTabControl();
            this.tpUreaGrid = new DevExpress.XtraTab.XtraTabPage();
            this.tpUreaChart = new DevExpress.XtraTab.XtraTabPage();
            this.gcUreaRemove = new DevExpress.XtraGrid.GridControl();
            this.gvUreaRemove = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cclUreaRemove = new DevExpress.XtraCharts.ChartControl();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tcUreaRemove)).BeginInit();
            this.tcUreaRemove.SuspendLayout();
            this.tpUreaGrid.SuspendLayout();
            this.tpUreaChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcUreaRemove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUreaRemove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cclUreaRemove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel10)).BeginInit();
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
            // tcUreaRemove
            // 
            this.tcUreaRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcUreaRemove.Location = new System.Drawing.Point(0, 48);
            this.tcUreaRemove.Name = "tcUreaRemove";
            this.tcUreaRemove.SelectedTabPage = this.tpUreaGrid;
            this.tcUreaRemove.Size = new System.Drawing.Size(840, 462);
            this.tcUreaRemove.TabIndex = 1;
            this.tcUreaRemove.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpUreaGrid,
            this.tpUreaChart});
            // 
            // tpUreaGrid
            // 
            this.tpUreaGrid.Controls.Add(this.gcUreaRemove);
            this.tpUreaGrid.Name = "tpUreaGrid";
            this.tpUreaGrid.Size = new System.Drawing.Size(833, 432);
            this.tpUreaGrid.Text = "溶质清除列表";
            // 
            // tpUreaChart
            // 
            this.tpUreaChart.Controls.Add(this.cclUreaRemove);
            this.tpUreaChart.Name = "tpUreaChart";
            this.tpUreaChart.Size = new System.Drawing.Size(833, 432);
            this.tpUreaChart.Text = "溶质清除报表";
            // 
            // gcUreaRemove
            // 
            this.gcUreaRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcUreaRemove.Location = new System.Drawing.Point(0, 0);
            this.gcUreaRemove.MainView = this.gvUreaRemove;
            this.gcUreaRemove.Name = "gcUreaRemove";
            this.gcUreaRemove.Size = new System.Drawing.Size(833, 432);
            this.gcUreaRemove.TabIndex = 0;
            this.gcUreaRemove.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUreaRemove});
            // 
            // gvUreaRemove
            // 
            this.gvUreaRemove.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gvUreaRemove.GridControl = this.gcUreaRemove;
            this.gvUreaRemove.Name = "gvUreaRemove";
            this.gvUreaRemove.OptionsBehavior.Editable = false;
            this.gvUreaRemove.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvUreaRemove.OptionsView.ShowGroupPanel = false;
            // 
            // cclUreaRemove
            // 
            xyDiagram5.AxisX.Range.ScrollingRange.SideMarginsEnabled = true;
            xyDiagram5.AxisX.Range.SideMarginsEnabled = true;
            xyDiagram5.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram5.AxisY.Range.ScrollingRange.SideMarginsEnabled = true;
            xyDiagram5.AxisY.Range.SideMarginsEnabled = true;
            xyDiagram5.AxisY.VisibleInPanesSerializable = "-1";
            this.cclUreaRemove.Diagram = xyDiagram5;
            this.cclUreaRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cclUreaRemove.Location = new System.Drawing.Point(0, 0);
            this.cclUreaRemove.Name = "cclUreaRemove";
            sideBySideBarSeriesLabel9.LineVisible = true;
            series5.Label = sideBySideBarSeriesLabel9;
            series5.Name = "Series 1";
            this.cclUreaRemove.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series5};
            sideBySideBarSeriesLabel10.LineVisible = true;
            this.cclUreaRemove.SeriesTemplate.Label = sideBySideBarSeriesLabel10;
            this.cclUreaRemove.Size = new System.Drawing.Size(833, 432);
            this.cclUreaRemove.TabIndex = 0;
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
            this.gridColumn2.Caption = "溶质清除例数";
            this.gridColumn2.FieldName = "UREA_COUNT";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // QueryUreaRemoveReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcUreaRemove);
            this.Controls.Add(this.ctlYearQuery);
            this.Name = "QueryUreaRemoveReport";
            this.Size = new System.Drawing.Size(840, 510);
            this.Load += new System.EventHandler(this.QueryUreaRemoveReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tcUreaRemove)).EndInit();
            this.tcUreaRemove.ResumeLayout(false);
            this.tpUreaGrid.ResumeLayout(false);
            this.tpUreaChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcUreaRemove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUreaRemove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cclUreaRemove)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CtlYearQuery ctlYearQuery;
        private DevExpress.XtraTab.XtraTabControl tcUreaRemove;
        private DevExpress.XtraTab.XtraTabPage tpUreaGrid;
        private DevExpress.XtraTab.XtraTabPage tpUreaChart;
        private DevExpress.XtraGrid.GridControl gcUreaRemove;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUreaRemove;
        private DevExpress.XtraCharts.ChartControl cclUreaRemove;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    }
}
