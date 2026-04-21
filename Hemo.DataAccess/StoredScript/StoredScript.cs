/*----------------------------------------------------------------
      // Copyright (C) 2005 (苏州)医疗科技发展有限公司
      // 文件名：StoredScript.cs
      // 文件功能描述：StoredScript
      // 创建标识：顾伟伟-2011-01-14
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Hemo.DataAccess
{
    public class StoredScript
    {
        public static Dictionary<string, string> _keySql = new Dictionary<string, string>();

        /// <summary>
        /// 根据ProviderName初使化SQL字典
        /// </summary>
        /// <param name="type"></param>
        static StoredScript()
        {
            switch (DatabaseFactory.DefaultProviderName)
            {
                case DatabaseFactory.OleDbProvider:
                    PopulateScriptFromProvider(typeof(OleDbScriptProvider));
                    break;
                case DatabaseFactory.OracleClientProvider:
                case DatabaseFactory.GlobalOracleProvicer:
                case DatabaseFactory.OracleTNSProvider:
                    PopulateScriptFromProvider(typeof(OracleScriptProvider));
                    break;
                case DatabaseFactory.SqlClientProvider:
                    PopulateScriptFromProvider(typeof(SqlScriptProvider));
                    break;
                default:
                    throw new NotSupportedException(DatabaseFactory.DefaultProviderName);
            }
            //if (DatabaseFactory.DefaultProviderName == DatabaseFactory.OleDbProvider)
            //    PopulateScriptFromProvider(typeof(OleDbScriptProvider));
            //else if (DatabaseFactory.DefaultProviderName == DatabaseFactory.OracleClientProvider)
            //    PopulateScriptFromProvider(typeof(OracleScriptProvider));
            //else if (DatabaseFactory.DefaultProviderName == DatabaseFactory.SqlClientProvider)
            //    PopulateScriptFromProvider(typeof(SqlScriptProvider));
            //else if (DatabaseFactory.DefaultProviderName == DatabaseFactory.GlobalOracleProvicer)
            //    PopulateScriptFromProvider(typeof(OracleScriptProvider));
            
            //else
            //    throw new NotSupportedException(DatabaseFactory.DefaultProviderName);
        }

        /// <summary>
        /// 初使化SQL字典
        /// </summary>
        /// <param name="type"></param>
        private static void PopulateScriptFromProvider(Type type)
        {
            PropertyInfo[] propertys = type.GetProperties();
            object instance = Activator.CreateInstance(type);
            foreach (PropertyInfo prop in propertys)
            {
                object propValue = prop.GetValue(instance, null);
                if (propValue != null)
                    _keySql.Add(prop.Name, propValue.ToString());
            }
        }

        /// <summary>
        /// 根据key值,返回对应的SQL语句
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Get(string key)
        {
            if (_keySql.ContainsKey(key))
                return _keySql[key];
            else
                throw new KeyNotFoundException(string.Format("找不到名为'{0}'的键", key));
        }
    }
}