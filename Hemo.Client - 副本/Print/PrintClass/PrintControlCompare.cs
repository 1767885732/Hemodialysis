/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技股份有限公司
 * 文件功能描述:打印控件对照类
 * 创建标识:刘配齐-2017年4月5日
 * ----------------------------------------------------------------*/

using System;

namespace Hemo.Client.Print.PrintClass
{
	/// <summary>
	/// PrintControlCompare 的摘要说明。
	/// 打印控件对照
	/// </summary>
	public class PrintControlCompare
    {
        #region 类变量

        /// <summary>
        /// 控件对照类
        /// </summary>
        public System.Collections.Hashtable Controls = null;

        #endregion

        #region 构造函数

        /// <summary>
		/// 默认
		/// </summary>
		public PrintControlCompare()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
            this.SetSystemControl();
		}

        #endregion

        #region 方法

        /// <summary>
		/// 设置系统控件对照
		/// </summary>
		public void SetSystemControl()
		{
			this.Controls = new System.Collections.Hashtable();
            this.Controls.Add("CheckBox", "CheckBox");
            this.Controls.Add("TextBox", "TextBox");
            this.Controls.Add("DateTimePicker", "DateTimePicker");
            this.Controls.Add("GroupBox", "GroupBox");
            this.Controls.Add("PictureBox", "PictureBox");
            this.Controls.Add("Label", "Label");
            this.Controls.Add("Panel", "Panel");
            this.Controls.Add("FpSpread", "FpSpread");
            //this.Controls.Add("FpSpread", "FpSpread");//新加一行
            this.Controls.Add("ListBox", "ListBox");
            this.Controls.Add("RichTextBox", "RichTextBox");
            this.Controls.Add("RadioButton", "RadioButton");
            //this.Controls.Add("TextBox", "TextBox");
        }

        #endregion
    }
}
