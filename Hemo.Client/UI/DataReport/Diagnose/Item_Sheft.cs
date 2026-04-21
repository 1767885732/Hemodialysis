/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：转归情况用户控件
// 创建时间：2015-04-17
// 创建者：贺建操
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
using Hemo.Model;
using Hemo.IService.DataReport;
using Hemo.Service;
using HEMODataReporter;
using HEMODataReporter.PostEntity;
using Hemo.Client.Core;
using Hemo.Client.UI.Machine;
using Hemo.Utilities;

namespace Hemo.Client.UI.DataReport.Diagnose
{
    public partial class Item_Sheft : ViewBase
    {
        #region 类变量

        private DataReportModel.MED_PATIENTSDataTable _patientDiagDataTable;
        private IDataReport objDataReport = ServiceManager.Instance.DataReportService;

        #endregion

        #region 属性

        public string _currentPatientHemoId { get; set; }

        #endregion

        #region 构造函数

        public Item_Sheft(string CurrentPatientHemoId)
        {
            InitializeComponent();
            _currentPatientHemoId = CurrentPatientHemoId;
            InzationData();
        }

        #endregion

        #region 事件

        private void gridView4_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var dr = gridView4.GetFocusedDataRow() as DataReportModel.MED_PATIENTSRow;
            if (dr == null)
                return;
            if (e.Clicks == 1)
            {
                if (dr.ISUPLOAD == "1")
                {
                    dr.ISUPLOAD = "0";
                }
                else if (dr.ISUPLOAD == "0")
                {
                    dr.ISUPLOAD = "1";
                }
                else
                {

                }
            }
            else if (e.Clicks == 2)
            {

            }
        }

        private void gridView4_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == gridColumn15)
            {
                var curRow = (DataReportModel.MED_PATIENTSRow)gridView4.GetDataRow(e.RowHandle);
                if (curRow == null)
                    return;
                if (curRow.ISUPLOAD == "2")
                {
                    var cloneRepository = e.RepositoryItem.Clone() as DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit;
                    cloneRepository.Click += new EventHandler(cloneRepository_Click);
                    cloneRepository.ReadOnly = true;
                    e.RepositoryItem = cloneRepository;
                }
            }
        }

        void cloneRepository_Click(object sender, EventArgs e)
        {
            MessageBox.Show("已上传不能再上传");
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载通路信息
        /// </summary>
        public override void InzationData()
        {
            using (var _worker = new BackgroundWorker())
            {
                _patientDiagDataTable = new DataReportModel.MED_PATIENTSDataTable();
                _worker.DoWork += delegate(object sender, DoWorkEventArgs e)
                {
                    _patientDiagDataTable = objDataReport.GetDataReportPatientDiagnoseList(_currentPatientHemoId, "1");

                };
                _worker.RunWorkerCompleted += delegate(object sender1, RunWorkerCompletedEventArgs r1)
                {
                    this.gridControl1.DataSource = _patientDiagDataTable;
                };
                _worker.RunWorkerAsync();
            }
        }

        public override void GetVascualToUpLoad(string resultInfo)
        {
            var dtSource = ((System.Data.DataView)(this.gridView4.DataSource)).Table as DataReportModel.MED_PATIENTSDataTable;
            var dt = new DataReportModel.MED_PATIENT_DATAREPORTDataTable();
            DataReporter reporter = HemoApplicationContext.Current.dataCurrentReport;// new DataReporter("hn002", "0");
            //做一个判断 
            if (reporter == null || !reporter.loginResult.Success)
            {
                reporter = HemoApplicationContext.Current.IsLoginHospitalHemoPlatForm;

                if (reporter != null && reporter.loginResult.Success)
                {
                    AutoClosedMsgBox.Show("登录成功！", "提示", 5000, 0);
                }
                else
                {
                    XtraMessageBox.Show("登录失败，请重新登录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            foreach (DataReportModel.MED_PATIENTSRow row in dtSource.Rows)
            {
                if (row.ISUPLOAD == "1")
                {

                    //去进行上传操作                   
                    ReportResult result = reporter.ReportZDZG(new ZDZGEntity(resultInfo, row.CREATE_DATE, row.IS_NEW));
                    if (result.Success)
                    {
                        var rowExtend = dt.NewMED_PATIENT_DATAREPORTRow();
                        rowExtend.ID = Guid.NewGuid().ToString();
                        rowExtend.HEMODIALYSIS_ID = row.HEMODIALYSIS_ID;
                        rowExtend.BASEINFO = result.Info;
                        rowExtend.STATE = "1";//成功
                        rowExtend.TYPE = "1";
                        rowExtend.UPTIME = System.DateTime.Now;
                        rowExtend.UPUSER = HemoApplicationContext.Current.CurrentUser.USER_ID;
                        rowExtend.MAPIP = row.HEMODIALYSIS_ID;
                        rowExtend.EXTEND = "ZDXX";
                        rowExtend.EXTEND1 = "诊断信息";
                        rowExtend.EXTEND5 = "全国数据上报平台";
                        dt.AddMED_PATIENT_DATAREPORTRow(rowExtend);
                        //break;
                    }
                    else
                    {
                        var rowExtend = dt.NewMED_PATIENT_DATAREPORTRow();
                        rowExtend.ID = Guid.NewGuid().ToString();
                        rowExtend.HEMODIALYSIS_ID = row.HEMODIALYSIS_ID;
                        rowExtend.BASEINFO = result.Info;
                        rowExtend.STATE = "0";//失败
                        rowExtend.TYPE = "1";
                        rowExtend.UPTIME = System.DateTime.Now;
                        rowExtend.UPUSER = HemoApplicationContext.Current.CurrentUser.USER_ID;
                        rowExtend.MAPIP = row.HEMODIALYSIS_ID;
                        rowExtend.EXTEND = "ZDXX";
                        rowExtend.EXTEND1 = "血透信息";
                        rowExtend.EXTEND5 = "全国数据上报平台";
                        dt.AddMED_PATIENT_DATAREPORTRow(rowExtend);
                    }
                }
            }
            var reINT = objDataReport.SavePatientIsUploadDt(dt);
            if (reINT > 0)
            {
                MessageBox.Show("成功");
            }
            else
            {
                MessageBox.Show("失败");
            }
        }

        #endregion
    }
}
