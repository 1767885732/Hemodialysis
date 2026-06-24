/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：抽象工厂类。
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
using System.Configuration;
using Hemo.IService;
using System.Reflection;

namespace Hemo.Service
{
    /// <summary>
    /// AbstractServiceFactory
    /// </summary>
    public abstract class AbstractServiceFactory
    {

        #region 私有变量赋值
        private static readonly string servicePath = ConfigurationManager.AppSettings["ServicePath"];
        private static readonly string serviceFactoryClass = ConfigurationManager.AppSettings["ServiceFactory"];
        #endregion

        #region 构造函数
        public AbstractServiceFactory()
        {
        }
        #endregion

        /// <summary>
        /// 实现服务工厂接口
        /// </summary>
        /// <returns></returns>
        public IServiceFactory GetServiceFactory()
        {
            return GetServiceFactory(servicePath);
        }

        /// <summary>
        /// 实现服务工厂接口
        /// </summary>
        /// <param name="servicePath"></param>
        /// <returns></returns>
        public IServiceFactory GetServiceFactory(string servicePath)
        {
            string className = servicePath + "." + serviceFactoryClass;
            return (IServiceFactory)Assembly.Load(servicePath).CreateInstance(className);
        }

        /// <summary>
        /// 实现服务工厂接口
        /// </summary>
        /// <param name="servicePath"></param>
        /// <param name="serviceFactoryClass"></param>
        /// <returns></returns>
        public IServiceFactory GetServiceFactory(string servicePath, string serviceFactoryClass)
        {
            string className = servicePath + "." + serviceFactoryClass;
            return (IServiceFactory)Assembly.Load(servicePath).CreateInstance(className);
        }
    }
}
