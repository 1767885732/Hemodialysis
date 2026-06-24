/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：医生管理控件备份
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
using Hemo.Client.Base.XtraBaseInfo;
using Hemo.Client.Modules;
using DevExpress.XtraBars.Docking2010.Customization;

namespace Hemo.Client.Controls
{
    public partial class CtlStartMainBak : XtraUserControl
    {
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
        public CardView LayoutViewPatient
        {
            get
            {
                return this.cardView1;
            }
        }

        private PatientModel.MED_PATIENTSRow _patientDocRow;

        public PatientModel.MED_PATIENTSRow PatientDocRow
        {
            get { return _patientDocRow; }
            set { _patientDocRow = value; }
        }


        public XtraTabControl TabPatients
        {
            get
            {
                return this.tabCtrlPatientDetail;
            }
        }

        public DevExpress.XtraBars.Docking.DockPanel DockGuide
        {
            get
            {
                return this.dockGuide;
            }
        }

        /// <summary>
        /// 病人列表
        /// </summary>
        public PatientModel.MED_PATIENTSDataTable PatientDataTable
        {
            get
            {
                return _patientDataTable;
            }
        }

        /// <summary>
        /// 返回列表数据源
        /// </summary>
        public DataTable GridSource
        {
            get
            {
                return (DataTable)grdPatient.DataSource;
            }
        }

        public CtlStartMainBak()
        {
            InitializeComponent();
        }
        private bool MianPageShow = true;
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CtlStartMainBak_Load(object sender, EventArgs e)
        {



        }


        /// <summary>
        /// 根据病人排班信息加载病人列表
        /// </summary>
        public void LoadPatientListSchedule(PatientModel.MED_PATIENTSDataTable pPatientDataTable)
        {
            if (pPatientDataTable != null && pPatientDataTable.Rows.Count > 0)
            {
                grdPatient.DataSource = pPatientDataTable;
                cardView1.FocusedRowHandle = 0;
            }
            else
            {
                grdPatient.DataSource = null;
                cardView1.FocusedRowHandle = 0;
            }
        }

        /// <summary>
        /// 加载病人信息
        /// </summary>
        public void LoadPatientList(string pType)
        {
            this.busyIndicator1.ShowLoadingScreenFor(this.grdPatient);

            _patientDataTable = new PatientModel.MED_PATIENTSDataTable();
            var dtPatient = _patientDataTable.Clone() as PatientModel.MED_PATIENTSDataTable;
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    if (pType == "全部")
                    {
                        _patientDataTable = objPatient.GetPatientList();
                    }
                    else if (pType == "住院" || pType == "门诊")
                    {
                        _patientDataTable = objPatient.GetPatientListByType(pType);
                    }
                    else if ((pType == "在透"))
                    {
                        dtPatient = objPatient.GetPatientList();
                        dtPatient.Where(row => row.IS_NEW.Equals("0") || row.IS_NEW.Equals("2") || row.IS_NEW.Equals("3")).CopyToDataTable(_patientDataTable, LoadOption.OverwriteChanges);
                    }
                    else if ((pType == "转归"))
                    {
                        dtPatient = objPatient.GetPatientList();
                        dtPatient.Where(row => row.IS_NEW.Equals("1") || row.IS_NEW.Equals("2") || row.IS_NEW.Equals("3")).CopyToDataTable(_patientDataTable, LoadOption.OverwriteChanges);
                    }
                    else if ((pType == "死亡"))
                    {
                        dtPatient = objPatient.GetPatientList();
                        dtPatient.Where(row => row.IS_NEW.Equals("1")).CopyToDataTable(_patientDataTable, LoadOption.OverwriteChanges);
                    }
                    foreach (PatientModel.MED_PATIENTSRow patientRow in _patientDataTable)
                    {
                        if (patientRow.SEX == "男")
                        {
                            patientRow.PATIENT_HEAD_PORTRAIT = Utility.BitmapToBytes(Resources.boy);
                        }
                        else
                        {
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
        /// <summary>
        /// 有处方的病人姓名显示为绿色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cardView1_CustomDrawCardCaption(object sender, CardCaptionCustomDrawEventArgs e)
        {
            try
            {
                if (Utility.CInt(this._patientDataTable.Rows[e.RowHandle]["RECIPECOUNT"].ToString()) > 0)
                    e.Appearance.ForeColor = Color.Green;
            }
            catch { }
        }


        private bool UpdatePatientTable(string patientID, bool isAll)
        {
            bool result = false;
            //取到数据后插入到病人表
            HemodialysisModel.MED_PAT_MASTER_INDEXDataTable pMaster;
            if (patientID.Length > 0 && isAll == false)
            {
                pMaster = _hemodialysisService.GetPatientMasterIndexByPatientID(patientID, strWardCode);
            }
            else
            {
                pMaster = _hemodialysisService.GetPatientMasterIndexList(strWardCode);
            }
            PatientModel.MED_PATIENTSDataTable patientDataTable = objPatient.GetPatientList();
            PatientModel.MED_PATIENTSRow patientRowData = null;
            PatientModel.MED_PATIENTSDataTable tmpPatient = new PatientModel.MED_PATIENTSDataTable();
            if (pMaster != null && pMaster.Rows.Count > 0)
            {
                foreach (HemodialysisModel.MED_PAT_MASTER_INDEXRow patIndexRow in pMaster.Rows)
                {

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

                    if (!patIndexRow.IsNAMENull())
                    {
                        patientRowData.NAME = patIndexRow.NAME;
                    }
                    if (!patIndexRow.IsNAME_PHONETICNull())
                    {
                        patientRowData.INPUT_CODE = patIndexRow.NAME_PHONETIC == string.Empty ? PinYinConverter.GetPYString(patIndexRow.NAME) : patIndexRow.NAME_PHONETIC;
                    }
                    if (!patIndexRow.IsSEXNull())
                    {
                        patientRowData.SEX = patIndexRow.SEX;
                    }
                    if (!patIndexRow.IsDATE_OF_BIRTHNull())
                    {
                        patientRowData.BIRTHDAY = patIndexRow.DATE_OF_BIRTH;
                        //根据出生日期计算年龄
                        patientRowData.AGE = Hemo.Utilities.Utility.GetAge(patientRowData.BIRTHDAY.ToShortDateString());
                    }
                    //if (!patIndexRow.IsBIRTH_PLACENull()) {
                    //    patientRowData.NATIVEPLACE = patIndexRow.BIRTH_PLACE;
                    //}
                    if (!patIndexRow.IsID_NONull())
                    {
                        patientRowData.CREDENTIALS_TYPE = "居民身份证";
                        patientRowData.CREDENTIALS_NUMBER = patIndexRow.ID_NO;
                    }
                    if (!patIndexRow.IsNATIONNull())
                    {
                        patientRowData.NATION = patIndexRow.NATION;
                    }
                    if (!patIndexRow.IsPHONE_NUMBER_HOMENull())
                    {
                        patientRowData.WORK_TELEPHONE = patIndexRow.PHONE_NUMBER_HOME;
                    }
                    if (!patIndexRow.IsMAILING_ADDRESSNull())
                    {
                        patientRowData.ADDRESS = patIndexRow.MAILING_ADDRESS;
                    }
                    if (!patIndexRow.IsCHARGE_TYPENull())
                    {
                        patientRowData.MEDICAL_TYPE = patIndexRow.CHARGE_TYPE;
                    }
                    if (!patIndexRow.IsNEXT_OF_KIN_PHONENull())
                    {
                        patientRowData.TELEPHONE = patIndexRow.NEXT_OF_KIN_PHONE;
                    }
                    if (!patIndexRow.IsVISIT_IDNull())
                    {
                        patientRowData.VISIT_ID = patIndexRow.VISIT_ID;
                    }
                    if (!patIndexRow.IsBED_NONull())
                    {
                        patientRowData.BED_NO = patIndexRow.BED_NO.ToString();
                    }
                    if (!patIndexRow.IsADMISSION_DATE_TIMENull())
                    {
                        patientRowData.SPECIFIC_TIME = Utility.CDate(patIndexRow.ADMISSION_DATE_TIME.ToString());
                    }
                    if (!patIndexRow.IsWARD_NAMENull())
                    {
                        patientRowData.WARD_CODE = patIndexRow.WARD_NAME;
                    }
                    if (!patIndexRow.IsDEPT_NAMENull())
                    {
                        patientRowData.WHAT_DEPARTMENT_IN = patIndexRow.DEPT_NAME;
                    }
                    if (!patIndexRow.IsDIAGNOSISNull())
                    {
                        patientRowData.DIAGNOSE = patIndexRow.DIAGNOSIS;
                    }
                    if (!patIndexRow.IsINP_NONull())
                    {
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
        public static bool SynchronizePatientByID(string inpNo)
        {
            InterFaceV4.ParmInputData paramIn = new InterFaceV4.ParmInputData();
            paramIn.performedcode = "HL23";
            paramIn.inpno = inpNo;
            string error = InterFaceV4.InterFaceV4.of_systeminterface("ICUMGR", "HIS", "HIS104", paramIn);//HIS104
            if (error.Length > 0)
            {
                //Sundries.MessageBox("新接口（3.0）报错：" + error, MessageBoxIcon.Error);
                MessageBox.Show("新接口（3.0）报错：" + error);// + MessageBoxIcon.Error
                return false;
            }
            return true;
        }

        /// 同步全部病人基本信息
        /// </summary>
        /// <returns></returns>
        public bool SynchronizeAllPatient(string pPerformedcode)
        {
            string error = InterfaceUtility.SynchronizePatient(pPerformedcode);
            if (error.Length > 0)
            {
                MessageBox.Show("新接口（3.0）报错：" + error);// + MessageBoxIcon.Error
                return false;
            }
            else
                return true;
        }

        #endregion

        #region 事件

        private void btnSync_Click(object sender, EventArgs e)
        {

            if (btnSync.Text == "显示入科病人")
            {
                btnSync.Text = "显示出科病人";
            }
            else
            {
                btnSync.Text = "显示入科病人";
            }
            DataTable dtPatient;
            this.busyIndicator1.ShowLoadingScreenFor(this.grdPatient);
            try
            {
                using (BackgroundWorker worker = new BackgroundWorker())
                {
                    worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                    {
                        if (txtNAME.Text.Length > 0 || txtHEMODIALYSIS_ID.Text.Length > 0)
                        {
                            _patientDataTable = objPatient.GetPatientListByParams(txtNAME.Text.Trim(), txtHEMODIALYSIS_ID.Text.Trim());
                        }
                        else
                        {
                            _patientDataTable = objPatient.GetPatientList();
                        }
                    };
                    worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                    {
                        if (_patientDataTable != null && _patientDataTable.Rows.Count > 0)
                        {
                            if (btnSync.Text == "显示入科病人")
                            {
                                dtPatient = Utility.GetSubTable(_patientDataTable as DataTable, "IS_NEW='1' OR IS_NEW =''");
                            }
                            else
                            {
                                dtPatient = Utility.GetSubTable(_patientDataTable as DataTable, "IS_NEW='0' OR IS_NEW =''");
                            }
                            grdPatient.DataSource = dtPatient;
                            cardView1.FocusedRowHandle = 0;
                        }
                        else
                        {
                            grdPatient.DataSource = null;
                        }
                        rdoGroup.SelectedIndex = 0;
                        busyIndicator1.HideLoadingScreen();

                    };
                    worker.RunWorkerAsync();
                }
            }
            catch (Exception ee)
            {
            }
        }

        /// <summary>
        /// Caption Image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cardView1_CustomCardCaptionImage(object sender, CardCaptionImageEventArgs e)
        {
            //object category = cardView1.GetDataRow(e.RowHandle)["SEX"];
            //for (int i = 0; i < repositoryItemImageComboBox1.Items.Count; i++)
            //    if (((ImageComboBoxItem)repositoryItemImageComboBox1.Items[i]).Value.Equals(category))
            //    {
            //        e.ImageIndex = i;
            //        break;
            //}
            var row = cardView1.GetDataRow(e.RowHandle) as PatientModel.MED_PATIENTSRow;
            if (row.SEX == "男")
            {
                e.ImageIndex = 0;
            }
            else
            {
                e.ImageIndex = 1;
            }
        }

        private void cardView1_DoubleClick(object sender, EventArgs e)
        {


            DataRow dr = this.cardView1.GetFocusedDataRow();

            PatientModel.MED_PATIENTSRow PatientDocRow;
            PatientDocRow = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            if (PatientDocRow != null)
            {
                using (PatientFixInfos FRM = new PatientFixInfos())
                {
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
        private void cardView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //BindMedicalDocment(cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow);
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            try
            {
                InterFaceV4.ParmInputData paramIn = new InterFaceV4.ParmInputData();
                paramIn.patientid = (cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow).PATIENT_ID;//patientID;
                paramIn.visitid = 1;
                string error = InterFaceV4.InterFaceV4.of_systeminterface("ICUMGR", "HIS", "HIS103", paramIn);
                if (error.Length > 0)
                {
                }
            }
            catch (Exception ee)
            {
            }
        }

        /// <summary>
        /// 根据病人姓名、输入吗、透析号查询对应的病人信息 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.busyIndicator1.ShowLoadingScreenFor(this.grdPatient);
            try
            {
                using (BackgroundWorker worker = new BackgroundWorker())
                {
                    DrugModel.MED_PATIENTS_CARDDataTable patient = null;
                    worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                    {
                        if (txtNAME.Text.Length > 0 || txtHEMODIALYSIS_ID.Text.Length > 0)
                        {
                            _patientDataTable = objPatient.GetPatientListByParams(txtNAME.Text.Trim(), txtHEMODIALYSIS_ID.Text.Trim());
                        }
                        else
                        {
                            _patientDataTable = objPatient.GetPatientList();
                        }
                        patient = objPatient.GetCardInfoByInfo(string.Empty, this.txtReadCard.Text.Trim());
                    };
                    worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                    {
                        if (patient != null && patient.Rows.Count > 0)
                        {
                            var p = Hemo.Utilities.Utility.GetSubTable(_patientDataTable, "1=2");
                            _patientDataTable.Where(i => i.HEMODIALYSIS_ID == patient[0].HEMODIALYSIS_ID).CopyToDataTable(p, LoadOption.PreserveChanges);
                            if (p != null && p.Rows.Count > 0)
                            {
                                grdPatient.DataSource = p;
                                cardView1.FocusedRowHandle = 0;
                            }
                            else
                            {
                                grdPatient.DataSource = null;
                            }
                        }
                        else
                        {
                            if (this.txtReadCard.Text.Length > 0)
                            {
                                grdPatient.DataSource = null;
                            }
                            else
                            {
                                if (_patientDataTable != null && _patientDataTable.Rows.Count > 0)
                                {
                                    grdPatient.DataSource = _patientDataTable;
                                    cardView1.FocusedRowHandle = 0;
                                }
                                else
                                {
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
            catch (Exception ee)
            {
            }
        }

        private void rdoGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rdoGroup.SelectedIndex.ToString())
            {
                case "0"://全部
                    LoadPatientList("全部");
                    break;
                case "1"://门诊
                    LoadPatientList("门诊");
                    break;
                case "2"://住院
                    LoadPatientList("住院");
                    break;
                case "3"://在透
                    LoadPatientList("在透");
                    break;
                case "4"://转归
                    LoadPatientList("转归");
                    break;
                case "5"://死亡
                    LoadPatientList("死亡");
                    break;
            }
        }

        /// <summary>
        /// 病人卡片选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutViewPatient_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            PatientModel.MED_PATIENTSRow row = cardView1.GetDataRow(e.FocusedRowHandle) as PatientModel.MED_PATIENTSRow;
            if (row != null)
            {
            }
        }

        private string ConvertToString(object o)
        {
            if (o == null)
                return string.Empty;
            if (o == DBNull.Value || o is DBNull)
                return string.Empty;
            return o.ToString();
        }


        private void cardView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(MousePosition);
            }
        }

        private void 患者处方ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var row = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;

            if (row != null)
            {
                //EditPrescribe frmEditPrescribe = new EditPrescribe(row.HEMODIALYSIS_ID);
                //frmEditPrescribe.ShowDialog();
                //if (frmEditPrescribe.DialogResult == System.Windows.Forms.DialogResult.Yes) {
                //    //this._CtlStartMainBak = new CtlStartMainBak();
                //    //this._CtlStartMainBak.Dock = DockStyle.Fill;
                //    //this.panelControl1.Controls.Add(this._CtlStartMainBak);
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
            else
            {
                XtraMessageBox.Show("请先选择一个病人！", "病患管理");
            }
        }

        private void 促红素ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PatientModel.MED_PATIENTSRow patientRow = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            ErythropoietinFrm erythropoietinFrm = new ErythropoietinFrm(patientRow, false);
            erythropoietinFrm.ShowDialog();
        }

        private void 药品医嘱ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PatientModel.MED_PATIENTSRow patientRow = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            //OrderSearchFrm orderExecFrm = new OrderSearchFrm(patientRow);
            //orderExecFrm.ShowDialog();
            var row = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            if (row != null)
            {
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
            else
            {
                XtraMessageBox.Show("请先选择一个病人！", "病患管理");
            }
        }

        private void 血管通路ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PatientModel.MED_PATIENTSRow patientRow = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            if (patientRow != null)
            {
                EditVascularAccess frmEditVascularAccess = new EditVascularAccess(patientRow.HEMODIALYSIS_ID);
                frmEditVascularAccess.ShowDialog();
            }
            else
            {
                XtraMessageBox.Show("请先选择一个病人！", "病患管理");
            }
        }

        private void 检查检验ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PatientModel.MED_PATIENTSRow patientRow = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            if (patientRow != null)
            {
                LabFrm labFrm = new LabFrm(patientRow);
                labFrm.ShowDialog();
            }
        }

        private void 患者检查ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PatientModel.MED_PATIENTSRow patientRow = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            if (patientRow != null)
            {
                using (ExamFrm examFrm = new ExamFrm())
                {
                    examFrm.PatientRow = patientRow;
                    examFrm.ShowDialog();
                }
            }
        }

        private void 治疗导向ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ShowGudieInfo();
            EditRecipeConfirmList frm = new EditRecipeConfirmList();
            frm.ShowDialog();
        }

        public void ShowGudieInfo()
        {
            var row = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            if (row != null)
            {
                string message = ctlShowGuide1.SetImageStatus(row);
                //AlertInfo info = new AlertInfo("透析导向提醒", message, "", null, "");
                //alertControl1.Show(this.ParentForm, info);
            }
        }

        private void cardView1_MouseUp(object sender, MouseEventArgs e)
        {
            ShowGudieInfo();
        }

        private void 补录治疗记录单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PatientModel.MED_PATIENTSRow patientRow = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            if (patientRow != null)
            {
                string _strCureID = _hemodialysisService.GetCureID(System.DateTime.Now.ToString(), patientRow.HEMODIALYSIS_ID);

                EditTreatment frmTreatment = new EditTreatment(patientRow.HEMODIALYSIS_ID, _strCureID, 0, 1);
                frmTreatment.IsReplenishTreat = true;
                // frmTreatment.ShowDialog();
                //if (frmTreatment.DialogResult == DialogResult.Yes)
                //{

                //}
            }
            else
            {
                XtraMessageBox.Show("请先选择一个病人！", "病患管理");
            }
        }

        private void 删除患者ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string patient_ID = (cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow).HEMODIALYSIS_ID;
            //....
            if (XtraMessageBox.Show("是否确定删除当前患者？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            if (objPatient.DeletePatientByPatient_id(patient_ID) > 0) { LoadPatientList("全部"); }


            //....
        }
        private void 修改患者ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ////根据ModultType获取到ModuleView，然后传入参数以供后边使用
            //((PatientEdit)MainFrm.viewModel.GetModule(ModuleType.PatientEdit)).LoadPatientInfoMehtno(this.cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow);
            ////显示模态窗口
            //Hemo.Client.Base.Common.ViewModel.ShowExistDocument(ModuleType.PatientEdit);
            PatientInfoUI frm = new PatientInfoUI();
            frm.Current = this.cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            frm.InitalizeData();
            FlyoutDialog.Show(this.FindForm(), frm);


            //EditPatientNew frmEditPatient = new EditPatientNew();
            //frmEditPatient.Current = this.cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            //frmEditPatient.ShowDialog();
        }
        private void 新增病历ToolStripMenuItem_Click(object sender, EventArgs e)
        {
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


        private void txtReadCard_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtReadCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQuery_Click(sender, e);
                this.txtReadCard.Focus();
                txtReadCard.SelectAll();
            }
        }

        private void 病程记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow row = cardView1.GetFocusedDataRow();
            if (row != null)
            {
                using (PatientProgressNote frmNote = new PatientProgressNote(row["HEMODIALYSIS_ID"].ToString()))
                {
                    frmNote.ShowDialog();
                }
            }
        }

        private void 治疗记录单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow row = cardView1.GetFocusedDataRow();
            if (row != null)
            {
                //string _strCureID = _hemodialysisService.GetCureID(System.DateTime.Now.ToString(), row["HEMODIALYSIS_ID"].ToString());
                //EditTreatment frmTreatment = new EditTreatment(row["HEMODIALYSIS_ID"].ToString(), _strCureID, 0);
                //frmTreatment.IsReplenishTreat = true;
                //frmTreatment.ShowDialog();

                PatientModel.MED_PATIENTSRow PatientDocRow;
                PatientDocRow = GetPatientRow(row);
                if (row != null)
                {
                    using (PatientFixInfos FRM = new PatientFixInfos())
                    {
                        //    FRM.AreaName = GetAreaName(this.ediSickArea.Text);
                        FRM.PatientDocRow = PatientDocRow;
                        FRM.ShowDialog();
                    }
                }
            }
            else
            {
                AutoClosedMsgBox.ShowForm("请先选择一个病人！", "病患管理", 1000, MessageBoxIcon.Warning);
            }
        }


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

        private void 历次就诊ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var row = cardView1.GetFocusedDataRow();
            if (row != null)
            {
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

        private void uRRKtVTSMDRD评估ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var row = cardView1.GetFocusedDataRow();
            if (row != null)
            {
                using (FrmSufficiencyURR frmSufficiencyURR = new FrmSufficiencyURR())
                {
                    frmSufficiencyURR.HemoId = row["HEMODIALYSIS_ID"].ToString();
                    frmSufficiencyURR.ShowDialog();
                }
            }
        }

        private void 透析充分性评估ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var row = cardView1.GetFocusedDataRow();
            if (row != null)
            {
                using (PatientSufficiency frm = new PatientSufficiency())
                {
                    frm.CurrentHemoId = row["HEMODIALYSIS_ID"].ToString();
                    frm.CurrentHemoName = row["NAME"].ToString();
                    frm.ShowDialog();
                }
            }
        }

        private void 风险评估ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (RiskAssessFrm frm = new RiskAssessFrm())
            {
                frm.ShowDialog();
            }
        }

        private void 营养评估ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var row = cardView1.GetFocusedDataRow();
            if (row != null)
            {
                using (NutritionSGAFrm frm = new NutritionSGAFrm())
                {
                    frm.CurrentHemoId = row["HEMODIALYSIS_ID"].ToString();
                    frm.ShowDialog();
                }
            }
        }

        #endregion
    }
}
