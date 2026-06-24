
/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: ViewModel 管理，获取资源文件
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/
using System;
using DevExpress.Mvvm;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Hemo.Client.Base.XtraBaseInfo
{
    #region ButtonInfo
    
    /// <summary>
    /// ButtonInfo
    /// </summary>
    public class ButtonInfo
    {
        public string Text { get; set; }
        public string Name { get; set; }
        public Image Image { get; set; }
        public Control PopupMenuContent { get; set; }

        public EventHandler mouseEventHandler { get; set; }
        public Type Type { get; set; }
    }

    #endregion
    #region ImageHelper
    
    /// <summary>
    /// ImageHelper
    /// </summary>
    public static class ImageHelper
    {
        public static Image GetImageFromToolbarResource(string name)
        {
            var imageStream = Assembly.GetEntryAssembly().GetManifestResourceStream("Hemo.Client.Resources.Toolbar." + name + ".png");
            return Image.FromStream(imageStream);
        }
    }
    #endregion
    #region ViewModelHelper
    
    /// <summary>
    /// ViewModelHelper
    /// </summary>
    public static class ViewModelHelper {
        public static TViewModel GetParentViewModel<TViewModel>(object viewModel) {
            var parentViewModelSupport = viewModel as ISupportParentViewModel;
            if (parentViewModelSupport != null) {
                return (TViewModel)parentViewModelSupport.ParentViewModel;
            }
            return default(TViewModel);
        }
        public static void EnsureModuleViewModel(object module, object parentViewModel, object parameter = null) {
            var vm = module as ISupportViewModel;
            if (vm != null) {
                object oldParentViewModel = null;
                var parentViewModelSupport = vm.ViewModel as ISupportParentViewModel;
                if (parentViewModelSupport != null) {
                    oldParentViewModel = parentViewModelSupport.ParentViewModel;
                }
                EnsureViewModel(vm.ViewModel, parentViewModel, parameter);
                if (oldParentViewModel != parentViewModel) {
                    vm.ParentViewModelAttached();
                }
            }
        }


        /// <summary>
        /// EnsureViewModel
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="parentViewModel"></param>
        /// <param name="parameter"></param>
        public static void EnsureViewModel(object viewModel, object parentViewModel, object parameter = null) {
            var parentViewModelSupport = viewModel as ISupportParentViewModel;
            if (parentViewModelSupport != null) {
                parentViewModelSupport.ParentViewModel = parentViewModel;
            }
            var parameterSupport = viewModel as ISupportParameter;
            if (parameterSupport != null && parameter != null) {
                parameterSupport.Parameter = parameter;
            }
        }
    }
    #endregion

}
