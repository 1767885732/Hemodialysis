/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司股份有限公司
// 文件名：CtlDrugUseRecord.cs
// 文件功能描述：自定义控件药品使用 
// 创建标识：刘超 2013-07-22
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;

namespace Hemo.Client.Controls
{
    public partial class CtlDrugUseRecord : DevExpress.XtraEditors.XtraUserControl
    {
        #region 成员变量

        private HemodialysisModel.MED_CURE_DRUGDataTable dtDrug = null;

        #endregion

        #region 属性

        public HemodialysisModel.MED_CURE_DRUGDataTable DtDrug
        {
            get { return dtDrug; }
            set { dtDrug = value; }
        }

        #endregion

        #region 构造函数

        public CtlDrugUseRecord()
        {
            InitializeComponent();
        }

        #endregion

        #region 方法

        public void LoadDrugRecord()
        {

        }

        #endregion
    }
}
