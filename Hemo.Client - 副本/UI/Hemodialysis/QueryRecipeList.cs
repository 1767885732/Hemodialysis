/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:用户控件类
 * 创建标识:刘超-2013年5月31日
 * 
 * 修改时间:2013年9月8日
 * 修改人:贺建操
 * 修改描述:修改方法
 * 
 * 修改时间:2013年12月17日
 * 修改人:贺建操
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年3月27日
 * 修改人:顾伟伟
 * 修改描述:新增方法SQL
 * ----------------------------------------------------------------*/
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.IService.Config;
using DevExpress.XtraEditors.Controls;
using Hemo.IService;
using Hemo.IService.Dict;
using Hemo.Client.UI.PatientFixUI;
using Hemo.Client.Core;
using Hemo.IService.Permission;
using Hemo.IService.PatientSchedule;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class QueryRecipeList : HemoBaseFrm
    {
        #region 变量
        private string hemoid = string.Empty;
        private int tabIndex = 0;
        private DateTime _currentDt = DateTime.Now;

        public DateTime CurrentDt
        {
            get { return _currentDt; }
            set { _currentDt = value; }
        }

        public string currentRecipeIdStr { get; set; }
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pHemoID"></param>
        /// <param name="pTabIndex"></param>

        public QueryRecipeList(string pHemoID, int pTabIndex)
        {
            InitializeComponent();
            hemoid = pHemoID;
            tabIndex = pTabIndex;
        }
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryRecipeList_Load(object sender, EventArgs e)
        {
            QueryRecipeListUI queryRecipeListUc = new QueryRecipeListUI(hemoid, tabIndex);
            queryRecipeListUc.CurrentDt = CurrentDt;
            queryRecipeListUc.currentRecipeIdStr = currentRecipeIdStr;
            queryRecipeListUc.Dock = DockStyle.Fill;
            this.Controls.Add(queryRecipeListUc);
        }

    }
}