/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:自定义控件
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hemo.Client.Controls.HemodialysisApply
{
    public partial class LTextBox : DevExpress.XtraEditors.TextEdit
    {
        public LTextBox()
        {
            this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;

        }
        /// <summary>
        /// 画线
        /// </summary>
        const int WM_PAINT = 0xF;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_PAINT)
            {
                using (Graphics g = this.CreateGraphics())
                {
                    using (Pen p = new Pen(Color.Black))
                    {
                        g.DrawLine(p, 0, this.Height - 1, this.Width, this.Height - 1);
                    }
                }
            }
        }
    }
}
