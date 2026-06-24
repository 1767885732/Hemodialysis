/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:增加窗体控件值的方法
 * 创建标识:吕志强-2017年2月3日
 * 
 * 修改时间:2017年6月21日
 * 修改人:贺建操
 * 修改描述:修复系统响应速度慢的问题
 * 
 * 修改时间:2017年7月23日
 * 修改人:吕志强
 * 修改描述:修改对外公开的方法
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hemo.Client.Modules
{
    public partial class NowDialysis : DevExpress.XtraEditors.XtraUserControl
    {
        public NowDialysis()
        {
            InitializeComponent();
        }

        //private void tabbedView1_QueryControl(object sender, DevExpress.XtraBars.Docking2010.Views.QueryControlEventArgs e)
        //{
        //    e.Control = new XtraUserControl2();
        //}
    }
}
