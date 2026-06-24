/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:应用程序上下文
 * 创建标识:顾伟伟-2017年1月30日
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DevExpress.XtraEditors;
using Hemo.Model;
using System.Net;
using System.Net.Sockets;
using Hemo.IService.Machine;
using Hemo.Service;
using Medicalsystem.Auth.Client;
using HEMODataReporter;
using Hemo.IService.Config;

namespace Hemo.Client.Core 
{
    /// <summary>
    /// 应用程序上下文
    /// </summary>
    public class HemoApplicationContext 
    {
        private static readonly HemoApplicationContext _context = new HemoApplicationContext();

        public static HemoApplicationContext Current 
        {
            get 
            {
                return _context;
            }
        }
        public DataTable RolesOffices = new DataTable();
        public PermissionModel.MED_USERSRow CurrentUser = null;
        private IMachine _machineService = ServiceManager.Instance.MachineService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        /// <summary>
        /// 显示当前版本号
        /// </summary>
        public string versionAddress { get; set; }
        private string _ipAddress = string.Empty;
        /// <summary>
        /// 获取或者设置机器IP地址
        /// </summary>
        public string IpAddress
        {
            get
            {
                if (!string.IsNullOrEmpty(_ipAddress))
                    return _ipAddress;

                var addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
                foreach (var address in addressList)
                {
                    if (address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        _ipAddress = address.ToString();
                        break;
                    }
                }
                return _ipAddress;
            }
            set
            {
                _ipAddress = value;
            }
        }
        public HEMODataReporter.DataReporter dataCurrentReport
        {
            get;
            set;
        }
        private string isPassDogValid = string.Empty;
        /// <summary>
        /// 加密狗认证
        /// </summary>
        public string IsPassDogValid
        {
            get
            {
                int dogCount = Utilities.Utility.GetHospitalBedCount();
                if (dogCount <= 0)
                {
                    isPassDogValid = Utilities.Utility.dogTipStr;
                }
                else
                {
                    var _machineDataTable = _machineService.GetNewMachineList();
                    var data = new MachineModel.MED_DIALYSIS_MACHINEDataTable();
                    _machineDataTable.Where(i => !string.IsNullOrEmpty(i.AREA_ID) && !string.IsNullOrEmpty(i.BED_ID)).CopyToDataTable<MachineModel.MED_DIALYSIS_MACHINERow>(data, LoadOption.PreserveChanges);
                    if (data != null && data.Rows.Count > 0)
                    {
                        int bedCount = data.Rows.Count;
                        //int dogCount = Utilities.Utility.GetHospitalBedCount();
                        if (bedCount > dogCount)
                        {
                            if (DAuthContext.Current.HospitalID != 1)
                                isPassDogValid = string.Format(Utilities.Utility.dogTipStr1,string.Empty);
                            else
                                isPassDogValid = Utilities.Utility.dogTipStr;
                        }
                        else
                            isPassDogValid = string.Empty;
                    }
                    else
                        isPassDogValid = string.Empty;
                }
                return isPassDogValid;
            }
            set
            {
                isPassDogValid = value;
            }
        }
        private ConfigModel.MED_COMMON_ITEMLISTDataTable interFaceDate = null;

        /// <summary>
        /// 获取系统接口
        /// </summary>
        public ConfigModel.MED_COMMON_ITEMLISTDataTable InterFaceDate
        {
            get
            {
                return interFaceDate = this._configService.GetConfigList(string.Empty, string.Empty, "接口", "1");
            }
            set
            {
                interFaceDate = value;
            }
        }

        private DataReporter isLoginHospitalHemoPlatForm = null;
        /// <summary>
        /// 登录全国血液净化病例信息登记系统
        /// </summary>
        public DataReporter IsLoginHospitalHemoPlatForm
        {
            get
            {
                //获取用户名和密码
                var plateFormInfo = this._configService.GetConfigList(string.Empty, string.Empty, "平台用户信息", "1");
                if (plateFormInfo == null || plateFormInfo.Rows.Count <= 0)
                {
                    XtraMessageBox.Show("请配置全国血液净化病例信息登记系统用户名和密码！", "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return null;
                }
                var userName = plateFormInfo[0].ITEM_NAME;
                var passWord = plateFormInfo[0].ITEM_VALUE;
                var reporter = new DataReporter(userName, passWord);
                HemoApplicationContext.Current.dataCurrentReport = reporter;

                return reporter;//返回GET登录信息
            }
            set { isLoginHospitalHemoPlatForm = value; }
        }
    }

    /// <summary>
    /// 登录启动类
    /// </summary>
    public class LoginEventArgs : EventArgs {

        public bool RunApp { get; set; }
        public string RunAppNames { get; set; }
    }
    /// <summary>
    /// 切换 登录启动类
    /// </summary>
    public class ShiftEventArgs : EventArgs 
    {
        public bool RunApp { get; set; }
        public XtraForm RunAppNames { get; set; }
    }
}
