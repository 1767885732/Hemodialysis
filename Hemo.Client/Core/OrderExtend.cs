/*----------------------------------------------------------------
* Copyright (C) 2005 医疗科技发展有限公司
* 文件功能描述: 根据医嘱 号显示医嘱号类
* 创建标识:贺建操-2017年1月30日
* ----------------------------------------------------------------*/
using Hemo.Model;

namespace Hemo.Client.Core
{
    public static class OrderExtend
    {
        /// <summary>
        /// 设置医嘱的组号
        /// </summary>
        /// <param name="orderDataTable"></param>
        public static void SetOrderGroupSign(this OrderModel.MED_ORDERSDataTable orderDataTable)
        {
            foreach (OrderModel.MED_ORDERSRow row in orderDataTable.Rows)
            {
                string groupSign = string.Empty;
                OrderModel.MED_ORDERSRow[] rows = orderDataTable.Select(string.Format("ORDER_NO = '{0}'", row.ORDER_NO)) as OrderModel.MED_ORDERSRow[];

                if (rows.Length > 1)
                {
                    int rowIndex = orderDataTable.Rows.IndexOf(row);

                    if (rowIndex == 0)
                        groupSign = "┌";
                    else if (rowIndex == orderDataTable.Rows.Count - 1)
                        groupSign = "└";
                    else
                    {
                        if (string.Compare(orderDataTable.Rows[rowIndex - 1]["ORDER_NO"].ToString(), row.ORDER_NO, true) != 0)
                            groupSign = "┌";
                        else if (string.Compare(orderDataTable.Rows[rowIndex + 1]["ORDER_NO"].ToString(), row.ORDER_NO, true) != 0)
                            groupSign = "└";
                        else
                            groupSign = "├";
                    }
                }

                row.ORDER_TEXT = string.Format("{0}{1}", groupSign, row.ORDER_TEXT);
            }
        }
    }
}
