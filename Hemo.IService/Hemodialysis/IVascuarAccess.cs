/*----------------------------------------------------------------
      // Copyright (C) 2005 (苏州)医疗科技发展有限公司
      // 文件名：Hemo.IService
      // 文件功能描述：病人血管通路接口定义文件
      // 创建标识：刘超-2013-3-13
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Hemo.Model;
using System.Data;

namespace Hemo.IService {
    [ServiceContract]
    public interface IVascuarAccess
    {

        #region 病人血管通路接口

        [OperationContract]
        HemoModel.MED_VASCULAR_ACCESSDataTable GetVascularAccessListByHEMODIALYSIS_ID(string pHEMODIALYSIS_ID);

        [OperationContract]
        HemoModel.MED_VASCULAR_ACCESSDataTable GetVascuarAccessListByID(string pVASCULAR_ACCESS_ID);

        [OperationContract]
        DataTable GetVascuarAccessNameByID(string pVASCULAR_ACCESS_ID);

        [OperationContract]
        int SaveVascularAccessInfo(HemoModel.MED_VASCULAR_ACCESSDataTable VascularAccessDataTable);

        [OperationContract]
        DataTable GetVascularAccessAllName(string pVid);
        #endregion
    }
}
