/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者费用记账编辑类
// 创建时间：2014-07-06
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.IService;
using DevExpress.XtraEditors.Controls;
using Hemo.IService.Config;
using Hemo.Client.UI.Hemodialysis;
using System.Configuration;
using Hemo.Client.Core;
using Hemo.IService.Dict;
using System.Linq;
namespace Hemo.Client.UI.Patient
{
    public partial class PatientBillRecordFrm : HemoBaseFrm
    {
        #region 类变量

        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IPatient _patientService = ServiceManager.Instance.PatientService;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _itemData;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _FeeItemData;
        public string currentHemoID = string.Empty;

        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        private HemodialysisModel.MED_CURE_MAIN_BILLDataTable _patientBill = new HemodialysisModel.MED_CURE_MAIN_BILLDataTable();

        private HemoModel.MED_PATIENT_PREPAYDataTable _patientRastBill = new HemoModel.MED_PATIENT_PREPAYDataTable();

        #endregion

        #region 属性

        public Client.Controls.Treatment.CtlTreatmentPerson PatientTreatmentInfo { get; set; }

        #endregion

        #region 构造函数

        public PatientBillRecordFrm()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void PatientRecord_Load(object sender, EventArgs e)
        {
            InzationData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lupCHECK_NURSE.Text.Trim() == "")
            {
                MessageBox.Show("请选择操作人!");
                return;
            }
            if (txtDRUG_NAME.Text.Trim() == "")
            {
                MessageBox.Show("请选择项目!");
                return;
            }
            _patientBill.AddMED_CURE_MAIN_BILLRow(Guid.NewGuid().ToString(), PatientTreatmentInfo.PatientScheduleRow.RECIPE_ID, txtDRUG_NAME.EditValue.ToString(), txtDRUG_NAME.Text, spnDRUG_TIMES.Value, txtAllFee.Text, lupUSE_TYPE.EditValue.ToString(), PatientTreatmentInfo.PatientScheduleRow.DIALYSIS_DATE, lupCHECK_NURSE.EditValue.ToString(), lupCHECK_NURSE.Text, PatientTreatmentInfo.PatientScheduleRow.DIALYSIS_DATE.ToString("yyyyMM"), PatientTreatmentInfo.PatientRow.HEMODIALYSIS_ID, PatientTreatmentInfo.PatientRow.PATIENT_ID, txtNote.Text);
            //EnableTypeInfo();
            this.txtDRUG_NAME.EditValue = string.Empty;
            this.spnDRUG_TIMES.EditValue = 0;
            this.txtRastCount.Text = "0";
            this.txtRastFee.Text = "0";
            this.txtAllFee.Text = string.Empty;
        }

        private void txtDRUG_NAME_EditValueChanged(object sender, EventArgs e)
        {
            EnableTypeInfo();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_patientService.SavePatientBillInfo(_patientBill) > 0)
            {
                InzationData();
                XtraMessageBox.Show("保存成功。", "记账项目");

            }
            else
            {
                XtraMessageBox.Show("保存失败。", "记账项目");

            }
        }

        private void spnDRUG_TIMES_EditValueChanged(object sender, EventArgs e)
        {
            EnableTypeInfo();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            var frm = new PatientBillListFrm();
            frm.DefaultHemoID = PatientTreatmentInfo.PatientRow.HEMODIALYSIS_ID;
            frm.ShowDialog();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除当前行?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                gvRecord.GetFocusedDataRow().Delete();
            }

        }

        private void dxSimpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 方法

        private void InzationData()
        {
            DataTable dtStaffSict = null;
            _patientRastBill = new HemoModel.MED_PATIENT_PREPAYDataTable();
            _patientBill = new HemodialysisModel.MED_CURE_MAIN_BILLDataTable();
            patientInfo.SetControlsEnabled(false);
            patientInfo.HEMODIALYSIS_ID = PatientTreatmentInfo.PatientRow.HEMODIALYSIS_ID;
            patientInfo.LoadPatientInfo();
            var hemoID = PatientTreatmentInfo.PatientRow.HEMODIALYSIS_ID;
            var hemoCureID = PatientTreatmentInfo.PatientScheduleRow.RECIPE_ID;
            var hemoTime = PatientTreatmentInfo.PatientScheduleRow.DIALYSIS_DATE;

            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {

                    dtStaffSict = _staffDictService.GetStaffDictList();
                    _itemData = _configService.GetConfigList("", "", "小记账项目", "1");
                    var dicSmall = _configService.GetConfigList("", "", "大记账项目", "1");
                    _itemData.Merge(dicSmall);
                    _FeeItemData = _configService.GetConfigList("", "", "费用", "1");
                    _patientBill = _patientService.GetPatientBillByHemoIDCureID(hemoID, hemoTime);
                    _patientBill.DefaultView.RowFilter = "RECIPE_ID='" + hemoCureID + "'";
                    //剩余费用总数
                    _patientRastBill = _patientService.GetRastBillByHemoID(hemoID);
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    txtDRUG_NAME.Properties.DataSource = _itemData;
                    BaseControlInfo.BindLookUpEdit(lupCHECK_NURSE, "EMP_NO", "NAME", dtStaffSict, "NAME", "操作人");
                    gcRecord.DataSource = _patientBill;
                    gridControl1.DataSource = _patientRastBill;
                    if (PatientTreatmentInfo.PatientRow.MEDICAL_TYPE == "医保")
                    {
                        lupUSE_TYPE.Enabled = true;
                    }
                    lupCHECK_NURSE.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;

                };
                worker.RunWorkerAsync();
            }
        }

        private void PatientCost()
        {
            #region MyRegion

            //var _prePay = _patientService.GetPatientPrePayInfos(PatientTreatmentInfo.PatientRow.HEMODIALYSIS_ID);

            //var queryDate = _patientService.GetPatientBillByHemoID(PatientTreatmentInfo.PatientRow.HEMODIALYSIS_ID, Convert.ToDateTime("2015-05-01"), DateTime.Now.Date.AddDays(1));
            //decimal allFee = 0;
            //foreach (HemoModel.MED_PATIENT_PREPAYRow row in _prePay.Rows)
            //{
            //    allFee += row.PREPAYCOST;
            //}
            //decimal itemFee = 0;
            //foreach (HemodialysisModel.MED_CURE_MAIN_BILLRow row in queryDate.Rows)
            //{
            //    itemFee += Utility.CDecimal(row.BILL_PRICE);
            //}
            //decimal costFee = allFee - itemFee;
            //this.txtRestCost.Text = string.Format("余额:{0}", costFee.ToString());

            #endregion
        }

        private void EnableTypeInfo()
        {
            this.errorProvider.ClearErrors();
            lupUSE_TYPE.Enabled = false;
            if (txtDRUG_NAME.EditValue == null || string.IsNullOrEmpty(txtDRUG_NAME.EditValue.ToString()))
            {
                return;
            }

            var rastFeeRow = _patientRastBill.FirstOrDefault(i => i.ITEM_ID == txtDRUG_NAME.EditValue.ToString());
            if (rastFeeRow != null)
            {
                if (rastFeeRow.ITEM_COUNT < int.Parse(spnDRUG_TIMES.EditValue.ToString()))
                {
                    this.errorProvider.SetError(spnDRUG_TIMES, "项目记账数量不能大于项目预交数量.");
                    return;
                }
                else
                {
                    this.txtRastCount.Text = (rastFeeRow.ITEM_COUNT - int.Parse(spnDRUG_TIMES.EditValue.ToString())).ToString();
                }
            }
            else
            {
                this.errorProvider.SetError(txtDRUG_NAME, "患者此项目费用为空不可以进行记账.");
                this.txtDRUG_NAME.EditValue = string.Empty;
                this.spnDRUG_TIMES.EditValue = 0;
                this.txtRastCount.Text = "0";
                this.txtRastFee.Text = "0";
                return;
            }

            var feeRow = _FeeItemData.FirstOrDefault(i => i.ITEM_VALUE == txtDRUG_NAME.EditValue.ToString());
            if (feeRow != null)
            {
                this.txtAllFee.Text = (decimal.Parse(feeRow.ITEM_NAME) * int.Parse(spnDRUG_TIMES.EditValue.ToString())).ToString();
                this.txtRastFee.Text = (rastFeeRow.PREPAYCOST - Utility.CDecimal(this.txtAllFee.Text)).ToString();
            }
            else
            {
                this.txtAllFee.Text = "0";
                this.txtRastFee.Text = (rastFeeRow.PREPAYCOST - Utility.CDecimal(this.txtAllFee.Text)).ToString();

            }
            lupUSE_TYPE.EditValue = "20";

            var rowDic = _itemData.FirstOrDefault(wh
                 => wh.ITEM_ID == txtDRUG_NAME.EditValue.ToString());
            if (rowDic != null && rowDic.ITEM_TYPE != "大记账项目")
            {
                lupUSE_TYPE.Enabled = false;
                return;
            }
            var list = _patientBill.Where(wh => wh.RowState != DataRowState.Deleted && wh.BILL_ITEM_ID == txtDRUG_NAME.EditValue.ToString() && wh.BILL_TYPE == "21").ToList();
            if (PatientTreatmentInfo.PatientRow.MEDICAL_TYPE == "医保" && (list.Count() == 0 || (list.Sum(wh => wh.BILL_COUNT) + spnDRUG_TIMES.Value) < 3))
            {
                lupUSE_TYPE.EditValue = "21";
                lupUSE_TYPE.Enabled = true;
            }
        }

        #endregion
    }
}