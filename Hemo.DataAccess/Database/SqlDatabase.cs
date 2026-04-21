/*----------------------------------------------------------------
      // Copyright (C) 2005 (苏州)医疗科技发展有限公司
      // 文件名：SqlDatabase.cs
      // 文件功能描述：SqlDatabase
      // 创建标识：顾伟伟-2011-01-14
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Hemo.DataAccess
{
   public class SqlDatabase:IDatabase
   {

       #region 私有变量
       private string _connectionString = string.Empty;
       #endregion

       #region 构造函数
       public SqlDatabase(string connectionString)
       {
           if (string.IsNullOrEmpty(connectionString))
               throw new ArgumentNullException("连接字符串不能为空");

           _connectionString = connectionString;
       }
       #endregion


       #region 事件
       /// <summary>
       /// 根据指定的查询语句，将数据填充到数据表中
       /// </summary>
       /// <param name="sql">查询语句</param>
       /// <param name="data">数据表</param>
       /// <returns>数据表</returns>
       public DataTable Fill(string sql, DataTable data)
        {
            return Fill(sql, data, null);
        }
        /// <summary>
        /// 根据指定的查询语句，参数,将数据填充到数据表中
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="data">数据表</param>
        /// <param name="parameters">参数</param>
        /// <returns>数据表</returns>
       public DataTable Fill(string sql, DataTable data, DbParameter[] parameters)
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, _connectionString))
            {
                if (parameters != null)
                {
                    adapter.SelectCommand.Parameters.AddRange(parameters);
                }
                adapter.Fill(data);

            }
            return data;
        }

        public DataSet Fill(string sql, DataSet dataSet, string tableName)
        {
            return Fill(sql, null, dataSet, tableName);
        }

        public DataSet Fill(string sql, DbParameter[] parameters, DataSet dataSet, string tableName)
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, _connectionString))
            {
                if (parameters != null)
                {
                    adapter.SelectCommand.Parameters.AddRange(parameters);
                }
                adapter.Fill(dataSet, tableName);
                return dataSet;
            }
        }
        /// <summary>
        /// 执行指定的SQL语句，并返回受影响的行数
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(string sql)
        {
            return ExecuteNonQuery(sql, null,null);
        }
        /// <summary>
        /// 执行指定的SQL语句，并返回受影响的行数
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parameter">参数</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(string sql, DbParameter[] parameter)
        {
            return ExecuteNonQuery(sql, parameter, null);
        }
       
        /// <summary>
        /// 保存数据,并自动调用相应的Insert,Update,Delete语句
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Update(DataTable data, string tableName)
        {
            return Update(data, tableName, null);
           
        }
        /// <summary>
        /// 保存数据,并自动调用相应的Insert,Update,Delete语句
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Update(DataTable data)
        {
            return Update(data, data.TableName);
        }
        /// <summary>
        /// 执行指定的查询，并返回第一行，第一列的结果
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <returns>结果</returns>
       public object ExecuteScalar(string sql)
        {
            return ExecuteScalar(sql, null);
        }
        /// <summary>
        /// 执行指定的查询，并返回结果中的第一行，第一列数据
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parameters">参数</param>
        /// <returns>结果</returns>
       public object ExecuteScalar(string sql, DbParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.Text;
                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                object result = command.ExecuteScalar();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        /// <summary>
        /// 生成查询参数
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public DbParameter BuildDbParameter(string parameterName, DbType dbType, object value)
        {
            SqlParameter parameter = new SqlParameter(string.Format(@"@{0}", parameterName), dbType);
            parameter.Value = value;
            return parameter;
        }

        /// <summary>
        ///  执行指定的SQL语句，并返回受影响的行数,并添加事务
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, DbWrapTransaction transaction)
        {
            return ExecuteNonQuery(sql, null, transaction);
        }
        /// <summary>
        ///  执行指定的SQL语句，并返回受影响的行数,并添加事务
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameter"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, DbParameter[] parameter, DbWrapTransaction transaction)
        {
            SqlConnection connection = transaction == null ? new SqlConnection(_connectionString) : transaction.InnerDbConnection as SqlConnection;
            try
            {
                if(connection.State!=ConnectionState.Open)
                   connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.Text;
                if (parameter != null)
                    command.Parameters.AddRange(parameter);
                if (transaction != null)
                    command.Transaction = transaction.InnerDbTransaction as SqlTransaction;
                int i = command.ExecuteNonQuery();
                return i;
            }
            catch (Exception e)
            {
                throw e; 
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    connection.Dispose();
                }
               
            }
        }
        /// <summary>
        /// 保存数据,并自动调用相应的Insert,Update,Delete语句,并支持事务
        /// </summary>
        /// <param name="data"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public int Update(DataTable data, DbWrapTransaction transaction)
        {
            return Update(data, data.TableName, transaction);
        }
        /// <summary>
        /// 保存数据,并自动调用相应的Insert,Update,Delete语句,并支持事务
        /// </summary>
        /// <param name="data"></param>
        /// <param name="tableName"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public int Update(DataTable data, string tableName, DbWrapTransaction transaction)
        {
            string sql = string.Format("select * from {0} where 1=2", tableName);
            return Update(sql, data, transaction); 
          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql">包含列的SQL语句</param>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Update(string sql, DataTable data)
        {


            SqlDataAdapter adapter = new SqlDataAdapter(sql, _connectionString);

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

            commandBuilder.ConflictOption = ConflictOption.OverwriteChanges;
            
            try
            {
                int i = commandBuilder.DataAdapter.Update(data);
                return i;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
               
                 adapter.Dispose();
                
            }
           
        }
       /// <summary>
       /// 创建一个事务
       /// </summary>
       /// <returns></returns>
        public DbWrapTransaction CreateDbTransaction()
        {
            
            DbWrapTransaction dbWrapTransaction = new DbWrapTransaction(new SqlConnection(_connectionString));

            return dbWrapTransaction;

        }

       /// <summary>
       /// 测试数据库连接是否成功
       /// </summary>
       /// <param name="strConn">连接字符串</param>
       /// <returns></returns>
       public bool TestDbConnection(string strConn)
       {
               SqlConnection connection = new SqlConnection(strConn);
               try
               {
                   connection.Open();

                   return true;
               }
               catch(Exception exc)
               {
                   return false;
               }
               finally
               {
                    connection.Close();
                   connection.Dispose();
               }
       }
       #endregion
       #region IDatabase 成员


       public int Update(string sql, DataTable data, DbWrapTransaction transaction)
       {
         
           SqlDataAdapter adapter = transaction == null ? new SqlDataAdapter(sql, _connectionString) : new SqlDataAdapter(sql, transaction.InnerDbConnection as SqlConnection);

           SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

           commandBuilder.ConflictOption = ConflictOption.OverwriteChanges;
           if (transaction != null)
           {
               SqlTransaction sqlTransaction = transaction.InnerDbTransaction as SqlTransaction;
               if (commandBuilder.DataAdapter.InsertCommand != null)
                   commandBuilder.DataAdapter.InsertCommand.Transaction = sqlTransaction;
               if (commandBuilder.DataAdapter.DeleteCommand != null)
                   commandBuilder.DataAdapter.DeleteCommand.Transaction = sqlTransaction;
               if (commandBuilder.DataAdapter.UpdateCommand != null)
                   commandBuilder.DataAdapter.UpdateCommand.Transaction = sqlTransaction;
               if (commandBuilder.DataAdapter.SelectCommand != null)
                   commandBuilder.DataAdapter.SelectCommand.Transaction = sqlTransaction;
           }
           try
           {
               int i = commandBuilder.DataAdapter.Update(data);
               return i;
           }
           catch (Exception e)
           {
               throw e;
           }
           finally
           {
               if (transaction == null)
               {
                   adapter.Dispose();
               }
           }
           
                
       }

       #endregion
    }
}
