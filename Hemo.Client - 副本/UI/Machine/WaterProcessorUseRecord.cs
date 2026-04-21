/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：水处理机运行记录维护用户控件类
// 创建时间：2015-08-06
// 创建者：吕志强
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
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Machine;
using Hemo.Service;
using Hemo.Model;
using System.Linq;
using Hemo.IService.Dict;
using Hemo.Utilities;
using Hemo.Client.Core;

namespace Hemo.Client.UI.Machine {
    public partial class WaterProcessorUseRecord : ViewBase {
        #region 成员变量

        private IMachine machineService = ServiceManager.Instance.MachineService;

        private IStaffDict staffService = ServiceManager.Instance.StaffDictService;

        private MachineModel.MED_WATERPROCESSOR_USERECORDDataTable dtRecord = null;

        private MachineModel.MED_DIALYSIS_MACHINEDataTable dtMachine = null;

        #endregion

        #region 构造函数

        public WaterProcessorUseRecord() {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WaterProcessorUseRecord_Load(object sender, EventArgs e) {
            this.deBeginDate.DateTime = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            this.deEndDate.DateTime = DateTime.Now.AddDays(-DateTime.Now.Day + 1).AddMonths(1).AddDays(-1);
            BindLookUpEdit();
            Query();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e) {
            Query();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e) {

            if (dtMachine == null)
            {
                XtraMessageBox.Show("请选择机器编号！");
                return;
            }

            dtRecord = dtRecord ?? new MachineModel.MED_WATERPROCESSOR_USERECORDDataTable();
            //var drRecord = dtRecord.FirstOrDefault(row => row.USEDATE == DateTime.Now.Date);
            //if (drRecord != null)
            //{
            //    XtraMessageBox.Show("当前日期对应的记录已经存在！");
            //    return;
            //}

            var drRecord = dtRecord.NewMED_WATERPROCESSOR_USERECORDRow();
            drRecord.RECORD_ID = Guid.NewGuid().ToString();
            drRecord.MACHINE_ID = dtMachine.Rows[0]["MACHINE_ID"].ToString();
            drRecord.USEDATE = DateTime.Now.Date;
            drRecord.MACHINE_NAME = dtMachine.Rows[0]["MACHINE_NAME"].ToString();
            (drRecord as DataRow)["FLNAME"] = dtMachine.Rows[0]["FLNAME"].ToString();
            drRecord.MACHINE_TYPE = dtMachine.Rows[0]["TYPE"].ToString();
            drRecord.MACHINE_MODEL = dtMachine.Rows[0]["MACHINE_MODEL"].ToString();
            drRecord.SANDJAR = "1";
            drRecord.CARBONJAR = "1";
            drRecord.RESINJAR = "1";
            drRecord.ISDELETE = "0";
            drRecord.EMP_NO = HemoApplicationContext.Current.CurrentUser.EMP_NO;
            dtRecord.AddMED_WATERPROCESSOR_USERECORDRow(drRecord);
            SetColumnEditable(true);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e) {
            SetColumnEditable(true);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e) {
            var row = this.gvRecord.GetFocusedDataRow();

            if (row == null) {
                XtraMessageBox.Show("请选择要删除的行！");
                return;
            }

            if (XtraMessageBox.Show("确定要删除选中的行吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK) {
                try {
                    row = dtRecord.FindByRECORD_ID(row["RECORD_ID"].ToString());
                    row["ISDELETE"] = "1";
                    machineService.SaveWaterProcessorRecord(dtRecord);
                    dtRecord.AcceptChanges();
                    XtraMessageBox.Show("删除成功！", "提示");
                }
                catch (Exception ex) {
                    XtraMessageBox.Show("删除失败！\n" + ex.Message, "提示");
                }
            }

            Query();
            SetColumnEditable(false);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e) {
            try {
                machineService.SaveWaterProcessorRecord(dtRecord);
                dtRecord.AcceptChanges();
                XtraMessageBox.Show("保存成功！", "提示");
            }
            catch (Exception ex) {
                XtraMessageBox.Show("保存失败！\n" + ex.Message, "提示");
            }

            Query();
            SetColumnEditable(false);
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e) {

            if (dtMachine == null || dtMachine.Rows.Count == 0)
            {
                XtraMessageBox.Show("请选择水处理机！", "提示");
                return;
            }

            UseRecordReport report = new UseRecordReport();
            report.IsMachineUseRecord = false;
            report.MachineId = dtMachine[0].MACHINE_ID;
            report.ShowDialog();
        }

        private void dxSimpleButton1_Click(object sender, EventArgs e)
        {
            if (this.ParentForm.GetType() == typeof(MachineList))
            {
                this.ParentForm.Close();
            }
            else
            {

            }
        }

        private void lupMachine_EditValueChanged(object sender, EventArgs e)
        {
            Query();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 绑定下拉项
        /// </summary>
        private void BindLookUpEdit() {
            DataTable dtItem = new DataTable();
            dtItem.Columns.Add(new DataColumn("ItemNo"));
            dtItem.Columns.Add(new DataColumn("ItemTag"));

            DataRow row = dtItem.NewRow();
            row["ItemNo"] = "0";
            row["ItemTag"] = "×";
            dtItem.Rows.Add(row);

            row = dtItem.NewRow();
            row["ItemNo"] = "1";
            row["ItemTag"] = "√";
            dtItem.Rows.Add(row);

            this.repositoryItemLookUpEdit1.DataSource = dtItem;
            this.repositoryItemLookUpEdit2.DataSource = dtItem;
            this.repositoryItemLookUpEdit3.DataSource = dtItem;

            DataTable dtStaff = staffService.GetStaffDictList();
            dtStaff = Utility.GetSubTable(dtStaff, "ZYNAME='护士'", "NAME");
            this.repositoryItemLookUpEdit4.DataSource = dtStaff;

            DataTable dtMachine = machineService.GetWaterMachineListByType("水处理机品牌");
            this.lupMachine.Properties.DataSource = dtMachine;
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void Query() {
            dtMachine = this.lupMachine.EditValue != null ? machineService.GetWaterMachineById(this.lupMachine.EditValue.ToString()) : dtMachine;
            if (this.lupMachine.EditValue != null)
            {
                this.gcRecord.DataSource = dtRecord = machineService.GetWaterProcessorRecordByIdAndDate(this.lupMachine.EditValue.ToString(), this.deBeginDate.DateTime, this.deEndDate.DateTime);
            }
        }

        /// <summary>
        /// 设置列可编辑属性
        /// </summary>
        /// <param name="flag"></param>
        private void SetColumnEditable(bool flag) {
            this.gvRecord.Columns["USEDATE"].OptionsColumn.AllowEdit = flag;
            this.gvRecord.Columns["OUTWATER_PRESSURE"].OptionsColumn.AllowEdit = flag;
            this.gvRecord.Columns["INWATER_PRESSURE"].OptionsColumn.AllowEdit = flag;
            this.gvRecord.Columns["INWATER_CONDUCTIVITY"].OptionsColumn.AllowEdit = flag;
            this.gvRecord.Columns["OUTWATER_CONDUCTIVITY"].OptionsColumn.AllowEdit = flag;
            this.gvRecord.Columns["WASTEWATER_FLOW"].OptionsColumn.AllowEdit = flag;
            this.gvRecord.Columns["SANDJAR"].OptionsColumn.AllowEdit = flag;
            this.gvRecord.Columns["CARBONJAR"].OptionsColumn.AllowEdit = flag;
            this.gvRecord.Columns["RESINJAR"].OptionsColumn.AllowEdit = flag;
            this.gvRecord.Columns["RESIDUALCHLORINE_TESTRESULT"].OptionsColumn.AllowEdit = flag;
            this.gvRecord.Columns["HARDNESS_TESTRESULT"].OptionsColumn.AllowEdit = flag;
            this.gvRecord.Columns["EMP_NO"].OptionsColumn.AllowEdit = flag;
        }

        #endregion
    }
}
