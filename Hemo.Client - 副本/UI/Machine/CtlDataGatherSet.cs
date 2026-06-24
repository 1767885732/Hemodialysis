/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：数据采集设定户控件类
// 创建时间：2016-03-26
// 创建者：吕志强
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
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Machine;
using Hemo.Service;
using Hemo.Model;
using Hemo.IService.Config;
using System.Linq;
using Hemo.Utilities;

namespace Hemo.Client.UI.Machine
{
    public partial class CtlDataGatherSet : DevExpress.XtraEditors.XtraUserControl
    {
        #region 成员变量

        private HemodialysisModel.MED_HEMO_PARAMETERS_SETTINGDataTable dtSetting = null;

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        private IMachine machineService = ServiceManager.Instance.MachineService;

        #endregion

        #region 构造函数

        public CtlDataGatherSet()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CtlDataGatherSet_Load(object sender, EventArgs e)
        {
            BindList();
            GetDataGatherSetList();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindList();
        }

        /// <summary>
        /// 设置血透机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSet_Click(object sender, EventArgs e)
        {
            if (this.gcMachine.DataSource == null || (this.gcMachine.DataSource as DataTable).Rows.Count == 0)
            {
                return;
            }

            var currentRow = this.gvMachine.GetFocusedDataRow();

            if (currentRow == null)
            {
                XtraMessageBox.Show("请选择一行要设置的记录！");
                return;
            }

            var findRow = dtSetting.FirstOrDefault(row => row.MACHINE_ID == currentRow["MACHINE_ID"].ToString());

            if (findRow == null)
            {
                findRow = dtSetting.NewMED_HEMO_PARAMETERS_SETTINGRow();
                findRow.MACHINE_ID = currentRow["MACHINE_ID"].ToString();
                findRow.MACHINE_LABEL = currentRow["MACHINE_LABEL"].ToString();
                dtSetting.Rows.Add(findRow);
            }

            findRow.GATHER_DEFAULT = this.seDefault.Value;
            findRow.GATHER_ACTUAL = this.seActual.Value;
            findRow.GATHER_COUNT = this.seCount.Value;
            findRow.STATUS = "1";

            try
            {
                hemoService.SaveDataGatherSet(dtSetting);
                XtraMessageBox.Show("设置成功！");
                BindList();
                GetDataGatherSetList();
            }
            catch (Exception)
            {
                XtraMessageBox.Show("设置失败！");
            }
        }

        /// <summary>
        /// 取消血透机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.gcMachine.DataSource == null || (this.gcMachine.DataSource as DataTable).Rows.Count == 0)
            {
                return;
            }

            var currentRow = this.gvMachine.GetFocusedDataRow();

            if (currentRow == null)
            {
                XtraMessageBox.Show("请选择一行要取消的记录！");
                return;
            }

            var findRow = dtSetting.FirstOrDefault(row => row.MACHINE_ID == currentRow["MACHINE_ID"].ToString());

            if (findRow == null)
            {
                XtraMessageBox.Show("设备对应的设置记录不存在！");
                return;
            }

            findRow.STATUS = "0";

            try
            {
                hemoService.SaveDataGatherSet(dtSetting);
                XtraMessageBox.Show("取消成功！");
                BindList();
                GetDataGatherSetList();
            }
            catch (Exception)
            {
                XtraMessageBox.Show("取消失败！");
            }
        }

        /// <summary>
        /// 列表行点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcMachine_Click(object sender, EventArgs e)
        {
            var currentRow = this.gvMachine.GetFocusedDataRow();

            if (dtSetting == null)
            {
                dtSetting = new HemodialysisModel.MED_HEMO_PARAMETERS_SETTINGDataTable();
            }

            var findRow = dtSetting.FirstOrDefault(row => row.MACHINE_ID == currentRow["MACHINE_ID"].ToString());

            if (findRow != null)
            {
                this.txtMachine.Text = findRow.MACHINE_LABEL;
                this.seDefault.Value = findRow.GATHER_DEFAULT;
                this.seActual.Value = findRow.GATHER_ACTUAL;
                this.seCount.Value = findRow.GATHER_COUNT;
            }
            else
            {
                this.txtMachine.Text = currentRow["MACHINE_LABEL"].ToString();
                this.seDefault.Value = 0;
                this.seActual.Value = 0;
                this.seCount.Value = 0;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 绑定列表
        /// </summary>
        private void BindList()
        {
            this.gcMachine.DataSource = machineService.GetMachineListForDataGather();
        }

        /// <summary>
        /// 获取数据采集设置列表
        /// </summary>
        private void GetDataGatherSetList()
        {
            dtSetting = hemoService.GetDataGatherSetList();
        }

        #endregion
    }
}