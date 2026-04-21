/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:检查报告单报表
 * 创建标识:吕志强-2016年9月5日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Hemo.Model;
using Hemo.Utilities;
using System.Data;

namespace Hemo.Client.Print
{
    public partial class PatientExamReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 类变量

        private PatientModel.MED_PATIENTSRow patientRow;

        private ReportRelationModel.EXAMMASTERDataTable dtMaster;

        private ReportRelationModel.EXAMDETAILDataTable dtDetail;

        #endregion

        #region 构造函数

        public PatientExamReport(PatientModel.MED_PATIENTSRow patientRow, ReportRelationModel.EXAMMASTERDataTable dtMaster, ReportRelationModel.EXAMDETAILDataTable dtDetail)
        {
            InitializeComponent();
            this.patientRow = patientRow;
            this.dtMaster = dtMaster;
            this.dtDetail = dtDetail;
        }

        #endregion

        #region 事件

        /// <summary>
        /// 打印前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientExamReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.lblHead.Text = Utility.GetHospitalName() + "检查报告单";
            this.lblType.Text = patientRow.WHAT_DEPARTMENT_IN;
            this.lblName.Text = patientRow.NAME;
            this.lblSex.Text = patientRow.SEX;
            this.lblPatientId.Text = patientRow.PATIENT_ID;
            this.lblDiagnose.Text = patientRow.DIAGNOSE;
            this.lblHemoId.Text = patientRow.HEMODIALYSIS_ID;

            DataSet dsSource = new DataSet();
            dsSource.Tables.Add(dtMaster);
            dsSource.Tables.Add(dtDetail);
            DataColumn parent = dtMaster.Columns["EXAM_NO"];
            DataColumn child = dtDetail.Columns["EXAM_NO"];
            DataRelation relation = new DataRelation("FK_EXAMMASTER_EXAMDETAIL", parent, child);
            dsSource.Relations.Add(relation);

            this.DataSource = dsSource;
            this.DetailReport.DataSource = dsSource;
        }

        #endregion
    }
}
