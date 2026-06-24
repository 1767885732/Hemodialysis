/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:StaffDictService
 * 创建标识:顾伟伟-2013年8月1日
 * 
 * 修改时间:2013年12月17日
 * 修改人:刘超
 * 修改描述:新增方法staffDictDataTable
 * ----------------------------------------------------------------*/
using System;
using Hemo.Business.Dict;
using Hemo.IService.Dict;
using Hemo.Model;

namespace Hemo.Service.Dict
{
    public class StaffDictService : MarshalByRefObject, IStaffDict
    {
        #region IStaffDict 成员

        public DictModel.MED_STAFF_DICTDataTable GetStaffDictList()
        {
            return StaffDictBll.GetStaffDictList();
        }

        public DictModel.MED_STAFF_DICTDataTable GetAllStaffDictList()
        {
            return StaffDictBll.GetAllStaffDictList();
        }

        public int SaveStaffDictInfo(DictModel.MED_STAFF_DICTDataTable staffDictDataTable)
        {
            return StaffDictBll.SaveStaffDictInfo(staffDictDataTable);
        }

        public string GetNewEMPNO()
        {
            return StaffDictBll.GetNewEMPNO();
        }

        /// <summary>
        /// 根据组长标识获取护士组长或组员
        /// </summary>
        /// <param name="isLeader"></param>
        /// <returns></returns>
        public DictModel.MED_STAFF_DICTDataTable GetStaffDictByLeaderFlag(string isLeader)
        {
            return StaffDictBll.GetStaffDictByLeaderFlag(isLeader);
        }

        /// <summary>
        /// 根据护士组长获取护士组员
        /// </summary>
        /// <param name="nurseLeader"></param>
        /// <returns></returns>
        public DictModel.MED_STAFF_DICTDataTable GetStaffDictByNurseLeader(string nurseLeader)
        {
            return StaffDictBll.GetStaffDictByNurseLeader(nurseLeader);
        }

        /// <summary>
        /// 根据护士组长获取护士组员及相关组员
        /// </summary>
        /// <param name="nurseLeader"></param>
        /// <returns></returns>
        public DictModel.MED_STAFF_DICTDataTable GetAllStaffDictByNurseLeader(string nurseLeader)
        {
            return StaffDictBll.GetAllStaffDictByNurseLeader(nurseLeader);
        }

        #endregion
    }
}
