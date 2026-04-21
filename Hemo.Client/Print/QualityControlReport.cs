/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：质量管理基础数据统计报表
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
    public partial class QualityControlReport : XtraReport
    {
        #region 构造函数

        public QualityControlReport(DataTable dtInfo, DataTable dtResult, string title)
        {
            InitializeComponent();
            this.Detail.Controls.Clear();
            this.lblHead.Text = title;
            if (dtInfo != null)
            {
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    if (dtInfo.Rows[i]["JOB"].ToString() == "008")
                    {
                        this.lblMachineCount.Text = dtInfo.Rows[i]["DCOUNT"].ToString();
                    }
                    else if (dtInfo.Rows[i]["JOB"].ToString() == "009")
                    {
                        this.lblSpecialistsCount.Text = dtInfo.Rows[i]["DCOUNT"].ToString();
                    }
                    else if (dtInfo.Rows[i]["JOB"].ToString() == "010")
                    {
                        this.lblParamedicCount.Text = dtInfo.Rows[i]["DCOUNT"].ToString();
                    }
                }

            }



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
            table.SizeF = new SizeF(1150F, 25F);
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

            XRTableCell cellHemo = new XRTableCell();
            cellHemo.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellHemo.Name = "xrTableCell1";
            cellHemo.Weight = 0.75D;
            cellHemo.StylePriority.UseBorders = false;
            cellHemo.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "HEMO_COUNT") });

            XRTableCell cellHD = new XRTableCell();
            cellHD.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellHD.Name = "xrTableCell2";
            cellHD.Weight = 0.75D;
            cellHD.StylePriority.UseBorders = false;
            cellHD.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "HD_COUNT") });

            XRTableCell cellHDF = new XRTableCell();
            cellHDF.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellHDF.Name = "xrTableCell3";
            cellHDF.Weight = 0.75D;
            cellHDF.StylePriority.UseBorders = false;
            cellHDF.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "HDF_COUNT") });

            XRTableCell cellHF = new XRTableCell();
            cellHF.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellHF.Name = "xrTableCell4";
            cellHF.Weight = 0.75D;
            cellHF.StylePriority.UseBorders = false;
            cellHF.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "HF_COUNT") });

            XRTableCell cellHP = new XRTableCell();
            cellHP.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellHP.Name = "xrTableCell5";
            cellHP.Weight = 0.75D;
            cellHP.StylePriority.UseBorders = false;
            cellHP.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "HP_COUNT") });

            XRTableCell cellHD_HP = new XRTableCell();
            cellHD_HP.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellHD_HP.Name = "xrTableCell6";
            cellHD_HP.Weight = 0.75D;
            cellHD_HP.StylePriority.UseBorders = false;
            cellHD_HP.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "HD_HP_COUNT") });

            XRTableCell cellDeath = new XRTableCell();
            cellDeath.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellDeath.Name = "xrTableCell7";
            cellDeath.Weight = 0.75D;
            cellDeath.StylePriority.UseBorders = false;
            cellDeath.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "DEATH_COUNT") });

            XRTableCell cellDeathRate = new XRTableCell();
            cellDeathRate.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellDeathRate.Name = "xrTableCell8";
            cellDeathRate.Weight = 0.75D;
            cellDeathRate.StylePriority.UseBorders = false;
            cellDeathRate.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "DEATH_RATE") });

            XRTableCell cellSevereComplication = new XRTableCell();
            cellSevereComplication.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellSevereComplication.Name = "xrTableCell9";
            cellSevereComplication.Weight = 0.75D;
            cellSevereComplication.StylePriority.UseBorders = false;
            cellSevereComplication.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "SEVERE_COMPLICATION") });

            XRTableCell cellHBSAG = new XRTableCell();
            cellHBSAG.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellHBSAG.Name = "xrTableCell10";
            cellHBSAG.Weight = 0.75D;
            cellHBSAG.StylePriority.UseBorders = false;
            cellHBSAG.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "HBSAG_POSITIVE") });

            XRTableCell cellHBEAG = new XRTableCell();
            cellHBEAG.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellHBEAG.Name = "xrTableCell11";
            cellHBEAG.Weight = 0.75D;
            cellHBEAG.StylePriority.UseBorders = false;
            cellHBEAG.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "HBEAG_POSITIVE") });

            XRTableCell cellANTI_HCV = new XRTableCell();
            cellANTI_HCV.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellANTI_HCV.Name = "xrTableCell12";
            cellANTI_HCV.Weight = 0.75D;
            cellANTI_HCV.StylePriority.UseBorders = false;
            cellANTI_HCV.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "ANTI_HCV_POSITIVE") });

            XRTableCell cellPeritonealDialysis = new XRTableCell();
            cellPeritonealDialysis.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellPeritonealDialysis.Name = "xrTableCell13";
            cellPeritonealDialysis.Weight = 0.75D;
            cellPeritonealDialysis.StylePriority.UseBorders = false;
            cellPeritonealDialysis.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "PERITONEAL_DIALYSIS") });

            XRTableCell cellRenalTransplant = new XRTableCell();
            cellRenalTransplant.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellRenalTransplant.Name = "xrTableCell14";
            cellRenalTransplant.Weight = 0.75D;
            cellRenalTransplant.StylePriority.UseBorders = false;
            cellRenalTransplant.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "RENAL_TRANSPLANT") });

            row.Cells.AddRange(new XRTableCell[] { cellDate, cellHemo, cellHD, cellHDF, cellHF, cellHP, cellHD_HP, cellDeath, cellDeathRate, cellSevereComplication, cellHBSAG, cellHBEAG, cellANTI_HCV, cellPeritonealDialysis, cellRenalTransplant });
            row.Cells[0].WidthF = 101F;
            row.Cells[1].WidthF = 70F;
            row.Cells[2].WidthF = 70F;
            row.Cells[3].WidthF = 70F;
            row.Cells[4].WidthF = 70F;
            row.Cells[5].WidthF = 70F;
            row.Cells[6].WidthF = 80F;
            row.Cells[7].WidthF = 70F;
            row.Cells[8].WidthF = 70F;
            row.Cells[9].WidthF = 80F;
            row.Cells[10].WidthF = 80F;
            row.Cells[11].WidthF = 80F;
            row.Cells[12].WidthF = 80F;
            row.Cells[13].WidthF = 80F;
            row.Cells[14].WidthF = 79F;
            this.Detail.Controls.AddRange(new XRControl[] { table });
        }

        #endregion
    }
}
