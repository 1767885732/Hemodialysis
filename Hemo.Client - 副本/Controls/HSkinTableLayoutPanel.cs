/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述:自定义TableLayoutPanel控件，防止之前的会出现控件重绘闪烁
// 创建时间：2016-08-21
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hemo.Client.Controls
{
    public  class HSkinTableLayoutPanel : TableLayoutPanel
    {
        public HSkinTableLayoutPanel()
        {
            // //设置tablelayoutpanel控件的DoubleBuffered 属性为true，这样可以减少或消除由于不断重绘所显示图面的某些部分而导致的闪烁
            this.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(this, true, null);  
        }
        //private  Color borderColor = Color.Black;  
  
  
        //public  Color BorderColor  
        //{  
        //    get  {  return  borderColor; } 
        //    set  {borderColor = value; }
        //}  
     
        //protected override void OnCellPaint(TableLayoutCellPaintEventArgs e) 
        //{   
        //    //绘制边框  
        //    this.OnCellPaint(e);  
        //    Pen pp =  new  Pen(BorderColor);  
        //    e.Graphics.DrawRectangle(pp,e.CellBounds.X,e.CellBounds.Y,e.CellBounds.X +  this .Width - 1,e.CellBounds.Y +  this .Height - 1);  
          
        //}
       
    }
}
