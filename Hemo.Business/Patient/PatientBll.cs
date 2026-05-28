/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:病患管理类
 * 创建标识:刘超-2013年8月1日
 * 
 * 修改时间:2013年12月17日
 * 修改人:吕志强
 * 修改描述:新增方法GetPatientKolcabaByHemoIDandDate
 * 
 * 修改时间:2014年4月27日
 * 修改人:顾伟伟
 * 修改描述:修改方法GetPatientListByParams
 * 
 * 修改时间:2014年9月3日
 * 修改人:刘超
 * 修改描述:新增方法GetPatientList
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Hemo.Model;
using Hemo.DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;


namespace Hemo.Business
{
    public partial class PatientBll : BaseClass
    {

        /// <summary>
        /// 根据病人姓名得到病人列表
        /// </summary>
        /// <param name="pPatienName">病人姓名</param>
        /// <param name="pHEMODIALYSIS_ID">透析号</param>
        /// <returns>病人列表</returns>
        public static PatientModel.MED_PATIENTSDataTable GetPatientListByParams(string pPassport, string pPatienName, string pHEMODIALYSIS_ID)
        {
            if (string.IsNullOrEmpty(pPassport))
                pPassport = "ALL";
            PatientModel.MED_PATIENTSDataTable Result = new PatientModel.MED_PATIENTSDataTable();
            DbParameter[] Params = new DbParameter[3];
            Params[0] = IDatabase.BuildDbParameter("NAME", DbType.String, pPatienName.Trim());
            //2013-09-04 福总现场OLEDB方式添加的参数
            // Params[1] = IDatabase.BuildDbParameter("NAME_OLE", DbType.String, pPatienName.Trim());
            Params[1] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHEMODIALYSIS_ID.Trim());
            Params[2] = IDatabase.BuildDbParameter("CREDENTIALS_NUMBER", DbType.String, pPassport.Trim());
            return GetData<PatientModel.MED_PATIENTSDataTable>(Result, "GetPatientListByParams", Params);
        }

        /// <summary>
        /// 根据病人姓名得到病人列表
        /// </summary>
        /// <param name="pPatienName">病人姓名</param>
        /// <param name="pHEMODIALYSIS_ID">透析号</param>
        /// <returns>病人列表</returns>
        public static PatientModel.MED_PATIENTSDataTable GetPatientListByParams(string pPatienName, string pHEMODIALYSIS_ID)
        {
            PatientModel.MED_PATIENTSDataTable Result = new PatientModel.MED_PATIENTSDataTable();
            if (string.IsNullOrEmpty(pPatienName))
            {
                pPatienName = "&@";
            }
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("NAME", DbType.String, pPatienName.Trim());
            //2013-09-04 福总现场OLEDB方式添加的参数
            // Params[1] = IDatabase.BuildDbParameter("NAME_OLE", DbType.String, pPatienName.Trim());
            Params[1] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHEMODIALYSIS_ID.Trim());
            return GetData<PatientModel.MED_PATIENTSDataTable>(Result, "GetPatientListByParams", Params);
        }

        /// <summary>
        /// 得到全部病人列表数据 
        /// </summary>
        /// <returns></returns>
        public static PatientModel.MED_PATIENTSDataTable GetPatientList()
        {
            PatientModel.MED_PATIENTSDataTable Result = new PatientModel.MED_PATIENTSDataTable();

            GetData<PatientModel.MED_PATIENTSDataTable>(Result, "GetPatientList", null);
            Result.DefaultView.Sort = "NAME ASC";
            return Result;
        }

        /// <summary>
        /// 根据病人类型，得到全部病人列表数据 
        /// </summary>
        /// <param name="pType">病人类型</param>
        /// <returns></returns>
        public static PatientModel.MED_PATIENTSDataTable GetPatientListByType(string pType)
        {
            PatientModel.MED_PATIENTSDataTable Result = new PatientModel.MED_PATIENTSDataTable();

            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("TIME_TYPE", DbType.String, pType.Trim());
            GetData<PatientModel.MED_PATIENTSDataTable>(Result, "GetPatientListByType", Params);
            Result.DefaultView.Sort = "NAME ASC";
            return Result;
        }
        /// <summary>
        /// 根据患者去向获取信息
        /// </summary>
        /// <param name="pType">病人类型</param>
        /// <returns></returns>
        public static PatientModel.MED_PATIENTSDataTable GetPatientListByWhere(string pWhere)
        {
            PatientModel.MED_PATIENTSDataTable Result = new PatientModel.MED_PATIENTSDataTable();

            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("IS_NEW", DbType.String, pWhere.Trim());
            GetData<PatientModel.MED_PATIENTSDataTable>(Result, "GetPatientListByWhere", Params);
            return Result;
        }
        /// <summary>
        /// 根据日期获取新登记患者列表
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static PatientModel.MED_PATIENTSDataTable GetNewRecordPatientListByDate(DateTime startDate, DateTime endDate)
        {
            PatientModel.MED_PATIENTSDataTable Result = new PatientModel.MED_PATIENTSDataTable();
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("STARTDATE", DbType.DateTime, startDate);
            Params[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            return GetData<PatientModel.MED_PATIENTSDataTable>(Result, "GetNewRecordPatientListByDate", Params);
        }

        /// <summary>
        /// 根据日期获取透析患者列表
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static PatientModel.MED_PATIENTSDataTable GetDialysisPatientListByDate(DateTime startDate, DateTime endDate)
        {
            PatientModel.MED_PATIENTSDataTable Result = new PatientModel.MED_PATIENTSDataTable();
            DbParameter[] Params = new DbParameter[2];
            Params[0] = IDatabase.BuildDbParameter("STARTDATE", DbType.DateTime, startDate);
            Params[1] = IDatabase.BuildDbParameter("ENDDATE", DbType.DateTime, endDate);
            return GetData<PatientModel.MED_PATIENTSDataTable>(Result, "GetDialysisPatientListByDate", Params);
        }

        /// <summary>
        /// 得到新生成的血透号
        /// </summary>
        /// <returns></returns>
        public static string GetNewHemoID()
        {
            //DataTable dt = new DataTable();
            //IDatabase database = DatabaseFactory.Create();
            //var sql = StoredScript.Get("GetNewHemoID");
            //return database.Fill(sql, dt).Rows[0][0].ToString();

            IDatabase database = DatabaseFactory.Create();
            var result = Convert.ToInt64(DateTime.Now.ToString("yyMMdd") + "0001");
            object maxHemoDialysisID = database.ExecuteScalar("SELECT MAX(HEMODIALYSIS_ID) FROM MED_PATIENTS  WHERE LENGTH(HEMODIALYSIS_ID)=10");
            if (maxHemoDialysisID != DBNull.Value)
            {
                if (maxHemoDialysisID.ToString().Substring(0, 2).Equals(DateTime.Now.ToString("yy")))
                    result = Int64.Parse(maxHemoDialysisID.ToString()) + 1;
            }
            return result.ToString();

        }

        /// <summary>
        /// 保存病人信息数据
        /// </summary>
        /// <param name="patientDataTable">病人信息</param>
        /// <returns></returns>
        public static int SavePatientInfo(PatientModel.MED_PATIENTSDataTable patientDataTable)
        {
            return SaveData<PatientModel.MED_PATIENTSDataTable>(patientDataTable);
        }

        /// <summary>
        /// 保存制卡信息
        /// </summary>
        /// <param name="patientDataTable"></param>
        /// <param name="patientCard"></param>
        /// <returns></returns>
        public static int SavePatientAndCardInfo(PatientModel.MED_PATIENTSDataTable patientDataTable, DrugModel.MED_PATIENTS_CARDDataTable patientCard)
        {
            var database = DatabaseFactory.Create();
            var patientPicDt = new PatientModel.MED_PATIENTS_PICDataTable();
            if (patientDataTable != null && patientDataTable.Rows.Count > 0)
            {
                patientPicDt = GetPatientPicByHemoId(patientDataTable[0].HEMODIALYSIS_ID);
                if (patientPicDt.Rows.Count > 0)
                {
                    patientPicDt[0].PAT_PIC = patientDataTable[0].PAT_PIC;
                }
                else
                {
                    if (!patientDataTable[0].IsPAT_PICNull() && patientDataTable[0].PAT_PIC != null)
                    {
                        var patientPicRow = patientPicDt.NewMED_PATIENTS_PICRow();
                        patientPicRow.HEMODIALYSIS_ID = patientDataTable[0].HEMODIALYSIS_ID;
                        patientPicRow.PAT_PIC = patientDataTable[0].PAT_PIC;
                        patientPicDt.AddMED_PATIENTS_PICRow(patientPicRow);
                    }
                }
            }
            using (var trans = database.CreateDbTransaction())
            {
                try
                {
                    database.Update(patientDataTable, trans);
                    if (patientPicDt != null && patientPicDt.Rows.Count > 0)
                        database.Update(patientPicDt, trans);
                    database.Update(patientCard, trans);

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
        /// GetPatientPicByHemoId
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public static PatientModel.MED_PATIENTS_PICDataTable GetPatientPicByHemoId(string hemoId)
        {
            var dt = new PatientModel.MED_PATIENTS_PICDataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            return GetData<PatientModel.MED_PATIENTS_PICDataTable>(dt, "GetPatientPicByHemoId", dbParams);
        }

        /// <summary>
        /// 保存患者照片
        /// </summary>
        /// <param name="dtPic"></param>
        /// <returns></returns>
        public static int SavePatientPic(PatientModel.MED_PATIENTS_PICDataTable dtPic)
        {
            return SaveData<PatientModel.MED_PATIENTS_PICDataTable>(dtPic);
        }

        /// <summary>
        /// 进行出库操作
        /// </summary>
        /// <param name="materialInfo"></param>
        /// <returns></returns>
        public static int SaveMaterialRecordOut(MaterialScheduleModel.MED_PATIENT_MATERIALDataTable materialInfo)
        {
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("SaveMaterialRecordOut");

            using (var trans = database.CreateDbTransaction())
            {
                try
                {
                    foreach (MaterialScheduleModel.MED_PATIENT_MATERIALRow materialRow in materialInfo.Rows)
                    {
                        if (!materialRow.MATERTYPE.Equals("MATERIAL"))
                            continue;
                        ;
                        var sqlExcute = string.Format(sql, Guid.NewGuid().ToString(), materialRow.MATERIAL_ID, materialRow.LASTUPDATEBY, materialRow.LASTUPDATEDATE, materialRow.PRICE, "1", materialRow.MATERIAL_NUMBER, materialRow.HEMODIALYSIS_ID, "自动出库", materialRow.FRIMNAME, "1", materialRow.MATERIAL_NAME, materialRow.LASTUPDATEBY, materialRow.MATERIALSPECE, materialRow.MATERIALUNIT, materialRow.TYPE, "20150701", materialRow.LASTUPDATEBY);
                        database.ExecuteNonQuery(sqlExcute);
                    }
                    database.Update(materialInfo, trans);
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
        /// 取消出库操作
        /// </summary>
        /// <param name="materialInfo"></param>
        /// <returns></returns>
        public static int CancelMaterialRecordOut(MaterialScheduleModel.MED_PATIENT_MATERIALDataTable materialInfo)
        {
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetMaterialOutPutByRecipeId");
            DbParameter paramPatient = database.BuildDbParameter("RECIPEID", DbType.String, materialInfo[0].RECIPEID);
            var outputDt = new DrugModel.MED_MATERIAL_OUTPUTDataTable();
            database.Fill(sql, outputDt, new DbParameter[] { paramPatient });

            foreach (DrugModel.MED_MATERIAL_OUTPUTRow medMaterialOutputRow in outputDt.Rows)
            {
                medMaterialOutputRow.STATUS = "0";
            }
            using (var trans = database.CreateDbTransaction())
            {
                try
                {
                    database.Update(outputDt, trans);

                    database.Update(materialInfo, trans);
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
        /// SavePatCardInfo
        /// </summary>
        /// <param name="cardInfo"></param>
        /// <returns></returns>
        public static int SavePatCardInfo(DrugModel.MED_PATIENTS_CARDDataTable cardInfo)
        {
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("UpdateCardStateByParam");
            DbParameter paramPatient = database.BuildDbParameter("STATE", DbType.String, "2");
            DbParameter paramPatient1 = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, cardInfo[0].HEMODIALYSIS_ID);

            using (var trans = database.CreateDbTransaction())
            {
                try
                {
                    database.ExecuteNonQuery(sql, new DbParameter[] { paramPatient, paramPatient1 }, trans);
                    database.Update(cardInfo, trans);
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
        /// GetCardInfoByCardInfo
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <param name="cardData"></param>
        /// <returns></returns>
        public static DrugModel.MED_PATIENTS_CARDDataTable GetCardInfoByCardInfo(string serialNumber, string cardData)
        {
            DrugModel.MED_PATIENTS_CARDDataTable result = new DrugModel.MED_PATIENTS_CARDDataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("SERIALNUMBER", DbType.String, serialNumber);
            dbParams[1] = IDatabase.BuildDbParameter("CARDNO", DbType.String, cardData);
            return GetData<DrugModel.MED_PATIENTS_CARDDataTable>(result, "GetCardInfoByCardInfo", dbParams);
        }

        /// <summary>
        /// GetCardInfoByInfo
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="cardData"></param>
        /// <returns></returns>
        public static DrugModel.MED_PATIENTS_CARDDataTable GetCardInfoByInfo(string hemoId, string cardData)
        {
            DrugModel.MED_PATIENTS_CARDDataTable result = new DrugModel.MED_PATIENTS_CARDDataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            dbParams[1] = IDatabase.BuildDbParameter("CARDNO", DbType.String, cardData);
            return GetData<DrugModel.MED_PATIENTS_CARDDataTable>(result, "GetCardInfoByInfo", dbParams);
        }

        /// <summary>
        /// UpdateCardStateByParam
        /// </summary>
        /// <param name="state"></param>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public static int UpdateCardStateByParam(string state, string hemoId)
        {
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("UpdateCardStateByParam");
            DbParameter paramPatient = database.BuildDbParameter("STATE", DbType.String, state.Trim());
            DbParameter paramPatient1 = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId.Trim());

            return database.ExecuteNonQuery(sql, new DbParameter[] { paramPatient, paramPatient1 });
        }

        /// <summary>
        /// 根据已排班的数据得到对应的病人数据
        /// </summary>
        /// <param name="DialysisDate">时间</param>
        /// <returns>病人数据列表</returns>
        public static DataTable GetPatientListBySchedule(DateTime DialysisDate, string pAreaID, string pBanCi)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[3];
            dbParams[0] = IDatabase.BuildDbParameter("DIALYSIS_DATE", DbType.String, DialysisDate.ToString("yyyy-MM-dd"));
            dbParams[1] = IDatabase.BuildDbParameter("AREA_ID", DbType.String, pAreaID.Trim());
            dbParams[2] = IDatabase.BuildDbParameter("BANCI_ID", DbType.String, pBanCi.Trim());
            //2013-09-04 福总现场OLEDB方式添加的参数
            //dbParams[3] = IDatabase.BuildDbParameter("AREA_ID_OLE", DbType.String, pAreaID.Trim());
            //dbParams[4] = IDatabase.BuildDbParameter("BANCI_ID_OLE", DbType.String, pBanCi.Trim());
            return GetData<DataTable>(result, "GetPatientListBySchedule", dbParams);
        }

        /// <summary>
        /// 根据已排班的数据得到对应的病人数据
        /// </summary>
        /// <param name="name">患者姓名</param>
        /// <param name="DialysisDate">时间</param>
        /// <returns>病人数据列表</returns>
        public static DataTable GetPatientListBySchedule(string name, DateTime DialysisDate, string pAreaID, string pBanCi)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[4];
            dbParams[0] = IDatabase.BuildDbParameter("NAME", DbType.String, name);
            dbParams[1] = IDatabase.BuildDbParameter("DIALYSIS_DATE", DbType.String, DialysisDate.ToString("yyyy-MM-dd"));
            dbParams[2] = IDatabase.BuildDbParameter("AREA_ID", DbType.String, pAreaID.Trim());
            dbParams[3] = IDatabase.BuildDbParameter("BANCI_ID", DbType.String, pBanCi.Trim());
            return GetData<DataTable>(result, "GetPatientListByScheduleAndName", dbParams);
        }

        /// <summary>
        /// GetPatientListByPatientID
        /// </summary>
        /// <param name="_patientID"></param>
        /// <returns></returns>
        public static PatientModel.MED_PATIENTSDataTable GetPatientListByPatientID(string _patientID)
        {
            PatientModel.MED_PATIENTSDataTable result = new PatientModel.MED_PATIENTSDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetPatientInfoByPatientID");
            DbParameter patientParam = database.BuildDbParameter("PATIENT_ID", DbType.String, _patientID.Trim());
            database.Fill(sql, result, new DbParameter[] { patientParam });
            return result;

        }

        /// <summary>
        /// GetPatientListByInpNo
        /// </summary>
        /// <param name="_inpNo"></param>
        /// <returns></returns>
        public static PatientModel.MED_PATIENTSDataTable GetPatientListByInpNo(string _inpNo)
        {
            PatientModel.MED_PATIENTSDataTable result = new PatientModel.MED_PATIENTSDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetPatientInfoByInpNo");
            DbParameter patientParam = database.BuildDbParameter("ADMISSION_NUMBER", DbType.String, _inpNo.Trim());
            database.Fill(sql, result, new DbParameter[] { patientParam });
            return result;
        }

        /// <summary>
        /// DeletePatientByPatient_id
        /// </summary>
        /// <param name="_patientID"></param>
        /// <returns></returns>
        public static int DeletePatientByPatient_id(string _patientID)
        {
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("DeletePatientByPatient_id");
            DbParameter paramPatient = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, _patientID.Trim());
            return database.ExecuteNonQuery(sql, new DbParameter[] { paramPatient });
        }

        /// <summary>
        /// SavePatientRecord
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int SavePatientRecord(PatientScheduleModel.MED_PATIENTRECORDDataTable dt)
        {
            return SaveData<PatientScheduleModel.MED_PATIENTRECORDDataTable>(dt);
        }

        /// <summary>
        /// SavePatientKolcaba
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int SavePatientKolcaba(PatientKolcabaModel.MED_PATIENT_KOLCABADataTable dt)
        {
            return SaveData<PatientKolcabaModel.MED_PATIENT_KOLCABADataTable>(dt);
        }

        /// <summary>
        /// MED_PATIENT_KOLCABADataTable
        /// </summary>
        /// <param name="hemoID"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static PatientKolcabaModel.MED_PATIENT_KOLCABADataTable QueryPatientKolcabaByParams(string hemoID, DateTime beginTime, DateTime endTime)
        {
            PatientKolcabaModel.MED_PATIENT_KOLCABADataTable RESULT = new PatientKolcabaModel.MED_PATIENT_KOLCABADataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetPatientKolcabaByParams");
            DbParameter param0 = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoID.Trim());
            DbParameter param1 = database.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            DbParameter param2 = database.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            database.Fill(sql, RESULT, new DbParameter[] { param0, param1, param2 });
            return RESULT;
        }

        /// <summary>
        /// QueryPatientMaterialByParams
        /// </summary>
        /// <param name="hemoID"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        public static MaterialScheduleModel.MED_PATIENT_MATERIALDataTable QueryPatientMaterialByParams(string hemoID, DateTime beginTime, DateTime endTime, string recipeId)
        {
            MaterialScheduleModel.MED_PATIENT_MATERIALDataTable RESULT = new MaterialScheduleModel.MED_PATIENT_MATERIALDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("QueryPatientMaterialByParams");
            DbParameter param0 = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoID.Trim());
            DbParameter param1 = database.BuildDbParameter("RECIPEID", DbType.String, recipeId.Trim());
            //DbParameter param2 = database.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            //DbParameter param3 = database.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            database.Fill(sql, RESULT, new DbParameter[] { param0, param1 });
            return RESULT;
        }

        /// <summary>
        /// GetPatientKolcabaByHemoIDandDate
        /// </summary>
        /// <param name="hemoID"></param>
        /// <param name="recordDATE"></param>
        /// <returns></returns>
        public static PatientKolcabaModel.MED_PATIENT_KOLCABADataTable GetPatientKolcabaByHemoIDandDate(string hemoID, DateTime recordDATE)
        {
            PatientKolcabaModel.MED_PATIENT_KOLCABADataTable RESULT = new PatientKolcabaModel.MED_PATIENT_KOLCABADataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetPatientKolcabaByHemoIDandDate");
            DbParameter param0 = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoID.Trim());
            DbParameter param1 = database.BuildDbParameter("CREATEDATE", DbType.DateTime, recordDATE);
            database.Fill(sql, RESULT, new DbParameter[] { param0, param1 });
            return RESULT;
        }

        /// <summary>
        /// QueryPatientSufficiencyByParams
        /// </summary>
        /// <param name="hemoID"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static PatientKolcabaModel.MED_PATIENT_SUFFICIENCYDataTable QueryPatientSufficiencyByParams(string hemoID, DateTime beginTime, DateTime endTime)
        {
            PatientKolcabaModel.MED_PATIENT_SUFFICIENCYDataTable RESULT = new PatientKolcabaModel.MED_PATIENT_SUFFICIENCYDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("QueryPatientSufficiencyByParams");
            DbParameter param0 = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoID.Trim());
            DbParameter param1 = database.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            DbParameter param2 = database.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            database.Fill(sql, RESULT, new DbParameter[] { param0, param1, param2 });
            return RESULT;
        }
        /// <summary>
        /// 根据ID删除充分性评估
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeletePatientSufficiencyById(string id)
        {
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = IDatabase.BuildDbParameter("ID", DbType.String, id);
            return DeleteData("DeletePatientSufficiencyById", dbParam);
        }

        /// <summary>
        /// GetPatientSufficiencyByHemoIDandDate
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static PatientKolcabaModel.MED_PATIENT_SUFFICIENCYDataTable GetPatientSufficiencyByHemoIDandDate(string ID)
        {
            PatientKolcabaModel.MED_PATIENT_SUFFICIENCYDataTable RESULT = new PatientKolcabaModel.MED_PATIENT_SUFFICIENCYDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetPatientSufficiencyByHemoIDandDate");
            DbParameter param0 = database.BuildDbParameter("ID", DbType.String, ID.Trim());
            database.Fill(sql, RESULT, new DbParameter[] { param0 });
            return RESULT;
        }

        /// <summary>
        /// SavePatientSufficiency
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int SavePatientSufficiency(PatientKolcabaModel.MED_PATIENT_SUFFICIENCYDataTable dt)
        {
            return SaveData<PatientKolcabaModel.MED_PATIENT_SUFFICIENCYDataTable>(dt);
        }

        /// <summary>
        /// GetPatientRecordByHemoIDandDate
        /// </summary>
        /// <param name="hemoID"></param>
        /// <param name="recordDATE"></param>
        /// <returns></returns>
        public static PatientScheduleModel.MED_PATIENTRECORDDataTable GetPatientRecordByHemoIDandDate(string hemoID, DateTime recordDATE)
        {
            PatientScheduleModel.MED_PATIENTRECORDDataTable RESULT = new PatientScheduleModel.MED_PATIENTRECORDDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetPatientRecordByHemoIDandDate");
            DbParameter param0 = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoID.Trim());
            DbParameter param1 = database.BuildDbParameter("CREATEDATE", DbType.DateTime, recordDATE);
            database.Fill(sql, RESULT, new DbParameter[] { param0, param1 });
            return RESULT;
        }

        /// <summary>
        /// QueryPatientRecordByParams
        /// </summary>
        /// <param name="hemoID"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static PatientScheduleModel.MED_PATIENTRECORDDataTable QueryPatientRecordByParams(string hemoID, DateTime beginTime, DateTime endTime)
        {
            PatientScheduleModel.MED_PATIENTRECORDDataTable RESULT = new PatientScheduleModel.MED_PATIENTRECORDDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetPatientRecordByParams");
            DbParameter param0 = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoID.Trim());
            DbParameter param1 = database.BuildDbParameter("BEGINTIME", DbType.DateTime, beginTime);
            DbParameter param2 = database.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            database.Fill(sql, RESULT, new DbParameter[] { param0, param1, param2 });
            return RESULT;
        }

        /// <summary>
        /// SavePatientRecordMoule
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int SavePatientRecordMoule(PatientScheduleModel.MED_RECORDMOULDDataTable dt)
        {
            return SaveData<PatientScheduleModel.MED_RECORDMOULDDataTable>(dt);
        }


        /// <summary>
        /// QueryModelByParams
        /// </summary>
        /// <returns></returns>
        public static DataTable QueryModelByParams()
        {
            DataTable RESULT = new DataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("QueryModelByParams");
            database.Fill(sql, RESULT);
            return RESULT;
        }

        /// <summary>
        /// MED_PATIENT_MATERIALDataTable
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        public static MaterialScheduleModel.MED_PATIENT_MATERIALDataTable QueryMaterialDetailByParams(string recipeId)
        {
            var RESULT = new MaterialScheduleModel.MED_PATIENT_MATERIALDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("QueryMaterialDetailByParams");
            DbParameter param0 = database.BuildDbParameter("RECIPEID", DbType.String, recipeId.Trim());
            database.Fill(sql, RESULT, new DbParameter[] { param0 });
            return RESULT;
        }

        /// <summary>
        /// MED_PATIENT_MATERIALDataTable
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        public static MaterialScheduleModel.MED_PATIENT_MATERIALDataTable QueryMaterialOutByParams(string recipeId)
        {
            var RESULT = new MaterialScheduleModel.MED_PATIENT_MATERIALDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("QueryMaterialOutByParams");
            DbParameter param0 = database.BuildDbParameter("RECIPEID", DbType.String, recipeId.Trim());
            database.Fill(sql, RESULT, new DbParameter[] { param0 });
            return RESULT;
        }

        /// <summary>
        /// DeleteMaterialRecordByName
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteMaterialRecordByName(string id)
        {
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("DeleteMaterialRecordByName");
            DbParameter paramPatient = database.BuildDbParameter("RECORDID", DbType.String, id);
            return database.ExecuteNonQuery(sql, new DbParameter[] { paramPatient });
        }
        /// <summary>
        /// 根据科室代码,获取科室的默认病历
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static MaterialScheduleModel.MED_HEMO_CURE_DEFAULT_MODEDataTable GetHemoDefaultModels(string relationId)
        {
            var data = new MaterialScheduleModel.MED_HEMO_CURE_DEFAULT_MODEDataTable();
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetHemoDefaultModels");
            var IdParameter = database.BuildDbParameter("RELATIONID", DbType.String, relationId);
            database.Fill(sql, data, new DbParameter[] { IdParameter });
            return data;
        }

        /// <summary>
        /// DeleteHemoDefaultModelById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteHemoDefaultModelById(string id)
        {
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("DeleteHemoDefaultModelById");
            var idParameter = database.BuildDbParameter("ID", DbType.String, id);
            return database.ExecuteNonQuery(sql, new DbParameter[] { idParameter });
        }
        /// <summary>
        /// 保存科室的默认
        /// </summary>
        /// <param name="data"></param>
        public static void SaveHemoDefaultModel(MaterialScheduleModel.MED_HEMO_CURE_DEFAULT_MODEDataTable data)
        {
            if (data.Count == 0)
                return;
            var database = DatabaseFactory.Create();
            //var officeIdParameter = database.BuildDbParameter("OFFICE_ID", DbType.String, data[0].OFFICE_ID);
            //var sql = StoredScript.Get("AdministratorDA_DeleteOfficeDefaultRecordByOfficeId");
            using (var trans = database.CreateDbTransaction())
            {
                try
                {
                    //database.ExecuteNonQuery(sql, new DbParameter[] { officeIdParameter }, trans);
                    database.Update(data, trans);
                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    throw e;
                }
            }
        }

        /// <summary>
        /// QueryMaterialPatientDataByParam
        /// </summary>
        /// <param name="starTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable QueryMaterialPatientDataByParam(DateTime starTime, DateTime endTime)
        {
            var data = new DataTable();
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("QueryMaterialPatientDataByParam");
            var dtStar = database.BuildDbParameter("StartTime", DbType.DateTime, starTime);
            var dtEnd = database.BuildDbParameter("EndTime", DbType.DateTime, endTime);
            database.Fill(sql, data, new DbParameter[] { dtStar, dtEnd });
            return data;
        }

        /// <summary>
        /// QueryMaterialPatientDetailByparam
        /// </summary>
        /// <param name="starTime"></param>
        /// <param name="endTime"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public static DataTable QueryMaterialPatientDetailByparam(DateTime starTime, DateTime endTime, string materialId)
        {
            var data = new DataTable();
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("QueryMaterialPatientDetailByparam");
            var dtStar = database.BuildDbParameter("StartTime", DbType.DateTime, starTime);
            var dtEnd = database.BuildDbParameter("EndTime", DbType.DateTime, endTime);
            var material = database.BuildDbParameter("MATERIAL_ID", DbType.String, materialId);
            database.Fill(sql, data, new DbParameter[] { dtStar, dtEnd, material });
            return data;
        }

        /// <summary>
        /// QueryPatientMaterialDataByParam
        /// </summary>
        /// <param name="starTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable QueryPatientMaterialDataByParam(DateTime starTime, DateTime endTime)
        {
            var data = new DataTable();
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("QueryPatientMaterialDataByParam");
            var dtStar = database.BuildDbParameter("StartTime", DbType.DateTime, starTime);
            var dtEnd = database.BuildDbParameter("EndTime", DbType.DateTime, endTime);
            database.Fill(sql, data, new DbParameter[] { dtStar, dtEnd });
            return data;
        }

        /// <summary>
        /// QueryPatientMaterialDetailByparam
        /// </summary>
        /// <param name="starTime"></param>
        /// <param name="endTime"></param>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public static DataTable QueryPatientMaterialDetailByparam(DateTime starTime, DateTime endTime, string hemoId)
        {
            var data = new DataTable();
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("QueryPatientMaterialDetailByparam");
            var dtStar = database.BuildDbParameter("StartTime", DbType.DateTime, starTime);
            var dtEnd = database.BuildDbParameter("EndTime", DbType.DateTime, endTime);
            var material = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            database.Fill(sql, data, new DbParameter[] { dtStar, dtEnd, material });
            return data;
        }

        /// <summary>
        /// GetVascularAccessNameByCureId
        /// </summary>
        /// <param name="cureId"></param>
        /// <returns></returns>
        public static string GetVascularAccessNameByCureId(string cureId)
        {
            var data = new DataTable();
            var database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetVascularAccessNameByCureId");
            var pcureId = database.BuildDbParameter("CURE_ID", DbType.String, cureId);
            database.Fill(sql, data, new DbParameter[] { pcureId });
            string accessName = string.Empty;
            if (data.Rows.Count > 0)
            {
                accessName = data.Rows[0]["ITEM_NAME"].ToString();
            }
            return accessName;
        }

        /// <summary>
        /// SaveMaterialRecord
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int SaveMaterialRecord(MaterialScheduleModel.MED_PATIENT_MATERIALDataTable dt)
        {
            return SaveData<MaterialScheduleModel.MED_PATIENT_MATERIALDataTable>(dt);
        }

        /// <summary>
        /// QueryMaterialModelByParams
        /// </summary>
        /// <param name="hemoID"></param>
        /// <returns></returns>
        public static DataTable QueryMaterialModelByParams(string hemoID)
        {
            DataTable RESULT = new DataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("QueryMaterialModelByParams");
            DbParameter param0 = database.BuildDbParameter("MATERIAL_MODEL_NAME", DbType.String, hemoID.Trim());
            database.Fill(sql, RESULT, new DbParameter[] { param0 });
            return RESULT;
        }

        /// <summary>
        /// DeleteMaterialModelByName
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteMaterialModelByName(string id)
        {
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("DeleteMaterialModelByName");
            DbParameter paramPatient = database.BuildDbParameter("MATERIAL_MODEL_NAME", DbType.String, id);
            return database.ExecuteNonQuery(sql, new DbParameter[] { paramPatient });
        }

        /// <summary>
        /// SaveMaterialModel
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int SaveMaterialModel(MaterialScheduleModel.MED_MATERIAL_MODELDataTable dt)
        {
            return SaveData<MaterialScheduleModel.MED_MATERIAL_MODELDataTable>(dt);
        }

        /// <summary>
        /// DeleteMaterialRecordByID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteMaterialRecordByID(string id)
        {
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("DeleteMaterialRecordByID");
            DbParameter paramPatient = database.BuildDbParameter("RECIPEID", DbType.String, id);
            return database.ExecuteNonQuery(sql, new DbParameter[] { paramPatient });
        }

        /// <summary>
        /// MED_RECORDMOULDDataTable
        /// </summary>
        /// <returns></returns>
        public static PatientScheduleModel.MED_RECORDMOULDDataTable GetRecordMouldList()
        {
            PatientScheduleModel.MED_RECORDMOULDDataTable RESULT = new PatientScheduleModel.MED_RECORDMOULDDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetRecordMouldList");
            database.Fill(sql, RESULT);
            return RESULT;

        }

        /// <summary>
        /// GetPatientBrowDrugList
        /// </summary>
        /// <param name="starTime"></param>
        /// <param name="endTime"></param>
        /// <param name="patientName"></param>
        /// <returns></returns>
        public static DataTable GetPatientBrowDrugList(DateTime starTime, DateTime endTime, string patientName)
        {
            var RESULT = new System.Data.DataTable("PatientBrow");
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetPatientBrowDrugList");
            DbParameter param0 = database.BuildDbParameter("STARTTIME", DbType.String, starTime);
            DbParameter param1 = database.BuildDbParameter("ENDTIME", DbType.DateTime, endTime);
            DbParameter param2 = database.BuildDbParameter("PATNAME", DbType.DateTime, patientName);
            database.Fill(sql, RESULT, new DbParameter[] { param0, param1, param2 });
            return RESULT;
        }

        /// <summary>
        /// MED_CURE_MAIN_BILLDataTable
        /// </summary>
        /// <param name="hemoID"></param>
        /// <param name="hemoDate"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_CURE_MAIN_BILLDataTable GetPatientBillByHemoIDCureID(string hemoID, DateTime hemoDate)
        {
            IDatabase database = DatabaseFactory.Create();
            var dtResult = new HemodialysisModel.MED_CURE_MAIN_BILLDataTable();
            DbParameter param0 = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoID);
            DbParameter param1 = database.BuildDbParameter("HEMODIALYSIS_IDDATE", DbType.DateTime, hemoDate);
            database.Fill(StoredScript.Get("GetUserWeekBillByBILL_CURE_ID"), dtResult, new DbParameter[] { param0, param1 });
            return dtResult;
        }

        /// <summary>
        /// SavePatientBillInfo
        /// </summary>
        /// <param name="_patientBill"></param>
        /// <returns></returns>
        public static int SavePatientBillInfo(HemodialysisModel.MED_CURE_MAIN_BILLDataTable _patientBill)
        {
            return SaveData<HemodialysisModel.MED_CURE_MAIN_BILLDataTable>(_patientBill);
        }

        /// <summary>
        /// GetUserBillListByStartEndDate
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="defaultHemoID"></param>
        /// <returns></returns>
        public static DataTable GetUserBillListByStartEndDate(DateTime start, DateTime end, string defaultHemoID)
        {
            var RESULT = new System.Data.DataTable("PatientBrow");
            IDatabase database = DatabaseFactory.Create();
            DbParameter param0 = database.BuildDbParameter("BEGINDATE", DbType.Date, start);
            DbParameter param1 = database.BuildDbParameter("ENDDATE", DbType.Date, end);
            DbParameter param2 = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, defaultHemoID);
            var sql = StoredScript.Get("GetUserBillListByStartEndDate");
            var parms = new DbParameter[] { param0, param1 };
            if (defaultHemoID.Trim() != "")
            {
                sql = StoredScript.Get("GetUserBillListByStartEndDateHomeID");
                parms = new DbParameter[] { param0, param1, param2 };
            }
            database.Fill(sql, RESULT, parms);
            return RESULT;
        }

        /// <summary>
        /// GetPatientBillByHemoID
        /// </summary>
        /// <param name="defaultHemoID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static HemodialysisModel.MED_CURE_MAIN_BILLDataTable GetPatientBillByHemoID(string defaultHemoID, DateTime startDate, DateTime endDate)
        {
            var RESULT = new HemodialysisModel.MED_CURE_MAIN_BILLDataTable();
            IDatabase database = DatabaseFactory.Create();
            DbParameter param0 = database.BuildDbParameter("BEGINDATE", DbType.Date, startDate);
            DbParameter param1 = database.BuildDbParameter("ENDDATE", DbType.Date, endDate);
            DbParameter param2 = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, defaultHemoID);

            var sql = StoredScript.Get("GetPatientBillByHemoIDListInfo");

            var parms = new DbParameter[] { param0, param1, param2 };
            database.Fill(sql, RESULT, parms);
            return RESULT;
        }

        /// <summary>
        /// MED_PATIENTS_CARDDataTable
        /// </summary>
        /// <returns></returns>
        public static DrugModel.MED_PATIENTS_CARDDataTable GetPatientCardDt()
        {
            var RESULT = new DrugModel.MED_PATIENTS_CARDDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetPatientCardDt");
            database.Fill(sql, RESULT);
            return RESULT;
        }

        /// <summary>
        /// MED_PATIENTS_CARDDataTable
        /// </summary>
        /// <returns></returns>
        public static DrugModel.MED_PATIENTS_CARDDataTable GetPatientsDt()
        {
            var RESULT = new DrugModel.MED_PATIENTS_CARDDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetPatientsDt");
            database.Fill(sql, RESULT);
            return RESULT;
        }


        #region 患者风险评估
        /// <summary>
        /// 获取风险评估信息
        /// </summary>
        /// <param name="begionTime">开始</param>
        /// <param name="endTime">结束</param>
        /// <param name="pName">患者姓名</param>
        /// <param name="nurseId">护士编号</param>
        /// <returns>数据表</returns>
        public static HemoModel.MED_PATIENTS_ASSESSMENT_SCOREDataTable GetPatientAssessScoreByDate(DateTime begionTime, DateTime endTime, string pName, string nurseId)
        {
            var RESULT = new HemoModel.MED_PATIENTS_ASSESSMENT_SCOREDataTable();
            IDatabase database = DatabaseFactory.Create();
            DbParameter param0 = database.BuildDbParameter("BEGINDATE", DbType.DateTime, begionTime);
            DbParameter param1 = database.BuildDbParameter("ENDDATE", DbType.DateTime, endTime);
            DbParameter param2 = database.BuildDbParameter("NAME", DbType.String, pName);
            DbParameter param3 = database.BuildDbParameter("NURSEID", DbType.String, nurseId);
            var sql = StoredScript.Get("GetPatientAssessScoreByDate");
            var parms = new DbParameter[] { param0, param1, param2, param3 };
            database.Fill(sql, RESULT, parms);
            return RESULT;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public static int DeletePatientAssessScoreById(string Id)
        {
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("DeletePatientAssessScoreById");
            DbParameter paramID = database.BuildDbParameter("ID", DbType.String, Id.Trim());
            return database.ExecuteNonQuery(sql, new DbParameter[] { paramID });
        }

        /// <summary>
        /// 保存风险评估信息
        /// </summary>
        /// <param name="_date"></param>
        /// <returns>是否成功</returns>
        public static int SavePatientAssessScore(HemoModel.MED_PATIENTS_ASSESSMENT_SCOREDataTable _date)
        {
            return SaveData<HemoModel.MED_PATIENTS_ASSESSMENT_SCOREDataTable>(_date);
        }
        #endregion
        #region 预交金
        public static HemoModel.MED_PATIENT_PREPAYDataTable GetRastBillByHemoID(string HemoID)
        {
            var RESULT = new HemoModel.MED_PATIENT_PREPAYDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetRastBillByHemoID");
            DbParameter param = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, HemoID);
            var parms = new DbParameter[] { param };
            database.Fill(sql, RESULT, parms);
            return RESULT;
        }

        public static HemoModel.MED_PATIENT_PREPAYDataTable GetCurrentRastByHemoID(string HemoID)
        {
            var RESULT = new HemoModel.MED_PATIENT_PREPAYDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetCurrentRastByHemoID");
            DbParameter param = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, HemoID);
            var parms = new DbParameter[] { param };
            database.Fill(sql, RESULT, parms);
            return RESULT;
        }
        public static HemoModel.MED_PATIENT_PREPAYDataTable GetPatientPrePayInfos(string hemoId)
        {
            var RESULT = new HemoModel.MED_PATIENT_PREPAYDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetPatientPrePayInfos");
            DbParameter param = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            var parms = new DbParameter[] { param };
            database.Fill(sql, RESULT, parms);
            return RESULT;
        }
        public static HemoModel.MED_PATIENT_PREPAYDataTable GetPatientPrePayInfosByUserId(string userID)
        {
            var RESULT = new HemoModel.MED_PATIENT_PREPAYDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetPatientPrePayInfosByUserId");
            DbParameter param = database.BuildDbParameter("CREATEBY", DbType.String, userID);
            var parms = new DbParameter[] { param };
            database.Fill(sql, RESULT, parms);
            return RESULT;
        }
        public static int SavePatientPrePayInfos(HemoModel.MED_PATIENT_PREPAYDataTable data)
        {
            return SaveData<HemoModel.MED_PATIENT_PREPAYDataTable>(data);
        }
        public static int SaveHemoPatientCorDon(HemoModel.MED_HEMO_CORDONDataTable data)
        {
            var result = new HemoModel.MED_HEMO_CORDONDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetHavingHemoPatientCorDon");
            database.Fill(sql, result);

            foreach (HemoModel.MED_HEMO_CORDONRow rowNum in data.Rows)
            {
                var row = result.FirstOrDefault(i => i.HEMODIALYSIS_ID == rowNum.HEMODIALYSIS_ID);
                if (row == null)
                {
                    result.LoadDataRow(rowNum.ItemArray, false);
                }
                else
                {
                    row.CORDON = rowNum.CORDON;
                    row.CREATEBY = rowNum.CREATEBY;
                    row.CREATEDATE = rowNum.CREATEDATE;
                }
            }

            return SaveData<HemoModel.MED_HEMO_CORDONDataTable>(result);
        }

        public static HemoModel.MED_HEMO_CORDONDataTable GetHemoPatientCorDon()
        {
            var RESULT = new HemoModel.MED_HEMO_CORDONDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetHemoPatientCorDon");
            database.Fill(sql, RESULT);
            return RESULT;
        }
        public static ConfigModel.MED_COMMON_ITEMLISTDataTable GetConfigAccountItem()
        {
            var RESULT = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetConfigAccountItem");
            database.Fill(sql, RESULT);
            return RESULT;
        }

        public static int SaveConfigAccountItem(ConfigModel.MED_COMMON_ITEMLISTDataTable data)
        {
            ConfigModel.MED_COMMON_ITEMLISTDataTable result = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetFeeItemConfigList");
            DbParameter param = database.BuildDbParameter("ITEM_TYPE", DbType.String, "费用");
            database.Fill(sql, result, new DbParameter[] { param });

            foreach (ConfigModel.MED_COMMON_ITEMLISTRow row in data.Rows)
            {
                var feeRow = result.FirstOrDefault(i => i.ITEM_VALUE == row.ITEM_ID);
                if (feeRow != null)
                {
                    feeRow.ITEM_NAME = row.PRICE;

                }
                else
                {
                    var newFeeRow = result.NewMED_COMMON_ITEMLISTRow();
                    newFeeRow.ITEM_ID = Guid.NewGuid().ToString();
                    newFeeRow.ITEM_VALUE = row.ITEM_ID;
                    newFeeRow.ITEM_NAME = row.PRICE;
                    newFeeRow.ITEM_TYPE = "费用";
                    newFeeRow.STATUS = "1";
                    newFeeRow.ORDER_NUMBER = row.ORDER_NUMBER;
                    result.AddMED_COMMON_ITEMLISTRow(newFeeRow);
                }
            }

            foreach (ConfigModel.MED_COMMON_ITEMLISTRow row in result.Rows)
            {
                data.ImportRow(row);
            }

            return SaveData<ConfigModel.MED_COMMON_ITEMLISTDataTable>(data);

        }

        public static int SaveConfigPPTItem(ConfigModel.MED_COMMON_ITEMLISTDataTable data, ConfigModel.MED_AUTO_UPDATER_ITEMSDataTable files)
        {
            var database = DatabaseFactory.Create();
            var sql = "DELETE FROM MED_AUTO_UPDATER_ITEMS WHERE APP_ID='ScreenShowTv'";
            using (var trans = database.CreateDbTransaction())
            {
                try
                {

                    database.Update(data, trans);
                    database.ExecuteNonQuery(sql);
                    database.Update(files, trans);

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

        public static decimal GetHemoAccountCostByHemoId(string hemoId)
        {
            decimal DecRestCost = 0;
            DataTable result = new DataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetHemoAccountCostByHemoId");
            DbParameter param = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            database.Fill(sql, result, new DbParameter[] { param });
            if (result != null && result.Rows.Count > 0)
            {
                DecRestCost = decimal.Parse(result.Rows[0][0].ToString());
            }
            return DecRestCost;
        }

        public static DataSet GetPatientRecipeChart(string hemoId, DateTime dtStar, DateTime dtEnd)
        {
            DataSet ds = new DataSet();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetPatientRecipeWeigthChart");
            var sql1 = StoredScript.Get("GetPatientRecipeCureChart");
            DbParameter param = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            DbParameter param1 = database.BuildDbParameter("BEGINDATE", DbType.DateTime, dtStar);
            DbParameter param2 = database.BuildDbParameter("ENDDATE", DbType.DateTime, dtEnd);

            DbParameter pparam = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            DbParameter pparam1 = database.BuildDbParameter("BEGINDATE", DbType.DateTime, dtStar);
            DbParameter pparam2 = database.BuildDbParameter("ENDDATE", DbType.DateTime, dtEnd);
            database.Fill(sql, dt1, new DbParameter[] { param, param1, param2 });
            database.Fill(sql1, dt2, new DbParameter[] { pparam, pparam1, pparam2 });
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);
            return ds;
        }

        public static DataSet GetPatientRecipeSPKTChart(string hemoId, DateTime dtStar, DateTime dtEnd)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetPatientRecipeSPKTChart");

            DbParameter param = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            DbParameter param1 = database.BuildDbParameter("BEGINDATE", DbType.DateTime, dtStar);
            DbParameter param2 = database.BuildDbParameter("ENDDATE", DbType.DateTime, dtEnd);
            
            database.Fill(sql, dt, new DbParameter[] { param, param1, param2 });            
            ds.Tables.Add(dt);
            
            return ds;
        }

        public static DataSet GetPatientRecipeURRChart(string hemoId, DateTime dtStar, DateTime dtEnd)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetPatientRecipeURRChart");

            DbParameter param = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            DbParameter param1 = database.BuildDbParameter("BEGINDATE", DbType.DateTime, dtStar);
            DbParameter param2 = database.BuildDbParameter("ENDDATE", DbType.DateTime, dtEnd);

            database.Fill(sql, dt, new DbParameter[] { param, param1, param2 });
            ds.Tables.Add(dt);

            return ds;
        }
        #endregion
        #region 交班
        /// <summary>
        /// 根据时间去获取交本内容
        /// </summary>
        /// <param name="dtChangeTime"></param>
        /// <returns></returns>
        public static HemoModel.MED_HEMO_CHAGEWORKDataTable GetChangeWorkByDate(DateTime begionTime, DateTime endTime)
        {
            var RESULT = new HemoModel.MED_HEMO_CHAGEWORKDataTable();
            IDatabase database = DatabaseFactory.Create();
            DbParameter param0 = database.BuildDbParameter("BEGINDATE", DbType.DateTime, begionTime);
            DbParameter param1 = database.BuildDbParameter("ENDDATE", DbType.DateTime, endTime);
            var sql = StoredScript.Get("GetChangeWorkByDate");
            var parms = new DbParameter[] { param0, param1 };
            database.Fill(sql, RESULT, parms);
            return RESULT;
        }

        /// <summary>
        /// 根据时间去获取交本内容
        /// </summary>
        /// <param name="dtChangeTime"></param>
        /// <returns></returns>
        public static HemoModel.MED_HEMO_CHAGEWORKDataTable GetChangeNurseWorkByDate(DateTime begionTime, DateTime endTime)
        {
            var RESULT = new HemoModel.MED_HEMO_CHAGEWORKDataTable();
            IDatabase database = DatabaseFactory.Create();
            DbParameter param0 = database.BuildDbParameter("BEGINDATE", DbType.DateTime, begionTime);
            DbParameter param1 = database.BuildDbParameter("ENDDATE", DbType.DateTime, endTime);
            var sql = StoredScript.Get("GetChangeNurseWorkByDate");
            var parms = new DbParameter[] { param0, param1 };
            database.Fill(sql, RESULT, parms);
            return RESULT;
        }
        /// <summary>
        /// 根据月份获取透析总人数
        /// </summary>
        /// <param name="workMonth"></param>
        /// <returns></returns>
        public static HemoModel.MED_HEMO_CHAGEWORK_EXTENDDataTable GetChageWorkExtendByMonth(string workMonth)
        {
            var RESULT = new HemoModel.MED_HEMO_CHAGEWORK_EXTENDDataTable();
            IDatabase database = DatabaseFactory.Create();
            DateTime currentDt = Convert.ToDateTime(string.Format("{0}/01", workMonth));
            var begionTime = currentDt.AddDays(1 - currentDt.Day);
            var endTime = currentDt.AddDays(1 - currentDt.Day).AddMonths(1).AddDays(-1);
            DbParameter param0 = database.BuildDbParameter("BEGINDATE", DbType.DateTime, begionTime);
            DbParameter param1 = database.BuildDbParameter("ENDDATE", DbType.DateTime, endTime);
            var sql = StoredScript.Get("GetChageWorkExtendByMonth");
            var parms = new DbParameter[] { param0, param1 };
            database.Fill(sql, RESULT, parms);
            return RESULT;
        }
        /// <summary>
        /// 保存交班记录
        /// </summary>
        /// <param name="_date"></param>
        /// <returns></returns>
        public static int SaveChageWork(HemoModel.MED_HEMO_CHAGEWORKDataTable _date)
        {
            return SaveData<HemoModel.MED_HEMO_CHAGEWORKDataTable>(_date);
        }
        /// <summary>
        /// 保存交班记录扩展
        /// </summary>
        /// <param name="_dateExtend"></param>
        /// <returns></returns>
        public static int SaveChageWorkExtend(HemoModel.MED_HEMO_CHAGEWORK_EXTENDDataTable _dateExtend)
        {
            return SaveData<HemoModel.MED_HEMO_CHAGEWORK_EXTENDDataTable>(_dateExtend);
        }
        public static int SaveNurseChangeWork(HemoModel.MED_HEMO_CHAGEWORKDataTable _dataMaster, HemoModel.MED_HEMO_CHAGEWORK_EXTENDDataTable _dataExtend)
        {

            var database = DatabaseFactory.Create();
            using (var trans = database.CreateDbTransaction())
            {
                try
                {

                    database.Update(_dataMaster, trans);
                    database.Update(_dataExtend, trans);
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
        public static int DeleteChangeWorkById(string _changeId)
        {
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("DeleteChangeWorkById");
            DbParameter paramID = database.BuildDbParameter("ID", DbType.String, _changeId.Trim());
            return database.ExecuteNonQuery(sql, new DbParameter[] { paramID });
        }
        public static DataTable GetScheduleCoutByAreaID(string areaId, DateTime dt)
        {
            var RESULT = new DataTable();
            IDatabase database = DatabaseFactory.Create();
            DbParameter param1 = database.BuildDbParameter("AREAID", DbType.String, areaId);
            DbParameter param2 = database.BuildDbParameter("DIALYSIS_DATE", DbType.DateTime, dt);

            var sql = StoredScript.Get("GetScheduleCoutByAreaID");
            var parms = new DbParameter[] { param1, param2 };
            database.Fill(sql, RESULT, parms);
            return RESULT;
        }
        public static DataTable GetScheduleCoutByParmars(string areaId, string banchiId, DateTime dt)
        {
            var RESULT = new DataTable();
            IDatabase database = DatabaseFactory.Create();
            DbParameter param1 = database.BuildDbParameter("AREAID", DbType.String, areaId);
            DbParameter param2 = database.BuildDbParameter("BANCI_ID", DbType.String, banchiId);
            DbParameter param3 = database.BuildDbParameter("DIALYSIS_DATE", DbType.DateTime, dt);

            var sql = StoredScript.Get("GetScheduleCoutByParmars");
            var parms = new DbParameter[] { param1, param2, param3 };
            database.Fill(sql, RESULT, parms);
            return RESULT;
        }
        public static HemoModel.MED_HEMO_CHAGEWORKDataTable GetChangeWorkByParm(string areaId, DateTime changTime)
        {
            var RESULT = new HemoModel.MED_HEMO_CHAGEWORKDataTable();
            IDatabase database = DatabaseFactory.Create();
            DbParameter param1 = database.BuildDbParameter("AREAID", DbType.String, areaId);
            DbParameter param2 = database.BuildDbParameter("CHANGETIME", DbType.DateTime, changTime);

            var sql = StoredScript.Get("GetChangeWorkByParm");
            var parms = new DbParameter[] { param1, param2 };
            database.Fill(sql, RESULT, parms);
            return RESULT;
        }
        #endregion


        #region 扫描件管理

        /// <summary>
        /// 插入文书扫描数据
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="docName"></param>
        /// <param name="docImage"></param>
        public static void InsertDocImage(string hemoId, string docName, byte[] docImage)
        {
            IDatabase database = DatabaseFactory.Create();
            string sql = string.Format("INSERT INTO MED_DOC_IMAGE(ID,HEMO_ID,DOC_NAME,DOC_IMAGE,UPLOAD_DATE) VALUES('{0}','{1}','{2}',:DOC_IMAGE,:UPLOAD_DATE)",
                                                     Guid.NewGuid().ToString(), hemoId, docName);
            DbParameter parameter1 = database.BuildDbParameter("DOC_IMAGE", DbType.Byte, docImage);
            DbParameter parameter2 = database.BuildDbParameter("UPLOAD_DATE", DbType.DateTime, DateTime.Now);

            DbParameter[] parms = new DbParameter[] { parameter1, parameter2 };

            database.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// 获取病人的文书扫描数据
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public static DataTable GetDocImage(string hemoId)
        {
            IDatabase database = DatabaseFactory.Create();
            string sql = string.Format("SELECT DOC_NAME,DOC_IMAGE FROM MED_DOC_IMAGE WHERE HEMO_ID='{0}' ORDER BY UPLOAD_DATE", hemoId);
            DataTable data = new DataTable("MED_DOC_IMAGE");
            database.Fill(sql, data);
            return data;
        }

        #endregion


        #region 死亡证明

        /// <summary>
        /// 保存死亡信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int SavePatientRegDealth(PatientModel.MED_PATIENTREGDEALTHDataTable data)
        {
            return SaveData<PatientModel.MED_PATIENTREGDEALTHDataTable>(data);
        }
        /// <summary>
        /// 获取死亡信息
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public static PatientModel.MED_PATIENTREGDEALTHDataTable GetPatientRegDealthByHemoId(string hemoId)
        {
            var data = new PatientModel.MED_PATIENTREGDEALTHDataTable();
            string sql = string.Format(@"SELECT * FROM MED_PATIENTREGDEALTH T WHERE T.HEMODIALYSIS_ID='{0}'", hemoId);
            IDatabase database = DatabaseFactory.Create();
            database.Fill(sql, data);
            return data;
        }
        /// <summary>
        /// 获取死亡信息
        /// </summary>
        /// <param name="BeginDT"></param>
        /// <param name="EndDT"></param>
        /// <returns></returns>
        public static PatientModel.MED_PATIENTREGDEALTHDataTable GetPatientRegDealthByData(DateTime BeginDT, DateTime EndDT)
        {
            var data = new PatientModel.MED_PATIENTREGDEALTHDataTable();
            IDatabase database = DatabaseFactory.Create();

            string sql = string.Format(@"SELECT T.*,
                                               S.NAME,
                                               S.SEX,
                                               (SELECT K.USER_NAME FROM MED_USERS K WHERE K.USER_ID = T.CREATEBY) CREATENAME
                                          FROM MED_PATIENTREGDEALTH T, MED_PATIENTS S
                                         WHERE T.HEMODIALYSIS_ID = S.HEMODIALYSIS_ID
                                                AND T.TYPE='0'   AND TRUNC(T.CREATEDATE) >= TRUNC(:DTSTAR)
                                           AND TRUNC(T.CREATEDATE) <= TRUNC(:DTEND)", BeginDT, EndDT);

            DbParameter param1 = database.BuildDbParameter("DTSTAR", DbType.Date, BeginDT);
            DbParameter param2 = database.BuildDbParameter("DTEND", DbType.Date, EndDT);
            var parms = new DbParameter[] { param1, param2 };
            database.Fill(sql, data, parms);
            return data;
        }
        /// <summary>
        /// 获取模板
        /// </summary>
        /// <returns></returns>
        public static PatientModel.MED_PATIENTREGDEALTHDataTable GetPatientRegDealthTemplate()
        {
            var data = new PatientModel.MED_PATIENTREGDEALTHDataTable();
            string sql = string.Format(@"SELECT * FROM MED_PATIENTREGDEALTH T WHERE T.TYPE='1'");
            IDatabase database = DatabaseFactory.Create();
            database.Fill(sql, data);
            return data;
        }
        /// <summary>
        /// 插入模板
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentId"></param>
        /// <param name="isCategory"></param>
        /// <param name="caption"></param>
        /// <param name="type"></param>
        /// <param name="createby"></param>
        /// <param name="createdate"></param>
        /// <returns></returns>
        public static int InsertTemplate(string id, string parentId, string isCategory, string caption, string type, string createby, DateTime createdate)
        {
            int result = 0;
            string sql = string.Format(@"
                                    INSERT INTO MED_PATIENTREGDEALTH
                                      (ID, HEMODIALYSIS_ID, TYPE, EXTEND1, EXTEND2, CREATEDATE, CREATEBY)
                                    VALUES
                                      ('{0}', '{1}', '{2}', '{3}', '{4}',  TO_DATE('{5}','YYYY-MM-DD HH24:MI:SS'), '{6}')", id, parentId, type, isCategory, caption, createdate, createby);
            IDatabase database = DatabaseFactory.Create();
            result = database.ExecuteNonQuery(sql);
            return result;
        }
        /// <summary>
        /// 更新模板
        /// </summary>
        /// <param name="id"></param>
        /// <param name="caption"></param>
        /// <returns></returns>
        public static int UpdateTemplate(string id, string caption)
        {
            int result = 0;
            string sql = string.Format(@"UPDATE MED_PATIENTREGDEALTH
                                       SET EXTEND2 = '{0}'
                                     WHERE ID = '{1}'", caption, id);
            IDatabase database = DatabaseFactory.Create();
            result = database.ExecuteNonQuery(sql);
            return result;
        }
        /// <summary>
        /// 更新模板
        /// </summary>
        /// <param name="id"></param>
        /// <param name="eventanialysis"></param>
        /// <param name="correctiveactions"></param>
        /// <returns></returns>
        public static int UpdateTemplate(string id, string eventanialysis, string correctiveactions)
        {
            int result = 0;
            string sql = string.Format(@"UPDATE MED_PATIENTREGDEALTH
                                       SET EVENTANALYSIS     = '{0}',
                                           CORRECTIVEACTIONS = '{1}'
                                     WHERE ID = '{2}'", eventanialysis, correctiveactions, id);
            IDatabase database = DatabaseFactory.Create();
            result = database.ExecuteNonQuery(sql);
            return result;
        }

        /// <summary>
        /// 更新模板
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int UpdateTemplate(PatientModel.MED_PATIENTREGDEALTHDataTable data)
        {
            return SaveData<PatientModel.MED_PATIENTREGDEALTHDataTable>(data);
        }
        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteTemplate(string[] id)
        {
            int result = 0;
            string sql = string.Format(@"DELETE MED_PATIENTREGDEALTH
                                    WHERE ID = :ID OR  HEMODIALYSIS_ID = :HEMODIALYSIS_ID");
            IDatabase database = DatabaseFactory.Create();
            using (var trans = database.CreateDbTransaction())
            {
                try
                {
                    foreach (var i in id)
                    {
                        var idParameter1 = database.BuildDbParameter("ID", DbType.String, i);
                        var idParameter2 = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, i);
                        result = database.ExecuteNonQuery(sql, new DbParameter[] { idParameter1, idParameter2 }, trans);
                    }
                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();

                    throw e;
                }
            }
            return result;
        }
        #endregion

        #region 患者手术相关
        public static ConfigModel.MED_PATIENTS_OPERATORDataTable GetPatientOperatorByDate(DateTime dtStar, DateTime dtEnd, string pName)
        {
            var result = new ConfigModel.MED_PATIENTS_OPERATORDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("GetPatientOperatorByDate");
            DbParameter param = database.BuildDbParameter("OPE_STAR", DbType.DateTime, dtStar);
            DbParameter param1 = database.BuildDbParameter("OPE_END", DbType.DateTime, dtEnd);
            if (!string.IsNullOrEmpty(pName))
                sql = string.Format("{0} AND T.NAME LIKE '%{1}%'", sql, pName);
            database.Fill(sql, result, new DbParameter[] { param, param1 });
            return result;
        }
        public static int SavePatientsOperator(ConfigModel.MED_PATIENTS_OPERATORDataTable _date)
        {
            return SaveData<ConfigModel.MED_PATIENTS_OPERATORDataTable>(_date);
        }

        public static int DeletePatientOperatorById(string Id)
        {
            IDatabase database = DatabaseFactory.Create();
            var sql = StoredScript.Get("DeletePatientOperatorById");
            DbParameter paramID = database.BuildDbParameter("ID", DbType.String, Id.Trim());
            return database.ExecuteNonQuery(sql, new DbParameter[] { paramID });
        }

        #endregion

        #region 透析事件

        public static PatientModel.MED_HEMO_EVENTINFODataTable GetHemoEventInfo(string hemoId, DateTime createdTime, string eventType)
        {
            var dtResult = new PatientModel.MED_HEMO_EVENTINFODataTable();
            IDatabase database = DatabaseFactory.Create();

            DbParameter param1 = database.BuildDbParameter("CREATERTIME", DbType.DateTime, createdTime);
            DbParameter param2 = database.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            DbParameter param3 = database.BuildDbParameter("EVENTTYPE", DbType.String, eventType);


            var sql = StoredScript.Get("GetHemoEventInfo");
            database.Fill(sql, dtResult, new DbParameter[] { param1, param2, param3 });
            return dtResult;

        }

        public static int SaveHemoEventInfo(PatientModel.MED_HEMO_EVENTINFODataTable data)
        {
            return SaveData<PatientModel.MED_HEMO_EVENTINFODataTable>(data);

        }
        public static int DeleteHemoEventInfoById(string id)
        {
            IDatabase database = DatabaseFactory.Create();

            DbParameter param1 = database.BuildDbParameter("ID", DbType.String, id);

            var sql = StoredScript.Get("DeleteHemoEventInfoById");
            return database.ExecuteNonQuery(sql, new DbParameter[] { param1 });
        }
        public static PatientModel.MED_HEMO_EVENTINFODataTable GetHemoEventInfoByBetweenDt(DateTime dtStar, DateTime dtEnd, string eventType)
        {
            var dtResult = new PatientModel.MED_HEMO_EVENTINFODataTable();
            IDatabase database = DatabaseFactory.Create();

            DbParameter param1 = database.BuildDbParameter("DTSTAR", DbType.DateTime, dtStar);
            DbParameter param2 = database.BuildDbParameter("DTEND", DbType.DateTime, dtEnd);
            DbParameter param3 = database.BuildDbParameter("EVENTTYPE", DbType.String, eventType);

            var sql = StoredScript.Get("GetHemoEventInfoByBetweenDt");
            database.Fill(sql, dtResult, new DbParameter[] { param1, param2, param3 });
            return dtResult;
        }
        public static PatientModel.MED_HEMO_OHTERLOGDataTable GetHemoOhterLogByDt(DateTime dt)
        {
            var dtResult = new PatientModel.MED_HEMO_OHTERLOGDataTable();
            IDatabase database = DatabaseFactory.Create();
            DbParameter param1 = database.BuildDbParameter("DTMONTH", DbType.DateTime, dt);
            var sql = StoredScript.Get("GetHemoOhterLogByDt");
            database.Fill(sql, dtResult, new DbParameter[] { param1 });
            return dtResult;
        }
        public static PatientModel.MED_HEMO_OHTERLOGDataTable GetHemoSingleOhterLogByDt(DateTime dt)
        {
            var dtResult = new PatientModel.MED_HEMO_OHTERLOGDataTable();
            IDatabase database = DatabaseFactory.Create();
            DbParameter param1 = database.BuildDbParameter("DTDAY", DbType.DateTime, dt);
            var sql = StoredScript.Get("GetHemoSingleOhterLogByDt");
            database.Fill(sql, dtResult, new DbParameter[] { param1 });
            return dtResult;
        }
        public static int DeleteHemoOtherLogById(string id)
        {
            IDatabase database = DatabaseFactory.Create();
            DbParameter param1 = database.BuildDbParameter("ID", DbType.String, id);
            var sql = StoredScript.Get("DeleteHemoOtherLogById");
            return database.ExecuteNonQuery(sql, new DbParameter[] { param1 });
        }

        public static int SaveHemoOtherLogInfo(PatientModel.MED_HEMO_OHTERLOGDataTable data)
        {
            return SaveData<PatientModel.MED_HEMO_OHTERLOGDataTable>(data);
        }

        public static DataTable GetHemoOtherLogCureDtByTime(DateTime currentdt)
        {
            var dtResult = new DataTable();

            IDatabase database = DatabaseFactory.Create();
            DbParameter param1 = database.BuildDbParameter("CURE_DATE", DbType.Date, currentdt);
            var sql = StoredScript.Get("GetHemoOtherLogCureDtByTime");
            return database.Fill(sql, dtResult, new DbParameter[] { param1 });
        }
        public static DataTable GetCureCountByDt(DateTime dtStar, DateTime dtEnd)
        {
            var dtResult = new DataTable();
            IDatabase database = DatabaseFactory.Create();
            DbParameter param1 = database.BuildDbParameter("DTSTAR", DbType.DateTime, dtStar);
            DbParameter param2 = database.BuildDbParameter("DTEND", DbType.DateTime, dtEnd);

            var sql = StoredScript.Get("GetCureCountByDt");
            return database.Fill(sql, dtResult, new DbParameter[] { param1, param2 });
        }

        #endregion

        /// <summary>
        /// 获取临时透析编号
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTempHemoId()
        {
            DataTable dtResult = new DataTable();
            return GetData<DataTable>(dtResult, "GetTempHemoId", null);
        }

        /// <summary>
        /// 保存临时透析编号
        /// </summary>
        /// <param name="dtHemoId"></param>
        /// <returns></returns>
        public static int SaveTempHemoId(DataTable dtHemoId)
        {
            return SaveData<DataTable>(dtHemoId);
        }

        public static DataTable SaveImportedPatients(PatientModel.MED_PATIENTSDataTable patients)
        {
            #region <<返回结果>>

            DataTable dtResult = new DataTable();
            DataColumn dcSuccessCount = new DataColumn("SuccessCount", typeof(string));
            DataColumn dcExistCount = new DataColumn("ExistCount", typeof(string));
            DataColumn dcExistDetail = new DataColumn("ExistDetail", typeof(string));
            dtResult.Columns.Add(dcSuccessCount);
            dtResult.Columns.Add(dcExistCount);
            dtResult.Columns.Add(dcExistDetail);
            DataRow drResult = dtResult.NewRow();
            dtResult.Rows.Add(drResult);

            int existCount = 0;
            StringBuilder existPatients = new StringBuilder();

            #endregion

            try
            {

                IDatabase database = DatabaseFactory.Create();
                PatientModel.MED_PATIENTSDataTable dt = new PatientModel.MED_PATIENTSDataTable();

                var maxHemoDialysisID = GenerateHemoDialysisID(database);

                foreach (DataRow drTemp in patients.Rows)
                {
                    #region <<筛选重复患者>>

                    object patientCount = database.ExecuteScalar("SELECT COUNT(*) FROM MED_PATIENTS WHERE NAME=:NAME AND CREDENTIALS_NUMBER=:CREDENTIALS_NUMBER",
                              new DbParameter[2] { database.BuildDbParameter("NAME", DbType.String, drTemp["NAME"]),
                                  database.BuildDbParameter("CREDENTIALS_NUMBER", DbType.String, drTemp["CREDENTIALS_NUMBER"]) });
                    int result = int.Parse(patientCount.ToString());
                    if (result > 0)
                    {
                        existCount++;
                        existPatients.Append("姓名：" + drTemp["NAME"] + " 身份证号：" + drTemp["CREDENTIALS_NUMBER"] + "\r\n");
                    }

                    #endregion

                    #region <<生成标准行>>

                    else
                    {
                        PatientModel.MED_PATIENTSRow row = dt.NewMED_PATIENTSRow();

                        //透析号
                        row.HEMODIALYSIS_ID = maxHemoDialysisID.ToString();
                        row.NAME = drTemp["Name"].ToString();
                        row.SEX = drTemp["SEX"].ToString();
                        //患者来源
                        string patientType = ConvertDataForPatientType(drTemp["TIME_TYPE"].ToString());
                        row.TIME_TYPE = patientType;
                        //住院号/门诊号
                        switch (patientType)
                        {
                            case "0":
                                row.PATIENT_ID = drTemp["PATIENT_ID"].ToString();
                                row.TIME_TYPE = "门诊";
                                break;
                            case "1":
                                row.ADMISSION_NUMBER = drTemp["PATIENT_ID"].ToString();
                                row.TIME_TYPE = "住院";
                                break;
                        }
                        row.PATIENT_ID = drTemp["PATIENT_ID"].ToString();
                        row.CREDENTIALS_TYPE = "身份证";
                        row.CREDENTIALS_NUMBER = drTemp["CREDENTIALS_NUMBER"].ToString();
                        row.MEDICAL_TYPE = drTemp["MEDICAL_TYPE"].ToString();//存医保号
                        //年龄
                        row.SetAGENull();
                        if (!string.IsNullOrEmpty(drTemp["AGE"].ToString()))
                        {
                            row.AGE = int.Parse(drTemp["AGE"].ToString());
                        }
                        row.INFECTIOUS_CHECK_RESULT = drTemp["INFECTIOUS_CHECK_RESULT"].ToString();
                        row.DIAGNOSE = drTemp["DIAGNOSE"].ToString();
                        //透析日期
                        row.SetSPECIFIC_TIMENull();
                        if (!string.IsNullOrEmpty(drTemp["SPECIFIC_TIME"].ToString()))
                        {
                            row.SPECIFIC_TIME = DateTime.Parse(drTemp["SPECIFIC_TIME"].ToString());
                        }
                        //生日
                        row.SetBIRTHDAYNull();
                        if (!string.IsNullOrEmpty(drTemp["BIRTHDAY"].ToString()))
                        {
                            row.BIRTHDAY = DateTime.Parse(drTemp["BIRTHDAY"].ToString());
                        }
                        row.NATIVEPLACE = drTemp["NATIVEPLACE"].ToString();
                        row.MARITAL = drTemp["MARITAL"].ToString();
                        row.EDUCATION = drTemp["EDUCATION"].ToString();
                        row.NATION = drTemp["NATION"].ToString();
                        row.INPUT_CODE = drTemp["INPUT_CODE"].ToString();
                        row.TELEPHONE = drTemp["TELEPHONE"].ToString();
                        row.ADDRESS = drTemp["ADDRESS"].ToString();
                        row.WORK_TELEPHONE = drTemp["TELEPHONE"].ToString();
                        row.JOB = drTemp["JOB"].ToString();
                        row.IS_NEW = "0";
                        //医院ID

                        row.CREATE_DATE = DateTime.Now;
                        dt.Rows.Add(row);

                        maxHemoDialysisID++;
                    }

                    #endregion
                }

                drResult["ExistDetail"] = existPatients;
                drResult["ExistCount"] = existCount;

                using (var trans = database.CreateDbTransaction())
                {
                    //导入患者
                    int successCount = database.Update(dt, trans);
                    drResult["SuccessCount"] = successCount;
                    trans.Commit();
                }
            }
            catch (Exception e)
            {
                //drResult["ExistDetail"] = e.Message;
                drResult["ExistDetail"] = "来自数据库的导入失败，请联系系统管理员！";
                drResult["ExistCount"] = 0;
                drResult["SuccessCount"] = "-1";
            }
            return dtResult;
        }
        private static string ConvertDataForPatientType(string patientType)
        {
            //0=门诊；1=住院
            string result;
            switch (patientType)
            {
                case "门诊":
                    result = "0";
                    break;
                case "住院":
                    result = "1";
                    break;
                default:
                    result = "0";
                    break;
            }
            return result;
        }
        /// <summary>
        /// 生成透析号
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        private static long GenerateHemoDialysisID(IDatabase database)
        {
            var result = Convert.ToInt64(DateTime.Now.ToString("yyMMdd") + "0001");
            object maxHemoDialysisID = database.ExecuteScalar("SELECT MAX(HEMODIALYSIS_ID) FROM MED_PATIENTS WHERE LENGTH(HEMODIALYSIS_ID)=10");
            if (maxHemoDialysisID != DBNull.Value)
            {
                if (maxHemoDialysisID.ToString().Substring(0, 2).Equals(DateTime.Now.ToString("yy")))
                    result = Convert.ToInt64(maxHemoDialysisID.ToString()) + 1;
            }
            return result;
        }



        /// <summary>
        /// 根据患者去向获取信息
        /// </summary>
        /// <param name="pType">病人类型</param>
        /// <returns></returns>
        public static PatientModel.MED_PATIENTS_EXTENDDataTable GetPatientExtendByParm(string filterStr)
        {
            PatientModel.MED_PATIENTS_EXTENDDataTable Result = new PatientModel.MED_PATIENTS_EXTENDDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("PATIENTS", DbType.String, filterStr.Trim());
            GetData<PatientModel.MED_PATIENTS_EXTENDDataTable>(Result, "GetPatientExtendByParm", Params);

            foreach (var row in Result)
            {
                row.HEMOAGE = GetHemoAgeByHemoID(row.HEMODIALYSIS_ID, Utilities.Utility.CDate(row.SPECIFIC_TIME));
            }
            Result.AcceptChanges();
            return Result;
        }
        public static string GetHemoAgeByHemoID(string hemoId, DateTime firstCureDt)
        {
            var result = string.Empty;
            var dt = new System.Data.DataTable();
            IDatabase database = DatabaseFactory.Create();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemoId);
            var sql = StoredScript.Get("GetHemoAgeByHemoID");
            database.Fill(sql, dt, Params);
            //因为医院要求根据 当时间时间算，所以逻辑先保留以后根据 治疗单算可以改，如果根据治疗单算可以去掉ELSE
            if (dt.Rows.Count > 0)
            {
                result = Utilities.Utility.GetPatientHemoAge(firstCureDt, DateTime.Now);//Utilities.Utility.CDate(dt.Rows[0]["HEMOAGE"].ToString())
            }
            else
            {
                //result="未透析";
                result = Utilities.Utility.GetPatientHemoAge(firstCureDt, DateTime.Now);

            }
            return result;
        }

        public static PatientModel.MED_PATIENTS_EXTENDDataTable GetPatientExtendByHemoId(string hemoId)
        {
            var RESULT = new PatientModel.MED_PATIENTS_EXTENDDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = string.Format("SELECT T.*,DECODE(T.DIRECTION,'0','入科', '1', '死亡', '2', '转其它透析室', '3', '转腹透', '4', '肾移值', '5', '放弃治疗','6', '暂不需要治疗',T.DIRECTION) DIRECTIONNAME FROM MED_PATIENTS_EXTEND T WHERE T.HEMODIALYSIS_ID={0}", hemoId);
            database.Fill(sql, RESULT);
            return RESULT;
        }

        public static PatientModel.MED_PATIENTSDataTable GetPatientListByHemoIds(string HemoidS)
        {
            PatientModel.MED_PATIENTSDataTable RESULT = new PatientModel.MED_PATIENTSDataTable();
            IDatabase database = DatabaseFactory.Create();
            var sql = "SELECT * FROM MED_PATIENTS {0}";

            if (string.IsNullOrEmpty(HemoidS))
            {
                sql = string.Format(sql, string.Empty);
            }
            else
            {
                sql = string.Format(sql, string.Format(" WHERE HEMODIALYSIS_ID IN ('{0}')", HemoidS));
            }

            sql = sql + "    ORDER BY HEMODIALYSIS_ID DESC ,NAME";
            database.Fill(sql, RESULT);
            return RESULT;
        }

    }
}

