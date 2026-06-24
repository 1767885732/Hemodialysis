/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:修改类
 * 创建标识:贺建操-2013年7月16日
 * 
 * 修改时间:2013年10月24日
 * 修改人:顾伟伟
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年2月1日
 * 修改人:吕志强
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年5月12日
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
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.IService;
using Hemo.IService.Config;
using Hemo.IService.Dict;
using Hemo.Client.Core;

namespace Hemo.Client.UI.Hemodialysis {
    public partial class EditEstimateInBasket :HemoBaseFrm
    {

        #region 变量
        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        #endregion
        #region 构造函数
        public EditEstimateInBasket(string pHemoID) {
            InitializeComponent();
            HemoID = pHemoID;
            loadLookUpEditList();
        }
        #endregion

        #region 事件

        private void btnClose_Click(object sender, EventArgs e) {

            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void loadLookUpEditList() {
            DataTable dtStaffSict = _staffDictService.GetStaffDictList();
            DataTable dtDoctorList = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'");
            if (dtDoctorList != null && dtDoctorList.Rows.Count > 0) {
                BaseControlInfo.BindLookUpEdit(cmbCHECK_USER_ID, "USER_NAME", "NAME", dtDoctorList, "NAME", "评估人");
                cmbCHECK_USER_ID.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
            }
            txtCREATE_DATE.EditValue = System.DateTime.Now.ToShortDateString();
            this.ctlUserLongInfo.HEMODIALYSIS_ID = HemoID;
            this.ctlUserLongInfo.LoadPatientInfo();
            
        }

        /// <summary>
        /// 病人内瘘评估数据
        /// </summary>
        private DataTable _InBasketDataTable;
        public DataTable InbasketDataTable {
            set {
                _InBasketDataTable = value;
            }
            get {
                return _InBasketDataTable;
            }
        }

        /// <summary>
        /// 内瘘评估id
        /// </summary>
        private string _id = string.Empty;
        public string ID {
            set {
                _id = value;
            }
            get {
                return _id;
            }
        }

        /// <summary>
        /// 透析ID
        /// </summary>
        private string hemoID;
        public string HemoID {
            get {
                return hemoID;
            }
            set {
                hemoID = value;
            }
        }


        private void loadInfo(string pID) {
            DataTable dtResult = objHemodialysisService.GetEsimateInBasketByID(ID);
            if (dtResult != null && dtResult.Rows.Count > 0) {
                ctlUserLongInfo.HEMODIALYSIS_ID = dtResult.Rows[0]["HEMODIALYSIS_ID"].ToString();
                ctlUserLongInfo.LoadPatientInfo();
                chkCLEAN.Checked = SetCheckValue(dtResult.Rows[0]["CLEAN"].ToString());
                chkRED_HOT.Checked = SetCheckValue(dtResult.Rows[0]["RED_HOT"].ToString());
                chkSWOLLEN_PAIN.Checked = SetCheckValue(dtResult.Rows[0]["SWOLLEN_PAIN"].ToString());
                chkECCHYMOSIS.Checked = SetCheckValue(dtResult.Rows[0]["ECCHYMOSIS"].ToString());
                rgpchkTREMOR.EditValue = dtResult.Rows[0]["TREMOR"].ToString();
                rgpNOISE.EditValue = dtResult.Rows[0]["NOISE"].ToString();
                rgbVASCULAR_ELASTICITY.EditValue = dtResult.Rows[0]["VASCULAR_ELASTICITY"].ToString();
                txtVASCULAR_OTHER.Text = dtResult.Rows[0]["VASCULAR_OTHER"].ToString();
                chkFLOW_BETTER.Checked = SetCheckValue(dtResult.Rows[0]["FLOW_BETTER"].ToString());
                chkSUCTION.Checked = SetCheckValue(dtResult.Rows[0]["SUCTION"].ToString());
                chkMOVEMENT_REVERSAL.Checked = SetCheckValue(dtResult.Rows[0]["MOVEMENT_REVERSAL"].ToString());
                txtPUNCTURE_DISTANCE.Text = dtResult.Rows[0]["PUNCTURE_DISTANCE"].ToString();
                chkFISTULA_SPACING.Checked = SetCheckValue(dtResult.Rows[0]["FISTULA_SPACING"].ToString());
                rgprgpPUNCTURE_DIRECTION.EditValue = dtResult.Rows[0]["PUNCTURE_DIRECTION"].ToString();
                chkWOUND_ALLERGY.Checked = SetCheckValue(dtResult.Rows[0]["WOUND_ALLERGY"].ToString());
                chkPLASTER_ALLERGY.Checked = SetCheckValue(dtResult.Rows[0]["PLASTER_ALLERGY"].ToString());
                txtREMARK.EditValue = dtResult.Rows[0]["REMARK"].ToString();
                txtCREATE_DATE.EditValue = Utility.CDate(dtResult.Rows[0]["CREATE_DATE"].ToString());
                cmbCHECK_USER_ID.EditValue = dtResult.Rows[0]["ASSESSMENT_PEOPLE"].ToString();
                
            }
        }

        private void SaveInfo() {
            HemodialysisModel.MED_ESTIMATE_IN_BASKETDataTable dtResult = new HemodialysisModel.MED_ESTIMATE_IN_BASKETDataTable();
            HemodialysisModel.MED_ESTIMATE_IN_BASKETRow drNew = null;
            if (ID.Length == 0) {
                drNew = dtResult.NewMED_ESTIMATE_IN_BASKETRow();
                drNew.HEMODIALYSIS_ID = ctlUserLongInfo.HEMODIALYSIS_ID;
            }
            else {
                dtResult = objHemodialysisService.GetEsimateInBasketByID(ID);
                drNew = dtResult.Rows[0] as HemodialysisModel.MED_ESTIMATE_IN_BASKETRow;
            }
            drNew.ID = System.Guid.NewGuid().ToString();
            drNew.CLEAN = GetCheckValue(chkCLEAN.Checked);
            drNew.RED_HOT = GetCheckValue(chkRED_HOT.Checked);
            drNew.SWOLLEN_PAIN = GetCheckValue(chkSWOLLEN_PAIN.Checked);
            drNew.ECCHYMOSIS = GetCheckValue(chkECCHYMOSIS.Checked);
            drNew.TREMOR = rgpchkTREMOR.EditValue.ToString();
            drNew.NOISE = rgpNOISE.EditValue.ToString();
            drNew.VASCULAR_ELASTICITY = rgbVASCULAR_ELASTICITY.EditValue.ToString();
            drNew.VASCULAR_OTHER = txtVASCULAR_OTHER.Text;
            drNew.FLOW_BETTER = GetCheckValue(chkFLOW_BETTER.Checked);
            drNew.SUCTION = GetCheckValue(chkSUCTION.Checked);
            drNew.MOVEMENT_REVERSAL = GetCheckValue(chkMOVEMENT_REVERSAL.Checked);            
            drNew.PUNCTURE_DISTANCE = txtPUNCTURE_DISTANCE.EditValue.ToString();
            drNew.FISTULA_SPACING = GetCheckValue(chkFISTULA_SPACING.Checked);
            drNew.PUNCTURE_DIRECTION = rgprgpPUNCTURE_DIRECTION.EditValue.ToString();
            drNew.WOUND_ALLERGY = GetCheckValue(chkWOUND_ALLERGY.Checked);
            drNew.PLASTER_ALLERGY = GetCheckValue(chkPLASTER_ALLERGY.Checked);
            drNew.REMARK = txtREMARK.Text;
            drNew.IS_DELETE = "0";
            drNew.CREATE_DATE = Utility.CDate(txtCREATE_DATE.EditValue.ToString());
            drNew.ASSESSMENT_PEOPLE = cmbCHECK_USER_ID.EditValue.ToString();
           

            if (ID.Length == 0) {
                dtResult.Rows.Add(drNew);
            }

            if (dtResult != null && dtResult.Rows.Count > 0) {
                int result = objHemodialysisService.SaveEstimateInBasketInfo(dtResult);
                if (result != 0) {
                    XtraMessageBox.Show("内瘘评估记录保存成功！");
                    this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                    this.Close();
                }
            }
        }


        /// <summary>
        /// 根据复选框状态返回值
        /// </summary>
        /// <param name="pCheckValue">复选框状态</param>
        /// <returns></returns>
        private string GetCheckValue(bool pCheckValue) {
            string result = string.Empty;
            if (pCheckValue) {
                result = "1";
            }
            else {
                result = "0";
            }
            return result;
        }

        /// <summary>
        /// 根据值设置复选框状态
        /// </summary>
        /// <param name="pValue">设置值</param>
        /// <returns></returns>
        private bool SetCheckValue(string pValue) {
            bool result;
            if (pValue == "1") {
                result = true;
            }
            else {
                result = false;
            }
            return result;
        }

        private void btnSave_Click(object sender, EventArgs e) {
            SaveInfo();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void EditEstimateInBasket_Load(object sender, EventArgs e) {
            loadInfo(ID);
        }
        #endregion
    }
}