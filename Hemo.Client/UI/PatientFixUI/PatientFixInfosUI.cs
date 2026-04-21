/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：新患者综合信息窗体类
// 创建时间：2017-3-28
// 创建者：贺建操
//  
// 修改时间：2017-04-22
// 修改人：吕志强
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
using Hemo.Client.UI.Machine;
using Hemo.Client.UI.PatientFixUI;
using Hemo.Client.UI.Lab;
using Hemo.Client.Base.XtraBaseInfo;

namespace Hemo.Client.UI.PatientFixUI
{
    public partial class PatientFixInfosUI : ViewBase
    {
        #region 变量及属性

        private PatientModel.MED_PATIENTSRow _patientDocRow;

        private IPatient objPatient = ServiceManager.Instance.PatientService;

        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;

        private bool isPageChanged = false;

        private string labFromTime = string.Empty;

        public string LabFromTime
        {
            get { return labFromTime; }
            set { labFromTime = value; }
        }

        private string labToTime = string.Empty;

        public string LabToTime
        {
            get { return labToTime; }
            set { labToTime = value; }
        }

        private string examFromTime = string.Empty;

        public string ExamFromTime
        {
            get { return examFromTime; }
            set { examFromTime = value; }
        }

        private string examToTime = string.Empty;

        public string ExamToTime
        {
            get { return examToTime; }
            set { examToTime = value; }
        }

        private string currentExamLabItem = string.Empty;

        public string CurrentExamLabItem
        {
            get { return currentExamLabItem; }
            set { currentExamLabItem = value; }
        }

        public PatientModel.MED_PATIENTSRow PatientDocRow
        {
            get { return _patientDocRow; }
            set { _patientDocRow = value; }
        }

        private string areaName;

        public string AreaName
        {
            get { return areaName; }
            set { areaName = value; }
        }

        private string openTab;

        public string OpenTab
        {
            get
            {
                return openTab;
            }
            set
            {
                openTab = value;
            }
        }

        private DateTime currentDate;

        public DateTime CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; }
        }

        public ModuleType CurrentUI { get; set; }

        #endregion

        #region 构造函数

        public PatientFixInfosUI() {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// xtraTabControl1页面改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e) {
            _patientDocRow = objPatient.GetPatientListByParams(string.Empty, _patientDocRow.HEMODIALYSIS_ID)[0];
            isPageChanged = true;
            ChangeAllControlByPatientInfoTab_1(_patientDocRow);
            isPageChanged = false;
        }

        /// <summary>
        /// xtraTabControl2页面改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e) {

            ChangeAllControlByPatientInfoTab_2(_patientDocRow);
        }

        private void simpleButton1_Click(object sender, EventArgs e) {

        }

        /// <summary>
        /// xtraTabControl3页面改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControl3_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e) {
            if (e.Page.Text == "肾科检验") {
                if (this.xtraTabPage26.Controls.Count > 0)
                    return;
                ExamLabItemUINew frm = new ExamLabItemUINew();
                frm.HemoId = _patientDocRow.HEMODIALYSIS_ID;
                frm.PatientName = _patientDocRow.NAME;
                frm.CurrentParent = this;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage26.Controls.Add(frm);
            }
            else if (e.Page.Text == "患者检验") {
                if (this.xtraTabPage27.Controls.Count > 0)
                    return;
                ctlLabFrm labFrm = new ctlLabFrm(_patientDocRow);
                labFrm.CurrentParent = this;
                labFrm.Dock = DockStyle.Fill;
                this.xtraTabPage27.Controls.Add(labFrm);
            }
            else if (e.Page.Text == "患者检查") {
                if (this.xtraTabPage28.Controls.Count > 0)
                    return;
                ctlExamFrm frm = new ctlExamFrm();
                frm.PatientRow = _patientDocRow;
                frm.CurrentParent = this;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage28.Controls.Add(frm);
            }
        }

        private void PatientFixInfos_Resize(object sender, EventArgs e) {
            if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage2) {
                (this.xtraTabPage2.Controls[0] as PatientBaseRecordUI).SetBaseRecordSize();
            }
        }

        /// <summary>
        /// 通过患者透析ID载入患者基本信息并切换全部已打开界面
        /// </summary>
        /// <param name="pHemoID"></param>
        public void ChangeAllControlByPatientInfoTab_1(PatientModel.MED_PATIENTSRow PatientDocRow) {
            //切换患者综合一览
            if (this.xtraTabPage22.Controls.Count > 0 && this.xtraTabControl1.SelectedTabPage == this.xtraTabPage22) {
                (this.xtraTabPage22.Controls[0] as PatientAllViewUI).CurrentHemoId = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage22.Controls[0] as PatientAllViewUI).InzationData();
            }
            //切换基本信息
            if (this.xtraTabPage5.Controls.Count > 0 && this.xtraTabControl1.SelectedTabPage == this.xtraTabPage5) {
                (this.xtraTabPage5.Controls[0] as PatientInfoUI).Current = PatientDocRow;
                (this.xtraTabPage5.Controls[0] as PatientInfoUI).SetBtnCloseHide();

                (this.xtraTabPage5.Controls[0] as PatientInfoUI).InitalizeData();
            }
            else if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage5) {
                PatientInfoUI frm = new PatientInfoUI();
                frm.SetBtnCloseHide();
                frm.Current = _patientDocRow;
                frm.InitalizeData();
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage5.Controls.Add(frm);
                return;
            }
            //切换患者病历
            if (this.xtraTabPage2.Controls.Count > 0 && this.xtraTabControl1.SelectedTabPage == this.xtraTabPage2) {
                (this.xtraTabPage2.Controls[0] as PatientBaseRecordUI).HemoId = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage2.Controls[0] as PatientBaseRecordUI).HemoBeginDate = Utilities.Utility.CDate(PatientDocRow.SPECIFIC_TIME.ToString());
                (this.xtraTabPage2.Controls[0] as PatientBaseRecordUI).LoadInfo();
                (this.xtraTabPage2.Controls[0] as PatientBaseRecordUI).SetBaseRecordSize();
            }
            else if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage2) {
                PatientBaseRecordUI frm = new PatientBaseRecordUI();
                frm.HemoId = _patientDocRow.HEMODIALYSIS_ID;
                frm.HemoBeginDate = Utilities.Utility.CDate(_patientDocRow.SPECIFIC_TIME.ToString());
                frm.LoadInfo();
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage2.Controls.Add(frm);
                frm.SetBaseRecordSize();
                return;
            }
            //切换病程记录
            if (this.xtraTabPage3.Controls.Count > 0 && this.xtraTabControl1.SelectedTabPage == this.xtraTabPage3) {
                (this.xtraTabPage3.Controls[0] as PatientProgressNoteUI).CurrentHemoId = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage3.Controls[0] as PatientProgressNoteUI).LoadInfo();
            }
            else if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage3) {
                PatientProgressNoteUI frm = new PatientProgressNoteUI(_patientDocRow.HEMODIALYSIS_ID);
                frm.CurrentHemoId = _patientDocRow.HEMODIALYSIS_ID;
                frm.LoadInfo();
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage3.Controls.Add(frm);
                return;
            }
            //切换透析方案
            if (this.xtraTabPage4.Controls.Count > 0 && this.xtraTabControl1.SelectedTabPage == this.xtraTabPage4) {
                int index = CurrentUI.Equals(ModuleType.TempDrug) ? 1 : 0;
                (this.xtraTabPage4.Controls[0] as QueryRecipeListUI).LoadInfo(PatientDocRow.HEMODIALYSIS_ID, index);
            }
            else if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage4) {
                var dtRecipe = _hemodialysisService.GetRecipeByHemodialysisIDAndDate(_patientDocRow.HEMODIALYSIS_ID, currentDate);
                string recipeId = (dtRecipe != null && dtRecipe.Rows.Count > 0) ? dtRecipe[0].RECIPE_ID : string.Empty;
                int index = CurrentUI.Equals(ModuleType.TempDrug) ? 1 : 0;
                QueryRecipeListUI frm = new QueryRecipeListUI(_patientDocRow.HEMODIALYSIS_ID, index);
                frm.currentRecipeIdStr = recipeId;
                frm.CurrentDt = currentDate;
                frm.AreaName = this.areaName;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage4.Controls.Add(frm);
                return;
            }
            //切换透析记录
            if (this.xtraTabPage1.Controls.Count > 0 && this.xtraTabControl1.SelectedTabPage == this.xtraTabPage1) {
                this.xtraTabPage1.Controls.Clear();
                PatientKnowBooksUI frm = new PatientKnowBooksUI();
                frm.BindDocTree(PatientDocRow);
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage1.Controls.Add(frm);
            }
            else if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage1) {
                PatientKnowBooksUI frm = new PatientKnowBooksUI();
                frm.BindDocTree(PatientDocRow);
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage1.Controls.Add(frm);
                return;
            }
            //切换检验检查
            if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage6) {
                if (isPageChanged) {
                    xtraTabControl3_SelectedPageChanged(this.xtraTabControl3, new DevExpress.XtraTab.TabPageChangedEventArgs(null, this.xtraTabControl3.SelectedTabPage));
                }
                else {
                    //患者切换，刷新界面
                    if (this.xtraTabControl3.TabPages[0].Controls.Count > 0) {
                        (this.xtraTabControl3.TabPages[0].Controls[0] as ExamLabItemUINew).HemoId = _patientDocRow.HEMODIALYSIS_ID;
                        (this.xtraTabControl3.TabPages[0].Controls[0] as ExamLabItemUINew).PatientName = _patientDocRow.NAME;
                        (this.xtraTabControl3.TabPages[0].Controls[0] as ExamLabItemUINew).LoadResultList();
                    }
                    if (this.xtraTabControl3.TabPages[1].Controls.Count > 0) {
                        (this.xtraTabControl3.TabPages[1].Controls[0] as ctlLabFrm).LoadLabInfo(_patientDocRow);
                    }
                    if (this.xtraTabControl3.TabPages[2].Controls.Count > 0) {
                        (this.xtraTabControl3.TabPages[2].Controls[0] as ctlExamFrm).PatientRow = _patientDocRow;
                        (this.xtraTabControl3.TabPages[2].Controls[0] as ctlExamFrm).LoadExamInfo();
                    }
                }
            }
            //切换血管通路 
            if (this.xtraTabPage7.Controls.Count > 0 && this.xtraTabControl1.SelectedTabPage == this.xtraTabPage7) {
                (this.xtraTabPage7.Controls[0] as EditVascularAccessUI).LoadData(PatientDocRow.HEMODIALYSIS_ID);
            }
            else if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage7) {
                EditVascularAccessUI frm = new EditVascularAccessUI();
                frm.LoadData(PatientDocRow.HEMODIALYSIS_ID);
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage7.Controls.Add(frm);
                return;
            }
            //切换患者医嘱查询
            if (this.xtraTabPage19.Controls.Count > 0 && this.xtraTabControl1.SelectedTabPage == this.xtraTabPage19) {
                (this.xtraTabPage19.Controls[0] as QueryDrugRecordUI).HemodialysisID = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage19.Controls[0] as QueryDrugRecordUI).SearchAndBind();
            }
            else if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage19) {
                QueryDrugRecordUI frm = new QueryDrugRecordUI();
                frm.HemodialysisID = _patientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage19.Controls.Add(frm);
                frm.SearchAndBind();
                return;
            }
            ////切换患者透析变化
            //if (this.xtraTabPage20.Controls.Count > 0 && this.xtraTabControl1.SelectedTabPage == this.xtraTabPage20) {
            //    (this.xtraTabPage20.Controls[0] as PatientRecipeFrm).HemoId = PatientDocRow.HEMODIALYSIS_ID;
            //    (this.xtraTabPage20.Controls[0] as PatientRecipeFrm).InzationDateControl();
            //    (this.xtraTabPage20.Controls[0] as PatientRecipeFrm).InzationData();
            //}
            //else if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage20) {

            //    PatientRecipeFrm frm = new PatientRecipeFrm();
            //    frm.HemoId = _patientDocRow.HEMODIALYSIS_ID;
            //    frm.InzationDateControl();
            //    frm.Dock = DockStyle.Fill;
            //    this.xtraTabPage20.Controls.Add(frm);
            //    frm.InzationData();
            //    return;

            //}
            //切换患者抢救记录
            if (this.xtraTabPage21.Controls.Count > 0 && this.xtraTabControl1.SelectedTabPage == this.xtraTabPage21) {
                string _strCureID = _hemodialysisService.GetCureID(currentDate.ToShortDateString(), PatientDocRow.HEMODIALYSIS_ID);
                (this.xtraTabPage21.Controls[0] as EmrgeRecordForDocUI).patientHemoId = PatientDocRow.HEMODIALYSIS_ID;

                if (!string.IsNullOrEmpty(_strCureID)) {
                    var date = _hemodialysisService.GetMainCureByCureID(_strCureID);
                    if (date != null && date.Rows.Count > 0) {
                        (this.xtraTabPage21.Controls[0] as EmrgeRecordForDocUI)._cureRow = date[0];
                    }
                }
                (this.xtraTabPage21.Controls[0] as EmrgeRecordForDocUI).LoadInfo();
            }
            else if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage21) {
                EmrgeRecordForDocUI frm = new EmrgeRecordForDocUI();
                string _strCureID = _hemodialysisService.GetCureID(currentDate.ToShortDateString(), _patientDocRow.HEMODIALYSIS_ID);
                frm.patientHemoId = _patientDocRow.HEMODIALYSIS_ID;
                if (!string.IsNullOrEmpty(_strCureID)) {
                    var date = _hemodialysisService.GetMainCureByCureID(_strCureID);
                    if (date != null && date.Rows.Count > 0) {
                        frm._cureRow = date[0];
                    }
                }
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage21.Controls.Add(frm);
                frm.LoadInfo();
                return;
            }
            if (this.xtraTabPage23.Controls.Count > 0 && this.xtraTabControl1.SelectedTabPage == this.xtraTabPage23) {
                (this.xtraTabPage23.Controls[0] as RegOfDealthFrm).currentHemoId = _patientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage23.Controls[0] as RegOfDealthFrm).InzationData();
                //(this.xtraTabPage23.Controls[0] as RegDealthQueryFrm).LoadInfo();
            }
            else if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage23) {
                RegOfDealthFrm frm = new RegOfDealthFrm();
                frm.currentHemoId = _patientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage23.Controls.Add(frm);
                frm.InzationData();
                return;
            }
            if (this.xtraTabPage30.Controls.Count > 0 && this.xtraTabControl1.SelectedTabPage == this.xtraTabPage30) {
                (this.xtraTabPage30.Controls[0] as PatTransferHandoverUI).HemoId = _patientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage30.Controls[0] as PatTransferHandoverUI).QueryData();
            }
            else if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage30) {
                PatTransferHandoverUI frm = new PatTransferHandoverUI();
                frm.HemoId = _patientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage30.Controls.Add(frm);
                frm.QueryData();
                return;
            }
        }

        public void ChangeAllControlByPatientInfoTab_2(PatientModel.MED_PATIENTSRow PatientDocRow) {
            //切换评估宣教
            if (this.xtraTabPage9.Controls.Count > 0 && this.xtraTabControl2.SelectedTabPage == this.xtraTabPage9) {
                (this.xtraTabPage9.Controls[0] as CtlPatientSufficiency).CurrentPatient = PatientDocRow;
                (this.xtraTabPage9.Controls[0] as CtlPatientSufficiency).HemoId = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage9.Controls[0] as CtlPatientSufficiency).Query();
            }
            else if (this.xtraTabControl2.SelectedTabPage == this.xtraTabPage9) {

                CtlPatientSufficiency frm = new CtlPatientSufficiency();
                frm.CurrentPatient = _patientDocRow;
                frm.HemoId = _patientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage9.Controls.Add(frm);
                frm.Query();
                return;

            }
            //切换患者透析充分性评估
            if (this.xtraTabPage10.Controls.Count > 0 && this.xtraTabControl2.SelectedTabPage == this.xtraTabPage10) {
                (this.xtraTabPage10.Controls[0] as PatientSufficiencyUI).CurrentHemoId = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage10.Controls[0] as PatientSufficiencyUI).CurrentHemoName = PatientDocRow.NAME;
                (this.xtraTabPage10.Controls[0] as PatientSufficiencyUI).LoadInfo();
            }
            else if (this.xtraTabControl2.SelectedTabPage == this.xtraTabPage10) {
                PatientSufficiencyUI frm = new PatientSufficiencyUI();
                frm.CurrentHemoId = _patientDocRow.HEMODIALYSIS_ID;
                frm.CurrentHemoName = _patientDocRow.NAME;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage10.Controls.Add(frm);
                frm.LoadInfo();
                return;
            }
            //切换患者营养评估
            if (this.xtraTabPage11.Controls.Count > 0 && this.xtraTabControl2.SelectedTabPage == this.xtraTabPage11) {
                (this.xtraTabPage11.Controls[0] as NutritionSGAUI).CurrentHemoId = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage11.Controls[0] as NutritionSGAUI).Query();
            }
            else if (this.xtraTabControl2.SelectedTabPage == this.xtraTabPage11) {
                NutritionSGAUI frm = new NutritionSGAUI();
                frm.CurrentHemoId = _patientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage11.Controls.Add(frm);
                frm.Query();
                return;
            }

            //切换患者风险评估
            if (this.xtraTabPage12.Controls.Count > 0 && this.xtraTabControl2.SelectedTabPage == this.xtraTabPage12) {
                (this.xtraTabPage12.Controls[0] as RiskAssessUI).InitalizeData();
            }
            else if (this.xtraTabControl2.SelectedTabPage == this.xtraTabPage12) {
                RiskAssessUI frm = new RiskAssessUI();
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage12.Controls.Add(frm);
                frm.InitalizeData();
                return;

            }
            //切换患者内瘘评估
            if (this.xtraTabPage13.Controls.Count > 0 && this.xtraTabControl2.SelectedTabPage == this.xtraTabPage13) {
                (this.xtraTabPage13.Controls[0] as QueryEstimateInBasketUI).HemoID = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage13.Controls[0] as QueryEstimateInBasketUI).queryData(PatientDocRow.HEMODIALYSIS_ID);
            }
            else if (this.xtraTabControl2.SelectedTabPage == this.xtraTabPage13) {

                QueryEstimateInBasketUI frm = new QueryEstimateInBasketUI();
                frm.HemoID = _patientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage13.Controls.Add(frm);
                frm.queryData(_patientDocRow.HEMODIALYSIS_ID);
                return;

            }
            //切换临时导管评估
            if (this.xtraTabPage14.Controls.Count > 0 && this.xtraTabControl2.SelectedTabPage == this.xtraTabPage14) {
                (this.xtraTabPage14.Controls[0] as QueryEstimateVenousListUI).IsTemp = true;
                (this.xtraTabPage14.Controls[0] as QueryEstimateVenousListUI).HemoId = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage14.Controls[0] as QueryEstimateVenousListUI).QueryData();
            }
            else if (this.xtraTabControl2.SelectedTabPage == this.xtraTabPage14) {

                QueryEstimateVenousListUI frm = new QueryEstimateVenousListUI();
                frm.IsTemp = true;
                frm.HemoId = _patientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage14.Controls.Add(frm);
                frm.QueryData();
                return;

            }
            //切换长期导管评估
            if (this.xtraTabPage15.Controls.Count > 0 && this.xtraTabControl2.SelectedTabPage == this.xtraTabPage15) {
                (this.xtraTabPage15.Controls[0] as QueryEstimateVenousListUI).IsTemp = false;
                (this.xtraTabPage15.Controls[0] as QueryEstimateVenousListUI).HemoId = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage15.Controls[0] as QueryEstimateVenousListUI).QueryData();
            }
            else if (this.xtraTabControl2.SelectedTabPage == this.xtraTabPage15) {

                QueryEstimateVenousListUI frm = new QueryEstimateVenousListUI();
                frm.IsTemp = false;
                frm.HemoId = _patientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage15.Controls.Add(frm);
                frm.QueryData();
                return;

            }
            //切换Kolcaba评估
            if (this.xtraTabPage16.Controls.Count > 0 && this.xtraTabControl2.SelectedTabPage == this.xtraTabPage16) {
                (this.xtraTabPage16.Controls[0] as PatientKolcabaUI).CurrentHemoId = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage16.Controls[0] as PatientKolcabaUI).CurrentHemoName = PatientDocRow.NAME;
                (this.xtraTabPage16.Controls[0] as PatientKolcabaUI).Query();
            }
            else if (this.xtraTabControl2.SelectedTabPage == this.xtraTabPage16) {

                PatientKolcabaUI frm = new PatientKolcabaUI();
                frm.CurrentHemoId = _patientDocRow.HEMODIALYSIS_ID;
                frm.CurrentHemoName = _patientDocRow.NAME;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage16.Controls.Add(frm);
                frm.Query();
                return;

            }
            //切换透析综合评估
            if (this.xtraTabPage17.Controls.Count > 0 && this.xtraTabControl2.SelectedTabPage == this.xtraTabPage17) {
                (this.xtraTabPage17.Controls[0] as AssessmentListUI).HemoID = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage17.Controls[0] as AssessmentListUI).patient = PatientDocRow;
                (this.xtraTabPage17.Controls[0] as AssessmentListUI).LoadInfo();
            }
            else if (this.xtraTabControl2.SelectedTabPage == this.xtraTabPage17) {
                AssessmentListUI frm = new AssessmentListUI();
                frm.HemoID = _patientDocRow.HEMODIALYSIS_ID;
                frm.patient = _patientDocRow;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage17.Controls.Add(frm);
                frm.LoadInfo();
                return;
            }
            //切换患者健康宣教
            if (this.xtraTabPage18.Controls.Count > 0 && this.xtraTabControl2.SelectedTabPage == this.xtraTabPage18) {
                (this.xtraTabPage18.Controls[0] as EditHealthEducationList).HEMODIALYSIS_ID = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage18.Controls[0] as EditHealthEducationList).LoadData(PatientDocRow.HEMODIALYSIS_ID);
            }
            else if (this.xtraTabControl2.SelectedTabPage == this.xtraTabPage18) {
                EditHealthEducationList frm = new EditHealthEducationList();
                frm.HEMODIALYSIS_ID = _patientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage18.Controls.Add(frm);
                frm.LoadData(PatientDocRow.HEMODIALYSIS_ID);
                return;
            }

            //切换贫血评估
            if (this.xtraTabPage24.Controls.Count > 0 && this.xtraTabControl2.SelectedTabPage == this.xtraTabPage24) {
                (this.xtraTabPage24.Controls[0] as AnemiaAssessment).HEMODIALYSIS_ID = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage24.Controls[0] as AnemiaAssessment).LoadData(PatientDocRow.HEMODIALYSIS_ID);
            }
            else if (this.xtraTabControl2.SelectedTabPage == this.xtraTabPage24) {
                AnemiaAssessment frm = new AnemiaAssessment();
                frm.HEMODIALYSIS_ID = _patientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage24.Controls.Add(frm);
                frm.LoadData(PatientDocRow.HEMODIALYSIS_ID);
                return;
            }

            //切换CKDMBD评估
            if (this.xtraTabPage25.Controls.Count > 0 && this.xtraTabControl2.SelectedTabPage == this.xtraTabPage25) {
                (this.xtraTabPage25.Controls[0] as CkdmbdAssessment).HEMODIALYSIS_ID = PatientDocRow.HEMODIALYSIS_ID;
                (this.xtraTabPage25.Controls[0] as CkdmbdAssessment).LoadData(PatientDocRow.HEMODIALYSIS_ID);
            }
            else if (this.xtraTabControl2.SelectedTabPage == this.xtraTabPage25) {
                CkdmbdAssessment frm = new CkdmbdAssessment();
                frm.HEMODIALYSIS_ID = _patientDocRow.HEMODIALYSIS_ID;
                frm.Dock = DockStyle.Fill;
                this.xtraTabPage25.Controls.Add(frm);
                frm.LoadData(PatientDocRow.HEMODIALYSIS_ID);
                return;
            }



        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载患者信息
        /// </summary>
        /// <param name="pPatientDocRow"></param>
        private void loadPatientInfo(PatientModel.MED_PATIENTSRow pPatientDocRow) {
            //lblPatientID.Text = pPatientDocRow.PATIENT_ID;
            //    var patientPicDt = objPatient.GetPatientPicByHemoId(pPatientDocRow.HEMODIALYSIS_ID);
        }

        /// <summary>
        /// 加载患者综合信息
        /// </summary>
        public void LoadPatientFixInfos() {
            this.Text = "患者综合信息";

            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage22;
            PatientAllViewUI frm = new PatientAllViewUI();
            frm.CurrentHemoId = _patientDocRow.HEMODIALYSIS_ID;
            frm.Dock = DockStyle.Fill;
            this.xtraTabPage22.Controls.Add(frm);
            frm.InzationData();

            //CtlPatientSufficiency frm2 = new CtlPatientSufficiency();
            //frm2.CurrentPatient = _patientDocRow;
            //frm2.HemoId = _patientDocRow.HEMODIALYSIS_ID;
            //frm2.Dock = DockStyle.Fill;
            //this.xtraTabPage9.Controls.Add(frm2);
            //frm2.Query();
            AnemiaAssessment frm2 = new AnemiaAssessment();
            frm2.HEMODIALYSIS_ID = _patientDocRow.HEMODIALYSIS_ID;
            frm2.Dock = DockStyle.Fill;
            this.xtraTabPage24.Controls.Add(frm2);
            frm2.LoadData(PatientDocRow.HEMODIALYSIS_ID);
        }

        public void OutChangePage(string name) {

            switch (name) {
                case "健康宣教":
                    this.xtraTabControl1.SelectedTabPage = this.xtraTabPage8;
                    this.xtraTabControl2.SelectedTabPage = this.xtraTabPage9;
                    break;
                case "患者基本资料":
                    this.xtraTabControl1.SelectedTabPage = this.xtraTabPage5;
                    break;
                case "通路建立":
                    this.xtraTabControl1.SelectedTabPage = this.xtraTabPage7;
                    break;
                case "通路评估":
                    this.xtraTabControl1.SelectedTabPage = this.xtraTabPage8;
                    this.xtraTabControl2.SelectedTabPage = this.xtraTabPage15;
                    break;
                case "透析方案制定":
                    this.xtraTabControl1.SelectedTabPage = this.xtraTabPage4;
                    break;
                case "当日透析方案确认":
                    this.xtraTabControl1.SelectedTabPage = this.xtraTabPage4;
                    break;
                default:
                    break;
            }
        }

        public void ChangePage(ModuleType type) {
            CurrentUI = type;
            switch (type) {
                case ModuleType.TempDrug:
                    this.xtraTabControl1.SelectedTabPage = this.xtraTabPage4;
                    break;
            }
        }

        #endregion

        private void btnLabInfo_Click(object sender, EventArgs e) {
            XtraForm form = new XtraForm();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Text = _patientDocRow.NAME + "的检验数据";
            ctlLabFrm labFrm = new ctlLabFrm(_patientDocRow);
            form.Size = labFrm.Size;
            labFrm.LoadLabInfo(_patientDocRow);
            labFrm.Dock = DockStyle.Fill;
            form.Controls.Add(labFrm);
            form.Show();
        }
    }
}