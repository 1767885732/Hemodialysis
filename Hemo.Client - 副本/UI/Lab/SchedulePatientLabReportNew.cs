/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：检验数据导出窗体类
// 创建时间：2014-06-17
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
    public partial class SchedulePatientLabReportNew : HemoBaseFrm
    {
        #region 类变量

        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;
        private string banchi = string.Empty;
        //存放检验项目名与报表列名的对应项
        private Dictionary<string, string> itemNames = new Dictionary<string, string>();

        #endregion

        #region 构造函数

        public SchedulePatientLabReportNew(DateTime beginTime, DateTime endTime, string banchi)
        {
            InitializeComponent();

            this.beginTime.EditValue = beginTime;
            this.endTime.EditValue = endTime;
            this.banchi = banchi;
            initItemNames();
        }

        #endregion

        #region 事件

        private void SchedulePatientLabReportNew_Load(object sender, EventArgs e)
        {
            InitDataNew();
        }

        private void btn_Query_Click(object sender, EventArgs e)
        {
            InitDataNew();
        }

        private void btn_ExportExcel_Click(object sender, EventArgs e)
        {
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
            DevExpress.XtraGrid.Export.BaseExportLink link = this.gridView1.CreateExportLink(provider);
            (link as DevExpress.XtraGrid.Export.GridViewExportLink).ExpandAll = false;
            link.ExportTo(true);
            provider.Dispose();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化itemNames字典
        /// </summary>
        private void initItemNames()
        {
            itemNames.Add("电解质检查", "item2");
            itemNames.Add("急诊潜血试验", "item3");
            itemNames.Add("急诊生化(干化学)", "item4");
            itemNames.Add("急诊生化（干化学）", "item4");
            itemNames.Add("梅毒血清试验(TRUST法)", "item5");
            itemNames.Add("梅毒血清试验（TRUST法）", "item5");
            itemNames.Add("脑利钠肽BNP", "item6");
            //
            itemNames.Add("尿常规+沉渣检测", "item7");
            itemNames.Add("凝血四项(PT+APTT+TT+FIB)", "item8");
            itemNames.Add("凝血四项（PT+APTT+TT+FIB）", "item8");
            itemNames.Add("常规生化全套检查", "item9");
            itemNames.Add("术前四项", "item10");
            itemNames.Add("心梗三项", "item11");
            //
            itemNames.Add("叶酸", "item12");
            itemNames.Add("乙肝表面抗体", "item13");
            itemNames.Add("乙肝两对半定量检测", "item14");
            itemNames.Add("总铁结合力", "item15");
        }

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

        private void InitDataNew()
        {
            try
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
                        dt = this._patientScheduleService.GetSchedulePatientLabResultMain(Utility.CDate(this.beginTime.EditValue.ToString()), Utility.CDate(this.endTime.EditValue.ToString()));
                    };
                    worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataTable use = new DataTable();
                            //this.gridView1.BestFitColumns();
                            for (int i = 0; i < this.gridView1.Columns.Count; i++)
                            {
                                if (i == 0)
                                {
                                    use.Columns.Add("name");
                                }
                                else if (i == 1)
                                {
                                    use.Columns.Add("date");
                                }
                                else
                                {
                                    use.Columns.Add("item" + i);
                                }
                            }
                            DataRow row = null;
                            for (int d1 = 0; d1 < dt.Rows.Count; d1++)
                            {
                                if (d1 == 0 || (!dt.Rows[d1]["PATIENT_NAME"].ToString().Equals(dt.Rows[d1 - 1]["PATIENT_NAME"]) || !dt.Rows[d1]["DATETIME"].ToString().Equals(dt.Rows[d1 - 1]["DATETIME"])))
                                {
                                    row = use.Rows.Add();
                                    row["name"] = dt.Rows[d1]["PATIENT_NAME"].ToString();
                                    row["date"] = dt.Rows[d1]["DATETIME"].ToString();
                                }
                                string item_name = dt.Rows[d1]["ITEM_NAME"].ToString();
                                string[] resultArr = dt.Rows[d1]["RESULT"].ToString().Split(',');
                                string result = string.Empty;
                                for (int num = 0; num < resultArr.Length; num++)
                                {
                                    if (num == resultArr.Length - 1)
                                    {
                                        result += resultArr[num];
                                    }
                                    else
                                    {
                                        if (num == 0 || num % 3 < 2)
                                        {
                                            result += resultArr[num] + "; ";
                                        }
                                        //else if (num % 3 == 2)
                                        //{
                                        //    result += resultArr[num] + ";\r\n";
                                        //}
                                        else
                                        {
                                            result += resultArr[num] + ";\r\n";
                                        }
                                    }
                                }
                                //for (int i = 0; i < this.gridView1.Columns.Count; i++)
                                //{
                                for (int j = 0; j < this.gridView1.Columns.Count; j++)
                                {
                                    if (gridView1.Columns[j].FieldName.Equals(itemNames[item_name]))
                                    {
                                        try
                                        { row[itemNames[item_name]] = result; }
                                        catch (Exception e)
                                        { XtraMessageBox.Show(e.Message + "\r\n" + e.StackTrace); }
                                    }
                                }
                                //}
                            }
                            gcLabMain.DataSource = use;
                        }
                        else
                        {
                            gcLabMain.DataSource = null;
                        }
                        gcLabMain.RefreshDataSource();
                        gcLabMain.Refresh();
                        busyIndicator1.HideLoadingScreen();

                    };
                    this.gridView1.BestFitColumns();
                    worker.RunWorkerAsync();
                }
            }
            catch (Exception e)
            {
                XtraMessageBox.Show(e.Message);
            }
        }

        #endregion
    }
}
