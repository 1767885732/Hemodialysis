/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：报表统计首页用户控件类
// 创建时间：2016-07-3
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.IService.Lab;
using Hemo.Service;
using Hemo.IService.Config;
using Hemo.Model;
using Hemo.Client.UI.Patient;
using DevExpress.XtraCharts;
using Hemo.Client.UI.ReportChart;
using Hemo.Client.Controls;
using DevExpress.XtraSplashScreen;

namespace Hemo.Client.UI.Machine
{
    public partial class IntegratedQuery : ViewBase
    {
        #region 类变量

        private ILab labService = ServiceManager.Instance.LabService;
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private DataTable dtReport = null;
        private DataTable dtHemoCount = null;
        private DataTable dtInfectionReport = null;
        private DataTable dtQualityReport = null;
        private HemodialysisModel.MED_HEALTH_EDUCATIONDataTable healthEdu = null;
        private DrugModel.MED_PATIENT_FOLLOWUPDataTable followUp = null;
        private HemoModel.MED_COMPLICATION_OTHERDataTable complicationTable = null;

        private DataSet allLabUnCurePatients = null;

        private DataTable dtMonitorReport = null;
        private DataTable dtResult = null;

        #endregion

        #region 属性

        private SplashScreenManager _loadForm;
        /// <summary>
        /// 等待窗体管理对象
        /// </summary>
        protected SplashScreenManager LoadForm
        {
            get
            {
                if (_loadForm == null)
                {
                    this._loadForm = new SplashScreenManager(this.Parent.FindForm(), typeof(FrmWaitForm), true, true);
                    //this._loadForm.CloseWaitForm();.ClosingDelay = 0;
                }
                return _loadForm;
            }
        }

        #endregion

        #region 构造函数

        public IntegratedQuery()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void IntegratedQuery_Load(object sender, EventArgs e)
        {
            QueryHemoCoutScaleReport queryHemoCoutScaleReport = new QueryHemoCoutScaleReport();
            queryHemoCoutScaleReport.IsFirstPage = true;
            queryHemoCoutScaleReport.SetSearchBarDisplayNone();
            queryHemoCoutScaleReport.Dock = DockStyle.Fill;
            xtraTabPage3.Controls.Add(queryHemoCoutScaleReport);

            CtlShowAllHemoInfo ctlShowAllHemoInfo = new CtlShowAllHemoInfo();
            ctlShowAllHemoInfo.IsFirstPage = true;
            ctlShowAllHemoInfo.SetSearchBarDisplayNone();
            ctlShowAllHemoInfo.Dock = DockStyle.Fill;
            xtraTabPage4.Controls.Add(ctlShowAllHemoInfo);

            InizationData();
        }

        private void labelControl__infectedPatients_Click(object sender, EventArgs e)
        {
            PatientsListForIntegrated frm = new PatientsListForIntegrated();
            frm._patient = this.labelControl__infectedPatients.Tag as PatientModel.MED_PATIENTSDataTable;
            string text = this.labelControl__infectedPatients.Text.Replace("\r\n", "");
            frm.Text = text.Substring(1, text.IndexOf('为'));
            frm.ShowDialog();
        }

        private void labelControl__rouBloodPatients_Click(object sender, EventArgs e)
        {
            PatientsListForIntegrated frm = new PatientsListForIntegrated();
            frm._patient = this.labelControl__rouBloodPatients.Tag as PatientModel.MED_PATIENTSDataTable;

            string text = this.labelControl__rouBloodPatients.Text.Replace("\r\n", "");
            frm.Text = text.Substring(1, text.IndexOf('为'));
            frm.ShowDialog();
        }

        private void labelControl__renalPatients_Click(object sender, EventArgs e)
        {
            PatientsListForIntegrated frm = new PatientsListForIntegrated();
            frm._patient = this.labelControl__renalPatients.Tag as PatientModel.MED_PATIENTSDataTable;
            string text = this.labelControl__renalPatients.Text.Replace("\r\n", "");
            frm.Text = text.Substring(1, text.IndexOf('为'));
            frm.ShowDialog();
        }

        private void labelControl__electrolytePatients_Click(object sender, EventArgs e)
        {
            PatientsListForIntegrated frm = new PatientsListForIntegrated();
            frm._patient = this.labelControl__electrolytePatients.Tag as PatientModel.MED_PATIENTSDataTable;

            string text = this.labelControl__electrolytePatients.Text.Replace("\r\n", "");
            frm.Text = text.Substring(1, text.IndexOf('为'));
            frm.ShowDialog();
        }

        private void labelControl__basketPatients_Click(object sender, EventArgs e)
        {
            PatientsListForIntegrated frm = new PatientsListForIntegrated();
            frm._patient = this.labelControl__basketPatients.Tag as PatientModel.MED_PATIENTSDataTable;
            string text = this.labelControl__basketPatients.Text.Replace("\r\n", "");
            frm.Text = text.Substring(1, text.IndexOf('为'));
            frm.ShowDialog();
        }

        private void labelControl__long_venousPatients_Click(object sender, EventArgs e)
        {
            PatientsListForIntegrated frm = new PatientsListForIntegrated();
            frm._patient = this.labelControl__long_venousPatients.Tag as PatientModel.MED_PATIENTSDataTable;
            string text = this.labelControl__long_venousPatients.Text.Replace("\r\n", "");
            frm.Text = text.Substring(1, text.IndexOf('为'));
            frm.ShowDialog();
        }

        private void labelControl__venous_catheterPatients_Click(object sender, EventArgs e)
        {
            PatientsListForIntegrated frm = new PatientsListForIntegrated();
            frm._patient = this.labelControl__venous_catheterPatients.Tag as PatientModel.MED_PATIENTSDataTable;
            string text = this.labelControl__venous_catheterPatients.Text.Replace("\r\n", "");
            frm.Text = text.Substring(1, text.IndexOf('为'));
            frm.ShowDialog();
        }

        #endregion

        #region 方法

        private void InizationData()
        {
            #region MyRegion

            int sumHemo = 0;
            int sumHD = 0;
            int sumHDF = 0;
            int sumHF = 0;
            int sumHP = 0;
            int sumHD_HP = 0;
            int sumDeath = 0;
            int sumDeathRate = 0;
            int sumSevereComplication = 0;
            int sumHBSAG = 0;
            int sumHBEAG = 0;
            int sumANTI_HCV = 0;
            int sumPeritonealDialysis = 0;
            int sumRenalTransplant = 0;

            #endregion

            #region MyRegion
            int sumNegative = 0;
            int sumHBsAg_Positive = 0;
            int sumHBeAg_Positive = 0;
            int sumAnti_HCV_Positive = 0;
            int sumAnti_TP_Positive = 0;
            int sumHIV_Positive = 0;
            int sumPositive = 0;
            #endregion

            #region MyRegion
            int sumUreaRemove = 0;
            int sumRenalAnemia = 0;
            int sumCa_P_Metabolism = 0;
            int sumSecondaryShpt = 0;
            int sumVenousCatheter = 0;
            int sumAutologousFistula = 0;
            int sumTempVenousCatheter = 0;
            int sumArtificialVessel = 0;
            int sumDoubleVein = 0;
            int sumHighAvf = 0;
            int sumJugularVenousCatheter = 0;
            int sumSubclavianVenousCatheter = 0;
            int sumFemoralVenousCatheter = 0;
            int sumPressureControl = 0;
            int sumTimeLess8 = 0;
            int sumTime8_9 = 0;
            int sumTime9_10 = 0;
            int sumTime10_11 = 0;
            int sumTime11_12 = 0;
            int sumTimeMore12 = 0;
            int sumComfort = 0;
            int sumMildDiscomfort = 0;
            int sumSevereDiscomfort = 0;
            int sumPeritonealDialysis1 = 0;
            #endregion
            //this.Enabled = false;
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                DateTime dt = System.DateTime.Now;
                DateTime startYear = new DateTime(dt.Year, 1, 1);  //本年年初 
                DateTime endYear = new DateTime(dt.Year, 12, 31);  //本年年末  
                ShowMessage();
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    #region 获取数据
                    //// 本年度透析总例数 
                    //dtReport = labService.GetQualityControlBaseDataByDate(startYear,endYear);
                    ////本年度持续透析人数
                    //dtHemoCount = _hemodialysisService.GetHemoCoutScale(startYear, endYear);
                    ////本年度院感检查
                    //dtInfectionReport = labService.GetInfectionCheckListByDate(startYear, endYear);
                    ////本年度质量管理
                    //this.complicationTable = _hemodialysisService.GetComplicationByParams(startYear, endYear);
                    ////本年度维持性患者检测指标
                    //dtMonitorReport = labService.GetQualityMonitorIndicatorByDate(startYear, endYear);
                    ////获取宣教
                    //healthEdu =  _hemodialysisService.GetHealthEducationByDateTime(startYear, endYear);
                    ////获取随访人数6
                    //followUp = _hemodialysisService.GetFollowUpByDateTime(startYear, endYear);
                    ////获取所有未评估或检验的患者
                    allLabUnCurePatients = _hemodialysisService.GetAllLabUnCurePatients();

                    dtResult = labService.GetQualityMonitorIndicatorByYear(System.DateTime.Now.ToString("yyyy"));

                    #endregion

                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    #region MyRegion
                    /*

                    #region FirstOne

                    dtReport.AsEnumerable().ToList().ForEach(row =>
                    {
                        sumHemo += int.Parse(row["HEMO_COUNT"].ToString());
                        sumHD += int.Parse(row["HD_COUNT"].ToString());
                        sumHDF += int.Parse(row["HDF_COUNT"].ToString());
                        sumHF += int.Parse(row["HF_COUNT"].ToString());
                        sumHP += int.Parse(row["HP_COUNT"].ToString());
                        sumHD_HP += int.Parse(row["HD_HP_COUNT"].ToString());
                        sumDeath += int.Parse(row["DEATH_COUNT"].ToString());
                        sumDeathRate += int.Parse(row["DEATH_RATE"].ToString());
                        sumSevereComplication += int.Parse(row["SEVERE_COMPLICATION"].ToString());
                        sumHBSAG += int.Parse(row["HBSAG_POSITIVE"].ToString());
                        sumHBEAG += int.Parse(row["HBEAG_POSITIVE"].ToString());
                        sumANTI_HCV += int.Parse(row["ANTI_HCV_POSITIVE"].ToString());
                        sumPeritonealDialysis += int.Parse(row["PERITONEAL_DIALYSIS"].ToString());
                        sumRenalTransplant += int.Parse(row["RENAL_TRANSPLANT"].ToString());
                    });
                    var allCount = sumHemo + sumHD+sumHDF+sumHF+sumHP+sumHD_HP;
                    this.labelControl_FirstOne.Text = string.Format("\r\n  本年度透析总例数<Color=blue>{0}</Color=blue> 例，血液透析 <Color=blue>{1}</Color=blue>  例，血液滤过 <Color=blue>{2}</Color=blue> 例,血透转肾移植例<Color=blue>{3}</Color=blue>例", allCount, sumHemo, sumHDF, sumRenalTransplant);

                    int allRCount = 0;
                    int threeCount = 0;
                    int twoCount =0;
                    int firstCount =0;
                    dtHemoCount.AsEnumerable().ToList().ForEach(row=>
                        {
                            allRCount += int.Parse(row["COUNT"].ToString());
                            if(row["NAME"].ToString().Equals("每周规律透析二次"))
                            {
                                twoCount = int.Parse(row["COUNT"].ToString());
                            }else if(row["NAME"].ToString().Equals("每周规律透析三次"))
                            {
                                threeCount= int.Parse(row["COUNT"].ToString());
                            }else if(row["NAME"].ToString().Equals("每周规律透析一次"))
                            {
                                firstCount= int.Parse(row["COUNT"].ToString());
                            }

                        });
                    this.labelControl_hemoCount.Text = string.Format("\r\n  本年度持续透析人数 <Color=blue>{0}</Color=blue>人，每周规律透析3次 <Color=blue>{1}</Color=blue>人，每周规律透析2次 <Color=blue>{2}</Color=blue> 人，其他<Color=blue>{3}</Color=blue> 人。", allRCount, threeCount, twoCount, firstCount);
                    #endregion
                    #region dtInfectionReport
                    dtInfectionReport.AsEnumerable().ToList().ForEach(row =>
                    {
                        sumNegative += int.Parse(row["NEGATIVE"].ToString());
                        sumHBsAg_Positive += int.Parse(row["HBSAG_POSITIVE"].ToString());
                        sumHBeAg_Positive += int.Parse(row["HBEAG_POSITIVE"].ToString());
                        sumAnti_HCV_Positive += int.Parse(row["ANTI_HCV_POSITIVE"].ToString());
                        sumAnti_TP_Positive += int.Parse(row["ANTI_TP_POSITIVE"].ToString());
                        sumHIV_Positive += int.Parse(row["HIV_POSITIVE"].ToString());
                        sumPositive += int.Parse(row["POSITIVE"].ToString());
                    });

                    this.labelControl_dtInfectionReport.Text = string.Format("\r\n  本年度院感检查：全阴患者例数 <Color=blue>{0}</Color=blue>人，乙肝例数<Color=blue>{1}</Color=blue>人（新增2人），丙肝例数 <Color=blue>{3}</Color=blue>人，梅毒例数 <Color=blue>{4}</Color=blue> 人，HIV例数 <Color=blue>{5}</Color=blue> 人", sumNegative, sumHBeAg_Positive, sumHBsAg_Positive, sumAnti_HCV_Positive, sumAnti_TP_Positive,sumHIV_Positive);

                    #endregion
                    #region complicationTable -- 质量管理

                    int sumCCSW = 0;
                    int sumXLSC= 0;
                    int sumDXT= 0;
                    int sumCC = 0;
                    int sumGXJ = 0;
                    complicationTable.AsEnumerable().ToList().ForEach(row =>
                    {
                        sumCCSW += int.Parse(ConvertToString(row["CCSW"]));
                        sumXLSC += int.Parse(ConvertToString(row["XLSC"]));
                        sumDXT += int.Parse(ConvertToString(row["DXT"]));
                        sumCC += sumDXT += int.Parse(ConvertToString(row["CC"]));
                        sumGXJ += int.Parse(ConvertToString(row["GXJ"]));
                    });


                    this.labelControl_complicationTable.Text = string.Format("\r\n  本年度质量管理：穿刺失误 <Color=blue>{0}</Color=blue>次，心率失常 <Color=blue>{1}</Color=blue>次，低血糖 <Color=blue>{2}</Color=blue> 次，抽搐 <Color=blue>{3}</Color=blue>次，高血钾 <Color=blue>{4}</Color=blue>次。",sumCCSW,sumXLSC,sumDXT,sumCC,sumGXJ);
                    #endregion
                    #region dtMonitorReport
                    dtMonitorReport.AsEnumerable().ToList().ForEach(row =>
                    {
                        sumAutologousFistula += int.Parse(row["AUTOLOGOUS_FISTULA"].ToString());
                        sumTempVenousCatheter += int.Parse(row["TEMP_VENOUS_CATHETER"].ToString());
                        sumArtificialVessel += int.Parse(row["ARTIFICIAL_VESSEL"].ToString());
                        sumDoubleVein += int.Parse(row["DOUBLE_VEIN"].ToString());
                        sumPressureControl += int.Parse(row["PRESSURE_CONTROL"].ToString());
                        sumComfort += int.Parse(row["COMFORT"].ToString());
                        sumMildDiscomfort += int.Parse(row["MILD_DISCOMFORT"].ToString());
                        sumSevereDiscomfort += int.Parse(row["SEVERE_DISCOMFORT"].ToString());
                    });


                    this.labelControl_dtMonitorReport.Text = string.Format("\r\n  本年度维持性患者检测指标：自体内瘘例数 <Color=blue>{0}</Color=blue> 例，临时静脉留置导管例数 <Color=blue>{1}</Color=blue>例，人造血管例数 <Color=blue>{2}</Color=blue>例，血压控制例数 <Color=blue>{3}</Color=blue> 例，舒适例数 <Color=blue>{4}</Color=blue>例，轻度不适例数 <Color=blue>{5}</Color=blue>例，重度不适例数 <Color=blue>{6}</Color=blue>例.",sumAutologousFistula,sumTempVenousCatheter,sumArtificialVessel,sumDoubleVein,sumPressureControl,sumComfort,sumMildDiscomfort,sumSevereDiscomfort);
                    #endregion
                    #region healthEdu

                    this.labelControl_healthEdu.Text = string.Format("\r\n  本年度开展透析患者健康宣教<Color=blue>{0}</Color=blue>次.随访<Color=blue>{1}</Color=blue>次", healthEdu.Count.ToString(), followUp.Count.ToString());

                    #endregion

                     */
                    #endregion

                    #region labe2的数据进行赋值。。。


                    this.labelControl__rouBloodPatients.Text = string.Format("※ 血常规30天未检查人数为<Color=blue><U>{0}</U></Color=blue>人 ", allLabUnCurePatients.Tables["_rouBloodPatients"].Rows.Count);
                    this.labelControl__rouBloodPatients.Tag = allLabUnCurePatients.Tables["_rouBloodPatients"];
                    this.labelControl__renalPatients.Text = string.Format("※ 肾功能30天未检查人数为<Color=blue><U>{0}</U></Color=blue>人 ", allLabUnCurePatients.Tables["_renalPatients"].Rows.Count);
                    this.labelControl__renalPatients.Tag = allLabUnCurePatients.Tables["_renalPatients"];
                    this.labelControl__electrolytePatients.Text = string.Format("※ 电解质90天未检查人数为<Color=blue><U>{0}</U></Color=blue>人 ", allLabUnCurePatients.Tables["_electrolytePatients"].Rows.Count);
                    this.labelControl__electrolytePatients.Tag = allLabUnCurePatients.Tables["_electrolytePatients"];
                    this.labelControl__infectedPatients.Text = string.Format("※ 传染病学指标180天未检查人数为<Color=blue><U>{0}</U></Color=blue>人 ", allLabUnCurePatients.Tables["_infectedPatients"].Rows.Count);
                    this.labelControl__infectedPatients.Tag = allLabUnCurePatients.Tables["_infectedPatients"];
                    this.labelControl__basketPatients.Text = string.Format("※ 内瘘未评估人数为<Color=blue><U>{0}</U></Color=blue>人\r\n", allLabUnCurePatients.Tables["_basketPatients"].Rows.Count);
                    this.labelControl__basketPatients.Tag = allLabUnCurePatients.Tables["_basketPatients"];
                    this.labelControl__long_venousPatients.Text = string.Format("※ 长期导管30天内未评估人数为<Color=blue><U>{0}</U></Color=blue>人 ", allLabUnCurePatients.Tables["_long_venousPatients"].Rows.Count);
                    this.labelControl__long_venousPatients.Tag = allLabUnCurePatients.Tables["_long_venousPatients"];
                    this.labelControl__venous_catheterPatients.Text = string.Format("※ 临时导管30天内未评估人数为<Color=blue><U>{0}</U></Color=blue>人 ", allLabUnCurePatients.Tables["_venous_catheterPatients"].Rows.Count);
                    this.labelControl__venous_catheterPatients.Tag = allLabUnCurePatients.Tables["_venous_catheterPatients"];


                    #endregion


                    this.officeDataView1.IniDateControl();
                    this.officeDataView1.InzationData();
                    //this.Enabled = true;
                    HideMessage();
                };
                worker.RunWorkerAsync();
            }
        }
        private string ConvertToString(object o)
        {
            if (o == null)
                return "0";
            if (o == DBNull.Value || o is DBNull)
                return "0";
            return o.ToString();
        }

        /// <summary>
        /// 设置报表外观
        /// </summary>
        /// <param name="chart"></param>
        /// <param name="text"></param>
        /// <param name="title"></param>
        private void SetChartSurface(ChartControl chart, string text, string title)
        {
            ((XYDiagram)chart.Diagram).AxisY.Title.Text = text;
            ((XYDiagram)chart.Diagram).AxisY.Title.Font = new Font("Tahoma", 10);
            ((XYDiagram)chart.Diagram).AxisY.Title.TextColor = Color.Red;
            ((XYDiagram)chart.Diagram).AxisY.Title.Visible = true;

            ChartTitle ctTitle = new ChartTitle();
            ctTitle.Text = title;
            ctTitle.Font = new Font("Tahoma", 12);
            ctTitle.Dock = ChartTitleDockStyle.Top;
            chart.Titles.Clear();
            chart.Titles.Add(ctTitle);

            chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
            chart.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
            chart.Legend.Direction = LegendDirection.TopToBottom;
        }

        /// <summary>
        /// 显示等待窗体
        /// </summary>
        public void ShowMessage()
        {
            bool flag = !this.LoadForm.IsSplashFormVisible;
            if (flag)
            {
                this.LoadForm.ShowWaitForm();
            }
        }
        /// <summary>
        /// 关闭等待窗体
        /// </summary>
        public void HideMessage()
        {
            bool isSplashFormVisible = this.LoadForm.IsSplashFormVisible;
            if (isSplashFormVisible)
            {
                this.LoadForm.CloseWaitForm();
            }
        }

        #endregion
    }
}
