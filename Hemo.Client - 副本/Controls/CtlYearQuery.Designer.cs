namespace Hemo.Client.Controls
{
    partial class CtlYearQuery
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtlYearQuery));
            this.tcDateOp = new DevExpress.XtraTab.XtraTabControl();
            this.tpDate1 = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnInstruction1 = new Hemo.Client.Controls.DXSimpleButton();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnPrint1 = new Hemo.Client.Controls.DXSimpleButton();
            this.beginTime = new DevExpress.XtraEditors.DateEdit();
            this.btnExpExcel1 = new Hemo.Client.Controls.DXSimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnQuery1 = new Hemo.Client.Controls.DXSimpleButton();
            this.endTime = new DevExpress.XtraEditors.DateEdit();
            this.tpDate2 = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lookUpYearTo = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.btnInstruction = new Hemo.Client.Controls.DXSimpleButton();
            this.lookUpYearFrom = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpMonth = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnPrint = new Hemo.Client.Controls.DXSimpleButton();
            this.btnExpExcel = new Hemo.Client.Controls.DXSimpleButton();
            this.btnQuery = new Hemo.Client.Controls.DXSimpleButton();
            this.chkIsYear = new DevExpress.XtraEditors.CheckEdit();
            this.lookUpQuarter = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tcDateOp)).BeginInit();
            this.tcDateOp.SuspendLayout();
            this.tpDate1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties)).BeginInit();
            this.tpDate2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpYearTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpYearFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpQuarter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // tcDateOp
            // 
            this.tcDateOp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcDateOp.Location = new System.Drawing.Point(0, 0);
            this.tcDateOp.Name = "tcDateOp";
            this.tcDateOp.SelectedTabPage = this.tpDate1;
            this.tcDateOp.Size = new System.Drawing.Size(933, 75);
            this.tcDateOp.TabIndex = 2;
            this.tcDateOp.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpDate1,
            this.tpDate2});
            // 
            // tpDate1
            // 
            this.tpDate1.Controls.Add(this.panelControl2);
            this.tpDate1.Image = ((System.Drawing.Image)(resources.GetObject("tpDate1.Image")));
            this.tpDate1.Name = "tpDate1";
            this.tpDate1.Size = new System.Drawing.Size(927, 44);
            this.tpDate1.Text = "日期范围";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnInstruction1);
            this.panelControl2.Controls.Add(this.labelControl5);
            this.panelControl2.Controls.Add(this.btnPrint1);
            this.panelControl2.Controls.Add(this.beginTime);
            this.panelControl2.Controls.Add(this.btnExpExcel1);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Controls.Add(this.btnQuery1);
            this.panelControl2.Controls.Add(this.endTime);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(927, 44);
            this.panelControl2.TabIndex = 36;
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
            this.btnInstruction1.Click += new System.EventHandler(this.btnInstruction1_Click);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 15);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.TabIndex = 31;
            this.labelControl5.Text = "开始日期";
            // 
            // btnPrint1
            // 
            this.btnPrint1.ImageIndex = 6;
            this.btnPrint1.Location = new System.Drawing.Point(750, 11);
            this.btnPrint1.Name = "btnPrint1";
            this.btnPrint1.Size = new System.Drawing.Size(75, 23);
            this.btnPrint1.TabIndex = 35;
            this.btnPrint1.Text = "打印";
            this.btnPrint1.Click += new System.EventHandler(this.btnPrint1_Click);
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
            // btnExpExcel1
            // 
            this.btnExpExcel1.ImageIndex = 4;
            this.btnExpExcel1.Location = new System.Drawing.Point(653, 11);
            this.btnExpExcel1.Name = "btnExpExcel1";
            this.btnExpExcel1.Size = new System.Drawing.Size(91, 23);
            this.btnExpExcel1.TabIndex = 34;
            this.btnExpExcel1.Text = "导出Excel";
            this.btnExpExcel1.Click += new System.EventHandler(this.btnExpExcel1_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(189, 15);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 32;
            this.labelControl4.Text = "结束日期";
            // 
            // btnQuery1
            // 
            this.btnQuery1.ImageIndex = 8;
            this.btnQuery1.Location = new System.Drawing.Point(572, 11);
            this.btnQuery1.Name = "btnQuery1";
            this.btnQuery1.Size = new System.Drawing.Size(75, 23);
            this.btnQuery1.TabIndex = 33;
            this.btnQuery1.Text = "查询";
            this.btnQuery1.Click += new System.EventHandler(this.btnQuery1_Click);
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
            // tpDate2
            // 
            this.tpDate2.Controls.Add(this.panelControl1);
            this.tpDate2.Image = ((System.Drawing.Image)(resources.GetObject("tpDate2.Image")));
            this.tpDate2.Name = "tpDate2";
            this.tpDate2.Size = new System.Drawing.Size(927, 44);
            this.tpDate2.Text = "月度季度年度";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lookUpYearTo);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.btnInstruction);
            this.panelControl1.Controls.Add(this.lookUpYearFrom);
            this.panelControl1.Controls.Add(this.lookUpMonth);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.btnPrint);
            this.panelControl1.Controls.Add(this.btnExpExcel);
            this.panelControl1.Controls.Add(this.btnQuery);
            this.panelControl1.Controls.Add(this.chkIsYear);
            this.panelControl1.Controls.Add(this.lookUpQuarter);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(927, 44);
            this.panelControl1.TabIndex = 2;
            // 
            // lookUpYearTo
            // 
            this.lookUpYearTo.Location = new System.Drawing.Point(142, 12);
            this.lookUpYearTo.Name = "lookUpYearTo";
            this.lookUpYearTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpYearTo.Properties.NullText = "";
            this.lookUpYearTo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lookUpYearTo.Size = new System.Drawing.Size(77, 20);
            this.lookUpYearTo.TabIndex = 823;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(125, 14);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(12, 14);
            this.labelControl6.TabIndex = 822;
            this.labelControl6.Text = "至";
            // 
            // btnInstruction
            // 
            this.btnInstruction.ImageIndex = 2;
            this.btnInstruction.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnInstruction.Location = new System.Drawing.Point(834, 11);
            this.btnInstruction.Name = "btnInstruction";
            this.btnInstruction.Size = new System.Drawing.Size(85, 23);
            this.btnInstruction.TabIndex = 821;
            this.btnInstruction.Text = "报表说明";
            this.btnInstruction.Visible = false;
            this.btnInstruction.Click += new System.EventHandler(this.btnInstruction_Click);
            // 
            // lookUpYearFrom
            // 
            this.lookUpYearFrom.Location = new System.Drawing.Point(44, 12);
            this.lookUpYearFrom.Name = "lookUpYearFrom";
            this.lookUpYearFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpYearFrom.Properties.NullText = "";
            this.lookUpYearFrom.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lookUpYearFrom.Size = new System.Drawing.Size(77, 20);
            this.lookUpYearFrom.TabIndex = 12;
            // 
            // lookUpMonth
            // 
            this.lookUpMonth.Location = new System.Drawing.Point(258, 12);
            this.lookUpMonth.Name = "lookUpMonth";
            this.lookUpMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpMonth.Properties.NullText = "";
            this.lookUpMonth.Size = new System.Drawing.Size(77, 20);
            this.lookUpMonth.TabIndex = 11;
            this.lookUpMonth.EditValueChanged += new System.EventHandler(this.lookUpMonth_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(228, 13);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 14);
            this.labelControl3.TabIndex = 10;
            this.labelControl3.Text = "月度";
            // 
            // btnPrint
            // 
            this.btnPrint.ImageIndex = 6;
            this.btnPrint.Location = new System.Drawing.Point(753, 11);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 9;
            this.btnPrint.Text = "打印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExpExcel
            // 
            this.btnExpExcel.ImageIndex = 10;
            this.btnExpExcel.Location = new System.Drawing.Point(658, 11);
            this.btnExpExcel.Name = "btnExpExcel";
            this.btnExpExcel.Size = new System.Drawing.Size(91, 23);
            this.btnExpExcel.TabIndex = 8;
            this.btnExpExcel.Text = "导出Excel";
            this.btnExpExcel.Click += new System.EventHandler(this.btnExpExcel_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.ImageIndex = 8;
            this.btnQuery.Location = new System.Drawing.Point(578, 11);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 7;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // chkIsYear
            // 
            this.chkIsYear.Location = new System.Drawing.Point(469, 13);
            this.chkIsYear.Name = "chkIsYear";
            this.chkIsYear.Properties.Caption = "按年度或跨年度";
            this.chkIsYear.Size = new System.Drawing.Size(109, 19);
            this.chkIsYear.TabIndex = 6;
            this.chkIsYear.CheckedChanged += new System.EventHandler(this.chkIsYear_CheckedChanged);
            // 
            // lookUpQuarter
            // 
            this.lookUpQuarter.Location = new System.Drawing.Point(380, 12);
            this.lookUpQuarter.Name = "lookUpQuarter";
            this.lookUpQuarter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpQuarter.Properties.NullText = "";
            this.lookUpQuarter.Size = new System.Drawing.Size(83, 20);
            this.lookUpQuarter.TabIndex = 3;
            this.lookUpQuarter.EditValueChanged += new System.EventHandler(this.lookUpQuarter_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(350, 14);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "季度";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(11, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "年度";
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "add.png");
            this.imageCollection1.Images.SetKeyName(1, "del.png");
            this.imageCollection1.Images.SetKeyName(2, "edit.png");
            this.imageCollection1.Images.SetKeyName(3, "exit.png");
            this.imageCollection1.Images.SetKeyName(4, "export.png");
            this.imageCollection1.Images.SetKeyName(5, "preview.png");
            this.imageCollection1.Images.SetKeyName(6, "print.png");
            this.imageCollection1.Images.SetKeyName(7, "save.png");
            this.imageCollection1.Images.SetKeyName(8, "search_16.png");
            this.imageCollection1.Images.SetKeyName(9, "date_add.ico");
            this.imageCollection1.Images.SetKeyName(10, "date_magnify.ico");
            // 
            // CtlYearQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcDateOp);
            this.Name = "CtlYearQuery";
            this.Size = new System.Drawing.Size(933, 75);
            this.Load += new System.EventHandler(this.CtlYearQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tcDateOp)).EndInit();
            this.tcDateOp.ResumeLayout(false);
            this.tpDate1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties)).EndInit();
            this.tpDate2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpYearTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpYearFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpQuarter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tcDateOp;
        private DevExpress.XtraTab.XtraTabPage tpDate1;
        private DevExpress.XtraTab.XtraTabPage tpDate2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LookUpEdit lookUpYearFrom;
        private DevExpress.XtraEditors.LookUpEdit lookUpMonth;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckEdit chkIsYear;
        private DevExpress.XtraEditors.LookUpEdit lookUpQuarter;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Hemo.Client.Controls.DXSimpleButton btnPrint;
        private Hemo.Client.Controls.DXSimpleButton btnExpExcel;
        private Hemo.Client.Controls.DXSimpleButton btnQuery;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.DateEdit endTime;
        private DevExpress.XtraEditors.DateEdit beginTime;
        private Hemo.Client.Controls.DXSimpleButton btnPrint1;
        private Hemo.Client.Controls.DXSimpleButton btnExpExcel1;
        private Hemo.Client.Controls.DXSimpleButton btnQuery1;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DXSimpleButton btnInstruction1;
        private DXSimpleButton btnInstruction;
        private DevExpress.XtraEditors.LookUpEdit lookUpYearTo;
        private DevExpress.XtraEditors.LabelControl labelControl6;
    }
}
