/*----------------------------------------------------------------
// Copyright (C) 2013 苏州麦迪斯顿医疗科技有限公司
// 描述：WCF服务工厂。
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
//using Hemo.IService;
using System.ServiceModel;

namespace Hemo.WCFClient
{
    public class ServiceFactory //: IServiceFactory
    {
        /// <summary>
        /// 初始化服务
        /// </summary>
        public void InitService()
        { }
        /// <summary>
        /// 创建错误日志服务
        /// </summary>
        /// <returns></returns>
        //public IExceptionLogService CreateExceptionLogService()
        //{
        //    ChannelFactory<IExceptionLogService> channelFactory = new ChannelFactory<IExceptionLogService>("Hemo.Service.ExceptionLogService");
        //    IExceptionLogService proxy = channelFactory.CreateChannel();
        //    return proxy;
        //}
    }
}
