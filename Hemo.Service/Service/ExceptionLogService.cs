/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：错误日志实现
// 创建时间：2013-03-07
// 创建者：Frank
//  
// 修改时间：
// 修改人：
// 修改描述：
//
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hemo.IService;
using Hemo.Business.Manager;

namespace Hemo.Service
{
    /// <summary>
    /// 错误日志实现
    /// </summary>
    public class ExceptionLogService : MarshalByRefObject, IExceptionLogService
    {
        public void GetExceptionLog()
        {
            ExceptionLogManager exceptionLogManager = new ExceptionLogManager();
            exceptionLogManager.GetExceptionLog();
        }
    }
}
