/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：复用记录维护窗体类
// 创建时间：2015-06-19
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using Hemo.Model;
using Hemo.IService.Machine;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.IService.Config;
using Hemo.Client.Print;
using Hemo.IService.Dict;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.UI.Machine {
    public partial class ReUsableRecord :HemoBaseFrm
    {
        #region 类变量

        private PatientModel.MED_PATIENTSRow _patientRow;

        private IConfig _configService = ServiceManager.Instance.ConfigService;

        private IMachine _machineService = ServiceManager.Instance.MachineService;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private MachineModel.MED_MACHINE_REUSABLEDataTable _date = null;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public ReUsableRecord(PatientModel.MED_PATIENTSRow patientRow)
        {
            InitializeComponent();
            this._patientRow = patientRow;
            this.Text = string.Format("姓名:{0}  性别:{1}  病案号:{2} 病人ID:{3}", this._patientRow.NAME, this._patientRow.SEX, this._patientRow.HEMODIALYSIS_ID, this._patientRow.PATIENT_ID);

            BaseControlInfo.BindLookUpEdit(lookUpEdit_machine, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "净化器类型", "1"), "ITEM_NAME", "净化器类型");

            DataTable dtStaffSict = _staffDictService.GetStaffDictList();
            if (dtStaffSict != null && dtStaffSict.Rows.Count > 0)
            {
                DataTable dtPunctureNurseList = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'", "name");
                if (dtPunctureNurseList != null && dtPunctureNurseList.Rows.Count > 0)
                {
                    this.repositoryItemLookUpEdit2.DataSource = dtPunctureNurseList;
                }
            }

            this.repositoryItemLookUpEdit1.DataSource = this._configService.GetConfigList(string.Empty, string.Empty, "净化器类型", "1");

            DateTime dt = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
            this.beginTime.EditValue = dt;
            this.endTime.EditValue = dt.AddMonths(1).AddDays(-1);

            this.lookUpEdit_machine.EditValue = "";
        }

        #endregion

        #region 事件

        private void btn_Entering_Click(object sender, EventArgs e)
        {
            ReUsableEnter frm = new ReUsableEnter(this._patientRow);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                btn_Query_Click(sender, e);
            }
        }

        private void btn_Query_Click(object sender, EventArgs e)
        {
            DateTime start = new DateTime(Utility.CDate(this.beginTime.EditValue.ToString()).Year, Utility.CDate(this.beginTime.EditValue.ToString()).Month, Utility.CDate(this.beginTime.EditValue.ToString()).Day, 0, 0, 0);
            DateTime end = new DateTime(Utility.CDate(this.endTime.EditValue.ToString()).Year, Utility.CDate(this.endTime.EditValue.ToString()).Month, Utility.CDate(this.endTime.EditValue.ToString()).Day, 23, 59, 59);
            LoadDateByParams(_patientRow.HEMODIALYSIS_ID.ToString(), start, end, lookUpEdit_machine.EditValue.ToString());
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            MachineReUsableReport _report = new MachineReUsableReport(_patientRow, Utility.CDate(this.beginTime.EditValue.ToString()), Utility.CDate(this.endTime.EditValue.ToString()));
            ReportPrintTool pt = new ReportPrintTool(_report);
            pt.ShowPreviewDialog();
        }

        private void ReUsableRecord_Load(object sender, EventArgs e)
        {
            LoadDateByParams(_patientRow.HEMODIALYSIS_ID.ToString(), Utility.CDate(this.beginTime.EditValue.ToString()), Utility.CDate(this.endTime.EditValue.ToString()), lookUpEdit_machine.EditValue.ToString());
        }

        private void gvMachine_DoubleClick(object sender, EventArgs e)
        {
            var dr = gvMachine.GetFocusedDataRow() as MachineModel.MED_MACHINE_REUSABLERow;
            if (dr == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请选择行", "提醒");
                return;
            }
            ReUsableEnter frm = new ReUsableEnter(this._patientRow);
            frm.Current = dr;
            frm.ShowDialog();



        }

        private void gcMachine_MouseDown(object sender, MouseEventArgs e)
        {
            var row = gvMachine.GetFocusedDataRow() as MachineModel.MED_MACHINE_REUSABLERow;
            if (row == null)
            {
                return;

            }
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(MousePosition);
            }
        }

        private void toolDelete_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("是否确定删除当前行数据？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            var row = gvMachine.GetFocusedDataRow() as MachineModel.MED_MACHINE_REUSABLERow;
            if (row == null)
            {
                return;

            }
            row.ISDELETE = "1";
            _machineService.SaveReUsableDatas(_date);
            _date.AcceptChanges();
            LoadDateByParams(_patientRow.HEMODIALYSIS_ID.ToString(), Utility.CDate(this.beginTime.EditValue.ToString()), Utility.CDate(this.endTime.EditValue.ToString()), lookUpEdit_machine.EditValue.ToString());

        }

        #endregion

        #region 方法

        private void LoadDateByParams(string hemoID, DateTime begintime, DateTime endtime, string machineType)
        {
            this.busyIndicator1.ShowLoadingScreenFor(this.gcMachine);
            _date = new MachineModel.MED_MACHINE_REUSABLEDataTable();
            using (var _worker = new BackgroundWorker())
            {
                _worker.DoWork += delegate(object sender, DoWorkEventArgs e)
                {
                    _date = _machineService.GetReUsableData(hemoID, begintime, endtime, machineType);
                };

                _worker.RunWorkerCompleted += delegate(object sender1, RunWorkerCompletedEventArgs e1)
                {
                    if (_date != null && _date.Rows.Count >= 0)
                    {
                        this.gcMachine.DataSource = _date;
                    }
                    this.busyIndicator1.Hide();

                };
                _worker.RunWorkerAsync();
            }
        }

        #endregion
    }
}