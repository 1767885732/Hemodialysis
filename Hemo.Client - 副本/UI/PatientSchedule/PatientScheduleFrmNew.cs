/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:新患者排班主窗体类
 * 创建标识:贺建操-2016年4月23日
 * ----------------------------------------------------------------*/

using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using Hemo.Client.Controls;
using Hemo.Utilities;
using Hemo.Client.UI.Patient;
using Hemo.WinForm;
using Hemo.Client.UI.ReportChart;
using Hemo.Client.Core;
using Hemo.Client.UI.User;
using DevExpress.XtraEditors;
using Hemo.Client.UI.Machine;
using System.Linq;
using Hemo.Client.UI.Lab;
using System.ComponentModel;
using Hemo.IService.PatientSchedule;
using Hemo.Service;
using Hemo.Client.Controls.Schedule;
using Hemo.Client.Controls.ScheduleNew;
using Hemo.IService;
using Hemo.IService.Dict;
using Hemo.Model;

namespace Hemo.Client.UI.PatientSchedule {
    public partial class PatientScheduleFrmNew : HemoBaseFrm {

        #region 变量

        private IPatientSchedule patientScheduleService = ServiceManager.Instance.PatientSchedule;
        private CtlScheduleMainGrid ctlScheduleMain;
        private DateTime _startTime;
        private DateTime _endTime;
        private int _nextWeekDays = 0;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private DictModel.MED_STAFF_DICTDataTable dtStaffSict = new DictModel.MED_STAFF_DICTDataTable();
        
        #endregion

        #region 构造函数

        public PatientScheduleFrmNew() {
            InitializeComponent();
            this.barBtn_User.Caption += string.IsNullOrEmpty(HemoApplicationContext.Current.CurrentUser.USER_NAME) ? "用户" : ":" + HemoApplicationContext.Current.CurrentUser.USER_NAME;
            this.barBtn_Date.Caption = DateTime.Today.ToString("yyyy年MM月dd日");
            this.barBtn_IP.Caption = HemoApplicationContext.Current.IpAddress;
            this.barBtn_Version.Caption = HemoApplicationContext.Current.versionAddress;
            dtStaffSict = _staffDictService.GetStaffDictList();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientScheduleFrmNew_Load(object sender, EventArgs e)
        {
            ctlScheduleMain = new CtlScheduleMainGrid();
            CalculateWeek();
            ctlScheduleMain.InizationData(false);
            ctlScheduleMain.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctlScheduleMain);
            inizationCount();
        }

        /// <summary>
        /// 查询排班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.ctlScheduleMain.InizationData(false);
            SetThisText();
        }

        /// <summary>
        /// 保存排班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveSchedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.ctlScheduleMain.SaveToScheduleData();
            SetThisText();
        }

        /// <summary>
        /// 保存排班模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barLargeButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.ctlScheduleMain.SaveToScheduleTemplate(false);
            SetThisText();
        }

        /// <summary>
        /// 打开模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            using (ScheduleTemplateManager frm = new ScheduleTemplateManager())
            {
                if (DialogResult.OK == frm.ShowDialog())
                {
                    this.ctlScheduleMain.ScheduleDataTableMain = frm.data;
                }
            }
        }

        /// <summary>
        /// 查询排班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barLargeButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            using (QueryScheduleByParam frm = new QueryScheduleByParam())
            {
                frm.ShowDialog();
            }
        }

        /// <summary>
        /// 排班报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPatientScheduleReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

            PatientScheduleReport frmPatientScheduleReport = new PatientScheduleReport();
            frmPatientScheduleReport.ShowDialog();

        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barLargeButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.ctlScheduleMain.InizationData(false);
            SetThisText();
            inizationCount();
        }

        /// <summary>
        /// 下周排班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnnextweekschedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            _nextWeekDays += 7;

            CalculateWeek();
            ctlScheduleMain.InizationData(false);
            SetThisText();
        }

        /// <summary>
        /// 当周排班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnthisweekschedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            _nextWeekDays -= 7;

            CalculateWeek();
            ctlScheduleMain.InizationData(false);
            SetThisText();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender_other"></param>
        /// <param name="e_other"></param>
        private void barButtonItem1_ItemClick(object sender_other, DevExpress.XtraBars.ItemClickEventArgs e_other) {

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

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ChangPwdFrm frm = new ChangPwdFrm(HemoApplicationContext.Current.CurrentUser.USER_ID);
            frm.ShowDialog();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.Close();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

        }

        /// <summary>
        /// 保存排班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSaveSchedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.ctlScheduleMain.SaveToScheduleData();
            SetThisText();
        }

        /// <summary>
        /// 保存排班模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSaveScheduleTemp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.ctlScheduleMain.SaveToScheduleTemplate(false);
            SetThisText();
        }

        /// <summary>
        /// 排班报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiPatientScheduleReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            PatientScheduleReport frmPatientScheduleReport = new PatientScheduleReport();
            frmPatientScheduleReport.ShowDialog();
        }

        /// <summary>
        /// 排班报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            SchedulePatientLabReport frm = new SchedulePatientLabReport(_startTime, _endTime, this.barEditItemBanCi.EditValue.ToString());
            frm.ShowDialog();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.Close();
        }

        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientScheduleFrmNew_FormClosing(object sender, FormClosingEventArgs e) {

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

        /// <summary>
        /// 新增患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAddPatient_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            using (EditPatientNew frmEditPatient = new EditPatientNew())
            {
                frmEditPatient.Current = null;
                frmEditPatient.ShowDialog();
            }
        }

        /// <summary>
        /// 编辑患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEditPatient_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

        }

        /// <summary>
        /// 排班公告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 查看患者信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctlScheduleMain.ShowPatientInfo();
        }

        /// <summary>
        /// 模板管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ScheduleTemplateManager frm = new ScheduleTemplateManager();
            if (DialogResult.OK == frm.ShowDialog())
            {
                this.ctlScheduleMain.ScheduleDataTableMain = frm.data;
            }
        }

        /// <summary>
        /// 排班值周安排
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (PatientDutyFrm frm = new PatientDutyFrm())
            {
                frm.ShowDialog();
            }
        }

        /// <summary>
        /// 排班信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SchedulePatientInfos frm = new SchedulePatientInfos();
            frm.ShowDialog();
        }

        #endregion 

        #region 方法

        /// <summary>
        /// 初始化数量
        /// </summary>
        private void inizationCount()
        {
            using (BackgroundWorker worker = new BackgroundWorker())
            {
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

                    foreach (DataRow row in dt1.Rows)
                    {
                        cureContent = cureContent + string.Format("{0}班未开始治疗<Color=blue> {1} </Color=blue>人,正在治疗<Color=blue> {2} </Color=blue>人,结束治疗<Color=blue> {3} </Color=blue>人  ", row["BANCHI"].ToString(), row["a"].ToString(), row["b"].ToString(), row["c"].ToString());
                    }
                    int allCount = 0;

                    foreach (DataRow row in dt2.Rows)
                    {
                        allCount = allCount + int.Parse(row["COUNT"].ToString());
                    }

                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        string banchi = dt2.Rows[i]["BANCHI"].ToString();
                        if ((i + 1) < dt2.Rows.Count)
                        {
                            if (dt2.Rows[i]["BANCHI"].ToString() == dt2.Rows[i + 1]["BANCHI"].ToString())
                            {
                                cureType = cureType + string.Format("<Color=blue> {0} </Color=blue><Color=blue> {1} </Color=blue>人", dt2.Rows[i]["PURIFICATION_MODE_NAME"].ToString(), dt2.Rows[i]["COUNT"].ToString());
                            }
                            else
                            {
                                cureType = cureType + string.Format("<Color=blue> {0} </Color=blue><Color=blue> {1} </Color=blue>人 ", dt2.Rows[i]["PURIFICATION_MODE_NAME"].ToString(), dt2.Rows[i]["COUNT"].ToString());
                                cureType = banchi + cureType;
                                cureTypeContent = cureTypeContent + cureType;
                                cureType = string.Empty;
                            }
                        }
                        else
                        {
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
                        this.lbCureCount.Text = string.Empty;

                    }
                    if (dt3.Rows.Count > 0)
                    {

                        string doctors = string.Empty;
                        string nusers = string.Empty;
                        for (int i = 0; i < dt3.Rows.Count; i++)
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
                        this.lb_count.Text = string.Format("当日值班医生:<Color=blue>{0}</Color=blue>,值班护士:<Color=blue>{1}</Color=blue> {2}", doctors, nusers, this.lb_count.Text);
                    }
                };
                worker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// 加载排班数据
        /// </summary>
        private void CalculateWeek()
        {
            //日期取服务器端值
            DateTime date = Utility.CDate(patientScheduleService.GetServerDate());
            DateTime startWeek = Utility.GetMonday(date).AddDays(this._nextWeekDays).Date;
            DateTime endWeek = startWeek.AddDays(6).Date;
            _startTime = startWeek.Date;
            _endTime = endWeek.Date;
            this.lblThisWeek.Caption = startWeek.ToShortDateString() + "~" + endWeek.ToShortDateString();

            this.ctlScheduleMain.LoadPatientScheduleData(startWeek.Date, endWeek.Date);
        }

        private void SetThisText()
        {
            //      this.Text = @"DoCare血液净化信息系统V1.0-病患排班";
        }

        #endregion
    }
}