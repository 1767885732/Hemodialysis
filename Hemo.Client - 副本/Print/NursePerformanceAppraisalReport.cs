/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:血液净化科护士绩效考核报表
 * 创建标识:吕志强-2017年6月15日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Model;
using System.Data;
using Hemo.Utilities;
using System.Linq;

namespace Hemo.Client.Print
{
    public partial class NursePerformanceAppraisalReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 类变量

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        private IConfig configService = ServiceManager.Instance.ConfigService;

        private HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable dtAddItem = null;

        private HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable dtMinusItem = null;

        private HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable dtLeader = null;

        private HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable dtMember = null;

        private DateTime beginTime;

        private DateTime endTime;

        private string empNo = string.Empty;

        #endregion

        #region 构造函数

        public NursePerformanceAppraisalReport(HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable dtAddItem, HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable dtMinusItem, DateTime beginTime, DateTime endTime, string empNo)
        {
            InitializeComponent();
            this.dtAddItem = dtAddItem;
            this.dtMinusItem = dtMinusItem;
            this.beginTime = beginTime;
            this.endTime = endTime;
            this.empNo = empNo;
            this.xrLabel1.Text = this.xrLabel1.Text + this.beginTime.Date.Year + "年" + this.beginTime.Date.Month + "月" + this.beginTime.Day + "日" + "～" + this.endTime.Date.Year + "年" + this.endTime.Date.Month + "月" + this.endTime.Day + "日";

            CreateTable();
            BindDataSource();
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
            table1.SizeF = new SizeF(1160F, 25F);
            table1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular);
            table1.StylePriority.UseBorders = false;
            table1.StylePriority.UseTextAlignment = false;
            table1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            table1.Rows.Clear();

            XRTableRow row1 = new XRTableRow();
            row1.Name = "xrTableRow1";
            row1.Weight = 1D;

            table1.Rows.AddRange(new XRTableRow[] { row1 });

            XRTableCell cell1 = new XRTableCell();
            cell1.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell1.Name = "xrTableCell1";
            cell1.Weight = 0.75D;
            cell1.StylePriority.UseBorders = false;
            cell1.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "ORDER_NO") });

            XRTableCell cell2 = new XRTableCell();
            cell2.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell2.Name = "xrTableCell2";
            cell2.Weight = 0.75D;
            cell2.StylePriority.UseBorders = false;
            cell2.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "NAME") });

            XRTableCell cell3 = new XRTableCell();
            cell3.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell3.Name = "xrTableCell3";
            cell3.Weight = 0.75D;
            cell3.StylePriority.UseBorders = false;
            cell3.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "ITEM_CONTENT") });

            XRTableCell cell4 = new XRTableCell();
            cell4.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell4.Name = "xrTableCell4";
            cell4.Weight = 0.75D;
            cell4.StylePriority.UseBorders = false;
            cell4.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "ITEM_COUNT") });

            XRTableCell cell5 = new XRTableCell();
            cell5.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell5.Name = "xrTableCell5";
            cell5.Weight = 0.75D;
            cell5.StylePriority.UseBorders = false;
            cell5.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "SCORE") });

            XRTableCell cell6 = new XRTableCell();
            cell6.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell6.Name = "xrTableCell6";
            cell6.Weight = 0.75D;
            cell6.StylePriority.UseBorders = false;
            cell6.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "TOTAL_SCORE") });

            row1.Cells.AddRange(new XRTableCell[] { cell1, cell2, cell3, cell4, cell5, cell6 });
            row1.Cells[0].WidthF = 62F;
            row1.Cells[1].WidthF = 100F;
            row1.Cells[2].WidthF = 700F;
            row1.Cells[3].WidthF = 100F;
            row1.Cells[4].WidthF = 100F;
            row1.Cells[5].WidthF = 99F;

            XRTable table2 = this.xrTable2;
            table2.Borders = (DevExpress.XtraPrinting.BorderSide)DevExpress.XtraPrinting.BorderSide.All;
            table2.BorderWidth = 1;
            table2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            table2.Name = "xrTable2";
            table2.SizeF = new SizeF(1160F, 25F);
            table2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular);
            table2.StylePriority.UseBorders = false;
            table2.StylePriority.UseTextAlignment = false;
            table2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            table2.Rows.Clear();

            XRTableRow row2 = new XRTableRow();
            row2.Name = "xrTableRow2";
            row2.Weight = 1D;

            table2.Rows.AddRange(new XRTableRow[] { row2 });

            XRTableCell cell7 = new XRTableCell();
            cell7.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell7.Name = "xrTableCell7";
            cell7.Weight = 0.75D;
            cell7.StylePriority.UseBorders = false;
            cell7.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "ORDER_NO") });

            XRTableCell cell8 = new XRTableCell();
            cell8.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell8.Name = "xrTableCell8";
            cell8.Weight = 0.75D;
            cell8.StylePriority.UseBorders = false;
            cell8.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "NAME") });

            XRTableCell cell9 = new XRTableCell();
            cell9.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell9.Name = "xrTableCell9";
            cell9.Weight = 0.75D;
            cell9.StylePriority.UseBorders = false;
            cell9.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "ITEM_CONTENT") });

            XRTableCell cell10 = new XRTableCell();
            cell10.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell10.Name = "xrTableCell10";
            cell10.Weight = 0.75D;
            cell10.StylePriority.UseBorders = false;
            cell10.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "ITEM_COUNT") });

            XRTableCell cell11 = new XRTableCell();
            cell11.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell11.Name = "xrTableCell11";
            cell11.Weight = 0.75D;
            cell11.StylePriority.UseBorders = false;
            cell11.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "SCORE") });

            XRTableCell cell12 = new XRTableCell();
            cell12.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cell12.Name = "xrTableCell12";
            cell12.Weight = 0.75D;
            cell12.StylePriority.UseBorders = false;
            cell12.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "TOTAL_SCORE") });

            row2.Cells.AddRange(new XRTableCell[] { cell7, cell8, cell9, cell10, cell11, cell12 });
            row2.Cells[0].WidthF = 62F;
            row2.Cells[1].WidthF = 100F;
            row2.Cells[2].WidthF = 700F;
            row2.Cells[3].WidthF = 100F;
            row2.Cells[4].WidthF = 100F;
            row2.Cells[5].WidthF = 99F;
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        private void BindDataSource()
        {
            ConfigModel.MED_COMMON_ITEMLISTDataTable dtConfig = configService.GetConfigList(string.Empty, string.Empty, "护士长", "1");
            ConfigModel.MED_COMMON_ITEMLISTRow config = null;
            if (dtConfig != null && dtConfig.Rows.Count > 0)
            {
                config = dtConfig.FirstOrDefault(r => r.ITEM_VALUE.Equals(empNo));
            }
            if (config != null)
            {
                //护士长角色
                dtLeader = hemoService.GetPerformanceAppraisalByDateAndLeaderFlag(beginTime, endTime, "1");
                dtMember = hemoService.GetPerformanceAppraisalByDateAndLeaderFlag(beginTime, endTime, "0");
            }
            else
            {
                dtLeader = hemoService.GetPerformanceAppraisalByDateAndNurse(beginTime, endTime, empNo);
                dtMember = hemoService.GetPerformanceAppraisalByDateAndNurseLeader(beginTime, endTime, empNo);
            }

            //组员绑定数据
            if (dtMember != null && dtMember.Rows.Count > 0)
            {
                dtMember.Columns.Add("TOTAL_SCORE", typeof(decimal));
                foreach (HemodialysisModel.MED_PERFORMANCE_APPRAISALRow row in dtMember.Rows)
                {
                    row["TOTAL_SCORE"] = GetTotalScore(row.APPRAISAL_CONTENT) + 100;
                }
                dtMember.DefaultView.Sort = "TOTAL_SCORE DESC";
                LoadDataSource(dtMember, false);
            }

            //组长绑定数据
            if (dtLeader != null && dtLeader.Rows.Count > 0)
            {
                dtLeader.Columns.Add("TOTAL_SCORE", typeof(decimal));
                foreach (HemodialysisModel.MED_PERFORMANCE_APPRAISALRow row in dtLeader.Rows)
                {
                    decimal sum = 0;
                    var rows = dtMember.Where(r => r["NURSE_LEADER"].ToString().Equals(row.CHECK_NURSE));
                    rows.ToList().ForEach(r => { sum += Utility.CDecimal(r["TOTAL_SCORE"].ToString()) - 100; });
                    row["TOTAL_SCORE"] = GetTotalScore(row.APPRAISAL_CONTENT) + sum * (decimal)0.6 + 100;
                }
                dtLeader.DefaultView.Sort = "TOTAL_SCORE DESC";
                LoadDataSource(dtLeader, true);
            }
        }

        /// <summary>
        /// 获取项目评分总分
        /// </summary>
        /// <param name="appraisalContent"></param>
        /// <returns></returns>
        private decimal GetTotalScore(string appraisalContent)
        {
            decimal totalScore = 0;
            var dtContent = Utility.Transfer_XML_To_DataTable(appraisalContent);
            if (dtContent != null && dtContent.Rows.Count > 0)
            {
                foreach (DataRow content in dtContent.Rows)
                {
                    var addItem = dtAddItem.FindByID(content["ID"].ToString());
                    var minusItem = dtMinusItem.FindByID(content["ID"].ToString());
                    decimal value = addItem != null ? addItem.ITEM_VALUE : -minusItem.ITEM_VALUE;
                    int count = content["COUNT"] != DBNull.Value ? Utility.CInt(content["COUNT"].ToString()) : 1;
                    totalScore += value * count;
                }
            }
            return totalScore;
        }

        /// <summary>
        /// 加载数据源
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="isLeader"></param>
        private void LoadDataSource(HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable dtSource, bool isLeader)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("ORDER_NO", typeof(int));
            dtResult.Columns.Add("NAME", typeof(System.String));
            dtResult.Columns.Add("ITEM_CONTENT", typeof(System.String));
            dtResult.Columns.Add("ITEM_COUNT", typeof(int));
            dtResult.Columns.Add("SCORE", typeof(decimal));
            dtResult.Columns.Add("TOTAL_SCORE", typeof(decimal));

            int number = 0;

            foreach (HemodialysisModel.MED_PERFORMANCE_APPRAISALRow row in dtSource.Rows)
            {
                number++;
                var dtContent = Utility.Transfer_XML_To_DataTable(row.APPRAISAL_CONTENT);
                if (dtContent != null && dtContent.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow content in dtContent.Rows)
                    {
                        var item = dtResult.NewRow();
                        if (i == 0)
                        {
                            item["ORDER_NO"] = number;
                            item["NAME"] = row["NURSE_NAME"];
                            item["TOTAL_SCORE"] = row["TOTAL_SCORE"];
                        }
                        var addItem = dtAddItem.FindByID(content["ID"].ToString());
                        var minusItem = dtMinusItem.FindByID(content["ID"].ToString());
                        item["ITEM_CONTENT"] = addItem != null ? addItem.ITEM_NAME : minusItem.ITEM_NAME;
                        item["ITEM_COUNT"] = content["COUNT"] != DBNull.Value ? content["COUNT"] : 1;
                        item["SCORE"] = addItem != null ? addItem.ITEM_VALUE * Utility.CInt(item["ITEM_COUNT"].ToString()) : -minusItem.ITEM_VALUE * Utility.CInt(item["ITEM_COUNT"].ToString());
                        dtResult.Rows.Add(item);
                        i++;
                    }
                }
            }

            if (isLeader)
            {
                this.DetailReport1.DataSource = dtResult;
            }
            else
            {
                this.DetailReport2.DataSource = dtResult;
            }
        }

        #endregion
    }
}
