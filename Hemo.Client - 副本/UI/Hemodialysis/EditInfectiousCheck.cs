/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：确认病人传染病控制状态
// 创建时间：2013-07-19
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
using Hemo.IService;
using Hemo.IService.Config;
using Hemo.IService.Dict;



namespace Hemo.Client.UI.Hemodialysis {
    public partial class EditInfectiousCheck :HemoBaseFrm
    {

        #region 私有变量
        private DataTable _infectiousDataTable;
        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        public EditInfectiousCheck() {
            InitializeComponent();
            loadLookUpEditList();
        }

        /// <summary>
        /// 病人质控数据表
        /// </summary>
        public DataTable infectiousDataTable {
            set {
                _infectiousDataTable = value;
            }
            get {
                return _infectiousDataTable;
            }
        }

        /// <summary>
        /// 透析病人ID
        /// </summary>
        public string HemodialysisID {
            get;
            set;
        }

        /// <summary>
        /// 检查ID
        /// </summary>
        public string InfectiousID {
            get;
            set;
        }

        #region 事件
        private void LoadData() {
            //   HemodialysisModel.MED_INFECTIOUS_CHECKRow infectiousRow = _infectiousDataTable.FindByINFECTIOUS_ID(InfectiousID);
            ctlUserLongInfo1.HEMODIALYSIS_ID = HemodialysisID;
            txtHEMODIALYSIS_ID.Text = HemodialysisID;
            ctlUserLongInfo1.LoadPatientInfo();
            if (infectiousDataTable != null && infectiousDataTable.Rows.Count > 0) {
                infectiousDataTable = Utility.GetSubTable(infectiousDataTable, "hemodialysis_id='" + HemodialysisID + "'");
                if (infectiousDataTable.Rows[0]["STATUS"].ToString() == "1") {
                    chkSTATUS.Checked = true;
                }
                else {
                    chkSTATUS.Checked = false;
                }
                BaseControlInfo.SetControlDataByDataTable(infectiousDataTable, panelControl1);
            }
        }

        private void loadLookUpEditList() {
            BaseControlInfo.BindLookUpEdit(cmbHEPATITIS_B, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "检查值", "1"), "ITEM_NAME", "乙肝检查");
            BaseControlInfo.BindLookUpEdit(cmbHEPATITIS_C, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "检查值", "1"), "ITEM_NAME", "丙肝检查");
            BaseControlInfo.BindLookUpEdit(cmbSYPHILIS, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "检查值", "1"), "ITEM_NAME", "梅毒检查");
            BaseControlInfo.BindLookUpEdit(cmbHIV, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "检查值", "1"), "ITEM_NAME", "HIV检查");
            DataTable dtStaffSict = _staffDictService.GetStaffDictList();
            DataTable dtDoctorList = Utility.GetSubTable(dtStaffSict, "ZYNAME='医生'");
            if (dtDoctorList != null && dtDoctorList.Rows.Count > 0) {
                BaseControlInfo.BindLookUpEdit(cmbCHECK_USER_ID, "USER_NAME", "NAME", dtDoctorList, "NAME", "责任医生");
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            HemodialysisModel.MED_INFECTIOUS_CHECKDataTable dtResult = new HemodialysisModel.MED_INFECTIOUS_CHECKDataTable();
            DataTable dt = BaseControlInfo.GetDataTableByPanel(infectiousDataTable, panelControl1);
            HemodialysisModel.MED_INFECTIOUS_CHECKRow drNew = null;
            if (infectiousDataTable.Rows.Count == 0) {
                drNew = dtResult.NewMED_INFECTIOUS_CHECKRow();
                drNew.HEMODIALYSIS_ID = dt.Rows[0]["HEMODIALYSIS_ID"].ToString();
            }
            else {
                dtResult = objHemodialysisService.GetMedInfectiousInfoByID(infectiousDataTable.Rows[0]["INFECTIOUS_ID"].ToString());
                drNew = dtResult.Rows[0] as HemodialysisModel.MED_INFECTIOUS_CHECKRow;
            }

            drNew.HEPATITIS_B = dt.Rows[0]["HEPATITIS_B"].ToString();
            drNew.HEPATITIS_C = dt.Rows[0]["HEPATITIS_C"].ToString();
            drNew.SYPHILIS = dt.Rows[0]["SYPHILIS"].ToString();
            drNew.HIV = dt.Rows[0]["HIV"].ToString();
            drNew.CHECK_USER_ID = dt.Rows[0]["CHECK_USER_ID"].ToString();
            drNew.STATUS = dt.Rows[0]["STATUS"].ToString();
            drNew.CREATE_DATE = System.DateTime.Now;
            drNew.HEPATITIS_B_DATE = Utility.CDate(dt.Rows[0]["HEPATITIS_B_DATE"].ToString());
            drNew.HEPATITIS_C_DATE = Utility.CDate(dt.Rows[0]["HEPATITIS_C_DATE"].ToString());
            drNew.SYPHILIS_DATE = Utility.CDate(dt.Rows[0]["SYPHILIS_DATE"].ToString());
            drNew.HIV_DATE = Utility.CDate(dt.Rows[0]["HIV_DATE"].ToString());
            drNew.PATIENT_ID = dt.Rows[0]["PATIENT_ID"].ToString();
            if (infectiousDataTable.Rows.Count == 0) {
                dtResult.Rows.Add(drNew);
            }

            if (dtResult != null && dtResult.Rows.Count > 0) {
                int result = objHemodialysisService.SaveMedInfectiousCheck(dtResult);
                if (result != 0) {
                    XtraMessageBox.Show("传染病检查数据保存成功！");
                    this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                    this.Close();
                }
            }
        }

        private void EditInfectiousCheck_Load(object sender, EventArgs e) {
            this.Text = "传染病入院检查";
            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountFrm(this);

            LoadData();
        }
        #endregion
    }
}