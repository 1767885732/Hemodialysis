/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者病程记录维护类
// 创建时间：2016-06-15
// 创建者：吕志强
//  
// 修改时间：2017-03-18
// 修改人：刘超
// 修改描述：修改界面及部分业务逻辑
//
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Config;
using Hemo.Model;
using Hemo.Service;
using Hemo.IService.Dict;
using Hemo.Utilities;
using Hemo.Client.Core;
using Hemo.Client.Print;
using Hemo.IService;
using System.Linq;
using Hemo.Client.UI.Hemodialysis;
using Hemo.Client.UI.Machine;
using DevExpress.XtraReports.UI;
using Hemo.HQCWebClient.Models;
using Hemo.HQCWebClient;
using Newtonsoft.Json;
using Hemo.IService.DataReport;
using DevExpress.XtraSplashScreen;
using Hemo.Client.Controls;

namespace Hemo.Client.UI.PatientFixUI
{
    public partial class PatientProgressNoteUI : ViewBase
    {
        #region 类变量

        private string currentHemoId = string.Empty;
        public string CurrentHemoId
        {
            get
            {
                return currentHemoId;
            }
            set
            {
                currentHemoId = value;
            }
        }

        private IHemodialysis hemodialysisService = ServiceManager.Instance.HemodialysisService;

        private IStaffDict staffDictService = ServiceManager.Instance.StaffDictService;

        private IPatient patientService = ServiceManager.Instance.PatientService;

        private IConfig configService = ServiceManager.Instance.ConfigService;

        private HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable progressNodeDataTable;

        private HemodialysisModel.MED_PATIENT_PROGRESS_NOTERow progressNodeRow;
        private IDataReport objDataReport = ServiceManager.Instance.DataReportService;

        private HemodialysisModel.MED_CURE_MAINDataTable dtCureMain;

        private PatientModel.MED_PATIENTSRow currentPatient;

        private string oldAccessType = "0";

        private bool isShow = true;

        #endregion

        #region 构造函数

        public PatientProgressNoteUI(string hemoId)
        {
            InitializeComponent();
            CurrentHemoId = hemoId;
            if (!string.IsNullOrEmpty(base.hemoId))
                currentHemoId = base.hemoId;
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientProgressNote_Load(object sender, EventArgs e)
        {
            LoadInfo();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            progressNodeRow = null;
            this.xtraTabControl1.SelectedTabPageIndex = 1;
            ClearControlText();
            oldAccessType = "0";
            LoadDefaultData();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (progressNodeRow == null)
            {
                if (!this.IsDataValidate())
                    return;

                //普陀人民医院每周统计病程，去掉限制
                //if (progressNodeDataTable != null && progressNodeDataTable.Rows.Count > 0)
                //{
                //    var row = progressNodeDataTable.FirstOrDefault(r => r.CREATE_DATE.ToString("yyyy-MM") == this.txtCREATE_DATE.DateTime.ToString("yyyy-MM"));
                //    if (row != null)
                //    {
                //        XtraMessageBox.Show("患者当月病程记录已存在！", "病程记录");
                //        return;
                //    }
                //}
            }

            progressNodeDataTable = progressNodeDataTable ?? new HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable();

            if (progressNodeRow == null)
            {
                progressNodeRow = progressNodeDataTable.NewMED_PATIENT_PROGRESS_NOTERow();
                progressNodeRow.ID = Guid.NewGuid().ToString();
                progressNodeRow.PATIENT_ID = currentPatient != null ? currentPatient.PATIENT_ID : null;
                progressNodeRow.HEMODIALYSIS_ID = currentHemoId;
                progressNodeRow.CREATE_DATE = Utility.CDate(this.txtCREATE_DATE.EditValue.ToString());
                progressNodeRow.IS_DELETE = "0";
                progressNodeDataTable.AddMED_PATIENT_PROGRESS_NOTERow(progressNodeRow);
            }

            progressNodeRow.DOCTOR_ID = this.cmbDOCTOR_ID.EditValue.ToString();
            progressNodeRow.PROGRESS_NODE = this.txtPROGRESS_NODE.Text.Trim();

            //不适主诉
            progressNodeRow.COMPLAINTS = (this.radCOMPLAINTS.EditValue != null) ? this.radCOMPLAINTS.EditValue.ToString() : null;
            progressNodeRow.COMPLAINTS_CONTENT = this.txtCOMPLAINTS_CONTENT.Text.Trim();

            //血管通路
            string strVASCULAR_ACCESS = string.Empty;
            //string strVASCULAR_ACCESS_SUGADJ = string.Empty;
            if (radVASCULAR_ACCESS.SelectedIndex == 0)
            {
                strVASCULAR_ACCESS = "0";
            }
            if (radVASCULAR_ACCESS.SelectedIndex == 1)
            {
                strVASCULAR_ACCESS = "1";
            }
            //if (radVASCULAR_ACCESS1.SelectedIndex == 0)
            //{
            //    strVASCULAR_ACCESS_SUGADJ = "0";
            //}
            //if (radVASCULAR_ACCESS1.SelectedIndex == 1)
            //{
            //    strVASCULAR_ACCESS_SUGADJ = "1";
            //}
            progressNodeRow.VASCULAR_ACCESS = strVASCULAR_ACCESS;
            //progressNodeRow.VASCULAR_ACCESS_SUGADJ = strVASCULAR_ACCESS_SUGADJ;
            progressNodeRow.VASCULAR_ACCESS_CHANGE = txtVASCULAR_ACCESS_CHANGE.Text.Trim();
            progressNodeRow.VASCULAR_ACCESS_NOTE = txtVASCULAR_ACCESS_NOTE.Text.Trim();

            //抗凝治疗
            string strTHERAPEUTIC_METHOD = string.Empty;
            //string strTHERAPEUTIC_METHOD_SUGADJ = string.Empty;
            if (radTHERAPEUTIC_METHOD.SelectedIndex == 0)
            {
                strTHERAPEUTIC_METHOD = "0";
            }
            if (radTHERAPEUTIC_METHOD.SelectedIndex == 1)
            {
                strTHERAPEUTIC_METHOD = "1";
            }
            //if (radTHERAPEUTIC_METHOD1.SelectedIndex == 0)
            //{
            //    strTHERAPEUTIC_METHOD_SUGADJ = "0";
            //}
            //if (radTHERAPEUTIC_METHOD1.SelectedIndex == 1)
            //{
            //    strTHERAPEUTIC_METHOD_SUGADJ = "1";
            //}
            progressNodeRow.THERAPEUTIC_METHOD = strTHERAPEUTIC_METHOD;
            //progressNodeRow.THERAPEUTIC_METHOD_SUGADJ = strTHERAPEUTIC_METHOD_SUGADJ;
            progressNodeRow.THERAPEUTIC_METHOD_CHANGE = txtTHERAPEUTIC_METHOD_CHANGE.Text.Trim();
            progressNodeRow.THERAPEUTIC_METHOD_NOTE = txtTHERAPEUTIC_METHOD_NOTE.Text.Trim();

            //容量控制
            progressNodeRow.CAPACITY_CONTROL = (this.radCAPACITY_CONTROL.EditValue != null) ? this.radCAPACITY_CONTROL.EditValue.ToString() : null;
            progressNodeRow.DRY_WEIHGT = this.txtDRY_WEIHGT.Text.Trim();
            progressNodeRow.MAX_DRY_WEIHGT = this.txtMAX_DRY_WEIHGT.Text.Trim();
            progressNodeRow.PERCENT_DRY_WEIGHT = this.lblPERCENT_DRY_WEIGHT.Text.Substring(1, this.lblPERCENT_DRY_WEIGHT.Text.Trim().IndexOf("%") - 1);

            //血压控制
            progressNodeRow.BLOOD_CONTROL = (this.radBLOOD_CONTROL.EditValue != null) ? this.radBLOOD_CONTROL.EditValue.ToString() : null;
            progressNodeRow.HIGH_BLOOD_PRESSURE = this.txtSYSTOLIC_PRESSURE.Text.Trim();
            progressNodeRow.LOW_BLOOD_PRESSURE = this.txtDIASTOLIC_PRESSURE.Text.Trim();

            //贫血纠正
            string strANEMIA_CORRECTION = string.Empty;
            //string strANEMIA_CORRECTION_SUGADJ = string.Empty;
            if (radANEMIA_CORRECTION.SelectedIndex == 0)
            {
                strANEMIA_CORRECTION = "0";
            }
            if (radANEMIA_CORRECTION.SelectedIndex == 1)
            {
                strANEMIA_CORRECTION = "1";
            }
            //if (radANEMIA_CORRECTION1.SelectedIndex == 0)
            //{
            //    strANEMIA_CORRECTION_SUGADJ = "0";
            //}
            //if (radANEMIA_CORRECTION1.SelectedIndex == 1)
            //{
            //    strANEMIA_CORRECTION_SUGADJ = "1";
            //}
            progressNodeRow.ANEMIA_CORRECTION = strANEMIA_CORRECTION;
            //progressNodeRow.ANEMIA_CORRECTION_SUGADJ = strANEMIA_CORRECTION_SUGADJ;
            progressNodeRow.ANEMIA_CORRECTION_BAD = txtANEMIA_CORRECTION_BAD.Text.Trim();
            progressNodeRow.ANEMIA_CORRECTION_NOTE = txtANEMIA_CORRECTION_NOTE.Text.Trim();

            //骨矿物质代谢絮乱控制
            string strBONE_MINERAL = string.Empty;
            //string strBONE_MINERAL_SUGADJ = string.Empty;
            if (radBONE_MINERAL.SelectedIndex == 0)
            {
                strBONE_MINERAL = "0";
            }
            if (radBONE_MINERAL.SelectedIndex == 1)
            {
                strBONE_MINERAL = "1";
            }
            //if (radBONE_MINERAL1.SelectedIndex == 0)
            //{
            //    strBONE_MINERAL_SUGADJ = "0";
            //}
            //if (radBONE_MINERAL1.SelectedIndex == 1)
            //{
            //    strBONE_MINERAL_SUGADJ = "1";
            //}
            progressNodeRow.BONE_MINERAL = strBONE_MINERAL;
            //progressNodeRow.BONE_MINERAL_SUGADJ = strBONE_MINERAL_SUGADJ;
            progressNodeRow.BONE_MINERAL_BAD = txtBONE_MINERAL_BAD.Text.Trim();
            progressNodeRow.BONE_MINERAL_NOTE = txtBONE_MINERAL_NOTE.Text.Trim();

            //营养情况
            string strNUTRITIONAL_STATUS = string.Empty;
            //string strNUTRITIONAL_STATUS_SUGADJ = string.Empty;
            if (radNUTRITIONAL_STATUS.SelectedIndex == 0)
            {
                strNUTRITIONAL_STATUS = "0";
            }
            if (radNUTRITIONAL_STATUS.SelectedIndex == 1)
            {
                strNUTRITIONAL_STATUS = "1";
            }
            //if (radNUTRITIONAL_STATUS1.SelectedIndex == 0)
            //{
            //    strNUTRITIONAL_STATUS_SUGADJ = "0";
            //}
            //if (radNUTRITIONAL_STATUS1.SelectedIndex == 1)
            //{
            //    strNUTRITIONAL_STATUS_SUGADJ = "1";
            //}
            progressNodeRow.NUTRITIONAL_STATUS = strNUTRITIONAL_STATUS;
            //progressNodeRow.NUTRITIONAL_STATUS_SUGADJ = strNUTRITIONAL_STATUS_SUGADJ;
            progressNodeRow.NUTRITIONAL_STATUS_BAD = txtNUTRITIONAL_STATUS_BAD.Text.Trim();
            progressNodeRow.NUTRITIONAL_STATUS_NOTE = txtNUTRITIONAL_STATUS_NOTE.Text.Trim();

            hemodialysisService.SavePatientProgressNoteInfo(progressNodeDataTable);
            AutoClosedMsgBox.ShowForm("保存成功！", this.Text, 1000, MessageBoxIcon.Asterisk);
            Query();
            progressNodeRow = progressNodeDataTable.FirstOrDefault(r => r.ID == progressNodeRow.ID);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var row = this.gvProgressNote.GetFocusedDataRow();
            if (row != null)
            {
                if (XtraMessageBox.Show("确认要删除选中的记录吗？", "病程记录", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    int result = hemodialysisService.DeletePatientProgressNoteById(row["ID"].ToString());
                    if (result > 0)
                    {
                        XtraMessageBox.Show("删除记录成功！", "病程记录");
                        Query();
                        if (progressNodeRow != null && progressNodeRow.ID.Equals(row["ID"].ToString()))
                        {
                            ClearControlText();
                            progressNodeRow = null;
                            LoadDefaultData();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            int[] index = this.gvProgressNote.GetSelectedRows();
            if (index == null || index.Length == 0)
            {
                XtraMessageBox.Show("请选择要打印的记录！", "病程记录");
                return;
            }

            int j = 0;
            string[] arrId = new string[index.Length];
            index.ToList().ForEach(i =>
            {
                arrId[j] = this.gvProgressNote.GetDataRow(i)["ID"].ToString();
                j++;
            });

            PatientProgressNoteReport report = new PatientProgressNoteReport();
            report.HemoId = currentHemoId;
            report.ArrId = arrId;
            ReportPrintTool pt = new ReportPrintTool(report);
            pt.ShowPreviewDialog();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        /// <summary>
        /// 双击列表行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcProgressNote_DoubleClick(object sender, EventArgs e)
        {
            //var row = this.gvProgressNote.GetFocusedDataRow();
            //if (row != null)
            //{

            //}
        }
        private void gvProgressNote_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            var rowCurrent = this.gvProgressNote.GetFocusedDataRow() as HemodialysisModel.MED_PATIENT_PROGRESS_NOTERow;

            if (rowCurrent == null || e.CellValue == null) return;

            if (e.Column == gridColumn17)
            {
                if (e.CellValue.ToString().Equals("已上传"))
                {
                    e.Appearance.Font = new Font("Tahoma", 11, FontStyle.Bold);
                    e.Appearance.BackColor = Color.Green;
                }
            }
        }
        private void gvProgressNote_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == gridColumn16)
            {
                var curRow = (HemodialysisModel.MED_PATIENT_PROGRESS_NOTERow)gvProgressNote.GetDataRow(e.RowHandle);
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
            XtraMessageBox.Show("已上传不能再上传");
        }
        private void gvProgressNote_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var dr = gvProgressNote.GetFocusedDataRow() as HemodialysisModel.MED_PATIENT_PROGRESS_NOTERow;
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
                //双击已上传患者自动打开明细项目
                progressNodeRow = progressNodeDataTable.FirstOrDefault(r => r.ID == dr["ID"].ToString());
                this.xtraTabControl1.SelectedTabPageIndex = 1;
                isShow = false;
                ClearControlText();
                InitData(dr);
                isShow = true;
                if (this.radVASCULAR_ACCESS.EditValue != null)
                {
                    oldAccessType = this.radVASCULAR_ACCESS.EditValue.ToString();
                }
            }
        }
        /// <summary>
        /// 病程列表、病程记录切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPageIndex == 0)
            {
                SetControlEnabled(true);
            }
            else
            {
                SetControlEnabled(false);
                this.txtCREATE_DATE.Enabled = progressNodeRow == null ? true : false;
            }
        }

        /// <summary>
        /// 记录时间改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCREATE_DATE_EditValueChanged(object sender, EventArgs e)
        {
            if (dtCureMain != null)
            {
                var rows = dtCureMain.Where(r => r.CURE_CREATE_DATE.ToString("yyyy-MM") == this.txtCREATE_DATE.DateTime.ToString("yyyy-MM"));
                if (rows != null && rows.Count() > 0)
                {
                    rows = rows.OrderByDescending(r => Utility.CDouble(r["DRY_WATER_VALUE"].ToString()));
                    var row = rows.ElementAt(0);
                    this.txtDRY_WEIHGT.Text = row.DRY_WEIGHT.ToString();

                    if (row["DRY_WATER_VALUE"] != DBNull.Value)
                    {
                        this.txtMAX_DRY_WEIHGT.Text = row.DRY_WATER_VALUE.ToString();
                        if (row.DRY_WEIGHT != 0)
                        {
                            this.lblPERCENT_DRY_WEIGHT.Text = "(" + Math.Round((decimal)(row.DRY_WATER_VALUE * 100) / row.DRY_WEIGHT, 2).ToString() + "%干体重)";
                        }
                        else
                        {
                            this.lblPERCENT_DRY_WEIGHT.Text = "(     %干体重)";
                        }
                    }
                    else
                    {
                        this.txtMAX_DRY_WEIHGT.Text = string.Empty;
                        this.lblPERCENT_DRY_WEIGHT.Text = "(     %干体重)";
                    }

                    this.txtSYSTOLIC_PRESSURE.Text = string.Empty;
                    rows = rows.OrderByDescending(r => Utility.CDouble(r["BEFORE_SYSTOLIC_PRESSURE"].ToString()));
                    row = rows.ElementAt(0);
                    if (row.BEFORE_SYSTOLIC_PRESSURE != 0)
                    {
                        this.txtSYSTOLIC_PRESSURE.Text = row.BEFORE_SYSTOLIC_PRESSURE.ToString() + "/" + row.BEFORE_DIASTOLIC_PRESSURE.ToString();
                    }

                    this.txtDIASTOLIC_PRESSURE.Text = string.Empty;
                    rows = rows.Where(r => (r["BEFORE_DIASTOLIC_PRESSURE"] != DBNull.Value) && (Utility.CDouble(r["BEFORE_DIASTOLIC_PRESSURE"].ToString()) != 0));
                    if (rows != null && rows.Count() > 0)
                    {
                        rows = rows.OrderBy(r => r.BEFORE_DIASTOLIC_PRESSURE);
                        row = rows.ElementAt(0);
                        this.txtDIASTOLIC_PRESSURE.Text = row.BEFORE_SYSTOLIC_PRESSURE.ToString() + "/" + row.BEFORE_DIASTOLIC_PRESSURE.ToString();
                    }
                }
                else
                {
                    this.txtDRY_WEIHGT.Text = string.Empty;
                    this.txtMAX_DRY_WEIHGT.Text = string.Empty;
                    this.lblPERCENT_DRY_WEIGHT.Text = "(     %干体重)";
                    this.txtSYSTOLIC_PRESSURE.Text = string.Empty;
                    this.txtDIASTOLIC_PRESSURE.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// 不适主诉选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radCOMPLAINTS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radCOMPLAINTS.SelectedIndex == 0)
            {
                this.txtCOMPLAINTS_CONTENT.Text = string.Empty;
                this.txtCOMPLAINTS_CONTENT.Enabled = false;
            }
            else
            {
                this.txtCOMPLAINTS_CONTENT.Enabled = true;
            }
        }

        /// <summary>
        /// 血管通路选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radVASCULAR_ACCESS_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.radVASCULAR_ACCESS.EditValue.ToString() == "1") {
            //    if (isShow) {
            //        using (EditVascularAccess frmEditVascularAccess = new EditVascularAccess(currentHemoId)) {
            //            DialogResult result = frmEditVascularAccess.ShowDialog();
            //            //    if (result == DialogResult.OK)
            //            //    {
            //            //        if (frmEditVascularAccess.AccessType == null)
            //            //        {
            //            //            this.radVASCULAR_ACCESS.EditValue = oldAccessType;
            //            //        }
            //            //        this.txtVASCULAR_ACCESS.Text = (frmEditVascularAccess.AccessType != null) ? frmEditVascularAccess.AccessType : this.txtVASCULAR_ACCESS.Text;
            //            //    }
            //            //    else
            //            //    {
            //            //        this.radVASCULAR_ACCESS.EditValue = oldAccessType;
            //            //    }
            //        }
            //    }
            //}
            //else {
            //    this.txtVASCULAR_ACCESS_CHANGE.Text = string.Empty;
            //}

            if (this.radVASCULAR_ACCESS.SelectedIndex == 0 || this.radVASCULAR_ACCESS.SelectedIndex == 1)
            {
                if (this.radVASCULAR_ACCESS.SelectedIndex == 0)
                {
                    this.txtVASCULAR_ACCESS_CHANGE.Text = string.Empty;
                    this.txtVASCULAR_ACCESS_CHANGE.Enabled = false;
                }
                else
                {
                    this.txtVASCULAR_ACCESS_CHANGE.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 血管通路建议调整选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radVASCULAR_ACCESS1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.radVASCULAR_ACCESS1.SelectedIndex == 0 || this.radVASCULAR_ACCESS1.SelectedIndex == 1)
            //{
            //    this.txtVASCULAR_ACCESS_NOTE.Enabled = true;
            //}
            //else
            //{
            //    this.txtVASCULAR_ACCESS_NOTE.Enabled = false;
            //}
        }

        /// <summary>
        /// 抗凝治疗选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radTHERAPEUTIC_METHOD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radTHERAPEUTIC_METHOD.SelectedIndex == 0 || this.radTHERAPEUTIC_METHOD.SelectedIndex == 1)
            {
                if (this.radTHERAPEUTIC_METHOD.SelectedIndex == 0)
                {
                    this.txtTHERAPEUTIC_METHOD_CHANGE.Text = string.Empty;
                    this.txtTHERAPEUTIC_METHOD_CHANGE.Enabled = false;
                }
                else
                {
                    this.txtTHERAPEUTIC_METHOD_CHANGE.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 抗凝治疗建议调整选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radTHERAPEUTIC_METHOD1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.radTHERAPEUTIC_METHOD1.SelectedIndex == 0 || this.radTHERAPEUTIC_METHOD1.SelectedIndex == 1)
            //{
            //    this.txtTHERAPEUTIC_METHOD_NOTE.Enabled = true;
            //}
            //else
            //{
            //    this.txtTHERAPEUTIC_METHOD_NOTE.Enabled = false;
            //}
        }

        /// <summary>
        /// 贫血纠正选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radANEMIA_CORRECTION_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.radANEMIA_CORRECTION.SelectedIndex == 0 || this.radANEMIA_CORRECTION.SelectedIndex == 1)
            //{
            //    if (this.radANEMIA_CORRECTION.SelectedIndex == 0)
            //    {
            //        this.txtANEMIA_CORRECTION_BAD.Text = string.Empty;
            //        this.txtANEMIA_CORRECTION_BAD.Enabled = false;
            //    }
            //    else
            //    {
            //        this.txtANEMIA_CORRECTION_BAD.Enabled = true;
            //    }
            //}
        }

        /// <summary>
        /// 贫血纠正建议调整选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radANEMIA_CORRECTION1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.radANEMIA_CORRECTION1.SelectedIndex == 0 || this.radANEMIA_CORRECTION1.SelectedIndex == 1)
            //{
            //    this.txtANEMIA_CORRECTION_NOTE.Enabled = true;
            //}
            //else
            //{
            //    this.txtANEMIA_CORRECTION_NOTE.Enabled = false;
            //}
        }

        /// <summary>
        /// 骨矿物质代谢絮乱控制选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radBONE_MINERAL_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.radBONE_MINERAL.SelectedIndex == 0 || this.radBONE_MINERAL.SelectedIndex == 1)
            //{
            //    if (this.radBONE_MINERAL.SelectedIndex == 0)
            //    {
            //        this.txtBONE_MINERAL_BAD.Text = string.Empty;
            //        this.txtBONE_MINERAL_BAD.Enabled = false;
            //    }
            //    else
            //    {
            //        this.txtBONE_MINERAL_BAD.Enabled = true;
            //    }
            //}
        }

        /// <summary>
        /// 骨矿物质代谢絮乱控制建议调整选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radBONE_MINERAL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.radBONE_MINERAL1.SelectedIndex == 0 || this.radBONE_MINERAL1.SelectedIndex == 1)
            //{
            //    this.txtBONE_MINERAL_NOTE.Enabled = true;
            //}
            //else
            //{
            //    this.txtBONE_MINERAL_NOTE.Enabled = false;
            //}
        }

        /// <summary>
        /// 营养情况选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radNUTRITIONAL_STATUS_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.radNUTRITIONAL_STATUS.SelectedIndex == 0 || this.radNUTRITIONAL_STATUS.SelectedIndex == 1)
            //{
            //    if (this.radNUTRITIONAL_STATUS.SelectedIndex == 0)
            //    {
            //        this.txtNUTRITIONAL_STATUS_BAD.Text = string.Empty;
            //        this.txtNUTRITIONAL_STATUS_BAD.Enabled = false;
            //    }
            //    else
            //    {
            //        this.txtNUTRITIONAL_STATUS_BAD.Enabled = true;
            //    }
            //}
        }

        /// <summary>
        /// /// <summary>
        /// 营养情况建议调整选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radNUTRITIONAL_STATUS1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.radNUTRITIONAL_STATUS1.SelectedIndex == 0 || this.radNUTRITIONAL_STATUS1.SelectedIndex == 1)
            //{
            //    this.txtNUTRITIONAL_STATUS_NOTE.Enabled = true;
            //}
            //else
            //{
            //    this.txtNUTRITIONAL_STATUS_NOTE.Enabled = false;
            //}
        }

        #endregion

        #region 方法

        public override void SetBottomPanel()
        {
            this.panelControl1.Visible = false;
        }
        /// <summary>
        /// 绑定下拉项
        /// </summary>
        private void BindDropdownItems()
        {
            DataTable dtStaffSict = staffDictService.GetStaffDictList();
            if (dtStaffSict != null && dtStaffSict.Rows.Count > 0)
            {
                DataTable dtPunctureNurseList = Utility.GetSubTable(dtStaffSict, "ZYNAME='医生'", "name");
                if (dtPunctureNurseList != null && dtPunctureNurseList.Rows.Count > 0)
                {
                    BaseControlInfo.BindLookUpEdit(cmbDOCTOR_ID, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "记录医生");
                }
            }

            //DataTable dtConfig = configService.GetConfigList(string.Empty, string.Empty, "净化方式", "1");
            //if (dtConfig != null && dtConfig.Rows.Count > 0)
            //{
            //    BaseControlInfo.BindLookUpEdit(cmbPURIFICATION_MODE, "ITEM_ID", "ITEM_NAME", dtConfig, "ITEM_NAME", "净化方式");
            //}

            //DataTable dtConfig2 = dtConfig.Copy();
            //if (dtConfig2 != null && dtConfig2.Rows.Count > 0)
            //{
            //    var row = dtConfig2.NewRow();
            //    row["ITEM_ID"] = "0";
            //    row["ITEM_NAME"] = "无变化";
            //    dtConfig2.Rows.Add(row);
            //    this.repositoryItemLookUpEdit1.DataSource = dtConfig2;
            //}

            DataTable dtYear = new DataTable();
            dtYear.Columns.Add(new DataColumn("Year"));
            DataRow rowYear;

            for (int i = 2010; i <= 2020; i++)
            {
                rowYear = dtYear.NewRow();
                rowYear["Year"] = i.ToString();
                dtYear.Rows.Add(rowYear);
            }

            BaseControlInfo.BindLookUpEdit(this.lupFromYear, "Year", "Year", dtYear, "Year", "年份");
            BaseControlInfo.BindLookUpEdit(this.lupToYear, "Year", "Year", dtYear, "Year", "年份");

            DataTable dtMonth = new DataTable();
            dtMonth.Columns.Add(new DataColumn("Month"));
            DataRow rowMonth;

            for (int i = 1; i <= 12; i++)
            {
                rowMonth = dtMonth.NewRow();
                rowMonth["Month"] = i.ToString();
                dtMonth.Rows.Add(rowMonth);
            }

            BaseControlInfo.BindLookUpEdit(this.lupFromMonth, "Month", "Month", dtMonth, "Month", "月份");
            BaseControlInfo.BindLookUpEdit(this.lupToMonth, "Month", "Month", dtMonth, "Month", "月份");
        }

        /// <summary>
        /// 根据透析编号、日期查询病程记录
        /// </summary>
        private void Query()
        {
            if (this.lupFromYear.Text != string.Empty && this.lupFromMonth.Text != string.Empty && this.lupToYear.Text != string.Empty && this.lupToMonth.Text != string.Empty)
            {
                DateTime beginDate = Utility.CDate(this.lupFromYear.Text + "-" + this.lupFromMonth.Text);
                DateTime endDate = Utility.CDate(this.lupToYear.Text + "-" + (int.Parse(this.lupToMonth.Text) + 1).ToString());
                LoadPatientProgressNoteByHemoIdAndDate(currentHemoId, beginDate, endDate);
            }
            else if (this.lupFromYear.Text == string.Empty && this.lupFromMonth.Text == string.Empty && this.lupToYear.Text == string.Empty && this.lupToMonth.Text == string.Empty)
            {
                LoadPatientProgressNoteByHemoId(currentHemoId);
            }
            else
            {
                XtraMessageBox.Show("起始截止日期选择不完整！", "病程记录");
            }
        }
        public override void Query(DateTime dtStar, DateTime dtEnd)
        {
            LoadPatientProgressNoteByHemoIdAndDate(currentHemoId, dtStar, dtEnd);
        }
        /// <summary>
        /// 根据病人透析ID载入该病人病程记录列表
        /// </summary>
        /// <param name="hemoId"></param>
        private void LoadPatientProgressNoteByHemoId(string hemoId)
        {
            progressNodeDataTable = hemodialysisService.GetPatientProgressNoteByHemoId(hemoId);
            this.gcProgressNote.DataSource = progressNodeDataTable as DataTable;
        }

        /// <summary>
        /// 根据病人透析ID、日期载入该病人病程记录列表
        /// </summary>
        /// <param name="hemoId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        private void LoadPatientProgressNoteByHemoIdAndDate(string hemoId, DateTime beginDate, DateTime endDate)
        {
            progressNodeDataTable = hemodialysisService.GetPatientProgressNoteByHemoIdAndDate(hemoId, beginDate, endDate);
            this.gcProgressNote.DataSource = progressNodeDataTable as DataTable;
        }

        /// <summary>
        /// 初始化病程记录数据
        /// </summary>
        /// <param name="row"></param>
        private void InitData(DataRow row)
        {
            if (row != null)
            {
                this.lblPATIENT_NAME.Text = row["PATIENT_NAME"].ToString();
                this.lblHEMODIALYSIS_ID.Text = row["HEMODIALYSIS_ID"].ToString();
                this.txtCREATE_DATE.EditValue = row["CREATE_DATE"];
                this.cmbDOCTOR_ID.EditValue = row["DOCTOR_ID"].ToString();

                //不适主诉
                this.radCOMPLAINTS.SelectedIndex = row["COMPLAINTS"].ToString() == "0" ? 0 : 1;
                this.txtCOMPLAINTS_CONTENT.Text = row["COMPLAINTS_CONTENT"].ToString();

                //血管通路
                string strVASCULAR_ACCESS = row["VASCULAR_ACCESS"].ToString();
                //string strVASCULAR_ACCESS_SUGADJ = row["VASCULAR_ACCESS_SUGADJ"].ToString();
                if (strVASCULAR_ACCESS == "0" || strVASCULAR_ACCESS == "1")
                {
                    this.radVASCULAR_ACCESS.SelectedIndex = Utility.CInt(strVASCULAR_ACCESS);
                }
                //if (strVASCULAR_ACCESS_SUGADJ == "0" || strVASCULAR_ACCESS_SUGADJ == "1")
                //{
                //    this.radVASCULAR_ACCESS1.SelectedIndex = Utility.CInt(strVASCULAR_ACCESS_SUGADJ);
                //}
                this.txtVASCULAR_ACCESS_CHANGE.Text = row["VASCULAR_ACCESS_CHANGE"].ToString();
                this.txtVASCULAR_ACCESS_NOTE.Text = row["VASCULAR_ACCESS_NOTE"].ToString();

                //抗凝治疗  
                string strTHERAPEUTIC_METHOD = row["THERAPEUTIC_METHOD"].ToString();
                //string strTHERAPEUTIC_METHOD_SUGADJ = row["THERAPEUTIC_METHOD_SUGADJ"].ToString();
                if (strTHERAPEUTIC_METHOD == "0" || strTHERAPEUTIC_METHOD == "1")
                {
                    this.radTHERAPEUTIC_METHOD.SelectedIndex = Utility.CInt(strTHERAPEUTIC_METHOD);
                }
                //if (strTHERAPEUTIC_METHOD_SUGADJ == "0" || strTHERAPEUTIC_METHOD_SUGADJ == "1")
                //{
                //    this.radTHERAPEUTIC_METHOD1.SelectedIndex = Utility.CInt(strTHERAPEUTIC_METHOD_SUGADJ);
                //}
                this.txtTHERAPEUTIC_METHOD_CHANGE.Text = row["THERAPEUTIC_METHOD_CHANGE"].ToString();
                this.txtTHERAPEUTIC_METHOD_NOTE.Text = row["THERAPEUTIC_METHOD_NOTE"].ToString();

                //容量控制
                this.radCAPACITY_CONTROL.EditValue = row["CAPACITY_CONTROL"].ToString();
                this.txtDRY_WEIHGT.Text = row["DRY_WEIHGT"].ToString();
                this.txtMAX_DRY_WEIHGT.Text = row["MAX_DRY_WEIHGT"].ToString();
                this.lblPERCENT_DRY_WEIGHT.Text = "(" + row["PERCENT_DRY_WEIGHT"].ToString() + "%干体重)";

                //血压控制
                this.radBLOOD_CONTROL.EditValue = row["BLOOD_CONTROL"].ToString();
                this.txtSYSTOLIC_PRESSURE.Text = row["HIGH_BLOOD_PRESSURE"].ToString();
                this.txtDIASTOLIC_PRESSURE.Text = row["LOW_BLOOD_PRESSURE"].ToString();

                //贫血纠正
                string strANEMIA_CORRECTION = row["ANEMIA_CORRECTION"].ToString();
                //string strANEMIA_CORRECTION_SUGADJ = row["ANEMIA_CORRECTION_SUGADJ"].ToString();
                if (strANEMIA_CORRECTION == "0" || strANEMIA_CORRECTION == "1")
                {
                    this.radANEMIA_CORRECTION.SelectedIndex = Utility.CInt(strANEMIA_CORRECTION);
                }
                //if (strANEMIA_CORRECTION_SUGADJ == "0" || strANEMIA_CORRECTION_SUGADJ == "1")
                //{
                //    this.radANEMIA_CORRECTION1.SelectedIndex = Utility.CInt(strANEMIA_CORRECTION_SUGADJ);
                //}
                this.txtANEMIA_CORRECTION_BAD.Text = row["ANEMIA_CORRECTION_BAD"].ToString();
                this.txtANEMIA_CORRECTION_NOTE.Text = row["ANEMIA_CORRECTION_NOTE"].ToString();

                //骨矿物质代谢絮乱控制
                string strBONE_MINERAL = row["BONE_MINERAL"].ToString();
                //string strBONE_MINERAL_SUGADJ = row["BONE_MINERAL_SUGADJ"].ToString();
                if (strBONE_MINERAL == "0" || strBONE_MINERAL == "1")
                {
                    this.radBONE_MINERAL.SelectedIndex = Utility.CInt(strBONE_MINERAL);
                }
                //if (strBONE_MINERAL_SUGADJ == "0" || strBONE_MINERAL_SUGADJ == "1")
                //{
                //    this.radBONE_MINERAL1.SelectedIndex = Utility.CInt(strBONE_MINERAL_SUGADJ);
                //}
                this.txtBONE_MINERAL_BAD.Text = row["BONE_MINERAL_BAD"].ToString();
                this.txtBONE_MINERAL_NOTE.Text = row["BONE_MINERAL_NOTE"].ToString();

                //营养情况
                string strNUTRITIONAL_STATUS = row["NUTRITIONAL_STATUS"].ToString();
                //string strNUTRITIONAL_STATUS_SUGADJ = row["NUTRITIONAL_STATUS_SUGADJ"].ToString();
                if (strNUTRITIONAL_STATUS == "0" || strNUTRITIONAL_STATUS == "1")
                {
                    this.radNUTRITIONAL_STATUS.SelectedIndex = Utility.CInt(strNUTRITIONAL_STATUS);
                }
                //if (strNUTRITIONAL_STATUS_SUGADJ == "0" || strNUTRITIONAL_STATUS_SUGADJ == "1")
                //{
                //    this.radNUTRITIONAL_STATUS1.SelectedIndex = Utility.CInt(strNUTRITIONAL_STATUS_SUGADJ);
                //}
                this.txtNUTRITIONAL_STATUS_BAD.Text = row["NUTRITIONAL_STATUS_BAD"].ToString();
                this.txtNUTRITIONAL_STATUS_NOTE.Text = row["NUTRITIONAL_STATUS_NOTE"].ToString();

                //备注
                this.txtPROGRESS_NODE.Text = row["PROGRESS_NODE"].ToString();
            }
        }

        /// <summary>
        /// 加载默认数据
        /// </summary>
        private void LoadDefaultData()
        {
            if (currentPatient == null)
            {
                DataTable dtPatient = patientService.GetPatientListByParams(string.Empty, currentHemoId);
                if (dtPatient != null && dtPatient.Rows.Count > 0)
                {
                    currentPatient = dtPatient.Rows[0] as PatientModel.MED_PATIENTSRow;
                }
            }

            if (currentPatient != null)
            {
                this.lblPATIENT_NAME.Text = currentPatient.NAME;
                this.lblSex.Text = currentPatient.SEX;
                lblAge.Text = Utility.GetAge(currentPatient.BIRTHDAY.ToString()).ToString();
            }

            this.lblHEMODIALYSIS_ID.Text = currentHemoId;
            this.txtCREATE_DATE.EditValue = System.DateTime.Now;
            this.cmbDOCTOR_ID.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
            this.radCOMPLAINTS.SelectedIndex = 0;
            this.radVASCULAR_ACCESS.SelectedIndex = 0;
            this.radTHERAPEUTIC_METHOD.SelectedIndex = 0;
            this.radCAPACITY_CONTROL.SelectedIndex = 0;
            this.radBLOOD_CONTROL.SelectedIndex = 0;
            this.radANEMIA_CORRECTION.SelectedIndex = 0;
            this.radBONE_MINERAL.SelectedIndex = 0;
            this.radNUTRITIONAL_STATUS.SelectedIndex = 0;
        }

        public override void LoadInfo()
        {
            this.Text = "病程记录";

            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountUI(this);

            BindDropdownItems();
            SetControlEnabled(true);
            LoadPatientProgressNoteByHemoId(CurrentHemoId);
            dtCureMain = hemodialysisService.GetMainCureByHemoID(CurrentHemoId);
            LoadDefaultData();
        }

        private bool IsDataValidate()
        {
            this.errorProvider.SetError(this.txtCREATE_DATE, string.Empty);
            this.errorProvider.SetError(this.txtPROGRESS_NODE, string.Empty);

            if (string.IsNullOrEmpty(this.txtCREATE_DATE.Text))
            {
                this.txtCREATE_DATE.Focus();
                this.errorProvider.SetError(this.txtCREATE_DATE, "记录时间不能为空");
                return false;
            }

            //if (string.IsNullOrEmpty(this.txtPROGRESS_NODE.Text))
            //{
            //    this.txtPROGRESS_NODE.Focus();
            //    this.errorProvider.SetError(this.txtPROGRESS_NODE, "病程记录不能为空");
            //    return false;
            //}
            return true;
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
                    this._loadForm = new SplashScreenManager(new Form(), typeof(FrmWaitForm), true, true);
                    //this._loadForm.CloseWaitForm();.ClosingDelay = 0;
                }
                return _loadForm;
            }
        }
        public override void GetVascualToUpLoad(string baseInfo)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(this.ParentForm.FindForm(), typeof(SplashScreen1));

            var dtSource = this.gcProgressNote.DataSource as HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable;
            //var dtSource = this.gcProgressNote.DataSource as ((System.Data.DataView)(this.gcProgressNote.DataSource)).Table as HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable;

            var dt = new DataReportModel.MED_PATIENT_DATAREPORTDataTable();
            ConfigModel.MED_COMMON_ITEMLISTDataTable dtConfig = configService.GetConfigList(string.Empty, string.Empty, "质控平台访问配置", "1");
            string userName = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("QCLoginName")).ITEM_VALUE;
            string saveApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("SavePatientProgressNote")).ITEM_VALUE;//保存病程记录的Api
            string getUserApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("GetUserByName")).ITEM_VALUE;
            string getTokenApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("GetToken")).ITEM_VALUE;

            MessageLog.Instance().Log(new LogEntity() { Type = userName + "--" + saveApi, LogDate = DateTime.Now });

            //获取用户信息
            ResultMsg<MedUserInfo> resultMsg = WebApiClient.GetUserByName(userName, getUserApi, getTokenApi);
            if (resultMsg == null)
            {
                XtraMessageBox.Show("验证质控平台用户信息失败！");
                return;
            }
            if (resultMsg.StatusCode == (int)StatusCodeEnum.Success)
            {
                MessageLog.Instance().Log(new LogEntity() { Type = getUserApi, LogDate = DateTime.Now, Content = resultMsg.Data.ToString() });

                var userInfo = JsonConvert.DeserializeObject<MedUserInfo>(resultMsg.Data.ToString());
                if (userInfo != null)
                {
                    int noteCount = 0;
                    foreach (HemodialysisModel.MED_PATIENT_PROGRESS_NOTERow row in dtSource.Rows)
                    {
                        if (row.ISUPLOAD == "1")
                        {
                            noteCount++;
                            DevExpress.XtraSplashScreen.SplashScreenManager.Default.SendCommand(SplashScreen1.SplashScreenCommand.SetText, string.Format("总共{0}条病程记录，正在上传第{1}条病程记录，请稍等...", dtSource.Where(note => note.ISUPLOAD == "1").Count().ToString(), noteCount.ToString()));
                            ResultMsg<string> result = WebApiClient.SavePatientProgressNoteInfo(new MED_PATIENT_PROGRESS_NOTEINFO()
                            {
                                ID = Guid.NewGuid().ToString(),
                                HospitalId = userInfo.Company_ID,
                                HospitalName = userInfo.CompanyName,
                                HospitalYear = DateTime.Now,
                                Creattime = DateTime.Now,
                                Creator = HemoApplicationContext.Current.CurrentUser.USER_NAME,
                                Editor = HemoApplicationContext.Current.CurrentUser.USER_NAME,
                                Edittime = DateTime.Now,
                                PATIENT_ID = row.PATIENT_ID,
                                HEMODIALYSIS_ID = row.HEMODIALYSIS_ID,
                                PROGRESS_NODE = row.PROGRESS_NODE,
                                DOCTOR_ID = row["DOCTOR_NAME"].ToString(),
                                COMPLAINTS = row.COMPLAINTS,
                                CAPACITY_CONTROL = row.CAPACITY_CONTROL,
                                DRY_WEIHGT = row.DRY_WEIHGT,
                                MAX_DRY_WEIHGT = row.MAX_DRY_WEIHGT,
                                PERCENT_DRY_WEIGHT = row.PERCENT_DRY_WEIGHT,
                                BLOOD_CONTROL = row.BLOOD_CONTROL,
                                /// <summary>
                                /// 收缩压
                                /// </summary>
                                HIGH_BLOOD_PRESSURE = row.HIGH_BLOOD_PRESSURE,
                                /// <summary>
                                /// 舒张压
                                /// </summary>
                                LOW_BLOOD_PRESSURE = row.LOW_BLOOD_PRESSURE,
                                /// <summary>
                                /// 血管通路 0=无变化、1=有变化
                                /// </summary>
                                VASCULAR_ACCESS = row.VASCULAR_ACCESS,
                                /// <summary>
                                /// 抗凝治疗 0=无变化、1=有变化
                                /// </summary>
                                THERAPEUTIC_METHOD = row.THERAPEUTIC_METHOD,
                                /// <summary>
                                /// 促红素方案
                                /// </summary>
                                ERYTHROPOIETIN = row.ERYTHROPOIETIN,
                                /// <summary>
                                /// 净化方式
                                /// </summary>
                                PURIFICATION_MODE = row.PURIFICATION_MODE,
                                /// <summary>
                                /// 并发症
                                /// </summary>
                                COMPLICATION = row.COMPLICATION,
                                /// <summary>
                                /// 血管通路建议、调整
                                /// </summary>
                                VASCULAR_ACCESS_NOTE = row.VASCULAR_ACCESS_NOTE,
                                /// <summary>
                                /// 抗凝治疗建议、调整
                                /// </summary>
                                THERAPEUTIC_METHOD_NOTE = row.THERAPEUTIC_METHOD_NOTE,
                                /// <summary>
                                /// 贫血纠正 0=达标、1=不达标
                                /// </summary>
                                ANEMIA_CORRECTION = row.ANEMIA_CORRECTION,
                                /// <summary>
                                /// 贫血纠正建议、调整
                                /// </summary>
                                ANEMIA_CORRECTION_NOTE = row.ANEMIA_CORRECTION_NOTE,
                                /// <summary>
                                /// 贫血纠正不达标记录
                                /// </summary>
                                ANEMIA_CORRECTION_BAD = row.ANEMIA_CORRECTION_BAD,
                                /// <summary>
                                /// 骨矿物质代谢絮乱控制 0=达标、1=不达标
                                /// </summary>
                                BONE_MINERAL = row.BONE_MINERAL,
                                /// <summary>
                                /// 骨矿物质代谢絮乱控制建议、调整
                                /// </summary>
                                BONE_MINERAL_NOTE = row.BONE_MINERAL_NOTE,
                                /// <summary>
                                /// 骨矿物质代谢絮乱控制不达标记录
                                /// </summary>
                                BONE_MINERAL_BAD = row.BONE_MINERAL_BAD,
                                /// <summary>
                                /// 营养情况 0=达标、1=不达标
                                /// </summary>
                                NUTRITIONAL_STATUS = row.NUTRITIONAL_STATUS,
                                /// <summary>
                                /// 营养情况建议、调整
                                /// </summary>
                                NUTRITIONAL_STATUS_NOTE = row.NUTRITIONAL_STATUS_NOTE,
                                /// <summary>
                                /// 营养情况不达标记录
                                /// </summary>
                                NUTRITIONAL_STATUS_BAD = row.NUTRITIONAL_STATUS_BAD,
                                /// <summary>
                                /// 血管通路变化
                                /// </summary>
                                VASCULAR_ACCESS_CHANGE = row.VASCULAR_ACCESS_CHANGE,
                                /// <summary>
                                /// 抗凝治疗变化
                                /// </summary>
                                THERAPEUTIC_METHOD_CHANGE = row.THERAPEUTIC_METHOD_CHANGE,
                                /// <summary>
                                /// 不适主诉内容
                                /// </summary>
                                COMPLAINTS_CONTENT = row.COMPLAINTS_CONTENT,
                                /// <summary>
                                /// 血管通路 0=建议、1=调整
                                /// </summary>
                                VASCULAR_ACCESS_SUGADJ = row.VASCULAR_ACCESS_SUGADJ,
                                /// <summary>
                                /// 抗凝治疗 0=建议、1=调整
                                /// </summary>
                                THERAPEUTIC_METHOD_SUGADJ = row.THERAPEUTIC_METHOD_SUGADJ,
                                /// <summary>
                                /// 贫血纠正 0=建议、1=调整
                                /// </summary>
                                ANEMIA_CORRECTION_SUGADJ = row.ANEMIA_CORRECTION_SUGADJ,
                                /// <summary>
                                /// 骨矿物质代谢絮乱控制 0=建议、1=调整
                                /// </summary>
                                BONE_MINERAL_SUGADJ = row.BONE_MINERAL_SUGADJ,
                                /// <summary>
                                /// 营养情况 0=建议、1=调整
                                /// </summary>
                                NUTRITIONAL_STATUS_SUGADJ = row.NUTRITIONAL_STATUS_SUGADJ,
                                /// <summary>
                                /// 是否删除
                                /// </summary>
                                IsDelete = 0
                            }, saveApi, getTokenApi);

                            if (result != null && result.StatusCode == (int)StatusCodeEnum.Success)
                            {
                                var rowExtend = dt.NewMED_PATIENT_DATAREPORTRow();
                                rowExtend.ID = Guid.NewGuid().ToString();
                                rowExtend.HEMODIALYSIS_ID = row.HEMODIALYSIS_ID;
                                rowExtend.BASEINFO = result.Info;
                                rowExtend.STATE = "1";//成功
                                rowExtend.TYPE = "2";
                                rowExtend.EXTEND = "HZBCXX";
                                rowExtend.EXTEND1 = "患者病程记录信息";
                                rowExtend.EXTEND5 = "福建省上报平台";
                                rowExtend.UPTIME = System.DateTime.Now;
                                rowExtend.UPUSER = HemoApplicationContext.Current.CurrentUser.USER_ID;
                                rowExtend.MAPIP = row.ID;
                                dt.AddMED_PATIENT_DATAREPORTRow(rowExtend);
                            }
                            else
                            {
                                var rowExtend = dt.NewMED_PATIENT_DATAREPORTRow();
                                rowExtend.ID = Guid.NewGuid().ToString();
                                rowExtend.HEMODIALYSIS_ID = row.HEMODIALYSIS_ID;
                                rowExtend.BASEINFO = result.Info;
                                rowExtend.STATE = "0";//失败
                                rowExtend.TYPE = "2";
                                rowExtend.EXTEND = "HZBCXX";
                                rowExtend.EXTEND1 = "患者病程记录信息";
                                rowExtend.EXTEND5 = "福建省上报平台";
                                rowExtend.UPTIME = System.DateTime.Now;
                                rowExtend.UPUSER = HemoApplicationContext.Current.CurrentUser.USER_ID;
                                rowExtend.MAPIP = row.ID;
                                dt.AddMED_PATIENT_DATAREPORTRow(rowExtend);
                            }
                        }
                    }

                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();

                    if (noteCount == 0)
                    {
                        XtraMessageBox.Show("没有患者病程记录要上传！");
                        return;
                    }

                    var count = objDataReport.SavePatientIsUploadDt(dt);
                    if (count > 0)
                    {
                        XtraMessageBox.Show("上传成功！");
                    }
                    else
                    {
                        XtraMessageBox.Show("上传失败！");
                    }
                }
                else
                {
                    XtraMessageBox.Show("验证质控平台用户信息失败！");
                }
            }
            else
            {
                XtraMessageBox.Show("上传失败！\r\n" + resultMsg.Info);
            }
        }

        public override void CheckAllState(bool isCheck)
        {
            try
            {
                var dtSource = ((System.Data.DataView)(this.gvProgressNote.DataSource)).Table as HemodialysisModel.MED_PATIENT_PROGRESS_NOTEDataTable;
                foreach (HemodialysisModel.MED_PATIENT_PROGRESS_NOTERow row in dtSource.Rows)
                {
                    if (row.UPSTATE == "已上传")
                        continue;

                    row.ISUPLOAD = isCheck ? "1" : "0";
                }
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// 设置控件是否启用
        /// </summary>
        /// <param name="flag"></param>
        private void SetControlEnabled(bool flag)
        {
            this.btnQuery.Enabled = flag;
            this.btnPrint.Enabled = flag;
            this.btnNew.Enabled = flag;
            this.btn_Save.Enabled = !flag;
            this.btnDelete.Enabled = flag;
        }

        /// <summary>
        /// 清除控件文本内容
        /// </summary>
        private void ClearControlText()
        {
            this.lblPATIENT_NAME.Text = string.Empty;
            this.lblSex.Text = string.Empty;
            this.lblAge.Text = string.Empty;
            this.lblHEMODIALYSIS_ID.Text = string.Empty;
            this.txtCREATE_DATE.EditValue = null;
            this.cmbDOCTOR_ID.EditValue = null;

            this.radCOMPLAINTS.EditValue = null;
            this.txtCOMPLAINTS_CONTENT.Text = string.Empty;

            this.radVASCULAR_ACCESS.EditValue = null;
            this.txtVASCULAR_ACCESS_CHANGE.Text = string.Empty;
            //this.radVASCULAR_ACCESS1.EditValue = null;
            this.txtVASCULAR_ACCESS_NOTE.Text = string.Empty;

            this.radTHERAPEUTIC_METHOD.EditValue = null;
            this.txtTHERAPEUTIC_METHOD_CHANGE.Text = string.Empty;
            //this.radTHERAPEUTIC_METHOD1.EditValue = null;
            this.txtTHERAPEUTIC_METHOD_NOTE.Text = string.Empty;

            this.radCAPACITY_CONTROL.EditValue = null;
            this.txtDRY_WEIHGT.Text = string.Empty;
            this.txtMAX_DRY_WEIHGT.Text = string.Empty;
            this.lblPERCENT_DRY_WEIGHT.Text = "(     %干体重)";

            this.radBLOOD_CONTROL.EditValue = null;
            this.txtSYSTOLIC_PRESSURE.Text = string.Empty;
            this.txtDIASTOLIC_PRESSURE.Text = string.Empty;

            this.radANEMIA_CORRECTION.EditValue = null;
            this.txtANEMIA_CORRECTION_BAD.Text = string.Empty;
            //this.radANEMIA_CORRECTION1.EditValue = null;
            this.txtANEMIA_CORRECTION_NOTE.Text = string.Empty;

            this.radBONE_MINERAL.EditValue = null;
            this.txtBONE_MINERAL_BAD.Text = string.Empty;
            //this.radBONE_MINERAL1.EditValue = null;
            this.txtBONE_MINERAL_NOTE.Text = string.Empty;

            this.radNUTRITIONAL_STATUS.EditValue = null;
            this.txtNUTRITIONAL_STATUS_BAD.Text = string.Empty;
            //this.radNUTRITIONAL_STATUS1.EditValue = null;
            this.txtNUTRITIONAL_STATUS_NOTE.Text = string.Empty;

            this.txtPROGRESS_NODE.Text = string.Empty;
        }

        #endregion      
    }
}