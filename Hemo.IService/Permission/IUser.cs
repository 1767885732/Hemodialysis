/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:登录、皮肤设置基本文件
 * 创建标识:吕志强-2013年7月27日
 * 
 * 修改时间:2013年11月4日
 * 修改人:顾伟伟
 * 修改描述:新增方法GetUserList
 * 
 * 修改时间:2014年2月12日
 * 修改人:吕志强
 * 修改描述:新增方法DeleteUserAreaMappingInfo
 * 
 * 修改时间:2014年5月23日
 * 修改人:贺建操
 * 修改描述:修改方法GetUserSkinDt
 * ----------------------------------------------------------------*/
using System.Data;
using System.ServiceModel;
using Hemo.Model;

namespace Hemo.IService.Permission
{
    [ServiceContract]
    public interface IUser
    {
        #region 登录、皮肤设置基本文件

        [OperationContract]
        int UpdateMedUsers(PermissionModel.MED_USERSDataTable userTable);

        [OperationContract]
        PermissionModel.MED_USERSDataTable GetUserList();

        [OperationContract]
        PermissionModel.MED_USERSDataTable VerifyUserLogin(string userName, string passWord);

        [OperationContract]
        DataTable GetPermissionListByUserID(string userId);

        [OperationContract]
        PermissionModel.MED_USERAREA_MAPPINGDataTable GetUserAreaMappingList(string userId);

        [OperationContract]
        int SaveUserAreaMappingInfo(PermissionModel.MED_USERAREA_MAPPINGDataTable userAreaMappingDt);

        [OperationContract]
        int DeleteUserAreaMappingInfo(string userId);

        [OperationContract]
        ConfigModel.MED_COMMON_ITEMLISTDataTable GetAreaList(string userId);

        [OperationContract]
        int ExecuteScheduleProceduce();

        [OperationContract]
        int SaveUserSkin(DictModel.MED_USERS_SKINDataTable dt);

        [OperationContract]
        DictModel.MED_USERS_SKINDataTable GetUserSkinDt();
        #endregion
    }
}
