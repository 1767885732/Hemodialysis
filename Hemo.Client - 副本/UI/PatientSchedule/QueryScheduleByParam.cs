/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:患者排班查询类
 * 创建标识:贺建操-2016年4月25日
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
using Hemo.Utilities;
using Hemo.Model;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.IService.PatientSchedule;

namespace Hemo.Client.UI.PatientSchedule
{
    public partial class QueryScheduleByParam :HemoBaseFrm
    {
        #region 类变量

        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;
        private PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable _data = null;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private DataTable dt = null;

        #endregion

        #region 构造函数

        public QueryScheduleByParam()
        {
            InitializeComponent();
            IntiUI();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Query_Click(object sender, EventArgs e)
        {
            this.busyIndicator1.ShowLoadingScreenFor(this.gridControl1);
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    string patients = this.tx_Patients.Text.Trim();
                    string banchi = this.ediBanCi.EditValue.ToString() == "0" ? string.Empty : this.ediBanCi.EditValue.ToString();
                    string roomid = this.ediSickArea.EditValue.ToString() == "c570d95c-76a2-4af4-893a-1357065623bf" ? string.Empty : this.ediSickArea.EditValue.ToString();
                    _data = _patientScheduleService.QueryPatientScheduleByParam(patients, banchi, roomid);
                    dt = _patientScheduleService.GetSchedulePatientCount(banchi, roomid);

                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    if (_data == null)
                        return;
                    this.gridControl1.DataSource = _data;
                    if (dt != null && dt.Rows.Count > 0)
                        this.lb_Count.Text = string.Format("当前班次.病区的总排班人数为:{0}人 HD:{1}人 HDF:{2}人", dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString());


                    this.busyIndicator1.Hide();
                };
                worker.RunWorkerAsync();
            }


        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryScheduleByParam_Load(object sender, EventArgs e)
        {
            this.busyIndicator1.ShowLoadingScreenFor(this.gridControl1);
            using (BackgroundWorker _worker = new BackgroundWorker())
            {
                _worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    _data = _patientScheduleService.QueryPatientScheduleByParam(string.Empty, string.Empty, string.Empty);
                    dt = _patientScheduleService.GetSchedulePatientCount(string.Empty, string.Empty);

                };
                _worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    if (_data == null)
                        return;
                    this.gridControl1.DataSource = _data;
                    if (dt != null && dt.Rows.Count > 0)
                        this.lb_Count.Text = string.Format("当前班次.病区的总排班人数为:{0}人 HD:{1}人 HDF:{2}人", dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString());
                    this.busyIndicator1.Hide();
                };
                _worker.RunWorkerAsync();
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化界面
        /// </summary>
        private void IntiUI()
        {
            Hemo.Utilities.Utility.BindLookUpEdit(ediBanCi, "ITEM_ID", "ITEM_NAME", Utility.GetBanCi(), "ITEM_NAME", "班次");
            ediBanCi.EditValue = "0";
            ConfigModel.MED_COMMON_ITEMLISTDataTable config = this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1");
            if (config != null && config.Rows.Count > 0)
            {
                DataRow SickAreaRow = config.NewRow();
                SickAreaRow["ITEM_NAME"] = "全部";
                SickAreaRow["ITEM_ID"] = "c570d95c-76a2-4af4-893a-1357065623bf";
                SickAreaRow["ORDER_NUMBER"] = 0;
                config.Rows.InsertAt(SickAreaRow, 0);
                Hemo.Utilities.Utility.BindLookUpEdit(ediSickArea, "ITEM_ID", "ITEM_NAME", (DataTable)config, "ITEM_NAME", "区域");
                this.ediSickArea.EditValue = config[0].ITEM_ID;
            }
        }

        #endregion
    }
}