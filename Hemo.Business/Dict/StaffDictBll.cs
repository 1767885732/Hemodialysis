/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:Hemo.Business.Dict
 * 创建标识:吕志强-2013年8月24日
 * 
 * 修改时间:2014年1月9日
 * 修改人:顾伟伟
 * 修改描述:通用窗体取值、赋值、清空值、验证控件值方法
 * 
 * 修改时间:2014年5月20日
 * 修改人:吕志强
 * 修改描述:更新根据药品厂商编号得到数据SQL
 * 
 * 修改时间:2014年9月26日
 * 修改人:贺建操
 * 修改描述:修复病人的处方无法确认衍生的问题
 * ----------------------------------------------------------------*/
using System.Data;
using Hemo.Model;
using System.Data.Common;


namespace Hemo.Business.Dict
{
    public class StaffDictBll : BaseClass
    {
        /// <summary>
        /// 获取MED_STAFF_DICT
        /// </summary>
        /// <returns></returns>
        public static DictModel.MED_STAFF_DICTDataTable GetStaffDictList()
        {
            DictModel.MED_STAFF_DICTDataTable result = new DictModel.MED_STAFF_DICTDataTable();
            return GetData<DictModel.MED_STAFF_DICTDataTable>(result, "GetStaffDictList", null);
        }
        /// <summary>
        /// 获取MED_STAFF_DICT
        /// </summary>
        /// <returns></returns>
        public static DictModel.MED_STAFF_DICTDataTable GetAllStaffDictList()
        {
            DictModel.MED_STAFF_DICTDataTable result = new DictModel.MED_STAFF_DICTDataTable();
            return GetData<DictModel.MED_STAFF_DICTDataTable>(result, "GetAllStaffDictList", null);
        }
        /// <summary>
        /// 保存SaveStaffDictInfo
        /// </summary>
        /// <param name="staffDictDataTable"></param>
        /// <returns></returns>
        public static int SaveStaffDictInfo(DictModel.MED_STAFF_DICTDataTable staffDictDataTable)
        {
            return SaveData<DictModel.MED_STAFF_DICTDataTable>(staffDictDataTable);
        }
        /// <summary>
        /// 获取GetNewEMPNO
        /// </summary>
        /// <returns></returns>
        public static string GetNewEMPNO()
        {
            //初始化值
            string result = "10000000";
            DataTable dt = new DataTable();

            GetData(dt, "GetNewEMPNO", null);

            if (dt != null && dt.Rows.Count > 0 && !string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                result = dt.Rows[0][0].ToString();

            return result;
        }

        /// <summary>
        /// 根据组长标识获取护士组长或组员
        /// </summary>
        /// <param name="isLeader"></param>
        /// <returns></returns>
        public static DictModel.MED_STAFF_DICTDataTable GetStaffDictByLeaderFlag(string isLeader)
        {
            var dtResult = new DictModel.MED_STAFF_DICTDataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("IS_LEADER", DbType.String, isLeader);
            return GetData<DictModel.MED_STAFF_DICTDataTable>(dtResult, "GetStaffDictByLeaderFlag", dbParams);
        }

        /// <summary>
        /// 根据护士组长获取护士组员
        /// </summary>
        /// <param name="nurseLeader"></param>
        /// <returns></returns>
        public static DictModel.MED_STAFF_DICTDataTable GetStaffDictByNurseLeader(string nurseLeader)
        {
            var dtResult = new DictModel.MED_STAFF_DICTDataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("NURSE_LEADER", DbType.String, nurseLeader);
            return GetData<DictModel.MED_STAFF_DICTDataTable>(dtResult, "GetStaffDictByNurseLeader", dbParams);
        }

        /// <summary>
        /// 根据护士组长获取护士组员及相关组员
        /// </summary>
        /// <param name="nurseLeader"></param>
        /// <returns></returns>
        public static DictModel.MED_STAFF_DICTDataTable GetAllStaffDictByNurseLeader(string nurseLeader)
        {
            var dtResult = new DictModel.MED_STAFF_DICTDataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("NURSE_LEADER", DbType.String, nurseLeader);
            return GetData<DictModel.MED_STAFF_DICTDataTable>(dtResult, "GetAllStaffDictByNurseLeader", dbParams);
        }
    }
}
