/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：医生管理控件
// 创建时间：2015-08-21
// 创建者：吕志强
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraTab;
using Hemo.Client.Doc;
using Hemo.Client.Properties;
using Hemo.IService;
using Hemo.IService.Config;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.Client.UI.Hemodialysis;
using Hemo.Client.UI.Erythropoietin;
using Hemo.Client.UI.Order;
using Hemo.Client.UI.Lab;
using System.Threading;
using DevExpress.XtraBars.Alerter;
using Hemo.Client.UI.Patient;
using Hemo.Client.UI.PatientFixUI;
using System.Diagnostics;
using Hemo.Client.UI.Assessment;

namespace Hemo.Client.Controls {
    public partial class CtlStartMain : XtraUserControl {
        #region 私有成员

        private PatientModel.MED_PATIENTSDataTable _patientDataTable;
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        CtlMedicalDocumentContainer _medicalDocContainer = new CtlMedicalDocumentContainer();
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private string strWardCode = ConfigurationManager.AppSettings["WardCode"].ToString();
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _purificationModeDataTable;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private VascuarAccessService objVascuarAccess = new VascuarAccessService();
        #endregion

        #region 初始化

        /// <summary>
        /// 病人卡片
        /// </summary>
        public CardView LayoutViewPatient {
            get {
                return this.cardView1;
            }
        }

        private PatientModel.MED_PATIENTSRow _patientDocRow;

        public PatientModel.MED_PATIENTSRow PatientDocRow {
            get { return _patientDocRow; }
            set { _patientDocRow = value; }
        }


        public CardView layoutViewConfirmPatient {
            get {
                return this.editRecipeConfirmListNew1.LayoutViewConfirmPatient;
            }
        }
        public XtraTabControl TabPatients {
            get {
                return this.tabCtrlPatientDetail;
            }
        }

        public DevExpress.XtraBars.Docking.DockPanel DockGuide {
            get {
                return this.dockGuide;
            }
        }
        //  public  

        /// <summary>
        /// 返回标签是否可见
        /// </summary>
        public bool ReturnVisible {
            get {
                return this.lblReturn.Visible;
            }
            set {
                this.lblReturn.Visible = value;
            }
        }

        /// <summary>
        /// 病人列表
        /// </summary>
        public PatientModel.MED_PATIENTSDataTable PatientDataTable {
            get {
                return _patientDataTable;
            }
        }

        /// <summary>
        /// 返回列表数据源
        /// </summary>
        public DataTable GridSource {
            get {
                return (DataTable)grdPatient.DataSource;
            }
        }

        public CtlStartMain() {
            InitializeComponent();
            //tlDocments.ExpandAll();
            LoadPatientList("全部");
            tabCtrlPatientDetail.SelectedTabPageIndex = 2;
        }
        private bool MianPageShow = true;
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CtlStartMain_Load(object sender, EventArgs e) {
            //LoadPatientData();
            editRecipeConfirmListNew1.InitList();
            dockGuide.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            this.tabCtrlPatientDetail.SelectedTabPageIndex = 2;
            this._purificationModeDataTable = this._configService.GetConfigList(string.Empty, string.Empty, "净化方式", "1");

            this.editRecipeConfirmListNew1.ConfirmDoubleClick += delegate(object sd, ConfirmClickEventArgs er)
            {
                if (er.confirmRow != null)
                {
                    MianPageShow = true;
                    tabCtrlPatientDetail.SelectedTabPageIndex = 1;
                    dockGuide.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                    PatientModel.MED_PATIENTSDataTable dtt = new PatientModel.MED_PATIENTSDataTable();

                    var row = dtt.NewMED_PATIENTSRow();
                    row.HEMODIALYSIS_ID = er.confirmRow["HEMODIALYSIS_ID"].ToString();
                    row.PATIENT_ID = er.confirmRow["PATIENT_ID"].ToString();
                    row.NAME = er.confirmRow["NAME"].ToString();
                    row.SEX = er.confirmRow["SEX"].ToString();
                    row.BIRTHDAY = Utility.CDate(er.confirmRow["BIRTHDAY"].ToString());
                    row.AGE = Utility.CDecimal(er.confirmRow["AGE"].ToString());
                    row.NATIVEPLACE = er.confirmRow["NATIVEPLACE"].ToString();
                    row.JOB = er.confirmRow["JOB"].ToString();
                    row.MARITAL = er.confirmRow["MARITAL"].ToString();
                    row.CREDENTIALS_TYPE = er.confirmRow["CREDENTIALS_TYPE"].ToString();
                    row.CREDENTIALS_NUMBER = er.confirmRow["CREDENTIALS_NUMBER"].ToString();
                    row.EDUCATION = er.confirmRow["EDUCATION"].ToString();
                    row.NATION = er.confirmRow["NATION"].ToString();
                    row.WORK_TELEPHONE = er.confirmRow["WORK_TELEPHONE"].ToString();
                    row.ADDRESS = er.confirmRow["ADDRESS"].ToString();
                    row.MEDICAL_TYPE = er.confirmRow["MEDICAL_TYPE"].ToString();
                    row.TELEPHONE = er.confirmRow["TELEPHONE"].ToString();
                    row.TIME_TYPE = er.confirmRow["TIME_TYPE"].ToString();
                    row.SPECIFIC_TIME = Utility.CDate(er.confirmRow["SPECIFIC_TIME"].ToString());
                    row.ADMISSION_NUMBER = er.confirmRow["ADMISSION_NUMBER"].ToString();
                    row.IS_NEW = er.confirmRow["IS_NEW"].ToString();
                    row.WHAT_HOSPITAL_IN = er.confirmRow["WHAT_HOSPITAL_IN"].ToString();
                    row.WHAT_DEPARTMENT_IN = er.confirmRow["WHAT_DEPARTMENT_IN"].ToString();
                    row.FIRST_VISIT = er.confirmRow["FIRST_VISIT"].ToString();
                    row.DIAGNOSE = er.confirmRow["DIAGNOSE"].ToString();
                    row.LEAVE_HOSPITAL_TIME = Utility.CDate(er.confirmRow["LEAVE_HOSPITAL_TIME"].ToString());
                    row.INFECTIOUS_CHECK_RESULT = er.confirmRow["INFECTIOUS_CHECK_RESULT"].ToString();
                    row.INPUT_CODE = er.confirmRow["INPUT_CODE"].ToString();
                    row.WARD_CODE = er.confirmRow["WARD_CODE"].ToString();
                    row.BED_NO = er.confirmRow["BED_NO"].ToString();
                    this.BindDocTree(row);
                    DataSet ds = new DataSet();
                    CtlMedicalDocument document = new CtlMedicalDocument(ds, 0, 0);
                    document.IsShowGrid(true);
                    _medicalDocContainer.CurrentMedicalDocument = document;
                    documentContainerHost.Child = _medicalDocContainer;
                }

            }
            ;

        }

        /// <summary>
        /// 加载病人数据
        /// </summary>
        public void LoadPatientData() {
            this.editRecipeConfirmListNew1.CalculateWeek();
            //BindPatientTreeNodes("全部");
            //BindMedicalDocment();
        }

        /// <summary>
        /// 根据病人排班信息加载病人列表
        /// </summary>
        public void LoadPatientListSchedule(PatientModel.MED_PATIENTSDataTable pPatientDataTable) {
            if (pPatientDataTable != null && pPatientDataTable.Rows.Count > 0) {
                grdPatient.DataSource = pPatientDataTable;
                cardView1.FocusedRowHandle = 0;
            }
            else {
                grdPatient.DataSource = null;
                cardView1.FocusedRowHandle = 0;
            }
        }

        /// <summary>
        /// 加载病人信息
        /// </summary>
        public void LoadPatientList(string pType) {
            this.busyIndicator1.ShowLoadingScreenFor(this.grdPatient);

            using (BackgroundWorker worker = new BackgroundWorker()) {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    if (pType == "全部") {
                        _patientDataTable = objPatient.GetPatientList();
                    }
                    else if (pType == "住院" || pType == "门诊") {
                        _patientDataTable = objPatient.GetPatientListByType(pType);
                    }
                    foreach (PatientModel.MED_PATIENTSRow patientRow in _patientDataTable) {
                        if (patientRow.SEX == "男") {
                            patientRow.PATIENT_HEAD_PORTRAIT = Utility.BitmapToBytes(Resources.boy);
                        }
                        else {
                            patientRow.PATIENT_HEAD_PORTRAIT = Utility.BitmapToBytes(Resources.gril);
                        }
                    }
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    grdPatient.DataSource = _patientDataTable;
                    cardView1.FocusedRowHandle = 0;
                    this.busyIndicator1.HideLoadingScreen();

                };
                worker.RunWorkerAsync();
            }
        }

        #endregion

        #region 方法

        private PatientModel.MED_PATIENTSRow GetPatientRow(DataRow confirmRow)
        {
            PatientModel.MED_PATIENTSDataTable dtt = new PatientModel.MED_PATIENTSDataTable();
            PatientModel.MED_PATIENTSRow row = dtt.NewMED_PATIENTSRow();
            row.HEMODIALYSIS_ID = confirmRow["HEMODIALYSIS_ID"].ToString();
            row.PATIENT_ID = confirmRow["PATIENT_ID"].ToString();
            row.NAME = confirmRow["NAME"].ToString();
            row.SEX = confirmRow["SEX"].ToString();
            if (!string.IsNullOrEmpty(confirmRow["BIRTHDAY"].ToString()))
                row.BIRTHDAY = Convert.ToDateTime(confirmRow["BIRTHDAY"].ToString());

            row.AGE = Convert.ToDecimal(confirmRow["AGE"].ToString());
            row.NATIVEPLACE = confirmRow["NATIVEPLACE"].ToString();
            row.JOB = confirmRow["JOB"].ToString();
            row.MARITAL = confirmRow["MARITAL"].ToString();
            row.CREDENTIALS_TYPE = confirmRow["CREDENTIALS_TYPE"].ToString();
            row.CREDENTIALS_NUMBER = confirmRow["CREDENTIALS_NUMBER"].ToString();
            row.EDUCATION = confirmRow["EDUCATION"].ToString();
            row.NATION = confirmRow["NATION"].ToString();
            row.WORK_TELEPHONE = confirmRow["WORK_TELEPHONE"].ToString();
            row.ADDRESS = confirmRow["ADDRESS"].ToString();
            row.MEDICAL_TYPE = confirmRow["MEDICAL_TYPE"].ToString();
            row.TELEPHONE = confirmRow["TELEPHONE"].ToString();
            row.TIME_TYPE = confirmRow["TIME_TYPE"].ToString();
            row.SPECIFIC_TIME = Convert.ToDateTime(confirmRow["SPECIFIC_TIME"].ToString());
            row.ADMISSION_NUMBER = confirmRow["ADMISSION_NUMBER"].ToString();
            row.IS_NEW = confirmRow["IS_NEW"].ToString();
            row.WHAT_HOSPITAL_IN = confirmRow["WHAT_HOSPITAL_IN"].ToString();
            row.WHAT_DEPARTMENT_IN = confirmRow["WHAT_DEPARTMENT_IN"].ToString();
            row.FIRST_VISIT = confirmRow["FIRST_VISIT"].ToString();
            row.DIAGNOSE = confirmRow["DIAGNOSE"].ToString();
            if (!string.IsNullOrEmpty(confirmRow["LEAVE_HOSPITAL_TIME"].ToString()))
                row.LEAVE_HOSPITAL_TIME = Convert.ToDateTime(confirmRow["LEAVE_HOSPITAL_TIME"]);
            row.INFECTIOUS_CHECK_RESULT = confirmRow["INFECTIOUS_CHECK_RESULT"].ToString();
            row.INPUT_CODE = confirmRow["INPUT_CODE"].ToString();
            row.WARD_CODE = confirmRow["WARD_CODE"].ToString();
            row.BED_NO = confirmRow["BED_NO"].ToString();
            return row;
        }
        /// <summary>
        /// 有处方的病人姓名显示为绿色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cardView1_CustomDrawCardCaption(object sender, CardCaptionCustomDrawEventArgs e) {
            try {
                if (Utility.CInt(this._patientDataTable.Rows[e.RowHandle]["RECIPECOUNT"].ToString()) > 0)
                    e.Appearance.ForeColor = Color.Green;
            }
            catch { }
        }

        private void BindDocTree(PatientModel.MED_PATIENTSRow patientRow) {
            this._patientDocRow = patientRow;
            this.tlDocments.Nodes[0].Nodes.Clear();

            //PatientModel.MED_PATIENTSRow patientRow = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            BindMedicalDocment(patientRow);
            if (patientRow == null)
                return;

            HemodialysisModel.MED_CURE_MAINDataTable cureMainDataTable = this._hemodialysisService.GetMainCureByHemoID(patientRow.HEMODIALYSIS_ID);

            foreach (HemodialysisModel.MED_CURE_MAINRow cureMainRow in cureMainDataTable.Rows) {
                //透析治疗单信息
                //node.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(itmNarBarCureDocument_LinkClicked);

                TreeNode node = new TreeNode();
                node.ImageIndex = 5;
                node.Tag = cureMainRow.CURE_ID;
                if (this._purificationModeDataTable.FindByITEM_ID(cureMainRow.PURIFICATION_MODE) != null) {
                    node.Text = string.Format("{0} {1}", cureMainRow.CURE_CREATE_DATE.ToString("yyyy-MM-dd"), this._purificationModeDataTable.FindByITEM_ID(cureMainRow.PURIFICATION_MODE).ITEM_NAME);
                }
                this.tlDocments.Nodes[0].Nodes.Add(node);
            }

            this.tlDocments.Nodes[0].ExpandAll();
        }

        private void BindMedicalDocment(PatientModel.MED_PATIENTSRow row) {
            //PatientModel.MED_PATIENTSRow row = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            if (row != null) {
                lblName.Text = row.NAME;
                lblAge.Text = row.AGE.ToString();
                lblSex.Text = row.SEX;
                if (row.SEX == "男") {
                    lblPicture.BackgroundImage = Properties.Resources.boy;
                }
                else {
                    lblPicture.BackgroundImage = Properties.Resources.gril;
                }
            }
        }

        private bool UpdatePatientTable(string patientID, bool isAll) {
            bool result = false;
            //取到数据后插入到病人表
            HemodialysisModel.MED_PAT_MASTER_INDEXDataTable pMaster;
            if (patientID.Length > 0 && isAll == false) {
                pMaster = _hemodialysisService.GetPatientMasterIndexByPatientID(patientID, strWardCode);
            }
            else {
                pMaster = _hemodialysisService.GetPatientMasterIndexList(strWardCode);
            }
            PatientModel.MED_PATIENTSDataTable patientDataTable = objPatient.GetPatientList();
            PatientModel.MED_PATIENTSRow patientRowData = null;
            PatientModel.MED_PATIENTSDataTable tmpPatient = new PatientModel.MED_PATIENTSDataTable();
            if (pMaster != null && pMaster.Rows.Count > 0) {
                foreach (HemodialysisModel.MED_PAT_MASTER_INDEXRow patIndexRow in pMaster.Rows) {

                    DataRow[] rows = patientDataTable.Select("PATIENT_ID = '" + patIndexRow.PATIENT_ID + "'");

                    if (rows.Length > 0)
                        patientRowData = rows[0] as PatientModel.MED_PATIENTSRow;

                    if (patientRowData == null) //new
                    {
                        patientRowData = patientDataTable.NewMED_PATIENTSRow();
                        patientRowData.PATIENT_ID = patIndexRow.PATIENT_ID;
                        patientRowData.HEMODIALYSIS_ID = objPatient.GetNewHemoID();
                        patientDataTable.AddMED_PATIENTSRow(patientRowData);
                    }

                    if (!patIndexRow.IsNAMENull()) {
                        patientRowData.NAME = patIndexRow.NAME;
                    }
                    if (!patIndexRow.IsNAME_PHONETICNull()) {
                        patientRowData.INPUT_CODE = patIndexRow.NAME_PHONETIC == string.Empty ? PinYinConverter.GetPYString(patIndexRow.NAME) : patIndexRow.NAME_PHONETIC;
                    }
                    if (!patIndexRow.IsSEXNull()) {
                        patientRowData.SEX = patIndexRow.SEX;
                    }
                    if (!patIndexRow.IsDATE_OF_BIRTHNull()) {
                        patientRowData.BIRTHDAY = patIndexRow.DATE_OF_BIRTH;
                        //根据出生日期计算年龄
                        patientRowData.AGE = Hemo.Utilities.Utility.GetAge(patientRowData.BIRTHDAY.ToShortDateString());
                    }
                    //if (!patIndexRow.IsBIRTH_PLACENull()) {
                    //    patientRowData.NATIVEPLACE = patIndexRow.BIRTH_PLACE;
                    //}
                    if (!patIndexRow.IsID_NONull()) {
                        patientRowData.CREDENTIALS_TYPE = "居民身份证";
                        patientRowData.CREDENTIALS_NUMBER = patIndexRow.ID_NO;
                    }
                    if (!patIndexRow.IsNATIONNull()) {
                        patientRowData.NATION = patIndexRow.NATION;
                    }
                    if (!patIndexRow.IsPHONE_NUMBER_HOMENull()) {
                        patientRowData.WORK_TELEPHONE = patIndexRow.PHONE_NUMBER_HOME;
                    }
                    if (!patIndexRow.IsMAILING_ADDRESSNull()) {
                        patientRowData.ADDRESS = patIndexRow.MAILING_ADDRESS;
                    }
                    if (!patIndexRow.IsCHARGE_TYPENull()) {
                        patientRowData.MEDICAL_TYPE = patIndexRow.CHARGE_TYPE;
                    }
                    if (!patIndexRow.IsNEXT_OF_KIN_PHONENull()) {
                        patientRowData.TELEPHONE = patIndexRow.NEXT_OF_KIN_PHONE;
                    }
                    if (!patIndexRow.IsVISIT_IDNull()) {
                        patientRowData.VISIT_ID = patIndexRow.VISIT_ID;
                    }
                    if (!patIndexRow.IsBED_NONull()) {
                        patientRowData.BED_NO = patIndexRow.BED_NO.ToString();
                    }
                    if (!patIndexRow.IsADMISSION_DATE_TIMENull()) {
                        patientRowData.SPECIFIC_TIME = Utility.CDate(patIndexRow.ADMISSION_DATE_TIME.ToString());
                    }
                    if (!patIndexRow.IsWARD_NAMENull()) {
                        patientRowData.WARD_CODE = patIndexRow.WARD_NAME;
                    }
                    if (!patIndexRow.IsDEPT_NAMENull()) {
                        patientRowData.WHAT_DEPARTMENT_IN = patIndexRow.DEPT_NAME;
                    }
                    if (!patIndexRow.IsDIAGNOSISNull()) {
                        patientRowData.DIAGNOSE = patIndexRow.DIAGNOSIS;
                    }
                    if (!patIndexRow.IsINP_NONull()) {
                        patientRowData.ADMISSION_NUMBER = patIndexRow.INP_NO;
                    }
                    patientRowData = null;
                    objPatient.SavePatientInfo(patientDataTable);
                }

                result = true;
            }
            return result;
        }

        /// <summary>
        /// 根据住院号号同步病人信息
        /// </summary>
        /// <returns></returns>
        public static bool SynchronizePatientByID(string inpNo) {
            InterFaceV4.ParmInputData paramIn = new InterFaceV4.ParmInputData();
            paramIn.performedcode = "HL23";
            paramIn.inpno = inpNo;
            string error = InterFaceV4.InterFaceV4.of_systeminterface("ICUMGR", "HIS", "HIS104", paramIn);//HIS104
            if (error.Length > 0) {
                //Sundries.MessageBox("新接口（3.0）报错：" + error, MessageBoxIcon.Error);
                MessageBox.Show("新接口（3.0）报错：" + error);// + MessageBoxIcon.Error
                return false;
            }
            return true;
        }

        /// 同步全部病人基本信息
        /// </summary>
        /// <returns></returns>
        public bool SynchronizeAllPatient(string pPerformedcode) {
            string error = InterfaceUtility.SynchronizePatient(pPerformedcode);
            if (error.Length > 0) {
                MessageBox.Show("新接口（3.0）报错：" + error);// + MessageBoxIcon.Error
                return false;
            }
            else
                return true;
        }

        #endregion

        #region 事件

        private void btnSync_Click(object sender, EventArgs e) {
            //try {
            //    //this.picLoading.Visible = true;
            //    this.busyIndicator1.ShowLoadingScreenFor(this.grdPatient);

            //    bool result = false;
            //    //同步全部

            //    //先导入病人到麻醉ICU库
            //    using (BackgroundWorker worker = new BackgroundWorker()) {
            //        worker.DoWork += (o1, e1) => {
            //            result = SynchronizeAllPatient(strWardCode);

            //            //同步全部病人到基础病人表
            //        };
            //        worker.RunWorkerCompleted += (o2, e2) => {
            //            if (result) {
            //                result = UpdatePatientTable("", true);
            //                if (result) {
            //                    // this.picLoading.Visible = false;
            //                    this.busyIndicator1.HideLoadingScreen();
            //                    XtraMessageBox.Show("同步病人信息成功！", "病人信息");
            //                    LoadPatientList("全部");
            //                }
            //            }
            //        };
            //        worker.RunWorkerAsync();
            //    }
            //}
            //catch (Exception ex) {
            //    //   this.picLoading.Visible = false;
            //    XtraMessageBox.Show(ex.Message, "病人信息");
            //    this.busyIndicator1.HideLoadingScreen();

            //}
            if (btnSync.Text == "显示入科病人") {
                btnSync.Text = "显示出科病人";
            }
            else {
                btnSync.Text = "显示入科病人";
            }
            DataTable dtPatient;
            this.busyIndicator1.ShowLoadingScreenFor(this.grdPatient);
            try {
                using (BackgroundWorker worker = new BackgroundWorker()) {
                    worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                    {
                        if (txtNAME.Text.Length > 0 || txtHEMODIALYSIS_ID.Text.Length > 0) {
                            _patientDataTable = objPatient.GetPatientListByParams(txtNAME.Text.Trim(), txtHEMODIALYSIS_ID.Text.Trim());
                        }
                        else {
                            _patientDataTable = objPatient.GetPatientList();
                        }
                    };
                    worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                    {
                        if (_patientDataTable != null && _patientDataTable.Rows.Count > 0) {
                            if (btnSync.Text == "显示入科病人") {
                                dtPatient = Utility.GetSubTable(_patientDataTable as DataTable, "IS_NEW='1' OR IS_NEW =''");
                            }
                            else {
                                dtPatient = Utility.GetSubTable(_patientDataTable as DataTable, "IS_NEW='0' OR IS_NEW =''");
                            }
                            grdPatient.DataSource = dtPatient;
                            cardView1.FocusedRowHandle = 0;
                        }
                        else {
                            grdPatient.DataSource = null;
                        }
                        rdoGroup.SelectedIndex = 0;
                        busyIndicator1.HideLoadingScreen();

                    };
                    worker.RunWorkerAsync();
                }
            }
            catch (Exception ee) {
            }
        }

        /// <summary>
        /// Caption Image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cardView1_CustomCardCaptionImage(object sender, CardCaptionImageEventArgs e) {
            //object category = cardView1.GetDataRow(e.RowHandle)["SEX"];
            //for (int i = 0; i < repositoryItemImageComboBox1.Items.Count; i++)
            //    if (((ImageComboBoxItem)repositoryItemImageComboBox1.Items[i]).Value.Equals(category))
            //    {
            //        e.ImageIndex = i;
            //        break;
            //}
            var row = cardView1.GetDataRow(e.RowHandle) as PatientModel.MED_PATIENTSRow;
            if (row.SEX == "男") {
                e.ImageIndex = 0;
            }
            else {
                e.ImageIndex = 1;
            }
        }

        private void cardView1_DoubleClick(object sender, EventArgs e) {
            //MianPageShow = false;
            //tabCtrlPatientDetail.SelectedTabPageIndex = 1;
            //dockGuide.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;

            //this.BindDocTree(cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow);
            //DataSet ds = new DataSet();
            //CtlMedicalDocument document = new CtlMedicalDocument(ds);
            //document.IsShowGrid(true);
            //_medicalDocContainer.CurrentMedicalDocument = document;
            //documentContainerHost.Child = _medicalDocContainer;


            //DataRow dr = this.cardView1.GetFocusedDataRow();

            //PatientModel.MED_PATIENTSRow PatientDocRow;
            //PatientDocRow = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            //if (dr != null) {
            //    PatientKnowBooks FRM = new PatientKnowBooks();
            //    FRM.BindDocTree(PatientDocRow);
            //    FRM.Show();
            //}

            DataRow dr = this.cardView1.GetFocusedDataRow();

            PatientModel.MED_PATIENTSRow PatientDocRow;
            PatientDocRow = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            if (PatientDocRow != null) {
                using (PatientFixInfos FRM = new PatientFixInfos()) {
                    FRM.PatientDocRow = PatientDocRow;
                    FRM.ShowDialog();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cardView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) {
            BindMedicalDocment(cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow);
        }

        private void btnOrders_Click(object sender, EventArgs e) {
            try {
                InterFaceV4.ParmInputData paramIn = new InterFaceV4.ParmInputData();
                paramIn.patientid = (cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow).PATIENT_ID;//patientID;
                paramIn.visitid = 1;
                string error = InterFaceV4.InterFaceV4.of_systeminterface("ICUMGR", "HIS", "HIS103", paramIn);
                if (error.Length > 0) {
                }
            }
            catch (Exception ee) {
            }
        }

        /// <summary>
        /// 根据病人姓名、输入吗、透析号查询对应的病人信息 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e) {
            this.busyIndicator1.ShowLoadingScreenFor(this.grdPatient);
            try {
                using (BackgroundWorker worker = new BackgroundWorker()) {
                    DrugModel.MED_PATIENTS_CARDDataTable patient = null;
                    worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                    {
                        if (txtNAME.Text.Length > 0 || txtHEMODIALYSIS_ID.Text.Length > 0) {
                            _patientDataTable = objPatient.GetPatientListByParams(txtNAME.Text.Trim(), txtHEMODIALYSIS_ID.Text.Trim());
                        }
                        else {
                            _patientDataTable = objPatient.GetPatientList();
                        }
                        patient = objPatient.GetCardInfoByInfo(string.Empty, this.txtReadCard.Text.Trim());
                    };
                    worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                    {
                        if (patient != null && patient.Rows.Count > 0) {
                            var p = Hemo.Utilities.Utility.GetSubTable(_patientDataTable, "1=2");
                            _patientDataTable.Where(i => i.HEMODIALYSIS_ID == patient[0].HEMODIALYSIS_ID).CopyToDataTable(p, LoadOption.PreserveChanges);
                            if (p != null && p.Rows.Count > 0) {
                                grdPatient.DataSource = p;
                                cardView1.FocusedRowHandle = 0;
                            }
                            else {
                                grdPatient.DataSource = null;
                            }
                        }
                        else {
                            if (this.txtReadCard.Text.Length > 0) {
                                grdPatient.DataSource = null;
                            }
                            else {
                                if (_patientDataTable != null && _patientDataTable.Rows.Count > 0) {
                                    grdPatient.DataSource = _patientDataTable;
                                    cardView1.FocusedRowHandle = 0;
                                }
                                else {
                                    grdPatient.DataSource = null;
                                }
                            }
                        }
                        rdoGroup.SelectedIndex = 0;
                        busyIndicator1.HideLoadingScreen();

                    };
                    worker.RunWorkerAsync();
                }
            }
            catch (Exception ee) {
            }
        }

        private void rdoGroup_SelectedIndexChanged(object sender, EventArgs e) {
            switch (rdoGroup.SelectedIndex.ToString()) {
                case "0"://全部
                    LoadPatientList("全部");
                    break;
                case "1"://CRRT
                    LoadPatientList("门诊");
                    break;
                case "2"://急诊
                    LoadPatientList("住院");
                    break;
            }
        }

        /// <summary>
        /// 病人卡片选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutViewPatient_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) {
            PatientModel.MED_PATIENTSRow row = cardView1.GetDataRow(e.FocusedRowHandle) as PatientModel.MED_PATIENTSRow;
            if (row != null) {
                BindMedicalDocment(row);
            }
        }

        /// <summary>
        /// 切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabCtrlPatient_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e) {
            if (e.Page.Text == "患者列表") {
                tabCtrlPatientDetail.SelectedTabPageIndex = 0;
            }
            else {
                tabCtrlPatientDetail.SelectedTabPageIndex = 1;
            }
            this.dockGuide.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
        }

        /// <summary>
        /// 文档选择打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlDocments_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            var row = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;

            if (e.Node.Text == "血液净化治疗知情同意书") {
                血液净化治疗知情同意书 document = new 血液净化治疗知情同意书();
                document.PatientName = row.NAME;
                document.AdmissionNumber = row.IsADMISSION_NUMBERNull() == true ? "" : row.ADMISSION_NUMBER;
                document.Diagnose = row.IsDIAGNOSENull() == true ? "" : row.DIAGNOSE;
                //document.VASCULAR_ACCESS = 

                //默认得到首个血管通路名称
                DataTable dtVA = objVascuarAccess.GetVascularAccessListByHEMODIALYSIS_ID(row.HEMODIALYSIS_ID);
                if (dtVA != null && dtVA.Rows.Count > 0) {
                    document.VASCULAR_ACCESS = dtVA.Rows[0]["VASCULAR_ACCESS_TYPE"].ToString();
                }

                _medicalDocContainer.CurrentMedicalDocument = document;
            }
            else if (e.Node.Text == "连续性肾脏替代治疗知情同意书") {
                连续性肾脏替代治疗知情同意书 document = new 连续性肾脏替代治疗知情同意书();
                if (row != null) {
                    document.PatientRow = row;
                    document.LoadDocumentInfo();
                }
                _medicalDocContainer.CurrentMedicalDocument = document;
            }
            else if (e.Node.Text == "血液灌流知情同意书") {
                血液灌流知情同意书 document = new 血液灌流知情同意书();
                if (row != null) {
                    document.PatientRow = row;
                    document.LoadDocumentInfo();
                }
                _medicalDocContainer.CurrentMedicalDocument = document;
            }
            else if (e.Node.Text == "中心静脉置管术知情同意书") {
                中心静脉置管术知情同意书 document = new 中心静脉置管术知情同意书();
                if (row != null) {
                    document.PatientRow = row;
                    document.LoadDocumentInfo();
                }
                _medicalDocContainer.CurrentMedicalDocument = document;
            }
            else if (e.Node.Text == "动静脉内瘘血管吻合术同意书") {
                动静脉内瘘血管吻合术同意书 document = new 动静脉内瘘血管吻合术同意书();
                if (row != null) {
                    document.PatientRow = row;
                    document.LoadDocumentInfo();
                }
                _medicalDocContainer.CurrentMedicalDocument = document;
            }
            else if (e.Node.Text == "授权委托书") {
                授权委托书 document = new 授权委托书();
                if (row != null) {
                    document.PatientRow = row;
                    document.LoadDocumentInfo();
                }
                _medicalDocContainer.CurrentMedicalDocument = document;
            }
            else if (e.Node.Text == "透析器重复使用申请书") {
                透析器重复使用申请书 document = new 透析器重复使用申请书();
                if (row != null) {
                    document.PatientRow = row;
                    document.LoadDocumentInfo();
                }
                _medicalDocContainer.CurrentMedicalDocument = document;
            }
            else if (e.Node.Text == "血液透析病历") {
                血液透析病历 document = new 血液透析病历();
                if (row != null) {
                    document.PatientRow = row;
                    document.LoadDocumentInfo();
                }
                _medicalDocContainer.CurrentMedicalDocument = document;
            }
            else if (e.Node.Text == "枸橼酸抗凝同意书") {
                枸橼酸抗凝同意书 document = new 枸橼酸抗凝同意书();
                if (row != null) {
                    document.PatientRow = row;
                    document.LoadDocumentInfo();
                }
                _medicalDocContainer.CurrentMedicalDocument = document;

            }
            else if (e.Node.Text == "急诊施行血液灌流同意书") {
                急诊施行血液灌流同意书 document = new 急诊施行血液灌流同意书();
                if (row != null) {
                    document.PatientRow = row;
                    document.LoadDocumentInfo();
                }
                _medicalDocContainer.CurrentMedicalDocument = document;

            }
            else if (e.Node.Text == "抗生素皮试知情同意书") {
                抗生素皮试知情同意书 document = new 抗生素皮试知情同意书();
                if (row != null) {
                    document.PatientRow = row;
                    document.LoadDocumentInfo();
                }
                _medicalDocContainer.CurrentMedicalDocument = document;
            }
            else if (e.Node.Text == "门诊长期血透同意书") {
                门诊长期血透同意书 document = new 门诊长期血透同意书();
                if (row != null) {
                    document.PatientRow = row;
                    document.LoadDocumentInfo();
                }
                _medicalDocContainer.CurrentMedicalDocument = document;
            }
            else if (e.Node.Text == "术后告知") {
                术后告知 document = new 术后告知();

                _medicalDocContainer.CurrentMedicalDocument = document;
            }
            else if (e.Node.Text == "血透病人告知书") {
                血透病人告知书 document = new 血透病人告知书();
                if (row != null) {
                    document.PatientRow = row;
                    document.LoadDocumentInfo();
                }
                _medicalDocContainer.CurrentMedicalDocument = document;
            }
            else if (e.Node.Text == "手术术前风险评估") {
                手术术前风险评估 document = new 手术术前风险评估();
                if (row != null) {
                    document.PatientRow = row;
                    document.LoadDocumentInfo();
                }
                _medicalDocContainer.CurrentMedicalDocument = document;
            }
            else if (e.Node.Text == "手术安全核查表") {
                手术安全核查表 document = new 手术安全核查表();
                if (row != null) {
                    document.PatientRow = row;
                    document.LoadDocumentInfo();
                }
                _medicalDocContainer.CurrentMedicalDocument = document;
            }
            else if (e.Node.Tag != null) {
                DataSet ds = _hemodialysisService.GetAllCure(e.Node.Tag.ToString());

                if (ds != null) {
                    CtlMedicalDocument document = new CtlMedicalDocument(ds, 0, 0);
                    document.IsShowGrid(true);

                    if (ds.Tables["MED_CURE_MAIN"] != null) {
                        if (ConvertToString(ds.Tables["MED_CURE_MAIN"].Rows[0]["SUMMARY2"]).Length > 0) {
                            _medicalDocContainer.HaveNextPage = true;

                        }
                        else { _medicalDocContainer.HaveNextPage = false; }
                    }
                    else {
                        _medicalDocContainer.HaveNextPage = false;
                    }


                    _medicalDocContainer.CurrentMedicalDocument = document;
                }
            }

            documentContainerHost.Child = _medicalDocContainer;
        }
        private string ConvertToString(object o) {
            if (o == null)
                return string.Empty;
            if (o == DBNull.Value || o is DBNull)
                return string.Empty;
            return o.ToString();
        }
        /// <summary>
        /// 隐藏文档窗体，显示病人列表窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblReturn_Click(object sender, EventArgs e) {
            if (MianPageShow) {
                tabCtrlPatientDetail.SelectedTabPageIndex = 2;
            }
            else {
                tabCtrlPatientDetail.SelectedTabPageIndex = 0;
            }
            DataSet ds = new DataSet();
            CtlMedicalDocument document = new CtlMedicalDocument(ds, 0, 0);
            document.IsShowGrid(true);
            _medicalDocContainer.CurrentMedicalDocument = document;
            documentContainerHost.Child = _medicalDocContainer;
            dockGuide.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
        }

        private void lblReturn2_Click(object sender, System.EventArgs e) {
            tabCtrlPatientDetail.SelectedTabPageIndex = 0;
            DataSet ds = new DataSet();
            CtlMedicalDocument document = new CtlMedicalDocument(ds, 0, 0);
            document.IsShowGrid(true);
            _medicalDocContainer.CurrentMedicalDocument = document;
            documentContainerHost.Child = _medicalDocContainer;
            dockGuide.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
        }


        public void InitRecipeConfirmData() {

            //this.editRecipeConfirmListNew1.InitList();

            try {
                this.editRecipeConfirmListNew1.CalculateWeek();
            }
            catch (Exception e) {
                string xx = e.ToString();
            }
        }
        private void cardView1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                contextMenuStrip1.Show(MousePosition);
            }
        }

        private void 患者处方ToolStripMenuItem_Click(object sender, EventArgs e) {
            var row = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;

            if (row != null) {
                //EditPrescribe frmEditPrescribe = new EditPrescribe(row.HEMODIALYSIS_ID);
                //frmEditPrescribe.ShowDialog();
                //if (frmEditPrescribe.DialogResult == System.Windows.Forms.DialogResult.Yes) {
                //    //this._ctlStartMain = new CtlStartMain();
                //    //this._ctlStartMain.Dock = DockStyle.Fill;
                //    //this.panelControl1.Controls.Add(this._ctlStartMain);
                //}
                QueryRecipeList frm = new QueryRecipeList(row.HEMODIALYSIS_ID, 0);
                try
                {
                    frm.currentRecipeIdStr = row["RECIPE_ID"].ToString();
                }
                catch
                {
                    frm.currentRecipeIdStr = string.Empty;
                }
                frm.ShowDialog();
            }
            else {
                XtraMessageBox.Show("请先选择一个病人！", "病患管理");
            }
        }

        private void 促红素ToolStripMenuItem_Click(object sender, EventArgs e) {
            PatientModel.MED_PATIENTSRow patientRow = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            ErythropoietinFrm erythropoietinFrm = new ErythropoietinFrm(patientRow, false);
            erythropoietinFrm.ShowDialog();
        }

        private void 药品医嘱ToolStripMenuItem_Click(object sender, EventArgs e) {
            //PatientModel.MED_PATIENTSRow patientRow = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            //OrderSearchFrm orderExecFrm = new OrderSearchFrm(patientRow);
            //orderExecFrm.ShowDialog();
            var row = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            if (row != null) {
                QueryRecipeList frm = new QueryRecipeList(row.HEMODIALYSIS_ID, 1);
                try
                {
                    frm.currentRecipeIdStr = row["RECIPE_ID"].ToString();
                }
                catch
                {
                    frm.currentRecipeIdStr = string.Empty;
                }
                frm.ShowDialog();
            }
            else {
                XtraMessageBox.Show("请先选择一个病人！", "病患管理");
            }
        }

        private void 血管通路ToolStripMenuItem_Click(object sender, EventArgs e) {
            PatientModel.MED_PATIENTSRow patientRow = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            if (patientRow != null) {
                EditVascularAccess frmEditVascularAccess = new EditVascularAccess(patientRow.HEMODIALYSIS_ID);
                frmEditVascularAccess.ShowDialog();
            }
            else {
                XtraMessageBox.Show("请先选择一个病人！", "病患管理");
            }
        }

        private void 检查检验ToolStripMenuItem_Click(object sender, EventArgs e) {
            PatientModel.MED_PATIENTSRow patientRow = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            if (patientRow != null) {
                LabFrm labFrm = new LabFrm(patientRow);
                labFrm.ShowDialog();
            }
        }

        private void 患者检查ToolStripMenuItem_Click(object sender, EventArgs e) {
            PatientModel.MED_PATIENTSRow patientRow = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            if (patientRow != null) {
                using (ExamFrm examFrm = new ExamFrm()) {
                    examFrm.PatientRow = patientRow;
                    examFrm.ShowDialog();
                }
            }
        }

        private void 治疗导向ToolStripMenuItem_Click(object sender, EventArgs e) {
            // ShowGudieInfo();
            EditRecipeConfirmList frm = new EditRecipeConfirmList();
            frm.ShowDialog();
        }

        public void ShowGudieInfo() {
            var row = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            if (row != null) {
                string message = ctlShowGuide1.SetImageStatus(row);
                //AlertInfo info = new AlertInfo("透析导向提醒", message, "", null, "");
                //alertControl1.Show(this.ParentForm, info);
            }
        }

        private void cardView1_MouseUp(object sender, MouseEventArgs e) {
            ShowGudieInfo();
        }

        private void 补录治疗记录单ToolStripMenuItem_Click(object sender, EventArgs e) {
            PatientModel.MED_PATIENTSRow patientRow = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            if (patientRow != null) {
                string _strCureID = _hemodialysisService.GetCureID(System.DateTime.Now.ToString(), patientRow.HEMODIALYSIS_ID);

                EditTreatment frmTreatment = new EditTreatment(patientRow.HEMODIALYSIS_ID, _strCureID, 0, 1);
                frmTreatment.IsReplenishTreat = true;
               // frmTreatment.ShowDialog();
                //if (frmTreatment.DialogResult == DialogResult.Yes)
                //{

                //}
            }
            else {
                XtraMessageBox.Show("请先选择一个病人！", "病患管理");
            }
        }

        private void 删除患者ToolStripMenuItem_Click(object sender, EventArgs e) {
            string patient_ID = (cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow).HEMODIALYSIS_ID;
            //....
            if (XtraMessageBox.Show("是否确定删除当前患者？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            if (objPatient.DeletePatientByPatient_id(patient_ID) > 0) { LoadPatientList("全部"); }


            //....
        }
        private void 修改患者ToolStripMenuItem_Click(object sender, System.EventArgs e) {
            EditPatientNew frmEditPatient = new EditPatientNew();
            frmEditPatient.Current = this.cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            frmEditPatient.ShowDialog();
        }
        private void 新增病历ToolStripMenuItem_Click(object sender, EventArgs e) {
            var row = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            //Hemo.Client.UI.Patient.PatientRecord _patientRecord = new UI.Patient.PatientRecord();
            //_patientRecord.currentHemoID = row.HEMODIALYSIS_ID;
            //_patientRecord.ShowDialog();

            //PatientRecordNew record = new PatientRecordNew();
            //record.CurrentHemoId = row.HEMODIALYSIS_ID;
            //record.ShowDialog();

            PatientBaseRecord record = new PatientBaseRecord();
            record.HemoId = row.HEMODIALYSIS_ID;
            record.ShowDialog();
        }

        private void tabCtrlPatientDetail_SelectedPageChanged(object sender, TabPageChangedEventArgs e) {
            if (tabCtrlPatientDetail.SelectedTabPageIndex == 2) {
                this.editRecipeConfirmListNew1.InitList();
                this.editRecipeConfirmListNew1.CalculateWeek();
            }
        }

        private void txtReadCard_KeyPress(object sender, KeyPressEventArgs e) {

        }

        private void txtReadCard_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                btnQuery_Click(sender, e);
                this.txtReadCard.Focus();
                txtReadCard.SelectAll();
            }
        }

        private void 病程记录ToolStripMenuItem_Click(object sender, EventArgs e) {
            DataRow row = cardView1.GetFocusedDataRow();
            if (row != null) {
                using (PatientProgressNote frmNote = new PatientProgressNote(row["HEMODIALYSIS_ID"].ToString())) {
                    frmNote.ShowDialog();
                }
            }
        }

        private void 治疗记录单ToolStripMenuItem_Click(object sender, EventArgs e) {
            DataRow row = cardView1.GetFocusedDataRow();
            if (row != null) {
                //string _strCureID = _hemodialysisService.GetCureID(System.DateTime.Now.ToString(), row["HEMODIALYSIS_ID"].ToString());
                //EditTreatment frmTreatment = new EditTreatment(row["HEMODIALYSIS_ID"].ToString(), _strCureID, 0);
                //frmTreatment.IsReplenishTreat = true;
                //frmTreatment.ShowDialog();

                PatientModel.MED_PATIENTSRow PatientDocRow;
                PatientDocRow = GetPatientRow(row);
                if (row != null) {
                    using (PatientFixInfos FRM = new PatientFixInfos()) {
                        //    FRM.AreaName = GetAreaName(this.ediSickArea.Text);
                        FRM.PatientDocRow = PatientDocRow;
                        FRM.ShowDialog();
                    }
                }
            }
            else {
                AutoClosedMsgBox.ShowForm("请先选择一个病人！", "病患管理", 1000, MessageBoxIcon.Warning);
            }
        }


        private void 历次就诊ToolStripMenuItem_Click(object sender, EventArgs e) {
            var row = cardView1.GetFocusedDataRow();
            if (row != null) {
                string file = ConfigurationManager.AppSettings["EMRS_Path"].ToString();

                //SecureString password = new SecureString();
                //string pass = "SUPCON";
                //pass.ToCharArray().ToList().ForEach(c => password.AppendChar(c));

                ProcessStartInfo info = new ProcessStartInfo(file);
                //info.UserName = "SUPCON";
                //info.Password = password;
                info.Arguments = string.Format("{0} {1} {2}", "SUPCON", "SUPCON" + "#" + row["PATIENT_ID"].ToString() + "#" + row["VISIT_ID"].ToString(), "SUPCON" + "#" + row["PATIENT_ID"].ToString() + "#" + row["VISIT_ID"].ToString());

                Process process = new Process();
                process.StartInfo = info;
                process.Start();
                this.ParentForm.WindowState = (this.ParentForm.WindowState != FormWindowState.Minimized) ? FormWindowState.Minimized : this.ParentForm.WindowState;
            }
        }

        private void uRRKtVTSMDRD评估ToolStripMenuItem_Click(object sender, EventArgs e) {
            var row = cardView1.GetFocusedDataRow();
            if (row != null) {
                using (FrmSufficiencyURR frmSufficiencyURR = new FrmSufficiencyURR()) {
                    frmSufficiencyURR.HemoId = row["HEMODIALYSIS_ID"].ToString();
                    frmSufficiencyURR.ShowDialog();
                }
            }
        }

        private void 透析充分性评估ToolStripMenuItem_Click(object sender, EventArgs e) {
            var row = cardView1.GetFocusedDataRow();
            if (row != null) {
                using (PatientSufficiency frm = new PatientSufficiency()) {
                    frm.CurrentHemoId = row["HEMODIALYSIS_ID"].ToString();
                    frm.CurrentHemoName = row["NAME"].ToString();
                    frm.ShowDialog();
                }
            }
        }

        private void 风险评估ToolStripMenuItem_Click(object sender, EventArgs e) {
            using (RiskAssessFrm frm = new RiskAssessFrm()) {
                frm.ShowDialog();
            }
        }

        private void 营养评估ToolStripMenuItem_Click(object sender, EventArgs e) {
            var row = cardView1.GetFocusedDataRow();
            if (row != null) {
                using (NutritionSGAFrm frm = new NutritionSGAFrm()) {
                    frm.CurrentHemoId = row["HEMODIALYSIS_ID"].ToString();
                    frm.ShowDialog();
                }
            }
        }


        #endregion
    }
}
