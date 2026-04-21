/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:患者排班主窗体类
 * 创建标识:贺建操-2016年3月18日
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
using Hemo.Client.UI.Lab;
using System.ComponentModel;
using Hemo.IService.PatientSchedule;
using Hemo.Service;

namespace Hemo.Client.UI.PatientSchedule
{
    public partial class PatientScheduleFrm :HemoBaseFrm
    {
        #region 变量

        private DateTime _startTime;

        private DateTime _endTime;

        private CtlScheduleMain ctlScheduleMain;

        private int _nextWeekDays = 0;

        private IPatientSchedule patientScheduleService = ServiceManager.Instance.PatientSchedule;

        #endregion

        #region 构造函数

        public PatientScheduleFrm()
        {
            InitializeComponent();
            this.barBtn_User.Caption += string.IsNullOrEmpty(HemoApplicationContext.Current.CurrentUser.USER_NAME) ? "用户" : ":" + HemoApplicationContext.Current.CurrentUser.USER_NAME;
            this.barBtn_Date.Caption = DateTime.Today.ToString("yyyy年MM月dd日");
            this.barBtn_IP.Caption = HemoApplicationContext.Current.IpAddress;
            this.barBtn_Version.Caption = HemoApplicationContext.Current.versionAddress;

        }
        
        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientScheduleFrm_Load(object sender, EventArgs e)
        {
            busyIndicator1.ShowLoadingScreenFor(panelControl1);
            timer1.Enabled = true;
            timer1.Start();
        }

        /// <summary>
        /// 打开模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string bac = string.Empty;
            if (this.barEditItemBanCi.EditValue.ToString() == "1") { bac = "上午"; }
            else if (this.barEditItemBanCi.EditValue.ToString() == "2") { bac = "下午"; }
            else if (this.barEditItemBanCi.EditValue.ToString() == "3") { bac = "晚班"; }
            else if (this.barEditItemBanCi.EditValue.ToString() == "4") { bac = "急诊"; }

            if (XtraMessageBox.Show(string.Format("确定打开班次为{0}的模版信息吗？", bac), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            this._nextWeekDays = 0;
            this.lblThisWeek.Enabled = false;
            this.btnthisweekschedule.Enabled = false;
            this.btnnextweekschedule.Enabled = false;
            CalculateWeek();




            object BanciID = this.barEditItemBanCi.EditValue.ToString();
            ctlScheduleMain.btnOpenTemplate_Click(BanciID, e);
            this.btnSaveSchedule.SuperTip.Items.Clear();
            this.btnSaveSchedule.SuperTip.Items.AddTitle("保存为模板");
            this.barStaticItem_currentdata.Appearance.ForeColor = System.Drawing.Color.Red;
            this.barStaticItem_currentdata.Caption = "当前为" + getBanCiName() + "模板数据";

        }

        /// <summary>
        /// 保存模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTemplate(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            object BanciID = this.barEditItemBanCi.EditValue.ToString();
            ctlScheduleMain.btnSaveTemplate_Click(BanciID, e);
        }

        /// <summary>
        /// 保存排班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveSchedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool isMouBan = false;
            if (string.Equals(this.barStaticItem_currentdata.Caption.Trim(), "当前为" + getBanCiName() + "模板数据"))
            {
                isMouBan = true;
            }
            e.Item.Caption = isMouBan.ToString();
            object BanciID = this.barEditItemBanCi.EditValue.ToString();
            ctlScheduleMain.btnSave_Click(BanciID, e);
        }

        /// <summary>
        /// 上周排班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPreWeekSchedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _nextWeekDays -= 7;

            CalculateWeek();
            this.btnSaveSchedule.SuperTip.Items.Clear();
            this.btnSaveSchedule.SuperTip.Items.AddTitle("提交排班");
            this.barStaticItem_currentdata.Appearance.ForeColor = System.Drawing.Color.Black;
            this.barStaticItem_currentdata.Caption = "当前为" + getBanCiName() + "排班数据";
        }

        /// <summary>
        /// 下周排班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextWeekSchedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _nextWeekDays += 7;

            CalculateWeek();
            this.btnSaveSchedule.SuperTip.Items.Clear();
            this.btnSaveSchedule.SuperTip.Items.AddTitle("提交排班");
            this.barStaticItem_currentdata.Appearance.ForeColor = System.Drawing.Color.Black;
            this.barStaticItem_currentdata.Caption = "当前为" + getBanCiName() + "排班数据";
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 班次改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEditItemBanCi_EditValueChanged(object sender, EventArgs e)
        {
            //if (barEditItemBanCi.EditValue.ToString() == "1")
            //{
            //    ctlScheduleMain.RgpBanCi.SelectedIndex = 0;
            //}
            //if (barEditItemBanCi.EditValue.ToString() == "2")
            //{
            //    ctlScheduleMain.RgpBanCi.SelectedIndex = 1;
            //}
            //if (barEditItemBanCi.EditValue.ToString() == "3")
            //{
            //    ctlScheduleMain.RgpBanCi.SelectedIndex = 2;
            //}
            //if (barEditItemBanCi.EditValue.ToString() == "4")
            //{
            //    ctlScheduleMain.RgpBanCi.SelectedIndex = 3;
            //}
            //this.btnSaveSchedule.SuperTip.Items.Clear();
            //this.btnSaveSchedule.SuperTip.Items.AddTitle("提交排班");
            //this.barStaticItem_currentdata.Appearance.ForeColor = System.Drawing.Color.Black;
            //this.barStaticItem_currentdata.Caption = "当前为排班数据";
            //ctlScheduleMain.rgpBANCI_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// 病人排班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPatientScheduleReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            PatientScheduleReport frmPatientScheduleReport = new PatientScheduleReport();
            frmPatientScheduleReport.ShowDialog();
        }

        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientScheduleFrm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        /// <summary>
        /// 排班表打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiPatientScheduleReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PatientScheduleReport frmPatientScheduleReport = new PatientScheduleReport();

            frmPatientScheduleReport.ShowDialog();
        }

        /// <summary>
        /// 保存排班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSaveSchedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // ctlScheduleMain.btnSave_Click(null, e);
            btnSaveSchedule_ItemClick(null, e);
        }

        /// <summary>
        /// 保存排班模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSaveScheduleTemp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // object x = this.barEditItemBanCi.EditValue.ToString();
            //  ctlScheduleMain.btnSaveTemplate_Click(x, e);
            btnSaveSchedule_ItemClick(null, e);
        } 

        /// <summary>
        /// 切换登录
        /// </summary>
        /// <param name="senderr"></param>
        /// <param name="er"></param>
        private void barButtonItem1_ItemClick(object senderr, DevExpress.XtraBars.ItemClickEventArgs er)
        {
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
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChangPwdFrm frm = new ChangPwdFrm(HemoApplicationContext.Current.CurrentUser.USER_ID);
            frm.ShowDialog();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        /// <summary>
        /// 刷新排班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barLargeButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            this.lblThisWeek.Enabled = true;
            this.btnthisweekschedule.Enabled = true;
            this.btnnextweekschedule.Enabled = true;

            //barEditItemBanCi_EditValueChanged(null, null);
            QueryScheduleData();
        }

        /// <summary>
        /// 查询排班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barLargeButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            QueryScheduleByParam frm = new QueryScheduleByParam();
            frm.ShowDialog();
        }

        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientScheduleFrm_FormClosing(object sender, FormClosingEventArgs e)
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

        /// <summary>
        /// 排班报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SchedulePatientLabReport frm = new SchedulePatientLabReport(_startTime, _endTime, this.barEditItemBanCi.EditValue.ToString());
            frm.ShowDialog();
        }

        /// <summary>
        /// 定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {

            timer1.Enabled = false;
            ctlScheduleMain = new CtlScheduleMain();
            ctlScheduleMain.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctlScheduleMain);
            InitBar();
            CalculateWeek();
            this.btnSaveSchedule.SuperTip.Items.Clear();
            this.btnSaveSchedule.SuperTip.Items.AddTitle("提交排班");
            this.barStaticItem_currentdata.Appearance.ForeColor = System.Drawing.Color.Black;
            this.barStaticItem_currentdata.Caption = "当前为" + getBanCiName() + "排班数据";

            busyIndicator1.HideLoadingScreen();
            panelControl1.Visible = false;

        }

        private void PatientScheduleFrm_MouseEnter(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 查询排班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            QueryScheduleData();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 计算星期
        /// </summary>
        private void CalculateWeek()
        {
            //DateTime startWeek = Utility.GetMonday(DateTime.Now).AddDays(this._nextWeekDays).Date;
            //DateTime endWeek = startWeek.AddDays(6).Date;
            //日期取服务器端值
            DateTime date = Utility.CDate(patientScheduleService.GetServerDate());
            DateTime startWeek = Utility.GetMonday(date).AddDays(this._nextWeekDays).Date;
            DateTime endWeek = startWeek.AddDays(6).Date;
            _startTime = startWeek.Date;
            _endTime = endWeek.Date;
            this.lblThisWeek.Caption = startWeek.ToShortDateString() + "~" + endWeek.ToShortDateString();

            this.ctlScheduleMain.LoadPatientScheduleData(startWeek.Date, endWeek.Date);
        }

        /// <summary>
        /// 加载工具栏
        /// </summary>
        private void InitBar()
        {
            DataTable dtBANCI = new DataTable();
            dtBANCI.Columns.Add(new DataColumn("ITEM_ID"));
            dtBANCI.Columns.Add(new DataColumn("ITEM_NAME"));

            DataRow row = dtBANCI.NewRow();
            row["ITEM_ID"] = "1";
            row["ITEM_NAME"] = "上午";
            dtBANCI.Rows.Add(row);

            row = dtBANCI.NewRow();
            row["ITEM_ID"] = "2";
            row["ITEM_NAME"] = "下午";
            dtBANCI.Rows.Add(row);

            //row = dtBANCI.NewRow();
            //row["ITEM_ID"] = "3";
            //row["ITEM_NAME"] = "晚班";
            //dtBANCI.Rows.Add(row);

            row = dtBANCI.NewRow();
            row["ITEM_ID"] = "4";
            row["ITEM_NAME"] = "急诊";
            dtBANCI.Rows.Add(row);

            Hemo.Utilities.Utility.BindLookUpEdit(this.barEditItemBanCi.Edit as RepositoryItemLookUpEdit,
                "ITEM_ID", "ITEM_NAME", dtBANCI, "ITEM_NAME", "班次");

            (this.barEditItemBanCi.Edit as RepositoryItemLookUpEdit).NullText = "上午";
            this.barEditItemBanCi.EditValue = "1";
        }

        /// <summary>
        /// 查询排班
        /// </summary>
        private void QueryScheduleData()
        {
            if (barEditItemBanCi.EditValue.ToString() == "1")
            {
                ctlScheduleMain.RgpBanCi.SelectedIndex = 0;
            }
            if (barEditItemBanCi.EditValue.ToString() == "2")
            {
                ctlScheduleMain.RgpBanCi.SelectedIndex = 1;
            }
            if (barEditItemBanCi.EditValue.ToString() == "3")
            {
                ctlScheduleMain.RgpBanCi.SelectedIndex = 2;
            }
            if (barEditItemBanCi.EditValue.ToString() == "4")
            {
                ctlScheduleMain.RgpBanCi.SelectedIndex = 3;
            }
            this.btnSaveSchedule.SuperTip.Items.Clear();
            this.btnSaveSchedule.SuperTip.Items.AddTitle("提交排班");
            this.barStaticItem_currentdata.Appearance.ForeColor = System.Drawing.Color.Black;
            this.barStaticItem_currentdata.Caption = "当前为" + getBanCiName() + "排班数据";
            ctlScheduleMain.rgpBANCI_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// 获取班次名称
        /// </summary>
        /// <returns></returns>
        private string getBanCiName()
        {
            string result = string.Empty;
            if (barEditItemBanCi.EditValue != null)
            {
                if (barEditItemBanCi.EditValue.ToString() == "1")
                {
                    result = "上午班";
                }
                if (barEditItemBanCi.EditValue.ToString() == "2")
                {
                    result = "下午班";
                }
                if (barEditItemBanCi.EditValue.ToString() == "3")
                {
                    result = "晚班";
                }
                if (barEditItemBanCi.EditValue.ToString() == "4")
                {
                    result = "急诊";
                }
            }
            return result;
        }

        #endregion
    }
}