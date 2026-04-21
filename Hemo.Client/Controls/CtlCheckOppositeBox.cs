/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司股份有限公司
// 文件名：CtlCheckOppositeBox.cs
// 文件功能描述：自定义控件 
// 创建标识：刘超 2013-07-22
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hemo.Client.Controls
{
    public partial class CtlCheckOppositeBox : DevExpress.XtraEditors.XtraUserControl
    {
        #region 变量

        private string checkId = string.Empty;
        public string CheckId
        {
            get { return checkId; }
            set { checkId = value; }
        }
        
    	#endregion

        #region 构造函数

        public CtlCheckOppositeBox()
        {
            InitializeComponent();
        }
        
	    #endregion

        #region 事件
        /// <summary>
        /// 第四个控件选择时其它不可以选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked == true)
            {
                checkEdit2.Checked = false;
                checkEdit3.Checked = false;
                checkEdit4.Checked = false;
                CheckId = "4";
            }
        }
        /// <summary>
        /// 第三个控件选择时其它不可以选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit2.Checked == true)
            {
                checkEdit1.Checked = false;
                checkEdit3.Checked = false;
                checkEdit4.Checked = false;
                CheckId = "3";
            }
        }
        /// <summary>
        /// 第二个控件选择时其它不可以选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkEdit3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit3.Checked == true)
            {
                checkEdit1.Checked = false;
                checkEdit2.Checked = false;
                checkEdit4.Checked = false;
                CheckId = "2";
            }
        }
        /// <summary>
        /// 第一个控件选择时其它不可以选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkEdit4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit4.Checked == true)
            {
                checkEdit1.Checked = false;
                checkEdit2.Checked = false;
                checkEdit3.Checked = false;
                CheckId = "1";
            }
        }
        
    	#endregion

        #region 方法
        /// <summary>
        /// 全择
        /// </summary>
        /// <returns></returns>
        public bool checkIsFill()
        {
            if (checkEdit1.Checked == false && checkEdit2.Checked == false && checkEdit3.Checked == false && checkEdit4.Checked == false)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 只选择一个
        /// </summary>
        public void checkChoose()
        {
            if (CheckId == "1")
            {
                checkEdit1.Checked = true;
            }
            else if (CheckId == "2")
            {
                checkEdit2.Checked = true;
            }
            else if (CheckId == "3")
            {
                checkEdit3.Checked = true;
            }
            else if (CheckId == "4")
            {
                checkEdit4.Checked = true;
            }
        }
        
	    #endregion
    }
}
