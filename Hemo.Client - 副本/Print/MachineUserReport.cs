/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:血透机使用情况记录报表
 * 创建标识:吕志强-2016年4月18日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraScheduler;
using Hemo.IService.Machine;
using Hemo.Service;
using Hemo.Model;

namespace Hemo.Client.Print
{
    public partial class MachineUserReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        public MachineUserReport(DateTime reportDate, MachineModel.MED_DIALYSIS_MACHINERow machineRow,string AreaName,string BedName)
        {
            InitializeComponent();

            this.xrlabMoon.Text = reportDate.ToString("yyyy年-MM月");
            this.lb_Machine.Text = string.Format("透析室:{0} 床位:{1}床 机器编号:{2}", AreaName,BedName, machineRow.MACHINE_MODEL);

            IMachine _machineService = ServiceManager.Instance.MachineService;
            this.DataSource = _machineService.GetUseAllDataByMachineIDAndData(machineRow.MACHINE_ID, reportDate);
            this.DataMember = "";
        }

        #endregion
    }
}
