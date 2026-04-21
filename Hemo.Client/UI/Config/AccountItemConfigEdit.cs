/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：记账项目编辑用户控件
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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Model;
using Hemo.IService;
using Hemo.Service;
using DevExpress.XtraEditors;

namespace Hemo.Client.UI.Config
{
    public partial class AccountItemConfigEdit : HemoBaseFrm
    {
        #region 类变量

        private ConfigModel.MED_COMMON_ITEMLISTRow _current = null;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _data = null;
        private IPatient _patientService = ServiceManager.Instance.PatientService;
        public bool IsMaxAccount = true;

        #endregion

        #region 属性

        public ConfigModel.MED_COMMON_ITEMLISTRow Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        #region 构造函数

        public AccountItemConfigEdit()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckInvalidData())
            {

                this.bindingSource1.EndEdit();
                this.bindingSource1.CurrencyManager.EndCurrentEdit();
                if (IsMaxAccount)
                {
                    var row = _data[0];
                    row.ITEM_TYPE = "大记账项目";
                }
                else
                {
                    var row = _data[0];
                    row.ITEM_TYPE = "小记账项目";
                }

                if (_patientService.SaveConfigAccountItem(_data) > 0)
                {
                    XtraMessageBox.Show("保存成功。", "基础信息");

                }
                else
                {
                    XtraMessageBox.Show("失败。", "基础信息");
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void AccountItemConfigEdit_Load(object sender, EventArgs e)
        {
            InzationData();
        }

        #endregion

        #region 方法

        private void InzationData()
        {
            this.Enabled = false;
            _data = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    if (_current != null)
                    {
                        _data.ImportRow(_current);
                    }
                    else
                    {
                        _data = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
                    }
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.bindingSource1.DataSource = _data;
                    if (_current == null)
                    {
                        this.bindingSource1.AddNew();

                        this.txtID.Text = Guid.NewGuid().ToString();
                    }
                    else
                    {
                    }
                    this.Enabled = true;
                };
                worker.RunWorkerAsync();
            }



        }

        private bool CheckInvalidData()
        {
            this.errorProvider.SetError(this.txtITEM_VALUE, string.Empty);
            this.errorProvider.SetError(this.txtITEM_NAME, string.Empty);

            if (string.IsNullOrEmpty(this.txtITEM_VALUE.Text))
            {
                this.txtITEM_VALUE.Focus();

                this.errorProvider.SetError(this.txtITEM_VALUE, "值不能为空！");

                return false;
            }

            if (string.IsNullOrEmpty(this.txtITEM_NAME.Text))
            {
                this.txtITEM_NAME.Focus();

                this.errorProvider.SetError(this.txtITEM_NAME, "名称不能为空！");

                return false;
            }
            if (string.IsNullOrEmpty(this.txtPrice.Text))
            {
                this.txtPrice.Focus();

                this.errorProvider.SetError(this.txtPrice, "价格不能为空！");

                return false;
            }
            if (string.IsNullOrEmpty(this.txtORDER_NUMBER.Text))
            {
                this.txtORDER_NUMBER.Focus();

                this.errorProvider.SetError(this.txtORDER_NUMBER, "排序字段不能为空！");

                return false;
            }

            return true;
        }

        #endregion
    }
}
