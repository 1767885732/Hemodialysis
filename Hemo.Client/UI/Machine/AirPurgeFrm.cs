/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：空气消毒记录查询维护窗体
// 创建时间：2016-06-25
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
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.Client.Print;
using Hemo.IService.Dict;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.UI.Machine {
    public partial class AirPurgeFrm :HemoBaseFrm
    {
        #region 类变量

        private Hemo.IService.Machine.IMachine _machineService = ServiceManager.Instance.MachineService;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private MachineModel.MED_MACHINE_AIRPURGEDataTable _date = null;

        #endregion

        #region 属性

        /// <summary>
        /// 透析室编号
        /// </summary>
        public string RoomID
        {
            set;
            get;
        }

        #endregion

        #region 构造函数

        public AirPurgeFrm()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void btn_Entering_Click(object sender, EventArgs e)
        {
            AirPurgeInput frm = new AirPurgeInput();
            frm.RoomID = RoomID;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                GetDataBydate();
            }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            if (this.beginTime.EditValue == null || this.endTime.EditValue == null)
            {
                XtraMessageBox.Show("请选择日期！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            if (DateTime.Parse(this.beginTime.EditValue.ToString()) > DateTime.Parse(this.endTime.EditValue.ToString()))
            {
                XtraMessageBox.Show("开始日期不能大于结束日期！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }



            AirPurgeReportList report = new AirPurgeReportList(Utility.CDate(this.beginTime.EditValue.ToString()), Utility.CDate(this.endTime.EditValue.ToString()), RoomID);
            ReportPrintTool pt = new ReportPrintTool(report);
            pt.ShowPreviewDialog();
        }

        private void btn_Query_Click(object sender, EventArgs e)
        {
            GetDataBydate();
        }

        private void gvMachine_DoubleClick(object sender, EventArgs e)
        {
            var row = gvMachine.GetFocusedDataRow() as MachineModel.MED_MACHINE_AIRPURGERow;
            if (row == null)
            {
                return;

            }

            AirPurgeInput frm = new AirPurgeInput();
            frm.RoomID = RoomID;
            frm.CurrentRow = row;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                GetDataBydate();
            }
        }

        private void AirPurgeFrm_Load(object sender, EventArgs e)
        {
            DateTime dt = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
            this.beginTime.EditValue = dt;
            this.endTime.EditValue = dt.AddMonths(1).AddDays(-1);

            DataTable dtStaffSict = _staffDictService.GetStaffDictList();
            if (dtStaffSict != null && dtStaffSict.Rows.Count > 0)
            {
                DataTable dtPunctureNurseList = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'", "name");
                if (dtPunctureNurseList != null && dtPunctureNurseList.Rows.Count > 0)
                {
                    this.repositoryItemLookUpEdit3.DataSource = dtPunctureNurseList;
                }
            }

            GetDataBydate();
        }

        private void toolDelete_Click(object sender, EventArgs e)
        {
            var row = gvMachine.GetFocusedDataRow() as MachineModel.MED_MACHINE_AIRPURGERow;
            if (row == null)
            {
                return;
            }
            if (XtraMessageBox.Show("是否确定删除当前行数据？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            row.ISDELETE = "1";
            _machineService.DeleteAirPurgeData(row.ID);//.SaveAirPurgeData(_date);
            //_date.AcceptChanges();
            GetDataBydate();
        }

        private void gcMachine_MouseDown(object sender, MouseEventArgs e)
        {
            var row = gvMachine.GetFocusedDataRow() as MachineModel.MED_MACHINE_AIRPURGERow;
            if (row == null)
            {
                return;
            }
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(MousePosition);
            }
        }

        private void dxSimpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 方法

        private void GetDataBydate()
        {
            _date = _machineService.GetAirPurgeData(Utilities.Utility.CDate(this.beginTime.EditValue.ToString()), Utilities.Utility.CDate(this.endTime.EditValue.ToString()), RoomID);
            this.gcMachine.DataSource = _date;
        }

        #endregion
    }
}