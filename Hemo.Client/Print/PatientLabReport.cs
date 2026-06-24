/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:检验报告单报表
 * 创建标识:吕志强-2016年8月7日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using DevExpress.XtraReports.UI;
using Hemo.IService.Machine;
using Hemo.Service;
using Hemo.Model;
using Hemo.IService.Config;
using System.Data;

namespace Hemo.Client.Print
{
    public partial class PatientLabReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 类变量

        private IConfig _configService = ServiceManager.Instance.ConfigService;

        #endregion

        #region 构造函数

        public PatientLabReport(PatientModel.MED_PATIENTSRow _patientRow, ReportRelationModel.LABMASTERDataTable dtmaster,ReportRelationModel.LABDETAILDataTable dtdetail)
        {
            InitializeComponent();
            //给有关系的主从表进行赋值
            foreach (ReportRelationModel.LABMASTERRow dr in dtmaster.Rows)
            {
                this.reportRelationModel1.LABMASTER.Rows.Add(dr.ItemArray);
            }
            foreach (ReportRelationModel.LABDETAILRow dr in dtdetail.Rows)
            {
                this.reportRelationModel1.LABDETAIL.Rows.Add(dr.ItemArray);
            }                   
            //报表内容赋值
            xrLabel2.Text = Utilities.Utility.GetHospitalName() + "检验报告单";
            this.xrLabelType.Text = _patientRow.WHAT_DEPARTMENT_IN;
            xrLabelName.Text = _patientRow.NAME;
            xrLabelSex.Text = _patientRow.SEX;
            xrLabelPatientID.Text = _patientRow.PATIENT_ID;
            xrLabel_diaglose.Text = _patientRow.DIAGNOSE;
            xrLabelInpatientid.Text = _patientRow.HEMODIALYSIS_ID;
            //指定一下数据源.
            this.DataSource = this.reportRelationModel1;
            this.DetailReport.DataSource = this.reportRelationModel1;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 对像转换
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private string ConvertToString(object o)
        {
            if (o == null)
                return string.Empty;
            if (o == DBNull.Value || o is DBNull)
                return string.Empty;
            return o.ToString();
        }

        #endregion
    }
}
