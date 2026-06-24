/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:ModuleTypesResolver
 * 创建标识:吕志强-2017年2月3日
 * 修改描述:修改对外公开的方法
 * ----------------------------------------------------------------*/
using Hemo.Client.Base.XtraBaseInfo;
namespace Hemo.Client.Base.Services
{
    #region ModuleTypesResolver
    
    /// <summary>
    /// IModuleTypesResolver
    /// </summary>
    public interface IModuleTypesResolver {
        string GetName(ModuleType moduleType);
        string GetTypeName(ModuleType moduleType);
        System.Guid GetId(ModuleType moduleType);
        ModuleType GetPeekModuleType(ModuleType type);
        ModuleType GetExportModuleType(ModuleType type);
        ModuleType GetPrintModuleType(ModuleType type);
        string GetReportTypeName(object reportType);
    }
    /// <summary>
    ///internal  ModuleTypesResolver
    /// </summary>
    internal class ModuleTypesResolver : IModuleTypesResolver {
        public ModuleType GetExportModuleType(ModuleType moduleType) {
            switch (moduleType) {
                default:
                    return ModuleType.Unknown;
            }
        }
        public ModuleType GetPrintModuleType(ModuleType moduleType) {
            switch (moduleType) {
                default:
                    return ModuleType.Unknown;
            }
        }
        public string GetName(ModuleType moduleType) {
            if (moduleType == ModuleType.Unknown) {
                return null;
            }
            return moduleType.ToString();
        }
        public string GetTypeName(ModuleType moduleType) {
            if (moduleType == ModuleType.Unknown) {
                return null;
            }
            return moduleType.ToString();
        }
        public string GetReportTypeName(object reportType) {
            return reportType.GetType().Name.Replace("ReportType", string.Empty) + reportType.ToString();
        }
        public System.Guid GetId(ModuleType moduleType) {
            switch (moduleType) {
                case ModuleType.Employees:
                case ModuleType.EmployeeEdit:
                case ModuleType.EmployeeView:
                    return new System.Guid("f4e3551d-6679-4db6-a103-1e25d7bc83a2");
                case ModuleType.CustomersModule:
                case ModuleType.CustomersPeek:
                case ModuleType.CustomersFilterPane:
                    return new System.Guid("f4e3551d-6679-4db6-a103-1e25d7bc83a3");
                default:
                    return System.Guid.Empty;
            }
        }
        public ModuleType GetMapModuleType(ModuleType moduleType) {
            switch (moduleType) {
                default:
                    return ModuleType.Unknown;
            }
        }
        public ModuleType GetPeekModuleType(ModuleType moduleType) {
            switch (moduleType) {
                case ModuleType.Employees:
                case ModuleType.CustomersModule:
                case ModuleType.CustomersPeek:
                case ModuleType.CustomersFilterPane:
                    return ModuleType.CustomersPeek;
                default:
                    return ModuleType.Unknown;
            }
        }
        public ModuleType GetNavPaneModuleType(ModuleType moduleType) {
            switch (moduleType) {
                case ModuleType.Employees:
                case ModuleType.CustomersModule:
                case ModuleType.CustomersPeek:
                case ModuleType.CustomersFilterPane:
                    return ModuleType.CustomersFilterPane;
                default:
                    return ModuleType.Unknown;
            }
        }
    }
    #endregion

}
