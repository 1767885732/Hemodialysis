/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司股份有限公司
// 文件名：CtlFirstPageView.cs
// 文件功能描述：自定义控件治疗单首页
// 创建标识：刘超 2013-07-22
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using DevExpress.Xpf.Core.HandleDecorator;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraWaitForm;
using UserControl = System.Windows.Controls.UserControl;
using Hemo.IService.Config;
using Hemo.Service;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using System.Windows.Media.Animation;
using DevExpress.XtraPrinting.Native;
using System.Windows.Forms;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Core.Native;
using Hemo.IService;
using Hemo.IService.Lab;
using Hemo.Model;


namespace Hemo.Client.Controls {
    /// <summary>
    /// CtlFirstPageView.xaml 的交互逻辑
    /// </summary>
    public partial class CtlFirstPageView : UserControl {
        #region 变量

        private IHemodialysis _hemoService = ServiceManager.Instance.HemodialysisService;
        private IPatient _patientService = ServiceManager.Instance.PatientService;
        private IVascuarAccess _vascuarAccess = ServiceManager.Instance.VascuarAccessService;
        private ILab _labService = ServiceManager.Instance.LabService;
        private int _nextSpan = 0;
        private int _minData = 0;
        private int _maxData = 5;
        private string currentHemoId = string.Empty;

        #endregion

        #region 构造函数
        public CtlFirstPageView() {
            InitializeComponent();
        }



        #endregion

        #region 方法


        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="hemoId">透析编号</param>
        public void InzationData(string hemoId) {
            currentHemoId = hemoId;
            ShowMessage();//显示等待窗口
            this.txtBFZ.Text = string.Empty;
            this.txtPGTX.Text = string.Empty;
            using (BackgroundWorker worker = new BackgroundWorker()) {
                #region 相关表

                var dtPastCureAndPressure = new DataTable();
                var dtLabResent = new DataTable();
                var dtInfectResent = new DataTable();
                var dtSufficiency = new DataTable();
                var dtAssessment = new DataTable();

                var minValue = _minData + _nextSpan;
                var maxValue = _maxData + _nextSpan;

                #endregion
                #region 检验常规监测项目

                string LabInfectionStr = string.Empty;
                string XUECHANGGUI = "血常规";
                string GANGONGNEG = "肝功能";
                string SHENGGONGNENG = "肾功能";
                string DIANJIEZHI = "电解质";
                string XUEZHI = "血脂";
                string FANYINGDANBAI = "反应蛋白";
                string JIAZHUANGSU = "甲状旁腺素";
                string TIE = "铁";
                string SHENGHUA = "SHENGHUA";

                #endregion
                #region 传染病常规监测项目
                string InfectionStr = string.Empty;
                string YIGAN = "已肝";
                string BINGGAN = "丙肝";
                string MEIDU = "梅毒";
                string AIZIBING = "艾滋病";
                string kvtAssess = string.Empty;
                string nourishAssess = string.Empty;
                #endregion
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    dtPastCureAndPressure = _hemoService.GetPatientCureAndPastPressureByParam(hemoId, minValue, maxValue);
                    dtLabResent = _labService.GetThreeMonthsCommonLabListByHemoID(hemoId);
                    dtInfectResent = _labService.GetSixMonthsCommonLabListByHemoID(hemoId);

                    dtSufficiency = _hemoService.GetEstimateSufficiencyByHemoIdAndDate(currentHemoId, "0", DateTime.Now.AddMonths(-3), DateTime.Now.Date);

                    dtAssessment = _hemoService.GetAssessmentByParams(currentHemoId, DateTime.Now.AddMonths(-3), DateTime.Now.Date);
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    #region SetValue
                    if (dtPastCureAndPressure.Rows.Count <= 0) {
                        HideMessage();
                        return;
                    }
                    if (dtPastCureAndPressure.Rows.Count >= 5) {
                        this.left.IsEnabled = true;
                        foreach (DataRow row in dtPastCureAndPressure.Rows) {
                            //6行进行赋值
                            if (int.Parse(row["ROWNUM"].ToString()) < 6) {
                                SetValue(row["ROWNUM"].ToString(), row, "TITLE", string.Empty);
                                SetValue(row["ROWNUM"].ToString(), row, "CONTET", string.Empty);
                                SetValue(row["ROWNUM"].ToString(), row, "KLFA", string.Empty);

                                #region 根据CURE_ID去取患者的用药记录。
                                var drugDt = _hemoService.GetCureDrugByCureID(row["CURE_ID"].ToString());
                                var strDrug = string.Empty;
                                foreach (DataRow drugRow in drugDt.Rows) {
                                    strDrug += string.Format("{0} {1} {2} \r\n", drugRow["DRUG_NAME"].ToString(),
                                        drugRow["DOSAGE"].ToString(), drugRow["UNIT_NAME"].ToString());
                                }
                                SetValue(row["ROWNUM"].ToString(), null, "DRUG", strDrug);
                                #endregion

                                #region 根据CURE_ID去取患者的并发症记录。
                                var compDt = _hemoService.GetComplicationOther(row["CURE_ID"].ToString());
                                var strComp = string.Empty;
                                foreach (DataRow compRow in compDt.Rows) {
                                    foreach (DataColumn compColumn in compDt.Columns) {
                                        strComp += compRow[compColumn].ToString();
                                    }
                                }
                                SetValue(row["ROWNUM"].ToString(), null, "COMPLICATION", strComp);
                                #endregion
                            }
                        }
                    }
                    else {
                        for (int i = 0; i < dtPastCureAndPressure.Rows.Count; i++) {
                            var row = dtPastCureAndPressure.Rows[i];
                            SetValue(row["ROWNUM"].ToString(), row, "TITLE", string.Empty);
                            SetValue(row["ROWNUM"].ToString(), row, "CONTET", string.Empty);
                            SetValue(row["ROWNUM"].ToString(), row, "KLFA", string.Empty);

                            #region 根据CURE_ID去取患者的用药记录。
                            var drugDt = _hemoService.GetCureDrugByCureID(row["CURE_ID"].ToString());
                            var strDrug = string.Empty;
                            foreach (DataRow drugRow in drugDt.Rows) {
                                strDrug += string.Format("{0} {1} {2} \r\n", drugRow["DRUG_NAME"].ToString(),
                                    drugRow["DOSAGE"].ToString(), drugRow["UNIT_NAME"].ToString());
                            }
                            SetValue(row["ROWNUM"].ToString(), null, "DRUG", strDrug);
                            #endregion

                            #region 根据CURE_ID去取患者的并发症记录。
                            var compDt = _hemoService.GetComplicationOther(row["CURE_ID"].ToString());
                            var strComp = string.Empty;
                            foreach (DataRow compRow in compDt.Rows) {
                                foreach (DataColumn compColumn in compDt.Columns) {
                                    strComp += compRow[compColumn].ToString();
                                }
                            }
                            SetValue(row["ROWNUM"].ToString(), null, "COMPLICATION", strComp);
                            #endregion
                        }
                        for (int i = dtPastCureAndPressure.Rows.Count; i < 5; i++) {
                            var rownum = (i + 1).ToString();
                            SetValue(rownum, null, "TITLE", string.Empty);
                            SetValue(rownum, null, "CONTET", string.Empty);
                            SetValue(rownum, null, "KLFA", string.Empty);
                            SetValue(rownum, null, "DRUG", string.Empty);
                            SetValue(rownum, null, "COMPLICATION", string.Empty);

                        }
                        this.left.IsEnabled = false;
                    }
                    #endregion

                    #region labResult and infection
                    if (dtLabResent.Rows.Count > 0) {
                        LabInfectionStr = "以下项目已超过三个月没有进行常规监测";

                        foreach (DataRow vRow in dtLabResent.Rows) {
                            if (vRow["XUECHANGGUI"].ToString().Equals("是")) {
                                XUECHANGGUI = string.Empty;
                            }
                            if (vRow["SHENGHUA"].ToString().Equals("是")) {
                                SHENGHUA = string.Empty;
                            }
                            if (vRow["DIANJIEZHI"].ToString().Equals("是")) {
                                DIANJIEZHI = string.Empty;
                            }
                            if (vRow["XUEZHI"].ToString().Equals("是")) {
                                XUEZHI = string.Empty;
                            }
                            if (vRow["FANYINGDANBAI"].ToString().Equals("是")) {
                                FANYINGDANBAI = string.Empty;
                            }
                            if (vRow["JIAZHUANGSU"].ToString().Equals("是")) {
                                JIAZHUANGSU = string.Empty;
                            }
                            if (vRow["TIE"].ToString().Equals("是")) {
                                TIE = string.Empty;
                            }
                            if (vRow["GANGONGNEG"].ToString().Equals("是")) {
                                GANGONGNEG = string.Empty;
                            }
                            if (vRow["SHENGGONGNENG"].ToString().Equals("是")) {
                                SHENGGONGNENG = string.Empty;
                            }
                        }
                        if (string.IsNullOrEmpty(XUECHANGGUI) && string.IsNullOrEmpty(SHENGGONGNENG) && string.IsNullOrEmpty(GANGONGNEG) && string.IsNullOrEmpty(DIANJIEZHI) && string.IsNullOrEmpty(XUEZHI) && string.IsNullOrEmpty(FANYINGDANBAI) && string.IsNullOrEmpty(JIAZHUANGSU) && string.IsNullOrEmpty(TIE) && string.IsNullOrEmpty(SHENGHUA)) {
                            LabInfectionStr = "已超过三个月没有进行检验常规监测项目检验";
                        }
                        else {
                            LabInfectionStr = string.Format("{0}:{1} {2} {3} {4} {5} {6} {7} {8} {9}", LabInfectionStr,
                                XUECHANGGUI, SHENGGONGNENG, GANGONGNEG, DIANJIEZHI, XUEZHI, FANYINGDANBAI, JIAZHUANGSU,
                                TIE, SHENGHUA);
                        }
                    }
                    else {
                        LabInfectionStr = "已超过三个月没有进行检验常规监测项目检验";
                    }

                    if (dtInfectResent.Rows.Count > 0) {
                        InfectionStr = "传染病项目已超过六个月未监测";

                        foreach (DataRow vRow in dtInfectResent.Rows) {
                            if (vRow["YIGAN"].ToString().Equals("是")) {
                                YIGAN = string.Empty;
                            }
                            if (vRow["BINGGAN"].ToString().Equals("是")) {
                                BINGGAN = string.Empty;
                            }
                            if (vRow["MEIDU"].ToString().Equals("是")) {
                                MEIDU = string.Empty;
                            }
                            if (vRow["AIZIBING"].ToString().Equals("是")) {
                                AIZIBING = string.Empty;
                            }
                        }
                        if (string.IsNullOrEmpty(YIGAN) && string.IsNullOrEmpty(BINGGAN) && string.IsNullOrEmpty(MEIDU) &&
                            string.IsNullOrEmpty(AIZIBING)) {
                                InfectionStr = "已超过六个月没有做传染病常规监测项目检验";
                        }
                        else {
                            InfectionStr = string.Format("{0}:{1} {2} {3} {4}", InfectionStr, YIGAN, BINGGAN, MEIDU,
                                AIZIBING);
                        }
                    }
                    else {
                        InfectionStr = "已超过六个月没有做传染病常规监测项目检验";
                    }

                    this.txtBFZ.Text = string.Format("{0}\r{1}", LabInfectionStr, InfectionStr);

                    #endregion

                    #region Assess For KVT Assess For nourished

                    if (dtSufficiency.Rows.Count > 0) {
                        kvtAssess = string.Empty;
                    }
                    else {
                        kvtAssess = "已超过三个月没有进行Urr和Kt/V评估";
                    }

                    if (dtAssessment.Rows.Count > 0) {
                        nourishAssess = string.Empty;
                    }
                    else {
                        nourishAssess = "营养评估";
                    }

                    this.txtPGTX.Text = string.Format("{0}\r{1}", kvtAssess, nourishAssess);

                    #endregion

                    //设置血管通路
                    SetVascuarInfo();
                    HideMessage();//关于等待窗口
                };
                worker.RunWorkerAsync();
            }
        }
        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="rowValue">RowNum</param>
        /// <param name="vRow">Row</param>
        /// <param name="strType">Type</param>
        private void SetValue(string rowValue, DataRow vRow, string strType, string Content) {
            #region TITLE

            if (strType.Equals("TITLE")) {
                string value = vRow == null ? string.Empty : Utilities.Utility.CDate(vRow["CREATE_DATE"].ToString()).ToString("yyyy-MM-dd");
                string cureId = vRow == null ? string.Empty : vRow["CURE_ID"].ToString();
                switch (rowValue) {
                    case "1":
                        this.txt10.Text = value;
                        this.txt10.Tag = cureId;
                        break;
                    case "2":
                        this.txt11.Text = value;
                        this.txt11.Tag = cureId;
                        break;
                    case "3":
                        this.txt12.Text = value;
                        this.txt12.Tag = cureId;
                        break;
                    case "4":
                        this.txt13.Text = value;
                        this.txt13.Tag = cureId;
                        break;
                    case "5":
                        this.txt14.Text = value;
                        this.txt14.Tag = cureId;
                        break;
                }
            }
            #endregion

            #region CONTET
            else if (strType.Equals("CONTET")) {
                var value = vRow == null ? string.Empty : vRow["BEFORE_DRY_WEIGHT"].ToString();
                var value1 = vRow == null ? string.Empty : vRow["AFTER_DRY_WEIGHT"].ToString();
                var value2 = vRow == null ? string.Empty : vRow["CARDIOTACH"].ToString();
                var value3 = vRow == null ? string.Empty : vRow["SYSTOLIC_PRESSURE"].ToString();
                var value4 = vRow == null ? string.Empty : vRow["UFR"].ToString();
                switch (rowValue) {
                    case "1":
                        this.txt1.Text = value;
                        this.txt2.Text = value1;
                        this.txt3.Text = value2;
                        this.txt4.Text = value3;
                        this.txt5.Text = value4;
                        break;
                    case "2":
                        this.txt21.Text = value;
                        this.txt22.Text = value1;
                        this.txt23.Text = value2;
                        this.txt24.Text = value3;
                        this.txt25.Text = value4;
                        break;
                    case "3":
                        this.txt31.Text = value;
                        this.txt32.Text = value1;
                        this.txt33.Text = value2;
                        this.txt34.Text = value3;
                        this.txt35.Text = value4;
                        break;
                    case "4":
                        this.txt41.Text = value;
                        this.txt42.Text = value1;
                        this.txt43.Text = value2;
                        this.txt44.Text = value3;
                        this.txt45.Text = value4;
                        break;
                    case "5":
                        this.txt51.Text = value;
                        this.txt52.Text = value1;
                        this.txt53.Text = value2;
                        this.txt54.Text = value3;
                        this.txt55.Text = value4;
                        break;
                }

            }
            #endregion

            #region OTHER
            else if (strType.Equals("KLFA")) {
                var value = vRow == null ? string.Empty : string.Format("{0} {1}", vRow["ITEM_NAME"].ToString(), vRow["LOT"].ToString());
                switch (rowValue) {
                    case "1":
                        this.txtKNFA0.Text = value;
                        break;
                    case "2":
                        this.txtKNFA1.Text = value;
                        break;
                    case "3":
                        this.txtKNFA2.Text = value;
                        break;
                    case "4":
                        this.txtKNFA3.Text = value;
                        break;
                    case "5":
                        this.txtKNFA4.Text = value;
                        break;
                }
            }
            #endregion

            #region DRUG
            else if (strType.Equals("DRUG")) {
                switch (rowValue) {
                    case "1":
                        this.txtYYJV0.Text = Content;
                        break;
                    case "2":
                        this.txtYYJV1.Text = Content;
                        break;
                    case "3":
                        this.txtYYJV2.Text = Content;
                        break;
                    case "4":
                        this.txtYYJV3.Text = Content;
                        break;
                    case "5":
                        this.txtYYJV4.Text = Content;
                        break;
                }
            }
            #endregion

            #region COMPLICATION
            else if (strType.Equals("COMPLICATION")) {
                switch (rowValue) {
                    case "1":
                        this.txtBFZ0.Text = Content;
                        break;
                    case "2":
                        this.txtBFZ1.Text = Content;
                        break;
                    case "3":
                        this.txtBFZ2.Text = Content;
                        break;
                    case "4":
                        this.txtBFZ3.Text = Content;
                        break;
                    case "5":
                        this.txtBFZ4.Text = Content;
                        break;
                }
            }
            #endregion
        }

        #region 血管通路写字并载入图片
        private HemoModel.MED_VASCULAR_ACCESSDataTable _vascularAccessDataTable;

        private void SetVascuarInfo() {
            var vascuarDt = _vascuarAccess.GetVascularAccessListByHEMODIALYSIS_ID(currentHemoId);
            if (vascuarDt != null && vascuarDt.Rows.Count > 0) {
                DataTable dtVaName = _vascuarAccess.GetVascularAccessAllName(vascuarDt.Rows[0]["VASCULAR_ACCESS_ID"].ToString());
                if (dtVaName != null && dtVaName.Rows.Count > 0) {
                    loadValPicture(dtVaName.Rows[0][0].ToString());
                    this.patientVascularName.Content = dtVaName.Rows[0][0].ToString();

                }
            }
        }

        private void drawValName(string pName) {
            if (_vascularAccessDataTable != null && _vascularAccessDataTable.Rows.Count > 0) {
                //using (Graphics g = Graphics.FromImage((Image)picVasAccess.Source)
                //{
                //    g.DrawString(pName, new Font("宋体", 12),
                //        Brushes.Red, new PointF(8, 485));
                //    g.Flush();
                //}
            }
        }

        private void loadValPicture(string pName) {
            if (pName.Contains("左侧颈内静脉")) {
                BitmapImage image = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + @"\Resources\human_organs_1.png", UriKind.Absolute));
                picVasAccess.Source = image;
            }
            else if (pName.Contains("右侧侧颈内静脉")) {
                BitmapImage image = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + @"\Resources\human_organs_2.png", UriKind.Absolute));
                picVasAccess.Source = image;
            }
            else if (pName.Contains("左侧锁骨下静脉")) {
                BitmapImage image = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + @"\Resources\human_organs_3.png", UriKind.Absolute));
                picVasAccess.Source = image;
            }
            else if (pName.Contains("右侧锁骨下静脉")) {
                BitmapImage image = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + @"\Resources\human_organs_4.png", UriKind.Absolute));
                picVasAccess.Source = image;//Image.FromFile(System.Windows.Forms.Application.StartupPath  + @"\Resources\human_organs_4.png");
            }
            else if (pName.Contains("左侧上臂")) {
                BitmapImage image = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + @"\Resources\human_organs_5.png", UriKind.Absolute));
                picVasAccess.Source = image;//Image.FromFile(System.Windows.Forms.Application.StartupPath  + @"\Resources\human_organs_5.png");
            }
            else if (pName.Contains("右侧下臂")) {
                BitmapImage image = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + @"\Resources\human_organs_6.png", UriKind.Absolute));
                picVasAccess.Source = image;//Image.FromFile(System.Windows.Forms.Application.StartupPath  + @"\Resources\human_organs_6.png");
            }
            else if (pName.Contains("左侧前臂")) {
                BitmapImage image = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + @"\Resources\human_organs_7.png", UriKind.Absolute));
                picVasAccess.Source = image;//Image.FromFile(System.Windows.Forms.Application.StartupPath  + @"\Resources\human_organs_7.png");
            }
            else if (pName.Contains("右侧前臂")) {
                BitmapImage image = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + @"\Resources\human_organs_8.png", UriKind.Absolute));
                picVasAccess.Source = image;//Image.FromFile(System.Windows.Forms.Application.StartupPath  + @"\Resources\human_organs_8.png");
            }
            else if (pName.Contains("左侧股静脉")) {
                BitmapImage image = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + @"\Resources\human_organs_9.png", UriKind.Absolute));
                picVasAccess.Source = image;
            }
            else if (pName.Contains("右侧股静脉"))
            {
                BitmapImage image = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + @"\Resources\human_organs_10.png", UriKind.Absolute));
                picVasAccess.Source = image;
            }
            else {
                BitmapImage image = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + @"\Resources\human_organs.png", UriKind.Absolute));
                picVasAccess.Source = image;
            }
            //drawValName(pName);
        }
        #endregion

        #endregion

        #region 事件

        #region Left Or Right OnMouseButtonDown

        private void Left_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (e.ClickCount == 1) {
                _nextSpan += 5;
                var value = _minData + _nextSpan;
                if (value < 5) {
                    _nextSpan = 0;
                    SetInzationMaxMin();

                    return;

                }
                if (!string.IsNullOrEmpty(currentHemoId)) {
                    InzationData(currentHemoId);

                }
            }
        }

        private void Right_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (e.ClickCount == 1) {
                _nextSpan -= 5;
                var value = _maxData + _nextSpan;
                if (value < 5) {
                    _nextSpan = 0;
                    SetInzationMaxMin();

                    return;

                }
                if (!string.IsNullOrEmpty(currentHemoId)) {
                    InzationData(currentHemoId);

                }
            }
        }

        private void SetInzationMaxMin() {
            _minData = 0;
            _maxData = 5;
        }

        #endregion

        #region LEFT OR RIGHT show hide

        private DoubleAnimation _doubleleftAnimation = null;
        private void Left_OnMouseEnter(object sender, MouseEventArgs e) {
            if (_doubleleftAnimation == null) {
                _doubleleftAnimation = new DoubleAnimation();
                _doubleleftAnimation.From = 0;
                _doubleleftAnimation.To = 1;
                _doubleleftAnimation.Duration = TimeSpan.FromMilliseconds(500);
                _doubleleftAnimation.FillBehavior = FillBehavior.Stop;
                _doubleleftAnimation.Completed += delegate
                {
                    this.left.Opacity = 1;
                };
            }

            this.left.BeginAnimation(System.Windows.Controls.Image.OpacityProperty, _doubleleftAnimation, HandoffBehavior.SnapshotAndReplace);
        }

        private DoubleAnimation _doubleLeftAnimation2 = null;
        private void Left_OnMouseLeave(object sender, MouseEventArgs e) {
            if (_doubleLeftAnimation2 == null) {
                _doubleLeftAnimation2 = new DoubleAnimation();
                _doubleLeftAnimation2.From = 1;
                _doubleLeftAnimation2.To = 0;
                _doubleLeftAnimation2.Duration = TimeSpan.FromMilliseconds(500);
                _doubleLeftAnimation2.FillBehavior = FillBehavior.Stop;
                _doubleLeftAnimation2.Completed += delegate
                {
                    this.left.Opacity = 0;
                };
            }

            this.left.BeginAnimation(System.Windows.Controls.Image.OpacityProperty, _doubleLeftAnimation2, HandoffBehavior.SnapshotAndReplace);
        }

        private DoubleAnimation _doubleRightAnimation = null;

        private void Right_OnMouseEnter(object sender, MouseEventArgs e) {
            if (_doubleRightAnimation == null) {
                _doubleRightAnimation = new DoubleAnimation();
                _doubleRightAnimation.From = 0;
                _doubleRightAnimation.To = 1;
                _doubleRightAnimation.Duration = TimeSpan.FromMilliseconds(500);
                _doubleRightAnimation.FillBehavior = FillBehavior.Stop;
                _doubleRightAnimation.Completed += delegate
                {
                    this.right.Opacity = 1;
                };
            }

            this.right.BeginAnimation(System.Windows.Controls.Image.OpacityProperty, _doubleRightAnimation, HandoffBehavior.SnapshotAndReplace);

        }
        private DoubleAnimation _doubleRightAnimation2 = null;

        private void Right_OnMouseLeave(object sender, MouseEventArgs e) {

            if (_doubleRightAnimation2 == null) {
                _doubleRightAnimation2 = new DoubleAnimation();
                _doubleRightAnimation2.From = 1;
                _doubleRightAnimation2.To = 0;
                _doubleRightAnimation2.Duration = TimeSpan.FromMilliseconds(500);
                _doubleRightAnimation2.FillBehavior = FillBehavior.Stop;
                _doubleRightAnimation2.Completed += delegate
                {
                    this.right.Opacity = 0;
                };
            }

            this.right.BeginAnimation(System.Windows.Controls.Image.OpacityProperty, _doubleRightAnimation2, HandoffBehavior.SnapshotAndReplace);

        }

        #endregion

        #region SplashScreenManager

        private SplashScreenManager _loadForm;
        /// <summary>
        /// 等待窗体管理对象
        /// </summary>
        protected SplashScreenManager LoadForm {
            get {
                if (_loadForm == null) {
                    this._loadForm = new SplashScreenManager(new Form(), typeof(FrmWaitForm), true, true);
                    //this._loadForm.CloseWaitForm();.ClosingDelay = 0;
                }
                return _loadForm;
            }
        }
        /// <summary>
        /// 显示等待窗体
        /// </summary>
        public void ShowMessage() {
            bool flag = !this.LoadForm.IsSplashFormVisible;
            if (flag) {
                this.LoadForm.ShowWaitForm();
            }
        }
        /// <summary>
        /// 关闭等待窗体
        /// </summary>
        public void HideMessage() {
            bool isSplashFormVisible = this.LoadForm.IsSplashFormVisible;
            if (isSplashFormVisible) {
                this.LoadForm.CloseWaitForm();
            }
        }

        #endregion

        /// <summary>
        /// PageTitle_MouseDoubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageTitle_MouseDoubleClick(object sender, MouseButtonEventArgs e) {



            #region //以后进行开发，根据每次治疗单对应不一样的血管通路

            //var cureId = ((System.Windows.Controls.TextBox)sender).Tag.ToString().Trim();
            // //根据 CureId去获取血管通路
            // var vascularAcessName = _patientService.GetVascularAccessNameByCureId(cureId);

            // this.patientVascularName.Content = vascularAcessName;

            // loadValPicture(vascularAcessName);


            #endregion

        }


        #endregion
    }
}
