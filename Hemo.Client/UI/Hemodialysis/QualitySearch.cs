/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:查询
 * 创建标识:贺建操-2013年7月29日
 * 
 * 修改时间:2013年11月6日
 * 修改人:刘超
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年2月14日
 * 修改人:刘超
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年5月25日
 * 修改人:刘超
 * 修改描述:新增方法
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.UI.Machine;
using Hemo.Client.Controls;
using Hemo.Model;
using Hemo.Service;
using Hemo.IService.Config;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class QualitySearch : ViewBase
    {
        /// <summary>
        /// 变量
        /// </summary>
        private IConfig _configService = ServiceManager.Instance.ConfigService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public QualitySearch()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QualitySearch_Load(object sender, EventArgs e)
        {
            ctlAutoSearch ctl = new ctlAutoSearch();
            var dtItem = this._configService.GetConfigList(string.Empty, string.Empty, "质控指标检验项目", "1");
            var dtLupCondition = this._configService.GetConfigList(string.Empty, string.Empty, "质控指标查询条件", "1");
            ctl.ItemDt = dtItem;
            ctl.LupCondition = dtLupCondition;
            ctl.Dock = DockStyle.Fill;
            this.Controls.Add(ctl);
        }
    }
}
