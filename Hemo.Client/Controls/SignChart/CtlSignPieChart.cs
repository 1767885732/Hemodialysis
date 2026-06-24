/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:报表基础控件饼图
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Config;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using DevExpress.XtraCharts;

namespace Hemo.Client.Controls.SignChart
{
    public partial class CtlSignPieChart : DevExpress.XtraEditors.XtraUserControl
    {
        #region 构造函数
        public CtlSignPieChart()
        {
            InitializeComponent();
        }
        #endregion

        #region 方法

        /// <summary>
        /// 绘制饼图
        /// </summary>
        public void DrawPie(DataTable pDt, string pTitle)
        {
            //测试数据
            DataTable dt = new DataTable();
            dt.Columns.Add("week", typeof(string));
            dt.Columns.Add("money", typeof(decimal));

            dt.Rows.Add("星期一", 1200);
            dt.Rows.Add("星期二", 1800);
            dt.Rows.Add("星期三", 890);
            dt.Rows.Add("星期四", 1080);
            pDt = dt;
            BaseChartInfo.SetChartTitle(chartPie, true, pTitle, true, 2, StringAlignment.Center, ChartTitleDockStyle.Top, true, new Font("宋体", 12, FontStyle.Bold), Color.Red, 10);
            BaseChartInfo.DrawChart(chartPie, pDt.Rows[0][0].ToString(), ViewType.Pie, pDt, pDt.Columns[0].ToString(), pDt.Columns[1].ToString());
        }

        #endregion
    }
}
