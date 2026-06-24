/*----------------------------------------------------------------
// Copyright (C) 2005 (北京)医疗科技发展有限公司
// 文件名：EditMachineUseFee.cs
// 文件功能描述：血液透析设备使用费用新增、编辑记录窗体类
// 创建标识：
// 修改时间：2014-5-5
// 修改人：吕志强
// 修改描述：添加属性，更新局部逻辑
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Service;
using Hemo.IService.Machine;
using Hemo.IService.Dict;
using Hemo.Utilities;
using Hemo.Client.Core;
using Hemo.Model;

namespace Hemo.Client.UI.Machine
{
    public partial class EditMachineUseFee :HemoBaseFrm
    {
        #region 成员变量

        /// <summary>
        /// 员工相关服务
        /// </summary>
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;

        /// <summary>
        /// 血透机相关服务
        /// </summary>
        private IMachine _machineService = ServiceManager.Instance.MachineService;

        /// <summary>
        /// 唯一ID
        /// </summary>
        private string _id;

        //病区
        private string _sickAreak;

        //床位
        private string _bedNum;

        //班次
        private string _banCi;

        /// <summary>
        /// 当前行
        /// </summary>
        private MachineModel.MED_MACHINE_USEFEERow _currentUseFeeRow;

        /// <summary>
        /// 设备费用数据
        /// </summary>
        private MachineModel.MED_MACHINE_USEFEEDataTable _useFeeTable;

        #endregion

        #region 构造函数

        /// <summary>
        /// 唯一ID
        /// </summary>
        /// <param name="id"></param>
        public EditMachineUseFee(string id)
        {
            InitializeComponent();

            this._id = id;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 病区
        /// </summary>
        public string SickAreak
        {
            get { return _sickAreak; }
            set { _sickAreak = value; }
        }

        /// <summary>
        /// 床位
        /// </summary>
        public string BedNum
        {
            get { return _bedNum; }
            set { _bedNum = value; }
        }

        /// <summary>
        /// 班次
        /// </summary>
        public string BanCi
        {
            get { return _banCi; }
            set { _banCi = value; }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditMachineUseFee_Load(object sender, EventArgs e)
        {
            //自动选择当前医生
            DataTable dtStaffSict = _staffDictService.GetStaffDictList();
            if (dtStaffSict != null && dtStaffSict.Rows.Count > 0)
            {
                DataTable dtPunctureDoctorList = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'", "name");
                if (dtPunctureDoctorList != null && dtPunctureDoctorList.Rows.Count > 0)
                {
                    BaseControlInfo.BindLookUpEdit(luEdit_User, "EMP_NO", "NAME", dtPunctureDoctorList, "NAME", "使用人");
                }
            }

            this.luEdit_User.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
            this.dateEdit_MachineUseDate.EditValue = DateTime.Now;

            InitData();
        }

        /// <summary>
        /// 点击保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (!this.CheckData())
            {
                return;
            }

            if (string.IsNullOrEmpty(this._id))
            {
                //新增
                this._currentUseFeeRow = this._useFeeTable.NewMED_MACHINE_USEFEERow();
                this.FillUseFeeRow(this._currentUseFeeRow);
                this._useFeeTable.AddMED_MACHINE_USEFEERow(this._currentUseFeeRow);
            }
            else
            {
                //修改
                this.FillUseFeeRow(this._currentUseFeeRow);
            }

            try
            {
                this._machineService.SaveUseFeeData(this._useFeeTable);
            }
            catch (Exception ex)
            {
                //日志
                XtraMessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// 点击取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化界面控件数据
        /// </summary>
        private void InitData()
        {
            if (string.IsNullOrEmpty(this._id))
            {
                //新增
                this._useFeeTable = new MachineModel.MED_MACHINE_USEFEEDataTable();
            }
            else
            {
                try
                {
                    this._useFeeTable = this._machineService.GetUseFeeData(this._id);
                    if (this._useFeeTable != null && this._useFeeTable.Rows.Count > 0)
                    {
                        this._currentUseFeeRow = (MachineModel.MED_MACHINE_USEFEERow)this._useFeeTable.Rows[0];
                    }
                }
                catch (Exception ex)
                {
                    //日志
                }

                if (this._currentUseFeeRow != null)
                {
                    //修改
                    this.dateEdit_MachineUseDate.EditValue = this._currentUseFeeRow["USEDATE"] != DBNull.Value ? this._currentUseFeeRow.USEDATE : DateTime.Now;
                    this.txtEdit_UseHour.Text = this._currentUseFeeRow["USEHOUR"] != DBNull.Value ? this._currentUseFeeRow.USEHOUR.ToString() : string.Empty;
                    this.txtEdit_Valuation.Text = this._currentUseFeeRow["VALUATION"] != DBNull.Value ? this._currentUseFeeRow.VALUATION.ToString() : string.Empty;
                    this.txtEdit_ArmyManTime.Text = this._currentUseFeeRow["ARMYMANTIME"] != DBNull.Value ? this._currentUseFeeRow.ARMYMANTIME.ToString() : string.Empty;
                    this.txtEdit_Charge.Text = this._currentUseFeeRow["CHARGE"] != DBNull.Value ? this._currentUseFeeRow.CHARGE.ToString() : string.Empty;
                    this.txtEdit_LocationManTime.Text = this._currentUseFeeRow["LOCATIONMANTIME"] != DBNull.Value ? this._currentUseFeeRow.LOCATIONMANTIME.ToString() : string.Empty;
                    this.txtEdit_MachineState.Text = this._currentUseFeeRow["MACHINESTATE"] != DBNull.Value ? this._currentUseFeeRow.MACHINESTATE.ToString() : "正常";
                    this.luEdit_User.EditValue = this._currentUseFeeRow["MACHINEUSER"] != DBNull.Value ? this._currentUseFeeRow.MACHINEUSER.ToString() : "正常";
                }
            }
        }

        /// <summary>
        /// 检查数据的合法性
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            decimal d;
            if (!decimal.TryParse(this.txtEdit_UseHour.Text.Trim(), out d))
            {
                XtraMessageBox.Show("'使用h数'请填入正确的数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (string.IsNullOrEmpty(this.txtEdit_MachineState.Text.Trim()))
            {
                XtraMessageBox.Show("请输入设备工作状态！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (this.luEdit_User.EditValue == null)
            {
                XtraMessageBox.Show("请选择使用人！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 用界面控件中的数据填充数据行
        /// </summary>
        /// <param name="useFeeRow"></param>
        private void FillUseFeeRow(MachineModel.MED_MACHINE_USEFEERow useFeeRow)
        {
            if (useFeeRow == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(this._id))
            {
                useFeeRow.DIALYSIS_ROOM_ID = _sickAreak;
                useFeeRow.BED_NUMBER = _bedNum;
                useFeeRow.BANCI_ID = _banCi;
            }

            useFeeRow.ID = Guid.NewGuid().ToString();
            useFeeRow.USEDATE = (DateTime)this.dateEdit_MachineUseDate.EditValue;

            decimal useHour;
            if (decimal.TryParse(this.txtEdit_UseHour.Text.Trim(), out useHour))
            {
                useFeeRow.USEHOUR = useHour;
            }

            decimal valuation;
            if (decimal.TryParse(this.txtEdit_Valuation.Text.Trim(), out valuation))
            {
                useFeeRow.VALUATION = valuation;
            }

            decimal armyManTime;
            if (decimal.TryParse(this.txtEdit_ArmyManTime.Text.Trim(), out armyManTime))
            {
                useFeeRow.ARMYMANTIME = armyManTime;
            }

            decimal charge;
            if (decimal.TryParse(this.txtEdit_Charge.Text.Trim(), out charge))
            {
                useFeeRow.CHARGE = charge;
            }

            decimal locationManTime;
            if (decimal.TryParse(this.txtEdit_LocationManTime.Text.Trim(), out locationManTime))
            {
                useFeeRow.LOCATIONMANTIME = locationManTime;
            }

            useFeeRow.MACHINESTATE = this.txtEdit_MachineState.Text.Trim();
            useFeeRow.MACHINEUSER = this.luEdit_User.EditValue.ToString();
            useFeeRow.ISDELETE = "0";
        }

        #endregion
    }
}