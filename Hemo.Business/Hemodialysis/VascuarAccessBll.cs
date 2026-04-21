/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:VascuarAccessBll
 * 创建标识:顾伟伟-2013年6月8日
 * 
 * 修改时间:2013年10月24日
 * 修改人:贺建操
 * 修改描述:GetVascularAccessListByHEMODIALYSIS_ID
 * 
 * 修改时间:2014年3月4日
 * 修改人:贺建操
 * 修改描述:GetVascuarAccessNameByID
 * 
 * 修改时间:2014年7月11日
 * 修改人:贺建操
 * 修改描述:GetVascularAccessAllName
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Hemo.Model;


namespace Hemo.Business {
    public partial class VascuarAccessBll : BaseClass {

        /// <summary>
        /// 根据病人血透编号得到透析号血透通路日期列表
        /// </summary>
        /// <param name="pHEMODIALYSIS_ID">透析号</param>
        /// <returns>病人血透通路日期列表</returns>
        public static HemoModel.MED_VASCULAR_ACCESSDataTable GetVascularAccessListByHEMODIALYSIS_ID(string pHEMODIALYSIS_ID) {
            HemoModel.MED_VASCULAR_ACCESSDataTable Result = new HemoModel.MED_VASCULAR_ACCESSDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("HEMODIALYSIS_ID", DbType.String, pHEMODIALYSIS_ID.Trim());
            return GetData<HemoModel.MED_VASCULAR_ACCESSDataTable>(Result, "GetVascuarAccessDateListByHemoID", Params);
        }

        /// <summary>
        /// 根据血管通路编号得到血管通路数据
        /// </summary>
        /// <param name="VASCULAR_ACCESS_ID">血管通路号</param>
        /// <returns>血管通路信息</returns>
        public static HemoModel.MED_VASCULAR_ACCESSDataTable GetVascuarAccessListByID(string pVASCULAR_ACCESS_ID) {
            HemoModel.MED_VASCULAR_ACCESSDataTable Result = new HemoModel.MED_VASCULAR_ACCESSDataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("VASCULAR_ACCESS_ID", DbType.String, pVASCULAR_ACCESS_ID.Trim());
            return GetData<HemoModel.MED_VASCULAR_ACCESSDataTable>(Result, "GetVascuarAccessListByID", Params);
        }

        /// <summary>
        /// 根据血管通路编号得到血管通路数据
        /// </summary>
        /// <param name="VASCULAR_ACCESS_ID">血管通路号</param>
        /// <returns>血管通路信息</returns>
        public static DataTable GetVascuarAccessNameByID(string pVASCULAR_ACCESS_ID) {
            DataTable Result = new DataTable();
            DbParameter[] Params = new DbParameter[1];
            Params[0] = IDatabase.BuildDbParameter("VASCULAR_ACCESS_ID", DbType.String, pVASCULAR_ACCESS_ID.Trim());
            return GetData<DataTable>(Result, "GetVascuarAccessNameByID", Params);
        }

        /// <summary>
        /// 保存血管通路信息
        /// </summary>
        /// <param name="patientDataTable">病人血管通路信息</param>
        /// <returns></returns>
        public static int SavePatientInfo(HemoModel.MED_VASCULAR_ACCESSDataTable vascularAccessDataTable) {
            return SaveData<HemoModel.MED_VASCULAR_ACCESSDataTable>(vascularAccessDataTable);
        }

        /// <summary>
        /// 获取患者血管通路全称
        /// </summary>
        /// <param name="pVid">通路ID</param>
        /// <returns></returns>
        public static DataTable GetVascularAccessAllName(string pVid) {
            DataTable dt = new DataTable();
            DbParameter[] dbParams = new DbParameter[1];
            dbParams[0] = IDatabase.BuildDbParameter("VASCULAR_ACCESS_ID", DbType.String, pVid);
            return GetData<DataTable>(dt, "GetVascularAccessAllName", dbParams);
        }
    }
}
