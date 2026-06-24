/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技股份有限公司
 * 文件功能描述:血液透析病历报表
 * 创建标识:吕志强-2016年5月8日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Hemo.Model;
using Hemo.IService;
using Hemo.Service;
using System.Linq;
using Hemo.IService.Config;
using System.Data;
namespace Hemo.Client.Print
{
    public partial class FirstHemoRecord : DevExpress.XtraReports.UI.XtraReport
    {
        #region 成员变量

        private string _currentHemoId;

        private DateTime _startTime;

        private DateTime _endTime;

        private IPatient patientService = ServiceManager.Instance.PatientService;

        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;

        #endregion

        #region 属性

        public string CurrentHemoId
        {
            get { return _currentHemoId; }
            set
            {
                _currentHemoId = value;
            }
        }

        public DateTime StartTime { get { return _startTime; } set { _startTime = value; } }

        public DateTime EndTime { get { return _endTime; } set { _endTime = value; } }

        #endregion

        #region 构造函数

        public FirstHemoRecord()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// ReportHeader打印前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.PrintingSystem.ShowMarginsWarning = false;
            DataTable dtRecord = patientService.QueryPatientRecordByParams(_currentHemoId, _startTime, _endTime);

            if (dtRecord != null && dtRecord.Rows.Count > 0)
            {
                DataRow recordRow = dtRecord.AsEnumerable().OrderBy(row => row["CREATEDATE"]).First();

                lblName.Text = recordRow["NAME"].ToString();
                lblSex.Text = recordRow["SEX"].ToString();
                lblAge.Text = recordRow["AGE"].ToString();
                lblWeight.Text = recordRow["WEIGHT"].ToString();
                lblHemoId.Text = recordRow["HEMODIALYSIS_ID"].ToString();
                lblIdNo.Text = recordRow["CREDENTIALS_NUMBER"].ToString();
                lblAddress.Text = recordRow["ADDRESS"].ToString();
                lblPhone.Text = recordRow["TELEPHONE"].ToString();
                lblAction.Text = recordRow["ACTION"].ToString();
                lblPresentIllness.Text = recordRow["PRESENTILLNESS"].ToString();
                lblOtherProtopathy.Text = recordRow["OTHER_PROTOPATHY"].ToString();
                lblOtherPastHistory.Text = recordRow["OTHER_PAST_HISTORY"].ToString();
                lblPhysicalExam.Text = "T: " + recordRow["T"].ToString() + "℃  P: " + recordRow["P"].ToString() + "次/分  R: " + recordRow["R"].ToString() + "次/分  BP: " + recordRow["BP"].ToString() + "mm/Hg";
                lblConsciousness.Text = recordRow["CONSCIOUSNESS"].ToString();
                lblDoubleLungAuscultation.Text = recordRow["DOUBLE_LUNG_AUSCULTATION"].ToString();
                lblRabat.Text = recordRow["RABAT"].ToString();
                lblOtherBUltrasonic.Text = recordRow["OTHER_B_ULTRASONIC"].ToString();
                lblBloodroutine.Text = "RBC: " + recordRow["RBC"].ToString() + "×10^12/L  WBC: " + recordRow["WBC"].ToString() + "×10^9个/L  HGB: " + recordRow["HGB"].ToString() + "g/L  PLT: " + recordRow["PLT"].ToString() + "×10^9个/L  HCT: " + recordRow["HCT"].ToString() + "%";
                lblBloodChemistry.Text = "BUN: " + recordRow["BUN"].ToString() + "mmol/L  Scr: " + recordRow["SCR"].ToString() + "μmol/L  UA: " + recordRow["UA"].ToString() + "mmol/L  K: " + recordRow["K"].ToString() + "mmol/L  Ca: " + recordRow["CA"].ToString() + "mmol/L  P: " + recordRow["PH"].ToString() + "mmol/L";
                lblOtherInfection.Text = recordRow["OTHER_INFECTION"].ToString();
                lblPT.Text = recordRow["PT"].ToString();
                lblAPTT.Text = recordRow["APTT"].ToString();
                lblINR.Text = recordRow["INR"].ToString();
                lblSerumIron.Text = recordRow["SERUM_IRON"].ToString();
                lblTIBC.Text = recordRow["TIBC"].ToString();
                lblFerritin.Text = recordRow["FERROPROTEIN"].ToString();
                lblPTH.Text = recordRow["PTH"].ToString();
                lblDrugApplication.Text = recordRow["DRUG_APPLICATION"].ToString();
                lblDiagnosis.Text = recordRow["DIAGNOSIS"].ToString();
                lblDrugUse.Text = recordRow["DRUG_USE"].ToString();
                lblDoctor.Text = recordRow["CREATEBY"].ToString();
                lblDate.Text = DateTime.Parse(recordRow["CREATEDATE"].ToString()).ToString("yyyy-MM-dd");

                if (recordRow["SUGAR_DIABETES"].ToString() == "1")
                {
                    chkSugarDiabetes.Checked = true;
                }
                else
                {
                    chkSugarDiabetes.Checked = false;
                }

                if (recordRow["HIGH_BLOOD_PRESSURE"].ToString() == "1")
                {
                    chkHighBloodPressure.Checked = true;
                }
                else
                {
                    chkHighBloodPressure.Checked = false;
                }

                if (recordRow["CHRONIC_NEPHRITIS"].ToString() == "1")
                {
                    chkChronicNephritis.Checked = true;
                }
                else
                {
                    chkChronicNephritis.Checked = false;
                }

                if (recordRow["POLYCYSTIC_KIDNEY"].ToString() == "1")
                {
                    chkPolycysticKidney.Checked = true;
                }
                else
                {
                    chkPolycysticKidney.Checked = false;
                }

                if (recordRow["URINARY_CALCULUS"].ToString() == "1")
                {
                    chkUrinaryCalculus.Checked = true;
                }
                else
                {
                    chkUrinaryCalculus.Checked = false;
                }

                if (recordRow["DRUG_ALLERGY_HISTORY"].ToString() == "1")
                {
                    chkDrugAllergyHistory.Checked = true;
                }
                else
                {
                    chkDrugAllergyHistory.Checked = false;
                }

                if (recordRow["BODY_POSITION"].ToString() == "0")
                {
                    chkAuto.Checked = true;
                    chkSitup.Checked = false;
                    chkSupine.Checked = false;
                }
                else if (recordRow["BODY_POSITION"].ToString() == "1")
                {
                    chkSitup.Checked = true;
                    chkAuto.Checked = false;
                    chkSupine.Checked = false;
                }
                else if (recordRow["BODY_POSITION"].ToString() == "2")
                {
                    chkSupine.Checked = true;
                    chkAuto.Checked = false;
                    chkSitup.Checked = false;
                }

                if (recordRow["CHECK_UP"].ToString() == "0")
                {
                    chkchkNoncooperate.Checked = true;
                    chkCooperate.Checked = false;
                }
                else if (recordRow["CHECK_UP"].ToString() == "1")
                {
                    chkCooperate.Checked = true;
                    chkchkNoncooperate.Checked = false;
                }

                if (recordRow["FULL_BODY_SKIN_1"].ToString() == "0")
                {
                    chkNoYellowDye.Checked = true;
                    chkYellowDye.Checked = false;
                }
                else if (recordRow["FULL_BODY_SKIN_1"].ToString() == "1")
                {
                    chkYellowDye.Checked = true;
                    chkNoYellowDye.Checked = false;
                }

                if (recordRow["FULL_BODY_SKIN_2"].ToString() == "0")
                {
                    chkNoBloodPoint.Checked = true;
                    chkBloodPoint.Checked = false;
                }
                else if (recordRow["FULL_BODY_SKIN_2"].ToString() == "1")
                {
                    chkBloodPoint.Checked = true;
                    chkNoBloodPoint.Checked = false;
                }

                if (recordRow["SICKLY_LOOK"].ToString() == "0")
                {
                    chkChronic.Checked = true;
                    chkAcute.Checked = false;
                }
                else if (recordRow["SICKLY_LOOK"].ToString() == "1")
                {
                    chkAcute.Checked = true;
                    chkChronic.Checked = false;
                }

                if (recordRow["ANEMIA"].ToString() == "0")
                {
                    chkNoAnemia.Checked = true;
                    chkAnemia.Checked = false;
                }
                else if (recordRow["ANEMIA"].ToString() == "1")
                {
                    chkAnemia.Checked = true;
                    chkNoAnemia.Checked = false;
                }

                if (recordRow["EDEMA"].ToString() == "0")
                {
                    chkNoEdema.Checked = true;
                    chkWholeBody.Checked = false;
                    chkLegs.Checked = false;
                    chkFace.Checked = false;
                }
                else if (recordRow["EDEMA"].ToString() == "1")
                {
                    chkWholeBody.Checked = true;
                    chkNoEdema.Checked = false;
                    chkLegs.Checked = false;
                    chkFace.Checked = false;
                }
                else if (recordRow["EDEMA"].ToString() == "2")
                {
                    chkLegs.Checked = true;
                    chkNoEdema.Checked = false;
                    chkWholeBody.Checked = false;
                    chkFace.Checked = false;
                }
                else if (recordRow["EDEMA"].ToString() == "3")
                {
                    chkFace.Checked = true;
                    chkNoEdema.Checked = false;
                    chkWholeBody.Checked = false;
                    chkLegs.Checked = false;
                }

                if (recordRow["CARDIACDULLNESS"].ToString() == "0")
                {
                    chkCardiacdullnessNormal.Checked = true;
                    chkCardiacdullnessHigh.Checked = false;
                }
                else if (recordRow["CARDIACDULLNESS"].ToString() == "1")
                {
                    chkCardiacdullnessHigh.Checked = true;
                    chkCardiacdullnessNormal.Checked = false;
                }

                if (recordRow["HEART_RATE"].ToString() == "0")
                {
                    chkEven.Checked = true;
                    chkUneven.Checked = false;
                }
                else if (recordRow["HEART_RATE"].ToString() == "1")
                {
                    chkUneven.Checked = true;
                    chkEven.Checked = false;
                }

                if (recordRow["NOISE"].ToString() == "0")
                {
                    chkNoNoise.Checked = true;
                    chkNoise.Checked = false;
                }
                else if (recordRow["NOISE"].ToString() == "1")
                {
                    chkNoise.Checked = true;
                    chkNoNoise.Checked = false;
                }

                if (recordRow["ASCITES"].ToString() == "0")
                {
                    chkNoAscites.Checked = true;
                    chkAscites.Checked = false;
                }
                else if (recordRow["ASCITES"].ToString() == "1")
                {
                    chkAscites.Checked = true;
                    chkNoAscites.Checked = false;
                }

                if (recordRow["RENAL_SIZE"].ToString() == "0")
                {
                    chkRenalSizeNormal.Checked = true;
                    chkRenalSizeReduce.Checked = false;
                    chkRenalSizeEnlarge.Checked = false;
                }
                else if (recordRow["RENAL_SIZE"].ToString() == "1")
                {
                    chkRenalSizeReduce.Checked = true;
                    chkRenalSizeNormal.Checked = false;
                    chkRenalSizeEnlarge.Checked = false;
                }
                else if (recordRow["RENAL_SIZE"].ToString() == "2")
                {
                    chkRenalSizeEnlarge.Checked = true;
                    chkRenalSizeNormal.Checked = false;
                    chkRenalSizeReduce.Checked = false;
                }

                if (recordRow["STRUCTURE"].ToString() == "0")
                {
                    chkClear.Checked = true;
                    chkBlur.Checked = false;
                }
                else if (recordRow["STRUCTURE"].ToString() == "1")
                {
                    chkBlur.Checked = true;
                    chkClear.Checked = false;
                }

                if (recordRow["IMX"].ToString() == "1")
                {
                    chkIMX.Checked = true;
                }
                else
                {
                    chkIMX.Checked = false;
                }

                if (recordRow["HCV"].ToString() == "1")
                {
                    chkHCV.Checked = true;
                }
                else
                {
                    chkHCV.Checked = false;
                }

                if (recordRow["TPPA"].ToString() == "1")
                {
                    chkTPPA.Checked = true;
                }
                else
                {
                    chkTPPA.Checked = false;
                }

                if (recordRow["AIDS"].ToString() == "1")
                {
                    chkAIDS.Checked = true;
                }
                else
                {
                    chkAIDS.Checked = false;
                }

                if (recordRow["CLOTTING_MECHANISM"].ToString() == "0")
                {
                    chkCruorNormal.Checked = true;
                    chkCruorExtend.Checked = false;
                }
                else if (recordRow["CLOTTING_MECHANISM"].ToString() == "1")
                {
                    chkCruorExtend.Checked = true;
                    chkCruorNormal.Checked = false;
                }

                if (recordRow["TREATMENT_PLAN"].ToString() == "1")
                {
                    chkOneTime.Checked = true;
                    chkTwoTime.Checked = false;
                    chkThreeTime.Checked = false;
                }
                else if (recordRow["TREATMENT_PLAN"].ToString() == "2")
                {
                    chkTwoTime.Checked = true;
                    chkOneTime.Checked = false;
                    chkThreeTime.Checked = false;
                }
                else if (recordRow["TREATMENT_PLAN"].ToString() == "3")
                {
                    chkThreeTime.Checked = true;
                    chkOneTime.Checked = false;
                    chkTwoTime.Checked = false;
                }
            }
        }

        /// <summary>
        /// Detail打印前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            CreateTable();
            this.DataSource = objHemodialysisService.GetCureDrugForPatientRecord(_currentHemoId);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 创建表
        /// </summary>
        private void CreateTable()
        {
            XRTable table = this.xrTable1;
            table.Borders = (DevExpress.XtraPrinting.BorderSide)DevExpress.XtraPrinting.BorderSide.All;
            table.BorderWidth = 1;
            table.LocationFloat = new DevExpress.Utils.PointFloat(12.5F, 10F);
            table.Name = "xrTable";
            table.SizeF = new SizeF(729.5F, 25F);
            table.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular);
            table.StylePriority.UseBorders = false;
            table.StylePriority.UseTextAlignment = false;
            table.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            table.Rows.Clear();

            XRTableRow row = new XRTableRow();
            row.Name = "xrTableRow";
            row.Weight = 1D;

            table.Rows.AddRange(new XRTableRow[] { row });

            XRTableCell cellDate = new XRTableCell();
            cellDate.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellDate.Name = "xrTableCell0";
            cellDate.Weight = 0.75D;
            cellDate.StylePriority.UseBorders = false;
            cellDate.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "CREATE_DATE") });

            XRTableCell cellDrugName = new XRTableCell();
            cellDrugName.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellDrugName.Name = "xrTableCell1";
            cellDrugName.Weight = 0.75D;
            cellDrugName.StylePriority.UseBorders = false;
            cellDrugName.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "DRUG_NAME") });

            XRTableCell cellDosage = new XRTableCell();
            cellDosage.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellDosage.Name = "xrTableCell2";
            cellDosage.Weight = 0.75D;
            cellDosage.StylePriority.UseBorders = false;
            cellDosage.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "DOSAGE") });

            XRTableCell cellUnitName = new XRTableCell();
            cellUnitName.Borders = (DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right);
            cellUnitName.Name = "xrTableCell3";
            cellUnitName.Weight = 0.75D;
            cellUnitName.StylePriority.UseBorders = false;
            cellUnitName.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "UNIT_NAME") });

            row.Cells.AddRange(new XRTableCell[] { cellDate, cellDrugName, cellDosage, cellUnitName });
            row.Cells[0].WidthF = 132F;
            row.Cells[1].WidthF = 248.5F;
            row.Cells[2].WidthF = 120F;
            row.Cells[3].WidthF = 229F;
        }

        #endregion
    }
}
