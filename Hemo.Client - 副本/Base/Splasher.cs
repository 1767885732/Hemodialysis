/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:  Splasher加载框类
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/
using System;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

namespace Hemo.Client.Base
{
    public class Splasher
    {
        #region 变量
        
        private static Form m_SplashForm = null;
        private static ISplashForm m_SplashInterface = null;
        private static Thread m_SplashThread = null;
        private static string m_TempStatus = string.Empty;

        /// <summary>
        /// delegate for status change
        /// </summary>
        /// <param name="NewStatusInfo"></param>
        private delegate void SplashStatusChangedHandle(string NewStatusInfo);
        #endregion
        #region 方法
        
        /// <summary>
        /// Show the SplashForm
        /// </summary>
        public static void Show(Type splashFormType)
        {
            if (m_SplashThread != null)
                return;
            if (splashFormType == null)
            {
                throw (new Exception("splashFormType is null"));
            }

            m_SplashThread = new Thread(new ThreadStart(delegate()
            {
                CreateInstance(splashFormType);
                Application.Run(m_SplashForm);
            }));

            m_SplashThread.IsBackground = true;
            m_SplashThread.SetApartmentState(ApartmentState.STA);
            m_SplashThread.Start();
        }



        /// <summary>
        /// set the loading Status
        /// </summary>
        public static string Status
        {
            set
            {
                if (m_SplashInterface == null || m_SplashForm == null)
                {
                    m_TempStatus = value;
                    return;
                }
                m_SplashForm.Invoke(
                        new SplashStatusChangedHandle(delegate(string str) { m_SplashInterface.SetStatusInfo(str); }),
                        new object[] { value }
                    );
            }

        }

        /// <summary>
        /// Colse the SplashForm
        /// </summary>
        public static void Close()
        {
            if (m_SplashThread == null || m_SplashForm == null) return;

            try
            {
                m_SplashForm.Invoke(new MethodInvoker(m_SplashForm.Close));
            }
            catch (Exception)
            {
            }
            m_SplashThread = null;
            m_SplashForm = null;
        }
        /// <summary>
        /// 创建splash窗口
        /// </summary>
        /// <param name="FormType"></param>
        private static void CreateInstance(Type FormType)
        {

            object obj = FormType.InvokeMember(null,
                                BindingFlags.DeclaredOnly |
                                BindingFlags.Public | BindingFlags.NonPublic |
                                BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null);
            m_SplashForm = obj as Form;
            m_SplashInterface = obj as ISplashForm;
            if (m_SplashForm == null)
            {
                throw (new Exception("Splash Screen must inherit from System.Windows.Forms.Form"));
            }
            if (m_SplashInterface == null)
            {
                throw (new Exception("must implement interface ISplashForm"));
            }

            if (!string.IsNullOrEmpty(m_TempStatus))
                m_SplashInterface.SetStatusInfo(m_TempStatus);
        }
        #endregion


    }
}
