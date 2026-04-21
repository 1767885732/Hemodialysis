/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:修复系统加载时数据缓存问题
 * 创建标识:顾伟伟-2016年12月29日
 * 
 * 修改时间:2017年5月16日
 * 修改人:刘超
 * 修改描述:修复系统加载时数据缓存问题
 * 
 * 修改时间:2017年6月17日
 * 修改人:贺建操
 * 修改描述:用户控件
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
    public partial class BeforeDialysis : DevExpress.XtraEditors.XtraUserControl
    {
        public BeforeDialysis()
        {
            InitializeComponent();
        }


        private void tabbedView1_QueryControl_1(object sender, DevExpress.XtraBars.Docking2010.Views.QueryControlEventArgs e)
        {
            e.Control = new XtraUserControl2();
        }
    }
}
