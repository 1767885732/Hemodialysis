/*----------------------------------------------------------------
      // Copyright (C) 2005 (苏州)医疗科技发展有限公司
      // 文件名：IDatabase.cs
      // 文件功能描述：IDatabase
      // 创建标识：顾伟伟-2011-01-14
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Hemo.DataAccess
{
    #region 定义数据库操作方法接口
    
    /// <summary>
    /// 定义数据库操作方法接口
    /// </summary>
    public interface IDatabase
    {
        #region 数据库基本操作方法
        
        DataTable Fill(string sql, DataTable data);

        DataTable Fill(string sql, DataTable data, DbParameter[] parameters);

        DataSet Fill(string sql, DataSet dataSet, string tableName);

        DataSet Fill(string sql, DbParameter[] parameters, DataSet dataSet, string tableName);

        int ExecuteNonQuery(string sql);

        int ExecuteNonQuery(string sql, DbWrapTransaction transaction);

        int ExecuteNonQuery(string sql, DbParameter[] parameter);

        int ExecuteNonQuery(string sql, DbParameter[] parameter, DbWrapTransaction transaction);

        int Update(DataTable data);

        int Update(DataTable data, string tableName);

        int Update(DataTable data, DbWrapTransaction transaction);

        int Update(DataTable data, string tableName, DbWrapTransaction transaction);

        int Update(string sql, DataTable data);
        int Update(string sql, DataTable data, DbWrapTransaction transaction);

        object ExecuteScalar(string sql);

        object ExecuteScalar(string sql, DbParameter[] parameters);

        DbParameter BuildDbParameter(string parameterName, DbType dbType, object value);

        DbWrapTransaction CreateDbTransaction();

        bool TestDbConnection(string strConn);
        #endregion
    }

    #endregion

    #region 一个使用UTF8和Base64String的简单加密/解密类

    /// <summary>
    /// 一个使用UTF8和Base64String的简单加密/解密类
    /// </summary>
    public class Cryptography
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="source">要加密的字符串</param>
        /// <returns></returns>
        public static string Encrypt(string source)
        {
            if (IsEncrypted(source))
                return source;
            byte[] bytes = UTF8Encoding.Default.GetBytes(source);
            return string.Format("{0}{1}{2}", "-START-", Convert.ToBase64String(bytes), "-END-").Trim();
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="source">要解密的字符串</param>
        /// <returns></returns>
        public static string Decrypt(string source)
        {
            if (!IsEncrypted(source))
                return source;
            source = source.Replace("-START-", string.Empty).Replace("-END-", string.Empty);
            var bytes = Convert.FromBase64String(source);
            return UTF8Encoding.Default.GetString(bytes);
        }
        /// <summary>
        /// 判断字符串是否已经被Medicalsystem.Docare.Common.Cryptography加密
        /// </summary>
        /// <param name="source">字符串</param>
        /// <returns></returns>
        public static bool IsEncrypted(string source)
        {
            if (source.StartsWith("-START-") && source.EndsWith("-END-"))
                return true;
            return false;
        }
    }
    #endregion
}
