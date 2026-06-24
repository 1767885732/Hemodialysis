/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：患者检验信息查询用户控件类
// 创建时间：2016-8-22
// 创建者：贺建操
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
using Hemo.IService.Lab;
using Hemo.Client.Base;
using DevExpress.XtraSplashScreen;
using Hemo.Client.Controls;
using DevExpress.XtraPrinting;
using Hemo.IService.Config;

namespace Hemo.Client.UI.Lab
{
    public partial class AllPatientLabRpt : BaseMoudleControl
    {
        #region 类变量

        private ILab _ilab = ServiceManager.Instance.LabService;
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        private string banchi = string.Empty;

        #endregion

        #region 属性

        public TaskViewModel ViewModel
        {
            get { return GetViewModel<TaskViewModel>(); }
        }

        private SplashScreenManager _loadForm;
        /// <summary>
        /// 等待窗体管理对象
        /// </summary>
        protected SplashScreenManager LoadForm
        {
            get
            {
                if (_loadForm == null)
                {
                    this._loadForm = new SplashScreenManager(this.ParentForm.FindForm(), typeof(FrmWaitForm), true, true);
                    //this._loadForm.CloseWaitForm();.ClosingDelay = 0;
                }
                return _loadForm;
            }
        }

        #endregion

        #region 构造函数

        public AllPatientLabRpt()
        {
            InitializeComponent();
            base.viewModelCore = CreateViewModel<TaskViewModel>();
        }

        #endregion

        #region 事件

        private void AllPatientLabRpt_Load(object sender, EventArgs e)
        {
            this.beginTime.EditValue = Utility.GetMonday(DateTime.Now).AddDays(0).Date;
            this.endTime.EditValue = Utility.GetMonday(DateTime.Now).AddDays(0).Date.AddDays(6).Date;
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


            var itemName = string.Empty;
            string fileName = "患者检验导出" + itemName + DateTime.Now.ToString("yyyyMMdd") + "." + "xls";
            SaveFileDialog dialog = new SaveFileDialog() { Title = "导出Excel", FileName = fileName, Filter = "Excel文件(*.xls)|*.*", RestoreDirectory = true };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                XlsExportOptions option = new XlsExportOptions() { TextExportMode = TextExportMode.Text };
                this.gcLabMain.ExportToXls(dialog.FileName);
                AutoClosedMsgBox.ShowForm("保存成功。", "提示", 1500, MessageBoxIcon.Information);
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

        protected internal override void OnTransitionCompleted()
        {
            base.OnTransitionCompleted();
        }

        private void InitDataNew()
        {
            try
            {
                ShowMessage();

                this.gridView1.Columns.Clear();
                DataTable dt = new DataTable();
                string strPatientType = string.Empty;
                if (cmbTimeType.EditValue != null)
                {
                    strPatientType = cmbTimeType.EditValue.ToString();
                }

                using (BackgroundWorker worker = new BackgroundWorker())
                {
                    worker.DoWork += (o1, e1) =>
                    {
                        var labdt = this._ilab.get_med_vw_xuehongdanbai_ext(Utility.CDate(this.beginTime.EditValue.ToString()), Utility.CDate(this.endTime.EditValue.ToString()));
                        if (this.chkConstant.Checked && labdt.Rows.Count > 0)
                        {
                            var dtHaving = this._hemodialysisService.GetHemoIdInLastWeekAndThreeMonthsByDate(Utility.CDate(this.beginTime.EditValue.ToString()), Utility.CDate(this.endTime.EditValue.ToString()));
                            dt = labdt.Copy();
                            dt.Rows.Clear();
                            foreach (DataRow dr in dtHaving.Rows)
                            {
                                var labRow = labdt.AsEnumerable().Where(i => i["透析号"].ToString().Trim().Equals(dr["HEMODIALYSIS_ID"].ToString().Trim()));
                                if (labRow == null) continue;
                                foreach (DataRow row in labRow)
                                {
                                    dt.LoadDataRow(row.ItemArray, true);
                                }
                            }
                        }
                        else
                        {
                            dt = labdt.Copy();
                        }


                    };
                    worker.RunWorkerCompleted += (o2, e2) =>
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (strPatientType.Length > 0)
                            {
                                dt = dt.AsEnumerable().Where(i => i["病人来源"].ToString().Equals(strPatientType)).CopyToDataTable();
                            }
                        }
                        dt = Utility.GetSubTable(dt, "1=1", "姓名,检验日期");
                        gcLabMain.DataSource = dt;
                        this.gridView1.Columns[5].DisplayFormat.FormatString = "YYYY-MM-DD hh:mm:ss";
                        this.gridView1.BestFitColumns();

                        HideMessage();
                        this.gridView1.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                        this.gridView1.Columns[1].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                        this.gridView1.Columns[2].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                        this.gridView1.Columns[3].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                        this.gridView1.Columns[4].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                        this.gridView1.Columns[5].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    };
                    worker.RunWorkerAsync();
                }
            }

            catch (Exception e)
            {
                HideMessage();
                XtraMessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// 显示等待窗体
        /// </summary>
        public void ShowMessage()
        {
            bool flag = !this.LoadForm.IsSplashFormVisible;
            if (flag)
            {
                this.LoadForm.ShowWaitForm();
            }
        }
        /// <summary>
        /// 关闭等待窗体
        /// </summary>
        public void HideMessage()
        {
            bool isSplashFormVisible = this.LoadForm.IsSplashFormVisible;
            if (isSplashFormVisible)
            {
                this.LoadForm.CloseWaitForm();
            }
        }
        /// <summary>
        /// 维持性透析患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkConstant_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion


    }
}
