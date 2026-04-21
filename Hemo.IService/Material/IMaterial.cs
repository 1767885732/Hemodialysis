/*----------------------------------------------------------------
      // Copyright (C) 2005 (苏州)医疗科技发展有限公司
      // 文件名：IMaterial.cs
      // 文件功能描述：耗材资料接口定义文件
      // 创建标识：刘超-2013-3-22
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
using System.ServiceModel;
using Hemo.Model;
using System.Data;

namespace Hemo.IService {

    [ServiceContract]
    public interface IMaterial
    {
        #region 耗材资料接口定义文件

        [OperationContract]
        MaterialModel.MED_MATERIAL_MASTERDataTable GetMaterialMasterList();

        [OperationContract]
        MaterialModel.MED_MATERIAL_MASTERDataTable GetMaterialMasterListByParams(MaterialModel.MED_MATERIAL_MASTERDataTable pTable);

        [OperationContract]
        MaterialModel.MED_MATERIAL_MASTERDataTable GetMaterialMasterListByMaterialID(string pMaterialID);

        [OperationContract]
        int SaveMaterialMasterInfo(MaterialModel.MED_MATERIAL_MASTERDataTable pTable);

        [OperationContract]
        MaterialScheduleModel.MED_MATERIAL_MASTERDataTable GetMaterialAll();

        [OperationContract]
        int DeleteMaterialInPut(string inputId);

        [OperationContract]
        int DeleteMaterialOutPut(string outPutId);


        [OperationContract]
        string GetNewMaterialID();

        [OperationContract]
        int SaveHemoMaterialInfo(MaterialModel.MED_HEMO_MATERIALDataTable pTable);
        [OperationContract]
        int DeleteMaterialInfo(string pMaterialID);
        [OperationContract]
        MaterialModel.MED_HEMO_MATERIAL_REPORTDataTable GetMaterialReport(string pUseMaterialID);

        [OperationContract]
        MaterialModel.MED_MATERIAL_MASTERDataTable GetUseMaterialList(string pHemoID);

        [OperationContract]
        DrugModel.MED_MATERIAL_INPUTDataTable GetMedMaterialInputDetail(DateTime dtStar, DateTime dtEnd);

        [OperationContract]
        DrugModel.MED_MATERIAL_INPUTDataTable GetMedMaterialInputDetailByCodeAndBatchNum(DateTime dtMonth, string code, string batchNum);

        [OperationContract]
        DrugModel.MED_MATERIAL_INPUTDataTable GetMedMaterialInputMaster(DateTime dtStar,DateTime dtEnd);

        [OperationContract]
        int SaveMedMaterialInputNew(DrugModel.MED_MATERIAL_INPUTDataTable data);

        [OperationContract]
        DrugModel.MED_MATERIAL_OUTPUTDataTable GetMedMaterialOutputMaster(DateTime dtMoonStar,DateTime dtEndStar);
        [OperationContract]
        DrugModel.MED_MATERIAL_OUTPUTDataTable GetMedMaterialOutputDetail(DateTime dtMoonStar,DateTime dtEndStar);

        [OperationContract]
        DrugModel.MED_MATERIAL_INPUTMASTERDataTable GetMedMaterialListByTypeId(string modetypeId);

        [OperationContract]
        DrugModel.MED_MATERIAL_OUTPUTDataTable GetOutPutByCode(string code);

        [OperationContract]
        DataTable GetMaterialStoreInOutByCode(string code);

        [OperationContract]
        DrugModel.MED_MATERIAL_OUTPUTDataTable GetMedMaterialOutputDetailByCodeAndBatchNum(DateTime dtMonth, string code, string batchNum);

        [OperationContract]
        int SaveMedMaterialOutputNew(DrugModel.MED_MATERIAL_OUTPUTDataTable data);

        [OperationContract]
        int CheckMaterialInOutStore(string checker);

        [OperationContract]
        int SaveFollowUp(DrugModel.MED_PATIENT_FOLLOWUPDataTable data);

        [OperationContract]
        DrugModel.MED_PATIENT_FOLLOWUPDataTable GetFollowUpByData(DateTime dtStart,DateTime dtEnd,string hemoId);

        [OperationContract]
        DrugModel.MED_PATIENT_FOLLOWUPDataTable GetFollowUpByHemoID(DateTime dtStart,string hemoid);
        #endregion
    }
}
