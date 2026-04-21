namespace Hemo.Client.Print {
    partial class HemoHealthEducationReport {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.lb_patientID = new DevExpress.XtraReports.UI.XRLabel();
            this.lb_hemoid = new DevExpress.XtraReports.UI.XRLabel();
            this.lb_sex = new DevExpress.XtraReports.UI.XRLabel();
            this.lb_Name = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.lb_patientID,
            this.lb_hemoid,
            this.lb_sex,
            this.lb_Name});
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
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(50F, 12.5F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(565.6249F, 34.375F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "";
            // 
            // lb_patientID
            // 
            this.lb_patientID.LocationFloat = new DevExpress.Utils.PointFloat(600F, 62.5F);
            this.lb_patientID.Name = "lb_patientID";
            this.lb_patientID.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lb_patientID.SizeF = new System.Drawing.SizeF(180.9585F, 23F);
            this.lb_patientID.StylePriority.UseTextAlignment = false;
            this.lb_patientID.Text = "病人编号：";
            this.lb_patientID.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lb_hemoid
            // 
            this.lb_hemoid.LocationFloat = new DevExpress.Utils.PointFloat(337.5F, 62.5F);
            this.lb_hemoid.Name = "lb_hemoid";
            this.lb_hemoid.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lb_hemoid.SizeF = new System.Drawing.SizeF(200F, 23F);
            this.lb_hemoid.StylePriority.UseTextAlignment = false;
            this.lb_hemoid.Text = "病案号：";
            this.lb_hemoid.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lb_sex
            // 
            this.lb_sex.LocationFloat = new DevExpress.Utils.PointFloat(122.9166F, 62.5F);
            this.lb_sex.Name = "lb_sex";
            this.lb_sex.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lb_sex.SizeF = new System.Drawing.SizeF(160.4167F, 23F);
            this.lb_sex.StylePriority.UseTextAlignment = false;
            this.lb_sex.Text = "性别：";
            this.lb_sex.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lb_Name
            // 
            this.lb_Name.LocationFloat = new DevExpress.Utils.PointFloat(0F, 62.5F);
            this.lb_Name.Name = "lb_Name";
            this.lb_Name.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lb_Name.SizeF = new System.Drawing.SizeF(62.5F, 23F);
            this.lb_Name.StylePriority.UseTextAlignment = false;
            this.lb_Name.Text = "姓名：";
            this.lb_Name.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // HemoHealthEducationReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Version = "10.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel lb_patientID;
        private DevExpress.XtraReports.UI.XRLabel lb_hemoid;
        private DevExpress.XtraReports.UI.XRLabel lb_sex;
        private DevExpress.XtraReports.UI.XRLabel lb_Name;
    }
}
