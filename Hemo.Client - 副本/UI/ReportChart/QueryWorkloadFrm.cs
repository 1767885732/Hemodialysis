/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:工作量统计查询类
 * 创建标识:吕志强-2017年7月10日
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
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Client.UI.Machine;
using Hemo.Utilities;
using Hemo.Model;
using Hemo.IService.Dict;


namespace Hemo.Client.UI.ReportChart {
    public partial class QueryWorkloadFrm : HemoBaseFrm {

        #region 成员变量
        /// <summary>
        /// 工作量数据表
        /// </summary>
        private HemoModel.MED_WORKLOADDataTable workloadTable = new HemoModel.MED_WORKLOADDataTable();

        /// <summary>
        /// 数据服务层
        /// </summary>
        private IHemodialysis hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;

        public string banChi { get; set; }
        public DateTime date { get; set; }
        public string _workArea { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryWorkloadFrm() {
            InitializeComponent();

            var dtAREA = this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1");
            var row = dtAREA.NewMED_COMMON_ITEMLISTRow();
            row.ITEM_ID = "9999";
            row.ITEM_VALUE = "9999";
            row.ITEM_NAME = "全部";
            row.ITEM_TYPE = "区域";
            row.STATUS = "1";
            row.ORDER_NUMBER = 11;
            dtAREA.Rows.InsertAt(row, 0);

            Utility.BindLookUpEdit(txtWORKAREA, "ITEM_ID", "ITEM_NAME", dtAREA, "ITEM_NAME", "病室");

            var dtStaffSict = _staffDictService.GetStaffDictList();


            DataTable dtPunctureNurseList = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'", "name");


            if (dtPunctureNurseList != null && dtPunctureNurseList.Rows.Count > 0) {
                BaseControlInfo.BindLookUpEdit(txtTJR, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "记录护士");
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void QueryWorkload_Load(object sender, EventArgs e) {
            DateTime beginDt = Utility.CDate(string.Format("{0}/{1}/{2}", DateTime.Now.Year, DateTime.Now.Month, "1"));
            DateTime endDt = beginDt.AddMonths(1).AddDays(-1);
            this.deBeginTime.DateTime = beginDt;
            this.deEndTime.DateTime = endDt;
            Query();
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e) {
            Query();
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e) {
            // DateTime beginDt = Utility.CDate(string.Format("{0}/{1}/{2}", DateTime.Now.Year,DateTime.Now.Month,"1"));
            //DateTime endDt = beginDt.AddMonths(1).AddDays(-1);
            //     WorkloadReport report = new WorkloadReport(Utility.CDate(this.deBeginTime.DateTime.ToString()), Utility.CDate(this.deEndTime.DateTime.ToString()));
            //   report.ShowDialog();
            //DataTable workLoad = this.gcWorkload.DataSource as DataTable;
            //if (workLoad != null && workLoad.Rows.Count > 0) {
            //    WorkloadReport report = new WorkloadReport(workLoad);

            //    report.ShowDialog();
            //}

            DateTime beginDt = deBeginTime.DateTime;
            DateTime endDt = deEndTime.DateTime;

            WorkloadReport report = new WorkloadReport(beginDt, endDt);
            report.ShowDialog();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e) {
            EditWorkload editForm = new EditWorkload(this.workloadTable, null);
            editForm._workDate = date;
            editForm._workclassNum = banChi;
            editForm._workArea = _workArea;
            if (editForm.ShowDialog() == DialogResult.OK) {
                Query();
            }
        }

        /// <summary>
        /// 删除一行数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e) {
            if (this.gvWorkload.GetFocusedDataRow() == null) {
                AutoClosedMsgBox.ShowForm("请选择一行要删除的记录！", this.Text, 1000, MessageBoxIcon.Asterisk);
                return;
            }

            if (XtraMessageBox.Show("是否确认删除当前选择记录？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
                return;
            }

            HemoModel.MED_WORKLOADRow dr = ((DataRowView)this.gvWorkload.GetFocusedRow()).Row as HemoModel.MED_WORKLOADRow;
            if (dr != null) {
                dr.Delete();
                this.hemodialysisService.SaveWorkload(this.workloadTable);
            }
        }

        /// <summary>
        /// 双击列表行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcWorkload_DoubleClick(object sender, EventArgs e) {
            HemoModel.MED_WORKLOADRow dr = ((DataRowView)this.gvWorkload.GetFocusedRow()).Row as HemoModel.MED_WORKLOADRow;
            if (dr != null) {
                EditWorkload editForm = new EditWorkload(this.workloadTable, dr);
                if (editForm.ShowDialog() == DialogResult.OK) {
                    Query();
                }
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            HemoModel.MED_WORKLOADRow dr = ((DataRowView)this.gvWorkload.GetFocusedRow()).Row as HemoModel.MED_WORKLOADRow;
            if (dr != null)
            {
                EditWorkload editForm = new EditWorkload(this.workloadTable, dr);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    Query();
                }
            }
        }

        private void labelControl74_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region 方法
        /// <summary>
        /// 查询数据
        /// </summary>
        private void Query() {
            this.workloadTable = new HemoModel.MED_WORKLOADDataTable();
            var date = this.hemodialysisService.GetWorkloadByDate(this.deBeginTime.DateTime, this.deEndTime.DateTime);
            if (this.txtWORKAREA.EditValue != null && this.txtTJR.EditValue != null) {
                if (this.txtWORKAREA.EditValue.ToString() == "9999") {
                    if (this.txtTJR.EditValue.ToString() == "") {
                        date.CopyToDataTable<HemoModel.MED_WORKLOADRow>(this.workloadTable, LoadOption.PreserveChanges);
                    }
                    else {
                        date.Where(i => i.TJR == this.txtTJR.EditValue.ToString()).CopyToDataTable<HemoModel.MED_WORKLOADRow>(this.workloadTable, LoadOption.PreserveChanges);
                    }
                }
                else {
                    if (this.txtTJR.EditValue.ToString() == "") {
                        date.Where(i => i.WORKAREA == this.txtWORKAREA.EditValue.ToString()).CopyToDataTable<HemoModel.MED_WORKLOADRow>(this.workloadTable, LoadOption.PreserveChanges);
                    }
                    else {
                        date.Where(i => i.WORKAREA == this.txtWORKAREA.EditValue.ToString() && i.TJR == this.txtTJR.EditValue.ToString()).CopyToDataTable<HemoModel.MED_WORKLOADRow>(this.workloadTable, LoadOption.PreserveChanges);

                    }
                }
            }
            else if (this.txtWORKAREA.EditValue != null && this.txtTJR.EditValue == null) {
                if (this.txtWORKAREA.EditValue.ToString() == "9999") {
                    date.CopyToDataTable<HemoModel.MED_WORKLOADRow>(this.workloadTable, LoadOption.PreserveChanges);
                }
                else {
                    date.Where(i => i.WORKAREA == this.txtWORKAREA.EditValue.ToString()).CopyToDataTable<HemoModel.MED_WORKLOADRow>(this.workloadTable, LoadOption.PreserveChanges);

                }
            }
            else if (this.txtWORKAREA.EditValue == null && this.txtTJR.EditValue != null) {
                if (this.txtTJR.EditValue.ToString() == "") {
                    date.CopyToDataTable<HemoModel.MED_WORKLOADRow>(this.workloadTable, LoadOption.PreserveChanges);
                }
                else {
                    date.Where(i => i.TJR == this.txtTJR.EditValue.ToString()).CopyToDataTable<HemoModel.MED_WORKLOADRow>(this.workloadTable, LoadOption.PreserveChanges);

                }
            }
            else {
                date.CopyToDataTable<HemoModel.MED_WORKLOADRow>(this.workloadTable, LoadOption.PreserveChanges);
            }
            this.gcWorkload.DataSource = this.workloadTable;
        }
        #endregion
    }
}