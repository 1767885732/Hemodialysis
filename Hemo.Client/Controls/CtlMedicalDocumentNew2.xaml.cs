/*----------------------------------------------------------------
// Copyright (C) 2013 苏州麦迪斯顿医疗科技股份有限公司股份有限公司
// 文件名：CtlMedicalDocumentNew2.cs
// 文件功能描述：血 液 净 化 治 疗 记 录 单
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

namespace Hemo.Client.Controls
{
    /// <summary>
    /// CtlMedicalDocument.xaml 的交互逻辑
    /// </summary>
    public partial class CtlMedicalDocumentNew2 : WPF_DocumentBase
    {
        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;

        private VascuarAccessService objVascuarAccess = new VascuarAccessService();


        private DataSet _CureMainData = new DataSet();
        private PatientScheduleModel.MED_PATIENT_SCHEDULERow currentPatientSchedule = null;
        public PatientScheduleModel.MED_PATIENT_SCHEDULERow CurrentPatientSchedule
        {
            get { return currentPatientSchedule; }
            set { currentPatientSchedule = value; }
        }

        public CtlMedicalDocumentNew2(PatientScheduleModel.MED_PATIENT_SCHEDULERow currentPatientSchedule, DataSet pDs)
        {
            InitializeComponent();
            this.HospitalTitle.Content = Utility.GetHospitalName();

            this.currentPatientSchedule = currentPatientSchedule;
            _CureMainData = pDs;
            loadData(pDs);
            IsShowGrid(false);
        }

        /// <summary>
        /// 根据治疗单数据集赋值
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
                        txtAllergic.Text = patientDataTable.Rows[0]["BASEINFO"].ToString();//过敏史

                    }
                }




                #endregion

                #region 加载处方信息

                if (pDs.Tables["MED_HEMO_RECIPE"] != null)
                {
                    DataTable retipeDataTable = pDs.Tables["MED_HEMO_RECIPE"];
                    if (retipeDataTable != null && retipeDataTable.Rows.Count > 0)
                    {
                        ////设置当前单子为CRRT或是HDF
                        //setPurificationMode(retipeDataTable.Rows[0]["PURIFICATION_MODE_NAME"].ToString());
                        //this.txtPurMode.Text= retipeDataTable.Rows[0]["PURIFICATION_MODE_NAME"].ToString()

                        txtVASCULAR_ACCESS_ID.Text = retipeDataTable.Rows[0]["VASCULAR_ACCESS_NAME"].ToString();
                        lblHEPARIN_SPECIES.Text = retipeDataTable.Rows[0]["HEPARIN_SPECIES_NAME"].ToString();
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
                        ////设置当前单子为CRRT或是HDF
                        //setPurificationMode(cureMainDataTable.Rows[0]["PURIFICATION_MODE_NAME"].ToString());

                        this.txtPurMode.Text = cureMainDataTable.Rows[0]["PURIFICATION_MODE_NAME"].ToString();

                        if (cureMainDataTable.Rows[0]["PURIFICATION_MODE_NAME"].ToString() == "HD+HP" || cureMainDataTable.Rows[0]["PURIFICATION_MODE_NAME"].ToString() == "HP")
                        {
                            this.lbglq.Visibility = System.Windows.Visibility.Visible;
                            this.txtIN_BASKET_WOUND_ALLERGY.Visibility = System.Windows.Visibility.Visible;
                            this.txtIN_BASKET_WOUND_ALLERGY.Text = cureMainDataTable.Rows[0]["IN_BASKET_WOUND_ALLERGY"].ToString();

                        }
                        else
                        {
                            this.lbglq.Visibility = System.Windows.Visibility.Hidden;
                            this.txtIN_BASKET_WOUND_ALLERGY.Visibility = System.Windows.Visibility.Hidden;
                        }

                        dCureDate = Utility.CDate(cureMainDataTable.Rows[0]["CURE_CREATE_DATE"].ToString());
                        txtAGE.Text = Utility.GetAgeByCureDate(birthday.ToString(), dCureDate.ToString()).ToString();
                        txtCureYear.Text = Utility.CDate(cureMainDataTable.Rows[0]["CURE_CREATE_DATE"].ToString()).Year.ToString();
                        txtCureMouth.Text = Utility.CDate(cureMainDataTable.Rows[0]["CURE_CREATE_DATE"].ToString()).Month.ToString();
                        txtCureDay.Text = Utility.CDate(cureMainDataTable.Rows[0]["CURE_CREATE_DATE"].ToString()).Day.ToString();

                        lblHEPARIN_SPECIES.Text = cureMainDataTable.Rows[0]["HEPARIN_SPECIES_NAME"].ToString();



                        lblFirst_Drug_Unit.Content = cureMainDataTable.Rows[0]["first_drug_unit_name"].ToString() == string.Empty ? "mg        追加：" : cureMainDataTable.Rows[0]["first_drug_unit_name"].ToString() + "        追加：";

                        string secondUnit = cureMainDataTable.Rows[0]["second_drug_unit_name"].ToString();

                        lblSecond_Drug_Unit.Content = secondUnit.Equals(string.Empty) ? "mg/h           鱼精蛋白：" : ((secondUnit.Equals("mg") || secondUnit.Equals("μg") || secondUnit.Equals("ml") || secondUnit.Equals("u")) ? secondUnit + "/h       鱼精蛋白：" : secondUnit + "/小时       鱼精蛋白：");

                        txtAnticoagulantUnit.Text = secondUnit + "/h";// lblSecond_Drug_Unit.Content.ToString();

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

                        #region CRRT

                        //txtFILTRATION_DISPLACEMENT_LIQUID.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["FILTRATION_DISPLACEMENT_LIQUID"].ToString()); //置换液
                        //txtFILTRATION_PERCOLATE.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["FILTRATION_PERCOLATE"].ToString());//滤出液
                        //txtDISPLACEMENT_LIQUID.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["DISPLACEMENT_LIQUID"].ToString());//输入血浆/白蛋白总量置换液
                        //txtPERCOLATE.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["PERCOLATE"].ToString());//滤出液

                        #endregion

                        txtAMYLACEUM.Text = cureMainDataTable.Rows[0]["AMYLACEUM"].ToString();
                        txtDOCTOR_ADVICE.Text = cureMainDataTable.Rows[0]["DOCTOR_ADVICE"].ToString();
                        txtPRIMARY_DOCTOR.Text = cureMainDataTable.Rows[0]["DOCTOR_NAME"].ToString();
                        txtPRIMARY_NURSE.Text = cureMainDataTable.Rows[0]["NURSE_NAME"].ToString();
                        txtCHECK_NURSE.Text = cureMainDataTable.Rows[0]["check_nurse_name"].ToString();
                        lupPUNCTURE_NURSE.Text = cureMainDataTable.Rows[0]["PUNCTURE_NURSE_NAME"].ToString();

                        txtPotassium.Text = cureMainDataTable.Rows[0]["POTASSIUM_ION"].ToString();//钾
                        txtCalcium.Text = cureMainDataTable.Rows[0]["CALCIUM_ION"].ToString();//钙
                        txtSodium.Text = cureMainDataTable.Rows[0]["SODION"].ToString();//钠
                        txtBicarbonate.Text = cureMainDataTable.Rows[0]["BIRCARBONATE"].ToString();//碳酸氢盐、根
                        txtFlow.Text = cureMainDataTable.Rows[0]["DIALYSATE_FLOW"].ToString();//血流量


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

                        //string strUSE_TYPE = cureMainDataTable.Rows[0]["USE_TYPE"].ToString();
                        //if (strUSE_TYPE.Trim().Length > 0)
                        //{
                        //    if (strUSE_TYPE.Trim() == "1")
                        //    {
                        //        chkUSE_TYPE1.IsChecked = true;
                        //    }
                        //    else
                        //    {
                        //        chkUSE_TYPE1.IsChecked = false;
                        //    }
                        //}

                        //血管通路相关
                        #region 导管评估
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

                        if (cureMainDataTable.Rows[0]["VASCULAR_ACCESS_BLOOD_INFECT"].ToString() == "1")
                        {
                            chkVASCULAR_ACCESS_BLOOD_INFECT1.IsChecked = true;
                        }
                        else if (cureMainDataTable.Rows[0]["VASCULAR_ACCESS_BLOOD_INFECT"].ToString() == "0")
                        {
                            chkVASCULAR_ACCESS_BLOOD_INFECT0.IsChecked = true;
                        }
                        if (cureMainDataTable.Rows[0]["IN_BASKET_PLASTER_ALLERGY"].ToString() == "1")
                        {
                            chkIN_BASKET_PLASTER_ALLERGY1.IsChecked = true;
                        }
                        else if (cureMainDataTable.Rows[0]["IN_BASKET_PLASTER_ALLERGY"].ToString() == "0")
                        {
                            chkIN_BASKET_PLASTER_ALLERGY0.IsChecked = true;
                        }


                        #endregion

                        #region 内瘘评估
                        if (cureMainDataTable.Rows[0]["IN_BASKET_TREMOR"].ToString() == "0")
                        {
                            chkIN_BASKET_TREMOR0.IsChecked = true;
                        }
                        else if (cureMainDataTable.Rows[0]["IN_BASKET_TREMOR"].ToString() == "1")
                        {
                            chkIN_BASKET_TREMOR1.IsChecked = true;
                        }
                        else if (cureMainDataTable.Rows[0]["IN_BASKET_TREMOR"].ToString() == "2")
                        {
                            chkIN_BASKET_TREMOR2.IsChecked = true;
                        }

                        if (cureMainDataTable.Rows[0]["IN_BASKET_NOISE"].ToString() == "0")
                        {
                            chkIN_BASKET_NOISE0.IsChecked = true;
                        }
                        else if (cureMainDataTable.Rows[0]["IN_BASKET_NOISE"].ToString() == "1")
                        {
                            chkIN_BASKET_NOISE1.IsChecked = true;
                        }
                        else if (cureMainDataTable.Rows[0]["IN_BASKET_NOISE"].ToString() == "2")
                        {
                            chkIN_BASKET_NOISE2.IsChecked = true;
                        }

                        if (cureMainDataTable.Rows[0]["IN_BASKET_RED_HOT"].ToString() == "0")
                        {
                            chkIN_BASKET_RED_HOT0.IsChecked = true;
                        }
                        else if (cureMainDataTable.Rows[0]["IN_BASKET_RED_HOT"].ToString() == "1")
                        {
                            chkIN_BASKET_RED_HOT1.IsChecked = true;
                        }

                        if (cureMainDataTable.Rows[0]["IN_BASKET_ECCHYMOSIS"].ToString() == "0")
                        {
                            chkIN_BASKET_ECCHYMOSIS0.IsChecked = true;
                        }
                        else if (cureMainDataTable.Rows[0]["IN_BASKET_ECCHYMOSIS"].ToString() == "1")
                        {
                            chkIN_BASKET_ECCHYMOSIS1.IsChecked = true;
                        }

                        if (cureMainDataTable.Rows[0]["IN_BASKET_VASCULAR_ELASTICITY"].ToString() == "0")
                        {
                            chkIN_BASKET_VASCULAR_ELASTICITY0.IsChecked = true;
                        }
                        else if (cureMainDataTable.Rows[0]["IN_BASKET_VASCULAR_ELASTICITY"].ToString() == "1")
                        {
                            chkIN_BASKET_VASCULAR_ELASTICITY1.IsChecked = true;
                        }
                        #endregion


                        #region 无肝素透析


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
                            //if (cureMainDataTable.Rows[0]["BEFORE_DRY_WEIGHT"].ToString() != "0")
                            //{
                            txtBEFORE_DRY_WEIGHT.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["BEFORE_DRY_WEIGHT"].ToString());
                       
                            //}
                            //else
                            //{
                            //    txtBEFORE_DRY_WEIGHT.Text = cureMainDataTable.Rows[0]["BEFORE_DRY_WEIGHT_TAG"].ToString();
                            //}
                            lbWeightTag.Content = "kg 衣物轮椅重";


                        }


                        //干体重
                        if (cureMainDataTable.Rows[0]["DRY_WEIGHT"].ToString() != "0")
                        {
                            txtDRY_WEIGHT.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["DRY_WEIGHT"].ToString());

                        }
                        //衣物轮椅重
                        txtDRY_WEIGHT_TAG.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["DRY_WEIGHT_TAG"].ToString());



                        if (!string.IsNullOrEmpty(cureMainDataTable.Rows[0]["INFECTIOUS_CHECK_RESULT"].ToString()))
                        {
                            textBox3.Text = cureMainDataTable.Rows[0]["INFECTIOUS_CHECK_RESULT"].ToString();
                        }
                        else
                        {
                            textBox3.Text = checkResult;
                        }

                        #endregion

                        #region 神志，病情

                        txtSenses.Text = cureMainDataTable.Rows[0]["SENSESNAME"].ToString();//神志
                        switch (cureMainDataTable.Rows[0]["FOCUS_LEVEL"].ToString())//病情
                        {
                            case "787d0dec-6523-4b58-a93c-5d4e2a9d6369"://一般    清醒
                                focuseLevel.IsChecked = true;
                                break;
                            case "787d0dec-6523-4b58-a93c-5d4e2a9d6370"://病重   嗜睡
                                focuseLevel1.IsChecked = true;
                                break;
                            case "787d0dec-6523-4b58-a93c-5d4e2a9d6371"://病危   意识模糊
                                focuseLevel2.IsChecked = true;
                                break;

                        }
                        txtAllergic.Text = cureMainDataTable.Rows[0]["ALLERGIC"].ToString();//过敏史

                        txtT.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["BEFORE_TEMPERATURE"].ToString());
                        txtP.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["BEFORE_BP"].ToString());
                        txtR.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["BR"].ToString());
                        txtBP.Text = cureMainDataTable.Rows[0]["BEFORE_SYSTOLIC_PRESSURE"].ToString();//BEFORE_SYSTOLIC_PRESSURE
                        txtBP1.Text = cureMainDataTable.Rows[0]["BEFORE_DIASTOLIC_PRESSURE"].ToString(); ;//BEFORE_DIASTOLIC_PRESSURE

                     
                        txtSUBJECTIVE_COMFORT.Text = setZeroToEmpty(cureMainDataTable.Rows[0]["SUBJECTIVE_COMFORTNAME"].ToString());

                        #endregion
                        //电子签名，去掉
                        //HemoModel.MED_CURE_SIGNDataTable dtCureSign = objHemodialysisService.GetCureSignByHemoIdAndCureId(this.txtHEMODIALYSIS_ID.Text, cureMainDataTable.Rows[0]["CURE_ID"].ToString());
                        //LoadCureSign(dtCureSign);
                    }
                }

                #endregion

              

                setVascularDefaultValue(txtVASCULAR_ACCESS_ID.Text.Trim());

                #region 加载透析参数列表

                if (pDs.Tables["MED_HEMODIALYSIS_PARAMETERS"] != null && pDs.Tables["MED_HEMODIALYSIS_PARAMETERS"].Rows.Count > 0)
                {
                    var dtHemoParameters = pDs.Tables["MED_HEMODIALYSIS_PARAMETERS"] as HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable;
                    strCureID = dtHemoParameters.Rows[0]["CURE_ID"].ToString();
                    strRecipe_ID = dtHemoParameters.Rows[0]["RECIPE_ID"].ToString();
                    //if (dtHemoParameters.Rows.Count < paramRowNum)
                    //{
                    //    //addCount = 10 - dtHemoParameters.AsEnumerable().Where(i => i["EXTENDED_FIELD_3"].ToString() != "抢救").ToList().Count;
                    //    addCount = paramRowNum - dtHemoParameters.AsEnumerable().ToList().Count;
                    //}

                    loadParamsGrid(dtHemoParameters, paramRowNum, strCureID, strRecipe_ID);
                }
                else
                {
                    var dtHemoParameters = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();
                    loadParamsGrid(dtHemoParameters, paramRowNum, strCureID, strRecipe_ID);
                }

                #endregion

                #region 加载医生记录及医嘱

                if (pDs.Tables["MED_CURE_DRUG"] != null)
                {
                    DataTable dtMED_CURE_DRUG = pDs.Tables["MED_CURE_DRUG"];
                    strCureID = dtMED_CURE_DRUG.Rows[0]["CURE_ID"].ToString();
                    strRecipe_ID = dtMED_CURE_DRUG.Rows[0]["RECIPE_ID"].ToString();
                    if (dtMED_CURE_DRUG.Rows.Count < 4)
                    {
                        addCount = 4 - dtMED_CURE_DRUG.Rows.Count;
                    }

                }
                else
                {
                    DataTable dtMED_CURE_DRUG = new HemodialysisModel.MED_CURE_DRUGDataTable();
                }

                #endregion


            }
            else
            {
                var dtHemoParameters = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();
                loadParamsGrid(dtHemoParameters, paramRowNum, strCureID, strRecipe_ID);
                //var dtMED_CURE_DRUG = new HemodialysisModel.MED_CURE_DRUGDataTable();
                // loadGiveDrugGrid(dtMED_CURE_DRUG, 4, strCureID, strRecipe_ID);
            }
        }
        /// <summary>
        /// 设置导管评估的显示设置
        /// </summary>
        /// <param name="pName"></param>
        private void setVascularDefaultValue(string pName)
        {

            if (pName.Contains("导管") || pName.Contains("置管"))
            {
                paramRowNum = 14;
                this.rowdgpg.Height = new GridLength(30, GridUnitType.Pixel);
                this.rowdgpg1.Height = new GridLength(30, GridUnitType.Pixel);
                this.rownlpg.Height = new GridLength(0.0, GridUnitType.Pixel);
                this.rownlpg1.Height = new GridLength(0.00, GridUnitType.Pixel);
                this.txcs.Height = new GridLength(528, GridUnitType.Pixel);
                this.grid1.Height = 528; //519;
                this.grdParameters.Height = 528; //519;
            }
            else if (pName.Contains("内瘘"))
            {
                paramRowNum = 14;
                this.rowdgpg.Height = new GridLength(0.0, GridUnitType.Pixel);
                this.rowdgpg1.Height = new GridLength(0.0, GridUnitType.Pixel);
                this.rownlpg.Height = new GridLength(30, GridUnitType.Pixel);
                this.rownlpg1.Height = new GridLength(30, GridUnitType.Pixel);
                this.txcs.Height = new GridLength(528, GridUnitType.Pixel);
                this.grid1.Height = 528;//519
                this.grdParameters.Height = 528;//519
            }
            else
            {
                paramRowNum = 16;

                this.rowdgpg.Height = new GridLength(0.0, GridUnitType.Pixel);
                this.rowdgpg1.Height = new GridLength(0.0, GridUnitType.Pixel);
                this.rownlpg.Height = new GridLength(0.0, GridUnitType.Pixel);
                this.rownlpg1.Height = new GridLength(0.0, GridUnitType.Pixel);
                this.txcs.Height = new GridLength(579, GridUnitType.Pixel);
                this.grid1.Height = 579;
                this.grdParameters.Height = 579;

            }
        }
        /// <summary>
        /// 根据时间获取小时和分钟单位
        /// </summary>
        /// <param name="pHours"></param>
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

        /// <summary>
        /// 载入透析参数数据列表
        /// </summary>
        /// <param name="dtHemoParameters"></param>
        /// <param name="count"></param>
        /// <param name="pCureID"></param>
        /// <param name="pRecipeID"></param>
        private void loadParamsGrid(HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable dt, int count, string pCureID, string pRecipeID)
        {
            var dtHemoParametersTemp = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();

            //if (rowNum1 != 0)
            //{
            //    dtHemoParametersTemp = objHemodialysisService.GetHemoParametersByHemoParamRow(pCureID, rowNum1, rowNum2, "sqlByParams");
            //}
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
                    string cureResultTotal = string.Empty;
                    //遍历循环数据是不是超出了一行所能放的区域
                    foreach (string str in item.CLINICAL_MANIFESTATION.Split('\r'))
                    {
                        cureResultTotal = "";
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
            int kk = dtHemoParameters.Rows.Count;
            int fxInt = 0;
            foreach (HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSRow item in dtHemoParameters.Rows)
            {
                fxInt++;
                if (!item.IsCLINICAL_MANIFESTATIONNull() && item.CLINICAL_MANIFESTATION.Contains("返血0.9%NS 300ml"))
                {
                    kk = fxInt;
                    break;
                }
            }


            currentParamNoShowInt = count - dtHemoParameters.Rows.Count;

            if (currentParamNoShowInt > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    DataRow dr = dtHemoParameters.NewRow();
                    dr["HEMODIALYSIS_PARAMETERS_ID"] = System.Guid.NewGuid().ToString();
                    dr["CURE_ID"] = (i + 1).ToString();
                    dr["RECIPE_ID"] = (i + 1).ToString();
                    dtHemoParameters.Rows.Add(dr);
                }
            }

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

                    if (dtHemoParameters.Rows[j][z].ToString() == "0")
                    {
                        //dtHemoParameters.Rows[j][z] = DBNull.Value;
                        if (j + 1 < kk)
                        {
                            dtHemoParameters.Rows[j][z] = "/";
                        }
                        else
                        {
                            dtHemoParameters.Rows[j][z] = string.Empty;
                        }

                    }
                    else if (dtHemoParameters.Rows[j][z].ToString() == "/")
                    {
                        if (j + 1 < kk)
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

            //拼接药品遗嘱执行
            #region 拼接药品遗嘱执行这边不需要了
            /*
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
                            dtHemoParameters.Rows[d + 1]["CLINICAL_MANIFESTATION"] = dtDrugIn.Rows[d]["NEW_DRUG_NAME"].ToString() + dtDrugIn.Rows[d]["DOSAGE"].ToString() + dtDrugIn.Rows[d]["UNIT_NAME"].ToString() + dtDrugIn.Rows[d]["DRUG_MODE_NAME"].ToString();
                        }
                    }
                }

                dtDrug = Utility.GetSubTable(dtDrug, "DRUG_TIMETYPE='透析后' and status = '1'");
                int iDCount = dtDrug.Rows.Count;
                if (dtDrug != null && dtDrug.Rows.Count > 0)
                {
                    if (iDCount >= 1)
                    {
                        if (dtHemoParameters.Rows[8]["CLINICAL_MANIFESTATION"].ToString().Length == 0)
                        {
                            if (dtDrug.Rows[0] != null)
                            {
                                dtHemoParameters.Rows[8]["CLINICAL_MANIFESTATION"] = dtDrug.Rows[0]["NEW_DRUG_NAME"].ToString() + dtDrug.Rows[0]["DOSAGE"].ToString() + dtDrug.Rows[0]["UNIT_NAME"].ToString() + dtDrug.Rows[0]["DRUG_MODE_NAME"].ToString();
                            }
                        }
                    }
                    if (iDCount >= 2)
                    {
                        if (dtHemoParameters.Rows[7]["CLINICAL_MANIFESTATION"].ToString().Length == 0)
                        {
                            if (dtDrug.Rows[1] != null)
                            {
                                dtHemoParameters.Rows[7]["CLINICAL_MANIFESTATION"] = dtDrug.Rows[1]["NEW_DRUG_NAME"].ToString() + dtDrug.Rows[1]["DOSAGE"].ToString() + dtDrug.Rows[1]["UNIT_NAME"].ToString() + dtDrug.Rows[1]["DRUG_MODE_NAME"].ToString();
                            }
                        }
                    }
                    if (iDCount >= 3)
                    {
                        if (dtHemoParameters.Rows[6]["CLINICAL_MANIFESTATION"].ToString().Length == 0)
                        {
                            if (dtDrug.Rows[2] != null)
                            {
                                dtHemoParameters.Rows[6]["CLINICAL_MANIFESTATION"] = dtDrug.Rows[2]["NEW_DRUG_NAME"].ToString() + dtDrug.Rows[2]["DOSAGE"].ToString() + dtDrug.Rows[2]["UNIT_NAME"].ToString() + dtDrug.Rows[2]["DRUG_MODE_NAME"].ToString();
                            }
                        }
                    }
                }
            }
            */
            #endregion
            grdParameters.ItemsSource = dtHemoParameters.DefaultView;
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
                        //LoadImage(this.imgPRIMARY_DOCTOR, dtCureSign[0].PRIMARY_DOCTOR_SIGN);
                    }
                }

                if (dtCureSign.Rows[0]["PRIMARY_NURSE_SIGN"] != DBNull.Value)
                {
                    if (dtCureSign[0].PRIMARY_NURSE_SIGN.Length > 0)
                    {
                        //LoadImage(this.imgPRIMARY_NURSE, dtCureSign[0].PRIMARY_NURSE_SIGN);
                    }
                }

                if (dtCureSign.Rows[0]["CHECK_NURSE_SIGN"] != DBNull.Value)
                {
                    if (dtCureSign[0].CHECK_NURSE_SIGN.Length > 0)
                    {
                        //LoadImage(this.imgCHECK_NURSE, dtCureSign[0].CHECK_NURSE_SIGN);
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

    }

}
