namespace Hemo.Client.UI.Lab
{
    partial class HBTrendChart
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
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.cmbEND_Time = new DevExpress.XtraEditors.TimeEdit();
            this.cmbEND_DATE = new DevExpress.XtraEditors.DateEdit();
            this.labelControl62 = new DevExpress.XtraEditors.LabelControl();
            this.cmbSTART_Time = new DevExpress.XtraEditors.TimeEdit();
            this.cmbSTART_DATE = new DevExpress.XtraEditors.DateEdit();
            this.ctlUserLongInfo1 = new Hemo.Client.Controls.CtlUserLongInfo();
            this.ctlSignChart1 = new Hemo.Client.Controls.SignChart.CtlSignChart();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_Time.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_DATE.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_DATE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_Time.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_DATE.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_DATE.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.ImageIndex = 4;
            this.btnSearch.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(258, 53);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 391;
            this.btnSearch.Text = "查询(&Q)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbEND_Time
            // 
            this.cmbEND_Time.EditValue = new System.DateTime(2013, 4, 8, 0, 0, 0, 0);
            this.cmbEND_Time.Location = new System.Drawing.Point(711, 54);
            this.cmbEND_Time.Name = "cmbEND_Time";
            this.cmbEND_Time.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbEND_Time.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.cmbEND_Time.Properties.Appearance.Options.UseBackColor = true;
            this.cmbEND_Time.Properties.Appearance.Options.UseForeColor = true;
            this.cmbEND_Time.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbEND_Time.Size = new System.Drawing.Size(81, 20);
            this.cmbEND_Time.TabIndex = 390;
            this.cmbEND_Time.Visible = false;
            // 
            // cmbEND_DATE
            // 
            this.cmbEND_DATE.EditValue = null;
            this.cmbEND_DATE.EnterMoveNextControl = true;
            this.cmbEND_DATE.Location = new System.Drawing.Point(122, 53);
            this.cmbEND_DATE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbEND_DATE.Name = "cmbEND_DATE";
            this.cmbEND_DATE.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbEND_DATE.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.cmbEND_DATE.Properties.Appearance.Options.UseFont = true;
            this.cmbEND_DATE.Properties.Appearance.Options.UseForeColor = true;
            this.cmbEND_DATE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbEND_DATE.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbEND_DATE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cmbEND_DATE.Size = new System.Drawing.Size(88, 24);
            this.cmbEND_DATE.TabIndex = 389;
            // 
            // labelControl62
            // 
            this.labelControl62.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl62.Location = new System.Drawing.Point(106, 56);
            this.labelControl62.Name = "labelControl62";
            this.labelControl62.Size = new System.Drawing.Size(9, 17);
            this.labelControl62.TabIndex = 388;
            this.labelControl62.Text = "~";
            // 
            // cmbSTART_Time
            // 
            this.cmbSTART_Time.EditValue = new System.DateTime(2013, 4, 8, 0, 0, 0, 0);
            this.cmbSTART_Time.Location = new System.Drawing.Point(624, 53);
            this.cmbSTART_Time.Name = "cmbSTART_Time";
            this.cmbSTART_Time.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbSTART_Time.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.cmbSTART_Time.Properties.Appearance.Options.UseBackColor = true;
            this.cmbSTART_Time.Properties.Appearance.Options.UseForeColor = true;
            this.cmbSTART_Time.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbSTART_Time.Size = new System.Drawing.Size(81, 20);
            this.cmbSTART_Time.TabIndex = 387;
            this.cmbSTART_Time.Visible = false;
            // 
            // cmbSTART_DATE
            // 
            this.cmbSTART_DATE.EditValue = null;
            this.cmbSTART_DATE.EnterMoveNextControl = true;
            this.cmbSTART_DATE.Location = new System.Drawing.Point(12, 52);
            this.cmbSTART_DATE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbSTART_DATE.Name = "cmbSTART_DATE";
            this.cmbSTART_DATE.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbSTART_DATE.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.cmbSTART_DATE.Properties.Appearance.Options.UseFont = true;
            this.cmbSTART_DATE.Properties.Appearance.Options.UseForeColor = true;
            this.cmbSTART_DATE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSTART_DATE.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbSTART_DATE.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cmbSTART_DATE.Size = new System.Drawing.Size(88, 24);
            this.cmbSTART_DATE.TabIndex = 386;
            // 
            // ctlUserLongInfo1
            // 
            this.ctlUserLongInfo1.DIAGNOSE = "";
            this.ctlUserLongInfo1.FormContainer = null;
            this.ctlUserLongInfo1.HEMODIALYSIS_ID = "";
            this.ctlUserLongInfo1.Location = new System.Drawing.Point(12, 2);
            this.ctlUserLongInfo1.Name = "ctlUserLongInfo1";
            this.ctlUserLongInfo1.PanBorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.ctlUserLongInfo1.PanelWidth = 808;
            this.ctlUserLongInfo1.PatientType = "";
            this.ctlUserLongInfo1.PatientTypeEnabled = true;
            this.ctlUserLongInfo1.Size = new System.Drawing.Size(808, 44);
            this.ctlUserLongInfo1.TabIndex = 259;
            // 
            // ctlSignChart1
            // 
            this.ctlSignChart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlSignChart1.HEMODIALYSIS_ID = "";
            this.ctlSignChart1.Location = new System.Drawing.Point(12, 81);
            this.ctlSignChart1.Name = "ctlSignChart1";
            this.ctlSignChart1.Size = new System.Drawing.Size(808, 319);
            this.ctlSignChart1.TabIndex = 0;
            // 
            // HBTrendChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 412);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cmbEND_Time);
            this.Controls.Add(this.cmbEND_DATE);
            this.Controls.Add(this.labelControl62);
            this.Controls.Add(this.cmbSTART_Time);
            this.Controls.Add(this.cmbSTART_DATE);
            this.Controls.Add(this.ctlUserLongInfo1);
            this.Controls.Add(this.ctlSignChart1);
            this.Name = "HBTrendChart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "血红蛋白趋势图";
            this.Load += new System.EventHandler(this.HBTrendChart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_Time.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_DATE.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEND_DATE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_Time.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_DATE.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSTART_DATE.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.SignChart.CtlSignChart ctlSignChart1;
        private Controls.CtlUserLongInfo ctlUserLongInfo1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.TimeEdit cmbEND_Time;
        private DevExpress.XtraEditors.DateEdit cmbEND_DATE;
        private DevExpress.XtraEditors.LabelControl labelControl62;
        private DevExpress.XtraEditors.TimeEdit cmbSTART_Time;
        private DevExpress.XtraEditors.DateEdit cmbSTART_DATE;
    }
}