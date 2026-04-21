/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：用户控件基类
// 创建时间：2014-04-06
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hemo.Client.UI.Machine
{
    /// <summary>
    /// 界面的基类
    /// </summary>
    [ToolboxItem(false)]
    public partial class ViewBase : DevExpress.XtraEditors.XtraUserControl
    {
        #region 类变量

        public event EventHandler<EventArgs> CloseButtonClicked;

        #endregion

        #region 属性
        public string hemoId { get; set; }
        public bool HasDirty { get; set; }

        #endregion

        #region 构造函数

        public ViewBase()
        {
            InitializeComponent();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 当View在TabControl中被选中时(SelectedPageChanged)调用此方法
        /// </summary>
        public virtual void OnTabPageViewSelectedHandler()
        {

        }
        public virtual void Query(DateTime dtStar, DateTime dtEnd)
        { }
        public virtual void GetVascualToUpLoad(string baseInfo)
        { }
        public virtual void CheckAllState(bool isCheck) { }
        public virtual void LoadInfo() { }
        public virtual void SetBottomPanel() { }
        public virtual void InzationData()
        { }
        /// <summary>
        /// 关闭界面
        /// </summary>
        public void CloseView()
        {
            OnClosingEventHandler();

            if (CloseButtonClicked != null)
                CloseButtonClicked(this, EventArgs.Empty);
        }
        /// <summary>
        /// 关闭前事件
        /// </summary>
        protected virtual void OnClosingEventHandler()
        {

        }

        #endregion
    }
}
