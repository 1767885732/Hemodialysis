/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:Hemo.Business.Config
 * 创建标识:吕志强-2013年8月2日
 * 
 * 修改时间:2013年12月18日
 * 修改人:贺建操
 * 修改描述:修复系统响应速度慢的问题
 * 
 * 修改时间:2014年4月28日
 * 修改人:顾伟伟
 * 修改描述:通用窗体取值、赋值、清空值、验证控件值方法
 * 
 * 修改时间:2014年9月4日
 * 修改人:顾伟伟
 * 修改描述:增加全局调用方法验证日期格式正确性
 * ----------------------------------------------------------------*/
using System.Data;
using System.Data.Common;
using Hemo.Model;
using Hemo.DataAccess;
using System;

namespace Hemo.Business.Config
{
    public class ConfigBll : BaseClass
    {
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <returns></returns>

        public static ConfigModel.MED_COMMON_ITEMLISTDataTable GetConfigList(string value, string name, string type, string status)
        {
            //ConfigModel.MED_COMMON_ITEMLISTDataTable result = new ConfigModel.MED_COMMON_ITEMLISTDataTable();

            //DbParameter[] dbParams = new DbParameter[4];
            //dbParams[0] = IDatabase.BuildDbParameter("ITEM_VALUE", DbType.String, value.Trim());
            //dbParams[1] = IDatabase.BuildDbParameter("ITEM_NAME", DbType.String, name.Trim());
            //dbParams[2] = IDatabase.BuildDbParameter("ITEM_TYPE", DbType.String, type.Trim());
            //dbParams[3] = IDatabase.BuildDbParameter("STATUS", DbType.String, status.Trim());
            ////2013-09-04 福总现场OLEDB方式添加的参数
            ////dbParams[4] = IDatabase.BuildDbParameter("STATUS_OLE", DbType.String, status.Trim());

            //return GetData<ConfigModel.MED_COMMON_ITEMLISTDataTable>(result, "GetConfigList", dbParams);

            ConfigModel.MED_COMMON_ITEMLISTDataTable result = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetConfigList");
            if (type.Trim() == "床位")
            {
                sql = sql + "   ORDER BY   t.ITEM_TYPE, t.ORDER_NUMBER --to_number(t.item_value)";
            }
            else
            {
                sql = sql + "    ORDER BY   t.ITEM_TYPE, t.ORDER_NUMBER";
            }

            var dbParams0 = database.BuildDbParameter("ITEM_VALUE", DbType.String, value.Trim());
            var dbParams1 = database.BuildDbParameter("ITEM_NAME", DbType.String, name.Trim());
            var dbParams2 = database.BuildDbParameter("ITEM_TYPE", DbType.String, type.Trim());
            var dbParams3 = database.BuildDbParameter("STATUS", DbType.String, status.Trim());
            //2013-09-04 福总现场OLEDB方式添加的参数
            //var dbParams4 = database.BuildDbParameter("STATUS_OLE", DbType.String, status.Trim());
            database.Fill(sql, result, new DbParameter[] { dbParams0, dbParams1, dbParams2, dbParams3 });
            return result;

        }

        /// <summary>
        /// 获取配置列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ConfigModel.MED_COMMON_ITEMLISTDataTable GetItemListByItemType(string type)
        {
            ConfigModel.MED_COMMON_ITEMLISTDataTable result = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetFeeItemConfigList");
            var dbParam = database.BuildDbParameter("ITEM_TYPE", DbType.String, type.Trim());
            database.Fill(sql, result, new DbParameter[] { dbParam });
            return result;
        }

        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <param name="configDataTable"></param>
        /// <returns></returns>
        public static int SaveConfigInfo(ConfigModel.MED_COMMON_ITEMLISTDataTable configDataTable)
        {
            return SaveData<ConfigModel.MED_COMMON_ITEMLISTDataTable>(configDataTable);
        }
        /// <summary>
        /// 保存信息
        /// </summary>
        /// <param name="functioDataTable"></param>
        /// <returns></returns>
        public static int SaveMedFuncitonCount(ConfigModel.MED_FUNCION_COUNTDataTable functioDataTable)
        {
            return SaveData<ConfigModel.MED_FUNCION_COUNTDataTable>(functioDataTable);
        }
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetConfigTypeList()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ITEM_TYPE"));
            dt.Columns.Add(new DataColumn("Count"));

            GetData(dt, "GetConfigTypeList", null);

            return dt;
        }

        /// <summary>
        /// 获取质控上传项与平台的匹配项目
        /// </summary>
        /// <param name="UPTYPE"></param>
        /// <returns></returns>
        public static ConfigModel.MED_UPTYPE_MGRDataTable GET_MED_UPTYPE_MGR(string UPTYPE)
        {

            ConfigModel.MED_UPTYPE_MGRDataTable result = new ConfigModel.MED_UPTYPE_MGRDataTable();
            return GetData<ConfigModel.MED_UPTYPE_MGRDataTable>(result, "GET_MED_UPTYPE_MGR", null);
        }



        /// <summary>
        /// 获取MED_COMMON_RELATION信息
        /// </summary>
        /// <returns></returns>
        public static ConfigModel.MED_COMMON_RELATIONDataTable GetCommRelation()
        {

            ConfigModel.MED_COMMON_RELATIONDataTable result = new ConfigModel.MED_COMMON_RELATIONDataTable();

            return GetData<ConfigModel.MED_COMMON_RELATIONDataTable>(result, "GetCommRelation", null);

        }
        /// <summary>
        /// 保存MED_COMMON_RELATION信息
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int SaveCommonRelation(ConfigModel.MED_COMMON_RELATIONDataTable dt)
        {
            return SaveData<ConfigModel.MED_COMMON_RELATIONDataTable>(dt);
        }

        /// <summary>
        /// 删除DeleteCommonRelationById
        /// </summary>
        /// <param name="relationId"></param>
        /// <returns></returns>
        public static int DeleteCommonRelationById(string relationId)
        {
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("DeleteCommonRelationById");
            DbParameter paramId = database.BuildDbParameter("ID", DbType.String, relationId);
            return database.ExecuteNonQuery(sql, new DbParameter[] { paramId });
        }

        /// <summary>
        /// 根据ID删除科室建制信息
        /// </summary>
        /// <param name="HOSPITAL_ID"></param>
        /// <returns></returns>
        public static int DeleteMED_HOSPITAL_INFOById(string HOSPITAL_ID)
        {
            IDatabase database = DatabaseFactory.Create();
            var sql = string.Format("DELETE FROM MED_HOSPITAL_INFO WHERE HOSPITAL_ID='{0}'", HOSPITAL_ID);
            using (var trans = database.CreateDbTransaction())
            {
                try
                {
                    database.ExecuteNonQuery(sql);
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
        /// <summary>
        /// 保存科室建制信息
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int SaveMED_HOSPITAL_INFO(ConfigModel.MED_HOSPITAL_INFODataTable dt)
        {
            return SaveData<ConfigModel.MED_HOSPITAL_INFODataTable>(dt);
        }
        /// <summary>
        /// 根据ID或者科室建制信息
        /// </summary>
        /// <param name="HOSPITAL_ID"></param>
        /// <returns></returns>
        public static ConfigModel.MED_HOSPITAL_INFODataTable GetMED_HOSPITAL_INFOById(string HOSPITAL_ID)
        {
            var resultData = new ConfigModel.MED_HOSPITAL_INFODataTable();
            IDatabase database = DatabaseFactory.Create();

            DbParameter param0 = database.BuildDbParameter("HOSPITAL_ID", DbType.String, HOSPITAL_ID);
            var sql = StoredScript.Get("GetMED_HOSPITAL_INFOById");
            database.Fill(sql, resultData, new DbParameter[] { param0 });
            return resultData;
        }
        /// <summary>
        /// 获取科室建制信息列表
        /// </summary>
        /// <returns></returns>
        public static ConfigModel.MED_HOSPITAL_INFODataTable GetMED_HOSPITAL_INFOList()
        {

            var resultData = new ConfigModel.MED_HOSPITAL_INFODataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetMED_HOSPITAL_INFOList");
            database.Fill(sql, resultData);
            return resultData;
        }



        /// <summary>
        /// 获取质控平台血透机，血滤机，水处理机的台数和品牌
        /// </summary>
        /// <returns></returns>

        public static DataTable GetQualityControlEquipmentInfo()
        {
            var resultData = new DataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetQualityControlEquipmentInfo");
            database.Fill(sql, resultData);
            return resultData;
        }
        /// <summary>
        /// 根据医院ID获取机器相关信息
        /// </summary>
        /// <param name="HOSPITAL_ID"></param>
        /// <returns></returns>

        public static ConfigModel.MED_EQUIPMENT_INFODataTable GetQualityControlEquipmentInfoByHospitalID(string HOSPITAL_ID)
        {
            var resultData = new ConfigModel.MED_EQUIPMENT_INFODataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetQualityControlEquipmentInfoByHospitalID");
            DbParameter param0 = database.BuildDbParameter("HOSPITAL_ID", DbType.String, HOSPITAL_ID);
            database.Fill(sql, resultData, new DbParameter[] { param0 });
            return resultData;
        }

        /// <summary>
        /// 根据医院ID删除机器相关信息
        /// </summary>
        /// <param name="HOSPITAL_ID"></param>
        /// <returns></returns>
        public static int DeleteQualityControlEquipmentInfoByHospitalID(string HOSPITAL_ID)
        {
            IDatabase database = DatabaseFactory.Create();
            var sql = string.Format("DELETE FROM MED_EQUIPMENT_INFO WHERE HOSPITAL_ID='{0}'", HOSPITAL_ID);
            using (var trans = database.CreateDbTransaction())
            {
                try
                {
                    database.ExecuteNonQuery(sql);
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
        /// <summary>
        /// 保存医院相关信息
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int SaveQualityControlEquipmentInfo(ConfigModel.MED_EQUIPMENT_INFODataTable dt)
        {
            return SaveData<ConfigModel.MED_EQUIPMENT_INFODataTable>(dt);
        }

        /// <summary>
        /// 保存信息至质控总表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>

        public static int SaveMED_QUALITY_BASE(ConfigModel.MED_QUALITY_BASEDataTable dt)
        {
            return SaveData<ConfigModel.MED_QUALITY_BASEDataTable>(dt);
        }
        /// <summary>
        /// 根据医院ID删除质控数据
        /// </summary>
        /// <param name="HOSPITAL_ID"></param>
        /// <returns></returns>
        public static int DeleteMED_QUALITY_BASE(string HOSPITAL_ID)
        {
            IDatabase database = DatabaseFactory.Create();
            var sql = string.Format("DELETE FROM MED_QUALITY_BASE WHERE HOSPITAL_ID='{0}'", HOSPITAL_ID);
            using (var trans = database.CreateDbTransaction())
            {
                try
                {
                    database.ExecuteNonQuery(sql);
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
        /// <summary>
        /// 获取GetMED_QUALITY_BASE
        /// </summary>
        /// <param name="HOSPITAL_ID"></param>
        /// <returns></returns>
        public static ConfigModel.MED_QUALITY_BASEDataTable GetMED_QUALITY_BASE(string HOSPITAL_ID)
        {
            var resultData = new ConfigModel.MED_QUALITY_BASEDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = "SELECT *FROM MED_QUALITY_BASE WHERE HOSPITAL_ID =:HOSPITAL_ID";
            DbParameter param0 = database.BuildDbParameter("HOSPITAL_ID", DbType.String, HOSPITAL_ID);
            database.Fill(sql, resultData, new DbParameter[] { param0 });
            return resultData;
        }
        /// <summary>
        /// 删除DeleteMED_QUALITY_BASEbyHOSPITAL_IDandKind
        /// </summary>
        /// <param name="HOSPITAL_ID"></param>
        /// <param name="Kind"></param>
        /// <returns></returns>
        public static int DeleteMED_QUALITY_BASEbyHOSPITAL_IDandKind(string HOSPITAL_ID, string Kind)
        {
            IDatabase database = DatabaseFactory.Create();
            var sql = string.Format("DELETE FROM MED_QUALITY_BASE WHERE HOSPITAL_ID='{0}' AND UPLOAD_TYPE='{1}'  ", HOSPITAL_ID, Kind);
            using (var trans = database.CreateDbTransaction())
            {
                try
                {
                    database.ExecuteNonQuery(sql);
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
        public static ConfigModel.MED_SCREEN_MSGDataTable GetMsgByParams(DateTime dtStart, DateTime dtEnd)
        {
            var returnDt = new ConfigModel.MED_SCREEN_MSGDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = string.Format("SELECT * FROM MED_SCREEN_MSG T WHERE TRUNC(T.INSERT_TIME) >=TRUNC(:dtStar) AND TRUNC(T.INSERT_TIME)<=TRUNC(:dtEnd)");
            var dbParams0 = database.BuildDbParameter("dtStar", DbType.DateTime, dtStart);
            var dbParams1 = database.BuildDbParameter("dtEnd", DbType.DateTime, dtEnd);
  
            database.Fill(sql, returnDt, new DbParameter[] { dbParams0, dbParams1 });

            return returnDt;
        }

        public static int SaveMsg(ConfigModel.MED_SCREEN_MSGDataTable dt)
        {
            return SaveData<ConfigModel.MED_SCREEN_MSGDataTable>(dt);

        }

    }
}
