/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司股份有限公司
// 文件名：CtlBook.cs
// 文件功能描述：加载文书扫描件类
// 创建标识：刘超 2013-07-22
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Hemo.Client.Controls
{
    /// <summary>
    /// CtlBook.xaml 的交互逻辑
    /// </summary>
    public partial class CtlBook : UserControl
    {
        private byte[] data = null;

        public CtlBook(byte[] data)
        {
            this.data = data;
            InitializeComponent();
            LoadImage();
        }

        /// <summary>
        /// 加载图片
        /// </summary>
        private void LoadImage()
        {
            if (data != null && data.Length > 0)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new MemoryStream(data);
                image.EndInit();
                imgBook.Stretch = Stretch.Fill;
                imgBook.Source = image;
            }
        }
    }
}
