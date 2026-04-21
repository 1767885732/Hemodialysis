/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：配置列表用户控件
// 创建时间：2017-11-30
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Hemo.IService.Config;
using Hemo.Model;
using Hemo.Service;
using System.ComponentModel;

namespace Hemo.Client.UI.Config {
    public partial class CtlConfigSet : XtraUserControl {
        #region 变量


        private ConfigModel.MED_COMMON_ITEMLISTDataTable _configDataTable;

        private IConfig _configService = ServiceManager.Instance.ConfigService;

        #endregion

        #region 构造函数

        public CtlConfigSet() {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CtlConfigList_Load(object sender, EventArgs e) {
            InitializeControls();
        }



        /// <summary>
        /// 清理字体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpt_Click(object sender, EventArgs e) {
            //Microsoft YaHei (TrueType)
            Hemo.Utilities.FontConfig.FixRegistryFonts();

        }

        /// <summary>
        /// 安装字体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e) {

        }

        #endregion

        #region 方法

        private void InitializeControls() {
        }

        #endregion

      
    }
}
