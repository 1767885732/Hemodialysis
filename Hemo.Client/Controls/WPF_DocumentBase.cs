using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;


namespace wpfDemo
{
    public class WPF_DocumentBase : UserControl
    {
        /// <summary>
        /// 下一个文档的XAML 对象
        /// </summary>
        public WPF_DocumentBase NextPage
        {
            get;
            set;
        }
     
    }
}
