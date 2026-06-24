namespace Hemo.Client.Controls.ScheduleNew
{
    partial class ScheduleRemarkFrm
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
            //this.components = new System.ComponentModel.Container();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_Close = new Hemo.Client.Controls.DXSimpleButton();
            this.btnSure = new Hemo.Client.Controls.DXSimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.startDate = new DevExpress.XtraEditors.DateEdit();
            this.MED_PATIENTREMARKFrm = new System.Windows.Forms.BindingSource(this.components);
            this.endDate = new DevExpress.XtraEditors.DateEdit();
            this.txtMark = new DevExpress.XtraEditors.MemoEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.lb_title = new System.Windows.Forms.Label();
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.txtID = new System.Windows.Forms.TextBox();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.screenMsgManager1 = new Hemo.Client.UI.Config.ScreenMsgManager();
            this.lab5 = new DevExpress.XtraEditors.LabelControl();
            this.txtFileDictionary = new DevExpress.XtraEditors.MemoEdit();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.btnCheckItem = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.startDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MED_PATIENTREMARKFrm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.xtraTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileDictionary.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btn_Close);
            this.panelControl1.Controls.Add(this.btnSure);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 314);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(713, 37);
            this.panelControl1.TabIndex = 7;
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.ImageIndex = 3;
            this.btn_Close.Location = new System.Drawing.Point(630, 6);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 25);
            this.btn_Close.TabIndex = 362;
            this.btn_Close.Text = "关闭(&C)";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btnSure
            // 
            this.btnSure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSure.ImageIndex = 7;
            this.btnSure.Location = new System.Drawing.Point(534, 6);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(85, 25);
            this.btnSure.TabIndex = 359;
            this.btnSure.Text = "保存(&S)";
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(194, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "开始时间:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "备注：";
            // 
            // startDate
            // 
            this.startDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.MED_PATIENTREMARKFrm, "BEGINTIME", true));
            this.startDate.EditValue = null;
            this.startDate.Enabled = false;
            this.startDate.Location = new System.Drawing.Point(259, 65);
            this.startDate.Name = "startDate";
            this.startDate.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.startDate.Properties.Appearance.Options.UseForeColor = true;
            this.startDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.startDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.startDate.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.startDate.Size = new System.Drawing.Size(180, 20);
            this.startDate.TabIndex = 292;
            // 
            // MED_PATIENTREMARKFrm
            // 
            this.MED_PATIENTREMARKFrm.AllowNew = true;
            this.MED_PATIENTREMARKFrm.DataSource = typeof(Hemo.Model.PermissionModel.MED_SCHEDULEREMARKRow);
            // 
            // endDate
            // 
            this.endDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.MED_PATIENTREMARKFrm, "ENDTIME", true));
            this.endDate.EditValue = null;
            this.endDate.Enabled = false;
            this.endDate.Location = new System.Drawing.Point(259, 116);
            this.endDate.Name = "endDate";
            this.endDate.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.endDate.Properties.Appearance.Options.UseForeColor = true;
            this.endDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.endDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.endDate.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.endDate.Size = new System.Drawing.Size(180, 20);
            this.endDate.TabIndex = 293;
            // 
            // txtMark
            // 
            this.txtMark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MED_PATIENTREMARKFrm, "CONTENT", true));
            this.txtMark.Location = new System.Drawing.Point(34, 168);
            this.txtMark.Name = "txtMark";
            this.txtMark.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtMark.Properties.Appearance.Options.UseForeColor = true;
            this.txtMark.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtMark.Size = new System.Drawing.Size(653, 140);
            this.txtMark.TabIndex = 291;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(194, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "结束时间:";
            // 
            // lb_title
            // 
            this.lb_title.AutoSize = true;
            this.lb_title.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_title.Location = new System.Drawing.Point(192, 27);
            this.lb_title.Name = "lb_title";
            this.lb_title.Size = new System.Drawing.Size(286, 23);
            this.lb_title.TabIndex = 0;
            this.lb_title.Text = "开始时间~结束时间的备注信息";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // txtID
            // 
            this.txtID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MED_PATIENTREMARKFrm, "ID", true));
            this.txtID.Location = new System.Drawing.Point(403, 315);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(1, 22);
            this.txtID.TabIndex = 294;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(719, 380);
            this.xtraTabControl1.TabIndex = 295;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.lb_title);
            this.xtraTabPage1.Controls.Add(this.label1);
            this.xtraTabPage1.Controls.Add(this.panelControl1);
            this.xtraTabPage1.Controls.Add(this.endDate);
            this.xtraTabPage1.Controls.Add(this.label2);
            this.xtraTabPage1.Controls.Add(this.txtMark);
            this.xtraTabPage1.Controls.Add(this.label3);
            this.xtraTabPage1.Controls.Add(this.startDate);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(713, 351);
            this.xtraTabPage1.Text = "静态大屏公告";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.screenMsgManager1);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(713, 351);
            this.xtraTabPage2.Text = "大屏语音播报";
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.btnCheckItem);
            this.xtraTabPage3.Controls.Add(this.btnSave);
            this.xtraTabPage3.Controls.Add(this.lab5);
            this.xtraTabPage3.Controls.Add(this.txtFileDictionary);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(713, 351);
            this.xtraTabPage3.Text = "大屏播放器内容设置";
            // 
            // screenMsgManager1
            // 
            this.screenMsgManager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.screenMsgManager1.HasDirty = false;
            this.screenMsgManager1.Location = new System.Drawing.Point(0, 0);
            this.screenMsgManager1.Name = "screenMsgManager1";
            this.screenMsgManager1.Size = new System.Drawing.Size(713, 351);
            this.screenMsgManager1.TabIndex = 0;
            // 
            // lab5
            // 
            this.lab5.Location = new System.Drawing.Point(83, 26);
            this.lab5.Name = "lab5";
            this.lab5.Size = new System.Drawing.Size(72, 14);
            this.lab5.TabIndex = 320;
            this.lab5.Text = "上传文件列表";
            // 
            // txtFileDictionary
            // 
            this.txtFileDictionary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileDictionary.Location = new System.Drawing.Point(83, 59);
            this.txtFileDictionary.Name = "txtFileDictionary";
            this.txtFileDictionary.Properties.MaxLength = 5;
            this.txtFileDictionary.Size = new System.Drawing.Size(465, 208);
            this.txtFileDictionary.TabIndex = 321;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(473, 288);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 323;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCheckItem
            // 
            this.btnCheckItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheckItem.ImageIndex = 9;
            this.btnCheckItem.Location = new System.Drawing.Point(369, 288);
            this.btnCheckItem.Name = "btnCheckItem";
            this.btnCheckItem.Size = new System.Drawing.Size(75, 25);
            this.btnCheckItem.TabIndex = 323;
            this.btnCheckItem.Text = "选择";
            this.btnCheckItem.Click += new System.EventHandler(this.btnCheckItem_Click);
            // 
            // ScheduleRemarkFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 380);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.txtID);
            this.Name = "ScheduleRemarkFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "当前周的备注";
            this.Load += new System.EventHandler(this.ScheduleRemarkFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.startDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MED_PATIENTREMARKFrm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage1.PerformLayout();
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage3.ResumeLayout(false);
            this.xtraTabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileDictionary.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Hemo.Client.Controls.DXSimpleButton btnSure;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit startDate;
        private DevExpress.XtraEditors.DateEdit endDate;
        private DevExpress.XtraEditors.MemoEdit txtMark;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lb_title;
        private System.Windows.Forms.BindingSource MED_PATIENTREMARKFrm;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider;
        private System.Windows.Forms.TextBox txtID;
        private DXSimpleButton btn_Close;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private UI.Config.ScreenMsgManager screenMsgManager1;
        private DevExpress.XtraEditors.LabelControl lab5;
        private DevExpress.XtraEditors.MemoEdit txtFileDictionary;
        private DXSimpleButton btnCheckItem;
        private DXSimpleButton btnSave;
    }
}