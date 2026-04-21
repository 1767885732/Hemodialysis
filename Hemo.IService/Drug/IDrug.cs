/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:新增方法IDrug
 * 创建标识:贺建操-2013年7月15日
 * 
 * 修改时间:2013年11月30日
 * 修改人:贺建操
 * 修改描述:修改方法GetDrugMasterList
 * 
 * 修改时间:2014年4月10日
 * 修改人:刘超
 * 修改描述:修改方法GetDrugMasterListByParams
 * 
 * 修改时间:2014年8月17日
 * 修改人:顾伟伟
 * 修改描述:新增方法GetDaleGateDrugMasterList
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Hemo.Model;
using System.Data;

namespace Hemo.IService {
    [ServiceContract]
    public interface IDrug {

        #region 药品主档
        [OperationContract]
        DrugModel.MED_DRUG_MASTERDataTable GetDrugMasterList();

        [OperationContract]
        DrugModel.MED_DRUG_MASTERDataTable GetDrugMasterListByParams(DrugModel.MED_DRUG_MASTERDataTable pTable);

        [OperationContract]
        DrugModel.MED_DRUG_MASTERDataTable GetDrugMasterListByDrugCode(string pDrugCode);

        [OperationContract]
        int SaveDrugMasterInfo(DrugModel.MED_DRUG_MASTERDataTable pTable);

        [OperationContract]
        string GetNewDrugCode();

        [OperationContract]
        int DownDrugFromBaseByName(string drugName);

        /// <summary>
        /// 得到托管药品主档列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]     
        DrugModel.MED_DRUG_MASTERDataTable GetDaleGateDrugMasterList();
        #endregion

        #region 药厂设置
        [OperationContract]
        DrugModel.MED_DRUG_FIRMDataTable GetDrugFirmList();

        [OperationContract]
        DrugModel.MED_DRUG_FIRMDataTable GetDrugFirmListByFirmType(string pFirmType);

        [OperationContract]
        DrugModel.MED_DRUG_FIRMDataTable GetDrugFirmListByParams(DrugModel.MED_DRUG_FIRMDataTable pTable);

        [OperationContract]
        DrugModel.MED_DRUG_FIRMDataTable GetDrugFrimListByFirmID(string pFirmID);

        [OperationContract]
        int SaveDrugFirmInfo(DrugModel.MED_DRUG_FIRMDataTable pTable);

        [OperationContract]
        int DeleteDrugFirmInfo(string pFirmID);

        [OperationContract]
        string GetNewFirmID();
        #endregion

        #region 药品耗材库存管理
        [OperationContract]
        int SaveMedMaterialInput(DrugModel.MED_MATERIAL_INPUTDataTable pTable);

        [OperationContract]
        DataTable GetMedMaterialInputList();

        [OperationContract]
        DrugModel.MED_MATERIAL_INPUTDataTable GetMedMaterialInputByID(string pID);

        [OperationContract]
        DataTable GetMedMaterialInputByHemoIdAndCode(string pCode, string pHemoID);

        [OperationContract]
        DataTable GetMedMaterialOutputByHemoIdAndCode(string pCode, string pHemoID);

        [OperationContract]
        DrugModel.MED_MATERIAL_CHECKDataTable GetMedMaterialCheckByHemoIdAndCode(string pCode, string pHemoID);

        [OperationContract]
        int SaveMedMaterialOutput(DrugModel.MED_MATERIAL_OUTPUTDataTable pTable);

        [OperationContract]
        DrugModel.MED_MATERIAL_OUTPUTDataTable GetMedMaterialOutputByID(string pID);

        [OperationContract]
        DataTable GetMedMaterialOutputList();

        [OperationContract]
        int SaveMedMaterialCheck(DrugModel.MED_MATERIAL_CHECKDataTable pTable);

        [OperationContract]
        DrugModel.MED_MATERIAL_CHECKDataTable GetMedMaterialCheckList(DateTime dtMoon,string checker);

        [OperationContract]
        DrugModel.MED_MATERIAL_CHECKDataTable GetMedMaterialCheckHisList();

        [OperationContract]
        bool SaveDeleteOutputAndCheckMaterialData(DrugModel.MED_MATERIAL_OUTPUTDataTable pOutTable, DrugModel.MED_MATERIAL_CHECKDataTable pCheckTable);
     
        [OperationContract]
        bool SaveDeleteInputAndCheckMaterialData(DrugModel.MED_MATERIAL_INPUTDataTable pInTable, DrugModel.MED_MATERIAL_CHECKDataTable pCheckTable);
        #endregion

        #region 单个病人药品出入库
        [OperationContract]
        DataTable QueryPatientDrugInputById(string pID, DateTime beginTime, DateTime endTime);


        [OperationContract]
        DataTable QueryPatientDrugOutputById(string pID, DateTime beginTime, DateTime endTime);


        [OperationContract]
        ReportRelationModel.PatientDrugOutPutPrintDataTable QueryPatientDrugOutPutToPrint(string pID, DateTime beginTime, DateTime endTime);

        [OperationContract]
        DataTable GetPatientDrugNumberByParam(string pID, string pCode);

        [OperationContract]
        int SavePatientDrugInput(DrugModel.MED_PATIENT_DRUG_INPUTDataTable dt);

        [OperationContract]
        int SavePatientDrugOutput(DrugModel.MED_PATIENT_DRUG_OUTPUTDataTable dt);

        [OperationContract]
        int UpdatePatientDrugInputByParam(string pID, string pCode, decimal sum);

        [OperationContract]
        int UpdatePatientDrugInputByOutPutParam(string pID, string pCode, decimal sum, string pInPutId);

        [OperationContract]
        int UpdatePatientDrugInputRemainByParam(string pID,decimal remain);

        [OperationContract]
        int DeletePatientDrugInputByID(string pID);

        [OperationContract]
        int DeletePatientDrugOutputByID(string pID);

        [OperationContract]
        int UpdatePatientDrugInputStatusByID(string pID);

        [OperationContract]
        DrugModel.MED_DRUG_MASTERDataTable GetDrugInputList(string currentHemoId);
        #endregion
    }
}
