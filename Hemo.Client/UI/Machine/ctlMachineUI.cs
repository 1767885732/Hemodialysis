/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：设备文书上传户控件类
// 创建时间：2016-03-17
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using Hemo.Model;
using Hemo.IService.Machine;
using Hemo.Service;

namespace Hemo.Client.UI.Machine
{
    public partial class ctlMachineUI : DevExpress.XtraEditors.XtraUserControl
    {
        #region 类变量

        private IMachine machineService = ServiceManager.Instance.MachineService;

        private MachineModel.MED_EQUIPMENT_MGRDataTable dt = null;
        private MachineModel.MED_EQUIPMENT_MGRRow dr = null;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public ctlMachineUI(MachineModel.MED_EQUIPMENT_MGRDataTable table, MachineModel.MED_EQUIPMENT_MGRRow row)
        {
            InitializeComponent();
            dt = table;
            dr = row;
        }

        #endregion

        #region 事件

        private void btnChose_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtFileName.Text = this.openFileDialog1.FileName;
                this.txtDocName.Text = this.txtFileName.Text.Substring(this.txtFileName.Text.LastIndexOf('\\') + 1);
            }
        }

        private void dxSimpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtFileName.Text.Trim()))
            {
                XtraMessageBox.Show("请选择要上传的文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(this.txtDocName.Text.Trim()))
            {
                XtraMessageBox.Show("请输入文件名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!File.Exists(this.txtFileName.Text.Trim()))
            {
                XtraMessageBox.Show("要上传的文件不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                byte[] docImage = null;

                using (Stream fs = new FileStream(this.txtFileName.Text.Trim(), FileMode.Open, FileAccess.Read))
                {
                    docImage = new byte[fs.Length];
                    fs.Read(docImage, 0, docImage.Length);
                }

                if (docImage == null)
                {
                    XtraMessageBox.Show("文件上传失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                dr.PIC = docImage;
                machineService.SaveMED_EQUIPMENT_MGR(dt);
                XtraMessageBox.Show("文件上传成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.LoadDocImage();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("文件上传失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        private void ctlMachineUI_Load(object sender, EventArgs e)
        {
            LoadDocImage();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载文书扫描数据
        /// </summary>
        private void LoadDocImage()
        {
            try
            {
                Image docImage = null;
                using (Stream imageStream = new MemoryStream(dr.PIC))
                {
                    docImage = new Bitmap(imageStream);
                }
                this.pictureBox1.Image = docImage;
            }
            catch (Exception ex)
            {
                // XtraMessageBox.Show("请上传文档(仅支持图片)！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion
    }
}
