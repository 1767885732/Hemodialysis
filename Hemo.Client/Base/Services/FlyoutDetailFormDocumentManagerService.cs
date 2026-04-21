
/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: Flyout 控件管理
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Mvvm;
using DevExpress.XtraBars.Docking2010.Customization;
using Hemo.Client.Base.XtraBaseInfo;
using Hemo.WinForm;



namespace Hemo.Client.Base.Services
{
    /// <summary>
    /// FlyoutDetailFormDocumentManagerService
    /// </summary>
    class FlyoutDetailFormDocumentManagerService : IDocumentManagerService, IDocumentOwner
    {
        #region 事件
        
        IList<IDocument> documents;
        private ModuleType viewModuleType;
        private ArrayList viewModultList = new ArrayList();
        #endregion
        #region 方法
        
        /// <summary>
        /// registelService
        /// </summary>
        /// <param name="viewModuleType"></param>
        public FlyoutDetailFormDocumentManagerService(ModuleType[] viewModuleType)
        {
            this.viewModuleType = viewModuleType[0];
            foreach (ModuleType item in viewModuleType)
            {
                viewModultList.Add(item);
            }
            this.documents = new List<IDocument>();
        }
        public IEnumerable<IDocument> Documents
        {
            get { return documents; }
        }
        /// <summary>
        /// 创建Decument
        /// </summary>
        /// <param name="documentType"></param>
        /// <param name="viewModel"></param>
        /// <param name="parameter"></param>
        /// <param name="parentViewModel"></param>
        /// <returns></returns>
        IDocument IDocumentManagerService.CreateDocument(string documentType, object viewModel, object parameter, object parentViewModel)
        {
            var actualModuleType = GetActualViewModuleType(documentType, parentViewModel);
            var moduleLocator = ((ISupportServices)parentViewModel).ServiceContainer.GetService<Services.IModuleLocator>();
            object view = null;
            if (parameter is Guid)
                view = moduleLocator.GetModule(actualModuleType, (Guid)parameter);
            else
                view = moduleLocator.GetModule(actualModuleType);
            viewModel = EnsureViewModel(viewModel, parameter, parentViewModel, view);
            return RegisterDocument((control) => new FlyoutDocument(this, control, viewModel), view, parameter);
        }
        #region Document
        protected class FlyoutDocument : IDocument, IDocumentInfo
        {
            readonly object content;
            readonly Control view;
            readonly FlyoutDetailFormDocumentManagerService owner;
            DocumentState state = DocumentState.Hidden;
            public FlyoutDocument(FlyoutDetailFormDocumentManagerService owner, Control control, object content)
            {
                this.owner = owner;
                this.view = control;
                this.content = content;
            }
            FlyoutDialog dialog;
            void dialog_Closed(object sender, EventArgs e)
            {
                RemoveFromDocumentsList();
                dialog.Closed -= dialog_Closed;
            }
            void IDocument.Show()
            {
                if (dialog == null)
                {
                    dialog = new FlyoutDialog(Program.MainForm, view, Program.MainForm.MenuManager);
                    dialog.Closed += dialog_Closed;
                    using (dialog)
                        dialog.ShowDialog();
                    dialog = null;
                }
                else dialog.Activate();
                state = DocumentState.Visible;
            }
            void IDocument.Hide()
            {
                if (dialog != null)
                {
                    dialog.Hide();
                    state = DocumentState.Hidden;
                }
            }
            void IDocument.Close(bool force)
            {
                if (dialog != null)
                {
                    dialog.Close();
                    state = DocumentState.Hidden;
                }
            }
            bool IDocument.DestroyOnClose
            {
                get { return true; }
                set { }
            }
            object IDocument.Id { get; set; }
            object IDocument.Title { get; set; }
            object IDocument.Content
            {
                get { return GetContent(); }
            }
            DocumentState IDocumentInfo.State { get { return state; } }
            void RemoveFromDocumentsList()
            {
                owner.documents.Remove(this);
            }
            object GetContent() { return content; }

            public string DocumentType
            {
                get { return null; }
            }
        }
        #endregion Document
        /// <summary>
        /// GetActualViewModuleType
        /// </summary>
        /// <param name="documentType"></param>
        /// <param name="parentViewModel"></param>
        /// <returns></returns>
        protected virtual ModuleType GetActualViewModuleType(string documentType, object parentViewModel)
        {
            foreach (ModuleType item in viewModultList)
            {
                if (item.ToString().Trim().Equals(documentType))
                {
                    viewModuleType = item ;
                }
            }
            
            return viewModuleType;
        }
        /// <summary>
        /// EnsureViewModel
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="parameter"></param>
        /// <param name="parentViewModel"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        protected object EnsureViewModel(object viewModel, object parameter, object parentViewModel, object view)
        {
            if (viewModel == null)
            {
                if (view is ISupportViewModel)
                    viewModel = ((ISupportViewModel)view).ViewModel;
                ViewModelHelper.EnsureModuleViewModel(view, parentViewModel, parameter);
            }
            IDocumentContent documentContent = viewModel as IDocumentContent;
            if (documentContent != null)
                documentContent.DocumentOwner = this;
            return viewModel;
        }
        /// <summary>
        /// RegisterDocument
        /// </summary>
        /// <param name="create"></param>
        /// <param name="view"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        protected IDocument RegisterDocument(Func<Control, IDocument> create, object view, object id)
        {
            IDocument document = create((Control)view);
            document.Id = id;
            documents.Add(document);
            return document;
        }
        /// <summary>
        /// ActiveDocument
        /// </summary>
        IDocument IDocumentManagerService.ActiveDocument
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        event ActiveDocumentChangedEventHandler IDocumentManagerService.ActiveDocumentChanged
        {
            add { }
            remove { }
        }
        void IDocumentOwner.Close(IDocumentContent documentContent, bool force)
        {
            var document = documents.FirstOrDefault((d) => object.Equals(d.Content, documentContent));
            if (document != null)
                document.Close(force);
        }
        #endregion

    }
}
