/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: 患者知情同意书
 * 创建标识:贺建操-2017年2月3日
 * ----------------------------------------------------------------*/
using System.Windows.Controls;

namespace Hemo.Client.Doc
{
    /// <summary>
    /// 内瘘手术安排表.xaml 的交互逻辑
    /// </summary>
    public partial class 内瘘手术安排表 : UserControl
    {
        public 内瘘手术安排表()
        {
            this.InitializeComponent();
            lblHospital.Content = Utilities.Utility.GetHospitalName();
        }
    }
}
