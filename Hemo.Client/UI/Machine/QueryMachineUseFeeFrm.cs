/*----------------------------------------------------------------
// Copyright (C) 2005 (北京)医疗科技发展有限公司
// 文件名：QueryMachineUseFeeFrm.cs
// 文件功能描述：血液透析设备使用费用窗体类
// 创建标识：
// 修改时间：2014-5-4
// 修改人：吕志强
// 修改描述：添加区域、床位、班次栏位
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
using Hemo.IService.Machine;
using Hemo.Client.Doc;
using Hemo.IService.Config;

namespace Hemo.Client.UI.Machine
{
    public partial class QueryMachineUseFeeFrm :HemoBaseFrm
    {
        #region 成员变量

        /// <summary>
        /// 员工相关服务
        /// </summary>
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;

        /// <summary>
        /// 血透机相关服务
        /// </summary>
        private IMachine _machineService = ServiceManager.Instance.MachineService;

        /// <summary>
        /// 设备使用费用表（字段合并）
        /// </summary>
        private DataTable _useFeeTable = null;

        /// <summary>
        /// 设备使用费用统计表
        /// </summary>
        private DataTable _useFeeStatisticsTable = null;

        private IConfig _configService = ServiceManager.Instance.ConfigService;

        private const string FULL_ITEM_ID = "c570d95c-76a2-4af4-893a-1357065623bf";

        private MachineModel.MED_DIALYSIS_MACHINEDataTable _machineDataTable;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryMachineUseFeeFrm()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件
        private void dxSimpleButton1_Click(object sender, EventArgs e) {
            this.Close();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MachineUseFeeFrm_Load(object sender, EventArgs e)
        {
            this.beginTime.EditValue = System.DateTime.Now.Year.ToString() + "-" + System.DateTime.Now.Month.ToString();
            BindOptionList();
            Query();
        }

        /// <summary>
        /// 根据选择的时间范围查询数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Query_Click(object sender, EventArgs e)
        {
            if (!this.CheckInputData())
            {
                return;
            }
            Query();
        }

        /// <summary>
        /// 打印报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Print_Click(object sender, EventArgs e)
        {
            if (!this.CheckInputData())
            {
                return;
            }

            var doc = new 血透机使用费用统计();
            doc.UseFeeTable = this._useFeeTable;
            doc.SickArea = this.ediSickArea.Text;
            doc.Bednum = this.lookUpEdit_Bed.Text;
            DateTime date = DateTime.Parse(this.beginTime.EditValue.ToString());
            DateTime startDate = new DateTime(date.Year, date.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            try
            {
                this._useFeeStatisticsTable = this._machineService.GetUseFeeStatisticsData(startDate, endDate);
                doc.UseFeeStatisticsTable = this._useFeeStatisticsTable;
            }
            catch (Exception ex)
            {
                //日志
            }

            var frm = new ShowPrintForm(doc);
            frm.ShowDialog();
        }

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Entering_Click(object sender, EventArgs e)
        {
            if (!this.CheckInputData())
            {
                return;
            }

            var editMachineUseFee = new EditMachineUseFee(string.Empty) { SickAreak = this.ediSickArea.EditValue.ToString(), BedNum = this.lookUpEdit_Bed.EditValue.ToString(), BanCi = this.lookUpEdit_BanCI.EditValue.ToString() };
            if (editMachineUseFee.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //重新绑定数据
                Query();
            }
        }

        /// <summary>
        /// 数据行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMachine_DoubleClick(object sender, EventArgs e)
        {
            var row = gv_MachineUseFee.GetFocusedDataRow();
            if (row == null)
            {
                return;
            }

            if (row["id"] != DBNull.Value && !string.IsNullOrEmpty(row["id"].ToString()))
            {
                var editMachineUseFee = new EditMachineUseFee(row["id"].ToString());
                if (editMachineUseFee.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //重新绑定数据
                    Query();
                }
            }
        }

        /// <summary>
        /// 右键菜单删除按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolDelete_Click(object sender, EventArgs e)
        {
            var row = gv_MachineUseFee.GetFocusedDataRow();
            if (row == null)
            {
                return;
            }

            if (XtraMessageBox.Show("是否确定删除当前行数据？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            MachineModel.MED_MACHINE_USEFEERow useFeeRow = null;
            var useFeeTable = this.GetUseFeeTableById(row["id"].ToString(), ref useFeeRow);
            if (useFeeRow != null)
            {
                useFeeRow.ISDELETE = "1";
                this._machineService.SaveUseFeeData(useFeeTable);
            }

            Query();
        }

        /// <summary>
        /// 数据行单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcMachine_MouseDown(object sender, MouseEventArgs e)
        {
            var row = gv_MachineUseFee.GetFocusedDataRow();
            if (row == null)
            {
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(MousePosition);
            }
        }

        /// <summary>
        /// 病区显示值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ediSickArea_EditValueChanged(object sender, EventArgs e)
        {
            string sickArea = string.Empty;

            if (this.ediSickArea.EditValue == null || this.ediSickArea.EditValue.ToString() == FULL_ITEM_ID)
            {
                sickArea = string.Empty;
            }
            else
            {
                sickArea = "AREA_ID = '" + this.ediSickArea.EditValue.ToString() + "'";
            }

            var dtSub = Utility.GetSubTable(_machineDataTable, sickArea);
            Utility.BindLookUpEdit(this.lookUpEdit_Bed, "BED_ID", "CWNAME", dtSub, "CWNAME", "床位");
        }

        #endregion

        #region 方法

        /// <summary>
        /// 查询数据
        /// </summary>
        private void Query()
        {
            try
            {
                DateTime date = DateTime.Parse(this.beginTime.EditValue.ToString());
                DateTime startDate = new DateTime(date.Year, date.Month, 1);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);

                this._useFeeTable = _machineService.GetUseFeeData(this.ediSickArea.EditValue.ToString(), this.lookUpEdit_Bed.EditValue.ToString(), startDate, endDate);
                this.gc_MachineUseFee.DataSource = this._useFeeTable;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("查询数据失败！\n" + ex.Message, "提示");
            }
        }

        /// <summary>
        /// 验证输入数据的合法性
        /// </summary>
        private bool CheckInputData()
        {
            if (this.beginTime.EditValue == null)
            {
                XtraMessageBox.Show("请选择年月！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 根据给定的唯一ID号，获取对应的基本表和行
        /// </summary>
        /// <param name="id"></param>
        /// <param name="useFeeRow"></param>
        /// <returns></returns>
        private MachineModel.MED_MACHINE_USEFEEDataTable GetUseFeeTableById(string id, ref MachineModel.MED_MACHINE_USEFEERow useFeeRow)
        {
            try
            {
                var useFeeTable = this._machineService.GetUseFeeData(id);
                if (useFeeTable != null && useFeeTable.Rows.Count > 0)
                {
                    useFeeRow = (MachineModel.MED_MACHINE_USEFEERow)useFeeTable.Rows[0];
                }

                return useFeeTable;
            }
            catch (Exception ex)
            {
                //日志
            }

            return null;
        }

        /// <summary>
        /// 病区、床位、班次下拉选项绑定
        /// </summary>
        private void BindOptionList()
        {
            //病区
            ConfigModel.MED_COMMON_ITEMLISTDataTable config = this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1");

            if (config != null && config.Rows.Count > 0)
            {
                Utility.BindLookUpEdit(this.ediSickArea, "ITEM_ID", "ITEM_NAME", (DataTable)config, "ITEM_NAME", "区域");
                this.ediSickArea.EditValue = this.Tag;
            }

            //床位
            _machineDataTable = _machineService.GetMachineList();
            string sickArea = string.Empty;

            if (this.ediSickArea.EditValue == null || this.ediSickArea.EditValue.ToString() == FULL_ITEM_ID)
            {
                sickArea = string.Empty;
            }
            else
            {
                sickArea = "AREA_ID = '" + this.ediSickArea.EditValue.ToString() + "'";
            }

            var dtSub = Utility.GetSubTable(_machineDataTable, sickArea);
            Utility.BindLookUpEdit(this.lookUpEdit_Bed, "BED_ID", "CWNAME", dtSub, "CWNAME", "床位");

            //班次
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

            Utility.BindLookUpEdit(this.lookUpEdit_BanCI, "ITEM_ID", "ITEM_NAME", dtBANCI, "ITEM_NAME", "班次");

            DataTable dtStaffSict = _staffDictService.GetStaffDictList();

            if (dtStaffSict != null && dtStaffSict.Rows.Count > 0)
            {
                DataTable dtPunctureDoctorList = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'", "name");
                if (dtPunctureDoctorList != null && dtPunctureDoctorList.Rows.Count > 0)
                {
                    this.repositoryItemLookUpEdit3.DataSource = dtPunctureDoctorList;
                }
            }

            this.repositoryItemLookUpEdit4.DataSource = _machineDataTable;
            this.repositoryItemLookUpEdit5.DataSource = _machineDataTable;
            this.repositoryItemLookUpEdit6.DataSource = dtBANCI;
        }

        #endregion
    }
}