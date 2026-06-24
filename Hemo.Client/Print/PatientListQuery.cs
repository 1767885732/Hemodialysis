/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:病人列表报表
 * 创建标识:贺建操-2016年6月7日
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
    public partial class PatientListQuery : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_patients"></param>
        public PatientListQuery(PatientModel.MED_PATIENTSDataTable _patients)
        {
            InitializeComponent();

            this.DataSource = _patients;
            this.DataMember = "";
        }

        #endregion
    }
}
