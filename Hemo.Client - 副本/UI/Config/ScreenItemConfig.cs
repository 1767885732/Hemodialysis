/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：大屏配置项用户控件
// 创建时间：2016-06-06
// 创建者：贺建操
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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Utilities;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.IService;
using Hemo.Model;
using DevExpress.XtraEditors;

namespace Hemo.Client.UI.Config
{
    public partial class ScreenItemConfig : XtraUserControl
    {
        #region 类变量

        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IPatient _patientService = ServiceManager.Instance.PatientService;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _configDataTable;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public ScreenItemConfig()
        {
            InitializeComponent();

        }

        #endregion

        #region 事件

        private void ScreenItemConfig_Load(object sender, EventArgs e)
        {
            InzationData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            using (var from = new ScreenItemConfigConfigEdit())
            {
                if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage1)
                {
                    var dr = gvConfig.GetFocusedDataRow() as ConfigModel.MED_COMMON_ITEMLISTRow;
                    from.Current = dr;
                }
                if (from.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    InzationData();
                }
            }
        }

        private void gvConfig_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                var dr = gvConfig.GetFocusedDataRow() as ConfigModel.MED_COMMON_ITEMLISTRow;
                if (dr != null)
                    this.btnEdit.Enabled = true;
                else
                    this.btnEdit.Enabled = false;
            }

        }

        #endregion

        #region 方法

        private void InzationData()
        {
            using (var _work = new BackgroundWorker())
            {
                _work.DoWork += delegate(object sender, DoWorkEventArgs e)
                {
                    _configDataTable = _configService.GetConfigList(string.Empty, string.Empty, "大屏配置项", "1");
                };
                _work.RunWorkerCompleted += delegate(object sender1, RunWorkerCompletedEventArgs e1)
                {
                    this.gcConfig.DataSource = _configDataTable;
                };
                _work.RunWorkerAsync();
            }
        }

        #endregion 
    }
}

