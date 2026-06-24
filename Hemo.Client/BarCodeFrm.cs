/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：扫码卡号处理窗体
// 创建时间：2015-06-04
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Hemo.Client
{
    public partial class BarCodeFrm : Form
    {
        #region 类变量

        BardCodeHooK BarCode = new BardCodeHooK();

        private delegate void ShowInfoDelegate(BardCodeHooK.BarCodes barCode);

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public BarCodeFrm()
        {
            InitializeComponent();
            BarCode.BarCodeEvent += new BardCodeHooK.BardCodeDeletegate(BarCode_BarCodeEvent);
        }

        #endregion

        #region 事件

        void BarCode_BarCodeEvent(BardCodeHooK.BarCodes barCode)
        {
            ShowInfo(barCode);
        }

        private void BarCodeFrm_Load(object sender, EventArgs e)
        {
            try
            {
                //如果不是默认的COM1端口，这里需要传入端口号
                //rsp = new RevSerialPort("COM1");
                //rsp.Start();
                ////usb
                BarCode.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region 方法

        private void ShowInfo(BardCodeHooK.BarCodes barCode)
        {
            if (barCode.IsValid)
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new ShowInfoDelegate(ShowInfo), new object[] { barCode });
                }
                else
                {
                    lblBarCode.Text = barCode.BarCode;
                    //richTextBarCode.AppendText(barCode.BarCode+"\r\n");
                    //richTextBarCode.AppendText(string.Format("KeyName:{0}VirtKey:{1}ScanCode:{2}AscII:{3}Chr:{4}IsValid:{5}\r\n", barCode.KeyName, barCode.VirtKey,
                    //   barCode.ScanCode, barCode.Ascll, barCode.Chr, barCode.IsValid));
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// 获取键盘输入或者USB扫描枪数据 可以是没有焦点 应为使用的是全局钩子
    /// USB扫描枪 是模拟键盘按下
    /// 这里主要处理扫描枪的值，手动输入的值不太好处理
    /// </summary>
    public class BardCodeHooK
    {
        #region 类变量

        public delegate void BardCodeDeletegate(BarCodes barCode);
        public event BardCodeDeletegate BarCodeEvent;
        delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);
        BarCodes barCode = new BarCodes();
        int hKeyboardHook = 0;
        string strBarCode = "";
        //定义成静态，这样不会抛出回收异常
        private static HookProc hookproc;

        #endregion  

        #region 结构

        public struct BarCodes
        {
            public int VirtKey;//虚拟吗
            public int ScanCode;//扫描码
            public string KeyName;//键名
            public uint Ascll;//Ascll
            public char Chr;//字符

            public string BarCode;//条码信息   保存最终的条码
            public bool IsValid;//条码是否有效
            public DateTime Time;//扫描时间,
        }

        private struct EventMsg
        {
            public int message;
            public int paramL;
            public int paramH;
            public int Time;
            public int hwnd;
        }

        #endregion

        #region 动态链接方法

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);

        [DllImport("user32", EntryPoint = "GetKeyNameText")]
        private static extern int GetKeyNameText(int IParam, StringBuilder lpBuffer, int nSize);

        [DllImport("user32", EntryPoint = "GetKeyboardState")]
        private static extern int GetKeyboardState(byte[] pbKeyState);

        [DllImport("user32", EntryPoint = "ToAscii")]
        private static extern bool ToAscii(int VirtualKey, int ScanCode, byte[] lpKeySate, ref uint lpChar, int uFlags);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string name);

        #endregion

        #region 方法

        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if (nCode == 0)
            {
                EventMsg msg = (EventMsg)Marshal.PtrToStructure(lParam, typeof(EventMsg));
                if (wParam == 0x100)//WM_KEYDOWN=0x100
                {
                    barCode.VirtKey = msg.message & 0xff;//虚拟吗
                    barCode.ScanCode = msg.paramL & 0xff;//扫描码
                    StringBuilder strKeyName = new StringBuilder(225);
                    if (GetKeyNameText(barCode.ScanCode * 65536, strKeyName, 255) > 0)
                    {
                        barCode.KeyName = strKeyName.ToString().Trim(new char[] { ' ', '\0' });
                    }
                    else
                    {
                        barCode.KeyName = "";
                    }
                    byte[] kbArray = new byte[256];
                    uint uKey = 0;
                    GetKeyboardState(kbArray);


                    if (ToAscii(barCode.VirtKey, barCode.ScanCode, kbArray, ref uKey, 0))
                    {
                        barCode.Ascll = uKey;
                        barCode.Chr = Convert.ToChar(uKey);
                    }

                    TimeSpan ts = DateTime.Now.Subtract(barCode.Time);

                    if (ts.TotalMilliseconds > 50)
                    {//时间戳，大于50 毫秒表示手动输入
                        strBarCode = barCode.Chr.ToString();
                    }
                    else
                    {
                        if ((msg.message & 0xff) == 13 && strBarCode.Length > 3)
                        {//回车
                            barCode.BarCode = strBarCode;
                            barCode.IsValid = true;
                        }
                        strBarCode += barCode.Chr.ToString();
                    }
                    barCode.Time = DateTime.Now;
                    if (BarCodeEvent != null)
                        BarCodeEvent(barCode);//触发事件
                    barCode.IsValid = false;
                }
            }
            return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
        }

        //安装钩子
        public bool Start()
        {
            if (hKeyboardHook == 0)
            {
                hookproc = new HookProc(KeyboardHookProc);


                //GetModuleHandle 函数 替代 Marshal.GetHINSTANCE
                //防止在 framework4.0中 注册钩子不成功
                IntPtr modulePtr = GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName);

                //WH_KEYBOARD_LL=13
                //全局钩子 WH_KEYBOARD_LL
                //  hKeyboardHook = SetWindowsHookEx(13, hookproc, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);

                hKeyboardHook = SetWindowsHookEx(13, hookproc, modulePtr, 0);
            }
            return (hKeyboardHook != 0);
        }

        //卸载钩子
        public bool Stop()
        {
            if (hKeyboardHook != 0)
            {
                return UnhookWindowsHookEx(hKeyboardHook);
            }
            return true;
        }

        #endregion
    }
}
