/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:登录验证服务类
 * 创建标识:顾伟伟-2013年5月11日
 * 
 * 修改时间:2013年8月19日
 * 修改人:贺建操
 * 修改描述:新增方法GetUserList
 * 
 * 修改时间:2013年11月27日
 * 修改人:贺建操
 * 修改描述:修改方法DeleteUserAreaMappingInfo
 * 
 * 修改时间:2014年3月7日
 * 修改人:贺建操
 * 修改描述:修改方法GetUserSkinDt
 * ----------------------------------------------------------------*/
using System;
using System.Data;
using Hemo.Business.Permission;
using Hemo.IService.Permission;
using Hemo.Model;

namespace Hemo.Service.Permission
{
    public class UserService : MarshalByRefObject, IUser
    {
        #region IUser 成员

        public int UpdateMedUsers(PermissionModel.MED_USERSDataTable userTable)
        {
            return UserBll.UpdateMedUsers(userTable);
        }

        public PermissionModel.MED_USERSDataTable GetUserList()
        {
            return UserBll.GetUserList();
        }

        /// <summary>
        /// 用户登录判断
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public PermissionModel.MED_USERSDataTable VerifyUserLogin(string userName, string passWord)
        {
            return UserBll.VerifyUserLogin(userName, passWord);
        }

        public DataTable GetPermissionListByUserID(string userId)
        {
            return UserBll.GetPermissionListByUserID(userId);
        }

        public PermissionModel.MED_USERAREA_MAPPINGDataTable GetUserAreaMappingList(string userId)
        {
            return UserBll.GetUserAreaMappingList(userId);
        }

        public int SaveUserAreaMappingInfo(PermissionModel.MED_USERAREA_MAPPINGDataTable userAreaMappingDt)
        {
            return UserBll.SaveUserAreaMappingInfo(userAreaMappingDt);
        }

        public int DeleteUserAreaMappingInfo(string userId)
        {
            return UserBll.DeleteUserAreaMappingInfo(userId);
        }

        public ConfigModel.MED_COMMON_ITEMLISTDataTable GetAreaList(string userId)
        {
            return UserBll.GetAreaList(userId);
        }

        public int ExecuteScheduleProceduce()
        {
            return UserBll.ExecuteScheduleProceduce();
        }
        public int SaveUserSkin(DictModel.MED_USERS_SKINDataTable dt)
        {
            return UserBll.SaveUserSkin(dt);
        }

        public DictModel.MED_USERS_SKINDataTable GetUserSkinDt()
        {
            return UserBll.GetUserSkinDt();
        }
        #endregion


       
    }
}
