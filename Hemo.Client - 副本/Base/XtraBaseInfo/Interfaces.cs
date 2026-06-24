/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: 系统模块设置
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/
using DevExpress.Utils.Menu;
using DevExpress.XtraBars.Ribbon;

namespace Hemo.Client.Base.XtraBaseInfo
{
    #region MoudleType
    
    /// <summary>
    /// 窗体MoudleType
    /// </summary>
    public enum ModuleType
    {
        Unknown,
        PatientMgr,  //患者管理
        PatientTreantmentFrmNew, //患者治疗
        PatientScheduleFrmN, //患者排班
        DataReportManagerMgr,//患者上报平台
        CtlSystemSet, //系统设置
        EqumentInfoMgr, //设备管理
        ReportMainMgr, //统计报表
        AllPatientList,//所有患者列表
        PatientEdit,//患者基本信息
        PatientDocImageMgr,//扫描件上传管理
        PatientDetail,//单患者综合界面管理
        PatientCardMgr,//透析卡片管理
        PatientOperatorUI,//患者手术信息
        TempDrug,//临时医嘱

        EmployeeView,
        EmployeeEdit,
        EmployeesPrint,
        EmployeeMailMerge,
        Employees,

        CustomersFilterPane,
        CustomersPeek,
        CustomerEditableView,
        CustomersModule,
        CustomerDetails,

        Dashboard,
        Tasks,
        EditTask,
        TaskPrint,
        ExitForm,

        Products,
        ProductsEditableView,

        Sales,
        SalesPrint,
        OrderView,
        Opportunities,
        EditNotes,
        Notes,
    }
    #endregion


    #region Interface

    /// <summary>
    /// IRibbonModule
    /// </summary>
    public interface IRibbonModule {
        RibbonControl Ribbon { get; }
    }
    /// <summary>
    /// ISupportViewModel
    /// </summary>
    public interface ISupportViewModel {
        object ViewModel { get; }
        void ParentViewModelAttached();
    }

    /// <summary>
    /// IMainModule
    /// </summary>
    public interface IMainModule : IPeekModulesHost,
    ISupportModuleLayout, ISupportTransitions, IDXMenuManagerProvider
    {
    }
    /// <summary>
    /// ISupportTransitions
    /// </summary>
    public interface ISupportTransitions
    {
        void StartTransition(bool effective);
        void EndTransition(bool effective);
    }
    /// <summary>
    /// ISupportModuleLayout
    /// </summary>
    public interface ISupportModuleLayout
    {
        void SaveLayoutToStream(System.IO.MemoryStream ms);
        void RestoreLayoutFromStream(System.IO.MemoryStream ms);
    }
    /// <summary>
    /// IPeekModulesHost
    /// </summary>
    public interface IPeekModulesHost
    {
        bool IsDocked(ModuleType type);
        void DockModule(ModuleType moduleType);
        void ShowPeek(ModuleType moduleType);
    }
    #endregion

}
