/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：设备维修记录管理用户控件类
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
using DevExpress.Xpf.Grid;
using Hemo.Model;
using Hemo.Service;
using DevExpress.XtraEditors;
using Hemo.IService.Dict;
using Hemo.Utilities;

namespace Hemo.Client.UI.Machine
{
    public partial class RepairRecordManager : ViewBase
    {
        #region 类变量

        private Hemo.IService.Machine.IMachine _machineService = ServiceManager.Instance.MachineService;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private MachineModel.MED_MACHINE_REPAIRSITUATIONDataTable _data = null;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public RepairRecordManager()
        {
            InitializeComponent();

            DateTime dt = DateTime.Now; //当前时间
            DateTime startWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d"))); //本周周一
            DateTime endWeek = startWeek.AddDays(6); //本周周五

            this.beginTime.EditValue = startWeek;
            this.endTime.EditValue = endWeek;
        }

        #endregion

        #region 事件

        private void RepairRecordManager_Load(object sender, EventArgs e)
        {
            InitalizeData();
        }

        //查询
        private void btn_Query_Click(object sender, EventArgs e)
        {
            InitalizeData();
        }
        //打印
        private void btn_Print_Click(object sender, EventArgs e)
        {
            MachineReportFrm frm = new MachineReportFrm(Utilities.Utility.CDate(this.beginTime.EditValue.ToString()), Utilities.Utility.CDate(this.endTime.EditValue.ToString()));
            frm.ShowDialog();
        }
        //新增记录
        private void btn_Entering_Click(object sender, EventArgs e)
        {
            RepairRecord frm = new RepairRecord();
            frm.CurrentRow = null;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                InitalizeData();
            }

        }

        //显示右击删除
        private void gcMachine_MouseDown(object sender, MouseEventArgs e)
        {
            var row = gvMachine.GetFocusedDataRow() as MachineModel.MED_MACHINE_REPAIRSITUATIONRow;
            if (row == null)
            {
                return;
            }
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(MousePosition);
            }
        }
        //修改
        private void gvMachine_DoubleClick(object sender, EventArgs e)
        {
            var row = gvMachine.GetFocusedDataRow() as MachineModel.MED_MACHINE_REPAIRSITUATIONRow;
            if (row == null)
            {
                return;
            }

            RepairRecord frm = new RepairRecord();
            frm.CurrentRow = row;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                InitalizeData();
            }
        }
        //删除
        private void toolDelete_Click(object sender, EventArgs e)
        {
            var row = gvMachine.GetFocusedDataRow() as MachineModel.MED_MACHINE_REPAIRSITUATIONRow;
            if (row == null)
            {
                return;
            }
            if (XtraMessageBox.Show("是否确定删除当前行数据？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.No)
                return;

            row.ISDELETE = "1";
            _machineService.SaveRepairDatas(_data);
            _data.AcceptChanges();
            InitalizeData();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            {
                var row = gvMachine.GetFocusedDataRow() as MachineModel.MED_MACHINE_REPAIRSITUATIONRow;
                if (row == null)
                {
                    return;
                }
                if (XtraMessageBox.Show("是否确定删除当前行数据？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.No)
                    return;

                row.ISDELETE = "1";
                _machineService.SaveRepairDatas(_data);
                _data.AcceptChanges();
                InitalizeData();
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitalizeData()
        {
            this.Enabled = false;
            _data = new MachineModel.MED_MACHINE_REPAIRSITUATIONDataTable();
            this.busyIndicator1.ShowLoadingScreenFor(gcMachine);
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    //GetData
                    _data = _machineService.GetAllRepairData(Utilities.Utility.CDate(this.beginTime.EditValue.ToString()), Utilities.Utility.CDate(this.endTime.EditValue.ToString()));
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    if (_data == null)
                    {
                        return;
                    }
                    this.gcMachine.DataSource = _data;
                    this.Enabled = true;
                    this.busyIndicator1.Hide();

                };
                worker.RunWorkerAsync();
            }
        }

        #endregion
    }
}
