/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:异常处理类
 * 创建标识:贺建操-2013年8月2日
 * 
 * 修改时间:2013年12月18日
 * 修改人:顾伟伟
 * 修改描述:修改方法WriteErrorLog
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Hemo.Utilities;
using System.Diagnostics;

namespace Hemo.Business.Manager
{
    public class ExceptionManager
    {
        /// <summary>
        /// 记录异常情况
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="Exception">异常</param>
        /// <returns></returns>
        public static void LogException(BaseUserInfo userInfo, Exception ex)
        {
            // 系统里应该可以配置是否记录异常现象
            if (!BaseSystemInfo.LogException)
            {
                return;
            }

            // Windows系统异常中
            if (BaseSystemInfo.EventLog)
            {
                if (!System.Diagnostics.EventLog.SourceExists(BaseSystemInfo.SoftName))
                {
                    System.Diagnostics.EventLog.CreateEventSource(BaseSystemInfo.SoftName, BaseSystemInfo.SoftFullName);
                }
                System.Diagnostics.EventLog eventLog = new System.Diagnostics.EventLog();
                eventLog.Source = BaseSystemInfo.SoftName;
                eventLog.WriteEntry(ex.Message, EventLogEntryType.Error);
            }

            //是否记录在错误日志文件中
            if (BaseSystemInfo.FileLog)
            {
                WriteErrorLog(ex);
            }

        }


        /// <summary>
        /// 同步对象
        /// </summary>
        private static Object _syncObj = new Object();

        /// <summary>
        ///  将异常的有关情况记录至日志
        /// </summary>
        /// <param name="ex">异常类</param>
        private static void WriteErrorLog(Exception ex)
        {
            lock (_syncObj)
            {
                StreamWriter LogTxt;
                //string AppFilePath = HttpContext.Current.Server.MapPath("~") + "\\ErrorLog.txt";
                string AppFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\ErrorLog.txt";
                if (System.IO.File.Exists(AppFilePath))
                {
                    LogTxt = File.AppendText(AppFilePath);
                }
                else
                {
                    LogTxt = File.CreateText(AppFilePath);
                }
                try
                {
                    LogTxt.WriteLine("异常发生时，服务器时间为 :\t" + DateTime.Now.ToString());
                    LogTxt.WriteLine("事件消息为:\t" + ex.Message);
                    if (ex.StackTrace != null)
                    {
                        LogTxt.WriteLine("异常发生时，调用堆栈上的桢的字符串表现形式:\t" + ex.StackTrace);
                    }
                    if (ex.TargetSite != null && !string.IsNullOrEmpty(ex.TargetSite.Name))
                    {
                        LogTxt.WriteLine("引发当前异常的函数为:\t" + ex.TargetSite.Name);
                    }
                    if (ex.Source != null)
                    {
                        LogTxt.WriteLine("导致错误的应用程序或对象的名称为:\t" + ex.Source);
                    }
                    LogTxt.WriteLine();
                    LogTxt.WriteLine();
                }
                finally
                {
                    LogTxt.Close();
                }
            }
        }
    }
}
