/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：维持性血透患者质量监测指标统计报表
// 创建时间：2016-07-16
// 创建者：吕志强
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Hemo.Client.Print
{
    public partial class QualityMonitorIndicatorReport : XtraReport
    {
        #region 构造函数

        public QualityMonitorIndicatorReport(DataTable dtResult, string title)
        {
            InitializeComponent();
            this.lblHead.Text = title;
            CreateTable();
            this.DetailReport1.DataMember = (dtResult != null) ? dtResult.TableName : string.Empty;
            this.DetailReport1.DataSource = dtResult;
            this.DetailReport2.DataMember = (dtResult != null) ? dtResult.TableName : string.Empty;
            this.DetailReport2.DataSource = dtResult;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 创建表
        /// </summary>
        private void CreateTable()
        {
            XRTable table1 = this.xrTable1;
            table1.Borders = (DevExpress.XtraPrinting.BorderSide)DevExpress.XtraPrinting.BorderSide.All;
            table1.BorderWidth = 1;
            table1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            table1.Name = "xrTable1";
            table1.SizeF = new SizeF(1120F, 25F);
            table1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular);
            table1.StylePriority.UseBorders = false;
            table1.StylePriority.UseTextAlignment = false;
            table1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            table1.Rows.Clear();

            XRTableRow row1 = new XRTableRow();
            row1.Name = "xrTableRow1";
            row1.Weight = 1D;

            table1.Rows.AddRange(new XRTableRow[] { row1 });

            XRTableCell cellDate = new XRTableCell();
            cellDate.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellDate.Name = "xrTableCell0";
            cellDate.Weight = 0.75D;
            cellDate.StylePriority.UseBorders = false;
            cellDate.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "DATE_MONTH") });

            XRTableCell cellUreaRemove = new XRTableCell();
            cellUreaRemove.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellUreaRemove.Name = "xrTableCell1";
            cellUreaRemove.Weight = 0.75D;
            cellUreaRemove.StylePriority.UseBorders = false;
            cellUreaRemove.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "UREA_REMOVE") });

            XRTableCell cellRenalAnemia = new XRTableCell();
            cellRenalAnemia.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellRenalAnemia.Name = "xrTableCell2";
            cellRenalAnemia.Weight = 0.75D;
            cellRenalAnemia.StylePriority.UseBorders = false;
            cellRenalAnemia.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "RENAL_ANEMIA") });

            XRTableCell cellCa_P_Metabolism = new XRTableCell();
            cellCa_P_Metabolism.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellCa_P_Metabolism.Name = "xrTableCell3";
            cellCa_P_Metabolism.Weight = 0.75D;
            cellCa_P_Metabolism.StylePriority.UseBorders = false;
            cellCa_P_Metabolism.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "CA_P_METABOLISM") });

            XRTableCell cellVenousCatheter = new XRTableCell();
            cellVenousCatheter.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellVenousCatheter.Name = "xrTableCell4";
            cellVenousCatheter.Weight = 0.75D;
            cellVenousCatheter.StylePriority.UseBorders = false;
            cellVenousCatheter.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "VENOUS_CATHETER") });

            XRTableCell cellAutologousFistula = new XRTableCell();
            cellAutologousFistula.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellAutologousFistula.Name = "xrTableCell5";
            cellAutologousFistula.Weight = 0.75D;
            cellAutologousFistula.StylePriority.UseBorders = false;
            cellAutologousFistula.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "AUTOLOGOUS_FISTULA") });

            XRTableCell cellTempVenousCatheter = new XRTableCell();
            cellTempVenousCatheter.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellTempVenousCatheter.Name = "xrTableCell6";
            cellTempVenousCatheter.Weight = 0.75D;
            cellTempVenousCatheter.StylePriority.UseBorders = false;
            cellTempVenousCatheter.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "TEMP_VENOUS_CATHETER") });

            XRTableCell cellArtificialVessel = new XRTableCell();
            cellArtificialVessel.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellArtificialVessel.Name = "xrTableCell7";
            cellArtificialVessel.Weight = 0.75D;
            cellArtificialVessel.StylePriority.UseBorders = false;
            cellArtificialVessel.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "ARTIFICIAL_VESSEL") });

            XRTableCell cellDoubleVein = new XRTableCell();
            cellDoubleVein.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellDoubleVein.Name = "xrTableCell8";
            cellDoubleVein.Weight = 0.75D;
            cellDoubleVein.StylePriority.UseBorders = false;
            cellDoubleVein.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "DOUBLE_VEIN") });

            XRTableCell cellHighAvf = new XRTableCell();
            cellHighAvf.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellHighAvf.Name = "xrTableCell9";
            cellHighAvf.Weight = 0.75D;
            cellHighAvf.StylePriority.UseBorders = false;
            cellHighAvf.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "HIGH_AVF") });

            XRTableCell cellJugularVenousCatheter = new XRTableCell();
            cellJugularVenousCatheter.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellJugularVenousCatheter.Name = "xrTableCell10";
            cellJugularVenousCatheter.Weight = 0.75D;
            cellJugularVenousCatheter.StylePriority.UseBorders = false;
            cellJugularVenousCatheter.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "JUGULAR_VENOUS_CATHETER") });

            XRTableCell cellSubclavianVenousCatheter = new XRTableCell();
            cellSubclavianVenousCatheter.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellSubclavianVenousCatheter.Name = "xrTableCell11";
            cellSubclavianVenousCatheter.Weight = 0.75D;
            cellSubclavianVenousCatheter.StylePriority.UseBorders = false;
            cellSubclavianVenousCatheter.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "SUBCLAVIAN_VENOUS_CATHETER") });

            XRTableCell cellFemoralVenousCatheter = new XRTableCell();
            cellFemoralVenousCatheter.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellFemoralVenousCatheter.Name = "xrTableCell12";
            cellFemoralVenousCatheter.Weight = 0.75D;
            cellFemoralVenousCatheter.StylePriority.UseBorders = false;
            cellFemoralVenousCatheter.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "FEMORAL_VENOUS_CATHETER") });

            row1.Cells.AddRange(new XRTableCell[] { cellDate, cellUreaRemove, cellRenalAnemia, cellCa_P_Metabolism, cellVenousCatheter, cellAutologousFistula, cellTempVenousCatheter, cellArtificialVessel, cellDoubleVein, cellHighAvf, cellJugularVenousCatheter, cellSubclavianVenousCatheter, cellFemoralVenousCatheter });
            row1.Cells[0].WidthF = 101F;
            row1.Cells[1].WidthF = 85F;
            row1.Cells[2].WidthF = 85F;
            row1.Cells[3].WidthF = 85F;
            row1.Cells[4].WidthF = 85F;
            row1.Cells[5].WidthF = 85F;
            row1.Cells[6].WidthF = 85F;
            row1.Cells[7].WidthF = 85F;
            row1.Cells[8].WidthF = 85F;
            row1.Cells[9].WidthF = 85F;
            row1.Cells[10].WidthF = 85F;
            row1.Cells[11].WidthF = 85F;
            row1.Cells[12].WidthF = 84F;

            XRTable table2 = this.xrTable2;
            table2.Borders = (DevExpress.XtraPrinting.BorderSide)DevExpress.XtraPrinting.BorderSide.All;
            table2.BorderWidth = 1;
            table2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            table2.Name = "xrTable2";
            table2.SizeF = new SizeF(1120F, 25F);
            table2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular);
            table2.StylePriority.UseBorders = false;
            table2.StylePriority.UseTextAlignment = false;
            table2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            table2.Rows.Clear();

            XRTableRow row2 = new XRTableRow();
            row2.Name = "xrTableRow2";
            row2.Weight = 1D;

            table2.Rows.AddRange(new XRTableRow[] { row2 });

            XRTableCell cellDate2 = new XRTableCell();
            cellDate2.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellDate2.Name = "xrTableCell13";
            cellDate2.Weight = 0.75D;
            cellDate2.StylePriority.UseBorders = false;
            cellDate2.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "DATE_MONTH") });

            XRTableCell cellSecondaryShpt = new XRTableCell();
            cellSecondaryShpt.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellSecondaryShpt.Name = "xrTableCell14";
            cellSecondaryShpt.Weight = 0.75D;
            cellSecondaryShpt.StylePriority.UseBorders = false;
            cellSecondaryShpt.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "SECONDARY_SHPT") });

            XRTableCell cellPressureControl = new XRTableCell();
            cellPressureControl.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellPressureControl.Name = "xrTableCell15";
            cellPressureControl.Weight = 0.75D;
            cellPressureControl.StylePriority.UseBorders = false;
            cellPressureControl.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "PRESSURE_CONTROL") });

            XRTableCell cellTimeLess8 = new XRTableCell();
            cellTimeLess8.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellTimeLess8.Name = "xrTableCell16";
            cellTimeLess8.Weight = 0.75D;
            cellTimeLess8.StylePriority.UseBorders = false;
            cellTimeLess8.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "TIME_LESS_8") });

            XRTableCell cellTime8_9 = new XRTableCell();
            cellTime8_9.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellTime8_9.Name = "xrTableCell17";
            cellTime8_9.Weight = 0.75D;
            cellTime8_9.StylePriority.UseBorders = false;
            cellTime8_9.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "TIME_8_9") });

            XRTableCell cellTime9_10 = new XRTableCell();
            cellTime9_10.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellTime9_10.Name = "xrTableCell18";
            cellTime9_10.Weight = 0.75D;
            cellTime9_10.StylePriority.UseBorders = false;
            cellTime9_10.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "TIME_9_10") });

            XRTableCell cellTime10_11 = new XRTableCell();
            cellTime10_11.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellTime10_11.Name = "xrTableCell19";
            cellTime10_11.Weight = 0.75D;
            cellTime10_11.StylePriority.UseBorders = false;
            cellTime10_11.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "TIME_10_11") });

            XRTableCell cellTime11_12 = new XRTableCell();
            cellTime11_12.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellTime11_12.Name = "xrTableCell20";
            cellTime11_12.Weight = 0.75D;
            cellTime11_12.StylePriority.UseBorders = false;
            cellTime11_12.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "TIME_11_12") });

            XRTableCell cellTimeMore12 = new XRTableCell();
            cellTimeMore12.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellTimeMore12.Name = "xrTableCell21";
            cellTimeMore12.Weight = 0.75D;
            cellTimeMore12.StylePriority.UseBorders = false;
            cellTimeMore12.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "TIME_MORE_12") });

            XRTableCell cellComfort = new XRTableCell();
            cellComfort.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellComfort.Name = "xrTableCell22";
            cellComfort.Weight = 0.75D;
            cellComfort.StylePriority.UseBorders = false;
            cellComfort.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "COMFORT") });

            XRTableCell cellMildDiscomfort = new XRTableCell();
            cellMildDiscomfort.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellMildDiscomfort.Name = "xrTableCell23";
            cellMildDiscomfort.Weight = 0.75D;
            cellMildDiscomfort.StylePriority.UseBorders = false;
            cellMildDiscomfort.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "MILD_DISCOMFORT") });

            XRTableCell cellSevereDiscomfort = new XRTableCell();
            cellSevereDiscomfort.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellSevereDiscomfort.Name = "xrTableCell24";
            cellSevereDiscomfort.Weight = 0.75D;
            cellSevereDiscomfort.StylePriority.UseBorders = false;
            cellSevereDiscomfort.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "SEVERE_DISCOMFORT") });

            XRTableCell cellPeritonealDialysis = new XRTableCell();
            cellPeritonealDialysis.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellPeritonealDialysis.Name = "xrTableCell25";
            cellPeritonealDialysis.Weight = 0.75D;
            cellPeritonealDialysis.StylePriority.UseBorders = false;
            cellPeritonealDialysis.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "PERITONEAL_DIALYSIS") });

            row2.Cells.AddRange(new XRTableCell[] { cellDate2, cellSecondaryShpt, cellPressureControl, cellTimeLess8, cellTime8_9, cellTime9_10, cellTime10_11, cellTime11_12, cellTimeMore12, cellComfort, cellMildDiscomfort, cellSevereDiscomfort, cellPeritonealDialysis });

            row2.Cells[0].WidthF = 101F;
            row2.Cells[1].WidthF = 85F;
            row2.Cells[2].WidthF = 85F;
            row2.Cells[3].WidthF = 85F;
            row2.Cells[4].WidthF = 85F;
            row2.Cells[5].WidthF = 85F;
            row2.Cells[6].WidthF = 85F;
            row2.Cells[7].WidthF = 85F;
            row2.Cells[8].WidthF = 85F;
            row2.Cells[9].WidthF = 85F;
            row2.Cells[10].WidthF = 85F;
            row2.Cells[11].WidthF = 85F;
            row2.Cells[12].WidthF = 84F;
        }

        #endregion
    }
}
