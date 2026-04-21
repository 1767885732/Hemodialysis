/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：水处理设备维护用户控件类
// 创建时间：2015-07-06
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

namespace Hemo.Client.UI.Machine {
    public partial class WaterHemoManager : ViewBase
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

        public WaterHemoManager()
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

        private void MahineWaterHemoManager_Load(object sender, EventArgs e)
        {
            DataTable dtStaffSict = _staffDictService.GetStaffDictList();
            if (dtStaffSict != null && dtStaffSict.Rows.Count > 0)
            {
                DataTable dtPunctureNurseList = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'", "name");
                if (dtPunctureNurseList != null && dtPunctureNurseList.Rows.Count > 0)
                {
                    this.repositoryItemLookUpEdit1.DataSource = dtPunctureNurseList;
                }
            }
            ConfigModel.MED_COMMON_ITEMLISTDataTable config = this._configService.GetConfigList(string.Empty,
               string.Empty, "消毒机器", "1");

            if (config != null && config.Rows.Count > 0)
            {
                DataRow sickAreaRow = config.NewRow();
                sickAreaRow["ITEM_NAME"] = "全部";
                sickAreaRow["ITEM_ID"] = "c570d95c-76a2-4af4-893a-1357065623bf";
                sickAreaRow["ORDER_NUMBER"] = 0;
                config.Rows.InsertAt(sickAreaRow, 0);
                Hemo.Utilities.Utility.BindLookUpEdit(lop_machine, "ITEM_ID", "ITEM_NAME", (DataTable)config, "ITEM_NAME", "消毒机器");

                this.repositoryItemLookUpEdit2.DataSource = config;
            }

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
            WaterHemoReport frm = new WaterHemoReport(_data, GetJiDuStartMonth());
            ReportPrintTool pt = new ReportPrintTool(frm);
            pt.ShowPreviewDialog();
        }
        //新增记录
        private void btn_Entering_Click(object sender, EventArgs e)
        {
            WaterHemoEdit frm = new WaterHemoEdit();
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

            WaterHemoEdit frm = new WaterHemoEdit();
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
                    string machineID = this.lop_machine.EditValue.ToString();
                    if (machineID == "c570d95c-76a2-4af4-893a-1357065623bf")
                    {
                        machineID = string.Empty;
                    }
                    _data = _machineService.GetWaterHemoDataByParms(Utilities.Utility.CDate(this.beginTime.EditValue.ToString()), Utilities.Utility.CDate(this.endTime.EditValue.ToString()), machineID);
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

        /// <summary>
        /// 判断当前是当年的第几季度
        /// </summary>
        /// <returns>第几季度，该季度开始的首月份</returns>
        private string GetJiDuStartMonth()
        {
            string returntStr = "第{0}季度";
            //一年四季
            string[] jiDuStr = new string[] { "01,02,03", "04,05,06", "07,08,09", "10,11,12" };

            string positionMonth = DateTime.Now.ToString("MM"); //当前月份
            #region 获取当前年度
            for (int i = 0; i < jiDuStr.Length; i++)
            {
                if (jiDuStr[i].IndexOf(positionMonth) != -1)
                {
                    returntStr = string.Format(returntStr, (i + 1).ToString().ToUpper());
                    break;
                }
            }
            #endregion

            return returntStr;
        }

        #endregion
    }
}
