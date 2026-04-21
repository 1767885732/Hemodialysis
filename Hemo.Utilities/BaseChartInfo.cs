/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：图表组件通用类
// 创建时间：2013-11-21
// 创建者：刘超
//  
// 修改时间：2014-4-30
// 修改人：吕志强
// 修改描述：更新图表组件通用类
//
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;

namespace Hemo.Utilities {
    public class BaseChartInfo {

        /// <summary>
        /// 静态方法
        /// </summary>
        /// <param name="control"></param>
        public static void ExportChart(DevExpress.XtraCharts.ChartControl control) { 
         //   control.ex
        }

        /// <summary>
        /// 绘制图形
        /// </summary>
        /// <param name="control">图表控件</param>
        /// <param name="seriesName">系列名</param>
        /// <param name="type">类型</param>
        /// <param name="dt">数据源</param>
        /// <param name="column1"></param>
        /// <param name="column2"></param>
        public static void DrawChart(DevExpress.XtraCharts.ChartControl control, string seriesName, ViewType type, DataTable dt, string column1, string column2) {
            Series series = new Series(seriesName, type);
            series.ArgumentScaleType = ScaleType.Qualitative;
            DataTable table = dt;
            SeriesPoint point = null;

            for (int i = 0; i < table.Rows.Count; i++) {
                if (column1.ToUpper() == "CURE_CREATE_DATE" || column1.ToUpper() == "CREATE_DATE" || column1.ToUpper() == "RESULTS_RPT_DATE_TIME")
                {
                    point = new SeriesPoint(Utility.CDate(table.Rows[i][column1].ToString()).ToShortDateString(), Utility.CDouble(table.Rows[i][column2].ToString()));
                }
                else {
                    point = new SeriesPoint(table.Rows[i][column1].ToString(), Utility.CDouble(table.Rows[i][column2].ToString()));
                }
                series.Points.Add(point);
            }

            if (type == ViewType.Line && (seriesName.Contains("透前收缩压") || seriesName.Contains("透前舒张压"))) {
                (series.Label as PointSeriesLabel).Angle = 120;
            }

            control.Series.Add(series);
            control.DataSource = table;
            
            //针对饼图的特殊处理
            /*
            if (type == ViewType.Pie) {
                //设置显示方式(Argument:显示图例说明，ArgumentAndValues:显示图例内容和数据)
                series.PointOptions.PointView = PointView.ArgumentAndValues;
                //设置数据显示形式(Percent:百分比,Currency:货币类型，数据前添加￥,Scientific:科学计数法)
                series.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                //数据是否保留小数(0：不保留小数位，1保留一位小数，2保留两位小数)
                series.PointOptions.ValueNumericOptions.Precision = 0;
                //数据以百分比显示时只能是Default和None
                ((PieSeriesLabel)series.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
            }
            
            if (type == ViewType.Pie)
            {
                //设置显示方式(Argument:显示图例说明，ArgumentAndValues:显示图例内容和数据)
                SeriesLabelBase a = series.Label;

                a.TextPattern = "{V:#.00}";
                //设置数据显示形式(Percent:百分比,Currency:货币类型，数据前添加￥,Scientific:科学计数法)
               // series.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                //数据是否保留小数(0：不保留小数位，1保留一位小数，2保留两位小数)
               // series.PointOptions.ValueNumericOptions.Precision = 0;
                //数据以百分比显示时只能是Default和None
                ((PieSeriesLabel)series.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
            
            }
             */
        }

        /// <summary>
        /// 填充明细资料
        /// </summary>
        /// <param name="control"></param>
        /// <param name="seriesName"></param>
        /// <param name="type"></param>
        /// <param name="dt"></param>
        /// <param name="column1"></param>
        /// <param name="column2"></param>
        public static void DrawChartDetail(DevExpress.XtraCharts.ChartControl control, string seriesName, ViewType type, DataTable dt, string column1, string column2)
        {
            Series series = new Series(seriesName, type);
            DataTable table = dt;
            SeriesPoint point = null;

            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (column1.ToUpper() == "CURE_CREATE_DATE" || column1.ToUpper() == "CREATE_DATE")
                {
                    point = new SeriesPoint(Utility.CDate(table.Rows[i][column1].ToString()).ToShortTimeString(), Utility.CDouble(table.Rows[i][column2].ToString()));
                }
                else
                {
                    point = new SeriesPoint(table.Rows[i][column1].ToString(), Utility.CDouble(table.Rows[i][column2].ToString()));
                }
                series.Points.Add(point);
            }

            if (type == ViewType.Line && (seriesName.Contains("透前收缩压") || seriesName.Contains("透前舒张压")))
            {
                (series.Label as PointSeriesLabel).Angle = 120;
            }
            //if (type == ViewType.Bar && seriesName.Contains("透前体重"))
            //{
            //    (series.Label as PointSeriesLabel).Angle = 120;
            //}

            control.Series.Add(series);
            /*
            //针对饼图的特殊处理
            if (type == ViewType.Pie)
            {
                //设置显示方式(Argument:显示图例说明，ArgumentAndValues:显示图例内容和数据)
                series.PointOptions.PointView = PointView.ArgumentAndValues;
                //设置数据显示形式(Percent:百分比,Currency:货币类型，数据前添加￥,Scientific:科学计数法)
                series.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                //数据是否保留小数(0：不保留小数位，1保留一位小数，2保留两位小数)
                series.PointOptions.ValueNumericOptions.Precision = 0;
                //数据以百分比显示时只能是Default和None
                ((PieSeriesLabel)series.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
            }
            */
        }
        /// <summary>
        /// 设置图表标题
        /// </summary>
        /// <param name="control">图表控件</param>
        /// /// <param name="isVisible">标题是否可见</param>
        /// <param name="text">标题文本</param>
        /// <param name="isWordWrop">是否换行</param>
        /// <param name="maxLineCount">最大允许行数</param>
        /// <param name="alignment">对齐方式</param>
        /// <param name="dock">位置</param>
        /// <param name="isAntialiasing">是否允许设置外观</param>
        /// <param name="font">字体</param>
        /// <param name="textColor">文本颜色</param>
        /// <param name="indent">字体缩进值</param>
        public static void SetChartTitle(DevExpress.XtraCharts.ChartControl control, bool isVisible, String text, bool isWordWrop, int maxLineCount, StringAlignment alignment, ChartTitleDockStyle dock, bool isAntialiasing, Font font, Color textColor, int indent) {
            //设置标题
            ChartTitle title = new ChartTitle();
            title.Visibility = isVisible ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
            //title.Visible = isVisible;
            //显示文本 
            title.Text = text;
            //是否允许换行
            title.WordWrap = isWordWrop;
            //最大允许行数
           // title.MaximumLinesCount = maxLineCount;
            title.MaxLineCount = maxLineCount;
            //对齐方式
            title.Alignment = alignment;
            //位置
            title.Dock = dock;
            //是否允许设置外观
            title.Antialiasing = isAntialiasing;
            //字体
            title.Font = font;
            //字体颜色
            title.TextColor = textColor;
            //缩进值
            title.Indent = indent;
            control.Titles.Add(title);
        }
        /// <summary>
        /// 为X轴添加标题
        /// </summary>
        /// <param name="control">图形控件</param>
        /// <param name="isVisible">标题是否可见</param>
        /// <param name="aligment">对齐方式</param>
        /// <param name="text">标题显示文本</param>
        /// <param name="color">标题字体颜色</param>
        /// <param name="isAntialiasing">是否允许设置外观</param>
        /// <param name="font">字体</param>
        public static void SetAxisX(DevExpress.XtraCharts.ChartControl control, bool isVisible, StringAlignment aligment, string text, Color color, bool isAntialiasing, Font font)
        {
            XYDiagram xydiagram = (XYDiagram)control.Diagram;
            //xydiagram.AxisX.Title.Visible = isVisible;
            xydiagram.AxisX.Title.Visibility = isVisible ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
            xydiagram.AxisX.Title.Alignment = aligment;
            xydiagram.AxisX.Title.Text = text;
            xydiagram.AxisX.Title.TextColor = color;
            xydiagram.AxisX.Title.Antialiasing = isAntialiasing;
            xydiagram.AxisX.Title.Font = font;
        }

        /// <summary>
        /// 为Y轴添加标题
        /// </summary>
        /// <param name="control">图形控件</param>
        /// <param name="isVisible">标题是否可见</param>
        /// <param name="aligment">对齐方式</param>
        /// <param name="text">标题显示文本</param>
        /// <param name="color">标题字体颜色</param>
        /// <param name="isAntialiasing">是否允许设置外观</param>
        /// <param name="font">字体</param>
        public static void SetAxisY(DevExpress.XtraCharts.ChartControl control, bool isVisible, StringAlignment aligment, string text, Color color, bool isAntialiasing, Font font) {
            XYDiagram xydiagram = (XYDiagram)control.Diagram;
            //xydiagram.AxisY.Title.Visible = isVisible;
            xydiagram.AxisX.Title.Visibility = isVisible ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
            xydiagram.AxisY.Title.Alignment = aligment;
            xydiagram.AxisY.Title.Text = text;
            xydiagram.AxisY.Title.TextColor = color;
            xydiagram.AxisY.Title.Antialiasing = isAntialiasing;
            xydiagram.AxisY.Title.Font = font;
            //xydiagram.AxisY.Range.MinValue = 0;
            //xydiagram.AxisY.Range.MaxValue = 200;
        }

        /// <summary>
        /// 为Y轴添加标题
        /// </summary>
        /// <param name="control">图形控件</param>
        /// <param name="isVisible">标题是否可见</param>
        /// <param name="aligment">对齐方式</param>
        /// <param name="text">标题显示文本</param>
        /// <param name="color">标题字体颜色</param>
        /// <param name="isAntialiasing">是否允许设置外观</param>
        /// <param name="font">字体</param>
        public static void SetAxisY(DevExpress.XtraCharts.ChartControl control, bool isVisible, StringAlignment aligment, string text, Color color, bool isAntialiasing, Font font, decimal minValue, decimal maxValue) {
            XYDiagram xydiagram = (XYDiagram)control.Diagram;
           // xydiagram.AxisY.Title.Visible = isVisible;
            xydiagram.AxisX.Title.Visibility = isVisible ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
            xydiagram.AxisY.Title.Alignment = aligment;
            xydiagram.AxisY.Title.Text = text;
            xydiagram.AxisY.Title.TextColor = color;
            xydiagram.AxisY.Title.Antialiasing = isAntialiasing;
            xydiagram.AxisY.Title.Font = font;
            xydiagram.AxisY.VisualRange.MinValue = minValue;
            xydiagram.AxisY.VisualRange.MaxValue = maxValue;
            //xydiagram.AxisY.Range.MinValue = minValue;
           // xydiagram.AxisY.Range.MaxValue = maxValue;
        }
    }
}
