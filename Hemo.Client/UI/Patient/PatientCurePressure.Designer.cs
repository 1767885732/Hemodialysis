namespace Hemo.Client.UI.Patient
{
    partial class PatientCurePressure
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
            this.ctlSignChart2 = new Hemo.Client.Controls.SignChart.CtlSignChart();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnQuery = new Hemo.Client.Controls.DXSimpleButton();
            this.QueryDate = new DevExpress.XtraEditors.DateEdit();
            this.lbTitle = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QueryDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QueryDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlSignChart2
            // 
            this.ctlSignChart2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlSignChart2.HEMODIALYSIS_ID = "";
            this.ctlSignChart2.Location = new System.Drawing.Point(0, 0);
            this.ctlSignChart2.Name = "ctlSignChart2";
            this.ctlSignChart2.Size = new System.Drawing.Size(703, 334);
            this.ctlSignChart2.TabIndex = 1;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lbTitle);
            this.panelControl1.Controls.Add(this.btnQuery);
            this.panelControl1.Controls.Add(this.QueryDate);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 334);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(703, 37);
            this.panelControl1.TabIndex = 2;
            // 
            // btnQuery
            // 
            this.btnQuery.ImageIndex = 8;
            this.btnQuery.Location = new System.Drawing.Point(612, 5);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 25);
            this.btnQuery.TabIndex = 304;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // QueryDate
            // 
            this.QueryDate.EditValue = null;
            this.QueryDate.Location = new System.Drawing.Point(491, 7);
            this.QueryDate.Name = "QueryDate";
            this.QueryDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.QueryDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.QueryDate.Size = new System.Drawing.Size(100, 21);
            this.QueryDate.TabIndex = 1;
            this.QueryDate.EditValueChanged += new System.EventHandler(this.QueryDate_EditValueChanged);
            // 
            // lbTitle
            // 
            this.lbTitle.Location = new System.Drawing.Point(5, 10);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(70, 14);
            this.lbTitle.TabIndex = 305;
            this.lbTitle.Text = "labelControl1";
            // 
            // PatientCurePressure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctlSignChart2);
            this.Controls.Add(this.panelControl1);
            this.Name = "PatientCurePressure";
            this.Size = new System.Drawing.Size(703, 371);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QueryDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QueryDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.SignChart.CtlSignChart ctlSignChart2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.DateEdit QueryDate;
        private Controls.DXSimpleButton btnQuery;
        private DevExpress.XtraEditors.LabelControl lbTitle;
    }
}
