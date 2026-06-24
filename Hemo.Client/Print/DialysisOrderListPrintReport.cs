using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using DevExpress.XtraReports.UI;
using Hemo.Model;
using Hemo.IService;

namespace Hemo.Client.Print
{
    /// <summary>
    /// 2026-05-20 刘建超 透析方案 医嘱列表打印 
    /// </summary>
    public partial class DialysisOrderListPrintReport : XtraReport
    {
        //透析患者基本信息
        private IPatient objPatient = Hemo.Service.ServiceManager.Instance.PatientService;
        private PatientModel.MED_PATIENTSDataTable _patientDataTable;


        public DialysisOrderListPrintReport(string pHemoID, HemodialysisModel.MED_CURE_DRUGDataTable lsyz, HemodialysisModel.MED_CURE_LONGDRUGDataTable cqyz,bool hide_cqyz=false,bool hide_lsyz=false)
        {
            InitializeComponent();
            _patientDataTable = objPatient.GetPatientListByParams("", pHemoID);
            if (_patientDataTable != null && _patientDataTable.Rows.Count > 0)
            {
                lblHemoID.Text = pHemoID;
                lblName.Text = _patientDataTable.Rows[0]["NAME"].ToString();
                lblSex.Text = _patientDataTable.Rows[0]["SEX"].ToString();
                lblAge.Text = _patientDataTable.Rows[0]["AGE"].ToString();
            }
            //长期医嘱
            if (!hide_cqyz)
            {
                SetBind(this.xrTableCell2, "STATUS2");//执行状态
                SetBind(this.xrTableCell3, "NAME");//开方医生
                SetBind(this.xrTableCell4, "DRUG_NAME");//药品名称
                SetBind(this.xrTableCell5, "DRUG_MODE_NAME");//药品用法
                SetBind(this.xrTableCell7, "DOSAGE");//单次剂量
                SetBind(this.xrTableCell6, "UNIT_NAME");//剂量单位
                SetBind(this.xrTableCell8, "DRUG_DAYS");//每周几
                SetBind(this.xrTableCell9, "REMARK");//备注
                SetBind(this.xrTableCell10, "CREATE_DATE");//开药时间
                SetBind(this.xrTableCell22, "DRUG_NURSE_ID");//执行护士
                SetBind(this.xrTableCell23, "EXEC_DATE");//执行时间
                SetBind(this.xrTableCell25, "STOP_TIME");//停止时间
                SetBind(this.xrTableCell24, "DRUG_TIMES");//频次
                this.DetailReport.DataSource = cqyz;
            }
            else
            {
                this.DetailReport.Visible = false;
            }
            //临时医嘱
            if (!hide_lsyz)
            {
                SetBind(this.xrTableCell12, "STATUS2");//执行状态
                SetBind(this.xrTableCell13, "NAME");//开方医生
                SetBind(this.xrTableCell14, "DRUG_NAME");//项目名称
                SetBind(this.xrTableCell15, "DRUG_TIMETYPE");//给药阶段
                SetBind(this.xrTableCell16, "DRUG_MODE_NAME");//药品用法
                SetBind(this.xrTableCell17, "DOSAGE");//单次剂量
                SetBind(this.xrTableCell18, "UNIT_NAME");//剂量单位
                SetBind(this.xrTableCell26, "REMARK");//备注
                SetBind(this.xrTableCell19, "CREATE_DATE");//开药时间
                SetBind(this.xrTableCell20, "DRUG_NURSE_NAME");//执行护士
                SetBind(this.xrTableCell21, "EXEC_DATE");//执行时间
                this.DetailReport1.DataSource = lsyz;
            }
            else
            {
                this.DetailReport1.Visible = false;
            }
        }
        /// <summary>
        /// 设置绑定字段
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="FieldName">字段名</param>
        /// <param name="formatString">字段格式化</param>
        private void SetBind(DevExpress.XtraReports.UI.XRTableCell cell, string FieldName, string formatString = "")
        {
            //formatString= "{0:yyyy/MM/dd HH:mm:ss}"
            if (formatString.Length > 0)
                cell.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, FieldName, formatString)});
            else
                cell.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null,FieldName)});
        }


    }
}
