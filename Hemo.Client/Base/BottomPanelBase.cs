/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:  窗体下边菜单的控件使用windowsUIButtonPanel
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Hemo.Client.Base.XtraBaseInfo;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using System.Reflection;
using DevExpress.Utils;

namespace Hemo.Client.Base
{
    public partial class BottomPanelBase : XtraUserControl
    {
        #region 构造函数
        
        public BottomPanelBase()
        {
            InitializeComponent();
            windowsUIButtonPanel1.QueryPeekFormContent += windowsUIButtonPanel1_QueryPeekFormContent;
            windowsUIButtonPanel1.ButtonClick += windowsUIButtonPanel1_ButtonClick;
        }
        #endregion

        #region 事件
        
        private void windowsUIButtonPanel1_ButtonClick(object sender, ButtonEventArgs e)
        {
            if (e.Button.Properties.Tag != null)
            {
                windowsUIButtonPanel1.ShowPeekForm(e.Button);
            }
        }
        private void windowsUIButtonPanel1_QueryPeekFormContent(object sender, QueryPeekFormContentEventArgs e)
        {
            if (e.Button.Properties.Tag != null && e.Button.Properties.Tag is Control)
            {
                e.Control = e.Button.Properties.Tag as Control;
            }
        }
        #endregion
        #region 方法
        
        private static Image splitterImageCore = null;
        public static Image SplitterImage
        {
            get
            {
                if (splitterImageCore == null)
                {
                    splitterImageCore = ResourceImageHelper.CreateBitmapFromResources("Hemo.Client.Resources.Separator.png", Assembly.GetExecutingAssembly());
                }
                return splitterImageCore;
            }
        }
        public void InitializeButtons(List<ButtonInfo> listButtonInfo, bool searchControlVisible)
        {
            Visible = true;
            windowsUIButtonPanel1.Buttons.Clear();
            windowsUIButtonPanel1.HidePeekForm();
            foreach (ButtonInfo buttonInfo in listButtonInfo)
            {
                WindowsUIButton button;
                if (buttonInfo.Text == null && buttonInfo.Image == null)
                {
                    button = new WindowsUIButton();
                    button.Enabled = false;
                    button.UseCaption = false;
                    button.Image = SplitterImage;
                    button.ImageLocation = DevExpress.XtraBars.Docking2010.ImageLocation.BeforeText;
                }
                else
                {
                    button = new WindowsUIButton(buttonInfo.Text, buttonInfo.Image, 0, ButtonStyle.PushButton, 0);
                    button.Appearance.BackColor = Color.White;
                    button.ImageLocation = DevExpress.XtraBars.Docking2010.ImageLocation.BeforeText;
                    button.Tag = buttonInfo.PopupMenuContent;
                    button.Click += buttonInfo.mouseEventHandler;
                }
                windowsUIButtonPanel1.Buttons.Add(button);
            }
            //if (searchControlVisible)
            //{
            //    searchLayoutControlItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //}
            //else
            //{
            //    searchLayoutControlItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //}
        }
        public void InitializeButtons(List<ButtonInfo> listButtonInfo)
        {
            InitializeButtons(listButtonInfo, true);
        }
        #endregion

    }
}
