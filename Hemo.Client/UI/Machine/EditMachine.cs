/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：血透机信息维护窗体
// 创建时间：2016-07-05
// 创建者：吕志强
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Config;
using Hemo.IService.Machine;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;

namespace Hemo.Client.UI.Machine
{
    public partial class EditMachine : HemoBaseFrm
    {
        #region 变量

        private MachineModel.MED_DIALYSIS_MACHINEDataTable _machineDataTable;

        private MachineModel.MED_DIALYSIS_MACHINERow _machineDictRow;

        private bool isMachine;

        private IConfig _configService = ServiceManager.Instance.ConfigService;

        private IMachine _machineService = ServiceManager.Instance.MachineService;

        #endregion

        #region 属性

        /// <summary>
        /// 是否血透机
        /// </summary>
        public bool IsMachine
        {
            get { return isMachine; }
            set { isMachine = value; }
        }

        #endregion

        #region 构造函数

        public EditMachine(MachineModel.MED_DIALYSIS_MACHINEDataTable machineDataTable, MachineModel.MED_DIALYSIS_MACHINERow machineDictRow)
        {
            InitializeComponent();

            _machineDataTable = machineDataTable;
            _machineDictRow = machineDictRow;
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditMachine_Load(object sender, EventArgs e)
        {
            InitializeControls();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.IsDataValidate())
                return;

            if (_machineDictRow == null) //新增
            {
                _machineDictRow = _machineDataTable.NewMED_DIALYSIS_MACHINERow();

                _machineDictRow.MACHINE_ID = Guid.NewGuid().ToString();
                //_machineDictRow.MACHINE_SERIAL_NO = Convert.ToString(_machineService.GetMachineList().Rows.Count + 1);

                _machineDataTable.AddMED_DIALYSIS_MACHINERow(_machineDictRow);
            }

            _machineDictRow.MACHINE_NAME = this.txtMACHINE_NAME.Text;
            _machineDictRow.TYPE = this.cbxTYPE.EditValue.ToString();
            _machineDictRow.MACHINE_MODEL = this.txtMACHINE_MODEL.Text;
            _machineDictRow.THERAPEUTIC_PROPERTIES = this.cbxTHERAPEUTIC_PROPERTIES.EditValue.ToString();
            _machineDictRow.OTHER_THERAPEUTIC = this.txtOTHER_THERAPEUTIC.Text;
            _machineDictRow.SETUP_DATE = this.deSETUP_DATE.DateTime;
            _machineDictRow.SERVICE_ENGINEER = this.txtSERVICE_ENGINEER.Text;
            _machineDictRow.PHONE_NO = this.txtPHONE_NO.Text;

            _machineService.SaveMachineInfo(_machineDataTable);
            AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);

            //  XtraMessageBox.Show("保存成功！");

            this.DialogResult = DialogResult.Yes;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitializeControls()
        {
            this.Text = isMachine ? "维护血透机信息" : "维护水处理机信息";
            this.lab1.Text = isMachine ? "透析机编号" : "水处理机编号";
            this.lab2.Text = isMachine ? "透析机分类" : "水处理机分类";
            this.lab3.Text = isMachine ? "透析机型号" : "水处理机型号";

            string type = isMachine ? "血透机品牌" : "水处理机品牌";
            Utility.BindLookUpEdit(this.cbxTYPE, "ITEM_ID", "ITEM_NAME", _configService.GetConfigList(string.Empty, string.Empty, type, "1"), "ITEM_NAME", type);

            if (_machineDictRow != null) //修改
            {
                this.txtMACHINE_NAME.Text = _machineDictRow.MACHINE_NAME;
                this.cbxTYPE.EditValue = _machineDictRow.TYPE;
                this.txtMACHINE_MODEL.Text = _machineDictRow.MACHINE_MODEL;
                this.cbxTHERAPEUTIC_PROPERTIES.EditValue = _machineDictRow.THERAPEUTIC_PROPERTIES;
                this.txtOTHER_THERAPEUTIC.Text = _machineDictRow.OTHER_THERAPEUTIC;
                this.deSETUP_DATE.DateTime = _machineDictRow.SETUP_DATE;
                this.txtSERVICE_ENGINEER.Text = _machineDictRow.SERVICE_ENGINEER;
                this.txtPHONE_NO.Text = _machineDictRow.PHONE_NO;
                this.txtMACHINE_SERIAL_NO.Value = Utility.CDecimal(_machineDictRow.MACHINE_SERIAL_NO);
            }
            else
                this.txtMACHINE_SERIAL_NO.Value = Utility.CDecimal(Convert.ToString(_machineService.GetMachineList().Rows.Count + 1));
        }

        private bool IsDataValidate()
        {
            this.errorProvider.SetError(this.txtMACHINE_NAME, string.Empty);
            this.errorProvider.SetError(this.cbxTYPE, string.Empty);
            string msg = string.Empty;

            if (string.IsNullOrEmpty(this.txtMACHINE_NAME.Text))
            {
                msg = isMachine ? "透析机编号不能为空！" : "水处理机编号不能为空！";
                this.txtMACHINE_NAME.Focus();
                this.errorProvider.SetError(this.txtMACHINE_NAME, msg);

                return false;
            }

            if (string.IsNullOrEmpty(this.cbxTYPE.Text))
            {
                msg = isMachine ? "请选择透析机分类！" : "请选择水处理机分类！";
                this.cbxTYPE.Focus();
                this.errorProvider.SetError(this.cbxTYPE, msg);

                return false;
            }
            if (string.IsNullOrEmpty(this.txtMACHINE_SERIAL_NO.Text))
            {
                this.txtMACHINE_SERIAL_NO.Focus();
                this.errorProvider.SetError(this.txtMACHINE_SERIAL_NO, "输入排序号");

                return false;
            }
            if (string.IsNullOrEmpty(this.cbxTHERAPEUTIC_PROPERTIES.Text))
            {
                this.cbxTHERAPEUTIC_PROPERTIES.Focus();
                this.errorProvider.SetError(this.cbxTHERAPEUTIC_PROPERTIES, "请选择治疗特征！");

                return false;
            }

            return true;
        }

        #endregion
    }
}
