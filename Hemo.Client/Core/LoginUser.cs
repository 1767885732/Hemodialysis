/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:登录用户
 * 创建标识:顾伟伟-2017年1月30日
 * ----------------------------------------------------------------*/
using Hemo.Model;

namespace Hemo.Client.Core
{
    public class LoginUser
    {
        #region 属性

        public static PermissionModel.MED_USERSRow User
        {
            private set;
            get;
        }

        #endregion

        #region 方法

        public static void SetUserInfo(PermissionModel.MED_USERSRow user)
        {
            User = user;
        }

        #endregion
    }
}
