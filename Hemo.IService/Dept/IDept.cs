/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:IDept
 * 创建标识:贺建操-2014年8月9日
 * ----------------------------------------------------------------*/
using System.ServiceModel;
using Hemo.Model;

namespace Hemo.IService.Dept
{
    [ServiceContract]
    public interface IDept
    {
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DeptModel.MED_DEPARTMENTDataTable GetDeptList();
    }
}
