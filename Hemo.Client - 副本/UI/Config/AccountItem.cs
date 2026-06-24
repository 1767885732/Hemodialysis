/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：记账项目维护用户控件
// 创建时间：2014-04-25
// 创建者：刘超
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
    public partial class AccountItem : XtraUserControl
    {
        #region 类变量

        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IPatient _patientService = ServiceManager.Instance.PatientService;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _configDataTable;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public AccountItem()
        {
            InitializeComponent();
            //var areaItems = this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1");
        }

        #endregion

        #region 事件

        private void AccountItem_Load(object sender, EventArgs e)
        {
            InzationData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            using (var from = new AccountItemConfigEdit())
            {
                from.Current = null;
                if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage1)
                {
                    from.IsMaxAccount = true;
                }
                else
                {
                    from.IsMaxAccount = false;
                }
                if (from.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    InzationData();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            using (var from = new AccountItemConfigEdit())
            {

                if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage1)
                {
                    var dr = gvConfig.GetFocusedDataRow() as ConfigModel.MED_COMMON_ITEMLISTRow;

                    from.Current = dr;
                    from.IsMaxAccount = true;
                }
                else
                {
                    var dr = gridView1.GetFocusedDataRow() as ConfigModel.MED_COMMON_ITEMLISTRow;

                    from.Current = dr;
                    from.IsMaxAccount = false;
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
            else
            {
                var dr = gridView1.GetFocusedDataRow() as ConfigModel.MED_COMMON_ITEMLISTRow;
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
                    _configDataTable = _patientService.GetConfigAccountItem();
                };
                _work.RunWorkerCompleted += delegate(object sender1, RunWorkerCompletedEventArgs e1)
                {
                    var bigItemConfig = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
                    var minItemConfig = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
                    _configDataTable.Where(i => i.ITEM_TYPE == "大记账项目").CopyToDataTable<ConfigModel.MED_COMMON_ITEMLISTRow>(bigItemConfig, LoadOption.PreserveChanges);
                    _configDataTable.Where(i => i.ITEM_TYPE == "小记账项目").CopyToDataTable<ConfigModel.MED_COMMON_ITEMLISTRow>(minItemConfig, LoadOption.PreserveChanges);

                    this.gcConfig.DataSource = bigItemConfig;
                    this.gridControl1.DataSource = minItemConfig;
                };
                _work.RunWorkerAsync();
            }
        }

        #endregion  
    }
}

