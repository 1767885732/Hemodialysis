/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:用户控件类
 * 创建标识:吕志强-2013年7月9日
 * 
 * 修改时间:2013年10月17日
 * 修改人:吕志强
 * 修改描述:修改方法
 * 
 * 修改时间:2014年1月25日
 * 修改人:贺建操
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年5月5日
 * 修改人:刘超
 * 修改描述:修改方法SQL
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.IService;
using Hemo.Service;
using Hemo.IService.Config;

namespace Hemo.Client.UI.Hemodialysis {
    public partial class ShowRescueRecord : DevExpress.XtraEditors.XtraForm 
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rescueRecord"></param>
        public ShowRescueRecord(string rescueRecord)
        {
            InitializeComponent();
            this.txtRescueRecord.Text = rescueRecord;
        }
    }
}