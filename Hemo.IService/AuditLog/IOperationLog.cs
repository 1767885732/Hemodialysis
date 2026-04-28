/*----------------------------------------------------------------
// Copyright (C) 2026
// 描述：操作审计日志服务接口
// 创建时间：2026-04-21
// 创建者：龙宇涵
//
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/
using System.ServiceModel;

namespace Hemo.IService.AuditLog
{
    /// <summary>
    /// 操作审计日志服务接口
    /// </summary>
    [ServiceContract]
    public interface IOperationLog
    {
        /// <summary>
        /// 写入操作审计日志（变更详情统一写入 CHANGE_DETAIL 字段）
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户姓名</param>
        /// <param name="loginName">登录名</param>
        /// <param name="operationType">操作类型（SAVE/DELETE/UPDATE等）</param>
        /// <param name="moduleName">模块名称（如：EditTreatment）</param>
        /// <param name="elementName">元素名称（如：透析参数、给药信息、血管通路等）</param>
        /// <param name="elementId">元素ID（如：CURE_ID、HEMODIALYSIS_PARAMETERS_ID等）</param>
        /// <param name="changeDetail">变更详情（如：透前体重: 65 → 68; 实际脱水: 2 → 3）</param>
        /// <param name="cureId">治疗单ID</param>
        /// <param name="hemodialysisId">血透编号</param>
        /// <param name="remark">备注</param>
        /// <returns>受影响的行数</returns>
        [OperationContract]
        int WriteLog(string userId, string userName, string loginName,
            string operationType, string moduleName, string elementName, string elementId,
            string changeDetail, string cureId, string hemodialysisId, string remark);
    }
}
