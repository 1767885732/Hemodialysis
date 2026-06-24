/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:WPF基础类
 * 创建标识:顾伟伟-2013年5月11日
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;


namespace Hemo.Utilities
{
    public class WPF_DocumentBase : UserControl
    {
        /// <summary>
        /// 透析参数剩余显示行数
        /// </summary>
        public int currentParamNoShowInt { get; set; }
        /// <summary>
        /// 当前参数最多行数
        /// </summary>
        public int paramRowNum { get; set; }

        public int removeRowNum { get; set; }
        public const int WordPixel = 408;
        /// <summary>
        /// 下一个文档的XAML 对象
        /// </summary>
        public WPF_DocumentBase NextPage
        {
            get;
            set;
        }

        /// <summary>
        /// 是否显示治疗单grid信息
        /// </summary>
        /// <param name="pValue"></param>
        public virtual void IsShowGrid(bool value)
        {
        }

        /// <summary>
        /// 设置Image、TextBox的是否可见
        /// </summary>
        /// <param name="imgVisible"></param>
        /// <param name="txtVisible"></param>
        public virtual void SetImageAndTextBoxVisible(Visibility imgVisible, Visibility txtVisible)
        {
        }

        /// <summary>
        /// 获取字符横向所占像素数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int GetPixelb(string str, System.Drawing.Graphics currentGps)
        {
            System.Drawing.Font font;
            System.Windows.Forms.PictureBox pb = new System.Windows.Forms.PictureBox();
            System.Drawing.Graphics g = currentGps == null ? pb.CreateGraphics() : currentGps;// pb.CreateGraphics();
            g.PageUnit = System.Drawing.GraphicsUnit.Pixel;
            int len;
            if (Encoding.Default.GetByteCount(str) == 2)
            {
                font = new System.Drawing.Font("SimSun", 12, System.Drawing.GraphicsUnit.Pixel);
                len = (int)(Math.Round(g.MeasureString(str, font).Width) * 0.75);
            }
            else
            {
                font = new System.Drawing.Font("Arial", 12, System.Drawing.GraphicsUnit.Pixel);
                len = (int)(Math.Round(g.MeasureString(str, font).Width) * 0.7);
            }
            font.Dispose();
            font = null;
            g.Dispose();
            g = null;
            pb.Dispose();
            pb = null;
            return len;
        }
    }
}
