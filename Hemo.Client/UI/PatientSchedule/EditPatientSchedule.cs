/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:排班备注维护类
 * 创建标识:贺建操-2017年3月12日
 * ----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Text.RegularExpressions;

namespace Hemo.Client.UI.PatientSchedule {
    public partial class EditPatientSchedule :HemoBaseFrm{
        #region 构造函数

        public EditPatientSchedule(string comments) {
            this.InitializeComponent();

            this.txtCOMMENTS.Text = comments;
        }

        #endregion

        #region 事件

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e) {
            if (checkInputValue()) {
                this.Tag = this.txtCOMMENTS.Text;

                this.DialogResult = DialogResult.Yes;
            }
        }

        /// <summary>
        /// 校验数据数据
        /// </summary>
        /// <returns></returns>
        private bool checkInputValue() {
            bool result = true;
            string pattern = @"^[a-zA-Z0-9]+$";//正则式子
            string param1 = null;
            if (txtCOMMENTS.Text.Length > 0) {
                Match m = Regex.Match(txtCOMMENTS.Text, pattern);
                // 匹配正则表达式，把Text跟pattern正则对比
                if (!m.Success)   // 判断输入的是不是英文和数字，不是进入
                {
                    param1 = this.txtCOMMENTS.Text;//将现在textBox的值保存下来
                    // 将光标定位到文本框的最后
                    this.txtCOMMENTS.SelectionStart = this.txtCOMMENTS.Text.Length;
                    result = false;
                    XtraMessageBox.Show("只能输入英文或数字！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else   //输入的是英文和数字 
                {
                    param1 = this.txtCOMMENTS.Text;  // 将现在textBox的值保存下来
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e) {
            this.Close();
        }

        /// <summary>
        /// txtCOMMENTS KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCOMMENTS_KeyDown(object sender, KeyEventArgs e)
        {
        }
        #endregion
    }
}
