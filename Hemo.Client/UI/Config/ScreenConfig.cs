/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：大屏上传文档窗体
// 创建时间：2016-06-06
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Config;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using System.IO;

namespace Hemo.Client.UI.Config {
    public partial class ScreenConfig :HemoBaseFrm{
        #region 构造函数

        public ScreenConfig() 
        {
            this.InitializeComponent();            
        }

        #endregion

        #region 事件

        private void btnSave_Click(object sender, EventArgs e)
        {
            File.Copy(this.txtFileDictionary.Text.Trim(), this.txtDirFolder.Text.Trim(), true);
            XtraMessageBox.Show("上传成功！");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "请选择要要更新的文件";
                ofd.Filter = "所有文件(*.*)|*.*";
                ofd.InitialDirectory = "E:\\";//注意这里写路径时要用c:\\而不是c:\
                ofd.RestoreDirectory = true;
                ofd.FilterIndex = 1;
                this.txtFileDictionary.Enabled = false;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.txtFileDictionary.Text = ofd.FileName;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
