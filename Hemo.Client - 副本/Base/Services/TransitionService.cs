/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: ITransitionService 用与不同的Docutment之前切换时的效果
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/
namespace Hemo.Client.Base.Services
{
    using Hemo.Client.Base.XtraBaseInfo;
    using System;

    #region TransitionService Class
    
    /// <summary>
    /// ITransitionService
    /// </summary>
    public interface ITransitionService {
        void StartTransition(bool effective);
        void EndTransition(bool effective);
    }
    /// <summary>
    /// TransitionService
    /// </summary>
    internal class TransitionService : ITransitionService {
        private ISupportTransitions supportTransitions;
        public TransitionService(ISupportTransitions supportTransitions) {
            this.supportTransitions = supportTransitions;
        }
        public void StartTransition(bool effective) {
            supportTransitions.StartTransition(effective);
        }
        public void EndTransition(bool effective) {
            supportTransitions.EndTransition(effective);
        }
    }
    /// <summary>
    /// TransitionServiceExtension
    /// </summary>
    public static class TransitionServiceExtension {
        public static IDisposable EnterTransition(this ITransitionService service, bool effective) {
            return new TransitionBatch(service, effective);
        }
        private class TransitionBatch : IDisposable {
            private ITransitionService service;
            bool effective;
            public TransitionBatch(ITransitionService service, bool effective) {
                this.effective = effective;
                this.service = service;
                service.StartTransition(effective);
            }
            public void Dispose() {
                service.EndTransition(effective);
            }
        }
    }
    #endregion

}
