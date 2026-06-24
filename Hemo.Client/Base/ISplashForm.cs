/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:  Splasher显示加载进度
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Hemo.Client.Base
{
    /// <summary>
    /// interface for Splash Screen
    /// </summary>
    public interface ISplashForm
    {
        void SetStatusInfo(string NewStatusInfo);
    }
}
