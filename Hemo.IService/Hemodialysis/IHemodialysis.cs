/*----------------------------------------------------------------
// Copyright (C) 2005 (苏州)医疗科技发展有限公司
// 文件名：IHemodialysis.cs
// 文件功能描述：IHemodialysis接口类定义文件
// 创建标识：
// 修改时间:2014-4-30
// 修改人：吕志强
// 修改描述：添加根据病人透析号获取透析参数数据接口方法
----------------------------------------------------------------*/

using System;
using System.Data;
using System.ServiceModel;
using Hemo.Model;

namespace Hemo.IService.Config
{
    [ServiceContract]
    public interface IHemodialysis
    {
        /// <summary>
        /// 获取全部长期处方数据
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_HEMO_RECIPEDataTable GetAllRecipe();

        /// <summary>
        /// GetBeforeHemodialysisSignList
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_BEFORE_HEMODIALYSIS_SIGNDataTable GetBeforeHemodialysisSignList();

        /// <summary>
        /// SaveBeforeHemodialysisSignInfo
        /// </summary>
        /// <param name="hemodialysisDataTable"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveBeforeHemodialysisSignInfo(HemodialysisModel.MED_BEFORE_HEMODIALYSIS_SIGNDataTable hemodialysisDataTable);

        /// <summary>
        /// 得到用药记录
        /// </summary>
        /// <param name="pHemoID"></param>
        /// <param name="beginCreateTime"></param>
        /// <param name="endCreateTime"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetDrugRecord(string pCureID, DateTime beginCreateTime, DateTime endCreateTime);

        /// <summary>
        /// 保存长期处方
        /// </summary>
        /// <param name="pRecipeDataTable"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveRecipe(HemodialysisModel.MED_HEMO_RECIPEDataTable pRecipeDataTable);
        //更新临时用药状态
        [OperationContract]
        int SaveExeDrugStatus(string status, string cure_drug_id);
        //更新长期用药状态
        [OperationContract]
        int SaveExeDrugLongStatus(string status, string cure_drug_id);
        /// <summary>
        /// 据长期处方ID得到对应处方数据
        /// </summary>
        /// <param name="pRecipeID"></param>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_HEMO_RECIPEDataTable GetRecipeByRecipeID(string pRecipeID);
        /// <summary>
        /// 获取净化次数最大值
        /// </summary>
        /// <param name="pRecipeID"></param>
        /// <returns></returns>
        [OperationContract]
        int GetCleanUpTimes(string pHemoID);

        /// <summary>
        /// 根据处方ID列表更新开方医生签名
        /// </summary>
        /// <param name="pRecipeIDList">处方ID列表</param>
        /// <param name="pUserID">医生ID</param>
        /// <returns></returns>
        [OperationContract]
        int SaveRecipeUserIDByRecipeIDList(string pRecipeIDList, string pUserID);

        /// <summary>
        /// 根据长期处方ID得到对应处方数据
        /// </summary>
        /// <param name="pHemodialysisID">透析号</param>
        /// <returns>透析列表数据</returns>
        [OperationContract]
        HemodialysisModel.MED_HEMO_RECIPEDataTable GetRecipeByHemodialysisID(string pHemodialysisID);

        /// <summary>
        /// GetLongRecipeByHemodialysisID
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_HEMO_RECIPEDataTable GetLongRecipeByHemodialysisID(string hemoId);

        /// <summary>
        /// GetRecipeByHemodialysisIDAndDate
        /// </summary>
        /// <param name="pHemodialysisID"></param>
        /// <param name="pRecipeDate"></param>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_HEMO_RECIPEDataTable GetRecipeByHemodialysisIDAndDate(string pHemodialysisID, DateTime pRecipeDate);

        /// <summary>
        /// 根据日期和透析编号得到处方、透析机、治疗方式数据
        /// </summary>
        /// <param name="pHemodialysisID">透析号</param>
        /// <param name="pDate">排班日期</param>
        /// <returns>透析列表数据</returns>
        [OperationContract]
        HemodialysisModel.GetPatientRecipeInfoDataTable GetPatientRecipeInfo(string pHemodialysisID, string pDate);

        /// <summary>
        /// ExecuteProLogInfos
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int ExecuteProLogInfos();

        /// <summary>
        /// 根据病人透析号得到状态为1的处方数量
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <returns></returns>
        [OperationContract]
        int GetRecipeStatusCountByHemoID(string pHemoID);

        /// <summary>
        /// 根据病人ID得到病人数量
        /// </summary>
        /// <param name="pPatientID">病人住院ID</param>
        /// <returns></returns>
        [OperationContract]
        int GetPatientCountByPatientID(string pPatientID);


        /// <summary>
        ///  inp_np号同步获取病人信息
        /// </summary>
        /// <param name="pPatientID">病人住院号</param>
        /// <returns>得到病人住院信息</returns>
        [OperationContract]
        HemodialysisModel.MED_PAT_MASTER_INDEXDataTable GetPatientMasterIndexByInpNo(string pInpNo, string pWardCode);


        /// <summary>
        /// 根据病人住院号同步获取病人信息
        /// </summary>
        /// <param name="pPatientID">病人住院号</param>
        /// <returns>得到病人住院信息</returns>
        [OperationContract]
        HemodialysisModel.MED_PAT_MASTER_INDEXDataTable GetPatientMasterIndexByPatientID(string pPatientID, string pWardCode);

        /// <summary>
        /// 获取全部病人信息列表从HIS表中
        /// </summary>
        [OperationContract]
        HemodialysisModel.MED_PAT_MASTER_INDEXDataTable GetPatientMasterIndexList(string pWardCode);

        /// <summary>
        /// 得到新生成的处方编号
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetNewRecipeID();
        [OperationContract]
        int UpdatePatientRecipePurificationModeBydate(DateTime recipeDate);
        /// <summary>
        /// 保存治疗单
        /// </summary>
        /// <param name="pRecipeDataTable">治疗单数据</param>
        /// <returns></returns>
        [OperationContract]
        int SaveCureMain(HemodialysisModel.MED_CURE_MAINDataTable pCureDataTable);

        /// <summary>
        /// 保存CRRT治疗单
        /// </summary>
        /// <param name="dtCure"></param>
        /// <returns></returns>
        int SaveCRRTCureMain(HemodialysisModel.MED_CURE_MAIN_CRRTDataTable dtCure);

        /// <summary>
        /// 得到新生成的治疗单编号
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetNewCureID();
        /// <summary>
        /// 获取血压控制例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetBloodControlsReport(string hemoId, DateTime beginTime, DateTime endTime);
        /// <summary>
        /// 根据治疗单编号得到治疗数据信息
        /// </summary>
        /// <param name="pCureID">治疗单编号</param>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_CURE_MAINDataTable GetMainCureByCureID(string pCureID);

        [OperationContract]
        /// <summary>
        /// 根据病人透析号和治疗单编号得到治疗单数量
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="CureCreateDate">透析日期</param>
        /// <returns>返回治疗单数量</returns>
        int GetMainCureCountByCreateDate(string pHemoID, DateTime pCureCreateDate);

        /// <summary>
        /// 根据病人透析号得到治疗数据
        /// </summary>
        /// <param name="pHemoID">病人透析号</param>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_CURE_MAINDataTable GetMainCureByHemoID(string pHemoID);

        /// <summary>
        /// GetMainCureByHemoIDAndDate
        /// </summary>
        /// <param name="pHemoID"></param>
        /// <param name="pBeginDate"></param>
        /// <param name="pEndDate"></param>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_CURE_MAINDataTable GetMainCureByHemoIDAndDate(string pHemoID, DateTime pBeginDate, DateTime pEndDate);

        /// <summary>
        /// 根据病人透析号获取透析参数数据
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable GetHemoParamsByHemoID(string hemoId);

        /// <summary>
        /// 根据处方号获取此处方的治疗信息
        /// </summary>
        /// <param name="pRecipeId">处方号</param>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_CURE_MAINDataTable GetMainCureByRecipeId(string pRecipeId);

        /// <summary>
        /// 根据治疗单ID和班次获取CRRT治疗单记录
        /// </summary>
        /// <param name="cureId"></param>
        /// <param name="banci"></param>
        /// <param name="createDate"></param>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_CURE_MAIN_CRRTDataTable GetCRRTCureByCureIdAndBanci(string cureId, string banci, DateTime createDate);

        [OperationContract]
        ConfigModel.MED_COMMON_ITEMLISTDataTable GetCommonItemListByItemType(string itemType);

        /// <summary>
        /// 根据治疗单ID获取CRRT治疗单记录
        /// </summary>
        /// <param name="cureId"></param>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_CURE_MAIN_CRRTDataTable GetCRRTCureByCureId(string cureId);

        /// <summary>
        /// 根据查询条件返回对应的透析病人单列表
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="pCureCreateDate">透析日期/排班日期</param>
        /// <param name="pBanCi">班次</param>
        /// <returns>对应的透析人员列表</returns>
        [OperationContract]
        DataTable GetPrintCureList(string pHemoID, string pCureCreateDate, string pBanCi, string pName);

        /// <summary>
        /// 根据病人透析号和治疗方式分组得到治疗数据
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataTable GetMainCureGroupByHemoIDAndPurificationMode(DateTime beginCureCreateDate, DateTime endCureCreateDate);

        /// <summary>
        /// 根据治疗单编号得到对应透析参数数据列表
        /// </summary>
        /// <param name="pCureID">治疗单编号</param>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable GetHemoParametersByCureID(string pCureID);

        /// <summary>
        /// 根据治疗单编号得到对应已删除透析参数数据列表
        /// </summary>
        /// <param name="pCureID">治疗单编号</param>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable GetDeleteHemoParametersByCureID(string pCureID);

        /// <summary>
        /// 根据透析参数ID获取透析参数列表数据
        /// </summary>
        /// <param name="hemoParamId">透析参数ID</param>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable GetHemoParametersByHemoParamId(string hemoParamId);

        [OperationContract]
        HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable GetHemoParametersByHemoParamRow(string hemoParamId, int rowNumber1, int rowNumber2, string sqlByParams);


        /// <summary>
        /// 得到对应透析参数数据列表
        /// </summary>
        /// <param name="pHemoID"></param>
        /// <param name="beginCreateTime"></param>
        /// <param name="endCreateTime"></param>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable GetHemoParameters(string pHemoID, DateTime beginCreateTime, DateTime endCreateTime);

        /// <summary>
        /// 得到对应透析参数配置数据列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        HemoModel.MED_HEMODIALYSIS_PARAMS_TYPEDataTable GetHemoParametersType();

        /// <summary>
        /// 更新医嘱状态
        /// </summary>
        /// <param name="hemoID">透析号</param>
        /// <param name="comNo">组合号</param>
        /// <param name="createDate">创建时间</param>
        /// <returns></returns>
        [OperationContract]
        int UpdataCureDrugStateByParma(string StateID, string hemoID, string comNo, DateTime createDate, string cureId, string recipeId,DateTime executeDt,string nusre);

        [OperationContract]
        int UpdataCureDrugStateByParma(string StateID, string hemoID, string comNo, string comSubNo, DateTime createDate, string cureId, string recipeId, DateTime executeDt, string nusre);

        /// <summary>
        /// 保存透析参数数据方法
        /// </summary>
        /// <param name="pHemoParametersDataTable">透析参数表</param>
        /// <returns></returns>
        [OperationContract]
        int SaveCureMainSaveHemoParameters(HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable pHemoParametersDataTable);

        /// <summary>
        /// 根据透析编号获取用药记录列表数据
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_DRUG_USE_RECORDDataTable GetDrugUseRecordListByHemoId(string hemoId);

        #region 治疗单处方相关

        /// <summary>
        /// 根据治疗单编号得到对应给药数据列表
        /// </summary>
        /// <param name="pCureID">治疗单编号</param>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_CURE_DRUGDataTable GetCureDrugByCureID(string pCureID);

        [OperationContract]
        HemodialysisModel.MED_CURE_DRUGDataTable GetCureDrugByHemoID(string pHemoID, DateTime currentDT);

        [OperationContract]
        HemodialysisModel.MED_CURE_DRUGDataTable GetCureDrugForPatientRecord(string hemoId);

        [OperationContract]
        HemodialysisModel.MED_CURE_DRUGDataTable GetValidCureDrugByHemoID(string pHemoID, DateTime DT);

        [OperationContract]
        HemodialysisModel.MED_CURE_DRUGDataTable GetValidCureDrugByRoomIdAndData(string RoomId,string banchID, DateTime dtStart,DateTime dtEnd,string hemoId);

        [OperationContract]
        HemodialysisModel.MED_CURE_DRUGDataTable GetCureDrugByHemoIDAndRecipeId(string pHemoID, string pRecipeId);

        [OperationContract]
        HemodialysisModel.MED_CURE_DRUGDataTable GetValidCureDrugByHemoRecipeID(string pHemoID, string pRecipeId);

        [OperationContract]
        HemodialysisModel.MED_CURE_DRUGDataTable GetUnExcuteCureDrugByHemoID(string pHemoID, DateTime DT);

        [OperationContract]
        HemodialysisModel.MED_CURE_DRUGDataTable GetUnExcuteCureDrugByHemoRecipeId(string pHemoID, string recipeId);

        [OperationContract]
        HemodialysisModel.MED_CURE_LONGDRUGDataTable GetLongCureDrugByHemoID(string pHemoID);

        #endregion
        /// <summary>
        /// 保存给药数据列表方法
        /// </summary>
        /// <param name="pCureDrugDataTable">给药数据表</param>
        /// <returns></returns>
        [OperationContract]
        int SaveCureDrug(HemodialysisModel.MED_CURE_DRUGDataTable pCureDrugDataTable);
        /// <summary>
        /// 保存长期医嘱
        /// </summary>
        /// <param name="pCureDrugDataTable"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveCureLongDrug(HemodialysisModel.MED_CURE_LONGDRUGDataTable pCureDrugDataTable);

        /// <summary>
        /// 保存医嘱模版
        /// </summary>
        /// <param name="pCureDrugTemplateDT"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveCureDrugTemplate(HemodialysisModel.MED_CURE_DRUG_TEMPLATEDataTable pCureDrugTemplateDT);

        [OperationContract]
        HemodialysisModel.MED_CURE_DRUG_TEMPLATEDataTable GetTemplateByParmas(string docID);

        /// <summary>
        /// 保存包括治疗单主表、给药表、透析参数表的数据集
        /// </summary>
        /// <param name="pDataSet">治疗单数据集</param>
        /// <returns></returns>
        [OperationContract]
        bool SaveAllCure(DataSet pDataSet);

        /// <summary>
        /// 根据治疗单编号，返回包括治疗单主表、给药表、透析参数表的数据集
        /// </summary>
        /// <param name="pCureID">治疗单编号</param>
        /// <returns>返回治疗单数据集</returns>
        [OperationContract]
        DataSet GetAllCure(string pCureID);

        /// <summary>
        /// 得到透析列表数据
        /// </summary>
        /// <param name="pDialysisDate">透析日期</param>
        /// <param name="pBanciID">班次</param>
        /// <param name="pName">患者姓名</param>
        /// <param name="pHemoID">患者透析号</param>
        /// <returns>透析单列表</returns>
        [OperationContract]
        HemodialysisModel.GetCureListDataTable GetCureList(string pDialysisDate, string pBanciID, string pName, string pHemoID);

        [OperationContract]
        string GetCureID(string pDialysisDate, string pHemoID);


        /// <summary>
        /// 根据透析号和处方号返回处方信息与人员信息
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="pRecipeID">处方号</param>
        /// <returns>处方信息</returns>
        [OperationContract]
        DataSet GetRecipeAndPatientInfo(string pHemoID, string pRecipeID);

        /// <summary>
        /// 根据透析号和净化方式得到对应的处方数量
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="pPurificationMode">净化方式</param>
        /// <returns>对应的处方数量</returns>
        [OperationContract]
        int GetRecipeCountByPurificationMode(string pHemoID, string pPurificationMode);


        [OperationContract]
        int GetTempRecipeCountByDate(string pHemoID, DateTime pDate);

        /// <summary>
        /// 得到该处方是否已经存在治疗单中的数量
        /// </summary>
        /// <param name="pRecipeID">处方编号</param>
        /// <param name="pPurificationMode">净化方式</param>
        [OperationContract]
        int GetRecipeCountInCureList(string pRecipeID);


        /// <summary>
        /// 获取所有启用的系统消息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_COMMON_MESSAGEDataTable GetAllMessage(decimal type);

        /// <summary>
        /// 保存系统消息
        /// </summary>
        /// <param name="messageDataTable"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveMsgInfo(HemodialysisModel.MED_COMMON_MESSAGEDataTable messageDataTable);

        /// <summary>
        /// 保存系统消息为已读
        /// </summary>
        /// <param name="msgID"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveMsgInfoToMarkRead(string msgID);

        /// <summary>
        /// 根据透析号获取患者临时与长期处方执行状态与内容列表
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetQueryRecipeList(string pHemoID);

        /// <summary>
        /// 根据当前已排班人员的透析ID生成当日临时处方
        /// </summary>
        /// <param name="pHemoList">当日已排班病人ID字符串</param>
        /// <returns></returns>
        [OperationContract]
        int SaveTodayRecipes();

        /// <summary>
        /// 计算患者透析次数
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetCureCountByHemoID(string pHemoID);

        /// <summary>
        /// 根据透析号得到对应治疗单数据SQL
        /// <param name="pHemoID">透析号</param>
        /// </summary>
        [OperationContract]
        DataTable GetMainCureListByHemoID(string pHemoID);

        /// <summary>
        /// 根据年月统计舒适度评价数据
        /// <param name="dateMonth">数据年月</param>
        /// </summary>
        [OperationContract]
        DataTable GetSubjectiveComfortData(string dateMonth);
        /// <summary>
        /// 得到时间段内不同透析类型的透析次数
        /// </summary>
        /// <param name="beginCureCreateDate">开始日期</param>
        /// <param name="endCureCreateDate">结束日期</param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetAllCureTypeCount(DateTime beginCureCreateDate, DateTime endCureCreateDate);

        /// <summary>
        /// 得到透析月度统计数据
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataTable GetAllCureCountByMonth(DateTime _bgionTime, DateTime _endTime);

        /// <summary>
        /// 得到全部病人乙肝、丙肝、梅毒、HIV检查结果列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_INFECTIOUS_CHECKDataTable GetMedInfectiousCheckList(string pHEMODIALYSIS_ID);

        [OperationContract]
        DataTable GetMedInfectiousCheck();

        [OperationContract]
        int UpdateMedInfectiousInfoByID(string pInfectiousCheckID);

        /// <summary>
        /// 保存传染病检查数据
        /// </summary>
        /// <param name="pInfectiousCheckDataTable"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveMedInfectiousCheck(HemodialysisModel.MED_INFECTIOUS_CHECKDataTable pInfectiousCheckDataTable);

        /// <summary>
        /// 根据传染病表的ID得到一条传染病检查数据 INFECTIOUS_CHECK
        /// </summary>
        /// <param name="pInfectiousCheckID">传染病表ID</param>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_INFECTIOUS_CHECKDataTable GetMedInfectiousInfoByID(string pInfectiousCheckID);

        [OperationContract]
        DataSet GetPatientGuideVisitInfo(string pHemoID, DateTime pDialysisDate);
        /// <summary>
        /// 根据类型和透析号获取数据
        /// </summary>
        /// <param name="pHemoID"></param>
        /// <param name="pItemType"></param>
        /// <returns></returns>
        [OperationContract]
        ConfigModel.MED_COMMON_ITEMLISTDataTable GetItemListByHemoIDandItemType(string pHemoID, string pItemType);

        /// <summary>
        /// 获取患者历次透析通路
        /// </summary>
        /// <param name="phemoId"></param>
        /// <returns></returns>
        [OperationContract]
        HemoModel.MED_VASCULAR_ACCESSDataTable GetPatientVasular_AccessDt(string phemoId);

        /// <summary>
        /// 得到疾病ICD分类
        /// </summary>
        /// <returns>ICD疾病分类列表</returns>
        [OperationContract]
        HemodialysisModel.MED_ICD_TYPEDataTable GetIcdType();

        /// <summary>
        /// 根据ICD分类编码取出对应的ICD病种
        /// </summary>
        /// <param name="pICD">ICD编码</param>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_ICD_LISTDataTable GetIcdListByID(string pICD);

        /// <summary>
        /// 根据ICD分类跨度编码取出对应的ICD病种
        /// </summary>
        /// <param name="pIdList">ICD分类跨度</param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetIcdListByIDList(string pIdList);

        /// <summary>
        /// 删除透析参数
        /// </summary>
        /// <param name="pID">透析参数ID</param>
        /// <returns></returns>
        [OperationContract]
        int DeleteHemodialysisParametersByID(string pID);

        /// <summary>
        /// 更新ICD的拼音码
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int UpdateIcdPinYin();

        /// <summary>
        /// 根据疾病名称和拼音码得到疾病列表
        /// </summary>
        /// <param name="pICD_NAME">疾病名称或拼音码</param>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_ICD_LISTDataTable GetIcdListByName(string pICD_NAME);

        [OperationContract]
        int DeleteCureORLongDrugByID(string drugType, string cureID);
        //获取医嘱组合号
        [OperationContract]
        string GetOrderComNo();

        /// <summary>
        /// 根据时间获取未执行的医嘱信息
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetUNExcuteOrdersbyData(DateTime dt);


        #region  健康宣教相关

        [OperationContract]
        DataTable GetPamarsDrugInfo(string pCureID, DateTime pCreateDate);

        [OperationContract]
        int SaveHealthEducationInfo(HemodialysisModel.MED_HEALTH_EDUCATIONDataTable healthDataTable);

        [OperationContract]
        HemodialysisModel.MED_HEALTH_EDUCATIONDataTable GetHealthEducationByHemoID(string pHemoID);

        [OperationContract]
        DataTable GetHealthEducationListByHemoID(string pHemoID);

        [OperationContract]
        HemodialysisModel.MED_HEALTH_EDUCATIONDataTable GetHealthEducationByHemoIdAndId(string pHemoID, string id);

        [OperationContract]
        HemodialysisModel.MED_HEALTH_EDUCATIONDataTable GetHealthEducationByDateTime(DateTime startTime, DateTime endTime);

        [OperationContract]
        DrugModel.MED_PATIENT_FOLLOWUPDataTable GetFollowUpByDateTime(DateTime startTime, DateTime endTime);

        [OperationContract]
        DataTable GetHealthEducationReportByHemoID(string pHemoID, string pID);

        [OperationContract]
        DataTable GetBaseHemoInfo(DateTime _begionTime, DateTime _endTime);

        [OperationContract]
        DataTable GetSexScale(DateTime _begionTime, DateTime _endTime);

        [OperationContract]
        DataTable GetAgeScale(DateTime _begionTime, DateTime _endTime);

        [OperationContract]
        string GetAllHemoCount(DateTime _begionTime, DateTime _endTime);

        [OperationContract]
        DataTable GetInfectiousScale(DateTime _begionTime, DateTime _endTime);

        [OperationContract]
        DataTable GetHemoCoutScale(DateTime _begionTime, DateTime _endTime);

        [OperationContract]
        int SavePatientProgressNoteInfo(HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable ProgressNoteDataTable);

        [OperationContract]
        DataTable GetPatientProgressNoteById(string id);

        [OperationContract]
        HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable GetPatientProgressNoteByHemoId(string hemoId);

        [OperationContract]
        HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable GetPatientProgressNoteByHemoIdAndDate(string hemoId, DateTime beginDate, DateTime endDate);

        [OperationContract]
        HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable GetPatientProgressNoteByIDAndCreateDate(string hemoID, DateTime createDate);

        [OperationContract]
        int DeletePatientProgressNoteById(string id);

        [OperationContract]
        HemodialysisModel.MED_HEMO_PARAMETERS_COLLECTIONDataTable GetHemoParametersCollectionByMonitorAndDate(string MonitorLable, DateTime pCreateDate);

        [OperationContract]
        HemodialysisModel.MED_HEMO_PARAMETERS_COLLECTIONDataTable GetHemoParametersCollectionByMonitorAndDoubleDate(string MonitorLable, DateTime pCreateDate, DateTime beginTime, DateTime endTime);

        [OperationContract]
        int SaveEstimateInBasketInfo(HemodialysisModel.MED_ESTIMATE_IN_BASKETDataTable EsitmateInBasketTable);

        [OperationContract]
        HemodialysisModel.MED_ESTIMATE_IN_BASKETDataTable GetEsimateInBasketByID(string pID);

        [OperationContract]
        DataTable GetEstimateInBasketByParams(string pHemoID, string pName, DateTime beginDate, DateTime endDate);

        [OperationContract]
        int DeleteEstimateInBasketById(string id);

        [OperationContract]
        HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable GetEstimateLongVenousByNameAndDate(string name, DateTime beginDate, DateTime endDate);

        [OperationContract]
        HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable GetEstimateVenousCatheterByNameAndDate(string name, DateTime beginDate, DateTime endDate);

        [OperationContract]
        HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable GetEstimateLongVenousByHemoIdAndDate(string hemoId, DateTime beginDate, DateTime endDate);

        [OperationContract]
        HemodialysisModel.MED_PREOPERATIVE_NURSINGDataTable GetPreoperativeNursingByNameAndDate(string name, DateTime beginDate, DateTime endDate);

        [OperationContract]
        HemodialysisModel.MED_PREOPERATIVE_NURSINGDataTable GetPreoperativeNursingByHemoIdAndDate(string hemoId, DateTime beginDate, DateTime endDate);

        [OperationContract]
        HemodialysisModel.MED_PAT_TRANSFER_HANDOVERDataTable GetPatTransferHandoverByNameAndDate(string name, DateTime beginDate, DateTime endDate);

        [OperationContract]
        HemodialysisModel.MED_PAT_TRANSFER_HANDOVERDataTable GetPatTransferHandoverByHemoIdAndDate(string hemoId, DateTime beginDate, DateTime endDate);

        [OperationContract]
        HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable GetEstimateVenousCatheterByHemoIdAndDate(string hemoId, DateTime beginDate, DateTime endDate);

        [OperationContract]
        HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable GetEstimateLongVenousList();

        [OperationContract]
        HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable GetEstimateVenousCatheterList();

        [OperationContract]
        HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable GetEstimateLongVenousById(string id);

        [OperationContract]
        HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable GetEstimateVenousCatheterById(string id);

        [OperationContract]
        HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable GetEstimateLongVenousByHemoIdAndSingleDate(string hemoId, DateTime createDate);

        [OperationContract]
        HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable GetEstimateVenousCatheterByHemoIdAndSingleDate(string hemoId, DateTime createDate);
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
        [OperationContract]
        DataTable GetHoldLabItemDt(string hemoId, string whereName, string whereFilter, DateTime dtStar, DateTime dtEnd, string condition);

        /// <summary>
        /// DeleteEstimateLongVenousById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        int DeleteEstimateLongVenousById(string id);

        /// <summary>
        /// DeleteEstimateVenousCatheterById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        int DeleteEstimateVenousCatheterById(string id);

        /// <summary>
        /// DeletePreoperativeNursingById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        int DeletePreoperativeNursingById(string id);

        /// <summary>
        /// DeletePatTransferHandoverById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        int DeletePatTransferHandoverById(string id);

        /// <summary>
        /// SaveEstimateVenous
        /// </summary>
        /// <param name="dtEstimateVenous"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveEstimateVenous(DataTable dtEstimateVenous);

        /// <summary>
        /// GetDataGatherSetList
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_HEMO_PARAMETERS_SETTINGDataTable GetDataGatherSetList();

        /// <summary>
        /// SaveDataGatherSet
        /// </summary>
        /// <param name="dtSetting"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveDataGatherSet(HemodialysisModel.MED_HEMO_PARAMETERS_SETTINGDataTable dtSetting);

        /// <summary>
        /// SaveWorkload
        /// </summary>
        /// <param name="workloadTable"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveWorkload(HemoModel.MED_WORKLOADDataTable workloadTable);

        /// <summary>
        /// GetWorkloadByParmas
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="banchiName"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [OperationContract]
        HemoModel.MED_WORKLOADDataTable GetWorkloadByParmas(string areaId, string banchiName, DateTime date);

        /// <summary>
        /// GetWorkloadByDate
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [OperationContract]
        HemoModel.MED_WORKLOADDataTable GetWorkloadByDate(DateTime beginDate, DateTime endDate);

        /// <summary>
        /// GetWorkloadByDate
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [OperationContract]
        HemoModel.MED_WORKLOADDataTable GetWorkloadByDateFZ(DateTime beginDate, DateTime endDate);

        /// <summary>
        /// GetWorkLoadCountByDate
        /// </summary>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        [OperationContract]
        HemoModel.MED_WORKLOADDataTable GetWorkLoadCountByDate(DateTime dateBegin, DateTime dateEnd);

        [OperationContract]
        HemoModel.MED_WORKLOADDataTable GetWorkLoadNurseCountByDate(DateTime dateBegin, DateTime dateEnd);

        /// <summary>
        /// SaveComplication
        /// </summary>
        /// <param name="complicationTable"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveComplication(HemoModel.MED_COMPLICATION_OTHERDataTable complicationTable);

        /// <summary>
        /// GetComplicationByDialysisAndCure
        /// </summary>
        /// <param name="DialysisId"></param>
        /// <param name="cureId"></param>
        /// <returns></returns>
        [OperationContract]
        HemoModel.MED_COMPLICATION_OTHERDataTable GetComplicationByDialysisAndCure(string DialysisId, string cureId);

        /// <summary>
        /// GetComplicationByDate
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [OperationContract]
        HemoModel.MED_COMPLICATION_OTHERDataTable GetComplicationByDate(DateTime date);


        #region 综合

        [OperationContract]
        HemoModel.MED_COMPLICATION_OTHERDataTable GetComplicationByParams(DateTime firstdate, DateTime lastdata);

        [OperationContract]
        DataTable GetDryWeightListByHemoID(string pHemoID);

        [OperationContract]
        DataTable GetRecentCureInfoByHemoId(string hemoId);

        [OperationContract]
        DataTable GetRecentPressureByHemoId(string hemoId);

        [OperationContract]
        DataTable GetPastCureInfoByHemoId(string hemoId);

        [OperationContract]
        DataTable GetPastPressureByHemoId(string hemoId);

        [OperationContract]
        DataTable GetPatientCureAndPastPressureByHemoId(string hemoId);

        [OperationContract]
        DataTable GetPatientCureAndPastPressureByParam(string hemoId, int minData, int maxData);

        [OperationContract]
        DataTable GetHemoCureCountList(string year);

        [OperationContract]
        string GetLastTimeCureDataByID(string pHemoID, DateTime pCURE_CREATE_DATE);

        [OperationContract]
        DataTable GetCurePurificationModeCountByDate(string pCureDate);
        #endregion
        //  [OperationContract]
        //   DataTable GetCurePurificationModeCountByDate(DateTime pCureDate);

        /// <summary>
        /// 保存患者借药数据
        /// </summary>
        /// <param name="borrowData"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveBorrowData(HemodialysisModel.MED_BORROW_MEDICINE_DETAILDataTable borrowData);


        /// <summary>
        /// 归还药品确认保存
        /// </summary>
        /// <param name="BackID"></param>
        /// <param name="dateTime"></param>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveBorrowDataBack(string BackID, DateTime dateTime, string backUserInfo, string userID);


        /// <summary>
        /// 查询患者评估记录
        /// </summary>
        /// <param name="pHemoID">血透病例号</param>       
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>数据表</returns>
        [OperationContract]
        HemoModel.MED_PATIENTS_ASSESSMENTDataTable GetAssessmentByParams(string pHemoID, DateTime beginDate, DateTime endDate);

        /// <summary>
        /// 删除一条评估记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [OperationContract]
        int DeleteAssessmentById(string id, string p);

        /// <summary>
        /// 保存评估
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveAssessmentByDate(HemoModel.MED_ASSESSMENTMASTERDataTable data);

        /// <summary>
        /// GetAssessmentByAssID
        /// </summary>
        /// <param name="AssessmentID"></param>
        /// <returns></returns>
        [OperationContract]
        HemoModel.MED_PATIENTS_ASSESSMENT_ATTRDataTable GetAssessmentByAssID(string AssessmentID);

        /// <summary>
        /// GetAssParamByHemoID
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_HEMO_RECIPEDataTable GetAssParamByHemoID(string hemoId);

        /// <summary>
        /// GetAllLabUnCurePatients
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataSet GetAllLabUnCurePatients();

        /// <summary>
        /// GetALLOfficeData
        /// </summary>
        /// <param name="_statOfficeData"></param>
        /// <param name="_endOfficeData"></param>
        /// <returns></returns>
        [OperationContract]
        DataSet GetALLOfficeData(DateTime _statOfficeData, DateTime _endOfficeData);


        //获取人未做评估的天数
        [OperationContract]
        int GetNotAssentDayByHemoId(string hemoId);
        [OperationContract]
        HemoModel.MED_HEMO_WORKOVERTIMEDataTable GetNurseWorkOverTimeRecordByDate(DateTime _statDate, DateTime _endDate);

        /// <summary>
        /// 保存护士工作时间
        /// </summary>
        /// <param name="_data"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveNurseWorkOverTime(HemoModel.MED_HEMO_WORKOVERTIMEDataTable _data);

        /// <summary>
        /// 删除护士工作
        /// </summary>
        /// <param name="_workNurseId"></param>
        /// <returns></returns>
        [OperationContract]
        int DeleteNurseWorkOverTimeByID(string _workNurseId);

        #region  处方相关


        [OperationContract]
        int CreatePatientRecipeBydate(DateTime recipeDate);

        [OperationContract]
        int DeleteUnExcuteRecipeByHemoID(string hemoId, DateTime dt);

        [OperationContract]
        PatientModel.MED_BASE_RECORDDataTable GetBaseRecordByHemoId(string hemoId);

        [OperationContract]
        int SaveBaseRecord(PatientModel.MED_BASE_RECORDDataTable dtRecord);

        [OperationContract]
        HemodialysisModel.MED_BASE_RECORD_EVENTDataTable GetRecordEventByHemoId(string hemoId);

        [OperationContract]
        int SaveRecordEvent(HemodialysisModel.MED_BASE_RECORD_EVENTDataTable dtRecord);

        [OperationContract]
        int DeleteRecordEventById(string id);

        [OperationContract]
        HemodialysisModel.MED_BASE_RECORD_DIAGNOSEDataTable GetRecordDiagnoseByHemoId(string hemoId);

        [OperationContract]
        int SaveRecordDiagnose(HemodialysisModel.MED_BASE_RECORD_DIAGNOSEDataTable dtRecord);

        [OperationContract]
        int DeleteRecordDiagnoseById(string id);

        [OperationContract]
        HemoModel.MED_PATIENTS_URRDataTable GetPatientURRByHemoId(string hemoId);

        [OperationContract]
        int SavePatientURR(HemoModel.MED_PATIENTS_URRDataTable dtPatientURR);

        [OperationContract]
        HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable GetEstimateSufficiencyByHemoId(string hemoId, string flag);

        [OperationContract]
        HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable GetEstimateSufficiencyByFlag(string[] flag);

        [OperationContract]
        HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable GetEstimateSufficiencyByHemoIdAndDate(string hemoId, string flag, DateTime beginDate, DateTime endDate);

        [OperationContract]
        HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable GetEstimateSufficiencyByFlagAndDate(string[] flag, DateTime beginDate, DateTime endDate);

        [OperationContract]
        HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable GetEstimateSufficiencyByHemoIdAndDate(string hemoId, string flag, DateTime createDate);

        [OperationContract]
        int SaveEstimateSufficiency(HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable dtSufficiency);

        [OperationContract]
        int DeleteEstimateSufficiencyById(string id);

        #endregion

        /// <summary>
        /// 获取一段时间内的透后体重
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="dtStar">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns>数据集</returns>
        [OperationContract]
        DataTable GetPastWeightListByParams(string pHemoID, DateTime dtStar, DateTime dtEnd);
        /// <summary>
        /// 获取一段时间内的透中血压
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="dtStar">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns>数据集</returns>
        [OperationContract]
        DataTable GetPastBloodPresureListByParams(string pHemoID, DateTime dtStar, DateTime dtEnd);

        /// <summary>
        /// SaveBookPicture
        /// </summary>
        /// <param name="dtBookPicture"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveBookPicture(HemoModel.MED_BOOK_PICTUREDataTable dtBookPicture);

        /// <summary>
        /// GetBookPictureByHemoAndBookName
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="bookName"></param>
        /// <returns></returns>
        [OperationContract]
        HemoModel.MED_BOOK_PICTUREDataTable GetBookPictureByHemoAndBookName(string hemoId, string bookName);

        /// <summary>
        /// SaveCureSign
        /// </summary>
        /// <param name="dtCureSign"></param>
        /// <returns></returns>
        [OperationContract]
        int SaveCureSign(HemoModel.MED_CURE_SIGNDataTable dtCureSign);

        /// <summary>
        /// GetCureSignByHemoIdAndCureId
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="cureId"></param>
        /// <returns></returns>
        [OperationContract]
        HemoModel.MED_CURE_SIGNDataTable GetCureSignByHemoIdAndCureId(string hemoId, string cureId);

        /// <summary>
        /// 根据日期获取最近一次透析为基准，上周透析过、三个月内连续透析过患者透析编号
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetHemoIdInLastWeekAndThreeMonthsByDate(DateTime beginTime, DateTime endTime);

        /// <summary>
        /// 根据日期获取患者透析编号
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        DataTable GetHemoIdByDate(DateTime beginTime, DateTime endTime);

        /// <summary>
        /// GetPatientBaseRecordProtopathyByDate
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetPatientBaseRecordProtopathyByDate(DateTime beginTime, DateTime endTime);

        /// <summary>
        /// 根据透析编号获取是否维持性透析患者
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        [OperationContract]
        string GetCureTypeByHemoId(string hemoId);

        /// <summary>
        /// 按月份根据透析编号和日期获取患者透析次数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetCureCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime);

        /// <summary>
        /// 根据治疗号获取并发症内容
        /// </summary>
        /// <param name="cureId"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetComplicationOther(string cureId);

        /// <summary>
        /// GetPatientHemoAge
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pAGE"></param>
        /// <param name="pHEMOAGE"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetPatientHemoAge(string pName, decimal pAGE, decimal pHEMOAGE);

        #region 报表相关
        [OperationContract]
        DataTable QueryPatientMoreInfoList(DateTime beginDate, DateTime endDate, string beginAge, string endAge, string beginHemoAge, string endHemoAge, string pSex);

        DataTable GetAccessCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime);

        DataTable GetDuctOperationCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime);

        DataTable GetSexCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime);

        DataTable GetSexCountByHemoIdAndDate2(string hemoId, DateTime beginTime, DateTime endTime);

        [OperationContract]
        DataTable GetSexCountByDate(DateTime beginTime, DateTime endTime);

        [OperationContract]
        DataTable GetSexCountByDate2(DateTime beginTime, DateTime endTime);

        DataTable GetAgeCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime);

        DataTable GetInfectousCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime);

        DataTable GetRegularCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime);

        DataTable GetWeekTwoOrThirdCountPatientsHemoId();

        [OperationContract]
        int DeleteHealthEducationByHemoIdAndId(string pHemoID, string id);


        [OperationContract]
        int DeleteMED_ANEMIA_CKDMBD_ASSESSbyID(string ID);

        [OperationContract]
        int SaveMED_ANEMIA_CKDMBD_ASSESS(HemodialysisModel.MED_ANEMIA_CKDMBD_ASSESSDataTable dt);

        [OperationContract]
        HemodialysisModel.MED_ANEMIA_CKDMBD_ASSESSDataTable GetMED_ANEMIA_CKDMBD_ASSESSbyDate(DateTime beginTime, DateTime endTime, string ASSESS_TYPE, string hemoId);

        [OperationContract]
        HemodialysisModel.MED_ANEMIA_CKDMBD_ASSESSDataTable GetMED_ANEMIA_CKDMBD_ASSESS(string ASSESS_TYPE, string hemoId);

        [OperationContract]
        DataTable GetDeathRate(DateTime pBeginTime, DateTime pEndTime, string HemoIDList);

        [OperationContract]
        int GetTempRecipeCount(string pHemoID);

        [OperationContract]
        HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable GetPerformanceAppraisalByDate(DateTime beginTime, DateTime endTime);

        [OperationContract]
        HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable GetPerformanceAppraisalByDateAndNurseLeader(DateTime beginTime, DateTime endTime, string nurseLeader);

        [OperationContract]
        HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable GetPerformanceAppraisalByDateAndLeaderFlag(DateTime beginTime, DateTime endTime, string isLeader);

        [OperationContract]
        HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable GetPerformanceAppraisalByDateAndNurse(DateTime beginTime, DateTime endTime, string nurse);

        [OperationContract]
        HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable GetPerformanceAppraisalRuleByType(string type);

        [OperationContract]
        HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable GetPerformanceAppraisalRuleByScoreType(string type);

        [OperationContract]
        HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable GetPerformanceAppraisalRuleById(string id);

        [OperationContract]
        int SavePerformanceAppraisalRule(HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable dtRule);

        [OperationContract]
        int SavePerformanceAppraisal(HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable dtAppraisal);

        [OperationContract]
        DataTable GetCurePatientByTimeAndFrequency(int time, int frequency);

        [OperationContract]
        DataTable GetPatientTypeIsNew(string pHemoID);
        #endregion

        [OperationContract]
        HemodialysisModel.MED_UPLOAD_LOGDataTable GetUploadLogByItemNameAndYear(string itemName, string year);

        [OperationContract]
        int SaveUploadLog(HemodialysisModel.MED_UPLOAD_LOGDataTable dtUploadLog);


        /// <summary>
        /// 根据日期获取透析参数的人员用户谁上机谁下机的查询
        /// </summary>
        /// <param name="dialisysDate"></param>
        /// <returns></returns>
        [OperationContract]
        HemoModel.MED_USER_WORKLOAD_EXTENDERDataTable GetParamUserWorkExtend(DateTime dialisysDate);
        /// <summary>
        /// 根据日期获取开始治疗【上机人员工作量】
        /// </summary>
        /// <param name="dialisysDate"></param>
        /// <returns></returns>
        [OperationContract]
        HemoModel.MED_USER_WORKLOAD_EXTENDERDataTable GetUserWorkExtendFromCureMain(DateTime dialisysDate);

        /// <summary>
        /// 根据日期获取透析参数的人员用户谁上机谁下机的查询
        /// </summary>
        /// <param name="dialisysDate"></param>
        /// <returns></returns>
        [OperationContract]
        HemoModel.MED_USER_WORKLOAD_EXTENDERDataTable GetParamALLUserWorkExtend(DateTime dtStar, DateTime dtEnd);

        /// <summary>
        /// 保存患者转运信息
        /// </summary>
        /// <param name="dtEstimateVenous"></param>
        /// <returns></returns>
        [OperationContract]
        int SavePatTransferHandover(HemodialysisModel.MED_PAT_TRANSFER_HANDOVERDataTable dtPatTransferHandover);



        /// <summary>
        /// 获取病人指定时间到当前时间的治疗单
        /// </summary>
        /// <param name="hemoId">透析号</param>
        /// <param name="cureDt">治疗时间</param>
        /// <returns></returns>
        [OperationContract]
        HemodialysisModel.MED_CURE_MAINDataTable GetCureListByHemoId(string hemoId, DateTime cureDt);

        [OperationContract]
        DataTable GetCureMainByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime);

        [OperationContract]
        DataTable GetPatientByHemoId(string hemoId);


        /// <summary>
        /// 根据透析编号和日期获取患者通路治疗类型例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetCureVascularTypeCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime);


        [OperationContract]
        DataTable GetInfectiousCountByParams(string hemoId, DateTime beginTime, DateTime endTime, string infectName, string infectCode);


        [OperationContract]
        DataTable GetLabValueLineTypeByparams(string hemoId, string itemName, DateTime begionTime, DateTime endTime);

        /// <summary>
        /// 根据透析编号和日期获取患者通路类型例数
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetVascularTypeCountByHemoIdAndDate(string hemoId, DateTime beginTime, DateTime endTime);
    }
}