/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：水处理机运行记录报表报表
// 创建时间：2016-03-11
// 创建者：吕志强
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Hemo.IService.Machine;
using Hemo.Service;
using System.Data;

namespace Hemo.Client.Print
{
    public partial class WaterProcessorUseReport : DevExpress.XtraReports.UI.XtraReport
    { 
        #region 成员变量

        private string machineId;

        private DateTime useDate;

        private IMachine machineService = ServiceManager.Instance.MachineService;

        #endregion

        #region 属性

        /// <summary>
        /// 机器ID
        /// </summary>
        public string MachineId
        {
            get { return machineId; }
            set { machineId = value; }
        }

        /// <summary>
        /// 使用日期
        /// </summary>
        public DateTime UseDate
        {
            get { return useDate; }
            set { useDate = value; }
        }

        #endregion

        #region 构造函数

        public WaterProcessorUseReport(string machineId, DateTime useDate)
        {
            this.machineId = machineId;
            this.useDate = useDate;
            InitializeComponent();
            LoadReport();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载报表
        /// </summary>
        private void LoadReport()
        {
            DataTable dtMachine = machineService.GetMachineById(machineId);
            if (dtMachine != null && dtMachine.Rows.Count > 0)
            {
                this.lblHead.Text = "编号：" + dtMachine.Rows[0]["MACHINE_NAME"].ToString() + "   分类：" + dtMachine.Rows[0]["FLNAME"].ToString() + "   型号：" + dtMachine.Rows[0]["MACHINE_MODEL"].ToString();
            }
            else
            {
                this.lblHead.Text = "编号：" + "XXX" + "   分类：" + "XXX" + "   型号：" + "XXX";
            }
            this.DataSource = machineService.GetWaterProcessorRecordByIdAndSingleDate(machineId, useDate);
        }

        #endregion
    }
}
