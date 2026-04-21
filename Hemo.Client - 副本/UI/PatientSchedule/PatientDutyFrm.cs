/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:当周医护人员排班类
 * 创建标识:贺建操-2017年3月18日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.IService.Dict;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.Model;
using Hemo.IService.PatientSchedule;
using System.Collections;
using Hemo.Client.Core;
using Hemo.IService.Permission;
using Hemo.IService.Config;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors;
using Hemo.Client.Print;
using Hemo.Client.UI.Hemodialysis;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.UI.PatientSchedule
{
    public partial class PatientDutyFrm :HemoBaseFrm
    {
        #region 变量
        
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private PermissionModel.MED_USERS_WEEKDUTYDataTable weekDuty = new PermissionModel.MED_USERS_WEEKDUTYDataTable();

        private IPatientSchedule _scheduleService = ServiceManager.Instance.PatientSchedule;
        private IPatientSchedule patientScheduleService = ServiceManager.Instance.PatientSchedule;
        private int _nextWeekDays = 0;
        private IUser _userService = ServiceManager.Instance.UserService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private DateTime startWeek = new DateTime();
        private DateTime endWeek = new DateTime();
        private DictModel.MED_STAFF_DICTDataTable dtStaffSict = new DictModel.MED_STAFF_DICTDataTable();
        private PermissionModel.MED_USERS_WEEKDUTYMASTERDataTable dutyMaster = new PermissionModel.MED_USERS_WEEKDUTYMASTERDataTable();
        #endregion

        #region 构造函数

        public PatientDutyFrm()
        {
            InitializeComponent();
            this.Text = "医生护士值班排班";
            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountFrm(this);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 数据初始化
        /// </summary>
        public void InizationData()
        {
            this.busyIndicator1.ShowLoadingScreenFor(this.gridControlForEmerger);
            dutyMaster = new PermissionModel.MED_USERS_WEEKDUTYMASTERDataTable();
       
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    //获取人员信息
                     dtStaffSict = _staffDictService.GetStaffDictList();
                    //获取值 班信息
                     weekDuty = _scheduleService.GetWeekDutyByDate(startWeek, endWeek);
                     if (weekDuty == null || weekDuty.Rows.Count <= 0)//没有排班数据时
                     {
                         if (startWeek > DateTime.Now)//确保为未来的数据
                         {
                             //创建数据.生成数据
                             int i = _scheduleService.CreateCurrntDataByLastWeek(startWeek.AddDays(-7), endWeek.AddDays(-7));
                             if (i > 0)
                             {
                                 //生成数据后查询
                                 weekDuty = _scheduleService.GetWeekDutyByDate(startWeek, endWeek);
                             }
                         }
                     }


                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    try
                    {
                        //获取所有护士
                        var users = dtStaffSict.Where(i => i.ZYNAME == "护士").ToList();
                        //生成Grid
                        users.ForEach(row =>
                        {
                            var newRow = dutyMaster.NewMED_USERS_WEEKDUTYMASTERRow();
                            newRow.USER_ID = row.EMP_NO;
                            newRow.USER_NAME = row.NAME;
                            newRow.TYPE = "N";
                            newRow.CREATEBY = HemoApplicationContext.Current.CurrentUser.USER_ID;
                            newRow.CREATEDATE = System.DateTime.Now;
                            foreach (PermissionModel.MED_USERS_WEEKDUTYRow k in weekDuty.Rows)
                            {
                                if (row.EMP_NO == k.USER_ID)
                                {                                    
                                    newRow[string.Format("{0}",k.WEEKDAY)] = k.WEEKDAY;
                                    newRow[string.Format("{0}DUTY", k.WEEKDAY)] = k.DUTYDAY;
                                    newRow[string.Format("{0}OFFICEID", k.WEEKDAY)] = k.OFFICEID;
                                    newRow[string.Format("{0}OFFICENAME", k.WEEKDAY)] = k.OFFICENAME;
                                    newRow.CREATEBY = k.CREATEBY;
                                    newRow.CREATEDATE = k.CREATEDATE;
                                }
                            }
                            dutyMaster.AddMED_USERS_WEEKDUTYMASTERRow(newRow);
                        });
                        //绑定..
                        this.gridControlForEmerger.DataSource = dutyMaster;
                    }
                    catch (Exception e)
                    {
                        AutoClosedMsgBox.ShowForm(e.Message,this.Text,1000,MessageBoxIcon.Error);
                    }
                 
                    this.busyIndicator1.HideLoadingScreen();
                };
                worker.RunWorkerAsync();
            }

        }

        /// <summary>
        /// 加载当前所在星期的时间段
        /// </summary>
        /// <param name="beginDate">星期一</param>
        /// <param name="endDate">星期日</param>
        public void LoadPatientScheduleData(DateTime beginDate, DateTime endDate)
        {
            //日期取服务器端值
       
            #region 赋值日期，gridView的columnsName
            //对于控件的列头进行变换赋值
            this.gridMonday.Caption = string.Format("星期一\r\n<{0}>", beginDate.ToString("yyyy-MM-dd"));   
            this.gridMonday.Tag = beginDate;
            DateTime dialysisDate = beginDate.AddDays(1);
            this.gridTuesday.Caption = string.Format("星期二\r\n<{0}>", dialysisDate.ToString("yyyy-MM-dd"));
            this.gridTuesday.Tag = dialysisDate;
            dialysisDate = beginDate.AddDays(2);
            this.gridWednesday.Caption = string.Format("星期三\r\n<{0}>", dialysisDate.ToString("yyyy-MM-dd"));
            this.gridWednesday.Tag = dialysisDate;
            dialysisDate = beginDate.AddDays(3);
            this.gridThursday.Caption = string.Format("星期四\r\n<{0}>", dialysisDate.ToString("yyyy-MM-dd"));
            this.gridThursday.Tag = dialysisDate;
            dialysisDate = beginDate.AddDays(4);
            this.gridFriday.Caption = string.Format("星期五\r\n<{0}>", dialysisDate.ToString("yyyy-MM-dd"));
            this.gridFriday.Tag = dialysisDate;
            dialysisDate = beginDate.AddDays(5);
            this.gridSaturday.Caption = string.Format("星期六\r\n<{0}>", dialysisDate.ToString("yyyy-MM-dd"));
            this.gridSaturday.Tag = dialysisDate;

            this.gridSunday.Caption = string.Format("星期日\r\n<{0}>", endDate.ToString("yyyy-MM-dd"));
            this.gridSunday.Tag = endDate;

            #endregion
        }

        /// <summary>
        /// 初始化日期..下周上周..
        /// </summary>
        private void CalculateWeek()
        {
            //日期取服务器端值
            DateTime date = Utility.CDate(patientScheduleService.GetServerDate());
            startWeek = Utility.GetMonday(date).AddDays(this._nextWeekDays).Date;
            endWeek = startWeek.AddDays(6).Date;
            LoadPatientScheduleData(startWeek, endWeek);
        }

        /// <summary>
        /// 保存护士值班数据
        /// </summary>
        private void SaveDutyNurse()
        {
            weekDuty = new PermissionModel.MED_USERS_WEEKDUTYDataTable();
            foreach (PermissionModel.MED_USERS_WEEKDUTYMASTERRow row in dutyMaster.Rows)
            {
                #region 进行显示与数据的行列转换,写入新表进行保存[正式库同样 出现了程序过大的问题，但本机未出现，同样采取分数据组方式进行保存测试。]

                SetRowData(row, gridMonday);
                SetRowData(row, gridSunday);
                SetRowData(row, gridSaturday);
                SetRowData(row, gridThursday);
                SetRowData(row, gridTuesday);
                SetRowData(row, gridWednesday);
                SetRowData(row, gridFriday);
          
                #endregion
            }
            if (XtraMessageBox.Show("确定保存值班信息吗？", "保存值班", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            if (_scheduleService.SaveWeekDutyData(weekDuty) > 0)
                AutoClosedMsgBox.ShowForm("保存成功！", "提示", 1000, MessageBoxIcon.Information);
            else
                AutoClosedMsgBox.ShowForm("保存排班信息失败！", "警告", 1000, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 设置行数据
        /// </summary>
        /// <param name="row"></param>
        /// <param name="gridCol"></param>
        private void SetRowData(PermissionModel.MED_USERS_WEEKDUTYMASTERRow row, GridColumn gridCol)
        {
            string filed = gridCol.Name.Substring(4);
            if (string.IsNullOrEmpty(row[string.Format("{0}", filed)].ToString()) || string.IsNullOrEmpty(row[string.Format("{0}OFFICEID", filed)].ToString()))
                return;
            var _dataRow = weekDuty.NewMED_USERS_WEEKDUTYRow();
            _dataRow.ID = Guid.NewGuid().ToString();
            _dataRow.USER_ID = row.USER_ID;                      
            _dataRow.WEEKDAY = row[string.Format("{0}", filed)].ToString();
            _dataRow.DUTYDAY = Convert.ToDateTime(row[string.Format("{0}DUTY", filed)].ToString());
            _dataRow.OFFICEID = row[string.Format("{0}OFFICEID", filed)].ToString();
            _dataRow.CREATEDATE = row.CREATEDATE;
            _dataRow.TYPE = row.TYPE;
            _dataRow.CREATEBY = row.CREATEBY;
            weekDuty.AddMED_USERS_WEEKDUTYRow(_dataRow);
        }
        
        /// <summary>
        /// 重置行数据
        /// </summary>
        /// <param name="row"></param>
        /// <param name="filed"></param>
        private void SetRowEmpty(PermissionModel.MED_USERS_WEEKDUTYMASTERRow row, string filed)
        {
            if (string.IsNullOrEmpty(row[string.Format("{0}", filed)].ToString()) || string.IsNullOrEmpty(row[string.Format("{0}OFFICEID", filed)].ToString()))
                return;
            row[string.Format("{0}",filed)] = string.Empty;
            row[string.Format("{0}OFFICEID", filed)] = string.Empty;
            row[string.Format("{0}OFFICENAME", filed)] = string.Empty;
            row[string.Format("{0}DUTY", filed)] = DBNull.Value;
            row.CREATEBY = string.Empty;
        }
        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientDutyFrm_Load(object sender, EventArgs e)
        {
            CalculateWeek();
            this.dutyDoctor1._date = Utility.CDate(patientScheduleService.GetServerDate());
            this.dutyDoctor1.InizationData();
            InizationData();
        }

        /// <summary>
        /// 上周
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_last_Click(object sender, EventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPage == this.TabPageDoctor)
            {
                this.dutyDoctor1.btnlast_Click();
            }
            else
            {
                _nextWeekDays -= 7;
                CalculateWeek();
                InizationData();

            }
        }

        /// <summary>
        /// 下周
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Next_Click(object sender, EventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPage == this.TabPageDoctor)
            {
                this.dutyDoctor1.btnNext_Click();
            }
            else
            {
                _nextWeekDays += 7;
                CalculateWeek();
                InizationData();

            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPage == this.TabPageDoctor)
            {
                if (this.dutyDoctor1.SaveDate() > 0)
                {
                    AutoClosedMsgBox.ShowForm("医生值信息保存成功!","值班",1000,MessageBoxIcon.Warning);
                }
            }
            else
            {
                SaveDutyNurse();
            }

        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 行点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var rowCurrent = this.gridView1.GetFocusedDataRow() as PermissionModel.MED_USERS_WEEKDUTYMASTERRow;

            if (rowCurrent == null || gridView1.FocusedColumn == null || gridView1.FocusedColumn.Name.ToString() == this.gridUSER.Name.ToString())
                return;

            var fileName = this.gridView1.FocusedColumn.Name.Substring(4);//获取星期几

            //打开患者录入界面
            if (e.Button == MouseButtons.Left && !this.gridView1.IsGroupRow(e.RowHandle))
            {
                #region

                this.contextMenuStrip1.Visible = false;

                if (e.Clicks == 2)
                {
                    UserDutyInputCtl frm = new UserDutyInputCtl();
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.itemName = string.Format("{0}", rowCurrent[this.gridView1.FocusedColumn.FieldName]);

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        //赋值
                        rowCurrent[string.Format("{0}", fileName)] = fileName;
                        rowCurrent[string.Format("{0}OFFICEID", fileName)] = frm.itemValue;
                        rowCurrent[string.Format("{0}OFFICENAME", fileName)] = frm.itemName;
                        rowCurrent[string.Format("{0}DUTY", fileName)] = Convert.ToDateTime(this.gridView1.FocusedColumn.Tag.ToString()).Date;
                        rowCurrent.CREATEBY = HemoApplicationContext.Current.CurrentUser.USER_ID;
                        rowCurrent.CREATEDATE = System.DateTime.Now;
                        return;
                    }
                }
                else if (e.Clicks == 1)
                {

                }

                #endregion
            }
            //右击菜单
            else if (e.Button == MouseButtons.Right && e.Clicks == 1)
            {
                this.contextMenuStrip1.Visible = true;
                this.contextMenuStrip1.Left = MousePosition.X;
                this.contextMenuStrip1.Top = MousePosition.Y;

            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Delete_Click(object sender, EventArgs e)
        {
            var row = this.gridView1.GetFocusedDataRow() as PermissionModel.MED_USERS_WEEKDUTYMASTERRow;
            if (row == null)
                return;
            if (DialogResult.Cancel == XtraMessageBox.Show("是否删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                return;
            var fileName = this.gridView1.FocusedColumn.Name.Substring(4);//获取星期几

            #region 删除
            SetRowEmpty(row, fileName);
            #endregion
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Print_Click(object sender, EventArgs e)
        {
            var dt = patientScheduleService.GetWeekDutyByTime(startWeek, endWeek);

            string timer = string.Format("{0}~{1}", startWeek.ToString("yyyy-MM-dd"), endWeek.ToString("yyyy-MM-dd"));
            NurseDutyReport frm = new NurseDutyReport(dt, timer);
            ReportPrintTool pt = new ReportPrintTool(frm);
            pt.ShowPreviewDialog();
        }

        /// <summary>
        /// TabIndex改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPage == this.TabPageDoctor)
            {
                this.btn_Print.Enabled = false;
            }
            else
            {
                this.btn_Print.Enabled = true;
            }
        }

        /// <summary>
        /// 选择页改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPage == this.TabPageDoctor)
            {
                this.btn_Print.Enabled = false;
            }
            else
            {
                this.btn_Print.Enabled = true;
            }
        }

        #endregion
    }
}

