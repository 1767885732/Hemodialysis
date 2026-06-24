namespace Hemo.Client.UI.Hemodialysis
{
    partial class HemoParametersChart
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
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel1 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel2 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView2 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new Hemo.Client.Controls.DXSimpleButton();
            this.btnSearch = new Hemo.Client.Controls.DXSimpleButton();
            this.lueType = new DevExpress.XtraEditors.LookUpEdit();
            this.lab4 = new DevExpress.XtraEditors.LabelControl();
            this.txtHemoID = new DevExpress.XtraEditors.TextEdit();
            this.lab3 = new DevExpress.XtraEditors.LabelControl();
            this.deEndCreateTime = new DevExpress.XtraEditors.DateEdit();
            this.lab2 = new DevExpress.XtraEditors.LabelControl();
            this.deBeginCreateTime = new DevExpress.XtraEditors.DateEdit();
            this.lab1 = new DevExpress.XtraEditors.LabelControl();
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.gcParams = new DevExpress.XtraGrid.GridControl();
            this.gvParams = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHemoID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndCreateTime.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndCreateTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBeginCreateTime.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBeginCreateTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcParams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvParams)).BeginInit();
            this.SuspendLayout();
            // 
            // chartControl1
            // 
            this.chartControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            xyDiagram1.AxisX.DateTimeOptions.Format = DevExpress.XtraCharts.DateTimeFormat.Custom;
            xyDiagram1.AxisX.DateTimeOptions.FormatString = "yyyy:MM:dd hh:mm:ss";
            xyDiagram1.AxisX.Label.Staggered = true;
            xyDiagram1.AxisX.Range.ScrollingRange.SideMarginsEnabled = true;
            xyDiagram1.AxisX.Range.SideMarginsEnabled = true;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.Range.ScrollingRange.SideMarginsEnabled = true;
            xyDiagram1.AxisY.Range.SideMarginsEnabled = true;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.chartControl1.Diagram = xyDiagram1;
            this.chartControl1.Location = new System.Drawing.Point(12, 56);
            this.chartControl1.Name = "chartControl1";
            pointSeriesLabel1.LineVisible = true;
            series1.Label = pointSeriesLabel1;
            series1.Name = "Series 1";
            series1.View = lineSeriesView1;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            pointSeriesLabel2.LineVisible = true;
            this.chartControl1.SeriesTemplate.Label = pointSeriesLabel2;
            this.chartControl1.SeriesTemplate.View = lineSeriesView2;
            this.chartControl1.Size = new System.Drawing.Size(860, 325);
            this.chartControl1.TabIndex = 0;
            chartTitle1.Text = "血液透析统计查询";
            this.chartControl1.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Controls.Add(this.lueType);
            this.panelControl1.Controls.Add(this.lab4);
            this.panelControl1.Controls.Add(this.txtHemoID);
            this.panelControl1.Controls.Add(this.lab3);
            this.panelControl1.Controls.Add(this.deEndCreateTime);
            this.panelControl1.Controls.Add(this.lab2);
            this.panelControl1.Controls.Add(this.deBeginCreateTime);
            this.panelControl1.Controls.Add(this.lab1);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(860, 38);
            this.panelControl1.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImageIndex = 3;
            this.btnClose.Location = new System.Drawing.Point(750, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 25);
            this.btnClose.TabIndex = 296;
            this.btnClose.Text = "关闭(&C) ";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSearch.ImageIndex = 8;
            this.btnSearch.Location = new System.Drawing.Point(669, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 25);
            this.btnSearch.TabIndex = 295;
            this.btnSearch.Text = "查询(&Q) ";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lueType
            // 
            this.lueType.Location = new System.Drawing.Point(495, 7);
            this.lueType.Name = "lueType";
            this.lueType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueType.Size = new System.Drawing.Size(168, 21);
            this.lueType.TabIndex = 7;
            // 
            // lab4
            // 
            this.lab4.Location = new System.Drawing.Point(441, 10);
            this.lab4.Name = "lab4";
            this.lab4.Size = new System.Drawing.Size(48, 14);
            this.lab4.TabIndex = 6;
            this.lab4.Text = "治疗参数";
            // 
            // txtHemoID
            // 
            this.txtHemoID.Location = new System.Drawing.Point(335, 7);
            this.txtHemoID.Name = "txtHemoID";
            this.txtHemoID.Size = new System.Drawing.Size(100, 21);
            this.txtHemoID.TabIndex = 5;
            // 
            // lab3
            // 
            this.lab3.Location = new System.Drawing.Point(281, 10);
            this.lab3.Name = "lab3";
            this.lab3.Size = new System.Drawing.Size(36, 14);
            this.lab3.TabIndex = 4;
            this.lab3.Text = "透析号";
            // 
            // deEndCreateTime
            // 
            this.deEndCreateTime.EditValue = null;
            this.deEndCreateTime.Location = new System.Drawing.Point(175, 7);
            this.deEndCreateTime.Name = "deEndCreateTime";
            this.deEndCreateTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEndCreateTime.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deEndCreateTime.Size = new System.Drawing.Size(100, 21);
            this.deEndCreateTime.TabIndex = 3;
            // 
            // lab2
            // 
            this.lab2.Location = new System.Drawing.Point(165, 10);
            this.lab2.Name = "lab2";
            this.lab2.Size = new System.Drawing.Size(4, 14);
            this.lab2.TabIndex = 2;
            this.lab2.Text = "-";
            // 
            // deBeginCreateTime
            // 
            this.deBeginCreateTime.EditValue = null;
            this.deBeginCreateTime.Location = new System.Drawing.Point(59, 7);
            this.deBeginCreateTime.Name = "deBeginCreateTime";
            this.deBeginCreateTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deBeginCreateTime.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deBeginCreateTime.Size = new System.Drawing.Size(100, 21);
            this.deBeginCreateTime.TabIndex = 1;
            // 
            // lab1
            // 
            this.lab1.Location = new System.Drawing.Point(5, 10);
            this.lab1.Name = "lab1";
            this.lab1.Size = new System.Drawing.Size(48, 14);
            this.lab1.TabIndex = 0;
            this.lab1.Text = "治疗日期";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // gcParams
            // 
            this.gcParams.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gcParams.Location = new System.Drawing.Point(12, 387);
            this.gcParams.MainView = this.gvParams;
            this.gcParams.Name = "gcParams";
            this.gcParams.Size = new System.Drawing.Size(860, 163);
            this.gcParams.TabIndex = 302;
            this.gcParams.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvParams});
            // 
            // gvParams
            // 
            this.gvParams.GridControl = this.gcParams;
            this.gvParams.Name = "gvParams";
            this.gvParams.OptionsBehavior.Editable = false;
            this.gvParams.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gvParams.OptionsView.ShowGroupPanel = false;
            this.gvParams.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvParams_CustomDrawCell);
            // 
            // HemoParametersChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(884, 562);
            this.Controls.Add(this.gcParams);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.chartControl1);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "HemoParametersChart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "血液透析统计查询";
            this.Load += new System.EventHandler(this.HemoParametersChart_Load);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHemoID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndCreateTime.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndCreateTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBeginCreateTime.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBeginCreateTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcParams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvParams)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lab1;
        private DevExpress.XtraEditors.DateEdit deBeginCreateTime;
        private DevExpress.XtraEditors.LabelControl lab2;
        private DevExpress.XtraEditors.DateEdit deEndCreateTime;
        private DevExpress.XtraEditors.LabelControl lab3;
        private DevExpress.XtraEditors.TextEdit txtHemoID;
        private DevExpress.XtraEditors.LabelControl lab4;
        private DevExpress.XtraEditors.LookUpEdit lueType;
        private Hemo.Client.Controls.DXSimpleButton btnSearch;
        private Hemo.Client.Controls.DXSimpleButton btnClose;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider;
        private DevExpress.XtraGrid.GridControl gcParams;
        public DevExpress.XtraGrid.Views.Grid.GridView gvParams;
    }
}