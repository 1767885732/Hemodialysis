/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司股份有限公司
// 文件名：CtlMedicalDocument4New.cs
// 文件功能描述：血 液 净 化 治 疗 记 录 单（分页）
// 创建标识：贺建操 2018-05-08
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.IService.Config;
using Hemo.Service;
using System.IO;
using System.Collections;
using Hemo.IService.Dict;

namespace Hemo.Client.Controls
{
    /// <summary>
    /// CtlMedicalDocument4New.xaml 的交互逻辑
    /// </summary>
    public partial class CtlMedicalDocument4New : WPF_DocumentBase
    {
        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;

        private VascuarAccessService objVascuarAccess = new VascuarAccessService();

        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private DataSet _CureMainData = new DataSet();
        private PatientScheduleModel.MED_PATIENT_SCHEDULERow currentPatientSchedule = null;

        private int rowNum1;
        private int rowNum2;
        private int pageNum;
        private string sqlParam;
        private string areaName;
        private HemodialysisModel.MED_CURE_MAIN_CRRTRow rowCRRT = null;

        public PatientScheduleModel.MED_PATIENT_SCHEDULERow CurrentPatientSchedule
        {
            get { return currentPatientSchedule; }
            set { currentPatientSchedule = value; }
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public CtlMedicalDocument4New()
        {
            InitializeComponent();
            this.HospitalTitle.Content = Utility.GetHospitalName();
            this.currentPatientSchedule = null;
            _CureMainData = new DataSet();
            loadData(null);
            IsShowGrid(false);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public CtlMedicalDocument4New(PatientScheduleModel.MED_PATIENT_SCHEDULERow currentPatientSchedule, DataSet pDs)
        {
            InitializeComponent();
            this.HospitalTitle.Content = Utility.GetHospitalName();

            this.currentPatientSchedule = currentPatientSchedule;

            _CureMainData = pDs;
            loadData(pDs);
            IsShowGrid(false);
        }

        /// <summary>
        /// CRRT分页构造函数
        /// </summary>
        public CtlMedicalDocument4New(DataSet _CureMainData, int _rowNum1, int _rowNum2, string _sqlParam, int _pageNum, string areaName)
        {
            InitializeComponent();
            this.HospitalTitle.Content = Utility.GetHospitalName();
            rowNum1 = _rowNum1;
            sqlParam = _sqlParam;
            pageNum = _pageNum;
            rowNum2 = _rowNum2;
            _CureMainData = _CureMainData;
            this.areaName = areaName;
            loadData(_CureMainData);
        }

        /// <summary>
        /// CRRT分页构造函数（带CRRT数据行）
        /// </summary>
        public CtlMedicalDocument4New(DataSet _CureMainData, HemodialysisModel.MED_CURE_MAIN_CRRTRow rowCRRT, int _rowNum1, int _rowNum2, int pageType, int _pageNum, string areaName)
        {
            InitializeComponent();
            this.HospitalTitle.Content = Utility.GetHospitalName();
            rowNum1 = _rowNum1;
            pageNum = _pageNum;
            rowNum2 = _rowNum2;
            _CureMainData = _CureMainData;
            this.areaName = areaName;
            this.rowCRRT = rowCRRT;
            loadData(_CureMainData);
        }

        /// <summary>
        /// 根据治疗单数据集赋值
        /// </summary>
        private void loadData(DataSet pDs)
        {
            string strCureID = string.Empty;
            string strRecipe_ID = string.Empty;
            int addCount = 0;
            string birthday = string.Empty;
            DateTime dCureDate = new DateTime();
            DateTime newFormStartTime = new DateTime(2026, 7, 6).Date;

            if (pDs != null && pDs.Tables.Count > 0)
            {
                string checkResult = string.Empty;
                string department = string.Empty;
                string strPatientType = string.Empty;
                string hemoId = string.Empty;

                #region 加载患者信息
                if (pDs.Tables["MED_PATIENTS"] != null)
                {
                    DataTable patientDataTable = pDs.Tables["MED_PATIENTS"];
                    if (patientDataTable != null && patientDataTable.Rows.Count > 0)
                    {
                        txtNAME.Text = patientDataTable.Rows[0]["NAME"].ToString();
                        txtSEX.Text = patientDataTable.Rows[0]["SEX"].ToString();
                        txtAGE.Text = patientDataTable.Rows[0]["AGE"].ToString();
                        txtPATIENT_ID.Text = patientDataTable.Rows[0]["PATIENT_ID"].ToString().Split('|')[0].ToString();
                        txtHEMODIALYSIS_ID.Text = patientDataTable.Rows[0]["HEMODIALYSIS_ID"].ToString();
                        hemoId = patientDataTable.Rows[0]["HEMODIALYSIS_ID"].ToString();
                        txtADMISSION_NUMBER.Text = patientDataTable.Rows[0]["ADMISSION_NUMBER"].ToString();
                        birthday = patientDataTable.Rows[0]["BIRTHDAY"].ToString();
                        strPatientType = patientDataTable.Rows[0]["TIME_TYPE"].ToString();
                        checkResult = patientDataTable.Rows[0]["INFECTIOUS_CHECK_RESULT"].ToString();
                        department = patientDataTable.Rows[0]["WHAT_DEPARTMENT_IN"].ToString();

                        DataTable dtCureCount = objHemodialysisService.GetCureCountByHemoID(patientDataTable.Rows[0]["HEMODIALYSIS_ID"].ToString());
                        if (dtCureCount != null && dtCureCount.Rows.Count > 0)
                        {
                            txtCLEAN_UP_TIMES.Text = dtCureCount.Rows[0][0].ToString();
                        }
                    }
                }
                #endregion

                #region 加载处方信息（判断新/旧表单）
                if (pDs.Tables["MED_HEMO_RECIPE"] != null)
                {
                    DataTable retipeDataTable = pDs.Tables["MED_HEMO_RECIPE"];
                    if (retipeDataTable != null && retipeDataTable.Rows.Count > 0)
                    {
                        DateTime recipeDate = Convert.ToDateTime(retipeDataTable.Rows[0]["RECIPE_DATE"]);
                        bool isNewForm = recipeDate.Date >= newFormStartTime;
                        updateFormStyle(isNewForm);

                        txtVASCULAR_ACCESS_ID.Text = retipeDataTable.Rows[0]["VASCULAR_ACCESS_NAME"].ToString();
                        txtFIRST_HEPARIN.Text = setZeroToEmpty(retipeDataTable.Rows[0]["FIRST_DRUG_DOSAGE"].ToString());
                        txtDOSIS_SUSTENTATIVA.Text = setZeroToEmpty(retipeDataTable.Rows[0]["SECOND_DRUG_DOSAGE"].ToString());
                        txtMACHINE_TYPE.Text = retipeDataTable.Rows[0]["MACHINE_TYPE_NAME"].ToString();
                        txtPURIFIER_NAME.Text = retipeDataTable.Rows[0]["PURIFIER_NAME"].ToString();
                        txtPURIFIER_M2.Text = setZeroToEmpty(retipeDataTable.Rows[0]["FIRST_PURIFIER_M2"].ToString());
                        txtMACHINE_ID.Text = retipeDataTable.Rows[0]["machine_name"].ToString();
                        txtUFR.Text = setZeroToEmpty(retipeDataTable.Rows[0]["UFR"].ToString());
                        txtDRY_WEIGHT.Text = setZeroToEmpty(retipeDataTable.Rows[0]["DRY_WEIGHT"].ToString());
                        txtBEFORE_DRY_WEIGHT.Text = setZeroToEmpty(retipeDataTable.Rows[0]["TODAY_WEIGHT"].ToString());
                        txtFREQUENCY_HOURS.Text = retipeDataTable.Rows[0]["FREQUENCY_HOURS"].ToString();

                        getHourAndMinute(setZeroToEmpty(retipeDataTable.Rows[0]["FREQUENCY_HOURS"].ToString()));
                    }
                }
                #endregion

                #region 加载治疗单信息
                if (pDs.Tables["MED_CURE_MAIN"] != null)
                {
                    DataTable cureMainDataTable = pDs.Tables["MED_CURE_MAIN"];
                    if (cureMainDataTable != null && cureMainDataTable.Rows.Count > 0)
                    {
                        DateTime cureMainDate = Convert.ToDateTime(cureMainDataTable.Rows[0]["CURE_CREATE_DATE"]);
                        bool isNewForm = cureMainDate.Date >= newFormStartTime;
                        updateFormStyle(isNewForm);

                        this.txtPurMode.Text = cureMainDataTable.Rows[0]["PURIFICATION_MODE_NAME"].ToString();

                        dCureDate = Utility.CDate(cureMainDataTable.Rows[0]["CURE_CREATE_DATE"].ToString());
                        txtAGE.Text = Utility.GetAgeByCureDate(birthday.ToString(), dCureDate.ToString()).ToString();

                        // 分页日期处理
                        string year = Utility.CDate(cureMainDataTable.Rows[0]["CURE_CREATE_DATE"].ToString()).Year.ToString();
                        string month = Utility.CDate(cureMainDataTable.Rows[0]["CURE_CREATE_DATE"].ToString()).Month.ToString();
                        string day = Utility.CDate(cureMainDataTable.Rows[0]["CURE_CREATE_DATE"].ToString()).Day.ToString();

                        if (areaName != null && areaName.Equals("CRRT"))
                        {
                            txtCureYear.Text = rowCRRT != null ? Utility.CDate(rowCRRT["CREATE_DATE"].ToString()).Year.ToString() : year;
                            txtCureMouth.Text = rowCRRT != null ? Utility.CDate(rowCRRT["CREATE_DATE"].ToString()).Month.ToString() : month;
                            txtCureDay.Text = rowCRRT != null ? Utility.CDate(rowCRRT["CREATE_DATE"].ToString()).Day.ToString() : day;
                        }
                        else
                        {
                            txtCureYear.Text = year;
                            txtCureMouth.Text = month;
                            txtCureDay.Text = day;
                        }

                        txtBeforeWeight.Text = objHemodialysisService.GetLastTimeCureDataByID(hemoId, dCureDate);

                        if (currentPatientSchedule != null)
                        {
                            txtWHAT_DEPARTMENT_IN.Text = currentPatientSchedule.AREANAME;
                            textBox1.Text = currentPatientSchedule.BEDNAME;

                            ConfigModel.MED_COMMON_ITEMLISTDataTable mED_COMMON_ITEMLISTRows = _configService.GetItemListByItemType("班次");
                            txtBanCi.Text = (from x in mED_COMMON_ITEMLISTRows.AsEnumerable()
                                             where x.ITEM_VALUE == currentPatientSchedule.BANCI_ID
                                             select x.ITEM_NAME).First();
                        }

                        lblFirst_Drug_Unit.Content = cureMainDataTable.Rows[0]["first_drug_unit_name"].ToString() == string.Empty ? "mg        追加：" : cureMainDataTable.Rows[0]["first_drug_unit_name"].ToString() + "        追加：";

                        string secondUnit = cureMainDataTable.Rows[0]["second_drug_unit_name"].ToString();
                        lblSecond_Drug_Unit.Content = secondUnit.Equals(string.Empty) ? "mg/h" : ((secondUnit.Equals("mg") || secondUnit.Equals("μg") || secondUnit.Equals("ml") || secondUnit.Equals("u")) ? secondUnit + "/h       " : secondUnit + "/小时       ");

                        txtVASCULAR_ACCESS_ID.Text = cureMainDataTable.Rows[0]["VASCULAR_ACCESS_NAME"].ToString();
                        if (cureMainDataTable.Rows[0]["VEIN"].ToString().Length > 0)
                        {
                            txtVASCULAR_ACCESS_ID.Text += "+" + cureMainDataTable.Rows[0]["VEIN"].ToString();
                        }
                        txtMACHINE_TYPE.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["MACHINE_TYPE_NAME"].ToString());
                        txtPURIFIER_NAME.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["purifier_new_name"].ToString());
                        txtPURIFIER_M2.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["PURIFIER_M2"].ToString());
                        txtMACHINE_ID.Text = cureMainDataTable.Rows[0]["MACHINE_ID"] != DBNull.Value ? cureMainDataTable.Rows[0]["MACHINE_ID"].ToString() : cureMainDataTable.Rows[0]["machine_name"].ToString();
                        txtMACHINE_ID_TAG.Text = cureMainDataTable.Rows[0]["MACHINE_ID_TAG"].ToString();
                        txtCLEAN_UP_TIMES.Text = cureMainDataTable.Rows[0]["CLEAN_UP_TIMES"].ToString();
                        txtFREQUENCY_HOURS.Text = cureMainDataTable.Rows[0]["FREQUENCY_HOURS"].ToString();
                        txtMinute.Text = cureMainDataTable.Rows[0]["FREQUENCY_MINUTE"].ToString();
                        txtUFR.Text = cureMainDataTable.Rows[0]["UFR"].ToString();
                        txtFILTRATION_DISPLACEMENT_LIQUID.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["FILTRATION_DISPLACEMENT_LIQUID"].ToString());
                        txtFILTRATION_PERCOLATE.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["FILTRATION_PERCOLATE"].ToString());
                        txtDISPLACEMENT_LIQUID.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["DISPLACEMENT_LIQUID"].ToString());
                        txtPERCOLATE.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["PERCOLATE"].ToString());

                        txtPRIMARY_DOCTOR.Text = cureMainDataTable.Rows[0]["DOCTOR_NAME"].ToString();
                        txtPRIMARY_NURSE.Text = cureMainDataTable.Rows[0]["NURSE_NAME"].ToString();
                        txtCHECK_NURSE.Text = cureMainDataTable.Rows[0]["check_nurse_name"].ToString();
                        lupPUNCTURE_NURSE.Text = cureMainDataTable.Rows[0]["PUNCTURE_NURSE_NAME"].ToString();

                        if (cureMainDataTable.Rows[0]["HEPARIN_SPECIES_NAME"].ToString() != "无肝素透析")
                        {
                            txtFIRST_HEPARIN.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["FIRST_HEPARIN"].ToString());
                            txtDOSIS_SUSTENTATIVA.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["DOSIS_SUSTENTATIVA"].ToString());
                        }

                        if (cureMainDataTable.Rows[0]["VASCULAR_ACCESS_TYPE"].ToString().ToUpper().Equals("TRUE"))
                        {
                            txtBEFORE_DRY_WEIGHT.Text = "卧床";
                            lbWeightTag.Content = "衣物轮椅重";
                        }
                        else
                        {
                            txtBEFORE_DRY_WEIGHT.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["BEFORE_DRY_WEIGHT"].ToString());
                            lbWeightTag.Content = "kg 衣物轮椅重";
                        }

                        if (cureMainDataTable.Rows[0]["IN_BED"].ToString().Equals("1"))
                        {
                            txtAFTER_DRY_WEIGHT.Text = "卧床";
                        }
                        else
                        {
                            txtAFTER_DRY_WEIGHT.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["AFTER_DRY_WEIGHT"].ToString());
                        }

                        if (cureMainDataTable.Rows[0]["DRY_WEIGHT"].ToString() != "0")
                        {
                            txtDRY_WEIGHT.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["DRY_WEIGHT"].ToString());
                        }
                        txtDRY_WEIGHT_TAG.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["DRY_WEIGHT_TAG"].ToString());

                        // 分页小结
                        string[] records = areaName != null && areaName.Equals("CRRT") ?
                            (rowCRRT != null ? rowCRRT.SUMMARY2.Split("|".ToCharArray()) : null) :
                            cureMainDataTable.Rows[0]["SUMMARY2"].ToString().Split("|".ToCharArray());

                        string strSummary2 = string.Empty;
                        if (pageNum == 2)
                        {
                            string strSummary = areaName != null && areaName.Equals("CRRT") ?
                                (rowCRRT != null ? rowCRRT.SUMMARY3 : string.Empty) :
                                cureMainDataTable.Rows[0]["SUMMARY3"].ToString();
                            strSummary2 = records.Length >= 1 ? records[0] : strSummary2;
                            strSummary2 = strSummary2 + " " + strSummary;
                        }
                        else if (pageNum == 3)
                        {
                            strSummary2 = records.Length >= 2 ? records[1] : strSummary2;
                        }
                        else if (pageNum == 4)
                        {
                            strSummary2 = records.Length >= 3 ? records[2] : strSummary2;
                        }
                        else if (pageNum == 5)
                        {
                            strSummary2 = records.Length >= 4 ? records[3] : strSummary2;
                        }
                        txtSUMMARY.Text = strSummary2;

                        txtBEFORE_BP.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["BEFORE_BP"].ToString());
                        txtAFTER_BP.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["AFTER_BP"].ToString());
                        txtBEFORE_SYSTOLIC_PRESSURE.Text = cureMainDataTable.Rows[0]["BEFORE_SYSTOLIC_PRESSURE"].ToString();
                        txtBEFORE_DIASTOLIC_PRESSURE.Text = cureMainDataTable.Rows[0]["BEFORE_DIASTOLIC_PRESSURE"].ToString();
                        txtAFTER_SYSTOLIC_PRESSURE.Text = cureMainDataTable.Rows[0]["AFTER_SYSTOLIC_PRESSURE"].ToString();
                        txtAFTER_DIASTOLIC_PRESSURE.Text = cureMainDataTable.Rows[0]["AFTER_DIASTOLIC_PRESSURE"].ToString();
                    }
                }
                #endregion

                #region 加载透析参数列表
                if (pDs.Tables["MED_HEMODIALYSIS_PARAMETERS"] != null && pDs.Tables["MED_HEMODIALYSIS_PARAMETERS"].Rows.Count > 0)
                {
                    var dtHemoParameters = pDs.Tables["MED_HEMODIALYSIS_PARAMETERS"] as HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable;
                    strCureID = dtHemoParameters.Rows[0]["CURE_ID"].ToString();
                    strRecipe_ID = dtHemoParameters.Rows[0]["RECIPE_ID"].ToString();

                    // 分页数据筛选：从 rowNum1 开始取 rowNum2 行
                    HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable dtPageData = dtHemoParameters.Clone() as HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable;

                    int startIndex = rowNum1;
                    int endIndex = Math.Min(startIndex + rowNum2, dtHemoParameters.Rows.Count);

                    for (int i = startIndex; i < endIndex; i++)
                    {
                        dtPageData.ImportRow(dtHemoParameters.Rows[i]);
                    }

                    // 如果数据不足 rowNum2 行，补空白行
                    if (dtPageData.Rows.Count < rowNum2)
                    {
                        for (int i = dtPageData.Rows.Count; i < rowNum2; i++)
                        {
                            DataRow dr = dtPageData.NewRow();
                            dr["HEMODIALYSIS_PARAMETERS_ID"] = System.Guid.NewGuid().ToString();
                            dr["CURE_ID"] = (i + 1).ToString();
                            dr["RECIPE_ID"] = (i + 1).ToString();
                            dtPageData.Rows.Add(dr);
                        }
                    }

                    loadParamsGrid(dtPageData, strCureID, strRecipe_ID);
                }
                else
                {
                    var dtHemoParameters = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();
                    loadParamsGrid(dtHemoParameters, strCureID, strRecipe_ID);
                }
                #endregion

                // 计算实际脱水量
                double beforeWeight, afterWight = 0;
                double.TryParse(txtBEFORE_DRY_WEIGHT.Text, out beforeWeight);
                double.TryParse(txtAFTER_DRY_WEIGHT.Text, out afterWight);
                txtDRY_WATER_VALUE.Text = Math.Round(beforeWeight - afterWight, 2).ToString();
            }
            else
            {
                var dtHemoParameters = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();
                loadParamsGrid(dtHemoParameters, strCureID, strRecipe_ID);
            }
        }

        /// <summary>
        /// 更新表单样式（新/旧表单）
        /// </summary>
        private void updateFormStyle(bool isNewForm)
        {
            if (isNewForm)
            {
                lblFiltrationType.Content = "血液滤过或血液透析滤过：透析中置换液总量：";

                lblFiltrationPercolate.Visibility = Visibility.Collapsed;
                txtFILTRATION_PERCOLATE.Visibility = Visibility.Collapsed;
                lblFiltrationPercolateUnit.Visibility = Visibility.Collapsed;

                lblFiltrationDisplacementUnit.Visibility = Visibility.Collapsed;

                rowPlasmaExchange.Height = new GridLength(0);
                spPlasmaExchange.Visibility = Visibility.Collapsed;

                lblNurseRecord.Text = "责任护士";
                lblSickLog.Content = "透析小结：";

                txtCHECK_NURSE.Visibility = Visibility.Visible;
                lblCheckNurse.Visibility = Visibility.Visible;
            }
            else
            {
                lblFiltrationType.Content = "血液滤过或血液透滤：透析中置换液总量：";

                lblFiltrationPercolate.Visibility = Visibility.Visible;
                txtFILTRATION_PERCOLATE.Visibility = Visibility.Visible;
                lblFiltrationPercolateUnit.Visibility = Visibility.Visible;

                lblFiltrationDisplacementUnit.Visibility = Visibility.Visible;

                rowPlasmaExchange.Height = new GridLength(30);
                spPlasmaExchange.Visibility = Visibility.Visible;

                lblNurseRecord.Text = "记录护士";
                lblSickLog.Content = "备注：";

                txtCHECK_NURSE.Visibility = Visibility.Collapsed;
                lblCheckNurse.Visibility = Visibility.Collapsed;
            }
        }

        private void setZeroToEmpty()
        {
            // 保留
        }

        private string setZeroToEmpty(string pValue)
        {
            string result = string.Empty;
            if (pValue.Trim() == "0")
            {
                return "/";
            }
            else if (string.IsNullOrEmpty(pValue.Trim()))
            {
                return "/";
            }
            else
            {
                result = pValue;
            }
            return result;
        }

        private void getHourAndMinute(string pHours)
        {
            if (pHours.IndexOf(".") > 0)
            {
                string[] strArrHours = pHours.Split('.');
                if (strArrHours.Length > 0)
                {
                    txtFREQUENCY_HOURS.Text = strArrHours[0];
                    txtMinute.Text = Utility.GetMinuteByHours(strArrHours[1]);
                }
            }
            else
            {
                txtFREQUENCY_HOURS.Text = pHours;
            }
        }

        private void loadParamsGrid(HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable dt, string pCureID, string pRecipeID)
        {
            var dtHemoParametersTemp = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();
            dt.CopyToDataTable<HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSRow>(dtHemoParametersTemp, LoadOption.PreserveChanges);

            var dtHemoParameters = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();

            for (int z = 0; z < dtHemoParameters.Columns.Count; z++)
            {
                if (dtHemoParameters.Columns[z].ColumnName != "CREATE_DATE")
                    dtHemoParameters.Columns[z].DataType = typeof(String);
            }

            var itemCollet = new List<string>();
            int p = 0;
            foreach (HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSRow item in dtHemoParametersTemp.Rows)
            {
                if (!item.IsCLINICAL_MANIFESTATIONNull())
                {
                    p = 0;
                    List<string> cureResultList = new List<string>();
                    string cureResult = "";
                    foreach (string str in item.CLINICAL_MANIFESTATION.Split('\r'))
                    {
                        if (string.IsNullOrEmpty(str))
                        {
                            cureResultList.Add(cureResult);
                        }
                        else
                        {
                            StringBuilder strbuilder = new StringBuilder();
                            int totalLen = 0;
                            for (int i = 0; i < str.Length; i++)
                            {
                                totalLen += GetPixelb(str[i].ToString(), null);
                                if (totalLen < WordPixel)
                                {
                                    strbuilder.Append(str[i]);
                                    if (i == str.Length - 1)
                                    {
                                        totalLen = 0;
                                        cureResultList.Add(strbuilder.ToString());
                                        strbuilder.Clear();
                                    }
                                }
                                else
                                {
                                    totalLen = 0;
                                    i--;
                                    cureResultList.Add(strbuilder.ToString());
                                    strbuilder.Clear();
                                }
                            }
                            strbuilder.Clear();
                            strbuilder = null;
                        }
                    }
                    string allStr = string.Empty;
                    foreach (string itemStr in cureResultList)
                    {
                        allStr += string.Format("{0}$", itemStr);
                    }

                    item.CLINICAL_MANIFESTATION = allStr;

                    var splitItem = item.CLINICAL_MANIFESTATION.Split('$');

                    foreach (var sitem in splitItem)
                    {
                        if (!string.IsNullOrEmpty(sitem))
                        {
                            p++;
                            item.CLINICAL_MANIFESTATION = sitem;
                            if (p == 1)
                                dtHemoParameters.LoadDataRow(item.ItemArray, LoadOption.PreserveChanges);
                            else
                            {
                                var dr = dtHemoParameters.NewMED_HEMODIALYSIS_PARAMETERSRow();
                                dr.HEMODIALYSIS_PARAMETERS_ID = System.Guid.NewGuid().ToString();
                                dr.CLINICAL_MANIFESTATION = sitem;
                                dr.CURE_ID = item.CURE_ID;
                                dr.RECIPE_ID = item.RECIPE_ID;
                                dtHemoParameters.AddMED_HEMODIALYSIS_PARAMETERSRow(dr);
                            }
                        }
                    }
                }
                else
                {
                    dtHemoParameters.LoadDataRow(item.ItemArray, LoadOption.PreserveChanges);
                }
            }

            // 使用 rowNum2 作为本页行数
            int paramRowCount = rowNum2 > 0 ? rowNum2 : 24;
            int currentParamNoShowInt = paramRowCount - dtHemoParameters.Rows.Count;

            if (currentParamNoShowInt > 0)
            {
                for (int i = 0; i < currentParamNoShowInt; i++)
                {
                    DataRow dr = dtHemoParameters.NewRow();
                    dr["HEMODIALYSIS_PARAMETERS_ID"] = System.Guid.NewGuid().ToString();
                    dr["CURE_ID"] = (i + 1).ToString();
                    dr["RECIPE_ID"] = (i + 1).ToString();
                    dtHemoParameters.Rows.Add(dr);
                }
            }

            // 处理0值和/值显示
            for (int j = 0; j < dtHemoParameters.Rows.Count; j++)
            {
                if (dtHemoParameters.Rows[j]["VASCULAR_ACCESS_ERRHYISIS"].ToString() == "1")
                {
                    dtHemoParameters.Rows[j]["VASCULAR_ACCESS_ERRHYISIS"] = "有";
                }
                else if (dtHemoParameters.Rows[j]["VASCULAR_ACCESS_ERRHYISIS"].ToString() == "0")
                {
                    dtHemoParameters.Rows[j]["VASCULAR_ACCESS_ERRHYISIS"] = "无";
                }

                if (dtHemoParameters.Rows[j]["VASCULAR_ACCESS_GLIDE"].ToString() == "1")
                {
                    dtHemoParameters.Rows[j]["VASCULAR_ACCESS_GLIDE"] = "有";
                }
                else if (dtHemoParameters.Rows[j]["VASCULAR_ACCESS_GLIDE"].ToString() == "0")
                {
                    dtHemoParameters.Rows[j]["VASCULAR_ACCESS_GLIDE"] = "无";
                }

                for (int z = 0; z < dtHemoParameters.Columns.Count; z++)
                {
                    if (dtHemoParameters.Rows[j][z].ToString() == "0" || dtHemoParameters.Rows[j][z].ToString() == "/")
                    {
                        if (j + 1 < dtHemoParameters.Rows.Count)
                        {
                            dtHemoParameters.Rows[j][z] = "/";
                        }
                        else
                        {
                            dtHemoParameters.Rows[j][z] = string.Empty;
                        }
                    }
                }
            }

            DictModel.MED_STAFF_DICTDataTable dtStaffSict = _staffDictService.GetStaffDictList();
            for (int i = 0; i < dtHemoParameters.Count(); i++)
            {
                string nurseId = dtHemoParameters[i]["NURSE_ID"].ToString();
                if (!string.IsNullOrEmpty(nurseId))
                {
                    dtHemoParameters[i]["EXTENDED_FIELD_1"] = (from x in dtStaffSict.AsEnumerable()
                                                               where x.EMP_NO == nurseId
                                                               select x.NAME).First();
                }
            }

            grdParameters.ItemsSource = dtHemoParameters.DefaultView;
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (this.grdParameters.ActualHeight > 0)
                {
                    // DataGrid 的实际高度包括表头+数据行，grid1 同样需要这个高度
                    this.grid1.Height = this.grdParameters.ActualHeight;
                    // 强制刷新布局
                    this.grid1.UpdateLayout();
                }
            }), System.Windows.Threading.DispatcherPriority.Render);
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void WPF_DocumentBase_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}