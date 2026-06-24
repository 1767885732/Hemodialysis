/*----------------------------------------------------------------
// Copyright (C) 2005 (苏州)医疗科技发展有限公司
// 文件名：LabBll.cs
// 文件功能描述：常规检查数据业务逻辑类
// 创建标识：
// 修改时间：2014-4-9
// 修改人：吕志强
// 修改描述：添加常规检查数据业务逻辑方法
//
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System.Data;
using System.Data.Common;
using System;
using Hemo.DataAccess;
using System.Linq;
using System.Text;
using Hemo.Utilities;

namespace Hemo.Business.Lab
{
    public class LabBll : BaseClass
    {
        /// <summary>
        /// 获取病人检验信息
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public static DataTable GetPatientLabList(string patientID)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("PATIENT_ID", DbType.String, patientID.Trim());

            return GetData(result, "GetPatientLabList", dbParams);
        }

        /// <summary>
        ///根据时间段获取病人的检验记录结果
        /// </summary>
        /// <param name="patientID">病人ID</param>
        /// <param name="pStartDate">检验报表开始时间</param>
        /// <param name="pEndDate">检验报表结束时间</param>
        /// <returns></returns>
        public static DataTable GetPatientLabListByDate(string patientID, DateTime pStartDate, DateTime pEndDate)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("PATIENT_ID", DbType.String, patientID.Trim());
            dbParams[1] = IDatabase.BuildDbParameter("STARTDATE", DbType.DateTime, pStartDate);
            dbParams[2] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, pEndDate);
            return GetData(result, "GetPatientLabListByDate", dbParams);
        }

        /// <summary>
        /// 获取三个月内常规检验数据列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetThreeMonthsCommonLabList(string pPatientType)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("PATIENT_TYPE", DbType.String, pPatientType.Trim());
            return GetData<DataTable>(result, "GetThreeMonthsCommonLabList", dbParams);
        }
        /// <summary>
        /// 获取三个月内常规检验数据列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetThreeMonthsCommonLabListByHemoId(string hemoId)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId.Trim());
            return GetData<DataTable>(result, "GetThreeMonthsCommonLabListByHemoID", dbParams);
        }

        /// <summary>
        /// 获取半年内常规检验数据列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSixMonthsCommonLabListByHemoId(string hemoId)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId.Trim());
            return GetData<DataTable>(result, "GetSixMonthsCommonLabListByHemoID", dbParams);
        }
        /// <summary>
        /// 获取半年内常规检验数据列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSixMonthsCommonLabList(string pPatientType)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("PATIENT_TYPE", DbType.String, pPatientType.Trim());
            return GetData<DataTable>(result, "GetSixMonthsCommonLabList", dbParams);
        }

        /// <summary>
        /// 按年份获取感染检查列表
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DataTable GetInfectionCheckListByYear(string year)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("YEAR", DbType.String, year);
            return GetData<DataTable>(result, "GetInfectionCheckListByYear", dbParams);
        }

        /// <summary>
        /// 按日期获取感染检查列表
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DataTable GetInfectionCheckListByDate(DateTime beginDate, DateTime endDate)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, beginDate);
            dbParams[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            return GetData<DataTable>(result, "GetInfectionCheckListByDate", dbParams);
        }

        /// <summary>
        /// 按年份获取质量管理基础数据列表
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DataTable GetQualityControlBaseDataByYear(string year)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("YEAR", DbType.String, year);
            return GetData<DataTable>(result, "GetQualityControlBaseDataByYear", dbParams);
        }

        /// <summary>
        /// 按日期获取质量管理基础数据列表
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DataTable GetQualityControlBaseDataByDate(DateTime beginDate, DateTime endDate)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, beginDate);
            dbParams[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            return GetData<DataTable>(result, "GetQualityControlBaseDataByDate", dbParams);
        }

        /// <summary>
        /// 获取血透机、专职人员统计数据
        /// </summary>
        public static DataTable GetMachineAndSpecialistCount()
        {
            DataTable result = new DataTable();
            return GetData<DataTable>(result, "GetMachineAndSpecialistCount", null);
        }

        /// <summary>
        /// 按年份获取患者质量监测指标列表
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DataTable GetQualityMonitorIndicatorByYear(string year)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("YEAR", DbType.String, year);
            return GetData<DataTable>(result, "GetQualityMonitorIndicatorByYear", dbParams);
        }

        /// <summary>
        /// 按日期获取患者质量监测指标列表
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DataTable GetQualityMonitorIndicatorByDate(DateTime beginDate, DateTime endDate)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, beginDate);
            dbParams[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            return GetData<DataTable>(result, "GetQualityMonitorIndicatorByDate", dbParams);
        }

        /// <summary>
        /// 获取溶质清除统计列表
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DataTable GetUreaRemoveCountList(string year)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("YEAR", DbType.String, year);
            return GetData<DataTable>(result, "GetUreaRemoveCountList", dbParams);
        }

        /// <summary>
        /// 获取肾性贫血纠正例数统计列表
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DataTable GetRenalAnemiaCountList(string year)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("YEAR", DbType.String, year);
            return GetData<DataTable>(result, "GetRenalAnemiaCountList", dbParams);
        }

        /// <summary>
        /// 获取病人血红蛋白趋势
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public static DataTable GetHBTrend(string patientId, DateTime startDate, DateTime endDate)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("PATIENT_ID", DbType.String, patientId.Trim());
            dbParams[1] = IDatabase.BuildDbParameter("STARTDATE", DbType.DateTime, startDate);
            dbParams[2] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            return GetData(result, "GetHBTrend", dbParams);
        }

        public static DataTable GetMedPatientQualityData(DateTime startDate, DateTime endDate)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("STARTDATE", DbType.DateTime, startDate);
            dbParams[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            return GetData(result, "GetMedPatientQualityData", dbParams);
        }

        /// <summary>
        /// 根据PatientId获取患者检查列表
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public static DataTable GetPatientExamList(string patientId)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("PATIENT_ID", DbType.String, patientId.Trim());

            return GetData(result, "GetPatientExamList", dbParams);
        }

        /// <summary>
        /// 根据PatientId和日期获取患者检查列表
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DataTable GetPatientExamListByDate(string patientId, DateTime startDate, DateTime endDate)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("PATIENT_ID", DbType.String, patientId.Trim());
            dbParams[1] = IDatabase.BuildDbParameter("STARTDATE", DbType.DateTime, startDate);
            dbParams[2] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            return GetData(result, "GetPatientExamListByDate", dbParams);
        }

        /// <summary>
        /// 根据检查序号获取患者检查明细列表
        /// </summary>
        /// <param name="examNo"></param>
        /// <returns></returns>
        public static DataTable GetPatientExamDetailListByNo(string examNo)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("EXAM_NO", DbType.String, examNo);

            return GetData(result, "GetPatientExamDetailListByNo", dbParams);
        }

        /// <summary>
        /// 生成质量监测数据
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public static int Save_MED_REPORT_DATA(DateTime startDate)
        {
            try
            {
                IDatabase database = DatabaseFactory.Create();
                DbParameter[] dbParams = new DbParameter[1];
                dbParams[0] = IDatabase.BuildDbParameter("REPORTDATE", DbType.DateTime, startDate);
                dbParams[0].Direction = ParameterDirection.Input;
                return database.ExecuteNonQuery("CALL PRO_MED_CALC_REPORT_MONTH(:REPORTDATE)", dbParams);
            }
            catch (Exception e)
            {
                return 0;
            }

        }

        /// <summary>
        /// 根据日期范围和项目获取检验数据
        /// </summary>
        /// <param name="STARTDATE"></param>
        /// <param name="ENDDATE"></param>
        /// <param name="ITEM_NAME"></param>
        /// <returns></returns>
        public static DataTable GetLabListByDateAndItems(DateTime STARTDATE, DateTime ENDDATE, string ITEM_NAME)
        {
            DataTable result = new DataTable();
            String sql = string.Empty;
            switch (ITEM_NAME)
            {
                case "常规生化全套检查":
                    sql = StoredScript.Get("MED_VIEW_CHANGGUISHENGHUA_EXT_Short");
                    break;
                case "电解质检查":
                    sql = StoredScript.Get("MED_VIEW_DIANJIEZHICHECK_EXT_Short");
                    break;
                case "血常规(含有核红细胞五分类)":
                    sql = StoredScript.Get("MED_VIEW_XUECHANGGUI_EXT_Short");
                    break;
                case "血常规（含网积红及有核红）":
                    sql = StoredScript.Get("MED_VIEW_XUECHANGGUI_2_EXT_Short");
                    break;
                case "肝功能检查":
                    sql = StoredScript.Get("MED_VIEW_GANGONGNENGCHECK_EXT_Short");
                    break;
                case "肾功能检查":
                    sql = StoredScript.Get("MED_VIEW_SHENGONGNENGCHECK_EXT_Short");
                    break;
                case "输血前四项检查":
                    sql = StoredScript.Get("MED_VIEW_SHUXUEQIANSIXIANG_EXT_Short");
                    break;
                case "甲状旁腺素":
                    sql = StoredScript.Get("MED_VIEW_JIAZHUANGPANGXIAN_EXT_Short");
                    break;
                case "乙肝两对半":
                    sql = StoredScript.Get("MED_VIEW_YIGANLIANGDUIBAN_EXT_Short");
                    break;
                case "血透检验导出":
                    sql = StoredScript.Get("MED_VIEW_XUEHONGDANBAI_EXT_short");
                    break;
                default:
                    sql = StoredScript.Get("MED_VIEW_CHANGGUISHENGHUA_EXT_Short");
                    break;
            }
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("STARTDATE", DbType.DateTime, STARTDATE);
            dbParams[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, ENDDATE);
            IDatabase.Fill(sql, result, dbParams);
            return result;
        }
        /// <summary>
        /// 根据日期范围和项目和患者信息获取检验数据
        /// </summary>
        /// <param name="STARTDATE"></param>
        /// <param name="ENDDATE"></param>
        /// <param name="ITEM_NAME"></param>
        /// <param name="PATIENTINFO"></param>
        /// <returns></returns>
        public static DataTable GetLabListByDateAndItemsAndHemoInfo(DateTime STARTDATE, DateTime ENDDATE, string ITEM_NAME, String PATIENTINFO)
        {
            DataTable result = new DataTable();
            String sql = string.Empty;
            switch (ITEM_NAME)
            {
                case "常规生化全套检查":
                    sql = StoredScript.Get("MED_VIEW_CHANGGUISHENGHUA_EXT_Long");
                    break;
                case "电解质检查":
                    sql = StoredScript.Get("MED_VIEW_DIANJIEZHICHECK_EXT_Long");
                    break;
                case "血常规(含有核红细胞五分类)":
                    sql = StoredScript.Get("MED_VIEW_XUECHANGGUI_EXT_Long");
                    break;
                case "血常规（含网积红及有核红）":
                    sql = StoredScript.Get("MED_VIEW_XUECHANGGUI_2_EXT_Long");
                    break;
                case "肝功能检查":
                    sql = StoredScript.Get("MED_VIEW_GANGONGNENGCHECK_EXT_Long");
                    break;
                case "肾功能检查":
                    sql = StoredScript.Get("MED_VIEW_SHENGONGNENGCHECK_EXT_Long");
                    break;
                case "输血前四项检查":
                    sql = StoredScript.Get("MED_VIEW_SHUXUEQIANSIXIANG_EXT_Long");
                    break;
                case "甲状旁腺素":
                    sql = StoredScript.Get("MED_VIEW_JIAZHUANGPANGXIAN_EXT_Long");
                    break;
                case "乙肝两对半":
                    sql = StoredScript.Get("MED_VIEW_YIGANLIANGDUIBAN_EXT_Long");
                    break;
                case "血透检验导出":
                    sql = StoredScript.Get("MED_VIEW_XUEHONGDANBAI_EXT_long");
                    break;
                default:
                    sql = StoredScript.Get("MED_VIEW_CHANGGUISHENGHUA_EXT_Long");
                    break;
            }
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("STARTDATE", DbType.DateTime, STARTDATE);
            dbParams[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, ENDDATE);
            dbParams[2] = IDatabase.BuildDbParameter("PATIENTINFO", DbType.String, PATIENTINFO);
            IDatabase.Fill(sql, result, dbParams);
            return result;
        }


        /// <summary>
        /// 根据日期范围、项目、项目明细获取检验数据
        /// </summary>
        /// <param name="STARTDATE"></param>
        /// <param name="ENDDATE"></param>
        /// <param name="ITEM_NAME"></param>
        /// <returns></returns>
        public static DataTable GetLabListByDateAndItemsAndDtl(DateTime STARTDATE, DateTime ENDDATE, string ITEM_NAME,DataTable dtTitle)
        {
            DataTable result = new DataTable();
            string str = string.Empty;
            StringBuilder sb = new StringBuilder();
            IDatabase database = DatabaseFactory.Create();
            string[] items = ITEM_NAME.Split("，".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            items.AsEnumerable().ToList().ForEach(item =>
            {
                sb.Append("ITEM_NAME LIKE '%" + item + "%'");
                sb.Append(" " + "OR" + " ");
            });
            if (sb.ToString().Length == 0)
            {
                str = "1= 1";
            }
            else
            {
                str = sb.ToString(0, sb.Length - 4);
            }
            StringBuilder sbDtl = new StringBuilder();
            String strsbDtl = string.Empty;
            for (int i = 0; i < dtTitle.Rows.Count; i++)
            {                
                sbDtl.Append(" REPORT_ITEM_NAME like '%" + dtTitle.Rows[i]["ITEM_VALUE"].ToString() + "%'");
                sbDtl.Append(" " + "OR" + " ");                             
            }
            if (sbDtl.ToString().Length == 0)
            {
                strsbDtl = "1= 1";
            }
            else
            {
                strsbDtl = sbDtl.ToString(0, sbDtl.Length - 4);
            }
            string sql = StoredScript.Get("GetLabListByDateAndItems");
            sql += @"AND (" + str + ")";
            sql += "AND trunc(m.RESULTS_RPT_DATE_TIME) >= TO_DATE('" + STARTDATE.ToShortDateString() + "','YYYY-MM-dd') AND ";
            sql += " trunc(m.RESULTS_RPT_DATE_TIME) <= TO_DATE('" + ENDDATE.ToShortDateString() + "','YYYY-MM-dd')";
            sql += " AND (" + strsbDtl + ")";
            return database.Fill(sql, result);
        }
        /// <summary>
        /// 根据日期范围、项目、项目明细和患者信息获取检验数据
        /// </summary>
        /// <param name="STARTDATE"></param>
        /// <param name="ENDDATE"></param>
        /// <param name="ITEM_NAME"></param>
        /// <param name="PATIENTINFO"></param>
        /// <returns></returns>
        public static DataTable GetLabListByDateAndItemsAndHemoInfoAndDtl(DateTime STARTDATE, DateTime ENDDATE, string ITEM_NAME, String PATIENTINFO, DataTable dtTitle)
        {
            DataTable result = new DataTable();
            StringBuilder sb = new StringBuilder();
            IDatabase database = DatabaseFactory.Create();
            string[] items = ITEM_NAME.Split("，".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            items.AsEnumerable().ToList().ForEach(item =>
            {
                sb.Append("ITEM_NAME LIKE '%" + item + "%'");
                sb.Append(" " + "OR" + " ");
            });
            string str = sb.ToString(0, sb.Length - 4);
            StringBuilder sbDtl = new StringBuilder();
            String strsbDtl = string.Empty;
            for(int i = 0 ; i <dtTitle.Rows.Count ; i ++)
            {
                sbDtl.Append(" REPORT_ITEM_NAME = '" + dtTitle.Rows[i]["ITEM_VALUE"].ToString() + "'");
                sbDtl.Append(" " + "OR" + " ");
            }
            if (sbDtl.ToString().Length == 0)
            {
                strsbDtl = "1= 1";
            }
            else
            {
                strsbDtl = sbDtl.ToString(0, sbDtl.Length - 4);
            }
            String sql = StoredScript.Get("GetLabListByDateAndItemsAndHemoInfo");
            sql += " AND trunc(m.RESULTS_RPT_DATE_TIME) >= TO_DATE('" + STARTDATE.ToShortDateString() + "','YYYY-MM-dd') AND  ";
            sql += " trunc(m.RESULTS_RPT_DATE_TIME) <= TO_DATE('" + ENDDATE.ToShortDateString() + "','YYYY-MM-dd') AND ";
            sql += "( PAT.HEMODIALYSIS_ID LIKE '%" + PATIENTINFO + "%' OR  PAT.NAME LIKE '%" + PATIENTINFO + "%' OR UPPER(PAT.INPUT_CODE) LIKE UPPER('%" + PATIENTINFO + "%')) AND";
            sql += "(" + str + ")";
            sql += "AND(" + strsbDtl + ")";
            return database.Fill(sql, result);
        }

        /// <summary>
        /// 行转列
        /// </summary>
        /// <param name="hemodialysis_id"></param>
        /// <param name="patient_id"></param>
        /// <param name="test_no"></param>
        /// <param name="check_date"></param>
        /// <param name="item_name"></param>
        /// <returns></returns>
        public static DataTable GETMED_HIS_ROWTOCOL(string hemodialysis_id, string patient_id, string test_no, DateTime check_date, string item_name)
        {
            DataTable result = new DataTable();
            var sql = StoredScript.Get("GETMED_HIS_ROWTOCOL");
            DbParameter[] dbParameter = new DbParameter[5];
            dbParameter[0] = IDatabase.BuildDbParameter("hemodialysis_id", DbType.String, hemodialysis_id);
            dbParameter[1] = IDatabase.BuildDbParameter("patient_id", DbType.String, patient_id);
            dbParameter[2] = IDatabase.BuildDbParameter("test_no", DbType.String, test_no);
            dbParameter[3] = IDatabase.BuildDbParameter("check_date", DbType.DateTime, check_date);
            dbParameter[4] = IDatabase.BuildDbParameter("item_name", DbType.String, item_name);
            IDatabase.Fill(sql, result, dbParameter);
            return result;          
        }

       /// <summary>
       /// 行转列插入
       /// </summary>
       /// <param name="hemodialysis_id"></param>
       /// <param name="patient_id"></param>
       /// <param name="test_no"></param>
       /// <param name="check_date"></param>
       /// <param name="item_name"></param>
       /// <returns></returns>
        public static int INSERT_UPDATE_MED_HIS_ROWTOCOL_END(string hemodialysis_id, string patient_id, string test_no, DateTime check_date, string item_name)
        {
            DataTable result = new DataTable();
            var sql = StoredScript.Get("INSERT_UPDATE_MED_HIS_ROWTOCOL_END");
            DbParameter[] dbParameter = new DbParameter[5];
            dbParameter[0] = IDatabase.BuildDbParameter("hemodialysis_id", DbType.String, hemodialysis_id);
            dbParameter[1] = IDatabase.BuildDbParameter("patient_id", DbType.String, patient_id);
            dbParameter[2] = IDatabase.BuildDbParameter("test_no", DbType.String, test_no);
            dbParameter[3] = IDatabase.BuildDbParameter("check_date", DbType.DateTime, check_date);
            dbParameter[4] = IDatabase.BuildDbParameter("item_name", DbType.String, item_name);
            try {
                return IDatabase.ExecuteNonQuery(sql, dbParameter);
            }
            catch {
                return 0;
            }
        }

        /// <summary>
        /// 保存行转列
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="dtTitle"></param>
        /// <param name="ITEM_NAME"></param>
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public static string SAVE_MED_HIS_ROWTOCOL(DataTable dtSource, DataTable dtTitle, string ITEM_NAME,DateTime begintime,DateTime endtime)
        {
            string ErrorMsg = string.Empty;
            ErrorMsg += "开始时间：" + System.DateTime.Now + "，数据行数：" + dtSource.Rows.Count + "，项目名：" + ITEM_NAME + "\r\n";
            if (dtSource.Rows.Count == 0 || dtTitle.Rows.Count == 0)
            {
                ErrorMsg += ITEM_NAME + "：无查询数据\r\n";
                return ErrorMsg;
            }
            var hemodialysis_id = string.Empty;
            var patient_id = string.Empty;
            var test_no = string.Empty;
            var check_date = new DateTime();
            var item_name = ITEM_NAME;
            var Ext_Xml = string.Empty;

            var rows = dtSource.AsEnumerable().Where(row => !String.IsNullOrEmpty(row["REPORT_ITEM_NAME"].ToString()) && !String.IsNullOrEmpty(row["RESULT"].ToString()));
            if (rows == null || rows.Count() == 0)
            {
                ErrorMsg += ITEM_NAME + "：无查询数据\r\n";
                return ErrorMsg;
            }
            dtSource = rows.CopyToDataTable();

            var r = (from d in dtSource.AsEnumerable() select d["TEST_NO"]).Distinct();
            DataTable dtTestNo = new DataTable();
            dtTestNo.Columns.Add("TEST_NO", System.Type.GetType("System.String"));
            foreach (var item in r)
            {
                dtTestNo.Rows.Add(item);//所有质控项目相关检验单号
            }

            var sqlInsert = StoredScript.Get("INSERTMED_HIS_ROWTOCOL");
            var sqlUpdate = StoredScript.Get("UPDATEMED_HIS_ROWTOCOL");
            var sqlIUdtl = StoredScript.Get("INSERT_UPDATE_MED_HIS_ROWTOCOL_END");
            var sqlDelete = @"DELETE FROM MED_HIS_ROWTOCOL WHERE ITEM_NAME ='" + ITEM_NAME + "' AND trunc(check_date) >= TO_DATE('" + begintime.ToShortDateString() + "','YYYY-MM-dd')  AND trunc(check_date) <= TO_DATE('" + endtime.ToShortDateString() + "','YYYY-MM-dd') ";
            IDatabase.ExecuteNonQuery(sqlDelete);//清空质控表对应项目
            for (int j = 0; j < dtTestNo.Rows.Count; j++)
            {
                var drs = dtSource.Select(" TEST_NO='" + dtTestNo.Rows[j]["TEST_NO"].ToString() + "' ");
                if (drs == null || drs.Count() == 0)
                {
                    continue;
                }
                var dt = drs.CopyToDataTable();
                var dtCol = Utility.DataTableRowtoColumnForQualityNew(dt, dtTitle);
                var isql = string.Empty;
                if (dtCol != null && dtCol.Rows.Count > 0)
                {
                    hemodialysis_id = dtCol.Rows[0]["透析号"].ToString();
                    patient_id = dtCol.Rows[0]["病人号"].ToString();
                    test_no = dtCol.Rows[0]["单号"].ToString();
                    check_date = Utility.CDate(dtCol.Rows[0]["检验日期"].ToString());
                    Ext_Xml = dtCol.Rows[0]["Ext_Xml"].ToString();

                    DbParameter[] dbParameter = new DbParameter[6];
                    dbParameter[0] = IDatabase.BuildDbParameter("hemodialysis_id", DbType.String, hemodialysis_id);
                    dbParameter[1] = IDatabase.BuildDbParameter("patient_id", DbType.String, patient_id);
                    dbParameter[2] = IDatabase.BuildDbParameter("test_no", DbType.String, test_no);
                    dbParameter[3] = IDatabase.BuildDbParameter("check_date", DbType.DateTime, check_date);
                    dbParameter[4] = IDatabase.BuildDbParameter("item_name", DbType.String, item_name);
                    dbParameter[5] = IDatabase.BuildDbParameter("Ext_Xml", DbType.String, Ext_Xml);
                    try
                    {
                        IDatabase.ExecuteNonQuery(sqlInsert, dbParameter);
                    }
                    catch (Exception ex)
                    {
                        ErrorMsg += ex.ToString() + "\r\n";
                        ErrorMsg += "更新his表异常\r\n";
                    }
                }

//                    isql += @"INSERT INTO med_his_rowtocol(hemodialysis_id, patient_id, test_no, check_date, item_name,Ext_Xml)
//                         VALUES('" + hemodialysis_id + "', '" + patient_id + "', '" + test_no + "',to_date('" + check_date + "' , 'yyyy-mm-dd hh24:mi:ss'), '" + item_name + "','" + Ext_Xml + "') ";
                
                #region
             
                //                for (int i = 0; i < dtCol.Rows.Count; i++)
//                {
//                    hemodialysis_id = dtCol.Rows[i]["透析号"].ToString();
//                    patient_id = dtCol.Rows[i]["病人号"].ToString();
//                    test_no = dtCol.Rows[i]["单号"].ToString();
//                    check_date = Utility.CDate(dtCol.Rows[i]["检验日期"].ToString());
//                    Ext_Xml = dtCol.Rows[i]["Ext_Xml"].ToString();
//                    isql += @"INSERT INTO med_his_rowtocol(hemodialysis_id, patient_id, test_no, check_date, item_name,Ext_Xml)
//                         VALUES('" + hemodialysis_id + "', '" + patient_id + "', '" + test_no + "', '" + check_date + "', '" + item_name + "','" + Ext_Xml + "') ";

//                    DataTable dtExt = GETMED_HIS_ROWTOCOL(hemodialysis_id, patient_id, test_no, check_date, item_name);
//                    DbParameter[] dbParameter = new DbParameter[6];
//                    dbParameter[0] = IDatabase.BuildDbParameter("hemodialysis_id", DbType.String, hemodialysis_id);
//                    dbParameter[1] = IDatabase.BuildDbParameter("patient_id", DbType.String, patient_id);
//                    dbParameter[2] = IDatabase.BuildDbParameter("test_no", DbType.String, test_no);
//                    dbParameter[3] = IDatabase.BuildDbParameter("check_date", DbType.DateTime, check_date);
//                    dbParameter[4] = IDatabase.BuildDbParameter("item_name", DbType.String, item_name);
//                    dbParameter[5] = IDatabase.BuildDbParameter("Ext_Xml", DbType.String, Ext_Xml);
//                    try
//                    {
//                        if (dtExt.Rows.Count > 0)
//                        {
//                            IDatabase.ExecuteNonQuery(sqlUpdate, dbParameter);
//                        }
//                        else
//                        {
//                            IDatabase.ExecuteNonQuery(sqlInsert, dbParameter);
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        ErrorMsg += ex.ToString() + "\r\n";
//                        ErrorMsg += "hemodialysis_id =" + hemodialysis_id + "patient_id=" + patient_id + "test_no=" + test_no + "check_date=" + check_date + "item_name=" + item_name + "\r\n";
//                        continue;
//                    }
                //                }
               
                //if (isql.Length > 0)
                //{
                //    try
                //    {
                //        IDatabase.ExecuteNonQuery(isql);
                //    }
                //    catch (Exception ex)
                //    {
                //        ErrorMsg += ex.ToString() + "\r\n";
                //        ErrorMsg += "更新his表异常\r\n";
                //    }
                //}
                #endregion

            }
            try
            {
                DbParameter[] dbParameter1 = new DbParameter[3];
                dbParameter1[0] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, begintime);
                dbParameter1[1] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endtime);
                dbParameter1[2] = IDatabase.BuildDbParameter("item_name", DbType.String, item_name);
                IDatabase.ExecuteNonQuery(sqlIUdtl, dbParameter1);
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString() + "\r\n";
                ErrorMsg += "更新end表异常\r\n";
            }
          
            ErrorMsg += "结束时间：" + System.DateTime.Now + "，转换前：" + dtSource.Rows.Count +"，项目名：" + ITEM_NAME + "\r\n";
            return ErrorMsg;
        }

        /// <summary>
        /// 获取血红蛋白
        /// </summary>
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public static DataTable get_med_vw_xuehongdanbai_ext(DateTime begintime,DateTime endtime)       
        {
            DataTable result = new DataTable();
            var sql = StoredScript.Get("get_med_vw_xuehongdanbai_ext");
            DbParameter[] dbParameter = new DbParameter[2];
            dbParameter[0] = IDatabase.BuildDbParameter("begintime", DbType.DateTime, begintime);
            dbParameter[1] = IDatabase.BuildDbParameter("endtime", DbType.String, endtime);
            IDatabase.Fill(sql, result, dbParameter);
            return result;
        }
    }
}
