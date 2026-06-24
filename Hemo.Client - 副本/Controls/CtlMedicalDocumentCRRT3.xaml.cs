/*----------------------------------------------------------------
 * Copyright (C) 2005 麦迪斯顿(苏州)医疗科技发展有限公司
 * 文件功能描述:CRRT大夜班次护理记录单
 * 创建标识:吕志强-2017年9月18日
 * 
 * 修改时间:2017年9月29日
 * 修改人:吕志强
 * 修改描述:更新CRRT大夜班次护理记录单加载业务逻辑
 * ----------------------------------------------------------------*/

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

namespace Hemo.Client.Controls
{
    /// <summary>
    /// CtlMedicalDocument.xaml 的交互逻辑
    /// </summary>
    public partial class CtlMedicalDocumentCRRT3 : WPF_DocumentBase
    {
        #region 类变量

        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;

        private VascuarAccessService objVascuarAccess = new VascuarAccessService();

        int rowNum1;

        int rowNum2;

        private DataSet _CureMainData = new DataSet();

        private PatientScheduleModel.MED_PATIENT_SCHEDULERow currentPatientSchedule = null;

        #endregion

        #region 属性

        public PatientScheduleModel.MED_PATIENT_SCHEDULERow CurrentPatientSchedule
        {
            get { return currentPatientSchedule; }
            set { currentPatientSchedule = value; }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPatientSchedule"></param>
        /// <param name="pDs"></param>
        /// <param name="_rowNum1"></param>
        /// <param name="_rowNum2"></param>
        public CtlMedicalDocumentCRRT3(PatientScheduleModel.MED_PATIENT_SCHEDULERow currentPatientSchedule,DataSet pDs, int _rowNum1, int _rowNum2)
        {
            InitializeComponent();
            rowNum1 = _rowNum1;
            rowNum2 = _rowNum2;
            _CureMainData = pDs;
            this.currentPatientSchedule = currentPatientSchedule;
            loadData(pDs);
            IsShowGrid(false);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 是否显示治疗单grid信息 
        /// </summary>
        /// <param name="pValue">是否显示</param>
        public override void IsShowGrid(bool pValue)
        {
            if (!pValue)
            {
                grdParameters.Visibility = System.Windows.Visibility.Hidden;
                grid1.Visibility = System.Windows.Visibility.Hidden;
                lblSickLog.Visibility = System.Windows.Visibility.Hidden;
                txtSUMMARY.Visibility = System.Windows.Visibility.Hidden;
                lblDoctor.Visibility = System.Windows.Visibility.Hidden;
                lblNurse.Visibility = System.Windows.Visibility.Hidden;
                bodSummary2.Visibility = System.Windows.Visibility.Hidden;
                bodSummary3.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                grdParameters.Visibility = System.Windows.Visibility.Visible;
                lblSickLog.Visibility = System.Windows.Visibility.Visible;
                txtSUMMARY.Visibility = System.Windows.Visibility.Visible;
                grid1.Visibility = System.Windows.Visibility.Visible;
                lblDoctor.Visibility = System.Windows.Visibility.Visible;
                lblNurse.Visibility = System.Windows.Visibility.Visible;
                bodSummary2.Visibility = System.Windows.Visibility.Visible;
                bodSummary3.Visibility = System.Windows.Visibility.Visible;
            }
        }

        /// <summary>
        /// 根据治疗单数据集加载记录单信息
        /// </summary>
        /// <param name="pDs">治疗单数据集</param>
        private void loadData(DataSet pDs)
        {
            string strCureID = string.Empty;
            string strRecipe_ID = string.Empty;
            int addCount = 0;
            string birthday = string.Empty;
            DateTime dCureDate = new DateTime(); ;
            if (pDs != null && pDs.Tables.Count > 0)
            {
                string checkResult = string.Empty;
                string department = string.Empty;
                string strPatientType = string.Empty;

                #region 加载患者信息

                if (pDs.Tables["MED_PATIENTS"] != null)
                {
                    DataTable patientDataTable = pDs.Tables["MED_PATIENTS"];
                    if (patientDataTable != null && patientDataTable.Rows.Count > 0)
                    {
                        txtNAME.Text = patientDataTable.Rows[0]["NAME"].ToString();
                        txtSEX.Text = patientDataTable.Rows[0]["SEX"].ToString();
                        txtAGE.Text = patientDataTable.Rows[0]["AGE"].ToString();
                        txtPATIENT_ID.Text = patientDataTable.Rows[0]["PATIENT_ID"].ToString();
                        txtHEMODIALYSIS_ID.Text = patientDataTable.Rows[0]["HEMODIALYSIS_ID"].ToString();
                        txtADMISSION_NUMBER.Text = patientDataTable.Rows[0]["ADMISSION_NUMBER"].ToString();
                        txtDiagnose.Text = patientDataTable.Rows[0]["DIAGNOSE"].ToString();
                        birthday = patientDataTable.Rows[0]["BIRTHDAY"].ToString();
                        strPatientType = patientDataTable.Rows[0]["TIME_TYPE"].ToString();
                        checkResult = patientDataTable.Rows[0]["INFECTIOUS_CHECK_RESULT"].ToString();
                        department = patientDataTable.Rows[0]["WHAT_DEPARTMENT_IN"].ToString();
                        //计算透析次数
                        DataTable dtCureCount = objHemodialysisService.GetCureCountByHemoID(patientDataTable.Rows[0]["HEMODIALYSIS_ID"].ToString());
                        if (dtCureCount != null && dtCureCount.Rows.Count > 0)
                        {
                            txtCLEAN_UP_TIMES.Text = dtCureCount.Rows[0][0].ToString();
                        }
                    }
                }

                #endregion

                #region 加载处方信息

                if (pDs.Tables["MED_HEMO_RECIPE"] != null)
                {
                    DataTable retipeDataTable = pDs.Tables["MED_HEMO_RECIPE"];
                    if (retipeDataTable != null && retipeDataTable.Rows.Count > 0)
                    {
                        txtType.Text = retipeDataTable.Rows[0]["PURIFICATION_MODE_NAME"].ToString();
                        txtVASCULAR_ACCESS_ID.Text = retipeDataTable.Rows[0]["VASCULAR_ACCESS_NAME"].ToString();
                        lblHEPARIN_SPECIES.Content = "抗凝方式：" + retipeDataTable.Rows[0]["HEPARIN_SPECIES_NAME"].ToString();
                        txtFIRST_HEPARIN.Text = setZeroToEmpty(retipeDataTable.Rows[0]["FIRST_DRUG_DOSAGE"].ToString());
                        txtDOSIS_SUSTENTATIVA.Text = setZeroToEmpty(retipeDataTable.Rows[0]["SECOND_DRUG_DOSAGE"].ToString());
                        txtMACHINE_TYPE.Text = retipeDataTable.Rows[0]["MACHINE_TYPE_NAME"].ToString();
                        txtPURIFIER_NAME.Text = retipeDataTable.Rows[0]["PURIFIER_NAME"].ToString();
                        txtPURIFIER_M2.Text = setZeroToEmpty(retipeDataTable.Rows[0]["FIRST_PURIFIER_M2"].ToString());
                        txtMACHINE_ID.Text = retipeDataTable.Rows[0]["machine_name"].ToString();
                        txtUFR.Text = setZeroToEmpty(retipeDataTable.Rows[0]["UFR"].ToString());
                        txtDRY_WEIGHT.Text = setZeroToEmpty(retipeDataTable.Rows[0]["DRY_WEIGHT"].ToString());
                        txtBEFORE_DRY_WEIGHT.Text = setZeroToEmpty(retipeDataTable.Rows[0]["TODAY_WEIGHT"].ToString());
                        txtDISPLACEMENT_FLOW.Text = setZeroToEmpty(retipeDataTable.Rows[0]["DISPLACEMENT_FLOW"].ToString());
                        chkCRRT.IsChecked = true;
                        chkHDF.IsChecked = false;
                    }
                }

                #endregion

                #region 加载治疗单信息

                if (pDs.Tables["MED_CURE_MAIN"] != null)
                {
                    DataTable cureMainDataTable = pDs.Tables["MED_CURE_MAIN"];
                    if (cureMainDataTable != null && cureMainDataTable.Rows.Count > 0)
                    {
                        var dtCRRTCure = objHemodialysisService.GetCRRTCureByCureIdAndBanci(cureMainDataTable.Rows[0]["CURE_ID"].ToString(), "3");
                        dCureDate = Utility.CDate(cureMainDataTable.Rows[0]["CURE_CREATE_DATE"].ToString());
                        txtAGE.Text = Utility.GetAgeByCureDate(birthday.ToString(), dCureDate.ToString()).ToString();
                        txtCureYear.Text = Utility.CDate(cureMainDataTable.Rows[0]["CURE_CREATE_DATE"].ToString()).Year.ToString();
                        txtCureMouth.Text = Utility.CDate(cureMainDataTable.Rows[0]["CURE_CREATE_DATE"].ToString()).Month.ToString();
                        txtCureDay.Text = Utility.CDate(cureMainDataTable.Rows[0]["CURE_CREATE_DATE"].ToString()).Day.ToString();
                        txtType.Text = cureMainDataTable.Rows[0]["PURIFICATION_MODE_NAME"].ToString();
                        txtVASCULAR_ACCESS_ID.Text = cureMainDataTable.Rows[0]["VASCULAR_ACCESS_NAME"].ToString();
                        lblHEPARIN_SPECIES.Content = "抗凝方式：" + cureMainDataTable.Rows[0]["HEPARIN_SPECIES_NAME"].ToString();
                        lblFirst_Drug_Unit.Content = cureMainDataTable.Rows[0]["first_drug_unit_name"].ToString() == string.Empty ? "mg" : cureMainDataTable.Rows[0]["first_drug_unit_name"].ToString();
                        string secondUnit = cureMainDataTable.Rows[0]["second_drug_unit_name"].ToString();
                        lblSecond_Drug_Unit.Content = secondUnit.Equals(string.Empty) ? "mg/h" : ((secondUnit.Equals("mg") || secondUnit.Equals("μg") || secondUnit.Equals("ml") || secondUnit.Equals("u")) ? secondUnit + "/h" : secondUnit + "/小时");
                        txtAnticoagulantUnit.Text = lblSecond_Drug_Unit.Content.ToString();
                        txtMACHINE_TYPE.Text = cureMainDataTable.Rows[0]["MACHINE_TYPE_NAME"].ToString();
                        txtPURIFIER_NAME.Text = cureMainDataTable.Rows[0]["purifier_new_name"].ToString();
                        txtPURIFIER_M2.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["PURIFIER_M2"].ToString());
                        txtMACHINE_ID.Text = cureMainDataTable.Rows[0]["MACHINE_ID"] != DBNull.Value ? cureMainDataTable.Rows[0]["MACHINE_ID"].ToString() : cureMainDataTable.Rows[0]["machine_name"].ToString();
                        txtMACHINE_ID_TAG.Text = cureMainDataTable.Rows[0]["MACHINE_ID_TAG"].ToString();
                        txtCLEAN_UP_TIMES.Text = cureMainDataTable.Rows[0]["CLEAN_UP_TIMES"].ToString();
                        txtUFR.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["UFR"].ToString());
                        txtDRY_WATER_VALUE.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["DRY_WATER_VALUE"].ToString());
                        txtDISPLACEMENT_FLOW.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["DISPLACEMENT_FLOW"].ToString());
                        txtUF.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["UF"].ToString());
                        txtSUM_UF.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["SUM_UF"].ToString());
                        txtDISPLACEMENT_LIQUID.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["DISPLACEMENT_LIQUID"].ToString());
                        txtPERCOLATE.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["PERCOLATE"].ToString());
                        txtAMYLACEUM.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["AMYLACEUM"].ToString());
                        txtDOCTOR_ADVICE.Text = cureMainDataTable.Rows[0]["DOCTOR_ADVICE"].ToString();
                        txtPRIMARY_DOCTOR.Text = (dtCRRTCure != null && dtCRRTCure.Rows.Count > 0) ? dtCRRTCure.Rows[0]["PRIMARY_DOCTOR_NAME"].ToString() : string.Empty;
                        txtPRIMARY_NURSE.Text = (dtCRRTCure != null && dtCRRTCure.Rows.Count > 0) ? dtCRRTCure.Rows[0]["PRIMARY_NURSE_NAME"].ToString() : string.Empty;
                        txtCHECK_NURSE.Text = (dtCRRTCure != null && dtCRRTCure.Rows.Count > 0) ? dtCRRTCure.Rows[0]["CHECK_NURSE_NAME"].ToString() : string.Empty;
                        chkCRRT.IsChecked = true;
                        chkHDF.IsChecked = false;
                        
                        if (cureMainDataTable.Rows[0]["VEIN"].ToString().Length > 0)
                        {
                            txtVASCULAR_ACCESS_ID.Text += "+" + cureMainDataTable.Rows[0]["VEIN"].ToString();
                        }
                        
                        if (!string.IsNullOrEmpty(cureMainDataTable.Rows[0]["WHAT_DEPARTMENT_IN"].ToString()))
                        {
                            txtWHAT_DEPARTMENT_IN.Text = cureMainDataTable.Rows[0]["WHAT_DEPARTMENT_IN"].ToString();
                        }
                        else
                        {
                            txtWHAT_DEPARTMENT_IN.Text = department;
                            if (strPatientType.Equals("门诊"))
                            {
                                if (txtWHAT_DEPARTMENT_IN.Text.Length == 0)
                                {
                                    txtWHAT_DEPARTMENT_IN.Text = "门诊";
                                }
                            }
                        }

                        string strUSE_TYPE = cureMainDataTable.Rows[0]["USE_TYPE"].ToString();
                        if (strUSE_TYPE.Trim().Length > 0)
                        {
                            if (strUSE_TYPE.Trim() == "1")
                            {
                                chkUSE_TYPE1.IsChecked = true;
                            }
                        }

                        //血管通路相关
                        if (cureMainDataTable.Rows[0]["VASCULAR_ACCESS_FIRM"].ToString() == "1")
                        {
                            chkVASCULAR_ACCESS_FIRM1.IsChecked = true;
                        }
                        else if (cureMainDataTable.Rows[0]["VASCULAR_ACCESS_FIRM"].ToString() == "0")
                        {
                            chkVASCULAR_ACCESS_FIRM0.IsChecked = true;
                        }

                        if (cureMainDataTable.Rows[0]["VASCULAR_ACCESS_GLIDE"].ToString() == "1")
                        {
                            chkVASCULAR_ACCESS_GLIDE1.IsChecked = true;
                        }
                        else if (cureMainDataTable.Rows[0]["VASCULAR_ACCESS_GLIDE"].ToString() == "0")
                        {
                            chkVASCULAR_ACCESS_GLIDE0.IsChecked = true;
                        }

                        if (cureMainDataTable.Rows[0]["VASCULAR_ACCESS_SWELLING"].ToString() == "1")
                        {
                            chkVASCULAR_ACCESS_SWELLING1.IsChecked = true;
                        }
                        else if (cureMainDataTable.Rows[0]["VASCULAR_ACCESS_SWELLING"].ToString() == "0")
                        {
                            chkVASCULAR_ACCESS_SWELLING0.IsChecked = true;
                        }

                        if (cureMainDataTable.Rows[0]["VASCULAR_ACCESS_ERRHYISIS"].ToString() == "1")
                        {
                            chkVASCULAR_ACCESS_ERRHYISIS1.IsChecked = true;
                        }
                        else if (cureMainDataTable.Rows[0]["VASCULAR_ACCESS_ERRHYISIS"].ToString() == "0")
                        {
                            chkVASCULAR_ACCESS_ERRHYISIS0.IsChecked = true;
                        }

                        if (cureMainDataTable.Rows[0]["VASCULAR_ACCESS_THROMBUS"].ToString() == "1")
                        {
                            chkVASCULAR_ACCESS_THROMBUS1.IsChecked = true;
                        }
                        else if (cureMainDataTable.Rows[0]["VASCULAR_ACCESS_THROMBUS"].ToString() == "0")
                        {
                            chkVASCULAR_ACCESS_THROMBUS0.IsChecked = true;
                        }

                        if (cureMainDataTable.Rows[0]["VASCULAR_ACCESS_BLOOD"].ToString() == "1")
                        {
                            chkVASCULAR_ACCESS_BLOOD1.IsChecked = true;
                        }
                        else if (cureMainDataTable.Rows[0]["VASCULAR_ACCESS_BLOOD"].ToString() == "0")
                        {
                            chkVASCULAR_ACCESS_BLOOD0.IsChecked = true;
                        }
                        
                        if (cureMainDataTable.Rows[0]["HEPARIN_SPECIES_NAME"].ToString() != "无肝素透析")
                        {
                            txtFIRST_HEPARIN.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["FIRST_HEPARIN"].ToString());
                            txtDOSIS_SUSTENTATIVA.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["DOSIS_SUSTENTATIVA"].ToString());
                        } 

                        if (cureMainDataTable.Rows[0]["BEFORE_DRY_WEIGHT"].ToString() != "0")
                        {
                            txtBEFORE_DRY_WEIGHT.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["BEFORE_DRY_WEIGHT"].ToString());
                            lblBeforeUnit.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                        {
                            txtBEFORE_DRY_WEIGHT.Text = cureMainDataTable.Rows[0]["BEFORE_DRY_WEIGHT_TAG"].ToString();
                            lblBeforeUnit.Visibility = System.Windows.Visibility.Hidden;
                        }

                        if (cureMainDataTable.Rows[0]["AFTER_DRY_WEIGHT"].ToString() != "0")
                        {
                            txtAFTER_DRY_WEIGHT.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["AFTER_DRY_WEIGHT"].ToString());
                            lblAfterUnit.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                        {
                            txtAFTER_DRY_WEIGHT.Text = cureMainDataTable.Rows[0]["AFTER_DRY_WEIGHT_TAG"].ToString();
                            lblAfterUnit.Visibility = System.Windows.Visibility.Hidden;
                        }

                        //干体重
                        if (cureMainDataTable.Rows[0]["DRY_WEIGHT"].ToString() != "0")
                        {
                            txtDRY_WEIGHT.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["DRY_WEIGHT"].ToString());
                            lblDRY_WEIGHT.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                        {
                            txtDRY_WEIGHT.Text = cureMainDataTable.Rows[0]["DRY_WEIGHT_TAG"].ToString();
                            lblDRY_WEIGHT.Visibility = System.Windows.Visibility.Hidden;
                        }

                        if (dtCRRTCure != null && dtCRRTCure.Rows.Count > 0)
                        {
                            string strSummary = dtCRRTCure.Rows[0]["SUMMARY"].ToString();
                            if (strSummary.Length <= 180)
                            {
                                txtSUMMARY.Text = "                  " + strSummary;
                            }
                            else
                            {
                                txtSUMMARY.Text = "                  " + strSummary.Substring(0, 180);
                            }
                        }

                        if (!string.IsNullOrEmpty(cureMainDataTable.Rows[0]["INFECTIOUS_CHECK_RESULT"].ToString()))
                        {
                            textBox3.Text = cureMainDataTable.Rows[0]["INFECTIOUS_CHECK_RESULT"].ToString();
                        }
                        else
                        {
                            textBox3.Text = checkResult;
                        }

                        HemoModel.MED_CURE_SIGNDataTable dtCureSign = objHemodialysisService.GetCureSignByHemoIdAndCureId(this.txtHEMODIALYSIS_ID.Text, cureMainDataTable.Rows[0]["CURE_ID"].ToString());
                        LoadCureSign(dtCureSign);
                    }
                }

                #endregion

                #region 加载时间信息

                txtBeginTime.Text = currentPatientSchedule["START_TIME"] != DBNull.Value ? currentPatientSchedule.START_TIME.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
                txtEndTime.Text = currentPatientSchedule["END_TIME"] != DBNull.Value ? currentPatientSchedule.END_TIME.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
                if (txtBeginTime.Text != string.Empty && txtEndTime.Text != string.Empty)
                {
                    DateTime begin = Utility.CDate(txtBeginTime.Text);
                    DateTime end = Utility.CDate(txtEndTime.Text);
                    TimeSpan span = end.Subtract(begin);
                    StringBuilder time = new StringBuilder();
                    if (span.Hours > 0)
                    {
                        time.Append(span.Hours.ToString()).Append("小时");
                    }
                    if (span.Minutes > 0)
                    {
                        time.Append(span.Minutes.ToString()).Append("分");
                    }
                    if (span.Seconds > 0)
                    {
                        time.Append(span.Seconds.ToString()).Append("秒");
                    }
                    txtSumTime.Text = time.ToString();
                }

                #endregion

                #region 加载透析参数列表

                if (pDs.Tables["MED_HEMODIALYSIS_PARAMETERS"] != null && pDs.Tables["MED_HEMODIALYSIS_PARAMETERS"].Rows.Count > 0)
                {
                    DataTable dtHemoParameters = pDs.Tables["MED_HEMODIALYSIS_PARAMETERS"];
                    var dtParam = dtHemoParameters.Clone();
                    dtHemoParameters.AsEnumerable().Where(row => row["CRRT_CLASS"].Equals("3")).CopyToDataTable(dtParam, LoadOption.OverwriteChanges);
                    strCureID = dtParam.Rows[0]["CURE_ID"].ToString();
                    strRecipe_ID = dtParam.Rows[0]["RECIPE_ID"].ToString();
                    if (dtParam.Rows.Count < 10)
                    {
                        //addCount = 10 - dtHemoParameters.AsEnumerable().Where(i => i["EXTENDED_FIELD_3"].ToString() != "抢救").ToList().Count;
                        addCount = 10 - dtParam.AsEnumerable().ToList().Count;
                    }

                    loadParamsGrid(dtParam, addCount, strCureID, strRecipe_ID);
                }
                else
                {
                    #region 如果透析参数表中没有记录，根据采集标示直接取采集表数据
                    //HemodialysisModel.MED_HEMO_PARAMETERS_COLLECTIONDataTable parameterCollectionData = objHemodialysisService.GetHemoParametersCollectionByMonitorAndDate("AK96_1", dCureDate);
                    //if (parameterCollectionData != null && parameterCollectionData.Rows.Count > 0)
                    //{
                    //    if (parameterCollectionData != null && parameterCollectionData.Rows.Count > 6)
                    //    {
                    //        for (int i = parameterCollectionData.Rows.Count - 1; i > 5; i--)
                    //        {
                    //            parameterCollectionData.Rows[i].Delete();
                    //        }
                    //        grdParameters.ItemsSource = parameterCollectionData.DefaultView;
                    //    }
                    //    else if (parameterCollectionData != null && parameterCollectionData.Rows.Count <= 6)
                    //    {
                    //        grdParameters.ItemsSource = parameterCollectionData.DefaultView;
                    //    }
                    //}
                    //else
                    //{
                    //    DataTable dtHemoParameters = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();
                    //    loadParamsGrid(dtHemoParameters, 10, strCureID, strRecipe_ID);
                    //}
                    #endregion
                    DataTable dtHemoParameters = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();
                    loadParamsGrid(dtHemoParameters, 10, strCureID, strRecipe_ID);
                }

                #endregion

                #region 加载医生记录及医嘱

                if (pDs.Tables["MED_CURE_DRUG"] != null)
                {
                    DataTable dtMED_CURE_DRUG = pDs.Tables["MED_CURE_DRUG"];
                    //if (dtMED_CURE_DRUG != null && dtMED_CURE_DRUG.Rows.Count > 0) {
                    //    grdGiveDrug.ItemsSource = dtMED_CURE_DRUG.DefaultView;
                    //}
                    strCureID = dtMED_CURE_DRUG.Rows[0]["CURE_ID"].ToString();
                    strRecipe_ID = dtMED_CURE_DRUG.Rows[0]["RECIPE_ID"].ToString();
                    if (dtMED_CURE_DRUG.Rows.Count < 4)
                    {
                        addCount = 4 - dtMED_CURE_DRUG.Rows.Count;
                    }
                    //组织临时用药内容
                    StringBuilder sbDrug = new StringBuilder();

                    ////成组药品的拼接
                    //DataTable dtTempDrug = dtMED_CURE_DRUG;
                    //for (int i = 0; i < dtTempDrug.Rows.Count; i++) {
                    //    DataTable dtSumDrug = Utility.GetSubTable(dtTempDrug, "COM_NO='" + dtTempDrug.Rows[i]["COM_NO"].ToString() + "'");
                    //    if (dtSumDrug != null && dtSumDrug.Rows.Count > 1) {
                    //        for (int j = 0; j < dtSumDrug.Rows.Count; j++) {
                    //            sbDrug.Append(dtSumDrug.Rows[j]["NEW_DRUG_NAME"].ToString());
                    //            sbDrug.Append(dtSumDrug.Rows[j]["DOSAGE"].ToString());
                    //            sbDrug.Append(dtSumDrug.Rows[j]["UNIT_NAME"].ToString());
                    //            sbDrug.Append(dtSumDrug.Rows[j]["DRUG_MODE_NAME"].ToString()).Append("+");
                    //        }
                    //    }
                    //    else {
                    //        sbDrug.Append(dtTempDrug.Rows[i]["NEW_DRUG_NAME"].ToString());
                    //        sbDrug.Append(dtTempDrug.Rows[i]["DOSAGE"].ToString());
                    //        sbDrug.Append(dtTempDrug.Rows[i]["UNIT_NAME"].ToString());
                    //        sbDrug.Append(dtTempDrug.Rows[i]["DRUG_MODE_NAME"].ToString()).Append(";");
                    //    }
                    //}

                    //单条药品拼接
                    for (int i = 0; i < dtMED_CURE_DRUG.Rows.Count; i++)
                    {
                        sbDrug.Append(dtMED_CURE_DRUG.Rows[i]["NEW_DRUG_NAME"].ToString());
                        sbDrug.Append(dtMED_CURE_DRUG.Rows[i]["DOSAGE"].ToString());
                        sbDrug.Append(dtMED_CURE_DRUG.Rows[i]["UNIT_NAME"].ToString());
                        sbDrug.Append(dtMED_CURE_DRUG.Rows[i]["DRUG_MODE_NAME"].ToString()).Append(";");
                    }

                    if (sbDrug.ToString().Length > 0)
                    {
                        txtDOCTOR_ADVICE.Text = txtDOCTOR_ADVICE.Text.Equals(string.Empty) ? sbDrug.ToString() : txtDOCTOR_ADVICE.Text + ";" + sbDrug.ToString();
                    }
                }
                else
                {
                    DataTable dtMED_CURE_DRUG = new HemodialysisModel.MED_CURE_DRUGDataTable();
                    //loadGiveDrugGrid(dtMED_CURE_DRUG, 4, strCureID, strRecipe_ID);
                }

                #endregion
            }
            else
            {
                DataTable dtHemoParameters = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();
                loadParamsGrid(dtHemoParameters, 10, strCureID, strRecipe_ID);
                DataTable dtMED_CURE_DRUG = new HemodialysisModel.MED_CURE_DRUGDataTable();
                // loadGiveDrugGrid(dtMED_CURE_DRUG, 4, strCureID, strRecipe_ID);
            }
        }

        private string setZeroToEmpty(string pValue)
        {
            string result = string.Empty;
            if (pValue.Trim() == "0")
            {
                return result;
            }
            else
            {
                result = pValue;
            }
            return result;
        }

        /// <summary>
        /// 设置血管通路
        /// </summary>
        /// <param name="pName"></param>
        private void setVascularAccess(string pName, string pHEMODIALYSIS_ID, string pVASCULAR_ACCESS_ID)
        {
            //switch (pName) {
            //    case "内瘘":
            //        chkInternalFistula.IsChecked = true;
            //        break;
            //    case "颈内静脉":
            //        chkJugularVein.IsChecked = true;
            //        break;
            //    case "锁骨下静脉":
            //        chkVenaeSubclavia.IsChecked = true;
            //        break;
            //    case "股静脉":
            //        chkFemoralVein.IsChecked = true;
            //        break;
            //    default:
            //        txtVASCULAR_ACCESS_ID.Text = pName;
            //        break;
            //}

            //DataTable GascuarAccessTable = objVascuarAccess.GetVascuarAccessNameByID(pHEMODIALYSIS_ID);
            //if (GascuarAccessTable != null && GascuarAccessTable.Rows.Count > 0) {
            //    GascuarAccessTable = Utility.GetSubTable(GascuarAccessTable, "VASCULAR_ACCESS_TYPE='" + pVASCULAR_ACCESS_ID + "'");
            //    if (GascuarAccessTable != null && GascuarAccessTable.Rows.Count > 0) {
            //        if (GascuarAccessTable.Rows[0]["vascular_access_type_name"].ToString() == "永久性静脉留置导管" || GascuarAccessTable.Rows[0]["vascular_access_type_name"].ToString() == "临时静脉留置导管") {
            //            if (GascuarAccessTable.Rows[0]["VASCULAR_POSTION_NAME"].ToString() == "颈内静脉") {
            //                chkJugularVein.IsChecked = true;
            //            }
            //            else if (GascuarAccessTable.Rows[0]["VASCULAR_POSTION_NAME"].ToString() == "锁骨下静脉") {
            //                chkVenaeSubclavia.IsChecked = true;
            //            }
            //            else if (GascuarAccessTable.Rows[0]["VASCULAR_POSTION_NAME"].ToString() == "股静脉") {
            //                chkFemoralVein.IsChecked = true;
            //            }
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 设置血管通路
        /// </summary>
        /// <param name="pVascularAccess"></param>
        private void setVASCULAR_ACCESS_ID(string pVascularAccess)
        {
            //switch (pVascularAccess) {
            //    case "内瘘":
            //        chkInternalFistula.IsChecked = true;
            //        break;
            //    case "颈内静脉":
            //        chkJugularVein.IsChecked = true;
            //        break;
            //    case "锁骨下静脉":
            //        chkVenaeSubclavia.IsChecked = true;
            //        break;
            //    case "股静脉":
            //        chkFemoralVein.IsChecked = true;
            //        break;
            //    default:
            txtVASCULAR_ACCESS_ID.Text = pVascularAccess;
            //    break;
            //  }
        }

        //private void loadGiveDrugGrid(DataTable dtMED_CURE_DRUG, int count, string pCureID, string pRecipeID) {
        //    if (count > 0) {
        //        for (int i = 1; i < count; i++) {
        //            DataRow dr = dtMED_CURE_DRUG.NewRow();
        //            dr["CURE_DRUG_ID"] = System.Guid.NewGuid().ToString();
        //            dr["CURE_ID"] = i.ToString();
        //            dr["RECIPE_ID"] = i.ToString();
        //            dtMED_CURE_DRUG.Rows.Add(dr);
        //        }
        //    }
        //    //药品执行记录需要现场做调试
        //   grdGiveDrug.ItemsSource = dtMED_CURE_DRUG.DefaultView;
        //}

        /// <summary>
        /// 载入透析参数数据列表
        /// </summary>
        /// <param name="dtParam"></param>
        /// <param name="count"></param>
        /// <param name="pCureID"></param>
        /// <param name="pRecipeID"></param>
        private void loadParamsGrid(DataTable dtParam, int count, string pCureID, string pRecipeID)
        {
            rowNum1 = dtParam.Rows.Count;
            var dtHemoParameters = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();

            if (rowNum1 != 0)
            {
                dtHemoParameters = objHemodialysisService.GetHemoParametersByHemoParamRow(pCureID, rowNum1, rowNum2, "sqlByParams");
                dtParam = dtHemoParameters.Clone();
                dtHemoParameters.AsEnumerable().Where(row => row["CRRT_CLASS"].Equals("3")).CopyToDataTable(dtParam, LoadOption.OverwriteChanges);
            }

            for (int i = 0; i < count; i++)
            {
                DataRow dr = dtParam.NewRow();
                dr["HEMODIALYSIS_PARAMETERS_ID"] = System.Guid.NewGuid().ToString();
                dr["CURE_ID"] = (i + 1).ToString();
                dr["RECIPE_ID"] = (i + 1).ToString();
                dtParam.Rows.Add(dr);
            }

            for (int j = 0; j < dtParam.Rows.Count; j++)
            {
                if (dtParam.Rows[j]["VASCULAR_ACCESS_ERRHYISIS"].ToString() == "1")
                {
                    dtParam.Rows[j]["VASCULAR_ACCESS_ERRHYISIS"] = "有";
                }
                else if (dtParam.Rows[j]["VASCULAR_ACCESS_ERRHYISIS"].ToString() == "0")
                {
                    dtParam.Rows[j]["VASCULAR_ACCESS_ERRHYISIS"] = "无";
                }

                if (dtParam.Rows[j]["VASCULAR_ACCESS_GLIDE"].ToString() == "1")
                {
                    dtParam.Rows[j]["VASCULAR_ACCESS_GLIDE"] = "有";
                }
                else if (dtParam.Rows[j]["VASCULAR_ACCESS_GLIDE"].ToString() == "0")
                {
                    dtParam.Rows[j]["VASCULAR_ACCESS_GLIDE"] = "无";
                }

                for (int z = 0; z < dtParam.Columns.Count; z++)
                {
                    if (dtParam.Rows[j][z].ToString() == "0")
                    {
                        dtParam.Rows[j][z] = DBNull.Value;
                    }
                }
                ////如果为23：59：00则认为是最后一行清空其他数据
                //var strCreateTime = Utility.CDate(dtHemoParameters.Rows[j]["CREATE_DATE"].ToString()).ToLongTimeString();
                //if (strCreateTime == "23:59:00") {
                //    dtHemoParameters.Rows[j]["VENOUS_PRESSURE"] = DBNull.Value;
                //    dtHemoParameters.Rows[j]["TRANSMEMBRANE_PRESSURE"] = DBNull.Value;
                //    dtHemoParameters.Rows[j]["CONDUCTIVITY"] = DBNull.Value;
                //    dtHemoParameters.Rows[j]["DISPLACEMENT"] = DBNull.Value;
                //    dtHemoParameters.Rows[j]["BLOOD_FLOW"] = DBNull.Value;
                //    dtHemoParameters.Rows[j]["DIALYSATE_RATE"] = DBNull.Value;
                //    dtHemoParameters.Rows[j]["URF"] = DBNull.Value;
                //    dtHemoParameters.Rows[j]["ANTICOAGULANT"] = DBNull.Value;
                //    dtHemoParameters.Rows[j]["TEMPERATURE"] = DBNull.Value;
                //    dtHemoParameters.Rows[j]["VASCULAR_ACCESS_GLIDE"] = DBNull.Value;
                //    dtHemoParameters.Rows[j]["CLINICAL_MANIFESTATION"] = DBNull.Value;
                //    dtHemoParameters.Rows[j]["VASCULAR_ACCESS_ERRHYISIS"] = DBNull.Value;
                //    dtHemoParameters.Rows[j]["CREATE_DATE"] = DBNull.Value;
                //}
            }

            //过滤第一行数据，拼接最后一行数据
            //if (dtHemoParameters != null && dtHemoParameters.Rows.Count >= 10) {
            //dtHemoParameters.Rows[0]["VENOUS_PRESSURE"] = DBNull.Value;
            //dtHemoParameters.Rows[0]["TRANSMEMBRANE_PRESSURE"] = DBNull.Value;
            //dtHemoParameters.Rows[0]["CONDUCTIVITY"] = DBNull.Value;
            //dtHemoParameters.Rows[0]["DISPLACEMENT"] = DBNull.Value;
            //dtHemoParameters.Rows[0]["BLOOD_FLOW"] = DBNull.Value;
            //dtHemoParameters.Rows[0]["DIALYSATE_RATE"] = DBNull.Value;
            //dtHemoParameters.Rows[0]["URF"] = DBNull.Value;
            //dtHemoParameters.Rows[0]["ANTICOAGULANT"] = DBNull.Value;
            //dtHemoParameters.Rows[0]["TEMPERATURE"] = DBNull.Value;
            //dtHemoParameters.Rows[0]["VASCULAR_ACCESS_GLIDE"] = DBNull.Value;
            //dtHemoParameters.Rows[0]["CLINICAL_MANIFESTATION"] = DBNull.Value;
            //dtHemoParameters.Rows[0]["VASCULAR_ACCESS_ERRHYISIS"] = DBNull.Value;
            // dtHemoParameters.Rows[0]["CREATE_DATE"] = DBNull.Value;
            //dtHemoParameters.Rows[9]["CLINICAL_MANIFESTATION"] = "返血0.9% NS 200ml";
            //}

            //拼接药品遗嘱执行
            if (_CureMainData.Tables["MED_CURE_DRUG"] != null)
            {
                DataTable dtDrug = _CureMainData.Tables["MED_CURE_DRUG"];
                int iDrugCount = dtDrug.Rows.Count;
                DataTable dtDrugIn = Utility.GetSubTable(dtDrug, "DRUG_TIMETYPE='透析中' and status = '1'");
                if (dtDrugIn != null && dtDrugIn.Rows.Count > 0)
                {
                    if (dtDrugIn.Rows.Count <= 8)
                    {
                        for (int d = 0; d < dtDrugIn.Rows.Count; d++)
                        {
                            dtParam.Rows[d + 1]["CLINICAL_MANIFESTATION"] = dtDrugIn.Rows[d]["NEW_DRUG_NAME"].ToString() + dtDrugIn.Rows[d]["DOSAGE"].ToString() + dtDrugIn.Rows[d]["UNIT_NAME"].ToString() + dtDrugIn.Rows[d]["DRUG_MODE_NAME"].ToString();
                        }
                    }
                }

                dtDrug = Utility.GetSubTable(dtDrug, "DRUG_TIMETYPE='透析后' and status = '1'");
                int iDCount = dtDrug.Rows.Count;
                if (dtDrug != null && dtDrug.Rows.Count > 0)
                {
                    if (iDCount >= 1)
                    {
                        if (dtParam.Rows[8]["CLINICAL_MANIFESTATION"].ToString().Length == 0)
                        {
                            if (dtDrug.Rows[0] != null)
                            {
                                dtParam.Rows[8]["CLINICAL_MANIFESTATION"] = dtDrug.Rows[0]["NEW_DRUG_NAME"].ToString() + dtDrug.Rows[0]["DOSAGE"].ToString() + dtDrug.Rows[0]["UNIT_NAME"].ToString() + dtDrug.Rows[0]["DRUG_MODE_NAME"].ToString();
                            }
                        }
                    }
                    if (iDCount >= 2)
                    {
                        if (dtParam.Rows[7]["CLINICAL_MANIFESTATION"].ToString().Length == 0)
                        {
                            if (dtDrug.Rows[1] != null)
                            {
                                dtParam.Rows[7]["CLINICAL_MANIFESTATION"] = dtDrug.Rows[1]["NEW_DRUG_NAME"].ToString() + dtDrug.Rows[1]["DOSAGE"].ToString() + dtDrug.Rows[1]["UNIT_NAME"].ToString() + dtDrug.Rows[1]["DRUG_MODE_NAME"].ToString();
                            }
                        }
                    }
                    if (iDCount >= 3)
                    {
                        if (dtParam.Rows[6]["CLINICAL_MANIFESTATION"].ToString().Length == 0)
                        {
                            if (dtDrug.Rows[2] != null)
                            {
                                dtParam.Rows[6]["CLINICAL_MANIFESTATION"] = dtDrug.Rows[2]["NEW_DRUG_NAME"].ToString() + dtDrug.Rows[2]["DOSAGE"].ToString() + dtDrug.Rows[2]["UNIT_NAME"].ToString() + dtDrug.Rows[2]["DRUG_MODE_NAME"].ToString();
                            }
                        }
                    }
                }
            }

            grdParameters.ItemsSource = dtParam.DefaultView;
        }

        private void InitGridTextBlock(Grid grid)
        {
            for (int i = 1; i < grid.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < grid.ColumnDefinitions.Count; j++)
                {
                    TextBox txtBox = new TextBox();
                    txtBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    txtBox.Width = 60;
                    txtBox.Height = 23;
                    Thickness thickness = new Thickness();
                    thickness.Top = 0;
                    thickness.Left = 0;
                    thickness.Right = 0;
                    thickness.Bottom = 0;
                    txtBox.BorderThickness = thickness;
                    txtBox.TextAlignment = TextAlignment.Center;
                    txtBox.TextWrapping = TextWrapping.Wrap;
                    txtBox.SetValue(Grid.RowProperty, i);
                    txtBox.SetValue(Grid.ColumnProperty, j);
                    grid.Children.Add(txtBox);
                }
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //int index = grdParameters.SelectedIndex; //当前行号
            //var row = grdParameters.ItemContainerGenerator.ContainerFromItem(grdParameters.Items[index]) as DataGridRow;
            //row.Background = new SolidColorBrush(Colors.White);//设置选中行的颜色
        }

        /// <summary>
        /// 加载透析单签名
        /// </summary>
        /// <param name="dtCureSign"></param>
        private void LoadCureSign(HemoModel.MED_CURE_SIGNDataTable dtCureSign)
        {
            if (dtCureSign != null && dtCureSign.Rows.Count > 0)
            {
                if (dtCureSign.Rows[0]["PRIMARY_DOCTOR_SIGN"] != DBNull.Value)
                {
                    if (dtCureSign[0].PRIMARY_DOCTOR_SIGN.Length > 0)
                    {
                        LoadImage(this.imgPRIMARY_DOCTOR, dtCureSign[0].PRIMARY_DOCTOR_SIGN);
                    }
                }

                if (dtCureSign.Rows[0]["PRIMARY_NURSE_SIGN"] != DBNull.Value)
                {
                    if (dtCureSign[0].PRIMARY_NURSE_SIGN.Length > 0)
                    {
                        LoadImage(this.imgPRIMARY_NURSE, dtCureSign[0].PRIMARY_NURSE_SIGN);
                    }
                }

                if (dtCureSign.Rows[0]["CHECK_NURSE_SIGN"] != DBNull.Value)
                {
                    if (dtCureSign[0].CHECK_NURSE_SIGN.Length > 0)
                    {
                        LoadImage(this.imgCHECK_NURSE, dtCureSign[0].CHECK_NURSE_SIGN);
                    }
                }
            }
        }

        /// <summary>
        /// 加载签名图片
        /// </summary>
        /// <param name="img"></param>
        /// <param name="source"></param>
        private void LoadImage(Image img, byte[] source)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = new MemoryStream(source);
            image.EndInit();
            if (image.Width <= img.Width && image.Height <= img.Height)
            {
                img.Stretch = Stretch.None;
            }
            else
            {
                img.Stretch = Stretch.Uniform;
            }
            img.Source = image;
        }

        /// <summary>
        /// 设置Image、TextBox的是否可见
        /// </summary>
        /// <param name="imgVisible"></param>
        /// <param name="txtVisible"></param>
        public override void SetImageAndTextBoxVisible(Visibility imgVisible,Visibility txtVisible)
        {
            this.imgPRIMARY_DOCTOR.Visibility = this.imgPRIMARY_NURSE.Visibility = this.imgCHECK_NURSE.Visibility = imgVisible;
            this.txtPRIMARY_DOCTOR.Visibility = this.txtPRIMARY_NURSE.Visibility = this.txtCHECK_NURSE.Visibility = txtVisible;
        }

        #endregion
    }
}
