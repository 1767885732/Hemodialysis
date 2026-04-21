/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:2014年度院感检查统计报表
 * 创建标识:吕志强-2015年6月20日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Hemo.Client.Print
{
    public partial class InfectionCheckReport : XtraReport
    {
        #region 构造函数

        public InfectionCheckReport(DataTable dtResult, string title)
        {
            InitializeComponent();
            this.Detail.Controls.Clear();
            this.lblHead.Text = title;
            CreateTable();
            this.DataSource = dtResult;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 创建表
        /// </summary>
        private void CreateTable()
        {
            XRTable table = new XRTable();
            table.Borders = (DevExpress.XtraPrinting.BorderSide)DevExpress.XtraPrinting.BorderSide.All;
            table.BorderWidth = 1;
            table.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            table.Name = "xrTable";
            table.SizeF = new SizeF(940F, 25F);
            table.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular);
            table.StylePriority.UseBorders = false;
            table.StylePriority.UseTextAlignment = false;
            table.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            XRTableRow row = new XRTableRow();
            row.Name = "xrTableRow";
            row.Weight = 1D;

            table.Rows.AddRange(new XRTableRow[] { row });

            XRTableCell cellDate = new XRTableCell();
            cellDate.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellDate.Name = "xrTableCell0";
            cellDate.Weight = 0.75D;
            cellDate.StylePriority.UseBorders = false;
            cellDate.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "DATE_MONTH") });

            XRTableCell cellNegative = new XRTableCell();
            cellNegative.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellNegative.Name = "xrTableCell1";
            cellNegative.Weight = 0.75D;
            cellNegative.StylePriority.UseBorders = false;
            cellNegative.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "NEGATIVE") });

            XRTableCell cellHBsAg_Positive = new XRTableCell();
            cellHBsAg_Positive.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellHBsAg_Positive.Name = "xrTableCell2";
            cellHBsAg_Positive.Weight = 0.75D;
            cellHBsAg_Positive.StylePriority.UseBorders = false;
            cellHBsAg_Positive.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "HBSAG_POSITIVE_DESC") });

            XRTableCell cellHBeAg_Positive = new XRTableCell();
            cellHBeAg_Positive.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellHBeAg_Positive.Name = "xrTableCell3";
            cellHBeAg_Positive.Weight = 0.75D;
            cellHBeAg_Positive.StylePriority.UseBorders = false;
            cellHBeAg_Positive.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "HBEAG_POSITIVE") });

            XRTableCell cellAnti_HCV_Positive = new XRTableCell();
            cellAnti_HCV_Positive.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellAnti_HCV_Positive.Name = "xrTableCell4";
            cellAnti_HCV_Positive.Weight = 0.75D;
            cellAnti_HCV_Positive.StylePriority.UseBorders = false;
            cellAnti_HCV_Positive.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "ANTI_HCV_POSITIVE") });

            XRTableCell cellAnti_TP_Positive = new XRTableCell();
            cellAnti_TP_Positive.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellAnti_TP_Positive.Name = "xrTableCell5";
            cellAnti_TP_Positive.Weight = 0.75D;
            cellAnti_TP_Positive.StylePriority.UseBorders = false;
            cellAnti_TP_Positive.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "ANTI_TP_POSITIVE") });

            XRTableCell cellHIV_Positive = new XRTableCell();
            cellHIV_Positive.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellHIV_Positive.Name = "xrTableCell6";
            cellHIV_Positive.Weight = 0.75D;
            cellHIV_Positive.StylePriority.UseBorders = false;
            cellHIV_Positive.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "HIV_POSITIVE") });

            XRTableCell cellPositive = new XRTableCell();
            cellPositive.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellPositive.Name = "xrTableCell7";
            cellPositive.Weight = 0.75D;
            cellPositive.StylePriority.UseBorders = false;
            cellPositive.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "POSITIVE") });

            row.Cells.AddRange(new XRTableCell[] { cellDate, cellNegative, cellHBsAg_Positive, cellHBeAg_Positive, cellAnti_HCV_Positive, cellAnti_TP_Positive, cellHIV_Positive, cellPositive });
            row.Cells[0].WidthF = 80F;
            row.Cells[1].WidthF = 100F;
            row.Cells[2].WidthF = 160F;
            row.Cells[3].WidthF = 140F;
            row.Cells[4].WidthF = 160F;
            row.Cells[5].WidthF = 130F;
            row.Cells[6].WidthF = 90F;
            row.Cells[7].WidthF = 80F;
            this.Detail.Controls.AddRange(new XRControl[] { table });
        }

        #endregion
    }
}
