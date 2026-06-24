/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：血液净化科设备检修一览表报表
// 创建时间：2015-10-22
// 创建者：贺建操
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
using Hemo.Model;
using Hemo.Utilities;

namespace Hemo.Client.Print
{
    public partial class RepairReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        public RepairReport(DateTime BedinDate, DateTime EndDate)
        {
            InitializeComponent();

            IMachine _machinePari = ServiceManager.Instance.MachineService;

            MachineModel.MED_MACHINE_REPAIRSITUATIONDataTable _repairDate = _machinePari.GetAllRepairData(BedinDate, EndDate);
            this.DataSource = _repairDate;
            this.DataMember = "";
        }

        #endregion
    }
}
