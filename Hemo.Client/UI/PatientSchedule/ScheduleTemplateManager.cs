/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:排班模板维护主窗体类
 * 创建标识:贺建操-2016年4月27日
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
using Hemo.Model;

namespace Hemo.Client.UI.PatientSchedule {
    public partial class ScheduleTemplateManager :HemoBaseFrm {

        #region 变量

        private IPatientSchedule patientScheduleService = ServiceManager.Instance.PatientSchedule;
        private CtlScheduleMainGrid ctlScheduleMain;
        private DateTime _startTime;
        private DateTime _endTime;
        private int _nextWeekDays = 0;

        public PermissionModel.MED_HEMO_SCHEDULEMASTERDataTable data = null;

        #endregion

        #region 构造函数与方法

        public ScheduleTemplateManager() {
            InitializeComponent();
            this.barBtn_User.Caption += string.IsNullOrEmpty(HemoApplicationContext.Current.CurrentUser.USER_NAME) ? "用户" : ":" + HemoApplicationContext.Current.CurrentUser.USER_NAME;
            this.barBtn_Date.Caption = DateTime.Today.ToString("yyyy年MM月dd日");
            this.barBtn_IP.Caption = HemoApplicationContext.Current.IpAddress;;
            this.barBtn_Version.Caption = HemoApplicationContext.Current.versionAddress;

        }
        
        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleTemplateManager_Load(object sender, EventArgs e)
        {
            ctlScheduleMain = new CtlScheduleMainGrid();
            CalculateWeek();
            ctlScheduleMain.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctlScheduleMain);
            ctlScheduleMain.InizationData(true);
        }

        /// <summary>
        /// 打开模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (TemplateList frm = new TemplateList())
            {
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                {
                    var templateId = frm.Tag.ToString();
                    var templateName = frm.templateName.ToString();
                    this.ctlScheduleMain._patientScheduleTemplateRow = frm._patientScheduleTemplateRow;

                    this.ctlScheduleMain.ImportTemplateScheduleData(templateId);
                    this.ctlScheduleMain.currentOpenTemplateID = templateId;
                    this.ctlScheduleMain.currentOpenTemplateName = templateName;
                    this.Text = string.Format("模板维护-{0}", templateName);
                }
            }
        }

        /// <summary>
        /// 保存模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barLargeButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctlScheduleMain.SaveToScheduleTemplate(true);
        }

        /// <summary>
        /// 应用模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("是否确认应用当前模板到排班中?", "应用模板", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                data = this.ctlScheduleMain.ScheduleDataTableMain;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        /// <summary>
        /// 模板维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ctlScheduleMain.InizationData(true);
            this.Text = string.Format("模板维护");

            this.ctlScheduleMain.currentOpenTemplateID = string.Empty;
            this.ctlScheduleMain.currentOpenTemplateName = string.Empty;
            this.ctlScheduleMain._patientScheduleTemplateRow = null;

        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        #endregion

        #region 方法

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

        #endregion
    }
}