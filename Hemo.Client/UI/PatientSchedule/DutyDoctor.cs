/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:当周值班医生排班类
 * 创建标识:贺建操-2017年3月11日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Client.UI.Machine;
using Hemo.IService.Dict;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.Model;
using Hemo.IService.PatientSchedule;
using System.Collections;
using Hemo.Client.Core;
using Hemo.IService.Permission;
using Hemo.IService.Config;

namespace Hemo.Client.UI.PatientSchedule
{
    [ToolboxItem(true)]
    public partial class DutyDoctor : ViewBase
    {
        #region 类变量

        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private PermissionModel.MED_USERS_WEEKDUTYDataTable weekDuty = new PermissionModel.MED_USERS_WEEKDUTYDataTable();

        private IPatientSchedule _scheduleService = ServiceManager.Instance.PatientSchedule;
        private IPatientSchedule patientScheduleService = ServiceManager.Instance.PatientSchedule;
        private int _nextWeekDays = 0;
        private IUser _userService = ServiceManager.Instance.UserService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private DateTime startWeek = new DateTime();
        private DateTime endWeek = new DateTime();

        #endregion

        #region 属性

        public DateTime _date { get; set; }

        #endregion

        #region 构造函数

        public DutyDoctor()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 下周
        /// </summary>
        public void btnNext_Click()
        {
            _nextWeekDays += 7;
            BandIniControl();
            BandInizationData();
            this.labelControl1.Text = string.Format("{0}~~{1}值班的医生", startWeek.ToString("yyyy-MM-dd"), endWeek.ToString("yyyy-MM-dd"));
            this.Text = this.labelControl1.Text;
        }

        /// <summary>
        /// 上周
        /// </summary>
        public void btnlast_Click()
        {
            _nextWeekDays -= 7;
            BandIniControl();

            BandInizationData();
            this.labelControl1.Text = string.Format("{0}~~{1}值班的医生", startWeek.ToString("yyyy-MM-dd"), endWeek.ToString("yyyy-MM-dd"));
            this.Text = this.labelControl1.Text;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InizationData()
        {
            BandIniControl();
            BandInizationData();
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void BandIniControl()
        {
            CalculateWeek();

            #region MyRegion
            this.lupMondayDoctor.Tag = startWeek;
            this.lupTuesdayDoctor.Tag = startWeek.AddDays(1);
            this.lupWednesdayDoctor.Tag = startWeek.AddDays(2);
            this.lupThursdayDoctor.Tag = startWeek.AddDays(3);
            this.lupFridayDoctor.Tag = startWeek.AddDays(4);
            this.lupSaturdayDoctor.Tag = startWeek.AddDays(5);
            this.lupSundayDoctor.Tag = startWeek.AddDays(6);

            #endregion

            DataTable dtStaffSict = _staffDictService.GetStaffDictList();

            //科室
            var offices = this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1");

            if (dtStaffSict != null && dtStaffSict.Rows.Count > 0)
            {
                DataTable dtDoctorList = Utility.GetSubTable(dtStaffSict, "ZYNAME='医生'", "NAME");
                if (dtDoctorList != null && dtDoctorList.Rows.Count > 0)
                {
                    BindCheckCombox(lupMondayDoctor, "EMP_NO", "NAME", dtDoctorList);
                    BindCheckCombox(lupTuesdayDoctor, "EMP_NO", "NAME", dtDoctorList);
                    BindCheckCombox(lupWednesdayDoctor, "EMP_NO", "NAME", dtDoctorList);
                    BindCheckCombox(lupThursdayDoctor, "EMP_NO", "NAME", dtDoctorList);
                    BindCheckCombox(lupFridayDoctor, "EMP_NO", "NAME", dtDoctorList);
                    BindCheckCombox(lupSaturdayDoctor, "EMP_NO", "NAME", dtDoctorList);
                    BindCheckCombox(lupSundayDoctor, "EMP_NO", "NAME", dtDoctorList);
                }
            }
        }

        /// <summary>
        /// 下拉项绑定
        /// </summary>
        /// <param name="control"></param>
        /// <param name="valuemember"></param>
        /// <param name="displaymember"></param>
        /// <param name="dtsourse"></param>
        private void BindCheckCombox(DevExpress.XtraEditors.CheckedComboBoxEdit control, string valuemember, string displaymember, DataTable dtsourse)
        {
            control.Properties.ValueMember = valuemember;
            control.Properties.DisplayMember = displaymember;
            control.Properties.DataSource = dtsourse;
        }

        /// <summary>
        /// 初始化时间范围
        /// </summary>
        private void CalculateWeek()
        {
            //日期取服务器端值
            //DateTime date = Utility.CDate(patientScheduleService.GetServerDate());
            startWeek = Utility.GetMonday(_date).AddDays(this._nextWeekDays).Date;
            endWeek = startWeek.AddDays(6).Date;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void BandInizationData()
        {
            using (var worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender, DoWorkEventArgs e)
                {
                    weekDuty = _scheduleService.GetWeekDutyByDateDoctor(startWeek, endWeek);

                    if (weekDuty == null || weekDuty.Rows.Count <= 0)
                    {
                        if (startWeek > DateTime.Now)
                        {
                            int i = _scheduleService.CreateCurrntDataByLastWeekDoctor(startWeek.AddDays(-7), endWeek.AddDays(-7));
                            if (i > 0)
                            {
                                weekDuty = _scheduleService.GetWeekDutyByDateDoctor(startWeek, endWeek);
                            }
                        }
                    }

                    //获取透析室


                };
                worker.RunWorkerCompleted += delegate(object sender1, RunWorkerCompletedEventArgs e1)
                {
                    if (weekDuty != null && weekDuty.Rows.Count > 0)
                    {
                        var nurse = weekDuty.Where(i => i.TYPE == "N");
                        var doctor = weekDuty.Where(i => i.TYPE == "D");
                        doctor.ToList().ForEach(row =>
                        {
                            switch (row.WEEKDAY)
                            {
                                case "Monday":
                                    lupMondayDoctor.EditValue = row.USER_ID;
                                    lupMondayDoctor.RefreshEditValue();
                                    break;
                                case "Tuesday":
                                    lupTuesdayDoctor.EditValue = row.USER_ID;
                                    lupTuesdayDoctor.RefreshEditValue();
                                    break;
                                case "Wednesday":
                                    lupWednesdayDoctor.EditValue = row.USER_ID;
                                    lupWednesdayDoctor.RefreshEditValue();
                                    break;
                                case "Thursday":
                                    lupThursdayDoctor.EditValue = row.USER_ID;
                                    lupThursdayDoctor.RefreshEditValue();
                                    break;
                                case "Friday":
                                    lupFridayDoctor.EditValue = row.USER_ID;
                                    lupFridayDoctor.RefreshEditValue();
                                    break;
                                case "Saturday":
                                    lupSaturdayDoctor.EditValue = row.USER_ID;
                                    lupSaturdayDoctor.RefreshEditValue();
                                    break;
                                case "Sunday":
                                    lupSundayDoctor.EditValue = row.USER_ID;
                                    lupSundayDoctor.RefreshEditValue();
                                    break;

                            }
                        });
                    }
                    else
                    {
                        foreach (var edit in this.tableLayoutPanel1.Controls)
                        {
                            if (edit is DevExpress.XtraEditors.CheckedComboBoxEdit)
                            {
                                var control = edit as DevExpress.XtraEditors.CheckedComboBoxEdit;
                                control.EditValue = string.Empty;
                            }
                        }

                    }

                };
                worker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public int SaveDate()
        {
            ArrayList al = new ArrayList() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            weekDuty = new PermissionModel.MED_USERS_WEEKDUTYDataTable();
            for (int i = 0; i < al.Count; i++)
            {
                foreach (var edit in this.tableLayoutPanel1.Controls)
                {
                    if (edit is DevExpress.XtraEditors.CheckedComboBoxEdit)
                    {
                        var control = edit as DevExpress.XtraEditors.CheckedComboBoxEdit;
                        if (string.IsNullOrEmpty(control.EditValue.ToString()))
                            continue;
                        if (control.Name.Equals(string.Format("lup{0}Doctor", al[i].ToString())))
                        {
                            var rowDoc = weekDuty.NewMED_USERS_WEEKDUTYRow();
                            rowDoc.ID = Guid.NewGuid().ToString();
                            rowDoc.USER_ID = control.EditValue.ToString();
                            rowDoc.WEEKDAY = Convert.ToDateTime(control.Tag.ToString()).DayOfWeek.ToString();
                            rowDoc.DUTYDAY = Convert.ToDateTime(control.Tag.ToString());
                            rowDoc.CREATEBY = Hemo.Client.Core.HemoApplicationContext.Current.CurrentUser.USER_ID;
                            rowDoc.CREATEDATE = DateTime.Now;
                            rowDoc.TYPE = "D";
                            weekDuty.AddMED_USERS_WEEKDUTYRow(rowDoc);
                        }
                    }
                }
            }

            return _scheduleService.SaveWeekDutyData(weekDuty);
        }

        #endregion
    }
}
