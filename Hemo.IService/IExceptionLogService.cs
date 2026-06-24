/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：错误日志处理定义。
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
using System.ServiceModel;

namespace Hemo.IService
{
    /// <summary>
    /// 错误日志处理定义
    /// </summary>
    [ServiceContract]
    public interface IExceptionLogService
    {
        [OperationContract]
        void GetExceptionLog();
    }
}
