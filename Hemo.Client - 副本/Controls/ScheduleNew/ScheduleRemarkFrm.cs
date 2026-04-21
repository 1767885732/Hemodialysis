/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:大屏公告事件窗口
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Model;
using Hemo.Client.Core;
using Hemo.IService.PatientSchedule;
using Hemo.Service;
using DevExpress.XtraEditors;
using Hemo.Utilities;
using Hemo.Client.UI.Hemodialysis;
using Hemo.IService.Config;
using Hemo.IService;

namespace Hemo.Client.Controls.ScheduleNew
{
    public partial class ScheduleRemarkFrm : HemoBaseFrm
    {
        #region 变量

        public DateTime beginTime { get; set; }
        public DateTime endTime { get; set; }

        private PermissionModel.MED_SCHEDULEREMARKDataTable _scheduleRemarkDataTable;
        private PermissionModel.MED_SCHEDULEREMARKRow _current;

        public PermissionModel.MED_SCHEDULEREMARKRow Current
        {
            get { return _current; }
            set { _current = value; }
        }

        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;

        private IConfig configService = ServiceManager.Instance.ConfigService;

        private ConfigModel.MED_AUTO_UPDATER_ITEMSDataTable files = new ConfigModel.MED_AUTO_UPDATER_ITEMSDataTable();

        private ConfigModel.MED_COMMON_ITEMLISTDataTable _data = null;
        private IPatient _patientService = ServiceManager.Instance.PatientService;

        private BusyIndicatorHelp busyIndicatorHelp = new BusyIndicatorHelp();

        #endregion

        #region 构造函数
        public ScheduleRemarkFrm()
        {
            InitializeComponent();
            this.Text = "大屏公告";
            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountFrm(this);
        }


        #endregion

        #region 方法
        /// <summary>
        /// 数据初始化
        /// </summary>
        public void InzationScheduleRemarkDate()
        {

            busyIndicatorHelp.ShowMessage();
            busyIndicatorHelp.SetWaitFormCaption("数据加载中....");
            this.Enabled = false;
            _scheduleRemarkDataTable = new PermissionModel.MED_SCHEDULEREMARKDataTable();
            _data = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    if (_current != null)
                    {
                        _scheduleRemarkDataTable.ImportRow(_current);
                    }
                    else
                    {
                        _scheduleRemarkDataTable = new PermissionModel.MED_SCHEDULEREMARKDataTable();
                    }
                    _data = configService.GetConfigList(string.Empty, string.Empty, "大屏配置项", "1");
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.MED_PATIENTREMARKFrm.DataSource = _scheduleRemarkDataTable;
                    if (_current == null)
                    {
                        this.MED_PATIENTREMARKFrm.AddNew();
                        this.txtID.Text = Guid.NewGuid().ToString();
                    }
                    this.lb_title.Text = string.Format("{0}至{1}的备注信息", beginTime.ToString("yyyy-MM-dd"), endTime.ToString("yyyy-MM-dd"));
                    this.startDate.EditValue = beginTime;
                    this.endDate.EditValue = endTime;
                    this.Enabled = true;

                    var IsCanUpload = this._data.FirstOrDefault(i => i.ITEM_NAME == "是否上传文件");
                    if (IsCanUpload != null && (IsCanUpload.ITEM_VALUE.Equals("是") || IsCanUpload.ITEM_VALUE.Equals("1")))
                    {
                        this.xtraTabPage3.PageEnabled = true;
                    }
                    else
                    {
                        this.xtraTabPage3.PageEnabled = false;
                    }
                    busyIndicatorHelp.HideMessage();
                };
                worker.RunWorkerAsync();
            }

        }
        /// <summary>
        /// 病人数据保存方法  
        /// </summary>
        /// <returns></returns>
        private int SaveData()
        {
            this.MED_PATIENTREMARKFrm.EndEdit();
            this.MED_PATIENTREMARKFrm.CurrencyManager.EndCurrentEdit();

            var row = _scheduleRemarkDataTable[0];
            row.ID = Guid.NewGuid().ToString();
            row.CREATEBY = LoginUser.User.USER_ID;
            row.CREATETIME = System.DateTime.Now;
            return _patientScheduleService.SaveScheduleRemark(_scheduleRemarkDataTable);
        }

        #endregion

        #region 事件
        /// <summary>
        /// 选择上传文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckItem_Click(object sender, EventArgs e)
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

                        this.txtFileDictionary.Text += item + "\r\n";

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
        private byte[] GetByteFromFile(string strPath)
        {
            System.IO.FileStream srm = new System.IO.FileStream(strPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            byte[] by = new byte[srm.Length];

            srm.Read(by, 0, by.Length);

            srm.Close();
            return by;

        }
        /// <summary>
        /// 保存内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                busyIndicatorHelp.ShowMessage();
                busyIndicatorHelp.SetWaitFormCaption("数据保存中....");
                int result = 0;
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
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleRemarkFrm_Load(object sender, EventArgs e)
        {
            InzationScheduleRemarkDate();
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSure_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveData() > 0)
                {
                    AutoClosedMsgBox.ShowForm("保存成功。", "本周备注信息", 1000, MessageBoxIcon.Asterisk);
                }
                else
                {
                    AutoClosedMsgBox.ShowForm("失败。", "本周备注信息", 1000, MessageBoxIcon.Asterisk);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                AutoClosedMsgBox.ShowForm(ex.Message, "本周备注信息", 1000, MessageBoxIcon.Asterisk);
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion


    }
}
