/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: 患者知情同意书
 * 创建标识:贺建操-2015年1月30日
 * ----------------------------------------------------------------*/
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

namespace Hemo.Client.Doc {
    /// <summary>
    /// 血管穿刺风险告之书.xaml 的交互逻辑
    /// </summary>
    public partial class 血管穿刺风险告之书 : UserControl {

        /// <summary>
        /// 同意书签字
        /// </summary>
        public byte[] BookPicture
        {
            get;
            set;
        }

        public 血管穿刺风险告之书() {
            InitializeComponent();
            lblHospital.Content = Utilities.Utility.GetHospitalName();
        }
        /// <summary>
        /// 加载 文档
        /// </summary>
        public void LoadDocumentInfo()
        {
            if (BookPicture != null && BookPicture.Length > 0)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new MemoryStream(BookPicture);
                image.EndInit();
                if (image.Width <= imgPatientSign.Width && image.Height <= imgPatientSign.Height)
                {
                    imgPatientSign.Stretch = Stretch.None;
                }
                else
                {
                    imgPatientSign.Stretch = Stretch.Uniform;
                }
                imgPatientSign.Source = image;
            }
        }
    }
}
