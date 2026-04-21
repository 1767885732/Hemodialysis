/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:厂商信息类
 * 创建标识:刘超-2016年3月8日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Service;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.IService;

namespace Hemo.Client.UI.Store
{
    public partial class CtlSupplierInfo : DevExpress.XtraEditors.XtraUserControl
    {
        #region 构造函数

        public CtlSupplierInfo()
        {
            InitializeComponent();
        }

        #endregion
    }
}
