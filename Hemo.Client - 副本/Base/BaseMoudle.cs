/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:  主窗体基类
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.Base.XtraBaseInfo;
using System.Linq.Expressions;

namespace Hemo.Client.Base
{
    public partial class BaseModule : XtraUserControl, ISupportViewModel
    {
        #region 变量
        private object viewModelCore;
        //public BaseModuleControl()
        //{
        //    InitializeComponent();
        //}
        protected BaseModule(Func<object> viewModelnjector)
        {
            BindingContext = new System.Windows.Forms.BindingContext();
            viewModelCore = viewModelnjector();
            InitServices();
        }

        int updateQueued = 0;


        #endregion

        #region 方法

        void InitServices()
        {
            var serviceContainer = GetServiceContainer();
            if (serviceContainer != null)
                OnInitServices(serviceContainer);
        }
        protected static TViewModel CreateViewModel<TViewModel>()
            where TViewModel : class, new()
        {
            return DevExpress.Mvvm.POCO.ViewModelSource.Create<TViewModel>();
        }
        protected static TViewModel CreateViewModel<TViewModel>(Expression<Func<TViewModel>> constructorExpression)
            where TViewModel : class, new()
        {
            return DevExpress.Mvvm.POCO.ViewModelSource.Create<TViewModel>(constructorExpression);
        }


        #endregion

        #region 事件
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (viewModelCore != null)
                {
                    updateQueued = -1;
                    ReleaseModule();
                    OnDisposing();
                }
                viewModelCore = null;
            }
            base.Dispose(disposing);
        }
        void ReleaseModule()
        {
            var locator = GetService<Services.IModuleLocator>();
            if (locator != null)
            {
                locator.ReleaseModule(this);
            }
        }
        protected void ReleaseModuleReports<TEnum>()
            where TEnum : struct
        {
            var locator = GetService<Services.IReportLocator>();
            if (locator != null)
            {
                foreach (TEnum key in Enum.GetValues(typeof(TEnum)))
                {
                    locator.ReleaseReport(key);
                }
            }
        }
        protected virtual void OnInitServices(DevExpress.Mvvm.IServiceContainer serviceContainer) { }
        protected virtual void OnDisposing() { }
        protected TViewModel GetViewModel<TViewModel>()
        {
            return (TViewModel)viewModelCore;
        }
        protected TViewModel GetParentViewModel<TViewModel>() where TViewModel : class
        {
            object temp = ((DevExpress.Mvvm.ISupportParentViewModel)viewModelCore).ParentViewModel;
            return temp is TViewModel ? (TViewModel)temp : null;
        }
        protected TViewModel TryGetModuleViewModel<TViewModel>(ref TViewModel moduleViewModel, ModuleType moduleType) where TViewModel : class
        {
            if (moduleViewModel == null)
            {
                var module = GetService<Services.IModuleLocator>().GetModule(moduleType);
                if (module != null)
                {
                    object mainViewModel = ((DevExpress.Mvvm.ISupportParentViewModel)viewModelCore).ParentViewModel;
                    moduleViewModel = ((ISupportViewModel)module).ViewModel as TViewModel;
                    ViewModelHelper.EnsureViewModel(moduleViewModel, mainViewModel);
                }
            }
            return moduleViewModel;
        }
        protected TService GetService<TService>() where TService : class
        {
            var serviceContainer = GetServiceContainer();
            return (serviceContainer != null) ? serviceContainer.GetService<TService>() : null;
        }
        DevExpress.Mvvm.IServiceContainer GetServiceContainer()
        {
            if (!(viewModelCore is DevExpress.Mvvm.ISupportServices))
                return null;
            return ((DevExpress.Mvvm.ISupportServices)viewModelCore).ServiceContainer;
        }
        object ISupportViewModel.ViewModel
        {
            get { return viewModelCore; }
        }
        void ISupportViewModel.ParentViewModelAttached()
        {
            OnParentViewModelAttached();
        }
        protected virtual void OnParentViewModelAttached() { }
        public BottomPanelBase BottomPanel
        {
            get
            {
                if (Parent == null || Parent.Parent == null)
                {
                    return null;
                }
                var mainForm = Parent.Parent as MainFrm;
                if (mainForm != null)
                {
                    return mainForm.bottomPanelBase1;
                }
                mainForm = Parent.Parent.Parent as MainFrm;
                if (mainForm != null)
                {
                    return mainForm.bottomPanelBase1;
                }
                return null;
            }
        }
  
 
        protected internal virtual void OnTransitionCompleted() { }
        #endregion


    }
}
