/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:继承IDisposable接口实现事务级数据库释放
 * 创建标识:吕志强-2011年8月2日
 * 
 * 修改时间:2011年12月18日
 * 修改人:贺建操
 * 修改描述:修改方法DbWrapTransaction
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace Hemo.DataAccess
{
   public class DbWrapTransaction:IDisposable
   {
       #region 变量
       
       private DbTransaction _dbTransaction;
       private DbConnection _dbConnection;
   


       public DbTransaction InnerDbTransaction
       {
           get
           {
               return _dbTransaction;
           }
           
       }
       public DbConnection InnerDbConnection
       {
           get
           {
               return _dbConnection;
           }
       }

       #endregion

       #region 构造函数
       public DbWrapTransaction(DbConnection connection)
       {
           _dbConnection = connection;
           _dbConnection.Open();
           _dbTransaction= _dbConnection.BeginTransaction();
       }

       #endregion

       /// <summary>
       /// 回滚
       /// </summary>
       public void Rollback()
       {
           if (_dbTransaction != null)
              _dbTransaction.Rollback();
       }

       /// <summary>
       /// 提交
       /// </summary>
       public void Commit()
       {
           if(_dbTransaction!=null)
              _dbTransaction.Commit();
           
       }

       /// <summary>
       /// 释放
       /// </summary>
       public void Dispose()
       {
           if (_dbConnection != null)
           {
               _dbConnection.Close();
               _dbConnection.Dispose();
           }
           if(_dbTransaction!=null)
               _dbTransaction.Dispose();
       }

      
    }
}
