/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:用户控件
 * 创建标识:贺建操-2016年12月28日
 * 
 * 修改时间:2017年5月15日
 * 修改人:刘超
 * 修改描述:修复系统响应速度慢的问题
 * 
 * 修改时间:2017年6月16日
 * 修改人:刘超
 * 修改描述:用户控件
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.IService;
using Hemo.Service;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.Utilities;
using DevExpress.XtraEditors.Controls;
using Hemo.Client.Base;
using Hemo.IService.Config;

namespace Hemo.Client.Modules
{
    public partial class PatientPrePay : BaseMoudleControl
    {
        #region 构造函数
        public PatientPrePay()
        {
            InitializeComponent();
            base.viewModelCore = CreateViewModel<TaskViewModel>();
            ///事件
            this.patientInfoCheck1.patientPickEvent += new EventHandler(patientInfoCheck1_patientPickEvent);
        }


        #endregion

        #region 变量
        private Hemo.Model.HemoModel.MED_PATIENT_PREPAYDataTable _prePay = null;
        private Hemo.Model.HemoModel.MED_PATIENT_PREPAYDataTable _prePayForUser = null;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _itemData;
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _FeeItemData;
        public TaskViewModel ViewModel
        {
            get { return GetViewModel<TaskViewModel>(); }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InzationData()
        {
            using (var _work = new BackgroundWorker())
            {
                this.busyIndicator1.ShowLoadingScreenFor(this.gridControl1);
                string hemoId = string.Empty;
                _prePay = new Model.HemoModel.MED_PATIENT_PREPAYDataTable();
                var _prePayTemp = new Model.HemoModel.MED_PATIENT_PREPAYDataTable();
                var _patientRastBill = new HemoModel.MED_PATIENT_PREPAYDataTable();

                //_prePayForUser = new HemoModel.MED_PATIENT_PREPAYDataTable();
                //_itemData = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
                //_FeeItemData = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
                if (this.patientInfoCheck1._patientDataTable.Rows.Count > 0)
                {
                    hemoId = this.patientInfoCheck1._patientDataTable[0].HEMODIALYSIS_ID;
                }
                _work.DoWork += delegate(object sender, DoWorkEventArgs e)
                {
                    ////预交记账项目
                    //_itemData = _configService.GetConfigList("", "", "小记账项目", "1");
                    //var dicSmall = _configService.GetConfigList("", "", "大记账项目", "1");
                    //_itemData.Merge(dicSmall);
                    ////获取记账项目的费用
                    //_FeeItemData = _configService.GetConfigList("", "", "费用", "1");

                    //获取预交金
                    _prePayTemp = objPatient.GetPatientPrePayInfos(hemoId);
                    _prePayForUser = objPatient.GetPatientPrePayInfosByUserId(Hemo.Client.Core.HemoApplicationContext.Current.CurrentUser.USER_ID);
                    _patientRastBill = objPatient.GetCurrentRastByHemoID(hemoId);


                };
                _work.RunWorkerCompleted += delegate(object sender1, RunWorkerCompletedEventArgs e1)
                {
                    if (!string.IsNullOrEmpty(hemoId))
                    {
                        var patientRow = this.patientInfoCheck1._patientDataTable[0];
                        this.txtHEMODIALYSIS_ID.Text = patientRow.HEMODIALYSIS_ID;
                        this.txtPATIENT_ID.Text = patientRow.PATIENT_ID;
                        this.txtNAME.Text = patientRow.NAME;
                        this.txtSEX.Text = patientRow.SEX;
                        this.txtAge.Text = patientRow.AGE.ToString();
                        this.txtBIRTHDAY.EditValue = patientRow.BIRTHDAY;
                        this.txtDIAGNOSE.Text = patientRow.DIAGNOSE;
                        this.cbxTIME_TYPE.EditValue = patientRow.TIME_TYPE;
                        this.txtMARITAL.Text = patientRow.MARITAL;
                        this.txtCardNo.Text = patientRow.CARDNO;
                        if (_patientRastBill != null && _patientRastBill.Rows.Count > 0)
                        {
                            this.txtRast.Text = string.Format("{0} 元", _patientRastBill[0].PREPAYCOST.ToString());
                        }
                        this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
                        //PatientCost();
                    }
                    _prePayTemp.Take(10).CopyToDataTable<HemoModel.MED_PATIENT_PREPAYRow>(_prePay, LoadOption.PreserveChanges);
                    this.gridControl1.DataSource = _prePay;
                    this.gridControl2.DataSource = _prePayForUser;
                    this.busyIndicator1.HideLoadingScreen();
                };
                _work.RunWorkerAsync();
            }
        }
        private void patientInfoCheck1_patientPickEvent(object sender, EventArgs e)
        {
            InzationData();
        }
        /// <summary>
        /// 早的版本中用去判断患者总费用结余的，现在不用啦
        /// </summary>
        private void PatientCost()
        {
            var _prePay = objPatient.GetPatientPrePayInfos(this.patientInfoCheck1._patientDataTable[0].HEMODIALYSIS_ID);

            var queryDate = objPatient.GetPatientBillByHemoID(this.patientInfoCheck1._patientDataTable[0].HEMODIALYSIS_ID, Convert.ToDateTime("2015-05-01"), DateTime.Now.Date.AddDays(1));
            decimal allFee = 0;
            foreach (HemoModel.MED_PATIENT_PREPAYRow row in _prePay.Rows)
            {
                allFee += row.PREPAYCOST;
            }
            decimal itemFee = 0;
            foreach (HemodialysisModel.MED_CURE_MAIN_BILLRow row in queryDate.Rows)
            {
                itemFee += Utility.CDecimal(row.BILL_PRICE);
            }
            decimal costFee = allFee - itemFee;

            //this.txtTotalCost.Text = string.Format("{0}", costFee.ToString());
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool VailidPrepay()
        {
            this.errorProvider.ClearErrors();
            if (this.patientInfoCheck1._patientDataTable.Rows.Count <= 0)
            {
                this.errorProvider.SetError(this.patientInfoCheck1, "请输入要交预交金的患者");
                this.patientInfoCheck1.SetFocuse();
                return false;
            }
            if (this.txtPrePay.Text.Length <= 0 || this.txtPrePay.Text == "0")
            {
                this.errorProvider.SetError(this.txtPrePay, "项目金额不能为0请去维护项目金额。");
                this.txtPrePay.Focus();
                return false;

            }
            return true;
        }

        private void Clear()
        {
            this.txtHEMODIALYSIS_ID.Text = string.Empty;
            this.txtPATIENT_ID.Text = string.Empty;
            this.txtNAME.Text = string.Empty;
            this.txtSEX.Text = string.Empty;
            this.txtAge.Text = string.Empty;
            this.txtBIRTHDAY.EditValue = string.Empty;
            this.txtDIAGNOSE.Text = string.Empty;
            this.cbxTIME_TYPE.EditValue = string.Empty;
            this.txtMARITAL.Text = string.Empty;
            this.txtCardNo.Text = string.Empty;
            //this.txtPrePay.Text = string.Empty;
            //this.txtTotalCost.Text = string.Empty;
            this.patientInfoCheck1._patientDataTable = new PatientModel.MED_PATIENTSDataTable();
            InzationData();
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage2;
        }
        /// <summary>
        /// 自动计算费用
        /// </summary>
        private void EnableTypeInfo()
        {
            //if (txtDRUG_NAME.EditValue == null)
            //{
            //    return;
            //}

            //var feeRow = _FeeItemData.FirstOrDefault(i => i.ITEM_VALUE == txtDRUG_NAME.EditValue.ToString());
            //if (feeRow != null)
            //{
            //    this.txtPrePay.Text = (decimal.Parse(feeRow.ITEM_NAME) * int.Parse(spnITEM_COUNT.EditValue.ToString())).ToString();
            //}
            //else
            //{
            //    this.txtPrePay.Text = "0";

            //}
        }
        #endregion

        #region 事件

        private void labelControl7_Click(object sender, EventArgs e)
        {

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (VailidPrepay())
            {
                var row = this._prePay.NewMED_PATIENT_PREPAYRow();
                row.ID = Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = this.patientInfoCheck1._patientDataTable[0].HEMODIALYSIS_ID;
                //row.ITEM_ID = this.txtDRUG_NAME.EditValue.ToString();
                //row.ITEM_COUNT = Convert.ToInt32(this.spnITEM_COUNT.EditValue.ToString());
                row.PREPAYCOST = Convert.ToDecimal(this.txtPrePay.Text);
                row.PAYTIME = System.DateTime.Now;
                row.CREATEBY = Hemo.Client.Core.HemoApplicationContext.Current.CurrentUser.USER_ID;
                row.CREATEDATE = System.DateTime.Now;
                this._prePay.AddMED_PATIENT_PREPAYRow(row);

                if (this.objPatient.SavePatientPrePayInfos(this._prePay) > 0)
                { XtraMessageBox.Show("保存成功。", "基础信息"); }
                else
                {
                    XtraMessageBox.Show("失败。", "基础信息");
                }
                this.txtPrePay.Text = "0";
                //this.spnITEM_COUNT.EditValue = 1;
                //this.txtDRUG_NAME.EditValue = string.Empty;

                //PatientCost();
                InzationData();
            }
        }

        private void PatientPrePay_Load(object sender, EventArgs e)
        {
            this.patientInfoCheck1.SetFocuse();
            this.patientInfoCheck1.Focus();
            InzationData();
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage2;

        }
        private void txtDRUG_NAME_EditValueChanged(object sender, EventArgs e)
        {
            //EnableTypeInfo();
        }
        private void spnITEM_COUNT_EditValueChanged(object sender, EventArgs e)
        {
            //EnableTypeInfo();
        }
        private void txtPrePay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (VailidPrepay())
                {
                    var row = this._prePay.NewMED_PATIENT_PREPAYRow();
                    row.ID = Guid.NewGuid().ToString();
                    row.HEMODIALYSIS_ID = this.patientInfoCheck1._patientDataTable[0].HEMODIALYSIS_ID;
                    //row.PREPAYCOST = Convert.ToDecimal(this.txtPrePay.Text);
                    row.PAYTIME = System.DateTime.Now;
                    row.CREATEBY = Hemo.Client.Core.HemoApplicationContext.Current.CurrentUser.USER_ID;
                    row.CREATEDATE = System.DateTime.Now;
                    this._prePay.AddMED_PATIENT_PREPAYRow(row);

                    if (this.objPatient.SavePatientPrePayInfos(this._prePay) > 0)
                    { XtraMessageBox.Show("保存成功。", "基础信息"); }
                    else
                    {
                        XtraMessageBox.Show("失败。", "基础信息");
                    }
                    this.txtPrePay.Text = "0";
                    //PatientCost();
                    InzationData();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            if (XtraMessageBox.Show("是否确认关闭？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ((DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog)this.Parent).Close();

            }

            //ViewModel.Close();
        }

        #endregion

      








    }
}
