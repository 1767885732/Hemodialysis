/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者信息编辑类
// 创建时间：2017-03-18
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Client.UI.Machine;
using Hemo.Model;
using System.Linq;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.IService;
using DevExpress.XtraEditors.Controls;
using Hemo.IService.Config;
using Hemo.Client.UI.Hemodialysis;
using System.Configuration;
using System.Drawing.Imaging;
using Hemo.Client.UI.Patient;
using DevExpress.XtraEditors;
using System.IO;
using Hemo.Client.Modules;
using Hemo.Client.Core;
using Hemo.HQCWebClient;
using Hemo.HQCWebClient.Models;
using Newtonsoft.Json;
using Docare.BarcodeInput;
using Hemo.Client.Controls;
using Hemo.Client.UI.Config;
using DevExpress.XtraBars.Docking2010.Customization;
using Hemo.WinForm;

namespace Hemo.Client.UI.PatientFixUI
{
    public partial class PatientInfoUI : ViewBase
    {
        #region 成员变量

        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        private IConfig _configService = ServiceManager.Instance.ConfigService;

        private IPatient objPatient = ServiceManager.Instance.PatientService;

        private PatientModel.MED_PATIENTSDataTable _patientDataTable;

        private PatientModel.MED_PATIENTSDataTable _allPatients;

        private DrugModel.MED_PATIENTS_CARDDataTable _patientCard = new DrugModel.MED_PATIENTS_CARDDataTable();

        private PatientModel.MED_PATIENTSRow _current;

        private string currentCardNo = string.Empty;

        private string addNewPatientRecord = string.Empty;

        private string strWardCode = ConfigurationManager.AppSettings["WardCode"].ToString();

        private const double _DefaultMaxPicWidth = 80;

        private const double _DefaultMaxPicHeight = 40;

        private bool isNew = false;

        #endregion

        #region 属性

        /// <summary>
        /// 获取或者设置当前的患者
        /// </summary>
        public PatientModel.MED_PATIENTSRow Current
        {
            get
            {
                return _current;
            }
            set
            {
                _current = value;
            }
        }

        public string GetAddPatientHemoID
        {
            get { return addNewPatientRecord; }
        }

        #endregion

        #region 构造函数

        public PatientInfoUI()
        {
            InitializeComponent();
            loadDefaultValue();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientInfoUI_Load(object sender, EventArgs e)
        {
            //InitalizeData();
            this.Text = "患者基本信息";

            // ProFunctionCount pfc = new ProFunctionCount();
            //   pfc.SaveFunctionCountUI(this);
            datCREATE_DATE.Text = System.DateTime.Now.ToShortDateString();
            isNew = _current == null ? true : false;
            this.txtPATIENT_ID.Focus();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnSave_Click(object sender, EventArgs e)
        {
            currentCardNo = string.Empty;
            if (IsDataValidate())
            {
                try
                {
                    addNewPatientRecord = this.txtHEMODIALYSIS_ID.Text.Trim();
                    if (SaveData() > 0)
                    {
                        //更新当前治疗单记录中传染病检验结果
                        DataTable dtCureMain = _hemodialysisService.GetMainCureByHemoID(this.txtHEMODIALYSIS_ID.Text.Trim());
                        if (dtCureMain != null && dtCureMain.Rows.Count > 0)
                        {
                            var row = dtCureMain.AsEnumerable().FirstOrDefault(r => Utility.CDate(r["CURE_CREATE_DATE"].ToString()).ToShortDateString().Equals(DateTime.Now.ToShortDateString()));
                            if (row != null)
                            {
                                row["INFECTIOUS_CHECK_RESULT"] = this.txtINFECTIOUS_CHECK_RESULT.Text.Trim();
                                _hemodialysisService.SaveCureMain(dtCureMain as HemodialysisModel.MED_CURE_MAINDataTable);
                            }
                        }
                        AutoClosedMsgBox.ShowForm("保存成功！", this.Text, 2000, MessageBoxIcon.Asterisk);
                        this.ParentForm.DialogResult = DialogResult.OK;
                    }

                }
                catch (Exception ex)
                {
                    AutoClosedMsgBox.ShowForm(ex.Message, this.Text, 2000, MessageBoxIcon.Error);

                }
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Parent.Parent.Name.Contains("PatientEdit"))
                {
                    ((PatientEdit)this.Parent.Parent).CloseCurrentDocument();

                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnAdd_Click(object sender, EventArgs e)
        {
            _patientDataTable = new PatientModel.MED_PATIENTSDataTable();
            this.mEDPATIENTSRowBindingSource.DataSource = _patientDataTable;
            this.mEDPATIENTSRowBindingSource.AddNew();
            //this.txtHEMODIALYSIS_ID.Text = objPatient.GetNewHemoID();
            this.cbxSPECIFIC_TIME.DateTime = DateTime.Now;

        }

        /// <summary>
        /// 生日文本区失去光标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBIRTHDAY_Leave(object sender, EventArgs e)
        {
            getAge();
        }

        /// <summary>
        /// 制卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnMakeCard_Click(object sender, EventArgs e)
        {
            this.picMain.Focus();

            currentCardNo = "btnMakeCard_Click";
            if (IsDataValidate())
            {
                try
                {
                    EditPatientCardInfo frm = new EditPatientCardInfo();
                    frm.HemoId = this.txtHEMODIALYSIS_ID.Text;
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        using (var _worker = new BackgroundWorker())
                        {
                            BusyIndicatorHelp busyIndicatorHelp = new BusyIndicatorHelp();

                            busyIndicatorHelp.ShowMessage();
                            busyIndicatorHelp.SetWaitFormCaption("数据保存中....");

                            this._patientCard = frm._patientCard;
                            addNewPatientRecord = this.txtHEMODIALYSIS_ID.Text.Trim();
                            int SaveReturnIn = -1;
                            _worker.DoWork += delegate (object o, DoWorkEventArgs e11)
                            {
                                System.Threading.Thread.Sleep(1000);
                                SaveReturnIn = SaveData();
                            };
                            _worker.RunWorkerCompleted += delegate (object o1, RunWorkerCompletedEventArgs e1)
                            {
                                if (SaveReturnIn > 0)
                                {
                                    busyIndicatorHelp.SetWaitFormCaption("数据保存成功....");
                                    busyIndicatorHelp.HideMessage();
                                    this.ParentForm.DialogResult = DialogResult.OK;
                                }
                                else
                                {
                                    busyIndicatorHelp.HideMessage();
                                    MessageBoxFrm boxfrm = new MessageBoxFrm();
                                    boxfrm.SetCaption("数据保存失败", "警告");
                                    boxfrm.ShowDialog();
                                }

                            };
                            _worker.RunWorkerAsync();
                        }

                    }
                }
                catch (Exception ex)
                {
                    AutoClosedMsgBox.ShowForm(ex.Message, "病人信息", 2000, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 住院号按下键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtADMISSION_NUMBER_KeyDown(object sender, KeyEventArgs e)
        {
            this.cbxTIME_TYPE.Text = "住院";
            if (e.KeyCode == Keys.Enter)
            {
                //修改时不去同步数据
                //if (_current == null)
                SynPatientAndGetPatientInfosByPatientID(this.txtADMISSION_NUMBER.Text.Trim(), "住院");
            }
        }

        /// <summary>
        /// 拍照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnPicture_Click(object sender, EventArgs e)
        {
            PatientPictureAction frm = new PatientPictureAction();
            frm.Text = "患者头像采集";
            if (this.picMain.Image != null)
                frm.SetPicturePriwView(this.picMain.Image);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.picMain.Image = frm.PatientPicture;
                //保存患者照片
                SaveData();
            }
        }

        /// <summary>
        /// 双击照片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picMain_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                PreviewPictureFrm picPreviewFrm = new PreviewPictureFrm();
                picPreviewFrm.DisplayImage(this.picMain.Image);
                picPreviewFrm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 年龄输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAGE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//这是允许输入退格键
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0-9数字
                {
                    e.Handled = true;
                }
            }
        }

        private void btnShowIcd_Click(object sender, EventArgs e)
        {
            ShowICDList frm = new ShowICDList(txtFIRST_VISIT.Text);
            frm.ShowDialog();
            txtFIRST_VISIT.Text += frm.IcdList;
        }

        /// <summary>
        /// 点击诊断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDIAGNOSE_Click(object sender, EventArgs e)
        {
            ShowICDList frm = new ShowICDList(txtFIRST_VISIT.Text);
            frm.ShowDialog();
            txtDIAGNOSE.Text += frm.IcdList;
        }

        /// <summary>
        /// ID按下键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPATIENT_ID_KeyDown(object sender, KeyEventArgs e)
        {
            this.cbxTIME_TYPE.Text = string.Empty;
            if (e.KeyCode == Keys.Enter)
            {
                //if (_current == null)
                SynPatientAndGetPatientInfosByPatientID(this.txtPATIENT_ID.Text.Trim(), string.Empty);
            }
        }
        /// <summary>
        /// 隐藏关闭按钮
        /// </summary>
        public void SetBtnCloseHide()
        {
            this.btnClose.Visible = false;
            this.panelControl2.Width = 775;
        }
        /// <summary>
        /// 身份证校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCREDENTIALS_NUMBER_Validated(object sender, EventArgs e)
        {
            if (cbxCREDENTIALS_TYPE.EditValue.ToString() != "居民身份证")
                return;

            IdentityCardValidted();
            this.txtTELEPHONE.Focus();
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpToWeb_Click(object sender, EventArgs e)
        {
            //上传之前先检查是否已经上传过治疗信息
            if (this.chkIsUp.Checked)
            {
                DialogResult dialog = XtraMessageBox.Show("患者信息已经上传过，是否继续？", "患者信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.Cancel)
                { return; }
            }

            if (!IsDataValidate())
            { return; }

            this.busyIndicator1.Show();

            string msg = string.Empty;
            bool success = true;
            ConfigModel.MED_COMMON_ITEMLISTDataTable dtConfig = _configService.GetConfigList(string.Empty, string.Empty, "质控平台访问配置", "1");
            string userName = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("QCLoginName")).ITEM_VALUE;
            string saveApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("SaveMedPatient")).ITEM_VALUE;
            string getUserApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("GetUserByName")).ITEM_VALUE;
            string getTokenApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("GetToken")).ITEM_VALUE;

            MessageLog.Instance().Log(new LogEntity() { Type = userName + "--" + saveApi + "--" + getUserApi + "--" + getTokenApi });

            //获取用户信息
            ResultMsg<MedUserInfo> resultMsg = WebApiClient.GetUserByName(userName, getUserApi, getTokenApi);
            if (resultMsg.StatusCode == (int)StatusCodeEnum.Success)
            {
                MessageLog.Instance().Log(new LogEntity() { Type = resultMsg.Data.ToString() });
                var userInfo = JsonConvert.DeserializeObject<MedUserInfo>(resultMsg.Data.ToString());
                if (userInfo != null)
                {
                    var row = _patientDataTable.Rows.Count > 0 ? _patientDataTable[0] : null;
                    if (row != null)
                    {
                        MedPatientsInfo info = new MedPatientsInfo();
                        info.ID = Guid.NewGuid().ToString();
                        info.HospitalId = userInfo.Company_ID;
                        info.HospitalName = userInfo.CompanyName;
                        info.HospitalYear = DateTime.Now;
                        info.Createtime = DateTime.Now;
                        info.Creator = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                        info.Name = row.NAME;
                        info.Address = row.ADDRESS;
                        info.AdmissionNumber = row.ADMISSION_NUMBER;
                        info.Age = Convert.ToInt32(row.AGE);
                        info.BedNo = row.BED_NO;
                        info.Birthday = row.BIRTHDAY;
                        info.CredentialsNumber = row.CREDENTIALS_NUMBER;
                        info.CredentialsType = row.CREDENTIALS_TYPE;
                        info.Diagnose = row.DIAGNOSE;
                        info.Education = row.EDUCATION;
                        info.FirstVisit = row.FIRST_VISIT;
                        info.HemodialysisId = row.HEMODIALYSIS_ID;
                        info.InfectiousCheckResult = row.INFECTIOUS_CHECK_RESULT;
                        info.InputCode = row.INPUT_CODE;
                        info.IsDelete = 0;
                        info.IsNew = row.IS_NEW;
                        info.Job = row.JOB;
                        info.LeaveHospitalTime = row.LEAVE_HOSPITAL_TIME;
                        info.Marital = row.MARITAL;
                        info.MedicalType = row.MEDICAL_TYPE;
                        info.Nation = row.NATION;
                        info.Nativeplace = row.NATIVEPLACE;
                        info.PatientId = row.PATIENT_ID;
                        info.Sex = row.SEX;
                        info.SpecificTime = row.SPECIFIC_TIME;
                        info.Telephone = row.TELEPHONE;
                        info.TimeType = row.TIME_TYPE;
                        //info.VisitId = row.VISIT_ID;
                        info.WardCode = row.WARD_CODE;
                        info.WhatDepartmentIn = row.WHAT_DEPARTMENT_IN;
                        info.WhatHospitalIn = row.WHAT_HOSPITAL_IN;
                        info.WorkTelephone = row.WORK_TELEPHONE;
                        ResultMsg<string> result = WebApiClient.SavePatientInfo(info, saveApi, getTokenApi);
                        if (result.StatusCode != (int)StatusCodeEnum.Success)
                        {
                            success = false;
                            msg = result.Info;
                        }
                    }
                    else
                    {
                        success = false;
                        msg = "患者信息为空！";
                    }
                }
                else
                {
                    success = false;
                    msg = "质控平台用户信息为空！";
                }
            }
            else
            {
                success = false;
                msg = resultMsg.Info;
            }

            if (success)
            {
                //更新上传标识
                _patientDataTable[0].IS_UP = "1";
                objPatient.SavePatientInfo(_patientDataTable);
                this.chkIsUp.Checked = true;

                //记录上传日志
                var dtUploadLog = new HemodialysisModel.MED_UPLOAD_LOGDataTable();
                var drUploadLog = dtUploadLog.NewMED_UPLOAD_LOGRow();
                drUploadLog.ID = Guid.NewGuid().ToString();
                drUploadLog.UPLOAD_ITEM_NAME = "患者信息";
                drUploadLog.HEMODIALYSIS_ID = this.txtHEMODIALYSIS_ID.Text;
                drUploadLog.BELONG_YEAR = DateTime.Now.ToShortDateString();
                drUploadLog.UPLOADER = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                drUploadLog.UPLOAD_DATE = DateTime.Now;
                dtUploadLog.AddMED_UPLOAD_LOGRow(drUploadLog);
                _hemodialysisService.SaveUploadLog(dtUploadLog);

                this.busyIndicator1.HideLoadingScreen();
                XtraMessageBox.Show("上传成功！");
            }
            else
            {
                this.busyIndicator1.HideLoadingScreen();
                XtraMessageBox.Show("上传失败！\r\n" + msg);
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 载入初始值
        /// </summary>
        private void loadDefaultValue()
        {
            DataTable dt = this._configService.GetConfigList(string.Empty, string.Empty, "民族", "1");
            if (dt != null && dt.Rows.Count > 0)
            {
                cbxNATION.Properties.DataSource = dt;//绑定数据源
                cbxNATION.Properties.PopupFormSize = new Size(120, 230);
                cbxNATION.Properties.DisplayMember = "ITEM_NAME";//要显示的字段,Text获得
                cbxNATION.Properties.ValueMember = "ITEM_NAME";//实际值的字段,EditValue获得 // DeptID
            }
        }

        /// <summary>
        /// 初使化数据
        /// </summary>
        public void InitalizeData()
        {
            this.Enabled = false;

            _patientDataTable = new PatientModel.MED_PATIENTSDataTable();
            _allPatients = new PatientModel.MED_PATIENTSDataTable();
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate (object sender1, DoWorkEventArgs e1)
                {
                    if (_current != null)
                    {
                        var row = _patientDataTable.NewMED_PATIENTSRow();
                        row.HEMODIALYSIS_ID = _current.HEMODIALYSIS_ID;
                        if (!_current.IsPATIENT_IDNull())
                            row.PATIENT_ID = _current.PATIENT_ID;
                        row.NAME = _current.NAME;
                        row.SEX = _current.SEX;
                        if (!_current.IsBIRTHDAYNull())
                            row.BIRTHDAY = _current.BIRTHDAY;
                        row.AGE = _current.AGE;
                        row.NATIVEPLACE = _current.NATIVEPLACE;
                        row.JOB = _current.JOB;
                        row.MARITAL = _current.MARITAL;
                        row.CREDENTIALS_TYPE = _current.CREDENTIALS_TYPE;
                        row.CREDENTIALS_NUMBER = _current.CREDENTIALS_NUMBER;
                        row.EDUCATION = _current.EDUCATION;
                        row.NATION = _current.NATION;
                        row.WORK_TELEPHONE = _current.WORK_TELEPHONE;
                        row.ADDRESS = _current.ADDRESS;
                        row.MEDICAL_TYPE = _current.MEDICAL_TYPE;
                        row.TELEPHONE = _current.TELEPHONE;
                        row.TIME_TYPE = _current.TIME_TYPE;
                        if (!_current.IsSPECIFIC_TIMENull())
                            row.SPECIFIC_TIME = _current.SPECIFIC_TIME;
                        row.ADMISSION_NUMBER = _current.ADMISSION_NUMBER;
                        row.IS_NEW = _current.IS_NEW;
                        row.WHAT_HOSPITAL_IN = _current.WHAT_HOSPITAL_IN;
                        row.WHAT_DEPARTMENT_IN = _current.WHAT_DEPARTMENT_IN;
                        row.FIRST_VISIT = _current.FIRST_VISIT;
                        row.DIAGNOSE = _current.DIAGNOSE;
                        if (!_current.IsLEAVE_HOSPITAL_TIMENull())
                            row.LEAVE_HOSPITAL_TIME = _current.LEAVE_HOSPITAL_TIME;
                        if (!_current.IsCREATE_DATENull())
                            row.CREATE_DATE = _current.CREATE_DATE;
                        row.INFECTIOUS_CHECK_RESULT = _current.INFECTIOUS_CHECK_RESULT;
                        row.INPUT_CODE = _current.INPUT_CODE;
                        row.WARD_CODE = _current.WARD_CODE;
                        row.BED_NO = _current.BED_NO;
                        row.PAT_LABEL = _current.IsPAT_LABELNull() ? "" : _current.PAT_LABEL;
                        var patientPicDt = objPatient.GetPatientPicByHemoId(_current.HEMODIALYSIS_ID);
                        if (patientPicDt != null && patientPicDt.Rows.Count > 0)
                        {
                            _current.PAT_PIC = patientPicDt[0].PAT_PIC;
                        }

                        if (_current["PAT_PIC"] != DBNull.Value)
                        {
                            row.PAT_PIC = _current.PAT_PIC;
                        }

                        var dtPatient = objPatient.GetPatientListByPatientID(_current.PATIENT_ID);
                        if (dtPatient != null && dtPatient.Rows.Count > 0)
                        {
                            _current.IS_UP = dtPatient[0].IS_UP;
                        }
                        row.IS_UP = _current.IS_UP;
                        _patientDataTable.AddMED_PATIENTSRow(row);
                        row.AcceptChanges();
                        row.SetModified();
                    }
                    else
                    {
                        _patientDataTable = new PatientModel.MED_PATIENTSDataTable();
                    }
                    _allPatients = objPatient.GetPatientList();
                };
                worker.RunWorkerCompleted += delegate (object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.mEDPATIENTSRowBindingSource.DataSource = _patientDataTable;

                    if (_current == null)
                    {
                        this.mEDPATIENTSRowBindingSource.AddNew();
                        //this.txtHEMODIALYSIS_ID.Text = objPatient.GetNewHemoID();
                        this.txtDIAGNOSE.Text = "尿毒症";
                        this.cbxTIME_TYPE.Text = "门诊";
                        this.cmbIS_NEW.Text = "入科";
                        this.cbxSPECIFIC_TIME.DateTime = DateTime.Now;
                        this.cbxLEAVE_HOSPITAL_TIME.EditValue = DateTime.Now;
                        this.chkIsUp.Checked = false;

                        this.txtHEMODIALYSIS_ID.Text = objPatient.GetNewHemoID();

                        //DataTable dtHemoId = objPatient.GetTempHemoId();
                        //dtHemoId.TableName = "MED_TEMP_HEMOID";
                        //if (dtHemoId.Rows.Count > 0)
                        //{
                        //    DataTable dtFind = objPatient.GetPatientListByParams(string.Empty, dtHemoId.Rows[0]["HEMODIALYSIS_ID"].ToString());
                        //    if (dtFind.Rows.Count > 0)
                        //    {
                        //        string hemoId = objPatient.GetNewHemoID();
                        //        dtHemoId.Rows[0]["HEMODIALYSIS_ID"] = hemoId;
                        //        objPatient.SaveTempHemoId(dtHemoId);
                        //        this.txtHEMODIALYSIS_ID.Text = hemoId;
                        //    }
                        //    else
                        //    {
                        //        this.txtHEMODIALYSIS_ID.Text = dtHemoId.Rows[0]["HEMODIALYSIS_ID"].ToString();
                        //    }
                        //}
                        //else
                        //{
                        //    string hemoId = objPatient.GetNewHemoID();
                        //    DataRow row = dtHemoId.NewRow();
                        //    row["ID"] = Guid.NewGuid().ToString();
                        //    row["HEMODIALYSIS_ID"] = hemoId;
                        //    dtHemoId.Rows.Add(row);
                        //    objPatient.SaveTempHemoId(dtHemoId);
                        //    this.txtHEMODIALYSIS_ID.Text = hemoId;
                        //}
                    }
                    else
                    {
                        this.cmbIS_NEW.Text = _current.IS_NEW == "0" ? "入科" : _current.IS_NEW == "1" ? "死亡" : _current.IS_NEW == "2" ? "转其它透析室" : _current.IS_NEW == "3" ? "转腹透" : _current.IS_NEW == "4" ? "肾移值" : _current.IS_NEW == "5" ? "放弃治疗" : "暂不需要治疗";
                        this.chkIsUp.Checked = (_current.IS_UP == "1" ? true : false);
                    }
                    this.txtNAME.Focus();
                    this.Enabled = true;

                };
                worker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// 判断数据输入是否合理 
        /// </summary>
        /// <returns></returns>
        private bool IsDataValidate()
        {
            this.errorProvider.ClearErrors();
            bool result = true;
            int iDate = 0;
            if (txtBIRTHDAY.Text.Length > 0)
            {
                iDate = Utility.CDate(txtBIRTHDAY.Text).CompareTo(System.DateTime.Now);
                if (iDate > 0)
                {
                    txtBIRTHDAY.Focus();
                    errorProvider.SetError(txtBIRTHDAY, "请输入正确的出生日期。");
                    return false;
                }
                else
                {
                    errorProvider.SetError(txtBIRTHDAY, string.Empty);
                }
            }
            if (Utility.CInt(txtAGE.Text) <= 0)
            {
                txtBIRTHDAY.Focus();
                errorProvider.SetError(txtBIRTHDAY, "请输入正确的出生日期。");
                return false;
            }
            //验证患者Patient_ID必填
            if (txtPATIENT_ID.Text.Length == 0 && txtADMISSION_NUMBER.Text.Length == 0)
            {
                cbxSPECIFIC_TIME.Focus();
                errorProvider.SetError(txtPATIENT_ID, "请输入患者门诊号或住院号。");
                return false;
            }
            else
            {
                errorProvider.SetError(txtPATIENT_ID, string.Empty);
            }

            if (txtBIRTHDAY.Text.Length > 0 && cbxSPECIFIC_TIME.Text.Length > 0)
            {
                iDate = Utility.CDate(txtBIRTHDAY.Text).CompareTo(Utility.CDate(cbxSPECIFIC_TIME.Text));
                if (iDate > 0)
                {
                    cbxSPECIFIC_TIME.Focus();
                    errorProvider.SetError(cbxSPECIFIC_TIME, "请输入正确的入院时间。");
                    return false;
                }
                else
                {
                    errorProvider.SetError(cbxSPECIFIC_TIME, string.Empty);
                }
            }

            //if (cbxLEAVE_HOSPITAL_TIME.Text.Length > 0 && cbxSPECIFIC_TIME.Text.Length > 0) {
            //    iDate = Utility.CDate(cbxSPECIFIC_TIME.Text).CompareTo(Utility.CDate(cbxLEAVE_HOSPITAL_TIME.Text));
            //    if (iDate > 0) {
            //        cbxLEAVE_HOSPITAL_TIME.Focus();
            //        errorProvider.SetError(cbxLEAVE_HOSPITAL_TIME, "请输入正确的出院时间，应晚于入院时间。");
            //        return false;
            //    }
            //    else {
            //        errorProvider.SetError(cbxLEAVE_HOSPITAL_TIME, string.Empty);
            //    }
            //}

            if (txtNAME.Text.Length == 0)
            {
                txtNAME.Focus();
                errorProvider.SetError(txtNAME, "请输入病人姓名。");
                return false;
            }
            else
            {
                errorProvider.SetError(txtNAME, string.Empty);
            }

            if (txtSEX.Text.Length == 0)
            {
                txtSEX.Focus();
                errorProvider.SetError(txtSEX, "请输入病人性别。");
                return false;
            }
            else
            {
                errorProvider.SetError(txtSEX, string.Empty);
            }

            //if (txtWORK_TELEPHONE.Text.Length > 0) {
            //    result = Utility.IsMobile(txtWORK_TELEPHONE.Text);
            //    result = Utility.IsTel(txtWORK_TELEPHONE.Text);
            //    if (!result) {
            //        errorProvider.SetError(txtWORK_TELEPHONE, "请输入正确的电话格式。（手机：13800000000 电话：010-2000000）");
            //        return result;
            //    }
            //    else {
            //        errorProvider.SetError(txtWORK_TELEPHONE, string.Empty);
            //    }
            //}

            //if (txtTELEPHONE.Text.Length > 0) {
            //    result = Utility.IsMobile(txtTELEPHONE.Text);
            //    result = Utility.IsTel(txtTELEPHONE.Text);
            //    if (!result) {
            //        errorProvider.SetError(txtTELEPHONE, "请输入正确的电话格式。（手机：13800000000 电话：010-2000000）");
            //        return result;
            //    }
            //    else {
            //        errorProvider.SetError(txtTELEPHONE, string.Empty);
            //    }
            //}

            if (txtAGE.Text.Length > 0)
            {
                if (!Utility.IsInt(txtAGE.Text))
                {
                    errorProvider.SetError(txtAGE, "请输入正确的年龄。");
                    return false;
                }
                else
                {
                    errorProvider.SetError(txtAGE, string.Empty);
                }
            }

            if (cbxCREDENTIALS_TYPE.EditValue.ToString() == "居民身份证")
            {
                result = Utility.IsIDCard(txtCREDENTIALS_NUMBER.Text);
                if (!result)
                {
                    errorProvider.SetError(txtCREDENTIALS_NUMBER, "请输入正确的身份证号码。");
                    return false;
                }
                else
                {
                    errorProvider.SetError(txtCREDENTIALS_NUMBER, string.Empty);
                }
            }

            if (string.IsNullOrEmpty(cbxCREDENTIALS_TYPE.EditValue.ToString()))
            {
                errorProvider.SetError(cbxCREDENTIALS_TYPE, "必须先把证件类型。");
                return false;
            }

            if (string.IsNullOrEmpty(this.txtCREDENTIALS_NUMBER.Text))
            {
                errorProvider.SetError(txtCREDENTIALS_NUMBER, "必须录入证件号码。");
                return false;
            }

            //result = Utility.IsChineseChars(txtNATIVEPLACE.Text);
            //if (!result) {
            //    errorProvider.SetError(txtNATIVEPLACE, "请使用汉字输入正确的籍贯。");
            //}
            //else {
            //    errorProvider.SetError(txtNATIVEPLACE, string.Empty);
            //}

            var havingPatient = _allPatients.Count(i => (i.NAME.Trim() == this.txtNAME.Text.Trim()) && i.HEMODIALYSIS_ID.Trim() != this.txtHEMODIALYSIS_ID.Text.Trim());
            if (havingPatient > 0)
            {
                if (XtraMessageBox.Show(string.Format("{0}患者已存在是否继续添加？", txtNAME.Text), this.Text, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                    return false;
            }

            var havingPatientTel = _allPatients.Count(i => (i.WORK_TELEPHONE.Trim() == this.txtWORK_TELEPHONE.Text.Trim()) && i.HEMODIALYSIS_ID.Trim() != this.txtHEMODIALYSIS_ID.Text.Trim());
            if (havingPatientTel > 0 && this.txtWORK_TELEPHONE.Text.Trim() != "")
            {
                if (XtraMessageBox.Show(string.Format("{0}患者联系号码已存在是否继续添加？", txtWORK_TELEPHONE.Text), this.Text, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                    return false;
            }

            var havingPatienta = _allPatients.Count(i => i.CREDENTIALS_NUMBER.Trim() == this.txtCREDENTIALS_NUMBER.Text.Trim() && i.HEMODIALYSIS_ID.Trim() != this.txtHEMODIALYSIS_ID.Text.Trim());

            if (havingPatienta > 0)
            {
                AutoClosedMsgBox.ShowForm(string.Format("身份证号为{0}\r\n的患者已存在请确认患者，避免重复添加。", txtCREDENTIALS_NUMBER.Text), this.Text, 1500, MessageBoxIcon.Error);
                return false;
            }
            if (!string.IsNullOrEmpty(currentCardNo))
            {
                DrugModel.MED_PATIENTS_CARDDataTable cardPatints = objPatient.GetPatientCardDt();

                var patient = cardPatints.FirstOrDefault(i => i.STATE.Trim() == "0" && (i.NAME.Trim() == this.txtNAME.Text.Trim()) && i.HEMODIALYSIS_ID.Trim() == this.txtHEMODIALYSIS_ID.Text.Trim());
                if (patient != null)
                {
                    AutoClosedMsgBox.ShowForm("此患者及制作卡数据已存在不可重复添加！", this.Text, 2000, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (string.IsNullOrEmpty(this.txtINFECTIOUS_CHECK_RESULT.Text))
            {
                errorProvider.SetError(txtINFECTIOUS_CHECK_RESULT, "请选择传染病及化验结果。");
                txtINFECTIOUS_CHECK_RESULT.Focus();
                return false;
            }

            if (cmbIS_NEW.Text != "入科" && cmbIS_NEW.Text.Length > 0 && cbxLEAVE_HOSPITAL_TIME.Text == "0001/1/1")
            {
                errorProvider.SetError(cbxLEAVE_HOSPITAL_TIME, "请录入去向日期。");
                cbxLEAVE_HOSPITAL_TIME.Focus();
                return false;
            }

            return result;
        }

        /// <summary>
        /// 病人数据保存方法  
        /// </summary>
        /// <returns></returns>
        private int SaveData()
        {
            this.mEDPATIENTSRowBindingSource.EndEdit();
            this.mEDPATIENTSRowBindingSource.CurrencyManager.EndCurrentEdit();

            var row = _patientDataTable[0];
            if (this.picMain.Image != null)
                row.PAT_PIC = ConvertImageToByte(this.picMain.Image);
            row.INPUT_CODE = PinYinConverter.GetPYString(row.NAME);
            row.AGE = Utility.CDecimal(txtAGE.Text);
            row.NATION = cbxNATION.Text;
            row.SPECIFIC_TIME = Utility.CDate(cbxSPECIFIC_TIME.Text);
            row.IS_NEW = this.cmbIS_NEW.SelectedIndex.ToString();
            row.LEAVE_HOSPITAL_TIME = Utility.CDate(cbxLEAVE_HOSPITAL_TIME.Text);
            if (isNew) { row.CREATE_DATE = System.DateTime.Now; }
            row.DIAGNOSE = txtDIAGNOSE.EditValue.ToString();
            row.PAT_LABEL = patLabel.Text.Trim();
            if (row.HEMODIALYSIS_ID.Length == 0)
            {
                row.HEMODIALYSIS_ID = objPatient.GetNewHemoID();
            }
            return objPatient.SavePatientAndCardInfo(_patientDataTable, this._patientCard);
        }

        private bool UpdatePatientTable(string patientID, bool isAll, string ptype, out PatientModel.MED_PATIENTSDataTable patientDataTable)
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
            //patientDataTable = objPatient.GetPatientList();
            patientDataTable = new PatientModel.MED_PATIENTSDataTable();
            PatientModel.MED_PATIENTSRow patientRowData = null;

            if (pMaster != null && pMaster.Rows.Count > 0)
            {
                foreach (HemodialysisModel.MED_PAT_MASTER_INDEXRow patIndexRow in pMaster.Rows)
                {
                    DataRow[] rows = _allPatients.Select("PATIENT_ID = '" + patIndexRow.PATIENT_ID + "'");

                    if (rows.Length > 0)
                    {
                        patientDataTable.ImportRow(rows[0] as PatientModel.MED_PATIENTSRow);
                        patientRowData = patientDataTable[0] as PatientModel.MED_PATIENTSRow;
                    }

                    if (patientRowData == null) //new
                    {
                        patientRowData = patientDataTable.NewMED_PATIENTSRow();
                        patientRowData.PATIENT_ID = patIndexRow.PATIENT_ID;
                        //patientRowData.HEMODIALYSIS_ID = objPatient.GetNewHemoID();
                        patientRowData.HEMODIALYSIS_ID = this.txtHEMODIALYSIS_ID.Text;
                        patientDataTable.AddMED_PATIENTSRow(patientRowData);
                        isNew = true;
                    }
                    else
                    {
                        isNew = false;
                    }

                    if (!patIndexRow.IsNAMENull())
                    {
                        patientRowData.NAME = patIndexRow.NAME;
                    }
                    if (!patIndexRow.IsNAME_PHONETICNull())
                    {
                        patientRowData.INPUT_CODE = PinYinConverter.GetPYString(patIndexRow.NAME);
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
                        patientRowData.SPECIFIC_TIME = DateTime.Now;
                    }
                    if (!patIndexRow.IsWARD_NAMENull())
                    {
                        patientRowData.WARD_CODE = patIndexRow.WARD_NAME;
                        patientRowData.TIME_TYPE = "住院";
                    }
                    else
                    {
                        patientRowData.TIME_TYPE = "门诊";
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
                    if (!patIndexRow.IsCREATE_DATENull())
                    {
                        patientRowData.CREATE_DATE = patIndexRow.CREATE_DATE;
                    }
                    patientRowData.IS_NEW = "0";
                    //objPatient.SavePatientInfo(patientDataTable);
                }

                result = true;
            }
            return result;
        }

        private void SynPatientAndGetPatientInfosByPatientID(string patientID, string ptype)
        {
            currentCardNo = patientID;

            this.Enabled = false;
            busyIndicator1.Show();
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate (object sender, DoWorkEventArgs e)
                {
                    string returnSynString = string.Empty;
                    returnSynString = InterfaceUtility.SynchronizePatientsByPatientId(patientID, HemoApplicationContext.Current.InterFaceDate.FirstOrDefault(i => i.ITEM_NAME == "血透同步数据接口").ITEM_VALUE.ToString());

                    if (returnSynString.Length == 0)
                    {
                        if (UpdatePatientTable(patientID, false, ptype, out _patientDataTable))
                        {
                            //同步数据成功。去取数据
                            //_patientDataTable = objPatient.GetPatientListByPatientID(patientID);
                        }
                    }
                    else
                    {
                        _patientDataTable = new PatientModel.MED_PATIENTSDataTable();

                        //同步数据失败记录日志....
                        MessageBox.Show(returnSynString, "同步数据失败！");
                    }
                };
                worker.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs e)
                {
                    this.mEDPATIENTSRowBindingSource.DataSource = _patientDataTable;
                    if (_patientDataTable != null && _patientDataTable.Rows.Count > 0)
                    {
                        txtCREDENTIALS_NUMBER.Text = _patientDataTable[0].CREDENTIALS_NUMBER;
                        if (_patientDataTable[0].CREDENTIALS_TYPE.Trim() == "居民身份证")
                            IdentityCardValidted();
                        if (_patientDataTable[0].TIME_TYPE == "住院")
                            this.cbxTIME_TYPE.Text = "住院";
                        else if (_patientDataTable[0].TIME_TYPE == "门诊")
                            this.cbxTIME_TYPE.Text = "门诊";
                    }
                    this.txtPATIENT_ID.Focus();
                    this.Enabled = true;
                    busyIndicator1.HideLoadingScreen();
                };
                worker.RunWorkerAsync();
            }
        }

        private byte[] ConvertImageToByte(Image image)
        {
            var imageCodecInfoList = ImageCodecInfo.GetImageEncoders();
            string mimeType = "image/jpeg";
            ImageCodecInfo myImageCodec = null;
            foreach (var imgCodec in imageCodecInfoList)
            {
                if (imgCodec.MimeType == mimeType)
                {
                    myImageCodec = imgCodec;
                    break;
                }
            }

            if (myImageCodec == null)
                return null;

            EncoderParameters encoderParams = new EncoderParameters(1);
            System.Drawing.Imaging.Encoder myCompressQuanlityEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameter myCompressQualityParam = new EncoderParameter(myCompressQuanlityEncoder, 80L);
            encoderParams.Param[0] = myCompressQualityParam;

            double dblPicWidth = 0;
            double dblPicHeight = 0;
            double dblPercent = 0;
            byte[] tempImage;

            //if (image.Width > _DefaultMaxPicWidth)
            //{
            //    //去掉按比例图片存储
            //    dblPercent = _DefaultMaxPicWidth / Convert.ToDouble(image.Width);
            //    dblPicWidth = _DefaultMaxPicWidth;
            //    dblPicHeight = Convert.ToInt32(image.Height * dblPercent);

            //    Bitmap tempPic = new Bitmap(Convert.ToInt32(dblPicWidth), Convert.ToInt32(dblPicHeight));
            //    var graphics = Graphics.FromImage(tempPic);
            //    Rectangle recPic = new Rectangle(0, 0, Convert.ToInt32(dblPicWidth), Convert.ToInt32(dblPicHeight));
            //    graphics.DrawImage(image, recPic);

            //    using (MemoryStream msPicture = new MemoryStream())
            //    {
            //        tempPic.Save(msPicture, myImageCodec, encoderParams);
            //        tempImage = msPicture.ToArray();
            //    }
            //    tempPic.Dispose();
            //}
            //else
            //{
            using (Bitmap bmpPic = new Bitmap(image))
            {
                using (MemoryStream msPicture = new MemoryStream())
                {
                    bmpPic.Save(msPicture, myImageCodec, encoderParams);
                    tempImage = msPicture.ToArray();
                }
            }
            //}

            return tempImage;
        }

        private void IdentityCardValidted()
        {
            try
            {
                this.errorProvider.ClearErrors();
                string identityCard = txtCREDENTIALS_NUMBER.Text.Trim();//获取得到输入的身份证号码
                if (string.IsNullOrEmpty(identityCard))
                {
                    errorProvider.SetError(txtCREDENTIALS_NUMBER, "身份证号码不能为空。");
                    if (txtCREDENTIALS_NUMBER.CanFocus)
                    {
                        txtCREDENTIALS_NUMBER.Focus();//设置当前输入焦点为textBox_IdentityCard
                    }
                    return;
                }
                else
                {
                    if (identityCard.Length != 15 && identityCard.Length != 18)//身份证号码只能为15位或18位其它不合法
                    {
                        errorProvider.SetError(txtCREDENTIALS_NUMBER, "身份证号码为15位或18位，请检查！。");

                        if (txtCREDENTIALS_NUMBER.CanFocus)
                        {
                            txtCREDENTIALS_NUMBER.Focus();
                        }
                        return;
                    }
                }
                string birthday = "";
                string sex = "";
                if (identityCard.Length == 18)//处理18位的身份证号码从号码中得到生日和性别代码
                {
                    birthday = identityCard.Substring(6, 4) + "-" + identityCard.Substring(10, 2) + "-" + identityCard.Substring(12, 2);
                    sex = identityCard.Substring(14, 3);
                }
                if (identityCard.Length == 15)
                {
                    birthday = "19" + identityCard.Substring(6, 2) + "-" + identityCard.Substring(8, 2) + "-" + identityCard.Substring(10, 2);
                    sex = identityCard.Substring(12, 3);
                }
                txtBIRTHDAY.DateTime = Utility.CDate(birthday);
                getAge();
                if (int.Parse(sex) % 2 == 0)//性别代码为偶数是女性奇数为男性
                {
                    this.txtSEX.Text = "女";
                }
                else
                {
                    this.txtSEX.Text = "男";
                }
            }
            catch (Exception ex)
            {
                errorProvider.SetError(txtCREDENTIALS_NUMBER, "身份证号码输入有误。");

                if (txtCREDENTIALS_NUMBER.CanFocus)
                {
                    txtCREDENTIALS_NUMBER.Focus();
                }
                return;
            }
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        private void getAge()
        {
            if (txtBIRTHDAY.Text.Length > 0 && txtBIRTHDAY.Text != "0001/1/1")
            {
                txtAGE.Text = Utility.GetAge(txtBIRTHDAY.Text).ToString();
            }
            else
            {
                txtAGE.Text = string.Empty;
            }
        }

        #endregion

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            ImportPatient impUi = new ImportPatient();

            FlyoutDialog.Show(Program.MainForm.FindForm(), impUi);
        }
    }
}
