/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：用户登录临时类
// 创建时间：2014-04-12
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using DevExpress.Utils;
using Hemo.Client.Core;
using Hemo.IService.Permission;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Hemo.Client
{
   public  class LoginTemp
   {
       #region 类变量

       private IUser _userService = ServiceManager.Instance.UserService;

       private DefaultBoolean IsLogin = DefaultBoolean.Default;

       #endregion

       #region 属性

       public string UserName { get; set; }

       public string PassWord { get; set; }

       #endregion

       #region 方法

       //模拟admin登录
       public void Login()
       {
           DataTable office = new DataTable();
           PermissionModel.MED_USERSDataTable loginUser = _userService.VerifyUserLogin(UserName.ToUpper(),
                   Utility.Encrypto(PassWord.ToUpper()));
           if (loginUser.Rows.Count > 0)
           {
               LoginUser.SetUserInfo(loginUser.Rows[0] as PermissionModel.MED_USERSRow);
               IsLogin = DefaultBoolean.True;
               HemoApplicationContext.Current.CurrentUser = loginUser.Rows[0] as PermissionModel.MED_USERSRow;
           }
           else { IsLogin = DefaultBoolean.False; }
           if (IsLogin == DefaultBoolean.True)
           {
               //AppendLoginNamesToTxt(userName);
               office = this._userService.GetPermissionListByUserID(LoginUser.User.USER_ID);
               HemoApplicationContext.Current.RolesOffices = office;
               HemoApplicationContext.Current.CurrentUser = LoginUser.User;
               //执行每日排班的存储过程、写入所在星期的一周数据
               this._userService.ExecuteScheduleProceduce();
           }
       }

       #endregion
    }
}
