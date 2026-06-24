/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技股份有限公司
 * 文件功能描述:患者借药统计报表
 * 创建标识:刘超-2016年3月10日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using DevExpress.XtraReports.UI;
using Hemo.IService.Machine;
using Hemo.Service;
using Hemo.Model;
using Hemo.IService.Config;

namespace Hemo.Client.Print
{
    public partial class BorrowPrintList : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        //  private IConfig _configService = ServiceManager.Instance.ConfigService;
        public BorrowPrintList()
        {
            InitializeComponent();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="data"></param>
        public void BindData(System.Data.DataTable data)
        {
            this.DataSource = data;
            //throw new NotImplementedException();
            this.DataMember = "";
        }

        #endregion
    }
}
