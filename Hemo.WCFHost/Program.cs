using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using System.Reflection;
using System.ServiceModel.Configuration;
using System.ServiceModel;

namespace Hemo.WCFHost
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            System.Console.WriteLine("WCFHost 服务器端已启动。。。");

            // 读取配置文件
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().Location);
            ServiceModelSectionGroup serviceModelSectionGroup = (ServiceModelSectionGroup)configuration.GetSectionGroup("system.serviceModel");
            // 开启每个服务
            foreach (ServiceElement serviceElement in serviceModelSectionGroup.Services.Services)
            {
                var serviceHost = new ServiceHost(Assembly.Load("Hemo.Service").GetType(serviceElement.Name), serviceElement.Endpoints[0].Address);
                serviceHost.Opened += delegate { Console.WriteLine("{0}", serviceHost.BaseAddresses[0]); };
                serviceHost.Open();
            }

            System.Console.WriteLine("服务器 当前时间：" + DateTime.Now.ToString());//BaseSystemInfo.DateTimeFormat
            //IDbHelper dbHelper = DbHelperFactory.GetHelper();
            //System.Console.WriteLine("数据库服务器 当前时间：" + dbHelper.GetDBDateTime());

            Application.Run(new Form1());
        }
    }
}
