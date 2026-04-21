/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：服务管理类。
// 创建时间：2013-03-07
// 创建者：Frank
//  
// 修改时间：
// 修改人：
// 修改描述：
//
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using Hemo.IService;
using Hemo.IService.Config;
using Hemo.IService.Dept;
using Hemo.IService.Dict;
using Hemo.IService.Erythropoietin;
using Hemo.IService.Lab;
using Hemo.IService.Machine;
using Hemo.IService.Order;
using Hemo.IService.PatientSchedule;
using Hemo.IService.Permission;
using Hemo.Utilities;
using Hemo.IService.DataReport;

namespace Hemo.Service
{
    /// <summary>
    /// ServiceManager
    /// </summary>
    public class ServiceManager : AbstractServiceFactory
    {
        #region 私有变量
        private static readonly string servicePath = BaseSystemInfo.ServicePath;
        private static readonly string serviceFactoryClass = BaseSystemInfo.ServiceFactory;
        #endregion

        public ServiceManager()
        {
            serviceFactory = GetServiceFactory(servicePath, serviceFactoryClass);
        }

        private IServiceFactory serviceFactory = null;
        private static ServiceManager instance = null;
        private static object locker = new Object();

        /// <summary>
        /// 单实例模式
        /// </summary>
        public static ServiceManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new ServiceManager();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// 初始化服务
        /// </summary>
        public void InitService()
        {
            serviceFactory.InitService();
        }

        /// <summary>
        /// 错误日志服务
        /// </summary>
        public IExceptionLogService ExceptionLogService
        {
            get
            {
                return serviceFactory.CreateExceptionLogService();
            }
        }

        /// <summary>
        /// 病人服务
        /// </summary>
        public IPatient PatientService
        {
            get
            {
                return serviceFactory.CreatePatientService();
            }
        }

        /// <summary>
        /// 血管通路服务 
        /// </summary>
        public IVascuarAccess VascuarAccessService {
            get {
                return serviceFactory.CreateVascuarAccessService();
            }
        }

        /// <summary>
        /// 药品相关服务 
        /// </summary>
        public IDrug DrugService
        {
            get
            {
                return serviceFactory.CreateDrugService();
            }
        }
        /// <summary>
        /// 系统参数服务
        /// </summary>
        public IConfig ConfigService
        {
            get
            {
                return serviceFactory.CreateConfigService();
            }
        }

        /// <summary>
        /// 系统参数服务
        /// </summary>
        public IHemodialysis HemodialysisService
        {
            get
            {
                return serviceFactory.CreateHemodialysisService();
            }
        }

        /// <summary>
        /// 医生资料设定服务
        /// </summary>
        public IStaffDict StaffDictService
        {
            get
            {
                return serviceFactory.CreateStaffDictService();
            }
        }

        /// <summary>
        /// 科室服务
        /// </summary>
        public IDept DeptService
        {
            get
            {
                return serviceFactory.CreateDeptService();
            }
        }

        /// <summary>
        /// 用户服务
        /// </summary>
        public IUser UserService
        {
            get
            {
                return serviceFactory.CreateUserService();
            }
        }


        /// <summary>
        /// 耗材管理服务
        /// </summary>
        public IMaterial MaterialService
        {
            get
            {
                return serviceFactory.CreateMaterialService();
            }
        }

        /// <summary>
        /// 血透机服务
        /// </summary>
        public IMachine MachineService
        {
            get
            {
                return serviceFactory.CreateMachineService();
            }
        }

        /// <summary>
        /// 病患排班服务
        /// </summary>
        public IPatientSchedule PatientSchedule
        {
            get
            {
                return serviceFactory.CreatePatientScheduleService();
            }
        }

        /// <summary>
        /// 检验记录服务
        /// </summary>
        public ILab LabService
        {
            get
            {
                return serviceFactory.CreateLabService();
            }
        }

        /// <summary>
        /// 医嘱服务
        /// </summary>
        public IOrder OrderService
        {
            get
            {
                return serviceFactory.CreateOrderService();
            }
        }

        /// <summary>
        /// 促红素服务
        /// </summary>
        public IErythropoietin ErythropoietinService
        {
            get
            {
                return serviceFactory.CreateErythropoietinService();
            }
        }
        
        /// <summary>
        /// 统计报表服务
        /// </summary>
        public IDataReport DataReportService
        {
            get
            {
                return serviceFactory.CreateDataReportService();
            }
        }
    }
}
