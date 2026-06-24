/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:用户控件类
 * 创建标识:刘超-2013年7月24日
 * 
 * 修改时间:2013年11月1日
 * 修改人:顾伟伟
 * 修改描述:修改方法
 * 
 * 修改时间:2014年2月9日
 * 修改人:吕志强
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年5月20日
 * 修改人:刘超
 * 修改描述:新增方法SQL
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Utilities;
using System.IO;
using Hemo.Model;
using System.Net;
using System.Configuration;
using System.Security.AccessControl;
using System.Management;
using Hemo.Service;
using Hemo.IService.Config;
using System.Linq;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class UploadPatientFile : DevExpress.XtraEditors.XtraForm
    {
        #region 类变量

        private IConfig configService = ServiceManager.Instance.ConfigService;

        private PatientModel.MED_PATIENTSRow patient = null;

        private ConfigModel.MED_COMMON_ITEMLISTDataTable dtServer = null;

        private int status = -1;

        #endregion

        #region 属性

        /// <summary>
        /// 患者
        /// </summary>
        public PatientModel.MED_PATIENTSRow Patient
        {
            get { return patient; }
            set { patient = value; }
        }

        #endregion

        #region 构造函数

        public UploadPatientFile()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UploadPatientFile_Load(object sender, EventArgs e)
        {
            BindLookUpEdit();
            LoadPatientInfo();
            dtServer = configService.GetConfigList(string.Empty, string.Empty, "患者资料上传", "1");
        }

        /// <summary>
        /// 选择文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "选择文件";
            dialog.Filter = "图片文件(*.bmp;*.jpg;*.jpeg;*.gif;*.png)|*.bmp;*.jpg;*.jpeg;*.gif;*.png|所有文件(*.*)|*.*";
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtFile.Text = dialog.FileName;
                string ext = dialog.FileName.Substring(dialog.FileName.LastIndexOf(".") + 1);
                if (ext.ToUpper().Equals("BMP") || ext.ToUpper().Equals("JPG") || ext.ToUpper().Equals("JPEG") || ext.ToUpper().Equals("GIF") || ext.ToUpper().Equals("PNG"))
                {
                    using (FileStream stream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        Image image = Image.FromStream(stream);
                        this.picView.Image = image;
                    }
                }
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (this.lupBook.EditValue == null)
            {
                XtraMessageBox.Show("请选择同意书类型！", "上传患者资料");
                return;
            }

            if (string.IsNullOrEmpty(this.txtFile.Text))
            {
                XtraMessageBox.Show("请选择要上传的资料！", "上传患者资料");
                return;
            }
            UploadFile();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UploadPatientFile_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载患者信息
        /// </summary>
        private void LoadPatientInfo()
        {
            this.ctlUserLongInfo.HEMODIALYSIS_ID = patient.HEMODIALYSIS_ID;
            this.ctlUserLongInfo.LoadPatientInfo();
        }

        /// <summary>
        /// 同意书类型下拉选项绑定
        /// </summary>
        private void BindLookUpEdit()
        {
            DataTable dtBook = new DataTable();
            dtBook.Columns.Add(new DataColumn("ITEM_VALUE"));
            dtBook.Columns.Add(new DataColumn("ITEM_NAME"));

            DataRow row = dtBook.NewRow();
            row["ITEM_VALUE"] = "1";
            row["ITEM_NAME"] = "连续性肾脏替代治疗知情同意书";
            dtBook.Rows.Add(row);

            row = dtBook.NewRow();
            row["ITEM_VALUE"] = "2";
            row["ITEM_NAME"] = "血液净化治疗知情同意书";
            dtBook.Rows.Add(row);

            row = dtBook.NewRow();
            row["ITEM_VALUE"] = "3";
            row["ITEM_NAME"] = "授权委托书";
            dtBook.Rows.Add(row);

            row = dtBook.NewRow();
            row["ITEM_VALUE"] = "4";
            row["ITEM_NAME"] = "血液灌流知情同意书";
            dtBook.Rows.Add(row);

            row = dtBook.NewRow();
            row["ITEM_VALUE"] = "5";
            row["ITEM_NAME"] = "动静脉内瘘血管吻合术同意书";
            dtBook.Rows.Add(row);

            row = dtBook.NewRow();
            row["ITEM_VALUE"] = "6";
            row["ITEM_NAME"] = "中心静脉置管术知情同意书";
            dtBook.Rows.Add(row);

            row = dtBook.NewRow();
            row["ITEM_VALUE"] = "7";
            row["ITEM_NAME"] = "枸橼酸抗凝同意书";
            dtBook.Rows.Add(row);

            row = dtBook.NewRow();
            row["ITEM_VALUE"] = "8";
            row["ITEM_NAME"] = "急诊施行血液灌流同意书";
            dtBook.Rows.Add(row);

            row = dtBook.NewRow();
            row["ITEM_VALUE"] = "9";
            row["ITEM_NAME"] = "抗生素皮试知情同意书";
            dtBook.Rows.Add(row);

            row = dtBook.NewRow();
            row["ITEM_VALUE"] = "10";
            row["ITEM_NAME"] = "血透同意书";
            dtBook.Rows.Add(row);

            row = dtBook.NewRow();
            row["ITEM_VALUE"] = "11";
            row["ITEM_NAME"] = "术后告知";
            dtBook.Rows.Add(row);

            row = dtBook.NewRow();
            row["ITEM_VALUE"] = "12";
            row["ITEM_NAME"] = "血透病人告知书";
            dtBook.Rows.Add(row);

            row = dtBook.NewRow();
            row["ITEM_VALUE"] = "13";
            row["ITEM_NAME"] = "穿刺风险告之书";
            dtBook.Rows.Add(row);

            row = dtBook.NewRow();
            row["ITEM_VALUE"] = "14";
            row["ITEM_NAME"] = "自购药品使用知情同意书";
            dtBook.Rows.Add(row);

            row = dtBook.NewRow();
            row["ITEM_VALUE"] = "15";
            row["ITEM_NAME"] = "无肝素血液透析风险知情同意书";
            dtBook.Rows.Add(row);

            row = dtBook.NewRow();
            row["ITEM_VALUE"] = "16";
            row["ITEM_NAME"] = "危重病人急诊行床边血液净化治疗同意书";
            dtBook.Rows.Add(row);

            this.lupBook.Properties.DataSource = dtBook;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        private void UploadFile()
        {
            if (dtServer != null && dtServer.Rows.Count > 0)
            {
                string serverIP = dtServer.First(item => item.ITEM_NAME.Equals("服务端IP")).ITEM_VALUE;
                string account = dtServer.First(item => item.ITEM_NAME.Equals("服务端帐户")).ITEM_VALUE;
                string serverFile = "file://" + serverIP + "/" + "UploadFile" + "/" + patient.HEMODIALYSIS_ID + "/" + this.lupBook.Text + "/" + Path.GetFileName(this.txtFile.Text.Trim());
                string rootPath = "\\\\" + serverIP + "\\" + "UploadFile";
                string fullPath = rootPath + "\\" + patient.HEMODIALYSIS_ID + "\\" + this.lupBook.Text + "\\";

                if (!Directory.Exists(fullPath))
                {
                    if (status != (int)ERROR_ID.ERROR_SUCCESS)
                    {
                        status = NetworkConnection.Connect(rootPath, account.Substring(0, account.IndexOf("/")), account.Substring(account.IndexOf("/") + 1));
                    }
                    if (status == (int)ERROR_ID.ERROR_SUCCESS)
                    {
                        Directory.CreateDirectory(fullPath);
                    }
                }

                WebClient client = new WebClient();
                client.Credentials = new NetworkCredential(account.Substring(0, account.IndexOf("/")), account.Substring(account.IndexOf("/") + 1));

                try
                {
                    client.UploadFileCompleted += new UploadFileCompletedEventHandler(UploadFileCompleted);
                    client.UploadFileAsync(new Uri(serverFile), "PUT", this.txtFile.Text.Trim());

                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("上传失败：" + ex.Message, "上传患者资料");
                }
            }
        }

        private void UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            if (e.Result != null && e.Error == null)
            {
                XtraMessageBox.Show("上传成功！", "上传患者资料");
            }
            else
            {
                XtraMessageBox.Show("上传失败！" + e.Error.Message, "上传患者资料");
            }
        }

        #endregion
    }
}