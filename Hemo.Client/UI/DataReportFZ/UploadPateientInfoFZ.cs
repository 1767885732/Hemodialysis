/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：患者信息上传用户控件
// 创建时间：2018-03-07
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
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using Hemo.Service;
using Hemo.Model;
using Hemo.Client.UI.Hemodialysis;
using Hemo.Client.UI.Machine;
using Hemo.IService.DataReport;
using HEMODataReporter;
using HEMODataReporter.PostEntity;
using Hemo.Client.Core;
using Hemo.Utilities;
using Hemo.Client.Modules;
using Hemo.HQCWebClient.Models;
using Hemo.HQCWebClient;
using Newtonsoft.Json;
using Hemo.IService.Config;
using Hemo.IService;
using DevExpress.XtraSplashScreen;
using Hemo.Client.Controls;

namespace Hemo.Client.UI.DataReportFZ
{
    public partial class UploadPateientInfoFZ : ViewBase
    {
        #region 私有成员
        /// <summary>
        /// 病人列表
        /// </summary>
        private DataReportModel.MED_PATIENTSDataTable _patientDataTable;
        private IDataReport objDataReport = ServiceManager.Instance.DataReportService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        private PatientModel.MED_PATIENTSDataTable dtSourceTemp = new PatientModel.MED_PATIENTSDataTable();// dtSource.Copy() as DataReportModel.MED_PATIENTSDataTable;

        private DataReporter reporter = null;
        #endregion

        #region 初始化方法
        public UploadPateientInfoFZ()
        {
            InitializeComponent();

            InzationData();
        }
        #endregion

        #region 各种事件

        private void btnQuery_Click(object sender, EventArgs e)
        {
            InzationData();
        }

        /// <summary>
        /// 对于 已上传的患者，双击进入明细 上传，自动打开明细
        /// 单机事件，进行选择上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                //双击已上传患者自动打开明细项目
                if (dr.UPSTATE == "已上传")
                {
                    //var form = this.Parent.FindForm() as DataReportManager;
                    var form = this.Parent.Parent.Parent.Parent as DataReportManagerMgr;
                    form._currentPatientRow = dr;
                    form.SetMenuVisbleFZ(true);
                    form.Close("患者治疗信息");
                    form.barButtonItem8_ItemClick(null, null);

                }
                else
                {
                    XtraMessageBox.Show("当前患者未上传，请先上传患者信息才可以继续上传其它信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }
        private void gridView4_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            var rowCurrent = this.gridView4.GetFocusedDataRow() as DataReportModel.MED_PATIENTSRow;

            if (rowCurrent == null || e.CellValue == null) return;

            if (e.Column == gridColumn16)
            {
                if (e.CellValue.ToString().Equals("已上传"))
                {
                    e.Appearance.Font = new Font("Tahoma", 11, FontStyle.Bold);
                    e.Appearance.BackColor = Color.Green;
                }
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
            XtraMessageBox.Show("已上传不能再上传");
        }
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioGroupFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            var patientSource = new DataReportModel.MED_PATIENTSDataTable();
            switch (this.radioGroupFilter.SelectedIndex.ToString())
            {
                case "0":
                    _patientDataTable.CopyToDataTable<DataReportModel.MED_PATIENTSRow>(patientSource, LoadOption.PreserveChanges);
                    break;
                case "1":
                    _patientDataTable.Where(i => i.ISUPLOAD == "2").CopyToDataTable<DataReportModel.MED_PATIENTSRow>(patientSource, LoadOption.PreserveChanges);
                    break;
                case "2":
                    _patientDataTable.Where(i => i.ISUPLOAD == "1").CopyToDataTable<DataReportModel.MED_PATIENTSRow>(patientSource, LoadOption.PreserveChanges);
                    break;
                default:
                    break;
            }
            this.gridControl1.DataSource = patientSource;
        }

        private void checkAll_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                var dtSource = ((System.Data.DataView)(this.gridView4.DataSource)).Table as DataReportModel.MED_PATIENTSDataTable;
                foreach (DataReportModel.MED_PATIENTSRow row in dtSource.Rows)
                {
                    if (row.UPSTATE == "已上传")
                        continue;

                    row.ISUPLOAD = this.checkAll.Checked ? "1" : "0";

                }
            }
            catch (Exception ex) { }
        }
        /// <summary>
        /// 文本框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPatientName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnQuery_Click(null, null);
            }
        }

        #endregion

        #region 数据方法

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

        private void btnUpLoad_Click(object sender, EventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(this.ParentForm.FindForm(), typeof(SplashScreen1));

            var dtSource = ((System.Data.DataView)(this.gridView4.DataSource)).Table as DataReportModel.MED_PATIENTSDataTable;

            var dt = new DataReportModel.MED_PATIENT_DATAREPORTDataTable();
            ConfigModel.MED_COMMON_ITEMLISTDataTable dtConfig = _configService.GetConfigList(string.Empty, string.Empty, "质控平台访问配置", "1");
            string userName = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("QCLoginName")).ITEM_VALUE;
            string saveApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("SaveMedPatient")).ITEM_VALUE;
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
                    int patientCount = 0;
                    foreach (DataReportModel.MED_PATIENTSRow row in dtSource.Rows)
                    {
                        if (row.ISUPLOAD == "1")
                        {
                            patientCount++;
                            DevExpress.XtraSplashScreen.SplashScreenManager.Default.SendCommand(SplashScreen1.SplashScreenCommand.SetText, string.Format("总共{0}条患者记录，正在上传第{1}条患者记录，请稍等...", dtSource.Where(p => p.ISUPLOAD == "1").Count().ToString(), patientCount.ToString()));
                            ResultMsg<string> result = WebApiClient.SavePatientInfo(new MedPatientsInfo()
                            {
                                ID = Guid.NewGuid().ToString(),
                                HospitalId = userInfo.Company_ID,
                                HospitalName = userInfo.CompanyName,
                                HospitalYear = DateTime.Now,
                                Createtime = DateTime.Now,
                                Creator = HemoApplicationContext.Current.CurrentUser.USER_NAME,
                                Name = row.NAME,
                                Address = row.IsADDRESSNull() ? string.Empty : row.ADDRESS,
                                AdmissionNumber = row.IsADMISSION_NUMBERNull() ? string.Empty : row.ADMISSION_NUMBER,
                                Age = Convert.ToInt32(row.AGE),
                                BedNo = row.IsBED_NONull() ? string.Empty : row.BED_NO,
                                Birthday = row.IsBIRTHDAYNull() ? DateTime.MinValue : row.BIRTHDAY,
                                CredentialsNumber = row.IsCREDENTIALS_NUMBERNull() ? string.Empty : row.CREDENTIALS_NUMBER,
                                CredentialsType = row.IsCREDENTIALS_TYPENull() ? string.Empty : row.CREDENTIALS_TYPE,
                                Diagnose = row.IsDIAGNOSENull() ? string.Empty : row.DIAGNOSE,
                                Education = row.IsEDUCATIONNull() ? string.Empty : row.EDUCATION,
                                FirstVisit = row.IsFIRST_VISITNull() ? string.Empty : row.FIRST_VISIT,
                                HemodialysisId = row.HEMODIALYSIS_ID,
                                InfectiousCheckResult = row.IsINFECTIOUS_CHECK_RESULTNull() ? string.Empty : row.INFECTIOUS_CHECK_RESULT,
                                InputCode = row.IsINPUT_CODENull() ? string.Empty : row.INPUT_CODE,
                                IsDelete = 0,
                                IsNew = row.IsIS_NEWNull() ? string.Empty : row.IS_NEW,
                                Job = row.IsJOBNull() ? string.Empty : row.JOB,
                                LeaveHospitalTime = row.IsLEAVE_HOSPITAL_TIMENull() ? DateTime.MinValue : row.LEAVE_HOSPITAL_TIME,
                                Marital = row.IsMARITALNull() ? string.Empty : row.MARITAL,
                                MedicalType = row.IsMEDICAL_TYPENull() ? string.Empty : row.MEDICAL_TYPE,
                                Nation = row.IsNATIONNull() ? string.Empty : row.NATION,
                                Nativeplace = row.IsNATIVEPLACENull() ? string.Empty : row.NATIVEPLACE,
                                PatientId = row.IsPATIENT_IDNull() ? string.Empty : row.PATIENT_ID,
                                Sex = row.IsSEXNull() ? string.Empty : row.SEX,
                                SpecificTime = row.IsSPECIFIC_TIMENull() ? DateTime.Now : row.SPECIFIC_TIME,
                                Telephone = row.IsTELEPHONENull() ? string.Empty : row.TELEPHONE,
                                TimeType = row.IsTIME_TYPENull() ? string.Empty : row.TIME_TYPE,/*VisitId =  row.IsCREDENTIALS_NUMBERNull()?string.Empty:row.VISIT_ID,*/
                                WardCode = row.IsWARD_CODENull() ? string.Empty : row.WARD_CODE,
                                WhatDepartmentIn = row.IsWHAT_DEPARTMENT_INNull() ? string.Empty : row.WHAT_DEPARTMENT_IN,
                                WhatHospitalIn = row.IsWHAT_HOSPITAL_INNull() ? string.Empty : row.WHAT_HOSPITAL_IN,
                                WorkTelephone = row.IsWORK_TELEPHONENull() ? string.Empty : row.WORK_TELEPHONE
                            }, saveApi, getTokenApi);

                            if (result != null && result.StatusCode == (int)StatusCodeEnum.Success)
                            {
                                var rowExtend = dt.NewMED_PATIENT_DATAREPORTRow();
                                rowExtend.ID = Guid.NewGuid().ToString();
                                rowExtend.HEMODIALYSIS_ID = row.HEMODIALYSIS_ID;
                                rowExtend.BASEINFO = result.Info;
                                rowExtend.STATE = "1";//成功
                                rowExtend.TYPE = "0";
                                rowExtend.EXTEND = "HZXX";
                                rowExtend.EXTEND1 = "患者基本信息";
                                rowExtend.EXTEND5 = "福建省上报平台";
                                rowExtend.UPTIME = System.DateTime.Now;
                                rowExtend.UPUSER = HemoApplicationContext.Current.CurrentUser.USER_ID;
                                rowExtend.MAPIP = row.HEMODIALYSIS_ID;
                                dt.AddMED_PATIENT_DATAREPORTRow(rowExtend);
                                var rowTemp = dtSourceTemp.FirstOrDefault(i => i.HEMODIALYSIS_ID == row.HEMODIALYSIS_ID);
                                if (rowTemp != null)
                                    rowTemp.IS_UP = "1";
                            }
                            else
                            {
                                var rowExtend = dt.NewMED_PATIENT_DATAREPORTRow();
                                rowExtend.ID = Guid.NewGuid().ToString();
                                rowExtend.HEMODIALYSIS_ID = row.HEMODIALYSIS_ID;
                                rowExtend.BASEINFO = result.Info;
                                rowExtend.STATE = "0";//失败
                                rowExtend.TYPE = "0";
                                rowExtend.EXTEND = "HZXX";
                                rowExtend.EXTEND1 = "患者基本信息";
                                rowExtend.EXTEND5 = "福建省上报平台";
                                rowExtend.UPTIME = System.DateTime.Now;
                                rowExtend.UPUSER = HemoApplicationContext.Current.CurrentUser.USER_ID;
                                rowExtend.MAPIP = row.HEMODIALYSIS_ID;
                                dt.AddMED_PATIENT_DATAREPORTRow(rowExtend);
                            }
                        }
                    }

                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();

                    if (patientCount == 0)
                    {
                        XtraMessageBox.Show("没有患者记录要上传！");
                        return;
                    }

                    objPatient.SavePatientInfo(dtSourceTemp);
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
            InzationData();
        }

        /// <summary>
        /// 加载病人信息
        /// </summary>
        private void InzationData()
        {
            using (var _worker = new BackgroundWorker())
            {
                _patientDataTable = new DataReportModel.MED_PATIENTSDataTable();
                var patientSource = new DataReportModel.MED_PATIENTSDataTable();
                _worker.DoWork += delegate(object sender, DoWorkEventArgs e)
                {
                    //获取数据
                    _patientDataTable = objDataReport.GetDataReportPatientListFZ();

                };
                _worker.RunWorkerCompleted += delegate(object sender1, RunWorkerCompletedEventArgs r1)
                {
                    //条件过滤
                    if (txtPatientName.Text.Trim().Length > 0 && txtHemoID.Text.Trim().Length > 0)
                    {
                        _patientDataTable.Where(i => (i.NAME.Contains(this.txtPatientName.Text.Trim()) || i.INPUT_CODE.ToUpper().Contains(this.txtPatientName.Text.Trim().ToUpper())) && i.HEMODIALYSIS_ID == this.txtHemoID.Text.Trim()).CopyToDataTable<DataReportModel.MED_PATIENTSRow>(patientSource, LoadOption.PreserveChanges);
                    }
                    else if (txtPatientName.Text.Trim().Length > 0 && txtHemoID.Text.Trim().Length == 0)
                    {
                        _patientDataTable.Where(i => (i.NAME.Contains(this.txtPatientName.Text.Trim()) || i.INPUT_CODE.ToUpper().Contains(this.txtPatientName.Text.Trim().ToUpper()))).CopyToDataTable<DataReportModel.MED_PATIENTSRow>(patientSource, LoadOption.PreserveChanges);
                    }
                    else if (txtPatientName.Text.Trim().Length == 0 && txtHemoID.Text.Trim().Length > 0)
                    {
                        _patientDataTable.Where(i => i.HEMODIALYSIS_ID == this.txtHemoID.Text.Trim()).CopyToDataTable<DataReportModel.MED_PATIENTSRow>(patientSource, LoadOption.PreserveChanges);
                    }
                    else
                    {
                        _patientDataTable.CopyToDataTable<DataReportModel.MED_PATIENTSRow>(patientSource, LoadOption.PreserveChanges);
                    }
                    _patientDataTable.CopyToDataTable<DataReportModel.MED_PATIENTSRow>(dtSourceTemp, LoadOption.PreserveChanges);

                    this.gridControl1.DataSource = patientSource;
                };
                _worker.RunWorkerAsync();
            }
        }

        #endregion


    }
}