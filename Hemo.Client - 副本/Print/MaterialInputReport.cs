/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:入库记录报表
 * 创建标识:贺建操-2016年5月15日
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
    public partial class MaterialInputReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MoonDate"></param>
        /// <param name="date"></param>
        public MaterialInputReport(DateTime MoonDate,DrugModel.MED_MATERIAL_INPUTDataTable date)
        {
            InitializeComponent();
            this.xrlabMoon.Text = MoonDate.ToString("yyyy年MM月");
            this.DataSource = date;
            this.DataMember = "";
        }

        #endregion
    }
}
