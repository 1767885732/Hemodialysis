/*----------------------------------------------------------------
// Copyright (C) 2005 (北京)医疗科技发展有限公司
// 文件名：HemodialysisService.cs
// 文件功能描述：HemodialysisService服务类
// 创建标识：
// 修改时间:2014-4-30
// 修改人：吕志强
// 修改描述：添加根据病人透析号获取透析参数数据服务方法
----------------------------------------------------------------*/

using System;
using System.Data;
using Hemo.Business.Config;
using Hemo.IService.Config;
using Hemo.Model;

namespace Hemo.Service.Config
{
    public class HemodialysisService : MarshalByRefObject, IHemodialysis
    {
        #region IHemodialysis 成员

        public HemodialysisModel.MED_HEMO_RECIPEDataTable GetAllRecipe()
        {
            return HemodialysisBll.GetAllRecipe();
        }

        public HemodialysisModel.MED_BEFORE_HEMODIALYSIS_SIGNDataTable GetBeforeHemodialysisSignList()
        {
            return HemodialysisBll.GetBeforeHemodialysisSignList();
        }

        public int SaveBeforeHemodialysisSignInfo(HemodialysisModel.MED_BEFORE_HEMODIALYSIS_SIGNDataTable hemodialysisDataTable)
        {
            return HemodialysisBll.SaveBeforeHemodialysisSignInfo(hemodialysisDataTable);
        }

        /// <summary>
        /// 保存长期处方数据
        /// </summary>
        /// <param name="pRecipeDataTable"></param>
        /// <returns></returns>
        public int SaveRecipe(HemodialysisModel.MED_HEMO_RECIPEDataTable pRecipeDataTable)
        {
            return HemodialysisBll.SaveRecipe(pRecipeDataTable);
        }
        //更新临时用药状态
        public int SaveExeDrugStatus(string status, string cure_drug_id)
        {
            return HemodialysisBll.SaveExeDrugStatus(status, cure_drug_id);
        }

        //更新长期用药状态
        public int SaveExeDrugLongStatus(string status, string cure_drug_id)
        {
            return HemodialysisBll.SaveExeDrugLongStatus(status, cure_drug_id);
        }

        /// <summary>
        /// 根据处方ID列表更新开方医生签名
        /// </summary>
        /// <param name="pRecipeIDList">处方ID列表</param>
        /// <param name="pUserID">医生ID</param>
        /// <returns></returns>
        public int SaveRecipeUserIDByRecipeIDList(string pRecipeIDList, string pUserID)
        {
            return HemodialysisBll.SaveRecipeUserIDByRecipeIDList(pRecipeIDList, pUserID);
        }

        /// <summary>
        /// 根据长期处方ID得到对应处方数据
        /// </summary>
        /// <param name="pRecipeID">处方ID</param>
        /// <returns>单条处方数据</returns>
        public HemodialysisModel.MED_HEMO_RECIPEDataTable GetRecipeByRecipeID(string pRecipeID)
        {
            return HemodialysisBll.GetRecipeByRecipeID(pRecipeID);
        }

        /// <summary>
        /// 根据长期处方ID得到对应处方数据
        /// </summary>
        /// <param name="pHemodialysisID">透析号</param>
        /// <returns>处方列表数据</returns>
        public HemodialysisModel.MED_HEMO_RECIPEDataTable GetRecipeByHemodialysisID(string pHemodialysisID)
        {
            return HemodialysisBll.GetRecipeByHemodialysisID(pHemodialysisID);
        }

        /// <summary>
        /// 根据透析编号获取长期处方
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_HEMO_RECIPEDataTable GetLongRecipeByHemodialysisID(string hemoId)
        {
            return HemodialysisBll.GetLongRecipeByHemodialysisID(hemoId);
        }

        /// <summary>
        /// 根据长期处方ID和日期得到对应处方数据
        /// </summary>
        /// <param name="pHemodialysisID">透析号</param>
        /// <param name="RecipeDate">处方日期</param>
        /// <returns>透析列表数据</returns>
        public HemodialysisModel.MED_HEMO_RECIPEDataTable GetRecipeByHemodialysisIDAndDate(string pHemodialysisID, DateTime pRecipeDate)
        {
            return HemodialysisBll.GetRecipeByHemodialysisIDAndDate(pHemodialysisID, pRecipeDate);
        }

        public HemodialysisModel.GetPatientRecipeInfoDataTable GetPatientRecipeInfo(string pHemodialysisID, string pDate)
        {
            return HemodialysisBll.GetPatientRecipeInfo(pHemodialysisID, pDate);
        }

        public int ExecuteProLogInfos()
        {
            return HemodialysisBll.ExecuteProLogInfos();
        }

        /// <summary>
        /// 根据病人透析号得到状态为1的处方数量
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <returns></returns>
        public int GetRecipeStatusCountByHemoID(string pHemoID)
        {
            return HemodialysisBll.GetRecipeStatusCountByHemoID(pHemoID);
        }

        /// <summary>
        /// 根据病人ID得到病人数量
        /// </summary>
        /// <param name="pPatientID">病人住院ID</param>
        /// <returns></returns>
        public int GetPatientCountByPatientID(string pPatientID)
        {
            return HemodialysisBll.GetPatientCountByPatientID(pPatientID);
        }


        /// <summary>
        /// 根据Inp_No号同步获取病人信息
        /// </summary>
        /// <param name="pInpNo">病人住院号</param>
        /// <returns>得到病人住院信息</returns>
        public HemodialysisModel.MED_PAT_MASTER_INDEXDataTable GetPatientMasterIndexByInpNo(string pInpNo, string pWardCode)
        {
            return HemodialysisBll.GetPatientMasterIndexByInpNo(pInpNo, pWardCode);
        }

        /// <summary>
        /// 根据病人住院号同步获取病人信息
        /// </summary>
        /// <param name="pPatientID">病人住院号</param>
        /// <returns>得到病人住院信息</returns>
        public HemodialysisModel.MED_PAT_MASTER_INDEXDataTable GetPatientMasterIndexByPatientID(string pPatientID, string pWardCode)
        {
            return HemodialysisBll.GetPatientMasterIndexByPatientID(pPatientID, pWardCode);
        }

        /// <summary>
        /// 获取全部病人信息列表从HIS表中
        /// </summary>
        public HemodialysisModel.MED_PAT_MASTER_INDEXDataTable GetPatientMasterIndexList(string pWardCode)
        {
            return HemodialysisBll.GetPatientMasterIndexList(pWardCode);
        }

        /// <summary>
        /// 得到新生成的处方编号
        /// </summary>
        /// <returns></returns>
        public string GetNewRecipeID()
        {
            return HemodialysisBll.GetNewRecipeID();
        }

        /// <summary>
        /// 获取所有启用的系统消息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_COMMON_MESSAGEDataTable GetAllMessage(decimal type)
        {
            return HemodialysisBll.GetAllMessage(type);
        }

        /// <summary>
        /// 保存系统消息
        /// </summary>
        /// <param name="messageDataTable"></param>
        /// <returns></returns>
        public int SaveMsgInfo(HemodialysisModel.MED_COMMON_MESSAGEDataTable messageDataTable)
        {
            return HemodialysisBll.SaveMsgInfo(messageDataTable);
        }

        /// <summary>
        /// 保存系统消息为已读
        /// </summary>
        /// <param name="msgID"></param>
        /// <returns></returns>
        public int SaveMsgInfoToMarkRead(string msgID)
        {
            return HemodialysisBll.SaveMsgInfoToMarkRead(msgID);
        }

        /// <summary>
        /// 根据透析号获取患者临时与长期处方执行状态与内容列表
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <returns></returns>
        public DataTable GetQueryRecipeList(string pHemoID)
        {
            return HemodialysisBll.GetQueryRecipeList(pHemoID);
        }


        /// <summary>
        /// 根据当前已排班人员的透析ID生成当日临时处方
        /// </summary>
        /// <returns></returns>
        public int SaveTodayRecipes()
        {
            return HemodialysisBll.SaveTodayRecipes();
        }

        /// <summary>
        /// 计算患者透析次数
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <returns></returns>
        public DataTable GetCureCountByHemoID(string pHemoID)
        {
            return HemodialysisBll.GetCureCountByHemoID(pHemoID);
        }


        /// <summary>
        /// 根据透析号得到对应治疗单数据SQL
        /// <param name="pHemoID">透析号</param>
        /// </summary>
        public DataTable GetMainCureListByHemoID(string pHemoID)
        {
            return HemodialysisBll.GetMainCureListByHemoID(pHemoID);
        }
        #endregion

        #region 治疗单

        /// <summary>
        /// 保存治疗单数据
        /// </summary>
        /// <param name="pRecipeDataTable"></param>
        /// <returns></returns>
        public int SaveCureMain(HemodialysisModel.MED_CURE_MAINDataTable pCureDataTable)
        {
            return HemodialysisBll.SaveCureMain(pCureDataTable);
        }

        /// <summary>
        /// 保存CRRT治疗单
        /// </summary>
        /// <param name="dtCure"></param>
        /// <returns></returns>
        public int SaveCRRTCureMain(HemodialysisModel.MED_CURE_MAIN_CRRTDataTable dtCure)
        {
            return HemodialysisBll.SaveCRRTCureMain(dtCure);
        }

        /// <summary>
        /// 得到新生成的治疗单编号
        /// </summary>
        /// <returns></returns>
        public string GetNewCureID()
        {
            return HemodialysisBll.GetNewCureID();
        }

        /// <summary>
        /// 根据治疗单编号得到治疗单数据
        /// </summary>
        /// <param name="pCureID"></param>
        public HemodialysisModel.MED_CURE_MAINDataTable GetMainCureByCureID(string pCureID)
        {
            return HemodialysisBll.GetMainCureByCureID(pCureID);
        }

        /// <summary>
        /// 根据病人透析号得到治疗数据
        /// </summary>
        /// <param name="pHemoID">病人透析号</param>
        public HemodialysisModel.MED_CURE_MAINDataTable GetMainCureByHemoID(string pHemoID)
        {
            return HemodialysisBll.GetMainCureByHemoID(pHemoID);
        }

        public HemodialysisModel.MED_CURE_MAINDataTable GetMainCureByHemoIDAndDate(string pHemoID, DateTime pBeginDate, DateTime pEndDate)
        {
            return HemodialysisBll.GetMainCureByHemoIDAndDate(pHemoID, pBeginDate, pEndDate);
        }

        /// <summary>
        /// 根据病人透析号获取透析参数数据
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable GetHemoParamsByHemoID(string hemoId)
        {
            return HemodialysisBll.GetHemoParamsByHemoID(hemoId);
        }

        /// <summary>
        /// 根据病人透析号和治疗单编号得到治疗单数量
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="CureCreateDate">透析日期</param>
        /// <returns>返回治疗单数量</returns>
        public int GetMainCureCountByCreateDate(string pHemoID, DateTime pCureCreateDate)
        {
            return HemodialysisBll.GetMainCureCountByCreateDate(pHemoID, pCureCreateDate);
        }

        /// <summary>
        /// 根据查询条件返回对应的透析病人单列表
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="pCureCreateDate">透析日期/排班日期</param>
        /// <param name="pBanCi">班次</param>
        /// <returns>对应的透析人员列表</returns>
        public DataTable GetPrintCureList(string pHemoID, string pCureCreateDate, string pBanCi, string pName)
        {
            return HemodialysisBll.GetPrintCureList(pHemoID, pCureCreateDate, pBanCi, pName);
        }
        public ConfigModel.MED_COMMON_ITEMLISTDataTable GetCommonItemListByItemType(string itemType)
        {
            return HemodialysisBll.GetCommonItemListByItemType(itemType);
        }
        /// <summary>
        /// 根据病人透析号和治疗方式分组得到治疗数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetMainCureGroupByHemoIDAndPurificationMode(DateTime beginCureCreateDate, DateTime endCureCreateDate)
        {
            return HemodialysisBll.GetMainCureGroupByHemoIDAndPurificationMode(beginCureCreateDate, endCureCreateDate);
        }

        /// <summary>
        /// 根据治疗单编号得到对应透析参数数据列表
        /// </summary>
        /// <param name="pCureID">治疗单编号</param>
        /// <returns></returns>
        public HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable GetHemoParametersByCureID(string pCureID)
        {
            return HemodialysisBll.GetHemoParametersByCureID(pCureID);
        }

        /// <summary>
        /// 得到用药记录
        /// </summary>
        /// <param name="pHemoID"></param>
        /// <param name="beginCreateTime"></param>
        /// <param name="endCreateTime"></param>
        /// <returns></returns>
        public DataTable GetDrugRecord(string pCureID, DateTime beginCreateTime, DateTime endCreateTime)
        {
            return HemodialysisBll.GetDrugRecord(pCureID, beginCreateTime, endCreateTime);
        }

        /// <summary>
        /// 根据治疗单编号得到对应已删除透析参数数据列表
        /// </summary>
        /// <param name="pCureID">治疗单编号</param>
        /// <returns></returns>
        public HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable GetDeleteHemoParametersByCureID(string pCureID)
        {
            return HemodialysisBll.GetDeleteHemoParametersByCureID(pCureID);
        }

        public HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable GetHemoParametersByHemoParamRow(string hemoParamId, int rowNumber1, int rowNumber2, string sqlByParams)
        {
            return HemodialysisBll.GetHemoParametersByHemoParamRow(hemoParamId, rowNumber1, rowNumber2, sqlByParams);
        }

        /// <summary>
        /// 根据透析参数ID获取透析参数列表数据
        /// </summary>
        /// <param name="hemoParamId"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable GetHemoParametersByHemoParamId(string hemoParamId)
        {
            return HemodialysisBll.GetHemoParametersByHemoParamId(hemoParamId);
        }
        /// <summary>
        /// 获取血压例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable GetBloodControlsReport(string hemoId, DateTime beginTime, DateTime endTime)
        {
            return HemodialysisBll.GetBloodControlsReport(hemoId, beginTime, endTime);
        }
        /// <summary>
        /// 得到对应透析参数数据列表
        /// </summary>
        /// <param name="pHemoID"></param>
        /// <param name="beginCreateTime"></param>
        /// <param name="endCreateTime"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable GetHemoParameters(string pHemoID, DateTime beginCreateTime, DateTime endCreateTime)
        {
            return HemodialysisBll.GetHemoParameters(pHemoID, beginCreateTime, endCreateTime);
        }

        public int UpdataCureDrugStateByParma(string statues, string hemoID, string comNo, DateTime createDate, string cureId, string recipeId,DateTime executeDt,string nuserId)
        {
            return HemodialysisBll.UpdataCureDrugStateByParma(statues, hemoID, comNo, createDate, cureId, recipeId, executeDt,nuserId);
        }

        public int UpdataCureDrugStateByParma(string statues, string hemoID, string comNo, string comSubNo, DateTime createDate, string cureId, string recipeId, DateTime executeDt, string nuserId)
        {
            return HemodialysisBll.UpdataCureDrugStateByParma(statues, hemoID, comNo, comSubNo, createDate, cureId, recipeId, executeDt, nuserId);
        }

        /// <summary>
        /// 得到对应透析参数配置数据列表
        /// </summary>
        /// <returns></returns>
        public HemoModel.MED_HEMODIALYSIS_PARAMS_TYPEDataTable GetHemoParametersType()
        {
            return HemodialysisBll.GetHemoParametersType();
        }

        /// <summary>
        /// 保存透析参数数据方法
        /// </summary>
        /// <param name="pHemoParametersDataTable">透析参数表</param>
        /// <returns></returns>
        public int SaveCureMainSaveHemoParameters(HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable pHemoParametersDataTable)
        {
            return HemodialysisBll.SaveHemoParameters(pHemoParametersDataTable);
        }

        /// <summary>
        /// 根据透析编号获取用药记录列表数据
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_DRUG_USE_RECORDDataTable GetDrugUseRecordListByHemoId(string hemoId)
        {
            return HemodialysisBll.GetDrugUseRecordListByHemoId(hemoId);
        }

        /// <summary>
        /// 根据治疗单编号得到对应给药数据列表
        /// </summary>
        /// <param name="pCureID">治疗单编号</param>
        /// <returns></returns>
        public HemodialysisModel.MED_CURE_DRUGDataTable GetCureDrugByCureID(string pCureID)
        {
            return HemodialysisBll.GetCureDrugByCureID(pCureID);
        }

        public HemodialysisModel.MED_CURE_DRUGDataTable GetCureDrugByHemoID(string pHemoID, DateTime currentDT)
        {
            return HemodialysisBll.GetCureDrugByHemoID(pHemoID, currentDT);
        }

        public HemodialysisModel.MED_CURE_DRUGDataTable GetCureDrugForPatientRecord(string hemoId)
        {
            return HemodialysisBll.GetCureDrugForPatientRecord(hemoId);
        }

        public HemodialysisModel.MED_CURE_DRUGDataTable GetValidCureDrugByHemoID(string pHemoID, DateTime DT)
        {
            return HemodialysisBll.GetValidCureDrugByHemoID(pHemoID, DT);
        }
        public HemodialysisModel.MED_CURE_DRUGDataTable GetValidCureDrugByRoomIdAndData(string pRoomId, string banchiId, DateTime dtStart, DateTime dtEnd, string hemoId)
        {
            return HemodialysisBll.GetValidCureDrugByRoomIdAndData(pRoomId, banchiId, dtStart, dtEnd, hemoId);
        }
        public HemodialysisModel.MED_CURE_DRUGDataTable GetValidCureDrugByHemoRecipeID(string pHemoID, string pRecipeId)
        {
            return HemodialysisBll.GetValidCureDrugByHemoRecipeID(pHemoID, pRecipeId);
        }
        public HemodialysisModel.MED_CURE_DRUGDataTable GetUnExcuteCureDrugByHemoID(string pHemoID, DateTime DT)
        {
            return HemodialysisBll.GetUnExcuteCureDrugByHemoID(pHemoID, DT);
        }

        public HemodialysisModel.MED_CURE_DRUGDataTable GetUnExcuteCureDrugByHemoRecipeId(string pHemoID, string recipeId)
        {
            return HemodialysisBll.GetUnExcuteCureDrugByHemoRecipeId(pHemoID, recipeId);

        }

        public HemodialysisModel.MED_CURE_LONGDRUGDataTable GetLongCureDrugByHemoID(string pHemoID) { return HemodialysisBll.GetLongCureDrugByHemoID(pHemoID); }
        /// <summary>
        /// 保存给药数据列表方法
        /// </summary>
        /// <param name="pCureDrugDataTable">给药数据表</param>
        /// <returns></returns>
        public int SaveCureDrug(HemodialysisModel.MED_CURE_DRUGDataTable pCureDrugDataTable)
        {
            return HemodialysisBll.SaveCureDrug(pCureDrugDataTable);
        }
        public int SaveCureLongDrug(HemodialysisModel.MED_CURE_LONGDRUGDataTable pCureDrugDataTable)
        {
            return HemodialysisBll.SaveCureLongDrug(pCureDrugDataTable);
        }
        public int SaveCureDrugTemplate(HemodialysisModel.MED_CURE_DRUG_TEMPLATEDataTable pCureDrugTemplateDT)
        {
            return HemodialysisBll.SaveCureDrugTemplate(pCureDrugTemplateDT);
        }


        public HemodialysisModel.MED_CURE_DRUG_TEMPLATEDataTable GetTemplateByParmas(string docID)
        {
            return HemodialysisBll.GetTemplateByParmas(docID);
        }
        /// <summary>
        /// 保存包括治疗单主表、给药表、透析参数表的数据集
        /// </summary>
        /// <param name="pDataSet">治疗单数据集</param>
        /// <returns></returns>
        public bool SaveAllCure(DataSet pDataSet)
        {
            return HemodialysisBll.SaveAllCure(pDataSet);
        }

        /// <summary>
        /// 根据治疗单编号，返回包括治疗单主表、给药表、透析参数表的数据集
        /// </summary>
        /// <param name="pCureID">治疗单编号</param>
        /// <returns>返回治疗单数据集</returns>
        public DataSet GetAllCure(string pCureID)
        {
            return HemodialysisBll.GetAllCure(pCureID);
        }

        /// <summary>
        /// 得到透析列表数据
        /// </summary>
        /// <param name="pDialysisDate">透析日期</param>
        /// <param name="pBanciID">班次</param>
        /// <param name="pName">患者姓名</param>
        /// <param name="pHemoID">患者透析号</param>
        /// <returns>透析单列表</returns>
        public HemodialysisModel.GetCureListDataTable GetCureList(string pDialysisDate, string pBanciID, string pName, string pHemoID)
        {
            return HemodialysisBll.GetCureList(pDialysisDate, pBanciID, pName, pHemoID);
        }

        /// <summary>
        /// 根据透析号和处方号返回处方信息与人员信息
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="pRecipeID">处方号</param>
        /// <returns>处方信息</returns>
        public DataSet GetRecipeAndPatientInfo(string pHemoID, string pRecipeID)
        {
            return HemodialysisBll.GetRecipeAndPatientInfo(pHemoID, pRecipeID);
        }

        /// <summary>
        /// 根据透析号和净化方式得到对应的处方数量
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="pPurificationMode">净化方式</param>
        /// <returns>对应的处方数量</returns>
        public int GetRecipeCountByPurificationMode(string pHemoID, string pPurificationMode)
        {
            return HemodialysisBll.GetRecipeCountByPurificationMode(pHemoID, pPurificationMode);
        }

        public int GetTempRecipeCountByDate(string pHemoID, DateTime pDate)
        {
            return HemodialysisBll.GetTempRecipeCountByDate(pHemoID, pDate);
        }

        /// <summary>
        /// 得到该处方是否已经存在治疗单中的数量
        /// </summary>
        /// <param name="pRecipeID">处方编号</param>
        /// <param name="pPurificationMode">净化方式</param>
        public int GetRecipeCountInCureList(string pRecipeID)
        {
            return HemodialysisBll.GetRecipeCountInCureList(pRecipeID);
        }


        /// <summary>
        /// 得到时间段内不同透析类型的透析次数
        /// </summary>
        /// <param name="beginCureCreateDate">开始日期</param>
        /// <param name="endCureCreateDate">结束日期</param>
        /// <returns></returns>
        public DataTable GetAllCureTypeCount(DateTime beginCureCreateDate, DateTime endCureCreateDate)
        {
            return HemodialysisBll.GetAllCureTypeCount(beginCureCreateDate, endCureCreateDate);
        }

        public DataTable GetAllCureCountByMonth(DateTime _bgionTime, DateTime _endTime)
        {
            return HemodialysisBll.GetAllCureCountByMonth(_bgionTime, _endTime);
        }

        /// <summary>
        /// 得到全部病人乙肝、丙肝、梅毒、HIV检查结果列表
        /// </summary>
        /// <returns></returns>
        public HemodialysisModel.MED_INFECTIOUS_CHECKDataTable GetMedInfectiousCheckList(string pHEMODIALYSIS_ID)
        {
            return HemodialysisBll.GetMedInfectiousCheckList(pHEMODIALYSIS_ID);
        }

        public int UpdateMedInfectiousInfoByID(string pInfectiousCheckID)
        {
            return HemodialysisBll.UpdateMedInfectiousInfoByID(pInfectiousCheckID);
        }


        /// <summary>
        /// 得到全部病人乙肝、丙肝、梅毒、HIV检查结果列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetMedInfectiousCheck()
        {
            return HemodialysisBll.GetMedInfectiousCheck();
        }

        /// <summary>
        /// 保存传染病检查数据
        /// </summary>
        /// <param name="pInfectiousCheckDataTable"></param>
        /// <returns></returns>
        public int SaveMedInfectiousCheck(HemodialysisModel.MED_INFECTIOUS_CHECKDataTable pInfectiousCheckDataTable)
        {
            return HemodialysisBll.SaveMedInfectiousCheck(pInfectiousCheckDataTable);
        }

        public HemodialysisModel.MED_INFECTIOUS_CHECKDataTable GetMedInfectiousInfoByID(string pInfectiousCheckID)
        {
            return HemodialysisBll.GetMedInfectiousInfoByID(pInfectiousCheckID);
        }


        public DataSet GetPatientGuideVisitInfo(string pHemoID, DateTime pDialysisDate)
        {
            return HemodialysisBll.GetPatientGuideVisitInfo(pHemoID, pDialysisDate);
        }

        /// <summary>
        /// 得到疾病ICD分类
        /// </summary>
        /// <returns>ICD疾病分类列表</returns>
        public HemodialysisModel.MED_ICD_TYPEDataTable GetIcdType()
        {
            return HemodialysisBll.GetIcdType();
        }

        /// <summary>
        /// 根据ICD分类编码取出对应的ICD病种
        /// </summary>
        /// <param name="pICD">ICD编码</param>
        /// <returns></returns>
        public HemodialysisModel.MED_ICD_LISTDataTable GetIcdListByID(string pICD)
        {
            return HemodialysisBll.GetIcdListByID(pICD);
        }

        /// <summary>
        /// 根据ICD分类跨度编码取出对应的ICD病种
        /// </summary>
        /// <param name="pIdList">ICD分类跨度</param>
        /// <returns></returns>
        public DataTable GetIcdListByIDList(string pIdList)
        {
            return HemodialysisBll.GetIcdListByIDList(pIdList);
        }


        /// <summary>
        /// 删除透析参数
        /// </summary>
        /// <param name="pID">透析参数ID</param>
        /// <returns></returns>
        public int DeleteHemodialysisParametersByID(string pID)
        {
            return HemodialysisBll.DeleteHemodialysisParametersByID(pID);
        }

        /// <summary>
        /// 获取血透治疗统计列表
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataTable GetHemoCureCountList(string year)
        {
            return HemodialysisBll.GetHemoCureCountList(year);
        }


        /// <summary>
        /// 得到患者上次治疗单内容
        /// </summary>
        /// <param name="pHemoID">透析ID</param>
        /// <returns></returns>
        public string GetLastTimeCureDataByID(string pHemoID, DateTime pCURE_CREATE_DATE)
        {
            return HemodialysisBll.GetLastTimeCureDataByID(pHemoID, pCURE_CREATE_DATE);
        }

        public DataTable GetDryWeightListByHemoID(string pHemoID)
        {
            return HemodialysisBll.GetDryWeightListByHemoID(pHemoID);
        }

        public DataTable GetRecentCureInfoByHemoId(string hemoId)
        {
            return HemodialysisBll.GetRecentCureInfoByHemoId(hemoId);
        }

        public DataTable GetRecentPressureByHemoId(string hemoId)
        {
            return HemodialysisBll.GetRecentPressureByHemoId(hemoId);
        }

        public DataTable GetPastCureInfoByHemoId(string hemoId)
        {
            return HemodialysisBll.GetPastCureInfoByHemoId(hemoId);
        }

        public DataTable GetPastPressureByHemoId(string hemoId)
        {
            return HemodialysisBll.GetPastPressureByHemoId(hemoId);
        }



        public DataTable GetPatientCureAndPastPressureByParam(string hemoId, int minData, int maxData)
        {
            return HemodialysisBll.GetPatientCureAndPastPressureByParam(hemoId, minData, maxData);

        }
        public DataTable GetPatientCureAndPastPressureByHemoId(string hemoId)
        {
            return HemodialysisBll.GetPatientCureAndPastPressureByHemoId(hemoId);
        }

        /// <summary>
        /// 根据治疗单ID和班次获取CRRT治疗单记录
        /// </summary>
        /// <param name="cureId"></param>
        /// <param name="banci"></param>
        /// <param name="createDate"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_CURE_MAIN_CRRTDataTable GetCRRTCureByCureIdAndBanci(string cureId, string banci, DateTime createDate)
        {
            return HemodialysisBll.GetCRRTCureByCureIdAndBanci(cureId, banci, createDate);
        }

        /// <summary>
        /// 根据治疗单ID获取CRRT治疗单记录
        /// </summary>
        /// <param name="cureId"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_CURE_MAIN_CRRTDataTable GetCRRTCureByCureId(string cureId)
        {
            return HemodialysisBll.GetCRRTCureByCureId(cureId);
        }

        #endregion

        #region 疾病相关
        public ConfigModel.MED_COMMON_ITEMLISTDataTable GetItemListByHemoIDandItemType(string pHemoID, string pItemType)
        {
            return HemodialysisBll.GetItemListByHemoIDandItemType(pHemoID, pItemType);
        }
        /// <summary>
        /// 获取患者的透析通路
        /// </summary>
        /// <param name="phemoId"></param>
        /// <returns></returns>
        public HemoModel.MED_VASCULAR_ACCESSDataTable GetPatientVasular_AccessDt(string phemoId)
        {
            return HemodialysisBll.GetPatientVasular_AccessDt(phemoId);
        }
        public int UpdateIcdPinYin()
        {
            return HemodialysisBll.UpdateIcdPinYin();
        }

        /// <summary>
        /// 根据疾病名称和拼音码得到疾病列表
        /// </summary>
        /// <param name="pICD_NAME">疾病名称或拼音码</param>
        /// <returns></returns>
        public HemodialysisModel.MED_ICD_LISTDataTable GetIcdListByName(string pICD_NAME)
        {
            return HemodialysisBll.GetIcdListByName(pICD_NAME);
        }

        public string GetCureID(string pDialysisDate, string pHemoID)
        {
            return HemodialysisBll.GetCureID(pDialysisDate, pHemoID);
        }

        public int DeleteCureORLongDrugByID(string drugType, string cureID)
        {
            return HemodialysisBll.DeleteCureORLongDrugByID(drugType, cureID);
        }

        public DataTable GetUNExcuteOrdersbyData(DateTime dt)
        {
            return HemodialysisBll.GetUNExcuteOrdersbyData(dt);
        }

        public DataTable GetPamarsDrugInfo(string pCureID, DateTime pCreateDate)
        {
            return HemodialysisBll.GetPamarsDrugInfo(pCureID, pCreateDate);
        }

        public DataTable GetCurePurificationModeCountByDate(string pCureDate)
        {
            return HemodialysisBll.GetCurePurificationModeCountByDate(pCureDate);
        }

        public int GetCleanUpTimes(string pHemoID)
        {
            return HemodialysisBll.GetCleanUpTimes(pHemoID);
        }

        public HemodialysisModel.MED_CURE_MAINDataTable GetMainCureByRecipeId(string pRecipeId)
        {
            return HemodialysisBll.GetMainCureByRecipeId(pRecipeId);
        }


        public string GetOrderComNo()
        {
            return HemodialysisBll.GetOrderComNo();
        }

        public int SaveHealthEducationInfo(HemodialysisModel.MED_HEALTH_EDUCATIONDataTable healthDataTable)
        {
            return HemodialysisBll.SaveHealthEducationInfo(healthDataTable);
        }

        public HemodialysisModel.MED_HEALTH_EDUCATIONDataTable GetHealthEducationByHemoID(string pHemoID)
        {
            return HemodialysisBll.GetHealthEducationByHemoID(pHemoID);
        }

        public DataTable GetHealthEducationListByHemoID(string pHemoID)
        {
            return HemodialysisBll.GetHealthEducationListByHemoID(pHemoID);
        }

        public HemodialysisModel.MED_HEALTH_EDUCATIONDataTable GetHealthEducationByHemoIdAndId(string pHemoID, string id)
        {
            return HemodialysisBll.GetHealthEducationByHemoIdAndId(pHemoID, id);
        }


        public DataTable GetHealthEducationReportByHemoID(string pHemoID, string pID)
        {
            return HemodialysisBll.GetHealthEducationReportByHemoID(pHemoID, pID);
        }


        public DataTable GetBaseHemoInfo(DateTime _begionTime, DateTime _endTime)
        {
            return HemodialysisBll.GetBaseHemoInfo(_begionTime, _endTime);
        }


        public DataTable GetSexScale(DateTime _begionTime, DateTime _endTime)
        {
            return HemodialysisBll.GetSexScale(_begionTime, _endTime);
        }

        public DataTable GetAgeScale(DateTime _begionTime, DateTime _endTime)
        {
            return HemodialysisBll.GetAgeScale(_begionTime, _endTime);
        }

        public string GetAllHemoCount(DateTime _begionTime, DateTime _endTime)
        {
            return HemodialysisBll.GetAllHemoCount(_begionTime, _endTime);
        }

        public DataTable GetInfectiousScale(DateTime _begionTime, DateTime _endTime)
        {
            return HemodialysisBll.GetInfectiousScale(_begionTime, _endTime);
        }

        public DataTable GetHemoCoutScale(DateTime _begionTime, DateTime _endTime)
        {
            return HemodialysisBll.GetHemoCoutScale(_begionTime, _endTime);
        }

        public int SavePatientProgressNoteInfo(HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable ProgressNoteDataTable)
        {
            return HemodialysisBll.SavePatientProgressNoteInfo(ProgressNoteDataTable);
        }

        public DataTable GetPatientProgressNoteById(string id)
        {
            return HemodialysisBll.GetPatientProgressNoteById(id);
        }

        public HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable GetPatientProgressNoteByHemoId(string hemoId)
        {
            return HemodialysisBll.GetPatientProgressNoteByHemoId(hemoId);
        }

        public HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable GetPatientProgressNoteByHemoIdAndDate(string hemoId, DateTime beginDate, DateTime endDate)
        {
            return HemodialysisBll.GetPatientProgressNoteByHemoIdAndDate(hemoId, beginDate, endDate);
        }

        public int DeletePatientProgressNoteById(string id)
        {
            return HemodialysisBll.DeletePatientProgressNoteById(id);
        }

        public HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable GetPatientProgressNoteByIDAndCreateDate(string hemoID, DateTime createDate)
        {
            return HemodialysisBll.GetPatientProgressNoteByIDAndCreateDate(hemoID, createDate);
        }

        public HemodialysisModel.MED_HEMO_PARAMETERS_COLLECTIONDataTable GetHemoParametersCollectionByMonitorAndDate(string MonitorLable, DateTime pCreateDate)
        {
            return HemodialysisBll.GetHemoParametersCollectionByMonitorAndDate(MonitorLable, pCreateDate);
        }

        public HemodialysisModel.MED_HEMO_PARAMETERS_COLLECTIONDataTable GetHemoParametersCollectionByMonitorAndDoubleDate(string MonitorLable, DateTime pCreateDate, DateTime beginTime, DateTime endTime)
        {
            return HemodialysisBll.GetHemoParametersCollectionByMonitorAndDoubleDate(MonitorLable, pCreateDate, beginTime, endTime);
        }

        public int SaveEstimateInBasketInfo(HemodialysisModel.MED_ESTIMATE_IN_BASKETDataTable EsitmateInBasketTable)
        {
            return HemodialysisBll.SaveEstimateInBasketInfo(EsitmateInBasketTable);
        }

        public HemodialysisModel.MED_ESTIMATE_IN_BASKETDataTable GetEsimateInBasketByID(string pID)
        {
            return HemodialysisBll.GetEsimateInBasketByID(pID);
        }

        public DataTable GetEstimateInBasketByParams(string pHemoID, string pName, DateTime beginDate, DateTime endDate)
        {
            return HemodialysisBll.GetEstimateInBasketByParams(pHemoID, pName, beginDate, endDate);
        }

        public int DeleteEstimateInBasketById(string id)
        {
            return HemodialysisBll.DeleteEstimateInBasketById(id);
        }
        #endregion

        /// <summary>
        /// 根据病人姓名&日期获取长期留置静脉导管评估数据列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable GetEstimateLongVenousByNameAndDate(string name, DateTime beginDate, DateTime endDate)
        {
            return HemodialysisBll.GetEstimateLongVenousByNameAndDate(name, beginDate, endDate);
        }

        /// <summary>
        /// 根据病人姓名&日期获取临时留置静脉导管评估数据列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable GetEstimateVenousCatheterByNameAndDate(string name, DateTime beginDate, DateTime endDate)
        {
            return HemodialysisBll.GetEstimateVenousCatheterByNameAndDate(name, beginDate, endDate);
        }

        /// <summary>
        /// 根据病人透析编号&日期获取长期留置静脉导管评估数据列表
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable GetEstimateLongVenousByHemoIdAndDate(string hemoId, DateTime beginDate, DateTime endDate)
        {
            return HemodialysisBll.GetEstimateLongVenousByHemoIdAndDate(hemoId, beginDate, endDate);
        }

        /// <summary>
        /// 根据病人透析编号&日期获取临时留置静脉导管评估数据列表
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable GetEstimateVenousCatheterByHemoIdAndDate(string hemoId, DateTime beginDate, DateTime endDate)
        {
            return HemodialysisBll.GetEstimateVenousCatheterByHemoIdAndDate(hemoId, beginDate, endDate);
        }

        /// <summary>
        /// 获取长期留置静脉导管评估数据列表
        /// </summary>
        /// <returns></returns>
        public HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable GetEstimateLongVenousList()
        {
            return HemodialysisBll.GetEstimateLongVenousList();
        }

        /// <summary>
        /// 获取临时留置静脉导管评估数据列表
        /// </summary>
        /// <returns></returns>
        public HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable GetEstimateVenousCatheterList()
        {
            return HemodialysisBll.GetEstimateVenousCatheterList();
        }

        /// <summary>
        /// 根据ID获取长期留置静脉导管评估数据列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable GetEstimateLongVenousById(string id)
        {
            return HemodialysisBll.GetEstimateLongVenousById(id);
        }

        /// <summary>
        /// 根据ID获取临时留置静脉导管评估数据列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable GetEstimateVenousCatheterById(string id)
        {
            return HemodialysisBll.GetEstimateVenousCatheterById(id);
        }

        /// <summary>
        /// 根据病人透析编号&单个日期获取长期留置静脉导管评估数据列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="createDate"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable GetEstimateLongVenousByHemoIdAndSingleDate(string hemoId, DateTime createDate)
        {
            return HemodialysisBll.GetEstimateLongVenousByHemoIdAndSingleDate(hemoId, createDate);
        }

        /// <summary>
        /// 根据病人透析编号&单个日期获取临时留置静脉导管评估数据列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="createDate"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable GetEstimateVenousCatheterByHemoIdAndSingleDate(string hemoId, DateTime createDate)
        {
            return HemodialysisBll.GetEstimateVenousCatheterByHemoIdAndSingleDate(hemoId, createDate);
        }

        /// <summary>
        /// 根据ID删除长期留置静脉导管评估数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteEstimateLongVenousById(string id)
        {
            return HemodialysisBll.DeleteEstimateLongVenousById(id);
        }

        /// <summary>
        /// 根据ID删除临时留置静脉导管评估数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteEstimateVenousCatheterById(string id)
        {
            return HemodialysisBll.DeleteEstimateVenousCatheterById(id);
        }

        /// <summary>
        /// 保存静脉导管评估数据
        /// </summary>
        /// <param name="dtEstimateVenous"></param>
        /// <returns></returns>
        public int SaveEstimateVenous(DataTable dtEstimateVenous)
        {
            return HemodialysisBll.SaveEstimateVenous(dtEstimateVenous);
        }

        /// <summary>
        /// 获取数据采集设置列表
        /// </summary>
        /// <returns></returns>
        public HemodialysisModel.MED_HEMO_PARAMETERS_SETTINGDataTable GetDataGatherSetList()
        {
            return HemodialysisBll.GetDataGatherSetList();
        }

        /// <summary>
        /// 保存数据采集设置
        /// </summary>
        /// <param name="dtSetting"></param>
        /// <returns></returns>
        public int SaveDataGatherSet(HemodialysisModel.MED_HEMO_PARAMETERS_SETTINGDataTable dtSetting)
        {
            return HemodialysisBll.SaveDataGatherSet(dtSetting);
        }

        #region 工作量统计
        public int SaveWorkload(HemoModel.MED_WORKLOADDataTable workloadTable)
        {
            return HemodialysisBll.SaveWorkload(workloadTable);
        }

        public HemoModel.MED_WORKLOADDataTable GetWorkloadByDate(DateTime beginDate, DateTime endDate)
        {
            return HemodialysisBll.GetWorkloadByDate(beginDate, endDate);
        }

        public int SaveComplication(HemoModel.MED_COMPLICATION_OTHERDataTable complicationTable)
        {
            return HemodialysisBll.SaveComplication(complicationTable);
        }

        public HemoModel.MED_COMPLICATION_OTHERDataTable GetComplicationByDate(DateTime date)
        {
            return HemodialysisBll.GetComplicationByDate(date);
        }

        public HemoModel.MED_COMPLICATION_OTHERDataTable GetComplicationByDialysisAndCure(string DialysisId, string cureId)
        {
            return HemodialysisBll.GetComplicationByDialysisAndCure(DialysisId, cureId);
        }

        public HemoModel.MED_COMPLICATION_OTHERDataTable GetComplicationByParams(DateTime firstdate, DateTime lastdata)
        {
            return HemodialysisBll.GetComplicationByParams(firstdate, lastdata);
        }
        public DataTable GetSubjectiveComfortData(string dateMonth)
        {
            return HemodialysisBll.GetSubjectiveComfortData(dateMonth);
        }


        public int SaveBorrowData(HemodialysisModel.MED_BORROW_MEDICINE_DETAILDataTable borrowData)
        {
            return HemodialysisBll.SaveBorrowData(borrowData);

        }


        public int SaveBorrowDataBack(string BackID, DateTime dateTime, string backUserInfo, string userID)
        {
            return HemodialysisBll.SaveBorrowDataBack(BackID, dateTime, backUserInfo, userID);

        }


        public HemodialysisModel.MED_HEALTH_EDUCATIONDataTable GetHealthEducationByDateTime(DateTime startTime, DateTime endTime)
        {
            return HemodialysisBll.GetHealthEducationByDateTime(startTime, endTime);

        }


        public HemoModel.MED_PATIENTS_ASSESSMENTDataTable GetAssessmentByParams(string pHemoID, DateTime beginDate, DateTime endDate)
        {
            return HemodialysisBll.GetAssessmentByParams(pHemoID, beginDate, endDate);
        }


        public int DeleteAssessmentById(string id, string pUser)
        {
            return HemodialysisBll.DeleteAssessmentById(id, pUser);

        }
        #endregion

        /// <summary>
        /// 获取患者的检验信息
        /// </summary>
        /// <param name="hemoId">透析号</param>
        /// <param name="whereName">查询条件比如传入白蛋白，铁蛋白，PTH水平等</param>
        /// <param name="whereFilter">检验查询条件</param>
        /// <param name="dtStar">开始时间</param>
        /// <param name="dtEnd">结束 时间</param>
        /// <returns></returns>
        public DataTable GetHoldLabItemDt(string hemoId, string whereName, string whereFilter, DateTime dtStar, DateTime dtEnd, string condition)
        {
            return HemodialysisBll.GetHoldLabItemDt(hemoId, whereName, whereFilter, dtStar, dtEnd, condition);
        }


        /// <summary>
        /// SaveAssessmentByDate
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int SaveAssessmentByDate(HemoModel.MED_ASSESSMENTMASTERDataTable data)
        {
            //对于Master表进行拆分
            var patientAssess = new HemoModel.MED_PATIENTS_ASSESSMENTDataTable();
            var patientAssessAttr = new HemoModel.MED_PATIENTS_ASSESSMENT_ATTRDataTable();
            var patientAssessRow = patientAssess.NewMED_PATIENTS_ASSESSMENTRow();
            patientAssessRow.ASSESSMENT_DATE = data[0].ASSESSMENT_DATE;
            patientAssessRow.ASSESSMENT_ID = data[0].ASSESSMENT_ID;
            patientAssessRow.ASSESSMENT_TYPE = data[0].ASSESSMENT_TYPE;
            patientAssessRow.ASSESSMENT_NOTE = data[0].ASSESSMENT_NOTE;
            patientAssessRow.HEMODIALYSIS_ID = data[0].HEMODIALYSIS_ID;
            patientAssessRow.CREATE_USER = data[0].CREATE_USER;
            patientAssessRow.STATUS = "1";
            patientAssess.AddMED_PATIENTS_ASSESSMENTRow(patientAssessRow);

            foreach (DataColumn column in data.Columns)
            {
                if (string.IsNullOrEmpty(data[0][string.Format("{0}", column.ColumnName)].ToString()))
                    continue;
                if (column.ColumnName.Contains("control") || column.ColumnName.Contains("cmbFIRST_PURIFIER_MODEL"))
                {
                    var patientAssessAttrRow = patientAssessAttr.NewMED_PATIENTS_ASSESSMENT_ATTRRow();
                    patientAssessAttrRow.ASSESSMENT_ATTR_ID = Guid.NewGuid().ToString();
                    patientAssessAttrRow.ASSESSMENT_ID = data[0].ASSESSMENT_ID;
                    patientAssessAttrRow.ATTR_ID = column.ColumnName;
                    patientAssessAttrRow.ATTR_VALUE = data[0][string.Format("{0}", column.ColumnName)].ToString();
                    patientAssessAttrRow.CREATE_DATE = data[0].ASSESSMENT_DATE;
                    patientAssessAttrRow.CREATE_USER = data[0].CREATE_USER;
                    patientAssessAttrRow.EDIT_DATE = data[0].ASSESSMENT_DATE;
                    patientAssessAttrRow.EDIT_USER = data[0].CREATE_USER;
                    patientAssessAttr.AddMED_PATIENTS_ASSESSMENT_ATTRRow(patientAssessAttrRow);
                }
            }

            return HemodialysisBll.SaveAssessmentByDate(patientAssess, patientAssessAttr);

        }


        #region  服务类

        public HemoModel.MED_PATIENTS_ASSESSMENT_ATTRDataTable GetAssessmentByAssID(string AssessmentID)
        {
            return HemodialysisBll.GetAssessmentByAssID(AssessmentID);
        }

        public DrugModel.MED_PATIENT_FOLLOWUPDataTable GetFollowUpByDateTime(DateTime startTime, DateTime endTime)
        {
            return HemodialysisBll.GetFollowUpByDateTime(startTime, endTime);
        }


        public DataSet GetAllLabUnCurePatients()
        {
            return HemodialysisBll.GetAllLabUnCurePatients();
        }



        public DataSet GetALLOfficeData(DateTime _statOfficeData, DateTime _endOfficeData)
        {
            return HemodialysisBll.GetALLOfficeData(_statOfficeData, _endOfficeData);
        }

        public HemodialysisModel.MED_HEMO_RECIPEDataTable GetAssParamByHemoID(string hemoId)
        {
            return HemodialysisBll.GetAssParamByHemoID(hemoId);
        }


        public int GetNotAssentDayByHemoId(string hemoId)
        {
            return HemodialysisBll.GetNotAssentDayByHemoId(hemoId);
        }

        public HemoModel.MED_HEMO_WORKOVERTIMEDataTable GetNurseWorkOverTimeRecordByDate(DateTime _statDate, DateTime _endDate)
        {
            return HemodialysisBll.GetNurseWorkOverTimeRecordByDate(_statDate, _endDate);
        }

        public int SaveNurseWorkOverTime(HemoModel.MED_HEMO_WORKOVERTIMEDataTable _data)
        {
            return HemodialysisBll.SaveNurseWorkOverTime(_data);
        }


        public int DeleteNurseWorkOverTimeByID(string _workNurseId)
        {
            return HemodialysisBll.DeleteNurseWorkOverTimeByID(_workNurseId);
        }


        public int CreatePatientRecipeBydate(DateTime recipeDate)
        {
            return HemodialysisBll.CreatePatientRecipeBydate(recipeDate);
        }


        public int DeleteUnExcuteRecipeByHemoID(string hemoId, DateTime dt)
        {
            return HemodialysisBll.DeleteUnExcuteRecipeByHemoID(hemoId, dt);

        }


        public HemoModel.MED_WORKLOADDataTable GetWorkLoadCountByDate(DateTime dateBegin, DateTime dateEnd)
        {
            return HemodialysisBll.GetWorkLoadCountByDate(dateBegin, dateEnd);
        }


        public HemoModel.MED_WORKLOADDataTable GetWorkloadByParmas(string areaId, string banchiName, DateTime date)
        {
            return HemodialysisBll.GetWorkloadByParmas(areaId, banchiName, date);
        }

        public PatientModel.MED_BASE_RECORDDataTable GetBaseRecordByHemoId(string hemoId)
        {
            return HemodialysisBll.GetBaseRecordByHemoId(hemoId);
        }

        public int SaveBaseRecord(PatientModel.MED_BASE_RECORDDataTable dtRecord)
        {
            return HemodialysisBll.SaveBaseRecord(dtRecord);
        }

        public HemodialysisModel.MED_BASE_RECORD_EVENTDataTable GetRecordEventByHemoId(string hemoId)
        {
            return HemodialysisBll.GetRecordEventByHemoId(hemoId);
        }

        public int SaveRecordEvent(HemodialysisModel.MED_BASE_RECORD_EVENTDataTable dtRecord)
        {
            return HemodialysisBll.SaveRecordEvent(dtRecord);
        }

        public int DeleteRecordEventById(string id)
        {
            return HemodialysisBll.DeleteRecordEventById(id);
        }

        public HemodialysisModel.MED_BASE_RECORD_DIAGNOSEDataTable GetRecordDiagnoseByHemoId(string hemoId)
        {
            return HemodialysisBll.GetRecordDiagnoseByHemoId(hemoId);
        }

        public int SaveRecordDiagnose(HemodialysisModel.MED_BASE_RECORD_DIAGNOSEDataTable dtRecord)
        {
            return HemodialysisBll.SaveRecordDiagnose(dtRecord);
        }

        public int DeleteRecordDiagnoseById(string id)
        {
            return HemodialysisBll.DeleteRecordDiagnoseById(id);
        }
        #endregion

        /// <summary>
        /// 根据透析编号获取患者URR统计数据
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public HemoModel.MED_PATIENTS_URRDataTable GetPatientURRByHemoId(string hemoId)
        {
            return HemodialysisBll.GetPatientURRByHemoId(hemoId);
        }

        /// <summary>
        /// 保存患者URR统计数据
        /// </summary>
        /// <param name="dtPatientURR"></param>
        /// <returns></returns>
        public int SavePatientURR(HemoModel.MED_PATIENTS_URRDataTable dtPatientURR)
        {
            return HemodialysisBll.SavePatientURR(dtPatientURR);
        }

        /// <summary>
        /// 根据透析编号获取充分性评估数据
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable GetEstimateSufficiencyByHemoId(string hemoId, string flag)
        {
            return HemodialysisBll.GetEstimateSufficiencyByHemoId(hemoId, flag);
        }

        /// <summary>
        /// 根据Flag获取充分性评估数据
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable GetEstimateSufficiencyByFlag(string[] flag)
        {
            return HemodialysisBll.GetEstimateSufficiencyByFlag(flag);
        }

        /// <summary>
        /// 根据透析编号、日期获取充分性评估数据
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="flag"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable GetEstimateSufficiencyByHemoIdAndDate(string hemoId, string flag, DateTime beginDate, DateTime endDate)
        {
            return HemodialysisBll.GetEstimateSufficiencyByHemoIdAndDate(hemoId, flag, beginDate, endDate);
        }

        /// <summary>
        /// 根据Flag、日期获取充分性评估数据
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable GetEstimateSufficiencyByFlagAndDate(string[] flag, DateTime beginDate, DateTime endDate)
        {
            return HemodialysisBll.GetEstimateSufficiencyByFlagAndDate(flag, beginDate, endDate);
        }

        /// <summary>
        /// 根据透析编号、日期获取充分性评估数据
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="flag"></param>
        /// <param name="createDate"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable GetEstimateSufficiencyByHemoIdAndDate(string hemoId, string flag, DateTime createDate)
        {
            return HemodialysisBll.GetEstimateSufficiencyByHemoIdAndDate(hemoId, flag, createDate);
        }

        /// <summary>
        /// 保存充分性评估数据
        /// </summary>
        /// <param name="dtSufficiency"></param>
        /// <returns></returns>
        public int SaveEstimateSufficiency(HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable dtSufficiency)
        {
            return HemodialysisBll.SaveEstimateSufficiency(dtSufficiency);
        }

        /// <summary>
        /// 根据ID删除充分性评估记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteEstimateSufficiencyById(string id)
        {
            return HemodialysisBll.DeleteEstimateSufficiencyById(id);
        }

        /// <summary>
        /// 获取一段时间内的透后体重
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="dtStar">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns>数据集</returns>
        public DataTable GetPastWeightListByParams(string pHemoID, DateTime dtStar, DateTime dtEnd)
        {
            return HemodialysisBll.GetPastWeightListByParams(pHemoID, dtStar, dtEnd);
        }

        /// <summary>
        /// 获取一段时间内的透中血压
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="dtStar">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns>数据集</returns>
        public DataTable GetPastBloodPresureListByParams(string pHemoID, DateTime dtStar, DateTime dtEnd)
        {
            return HemodialysisBll.GetPastBloodPresureListByParams(pHemoID, dtStar, dtEnd);

        }

        public int UpdatePatientRecipePurificationModeBydate(DateTime recipeDate)
        {
            return HemodialysisBll.UpdatePatientRecipePurificationModeBydate(recipeDate);
        }

        /// <summary>
        /// 保存患者同意书
        /// </summary>
        /// <param name="dtBookPicture"></param>
        /// <returns></returns>
        public int SaveBookPicture(HemoModel.MED_BOOK_PICTUREDataTable dtBookPicture)
        {
            return HemodialysisBll.SaveBookPicture(dtBookPicture);
        }

        /// <summary>
        /// 根据透析编号和同意书名字获取同意书签名
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="bookName"></param>
        /// <returns></returns>
        public HemoModel.MED_BOOK_PICTUREDataTable GetBookPictureByHemoAndBookName(string hemoId, string bookName)
        {
            return HemodialysisBll.GetBookPictureByHemoAndBookName(hemoId, bookName);
        }

        /// <summary>
        /// 保存透析单签字
        /// </summary>
        /// <param name="dtCureSign"></param>
        /// <returns></returns>
        public int SaveCureSign(HemoModel.MED_CURE_SIGNDataTable dtCureSign)
        {
            return HemodialysisBll.SaveCureSign(dtCureSign);
        }

        /// <summary>
        /// 根据透析编号和治疗单ID获取透析单签字
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="cureId"></param>
        /// <returns></returns>
        public HemoModel.MED_CURE_SIGNDataTable GetCureSignByHemoIdAndCureId(string hemoId, string cureId)
        {
            return HemodialysisBll.GetCureSignByHemoIdAndCureId(hemoId, cureId);
        }

        public HemodialysisModel.MED_CURE_DRUGDataTable GetCureDrugByHemoIDAndRecipeId(string pHemoID, string pRecipeId)
        {
            return HemodialysisBll.GetCureDrugByHemoIDAndRecipeId(pHemoID, pRecipeId);
        }

        /// <summary>
        /// 根据日期获取最近一次透析为基准，上周透析过、三个月内连续透析过患者透析编号
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable GetHemoIdInLastWeekAndThreeMonthsByDate(DateTime beginTime, DateTime endTime)
        {
            return HemodialysisBll.GetHemoIdInLastWeekAndThreeMonthsByDate(beginTime, endTime);
        }

        /// <summary>
        /// 根据日期获取患者透析编号
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable GetHemoIdByDate(DateTime beginTime, DateTime endTime)
        {
            return HemodialysisBll.GetHemoIdByDate(beginTime, endTime);
        }

        public DataTable GetPatientBaseRecordProtopathyByDate(DateTime beginTime, DateTime endTime)
        {
            return HemodialysisBll.GetPatientBaseRecordProtopathyByDate(beginTime, endTime);

        }
        /// <summary>
        /// 根据透析编号获取是否维持性透析患者
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public string GetCureTypeByHemoId(string hemoId)
        {
            return HemodialysisBll.GetCureTypeByHemoId(hemoId);
        }

        /// <summary>
        /// 按月份根据透析编号和日期获取患者透析次数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable GetCureCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime)
        {
            return HemodialysisBll.GetCureCountByHemoIdAndDate(hemoId, beginTime, endTime);
        }

        /// <summary>
        /// GetComplicationOther
        /// </summary>
        /// <param name="cureId"></param>
        /// <returns></returns>
        public DataTable GetComplicationOther(string cureId)
        {
            return HemodialysisBll.GetComplicationOther(cureId);

        }

        /// <summary>
        /// GetPatientHemoAge
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pAGE"></param>
        /// <param name="pHEMOAGE"></param>
        /// <returns></returns>
        public DataTable GetPatientHemoAge(string pName, decimal pAGE, decimal pHEMOAGE)
        {
            return HemodialysisBll.GetPatientHemoAge(pName, pAGE, pHEMOAGE);
        }

        /// <summary>
        /// QueryPatientMoreInfoList
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="beginAge"></param>
        /// <param name="endAge"></param>
        /// <param name="beginHemoAge"></param>
        /// <param name="endHemoAge"></param>
        /// <param name="pSex"></param>
        /// <returns></returns>
        public DataTable QueryPatientMoreInfoList(DateTime beginDate, DateTime endDate, string beginAge, string endAge, string beginHemoAge, string endHemoAge, string pSex)
        {
            return HemodialysisBll.QueryPatientMoreInfoList(beginDate, endDate, beginAge, endAge, beginHemoAge, endHemoAge, pSex);
        }

        /// <summary>
        /// 按月份根据透析编号和日期获取患者血管通路例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable GetAccessCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime)
        {
            return HemodialysisBll.GetAccessCountByHemoIdAndDate(hemoId, beginTime, endTime);
        }

        /// <summary>
        /// 根据透析编号和日期获取患者导管手术例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable GetDuctOperationCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime)
        {
            return HemodialysisBll.GetDuctOperationCountByHemoIdAndDate(hemoId, beginTime, endTime);
        }

        /// <summary>
        /// 按月份根据透析编号和日期获取透析患者男女例次
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable GetSexCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime)
        {
            return HemodialysisBll.GetSexCountByHemoIdAndDate(hemoId, beginTime, endTime);
        }

        /// <summary>
        /// 根据透析编号和日期获取透析患者男女例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable GetSexCountByHemoIdAndDate2(string hemoId, DateTime beginTime, DateTime endTime)
        {
            return HemodialysisBll.GetSexCountByHemoIdAndDate2(hemoId, beginTime, endTime);
        }

        /// <summary>
        /// 根据日期获取透析患者男女例次
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable GetSexCountByDate(DateTime beginTime, DateTime endTime)
        {
            return HemodialysisBll.GetSexCountByDate(beginTime, endTime);
        }

        /// <summary>
        /// 根据日期获取透析患者男女例数
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable GetSexCountByDate2(DateTime beginTime, DateTime endTime)
        {
            return HemodialysisBll.GetSexCountByDate2(beginTime, endTime);
        }

        /// <summary>
        /// 根据透析编号和日期获取透析患者不同年龄段例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable GetAgeCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime)
        {
            return HemodialysisBll.GetAgeCountByHemoIdAndDate(hemoId, beginTime, endTime);
        }

        /// <summary>
        /// 根据透析编号和日期获取透析患者传染病例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable GetInfectousCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime)
        {
            return HemodialysisBll.GetInfectousCountByHemoIdAndDate(hemoId, beginTime, endTime);
        }

        /// <summary>
        /// 根据透析编号和日期获取规律透析患者例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable GetRegularCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime)
        {
            return HemodialysisBll.GetRegularCountByHemoIdAndDate(hemoId, beginTime, endTime);
        }

        /// <summary>
        /// 获取每周2次或者3次的患者
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable GetWeekTwoOrThirdCountPatientsHemoId()
        {
            return HemodialysisBll.GetWeekTwoOrThirdCountPatientsHemoId();
        }

        /// <summary>
        /// DeleteHealthEducationByHemoIdAndId
        /// </summary>
        /// <param name="pHemoID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteHealthEducationByHemoIdAndId(string pHemoID, string id)
        {
            return HemodialysisBll.DeleteHealthEducationByHemoIdAndId(pHemoID, id);
        }

        /// <summary>
        /// DeleteMED_ANEMIA_CKDMBD_ASSESSbyID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int DeleteMED_ANEMIA_CKDMBD_ASSESSbyID(string ID)
        {
            return HemodialysisBll.DeleteMED_ANEMIA_CKDMBD_ASSESSbyID(ID);
        }

        /// <summary>
        /// SaveMED_ANEMIA_CKDMBD_ASSESS
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int SaveMED_ANEMIA_CKDMBD_ASSESS(HemodialysisModel.MED_ANEMIA_CKDMBD_ASSESSDataTable dt)
        {

            return HemodialysisBll.SaveMED_ANEMIA_CKDMBD_ASSESS(dt);
        }

        /// <summary>
        /// GetMED_ANEMIA_CKDMBD_ASSESSbyDate
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="ASSESS_TYPE"></param>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_ANEMIA_CKDMBD_ASSESSDataTable GetMED_ANEMIA_CKDMBD_ASSESSbyDate(DateTime beginTime, DateTime endTime, string ASSESS_TYPE, string hemoId)
        {
            return HemodialysisBll.GetMED_ANEMIA_CKDMBD_ASSESSbyDate(beginTime, endTime, ASSESS_TYPE, hemoId);

        }

        /// <summary>
        /// GetMED_ANEMIA_CKDMBD_ASSESS
        /// </summary>
        /// <param name="ASSESS_TYPE"></param>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_ANEMIA_CKDMBD_ASSESSDataTable GetMED_ANEMIA_CKDMBD_ASSESS(string ASSESS_TYPE, string hemoId)
        {
            return HemodialysisBll.GetMED_ANEMIA_CKDMBD_ASSESS(ASSESS_TYPE, hemoId);

        }

        /// <summary>
        /// GetDeathRate
        /// </summary>
        /// <param name="pBeginTime"></param>
        /// <param name="pEndTime"></param>
        /// <param name="HemoIDList"></param>
        /// <returns></returns>
        public DataTable GetDeathRate(DateTime pBeginTime, DateTime pEndTime, string HemoIDList)
        {
            return HemodialysisBll.GetDeathRate(pBeginTime, pEndTime, HemoIDList);
        }

        /// <summary>
        /// GetTempRecipeCount
        /// </summary>
        /// <param name="pHemoID"></param>
        /// <returns></returns>
        public int GetTempRecipeCount(string pHemoID)
        {
            return HemodialysisBll.GetTempRecipeCount(pHemoID);
        }

        /// <summary>
        /// 根据日期获取护士绩效考核记录列表
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable GetPerformanceAppraisalByDate(DateTime beginTime, DateTime endTime)
        {
            return HemodialysisBll.GetPerformanceAppraisalByDate(beginTime, endTime);
        }

        /// <summary>
        /// 根据日期、护士组长获取护士绩效考核记录列表
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="nurseLeader"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable GetPerformanceAppraisalByDateAndNurseLeader(DateTime beginTime, DateTime endTime, string nurseLeader)
        {
            return HemodialysisBll.GetPerformanceAppraisalByDateAndNurseLeader(beginTime, endTime, nurseLeader);
        }

        /// <summary>
        /// 根据日期、组长标识获取护士组长或组员绩效考核记录列表
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="isLeader"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable GetPerformanceAppraisalByDateAndLeaderFlag(DateTime beginTime, DateTime endTime, string isLeader)
        {
            return HemodialysisBll.GetPerformanceAppraisalByDateAndLeaderFlag(beginTime, endTime, isLeader);
        }

        /// <summary>
        /// 根据日期、护士获取护士绩效考核记录列表
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="nurse"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable GetPerformanceAppraisalByDateAndNurse(DateTime beginTime, DateTime endTime, string nurse)
        {
            return HemodialysisBll.GetPerformanceAppraisalByDateAndNurse(beginTime, endTime, nurse);
        }

        /// <summary>
        /// 根据类型获取护士绩效考核规则记录列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable GetPerformanceAppraisalRuleByType(string type)
        {
            return HemodialysisBll.GetPerformanceAppraisalRuleByType(type);
        }

        /// <summary>
        /// 根据得分类型获取护士绩效考核规则记录列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable GetPerformanceAppraisalRuleByScoreType(string type)
        {
            return HemodialysisBll.GetPerformanceAppraisalRuleByScoreType(type);
        }

        /// <summary>
        /// 根据ID获取护士绩效考核规则记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable GetPerformanceAppraisalRuleById(string id)
        {
            return HemodialysisBll.GetPerformanceAppraisalRuleById(id);
        }

        /// <summary>
        /// 保存护士绩效考核规则记录
        /// </summary>
        /// <param name="dtRule"></param>
        /// <returns></returns>
        public int SavePerformanceAppraisalRule(HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable dtRule)
        {
            return HemodialysisBll.SavePerformanceAppraisalRule(dtRule);
        }

        /// <summary>
        /// 保存护士绩效考核记录
        /// </summary>
        /// <param name="dtAppraisal"></param>
        /// <returns></returns>
        public int SavePerformanceAppraisal(HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable dtAppraisal)
        {
            return HemodialysisBll.SavePerformanceAppraisal(dtAppraisal);
        }

        /// <summary>
        /// 根据时间、透析次数获取透析患者
        /// </summary>
        /// <param name="time"></param>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public DataTable GetCurePatientByTimeAndFrequency(int time, int frequency)
        {
            return HemodialysisBll.GetCurePatientByTimeAndFrequency(time, frequency);
        }

        /// <summary>
        /// 获取患者是否是最新三个月开始治疗的，如果是就算新入患者返回1，否则返回0。
        /// </summary>
        /// <param name="pHemoID">患者透析ID</param>
        /// <returns></returns>
        public DataTable GetPatientTypeIsNew(string pHemoID)
        {
            return HemodialysisBll.GetPatientTypeIsNew(pHemoID);
        }

        /// <summary>
        /// 根据项名称和归属年份获取上传数据日志
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_UPLOAD_LOGDataTable GetUploadLogByItemNameAndYear(string itemName, string year)
        {
            return HemodialysisBll.GetUploadLogByItemNameAndYear(itemName, year);
        }

        /// <summary>
        /// 保存上传数据日志
        /// </summary>
        /// <param name="dtUploadLog"></param>
        /// <returns></returns>
        public int SaveUploadLog(HemodialysisModel.MED_UPLOAD_LOGDataTable dtUploadLog)
        {
            return HemodialysisBll.SaveUploadLog(dtUploadLog);
        }

        /// <summary>
        /// 根据日期获取工作量
        /// </summary>
        /// <param name="dialisysDate"></param>
        /// <returns></returns>
        public HemoModel.MED_USER_WORKLOAD_EXTENDERDataTable GetParamUserWorkExtend(DateTime dialisysDate)
        {
            return HemodialysisBll.GetParamUserWorkExtend(dialisysDate);

        }

        /// <summary>
        /// 获取工作量
        /// </summary>
        /// <param name="dtStar"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public HemoModel.MED_USER_WORKLOAD_EXTENDERDataTable GetParamALLUserWorkExtend(DateTime dtStar, DateTime dtEnd)
        {
            return HemodialysisBll.GetParamALLUserWorkExtend(dtStar, dtEnd);

        }

        /// <summary>
        /// 根据日期获取工作量
        /// </summary>
        /// <param name="dialisysDate"></param>
        /// <returns></returns>
        public HemoModel.MED_USER_WORKLOAD_EXTENDERDataTable GetUserWorkExtendFromCureMain(DateTime dialisysDate)
        {
            return HemodialysisBll.GetUserWorkExtendFromCureMain(dialisysDate);

        }

        /// <summary>
        /// 根据时间期获取工作量
        /// </summary>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public HemoModel.MED_WORKLOADDataTable GetWorkLoadNurseCountByDate(DateTime dateBegin, DateTime dateEnd)
        {
            return HemodialysisBll.GetWorkLoadNurseCountByDate(dateBegin, dateEnd);
        }

        /// <summary>
        /// 福总获取工作量统计
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public HemoModel.MED_WORKLOADDataTable GetWorkloadByDateFZ(DateTime beginDate, DateTime endDate)
        {
            return HemodialysisBll.GetWorkloadByDateFZ(beginDate, endDate);
        }

        /// <summary>
        /// 上海普陀区人民医院获取术前护理评估
        /// </summary>
        /// <param name="name"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_PREOPERATIVE_NURSINGDataTable GetPreoperativeNursingByNameAndDate(string name, DateTime beginDate, DateTime endDate)
        {
            return HemodialysisBll.GetPreoperativeNursingByNameAndDate(name, beginDate, endDate);
        }

        /// <summary>
        /// 上海普陀区人民医院获取术前护理评估
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_PREOPERATIVE_NURSINGDataTable GetPreoperativeNursingByHemoIdAndDate(string hemoId, DateTime beginDate, DateTime endDate)
        {
            return HemodialysisBll.GetPreoperativeNursingByHemoIdAndDate(hemoId, beginDate, endDate);
        }

        /// <summary>
        /// 上海普陀区人民医院获取患者转运交接
        /// </summary>
        /// <param name="name"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_PAT_TRANSFER_HANDOVERDataTable GetPatTransferHandoverByNameAndDate(string name, DateTime beginDate, DateTime endDate)
        {
            return HemodialysisBll.GetPatTransferHandoverByNameAndDate(name, beginDate, endDate);
        }

        /// <summary>
        /// 上海普陀区人民医院获取患者转运交接
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public HemodialysisModel.MED_PAT_TRANSFER_HANDOVERDataTable GetPatTransferHandoverByHemoIdAndDate(string hemoId, DateTime beginDate, DateTime endDate)
        {
            return HemodialysisBll.GetPatTransferHandoverByHemoIdAndDate(hemoId, beginDate, endDate);
        }

        /// <summary>
        /// 根据ID删除术前护理评估
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeletePreoperativeNursingById(string id)
        {
            return HemodialysisBll.DeletePreoperativeNursingById(id);
        }

        /// <summary>
        /// 根据ID删除患者转运交接
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeletePatTransferHandoverById(string id)
        {
            return HemodialysisBll.DeletePatTransferHandoverById(id);
        }

        /// <summary>
        /// 保存患者转运交接
        /// </summary>
        /// <param name="dtPatTransferHandover"></param>
        /// <returns></returns>
        public int SavePatTransferHandover(HemodialysisModel.MED_PAT_TRANSFER_HANDOVERDataTable dtPatTransferHandover)
        {
            return HemodialysisBll.SavePatTransferHandover(dtPatTransferHandover);
        }
        /// <summary>
        /// 获取病人指定时间到当前时间的治疗单
        /// </summary>
        /// <param name="hemoId">透析号</param>
        /// <param name="cureDt">治疗时间</param>
        /// <returns></returns>
        public HemodialysisModel.MED_CURE_MAINDataTable GetCureListByHemoId(string hemoId, DateTime cureDt)
        {
            return HemodialysisBll.GetCureListByHemoId(hemoId, cureDt);
        }

        public DataTable GetCureMainByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime)
        {
            return HemodialysisBll.GetCureMainByHemoIdAndDate(hemoId, beginTime, endTime);
        }


        public DataTable GetPatientByHemoId(string hemoId)
        {
            return HemodialysisBll.GetPatientByHemoId(hemoId);
        }


        /// <summary>
        /// 根据透析编号和日期获取患者通路治疗类型例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable GetCureVascularTypeCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime)
        {
            return HemodialysisBll.GetCureVascularTypeCountByHemoIdAndDate(hemoId, beginTime, endTime);
        }

        /// <summary>
        /// 获取传染病已肝丙肝的例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="infectName"></param>
        /// <returns></returns>
        public DataTable GetInfectiousCountByParams(string hemoId, DateTime beginTime, DateTime endTime, string infectName, string infectCode)
        {
            return HemodialysisBll.GetInfectiousCountByParams(hemoId, beginTime, endTime, infectName, infectCode);
        }

        public DataTable GetLabValueLineTypeByparams(string hemoId, string itemName, DateTime beginTime, DateTime endTime)
        {
            return HemodialysisBll.GetLabValueLineTypeByparams(hemoId, itemName, beginTime, endTime);
        }

        /// <summary>
        /// 根据透析编号和日期获取患者通路类型例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable GetVascularTypeCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime)
        {
            return HemodialysisBll.GetVascularTypeCountByHemoIdAndDate(hemoId, beginTime, endTime);
        }
    }
}
