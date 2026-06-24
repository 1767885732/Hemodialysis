/*----------------------------------------------------------------
// Copyright (C) 2005 (北京)医疗科技发展有限公司
// 文件名：MachineUseRecord.cs
// 文件功能描述：血液透析设备使用情况记录窗体类
// 创建标识：
// 修改时间：2014-4-16
// 修改人：吕志强
// 修改描述：更新新增、编辑功能Bug
//
// 修改时间：2014-4-22
// 修改人：吕志强
// 修改描述：新增、编辑功能改造
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.Core;
using Hemo.Client.Print;
using Hemo.Model;
using Hemo.IService.Machine;
using Hemo.Service;
using Hemo.IService.Config;
using Hemo.Utilities;
using Hemo.IService.Dict;

namespace Hemo.Client.UI.Machine
{
    public partial class MachineUseRecord : DevExpress.XtraEditors.XtraUserControl
    {
        #region 成员变量

        private MachineModel.MED_DIALYSIS_MACHINEDataTable _machineDataTable;

        private IMachine _machineService = ServiceManager.Instance.MachineService;

        private IConfig _configService = ServiceManager.Instance.ConfigService;

        private MachineModel.MED_MACHINE_USERECORDDataTable _data = null;

        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;

        private const string FULL_ITEM_ID = "c570d95c-76a2-4af4-893a-1357065623bf";

        private const string NORMAL_STATE = "正常";

        private const string DEFAULT_TIME = "5";

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public MachineUseRecord()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MachineUseRecord_Load(object sender, EventArgs e)
        {
            #region 区域

            ConfigModel.MED_COMMON_ITEMLISTDataTable config = this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1");

            if (config != null && config.Rows.Count > 0)
            {
                Hemo.Utilities.Utility.BindLookUpEdit(ediSickArea, "ITEM_ID", "ITEM_NAME", (DataTable)config, "ITEM_NAME", "区域");
                ediSickArea.EditValue = this.Tag;
            }

            #endregion

            #region 班次

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

            Utility.BindLookUpEdit(lookUpEdit_BanCI, "ITEM_ID", "ITEM_NAME", dtBANCI, "ITEM_NAME", "班次");
            this.repositoryItemLookUpEdit5.DataSource = dtBANCI;

            #endregion

            this.txtCREATE_DATE.EditValue = DateTime.Now.Date;
            BindGrid();
        }

        private void gcMachine_MouseDown(object sender, MouseEventArgs e)
        {
            var dr = gvMachine.GetFocusedDataRow() as MachineModel.MED_MACHINE_USERECORDRow;
            if (dr == null)
            {
                this.contextMenuStrip1.Hide();
                return;
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.contextMenuStrip1.Show(MousePosition);
            }
            else
            {
                this.contextMenuStrip1.Hide();
            }
        }

        private void toolRecord_Click(object sender, EventArgs e)
        {

            var dr = gvMachine.GetFocusedDataRow() as MachineModel.MED_MACHINE_USERECORDRow;
            if (dr == null)
            {
                return;
            }
            if (XtraMessageBox.Show("是否确定删除当前行数据？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            dr.ISDELETE = "1";
            _machineService.SaveMachineUserRecord(_data);
            _data.AcceptChanges();
            Query();
        }

        /// <summary>
        /// 使用记录列表行双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMachine_DoubleClick(object sender, EventArgs e)
        {
            if (this.gvMachine.DataRowCount == 0)
            {
                return;
            }

            UserDetialFrmNew frm = new UserDetialFrmNew() { IsMachineEnable = false };
            frm.CurrentRow = gvMachine.GetFocusedDataRow() as MachineModel.MED_MACHINE_USERECORDRow;
            frm.MachineDataTable = _machineDataTable;
            frm.UseRecordData = _data;
            frm.MachineDialysisRow = null;
            frm.Text = ediSickArea.Text + " " + gvMachine.GetFocusedRowCellDisplayText(this.gridColumn7) + " " + frm.CurrentRow.MACHINE_MODEL;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Query();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //begin 原来代码
            //var row = _machineDataTable.FirstOrDefault(i => i.AREA_ID == this.ediSickArea.EditValue.ToString() && i.BED_ID == this.lookUpEdit_Bed.EditValue.ToString());
            //UserDetialFrmNew frm = new UserDetialFrmNew() { IsMachineEnable = true };
            //frm.CurrentRow = null;
            //frm.MachineDataTable = _machineDataTable;
            //frm.UseRecordData = _data;
            //frm.MachineDialysisRow = row;
            //frm.Text = ediSickArea.Text + " " + gvMachine.GetFocusedRowCellDisplayText(this.gridColumn7) + row.MACHINE_MODEL;
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    Query();
            //}
            //end

            _data = _data ?? new MachineModel.MED_MACHINE_USERECORDDataTable();
            var useDate = (this.txtCREATE_DATE.EditValue != null) ? DateTime.Parse(this.txtCREATE_DATE.EditValue.ToString()).Date : DateTime.Now.Date;
            var recordRow = _data.FirstOrDefault(row => row.BANCI_ID == this.lookUpEdit_BanCI.EditValue.ToString() && row.USEDATE == useDate);

            if (recordRow != null)
            {
                XtraMessageBox.Show("所选班次和使用日期对应的记录已经存在！", "提示");
                return;
            }

            var machineRow = _machineDataTable.FirstOrDefault(row => row.AREA_ID == this.ediSickArea.EditValue.ToString() && row.BED_ID == this.lookUpEdit_Bed.EditValue.ToString());
            recordRow = _data.NewMED_MACHINE_USERECORDRow();

            recordRow.RECORD_ID = Guid.NewGuid().ToString();
            recordRow.MACHINE_ID = machineRow.MACHINE_ID;
            recordRow.MACHINE_NAME = machineRow.MACHINE_NAME;
            recordRow.MACHINE_TYPE = machineRow.TYPE;
            recordRow.MACHINE_MODEL = machineRow.MACHINE_MODEL;
            recordRow.THERAPEUTIC_PROPERTIES = machineRow.THERAPEUTIC_PROPERTIES;
            recordRow.BANCI_ID = this.lookUpEdit_BanCI.EditValue.ToString();
            recordRow.DIALYSIS_ROOM_ID = this.ediSickArea.EditValue.ToString();
            recordRow.BED_NUMBER = this.lookUpEdit_Bed.EditValue.ToString();
            recordRow.USEDATE = (this.txtCREATE_DATE.EditValue != null) ? DateTime.Parse(this.txtCREATE_DATE.EditValue.ToString()).Date : DateTime.Now.Date;
            recordRow.MACHINE_CHECK = NORMAL_STATE;
            recordRow.MACHINE_ALARM = NORMAL_STATE;
            recordRow.DEGASSING = NORMAL_STATE;
            recordRow.WORKING = NORMAL_STATE;
            recordRow.USERTIME = DEFAULT_TIME;
            recordRow.DEALWITH = string.Empty;
            recordRow.CREATEDATE = DateTime.Now;
            recordRow.ISDELETE = "0";
            recordRow.OPERATION = HemoApplicationContext.Current.CurrentUser.EMP_NO;

            _data.AddMED_MACHINE_USERECORDRow(recordRow);
            SetColumnEditable(true);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            SetColumnEditable(true);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //保存数据
                _machineService.SaveMachineUserRecord(_data);
                _data.AcceptChanges();
                AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);

                //XtraMessageBox.Show("保存成功！", "提示");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("保存失败！\n" + ex.Message, "提示");
            }

            Query();
            SetColumnEditable(false);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            UseRecordReport frm = new UseRecordReport();
            //frm.CurrentRow = _machineDataTable.FirstOrDefault(i => i.AREA_ID == this.ediSickArea.EditValue.ToString() && i.BED_ID == this.lookUpEdit_Bed.EditValue.ToString());
            //frm.AreaID = this.ediSickArea.Text.Trim();
            //frm.BedID = this.lookUpEdit_Bed.Text.Trim();
            frm.IsMachineUseRecord = true;
            frm.ShowDialog();
        }

        private void ediSickArea_EditValueChanged(object sender, EventArgs e)
        {
            string SelectStr = string.Empty;
            if (ediSickArea.EditValue == null ||
                ediSickArea.EditValue.ToString() == FULL_ITEM_ID)
            {
                SelectStr = string.Empty;
            }
            else
            {
                SelectStr = "AREA_ID = '" + this.ediSickArea.EditValue.ToString() + "'";
            }
            var dt = Utility.GetSubTable(_machineDataTable, SelectStr);
            Hemo.Utilities.Utility.BindLookUpEdit(lookUpEdit_Bed, "BED_ID", "CWNAME", dt,
                "CWNAME", "床位");

            Query();
        }

        /// <summary>
        /// 机器名称下拉框选项值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemLookUpEdit4_EditValueChanged(object sender, EventArgs e)
        {
            var selectItem = ((sender as LookUpEdit).GetSelectedDataRow() as DataRowView).Row as MachineModel.MED_DIALYSIS_MACHINERow;
            var recordRow = this.gvMachine.GetDataRow(this.gvMachine.FocusedRowHandle) as Hemo.Model.MachineModel.MED_MACHINE_USERECORDRow;

            recordRow.MACHINE_ID = selectItem.MACHINE_ID;
            recordRow.MACHINE_NAME = selectItem.MACHINE_NAME;
            recordRow.MACHINE_TYPE = selectItem.TYPE;
            recordRow.MACHINE_MODEL = selectItem.MACHINE_MODEL;
            recordRow.THERAPEUTIC_PROPERTIES = selectItem.THERAPEUTIC_PROPERTIES;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 使用记录列表绑定数据
        /// </summary>
        private void BindGrid()
        {
            _machineDataTable = this._machineService.GetMachineList();
            //透析室
            repositoryItemLookUpEdit1.DataSource = _machineDataTable;
            //床位
            repositoryItemLookUpEdit2.DataSource = _machineDataTable;
            //机器
            repositoryItemLookUpEdit4.DataSource = _machineDataTable;

            DataTable dtStaffSict = _staffDictService.GetStaffDictList();

            if (dtStaffSict != null && dtStaffSict.Rows.Count > 0)
            {
                DataTable dtPunctureNurseList = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'", "name");
                if (dtPunctureNurseList != null && dtPunctureNurseList.Rows.Count > 0)
                {
                    DataTable dtSub = new DataTable();
                    dtSub.Columns.Add(new DataColumn("EMP_NO"));
                    dtSub.Columns.Add(new DataColumn("NAME"));
                    dtSub.Columns.Add(new DataColumn("INPUT_CODE"));

                    dtPunctureNurseList.AsEnumerable().ToList().ForEach(row =>
                    {
                        DataRow subRow = dtSub.NewRow();
                        subRow["EMP_NO"] = row["EMP_NO"];
                        subRow["NAME"] = row["NAME"];
                        subRow["INPUT_CODE"] = row["INPUT_CODE"];
                        dtSub.Rows.Add(subRow);
                    });

                    //处理人
                    repositoryItemLookUpEdit3.DataSource = dtSub;
                }
            }

            #region 床位

            string SelectStr = string.Empty;
            if (ediSickArea.EditValue == null || ediSickArea.EditValue.ToString() == FULL_ITEM_ID)
            {
                SelectStr = string.Empty;
            }
            else
            {
                SelectStr = "AREA_ID = '" + this.ediSickArea.EditValue.ToString() + "'";
            }

            var dt = Utility.GetSubTable(_machineDataTable, SelectStr);
            Hemo.Utilities.Utility.BindLookUpEdit(lookUpEdit_Bed, "BED_ID", "CWNAME", dt, "CWNAME", "床位");

            #endregion

            Query();
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            string strSickArea = string.Empty;
            string bedNumber = string.Empty;
            string banchi = string.Empty;

            if (ediSickArea.EditValue == null || ediSickArea.EditValue.ToString() == FULL_ITEM_ID)
            {
                strSickArea = string.Empty;
            }
            else
            {
                strSickArea = this.ediSickArea.EditValue.ToString();
            }

            if (lookUpEdit_Bed.EditValue == null || lookUpEdit_Bed.EditValue.ToString() == FULL_ITEM_ID)
            {
                bedNumber = string.Empty;
            }
            else
            {
                bedNumber = this.lookUpEdit_Bed.EditValue.ToString();
            }

            if (lookUpEdit_BanCI.EditValue == null || lookUpEdit_BanCI.EditValue.ToString() == "0")
            {
                banchi = string.Empty;
            }
            else
            {
                banchi = this.lookUpEdit_BanCI.EditValue.ToString();
            }

            string useMouth = string.Empty;
            if (txtCREATE_DATE.Text.Length > 0)
            {
                useMouth = Utility.CDate(txtCREATE_DATE.EditValue.ToString()).ToString("yyyy-MM");
            }

            this.gcMachine.DataSource = _data = _machineService.GetUseAllDataByMachineID(strSickArea, bedNumber, banchi, useMouth);
        }

        /// <summary>
        /// 设置列可编辑属性
        /// </summary>
        /// <param name="flag"></param>
        private void SetColumnEditable(bool flag)
        {
            this.gvMachine.Columns["MACHINE_ID"].OptionsColumn.AllowEdit = flag;
            this.gvMachine.Columns["MACHINE_CHECK"].OptionsColumn.AllowEdit = flag;
            this.gvMachine.Columns["MACHINE_ALARM"].OptionsColumn.AllowEdit = flag;
            this.gvMachine.Columns["DEGASSING"].OptionsColumn.AllowEdit = flag;
            this.gvMachine.Columns["WORKING"].OptionsColumn.AllowEdit = flag;
            this.gvMachine.Columns["USERTIME"].OptionsColumn.AllowEdit = flag;
            this.gvMachine.Columns["OPERATION"].OptionsColumn.AllowEdit = flag;
            this.gvMachine.Columns["DEALWITH"].OptionsColumn.AllowEdit = flag;
        }

        #endregion
    }
}
