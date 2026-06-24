/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:OrderService服务类
 * 创建标识:贺建操-2013年6月29日
 * ----------------------------------------------------------------*/
using System;
using Hemo.Business.Order;
using Hemo.IService.Order;
using Hemo.Model;

namespace Hemo.Service.Order
{
    public class OrderService : MarshalByRefObject, IOrder
    {
        #region IOrder 成员

        public OrderModel.MED_ORDERSDataTable GetOrderList(string patientID, DateTime beginEnterDate, DateTime endEnterDate)
        {
            return OrderBll.GetOrderList(patientID, beginEnterDate, endEnterDate);
        }

        public OrderModel.MED_ORDERSDataTable GetOrderList4Erythropoietin(string patientID, DateTime beginEnterDate, DateTime endEnterDate)
        {
            return OrderBll.GetOrderList4Erythropoietin(patientID, beginEnterDate, endEnterDate);
        }

        public int SaveDrugExecInfo(OrderModel.MED_DRUG_EXECDataTable drugExecDataTable)
        {
            return OrderBll.SaveDrugExecInfo(drugExecDataTable);
        }

        #endregion
    }
}
