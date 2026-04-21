using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.UI.Machine;
using Hemo.Model;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Client.Controls;
using Hemo.Utilities;
using Hemo.IService.PatientSchedule;
using Hemo.HQCWebClient.Models;
using Hemo.HQCWebClient;
using Newtonsoft.Json;
using Hemo.Client.Core;
using Hemo.IService.DataReport;
using DevExpress.XtraSplashScreen;

namespace Hemo.Client.UI.DataReportFZ
{
    public partial class UploadCureInfo : ViewBase
    {
        #region 类变量

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        private IPatientSchedule scheduleService = ServiceManager.Instance.PatientSchedule;

        private IConfig configService = ServiceManager.Instance.ConfigService;

        private IDataReport dataReportService = ServiceManager.Instance.DataReportService;

        private CtlMedicalDocumentContainer medicalDocContainer = new CtlMedicalDocumentContainer();

        private ConfigModel.MED_COMMON_ITEMLISTDataTable dtConfig = new ConfigModel.MED_COMMON_ITEMLISTDataTable();

        private string currentHemoId = string.Empty;

        private string loginName = string.Empty;

        private string getUserApi = string.Empty;

        private string getTokenApi = string.Empty;

        #endregion

        #region 属性

        public string CurrentHemoId
        {
            get { return currentHemoId; }
            set { currentHemoId = value; }
        }

        #endregion

        #region 构造函数

        public UploadCureInfo(string hemoId)
        {
            InitializeComponent();
            currentHemoId = hemoId;
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UploadCureInfo_Load(object sender, EventArgs e)
        {
            dtConfig = configService.GetConfigList(string.Empty, string.Empty, "质控平台访问配置", "1");
            if (dtConfig != null && dtConfig.Rows.Count > 0)
            {
                loginName = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("QCLoginName")).ITEM_VALUE;
                getUserApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("GetUserByName")).ITEM_VALUE;
                getTokenApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("GetToken")).ITEM_VALUE;
            }
        }

        /// <summary>
        /// 点击列表行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvCure_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var row = this.gvCure.GetFocusedDataRow() as HemodialysisModel.MED_CURE_MAINRow;
            if (row == null) { return; }
            if (e.Clicks == 1)
            {
                row.ISUPLOAD = (row.ISUPLOAD == "1") ? "0" : (row.ISUPLOAD == "0" ? "1" : row.ISUPLOAD);
            }
            else if (e.Clicks == 2)
            {
                this.xtraTabControl1.SelectedTabPageIndex = 1;
                LoadCureDocument(row);
            }
        }

        private void gvCure_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            var rowCurrent = this.gvCure.GetFocusedDataRow() as HemodialysisModel.MED_CURE_MAINRow;

            if (rowCurrent == null || e.CellValue == null) return;

            if (e.Column == gridColumn3)
            {
                if (e.CellValue.ToString().Equals("已上传"))
                {
                    e.Appearance.Font = new Font("Tahoma", 11, FontStyle.Bold);
                    e.Appearance.BackColor = Color.Green;
                }
            }
        }

        private void gvCure_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == gridColumn1)
            {
                var curRow = (HemodialysisModel.MED_CURE_MAINRow)gvCure.GetDataRow(e.RowHandle);
                if (curRow == null)
                    return;
                if (curRow.ISUPLOAD == "2")
                {
                    var cloneRepository = e.RepositoryItem.Clone() as DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit;
                    cloneRepository.Click += new EventHandler(cloneRepository_Click);
                    cloneRepository.ReadOnly = true;
                    e.RepositoryItem = cloneRepository;
                }
            }
        }

        private void cloneRepository_Click(object sender, EventArgs e)
        {
            XtraMessageBox.Show("已上传不能再上传");
        }

        #endregion

        #region 方法

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        public override void Query(DateTime dtBegin, DateTime dtEnd)
        {
            HemodialysisModel.MED_CURE_MAINDataTable dtCure = hemoService.GetMainCureByHemoIDAndDate(currentHemoId, dtBegin, dtEnd);
            this.gcCure.DataSource = dtCure;
        }

        private SplashScreenManager _loadForm;
        /// <summary>
        /// 等待窗体管理对象
        /// </summary>
        protected SplashScreenManager LoadForm
        {
            get
            {
                if (_loadForm == null)
                {
                    this._loadForm = new SplashScreenManager(new Form(), typeof(FrmWaitForm), true, true);
                    //this._loadForm.CloseWaitForm();.ClosingDelay = 0;
                }
                return _loadForm;
            }
        }
        /// <summary>
        /// 上传治疗信息
        /// </summary>
        /// <param name="baseInfo"></param>
        public override void GetVascualToUpLoad(string baseInfo)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(this.ParentForm.FindForm(), typeof(SplashScreen1));

            var dtCure = this.gcCure.DataSource as HemodialysisModel.MED_CURE_MAINDataTable;
            var dtReport = new DataReportModel.MED_PATIENT_DATAREPORTDataTable();
            if (dtCure != null && dtCure.Rows.Count > 0)
            {
                string msg = string.Empty;
                bool success = true;
                string saveCureApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("SaveCureInfo")).ITEM_VALUE;
                string saveParamApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("SaveHemoParameters")).ITEM_VALUE;

                MessageLog.Instance().Log(new LogEntity() { Type = loginName + "--" + saveCureApi, LogDate = DateTime.Now });

                //获取用户信息
                ResultMsg<MedUserInfo> resultMsg = WebApiClient.GetUserByName(loginName, getUserApi, getTokenApi);
                if (resultMsg == null)
                {
                    XtraMessageBox.Show("验证质控平台用户信息失败！");
                    return;
                }
                if (resultMsg.StatusCode == (int)StatusCodeEnum.Success)
                {
                    MessageLog.Instance().Log(new LogEntity() { Type = getUserApi, LogDate = DateTime.Now, Content = resultMsg.Data.ToString() });

                    var dtResult = JsonConvert.DeserializeObject<MedUserInfo>(resultMsg.Data.ToString());
                    if (dtResult != null)
                    {
                        int cureCount = 0;
                        //上传治疗信息
                        foreach (HemodialysisModel.MED_CURE_MAINRow row in dtCure.Rows)
                        {
                            if (row.ISUPLOAD == "1")
                            {
                                cureCount++;
                                DevExpress.XtraSplashScreen.SplashScreenManager.Default.SendCommand(SplashScreen1.SplashScreenCommand.SetText, string.Format("总共{0}条治疗单，正在上传第{1}条治疗单，请稍等...", dtCure.Where(cure => cure.ISUPLOAD == "1").Count().ToString(), cureCount.ToString()));
                                MED_CURE_MAIN cureInfo = new MED_CURE_MAIN();
                                cureInfo.ID = Guid.NewGuid().ToString();
                                cureInfo.HospitalId = dtResult.Company_ID;
                                cureInfo.HospitalYear = DateTime.Now.Date;
                                cureInfo.HospitalName = dtResult.CompanyName;
                                cureInfo.CURE_ID = row.CURE_ID;
                                cureInfo.RECIPE_ID = row.RECIPE_ID;
                                cureInfo.HEMODIALYSIS_ID = row.HEMODIALYSIS_ID;
                                cureInfo.RECIPE_TYPE = row["RECIPE_TYPE"].ToString();
                                cureInfo.CALCIUM_ION = Utility.CDecimal(row["CALCIUM_ION"].ToString());
                                cureInfo.CURE_STATUS = row["CURE_STATUS"].ToString();
                                cureInfo.DOCTOR_ID = row["DOCTOR_ID"].ToString();
                                cureInfo.RECIPE_DATE = Utility.CDate(row["RECIPE_DATE"].ToString());
                                cureInfo.BLOOW_FLOW = Utility.CDecimal(row["BLOOW_FLOW"].ToString());
                                cureInfo.DIALYSATE_FLOW = Utility.CDecimal(row["DIALYSATE_FLOW"].ToString());
                                cureInfo.DIALYSATE_TEMPERATURE = Utility.CDecimal(row["DIALYSATE_TEMPERATURE"].ToString());
                                cureInfo.UFR = Utility.CDecimal(row["UFR"].ToString());
                                cureInfo.SODION = Utility.CDecimal(row["SODION"].ToString());
                                cureInfo.POTASSIUM_ION = Utility.CDecimal(row["POTASSIUM_ION"].ToString());
                                cureInfo.PERFORM_SCHEDULE = Utility.CDate(row["PERFORM_SCHEDULE"].ToString());
                                cureInfo.PURIFICATION_MODE = row["PURIFICATION_MODE_NAME"].ToString();
                                cureInfo.CLEAN_UP_TIMES = Utility.CDecimal(row["CLEAN_UP_TIMES"].ToString());
                                cureInfo.NURSE_ID = row["NURSE_ID"].ToString();
                                cureInfo.FREQUENCY_HOURS = Utility.CDecimal(row["FREQUENCY_HOURS"].ToString());
                                cureInfo.BEGIN_TIME = Utility.CDate(row["BEGIN_TIME"].ToString());
                                cureInfo.END_TIME = Utility.CDate(row["END_TIME"].ToString());
                                cureInfo.LAST_TIME_DRY_WEIGHT = Utility.CDecimal(row["LAST_TIME_DRY_WEIGHT"].ToString());
                                cureInfo.BEFORE_DRY_WEIGHT = Utility.CDecimal(row["BEFORE_DRY_WEIGHT"].ToString());
                                cureInfo.AFTER_DRY_WEIGHT = Utility.CDecimal(row["AFTER_DRY_WEIGHT"].ToString());
                                cureInfo.BEFORE_SYSTOLIC_PRESSURE = Utility.CDecimal(row["BEFORE_SYSTOLIC_PRESSURE"].ToString());
                                cureInfo.BEFORE_DIASTOLIC_PRESSURE = Utility.CDecimal(row["BEFORE_DIASTOLIC_PRESSURE"].ToString());
                                cureInfo.AFTER_SYSTOLIC_PRESSURE = Utility.CDecimal(row["AFTER_SYSTOLIC_PRESSURE"].ToString());
                                cureInfo.AFTER_DIASTOLIC_PRESSURE = Utility.CDecimal(row["AFTER_DIASTOLIC_PRESSURE"].ToString());
                                cureInfo.DRY_WATER_VALUE = Utility.CDecimal(row["DRY_WATER_VALUE"].ToString());
                                cureInfo.BEFORE_TEMPERATURE = Utility.CDecimal(row["BEFORE_TEMPERATURE"].ToString());
                                cureInfo.AFTER_TEMPERATURE = Utility.CDecimal(row["AFTER_TEMPERATURE"].ToString());
                                cureInfo.BEFORE_HEART_RATE = Utility.CDecimal(row["BEFORE_HEART_RATE"].ToString());
                                cureInfo.AFTER_HEART_RATE = Utility.CDecimal(row["AFTER_HEART_RATE"].ToString());
                                cureInfo.PRIMARY_NURSE = row["PRIMARY_NURSE_NAME"].ToString();
                                cureInfo.PRIMARY_DOCTOR = row["PRIMARY_DOCTOR_NAME"].ToString();
                                cureInfo.PUNCTURE_NURSE = row["PUNCTURE_NURSE_NAME"].ToString();
                                cureInfo.MACHINE_ID = row["MACHINE_ID"].ToString();
                                cureInfo.VASCULAR_ACCESS_ID = row["VASCULAR_ACCESS_NAME"].ToString();
                                cureInfo.HEPARIN_SPECIES = row["HEPARIN_SPECIES_NAME"].ToString();
                                cureInfo.FIRST_HEPARIN = Utility.CDecimal(row["FIRST_HEPARIN"].ToString());
                                cureInfo.DOSIS_SUSTENTATIVA = Utility.CDecimal(row["DOSIS_SUSTENTATIVA"].ToString());
                                cureInfo.MACHINE_TYPE = row["MACHINE_TYPE_NAME"].ToString();
                                cureInfo.PURIFIER_NAME = row["PURIFIER_FULL_NAME"].ToString();
                                cureInfo.PURIFIER_M2 = Utility.CDecimal(row["PURIFIER_M2"].ToString());
                                cureInfo.USE_TYPE = row["USE_TYPE"].ToString();
                                cureInfo.REUSE_TIMES = Utility.CDecimal(row["REUSE_TIMES"].ToString());
                                cureInfo.A_LIQUID = row["A_LIQUID"].ToString();
                                cureInfo.B_LIQUID = row["B_LIQUID"].ToString();
                                cureInfo.BIRCARBONATE = Utility.CDecimal(row["BIRCARBONATE"].ToString());
                                cureInfo.AMYLACEUM = Utility.CDecimal(row["AMYLACEUM"].ToString());
                                cureInfo.SUMMARY = row["SUMMARY"].ToString();
                                cureInfo.CURE_CREATE_DATE = Utility.CDate(row["CURE_CREATE_DATE"].ToString());
                                cureInfo.VASCULAR_ACCESS_FIRM = row["VASCULAR_ACCESS_FIRM"].ToString();
                                cureInfo.VASCULAR_ACCESS_GLIDE = row["VASCULAR_ACCESS_GLIDE"].ToString();
                                cureInfo.VASCULAR_ACCESS_SWELLING = row["VASCULAR_ACCESS_SWELLING"].ToString();
                                cureInfo.VASCULAR_ACCESS_ERRHYISIS = row["VASCULAR_ACCESS_ERRHYISIS"].ToString();
                                cureInfo.VASCULAR_ACCESS_THROMBUS = row["VASCULAR_ACCESS_THROMBUS"].ToString();
                                cureInfo.VASCULAR_ACCESS_BLOOD = row["VASCULAR_ACCESS_BLOOD"].ToString();
                                cureInfo.FILTRATION_DISPLACEMENT_LIQUID = Utility.CDecimal(row["FILTRATION_DISPLACEMENT_LIQUID"].ToString());
                                cureInfo.FILTRATION_PERCOLATE = Utility.CDecimal(row["FILTRATION_PERCOLATE"].ToString());
                                cureInfo.DISPLACEMENT_LIQUID = Utility.CDecimal(row["DISPLACEMENT_LIQUID"].ToString());
                                cureInfo.PERCOLATE = Utility.CDecimal(row["PERCOLATE"].ToString());
                                cureInfo.DOCTOR_ADVICE = row["DOCTOR_ADVICE"].ToString();
                                cureInfo.SUMMARY2 = row["SUMMARY2"].ToString();
                                cureInfo.CHECK_NURSE = row["CHECK_NURSE_NAME"].ToString();
                                cureInfo.FIRST_DRUG_UNIT = row["FIRST_DRUG_UNIT_NAME"].ToString();
                                cureInfo.SECOND_DRUG_UNIT = row["SECOND_DRUG_UNIT_NAME"].ToString();
                                cureInfo.VEIN = row["VEIN"].ToString();
                                cureInfo.BEFORE_DRY_WEIGHT_TAG = row["BEFORE_DRY_WEIGHT_TAG"].ToString();
                                cureInfo.AFTER_DRY_WEIGHT_TAG = row["AFTER_DRY_WEIGHT_TAG"].ToString();
                                cureInfo.REUSE_TIMES_TAG = row["REUSE_TIMES_TAG"].ToString();
                                cureInfo.MACHINE_ID_TAG = row["MACHINE_ID_TAG"].ToString();
                                cureInfo.BLOOD_UP = row["BLOOD_UP"].ToString();
                                cureInfo.BLOOD_TYPE = row["BLOOD_TYPE"].ToString();
                                cureInfo.BLOOD_TRANSFUSION = row["BLOOD_TRANSFUSION"].ToString();
                                cureInfo.COAGULATION_IN_DIALYSER = row["COAGULATION_IN_DIALYSER"].ToString();
                                cureInfo.IN_BASKET_CLEAN = row["IN_BASKET_CLEAN"].ToString();
                                cureInfo.IN_BASKET_RED_HOT = row["IN_BASKET_RED_HOT"].ToString();
                                cureInfo.IN_BASKET_ECCHYMOSIS = row["IN_BASKET_ECCHYMOSIS"].ToString();
                                cureInfo.IN_BASKET_TREMOR = row["IN_BASKET_TREMOR"].ToString();
                                cureInfo.IN_BASKET_NOISE = row["IN_BASKET_NOISE"].ToString();
                                cureInfo.IN_BASKET_VASCULAR_ELASTICITY = row["IN_BASKET_VASCULAR_ELASTICITY"].ToString();
                                cureInfo.IN_BASKET_VASCULAR_OTHER = row["IN_BASKET_VASCULAR_OTHER"].ToString();
                                cureInfo.IN_BASKET_WOUND_ALLERGY = row["IN_BASKET_WOUND_ALLERGY"].ToString();
                                cureInfo.IN_BASKET_PLASTER_ALLERGY = row["IN_BASKET_PLASTER_ALLERGY"].ToString();
                                cureInfo.VASCULAR_ACCESS_TYPE = row["VASCULAR_ACCESS_TYPE"].ToString();
                                cureInfo.SUBJECTIVE_COMFORT = row["SUBJECTIVE_COMFORT"].ToString();
                                cureInfo.DRY_WEIGHT = Utility.CDecimal(row["DRY_WEIGHT"].ToString());
                                cureInfo.DRY_WEIGHT_TAG = row["DRY_WEIGHT_TAG"].ToString();
                                cureInfo.VASCULAR_ACCESS_STATE = row["VASCULAR_ACCESS_STATE"].ToString();
                                cureInfo.MACHINE_STATUS = row["MACHINE_STATUS"].ToString();
                                cureInfo.BEFORE_BP = Utility.CDecimal(row["BEFORE_BP"].ToString());
                                cureInfo.AFTER_BP = Utility.CDecimal(row["AFTER_BP"].ToString());
                                cureInfo.SUMMARY3 = row["SUMMARY3"].ToString();
                                cureInfo.WHAT_DEPARTMENT_IN = row["WHAT_DEPARTMENT_IN"].ToString();
                                cureInfo.INFECTIOUS_CHECK_RESULT = row["INFECTIOUS_CHECK_RESULT"].ToString();
                                cureInfo.FREQUENCY_MINUTE = Utility.CDecimal(row["FREQUENCY_MINUTE"].ToString());
                                cureInfo.DISPLACEMENT_MODE = row["DISPLACEMENT_MODE_NAME"].ToString();
                                cureInfo.DISPLACEMENT_RECIPE = row["DISPLACEMENT_RECIPE_NAME"].ToString();
                                cureInfo.DISPLACEMENT_SPECIAL_ADJUST = row["DISPLACEMENT_SPECIAL_ADJUST"].ToString();
                                cureInfo.ANTICOAGULANT_USE = row["ANTICOAGULANT_USE"].ToString();
                                cureInfo.SPECIAL_MATTER = row["SPECIAL_MATTER"].ToString();
                                cureInfo.UFR2 = Utility.CDecimal(row["UFR2"].ToString());
                                cureInfo.DISPLACEMENT_FLOW = Utility.CDecimal(row["DISPLACEMENT_FLOW"].ToString());
                                cureInfo.UF = Utility.CDecimal(row["UF"].ToString());
                                cureInfo.SUM_UF = Utility.CDecimal(row["SUM_UF"].ToString());
                                cureInfo.VASCULAR_ACCESS_BLOOD_INFECT = row["VASCULAR_ACCESS_BLOOD_INFECT"].ToString();
                                cureInfo.IsDelete = 0;
                                cureInfo.Editor = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                                cureInfo.Edittime = DateTime.Now;
                                cureInfo.Creator = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                                cureInfo.Creattime = DateTime.Now;

                                ResultMsg<string> result = WebApiClient.SaveCureInfo(cureInfo, saveCureApi, getTokenApi);

                                if (result != null && result.StatusCode == (int)StatusCodeEnum.Success)
                                {
                                    var rowExtend = dtReport.NewMED_PATIENT_DATAREPORTRow();
                                    rowExtend.ID = Guid.NewGuid().ToString();
                                    rowExtend.HEMODIALYSIS_ID = row.HEMODIALYSIS_ID;
                                    rowExtend.BASEINFO = result.Info;
                                    rowExtend.STATE = "1";//成功
                                    rowExtend.TYPE = "3";
                                    rowExtend.EXTEND = "HZZLXX";
                                    rowExtend.EXTEND1 = "患者治疗信息";
                                    rowExtend.EXTEND5 = "福建省上报平台";
                                    rowExtend.UPTIME = System.DateTime.Now;
                                    rowExtend.UPUSER = HemoApplicationContext.Current.CurrentUser.USER_ID;
                                    rowExtend.MAPIP = row.CURE_ID;
                                    dtReport.AddMED_PATIENT_DATAREPORTRow(rowExtend);

                                    //上传透析参数
                                    var dtParams = hemoService.GetHemoParametersByCureID(row.CURE_ID);
                                    if (dtParams != null && dtParams.Rows.Count > 0)
                                    {
                                        foreach (HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSRow param in dtParams.Rows)
                                        {
                                            MED_HEMODIALYSIS_PARAMETERS paramInfo = new MED_HEMODIALYSIS_PARAMETERS();
                                            paramInfo.ID = Guid.NewGuid().ToString();
                                            paramInfo.CURE_ID = cureInfo.ID;
                                            paramInfo.RECIPE_ID = param.RECIPE_ID;
                                            paramInfo.CREATE_DATE = Utility.CDate(param["CREATE_DATE"].ToString());
                                            paramInfo.VENOUS_PRESSURE = Utility.CDecimal(param["VENOUS_PRESSURE"].ToString());
                                            paramInfo.TRANSMEMBRANE_PRESSURE = Utility.CDecimal(param["TRANSMEMBRANE_PRESSURE"].ToString());
                                            paramInfo.TEMPERATURE = Utility.CDecimal(param["TEMPERATURE"].ToString());
                                            paramInfo.SYSTOLIC_PRESSURE = Utility.CDecimal(param["SYSTOLIC_PRESSURE"].ToString());
                                            paramInfo.DIASTOLIC_PRESSURE = Utility.CDecimal(param["DIASTOLIC_PRESSURE"].ToString());
                                            paramInfo.CARDIOTACH = Utility.CDecimal(param["CARDIOTACH"].ToString());
                                            paramInfo.BREATH = Utility.CDecimal(param["BREATH"].ToString());
                                            paramInfo.KT_V = param["KT_V"].ToString();
                                            paramInfo.CURE_MODE = param["CURE_MODE"].ToString();
                                            paramInfo.CLINICAL_MANIFESTATION = param["CLINICAL_MANIFESTATION"].ToString();
                                            paramInfo.BLOOD_FLOW = Utility.CDecimal(param["BLOOD_FLOW"].ToString());
                                            paramInfo.SODIUM_ION = Utility.CDecimal(param["SODIUM_ION"].ToString());
                                            paramInfo.DIALYSATE_RATE = Utility.CDecimal(param["DIALYSATE_RATE"].ToString());
                                            paramInfo.URF = Utility.CDecimal(param["URF"].ToString());
                                            paramInfo.CONDUCTIVITY = Utility.CDecimal(param["CONDUCTIVITY"].ToString());
                                            paramInfo.NURSE_ID = param["NURSE_ID"].ToString();
                                            paramInfo.DISPLACEMENT = Utility.CDecimal(param["DISPLACEMENT"].ToString());
                                            paramInfo.VASCULAR_ACCESS_ERRHYISIS = param["VASCULAR_ACCESS_ERRHYISIS"].ToString();
                                            paramInfo.VASCULAR_ACCESS_GLIDE = param["VASCULAR_ACCESS_GLIDE"].ToString();
                                            paramInfo.EXTENDED_FIELD_1 = param["EXTENDED_FIELD_1"].ToString();
                                            paramInfo.EXTENDED_FIELD_2 = param["EXTENDED_FIELD_2"].ToString();
                                            paramInfo.EXTENDED_FIELD_3 = param["EXTENDED_FIELD_3"].ToString();
                                            paramInfo.EXTENDED_FIELD_4 = param["EXTENDED_FIELD_4"].ToString();
                                            paramInfo.EXTENDED_FIELD_5 = param["EXTENDED_FIELD_5"].ToString();
                                            paramInfo.ANTICOAGULANT = Utility.CDecimal(param["ANTICOAGULANT"].ToString());
                                            paramInfo.VENOUS_PRESSURE_UNIT = param["VENOUS_PRESSURE_UNIT"].ToString();
                                            paramInfo.ANTICOAGULANTUNIT = param["ANTICOAGULANTUNIT"].ToString();
                                            paramInfo.ARTERY_PRESSURE = Utility.CDecimal(param["ARTERY_PRESSURE"].ToString());
                                            paramInfo.CRRT_CLASS = param["CRRT_CLASS"].ToString();

                                            result = WebApiClient.SaveHemoParameters(paramInfo, saveParamApi, getTokenApi);

                                            if (result != null && result.StatusCode == (int)StatusCodeEnum.Success)
                                            {
                                                rowExtend = dtReport.NewMED_PATIENT_DATAREPORTRow();
                                                rowExtend.ID = Guid.NewGuid().ToString();
                                                rowExtend.HEMODIALYSIS_ID = row.HEMODIALYSIS_ID;
                                                rowExtend.BASEINFO = result.Info;
                                                rowExtend.STATE = "1";//成功
                                                rowExtend.TYPE = "4";
                                                rowExtend.EXTEND = "HZTXCS";
                                                rowExtend.EXTEND1 = "患者透析参数";
                                                rowExtend.EXTEND5 = "福建省上报平台";
                                                rowExtend.UPTIME = System.DateTime.Now;
                                                rowExtend.UPUSER = HemoApplicationContext.Current.CurrentUser.USER_ID;
                                                rowExtend.MAPIP = param.HEMODIALYSIS_PARAMETERS_ID;
                                                dtReport.AddMED_PATIENT_DATAREPORTRow(rowExtend);
                                            }
                                            else
                                            {
                                                rowExtend = dtReport.NewMED_PATIENT_DATAREPORTRow();
                                                rowExtend.ID = Guid.NewGuid().ToString();
                                                rowExtend.HEMODIALYSIS_ID = row.HEMODIALYSIS_ID;
                                                rowExtend.BASEINFO = result.Info;
                                                rowExtend.STATE = "0";//失败
                                                rowExtend.TYPE = "4";
                                                rowExtend.EXTEND = "HZTXCS";
                                                rowExtend.EXTEND1 = "患者透析参数";
                                                rowExtend.EXTEND5 = "福建省上报平台";
                                                rowExtend.UPTIME = System.DateTime.Now;
                                                rowExtend.UPUSER = HemoApplicationContext.Current.CurrentUser.USER_ID;
                                                rowExtend.MAPIP = param.HEMODIALYSIS_PARAMETERS_ID;
                                                dtReport.AddMED_PATIENT_DATAREPORTRow(rowExtend);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    var rowExtend = dtReport.NewMED_PATIENT_DATAREPORTRow();
                                    rowExtend.ID = Guid.NewGuid().ToString();
                                    rowExtend.HEMODIALYSIS_ID = row.HEMODIALYSIS_ID;
                                    rowExtend.BASEINFO = result.Info;
                                    rowExtend.STATE = "0";//失败
                                    rowExtend.TYPE = "3";
                                    rowExtend.EXTEND = "HZZLXX";
                                    rowExtend.EXTEND1 = "患者治疗信息";
                                    rowExtend.EXTEND5 = "福建省上报平台";
                                    rowExtend.UPTIME = System.DateTime.Now;
                                    rowExtend.UPUSER = HemoApplicationContext.Current.CurrentUser.USER_ID;
                                    rowExtend.MAPIP = row.CURE_ID;
                                    dtReport.AddMED_PATIENT_DATAREPORTRow(rowExtend);
                                }
                            }
                        }

                        DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();

                        if (cureCount == 0)
                        {
                            XtraMessageBox.Show("没有患者透析记录要上传！");
                            return;
                        }

                        var count = dataReportService.SavePatientIsUploadDt(dtReport);
                        if (count <= 0)
                        {
                            success = false;
                        }
                    }
                    else
                    {
                        success = false;
                        msg = "验证质控平台用户信息失败！";
                    }
                }
                else
                {
                    success = false;
                    msg = resultMsg.Info;
                }

                if (success)
                {
                    XtraMessageBox.Show("上传成功！");
                }
                else
                {
                    XtraMessageBox.Show("上传失败！\r\n" + msg);
                }
            }
        }

        /// <summary>
        /// 加载透析记录单
        /// </summary>
        /// <param name="row"></param>
        private void LoadCureDocument(HemodialysisModel.MED_CURE_MAINRow row)
        {
            medicalDocContainer.HaveNextPage = false;
            int countNum = 0;
            int countParam;
            string[] records = null;
            bool isShow = true;
            HemodialysisModel.MED_CURE_MAIN_CRRTDataTable dtCRRTCure = null;
            DataSet data = hemoService.GetAllCure(row.CURE_ID);

            if (data.Tables["MED_HEMODIALYSIS_PARAMETERS"] != null)
            {
                if (row["AREA_NAME"].ToString().Equals("CRRT"))
                {
                    var dtCRRTTemp = hemoService.GetCRRTCureByCureId(row.CURE_ID);
                    if (dtCRRTTemp != null && dtCRRTTemp.Rows.Count > 0)
                    {
                        dtCRRTCure = dtCRRTTemp.Clone() as HemodialysisModel.MED_CURE_MAIN_CRRTDataTable;
                        dtCRRTCure.ImportRow(dtCRRTTemp.OrderByDescending(r => r.CREATE_DATE).First());
                        var rows = data.Tables["MED_HEMODIALYSIS_PARAMETERS"].AsEnumerable().Where(r => Utility.CDate(r["CREATE_DATE"].ToString()).Date.CompareTo(dtCRRTCure[0].CREATE_DATE) == 0 && r["CRRT_CLASS"].ToString().Equals(dtCRRTCure[0].CRRT_CLASS));
                        countNum = rows != null ? rows.Count() : 0;
                    }
                }
                else
                {
                    countNum = data.Tables["MED_HEMODIALYSIS_PARAMETERS"].Rows.Count;
                }
            }

            if (data.Tables["MED_CURE_MAIN"] != null)
            {
                DataTable dtCureMain = data.Tables["MED_CURE_MAIN"];
                if (row["AREA_NAME"].ToString().Equals("CRRT"))
                {
                    if (dtCRRTCure != null && dtCRRTCure.Rows.Count > 0)
                    {
                        records = dtCRRTCure[0].SUMMARY2.Split("|".ToCharArray());
                    }
                }
                else
                {
                    records = dtCureMain.Rows[0]["SUMMARY2"].ToString().Split("|".ToCharArray());
                }
            }

            WPF_DocumentBase document = null;
            if (row["AREA_NAME"].ToString().Equals("CRRT"))
            {
                var schedule = scheduleService.GetPatientScheduleByRecipeId(dtCRRTCure[0].RECIPE_ID);
                var r = (schedule != null && schedule.Rows.Count > 0) ? schedule[0] : null;
                document = new CtlMedicalDocumentCRRT(r, data, countNum, 10, dtCRRTCure[0].CRRT_CLASS, dtCRRTCure[0].CREATE_DATE);
            }
            else
            {
                document = new CtlMedicalDocument(data, countNum, 10);
            }
            document.IsShowGrid(isShow);
            medicalDocContainer.Add(document);

            //进行计算分页
            //计算可以有多少页的参数页
            int pageCount = (countNum - 10) / 20;
            int pageCountExt = (countNum - 10) % 20;
            if (pageCountExt > 0)
            {
                pageCount++;
            }
            countParam = countNum - 10;

            for (int i = 2; i < pageCount + 2; i++)
            {
                medicalDocContainer.Remove(i.ToString());
                CtlMedicalDocument3 document1 = row["AREA_NAME"].ToString().Equals("CRRT") ? new CtlMedicalDocument3(data, (dtCRRTCure != null && dtCRRTCure.Rows.Count > 0) ? dtCRRTCure[0] : null, countParam, 20, 1, i, row["AREA_NAME"].ToString()) : new CtlMedicalDocument3(data, countParam, 20, "sqlByParams", i, row["AREA_NAME"].ToString());
                medicalDocContainer.Add(i.ToString(), document1);
                countParam = countParam - 20;
            }

            DataTable cureMainDataTable = data.Tables["MED_CURE_MAIN"];

            if (cureMainDataTable != null)
            {
                string strSummary = cureMainDataTable.Rows[0]["SUMMARY"].ToString();

                string strSummary1 = cureMainDataTable.Rows[0]["SUMMARY2"].ToString().Replace("|||", "");

                //病情记录
                if (strSummary.Length > 164 || strSummary1.Length > 0)
                {
                    var lastDoc = pageCount + 2;
                    CtlMedicalDocument2 document2 = new CtlMedicalDocument2(data);
                    medicalDocContainer.Add(lastDoc.ToString(), document2);
                }
            }

            this.elementHost.Child = medicalDocContainer;
        }

        public override void CheckAllState(bool isCheck)
        {
            try
            {
                var dtSource = ((System.Data.DataView)(this.gvCure.DataSource)).Table as HemodialysisModel.MED_CURE_MAINDataTable;
                foreach (HemodialysisModel.MED_CURE_MAINRow row in dtSource.Rows)
                {
                    if (row.UPSTATE == "已上传")
                        continue;

                    row.ISUPLOAD = isCheck ? "1" : "0";
                }
            }
            catch (Exception ex) { }
        }

        #endregion
    }
}
