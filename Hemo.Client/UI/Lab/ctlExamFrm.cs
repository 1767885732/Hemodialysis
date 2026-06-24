/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：患者检查信息查询同步用户控件类
// 创建时间：2016-7-20
// 创建者：吕志强
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
using Hemo.IService.Lab;
using Hemo.Service;
using System.Linq;
using Hemo.Client.Print;
using DevExpress.XtraReports.UI;
using Hemo.Client.UI.Machine;
using Hemo.Client.Core;
using Hemo.Utilities;
using Hemo.Client.UI.PatientFixUI;
using Hemo.Client.Controls;

namespace Hemo.Client.UI.Lab
{
    public partial class ctlExamFrm : ViewBase
    {
        #region 类变量

        private PatientModel.MED_PATIENTSRow patientRow;

        private DataTable dtMain = null;

        private ViewBase currentParent = null;

        private ILab labService = ServiceManager.Instance.LabService;

        #endregion

        #region 属性

        public PatientModel.MED_PATIENTSRow PatientRow
        {
            get { return patientRow; }
            set { patientRow = value; }
        }

        public ViewBase CurrentParent
        {
            get { return currentParent; }
            set { currentParent = value; }
        }

        #endregion

        #region 构造函数

        public ctlExamFrm()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExamFrm_Load(object sender, EventArgs e)
        {
            if (currentParent != null)
            {
                if (currentParent.GetType().Name.Equals("PatientFixInfosUI"))
                {
                    this.cmbSTART_DATE.EditValue = (currentParent as PatientFixInfosUI).ExamFromTime.Equals(string.Empty) ? null : (currentParent as PatientFixInfosUI).ExamFromTime;
                    this.cmbEND_DATE.EditValue = (currentParent as PatientFixInfosUI).ExamToTime.Equals(string.Empty) ? null : (currentParent as PatientFixInfosUI).ExamToTime;
                }
                else if (currentParent.GetType().Name.Equals("CtlUserCureList"))
                {
                    this.cmbSTART_DATE.EditValue = (currentParent as CtlUserCureList).ExamFromTime.Equals(string.Empty) ? null : (currentParent as CtlUserCureList).ExamFromTime;
                    this.cmbEND_DATE.EditValue = (currentParent as CtlUserCureList).ExamToTime.Equals(string.Empty) ? null : (currentParent as CtlUserCureList).ExamToTime;
                }
            }
            LoadExamInfo();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (currentParent != null)
            {
                if (currentParent.GetType().Name.Equals("PatientFixInfosUI"))
                {
                    (currentParent as PatientFixInfosUI).ExamFromTime = this.cmbSTART_DATE.Text;
                    (currentParent as PatientFixInfosUI).ExamToTime = this.cmbEND_DATE.Text;
                }
                else if (currentParent.GetType().Name.Equals("CtlUserCureList"))
                {
                    (currentParent as CtlUserCureList).ExamFromTime = this.cmbSTART_DATE.Text;
                    (currentParent as CtlUserCureList).ExamToTime = this.cmbEND_DATE.Text;
                }
            }

            this.busyIndicator.ShowLoadingScreenFor(this.gcLabMain);
            LoadMainData();
            this.busyIndicator.HideLoadingScreen();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 同步检查记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSyncExamInfo_Click(object sender, EventArgs e)
        {
            SyncLabInfo();
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (this.gvLabMain.RowCount == 0)
            {
                MessageBox.Show("没有可打印的数据！");
                return;
            }

            var dtMaster = new ReportRelationModel.EXAMMASTERDataTable();
            var dtDetail = new ReportRelationModel.EXAMDETAILDataTable();

            dtMain.AsEnumerable().ToList().ForEach(row =>
            {
                var rowMaster = dtMaster.NewEXAMMASTERRow();
                rowMaster.EXAM_NO = row["EXAM_NO"].ToString();
                rowMaster.EXAM_CLASS = row["EXAM_CLASS"].ToString();
                rowMaster.EXAM_SUB_CLASS = row["EXAM_SUB_CLASS"].ToString();
                dtMaster.AddEXAMMASTERRow(rowMaster);

                var dtItem = labService.GetPatientExamDetailListByNo(row["EXAM_NO"].ToString());
                dtItem.AsEnumerable().ToList().ForEach(r =>
                {
                    var rowDetail = dtDetail.NewEXAMDETAILRow();
                    rowDetail.EXAM_NO = r["EXAM_NO"].ToString();
                    rowDetail.EXAM_ITEM = r["EXAM_ITEM"].ToString();
                    rowDetail.IMPRESSION = r["IMPRESSION"].ToString();
                    rowDetail.IS_ABNORMAL = r["IS_ABNORMAL"].ToString();
                    dtDetail.AddEXAMDETAILRow(rowDetail);
                });
            });

            PatientExamReport report = new PatientExamReport(patientRow, dtMaster, dtDetail);
            ReportPrintTool pt = new ReportPrintTool(report);
            pt.ShowPreviewDialog();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
  
        }

        /// <summary>
        /// 点击检查主表行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvLabMain_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            LoadDetailData();
        }

        #endregion

        #region 方法

        public void LoadExamInfo()
        {
            this.busyIndicator.ShowLoadingScreenFor(this.gcLabMain);
            //LoadDefaultDate();
            LoadPatient();
            LoadMainData();
            this.busyIndicator.HideLoadingScreen();
        }

        /// <summary>
        /// 加载默认日期
        /// </summary>
        private void LoadDefaultDate()
        {
            DateTime dtStart = new DateTime(System.DateTime.Now.Year, 1, 1);
            DateTime dtEnd = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day);
            this.cmbSTART_DATE.EditValue = dtStart;
            this.cmbEND_DATE.EditValue = dtEnd;
        }

        /// <summary>
        /// 加载患者
        /// </summary>
        private void LoadPatient()
        {
            this.ctlUserInfo.HEMODIALYSIS_ID = patientRow.HEMODIALYSIS_ID;
            this.ctlUserInfo.LoadPatientInfo();
            this.lblPicture.BackgroundImage = patientRow.SEX == "男" ? Properties.Resources.boy : Properties.Resources.gril;
        }

        /// <summary>
        /// 加载主记录
        /// </summary>
        private void LoadMainData()
        {
            if (this.cmbSTART_DATE.EditValue != null && this.cmbEND_DATE.EditValue != null)
            {
                dtMain = labService.GetPatientExamListByDate(patientRow.PATIENT_ID, this.cmbSTART_DATE.DateTime, this.cmbEND_DATE.DateTime);
                if (dtMain == null || dtMain.Rows.Count == 0)
                {
                    if (patientRow.ADMISSION_NUMBER.Length > 0)
                    {
                        dtMain = labService.GetPatientExamListByDate(patientRow.ADMISSION_NUMBER, this.cmbSTART_DATE.DateTime, this.cmbEND_DATE.DateTime);
                    }
                }
            }
            else
            {
                dtMain = labService.GetPatientExamList(patientRow.PATIENT_ID);
                if (dtMain == null || dtMain.Rows.Count == 0)
                {
                    if (patientRow.ADMISSION_NUMBER.Length > 0)
                    {
                        dtMain = labService.GetPatientExamList(patientRow.ADMISSION_NUMBER);
                    }
                }
            }

            this.gcLabMain.DataSource = dtMain;
            LoadDetailData();
        }

        /// <summary>
        /// 加载明细记录
        /// </summary>
        private void LoadDetailData()
        {
            if (this.gvLabMain.RowCount > 0)
            {
                string examNo = this.gvLabMain.GetFocusedRowCellValue("EXAM_NO").ToString();
                DataTable dtDetail = labService.GetPatientExamDetailListByNo(examNo);
                dtDetail.AsEnumerable().ToList().ForEach(row =>
                {
                    row["EXAM_ITEM"] = row["EXAM_ITEM"].ToString().Trim().Replace("\r", "").Replace("\n", "");
                    row["IMPRESSION"] = row["IMPRESSION"].ToString().Trim().Replace("\r", "").Replace("\n", "");
                });
                this.gcLabDetail.DataSource = dtDetail;
            }
            else
            {
                this.gcLabDetail.DataSource = null;
            }
        }

        /// <summary>
        /// 同步检查记录
        /// </summary>
        private void SyncLabInfo()
        {
            bool result = false;
            this.busyIndicator.ShowLoadingScreenFor(gcLabMain);

            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += (o1, e1) =>
                {
                    try
                    {
                        string interFaceUrl = HemoApplicationContext.Current.InterFaceDate.FirstOrDefault(i => i.ITEM_NAME == "血透同步数据接口").ITEM_VALUE.ToString();
                        string error = InterfaceUtility.SynchronizeExamDatasByPatientId(patientRow.PATIENT_ID, interFaceUrl);

                        if (string.IsNullOrEmpty(error))
                            result = true;
                        else
                            AutoClosedMsgBox.ShowForm(string.Format("新接口（3.0）报错：{0}", error), "提示", 1000, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        AutoClosedMsgBox.ShowForm(ex.Message, "提示", 1000, MessageBoxIcon.Warning);
                    }
                };
                worker.RunWorkerCompleted += (o2, e2) =>
                {
                    try
                    {
                        if (result)
                            LoadMainData();
                        this.busyIndicator.HideLoadingScreen();
                    }
                    catch (Exception ex)
                    {
                        AutoClosedMsgBox.ShowForm(ex.Message, "提示", 1000, MessageBoxIcon.Warning);
                    }
                };
                worker.RunWorkerAsync();
            }
        }

        #endregion
    }
}