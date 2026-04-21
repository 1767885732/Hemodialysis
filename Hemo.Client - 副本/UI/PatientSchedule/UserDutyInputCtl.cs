/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:选择患者列表类
 * 创建标识:贺建操-2016年4月28日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Model;
using Hemo.IService;
using Hemo.Service;
using Hemo.Client.UI.Patient;
using Hemo.IService.Config;

namespace Hemo.Client.UI.PatientSchedule
{
    public partial class UserDutyInputCtl :HemoBaseFrm
    {
        #region 类变量

        private IPatient _patientService = ServiceManager.Instance.PatientService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _data = new ConfigModel.MED_COMMON_ITEMLISTDataTable();

        #endregion

        #region 属性

        public string itemValue { get; set; }

        public string itemName { get; set; }

        #endregion

        #region 构造函数

        public UserDutyInputCtl()
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
        private void UserDutyInputCtl_Load(object sender, EventArgs e)
        {
            InzationPatientDate();
        }

        /// <summary>
        /// 文本改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_FilterPatient_TextChanged(object sender, EventArgs e)
        {
            ConfigModel.MED_COMMON_ITEMLISTDataTable filterData = new ConfigModel.MED_COMMON_ITEMLISTDataTable();

            var d = this._data.Where(i => i.ITEM_VALUE.ToUpper().IndexOf(txt_FilterPatient.Text.Trim().ToUpper()) >= 0 || i.ITEM_NAME.ToUpper().IndexOf(txt_FilterPatient.Text.Trim().ToUpper()) >= 0).ToList();
            foreach (var row in d)
            {
                filterData.ImportRow(row);
            }

            this.gridControlPatient.DataSource = filterData;
        }

        /// <summary>
        /// gridView1 MouseDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            var row = this.gridView1.GetFocusedDataRow() as ConfigModel.MED_COMMON_ITEMLISTRow;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = gridView1.CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                //判断光标是否在行范围内 
                if (hInfo.InRow)
                {
                    itemValue = row.ITEM_ID;
                    itemName = row.ITEM_NAME;

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
        }

        private void UserDutyInputCtl_FormClosing(object sender, FormClosingEventArgs e)
        {


        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSure_Click(object sender, EventArgs e)
        {
            var row = this.gridView1.GetFocusedDataRow() as ConfigModel.MED_COMMON_ITEMLISTRow;

            itemValue = row.ITEM_ID;
            itemName = row.ITEM_NAME;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InzationPatientDate()
        {
            this.busyIndicator1.ShowLoadingScreenFor(this.gridControlPatient);
            var areaComm = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
            var dutyComm = new ConfigModel.MED_COMMON_ITEMLISTDataTable();

            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    areaComm = this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1");
                    dutyComm = this._configService.GetConfigList(string.Empty, string.Empty, "假期类型", "1");
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    foreach (ConfigModel.MED_COMMON_ITEMLISTRow row in areaComm.Rows)
                    {
                        this._data.ImportRow(row);
                    }
                    foreach (ConfigModel.MED_COMMON_ITEMLISTRow row in dutyComm.Rows)
                    {
                        this._data.ImportRow(row);
                    }

                    this.gridControlPatient.DataSource = this._data;

                    if (!string.IsNullOrEmpty(itemName))
                        this.txt_FilterPatient.Text = itemName;
                    this.busyIndicator1.HideLoadingScreen();
                };
                worker.RunWorkerAsync();
            }

        }

        #endregion
    }
}
