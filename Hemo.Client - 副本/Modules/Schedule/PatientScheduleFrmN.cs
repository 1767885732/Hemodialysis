/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:增加窗体控件值的方法
 * 创建标识:吕志强-2017年1月22日
 * 
 * 修改时间:2017年6月9日
 * 修改人:刘超
 * 修改描述:增加窗体控件值的方法
 * 
 * 修改时间:2017年7月11日
 * 修改人:顾伟伟
 * 修改描述:修复系统响应速度慢的问题
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.PatientSchedule;
using Hemo.Service;
using Hemo.Client.Controls.Schedule;
using Hemo.IService.Dict;
using Hemo.Model;
using Hemo.Client.Core;
using Hemo.Utilities;
using Hemo.Client.UI.PatientSchedule;
using Hemo.WinForm;
using Hemo.Client.UI.User;
using Hemo.Client.UI.Lab;
using Hemo.Client.UI.Patient;
using Hemo.Client.Controls.ScheduleNew;
using Color = System.Windows.Media.Color;
using DevExpress.Utils.Animation;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010.Customization;
using Hemo.Client.Modules.Patient;

namespace Hemo.Client.Modules
{
    public partial class PatientScheduleFrmN : DevExpress.XtraEditors.XtraUserControl
    {
        #region 变量

        private IPatientSchedule patientScheduleService = ServiceManager.Instance.PatientSchedule;
        private CtlScheduleMainGrid ctlScheduleMain;
        private DateTime _startTime;
        private DateTime _endTime;
        private int _nextWeekDays = 0;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private DictModel.MED_STAFF_DICTDataTable dtStaffSict = new DictModel.MED_STAFF_DICTDataTable();
        #endregion

        #region 构造函数与方法
        public PatientScheduleFrmN()
        {
            InitializeComponent();

            this.barBtn_User.Caption += string.IsNullOrEmpty(HemoApplicationContext.Current.CurrentUser.USER_NAME) ? "用户" : ":" + HemoApplicationContext.Current.CurrentUser.USER_NAME;
            this.barBtn_Date.Caption = DateTime.Today.ToString("yyyy年MM月dd日");
            this.barBtn_IP.Caption = HemoApplicationContext.Current.IpAddress;

            dtStaffSict = _staffDictService.GetStaffDictList();
            this.panelBottom.Visible = false;
        }

        private void PatientScheduleFrmN_Load(object sender, EventArgs e)
        {
             ctlScheduleMain = new CtlScheduleMainGrid();
            CalculateWeek();
            ctlScheduleMain.InizationData(false);
            ctlScheduleMain.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctlScheduleMain);
            inizationCount();
         }

        public void InizationData(bool flag)
        {
            if (ctlScheduleMain != null)
            {
                ctlScheduleMain.InizationData(flag);
            }
        }

        private void inizationCount() {
            using (BackgroundWorker worker = new BackgroundWorker()) {
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    dt1 = patientScheduleService.GetCureCountByParam(Utility.CDate(patientScheduleService.GetServerDate()));
                    dt2 = patientScheduleService.GetPurificationModeCountByParam(Utility.CDate(patientScheduleService.GetServerDate()));
                    dt3 = patientScheduleService.GetCurrentDutyUser();
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {

                    string cureContent = "当天治疗情况";
                    string cureType = string.Empty;
                    string cureTypeContent = string.Empty;

                    foreach (DataRow row in dt1.Rows) {
                        cureContent = cureContent + string.Format("{0}班未开始治疗<Color=blue> {1} </Color=blue>人,正在治疗<Color=blue> {2} </Color=blue>人,结束治疗<Color=blue> {3} </Color=blue>人  ", row["BANCHI"].ToString(), row["a"].ToString(), row["b"].ToString(), row["c"].ToString());
                    }
                    int allCount = 0;

                    foreach (DataRow row in dt2.Rows) {
                        allCount = allCount + int.Parse(row["COUNT"].ToString());
                    }

                    for (int i = 0; i < dt2.Rows.Count; i++) {
                        string banchi = dt2.Rows[i]["BANCHI"].ToString();
                        if ((i + 1) < dt2.Rows.Count) {
                            if (dt2.Rows[i]["BANCHI"].ToString() == dt2.Rows[i + 1]["BANCHI"].ToString()) {
                                cureType = cureType + string.Format("<Color=blue> {0} </Color=blue><Color=blue> {1} </Color=blue>人", dt2.Rows[i]["PURIFICATION_MODE_NAME"].ToString(), dt2.Rows[i]["COUNT"].ToString());
                            }
                            else {
                                cureType = cureType + string.Format("<Color=blue> {0} </Color=blue><Color=blue> {1} </Color=blue>人 ", dt2.Rows[i]["PURIFICATION_MODE_NAME"].ToString(), dt2.Rows[i]["COUNT"].ToString());
                                cureType = banchi + cureType;
                                cureTypeContent = cureTypeContent + cureType;
                                cureType = string.Empty;
                            }
                        }
                        else {
                            cureType = cureType + string.Format("<Color=blue> {0} </Color=blue><Color=blue> {1} </Color=blue>人", dt2.Rows[i]["PURIFICATION_MODE_NAME"].ToString(), dt2.Rows[i]["COUNT"].ToString());
                            cureType = banchi + cureType;
                            cureTypeContent = cureTypeContent + cureType;
                            cureType = string.Empty;
                        }
                    }
                    if (dt1.Rows.Count > 0)
                    {
                        this.lb_count.Text = string.Format("当天的治疗情况:总人数据为<Color=blue> {0} </Color=blue>人其中{1}", allCount.ToString(), cureTypeContent);
                    }
                    else
                    {
                        this.lb_count.Text = string.Empty;
                    }
                    if (dt2.Rows.Count > 0)
                    {
                        this.lbCureCount.Text = cureContent;
                    }
                    else
                    {
                        this.lbCureCount.Text = string.Empty ;
 
                    }
                    if (dt3.Rows.Count > 0)
                    {

                        string doctors = string.Empty;
                        string nusers = string.Empty;
                        for (int i = 0; i < dt3.Rows.Count;i++ )
                        {
                            if (dt3.Rows[i][0].ToString() == "D")
                            {
                                string[] sArray = dt3.Rows[i][1].ToString().Split(',');
                                foreach (var str in sArray)
                                {
                                    var row = dtStaffSict.FindByEMP_NO(str.Trim());
                                    if (row != null)
                                        doctors += row.NAME + " ";
                                }
                            }
                            else
                            {
                                if (dt3.Rows.Count > 1)
                                {
                                    string[] sArray1 = dt3.Rows[i][1].ToString().Split(',');
                                    foreach (var str in sArray1)
                                    {
                                        var row = dtStaffSict.FindByEMP_NO(str.Trim());
                                        if (row != null)
                                            nusers += row.NAME + " ";
                                    }
                                }
                            }
                        }


                         //, dt3.Rows[1][1].ToString()
                        this.lb_count.Text = string.Format("当日值班医生:<Color=blue>{0}</Color=blue>,值班护士:<Color=blue>{1}</Color=blue> {2}",doctors,nusers, this.lb_count.Text);
                    }
                };
                worker.RunWorkerAsync();
            }
        }

        private void CalculateWeek() {
            //日期取服务器端值
            DateTime date = Utility.CDate(patientScheduleService.GetServerDate());
            DateTime startWeek = Utility.GetMonday(date).AddDays(this._nextWeekDays).Date;
            DateTime endWeek = startWeek.AddDays(6).Date;
            _startTime = startWeek.Date;
            _endTime = endWeek.Date;
            this.lblThisWeek.Caption = startWeek.ToShortDateString() + "~" + endWeek.ToShortDateString();

            this.ctlScheduleMain.LoadPatientScheduleData(startWeek.Date, endWeek.Date);
        }
        private void SetThisText() {
      //      this.Text = @"DoCare血液净化信息系统V1.0-病患排班";
        }
        #endregion

        #region 事件

        private void barQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctlScheduleMain.InizationData(false);
            SetThisText();
        }


        private void btnSaveSchedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctlScheduleMain.SaveToScheduleData();
            SetThisText();
        }

        private void barLargeButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctlScheduleMain.SaveToScheduleTemplate(false);
            SetThisText();
        }

        private void btnOpenTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (ScheduleTemplateManager frm = new ScheduleTemplateManager())
            {
                if (DialogResult.OK == frm.ShowDialog())
                {
                    this.ctlScheduleMain.ScheduleDataTableMain = frm.data;
                }
            }
        }

        private void barLargeButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (QueryScheduleByParam frm = new QueryScheduleByParam())
            {
                frm.ShowDialog();
            }
        }

        private void btnPatientScheduleReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            PatientScheduleReport frmPatientScheduleReport = new PatientScheduleReport();
            frmPatientScheduleReport.ShowDialog();

        }

        private void barLargeButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctlScheduleMain.InizationData(false);
            SetThisText();
            inizationCount();
        }

        private void btnnextweekschedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _nextWeekDays += 7;

            CalculateWeek();
            ctlScheduleMain.InizationData(false);
            SetThisText();
        }

        private void btnthisweekschedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _nextWeekDays -= 7;

            CalculateWeek();
            ctlScheduleMain.InizationData(false);
            SetThisText();
        }

        private void barButtonItem1_ItemClick(object sender_other, DevExpress.XtraBars.ItemClickEventArgs e_other)
        {

            if (ctlScheduleMain.IsDirty)
            {
                if (XtraMessageBox.Show("排班数据未保存，是否确认退出当前系统？", "提示", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }
            }

            LoginScreen frm = new LoginScreen();
            frm.ShiftRoles = HemoApplicationContext.Current.RolesOffices;
            frm.LoginEvent += delegate(object sender, LoginEventArgs e)
            {
                frm.Dispose();
                this.Dispose();
                Program.Show(e.RunApp, e.RunAppNames);

            };
            frm.ShowDialog();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChangPwdFrm frm = new ChangPwdFrm(HemoApplicationContext.Current.CurrentUser.USER_ID);
            frm.ShowDialog();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           // this.Close();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barSaveSchedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctlScheduleMain.SaveToScheduleData();
            SetThisText();
        }

        private void barSaveScheduleTemp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctlScheduleMain.SaveToScheduleTemplate(false);
            SetThisText();
        }

        private void bbiPatientScheduleReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PatientScheduleReport frmPatientScheduleReport = new PatientScheduleReport();
            frmPatientScheduleReport.ShowDialog();
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SchedulePatientLabReport frm = new SchedulePatientLabReport(_startTime, _endTime, this.barEditItemBanCi.EditValue.ToString());
            frm.ShowDialog();
        }

        private void btnClose1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           // this.Close();
        }

        private void PatientScheduleFrmNew_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (ctlScheduleMain.IsDirty)
            {
                if (XtraMessageBox.Show("排班数据未保存，是否确认退出当前系统？", "提示", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
                {
                    e.Cancel = true;
                }
                else
                {
                    Program.HideClose = true;
                }
            }
            else
            {
                if (XtraMessageBox.Show("您确定退出当前系统吗？", "提示", MessageBoxButtons.OKCancel) !=
                   System.Windows.Forms.DialogResult.OK)
                {

                    e.Cancel = true;
                }
                else
                {
                    Program.HideClose = true;
                }
            }
        }

        private void barAddPatient_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (EditPatientNew frmEditPatient = new EditPatientNew())
            {
                frmEditPatient.Current = null;
                frmEditPatient.ShowDialog();
            }
        }

        private void barEditPatient_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barScreen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DateTime date = Utility.CDate(patientScheduleService.GetServerDate()).Date;
            var startTime = date;
            var endTime = date.AddDays(6);
            var data = patientScheduleService.GetScheduleRemarkByDate(startTime, endTime);

            using (ScheduleRemarkFrm frm = new ScheduleRemarkFrm())
            {
                frm.beginTime = startTime;
                frm.endTime = endTime;
                if (data != null && data.Rows.Count > 0)
                {
                    frm.Current = data[0];
                }
                else
                {
                    frm.Current = null;
                }
                frm.ShowDialog();
            }
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctlScheduleMain.ShowPatientInfo();
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ScheduleTemplateManager frm = new ScheduleTemplateManager();
            if (DialogResult.OK == frm.ShowDialog())
            {
                this.ctlScheduleMain.ScheduleDataTableMain = frm.data;
            }
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (PatientDutyFrm frm = new PatientDutyFrm())
            {
                frm.ShowDialog();
            }
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SchedulePatientInfos frm = new SchedulePatientInfos();
            frm.ShowDialog();
        }


        private void lbHide_Click(object sender, EventArgs e)
        {
            //Transition transiton = new Transition();
            //transiton.ShowWaitingIndicator = DefaultBoolean.False;
            //transiton.Control = this;
            //transiton.TransitionType = new FadeTransition();
            //TransitionManager manager = new TransitionManager();
            //manager.Transitions.Add(transiton);
            //this.lbHide.Parent = this;
            //Random r = new Random();
            //manager.StartTransition(this);
            if (panelBottom.Visible)
            {
                this.panelBottom.Visible = false;
                this.lbHide.Appearance.Image = global::Hemo.Client.Properties.Resources.arrowTop;

            }
            else
            {
                this.panelBottom.Visible = true;
                this.lbHide.Appearance.Image = global::Hemo.Client.Properties.Resources.arrowBottom;

                
            }
            //manager.EndTransition();
        }

        private void lbHide_MouseHover(object sender, EventArgs e)
        {
            this.lbHide.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(243)))), ((int)(((byte)(183)))));
         
        }

        private void lbHide_MouseLeave(object sender, EventArgs e)
        {
            this.lbHide.Appearance.BackColor = System.Drawing.Color.Transparent;


        }

        private void barButtonItem40_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FlyoutDialog.Show(this.FindForm(), new FormPatientOperatorUI());
        }

        private void barButtonItem41_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var currentPatient = this.ctlScheduleMain.GetCurrentPatient();
            if (currentPatient != null && currentPatient.Rows.Count > 0)
            {
                var patientCardOperator = new PatientCardOperatorUI();
                patientCardOperator.currentHemoId = currentPatient[0].HEMODIALYSIS_ID;
                patientCardOperator.InzationData();
                FlyoutDialog.Show(this.FindForm(), patientCardOperator);
            }
            else
            {
                var patientCardOperator = new PatientCardOperatorUI();
                //patientCardOperator.currentHemoId = currentPatient[0].HEMODIALYSIS_ID;
                patientCardOperator.InzationData();
                FlyoutDialog.Show(this.FindForm(), patientCardOperator);
            }
        }

        #endregion
    }
}