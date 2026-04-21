/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:Config接口定义
 * 创建标识:贺建操-2013年8月27日
 * ----------------------------------------------------------------*/
using System.Data;
using System.ServiceModel;
using Hemo.Model;
using System;

namespace Hemo.IService.Config
{
    [ServiceContract]
    public interface IConfig
    {
        #region 定义基础数据等操作接口

        [OperationContract]
        ConfigModel.MED_COMMON_ITEMLISTDataTable GetConfigList(string value, string name, string type, string status);

        [OperationContract]
        int SaveConfigInfo(ConfigModel.MED_COMMON_ITEMLISTDataTable configDataTable);

        [OperationContract]
        DataTable GetConfigTypeList();

        [OperationContract]
        ConfigModel.MED_COMMON_RELATIONDataTable GetCommRelation();

        [OperationContract]
        int SaveCommonRelation(ConfigModel.MED_COMMON_RELATIONDataTable dt);

        [OperationContract]
        int DeleteCommonRelationById(string relationId);

        [OperationContract]
        int SaveMedFuncitonCount(ConfigModel.MED_FUNCION_COUNTDataTable dt);

        [OperationContract]
        int DeleteMED_HOSPITAL_INFOById(string HOSPITAL_ID);

        [OperationContract]
        int SaveMED_HOSPITAL_INFO(ConfigModel.MED_HOSPITAL_INFODataTable dt);

        [OperationContract]
        ConfigModel.MED_HOSPITAL_INFODataTable GetMED_HOSPITAL_INFOById(string HOSPITAL_ID);

        [OperationContract]
        ConfigModel.MED_HOSPITAL_INFODataTable GetMED_HOSPITAL_INFOList();

        [OperationContract]
        ConfigModel.MED_EQUIPMENT_INFODataTable GetQualityControlEquipmentInfoByHospitalID(string HOSPITAL_ID);

        [OperationContract]
        int DeleteQualityControlEquipmentInfoByHospitalID(string HOSPITAL_ID);

        [OperationContract]
        int SaveQualityControlEquipmentInfo(ConfigModel.MED_EQUIPMENT_INFODataTable dt);

        [OperationContract]
        DataTable GetQualityControlEquipmentInfo();

        [OperationContract]
        int SaveMED_QUALITY_BASE(ConfigModel.MED_QUALITY_BASEDataTable dt);

        [OperationContract]
        int DeleteMED_QUALITY_BASE(string HOSPITAL_ID);

        [OperationContract]
        ConfigModel.MED_QUALITY_BASEDataTable GetMED_QUALITY_BASE(string HOSPITAL_ID);

        [OperationContract]
        int DeleteMED_QUALITY_BASEbyHOSPITAL_IDandKind(string HOSPITAL_ID, string Kind);

        [OperationContract]
        ConfigModel.MED_SCREEN_MSGDataTable GetMsgByParams(DateTime dtStart, DateTime dtEnd);

        [OperationContract]
        int SaveMsg(ConfigModel.MED_SCREEN_MSGDataTable dt);

        [OperationContract]
        ConfigModel.MED_UPTYPE_MGRDataTable GET_MED_UPTYPE_MGR(string UPTYPE);

        [OperationContract]
        ConfigModel.MED_COMMON_ITEMLISTDataTable GetItemListByItemType(string type);
        #endregion

    }
}
