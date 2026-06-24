/*----------------------------------------------------------------
// Copyright (C) 2005 (苏州)医疗科技发展有限公司
// 文件名：IMachine.cs
// 文件功能描述：血液透析设备接口定义文件
// 创建标识：
// 修改时间：2014-5-4
// 修改人：吕志强
// 修改描述：更新获取设备使用费用数据接口方法
----------------------------------------------------------------*/

using System.ServiceModel;
using Hemo.Model;
using System;
using System.Data;

namespace Hemo.IService.Machine
{
    [ServiceContract]
    public interface IMachine
    {
        #region 血液透析设备接口定义文件

        [OperationContract]
        MachineModel.MED_DIALYSIS_MACHINEDataTable GetMachineList();

        [OperationContract]
        MachineModel.MED_DIALYSIS_MACHINEDataTable GetNewMachineList();

        [OperationContract]
        PermissionModel.MED_DIALYSIS_MACHINEEXTENDDataTable GetMachineListByUserID(string userId);
        [OperationContract]
        MachineModel.MED_DIALYSIS_MACHINEDataTable GetMachineListByTypeAndBedId(string type, string bedId);

        [OperationContract]
        MachineModel.MED_DIALYSIS_MACHINEDataTable GetMachineByMachineName(string areaId, string machineName);

        [OperationContract]
        MachineModel.MED_DIALYSIS_MACHINEDataTable GetMachineListForDataGather();

        [OperationContract]
        int SaveMachineInfo(MachineModel.MED_DIALYSIS_MACHINEDataTable machineDataTable);

        [OperationContract]
        MachineModel.MED_MACHINE_USERECORDDataTable GetUseDataByParms(string machineID, DateTime userDay);

        [OperationContract]
        MachineModel.MED_MACHINE_USERECORDDataTable GetUseAllDataByMachineID(string _areaID, string _bedID, string _banchi, string useMonthy);

        [OperationContract]
        MachineModel.MED_MACHINE_USERECORDDataTable GetUseAllDataByMachineIDAndData(string _machineID, DateTime useMonthy);

        [OperationContract]
        int SaveMachineUserRecord(MachineModel.MED_MACHINE_USERECORDDataTable Data);

        [OperationContract]
        MachineModel.MED_MACHINE_REPAIRSITUATIONDataTable GetDataByDate(DateTime RepairData);

        [OperationContract]
        MachineModel.MED_MACHINE_REPAIRSITUATIONDataTable GetAllRepairData(DateTime beginDate, DateTime endDate);

        [OperationContract]
        int SaveRepairDatas(MachineModel.MED_MACHINE_REPAIRSITUATIONDataTable date);

        /// <summary>
        /// 复用记录单
        /// </summary>
        /// <param name="data">复用记录数据</param>
        /// <returns>返回结果</returns>
        [OperationContract]
        int SaveReUsableDatas(MachineModel.MED_MACHINE_REUSABLEDataTable data);

        [OperationContract]
        MachineModel.MED_MACHINE_REUSABLEDataTable GetReUsableData(string hemoID, DateTime beginDate, DateTime endDate, string machinetype);

        /// <summary>
        /// 保存空气净化数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveAirPurgeData(MachineModel.MED_MACHINE_AIRPURGEDataTable data);
        /// <summary>
        /// 保存空气净化数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveAirPurgeDataFirstDelete(MachineModel.MED_MACHINE_AIRPURGEDataTable data);

        [OperationContract]
        int DeleteAirPurgeData(string arrPurgeId);
        /// <summary>
        /// 根据开始时间和结束时间获取空气净化数据
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [OperationContract]
        MachineModel.MED_MACHINE_AIRPURGEDataTable GetAirPurgeData(DateTime beginDate, DateTime endDate, string roomID);

        [OperationContract]
        MachineModel.MED_MACHINE_AIRPURGEDataTable GetAirPurgeDataById(string id);

        /// <summary>
        /// 保存血透机使用费用数据（BY 周祥）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveUseFeeData(MachineModel.MED_MACHINE_USEFEEDataTable data);

        /// <summary>
        /// 根据条件获取血透机使用费用数据
        /// </summary>
        /// <param name="sickArea"></param>
        /// <param name="bedNo"></param>
        /// <param name="banCi"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetUseFeeData(string sickArea, string bedNo, DateTime beginDate, DateTime endDate);

        /// <summary>
        /// 根据开始时间和结束时间获取血透机使用费用统计数据（BY 周祥）
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetUseFeeStatisticsData(DateTime beginDate, DateTime endDate);

        /// <summary>
        /// 根据唯一ID获取血透机使用费用数据（BY 周祥）
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [OperationContract]
        MachineModel.MED_MACHINE_USEFEEDataTable GetUseFeeData(string id);

        /// <summary>
        /// 保存医疗设备主机登记数据（BY 周祥）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveMainframeData(MachineModel.MED_MACHINE_MAINFRAMEDataTable data);

        /// <summary>
        /// 获取医疗设备主机登记数据（BY 周祥）
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [OperationContract]
        MachineModel.MED_MACHINE_MAINFRAMEDataTable GetMainframeData();

        /// <summary>
        /// 保存医疗设备主机附属设备数据（BY 周祥）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveAccessoryEquipData(MachineModel.MED_MACHINE_ACCESSORYEQUIPDataTable data);

        /// <summary>
        /// 获取医疗设备主机附属设备数据（BY 周祥）
        /// </summary>
        /// <param name="mainframeId"></param>
        /// <returns></returns>
        [OperationContract]
        MachineModel.MED_MACHINE_ACCESSORYEQUIPDataTable GetAccessoryEquipData(string mainframeId);

        #region 配制液桶消毒记录

        [OperationContract]
        MachineModel.MED_MACHINE_MIXBARRELDataTable GetMixDataByParms(DateTime _beginDate, DateTime _endDate);

        [OperationContract]
        int SaveMixBarrelData(MachineModel.MED_MACHINE_MIXBARRELDataTable _data);

        #endregion

        #region 透析用水内毒素检测
        //内毒素
        [OperationContract]
        MachineModel.MED_MACHINE_MIXBARRELDataTable GetWaterHemoDataByParms(DateTime _begionDate, DateTime _endDate, string _machineID);
        //水处理
        [OperationContract]
        MachineModel.MED_MACHINE_MIXBARRELDataTable GetWaterTreatmentDataByParms(DateTime _begionDate, DateTime _endDate, string _machineID);
        //贵重医疗气节
        [OperationContract]
        MachineModel.MED_MACHINE_MIXBARRELDataTable GetHosEquipmentDataByParms(DateTime _begionDate, DateTime _endDate, string _machineID);


        #endregion

        #region 透析液内毒素检测


        #endregion

        [OperationContract]
        MachineModel.MED_DIALYSIS_MACHINEDataTable GetMachineListByType(string type);

        [OperationContract]
        MachineModel.MED_DIALYSIS_MACHINEDataTable GetWaterMachineListByType(string type);

        [OperationContract]
        MachineModel.MED_WATERPROCESSOR_USERECORDDataTable GetWaterProcessorRecordByIdAndDate(string id, DateTime beginDate, DateTime endDate);

        [OperationContract]
        MachineModel.MED_WATERPROCESSOR_USERECORDDataTable GetWaterProcessorRecordByIdAndSingleDate(string id, DateTime useDate);

        [OperationContract]
        MachineModel.MED_DIALYSIS_MACHINEDataTable GetMachineById(string id);

        [OperationContract]
        MachineModel.MED_DIALYSIS_MACHINEDataTable GetWaterMachineById(string id);

        [OperationContract]
        int SaveWaterProcessorRecord(MachineModel.MED_WATERPROCESSOR_USERECORDDataTable dtRecord);

        [OperationContract]
        MachineModel.MED_MACHINE_USERECORDDataTable GetMachineUseRecordList(string area, string bed, string banCi, DateTime beginDate, DateTime endDate);

        [OperationContract]
        MachineModel.MED_MACHINE_USERECORDDataTable GetMachineUseRecordByIdAndDate(string id, DateTime beginDate, DateTime endDate);

        [OperationContract]
        MachineModel.MED_MACHINE_USERECORDDataTable GetMachineUseRecordBySignIdAndDate(string Signid, DateTime beginDate, DateTime endDate);

        [OperationContract]
        MachineModel.MED_MACHINE_USERECORDDataTable GetMachineUseRecordByIdAndSingleDate(string id, DateTime useDate);

        [OperationContract]
        MachineModel.MED_DIALYSIS_MACHINEDataTable GetWaterProcessorList();

        [OperationContract]
        MachineModel.MED_PROCESS_SETDataTable GetProcessSetDataById(string id);
        [OperationContract]
        MachineModel.MED_PROCESS_SETDataTable GetProcessSetDataList();
        [OperationContract]
        int SaveProcessSetData(MachineModel.MED_PROCESS_SETDataTable dt);
        [OperationContract]
        int DeleteProcessSetDataById(string id);

        [OperationContract]
        int SaveMED_EQUIPMENT_MGR(MachineModel.MED_EQUIPMENT_MGRDataTable dt);

        [OperationContract]
        int DeleteMED_EQUIPMENT_MGR(string id);

        [OperationContract]
        MachineModel.MED_EQUIPMENT_MGRDataTable SelectMED_EQUIPMENT_MGRByTime(DateTime beginTime, DateTime endTime);


        [OperationContract]
        int SaveMED_EQUIPMENT_MAINTENANCE(MachineModel.MED_EQUIPMENT_MAINTENANCEDataTable dt);

        [OperationContract]
        int DeleteMED_EQUIPMENT_MAINTENANCE(string id);

        [OperationContract]
        MachineModel.MED_EQUIPMENT_MAINTENANCEDataTable SelectMED_EQUIPMENT_MAINTENANCEByTime(DateTime beginTime, DateTime endTime);

        [OperationContract]
        int SaveMED_EQUIPMENT_REPAIR(MachineModel.MED_EQUIPMENT_REPAIRDataTable dt);

        [OperationContract]
        int DeleteMED_EQUIPMENT_REPAIR(string id);

        [OperationContract]
        MachineModel.MED_EQUIPMENT_REPAIRDataTable SelectMED_EQUIPMENT_REPAIRByTime(DateTime beginTime, DateTime endTime);
        #endregion
    }
}
