/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:Dict
 * 创建标识:顾伟伟-2013年8月1日
 * 
 * 修改时间:2013年12月17日
 * 修改人:刘超
 * 修改描述:新增方法staffDictDataTable
 * ----------------------------------------------------------------*/
using System.ServiceModel;
using Hemo.Model;

namespace Hemo.IService.Dict
{
    [ServiceContract]
    public interface IStaffDict
    {
        /// <summary>
        /// GetStaffDictList
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DictModel.MED_STAFF_DICTDataTable GetStaffDictList();

        /// <summary>
        /// GetAllStaffDictList
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DictModel.MED_STAFF_DICTDataTable GetAllStaffDictList();

        /// <summary>
        /// staffDictDataTable
        /// </summary>
        /// <param name="staffDictDataTable"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveStaffDictInfo(DictModel.MED_STAFF_DICTDataTable staffDictDataTable);

        /// <summary>
        /// GetNewEMPNO
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetNewEMPNO();

        /// <summary>
        /// GetStaffDictByLeaderFlag
        /// </summary>
        /// <param name="isLeader"></param>
        /// <returns></returns>
        [OperationContract]
        DictModel.MED_STAFF_DICTDataTable GetStaffDictByLeaderFlag(string isLeader);

        /// <summary>
        /// GetStaffDictByNurseLeader
        /// </summary>
        /// <param name="nurseLeader"></param>
        /// <returns></returns>
        [OperationContract]
        DictModel.MED_STAFF_DICTDataTable GetStaffDictByNurseLeader(string nurseLeader);

        /// <summary>
        /// GetAllStaffDictByNurseLeader
        /// </summary>
        /// <param name="nurseLeader"></param>
        /// <returns></returns>
        [OperationContract]
        DictModel.MED_STAFF_DICTDataTable GetAllStaffDictByNurseLeader(string nurseLeader);
    }
}
