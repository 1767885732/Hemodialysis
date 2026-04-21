/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:Hemo.Business.Permission
 * 创建标识:贺建操-2013年8月27日
 * 
 * 修改时间:2014年1月12日
 * 修改人:刘超
 * 修改描述:新增方法UpdateMedUsers
 * ----------------------------------------------------------------*/
using System.Data;
using System.Data.Common;
using Hemo.DataAccess;
using Hemo.Model;

namespace Hemo.Business.Permission
{
    public class UserBll : BaseClass
    {
        /// <summary>
        /// UpdateMedUsers
        /// </summary>
        /// <param name="userTable"></param>
        /// <returns></returns>
        public static int UpdateMedUsers(PermissionModel.MED_USERSDataTable userTable)
        {
            // return SaveData<PermissionModel.MED_USERSDataTable>(userTable);
            return IDatabase.Update(userTable);
        }

        /// <summary>
        /// GetUserList
        /// </summary>
        /// <returns></returns>
        public static PermissionModel.MED_USERSDataTable GetUserList()
        {
            PermissionModel.MED_USERSDataTable result = new PermissionModel.MED_USERSDataTable();
            return GetData<PermissionModel.MED_USERSDataTable>(result, "GetUserList", null);
        }

        /// <summary>
        /// 用户登录判断
        /// </summary>
        /// <returns></returns>
        public static PermissionModel.MED_USERSDataTable VerifyUserLogin(string userName, string passWord)
        {
            PermissionModel.MED_USERSDataTable result = new PermissionModel.MED_USERSDataTable();
            DbParameter[] dbParams = new DbParameter[2];
            dbParams[0] = IDatabase.BuildDbParameter("USER_NAME", DbType.String, userName);
            dbParams[1] = IDatabase.BuildDbParameter("USER_PWD", DbType.String, passWord);
            return GetData<PermissionModel.MED_USERSDataTable>(result, "GetUserLogin", dbParams);
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DataTable GetPermissionListByUserID(string userId)
        {
            DataTable result = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];

            dbParams[0] = IDatabase.BuildDbParameter("USER_ID", DbType.String, userId.Trim());

            return GetData(result, "GetPermissionListByUserID", dbParams);
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static PermissionModel.MED_USERAREA_MAPPINGDataTable GetUserAreaMappingList(string userId)
        {
            PermissionModel.MED_USERAREA_MAPPINGDataTable result = new PermissionModel.MED_USERAREA_MAPPINGDataTable();
            DbParameter[] dbParams = new DbParameter[1];

            dbParams[0] = IDatabase.BuildDbParameter("USER_ID", DbType.String, userId.Trim());

            return GetData<PermissionModel.MED_USERAREA_MAPPINGDataTable>(result, "GetUserAreaMappingList", dbParams);
        }

        /// <summary>
        /// 保存信息
        /// </summary>
        /// <param name="userAreaMappingDt"></param>
        /// <returns></returns>
        public static int SaveUserAreaMappingInfo(PermissionModel.MED_USERAREA_MAPPINGDataTable userAreaMappingDt)
        {
            return SaveData<PermissionModel.MED_USERAREA_MAPPINGDataTable>(userAreaMappingDt);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int DeleteUserAreaMappingInfo(string userId)
        {
            DbParameter[] dbParams = new DbParameter[1];

            dbParams[0] = IDatabase.BuildDbParameter("USER_ID", DbType.String, userId.Trim());

            return DeleteData("DeleteUserAreaMappingInfo", dbParams);
        }

        /// <summary>
        /// GetAreaList
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static ConfigModel.MED_COMMON_ITEMLISTDataTable GetAreaList(string userId)
        {
            ConfigModel.MED_COMMON_ITEMLISTDataTable result = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
            DbParameter[] dbParams = new DbParameter[1];

            dbParams[0] = IDatabase.BuildDbParameter("USER_ID", DbType.String, userId.Trim());

            return GetData<ConfigModel.MED_COMMON_ITEMLISTDataTable>(result, "GetAreaList", dbParams);
        }

        /// <summary>
        /// SaveUserSkin
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int SaveUserSkin(DictModel.MED_USERS_SKINDataTable dt)
        {
            return SaveData<DictModel.MED_USERS_SKINDataTable>(dt);
        }

        /// <summary>
        /// GetUserSkinDt
        /// </summary>
        /// <returns></returns>
        public static DictModel.MED_USERS_SKINDataTable GetUserSkinDt()
        {
            var result = new DictModel.MED_USERS_SKINDataTable();
            return GetData<DictModel.MED_USERS_SKINDataTable>(result, "GetUserSkinDt", null);

        }

        /// <summary>
        /// ExecuteScheduleProceduce
        /// </summary>
        /// <returns></returns>
        public static int ExecuteScheduleProceduce()
        {
            try
            {
                IDatabase database = DatabaseFactory.Create();
                //PRO_MED_PATIENTSCHEDULE() 排班的存储过程
                //  database.ExecuteNonQuery("CALL PRO_MED_PATIENTSCHEDULE()");
                return database.ExecuteNonQuery("CALL PRO_MED_CUREDRUG()");
            }
            catch
            {
                return 0;
            }

        }
    }
}
