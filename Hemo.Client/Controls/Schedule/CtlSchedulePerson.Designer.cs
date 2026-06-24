namespace Hemo.Client.Controls.Schedule
{
    partial class CtlSchedulePerson
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
            this.panelControl14 = new DevExpress.XtraEditors.PanelControl();
            this.labComments = new DevExpress.XtraEditors.LabelControl();
            this.labPatientName = new DevExpress.XtraEditors.LabelControl();
            this.labTreatInfo = new DevExpress.XtraEditors.LabelControl();
            this.labMachineName = new DevExpress.XtraEditors.LabelControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSetTreatInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditComments = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeletePatient = new System.Windows.Forms.ToolStripMenuItem();
            this.检验记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl14)).BeginInit();
            this.panelControl14.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl14
            // 
            this.panelControl14.Controls.Add(this.labComments);
            this.panelControl14.Controls.Add(this.labPatientName);
            this.panelControl14.Controls.Add(this.labTreatInfo);
            this.panelControl14.Controls.Add(this.labMachineName);
            this.panelControl14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl14.Location = new System.Drawing.Point(0, 0);
            this.panelControl14.Name = "panelControl14";
            this.panelControl14.Size = new System.Drawing.Size(105, 66);
            this.panelControl14.TabIndex = 6;
            // 
            // labComments
            // 
            this.labComments.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labComments.Appearance.Options.UseFont = true;
            this.labComments.Location = new System.Drawing.Point(3, 41);
            this.labComments.Name = "labComments";
            this.labComments.Size = new System.Drawing.Size(0, 14);
            this.labComments.TabIndex = 4;
            // 
            // labPatientName
            // 
            this.labPatientName.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labPatientName.Appearance.Options.UseFont = true;
            this.labPatientName.Location = new System.Drawing.Point(36, 7);
            this.labPatientName.Name = "labPatientName";
            this.labPatientName.Size = new System.Drawing.Size(0, 14);
            this.labPatientName.TabIndex = 3;
            this.labPatientName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labPatientName_MouseDown);
            // 
            // labTreatInfo
            // 
            this.labTreatInfo.Location = new System.Drawing.Point(3, 24);
            this.labTreatInfo.Name = "labTreatInfo";
            this.labTreatInfo.Size = new System.Drawing.Size(0, 14);
            this.labTreatInfo.TabIndex = 1;
            // 
            // labMachineName
            // 
            this.labMachineName.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.labMachineName.Appearance.ForeColor = System.Drawing.Color.Green;
            this.labMachineName.Appearance.Options.UseFont = true;
            this.labMachineName.Appearance.Options.UseForeColor = true;
            this.labMachineName.Appearance.Options.UseTextOptions = true;
            this.labMachineName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labMachineName.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labMachineName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labMachineName.Location = new System.Drawing.Point(0, 0);
            this.labMachineName.Name = "labMachineName";
            this.labMachineName.Size = new System.Drawing.Size(28, 23);
            this.labMachineName.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSetTreatInfo,
            this.tsmiEditComments,
            this.检验记录ToolStripMenuItem,
            this.tsmiDeletePatient});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(197, 114);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // tsmiSetTreatInfo
            // 
            this.tsmiSetTreatInfo.Name = "tsmiSetTreatInfo";
            this.tsmiSetTreatInfo.Size = new System.Drawing.Size(196, 22);
            this.tsmiSetTreatInfo.Text = "设置治疗方式和透析器";
            this.tsmiSetTreatInfo.Visible = false;
            this.tsmiSetTreatInfo.Click += new System.EventHandler(this.tsmiSetTreatInfo_Click);
            // 
            // tsmiEditComments
            // 
            this.tsmiEditComments.Name = "tsmiEditComments";
            this.tsmiEditComments.Size = new System.Drawing.Size(196, 22);
            this.tsmiEditComments.Text = "维护备注信息";
            this.tsmiEditComments.Click += new System.EventHandler(this.tsmiEditComments_Click);
            // 
            // tsmiDeletePatient
            // 
            this.tsmiDeletePatient.Name = "tsmiDeletePatient";
            this.tsmiDeletePatient.Size = new System.Drawing.Size(196, 22);
            this.tsmiDeletePatient.Text = "删除患者";
            this.tsmiDeletePatient.Click += new System.EventHandler(this.tsmiDeletePatient_Click);
            // 
            // 检验记录ToolStripMenuItem
            // 
            this.检验记录ToolStripMenuItem.Name = "检验记录ToolStripMenuItem";
            this.检验记录ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.检验记录ToolStripMenuItem.Text = "检验记录";
            this.检验记录ToolStripMenuItem.Click += new System.EventHandler(this.检验记录ToolStripMenuItem_Click);
            // 
            // CtlSchedulePerson
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.panelControl14);
            this.Name = "CtlSchedulePerson";
            this.Size = new System.Drawing.Size(105, 66);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.CtlSchedulePerson_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.CtlSchedulePerson_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl14)).EndInit();
            this.panelControl14.ResumeLayout(false);
            this.panelControl14.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl14;
        private DevExpress.XtraEditors.LabelControl labTreatInfo;
        private DevExpress.XtraEditors.LabelControl labMachineName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiSetTreatInfo;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditComments;
        private DevExpress.XtraEditors.LabelControl labPatientName;
        private DevExpress.XtraEditors.LabelControl labComments;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeletePatient;
        private System.Windows.Forms.ToolStripMenuItem 检验记录ToolStripMenuItem;
    }
}
