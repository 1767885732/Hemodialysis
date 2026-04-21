namespace Hemo.Client.UI.ReportChart
{
    partial class QueryQualityMonitorCureReport
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
            this.gcCure = new DevExpress.XtraGrid.GridControl();
            this.gvCure = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.busyIndicator = new Hemo.Client.Controls.BusyIndicator();
            ((System.ComponentModel.ISupportInitialize)(this.gcCure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCure)).BeginInit();
            this.SuspendLayout();
            // 
            // gcCure
            // 
            this.gcCure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcCure.Location = new System.Drawing.Point(0, 0);
            this.gcCure.MainView = this.gvCure;
            this.gcCure.Name = "gcCure";
            this.gcCure.Size = new System.Drawing.Size(860, 430);
            this.gcCure.TabIndex = 1;
            this.gcCure.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCure});
            // 
            // gvCure
            // 
            this.gvCure.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gvCure.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gvCure.GridControl = this.gcCure;
            this.gvCure.Name = "gvCure";
            this.gvCure.OptionsBehavior.Editable = false;
            this.gvCure.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "名称";
            this.gridColumn1.FieldName = "CURE_NAME";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "条件";
            this.gridColumn2.FieldName = "CURE_CONDITION";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "人数（人次）";
            this.gridColumn3.FieldName = "CURE_COUNT";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "比例";
            this.gridColumn4.FieldName = "CURE_RATIO";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // busyIndicator
            // 
            this.busyIndicator.Location = new System.Drawing.Point(372, 192);
            this.busyIndicator.Name = "busyIndicator";
            this.busyIndicator.Size = new System.Drawing.Size(100, 40);
            this.busyIndicator.TabIndex = 89;
            this.busyIndicator.Visible = false;
            // 
            // QueryQualityMonitorCureReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.busyIndicator);
            this.Controls.Add(this.gcCure);
            this.Name = "QueryQualityMonitorCureReport";
            this.Size = new System.Drawing.Size(860, 430);
            ((System.ComponentModel.ISupportInitialize)(this.gcCure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCure)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcCure;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCure;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private Controls.BusyIndicator busyIndicator;
    }
}
