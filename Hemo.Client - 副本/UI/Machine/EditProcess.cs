/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：流程节点设定窗体
// 创建时间：2016-04-21
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
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Machine;
using Hemo.Service;
using Hemo.Model;
using Hemo.IService.Config;
using Hemo.Utilities;

namespace Hemo.Client.UI.Machine
{
    public partial class EditProcess : HemoBaseFrm
    {
        #region 类变量

        IMachine _imachine = ServiceManager.Instance.MachineService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private static MachineModel.MED_PROCESS_SETRow _row;
        MachineModel.MED_PROCESS_SETDataTable dt;

        #endregion

        #region 属性

        public MachineModel.MED_PROCESS_SETRow Row
        {
            get { return _row; }
            set { _row = value; }
        }

        #endregion

        #region 构造函数

        public EditProcess(MachineModel.MED_PROCESS_SETRow row, MachineModel.MED_PROCESS_SETDataTable dt)
        {
            InitializeComponent();
            _row = row;
            this.dt = dt;
        }

        #endregion

        #region 事件

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_row == null)
            {
                _row = dt.NewMED_PROCESS_SETRow();
                _row.ID = Guid.NewGuid().ToString();
                dt.AddMED_PROCESS_SETRow(_row);
            }
            _row.NAME = this.txtName.Text;
            if (this.lpPOCESSID.EditValue != null)
            { _row.PROCESS_ID = this.lpPOCESSID.EditValue.ToString(); }
            else
            { _row.PROCESS_ID = ""; }
            _row.SORT_ID = this.txtSort.Text.ToString();
            _row.IS_STOP = this.cbxSTATUS.SelectedIndex.ToString();
            int result = _imachine.SaveProcessSetData(dt);
            if (result > 0)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void EditProcess_Load(object sender, EventArgs e)
        {
            ConfigModel.MED_COMMON_ITEMLISTDataTable config = this._configService.GetConfigList(string.Empty, string.Empty, "流程节点", "1");
            if (config != null && config.Rows.Count > 0)
            {

                Utility.BindLookUpEdit(lpPOCESSID, "ITEM_ID", "ITEM_NAME", (DataTable)config, "ITEM_NAME", "节点");
            }
            this.cbxSTATUS.SelectedIndex = 0;
            if (_row != null)
            {
                this.txtName.Text = _row["NAME"].ToString();
                this.lpPOCESSID.EditValue = _row["PROCESS_ID"].ToString();
                this.txtSort.Text = _row.SORT_ID;
                this.cbxSTATUS.SelectedIndex = Utility.CInt(_row.IS_STOP);
            }
        }

        #endregion
    }
}
