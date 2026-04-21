/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司
// 描述：病历基本资料报表类
// 创建时间：2015-08-12
// 创建者：吕志强
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Hemo.IService;
using Hemo.Service;
using Hemo.Model;
using Hemo.IService.Config;
using Hemo.Utilities;

namespace Hemo.Client.Print
{
    public partial class PatientBaseRecordReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 类变量

        private string hemoId = string.Empty;

        private PatientModel.MED_PATIENTSDataTable dtPatient = null;

        private PatientModel.MED_BASE_RECORDDataTable dtRecord = null;

        private IPatient patientService = ServiceManager.Instance.PatientService;

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        #endregion

        #region 属性

        /// <summary>
        /// 透析编号
        /// </summary>
        public string HemoId
        {
            get { return hemoId; }
            set { hemoId = value; }
        }

        #endregion

        #region 构造函数

        public PatientBaseRecordReport(string hemoId)
        {
            InitializeComponent();
            this.hemoId = hemoId;

            CreateEventTable();
            var dtEvent = hemoService.GetRecordEventByHemoId(hemoId);
            this.DetailReport1.DataMember = string.Empty;
            this.DetailReport1.DataSource = dtEvent;

            CreateDiagnoseTable();
            var dtDiagnose = hemoService.GetRecordDiagnoseByHemoId(hemoId);
            this.DetailReport2.DataMember = string.Empty;
            this.DetailReport2.DataSource = dtDiagnose;
        }

        #endregion

        #region 事件

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            LoadUserInfo();
            LoadBaseRecord();
        }

        /// <summary>
        /// 在该事件中实现，列表记录只能显示一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Detail1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //CreateEventTable();
            //var dtResult = hemoService.GetRecordEventByHemoId(hemoId);
            //this.DetailReport1.DataMember = string.Empty;
            //this.DetailReport1.DataSource = dtResult;
        }

        /// <summary>
        /// 在该事件中实现，列表记录只能显示一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Detail2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //CreateDiagnoseTable();
            //var dtResult = hemoService.GetRecordDiagnoseByHemoId(hemoId);
            //this.DetailReport2.DataMember = string.Empty;
            //this.DetailReport2.DataSource = dtResult;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载用户信息
        /// </summary>
        private void LoadUserInfo()
        {
            dtPatient = patientService.GetPatientListByParams(string.Empty, hemoId);
            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                this.lblName.Text = dtPatient[0].NAME;
                this.lblSex.Text = dtPatient[0].SEX;
                this.lblBirthday.Text = dtPatient[0].BIRTHDAY.ToString("yyyy-MM-dd");
                this.lblHemoId.Text = dtPatient[0].HEMODIALYSIS_ID;
                this.lblJob.Text = dtPatient[0].JOB;
                this.lblMarriage.Text = dtPatient[0].MARITAL;
                this.lblEducate.Text = dtPatient[0].EDUCATION;
                this.lblWorkPhone.Text = dtPatient[0].WORK_TELEPHONE;
                this.lblAddress.Text = dtPatient[0].ADDRESS;
                this.lblPhone.Text = dtPatient[0].TELEPHONE;
                this.lblContact.Text = dtPatient[0].NATIVEPLACE;
                this.lblDiagnose.Text = dtPatient[0].DIAGNOSE;
            }
        }

        /// <summary>
        /// 加载患者病历基本资料
        /// </summary>
        private void LoadBaseRecord()
        {
            dtRecord = hemoService.GetBaseRecordByHemoId(hemoId);
            if (dtRecord != null && dtRecord.Rows.Count > 0)
            {
                this.chkCGN.Checked = dtRecord.Rows[0]["CGN"] != DBNull.Value ? (dtRecord.Rows[0]["CGN"].ToString() == "1" ? true : false) : false;
                this.chkCIN.Checked = dtRecord.Rows[0]["CIN"] != DBNull.Value ? (dtRecord.Rows[0]["CIN"].ToString() == "1" ? true : false) : false;
                this.chkDN.Checked = dtRecord.Rows[0]["DN"] != DBNull.Value ? (dtRecord.Rows[0]["DN"].ToString() == "1" ? true : false) : false;
                this.chkHRD.Checked = dtRecord.Rows[0]["HRD"] != DBNull.Value ? (dtRecord.Rows[0]["HRD"].ToString() == "1" ? true : false) : false;
                this.chkPCKD.Checked = dtRecord.Rows[0]["PCKD"] != DBNull.Value ? (dtRecord.Rows[0]["PCKD"].ToString() == "1" ? true : false) : false;
                this.chkUUO.Checked = dtRecord.Rows[0]["UUO"] != DBNull.Value ? (dtRecord.Rows[0]["UUO"].ToString() == "1" ? true : false) : false;
                this.chkRENAL_TUMOR.Checked = dtRecord.Rows[0]["RENAL_TUMOR"] != DBNull.Value ? (dtRecord.Rows[0]["RENAL_TUMOR"].ToString() == "1" ? true : false) : false;
                this.chkOTHER_PROTOPATHY.Checked = dtRecord.Rows[0]["OTHER_PROTOPATHY"] != DBNull.Value ? (dtRecord.Rows[0]["OTHER_PROTOPATHY"].ToString() == "1" ? true : false) : false;
                this.lblOTHER_PROTOPATHY.Text = dtRecord.Rows[0]["OTHER_PROTOPATHY_TEXT"].ToString();

                this.chkHTN.Checked = dtRecord.Rows[0]["HTN"] != DBNull.Value ? (dtRecord.Rows[0]["HTN"].ToString() == "1" ? true : false) : false;
                this.chkDM.Checked = dtRecord.Rows[0]["DM"] != DBNull.Value ? (dtRecord.Rows[0]["DM"].ToString() == "1" ? true : false) : false;
                this.chkCAD.Checked = dtRecord.Rows[0]["CAD"] != DBNull.Value ? (dtRecord.Rows[0]["CAD"].ToString() == "1" ? true : false) : false;
                this.chkCHF.Checked = dtRecord.Rows[0]["CHF"] != DBNull.Value ? (dtRecord.Rows[0]["CHF"].ToString() == "1" ? true : false) : false;
                this.chkCVA.Checked = dtRecord.Rows[0]["CVA"] != DBNull.Value ? (dtRecord.Rows[0]["CVA"].ToString() == "1" ? true : false) : false;
                this.chkPAOD.Checked = dtRecord.Rows[0]["PAOD"] != DBNull.Value ? (dtRecord.Rows[0]["PAOD"].ToString() == "1" ? true : false) : false;
                this.chkCOPD.Checked = dtRecord.Rows[0]["COPD"] != DBNull.Value ? (dtRecord.Rows[0]["COPD"].ToString() == "1" ? true : false) : false;
                this.chkANOIA.Checked = dtRecord.Rows[0]["ANOIA"] != DBNull.Value ? (dtRecord.Rows[0]["ANOIA"].ToString() == "1" ? true : false) : false;
                this.chkOTHER_COMORBIDITY.Checked = dtRecord.Rows[0]["OTHER_COMORBIDITY"] != DBNull.Value ? (dtRecord.Rows[0]["OTHER_COMORBIDITY"].ToString() == "1" ? true : false) : false;
                this.lblOTHER_COMORBIDITY.Text = dtRecord.Rows[0]["OTHER_COMORBIDITY_TEXT"].ToString();

                this.lblFAMILY_HISTORY.Text = dtRecord.Rows[0]["FAMILY_HISTORY"].ToString();
                this.lblOPERATION_HISTORY.Text = dtRecord.Rows[0]["OPERATION_HISTORY"].ToString();
                this.chkSMOKE_NO.Checked = dtRecord.Rows[0]["SMOKE"].ToString() == "0" ? true : false;
                this.chkSMOKE_YES.Checked = dtRecord.Rows[0]["SMOKE"].ToString() == "1" ? true : false;
                //this.lblSMOKE_YEAR.Text = dtRecord.Rows[0]["SMOKE_YEAR"].ToString();
                this.lblSMOKE_NUM.Text = dtRecord.Rows[0]["SMOKE_NUM"].ToString();
                this.lblDRUG_ALLERGY.Text = dtRecord.Rows[0]["DRUG_ALLERGY"].ToString();
                this.lblFOOD_ALLERGY.Text = dtRecord.Rows[0]["FOOD_ALLERGY"].ToString();
                this.lblDIALYZER_ALLERGY.Text = dtRecord.Rows[0]["DIALYZER_ALLERGY"].ToString();

                this.lblDIALYSIS_BEGIN.Text = dtRecord.Rows[0]["DIALYSIS_BEGIN"] != DBNull.Value ? Utility.CDate(dtRecord.Rows[0]["DIALYSIS_BEGIN"].ToString()).ToString("yyyy-MM-dd") : string.Empty;
                this.chkPD_EXIST_NO.Checked = dtRecord.Rows[0]["PD_EXIST"].ToString() == "0" ? true : false;
                this.chkPD_EXIST_YES.Checked = dtRecord.Rows[0]["PD_EXIST"].ToString() == "1" ? true : false;
                this.lblPD_YEAR.Text = dtRecord.Rows[0]["PD_YEAR"].ToString();
                this.chkRENAL_TRANSPLANT_EXIST_NO.Checked = dtRecord.Rows[0]["RENAL_TRANSPLANT_EXIST"].ToString() == "0" ? true : false;
                this.chkRENAL_TRANSPLANT_EXIST_YES.Checked = dtRecord.Rows[0]["RENAL_TRANSPLANT_EXIST"].ToString() == "1" ? true : false;
                this.lblRENAL_TRANSPLANT_YEAR.Text = dtRecord.Rows[0]["RENAL_TRANSPLANT_YEAR"].ToString();
                this.lblINTO_DATE.Text = dtRecord.Rows[0]["INTO_DATE"] != DBNull.Value ? Utility.CDate(dtRecord.Rows[0]["INTO_DATE"].ToString()).ToString("yyyy-MM-dd") : string.Empty;
                this.lblINTO_HOSPITAL.Text = dtRecord.Rows[0]["INTO_HOSPITAL"].ToString();
                this.lblDIALYSIS_END.Text = dtRecord.Rows[0]["DIALYSIS_END"] != DBNull.Value ? Utility.CDate(dtRecord.Rows[0]["DIALYSIS_END"].ToString()).ToString("yyyy-MM-dd") : string.Empty;
                this.chkTransfer_Hospital.Checked = dtRecord.Rows[0]["DIALYSIS_END_REASON"].ToString() == "0" ? true : false;
                this.chkTransplant.Checked = dtRecord.Rows[0]["DIALYSIS_END_REASON"].ToString() == "1" ? true : false;
                this.chkPD.Checked = dtRecord.Rows[0]["DIALYSIS_END_REASON"].ToString() == "2" ? true : false;
                this.chkRecover_Kidney.Checked = dtRecord.Rows[0]["DIALYSIS_END_REASON"].ToString() == "3" ? true : false;
                this.chkDead.Checked = dtRecord.Rows[0]["DIALYSIS_END_REASON"].ToString() == "4" ? true : false;
                this.lblDEAD_DATE.Text = dtRecord.Rows[0]["DEAD_DATE"] != DBNull.Value ? Utility.CDate(dtRecord.Rows[0]["DEAD_DATE"].ToString()).ToString("yyyy-MM-dd") : string.Empty;
                this.lblDEAD_REASON.Text = dtRecord.Rows[0]["DEAD_REASON"].ToString();
                this.txtONSET_PASS.Text = dtRecord.Rows[0]["ONSET_PASS"].ToString();
            }
        }

        /// <summary>
        /// 创建事件表
        /// </summary>
        private void CreateEventTable()
        {
            XRTable table1 = this.xrTable1;
            table1.Borders = (DevExpress.XtraPrinting.BorderSide)DevExpress.XtraPrinting.BorderSide.All;
            table1.BorderWidth = 1;
            table1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            table1.Name = "xrTable1";
            table1.SizeF = new SizeF(600F, 25F);
            table1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular);
            table1.StylePriority.UseBorders = false;
            table1.StylePriority.UseTextAlignment = false;
            table1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            table1.Rows.Clear();

            XRTableRow row1 = new XRTableRow();
            row1.Name = "xrTableRow1";
            row1.Weight = 1D;

            table1.Rows.AddRange(new XRTableRow[] { row1 });

            XRTableCell cellCREATE_DATE = new XRTableCell();
            cellCREATE_DATE.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellCREATE_DATE.Name = "xrTableCell0";
            cellCREATE_DATE.Weight = 0.75D;
            cellCREATE_DATE.StylePriority.UseBorders = false;
            cellCREATE_DATE.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "CREATE_DATE", "{0:yyyy-MM-dd}") });

            XRTableCell cellDIALYSIS_STAGE = new XRTableCell();
            cellDIALYSIS_STAGE.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellDIALYSIS_STAGE.Name = "xrTableCell1";
            cellDIALYSIS_STAGE.Weight = 0.75D;
            cellDIALYSIS_STAGE.StylePriority.UseBorders = false;
            cellDIALYSIS_STAGE.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "DIALYSIS_STAGE") });

            XRTableCell cellCOMPLICATION = new XRTableCell();
            cellCOMPLICATION.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellCOMPLICATION.Name = "xrTableCell2";
            cellCOMPLICATION.Weight = 0.75D;
            cellCOMPLICATION.StylePriority.UseBorders = false;
            cellCOMPLICATION.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "COMPLICATION") });

            XRTableCell cellCHRONIC_EVENT = new XRTableCell();
            cellCHRONIC_EVENT.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellCHRONIC_EVENT.Name = "xrTableCell3";
            cellCHRONIC_EVENT.Weight = 0.75D;
            cellCHRONIC_EVENT.StylePriority.UseBorders = false;
            cellCHRONIC_EVENT.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "CHRONIC_EVENT") });

            XRTableCell cellDOCTOR_NAME = new XRTableCell();
            cellDOCTOR_NAME.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellDOCTOR_NAME.Name = "xrTableCell4";
            cellDOCTOR_NAME.Weight = 0.75D;
            cellDOCTOR_NAME.StylePriority.UseBorders = false;
            cellDOCTOR_NAME.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "DOCTOR_NAME") });

            row1.Cells.AddRange(new XRTableCell[] { cellCREATE_DATE, cellDIALYSIS_STAGE, cellCOMPLICATION, cellCHRONIC_EVENT, cellDOCTOR_NAME });
            row1.Cells[0].WidthF = 101F;
            row1.Cells[1].WidthF = 100F;
            row1.Cells[2].WidthF = 100F;
            row1.Cells[3].WidthF = 200F;
            row1.Cells[4].WidthF = 99F;
        }

        /// <summary>
        /// 创建诊断表
        /// </summary>
        private void CreateDiagnoseTable()
        {
            XRTable table2 = this.xrTable2;
            table2.Borders = (DevExpress.XtraPrinting.BorderSide)DevExpress.XtraPrinting.BorderSide.All;
            table2.BorderWidth = 1;
            table2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            table2.Name = "xrTable2";
            table2.SizeF = new SizeF(500F, 25F);
            table2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular);
            table2.StylePriority.UseBorders = false;
            table2.StylePriority.UseTextAlignment = false;
            table2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            table2.Rows.Clear();

            XRTableRow row2 = new XRTableRow();
            row2.Name = "xrTableRow2";
            row2.Weight = 1D;

            table2.Rows.AddRange(new XRTableRow[] { row2 });

            XRTableCell cellIN_HOSPITAL_DATE = new XRTableCell();
            cellIN_HOSPITAL_DATE.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellIN_HOSPITAL_DATE.Name = "xrTableCell5";
            cellIN_HOSPITAL_DATE.Weight = 0.75D;
            cellIN_HOSPITAL_DATE.StylePriority.UseBorders = false;
            cellIN_HOSPITAL_DATE.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "IN_HOSPITAL_DATE", "{0:yyyy-MM-dd}") });

            XRTableCell cellLEAVE_HOSPITAL_DATE = new XRTableCell();
            cellLEAVE_HOSPITAL_DATE.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellLEAVE_HOSPITAL_DATE.Name = "xrTableCell6";
            cellLEAVE_HOSPITAL_DATE.Weight = 0.75D;
            cellLEAVE_HOSPITAL_DATE.StylePriority.UseBorders = false;
            cellLEAVE_HOSPITAL_DATE.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "LEAVE_HOSPITAL_DATE", "{0:yyyy-MM-dd}") });

            XRTableCell cellDIAGNOSE = new XRTableCell();
            cellDIAGNOSE.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellDIAGNOSE.Name = "xrTableCell7";
            cellDIAGNOSE.Weight = 0.75D;
            cellDIAGNOSE.StylePriority.UseBorders = false;
            cellDIAGNOSE.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "DIAGNOSE") });

            XRTableCell cellDOCTOR_NAME2 = new XRTableCell();
            cellDOCTOR_NAME2.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellDOCTOR_NAME2.Name = "xrTableCell8";
            cellDOCTOR_NAME2.Weight = 0.75D;
            cellDOCTOR_NAME2.StylePriority.UseBorders = false;
            cellDOCTOR_NAME2.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "DOCTOR_NAME") });

            row2.Cells.AddRange(new XRTableCell[] { cellIN_HOSPITAL_DATE, cellLEAVE_HOSPITAL_DATE, cellDIAGNOSE, cellDOCTOR_NAME2 });
            row2.Cells[0].WidthF = 101F;
            row2.Cells[1].WidthF = 100F;
            row2.Cells[2].WidthF = 200F;
            row2.Cells[3].WidthF = 99F;
        }

        #endregion
    }
}
