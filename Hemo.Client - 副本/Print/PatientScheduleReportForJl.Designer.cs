namespace Hemo.Client.Print
{
    partial class PatientScheduleReportForJl
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.reportRelationModel1 = new Hemo.Model.ReportRelationModel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.lblDay6 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDay5 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDay4 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDay3 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblAM6 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblAM4 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblAM5 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblAM3 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblAM2 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblAM1 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblRoom = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDay2 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDay1 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportRelationModel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTable1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(770.9583F, 25F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTable1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTable1_BeforePrint);
            this.xrTable1.AfterPrint += new System.EventHandler(this.xrTable1_AfterPrint);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5,
            this.xrTableCell4,
            this.xrTableCell1,
            this.xrTableCell6,
            this.xrTableCell2,
            this.xrTableCell8,
            this.xrTableCell7,
            this.xrTableCell3});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_PATIENT_SCHEDULE.AREANAME")});
            this.xrTableCell5.Font = new System.Drawing.Font("Times New Roman", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrTableCell5.ProcessDuplicates = DevExpress.XtraReports.UI.ValueSuppressType.Suppress;
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.StylePriority.UsePadding = false;
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "xrTableCell5";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell5.Weight = 0.19815025143213561D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_PATIENT_SCHEDULE.BEDNAME")});
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "xrTableCell4";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell4.Weight = 0.18601861777514522D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_PATIENT_SCHEDULE.MONDAY")});
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "xrTableCell1";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell1.Weight = 0.43479828993388875D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_PATIENT_SCHEDULE.TUESDAY")});
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "xrTableCell6";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell6.Weight = 0.43479827260330123D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_PATIENT_SCHEDULE.WEDNESDAY")});
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "xrTableCell2";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell2.Weight = 0.43479827316846065D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_PATIENT_SCHEDULE.THURSDAY")});
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "xrTableCell8";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell8.Weight = 0.43479829313674145D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_PATIENT_SCHEDULE.FRIDAY")});
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "xrTableCell7";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell7.Weight = 0.43479827787795111D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_PATIENT_SCHEDULE.SATURDAY")});
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "xrTableCell3";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell3.Weight = 0.43479819934138347D;
            // 
            // Detail
            // 
            this.Detail.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine1,
            this.xrTable1});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.StylePriority.UseBorders = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            this.Detail.AfterPrint += new System.EventHandler(this.Detail_AfterPrint);
            // 
            // xrLine1
            // 
            this.xrLine1.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(2F, 23F);
            // 
            // reportRelationModel1
            // 
            this.reportRelationModel1.DataSetName = "ReportRelationModel";
            this.reportRelationModel1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblDay6,
            this.lblDay5,
            this.lblDay4,
            this.lblDay3,
            this.lblAM6,
            this.lblAM4,
            this.lblAM5,
            this.lblAM3,
            this.lblAM2,
            this.lblAM1,
            this.lblRoom,
            this.lblDay2,
            this.lblDay1,
            this.lblDate,
            this.xrLabel8});
            this.TopMargin.HeightF = 97.91667F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblDay6
            // 
            this.lblDay6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblDay6.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lblDay6.LocationFloat = new DevExpress.Utils.PointFloat(658.9583F, 51.91666F);
            this.lblDay6.Name = "lblDay6";
            this.lblDay6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblDay6.SizeF = new System.Drawing.SizeF(112F, 23F);
            this.lblDay6.StylePriority.UseBorders = false;
            this.lblDay6.StylePriority.UseFont = false;
            this.lblDay6.StylePriority.UseTextAlignment = false;
            this.lblDay6.Text = "周六";
            this.lblDay6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblDay5
            // 
            this.lblDay5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblDay5.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lblDay5.LocationFloat = new DevExpress.Utils.PointFloat(546.9583F, 51.91669F);
            this.lblDay5.Name = "lblDay5";
            this.lblDay5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblDay5.SizeF = new System.Drawing.SizeF(112F, 23F);
            this.lblDay5.StylePriority.UseBorders = false;
            this.lblDay5.StylePriority.UseFont = false;
            this.lblDay5.StylePriority.UseTextAlignment = false;
            this.lblDay5.Text = "周五";
            this.lblDay5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblDay4
            // 
            this.lblDay4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblDay4.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lblDay4.LocationFloat = new DevExpress.Utils.PointFloat(434.9583F, 51.91666F);
            this.lblDay4.Name = "lblDay4";
            this.lblDay4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblDay4.SizeF = new System.Drawing.SizeF(112F, 23F);
            this.lblDay4.StylePriority.UseBorders = false;
            this.lblDay4.StylePriority.UseFont = false;
            this.lblDay4.StylePriority.UseTextAlignment = false;
            this.lblDay4.Text = "周四";
            this.lblDay4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblDay3
            // 
            this.lblDay3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblDay3.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lblDay3.LocationFloat = new DevExpress.Utils.PointFloat(322.9583F, 51.91666F);
            this.lblDay3.Name = "lblDay3";
            this.lblDay3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblDay3.SizeF = new System.Drawing.SizeF(112F, 23F);
            this.lblDay3.StylePriority.UseBorders = false;
            this.lblDay3.StylePriority.UseFont = false;
            this.lblDay3.StylePriority.UseTextAlignment = false;
            this.lblDay3.Text = "周三";
            this.lblDay3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblAM6
            // 
            this.lblAM6.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.lblAM6.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lblAM6.LocationFloat = new DevExpress.Utils.PointFloat(658.9583F, 74.91665F);
            this.lblAM6.Name = "lblAM6";
            this.lblAM6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblAM6.SizeF = new System.Drawing.SizeF(112F, 23F);
            this.lblAM6.StylePriority.UseBorders = false;
            this.lblAM6.StylePriority.UseFont = false;
            this.lblAM6.StylePriority.UseTextAlignment = false;
            this.lblAM6.Text = "六04/25";
            this.lblAM6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblAM4
            // 
            this.lblAM4.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.lblAM4.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lblAM4.LocationFloat = new DevExpress.Utils.PointFloat(434.9583F, 74.91665F);
            this.lblAM4.Name = "lblAM4";
            this.lblAM4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblAM4.SizeF = new System.Drawing.SizeF(112F, 23F);
            this.lblAM4.StylePriority.UseBorders = false;
            this.lblAM4.StylePriority.UseFont = false;
            this.lblAM4.StylePriority.UseTextAlignment = false;
            this.lblAM4.Text = "四04/23";
            this.lblAM4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblAM5
            // 
            this.lblAM5.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.lblAM5.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lblAM5.LocationFloat = new DevExpress.Utils.PointFloat(546.9583F, 74.91665F);
            this.lblAM5.Name = "lblAM5";
            this.lblAM5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblAM5.SizeF = new System.Drawing.SizeF(112F, 23F);
            this.lblAM5.StylePriority.UseBorders = false;
            this.lblAM5.StylePriority.UseFont = false;
            this.lblAM5.StylePriority.UseTextAlignment = false;
            this.lblAM5.Text = "五04/24";
            this.lblAM5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblAM3
            // 
            this.lblAM3.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.lblAM3.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lblAM3.LocationFloat = new DevExpress.Utils.PointFloat(322.9583F, 74.91665F);
            this.lblAM3.Name = "lblAM3";
            this.lblAM3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblAM3.SizeF = new System.Drawing.SizeF(112F, 23F);
            this.lblAM3.StylePriority.UseBorders = false;
            this.lblAM3.StylePriority.UseFont = false;
            this.lblAM3.StylePriority.UseTextAlignment = false;
            this.lblAM3.Text = "三04/22";
            this.lblAM3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblAM2
            // 
            this.lblAM2.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.lblAM2.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lblAM2.LocationFloat = new DevExpress.Utils.PointFloat(210.9583F, 74.91665F);
            this.lblAM2.Name = "lblAM2";
            this.lblAM2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblAM2.SizeF = new System.Drawing.SizeF(112F, 23F);
            this.lblAM2.StylePriority.UseBorders = false;
            this.lblAM2.StylePriority.UseFont = false;
            this.lblAM2.StylePriority.UseTextAlignment = false;
            this.lblAM2.Text = "二04/21";
            this.lblAM2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblAM1
            // 
            this.lblAM1.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.lblAM1.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lblAM1.LocationFloat = new DevExpress.Utils.PointFloat(98.95834F, 74.91665F);
            this.lblAM1.Name = "lblAM1";
            this.lblAM1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblAM1.SizeF = new System.Drawing.SizeF(112F, 23F);
            this.lblAM1.StylePriority.UseBorders = false;
            this.lblAM1.StylePriority.UseFont = false;
            this.lblAM1.StylePriority.UseTextAlignment = false;
            this.lblAM1.Text = "一04/20";
            this.lblAM1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblRoom
            // 
            this.lblRoom.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.lblRoom.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lblRoom.LocationFloat = new DevExpress.Utils.PointFloat(0F, 74.91668F);
            this.lblRoom.Name = "lblRoom";
            this.lblRoom.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblRoom.SizeF = new System.Drawing.SizeF(98.95833F, 22.99999F);
            this.lblRoom.StylePriority.UseBorders = false;
            this.lblRoom.StylePriority.UseFont = false;
            this.lblRoom.StylePriority.UseTextAlignment = false;
            this.lblRoom.Text = "班次";
            this.lblRoom.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblDay2
            // 
            this.lblDay2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblDay2.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lblDay2.LocationFloat = new DevExpress.Utils.PointFloat(210.9583F, 51.91666F);
            this.lblDay2.Name = "lblDay2";
            this.lblDay2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblDay2.SizeF = new System.Drawing.SizeF(112F, 23F);
            this.lblDay2.StylePriority.UseBorders = false;
            this.lblDay2.StylePriority.UseFont = false;
            this.lblDay2.StylePriority.UseTextAlignment = false;
            this.lblDay2.Text = "周二";
            this.lblDay2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblDay1
            // 
            this.lblDay1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblDay1.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lblDay1.LocationFloat = new DevExpress.Utils.PointFloat(98.95834F, 51.91666F);
            this.lblDay1.Name = "lblDay1";
            this.lblDay1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblDay1.SizeF = new System.Drawing.SizeF(112F, 23F);
            this.lblDay1.StylePriority.UseBorders = false;
            this.lblDay1.StylePriority.UseFont = false;
            this.lblDay1.StylePriority.UseTextAlignment = false;
            this.lblDay1.Text = "周一";
            this.lblDay1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblDate
            // 
            this.lblDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblDate.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lblDate.LocationFloat = new DevExpress.Utils.PointFloat(0F, 51.91666F);
            this.lblDate.Name = "lblDate";
            this.lblDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblDate.SizeF = new System.Drawing.SizeF(98.95834F, 23F);
            this.lblDate.StylePriority.UseBorders = false;
            this.lblDate.StylePriority.UseFont = false;
            this.lblDate.StylePriority.UseTextAlignment = false;
            this.lblDate.Text = "日期";
            this.lblDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel8
            // 
            this.xrLabel8.BackColor = System.Drawing.Color.Transparent;
            this.xrLabel8.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel8.ForeColor = System.Drawing.Color.Black;
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(119.7917F, 10.00001F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 8, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(525.7084F, 35.75001F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UsePadding = false;
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // BottomMargin
            // 
            this.BottomMargin.BorderWidth = 0;
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine2});
            this.BottomMargin.HeightF = 13F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.StylePriority.UseBorderWidth = false;
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLine2
            // 
            this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLine2.Name = "xrLine2";
            this.xrLine2.SizeF = new System.Drawing.SizeF(770.96F, 2F);
            // 
            // PatientScheduleReportForJl
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.DataMember = "LABMASTER";
            this.DataSource = this.reportRelationModel1;
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic);
            this.Margins = new System.Drawing.Printing.Margins(25, 25, 98, 13);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "10.1";
            this.DesignerLoaded += new DevExpress.XtraReports.UserDesigner.DesignerLoadedEventHandler(this.PatientScheduleReportForJl_DesignerLoaded);
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PatientScheduleReportForJl_BeforePrint);
            this.AfterPrint += new System.EventHandler(this.PatientScheduleReportForJl_AfterPrint);
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportRelationModel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRLabel xrLabel8;
        private DevExpress.XtraReports.UI.XRLabel lblDate;
        private DevExpress.XtraReports.UI.XRLabel lblAM3;
        private DevExpress.XtraReports.UI.XRLabel lblAM2;
        private DevExpress.XtraReports.UI.XRLabel lblAM1;
        private DevExpress.XtraReports.UI.XRLabel lblRoom;
        private DevExpress.XtraReports.UI.XRLabel lblDay2;
        private DevExpress.XtraReports.UI.XRLabel lblDay1;
        private DevExpress.XtraReports.UI.XRLabel lblAM6;
        private DevExpress.XtraReports.UI.XRLabel lblAM4;
        private DevExpress.XtraReports.UI.XRLabel lblAM5;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRLabel lblDay6;
        private DevExpress.XtraReports.UI.XRLabel lblDay5;
        private DevExpress.XtraReports.UI.XRLabel lblDay4;
        private DevExpress.XtraReports.UI.XRLabel lblDay3;
        private Model.ReportRelationModel reportRelationModel1;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell8;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell7;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.XRLine xrLine2;
    }
}
