/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:Hemo.Business
 * 创建标识:贺建操-2014年8月9日
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hemo.Utilities;
using Hemo.DataAccess;
using System.Data;
using System.Data.Common;

namespace Hemo.Business
{
    public class BaseClass
    {
        private static IDatabase _iDatabase = null;
        /// <summary>
        /// 单例模式
        /// </summary>
        public static IDatabase IDatabase
        {
            get
            {
                if (_iDatabase == null)
                {
                    _iDatabase = DatabaseFactory.Create();
                }
                return _iDatabase;
            }
        }

        ///// <summary>
        ///// 数据库连接字符串
        ///// </summary>
        //public static string ConnectionString {
        //    get {
        //        return IDatabase.ConnectionString;
        //    }
        //}
        /// <summary>
        /// 保存DataTable数据
        /// </summary>
        /// <param name="data">DataTable数据集</param>
        /// <returns></returns>
        public static int SaveData<T>(T data)
        {
            return SaveData<T>(data, null);
        }
        /// <summary>
        /// 保存DataTable数据With Transaction
        /// </summary>
        /// <param name="data">DataTable数据集</param>
        /// <returns></returns>
        public static int SaveData<T>(T data, DbWrapTransaction transaction)
        {
            var dt = data as DataTable;
            string tableName = dt.TableName.ToString();
            try
            {
                return IDatabase.Update((data as DataTable), transaction);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLogContet(tableName);
                Logger.WriteErrorLog(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 从数据查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="sqlKey"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static T GetData<T>(T data, string sqlKey, DbParameter[] parameters)
        {
            string errorContent = string.Format("SqlKey:{0}\r\nSqlContent :", sqlKey);
            try
            {
                string Sql = StoredScript.Get(sqlKey);
                errorContent += Sql;                
                if (IDatabase != null)
                {
                    IDatabase.Fill(Sql, data as DataTable, parameters);
                }
                Logger.WriteErrorLogContet("sql: "+errorContent+"||"+"DataTableRows:"+(data as DataTable).Rows.Count.ToString());
                return data;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLogContet(errorContent);

                Logger.WriteErrorLog(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sqlKey"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int DeleteData(string sqlKey, DbParameter[] parameters)
        {
            string errorContent = string.Format("SqlKey:{0}\r\nSqlContent :", sqlKey);
            try
            {
                int Result = -1;
                string Sql = StoredScript.Get(sqlKey);
                if (IDatabase != null)
                {
                    Result = IDatabase.ExecuteNonQuery(Sql, parameters);
                }
                return Result;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLogContet(errorContent);
                Logger.WriteErrorLog(ex);
                throw ex;
            }
        }


    }
}
