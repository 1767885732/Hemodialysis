/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:Hemo.Business.Erythropoietin
 * 创建标识:刘超-2013年8月1日
 * 
 * 修改时间:2013年12月17日
 * 修改人:吕志强
 * 修改描述:通用窗体取值、赋值、清空值、验证控件值方法
 * 
 * 修改时间:2014年4月27日
 * 修改人:顾伟伟
 * 修改描述:修复病人的处方无法确认衍生的问题
 * 
 * 修改时间:2014年9月3日
 * 修改人:刘超
 * 修改描述:更新根据药品厂商编号得到数据SQL
 * ----------------------------------------------------------------*/
using System;
using System.Data;
using System.Data.Common;
using Hemo.Model;

namespace Hemo.Business.Erythropoietin
{
    public class ErythropoietinBll : BaseClass
    {
        /// <summary>
        /// MED_ERYTHROPOIETINDataTable
        /// </summary>
        /// <param name="hemodialysisID"></param>
        /// <returns></returns>
        public static ErythropoietinModel.MED_ERYTHROPOIETINDataTable GetErythropoietinList(string hemodialysisID)
        {
            ErythropoietinModel.MED_ERYTHROPOIETINDataTable result = new ErythropoietinModel.MED_ERYTHROPOIETINDataTable();
            DbParameter[] dbParams = new DbParameter[1];

            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemodialysisID.Trim());

            return GetData<ErythropoietinModel.MED_ERYTHROPOIETINDataTable>(result, "GetErythropoietinList", dbParams);
        }
        /// <summary>
        /// MED_ERYTHROPOIETINDataTable
        /// </summary>
        /// <param name="hemodialysisID"></param>
        /// <param name="beginCreateDate"></param>
        /// <param name="endCreateDate"></param>
        /// <returns></returns>
        public static ErythropoietinModel.MED_ERYTHROPOIETINDataTable GetErythropoietinListByTimeSpan(string hemodialysisID, DateTime beginCreateDate, DateTime endCreateDate)
        {
            ErythropoietinModel.MED_ERYTHROPOIETINDataTable result = new ErythropoietinModel.MED_ERYTHROPOIETINDataTable();
            DbParameter[] dbParams = new DbParameter[3];

            dbParams[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, hemodialysisID.Trim());
            dbParams[1] = IDatabase.BuildDbParameter("BEGINCREATE_TIME", DbType.String, beginCreateDate);
            dbParams[2] = IDatabase.BuildDbParameter("ENDCREATE_TIME", DbType.String, endCreateDate);

            return GetData<ErythropoietinModel.MED_ERYTHROPOIETINDataTable>(result, "GetErythropoietinListByTimeSpan", dbParams);
        }
        /// <summary>
        /// SaveErythropoietinInfo
        /// </summary>
        /// <param name="erythropoietinDataTable"></param>
        /// <returns></returns>
        public static int SaveErythropoietinInfo(ErythropoietinModel.MED_ERYTHROPOIETINDataTable erythropoietinDataTable)
        {
            return SaveData<ErythropoietinModel.MED_ERYTHROPOIETINDataTable>(erythropoietinDataTable);
        }
        /// <summary>
        /// MED_ERYTHROPOIETIN_EXECDataTable
        /// </summary>
        /// <param name="erythropoietinID"></param>
        /// <param name="beginExecDate"></param>
        /// <param name="endExecDate"></param>
        /// <returns></returns>
        public static ErythropoietinModel.MED_ERYTHROPOIETIN_EXECDataTable GetErythropoietinExecList(string erythropoietinID, DateTime beginExecDate, DateTime endExecDate)
        {
            ErythropoietinModel.MED_ERYTHROPOIETIN_EXECDataTable result = new ErythropoietinModel.MED_ERYTHROPOIETIN_EXECDataTable();
            DbParameter[] dbParams = new DbParameter[3];

            dbParams[0] = IDatabase.BuildDbParameter("ERYTHROPOIETIN_ID", DbType.String, erythropoietinID.Trim());
            dbParams[1] = IDatabase.BuildDbParameter("BEGINEXEC_DATE_TIME", DbType.String, beginExecDate);
            dbParams[2] = IDatabase.BuildDbParameter("ENDEXEC_DATE_TIME", DbType.String, endExecDate);

            return GetData<ErythropoietinModel.MED_ERYTHROPOIETIN_EXECDataTable>(result, "GetErythropoietinExecList", dbParams);
        }
        /// <summary>
        /// SaveErythropoietinExecInfo
        /// </summary>
        /// <param name="erythropoietinExecDataTable"></param>
        /// <returns></returns>
        public static int SaveErythropoietinExecInfo(ErythropoietinModel.MED_ERYTHROPOIETIN_EXECDataTable erythropoietinExecDataTable)
        {
            return SaveData<ErythropoietinModel.MED_ERYTHROPOIETIN_EXECDataTable>(erythropoietinExecDataTable);
        }
    }
}
