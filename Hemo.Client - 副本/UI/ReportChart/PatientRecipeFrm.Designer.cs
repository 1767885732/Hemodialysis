namespace Hemo.Client.UI.ReportChart
{
    partial class PatientRecipeFrm
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
            this.ctlTendencyChart1 = new Hemo.Client.Controls.SignChart.CtlTendencyChart();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnPrint = new Hemo.Client.Controls.DXSimpleButton();
            this.beginTime = new DevExpress.XtraEditors.DateEdit();
            this.btnExpExcel = new Hemo.Client.Controls.DXSimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnQuery = new Hemo.Client.Controls.DXSimpleButton();
            this.endTime = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlTendencyChart1
            // 
            this.ctlTendencyChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlTendencyChart1.Location = new System.Drawing.Point(0, 44);
            this.ctlTendencyChart1.Name = "ctlTendencyChart1";
            this.ctlTendencyChart1.Size = new System.Drawing.Size(948, 468);
            this.ctlTendencyChart1.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.btnPrint);
            this.panelControl1.Controls.Add(this.beginTime);
            this.panelControl1.Controls.Add(this.btnExpExcel);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.btnQuery);
            this.panelControl1.Controls.Add(this.endTime);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(948, 44);
            this.panelControl1.TabIndex = 1;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(22, 13);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.TabIndex = 38;
            this.labelControl5.Text = "开始日期";
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.ImageIndex = 6;
            this.btnPrint.Location = new System.Drawing.Point(868, 9);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 29);
            this.btnPrint.TabIndex = 42;
            this.btnPrint.Text = "打印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // beginTime
            // 
            this.beginTime.EditValue = null;
            this.beginTime.Location = new System.Drawing.Point(86, 9);
            this.beginTime.Name = "beginTime";
            this.beginTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.beginTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beginTime.Size = new System.Drawing.Size(108, 20);
            this.beginTime.TabIndex = 37;
            // 
            // btnExpExcel
            // 
            this.btnExpExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExpExcel.ImageIndex = 9;
            this.btnExpExcel.Location = new System.Drawing.Point(771, 9);
            this.btnExpExcel.Name = "btnExpExcel";
            this.btnExpExcel.Size = new System.Drawing.Size(93, 29);
            this.btnExpExcel.TabIndex = 41;
            this.btnExpExcel.Text = "导出Excel";
            this.btnExpExcel.Click += new System.EventHandler(this.btnExpExcel_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(210, 12);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 39;
            this.labelControl4.Text = "结束日期";
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.ImageIndex = 8;
            this.btnQuery.Location = new System.Drawing.Point(689, 9);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 29);
            this.btnQuery.TabIndex = 40;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // endTime
            // 
            this.endTime.EditValue = null;
            this.endTime.Location = new System.Drawing.Point(276, 9);
            this.endTime.Name = "endTime";
            this.endTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.endTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.endTime.Size = new System.Drawing.Size(117, 20);
            this.endTime.TabIndex = 36;
            // 
            // PatientRecipeFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctlTendencyChart1);
            this.Controls.Add(this.panelControl1);
            this.Name = "PatientRecipeFrm";
            this.Size = new System.Drawing.Size(948, 512);
            this.Load += new System.EventHandler(this.PatientRecipeFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.SignChart.CtlTendencyChart ctlTendencyChart1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private Controls.DXSimpleButton btnPrint;
        private DevExpress.XtraEditors.DateEdit beginTime;
        private Controls.DXSimpleButton btnExpExcel;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private Controls.DXSimpleButton btnQuery;
        private DevExpress.XtraEditors.DateEdit endTime;
    }
}