/*----------------------------------------------------------------
// Copyright (C) 2005 (苏州)医疗科技发展有限公司
// 文件名：MachineBll.cs
// 文件功能描述：血液透析设备业务逻辑类
// 创建标识：
// 修改时间：2014-5-4
// 修改人：吕志强
// 修改描述：更新获取设备使用费用数据方法
----------------------------------------------------------------*/

using Hemo.Model;
using Hemo.DataAccess;
using System.Data.Common;
using System.Data;
using System;

namespace Hemo.Business.Machine {
    public class MachineBll : BaseClass {
        /// <summary>
        /// 获取GetMachineList
        /// </summary>
        /// <returns></returns>
        public static MachineModel.MED_DIALYSIS_MACHINEDataTable GetMachineList() {
            MachineModel.MED_DIALYSIS_MACHINEDataTable result = new MachineModel.MED_DIALYSIS_MACHINEDataTable();

            return GetData<MachineModel.MED_DIALYSIS_MACHINEDataTable>(result, "GetMachineList", null);
        }

        /// <summary>
        /// MED_DIALYSIS_MACHINEEXTENDDataTable
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static PermissionModel.MED_DIALYSIS_MACHINEEXTENDDataTable GetMachineListByUserID(string userID) {
            PermissionModel.MED_DIALYSIS_MACHINEEXTENDDataTable reultData = new PermissionModel.MED_DIALYSIS_MACHINEEXTENDDataTable();
            IDatabase database = DatabaseFactory.Create();
            DbParameter param0 = database.BuildDbParameter("USER_ID", DbType.String, userID);
            var sql = StoredScript.Get("GetMachineListByUserID");
            database.Fill(sql, reultData, new DbParameter[] { param0 });
            return reultData;
        }

        /// <summary>
        /// MED_DIALYSIS_MACHINEDataTable
        /// </summary>
        /// <returns></returns>
        public static MachineModel.MED_DIALYSIS_MACHINEDataTable GetNewMachineList() {
            MachineModel.MED_DIALYSIS_MACHINEDataTable result = new MachineModel.MED_DIALYSIS_MACHINEDataTable();

            return GetData<MachineModel.MED_DIALYSIS_MACHINEDataTable>(result, "GetNewMachineList", null);
        }

        /// <summary>
        /// 获取对应数据采集的设备列表
        /// </summary>
        /// <returns></returns>
        public static MachineModel.MED_DIALYSIS_MACHINEDataTable GetMachineListForDataGather() {
            MachineModel.MED_DIALYSIS_MACHINEDataTable result = new MachineModel.MED_DIALYSIS_MACHINEDataTable();
            return GetData<MachineModel.MED_DIALYSIS_MACHINEDataTable>(result, "GetMachineListForDataGather", null);
        }

        /// <summary>
        /// SaveMachineInfo
        /// </summary>
        /// <param name="machineDataTable"></param>
        /// <returns></returns>
        public static int SaveMachineInfo(MachineModel.MED_DIALYSIS_MACHINEDataTable machineDataTable) {
            return SaveData<MachineModel.MED_DIALYSIS_MACHINEDataTable>(machineDataTable);
        }

        /// <summary>
        /// MED_MACHINE_USERECORDDataTable
        /// </summary>
        /// <param name="machineID"></param>
        /// <param name="userDay"></param>
        /// <returns></returns>
        public static MachineModel.MED_MACHINE_USERECORDDataTable GetUseDataByParms(string machineID, System.DateTime userDay) {
            MachineModel.MED_MACHINE_USERECORDDataTable reultData = new MachineModel.MED_MACHINE_USERECORDDataTable();
            IDatabase database = DatabaseFactory.Create();
            DbParameter param0 = database.BuildDbParameter("MACHINE_ID", DbType.String, machineID);
            DbParameter param1 = database.BuildDbParameter("USEDATA", DbType.DateTime, userDay);
            var sql = StoredScript.Get("GetUseDataByParms");
            database.Fill(sql, reultData, new DbParameter[] { param0, param1 });


            return reultData;
        }

        /// <summary>
        /// MED_MACHINE_USERECORDDataTable
        /// </summary>
        /// <param name="_areaID"></param>
        /// <param name="_bedID"></param>
        /// <param name="_banchi"></param>
        /// <param name="useMonthy"></param>
        /// <returns></returns>
        public static MachineModel.MED_MACHINE_USERECORDDataTable GetUseAllDataByMachineID(string _areaID, string _bedID, string _banchi, string useMonthy) {
            MachineModel.MED_MACHINE_USERECORDDataTable reultData = new MachineModel.MED_MACHINE_USERECORDDataTable();
            IDatabase database = DatabaseFactory.Create();
            DbParameter param0 = database.BuildDbParameter("DIALYSIS_ROOM_ID", DbType.String, _areaID);
            DbParameter param1 = database.BuildDbParameter("BED_NUMBER", DbType.String, _bedID);
            DbParameter param2 = database.BuildDbParameter("BANCI_ID", DbType.String, _banchi);
            DbParameter param3 = database.BuildDbParameter("USEDATA", DbType.String, useMonthy);
            var sql = StoredScript.Get("GetUseAllDataByMachineID");
            database.Fill(sql, reultData, new DbParameter[] { param0, param1, param2, param3 });
            return reultData;
        }

        /// <summary>
        /// MED_MACHINE_USERECORDDataTable
        /// </summary>
        /// <param name="_machineID"></param>
        /// <param name="useMonthy"></param>
        /// <returns></returns>
        public static MachineModel.MED_MACHINE_USERECORDDataTable GetUseAllDataByMachineIDAndData(string _machineID, DateTime useMonthy) {
            MachineModel.MED_MACHINE_USERECORDDataTable reultData = new MachineModel.MED_MACHINE_USERECORDDataTable();
            IDatabase database = DatabaseFactory.Create();
            DbParameter param0 = database.BuildDbParameter("MACHINE_ID", DbType.String, _machineID);
            DbParameter param1 = database.BuildDbParameter("USEDATA", DbType.DateTime, useMonthy);
            var sql = StoredScript.Get("GetUseAllDataByMachineIDAndData");
            database.Fill(sql, reultData, new DbParameter[] { param0, param1 });
            return reultData;
        }

        /// <summary>
        /// MED_MACHINE_USERECORDDataTable
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static int SaveMachineUserRecord(MachineModel.MED_MACHINE_USERECORDDataTable Data) {
            return SaveData<MachineModel.MED_MACHINE_USERECORDDataTable>(Data);
        }

        /// <summary>
        /// MED_MACHINE_REPAIRSITUATIONDataTable
        /// </summary>
        /// <param name="RepairData"></param>
        /// <returns></returns>
        public static MachineModel.MED_MACHINE_REPAIRSITUATIONDataTable GetDataByDate(DateTime RepairData) {
            var reSult = new MachineModel.MED_MACHINE_REPAIRSITUATIONDataTable();
            IDatabase database = DatabaseFactory.Create();
            DbParameter param = database.BuildDbParameter("USETIME", DbType.Date, RepairData);
            var sql = StoredScript.Get("GetDataByDate");
            database.Fill(sql, reSult, new DbParameter[] { param });
            return reSult;
        }

        /// <summary>
        /// MED_MACHINE_REPAIRSITUATIONDataTable
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static MachineModel.MED_MACHINE_REPAIRSITUATIONDataTable GetAllRepairData(DateTime beginDate, DateTime endDate) {
            var reSult = new MachineModel.MED_MACHINE_REPAIRSITUATIONDataTable();
            IDatabase database = DatabaseFactory.Create();
            DbParameter param0 = database.BuildDbParameter("beginData", DbType.Date, beginDate);
            DbParameter param1 = database.BuildDbParameter("endData", DbType.Date, endDate);
            var sql = StoredScript.Get("GetAllRepairData");
            database.Fill(sql, reSult, new DbParameter[] { param0, param1 });
            return reSult;
        }

        /// <summary>
        /// MED_MACHINE_REPAIRSITUATIONDataTable
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int SaveRepairDatas(MachineModel.MED_MACHINE_REPAIRSITUATIONDataTable date) {
            return SaveData<MachineModel.MED_MACHINE_REPAIRSITUATIONDataTable>(date);
        }

        /// <summary>
        /// MED_MACHINE_REUSABLEDataTable
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int SaveReUsableDatas(MachineModel.MED_MACHINE_REUSABLEDataTable data) {
            return SaveData<MachineModel.MED_MACHINE_REUSABLEDataTable>(data);
        }

        /// <summary>
        /// MED_MACHINE_REUSABLEDataTable
        /// </summary>
        /// <param name="hemoID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="machinetype"></param>
        /// <returns></returns>
        public static MachineModel.MED_MACHINE_REUSABLEDataTable GetReUsableData(string hemoID, DateTime beginDate, DateTime endDate, string machinetype) {
            var resultData = new MachineModel.MED_MACHINE_REUSABLEDataTable();
            IDatabase database = DatabaseFactory.Create();

            DbParameter param0 = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoID);
            DbParameter param1 = database.BuildDbParameter("BEGINDATE", DbType.DateTime, beginDate);
            DbParameter param2 = database.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            DbParameter param3 = database.BuildDbParameter("MACHINETYPE", DbType.String, machinetype);
            var sql = StoredScript.Get("GetReUsableData");
            database.Fill(sql, resultData, new DbParameter[] { param0, param1, param2, param3 });
            return resultData;
        }

        /// <summary>
        /// MED_MACHINE_AIRPURGEDataTable
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int SaveAirPurgeData(MachineModel.MED_MACHINE_AIRPURGEDataTable data) {
            return SaveData<MachineModel.MED_MACHINE_AIRPURGEDataTable>(data);
        }

        /// <summary>
        /// MED_MACHINE_AIRPURGEDataTable
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int SaveAirPurgeDataFirstDelete(MachineModel.MED_MACHINE_AIRPURGEDataTable data) {
            IDatabase database = DatabaseFactory.Create();
            var sql = string.Format("DELETE FROM MED_MACHINE_AIRPURGE WHERE ID='{0}'", data[0].ID);



            using (var trans = database.CreateDbTransaction()) {
                try {
                    database.ExecuteNonQuery(sql);
                    database.Update(data);
                    trans.Commit();
                }
                catch (Exception e) {
                    trans.Rollback();
                    throw e;
                }

            }
            return 1;

        }

        /// <summary>
        /// MED_MACHINE_AIRPURGEDataTable
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="roomID"></param>
        /// <returns></returns>
        public static MachineModel.MED_MACHINE_AIRPURGEDataTable GetAirPurgeData(DateTime beginDate, DateTime endDate, string roomID) {
            var resultData = new MachineModel.MED_MACHINE_AIRPURGEDataTable();
            IDatabase database = DatabaseFactory.Create();

            DbParameter param0 = database.BuildDbParameter("BEGINDATE", DbType.DateTime, beginDate);
            DbParameter param1 = database.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            DbParameter param2 = database.BuildDbParameter("ROOM_ID", DbType.String, roomID);
            var sql = StoredScript.Get("GetAirPurgeData");
            database.Fill(sql, resultData, new DbParameter[] { param0, param1, param2 });
            return resultData;
        }

        /// <summary>
        /// 根据ID获取空气消毒记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MachineModel.MED_MACHINE_AIRPURGEDataTable GetAirPurgeDataById(string id)
        {
            MachineModel.MED_MACHINE_AIRPURGEDataTable dtResult = new MachineModel.MED_MACHINE_AIRPURGEDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("ID", DbType.String, id);
            return GetData<MachineModel.MED_MACHINE_AIRPURGEDataTable>(dtResult, "GetAirPurgeDataById", Params);
        }

        //透析液消毒数据业务
        public static MachineModel.MED_MACHINE_MIXBARRELDataTable GetMixDataByParms(DateTime _beginDate, DateTime _endDate) {
            var resultData = new MachineModel.MED_MACHINE_MIXBARRELDataTable();
            IDatabase database = DatabaseFactory.Create();

            DbParameter param0 = database.BuildDbParameter("BEGINDATE", DbType.DateTime, _beginDate);
            DbParameter param1 = database.BuildDbParameter("ENDDATE", DbType.DateTime, _endDate);
            var sql = StoredScript.Get("GetMixDataByParms");
            database.Fill(sql, resultData, new DbParameter[] { param0, param1 });
            return resultData;
        }

        /// <summary>
        /// MED_MACHINE_MIXBARRELDataTable
        /// </summary>
        /// <param name="_data"></param>
        /// <returns></returns>
        public static int SaveMixBarrelData(MachineModel.MED_MACHINE_MIXBARRELDataTable _data) {
            return SaveData<MachineModel.MED_MACHINE_MIXBARRELDataTable>(_data);
        }

        /// <summary>
        /// MED_MACHINE_MIXBARRELDataTable
        /// </summary>
        /// <param name="_begionDate"></param>
        /// <param name="_endDate"></param>
        /// <param name="_machineID"></param>
        /// <returns></returns>
        public static MachineModel.MED_MACHINE_MIXBARRELDataTable GetWaterHemoDataByParms(DateTime _begionDate, DateTime _endDate, string _machineID) {
            var resultData = new MachineModel.MED_MACHINE_MIXBARRELDataTable();
            IDatabase database = DatabaseFactory.Create();

            DbParameter param0 = database.BuildDbParameter("BEGINDATE", DbType.DateTime, _begionDate);
            DbParameter param1 = database.BuildDbParameter("ENDDATE", DbType.DateTime, _endDate);
            DbParameter param2 = database.BuildDbParameter("MACHINE", DbType.String, _machineID);
            var sql = StoredScript.Get("GetWaterHemoDataByParms");
            database.Fill(sql, resultData, new DbParameter[] { param0, param1, param2 });
            return resultData;
        }

        /// <summary>
        /// MED_MACHINE_MIXBARRELDataTable
        /// </summary>
        /// <param name="_begionDate"></param>
        /// <param name="_endDate"></param>
        /// <param name="_machineID"></param>
        /// <returns></returns>
        public static MachineModel.MED_MACHINE_MIXBARRELDataTable GetWaterTreatmentDataByParms(DateTime _begionDate, DateTime _endDate, string _machineID) {
            var resultData = new MachineModel.MED_MACHINE_MIXBARRELDataTable();
            IDatabase database = DatabaseFactory.Create();

            DbParameter param0 = database.BuildDbParameter("BEGINDATE", DbType.DateTime, _begionDate);
            DbParameter param1 = database.BuildDbParameter("ENDDATE", DbType.DateTime, _endDate);
            DbParameter param2 = database.BuildDbParameter("MACHINE", DbType.String, _machineID);
            var sql = StoredScript.Get("GetWaterTreatmentDataByParms");
            database.Fill(sql, resultData, new DbParameter[] { param0, param1, param2 });
            return resultData;
        }

        /// <summary>
        /// MED_MACHINE_MIXBARRELDataTable
        /// </summary>
        /// <param name="_begionDate"></param>
        /// <param name="_endDate"></param>
        /// <param name="_machineID"></param>
        /// <returns></returns>
        public static MachineModel.MED_MACHINE_MIXBARRELDataTable GetHosEquipmentDataByParms(DateTime _begionDate, DateTime _endDate, string _machineID) {
            var resultData = new MachineModel.MED_MACHINE_MIXBARRELDataTable();
            IDatabase database = DatabaseFactory.Create();

            DbParameter param0 = database.BuildDbParameter("BEGINDATE", DbType.DateTime, _begionDate);
            DbParameter param1 = database.BuildDbParameter("ENDDATE", DbType.DateTime, _endDate);
            DbParameter param2 = database.BuildDbParameter("MACHINE", DbType.String, _machineID);
            var sql = StoredScript.Get("GetHosEquipmentDataByParms");
            database.Fill(sql, resultData, new DbParameter[] { param0, param1, param2 });
            return resultData;
        }

        /// <summary>
        /// 保存血透机使用费用数据（BY 周祥）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int SaveUseFeeData(MachineModel.MED_MACHINE_USEFEEDataTable data) {
            return SaveData<MachineModel.MED_MACHINE_USEFEEDataTable>(data);
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
        public static DataTable GetUseFeeData(string sickArea, string bedNo, DateTime beginDate, DateTime endDate) {
            var resultData = new DataTable();
            resultData.TableName = "合并列数据表";
            IDatabase database = DatabaseFactory.Create();

            DbParameter param0 = database.BuildDbParameter("DIALYSIS_ROOM_ID", DbType.String, sickArea);
            DbParameter param1 = database.BuildDbParameter("BED_NUMBER", DbType.String, bedNo);

            DbParameter param2 = database.BuildDbParameter("beginData", DbType.DateTime, beginDate);
            DbParameter param3 = database.BuildDbParameter("endData", DbType.DateTime, endDate);

            var sql = StoredScript.Get("GetUSEFEEData");
            database.Fill(sql, resultData, new DbParameter[] { param0, param1, param2, param3 });
            return resultData;
        }

        /// <summary>
        /// 根据开始时间和结束时间获取血透机使用费用统计数据（BY 周祥）
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DataTable GetUseFeeStatisticsData(DateTime beginDate, DateTime endDate) {
            var resultData = new DataTable();
            resultData.TableName = "统计数据表";
            IDatabase database = DatabaseFactory.Create();

            DbParameter param0 = database.BuildDbParameter("beginData", DbType.DateTime, beginDate);
            DbParameter param1 = database.BuildDbParameter("endData", DbType.DateTime, endDate);
            var sql = StoredScript.Get("GetUSEFEEStatisticsData");
            database.Fill(sql, resultData, new DbParameter[] { param0, param1 });
            return resultData;
        }

        /// <summary>
        /// 根据唯一ID获取血透机使用费用数据（BY 周祥）
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static MachineModel.MED_MACHINE_USEFEEDataTable GetUseFeeData(string id) {
            var resultData = new MachineModel.MED_MACHINE_USEFEEDataTable();
            IDatabase database = DatabaseFactory.Create();

            DbParameter param0 = database.BuildDbParameter("id", DbType.String, id);
            var sql = StoredScript.Get("GetUSEFEEDataById");
            database.Fill(sql, resultData, new DbParameter[] { param0 });
            return resultData;
        }

        /// <summary>
        /// 保存医疗设备主机登记数据（BY 周祥）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int SaveMainframeData(MachineModel.MED_MACHINE_MAINFRAMEDataTable data) {
            return SaveData<MachineModel.MED_MACHINE_MAINFRAMEDataTable>(data);
        }

        /// <summary>
        /// 获取医疗设备主机登记数据（BY 周祥）
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static MachineModel.MED_MACHINE_MAINFRAMEDataTable GetMainframeData() {
            var resultData = new MachineModel.MED_MACHINE_MAINFRAMEDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetMainframeData");
            database.Fill(sql, resultData);
            return resultData;
        }

        /// <summary>
        /// 保存医疗设备主机附属设备数据（BY 周祥）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int SaveAccessoryEquipData(MachineModel.MED_MACHINE_ACCESSORYEQUIPDataTable data) {
            return SaveData<MachineModel.MED_MACHINE_ACCESSORYEQUIPDataTable>(data);
        }

        /// <summary>
        /// 获取医疗设备主机附属设备数据（BY 周祥）
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static MachineModel.MED_MACHINE_ACCESSORYEQUIPDataTable GetAccessoryEquipData(string mainframeId) {
            var resultData = new MachineModel.MED_MACHINE_ACCESSORYEQUIPDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetAccessoryEquipData");
            DbParameter param0 = database.BuildDbParameter("mainframeId", DbType.String, mainframeId);
            database.Fill(sql, resultData, new DbParameter[] { param0 });
            return resultData;
        }

        /// <summary>
        /// 根据类型获取机器列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static MachineModel.MED_DIALYSIS_MACHINEDataTable GetMachineListByType(string type) {
            MachineModel.MED_DIALYSIS_MACHINEDataTable dtMachine = new MachineModel.MED_DIALYSIS_MACHINEDataTable();
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("TYPE", DbType.String, type);
            return GetData<MachineModel.MED_DIALYSIS_MACHINEDataTable>(dtMachine, "GetMachineListByType", dbParam);
        }

        /// <summary>
        /// 根据类型获取机器列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static MachineModel.MED_DIALYSIS_MACHINEDataTable GetWaterMachineListByType(string type) {
            MachineModel.MED_DIALYSIS_MACHINEDataTable dtMachine = new MachineModel.MED_DIALYSIS_MACHINEDataTable();
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("TYPE", DbType.String, type);
            return GetData<MachineModel.MED_DIALYSIS_MACHINEDataTable>(dtMachine, "GetWaterMachineListByType", dbParam);
        }

        /// <summary>
        /// 根据机器ID&日期获取水处理记录列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static MachineModel.MED_WATERPROCESSOR_USERECORDDataTable GetWaterProcessorRecordByIdAndDate(string id, DateTime beginDate, DateTime endDate) {
            MachineModel.MED_WATERPROCESSOR_USERECORDDataTable dtRecord = new MachineModel.MED_WATERPROCESSOR_USERECORDDataTable();
            DbParameter[] dbParam = new DbParameter[3];
            dbParam[0] = IDatabase.BuildDbParameter("MACHINE_ID", DbType.String, id);
            dbParam[1] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, beginDate);
            dbParam[2] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            return GetData<MachineModel.MED_WATERPROCESSOR_USERECORDDataTable>(dtRecord, "GetWaterProcessorRecordByIdAndDate", dbParam);
        }

        /// <summary>
        /// 根据机器ID&单个日期获取水处理记录列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="useDate"></param>
        /// <returns></returns>
        public static MachineModel.MED_WATERPROCESSOR_USERECORDDataTable GetWaterProcessorRecordByIdAndSingleDate(string id, DateTime useDate) {
            MachineModel.MED_WATERPROCESSOR_USERECORDDataTable dtRecord = new MachineModel.MED_WATERPROCESSOR_USERECORDDataTable();
            DbParameter[] dbParam = new DbParameter[2];
            dbParam[0] = IDatabase.BuildDbParameter("MACHINE_ID", DbType.String, id);
            dbParam[1] = IDatabase.BuildDbParameter("USEDATE", DbType.DateTime, useDate);
            return GetData<MachineModel.MED_WATERPROCESSOR_USERECORDDataTable>(dtRecord, "GetWaterProcessorRecordByIdAndSingleDate", dbParam);
        }

        /// <summary>
        /// 根据机器ID获取机器信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MachineModel.MED_DIALYSIS_MACHINEDataTable GetMachineById(string id) {
            MachineModel.MED_DIALYSIS_MACHINEDataTable dtMachine = new MachineModel.MED_DIALYSIS_MACHINEDataTable();
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("MACHINE_ID", DbType.String, id);
            return GetData<MachineModel.MED_DIALYSIS_MACHINEDataTable>(dtMachine, "GetMachineById", dbParam);
        }

        /// <summary>
        /// 根据机器ID获取机器信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MachineModel.MED_DIALYSIS_MACHINEDataTable GetWaterMachineById(string id) {
            MachineModel.MED_DIALYSIS_MACHINEDataTable dtMachine = new MachineModel.MED_DIALYSIS_MACHINEDataTable();
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("MACHINE_ID", DbType.String, id);
            return GetData<MachineModel.MED_DIALYSIS_MACHINEDataTable>(dtMachine, "GetWaterMachineById", dbParam);
        }

        /// <summary>
        /// 保存水处理记录列表
        /// </summary>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        public static int SaveWaterProcessorRecord(MachineModel.MED_WATERPROCESSOR_USERECORDDataTable dtRecord) {
            return SaveData<MachineModel.MED_WATERPROCESSOR_USERECORDDataTable>(dtRecord);
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
        public static MachineModel.MED_MACHINE_USERECORDDataTable GetMachineUseRecordList(string area, string bed, string banCi, DateTime beginDate, DateTime endDate)
        {
            MachineModel.MED_MACHINE_USERECORDDataTable dtRecord = new MachineModel.MED_MACHINE_USERECORDDataTable();
            DbParameter[] dbParam = new DbParameter[5];
            dbParam[0] = IDatabase.BuildDbParameter("DIALYSIS_ROOM_ID", DbType.String, area);
            dbParam[1] = IDatabase.BuildDbParameter("BED_NUMBER", DbType.String, bed);
            dbParam[2] = IDatabase.BuildDbParameter("BANCI_ID", DbType.String, banCi);
            dbParam[3] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, beginDate);
            dbParam[4] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            return GetData<MachineModel.MED_MACHINE_USERECORDDataTable>(dtRecord, "GetMachineUseRecordList", dbParam);
        }

        /// <summary>
        /// 根据机器ID&日期获取血透机运行记录列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static MachineModel.MED_MACHINE_USERECORDDataTable GetMachineUseRecordByIdAndDate(string id, DateTime beginDate, DateTime endDate) {
            MachineModel.MED_MACHINE_USERECORDDataTable dtRecord = new MachineModel.MED_MACHINE_USERECORDDataTable();
            DbParameter[] dbParam = new DbParameter[3];
            dbParam[0] = IDatabase.BuildDbParameter("MACHINE_ID", DbType.String, id);
            dbParam[1] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, beginDate);
            dbParam[2] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            return GetData<MachineModel.MED_MACHINE_USERECORDDataTable>(dtRecord, "GetMachineUseRecordByIdAndDate", dbParam);
        }

        /// <summary>
        /// 根据签名&日期获取血透机运行记录列表
        /// </summary>
        /// <param name="Signid"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static MachineModel.MED_MACHINE_USERECORDDataTable GetMachineUseRecordBySignIdAndDate(string Signid, DateTime beginDate, DateTime endDate) {
            MachineModel.MED_MACHINE_USERECORDDataTable dtRecord = new MachineModel.MED_MACHINE_USERECORDDataTable();
            DbParameter[] dbParam = new DbParameter[2];
            Signid = string.Empty;
           // dbParam[0] = IDatabase.BuildDbParameter("SIGN_NAME", DbType.String, Signid);
            dbParam[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, beginDate);
            dbParam[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            return GetData<MachineModel.MED_MACHINE_USERECORDDataTable>(dtRecord, "GetMachineUseRecordBySignIdAndDate", dbParam);
        }

        /// <summary>
        /// 根据机器ID&单个日期获取血透机运行记录列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="useDate"></param>
        /// <returns></returns>
        public static MachineModel.MED_MACHINE_USERECORDDataTable GetMachineUseRecordByIdAndSingleDate(string id, DateTime useDate) {
            MachineModel.MED_MACHINE_USERECORDDataTable dtRecord = new MachineModel.MED_MACHINE_USERECORDDataTable();
            DbParameter[] dbParam = new DbParameter[2];
            dbParam[0] = IDatabase.BuildDbParameter("MACHINE_ID", DbType.String, id);
            dbParam[1] = IDatabase.BuildDbParameter("USEDATE", DbType.DateTime, useDate);
            return GetData<MachineModel.MED_MACHINE_USERECORDDataTable>(dtRecord, "GetMachineUseRecordByIdAndSingleDate", dbParam);
        }

        /// <summary>
        /// 获取水处理机列表
        /// </summary>
        /// <returns></returns>
        public static MachineModel.MED_DIALYSIS_MACHINEDataTable GetWaterProcessorList() {
            MachineModel.MED_DIALYSIS_MACHINEDataTable result = new MachineModel.MED_DIALYSIS_MACHINEDataTable();

            return GetData<MachineModel.MED_DIALYSIS_MACHINEDataTable>(result, "GetWaterProcessorList", null);
        }

        /// <summary>
        /// 根据机器ID获取机器信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MachineModel.MED_DIALYSIS_MACHINEDataTable GetMachineByMachineName(string areaId, string machineName)
        {
            MachineModel.MED_DIALYSIS_MACHINEDataTable dtMachine = new MachineModel.MED_DIALYSIS_MACHINEDataTable();
            DbParameter[] dbParam = new DbParameter[2];
            dbParam[0] = IDatabase.BuildDbParameter("AREA_ID", DbType.String, areaId);
            dbParam[1] = IDatabase.BuildDbParameter("MACHINE_NAME", DbType.String, machineName);
            return GetData<MachineModel.MED_DIALYSIS_MACHINEDataTable>(dtMachine, "GetMachineByMachineName", dbParam);
        }
        /// <summary>
        /// 根据类型、床位获取机器列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="bedId"></param>
        /// <returns></returns>
        public static MachineModel.MED_DIALYSIS_MACHINEDataTable GetMachineListByTypeAndBedId(string type, string bedId) {
            MachineModel.MED_DIALYSIS_MACHINEDataTable dtMachine = new MachineModel.MED_DIALYSIS_MACHINEDataTable();
            DbParameter[] dbParam = new DbParameter[2];
            dbParam[0] = IDatabase.BuildDbParameter("TYPE", DbType.String, type);
            dbParam[1] = IDatabase.BuildDbParameter("BED_ID", DbType.String, bedId);
            return GetData<MachineModel.MED_DIALYSIS_MACHINEDataTable>(dtMachine, "GetMachineListByTypeAndBedId", dbParam);
        }
        public static int DeleteAirPurgeData(string arrPurgeId) {
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("DeleteAirPurgeData");
            DbParameter param0 = database.BuildDbParameter("ID", DbType.String, arrPurgeId);
            return database.ExecuteNonQuery(sql, new DbParameter[] { param0 });
        }
        /// <summary>
        /// 保存流程节点明细
        /// </summary>
        /// <param name="dtMPS"></param>
        /// <returns></returns>
        public static int SaveProcessSetData(MachineModel.MED_PROCESS_SETDataTable dtMPS)
        {
            return SaveData<MachineModel.MED_PROCESS_SETDataTable>(dtMPS);
        }
        /// <summary>
        /// 根据ID获取流程节点明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MachineModel.MED_PROCESS_SETDataTable GetProcessSetDataById(string id)
        {
            MachineModel.MED_PROCESS_SETDataTable dt = new MachineModel.MED_PROCESS_SETDataTable();
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("id", DbType.String, id);
            return GetData<MachineModel.MED_PROCESS_SETDataTable>(dt, "GetProcessSetDataById", dbParam);
        }
        /// <summary>
        /// 获取流程节点明细列表
        /// </summary>
        /// <returns></returns>
        public static MachineModel.MED_PROCESS_SETDataTable GetProcessSetDataList()
        {
            MachineModel.MED_PROCESS_SETDataTable dt = new MachineModel.MED_PROCESS_SETDataTable();
            return GetData<MachineModel.MED_PROCESS_SETDataTable>(dt, "GetProcessSetDataList", null);
        }
        /// <summary>
        /// 根据ID删除流程节点明细资料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteProcessSetDataById(string id)
        {
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("DeleteProcessSetDataById");
            DbParameter param0 = database.BuildDbParameter("id", DbType.String, id);
            return database.ExecuteNonQuery(sql, new DbParameter[] { param0 });
        }

        /// <summary>
        /// 保存机器设备信息
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int SaveMED_EQUIPMENT_MGR(MachineModel.MED_EQUIPMENT_MGRDataTable dt)
        {
            return SaveData<MachineModel.MED_EQUIPMENT_MGRDataTable>(dt);
        }

        /// <summary>
        /// 删除机器设备信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteMED_EQUIPMENT_MGR(string id)
        {
            var sql = StoredScript.Get("DeleteMED_EQUIPMENT_MGR");
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("id", DbType.String, id);
            return IDatabase.ExecuteNonQuery(sql, dbParam);
        }

        /// <summary>
        /// 选择机器设备
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static MachineModel.MED_EQUIPMENT_MGRDataTable SelectMED_EQUIPMENT_MGRByTime(DateTime beginTime, DateTime endTime)
        {
            MachineModel.MED_EQUIPMENT_MGRDataTable dt = new MachineModel.MED_EQUIPMENT_MGRDataTable();
            var sql = StoredScript.Get("SelectMED_EQUIPMENT_MGRByTime");
            DbParameter[] dbParam = new DbParameter[2];
            dbParam[0] = IDatabase.BuildDbParameter("beginTime", DbType.DateTime, beginTime);
            dbParam[1] = IDatabase.BuildDbParameter("endTime", DbType.DateTime, endTime);
            IDatabase.Fill(sql, dt, dbParam);
            return dt;    
        }

        /// <summary>
        /// 保存设备保养信息
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int SaveMED_EQUIPMENT_MAINTENANCE(MachineModel.MED_EQUIPMENT_MAINTENANCEDataTable dt)
        {
            return SaveData<MachineModel.MED_EQUIPMENT_MAINTENANCEDataTable>(dt);
        }

        /// <summary>
        /// 删除设备保养信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteMED_EQUIPMENT_MAINTENANCE(string id)
        {
            var sql = StoredScript.Get("DeleteMED_EQUIPMENT_MAINTENANCE");
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("id", DbType.String, id);
            return IDatabase.ExecuteNonQuery(sql, dbParam);
        }

        /// <summary>
        /// 选择设备保养信息
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static MachineModel.MED_EQUIPMENT_MAINTENANCEDataTable SelectMED_EQUIPMENT_MAINTENANCEByTime(DateTime beginTime, DateTime endTime)
        {
            MachineModel.MED_EQUIPMENT_MAINTENANCEDataTable dt = new MachineModel.MED_EQUIPMENT_MAINTENANCEDataTable();
            var sql = StoredScript.Get("SelectMED_EQUIPMENT_MAINTENANCEByTime");
            DbParameter[] dbParam = new DbParameter[2];
            dbParam[0] = IDatabase.BuildDbParameter("beginTime", DbType.DateTime, beginTime);
            dbParam[1] = IDatabase.BuildDbParameter("endTime", DbType.DateTime, endTime);
            IDatabase.Fill(sql, dt, dbParam);
            return dt;
        }

        /// <summary>
        /// 保存设备维修信息
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int SaveMED_EQUIPMENT_REPAIR(MachineModel.MED_EQUIPMENT_REPAIRDataTable dt)
        {
            return SaveData<MachineModel.MED_EQUIPMENT_REPAIRDataTable>(dt);
        }

        /// <summary>
        /// 删除设备保养信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteMED_EQUIPMENT_REPAIR(string id)
        {
            var sql = StoredScript.Get("DeleteMED_EQUIPMENT_REPAIR");
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("id", DbType.String, id);
            return IDatabase.ExecuteNonQuery(sql, dbParam);
        }

        /// <summary>
        /// 选择设备维修信息
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static MachineModel.MED_EQUIPMENT_REPAIRDataTable SelectMED_EQUIPMENT_REPAIRByTime(DateTime beginTime, DateTime endTime)
        {
            MachineModel.MED_EQUIPMENT_REPAIRDataTable dt = new MachineModel.MED_EQUIPMENT_REPAIRDataTable();
            var sql = StoredScript.Get("SelectMED_EQUIPMENT_REPAIRByTime");
            DbParameter[] dbParam = new DbParameter[2];
            dbParam[0] = IDatabase.BuildDbParameter("beginTime", DbType.DateTime, beginTime);
            dbParam[1] = IDatabase.BuildDbParameter("endTime", DbType.DateTime, endTime);
            IDatabase.Fill(sql, dt, dbParam);
            return dt;
        }
    }
}
