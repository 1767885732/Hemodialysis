/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：报表打印窗体类
// 创建时间：2015-06-21
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Controls;
using DevExpress.XtraEditors;
using Hemo.Client.Controls;

namespace Hemo.Client.UI.Machine
{
    public partial class ShowPrintForm :HemoBaseFrm
    {
        #region 类变量

        private CtlMedicalDocumentContainer _medicalDocContainer = new CtlMedicalDocumentContainer();

        #endregion

        #region 构造函数

        public ShowPrintForm(UserControl userCtl)
        {
            InitializeComponent();

            this._medicalDocContainer.CurrentMedicalDocument = userCtl;
            this.documentContainerHost.Child = this._medicalDocContainer;
        }

        #endregion
    }
}