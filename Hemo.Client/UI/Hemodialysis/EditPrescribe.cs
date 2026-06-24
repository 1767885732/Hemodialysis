/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：透析处方窗体 
// 创建时间：2013-03-27
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
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.IService.Config;
using DevExpress.XtraEditors.Controls;
using Hemo.IService;
using Hemo.IService.Dict;
using Hemo.Client.Core;
using Hemo.IService.PatientSchedule;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class EditPrescribe : HemoBaseFrm
    {

        #region 私有成员
        /// <summary>
        /// 处方表
        /// </summary>
        private HemodialysisModel.MED_HEMO_RECIPEDataTable _recipeDatatable;
        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IDrug objDrug = ServiceManager.Instance.DrugService;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private IVascuarAccess _vascuarAccess = ServiceManager.Instance.VascuarAccessService;
        private IPatientSchedule patientScheduleService = ServiceManager.Instance.PatientSchedule;
        private ConfigModel.MED_COMMON_RELATIONDataTable _relationData = new ConfigModel.MED_COMMON_RELATIONDataTable();
        private ConfigModel.MED_COMMON_ITEMLISTDataTable dtAccess = null;
        private HemoModel.MED_VASCULAR_ACCESSDataTable dtVasularAccess = null;

        private string _recipeID = string.Empty;
        private string _hemoID = string.Empty;
        private string areaName = string.Empty;

        public string AreaName
        {
            get { return areaName; }
            set { areaName = value; }
        }
        private bool isLong = false;

        public bool IsLong
        {
            get { return isLong; }
            set { isLong = value; }
        }
        /// <summary>
        /// 是否为新增血管通路
        /// </summary>
        private bool isAdd = false;
        private string lastHemoWeight = string.Empty;
        #endregion

        #region 初始化方法
        public EditPrescribe(string pHemoID, string pRecipeID)
        {
            InitializeComponent();
            loadData(pHemoID, pRecipeID);
        }

        private void loadData(string pHemodialysisID, string pRecipeID)
        {
            ctlUserLongInfo1.HEMODIALYSIS_ID = pHemodialysisID;
            txtHEMODIALYSIS_ID.Text = pHemodialysisID;
            ctlUserLongInfo1.LoadPatientInfo();
            _hemoID = pHemodialysisID;
            _recipeID = pRecipeID;
            setExecRecipeStatus(pRecipeID);
            _relationData = this._configService.GetCommRelation();
        }

        private void setExecRecipeStatus(string pRecipeID)
        {
            int cCount = objHemodialysisService.GetRecipeCountInCureList(pRecipeID);
            if (cCount >= 1)
            {
                btnSave.Enabled = false;
                Utilities.BaseControlInfo.SetControlEnabled(groupControl1, false);
                Utilities.BaseControlInfo.SetControlEnabled(groupControl2, false);
                Utilities.BaseControlInfo.SetControlEnabled(groupControl3, false);
                Utilities.BaseControlInfo.SetControlEnabled(groupControl4, false);
                Utilities.BaseControlInfo.SetControlEnabled(groupControl5, false);

            }
        }

        private void EditPrescribe_Load(object sender, EventArgs e)
        {

            this.Text = "透析方案制定";
            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountFrm(this);
            loadLookUpEditList();
            showRecipeInfo(_recipeID);
            if (_recipeID.Length == 0)
            {
                addNewRecipe();
            }
            this.lupRECIPE_TYPE.Enabled = false;
        }

        /// <summary>
        /// 根据透析号得到病人血管通路日期列表并绑定给树控件
        /// </summary>
        /// <param name="pHEMODIALYSIS_ID">透析号</param>
        private void showTreeList(string pHEMODIALYSIS_ID)
        {
            //_recipeDatatable = objHemodialysisService.GetRecipeByHemodialysisID(pHEMODIALYSIS_ID);
            //if (_recipeDatatable != null && _recipeDatatable.Rows.Count > 0) {

            //    treeList1.DataSource = _recipeDatatable;
            //    this.treeList1.KeyFieldName = _recipeDatatable.Columns["RECIPE_ID"].ToString();
            //    this.treeListColumn1.FieldName = "PURIFICATION_MODE_NAME";
            //    this.treeListColumn3.FieldName = "STATUSNAME";
            //}
        }

        /// <summary>
        /// 载入窗体下拉框数据
        /// </summary>
        private void loadLookUpEditList()
        {
            BaseControlInfo.BindLookUpEdit(cmbFIRST_PURIFIER_MODEL, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "净化器类型", "1"), "ITEM_NAME", "净化器类型");
            BaseControlInfo.BindLookUpEdit(cmbSECOND_PURIFIER_MODEL, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "净化器类型", "1"), "ITEM_NAME", "净化器类型");
            BaseControlInfo.BindLookUpEdit(cmbFIRST_PURIFIER_NAME, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "透析膜", "1"), "ITEM_NAME", "透析膜");
            BaseControlInfo.BindLookUpEdit(cmbSECOND_PURIFIER_NAME, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "透析膜", "1"), "ITEM_NAME", "透析膜");
            BaseControlInfo.BindLookUpEdit(cmbTHERAPEUTIC_METHOD, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "治疗方法", "1"), "ITEM_NAME", "治疗方法");
            BaseControlInfo.BindLookUpEdit(cmbFIRST_DRUG_UNIT, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "药品单位", "1"), "ITEM_NAME", "药品单位");
            BaseControlInfo.BindLookUpEdit(cmbSECOND_DRUG_UNIT, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "药品单位", "1"), "ITEM_NAME", "药品单位");
            BaseControlInfo.BindLookUpEdit(cmbFIRST_DRUG_MODE, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "注射方式", "1"), "ITEM_NAME", "注射方式");
            BaseControlInfo.BindLookUpEdit(cmbSECOND_DRUG_MODE, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "注射方式", "1"), "ITEM_NAME", "注射方式");
            //BaseControlInfo.BindLookUpEdit(cmbVASCULAR_ACCESS_ID, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "血管通路", "1"), "ITEM_NAME", "血管通路");


            //dtAccess = objHemodialysisService.GetItemListByHemoIDandItemType(_hemoID, "血管通路");
            //BaseControlInfo.BindLookUpEdit(cmbVASCULAR_ACCESS_ID, "ITEM_ID", "ITEM_NAME", dtAccess, "ITEM_NAME", "血管通路");
            //this.cmbVASCULAR_ACCESS_ID.Properties.DropDownRows = dtAccess.Rows.Count == 0 ? 1 : dtAccess.Rows.Count;

            dtVasularAccess = objHemodialysisService.GetPatientVasular_AccessDt(_hemoID);
            BaseControlInfo.BindLookUpEdit(this.cmbVASCULAR_ACCESS_ID, "VASCULAR_ACCESS_ID", "PATIENT_ID", dtVasularAccess, "PATIENT_ID", "血管通路");
            if (dtVasularAccess.Rows.Count > 0)
                this.cmbVASCULAR_ACCESS_ID.EditValue = dtVasularAccess[0].VASCULAR_ACCESS_ID;
            else
                this.cmbVASCULAR_ACCESS_ID.EditValue = null;



            string type = areaName.Equals("CRRT") ? "CRRT净化方式" : "净化方式";
            DataTable dtPurification = this._configService.GetConfigList(string.Empty, string.Empty, type, "1");
            if (dtPurification != null && dtPurification.Rows.Count > 0)
            {
                BaseControlInfo.BindLookUpEdit(cmbPURIFICATION_MODE, "ITEM_ID", "ITEM_NAME", dtPurification, "ITEM_NAME", "净化方式");
                cmbPURIFICATION_MODE.EditValue = dtPurification.Rows[0]["ITEM_ID"].ToString();
            }
            txtFIRST_DRUG_NAME.Properties.DataSource = txtSECOND_DRUG_NAME.Properties.DataSource = objDrug.GetDrugMasterList();//绑定数据源
            txtFIRST_DRUG_NAME.Properties.Columns.Add(new LookUpColumnInfo("DRUG_CODE", "编号"));   //大小写敏感
            txtFIRST_DRUG_NAME.Properties.Columns.Add(new LookUpColumnInfo("DRUG_NAME", "名称"));
            txtFIRST_DRUG_NAME.Properties.TextEditStyle = TextEditStyles.Standard;
            txtFIRST_DRUG_NAME.Properties.DisplayMember = "DRUG_NAME";//要显示的字段,Text获得
            txtFIRST_DRUG_NAME.Properties.ValueMember = "DRUG_CODE";//实际值的字段,EditValue获得 // DeptID

            txtSECOND_DRUG_NAME.Properties.Columns.Add(new LookUpColumnInfo("DRUG_CODE", "编号"));   //大小写敏感
            txtSECOND_DRUG_NAME.Properties.Columns.Add(new LookUpColumnInfo("DRUG_NAME", "名称"));
            txtSECOND_DRUG_NAME.Properties.TextEditStyle = TextEditStyles.Standard;
            txtSECOND_DRUG_NAME.Properties.DisplayMember = "DRUG_NAME";//要显示的字段,Text获得
            txtSECOND_DRUG_NAME.Properties.ValueMember = "DRUG_CODE";//实际值的字段,EditValue获得 // DeptID

            DataTable dtStaffSict = _staffDictService.GetStaffDictList();
            DataTable dtDoctorList = Utility.GetSubTable(dtStaffSict, "ZYNAME='医生'");
            if (dtDoctorList != null && dtDoctorList.Rows.Count > 0)
            {
                BaseControlInfo.BindLookUpEdit(cmbUSER_ID, "EMP_NO", "NAME", dtDoctorList, "NAME", "责任医生");
            }

            DataTable dtType = new DataTable();
            dtType.Columns.Add(new DataColumn("ITEM_ID"));
            dtType.Columns.Add(new DataColumn("ITEM_NAME"));

            DataRow row = dtType.NewRow();
            row["ITEM_ID"] = "1";
            row["ITEM_NAME"] = "长期";
            dtType.Rows.Add(row);

            row = dtType.NewRow();
            row["ITEM_ID"] = "0";
            row["ITEM_NAME"] = "临时";
            dtType.Rows.Add(row);

            Utility.BindLookUpEdit(lupRECIPE_TYPE, "ITEM_ID", "ITEM_NAME", dtType, "ITEM_NAME", "类型");
            this.lupRECIPE_TYPE.EditValue = "1";
            this.lupRECIPE_TYPE.Text = "长期";

        }
        #endregion

        #region 数据处理
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        private bool IsDataValidate()
        {
            bool result = true;
            if (txtRECIPE_DATE.EditValue == null)
            {
                XtraMessageBox.Show("请输入处方日期。", "透析处方");
                txtRECIPE_DATE.Focus();
                return false;
            }

            result = BaseControlInfo.CheckpLookUpEdit(cmbPURIFICATION_MODE, "请选择净化方式。", "透析处方");
            if (result == false)
            {
                return result;
            }

            //result = BaseControlInfo.CheckSpinEdit(this.txtUFR, "请录入预计脱水。", this.Text);
            //if (result == false)
            //{
            //    return result;
            //}

            result = BaseControlInfo.CheckpLookUpEdit(cmbFIRST_PURIFIER_MODEL, "请选择净化器型号。", "透析处方");
            if (result == false)
            {
                return result;
            }

            decimal dTemp = 0;
            //钠	135~145
            dTemp = Utility.CDecimal(txtSODION.Text);
            if (dTemp > 145 || dTemp < 135)
            {
                XtraMessageBox.Show("钠离子的值正常范围在135~145之间。", "透析处方");
                txtSODION.Focus();
                return false;
            }

            //钾	0~4
            dTemp = Utility.CDecimal(txtPOTASSIUM_ION.Text);
            if (dTemp > 4 || dTemp < 0)
            {
                XtraMessageBox.Show("钾离子的值正常范围在0~4之间。", "透析处方");
                txtPOTASSIUM_ION.Focus();
                return false;
            }

            //钙	1.25~1.75
            dTemp = Utility.CDecimal(txtCALCIUM_ION.Text);
            if (double.Parse(dTemp.ToString()) < 1.25 || double.Parse(dTemp.ToString()) > 1.75)
            {
                XtraMessageBox.Show("钙离子的值正常范围在1.25~1.75之间。", "透析处方");
                txtCALCIUM_ION.Focus();
                return false;
            }

            //碳酸氢根	30~40
            dTemp = Utility.CDecimal(txtBICARBONATE_RADICAL.Text);
            if (dTemp < 30 || dTemp > 40)
            {
                XtraMessageBox.Show("碳酸氢根的值正常范围在30~40之间。", "透析处方");
                txtBICARBONATE_RADICAL.Focus();
                return false;
            }

            //验证血管通路是否存在
            HemoModel.MED_VASCULAR_ACCESSDataTable vTable = _vascuarAccess.GetVascularAccessListByHEMODIALYSIS_ID(ctlUserLongInfo1.HEMODIALYSIS_ID);
            if (vTable == null || vTable.Rows.Count <= 0)
            {
                XtraMessageBox.Show("选择的血管通路类型尚未建立，请先建立病人的血管通路信息。", "透析处方");
                //cmbVASCULAR_ACCESS_ID.Focus();
                return false;
            }

            string strVASCULAR_ACCESS_ID = cmbVASCULAR_ACCESS_ID.EditValue.ToString();
            if (strVASCULAR_ACCESS_ID.Length == 0)
            {
                XtraMessageBox.Show("请选择病人血管通路。", "透析处方");
                cmbVASCULAR_ACCESS_ID.Focus();
                return false;
            }
            else
            {
                if (vTable != null && vTable.Rows.Count > 0)
                {
                    DataTable tmpTable = Utility.GetSubTable((DataTable)vTable, "VASCULAR_ACCESS_ID='" + strVASCULAR_ACCESS_ID + "'");
                    if (tmpTable.Rows.Count == 0)
                    {
                        XtraMessageBox.Show("选择的血管通路类型尚未建立，请先建立病人的血管通路信息。", "透析处方");
                        //cmbVASCULAR_ACCESS_ID.Focus();
                        return false;
                    }
                }
            }

            //判断透析处方有效的状态数量
            dTemp = objHemodialysisService.GetRecipeStatusCountByHemoID(ctlUserLongInfo1.HEMODIALYSIS_ID);
            if (dTemp == 0 && chkStatus.Checked == false)
            {
                XtraMessageBox.Show("请设置一张启用的透析处方。", "透析处方");
                chkStatus.Focus();
                return false;
            }

            DataTable dt = objHemodialysisService.GetMainCureListByHemoID(ctlUserLongInfo1.HEMODIALYSIS_ID);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows.Count >= 1)
                {
                    // string strType = lupRECIPE_TYPE.EditValue.ToString();
                    if (isAdd)
                    {
                        XtraMessageBox.Show("已存在一条长期医嘱，请先停用当前长期医嘱。", "透析处方");
                        return false;
                    }
                }
            }

            //if (dTemp > 1 && chkStatus.Checked) {
            //    XtraMessageBox.Show("有多张启用的透析处方，请确认只有一张有效的处方。", "透析处方");
            //    chkStatus.Focus();
            //    return false;
            //}
            return result;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        private int SaveData()
        {
            int result = 0;
            DataTable dt = _recipeDatatable;
            DataRow dr;
            int rCount = 0;

            if (txtRECIPE_ID.Text.Length == 0)
            {
                if (lupRECIPE_TYPE.EditValue.ToString() == "1")
                {
                    rCount = objHemodialysisService.GetRecipeCountByPurificationMode(txtHEMODIALYSIS_ID.Text.Trim(), "");// cmbPURIFICATION_MODE.Text.ToString()

                    if (rCount >= 1)
                    {
                        XtraMessageBox.Show("已经存一张长期医嘱，请先停止在开立新的医嘱。", "透析处方");
                        return 0;
                    }
                }
                else
                {
                    rCount = objHemodialysisService.GetTempRecipeCountByDate(txtHEMODIALYSIS_ID.Text.Trim(), Utility.CDate(txtRECIPE_DATE.EditValue.ToString()));
                    if (rCount >= 1)
                    {
                        XtraMessageBox.Show("已经存一张当天的临时医嘱，请作废后重开临时医嘱。", "透析处方");
                        return 0;
                    }
                }

                txtRECIPE_ID.Text = objHemodialysisService.GetNewRecipeID();
                _recipeDatatable = new HemodialysisModel.MED_HEMO_RECIPEDataTable();
                dr = _recipeDatatable.NewRow();
                dr["RECIPE_ID"] = txtRECIPE_ID.Text;
                dr["HEMODIALYSIS_ID"] = ctlUserLongInfo1.HEMODIALYSIS_ID;
                dr["USER_ID"] = cmbUSER_ID.EditValue;
                dr["DRY_WEIGHT"] = this.txtDRY_WEIGHT.Value.ToString();
                dr["UFR"] = this.txtUFR.Value.ToString();
                //dr["STATUS"] = chkStatus.Checked == true ? "1" : "0";
                dr["STATUS"] = "1";
                dr["REMARK"] = txtREMARK.Text;
                dr["RECIPE_TYPE"] = lupRECIPE_TYPE.EditValue;//处方类型：0：临时 1：长期
                if (txtRECIPE_DATE.EditValue != null) //RECIPE_DATE
                {
                    dr["RECIPE_DATE"] = Utility.CDate(txtRECIPE_DATE.EditValue.ToString()).ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ":" + DateTime.Now.Second;
                }
                else
                {
                    dr["RECIPE_DATE"] = System.DateTime.Now;
                }
                _recipeDatatable.Rows.Add(dr);
            }
            else
            {
                //判断该处方是否被执行如果已经被执行则不能修改保存
                int cCount = objHemodialysisService.GetRecipeCountInCureList(txtRECIPE_ID.Text.Trim());
                if (cCount >= 1)
                {
                    XtraMessageBox.Show("该处方已经被执行，不能修改处方。", "透析处方");
                    return 0;
                }


                //2015-08-28 刘超 判断处方是否已经确定，如果确定不能修改
                cCount = 0;


                // _recipeDatatable.Rows[0]["STATUS"] = chkStatus.Checked ? "1" : "0";
                //  _recipeDatatable.Rows[0]["STATUS"] = "1";
                if (txtRECIPE_DATE.EditValue != null) //RECIPE_DATE
                {
                    _recipeDatatable.Rows[0]["RECIPE_DATE"] = Utility.CDate(txtRECIPE_DATE.EditValue.ToString()).ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ":" + DateTime.Now.Second;
                }
                else
                {
                    _recipeDatatable.Rows[0]["RECIPE_DATE"] = System.DateTime.Now;
                }
                _recipeDatatable.Rows[0]["DRY_WEIGHT"] = this.txtDRY_WEIGHT.Value.ToString();
                _recipeDatatable.Rows[0]["UFR"] = this.txtUFR.Value.ToString();
                // dr = _recipeDatatable.Rows[0];
            }
            dt = BaseControlInfo.GetDataTableByPanel(_recipeDatatable, panControl);
            if (txtRECIPE_DATE.EditValue != null) //RECIPE_DATE
            {
                dt.Rows[0]["RECIPE_DATE"] = Utility.CDate(txtRECIPE_DATE.EditValue.ToString()).ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ":" + DateTime.Now.Second;
            }
            else
            {
                dt.Rows[0]["RECIPE_DATE"] = System.DateTime.Now;
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                //int iStatus = objHemodialysisService.GetRecipeByHemodialysisID(ctlUserLongInfo1.HEMODIALYSIS_ID);
                //if (iStatus > 1) {

                //}
                result = objHemodialysisService.SaveRecipe((HemodialysisModel.MED_HEMO_RECIPEDataTable)dt);
            }
            return result;
        }
        #endregion

        #region 各种事件
        /// <summary>
        /// 左侧树控件点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            //busyIndicator1.ShowLoadingScreenFor(treeList1);
            //if (e.Node != null) {
            //    showRecipeInfo(e.Node.GetValue("RECIPE_ID").ToString());
            //}
        }

        private void showRecipeInfo(string pRecipeID)
        {
            busyIndicator1.ShowLoadingScreenFor(this);
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    _recipeDatatable = objHemodialysisService.GetRecipeByRecipeID(pRecipeID);
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    if (_recipeDatatable != null && _recipeDatatable.Rows.Count > 0)
                    {
                        BaseControlInfo.ClearControlText(panControl);
                        cmbUSER_ID.EditValue = string.Empty;
                        BaseControlInfo.SetControlDataByDataTable(_recipeDatatable, panControl);
                        cmbUSER_ID.EditValue = _recipeDatatable.Rows[0]["USER_ID"];

                        if (!_recipeDatatable[0].IsVASCULAR_ACCESS_IDNull())
                            this.cmbVASCULAR_ACCESS_ID.EditValue = _recipeDatatable[0].VASCULAR_ACCESS_ID;
                        else
                            this.cmbVASCULAR_ACCESS_ID.EditValue = dtVasularAccess[0].VASCULAR_ACCESS_ID;



                        //if (this.cmbVASCULAR_ACCESS_ID.Properties.DropDownRows > 1)
                        //{
                        //    this.cmbVASCULAR_ACCESS_ID.EditValue = dtAccess[0].ITEM_ID;
                        //}

                        ////载入上次透后体重
                        //if (IsLong) {
                        //    //如果是长期处方的话上次透后只取离当前时间最近的一次要不然按时间取的话会没有值。
                        //    txtLastWeight.Text = objHemodialysisService.GetLastTimeCureDataByID(_hemoID, DateTime.Now.Date);
                        //}
                        //else {
                        //    txtLastWeight.Text = objHemodialysisService.GetLastTimeCureDataByID(_hemoID, Utility.CDate(_recipeDatatable.Rows[0]["RECIPE_DATE"].ToString()));
                        //}


                        loadWeightList(_hemoID);

                        //cmbWeightList.DataSource = dtWeight;
                        //if (_recipeDatatable.Rows[0]["STATUS"].ToString() == "1") {
                        //    chkStatus.Checked = true;
                        //}
                        //else {
                        //    chkStatus.Checked = false;
                        //}
                    }
                    isAdd = false;
                    this.busyIndicator1.HideLoadingScreen();
                };
                worker.RunWorkerAsync();
            }
        }

        //载入历次干体重
        private void loadWeightList(string pHemoID)
        {
            DataTable dtWeightList = objHemodialysisService.GetDryWeightListByHemoID(pHemoID);
            if (dtWeightList != null && dtWeightList.Rows.Count > 0)
            {
                cmbWeightList.DataSource = dtWeightList;
                cmbWeightList.DisplayMember = "AHTERWEIGHT";
            }
        }

        /// <summary>
        /// 根据当前患者排班患者插入当天的处方数据
        /// </summary>
        private void insertPatientTodayRecipe()
        {
            DateTime date = Utility.CDate(patientScheduleService.GetServerDate()).Date;
            DateTime startWeek = Utility.GetMonday(date).Date;
            DateTime endWeek = startWeek.AddDays(6).Date;
            DateTime dt = new DateTime();
            if (lupRECIPE_TYPE.EditValue.ToString() == "1")
            {
                dt = date;
                objHemodialysisService.DeleteUnExcuteRecipeByHemoID(ctlUserLongInfo1.HEMODIALYSIS_ID, dt);
                for (int i = 0; i < 14; i++)
                {
                    if (date <= startWeek.AddDays(i))
                        objHemodialysisService.CreatePatientRecipeBydate(startWeek.AddDays(i));
                }
            }
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsDataValidate())
            {
                if (XtraMessageBox.Show("确定保存当前处方信息吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                if (SaveData() > 0)
                {
                    //插入临时医嘱
                    insertPatientTodayRecipe();
                    AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);
                    //XtraMessageBox.Show("保存成功。", "透析处方");
                    this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                    this.Close();
                    //showTreeList(ctlUserLongInfo1.HEMODIALYSIS_ID);
                }
            }
        }

        /// <summary>
        /// 设置不能为负数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnFREQUENCY_WEEK_TextChanged(object sender, EventArgs e)
        {
            if (spnFREQUENCY_WEEK.Text.IndexOf('-') > -1)
            {
                spnFREQUENCY_WEEK.Text = spnFREQUENCY_WEEK.Text.Replace("-", "");
            }
        }

        /// <summary>
        /// 新增处方
        /// </summary>
        private void addNewRecipe()
        {
            BaseControlInfo.ClearControlText(groupControl1);
            BaseControlInfo.ClearControlText(groupControl2);
            BaseControlInfo.ClearControlText(groupControl3);
            BaseControlInfo.ClearControlText(groupControl4);
            BaseControlInfo.ClearControlText(groupControl5);
            txtHEMODIALYSIS_ID.Text = ctlUserLongInfo1.HEMODIALYSIS_ID;
            //txtLastWeight.Text = lastHemoWeight;
            loadWeightList(txtHEMODIALYSIS_ID.Text);
            //  txtRECIPE_DATE.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            // txtPURIFICATION_MODE1.Focus();
            isAdd = true;
            _recipeDatatable = null;
            txtRECIPE_DATE.EditValue = Utility.CDate(System.DateTime.Now.ToString());
            cmbPURIFICATION_MODE.Text = "HD";
            spnFREQUENCY_WEEK.Text = "1";
            spnFREQUENCY_TIMES.Text = "3";
            spnFREQUENCY_HOURS.Text = "4";
            this.lupRECIPE_TYPE.EditValue = "1";
            this.lupRECIPE_TYPE.Text = "长期";
            spnSPKT_V.Text = "1.2";
            spnURR.Text = "65";
            if (cmbVASCULAR_ACCESS_ID.Properties.DataSource != null)
            {
                if ((((Hemo.Model.HemoModel.MED_VASCULAR_ACCESSDataTable)(cmbVASCULAR_ACCESS_ID.Properties.DataSource))).Count > 1)
                {
                    cmbVASCULAR_ACCESS_ID.Text = (((Hemo.Model.HemoModel.MED_VASCULAR_ACCESSDataTable)(cmbVASCULAR_ACCESS_ID.Properties.DataSource))).Rows[0]["PATIENT_ID"].ToString();
                }
            }
            txtSODION.EditValue = "140";
            txtPOTASSIUM_ION.EditValue = "2";
            txtCALCIUM_ION.EditValue = "1.5";
            txtBICARBONATE_RADICAL.EditValue = "35";
            txtDIALYSATE_FLOW.EditValue = "500";
            txtDIALYSATE_TEMPERATURE.EditValue = "36.5";

            cmbUSER_ID.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
            // cmbUSER_ID.Text = HemoApplicationContext.Current.CurrentUser.EMP_NO;
        }

        /// <summary>
        /// 新增清空控件值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            addNewRecipe();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFIRST_PURIFIER_M2_Leave(object sender, EventArgs e)
        {
            checkNumberValue(txtFIRST_PURIFIER_M2);

        }

        private void txtDRY_WEIGHT_TextChanged(object sender, EventArgs e)
        {
            checkNumberValue(txtDRY_WEIGHT);
        }

        private void txtUFR_TextChanged(object sender, EventArgs e)
        {
            checkNumberValue(txtUFR);
        }

        //过滤符号
        private void checkNumberValue(SpinEdit pEdit)
        {
            if (pEdit.Text.IndexOf("-") > -1)
            {
                pEdit.Text = pEdit.Text.Replace("-", "");
            }
        }

        private void treeList1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(MousePosition);
            }
        }

        private void 患者处方ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chkStatus.Checked = true;
            btnSave_Click(null, null);
        }

        private void cmbVASCULAR_ACCESS_ID_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (cmbVASCULAR_ACCESS_ID.Properties.DataSource != null)
            {
                if (((DataTable)(cmbVASCULAR_ACCESS_ID.Properties.DataSource)).Rows.Count <= 0)
                {
                    XtraMessageBox.Show("选择的血管通路类型尚未建立，请先建立病人的血管通路信息。", "透析处方");
                }
            }
        }


        private void spnFREQUENCY_TIMES_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(spnFREQUENCY_TIMES);
        }

        private void spnFREQUENCY_HOURS_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(spnFREQUENCY_HOURS);
        }

        private void spnSPKT_V_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(spnSPKT_V);

        }

        private void spnURR_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(spnURR);
        }

        private void txtFIRST_PURIFIER_M2_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(txtFIRST_PURIFIER_M2);
        }

        private void txtFIRST_PURIFIER_KOA_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(txtFIRST_PURIFIER_KOA);
        }

        private void txtFIRST_PURIFIER_KUF_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(txtFIRST_PURIFIER_KUF);

        }

        private void txtSECOND_PURIFIER_M2_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(txtSECOND_PURIFIER_M2);
        }

        private void txtSECOND_PURIFIER_KOA_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(txtSECOND_PURIFIER_KOA);
        }

        private void txtSECOND_PURIFIER_KUF_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(txtSECOND_PURIFIER_KUF);
        }

        private void txtSODION_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(txtSODION);
        }

        private void txtPOTASSIUM_ION_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(txtPOTASSIUM_ION);

        }

        private void txtCALCIUM_ION_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(txtCALCIUM_ION);

        }

        private void txtBICARBONATE_RADICAL_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(txtBICARBONATE_RADICAL);

        }

        private void spnBLOOW_FLOW_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(spnBLOOW_FLOW);
        }

        private void txtDIALYSATE_FLOW_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(txtDIALYSATE_FLOW);
        }

        private void txtDIALYSATE_TEMPERATURE_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(txtDIALYSATE_TEMPERATURE);
        }

        private void txtBLOOD_DISPLACEMENT_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(txtBLOOD_DISPLACEMENT);

        }

        private void txtDISPLACEMENT_LIQUID_EditValueChanged(object sender, EventArgs e)
        {
            checkNumberValue(txtDISPLACEMENT_LIQUID);
        }

        private void cmbFIRST_PURIFIER_MODEL_Leave(object sender, EventArgs e)
        {

        }

        private void cmbFIRST_PURIFIER_MODEL_EditValueChanged(object sender, EventArgs e)
        {
            if (this.cmbFIRST_PURIFIER_MODEL.EditValue == null) return;
            var _relationshift = new ConfigModel.MED_COMMON_RELATIONDataTable();
            _relationData.Where(i => i.RELATIONTYPE == "1").CopyToDataTable<ConfigModel.MED_COMMON_RELATIONRow>(_relationshift, LoadOption.PreserveChanges);

            var row = _relationshift.FirstOrDefault(i => i.RELATIONNAME == this.cmbFIRST_PURIFIER_MODEL.EditValue.ToString());

            if (row != null)
            {
                cmbFIRST_PURIFIER_NAME.EditValue = row.ITEMNAME;
                txtFIRST_PURIFIER_M2.Text = row.DOSAGE;
                txtFIRST_PURIFIER_KOA.Text = row.UNIT;
                txtFIRST_PURIFIER_KUF.Text = row.DRUGMODE;
            }
            else
            {
                if (this.cmbFIRST_PURIFIER_MODEL.Text.Equals("威高F15"))
                {
                    this.cmbFIRST_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";
                    this.txtFIRST_PURIFIER_M2.EditValue = 1.5;
                }
                else if (this.cmbFIRST_PURIFIER_MODEL.Text.Equals("FX80"))
                {
                    this.cmbFIRST_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";
                    this.txtFIRST_PURIFIER_M2.EditValue = 1.8;
                }
                else if (this.cmbFIRST_PURIFIER_MODEL.Text.Equals("金宝Polyflux 14L"))
                {
                    this.cmbFIRST_PURIFIER_NAME.EditValue = "52269ca7-b9ba-490a-9164-8199220ec61b";
                    this.txtFIRST_PURIFIER_M2.EditValue = 1.8;
                }
                else if (this.cmbFIRST_PURIFIER_MODEL.Text.Equals("HP130"))
                {
                    this.cmbFIRST_PURIFIER_NAME.EditValue = "92f926c3-cb00-43cf-b4b5-7011597d51e7";
                }
            }
        }

        private void lupRECIPE_TYPE_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl4_Click(object sender, EventArgs e)
        {

        }

        private void cmbSECOND_PURIFIER_MODEL_EditValueChanged(object sender, EventArgs e)
        {
            if (this.cmbSECOND_PURIFIER_MODEL.EditValue == null) return;
            var _relationshift = new ConfigModel.MED_COMMON_RELATIONDataTable();
            _relationData.Where(i => i.RELATIONTYPE == "1").CopyToDataTable<ConfigModel.MED_COMMON_RELATIONRow>(_relationshift, LoadOption.PreserveChanges);

            var row = _relationshift.FirstOrDefault(i => i.RELATIONNAME == this.cmbSECOND_PURIFIER_MODEL.EditValue.ToString());

            if (row != null)
            {
                cmbSECOND_PURIFIER_NAME.EditValue = row.ITEMNAME;
                txtSECOND_PURIFIER_M2.Text = row.DOSAGE;
                txtSECOND_PURIFIER_KOA.Text = row.UNIT;
                txtSECOND_PURIFIER_KUF.Text = row.DRUGMODE;
            }
            else
            {
                if (this.cmbSECOND_PURIFIER_MODEL.Text.Equals("威高F15"))
                {
                    this.cmbSECOND_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";
                    this.txtSECOND_PURIFIER_M2.EditValue = 1.5;
                }
                else if (this.cmbSECOND_PURIFIER_MODEL.Text.Equals("FX80"))
                {
                    this.cmbSECOND_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";
                    this.txtSECOND_PURIFIER_M2.EditValue = 1.8;
                }
                else if (this.cmbSECOND_PURIFIER_MODEL.Text.Equals("金宝Polyflux 14L"))
                {
                    this.cmbSECOND_PURIFIER_NAME.EditValue = "52269ca7-b9ba-490a-9164-8199220ec61b";
                    this.txtSECOND_PURIFIER_M2.EditValue = 1.8;
                }
                else if (this.cmbSECOND_PURIFIER_MODEL.Text.Equals("HP130"))
                {
                    this.cmbSECOND_PURIFIER_NAME.EditValue = "92f926c3-cb00-43cf-b4b5-7011597d51e7";
                }
            }
        }

        private void cmbTHERAPEUTIC_METHOD_EditValueChanged(object sender, EventArgs e)
        {
            if (this.cmbTHERAPEUTIC_METHOD.Text.Equals("低分子肝素抗凝"))
            {
                cmbFIRST_DRUG_UNIT.EditValue = "74e7e438-7f88-4e51-b8f1-376023e803c1";
                cmbFIRST_DRUG_MODE.EditValue = "d18bc2cf-a2f6-4544-ad1c-2dfe93b82d7d";
                cmbSECOND_DRUG_UNIT.EditValue = "74e7e438-7f88-4e51-b8f1-376023e803c1";
                cmbSECOND_DRUG_MODE.EditValue = "d18bc2cf-a2f6-4544-ad1c-2dfe93b82d7d";
            }
            else if (this.cmbTHERAPEUTIC_METHOD.Text.Equals("普通肝素抗凝") || this.cmbTHERAPEUTIC_METHOD.Text.Equals("阿加曲班"))
            {
                this.cmbFIRST_DRUG_UNIT.EditValue = "a0a7c82b-78d8-431c-b9f9-fd78f34c8a4c";
                this.cmbFIRST_DRUG_MODE.EditValue = "d18bc2cf-a2f6-4544-ad1c-2dfe93b82d7d";
                this.cmbSECOND_DRUG_UNIT.EditValue = "a0a7c82b-78d8-431c-b9f9-fd78f34c8a4c";
                this.cmbSECOND_DRUG_MODE.EditValue = "d18bc2cf-a2f6-4544-ad1c-2dfe93b82d7d";
            }
            else
            {
                this.cmbFIRST_DRUG_UNIT.EditValue = string.Empty;
                this.cmbFIRST_DRUG_MODE.EditValue = string.Empty;
                this.cmbSECOND_DRUG_UNIT.EditValue = string.Empty;
                this.cmbSECOND_DRUG_MODE.EditValue = string.Empty;
            }
        }
        #endregion

        #region 给药记录功能模块
        /// <summary>
        /// 给药表
        /// </summary>
        private HemodialysisModel.MED_CURE_DRUGDataTable _CureDrugDatatable;
        /// <summary>
        ///  根据治疗单编号得到该治疗单的临时给要列表
        /// </summary>
        /// <param name="pCureID">治疗单编号</param>
        private void loadGrid(string pCureID)
        {
            //载入给药信息列表
            _CureDrugDatatable = objHemodialysisService.GetCureDrugByCureID(pCureID);
            if (_CureDrugDatatable != null && _CureDrugDatatable.Rows.Count > 0)
            {
                //  gridControl4.DataSource = _CureDrugDatatable;
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == tpreport)
            {
                this.patientRecipeFrm1.HemoId = _hemoID;

                this.patientRecipeFrm1.InzationDateControl();
                this.patientRecipeFrm1.InzationData();
            }
        }
        #endregion
        /// <summary>
        /// 模板变动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPURIFICATION_MODE_EditValueChanged(object sender, EventArgs e)
        {
            if (this.cmbPURIFICATION_MODE.EditValue == null) return;
            if (!string.IsNullOrEmpty(this.cmbPURIFICATION_MODE.EditValue.ToString()))
            {
                this.xtraTabPage2.Text = string.Format("{0}对应的治疗项目", this.cmbPURIFICATION_MODE.Text);
                this.patientMaterialCureUI1.InzationData(this.cmbPURIFICATION_MODE.EditValue.ToString());
            }

            if (this.cmbPURIFICATION_MODE.Text == "HD")
            {
                //this.cmbFIRST_PURIFIER_MODEL.EditValue = "90150299-e0a1-40b8-b9e8-82d504c88f21";//F6HPS
                this.cmbFIRST_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";//聚砜膜
                this.txtFIRST_PURIFIER_M2.EditValue = 1.3;
                this.cmbSECOND_PURIFIER_MODEL.EditValue = null;
                this.cmbSECOND_PURIFIER_NAME.EditValue = null;
                this.txtSECOND_PURIFIER_M2.EditValue = 0;
            }
            else if (this.cmbPURIFICATION_MODE.Text == "HDF")
            {
                this.cmbFIRST_PURIFIER_MODEL.EditValue = "59b5a10f-5a1f-4192-bc70-6be6b40efdb1";//FX80
                this.cmbFIRST_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";//聚砜膜
                this.txtFIRST_PURIFIER_M2.EditValue = 1.8;
                this.cmbSECOND_PURIFIER_MODEL.EditValue = null;
                this.cmbSECOND_PURIFIER_NAME.EditValue = null;
                this.txtSECOND_PURIFIER_M2.EditValue = 0;
            }
            else if (this.cmbPURIFICATION_MODE.Text == "HD+HP")
            {
                //this.cmbFIRST_PURIFIER_MODEL.EditValue = "90150299-e0a1-40b8-b9e8-82d504c88f21";//F6HPS
                this.cmbFIRST_PURIFIER_NAME.EditValue = "7b85aa34-520c-46eb-869a-188391bfbebc";//聚砜膜
                this.txtFIRST_PURIFIER_M2.EditValue = 1.3;
                this.cmbSECOND_PURIFIER_MODEL.EditValue = "61c16922-87a4-4176-8e6c-726b04baea7b";//HP130
                this.cmbSECOND_PURIFIER_NAME.EditValue = "92f926c3-cb00-43cf-b4b5-7011597d51e7";//树脂
                this.txtSECOND_PURIFIER_M2.EditValue = 0;
            }
            else
            {
                //this.cmbFIRST_PURIFIER_MODEL.EditValue = null;
                //this.cmbFIRST_PURIFIER_NAME.EditValue = null;
                //this.txtFIRST_PURIFIER_M2.EditValue = 0;
                //this.cmbSECOND_PURIFIER_MODEL.EditValue = null;
                //this.cmbSECOND_PURIFIER_NAME.EditValue = null;
                //this.txtSECOND_PURIFIER_M2.EditValue = 0;
            }
        }
    }
}