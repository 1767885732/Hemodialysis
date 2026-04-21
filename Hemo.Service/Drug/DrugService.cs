/*----------------------------------------------------------------
      // Copyright (C) 2005 (北京)医疗科技发展有限公司
      // 文件名：IPatient.cs
      // 文件功能描述：药品主档、药厂设置相关数据服务层
      // 创建标识：刘超-2013-3-19
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
using System.Data;

namespace Hemo.Service {
    public class DrugService : MarshalByRefObject, IDrug {

        #region 药品主档
        /// <summary>
        /// 得到全部药品主档列表
        /// </summary>
        /// <returns></returns>
        public DrugModel.MED_DRUG_MASTERDataTable GetDrugMasterList() {
            return DrugBll.GetDrugMasterList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DrugModel.MED_DRUG_MASTERDataTable GetDaleGateDrugMasterList()
        {
            return DrugBll.GetDaleGateDrugMasterList();
        }
        /// <summary>
        /// 根据查询参数得到药品主挡数据
        /// </summary>
        /// <param name="pTable">收集的查询条件TABLE</param>
        /// <returns></returns>
        public DrugModel.MED_DRUG_MASTERDataTable GetDrugMasterListByParams(DrugModel.MED_DRUG_MASTERDataTable pTable) {
            return DrugBll.GetDrugMasterListByParams(pTable);
        }

        /// <summary>
        /// 根据药品编号得到数据
        /// </summary>
        /// <param name="pDrugCode">药品编号</param>
        /// <returns></returns>
        public DrugModel.MED_DRUG_MASTERDataTable GetDrugMasterListByDrugCode(string pDrugCode) {
            return DrugBll.GetDrugMasterListByDrugCode(pDrugCode);
        }

        /// <summary>
        /// 保存药品主档资料
        /// </summary>
        /// <param name="pTable">药品主档记录</param>
        /// <returns></returns>
        public int SaveDrugMasterInfo(DrugModel.MED_DRUG_MASTERDataTable pTable) {
            return DrugBll.SaveDrugMasterInfo(pTable);
        }

        /// <summary>
        /// 根据药名从药库同步药
        /// </summary>
        /// <param name="drugName"></param>
        /// <returns></returns>
        public int DownDrugFromBaseByName(string drugName)
        {
            return DrugBll.DownDrugFromBaseByName(drugName);
        }

        /// <summary>
        /// 得到新生成的药品编号
        /// </summary>
        /// <returns></returns>
        public string GetNewDrugCode() {
            return DrugBll.GetNewDrugCode();
        }
        #endregion

        #region 药厂设置
        /// <summary>
        /// 得到全部药厂列表
        /// </summary>
        /// <returns></returns>
        public DrugModel.MED_DRUG_FIRMDataTable GetDrugFirmList() {
            return DrugBll.GetDrugFirmList();
        }

        /// <summary>
        /// 根据厂商类别得到厂商列表,
        /// </summary>
        /// <param name="pFirmType">药品厂商:FirmType=0,耗材厂商:FirmType=1</param>
        /// <returns></returns>
        public DrugModel.MED_DRUG_FIRMDataTable GetDrugFirmListByFirmType(string pFirmType) {
            return DrugBll.GetDrugFirmListByFirmType(pFirmType);
        }


        /// <summary>
        /// 根据查询参数得到药品厂商数据
        /// </summary>
        /// <param name="pTable">收集的查询条件TABLE</param>
        /// <returns></returns>
        public DrugModel.MED_DRUG_FIRMDataTable GetDrugFirmListByParams(DrugModel.MED_DRUG_FIRMDataTable pTable) {
            return DrugBll.GetDrugFirmListByParams(pTable);
        }

        /// <summary>
        /// 根据药厂编号得到数据
        /// </summary>
        /// <param name="pDrugCode">药厂编号</param>
        /// <returns></returns>
        public DrugModel.MED_DRUG_FIRMDataTable GetDrugFrimListByFirmID(string pFirmID) {
            return DrugBll.GetDrugFrimListByFirmID(pFirmID);
        }

        /// <summary>
        /// 保存药厂资料
        /// </summary>
        /// <param name="pTable">药厂数据</param>
        /// <returns></returns>
        public int SaveDrugFirmInfo(DrugModel.MED_DRUG_FIRMDataTable pTable) {
            return DrugBll.SaveDrugFirmInfo(pTable);
        }
        /// <summary>
        /// 删除药厂信息
        /// </summary>
        /// <param name="pFirmID"></param>
        /// <returns></returns>
        public int DeleteDrugFirmInfo(string pFirmID)
        {
            return DrugBll.DeleteDrugFirmInfo(pFirmID);
        }
        /// <summary>
        /// 得到新生成的药厂编号
        /// </summary>
        /// <returns></returns>
        public string GetNewFirmID() {
            return DrugBll.GetNewFirmID();
        }
        #endregion

        #region 药品耗材库存管理
        /// <summary>
        /// 保存药品耗材入库信息
        /// </summary>
        /// <param name="pTable">药品耗材入库信息</param>
        /// <returns></returns>
        public int SaveMedMaterialInput(DrugModel.MED_MATERIAL_INPUTDataTable pTable) {
            return DrugBll.SaveMedMaterialInput(pTable);
        }

        /// <summary>
        /// 得到入库药品耗材列表信息
        /// </summary>
        /// <returns>返回要耗材入库信息</returns>
        public DataTable GetMedMaterialInputList() {
            return DrugBll.GetMedMaterialInputList();
        }

        /// <summary>
        /// 根据ID得到一条入库耗材信息
        /// </summary>
        /// <param name="pID">ID编号</param>
        /// <returns></returns>
        public DrugModel.MED_MATERIAL_INPUTDataTable GetMedMaterialInputByID(string pID) {
            return DrugBll.GetMedMaterialInputByID(pID);
        }

        /// <summary>
        /// 根据透析号和药品编号得到入库的价格和数量
        /// </summary>
        /// <param name="pCode">药品编号</param>
        /// <param name="pHemoID">透析号</param>
        /// <returns>入库数量和价格</returns>
        public DataTable GetMedMaterialInputByHemoIdAndCode(string pCode, string pHemoID) {
            return DrugBll.GetMedMaterialInputByHemoIdAndCode(pCode, pHemoID);
        }

        /// <summary>
        /// 根据透析号和药品编号得到出库的价格和数量
        /// </summary>
        /// <param name="pCode">药品编号</param>
        /// <param name="pHemoID">透析号</param>
        /// <returns>出库数量和价格</returns>
        public DataTable GetMedMaterialOutputByHemoIdAndCode(string pCode, string pHemoID) {
            return DrugBll.GetMedMaterialOutputByHemoIdAndCode(pCode, pHemoID);
        }

        /// <summary>
        /// 根据透析号和药品编号得到实际库存数量
        /// </summary>
        /// <param name="pCode">药品编号</param>
        /// <param name="pHemoID">透析号</param>
        /// <returns>实际库存数量</returns>
        public DrugModel.MED_MATERIAL_CHECKDataTable GetMedMaterialCheckByHemoIdAndCode(string pCode, string pHemoID) {
            return DrugBll.GetMedMaterialCheckByHemoIdAndCode(pCode, pHemoID);
        }

        /// <summary>
        /// 保存药品耗材入库信息
        /// </summary>
        /// <param name="pTable">药品耗材出库信息</param>
        /// <returns></returns>
        public int SaveMedMaterialOutput(DrugModel.MED_MATERIAL_OUTPUTDataTable pTable) {
            return DrugBll.SaveMedMaterialOutput(pTable);
        }

        /// <summary>
        /// 根据ID得到一条出库耗材信息
        /// </summary>
        /// <param name="pID">ID编号</param>
        /// <returns></returns>
        public DrugModel.MED_MATERIAL_OUTPUTDataTable GetMedMaterialOutputByID(string pID) {
            return DrugBll.GetMedMaterialOutputByID(pID);
        }

        /// <summary>
        /// 得到出库药品耗材列表信息
        /// </summary>
        /// <returns>返回要耗材出库信息</returns>
        public DataTable GetMedMaterialOutputList() {
            return DrugBll.GetMedMaterialOutputList();
        }

        /// <summary>
        /// 保存药品耗材盘点信息
        /// </summary>
        /// <param name="pTable">药品耗材盘点信息</param>
        /// <returns></returns>
        public int SaveMedMaterialCheck(DrugModel.MED_MATERIAL_CHECKDataTable pTable) {
            return DrugBll.SaveMedMaterialCheck(pTable);
        }

        /// <summary>
        /// 得到库存盘点药品耗材列表信息
        /// </summary>
        /// <returns>返回要耗材盘点信息</returns>
        public DrugModel.MED_MATERIAL_CHECKDataTable GetMedMaterialCheckList(DateTime dtMoon,string checker)
        {
            return DrugBll.GetMedMaterialCheckList(dtMoon,checker);
        }
        /// <summary>
        /// 获取所有的盘点记录的时间和日期
        /// </summary>
        /// <returns></returns>
        public DrugModel.MED_MATERIAL_CHECKDataTable GetMedMaterialCheckHisList()
        {
            return DrugBll.GetMedMaterialCheckHisList();
 
        }



        /// <summary>
        /// 事务更新出库状态和盘库信息表数据
        /// </summary>
        /// <param name="pOutTable">出库表</param>
        /// <param name="pCheckTable">盘库表</param>
        /// <returns></returns>
        public bool SaveDeleteOutputAndCheckMaterialData(DrugModel.MED_MATERIAL_OUTPUTDataTable pOutTable, DrugModel.MED_MATERIAL_CHECKDataTable pCheckTable) {
            return DrugBll.SaveDeleteOutputAndCheckMaterialData(pOutTable, pCheckTable);
        }

        /// <summary>
        /// 事务更新入库状态和盘库信息表数据
        /// </summary>
        /// <param name="pInTable">入库表</param>
        /// <param name="pCheckTable">盘库表</param>
        /// <returns></returns>
        public bool SaveDeleteInputAndCheckMaterialData(DrugModel.MED_MATERIAL_INPUTDataTable pInTable, DrugModel.MED_MATERIAL_CHECKDataTable pCheckTable) {
            return DrugBll.SaveDeleteInputAndCheckMaterialData(pInTable, pCheckTable);
        }
        #endregion

        #region 单个病人药品出入库管理
        /// <summary>
        /// 根据透析号查询病人药品入库
        /// </summary>
        /// <param name="pID">透析号</param>
        /// <returns></returns>
        public DataTable QueryPatientDrugInputById(string pID, DateTime beginTime, DateTime endTime)
        {
            return DrugBll.QueryPatientDrugInputById(pID,beginTime,endTime);
        }

        /// <summary>
        /// 根据透析号查询病人药品出库
        /// </summary>
        /// <param name="pID">透析号</param>
        /// <returns></returns>
        public DataTable QueryPatientDrugOutputById(string pID, DateTime beginTime, DateTime endTime)
        {
            return DrugBll.QueryPatientDrugOutputById(pID, beginTime, endTime);
        }

        /// <summary>
        /// 根据病人透析号和药品code查询病人药品托管剩余
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="pCode"></param>
        /// <returns></returns>
        public DataTable GetPatientDrugNumberByParam(string pID, string pCode)
        {
            return DrugBll.GetPatientDrugNumberByParam(pID, pCode);
        }

        public int SavePatientDrugInput(DrugModel.MED_PATIENT_DRUG_INPUTDataTable dt)
        {
            return DrugBll.SavePatientDrugInput(dt);
        }
        public int SavePatientDrugOutput(DrugModel.MED_PATIENT_DRUG_OUTPUTDataTable dt)
        {
            return DrugBll.SavePatientDrugOutput(dt);
        }
        public int UpdatePatientDrugInputByParam(string pID, string pCode,decimal sum)
        {
            return DrugBll.UpdatePatientDrugInputByParam(pID, pCode,sum);
        }
        public int UpdatePatientDrugInputByOutPutParam(string pID, string pCode, decimal sum, string pInPutId)
        {
            return DrugBll.UpdatePatientDrugInputByOutPutParam(pID, pCode, sum, pInPutId);
        }
        public int UpdatePatientDrugInputRemainByParam(string pID, decimal remain)
        {
            return DrugBll.UpdatePatientDrugInputRemainByParam(pID, remain);
        }
        public int DeletePatientDrugInputByID(string pID)
        {
            return DrugBll.DeletePatientDrugInputByID(pID);
        }
        public int DeletePatientDrugOutputByID(string pID)
        {
            return DrugBll.DeletePatientDrugOutputByID(pID);
        }
        public int UpdatePatientDrugInputStatusByID(string pID)
        {
            return DrugBll.UpdatePatientDrugInputStatusByID(pID);
        }
        public DrugModel.MED_DRUG_MASTERDataTable GetDrugInputList(string currentHemoId)
        {
            return DrugBll.GetDrugInputList(currentHemoId);
        }





        public ReportRelationModel.PatientDrugOutPutPrintDataTable QueryPatientDrugOutPutToPrint(string pID, DateTime beginTime, DateTime endTime)
        {
            return DrugBll.QueryPatientDrugOutPutToPrint(pID, beginTime, endTime);
        }

        #endregion
    }
}
