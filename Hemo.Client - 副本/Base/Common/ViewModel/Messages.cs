/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:SelectedItemSynchronizationMessage基类
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/
using System;

namespace Hemo.Client.Base.Common.ViewModel
{
    /// <summary>
    /// SelectedItemSynchronizationMessage
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class SelectedItemSynchronizationMessage<TEntity> where TEntity : class
    {
        public SelectedItemSynchronizationMessage(TEntity entity)
        {
            Entity = entity;
        }
        /// <summary>
        /// Entity
        /// </summary>
        public TEntity Entity { get; private set; }
    }
}
