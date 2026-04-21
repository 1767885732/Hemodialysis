/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司股份有限公司
// 文件名：CtlMedicalDocument.cs
// 文件功能描述：血 液 净 化 治 疗 记 录 单
// 创建标识：刘超 2013-07-22
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
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.Model;
using System.Windows.Markup;
using System.IO;

namespace Hemo.Client.Controls
{
    /// <summary>
    /// CtlMedicalDocument3.xaml 的交互逻辑
    /// </summary>
    public partial class CtlMedicalDocument3 : WPF_DocumentBase
    {
        #region 类变量

        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;
        private DataSet ds = new DataSet();
        private HemodialysisModel.MED_CURE_MAIN_CRRTRow rowCRRT = null;
        int rowNum1;
        int rowNum2;
        int pageType;
        int pageNum;
        string sqlParam;
        string areaName;

        #endregion

        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_CureMainData"></param>
        /// <param name="_rowNum1"></param>
        /// <param name="_rowNum2"></param>
        /// <param name="_sqlParam"></param>
        /// <param name="_pageNum"></param>
        /// <param name="areaName"></param>
        public CtlMedicalDocument3(DataSet _CureMainData, int _rowNum1, int _rowNum2, string _sqlParam, int _pageNum, string areaName)
        {
            InitializeComponent();
            this.label1.Content = Utility.GetHospitalName();
            rowNum1 = _rowNum1;
            sqlParam = _sqlParam;
            pageNum = _pageNum;
            rowNum2 = _rowNum2;
            ds = _CureMainData;
            this.areaName = areaName;
            loadData(_CureMainData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_CureMainData"></param>
        /// <param name="rowCRRT"></param>
        /// <param name="_rowNum1"></param>
        /// <param name="_rowNum2"></param>
        /// <param name="pageType"></param>
        /// <param name="_pageNum"></param>
        /// <param name="areaName"></param>
        public CtlMedicalDocument3(DataSet _CureMainData, HemodialysisModel.MED_CURE_MAIN_CRRTRow rowCRRT, int _rowNum1, int _rowNum2, int pageType, int _pageNum, string areaName)
        {
            InitializeComponent();
            rowNum1 = _rowNum1;
            pageNum = _pageNum;
            rowNum2 = _rowNum2;
            ds = _CureMainData;
            this.areaName = areaName;
            this.rowCRRT = rowCRRT;
            this.pageType = pageType;
            loadData(_CureMainData);
        }

        #endregion

        /// <summary>
        /// 根据治疗单数据集赋值
        /// </summary>
        /// <param name="pDs">治疗单数据集</param>
        private void loadData(DataSet pDs)
        {
            string strCureID = string.Empty;
            string strRecipe_ID = string.Empty;
            int addCount = 0;

            if (pDs != null && pDs.Tables.Count > 0)
            {
                #region 加载患者信息

                if (pDs.Tables["MED_PATIENTS"] != null)
                {
                    DataTable patientDataTable = pDs.Tables["MED_PATIENTS"];
                    if (patientDataTable != null && patientDataTable.Rows.Count > 0)
                    {
                        txtNAME.Text = patientDataTable.Rows[0]["NAME"].ToString();
                        txtSEX.Text = patientDataTable.Rows[0]["SEX"].ToString();
                        txtAGE.Text = patientDataTable.Rows[0]["AGE"].ToString();
                        txtPATIENT_ID.Text = patientDataTable.Rows[0]["ADMISSION_NUMBER"].ToString();
                        txtHEMODIALYSIS_ID.Text = patientDataTable.Rows[0]["HEMODIALYSIS_ID"].ToString();
                        this.txtID.Text = patientDataTable.Rows[0]["PATIENT_ID"].ToString();
                        txtWHAT_DEPARTMENT_IN.Text = patientDataTable.Rows[0]["WHAT_DEPARTMENT_IN"].ToString();
                        //计算透析次数
                        DataTable dtCureCount = objHemodialysisService.GetCureCountByHemoID(patientDataTable.Rows[0]["HEMODIALYSIS_ID"].ToString());
                        if (dtCureCount != null && dtCureCount.Rows.Count > 0)
                        {
                            txtCLEAN_UP_TIMES.Text = dtCureCount.Rows[0][0].ToString();
                        }
                    }
                }

                #endregion

                #region 加载治疗单信息

                if (pDs.Tables["MED_CURE_MAIN"] != null)
                {
                    DataTable cureMainDataTable = pDs.Tables["MED_CURE_MAIN"];
                    string strSummary2 = string.Empty;
                    if (cureMainDataTable != null && cureMainDataTable.Rows.Count > 0)
                    {
                        string year = Utility.CDate(cureMainDataTable.Rows[0]["CURE_CREATE_DATE"].ToString()).Year.ToString();
                        string month = Utility.CDate(cureMainDataTable.Rows[0]["CURE_CREATE_DATE"].ToString()).Month.ToString();
                        string day = Utility.CDate(cureMainDataTable.Rows[0]["CURE_CREATE_DATE"].ToString()).Day.ToString();
                        if (areaName.Equals("CRRT"))
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
                        txtAnticoagulantUnit.Text = cureMainDataTable.Rows[0]["second_drug_unit_name"].ToString() == string.Empty ? "mg/h" : cureMainDataTable.Rows[0]["second_drug_unit_name"].ToString() + "/h";

                        txtCLEAN_UP_TIMES.Text = cureMainDataTable.Rows[0]["CLEAN_UP_TIMES"].ToString();
                        string[] records = areaName.Equals("CRRT") ? (rowCRRT != null ? rowCRRT.SUMMARY2.Split("|".ToCharArray()) : null) : cureMainDataTable.Rows[0]["SUMMARY2"].ToString().Split("|".ToCharArray());
                        if (pageNum == 2)
                        {
                            string strSummary = areaName.Equals("CRRT") ? (rowCRRT != null ? rowCRRT.SUMMARY3 : string.Empty) : cureMainDataTable.Rows[0]["SUMMARY3"].ToString();
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

                        //txtSUMMARY.Text = strSummary2;
                        txtPRIMARY_DOCTOR.Text = areaName.Equals("CRRT") ? (rowCRRT != null ? rowCRRT["PRIMARY_DOCTOR_NAME"].ToString() : string.Empty) : cureMainDataTable.Rows[0]["DOCTOR_NAME"].ToString();
                        txtPRIMARY_NURSE.Text = areaName.Equals("CRRT") ? (rowCRRT != null ? rowCRRT["PRIMARY_NURSE_NAME"].ToString() : string.Empty) : cureMainDataTable.Rows[0]["NURSE_NAME"].ToString();
                        txtCHECK_NURSE.Text = areaName.Equals("CRRT") ? (rowCRRT != null ? rowCRRT["CHECK_NURSE_NAME"].ToString() : string.Empty) : cureMainDataTable.Rows[0]["check_nurse_name"].ToString();

                        HemoModel.MED_CURE_SIGNDataTable dtCureSign = objHemodialysisService.GetCureSignByHemoIdAndCureId(this.txtHEMODIALYSIS_ID.Text, cureMainDataTable.Rows[0]["CURE_ID"].ToString());
                        LoadCureSign(dtCureSign);
                    }
                }

                #endregion

                #region 加载透析参数

                if (pDs.Tables["MED_HEMODIALYSIS_PARAMETERS"] != null && pDs.Tables["MED_HEMODIALYSIS_PARAMETERS"].Rows.Count > 0)
                {
                    DataTable dtHemoParameters = pDs.Tables["MED_HEMODIALYSIS_PARAMETERS"];
                    strCureID = dtHemoParameters.Rows[0]["CURE_ID"].ToString();
                    strRecipe_ID = dtHemoParameters.Rows[0]["RECIPE_ID"].ToString();

                    if (areaName.Equals("CRRT"))
                    {
                        var dtParam = dtHemoParameters.Clone();
                        if (rowCRRT != null)
                        {
                            dtHemoParameters.AsEnumerable().Where(row => Utility.CDate(row["CREATE_DATE"].ToString()).Date.CompareTo(rowCRRT.CREATE_DATE.Date) == 0 && row["CRRT_CLASS"].Equals(rowCRRT.CRRT_CLASS)).CopyToDataTable(dtParam, LoadOption.OverwriteChanges);
                        }
                        dtHemoParameters = dtParam.Clone();

                        if (pageType == 0)
                        {
                            if (dtParam != null && dtParam.Rows.Count > 0)
                            {
                                dtParam.AsEnumerable().OrderByDescending(row => row.Field<DateTime?>("CREATE_DATE")).Take(rowNum1).OrderByDescending(row => row.Field<DateTime?>("CREATE_DATE")).Take(rowNum2).OrderBy(row => row.Field<DateTime?>("CREATE_DATE")).CopyToDataTable(dtHemoParameters, LoadOption.OverwriteChanges);
                            }
                        }
                        else
                        {
                            if (dtParam != null && dtParam.Rows.Count > 0)
                            {
                                dtParam.AsEnumerable().OrderByDescending(row => row.Field<DateTime?>("CREATE_DATE")).Take(rowNum1).OrderBy(row => row.Field<DateTime?>("CREATE_DATE")).Take(rowNum2).OrderBy(row => row.Field<DateTime?>("CREATE_DATE")).CopyToDataTable(dtHemoParameters, LoadOption.OverwriteChanges);
                            }
                        }
                    }
                    else
                    {
                        if (sqlParam == "sqlByParams")
                        {
                            dtHemoParameters = objHemodialysisService.GetHemoParametersByHemoParamRow(strCureID, rowNum1, rowNum2, "sqlByParams");
                        }
                        else
                        {
                            dtHemoParameters = objHemodialysisService.GetHemoParametersByHemoParamRow(strCureID, rowNum1, rowNum2, "");
                        }
                    }

                    if (dtHemoParameters.Rows.Count < 10)
                    {
                        //addCount = 20 - dtHemoParameters.AsEnumerable().Where(i => i["EXTENDED_FIELD_3"].ToString() == "抢救").ToList().Count;
                        addCount = 24 - dtHemoParameters.AsEnumerable().ToList().Count;
                    }

                    loadParamsGrid(dtHemoParameters, addCount, strCureID, strRecipe_ID);
                }

                #endregion
            }
        }

        /// <summary>
        /// 载入透析参数数据列表
        /// </summary>
        /// <param name="dtHemoParameters"></param>
        /// <param name="count"></param>
        /// <param name="pCureID"></param>
        /// <param name="pRecipeID"></param>
        private void loadParamsGrid(DataTable dtHemoParameters, int count, string pCureID, string pRecipeID)
        {
            //var dtHemoParameters = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();
            //dt.AsEnumerable().Where(i => i["EXTENDED_FIELD_3"].ToString() != "抢救").CopyToDataTable(dtHemoParameters, LoadOption.OverwriteChanges);            
            for (int i = 0; i < count; i++)
            {
                DataRow dr = dtHemoParameters.NewRow();
                dr["HEMODIALYSIS_PARAMETERS_ID"] = System.Guid.NewGuid().ToString();
                dr["CURE_ID"] = (i + 1).ToString();
                dr["RECIPE_ID"] = (i + 1).ToString();
                dtHemoParameters.Rows.Add(dr);
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
                        dtHemoParameters.Rows[j][z] = DBNull.Value;
                    }
                }
            }
            //过滤第一行数据，拼接最后一行数据
            //if (dtHemoParameters != null && dtHemoParameters.Rows.Count >= 20)
            //{
            //    dtHemoParameters.Rows[19]["CLINICAL_MANIFESTATION"] = "返血0.9% NS 200ml";
            //}

            //拼接药品遗嘱执行
            if (ds.Tables["MED_CURE_DRUG"] != null)
            {
                DataTable dtDrug = ds.Tables["MED_CURE_DRUG"];
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

            QJgrdParameters.ItemsSource = dtHemoParameters.DefaultView;
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
        public override void SetImageAndTextBoxVisible(Visibility imgVisible, Visibility txtVisible)
        {
            this.imgPRIMARY_DOCTOR.Visibility = this.imgPRIMARY_NURSE.Visibility = this.imgCHECK_NURSE.Visibility = imgVisible;
            this.txtPRIMARY_DOCTOR.Visibility = this.txtPRIMARY_NURSE.Visibility = this.txtCHECK_NURSE.Visibility = txtVisible;
        }
    }
}
