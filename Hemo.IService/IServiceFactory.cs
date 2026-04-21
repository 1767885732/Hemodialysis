/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：描述只需简述，具体详情在类的注释中描述。
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

using Hemo.IService.Config;
using Hemo.IService.Dept;
using Hemo.IService.Dict;
using Hemo.IService.Erythropoietin;
using Hemo.IService.Lab;
using Hemo.IService.Machine;
using Hemo.IService.Order;
using Hemo.IService.PatientSchedule;
using Hemo.IService.Permission;
using Hemo.IService.DataReport;

namespace Hemo.IService
{
    /// <summary>
    /// 服务工厂接口定义
    /// </summary>
    public interface IServiceFactory
    {
        /// <summary>
        /// 初始化服务
        /// </summary>
        void InitService();

        /// <summary>
        /// 创建错误日志服务
        /// </summary>
        /// <returns></returns>
        IExceptionLogService CreateExceptionLogService();

        /// <summary>
        /// 创建病人服务 
        /// </summary>
        /// <returns></returns>
        IPatient CreatePatientService();
        /// <summary>
        /// 血管通路服务
        /// </summary>
        /// <returns></returns>
        IVascuarAccess CreateVascuarAccessService();
        /// <summary>
        /// 创建药品相关服务
        /// </summary>
        /// <returns></returns>
        IDrug CreateDrugService();

        /// <summary>
        /// 创建配置服务
        /// </summary>
        /// <returns></returns>
        IConfig CreateConfigService();

        /// <summary>
        /// 创建血透相关服务
        /// </summary>
        /// <returns></returns>
        IHemodialysis CreateHemodialysisService();

        /// <summary>
        /// 创建职员向光服务
        /// </summary>
        /// <returns></returns>
        IStaffDict CreateStaffDictService();

        /// <summary>
        /// 创建部门相关服务
        /// </summary>
        /// <returns></returns>
        IDept CreateDeptService();

        /// <summary>
        /// 创建用户相关服务
        /// </summary>
        /// <returns></returns>
        IUser CreateUserService();

        /// <summary>
        /// 创建耗材管理相关服务
        /// </summary>
        /// <returns></returns>
        IMaterial CreateMaterialService();

        /// <summary>
        /// 创建机器管理相关服务
        /// </summary>
        /// <returns></returns>
        IMachine CreateMachineService();

        /// <summary>
        /// 创建病患排班服务
        /// </summary>
        /// <returns></returns>
        IPatientSchedule CreatePatientScheduleService();

        /// <summary>
        /// 创建检验记录服务
        /// </summary>
        /// <returns></returns>
        ILab CreateLabService();

        /// <summary>
        /// 创建医嘱服务
        /// </summary>
        /// <returns></returns>
        IOrder CreateOrderService();

        /// <summary>
        /// 创建促红素服务
        /// </summary>
        /// <returns></returns>
        IErythropoietin CreateErythropoietinService();

        /// <summary>
        /// 创建报表相关服务
        /// </summary>
        /// <returns></returns>
        IDataReport CreateDataReportService();
    }
}
