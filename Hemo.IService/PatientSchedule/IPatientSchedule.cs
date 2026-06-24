/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:患者排班接口定义文件
 * 创建标识:贺建操-2013年7月5日
 * 
 * 修改时间:2013年10月13日
 * 修改人:顾伟伟
 * 修改描述:修改方法SQLGetPatientScheduleSignle
 * 
 * 修改时间:2014年1月21日
 * 修改人:贺建操
 * 修改描述:修改方法GetPatientScheduleList4Report
 * 
 * 修改时间:2014年5月1日
 * 修改人:顾伟伟
 * 修改描述:新增方法GetPatientScheduleByParames
 * ----------------------------------------------------------------*/
using System;
using System.ServiceModel;
using Hemo.Model;
using System.Data;

namespace Hemo.IService.PatientSchedule
{
    public interface IPatientSchedule
    {
        #region 患者排班接口定义文件

        [OperationContract]
        PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleSignle(DateTime dialysisDate, string hemodialysisID);

        [OperationContract]
        PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleList(string userId, DateTime beginDialysisDate, DateTime endDialysisDate, string banciID);
        [OperationContract]
        PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleListByPara(string userId, DateTime beginDialysisDate, DateTime endDialysisDate);

        [OperationContract]
        PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleListByPara2(string userId, DateTime beginDialysisDate, DateTime endDialysisDate);

        [OperationContract]
        DataTable GetSchedulePatientLabResult(string userId, DateTime beginDate, DateTime endDate, string banchiID, string patientType);

        [OperationContract]
        PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleList4Report(DateTime beginDialysisDate, DateTime endDialysisDate);

        ReportRelationModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleListReportForJL(DateTime beginDialysisDate, DateTime endDialysisDate, string banChi);

        [OperationContract]
        PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleByParames(DateTime beginDialysisDate, DateTime endDialysisDate, string banciID, string areaIDe);

        [OperationContract]
        PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleListByTemplateID(string templateID);

        [OperationContract]
        PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable GetPatientScheduleTemplateList(string banciID);
        [OperationContract]
        PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable GetPatientScheduleAllTemplateList();
        [OperationContract]
        DataTable GetSchedulePatientCheck(DateTime queryData, string banChi);
        [OperationContract]
        PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable GetPatientScheduleTempDataList(string templateId);

        [OperationContract]
        PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable GetPatientScheduleTempDataListNew(string templateId);

        [OperationContract]
        PatientScheduleModel.TRANS_CURE_INFODataTable GetCureInfoByHemoID(string _hemoID);

        [OperationContract]
        DataTable GetSchedulePatientLabResultMain(DateTime beginDate, DateTime endDate);

        [OperationContract]
        PatientScheduleModel.TRANS_SCHEDULE_INFODataTable GetSCHEDULEInfoByHemoID(string _hemoID);

        [OperationContract]
        PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleByRecipeId(string recipeId);

        [OperationContract]
        PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable QueryPatientScheduleByParam(string Patients, string BanChi, string Room);

        [OperationContract]
        DataTable GetSchedulePatientCount(string BanChi, string Room);

        [OperationContract]
        int InSertExecProcLog(string id, string ExecParam);


        [OperationContract]
        int SavePatientScheduleInfo(PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable patientScheduleDataTable);

        [OperationContract]
        int SavePatientScheduleInfoNew(PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable patientScheduleDataTable, DateTime beginDate, DateTime endDate, string userId, bool dateIsFromTemplate, string bloodHemoRoom, bool isHeadNurse);

        [OperationContract]
        int SavePatientScheduleTemplateDataInfoNew(PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable patientScheduleTemplateDataTable);

        [OperationContract]
        int SavePatientScheduleTemplateInfo(PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable patientScheduleTemplateDataTable);

        [OperationContract]
        int SavePatientScheduleTemplateDataInfo(PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable patientScheduleTemplateDataTable);

        [OperationContract]
        int DeletePatientSchedule(DateTime beginDialysisDate, DateTime endDialysisDate, string banciID, string userID);

        [OperationContract]
        int DeletePatientScheduleDateTemp(string PATIENT_SCHEDULE_TEMPLATE_ID);
        [OperationContract]
        int DeletePatientScheduleDateByID(string PATIENT_SCHEDULE_ID);

        [OperationContract]
        PatientScheduleModel.MED_HEMO_APPLYDataTable GetHemodialysisApplyList(string hemodialysisID);

        [OperationContract]
        int SaveHemodialysisApplyInfo(PatientScheduleModel.MED_HEMO_APPLYDataTable hemodialysisApplyDataTable);

        [OperationContract]
        int DeleteHemodialysisApply(string applyID);

        [OperationContract]
        string GetPatientScheduleRecipeIDByStartTime(string pHemoID, DateTime beginDialysisDate);

        [OperationContract]
        string GetServerDate();

        [OperationContract]
        int SaveScheduleRemark(PermissionModel.MED_SCHEDULEREMARKDataTable data);

        [OperationContract]
        PermissionModel.MED_SCHEDULEREMARKDataTable GetScheduleRemarkByDate(DateTime _begin, DateTime _endTime);

        [OperationContract]
        int DeleteScheduleTemplateByTemplateId(string templateId);

        [OperationContract]
        DataTable GetPurificationModeCountByParam(DateTime dt);

        [OperationContract]
        DataTable GetCureCountByParam(DateTime dt);
        [OperationContract]
        DataTable GetCureBillByCureID(string cureID);

        [OperationContract]
        PermissionModel.MED_USERS_WEEKDUTYDataTable GetWeekDutyByDate(DateTime starDate, DateTime endData);

        [OperationContract]
        PermissionModel.MED_USERS_WEEKDUTYDataTable GetWeekDutyByDateDoctor(DateTime starDate, DateTime endData);

        [OperationContract]
        int SaveWeekDutyData(PermissionModel.MED_USERS_WEEKDUTYDataTable data);

        [OperationContract]
        int CreateCurrntDataByLastWeekDoctor(DateTime starDate, DateTime endData);

        [OperationContract]
        int CreateCurrntDataByLastWeek(DateTime starDate, DateTime endData);
        [OperationContract]
        ReportRelationModel.MED_PATIENTDUTYDataTable GetWeekDutyByTime(DateTime starDate, DateTime endData);

        [OperationContract]
        DataTable GetCurrentDutyUser();

        [OperationContract]
        ReportRelationModel.SCHEDULEPATIENTINFODataTable GetQuerySchedulePatientInfo(DateTime queryData);

        [OperationContract]
        string GetCurrentDateNurseDuty(DateTime dt);

        [OperationContract]
        string GetCurrentScheduleInfoByHemoId(string hemoId);

        [OperationContract]
        PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetLatestScheduleByBedNumber(string bedNumber);

        #endregion
    }
}
