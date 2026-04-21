using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Hemo.Utilities
{
    /// <summary>
    /// 添加消息存档，目前直接存放在text里面
    /// </summary>
    public class MessageLog
    {
        private Queue<LogEntity> _q = null;

        private static MessageLog m = null;

        private string _filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Log\\WebLog.txt";

        private Action<Queue<LogEntity>> _act = null;

        private static readonly object _locker = new object();


        public static MessageLog Instance()
        {

            if (m == null)
            {
                lock (_locker)
                { m = new MessageLog(); }

            }
            return m;
        }

        public MessageLog()
        {
            _act = new Action<Queue<LogEntity>>(Write);
        }

        public void Log(LogEntity entity)
        {
            Save(entity);
        }

        protected void Save(LogEntity entity)
        {
            if (_q == null)
            {
                _q = new Queue<LogEntity>();
            }
            _q.Enqueue(entity);
            _act(_q);
        }

        protected void Write(Queue<LogEntity> q)
        {
            if (q != null && q.Count > 0)
            {
                lock (_locker)
                {
                    string directory = AppDomain.CurrentDomain.BaseDirectory + "\\Log";
                    if (!System.IO.Directory.Exists(directory))
                    {
                        System.IO.Directory.CreateDirectory(directory);
                    }
                    if (!System.IO.File.Exists(_filePath))
                    {
                        using (File.CreateText(_filePath)) { }
                    }
                    foreach (LogEntity e in q)
                    {
                        FileStream fs = new FileStream(_filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                        StreamWriter sw = new StreamWriter(fs);
                        sw.WriteLine("---------" + System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "----------");
                        sw.WriteLine("类型:" + e.Type);
                        sw.WriteLine("时间:" + e.LogDate);
                        sw.WriteLine("IP地址:" + e.Ip);
                        sw.WriteLine("内容:" + e.Content);
                        sw.WriteLine("--------------------------------------------");
                        sw.Flush();
                        sw.Close();
                        fs.Close();
                    }
                }
            }
        }
    }

    public class LogEntity
    {
        public string Type { get; set; }
        public DateTime LogDate { get; set; }

        public string Ip { get; set; }
        public string Content { get; set; }

    }
}