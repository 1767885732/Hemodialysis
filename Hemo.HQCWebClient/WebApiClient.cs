/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:WebApiClient
 * 创建标识:吕志强-2017年9月18日
 * 
 * 修改时间:2017年9月18日
 * 修改人:吕志强
 * 修改描述:新增方法SaveHospitalMgrInfo
 * ----------------------------------------------------------------*/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hemo.HQCWebClient.Models;

namespace Hemo.HQCWebClient
{
    /// <summary>
    /// WebApi服务访问客户端
    /// </summary>
    public class WebApiClient
    {
        private static int staffId = 0;

        static WebApiClient()
        {
            Random random = new Random();
            staffId = random.Next(1, 100);
        }

        /// <summary>
        /// 保存科室信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="saveApi"></param>
        /// <param name="tokenApi"></param>
        /// <returns></returns>
        public static ResultMsg<string> SaveHospitalMgrInfo(MedHospitalMgrInfo info, string saveApi, string tokenApi)
        {
            ResultMsg<string> resultMsg = null;
            if (info != null)
            {
                int staffId = int.Parse(info.HospitalId);
                var query = JsonConvert.SerializeObject(info);
                resultMsg = WebApiHelper.PostWebApi<string>(saveApi, tokenApi, query, staffId, true);
                return resultMsg;
            }

            resultMsg = new ResultMsg<string>();
            resultMsg.StatusCode = (int)StatusCodeEnum.ParameterError;
            resultMsg.Info = StatusCodeEnum.ParameterError.GetEnumText();
            resultMsg.Data = string.Empty;
            return resultMsg;
        }

        /// <summary>
        /// 获取科室信息
        /// </summary>
        /// <param name="getApi"></param>
        /// <param name="tokenApi"></param>
        /// <returns></returns>
        public static ResultMsg<DataTable> GetHospitalMgrInfo(string getApi, string tokenApi)
        {
            ResultMsg<DataTable> resultMsg = null;
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("staffId", staffId.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            resultMsg = WebApiHelper.GetWebApi<DataTable>(getApi, tokenApi, parameters.Item1, parameters.Item2, staffId, true);
            return resultMsg;
        }

        /// <summary>
        /// 获取质控平台用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="getApi"></param>
        /// <param name="tokenApi"></param>
        /// <returns></returns>
        public static ResultMsg<MedUserInfo> GetUserByName(string userName, string getApi, string tokenApi)
        {
            ResultMsg<MedUserInfo> resultMsg = null;
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("staffId", staffId.ToString());
            parames.Add("userName", userName);
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            string query = parameters.Item1.Substring(parameters.Item1.IndexOf("userName"));
            string queryStr = parameters.Item2.Substring(parameters.Item2.IndexOf("userName"));
            resultMsg = WebApiHelper.GetWebApi<MedUserInfo>(getApi, tokenApi, query, queryStr, staffId, true);
            return resultMsg;
        }

        /// <summary>
        /// 根据类型获取字典列表
        /// </summary>
        /// <param name="dictType"></param>
        /// <param name="getApi"></param>
        /// <param name="tokenApi"></param>
        /// <returns></returns>
        public static ResultMsg<List<MedDictData>> GetDictListByType(string dictType, string getApi, string tokenApi)
        {
            ResultMsg<List<MedDictData>> resultMsg = null;
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("staffId", staffId.ToString());
            parames.Add("dictType", dictType);
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            string query = parameters.Item1.Substring(parameters.Item1.IndexOf("dictType"));
            string queryStr = parameters.Item2.Substring(parameters.Item2.IndexOf("dictType"));
            resultMsg = WebApiHelper.GetWebApi<List<MedDictData>>(getApi, tokenApi, query, queryStr, staffId, true);
            return resultMsg;
        }

        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <param name="getApi"></param>
        /// <param name="tokenApi"></param>
        /// <returns></returns>
        public static ResultMsg<List<MedDictData>> GetDictList(string getApi, string tokenApi)
        {
            ResultMsg<List<MedDictData>> resultMsg = null;
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("staffId", staffId.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            resultMsg = WebApiHelper.GetWebApi<List<MedDictData>>(getApi, tokenApi, parameters.Item1, parameters.Item2, staffId, true);
            return resultMsg;
        }

        /// <summary>
        /// 获取类型字典列表
        /// </summary>
        /// <param name="getApi"></param>
        /// <param name="tokenApi"></param>
        /// <returns></returns>
        public static ResultMsg<List<MedDictType>> GetDictTypeList(string getApi, string tokenApi)
        {
            ResultMsg<List<MedDictType>> resultMsg = null;
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("staffId", staffId.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            resultMsg = WebApiHelper.GetWebApi<List<MedDictType>>(getApi, tokenApi, parameters.Item1, parameters.Item2, staffId, true);
            return resultMsg;
        }

        /// <summary>
        /// 保存Monitor Index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="saveApi"></param>
        /// <param name="tokenApi"></param>
        /// <returns></returns>
        public static ResultMsg<string> SaveMonitorIndex(MedMonitorIndex index, string saveApi, string tokenApi)
        {
            ResultMsg<string> resultMsg = null;
            if (index != null)
            {
                int staffId = int.Parse(index.HospitalId);
                var query = JsonConvert.SerializeObject(index);
                resultMsg = WebApiHelper.PostWebApi<string>(saveApi, tokenApi, query, staffId, true);
                return resultMsg;
            }

            resultMsg = new ResultMsg<string>();
            resultMsg.StatusCode = (int)StatusCodeEnum.ParameterError;
            resultMsg.Info = StatusCodeEnum.ParameterError.GetEnumText();
            resultMsg.Data = string.Empty;
            return resultMsg;
        }

        /// <summary>
        /// 保存Primary Data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="saveApi"></param>
        /// <param name="tokenApi"></param>
        /// <returns></returns>
        public static ResultMsg<string> SavePrimaryData(MedPrimaryData data, string saveApi, string tokenApi)
        {
            ResultMsg<string> resultMsg = null;
            if (data != null)
            {
                var query = JsonConvert.SerializeObject(data);
                resultMsg = WebApiHelper.PostWebApi<string>(saveApi, tokenApi, query, staffId, true);
                return resultMsg;
            }

            resultMsg = new ResultMsg<string>();
            resultMsg.StatusCode = (int)StatusCodeEnum.ParameterError;
            resultMsg.Info = StatusCodeEnum.ParameterError.GetEnumText();
            resultMsg.Data = string.Empty;
            return resultMsg;
        }

        /// <summary>
        /// 保存Hospital Infection
        /// </summary>
        /// <param name="infection"></param>
        /// <param name="saveApi"></param>
        /// <param name="tokenApi"></param>
        /// <returns></returns>
        public static ResultMsg<string> SaveHospitalInfection(MedHospitalInfection infection, string saveApi, string tokenApi)
        {
            ResultMsg<string> resultMsg = null;
            if (infection != null)
            {
                var query = JsonConvert.SerializeObject(infection);
                resultMsg = WebApiHelper.PostWebApi<string>(saveApi, tokenApi, query, staffId, true);
                return resultMsg;
            }

            resultMsg = new ResultMsg<string>();
            resultMsg.StatusCode = (int)StatusCodeEnum.ParameterError;
            resultMsg.Info = StatusCodeEnum.ParameterError.GetEnumText();
            resultMsg.Data = string.Empty;
            return resultMsg;
        }

        /// <summary>
        /// 保存Machine Mgr
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="saveApi"></param>
        /// <param name="tokenApi"></param>
        /// <returns></returns>
        public static ResultMsg<string> SaveMachineMgr(MedMachineMgr machine, string saveApi, string tokenApi)
        {
            ResultMsg<string> resultMsg = null;
            if (machine != null)
            {
                var query = JsonConvert.SerializeObject(machine);
                resultMsg = WebApiHelper.PostWebApi<string>(saveApi, tokenApi, query, staffId, true);
                return resultMsg;
            }

            resultMsg = new ResultMsg<string>();
            resultMsg.StatusCode = (int)StatusCodeEnum.ParameterError;
            resultMsg.Info = StatusCodeEnum.ParameterError.GetEnumText();
            resultMsg.Data = string.Empty;
            return resultMsg;
        }

        /// <summary>
        /// 保存WorkLoadAccount
        /// </summary>
        /// <param name="workLoadAccount"></param>
        /// <param name="saveApi"></param>
        /// <param name="tokenApi"></param>
        /// <returns></returns>
        public static ResultMsg<string> SaveWorkLoadAccount(MedWorkLoadAccount workLoadAccount, string saveApi, string tokenApi)
        {
            ResultMsg<string> resultMsg = null;
            if (workLoadAccount != null)
            {
                var query = JsonConvert.SerializeObject(workLoadAccount);
                resultMsg = WebApiHelper.PostWebApi<string>(saveApi, tokenApi, query, staffId, true);
                return resultMsg;
            }

            resultMsg = new ResultMsg<string>();
            resultMsg.StatusCode = (int)StatusCodeEnum.ParameterError;
            resultMsg.Info = StatusCodeEnum.ParameterError.GetEnumText();
            resultMsg.Data = string.Empty;
            return resultMsg;
        }

        /// <summary>
        /// 保存HemoYreaChart
        /// </summary>
        /// <param name="hemoYreaChart"></param>
        /// <param name="saveApi"></param>
        /// <param name="tokenApi"></param>
        /// <returns></returns>
        public static ResultMsg<string> SaveHemoYreaChart(MedHemoYreaChart hemoYreaChart, string saveApi, string tokenApi)
        {
            ResultMsg<string> resultMsg = null;
            if (hemoYreaChart != null)
            {
                var query = JsonConvert.SerializeObject(hemoYreaChart);
                resultMsg = WebApiHelper.PostWebApi<string>(saveApi, tokenApi, query, staffId, true);
                return resultMsg;
            }

            resultMsg = new ResultMsg<string>();
            resultMsg.StatusCode = (int)StatusCodeEnum.ParameterError;
            resultMsg.Info = StatusCodeEnum.ParameterError.GetEnumText();
            resultMsg.Data = string.Empty;
            return resultMsg;
        }

        /// <summary>
        /// 保存Clinical Mgr
        /// </summary>
        /// <param name="clinicalMgr"></param>
        /// <param name="saveApi"></param>
        /// <param name="tokenApi"></param>
        /// <returns></returns>
        public static ResultMsg<string> SaveClinicalMgr(MedClinicalMgr clinicalMgr, string saveApi, string tokenApi)
        {
            ResultMsg<string> resultMsg = null;
            if (clinicalMgr != null)
            {
                var query = JsonConvert.SerializeObject(clinicalMgr);
                resultMsg = WebApiHelper.PostWebApi<string>(saveApi, tokenApi, query, staffId, true);
                return resultMsg;
            }

            resultMsg = new ResultMsg<string>();
            resultMsg.StatusCode = (int)StatusCodeEnum.ParameterError;
            resultMsg.Info = StatusCodeEnum.ParameterError.GetEnumText();
            resultMsg.Data = string.Empty;
            return resultMsg;
        }

        /// <summary>
        /// 保存Patient Info
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="saveApi"></param>
        /// <param name="tokenApi"></param>
        /// <returns></returns>
        public static ResultMsg<string> SavePatientInfo(MedPatientsInfo patientInfo, string saveApi, string tokenApi)
        {
            ResultMsg<string> resultMsg = null;
            if (patientInfo != null)
            {
                var query = JsonConvert.SerializeObject(patientInfo);
                resultMsg = WebApiHelper.PostWebApi<string>(saveApi, tokenApi, query, staffId, true);
                return resultMsg;
            }

            resultMsg = new ResultMsg<string>();
            resultMsg.StatusCode = (int)StatusCodeEnum.ParameterError;
            resultMsg.Info = StatusCodeEnum.ParameterError.GetEnumText();
            resultMsg.Data = string.Empty;
            return resultMsg;
        }

        /// <summary>
        /// 保存Patient List
        /// </summary>
        /// <param name="infoList"></param>
        /// <param name="saveApi"></param>
        /// <param name="tokenApi"></param>
        /// <returns></returns>
        public static ResultMsg<string> SavePatientInfo(List<MedPatientsInfo> infoList, string saveApi, string tokenApi)
        {
            ResultMsg<string> resultMsg = null;
            if (infoList != null && infoList.Count > 0)
            {
                var query = JsonConvert.SerializeObject(infoList);
                resultMsg = WebApiHelper.PostWebApi<string>(saveApi, tokenApi, query, staffId, true);
                return resultMsg;
            }

            resultMsg = new ResultMsg<string>();
            resultMsg.StatusCode = (int)StatusCodeEnum.ParameterError;
            resultMsg.Info = StatusCodeEnum.ParameterError.GetEnumText();
            resultMsg.Data = string.Empty;
            return resultMsg;
        }

        /// <summary>
        /// 保存Base Record Info
        /// </summary>
        /// <param name="baseRecordInfo"></param>
        /// <param name="saveApi"></param>
        /// <param name="tokenApi"></param>
        /// <returns></returns>
        public static ResultMsg<string> SaveBaseRecordInfo(MedBaseRecordInfo baseRecordInfo, string saveApi, string tokenApi)
        {
            ResultMsg<string> resultMsg = null;
            if (baseRecordInfo != null)
            {
                var query = JsonConvert.SerializeObject(baseRecordInfo);
                resultMsg = WebApiHelper.PostWebApi<string>(saveApi, tokenApi, query, staffId, true);
                return resultMsg;
            }

            resultMsg = new ResultMsg<string>();
            resultMsg.StatusCode = (int)StatusCodeEnum.ParameterError;
            resultMsg.Info = StatusCodeEnum.ParameterError.GetEnumText();
            resultMsg.Data = string.Empty;
            return resultMsg;
        }

        /// <summary>
        /// 保存患者病程记录
        /// </summary>
        /// <param name="baseRecordInfo"></param>
        /// <param name="saveApi"></param>
        /// <param name="tokenApi"></param>
        /// <returns></returns>
        public static ResultMsg<string> SavePatientProgressNoteInfo(MED_PATIENT_PROGRESS_NOTEINFO baseInfo, string saveApi, string tokenApi)
        {
            ResultMsg<string> resultMsg = null;
            if (baseInfo != null)
            {
                var query = JsonConvert.SerializeObject(baseInfo);
                resultMsg = WebApiHelper.PostWebApi<string>(saveApi, tokenApi, query, staffId, true);
                return resultMsg;
            }

            resultMsg = new ResultMsg<string>();
            resultMsg.StatusCode = (int)StatusCodeEnum.ParameterError;
            resultMsg.Info = StatusCodeEnum.ParameterError.GetEnumText();
            resultMsg.Data = string.Empty;
            return resultMsg;
        }

        /// <summary>
        /// 保存患者治疗信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="saveApi"></param>
        /// <param name="tokenApi"></param>
        /// <returns></returns>
        public static ResultMsg<string> SaveCureInfo(MED_CURE_MAIN info, string saveApi, string tokenApi)
        {
            ResultMsg<string> resultMsg = null;
            if (info != null)
            {
                var query = JsonConvert.SerializeObject(info);
                resultMsg = WebApiHelper.PostWebApi<string>(saveApi, tokenApi, query, staffId, true);
                return resultMsg;
            }

            resultMsg = new ResultMsg<string>();
            resultMsg.StatusCode = (int)StatusCodeEnum.ParameterError;
            resultMsg.Info = StatusCodeEnum.ParameterError.GetEnumText();
            resultMsg.Data = string.Empty;
            return resultMsg;
        }

        /// <summary>
        /// 保存透析参数
        /// </summary>
        /// <param name="info"></param>
        /// <param name="saveApi"></param>
        /// <param name="tokenApi"></param>
        /// <returns></returns>
        public static ResultMsg<string> SaveHemoParameters(MED_HEMODIALYSIS_PARAMETERS info, string saveApi, string tokenApi)
        {
            ResultMsg<string> resultMsg = null;
            if (info != null)
            {
                var query = JsonConvert.SerializeObject(info);
                resultMsg = WebApiHelper.PostWebApi<string>(saveApi, tokenApi, query, staffId, true);
                return resultMsg;
            }

            resultMsg = new ResultMsg<string>();
            resultMsg.StatusCode = (int)StatusCodeEnum.ParameterError;
            resultMsg.Info = StatusCodeEnum.ParameterError.GetEnumText();
            resultMsg.Data = string.Empty;
            return resultMsg;
        }
    }
}
