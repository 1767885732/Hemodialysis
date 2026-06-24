/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:IDataReport接口
 * 创建标识:刘超-2013年6月28日
 * 
 * 修改时间:2013年11月13日
 * 修改人:刘超
 * 修改描述:新增方法GetDataReportPatientList
 * 
 * 修改时间:2014年3月24日
 * 修改人:刘超
 * 修改描述:新增方法GetDataReportPatientRecipeList
 * 
 * 修改时间:2014年7月31日
 * 修改人:顾伟伟
 * 修改描述:修改方法GetDataReportPatientEstimateSufficiencyList
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Hemo.Model;
using System.Data;

namespace Hemo.IService.DataReport
{
    [ServiceContract]
    public interface IDataReport
    {
        #region 全国数据上报平台
        
        #region 基本信息与血透信息

        /// <summary>
        /// 获取上传患者列表，其中State为患者上传标记
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataReportModel.MED_PATIENTSDataTable GetDataReportPatientList();

        /// <summary>
        /// 保存已经上传成功的患者
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [OperationContract]
        int SavePatientIsUploadDt(DataReportModel.MED_PATIENT_DATAREPORTDataTable dt);
        /// <summary>
        /// 根据透析ID获取血管通路
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        [OperationContract]
        DataReportModel.MED_VASCULAR_ACCESSDataTable GetDataReportPatientVascularList(string hemoId);

        /// <summary>
        /// 根据透析ID获取患者处方信息
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        [OperationContract]
        DataReportModel.MED_HEMO_RECIPEDataTable GetDataReportPatientRecipeList(string hemoId,string typeUp);

        /// <summary>
        /// 根据透析ID获取患者充分性评估信息
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        [OperationContract]
        DataReportModel.MED_ESTIMATE_SUFFICIENCYDataTable GetDataReportPatientEstimateSufficiencyList(string hemoId);

        /// <summary>
        /// 根据透析ID获取患者治疗信息
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        [OperationContract]
        DataReportModel.MED_CURE_MAINDataTable GetDataReportPatientBloodList(string hemoId, string typeUp);

        /// <summary>
        /// 根据透析ID获取患者治疗【肝素】信息
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        [OperationContract]
        DataReportModel.MED_CURE_MAINDataTable GetDataReportPatientAncitoaList(string hemoId, string typeUp);


        #endregion
        #region 诊断信息
        /// <summary>
        /// 获取上传患者列表诊断信息，其中State为患者上传标记
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataReportModel.MED_PATIENTSDataTable GetDataReportPatientDiagnoseList(string hemoId,string typeUp);
        #endregion
        #region 检查检验信息
        /// <summary>
        /// 获取上传患者列表检验信息信息，其中State为患者上传标记
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataReportModel.MED_LAB_MASTERDataTable GetDataReportPatientLabList(string patientId, string typeUp,DateTime dtStar,DateTime dtEnd);
        #endregion

        #endregion
        #region 福建省数据上报平台
        /// <summary>
        /// 获取上传患者列表，其中State为患者上传标记
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataReportModel.MED_PATIENTSDataTable GetDataReportPatientListFZ();

        /// <summary>
        /// 保存已经上传成功的患者
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [OperationContract]
        int SavePatientIsUploadDtFZ(DataReportModel.MED_PATIENT_DATAREPORTDataTable dt);

        #region 上报平台一键上传
        [OperationContract]
        DataReportModel.MED_PATIENT_DATAREPORTDataTable GetHavingUpLoadPatient(string upType,string state,string recordType,string type);
        #endregion


        #endregion

    }
}
