/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:报表基础控件线性图
 * 创建标识:贺建操-2013年7月16日
 * 
 * 修改时间:2013年10月24日
 * 修改人:吕志强
 * 修改描述:修改方法
 * 
 * 修改时间:2014年2月1日
 * 修改人:贺建操
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2017年9月29日
 * 修改人:刘配齐
 * 修改描述:修复大范围查询下x轴不能滚动的bug
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;

namespace Hemo.Client.Controls.SignChart
{
    public partial class CtlTendencyChart : DevExpress.XtraEditors.XtraUserControl
    {
        public CtlTendencyChart()
        {
            InitializeComponent();
        }

        #region 方法
        /// <summary>
        /// 颜色集
        /// </summary>
        List<Color> colorList = new List<Color>() { Color.Red, Color.Aqua, Color.CadetBlue, Color.DarkGreen, Color.BlueViolet };

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="dt"></param>
        public void InzatioData(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                this.gridView1.Columns.Clear();
                this.gridControlData.DataSource = dt;

                for (int i = 0; i < this.gridView1.Columns.Count; i++)
                {
                    this.gridView1.Columns[i].Width = 100;
                }
                //this.chartControl1 = new ChartControl();
                CreateChart(dt);
            }
        }
        /// <summary>
        /// 创建图形对你
        /// </summary>
        /// <param name="dt"></param>
        private void CreateChart(DataTable dt)
        {
            #region Series
            ////创建几个图形的对象
            List<Series> list = new List<Series>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(CreateSeries(dt.Rows[i][0].ToString(), ViewType.Line, dt, i));
            }
            #endregion

            chartControl1.Series.Clear();
            chartControl1.Series.AddRange(list.ToArray());
            chartControl1.Legend.Visible = false;
            chartControl1.SeriesTemplate.Label.Visible = true;// DefaultBoolean.True;//LabelsVisibility
            ((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Clear();
            if (dt.Columns.Count > 8)
            {
                ((XYDiagram)this.chartControl1.Diagram).AxisX.VisualRange.MinValue = dt.Columns[1].ColumnName;
                ((XYDiagram)this.chartControl1.Diagram).AxisX.VisualRange.MaxValue = dt.Columns[7].ColumnName;
            }

            for (int i = 0; i < list.Count; i++)
            {
                //设置图表线型颜色
                //list[i].View.Color = colorList[i];
                //创建图表的第二坐标系
                // CreateAxisY(list[i]);
            }
        }

        /// <summary>
        /// 根据数据创建一个图形展现
        /// </summary>
        /// <param name="caption">图形标题</param>
        /// <param name="viewType">图形类型</param>
        /// <param name="dt">数据DataTable</param>
        /// <param name="rowIndex">图形数据的行序号</param>
        /// <returns></returns>
        private Series CreateSeries(string caption, ViewType viewType, DataTable dt, int rowIndex)
        {
            Series series = new Series(caption, viewType);
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                string argument = dt.Columns[i].ColumnName;//参数名称
                //decimal value = (decimal)dt.Rows[rowIndex][i];//参数值
                if (string.IsNullOrEmpty(dt.Rows[rowIndex][i].ToString()))
                    continue;
                string value = dt.Rows[rowIndex][i].ToString();//参数值

                series.Points.Add(new SeriesPoint(argument, value));
            }

            //必须设置ArgumentScaleType的类型，否则显示会转换为日期格式，导致不是希望的格式显示
            //也就是说，显示字符串的参数，必须设置类型为ScaleType.Qualitative
            series.ArgumentScaleType = ScaleType.Qualitative;
            series.Label.Visible = true;//.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;//显示标注标签
            series.Label.ResolveOverlappingMode = ResolveOverlappingMode.JustifyAllAroundPoint;
            return series;
        }
        /// <summary>
        /// 设置Title
        /// </summary>
        /// <param name="strTitle"></param>
        public void SetContolTitle(string strTitle)
        {
            this.lb_Text.Text = strTitle;
        }
        /// <summary>
        /// 隐藏下边的panel
        /// </summary>
        public void HideBottomPanel()
        {
            this.BottomPanel.Visible = false;

        }

        /// <summary>
        /// 创建图表的第二坐标系
        /// </summary>
        /// <param name="series">Series对象</param>
        /// <returns></returns>
        private SecondaryAxisY CreateAxisY(Series series)
        {
            SecondaryAxisY myAxis = new SecondaryAxisY(series.Name);
            ((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Add(myAxis);
            ((LineSeriesView)series.View).AxisY = myAxis;
            myAxis.Title.Text = series.Name;
            myAxis.Title.Alignment = StringAlignment.Far; //顶部对齐
            myAxis.Title.Visible = true; //显示标题
            myAxis.Title.Font = new Font("宋体", 9.0f);

            Color color = series.View.Color;//设置坐标的颜色和图表线条颜色一致

            myAxis.Title.TextColor = color;
            myAxis.Label.TextColor = color;
            myAxis.Color = color;

            return myAxis;
        }

        /// <summary>
        /// 打印
        /// </summary>
        public void PrintChart()
        {
            this.chartControl1.ShowPrintPreview(DevExpress.XtraCharts.Printing.PrintSizeMode.Zoom);
        }
        /// <summary>
        /// 可以自定打印
        /// </summary>
        public void PrintSelf()
        {
            //this.chartControl1.ShowPrintPreview(DevExpress.XtraCharts.Printing.PrintSizeMode.Zoom);
            DevExpress.XtraPrintingLinks.CompositeLink compositeLink = new DevExpress.XtraPrintingLinks.CompositeLink();
            DevExpress.XtraPrinting.PrintingSystem ps = new DevExpress.XtraPrinting.PrintingSystem();

            compositeLink.PrintingSystem = ps;
            compositeLink.Landscape = true;
            compositeLink.PaperKind = System.Drawing.Printing.PaperKind.A4;

            DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(ps);
            ps.PageSettings.Landscape = true;
            link.Component = this.chartControl1;
            compositeLink.Links.Add(link);

            link.CreateDocument();  //建立文档
            ps.PreviewFormEx.Show();//进行预览
        }
        /// <summary>
        /// 导出Excel
        /// </summary>
        public void ExportGridToExcel()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "导出Excel";
            fileDialog.Filter = "Excel文件(*.xls)|*.xls";
            DialogResult dialogResult = fileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                gridControlData.ExportToXls(fileDialog.FileName);
                DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 打印GridControl
        /// </summary>
        public void PrintGridView()
        {
            this.gridControlData.ShowPrintPreview();
        }


        #endregion
    }
}

