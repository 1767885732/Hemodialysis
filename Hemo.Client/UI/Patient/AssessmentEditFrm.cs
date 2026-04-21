/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者透析评估单
// 创建时间：2015-05-15
// 创建者：刘超
//  
// 修改时间：2015-05-26
// 修改人：贺建操
// 修改描述：修改界面及部分业务逻辑
//
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.Client.Print;
using Hemo.IService;
using Hemo.Client.Core;
using Hemo.Model;
using System.Drawing.Printing;

namespace Hemo.Client.UI.Patient {
    public partial class AssessmentEditFrm : HemoBaseFrm {
        #region 类变量

        private HemoModel.MED_ASSESSMENTMASTERDataTable medAssessTable;
        private IConfig _configService = ServiceManager.Instance.ConfigService;

        private HemoModel.MED_ASSESSMENTMASTERRow medAssessRow;
        private PatientModel.MED_PATIENTSRow patient = null;

        private bool isAdd;
        public HemodialysisModel.MED_HEMO_RECIPEDataTable recipeDt = null;
        private IHemodialysis hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IPatient objPatient = ServiceManager.Instance.PatientService;

        #endregion

        #region 属性

        public string AssessmentID { get; set; }
        public string HemoID { get; set; }

        #endregion

        #region 构造函数

        public AssessmentEditFrm(HemoModel.MED_ASSESSMENTMASTERDataTable table, HemoModel.MED_ASSESSMENTMASTERRow row, PatientModel.MED_PATIENTSRow patientRow)
        {
            InitializeComponent();
            this.patient = patientRow;
            if (table == null)
                table = new HemoModel.MED_ASSESSMENTMASTERDataTable();
            this.medAssessTable = table;
            if (row == null)
            {
                this.isAdd = true;
                this.medAssessRow = this.medAssessTable.NewMED_ASSESSMENTMASTERRow();
                this.medAssessRow.control0 = patient.NAME;
                this.medAssessRow.control1 = patient.SEX == "男" ? "0" : "1";
                this.medAssessRow.control2 = patient.AGE.ToString();
                this.medAssessRow.control3 = System.DateTime.Now.Date.ToString("yyyy-MM-dd");


            }
            else
            {
                this.isAdd = false;
                this.medAssessRow = row;
            }

            labelHostName.Text = Utility.GetHospitalName();
        }

        #endregion

        #region 事件

        private void AssessmentListFrm_Load(object sender, EventArgs e)
        {

            BaseControlInfo.BindLookUpEdit(cmbFIRST_PURIFIER_MODEL, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "净化器类型", "1"), "ITEM_NAME", "净化器类型");


            if (this.medAssessRow == null)
            {
                control53.Checked = true;
                return;
            }
            if (this.isAdd && recipeDt != null && recipeDt.Rows.Count > 0)
            {
                this.medAssessRow.control14 = recipeDt[0].IsUFRNull() ? string.Empty : recipeDt[0].UFR.ToString();
                this.medAssessRow.control15 = recipeDt[0].IsTODAY_WEIGHTNull() ? string.Empty : recipeDt[0].TODAY_WEIGHT.ToString();
                this.medAssessRow.control17 = string.Format("{0}/{1}", recipeDt[0].IsTODAY_BLOODANull() ? string.Empty : recipeDt[0].TODAY_BLOODA.ToString(), recipeDt[0].IsTODAY_BLOODBNull() ? string.Empty : recipeDt[0].TODAY_BLOODB.ToString());
                if (!recipeDt[0].IsPURIFICATION_MODENull())
                {
                    if (recipeDt[0].PURIFICATION_MODE == "HD")
                    {
                        this.medAssessRow.control5 = recipeDt[0].IsFREQUENCY_TIMESNull() ? string.Empty : recipeDt[0].FREQUENCY_TIMES.ToString();
                        this.medAssessRow.control6 = recipeDt[0].IsFREQUENCY_HOURSNull() ? string.Empty : recipeDt[0].FREQUENCY_HOURS.ToString();
                    }
                    else if (recipeDt[0].PURIFICATION_MODE == "HDF")
                    {
                        this.medAssessRow.control9 = recipeDt[0].IsFREQUENCY_TIMESNull() ? string.Empty : recipeDt[0].FREQUENCY_TIMES.ToString();
                        this.medAssessRow.control10 = recipeDt[0].IsFREQUENCY_HOURSNull() ? string.Empty : recipeDt[0].FREQUENCY_HOURS.ToString();
                    }
                    else if (recipeDt[0].PURIFICATION_MODE == "HF")
                    {
                        this.medAssessRow.control7 = recipeDt[0].IsFREQUENCY_TIMESNull() ? string.Empty : recipeDt[0].FREQUENCY_TIMES.ToString();
                        this.medAssessRow.control8 = recipeDt[0].IsFREQUENCY_HOURSNull() ? string.Empty : recipeDt[0].FREQUENCY_HOURS.ToString();
                    }
                    else if (recipeDt[0].PURIFICATION_MODE == "HP")
                    {
                        this.medAssessRow.control11 = recipeDt[0].IsFREQUENCY_TIMESNull() ? string.Empty : recipeDt[0].FREQUENCY_TIMES.ToString();
                        this.medAssessRow.control12 = recipeDt[0].IsFREQUENCY_HOURSNull() ? string.Empty : recipeDt[0].FREQUENCY_HOURS.ToString();
                    }
                }
                if (!recipeDt[0].IsTHERAPEUTIC_METHODNull())
                {
                    if (recipeDt[0].THERAPEUTIC_METHOD == "普通肝素抗凝")
                    {
                        this.medAssessRow.control25 = string.Format("{0}{1}", recipeDt[0].IsFIRST_DRUG_DOSAGENull() ? string.Empty : recipeDt[0].FIRST_DRUG_DOSAGE, recipeDt[0].IsFIRST_DRUG_UNITNull() ? string.Empty : recipeDt[0].FIRST_DRUG_UNIT);

                    }
                    else if (recipeDt[0].THERAPEUTIC_METHOD == "低分子肝素抗凝")
                    {
                        this.medAssessRow.control27 = string.Format("{0}{1}", recipeDt[0].IsFIRST_DRUG_DOSAGENull() ? string.Empty : recipeDt[0].FIRST_DRUG_DOSAGE, recipeDt[0].IsFIRST_DRUG_UNITNull() ? string.Empty : recipeDt[0].FIRST_DRUG_UNIT);
                    }
                    else if (recipeDt[0].THERAPEUTIC_METHOD == "无肝素透析")
                    {
                        this.medAssessRow.control32 = string.Format("{0}{1}", recipeDt[0].IsFIRST_DRUG_DOSAGENull() ? string.Empty : recipeDt[0].FIRST_DRUG_DOSAGE, recipeDt[0].IsFIRST_DRUG_UNITNull() ? string.Empty : recipeDt[0].FIRST_DRUG_UNIT);
                    }
                }

            }
            if (!string.IsNullOrEmpty(HemoID))
            {
                BindData(this.printDocumentPanel1 as DevExpress.XtraEditors.PanelControl);
            }
            //Rectangle rect = new Rectangle();
            //rect = Screen.GetWorkingArea(this);
            ////rect.Width;//屏幕宽
            ////rect.Height;//屏幕高
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            ControlPrint frm = new ControlPrint(this.printDocumentPanel1);
            frm.ApplyBestSize();
            printPreviewDialog1.Document = (PrintDocument)frm;

            printPreviewDialog1.ShowDialog();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.isAdd)
            {
                this.medAssessRow.ASSESSMENT_ID = Guid.NewGuid().ToString();
                this.medAssessRow.ASSESSMENT_DATE = System.DateTime.Now.Date;
                this.medAssessRow.ASSESSMENT_TYPE = "月度透析";
                this.medAssessRow.ASSESSMENT_NOTE = HemoApplicationContext.Current.CurrentUser.USER_NAME + this.medAssessRow.ASSESSMENT_ID;
                this.medAssessRow.HEMODIALYSIS_ID = patient.HEMODIALYSIS_ID;
                this.medAssessRow.CREATE_USER = HemoApplicationContext.Current.CurrentUser.USER_ID;
                this.medAssessTable.AddMED_ASSESSMENTMASTERRow(this.medAssessRow);
            }

            if (this.hemodialysisService.SaveAssessmentByDate(this.medAssessTable) == 1)
            {
                AutoClosedMsgBox.ShowForm("保存成功！", "系统提示", 1000, MessageBoxIcon.Warning);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                AutoClosedMsgBox.ShowForm("保存失败！", "系统提示", 1000, MessageBoxIcon.Warning);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void control23_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void control53_CheckedChanged(object sender, EventArgs e)
        {
            if (control53.Checked)
            {
                this.panelControl28.Visible = true;
                this.panelControl29.Visible = true;
                this.panelControl30.Visible = true;
                this.panelControl31.Visible = true;
            }
            else
            {
                this.panelControl28.Visible = false;
                this.panelControl29.Visible = false;
                this.panelControl30.Visible = false;
                this.panelControl31.Visible = false;
            }
        }

        #endregion

        #region 方法

        private void BindData(DevExpress.XtraEditors.PanelControl panel)
        {
            foreach (var ctl in panel.Controls)
            {
                if (ctl is BaseEdit)
                {
                    (ctl as BaseEdit).BindingAssessDataRow(this.medAssessRow);
                }
                else if (ctl is DevExpress.XtraEditors.PanelControl)
                {
                    BindData(ctl as DevExpress.XtraEditors.PanelControl);
                }
                else if (ctl is CheckedListBoxControl)
                {
                    (ctl as CheckedListBoxControl).BindingCheckedDataRow(this.medAssessRow);
                }
                else if (ctl is System.Windows.Forms.TableLayoutPanel)
                {
                    BindTableLayoutData(ctl as System.Windows.Forms.TableLayoutPanel);
                }
                //else if (ctl is LookUpEdit)
                //{
                //    (ctl as LookUpEdit).BindingLookUpEditDataRow(this.medAssessRow);
                //}
            }
        }

        private void BindTableLayoutData(System.Windows.Forms.TableLayoutPanel tPanel)
        {
            foreach (var tctl in tPanel.Controls)
            {
                if (tctl is BaseEdit)
                {
                    (tctl as BaseEdit).BindingAssessDataRow(this.medAssessRow);
                }
                else if (tctl is DevExpress.XtraEditors.PanelControl)
                {
                    BindData(tctl as DevExpress.XtraEditors.PanelControl);
                }
                else if (tctl is CheckedListBoxControl)
                {
                    (tctl as CheckedListBoxControl).BindingCheckedDataRow(this.medAssessRow);
                }
                else if (tctl is System.Windows.Forms.TableLayoutPanel)
                {
                    BindTableLayoutData(tctl as System.Windows.Forms.TableLayoutPanel);
                }

            }
        }

        #endregion
    }
}