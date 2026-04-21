/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:修改类
 * 创建标识:吕志强-2013年7月9日
 * 
 * 修改时间:2013年10月17日
 * 修改人:贺建操
 * 修改描述:修改方法
 * 
 * 修改时间:2014年1月25日
 * 修改人:顾伟伟
 * 修改描述:新增方法
 * 
 * 修改时间:2014年5月5日
 * 修改人:顾伟伟
 * 修改描述:修改方法SQL
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService;
using Hemo.Service;
using Hemo.Model;
using Hemo.IService.Config;
using Hemo.Client.Core;
using Hemo.Utilities;
using Hemo.Client.UI.Lab;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class EditEstimateVenous : HemoBaseFrm
    {
        #region 成员变量

        private bool isTemp = false;

        private DataRow currentRow = null;

        private string hemoId = string.Empty;

        private IPatient patientService = ServiceManager.Instance.PatientService;

        private IHemodialysis hemodialysisService = ServiceManager.Instance.HemodialysisService;

        private DataTable dtEstimateVenous = null;

        private DataTable dtCurrentEstimateVenous = null;

        #endregion

        #region 属性

        /// <summary>
        /// 是否临时
        /// </summary>
        public bool IsTemp
        {
            get { return isTemp; }
            set { isTemp = value; }
        }

        /// <summary>
        /// 透析编号
        /// </summary>
        public string HemoId
        {
            get { return hemoId; }
            set { hemoId = value; }
        }

        /// <summary>
        /// 当前编辑行
        /// </summary>
        public DataRow CurrentRow
        {
            get { return currentRow; }
            set { currentRow = value; }
        }

        #endregion

        #region 构造函数

        public EditEstimateVenous() {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditEstimateVenous_Load(object sender, EventArgs e) {
            this.Text = isTemp ? "编辑患者临时留置静脉导管评估记录" : "编辑患者长期留置静脉导管评估记录";
            this.deCreate_Date.DateTime = DateTime.Now.Date;
            this.chkSuture.Enabled = isTemp;

            LoadPatientInfo();
            LoadEstimateVenousList();

            if (currentRow != null) {
                LoadEstimateVenous();
                dtCurrentEstimateVenous = isTemp ? hemodialysisService.GetEstimateVenousCatheterById(currentRow["ID"].ToString()) as DataTable : hemodialysisService.GetEstimateLongVenousById(currentRow["ID"].ToString()) as DataTable;
                this.deCreate_Date.Enabled = false;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e) {
            if (currentRow == null) {
                //新增
                if (dtEstimateVenous != null && dtEstimateVenous.Rows.Count > 0) {
                    foreach (DataRow row in dtEstimateVenous.Rows) {
                        if ((row["HEMODIALYSIS_ID"].ToString() == this.ctlUserLongInfo.HEMODIALYSIS_ID) && (DateTime.Parse(row["CREATE_DATE"].ToString()) == this.deCreate_Date.DateTime.Date)) {
                            XtraMessageBox.Show("同一录入日期的记录不能重复！");
                            return;
                        }
                    }
                }
            }

            dtCurrentEstimateVenous = GetCurrentEstimateVenousDataTable();
            int result = hemodialysisService.SaveEstimateVenous(dtCurrentEstimateVenous);
            if (result > 0) {
                this.DialogResult = DialogResult.OK;
                AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);

                //   XtraMessageBox.Show("保存静脉导管评估记录成功！");
            }
            else {
                XtraMessageBox.Show("保存静脉导管评估记录失败！");
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载病人信息
        /// </summary>
        private void LoadPatientInfo() {
            PatientModel.MED_PATIENTSDataTable dtPatient = patientService.GetPatientListByParams(string.Empty, hemoId);
            if (dtPatient != null && dtPatient.Rows.Count > 0) {
                this.ctlUserLongInfo.HEMODIALYSIS_ID = dtPatient[0].HEMODIALYSIS_ID;
                this.ctlUserLongInfo.LoadPatientInfo();
            }
        }

        /// <summary>
        /// 加载静脉导管评估数据
        /// </summary>
        private void LoadEstimateVenous() {
            if (currentRow != null) {
                this.chkApplication.Checked = currentRow["APPLICATION"].ToString() == "是" ? true : false;
                if (isTemp) {
                    this.chkSuture.Checked = currentRow["SUTURE"].ToString() == "是" ? true : false;
                }
                this.chkCatheter.Checked = currentRow["CATHETER"].ToString() == "是" ? true : false;
                this.chkClean_And_Dry.Checked = currentRow["CLEAN_AND_DRY"].ToString() == "是" ? true : false;
                this.chkFlow_Pollution.Checked = currentRow["FLOW_POLLUTION"].ToString() == "有" ? true : false;
                this.chkRed.Checked = currentRow["RED"].ToString() == "是" ? true : false;
                this.chkSwollen.Checked = currentRow["SWOLLEN"].ToString() == "是" ? true : false;
                this.chkHot.Checked = currentRow["HOT"].ToString() == "是" ? true : false;
                this.chkPain.Checked = currentRow["PAIN"].ToString() == "是" ? true : false;
                this.chkArtery_Clarification.Checked = currentRow["ARTERY_CLARIFICATION"].ToString() == "是" ? true : false;
                this.chkArtery_Blood.Checked = currentRow["ARTERY_BLOOD"].ToString() == "有" ? true : false;
                this.chkArtery_Flow_Velocity.Checked = currentRow["ARTERY_FLOW_VELOCITY"].ToString() == "是" ? true : false;
                this.chkArtery_Reflux.Checked = currentRow["ARTERY_REFLUX"].ToString() == "是" ? true : false;
                this.chkArtery_Thrombosis.Checked = currentRow["ARTERY_THROMBOSIS"].ToString() == "是" ? true : false;
                this.chkVein_Clarification.Checked = currentRow["VEIN_CLARIFICATION"].ToString() == "是" ? true : false;
                this.chkVein_Blood.Checked = currentRow["VEIN_BLOOD"].ToString() == "有" ? true : false;
                this.chkVein_Flow_Velocity.Checked = currentRow["VEIN_FLOW_VELOCITY"].ToString() == "是" ? true : false;
                this.chkVein_Reflux.Checked = currentRow["VEIN_REFLUX"].ToString() == "是" ? true : false;
                this.chkVein_Thrombosis.Checked = currentRow["VEIN_THROMBOSIS"].ToString() == "是" ? true : false;
                this.deCreate_Date.DateTime = (DateTime)currentRow["CREATE_DATE"];
                this.chkFlow_Better.Checked = currentRow["FLOW_BETTER"].ToString() == "是" ? true : false;
                this.chkSuction.Checked = currentRow["SUCTION"].ToString() == "有" ? true : false;
                this.rgpBlood_Lead_End.EditValue = currentRow["BLOOD_LEAD_END"].ToString();
                this.txtPosition_Requirement.Text = currentRow["POSITION_REQUIREMENT"].ToString();
                this.txtHeparin_Sodium.Text = currentRow["HEPARIN_SODIUM"].ToString();
                this.txtUrokinase.Text = currentRow["UROKINASE"].ToString();
                this.txtOther.Text = currentRow["OTHER"].ToString();
                this.chkIn_Dressing.Checked = currentRow["IN_DRESSING"].ToString() == "是" ? true : false;
                this.chkInfected.Checked = currentRow["INFECTED"].ToString() == "是 " ? true : false;
                this.chkThrombolysis.Checked = currentRow["THROMBOLYSIS"].ToString() == "是" ? true : false;
            }
        }

        /// <summary>
        /// 加载静脉导管评估数据列表
        /// </summary>
        private void LoadEstimateVenousList() {
            dtEstimateVenous = isTemp ? hemodialysisService.GetEstimateVenousCatheterList() as DataTable : hemodialysisService.GetEstimateLongVenousList() as DataTable;
        }

        /// <summary>
        /// 获取当前新增或编辑操作的DataTable
        /// </summary>
        /// <returns></returns>
        private DataTable GetCurrentEstimateVenousDataTable() {
            if (currentRow == null) {
                //新增
                dtCurrentEstimateVenous = isTemp ? new HemodialysisModel.MED_ESTIMATE_VENOUS_CATHETERDataTable() as DataTable : new HemodialysisModel.MED_ESTIMATE_LONG_VENOUSDataTable() as DataTable;
                var row = dtCurrentEstimateVenous.NewRow();
                row["ID"] = Guid.NewGuid().ToString();
                row["CREATE_DATE"] = this.deCreate_Date.DateTime.Date;
                row["APPLICATION"] = this.chkApplication.Checked ? "1" : "0";
                if (isTemp) {
                    row["SUTURE"] = this.chkSuture.Checked ? "1" : "0";
                }
                row["HEMODIALYSIS_ID"] = this.ctlUserLongInfo.HEMODIALYSIS_ID;
                row["CATHETER"] = this.chkCatheter.Checked ? "1" : "0";
                row["CLEAN_AND_DRY"] = this.chkClean_And_Dry.Checked ? "1" : "0";
                row["FLOW_POLLUTION"] = this.chkFlow_Pollution.Checked ? "1" : "0";
                row["RED"] = this.chkRed.Checked ? "1" : "0";
                row["SWOLLEN"] = this.chkSwollen.Checked ? "1" : "0";
                row["HOT"] = this.chkHot.Checked ? "1" : "0";
                row["PAIN"] = this.chkPain.Checked ? "1" : "0";
                row["ARTERY_CLARIFICATION"] = this.chkArtery_Clarification.Checked ? "1" : "0";
                row["ARTERY_BLOOD"] = this.chkArtery_Blood.Checked ? "1" : "0";
                row["ARTERY_FLOW_VELOCITY"] = this.chkArtery_Flow_Velocity.Checked ? "1" : "0";
                row["ARTERY_REFLUX"] = this.chkArtery_Reflux.Checked ? "1" : "0";
                row["ARTERY_THROMBOSIS"] = this.chkArtery_Thrombosis.Checked ? "1" : "0";
                row["VEIN_CLARIFICATION"] = this.chkVein_Clarification.Checked ? "1" : "0";
                row["VEIN_BLOOD"] = this.chkVein_Blood.Checked ? "1" : "0";
                row["VEIN_FLOW_VELOCITY"] = this.chkVein_Flow_Velocity.Checked ? "1" : "0";
                row["VEIN_REFLUX"] = this.chkVein_Reflux.Checked ? "1" : "0";
                row["VEIN_THROMBOSIS"] = this.chkVein_Thrombosis.Checked ? "1" : "0";
                row["FLOW_BETTER"] = this.chkFlow_Better.Checked ? "1" : "0";
                row["SUCTION"] = this.chkSuction.Checked ? "1" : "0";
                row["BLOOD_LEAD_END"] = this.rgpBlood_Lead_End.EditValue.ToString();
                row["POSITION_REQUIREMENT"] = this.txtPosition_Requirement.Text;
                row["HEPARIN_SODIUM"] = this.txtHeparin_Sodium.Text;
                row["UROKINASE"] = this.txtUrokinase.Text;
                row["OTHER"] = this.txtOther.Text;
                row["IN_DRESSING"] = this.chkIn_Dressing.Checked ? "1" : "0";
                row["INFECTED"] = this.chkInfected.Checked ? "1" : "0";
                row["THROMBOLYSIS"] = this.chkThrombolysis.Checked ? "1" : "0";
                row["IS_DELETE"] = "0";
                row["USER_ID"] = HemoApplicationContext.Current.CurrentUser.USER_ID;
                row["EDIT_DATE"] = DateTime.Now;
                dtCurrentEstimateVenous.Rows.Add(row);
            }
            else {
                //编辑
                dtCurrentEstimateVenous.Rows[0]["APPLICATION"] = this.chkApplication.Checked ? "1" : "0";
                if (isTemp) {
                    dtCurrentEstimateVenous.Rows[0]["SUTURE"] = this.chkSuture.Checked ? "1" : "0";
                }
                dtCurrentEstimateVenous.Rows[0]["CATHETER"] = this.chkCatheter.Checked ? "1" : "0";
                dtCurrentEstimateVenous.Rows[0]["CLEAN_AND_DRY"] = this.chkClean_And_Dry.Checked ? "1" : "0";
                dtCurrentEstimateVenous.Rows[0]["FLOW_POLLUTION"] = this.chkFlow_Pollution.Checked ? "1" : "0";
                dtCurrentEstimateVenous.Rows[0]["RED"] = this.chkRed.Checked ? "1" : "0";
                dtCurrentEstimateVenous.Rows[0]["SWOLLEN"] = this.chkSwollen.Checked ? "1" : "0";
                dtCurrentEstimateVenous.Rows[0]["HOT"] = this.chkHot.Checked ? "1" : "0";
                dtCurrentEstimateVenous.Rows[0]["PAIN"] = this.chkPain.Checked ? "1" : "0";
                dtCurrentEstimateVenous.Rows[0]["ARTERY_CLARIFICATION"] = this.chkArtery_Clarification.Checked ? "1" : "0";
                dtCurrentEstimateVenous.Rows[0]["ARTERY_BLOOD"] = this.chkArtery_Blood.Checked ? "1" : "0";
                dtCurrentEstimateVenous.Rows[0]["ARTERY_FLOW_VELOCITY"] = this.chkArtery_Flow_Velocity.Checked ? "1" : "0";
                dtCurrentEstimateVenous.Rows[0]["ARTERY_REFLUX"] = this.chkArtery_Reflux.Checked ? "1" : "0";
                dtCurrentEstimateVenous.Rows[0]["ARTERY_THROMBOSIS"] = this.chkArtery_Thrombosis.Checked ? "1" : "0";
                dtCurrentEstimateVenous.Rows[0]["VEIN_CLARIFICATION"] = this.chkVein_Clarification.Checked ? "1" : "0";
                dtCurrentEstimateVenous.Rows[0]["VEIN_BLOOD"] = this.chkVein_Blood.Checked ? "1" : "0";
                dtCurrentEstimateVenous.Rows[0]["VEIN_FLOW_VELOCITY"] = this.chkVein_Flow_Velocity.Checked ? "1" : "0";
                dtCurrentEstimateVenous.Rows[0]["VEIN_REFLUX"] = this.chkVein_Reflux.Checked ? "1" : "0";
                dtCurrentEstimateVenous.Rows[0]["VEIN_THROMBOSIS"] = this.chkVein_Thrombosis.Checked ? "1" : "0";
                dtCurrentEstimateVenous.Rows[0]["FLOW_BETTER"] = this.chkFlow_Better.Checked ? "1" : "0";
                dtCurrentEstimateVenous.Rows[0]["SUCTION"] = this.chkSuction.Checked ? "1" : "0";
                dtCurrentEstimateVenous.Rows[0]["BLOOD_LEAD_END"] = this.rgpBlood_Lead_End.EditValue.ToString();
                dtCurrentEstimateVenous.Rows[0]["POSITION_REQUIREMENT"] = this.txtPosition_Requirement.Text;
                dtCurrentEstimateVenous.Rows[0]["HEPARIN_SODIUM"] = this.txtHeparin_Sodium.Text;
                dtCurrentEstimateVenous.Rows[0]["UROKINASE"] = this.txtUrokinase.Text;
                dtCurrentEstimateVenous.Rows[0]["OTHER"] = this.txtOther.Text;
                dtCurrentEstimateVenous.Rows[0]["IN_DRESSING"] = this.chkIn_Dressing.Checked ? "1" : "0";
                dtCurrentEstimateVenous.Rows[0]["INFECTED"] = this.chkInfected.Checked ? "1" : "0";
                dtCurrentEstimateVenous.Rows[0]["THROMBOLYSIS"] = this.chkThrombolysis.Checked ? "1" : "0";
                dtCurrentEstimateVenous.Rows[0]["EDIT_DATE"] = DateTime.Now;
            }

            return dtCurrentEstimateVenous;
        }

        #endregion

        private void dxSimpleButton1_Click(object sender, EventArgs e) {
            PatientModel.MED_PATIENTSRow _patientDocRow;
            _patientDocRow = patientService.GetPatientListByParams(string.Empty, HemoId)[0];
            if (_patientDocRow != null) {
                XtraForm form = new XtraForm();
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Text = _patientDocRow.NAME + "的检验数据";
                ctlLabFrm labFrm = new ctlLabFrm(_patientDocRow);
                form.Size = labFrm.Size;
                labFrm.LoadLabInfo(_patientDocRow);
                labFrm.Dock = DockStyle.Fill;
                form.Controls.Add(labFrm);
                form.Show();
            }
        }
    }
}