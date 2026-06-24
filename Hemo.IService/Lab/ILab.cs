/*----------------------------------------------------------------
// Copyright (C) 2005 (苏州)医疗科技发展有限公司
// 文件名：ILab.cs
// 文件功能描述：常规检查数据接口定义文件
// 创建标识：
// 修改时间：2014-4-9
// 修改人：吕志强
// 修改描述：添加常规检查数据接口方法
//
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System.Data;
using System.ServiceModel;
using System;

namespace Hemo.IService.Lab
{
    [ServiceContract]
    public interface ILab
    {

        #region 常规检查数据接口

        [OperationContract]
        DataTable GetPatientLabList(string patientID);

        [OperationBehavior]
        DataTable GetPatientLabListByDate(string patientID, DateTime pStartDate, DateTime pEndDate);

        [OperationContract]
        DataTable GetThreeMonthsCommonLabList(string pPatientType);

        [OperationContract]
        DataTable GetThreeMonthsCommonLabListByHemoID(string hemoId);

        [OperationContract]
        DataTable GetSixMonthsCommonLabListByHemoID(string hemoId);
        [OperationContract]
        DataTable GetSixMonthsCommonLabList(string pPatientType);

        [OperationContract]
        DataTable GetInfectionCheckListByYear(string year);

        [OperationContract]
        DataTable GetInfectionCheckListByDate(DateTime beginDate, DateTime endDate);

        [OperationContract]
        DataTable GetQualityControlBaseDataByYear(string year);

        [OperationContract]
        DataTable GetQualityControlBaseDataByDate(DateTime beginDate, DateTime endDate);

        [OperationContract]
        DataTable GetMachineAndSpecialistCount();

        [OperationContract]
        DataTable GetQualityMonitorIndicatorByYear(string year);

        [OperationContract]
        DataTable GetQualityMonitorIndicatorByDate(DateTime beginDate, DateTime endDate);

        [OperationContract]
        DataTable GetUreaRemoveCountList(string year);

        [OperationContract]
        DataTable GetRenalAnemiaCountList(string year);

        [OperationContract]
        DataTable GetHBTrend(string patientId, DateTime startDate, DateTime endDate);

        [OperationContract]
        DataTable GetMedPatientQualityData(DateTime startDate, DateTime endDate);

        [OperationContract]
        DataTable GetPatientExamList(string patientId);

        [OperationBehavior]
        DataTable GetPatientExamListByDate(string patientId, DateTime startDate, DateTime endDate);

        [OperationBehavior]
        DataTable GetPatientExamDetailListByNo(string examNo);

        [OperationContract]
        int Save_MED_REPORT_DATA(DateTime startDate);

        [OperationContract]
        DataTable GetLabListByDateAndItems(DateTime STARTDATE, DateTime ENDDATE, string ITEM_NAME);


        [OperationContract]
        DataTable GetLabListByDateAndItemsAndHemoInfo(DateTime STARTDATE, DateTime ENDDATE, string ITEM_NAME, String PATIENTINFO);

        [OperationContract]
        string SAVE_MED_HIS_ROWTOCOL(DataTable dtSource, DataTable dtTitle, string ITEM_NAME, DateTime begintime, DateTime endtime);

        [OperationContract]
        DataTable GetLabListByDateAndItemsAndDtl(DateTime STARTDATE, DateTime ENDDATE, string ITEM_NAME, DataTable dtTitle);

        [OperationContract]
        DataTable get_med_vw_xuehongdanbai_ext(DateTime begintime, DateTime endtime);


        [OperationContract]
        DataTable GetLabListByDateAndItemsAndHemoInfoAndDtl(DateTime STARTDATE, DateTime ENDDATE, string ITEM_NAME, String PATIENTINFO, DataTable dtTitle);

        [OperationContract]
        int INSERT_UPDATE_MED_HIS_ROWTOCOL_END(string hemodialysis_id, string patient_id, string test_no, DateTime check_date, string item_name);
        #endregion
    }
}
