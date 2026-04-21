namespace Hemo.Client.Print
{
    partial class WaterProcessorUseReport
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
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.lblHead = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.lblEmpNo = new DevExpress.XtraReports.UI.XRLabel();
            this.lblHardness = new DevExpress.XtraReports.UI.XRLabel();
            this.lblResidualChlorine = new DevExpress.XtraReports.UI.XRLabel();
            this.lblNormalTag = new DevExpress.XtraReports.UI.XRLabel();
            this.lblResinJar = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCarbonJar = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSandJar = new DevExpress.XtraReports.UI.XRLabel();
            this.lblWasteFlow = new DevExpress.XtraReports.UI.XRLabel();
            this.lblOutConductivity = new DevExpress.XtraReports.UI.XRLabel();
            this.lblInConductivity = new DevExpress.XtraReports.UI.XRLabel();
            this.lblInPressure = new DevExpress.XtraReports.UI.XRLabel();
            this.lblOutPressure = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDate = new DevExpress.XtraReports.UI.XRLabel();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.machineModel1 = new Hemo.Model.MachineModel();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.machineModel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(755F, 25F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_WATERPROCESSOR_USERECORD.USEDATE", "{0:yyyy-MM-dd}")});
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.Text = "xrTableCell1";
            this.xrTableCell1.Weight = 1.00999998309796D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_WATERPROCESSOR_USERECORD.OUTWATER_PRESSURE")});
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.Text = "xrTableCell2";
            this.xrTableCell2.Weight = 0.64999996185304D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_WATERPROCESSOR_USERECORD.INWATER_PRESSURE")});
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.Text = "xrTableCell3";
            this.xrTableCell3.Weight = 0.64999996185302211D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_WATERPROCESSOR_USERECORD.INWATER_CONDUCTIVITY")});
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.Text = "xrTableCell4";
            this.xrTableCell4.Weight = 0.64999998474121212D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_WATERPROCESSOR_USERECORD.OUTWATER_CONDUCTIVITY")});
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.Text = "xrTableCell5";
            this.xrTableCell5.Weight = 0.649999954223646D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_WATERPROCESSOR_USERECORD.WASTEWATER_FLOW")});
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseBorders = false;
            this.xrTableCell6.Text = "xrTableCell6";
            this.xrTableCell6.Weight = 0.64999996185303677D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_WATERPROCESSOR_USERECORD.SANDJAR")});
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseBorders = false;
            this.xrTableCell7.Text = "xrTableCell7";
            this.xrTableCell7.Weight = 0.34999997975276564D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_WATERPROCESSOR_USERECORD.CARBONJAR")});
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseBorders = false;
            this.xrTableCell8.Text = "xrTableCell8";
            this.xrTableCell8.Weight = 0.34999998166011342D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_WATERPROCESSOR_USERECORD.RESINJAR")});
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorders = false;
            this.xrTableCell9.Text = "xrTableCell9";
            this.xrTableCell9.Weight = 0.49999997557127046D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_WATERPROCESSOR_USERECORD.RESIDUALCHLORINE_TESTRESULT")});
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseBorders = false;
            this.xrTableCell10.Text = "xrTableCell10";
            this.xrTableCell10.Weight = 0.64999996900559065D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_WATERPROCESSOR_USERECORD.HARDNESS_TESTRESULT")});
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseBorders = false;
            this.xrTableCell11.Text = "xrTableCell11";
            this.xrTableCell11.Weight = 0.64999996924400927D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_WATERPROCESSOR_USERECORD.EMP_NO")});
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseBorders = false;
            this.xrTableCell12.Text = "xrTableCell12";
            this.xrTableCell12.Weight = 0.78999996267099359D;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblHead,
            this.lblTitle,
            this.lblEmpNo,
            this.lblHardness,
            this.lblResidualChlorine,
            this.lblNormalTag,
            this.lblResinJar,
            this.lblCarbonJar,
            this.lblSandJar,
            this.lblWasteFlow,
            this.lblOutConductivity,
            this.lblInConductivity,
            this.lblInPressure,
            this.lblOutPressure,
            this.lblDate});
            this.TopMargin.HeightF = 150F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblHead
            // 
            this.lblHead.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.lblHead.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50F);
            this.lblHead.Name = "lblHead";
            this.lblHead.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblHead.SizeF = new System.Drawing.SizeF(281.2084F, 35F);
            this.lblHead.StylePriority.UseBorders = false;
            this.lblHead.StylePriority.UseTextAlignment = false;
            this.lblHead.Text = "编号：1   分类：金宝   型号：AK-96";
            this.lblHead.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lblTitle.LocationFloat = new DevExpress.Utils.PointFloat(237F, 10F);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblTitle.SizeF = new System.Drawing.SizeF(281.2084F, 35F);
            this.lblTitle.StylePriority.UseBorders = false;
            this.lblTitle.StylePriority.UseTextAlignment = false;
            this.lblTitle.Text = "水处理机运行记录报表";
            this.lblTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblEmpNo
            // 
            this.lblEmpNo.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblEmpNo.LocationFloat = new DevExpress.Utils.PointFloat(675F, 85.00002F);
            this.lblEmpNo.Name = "lblEmpNo";
            this.lblEmpNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblEmpNo.SizeF = new System.Drawing.SizeF(80F, 65F);
            this.lblEmpNo.StylePriority.UseBorders = false;
            this.lblEmpNo.StylePriority.UseTextAlignment = false;
            this.lblEmpNo.Text = "工号";
            this.lblEmpNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblHardness
            // 
            this.lblHardness.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblHardness.LocationFloat = new DevExpress.Utils.PointFloat(610F, 85.00002F);
            this.lblHardness.Name = "lblHardness";
            this.lblHardness.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblHardness.SizeF = new System.Drawing.SizeF(65F, 65F);
            this.lblHardness.StylePriority.UseBorders = false;
            this.lblHardness.StylePriority.UseTextAlignment = false;
            this.lblHardness.Text = "硬度检测结果";
            this.lblHardness.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblResidualChlorine
            // 
            this.lblResidualChlorine.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblResidualChlorine.LocationFloat = new DevExpress.Utils.PointFloat(545F, 85F);
            this.lblResidualChlorine.Name = "lblResidualChlorine";
            this.lblResidualChlorine.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblResidualChlorine.SizeF = new System.Drawing.SizeF(65F, 65F);
            this.lblResidualChlorine.StylePriority.UseBorders = false;
            this.lblResidualChlorine.StylePriority.UseTextAlignment = false;
            this.lblResidualChlorine.Text = "余氯检测结果";
            this.lblResidualChlorine.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblNormalTag
            // 
            this.lblNormalTag.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.lblNormalTag.LocationFloat = new DevExpress.Utils.PointFloat(425F, 85F);
            this.lblNormalTag.Name = "lblNormalTag";
            this.lblNormalTag.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblNormalTag.SizeF = new System.Drawing.SizeF(120F, 30F);
            this.lblNormalTag.StylePriority.UseBorders = false;
            this.lblNormalTag.StylePriority.UseTextAlignment = false;
            this.lblNormalTag.Text = "正常：√ 故障：×";
            this.lblNormalTag.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblResinJar
            // 
            this.lblResinJar.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblResinJar.LocationFloat = new DevExpress.Utils.PointFloat(495F, 115F);
            this.lblResinJar.Name = "lblResinJar";
            this.lblResinJar.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblResinJar.SizeF = new System.Drawing.SizeF(50F, 35F);
            this.lblResinJar.StylePriority.UseBorders = false;
            this.lblResinJar.StylePriority.UseTextAlignment = false;
            this.lblResinJar.Text = "树脂罐";
            this.lblResinJar.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblCarbonJar
            // 
            this.lblCarbonJar.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblCarbonJar.LocationFloat = new DevExpress.Utils.PointFloat(460F, 115F);
            this.lblCarbonJar.Name = "lblCarbonJar";
            this.lblCarbonJar.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblCarbonJar.SizeF = new System.Drawing.SizeF(35F, 35F);
            this.lblCarbonJar.StylePriority.UseBorders = false;
            this.lblCarbonJar.StylePriority.UseTextAlignment = false;
            this.lblCarbonJar.Text = "碳罐";
            this.lblCarbonJar.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblSandJar
            // 
            this.lblSandJar.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblSandJar.LocationFloat = new DevExpress.Utils.PointFloat(425F, 115F);
            this.lblSandJar.Name = "lblSandJar";
            this.lblSandJar.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblSandJar.SizeF = new System.Drawing.SizeF(35F, 35F);
            this.lblSandJar.StylePriority.UseBorders = false;
            this.lblSandJar.StylePriority.UseTextAlignment = false;
            this.lblSandJar.Text = "沙罐";
            this.lblSandJar.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblWasteFlow
            // 
            this.lblWasteFlow.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblWasteFlow.LocationFloat = new DevExpress.Utils.PointFloat(360F, 85.00002F);
            this.lblWasteFlow.Name = "lblWasteFlow";
            this.lblWasteFlow.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblWasteFlow.SizeF = new System.Drawing.SizeF(65F, 65F);
            this.lblWasteFlow.StylePriority.UseBorders = false;
            this.lblWasteFlow.StylePriority.UseTextAlignment = false;
            this.lblWasteFlow.Text = "废水流量(ml/min)";
            this.lblWasteFlow.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblOutConductivity
            // 
            this.lblOutConductivity.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblOutConductivity.LocationFloat = new DevExpress.Utils.PointFloat(295F, 85.00002F);
            this.lblOutConductivity.Name = "lblOutConductivity";
            this.lblOutConductivity.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblOutConductivity.SizeF = new System.Drawing.SizeF(65F, 65F);
            this.lblOutConductivity.StylePriority.UseBorders = false;
            this.lblOutConductivity.StylePriority.UseTextAlignment = false;
            this.lblOutConductivity.Text = "产水电导度(us/cm)";
            this.lblOutConductivity.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblInConductivity
            // 
            this.lblInConductivity.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblInConductivity.LocationFloat = new DevExpress.Utils.PointFloat(230F, 85F);
            this.lblInConductivity.Name = "lblInConductivity";
            this.lblInConductivity.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblInConductivity.SizeF = new System.Drawing.SizeF(65F, 65F);
            this.lblInConductivity.StylePriority.UseBorders = false;
            this.lblInConductivity.StylePriority.UseTextAlignment = false;
            this.lblInConductivity.Text = "进水电导度(us/cm)";
            this.lblInConductivity.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblInPressure
            // 
            this.lblInPressure.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblInPressure.LocationFloat = new DevExpress.Utils.PointFloat(165F, 85F);
            this.lblInPressure.Name = "lblInPressure";
            this.lblInPressure.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblInPressure.SizeF = new System.Drawing.SizeF(65F, 65F);
            this.lblInPressure.StylePriority.UseBorders = false;
            this.lblInPressure.StylePriority.UseTextAlignment = false;
            this.lblInPressure.Text = "RO进水压力(MPa)";
            this.lblInPressure.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblOutPressure
            // 
            this.lblOutPressure.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblOutPressure.LocationFloat = new DevExpress.Utils.PointFloat(100F, 85F);
            this.lblOutPressure.Name = "lblOutPressure";
            this.lblOutPressure.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblOutPressure.SizeF = new System.Drawing.SizeF(65F, 65F);
            this.lblOutPressure.StylePriority.UseBorders = false;
            this.lblOutPressure.StylePriority.UseTextAlignment = false;
            this.lblOutPressure.Text = "高压泵出水压力(MPa)";
            this.lblOutPressure.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblDate
            // 
            this.lblDate.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblDate.LocationFloat = new DevExpress.Utils.PointFloat(0F, 85F);
            this.lblDate.Name = "lblDate";
            this.lblDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblDate.SizeF = new System.Drawing.SizeF(100F, 65F);
            this.lblDate.StylePriority.UseBorders = false;
            this.lblDate.StylePriority.UseTextAlignment = false;
            this.lblDate.Text = "日期";
            this.lblDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // machineModel1
            // 
            this.machineModel1.DataSetName = "MachineModel";
            this.machineModel1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // WaterProcessorUseReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.DataSource = this.machineModel1;
            this.Margins = new System.Drawing.Printing.Margins(50, 22, 150, 100);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "10.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.machineModel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRLabel lblDate;
        private DevExpress.XtraReports.UI.XRLabel lblOutPressure;
        private DevExpress.XtraReports.UI.XRLabel lblInPressure;
        private DevExpress.XtraReports.UI.XRLabel lblInConductivity;
        private DevExpress.XtraReports.UI.XRLabel lblOutConductivity;
        private DevExpress.XtraReports.UI.XRLabel lblWasteFlow;
        private DevExpress.XtraReports.UI.XRLabel lblCarbonJar;
        private DevExpress.XtraReports.UI.XRLabel lblSandJar;
        private DevExpress.XtraReports.UI.XRLabel lblResinJar;
        private DevExpress.XtraReports.UI.XRLabel lblNormalTag;
        private DevExpress.XtraReports.UI.XRLabel lblResidualChlorine;
        private DevExpress.XtraReports.UI.XRLabel lblHardness;
        private DevExpress.XtraReports.UI.XRLabel lblEmpNo;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private Model.MachineModel machineModel1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell7;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell8;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell9;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell10;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell11;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell12;
        private DevExpress.XtraReports.UI.XRLabel lblTitle;
        private DevExpress.XtraReports.UI.XRLabel lblHead;
    }
}
