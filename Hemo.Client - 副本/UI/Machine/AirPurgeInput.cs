/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：空气消毒记录编辑窗体
// 创建时间：2016-06-25
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

namespace Hemo.Client.UI.Machine
{
    public partial class AirPurgeInput :HemoBaseFrm
    {
        #region 类变量

        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private IMachine _machineService = ServiceManager.Instance.MachineService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;

        private MachineModel.MED_MACHINE_AIRPURGERow _currentRow = null;

        private MachineModel.MED_MACHINE_AIRPURGEDataTable _airDate = null;

        #endregion

        #region 属性

        public MachineModel.MED_MACHINE_AIRPURGERow CurrentRow
        {
            get { return _currentRow; }
            set { _currentRow = value; }
        }

        public MachineModel.MED_MACHINE_AIRPURGEDataTable MachineDataTable
        {
            get { return _airDate; }
            set { _airDate = value; }
        }

        public string RoomID
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        public AirPurgeInput()
        {
            InitializeComponent();

            DataTable dtStaffSict = _staffDictService.GetStaffDictList();
            if (dtStaffSict != null && dtStaffSict.Rows.Count > 0)
            {
                DataTable dtPunctureNurseList = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'", "name");
                if (dtPunctureNurseList != null && dtPunctureNurseList.Rows.Count > 0)
                {
                    BaseControlInfo.BindLookUpEdit(lopOperator, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "使用人");
                    BaseControlInfo.BindLookUpEdit(lopTrendPurger, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "使用人");
                }
            }

            ConfigModel.MED_COMMON_ITEMLISTDataTable config = this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1");
            if (config != null && config.Rows.Count > 0)
            {
                DataRow sickAreaRow = config.NewRow();
                sickAreaRow["ITEM_NAME"] = "全部";
                sickAreaRow["ITEM_ID"] = "c570d95c-76a2-4af4-893a-1357065623bf";
                sickAreaRow["ORDER_NUMBER"] = 0;
                config.Rows.InsertAt(sickAreaRow, 0);
                Hemo.Utilities.Utility.BindLookUpEdit(ediSickArea, "ITEM_ID", "ITEM_NAME", (DataTable)config, "ITEM_NAME", "区域");
            }

            //this.lopOperator.EditValue = Hemo.Client.Core.HemoApplicationContext.Current.CurrentUser.EMP_NO;
            //this.lopTrendPurger.EditValue = Hemo.Client.Core.HemoApplicationContext.Current.CurrentUser.EMP_NO;

            this.dateEdit_PurgeDate.EditValue = System.DateTime.Now.Date;

            this.timeEdit_TrendTime.EditValue = System.DateTime.Now.Date.AddHours(10);
            this.timeEdit_StaticTime.EditValue = System.DateTime.Now.Date.AddHours(20);
        }

        #endregion

        #region 事件

        private void AirPurgeInput_Load(object sender, EventArgs e)
        {
            ediSickArea.EditValue = RoomID;

            if (_currentRow == null)
            {
                if (DateTime.Now.Hour >= 10)
                {
                    this.lopTrendPurger.EditValue = Hemo.Client.Core.HemoApplicationContext.Current.CurrentUser.EMP_NO;
                }
                if (DateTime.Now.Hour >= 20)
                {
                    this.lopOperator.EditValue = Hemo.Client.Core.HemoApplicationContext.Current.CurrentUser.EMP_NO;
                }

                this.dateEdit_PurgeDate.EditValue = System.DateTime.Now.Date;
                this.txt_workstate.Text = "正常";
                this.txt_trendworkstate.Text = "正常";
                this.timeEdit_TrendTime.EditValue = System.DateTime.Now.Date.AddHours(10);
                this.timeEdit_TrendTimeEND.EditValue = System.DateTime.Now.Date.AddHours(12);
                this.timeEdit_StaticTime.EditValue = System.DateTime.Now.Date.AddHours(20);
                this.timeEdit_StaticTimeEND.EditValue = System.DateTime.Now.Date.AddHours(22);
                _airDate = new MachineModel.MED_MACHINE_AIRPURGEDataTable();
            }
            else
            {
                this.lopOperator.EditValue = _currentRow.PURGER;
                this.lopTrendPurger.EditValue = _currentRow.TRENDPURGER;

                this.dateEdit_PurgeDate.EditValue = _currentRow.PURGEDATE;
                this.dateEdit_PurgeDate.Enabled = false;
                this.timeEdit_TrendTime.EditValue = _currentRow.TRENDPURGETIME;
                this.timeEdit_TrendTimeEND.EditValue = _currentRow.TRENDPURGETIMEEND;
                this.timeEdit_StaticTime.EditValue = _currentRow.STATICPURGETIME;
                this.timeEdit_StaticTimeEND.EditValue = _currentRow.STATICPURGETIMEEND;
                this.txt_workstate.Text = _currentRow.WORKSTATE;
                this.txt_trendworkstate.Text = _currentRow.TRENDWORKSTATE;
                this.ediSickArea.EditValue = _currentRow.ROOM_ID;
                _airDate = _machineService.GetAirPurgeDataById(_currentRow.ID);
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            //检查是否提前录入记录，若提前，给予提示，不予保存
            if (this.dateEdit_PurgeDate.DateTime.Date.CompareTo(DateTime.Now.Date) == 0)
            {
                var dtConfig = _configService.GetConfigList(string.Empty, string.Empty, "质控校验", "1");
                if (this.lopTrendPurger.EditValue != null && this.lopTrendPurger.EditValue != string.Empty)
                {
                    DateTime start = Utility.CDate(DateTime.Now.ToShortDateString() + " " + "10:00:00");
                    if (!Utility.CheckRecordTimeIsValid("空气消毒记录", start, start, dtConfig))
                    {
                        XtraMessageBox.Show("早班不得早于10点录入记录！", "空气消毒记录");
                        return;
                    }
                }

                if (this.lopOperator.EditValue != null && this.lopOperator.EditValue != string.Empty)
                {
                    DateTime start = Utility.CDate(DateTime.Now.ToShortDateString() + " " + "20:00:00");
                    if (!Utility.CheckRecordTimeIsValid("空气消毒记录", start, start, dtConfig))
                    {
                        XtraMessageBox.Show("晚班不得早于20点录入记录！", "空气消毒记录");
                        return;
                    }
                }
            }
            else if (this.dateEdit_PurgeDate.DateTime.Date.CompareTo(DateTime.Now.Date) > 0)
            {
                XtraMessageBox.Show("不能提前录入记录！", "空气消毒记录");
                return;
            }

            if (_airDate == null || _airDate.Rows.Count <= 0)
            {
                var row = _airDate.NewMED_MACHINE_AIRPURGERow();
                row.ID = Guid.NewGuid().ToString();
                row.PURGEDATE = Utilities.Utility.CDate(this.dateEdit_PurgeDate.EditValue.ToString());
                row.TRENDPURGETIMEEND = Utilities.Utility.CDate(this.timeEdit_TrendTimeEND.EditValue.ToString());
                row.TRENDPURGETIME = Utilities.Utility.CDate(this.timeEdit_TrendTime.EditValue.ToString());
                row.STATICPURGETIME = Utilities.Utility.CDate(this.timeEdit_StaticTime.EditValue.ToString());
                row.STATICPURGETIMEEND = Utilities.Utility.CDate(this.timeEdit_StaticTimeEND.EditValue.ToString());
                row.WORKSTATE = this.txt_workstate.Text.Trim();
                row.TRENDWORKSTATE = this.txt_trendworkstate.Text.Trim();
                row.PURGER = this.lopOperator.EditValue.ToString();
                row.TRENDPURGER = this.lopTrendPurger.EditValue.ToString();
                row.CREATE_DATE = System.DateTime.Now;
                row.OPERATE = Hemo.Client.Core.HemoApplicationContext.Current.CurrentUser.USER_NAME;
                row.ROOM_ID = ediSickArea.EditValue.ToString();
                row.ISDELETE = "0";
                _airDate.AddMED_MACHINE_AIRPURGERow(row);

                _machineService.SaveAirPurgeData(_airDate);

            }
            else
            {
                var row = _airDate[0];
                row.PURGEDATE = Utilities.Utility.CDate(this.dateEdit_PurgeDate.EditValue.ToString());
                row.TRENDPURGETIME = Utilities.Utility.CDate(this.timeEdit_TrendTime.EditValue.ToString());
                row.STATICPURGETIME = Utilities.Utility.CDate(this.timeEdit_StaticTime.EditValue.ToString());
                row.STATICPURGETIMEEND = Utilities.Utility.CDate(this.timeEdit_StaticTimeEND.EditValue.ToString());
                row.TRENDPURGETIMEEND = Utilities.Utility.CDate(this.timeEdit_TrendTimeEND.EditValue.ToString());
                row.CREATE_DATE = System.DateTime.Now;
                row.WORKSTATE = this.txt_workstate.Text.Trim();
                row.TRENDWORKSTATE = this.txt_trendworkstate.Text.Trim();
                row.PURGER = this.lopOperator.EditValue.ToString();
                row.TRENDPURGER = this.lopTrendPurger.EditValue.ToString();
                row.OPERATE = Hemo.Client.Core.HemoApplicationContext.Current.CurrentUser.USER_NAME;
                row.ROOM_ID = ediSickArea.EditValue.ToString();

                _machineService.SaveAirPurgeData(_airDate);
            }
            if (XtraMessageBox.Show("保存成功！是否继续录入其它班次的信息？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
            {
                this.dateEdit_PurgeDate.EditValue = Utilities.Utility.CDate(this.dateEdit_PurgeDate.EditValue.ToString()).AddDays(1);
                this.txt_workstate.Text = "正常";
                this.txt_trendworkstate.Text = "正常";
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        #endregion
    }
}