/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：病人血管通路数据服务层
// 创建时间：2013-03-13
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
//
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hemo.IService;
using Hemo.Business;
using Hemo.Model;
using System.Data;

namespace Hemo.Service {
    public class VascuarAccessService : MarshalByRefObject, IVascuarAccess {

        /// <summary>
        /// 根据病人血透编号得到透析号血透通路日期列表
        /// </summary>
        /// <param name="pHEMODIALYSIS_ID">透析号</param>
        /// <returns>病人血透通路日期列表</returns>
        public HemoModel.MED_VASCULAR_ACCESSDataTable GetVascularAccessListByHEMODIALYSIS_ID(string pHEMODIALYSIS_ID) {
            return VascuarAccessBll.GetVascularAccessListByHEMODIALYSIS_ID(pHEMODIALYSIS_ID);
        }

        /// <summary>
        /// 根据血管通路编号得到血管通路数据
        /// </summary>
        /// <param name="VASCULAR_ACCESS_ID">血管通路号</param>
        /// <returns>血管通路信息</returns>
        public HemoModel.MED_VASCULAR_ACCESSDataTable GetVascuarAccessListByID(string pVASCULAR_ACCESS_ID) {
            return VascuarAccessBll.GetVascuarAccessListByID(pVASCULAR_ACCESS_ID);
        }

        /// <summary>
        /// 根据血管通路编号得到血管通路数据
        /// </summary>
        /// <param name="VASCULAR_ACCESS_ID">血管通路号</param>
        /// <returns>血管通路信息</returns>
        public DataTable GetVascuarAccessNameByID(string pVASCULAR_ACCESS_ID) {
            return VascuarAccessBll.GetVascuarAccessNameByID(pVASCULAR_ACCESS_ID);
        }

        /// <summary>
        /// 保存血管通路数据
        /// </summary>
        /// <param name="patientDataTable">血管通路数据表</param>
        /// <returns>更新成功行数</returns>
        public int SaveVascularAccessInfo(HemoModel.MED_VASCULAR_ACCESSDataTable VascularAccessDataTable) {
            return VascuarAccessBll.SaveData(VascularAccessDataTable);
        }

        /// <summary>
        /// GetVascularAccessAllName
        /// </summary>
        /// <param name="pVid"></param>
        /// <returns></returns>
        public DataTable GetVascularAccessAllName(string pVid) {
            return VascuarAccessBll.GetVascularAccessAllName(pVid);
        }
    }
}
