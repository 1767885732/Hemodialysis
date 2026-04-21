/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:用户控件打印接口
 * 创建标识:贺建操-2017年1月17日
 * ----------------------------------------------------------------*/

using System;
using System.Collections;
using System.Drawing;
namespace Hemo.Client.Print.PrintClass
{
	public interface IControlPrintable
    {
        #region 属性

        /// <summary>
		/// 控件需要的大小
		/// </summary>
		Size ControlSize{get;} 
		/// <summary>
		/// 是否可以自动扩展大小
		/// </summary>
		bool IsCanExtend{get;set;}
		/// <summary>
		/// 当前控件集合
		/// </summary>
		ArrayList Components{get;set;}
		/// <summary>
		/// 横排数量
		/// </summary>
		int HorizontalNum{get;set;}
		/// <summary>
		/// 竖排数量
		/// </summary>
		int VerticalNum{get;set;}
		/// <summary>
		/// 数值
		/// </summary>
		object ControlValue{set;get;}
		/// <summary>
		/// 是否显示网格
		/// </summary>
		bool IsShowGrid{get;set;}
		/// <summary>
		/// 横排间隔大小
		/// </summary>
		int HorizontalBlankWidth{get;set;}
		/// <summary>
		/// 竖排间隔大小
		/// </summary>
		int VerticalBlankHeight{get;set;}
        /// <summary>
        /// 开始调整横排间隔大小
        /// </summary>
		int BeginHorizontalBlankWidth{get;set;}
        /// <summary>
        /// 开始调整竖排间隔大小
        /// </summary>
		int BeginVerticalBlankHeight{get;set; }

        #endregion
    }
}
