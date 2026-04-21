/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:DataReportService服务
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
using Hemo.IService.DataReport;
using Hemo.Model;
using Hemo.Business.DataReport;

namespace Hemo.Service.DataReport
{
    public class DataReportService : MarshalByRefObject,IDataReport
    {
        #region 全国数据上报平台
        #region 血透信息

        public DataReportModel.MED_PATIENTSDataTable GetDataReportPatientList()
        {
            return DataReportBll.GetDataReportPatientList();

        }

        public int SavePatientIsUploadDt(DataReportModel.MED_PATIENT_DATAREPORTDataTable dt)
        {
            return DataReportBll.SavePatientIsUploadDt(dt);
        }

        public DataReportModel.MED_VASCULAR_ACCESSDataTable GetDataReportPatientVascularList(string hemoId)
        {
            return DataReportBll.GetDataReportPatientVascularList(hemoId);

        }

        public DataReportModel.MED_HEMO_RECIPEDataTable GetDataReportPatientRecipeList(string hemoId, string typeUp)
        {
            return DataReportBll.GetDataReportPatientRecipeList(hemoId,typeUp);

        }

        public DataReportModel.MED_CURE_MAINDataTable GetDataReportPatientBloodList(string hemoId, string typeUp)
        {
            return DataReportBll.GetDataReportPatientBloodList(hemoId, typeUp);

        }

        public DataReportModel.MED_CURE_MAINDataTable GetDataReportPatientAncitoaList(string hemoId, string typeUp)
        {
            return DataReportBll.GetDataReportPatientAncitoaList(hemoId, typeUp);

        }

        public DataReportModel.MED_ESTIMATE_SUFFICIENCYDataTable GetDataReportPatientEstimateSufficiencyList(string hemoId)
        {
            return DataReportBll.GetDataReportPatientEstimateSufficiencyList(hemoId);
        }

        #endregion

        #region 诊断信息
        
        public DataReportModel.MED_PATIENTSDataTable GetDataReportPatientDiagnoseList(string hemoId, string typeUp)
        {
            return DataReportBll.GetDataReportPatientDiagnoseList(hemoId,typeUp);
        }


        #endregion

        #region 检查检验信息
        public DataReportModel.MED_LAB_MASTERDataTable GetDataReportPatientLabList(string patientId, string typeUp,DateTime dtStar,DateTime dtEnd)
        {
            return DataReportBll.GetDataReportPatientLabList(patientId, typeUp,dtStar,dtEnd);
        }
        #endregion

        #endregion

        #region 福建省数据上报平台
        /// <summary>
        /// 获取上传患者列表，其中State为患者上传标记
        /// </summary>
        /// <returns></returns>
        public DataReportModel.MED_PATIENTSDataTable GetDataReportPatientListFZ()
        {
            return DataReportBll.GetDataReportPatientListFZ();

        }
        /// <summary>
        /// 保存已经上传成功的患者
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int SavePatientIsUploadDtFZ(DataReportModel.MED_PATIENT_DATAREPORTDataTable dt)
        {
            return DataReportBll.SavePatientIsUploadDtFZ(dt);

        }
        /// <summary>
        /// 根据条件获取上传成功的记录
        /// </summary>
        /// <param name="upType">EXTEND5</param>
        /// <param name="state">STATE</param>
        /// <param name="recordType">EXTEND</param>
        /// <param name="type">TYPE</param>
        /// <returns></returns>
        public DataReportModel.MED_PATIENT_DATAREPORTDataTable GetHavingUpLoadPatient(string upType, string state, string recordType, string type)
        {
            return DataReportBll.GetHavingUpLoadPatient(upType,state,recordType,type);

        }
        #endregion


    }
}
