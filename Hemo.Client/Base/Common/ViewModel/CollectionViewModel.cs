/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:显示控件用ViewModel的方法
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataAnnotations;
using Hemo.Client.Base.Common.Utils;
using MessageBoxButton = System.Windows.MessageBoxButton;
using MessageBoxImage = System.Windows.MessageBoxImage;
using MessageBoxResult = System.Windows.MessageBoxResult;
using Hemo.Client.Base.Common;

namespace Hemo.Client.Base.Common.ViewModel
{

    public partial class CollectionViewModel : IDocumentContent
    {
        public CollectionViewModel()
        {
        }
        #region methond
        

        protected IDocumentManagerService DocumentManagerService { get { return this.GetService<IDocumentManagerService>(); } }


        public virtual void CreateNewDocument(object parameter)
        {
            IDocument document = CreateDocument(parameter);
            if (document != null)
                document.Show();
        }

        public virtual void ShowExistDocument(object parameter)
        {
            ShowDocument(parameter.ToString());

        }


        /// <summary>
        /// Closes the corresponding view.
        /// Since SingleObjectViewModelBase is a POCO view model, an instance of this class will also expose the CloseCommand property that can be used as a binding source in views.
        /// </summary>
        public void Close()
        {
            if (DocumentOwner != null)
                DocumentOwner.Close(this);
        }
        protected IDocumentOwner DocumentOwner { get; private set; }

        protected virtual void OnDestroy()
        {
            Messenger.Default.Unregister(this);
        }

        void ShowDocument(string key)
        {
            IDocument document = FindEntityDocument(key) ?? CreateDocument(key);
            if (document != null)
                document.Show();
        }

        protected virtual IDocument CreateDocument(object parameter)
        {
            if (DocumentManagerService == null) return null;
            return DocumentManagerService.CreateDocument(parameter.ToString(), parameter, this);
        }

        protected void DestroyDocument(IDocument document)
        {
            if (document != null)
                document.Close();
        }

        protected IDocument FindEntityDocument(string key)
        {
            if (DocumentManagerService == null) return null;
            foreach (IDocument document in DocumentManagerService.Documents)
            {
                var entityViewModel = document.Content as string;
                if (entityViewModel != null && object.Equals(entityViewModel, key))
                    return document;
            }
            return null;
        }
        #endregion

        #region IDocumentContent
        object IDocumentContent.Title { get { return null; } }

        void IDocumentContent.OnClose(CancelEventArgs e) { }

        void IDocumentContent.OnDestroy()
        {
            OnDestroy();
        }

        IDocumentOwner IDocumentContent.DocumentOwner
        {
            get { return DocumentOwner; }
            set { DocumentOwner = value; }
        }
        #endregion

    }
}
