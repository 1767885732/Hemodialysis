namespace Hemo.Client.UI.ReportChart
{
    partial class QueryQualityMonitorInfectReport
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
            this.gcInfect = new DevExpress.XtraGrid.GridControl();
            this.gvInfect = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.busyIndicator = new Hemo.Client.Controls.BusyIndicator();
            ((System.ComponentModel.ISupportInitialize)(this.gcInfect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInfect)).BeginInit();
            this.SuspendLayout();
            // 
            // gcInfect
            // 
            this.gcInfect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcInfect.Location = new System.Drawing.Point(0, 0);
            this.gcInfect.MainView = this.gvInfect;
            this.gcInfect.Name = "gcInfect";
            this.gcInfect.Size = new System.Drawing.Size(860, 430);
            this.gcInfect.TabIndex = 0;
            this.gcInfect.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvInfect});
            // 
            // gvInfect
            // 
            this.gvInfect.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gvInfect.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gvInfect.GridControl = this.gcInfect;
            this.gvInfect.Name = "gvInfect";
            this.gvInfect.OptionsBehavior.Editable = false;
            this.gvInfect.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "名称";
            this.gridColumn1.FieldName = "INFECT_NAME";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "条件";
            this.gridColumn2.FieldName = "INFECT_CONDITION";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "人数";
            this.gridColumn3.FieldName = "INFECT_COUNT";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "比例";
            this.gridColumn4.FieldName = "INFECT_RATIO";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // busyIndicator
            // 
            this.busyIndicator.Location = new System.Drawing.Point(372, 192);
            this.busyIndicator.Name = "busyIndicator";
            this.busyIndicator.Size = new System.Drawing.Size(100, 40);
            this.busyIndicator.TabIndex = 88;
            this.busyIndicator.Visible = false;
            // 
            // QueryQualityMonitorInfectReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.busyIndicator);
            this.Controls.Add(this.gcInfect);
            this.Name = "QueryQualityMonitorInfectReport";
            this.Size = new System.Drawing.Size(860, 430);
            ((System.ComponentModel.ISupportInitialize)(this.gcInfect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInfect)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcInfect;
        private DevExpress.XtraGrid.Views.Grid.GridView gvInfect;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private Controls.BusyIndicator busyIndicator;
    }
}
