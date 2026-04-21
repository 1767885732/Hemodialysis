/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:Config服务类
 * 创建标识:贺建操-2013年8月27日
 * ----------------------------------------------------------------*/
using System;
using System.Data;
using Hemo.Business.Config;
using Hemo.IService.Config;
using Hemo.Model;

namespace Hemo.Service.Config
{
    public class ConfigService : MarshalByRefObject, IConfig
    {
        #region IConfig 成员

        public ConfigModel.MED_COMMON_ITEMLISTDataTable GetConfigList(string value, string name, string type, string status)
        {
            return ConfigBll.GetConfigList(value, name, type, status);
        }

        public int SaveConfigInfo(ConfigModel.MED_COMMON_ITEMLISTDataTable configDataTable)
        {
            return ConfigBll.SaveConfigInfo(configDataTable);
        }

        public DataTable GetConfigTypeList()
        {
            return ConfigBll.GetConfigTypeList();
        }

        #endregion

        #region RelationConfig

        public ConfigModel.MED_COMMON_RELATIONDataTable GetCommRelation()
        {
            return ConfigBll.GetCommRelation();
        }

        public int SaveCommonRelation(ConfigModel.MED_COMMON_RELATIONDataTable dt)
        {
            return ConfigBll.SaveCommonRelation(dt);
        }
        public int DeleteCommonRelationById(string relationId)
        {
            return ConfigBll.DeleteCommonRelationById(relationId);
        }

        #endregion

        #region 定义基础配置服务类

        public int SaveMedFuncitonCount(ConfigModel.MED_FUNCION_COUNTDataTable dt)
        {
            return ConfigBll.SaveMedFuncitonCount(dt);
        }



        public int DeleteMED_HOSPITAL_INFOById(string HOSPITAL_ID)
        {
            return ConfigBll.DeleteMED_HOSPITAL_INFOById(HOSPITAL_ID);
        }

        public int SaveMED_HOSPITAL_INFO(ConfigModel.MED_HOSPITAL_INFODataTable dt)
        {
            return ConfigBll.SaveMED_HOSPITAL_INFO(dt);
        }

        public ConfigModel.MED_HOSPITAL_INFODataTable GetMED_HOSPITAL_INFOById(string HOSPITAL_ID)
        {
            return ConfigBll.GetMED_HOSPITAL_INFOById(HOSPITAL_ID);
        }

        public ConfigModel.MED_HOSPITAL_INFODataTable GetMED_HOSPITAL_INFOList()
        {
            return ConfigBll.GetMED_HOSPITAL_INFOList();
        }

        public DataTable GetQualityControlEquipmentInfo()
        {
            return ConfigBll.GetQualityControlEquipmentInfo();
        }

        public ConfigModel.MED_EQUIPMENT_INFODataTable GetQualityControlEquipmentInfoByHospitalID(string HOSPITAL_ID)
        {
            return ConfigBll.GetQualityControlEquipmentInfoByHospitalID(HOSPITAL_ID);
        }

        public int DeleteQualityControlEquipmentInfoByHospitalID(string HOSPITAL_ID)
        {
            return ConfigBll.DeleteQualityControlEquipmentInfoByHospitalID(HOSPITAL_ID);
        }

        public int SaveQualityControlEquipmentInfo(ConfigModel.MED_EQUIPMENT_INFODataTable dt)
        {
            return ConfigBll.SaveQualityControlEquipmentInfo(dt);
        }

        public int SaveMED_QUALITY_BASE(ConfigModel.MED_QUALITY_BASEDataTable dt)
        {
            return ConfigBll.SaveMED_QUALITY_BASE(dt);
        }


        public int DeleteMED_QUALITY_BASE(string HOSPITAL_ID)
        {
            return ConfigBll.DeleteMED_QUALITY_BASE(HOSPITAL_ID);
        }
        public ConfigModel.MED_QUALITY_BASEDataTable GetMED_QUALITY_BASE(string HOSPITAL_ID)
        {
            return ConfigBll.GetMED_QUALITY_BASE(HOSPITAL_ID);
        }

        public int DeleteMED_QUALITY_BASEbyHOSPITAL_IDandKind(string HOSPITAL_ID, string Kind)
        {
            return ConfigBll.DeleteMED_QUALITY_BASEbyHOSPITAL_IDandKind(HOSPITAL_ID, Kind);
        }
        #endregion


        public ConfigModel.MED_SCREEN_MSGDataTable GetMsgByParams(DateTime dtStart, DateTime dtEnd)
        {
            return ConfigBll.GetMsgByParams(dtStart, dtEnd);
        }

        public int SaveMsg(ConfigModel.MED_SCREEN_MSGDataTable dt)
        {
            return ConfigBll.SaveMsg(dt);
        }


        public ConfigModel.MED_UPTYPE_MGRDataTable GET_MED_UPTYPE_MGR(string UPTYPE)
        {
            return ConfigBll.GET_MED_UPTYPE_MGR(UPTYPE);
        }

        public ConfigModel.MED_COMMON_ITEMLISTDataTable GetItemListByItemType(string type)
        {
            return ConfigBll.GetItemListByItemType(type);
        }
    }
}
