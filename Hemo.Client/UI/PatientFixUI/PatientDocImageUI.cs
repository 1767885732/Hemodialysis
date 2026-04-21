/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者电子扫描件上传类
// 创建时间：2016-8-18
// 创建者：刘超
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraEditors;
using Hemo.Client.Modules;
using Hemo.IService;
using Hemo.Service;
using Hemo.Model;
using Hemo.Client.UI.Machine;

namespace Hemo.Client.UI.PatientFixUI
{
    public partial class PatientDocImageUI : ViewBase
    {
        #region 变量和属性
        /// <summary>
        /// 病人透析号
        /// </summary>
        private string _hemoId = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        private IPatient _patientService = ServiceManager.Instance.PatientService;

        /// <summary>
        /// 病人文书扫描数据
        /// </summary>
        private DataTable _docImageData = null;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hemoId"></param>
        public PatientDocImageUI(PatientModel.MED_PATIENTSRow patientRow)
        {
            InitializeComponent();

            this._hemoId = patientRow.HEMODIALYSIS_ID;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientDocImageUI_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                this.LoadDocImage();
                this.LoadDocTree();
            }
        }

        /// <summary>
        /// 选择文件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dxSimpleButton1_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //文件完整路径名
                this.txtFileName.Text = this.openFileDialog1.FileName;
                this.txtDocName.Text = this.txtFileName.Text.Substring(this.txtFileName.Text.LastIndexOf('\\')+1);
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dxSimpleButton2_Click(object sender, EventArgs e)
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

                this._patientService.InsertDocImage(this._hemoId, this.txtDocName.Text.Trim(), docImage);
                XtraMessageBox.Show("文件上传成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.LoadDocImage();
                this.LoadDocTree();
            }
            catch (Exception ex)
            {
                //log
                XtraMessageBox.Show("文件上传失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 双击知情同意书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlDocments_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode node = this.tlDocments.GetNodeAt(e.X, e.Y);

            if (node == null || node.Tag == null)
            {
                return;
            }

            try
            {
                byte[] docImageBytes = node.Tag as byte[];
                Image docImage = null;
                using (Stream imageStream = new MemoryStream(docImageBytes))
                {
                    docImage = new Bitmap(imageStream);
                }

                this.pictureBox1.Image = docImage;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("加载图像失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Parent.Parent.Name.Contains("PatientDocImageMgr"))
                {
                    ((PatientDocImageMgr)this.Parent.Parent).CloseCurrentDocument();

                }
            }
            catch { }

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
                this._docImageData = this._patientService.GetDocImage(this._hemoId);
            }
            catch (Exception ex)
            { 
                //log
            }
        }

        /// <summary>
        /// 加载文书树
        /// </summary>
        private void LoadDocTree()
        {
            if (this._docImageData == null || this._docImageData.Rows.Count == 0)
            {
                return;
            }

            this.tlDocments.Nodes[0].Nodes.Clear();

            foreach (DataRow docImage in this._docImageData.Rows)
            {
                TreeNode node = new TreeNode(docImage["DOC_NAME"].ToString());
                node.Tag = docImage["DOC_IMAGE"];
                this.tlDocments.Nodes[0].Nodes.Add(node);
            }

            this.tlDocments.ExpandAll();
        }
        #endregion
    }
}
