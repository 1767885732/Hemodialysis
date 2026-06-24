/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:DeptService
 * 创建标识:贺建操-2014年8月9日
 * ----------------------------------------------------------------*/
using System;
using Hemo.Business.Dept;
using Hemo.IService.Dept;
using Hemo.Model;

namespace Hemo.Service.Dept
{
    public class DeptService : MarshalByRefObject, IDept
    {
        #region IDept 成员

        public DeptModel.MED_DEPARTMENTDataTable GetDeptList()
        {
            return DeptBll.GetDeptList();
        }

        #endregion
    }
}
