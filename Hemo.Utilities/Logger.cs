/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:错误日志生成类
 * 创建标识:贺建操-2013年6月29日
 * ----------------------------------------------------------------*/
using System;
using System.Data;
using System.Configuration;
using System.IO;

namespace Hemo.Utilities {
    public static class Logger {
        /// <summary>
        /// 同步对象
        /// </summary>
        private static Object _syncObj = new Object();

        /// <summary>
        ///  将异常的有关情况记录至日志
        /// </summary>
        /// <param name="ex">异常类</param>
        public static void WriteErrorLog(Exception ex) {
            lock (_syncObj) {
                StreamWriter LogTxt;
                //string AppFilePath = HttpContext.Current.Server.MapPath("~") + "\\ErrorLog.txt";
                string AppFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\ErrorLog.txt";
                if (System.IO.File.Exists(AppFilePath)) {
                    LogTxt = File.AppendText(AppFilePath);
                }
                else {
                    LogTxt = File.CreateText(AppFilePath);
                }
                try {
                    LogTxt.WriteLine("异常发生时，服务器时间为 :\t" + DateTime.Now.ToString());
                    LogTxt.WriteLine("事件消息为:\t" + ex.Message);
                    LogTxt.WriteLine("异常发生时，调用堆栈上的桢的字符串表现形式:\t" + ex.StackTrace);
                    LogTxt.WriteLine("引发当前异常的函数为:\t" + ex.TargetSite.Name);
                    LogTxt.WriteLine("导致错误的应用程序或对象的名称为:\t" + ex.Source);
                    LogTxt.WriteLine();
                    LogTxt.WriteLine();
                }
                finally {
                    LogTxt.Close();
                }
            }
        }
        public static void WriteErrorLogContet(string error)
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
                    LogTxt.WriteLine("\t服务器时间为 :\t" + DateTime.Now.ToString());
                    LogTxt.WriteLine("执行Sql:\t" + error);

                    LogTxt.WriteLine();
                    LogTxt.WriteLine();
                }
                finally
                {
                    LogTxt.Close();
                }
            }
        }

        public static void WriteInfoLog(string message)
        {
            lock (_syncObj)
            {
                StreamWriter LogTxt;
                string AppFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\info.txt";
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
                    LogTxt.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + message);
                }
                finally
                {
                    LogTxt.Close();
                }
            }
        }
    }
}
