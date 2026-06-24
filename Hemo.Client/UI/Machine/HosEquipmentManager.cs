/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：贵重医疗设备保养及检测登记维护列表
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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Xpf.Grid;
using Hemo.Client.Print;
using Hemo.Model;
using Hemo.Service;
using DevExpress.XtraEditors;
using Hemo.IService.Dict;
using Hemo.Utilities;
using Hemo.IService.Config;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.UI.Machine
{
    public partial class HosEquipmentManager : ViewBase
    {
        #region 类变量

        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private Hemo.IService.Machine.IMachine _machineService = ServiceManager.Instance.MachineService;

        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private MachineModel.MED_MACHINE_MIXBARRELDataTable _data = null;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public HosEquipmentManager()
        {
            InitializeComponent();

            DateTime dt = DateTime.Now; //当前时间
            DateTime startQuarter = dt.AddMonths(0 - (dt.Month - 1) % 3).AddDays(1 - dt.Day);  //本季度初 
            DateTime endQuarter = startQuarter.AddMonths(3).AddDays(-1);  //本季度末  

            this.beginTime.EditValue = startQuarter;
            this.endTime.EditValue = endQuarter;
        }

        #endregion

        #region 事件

        private void HosEquipmentManager_Load(object sender, EventArgs e)
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
            HosEquipmentReport frm = new HosEquipmentReport(_data);
            ReportPrintTool pt = new ReportPrintTool(frm);
            pt.ShowPreviewDialog();
        }
        //新增记录
        private void btn_Entering_Click(object sender, EventArgs e)
        {
            HosEquipmentEdit frm = new HosEquipmentEdit();
            frm.CurrentRow = null;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                InitalizeData();
            }
        }
        //显示右击删除
        private void gcMachine_MouseDown(object sender, MouseEventArgs e)
        {
            var row = gvMachine.GetFocusedDataRow() as MachineModel.MED_MACHINE_MIXBARRELRow;
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
            var row = gvMachine.GetFocusedDataRow() as MachineModel.MED_MACHINE_MIXBARRELRow;
            if (row == null)
            {
                return;
            }

            HosEquipmentEdit frm = new HosEquipmentEdit();
            frm.CurrentRow = row;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                InitalizeData();
            }
        }
        //删除
        private void toolDelete_Click(object sender, EventArgs e)
        {
            var row = gvMachine.GetFocusedDataRow() as MachineModel.MED_MACHINE_MIXBARRELRow;
            if (row == null)
            {
                return;
            }
            if (XtraMessageBox.Show("是否确定删除当前行数据？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.No)
                return;

            row.ISDELETE = "1";
            _machineService.SaveMixBarrelData(_data);
            _data.AcceptChanges();
            InitalizeData();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            var row = gvMachine.GetFocusedDataRow() as MachineModel.MED_MACHINE_MIXBARRELRow;
            if (row == null)
            {
                return;
            }
            if (XtraMessageBox.Show("是否确定删除当前行数据？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.No)
                return;

            row.ISDELETE = "1";
            _machineService.SaveMixBarrelData(_data);
            _data.AcceptChanges();
            InitalizeData();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitalizeData()
        {
            this.Enabled = false;
            _data = new MachineModel.MED_MACHINE_MIXBARRELDataTable();
            this.busyIndicator1.ShowLoadingScreenFor(gcMachine);
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    //GetData

                    _data = _machineService.GetHosEquipmentDataByParms(Utilities.Utility.CDate(this.beginTime.EditValue.ToString()), Utilities.Utility.CDate(this.endTime.EditValue.ToString()), txt_machine.Text.Trim());
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
