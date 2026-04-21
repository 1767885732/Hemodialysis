/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：编辑配置窗体
// 创建时间：2014-03-06
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Config;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;

namespace Hemo.Client.UI.Config {
    public partial class EditConfig :HemoBaseFrm{
        #region 变量

        private int _initialValue = 10;
        private string _itemType;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _configDataTable;
        private ConfigModel.MED_COMMON_ITEMLISTRow _configDataRow;
        private DataTable _dtConfigType = new DataTable();
        private IConfig _configService = ServiceManager.Instance.ConfigService;

        #endregion

        #region 构造函数

        public EditConfig(string itemType, ConfigModel.MED_COMMON_ITEMLISTDataTable configDataTable, ConfigModel.MED_COMMON_ITEMLISTRow configDataRow) {
            this.InitializeComponent();

            this.Text = this._itemType = itemType;
            this._configDataTable = configDataTable;
            this._configDataRow = configDataRow;
        }

        #endregion

        #region 方法

        private void InitializeControls() {
            this._dtConfigType = this._configService.GetConfigTypeList();

            this.cbxSTATUS.SelectedIndex = 0;

            if (this._configDataRow == null) //新增
                this.txtORDER_NUMBER.Text = this.GetMaxOrderNumber();
            else //修改
            {
                this.txtITEM_VALUE.Text = this._configDataRow.ITEM_VALUE;
                this.txtITEM_NAME.Text = this._configDataRow.ITEM_NAME;
                this.cbxSTATUS.SelectedIndex = Utility.CInt(this._configDataRow.STATUS);
                this.txtORDER_NUMBER.Text = this._configDataRow.ORDER_NUMBER.ToString();
            }
        }

        private string GetMaxOrderNumber() {
            int i = this._initialValue;

            DataRow[] rows = this._dtConfigType.Select(string.Format("ITEM_TYPE = '{0}'", this._itemType));

            if (rows.Length > 0)
                i += Utility.CInt(rows[0]["Count"].ToString());

            return i.ToString();
        }

        private bool IsDataValidate() {
            this.errorProvider.SetError(this.txtITEM_VALUE, string.Empty);
            this.errorProvider.SetError(this.txtITEM_NAME, string.Empty);

            if (string.IsNullOrEmpty(this.txtITEM_VALUE.Text)) {
                this.txtITEM_VALUE.Focus();

                this.errorProvider.SetError(this.txtITEM_VALUE, "值不能为空！");

                return false;
            }

            if (string.IsNullOrEmpty(this.txtITEM_NAME.Text)) {
                this.txtITEM_NAME.Focus();

                this.errorProvider.SetError(this.txtITEM_NAME, "名称不能为空！");

                return false;
            }

            if (string.IsNullOrEmpty(this.txtORDER_NUMBER.Text)) {
                this.txtORDER_NUMBER.Focus();

                this.errorProvider.SetError(this.txtORDER_NUMBER, "排序字段不能为空！");

                return false;
            }

            return true;
        }

        #endregion

        #region 事件
        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void EditConfig_Load(object sender, EventArgs e) {
            this.InitializeControls();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e) {
            if (!this.IsDataValidate())
                return;

            if (this._configDataRow == null) //新增
            {
                this._configDataRow = this._configDataTable.NewMED_COMMON_ITEMLISTRow();

                this._configDataRow.ITEM_ID = Guid.NewGuid().ToString();
                this._configDataRow.ITEM_TYPE = this._itemType;

                this._configDataTable.AddMED_COMMON_ITEMLISTRow(this._configDataRow);
            }

            this._configDataRow.ITEM_VALUE = this.txtITEM_VALUE.Text;
            this._configDataRow.ITEM_NAME = this.txtITEM_NAME.Text;
            this._configDataRow.STATUS = this.cbxSTATUS.SelectedIndex.ToString();
            this._configDataRow.ORDER_NUMBER = Utility.CDecimal(this.txtORDER_NUMBER.Text);

            this._configService.SaveConfigInfo(this._configDataTable);
            AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);

            //XtraMessageBox.Show("保存成功！");

            this.DialogResult = DialogResult.Yes;
        }

        #endregion
    }
}
