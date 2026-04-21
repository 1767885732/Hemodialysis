/*----------------------------------------------------------------
// Copyright (C) 2005 (北京)医疗科技发展有限公司
// 文件名：LabService.cs
// 文件功能描述：常规检查数据服务类
// 创建标识：
// 修改时间：2014-4-9
// 修改人：吕志强
// 修改描述：添加常规检查数据服务方法
//
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Data;
using Hemo.Business.Lab;
using Hemo.IService.Lab;

namespace Hemo.Service.Lab
{
    public class LabService : MarshalByRefObject, ILab
    {
        #region ILab 成员

        public DataTable GetPatientLabList(string patientID)
        {
            return LabBll.GetPatientLabList(patientID);
        }

        public DataTable GetPatientLabListByDate(string patientID, DateTime startDate, DateTime endDate)
        {
            return LabBll.GetPatientLabListByDate(patientID, startDate, endDate);
        }

        /// <summary>
        /// 获取三个月内常规检验数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetThreeMonthsCommonLabList(string pPatientType)
        {
            return LabBll.GetThreeMonthsCommonLabList(pPatientType);
        }
        public DataTable GetThreeMonthsCommonLabListByHemoID(string hemoId)
        {
            return LabBll.GetThreeMonthsCommonLabListByHemoId(hemoId);
        }

        public DataTable GetSixMonthsCommonLabListByHemoID(string hemoId)
        {
            return LabBll.GetSixMonthsCommonLabListByHemoId(hemoId);
        }
        /// <summary>
        /// 获取半年内常规检验数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetSixMonthsCommonLabList(string pPatientType)
        {
            return LabBll.GetSixMonthsCommonLabList(pPatientType);
        }

        /// <summary>
        /// 按年份获取感染检查列表
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataTable GetInfectionCheckListByYear(string year)
        {
            return LabBll.GetInfectionCheckListByYear(year);
        }

        /// <summary>
        /// 按日期获取感染检查列表
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable GetInfectionCheckListByDate(DateTime beginDate, DateTime endDate)
        {
            return LabBll.GetInfectionCheckListByDate(beginDate, endDate);
        }

        /// <summary>
        /// 按年份获取质量管理基础数据列表
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataTable GetQualityControlBaseDataByYear(string year)
        {
            return LabBll.GetQualityControlBaseDataByYear(year);
        }

        /// <summary>
        /// 按日期获取质量管理基础数据列表
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable GetQualityControlBaseDataByDate(DateTime beginDate, DateTime endDate)
        {
            return LabBll.GetQualityControlBaseDataByDate(beginDate, endDate);
        }

        /// <summary>
        /// 获取血透机、专职人员统计数据
        /// </summary>
        public DataTable GetMachineAndSpecialistCount()
        {
            return LabBll.GetMachineAndSpecialistCount();
        }

        /// <summary>
        /// 按年份获取患者质量监测指标列表
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataTable GetQualityMonitorIndicatorByYear(string year)
        {
            return LabBll.GetQualityMonitorIndicatorByYear(year);
        }

        /// <summary>
        /// 按日期获取患者质量监测指标列表
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable GetQualityMonitorIndicatorByDate(DateTime beginDate, DateTime endDate)
        {
            return LabBll.GetQualityMonitorIndicatorByDate(beginDate, endDate);
        }

        /// <summary>
        /// 获取溶质清除统计列表
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataTable GetUreaRemoveCountList(string year)
        {
            return LabBll.GetUreaRemoveCountList(year);
        }

        /// <summary>
        /// 获取肾性贫血纠正例数统计列表
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataTable GetRenalAnemiaCountList(string year)
        {
            return LabBll.GetRenalAnemiaCountList(year);
        }

        /// <summary>
        /// 获取病人血红蛋白趋势
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public DataTable GetHBTrend(string patientId, DateTime startDate, DateTime endDate)
        {
            return LabBll.GetHBTrend(patientId, startDate, endDate);
        }

        /// <summary>
        /// 根据PatientId获取患者检查列表
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public DataTable GetPatientExamList(string patientId)
        {
            return LabBll.GetPatientExamList(patientId);
        }

        /// <summary>
        /// 根据PatientId和日期获取患者检查列表
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable GetPatientExamListByDate(string patientId, DateTime startDate, DateTime endDate)
        {
            return LabBll.GetPatientExamListByDate(patientId, startDate, endDate);
        }

        /// <summary>
        /// 根据检查序号获取患者检查明细列表
        /// </summary>
        /// <param name="examNo"></param>
        /// <returns></returns>
        public DataTable GetPatientExamDetailListByNo(string examNo)
        {
            return LabBll.GetPatientExamDetailListByNo(examNo);
        }

        #endregion

        #region 常规检查数据服务类

        public DataTable GetMedPatientQualityData(DateTime startDate, DateTime endDate)
        {
            return LabBll.GetMedPatientQualityData(startDate, endDate);
        }

        public int Save_MED_REPORT_DATA(DateTime startDate)
        {
            return LabBll.Save_MED_REPORT_DATA(startDate);
        }

        public DataTable GetLabListByDateAndItems(DateTime STARTDATE, DateTime ENDDATE, string ITEM_NAME)
        {
            return LabBll.GetLabListByDateAndItems(STARTDATE, ENDDATE, ITEM_NAME);
        }

        public DataTable GetLabListByDateAndItemsAndHemoInfo(DateTime STARTDATE, DateTime ENDDATE, string ITEM_NAME, String PATIENTINFO)
        {
            return LabBll.GetLabListByDateAndItemsAndHemoInfo(STARTDATE, ENDDATE, ITEM_NAME, PATIENTINFO);
        }

        public string SAVE_MED_HIS_ROWTOCOL(DataTable dtSource, DataTable dtTitle, string ITEM_NAME, DateTime begintime, DateTime endtime)
        {
            return LabBll.SAVE_MED_HIS_ROWTOCOL(dtSource, dtTitle, ITEM_NAME, begintime, endtime);
        }

        public DataTable GetLabListByDateAndItemsAndDtl(DateTime STARTDATE, DateTime ENDDATE, string ITEM_NAME, DataTable dtTitle)
        {
            return LabBll.GetLabListByDateAndItemsAndDtl(STARTDATE, ENDDATE, ITEM_NAME, dtTitle);
        }

        public DataTable GetLabListByDateAndItemsAndHemoInfoAndDtl(DateTime STARTDATE, DateTime ENDDATE, string ITEM_NAME, String PATIENTINFO, DataTable dtTitle)
        {
            return LabBll.GetLabListByDateAndItemsAndHemoInfoAndDtl(STARTDATE, ENDDATE, ITEM_NAME, PATIENTINFO, dtTitle);
        }

        public DataTable get_med_vw_xuehongdanbai_ext(DateTime begintime, DateTime endtime)
        {
            return LabBll.get_med_vw_xuehongdanbai_ext(begintime, endtime);
        }

        public int INSERT_UPDATE_MED_HIS_ROWTOCOL_END(string hemodialysis_id, string patient_id, string test_no, DateTime check_date, string item_name)
        {
            return LabBll.INSERT_UPDATE_MED_HIS_ROWTOCOL_END(hemodialysis_id, patient_id, test_no, check_date, item_name);
        }
        #endregion
    }
}
