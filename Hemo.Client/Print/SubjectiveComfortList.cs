/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：患者透析时间及舒适度评价报表
// 创建时间：2015-11-27
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

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
    public partial class SubjectiveComfortList : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        public SubjectiveComfortList()
        {
            InitializeComponent();
        }

        #endregion

        #region 方法

        public void BindData(DateTime dateTime)
        {
            IHemodialysis hemoDialysis = ServiceManager.Instance.HemodialysisService;
            var data = hemoDialysis.GetSubjectiveComfortData(dateTime.ToString("yyyy-MM"));
            this.DataSource = data;
            this.xrLabel1.Text = string.Format("({0})", dateTime.ToString("yyyy-MM"));
            //throw new NotImplementedException();
            this.DataMember = "";
        }

        #endregion
    }
}
