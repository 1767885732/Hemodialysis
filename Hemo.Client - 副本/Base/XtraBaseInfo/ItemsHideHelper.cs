/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: Button 集合中控制显示与隐藏
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace Hemo.Client.Base.XtraBaseInfo
{
    /// <summary>
    /// ItemsHideHelper
    /// </summary>
    public static class ItemsHideHelper
    {
        #region methond
        
        public static void Hide(ICollection baseItemCollection, SimpleLabelItem button)
        {
            HideCore(baseItemCollection, button, false);
        }
        public static void HideCore(ICollection baseItemCollection, SimpleLabelItem button, bool conversely)
        {
            foreach (BaseLayoutItem bli in baseItemCollection)
            {
                bli.Visibility = LayoutVisibility.Never;
            }

            //if (conversely)
            //{
            //    button.Image = Image.FromStream(Assembly.GetEntryAssembly().GetManifestResourceStream("Hemo.Client.Resources.ArrowLeft.png"));
            //}
            //else
            //{
            //    button.Image = Image.FromStream(Assembly.GetEntryAssembly().GetManifestResourceStream("Hemo.Client.Resources.ArrowRight.png"));
            //}

            String projectName = Assembly.GetExecutingAssembly().GetName().Name.ToString();

            if (conversely)
            {
                button.Image = Image.FromStream(Assembly.GetEntryAssembly().GetManifestResourceStream(projectName + ".Resources.arrowleft.png"));
            }
            else
            {
                button.Image = Image.FromStream(Assembly.GetEntryAssembly().GetManifestResourceStream(projectName + ".Resources.arrowright.png"));
            }
        }

        public static void Expand(ICollection baseItemCollection, SimpleLabelItem button)
        {
            ExpandCore(baseItemCollection, button, false);
        }
        public static void ExpandCore(ICollection baseItemCollection, SimpleLabelItem button, bool conversely)
        {
            foreach (BaseLayoutItem bli in baseItemCollection)
            {
                bli.Visibility = LayoutVisibility.Always;
            }
            if (conversely)
            {
                button.Image = Image.FromStream(Assembly.GetEntryAssembly().GetManifestResourceStream("Hemo.Client.Resources.arrowright.png"));
            }
            else
            {
                button.Image = Image.FromStream(Assembly.GetEntryAssembly().GetManifestResourceStream("Hemo.Client.Resources.arrowleft.png"));
            }
        }
        #endregion

    }
}
