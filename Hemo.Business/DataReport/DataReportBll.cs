/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:Hemo.Business.DataReport
 * 创建标识:吕志强-2013年8月2日
 * 
 * 修改时间:2013年12月18日
 * 修改人:贺建操
 * 修改描述:增加GetDataReportPatientList
 * 
 * 修改时间:2014年4月28日
 * 修改人:顾伟伟
 * 修改描述:增加GSavePatientIsUploadDt
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hemo.Model;
using System.Data.Common;
using System.Data;
using Hemo.DataAccess;

namespace Hemo.Business.DataReport
{
    public class DataReportBll : BaseClass
    {

        #region 全国数据上报平台
        #region 血透信息

        /// <summary>
        /// 获取上传患者列表，其中State为患者上传标记
        /// </summary>
        /// <returns></returns>
        public static DataReportModel.MED_PATIENTSDataTable GetDataReportPatientList()
        {
            DataReportModel.MED_PATIENTSDataTable result = new DataReportModel.MED_PATIENTSDataTable();
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetDataReportPatientList");
            return database.Fill(sql, result) as DataReportModel.MED_PATIENTSDataTable;
        }
        /// <summary>
        /// 保存已经上传成功的患者
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int SavePatientIsUploadDt(DataReportModel.MED_PATIENT_DATAREPORTDataTable dt)
        {
            return SaveData<DataReportModel.MED_PATIENT_DATAREPORTDataTable>(dt);

        }
        /// <summary>
        /// 根据透析ID获取血管通路
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public static DataReportModel.MED_VASCULAR_ACCESSDataTable GetDataReportPatientVascularList(string hemoId)
        {
            DataReportModel.MED_VASCULAR_ACCESSDataTable result = new DataReportModel.MED_VASCULAR_ACCESSDataTable();
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetDataReportPatientVascularList");
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            return database.Fill(sql, result, dbParams) as DataReportModel.MED_VASCULAR_ACCESSDataTable;
        }
        /// <summary>
        /// 根据透析ID获取患者处方信息
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public static DataReportModel.MED_HEMO_RECIPEDataTable GetDataReportPatientRecipeList(string hemoId, string typeUp)
        {
            DataReportModel.MED_HEMO_RECIPEDataTable result = new DataReportModel.MED_HEMO_RECIPEDataTable();
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetDataReportPatientRecipeList");
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParams[1] = IDatabase.BuildDbParameter("TYPE", DbType.String, typeUp);

            return database.Fill(sql, result, dbParams) as DataReportModel.MED_HEMO_RECIPEDataTable;
        }
        /// <summary>
        /// 根据透析ID获取患者治疗信息
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="typeUp"></param>
        /// <returns></returns>
        public static DataReportModel.MED_CURE_MAINDataTable GetDataReportPatientBloodList(string hemoId, string typeUp)
        {
            DataReportModel.MED_CURE_MAINDataTable result = new DataReportModel.MED_CURE_MAINDataTable();
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetDataReportPatientBloodList");
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParams[1] = IDatabase.BuildDbParameter("TYPE", DbType.String, typeUp);

            return database.Fill(sql, result, dbParams) as DataReportModel.MED_CURE_MAINDataTable;
        }
        /// <summary>
        /// 根据透析ID获取患者治疗【肝素】信息
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="typeUp"></param>
        /// <returns></returns>
        public static DataReportModel.MED_CURE_MAINDataTable GetDataReportPatientAncitoaList(string hemoId, string typeUp)
        {
            DataReportModel.MED_CURE_MAINDataTable result = new DataReportModel.MED_CURE_MAINDataTable();
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetDataReportPatientAncitoaList");
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParams[1] = IDatabase.BuildDbParameter("TYPE", DbType.String, typeUp);

            return database.Fill(sql, result, dbParams) as DataReportModel.MED_CURE_MAINDataTable;
        }

        /// <summary>
        /// 根据透析ID获取患者治疗充分性评估信息
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="typeUp"></param>
        /// <returns></returns>
        public static DataReportModel.MED_ESTIMATE_SUFFICIENCYDataTable GetDataReportPatientEstimateSufficiencyList(string hemoId)
        {
            DataReportModel.MED_ESTIMATE_SUFFICIENCYDataTable result = new DataReportModel.MED_ESTIMATE_SUFFICIENCYDataTable();
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetDataReportPatientEstimateSufficiencyList");
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);

            return database.Fill(sql, result, dbParams) as DataReportModel.MED_ESTIMATE_SUFFICIENCYDataTable;
        }

        #endregion
        #region 诊断信息
        /// <summary>
        /// 获取上传患者列表用于上传诊断信息，其中State为患者上传标记
        /// </summary>
        /// <returns></returns>
        public static DataReportModel.MED_PATIENTSDataTable GetDataReportPatientDiagnoseList(string hemoId, string typeUp)
        {
            DataReportModel.MED_PATIENTSDataTable result = new DataReportModel.MED_PATIENTSDataTable();
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetDataReportPatientDiagnoseList");
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParams[1] = IDatabase.BuildDbParameter("TYPE", DbType.String, typeUp);

            return database.Fill(sql, result, dbParams) as DataReportModel.MED_PATIENTSDataTable;
        }
        #endregion
        #region 上传检验信息获取方法
        /// <summary>
        /// 获取上传患者检验信息列表用于上传检验信息
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="typeUp"></param>
        /// <returns></returns>
        public static DataReportModel.MED_LAB_MASTERDataTable GetDataReportPatientLabList(string patientId, string typeUp, DateTime dtStar, DateTime dtEnd)
        {
            DataReportModel.MED_LAB_MASTERDataTable result = new DataReportModel.MED_LAB_MASTERDataTable();
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetDataReportPatientLabList");
            DbParameter[] dbParams = new DbParameter[4];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, patientId);
            dbParams[1] = IDatabase.BuildDbParameter("TYPE", DbType.String, typeUp);
            dbParams[2] = IDatabase.BuildDbParameter("DTSTAR", DbType.DateTime, dtStar);
            dbParams[3] = IDatabase.BuildDbParameter("DTEND", DbType.DateTime, dtEnd);
            return database.Fill(sql, result, dbParams) as DataReportModel.MED_LAB_MASTERDataTable;
        }

        #endregion
        #endregion

        #region 福建省数据上报平台

        /// <summary>
        /// 获取上传患者列表，其中State为患者上传标记
        /// </summary>
        /// <returns></returns>
        public static DataReportModel.MED_PATIENTSDataTable GetDataReportPatientListFZ()
        {
            DataReportModel.MED_PATIENTSDataTable result = new DataReportModel.MED_PATIENTSDataTable();
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetDataReportPatientListFZ");
            return database.Fill(sql, result) as DataReportModel.MED_PATIENTSDataTable;
        }
        /// <summary>
        /// 保存已经上传成功的患者
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int SavePatientIsUploadDtFZ(DataReportModel.MED_PATIENT_DATAREPORTDataTable dt)
        {
            return SaveData<DataReportModel.MED_PATIENT_DATAREPORTDataTable>(dt);

        }
        /// <summary>
        /// 根据条件获取上传成功的记录
        /// </summary>
        /// <param name="upType">EXTEND5</param>
        /// <param name="state">STATE</param>
        /// <param name="recordType">EXTEND</param>
        /// <param name="type">TYPE</param>
        /// <returns></returns>
        public static DataReportModel.MED_PATIENT_DATAREPORTDataTable GetHavingUpLoadPatient(string upType, string state, string recordType, string type)
        {
            var result = new DataReportModel.MED_PATIENT_DATAREPORTDataTable();

            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetHavingUpLoadPatient");
            DbParameter[] dbParams = new DbParameter[4];
            dbParams[0] = IDatabase.BuildDbParameter("EXTEND5", DbType.String, upType);
            dbParams[1] = IDatabase.BuildDbParameter("STATE", DbType.String, state);
            dbParams[2] = IDatabase.BuildDbParameter("EXTEND", DbType.String, recordType);
            dbParams[3] = IDatabase.BuildDbParameter("TYPE", DbType.String, type);
            database.Fill(sql, result, dbParams);
            return result;
        }
        #endregion

    }
}
