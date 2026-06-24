/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技股份有限公司
 * 文件功能描述:空气消毒记录报表
 * 创建标识:吕志强-2016年5月15日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using DevExpress.XtraReports.UI;
using Hemo.IService.Machine;
using Hemo.Service;
using Hemo.Model;
using Hemo.IService.Config;

namespace Hemo.Client.Print
{
    public partial class AirPurgeReportList : DevExpress.XtraReports.UI.XtraReport
    {
        #region 类变量

        private IConfig _configService = ServiceManager.Instance.ConfigService;

        #endregion

        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BedinDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="roomID"></param>
        public AirPurgeReportList(DateTime BedinDate, DateTime EndDate,string roomID)
        {
            InitializeComponent();

            ConfigModel.MED_COMMON_ITEMLISTDataTable config = this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1");
            var roomRow =  config.FirstOrDefault(i => i.ITEM_ID == roomID);
          
            IMachine _machinePari = ServiceManager.Instance.MachineService;
            
            MachineModel.MED_MACHINE_AIRPURGEDataTable _airDate = _machinePari.GetAirPurgeData(BedinDate, EndDate, roomID);

            this.xrlabMoon.Text = BedinDate.ToString("yyyy年MM月");
            this.xrlab_Room.Text = roomRow.ITEM_NAME.Trim();
            this.DataSource = _airDate;
            this.DataMember = "";
            xrLabel1.Text = Utilities.Utility.GetHospitalName();
        }

        #endregion
    }
}
