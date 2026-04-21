/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:排班患者信息查询类
 * 创建标识:贺建操-2016年4月27日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.IService.PatientSchedule;
using Hemo.Service;
using Hemo.Client.Print;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Hemo.Model;
using Hemo.IService.Config;

namespace Hemo.Client.UI.PatientSchedule
{
    public partial class SchedulePatientInfos : HemoBaseFrm
    {
        #region 构造函数

        public SchedulePatientInfos()
        {
            InitializeComponent();
        }

        #endregion
        
        #region 变量
        private IPatientSchedule schedule = ServiceManager.Instance.PatientSchedule;
        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;

        private Hemo.Model.ReportRelationModel.SCHEDULEPATIENTINFODataTable data = new Model.ReportRelationModel.SCHEDULEPATIENTINFODataTable();
        #endregion

        #region 方法

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InzationData()
        {
            busyIndicator1.Visible = true;
            busyIndicator1.ShowLoadingScreenFor(gridControl);

            using (BackgroundWorker worker = new BackgroundWorker())
            {
                data = new Model.ReportRelationModel.SCHEDULEPATIENTINFODataTable();
                var checkAMDt = new DataTable();
                var checkPMDt = new DataTable();
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    data = schedule.GetQuerySchedulePatientInfo(this.deBeginTime.DateTime);
                    checkAMDt = schedule.GetSchedulePatientCheck(this.deBeginTime.DateTime, "1");
                    checkPMDt = schedule.GetSchedulePatientCheck(this.deBeginTime.DateTime, "2");
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    foreach (ReportRelationModel.SCHEDULEPATIENTINFORow row in data.Rows)
                    {
                        var drug = objHemodialysisService.GetValidCureDrugByHemoID(row.HEMODIALYSIS_ID,row.DIALYSIS_DATE);
                        string drugName = string.Empty;
                        foreach (HemodialysisModel.MED_CURE_DRUGRow drugRow in drug.Rows)
                        {
                            drugName += string.Format("{0}{1}{2}", drugRow.DRUG_NAME, drugRow.DOSAGE, drugRow.UNIT_NAME);
                        }
                        row.DRUGNAME = drugName;
                        foreach (DataRow crow in checkAMDt.Rows)
                        {
                            if (row.HEMODIALYSIS_ID == crow["HEMODIALYSIS_ID"].ToString() && row.BANCHIID == "1")
                            {
                                row.CHECKNUM = crow["RANKER"].ToString();
                            }
                        }
                        foreach (DataRow crow in checkPMDt.Rows)
                        {
                            if (row.HEMODIALYSIS_ID == crow["HEMODIALYSIS_ID"].ToString() && row.BANCHIID == "2")
                            {
                                row.CHECKNUM = crow["RANKER"].ToString();
                            }
                        }
                    }
                    this.gridControl.DataSource = data;
                    this.busyIndicator1.HideLoadingScreen();
                };
                worker.RunWorkerAsync();
            }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            InzationData();
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            SchedulePatientInfostForJL frm = new SchedulePatientInfostForJL(this.deBeginTime.DateTime, data);
            frm.ShowDialog();
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
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SchedulePatientInfos_Load(object sender, EventArgs e)
        {
            this.deBeginTime.DateTime=System.DateTime.Now.Date;
            InzationData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            var row = gridView1.GetDataRow(e.RowHandle) as Hemo.Model.ReportRelationModel.SCHEDULEPATIENTINFORow;
            GridGroupRowInfo GridGroupRowInfo = e.Info as GridGroupRowInfo;
            if (GridGroupRowInfo.Column.GroupIndex == 0)
            {
                GridGroupRowInfo.GroupText = "班次:" + row.BANCHINAME;// +"-" + GridGroupRowInfo.EditValue.ToString();
            }
            else if (GridGroupRowInfo.Column.GroupIndex == 1)
            {
                GridGroupRowInfo.GroupText = row.AREANAME;// +"-" + GridGroupRowInfo.EditValue.ToString();
            }
        }

        #endregion
    }
}
