/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：容器有于显示拖动条
// 创建时间：2015-08-21
// 创建者：吕志强
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hemo.Client.Controls
{
    public partial class DocumentContainer : DevExpress.XtraEditors.XtraScrollableControl
    {
        #region 变量
        
        private int widthValue = 816;
        private int heightValue = 1100;
        List<Control> componentList = new List<Control>();

        #endregion

        #region 事件

        public DocumentContainer()
        {


        }
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            componentList.Add(e.Control);

        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateView();
        }
        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            componentList.Remove(e.Control);
            UpdateView();
        }

        #endregion

        #region 方法

        private void UpdateView()
        {
            var xL = 3;
            if (this.Width > widthValue)
            {
                xL = (int)((this.Width - widthValue) / 2);
            }
            var yl = 3 - this.VerticalScroll.Value;
            foreach (var ctl in componentList)
            {
                ctl.Width = widthValue;
                ctl.Height = heightValue;
                ctl.BackColor = Color.White;
                ctl.Location = new Point(xL, yl);
                yl = yl + heightValue+10;
            }


        }

        #endregion
    }
}
