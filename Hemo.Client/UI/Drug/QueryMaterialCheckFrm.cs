/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：耗材盘点窗体
// 创建时间：2013-07-08
// 创建者：刘超
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
using Hemo.Client.Core;
using Hemo.Client.Print;


namespace Hemo.Client.UI.Drug
{
    public partial class QueryMaterialCheckFrm : HemoBaseFrm
    {
        #region 构造函数

        public QueryMaterialCheckFrm()
        {
            InitializeComponent();
        }

        #endregion
    }
}
