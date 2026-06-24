namespace Hemo.Client.UI.Hemodialysis
{
    partial class QueryEstimateSufficiency
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryEstimateSufficiency));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnPrint = new Hemo.Client.Controls.DXSimpleButton();
            this.btnQuery = new Hemo.Client.Controls.DXSimpleButton();
            this.chkMDRD = new DevExpress.XtraEditors.CheckEdit();
            this.chkTS = new DevExpress.XtraEditors.CheckEdit();
            this.chkURR = new DevExpress.XtraEditors.CheckEdit();
            this.txtToDate = new DevExpress.XtraEditors.DateEdit();
            this.txtFromDate = new DevExpress.XtraEditors.DateEdit();
            this.lblToDate = new DevExpress.XtraEditors.LabelControl();
            this.lblFromDate = new DevExpress.XtraEditors.LabelControl();
            this.gcSufficiency = new DevExpress.XtraGrid.GridControl();
            this.gvSufficiency = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.printingSystem1 = new DevExpress.XtraPrinting.PrintingSystem(this.components);
            this.printableComponentLink1 = new DevExpress.XtraPrinting.PrintableComponentLink(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkMDRD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkURR.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSufficiency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSufficiency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.printableComponentLink1.ImageCollection)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnPrint);
            this.panelControl1.Controls.Add(this.btnQuery);
            this.panelControl1.Controls.Add(this.chkMDRD);
            this.panelControl1.Controls.Add(this.chkTS);
            this.panelControl1.Controls.Add(this.chkURR);
            this.panelControl1.Controls.Add(this.txtToDate);
            this.panelControl1.Controls.Add(this.txtFromDate);
            this.panelControl1.Controls.Add(this.lblToDate);
            this.panelControl1.Controls.Add(this.lblFromDate);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(784, 40);
            this.panelControl1.TabIndex = 0;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.ImageIndex = 6;
            this.btnPrint.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(684, 8);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(73, 23);
            this.btnPrint.TabIndex = 324;
            this.btnPrint.Text = "打印(&Q)";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.ImageIndex = 8;
            this.btnQuery.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnQuery.Location = new System.Drawing.Point(605, 8);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(73, 23);
            this.btnQuery.TabIndex = 323;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // chkMDRD
            // 
            this.chkMDRD.Location = new System.Drawing.Point(511, 12);
            this.chkMDRD.Name = "chkMDRD";
            this.chkMDRD.Properties.Caption = "MDRD";
            this.chkMDRD.Size = new System.Drawing.Size(55, 19);
            this.chkMDRD.TabIndex = 322;
            // 
            // chkTS
            // 
            this.chkTS.Location = new System.Drawing.Point(465, 12);
            this.chkTS.Name = "chkTS";
            this.chkTS.Properties.Caption = "TS";
            this.chkTS.Size = new System.Drawing.Size(40, 19);
            this.chkTS.TabIndex = 321;
            // 
            // chkURR
            // 
            this.chkURR.Location = new System.Drawing.Point(384, 12);
            this.chkURR.Name = "chkURR";
            this.chkURR.Properties.Caption = "URR|Kt/V";
            this.chkURR.Size = new System.Drawing.Size(75, 19);
            this.chkURR.TabIndex = 320;
            // 
            // txtToDate
            // 
            this.txtToDate.EditValue = null;
            this.txtToDate.Location = new System.Drawing.Point(235, 9);
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtToDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.txtToDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtToDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtToDate.Size = new System.Drawing.Size(100, 21);
            this.txtToDate.TabIndex = 319;
            // 
            // txtFromDate
            // 
            this.txtFromDate.EditValue = null;
            this.txtFromDate.Location = new System.Drawing.Point(66, 9);
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFromDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.txtFromDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtFromDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtFromDate.Size = new System.Drawing.Size(100, 21);
            this.txtFromDate.TabIndex = 318;
            // 
            // lblToDate
            // 
            this.lblToDate.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDate.Appearance.Options.UseFont = true;
            this.lblToDate.Location = new System.Drawing.Point(181, 12);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(48, 17);
            this.lblToDate.TabIndex = 317;
            this.lblToDate.Text = "结束日期";
            // 
            // lblFromDate
            // 
            this.lblFromDate.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDate.Appearance.Options.UseFont = true;
            this.lblFromDate.Location = new System.Drawing.Point(12, 12);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(48, 17);
            this.lblFromDate.TabIndex = 316;
            this.lblFromDate.Text = "开始日期";
            // 
            // gcSufficiency
            // 
            this.gcSufficiency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSufficiency.Location = new System.Drawing.Point(0, 40);
            this.gcSufficiency.MainView = this.gvSufficiency;
            this.gcSufficiency.Name = "gcSufficiency";
            this.gcSufficiency.Size = new System.Drawing.Size(784, 322);
            this.gcSufficiency.TabIndex = 6;
            this.gcSufficiency.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSufficiency});
            // 
            // gvSufficiency
            // 
            this.gvSufficiency.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn18,
            this.gridColumn19,
            this.gridColumn13,
            this.gridColumn15,
            this.gridColumn9,
            this.gridColumn12,
            this.gridColumn11,
            this.gridColumn3,
            this.gridColumn16,
            this.gridColumn17,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn10,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn4});
            this.gvSufficiency.GridControl = this.gcSufficiency;
            this.gvSufficiency.Name = "gvSufficiency";
            this.gvSufficiency.OptionsBehavior.Editable = false;
            this.gvSufficiency.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvSufficiency.OptionsView.ColumnAutoWidth = false;
            this.gvSufficiency.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "录入日期";
            this.gridColumn5.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn5.FieldName = "CREATE_DATE";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            this.gridColumn5.Width = 100;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "姓名";
            this.gridColumn18.FieldName = "NAME";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 1;
            this.gridColumn18.Width = 66;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "性别";
            this.gridColumn19.FieldName = "SEX";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 3;
            this.gridColumn19.Width = 67;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "年龄";
            this.gridColumn13.FieldName = "AGE";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 2;
            this.gridColumn13.Width = 71;
            // 
            // gridColumn15
            // 
            this.gridColumn15.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn15.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn15.Caption = "是否黑人";
            this.gridColumn15.FieldName = "BLACK";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 4;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "体重（kg）";
            this.gridColumn9.FieldName = "WEIGHT";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 5;
            this.gridColumn9.Width = 74;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn12.Caption = "URR";
            this.gridColumn12.FieldName = "DISPLAY_URR";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 6;
            this.gridColumn12.Width = 63;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Kt/V";
            this.gridColumn11.FieldName = "SPKT_V";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 7;
            this.gridColumn11.Width = 59;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn3.Caption = "转铁蛋白饱和度";
            this.gridColumn3.FieldName = "DISPLAY_TS";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 8;
            this.gridColumn3.Width = 105;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "肾小球滤过率";
            this.gridColumn16.FieldName = "GFR";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 9;
            this.gridColumn16.Width = 95;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "肌酐清除率";
            this.gridColumn17.FieldName = "CCR";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 10;
            this.gridColumn17.Width = 80;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "透前血尿素";
            this.gridColumn6.FieldName = "BEFORE_BLOODUREA";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 11;
            this.gridColumn6.Width = 80;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "透后血尿素";
            this.gridColumn7.FieldName = "AFTER_BLOODUREA";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 12;
            this.gridColumn7.Width = 80;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "超滤量（L）";
            this.gridColumn8.FieldName = "UF";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 13;
            this.gridColumn8.Width = 85;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "时间（h）";
            this.gridColumn10.FieldName = "TIME";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 14;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "血清铁（μmol/L）";
            this.gridColumn1.FieldName = "SI";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 15;
            this.gridColumn1.Width = 115;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "总铁结合力（μmol/L）";
            this.gridColumn2.FieldName = "TIBC";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 16;
            this.gridColumn2.Width = 140;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "血清肌酐（mg/dL）";
            this.gridColumn4.FieldName = "SCR";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 17;
            this.gridColumn4.Width = 125;
            // 
            // printingSystem1
            // 
            this.printingSystem1.Links.AddRange(new object[] {
            this.printableComponentLink1});
            // 
            // printableComponentLink1
            // 
            this.printableComponentLink1.Component = this.gcSufficiency;
            // 
            // 
            // 
            this.printableComponentLink1.ImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("printableComponentLink1.ImageCollection.ImageStream")));
            this.printableComponentLink1.Landscape = true;
            this.printableComponentLink1.Margins = new System.Drawing.Printing.Margins(50, 50, 50, 50);
            this.printableComponentLink1.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.printableComponentLink1.PrintingSystem = this.printingSystem1;
            this.printableComponentLink1.PrintingSystemBase = this.printingSystem1;
            this.printableComponentLink1.CreateReportHeaderArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(this.printableComponentLink1_CreateReportHeaderArea);
            // 
            // QueryEstimateSufficiency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 362);
            this.Controls.Add(this.gcSufficiency);
            this.Controls.Add(this.panelControl1);
            this.Name = "QueryEstimateSufficiency";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "查询患者URR|Kt/V|TS|MDRD评估记录";
            this.Load += new System.EventHandler(this.QueryEstimateSufficiency_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkMDRD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkURR.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSufficiency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSufficiency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.printableComponentLink1.ImageCollection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.DateEdit txtToDate;
        private DevExpress.XtraEditors.DateEdit txtFromDate;
        private DevExpress.XtraEditors.LabelControl lblToDate;
        private DevExpress.XtraEditors.LabelControl lblFromDate;
        private DevExpress.XtraEditors.CheckEdit chkURR;
        private DevExpress.XtraEditors.CheckEdit chkTS;
        private DevExpress.XtraEditors.CheckEdit chkMDRD;
        private Controls.DXSimpleButton btnQuery;
        private DevExpress.XtraGrid.GridControl gcSufficiency;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSufficiency;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private Controls.DXSimpleButton btnPrint;
        private DevExpress.XtraPrinting.PrintingSystem printingSystem1;
        private DevExpress.XtraPrinting.PrintableComponentLink printableComponentLink1;
    }
}