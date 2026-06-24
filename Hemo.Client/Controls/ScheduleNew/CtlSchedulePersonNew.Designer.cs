namespace Hemo.Client.Controls.Schedule
{
    partial class CtlSchedulePersonNew
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSetTreatInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditComments = new System.Windows.Forms.ToolStripMenuItem();
            this.检验记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeletePatient = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSetTreatInfo,
            this.tsmiEditComments,
            this.检验记录ToolStripMenuItem,
            this.tsmiDeletePatient});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(197, 92);
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
            // 检验记录ToolStripMenuItem
            // 
            this.检验记录ToolStripMenuItem.Name = "检验记录ToolStripMenuItem";
            this.检验记录ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.检验记录ToolStripMenuItem.Text = "检验记录";
            this.检验记录ToolStripMenuItem.Click += new System.EventHandler(this.检验记录ToolStripMenuItem_Click);
            // 
            // tsmiDeletePatient
            // 
            this.tsmiDeletePatient.Name = "tsmiDeletePatient";
            this.tsmiDeletePatient.Size = new System.Drawing.Size(196, 22);
            this.tsmiDeletePatient.Text = "删除患者";
            this.tsmiDeletePatient.Click += new System.EventHandler(this.tsmiDeletePatient_Click);
            // 
            // CtlSchedulePersonNew
            // 
            this.AllowDrop = true;
            this.Size = new System.Drawing.Size(105, 47);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.CtlSchedulePersonNew_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.CtlSchedulePersonNew_DragEnter);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CtlSchedulePersonNew_MouseDown);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem tsmiSetTreatInfo;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditComments;
        private System.Windows.Forms.ToolStripMenuItem 检验记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeletePatient;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

    }
}
