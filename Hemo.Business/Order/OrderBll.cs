/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:订单类
 * 创建标识:贺建操-2013年7月27日
 * 
 * 修改时间:2013年12月12日
 * 修改人:顾伟伟
 * 修改描述:修改方法GetOrderList
 * 
 * 修改时间:2014年4月22日
 * 修改人:吕志强
 * 修改描述:修改方法GetOrderList4Erythropoietin
 * ----------------------------------------------------------------*/
using System;
using System.Data;
using System.Data.Common;
using Hemo.DataAccess;
using Hemo.Model;

namespace Hemo.Business.Order
{
    public class OrderBll : BaseClass
    {   
        /// <summary>
        ///获取 GetOrderList
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="beginEnterDate"></param>
        /// <param name="endEnterDate"></param>
        /// <returns></returns>
        public static OrderModel.MED_ORDERSDataTable GetOrderList(string patientID, DateTime beginEnterDate, DateTime endEnterDate)
        {
            OrderModel.MED_ORDERSDataTable result = new OrderModel.MED_ORDERSDataTable();
            DbParameter[] dbParams = new DbParameter[3];

            dbParams[0] = IDatabase.BuildDbParameter("PATIENT_ID", DbType.String, patientID.Trim());
            dbParams[1] = IDatabase.BuildDbParameter("BEGINENTER_DATE_TIME", DbType.String, beginEnterDate);
            dbParams[2] = IDatabase.BuildDbParameter("ENDENTER_DATE_TIME", DbType.String, endEnterDate);

            return GetData<OrderModel.MED_ORDERSDataTable>(result, "GetOrderList", dbParams);
        }

        /// <summary>
        /// 获取GetOrderList4Erythropoietin
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="beginEnterDate"></param>
        /// <param name="endEnterDate"></param>
        /// <returns></returns>
        public static OrderModel.MED_ORDERSDataTable GetOrderList4Erythropoietin(string patientID, DateTime beginEnterDate, DateTime endEnterDate)
        {
            OrderModel.MED_ORDERSDataTable result = new OrderModel.MED_ORDERSDataTable();
            DbParameter[] dbParams = new DbParameter[3];

            dbParams[0] = IDatabase.BuildDbParameter("PATIENT_ID", DbType.String, patientID.Trim());
            dbParams[1] = IDatabase.BuildDbParameter("BEGINENTER_DATE_TIME", DbType.String, beginEnterDate);
            dbParams[2] = IDatabase.BuildDbParameter("ENDENTER_DATE_TIME", DbType.String, endEnterDate);

            return GetData<OrderModel.MED_ORDERSDataTable>(result, "GetOrderList4Erythropoietin", dbParams);
        }

        /// <summary>
        /// 保存SaveDrugExecInfo
        /// </summary>
        /// <param name="drugExecDataTable"></param>
        /// <returns></returns>
        public static int SaveDrugExecInfo(OrderModel.MED_DRUG_EXECDataTable drugExecDataTable)
        {
            return SaveData<OrderModel.MED_DRUG_EXECDataTable>(drugExecDataTable);
        }
    }
}
