/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：设备维修登记窗体
// 创建时间：2015-07-20
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
using DevExpress.Charts.Native;
using DevExpress.Data.Mask;
using Hemo.Model;
using Hemo.IService.Dict;
using Hemo.IService.Machine;
using Hemo.Service;
using DevExpress.XtraEditors;
using Hemo.IService.Config;

namespace Hemo.Client.UI.Machine
{
    public partial class RepairRecord : HemoBaseFrm
    {
        #region 类变量

        private MachineModel.MED_DIALYSIS_MACHINEDataTable _machineList = null;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _areaRoomDt;
        private MachineModel.MED_MACHINE_REPAIRSITUATIONDataTable _data = null;
        private MachineModel.MED_MACHINE_REPAIRSITUATIONRow _currentRow = null;
        private IMachine _machineService = ServiceManager.Instance.MachineService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;

        #endregion

        #region 属性

        public MachineModel.MED_MACHINE_REPAIRSITUATIONRow CurrentRow
        {
            get
            {
                return _currentRow;
            }
            set
            {
                _currentRow = value;
            }
        }

        #endregion

        #region 构造函数

        public RepairRecord()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (!Valiate())
                return;

            this.HemoDateBindings.EndEdit();
            this.HemoDateBindings.CurrencyManager.EndCurrentEdit();

            var row = _data[0];
            row.ISDELETE = "0";
            row.CREATETIME = System.DateTime.Now;


            //保存数据
            if (_machineService.SaveRepairDatas(_data) >= 1)
            {
                if (XtraMessageBox.Show("保存成功！是否继续维修记录的信息？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
                {
                    this.txt_useTime.EditValue = Convert.ToDateTime(txt_useTime.EditValue.ToString()).AddDays(1).Date;
                    _currentRow = null;
                }
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
            else
            {
                XtraMessageBox.Show("保存失败！");
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }

        private void btn_cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RepairRecord_Load(object sender, EventArgs e)
        {
            InzationDate();
        }

        private void txtCREATE_DATE_EditValueChanged(object sender, EventArgs e)
        {
            if (_currentRow == null)
            {
                this.txt_engineer.Text = this.txt_SYMPTOM.Text = this.txt_REPAIRSITUATION.Text = string.Empty;
            }
        }

        private void customGridLookUpEdit_AreaRoom_EditValueChanged(object sender, EventArgs e)
        {
            if (this.customGridLookUpEdit_AreaRoom.EditValue != null)
            {
                this.customGridLookUpEdit_Pname.Properties.DataSource = this._machineList.Where(i => i.AREA_ID == this.customGridLookUpEdit_AreaRoom.EditValue.ToString()).ToList();
            }
        }

        private void customGridLookUpEdit_Pname_EditValueChanged(object sender, EventArgs e)
        {
            var row = this.customGridLookUpEdit_Pname.GetSelectedDataRow() as MachineModel.MED_DIALYSIS_MACHINERow;
            if (row != null)
            {
                this.txt_pnumber.Text = row.PHONE_NO;
                this.txt_Brand.Text = row.FLNAME;
                this.txt_seriesnum.Text = row.MACHINE_SERIAL_NO;
                this.txt_useTime.EditValue = row.SETUP_DATE;
            }
        }

        private void radioGroup3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void labelControl14_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region 方法

        private bool Valiate()
        {
            bool result = true;
            if (this.txt_engineer.Text.Trim().Length <= 0)
            {
                result = false;
                this.txt_engineer.Focus();
                errorProvider.SetError(txt_engineer, "请输入工程师名称。");
            }

            if (this.txt_SYMPTOM.Text.Trim().Length <= 0)
            {
                result = false;
                this.txt_SYMPTOM.Focus();
                errorProvider.SetError(txt_SYMPTOM, "请输入维修内容。");

            }

            if (this.txt_REPAIRSITUATION.Text.Trim().Length <= 0)
            {
                result = false;
                this.txt_REPAIRSITUATION.Focus();
                errorProvider.SetError(txt_REPAIRSITUATION, "请输入维修情况。");

            }
            return result;
        }

        public void InzationDate()
        {
            this.Enabled = false;
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                _data = new MachineModel.MED_MACHINE_REPAIRSITUATIONDataTable();
                _machineList = new MachineModel.MED_DIALYSIS_MACHINEDataTable();
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    if (_currentRow != null)
                    {
                        _data.ImportRow(_currentRow);

                    }
                    else
                    {
                        _data = new MachineModel.MED_MACHINE_REPAIRSITUATIONDataTable();
                    }
                    this._areaRoomDt = this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1");

                    this._machineList = _machineService.GetMachineList();//GetMachineList
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.customGridLookUpEdit_AreaRoom.Properties.DataSource = this._areaRoomDt;
                    this.customGridLookUpEdit_Pname.Properties.DataSource = this._machineList; ;
                    this.HemoDateBindings.DataSource = _data;
                    if (_currentRow == null)
                    {
                        this.HemoDateBindings.AddNew();
                        this.txt_ID.Text = Guid.NewGuid().ToString();
                        this.txt_useTime.EditValue = System.DateTime.Now;
                    }
                    else
                    {

                    }

                    this.Enabled = true;
                };
                worker.RunWorkerAsync();
            }
        }

        #endregion
    }
}
