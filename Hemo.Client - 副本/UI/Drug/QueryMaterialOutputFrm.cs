/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：药品耗材出库信息列表窗体
// 创建时间：2013-07-30
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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Hemo.Service;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.IService;
using Hemo.Client.Print;
using Hemo.IService.Config;


namespace Hemo.Client.UI.Drug {
    public partial class QueryMaterialOutputFrm : HemoBaseFrm
    {
        #region 构造函数

        public QueryMaterialOutputFrm() {
            InitializeComponent();
        }

        #endregion
    }
}
