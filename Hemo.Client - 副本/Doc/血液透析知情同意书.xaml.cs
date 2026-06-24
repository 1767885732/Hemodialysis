/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: 患者知情同意书
 * 创建标识:贺建操-2015年1月14日
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

namespace Hemo.Client.Doc {
    /// <summary>
    /// 血液透析知情同意书.xaml 的交互逻辑
    /// </summary>
    public partial class 血液透析知情同意书 : UserControl {
        public 血液透析知情同意书() {
            InitializeComponent();
            label1.Content = Utilities.Utility.GetHospitalName();
        }
    }
}
