namespace Hemo.Client.Print
{
    partial class MachineUserReportNew
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
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.lblDEALWITH = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSIGN_NAME = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDAY_WAY = new DevExpress.XtraReports.UI.XRLabel();
            this.lblOUTER_DEGASSING = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCLASS_WAY = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.lblHead = new DevExpress.XtraReports.UI.XRLabel();
            this.lblINNER_DEGASSING = new DevExpress.XtraReports.UI.XRLabel();
            this.lblUSERTIME = new DevExpress.XtraReports.UI.XRLabel();
            this.lblWORKING = new DevExpress.XtraReports.UI.XRLabel();
            this.lblBANCI_ID = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDEGASSING = new DevExpress.XtraReports.UI.XRLabel();
            this.lblMACHINE_ALARM = new DevExpress.XtraReports.UI.XRLabel();
            this.lblMACHINE_CHECK = new DevExpress.XtraReports.UI.XRLabel();
            this.lblUSEDATE = new DevExpress.XtraReports.UI.XRLabel();
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
            this.xrTable1.SizeF = new System.Drawing.SizeF(1169F, 25F);
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
            this.xrTableCell12,
            this.xrTableCell13});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_MACHINE_USERECORD.USEDATE", "{0:yyyy-MM-dd}")});
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.Text = "xrTableCell1";
            this.xrTableCell1.Weight = 1D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_MACHINE_USERECORD.BANCI_ID")});
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.Text = "xrTableCell2";
            this.xrTableCell2.Weight = 0.65000000000000013D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_MACHINE_USERECORD.MACHINE_CHECK")});
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.Text = "xrTableCell3";
            this.xrTableCell3.Weight = 0.7999999999999996D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_MACHINE_USERECORD.MACHINE_ALARM")});
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.Text = "xrTableCell4";
            this.xrTableCell4.Weight = 0.80000000000000016D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_MACHINE_USERECORD.DEGASSING")});
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.Text = "xrTableCell5";
            this.xrTableCell5.Weight = 0.79999999999999982D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_MACHINE_USERECORD.WORKING")});
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseBorders = false;
            this.xrTableCell6.Text = "xrTableCell6";
            this.xrTableCell6.Weight = 0.8D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_MACHINE_USERECORD.USERTIME")});
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseBorders = false;
            this.xrTableCell7.Text = "xrTableCell7";
            this.xrTableCell7.Weight = 0.65000000000000013D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_MACHINE_USERECORD.INNER_DEGASSING")});
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseBorders = false;
            this.xrTableCell8.Text = "xrTableCell8";
            this.xrTableCell8.Weight = 0.64999999999999991D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_MACHINE_USERECORD.CLASS_WAY")});
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.Text = "xrTableCell9";
            this.xrTableCell9.Weight = 1D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_MACHINE_USERECORD.OUTER_DEGASSING")});
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Text = "xrTableCell10";
            this.xrTableCell10.Weight = 0.65000000000000013D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_MACHINE_USERECORD.DAY_WAY")});
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.Text = "xrTableCell11";
            this.xrTableCell11.Weight = 1D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_MACHINE_USERECORD.SIGN_NAME")});
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.Text = "xrTableCell12";
            this.xrTableCell12.Weight = 0.65D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MED_MACHINE_USERECORD.DEALWITH")});
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Text = "xrTableCell13";
            this.xrTableCell13.Weight = 2.2399987792968745D;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblDEALWITH,
            this.lblSIGN_NAME,
            this.lblDAY_WAY,
            this.lblOUTER_DEGASSING,
            this.lblCLASS_WAY,
            this.lblTitle,
            this.lblHead,
            this.lblINNER_DEGASSING,
            this.lblUSERTIME,
            this.lblWORKING,
            this.lblBANCI_ID,
            this.lblDEGASSING,
            this.lblMACHINE_ALARM,
            this.lblMACHINE_CHECK,
            this.lblUSEDATE});
            this.TopMargin.HeightF = 150F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblDEALWITH
            // 
            this.lblDEALWITH.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblDEALWITH.LocationFloat = new DevExpress.Utils.PointFloat(945F, 84.99998F);
            this.lblDEALWITH.Name = "lblDEALWITH";
            this.lblDEALWITH.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblDEALWITH.SizeF = new System.Drawing.SizeF(224F, 64.99999F);
            this.lblDEALWITH.StylePriority.UseBorders = false;
            this.lblDEALWITH.StylePriority.UseTextAlignment = false;
            this.lblDEALWITH.Text = "处理";
            this.lblDEALWITH.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblSIGN_NAME
            // 
            this.lblSIGN_NAME.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblSIGN_NAME.LocationFloat = new DevExpress.Utils.PointFloat(880F, 84.99998F);
            this.lblSIGN_NAME.Name = "lblSIGN_NAME";
            this.lblSIGN_NAME.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblSIGN_NAME.SizeF = new System.Drawing.SizeF(65F, 65F);
            this.lblSIGN_NAME.StylePriority.UseBorders = false;
            this.lblSIGN_NAME.StylePriority.UseTextAlignment = false;
            this.lblSIGN_NAME.Text = "操作者";
            this.lblSIGN_NAME.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblDAY_WAY
            // 
            this.lblDAY_WAY.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblDAY_WAY.LocationFloat = new DevExpress.Utils.PointFloat(780F, 85.00004F);
            this.lblDAY_WAY.Name = "lblDAY_WAY";
            this.lblDAY_WAY.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblDAY_WAY.SizeF = new System.Drawing.SizeF(100F, 65F);
            this.lblDAY_WAY.StylePriority.UseBorders = false;
            this.lblDAY_WAY.StylePriority.UseTextAlignment = false;
            this.lblDAY_WAY.Text = "机器外部消毒方法";
            this.lblDAY_WAY.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblOUTER_DEGASSING
            // 
            this.lblOUTER_DEGASSING.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblOUTER_DEGASSING.LocationFloat = new DevExpress.Utils.PointFloat(715F, 84.99998F);
            this.lblOUTER_DEGASSING.Name = "lblOUTER_DEGASSING";
            this.lblOUTER_DEGASSING.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblOUTER_DEGASSING.SizeF = new System.Drawing.SizeF(65F, 65F);
            this.lblOUTER_DEGASSING.StylePriority.UseBorders = false;
            this.lblOUTER_DEGASSING.StylePriority.UseTextAlignment = false;
            this.lblOUTER_DEGASSING.Text = "机器外部消毒";
            this.lblOUTER_DEGASSING.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblCLASS_WAY
            // 
            this.lblCLASS_WAY.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblCLASS_WAY.LocationFloat = new DevExpress.Utils.PointFloat(615F, 85.00001F);
            this.lblCLASS_WAY.Name = "lblCLASS_WAY";
            this.lblCLASS_WAY.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblCLASS_WAY.SizeF = new System.Drawing.SizeF(100F, 65F);
            this.lblCLASS_WAY.StylePriority.UseBorders = false;
            this.lblCLASS_WAY.StylePriority.UseTextAlignment = false;
            this.lblCLASS_WAY.Text = "机器内部消毒方法";
            this.lblCLASS_WAY.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.LocationFloat = new DevExpress.Utils.PointFloat(433.7916F, 10F);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblTitle.SizeF = new System.Drawing.SizeF(281.2084F, 35F);
            this.lblTitle.StylePriority.UseBorders = false;
            this.lblTitle.StylePriority.UseFont = false;
            this.lblTitle.StylePriority.UseTextAlignment = false;
            this.lblTitle.Text = "血透机运行记录报表";
            this.lblTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblHead
            // 
            this.lblHead.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.lblHead.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50F);
            this.lblHead.Name = "lblHead";
            this.lblHead.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblHead.SizeF = new System.Drawing.SizeF(600F, 35F);
            this.lblHead.StylePriority.UseBorders = false;
            this.lblHead.StylePriority.UseTextAlignment = false;
            this.lblHead.Text = "透析室：{0}   床位：{1}   品牌：{2}   编号：{3}   透析方式：{4}";
            this.lblHead.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblINNER_DEGASSING
            // 
            this.lblINNER_DEGASSING.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblINNER_DEGASSING.LocationFloat = new DevExpress.Utils.PointFloat(550F, 85.00002F);
            this.lblINNER_DEGASSING.Name = "lblINNER_DEGASSING";
            this.lblINNER_DEGASSING.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblINNER_DEGASSING.SizeF = new System.Drawing.SizeF(65F, 65F);
            this.lblINNER_DEGASSING.StylePriority.UseBorders = false;
            this.lblINNER_DEGASSING.StylePriority.UseTextAlignment = false;
            this.lblINNER_DEGASSING.Text = "机器内部管路消毒";
            this.lblINNER_DEGASSING.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblUSERTIME
            // 
            this.lblUSERTIME.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblUSERTIME.LocationFloat = new DevExpress.Utils.PointFloat(485F, 85.00002F);
            this.lblUSERTIME.Name = "lblUSERTIME";
            this.lblUSERTIME.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblUSERTIME.SizeF = new System.Drawing.SizeF(65F, 65F);
            this.lblUSERTIME.StylePriority.UseBorders = false;
            this.lblUSERTIME.StylePriority.UseTextAlignment = false;
            this.lblUSERTIME.Text = "使用时间（h）";
            this.lblUSERTIME.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblWORKING
            // 
            this.lblWORKING.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblWORKING.LocationFloat = new DevExpress.Utils.PointFloat(405F, 85F);
            this.lblWORKING.Name = "lblWORKING";
            this.lblWORKING.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblWORKING.SizeF = new System.Drawing.SizeF(80F, 65F);
            this.lblWORKING.StylePriority.UseBorders = false;
            this.lblWORKING.StylePriority.UseTextAlignment = false;
            this.lblWORKING.Text = "运转情况";
            this.lblWORKING.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblBANCI_ID
            // 
            this.lblBANCI_ID.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblBANCI_ID.LocationFloat = new DevExpress.Utils.PointFloat(100F, 85.00002F);
            this.lblBANCI_ID.Name = "lblBANCI_ID";
            this.lblBANCI_ID.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblBANCI_ID.SizeF = new System.Drawing.SizeF(65F, 65F);
            this.lblBANCI_ID.StylePriority.UseBorders = false;
            this.lblBANCI_ID.StylePriority.UseTextAlignment = false;
            this.lblBANCI_ID.Text = "班次";
            this.lblBANCI_ID.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblDEGASSING
            // 
            this.lblDEGASSING.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblDEGASSING.LocationFloat = new DevExpress.Utils.PointFloat(325F, 85.00002F);
            this.lblDEGASSING.Name = "lblDEGASSING";
            this.lblDEGASSING.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblDEGASSING.SizeF = new System.Drawing.SizeF(80F, 65F);
            this.lblDEGASSING.StylePriority.UseBorders = false;
            this.lblDEGASSING.StylePriority.UseTextAlignment = false;
            this.lblDEGASSING.Text = "消毒功能";
            this.lblDEGASSING.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblMACHINE_ALARM
            // 
            this.lblMACHINE_ALARM.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblMACHINE_ALARM.LocationFloat = new DevExpress.Utils.PointFloat(245F, 85F);
            this.lblMACHINE_ALARM.Name = "lblMACHINE_ALARM";
            this.lblMACHINE_ALARM.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblMACHINE_ALARM.SizeF = new System.Drawing.SizeF(80F, 65F);
            this.lblMACHINE_ALARM.StylePriority.UseBorders = false;
            this.lblMACHINE_ALARM.StylePriority.UseTextAlignment = false;
            this.lblMACHINE_ALARM.Text = "报警情况";
            this.lblMACHINE_ALARM.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblMACHINE_CHECK
            // 
            this.lblMACHINE_CHECK.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblMACHINE_CHECK.LocationFloat = new DevExpress.Utils.PointFloat(165F, 85F);
            this.lblMACHINE_CHECK.Name = "lblMACHINE_CHECK";
            this.lblMACHINE_CHECK.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblMACHINE_CHECK.SizeF = new System.Drawing.SizeF(80F, 65F);
            this.lblMACHINE_CHECK.StylePriority.UseBorders = false;
            this.lblMACHINE_CHECK.StylePriority.UseTextAlignment = false;
            this.lblMACHINE_CHECK.Text = "监测情况";
            this.lblMACHINE_CHECK.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblUSEDATE
            // 
            this.lblUSEDATE.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblUSEDATE.LocationFloat = new DevExpress.Utils.PointFloat(0F, 85F);
            this.lblUSEDATE.Name = "lblUSEDATE";
            this.lblUSEDATE.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblUSEDATE.SizeF = new System.Drawing.SizeF(100F, 65F);
            this.lblUSEDATE.StylePriority.UseBorders = false;
            this.lblUSEDATE.StylePriority.UseTextAlignment = false;
            this.lblUSEDATE.Text = "使用日期";
            this.lblUSEDATE.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
            // MachineUserReportNew
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.DataSource = this.machineModel1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 150, 100);
            this.PageHeight = 827;
            this.PageWidth = 1169;
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
        private DevExpress.XtraReports.UI.XRLabel lblUSEDATE;
        private DevExpress.XtraReports.UI.XRLabel lblMACHINE_CHECK;
        private DevExpress.XtraReports.UI.XRLabel lblMACHINE_ALARM;
        private DevExpress.XtraReports.UI.XRLabel lblDEGASSING;
        private DevExpress.XtraReports.UI.XRLabel lblBANCI_ID;
        private DevExpress.XtraReports.UI.XRLabel lblWORKING;
        private DevExpress.XtraReports.UI.XRLabel lblUSERTIME;
        private DevExpress.XtraReports.UI.XRLabel lblINNER_DEGASSING;
        private Model.MachineModel machineModel1;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell7;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell8;
        private DevExpress.XtraReports.UI.XRLabel lblHead;
        private DevExpress.XtraReports.UI.XRLabel lblTitle;
        private DevExpress.XtraReports.UI.XRLabel lblCLASS_WAY;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell9;
        private DevExpress.XtraReports.UI.XRLabel lblOUTER_DEGASSING;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell10;
        private DevExpress.XtraReports.UI.XRLabel lblDAY_WAY;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell11;
        private DevExpress.XtraReports.UI.XRLabel lblSIGN_NAME;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell12;
        private DevExpress.XtraReports.UI.XRLabel lblDEALWITH;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell13;
    }
}
