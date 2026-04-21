namespace Hemo.Client.UI.ReportChart
{
    partial class QueryInfectionScaleReport
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
            DevExpress.XtraCharts.SimpleDiagram3D simpleDiagram3D1 = new DevExpress.XtraCharts.SimpleDiagram3D();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.Pie3DSeriesLabel pie3DSeriesLabel1 = new DevExpress.XtraCharts.Pie3DSeriesLabel();
            DevExpress.XtraCharts.Pie3DSeriesView pie3DSeriesView1 = new DevExpress.XtraCharts.Pie3DSeriesView();
            DevExpress.XtraCharts.Pie3DSeriesLabel pie3DSeriesLabel2 = new DevExpress.XtraCharts.Pie3DSeriesLabel();
            DevExpress.XtraCharts.Pie3DSeriesView pie3DSeriesView2 = new DevExpress.XtraCharts.Pie3DSeriesView();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.dtMonth = new DevExpress.XtraEditors.DateEdit();
            this.btn_ExpExcel = new Hemo.Client.Controls.DXSimpleButton();
            this.btn_Query = new Hemo.Client.Controls.DXSimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gcCount = new DevExpress.XtraGrid.GridControl();
            this.gvCount = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.busyIndicator = new Hemo.Client.Controls.BusyIndicator();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(simpleDiagram3D1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pie3DSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pie3DSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pie3DSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pie3DSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtMonth.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCount)).BeginInit();
            this.SuspendLayout();
            // 
            // chartControl1
            // 
            simpleDiagram3D1.RotationMatrixSerializable = "1;0;0;0;0;0.5;-0.866025403784439;0;0;0.866025403784439;0.5;0;0;0;0;1";
            this.chartControl1.Diagram = simpleDiagram3D1;
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.chartControl1.Location = new System.Drawing.Point(600, 0);
            this.chartControl1.Name = "chartControl1";
            pie3DSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
            series1.Label = pie3DSeriesLabel1;
            series1.Name = "Series 1";
            series1.View = pie3DSeriesView1;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            pie3DSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartControl1.SeriesTemplate.Label = pie3DSeriesLabel2;
            this.chartControl1.SeriesTemplate.View = pie3DSeriesView2;
            this.chartControl1.Size = new System.Drawing.Size(334, 472);
            this.chartControl1.TabIndex = 4;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.dtMonth);
            this.panelControl1.Controls.Add(this.btn_ExpExcel);
            this.panelControl1.Controls.Add(this.btn_Query);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 472);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(934, 38);
            this.panelControl1.TabIndex = 5;
            // 
            // dtMonth
            // 
            this.dtMonth.EditValue = null;
            this.dtMonth.Location = new System.Drawing.Point(82, 11);
            this.dtMonth.Name = "dtMonth";
            this.dtMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtMonth.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtMonth.Properties.DisplayFormat.FormatString = "yyyy年MM月";
            this.dtMonth.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtMonth.Properties.EditFormat.FormatString = "yyyy年MM月";
            this.dtMonth.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtMonth.Properties.Mask.EditMask = "y";
            this.dtMonth.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dtMonth.Size = new System.Drawing.Size(112, 20);
            this.dtMonth.TabIndex = 31;
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
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 27;
            this.labelControl1.Text = "统计月份：";
            // 
            // gcCount
            // 
            this.gcCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcCount.Location = new System.Drawing.Point(0, 0);
            this.gcCount.MainView = this.gvCount;
            this.gcCount.Name = "gcCount";
            this.gcCount.Size = new System.Drawing.Size(600, 472);
            this.gcCount.TabIndex = 83;
            this.gcCount.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCount});
            // 
            // gvCount
            // 
            this.gvCount.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn2,
            this.gridColumn5});
            this.gvCount.GridControl = this.gcCount;
            this.gvCount.Name = "gvCount";
            this.gvCount.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvCount.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvCount.OptionsBehavior.Editable = false;
            this.gvCount.OptionsBehavior.ReadOnly = true;
            this.gvCount.OptionsView.ShowGroupPanel = false;
            this.gvCount.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvCount_RowClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "日期";
            this.gridColumn1.FieldName = "CREATETIME";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "项目名称";
            this.gridColumn3.FieldName = "ITEMNAME";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "项目例数";
            this.gridColumn4.FieldName = "ITEMCOUNT";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "患者总数";
            this.gridColumn2.FieldName = "PATIENTCOUNT";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "比例";
            this.gridColumn5.FieldName = "ITEMSCALE";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // busyIndicator
            // 
            this.busyIndicator.Location = new System.Drawing.Point(500, 200);
            this.busyIndicator.Name = "busyIndicator";
            this.busyIndicator.Size = new System.Drawing.Size(100, 40);
            this.busyIndicator.TabIndex = 85;
            this.busyIndicator.Visible = false;
            // 
            // QueryInfectionScaleReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.busyIndicator);
            this.Controls.Add(this.gcCount);
            this.Controls.Add(this.chartControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "QueryInfectionScaleReport";
            this.Size = new System.Drawing.Size(934, 510);
            this.Load += new System.EventHandler(this.QueryInfectionScaleReport_Load);
            ((System.ComponentModel.ISupportInitialize)(simpleDiagram3D1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pie3DSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pie3DSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pie3DSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pie3DSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtMonth.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Controls.DXSimpleButton btn_ExpExcel;
        private Controls.DXSimpleButton btn_Query;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl gcCount;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCount;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private Controls.BusyIndicator busyIndicator;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.DateEdit dtMonth;

    }
}
