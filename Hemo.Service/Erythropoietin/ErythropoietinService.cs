/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:ErythropoietinService
 * 创建标识:吕志强-2013年7月5日
 * 
 * 修改时间:2013年10月13日
 * 修改人:贺建操
 * 修改描述:修改方法SaveErythropoietinExecInfo
 * ----------------------------------------------------------------*/
using System;
using Hemo.Business.Erythropoietin;
using Hemo.IService.Erythropoietin;
using Hemo.Model;

namespace Hemo.Service.Erythropoietin
{
    public class ErythropoietinService : MarshalByRefObject, IErythropoietin
    {
        #region IErythropoietin 成员

        public ErythropoietinModel.MED_ERYTHROPOIETINDataTable GetErythropoietinList(string hemodialysisID)
        {
            return ErythropoietinBll.GetErythropoietinList(hemodialysisID);
        }

        public ErythropoietinModel.MED_ERYTHROPOIETINDataTable GetErythropoietinListByTimeSpan(string hemodialysisID, DateTime beginCreateDate, DateTime endCreateDate)
        {
            return ErythropoietinBll.GetErythropoietinListByTimeSpan(hemodialysisID, beginCreateDate, endCreateDate);
        }

        public int SaveErythropoietinInfo(ErythropoietinModel.MED_ERYTHROPOIETINDataTable erythropoietinDataTable)
        {
            return ErythropoietinBll.SaveErythropoietinInfo(erythropoietinDataTable);
        }

        public ErythropoietinModel.MED_ERYTHROPOIETIN_EXECDataTable GetErythropoietinExecList(string erythropoietinID, DateTime beginExecDate, DateTime endExecDate)
        {
            return ErythropoietinBll.GetErythropoietinExecList(erythropoietinID, beginExecDate, endExecDate);
        }

        public int SaveErythropoietinExecInfo(ErythropoietinModel.MED_ERYTHROPOIETIN_EXECDataTable erythropoietinExecDataTable)
        {
            return ErythropoietinBll.SaveErythropoietinExecInfo(erythropoietinExecDataTable);
        }

        #endregion
    }
}
