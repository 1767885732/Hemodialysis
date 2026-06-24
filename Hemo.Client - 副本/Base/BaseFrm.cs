/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:  普通窗体基类
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hemo.Client.Base
{
    public partial class BaseFrm : DevExpress.XtraEditors.XtraForm
    {
        public BaseFrm()
        {
            InitializeComponent();
        }

    }
}