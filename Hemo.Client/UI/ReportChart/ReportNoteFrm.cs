/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:报表说明类
 * 创建标识:吕志强-2017年4月20日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hemo.Client.UI.ReportChart
{
    public partial class ReportNoteFrm : HemoBaseFrm
    {
        #region 类变量

        ReportTypeEnum reportType;

        #endregion

        #region 属性

        /// <summary>
        /// 报表类型
        /// </summary>
        public ReportTypeEnum ReportType
        {
            get { return reportType; }
            set { reportType = value; }
        }

        #endregion

        #region 构造函数

        public ReportNoteFrm()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportNoteFrm_Load(object sender, EventArgs e)
        {
            string note = "备注：维持性透析指的是，以当前时间为参考起点，上周透析过，三个月内连续透析过，并且每周透析两次或以上。";
            switch (reportType)
            {
                case ReportTypeEnum.治疗数据:
                    this.lblReportName.Text = "治疗数据";
                    this.meNote.Text = "统计维持性透析患者指定时间范围内相关透析体征数据。\r\n\r\n";
                    this.meNote.Text += note;
                    break;
                case ReportTypeEnum.血透患者记录:
                    this.lblReportName.Text = "血透记录单";
                    this.meNote.Text = "统计血透患者相关基本信息，查阅历史透析记录单。";
                    break;
                case ReportTypeEnum.数据汇总:
                    this.lblReportName.Text = "数据汇总";
                    this.meNote.Text = "统计维持性透析患者指定时间范围内不同透析方式的透析人次以及汇总。\r\n\r\n";
                    this.meNote.Text += note;
                    break;
                case ReportTypeEnum.导管手术例数:
                    this.lblReportName.Text = "导管手术例数";
                    this.meNote.Text = "统计维持性透析患者指定时间范围内不同导管手术的例数以及汇总，患者在一段时间范围内，若存在多个相同导管手术，取最新日期对应记录进行统计。\r\n\r\n";
                    this.meNote.Text += note;
                    break;
                case ReportTypeEnum.透析男女比例:
                    this.lblReportName.Text = "透析男女比例";
                    this.meNote.Text = "统计维持性透析患者指定时间范围内透析男女例数以及汇总。\r\n\r\n";
                    this.meNote.Text += note;
                    break;
                case ReportTypeEnum.透析年龄段:
                    this.lblReportName.Text = "透析年龄段";
                    this.meNote.Text = "统计维持性透析患者指定时间范围内透析年龄各个阶段例数以及汇总。\r\n\r\n";
                    this.meNote.Text += note;
                    break;
                case ReportTypeEnum.传染病:
                    this.lblReportName.Text = "传染病";
                    this.meNote.Text = "统计维持性透析患者指定时间范围内透析传染病例数以及汇总，以患者透析第一周内传染病检查结果为准进行统计。\r\n\r\n";
                    this.meNote.Text += note;
                    break;
                case ReportTypeEnum.规律透析比例:
                    this.lblReportName.Text = "规律透析比例";
                    this.meNote.Text = "统计维持性透析患者指定时间范围内规律透析例数以及汇总。\r\n\r\n";
                    this.meNote.Text += note;
                    break;
                case ReportTypeEnum.工作量统计:
                    break;
            }
            this.meNote.DeselectAll();
        }

        #endregion
    }
}