/*----------------------------------------------------------------
// Copyright (C) 2005 (北京)医疗科技发展有限公司
// 文件名：MachineService.cs
// 文件功能描述：血液透析设备服务类
// 创建标识：
// 修改时间：2014-5-4
// 修改人：吕志强
// 修改描述：更新获取设备使用费用数据服务方法
----------------------------------------------------------------*/

using System;
using Hemo.Business.Machine;
using Hemo.IService.Machine;
using Hemo.Model;
using System.Data;

namespace Hemo.Service.Machine {
    public class MachineService : MarshalByRefObject, IMachine {
        #region IMachine 成员

        public MachineModel.MED_DIALYSIS_MACHINEDataTable GetMachineList() {
            return MachineBll.GetMachineList();
        }

        public MachineModel.MED_DIALYSIS_MACHINEDataTable GetNewMachineList() {
            return MachineBll.GetNewMachineList();
        }
        public PermissionModel.MED_DIALYSIS_MACHINEEXTENDDataTable GetMachineListByUserID(string userId) {
            return MachineBll.GetMachineListByUserID(userId);
        }
        /// <summary>
        /// 获取对应数据采集的设备列表
        /// </summary>
        /// <returns></returns>
        public MachineModel.MED_DIALYSIS_MACHINEDataTable GetMachineListForDataGather() {
            return MachineBll.GetMachineListForDataGather();
        }

        public int SaveMachineInfo(MachineModel.MED_DIALYSIS_MACHINEDataTable machineDataTable) {
            return MachineBll.SaveMachineInfo(machineDataTable);
        }

        public MachineModel.MED_MACHINE_USERECORDDataTable GetUseDataByParms(string machineID, DateTime userDay) {
            return MachineBll.GetUseDataByParms(machineID, userDay);
        }

        public int SaveMachineUserRecord(MachineModel.MED_MACHINE_USERECORDDataTable Data) {
            return MachineBll.SaveMachineUserRecord(Data);
        }

        public MachineModel.MED_MACHINE_USERECORDDataTable GetUseAllDataByMachineID(string _areaID, string _bedID, string _banchi, string useMonthy) {
            return MachineBll.GetUseAllDataByMachineID(_areaID, _bedID, _banchi, useMonthy);
        }

        public MachineModel.MED_MACHINE_USERECORDDataTable GetUseAllDataByMachineIDAndData(string _machineID, DateTime useMonthy) {
            return MachineBll.GetUseAllDataByMachineIDAndData(_machineID, useMonthy);
        }

        #endregion

        #region 血液透析设备服务类

        public MachineModel.MED_MACHINE_REPAIRSITUATIONDataTable GetDataByDate(DateTime RepairData) {
            return MachineBll.GetDataByDate(RepairData);
        }

        public MachineModel.MED_MACHINE_REPAIRSITUATIONDataTable GetAllRepairData(DateTime beginDate, DateTime endDate) {
            return MachineBll.GetAllRepairData(beginDate, endDate);
        }

        public int SaveRepairDatas(MachineModel.MED_MACHINE_REPAIRSITUATIONDataTable date) {
            return MachineBll.SaveRepairDatas(date);
        }

        public int SaveReUsableDatas(MachineModel.MED_MACHINE_REUSABLEDataTable data) {
            return MachineBll.SaveReUsableDatas(data);
        }

        public MachineModel.MED_MACHINE_REUSABLEDataTable GetReUsableData(string hemoID, DateTime beginDate, DateTime endDate, string machinetype) {
            return MachineBll.GetReUsableData(hemoID, beginDate, endDate, machinetype);
        }

        public int SaveAirPurgeData(MachineModel.MED_MACHINE_AIRPURGEDataTable data) {
            return MachineBll.SaveAirPurgeData(data);
        }

        public MachineModel.MED_MACHINE_AIRPURGEDataTable GetAirPurgeData(DateTime beginDate, DateTime endDate, string roomID) {
            return MachineBll.GetAirPurgeData(beginDate, endDate, roomID);
        }
        #endregion
        /// <summary>
        /// 根据ID获取空气消毒记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MachineModel.MED_MACHINE_AIRPURGEDataTable GetAirPurgeDataById(string id)
        {
            return MachineBll.GetAirPurgeDataById(id);
        }

        public MachineModel.MED_MACHINE_MIXBARRELDataTable GetMixDataByParms(DateTime _beginDate, DateTime _endDate) {
            return MachineBll.GetMixDataByParms(_beginDate, _endDate);
        }

        public int SaveMixBarrelData(MachineModel.MED_MACHINE_MIXBARRELDataTable _data) {
            return MachineBll.SaveMixBarrelData(_data);
        }

        public MachineModel.MED_MACHINE_MIXBARRELDataTable GetWaterHemoDataByParms(DateTime _begionDate, DateTime _endDate, string _machineID) {
            return MachineBll.GetWaterHemoDataByParms(_begionDate, _endDate, _machineID);
        }

        public MachineModel.MED_MACHINE_MIXBARRELDataTable GetWaterTreatmentDataByParms(DateTime _begionDate, DateTime _endDate, string _machineID) {
            return MachineBll.GetWaterTreatmentDataByParms(_begionDate, _endDate, _machineID);
        }

        public MachineModel.MED_MACHINE_MIXBARRELDataTable GetHosEquipmentDataByParms(DateTime _begionDate, DateTime _endDate, string _machineID) {
            return MachineBll.GetHosEquipmentDataByParms(_begionDate, _endDate, _machineID);
        }

        /// <summary>
        /// 保存血透机使用费用数据（BY 周祥）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int SaveUseFeeData(MachineModel.MED_MACHINE_USEFEEDataTable data) {
            return MachineBll.SaveUseFeeData(data);
        }

        /// <summary>
        /// 根据条件获取血透机使用费用数据
        /// </summary>
        /// <param name="sickArea"></param>
        /// <param name="bedNo"></param>
        /// <param name="banCi"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable GetUseFeeData(string sickArea, string bedNo, DateTime beginDate, DateTime endDate) {
            return MachineBll.GetUseFeeData(sickArea, bedNo, beginDate, endDate);
        }

        /// <summary>
        /// 根据开始时间和结束时间获取血透机使用费用统计数据（BY 周祥）
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable GetUseFeeStatisticsData(DateTime beginDate, DateTime endDate) {
            return MachineBll.GetUseFeeStatisticsData(beginDate, endDate);
        }

        /// <summary>
        /// 根据唯一ID获取血透机使用费用数据（BY 周祥）
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public MachineModel.MED_MACHINE_USEFEEDataTable GetUseFeeData(string id) {
            return MachineBll.GetUseFeeData(id);
        }

        /// <summary>
        /// 保存医疗设备主机登记数据（BY 周祥）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int SaveMainframeData(MachineModel.MED_MACHINE_MAINFRAMEDataTable data) {
            return MachineBll.SaveMainframeData(data);
        }

        /// <summary>
        /// 获取医疗设备主机登记数据（BY 周祥）
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public MachineModel.MED_MACHINE_MAINFRAMEDataTable GetMainframeData() {
            return MachineBll.GetMainframeData();
        }

        /// <summary>
        /// 保存医疗设备主机附属设备数据（BY 周祥）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int SaveAccessoryEquipData(MachineModel.MED_MACHINE_ACCESSORYEQUIPDataTable data) {
            return MachineBll.SaveAccessoryEquipData(data);
        }

        /// <summary>
        /// 获取医疗设备主机附属设备数据（BY 周祥）
        /// </summary>
        /// <param name="mainframeId"></param>
        /// <returns></returns>
        public MachineModel.MED_MACHINE_ACCESSORYEQUIPDataTable GetAccessoryEquipData(string mainframeId) {
            return MachineBll.GetAccessoryEquipData(mainframeId);
        }

        /// <summary>
        /// 根据类型获取机器列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public MachineModel.MED_DIALYSIS_MACHINEDataTable GetMachineListByType(string type) {
            return MachineBll.GetMachineListByType(type);
        }

        /// <summary>
        /// 根据类型获取机器列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public MachineModel.MED_DIALYSIS_MACHINEDataTable GetWaterMachineListByType(string type) {
            return MachineBll.GetWaterMachineListByType(type);
        }

        /// <summary>
        /// 根据机器ID&日期获取水处理记录列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public MachineModel.MED_WATERPROCESSOR_USERECORDDataTable GetWaterProcessorRecordByIdAndDate(string id, DateTime beginDate, DateTime endDate) {
            return MachineBll.GetWaterProcessorRecordByIdAndDate(id, beginDate, endDate);
        }

        /// <summary>
        /// 根据机器ID&单个日期获取水处理记录列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="useDate"></param>
        /// <returns></returns>
        public MachineModel.MED_WATERPROCESSOR_USERECORDDataTable GetWaterProcessorRecordByIdAndSingleDate(string id, DateTime useDate) {
            return MachineBll.GetWaterProcessorRecordByIdAndSingleDate(id, useDate);
        }

        /// <summary>
        /// 根据机器ID获取机器信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MachineModel.MED_DIALYSIS_MACHINEDataTable GetMachineById(string id) {
            return MachineBll.GetMachineById(id);
        }

        /// <summary>
        /// 根据机器ID获取机器信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MachineModel.MED_DIALYSIS_MACHINEDataTable GetWaterMachineById(string id) {
            return MachineBll.GetWaterMachineById(id);
        }

        /// <summary>
        /// 保存水处理记录列表
        /// </summary>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        public int SaveWaterProcessorRecord(MachineModel.MED_WATERPROCESSOR_USERECORDDataTable dtRecord) {
            return MachineBll.SaveWaterProcessorRecord(dtRecord);
        }

        /// <summary>
        /// 根据病区、床位、班次、日期获取血透机运行记录列表
        /// </summary>
        /// <param name="area"></param>
        /// <param name="bed"></param>
        /// <param name="banCi"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public MachineModel.MED_MACHINE_USERECORDDataTable GetMachineUseRecordList(string area, string bed, string banCi, DateTime beginDate, DateTime endDate)
        {
            return MachineBll.GetMachineUseRecordList(area, bed, banCi, beginDate, endDate);
        }

        /// <summary>
        /// 根据机器ID&日期获取血透机运行记录列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public MachineModel.MED_MACHINE_USERECORDDataTable GetMachineUseRecordByIdAndDate(string id, DateTime beginDate, DateTime endDate) {
            return MachineBll.GetMachineUseRecordByIdAndDate(id, beginDate, endDate);
        }

        public MachineModel.MED_MACHINE_USERECORDDataTable GetMachineUseRecordBySignIdAndDate(string Signid, DateTime beginDate, DateTime endDate) {
            return MachineBll.GetMachineUseRecordBySignIdAndDate(Signid, beginDate, endDate);
        }

        /// <summary>
        /// 根据机器ID&单个日期获取血透机运行记录列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="useDate"></param>
        /// <returns></returns>
        public MachineModel.MED_MACHINE_USERECORDDataTable GetMachineUseRecordByIdAndSingleDate(string id, DateTime useDate) {
            return MachineBll.GetMachineUseRecordByIdAndSingleDate(id, useDate);
        }

        /// <summary>
        /// 获取水处理机列表
        /// </summary>
        /// <returns></returns>
        public MachineModel.MED_DIALYSIS_MACHINEDataTable GetWaterProcessorList() {
            return MachineBll.GetWaterProcessorList();
        }


        public MachineModel.MED_DIALYSIS_MACHINEDataTable GetMachineByMachineName(string areaId, string machineName)
        {
            return MachineBll.GetMachineByMachineName(areaId, machineName);
        }
        public MachineModel.MED_DIALYSIS_MACHINEDataTable GetMachineListByTypeAndBedId(string type, string bedId) {
            return MachineBll.GetMachineListByTypeAndBedId(type, bedId);
        }


        public int DeleteAirPurgeData(string arrPurgeId) {
            return MachineBll.DeleteAirPurgeData(arrPurgeId);
        }


        public int SaveAirPurgeDataFirstDelete(MachineModel.MED_MACHINE_AIRPURGEDataTable data) {
            return MachineBll.SaveAirPurgeDataFirstDelete(data);
        }

        public MachineModel.MED_PROCESS_SETDataTable GetProcessSetDataById(string id)
        {
            return MachineBll.GetProcessSetDataById(id);
        }
        public MachineModel.MED_PROCESS_SETDataTable GetProcessSetDataList()
        {
            return MachineBll.GetProcessSetDataList();
        }
        public int SaveProcessSetData(MachineModel.MED_PROCESS_SETDataTable dt)
        {
            return MachineBll.SaveProcessSetData(dt);
        }

        public int DeleteProcessSetDataById(string id)
        {
            return MachineBll.DeleteProcessSetDataById(id);
        }

        #region 设备维修保养管理服务类

        public int SaveMED_EQUIPMENT_MGR(MachineModel.MED_EQUIPMENT_MGRDataTable dt)
        {
            return MachineBll.SaveMED_EQUIPMENT_MGR(dt);
        }


        public int DeleteMED_EQUIPMENT_MGR(string id)
        {
            return MachineBll.DeleteMED_EQUIPMENT_MGR(id);
        }

        public MachineModel.MED_EQUIPMENT_MGRDataTable SelectMED_EQUIPMENT_MGRByTime(DateTime beginTime, DateTime endTime)
        {
            return MachineBll.SelectMED_EQUIPMENT_MGRByTime(beginTime, endTime);
        }

        public int SaveMED_EQUIPMENT_MAINTENANCE(MachineModel.MED_EQUIPMENT_MAINTENANCEDataTable dt)
        {
            return MachineBll.SaveMED_EQUIPMENT_MAINTENANCE(dt);
        }


        public int DeleteMED_EQUIPMENT_MAINTENANCE(string id)
        {
            return MachineBll.DeleteMED_EQUIPMENT_MAINTENANCE(id);
        }

        public MachineModel.MED_EQUIPMENT_MAINTENANCEDataTable SelectMED_EQUIPMENT_MAINTENANCEByTime(DateTime beginTime, DateTime endTime)
        {
            return MachineBll.SelectMED_EQUIPMENT_MAINTENANCEByTime(beginTime, endTime);
        }

        public int SaveMED_EQUIPMENT_REPAIR(MachineModel.MED_EQUIPMENT_REPAIRDataTable dt)
        {
            return MachineBll.SaveMED_EQUIPMENT_REPAIR(dt);
        }


        public int DeleteMED_EQUIPMENT_REPAIR(string id)
        {
            return MachineBll.DeleteMED_EQUIPMENT_REPAIR(id);
        }

        public MachineModel.MED_EQUIPMENT_REPAIRDataTable SelectMED_EQUIPMENT_REPAIRByTime(DateTime beginTime, DateTime endTime)
        {
            return MachineBll.SelectMED_EQUIPMENT_REPAIRByTime(beginTime, endTime);
        }
        #endregion

    }
}
