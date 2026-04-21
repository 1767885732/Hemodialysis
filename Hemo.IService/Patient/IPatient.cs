/*----------------------------------------------------------------
      // Copyright (C) 2005 (苏州)医疗科技发展有限公司
      // 文件名：IPatient.cs
      // 文件功能描述：病人基本信息数据接口定义文件
      // 创建标识：刘超-2013-3-11
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
using System.ServiceModel;
using Hemo.Model;
using System.Data;

namespace Hemo.IService
{
    [ServiceContract]
    public interface IPatient
    {
        #region 病人基本信息数据接口定义文件


        [OperationContract]
        PatientModel.MED_PATIENTSDataTable GetPatientListByParams(string pPassport, string pPatienName, string pHEMODIALYSIS_ID);

        [OperationContract]
        PatientModel.MED_PATIENTSDataTable GetPatientListByParams(string pPatienName, string pHEMODIALYSIS_ID);

        [OperationContract]
        PatientModel.MED_PATIENTSDataTable GetPatientList();

        [OperationContract]
        PatientModel.MED_PATIENTSDataTable GetPatientListByType(string pType);
        /// <summary>
        /// 根据患者去向获取信息
        /// </summary>
        /// <param name="pType"></param>
        /// <returns></returns>
        [OperationContract]
        PatientModel.MED_PATIENTSDataTable GetPatientListByWhere(string pWhere);

        [OperationContract]
        PatientModel.MED_PATIENTSDataTable GetNewRecordPatientListByDate(DateTime startDate, DateTime endDate);

        [OperationContract]
        PatientModel.MED_PATIENTSDataTable GetDialysisPatientListByDate(DateTime startDate, DateTime endDate);

        [OperationContract]
        int SavePatientInfo(PatientModel.MED_PATIENTSDataTable patientDataTable);
        [OperationContract]
        int SavePatientAndCardInfo(PatientModel.MED_PATIENTSDataTable patientDataTable, DrugModel.MED_PATIENTS_CARDDataTable patientCard);
        [OperationContract]
        int SavePatCardInfo(DrugModel.MED_PATIENTS_CARDDataTable cardInfo);
        [OperationContract]
        DrugModel.MED_PATIENTS_CARDDataTable GetCardInfoByCardInfo(string serialNumber, string cardData);

        DrugModel.MED_PATIENTS_CARDDataTable GetCardInfoByInfo(string hemoId, string cardData);

        [OperationContract]
        int UpdateCardStateByParam(string state, string hemoId);

        [OperationContract]
        MaterialScheduleModel.MED_HEMO_CURE_DEFAULT_MODEDataTable GetHemoDefaultModels(string Id);

        [OperationContract]
        int DeleteHemoDefaultModelById(string id);

        [OperationContract]
        void SaveHemoDefaultModel(MaterialScheduleModel.MED_HEMO_CURE_DEFAULT_MODEDataTable data);


        [OperationContract]
        DataTable QueryMaterialPatientDataByParam(DateTime starTime, DateTime endTime);

        [OperationContract]
        DataTable QueryMaterialPatientDetailByparam(DateTime starTime, DateTime endTime, string materialId);


        [OperationContract]
        DataTable QueryPatientMaterialDataByParam(DateTime starTime, DateTime endTime);

        [OperationContract]
        DataTable QueryPatientMaterialDetailByparam(DateTime starTime, DateTime endTime, string hemoId);


        [OperationContract]
        string GetNewHemoID();

        [OperationContract]
        DataTable GetPatientListBySchedule(DateTime DialysisDate, string pAreaID, string pBanCi);

        [OperationContract]
        DataTable GetPatientListBySchedule(string name, DateTime DialysisDate, string pAreaID, string pBanCi);

        [OperationContract]
        PatientModel.MED_PATIENTSDataTable GetPatientListByPatientID(string _patientID);

        [OperationContract]
        PatientModel.MED_PATIENTSDataTable GetPatientListByInpNo(string _inpNo);

        [OperationContract]
        PatientModel.MED_PATIENTS_PICDataTable GetPatientPicByHemoId(string hemoId);

        int SavePatientPic(PatientModel.MED_PATIENTS_PICDataTable dtPic);

        [OperationContract]
        int DeletePatientByPatient_id(string _patientID);

        [OperationContract]
        int SavePatientRecord(PatientScheduleModel.MED_PATIENTRECORDDataTable dt);

        [OperationContract]
        int SavePatientKolcaba(PatientKolcabaModel.MED_PATIENT_KOLCABADataTable dt);

        [OperationContract]
        PatientKolcabaModel.MED_PATIENT_KOLCABADataTable GetPatientKolcabaByHemoIDandDate(string hemoId, DateTime recordData);

        [OperationContract]
        PatientKolcabaModel.MED_PATIENT_KOLCABADataTable QueryPatientKolcabaByParams(string hemoID, DateTime beginTime, DateTime endTime);

        [OperationContract]
        PatientKolcabaModel.MED_PATIENT_SUFFICIENCYDataTable QueryPatientSufficiencyByParams(string hemoID, DateTime beginTime, DateTime endTime);

        [OperationContract]
        int DeletePatientSufficiencyById(string id);

        [OperationContract]
        PatientKolcabaModel.MED_PATIENT_SUFFICIENCYDataTable GetPatientSufficiencyByHemoIDandDate(string Id);

        [OperationContract]
        int SavePatientSufficiency(PatientKolcabaModel.MED_PATIENT_SUFFICIENCYDataTable dt);

        [OperationContract]
        MaterialScheduleModel.MED_PATIENT_MATERIALDataTable QueryPatientMaterialByParams(string hemoID, DateTime beginTime, DateTime endTime, string recipeId);

        [OperationContract]
        PatientScheduleModel.MED_PATIENTRECORDDataTable GetPatientRecordByHemoIDandDate(string hemoId, DateTime recordData);

        [OperationContract]
        PatientScheduleModel.MED_PATIENTRECORDDataTable QueryPatientRecordByParams(string hemoID, DateTime beginTime, DateTime endTime);

        [OperationContract]
        int DeleteMaterialRecordByID(string id);

        [OperationContract]
        int DeleteMaterialRecordByName(string id);
        [OperationContract]
        int SaveMaterialRecord(MaterialScheduleModel.MED_PATIENT_MATERIALDataTable dt);
        /// <summary>
        //更新出库申请表，并且进行出库操作
        /// </summary>
        /// <param name="dt">出库申请表</param>
        /// <returns></returns>
        [OperationContract]
        int SaveMaterialRecordOut(MaterialScheduleModel.MED_PATIENT_MATERIALDataTable dt);
        /// <summary>
        /// /更新出库申请表，并且进行取消出库操作
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [OperationContract]
        int CancelMaterialRecordOut(MaterialScheduleModel.MED_PATIENT_MATERIALDataTable dt);

        [OperationContract]
        DataTable QueryMaterialModelByParams(string ModelName);
        [OperationContract]
        int DeleteMaterialModelByName(string id);
        [OperationContract]
        int SaveMaterialModel(MaterialScheduleModel.MED_MATERIAL_MODELDataTable dt);
        [OperationContract]
        MaterialScheduleModel.MED_PATIENT_MATERIALDataTable QueryMaterialDetailByParams(string recipeId);

        [OperationContract]
        MaterialScheduleModel.MED_PATIENT_MATERIALDataTable QueryMaterialOutByParams(string recipeId);

        [OperationContract]
        DataTable QueryModelByParams();

        [OperationContract]
        int SavePatientRecordMoule(PatientScheduleModel.MED_RECORDMOULDDataTable dt);

        [OperationContract]
        PatientScheduleModel.MED_RECORDMOULDDataTable GetRecordMouldList();

        /// <summary>
        /// 根据时间和患者姓名查询患者借药信息 add by jiangguang 2014-12-22
        /// </summary>
        /// <param name="starTime">开始时间</param>
        /// <param name="endTime">结束时间 </param>
        /// <param name="patientName">患者姓名</param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetPatientBrowDrugList(DateTime starTime, DateTime endTime, string patientName);
        /// <summary>
        /// 获取患者账单信息
        /// </summary>
        /// <param name="hemoID">血透病历号</param>
        /// <param name="hemoCureID">治疗单号</param>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_CURE_MAIN_BILLDataTable GetPatientBillByHemoIDCureID(string hemoID, DateTime hemoCureID);
        [OperationContract]
        int SavePatientBillInfo(HemodialysisModel.MED_CURE_MAIN_BILLDataTable _patientBill);

        [OperationContract]
        DataTable GetUserBillListByStartEndDate(DateTime start, DateTime end, string defaultHemoID);
        [OperationContract]
        HemodialysisModel.MED_CURE_MAIN_BILLDataTable GetPatientBillByHemoID(string DefaultHemoID, DateTime startDate, DateTime endDate);

        [OperationContract]
        HemoModel.MED_PATIENT_PREPAYDataTable GetRastBillByHemoID(string HemoID);

        [OperationContract]
        HemoModel.MED_PATIENT_PREPAYDataTable GetCurrentRastByHemoID(string HemoID);

        /// <summary>
        /// 根据CureID获取血管通路名称
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetVascularAccessNameByCureId(string cureId);


        [OperationContract]
        DrugModel.MED_PATIENTS_CARDDataTable GetPatientCardDt();
        [OperationContract]
        DrugModel.MED_PATIENTS_CARDDataTable GetPatientsDt();
        #endregion

        #region 患者手术相关
        /// <summary>
        /// 保存患者手术相关信息
        /// </summary>
        /// <param name="_date"></param>
        /// <returns></returns>
        [OperationContract]
        int SavePatientsOperator(ConfigModel.MED_PATIENTS_OPERATORDataTable _date);
        /// <summary>
        /// 根据ID删除信息
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        [OperationContract]
        int DeletePatientOperatorById(string Id);
        /// <summary>
        /// 查询一段时间内患者的手术相关信息
        /// </summary>
        /// <param name="begionTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pName">姓名</param>
        /// <returns>数据集</returns>        
        [OperationContract]
        ConfigModel.MED_PATIENTS_OPERATORDataTable GetPatientOperatorByDate(DateTime begionTime, DateTime endTime, string pName);
        #endregion


        #region 患者风险评估
        /// <summary>
        /// 获取患者风险评估信息
        /// </summary>
        /// <param name="begionTime">查询开始</param>
        /// <param name="endTime">查询结束</param>
        /// <param name="pName">患者姓名</param>
        /// <param name="nurseId">护士编号</param>
        /// <returns></returns>
        [OperationContract]
        HemoModel.MED_PATIENTS_ASSESSMENT_SCOREDataTable GetPatientAssessScoreByDate(DateTime begionTime, DateTime endTime, string pName, string nurseId);
        /// <summary>
        /// 根据ID删除信息
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        [OperationContract]
        int DeletePatientAssessScoreById(string Id);
        /// <summary>
        /// 保存患者评分
        /// </summary>
        /// <param name="_date"></param>
        /// <returns></returns>
        [OperationContract]
        int SavePatientAssessScore(HemoModel.MED_PATIENTS_ASSESSMENT_SCOREDataTable _date);


        #endregion



        #region 患者死亡证明

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        int SavePatientRegDealth(PatientModel.MED_PATIENTREGDEALTHDataTable data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        [OperationContract]
        PatientModel.MED_PATIENTREGDEALTHDataTable GetPatientRegDealthByHemoId(string hemoId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BeginDT"></param>
        /// <param name="EndDT"></param>
        /// <returns></returns>
        [OperationContract]
        PatientModel.MED_PATIENTREGDEALTHDataTable GetPatientRegDealthByData(DateTime BeginDT, DateTime EndDT);

        [OperationContract]
        PatientModel.MED_PATIENTREGDEALTHDataTable GetPatientRegDealthTemplate();

        [OperationContract]
        int InsertTemplate(string id, string parentId, string isCategory, string caption, string type, string createby, DateTime createdate);

        [OperationContract]
        int UpdateTemplate(string id, string caption);

        [OperationContract]
        int UpdateTemplate(string id, string eventanialysis, string correctiveactions);

        [OperationContract]
        int UpdateTemplate(PatientModel.MED_PATIENTREGDEALTHDataTable data);

        [OperationContract]
        int DeleteTemplate(string[] id);

        #endregion


        #region 交班
        [OperationContract]
        HemoModel.MED_HEMO_CHAGEWORKDataTable GetChangeWorkByDate(DateTime begionTime, DateTime endTime);
        [OperationContract]
        HemoModel.MED_HEMO_CHAGEWORKDataTable GetChangeNurseWorkByDate(DateTime begionTime, DateTime endTime);
        [OperationContract]
        int DeleteChangeWorkById(string _changeId);

        [OperationContract]
        HemoModel.MED_HEMO_CHAGEWORK_EXTENDDataTable GetChageWorkExtendByMonth(string workMonth);

        [OperationContract]
        DataTable GetScheduleCoutByAreaID(string areaId, DateTime dt);

        [OperationContract]
        DataTable GetScheduleCoutByParmars(string areaId, string banchiId, DateTime dt);

        [OperationContract]
        int SaveChageWork(HemoModel.MED_HEMO_CHAGEWORKDataTable _date);
        [OperationContract]
        int SaveChageWorkExtend(HemoModel.MED_HEMO_CHAGEWORK_EXTENDDataTable _dateExtend);
        [OperationContract]
        int SaveNurseChangeWork(HemoModel.MED_HEMO_CHAGEWORKDataTable _dataMaster, HemoModel.MED_HEMO_CHAGEWORK_EXTENDDataTable _dataExtend);
        [OperationContract]
        HemoModel.MED_HEMO_CHAGEWORKDataTable GetChangeWorkByParm(string areaId, DateTime changTime);
        #endregion
        #region 预交金

        [OperationContract]
        HemoModel.MED_PATIENT_PREPAYDataTable GetPatientPrePayInfos(string hemoId);
        [OperationContract]
        HemoModel.MED_PATIENT_PREPAYDataTable GetPatientPrePayInfosByUserId(string userID);

        [OperationContract]
        int SavePatientPrePayInfos(HemoModel.MED_PATIENT_PREPAYDataTable data);


        [OperationContract]
        int SaveHemoPatientCorDon(HemoModel.MED_HEMO_CORDONDataTable data);

        [OperationContract]
        HemoModel.MED_HEMO_CORDONDataTable GetHemoPatientCorDon();

        [OperationContract]
        ConfigModel.MED_COMMON_ITEMLISTDataTable GetConfigAccountItem();

        [OperationContract]
        int SaveConfigAccountItem(ConfigModel.MED_COMMON_ITEMLISTDataTable data);

        [OperationContract]
        int SaveConfigPPTItem(ConfigModel.MED_COMMON_ITEMLISTDataTable data, ConfigModel.MED_AUTO_UPDATER_ITEMSDataTable files);

        [OperationContract]
        decimal GetHemoAccountCostByHemoId(string hemoId);

        [OperationContract]
        DataSet GetPatientRecipeChart(string hemoId, DateTime dtStar, DateTime dtEnd);

        #endregion
        #region 扫描件管理

        [OperationContract]
        void InsertDocImage(string hemoId, string docName, byte[] docImage);

        DataTable GetDocImage(string hemoId);

        #endregion


        #region 透析事件
        [OperationContract]
        PatientModel.MED_HEMO_EVENTINFODataTable GetHemoEventInfo(string hemoId, DateTime createdTime, string eventType);
        [OperationContract]
        int SaveHemoEventInfo(PatientModel.MED_HEMO_EVENTINFODataTable data);

        [OperationContract]
        int DeleteHemoEventInfoById(string id);

        [OperationContract]
        PatientModel.MED_HEMO_EVENTINFODataTable GetHemoEventInfoByBetweenDt(DateTime dtStar, DateTime dtEnd, string eventType);

        [OperationContract]
        PatientModel.MED_HEMO_OHTERLOGDataTable GetHemoOhterLogByDt(DateTime dt);

        [OperationContract]
        PatientModel.MED_HEMO_OHTERLOGDataTable GetHemoSingleOhterLogByDt(DateTime currentdt);

        [OperationContract]
        int DeleteHemoOtherLogById(string id);

        [OperationContract]
        int SaveHemoOtherLogInfo(PatientModel.MED_HEMO_OHTERLOGDataTable data);

        [OperationContract]
        DataTable GetHemoOtherLogCureDtByTime(DateTime currentdt);

        [OperationContract]
        DataTable GetCureCountByDt(DateTime dtStar, DateTime dtEnd);
        #endregion

        [OperationContract]
        DataTable GetTempHemoId();

        [OperationContract]
        int SaveTempHemoId(DataTable dtHemoId);


        [OperationContract]
        DataTable SaveImportedPatients(PatientModel.MED_PATIENTSDataTable patients);

        [OperationContract]
        DataSet GetPatientRecipeSPKTChart(string hemoId, DateTime beginTime, DateTime endTime);

        [OperationContract]
        DataSet GetPatientRecipeURRChart(string hemoId, DateTime beginTime, DateTime endTime);



        [OperationContract]
        PatientModel.MED_PATIENTS_EXTENDDataTable GetPatientExtendByParm(string filterStr);

        [OperationContract]
        PatientModel.MED_PATIENTS_EXTENDDataTable GetPatientExtendByHemoId(string HemoId);

        [OperationContract]
        PatientModel.MED_PATIENTSDataTable GetPatientListByHemoIds(string HemoidS);

    }
}
