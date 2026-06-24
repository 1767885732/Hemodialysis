/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：wpf绘制报表类
// 创建时间：2014-01-14
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
//
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;

namespace Hemo.Utilities {

    public class WindowGrid {

        /// <summary>
        /// 插入到CELL
        /// </summary>
        /// <param name="grid">表格</param>
        /// <param name="text">文本</param>
        /// <param name="row">行</param>
        /// <param name="column">列</param>
        /// <returns></returns>
        public TextBlock InsertCell(Grid grid, string text, int row, int column) {
            var label = new TextBlock();
            label.Margin = new Thickness(0, 0, 0, 0);
            label.TextWrapping = TextWrapping.Wrap;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.Text = text;
            label.SetValue(Grid.RowProperty, row);//指定行列
            label.SetValue(Grid.ColumnProperty, column);
            grid.Children.Add(label);
            return label;
        }

        /// <summary>
        /// 插入至cell
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="datarow"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="colName"></param>
        /// <param name="isEdit"></param>
        /// <returns></returns>
        public TextBlock InsertCell(Grid grid, System.Data.DataRow datarow, int row, int column, string colName, bool isEdit) {
            var label = new TextBlock();
            label.Margin = new Thickness(0, 0, 0, 0);
            label.TextWrapping = TextWrapping.Wrap;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.Text = datarow[colName].ToString();
            label.SetValue(Grid.RowProperty, row);//指定行列
            label.SetValue(Grid.ColumnProperty, column);
            if (isEdit) {
                label.Tag = datarow;
            }
            grid.Children.Add(label);
            return label;
        }

        /// <summary>
        /// 插入CELL
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="rm"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public TextBlock InsertCell(Grid grid, System.Data.DataRow rm, int row, int column, string text) {
            var label = new TextBlock {
                Margin = new Thickness(3, 0, 0, 0),
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = text
            };
            label.SetValue(Grid.RowProperty, row);
            label.SetValue(Grid.ColumnProperty, column);
            label.Tag = rm;//在textblock里的tag指向对应的实体
            grid.Children.Add(label);

            return label;
        }

        /// <summary>
        /// 重载插入CELL 指定跨列跨行
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="rm"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="rowSpan">跨行</param>
        /// <param name="columnSpan">跨列</param>
        /// <param name="text"></param>
        /// <returns></returns>
        public TextBlock InsertCell(Grid grid, System.Data.DataRow rm, int row,
            int column, int rowSpan, int columnSpan, string text) {
            var label = this.InsertCell(grid, rm, row, column, text);
            if (rowSpan != 0)
                label.SetValue(Grid.RowSpanProperty, rowSpan);
            if (columnSpan != 0)
                label.SetValue(Grid.ColumnSpanProperty, columnSpan);
            return label;
        }

        /// <summary>
        /// 重载插入CELL 指定跨列跨行
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="text"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="rowSpan"></param>
        /// <param name="columnSpan"></param>
        /// <returns></returns>
        public TextBlock InsertCell(Grid grid, string text, int row, int column, int rowSpan, int columnSpan) {
            var label = this.InsertCell(grid, text, row, column);
            if (rowSpan != 0)
                label.SetValue(Grid.RowSpanProperty, rowSpan);
            if (columnSpan != 0)
                label.SetValue(Grid.ColumnSpanProperty, columnSpan);
            return label;
        }

        /// <summary>
        /// 重载插入CELL 指定跨列跨行
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="text"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="rowSpan"></param>
        /// <param name="columnSpan"></param>
        /// <returns></returns>
        public TextBlock InsertCell(Grid grid, string text, int row, int column, int rowSpan, int columnSpan, double rowHeight) {
            var label = this.InsertCell(grid, text, row, column);
            if (rowSpan != 0)
                label.SetValue(Grid.RowSpanProperty, rowSpan);
            if (columnSpan != 0)
                label.SetValue(Grid.ColumnSpanProperty, columnSpan);
            if (rowHeight > 0) {
                var height = new RowHeight { Height = rowHeight };
                label.Tag = height;
            }
            return label;
        }

        /// <summary>
        /// 行高
        /// </summary>
        public class RowHeight {
            public double Height = 0.0;
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="datarow"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="rowSpan"></param>
        /// <param name="columnSpan"></param>
        /// <param name="colName"></param>
        /// <param name="isedit"></param>
        /// <returns></returns>
        public TextBlock InsertCell(Grid grid, System.Data.DataRow datarow, int row, int column, int rowSpan, int columnSpan, string colName, bool isedit) {
            var label = this.InsertCell(grid, datarow, row, column, colName, isedit);
            if (rowSpan != 0)
                label.SetValue(Grid.RowSpanProperty, rowSpan);
            if (columnSpan != 0)
                label.SetValue(Grid.ColumnSpanProperty, columnSpan);
            return label;
        }

        /// <summary>
        /// 重载插入CELL 可以指定水平对齐方式
        /// </summary>
        public TextBlock InsertCell(Grid grid, System.Data.DataRow rm,
            int row, int column, int rowSpan, int columnSpan,
            HorizontalAlignment horizontalAlignment, string text) {
            var label = this.InsertCell(grid, rm, row, column, rowSpan, columnSpan, text);
            label.HorizontalAlignment = horizontalAlignment;
            return label;
        }

        /// <summary>
        /// 重载插入CELL 可以指定水平对齐方式
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="text"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="rowSpan"></param>
        /// <param name="columnSpan"></param>
        /// <param name="horizontalAlignment"></param>
        /// <returns></returns>
        public TextBlock InsertCell(Grid grid, string text, int row, int column, int rowSpan, int columnSpan,
            HorizontalAlignment horizontalAlignment) {
            var label = this.InsertCell(grid, text, row, column, rowSpan, columnSpan);
            label.HorizontalAlignment = horizontalAlignment;
            return label;
        }

    }
}
