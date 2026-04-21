/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：健康宣教 
// 创建时间：2013-12-30
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
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
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using DevExpress.XtraEditors.Controls;
using Hemo.IService;
using Hemo.IService.Dict;
using Hemo.Client.Core;
using Hemo.IService.Config;
using Hemo.IService.PatientSchedule;

namespace Hemo.Client.UI.Hemodialysis {
    public partial class EditHealthEducation : HemoBaseFrm {

        #region 初始化
        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private IPatientSchedule patientScheduleService = ServiceManager.Instance.PatientSchedule;

        public EditHealthEducation() {
            InitializeComponent();
            this.ctlUserLongInfo1.patientInfoCheck1.patientPickEvent += new EventHandler(patientInfoCheck1_patientPickEvent);
            loadDropDownList();
            loadHealthInfo();
        }

        void patientInfoCheck1_patientPickEvent(object sender, EventArgs e) {
            if (this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable.Rows.Count > 0) {
                this._hemodialysisID = this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable[0].HEMODIALYSIS_ID;
            }
            loadHealthInfo();
            LoadData(HEMODIALYSIS_ID,ID);
        }

        #endregion

        #region 属性
        /// <summary>
        /// 透析号
        /// </summary>
        private string _hemodialysisID = string.Empty;
        private string _id = string.Empty;

        public string HEMODIALYSIS_ID {
            set {
                _hemodialysisID = value;
            }
            get {
                return _hemodialysisID;
            }
        }
        public string ID
        {
            set {
                _id = value;
            }
            get {
                return _id;
            }
        }

        #endregion

        #region 事件
        private void btnSave_Click(object sender, EventArgs e) {
            int tabIndex = tabs.SelectedTabPageIndex;
            SaveData(tabIndex);
        }

        private void EditHealthEducation_Load(object sender, EventArgs e) {
            LoadData(HEMODIALYSIS_ID,ID);
            this.ctlUserLongInfo1.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void chkHEALTH_WRITTEN_2_CheckedChanged(object sender, EventArgs e) {

        }

        private void btnPrint_Click(object sender, EventArgs e) {
            ShowPrintCureInfo frm = new ShowPrintCureInfo();
            frm.LoadHealthEducation(HEMODIALYSIS_ID,ID);
            frm.ShowDialog();
        }
        #endregion

        #region 方法

        private void groupControl1_Paint(object sender, PaintEventArgs e) {

        }

        private void loadHealthInfo() {
            ctlUserLongInfo1.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
        }

        private void loadDropDownList() {
            DataTable dtStaffSict = _staffDictService.GetStaffDictList();
            if (dtStaffSict != null && dtStaffSict.Rows.Count > 0) {
                DataTable dtPunctureNurseList = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'", "name");
                if (dtPunctureNurseList != null && dtPunctureNurseList.Rows.Count > 0) {
                    BaseControlInfo.BindLookUpEdit(txtHEALTH_NURSE_ID_1, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "健教护士");
                    BaseControlInfo.BindLookUpEdit(txtHEALTH_HEADMAN_ID_1, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "组长签名");
                    BaseControlInfo.BindLookUpEdit(txtHEALTH_NURSE_ID_2, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "健教护士");
                    BaseControlInfo.BindLookUpEdit(txtHEALTH_HEADMAN_ID_2, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "组长签名");
                    BaseControlInfo.BindLookUpEdit(txtHEALTH_NURSE_ID_3, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "健教护士");
                    BaseControlInfo.BindLookUpEdit(txtHEALTH_HEADMAN_ID_3, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "组长签名");
                    BaseControlInfo.BindLookUpEdit(txtHEALTH_NURSE_ID_4, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "健教护士");
                    BaseControlInfo.BindLookUpEdit(txtHEALTH_HEADMAN_ID_4, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "组长签名");
                    BaseControlInfo.BindLookUpEdit(txtHEALTH_NURSE_ID_5, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "健教护士");
                    BaseControlInfo.BindLookUpEdit(txtHEALTH_HEADMAN_ID_5, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "组长签名");
                    BaseControlInfo.BindLookUpEdit(txtHEALTH_HEADNURSE_ID, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "护士长签名");
                }
            }
            DateTime date = Utility.CDate(patientScheduleService.GetServerDate());
            DateTime startWeek = Utility.GetMonday(date).AddDays(0).Date;

            txtHEALTH_NURSE_DATE_1.EditValue = txtHEALTH_NURSE_DATE_2.EditValue = txtHEALTH_NURSE_DATE_3.EditValue = txtHEALTH_NURSE_DATE_4.EditValue =
                txtHEALTH_NURSE_DATE_5.EditValue = startWeek;
            // txtHEALTH_HEADMAN_DATE_1.EditValue = txtHEALTH_HEADMAN_DATE_2.EditValue = txtHEALTH_HEADMAN_DATE_3.EditValue = txtHEALTH_HEADMAN_DATE_4.EditValue = txtHEALTH_HEADMAN_DATE_5.EditValue = startWeek;

        }


        /// <summary>
        /// 数据保存
        /// </summary>
        /// <param name="pTabIndex"></param>
        private void SaveData(int pTabIndex) {
            HemodialysisModel.MED_HEALTH_EDUCATIONDataTable oldData = objHemodialysisService.GetHealthEducationByHemoIdAndId(HEMODIALYSIS_ID,ID);
            int dCount = oldData.Rows.Count;

            //新增
            if (dCount == 0) {
                HemodialysisModel.MED_HEALTH_EDUCATIONDataTable dataTable = new HemodialysisModel.MED_HEALTH_EDUCATIONDataTable();
                HemodialysisModel.MED_HEALTH_EDUCATIONRow row = dataTable.NewRow() as HemodialysisModel.MED_HEALTH_EDUCATIONRow;
                var rowID = System.Guid.NewGuid().ToString();
                #region 入室宣教首诊

                #region 入室宣教第一行
                row.HEALTH_ID = System.Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                row.HEALTH_TYPE = "IN_ROMM_1_1";
                if (txtHEALTH_NURSE_DATE_1.EditValue != null) {
                    row.HEALTH_NURSE_DATE = Utility.CDate(txtHEALTH_NURSE_DATE_1.EditValue.ToString());
                }

                if (txtHEALTH_HEADMAN_ID_1.EditValue != null) {
                    row.HEALTH_HEADMAN_ID = txtHEALTH_HEADMAN_ID_1.EditValue.ToString();
                }
                row.HEALTH_VERBAL = setTrueFalseToString(chkHEALTH_VERBAL_1.Checked);
                row.HEALTH_WRITTEN = setTrueFalseToString(chkHEALTH_WRITTEN_1.Checked);
                row.HEALTH_APPRAISE = cmbHEALTH_APPRAISE_1.Text;
                row.HEALTH_HEADMAN_APPRAISE = cmbHEALTH_HEADMAN_APPRAISE_1.Text;
                if (txtHEALTH_NURSE_ID_1.EditValue != null) {
                    row.HEALTH_NURSE_ID = txtHEALTH_NURSE_ID_1.EditValue.ToString();
                }
                if (txtHEALTH_HEADMAN_DATE_1.EditValue != null) {
                    row.HEALTH_HEADMAN_DATE = Utility.CDate(txtHEALTH_HEADMAN_DATE_1.EditValue.ToString());
                }
                if (txtHEALTH_HEADNURSE_ID.EditValue != null) {
                    row.HEALTH_HEADNURSE_ID = txtHEALTH_HEADNURSE_ID.EditValue.ToString();
                }
                row.ORDER_NUM = 1;
                row.CREATE_DATE = System.DateTime.Now; //
                row.ID = rowID;
                dataTable.Rows.Add(row);
                #endregion

                #region 入室宣教第二行
                row = dataTable.NewRow() as HemodialysisModel.MED_HEALTH_EDUCATIONRow;
                row.HEALTH_ID = System.Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                row.HEALTH_TYPE = "IN_ROMM_1_2";
                row.HEALTH_VERBAL = setTrueFalseToString(chkHEALTH_VERBAL_2.Checked);
                row.HEALTH_WRITTEN = setTrueFalseToString(chkHEALTH_WRITTEN_2.Checked);
                row.HEALTH_APPRAISE = cmbHEALTH_APPRAISE_2.Text;
                row.HEALTH_HEADMAN_APPRAISE = cmbHEALTH_HEADMAN_APPRAISE_2.Text;
                row.ORDER_NUM = 2;
                row.CREATE_DATE = System.DateTime.Now; //
                row.ID = rowID;
                dataTable.Rows.Add(row);
                #endregion

                #region 入室宣教第三行
                row = dataTable.NewRow() as HemodialysisModel.MED_HEALTH_EDUCATIONRow;
                row.HEALTH_ID = System.Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                row.HEALTH_TYPE = "IN_ROMM_1_3";
                row.HEALTH_VERBAL = setTrueFalseToString(chkHEALTH_VERBAL_3.Checked);
                row.HEALTH_WRITTEN = setTrueFalseToString(chkHEALTH_WRITTEN_3.Checked);
                row.HEALTH_APPRAISE = cmbHEALTH_APPRAISE_3.Text;
                row.HEALTH_HEADMAN_APPRAISE = cmbHEALTH_HEADMAN_APPRAISE_3.Text;
                row.ORDER_NUM = 3;
                row.CREATE_DATE = System.DateTime.Now; //
                row.ID = rowID;
                dataTable.Rows.Add(row);
                #endregion

                #region 入室宣教第四行
                row = dataTable.NewRow() as HemodialysisModel.MED_HEALTH_EDUCATIONRow;
                row.HEALTH_ID = System.Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                row.HEALTH_TYPE = "IN_ROMM_1_4";
                row.HEALTH_VERBAL = setTrueFalseToString(chkHEALTH_VERBAL_4.Checked);
                row.HEALTH_WRITTEN = setTrueFalseToString(chkHEALTH_WRITTEN_4.Checked);
                row.HEALTH_APPRAISE = cmbHEALTH_APPRAISE_4.Text;
                row.HEALTH_HEADMAN_APPRAISE = cmbHEALTH_HEADMAN_APPRAISE_4.Text;
                row.ORDER_NUM = 4;
                row.CREATE_DATE = System.DateTime.Now; //
                row.ID = rowID;
                dataTable.Rows.Add(row);
                #endregion

                #endregion

                #region 疾病知识

                #region 疾病知识第一行
                row = dataTable.NewRow() as HemodialysisModel.MED_HEALTH_EDUCATIONRow;
                row.HEALTH_ID = System.Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                row.HEALTH_TYPE = "Knowledge_Of_Disease_2_1";
                if (txtHEALTH_NURSE_DATE_2.EditValue != null) {
                    row.HEALTH_NURSE_DATE = Utility.CDate(txtHEALTH_NURSE_DATE_2.EditValue.ToString());
                }

                if (txtHEALTH_HEADMAN_ID_2.EditValue != null) {
                    row.HEALTH_HEADMAN_ID = txtHEALTH_HEADMAN_ID_2.EditValue.ToString();
                }

                row.HEALTH_VERBAL = setTrueFalseToString(chkHEALTH_VERBAL_2_1.Checked);
                row.HEALTH_WRITTEN = setTrueFalseToString(chkHEALTH_WRITTEN_2_1.Checked);
                row.HEALTH_APPRAISE = cmbHEALTH_APPRAISE_2_1.Text;
                row.HEALTH_HEADMAN_APPRAISE = cmbHEALTH_HEADMAN_APPRAISE_2_1.Text;
                if (txtHEALTH_NURSE_ID_2.EditValue != null) {
                    row.HEALTH_NURSE_ID = txtHEALTH_NURSE_ID_2.EditValue.ToString();
                }
                if (txtHEALTH_HEADMAN_DATE_2.EditValue != null) {
                    row.HEALTH_HEADMAN_DATE = Utility.CDate(txtHEALTH_HEADMAN_DATE_2.EditValue.ToString());
                }
                row.ORDER_NUM = 5;
                row.CREATE_DATE = System.DateTime.Now; //
                row.ID = rowID;
                dataTable.Rows.Add(row);
                #endregion

                #region 疾病知识第二行
                row = dataTable.NewRow() as HemodialysisModel.MED_HEALTH_EDUCATIONRow;
                row.HEALTH_ID = System.Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                row.HEALTH_TYPE = "Knowledge_Of_Disease_2_2";
                row.HEALTH_VERBAL = setTrueFalseToString(chkHEALTH_VERBAL_2_1.Checked);
                row.HEALTH_WRITTEN = setTrueFalseToString(chkHEALTH_WRITTEN_2_1.Checked);
                row.HEALTH_APPRAISE = cmbHEALTH_APPRAISE_2_1.Text;
                row.HEALTH_HEADMAN_APPRAISE = cmbHEALTH_HEADMAN_APPRAISE_2_1.Text;
                row.ORDER_NUM = 6;
                row.CREATE_DATE = System.DateTime.Now; //
                row.ID = rowID;
                dataTable.Rows.Add(row);
                #endregion

                #region 疾病知识第三行
                row = dataTable.NewRow() as HemodialysisModel.MED_HEALTH_EDUCATIONRow;
                row.HEALTH_ID = System.Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                row.HEALTH_TYPE = "Knowledge_Of_Disease_2_3";
                row.HEALTH_VERBAL = setTrueFalseToString(chkHEALTH_VERBAL_2_3.Checked);
                row.HEALTH_WRITTEN = setTrueFalseToString(chkHEALTH_WRITTEN_2_3.Checked);
                row.HEALTH_APPRAISE = cmbHEALTH_APPRAISE_2_3.Text;
                row.HEALTH_HEADMAN_APPRAISE = cmbHEALTH_HEADMAN_APPRAISE_2_3.Text;
                row.ORDER_NUM = 7;
                row.CREATE_DATE = System.DateTime.Now; //
                row.ID = rowID;
                dataTable.Rows.Add(row);
                #endregion

                #region 疾病知识第四行
                row = dataTable.NewRow() as HemodialysisModel.MED_HEALTH_EDUCATIONRow;
                row.HEALTH_ID = System.Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                row.HEALTH_TYPE = "Knowledge_Of_Disease_2_4";
                row.HEALTH_VERBAL = setTrueFalseToString(chkHEALTH_VERBAL_2_4.Checked);
                row.HEALTH_WRITTEN = setTrueFalseToString(chkHEALTH_WRITTEN_2_4.Checked);
                row.HEALTH_APPRAISE = cmbHEALTH_APPRAISE_2_4.Text;
                row.HEALTH_HEADMAN_APPRAISE = cmbHEALTH_HEADMAN_APPRAISE_2_4.Text;
                row.ORDER_NUM = 8;
                row.CREATE_DATE = System.DateTime.Now; //
                row.ID = rowID;
                dataTable.Rows.Add(row);
                #endregion

                #region 疾病知识第五行
                row = dataTable.NewRow() as HemodialysisModel.MED_HEALTH_EDUCATIONRow;
                row.HEALTH_ID = System.Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                row.HEALTH_TYPE = "Knowledge_Of_Disease_2_5";
                row.HEALTH_VERBAL = setTrueFalseToString(chkHEALTH_VERBAL_2_5.Checked);
                row.HEALTH_WRITTEN = setTrueFalseToString(chkHEALTH_WRITTEN_2_5.Checked);
                row.HEALTH_APPRAISE = cmbHEALTH_APPRAISE_2_5.Text;
                row.HEALTH_HEADMAN_APPRAISE = cmbHEALTH_HEADMAN_APPRAISE_2_5.Text;
                row.ORDER_NUM = 9;
                row.CREATE_DATE = System.DateTime.Now; //
                row.ID = rowID;
                dataTable.Rows.Add(row);
                #endregion

                #endregion

                #region 心里护理
                #region 心里护理第一行
                row = dataTable.NewRow() as HemodialysisModel.MED_HEALTH_EDUCATIONRow;
                row.HEALTH_ID = System.Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                row.HEALTH_TYPE = "Heart_Care_3_1";
                if (txtHEALTH_NURSE_DATE_3.EditValue != null) {
                    row.HEALTH_NURSE_DATE = Utility.CDate(txtHEALTH_NURSE_DATE_3.EditValue.ToString());
                }

                if (txtHEALTH_HEADMAN_ID_3.EditValue != null) {
                    row.HEALTH_HEADMAN_ID = txtHEALTH_HEADMAN_ID_3.EditValue.ToString();
                }

                row.HEALTH_VERBAL = setTrueFalseToString(chkHEALTH_VERBAL_3_1.Checked);
                row.HEALTH_WRITTEN = setTrueFalseToString(chkHEALTH_WRITTEN_3_1.Checked);
                row.HEALTH_APPRAISE = cmbHEALTH_APPRAISE_3_1.Text;
                row.HEALTH_HEADMAN_APPRAISE = cmbHEALTH_HEADMAN_APPRAISE_3_1.Text;
                if (txtHEALTH_NURSE_ID_3.EditValue != null) {
                    row.HEALTH_NURSE_ID = txtHEALTH_NURSE_ID_3.EditValue.ToString();
                }
                if (txtHEALTH_HEADMAN_DATE_3.EditValue != null) {
                    row.HEALTH_HEADMAN_DATE = Utility.CDate(txtHEALTH_HEADMAN_DATE_3.EditValue.ToString());
                }
                row.ORDER_NUM = 10;
                row.CREATE_DATE = System.DateTime.Now; //
                row.ID = rowID;
                dataTable.Rows.Add(row);
                #endregion

                #region 心里护理第二行
                row = dataTable.NewRow() as HemodialysisModel.MED_HEALTH_EDUCATIONRow;
                row.HEALTH_ID = System.Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                row.HEALTH_TYPE = "Heart_Care_3_2";
                row.HEALTH_VERBAL = setTrueFalseToString(chkHEALTH_VERBAL_3_2.Checked);
                row.HEALTH_WRITTEN = setTrueFalseToString(chkHEALTH_WRITTEN_3_2.Checked);
                row.HEALTH_APPRAISE = cmbHEALTH_APPRAISE_3_2.Text;
                row.HEALTH_HEADMAN_APPRAISE = cmbHEALTH_HEADMAN_APPRAISE_3_2.Text;
                row.ORDER_NUM = 11;
                row.CREATE_DATE = System.DateTime.Now; //
                row.ID = rowID;
                dataTable.Rows.Add(row);
                #endregion
                #endregion

                #region 血管通路护理

                #region 血管通路护理第一行
                row = dataTable.NewRow() as HemodialysisModel.MED_HEALTH_EDUCATIONRow;
                row.HEALTH_ID = System.Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                row.HEALTH_TYPE = "VASCULAR_ACCESS_4_1";
                if (txtHEALTH_NURSE_DATE_4.EditValue != null) {
                    row.HEALTH_NURSE_DATE = Utility.CDate(txtHEALTH_NURSE_DATE_4.EditValue.ToString());
                }

                if (txtHEALTH_HEADMAN_ID_4.EditValue != null) {
                    row.HEALTH_HEADMAN_ID = txtHEALTH_HEADMAN_ID_4.EditValue.ToString();
                }

                row.HEALTH_VERBAL = setTrueFalseToString(chkHEALTH_VERBAL_4_1.Checked);
                row.HEALTH_WRITTEN = setTrueFalseToString(chkHEALTH_WRITTEN_4_1.Checked);
                row.HEALTH_APPRAISE = cmbHEALTH_APPRAISE_4_1.Text;
                row.HEALTH_HEADMAN_APPRAISE = cmbHEALTH_HEADMAN_APPRAISE_4_1.Text;
                if (txtHEALTH_NURSE_ID_4.EditValue != null) {
                    row.HEALTH_NURSE_ID = txtHEALTH_NURSE_ID_4.EditValue.ToString();
                }
                if (txtHEALTH_HEADMAN_DATE_4.EditValue != null) {
                    row.HEALTH_HEADMAN_DATE = Utility.CDate(txtHEALTH_HEADMAN_DATE_4.EditValue.ToString());
                }
                row.ORDER_NUM = 12;
                row.CREATE_DATE = System.DateTime.Now; //
                row.ID = rowID;
                dataTable.Rows.Add(row);
                #endregion

                #region 血管通路护理第二行
                row = dataTable.NewRow() as HemodialysisModel.MED_HEALTH_EDUCATIONRow;
                row.HEALTH_ID = System.Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                row.HEALTH_TYPE = "VASCULAR_ACCESS_4_2";
                row.HEALTH_VERBAL = setTrueFalseToString(chkHEALTH_VERBAL_4_2.Checked);
                row.HEALTH_WRITTEN = setTrueFalseToString(chkHEALTH_WRITTEN_4_2.Checked);
                row.HEALTH_APPRAISE = cmbHEALTH_APPRAISE_4_2.Text;
                row.HEALTH_HEADMAN_APPRAISE = cmbHEALTH_HEADMAN_APPRAISE_4_2.Text;
                row.ORDER_NUM = 13;
                row.CREATE_DATE = System.DateTime.Now; //
                row.ID = rowID;
                dataTable.Rows.Add(row);
                #endregion

                #region 血管通路护理第三行
                row = dataTable.NewRow() as HemodialysisModel.MED_HEALTH_EDUCATIONRow;
                row.HEALTH_ID = System.Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                row.HEALTH_TYPE = "VASCULAR_ACCESS_4_3";
                row.HEALTH_VERBAL = setTrueFalseToString(chkHEALTH_VERBAL_4_3.Checked);
                row.HEALTH_WRITTEN = setTrueFalseToString(chkHEALTH_WRITTEN_4_3.Checked);
                row.HEALTH_APPRAISE = cmbHEALTH_APPRAISE_4_3.Text;
                row.HEALTH_HEADMAN_APPRAISE = cmbHEALTH_HEADMAN_APPRAISE_4_3.Text;
                row.ORDER_NUM = 14;
                row.CREATE_DATE = System.DateTime.Now; //
                row.ID = rowID;
                dataTable.Rows.Add(row);
                #endregion

                #endregion

                #region 规律血透指导

                #region 规律血透指导第一行
                row = dataTable.NewRow() as HemodialysisModel.MED_HEALTH_EDUCATIONRow;
                row.HEALTH_ID = System.Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                row.HEALTH_TYPE = "Hemo_Guide_5_1";
                if (txtHEALTH_NURSE_DATE_5.EditValue != null) {
                    row.HEALTH_NURSE_DATE = Utility.CDate(txtHEALTH_NURSE_DATE_5.EditValue.ToString());
                }
                row.HEALTH_VERBAL = setTrueFalseToString(chkHEALTH_VERBAL_5_1.Checked);
                row.HEALTH_WRITTEN = setTrueFalseToString(chkHEALTH_WRITTEN_5_1.Checked);
                row.HEALTH_APPRAISE = cmbHEALTH_APPRAISE_5_1.Text;
                row.HEALTH_HEADMAN_APPRAISE = cmbHEALTH_HEADMAN_APPRAISE_5_1.Text;
                if (txtHEALTH_NURSE_ID_5.EditValue != null) {
                    row.HEALTH_NURSE_ID = txtHEALTH_NURSE_ID_5.EditValue.ToString();
                }

                if (txtHEALTH_HEADMAN_ID_5.EditValue != null) {
                    row.HEALTH_HEADMAN_ID = txtHEALTH_HEADMAN_ID_5.EditValue.ToString();
                }

                if (txtHEALTH_HEADMAN_DATE_5.EditValue != null) {
                    row.HEALTH_HEADMAN_DATE = Utility.CDate(txtHEALTH_HEADMAN_DATE_5.EditValue.ToString());
                }
                row.CREATE_DATE = System.DateTime.Now;
                row.ID = rowID;
                row.ORDER_NUM = 15;
                dataTable.Rows.Add(row);
                #endregion

                #region 规律血透指导第二行
                row = dataTable.NewRow() as HemodialysisModel.MED_HEALTH_EDUCATIONRow;
                row.HEALTH_ID = System.Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                row.HEALTH_TYPE = "Hemo_Guide_5_2";
                row.HEALTH_VERBAL = setTrueFalseToString(chkHEALTH_VERBAL_5_2.Checked);
                row.HEALTH_WRITTEN = setTrueFalseToString(chkHEALTH_WRITTEN_5_2.Checked);
                row.HEALTH_APPRAISE = cmbHEALTH_APPRAISE_5_2.Text;
                row.HEALTH_HEADMAN_APPRAISE = cmbHEALTH_HEADMAN_APPRAISE_5_2.Text;
                row.ORDER_NUM = 16;
                row.CREATE_DATE = System.DateTime.Now;
                row.ID = rowID;
                dataTable.Rows.Add(row);
                #endregion

                #region 规律血透指导第三行
                row = dataTable.NewRow() as HemodialysisModel.MED_HEALTH_EDUCATIONRow;
                row.HEALTH_ID = System.Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                row.HEALTH_TYPE = "Hemo_Guide_5_3";
                row.HEALTH_VERBAL = setTrueFalseToString(chkHEALTH_VERBAL_5_3.Checked);
                row.HEALTH_WRITTEN = setTrueFalseToString(chkHEALTH_WRITTEN_5_3.Checked);
                row.HEALTH_APPRAISE = cmbHEALTH_APPRAISE_5_3.Text;
                row.HEALTH_HEADMAN_APPRAISE = cmbHEALTH_HEADMAN_APPRAISE_5_3.Text;
                row.ORDER_NUM = 17;
                row.CREATE_DATE = System.DateTime.Now;
                row.ID = rowID;
                dataTable.Rows.Add(row);
                #endregion

                #region 规律血透指导第四行
                row = dataTable.NewRow() as HemodialysisModel.MED_HEALTH_EDUCATIONRow;
                row.HEALTH_ID = System.Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                row.HEALTH_TYPE = "Hemo_Guide_5_4";
                row.HEALTH_VERBAL = setTrueFalseToString(chkHEALTH_VERBAL_5_4.Checked);
                row.HEALTH_WRITTEN = setTrueFalseToString(chkHEALTH_WRITTEN_5_4.Checked);
                row.HEALTH_APPRAISE = cmbHEALTH_APPRAISE_5_4.Text;
                row.HEALTH_HEADMAN_APPRAISE = cmbHEALTH_HEADMAN_APPRAISE_5_4.Text;
                row.ORDER_NUM = 18;
                row.CREATE_DATE = System.DateTime.Now;
                row.ID = rowID;
                dataTable.Rows.Add(row);
                #endregion

                #region 规律血透指导第五行
                row = dataTable.NewRow() as HemodialysisModel.MED_HEALTH_EDUCATIONRow;
                row.HEALTH_ID = System.Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                row.HEALTH_TYPE = "Hemo_Guide_5_5";
                row.HEALTH_VERBAL = setTrueFalseToString(chkHEALTH_VERBAL_5_5.Checked);
                row.HEALTH_WRITTEN = setTrueFalseToString(chkHEALTH_WRITTEN_5_5.Checked);
                row.HEALTH_APPRAISE = cmbHEALTH_APPRAISE_5_5.Text;
                row.HEALTH_HEADMAN_APPRAISE = cmbHEALTH_HEADMAN_APPRAISE_5_5.Text;
                row.ORDER_NUM = 19;
                row.CREATE_DATE = System.DateTime.Now;
                row.ID = rowID;
                dataTable.Rows.Add(row);
                #endregion

                #region 规律血透指导第六行
                row = dataTable.NewRow() as HemodialysisModel.MED_HEALTH_EDUCATIONRow;
                row.HEALTH_ID = System.Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                row.HEALTH_TYPE = "Hemo_Guide_5_6";
                row.HEALTH_VERBAL = setTrueFalseToString(chkHEALTH_VERBAL_5_6.Checked);
                row.HEALTH_WRITTEN = setTrueFalseToString(chkHEALTH_WRITTEN_5_6.Checked);
                row.HEALTH_APPRAISE = cmbHEALTH_APPRAISE_5_6.Text;
                row.HEALTH_HEADMAN_APPRAISE = cmbHEALTH_HEADMAN_APPRAISE_5_6.Text;
                row.ORDER_NUM = 20;
                row.CREATE_DATE = System.DateTime.Now;
                row.ID = rowID;
                dataTable.Rows.Add(row);
                #endregion

                #endregion

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    ID = rowID; //修复直接打印的BUG
                    if (objHemodialysisService.SaveHealthEducationInfo(dataTable) > 0)
                    {
                        AutoClosedMsgBox.ShowForm("数据保存成功！", "健康宣教", 1000, MessageBoxIcon.Warning);
                    }
                }
            }
            //修改
            else {
                if (oldData != null && oldData.Rows.Count > 0) {
                    DataTable dtTemp = oldData as DataTable;
                    #region 入室宣教
                    #region 入室宣教第一行
                    dtTemp = Utility.GetSubTable(oldData, "HEALTH_TYPE='IN_ROMM_1_1'");
                    if (dtTemp != null && dtTemp.Rows.Count > 0) {
                        oldData.Rows[0]["HEALTH_VERBAL"] = setTrueFalseToString(chkHEALTH_VERBAL_1.Checked);
                        oldData.Rows[0]["HEALTH_WRITTEN"] = setTrueFalseToString(chkHEALTH_WRITTEN_1.Checked);
                        oldData.Rows[0]["HEALTH_APPRAISE"] = cmbHEALTH_APPRAISE_1.Text;
                        oldData.Rows[0]["HEALTH_HEADMAN_APPRAISE"] = cmbHEALTH_HEADMAN_APPRAISE_1.Text;
                        if (txtHEALTH_NURSE_ID_1.EditValue != null) {
                            oldData.Rows[0]["HEALTH_NURSE_ID"] = txtHEALTH_NURSE_ID_1.EditValue.ToString();
                        }

                        if (txtHEALTH_HEADMAN_ID_1.EditValue != null) {
                            oldData.Rows[0]["HEALTH_HEADMAN_ID"] = txtHEALTH_HEADMAN_ID_1.EditValue.ToString();
                        }

                        if (txtHEALTH_NURSE_DATE_1.EditValue != null) {
                            oldData.Rows[0]["HEALTH_NURSE_DATE"] = Utility.CDate(txtHEALTH_NURSE_DATE_1.EditValue.ToString());
                        }
                        if (txtHEALTH_HEADMAN_DATE_1.EditValue != null) {
                            oldData.Rows[0]["HEALTH_HEADMAN_DATE"] = Utility.CDate(txtHEALTH_HEADMAN_DATE_1.EditValue.ToString());
                        }
                        if (txtHEALTH_HEADNURSE_ID.EditValue != null) {
                            oldData.Rows[0]["HEALTH_HEADNURSE_ID"] = txtHEALTH_HEADNURSE_ID.EditValue.ToString();
                        }
                    }
                    #endregion

                    #region 入室宣教第二行
                    dtTemp = Utility.GetSubTable(oldData, "HEALTH_TYPE='IN_ROMM_1_2'");
                    if (dtTemp != null && dtTemp.Rows.Count > 0) {
                        oldData.Rows[1]["HEALTH_VERBAL"] = setTrueFalseToString(chkHEALTH_VERBAL_2.Checked);
                        oldData.Rows[1]["HEALTH_WRITTEN"] = setTrueFalseToString(chkHEALTH_WRITTEN_2.Checked);
                        oldData.Rows[1]["HEALTH_APPRAISE"] = cmbHEALTH_APPRAISE_2.Text;
                        oldData.Rows[1]["HEALTH_HEADMAN_APPRAISE"] = cmbHEALTH_HEADMAN_APPRAISE_2.Text;
                    }
                    #endregion

                    #region 入室宣教第三行
                    dtTemp = Utility.GetSubTable(oldData, "HEALTH_TYPE='IN_ROMM_1_3'");
                    if (dtTemp != null && dtTemp.Rows.Count > 0) {
                        oldData.Rows[2]["HEALTH_VERBAL"] = setTrueFalseToString(chkHEALTH_VERBAL_3.Checked);
                        oldData.Rows[2]["HEALTH_WRITTEN"] = setTrueFalseToString(chkHEALTH_WRITTEN_3.Checked);
                        oldData.Rows[2]["HEALTH_APPRAISE"] = cmbHEALTH_APPRAISE_3.Text;
                        oldData.Rows[2]["HEALTH_HEADMAN_APPRAISE"] = cmbHEALTH_HEADMAN_APPRAISE_3.Text;
                    }
                    #endregion

                    #region 入室宣教第四行
                    dtTemp = Utility.GetSubTable(oldData, "HEALTH_TYPE='IN_ROMM_1_4'");
                    if (dtTemp != null && dtTemp.Rows.Count > 0) {
                        oldData.Rows[3]["HEALTH_VERBAL"] = setTrueFalseToString(chkHEALTH_VERBAL_4.Checked);
                        oldData.Rows[3]["HEALTH_WRITTEN"] = setTrueFalseToString(chkHEALTH_WRITTEN_4.Checked);
                        oldData.Rows[3]["HEALTH_APPRAISE"] = cmbHEALTH_APPRAISE_4.Text;
                        oldData.Rows[3]["HEALTH_HEADMAN_APPRAISE"] = cmbHEALTH_HEADMAN_APPRAISE_4.Text;
                    }
                    #endregion
                    #endregion

                    #region 疾病知识

                    #region 疾病知识第一行
                    dtTemp = Utility.GetSubTable(oldData, "HEALTH_TYPE='Knowledge_Of_Disease_2_1'");
                    if (dtTemp != null && dtTemp.Rows.Count > 0) {
                        oldData.Rows[4]["HEALTH_VERBAL"] = setTrueFalseToString(chkHEALTH_VERBAL_2_1.Checked);
                        oldData.Rows[4]["HEALTH_WRITTEN"] = setTrueFalseToString(chkHEALTH_WRITTEN_2_1.Checked);
                        oldData.Rows[4]["HEALTH_APPRAISE"] = cmbHEALTH_APPRAISE_2_1.Text;
                        oldData.Rows[4]["HEALTH_HEADMAN_APPRAISE"] = cmbHEALTH_HEADMAN_APPRAISE_2_1.Text;
                        if (txtHEALTH_NURSE_ID_2.EditValue != null) {
                            oldData.Rows[4]["HEALTH_NURSE_ID"] = txtHEALTH_NURSE_ID_2.EditValue.ToString();
                        }
                        if (txtHEALTH_NURSE_DATE_2.EditValue != null) {
                            oldData.Rows[4]["HEALTH_NURSE_DATE"] = Utility.CDate(txtHEALTH_NURSE_DATE_2.EditValue.ToString());
                        }
                        if (txtHEALTH_HEADMAN_DATE_2.EditValue != null) {
                            oldData.Rows[4]["HEALTH_HEADMAN_DATE"] = Utility.CDate(txtHEALTH_HEADMAN_DATE_2.EditValue.ToString());
                        }

                        if (txtHEALTH_HEADMAN_ID_2.EditValue != null) {
                            oldData.Rows[4]["HEALTH_HEADMAN_ID"] = txtHEALTH_HEADMAN_ID_2.EditValue.ToString();
                        }
                    }
                    #endregion

                    #region 疾病知识第二行
                    dtTemp = Utility.GetSubTable(oldData, "HEALTH_TYPE='Knowledge_Of_Disease_2_2'");
                    if (dtTemp != null && dtTemp.Rows.Count > 0) {
                        oldData.Rows[5]["HEALTH_VERBAL"] = setTrueFalseToString(chkHEALTH_VERBAL_2_2.Checked);
                        oldData.Rows[5]["HEALTH_WRITTEN"] = setTrueFalseToString(chkHEALTH_WRITTEN_2_2.Checked);
                        oldData.Rows[5]["HEALTH_APPRAISE"] = cmbHEALTH_APPRAISE_2_2.Text;
                        oldData.Rows[5]["HEALTH_HEADMAN_APPRAISE"] = cmbHEALTH_HEADMAN_APPRAISE_2_2.Text;
                    }
                    #endregion

                    #region 疾病知识第三行
                    dtTemp = Utility.GetSubTable(oldData, "HEALTH_TYPE='Knowledge_Of_Disease_2_3'");
                    if (dtTemp != null && dtTemp.Rows.Count > 0) {
                        oldData.Rows[6]["HEALTH_VERBAL"] = setTrueFalseToString(chkHEALTH_VERBAL_2_3.Checked);
                        oldData.Rows[6]["HEALTH_WRITTEN"] = setTrueFalseToString(chkHEALTH_WRITTEN_2_3.Checked);
                        oldData.Rows[6]["HEALTH_APPRAISE"] = cmbHEALTH_APPRAISE_2_3.Text;
                        oldData.Rows[6]["HEALTH_HEADMAN_APPRAISE"] = cmbHEALTH_HEADMAN_APPRAISE_2_3.Text;
                    }
                    #endregion

                    #region 疾病知识第四行
                    dtTemp = Utility.GetSubTable(oldData, "HEALTH_TYPE='Knowledge_Of_Disease_2_4'");
                    if (dtTemp != null && dtTemp.Rows.Count > 0) {
                        oldData.Rows[7]["HEALTH_VERBAL"] = setTrueFalseToString(chkHEALTH_VERBAL_2_4.Checked);
                        oldData.Rows[7]["HEALTH_WRITTEN"] = setTrueFalseToString(chkHEALTH_WRITTEN_2_4.Checked);
                        oldData.Rows[7]["HEALTH_APPRAISE"] = cmbHEALTH_APPRAISE_2_4.Text;
                        oldData.Rows[7]["HEALTH_HEADMAN_APPRAISE"] = cmbHEALTH_HEADMAN_APPRAISE_2_4.Text;
                    }
                    #endregion

                    #region 疾病知识第五行
                    dtTemp = Utility.GetSubTable(oldData, "HEALTH_TYPE='Knowledge_Of_Disease_2_5'");
                    if (dtTemp != null && dtTemp.Rows.Count > 0) {
                        oldData.Rows[8]["HEALTH_VERBAL"] = setTrueFalseToString(chkHEALTH_VERBAL_2_5.Checked);
                        oldData.Rows[8]["HEALTH_WRITTEN"] = setTrueFalseToString(chkHEALTH_WRITTEN_2_5.Checked);
                        oldData.Rows[8]["HEALTH_APPRAISE"] = cmbHEALTH_APPRAISE_2_5.Text;
                        oldData.Rows[8]["HEALTH_HEADMAN_APPRAISE"] = cmbHEALTH_HEADMAN_APPRAISE_2_5.Text;
                    }
                    #endregion

                    #endregion

                    #region 心里护理
                    #region 心里护理第一行
                    dtTemp = Utility.GetSubTable(oldData, "HEALTH_TYPE='Heart_Care_3_1'");
                    if (dtTemp != null && dtTemp.Rows.Count > 0) {
                        oldData.Rows[9]["HEALTH_VERBAL"] = setTrueFalseToString(chkHEALTH_VERBAL_3_1.Checked);
                        oldData.Rows[9]["HEALTH_WRITTEN"] = setTrueFalseToString(chkHEALTH_WRITTEN_3_1.Checked);
                        oldData.Rows[9]["HEALTH_APPRAISE"] = cmbHEALTH_APPRAISE_3_1.Text;
                        oldData.Rows[9]["HEALTH_HEADMAN_APPRAISE"] = cmbHEALTH_HEADMAN_APPRAISE_3_1.Text;
                        if (txtHEALTH_NURSE_ID_3.EditValue != null) {
                            oldData.Rows[9]["HEALTH_NURSE_ID"] = txtHEALTH_NURSE_ID_3.EditValue.ToString();
                        }
                        if (txtHEALTH_NURSE_DATE_3.EditValue != null) {
                            oldData.Rows[9]["HEALTH_NURSE_DATE"] = Utility.CDate(txtHEALTH_NURSE_DATE_3.EditValue.ToString());
                        }
                        if (txtHEALTH_HEADMAN_DATE_3.EditValue != null) {
                            oldData.Rows[9]["HEALTH_HEADMAN_DATE"] = Utility.CDate(txtHEALTH_HEADMAN_DATE_3.EditValue.ToString());
                        }

                        if (txtHEALTH_HEADMAN_ID_3.EditValue != null) {
                            oldData.Rows[9]["HEALTH_HEADMAN_ID"] = txtHEALTH_HEADMAN_ID_3.EditValue.ToString();
                        }
                    }
                    #endregion

                    #region 心里护理第二行
                    dtTemp = Utility.GetSubTable(oldData, "HEALTH_TYPE='Heart_Care_3_2'");
                    if (dtTemp != null && dtTemp.Rows.Count > 0) {
                        oldData.Rows[10]["HEALTH_VERBAL"] = setTrueFalseToString(chkHEALTH_VERBAL_3_2.Checked);
                        oldData.Rows[10]["HEALTH_WRITTEN"] = setTrueFalseToString(chkHEALTH_WRITTEN_3_2.Checked);
                        oldData.Rows[10]["HEALTH_APPRAISE"] = cmbHEALTH_APPRAISE_3_2.Text;
                        oldData.Rows[10]["HEALTH_HEADMAN_APPRAISE"] = cmbHEALTH_HEADMAN_APPRAISE_3_2.Text;
                    }
                    #endregion
                    #endregion

                    #region 血管通路护理

                    #region 血管通路护理第一行
                    dtTemp = Utility.GetSubTable(oldData, "HEALTH_TYPE='VASCULAR_ACCESS_4_1'");
                    if (dtTemp != null && dtTemp.Rows.Count > 0) {
                        oldData.Rows[11]["HEALTH_VERBAL"] = setTrueFalseToString(chkHEALTH_VERBAL_4_1.Checked);
                        oldData.Rows[11]["HEALTH_WRITTEN"] = setTrueFalseToString(chkHEALTH_WRITTEN_4_1.Checked);
                        oldData.Rows[11]["HEALTH_APPRAISE"] = cmbHEALTH_APPRAISE_4_1.Text;
                        oldData.Rows[11]["HEALTH_HEADMAN_APPRAISE"] = cmbHEALTH_HEADMAN_APPRAISE_4_1.Text;
                        if (txtHEALTH_NURSE_ID_4.EditValue != null) {
                            oldData.Rows[11]["HEALTH_NURSE_ID"] = txtHEALTH_NURSE_ID_4.EditValue.ToString();
                        }
                        if (txtHEALTH_NURSE_DATE_4.EditValue != null) {
                            oldData.Rows[11]["HEALTH_NURSE_DATE"] = Utility.CDate(txtHEALTH_NURSE_DATE_4.EditValue.ToString());
                        }
                        if (txtHEALTH_HEADMAN_DATE_4.EditValue != null) {
                            oldData.Rows[11]["HEALTH_HEADMAN_DATE"] = Utility.CDate(txtHEALTH_HEADMAN_DATE_4.EditValue.ToString());
                        }

                        if (txtHEALTH_HEADMAN_ID_4.EditValue != null) {
                            oldData.Rows[11]["HEALTH_HEADMAN_ID"] = txtHEALTH_HEADMAN_ID_4.EditValue.ToString();
                        }
                    }
                    #endregion

                    #region 血管通路护理第二行
                    dtTemp = Utility.GetSubTable(oldData, "HEALTH_TYPE='VASCULAR_ACCESS_4_2'");
                    if (dtTemp != null && dtTemp.Rows.Count > 0) {
                        oldData.Rows[12]["HEALTH_VERBAL"] = setTrueFalseToString(chkHEALTH_VERBAL_4_2.Checked);
                        oldData.Rows[12]["HEALTH_WRITTEN"] = setTrueFalseToString(chkHEALTH_WRITTEN_4_2.Checked);
                        oldData.Rows[12]["HEALTH_APPRAISE"] = cmbHEALTH_APPRAISE_4_2.Text;
                        oldData.Rows[12]["HEALTH_HEADMAN_APPRAISE"] = cmbHEALTH_HEADMAN_APPRAISE_4_2.Text;
                    }
                    #endregion

                    #region 血管通路护理第三行
                    dtTemp = Utility.GetSubTable(oldData, "HEALTH_TYPE='VASCULAR_ACCESS_4_3'");
                    if (dtTemp != null && dtTemp.Rows.Count > 0) {
                        oldData.Rows[13]["HEALTH_VERBAL"] = setTrueFalseToString(chkHEALTH_VERBAL_4_3.Checked);
                        oldData.Rows[13]["HEALTH_WRITTEN"] = setTrueFalseToString(chkHEALTH_WRITTEN_4_3.Checked);
                        oldData.Rows[13]["HEALTH_APPRAISE"] = cmbHEALTH_APPRAISE_4_3.Text;
                        oldData.Rows[13]["HEALTH_HEADMAN_APPRAISE"] = cmbHEALTH_HEADMAN_APPRAISE_4_3.Text;
                    }
                    #endregion

                    #endregion

                    #region 规律血透指导

                    #region 规律血透指导第一行
                    dtTemp = Utility.GetSubTable(oldData, "HEALTH_TYPE='Hemo_Guide_5_1'");
                    if (dtTemp != null && dtTemp.Rows.Count > 0) {
                        oldData.Rows[14]["HEALTH_VERBAL"] = setTrueFalseToString(chkHEALTH_VERBAL_5_1.Checked);
                        oldData.Rows[14]["HEALTH_WRITTEN"] = setTrueFalseToString(chkHEALTH_WRITTEN_5_1.Checked);
                        oldData.Rows[14]["HEALTH_APPRAISE"] = cmbHEALTH_APPRAISE_5_1.Text;
                        oldData.Rows[14]["HEALTH_HEADMAN_APPRAISE"] = cmbHEALTH_HEADMAN_APPRAISE_5_1.Text;
                        if (txtHEALTH_NURSE_ID_5.EditValue != null) {
                            oldData.Rows[14]["HEALTH_NURSE_ID"] = txtHEALTH_NURSE_ID_5.EditValue.ToString();
                        }
                        if (txtHEALTH_NURSE_DATE_5.EditValue != null) {
                            oldData.Rows[14]["HEALTH_NURSE_DATE"] = Utility.CDate(txtHEALTH_NURSE_DATE_5.EditValue.ToString());
                        }
                        if (txtHEALTH_HEADMAN_DATE_5.EditValue != null) {
                            oldData.Rows[14]["HEALTH_HEADMAN_DATE"] = Utility.CDate(txtHEALTH_HEADMAN_DATE_5.EditValue.ToString());
                        }

                        if (txtHEALTH_HEADMAN_ID_5.EditValue != null) {
                            oldData.Rows[14]["HEALTH_HEADMAN_ID"] = txtHEALTH_HEADMAN_ID_5.EditValue.ToString();
                        }
                    }
                    #endregion

                    #region 规律血透指导第二行
                    dtTemp = Utility.GetSubTable(oldData, "HEALTH_TYPE='Hemo_Guide_5_2'");
                    if (dtTemp != null && dtTemp.Rows.Count > 0) {
                        oldData.Rows[15]["HEALTH_VERBAL"] = setTrueFalseToString(chkHEALTH_VERBAL_5_2.Checked);
                        oldData.Rows[15]["HEALTH_WRITTEN"] = setTrueFalseToString(chkHEALTH_WRITTEN_5_2.Checked);
                        oldData.Rows[15]["HEALTH_APPRAISE"] = cmbHEALTH_APPRAISE_5_2.Text;
                        oldData.Rows[15]["HEALTH_HEADMAN_APPRAISE"] = cmbHEALTH_HEADMAN_APPRAISE_5_2.Text;
                    }
                    #endregion

                    #region 规律血透指导第三行
                    dtTemp = Utility.GetSubTable(oldData, "HEALTH_TYPE='Hemo_Guide_5_3'");
                    if (dtTemp != null && dtTemp.Rows.Count > 0) {
                        oldData.Rows[16]["HEALTH_VERBAL"] = setTrueFalseToString(chkHEALTH_VERBAL_5_3.Checked);
                        oldData.Rows[16]["HEALTH_WRITTEN"] = setTrueFalseToString(chkHEALTH_WRITTEN_5_3.Checked);
                        oldData.Rows[16]["HEALTH_APPRAISE"] = cmbHEALTH_APPRAISE_5_3.Text;
                        oldData.Rows[16]["HEALTH_HEADMAN_APPRAISE"] = cmbHEALTH_HEADMAN_APPRAISE_5_3.Text;
                    }
                    #endregion

                    #region 规律血透指导第四行
                    dtTemp = Utility.GetSubTable(oldData, "HEALTH_TYPE='Hemo_Guide_5_4'");
                    if (dtTemp != null && dtTemp.Rows.Count > 0) {
                        oldData.Rows[17]["HEALTH_VERBAL"] = setTrueFalseToString(chkHEALTH_VERBAL_5_4.Checked);
                        oldData.Rows[17]["HEALTH_WRITTEN"] = setTrueFalseToString(chkHEALTH_WRITTEN_5_4.Checked);
                        oldData.Rows[17]["HEALTH_APPRAISE"] = cmbHEALTH_APPRAISE_5_4.Text;
                        oldData.Rows[17]["HEALTH_HEADMAN_APPRAISE"] = cmbHEALTH_HEADMAN_APPRAISE_5_4.Text;
                    }
                    #endregion

                    #region 规律血透指导第五行
                    dtTemp = Utility.GetSubTable(oldData, "HEALTH_TYPE='Hemo_Guide_5_5'");
                    if (dtTemp != null && dtTemp.Rows.Count > 0) {
                        oldData.Rows[18]["HEALTH_VERBAL"] = setTrueFalseToString(chkHEALTH_VERBAL_5_5.Checked);
                        oldData.Rows[18]["HEALTH_WRITTEN"] = setTrueFalseToString(chkHEALTH_WRITTEN_5_5.Checked);
                        oldData.Rows[18]["HEALTH_APPRAISE"] = cmbHEALTH_APPRAISE_5_5.Text;
                        oldData.Rows[18]["HEALTH_HEADMAN_APPRAISE"] = cmbHEALTH_HEADMAN_APPRAISE_5_5.Text;
                    }
                    #endregion

                    #region 规律血透指导第六行
                    dtTemp = Utility.GetSubTable(oldData, "HEALTH_TYPE='Hemo_Guide_5_6'");
                    if (dtTemp != null && dtTemp.Rows.Count > 0) {
                        oldData.Rows[19]["HEALTH_VERBAL"] = setTrueFalseToString(chkHEALTH_VERBAL_5_6.Checked);
                        oldData.Rows[19]["HEALTH_WRITTEN"] = setTrueFalseToString(chkHEALTH_WRITTEN_5_6.Checked);
                        oldData.Rows[19]["HEALTH_APPRAISE"] = cmbHEALTH_APPRAISE_5_6.Text;
                        oldData.Rows[19]["HEALTH_HEADMAN_APPRAISE"] = cmbHEALTH_HEADMAN_APPRAISE_5_6.Text;
                    }
                    #endregion

                    #endregion

                    if (oldData != null && oldData.Rows.Count > 0) {
                        if (objHemodialysisService.SaveHealthEducationInfo(oldData) > 0) {
                            AutoClosedMsgBox.ShowForm("数据保存成功！", "健康宣教", 1000, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }

        private string setTrueFalseToString(bool pValue) {
            string result = string.Empty;
            result = pValue ? "1" : "0";
            return result;
        }

        private bool setStringToTrueFalse(string pValue) {
            bool result = false;
            if (pValue == "1") {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 数据载入赋值
        /// </summary>
        /// <param name="pHemoID"></param>
        private void LoadData(string pHemoID,string pID) {
            if (string.IsNullOrEmpty(pHemoID))
                return;
            ctlUserLongInfo1.HEMODIALYSIS_ID = pHemoID;
            ctlUserLongInfo1.LoadPatientInfo();
            ctlUserLongInfo1.Enabled = false;
            HemodialysisModel.MED_HEALTH_EDUCATIONDataTable oldData = objHemodialysisService.GetHealthEducationByHemoIdAndId(pHemoID,pID);
            if (oldData != null && oldData.Rows.Count > 0) {

                #region 入室宣教

                #region 入室宣教第一行
                chkHEALTH_VERBAL_1.Checked = setStringToTrueFalse(oldData.Rows[0]["HEALTH_VERBAL"].ToString());
                chkHEALTH_WRITTEN_1.Checked = setStringToTrueFalse(oldData.Rows[0]["HEALTH_WRITTEN"].ToString());
                cmbHEALTH_APPRAISE_1.Text = oldData.Rows[0]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_1.Text = oldData.Rows[0]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (oldData.Rows[0]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_1.EditValue = oldData.Rows[0]["HEALTH_NURSE_ID"].ToString();
                }
                if (oldData.Rows[0]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_1.EditValue = oldData.Rows[0]["HEALTH_NURSE_DATE"];
                }
                if (oldData.Rows[0]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_1.EditValue = oldData.Rows[0]["HEALTH_HEADMAN_DATE"];
                }
                if (oldData.Rows[0]["HEALTH_HEADNURSE_ID"] != null) {
                    txtHEALTH_HEADNURSE_ID.EditValue = oldData.Rows[0]["HEALTH_HEADNURSE_ID"];
                }

                if (oldData.Rows[0]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_1.EditValue = oldData.Rows[0]["HEALTH_HEADMAN_ID"];
                }

                #endregion

                #region 入室宣教第二行
                chkHEALTH_VERBAL_2.Checked = setStringToTrueFalse(oldData.Rows[1]["HEALTH_VERBAL"].ToString());
                chkHEALTH_WRITTEN_2.Checked = setStringToTrueFalse(oldData.Rows[1]["HEALTH_WRITTEN"].ToString());
                cmbHEALTH_APPRAISE_2.Text = oldData.Rows[1]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_2.Text = oldData.Rows[1]["HEALTH_HEADMAN_APPRAISE"].ToString();
                #endregion

                #region 入室宣教第三行
                chkHEALTH_VERBAL_3.Checked = setStringToTrueFalse(oldData.Rows[2]["HEALTH_VERBAL"].ToString());
                chkHEALTH_WRITTEN_3.Checked = setStringToTrueFalse(oldData.Rows[2]["HEALTH_WRITTEN"].ToString());
                cmbHEALTH_APPRAISE_3.Text = oldData.Rows[2]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_3.Text = oldData.Rows[2]["HEALTH_HEADMAN_APPRAISE"].ToString();
                #endregion

                #region 入室宣教第四行
                chkHEALTH_VERBAL_4.Checked = setStringToTrueFalse(oldData.Rows[3]["HEALTH_VERBAL"].ToString());
                chkHEALTH_WRITTEN_4.Checked = setStringToTrueFalse(oldData.Rows[3]["HEALTH_WRITTEN"].ToString());
                cmbHEALTH_APPRAISE_4.Text = oldData.Rows[3]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_4.Text = oldData.Rows[3]["HEALTH_HEADMAN_APPRAISE"].ToString();
                #endregion

                #endregion

                #region 疾病知识

                #region 疾病知识第一行
                chkHEALTH_VERBAL_2_1.Checked = setStringToTrueFalse(oldData.Rows[4]["HEALTH_VERBAL"].ToString());
                chkHEALTH_WRITTEN_2_1.Checked = setStringToTrueFalse(oldData.Rows[4]["HEALTH_WRITTEN"].ToString());
                cmbHEALTH_APPRAISE_2_1.Text = oldData.Rows[4]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_2_1.Text = oldData.Rows[4]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (oldData.Rows[4]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_2.EditValue = oldData.Rows[4]["HEALTH_NURSE_ID"].ToString();
                }
                if (oldData.Rows[4]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_2.EditValue = oldData.Rows[4]["HEALTH_NURSE_DATE"];
                }
                if (oldData.Rows[4]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_2.EditValue = oldData.Rows[4]["HEALTH_HEADMAN_DATE"];
                }

                if (oldData.Rows[4]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_2.EditValue = oldData.Rows[4]["HEALTH_HEADMAN_ID"];
                }
                #endregion

                #region 疾病知识第二行
                chkHEALTH_VERBAL_2_2.Checked = setStringToTrueFalse(oldData.Rows[5]["HEALTH_VERBAL"].ToString());
                chkHEALTH_WRITTEN_2_2.Checked = setStringToTrueFalse(oldData.Rows[5]["HEALTH_WRITTEN"].ToString());
                cmbHEALTH_APPRAISE_2_2.Text = oldData.Rows[5]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_2_2.Text = oldData.Rows[5]["HEALTH_HEADMAN_APPRAISE"].ToString();
                #endregion

                #region 疾病知识第三行
                chkHEALTH_VERBAL_2_3.Checked = setStringToTrueFalse(oldData.Rows[6]["HEALTH_VERBAL"].ToString());
                chkHEALTH_WRITTEN_2_3.Checked = setStringToTrueFalse(oldData.Rows[6]["HEALTH_WRITTEN"].ToString());
                cmbHEALTH_APPRAISE_2_3.Text = oldData.Rows[6]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_2_3.Text = oldData.Rows[6]["HEALTH_HEADMAN_APPRAISE"].ToString();
                #endregion

                #region 疾病知识第四行
                chkHEALTH_VERBAL_2_4.Checked = setStringToTrueFalse(oldData.Rows[7]["HEALTH_VERBAL"].ToString());
                chkHEALTH_WRITTEN_2_4.Checked = setStringToTrueFalse(oldData.Rows[7]["HEALTH_WRITTEN"].ToString());
                cmbHEALTH_APPRAISE_2_4.Text = oldData.Rows[7]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_2_4.Text = oldData.Rows[7]["HEALTH_HEADMAN_APPRAISE"].ToString();
                #endregion

                #region 疾病知识第五行
                chkHEALTH_VERBAL_2_5.Checked = setStringToTrueFalse(oldData.Rows[8]["HEALTH_VERBAL"].ToString());
                chkHEALTH_WRITTEN_2_5.Checked = setStringToTrueFalse(oldData.Rows[8]["HEALTH_WRITTEN"].ToString());
                cmbHEALTH_APPRAISE_2_5.Text = oldData.Rows[8]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_2_5.Text = oldData.Rows[8]["HEALTH_HEADMAN_APPRAISE"].ToString();
                #endregion

                #endregion

                #region 心里护理

                #region 心里护理第一行
                chkHEALTH_VERBAL_3_1.Checked = setStringToTrueFalse(oldData.Rows[9]["HEALTH_VERBAL"].ToString());
                chkHEALTH_WRITTEN_3_1.Checked = setStringToTrueFalse(oldData.Rows[9]["HEALTH_WRITTEN"].ToString());
                cmbHEALTH_APPRAISE_3_1.Text = oldData.Rows[9]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_3_1.Text = oldData.Rows[9]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (oldData.Rows[9]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_3.EditValue = oldData.Rows[9]["HEALTH_NURSE_ID"].ToString();
                }
                if (oldData.Rows[9]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_3.EditValue = oldData.Rows[9]["HEALTH_NURSE_DATE"];
                }
                if (oldData.Rows[9]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_3.EditValue = oldData.Rows[9]["HEALTH_HEADMAN_DATE"];
                }

                if (oldData.Rows[9]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_3.EditValue = oldData.Rows[9]["HEALTH_HEADMAN_ID"];
                }
                #endregion


                #region 心里护理第二行
                chkHEALTH_VERBAL_3_2.Checked = setStringToTrueFalse(oldData.Rows[10]["HEALTH_VERBAL"].ToString());
                chkHEALTH_WRITTEN_3_2.Checked = setStringToTrueFalse(oldData.Rows[10]["HEALTH_WRITTEN"].ToString());
                cmbHEALTH_APPRAISE_3_2.Text = oldData.Rows[10]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_3_2.Text = oldData.Rows[10]["HEALTH_HEADMAN_APPRAISE"].ToString();
                #endregion

                #endregion

                #region 血管通路护理

                #region 血管通路护理第一行
                chkHEALTH_VERBAL_4_1.Checked = setStringToTrueFalse(oldData.Rows[11]["HEALTH_VERBAL"].ToString());
                chkHEALTH_WRITTEN_4_1.Checked = setStringToTrueFalse(oldData.Rows[11]["HEALTH_WRITTEN"].ToString());
                cmbHEALTH_APPRAISE_4_1.Text = oldData.Rows[11]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_4_1.Text = oldData.Rows[11]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (oldData.Rows[11]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_4.EditValue = oldData.Rows[11]["HEALTH_NURSE_ID"].ToString();
                }
                if (oldData.Rows[11]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_4.EditValue = oldData.Rows[11]["HEALTH_NURSE_DATE"];
                }
                if (oldData.Rows[11]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_4.EditValue = oldData.Rows[11]["HEALTH_HEADMAN_DATE"];
                }
                if (oldData.Rows[11]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_4.EditValue = oldData.Rows[11]["HEALTH_HEADMAN_ID"];
                }
                #endregion

                #region 血管通路护理第二行
                chkHEALTH_VERBAL_4_2.Checked = setStringToTrueFalse(oldData.Rows[12]["HEALTH_VERBAL"].ToString());
                chkHEALTH_WRITTEN_4_2.Checked = setStringToTrueFalse(oldData.Rows[12]["HEALTH_WRITTEN"].ToString());
                cmbHEALTH_APPRAISE_4_2.Text = oldData.Rows[12]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_4_2.Text = oldData.Rows[12]["HEALTH_HEADMAN_APPRAISE"].ToString();

                #endregion

                #region 血管通路护理第三行
                chkHEALTH_VERBAL_4_3.Checked = setStringToTrueFalse(oldData.Rows[13]["HEALTH_VERBAL"].ToString());
                chkHEALTH_WRITTEN_4_3.Checked = setStringToTrueFalse(oldData.Rows[13]["HEALTH_WRITTEN"].ToString());
                cmbHEALTH_APPRAISE_4_3.Text = oldData.Rows[13]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_4_3.Text = oldData.Rows[13]["HEALTH_HEADMAN_APPRAISE"].ToString();

                #endregion

                #endregion

                #region 规律血管指导

                #region 规律血管指导第一行
                chkHEALTH_VERBAL_5_1.Checked = setStringToTrueFalse(oldData.Rows[14]["HEALTH_VERBAL"].ToString());
                chkHEALTH_WRITTEN_5_1.Checked = setStringToTrueFalse(oldData.Rows[14]["HEALTH_WRITTEN"].ToString());
                cmbHEALTH_APPRAISE_5_1.Text = oldData.Rows[14]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_5_1.Text = oldData.Rows[14]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (oldData.Rows[14]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_5.EditValue = oldData.Rows[14]["HEALTH_NURSE_ID"].ToString();
                }
                if (oldData.Rows[14]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_5.EditValue = oldData.Rows[14]["HEALTH_NURSE_DATE"];
                }
                if (oldData.Rows[14]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_5.EditValue = oldData.Rows[14]["HEALTH_HEADMAN_DATE"];
                }
                if (oldData.Rows[14]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_5.EditValue = oldData.Rows[14]["HEALTH_HEADMAN_ID"];
                }
                #endregion

                #region 规律血管指导第二行
                chkHEALTH_VERBAL_5_2.Checked = setStringToTrueFalse(oldData.Rows[15]["HEALTH_VERBAL"].ToString());
                chkHEALTH_WRITTEN_5_2.Checked = setStringToTrueFalse(oldData.Rows[15]["HEALTH_WRITTEN"].ToString());
                cmbHEALTH_APPRAISE_5_2.Text = oldData.Rows[15]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_5_2.Text = oldData.Rows[15]["HEALTH_HEADMAN_APPRAISE"].ToString();
                #endregion

                #region 规律血管指导第三行
                chkHEALTH_VERBAL_5_3.Checked = setStringToTrueFalse(oldData.Rows[16]["HEALTH_VERBAL"].ToString());
                chkHEALTH_WRITTEN_5_3.Checked = setStringToTrueFalse(oldData.Rows[16]["HEALTH_WRITTEN"].ToString());
                cmbHEALTH_APPRAISE_5_3.Text = oldData.Rows[16]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_5_3.Text = oldData.Rows[16]["HEALTH_HEADMAN_APPRAISE"].ToString();
                #endregion

                #region 规律血管指导第四行
                chkHEALTH_VERBAL_5_4.Checked = setStringToTrueFalse(oldData.Rows[17]["HEALTH_VERBAL"].ToString());
                chkHEALTH_WRITTEN_5_4.Checked = setStringToTrueFalse(oldData.Rows[17]["HEALTH_WRITTEN"].ToString());
                cmbHEALTH_APPRAISE_5_4.Text = oldData.Rows[17]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_5_4.Text = oldData.Rows[17]["HEALTH_HEADMAN_APPRAISE"].ToString();
                #endregion

                #region 规律血管指导第五行
                chkHEALTH_VERBAL_5_5.Checked = setStringToTrueFalse(oldData.Rows[18]["HEALTH_VERBAL"].ToString());
                chkHEALTH_WRITTEN_5_5.Checked = setStringToTrueFalse(oldData.Rows[18]["HEALTH_WRITTEN"].ToString());
                cmbHEALTH_APPRAISE_5_5.Text = oldData.Rows[18]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_5_5.Text = oldData.Rows[18]["HEALTH_HEADMAN_APPRAISE"].ToString();
                #endregion

                #region 规律血管指导第六行
                chkHEALTH_VERBAL_5_6.Checked = setStringToTrueFalse(oldData.Rows[19]["HEALTH_VERBAL"].ToString());
                chkHEALTH_WRITTEN_5_6.Checked = setStringToTrueFalse(oldData.Rows[19]["HEALTH_WRITTEN"].ToString());
                cmbHEALTH_APPRAISE_5_6.Text = oldData.Rows[19]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_5_6.Text = oldData.Rows[19]["HEALTH_HEADMAN_APPRAISE"].ToString();
                #endregion

                #endregion
            }
        }
        #endregion


    }
}