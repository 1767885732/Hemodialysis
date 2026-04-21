/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:ViewModel基类
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DevExpress.DevAV
{
    /// <summary>
    /// DocumentContentViewModelBase
    /// </summary>
    public abstract class DocumentContentViewModelBase : IDocumentContent
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        protected DocumentContentViewModelBase() { }
        #region 方法

        void IDocumentContent.OnClose(CancelEventArgs e) { }
        void IDocumentContent.OnDestroy() { }
        #endregion
        #region 属性
        
        IDocumentOwner IDocumentContent.DocumentOwner { get; set; }
        object IDocumentContent.Title { get { return GetTitle(); } }

        [Command]
        public void Close()
        {
            ((IDocumentContent)this).DocumentOwner.Close(this);
        }

        protected virtual string GetTitle()
        {
            return null;
        }
        #endregion

    }
}
