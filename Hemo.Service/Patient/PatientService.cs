/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：病人基础信息数据服务层
// 创建时间：2013-03-11
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
//
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hemo.IService;
using Hemo.Business;
using Hemo.Model;
using System.Data;

namespace Hemo.Service
{
    public class PatientService : MarshalByRefObject, IPatient
    {

        /// <summary>
        /// 根据病人姓名得到病人列表
        /// </summary>
        /// <param name="pPatienName">病人姓名</param>
        /// <returns>病人列表</returns>
        public PatientModel.MED_PATIENTSDataTable GetPatientListByParams(string pPassport, string pPatienName, string pHEMODIALYSIS_ID)
        {
            return PatientBll.GetPatientListByParams(pPassport, pPatienName, pHEMODIALYSIS_ID);
        }

        /// <summary>
        /// 根据病人姓名得到病人列表
        /// </summary>
        /// <param name="pPatienName">病人姓名</param>
        /// <returns>病人列表</returns>
        public PatientModel.MED_PATIENTSDataTable GetPatientListByParams(string pPatienName, string pHEMODIALYSIS_ID)
        {
            return PatientBll.GetPatientListByParams(pPatienName, pHEMODIALYSIS_ID);
        }

        /// <summary>
        /// 得到病人列表
        /// </summary>
        /// <returns>病人列表</returns>
        public PatientModel.MED_PATIENTSDataTable GetPatientList()
        {
            return PatientBll.GetPatientList();
        }

        /// <summary>
        /// 根据病人类型，得到全部病人列表数据 
        /// </summary>
        /// <param name="pType">病人类型</param>
        /// <returns></returns>
        public PatientModel.MED_PATIENTSDataTable GetPatientListByType(string pType)
        {
            return PatientBll.GetPatientListByType(pType);
        }
        /// <summary>
        /// 根据患者去向获取信息
        /// </summary>
        /// <param name="pType"></param>
        /// <returns></returns>
        public PatientModel.MED_PATIENTSDataTable GetPatientListByWhere(string pWhere)
        {
            return PatientBll.GetPatientListByWhere(pWhere);
        }
        /// <summary>
        /// 根据日期获取新登记患者列表
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public PatientModel.MED_PATIENTSDataTable GetNewRecordPatientListByDate(DateTime startDate, DateTime endDate)
        {
            return PatientBll.GetNewRecordPatientListByDate(startDate, endDate);
        }

        /// <summary>
        /// 根据日期获取透析患者列表
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public PatientModel.MED_PATIENTSDataTable GetDialysisPatientListByDate(DateTime startDate, DateTime endDate)
        {
            return PatientBll.GetDialysisPatientListByDate(startDate, endDate);
        }

        /// <summary>
        /// 保存病人信息
        /// </summary>
        /// <param name="patientDataTable">病人信息</param>
        /// <returns></returns>
        public int SavePatientInfo(PatientModel.MED_PATIENTSDataTable patientDataTable)
        {
            return PatientBll.SavePatientInfo(patientDataTable);
        }
        /// <summary>
        /// 保存病人信息
        /// </summary>
        /// <param name="patientDataTable">病人信息</param>
        /// <returns></returns>
        public int SavePatientAndCardInfo(PatientModel.MED_PATIENTSDataTable patientDataTable, DrugModel.MED_PATIENTS_CARDDataTable patientCard)
        {
            return PatientBll.SavePatientAndCardInfo(patientDataTable, patientCard);
        }

        public int SavePatCardInfo(DrugModel.MED_PATIENTS_CARDDataTable cardInfo)
        {
            return PatientBll.SavePatCardInfo(cardInfo);
        }
        public DrugModel.MED_PATIENTS_CARDDataTable GetCardInfoByCardInfo(string serialNumber, string cardData)
        {
            return PatientBll.GetCardInfoByCardInfo(serialNumber, cardData);
        }
        public DrugModel.MED_PATIENTS_CARDDataTable GetCardInfoByInfo(string hemoId, string cardData)
        {
            return PatientBll.GetCardInfoByInfo(hemoId, cardData);
        }
        public int UpdateCardStateByParam(string state, string hemoId)
        {
            return PatientBll.UpdateCardStateByParam(state, hemoId);

        }
        /// <summary>
        /// 得到新的血透号
        /// </summary>
        /// <returns>返回新生成的血透号</returns>
        public string GetNewHemoID()
        {
            return PatientBll.GetNewHemoID();
        }

        /// <summary>
        /// 根据已排班的数据得到对应的病人数据
        /// </summary>
        /// <param name="DialysisDate">日期</param>
        /// <returns>病人数据列表</returns>
        public DataTable GetPatientListBySchedule(DateTime DialysisDate, string pAreaID, string pBanCi)
        {
            return PatientBll.GetPatientListBySchedule(DialysisDate, pAreaID, pBanCi);
        }

        /// <summary>
        /// 根据已排班的数据得到对应的病人数据
        /// </summary>
        /// <param name="name">患者姓名</param>
        /// <param name="DialysisDate">日期</param>
        /// <returns>病人数据列表</returns>
        public DataTable GetPatientListBySchedule(string name, DateTime DialysisDate, string pAreaID, string pBanCi)
        {
            return PatientBll.GetPatientListBySchedule(name, DialysisDate, pAreaID, pBanCi);
        }

        public PatientModel.MED_PATIENTSDataTable GetPatientListByPatientID(string _patientID)
        {
            return PatientBll.GetPatientListByPatientID(_patientID);
        }

        public PatientModel.MED_PATIENTSDataTable GetPatientListByInpNo(string _inpNo)
        {
            return PatientBll.GetPatientListByInpNo(_inpNo);
        }
        public PatientModel.MED_PATIENTS_PICDataTable GetPatientPicByHemoId(string hemoId)
        {
            return PatientBll.GetPatientPicByHemoId(hemoId);
        }

        /// <summary>
        /// 保存患者照片
        /// </summary>
        /// <param name="dtPic"></param>
        /// <returns></returns>
        public int SavePatientPic(PatientModel.MED_PATIENTS_PICDataTable dtPic)
        {
            return PatientBll.SavePatientPic(dtPic);
        }

        public int DeletePatientByPatient_id(string _patientID)
        {
            return PatientBll.DeletePatientByPatient_id(_patientID);
        }

        public int SaveMaterialRecordOut(MaterialScheduleModel.MED_PATIENT_MATERIALDataTable dt)
        {
            return PatientBll.SaveMaterialRecordOut(dt);

        }

        public int CancelMaterialRecordOut(MaterialScheduleModel.MED_PATIENT_MATERIALDataTable dt)
        {
            return PatientBll.CancelMaterialRecordOut(dt);

        }

        public int SavePatientRecord(PatientScheduleModel.MED_PATIENTRECORDDataTable dt)
        {
            return PatientBll.SavePatientRecord(dt);
        }

        public int SavePatientKolcaba(PatientKolcabaModel.MED_PATIENT_KOLCABADataTable dt)
        {
            return PatientBll.SavePatientKolcaba(dt);
        }

        public PatientKolcabaModel.MED_PATIENT_KOLCABADataTable GetPatientKolcabaByHemoIDandDate(string hemoId, DateTime recordData)
        {
            return PatientBll.GetPatientKolcabaByHemoIDandDate(hemoId, recordData);
        }

        public PatientKolcabaModel.MED_PATIENT_KOLCABADataTable QueryPatientKolcabaByParams(string hemoID, DateTime beginTime, DateTime endTime)
        {
            return PatientBll.QueryPatientKolcabaByParams(hemoID, beginTime, endTime);
        }

        public PatientKolcabaModel.MED_PATIENT_SUFFICIENCYDataTable QueryPatientSufficiencyByParams(string hemoID, DateTime beginTime, DateTime endTime)
        {
            return PatientBll.QueryPatientSufficiencyByParams(hemoID, beginTime, endTime);
        }

        /// <summary>
        /// 根据ID删除充分性数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeletePatientSufficiencyById(string id)
        {
            return PatientBll.DeletePatientSufficiencyById(id);
        }

        public PatientKolcabaModel.MED_PATIENT_SUFFICIENCYDataTable GetPatientSufficiencyByHemoIDandDate(string Id)
        {
            return PatientBll.GetPatientSufficiencyByHemoIDandDate(Id);
        }

        public int SavePatientSufficiency(PatientKolcabaModel.MED_PATIENT_SUFFICIENCYDataTable dt)
        {
            return PatientBll.SavePatientSufficiency(dt);
        }

        public MaterialScheduleModel.MED_PATIENT_MATERIALDataTable QueryPatientMaterialByParams(string hemoID, DateTime beginTime, DateTime endTime, string recipeId)
        {
            return PatientBll.QueryPatientMaterialByParams(hemoID, beginTime, endTime, recipeId);
        }

        public PatientScheduleModel.MED_PATIENTRECORDDataTable GetPatientRecordByHemoIDandDate(string hemoId, DateTime recordData)
        {
            return PatientBll.GetPatientRecordByHemoIDandDate(hemoId, recordData);
        }


        public PatientScheduleModel.MED_PATIENTRECORDDataTable QueryPatientRecordByParams(string hemoID, DateTime beginTime, DateTime endTime)
        {
            return PatientBll.QueryPatientRecordByParams(hemoID, beginTime, endTime);
        }

        public int DeleteMaterialRecordByID(string id)
        {
            return PatientBll.DeleteMaterialRecordByID(id);
        }

        public DataTable QueryModelByParams()
        {
            return PatientBll.QueryModelByParams();
        }
        public MaterialScheduleModel.MED_PATIENT_MATERIALDataTable QueryMaterialDetailByParams(string recipeId)
        {
            return PatientBll.QueryMaterialDetailByParams(recipeId);
        }
        public int DeleteMaterialRecordByName(string id)
        {
            return PatientBll.DeleteMaterialRecordByName(id);
        }
        public int SaveMaterialRecord(MaterialScheduleModel.MED_PATIENT_MATERIALDataTable dt)
        {
            return PatientBll.SaveMaterialRecord(dt);
        }
        public DataTable QueryMaterialModelByParams(string hemoID)
        {
            return PatientBll.QueryMaterialModelByParams(hemoID);
        }
        public int DeleteMaterialModelByName(string id)
        {
            return PatientBll.DeleteMaterialModelByName(id);
        }
        public int SaveMaterialModel(MaterialScheduleModel.MED_MATERIAL_MODELDataTable dt)
        {
            return PatientBll.SaveMaterialModel(dt);
        }
        public int SavePatientRecordMoule(PatientScheduleModel.MED_RECORDMOULDDataTable dt)
        {
            return PatientBll.SavePatientRecordMoule(dt);
        }


        public PatientScheduleModel.MED_RECORDMOULDDataTable GetRecordMouldList()
        {
            return PatientBll.GetRecordMouldList();
        }


        public DataTable GetPatientBrowDrugList(DateTime starTime, DateTime endTime, string patientName)
        {
            return PatientBll.GetPatientBrowDrugList(starTime, endTime, patientName);
        }


        public HemodialysisModel.MED_CURE_MAIN_BILLDataTable GetPatientBillByHemoIDCureID(string hemoID, DateTime cureTime)
        {
            return PatientBll.GetPatientBillByHemoIDCureID(hemoID, cureTime);
        }

        /// <summary>
        /// 保存患者账单表
        /// </summary>
        /// <param name="_patientBill"></param>
        public int SavePatientBillInfo(HemodialysisModel.MED_CURE_MAIN_BILLDataTable _patientBill)
        {
            return PatientBll.SavePatientBillInfo(_patientBill);
        }


        public DataTable GetUserBillListByStartEndDate(DateTime start, DateTime end, string defaultHemoID)
        {
            return PatientBll.GetUserBillListByStartEndDate(start, end, defaultHemoID);
        }


        public HemodialysisModel.MED_CURE_MAIN_BILLDataTable GetPatientBillByHemoID(string defaultHemoID, DateTime startDate, DateTime endDate)
        {
            return PatientBll.GetPatientBillByHemoID(defaultHemoID, startDate, endDate);

        }



        public DrugModel.MED_PATIENTS_CARDDataTable GetPatientCardDt()
        {
            return PatientBll.GetPatientCardDt();
        }

        public DrugModel.MED_PATIENTS_CARDDataTable GetPatientsDt()
        {
            return PatientBll.GetPatientsDt();

        }

        #region 交班
        public HemoModel.MED_HEMO_CHAGEWORKDataTable GetChangeWorkByDate(DateTime begionTime, DateTime endTime)
        {
            return PatientBll.GetChangeWorkByDate(begionTime, endTime);
        }

        public HemoModel.MED_HEMO_CHAGEWORKDataTable GetChangeNurseWorkByDate(DateTime begionTime, DateTime endTime)
        {
            return PatientBll.GetChangeNurseWorkByDate(begionTime, endTime);
        }
        public HemoModel.MED_HEMO_CHAGEWORK_EXTENDDataTable GetChageWorkExtendByMonth(string workMonth)
        {
            return PatientBll.GetChageWorkExtendByMonth(workMonth);
        }

        public int SaveChageWork(HemoModel.MED_HEMO_CHAGEWORKDataTable _date)
        {
            return PatientBll.SaveChageWork(_date);
        }

        public int SaveChageWorkExtend(HemoModel.MED_HEMO_CHAGEWORK_EXTENDDataTable _dateExtend)
        {
            return PatientBll.SaveChageWorkExtend(_dateExtend);
        }


        public int SaveNurseChangeWork(HemoModel.MED_HEMO_CHAGEWORKDataTable _dataMaster, HemoModel.MED_HEMO_CHAGEWORK_EXTENDDataTable _dataExtend)
        {
            return PatientBll.SaveNurseChangeWork(_dataMaster, _dataExtend);
        }

        public int DeleteChangeWorkById(string _changeId)
        {
            return PatientBll.DeleteChangeWorkById(_changeId);
        }

        public DataTable GetScheduleCoutByAreaID(string areaId, DateTime dt)
        {
            return PatientBll.GetScheduleCoutByAreaID(areaId, dt);
        }

        public DataTable GetScheduleCoutByParmars(string areaId, string banchiId, DateTime dt)
        {
            return PatientBll.GetScheduleCoutByParmars(areaId, banchiId, dt);
        }

        public HemoModel.MED_HEMO_CHAGEWORKDataTable GetChangeWorkByParm(string areaId, DateTime changTime)
        {
            return PatientBll.GetChangeWorkByParm(areaId, changTime);
        }
        #endregion

        #region 预交金


        public HemoModel.MED_PATIENT_PREPAYDataTable GetRastBillByHemoID(string HemoID)
        {
            return PatientBll.GetRastBillByHemoID(HemoID);
        }

        public HemoModel.MED_PATIENT_PREPAYDataTable GetCurrentRastByHemoID(string HemoID)
        {
            return PatientBll.GetCurrentRastByHemoID(HemoID);

        }

        public HemoModel.MED_PATIENT_PREPAYDataTable GetPatientPrePayInfos(string hemoId)
        {
            return PatientBll.GetPatientPrePayInfos(hemoId);

        }

        public HemoModel.MED_PATIENT_PREPAYDataTable GetPatientPrePayInfosByUserId(string userID)
        {
            return PatientBll.GetPatientPrePayInfosByUserId(userID);

        }

        public int SavePatientPrePayInfos(HemoModel.MED_PATIENT_PREPAYDataTable data)
        {
            return PatientBll.SavePatientPrePayInfos(data);
        }






        public int SaveHemoPatientCorDon(HemoModel.MED_HEMO_CORDONDataTable data)
        {
            return PatientBll.SaveHemoPatientCorDon(data);
        }

        public HemoModel.MED_HEMO_CORDONDataTable GetHemoPatientCorDon()
        {
            return PatientBll.GetHemoPatientCorDon();
        }

        public ConfigModel.MED_COMMON_ITEMLISTDataTable GetConfigAccountItem()
        {
            return PatientBll.GetConfigAccountItem();

        }

        public int SaveConfigAccountItem(ConfigModel.MED_COMMON_ITEMLISTDataTable data)
        {
            return PatientBll.SaveConfigAccountItem(data);

        }
        public int SaveConfigPPTItem(ConfigModel.MED_COMMON_ITEMLISTDataTable data, ConfigModel.MED_AUTO_UPDATER_ITEMSDataTable files)
        {
            return PatientBll.SaveConfigPPTItem(data, files);
        }

        public decimal GetHemoAccountCostByHemoId(string hemoId)
        {
            return PatientBll.GetHemoAccountCostByHemoId(hemoId);

        }

        public DataSet GetPatientRecipeChart(string hemoId, DateTime dtStar, DateTime dtEnd)
        {
            return PatientBll.GetPatientRecipeChart(hemoId, dtStar, dtEnd);

        }

        public DataSet GetPatientRecipeSPKTChart(string hemoId, DateTime dtStar, DateTime dtEnd)
        {
            return PatientBll.GetPatientRecipeSPKTChart(hemoId, dtStar, dtEnd);

        }

        public DataSet GetPatientRecipeURRChart(string hemoId, DateTime dtStar, DateTime dtEnd)
        {
            return PatientBll.GetPatientRecipeURRChart(hemoId, dtStar, dtEnd);

        }
        #endregion
        /// <summary>
        /// 获取风险评估信息
        /// </summary>
        /// <param name="begionTime">开始</param>
        /// <param name="endTime">结束</param>
        /// <param name="pName">患者姓名</param>
        /// <param name="nurseId">护士编号</param>
        /// <returns>数据表</returns>
        public HemoModel.MED_PATIENTS_ASSESSMENT_SCOREDataTable GetPatientAssessScoreByDate(DateTime begionTime, DateTime endTime, string pName, string nurseId)
        {
            return PatientBll.GetPatientAssessScoreByDate(begionTime, endTime, pName, nurseId);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public int DeletePatientAssessScoreById(string Id)
        {
            return PatientBll.DeletePatientAssessScoreById(Id);

        }

        /// <summary>
        /// 保存风险评估信息
        /// </summary>
        /// <param name="_date"></param>
        /// <returns>是否成功</returns>
        public int SavePatientAssessScore(HemoModel.MED_PATIENTS_ASSESSMENT_SCOREDataTable _date)
        {
            return PatientBll.SavePatientAssessScore(_date);

        }


        #region 扫描件管理


        /// <summary>
        /// 插入文书扫描数据
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="docName"></param>
        /// <param name="docImage"></param>
        public void InsertDocImage(string hemoId, string docName, byte[] docImage)
        {
            PatientBll.InsertDocImage(hemoId, docName, docImage);
        }

        public DataTable GetDocImage(string hemoId)
        {
            return PatientBll.GetDocImage(hemoId);
        }

        #endregion


        #region  病人相关

        public MaterialScheduleModel.MED_HEMO_CURE_DEFAULT_MODEDataTable GetHemoDefaultModels(string Id)
        {
            return PatientBll.GetHemoDefaultModels(Id);
        }


        public int DeleteHemoDefaultModelById(string id)
        {
            return PatientBll.DeleteHemoDefaultModelById(id);
        }


        public void SaveHemoDefaultModel(MaterialScheduleModel.MED_HEMO_CURE_DEFAULT_MODEDataTable data)
        {
            PatientBll.SaveHemoDefaultModel(data);
        }
        public MaterialScheduleModel.MED_PATIENT_MATERIALDataTable QueryMaterialOutByParams(string recipeId)
        {
            return PatientBll.QueryMaterialOutByParams(recipeId);

        }


        public DataTable QueryMaterialPatientDataByParam(DateTime starTime, DateTime endTime)
        {
            return PatientBll.QueryMaterialPatientDataByParam(starTime, endTime);
        }

        public DataTable QueryMaterialPatientDetailByparam(DateTime starTime, DateTime endTime, string materialId)
        {
            return PatientBll.QueryMaterialPatientDetailByparam(starTime, endTime, materialId);
        }

        public DataTable QueryPatientMaterialDataByParam(DateTime starTime, DateTime endTime)
        {
            return PatientBll.QueryPatientMaterialDataByParam(starTime, endTime);
        }

        public DataTable QueryPatientMaterialDetailByparam(DateTime starTime, DateTime endTime, string materialId)
        {
            return PatientBll.QueryPatientMaterialDetailByparam(starTime, endTime, materialId);

        }
        public string GetVascularAccessNameByCureId(string cureId)
        {
            return PatientBll.GetVascularAccessNameByCureId(cureId);

        }
        #endregion

        #region 死亡证明


        public int SavePatientRegDealth(PatientModel.MED_PATIENTREGDEALTHDataTable data)
        {
            return PatientBll.SavePatientRegDealth(data);
        }

        public PatientModel.MED_PATIENTREGDEALTHDataTable GetPatientRegDealthByHemoId(string hemoId)
        {
            return PatientBll.GetPatientRegDealthByHemoId(hemoId);
        }

        public PatientModel.MED_PATIENTREGDEALTHDataTable GetPatientRegDealthByData(DateTime BeginDT, DateTime EndDT)
        {
            return PatientBll.GetPatientRegDealthByData(BeginDT, EndDT);
        }

        public PatientModel.MED_PATIENTREGDEALTHDataTable GetPatientRegDealthTemplate()
        {
            return PatientBll.GetPatientRegDealthTemplate();
        }

        public int InsertTemplate(string id, string parentId, string isCategory, string caption, string type, string createby, DateTime createdate)
        {
            return PatientBll.InsertTemplate(id, parentId, isCategory, caption, type, createby, createdate);
        }

        public int UpdateTemplate(string id, string caption)
        {
            return PatientBll.UpdateTemplate(id, caption);
        }

        public int UpdateTemplate(string id, string eventanialysis, string correctiveactions)
        {
            return PatientBll.UpdateTemplate(id, eventanialysis, correctiveactions);
        }


        public int UpdateTemplate(PatientModel.MED_PATIENTREGDEALTHDataTable data)
        {
            return PatientBll.UpdateTemplate(data);
        }

        public int DeleteTemplate(string[] id)
        {
            return PatientBll.DeleteTemplate(id);
        }
        #endregion



        #region 患者手术相关

        public int SavePatientsOperator(ConfigModel.MED_PATIENTS_OPERATORDataTable _date)
        {
            return PatientBll.SavePatientsOperator(_date);

        }

        public int DeletePatientOperatorById(string Id)
        {
            return PatientBll.DeletePatientOperatorById(Id);

        }

        public ConfigModel.MED_PATIENTS_OPERATORDataTable GetPatientOperatorByDate(DateTime begionTime, DateTime endTime, string pName)
        {
            return PatientBll.GetPatientOperatorByDate(begionTime, endTime, pName);

        }
        #endregion

        #region 透析事件


        public PatientModel.MED_HEMO_EVENTINFODataTable GetHemoEventInfo(string hemoId, DateTime createdTime, string eventType)
        {
            return PatientBll.GetHemoEventInfo(hemoId, createdTime, eventType);

        }

        public int SaveHemoEventInfo(PatientModel.MED_HEMO_EVENTINFODataTable data)
        {
            return PatientBll.SaveHemoEventInfo(data);

        }

        public int DeleteHemoEventInfoById(string id)
        {
            return PatientBll.DeleteHemoEventInfoById(id);

        }

        public PatientModel.MED_HEMO_EVENTINFODataTable GetHemoEventInfoByBetweenDt(DateTime dtStar, DateTime dtEnd, string eventType)
        {
            return PatientBll.GetHemoEventInfoByBetweenDt(dtStar, dtEnd, eventType);
        }
        public PatientModel.MED_HEMO_OHTERLOGDataTable GetHemoOhterLogByDt(DateTime dt)
        {
            return PatientBll.GetHemoOhterLogByDt(dt);
        }
        public PatientModel.MED_HEMO_OHTERLOGDataTable GetHemoSingleOhterLogByDt(DateTime currentdt)
        {
            return PatientBll.GetHemoSingleOhterLogByDt(currentdt);

        }

        public int DeleteHemoOtherLogById(string id)
        {
            return PatientBll.DeleteHemoOtherLogById(id);
        }

        public int SaveHemoOtherLogInfo(PatientModel.MED_HEMO_OHTERLOGDataTable data)
        {
            return PatientBll.SaveHemoOtherLogInfo(data);
        }
        public DataTable GetHemoOtherLogCureDtByTime(DateTime currentdt)
        {
            return PatientBll.GetHemoOtherLogCureDtByTime(currentdt);
        }
        public DataTable GetCureCountByDt(DateTime dtStar, DateTime dtEnd)
        {
            return PatientBll.GetCureCountByDt(dtStar, dtEnd);
        }

        #endregion

        /// <summary>
        /// 获取临时透析编号
        /// </summary>
        /// <returns></returns>
        public DataTable GetTempHemoId()
        {
            return PatientBll.GetTempHemoId();
        }

        /// <summary>
        /// 保存临时透析编号
        /// </summary>
        /// <param name="dtHemoId"></param>
        /// <returns></returns>
        public int SaveTempHemoId(DataTable dtHemoId)
        {
            return PatientBll.SaveTempHemoId(dtHemoId);
        }


        public DataTable SaveImportedPatients(PatientModel.MED_PATIENTSDataTable patients)
        {
            return PatientBll.SaveImportedPatients(patients);
        }

        public PatientModel.MED_PATIENTS_EXTENDDataTable GetPatientExtendByParm(string filterStr)
        {
            return PatientBll.GetPatientExtendByParm(filterStr);

        }

        public PatientModel.MED_PATIENTS_EXTENDDataTable GetPatientExtendByHemoId(string HemoId)
        {
            return PatientBll.GetPatientExtendByHemoId(HemoId);
        }
        public PatientModel.MED_PATIENTSDataTable GetPatientListByHemoIds(string HemoidS)
        {
            return PatientBll.GetPatientListByHemoIds(HemoidS);
        }

    }
}
