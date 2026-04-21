/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：患者检验信息查询同步用户控件类
// 创建时间：2016-7-20
// 创建者：吕志强
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Hemo.IService.Lab;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using System.Windows.Forms;
using System.Collections;
using Hemo.Client.Print;
using Hemo.IService.Config;
using Hemo.IService.Dict;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using Hemo.Client.UI.Machine;
using Hemo.Client.Core;
using Hemo.Client.Controls;
using Hemo.Client.UI.PatientFixUI;
using Hemo.Client.UI.Patient;
using DevExpress.XtraPrinting;

namespace Hemo.Client.UI.Lab
{
    public partial class ctlLabFrm : ViewBase
    {
        #region 变量

        public string currentHemoID = string.Empty;
        private PatientModel.MED_PATIENTSRow _patientRow;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private ILab _labService = ServiceManager.Instance.LabService;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;

        DataTable _dtPatientLab = new DataTable();

        private ArrayList al = new ArrayList();

        private ConfigModel.MED_COMMON_ITEMLISTDataTable _ItemTable;

        private HemodialysisModel.MED_INFECTIOUS_CHECKDataTable dtResult = new HemodialysisModel.MED_INFECTIOUS_CHECKDataTable();

        private ViewBase currentParent = null;

        #endregion

        #region 属性

        public ViewBase CurrentParent
        {
            get { return currentParent; }
            set { currentParent = value; }
        }

        #endregion

        #region 构造函数

        public ctlLabFrm(PatientModel.MED_PATIENTSRow patientRow)
        {
            InitializeComponent();
            _patientRow = patientRow;
            LoadLookUpEditList();
        }

        #endregion

        #region 方法

        public void LoadLabInfo(PatientModel.MED_PATIENTSRow patientRow)
        {
            _patientRow = patientRow;
            this.busyIndicator1.ShowLoadingScreenFor(gcLabMain);
            //this.LoadDefaultMonth();
            LoadLabHeadData();
            LoadLabMainData();
            this.busyIndicator1.HideLoadingScreen();
        }

        /// <summary>
        /// 同步检验记录
        /// </summary>
        private void SyncLabInfo()
        {
            bool result = false;
            this.busyIndicator1.ShowLoadingScreenFor(gcLabMain);
            //this.btnSyncLabInfo.Visible = false;
            // this.picLoading.Visible = true;
            var frm = new PatientIdFrm();
            frm._CurrenRow = this._patientRow;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //this._patientRow.PATIENT_ID = frm.LastPatientId;
            }
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += (o1, e1) =>
                {
                    try
                    {
                        string visit = string.Empty;
                        if (!this._patientRow.IsVISIT_IDNull())
                        {
                            visit = _patientRow.VISIT_ID.ToString();
                        }
                        string interFaceUrl = HemoApplicationContext.Current.InterFaceDate.FirstOrDefault(i => i.ITEM_NAME == "血透同步数据接口").ITEM_VALUE.ToString();
                        //string error = InterfaceUtility.SynchronizeSigleValidation(this._patientRow.PATIENT_ID, Utility.CInt(visit));
                        string error = InterfaceUtility.SynchronizeLabDatasByPatiets(frm.LastPatientId, interFaceUrl);

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
                            this.LoadLabMainData();
                        this.busyIndicator1.HideLoadingScreen();
                        //this.btnSyncLabInfo.Visible = true;
                        //     this.picLoading.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        AutoClosedMsgBox.ShowForm(ex.Message, "提示", 1000, MessageBoxIcon.Warning);
                    }
                };
                worker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// 加载窗体头部数据
        /// </summary>
        private void LoadLabHeadData()
        {
            this.ctlUserInfo.HEMODIALYSIS_ID = _patientRow.HEMODIALYSIS_ID;
            this.ctlUserInfo.LoadPatientInfo();
            currentHemoID = _patientRow.HEMODIALYSIS_ID;

            //载入患者筛选的的检验列表
            DataTable dt = _hemodialysisService.GetMedInfectiousCheckList(currentHemoID) as DataTable;
            gcPatientInfo.DataSource = dt;

            //if (_patientRow.SEX == "男") {
            //    lblPicture.BackgroundImage = Properties.Resources.boy;
            //}
            //else {
            //    lblPicture.BackgroundImage = Properties.Resources.gril;
            //}

            _ItemTable = this._configService.GetConfigList(string.Empty, string.Empty, "检验过滤项目", "1");
            checkedListBoxControl_Filter.Items.Clear();
            foreach (ConfigModel.MED_COMMON_ITEMLISTRow item in _ItemTable.Rows)
            {
                DevExpress.XtraEditors.Controls.CheckedListBoxItem checkitem = new DevExpress.XtraEditors.Controls.CheckedListBoxItem();
                checkitem.Description = item.ITEM_NAME;
                checkitem.Value = item.ITEM_NAME;
                this.checkedListBoxControl_Filter.Items.Add(checkitem);
            }
        }

        private void LoadLabList()
        {
            //DataTable dtLab = new DataTable();
            if (cmbSTART_DATE.EditValue != null && cmbEND_DATE.EditValue != null)
            {
                _dtPatientLab = this._labService.GetPatientLabListByDate(this._patientRow.HEMODIALYSIS_ID, Utility.CDate(cmbSTART_DATE.Text), Utility.CDate(cmbEND_DATE.Text));
                //if (_dtPatientLab == null || _dtPatientLab.Rows.Count == 0)
                //{
                //    if (this._patientRow.ADMISSION_NUMBER.Length > 0)
                //    {
                //        _dtPatientLab = this._labService.GetPatientLabListByDate(this._patientRow.ADMISSION_NUMBER, Utility.CDate(cmbSTART_DATE.Text), Utility.CDate(cmbEND_DATE.Text));
                //    }
                //}
            }
            else
            {
                _dtPatientLab = this._labService.GetPatientLabList(this._patientRow.HEMODIALYSIS_ID);
                //if (_dtPatientLab == null || _dtPatientLab.Rows.Count == 0)
                //{
                //    if (this._patientRow.ADMISSION_NUMBER.Length > 0)
                //    {
                //        _dtPatientLab = this._labService.GetPatientLabList(this._patientRow.ADMISSION_NUMBER);
                //    }
                //}
            }
        }

        private void LoadLabMainData()
        {
            LoadLabList();
            FilterLabMainDataToShow();
        }

        private void FilterLabMainDataToShow()
        {
            var groupList = (from r in this._dtPatientLab.AsEnumerable()
                             select new
                             {
                                 TEST_NO = r["TEST_NO"].ToString(),
                                 SPECIMEN = r["SPECIMEN"].ToString(),
                                 SUBJECT = r["SUBJECT"].ToString(),
                                 RESULTS_RPT_DATE_TIME = r["RESULTS_RPT_DATE_TIME"].ToString()
                             }).Distinct().ToList();
            if (al.Count > 0)
            {
                ArrayList dt = new ArrayList();
                for (int i = 0; i < al.Count; i++)
                {
                    foreach (var item in groupList.ToList())
                    {

                        if (al[i].ToString().Contains(item.SUBJECT) || item.SUBJECT.ToString().Contains(al[i].ToString()))
                        {
                            dt.Add(item);//.Rows.Add(item);
                        }
                    }
                }

                this.gcLabMain.DataSource = dt;
            }
            else
            {
                this.gcLabMain.DataSource = groupList;
            }
            this.LoadLabMainDetail();
        }

        /// <summary>
        /// 设置默认载入月时间
        /// </summary>
        private void LoadDefaultMonth()
        {
            DateTime dtStart = new DateTime(System.DateTime.Now.Year, 1, 1);
            DateTime dtEnd = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day);

            this.cmbSTART_DATE.EditValue = dtStart;
            this.cmbEND_DATE.EditValue = dtEnd;
        }

        private void LoadLabMainDetail()
        {
            if (this.gvLabMain.RowCount > 0)
            {
                //this._dtPatientLab = this._labService.GetPatientLabList(this._patientRow.PATIENT_ID);
                string testNo = this.gvLabMain.GetFocusedRowCellValue("TEST_NO").ToString();

                var list = (from r in this._dtPatientLab.AsEnumerable()
                            where string.Compare(testNo, r["TEST_NO"].ToString(), true) == 0
                            select new
                            {
                                REPORT_ITEM_NAME = r["REPORT_ITEM_NAME"],
                                RESULT = r["RESULT"],
                                UNITS = r["UNITS"],
                                REFER_CONTEXT = r["REFERENCE_RESULT"]
                            }).ToList();

                this.gcLabDetail.DataSource = list;
            }
            else
            {
                this.gcLabDetail.DataSource = null;
            }
        }

        private void LoadLookUpEditList()
        {
            BaseControlInfo.BindLookUpEdit(cmbHEPATITIS_B, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "检查值", "1"), "ITEM_NAME", "乙肝检查");
            BaseControlInfo.BindLookUpEdit(cmbHEPATITIS_C, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "检查值", "1"), "ITEM_NAME", "丙肝检查");
            BaseControlInfo.BindLookUpEdit(cmbSYPHILIS, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "检查值", "1"), "ITEM_NAME", "梅毒检查");
            BaseControlInfo.BindLookUpEdit(cmbHIV, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "检查值", "1"), "ITEM_NAME", "HIV检查");
            DataTable dtStaffSict = _staffDictService.GetStaffDictList();
            DataTable dtDoctorList = Utility.GetSubTable(dtStaffSict, "ZYNAME='医生'");
            if (dtDoctorList != null && dtDoctorList.Rows.Count > 0)
            {
                BaseControlInfo.BindLookUpEdit(cmbCHECK_USER_ID, "USER_NAME", "NAME", dtDoctorList, "NAME", "责任医生");
            }
        }

        private void SaveCheck()
        {
            currentHemoID = this.ctlUserInfo.HEMODIALYSIS_ID;
            if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                HemodialysisModel.MED_INFECTIOUS_CHECKRow drNew = null;
                if (dtResult.Rows.Count == 0)
                {
                    drNew = dtResult.NewMED_INFECTIOUS_CHECKRow();
                    drNew.HEMODIALYSIS_ID = currentHemoID;
                }
                else
                {
                    dtResult = _hemodialysisService.GetMedInfectiousInfoByID(dtResult.Rows[0]["INFECTIOUS_ID"].ToString());
                    drNew = dtResult.Rows[0] as HemodialysisModel.MED_INFECTIOUS_CHECKRow;
                }

                drNew.HEPATITIS_B = cmbHEPATITIS_B.Text;
                drNew.HEPATITIS_C = cmbHEPATITIS_C.Text;
                drNew.SYPHILIS = cmbSYPHILIS.Text;
                drNew.HIV = cmbHIV.Text;
                drNew.CHECK_USER_ID = cmbCHECK_USER_ID.Text;

                drNew.STATUS = "0";


                drNew.CREATE_DATE = System.DateTime.Now;
                drNew.HEPATITIS_B_DATE = Utility.CDate(Utility.CDate(datHEPATITIS_B_DATE.Text).ToShortDateString());
                drNew.HEPATITIS_C_DATE = Utility.CDate(Utility.CDate(datHEPATITIS_C_DATE.Text).ToShortDateString());
                drNew.SYPHILIS_DATE = Utility.CDate(Utility.CDate(datSYPHILIS_DATE.Text).ToShortDateString()); ;
                drNew.HIV_DATE = Utility.CDate(Utility.CDate(datHIV_DATE.Text).ToShortDateString());
                //drNew.PATIENT_ID = dt.Rows[0]["PATIENT_ID"].ToString();
                drNew.INFECTIOUS_ID = Guid.NewGuid().ToString();
                if (dtResult.Rows.Count == 0)
                {
                    dtResult.Rows.Add(drNew);
                }

                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    int result = _hemodialysisService.SaveMedInfectiousCheck(dtResult);
                    if (result != 0)
                    {
                        XtraMessageBox.Show("检验筛选记录保存成功！");
                        DataTable dt = _hemodialysisService.GetMedInfectiousCheckList(currentHemoID) as DataTable;
                        gcPatientInfo.DataSource = dt;
                    }
                }
            }
        }

        #endregion

        #region 事件

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void ctlLabFrm_Load(object sender, EventArgs e)
        {
            if (currentParent != null)
            {
                if (currentParent.GetType().Name.Equals("PatientFixInfosUI"))
                {
                    this.cmbSTART_DATE.EditValue = (currentParent as PatientFixInfosUI).LabFromTime.Equals(string.Empty) ? null : (currentParent as PatientFixInfosUI).LabFromTime;
                    this.cmbEND_DATE.EditValue = (currentParent as PatientFixInfosUI).LabToTime.Equals(string.Empty) ? null : (currentParent as PatientFixInfosUI).LabToTime;
                }
                else if (currentParent.GetType().Name.Equals("CtlUserCureList"))
                {
                    this.cmbSTART_DATE.EditValue = (currentParent as CtlUserCureList).LabFromTime.Equals(string.Empty) ? null : (currentParent as CtlUserCureList).LabFromTime;
                    this.cmbEND_DATE.EditValue = (currentParent as CtlUserCureList).LabToTime.Equals(string.Empty) ? null : (currentParent as CtlUserCureList).LabToTime;
                }
            }

            this.busyIndicator1.ShowLoadingScreenFor(gcLabMain);
            //this.LoadDefaultMonth();
            LoadLabHeadData();
            LoadLabMainData();
            this.busyIndicator1.HideLoadingScreen();
        }

        private void gvLabMain_RowClick(object sender, RowClickEventArgs e)
        {
            this.LoadLabMainDetail();
        }

        /// <summary>
        /// 同步检验记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSyncLabInfo_Click(object sender, EventArgs e)
        {
            this.SyncLabInfo();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (_dtPatientLab.Rows.Count == 0)
            {
                MessageBox.Show("没有可打印的数据！");
                return;
            }
            if (this.gvLabMain.GetFocusedRow() == null)
            {
                MessageBox.Show("请选择要打印的检验记录！");
                return;
            }
            string LabReportType = System.Configuration.ConfigurationManager.AppSettings["LabReportPrintType"].ToString();
            if (string.IsNullOrEmpty(LabReportType) || LabReportType != "0")
            {
                LabResultPrint frm = new LabResultPrint();
                frm.dtPatientLab = _dtPatientLab;
                frm.PatientRow = _patientRow;
                frm.Show();
                frm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
            else if (LabReportType.Equals("0"))
            {
                string testNo = this.gvLabMain.GetFocusedRowCellValue("TEST_NO").ToString();
                //分组
                var query = from t in _dtPatientLab.AsEnumerable()
                            where t.Field<string>("TEST_NO").Equals(testNo)
                            group t by new { t1 = t.Field<string>("TEST_NO"), t2 = t.Field<string>("SPECIMEN"), t3 = t.Field<string>("SUBJECT"), t4 = t.Field<DateTime?>("RESULTS_RPT_DATE_TIME") } into m
                            select new
                            {
                                TEST_NO = m.Key.t1,
                                SPECIMEN = m.Key.t2,
                                SUBJECT = m.Key.t3,
                                RESULT_DATE = m.Key.t4,
                            };


                var masterdt = new ReportRelationModel.LABMASTERDataTable();
                var detaildt = new ReportRelationModel.LABDETAILDataTable();

                foreach (var item in query.ToList())
                {
                    //取得主表
                    var newRow = masterdt.NewLABMASTERRow();
                    newRow.TEST_NO = item.TEST_NO;
                    newRow.ITEM_TYPE = item.SPECIMEN;
                    newRow.ITEMNAME = item.SUBJECT;
                    if (item.RESULT_DATE != null)
                    {
                        newRow.RESULT_DATE = item.RESULT_DATE.Value;
                    }
                    masterdt.AddLABMASTERRow(newRow);
                    //根据主表TESTNo获得检验明细
                    var itemRows = _dtPatientLab.AsEnumerable().Where(i => i["TEST_NO"].ToString() == item.TEST_NO).ToList();
                    foreach (var item1 in itemRows)
                    {
                        try
                        {
                            var detailRow = detaildt.NewLABDETAILRow();
                            detailRow.TEST_NO = item1["TEST_NO"].ToString();
                            detailRow.REPORT_ITEM_NAME = item1["REPORT_ITEM_NAME"].ToString();
                            detailRow.RESULT = item1["RESULT"].ToString();
                            detailRow.UNITS = item1["UNITS"].ToString();
                            detailRow.REFERENCE_RESULT = item1["REFERENCE_RESULT"].ToString();
                            detaildt.AddLABDETAILRow(detailRow);
                        }
                        catch (Exception a) { }
                    }
                }
                //传入进去
                PatientLabReport frm = new PatientLabReport(_patientRow, masterdt, detaildt);
                ReportPrintTool pt = new ReportPrintTool(frm);
                pt.ShowPreviewDialog();
            }
        }

        private void btnTrendChart_Click(object sender, EventArgs e)
        {
            HBTrendChart chart = new HBTrendChart(this._patientRow.PATIENT_ID, _patientRow.HEMODIALYSIS_ID);
            chart.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (currentParent != null)
            {
                if (currentParent.GetType().Name.Equals("PatientFixInfosUI"))
                {
                    (currentParent as PatientFixInfosUI).LabFromTime = this.cmbSTART_DATE.Text;
                    (currentParent as PatientFixInfosUI).LabToTime = this.cmbEND_DATE.Text;
                }
                else if (currentParent.GetType().Name.Equals("CtlUserCureList"))
                {
                    (currentParent as CtlUserCureList).LabFromTime = this.cmbSTART_DATE.Text;
                    (currentParent as CtlUserCureList).LabToTime = this.cmbEND_DATE.Text;
                }
            }

            this.busyIndicator1.ShowLoadingScreenFor(gcLabMain);
            LoadLabMainData();
            this.busyIndicator1.HideLoadingScreen();
        }

        private void checkedListBoxControl_Filter_SelectedValueChanged(object sender, EventArgs e)
        {
            foreach (var item in checkedListBoxControl_Filter.SelectedItems)
            {

            }
        }

        private void checkedListBoxControl_Filter_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            al = new ArrayList();
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in this.checkedListBoxControl_Filter.Items)
            {
                if (item.CheckState == CheckState.Checked)
                {
                    al.Add(item.Value.ToString());
                }
            }
            FilterLabMainDataToShow();
        }

        private void btnLabResult_Click(object sender, EventArgs e)
        {
            SaveCheck();
        }

        private void btn_call_Click(object sender, EventArgs e)
        {

        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            DataRow dr = gvMachine.GetFocusedDataRow();
            if (dr != null)
            {
                int result = _hemodialysisService.UpdateMedInfectiousInfoByID(dr["INFECTIOUS_ID"].ToString());
                if (result == 1)
                {
                    AutoClosedMsgBox.ShowForm("删除成功!", "病患管理", 1500, MessageBoxIcon.Information);
                    DataTable dt = _hemodialysisService.GetMedInfectiousCheckList(currentHemoID) as DataTable;
                    gcPatientInfo.DataSource = dt;
                }
            }
            else
            {
                AutoClosedMsgBox.ShowForm("请选择要删除的检验筛选记录!", "病患管理", 1500, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {

        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_Delete_Click(sender, e);
        }

        private void gcPatientInfo_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void gcPatientInfo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(MousePosition);
            }
        }

        private void gvMachine_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
            }
        }

        /// <summary>
        /// 导出检验右侧明细数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportLab_Click(object sender, EventArgs e)
        {
            var date = this.gvLabMain.GetFocusedRowCellValue("RESULTS_RPT_DATE_TIME").ToString();
            date = !string.IsNullOrEmpty(date) ? Utility.CDate(date).ToString("yyyyMMdd") : DateTime.Now.ToString("yyyyMMdd");
            string fileName = "检验信息" + "-" + _patientRow.NAME + "-" + date + "." + "xls";
            SaveFileDialog dialog = new SaveFileDialog() { Title = "导出Excel", FileName = fileName, Filter = "Excel文件(*.xls)|*.*", RestoreDirectory = true };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                XlsExportOptions option = new XlsExportOptions() { TextExportMode = TextExportMode.Text };
                this.gcLabDetail.ExportToXls(dialog.FileName);
                AutoClosedMsgBox.ShowForm("导出成功！", "提示", 1500, MessageBoxIcon.Information);
            }
        }

        #endregion
    }
}
