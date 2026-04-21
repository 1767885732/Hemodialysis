/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者综合信息窗体类
// 创建时间：2016-6-28
// 创建者：刘超
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.Client.UI.Hemodialysis;
using Hemo.IService;
using Hemo.Service;
using Hemo.Client.Core;
using Hemo.IService.Config;
using Hemo.IService.PatientSchedule;
using Hemo.Client.Controls;
using Hemo.Client.UI.Patient;
using Hemo.Client.UI.Assessment;
using Hemo.Client.UI.ReportChart;
using Hemo.Client.UI.Lab;

namespace Hemo.Client.UI.PatientFixUI {
    public partial class PatientFixInfos : HemoBaseFrm {
        #region 变量

        private PatientModel.MED_PATIENTSRow _patientDocRow;
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;


        public PatientModel.MED_PATIENTSRow PatientDocRow {
            get { return _patientDocRow; }
            set { _patientDocRow = value; }
        }

        private string areaName;

        public string AreaName {
            get { return areaName; }
            set { areaName = value; }
        }

        private string openTab;
        public string OpenTab {
            get {
                return openTab;
            }
            set {
                openTab = value;
            }
        }

        public DateTime currentDt { get; set; }
        #endregion

        #region 构造函数

        public PatientFixInfos()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// xtraTabControl1选项页面改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page.Text == "基本信息")
            {
                if (this.xtraTabPage5.Controls.Count > 0)
                    return;
                PatientInfoUI frm = new PatientInfoUI();
                frm.Current = _patientDocRow;
                frm.InitalizeData();
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage5.Controls.Add(frm);
            }
            else if (e.Page.Text == "患者病历")
            {
                if (this.xtraTabPage2.Controls.Count > 0)
                    return;
                PatientBaseRecordUI frm = new PatientBaseRecordUI();
                frm.HemoId = _patientDocRow.HEMODIALYSIS_ID;
                frm.HemoBeginDate = Utilities.Utility.CDate(_patientDocRow.SPECIFIC_TIME.ToString());
                frm.Dock = DockStyle.Fill;

                this.xtraTabPage2.Controls.Add(frm);
                frm.SetBaseRecordSize();
            }
            else if (e.Page.Text == "病程记录")
            {
                if (this.xtraTabPage3.Controls.Count > 0)
                    return;
                PatientProgressNoteUI frm = new PatientProgressNoteUI(_patientDocRow.HEMODIALYSIS_ID);
                frm.Dock = DockStyle.Fill;

                this.xtraTabPage3.Controls.Add(frm);
            }
            else if (e.Page.Text == "透析方案")
            {
                if (this.xtraTabPage4.Controls.Count > 0)
                    return;
                QueryRecipeListUI frm = new QueryRecipeListUI(_patientDocRow.HEMODIALYSIS_ID, 0);
                frm.CurrentDt = currentDt;
                frm.Dock = DockStyle.Fill;

                this.xtraTabPage4.Controls.Add(frm);
            }
            else if (e.Page.Text == "透析记录")
            {
                if (this.xtraTabPage1.Controls.Count > 0)
                    return;
                PatientKnowBooksUI frm = new PatientKnowBooksUI();
                frm.BindDocTree(PatientDocRow);
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage1.Controls.Add(frm);
            }
            else if (e.Page.Text == "检验检查")
            {
                ExamLabItemUINew frm = new ExamLabItemUINew();
                frm.HemoId = _patientDocRow.HEMODIALYSIS_ID;
                frm.PatientName = _patientDocRow.NAME;
                frm.CurrentParent = null;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage22.Controls.Add(frm);
            }
            else if (e.Page.Text == "血管通路")
            {
                if (this.xtraTabPage7.Controls.Count > 0)
                    return;
                EditVascularAccessUI frm = new EditVascularAccessUI();
                frm.LoadData(_patientDocRow.HEMODIALYSIS_ID);
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage7.Controls.Add(frm);
            }
            else if (e.Page.Text == "评估宣教")
            {
                if (this.xtraTabPage9.Controls.Count > 0)
                    return;
                CtlPatientSufficiency frm = new CtlPatientSufficiency();
                frm.CurrentPatient = _patientDocRow;
                frm.HemoId = _patientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage9.Controls.Add(frm);
            }
            else if (e.Page.Text == "医嘱查询")
            {
                if (this.xtraTabPage19.Controls.Count > 0)
                    return;
                QueryDrugRecordUI frm = new QueryDrugRecordUI();
                frm.HemodialysisID = _patientDocRow.HEMODIALYSIS_ID;
                frm.SearchAndBind();
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage19.Controls.Add(frm);
            }
            else if (e.Page.Text == "透析变化")
            {
                if (this.xtraTabPage20.Controls.Count > 0)
                    return;
                PatientRecipeFrm frm = new PatientRecipeFrm();
                frm.HemoId = _patientDocRow.HEMODIALYSIS_ID;
                frm.InzationDateControl();
                frm.InzationData();
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage20.Controls.Add(frm);
            }
            else if (e.Page.Text == "抢救记录")
            {
                if (this.xtraTabPage21.Controls.Count > 0)
                    return;
                EmrgeRecordForDocUI frm = new EmrgeRecordForDocUI();
                string _strCureID = _hemodialysisService.GetCureID(System.DateTime.Now.ToShortDateString(), _patientDocRow.HEMODIALYSIS_ID);
                frm.patientHemoId = _patientDocRow.HEMODIALYSIS_ID;
                if (!string.IsNullOrEmpty(_strCureID))
                {
                    var date = _hemodialysisService.GetMainCureByCureID(_strCureID);
                    if (date != null && date.Rows.Count > 0)
                    {
                        frm._cureRow = date[0];
                    }
                }
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage21.Controls.Add(frm);
            }
        }

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page.Text == "透析充分性评估")
            {
                if (this.xtraTabPage10.Controls.Count > 0)
                    return;
                PatientSufficiencyUI frm = new PatientSufficiencyUI();
                frm.CurrentHemoId = _patientDocRow.HEMODIALYSIS_ID;
                frm.CurrentHemoName = _patientDocRow.NAME;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage10.Controls.Add(frm);
            }
            else if (e.Page.Text == "营养评估")
            {
                if (this.xtraTabPage11.Controls.Count > 0)
                    return;
                NutritionSGAUI frm = new NutritionSGAUI();
                frm.CurrentHemoId = _patientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage11.Controls.Add(frm);
            }
            else if (e.Page.Text == "风险评估")
            {
                if (this.xtraTabPage12.Controls.Count > 0)
                    return;
                RiskAssessUI frm = new RiskAssessUI();
                //frm.CurrentHemoId = _patientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage12.Controls.Add(frm);
            }
            else if (e.Page.Text == "内瘘评估")
            {
                QueryEstimateInBasketUI frm = new QueryEstimateInBasketUI();
                frm.HemoID = _patientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage13.Controls.Add(frm);
                frm.queryData(_patientDocRow.HEMODIALYSIS_ID);
            }
            else if (e.Page.Text == "临时导管评估")
            {
                QueryEstimateVenousListUI frm = new QueryEstimateVenousListUI();
                frm.IsTemp = true;
                frm.HemoId = _patientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage14.Controls.Add(frm);
                frm.QueryData();
            }
            else if (e.Page.Text == "长期导管评估")
            {
                QueryEstimateVenousListUI frm = new QueryEstimateVenousListUI();
                frm.IsTemp = false;
                frm.HemoId = _patientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage15.Controls.Add(frm);
                frm.QueryData();
            }
            else if (e.Page.Text == "Kolcaba评估")
            {
                PatientKolcabaUI frm = new PatientKolcabaUI();
                frm.CurrentHemoId = _patientDocRow.HEMODIALYSIS_ID;
                frm.CurrentHemoName = _patientDocRow.NAME;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage16.Controls.Add(frm);
            }
            else if (e.Page.Text == "透析综合评估")
            {
                AssessmentListUI frm = new AssessmentListUI();
                frm.HemoID = _patientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage17.Controls.Add(frm);
            }
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientFixInfos_Load(object sender, EventArgs e)
        {
            this.Text = "患者综合信息";
            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountFrm(this);
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            PatientKnowBooksUI frm = new PatientKnowBooksUI();
            frm.BindDocTree(PatientDocRow);
            frm.Dock = DockStyle.Fill;
            this.xtraTabPage1.Controls.Add(frm);
            loadPatientInfo(PatientDocRow);
        }

        private void PatientFixInfos_Resize(object sender, EventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                (this.xtraTabPage2.Controls[0] as PatientBaseRecordUI).SetBaseRecordSize();
            }
        }

        /// <summary>
        /// 通过患者透析ID载入患者基本信息并切换全部已打开界面
        /// </summary>
        /// <param name="pHemoID"></param>
        private void changeAllControlByPatientInfo(string pHemoID)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string HemoId = string.Empty;

            if (this.xtraTabPage1.Controls.Count > 0)
            {
                //   PatientKnowBooksUI frm = new PatientKnowooksUI();
                if (PatientDocRow.HEMODIALYSIS_ID == "10000022")
                {
                    (this.xtraTabPage1.Controls[0] as PatientKnowBooksUI).BindDocTree(PatientDocRow);
                    loadPatientInfo(PatientDocRow);
                    PatientModel.MED_PATIENTSDataTable medPatient = objPatient.GetPatientListByParams("", "10000119");
                    PatientDocRow = medPatient.Rows[0] as PatientModel.MED_PATIENTSRow;
                }
                else if (PatientDocRow.HEMODIALYSIS_ID == "10000119")
                {
                    (this.xtraTabPage1.Controls[0] as PatientKnowBooksUI).BindDocTree(PatientDocRow);
                    loadPatientInfo(PatientDocRow);
                    PatientModel.MED_PATIENTSDataTable medPatient = objPatient.GetPatientListByParams("", "10000022");
                    PatientDocRow = medPatient.Rows[0] as PatientModel.MED_PATIENTSRow;
                }
            }
        }

        private void xtraTabControl3_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page.Text == "肾科检验")
            {
                if (this.xtraTabPage22.Controls.Count > 0)
                    return;
                ExamLabItemUINew frm = new ExamLabItemUINew();
                frm.HemoId = _patientDocRow.HEMODIALYSIS_ID;
                frm.PatientName = _patientDocRow.NAME;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage22.Controls.Add(frm);
            }
            else if (e.Page.Text == "患者检验")
            {
                if (this.xtraTabPage23.Controls.Count > 0)
                    return;
                ctlLabFrm labFrm = new ctlLabFrm(_patientDocRow);
                labFrm.Dock = DockStyle.Fill;
                this.xtraTabPage23.Controls.Add(labFrm);
            }
            else if (e.Page.Text == "患者检查")
            {
                if (this.xtraTabPage24.Controls.Count > 0)
                    return;
                ctlExamFrm frm = new ctlExamFrm();
                frm.PatientRow = _patientDocRow;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage24.Controls.Add(frm);
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载患者信息
        /// </summary>
        /// <param name="pPatientDocRow"></param>
        private void loadPatientInfo(PatientModel.MED_PATIENTSRow pPatientDocRow)
        {
            lblName.Text = pPatientDocRow.NAME;
            lblSex.Text = pPatientDocRow.SEX;
            lblAge.Text = Utilities.Utility.GetAge(pPatientDocRow.BIRTHDAY.ToShortDateString()).ToString();
            lblHemoID.Text = pPatientDocRow.HEMODIALYSIS_ID;
            //lblHosptialNo.Text = pPatientDocRow.ADMISSION_NUMBER;
            lblFrom.Text = pPatientDocRow.TIME_TYPE;
            lblSPECIFIC_TIME.Text = Utilities.Utility.CDate(pPatientDocRow.SPECIFIC_TIME.ToShortDateString()).ToShortDateString();
            //lblPatientID.Text = pPatientDocRow.PATIENT_ID;
            var patientPicDt = objPatient.GetPatientPicByHemoId(pPatientDocRow.HEMODIALYSIS_ID);
            if (patientPicDt != null && patientPicDt.Rows.Count > 0)
            {
                picMain.Image = ImageHelper.ConvertByteToImage(patientPicDt[0].PAT_PIC);
            }
            lblPatientType.Text = _hemodialysisService.GetCureTypeByHemoId(pPatientDocRow.HEMODIALYSIS_ID);
            lblPatientSch.Text = _patientScheduleService.GetCurrentScheduleInfoByHemoId(pPatientDocRow.HEMODIALYSIS_ID);

        }

        #endregion    
    }
}