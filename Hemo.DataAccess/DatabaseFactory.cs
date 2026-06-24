/*----------------------------------------------------------------
      // Copyright (C) 2005 (苏州)医疗科技发展有限公司
      // 文件名：DatabaseFactory.cs
      // 文件功能描述：DatabaseFactory
      // 创建标识：顾伟伟-2011-01-14
 *
 * 修改时间:2026年4月15日
 * 修改人:刘建超
 * 修改描述:添加TNS连接oracle
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Medicalsystem.iMedical.Data.Database;


namespace Hemo.DataAccess
{
    public static class DatabaseFactory
    {
        #region 变量命名
        public const string defaultConnectionStringName = "hemo";
        public const string OracleClientProvider = "system.data.oracleclient";
        public const string SqlClientProvider = "system.data.sqlclient";
        public const string OleDbProvider = "system.data.oledb";
        public const string GlobalOracleProvicer = "system.data.globaloracleclient";
        public const string OracleManageDataAccessProvicer = "Oracle.ManagedDataAccess";
        public const string OracleTNSProvider = "tns";

        private static string _providerName = string.Empty;
        private static string _connectionString = string.Empty;


        public static string DefaultProviderName
        {
            get
            {
                return _providerName;
            }
        }
        #endregion

        #region 静态构造函数
        static DatabaseFactory()
        {
            if (string.IsNullOrEmpty(_providerName))
            {
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[DatabaseFactory.defaultConnectionStringName];
                _providerName = settings.ProviderName.ToLower();
                _connectionString = Cryptography.IsEncrypted(settings.ConnectionString) ? Cryptography.Decrypt(settings.ConnectionString) : settings.ConnectionString;

            }
        }
        #endregion

        /// <summary>
        /// 创建连接
        /// </summary>
        /// <returns></returns>
        public static IDatabase Create()
        {
            return Create(_connectionString, _providerName);
        }

        /// <summary>
        /// 创建连接（多态）
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        public static IDatabase Create(string connectionStringName)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[connectionStringName];
            return Create(settings.ConnectionString, settings.ProviderName.ToLower());
        }

        /// <summary>
        /// 根据不同的驱动类型加载数据库
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="providerName"></param>
        /// <returns></returns>
        public static IDatabase Create(string connectionString, string providerName)
        {
            switch (providerName)
            {
                case OracleClientProvider:
                    return new OracleDatabase(connectionString);

                case SqlClientProvider:
                    return new SqlDatabase(connectionString);

                case OleDbProvider:
                    return new OledbDatabase(connectionString);

                case GlobalOracleProvicer:
                    return new GlobalOracleDatabase(connectionString);

                case OracleManageDataAccessProvicer:
                    return new OracleManagerDatabase(connectionString);

                //case OracleTNSProvider:
                //    return new OracleTNSDatabase(connectionString);

                default:
                    throw new NotSupportedException(providerName);
            }

        }

    }
}
