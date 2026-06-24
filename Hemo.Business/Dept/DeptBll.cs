/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:Hemo.Business.Dept
 * 创建标识:贺建操-2013年8月2日
 * ----------------------------------------------------------------*/
using Hemo.Model;

namespace Hemo.Business.Dept
{
    public class DeptBll : BaseClass
    {
        /// <summary>
        /// 获取MED_DEPARTMENT
        /// </summary>
        /// <returns></returns>
        public static DeptModel.MED_DEPARTMENTDataTable GetDeptList()
        {
            DeptModel.MED_DEPARTMENTDataTable result = new DeptModel.MED_DEPARTMENTDataTable();

            return GetData<DeptModel.MED_DEPARTMENTDataTable>(result, "GetDeptList", null);
        }
    }
}
