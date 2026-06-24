/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:护士交班
 * 创建标识:刘超-2013年7月20日
 * 
 * 修改时间:2013年10月28日
 * 修改人:刘超
 * 修改描述:新增方法
 * 
 * 修改时间:2014年2月5日
 * 修改人:贺建操
 * 修改描述:新增方法
 * 
 * 修改时间:2014年5月16日
 * 修改人:刘超
 * 修改描述:新增方法SQL
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Model;
using Hemo.IService;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.IService.PatientSchedule;
using Hemo.Client.Print;

namespace Hemo.Client.UI.Hemodialysis
{
    /// <summary>
    /// 护士交班
    /// </summary>
    public partial class NurseChangeWork : HemoBaseFrm
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public NurseChangeWork()
        {
            InitializeComponent();
        }


        #region 变量
        private  HemoModel.MED_HEMO_CHAGEWORKDataTable _workMaster = new HemoModel.MED_HEMO_CHAGEWORKDataTable();
        private HemoModel.MED_HEMO_CHAGEWORK_EXTENDDataTable _workExtend = new HemoModel.MED_HEMO_CHAGEWORK_EXTENDDataTable();
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        private IPatientSchedule patientScheduleService = ServiceManager.Instance.PatientSchedule;

        public string currentArea { get; set; }
       
        private DateTime dtMaster;
        #endregion


        #region 方法
        private void InzationData()
        {
            using (var _worker = new BackgroundWorker())
            {
                DateTime currentDt = Convert.ToDateTime(string.Format("{0}/{1}/01", this.date_QueryDate.DateTime.Year.ToString(),this.date_QueryDate.DateTime.Month));
                var begionTime = currentDt.AddDays(1 - currentDt.Day);
                var endTime = currentDt.AddDays(1 - currentDt.Day).AddMonths(1).AddDays(-1);
                _worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    _workMaster = objPatient.GetChangeNurseWorkByDate(begionTime, endTime);
                    _workExtend = objPatient.GetChageWorkExtendByMonth(string.Format("{0}/{1}",this.date_QueryDate.DateTime.Year.ToString().Trim(),this.date_QueryDate.DateTime.Month.ToString().Trim()));
                };
                _worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    foreach (HemoModel.MED_HEMO_CHAGEWORK_EXTENDRow row in _workExtend.Rows)
                    {
                        foreach (HemoModel.MED_HEMO_CHAGEWORKRow rowMaster in _workMaster.Rows)
                        {
                            if (rowMaster.CHANGETIME == row.CHANGETIME)
                            {
                                rowMaster.EXTENDID = row.ID;
                            }
                        }
                    }
                    this.gridControl2.DataSource = _workExtend;
                    if (dtMaster != null && this.xtraTabControl1.SelectedTabPage == this.xtraTabeDetail)
                    {
                        var dtDetail = new HemoModel.MED_HEMO_CHAGEWORKDataTable();
                        _workMaster.Where(i => i.CHANGETIME == dtMaster).CopyToDataTable(dtDetail, LoadOption.PreserveChanges);
                        if (dtDetail != null && dtDetail.Rows.Count > 0)
                            this.gridControl1.DataSource = dtDetail;
                    }

                };
                _worker.RunWorkerAsync();
            }
        }
        private void InitLupEditor()
        {

            this.date_QueryDate.DateTime = Utility.CDate(patientScheduleService.GetServerDate());

            //DataTable dtMonth = new DataTable();
            //dtMonth.Columns.Add(new DataColumn("Month"));
            //DataRow rowMonth;

            //for (int i = 1; i <= 12; i++)
            //{
            //    rowMonth = dtMonth.NewRow();
            //    rowMonth["Month"] = i.ToString() + "月";
            //    dtMonth.Rows.Add(rowMonth);
            //}

            //BaseControlInfo.BindLookUpEdit(this.lookUpMonth, "Month", "Month", dtMonth, "Month", "月度");

            //this.lookUpMonth.Text = DateTime.Now.Month.ToString();
        }
        #endregion


        #region 事件


        private void NurseChangeWork_Load(object sender, EventArgs e)
        {
            this.Text = "护士交班记录";

            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountFrm(this);

            InitLupEditor();
            InzationData();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            var dr = gridView1.GetFocusedDataRow() as HemoModel.MED_HEMO_CHAGEWORKRow;
            if (dr == null)
            {
                AutoClosedMsgBox.ShowForm("请选择要修改的明细数据", "提示", 1500, MessageBoxIcon.Warning);
                return;
            }
            using (EditChangeWork frm = new EditChangeWork())
            {
                frm.ChangeWorkHaving = _workMaster;
                frm.CurrentData = dr;

                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    InzationData();
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            using (NurseChangeWorkDateReport frm = new NurseChangeWorkDateReport(_workMaster, _workExtend))
            {
                frm.ShowDialog();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)                                                                                                        
        {
            using (EditChangeWork frm = new EditChangeWork())
            {
                frm.currentArea = currentArea;
                frm.ChangeWorkHaving = _workMaster;
               
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    InzationData();
                }
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            InzationData();
        }


        private void gridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var rowCurrent = this.gridView2.GetFocusedDataRow() as HemoModel.MED_HEMO_CHAGEWORK_EXTENDRow;

            if (rowCurrent == null)
                return;

            //打开患者录入界面
            if (e.Button == MouseButtons.Left)
            {
                #region 双击去获取明细数据。
                if (e.Clicks == 2)
                {
                    dtMaster = rowCurrent.CHANGETIME;
                    var dtDetail = new HemoModel.MED_HEMO_CHAGEWORKDataTable();
                    _workMaster.Where(i => i.CHANGETIME == rowCurrent.CHANGETIME).CopyToDataTable(dtDetail, LoadOption.PreserveChanges);
                    if (dtDetail != null && dtDetail.Rows.Count > 0)
                        this.gridControl1.DataSource = dtDetail;
                    else
                        this.gridControl1.DataSource = null;
                    this.xtraTabControl1.SelectedTabPage = this.xtraTabeDetail;
                }
                #endregion
            }

        }

        #endregion

        #region 其他事件
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabMaster)
            {
                Utilities.AutoClosedMsgBox.ShowForm("请选择要删除的交班明细康！", "删除交班记录", 1500, MessageBoxIcon.Information);
                return; 
            }
            
            var rowCurrent = this.gridView1.GetFocusedDataRow() as HemoModel.MED_HEMO_CHAGEWORKRow;
            if (rowCurrent == null)
                return;

            if (DevExpress.XtraEditors.XtraMessageBox.Show("是否确认删除当前信息？","提示",MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                if (objPatient.DeleteChangeWorkById(rowCurrent.ID) > 0)
                {
                    Utilities.AutoClosedMsgBox.ShowForm("删除成功！", "删除交班记录", 1500, MessageBoxIcon.Information);
                    DateTime currentDt = Convert.ToDateTime(string.Format("{0}/{1}/01", this.date_QueryDate.DateTime.Year.ToString(), this.date_QueryDate.DateTime.Month.ToString().Trim()));
                    var begionTime = currentDt.AddDays(1 - currentDt.Day);
                    var endTime = currentDt.AddDays(1 - currentDt.Day).AddMonths(1).AddDays(-1);
                    _workMaster = objPatient.GetChangeNurseWorkByDate(begionTime, endTime);
                    _workExtend = objPatient.GetChageWorkExtendByMonth(this.date_QueryDate.DateTime.Month.ToString().Trim());
                    var dtMaster = new HemoModel.MED_HEMO_CHAGEWORKDataTable();
                    _workMaster.Where(i => i.CHANGETIME == rowCurrent.CHANGETIME).CopyToDataTable<HemoModel.MED_HEMO_CHAGEWORKRow>(dtMaster, LoadOption.PreserveChanges);
                    this.gridControl1.DataSource = dtMaster;
                    this.gridControl2.DataSource = _workExtend;
                  
                }
                else
                {
                    Utilities.AutoClosedMsgBox.ShowForm("删除失败！", "删除交班记录", 1500, MessageBoxIcon.Information);

                }
            }

        }
        #endregion

    }
}
