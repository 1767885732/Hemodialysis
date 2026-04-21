/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: 患者知情同意书
 * 创建标识:贺建操-2016年5月11日
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
using Hemo.Model;
using Hemo.Utilities;
using System.Data;

namespace Hemo.Client.Doc {
    /// <summary>
    /// 健康宣教指导表.xaml 的交互逻辑
    /// </summary>
    public partial class 健康宣教指导表 : UserControl {
        public 健康宣教指导表() {
            InitializeComponent();
            lblHospital.Content = Utilities.Utility.GetHospitalName();
            grid1.SetValue(GridHelper.ShowBorderProperty, true);//显示边框
            grid1.SetValue(GridHelper.GridLineBrushProperty, Brushes.Black);//黑色边框
        }

        public PatientModel.MED_PATIENTSRow PatientRow {
            set;
            get;
        }

        public DataTable HealthDataTable {
            set;
            get;
        }

        /// <summary>
        /// 加载 文档
        /// </summary>
        public void LoadDocumentInfo() {
            if (this.PatientRow != null) {
                this.txtNAME.Text = this.PatientRow.IsNAMENull() ? string.Empty : this.PatientRow.NAME;
                this.txtSEX.Text = this.PatientRow.IsSEXNull() ? string.Empty : this.PatientRow.SEX;
                this.txtAGE.Text = this.PatientRow.IsAGENull() ? string.Empty : Utility.CInt(this.PatientRow.AGE.ToString()).ToString();
                this.txtPATIENT_ID.Text = this.PatientRow.IsPATIENT_IDNull() ? string.Empty : this.PatientRow.PATIENT_ID;
                this.txtWHAT_DEPARTMENT_IN.Text = this.PatientRow.IsDIAGNOSENull() ? string.Empty : this.PatientRow.DIAGNOSE;
            }

            if (HealthDataTable != null && HealthDataTable.Rows.Count > 0) {
                txtHeadNurse.Text = HealthDataTable.Rows[0]["health_headnurse_id"].ToString();
                #region 入室宣教

                #region 入室宣教第一行
                chkHEALTH_WRITTEN_1.Text = HealthDataTable.Rows[0]["HEALTH_WRITTEN"].ToString();
                cmbHEALTH_APPRAISE_1.Text = HealthDataTable.Rows[0]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_1.Text = HealthDataTable.Rows[0]["HEALTH_HEADMAN_APPRAISE"].ToString();
                chkHEALTH_VERBAL_1.Text = HealthDataTable.Rows[0]["HEALTH_VERBAL"].ToString();
                if (HealthDataTable.Rows[0]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_1.Text = HealthDataTable.Rows[0]["HEALTH_NURSE_ID"].ToString();
                }
                if (HealthDataTable.Rows[0]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_1.Text = Utility.CDate(HealthDataTable.Rows[0]["HEALTH_NURSE_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[0]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_1.Text = Utility.CDate(HealthDataTable.Rows[0]["HEALTH_HEADMAN_DATE"].ToString()).ToShortDateString();
                }

                if (HealthDataTable.Rows[0]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_1.Text = HealthDataTable.Rows[0]["HEALTH_HEADMAN_ID"].ToString();
                }

                #endregion

                #region 入室宣教第二行
                chkHEALTH_VERBAL_2.Text = HealthDataTable.Rows[1]["HEALTH_VERBAL"].ToString();
                chkHEALTH_WRITTEN_2.Text = HealthDataTable.Rows[1]["HEALTH_WRITTEN"].ToString();
                cmbHEALTH_APPRAISE_2.Text = HealthDataTable.Rows[1]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_2.Text = HealthDataTable.Rows[1]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (HealthDataTable.Rows[0]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_2.Text = HealthDataTable.Rows[0]["HEALTH_NURSE_ID"].ToString();
                }
                if (HealthDataTable.Rows[0]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_2.Text = Utility.CDate(HealthDataTable.Rows[0]["HEALTH_NURSE_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[0]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_2.Text = Utility.CDate(HealthDataTable.Rows[0]["HEALTH_HEADMAN_DATE"].ToString()).ToShortDateString();
                }

                if (HealthDataTable.Rows[0]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_2.Text = HealthDataTable.Rows[0]["HEALTH_HEADMAN_ID"].ToString();
                }
                #endregion

                #region 入室宣教第三行
                chkHEALTH_VERBAL_3.Text = HealthDataTable.Rows[2]["HEALTH_VERBAL"].ToString();
                chkHEALTH_WRITTEN_3.Text = HealthDataTable.Rows[2]["HEALTH_WRITTEN"].ToString();
                cmbHEALTH_APPRAISE_3.Text = HealthDataTable.Rows[2]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_3.Text = HealthDataTable.Rows[2]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (HealthDataTable.Rows[0]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_3.Text = HealthDataTable.Rows[0]["HEALTH_NURSE_ID"].ToString();
                }
                if (HealthDataTable.Rows[0]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_3.Text = Utility.CDate(HealthDataTable.Rows[0]["HEALTH_NURSE_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[0]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_3.Text = Utility.CDate(HealthDataTable.Rows[0]["HEALTH_HEADMAN_DATE"].ToString()).ToShortDateString();
                }

                if (HealthDataTable.Rows[0]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_3.Text = HealthDataTable.Rows[0]["HEALTH_HEADMAN_ID"].ToString();
                }
                #endregion

                #region 入室宣教第四行
                chkHEALTH_VERBAL_4.Text = HealthDataTable.Rows[3]["HEALTH_VERBAL"].ToString();
                chkHEALTH_WRITTEN_4.Text = HealthDataTable.Rows[3]["HEALTH_WRITTEN"].ToString();
                cmbHEALTH_APPRAISE_4.Text = HealthDataTable.Rows[3]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_4.Text = HealthDataTable.Rows[3]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (HealthDataTable.Rows[0]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_4.Text = HealthDataTable.Rows[0]["HEALTH_NURSE_ID"].ToString();
                }
                if (HealthDataTable.Rows[0]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_4.Text = Utility.CDate(HealthDataTable.Rows[0]["HEALTH_NURSE_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[0]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_4.Text = Utility.CDate(HealthDataTable.Rows[0]["HEALTH_HEADMAN_DATE"].ToString()).ToShortDateString();
                }

                if (HealthDataTable.Rows[0]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_4.Text = HealthDataTable.Rows[0]["HEALTH_HEADMAN_ID"].ToString();
                }
                #endregion

                #endregion

                #region 疾病知识

                #region 疾病知识第一行
                chkHEALTH_VERBAL_5.Text = HealthDataTable.Rows[4]["HEALTH_VERBAL"].ToString();
                chkHEALTH_WRITTEN_5.Text = HealthDataTable.Rows[4]["HEALTH_WRITTEN"].ToString();
                cmbHEALTH_APPRAISE_5.Text = HealthDataTable.Rows[4]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_5.Text = HealthDataTable.Rows[4]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (HealthDataTable.Rows[4]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_5.Text = HealthDataTable.Rows[4]["HEALTH_NURSE_ID"].ToString();
                }
                if (HealthDataTable.Rows[4]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_5.Text = Utility.CDate(HealthDataTable.Rows[4]["HEALTH_NURSE_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[4]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_5.Text = Utility.CDate(HealthDataTable.Rows[4]["HEALTH_HEADMAN_DATE"].ToString()).ToShortDateString();
                }

                if (HealthDataTable.Rows[4]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_5.Text = HealthDataTable.Rows[4]["HEALTH_HEADMAN_ID"].ToString();
                }
                #endregion

                #region 疾病知识第二行
                chkHEALTH_VERBAL_6.Text = HealthDataTable.Rows[5]["HEALTH_VERBAL"].ToString();
                chkHEALTH_WRITTEN_6.Text = HealthDataTable.Rows[5]["HEALTH_WRITTEN"].ToString();
                cmbHEALTH_APPRAISE_6.Text = HealthDataTable.Rows[5]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_6.Text = HealthDataTable.Rows[5]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (HealthDataTable.Rows[4]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_6.Text = HealthDataTable.Rows[4]["HEALTH_NURSE_ID"].ToString();
                }
                if (HealthDataTable.Rows[4]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_6.Text = Utility.CDate(HealthDataTable.Rows[4]["HEALTH_NURSE_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[4]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_6.Text = Utility.CDate(HealthDataTable.Rows[4]["HEALTH_HEADMAN_DATE"].ToString()).ToShortDateString();
                }

                if (HealthDataTable.Rows[4]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_6.Text = HealthDataTable.Rows[4]["HEALTH_HEADMAN_ID"].ToString();
                }
                #endregion

                #region 疾病知识第三行
                chkHEALTH_VERBAL_7.Text = HealthDataTable.Rows[6]["HEALTH_VERBAL"].ToString();
                chkHEALTH_WRITTEN_7.Text = HealthDataTable.Rows[6]["HEALTH_WRITTEN"].ToString();
                cmbHEALTH_APPRAISE_7.Text = HealthDataTable.Rows[6]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_7.Text = HealthDataTable.Rows[6]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (HealthDataTable.Rows[4]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_7.Text = HealthDataTable.Rows[4]["HEALTH_NURSE_ID"].ToString();
                }
                if (HealthDataTable.Rows[4]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_7.Text = Utility.CDate(HealthDataTable.Rows[4]["HEALTH_NURSE_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[4]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_7.Text = Utility.CDate(HealthDataTable.Rows[4]["HEALTH_HEADMAN_DATE"].ToString()).ToShortDateString();
                }

                if (HealthDataTable.Rows[4]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_7.Text = HealthDataTable.Rows[4]["HEALTH_HEADMAN_ID"].ToString();
                }
                #endregion

                #region 疾病知识第四行
                chkHEALTH_VERBAL_8.Text = HealthDataTable.Rows[7]["HEALTH_VERBAL"].ToString();
                chkHEALTH_WRITTEN_8.Text = HealthDataTable.Rows[7]["HEALTH_WRITTEN"].ToString();
                cmbHEALTH_APPRAISE_8.Text = HealthDataTable.Rows[7]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_8.Text = HealthDataTable.Rows[7]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (HealthDataTable.Rows[4]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_8.Text = HealthDataTable.Rows[4]["HEALTH_NURSE_ID"].ToString();
                }
                if (HealthDataTable.Rows[4]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_8.Text = Utility.CDate(HealthDataTable.Rows[4]["HEALTH_NURSE_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[4]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_8.Text = Utility.CDate(HealthDataTable.Rows[4]["HEALTH_HEADMAN_DATE"].ToString()).ToShortDateString();
                }

                if (HealthDataTable.Rows[4]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_8.Text = HealthDataTable.Rows[4]["HEALTH_HEADMAN_ID"].ToString();
                }
                #endregion

                #region 疾病知识第五行
                chkHEALTH_VERBAL_9.Text = HealthDataTable.Rows[8]["HEALTH_VERBAL"].ToString();
                chkHEALTH_WRITTEN_9.Text = HealthDataTable.Rows[8]["HEALTH_WRITTEN"].ToString();
                cmbHEALTH_APPRAISE_9.Text = HealthDataTable.Rows[8]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_9.Text = HealthDataTable.Rows[8]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (HealthDataTable.Rows[4]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_9.Text = HealthDataTable.Rows[4]["HEALTH_NURSE_ID"].ToString();
                }
                if (HealthDataTable.Rows[4]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_9.Text = Utility.CDate(HealthDataTable.Rows[4]["HEALTH_NURSE_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[4]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_9.Text = Utility.CDate(HealthDataTable.Rows[4]["HEALTH_HEADMAN_DATE"].ToString()).ToShortDateString();
                }

                if (HealthDataTable.Rows[4]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_9.Text = HealthDataTable.Rows[4]["HEALTH_HEADMAN_ID"].ToString();
                }
                #endregion

                #endregion

                #region 心里护理

                #region 心里护理第一行
                chkHEALTH_VERBAL_10.Text = HealthDataTable.Rows[9]["HEALTH_VERBAL"].ToString();
                chkHEALTH_WRITTEN_10.Text = HealthDataTable.Rows[9]["HEALTH_WRITTEN"].ToString();
                cmbHEALTH_APPRAISE_10.Text = HealthDataTable.Rows[9]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_10.Text = HealthDataTable.Rows[9]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (HealthDataTable.Rows[9]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_10.Text = HealthDataTable.Rows[9]["HEALTH_NURSE_ID"].ToString();
                }
                if (HealthDataTable.Rows[9]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_10.Text = Utility.CDate(HealthDataTable.Rows[9]["HEALTH_NURSE_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[9]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_10.Text = Utility.CDate(HealthDataTable.Rows[9]["HEALTH_HEADMAN_DATE"].ToString()).ToShortDateString();
                }

                if (HealthDataTable.Rows[9]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_10.Text = HealthDataTable.Rows[9]["HEALTH_HEADMAN_ID"].ToString();
                }
                #endregion


                #region 心里护理第二行
                chkHEALTH_VERBAL_11.Text = HealthDataTable.Rows[10]["HEALTH_VERBAL"].ToString();
                chkHEALTH_WRITTEN_11.Text = HealthDataTable.Rows[10]["HEALTH_WRITTEN"].ToString();
                cmbHEALTH_APPRAISE_11.Text = HealthDataTable.Rows[10]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_11.Text = HealthDataTable.Rows[10]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (HealthDataTable.Rows[9]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_11.Text = HealthDataTable.Rows[9]["HEALTH_NURSE_ID"].ToString();
                }
                if (HealthDataTable.Rows[9]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_11.Text = Utility.CDate(HealthDataTable.Rows[9]["HEALTH_NURSE_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[9]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_11.Text = Utility.CDate(HealthDataTable.Rows[9]["HEALTH_HEADMAN_DATE"].ToString()).ToShortDateString();
                }

                if (HealthDataTable.Rows[9]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_11.Text = HealthDataTable.Rows[9]["HEALTH_HEADMAN_ID"].ToString();
                }
                #endregion

                #endregion

                #region 血管通路护理

                #region 血管通路护理第一行
                chkHEALTH_VERBAL_12.Text = HealthDataTable.Rows[11]["HEALTH_VERBAL"].ToString();
                chkHEALTH_WRITTEN_12.Text = HealthDataTable.Rows[11]["HEALTH_WRITTEN"].ToString();
                cmbHEALTH_APPRAISE_12.Text = HealthDataTable.Rows[11]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_12.Text = HealthDataTable.Rows[11]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (HealthDataTable.Rows[11]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_12.Text = HealthDataTable.Rows[11]["HEALTH_NURSE_ID"].ToString();
                }
                if (HealthDataTable.Rows[11]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_12.Text = Utility.CDate(HealthDataTable.Rows[11]["HEALTH_NURSE_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[11]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_12.Text = Utility.CDate(HealthDataTable.Rows[11]["HEALTH_HEADMAN_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[11]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_12.Text = HealthDataTable.Rows[11]["HEALTH_HEADMAN_ID"].ToString();
                }
                #endregion

                #region 血管通路护理第二行
                chkHEALTH_VERBAL_13.Text = HealthDataTable.Rows[12]["HEALTH_VERBAL"].ToString();
                chkHEALTH_WRITTEN_13.Text = HealthDataTable.Rows[12]["HEALTH_WRITTEN"].ToString();
                cmbHEALTH_APPRAISE_13.Text = HealthDataTable.Rows[12]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_13.Text = HealthDataTable.Rows[12]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (HealthDataTable.Rows[11]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_13.Text = HealthDataTable.Rows[11]["HEALTH_NURSE_ID"].ToString();
                }
                if (HealthDataTable.Rows[11]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_13.Text = Utility.CDate(HealthDataTable.Rows[11]["HEALTH_NURSE_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[11]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_13.Text = Utility.CDate(HealthDataTable.Rows[11]["HEALTH_HEADMAN_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[11]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_13.Text = HealthDataTable.Rows[11]["HEALTH_HEADMAN_ID"].ToString();
                }
                #endregion

                #region 血管通路护理第三行
                chkHEALTH_VERBAL_14.Text = HealthDataTable.Rows[13]["HEALTH_VERBAL"].ToString();
                chkHEALTH_WRITTEN_14.Text = HealthDataTable.Rows[13]["HEALTH_WRITTEN"].ToString();
                cmbHEALTH_APPRAISE_14.Text = HealthDataTable.Rows[13]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_14.Text = HealthDataTable.Rows[13]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (HealthDataTable.Rows[11]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_14.Text = HealthDataTable.Rows[11]["HEALTH_NURSE_ID"].ToString();
                }
                if (HealthDataTable.Rows[11]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_14.Text = Utility.CDate(HealthDataTable.Rows[11]["HEALTH_NURSE_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[11]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_14.Text = Utility.CDate(HealthDataTable.Rows[11]["HEALTH_HEADMAN_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[11]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_14.Text = HealthDataTable.Rows[11]["HEALTH_HEADMAN_ID"].ToString();
                }
                #endregion

                #endregion

                #region 规律血管指导

                #region 规律血管指导第一行
                chkHEALTH_VERBAL_15.Text = HealthDataTable.Rows[14]["HEALTH_VERBAL"].ToString();
                chkHEALTH_WRITTEN_15.Text = HealthDataTable.Rows[14]["HEALTH_WRITTEN"].ToString();
                cmbHEALTH_APPRAISE_15.Text = HealthDataTable.Rows[14]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_15.Text = HealthDataTable.Rows[14]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (HealthDataTable.Rows[14]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_15.Text = HealthDataTable.Rows[14]["HEALTH_NURSE_ID"].ToString();
                }
                if (HealthDataTable.Rows[14]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_15.Text = Utility.CDate(HealthDataTable.Rows[14]["HEALTH_NURSE_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[14]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_15.Text = Utility.CDate(HealthDataTable.Rows[14]["HEALTH_HEADMAN_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[14]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_15.Text = HealthDataTable.Rows[14]["HEALTH_HEADMAN_ID"].ToString();
                }
                #endregion

                #region 规律血管指导第二行
                chkHEALTH_VERBAL_16.Text = HealthDataTable.Rows[15]["HEALTH_VERBAL"].ToString();
                chkHEALTH_WRITTEN_16.Text = HealthDataTable.Rows[15]["HEALTH_WRITTEN"].ToString();
                cmbHEALTH_APPRAISE_16.Text = HealthDataTable.Rows[15]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_16.Text = HealthDataTable.Rows[15]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (HealthDataTable.Rows[14]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_16.Text = HealthDataTable.Rows[14]["HEALTH_NURSE_ID"].ToString();
                }
                if (HealthDataTable.Rows[14]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_16.Text = Utility.CDate(HealthDataTable.Rows[14]["HEALTH_NURSE_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[14]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_16.Text = Utility.CDate(HealthDataTable.Rows[14]["HEALTH_HEADMAN_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[14]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_16.Text = HealthDataTable.Rows[14]["HEALTH_HEADMAN_ID"].ToString();
                }

                #endregion

                #region 规律血管指导第三行
                chkHEALTH_VERBAL_17.Text = HealthDataTable.Rows[16]["HEALTH_VERBAL"].ToString();
                chkHEALTH_WRITTEN_17.Text = HealthDataTable.Rows[16]["HEALTH_WRITTEN"].ToString();
                cmbHEALTH_APPRAISE_17.Text = HealthDataTable.Rows[16]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_17.Text = HealthDataTable.Rows[16]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (HealthDataTable.Rows[14]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_17.Text = HealthDataTable.Rows[14]["HEALTH_NURSE_ID"].ToString();
                }
                if (HealthDataTable.Rows[14]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_17.Text = Utility.CDate(HealthDataTable.Rows[14]["HEALTH_NURSE_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[14]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_17.Text = Utility.CDate(HealthDataTable.Rows[14]["HEALTH_HEADMAN_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[14]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_17.Text = HealthDataTable.Rows[14]["HEALTH_HEADMAN_ID"].ToString();
                }
                #endregion

                #region 规律血管指导第四行
                chkHEALTH_VERBAL_18.Text = HealthDataTable.Rows[17]["HEALTH_VERBAL"].ToString();
                chkHEALTH_WRITTEN_18.Text = HealthDataTable.Rows[17]["HEALTH_WRITTEN"].ToString();
                cmbHEALTH_APPRAISE_18.Text = HealthDataTable.Rows[17]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_18.Text = HealthDataTable.Rows[17]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (HealthDataTable.Rows[14]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_18.Text = HealthDataTable.Rows[14]["HEALTH_NURSE_ID"].ToString();
                }
                if (HealthDataTable.Rows[14]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_18.Text = Utility.CDate(HealthDataTable.Rows[14]["HEALTH_NURSE_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[14]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_18.Text = Utility.CDate(HealthDataTable.Rows[14]["HEALTH_HEADMAN_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[14]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_18.Text = HealthDataTable.Rows[14]["HEALTH_HEADMAN_ID"].ToString();
                }
                #endregion

                #region 规律血管指导第五行
                chkHEALTH_VERBAL_19.Text = HealthDataTable.Rows[18]["HEALTH_VERBAL"].ToString();
                chkHEALTH_WRITTEN_19.Text = HealthDataTable.Rows[18]["HEALTH_WRITTEN"].ToString();
                cmbHEALTH_APPRAISE_19.Text = HealthDataTable.Rows[18]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_19.Text = HealthDataTable.Rows[18]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (HealthDataTable.Rows[14]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_19.Text = HealthDataTable.Rows[14]["HEALTH_NURSE_ID"].ToString();
                }
                if (HealthDataTable.Rows[14]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_19.Text = Utility.CDate(HealthDataTable.Rows[14]["HEALTH_NURSE_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[14]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_19.Text = Utility.CDate(HealthDataTable.Rows[14]["HEALTH_HEADMAN_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[14]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_19.Text = HealthDataTable.Rows[14]["HEALTH_HEADMAN_ID"].ToString();
                }
                #endregion

                #region 规律血管指导第六行
                chkHEALTH_VERBAL_20.Text = HealthDataTable.Rows[19]["HEALTH_VERBAL"].ToString();
                chkHEALTH_WRITTEN_20.Text = HealthDataTable.Rows[19]["HEALTH_WRITTEN"].ToString();
                cmbHEALTH_APPRAISE_20.Text = HealthDataTable.Rows[19]["HEALTH_APPRAISE"].ToString();
                cmbHEALTH_HEADMAN_APPRAISE_20.Text = HealthDataTable.Rows[19]["HEALTH_HEADMAN_APPRAISE"].ToString();
                if (HealthDataTable.Rows[14]["HEALTH_NURSE_ID"] != null) {
                    txtHEALTH_NURSE_ID_20.Text = HealthDataTable.Rows[14]["HEALTH_NURSE_ID"].ToString();
                }
                if (HealthDataTable.Rows[14]["HEALTH_NURSE_DATE"] != null) {
                    txtHEALTH_NURSE_DATE_20.Text = Utility.CDate(HealthDataTable.Rows[14]["HEALTH_NURSE_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[14]["HEALTH_HEADMAN_DATE"] != null) {
                    txtHEALTH_HEADMAN_DATE_20.Text = Utility.CDate(HealthDataTable.Rows[14]["HEALTH_HEADMAN_DATE"].ToString()).ToShortDateString();
                }
                if (HealthDataTable.Rows[14]["HEALTH_HEADMAN_ID"] != null) {
                    txtHEALTH_HEADMAN_ID_20.Text = HealthDataTable.Rows[14]["HEALTH_HEADMAN_ID"].ToString();
                }
                #endregion

                #endregion

            }
        }

        public class GridHelper {
            /// <summary>
            /// 是否显示边框
            /// </summary>
            public static readonly DependencyProperty ShowBorderProperty =
                DependencyProperty.RegisterAttached("ShowBorder", typeof(bool), typeof(GridHelper),
            new PropertyMetadata(OnShowBorderChanged));

            /// <summary>
            /// 边框边线的宽度
            /// </summary>
            public static readonly DependencyProperty GridLineThicknessProperty =
                DependencyProperty.RegisterAttached("GridLineThickness", typeof(double), typeof(GridHelper),
                new PropertyMetadata(OnGridLineThicknessChanged));
            /// <summary>
            /// 边框的颜色
            /// </summary>
            public static readonly DependencyProperty GridLineBrushProperty =
                DependencyProperty.RegisterAttached("GridLineBrush", typeof(Brush), typeof(GridHelper),
                new PropertyMetadata(OnGridLineBrushChanged));

            #region ShowBorder
            /// <summary>
            /// 依赖属性的一堆设置
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public static bool GetShowBorder(DependencyObject obj) {
                return (bool)obj.GetValue(ShowBorderProperty);
            }
            public static void SetShowBorder(DependencyObject obj, bool value) {
                obj.SetValue(ShowBorderProperty, value);
            }
            private static void OnShowBorderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
                var grid = d as Grid;
                if ((bool)e.OldValue) {
                    grid.Loaded -= (s, arg) => { };
                }
                if ((bool)e.NewValue) {
                    grid.Loaded += new RoutedEventHandler(GridLoaded);
                }
            }
            #endregion

            #region GridLineThickness
            public static double GetGridLineThickness(DependencyObject obj) {
                return (double)obj.GetValue(GridLineThicknessProperty);
            }
            public static void SetGridLineThickness(DependencyObject obj, double value) {
                obj.SetValue(GridLineThicknessProperty, value);
            }
            private static void OnGridLineThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {

            }
            #endregion

            #region GridLineBrush
            public static Brush GetGridLineBrush(DependencyObject obj) {
                Brush brush = (Brush)obj.GetValue(GridLineBrushProperty);
                return brush == null ? Brushes.LightGray : brush;
            }
            public static void SetGridLineBrush(DependencyObject obj, Brush value) {
                obj.SetValue(GridLineBrushProperty, value);
            }
            private static void OnGridLineBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            }
            #endregion

            /// <summary>
            /// 把cell里的元素加一个border 然后把原来的remove 再add进去
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private static void GridLoaded(object sender, RoutedEventArgs e) {
                Grid grid = sender as Grid;

                var row_count = grid.RowDefinitions.Count;
                var column_count = grid.ColumnDefinitions.Count;

                #region
                var controls = grid.Children;
                var count = controls.Count;

                for (int i = 0; i < count; i++) {
                    var item = controls[i] as FrameworkElement;
                    var row = Grid.GetRow(item);
                    var column = Grid.GetColumn(item);
                    var rowspan = Grid.GetRowSpan(item);
                    var columnspan = Grid.GetColumnSpan(item);

                    var settingThickness = GetGridLineThickness(grid);
                    Thickness thickness = new Thickness(settingThickness / 2);
                    if (row == 0)
                        thickness.Top = settingThickness;
                    if (row + rowspan == row_count)
                        thickness.Bottom = settingThickness;
                    if (column == 0)
                        thickness.Left = settingThickness;
                    if (column + columnspan == column_count)
                        thickness.Right = settingThickness;

                    //画框默认只画右边跟下边 2个边
                    var border = new Border() {
                        BorderBrush = GetGridLineBrush(grid),
                        BorderThickness = new Thickness(0, 0, 1, 1),
                        Padding = new Thickness(0)
                    };
                    int rowIndex = int.Parse(item.GetValue(Grid.RowProperty).ToString());
                    int colIndex = int.Parse(item.GetValue(Grid.ColumnProperty).ToString());
                    if (rowIndex == 0)//如果是第一行的元素 画 上边右边下边
                        border.BorderThickness = new Thickness(0, 1, 1, 1);
                    if (colIndex == 0)//如果是第一列的元素 画 左边右边下边
                        border.BorderThickness = new Thickness(1, 0, 1, 1);
                    if (colIndex == 0 && rowIndex == 0)//如果既是第一行右是第一列的元素 四边画全
                        border.BorderThickness = new Thickness(1, 1, 1, 1);

                    Grid.SetRow(border, row);
                    Grid.SetColumn(border, column);
                    Grid.SetRowSpan(border, rowspan);
                    Grid.SetColumnSpan(border, columnspan);


                    if (item is Border)//已经添加了就要添加框了
                    {
                        continue;

                    }
                    grid.Children.RemoveAt(i);
                    border.Child = item;
                    grid.Children.Insert(i, border);
                }
                #endregion
            }
        }
    }
}
