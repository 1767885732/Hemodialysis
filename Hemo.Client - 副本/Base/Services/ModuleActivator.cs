/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: 窗体显示的是moduletype对应的焦点
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/
namespace Hemo.Client.Base.Services
{
    using Hemo.Client.Modules;
    using Hemo.Model;
    using System;
    using System.Reflection;
    #region ModuleActivator
    
    /// <summary>
    /// IModuleActivator
    /// </summary>
    public interface IModuleActivator {
        object CreateModule(string moduleTypeName);
        object CreateModule(string moduleTypeName, object viewModel);
    }
    /// <summary>
    /// ModuleActivator
    /// </summary>
    internal class ModuleActivator : IModuleActivator {
        private Assembly moduleAssembly;
        private string rootNamespace;
        public ModuleActivator(Assembly moduleAssembly, string rootNamespace) {
            this.moduleAssembly = moduleAssembly;
            this.rootNamespace = rootNamespace;
        }
        public object CreateModule(string moduleTypeName) {
            var moduleType = moduleAssembly.GetType(rootNamespace + '.' + moduleTypeName);
            if ( moduleType ==null)
            {
                return null;
            }
            if (moduleType == Type.GetType("Hemo.Client.Modules.PatientDetail"))
            {
                return PatientDetail.PdInstance();
            }
            return Activator.CreateInstance(moduleType);
        }
        public object CreateModule(string moduleTypeName, object viewModel) {
            var moduleType = moduleAssembly.GetType(rootNamespace + '.' + moduleTypeName);
            return Activator.CreateInstance(moduleType, new object[] { viewModel });
        }
    }
    /// <summary>
    /// IReportActivator
    /// </summary>
    public interface IReportActivator {
        object CreateReport(string reportTypeName);
    }
    /// <summary>
    /// ReportActivator
    /// </summary>
    internal class ReportActivator : IReportActivator {
        private Assembly reportAssembly;
        private string rootNamespace;
        public ReportActivator(Assembly moduleAssembly, string rootNamespace) {
            reportAssembly = moduleAssembly;
            this.rootNamespace = rootNamespace;
        }
        public object CreateReport(string reportTypeName) {
            var moduleType = reportAssembly.GetType(rootNamespace + '.' + reportTypeName);
            return Activator.CreateInstance(moduleType);
        }
    }
    #endregion

}
