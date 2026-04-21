namespace Hemo.Client.UI.PatientFixUI
{
    partial class ShowCustomerLabChart
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
            this.components = new System.ComponentModel.Container();
            this.components = new System.ComponentModel.Container();
            this.ctlSignChart1 = new Hemo.Client.Controls.SignChart.CtlSignChart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnInstruction1 = new Hemo.Client.Controls.DXSimpleButton();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnPrint = new Hemo.Client.Controls.DXSimpleButton();
            this.beginTime = new DevExpress.XtraEditors.DateEdit();
            this.btnExpExcel = new Hemo.Client.Controls.DXSimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnQuery = new Hemo.Client.Controls.DXSimpleButton();
            this.endTime = new DevExpress.XtraEditors.DateEdit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlSignChart1
            // 
            this.ctlSignChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlSignChart1.HEMODIALYSIS_ID = "";
            this.ctlSignChart1.Location = new System.Drawing.Point(0, 0);
            this.ctlSignChart1.Name = "ctlSignChart1";
            this.ctlSignChart1.Size = new System.Drawing.Size(801, 297);
            this.ctlSignChart1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ctlSignChart1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(801, 297);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panelControl2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 303);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(801, 43);
            this.panel2.TabIndex = 2;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnInstruction1);
            this.panelControl2.Controls.Add(this.labelControl5);
            this.panelControl2.Controls.Add(this.btnPrint);
            this.panelControl2.Controls.Add(this.beginTime);
            this.panelControl2.Controls.Add(this.btnExpExcel);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Controls.Add(this.btnQuery);
            this.panelControl2.Controls.Add(this.endTime);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(801, 43);
            this.panelControl2.TabIndex = 37;
            // 
            // btnInstruction1
            // 
            this.btnInstruction1.ImageIndex = 2;
            this.btnInstruction1.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnInstruction1.Location = new System.Drawing.Point(831, 11);
            this.btnInstruction1.Name = "btnInstruction1";
            this.btnInstruction1.Size = new System.Drawing.Size(85, 23);
            this.btnInstruction1.TabIndex = 821;
            this.btnInstruction1.Text = "报表说明";
            this.btnInstruction1.Visible = false;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 15);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.TabIndex = 31;
            this.labelControl5.Text = "开始日期";
            // 
            // btnPrint
            // 
            this.btnPrint.ImageIndex = 10;
            this.btnPrint.Location = new System.Drawing.Point(696, 11);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 35;
            this.btnPrint.Text = "关闭";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // beginTime
            // 
            this.beginTime.EditValue = null;
            this.beginTime.Location = new System.Drawing.Point(66, 12);
            this.beginTime.Name = "beginTime";
            this.beginTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.beginTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beginTime.Size = new System.Drawing.Size(108, 20);
            this.beginTime.TabIndex = 30;
            // 
            // btnExpExcel
            // 
            this.btnExpExcel.ImageIndex = 4;
            this.btnExpExcel.Location = new System.Drawing.Point(610, 11);
            this.btnExpExcel.Name = "btnExpExcel";
            this.btnExpExcel.Size = new System.Drawing.Size(75, 23);
            this.btnExpExcel.TabIndex = 34;
            this.btnExpExcel.Text = "导出";
            this.btnExpExcel.Click += new System.EventHandler(this.btnExpExcel_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(189, 15);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 32;
            this.labelControl4.Text = "结束日期";
            // 
            // btnQuery
            // 
            this.btnQuery.ImageIndex = 8;
            this.btnQuery.Location = new System.Drawing.Point(526, 11);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 33;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // endTime
            // 
            this.endTime.EditValue = null;
            this.endTime.Location = new System.Drawing.Point(243, 12);
            this.endTime.Name = "endTime";
            this.endTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.endTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.endTime.Size = new System.Drawing.Size(117, 20);
            this.endTime.TabIndex = 29;
            // 
            // ShowCustomerLabChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 346);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ShowCustomerLabChart";
            this.Text = "患者检查检验图表";
            this.Load += new System.EventHandler(this.ShowCustomerLabChart_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.SignChart.CtlSignChart ctlSignChart1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private Controls.DXSimpleButton btnInstruction1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private Controls.DXSimpleButton btnPrint;
        private DevExpress.XtraEditors.DateEdit beginTime;
        private Controls.DXSimpleButton btnExpExcel;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private Controls.DXSimpleButton btnQuery;
        private DevExpress.XtraEditors.DateEdit endTime;
    }
}