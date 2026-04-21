/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:患者排班类
 * 创建标识:顾伟伟-2013年6月8日
 * 
 * 修改时间:2013年10月24日
 * 修改人:贺建操
 * 修改描述:新增方法GetPatientScheduleSignle
 * 
 * 修改时间:2014年3月4日
 * 修改人:贺建操
 * 修改描述:修改方法GetSchedulePatientLabResultMain
 * 
 * 修改时间:2014年7月11日
 * 修改人:贺建操
 * 修改描述:修改方法GetPatientScheduleListReportForJL
 * 
 * 修改时间:2026年4月15日
 * 修改人:刘建超
 * 修改描述:使用TNS连接Oracle数据库传入参数时间类型特指
 * ----------------------------------------------------------------*/
using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using Hemo.Model;
using Hemo.DataAccess;
using Hemo.Utilities;

namespace Hemo.Business.PatientSchedule
{
    public class PatientScheduleBll : BaseClass
    {
        /// <summary>
        /// MED_PATIENT_SCHEDULEDataTable
        /// </summary>
        /// <param name="dialysisDate"></param>
        /// <param name="hemodialysisID"></param>
        /// <returns></returns>
        public static PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleSignle(DateTime dialysisDate, string hemodialysisID)
        {
            PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable result = new PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable();
            DbParameter[] dbParams = new DbParameter[2];
            //2013-09-04 福总现场OLEDB方式添加的参数，调换了位置
            //dbParams[0] = IDatabase.BuildDbParameter("DIALYSIS_DATE", DbType.String, Utilities.Utility.CDate(dialysisDate.ToString()).ToShortDateString());
            dbParams[0] = IDatabase.BuildDbParameter("DIALYSIS_DATE", DbType.DateTime, Utilities.Utility.CDate(dialysisDate.ToString()).ToString("yyyy-MM-dd"));
            dbParams[1] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemodialysisID.Trim());
            return GetData<PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable>(result, "GetPatientScheduleSignle", dbParams);
        }

        /// <summary>
        /// MED_PATIENT_SCHEDULEDataTable
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="beginDialysisDate"></param>
        /// <param name="endDialysisDate"></param>
        /// <param name="banciID"></param>
        /// <returns></returns>
        public static PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleList(string userId, DateTime beginDialysisDate, DateTime endDialysisDate, string banciID)
        {
            PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable result = new PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable();
            DbParameter[] dbParams = new DbParameter[4];
            //DbParameter[] dbParams = new DbParameter[2];            
            dbParams[0] = IDatabase.BuildDbParameter("BEGINDIALYSIS_DATE", DbType.DateTime, beginDialysisDate.ToString("yyyy/MM/dd HH:mm:ss"));
            dbParams[1] = IDatabase.BuildDbParameter("ENDDIALYSIS_DATE", DbType.DateTime, endDialysisDate.ToString("yyyy/MM/dd HH:mm:ss"));
            dbParams[2] = IDatabase.BuildDbParameter("BANCI_ID", DbType.String, banciID.Trim());
            dbParams[3] = IDatabase.BuildDbParameter("USER_ID", DbType.String, userId.Trim());

            //return GetData<PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable>(result, "GetPatientScheduleList", null);
            return GetData<PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable>(result, "GetPatientScheduleList", dbParams);
        }

        /// <summary>
        /// MED_PATIENT_SCHEDULEDataTable
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="beginDialysisDate"></param>
        /// <param name="endDialysisDate"></param>
        /// <returns></returns>
        public static PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleListByPara(string userId, DateTime beginDialysisDate, DateTime endDialysisDate)
        {
            PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable result = new PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINDIALYSIS_DATE", DbType.DateTime, beginDialysisDate);
            dbParams[1] = IDatabase.BuildDbParameter("ENDDIALYSIS_DATE", DbType.DateTime, endDialysisDate);
            dbParams[2] = IDatabase.BuildDbParameter("USER_ID", DbType.String, userId.Trim());

            return GetData<PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable>(result, "GetPatientScheduleListByPara", dbParams);
        }

        /// <summary>
        /// MED_PATIENT_SCHEDULEDataTable
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="beginDialysisDate"></param>
        /// <param name="endDialysisDate"></param>
        /// <returns></returns>
        public static PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleListByPara2(string userId, DateTime beginDialysisDate, DateTime endDialysisDate)
        {
            PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable result = new PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINDIALYSIS_DATE", DbType.DateTime, beginDialysisDate);
            dbParams[1] = IDatabase.BuildDbParameter("ENDDIALYSIS_DATE", DbType.DateTime, endDialysisDate);
            dbParams[2] = IDatabase.BuildDbParameter("USER_ID", DbType.String, userId.Trim());

            return GetData<PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable>(result, "GetPatientScheduleListByPara2", dbParams);
        }

        /// <summary>
        /// GetSchedulePatientLabResult
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="banchiID"></param>
        /// <param name="patientType"></param>
        /// <returns></returns>
        public static System.Data.DataTable GetSchedulePatientLabResult(string userId, DateTime beginDate, DateTime endDate, string banchiID, string patientType)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[5];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINDIALYSIS_DATE", DbType.DateTime, beginDate);
            dbParams[1] = IDatabase.BuildDbParameter("ENDDIALYSIS_DATE", DbType.DateTime, endDate);
            dbParams[2] = IDatabase.BuildDbParameter("BANCI_ID", DbType.String, banchiID.Trim());
            dbParams[3] = IDatabase.BuildDbParameter("USER_ID", DbType.String, userId.Trim());
            dbParams[4] = IDatabase.BuildDbParameter("PATIENT_TYPE", DbType.String, patientType.Trim());

            return GetData<DataTable>(result, "GetSchedulePatientLabResult", dbParams);
        }

        /// <summary>
        /// GetSchedulePatientLabResultMain
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static System.Data.DataTable GetSchedulePatientLabResultMain(DateTime beginDate, DateTime endDate)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINDIALYSIS_DATE", DbType.DateTime, beginDate);
            dbParams[1] = IDatabase.BuildDbParameter("ENDDIALYSIS_DATE", DbType.DateTime, endDate);
            return GetData<DataTable>(result, "GetSchedulePatientLabResultMain", dbParams);
        }

        /// <summary>
        /// GetCureInfoByHemoID
        /// </summary>
        /// <param name="_hemoID"></param>
        /// <returns></returns>
        public static PatientScheduleModel.TRANS_CURE_INFODataTable GetCureInfoByHemoID(string _hemoID)
        {
            PatientScheduleModel.TRANS_CURE_INFODataTable result = new PatientScheduleModel.TRANS_CURE_INFODataTable();
            DbParameter[] dbParameters = new DbParameter[1];
            dbParameters[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, _hemoID);
            return GetData<PatientScheduleModel.TRANS_CURE_INFODataTable>(result, "GetCureInfoByHemoID", dbParameters);
        }

        /// <summary>
        /// GetSCHEDULEInfoByHemoID
        /// </summary>
        /// <param name="_hemoID"></param>
        /// <returns></returns>
        public static PatientScheduleModel.TRANS_SCHEDULE_INFODataTable GetSCHEDULEInfoByHemoID(string _hemoID)
        {
            PatientScheduleModel.TRANS_SCHEDULE_INFODataTable result = new PatientScheduleModel.TRANS_SCHEDULE_INFODataTable();
            DbParameter[] dbParameters = new DbParameter[1];
            dbParameters[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, _hemoID);
            return GetData<PatientScheduleModel.TRANS_SCHEDULE_INFODataTable>(result, "GetSCHEDULEInfoByHemoID", dbParameters);
        }

        /// <summary>
        /// GetPatientScheduleList4Report
        /// </summary>
        /// <param name="beginDialysisDate"></param>
        /// <param name="endDialysisDate"></param>
        /// <returns></returns>
        public static PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleList4Report(DateTime beginDialysisDate, DateTime endDialysisDate)
        {
            PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable result = new PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINDIALYSIS_DATE", DbType.DateTime, beginDialysisDate);
            dbParams[1] = IDatabase.BuildDbParameter("ENDDIALYSIS_DATE", DbType.DateTime, endDialysisDate);

            return GetData<PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable>(result, "GetPatientScheduleList4Report", dbParams);
        }

        /// <summary>
        /// GetPatientScheduleListReportForJL
        /// </summary>
        /// <param name="beginDialysisDate"></param>
        /// <param name="endDialysisDate"></param>
        /// <param name="banChi"></param>
        /// <returns></returns>
        public static ReportRelationModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleListReportForJL(DateTime beginDialysisDate, DateTime endDialysisDate,string banChi)
        {
            ReportRelationModel.MED_PATIENT_SCHEDULEDataTable result = new ReportRelationModel.MED_PATIENT_SCHEDULEDataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINDIALYSIS_DATE", DbType.DateTime, beginDialysisDate);
            dbParams[1] = IDatabase.BuildDbParameter("ENDDIALYSIS_DATE", DbType.DateTime, endDialysisDate);
            dbParams[2] = IDatabase.BuildDbParameter("BANCI_ID", DbType.String, banChi);


            return GetData<ReportRelationModel.MED_PATIENT_SCHEDULEDataTable>(result, "GetPatientScheduleListReportForJL", dbParams);
        }

        /// <summary>
        /// GetPatientScheduleByParames
        /// </summary>
        /// <param name="beginDialysisDate"></param>
        /// <param name="endDialysisDate"></param>
        /// <param name="banciID"></param>
        /// <param name="areaID"></param>
        /// <returns></returns>
        public static PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleByParames(DateTime beginDialysisDate, DateTime endDialysisDate, string banciID, string areaID)
        {
            PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable result = new PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable();
            DbParameter[] dbParams = new DbParameter[4];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINDIALYSIS_DATE", DbType.DateTime, beginDialysisDate);
            dbParams[1] = IDatabase.BuildDbParameter("ENDDIALYSIS_DATE", DbType.DateTime, endDialysisDate);
            dbParams[2] = IDatabase.BuildDbParameter("AREA_ID", DbType.String, areaID.Trim());
            dbParams[3] = IDatabase.BuildDbParameter("BANCI_ID", DbType.String, banciID.Trim());
            //2013-09-04 福总现场OLEDB方式添加的参数
            //dbParams[4] = IDatabase.BuildDbParameter("AREA_ID_OLE", DbType.String, areaID.Trim());
            //dbParams[5] = IDatabase.BuildDbParameter("BANCI_ID_OLE", DbType.String, banciID.Trim());
            return GetData<PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable>(result, "GetPatientScheduleByParames", dbParams);
        }

        /// <summary>
        /// GetPatientScheduleListByTemplateID
        /// </summary>
        /// <param name="templateID"></param>
        /// <returns></returns>
        public static PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleListByTemplateID(string templateID)
        {
            PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable result = new PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("PATIENT_SCHEDULE_TEMPLATE_ID", DbType.String, templateID.Trim());

            return GetData<PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable>(result, "GetPatientScheduleListByTemplateID", dbParams);
        }

        /// <summary>
        /// GetPatientScheduleTemplateList
        /// </summary>
        /// <param name="banciID"></param>
        /// <returns></returns>
        public static PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable GetPatientScheduleTemplateList(string banciID)
        {
            PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable result = new PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("BANCI_ID", DbType.String, banciID.Trim());

            return GetData<PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable>(result, "GetPatientScheduleTemplateList", dbParams);
        }

        /// <summary>
        /// GetPatientScheduleAllTemplateList
        /// </summary>
        /// <returns></returns>
        public static PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable GetPatientScheduleAllTemplateList()
        {
            PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable result = new PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable();


            return GetData<PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable>(result, "GetPatientScheduleAllTemplateList", null);
        }

        /// <summary>
        /// GetPatientScheduleTempDataList
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public static PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable GetPatientScheduleTempDataList(string templateId)
        {
            PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable result = new PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable();

            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("PATIENT_SCHEDULE_TEMPLATE_ID", DbType.String, templateId.Trim());

            return GetData<PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable>(result, "GetPatientScheduleTempDataList", dbParams);
        }

        /// <summary>
        /// GetPatientScheduleTempDataListNew
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public static PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable GetPatientScheduleTempDataListNew(string templateId)
        {
            PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable result = new PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable();

            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("PATIENT_SCHEDULE_TEMPLATE_ID", DbType.String, templateId.Trim());

            return GetData<PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable>(result, "GetPatientScheduleTempDataListNew", dbParams);
        }

        /// <summary>
        /// SavePatientScheduleInfo
        /// </summary>
        /// <param name="patientScheduleDataTable"></param>
        /// <returns></returns>
        public static int SavePatientScheduleInfo(PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable patientScheduleDataTable)
        {
            return SaveData<PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable>(patientScheduleDataTable);
        }

        /// <summary>
        /// GetCureBillByCureID
        /// </summary>
        /// <param name="cureID"></param>
        /// <returns></returns>
        public static DataTable GetCureBillByCureID(string cureID)
        {
            IDatabase database = DatabaseFactory.Create();
            DbParameter idParameter = database.BuildDbParameter("RECIPE_ID", DbType.String, cureID);

            return GetData<DataTable>(new DataTable(), "GetCheckUserBillByBILL_CURE_ID", new DbParameter[] { idParameter });
        }

        /// <summary>
        /// SavePatientScheduleTemplateInfo
        /// </summary>
        /// <param name="patientScheduleTemplateDataTable"></param>
        /// <returns></returns>
        public static int SavePatientScheduleTemplateInfo(PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable patientScheduleTemplateDataTable)
        {
            return SaveData<PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable>(patientScheduleTemplateDataTable);
        }

        /// <summary>
        /// SavePatientScheduleTemplateDataInfo
        /// </summary>
        /// <param name="patientScheduleTemplateDataTable"></param>
        /// <returns></returns>
        public static int SavePatientScheduleTemplateDataInfo(PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable patientScheduleTemplateDataTable)
        {
            var database = DatabaseFactory.Create();

            var sql = StoredScript.Get("PatientScheduleTemplateDeleteByTemplateId");
            string id = patientScheduleTemplateDataTable.Rows[0]["PATIENT_SCHEDULE_TEMPLATE_ID"].ToString();
            var idParameter = database.BuildDbParameter("PATIENT_SCHEDULE_TEMPLATE_ID", DbType.String, id);
            using (var trans = database.CreateDbTransaction())
            {
                try
                {
                    database.ExecuteNonQuery(sql, new DbParameter[] { idParameter }, trans);

                    database.Update(patientScheduleTemplateDataTable, trans);


                    trans.Commit();

                }
                catch (Exception e)
                {
                    trans.Rollback();
                    throw e;
                }

            }
            return 1;
            //return SaveData<PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable>(patientScheduleTemplateDataTable);
        }

        /// <summary>
        /// DeletePatientSchedule
        /// </summary>
        /// <param name="beginDialysisDate"></param>
        /// <param name="endDialysisDate"></param>
        /// <param name="banciID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static int DeletePatientSchedule(DateTime beginDialysisDate, DateTime endDialysisDate, string banciID, string userID)
        {
            DbParameter[] dbParams = new DbParameter[4];

            dbParams[0] = IDatabase.BuildDbParameter("BEGINDIALYSIS_DATE", DbType.DateTime, beginDialysisDate);
            dbParams[1] = IDatabase.BuildDbParameter("ENDDIALYSIS_DATE", DbType.DateTime, endDialysisDate);
            dbParams[2] = IDatabase.BuildDbParameter("BANCI_ID", DbType.String, banciID.Trim());
            dbParams[3] = IDatabase.BuildDbParameter("USER_ID", DbType.String, userID.Trim());

            return DeleteData("DeletePatientSchedule", dbParams);
        }

        /// <summary>
        /// DeletePatientScheduleDateTemp
        /// </summary>
        /// <param name="PATIENT_SCHEDULE_TEMPLATE_ID"></param>
        /// <returns></returns>
        public static int DeletePatientScheduleDateTemp(string PATIENT_SCHEDULE_TEMPLATE_ID)
        {
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("PATIENT_SCHEDULE_TEMPLATE_ID", DbType.String, PATIENT_SCHEDULE_TEMPLATE_ID);
            return DeleteData("DeletePatientScheduleDateTemp", dbParams);
        }

        /// <summary>
        /// DeletePatientScheduleDateByID
        /// </summary>
        /// <param name="PATIENT_SCHEDULE_ID"></param>
        /// <returns></returns>
        public static int DeletePatientScheduleDateByID(string PATIENT_SCHEDULE_ID)
        {
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("PATIENT_SCHEDULE_ID", DbType.String, PATIENT_SCHEDULE_ID.Trim());

            return DeleteData("DeletePatientScheduleDateByID", dbParams);
        }

        /// <summary>
        /// GetHemodialysisApplyList
        /// </summary>
        /// <param name="hemodialysisID"></param>
        /// <returns></returns>
        public static PatientScheduleModel.MED_HEMO_APPLYDataTable GetHemodialysisApplyList(string hemodialysisID)
        {
            PatientScheduleModel.MED_HEMO_APPLYDataTable result = new PatientScheduleModel.MED_HEMO_APPLYDataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemodialysisID.Trim());

            return GetData<PatientScheduleModel.MED_HEMO_APPLYDataTable>(result, "GetHemodialysisApplyList", dbParams);
        }

        /// <summary>
        /// SaveHemodialysisApplyInfo
        /// </summary>
        /// <param name="hemodialysisApplyDataTable"></param>
        /// <returns></returns>
        public static int SaveHemodialysisApplyInfo(PatientScheduleModel.MED_HEMO_APPLYDataTable hemodialysisApplyDataTable)
        {
            return SaveData<PatientScheduleModel.MED_HEMO_APPLYDataTable>(hemodialysisApplyDataTable);
        }


        /// <summary>
        /// DeleteHemodialysisApply
        /// </summary>
        /// <param name="applyID"></param>
        /// <returns></returns>
        public static int DeleteHemodialysisApply(string applyID)
        {
            PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable result = new PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("APPLY_ID", DbType.String, applyID);

            return DeleteData("DeleteHemodialysisApply", dbParams);
        }

        /// <summary>
        /// InSertExecProcLog
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ExecParam"></param>
        /// <returns></returns>
        public static int InSertExecProcLog(string id, string ExecParam)
        {
            IDatabase database = DatabaseFactory.Create();
            DbParameter param0 = database.BuildDbParameter("ID", DbType.String, id);
            DbParameter param1 = database.BuildDbParameter("EXECPARAM", DbType.String, ExecParam.Trim());
            var sql = StoredScript.Get("InSertExecProcLog");
            return database.ExecuteNonQuery(sql, new DbParameter[] { param0, param1 });

        }
        /// <summary>
        /// 根据透析号和透析开始时间得到一条处方编号
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="beginDialysisDate">开始时间</param>
        /// <returns>处方编号</returns>
        public static string GetPatientScheduleRecipeIDByStartTime(string pHemoID, DateTime beginDialysisDate)
        {
            string result = string.Empty;
            PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable dt = new PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            dbParams[1] = IDatabase.BuildDbParameter("START_TIME", DbType.DateTime, Utilities.Utility.CDate(beginDialysisDate.ToString()).ToShortDateString());
            dt = GetData<PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable>(dt, "GetPatientScheduleRecipeIDByStartTime", dbParams);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = dt.Rows[0]["RECIPE_ID"].ToString();
            }
            return result;
        }

        /// <summary>
        /// GetSchedulePatientCount
        /// </summary>
        /// <param name="BanChi"></param>
        /// <param name="Room"></param>
        /// <returns></returns>
        public static System.Data.DataTable GetSchedulePatientCount(string BanChi, string Room)
        {
            DataTable dt = new DataTable();
            var database = DatabaseFactory.Create();
            var param0 = database.BuildDbParameter("BANCI_ID", DbType.String, BanChi);
            var param1 = database.BuildDbParameter("DIALYSIS_ROOM_ID", DbType.String, Room);
            var sql = StoredScript.Get("GetSchedulePatientCount");
            database.Fill(sql, dt, new DbParameter[] { param0, param1 });
            return dt;
        }


        /// <summary>
        /// QueryPatientScheduleByParam
        /// </summary>
        /// <param name="Patients"></param>
        /// <param name="BanChi"></param>
        /// <param name="Room"></param>
        /// <returns></returns>
        public static PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable QueryPatientScheduleByParam(string Patients, string BanChi, string Room)
        {
            var result = new PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable();
            var database = DatabaseFactory.Create();
            var params0 = database.BuildDbParameter("PATIENTS", DbType.String, Patients);
            var params1 = database.BuildDbParameter("BANCI_ID", DbType.String, BanChi);
            var params2 = database.BuildDbParameter("DIALYSIS_ROOM_ID", DbType.String, Room);
            var sql = StoredScript.Get("QueryPatientScheduleByParam");
            database.Fill(sql, result, new DbParameter[] { params0, params1, params2 });
            return result;

        }

        /// <summary>
        /// 获取服务端日期
        /// </summary>
        /// <returns></returns>
        public static string GetServerDate()
        {
            string result = string.Empty;
            DataTable dtResult = new DataTable();
            dtResult = GetData(dtResult, "GetServerDate", null);
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                result = dtResult.Rows[0]["SYSDATE"].ToString();
            }
            return result;
        }

        /// <summary>
        /// SaveScheduleRemark
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int SaveScheduleRemark(PermissionModel.MED_SCHEDULEREMARKDataTable data)
        {
            return SaveData<PermissionModel.MED_SCHEDULEREMARKDataTable>(data);
        }

        /// <summary>
        /// GetScheduleRemarkByDate
        /// </summary>
        /// <param name="_begin"></param>
        /// <param name="_endTime"></param>
        /// <returns></returns>
        public static PermissionModel.MED_SCHEDULEREMARKDataTable GetScheduleRemarkByDate(DateTime _begin, DateTime _endTime)
        {
            var result = new PermissionModel.MED_SCHEDULEREMARKDataTable();
            var database = DatabaseFactory.Create();
            //var params0 = database.BuildDbParameter("BEGINTIME", DbType.DateTime, _begin);
            //var params1 = database.BuildDbParameter("ENDTIME", DbType.DateTime, _endTime);
            var sql = StoredScript.Get("GetScheduleRemarkByDate");
            database.Fill(sql, result);//, new DbParameter[] { params0, params1 });
            return result;
        }

        /// <summary>
        /// DeleteScheduleTemplateByTemplateId
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public static int DeleteScheduleTemplateByTemplateId(string templateId)
        {

            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("PatientScheduleTemplateDeleteByTemplateId");
            var sql1 = StoredScript.Get("PatientScheduleTemplateMenuDeleteByTemplateId");
            var idParameter = database.BuildDbParameter("PATIENT_SCHEDULE_TEMPLATE_ID", DbType.String, templateId);
            var idParameter1 = database.BuildDbParameter("PATIENT_SCHEDULE_TEMPLATE_ID", DbType.String, templateId);
            using (var trans = database.CreateDbTransaction())
            {
                try
                {
                    database.ExecuteNonQuery(sql, new DbParameter[] { idParameter }, trans);
                    database.ExecuteNonQuery(sql1, new DbParameter[] { idParameter1 }, trans);
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
        /// GetPurificationModeCountByParam
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static System.Data.DataTable GetPurificationModeCountByParam(DateTime dt)
        {
            var result = new DataTable();
            var database = DatabaseFactory.Create();
            var params0 = database.BuildDbParameter("DIALYSIS_DATE", DbType.DateTime, dt);
            var sql = StoredScript.Get("GetPurificationModeCountByParam");

            database.Fill(sql, result, new DbParameter[] { params0 });
            return result;
        }

        /// <summary>
        /// GetCureCountByParam
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static System.Data.DataTable GetCureCountByParam(DateTime dt)
        {
            var result = new DataTable();
            var database = DatabaseFactory.Create();
            var params0 = database.BuildDbParameter("DIALYSIS_DATE", DbType.DateTime, dt);
            var sql = StoredScript.Get("GetCureCountByParam");

            database.Fill(sql, result, new DbParameter[] { params0 });
            return result;
        }

        /// <summary>
        /// GetWeekDutyByDate
        /// </summary>
        /// <param name="starDate"></param>
        /// <param name="endData"></param>
        /// <returns></returns>
        public static PermissionModel.MED_USERS_WEEKDUTYDataTable GetWeekDutyByDate(DateTime starDate, DateTime endData)
        {
            var result = new PermissionModel.MED_USERS_WEEKDUTYDataTable();
            var database = DatabaseFactory.Create();
            var params0 = database.BuildDbParameter("BEGINDATE", DbType.DateTime, starDate);
            var params1 = database.BuildDbParameter("ENDDATE", DbType.DateTime, endData);
            var sql = StoredScript.Get("GetWeekDutyByDate");
            database.Fill(sql, result, new DbParameter[] { params0, params1 });
            return result;
        }

        /// <summary>
        /// GetWeekDutyByDateDoctor
        /// </summary>
        /// <param name="starDate"></param>
        /// <param name="endData"></param>
        /// <returns></returns>
        public static PermissionModel.MED_USERS_WEEKDUTYDataTable GetWeekDutyByDateDoctor(DateTime starDate, DateTime endData)
        {
            var result = new PermissionModel.MED_USERS_WEEKDUTYDataTable();
            var database = DatabaseFactory.Create();
            var params0 = database.BuildDbParameter("BEGINDATE", DbType.DateTime, starDate);
            var params1 = database.BuildDbParameter("ENDDATE", DbType.DateTime, endData);
            var sql = StoredScript.Get("GetWeekDutyByDateDoctor");
            database.Fill(sql, result, new DbParameter[] { params0, params1 });
            return result;
        }

        /// <summary>
        /// SaveWeekDutyData
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int SaveWeekDutyData(PermissionModel.MED_USERS_WEEKDUTYDataTable data)
        {
            var database = DatabaseFactory.Create();

            //var max = Convert.ToDateTime(data.Compute("max([DUTYDAY])", ""));
            //var min= Convert.ToDateTime(data.Compute("min([DUTYDAY])", ""));
            if (data == null && data.Rows.Count <= 0)
                return 0;

            var tye = data[0].TYPE.ToString();
            DateTime date = data[0].DUTYDAY;
            DateTime startWeek = Utility.GetMonday(date).Date;
            DateTime endWeek = startWeek.AddDays(6).Date;

            var idParameter = database.BuildDbParameter("BEGINDATE", DbType.DateTime, startWeek);
            var idParameter1 = database.BuildDbParameter("ENDDATE", DbType.DateTime, endWeek);
            var idParameter2 = database.BuildDbParameter("TYPE", DbType.String, tye);
            var sql = StoredScript.Get("PatientDutyDeleteByData");
            using (var trans = database.CreateDbTransaction())
            {
                try
                {
                    database.ExecuteNonQuery(sql, new DbParameter[] { idParameter, idParameter1, idParameter2 }, trans);
                    SaveData<PermissionModel.MED_USERS_WEEKDUTYDataTable>(data);
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
        /// CreateCurrntDataByLastWeek
        /// </summary>
        /// <param name="starDate"></param>
        /// <param name="endData"></param>
        /// <returns></returns>
        public static int CreateCurrntDataByLastWeek(DateTime starDate, DateTime endData)
        {
            var result = new PermissionModel.MED_USERS_WEEKDUTYDataTable();
            var database = DatabaseFactory.Create();
            var params0 = database.BuildDbParameter("BEGINDATE", DbType.DateTime, starDate);
            var params1 = database.BuildDbParameter("ENDDATE", DbType.DateTime, endData);
            var sql = StoredScript.Get("CreateCurrntDataByLastWeek");
            return database.ExecuteNonQuery(sql, new DbParameter[] { params0, params1 });

        }

        /// <summary>
        /// CreateCurrntDataByLastWeekDoctor
        /// </summary>
        /// <param name="starDate"></param>
        /// <param name="endData"></param>
        /// <returns></returns>
        public static int CreateCurrntDataByLastWeekDoctor(DateTime starDate, DateTime endData)
        {
            var result = new PermissionModel.MED_USERS_WEEKDUTYDataTable();
            var database = DatabaseFactory.Create();
            var params0 = database.BuildDbParameter("BEGINDATE", DbType.DateTime, starDate);
            var params1 = database.BuildDbParameter("ENDDATE", DbType.DateTime, endData);
            var sql = StoredScript.Get("CreateCurrntDataByLastWeekDoctor");
            return database.ExecuteNonQuery(sql, new DbParameter[] { params0, params1 });

        }

        /// <summary>
        /// GetCurrentDutyUser
        /// </summary>
        /// <returns></returns>
        public static System.Data.DataTable GetCurrentDutyUser()
        {
            var result = new DataTable();
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetCurrentDutyUser");
            return database.Fill(sql, result);
        }

        /// <summary>
        /// GetWeekDutyByTime
        /// </summary>
        /// <param name="starDate"></param>
        /// <param name="endData"></param>
        /// <returns></returns>
        public static ReportRelationModel.MED_PATIENTDUTYDataTable GetWeekDutyByTime(DateTime starDate, DateTime endData)
        {
            var result = new ReportRelationModel.MED_PATIENTDUTYDataTable();
            var database = DatabaseFactory.Create();
            var params0 = database.BuildDbParameter("BEGINDATE", DbType.DateTime, starDate);
            var params1 = database.BuildDbParameter("ENDDATE", DbType.DateTime, endData);
            var sql = StoredScript.Get("GetWeekDutyByTime");
            database.Fill(sql,result, new DbParameter[] { params0, params1 });
            return result;
        }

        /// <summary>
        /// GetQuerySchedulePatientInfo
        /// </summary>
        /// <param name="queryData"></param>
        /// <returns></returns>
        public static ReportRelationModel.SCHEDULEPATIENTINFODataTable GetQuerySchedulePatientInfo(DateTime queryData)
        {
            var result = new ReportRelationModel.SCHEDULEPATIENTINFODataTable();
            var database = DatabaseFactory.Create();
            var params0 = database.BuildDbParameter("QUERYDATE", DbType.DateTime, queryData);
            var sql = StoredScript.Get("GetQuerySchedulePatientInfo");
            database.Fill(sql, result, new DbParameter[] { params0 });
            return result;
        }

        /// <summary>
        /// GetCurrentDateNurseDuty
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetCurrentDateNurseDuty(DateTime dt)
        {
            string returnStr = string.Empty;
            DataTable result = new DataTable();
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetCurrentDateNurseDuty");
            var params0 = database.BuildDbParameter("DUTYDAY", DbType.DateTime, dt);

            database.Fill(sql, result, new DbParameter[] { params0 });
            if (result.Rows.Count > 0)
            {
                returnStr = result.Rows[0][0].ToString();
            }

            return returnStr;
        }

        /// <summary>
        /// GetSchedulePatientCheck
        /// </summary>
        /// <param name="queryData"></param>
        /// <param name="banChi"></param>
        /// <returns></returns>
        public static DataTable GetSchedulePatientCheck(DateTime queryData, string banChi)
        {
            DataTable result = new DataTable();
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetSchedulePatientCheck");
            var params1 = database.BuildDbParameter("CHECKDATE", DbType.DateTime, queryData);
            var params0 = database.BuildDbParameter("BANCHI", DbType.String, banChi);
            database.Fill(sql, result, new DbParameter[] { params0, params1 });
            return result;
        }

        /// <summary>
        /// GetPatientScheduleByRecipeId
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        public static PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleByRecipeId(string recipeId)
        {
            PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable result = new PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("RECIPE_ID", DbType.String, recipeId.Trim());
            return GetData<PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable>(result, "GetPatientScheduleByRecipeId", dbParams);
        }

        /// <summary>
        /// 根据透析编号获取患者当周排班信息
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public static string GetCurrentScheduleInfoByHemoId(string hemoId)
        {
            string result = string.Empty;
            DataTable dtResult = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dtResult = GetData<DataTable>(dtResult, "GetCurrentScheduleInfoByHemoId", dbParams);
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                dtResult.AsEnumerable().ToList().ForEach(row =>
                {
                    result += row["WEEK"].ToString() + row["CLASS_NAME"].ToString() + row["ROOM"].ToString() + row["BED"].ToString() + "/";
                });
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }

        /// <summary>
        /// 根据床号获取最新的排班记录
        /// </summary>
        /// <param name="bedNumber"></param>
        /// <returns></returns>
        public static PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetLatestScheduleByBedNumber(string bedNumber)
        {
            PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable result = new PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("BED_NUMBER", DbType.String, bedNumber);
            return GetData<PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable>(result, "GetLatestScheduleByBedNumber", dbParams);
        }
    }
}
