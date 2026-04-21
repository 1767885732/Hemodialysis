/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技股份有限公司
 * 文件功能描述:医生交班记录报表
 * 创建标识:刘超-2016年7月25日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Linq;
using System.Data;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Hemo.Model;

namespace Hemo.Client.Print
{
    public partial class DocChangeWorkReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        public DocChangeWorkReport(HemoModel.MED_HEMO_CHAGEWORKDataTable _docChangeWork,string title)
        {
            InitializeComponent();

            this.lbTitle.Text = string.Format("交班日期:{0}", title);
            var data = new HemoModel.MED_HEMO_CHAGEWORKDataTable();
            _docChangeWork.OrderBy(i => i.NAME).CopyToDataTable<HemoModel.MED_HEMO_CHAGEWORKRow>(data, LoadOption.PreserveChanges);
            this.DataSource = data;
            this.DataMember = "";
        }

        #endregion
    }
}
