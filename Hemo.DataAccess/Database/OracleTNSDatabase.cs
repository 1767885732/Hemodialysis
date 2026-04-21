/*----------------------------------------------------------------
      // Copyright (C) 2005 (苏州)医疗科技发展有限公司
      // 文件名：OracleTNSDatabase.cs
      // 文件功能描述：OracleManagerDatabase
      // 创建标识：刘建超-2026-04-15
----------------------------------------------------------------*/
using Hemo.DataAccess;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Medicalsystem.iMedical.Data.Database
{
    public class OracleTNSDatabase : IDatabase
    {
        #region 私有变量
        private string _connectionString = string.Empty;
        #endregion

        #region 构造函数
        public OracleTNSDatabase(string connectionString)
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
            using (OracleDataAdapter adapter = new OracleDataAdapter(sql, _connectionString))
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
            using (OracleDataAdapter adapter = new OracleDataAdapter(sql, _connectionString))
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
            return ExecuteNonQuery(sql, null, null);
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
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                using (OracleCommand command = new OracleCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);
                    return command.ExecuteScalar();
                }
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

            OracleParameter parameter = new OracleParameter($":{parameterName}", ConvertDbTypeToOracleDbType(dbType));
            parameter.Value = value ?? DBNull.Value;  // 处理 null 值
            return parameter;
        }

        /// <summary>
        /// 执行指定的SQL语句，并返回受影响的行数,并添加事务
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, DbWrapTransaction transaction)
        {
            return ExecuteNonQuery(sql, null, transaction);
        }

        /// <summary>
        /// 执行指定的SQL语句，并返回受影响的行数,并添加事务
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameter"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, DbParameter[] parameter, DbWrapTransaction transaction)
        {
            using (OracleConnection connection = transaction == null ? new OracleConnection(_connectionString) : (transaction.InnerDbConnection as OracleConnection))
            {
                bool needDisposeConnection = transaction == null;

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                using (OracleCommand command = new OracleCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    if (parameter != null)
                        command.Parameters.AddRange(parameter);
                    if (transaction != null)
                        command.Transaction = transaction.InnerDbTransaction as OracleTransaction;

                    return command.ExecuteNonQuery();
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
            string sql = $"select * from {tableName} where 1=2";
            return Update(sql, data, transaction);
        }

        /// <summary>
        /// 使用 SQL 语句更新数据表
        /// </summary>
        /// <param name="sql">包含列的SQL语句</param>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Update(string sql, DataTable data)
        {
            using (OracleDataAdapter adapter = new OracleDataAdapter(sql, _connectionString))
            {
                OracleCommandBuilder commandBuilder = new OracleCommandBuilder(adapter);
                commandBuilder.ConflictOption = ConflictOption.OverwriteChanges;
                commandBuilder.DataAdapter.UpdateBatchSize = 1;

                return commandBuilder.DataAdapter.Update(data);
            }
        }

        /// <summary>
        /// 创建一个事务
        /// </summary>
        /// <returns></returns>
        public DbWrapTransaction CreateDbTransaction()
        {
            DbWrapTransaction dbWrapTransaction = new DbWrapTransaction(new OracleConnection(_connectionString));
            return dbWrapTransaction;
        }

        /// <summary>
        /// 测试数据库连接是否成功
        /// </summary>
        /// <param name="strConn">连接字符串</param>
        /// <returns></returns>
        public bool TestDbConnection(string strConn)
        {
            using (OracleConnection connection = new OracleConnection(strConn))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        #endregion

        #region IDatabase 成员

        public int Update(string sql, DataTable data, DbWrapTransaction transaction)
        {
            using (OracleDataAdapter adapter = transaction == null
                ? new OracleDataAdapter(sql, _connectionString)
                : new OracleDataAdapter(sql, transaction.InnerDbConnection as OracleConnection))
            {
                OracleCommandBuilder commandBuilder = new OracleCommandBuilder(adapter);
                commandBuilder.DataAdapter.UpdateBatchSize = 1;
                commandBuilder.ConflictOption = ConflictOption.OverwriteChanges;

                if (transaction != null)
                {
                    OracleTransaction oracleTransaction = transaction.InnerDbTransaction as OracleTransaction;
                    if (commandBuilder.DataAdapter.InsertCommand != null)
                        commandBuilder.DataAdapter.InsertCommand.Transaction = oracleTransaction;
                    if (commandBuilder.DataAdapter.DeleteCommand != null)
                        commandBuilder.DataAdapter.DeleteCommand.Transaction = oracleTransaction;
                    if (commandBuilder.DataAdapter.UpdateCommand != null)
                        commandBuilder.DataAdapter.UpdateCommand.Transaction = oracleTransaction;
                    if (commandBuilder.DataAdapter.SelectCommand != null)
                        commandBuilder.DataAdapter.SelectCommand.Transaction = oracleTransaction;
                }

                return commandBuilder.DataAdapter.Update(data);
            }
        }

        #endregion


        /// <summary>
        /// 将 System.Data.DbType 转换为 OracleDbType
        /// </summary>
        private OracleDbType ConvertDbTypeToOracleDbType(DbType dbType)
        {
            switch (dbType)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.String:
                case DbType.StringFixedLength:
                    return OracleDbType.Varchar2;

                case DbType.Int16:
                    return OracleDbType.Int16;
                case DbType.Int32:
                    return OracleDbType.Int32;
                case DbType.Int64:
                    return OracleDbType.Int64;

                case DbType.Decimal:
                    return OracleDbType.Decimal;
                case DbType.Double:
                    return OracleDbType.Double;
                case DbType.Single:
                    return OracleDbType.Single;

                case DbType.DateTime:
                    return OracleDbType.Date;
                case DbType.DateTime2:
                    return OracleDbType.TimeStamp;

                case DbType.Boolean:
                    return OracleDbType.Int16;

                case DbType.Binary:
                    return OracleDbType.Blob;

                case DbType.Guid:
                    return OracleDbType.Raw;

                case DbType.Byte:
                    return OracleDbType.Byte;

                default:
                    return OracleDbType.Varchar2;
            }
        }
    }
}