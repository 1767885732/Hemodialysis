/*----------------------------------------------------------------
      // Copyright (C) 2005 (北京)医疗科技发展有限公司
      // 文件名：IPatient.cs
      // 文件功能描述：耗材设置相关数据服务层
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
using Hemo.Business;
using Hemo.Model;
using Hemo.IService;

namespace Hemo.Service {
    public class MaterialService : MarshalByRefObject, IMaterial {

        /// <summary>
        /// 得到全部耗材列表列表
        /// </summary>
        /// <returns></returns>
        public MaterialModel.MED_MATERIAL_MASTERDataTable GetMaterialMasterList() {
            return MaterialBll.GetMaterialMasterList();
        }

        /// <summary>
        /// 根据查询参数得到耗材列表数据
        /// </summary>
        /// <param name="pTable">收集的查询条件TABLE</param>
        /// <returns></returns>
        public MaterialModel.MED_MATERIAL_MASTERDataTable GetMaterialMasterListByParams(MaterialModel.MED_MATERIAL_MASTERDataTable pTable) {
            return MaterialBll.GetMaterialMasterListByParams(pTable);
        }

        public MaterialScheduleModel.MED_MATERIAL_MASTERDataTable GetMaterialAll()
        {
            return MaterialBll.GetMaterialAll();
        }

        /// <summary>
        /// 根据耗材编号得到数据
        /// </summary>
        /// <param name="pMaterialID">耗材编号</param>
        /// <returns></returns>
        public MaterialModel.MED_MATERIAL_MASTERDataTable GetMaterialMasterListByMaterialID(string pMaterialID) {
            return MaterialBll.GetMaterialMasterListByMaterialID(pMaterialID);
        }

        /// <summary>
        /// 保存耗材资料
        /// </summary>
        /// <param name="pTable">耗材资料记录</param>
        /// <returns></returns>
        public int SaveMaterialMasterInfo(MaterialModel.MED_MATERIAL_MASTERDataTable pTable) {
            return MaterialBll.SaveMaterialMasterInfo(pTable);
        }

        /// <summary>
        /// 得到新生成的耗材编号
        /// </summary>
        /// <returns></returns>
        public string GetNewMaterialID() {
            return MaterialBll.GetNewMaterialID();
        }

        /// <summary>
        /// 保存血透耗材领用表
        /// </summary>
        /// <param name="pTable">耗材信息</param>
        /// <returns></returns>
        public int SaveHemoMaterialInfo(MaterialModel.MED_HEMO_MATERIALDataTable pTable) {
            return MaterialBll.SaveHemoMaterialInfo(pTable);
        }

        /// <summary>
        /// 得到领用耗材报表
        /// </summary>
        /// <param name="pUseMaterialID">领用耗材编号</param>
        /// <returns></returns>
        public MaterialModel.MED_HEMO_MATERIAL_REPORTDataTable GetMaterialReport(string pUseMaterialID) {
            return MaterialBll.GetMaterialReport(pUseMaterialID);
        }

        /// <summary>
        /// 根据透析号得到耗材领用数据
        /// </summary>
        /// <param name="pUseMaterialID">透析号</param>
        /// <returns></returns>
        public MaterialModel.MED_MATERIAL_MASTERDataTable GetUseMaterialList(string pHemoID) {
            return MaterialBll.GetUseMaterialList(pHemoID);
        }

        #region 耗材相关服务类

        public DrugModel.MED_MATERIAL_INPUTDataTable GetMedMaterialInputListNew(DateTime dtMoon)
        {
            return MaterialBll.GetMedMaterialInputListNew(dtMoon);

        }


        public int SaveMedMaterialInputNew(DrugModel.MED_MATERIAL_INPUTDataTable data)
        {
            return MaterialBll.SaveMedMaterialInputNew(data);
        }

        public DrugModel.MED_MATERIAL_OUTPUTDataTable GetMedMaterialOutputListNew(DateTime dtMoon)
        {
            return MaterialBll.GetMedMaterialOutputListNew(dtMoon);
        }


        public int SaveMedMaterialOutputNew(DrugModel.MED_MATERIAL_OUTPUTDataTable data)
        {
            return MaterialBll.SaveMedMaterialOutputNew(data);
        }


        public DrugModel.MED_MATERIAL_INPUTMASTERDataTable GetMedMaterialListByTypeId(string modetypeId)
        {
            return MaterialBll.GetMedMaterialListByTypeId(modetypeId);
        }


        public int CheckMaterialInOutStore(string checker)
        {
            return MaterialBll.CheckMaterialInOutStore(checker);
        }


        public int SaveFollowUp(DrugModel.MED_PATIENT_FOLLOWUPDataTable data)
        {
            return MaterialBll.SaveFollowUp(data);
        }

        public DrugModel.MED_PATIENT_FOLLOWUPDataTable GetFollowUpByData(DateTime dtStart, DateTime dtEnd, string hemoId)
        {
            return MaterialBll.GetFollowUpByData(dtStart, dtEnd,hemoId);
        }


        public DrugModel.MED_PATIENT_FOLLOWUPDataTable GetFollowUpByHemoID(DateTime dtStart, string hemoid)
        {
            return MaterialBll.GetFollowUpByHemoID(dtStart, hemoid);
        }


        public DrugModel.MED_MATERIAL_INPUTDataTable GetMedMaterialInputMaster(DateTime dtStar, DateTime dtEnd)
        {
            return MaterialBll.GetMedMaterialInputMaster(dtStar,dtEnd);

        }
        public DrugModel.MED_MATERIAL_INPUTDataTable GetMedMaterialInputDetail(DateTime dtStar, DateTime dtEnd)
        {
            return MaterialBll.GetMedMaterialInputDetail(dtStar,dtEnd);

        }

        public DrugModel.MED_MATERIAL_INPUTDataTable GetMedMaterialInputDetailByCodeAndBatchNum(DateTime dtMonth, string code, string batchNum)
        {
            return MaterialBll.GetMedMaterialInputDetailByCodeAndBatchNum(dtMonth, code, batchNum);
        }

        public DrugModel.MED_MATERIAL_OUTPUTDataTable GetMedMaterialOutputMaster(DateTime dtMoonStar,DateTime dtEndStar)
        {
            return MaterialBll.GetMedMaterialOutputMaster(dtMoonStar,dtEndStar);
        }
        public DrugModel.MED_MATERIAL_OUTPUTDataTable GetMedMaterialOutputDetail(DateTime dtMoonStar,DateTime dtEndStar)
        {
            return MaterialBll.GetMedMaterialOutputDetail(dtMoonStar,dtEndStar);
        }


        public DrugModel.MED_MATERIAL_OUTPUTDataTable GetMedMaterialOutputDetailByCodeAndBatchNum(DateTime dtMonth, string code, string batchNum)
        {
            return MaterialBll.GetMedMaterialOutputDetailByCodeAndBatchNum(dtMonth, code, batchNum);
        }
        public int DeleteMaterialInfo(string pMaterialID)
        {
            return MaterialBll.DeleteMaterialInfo(pMaterialID);
        }


        public int DeleteMaterialInPut(string inputId)
        {
            return MaterialBll.DeleteMaterialInPut(inputId);

        }

        public int DeleteMaterialOutPut(string outPutId)
        {
            return MaterialBll.DeleteMaterialOutPut(outPutId);

        }



        public System.Data.DataTable GetMaterialStoreInOutByCode(string code)
        {
            return MaterialBll.GetMaterialStoreInOutByCode(code);
        }


        public DrugModel.MED_MATERIAL_OUTPUTDataTable GetOutPutByCode(string code)
        {
            return MaterialBll.GetOutPutByCode(code);
        }
        #endregion
    }
}
