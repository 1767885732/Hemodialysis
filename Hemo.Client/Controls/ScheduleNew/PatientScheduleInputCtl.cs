/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:排班控件患者录入窗体
 * 创建标识:贺建操-2014年8月2日
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
using Hemo.Client.UI.PatientFixUI;
using DevExpress.XtraBars.Docking2010.Customization;

namespace Hemo.Client.Controls.ScheduleNew
{
    public partial class PatientScheduleInputCtl : HemoBaseFrm
    {
        #region 变量

        private PatientModel.MED_PATIENTSDataTable _patientDataTable;

        public PatientModel.MED_PATIENTSRow _PatientRow = null;

        private IPatient _patientService = ServiceManager.Instance.PatientService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _hemoMethond;
        public string patientValue { get; set; }
        public string patientName { get; set; }
        public string patientID { get; set; }
        public string patientRemark { get; set; }
        public string hemoID { get; set; }
        public string scheduleMode { get; set; }
        public string infectious_check_result { get; set; }

        private bool isQuarantineArea = false;

        public bool IsQuarantineArea
        {
            get { return isQuarantineArea; }
            set { isQuarantineArea = value; }
        }


        #endregion

        public PatientScheduleInputCtl()
        {
            InitializeComponent();
        }
        #region 方法
        /// <summary>
        /// 加载 数据
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
                    if (IsQuarantineArea)
                    {
                        data.Where(i => i.IS_DELETE == "0" && i.IS_NEW == "0").CopyToDataTable<PatientModel.MED_PATIENTSRow>(this._patientDataTable, LoadOption.PreserveChanges);
                    }
                    else
                    {
                        data.Where(i => i.IS_DELETE == "0" && i.IS_NEW == "0").CopyToDataTable<PatientModel.MED_PATIENTSRow>(this._patientDataTable, LoadOption.PreserveChanges);

                    }
                    this._hemoMethond = this._configService.GetConfigList(string.Empty, string.Empty, "净化方式", "1");
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.gridControlPatient.DataSource = this._patientDataTable;
                    this.customGridLookUpEdit1.Properties.DataSource = this._hemoMethond;

                    if (!string.IsNullOrEmpty(patientValue))
                        this.txt_FilterPatient.Text = patientValue.Substring(0, 3);
                    if (!string.IsNullOrEmpty(patientRemark))
                        this.txtRemark.Text = patientRemark;
                    if (!string.IsNullOrEmpty(scheduleMode))
                        customGridLookUpEdit1.EditValue = scheduleMode;
                    this.busyIndicator1.HideLoadingScreen();
                };
                worker.RunWorkerAsync();
            }

        }
        #endregion
        #region 事件
     

        private void PatientScheduleInputCtl_Load(object sender, EventArgs e)
        {
            InzationPatientDate();

        }
        /// <summary>
        /// 过滤
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
        /// 回车时录入 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            var row = this.gridView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            if (row == null)
            {
                return;
            }
            _PatientRow = row;
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(this.customGridLookUpEdit1.Text))
                    patientValue = string.Format("{0} {1}", row.NAME, txtRemark.Text.Trim());
                else
                    patientValue = string.Format("{0} {1} {2}", row.NAME, this.customGridLookUpEdit1.Text.ToString(), txtRemark.Text.Trim());
                patientName = row.NAME;
                patientID = row.PATIENT_ID;
                hemoID = row.HEMODIALYSIS_ID;
                scheduleMode = this.customGridLookUpEdit1.EditValue.ToString();
                patientRemark = txtRemark.Text.Trim();
                if (!row.IsINFECTIOUS_CHECK_RESULTNull() || row.INFECTIOUS_CHECK_RESULT != null)
                    infectious_check_result = row.INFECTIOUS_CHECK_RESULT.ToString();
                else
                    infectious_check_result = string.Empty;

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        /// <summary>
        /// 双击时录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            var row = this.gridView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            if (row == null)
            {
                return;
            }
            _PatientRow = row;

            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = gridView1.CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                //判断光标是否在行范围内 
                if (hInfo.InRow)
                {
                    if (string.IsNullOrEmpty(this.customGridLookUpEdit1.Text))
                        patientValue = string.Format("{0} {1}", row.NAME, txtRemark.Text.Trim());
                    else
                        patientValue = string.Format("{0} {1} {2}", row.NAME, this.customGridLookUpEdit1.Text.ToString(), txtRemark.Text.Trim());
                    patientName = row.NAME;
                    patientID = row.PATIENT_ID;
                    hemoID = row.HEMODIALYSIS_ID;
                    patientRemark = txtRemark.Text.Trim();
                    scheduleMode = this.customGridLookUpEdit1.EditValue.ToString();
                    if (!row.IsINFECTIOUS_CHECK_RESULTNull() || row.INFECTIOUS_CHECK_RESULT != null)
                        infectious_check_result = row.INFECTIOUS_CHECK_RESULT.ToString();
                    else
                        infectious_check_result = string.Empty;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
        }

        private void PatientScheduleInputCtl_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

        }
        /// <summary>
        /// 确定时录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSure_Click(object sender, EventArgs e)
        {
            var row = this.gridView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            _PatientRow = row;

            if (string.IsNullOrEmpty(this.customGridLookUpEdit1.Text))
                patientValue = string.Format("{0} {1}", row.NAME, txtRemark.Text.Trim());
            else
                patientValue = string.Format("{0} {1} {2}", row.NAME, this.customGridLookUpEdit1.Text.ToString(), txtRemark.Text.Trim());

            patientName = row.NAME;
            patientID = row.PATIENT_ID;
            scheduleMode = this.customGridLookUpEdit1.EditValue.ToString();

            hemoID = row.HEMODIALYSIS_ID;
            patientRemark = txtRemark.Text.Trim();
            if (!row.IsINFECTIOUS_CHECK_RESULTNull() || row.INFECTIOUS_CHECK_RESULT != null)
                infectious_check_result = row.INFECTIOUS_CHECK_RESULT.ToString();
            else
                infectious_check_result = string.Empty;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

        }
        /// <summary>
        /// 新增患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //EditPatientNew frmEditPatient = new EditPatientNew();
            //frmEditPatient.Current = null;
            //if (frmEditPatient.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            //{
            //    InzationPatientDate();
            //}

            var row = this.gridView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            if (row != null)
            {
                var patientInfoUi = new PatientInfoUI();
                patientInfoUi.Current = null;
                patientInfoUi.InitalizeData();
                FlyoutDialog.Show(this.FindForm(), patientInfoUi);
            }
        }
        /// <summary>
        /// 编辑患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            //EditPatientNew frmEditPatient = new EditPatientNew();
            //var row = this.gridView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            //if (row != null)
            //{
            //    frmEditPatient.Current = row;
            //    if (frmEditPatient.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        InzationPatientDate();
            //    }
            //}

            var row = this.gridView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            if (row != null)
            {
                var patientInfoUi = new PatientInfoUI();
                patientInfoUi.Current = row;
                patientInfoUi.InitalizeData();
                FlyoutDialog.Show(this.FindForm(), patientInfoUi);
            }
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
    }
}
