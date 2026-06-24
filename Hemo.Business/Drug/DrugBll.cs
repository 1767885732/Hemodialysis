/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:DrugBll
 * 创建标识:贺建操-2011年7月27日
 * 
 * 修改时间:2013年12月12日
 * 修改人:顾伟伟
 * 修改描述:增加全局调用方法验证日期格式正确性
 * 
 * 修改时间:2014年4月22日
 * 修改人:吕志强
 * 修改描述:增加全局调用方法验证日期格式正确性
 * 
 * 修改时间:2014年8月29日
 * 修改人:顾伟伟
 * 修改描述:修复系统响应速度慢的问题
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Hemo.Model;
using Hemo.DataAccess;
namespace Hemo.Business {
    public class DrugBll : BaseClass {

        #region 药品主档
        /// <summary>
        /// 得到全部药品主档列表
        /// </summary>
        /// <returns></returns>
        public static DrugModel.MED_DRUG_MASTERDataTable GetDrugMasterList() {
            DrugModel.MED_DRUG_MASTERDataTable Result = new DrugModel.MED_DRUG_MASTERDataTable();
            return GetData<DrugModel.MED_DRUG_MASTERDataTable>(Result, "GetDrugMasterList", null);
        }
        /// <summary>
        /// 得到托管药品主档列表
        /// </summary>
        /// <returns></returns>
        public static DrugModel.MED_DRUG_MASTERDataTable GetDaleGateDrugMasterList()
        {
            DrugModel.MED_DRUG_MASTERDataTable Result = new DrugModel.MED_DRUG_MASTERDataTable();
            return GetData<DrugModel.MED_DRUG_MASTERDataTable>(Result, "GetDaleGateDrugMasterList", null);
        }
        /// <summary>
        /// 根据查询参数得到药品主挡数据
        /// </summary>
        /// <param name="pTable">收集的查询条件TABLE</param>
        /// <returns></returns>
        public static DrugModel.MED_DRUG_MASTERDataTable GetDrugMasterListByParams(DrugModel.MED_DRUG_MASTERDataTable pTable) {
            DrugModel.MED_DRUG_MASTERDataTable Result = new DrugModel.MED_DRUG_MASTERDataTable();
            DbParameter[] Params = new DbParameter[3];
            if (pTable != null && pTable.Rows.Count > 0) {
                Params[0] = IDatabase.BuildDbParameter("DRUG_CODE", DbType.String, pTable.Rows[0]["DRUG_CODE"].ToString());
                Params[1] = IDatabase.BuildDbParameter("DRUG_NAME", DbType.String, pTable.Rows[0]["DRUG_NAME"].ToString());
                //2013-09-04 福总现场OLEDB方式添加的参数
               // Params[2] = IDatabase.BuildDbParameter("DRUG_NAME_OLE", DbType.String, pTable.Rows[0]["DRUG_NAME"].ToString());
                Params[2] = IDatabase.BuildDbParameter("FIRM_ID", DbType.String, pTable.Rows[0]["FIRM_ID"].ToString());
            }
            return GetData<DrugModel.MED_DRUG_MASTERDataTable>(Result, "GetDrugMasterListByParams", Params);
        }

        /// <summary>
        /// 根据药品编号得到数据
        /// </summary>
        /// <param name="pDrugCode">药品编号</param>
        /// <returns></returns>
        public static DrugModel.MED_DRUG_MASTERDataTable GetDrugMasterListByDrugCode(string pDrugCode) {
            DrugModel.MED_DRUG_MASTERDataTable Result = new DrugModel.MED_DRUG_MASTERDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("DRUG_CODE", DbType.String, pDrugCode);
            return GetData<DrugModel.MED_DRUG_MASTERDataTable>(Result, "GetDrugMasterListByDrugCode", Params);
        }

        /// <summary>
        /// 保存药品主档资料
        /// </summary>
        /// <param name="pTable">药品主档记录</param>
        /// <returns></returns>
        public static int SaveDrugMasterInfo(DrugModel.MED_DRUG_MASTERDataTable pTable) {
            return SaveData<DrugModel.MED_DRUG_MASTERDataTable>(pTable);
        }

        /// <summary>
        /// 根据药名从药库同步药
        /// </summary>
        /// <param name="drugName"></param>
        /// <returns></returns>
        public static int DownDrugFromBaseByName(string drugName)
        {
            try
            {
                IDatabase database = DatabaseFactory.Create();
                DbParameter[] Params = new DbParameter[1];
                Params[0] = IDatabase.BuildDbParameter("FIND_NAME", DbType.String, drugName);
                Params[0].Direction = ParameterDirection.Input;
                return database.ExecuteNonQuery("CALL PRO_MED_DRUG_MASTER(:FIND_NAME)", Params);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 得到新生成的药品编号
        /// </summary>
        /// <returns></returns>
        public static string GetNewDrugCode() {
            //初始化药品编号的开始值
            string result = "10000";
            DrugModel.MED_DRUG_MASTERDataTable Result = new DrugModel.MED_DRUG_MASTERDataTable();
            int drugCount = GetDrugMasterList().Rows.Count;
            if (drugCount > 0) {
                result = GetData<DrugModel.MED_DRUG_MASTERDataTable>(Result, "GetNewDrugCode", null).Rows[0][0].ToString();
            }
            return result;
        }
        #endregion

        #region 药厂设置
        /// <summary>
        /// 得到全部厂商列表列表
        /// </summary>
        /// <returns></returns>
        public static DrugModel.MED_DRUG_FIRMDataTable GetDrugFirmList() {
            DrugModel.MED_DRUG_FIRMDataTable Result = new DrugModel.MED_DRUG_FIRMDataTable();
            return GetData<DrugModel.MED_DRUG_FIRMDataTable>(Result, "GetDrugFirmList", null);
        }

        /// <summary>
        /// 根据厂商类别得到厂商列表,
        /// </summary>
        /// <param name="pFirmType">药品厂商:FirmType=0,耗材厂商:FirmType=1</param>
        /// <returns></returns>
        public static DrugModel.MED_DRUG_FIRMDataTable GetDrugFirmListByFirmType(string pFirmType) {
            DrugModel.MED_DRUG_FIRMDataTable Result = new DrugModel.MED_DRUG_FIRMDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("FIRM_TYPE", DbType.String, pFirmType);
            return GetData<DrugModel.MED_DRUG_FIRMDataTable>(Result, "GetDrugFirmListByFirmType", Params);
        }

        /// <summary>
        /// 根据查询参数得到药品厂商数据
        /// </summary>
        /// <param name="pTable">收集的查询条件TABLE</param>
        /// <returns></returns>
        public static DrugModel.MED_DRUG_FIRMDataTable GetDrugFirmListByParams(DrugModel.MED_DRUG_FIRMDataTable pTable) {
            DrugModel.MED_DRUG_FIRMDataTable Result = new DrugModel.MED_DRUG_FIRMDataTable();
            DbParameter[] Params = new DbParameter[7];
            if (pTable != null && pTable.Rows.Count > 0) {
                Params[0] = IDatabase.BuildDbParameter("FIRM_ID", DbType.String, pTable.Rows[0]["FIRM_ID"].ToString());
                Params[1] = IDatabase.BuildDbParameter("FIRM_NAME", DbType.String, pTable.Rows[0]["FIRM_NAME"].ToString());
                Params[2] = IDatabase.BuildDbParameter("FIRM_PINYIN", DbType.String, pTable.Rows[0]["FIRM_NAME"].ToString());
                Params[3] = IDatabase.BuildDbParameter("FIRM_ADDRESS", DbType.String, pTable.Rows[0]["FIRM_ADDRESS"].ToString());
                Params[4] = IDatabase.BuildDbParameter("TELEPHONE", DbType.String, pTable.Rows[0]["TELEPHONE"].ToString());
                Params[5] = IDatabase.BuildDbParameter("MOBILE_PHONE", DbType.String, pTable.Rows[0]["MOBILE_PHONE"].ToString());
                Params[6] = IDatabase.BuildDbParameter("FIRM_TYPE", DbType.String, pTable.Rows[0]["FIRM_TYPE"].ToString());
            }
            return GetData<DrugModel.MED_DRUG_FIRMDataTable>(Result, "GetDrugFirmListByParams", Params);
        }

        /// <summary>
        /// 根据药厂编号得到数据
        /// </summary>
        /// <param name="pDrugCode">药厂编号</param>
        /// <returns></returns>
        public static DrugModel.MED_DRUG_FIRMDataTable GetDrugFrimListByFirmID(string pFirmID) {
            DrugModel.MED_DRUG_FIRMDataTable Result = new DrugModel.MED_DRUG_FIRMDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("FIRM_ID", DbType.String, pFirmID);
            return GetData<DrugModel.MED_DRUG_FIRMDataTable>(Result, "GetDrugFrimListByFirmID", Params);
        }

        /// <summary>
        /// 保存药厂资料
        /// </summary>
        /// <param name="pTable">药厂数据</param>
        /// <returns></returns>
        public static int SaveDrugFirmInfo(DrugModel.MED_DRUG_FIRMDataTable pTable) {
            return SaveData<DrugModel.MED_DRUG_FIRMDataTable>(pTable);
        }
         /// <summary>
        /// 删除药厂信息
        /// </summary>
        /// <param name="pFirmID"></param>
        /// <returns></returns>
        public static int DeleteDrugFirmInfo(string pFirmID)
        {
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("DeleteDrugFirmInfo");
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("FIRM_ID", DbType.String, pFirmID);
            return database.ExecuteNonQuery(sql, Params);
        }
        /// <summary>
        /// 得到新生成的药厂编号
        /// </summary>
        /// <returns></returns>
        public static string GetNewFirmID() {
            //初始药厂编号的开始值
            string result = "10000";
            DrugModel.MED_DRUG_FIRMDataTable Result = new DrugModel.MED_DRUG_FIRMDataTable();
            int drugCount = GetDrugFirmList().Rows.Count;
            if (drugCount > 0) {
                result = GetData<DrugModel.MED_DRUG_FIRMDataTable>(Result, "GetNewFirmID", null).Rows[0][0].ToString();
            }
            return result;
        }
        #endregion

        #region 药品耗材库存管理
        /// <summary>
        /// 保存药品耗材入库信息
        /// </summary>
        /// <param name="pTable">药品耗材入库信息</param>
        /// <returns></returns>
        public static int SaveMedMaterialInput(DrugModel.MED_MATERIAL_INPUTDataTable pTable) {
            return SaveData<DrugModel.MED_MATERIAL_INPUTDataTable>(pTable);
        }

        /// <summary>
        /// 得到入库药品耗材列表信息
        /// </summary>
        /// <returns>返回要耗材入库信息</returns>
        public static DataTable GetMedMaterialInputList() {
            DataTable result = new DataTable();
            return GetData(result, "GetMedMaterialInputList", null);
        }


        /// <summary>
        /// 根据ID得到一条入库耗材信息
        /// </summary>
        /// <param name="pID">ID编号</param>
        /// <returns></returns>
        public static DrugModel.MED_MATERIAL_INPUTDataTable GetMedMaterialInputByID(string pID) {
            DrugModel.MED_MATERIAL_INPUTDataTable Result = new DrugModel.MED_MATERIAL_INPUTDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("ID", DbType.String, pID);
            return GetData<DrugModel.MED_MATERIAL_INPUTDataTable>(Result, "GetMedMaterialInputByID", Params);
        }

        /// <summary>
        /// 根据透析号和药品编号得到入库的价格和数量
        /// </summary>
        /// <param name="pCode">药品编号</param>
        /// <param name="pHemoID">透析号</param>
        /// <returns>入库数量和价格</returns>
        public static DataTable GetMedMaterialInputByHemoIdAndCode(string pCode,string pHemoID) {
            DataTable result = new DataTable();
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("HEMO_ID", DbType.String, pHemoID);
            Params[1] = IDatabase.BuildDbParameter("CODE", DbType.String, pCode);
            return GetData(result, "GetMedMaterialInputByHemoIdAndCode", Params);
        }

 
        /// <summary>
        /// 根据透析号和药品编号得到出库的价格和数量
        /// </summary>
        /// <param name="pCode">药品编号</param>
        /// <param name="pHemoID">透析号</param>
        /// <returns>出库数量和价格</returns>
        public static DataTable GetMedMaterialOutputByHemoIdAndCode(string pCode, string pHemoID) {
            DataTable result = new DataTable();
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("HEMO_ID", DbType.String, pHemoID);
            Params[1] = IDatabase.BuildDbParameter("CODE", DbType.String, pCode);
            return GetData(result, "GetMedMaterialOutputByHemoIdAndCode", Params);
        }


        /// <summary>
        /// 根据透析号和药品编号得到实际库存数量
        /// </summary>
        /// <param name="pCode">药品编号</param>
        /// <param name="pHemoID">透析号</param>
        /// <returns>实际库存数量</returns>
        public static DrugModel.MED_MATERIAL_CHECKDataTable GetMedMaterialCheckByHemoIdAndCode(string pCode, string pHemoID) {
            DrugModel.MED_MATERIAL_CHECKDataTable result = new DrugModel.MED_MATERIAL_CHECKDataTable();
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("HEMO_ID", DbType.String, pHemoID);
            Params[1] = IDatabase.BuildDbParameter("CODE", DbType.String, pCode);
            return GetData<DrugModel.MED_MATERIAL_CHECKDataTable>(result, "GetMedMaterialCheckByHemoIdAndCode", Params);
        }

        /// <summary>
        /// 保存药品耗材出库信息
        /// </summary>
        /// <param name="pTable">药品耗材出库信息</param>
        /// <returns></returns>
        public static int SaveMedMaterialOutput(DrugModel.MED_MATERIAL_OUTPUTDataTable pTable) {
            return SaveData<DrugModel.MED_MATERIAL_OUTPUTDataTable>(pTable);
        }


        /// <summary>
        /// 根据ID得到一条出库耗材信息
        /// </summary>
        /// <param name="pID">ID编号</param>
        /// <returns></returns>
        public static DrugModel.MED_MATERIAL_OUTPUTDataTable GetMedMaterialOutputByID(string pID) {
            DrugModel.MED_MATERIAL_OUTPUTDataTable Result = new DrugModel.MED_MATERIAL_OUTPUTDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("ID", DbType.String, pID);
            return GetData<DrugModel.MED_MATERIAL_OUTPUTDataTable>(Result, "GetMedMaterialOutputByID", Params);
        }


        /// <summary>
        /// 得到出库药品耗材列表信息
        /// </summary>
        /// <returns>返回要耗材出库信息</returns>
        public static DataTable GetMedMaterialOutputList()
        {
            DataTable result = new DataTable();
            return GetData(result, "GetMedMaterialOutputList", null);
        }

        /// <summary>
        /// 保存药品耗材盘点信息
        /// </summary>
        /// <param name="pTable">药品耗材盘点信息</param>
        /// <returns></returns>
        public static int SaveMedMaterialCheck(DrugModel.MED_MATERIAL_CHECKDataTable pTable) 
        {
            return SaveData<DrugModel.MED_MATERIAL_CHECKDataTable>(pTable);
        }

        /// <summary>
        /// 得到库存盘点药品耗材列表信息
        /// </summary>
        /// <returns>返回要耗材盘点信息</returns>
        public static DrugModel.MED_MATERIAL_CHECKDataTable GetMedMaterialCheckList(DateTime dtMoon,string checker)
        {
            DrugModel.MED_MATERIAL_CHECKDataTable result = new DrugModel.MED_MATERIAL_CHECKDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetMedMaterialCheckList");
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("CHECK_DATE", DbType.DateTime, dtMoon);
            string orderStr = "  ORDER BY T.MODETYPE,T.CODE,  T1.MATERIAL_SPEC,t.batch_number";
            if (!string.IsNullOrEmpty(checker.Trim()))
            {
                var whereSql = string.Format("AND MC.CHECKER = '{0}'",  checker);

                sql = string.Format(sql +"  {1}", whereSql,orderStr);
            }
            else
            {
                sql = string.Format(sql + "  {1}", string.Empty, orderStr);
            }
            database.Fill(sql, result, Params);
            return result;
        }
        /// <summary>
        /// 获取所有的盘点记录的时间和日期
        /// </summary>
        /// <returns>数据集合</returns>
        public static DrugModel.MED_MATERIAL_CHECKDataTable GetMedMaterialCheckHisList()
        {
            DrugModel.MED_MATERIAL_CHECKDataTable RESULT = new DrugModel.MED_MATERIAL_CHECKDataTable();
            return GetData(RESULT, "GetMedMaterialCheckHisList", null);
        }

        /// <summary>
        /// 事务更新出库状态和盘库信息表数据
        /// </summary>
        /// <param name="pOutTable">出库表</param>
        /// <param name="pCheckTable">盘库表</param>
        /// <returns></returns>
        public static bool SaveDeleteOutputAndCheckMaterialData(DrugModel.MED_MATERIAL_OUTPUTDataTable pOutTable,DrugModel.MED_MATERIAL_CHECKDataTable pCheckTable) {
            using (DbWrapTransaction transaction = IDatabase.CreateDbTransaction()) {
                try {
                    SaveMedMaterialOutput(pOutTable);
                    SaveMedMaterialCheck(pCheckTable);
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex) {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 事务更新入库状态和盘库信息表数据
        /// </summary>
        /// <param name="pInTable">入库表</param>
        /// <param name="pCheckTable">盘库表</param>
        /// <returns></returns>
        public static bool SaveDeleteInputAndCheckMaterialData(DrugModel.MED_MATERIAL_INPUTDataTable pInTable, DrugModel.MED_MATERIAL_CHECKDataTable pCheckTable) {
            using (DbWrapTransaction transaction = IDatabase.CreateDbTransaction()) {
                try {
                    SaveMedMaterialInput(pInTable);
                    SaveMedMaterialCheck(pCheckTable);
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex) {
                    throw ex;
                }
            }
        }
        #endregion

        #region 单个病人药品出入库管理
        /// <summary>
        /// 根据透析号查询病人药品入库
        /// </summary>
        /// <param name="pID">透析号</param>
        /// <returns></returns>
        public static DataTable QueryPatientDrugInputById(string pID, DateTime beginTime, DateTime endTime)
        {
            DataTable RESULT = new DataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("QueryPatientDrugInputById");
            DbParameter param0 = database.BuildDbParameter("ID", DbType.String, pID.Trim());
            DbParameter param1 = database.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            DbParameter param2 = database.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            database.Fill(sql, RESULT, new DbParameter[] { param0, param1, param2 });
            return RESULT;
        
        }

        /// <summary>
        /// 根据透析号查询病人药品出库
        /// </summary>
        /// <param name="pID">透析号</param>
        /// <returns></returns>
        public static DataTable QueryPatientDrugOutputById(string pID, DateTime beginTime, DateTime endTime)
        {
            DataTable RESULT = new DataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("QueryPatientDrugOutputById");
            DbParameter param0 = database.BuildDbParameter("ID", DbType.String, pID.Trim());
            DbParameter param1 = database.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            DbParameter param2 = database.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            database.Fill(sql, RESULT, new DbParameter[] { param0, param1, param2 });
            return RESULT;

        }
        public static ReportRelationModel.PatientDrugOutPutPrintDataTable QueryPatientDrugOutPutToPrint(string pID, DateTime beginTime, DateTime endTime)
        {
            var RESULT = new ReportRelationModel.PatientDrugOutPutPrintDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("QueryPatientDrugOutPutToPrint");
            DbParameter param0 = database.BuildDbParameter("ID", DbType.String, pID.Trim());
            DbParameter param1 = database.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            DbParameter param2 = database.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            database.Fill(sql, RESULT, new DbParameter[] { param0, param1, param2 });
            return RESULT;
        }
        public static DataTable GetPatientDrugNumberByParam(string pID, string pCode)
        {
            DataTable result = new DataTable();
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("ID", DbType.String, pID);
            Params[1] = IDatabase.BuildDbParameter("CODE", DbType.String, pCode);
            return GetData(result, "GetPatientDrugNumberByParam", Params);
        }

        public static int SavePatientDrugInput(DrugModel.MED_PATIENT_DRUG_INPUTDataTable dt)
        {
            return SaveData<DrugModel.MED_PATIENT_DRUG_INPUTDataTable>(dt);
        }

        public static int SavePatientDrugOutput(DrugModel.MED_PATIENT_DRUG_OUTPUTDataTable dt)
        {
            return SaveData<DrugModel.MED_PATIENT_DRUG_OUTPUTDataTable>(dt);
        }

        public static int UpdatePatientDrugInputByParam(string pID, string pCode,decimal sum)
        {
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("UpdatePatientDrugInputByParam");
            DbParameter[] Params = new DbParameter[3];
            Params[0] = IDatabase.BuildDbParameter("ID", DbType.String, pID);
            Params[1] = IDatabase.BuildDbParameter("CODE", DbType.String, pCode);
            Params[2] = IDatabase.BuildDbParameter("SUM", DbType.Decimal, sum);
            return database.ExecuteNonQuery(sql, Params);
        }
        public static int UpdatePatientDrugInputByOutPutParam(string pID, string pCode, decimal sum,string pInPutId)
        {
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("UpdatePatientDrugInputByOutPutParam");
            DbParameter[] Params = new DbParameter[4];
            Params[0] = IDatabase.BuildDbParameter("ID", DbType.String, pID);
            Params[1] = IDatabase.BuildDbParameter("CODE", DbType.String, pCode);
            Params[2] = IDatabase.BuildDbParameter("SUM", DbType.Decimal, sum);
            Params[3] = IDatabase.BuildDbParameter("INPUT_ID", DbType.String, pInPutId);

            return database.ExecuteNonQuery(sql, Params);
        }
        public static int UpdatePatientDrugInputRemainByParam(string pID, decimal remain)
        {
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("UpdatePatientDrugInputRemainByParam");
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("ID", DbType.String, pID);
            Params[1] = IDatabase.BuildDbParameter("REMAIN", DbType.Decimal, remain);
            return database.ExecuteNonQuery(sql, Params);
        }

        public static int DeletePatientDrugInputByID(string pID)
        {
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("DeletePatientDrugInputByID");
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("ID", DbType.String, pID);
            return database.ExecuteNonQuery(sql, Params);
        }

        public static int DeletePatientDrugOutputByID(string pID)
        {
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("DeletePatientDrugOutputByID");
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("ID", DbType.String, pID);
            return database.ExecuteNonQuery(sql, Params);
        }

        public static int UpdatePatientDrugInputStatusByID(string pID)
        {
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("UpdatePatientDrugInputStatusByID");
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("ID", DbType.String, pID);
            return database.ExecuteNonQuery(sql, Params);
        }

        public static DrugModel.MED_DRUG_MASTERDataTable GetDrugInputList(string currentHemoId)
        {
            DrugModel.MED_DRUG_MASTERDataTable Result = new DrugModel.MED_DRUG_MASTERDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, currentHemoId);

            return GetData<DrugModel.MED_DRUG_MASTERDataTable>(Result, "GetDrugInputList", Params);
        }
        #endregion
    }
}
