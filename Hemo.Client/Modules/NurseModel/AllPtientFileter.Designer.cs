namespace Hemo.Client.Modules.NurseModel
{
    partial class AllPtientFileter
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
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.dateEditYear = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnExport = new Hemo.Client.Controls.DXSimpleButton();
            this.btnQuery = new Hemo.Client.Controls.DXSimpleButton();
            this.txtNAME = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.patientExtendUI1 = new Hemo.Client.UI.PatientFixUI.PatientExtendUI();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditYear.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNAME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl3
            // 
            this.panelControl3.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelControl3.Appearance.Options.UseBackColor = true;
            this.panelControl3.Controls.Add(this.labelControl5);
            this.panelControl3.Controls.Add(this.radioGroup1);
            this.panelControl3.Controls.Add(this.dateEditYear);
            this.panelControl3.Controls.Add(this.labelControl2);
            this.panelControl3.Controls.Add(this.btnExport);
            this.panelControl3.Controls.Add(this.btnQuery);
            this.panelControl3.Controls.Add(this.txtNAME);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1022, 47);
            this.panelControl3.TabIndex = 50;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl5.Location = new System.Drawing.Point(563, 12);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(87, 21);
            this.labelControl5.TabIndex = 313;
            this.labelControl5.Text = "姓名/透析号";
            // 
            // radioGroup1
            // 
            this.radioGroup1.EditValue = "0";
            this.radioGroup1.Location = new System.Drawing.Point(208, 8);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroup1.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.Appearance.Options.UseFont = true;
            this.radioGroup1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "在透病人"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "新入科病人"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("2", "转出病人")});
            this.radioGroup1.Size = new System.Drawing.Size(373, 30);
            this.radioGroup1.TabIndex = 316;
            // 
            // dateEditYear
            // 
            this.dateEditYear.EditValue = null;
            this.dateEditYear.Location = new System.Drawing.Point(71, 8);
            this.dateEditYear.Name = "dateEditYear";
            this.dateEditYear.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.dateEditYear.Properties.Appearance.Options.UseFont = true;
            this.dateEditYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditYear.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditYear.Properties.DisplayFormat.FormatString = "yyyy";
            this.dateEditYear.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateEditYear.Properties.EditFormat.FormatString = "yyyy";
            this.dateEditYear.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateEditYear.Size = new System.Drawing.Size(127, 28);
            this.dateEditYear.TabIndex = 315;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl2.Location = new System.Drawing.Point(8, 12);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 21);
            this.labelControl2.TabIndex = 313;
            this.labelControl2.Text = "年份：";
            // 
            // btnExport
            // 
            this.btnExport.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnExport.Appearance.Options.UseFont = true;
            this.btnExport.ImageIndex = 14;
            this.btnExport.Location = new System.Drawing.Point(915, 8);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(101, 33);
            this.btnExport.TabIndex = 12;
            this.btnExport.Text = "导出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnQuery.Appearance.Options.UseFont = true;
            this.btnQuery.ImageIndex = 8;
            this.btnQuery.Location = new System.Drawing.Point(806, 8);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(101, 33);
            this.btnQuery.TabIndex = 12;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtNAME
            // 
            this.txtNAME.EditValue = "";
            this.txtNAME.EnterMoveNextControl = true;
            this.txtNAME.Location = new System.Drawing.Point(672, 8);
            this.txtNAME.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtNAME.Name = "txtNAME";
            this.txtNAME.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtNAME.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtNAME.Properties.Appearance.Options.UseFont = true;
            this.txtNAME.Properties.Appearance.Options.UseForeColor = true;
            this.txtNAME.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtNAME.Size = new System.Drawing.Size(127, 28);
            this.txtNAME.TabIndex = 10;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl4.Location = new System.Drawing.Point(20, 73);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(0, 16);
            this.labelControl4.TabIndex = 314;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl3.Location = new System.Drawing.Point(18, 12);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(0, 16);
            this.labelControl3.TabIndex = 314;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 450);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1022, 97);
            this.panelControl1.TabIndex = 52;
            // 
            // patientExtendUI1
            // 
            this.patientExtendUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientExtendUI1.Location = new System.Drawing.Point(0, 47);
            this.patientExtendUI1.Name = "patientExtendUI1";
            this.patientExtendUI1.Size = new System.Drawing.Size(1022, 403);
            this.patientExtendUI1.TabIndex = 51;
            // 
            // AllPtientFileter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.patientExtendUI1);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl1);
            this.Name = "AllPtientFileter";
            this.Size = new System.Drawing.Size(1022, 547);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditYear.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNAME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl3;
        private Controls.DXSimpleButton btnQuery;
        private UI.PatientFixUI.PatientExtendUI patientExtendUI1;
        private DevExpress.XtraEditors.DateEdit dateEditYear;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private Controls.DXSimpleButton btnExport;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtNAME;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
    }
}
