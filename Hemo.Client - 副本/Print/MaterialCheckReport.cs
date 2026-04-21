/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:月盘点记录记录报表
 * 创建标识:刘超-2016年3月15日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Hemo.IService.Machine;
using Hemo.Service;
using System.Linq;
using Hemo.Model;
using Hemo.Utilities;
using System.Data;

namespace Hemo.Client.Print
{
    public partial class MaterialCheckReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MoonDate"></param>
        /// <param name="date"></param>
        public MaterialCheckReport(DateTime MoonDate,DrugModel.MED_MATERIAL_CHECKDataTable date)
        {
            InitializeComponent();
            this.xrlabMoon.Text = MoonDate.ToString("yyyy年MM月dd日");
            var dtDetail = new DrugModel.MED_MATERIAL_CHECKDataTable();
            date.Where(i => i.CHECKTYPE == "1").CopyToDataTable(dtDetail, LoadOption.PreserveChanges);
            this.DataSource = dtDetail;
            this.DataMember = "";
        }

        #endregion
    }
}
