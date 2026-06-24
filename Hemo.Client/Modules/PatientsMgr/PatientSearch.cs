/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:用户控件
 * 创建标识:刘超-2017年1月18日
 * 
 * 修改时间:2017年6月5日
 * 修改人:吕志强
 * 修改描述:修改对外公开的方法
 * 
 * 修改时间:2017年7月7日
 * 修改人:顾伟伟
 * 修改描述:修复系统加载时数据缓存问题
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.IService;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Client;

namespace Hemo.Client.Modules
{
    /// <summary>
    /// 病历列表 
    /// </summary>
    public partial class PatientSearch : HemoBaseFrm
    {
        #region 变量

        private PatientModel.MED_PATIENTSDataTable _patientDataTable;

        private IPatient _patientService = ServiceManager.Instance.PatientService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _hemoMethond;

        public string hemoId { set; get; }

        public string patientValue { get; set; }

        #endregion

        #region 构造函数

        public PatientSearch()
        {
            InitializeComponent();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取数据
        /// </summary>
        public void InzationPatientDate()
        {
            this.busyIndicator1.ShowLoadingScreenFor(this.gridControlPatient);

            using (BackgroundWorker worker = new BackgroundWorker())
            {
                _patientDataTable = new PatientModel.MED_PATIENTSDataTable();
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    var data = this._patientService.GetPatientList();
                    data.Where(i => i.IS_DELETE == "0" && i.IS_NEW == "0").CopyToDataTable<PatientModel.MED_PATIENTSRow>(this._patientDataTable, LoadOption.PreserveChanges);
                    this._hemoMethond = this._configService.GetConfigList(string.Empty, string.Empty, "净化方式", "1");
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.gridControlPatient.DataSource = this._patientDataTable;
                    this.customGridLookUpEdit1.Properties.DataSource = this._hemoMethond;
                    this.busyIndicator1.HideLoadingScreen();
                };
                worker.RunWorkerAsync();
            }

        }

        #endregion

        #region 事件

        /// <summary>
        /// 登录时加载 数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientSearch_Load(object sender, EventArgs e)
        {
            InzationPatientDate();
        }

        /// <summary>
        /// 根据病人信息进行过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_FilterPatient_TextChanged(object sender, EventArgs e)
        {
            PatientModel.MED_PATIENTSDataTable data = new PatientModel.MED_PATIENTSDataTable();

            var d = this._patientDataTable.Where(i => i.INPUT_CODE.ToUpper().IndexOf(txt_FilterPatient.Text.Trim().ToUpper()) >= 0 || i.NAME.ToUpper().IndexOf(txt_FilterPatient.Text.Trim().ToUpper()) >= 0).ToList();
            foreach (var row in d)
            {
                data.ImportRow(row);
            }

            this.gridControlPatient.DataSource = data;
        }
        /// <summary>
        /// 查杀
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            InzationPatientDate();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// 或者双击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            var row = this.gridView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                hemoId = row.HEMODIALYSIS_ID;
                this.DialogResult = DialogResult.OK;
            }

        }

        #endregion
    }
}