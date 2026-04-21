/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:修复系统响应速度慢的问题
 * 创建标识:顾伟伟-2017年2月12日
 * 
 * 修改时间:2017年6月30日
 * 修改人:贺建操
 * 修改描述:修复系统响应速度慢的问题
 * 
 * 修改时间:2017年8月1日
 * 修改人:顾伟伟
 * 修改描述:修改对外公开的方法
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
using Hemo.Model;
using Hemo.IService.Config;
using Hemo.Service;

namespace Hemo.Client.Modules.PatientsMgr
{
    public partial class SearchByP : DevExpress.XtraEditors.XtraUserControl
    {
        #region 变量
        
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        #endregion
        #region 构造函数
        
        public SearchByP()
        {
            InitializeComponent();
        }
        #endregion

        #region 事件

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
           var x = this.Parent;
          // PatientDtlEditMain.SelectGroup();
        }
        /// <summary>
        /// 登录窗体时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchByP_Load(object sender, EventArgs e)
        {
            ConfigModel.MED_COMMON_ITEMLISTDataTable config = this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1");
            if (config != null && config.Rows.Count > 0)
            {
                DataRow SickAreaRow = config.NewRow();
                SickAreaRow["ITEM_NAME"] = "全部";
                SickAreaRow["ITEM_ID"] = "c570d95c-76a2-4af4-893a-1357065623bf";
                SickAreaRow["ORDER_NUMBER"] = 0;
                config.Rows.InsertAt(SickAreaRow, 0);
                Hemo.Utilities.Utility.BindLookUpEdit(ediSickArea, "ITEM_ID", "ITEM_NAME", (DataTable)config, "ITEM_NAME", "区域");
            }
            if (config.Rows.Count > 0)
            {
                ediSickArea.EditValue = config[1].ITEM_ID;
            }
        }

        #endregion

    }
}
