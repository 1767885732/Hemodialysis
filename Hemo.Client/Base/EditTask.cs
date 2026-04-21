/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: 测试控件。作废
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/
using System;
using System.Linq;
using DevExpress.DevAV;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using Hemo.Client.Base.Services;
using Hemo.Client.Base.XtraBaseInfo;
using Hemo.Client.Base;

namespace Hemo.Client.Modules
{
    public partial class EditTask : BaseMoudleControl
    {
        #region 构造函数
        
        public EditTask()
        {
            InitializeComponent();
            base.viewModelCore = CreateViewModel<TaskViewModel>();
            base.InitServices();
        }
        #endregion
        #region 方法
        
        void BindCommands()
        {
            //saveSimpleButton.BindCommand(() => ViewModel.SaveAndClose(), ViewModel);
            cancelSimpleButton.BindCommand(() => ViewModel.Close(), ViewModel);
        }
        protected override void OnInitServices(DevExpress.Mvvm.IServiceContainer serviceContainer)
        {
            base.OnInitServices(serviceContainer);
            serviceContainer.RegisterService(new FlyoutDetailFormDocumentManagerService(new ModuleType[] {ModuleType.EditTask}));
        }

        public TaskViewModel ViewModel
        {
            get { return GetViewModel<TaskViewModel>(); }
        }
        #endregion
        #region 事件
        private void cancelSimpleButton_Click(object sender, EventArgs e)
        {
            ViewModel.Close();
        }
      

        #endregion

    }
}
