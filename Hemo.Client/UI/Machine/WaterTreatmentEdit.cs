/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：水处理消毒记录登记窗体类
// 创建时间：2015-08-07
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
    public partial class WaterTreatmentEdit :HemoBaseFrm
    {
        #region 类变量

        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private IMachine _machineService = ServiceManager.Instance.MachineService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
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

        public WaterTreatmentEdit()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void WaterTreatmentMachineEdit_Load(object sender, EventArgs e)
        {
            DataTable dtStaffSict = _staffDictService.GetStaffDictList();
            if (dtStaffSict != null && dtStaffSict.Rows.Count > 0)
            {
                DataTable dtPunctureNurseList = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'", "name");
                if (dtPunctureNurseList != null && dtPunctureNurseList.Rows.Count > 0)
                {
                    BaseControlInfo.BindLookUpEdit(lop_sign, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "使用人");
                }
            }
            ConfigModel.MED_COMMON_ITEMLISTDataTable config = this._configService.GetConfigList(string.Empty,
                  string.Empty, "消毒机器", "1");

            if (config != null && config.Rows.Count > 0)
            {
                Hemo.Utilities.Utility.BindLookUpEdit(lop_machine, "ITEM_ID", "ITEM_NAME", (DataTable)config,
                    "ITEM_NAME", "消毒机器");
            }
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
                    row.TYPE = "2";//水处理
                    row.CREATEDATE = System.DateTime.Now;
                    row.CREATER = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                    _machineService.SaveMixBarrelData(_data);
                    if (XtraMessageBox.Show("保存成功！是否继续录入信息？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
                    {
                        this.mEDMACHINEMIXBARRELDataTableBindingSource.AddNew();
                        this.lb_id.Tag = Guid.NewGuid().ToString();
                        this.txt_eventext.Text = "取0.2ml标准试剂待用。取0.2ml反渗水和0.6ml无热源水均匀混合稀释，取稀释后反渗水0.2ml与0.2ml标准试剂混合，放置37℃左右环境下10-20分钟，观察有无凝胶生成";
                        this.txt_result.Text = "试管内无凝胶生成";
                        this.lop_sign.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                        this.dateEdit_Disinfect.DateTime = row.DISINFECTDATE.AddDays(1);
                        this.dateEdit_Disinfect.Enabled = true;
                        this.lop_machine.Enabled = true;
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
                        this.txt_eventext.Text = "取0.2ml标准试剂待用。取0.2ml反渗水和0.6ml无热源水均匀混合稀释，取稀释后反渗水0.2ml与0.2ml标准试剂混合，放置37℃左右环境下10-20分钟，观察有无凝胶生成";
                        this.txt_result.Text = "试管内无凝胶生成";
                        this.lop_sign.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                        this.dateEdit_Disinfect.DateTime = DateTime.Now.Date;
                    }
                    else
                    {
                        this.dateEdit_Disinfect.Enabled = false;
                        this.lop_machine.Enabled = false;
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
                this.errorProvider.SetError(dateEdit_Disinfect, "请选择桶消毒日期！");
                return false;
            }
            if (string.IsNullOrEmpty(this.txt_eventext.Text.Trim()))
            {
                this.txt_eventext.Focus();
                this.errorProvider.SetError(txt_eventext, "请录入事件内容！");
                return false;
            }
            if (string.IsNullOrEmpty(this.txt_result.Text.Trim()))
            {
                this.txt_result.Focus();
                this.errorProvider.SetError(txt_result, "请录入结果内容！");
                return false;
            }
            if (string.IsNullOrEmpty(this.lop_sign.Text))
            {
                this.lop_sign.Focus();
                this.errorProvider.SetError(lop_sign, "请选择签名！");
                return false;
            }
            if (string.IsNullOrEmpty(this.lop_machine.Text))
            {
                this.lop_machine.Focus();
                this.errorProvider.SetError(this.lop_machine, "请选择机器！");
                return false;
            }

            return true;
        }

        #endregion 
    }
}