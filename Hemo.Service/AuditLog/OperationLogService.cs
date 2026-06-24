/*----------------------------------------------------------------
// Copyright (C) 2026
// 描述：操作审计日志服务实现
// 创建时间：2026-04-21
// 创建者：龙宇涵
//
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/
using System;
using Hemo.Business.AuditLog;
using Hemo.IService.AuditLog;

namespace Hemo.Service.AuditLog
{
    /// <summary>
    /// 操作审计日志服务实现
    /// </summary>
    public class OperationLogService : MarshalByRefObject, IOperationLog
    {
        #region IOperationLog 成员

        /// <summary>
        /// 写入操作审计日志
        /// </summary>
        public int WriteLog(string userId, string userName, string loginName,
            string operationType, string moduleName, string elementName, string elementId,
            string changeDetail, string cureId, string hemodialysisId, string remark)
        {
            return OperationLogBll.WriteLog(userId, userName, loginName,
                operationType, moduleName, elementName, elementId,
                changeDetail, cureId, hemodialysisId, remark);
        }

        #endregion
    }
}
