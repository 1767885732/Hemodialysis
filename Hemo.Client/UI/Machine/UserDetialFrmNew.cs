/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：血液透析设备使用情况记录维护窗体类
// 创建时间：2014-04-05
// 创建者：刘超
//  
// 修改时间：2014-04-16
// 修改人：吕志强
// 修改描述：更新新增、编辑功能Bug
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Utilities;
using Hemo.IService.Dict;
using Hemo.Service;
using Hemo.Model;
using Hemo.IService.Machine;
using Hemo.Client.Core;

namespace Hemo.Client.UI.Machine
{
    public partial class UserDetialFrmNew :HemoBaseFrm
    {
        #region 成员变量

        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;

        private IMachine _machineService = ServiceManager.Instance.MachineService;

        private MachineModel.MED_MACHINE_USERECORDDataTable _data = null;

        private MachineModel.MED_MACHINE_USERECORDRow _currentRow = null;

        private MachineModel.MED_DIALYSIS_MACHINEDataTable _machineDataTable;

        private MachineModel.MED_DIALYSIS_MACHINERow _machineDialysisRow = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserDetialFrmNew()
        {
            InitializeComponent();
        }

        #endregion

        #region 属性

        public MachineModel.MED_MACHINE_USERECORDDataTable UseRecordData
        {
            get { return _data; }
            set { _data = value; }
        }

        public MachineModel.MED_MACHINE_USERECORDRow CurrentRow
        {
            get { return _currentRow; }
            set { _currentRow = value; }
        }

        public MachineModel.MED_DIALYSIS_MACHINEDataTable MachineDataTable
        {
            get { return _machineDataTable; }
            set { _machineDataTable = value; }
        }

        public MachineModel.MED_DIALYSIS_MACHINERow MachineDialysisRow
        {
            get
            {
                return _machineDialysisRow;
            }
            set
            {
                _machineDialysisRow = value;
            }
        }

        public bool IsMachineEnable { get; set; }

        #endregion

        #region 事件

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (_data == null)
            {
                _data = new MachineModel.MED_MACHINE_USERECORDDataTable();
            }

            if (_data.Rows.Count == 0)
            {
                var row = _data.NewMED_MACHINE_USERECORDRow();
                var selectRow = (this.lookUpEdit_machine.GetSelectedDataRow() as DataRowView).Row as MachineModel.MED_DIALYSIS_MACHINERow;

                row.RECORD_ID = Guid.NewGuid().ToString();
                row.MACHINE_ID = selectRow.MACHINE_ID;
                row.MACHINE_NAME = selectRow.MACHINE_NAME;
                row.MACHINE_TYPE = selectRow.TYPE;
                row.MACHINE_MODEL = selectRow.MACHINE_MODEL;
                row.THERAPEUTIC_PROPERTIES = selectRow.THERAPEUTIC_PROPERTIES;
                row.DIALYSIS_ROOM_ID = selectRow.AREA_ID;
                row.BED_NUMBER = selectRow.BED_ID;

                row.BANCI_ID = this.lookUpEdit_BanCi.EditValue.ToString();
                row.MACHINE_CHECK = this.MACHINE_CHECK.Text.Trim();
                row.MACHINE_ALARM = this.MACHINE_ALARM.Text.Trim();
                row.DEGASSING = this.DEGASSING.Text.Trim();
                row.WORKING = this.WORKING.Text.Trim();
                row.OPERATION = this.lopOperator.EditValue.ToString();
                row.USERTIME = this.user_time.EditValue.ToString();
                row.USEDATE = DateTime.Parse(this.txtCREATE_DATE.EditValue.ToString()).Date;
                row.DEALWITH = this.DEALWITH.Text.Trim();
                row.CREATEDATE = System.DateTime.Now;
                row.ISDELETE = "0";
                _data.AddMED_MACHINE_USERECORDRow(row);
                _machineService.SaveMachineUserRecord(_data);
            }
            else
            {
                var HaveRow = _data.FirstOrDefault(i => i.BANCI_ID == this.lookUpEdit_BanCi.EditValue.ToString() && i.USEDATE == Utility.CDate(this.txtCREATE_DATE.EditValue.ToString()));

                if (HaveRow == null)
                {
                    var row = _data.NewMED_MACHINE_USERECORDRow();

                    if (IsMachineEnable)
                    {
                        var selectRow = (this.lookUpEdit_machine.GetSelectedDataRow() as DataRowView).Row as MachineModel.MED_DIALYSIS_MACHINERow;

                        row.RECORD_ID = Guid.NewGuid().ToString();
                        row.MACHINE_ID = selectRow.MACHINE_ID;
                        row.MACHINE_NAME = selectRow.MACHINE_NAME;
                        row.MACHINE_TYPE = selectRow.TYPE;
                        row.MACHINE_MODEL = selectRow.MACHINE_MODEL;
                        row.THERAPEUTIC_PROPERTIES = selectRow.THERAPEUTIC_PROPERTIES;
                        row.DIALYSIS_ROOM_ID = selectRow.AREA_ID;
                        row.BED_NUMBER = selectRow.BED_ID;
                    }
                    else
                    {
                        row.MACHINE_ID = _currentRow.MACHINE_ID;
                        row.MACHINE_NAME = _currentRow.MACHINE_NAME;
                        row.MACHINE_TYPE = _currentRow.MACHINE_TYPE;
                        row.MACHINE_MODEL = _currentRow.MACHINE_MODEL;
                        row.THERAPEUTIC_PROPERTIES = _currentRow.THERAPEUTIC_PROPERTIES;
                        row.DIALYSIS_ROOM_ID = _currentRow.DIALYSIS_ROOM_ID;
                        row.BED_NUMBER = _currentRow.BED_NUMBER;
                    }

                    row.BANCI_ID = this.lookUpEdit_BanCi.EditValue.ToString();
                    row.MACHINE_CHECK = this.MACHINE_CHECK.Text.Trim();
                    row.MACHINE_ALARM = this.MACHINE_ALARM.Text.Trim();
                    row.DEGASSING = this.DEGASSING.Text.Trim();
                    row.WORKING = this.WORKING.Text.Trim();
                    row.OPERATION = this.lopOperator.EditValue.ToString();
                    row.USERTIME = this.user_time.EditValue.ToString();
                    row.USEDATE = DateTime.Parse(this.txtCREATE_DATE.EditValue.ToString()).Date;
                    row.DEALWITH = this.DEALWITH.Text.Trim();
                    row.ISDELETE = "0";
                    row.CREATEDATE = System.DateTime.Now;
                    _data.AddMED_MACHINE_USERECORDRow(row);
                    _machineService.SaveMachineUserRecord(_data);
                }
                else
                {
                    HaveRow.MACHINE_CHECK = this.MACHINE_CHECK.Text.Trim();
                    HaveRow.MACHINE_ALARM = this.MACHINE_ALARM.Text.Trim();
                    HaveRow.DEGASSING = this.DEGASSING.Text.Trim();
                    HaveRow.WORKING = this.WORKING.Text.Trim();
                    HaveRow.OPERATION = this.lopOperator.EditValue.ToString();
                    HaveRow.USERTIME = this.user_time.EditValue.ToString();
                    HaveRow.USEDATE = DateTime.Parse(this.txtCREATE_DATE.EditValue.ToString()).Date;
                    HaveRow.DEALWITH = this.DEALWITH.Text.Trim();
                    _machineService.SaveMachineUserRecord(_data);
                }
            }

            _data.AcceptChanges();

            if (XtraMessageBox.Show("保存成功！是否继续录入其它班次的信息？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
            {
                if (int.Parse(this.lookUpEdit_BanCi.EditValue.ToString()) < 4)
                {
                    this.lookUpEdit_BanCi.EditValue = Convert.ToString(int.Parse(this.lookUpEdit_BanCi.EditValue.ToString()) + 1);
                }
                else
                {
                    this.txtCREATE_DATE.EditValue = DateTime.Parse(this.txtCREATE_DATE.EditValue.ToString()).Date.AddDays(1);
                    this.lookUpEdit_BanCi.EditValue = "1";
                }
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserDetialFrmNew_Load(object sender, EventArgs e)
        {
            LoadData();
            this.lookUpEdit_machine.Enabled = IsMachineEnable;
        }

        private void lookUpEdit_BanCi_EditValueChanged(object sender, EventArgs e)
        {
            this.MACHINE_CHECK.Text = string.Empty;
            this.MACHINE_ALARM.Text = string.Empty;
            this.WORKING.Text = string.Empty;
            this.DEGASSING.Text = string.Empty;
            this.DEALWITH.Text = string.Empty;
            this.lopOperator.EditValue = string.Empty;
            this.user_time.EditValue = 5;
            MACHINE_ALARM.Text = MACHINE_CHECK.Text = WORKING.Text = DEGASSING.Text = "正常";
        }

        private void txtCREATE_DATE_EditValueChanged(object sender, EventArgs e)
        {
            this.MACHINE_CHECK.Text = string.Empty;
            this.WORKING.Text = string.Empty;
            this.DEGASSING.Text = string.Empty;
            this.DEALWITH.Text = string.Empty;
            this.lopOperator.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
            this.MACHINE_ALARM.Text = string.Empty;
            this.user_time.EditValue = 5;
            MACHINE_ALARM.Text = MACHINE_CHECK.Text = WORKING.Text = DEGASSING.Text = "正常";
        }

        private void lookUpEdit_machine_EditValueChanged(object sender, EventArgs e)
        {
            this.MACHINE_CHECK.Text = string.Empty;
            this.WORKING.Text = string.Empty;
            this.DEGASSING.Text = string.Empty;
            this.DEALWITH.Text = string.Empty;
            this.lopOperator.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO; ;
            this.user_time.EditValue = 5;
            this.MACHINE_ALARM.Text = string.Empty;
            MACHINE_ALARM.Text = MACHINE_CHECK.Text = WORKING.Text = DEGASSING.Text = "正常";
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            #region 绑定数据

            this.lookUpEdit_machine.Properties.DataSource = _machineDataTable;

            //责任护士、穿刺护士、责任医生下拉框绑定数据
            DataTable dtStaffSict = _staffDictService.GetStaffDictList();
            if (dtStaffSict != null && dtStaffSict.Rows.Count > 0)
            {
                DataTable dtPunctureNurseList = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'", "name");
                if (dtPunctureNurseList != null && dtPunctureNurseList.Rows.Count > 0)
                {
                    BaseControlInfo.BindLookUpEdit(lopOperator, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "使用人");
                }
            }

            //班次下拉框绑定数据
            DataTable dtBANCI = new DataTable();
            dtBANCI.Columns.Add(new DataColumn("ITEM_ID"));
            dtBANCI.Columns.Add(new DataColumn("ITEM_NAME"));

            DataRow row = dtBANCI.NewRow();
            row["ITEM_ID"] = "1";
            row["ITEM_NAME"] = "上午";
            dtBANCI.Rows.Add(row);

            row = dtBANCI.NewRow();
            row["ITEM_ID"] = "2";
            row["ITEM_NAME"] = "下午";
            dtBANCI.Rows.Add(row);

            //row = dtBANCI.NewRow();
            //row["ITEM_ID"] = "3";
            //row["ITEM_NAME"] = "晚班";
            //dtBANCI.Rows.Add(row);

            row = dtBANCI.NewRow();
            row["ITEM_ID"] = "4";
            row["ITEM_NAME"] = "急诊";
            dtBANCI.Rows.Add(row);

            Utility.BindLookUpEdit(lookUpEdit_BanCi, "ITEM_ID", "ITEM_NAME", dtBANCI, "ITEM_NAME", "班次");

            #endregion

            #region 赋值

            if (_currentRow != null)
            {
                this.lookUpEdit_machine.EditValue = _currentRow.MACHINE_ID;
                MACHINE_ALARM.Text = _currentRow.MACHINE_ALARM;
                MACHINE_CHECK.Text = _currentRow.MACHINE_CHECK;
                WORKING.Text = _currentRow.WORKING;
                DEGASSING.Text = _currentRow.DEGASSING;
                this.txtCREATE_DATE.EditValue = _currentRow.USEDATE;
                this.lookUpEdit_BanCi.EditValue = _currentRow.BANCI_ID;
                this.user_time.Text = _currentRow.USERTIME;
                this.DEALWITH.Text = _currentRow.DEALWITH;
                this.lopOperator.EditValue = _currentRow.OPERATION;
            }
            else
            {
                this.lookUpEdit_machine.EditValue = MachineDialysisRow.MACHINE_ID;
                MACHINE_ALARM.Text = MACHINE_CHECK.Text = WORKING.Text = DEGASSING.Text = "正常";
                this.lookUpEdit_BanCi.EditValue = GetBanChi();
                this.txtCREATE_DATE.EditValue = System.DateTime.Now.Date;
                this.user_time.EditValue = 5;
                lopOperator.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
            }

            #endregion
        }

        private int GetBanChi()
        {
            var hour = System.DateTime.Now.Hour;
            if (hour >= 8 && hour <= 13)
            {
                return 1;
            }
            else if (hour > 13 && hour <= 19)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        #endregion
    }
}