/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:Hemo.IService.Erythropoietin
 * 创建标识:吕志强-2013年7月5日
 * 
 * 修改时间:2013年10月13日
 * 修改人:贺建操
 * 修改描述:修改方法SaveErythropoietinExecInfo
 * ----------------------------------------------------------------*/

using System;
using System.ServiceModel;
using Hemo.Model;

namespace Hemo.IService.Erythropoietin
{
    [ServiceContract]
    public interface IErythropoietin
    {   
        /// <summary>
        /// GetErythropoietinList
        /// </summary>
        /// <param name="hemodialysisID"></param>
        /// <returns></returns>
        [OperationContract]
        ErythropoietinModel.MED_ERYTHROPOIETINDataTable GetErythropoietinList(string hemodialysisID);

        /// <summary>
        /// GetErythropoietinListByTimeSpan
        /// </summary>
        /// <param name="hemodialysisID"></param>
        /// <param name="beginCreateDate"></param>
        /// <param name="endCreateDate"></param>
        /// <returns></returns>
        [OperationContract]
        ErythropoietinModel.MED_ERYTHROPOIETINDataTable GetErythropoietinListByTimeSpan(string hemodialysisID, DateTime beginCreateDate, DateTime endCreateDate);

        /// <summary>
        /// SaveErythropoietinInfo
        /// </summary>
        /// <param name="erythropoietinDataTable"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveErythropoietinInfo(ErythropoietinModel.MED_ERYTHROPOIETINDataTable erythropoietinDataTable);

        /// <summary>
        /// GetErythropoietinExecList
        /// </summary>
        /// <param name="erythropoietinID"></param>
        /// <param name="beginExecDate"></param>
        /// <param name="endExecDate"></param>
        /// <returns></returns>
        [OperationContract]
        ErythropoietinModel.MED_ERYTHROPOIETIN_EXECDataTable GetErythropoietinExecList(string erythropoietinID, DateTime beginExecDate, DateTime endExecDate);

        /// <summary>
        /// SaveErythropoietinExecInfo
        /// </summary>
        /// <param name="erythropoietinExecDataTable"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveErythropoietinExecInfo(ErythropoietinModel.MED_ERYTHROPOIETIN_EXECDataTable erythropoietinExecDataTable);
    }
}
