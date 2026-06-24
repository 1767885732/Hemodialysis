/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：血透机运行记录维护用户控件类
// 创建时间：2016-05-28
// 创建者：吕志强
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
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Machine;
using Hemo.Service;
using Hemo.Model;
using System.Linq;
using Hemo.IService.Dict;
using Hemo.Utilities;
using Hemo.Client.Core;
using Hemo.IService;
using Hemo.IService.PatientSchedule;
using Hemo.IService.Config;

namespace Hemo.Client.UI.Machine
{
    public partial class MachineUseRecordNew : ViewBase
    {
        #region 成员变量

        private IMachine machineService = ServiceManager.Instance.MachineService;

        private IStaffDict staffService = ServiceManager.Instance.StaffDictService;

        private IConfig configService = ServiceManager.Instance.ConfigService;

        private MachineModel.MED_MACHINE_USERECORDDataTable dtRecord = null;

        private MachineModel.MED_DIALYSIS_MACHINEDataTable dtDialysisMachine = null;

        private MachineModel.MED_DIALYSIS_MACHINEDataTable dtMachineList = null;

        private string areaId = string.Empty;

        private const string NORMAL_STATE = "正常";

        private const string DEFAULT_TIME = "5";

        private bool isFirstLoad = false;

        #endregion

        #region 属性

        /// <summary>
        /// 病区ID
        /// </summary>
        public string AreaId
        {
            get { return areaId; }
            set { areaId = value; }
        }

        #endregion

        #region 构造函数

        public MachineUseRecordNew()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MachineUseRecordNew_Load(object sender, EventArgs e)
        {
            //this.deBeginDate.DateTime = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            //this.deEndDate.DateTime = DateTime.Now.AddDays(-DateTime.Now.Day + 1).AddMonths(1).AddDays(-1);

            if (this.ParentForm.GetType() != typeof(MachineList))
            {
                //this.btnAdd.Visible = false;
                //this.btnEdit.Visible = false;
                //this.btnDelete.Visible = false;
                //this.btnClose.Visible = false;
                //this.btnSave.Visible = false;
                this.AreaId = "86e8515c-70ce-49df-b470-6ad68e384b32";
            }


            this.deBeginDate.DateTime = DateTime.Now;
            this.deEndDate.DateTime = DateTime.Now;
            isFirstLoad = true;
            BindLookUpEdit();
            isFirstLoad = false;
            Query();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            #region 控制护士录入时间在限定的时间范围内

            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            string banCi = this.lupBanCi.EditValue.ToString();

            if (banCi.Equals("1"))
            {
                start = Utility.CDate(start.ToShortDateString() + " 7:30");
                end = Utility.CDate(end.ToShortDateString() + " 11:30");
            }
            else if (banCi.Equals("2"))
            {
                start = Utility.CDate(start.ToShortDateString() + " 12:30");
                end = Utility.CDate(end.ToShortDateString() + " 16:30");
            }
            else if (banCi.Equals("3"))
            {
                start = Utility.CDate(start.ToShortDateString() + " 18:00");
                end = Utility.CDate(end.ToShortDateString() + " 22:00");
            }

            //注释，以免影响护士补录记录
            //var dtConfig = configService.GetConfigList(string.Empty, string.Empty, "质控校验", "1");
            //if (Utility.GetRunScheduleByType("血透机运行记录", start, end, dtConfig))
            //{
            //    XtraMessageBox.Show("未在上下机时间段内，无法录入！", "血透机运行记录", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            #endregion

            dtRecord = dtRecord ?? new MachineModel.MED_MACHINE_USERECORDDataTable();
            //注释，以免影响护士补录记录
            //var drRecord = dtRecord.FirstOrDefault(row => row.USEDATE == DateTime.Now.Date && row.BANCI_ID.Equals(this.lupBanCi.EditValue.ToString()));
            //if (drRecord != null)
            //{
            //    XtraMessageBox.Show("当前日期当前班次对应的记录已经存在！");
            //    return;
            //}

            var drMachine = dtMachineList.FirstOrDefault(row => row.AREA_ID.Equals(this.lupArea.EditValue.ToString()) && row.BED_ID.Equals(this.lupBed.EditValue.ToString()));
            var drRecord = dtRecord.NewMED_MACHINE_USERECORDRow();

            drRecord.RECORD_ID = Guid.NewGuid().ToString();
            drRecord.MACHINE_ID = drMachine.MACHINE_ID;
            drRecord.MACHINE_NAME = drMachine.MACHINE_NAME;
            drRecord.MACHINE_TYPE = drMachine.TYPE;
            drRecord.MACHINE_MODEL = drMachine.MACHINE_MODEL;
            (drRecord as DataRow)["MACHINE_FULL_NAME"] = drMachine.MACHINE_MODEL + "-" + drMachine.MACHINE_NAME;
            (drRecord as DataRow)["FLNAME"] = drMachine.FLNAME;
            drRecord.THERAPEUTIC_PROPERTIES = drMachine.THERAPEUTIC_PROPERTIES;
            drRecord.BANCI_ID = this.lupBanCi.EditValue.ToString();
            drRecord.DIALYSIS_ROOM_ID = this.lupArea.EditValue.ToString();
            drRecord.BED_NUMBER = this.lupBed.EditValue.ToString();
            drRecord.MACHINE_CHECK = NORMAL_STATE;
            drRecord.MACHINE_ALARM = NORMAL_STATE;
            drRecord.DEGASSING = NORMAL_STATE;
            drRecord.WORKING = NORMAL_STATE;
            drRecord.USERTIME = DEFAULT_TIME;
            drRecord.INNER_DEGASSING = "1";
            drRecord.CLASS_WAY = "热化学消毒";
            drRecord.OUTER_DEGASSING = "1";
            drRecord.DAY_WAY = "含氯制剂擦拭";
            drRecord.USEDATE = DateTime.Now.Date;
            drRecord.CREATEDATE = DateTime.Now;
            drRecord.ISDELETE = "0";
            drRecord.SIGN_NAME = HemoApplicationContext.Current.CurrentUser.EMP_NO;
            if (banCi.Equals("1"))
            {
                drRecord.ON_TIME = "07:30";
                drRecord.OFF_TIME = "11:30";
                drRecord.CLASS_TIME = "12:00";
                drRecord.DAY_TIME = "12:00";
            }
            else if (banCi.Equals("2"))
            {
                drRecord.ON_TIME = "12:30";
                drRecord.OFF_TIME = "16:30";
                drRecord.CLASS_TIME = "17:00";
                drRecord.DAY_TIME = "17:00";
            }
            else if (banCi.Equals("3"))
            {
                drRecord.ON_TIME = "18:00";
                drRecord.OFF_TIME = "22:00";
                drRecord.CLASS_TIME = "22:30";
                drRecord.DAY_TIME = "22:30";
            }

            dtRecord.AddMED_MACHINE_USERECORDRow(drRecord);
            SetColumnEditable(true);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            SetColumnEditable(true);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var row = this.gvRecord.GetFocusedDataRow();

            if (row == null)
            {
                XtraMessageBox.Show("请选择要删除的行！");
                return;
            }

            if (XtraMessageBox.Show("确定要删除选中的行吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    row = dtRecord.FindByRECORD_ID(row["RECORD_ID"].ToString());
                    row["ISDELETE"] = "1";
                    machineService.SaveMachineUserRecord(dtRecord);
                    dtRecord.AcceptChanges();
                    XtraMessageBox.Show("删除成功！", "提示");
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("删除失败！\n" + ex.Message, "提示");
                }
            }

            Query();
            SetColumnEditable(false);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                machineService.SaveMachineUserRecord(dtRecord);
                dtRecord.AcceptChanges();
                AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("保存失败！\n" + ex.Message, "提示");
            }

            Query();
            SetColumnEditable(false);
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            UseRecordReport report = new UseRecordReport();
            report.IsMachineUseRecord = true;
            report.Area = this.lupArea.Text;
            report.Bed = this.lupBed.Text;
            report.RowMachine = (this.lupMachine.GetSelectedDataRow() as DataRowView).Row;
            report.ShowDialog();
        }


        /// <summary>
        /// 病区改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lupArea_EditValueChanged(object sender, EventArgs e)
        {
            if (!isFirstLoad)
            {
                #region 机器编号

                var rows = dtDialysisMachine.Select("AREA_ID='" + this.lupArea.EditValue.ToString() + "'");
                DataTable dtSub = dtDialysisMachine.Clone();
                rows.AsEnumerable().ToList().ForEach(r => dtSub.ImportRow(r));
                if (dtSub.Rows.Count > 0)
                {
                    this.lupMachine.Properties.DataSource = dtSub;
                }

                #endregion

                #region 床位

                string strSelect = string.Empty;
                strSelect = this.lupArea.EditValue == null ? string.Empty : "AREA_ID = '" + this.lupArea.EditValue.ToString() + "'";
                var dtBed = Utility.GetSubTable(dtMachineList, strSelect, "CWNO");
                Utility.BindLookUpEdit(this.lupBed, "BED_ID", "CWNAME", dtBed, "CWNAME", "床位");

                #endregion
            }
        }

        /// <summary>
        /// 床位改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lupBed_EditValueChanged(object sender, EventArgs e)
        {
            this.lupMachine.EditValue = (this.lupBed.GetSelectedDataRow() as DataRowView).Row["MACHINE_ID"].ToString();
            if (!isFirstLoad)
            {
                Query();
            }
        }

        /// <summary>
        /// 班次改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lupBanCi_EditValueChanged(object sender, EventArgs e)
        {
            if (!isFirstLoad)
            {
                Query();
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 绑定下拉项
        /// </summary>
        private void BindLookUpEdit()
        {
            dtMachineList = machineService.GetMachineList();

            #region 病区

            ConfigModel.MED_COMMON_ITEMLISTDataTable dtArea = configService.GetConfigList(string.Empty, string.Empty, "区域", "1");
            if (dtArea != null && dtArea.Rows.Count > 0)
            {
                Utility.BindLookUpEdit(this.lupArea, "ITEM_ID", "ITEM_NAME", (DataTable)dtArea, "ITEM_NAME", "区域");
                this.lupArea.EditValue = areaId;
            }

            this.repositoryItemLookUpEdit3.DataSource = dtMachineList;

            #endregion

            #region 机器编号

            dtDialysisMachine = machineService.GetMachineListByType("血透机品牌");
            var rows = dtDialysisMachine.Select("AREA_ID='" + this.lupArea.EditValue.ToString() + "'");
            DataTable dtSub = dtDialysisMachine.Clone();
            rows.AsEnumerable().ToList().ForEach(r => dtSub.ImportRow(r));
            if (dtSub.Rows.Count > 0)
            {
                this.lupMachine.Properties.DataSource = dtSub;
            }

            #endregion

            #region 床位

            string strSelect = string.Empty;
            strSelect = this.lupArea.EditValue == null ? string.Empty : "AREA_ID = '" + this.lupArea.EditValue.ToString() + "'";
            var dtBed = Utility.GetSubTable(dtMachineList, strSelect, "CWNO");
            Utility.BindLookUpEdit(this.lupBed, "BED_ID", "CWNAME", dtBed, "CWNAME", "床位");

            this.repositoryItemLookUpEdit4.DataSource = dtMachineList;

            #endregion

            #region 班次

            ConfigModel.MED_COMMON_ITEMLISTDataTable dtBanCi = configService.GetConfigList(string.Empty, string.Empty, "班次", "1");
            if (dtBanCi != null && dtBanCi.Rows.Count > 0)
            {
                Utility.BindLookUpEdit(this.lupBanCi, "ITEM_VALUE", "ITEM_NAME", dtBanCi, "ITEM_NAME", "班次");
                this.repositoryItemLookUpEdit2.DataSource = dtBanCi;
            }

            #endregion

            #region 操作者

            DataTable dtStaff = staffService.GetStaffDictList();
            dtStaff = Utility.GetSubTable(dtStaff, "ZYNAME='护士'", "NAME");
            this.repositoryItemLookUpEdit1.DataSource = dtStaff;

            #endregion

            #region 机器是否消毒

            DataTable dtFlag = new DataTable();
            dtFlag.Columns.Add(new DataColumn("ITEM_VALUE"));
            dtFlag.Columns.Add(new DataColumn("ITEM_FLAG"));

            DataRow row = dtFlag.NewRow();
            row["ITEM_VALUE"] = "0";
            row["ITEM_FLAG"] = "×";
            dtFlag.Rows.Add(row);

            row = dtFlag.NewRow();
            row["ITEM_VALUE"] = "1";
            row["ITEM_FLAG"] = "√";
            dtFlag.Rows.Add(row);

            this.repositoryItemLookUpEdit5.DataSource = dtFlag;
            this.repositoryItemLookUpEdit6.DataSource = dtFlag;

            #endregion
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            string area = this.lupArea.EditValue != null ? this.lupArea.EditValue.ToString() : string.Empty;
            string bed = this.lupBed.EditValue != null ? this.lupBed.EditValue.ToString() : string.Empty;
            string banCi = this.lupBanCi.EditValue != null ? this.lupBanCi.EditValue.ToString() : string.Empty;
            this.gcRecord.DataSource = dtRecord = machineService.GetMachineUseRecordList(area, bed, banCi, this.deBeginDate.DateTime, this.deEndDate.DateTime);
        }

        /// <summary>
        /// 设置列可编辑属性
        /// </summary>
        /// <param name="flag"></param>
        private void SetColumnEditable(bool flag)
        {
            this.gvRecord.Columns["USEDATE"].OptionsColumn.AllowEdit = flag;
            this.gvRecord.Columns["MACHINE_CHECK"].OptionsColumn.AllowEdit = flag;
            this.gvRecord.Columns["MACHINE_ALARM"].OptionsColumn.AllowEdit = flag;
            this.gvRecord.Columns["DEGASSING"].OptionsColumn.AllowEdit = flag;
            this.gvRecord.Columns["WORKING"].OptionsColumn.AllowEdit = flag;
            this.gvRecord.Columns["USERTIME"].OptionsColumn.AllowEdit = flag;
            this.gvRecord.Columns["INNER_DEGASSING"].OptionsColumn.AllowEdit = flag;
            this.gvRecord.Columns["CLASS_WAY"].OptionsColumn.AllowEdit = flag;
            this.gvRecord.Columns["OUTER_DEGASSING"].OptionsColumn.AllowEdit = flag;
            this.gvRecord.Columns["DAY_WAY"].OptionsColumn.AllowEdit = flag;
            this.gvRecord.Columns["SIGN_NAME"].OptionsColumn.AllowEdit = flag;
            this.gvRecord.Columns["DEALWITH"].OptionsColumn.AllowEdit = flag;
        }

       
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this.ParentForm.GetType() == typeof(MachineList))
            {
                this.ParentForm.Close();
            }
            else
            {

            }

        }
        #endregion
    }
}
