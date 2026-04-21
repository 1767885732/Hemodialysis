/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:出库记录报表
 * 创建标识:贺建操-2016年5月17日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Hemo.IService.Machine;
using Hemo.Service;
using Hemo.Model;
using Hemo.Utilities;

namespace Hemo.Client.Print
{
    public partial class MaterialOutputReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        public MaterialOutputReport(DateTime MoonDate,DrugModel.MED_MATERIAL_OUTPUTDataTable date)
        {
            InitializeComponent();
            this.xrlabMoon.Text = MoonDate.ToString("yyyy年MM月");
            this.DataSource = date;
            this.DataMember = "";
        }

        #endregion
    }
}
