/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：服务工厂。
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
using Hemo.Service.Config;
using Hemo.Service.Dept;
using Hemo.Service.Dict;
using Hemo.Service.Erythropoietin;
using Hemo.Service.Lab;
using Hemo.Service.Machine;
using Hemo.Service.Order;
using Hemo.Service.PatientSchedule;
using Hemo.Service.Permission;
using Hemo.IService.DataReport;
using Hemo.Service.DataReport;

namespace Hemo.Service
{
    /// <summary>
    /// 本地服务的具体实现接口
    /// </summary>
    public class ServiceFactory : IServiceFactory
    {
        /// <summary>
        /// 初始化服务
        /// </summary>
        public void InitService()
        {

        }

        /// <summary>
        /// 创建错误日志服务
        /// </summary>
        /// <returns></returns>
        public IExceptionLogService CreateExceptionLogService()
        {
            return new ExceptionLogService();
        }

        /// <summary>
        /// 创建病人服务
        /// </summary>
        /// <returns></returns>
        public IPatient CreatePatientService()
        {
            return new PatientService();
        }
        /// <summary>
        /// 血管通路服务
        /// </summary>
        /// <returns></returns>
        public IVascuarAccess CreateVascuarAccessService() {
            return new VascuarAccessService();
        }

        /// <summary>
        /// 创建药品相关服务
        /// </summary>
        /// <returns></returns>
        public IDrug CreateDrugService()
        {
            return new DrugService();
        }

        /// <summary>
        /// 系统参数服务
        /// </summary>
        /// <returns></returns>
        public IConfig CreateConfigService()
        {
            return new ConfigService();
        }

        /// <summary>
        /// 血透服务
        /// </summary>
        /// <returns></returns>
        public IHemodialysis CreateHemodialysisService()
        {
            return new HemodialysisService();
        }

        /// <summary>
        /// 医生资料设定服务
        /// </summary>
        /// <returns></returns>
        public IStaffDict CreateStaffDictService()
        {
            return new StaffDictService();
        }

        /// <summary>
        /// 科室服务
        /// </summary>
        /// <returns></returns>
        public IDept CreateDeptService()
        {
            return new DeptService();
        }

        /// <summary>
        /// 用户服务
        /// </summary>
        /// <returns></returns>
        public IUser CreateUserService()
        {
            return new UserService();
        }

        /// <summary>
        /// 耗材管理服务
        /// </summary>
        /// <returns></returns>
        public IMaterial CreateMaterialService()
        {
            return new MaterialService();
        }

        /// <summary>
        /// 血透机服务
        /// </summary>
        /// <returns></returns>
        public IMachine CreateMachineService()
        {
            return new MachineService();
        }

        /// <summary>
        /// 病患排班服务
        /// </summary>
        /// <returns></returns>
        public IPatientSchedule CreatePatientScheduleService()
        {
            return new PatientScheduleService();
        }

        /// <summary>
        /// 创建检验记录服务
        /// </summary>
        /// <returns></returns>
        public ILab CreateLabService()
        {
            return new LabService();
        }

        /// <summary>
        /// 创建医嘱服务
        /// </summary>
        /// <returns></returns>
        public IOrder CreateOrderService()
        {
            return new OrderService();
        }

        /// <summary>
        /// 创建促红素服务
        /// </summary>
        /// <returns></returns>
        public IErythropoietin CreateErythropoietinService()
        {
            return new ErythropoietinService();
        }


        /// <summary>
        /// 统计报表服务
        /// </summary>
        /// <returns></returns>
        public IDataReport CreateDataReportService()
        {
            return new DataReportService();

        }
    }
}
