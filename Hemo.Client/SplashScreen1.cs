/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：界面加载等待界面
// 创建时间：2017-03-08
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
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using Hemo.Client.Base;

namespace Hemo.Client
{
    public partial class SplashScreen1 : SplashScreen
    {
        #region 构造函数

        public SplashScreen1()
        {
            InitializeComponent();
        }

        #endregion

        #region 枚举

        public enum SplashScreenCommand
        {
            SetProgress,
            SetText
        }

        #endregion

        #region 方法

        public void SetStatusInfo(string NewStatusInfo)
        {
            labelControl2.Text = NewStatusInfo;
        }

        public override void ProcessCommand(Enum cmd, object arg)
        {
            if (cmd.ToString() == SplashScreenCommand.SetText.ToString())
            {
                SetStatusInfo(arg.ToString());
            }
            base.ProcessCommand(cmd, arg);
        }

        #endregion
    }
}