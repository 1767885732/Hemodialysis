/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：贵重医疗设备保养及检测登记窗体
// 创建时间：2016-05-24
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
using System.Data.Metadata.Edm;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Dict;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.Model;
using Hemo.IService.Machine;
using Hemo.IService.Config;
using Hemo.Client.Core;

namespace Hemo.Client.UI.Machine
{
    public partial class HosEquipmentEdit :HemoBaseFrm
    {
        #region 类变量

        private IMachine _machineService = ServiceManager.Instance.MachineService;
        private MachineModel.MED_MACHINE_MIXBARRELDataTable _data = null;
        private MachineModel.MED_MACHINE_MIXBARRELRow _currentRow = null;

        #endregion

        #region 属性

        public MachineModel.MED_MACHINE_MIXBARRELRow CurrentRow
        {
            get { return _currentRow; }
            set { _currentRow = value; }
        }

        #endregion

        #region 构造函数

        public HosEquipmentEdit()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void HosEquipmentEdit_Load(object sender, EventArgs e)
        {

            InitalizeData();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (IsDataValidate())
            {
                try
                {
                    this.mEDMACHINEMIXBARRELDataTableBindingSource.EndEdit();
                    this.mEDMACHINEMIXBARRELDataTableBindingSource.CurrencyManager.EndCurrentEdit();

                    var row = _data.FirstOrDefault(i => i.ID == this.lb_id.Tag.ToString()); ;
                    if (string.IsNullOrEmpty(row.ID))
                    {
                        row.ID = Guid.NewGuid().ToString();
                    }
                    row.ISDELETE = "0";
                    row.TYPE = "3";//水处理
                    row.CREATEDATE = System.DateTime.Now;
                    row.CREATER = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                    _machineService.SaveMixBarrelData(_data);
                    if (XtraMessageBox.Show("保存成功！是否继续录入信息？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
                    {
                        this.mEDMACHINEMIXBARRELDataTableBindingSource.AddNew();
                        this.lb_id.Tag = Guid.NewGuid().ToString();
                        this.txt_eventext.Text = string.Empty;
                        this.txt_result.Text = string.Empty;
                        this.dateEdit_Disinfect.DateTime = row.DISINFECTDATE.AddDays(1);
                        this.dateEdit_check.DateTime = row.DISINFECTDATE.AddDays(1);
                        this.dateEdit_Disinfect.Enabled = true;
                    }
                    else
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    if (ex.Message == "ORA-00001: unique constraint (MEDHEMO.PK_MIXBARREL) violated")
                    {
                        error = "此数据已存在请重新其它日期数据！";
                    }

                    XtraMessageBox.Show(error, "提示");
                    this.DialogResult = DialogResult.Cancel;
                }
            }
        }

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初使化数据
        /// </summary>
        private void InitalizeData()
        {
            this.Enabled = false;

            _data = new MachineModel.MED_MACHINE_MIXBARRELDataTable();
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {

                    if (CurrentRow != null)
                    {
                        _data.LoadDataRow(_currentRow.ItemArray, true);
                    }
                    else
                    {
                        _data = new MachineModel.MED_MACHINE_MIXBARRELDataTable();
                    }
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.mEDMACHINEMIXBARRELDataTableBindingSource.DataSource = _data;

                    if (CurrentRow == null)
                    {
                        this.mEDMACHINEMIXBARRELDataTableBindingSource.AddNew();
                        this.lb_id.Tag = Guid.NewGuid().ToString();
                        this.txt_eventext.Text = string.Empty;
                        this.txt_result.Text = string.Empty;
                        this.dateEdit_Disinfect.DateTime = DateTime.Now.Date;
                        this.dateEdit_check.DateTime = DateTime.Now.Date;
                    }
                    else
                    {
                        this.dateEdit_Disinfect.Enabled = false;

                    }
                    this.dateEdit_Disinfect.Focus();
                    this.Enabled = true;

                };
                worker.RunWorkerAsync();
            }
        }

        private bool IsDataValidate()
        {
            if (dateEdit_Disinfect.Text.Length <= 0)
            {
                this.dateEdit_Disinfect.Focus();
                this.errorProvider.SetError(dateEdit_Disinfect, "请选择启用日期！");
                return false;
            }
            if (string.IsNullOrEmpty(this.txt_eventext.Text.Trim()))
            {
                this.txt_eventext.Focus();
                this.errorProvider.SetError(txt_eventext, "请录入维修的内容！");
                return false;
            }
            if (string.IsNullOrEmpty(this.txt_machine.Text.Trim()))
            {
                this.txt_eventext.Focus();
                this.errorProvider.SetError(txt_eventext, "请录入贵重物品名称！");
                return false;
            }
            if (string.IsNullOrEmpty(this.txt_result.Text.Trim()))
            {
                this.txt_result.Focus();
                this.errorProvider.SetError(txt_result, "请录入保养的内容！");
                return false;
            }
            if (this.dateEdit_check.Text.Length <= 0)
            {
                this.dateEdit_Disinfect.Focus();
                this.errorProvider.SetError(dateEdit_Disinfect, "请选择效验日期！");
                return false;
            }

            return true;
        }

        #endregion 
    }
}