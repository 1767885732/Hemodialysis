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
using Hemo.Model;
using Hemo.Utilities;
using Hemo.IService.Config;
using Hemo.Service;

namespace Hemo.Client.Controls
{
    /// <summary>
    /// CtlMedicalDocument2.xaml 的交互逻辑
    /// 血液净化治疗记录单
    /// </summary>
    public partial class CtlMedicalDocument2 : UserControl
    {
        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;

        public CtlMedicalDocument2(DataSet _CureMainData)
        {
            InitializeComponent();
            this.label1.Content = Utility.GetHospitalName();
            loadData(_CureMainData);
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

            if (pDs != null && pDs.Tables.Count > 0)
            {
                //病人信息表
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

                //治疗单主表,缺超重和总用量，是否需要计算？
                if (pDs.Tables["MED_CURE_MAIN"] != null)
                {
                    DataTable cureMainDataTable = pDs.Tables["MED_CURE_MAIN"];
                    string strSummary1 = string.Empty;
                    string strSummary2 = string.Empty;
                    if (cureMainDataTable != null && cureMainDataTable.Rows.Count > 0)
                    {
                        txtCureYear.Text = Utility.CDate(cureMainDataTable.Rows[0]["CURE_CREATE_DATE"].ToString()).Year.ToString();
                        txtCureMouth.Text = Utility.CDate(cureMainDataTable.Rows[0]["CURE_CREATE_DATE"].ToString()).Month.ToString();
                        txtCureDay.Text = Utility.CDate(cureMainDataTable.Rows[0]["CURE_CREATE_DATE"].ToString()).Day.ToString();

                        txtCLEAN_UP_TIMES.Text = cureMainDataTable.Rows[0]["CLEAN_UP_TIMES"].ToString();
                        strSummary1 = cureMainDataTable.Rows[0]["SUMMARY"].ToString();
                        strSummary2 = cureMainDataTable.Rows[0]["SUMMARY2"].ToString() + " " + cureMainDataTable.Rows[0]["SUMMARY3"].ToString();
                        strSummary2 = strSummary2.Replace("|||", "");
                        if (strSummary2.Trim().Length == 0)
                        {
                            if (strSummary1.Length > 164)
                            {
                                txtSUMMARY.Text = strSummary1.Substring(164, strSummary1.Length - 164);
                            }
                        }
                        else
                        {
                            //病情记录
                            if (strSummary1.Length > 164)
                            {
                                txtSUMMARY.Text = strSummary1.Substring(164, strSummary1.Length - 164) + "\r\n 抢救记录:" + cureMainDataTable.Rows[0]["SUMMARY2"].ToString();
                            }
                            else
                            {
                                txtSUMMARY.Text = " 抢救记录:" + strSummary2.Replace("|", "   ");
                            }

                            txtPRIMARY_DOCTOR.Text = cureMainDataTable.Rows[0]["DOCTOR_NAME"].ToString();
                            txtPRIMARY_NURSE.Text = cureMainDataTable.Rows[0]["NURSE_NAME"].ToString();
                            txtCHECK_NURSE.Text = cureMainDataTable.Rows[0]["check_nurse_name"].ToString();
                        }
                    }
                }


            }
        }

    }
}
