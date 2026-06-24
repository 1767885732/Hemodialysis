/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:耗材类
 * 创建标识:吕志强-2013年8月24日
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.DataAccess;

namespace Hemo.Business {
    public class MaterialBll : BaseClass {
        #region 耗材管理
        /// <summary>
        /// 得到全部耗材列表列表
        /// </summary>
        /// <returns></returns>
        public static MaterialModel.MED_MATERIAL_MASTERDataTable GetMaterialMasterList() {
            MaterialModel.MED_MATERIAL_MASTERDataTable Result = new MaterialModel.MED_MATERIAL_MASTERDataTable();
            return GetData<MaterialModel.MED_MATERIAL_MASTERDataTable>(Result, "GetMaterialMasterList", null);
        }

        /// <summary>
        /// 根据查询参数得到耗材列表数据
        /// </summary>
        /// <param name="pTable">收集的查询条件TABLE</param>
        /// <returns></returns>
        public static MaterialModel.MED_MATERIAL_MASTERDataTable GetMaterialMasterListByParams(MaterialModel.MED_MATERIAL_MASTERDataTable pTable) {
            MaterialModel.MED_MATERIAL_MASTERDataTable Result = new MaterialModel.MED_MATERIAL_MASTERDataTable();
            DbParameter[] Params = new DbParameter[3];
            if (pTable != null && pTable.Rows.Count > 0) {
                Params[0] = IDatabase.BuildDbParameter("MATERIAL_ID", DbType.String, pTable.Rows[0]["MATERIAL_ID"].ToString());
                Params[1] = IDatabase.BuildDbParameter("MATERIAL_NAME", DbType.String, pTable.Rows[0]["MATERIAL_NAME"].ToString());
                //2013-09-04 福总现场OLEDB方式添加的参数
               //Params[2] = IDatabase.BuildDbParameter("MATERIAL_NAME_OLE", DbType.String, pTable.Rows[0]["MATERIAL_NAME"].ToString());
                Params[2] = IDatabase.BuildDbParameter("FIRM_NAME", DbType.String, pTable.Rows[0]["FIRM_NAME"].ToString());
            }
            return GetData<MaterialModel.MED_MATERIAL_MASTERDataTable>(Result, "GetMaterialMasterListByParams", Params);
        }

        public static MaterialScheduleModel.MED_MATERIAL_MASTERDataTable GetMaterialAll()
        {
            MaterialScheduleModel.MED_MATERIAL_MASTERDataTable RESULT = new MaterialScheduleModel.MED_MATERIAL_MASTERDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetMaterialAll");
            database.Fill(sql, RESULT);
            return RESULT;
        }

        /// <summary>
        /// 根据耗材编号得到数据
        /// </summary>
        /// <param name="pMaterialID">耗材编号</param>
        /// <returns></returns>
        public static MaterialModel.MED_MATERIAL_MASTERDataTable GetMaterialMasterListByMaterialID(string pMaterialID) {
            MaterialModel.MED_MATERIAL_MASTERDataTable Result = new MaterialModel.MED_MATERIAL_MASTERDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("MATERIAL_ID", DbType.String, pMaterialID);
            return GetData<MaterialModel.MED_MATERIAL_MASTERDataTable>(Result, "GetMaterialMasterListByMaterialID", Params);
        }

        /// <summary>
        /// 保存耗材资料
        /// </summary>
        /// <param name="pTable">耗材资料记录</param>
        /// <returns></returns>
        public static int SaveMaterialMasterInfo(MaterialModel.MED_MATERIAL_MASTERDataTable pTable) {
            return SaveData<MaterialModel.MED_MATERIAL_MASTERDataTable>(pTable);
        }

        /// <summary>
        /// 得到耗材数量
        /// </summary>
        /// <returns></returns>
        public static int GetMaterialMasterCount() {
            int result = 0;
            string strSql = "SELECT COUNT(*) FROM MED_MATERIAL_MASTER";
            object obj = IDatabase.ExecuteScalar(strSql, null);
            if (obj != null) {
                int.TryParse(obj.ToString(), out result);
            }
            return result;
        }

        /// <summary>
        /// 得到新生成的耗材编号
        /// </summary>
        /// <returns></returns>
        public static string GetNewMaterialID() {
            //初始耗材编号的开始值
            string result = "1000000";
            MaterialModel.MED_MATERIAL_MASTERDataTable Result = new MaterialModel.MED_MATERIAL_MASTERDataTable();
            int iCount = GetMaterialMasterCount();
            if (iCount > 0) {
                result = GetData<MaterialModel.MED_MATERIAL_MASTERDataTable>(Result, "GetNewMaterialID", null).Rows[0][0].ToString();
            }
            return result;
        }
        public static DrugModel.MED_MATERIAL_INPUTDataTable GetMedMaterialInputListNew(DateTime dtMoon)
        {
            var Result = new DrugModel.MED_MATERIAL_INPUTDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("INPUT_DATE", DbType.DateTime, dtMoon);
            return GetData<DrugModel.MED_MATERIAL_INPUTDataTable>(Result, "GetMedMaterialInputListNew", Params); 
        }
        public static int SaveMedMaterialInputNew(DrugModel.MED_MATERIAL_INPUTDataTable data)
        {
            return SaveData<DrugModel.MED_MATERIAL_INPUTDataTable>(data);
 
        }
        public static DrugModel.MED_MATERIAL_OUTPUTDataTable GetMedMaterialOutputListNew(DateTime dtMoon)
        {
            var Result = new DrugModel.MED_MATERIAL_OUTPUTDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("OUPUT_DATE", DbType.DateTime, dtMoon);
            return GetData<DrugModel.MED_MATERIAL_OUTPUTDataTable>(Result, "GetMedMaterialOutputListNew", Params);
        }

        /// <summary>
        /// 入库主汇总表
        /// </summary>
        /// <param name="dtMoon"></param>
        /// <returns></returns>
        public static DrugModel.MED_MATERIAL_INPUTDataTable GetMedMaterialInputMaster(DateTime dtStar, DateTime dtEnd)
        {
            var Result = new DrugModel.MED_MATERIAL_INPUTDataTable();
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("DTSTAR", DbType.DateTime, dtStar);
            Params[1] = IDatabase.BuildDbParameter("DTEND", DbType.DateTime, dtEnd);

            return GetData<DrugModel.MED_MATERIAL_INPUTDataTable>(Result, "GetMedMaterialInputMaster", Params);
        }
        /// <summary>
        /// 入库明细表
        /// </summary>
        /// <param name="dtMoon"></param>
        /// <returns></returns>
        public static DrugModel.MED_MATERIAL_INPUTDataTable GetMedMaterialInputDetail(DateTime dtStar, DateTime dtEnd)
        {
            var Result = new DrugModel.MED_MATERIAL_INPUTDataTable();
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("DTSTAR", DbType.DateTime, dtStar);
            Params[1] = IDatabase.BuildDbParameter("DTEND", DbType.DateTime, dtEnd);
            return GetData<DrugModel.MED_MATERIAL_INPUTDataTable>(Result, "GetMedMaterialInputDetail", Params);
        }
        public static DrugModel.MED_MATERIAL_INPUTDataTable GetMedMaterialInputDetailByCodeAndBatchNum(DateTime dtMonth, string code, string batchNum)
        {
            var Result = new DrugModel.MED_MATERIAL_INPUTDataTable();
            DbParameter[] Params = new DbParameter[3];
            Params[0] = IDatabase.BuildDbParameter("CODE", DbType.String, code);
            Params[1] = IDatabase.BuildDbParameter("BATCH_NUM", DbType.String, batchNum);
            Params[2] = IDatabase.BuildDbParameter("INPUT_DATE", DbType.DateTime, dtMonth);
            return GetData<DrugModel.MED_MATERIAL_INPUTDataTable>(Result, "GetMedMaterialInputDetailByCodeAndBatchNum", Params);
        }
        /// <summary>
        /// 出库明细表
        /// </summary>
        /// <param name="dtMoon"></param>
        /// <returns></returns>
        public static DrugModel.MED_MATERIAL_OUTPUTDataTable GetMedMaterialOutputDetail(DateTime dtMoonStar,DateTime dtEndStar)
        {
            var Result = new DrugModel.MED_MATERIAL_OUTPUTDataTable();
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("DTSTAR", DbType.DateTime, dtMoonStar);
            Params[1] = IDatabase.BuildDbParameter("DTEND", DbType.DateTime, dtEndStar);

            return GetData<DrugModel.MED_MATERIAL_OUTPUTDataTable>(Result, "GetMedMaterialOutputDetail", Params);
        }
        public static DrugModel.MED_MATERIAL_OUTPUTDataTable GetMedMaterialOutputDetailByCodeAndBatchNum(DateTime dtMonth, string code, string batchNum)
        {
            var Result = new DrugModel.MED_MATERIAL_OUTPUTDataTable();
            DbParameter[] Params = new DbParameter[3];
            Params[0] = IDatabase.BuildDbParameter("CODE", DbType.String, code);
            Params[1] = IDatabase.BuildDbParameter("BATCH_NUM", DbType.String, batchNum);
            Params[2] = IDatabase.BuildDbParameter("OUPUT_DATE", DbType.DateTime, dtMonth);
            return GetData<DrugModel.MED_MATERIAL_OUTPUTDataTable>(Result, "GetMedMaterialOutputDetailByCodeAndBatchNum", Params);
        }
        /// <summary>
        /// 出库汇总表
        /// </summary>
        /// <param name="dtMoon"></param>
        /// <returns></returns>
        public static DrugModel.MED_MATERIAL_OUTPUTDataTable GetMedMaterialOutputMaster(DateTime dtMoonStar,DateTime dtEndStar)
        {
            var Result = new DrugModel.MED_MATERIAL_OUTPUTDataTable();
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("DTSTAR", DbType.DateTime, dtMoonStar);
            Params[1] = IDatabase.BuildDbParameter("DTEND", DbType.DateTime, dtEndStar);

            return GetData<DrugModel.MED_MATERIAL_OUTPUTDataTable>(Result, "GetMedMaterialOutputMaster", Params);
        }
        public static int SaveMedMaterialOutputNew(DrugModel.MED_MATERIAL_OUTPUTDataTable data)
        {
            return SaveData<DrugModel.MED_MATERIAL_OUTPUTDataTable>(data);

        }
        public static DrugModel.MED_MATERIAL_INPUTMASTERDataTable GetMedMaterialListByTypeId(string modetypeId)
        {
            var Result = new DrugModel.MED_MATERIAL_INPUTMASTERDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetMedMaterialListByTypeId");
            if (string.IsNullOrEmpty(modetypeId))
                modetypeId = "ALL";
            sql = sql.Replace(":MODETYPE", modetypeId);
            return database.Fill(sql, Result) as DrugModel.MED_MATERIAL_INPUTMASTERDataTable;
        }
        /// <summary>
        /// 删除耗材
        /// </summary>
        /// <returns></returns>
        public static int DeleteMaterialInfo(string pMaterialID)
        {
            //var Result = 0;
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("MaterialID", DbType.String, pMaterialID);
            return DeleteData("DeleteMaterialInfo", Params);
            //return GetData(Result, "DeleteMaterialInfo", Params);
        }
        public static int DeleteMaterialInPut(string inputId)
        {
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("ID", DbType.String, inputId);

            return DeleteData("DeleteMaterialInPut", dbParams);

        }

        public static int DeleteMaterialOutPut(string outPutId)
        {
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("ID", DbType.String, outPutId);

            return DeleteData("DeleteMaterialOutPut", dbParams);
        }
        public static System.Data.DataTable GetMaterialStoreInOutByCode(string code)
        {
            var Result = new DataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetMaterialStoreInOutByCode");
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("CODE", DbType.String, code);         

            return database.Fill(sql, Result,dbParams);
        }

        public static DrugModel.MED_MATERIAL_OUTPUTDataTable GetOutPutByCode(string code)
        {
            var Result = new DrugModel.MED_MATERIAL_OUTPUTDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetOutPutByCode");
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("CODE", DbType.String, code);
            database.Fill(sql, Result, dbParams);
            return Result;

        }
        public static int CheckMaterialInOutStore(string checker)
        {
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("CheckMaterialInOutStore");
            sql = sql.Replace(":CHECKER", checker);
            var sqldetail = StoredScript.Get("CheckMaterialInOutStoreDetail");
            sqldetail = sqldetail.Replace(":CHECKER", checker);

            using (var trans = database.CreateDbTransaction())
            {
                try
                {
                    database.ExecuteNonQuery(sql);
                    database.ExecuteNonQuery(sqldetail);
                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    throw e;
                }

            }
            return 1;
        }
        #endregion

        #region 透析耗材领用
        /// <summary>
        /// 透析耗材领用
        /// </summary>
        /// <param name="pTable">透析耗材领用表</param>  
        /// <returns></returns>
        public static int SaveHemoMaterialInfo(MaterialModel.MED_HEMO_MATERIALDataTable pTable) {
            return SaveData<MaterialModel.MED_HEMO_MATERIALDataTable>(pTable);
        }

        /// <summary>
        /// 根据耗材领用编号得到耗材领用数据
        /// </summary>
        /// <param name="pUseMaterialID">耗材领用编号</param>
        /// <returns></returns>
        public static MaterialModel.MED_HEMO_MATERIAL_REPORTDataTable GetMaterialReport(string pUseMaterialID) {
            MaterialModel.MED_HEMO_MATERIAL_REPORTDataTable Result = new MaterialModel.MED_HEMO_MATERIAL_REPORTDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("use_material_id", DbType.String, pUseMaterialID);
            return GetData<MaterialModel.MED_HEMO_MATERIAL_REPORTDataTable>(Result, "GetMaterialReport", Params);
        }

        /// <summary>
        /// 根据耗材领用编号得到耗材领用数据
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <returns></returns>
        public static MaterialModel.MED_MATERIAL_MASTERDataTable GetUseMaterialList(string pHemoID) {
            MaterialModel.MED_MATERIAL_MASTERDataTable Result = new MaterialModel.MED_MATERIAL_MASTERDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("hemodialysis_id", DbType.String, pHemoID);
            return GetData<MaterialModel.MED_MATERIAL_MASTERDataTable>(Result, "GetUseMaterialList", Params);
        }

        public static int SaveFollowUp(DrugModel.MED_PATIENT_FOLLOWUPDataTable data)
        {
            return SaveData<DrugModel.MED_PATIENT_FOLLOWUPDataTable>(data);
        }

        public static DrugModel.MED_PATIENT_FOLLOWUPDataTable GetFollowUpByData(DateTime dtStart, DateTime dtEnd,string hemoId)
        {
            var Result = new DrugModel.MED_PATIENT_FOLLOWUPDataTable();
            DbParameter[] Params = new DbParameter[3];
            Params[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, dtStart);
            Params[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, dtEnd);
            Params[2] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);

            return GetData<DrugModel.MED_PATIENT_FOLLOWUPDataTable>(Result, "GetFollowUpByData", Params);  
        }
        public static DrugModel.MED_PATIENT_FOLLOWUPDataTable GetFollowUpByHemoID(DateTime dtStart, string hemoid)
        {
            var Result = new DrugModel.MED_PATIENT_FOLLOWUPDataTable();
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoid);
            Params[1] = IDatabase.BuildDbParameter("FOLLOWDATE", DbType.DateTime, dtStart);
            return GetData<DrugModel.MED_PATIENT_FOLLOWUPDataTable>(Result, "GetFollowUpByHemoID", Params);  
        }
        #endregion
    }
}
