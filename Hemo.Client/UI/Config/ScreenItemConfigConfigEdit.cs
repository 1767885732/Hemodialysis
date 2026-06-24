/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：大屏配置项维护窗体
// 创建时间：2016-06-06
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Model;
using Hemo.IService;
using Hemo.Service;
using DevExpress.XtraEditors;
using Hemo.Utilities;
using System.Xml;
using Hemo.Client.Controls;

namespace Hemo.Client.UI.Config
{
    public partial class ScreenItemConfigConfigEdit : HemoBaseFrm
    {
        #region 类变量

        private ConfigModel.MED_AUTO_UPDATER_ITEMSDataTable files = new ConfigModel.MED_AUTO_UPDATER_ITEMSDataTable();

        private ConfigModel.MED_COMMON_ITEMLISTRow _current = null;

        private ConfigModel.MED_COMMON_ITEMLISTDataTable _data = null;

        private IPatient _patientService = ServiceManager.Instance.PatientService;

        #endregion

        #region 属性

        public ConfigModel.MED_COMMON_ITEMLISTRow Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        #region 构造函数

        public ScreenItemConfigConfigEdit()
        {
            InitializeComponent();
            this.busyIndicator1.labelControl1.Text = "保存中...";
        }

        #endregion

        #region 事件

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckInvalidData())
            {
                this.bindingSource1.EndEdit();
                this.bindingSource1.CurrencyManager.EndCurrentEdit();
                var row = _data[0];
                row.ITEM_TYPE = "大屏配置项";
                int result = 0;
       
                using (BackgroundWorker worker = new BackgroundWorker())
                {
                    BusyIndicatorHelp busyIndicatorHelp = new BusyIndicatorHelp();

                    busyIndicatorHelp.ShowMessage();
                    busyIndicatorHelp.SetWaitFormCaption("数据保存中....");
                    worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                    {
                        result = _patientService.SaveConfigPPTItem(_data, files);
                    };
                    worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                    {
                        if (result > 0)
                        {
                            AutoClosedMsgBox.ShowForm("保存成功。", "基础信息", 1000, MessageBoxIcon.Information);
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        }
                        else
                        {
                            AutoClosedMsgBox.ShowForm("保存失败。", "基础信息", 1000, MessageBoxIcon.Information);
                        }
                        busyIndicatorHelp.HideMessage();
                    };
                    worker.RunWorkerAsync();
                }

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void ScreenItemConfigConfigEdit_Load(object sender, EventArgs e)
        {
            InzationData();
        }

        private void btnChose_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "请选择要要更新的文件";
                ofd.Filter = "所有文件(*.*)|*.*";
                ofd.InitialDirectory = "E:\\";//注意这里写路径时要用c:\\而不是c:\
                ofd.RestoreDirectory = true;
                ofd.FilterIndex = 1;
                ofd.Multiselect = true;
                this.txtFileDictionary.Enabled = false;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (string item in ofd.FileNames)
                    {
                        string fileName = item.Substring(item.LastIndexOf("\\") + 1);

                        this.txtFileDictionary.Text += item+"\r\n";

                        var row = files.NewMED_AUTO_UPDATER_ITEMSRow();
                        row.ID = Guid.NewGuid().ToString();
                        row.CONTENT = GetByteFromFile(item);
                        row.VERSION_NO = fileName;
                        row.APP_ID = "ScreenShowTv";
                        row.UPLOAD_TIME = System.DateTime.Now;
                        row.MEMO = "用于大屏的视屏播放显示";
                        files.AddMED_AUTO_UPDATER_ITEMSRow(row);
                    }
                }
            }
        }
        private void txtITEM_VALUE_EditValueChanged(object sender, EventArgs e)
        {
            if (this.panelControl2.Visible)
            {
                if (this.txtITEM_VALUE.Text == "是")
                    this.panelControl2.Enabled = true;
                else
                    this.panelControl2.Enabled = false;
            }
        }

        #endregion

        #region 方法

        private void InzationData()
        {
            this.Enabled = false;
            _data = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    if (_current != null)
                    {
                        _data.ImportRow(_current);
                    }
                    else
                    {
                        _data = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
                    }
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.bindingSource1.DataSource = _data;
                    if (_current == null)
                    {
                        this.bindingSource1.AddNew();

                        this.txtID.Text = Guid.NewGuid().ToString();
                    }
                    else
                    {
                        if (this.txtITEM_NAME.Text == "是否上传文件")
                            this.panelControl2.Visible = true;
                        else
                            this.panelControl2.Visible = false;
                    }
                    //if (this.txtITEM_NAME.Text != "是否播放PPT")
                    //{
                    //    this.txtITEM_VALUE.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
                    //    this.txtITEM_VALUE.Properties.Mask.EditMask = "T";
                    //    this.lbTip.Text = "只能输入时间";
                    //}
                    //else
                    //{
                    //    this.txtITEM_VALUE.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                    //    this.txtITEM_VALUE.Properties.Mask.EditMask = "是|否";
                    //    this.lbTip.Text = "只能输入是或者否";

                    //}
                    this.Enabled = true;
                };
                worker.RunWorkerAsync();
            }
        }

        private bool CheckInvalidData()
        {
            this.errorProvider.ClearErrors();

            if (string.IsNullOrEmpty(this.txtITEM_VALUE.Text))
            {
                this.txtITEM_VALUE.Focus();

                this.errorProvider.SetError(this.txtITEM_VALUE, "值不能为空！");

                return false;
            }

            if (string.IsNullOrEmpty(this.txtITEM_NAME.Text))
            {
                this.txtITEM_NAME.Focus();

                this.errorProvider.SetError(this.txtITEM_NAME, "名称不能为空！");

                return false;
            }
            if (string.IsNullOrEmpty(this.txtORDER_NUMBER.Text))
            {
                this.txtORDER_NUMBER.Focus();

                this.errorProvider.SetError(this.txtORDER_NUMBER, "排序字段不能为空！");

                return false;
            }

            if (this.txtITEM_NAME.Text.Trim().Equals("是否上传文件"))
            {
                if (this.txtITEM_VALUE.Text != "是" && this.txtITEM_VALUE.Text != "否")
                {
                    this.txtITEM_VALUE.Focus();

                    this.errorProvider.SetError(this.txtITEM_VALUE, "值只能为是或者否！");

                    return false;
                }
            }


            if (panelControl2.Visible)
            {
                if (string.IsNullOrEmpty(this.txtFileDictionary.Text))
                {
                    if (XtraMessageBox.Show("未上传PPT，是否继续？", "警告", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                        return false;
                }
            }
            return true;
        }

        private byte[] GetByteFromFile(string strPath)
        {
            System.IO.FileStream srm = new System.IO.FileStream(strPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            byte[] by = new byte[srm.Length];

            srm.Read(by, 0, by.Length);

            srm.Close();
            return by;

        }

        #endregion
    }
}
