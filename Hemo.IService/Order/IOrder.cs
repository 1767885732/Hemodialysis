/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:订单接口文件
 * 创建标识:吕志强-2013年7月5日
 * 
 * 修改时间:2013年10月13日
 * 修改人:贺建操
 * 修改描述:修改方法SaveDrugExecInfo
 * ----------------------------------------------------------------*/
using System;
using System.ServiceModel;
using Hemo.Model;

namespace Hemo.IService.Order
{
    [ServiceContract]
    public interface IOrder
    {
        #region 订单接口文件

        [OperationContract]
        OrderModel.MED_ORDERSDataTable GetOrderList(string patientID, DateTime beginEnterDate, DateTime endEnterDate);

        [OperationContract]
        OrderModel.MED_ORDERSDataTable GetOrderList4Erythropoietin(string patientID, DateTime beginEnterDate, DateTime endEnterDate);

        [OperationContract]
        int SaveDrugExecInfo(OrderModel.MED_DRUG_EXECDataTable drugExecDataTable);
        #endregion
    }
}
