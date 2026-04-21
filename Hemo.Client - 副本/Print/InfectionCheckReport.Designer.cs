namespace Hemo.Client.Print
{
    partial class InfectionCheckReport
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
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.lblHead = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblNegative = new DevExpress.XtraReports.UI.XRLabel();
            this.lblHBsAg_Positive = new DevExpress.XtraReports.UI.XRLabel();
            this.lblHBeAg_Positive = new DevExpress.XtraReports.UI.XRLabel();
            this.lblAnti_HCV_Positive = new DevExpress.XtraReports.UI.XRLabel();
            this.lblHIV_Positive = new DevExpress.XtraReports.UI.XRLabel();
            this.lblPositive = new DevExpress.XtraReports.UI.XRLabel();
            this.lblAnti_TP_Positive = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblAnti_TP_Positive,
            this.lblPositive,
            this.lblHIV_Positive,
            this.lblAnti_HCV_Positive,
            this.lblHBeAg_Positive,
            this.lblHBsAg_Positive,
            this.lblNegative,
            this.lblDate,
            this.lblHead});
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblHead
            // 
            this.lblHead.BackColor = System.Drawing.Color.Transparent;
            this.lblHead.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.lblHead.ForeColor = System.Drawing.Color.Black;
            this.lblHead.LocationFloat = new DevExpress.Utils.PointFloat(180F, 10F);
            this.lblHead.Name = "lblHead";
            this.lblHead.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblHead.SizeF = new System.Drawing.SizeF(528.8334F, 35.75001F);
            this.lblHead.StylePriority.UseFont = false;
            this.lblHead.Text = "2014年度院感检查统计";
            this.lblHead.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblDate
            // 
            this.lblDate.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblDate.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lblDate.LocationFloat = new DevExpress.Utils.PointFloat(0F, 77F);
            this.lblDate.Name = "lblDate";
            this.lblDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblDate.SizeF = new System.Drawing.SizeF(80F, 23F);
            this.lblDate.StylePriority.UseBorders = false;
            this.lblDate.StylePriority.UseFont = false;
            this.lblDate.StylePriority.UseTextAlignment = false;
            this.lblDate.Text = "日期";
            this.lblDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblNegative
            // 
            this.lblNegative.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblNegative.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lblNegative.LocationFloat = new DevExpress.Utils.PointFloat(80F, 77F);
            this.lblNegative.Name = "lblNegative";
            this.lblNegative.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblNegative.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.lblNegative.StylePriority.UseBorders = false;
            this.lblNegative.StylePriority.UseFont = false;
            this.lblNegative.StylePriority.UseTextAlignment = false;
            this.lblNegative.Text = "全阴患者例数";
            this.lblNegative.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblHBsAg_Positive
            // 
            this.lblHBsAg_Positive.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblHBsAg_Positive.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lblHBsAg_Positive.LocationFloat = new DevExpress.Utils.PointFloat(180F, 77F);
            this.lblHBsAg_Positive.Name = "lblHBsAg_Positive";
            this.lblHBsAg_Positive.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblHBsAg_Positive.SizeF = new System.Drawing.SizeF(160F, 23F);
            this.lblHBsAg_Positive.StylePriority.UseBorders = false;
            this.lblHBsAg_Positive.StylePriority.UseFont = false;
            this.lblHBsAg_Positive.StylePriority.UseTextAlignment = false;
            this.lblHBsAg_Positive.Text = "乙肝表面抗原阳性例数";
            this.lblHBsAg_Positive.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblHBeAg_Positive
            // 
            this.lblHBeAg_Positive.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblHBeAg_Positive.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lblHBeAg_Positive.LocationFloat = new DevExpress.Utils.PointFloat(340F, 77F);
            this.lblHBeAg_Positive.Name = "lblHBeAg_Positive";
            this.lblHBeAg_Positive.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblHBeAg_Positive.SizeF = new System.Drawing.SizeF(140F, 23F);
            this.lblHBeAg_Positive.StylePriority.UseBorders = false;
            this.lblHBeAg_Positive.StylePriority.UseFont = false;
            this.lblHBeAg_Positive.StylePriority.UseTextAlignment = false;
            this.lblHBeAg_Positive.Text = "乙肝E抗原阳性例数";
            this.lblHBeAg_Positive.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblAnti_HCV_Positive
            // 
            this.lblAnti_HCV_Positive.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblAnti_HCV_Positive.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lblAnti_HCV_Positive.LocationFloat = new DevExpress.Utils.PointFloat(480F, 77F);
            this.lblAnti_HCV_Positive.Name = "lblAnti_HCV_Positive";
            this.lblAnti_HCV_Positive.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblAnti_HCV_Positive.SizeF = new System.Drawing.SizeF(160F, 23F);
            this.lblAnti_HCV_Positive.StylePriority.UseBorders = false;
            this.lblAnti_HCV_Positive.StylePriority.UseFont = false;
            this.lblAnti_HCV_Positive.StylePriority.UseTextAlignment = false;
            this.lblAnti_HCV_Positive.Text = "丙肝病毒抗体阳性例数";
            this.lblAnti_HCV_Positive.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblHIV_Positive
            // 
            this.lblHIV_Positive.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblHIV_Positive.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lblHIV_Positive.LocationFloat = new DevExpress.Utils.PointFloat(770F, 77F);
            this.lblHIV_Positive.Name = "lblHIV_Positive";
            this.lblHIV_Positive.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblHIV_Positive.SizeF = new System.Drawing.SizeF(90F, 23F);
            this.lblHIV_Positive.StylePriority.UseBorders = false;
            this.lblHIV_Positive.StylePriority.UseFont = false;
            this.lblHIV_Positive.StylePriority.UseTextAlignment = false;
            this.lblHIV_Positive.Text = "HIV阳性例数";
            this.lblHIV_Positive.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblPositive
            // 
            this.lblPositive.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblPositive.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lblPositive.LocationFloat = new DevExpress.Utils.PointFloat(860F, 77F);
            this.lblPositive.Name = "lblPositive";
            this.lblPositive.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblPositive.SizeF = new System.Drawing.SizeF(80F, 23F);
            this.lblPositive.StylePriority.UseBorders = false;
            this.lblPositive.StylePriority.UseFont = false;
            this.lblPositive.StylePriority.UseTextAlignment = false;
            this.lblPositive.Text = "转阳例数";
            this.lblPositive.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblAnti_TP_Positive
            // 
            this.lblAnti_TP_Positive.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblAnti_TP_Positive.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lblAnti_TP_Positive.LocationFloat = new DevExpress.Utils.PointFloat(640F, 77F);
            this.lblAnti_TP_Positive.Name = "lblAnti_TP_Positive";
            this.lblAnti_TP_Positive.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblAnti_TP_Positive.SizeF = new System.Drawing.SizeF(130F, 23F);
            this.lblAnti_TP_Positive.StylePriority.UseBorders = false;
            this.lblAnti_TP_Positive.StylePriority.UseFont = false;
            this.lblAnti_TP_Positive.StylePriority.UseTextAlignment = false;
            this.lblAnti_TP_Positive.Text = "梅毒抗体阳性例数";
            this.lblAnti_TP_Positive.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // InfectionCheckReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(50, 178, 100, 100);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "10.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRLabel lblHead;
        private DevExpress.XtraReports.UI.XRLabel lblDate;
        private DevExpress.XtraReports.UI.XRLabel lblNegative;
        private DevExpress.XtraReports.UI.XRLabel lblHBsAg_Positive;
        private DevExpress.XtraReports.UI.XRLabel lblHBeAg_Positive;
        private DevExpress.XtraReports.UI.XRLabel lblAnti_HCV_Positive;
        private DevExpress.XtraReports.UI.XRLabel lblHIV_Positive;
        private DevExpress.XtraReports.UI.XRLabel lblPositive;
        private DevExpress.XtraReports.UI.XRLabel lblAnti_TP_Positive;
    }
}
