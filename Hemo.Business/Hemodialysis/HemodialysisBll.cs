/*----------------------------------------------------------------
// Copyright (C) 2005 (苏州)医疗科技发展有限公司
// 文件名：HemodialysisBll.cs
// 文件功能描述：HemodialysisBll业务逻辑类
// 创建标识：
// 修改时间:2014-4-30
// 修改人：吕志强
// 修改描述：添加根据病人透析号获取透析参数数据业务逻辑方法
 // 修改时间:2014-7-10
// 修改人：刘超
// 修改描述：添加保存患者透析病程记录数据业务逻辑方法
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.Common;
using Hemo.DataAccess;
using Hemo.Model;
using System.Text;
using System.Linq;

namespace Hemo.Business.Config
{
    public class HemodialysisBll : BaseClass
    {
        #region 患者处方

        public static HemodialysisModel.MED_HEMO_RECIPEDataTable GetAllRecipe()
        {
            HemodialysisModel.MED_HEMO_RECIPEDataTable result = new HemodialysisModel.MED_HEMO_RECIPEDataTable();

            return GetData<HemodialysisModel.MED_HEMO_RECIPEDataTable>(result, "GetAllRecipe", null);
        }

        public static HemodialysisModel.MED_BEFORE_HEMODIALYSIS_SIGNDataTable GetBeforeHemodialysisSignList()
        {
            HemodialysisModel.MED_BEFORE_HEMODIALYSIS_SIGNDataTable result = new HemodialysisModel.MED_BEFORE_HEMODIALYSIS_SIGNDataTable();

            return GetData<HemodialysisModel.MED_BEFORE_HEMODIALYSIS_SIGNDataTable>(result, "GetBeforeHemodialysisSignList", null);
        }

        public static int SaveBeforeHemodialysisSignInfo(HemodialysisModel.MED_BEFORE_HEMODIALYSIS_SIGNDataTable hemodialysisDataTable)
        {
            return SaveData<HemodialysisModel.MED_BEFORE_HEMODIALYSIS_SIGNDataTable>(hemodialysisDataTable);
        }

        /// <summary>
        /// 保存长期处方方法
        /// </summary>
        /// <param name="pRecipeDataTable">长期处方数据表</param>
        /// <returns></returns>
        public static int SaveRecipe(HemodialysisModel.MED_HEMO_RECIPEDataTable pRecipeDataTable)
        {
            //判断当前处方状态为1时，把该用户的所有处方状态置为0。
            int result = 0;
            using (DbWrapTransaction transaction = IDatabase.CreateDbTransaction())
            {
                try
                {
                    if (pRecipeDataTable != null && pRecipeDataTable.Rows.Count > 0)
                    {
                        if (pRecipeDataTable.Rows[0]["STATUS"].ToString() == "1")
                        {
                            HemodialysisModel.MED_HEMO_RECIPEDataTable tmpRecipeTable = GetRecipeByHemodialysisID(pRecipeDataTable.Rows[0]["HEMODIALYSIS_ID"].ToString());
                            if (pRecipeDataTable.Rows[0]["HEMODIALYSIS_ID"].ToString() != pRecipeDataTable.Rows[0]["HEMODIALYSIS_ID"].ToString())
                            {
                                for (int i = 0; i < tmpRecipeTable.Rows.Count; i++)
                                {
                                    tmpRecipeTable.Rows[i]["STATUS"] = "0";
                                }
                                result = SaveData<HemodialysisModel.MED_HEMO_RECIPEDataTable>(tmpRecipeTable);
                            }
                        }
                    }
                    result = SaveData<HemodialysisModel.MED_HEMO_RECIPEDataTable>(pRecipeDataTable);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return result;
            }
        }


        //更新临时用药状态
        public static int SaveExeDrugStatus(string status, string CURE_DRUG_ID)
        {
            IDatabase database = DatabaseFactory.Create();
            string sql = StoredScript.Get("SaveExeDrugStatus");
            var para = database.BuildDbParameter("STATUS", DbType.String, status);
            var para1 = database.BuildDbParameter("CURE_DRUG_ID", DbType.String, CURE_DRUG_ID);
            return database.ExecuteNonQuery(sql, new DbParameter[] { para, para1 });
        }

        //更新长期用药状态
        public static int SaveExeDrugLongStatus(string status, string CURE_DRUG_ID)
        {
            IDatabase database = DatabaseFactory.Create();
            string sql = StoredScript.Get("SaveExeDrugLongStatus");
            var para = database.BuildDbParameter("STATUS", DbType.String, status);
            var para1 = database.BuildDbParameter("CURE_DRUG_ID", DbType.String, CURE_DRUG_ID);
            return database.ExecuteNonQuery(sql, new DbParameter[] { para, para1 });
        }

        /// <summary>
        /// 根据长期处方ID得到对应处方数据
        /// </summary>
        /// <param name="pRecipeID">处方ID</param>
        /// <returns></returns>
        public static HemodialysisModel.MED_HEMO_RECIPEDataTable GetRecipeByRecipeID(string pRecipeID)
        {
            HemodialysisModel.MED_HEMO_RECIPEDataTable Result = new HemodialysisModel.MED_HEMO_RECIPEDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("RECIPE_ID", DbType.String, pRecipeID);
            return GetData<HemodialysisModel.MED_HEMO_RECIPEDataTable>(Result, "GetRecipeByRecipeID", Params);
        }

        /// <summary>
        /// 根据处方ID列表更新开方医生签名
        /// </summary>
        /// <param name="pRecipeIDList">处方ID列表</param>
        /// <param name="pUserID">医生ID</param>
        /// <returns></returns>
        public static int SaveRecipeUserIDByRecipeIDList(string pRecipeIDList, string pUserID)
        {
            IDatabase database = DatabaseFactory.Create();
            string sql = StoredScript.Get("SaveRecipeUserIDByRecipeIDList");
            //var para = database.BuildDbParameter("USER_ID", DbType.String, pUserID);
            //var para1 = database.BuildDbParameter("RECIPE_ID", DbType.String, pRecipeIDList);
            sql = string.Format(sql, pUserID, pRecipeIDList);
            return database.ExecuteNonQuery(sql);
        }


        /// <summary>
        /// 根据长期处方ID得到对应处方数据
        /// </summary>
        /// <param name="pHemodialysisID">透析号</param>
        /// <returns>透析列表数据</returns>
        public static HemodialysisModel.MED_HEMO_RECIPEDataTable GetRecipeByHemodialysisID(string pHemodialysisID)
        {
            HemodialysisModel.MED_HEMO_RECIPEDataTable Result = new HemodialysisModel.MED_HEMO_RECIPEDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemodialysisID);
            return GetData<HemodialysisModel.MED_HEMO_RECIPEDataTable>(Result, "GetRecipeByHemodialysisID", Params);
        }

        /// <summary>
        /// 根据透析编号获取长期处方
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_HEMO_RECIPEDataTable GetLongRecipeByHemodialysisID(string hemoId)
        {
            HemodialysisModel.MED_HEMO_RECIPEDataTable Result = new HemodialysisModel.MED_HEMO_RECIPEDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            return GetData<HemodialysisModel.MED_HEMO_RECIPEDataTable>(Result, "GetLongRecipeByHemodialysisID", Params);
        }

        /// <summary>
        /// 根据长期处方ID和日期得到对应处方数据
        /// </summary>
        /// <param name="pHemodialysisID">透析号</param>
        /// <param name="RecipeDate">处方日期</param>
        /// <returns>透析列表数据</returns>
        public static HemodialysisModel.MED_HEMO_RECIPEDataTable GetRecipeByHemodialysisIDAndDate(string pHemodialysisID, DateTime pRecipeDate)
        {
            HemodialysisModel.MED_HEMO_RECIPEDataTable Result = new HemodialysisModel.MED_HEMO_RECIPEDataTable();
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemodialysisID);
            Params[1] = IDatabase.BuildDbParameter("RecipeDate", DbType.Date, pRecipeDate);
            return GetData<HemodialysisModel.MED_HEMO_RECIPEDataTable>(Result, "GetRecipeByHemodialysisIDAndDate", Params);
        }

        /// <summary>
        ///根据日期和透析编号得到处方、透析机、治疗方式数据
        /// </summary>
        /// <param name="pHemodialysisID">透析号</param>
        /// <param name="pDate">排班日期</param>
        /// <returns>透析列表数据</returns>
        public static HemodialysisModel.GetPatientRecipeInfoDataTable GetPatientRecipeInfo(string pHemodialysisID, string pDate)
        {
            HemodialysisModel.GetPatientRecipeInfoDataTable Result = new HemodialysisModel.GetPatientRecipeInfoDataTable();
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemodialysisID);
            Params[1] = IDatabase.BuildDbParameter("DIALYSIS_DATE", DbType.String, pDate);
            return GetData<HemodialysisModel.GetPatientRecipeInfoDataTable>(Result, "GetPatientRecipeInfo", Params);
        }

        /// <summary>
        /// 根据病区号码获取全部病人信息列表从
        /// </summary>
        /// <param name="pWard_Code">病区编号</param>
        /// <returns></returns>
        public static HemodialysisModel.MED_PAT_MASTER_INDEXDataTable GetPatientMasterIndexList(string pWardCode)
        {
            HemodialysisModel.MED_PAT_MASTER_INDEXDataTable Result = new HemodialysisModel.MED_PAT_MASTER_INDEXDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("Ward_Code", DbType.String, pWardCode);
            return GetData<HemodialysisModel.MED_PAT_MASTER_INDEXDataTable>(Result, "GetPatientMasterIndexList", Params);
        }

        /// <summary>
        /// inp_np号同步获取病人信息
        /// </summary>
        /// <param name="pPatientID">InpNo</param>
        /// <returns>得到病人住院信息</returns>
        public static HemodialysisModel.MED_PAT_MASTER_INDEXDataTable GetPatientMasterIndexByInpNo(string pInpNo, string pWardCode)
        {
            HemodialysisModel.MED_PAT_MASTER_INDEXDataTable Result = new HemodialysisModel.MED_PAT_MASTER_INDEXDataTable();
            DbParameter[] Params = new DbParameter[1];
            // Params[0] = IDatabase.BuildDbParameter("Ward_Code", DbType.String, pWardCode);
            Params[0] = IDatabase.BuildDbParameter("InpNo", DbType.String, pInpNo);
            return GetData<HemodialysisModel.MED_PAT_MASTER_INDEXDataTable>(Result, "GetPatientMasterIndexByInpNo", Params);
        }

        /// <summary>
        /// 根据病人住院号同步获取病人信息
        /// </summary>
        /// <param name="pPatientID">病人住院号</param>
        /// <returns>得到病人住院信息</returns>
        public static HemodialysisModel.MED_PAT_MASTER_INDEXDataTable GetPatientMasterIndexByPatientID(string pPatientID, string pWardCode)
        {
            HemodialysisModel.MED_PAT_MASTER_INDEXDataTable Result = new HemodialysisModel.MED_PAT_MASTER_INDEXDataTable();
            DbParameter[] Params = new DbParameter[1];
            // Params[0] = IDatabase.BuildDbParameter("Ward_Code", DbType.String, pWardCode);
            Params[0] = IDatabase.BuildDbParameter("PatientID", DbType.String, pPatientID);
            return GetData<HemodialysisModel.MED_PAT_MASTER_INDEXDataTable>(Result, "GetPatientMasterIndexByPatientID", Params);
        }

        /// <summary>
        /// 根据病人ID得到病人数量
        /// </summary>
        /// <param name="pPatientID">病人住院ID</param>
        /// <returns></returns>
        public static int GetPatientCountByPatientID(string pPatientID)
        {
            int result = 0;
            string strSql = "select count(1) from MED_PATIENTS where PATIENT_ID= '" + pPatientID + "'";
            object obj = IDatabase.ExecuteScalar(strSql, null);
            if (obj != null)
            {
                int.TryParse(obj.ToString(), out result);
            }
            return result;
        }

        /// <summary>
        /// 根据当前日期得到处方号数量
        /// </summary>
        /// <returns></returns>
        public static int GetRecipeIDCount()
        {
            int result = 0;
            string strNowDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            string strSql = "SELECT COUNT(1) FROM MED_HEMO_RECIPE WHERE SUBSTR(RECIPE_ID,0,10) = '" + strNowDate + "'";
            object obj = IDatabase.ExecuteScalar(strSql, null);
            if (obj != null)
            {
                int.TryParse(obj.ToString(), out result);
            }
            return result;
        }

        /// <summary>
        /// 根据病人透析号得到状态为1的处方数量
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <returns></returns>
        public static int GetRecipeStatusCountByHemoID(string pHemoID)
        {
            int result = 0;
            string strSql = "SELECT COUNT(1) FROM MED_HEMO_RECIPE WHERE hemodialysis_id = '" + pHemoID + "' and status = '1'";
            object obj = IDatabase.ExecuteScalar(strSql, null);
            if (obj != null)
            {
                int.TryParse(obj.ToString(), out result);
            }
            return result;
        }

        /// <summary>
        /// 根据透析号和净化方式得到对应的处方数量
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="pPurificationMode">净化方式</param>
        /// <returns>对应的处方数量</returns>
        public static int GetRecipeCountByPurificationMode(string pHemoID, string pPurificationMode)
        {
            int result = 0;
            string strNowDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            string strSql = @"select count(1) from
            (select m.item_name,t.hemodialysis_id from med_hemo_recipe t
            left join med_common_itemlist m on t.purification_mode = m.item_id
            where t.hemodialysis_id='" + pHemoID + "' and m.item_name like '%" + pPurificationMode + "%' and t.RECIPE_TYPE='1'  and t.status='1')";
            object obj = IDatabase.ExecuteScalar(strSql, null);
            if (obj != null)
            {
                int.TryParse(obj.ToString(), out result);
            }
            return result;
        }

        /// <summary>
        /// 根据日期判断有效的临时医嘱数量
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="pDate">日期</param>
        /// <returns>对应的处方数量</returns>
        public static int GetTempRecipeCountByDate(string pHemoID, DateTime pDate)
        {
            int result = 0;
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("pDate", DbType.DateTime, pDate);

            string strSql = @"select count(1) from
            med_hemo_recipe t where t.hemodialysis_id='" + pHemoID + "' and trunc(t.recipe_date)=trunc(:pDate) and t.RECIPE_TYPE='0' and t.status!='2'";
            object obj = IDatabase.ExecuteScalar(strSql, Params);
            if (obj != null)
            {
                int.TryParse(obj.ToString(), out result);
            }
            return result;
        }

        /// <summary>
        /// 得到该处方是否已经存在治疗单中的数量
        /// </summary>
        /// <param name="pRecipeID">处方编号</param>
        /// <returns></returns>
        public static int GetRecipeCountInCureList(string pRecipeID)
        {
            int result = 0;
            string strNowDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            string strSql = @"select count(1) from med_cure_main where RECIPE_ID='" + pRecipeID + "'";
            object obj = IDatabase.ExecuteScalar(strSql, null);
            if (obj != null)
            {
                int.TryParse(obj.ToString(), out result);
            }
            return result;
        }

        /// <summary>
        /// 得到新生成的处方ID号
        /// </summary>
        /// <returns></returns>
        public static string GetNewRecipeID()
        {
            string result = string.Empty;
            string strNowDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            HemodialysisModel.MED_HEMO_RECIPEDataTable dt = new HemodialysisModel.MED_HEMO_RECIPEDataTable();
            int RecipeIDCount = GetRecipeIDCount();
            string strNewIDSql = @"SELECT to_char(sysdate,'yyyy-mm-dd')||'-'||(MAX(SUBSTR(RECIPE_ID,12,4))+1) as RECIPE_ID
                        FROM MED_HEMO_RECIPE WHERE SUBSTR(RECIPE_ID,0,10) = :TodayDate";//2013-08-10报错！！！！
            if (RecipeIDCount == 0)
            {
                //初始化处方ID
                result = strNowDate + "-1000";
            }
            else
            {//新生成处方ID
                DbParameter[] Params = new DbParameter[1];
                Params[0] = IDatabase.BuildDbParameter("TodayDate", DbType.String, strNowDate);
                object obj = IDatabase.ExecuteScalar(strNewIDSql, Params);
                result = obj.ToString();
            }
            return result;
        }

        /// <summary>
        /// 获取所有启用的系统消息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_COMMON_MESSAGEDataTable GetAllMessage(decimal type)
        {
            HemodialysisModel.MED_COMMON_MESSAGEDataTable result = new HemodialysisModel.MED_COMMON_MESSAGEDataTable();
            DbParameter[] Params = new DbParameter[1];

            Params[0] = IDatabase.BuildDbParameter("TYPE", DbType.Decimal, type);

            return GetData<HemodialysisModel.MED_COMMON_MESSAGEDataTable>(result, "GetAllMessage", Params);
        }

        /// <summary>
        /// 保存系统消息
        /// </summary>
        /// <param name="messageDataTable"></param>
        /// <returns></returns>
        public static int SaveMsgInfo(HemodialysisModel.MED_COMMON_MESSAGEDataTable messageDataTable)
        {
            return SaveData<HemodialysisModel.MED_COMMON_MESSAGEDataTable>(messageDataTable);
        }

        /// <summary>
        /// 保存系统消息为已读
        /// </summary>
        /// <param name="msgID"></param>
        /// <returns></returns>
        public static int SaveMsgInfoToMarkRead(string msgID)
        {
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("MSG_ID", DbType.String, msgID);
            //STATUS 1：未读；2：已读
            return IDatabase.ExecuteNonQuery("UPDATE MED_COMMON_MESSAGE SET STATUS = '2' WHERE MSG_ID = :MSG_ID", Params);
        }

        /// <summary>
        /// 根据透析号获取患者临时与长期处方执行状态与内容列表
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <returns></returns>
        public static DataTable GetQueryRecipeList(string pHemoID)
        {
            DataTable result = new DataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            result = GetData(result, "GetQueryRecipeList", Params);
            return result;
        }

        /// <summary>
        /// 根据当前已排班人员的透析ID生成当日临时处方
        /// </summary>
        /// <returns></returns>
        public static int SaveTodayRecipes()
        {
            IDatabase database = DatabaseFactory.Create();
            //创建当天排班的临时处方的存储过程
            return database.ExecuteNonQuery("CALL PRO_MED_SaveTodayRecipes()");
        }
        #endregion

        #region 治疗单

        /// <summary>
        /// 计算患者透析次数
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <returns></returns>
        public static DataTable GetCureCountByHemoID(string pHemoID)
        {
            DataTable result = new DataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            result = GetData(result, "GetCureCountByHemoID", Params);
            return result;
        }

        public static DataTable GetMainCureListByHemoID(string pHemoID)
        {
            DataTable result = new DataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            result = GetData(result, "GetMainCureListByHemoID", Params);
            return result;
        }

        /// <summary>
        /// 根据当前日期得到治疗单数量
        /// </summary>
        /// <returns></returns>
        public static int GetCureIDCount()
        {
            int result = 0;
            string strNowDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            string strSql = "SELECT COUNT(1) FROM MED_CURE_MAIN WHERE SUBSTR(CURE_ID,0,10) = '" + strNowDate + "'";
            object obj = IDatabase.ExecuteScalar(strSql, null);
            if (obj != null)
            {
                int.TryParse(obj.ToString(), out result);
            }
            return result;
        }

        /// <summary>
        /// 得到新生成的治疗单ID号 
        /// </summary>
        /// <returns></returns>
        public static string GetNewCureID()
        {
            string result = string.Empty;
            //            string strNowDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            //            HemodialysisModel.MED_CURE_MAINDataTable dt = new HemodialysisModel.MED_CURE_MAINDataTable();
            //            int RecipeIDCount = GetCureIDCount();
            //            string strNewIDSql = @"SELECT to_char(sysdate,'yyyy-mm-dd')||'-'||(MAX(SUBSTR(CURE_ID,12,4))+1) as CURE_ID
            //                        FROM MED_CURE_MAIN WHERE SUBSTR(CURE_ID,0,10) = :TodayDate";
            //            if (RecipeIDCount == 0) {
            //                //初始治疗单ID
            //                result = strNowDate + "-1000";
            //            }
            //            else {//新生成治疗单ID
            //                DbParameter[] Params = new DbParameter[1];
            //                Params[0] = IDatabase.BuildDbParameter("TodayDate", DbType.String, strNowDate);
            //                object obj = IDatabase.ExecuteScalar(strNewIDSql, Params);
            //                result = obj.ToString();
            //            }

            string strSql = "select to_char(SYSDATE,'yyyymmdd')||lpad(MED_CURE_MAIN_NEWID.nextval, 8, '0') as recipe_id from dual";
            object obj = IDatabase.ExecuteScalar(strSql, null);
            result = obj.ToString();
            return result;
        }

        /// <summary>
        /// 根据治疗单编号得到治疗数据
        /// </summary>
        /// <param name="pCureID">治疗单编号</param>
        /// <returns></returns>
        public static HemodialysisModel.MED_CURE_MAINDataTable GetMainCureByCureID(string pCureID)
        {
            HemodialysisModel.MED_CURE_MAINDataTable Result = new HemodialysisModel.MED_CURE_MAINDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("CURE_ID", DbType.String, pCureID);
            return GetData<HemodialysisModel.MED_CURE_MAINDataTable>(Result, "GetMainCureByCureID", Params);
        }

        /// <summary>
        /// 根据病人透析号得到治疗数据
        /// </summary>
        /// <param name="pCureID">病人透析号</param>
        /// <returns></returns>
        public static HemodialysisModel.MED_CURE_MAINDataTable GetMainCureByHemoID(string pHemoID)
        {
            HemodialysisModel.MED_CURE_MAINDataTable Result = new HemodialysisModel.MED_CURE_MAINDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            return GetData<HemodialysisModel.MED_CURE_MAINDataTable>(Result, "GetMainCureByHemoID", Params);
        }

        /// <summary>
        /// 根据病人透析号得到治疗数据
        /// </summary>
        /// <param name="pCureID">病人透析号</param>
        /// <param name="pBeginDate">透析开始日期</param>
        /// <param name="pEndDate">透析结束日期</param>
        /// <returns></returns>
        public static HemodialysisModel.MED_CURE_MAINDataTable GetMainCureByHemoIDAndDate(string pHemoID, DateTime pBeginDate, DateTime pEndDate)
        {
            HemodialysisModel.MED_CURE_MAINDataTable Result = new HemodialysisModel.MED_CURE_MAINDataTable();
            DbParameter[] Params = new DbParameter[3];
            Params[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            Params[1] = IDatabase.BuildDbParameter("BEGIN_DATE", DbType.DateTime, pBeginDate);
            Params[2] = IDatabase.BuildDbParameter("END_DATE", DbType.DateTime, pEndDate);
            return GetData<HemodialysisModel.MED_CURE_MAINDataTable>(Result, "GetMainCureByHemoIDAndDate", Params);
        }

        /// <summary>
        /// 根据病人透析号获取透析参数数据
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable GetHemoParamsByHemoID(string hemoId)
        {
            HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable result = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            return GetData<HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable>(result, "GetHemoParamsByHemoID", Params);
        }

        public static HemodialysisModel.MED_CURE_MAINDataTable GetMainCureByRecipeId(string pRecipeId)
        {
            HemodialysisModel.MED_CURE_MAINDataTable Result = new HemodialysisModel.MED_CURE_MAINDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("RECIPE_ID", DbType.String, pRecipeId);
            return GetData<HemodialysisModel.MED_CURE_MAINDataTable>(Result, "GetMainCureByRecipeId", Params);
        }

        public static string GetOrderComNo()
        {
            string returnStr = string.Empty;
            DataTable dt = new DataTable();
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetOrderComNo");
            database.Fill(sql, dt);
            returnStr = dt.Rows[0][0].ToString();
            return returnStr;
        }

        /// <summary>
        /// 根据病人透析号和治疗单编号得到治疗单数量
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="CureCreateDate">透析日期</param>
        /// <returns>返回治疗单数量</returns>
        public static int GetMainCureCountByCreateDate(string pHemoID, DateTime pCureCreateDate)
        {
            int result = 0;
            HemodialysisModel.MED_CURE_MAINDataTable dt = new HemodialysisModel.MED_CURE_MAINDataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            dbParams[1] = IDatabase.BuildDbParameter("CURE_CREATE_DATE", DbType.String, Utilities.Utility.CDate(pCureCreateDate.ToString()).ToShortDateString());
            dt = GetData<HemodialysisModel.MED_CURE_MAINDataTable>(dt, "GetMainCureCountByCreateDate", dbParams);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = dt.Rows.Count;
            }
            return result;
        }

        /// <summary>
        /// 根据查询条件返回对应的透析病人单列表
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="pCureCreateDate">透析日期/排班日期</param>
        /// <param name="pBanCi">班次</param>
        /// <param name="pName">姓名</param>
        /// <returns>对应的透析人员列表</returns>
        public static DataTable GetPrintCureList(string pHemoID, string pCureCreateDate, string pBanCi, string pName)
        {
            DataTable result = new DataTable();
            if (pCureCreateDate.Length > 0)
            {
                pCureCreateDate = Utilities.Utility.CDate(pCureCreateDate.ToString()).ToString("yyyy/MM/dd");
            }
            DbParameter[] dbParams = new DbParameter[4];
            dbParams[0] = IDatabase.BuildDbParameter("BANCI_ID", DbType.String, pBanCi);
            dbParams[1] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            dbParams[2] = IDatabase.BuildDbParameter("NAME", DbType.String, pName);
            dbParams[3] = IDatabase.BuildDbParameter("CURE_CREATE_DATE", DbType.String, pCureCreateDate);
            //2013-09-04 福总现场OLEDB方式添加的参数
            //dbParams[4] = IDatabase.BuildDbParameter("CURE_CREATE_DATE_OLE", DbType.String, pCureCreateDate);
            return GetData(result, "GetPrintCureList", dbParams);
        }

        /// <summary>
        /// 根据病人透析号和治疗方式分组得到治疗数据
        /// </summary>
        /// <returns></returns> 
        public static DataTable GetMainCureGroupByHemoIDAndPurificationMode(DateTime beginCureCreateDate, DateTime endCureCreateDate)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINCURE_CREATE_DATE", DbType.String, beginCureCreateDate);
            dbParams[1] = IDatabase.BuildDbParameter("ENDCURE_CREATE_DATE", DbType.String, endCureCreateDate);

            return GetData(result, "GetMainCureGroupByHemoIDAndPurificationMode", dbParams);
        }

        /// <summary>
        /// 保存治疗单信息方法
        /// </summary>
        /// <param name="pCureDataTable">治疗单数据表</param>
        /// <returns></returns>
        public static int SaveCureMain(HemodialysisModel.MED_CURE_MAINDataTable pCureDataTable)
        {
            return SaveData<HemodialysisModel.MED_CURE_MAINDataTable>(pCureDataTable);
        }

        /// <summary>
        /// 保存CRRT治疗单
        /// </summary>
        /// <param name="dtCure"></param>
        /// <returns></returns>
        public static int SaveCRRTCureMain(HemodialysisModel.MED_CURE_MAIN_CRRTDataTable dtCure)
        {
            return SaveData<HemodialysisModel.MED_CURE_MAIN_CRRTDataTable>(dtCure);
        }

        /// <summary>
        /// 根据治疗单编号得到对应透析参数数据列表
        /// </summary>
        /// <param name="pCureID">治疗单编号</param>
        /// <returns></returns>
        public static HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable GetHemoParametersByCureID(string pCureID)
        {
            HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable Result = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("CURE_ID", DbType.String, pCureID);
            return GetData<HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable>(Result, "GetHemoParametersByCureID", Params);
        }

        /// <summary>
        /// 根据治疗单编号得到对应已删除透析参数数据列表
        /// </summary>
        /// <param name="pCureID">治疗单编号</param>
        /// <returns></returns>
        public static HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable GetDeleteHemoParametersByCureID(string pCureID)
        {
            HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable Result = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("CURE_ID", DbType.String, pCureID);
            return GetData<HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable>(Result, "GetDeleteHemoParametersByCureID", Params);
        }

        /// <summary>
        /// 根据透析参数ID获取透析参数列表数据
        /// </summary>
        /// <param name="hemoParamId"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable GetHemoParametersByHemoParamId(string hemoParamId)
        {
            HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable Result = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_PARAMETERS_ID", DbType.String, hemoParamId);
            return GetData<HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable>(Result, "GetHemoParametersByHemoParamId", Params);
        }


        public static HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable GetHemoParametersByHemoParamRow(string hemoParamId, int rowNumber1, int rowNumber2, string sqlByParams)
        {
            HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable Result = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();
            DbParameter[] Params = new DbParameter[3];
            Params[0] = IDatabase.BuildDbParameter("CURE_ID", DbType.String, hemoParamId);
            Params[1] = IDatabase.BuildDbParameter("ROWNUMBER1", DbType.Int32, rowNumber1);
            Params[2] = IDatabase.BuildDbParameter("ROWNUMBER2", DbType.Int32, rowNumber2);
            if (sqlByParams == "sqlByParams")
            {
                return GetData<HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable>(Result, "GetHemoParametersByHemoParamRow", Params);
            }
            else
            {
                return GetData<HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable>(Result, "GetHemoParametersByHemoParamRowNew", Params);
            }

        }


        /// <summary>
        /// 得到对应透析参数数据列表
        /// </summary>
        /// <param name="pHemoID"></param>
        /// <param name="beginCreateTime"></param>
        /// <param name="endCreateTime"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable GetHemoParameters(string pHemoID, DateTime beginCreateTime, DateTime endCreateTime)
        {
            HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable result = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();
            DbParameter[] dbParams = new DbParameter[3];

            dbParams[0] = IDatabase.BuildDbParameter("BEGIN_CREATE_DATE", DbType.Date, beginCreateTime);
            dbParams[1] = IDatabase.BuildDbParameter("END_CREATE_DATE", DbType.Date, endCreateTime);
            dbParams[2] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);

            return GetData<HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable>(result, "GetHemoParameters", dbParams);
        }

        /// <summary>
        /// 得到对应透析参数配置数据列表
        /// </summary>
        /// <returns></returns>
        public static HemoModel.MED_HEMODIALYSIS_PARAMS_TYPEDataTable GetHemoParametersType()
        {
            HemoModel.MED_HEMODIALYSIS_PARAMS_TYPEDataTable result = new HemoModel.MED_HEMODIALYSIS_PARAMS_TYPEDataTable();

            return GetData<HemoModel.MED_HEMODIALYSIS_PARAMS_TYPEDataTable>(result, "GetHemoParametersType", null);
        }

        public static int UpdataCureDrugStateByParma(string statues, string hemoID, string comNo, DateTime createDate, string cureId, string recipeId, DateTime executeDt, string nuserId)
        {
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("UpdataCureDrugStateByParma");
            var dbparms0 = database.BuildDbParameter("STATUS", DbType.String, statues);
            var dbparms1 = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoID);
            var dbparms2 = database.BuildDbParameter("CURE_ID", DbType.String, cureId);
            var dbparms3 = database.BuildDbParameter("RECIPE_ID", DbType.String, recipeId);
            var dbparms4 = database.BuildDbParameter("COM_NO", DbType.String, comNo);
            var dbparms5 = database.BuildDbParameter("CREATE_DATE", DbType.Date, createDate);
            var dbparms6 = database.BuildDbParameter("EXEC_DATE", DbType.DateTime, executeDt);
            var dbparms7 = database.BuildDbParameter("DRUG_NURSE_ID", DbType.String, nuserId);

            return database.ExecuteNonQuery(sql, new DbParameter[] { dbparms0, dbparms1, dbparms2, dbparms3, dbparms4, dbparms5, dbparms6, dbparms7 });
        }

        public static int UpdataCureDrugStateByParma(string statues, string hemoID, string comNo, string comSubNo, DateTime createDate, string cureId, string recipeId, DateTime executeDt, string nuserId)
        {
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("UpdataCureDrugStateByParma2");
            var dbparms0 = database.BuildDbParameter("STATUS", DbType.String, statues);
            var dbparms1 = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoID);
            var dbparms2 = database.BuildDbParameter("CURE_ID", DbType.String, cureId);
            var dbparms3 = database.BuildDbParameter("RECIPE_ID", DbType.String, recipeId);
            var dbparms4 = database.BuildDbParameter("COM_NO", DbType.String, comNo);
            //var dbparms5 = database.BuildDbParameter("COM_SUB_NO", DbType.String, comSubNo);
            var dbparms6 = database.BuildDbParameter("CREATE_DATE", DbType.Date, createDate);
            var dbparms7 = database.BuildDbParameter("EXEC_DATE", DbType.String, String.Empty);
            var dbparms8 = database.BuildDbParameter("DRUG_NURSE_ID", DbType.String, nuserId);

            return database.ExecuteNonQuery(sql, new DbParameter[] { dbparms0, dbparms1, dbparms2, dbparms3, dbparms4, dbparms6, dbparms7, dbparms8 });//dbparms5,
        }

        /// <summary>
        /// 保存透析参数数据方法
        /// </summary> 
        /// <param name="pHemoParametersDataTable">透析参数表</param>
        /// <returns></returns>
        public static int SaveHemoParameters(HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable pHemoParametersDataTable)
        {
            var resultInt = SaveData<HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable>(pHemoParametersDataTable);
            if (resultInt > 0)
            {
                var dtParam = GetHemoParametersByCureID(pHemoParametersDataTable[0].CURE_ID);
                //更新开始时间表
                if (dtParam != null && dtParam.Rows.Count == 1)
                {
                    var scheduleDt = PatientSchedule.PatientScheduleBll.GetPatientScheduleByRecipeId(pHemoParametersDataTable[0].RECIPE_ID);
                    if (scheduleDt != null && scheduleDt.Rows.Count > 0)
                    {
                        scheduleDt[0].START_TIME = dtParam[0].CREATE_DATE;
                    }
                }
            }
            return resultInt;
        }

        /// <summary>
        /// 根据透析编号获取用药记录列表数据
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_DRUG_USE_RECORDDataTable GetDrugUseRecordListByHemoId(string hemoId)
        {
            HemodialysisModel.MED_DRUG_USE_RECORDDataTable dtRecord = new HemodialysisModel.MED_DRUG_USE_RECORDDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetDrugUseRecordListByHemoId");
            var createTimeParamater = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            database.Fill(sql, dtRecord, new DbParameter[] { createTimeParamater });
            return dtRecord;
        }

        /// <summary>
        /// 根据治疗单编号得到对应给药数据列表
        /// </summary>
        /// <param name="pCureID">治疗单编号</param>
        /// <returns></returns>
        public static HemodialysisModel.MED_CURE_DRUGDataTable GetCureDrugByCureID(string pCureID)
        {
            HemodialysisModel.MED_CURE_DRUGDataTable Result = new HemodialysisModel.MED_CURE_DRUGDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("CURE_ID", DbType.String, pCureID);
            return GetData<HemodialysisModel.MED_CURE_DRUGDataTable>(Result, "GetCureDrugByCureID", Params);
        }

        public static HemodialysisModel.MED_CURE_DRUGDataTable GetCureDrugByHemoID(string pHemoID, DateTime currentDT)
        {
            HemodialysisModel.MED_CURE_DRUGDataTable dTable = new HemodialysisModel.MED_CURE_DRUGDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetCureDrugByHemoID");
            var createTimeParamater = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            var currentDtParamater = database.BuildDbParameter("CURRENTDT", DbType.DateTime, currentDT);
            database.Fill(sql, dTable, new DbParameter[] { createTimeParamater, currentDtParamater });
            return dTable;
        }

        public static HemodialysisModel.MED_CURE_DRUGDataTable GetCureDrugForPatientRecord(string hemoId)
        {
            HemodialysisModel.MED_CURE_DRUGDataTable dtResult = new HemodialysisModel.MED_CURE_DRUGDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            return GetData<HemodialysisModel.MED_CURE_DRUGDataTable>(dtResult, "GetCureDrugForPatientRecord", Params);
        }
        public static HemodialysisModel.MED_CURE_DRUGDataTable GetCureDrugByHemoIDAndRecipeId(string pHemoID, string pRecipeId)
        {
            HemodialysisModel.MED_CURE_DRUGDataTable dTable = new HemodialysisModel.MED_CURE_DRUGDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetCureDrugByHemoIDAndRecipeId");
            var createTimeParamater = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            var recipeStarParamater = database.BuildDbParameter("RECIPE_ID", DbType.String, pRecipeId);
            database.Fill(sql, dTable, new DbParameter[] { createTimeParamater, recipeStarParamater });
            return dTable;
        }
        public static HemodialysisModel.MED_CURE_DRUGDataTable GetValidCureDrugByHemoID(string pHemoID, DateTime dt)
        {
            HemodialysisModel.MED_CURE_DRUGDataTable dTable = new HemodialysisModel.MED_CURE_DRUGDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetValidCureDrugByHemoID");
            var createTimeParamater = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            var createTimeParamater1 = database.BuildDbParameter("CREATE_DATE", DbType.DateTime, dt);

            database.Fill(sql, dTable, new DbParameter[] { createTimeParamater, createTimeParamater1 });
            return dTable;
        }
        public static HemodialysisModel.MED_CURE_DRUGDataTable GetValidCureDrugByRoomIdAndData(string pRoomID, string banchiID, DateTime dtStart, DateTime dtEnd, string hemoId)
        {
            HemodialysisModel.MED_CURE_DRUGDataTable dTable = new HemodialysisModel.MED_CURE_DRUGDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetValidCureDrugByRoomIdAndData");
            if (hemoId.Equals("kka") || string.IsNullOrEmpty(hemoId))
            {
                sql = string.Format(sql, string.Empty);
            }
            else
            {
                string whereSql = " AND T.HEMODIALYSIS_ID= '" + hemoId + "'";
                sql = string.Format(sql, whereSql);
            }
            var createTimeParamater = database.BuildDbParameter("DIALYSIS_ROOM_ID", DbType.String, pRoomID);
            var createTimeParamater1 = database.BuildDbParameter("DTSTAR", DbType.DateTime, dtStart);
            var createTimeParamater2 = database.BuildDbParameter("DTEND", DbType.DateTime, dtEnd);
            var createTimeParamater3 = database.BuildDbParameter("BANCI_ID", DbType.String, banchiID);
            database.Fill(sql, dTable, new DbParameter[] { createTimeParamater, createTimeParamater1, createTimeParamater2, createTimeParamater3 });

            return dTable;
        }

        public static HemodialysisModel.MED_CURE_DRUGDataTable GetValidCureDrugByHemoRecipeID(string pHemoID, string pRecipeId)
        {
            HemodialysisModel.MED_CURE_DRUGDataTable dTable = new HemodialysisModel.MED_CURE_DRUGDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetValidCureDrugByHemoRecipeID");
            var createTimeParamater = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            var createTimeParamater1 = database.BuildDbParameter("RECIPE_ID", DbType.DateTime, pRecipeId);

            database.Fill(sql, dTable, new DbParameter[] { createTimeParamater, createTimeParamater1 });
            return dTable;
        }

        public static HemodialysisModel.MED_CURE_DRUGDataTable GetUnExcuteCureDrugByHemoID(string pHemoID, DateTime dt)
        {
            HemodialysisModel.MED_CURE_DRUGDataTable dTable = new HemodialysisModel.MED_CURE_DRUGDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetUnExcuteCureDrugByHemoID");
            var createTimeParamater = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            var createTimeParamater1 = database.BuildDbParameter("CREATE_DATE", DbType.DateTime, dt);

            database.Fill(sql, dTable, new DbParameter[] { createTimeParamater, createTimeParamater1 });
            return dTable;
        }
        public static HemodialysisModel.MED_CURE_DRUGDataTable GetUnExcuteCureDrugByHemoRecipeId(string pHemoID, string pRecipeId)
        {
            HemodialysisModel.MED_CURE_DRUGDataTable dTable = new HemodialysisModel.MED_CURE_DRUGDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetUnExcuteCureDrugByHemoRecipeId");
            var createTimeParamater = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            var createTimeParamater1 = database.BuildDbParameter("RECIPE_ID", DbType.String, pRecipeId);

            database.Fill(sql, dTable, new DbParameter[] { createTimeParamater, createTimeParamater1 });
            return dTable;
        }
        public static HemodialysisModel.MED_CURE_LONGDRUGDataTable GetLongCureDrugByHemoID(string pHemoID)
        {
            HemodialysisModel.MED_CURE_LONGDRUGDataTable dTable = new HemodialysisModel.MED_CURE_LONGDRUGDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetLongCureDrugByHemoID");
            var createTimeParamater = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            database.Fill(sql, dTable, new DbParameter[] { createTimeParamater });
            return dTable;
        }

        /// <summary>
        /// 保存给药数据列表方法
        /// </summary>
        /// <param name="pCureDrugDataTable">给药数据表</param>
        /// <returns></returns>
        public static int SaveCureDrug(HemodialysisModel.MED_CURE_DRUGDataTable pCureDrugDataTable)
        {
            return SaveData<HemodialysisModel.MED_CURE_DRUGDataTable>(pCureDrugDataTable);
        }
        public static int SaveCureLongDrug(HemodialysisModel.MED_CURE_LONGDRUGDataTable pCureDrugDataTable)
        {
            return SaveData<HemodialysisModel.MED_CURE_LONGDRUGDataTable>(pCureDrugDataTable);
        }
        public static int SaveCureDrugTemplate(HemodialysisModel.MED_CURE_DRUG_TEMPLATEDataTable pCureDrugTemplateDT)
        {
            return SaveData<HemodialysisModel.MED_CURE_DRUG_TEMPLATEDataTable>(pCureDrugTemplateDT);
        }
        public static HemodialysisModel.MED_CURE_DRUG_TEMPLATEDataTable GetTemplateByParmas(string docID)
        {
            HemodialysisModel.MED_CURE_DRUG_TEMPLATEDataTable resultDT = new HemodialysisModel.MED_CURE_DRUG_TEMPLATEDataTable();
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetTemplateByParmas");

            var param0 = database.BuildDbParameter("DOCTOR_ID", DbType.String, docID);
            database.Fill(sql, resultDT, new DbParameter[] { param0 });
            return resultDT;

        }
        public static bool SaveDeleteOutputAndCheckMaterialData()
        {
            using (DbWrapTransaction transaction = IDatabase.CreateDbTransaction())
            {
                try
                {
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 保存包括治疗单主表、给药表、透析参数表的数据集
        /// </summary>
        /// <param name="pDataSet">治疗单数据集</param>
        /// <returns></returns>
        public static bool SaveAllCure(DataSet pDataSet)
        {
            int iCount = 0;
            using (DbWrapTransaction transaction = IDatabase.CreateDbTransaction())
            {
                try
                {
                    //治疗单主表
                    HemodialysisModel.MED_CURE_MAINDataTable pCureDataTable;
                    //给药表
                    HemodialysisModel.MED_CURE_DRUGDataTable pCureDrugDataTable;
                    //透析参数表
                    HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable pHemoParametersDataTable;

                    if (pDataSet != null && pDataSet.Tables.Count > 0)
                    {
                        //保存治疗单主表
                        if (pDataSet.Tables["MED_CURE_MAIN"] != null && pDataSet.Tables["MED_CURE_MAIN"].Rows.Count > 0)
                        {
                            pCureDataTable = (HemodialysisModel.MED_CURE_MAINDataTable)pDataSet.Tables["MED_CURE_MAIN"];
                            iCount = SaveCureMain(pCureDataTable);
                            //判断是否是临时医嘱，如果是开始治疗后更新医嘱状态
                            HemodialysisModel.MED_HEMO_RECIPEDataTable reciptDt = GetRecipeByRecipeID(pCureDataTable.Rows[0]["RECIPE_ID"].ToString());
                            if (reciptDt != null && reciptDt.Rows.Count > 0 && reciptDt.Rows[0]["RECIPE_TYPE"].ToString() == "0")
                            {
                                reciptDt.Rows[0]["STATUS"] = "0";
                                SaveRecipe(reciptDt);
                            }
                        }
                        //保存给药表
                        if (pDataSet.Tables["MED_CURE_DRUG"] != null && pDataSet.Tables["MED_CURE_DRUG"].Rows.Count > 0)
                        {
                            pCureDrugDataTable = (HemodialysisModel.MED_CURE_DRUGDataTable)pDataSet.Tables["MED_CURE_DRUG"];
                            iCount = SaveCureDrug(pCureDrugDataTable);
                        }
                        //保存透析参数表
                        if (pDataSet.Tables["MED_HEMODIALYSIS_PARAMETERS"] != null && pDataSet.Tables["MED_HEMODIALYSIS_PARAMETERS"].Rows.Count > 0)
                        {
                            pHemoParametersDataTable = (HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable)pDataSet.Tables["MED_HEMODIALYSIS_PARAMETERS"];
                            iCount = SaveHemoParameters(pHemoParametersDataTable);
                        }
                    }
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    transaction.Dispose();
                }
            }
        }

        /// <summary>
        /// 根据治疗单编号，返回包括治疗单主表、给药表、透析参数表的数据集
        /// </summary>
        /// <param name="pCureID">治疗单编号</param>
        /// <returns>返回治疗单数据集</returns>
        public static DataSet GetAllCure(string pCureID)
        {
            DataSet ds = new DataSet();
            //病人基本信息表
            PatientModel.MED_PATIENTSDataTable _PatientDataTable = new PatientModel.MED_PATIENTSDataTable();
            //治疗单主表
            HemodialysisModel.MED_CURE_MAINDataTable _CureMainDataTable = new HemodialysisModel.MED_CURE_MAINDataTable();
            //给药表
            HemodialysisModel.MED_CURE_DRUGDataTable _CureDrugDataTable = new HemodialysisModel.MED_CURE_DRUGDataTable();
            //透析参数表
            HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable _HemoParametersDataTable = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();

            _CureMainDataTable = GetMainCureByCureID(pCureID);
            if (_CureMainDataTable != null && _CureMainDataTable.Rows.Count > 0)
            {
                ds.Tables.Add(_CureMainDataTable);
                _PatientDataTable = PatientBll.GetPatientListByParams("", _CureMainDataTable.Rows[0]["HEMODIALYSIS_ID"].ToString());
                if (_PatientDataTable != null && _PatientDataTable.Rows.Count > 0)
                {
                    ds.Tables.Add(_PatientDataTable);
                }
            }

            _CureDrugDataTable = GetCureDrugByCureID(pCureID);
            if (_CureDrugDataTable != null && _CureDrugDataTable.Rows.Count > 0)
            {
                ds.Tables.Add(_CureDrugDataTable);
            }

            _HemoParametersDataTable = GetHemoParametersByCureID(pCureID);

            if (_HemoParametersDataTable != null)//&& _HemoParametersDataTable.Rows.Count > 0
            {
                ds.Tables.Add(_HemoParametersDataTable);

            }

            return ds;
        }

        /// <summary>
        /// 药品参数匹配
        /// </summary>
        /// <param name="pCureID"></param>
        /// <param name="pCreateDate"></param>
        /// <returns></returns>
        public static DataTable GetPamarsDrugInfo(string pCureID, DateTime pCreateDate)
        {
            DataTable dt = new DataTable();
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("CURE_ID", DbType.String, pCureID);
            Params[1] = IDatabase.BuildDbParameter("CREATE_DATE", DbType.String, pCreateDate.ToString());
            return GetData<DataTable>(dt, "GetPamarsDrugInfo", Params);
        }


        /// <summary>
        /// 根据透析号和处方号返回处方信息与人员信息
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="pRecipeID">处方号</param>
        /// <returns></returns>
        public static DataSet GetRecipeAndPatientInfo(string pHemoID, string pRecipeID)
        {
            DataSet ds = new DataSet();
            //病人基本信息表
            PatientModel.MED_PATIENTSDataTable _PatientDataTable = new PatientModel.MED_PATIENTSDataTable();

            _PatientDataTable = PatientBll.GetPatientListByParams("", pHemoID);
            if (_PatientDataTable != null && _PatientDataTable.Rows.Count > 0)
            {
                ds.Tables.Add(_PatientDataTable);
            }

            //处方表
            DataTable _RecipeDataTable = HemodialysisBll.GetRecipeInfoInCureFunction(pRecipeID);
            _RecipeDataTable.TableName = "MED_HEMO_RECIPE";
            if (_RecipeDataTable != null && _RecipeDataTable.Rows.Count > 0)
            {
                ds.Tables.Add(_RecipeDataTable);
            }
            return ds;
        }

        /// <summary>
        /// 在治疗单页面，未开始治疗时读取默认处方信息的内容
        /// </summary>
        /// <param name="pRecipeID">处方编号</param>
        /// <returns></returns>
        public static DataTable GetRecipeInfoInCureFunction(string pRecipeID)
        {
            DataTable Result = new DataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("RECIPE_ID", DbType.String, pRecipeID);
            return GetData<DataTable>(Result, "GetRecipeInfoInCureFunction", Params);

        }

        public static int ExecuteProLogInfos()
        {
            int ReturnInt = 0;
            //长期医嘱生成临时医嘱后进行存储过程日志生成。确保存储过程不再去生成此条临时医嘱
            //获取是否已经有生成日志，如果有日志就不再写入不然就要写入。
            DataTable Result = new DataTable();

            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("ExecuteProLogInfos");
            var sql2 = StoredScript.Get("ExecuteProLogInfos2");
            database.Fill(sql, Result);
            if (Convert.ToInt32(Result.Rows[0][0].ToString()) <= 0) //无数据写入否则不做操作。
            {
                ReturnInt = database.ExecuteNonQuery(sql2);
            }
            else
            {
                ReturnInt = 0;
            }

            return ReturnInt;

        }
        /// <summary>
        /// 得到透析列表数据
        /// </summary>
        /// <param name="pDialysisDate">透析日期</param>
        /// <param name="pBanciID">班次</param>
        /// <param name="pName">患者姓名</param>
        /// <param name="pHemoID">患者透析号</param>
        /// <returns>透析单列表</returns>
        public static HemodialysisModel.GetCureListDataTable GetCureList(string pDialysisDate, string pBanciID, string pName, string pHemoID)
        {
            HemodialysisModel.GetCureListDataTable Result = new HemodialysisModel.GetCureListDataTable();
            DbParameter[] Params = new DbParameter[4];
            Params[0] = IDatabase.BuildDbParameter("DIALYSIS_DATE", DbType.String, Utilities.Utility.CDate(pDialysisDate).ToShortDateString());
            Params[1] = IDatabase.BuildDbParameter("BANCI_ID", DbType.String, pBanciID);
            Params[2] = IDatabase.BuildDbParameter("NAME", DbType.String, pName);
            Params[3] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            return GetData<HemodialysisModel.GetCureListDataTable>(Result, "GetCureList", Params);
        }
        public static string GetCureID(string pDialysisDate, string pHemoID)
        {
            DataTable dt = new DataTable();
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("CURE_CREATE_DATE", DbType.String, Utilities.Utility.CDate(pDialysisDate).ToShortDateString());
            Params[1] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            GetData<DataTable>(dt, "GetCureIDByHemoIDAndCureData", Params);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        public static PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleList(string userId, DateTime beginDialysisDate, DateTime endDialysisDate, string banciID)
        {
            PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable result = new PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable();
            DbParameter[] dbParams = new DbParameter[4];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINDIALYSIS_DATE", DbType.String, beginDialysisDate);
            dbParams[1] = IDatabase.BuildDbParameter("ENDDIALYSIS_DATE", DbType.String, endDialysisDate);
            dbParams[2] = IDatabase.BuildDbParameter("BANCI_ID", DbType.String, banciID.Trim());
            dbParams[3] = IDatabase.BuildDbParameter("USER_ID", DbType.String, userId.Trim());

            return GetData<PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable>(result, "GetPatientScheduleList", dbParams);
        }
        public static int GetCleanUpTimes(string pHemoID)
        {
            DataTable DT = new DataTable();
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetCleanUpTimes");
            var dbparams = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            database.Fill(sql, DT, new DbParameter[] { dbparams });
            if (DT.Rows[0][0].ToString().Trim().Length > 0)
            {
                return int.Parse(DT.Rows[0][0].ToString());
            }
            else
            {
                return 1;
            }

        }


        public static DataTable GetBaseHemoInfo(DateTime _begionTime, DateTime _endTime)
        {
            DataTable Result = new DataTable();
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, _begionTime);
            Params[1] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, _endTime);
            return GetData<DataTable>(Result, "GetBaseHemoInfo", Params);
        }

        public static DataTable GetSexScale(DateTime _begionTime, DateTime _endTime)
        {
            DataTable Result = new DataTable();
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, _begionTime);
            Params[1] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, _endTime);
            return GetData<DataTable>(Result, "GetSexScale", Params);
        }

        public static DataTable GetAgeScale(DateTime _begionTime, DateTime _endTime)
        {
            DataTable Result = new DataTable();
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, _begionTime);
            Params[1] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, _endTime);
            return GetData<DataTable>(Result, "GetAgeScale", Params);
        }

        public static string GetAllHemoCount(DateTime _begionTime, DateTime _endTime)
        {
            DataTable Result = new DataTable();
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, _begionTime);
            Params[1] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, _endTime);
            GetData<DataTable>(Result, "GetAllHemoCount", Params);

            if (Result.Rows.Count > 0)
            {
                return Result.Rows[0][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        public static DataTable GetInfectiousScale(DateTime _begionTime, DateTime _endTime)
        {
            DataTable Result = new DataTable();
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, _begionTime);
            Params[1] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, _endTime);
            return GetData<DataTable>(Result, "GetInfectiousScale", Params);
        }

        public static DataTable GetHemoCoutScale(DateTime _begionTime, DateTime _endTime)
        {
            DataTable Result = new DataTable();
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, _begionTime);
            Params[1] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, _endTime);
            return GetData<DataTable>(Result, "GetHemoCoutScale", Params);
        }

        /// <summary>
        /// 获取血透治疗统计列表
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DataTable GetHemoCureCountList(string year)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("YEAR", DbType.String, year);
            return GetData<DataTable>(result, "GetHemoCureCountList", dbParams);
        }

        /// <summary>
        /// 得到患者上次治疗单内容
        /// </summary>
        /// <param name="pHemoID">透析ID</param>
        /// <returns></returns>
        public static string GetLastTimeCureDataByID(string pHemoID, DateTime pCURE_CREATE_DATE)
        {
            string result = string.Empty;
            DataTable dt = new DataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID.Trim());
            dbParams[1] = IDatabase.BuildDbParameter("CURE_CREATE_DATE", DbType.DateTime, pCURE_CREATE_DATE);

            dt = GetData<DataTable>(dt, "GetLastTimeCureDataByID", dbParams);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = dt.Rows[0]["AFTER_DRY_WEIGHT"].ToString();
            }
            return result;
        }

        /// <summary>
        /// 得到患者历次透过体重及时间
        /// </summary>
        /// <param name="pHemoID">透析ID</param>
        /// <returns></returns>
        public static DataTable GetDryWeightListByHemoID(string pHemoID)
        {
            DataTable dt = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID.Trim());
            return GetData<DataTable>(dt, "GetDryWeightListByHemoID", dbParams);
        }

        /// <summary>
        /// 得到患者最近治疗单信息及时间
        /// </summary>
        /// <param name="hemoId">透析ID</param>
        /// <returns></returns>
        public static DataTable GetRecentCureInfoByHemoId(string hemoId)
        {
            DataTable dt = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId.Trim());
            return GetData<DataTable>(dt, "GetRecentCureInfoByHemoId", dbParams);
        }

        /// <summary>
        /// 得到患者最近血压及时间
        /// </summary>
        /// <param name="hemoId">透析ID</param>
        /// <returns></returns>
        public static DataTable GetRecentPressureByHemoId(string hemoId)
        {
            DataTable dt = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId.Trim());
            return GetData<DataTable>(dt, "GetRecentPressureByHemoId", dbParams);
        }

        /// <summary>
        /// 根据透析ID得到患者历次治疗单信息
        /// </summary>
        /// <param name="hemoId">透析ID</param>
        /// <returns></returns>
        public static DataTable GetPastCureInfoByHemoId(string hemoId)
        {
            DataTable dt = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId.Trim());
            return GetData<DataTable>(dt, "GetPastCureInfoByHemoId", dbParams);
        }

        /// <summary>
        /// 根据透析ID得到患者历次血压信息
        /// </summary>
        /// <param name="hemoId">透析ID</param>
        /// <returns></returns>
        public static DataTable GetPastPressureByHemoId(string hemoId)
        {
            DataTable dt = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId.Trim());
            return GetData<DataTable>(dt, "GetPastPressureByHemoId", dbParams);
        }


        /// <summary>
        /// 将患者历次透析记录与透析中首次血压合并处理
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPatientCureAndPastPressureByHemoId(string hemoId)
        {
            DataTable dt = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId.Trim());
            return GetData<DataTable>(dt, "GetPatientCureAndPastPressureByHemoId", dbParams);
        }

        /// 将患者历次透析记录与透析中首次血压合并处理
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPatientCureAndPastPressureByParam(string hemoId, int minData, int maxData)
        {
            DataTable dt = new DataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId.Trim());
            dbParams[1] = IDatabase.BuildDbParameter("MINDATA", DbType.Int32, minData);
            dbParams[2] = IDatabase.BuildDbParameter("MAXDATA", DbType.Int32, maxData);

            return GetData<DataTable>(dt, "GetPatientCureAndPastPressureByParam", dbParams);
        }

        /// <summary>
        /// 根据透析日期统计患者透析器个数，从透析单取，只有开始治疗才能有数据
        /// </summary>
        /// <param name="pCureDate">治疗时间</param>
        /// <returns></returns>
        public static DataTable GetCurePurificationModeCountByDate(string pCureDate)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("CURE_CREATE_DATE", DbType.DateTime, Utilities.Utility.CDate(pCureDate).Date);
            return GetData<DataTable>(result, "GetCurePurificationModeCountByDate", dbParams);
        }

        /// <summary>
        /// 根据治疗单ID和班次获取CRRT治疗单记录
        /// </summary>
        /// <param name="cureId"></param>
        /// <param name="banci"></param>
        /// <param name="createDate"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_CURE_MAIN_CRRTDataTable GetCRRTCureByCureIdAndBanci(string cureId, string banci, DateTime createDate)
        {
            HemodialysisModel.MED_CURE_MAIN_CRRTDataTable dtResult = new HemodialysisModel.MED_CURE_MAIN_CRRTDataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("CURE_ID", DbType.String, cureId);
            dbParams[1] = IDatabase.BuildDbParameter("CRRT_CLASS", DbType.String, banci);
            dbParams[2] = IDatabase.BuildDbParameter("CREATE_DATE", DbType.DateTime, createDate);
            return GetData<HemodialysisModel.MED_CURE_MAIN_CRRTDataTable>(dtResult, "GetCRRTCureByCureIdAndBanci", dbParams);
        }

        /// <summary>
        /// 根据治疗单ID获取CRRT治疗单记录
        /// </summary>
        /// <param name="cureId"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_CURE_MAIN_CRRTDataTable GetCRRTCureByCureId(string cureId)
        {
            HemodialysisModel.MED_CURE_MAIN_CRRTDataTable dtResult = new HemodialysisModel.MED_CURE_MAIN_CRRTDataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("CURE_ID", DbType.String, cureId);
            return GetData<HemodialysisModel.MED_CURE_MAIN_CRRTDataTable>(dtResult, "GetCRRTCureByCureId", dbParams);
        }

        #endregion

        #region 报表相关
        /// <summary>
        /// 得到时间段内不同透析类型的透析次数
        /// </summary>
        /// <param name="beginCureCreateDate">开始日期</param>
        /// <param name="endCureCreateDate">结束日期</param>
        /// <returns></returns>
        public static DataTable GetAllCureTypeCount(DateTime beginCureCreateDate, DateTime endCureCreateDate)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINCURE_CREATE_DATE", DbType.String, beginCureCreateDate);
            dbParams[1] = IDatabase.BuildDbParameter("ENDCURE_CREATE_DATE", DbType.String, endCureCreateDate);
            ////2013-09-04 福总现场OLEDB方式添加的参数
            //dbParams[2] = IDatabase.BuildDbParameter("BEGINCURE_CREATE_DATE2", DbType.String, beginCureCreateDate);
            //dbParams[3] = IDatabase.BuildDbParameter("ENDCURE_CREATE_DATE3", DbType.String, endCureCreateDate);
            //dbParams[4] = IDatabase.BuildDbParameter("BEGINCURE_CREATE_DATE4", DbType.String, beginCureCreateDate);
            //dbParams[5] = IDatabase.BuildDbParameter("ENDCURE_CREATE_DATE5", DbType.String, endCureCreateDate);
            //dbParams[6] = IDatabase.BuildDbParameter("BEGINCURE_CREATE_DATE6", DbType.String, beginCureCreateDate);
            //dbParams[7] = IDatabase.BuildDbParameter("ENDCURE_CREATE_DATE7", DbType.String, endCureCreateDate);
            //dbParams[8] = IDatabase.BuildDbParameter("BEGINCURE_CREATE_DATE8", DbType.String, beginCureCreateDate);
            //dbParams[9] = IDatabase.BuildDbParameter("ENDCURE_CREATE_DATE9", DbType.String, endCureCreateDate);
            //dbParams[10] = IDatabase.BuildDbParameter("BEGINCURE_CREATE_DATE10", DbType.String, beginCureCreateDate);
            //dbParams[11] = IDatabase.BuildDbParameter("ENDCURE_CREATE_DATE11", DbType.String, endCureCreateDate);
            //dbParams[12] = IDatabase.BuildDbParameter("BEGINCURE_CREATE_DATE12", DbType.String, beginCureCreateDate);
            //dbParams[13] = IDatabase.BuildDbParameter("ENDCURE_CREATE_DATE13", DbType.String, endCureCreateDate);
            //dbParams[14] = IDatabase.BuildDbParameter("BEGINCURE_CREATE_DATE14", DbType.String, beginCureCreateDate);
            //dbParams[15] = IDatabase.BuildDbParameter("ENDCURE_CREATE_DATE15", DbType.String, endCureCreateDate);
            //dbParams[16] = IDatabase.BuildDbParameter("BEGINCURE_CREATE_DATE16", DbType.String, beginCureCreateDate);
            //dbParams[17] = IDatabase.BuildDbParameter("ENDCURE_CREATE_DATE17", DbType.String, endCureCreateDate);
            //dbParams[18] = IDatabase.BuildDbParameter("BEGINCURE_CREATE_DATE18", DbType.String, beginCureCreateDate);
            //dbParams[19] = IDatabase.BuildDbParameter("ENDCURE_CREATE_DATE19", DbType.String, endCureCreateDate);
            //dbParams[20] = IDatabase.BuildDbParameter("BEGINCURE_CREATE_DATE20", DbType.String, beginCureCreateDate);
            //dbParams[21] = IDatabase.BuildDbParameter("ENDCURE_CREATE_DATE21", DbType.String, endCureCreateDate);
            return GetData(result, "GetAllCureTypeCount", dbParams);
        }

        /// <summary>
        /// 得到时间段内不同透析类型的透析次数
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllCureCountByMonth(DateTime _bgionTime, DateTime _endTime)
        {
            DataTable result = new DataTable();
            string strSql = StoredScript.Get("GetAllCureCountByMonth");
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, _bgionTime);
            dbParams[1] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, _endTime);
            result = IDatabase.Fill(strSql, result, dbParams);
            return result;
        }
        #endregion

        #region 检验项目质控
        /// <summary>
        /// 得到全部病人乙肝、丙肝、梅毒、HIV检查结果列表
        /// </summary>
        /// <returns></returns>
        public static HemodialysisModel.MED_INFECTIOUS_CHECKDataTable GetMedInfectiousCheckList(string pHEMODIALYSIS_ID)
        {
            HemodialysisModel.MED_INFECTIOUS_CHECKDataTable Result = new HemodialysisModel.MED_INFECTIOUS_CHECKDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHEMODIALYSIS_ID);
            return GetData<HemodialysisModel.MED_INFECTIOUS_CHECKDataTable>(Result, "GetMedInfectiousCheckList", Params);
        }

        /// <summary>
        /// 得到全部病人乙肝、丙肝、梅毒、HIV检查结果列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMedInfectiousCheck()
        {
            DataTable result = new DataTable();
            string strSql = StoredScript.Get("GetMedInfectiousCheckList");
            result = IDatabase.Fill(strSql, result);
            return result;
        }

        public static int UpdateMedInfectiousInfoByID(string pInfectiousCheckID)
        {
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("INFECTIOUS_ID", DbType.String, pInfectiousCheckID);
            return IDatabase.ExecuteNonQuery("UPDATE MED_INFECTIOUS_CHECK SET STATUS='1' where INFECTIOUS_ID = :INFECTIOUS_ID  AND STATUS='0'", Params);
        }

        /// <summary>
        /// 保存传染病检查数据
        /// </summary>
        /// <param name="pInfectiousCheckDataTable"></param>
        /// <returns></returns>
        public static int SaveMedInfectiousCheck(HemodialysisModel.MED_INFECTIOUS_CHECKDataTable pInfectiousCheckDataTable)
        {
            return SaveData<HemodialysisModel.MED_INFECTIOUS_CHECKDataTable>(pInfectiousCheckDataTable);
        }

        /// <summary>
        /// 根据传染病表的ID得到一条传染病检查数据 INFECTIOUS_CHECK
        /// </summary>
        /// <param name="pInfectiousCheckID">传染病表ID</param>
        /// <returns></returns>
        public static HemodialysisModel.MED_INFECTIOUS_CHECKDataTable GetMedInfectiousInfoByID(string pInfectiousCheckID)
        {
            HemodialysisModel.MED_INFECTIOUS_CHECKDataTable Result = new HemodialysisModel.MED_INFECTIOUS_CHECKDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("INFECTIOUS_ID", DbType.String, pInfectiousCheckID);
            return GetData<HemodialysisModel.MED_INFECTIOUS_CHECKDataTable>(Result, "GetMedInfectiousInfoByID", Params);
        }        
        #endregion

        #region 导向式治疗相关
        /// <summary>
        /// 根据患者透析号和排班治疗日期得到导向式的治疗的相关数据
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="pDialysisDate">透析排班日期</param>
        /// <returns></returns>
        public static DataSet GetPatientGuideVisitInfo(string pHemoID, DateTime pDialysisDate)
        {
            DataSet ds = new DataSet();
            PatientModel.MED_PATIENTSDataTable patient = new PatientModel.MED_PATIENTSDataTable();
            HemoModel.MED_VASCULAR_ACCESSDataTable vascularAccess = new HemoModel.MED_VASCULAR_ACCESSDataTable();
            PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable schedual = new PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable();
            HemodialysisModel.MED_HEMO_RECIPEDataTable recipe = new HemodialysisModel.MED_HEMO_RECIPEDataTable();
            HemodialysisModel.GetCureListDataTable cure = new HemodialysisModel.GetCureListDataTable();
            patient = PatientBll.GetPatientListByParams("", pHemoID);
            vascularAccess = VascuarAccessBll.GetVascularAccessListByHEMODIALYSIS_ID(pHemoID);
            schedual = PatientSchedule.PatientScheduleBll.GetPatientScheduleSignle(pDialysisDate, pHemoID);
            // recipe = GetRecipeByHemodialysisID(pHemoID);
            cure = GetCureList(pDialysisDate.ToString(), "", "", pHemoID);
            if (patient != null && patient.Rows.Count > 0)
            {
                ds.Tables.Add((DataTable)(patient));
            }
            if (vascularAccess != null && vascularAccess.Rows.Count > 0)
            {
                ds.Tables.Add((DataTable)(vascularAccess));
            }
            if (schedual != null && schedual.Rows.Count > 0)
            {
                ds.Tables.Add((DataTable)(schedual));
            }
            //if (recipe != null && recipe.Rows.Count > 0) {
            //    ds.Tables.Add((DataTable)(recipe));
            //}
            if (cure != null && cure.Rows.Count > 0)
            {
                ds.Tables.Add((DataTable)(cure));
            }
            return ds;
        }

        public static ConfigModel.MED_COMMON_ITEMLISTDataTable GetItemListByHemoIDandItemType(string pHemoID, string pItemType)
        {
            ConfigModel.MED_COMMON_ITEMLISTDataTable result = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID.Trim());
            dbParams[1] = IDatabase.BuildDbParameter("ITEM_TYPE", DbType.String, pItemType.Trim());

            return GetData<ConfigModel.MED_COMMON_ITEMLISTDataTable>(result, "GetConfigListByHemoIDandItemType", dbParams);
        }
        /// <summary>
        /// 获取患者列次的透析通路
        /// </summary>
        /// <param name="pHemoID"></param>
        /// <param name="pItemType"></param>
        /// <returns></returns>
        public static HemoModel.MED_VASCULAR_ACCESSDataTable GetPatientVasular_AccessDt(string pHemoID)
        {
            var result = new HemoModel.MED_VASCULAR_ACCESSDataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID.Trim());

            return GetData<HemoModel.MED_VASCULAR_ACCESSDataTable>(result, "GetPatientVasular_AccessDt", dbParams);
        }
        #endregion

        #region 病人疾病ICD
        /// <summary>
        /// 得到疾病ICD分类
        /// </summary>
        /// <returns>ICD疾病分类列表</returns>
        public static HemodialysisModel.MED_ICD_TYPEDataTable GetIcdType()
        {
            HemodialysisModel.MED_ICD_TYPEDataTable result = new HemodialysisModel.MED_ICD_TYPEDataTable();
            return GetData<HemodialysisModel.MED_ICD_TYPEDataTable>(result, "GetIcdType", null);
        }

        /// <summary>
        /// 根据ICD分类编码取出对应的ICD病种
        /// </summary>
        /// <param name="pICD">ICD编码</param>
        /// <returns></returns>
        public static HemodialysisModel.MED_ICD_LISTDataTable GetIcdListByID(string pICD)
        {
            HemodialysisModel.MED_ICD_LISTDataTable result = new HemodialysisModel.MED_ICD_LISTDataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("ICD", DbType.String, pICD.Trim());
            return GetData<HemodialysisModel.MED_ICD_LISTDataTable>(result, "GetIcdListByID", dbParams);
        }

        /// <summary>
        /// 根据ICD分类跨度编码取出对应的ICD病种
        /// </summary>
        /// <param name="pIdList">ICD分类跨度</param>
        /// <returns></returns>
        public static DataTable GetIcdListByIDList(string pIdList)
        {
            DataTable result = new DataTable();
            string strWhere = string.Empty;
            //分析并拼接病种ID编码，有三种规则：C51-C58与A15.0-A15.3,A16.0-A16.2与S02,S12,S22,S32,S42,S52,S62,S72,S82,S92,T02,T08,T10,T12
            //程序需要完善
            if (pIdList.IndexOf("-") > -1)
            {
                string[] arrWhere = pIdList.Split('-');
                //I10-I15
                if (arrWhere.Length > 0)
                {
                    string iCode = (arrWhere[0].Substring(0, 1));
                    int iBegin = Utilities.Utility.CInt(arrWhere[0].Substring(1, arrWhere[0].Length - 1));
                    int iEnd = Utilities.Utility.CInt(arrWhere[1].Substring(1, arrWhere[0].Length - 1));
                    int iResult = iEnd - iBegin;
                    for (int i = 0; i <= iResult; i++)
                    {
                        strWhere += " or substr(id,0,3) = '" + iCode + (iBegin + i).ToString() + "'";
                    }
                }
            }

            if (strWhere.Length > 0)
            {
                strWhere = strWhere.Substring(3, strWhere.Length - 3);
            }

            string strSql = "select * from med_icd_list where " + strWhere;
            IDatabase.Fill(strSql, result);
            return result;
        }

        /// <summary>
        /// 删除透析参数
        /// </summary>
        /// <param name="pID">透析参数ID</param>
        /// <returns></returns>
        public static int DeleteHemodialysisParametersByID(string pID)
        {
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("DeleteHemodialysisParametersByID");
            var dbParams = database.BuildDbParameter("HEMODIALYSIS_PARAMETERS_ID", DbType.String, pID.Trim());
            return database.ExecuteNonQuery(sql, new DbParameter[] { dbParams });
        }

        public static int UpdateIcdPinYin()
        {
            HemodialysisModel.MED_ICD_LISTDataTable icdlist = new HemodialysisModel.MED_ICD_LISTDataTable();
            icdlist = GetData<HemodialysisModel.MED_ICD_LISTDataTable>(icdlist, "GetIcdList", null);
            foreach (HemodialysisModel.MED_ICD_LISTRow row in icdlist)
            {
                row.ICD_PINYIN = PinYinConverter.GetPYString(row.ICD_NAME);
            }
            return IDatabase.Update(icdlist as DataTable, "MED_ICD_LIST");//SaveData<HemodialysisModel.MED_ICD_LISTDataTable>(icdlist);
        }
        public static int DeleteCureORLongDrugByID(string drugType, string cureID)
        {
            var database = DatabaseFactory.Create();
            var sql = string.Empty;
            if (drugType == "long")
            {
                sql = StoredScript.Get("DeleteLongDrugByID");
            }
            else
            {
                sql = StoredScript.Get("DeleteDrugListByID");
            }
            var dbParams = database.BuildDbParameter("CURE_DRUG_ID", DbType.String, cureID.Trim());
            return database.ExecuteNonQuery(sql, new DbParameter[] { dbParams });
        }
        public static HemodialysisModel.MED_ICD_LISTDataTable GetIcdListByName(string pICD_NAME)
        {
            HemodialysisModel.MED_ICD_LISTDataTable result = new HemodialysisModel.MED_ICD_LISTDataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("ICD_NAME", DbType.String, pICD_NAME.Trim());
            return GetData<HemodialysisModel.MED_ICD_LISTDataTable>(result, "GetIcdListByName", dbParams);
        }

        public static DataTable GetUNExcuteOrdersbyData(DateTime dt)
        {
            DataTable dtable = new DataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetUNExcuteOrdersbyData");
            var createTimeParamater = database.BuildDbParameter("Cure_Create_Date", DbType.DateTime, dt);
            database.Fill(sql, dtable, new DbParameter[] { createTimeParamater });
            return dtable;
        }
        #endregion

        #region 健康宣教
        /// <summary>
        /// 保存健康宣教内容
        /// </summary>
        /// <param name="healthDataTable">健康宣教数据</param>
        /// <returns></returns>
        public static int SaveHealthEducationInfo(HemodialysisModel.MED_HEALTH_EDUCATIONDataTable healthDataTable)
        {
            return SaveData<HemodialysisModel.MED_HEALTH_EDUCATIONDataTable>(healthDataTable);
        }

        /// <summary>
        /// 根据患者透析单号和健康宣教ID获取列表数据
        /// </summary>
        /// <param name="pHemoID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_HEALTH_EDUCATIONDataTable GetHealthEducationByHemoIdAndId(string pHemoID, string id)
        {
            HemodialysisModel.MED_HEALTH_EDUCATIONDataTable result = new HemodialysisModel.MED_HEALTH_EDUCATIONDataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            dbParams[1] = IDatabase.BuildDbParameter("ID", DbType.String, id);
            return GetData<HemodialysisModel.MED_HEALTH_EDUCATIONDataTable>(result, "GetHealthEducationByHemoIdAndId", dbParams);
        }

        public static int DeleteHealthEducationByHemoIdAndId(string pHemoID, string id)
        {
            DbParameter[] dbParam = new DbParameter[2];
            dbParam[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            dbParam[1] = IDatabase.BuildDbParameter("ID", DbType.String, id);
            return DeleteData("DeleteHealthEducationByHemoIdAndId", dbParam);
        }


        public static DataTable GetHealthEducationListByHemoID(string pHemoID)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID.Trim());
            return GetData(result, "GetHealthEducationListByHemoID", dbParams);
        }

        /// <summary>
        /// 根据患者透析号得到宣教数据
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <returns></returns>
        public static HemodialysisModel.MED_HEALTH_EDUCATIONDataTable GetHealthEducationByHemoID(string pHemoID)
        {
            HemodialysisModel.MED_HEALTH_EDUCATIONDataTable result = new HemodialysisModel.MED_HEALTH_EDUCATIONDataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID.Trim());
            return GetData<HemodialysisModel.MED_HEALTH_EDUCATIONDataTable>(result, "GetHealthEducationByHemoID", dbParams);
        }
        public static HemodialysisModel.MED_HEALTH_EDUCATIONDataTable GetHealthEducationByDateTime(DateTime startTime, DateTime endTime)
        {

            HemodialysisModel.MED_HEALTH_EDUCATIONDataTable result = new HemodialysisModel.MED_HEALTH_EDUCATIONDataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("STARTTIME", DbType.DateTime, startTime);
            dbParams[1] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            return GetData<HemodialysisModel.MED_HEALTH_EDUCATIONDataTable>(result, "GetHealthEducationByDateTime", dbParams);

        }

        public static DrugModel.MED_PATIENT_FOLLOWUPDataTable GetFollowUpByDateTime(DateTime startTime, DateTime endTime)
        {

            DrugModel.MED_PATIENT_FOLLOWUPDataTable result = new DrugModel.MED_PATIENT_FOLLOWUPDataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("STARTTIME", DbType.DateTime, startTime);
            dbParams[1] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            return GetData<DrugModel.MED_PATIENT_FOLLOWUPDataTable>(result, "GetFollowUpByDateTime", dbParams);
        }
        /// <summary>
        /// 根据患者透析号得到宣教报表数据
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <returns></returns>
        public static DataTable GetHealthEducationReportByHemoID(string pHemoID, string pId)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID.Trim());
            dbParams[1] = IDatabase.BuildDbParameter("ID", DbType.String, pId.Trim());
            return GetData(result, "GetHealthEducationReportByHemoID", dbParams);
        }
        #endregion

        #region 患者病程记录
        /// <summary>
        /// 保存患者病程记录方法
        /// </summary>
        /// <param name="ProgressNoteDataTable">病程记录内容表</param>
        /// <returns></returns>
        public static int SavePatientProgressNoteInfo(HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable ProgressNoteDataTable)
        {
            return SaveData<HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable>(ProgressNoteDataTable);
        }

        /// <summary>
        /// 根据ID得到病程记录数据
        /// </summary>
        /// <param name="hemoId">透析ID</param>
        /// <returns></returns>
        public static DataTable GetPatientProgressNoteById(string id)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("ID", DbType.String, id);
            return GetData<DataTable>(result, "GetPatientProgressNoteById", dbParams);
        }

        /// <summary>
        /// 根据患者透析ID得到病程记录数据
        /// </summary>
        /// <param name="hemoId">透析ID</param>
        /// <returns></returns>
        public static HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable GetPatientProgressNoteByHemoId(string hemoId)
        {
            HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable result = new HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId.Trim());
            return GetData<HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable>(result, "GetPatientProgressNoteByHemoId", dbParams);
        }

        /// <summary>
        /// 根据患者透析ID、日期得到病程记录数据
        /// </summary>
        /// <param name="hemoId">透析ID</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public static HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable GetPatientProgressNoteByHemoIdAndDate(string hemoId, DateTime beginDate, DateTime endDate)
        {
            HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable result = new HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId.Trim());
            dbParams[1] = IDatabase.BuildDbParameter("BEGINDATE", DbType.Date, beginDate);
            dbParams[2] = IDatabase.BuildDbParameter("ENDDATE", DbType.Date, endDate);
            return GetData<HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable>(result, "GetPatientProgressNoteByHemoIdAndDate", dbParams);
        }

        /// <summary>
        /// 根据ID删除患者病程记录
        /// </summary>
        /// <param name="pID">主键ID</param>
        /// <returns></returns>
        public static int DeletePatientProgressNoteById(string id)
        {
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("DeletePatientProgressNoteById");
            var dbParams = database.BuildDbParameter("ID", DbType.String, id.Trim());
            return database.ExecuteNonQuery(sql, new DbParameter[] { dbParams });
        }

        /// <summary>
        /// 根据患者透析ID得到病程记录数据
        /// </summary>
        /// <param name="pHemoID">透析ID</param>
        /// <returns></returns>
        public static HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable GetPatientProgressNoteByIDAndCreateDate(string pHemoID)
        {
            HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable result = new HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID.Trim());
            return GetData<HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable>(result, "GetPatientProgressNoteHemoID", dbParams);
        }

        /// <summary>
        /// 根据透析ID和创建时间得到一条病程记录
        /// </summary>
        /// <param name="hemoID">透析ID</param>
        /// <param name="createDate">创建日期</param>
        /// <returns></returns>
        public static HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable GetPatientProgressNoteByIDAndCreateDate(string hemoID, DateTime createDate)
        {
            HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable RESULT = new HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetPatientProgressNoteByIDAndCreateDate");
            DbParameter param0 = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoID.Trim());
            DbParameter param1 = database.BuildDbParameter("CREATEDATE", DbType.DateTime, createDate);
            database.Fill(sql, RESULT, new DbParameter[] { param0, param1 });
            return RESULT;
        }
        #endregion

        #region 获取血透机采集表数据功能

        /// <summary>
        /// 根据透析机标示与采集时间获取透析机数据
        /// </summary>
        /// <param name="MonitorLable">透析机标示</param>
        /// <param name="pCreateDate">采集时间</param>
        /// <returns>透析机数据</returns>
        public static HemodialysisModel.MED_HEMO_PARAMETERS_COLLECTIONDataTable GetHemoParametersCollectionByMonitorAndDate(string MonitorLable, DateTime pCreateDate)
        {
            HemodialysisModel.MED_HEMO_PARAMETERS_COLLECTIONDataTable result = new HemodialysisModel.MED_HEMO_PARAMETERS_COLLECTIONDataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("MONITOR_LABEL", DbType.String, MonitorLable.Trim());
            dbParams[1] = IDatabase.BuildDbParameter("CREATE_DATE", DbType.DateTime, pCreateDate);
            return GetData<HemodialysisModel.MED_HEMO_PARAMETERS_COLLECTIONDataTable>(result, "GetHemoParametersCollectionByMonitorAndDate", dbParams);
        }

        /// <summary>
        /// 根据透析机标识与采集时间获取透析机数据
        /// </summary>
        /// <param name="MonitorLable"></param>
        /// <param name="pCreateDate"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_HEMO_PARAMETERS_COLLECTIONDataTable GetHemoParametersCollectionByMonitorAndDoubleDate(string MonitorLable, DateTime pCreateDate, DateTime beginTime, DateTime endTime)
        {
            HemodialysisModel.MED_HEMO_PARAMETERS_COLLECTIONDataTable result = new HemodialysisModel.MED_HEMO_PARAMETERS_COLLECTIONDataTable();
            DbParameter[] dbParams = new DbParameter[4];
            dbParams[0] = IDatabase.BuildDbParameter("MONITOR_LABEL", DbType.String, MonitorLable.Trim());
            dbParams[1] = IDatabase.BuildDbParameter("CREATE_DATE", DbType.DateTime, pCreateDate);
            dbParams[2] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[3] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            return GetData<HemodialysisModel.MED_HEMO_PARAMETERS_COLLECTIONDataTable>(result, "GetHemoParametersCollectionByMonitorAndDoubleDate", dbParams);
        }

        /// <summary>
        /// 获取数据采集设置列表
        /// </summary>
        /// <returns></returns>
        public static HemodialysisModel.MED_HEMO_PARAMETERS_SETTINGDataTable GetDataGatherSetList()
        {
            HemodialysisModel.MED_HEMO_PARAMETERS_SETTINGDataTable dtSetting = new HemodialysisModel.MED_HEMO_PARAMETERS_SETTINGDataTable();
            return GetData<HemodialysisModel.MED_HEMO_PARAMETERS_SETTINGDataTable>(dtSetting, "GetDataGatherSetList", null);
        }

        /// <summary>
        /// 保存数据采集设置
        /// </summary>
        /// <param name="dtSetting"></param>
        /// <returns></returns>
        public static int SaveDataGatherSet(HemodialysisModel.MED_HEMO_PARAMETERS_SETTINGDataTable dtSetting)
        {
            return SaveData<HemodialysisModel.MED_HEMO_PARAMETERS_SETTINGDataTable>(dtSetting);
        }

        #endregion

        #region 血管通路评估
        /// <summary>
        /// 保存患者内瘘评估记录
        /// </summary>
        /// <param name="EsitmateInBasketTable">内瘘评估记录表</param>
        /// <returns></returns>
        public static int SaveEstimateInBasketInfo(HemodialysisModel.MED_ESTIMATE_IN_BASKETDataTable EsitmateInBasketTable)
        {
            return SaveData<HemodialysisModel.MED_ESTIMATE_IN_BASKETDataTable>(EsitmateInBasketTable);
        }

        /// <summary>
        /// 根据ID得到一条内瘘评估记录
        /// </summary>
        /// <param name="pID">内瘘评估ID</param>
        /// <returns></returns>
        public static HemodialysisModel.MED_ESTIMATE_IN_BASKETDataTable GetEsimateInBasketByID(string pID)
        {
            HemodialysisModel.MED_ESTIMATE_IN_BASKETDataTable result = new HemodialysisModel.MED_ESTIMATE_IN_BASKETDataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("ID", DbType.String, pID);
            return GetData<HemodialysisModel.MED_ESTIMATE_IN_BASKETDataTable>(result, "GetEstimateInBasketByID", dbParams);
        }

        /// <summary>
        /// 根据查询条件得到内瘘评估列表内容
        /// </summary>
        /// <param name="pHemoID">透析ID</param>
        /// <param name="pName">患者姓名</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>病人内瘘评估列表</returns>
        public static DataTable GetEstimateInBasketByParams(string pHemoID, string pName, DateTime beginDate, DateTime endDate)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[4];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            dbParams[1] = IDatabase.BuildDbParameter("NAME", DbType.String, pName);
            dbParams[2] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, beginDate);
            dbParams[3] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            return GetData<DataTable>(result, "GetEstimateInBasketByParams", dbParams);
        }

        /// <summary>
        /// 根据ID删除患者内瘘评估数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteEstimateInBasketById(string id)
        {
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("ID", DbType.String, id);
            return DeleteData("DeleteEstimateInBasketById", dbParam);
        }
        #endregion

        #region 静脉导管评估

        /// <summary>
        /// 根据病人姓名&日期获取长期留置静脉导管评估数据列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable GetEstimateLongVenousByNameAndDate(string name, DateTime beginDate, DateTime endDate)
        {
            HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable dtEstimateVenous = new HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable();
            DbParameter[] dbParam = new DbParameter[3];
            dbParam[0] = IDatabase.BuildDbParameter("NAME", DbType.String, name);
            dbParam[1] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, beginDate);
            dbParam[2] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            return GetData<HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable>(dtEstimateVenous, "GetEstimateLongVenousByNameAndDate", dbParam);
        }

        /// <summary>
        /// 根据病人姓名&日期获取临时留置静脉导管评估数据列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable GetEstimateVenousCatheterByNameAndDate(string name, DateTime beginDate, DateTime endDate)
        {
            HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable dtEstimateVenous = new HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable();
            DbParameter[] dbParam = new DbParameter[3];
            dbParam[0] = IDatabase.BuildDbParameter("NAME", DbType.String, name);
            dbParam[1] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, beginDate);
            dbParam[2] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            return GetData<HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable>(dtEstimateVenous, "GetEstimateVenousCatheterByNameAndDate", dbParam);
        }

        /// <summary>
        /// 根据病人透析编号&日期获取长期留置静脉导管评估数据列表
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable GetEstimateLongVenousByHemoIdAndDate(string hemoId, DateTime beginDate, DateTime endDate)
        {
            HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable dtEstimateVenous = new HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable();
            DbParameter[] dbParam = new DbParameter[3];
            dbParam[0] = IDatabase.BuildDbParameter("HEMOID", DbType.String, hemoId);
            dbParam[1] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, beginDate);
            dbParam[2] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            return GetData<HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable>(dtEstimateVenous, "GetEstimateLongVenousByHemoIdAndDate", dbParam);
        }

        /// <summary>
        /// 根据病人透析编号&日期获取临时留置静脉导管评估数据列表
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable GetEstimateVenousCatheterByHemoIdAndDate(string hemoId, DateTime beginDate, DateTime endDate)
        {
            HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable dtEstimateVenous = new HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable();
            DbParameter[] dbParam = new DbParameter[3];
            dbParam[0] = IDatabase.BuildDbParameter("HEMOID", DbType.String, hemoId);
            dbParam[1] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, beginDate);
            dbParam[2] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            return GetData<HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable>(dtEstimateVenous, "GetEstimateVenousCatheterByHemoIdAndDate", dbParam);
        }

        /// <summary>
        /// 获取长期留置静脉导管评估数据列表
        /// </summary>
        /// <returns></returns>
        public static HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable GetEstimateLongVenousList()
        {
            HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable dtEstimateVenous = new HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable();
            return GetData<HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable>(dtEstimateVenous, "GetEstimateLongVenousList", null);
        }

        /// <summary>
        /// 获取临时留置静脉导管评估数据列表
        /// </summary>
        /// <returns></returns>
        public static HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable GetEstimateVenousCatheterList()
        {
            HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable dtEstimateVenous = new HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable();
            return GetData<HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable>(dtEstimateVenous, "GetEstimateVenousCatheterList", null);
        }

        /// <summary>
        /// 根据ID获取长期留置静脉导管评估数据列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable GetEstimateLongVenousById(string id)
        {
            HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable dtEstimateVenous = new HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable();
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("ID", DbType.String, id);
            return GetData<HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable>(dtEstimateVenous, "GetEstimateLongVenousById", dbParam);
        }

        /// <summary>
        /// 根据ID获取临时留置静脉导管评估数据列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable GetEstimateVenousCatheterById(string id)
        {
            HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable dtEstimateVenous = new HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable();
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("ID", DbType.String, id);
            return GetData<HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable>(dtEstimateVenous, "GetEstimateVenousCatheterById", dbParam);
        }

        /// <summary>
        /// 根据病人透析编号&单个日期获取长期留置静脉导管评估数据列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="createDate"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable GetEstimateLongVenousByHemoIdAndSingleDate(string hemoId, DateTime createDate)
        {
            HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable dtEstimateVenous = new HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable();
            DbParameter[] dbParam = new DbParameter[2];
            dbParam[0] = IDatabase.BuildDbParameter("HEMOID", DbType.String, hemoId);
            dbParam[1] = IDatabase.BuildDbParameter("CREATE_DATE", DbType.DateTime, createDate);
            return GetData<HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable>(dtEstimateVenous, "GetEstimateLongVenousByHemoIdAndSingleDate", dbParam);
        }

        /// <summary>
        /// 根据病人透析编号&单个日期获取临时留置静脉导管评估数据列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="createDate"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable GetEstimateVenousCatheterByHemoIdAndSingleDate(string hemoId, DateTime createDate)
        {
            HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable dtEstimateVenous = new HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable();
            DbParameter[] dbParam = new DbParameter[2];
            dbParam[0] = IDatabase.BuildDbParameter("HEMOID", DbType.String, hemoId);
            dbParam[1] = IDatabase.BuildDbParameter("CREATE_DATE", DbType.DateTime, createDate);
            return GetData<HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable>(dtEstimateVenous, "GetEstimateVenousCatheterByHemoIdAndSingleDate", dbParam);
        }

        /// <summary>
        /// 根据ID删除长期留置静脉导管评估数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteEstimateLongVenousById(string id)
        {
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("ID", DbType.String, id);
            return DeleteData("DeleteEstimateLongVenousById", dbParam);
        }

        /// <summary>
        /// 根据ID删除临时留置静脉导管评估数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteEstimateVenousCatheterById(string id)
        {
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("ID", DbType.String, id);
            return DeleteData("DeleteEstimateVenousCatheterById", dbParam);
        }

        /// <summary>
        /// 保存静脉导管评估数据
        /// </summary>
        /// <param name="dtEstimateVenous"></param>
        /// <returns></returns>
        public static int SaveEstimateVenous(DataTable dtEstimateVenous)
        {
            return SaveData<DataTable>(dtEstimateVenous);
        }

        #endregion
        /// <summary>
        /// 保存工作量统计
        /// </summary>
        /// <param name="workloadTable"></param>
        /// <returns></returns>
        public static int SaveWorkload(HemoModel.MED_WORKLOADDataTable workloadTable)
        {
            return SaveData<HemoModel.MED_WORKLOADDataTable>(workloadTable);
        }
        /// <summary>
        /// 获取工作量统计
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static HemoModel.MED_WORKLOADDataTable GetWorkloadByDate(DateTime beginDate, DateTime endDate)
        {
            //  DateTime monthFirstDate = new DateTime(date.Year, date.Month, 1);
            //  DateTime monthLastDate = monthFirstDate.AddMonths(1);

            HemoModel.MED_WORKLOADDataTable result = new HemoModel.MED_WORKLOADDataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, beginDate);
            dbParams[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            return GetData<HemoModel.MED_WORKLOADDataTable>(result, "GetWorkloadByParams", dbParams);
        }
        /// <summary>
        /// 获取工作量统计
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static HemoModel.MED_WORKLOADDataTable GetWorkloadByDateFZ(DateTime beginDate, DateTime endDate)
        {
            HemoModel.MED_WORKLOADDataTable result = new HemoModel.MED_WORKLOADDataTable();
            var database = DatabaseFactory.Create();
            var table = new DataTable();
            var sql = StoredScript.Get("GetWorkloadByParamsFZ");
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, beginDate);
            dbParams[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            database.Fill(sql, table, dbParams);
            if (table != null)
            {
                foreach (DataRow dr in table.Rows)
                {
                    var resultTemp = result.FirstOrDefault(i => i.NURSE_ID == dr["NURSE_ID"].ToString());
                    if (resultTemp == null)
                    {
                        var row = result.NewMED_WORKLOADRow();
                        row.ID = Guid.NewGuid().ToString();
                        row.NURSE_ID = dr["NURSE_ID"].ToString();
                        row.TJR = dr["NAME"].ToString();
                        row.WORKDATE = DateTime.Now;
                        switch (dr["ITEM_NAME"].ToString())
                        {
                            case "HD":
                                row.WGSXT = decimal.Parse(dr["COUNT"].ToString());
                                break;
                            case "HDF":
                                row.GTLTX = decimal.Parse(dr["COUNT"].ToString());
                                break;
                            case "HD+HP":
                                row.GYSXYTX = decimal.Parse(dr["COUNT"].ToString());
                                break;
                        }
                        result.AddMED_WORKLOADRow(row);
                    }
                    else
                    {
                        switch (dr["ITEM_NAME"].ToString())
                        {
                            case "HD":
                                resultTemp.WGSXT = decimal.Parse(dr["COUNT"].ToString());
                                break;
                            case "HDF":
                                resultTemp.GTLTX = decimal.Parse(dr["COUNT"].ToString());
                                break;
                            case "HD+HP":
                                resultTemp.GYSXYTX = decimal.Parse(dr["COUNT"].ToString());
                                break;
                        }
                    }
                }
                var resultWTemp = new HemoModel.MED_WORKLOADDataTable();
                result.CopyToDataTable<HemoModel.MED_WORKLOADRow>(resultWTemp, LoadOption.PreserveChanges);
                decimal wgsxtAl = 0;
                decimal QJRCAl = 0;
                decimal GTLTXAl = 0;
                decimal GYSXYTXAL = 0;

                foreach (var row in result)
                {
                    var wgsxt = row.IsWGSXTNull() ? 0 : row.WGSXT;
                    var GTLTX = row.IsGTLTXNull() ? 0 : row.GTLTX;
                    var GYSXYTX = row.IsGYSXYTXNull() ? 0 : row.GYSXYTX;
                    var count = wgsxt + GTLTX + GYSXYTX;
                    var resultTempTemp = resultWTemp.FirstOrDefault(i => i.NURSE_ID == row.NURSE_ID);
                    if (resultTempTemp != null)
                    {
                        resultTempTemp.WGSXT = wgsxt;
                        resultTempTemp.GTLTX = GTLTX;
                        resultTempTemp.GYSXYTX = GYSXYTX;
                        resultTempTemp.QJRC = count;
                        wgsxtAl += wgsxt;
                        GTLTXAl += GTLTX;
                        GYSXYTXAL += GYSXYTX;
                        QJRCAl += count;
                    }
                }
                var rowt = resultWTemp.NewMED_WORKLOADRow();
                rowt.ID = Guid.NewGuid().ToString();
                rowt.NURSE_ID = "hj";
                rowt.TJR = "合计";
                rowt.WORKDATE = DateTime.Now;
                rowt.WGSXT = wgsxtAl;
                rowt.GTLTX = GTLTXAl;
                rowt.GYSXYTX = GYSXYTXAL;
                rowt.QJRC = QJRCAl;
                resultWTemp.AddMED_WORKLOADRow(rowt);

                result = new HemoModel.MED_WORKLOADDataTable();
                resultWTemp.CopyToDataTable<HemoModel.MED_WORKLOADRow>(result, LoadOption.PreserveChanges);

            }
            return result;
        }
        /// <summary>
        /// 获取工作量统计
        /// </summary>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public static HemoModel.MED_WORKLOADDataTable GetWorkLoadCountByDate(DateTime dateBegin, DateTime dateEnd)
        {
            HemoModel.MED_WORKLOADDataTable result = new HemoModel.MED_WORKLOADDataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, dateBegin);
            dbParams[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, dateEnd);
            return GetData<HemoModel.MED_WORKLOADDataTable>(result, "GetWorkLoadCountByDate", dbParams);
        }
        /// <summary>
        /// 获取人员工作量统计
        /// </summary>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public static HemoModel.MED_WORKLOADDataTable GetWorkLoadNurseCountByDate(DateTime dateBegin, DateTime dateEnd)
        {
            HemoModel.MED_WORKLOADDataTable result = new HemoModel.MED_WORKLOADDataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, dateBegin);
            dbParams[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, dateEnd);
            return GetData<HemoModel.MED_WORKLOADDataTable>(result, "GetWorkLoadNurseCountByDate", dbParams);
        }
        /// <summary>
        /// GetWorkloadByParmas
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="banchiName"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static HemoModel.MED_WORKLOADDataTable GetWorkloadByParmas(string areaId, string banchiName, DateTime date)
        {
            var RESULT = new HemoModel.MED_WORKLOADDataTable();
            IDatabase database = DatabaseFactory.Create();
            DbParameter param1 = database.BuildDbParameter("WORKAREA", DbType.String, areaId);
            DbParameter param2 = database.BuildDbParameter("WORKCLASSNUM", DbType.String, banchiName);
            DbParameter param3 = database.BuildDbParameter("WORKDATE", DbType.DateTime, date);
            var sql = StoredScript.Get("GetWorkloadByParmas");
            var parms = new DbParameter[] { param1, param2, param3 };
            database.Fill(sql, RESULT, parms);
            return RESULT;

        }

        /// <summary>
        /// 获取患者的检验信息
        /// </summary>
        /// <param name="hemoId">透析号</param>
        /// <param name="whereName">查询条件比如传入白蛋白，铁蛋白，PTH水平等</param>
        /// <param name="whereFilter">检验查询条件</param>
        /// <param name="dtStar">开始时间</param>
        /// <param name="dtEnd">结束 时间</param>
        /// <returns></returns>
        public static DataTable GetHoldLabItemDt(string hemoId, string whereName, string whereFilter, DateTime dtStar, DateTime dtEnd, string condition)
        {
            DataTable dt = new DataTable();
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetHoldLabItemDt");
            string whereNameNew = "1=1";
            whereNameNew = condition;
            //switch (whereName)
            //{
            //    case "高钾血症":
            //        whereNameNew = string.Format("T2.REPORT_ITEM_NAME LIKE '%' || '{0}' || '%' AND T1.ITEM_NAME LIKE '%'||'{1}'|| '%'", "钾(间接ISE法)", "电解质检查");
            //        break;
            //    case "高磷血症":
            //        whereNameNew = string.Format("T2.REPORT_ITEM_NAME LIKE '%' || '{0}' || '%' AND T1.ITEM_NAME LIKE '%'||'{1}'|| '%'", "磷(钼酸盐紫外法)", "电解质检查");
            //        break;
            //    case "PTH":
            //        whereNameNew = string.Format("T2.REPORT_ITEM_NAME LIKE '%' || '{0}' || '%' AND T1.ITEM_NAME LIKE '%'||'{1}'|| '%'", "全段甲状旁腺素", "甲状旁腺激素测定");
            //        break;
            //    case "二氧化碳结合力":
            //        whereNameNew = string.Format("T2.REPORT_ITEM_NAME LIKE '%' || '{0}' || '%' AND T1.ITEM_NAME LIKE '%'||'{1}'|| '%'", "二氧化碳", "肝功能检查");
            //        break;
            //    case "CRP":
            //        whereNameNew = string.Format("T2.REPORT_ITEM_NAME LIKE '%' || '{0}' || '%' AND (T1.ITEM_NAME LIKE '%'||'{1}'|| '%' OR T1.ITEM_NAME LIKE '%'||'{2}'|| '%')", "C-反应蛋白", "C-反应蛋白", "超敏C反应蛋白");
            //        break;
            //    case "铁饱和度":
            //        whereNameNew = string.Format("(T2.REPORT_ITEM_NAME ='{0}' OR T2.REPORT_ITEM_NAME ='{1}' OR T2.REPORT_ITEM_NAME ='{2}') AND (T1.ITEM_NAME ='{3}' OR T1.ITEM_NAME ='{4}')", "铁", "总铁结合力", "铁(亚铁嗪法)", "铁", "总铁结合力");
            //        break;
            //    case "血红蛋白":
            //        whereNameNew = string.Format("T2.REPORT_ITEM_NAME LIKE '%' || '{0}' || '%' AND T1.ITEM_NAME LIKE '%'||'{1}'|| '%'", "血红蛋白测定", "血常规");
            //        break;
            //    case "钙":
            //        whereNameNew = string.Format("T2.REPORT_ITEM_NAME LIKE '%' || '{0}' || '%' AND T1.ITEM_NAME LIKE '%'||'{1}'|| '%'", "钙", "电解质");
            //        break;
            //    case "白蛋白":
            //        whereNameNew = string.Format("T2.REPORT_ITEM_NAME LIKE '%' || '{0}' || '%' AND T1.ITEM_NAME LIKE '%'||'{1}'|| '%'", "白蛋白", "常规生化全套检查");
            //        break;
            //    case "铁蛋白":
            //        whereNameNew = string.Format("T2.REPORT_ITEM_NAME LIKE '%' || '{0}' || '%' AND T1.ITEM_NAME LIKE '%'||'{1}'|| '%'", "铁蛋白", "铁蛋白");
            //        break;
            //    default:
            //        whereNameNew = string.Format("T2.REPORT_ITEM_NAME LIKE '%' || '{0}' || '%'", whereName);
            //        break;
            //}
            sql = string.Format(sql, hemoId, dtStar, dtEnd, whereNameNew);
            string[] filterStrings = whereFilter.Split(new string[] { "AND" }, StringSplitOptions.None);
            string commItemValue = string.Empty;
            for (int i = 0; i < filterStrings.Length; i++)
            {
                commItemValue += string.Format(" AND K.RESULT {0}", filterStrings[i].Trim());
            }
            if (whereName == "铁饱和度")
            {
                var sqlT = StoredScript.Get("GetHoldLabItemDtTie");
                sqlT = string.Format(sqlT, hemoId, dtStar, dtEnd, whereNameNew);
                DataTable dtResutlold = new DataTable();
                dtResutlold.Columns.Add("PATIENT_ID", Type.GetType("System.String"));
                DataTable dtResutl = new DataTable();
                dtResutl.Columns.Add("PATIENT_ID", Type.GetType("System.String"));
                var dtTie = database.Fill(sqlT, dt);
                if (dtTie != null && dtTie.Rows.Count > 0)
                {
                    DataTable dtTestNo = new DataTable();
                    dtTestNo.Columns.Add("TEST_NO", Type.GetType("System.String"));
                    var n = (from d in dtTie.AsEnumerable() select d["TEST_NO"]).Distinct();
                    foreach (var item in n)
                    {
                        dtTestNo.Rows.Add(item);
                    }
                    for (int i = 0; i < dtTestNo.Rows.Count; i++)
                    {
                        var dr = dtTie.Select(" TEST_NO ='" + dtTestNo.Rows[i]["TEST_NO"] + "'");
                        if (dr == null || dr.Count() <= 1)
                        {
                            continue;
                        }
                        var l1 = dr.AsEnumerable().FirstOrDefault(row => row["REPORT_ITEM_NAME"].Equals("铁") || row["REPORT_ITEM_NAME"].Equals("铁(亚铁嗪法)"));
                        var l2 = dr.AsEnumerable().FirstOrDefault(row => row["REPORT_ITEM_NAME"].Equals("总铁结合力"));
                        if (l1 != null && l2 != null)
                        {
                            try
                            {
                                var result = Math.Round(Utilities.Utility.CDecimal(l1["RESULT"].ToString()) / Utilities.Utility.CDecimal(l2["RESULT"].ToString()) * 100, 2);
                                if (result < 25)
                                {
                                    dtResutlold.Rows.Add(dtResutlold.NewRow()["PATIENT_ID"] = l1["PATIENT_ID"]);
                                }
                            }
                            catch (Exception ex)
                            { continue; }
                        }
                    }
                }
                var xy = (from d in dtResutlold.AsEnumerable() select d["PATIENT_ID"]).Distinct();
                foreach (var item in xy)
                {
                    dtResutl.Rows.Add(item);
                }
                return dtResutl;
            }
            else
            {
                sql = string.Format("{0} {1}", sql, commItemValue);
                return database.Fill(sql, dt);
            }
        }

        /// <summary>
        /// SaveComplication
        /// </summary>
        /// <param name="complicationTable"></param>
        /// <returns></returns>
        public static int SaveComplication(HemoModel.MED_COMPLICATION_OTHERDataTable complicationTable)
        {
            return SaveData<HemoModel.MED_COMPLICATION_OTHERDataTable>(complicationTable);
        }
        /// <summary>
        /// MED_COMPLICATION_OTHERDataTable
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static HemoModel.MED_COMPLICATION_OTHERDataTable GetComplicationByDate(DateTime date)
        {
            DateTime monthFirstDate = new DateTime(date.Year, date.Month, 1);
            DateTime monthLastDate = monthFirstDate.AddMonths(1);

            HemoModel.MED_COMPLICATION_OTHERDataTable result = new HemoModel.MED_COMPLICATION_OTHERDataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, monthFirstDate);
            dbParams[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, monthLastDate);
            return GetData<HemoModel.MED_COMPLICATION_OTHERDataTable>(result, "GetComplicationByParams", dbParams);
        }
        /// <summary>
        /// MED_COMPLICATION_OTHERDataTable
        /// </summary>
        /// <param name="DialysisId"></param>
        /// <param name="cureId"></param>
        /// <returns></returns>
        public static HemoModel.MED_COMPLICATION_OTHERDataTable GetComplicationByDialysisAndCure(string DialysisId, string cureId)
        {
            HemoModel.MED_COMPLICATION_OTHERDataTable result = new HemoModel.MED_COMPLICATION_OTHERDataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.DateTime, DialysisId);
            dbParams[1] = IDatabase.BuildDbParameter("CURE_ID", DbType.DateTime, cureId);
            return GetData<HemoModel.MED_COMPLICATION_OTHERDataTable>(result, "GetComplicationByDialysisAndCure", dbParams);
        }
        /// <summary>
        /// MED_COMPLICATION_OTHERDataTable
        /// </summary>
        /// <param name="Firstdate"></param>
        /// <param name="LastData"></param>
        /// <returns></returns>
        public static HemoModel.MED_COMPLICATION_OTHERDataTable GetComplicationByParams(DateTime Firstdate, DateTime LastData)
        {
            HemoModel.MED_COMPLICATION_OTHERDataTable result = new HemoModel.MED_COMPLICATION_OTHERDataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, Firstdate);
            dbParams[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, LastData);
            return GetData<HemoModel.MED_COMPLICATION_OTHERDataTable>(result, "GetComplicationByParams", dbParams);
        }
        /// <summary>
        /// GetSubjectiveComfortData
        /// </summary>
        /// <param name="dateMonth"></param>
        /// <returns></returns>
        public static DataTable GetSubjectiveComfortData(string dateMonth)
        {
            var dataTable = new System.Data.DataTable("SubjectiveComfortData");
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("DATEMONTH", DbType.String, dateMonth);
            return GetData<System.Data.DataTable>(dataTable, "GetSubjectiveComfortData", dbParam);

        }
        /// <summary>
        /// SaveBorrowData
        /// </summary>
        /// <param name="borrowData"></param>
        /// <returns></returns>
        public static int SaveBorrowData(HemodialysisModel.MED_BORROW_MEDICINE_DETAILDataTable borrowData)
        {
            return SaveData<HemodialysisModel.MED_BORROW_MEDICINE_DETAILDataTable>(borrowData);

        }
        /// <summary>
        /// SaveBorrowDataBack
        /// </summary>
        /// <param name="backID"></param>
        /// <param name="dateTime"></param>
        /// <param name="backUserInfo"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static int SaveBorrowDataBack(string backID, DateTime dateTime, string backUserInfo, string userID)
        {
            HemodialysisModel.MED_BORROW_MEDICINE_DETAILDataTable dataTable = new HemodialysisModel.MED_BORROW_MEDICINE_DETAILDataTable();
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("BORROW_ID", DbType.String, backID);
            dataTable = GetData<HemodialysisModel.MED_BORROW_MEDICINE_DETAILDataTable>(dataTable, "GetPatientBrowDrugListByID", dbParam);
            if (dataTable.Count == 0)
            {
                return 0;
            }
            var rowN = dataTable.NewMED_BORROW_MEDICINE_DETAILRow();
            rowN.ItemArray = dataTable[0].ItemArray;
            rowN.BORROW_ID = Guid.NewGuid().ToString();
            rowN.OLD_ID = backID;
            rowN.BORROW_USER = backUserInfo;
            rowN.OPT_USER = userID;
            rowN.BORROW_TYPE = "1";
            rowN.BORROW_DAY = dateTime;
            dataTable.AddMED_BORROW_MEDICINE_DETAILRow(rowN);

            return SaveData<HemodialysisModel.MED_BORROW_MEDICINE_DETAILDataTable>(dataTable);

        }
        /// <summary>
        /// MED_PATIENTS_ASSESSMENTDataTable
        /// </summary>
        /// <param name="pHemoID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>

        public static HemoModel.MED_PATIENTS_ASSESSMENTDataTable GetAssessmentByParams(string pHemoID, DateTime beginDate, DateTime endDate)
        {
            HemoModel.MED_PATIENTS_ASSESSMENTDataTable dataTable = new HemoModel.MED_PATIENTS_ASSESSMENTDataTable();
            DbParameter[] dbParam = new DbParameter[3];
            dbParam[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            dbParam[1] = IDatabase.BuildDbParameter("BEGINDATE", DbType.Date, beginDate);
            dbParam[2] = IDatabase.BuildDbParameter("ENDDATE", DbType.Date, endDate);
            return GetData<HemoModel.MED_PATIENTS_ASSESSMENTDataTable>(dataTable, "GetAssessmentByParams", dbParam);
        }
        /// <summary>
        /// DeleteAssessmentById
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pUser"></param>
        /// <returns></returns>
        public static int DeleteAssessmentById(string id, string pUser)
        {
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetDeleteAssessmentByParams");

            var dbParams = database.BuildDbParameter("ASSESSMENT_NOTE", DbType.String, string.Format("({0}:{1}删除)", DateTime.Now, pUser));

            var dbParams1 = database.BuildDbParameter("ASSESSMENT_ID", DbType.String, id);
            return database.ExecuteNonQuery(sql, new DbParameter[] { dbParams, dbParams1 });
        }

        /// <summary>
        /// MED_PATIENTS_ASSESSMENT_ATTRDataTable
        /// </summary>
        /// <param name="AssessmentID"></param>
        /// <returns></returns>
        public static HemoModel.MED_PATIENTS_ASSESSMENT_ATTRDataTable GetAssessmentByAssID(string AssessmentID)
        {
            HemoModel.MED_PATIENTS_ASSESSMENT_ATTRDataTable dataTable = new HemoModel.MED_PATIENTS_ASSESSMENT_ATTRDataTable();
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("ASSESSMENT_ID", DbType.String, AssessmentID);
            return GetData<HemoModel.MED_PATIENTS_ASSESSMENT_ATTRDataTable>(dataTable, "GetAssessmentByAssID", dbParam);
        }


        /// <summary>
        /// SaveAssessmentByDate
        /// </summary>
        /// <param name="dtMaster"></param>
        /// <param name="dtDetials"></param>
        /// <returns></returns>
        public static int SaveAssessmentByDate(HemoModel.MED_PATIENTS_ASSESSMENTDataTable dtMaster, HemoModel.MED_PATIENTS_ASSESSMENT_ATTRDataTable dtDetials)
        {

            var database = DatabaseFactory.Create();

            var sql = StoredScript.Get("DeleteAssessmentInfosByAssessmentId");
            var sql1 = StoredScript.Get("DeleteAssessmentArrtInfosByAssessmentId");
            string assessMent_Id = dtMaster[0].ASSESSMENT_ID;
            var idParameter = database.BuildDbParameter("ASSESSMENT_ID", DbType.String, assessMent_Id);
            var ArrtidParameter = database.BuildDbParameter("ASSESSMENT_ID", DbType.String, assessMent_Id);

            using (var trans = database.CreateDbTransaction())
            {
                try
                {
                    database.ExecuteNonQuery(sql, new DbParameter[] { idParameter }, trans);
                    database.ExecuteNonQuery(sql1, new DbParameter[] { ArrtidParameter }, trans);

                    database.Update(dtMaster, trans);
                    database.Update(dtDetials, trans);

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
        /// MED_HEMO_RECIPEDataTable
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_HEMO_RECIPEDataTable GetAssParamByHemoID(string hemoId)
        {
            HemodialysisModel.MED_HEMO_RECIPEDataTable dataTable = new HemodialysisModel.MED_HEMO_RECIPEDataTable();
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            return GetData<HemodialysisModel.MED_HEMO_RECIPEDataTable>(dataTable, "GetAssParamByHemoID", dbParam);
        }

        /// <summary>
        /// GetAllLabUnCurePatients
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllLabUnCurePatients()
        {
            DataSet ds = new DataSet();

            #region allPatientsTables
            //传染病
            PatientModel.MED_PATIENTSDataTable _infectedPatients = new PatientModel.MED_PATIENTSDataTable();
            _infectedPatients.TableName = "_infectedPatients";
            GetData<PatientModel.MED_PATIENTSDataTable>(_infectedPatients, "GetInfectedPatients", null);
            ds.Tables.Add(_infectedPatients);
            //血常规
            PatientModel.MED_PATIENTSDataTable _rouBloodPatients = new PatientModel.MED_PATIENTSDataTable();
            _rouBloodPatients.TableName = "_rouBloodPatients";
            GetData<PatientModel.MED_PATIENTSDataTable>(_rouBloodPatients, "GetRouBloodPatients", null);
            ds.Tables.Add(_rouBloodPatients);


            //肾功能Ⅰ
            PatientModel.MED_PATIENTSDataTable _renalPatients = new PatientModel.MED_PATIENTSDataTable();
            _renalPatients.TableName = "_renalPatients";
            GetData<PatientModel.MED_PATIENTSDataTable>(_renalPatients, "GetRenalPatients", null);
            ds.Tables.Add(_renalPatients);


            //电解质
            PatientModel.MED_PATIENTSDataTable _electrolytePatients = new PatientModel.MED_PATIENTSDataTable();
            _electrolytePatients.TableName = "_electrolytePatients";
            GetData<PatientModel.MED_PATIENTSDataTable>(_electrolytePatients, "GetElectrolytePatients", null);
            ds.Tables.Add(_electrolytePatients);

            //内瘘评估
            PatientModel.MED_PATIENTSDataTable _basketPatients = new PatientModel.MED_PATIENTSDataTable();
            _basketPatients.TableName = "_basketPatients";
            GetData<PatientModel.MED_PATIENTSDataTable>(_basketPatients, "GetBasketPatients", null);
            ds.Tables.Add(_basketPatients);



            //长期导管评估
            PatientModel.MED_PATIENTSDataTable _long_venousPatients = new PatientModel.MED_PATIENTSDataTable();
            _long_venousPatients.TableName = "_long_venousPatients";
            GetData<PatientModel.MED_PATIENTSDataTable>(_long_venousPatients, "GetLongVenousPatients", null);
            ds.Tables.Add(_long_venousPatients);


            //临时导管评估
            PatientModel.MED_PATIENTSDataTable _venous_catheterPatients = new PatientModel.MED_PATIENTSDataTable();
            _venous_catheterPatients.TableName = "_venous_catheterPatients";
            GetData<PatientModel.MED_PATIENTSDataTable>(_long_venousPatients, "GetVenousCatheterPatients", null);
            ds.Tables.Add(_venous_catheterPatients);

            //透析评估
            PatientModel.MED_PATIENTSDataTable _patients_assessment = new PatientModel.MED_PATIENTSDataTable();
            _patients_assessment.TableName = "_patients_assessment";
            GetData<PatientModel.MED_PATIENTSDataTable>(_long_venousPatients, "GetAssessmentPatients", null);
            ds.Tables.Add(_patients_assessment);
            #endregion



            return ds;
        }

        /// <summary>
        /// GetALLOfficeData
        /// </summary>
        /// <param name="_statOfficeData"></param>
        /// <param name="_endOfficeData"></param>
        /// <returns></returns>
        public static DataSet GetALLOfficeData(DateTime _statOfficeData, DateTime _endOfficeData)
        {
            DataSet ds = new DataSet();

            #region allPatientsTables
            //本周共排班XX人次
            DataTable dt1 = new DataTable();
            dt1.TableName = "dt1";
            DbParameter[] dbParam = new DbParameter[2];
            dbParam[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, _statOfficeData);
            dbParam[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, _endOfficeData);

            GetData<DataTable>(dt1, "GetCurrentWeekCount", dbParam);
            ds.Tables.Add(dt1);
            //已上机XX人次
            DataTable dt2 = new DataTable();
            dt2.TableName = "dt2";
            DbParameter[] dbParam1 = new DbParameter[2];
            dbParam1[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, _statOfficeData);
            dbParam1[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, _endOfficeData);
            GetData<DataTable>(dt2, "GetWeekedCount", dbParam1);
            ds.Tables.Add(dt2);
            //今天上午XX人
            DataTable dt3 = new DataTable();
            dt3.TableName = "dt3";
            GetData<DataTable>(dt3, "GetMoningCountPatients", null);
            ds.Tables.Add(dt3);
            //-现在透析XX人   ：等待:总数减去透析的人 
            DataTable dt4 = new DataTable();
            dt4.TableName = "dt4";
            GetData<DataTable>(dt4, "GetMoningHemoPatients", null);
            ds.Tables.Add(dt4);
            //今日下午XX人      
            DataTable dt5 = new DataTable();
            dt5.TableName = "dt5";
            GetData<DataTable>(dt5, "GetAfterCountPatients", null);
            ds.Tables.Add(dt5);
            //现在透析 XX人  等待:下午人减去现在透析的人就是等待的人   
            DataTable dt6 = new DataTable();
            dt6.TableName = "dt6";
            GetData<DataTable>(dt6, "GetAfterHemoPatients", null);
            ds.Tables.Add(dt6);
            //首次透析人数
            DataTable dt7 = new DataTable();
            dt7.TableName = "dt7";
            DbParameter[] dbParam6 = new DbParameter[2];
            dbParam6[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, _statOfficeData);
            dbParam6[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, _endOfficeData);
            GetData<DataTable>(dt7, "GetFirstHemoPatients", dbParam6);
            ds.Tables.Add(dt7);
            //急诊患者人数
            DataTable dt8 = new DataTable();
            dt8.TableName = "dt8";
            DbParameter[] dbParam7 = new DbParameter[2];
            dbParam7[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, _statOfficeData);
            dbParam7[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, _endOfficeData);
            GetData<DataTable>(dt8, "GetEmergeHemoPatients", dbParam7);
            ds.Tables.Add(dt8);
            //在科抢救患者的人数
            DataTable dt9 = new DataTable();
            dt9.TableName = "dt9";
            DbParameter[] dbParam8 = new DbParameter[2];
            dbParam8[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, _statOfficeData);
            dbParam8[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, _endOfficeData);
            GetData<DataTable>(dt9, "GetInHosEmergePatients", dbParam8);
            ds.Tables.Add(dt9);
            //CRRT患者
            DataTable dt10 = new DataTable();
            dt10.TableName = "dt10";
            //DbParameter[] dbParam9 = new DbParameter[2];
            //dbParam9[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, _statOfficeData);
            //dbParam9[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, _endOfficeData);
            GetData<DataTable>(dt10, "GetCRRTPatients", null);
            ds.Tables.Add(dt10);
            //血管通路手术 XX 台
            DataTable dt11 = new DataTable();
            dt11.TableName = "dt11";
            DbParameter[] dbParam10 = new DbParameter[2];
            dbParam10[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, _statOfficeData);
            dbParam10[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, _endOfficeData);
            GetData<DataTable>(dt11, "GetVasularAccessPatients", dbParam10);
            ds.Tables.Add(dt11);
            //未下诊断的 XX 人
            DataTable dt12 = new DataTable();
            dt12.TableName = "dt12";
            DbParameter[] dbParam11 = new DbParameter[2];
            dbParam11[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, _statOfficeData);
            dbParam11[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, _endOfficeData);
            GetData<DataTable>(dt12, "GetUnDilogsPatients", dbParam11);
            ds.Tables.Add(dt12);
            //透析中并发症发生 XX次  ，不良事件发生 XX 起
            DataTable dt13 = new DataTable();
            dt13.TableName = "dt13";
            DbParameter[] dbParam12 = new DbParameter[2];
            dbParam12[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, _statOfficeData);
            dbParam12[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, _endOfficeData);
            GetData<DataTable>(dt13, "GetComplactionOtherPatients", dbParam12);
            ds.Tables.Add(dt13);
            //宣教 XX  人  、  未宣教 XX   人
            DataTable dt14 = new DataTable();
            dt14.TableName = "dt14";
            DbParameter[] dbParam13 = new DbParameter[2];
            dbParam13[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, _statOfficeData);
            dbParam13[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, _endOfficeData);
            GetData<DataTable>(dt14, "GetEducationPatients", dbParam13);
            ds.Tables.Add(dt14);

            //当日的值医生和护士.D为医生N为护士
            var dt15 = new DataTable();
            dt15.TableName = "dt15";
            GetData<DataTable>(dt15, "GetCurrentDutyUser", null);
            ds.Tables.Add(dt15);
            #endregion



            return ds;
        }

        /// <summary>
        /// MED_HEMO_WORKOVERTIMEDataTable
        /// </summary>
        /// <param name="_statDate"></param>
        /// <param name="_endDate"></param>
        /// <returns></returns>
        public static HemoModel.MED_HEMO_WORKOVERTIMEDataTable GetNurseWorkOverTimeRecordByDate(DateTime _statDate, DateTime _endDate)
        {
            HemoModel.MED_HEMO_WORKOVERTIMEDataTable dataTable = new HemoModel.MED_HEMO_WORKOVERTIMEDataTable();
            DbParameter[] dbParam = new DbParameter[2];
            dbParam[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, _statDate);
            dbParam[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, _endDate);
            return GetData<HemoModel.MED_HEMO_WORKOVERTIMEDataTable>(dataTable, "GetNurseWorkOverTimeRecordByDate", dbParam);
        }

        /// <summary>
        /// SaveNurseWorkOverTime
        /// </summary>
        /// <param name="_data"></param>
        /// <returns></returns>
        public static int SaveNurseWorkOverTime(HemoModel.MED_HEMO_WORKOVERTIMEDataTable _data)
        {
            return SaveData<HemoModel.MED_HEMO_WORKOVERTIMEDataTable>(_data);

        }

        /// <summary>
        /// DeleteNurseWorkOverTimeByID
        /// </summary>
        /// <param name="_workNurseId"></param>
        /// <returns></returns>
        public static int DeleteNurseWorkOverTimeByID(string _workNurseId)
        {
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("DeleteNurseWorkOverTimeByID");

            var dbParams = database.BuildDbParameter("ID", DbType.String, _workNurseId);

            return database.ExecuteNonQuery(sql, new DbParameter[] { dbParams });
        }

        /// <summary>
        /// GetNotAssentDayByHemoId
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public static int GetNotAssentDayByHemoId(string hemoId)
        {
            DataTable Result = new DataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);

            var dt = GetData<DataTable>(Result, "GetNotAssentDayByHemoId", Params);
            if (dt != null && dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// CreatePatientRecipeBydate
        /// </summary>
        /// <param name="recipeDate"></param>
        /// <returns></returns>
        public static int CreatePatientRecipeBydate(DateTime recipeDate)
        {
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("CreatePatientRecipeBydate");
            var dbParam = database.BuildDbParameter("RECIPEDATE", DbType.DateTime, recipeDate);
            return database.ExecuteNonQuery(sql, new DbParameter[] { dbParam });
        }

        /// <summary>
        /// DeleteUnExcuteRecipeByHemoID
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int DeleteUnExcuteRecipeByHemoID(string hemoId, DateTime dt)
        {
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("DeleteUnExcuteRecipeByHemoID");
            var dbParam = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            var dbParam1 = database.BuildDbParameter("RECIPE_DATE", DbType.DateTime, dt);

            return database.ExecuteNonQuery(sql, new DbParameter[] { dbParam, dbParam1 });
        }

        /// <summary>
        /// MED_BASE_RECORDDataTable
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public static PatientModel.MED_BASE_RECORDDataTable GetBaseRecordByHemoId(string hemoId)
        {
            PatientModel.MED_BASE_RECORDDataTable dtResult = new PatientModel.MED_BASE_RECORDDataTable();
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            return GetData<PatientModel.MED_BASE_RECORDDataTable>(dtResult, "GetBaseRecordByHemoId", dbParam);
        }

        /// <summary>
        /// SaveBaseRecord
        /// </summary>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        public static int SaveBaseRecord(PatientModel.MED_BASE_RECORDDataTable dtRecord)
        {
            return SaveData<PatientModel.MED_BASE_RECORDDataTable>(dtRecord);
        }

        /// <summary>
        /// MED_BASE_RECORD_EVENTDataTable
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_BASE_RECORD_EVENTDataTable GetRecordEventByHemoId(string hemoId)
        {
            HemodialysisModel.MED_BASE_RECORD_EVENTDataTable dtResult = new HemodialysisModel.MED_BASE_RECORD_EVENTDataTable();
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            return GetData<HemodialysisModel.MED_BASE_RECORD_EVENTDataTable>(dtResult, "GetRecordEventByHemoId", dbParam);
        }

        /// <summary>
        /// SaveRecordEvent
        /// </summary>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        public static int SaveRecordEvent(HemodialysisModel.MED_BASE_RECORD_EVENTDataTable dtRecord)
        {
            return SaveData<HemodialysisModel.MED_BASE_RECORD_EVENTDataTable>(dtRecord);
        }

        /// <summary>
        /// DeleteRecordEventById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteRecordEventById(string id)
        {
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("ID", DbType.String, id);
            return DeleteData("DeleteRecordEventById", dbParam);
        }

        /// <summary>
        /// MED_BASE_RECORD_DIAGNOSEDataTable
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_BASE_RECORD_DIAGNOSEDataTable GetRecordDiagnoseByHemoId(string hemoId)
        {
            HemodialysisModel.MED_BASE_RECORD_DIAGNOSEDataTable dtResult = new HemodialysisModel.MED_BASE_RECORD_DIAGNOSEDataTable();
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            return GetData<HemodialysisModel.MED_BASE_RECORD_DIAGNOSEDataTable>(dtResult, "GetRecordDiagnoseByHemoId", dbParam);
        }

        /// <summary>
        /// MED_BASE_RECORD_DIAGNOSEDataTable
        /// </summary>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        public static int SaveRecordDiagnose(HemodialysisModel.MED_BASE_RECORD_DIAGNOSEDataTable dtRecord)
        {
            return SaveData<HemodialysisModel.MED_BASE_RECORD_DIAGNOSEDataTable>(dtRecord);
        }

        /// <summary>
        /// DeleteRecordDiagnoseById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteRecordDiagnoseById(string id)
        {
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("ID", DbType.String, id);
            return DeleteData("DeleteRecordDiagnoseById", dbParam);
        }

        /// <summary>
        /// 根据透析编号获取患者URR统计数据
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public static HemoModel.MED_PATIENTS_URRDataTable GetPatientURRByHemoId(string hemoId)
        {
            HemoModel.MED_PATIENTS_URRDataTable dtResult = new HemoModel.MED_PATIENTS_URRDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            return GetData<HemoModel.MED_PATIENTS_URRDataTable>(dtResult, "GetPatientURRByHemoId", Params);
        }

        /// <summary>
        /// 保存患者URR统计数据
        /// </summary>
        /// <param name="dtPatientURR"></param>
        /// <returns></returns>
        public static int SavePatientURR(HemoModel.MED_PATIENTS_URRDataTable dtPatientURR)
        {
            return SaveData<HemoModel.MED_PATIENTS_URRDataTable>(dtPatientURR);
        }

        /// <summary>
        /// 根据透析编号获取充分性评估数据
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable GetEstimateSufficiencyByHemoId(string hemoId, string flag)
        {
            HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable dtResult = new HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable();
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            Params[1] = IDatabase.BuildDbParameter("FLAG", DbType.String, flag);
            return GetData<HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable>(dtResult, "GetEstimateSufficiencyByHemoId", Params);
        }

        /// <summary>
        /// 根据Flag获取充分性评估数据
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable GetEstimateSufficiencyByFlag(string[] flag)
        {
            HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable dtResult = new HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable();
            IDatabase database = DatabaseFactory.Create();
            StringBuilder str = new StringBuilder();
            foreach (string s in flag)
            {
                str.Append("'" + s + "'");
                str.Append(",");
            }
            string sql = StoredScript.Get("GetEstimateSufficiencyByFlag");
            sql = string.Format(sql, str.ToString().Substring(0, str.Length - 1));
            database.Fill(sql, dtResult);
            return dtResult;
        }

        /// <summary>
        /// 根据透析编号、日期获取充分性评估数据
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="flag"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable GetEstimateSufficiencyByHemoIdAndDate(string hemoId, string flag, DateTime beginDate, DateTime endDate)
        {
            HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable dtResult = new HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable();
            DbParameter[] Params = new DbParameter[4];
            Params[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            Params[1] = IDatabase.BuildDbParameter("FLAG", DbType.String, flag);
            Params[2] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, beginDate);
            Params[3] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            return GetData<HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable>(dtResult, "GetEstimateSufficiencyByHemoIdAndDate", Params);
        }

        /// <summary>
        /// 根据Flag、日期获取充分性评估数据
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable GetEstimateSufficiencyByFlagAndDate(string[] flag, DateTime beginDate, DateTime endDate)
        {
            HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable dtResult = new HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable();
            IDatabase database = DatabaseFactory.Create();
            StringBuilder str = new StringBuilder();
            foreach (string s in flag)
            {
                str.Append("'" + s + "'");
                str.Append(",");
            }
            string sql = StoredScript.Get("GetEstimateSufficiencyByFlagAndDate");
            sql = string.Format(sql, str.ToString().Substring(0, str.Length - 1), "to_date('" + beginDate.ToShortDateString() + "'," + "'yyyy/mm/dd'" + ")", "to_date('" + endDate.ToShortDateString() + "'," + "'yyyy/mm/dd'" + ")");
            database.Fill(sql, dtResult);
            return dtResult;
        }

        /// <summary>
        /// 根据透析编号、日期获取充分性评估数据
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="flag"></param>
        /// <param name="createDate"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable GetEstimateSufficiencyByHemoIdAndDate(string hemoId, string flag, DateTime createDate)
        {
            HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable dtResult = new HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable();
            DbParameter[] Params = new DbParameter[3];
            Params[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            Params[1] = IDatabase.BuildDbParameter("FLAG", DbType.String, flag);
            Params[2] = IDatabase.BuildDbParameter("CREATEDATE", DbType.DateTime, createDate);
            return GetData<HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable>(dtResult, "GetEstimateSufficiencyByHemoIdAndCreateDate", Params);
        }

        /// <summary>
        /// 保存充分性评估数据
        /// </summary>
        /// <param name="dtSufficiency"></param>
        /// <returns></returns>
        public static int SaveEstimateSufficiency(HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable dtSufficiency)
        {
            return SaveData<HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable>(dtSufficiency);
        }

        /// <summary>
        /// 根据ID删除充分性评估记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteEstimateSufficiencyById(string id)
        {
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("ID", DbType.String, id);
            return DeleteData("DeleteEstimateSufficiencyById", dbParam);
        }

        /// <summary>
        /// 获取一段时间内的透后体重
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="dtStar">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns>数据集</returns>
        public static DataTable GetPastWeightListByParams(string pHemoID, DateTime dtStar, DateTime dtEnd)
        {
            var dataTable = new DataTable();
            DbParameter[] dbParam = new DbParameter[3];
            dbParam[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, dtStar);
            dbParam[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, dtEnd);
            dbParam[2] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);

            return GetData<DataTable>(dataTable, "GetPastWeightListByParams", dbParam);
        }

        /// <summary>
        /// 获取一段时间内的透中血压
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="dtStar">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns>数据集</returns>
        public static DataTable GetPastBloodPresureListByParams(string pHemoID, DateTime dtStar, DateTime dtEnd)
        {
            var dataTable = new DataTable();
            DbParameter[] dbParam = new DbParameter[3];
            dbParam[0] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, dtStar);
            dbParam[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, dtEnd);
            dbParam[2] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            return GetData<DataTable>(dataTable, "GetPastBloodPresureListByParams", dbParam);
        }
        public static int UpdatePatientRecipePurificationModeBydate(DateTime recipeDate)
        {
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("UpdatePatientRecipePurificationModeBydate");
            var dbParam = database.BuildDbParameter("RECIPEDATE", DbType.DateTime, recipeDate);
            return database.ExecuteNonQuery(sql, new DbParameter[] { dbParam });
        }

        /// <summary>
        /// 得到用药记录
        /// </summary>
        /// <param name="pHemoID"></param>
        /// <param name="beginCreateTime"></param>
        /// <param name="endCreateTime"></param>
        /// <returns></returns>
        public static DataTable GetDrugRecord(string pCureID, DateTime beginCreateTime, DateTime endCreateTime)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[3];

            dbParams[0] = IDatabase.BuildDbParameter("BEGIN_CREATE_DATE", DbType.Date, beginCreateTime);
            dbParams[1] = IDatabase.BuildDbParameter("END_CREATE_DATE", DbType.Date, endCreateTime);
            dbParams[2] = IDatabase.BuildDbParameter("CURE_ID", DbType.String, pCureID);

            return GetData<DataTable>(result, "GetDrugRecord", dbParams);
        }

        /// <summary>
        /// 保存患者同意书
        /// </summary>
        /// <param name="dtBookPicture"></param>
        /// <returns></returns>
        public static int SaveBookPicture(HemoModel.MED_BOOK_PICTUREDataTable dtBookPicture)
        {
            return SaveData(dtBookPicture);
        }

        /// <summary>
        /// 根据透析编号和同意书名字获取同意书签名
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="bookName"></param>
        /// <returns></returns>
        public static HemoModel.MED_BOOK_PICTUREDataTable GetBookPictureByHemoAndBookName(string hemoId, string bookName)
        {
            HemoModel.MED_BOOK_PICTUREDataTable dtResult = new HemoModel.MED_BOOK_PICTUREDataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParams[1] = IDatabase.BuildDbParameter("BOOK_NAME", DbType.String, bookName);

            return GetData<HemoModel.MED_BOOK_PICTUREDataTable>(dtResult, "GetBookPictureByHemoAndBookName", dbParams);
        }

        /// <summary>
        /// 保存透析单签字
        /// </summary>
        /// <param name="dtCureSign"></param>
        /// <returns></returns>
        public static int SaveCureSign(HemoModel.MED_CURE_SIGNDataTable dtCureSign)
        {
            return SaveData(dtCureSign);
        }

        /// <summary>
        /// 根据透析编号和治疗单ID获取透析单签字
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="cureId"></param>
        /// <returns></returns>
        public static HemoModel.MED_CURE_SIGNDataTable GetCureSignByHemoIdAndCureId(string hemoId, string cureId)
        {
            HemoModel.MED_CURE_SIGNDataTable dtResult = new HemoModel.MED_CURE_SIGNDataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParams[1] = IDatabase.BuildDbParameter("CURE_ID", DbType.String, cureId);

            return GetData<HemoModel.MED_CURE_SIGNDataTable>(dtResult, "GetCureSignByHemoIdAndCureId", dbParams);
        }

        /// <summary>
        /// 根据日期获取最近一次透析为基准，上周透析过、三个月内连续透析过患者透析编号
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetHemoIdInLastWeekAndThreeMonthsByDate(DateTime beginTime, DateTime endTime)
        {
            DataTable dtResult = new DataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[1] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            return GetData<DataTable>(dtResult, "GetHemoIdInLastWeekAndThreeMonthsByDate", dbParams);
        }

        /// <summary>
        /// 根据日期获取患者透析编号
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetHemoIdByDate(DateTime beginTime, DateTime endTime)
        {
            DataTable dtResult = new DataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[1] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            return GetData<DataTable>(dtResult, "GetHemoIdByDate", dbParams);
        }

        public static DataTable GetPatientBaseRecordProtopathyByDate(DateTime beginTime, DateTime endTime)
        {
            DataTable dtResult = new DataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[1] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            return GetData<DataTable>(dtResult, "GetPatientBaseRecordProtopathyByDate", dbParams);
        }

        /// <summary>
        /// 根据透析编号获取是否维持性透析患者
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public static string GetCureTypeByHemoId(string hemoId)
        {
            string result = "临时患者";
            DataTable dtResult = new DataTable();
            dtResult = GetData<DataTable>(dtResult, "GetHemoIdInLastWeekAndThreeMonths", null);

            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                var row = dtResult.AsEnumerable().FirstOrDefault(r => r["HEMODIALYSIS_ID"].ToString().Equals(hemoId));
                if (row != null) { result = "维持性患者"; }
            }
            return result;
        }

        /// <summary>
        /// 按月份根据透析编号和日期获取患者透析次数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetCureCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime)
        {
            DataTable dtResult = new DataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParams[1] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[2] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            return GetData<DataTable>(dtResult, "GetCureCountByHemoIdAndDate", dbParams);
        }

        public static DataTable GetComplicationOther(string cureId)
        {
            DataTable dtResult = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("CURE_ID", DbType.String, cureId);
            return GetData<DataTable>(dtResult, "GetComplicationOther", dbParams);
        }

        /// <summary>
        /// 获取患者透析年龄
        /// </summary>
        /// <param name="pName">患者姓名</param>
        /// <param name="beginDate">年龄</param>
        /// <param name="endDate">透析年龄</param>
        /// <returns></returns>
        public static DataTable GetPatientHemoAge(string pName, decimal pAGE, decimal pHEMOAGE)
        {
            DataTable dt = new DataTable();
            DbParameter[] dbParam = new DbParameter[3];
            dbParam[0] = IDatabase.BuildDbParameter("NAME", DbType.String, pName);
            dbParam[1] = IDatabase.BuildDbParameter("AGE", DbType.Decimal, pAGE);
            dbParam[2] = IDatabase.BuildDbParameter("HEMOAGE", DbType.Decimal, pHEMOAGE);
            return GetData<DataTable>(dt, "GetPatientHemoAge", dbParam);
        }

        /// <summary>
        /// QueryPatientMoreInfoList
        /// </summary>
        /// <returns></returns>
        public static DataTable QueryPatientMoreInfoList(DateTime beginDate, DateTime endDate, string beginAge, string endAge, string beginHemoAge, string endHemoAge, string pSex)
        {
            DataTable dt = new DataTable();

            IDatabase database = DatabaseFactory.Create();
            string sql = StoredScript.Get("QueryPatientMoreInfoList");
            string sqlWhere = string.Empty;


            if (beginDate != null && endDate != null)
            {
                sqlWhere += " AND (TRUNC(CURE.CURE_CREATE_DATE) >= TRUNC(to_date('" + beginDate.ToShortDateString() + "','yyyy-mm-dd'))";
                sqlWhere += " AND TRUNC(CURE.CURE_CREATE_DATE) >= TRUNC(to_date('" + endDate.ToShortDateString() + "','yyyy-mm-dd')))";
            }

            if (beginAge.Length > 0 && endAge.Length > 0)
            {
                sqlWhere += " AND (TO_CHAR(SYSDATE,'YYYY')-TO_CHAR(T.BIRTHDAY, 'YYYY')) BETWEEN " + beginAge + " AND " + endAge;
            }

            if (beginHemoAge.Length > 0 && endHemoAge.Length > 0)
            {
                sqlWhere += " AND (TO_CHAR(SYSDATE,'YYYY')-TO_CHAR(T.SPECIFIC_TIME, 'YYYY')) BETWEEN " + beginHemoAge + " AND " + endHemoAge;
            }

            if (pSex.Length > 0)
            {
                sqlWhere += " AND T.SEX='" + pSex + "'";
            }

            if (sqlWhere.Length > 0)
            {
                sql += sqlWhere;
            }
            return database.Fill(sql, dt);
        }

        /// <summary>
        /// 按月份根据透析编号和日期获取患者血管通路例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetAccessCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime)
        {
            DataTable dtResult = new DataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParams[1] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[2] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            return GetData<DataTable>(dtResult, "GetAccessCountByHemoIdAndDate", dbParams);
        }

        /// <summary>
        /// 根据透析编号和日期获取患者导管手术例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetDuctOperationCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime)
        {
            DataTable dtResult = new DataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParams[1] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[2] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            return GetData<DataTable>(dtResult, "GetDuctOperationCountByHemoIdAndDate", dbParams);
        }

        /// <summary>
        /// 按月份根据透析编号和日期获取透析患者男女例次
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetSexCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime)
        {
            DataTable dtResult = new DataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParams[1] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[2] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            return GetData<DataTable>(dtResult, "GetSexCountByHemoIdAndDate", dbParams);
        }

        /// <summary>
        /// 根据透析编号和日期获取透析患者男女例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetSexCountByHemoIdAndDate2(string hemoId, DateTime beginTime, DateTime endTime)
        {
            DataTable dtResult = new DataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParams[1] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[2] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            return GetData<DataTable>(dtResult, "GetSexCountByHemoIdAndDate2", dbParams);
        }

        /// <summary>
        /// 根据日期获取透析患者男女例次
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetSexCountByDate(DateTime beginTime, DateTime endTime)
        {
            DataTable dtResult = new DataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[1] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            return GetData<DataTable>(dtResult, "GetSexCountByDate", dbParams);
        }

        /// <summary>
        /// 根据日期获取透析患者男女例数
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetSexCountByDate2(DateTime beginTime, DateTime endTime)
        {
            DataTable dtResult = new DataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[1] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            return GetData<DataTable>(dtResult, "GetSexCountByDate2", dbParams);
        }

        /// <summary>
        /// 根据透析编号和日期获取透析患者不同年龄段例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetAgeCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime)
        {
            DataTable dtResult = new DataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParams[1] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[2] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            return GetData<DataTable>(dtResult, "GetAgeCountByHemoIdAndDate", dbParams);
        }

        /// <summary>
        /// 根据透析编号和日期获取透析患者传染病例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetInfectousCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime)
        {
            DataTable dtResult = new DataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParams[1] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[2] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            return GetData<DataTable>(dtResult, "GetInfectousCountByHemoIdAndDate", dbParams);
        }

        /// <summary>
        /// 根据透析编号和日期获取规律透析患者例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetRegularCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime)
        {
            DataTable dtResult = new DataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParams[1] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[2] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            return GetData<DataTable>(dtResult, "GetRegularCountByHemoIdAndDate", dbParams);
        }

        /// <summary>
        /// 获取每周2次或者3次的患者
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetWeekTwoOrThirdCountPatientsHemoId()
        {
            DataTable dtResult = new DataTable();
            return GetData<DataTable>(dtResult, "GetWeekTwoOrThirdCountPatientsHemoId", null);
        }

        /// <summary>
        /// 删除贫血评估或者CKDMBD评估的一条记录
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteMED_ANEMIA_CKDMBD_ASSESSbyID(string ID)
        {
            IDatabase database = DatabaseFactory.Create();
            var sql = string.Format("DELETE FROM MED_ANEMIA_CKDMBD_ASSESS WHERE ID='{0}'", ID);
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
        /// 保存贫血评估或者CKDMBD评估
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>

        public static int SaveMED_ANEMIA_CKDMBD_ASSESS(HemodialysisModel.MED_ANEMIA_CKDMBD_ASSESSDataTable dt)
        {
            return SaveData<HemodialysisModel.MED_ANEMIA_CKDMBD_ASSESSDataTable>(dt);
        }

        /// <summary>
        /// MED_ANEMIA_CKDMBD_ASSESSDataTable
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="ASSESS_TYPE"></param>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_ANEMIA_CKDMBD_ASSESSDataTable GetMED_ANEMIA_CKDMBD_ASSESSbyDate(DateTime beginTime, DateTime endTime, string ASSESS_TYPE, string hemoId)
        {


            var resultData = new HemodialysisModel.MED_ANEMIA_CKDMBD_ASSESSDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetMED_ANEMIA_CKDMBD_ASSESSbyDate");
            DbParameter[] dbParams = new DbParameter[4];
            dbParams[0] = IDatabase.BuildDbParameter("ASSESS_TYPE", DbType.String, ASSESS_TYPE);
            dbParams[1] = IDatabase.BuildDbParameter("beginTime", DbType.DateTime, beginTime);
            dbParams[2] = IDatabase.BuildDbParameter("endTime", DbType.DateTime, endTime);
            dbParams[3] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            database.Fill(sql, resultData, dbParams);
            return resultData;

        }

        /// <summary>
        /// MED_ANEMIA_CKDMBD_ASSESSDataTable
        /// </summary>
        /// <param name="ASSESS_TYPE"></param>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_ANEMIA_CKDMBD_ASSESSDataTable GetMED_ANEMIA_CKDMBD_ASSESS(string ASSESS_TYPE, string hemoId)
        {

            var resultData = new HemodialysisModel.MED_ANEMIA_CKDMBD_ASSESSDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetMED_ANEMIA_CKDMBD_ASSESS");
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("ASSESS_TYPE", DbType.String, ASSESS_TYPE);
            dbParams[1] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            database.Fill(sql, resultData, dbParams);
            return resultData;
        }

        /// <summary>
        /// 得到年度患者死亡人数
        /// </summary>
        /// <param name="pBeginTime"></param>
        /// <param name="pEndTime"></param>
        /// <returns></returns>
        public static DataTable GetDeathRate(DateTime pBeginTime, DateTime pEndTime, string HemoIDList)
        {
            DataTable resultData = new DataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetDeathRate");
            if (HemoIDList.Length > 0)
            {
                sql += HemoIDList;
            }
            sql += " AND T.LEAVE_HOSPITAL_TIME >=TO_DATE('" + pBeginTime.ToShortDateString() + "','YYYY-MM-dd') AND ";
            sql += "T.LEAVE_HOSPITAL_TIME <=TO_DATE('" + pEndTime.ToShortDateString() + "','YYYY-MM-dd')";
           // sql += " GROUP BY T.LEAVE_HOSPITAL_TIME ORDER BY DEAD_DATE";
            return database.Fill(sql, resultData);
        }

        /// <summary>
        /// 根据透析ID获取患者临时透析方案的数量
        /// </summary>
        /// <param name="pHemoID"></param>
        /// <returns></returns>
        public static int GetTempRecipeCount(string pHemoID)
        {
            int result = 0;
            var resultData = new DataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetTempRecipeCount");
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            database.Fill(sql, resultData, dbParams);
            if (resultData != null && resultData.Rows.Count > 0)
            {
                result = Utilities.Utility.CInt(resultData.Rows[0][0].ToString());
            }
            return result;
        }

        /// <summary>
        /// 根据日期获取护士绩效考核记录列表
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable GetPerformanceAppraisalByDate(DateTime beginTime, DateTime endTime)
        {
            var dtResult = new HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[1] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            return GetData<HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable>(dtResult, "GetPerformanceAppraisalByDate", dbParams);
        }

        /// <summary>
        /// 根据日期、护士组长获取护士绩效考核记录列表
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="nurseLeader"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable GetPerformanceAppraisalByDateAndNurseLeader(DateTime beginTime, DateTime endTime, string nurseLeader)
        {
            var dtResult = new HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[1] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            dbParams[2] = IDatabase.BuildDbParameter("NURSE_LEADER", DbType.String, nurseLeader);
            return GetData<HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable>(dtResult, "GetPerformanceAppraisalByDateAndNurseLeader", dbParams);
        }

        /// <summary>
        /// 根据日期、组长标识获取护士组长或组员绩效考核记录列表
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="isLeader"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable GetPerformanceAppraisalByDateAndLeaderFlag(DateTime beginTime, DateTime endTime, string isLeader)
        {
            var dtResult = new HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[1] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            dbParams[2] = IDatabase.BuildDbParameter("IS_LEADER", DbType.String, isLeader);
            return GetData<HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable>(dtResult, "GetPerformanceAppraisalByDateAndLeaderFlag", dbParams);
        }

        /// <summary>
        /// 根据日期、护士获取护士绩效考核记录列表
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="nurse"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable GetPerformanceAppraisalByDateAndNurse(DateTime beginTime, DateTime endTime, string nurse)
        {
            var dtResult = new HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[1] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            dbParams[2] = IDatabase.BuildDbParameter("CHECK_NURSE", DbType.String, nurse);
            return GetData<HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable>(dtResult, "GetPerformanceAppraisalByDateAndNurse", dbParams);
        }

        /// <summary>
        /// 根据类型获取护士绩效考核规则记录列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable GetPerformanceAppraisalRuleByType(string type)
        {
            var dtResult = new HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("ITEM_TYPE", DbType.String, type);
            return GetData<HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable>(dtResult, "GetPerformanceAppraisalRuleByType", dbParams);
        }

        /// <summary>
        /// 根据得分类型获取护士绩效考核规则记录列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable GetPerformanceAppraisalRuleByScoreType(string type)
        {
            var dtResult = new HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("SCORE_TYPE", DbType.String, type);
            return GetData<HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable>(dtResult, "GetPerformanceAppraisalRuleByScoreType", dbParams);
        }

        /// <summary>
        /// 根据ID获取护士绩效考核规则记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable GetPerformanceAppraisalRuleById(string id)
        {
            var dtResult = new HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("ID", DbType.String, id);
            return GetData<HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable>(dtResult, "GetPerformanceAppraisalRuleById", dbParams);
        }

        /// <summary>
        /// 保存护士绩效考核规则记录
        /// </summary>
        /// <param name="dtRule"></param>
        /// <returns></returns>
        public static int SavePerformanceAppraisalRule(HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable dtRule)
        {
            return SaveData<HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable>(dtRule);
        }

        /// <summary>
        /// 保存护士绩效考核记录
        /// </summary>
        /// <param name="dtAppraisal"></param>
        /// <returns></returns>
        public static int SavePerformanceAppraisal(HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable dtAppraisal)
        {
            return SaveData<HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable>(dtAppraisal);
        }

        /// <summary>
        /// 根据时间、透析次数获取透析患者
        /// </summary>
        /// <param name="time"></param>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public static DataTable GetCurePatientByTimeAndFrequency(int time, int frequency)
        {
            var dtResult = new DataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("TIME", DbType.Int32, time);
            dbParams[1] = IDatabase.BuildDbParameter("FREQUENCY", DbType.Int32, frequency);
            return GetData<DataTable>(dtResult, "GetCurePatientByTimeAndFrequency", dbParams);
        }

        /// <summary>
        /// 获取患者是否是最新三个月开始治疗的，如果是就算新入患者返回1，否则返回0。
        /// </summary>
        /// <param name="pHemoID">患者透析ID</param>
        /// <returns></returns>
        public static DataTable GetPatientTypeIsNew(string pHemoID)
        {
            DataTable dtResult = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHemoID);
            return GetData<DataTable>(dtResult, "GetPatientTypeIsNew", dbParams);
        }

        /// <summary>
        /// 保存上传数据日志
        /// </summary>
        /// <param name="dtUploadLog"></param>
        /// <returns></returns>
        public static int SaveUploadLog(HemodialysisModel.MED_UPLOAD_LOGDataTable dtUploadLog)
        {
            return SaveData<HemodialysisModel.MED_UPLOAD_LOGDataTable>(dtUploadLog);
        }

        /// <summary>
        /// 根据项名称和归属年份获取上传数据日志
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_UPLOAD_LOGDataTable GetUploadLogByItemNameAndYear(string itemName, string year)
        {
            HemodialysisModel.MED_UPLOAD_LOGDataTable dtResult = new HemodialysisModel.MED_UPLOAD_LOGDataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("UPLOAD_ITEM_NAME", DbType.String, itemName);
            dbParams[1] = IDatabase.BuildDbParameter("BELONG_YEAR", DbType.String, year);
            return GetData<HemodialysisModel.MED_UPLOAD_LOGDataTable>(dtResult, "GetUploadLogByItemNameAndYear", dbParams);
        }
        /// <summary>
        /// 根据日期获取当天工作量数据
        /// </summary>
        /// <param name="dialisysDate"></param>
        /// <returns></returns>
        public static HemoModel.MED_USER_WORKLOAD_EXTENDERDataTable GetParamUserWorkExtend(DateTime dialisysDate)
        {
            var returnDt = new HemoModel.MED_USER_WORKLOAD_EXTENDERDataTable();
            IDatabase database = DatabaseFactory.Create();
            string sql = StoredScript.Get("GetParamUserWorkExtend");

            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("DIALYSIS_DATE", DbType.DateTime, dialisysDate);

            database.Fill(sql, returnDt, dbParam);
            return returnDt;
        }
        /// <summary>
        /// 根据日期获取当天工作量数据
        /// </summary>
        /// <param name="dialisysDate"></param>
        /// <returns></returns>
        public static HemoModel.MED_USER_WORKLOAD_EXTENDERDataTable GetUserWorkExtendFromCureMain(DateTime dialisysDate)
        {
            var returnDt = new HemoModel.MED_USER_WORKLOAD_EXTENDERDataTable();
            IDatabase database = DatabaseFactory.Create();
            string sql = StoredScript.Get("GetUserWorkExtendFromCureMain");
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("DIALYSIS_DATE", DbType.DateTime, dialisysDate);

            database.Fill(sql, returnDt, dbParam);
            return returnDt;
        }
        /// <summary>
        /// 根据时间断获取工作量总数据
        /// </summary>
        /// <param name="dialisysDate"></param>
        /// <returns></returns>
        public static HemoModel.MED_USER_WORKLOAD_EXTENDERDataTable GetParamALLUserWorkExtend(DateTime dtStar, DateTime dtEnd)
        {
            var returnDt = new HemoModel.MED_USER_WORKLOAD_EXTENDERDataTable();
            IDatabase database = DatabaseFactory.Create();
            string sql = StoredScript.Get("GetParamALLUserWorkExtend");

            DbParameter[] dbParam = new DbParameter[2];
            dbParam[0] = IDatabase.BuildDbParameter("DTSTAR", DbType.DateTime, dtStar);
            dbParam[1] = IDatabase.BuildDbParameter("DTEND", DbType.DateTime, dtEnd);

            database.Fill(sql, returnDt, dbParam);
            return returnDt;
        }

        /// <summary>
        /// 根据姓名时间段获取患者术前护理评估
        /// </summary>
        /// <param name="name"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_PREOPERATIVE_NURSINGDataTable GetPreoperativeNursingByNameAndDate(string name, DateTime beginDate, DateTime endDate)
        {
            HemodialysisModel.MED_PREOPERATIVE_NURSINGDataTable dtPreoperativeNursing = new HemodialysisModel.MED_PREOPERATIVE_NURSINGDataTable();
            DbParameter[] dbParam = new DbParameter[3];
            dbParam[0] = IDatabase.BuildDbParameter("NAME", DbType.String, name);
            dbParam[1] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, beginDate);
            dbParam[2] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            return GetData<HemodialysisModel.MED_PREOPERATIVE_NURSINGDataTable>(dtPreoperativeNursing, "GetPreoperativeNursingByNameAndDate", dbParam);
        }

        /// <summary>
        /// 根据透析号时间段获取患者术前护理评估
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_PREOPERATIVE_NURSINGDataTable GetPreoperativeNursingByHemoIdAndDate(string hemoId, DateTime beginDate, DateTime endDate)
        {
            HemodialysisModel.MED_PREOPERATIVE_NURSINGDataTable dtPreoperativeNursing = new HemodialysisModel.MED_PREOPERATIVE_NURSINGDataTable();
            DbParameter[] dbParam = new DbParameter[3];
            dbParam[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParam[1] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, beginDate);
            dbParam[2] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            return GetData<HemodialysisModel.MED_PREOPERATIVE_NURSINGDataTable>(dtPreoperativeNursing, "GetPreoperativeNursingByHemoIdAndDate", dbParam);
        }

        /// <summary>
        /// 根据姓名时间段获取患者转运交接单信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_PAT_TRANSFER_HANDOVERDataTable GetPatTransferHandoverByNameAndDate(string name, DateTime beginDate, DateTime endDate)
        {
            HemodialysisModel.MED_PAT_TRANSFER_HANDOVERDataTable dtPatTransferHandover = new HemodialysisModel.MED_PAT_TRANSFER_HANDOVERDataTable();
            DbParameter[] dbParam = new DbParameter[3];
            dbParam[0] = IDatabase.BuildDbParameter("NAME", DbType.String, name);
            dbParam[1] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, beginDate);
            dbParam[2] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            return GetData<HemodialysisModel.MED_PAT_TRANSFER_HANDOVERDataTable>(dtPatTransferHandover, "GetPatTransferHandoverByNameAndDate", dbParam);
        }

        /// <summary>
        /// 根据透析号时间段获取患者转运交接单信息
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_PAT_TRANSFER_HANDOVERDataTable GetPatTransferHandoverByHemoIdAndDate(string hemoId, DateTime beginDate, DateTime endDate)
        {
            HemodialysisModel.MED_PAT_TRANSFER_HANDOVERDataTable dtPatTransferHandover = new HemodialysisModel.MED_PAT_TRANSFER_HANDOVERDataTable();
            DbParameter[] dbParam = new DbParameter[3];
            dbParam[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParam[1] = IDatabase.BuildDbParameter("BEGINDATE", DbType.DateTime, beginDate);
            dbParam[2] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            return GetData<HemodialysisModel.MED_PAT_TRANSFER_HANDOVERDataTable>(dtPatTransferHandover, "GetPatTransferHandoverByHemoIdAndDate", dbParam);
        }

        /// <summary>
        /// 根据ID删除患者术前护理评估
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeletePreoperativeNursingById(string id)
        {
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("ID", DbType.String, id);
            return DeleteData("DeletePreoperativeNursingById", dbParam);
        }

        /// <summary>
        /// 根据ID删除患者转运交接单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeletePatTransferHandoverById(string id)
        {
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("ID", DbType.String, id);
            return DeleteData("DeletePatTransferHandoverById", dbParam);
        }

        /// <summary>
        /// 保存患者转运交接
        /// </summary>
        /// <param name="dtPatTransferHandover"></param>
        /// <returns></returns>
        public static int SavePatTransferHandover(DataTable dtPatTransferHandover)
        {
            return SaveData<DataTable>(dtPatTransferHandover);
        }


        /// <summary>
        /// 获取血压例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetBloodControlsReport(string hemoId, DateTime beginTime, DateTime endTime)
        {
            DataTable dtResult = new DataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParams[1] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[2] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            return GetData<DataTable>(dtResult, "GetBloodControlsReport", dbParams);
        }
        public static ConfigModel.MED_COMMON_ITEMLISTDataTable GetCommonItemListByItemType(string itemType)
        {
            ConfigModel.MED_COMMON_ITEMLISTDataTable dtResult = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("ITEM_TYPE", DbType.String, itemType);
            return GetData<ConfigModel.MED_COMMON_ITEMLISTDataTable>(dtResult, "GetCommonItemListByItemType", dbParams);
        }


        /// <summary>
        /// 获取病人指定时间到当前时间的治疗单
        /// </summary>
        /// <param name="hemoId">透析号</param>
        /// <param name="cureDt">治疗时间</param>
        /// <returns></returns>
        public static HemodialysisModel.MED_CURE_MAINDataTable GetCureListByHemoId(string hemoId, DateTime cureDt)
        {
            var returnDt = new HemodialysisModel.MED_CURE_MAINDataTable();
            IDatabase database = DatabaseFactory.Create();
            string sql = StoredScript.Get("GetCureListByHemoId");
            DbParameter[] dbParam = new DbParameter[2];
            dbParam[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParam[1] = IDatabase.BuildDbParameter("CURE_CREATE_DATE", DbType.DateTime, cureDt);
            database.Fill(sql, returnDt, dbParam);
            return returnDt;
        }

        public static DataTable GetCureMainByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime)
        {
            DataTable dtResult = new DataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParams[1] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[2] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            return GetData<DataTable>(dtResult, "GetCureMainByHemoIdAndDate", dbParams);
        }
        public static DataTable GetPatientByHemoId(string hemoId)
        {
            DataTable dtResult = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            return GetData<DataTable>(dtResult, "GetPatientByHemoId", dbParams);
        }

        /// <summary>
        /// 根据透析编号和日期获取患者通路治疗类型例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetCureVascularTypeCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime)
        {
            DataTable dtResult = new DataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParams[1] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[2] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            return GetData<DataTable>(dtResult, "GetCureVascularTypeCountByHemoIdAndDate", dbParams);
        }
        /// <summary>
        /// 根据透析编号和日期获取传染病已肝丙肝的例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetInfectiousCountByParams(string hemoId, DateTime beginTime, DateTime endTime, string infectName, string infectCode)
        {
            DataTable dtResult = new DataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetInfectiousCountByParams");
            var _reportItemName = string.Empty;
            var _reportItemCode = string.Empty;
            if (infectName.Contains("乙肝"))
            {
                _reportItemName = "乙肝表面抗原";
                _reportItemCode = "HBsAg";
            }
            else if (infectName.Contains("丙肝"))
            {
                _reportItemName = "丙肝抗体";
                _reportItemCode = "HCV";
            }
            else if (infectName.Contains("生化全套") || infectName.Contains("肾功电解质"))
            {
                sql = StoredScript.Get("GetInfectiousCountByParamsExt");
                _reportItemName = infectName;
                _reportItemCode = "尿素";
            }
            else
            {
                _reportItemName = infectName;
                _reportItemCode = infectCode;
            }
            DbParameter[] dbParams = new DbParameter[5];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParams[1] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[2] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            dbParams[3] = IDatabase.BuildDbParameter("REPORT_ITEM_NAME", DbType.String, _reportItemName);
            dbParams[4] = IDatabase.BuildDbParameter("REPORT_ITEM_CODE", DbType.String, _reportItemCode);
            return database.Fill(sql, dtResult, dbParams);
        }


        /// <summary>
        /// 根据透析编号和日期获取患者通路类型例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetLabValueLineTypeByparams(string hemoId, string itemName, DateTime beginTime, DateTime endTime)
        {
            DataTable dtResult = new DataTable();
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetLabValueLineTypeByparams");
            DbParameter[] dbParams = new DbParameter[4];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParams[1] = IDatabase.BuildDbParameter("REPORT_ITEM_NAME", DbType.String, itemName);
            dbParams[2] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[3] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            //检验图表里的白蛋白为生化全套里的，钾、钠、磷、血钙所对应的是生化全套或是肾功能电解质透前。
            if (itemName.Contains("白蛋白"))
            {
                sql = StoredScript.Get("GetLabValueLineTypeByparamsONE");
            }
            else if (itemName.Contains("钾") || itemName.Contains("钠") || itemName.Contains("磷") || itemName.Contains("血钙"))
            {
                sql = StoredScript.Get("GetLabValueLineTypeByparamsTWO");
            }
            else if (itemName.Contains("全段甲状旁腺"))
            {
                itemName = "全段甲状旁腺";
                sql = StoredScript.Get("GetLabValueLineTypeByparamsThree");
            }
            database.Fill(sql, dtResult, dbParams);
            return dtResult;
        }


        /// <summary>
        /// 根据透析编号和日期获取患者通路类型例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetVascularTypeCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime)
        {
            DataTable dtResult = new DataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParams[1] = IDatabase.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            dbParams[2] = IDatabase.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            return GetData<DataTable>(dtResult, "GetVascularTypeCountByHemoIdAndDate", dbParams);
        }
    }
}
