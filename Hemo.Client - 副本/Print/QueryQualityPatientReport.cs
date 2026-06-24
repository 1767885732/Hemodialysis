/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：维持性血透患者质量监测表报表
// 创建时间：2015-12-28
// 创建者：刘超
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
using Hemo.Model;
using Hemo.IService.Machine;
using Hemo.Service;
using Hemo.IService.Config;
using System.Data;

namespace Hemo.Client.Print
{
    public partial class QueryQualityPatientReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="date"></param>
        /// <param name="workloadTable"></param>
        public QueryQualityPatientReport(DataTable date)
        {
            InitializeComponent();


            this.xrLabelTitle.Text = System.DateTime.Parse(ConvertToString(date.Rows[0]["REPORT_ADD_DATE"])).ToString("yyyy年MM月");


            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "NAME")});
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PJXY")});
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PJHEMODATE")});
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "RZQC")});
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SJPXJZ")});
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GLCJ")});
            this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "JZPXKJ")});
            this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ZGSSD")});
            this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ZGQDBS")});
            this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ZGZDBS")});
            this.DataSource = date;
            this.DataMember = "";
        }
        /// <summary>
        /// 对像转换
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private string ConvertToString(object o)
        {
            if (o == null)
                return string.Empty;
            if (o == DBNull.Value || o is DBNull)
                return string.Empty;
            return o.ToString();
            
        }
        #endregion
    }
}
