using Hemo.Client.Controls.Common;
namespace Hemo.Client.UI.Config
{
    partial class ImportPatient
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tbLogFile = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ofdLog = new System.Windows.Forms.OpenFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dxSimpleButton1 = new Hemo.Client.Controls.Common.DXSimpleButton();
            this.dxSimpleButton2 = new Hemo.Client.Controls.Common.DXSimpleButton();
            this.dxSimpleButton3 = new Hemo.Client.Controls.Common.DXSimpleButton();
            this.btnClose = new Hemo.Client.Controls.Common.DXSimpleButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbFileName
            // 
            this.tbFileName.Location = new System.Drawing.Point(84, 9);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.ReadOnly = true;
            this.tbFileName.Size = new System.Drawing.Size(395, 21);
            this.tbFileName.TabIndex = 2;
            // 
            // ofd
            // 
            this.ofd.FileName = "openFileDialog1";
            // 
            // tbMessage
            // 
            this.tbMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbMessage.Location = new System.Drawing.Point(0, 61);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.ReadOnly = true;
            this.tbMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMessage.Size = new System.Drawing.Size(725, 168);
            this.tbMessage.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "患者信息导入";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(278, 123);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 6;
            this.progressBar1.Visible = false;
            // 
            // tbLogFile
            // 
            this.tbLogFile.BackColor = System.Drawing.Color.LightGray;
            this.tbLogFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLogFile.Location = new System.Drawing.Point(0, 260);
            this.tbLogFile.Multiline = true;
            this.tbLogFile.Name = "tbLogFile";
            this.tbLogFile.ReadOnly = true;
            this.tbLogFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLogFile.Size = new System.Drawing.Size(725, 188);
            this.tbLogFile.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(725, 23);
            this.panel1.TabIndex = 10;
            // 
            // ofdLog
            // 
            this.ofdLog.FileName = "openFileDialog1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dxSimpleButton1);
            this.panel2.Controls.Add(this.tbFileName);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.dxSimpleButton2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 23);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(725, 38);
            this.panel2.TabIndex = 14;
            // 
            // dxSimpleButton1
            // 
            this.dxSimpleButton1.ImageIndex = 0;
            this.dxSimpleButton1.Location = new System.Drawing.Point(3, 7);
            this.dxSimpleButton1.Name = "dxSimpleButton1";
            this.dxSimpleButton1.Size = new System.Drawing.Size(75, 23);
            this.dxSimpleButton1.TabIndex = 11;
            this.dxSimpleButton1.Text = "选择文件";
            this.dxSimpleButton1.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // dxSimpleButton2
            // 
            this.dxSimpleButton2.ImageIndex = 0;
            this.dxSimpleButton2.Location = new System.Drawing.Point(485, 7);
            this.dxSimpleButton2.Name = "dxSimpleButton2";
            this.dxSimpleButton2.Size = new System.Drawing.Size(55, 23);
            this.dxSimpleButton2.TabIndex = 12;
            this.dxSimpleButton2.Text = "导入";
            this.dxSimpleButton2.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dxSimpleButton3
            // 
            this.dxSimpleButton3.Dock = System.Windows.Forms.DockStyle.Top;
            this.dxSimpleButton3.ImageIndex = 0;
            this.dxSimpleButton3.Location = new System.Drawing.Point(0, 229);
            this.dxSimpleButton3.Name = "dxSimpleButton3";
            this.dxSimpleButton3.Size = new System.Drawing.Size(725, 31);
            this.dxSimpleButton3.TabIndex = 13;
            this.dxSimpleButton3.Text = "打开日志";
            this.dxSimpleButton3.Click += new System.EventHandler(this.btnOpenLog_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.ImageIndex = 0;
            this.btnClose.Location = new System.Drawing.Point(546, 7);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(55, 23);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "关闭";
            // 
            // ImportPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbLogFile);
            this.Controls.Add(this.dxSimpleButton3);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.progressBar1);
            this.Name = "ImportPatient";
            this.Size = new System.Drawing.Size(725, 448);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox tbLogFile;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.OpenFileDialog ofdLog;
        private DXSimpleButton dxSimpleButton1;
        private DXSimpleButton dxSimpleButton2;
        private DXSimpleButton dxSimpleButton3;
        private System.Windows.Forms.Panel panel2;
        private DXSimpleButton btnClose;
    }
}
