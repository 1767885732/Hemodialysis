/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:配液桶消毒记录报表
 * 创建标识:贺建操-2016年4月17日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Hemo.Model;

namespace Hemo.Client.Print
{
    public partial class MixMachineReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        public MixMachineReport(MachineModel.MED_MACHINE_MIXBARRELDataTable _data)
        {
            InitializeComponent();

            this.DataSource = _data;
        }

        #endregion
    }
}
