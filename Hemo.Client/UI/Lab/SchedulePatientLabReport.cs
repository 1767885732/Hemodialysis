/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：检验数据导出窗体类
// 创建时间：2014-03-24
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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Model;
using Hemo.IService.PatientSchedule;
using Hemo.Service;
using Hemo.Client.Core;
using Hemo.Utilities;
using DevExpress.XtraEditors;

namespace Hemo.Client.UI.Lab
{
    public partial class SchedulePatientLabReport : HemoBaseFrm
    {
        #region 类变量

        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;
        private string banchi = string.Empty;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public SchedulePatientLabReport(DateTime beginTime, DateTime endTime, string banchi)
        {
            InitializeComponent();

            this.beginTime.EditValue = beginTime;
            this.endTime.EditValue = endTime;
            this.banchi = banchi;
        }

        #endregion

        #region 事件

        private void SchedulePatientLabReport_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void btn_Query_Click(object sender, EventArgs e)
        {
            InitData();
        }

        private void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("确定要将本页面数据导入到Excel内?") != DialogResult.OK)
                return;
            if (this.gcLabMain.DataSource == null)
            {
                XtraMessageBox.Show("没有数据要导出！");
                return;
            }
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "导出Excel";
            fileDialog.Filter = "Excel文件(*.xls)|*.xls";
            DialogResult dialogResult = fileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                options.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
                options.ShowGridLines = true;

                ExportTo(new DevExpress.XtraExport.ExportXlsProvider(fileDialog.FileName));
                //this.gcLabMain.ExportToXls(fileDialog.FileName, options);
                AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);

                // DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ExportTo(DevExpress.XtraExport.IExportProvider provider)
        {
            DevExpress.XtraGrid.Export.BaseExportLink link = this.gvLabMain.CreateExportLink(provider);
            (link as DevExpress.XtraGrid.Export.GridViewExportLink).ExpandAll = false;
            link.ExportTo(true);
            provider.Dispose();
        }

        #endregion

        #region 方法

        private void InitData()
        {
            DataTable dt = new DataTable();
            string strPatientType = string.Empty;
            this.busyIndicator1.ShowLoadingScreenFor(this.gcLabMain);
            if (cmbTimeType.EditValue != null)
            {
                strPatientType = cmbTimeType.EditValue.ToString();
            }
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    dt = this._patientScheduleService.GetSchedulePatientLabResult(LoginUser.User.USER_ID, Utility.CDate(this.beginTime.EditValue.ToString()), Utility.CDate(this.endTime.EditValue.ToString()), this.banchi, strPatientType);
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        var dtSource = new DataTable();
                        foreach (DataColumn dc in dt.Columns)
                        {
                            dtSource.Columns.Add(dc.ColumnName.ToString());
                        }
                        int inJet = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            for (int j = i + 1; j < dt.Rows.Count; j++)
                            {
                                if (dt.Rows[i]["DATER"].ToString() == dt.Rows[j]["DATER"].ToString() && dt.Rows[i]["PATIENT_ID"].ToString() == dt.Rows[j]["PATIENT_ID"].ToString() && dt.Rows[i]["NAME"].ToString() == dt.Rows[j]["NAME"].ToString())
                                {
                                    for (int k = 4; k < dt.Columns.Count; k++)
                                    {
                                        if (string.IsNullOrEmpty(dt.Rows[i][k].ToString()))
                                            dt.Rows[i][k] = dt.Rows[j][k];
                                    }
                                    inJet = j;
                                }
                            }
                            dtSource.ImportRow(dt.Rows[i]);
                            if (i < inJet)
                                i = inJet;
                        }
                        bool isDelete = true;
                        for (int i = 0; i < dtSource.Rows.Count; i++)
                        {
                            for (int k = 4; k < dtSource.Columns.Count; k++)
                            {
                                if (!string.IsNullOrEmpty(dtSource.Rows[i][k].ToString()))
                                {
                                    isDelete = false;
                                    break;
                                }
                            }
                            if (isDelete)
                                dtSource.Rows[i].Delete();
                            isDelete = true;
                        }

                        gcLabMain.DataSource = dtSource;
                    }
                    else
                    {
                        gcLabMain.DataSource = null;
                    }
                    busyIndicator1.HideLoadingScreen();

                };
                worker.RunWorkerAsync();
            }

        }

        #endregion
    }
}
