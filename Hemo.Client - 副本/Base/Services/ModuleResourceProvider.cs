/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:IModuleResourceProvider
 * 创建标识:吕志强-2017年1月29日
 * ----------------------------------------------------------------*/
using Hemo.Client.Base.XtraBaseInfo;


namespace Hemo.Client.Base.Services
{
    #region ModuleResourceProvider
    
    /// <summary>
    /// IModuleResourceProvider
    /// </summary>
    public interface IModuleResourceProvider {
        string GetCaption(ModuleType moduleType);
        object GetModuleImage(ModuleType moduleType);
    }
    /// <summary>
    /// ModuleResourceProvider
    /// </summary>
    public class ModuleResourceProvider : IModuleResourceProvider {
        public string GetCaption(ModuleType moduleType) {
            if (moduleType == ModuleType.Unknown) {
                return null;
            }
            return moduleType.ToString();
        }
        public virtual object GetModuleImage(ModuleType moduleType) {
            return null;
        }
    }
    #endregion

}
namespace Hemo.Client.Base.Services.Win
{
    #region ModuleResourceProvider
    /// <summary>
    /// ModuleResourceProvider
    /// </summary>
    public class ModuleResourceProvider : Hemo.Client.Base.Services.ModuleResourceProvider {
        public override object GetModuleImage(ModuleType moduleType) {
            if (moduleType == ModuleType.Unknown) {
                return null;
            }
            switch (moduleType) {
                case ModuleType.Employees:
                case ModuleType.CustomersModule:
                case ModuleType.CustomersFilterPane:
                  
                    return DevExpress.Images.ImageResourceCache.Default.GetImage(@"images/people/usergroup_32x32.png");
                default:
                    return null;
            }
        }
    }
    #endregion

}
