/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：用户基本信息上传管理用户控件
// 创建时间：2018-03-08
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
using System.Linq;
using DevExpress.XtraEditors;
using Hemo.Client.UI.Machine;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Hemo.Model;
using Hemo.Client.Controls;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Client.UI.PatientFixUI;
using Hemo.HQCWebClient.Models;
using Hemo.HQCWebClient;
using Newtonsoft.Json;
using Hemo.IService.DataReport;
using Hemo.Client.Core;
using DevExpress.XtraSplashScreen;
using Hemo.Utilities;

namespace Hemo.Client.UI.DataReportFZ.CureInfo
{
    public partial class PatientCureInfo : ViewBase
    {
        #region 类变量

        private ViewBase itemForm = null;

        #endregion

        #region 属性

        private IConfig configService = ServiceManager.Instance.ConfigService;
        private DataReportModel.MED_PATIENT_DATAREPORTDataTable dt = new DataReportModel.MED_PATIENT_DATAREPORTDataTable();

        public DataReportModel.MED_PATIENTSRow _currentPatientRow { get; set; }
        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;
        private IDataReport objDataReport = ServiceManager.Instance.DataReportService;

        private CtlBaseRecord ctlBaseRecord = null;
        private PatientModel.MED_BASE_RECORDDataTable dtRecord = null;

        #endregion

        #region 构造函数

        public PatientCureInfo(DataReportModel.MED_PATIENTSRow CurrentPatient)
        {
            InitializeComponent();
            _currentPatientRow = CurrentPatient;
            this.ctlUserLongInfo1.HEMODIALYSIS_ID = CurrentPatient.HEMODIALYSIS_ID;
            this.ctlUserLongInfo1.LoadPatientInfo();
            DateTime dt = DateTime.Now;  //当前时间  
            DateTime startQuarter = dt.AddMonths(0 - (dt.Month - 1) % 3).AddDays(1 - dt.Day);  //本季度初 

            DateTime endQuarter = startQuarter.AddMonths(3).AddDays(-1);  //本季度末  
            this.cmbSTART_DATE.DateTime = startQuarter.Date;
            this.cmbEND_DATE.DateTime = endQuarter.Date;

            this.treeViewInfo.SetFocusedNode(this.treeViewInfo.Nodes[0]);
            SetQuerBtnsVisit(false);

            var dtData = hemoService.GetBaseRecordByHemoId(_currentPatientRow.HEMODIALYSIS_ID);
            var itemForm1 = new CtlBaseRecord();
            itemForm1.HemoId = this.ctlUserLongInfo1.HEMODIALYSIS_ID;
            itemForm1.BeginDate = Utilities.Utility.CDate(this._currentPatientRow.SPECIFIC_TIME.ToString());
            itemForm1.BaseRecord = dtData;
            itemForm1.LoadBaseRecord();
            //itemForm1.Dock = DockStyle.Fill;

            this.xtraTabPage1.Controls.Add(itemForm1);
        }

        #endregion

        #region 事件

        private void btnUpLoad_Click(object sender, EventArgs e)
        {
            if (this.xtraTabPage1.Controls.Count > 0)
            {
                if (this.xtraTabPage1.Controls[0].Name == "CtlBaseRecord")
                {
                    ctlBaseRecord = this.xtraTabPage1.Controls[0] as CtlBaseRecord;
                    UpLoadBaseRecord();
                }
                else
                    itemForm.GetVascualToUpLoad(_currentPatientRow.BASEINFO);
            }
            else
            {
                XtraMessageBox.Show("上传失败，无上传内容！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Query();
        }

        private void checkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.xtraTabPage1.Controls.Count > 0)
            {
                if (this.xtraTabPage1.Controls[0].Name != "CtlBaseRecord")
                {
                    itemForm.CheckAllState(this.checkAll.Checked);
                }
            }
            else
            {
                XtraMessageBox.Show("上传失败，无上传内容！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void treeViewInfo_MouseDown(object sender, MouseEventArgs e)
        {
            #region 获取点击节点的信息

            TreeListHitInfo hi = treeViewInfo.CalcHitInfo(e.Location);
            TreeListNode CurrentNode = hi.Node;
            string currentValue = string.Empty;
            if (CurrentNode != null)
            {
                currentValue = CurrentNode.GetValue(LabAndCheckColumn).ToString();
            }
            else
            {
                return;
            }

            #endregion

            if (e.Button == MouseButtons.Left)//左键
            {
                this.xtraTabPage1.Controls.Clear();
                if (currentValue.Equals("患者病历"))
                {
                    SetQuerBtnsVisit(false);

                    var dt = hemoService.GetBaseRecordByHemoId(_currentPatientRow.HEMODIALYSIS_ID);
                    var itemForm1 = new CtlBaseRecord();
                    itemForm1.HemoId = this.ctlUserLongInfo1.HEMODIALYSIS_ID;
                    itemForm1.BeginDate = Utilities.Utility.CDate(this._currentPatientRow.SPECIFIC_TIME.ToString());
                    itemForm1.BaseRecord = dt;
                    itemForm1.LoadBaseRecord();
                    //itemForm1.Dock = DockStyle.Fill;
                    this.xtraTabPage1.Controls.Add(itemForm1);
                    Query();
                }
                else if (currentValue.Equals("患者病程"))
                {
                    SetQuerBtnsVisit(true);

                    itemForm = new PatientProgressNoteUI(_currentPatientRow.HEMODIALYSIS_ID);
                    itemForm.hemoId = _currentPatientRow.HEMODIALYSIS_ID;
                    itemForm.LoadInfo();
                    itemForm.SetBottomPanel();
                    itemForm.Dock = DockStyle.Fill;
                    this.xtraTabPage1.Controls.Add(itemForm);
                    Query();
                }
                else if (currentValue.Equals("透析记录"))
                {
                    SetQuerBtnsVisit(true);

                    itemForm = new UploadCureInfo(_currentPatientRow.HEMODIALYSIS_ID);
                    itemForm.hemoId = _currentPatientRow.HEMODIALYSIS_ID;
                    itemForm.Dock = DockStyle.Fill;
                    this.xtraTabPage1.Controls.Add(itemForm);
                    Query();
                }
            }
            else if (e.Button == MouseButtons.Right)//右键
            {
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Query();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            if (this.xtraTabPage1.Controls.Count > 0)
            {
                if (this.xtraTabPage1.Controls[0].Name != "CtlBaseRecord")
                {
                    itemForm.Query(this.cmbSTART_DATE.DateTime.AddYears(-2), this.cmbEND_DATE.DateTime);
                }

                else
                {
                    (this.xtraTabPage1.Controls[0] as CtlBaseRecord).LoadBaseRecord();
                }
            }
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
        /// <summary>
        /// 患者病历信息上传
        /// </summary>
        private void UpLoadBaseRecord()
        {
            //上传之前先检查是否已经上传过患者病历
            if (this.ctlBaseRecord.chkIsUp.Checked)
            {
                DialogResult dialog = XtraMessageBox.Show("患者病历已经上传过，是否继续？", "患者病历", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.Cancel)
                { return; }
            }
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(this.ParentForm.FindForm(), typeof(SplashScreen1));

            string msg = string.Empty;
            bool success = true;
            ConfigModel.MED_COMMON_ITEMLISTDataTable dtConfig = configService.GetConfigList(string.Empty, string.Empty, "质控平台访问配置", "1");
            string saveApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("SaveMedBaseRecord")).ITEM_VALUE;
            string getUserApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("GetUserByName")).ITEM_VALUE;
            string getTokenApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("GetToken")).ITEM_VALUE;
            string userName = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("QCLoginName")).ITEM_VALUE;

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
                    DevExpress.XtraSplashScreen.SplashScreenManager.Default.SendCommand(SplashScreen1.SplashScreenCommand.SetText, "正在上传病历信息，请稍等...");
                    dtRecord = this.ctlBaseRecord.GetBaseRecordDataTable();
                    var row = dtRecord.Rows.Count > 0 ? dtRecord[0] : null;
                    if (row != null)
                    {
                        MedBaseRecordInfo info = new MedBaseRecordInfo();
                        info.ID = Guid.NewGuid().ToString();
                        info.HospitalId = userInfo.Company_ID;
                        info.HospitalName = userInfo.CompanyName;
                        info.HospitalYear = System.DateTime.Now;
                        info.Createtime = System.DateTime.Now;
                        info.Creator = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                        info.Anoia = row.ANOIA;
                        info.Cad = row.CAD;
                        info.Cgn = row.CGN;
                        info.Chf = row.CHF;
                        info.Cin = row.CIN;
                        info.Copd = row.COPD;
                        info.Cva = row.CVA;

                        if (!row.IsDEAD_DATENull())
                        {
                            info.DeadDate = row.DEAD_DATE;
                        }
                        info.DeadReason = row.DEAD_REASON;

                        if (!row.IsDIALYSIS_BEGINNull())
                        {
                            info.DialysisBegin = row.DIALYSIS_BEGIN;
                        }
                        if (!row.IsDIALYSIS_ENDNull())
                        {
                            info.DialysisEnd = row.DIALYSIS_END;
                        }

                        info.DialysisEndReason = row.DIALYSIS_END_REASON;
                        info.DialysisYears = row.DIALYSIS_YEARS;
                        info.DialyzerAllergy = row.DIALYZER_ALLERGY;
                        info.Dm = row.DM;
                        info.Dn = row.DN;
                        info.DrugAllergy = row.DRUG_ALLERGY;
                        info.FamilyHistory = row.FAMILY_HISTORY;
                        info.FoodAllergy = row.FOOD_ALLERGY;
                        info.HemodialysisId = row.HEMODIALYSIS_ID;
                        info.PatientName = row["PatientName"].ToString();
                        info.Htn = row.HTN;

                        if (!row.IsINTO_DATENull())
                        {
                            info.IntoDate = row.INTO_DATE;
                        }

                        info.IntoHospital = row.INTO_HOSPITAL;
                        info.Isfirstdialysis = row.ISFIRSTDIALYSIS;
                        info.OnsetPass = row.ONSET_PASS;
                        info.OperationHistory = row.OPERATION_HISTORY;
                        info.OtherComorbidity = row.OTHER_COMORBIDITY;
                        info.OtherComorbidityText = row.OTHER_COMORBIDITY_TEXT;
                        info.OtherProtopathy = row.OTHER_PROTOPATHY;
                        info.OtherProtopathyText = row.OTHER_PROTOPATHY_TEXT;
                        info.Paod = row.PAOD;
                        info.Pckd = row.PCKD;
                        info.PdExist = row.PD_EXIST;
                        if (!row.IsPD_YEARNull())
                        {
                            info.PdYear = Convert.ToInt32(row.PD_YEAR);
                        }

                        info.PROGRESSNODE = "";
                        info.RenalTransplantExist = row.RENAL_TRANSPLANT_EXIST;
                        if (!row.IsRENAL_TRANSPLANT_YEARNull())
                        {
                            info.RenalTransplantYear = Convert.ToInt32(row.RENAL_TRANSPLANT_YEAR);
                        }

                        info.RenalTumor = row.RENAL_TUMOR;
                        info.Smoke = row.SMOKE;
                        if (!row.IsSMOKE_NUMNull())
                        {
                            info.SmokeNum = Convert.ToInt32(row.SMOKE_NUM);
                        }
                        if (!row.IsSMOKE_YEARNull())
                        {
                            info.SmokeYear = Convert.ToInt32(row.SMOKE_YEAR);
                        }

                        info.Tfsb = row.TFSB;
                        info.Uuo = row.UUO;
                        info.Xujiu = row.XUJIU;
                        info.XujiuDesc = row.XUJIU_DESC;
                        ResultMsg<string> result = WebApiClient.SaveBaseRecordInfo(info, saveApi, getTokenApi);

                        if (result == null)
                        {
                            XtraMessageBox.Show("上传失败！");
                            return;
                        }

                        if (result.StatusCode != (int)StatusCodeEnum.Success)
                        {
                            success = false;
                        }
                        msg = result.Info;
                        DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();
                    }
                    else
                    {
                        success = false;
                        msg = "患者病历信息为空！";
                    }
                }
                else
                {
                    success = false;
                    msg = "验证质控平台用户信息失败！";
                }
            }
            else
            {
                success = false;
                msg = resultMsg.Info;
            }

            if (success)
            {
                //更新上传标识
                dtRecord[0].IS_UP = "1";
                hemoService.SaveBaseRecord(dtRecord);
                this.ctlBaseRecord.chkIsUp.Checked = true;

                var rowExtend = dt.NewMED_PATIENT_DATAREPORTRow();
                rowExtend.ID = Guid.NewGuid().ToString();
                rowExtend.HEMODIALYSIS_ID = _currentPatientRow.HEMODIALYSIS_ID;
                rowExtend.BASEINFO = msg;
                rowExtend.STATE = "1";//成功
                rowExtend.TYPE = "1";
                rowExtend.EXTEND = "HZBLXX";
                rowExtend.EXTEND1 = "患者病历信息";
                rowExtend.EXTEND5 = "福建省上报平台";
                rowExtend.UPTIME = System.DateTime.Now;
                rowExtend.UPUSER = HemoApplicationContext.Current.CurrentUser.USER_ID;
                rowExtend.MAPIP = _currentPatientRow.HEMODIALYSIS_ID;
                dt.AddMED_PATIENT_DATAREPORTRow(rowExtend);
                var reINT = objDataReport.SavePatientIsUploadDt(dt);

                //记录上传日志
                //var dtUploadLog = new HemodialysisModel.MED_UPLOAD_LOGDataTable();
                //var drUploadLog = dtUploadLog.NewMED_UPLOAD_LOGRow();
                //drUploadLog.ID = Guid.NewGuid().ToString();
                //drUploadLog.UPLOAD_ITEM_NAME = "患者病历";
                //drUploadLog.HEMODIALYSIS_ID = hemoId;
                //drUploadLog.BELONG_YEAR = DateTime.Now.ToShortDateString();
                //drUploadLog.UPLOADER = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                //drUploadLog.UPLOAD_DATE = DateTime.Now;
                //dtUploadLog.AddMED_UPLOAD_LOGRow(drUploadLog);
                //hemoService.SaveUploadLog(dtUploadLog);
                XtraMessageBox.Show("上传成功！");
            }
            else
            {
                var rowExtend = dt.NewMED_PATIENT_DATAREPORTRow();
                rowExtend.ID = Guid.NewGuid().ToString();
                rowExtend.HEMODIALYSIS_ID = _currentPatientRow.HEMODIALYSIS_ID;
                rowExtend.BASEINFO = msg;
                rowExtend.STATE = "0";//失败
                rowExtend.TYPE = "1";
                rowExtend.EXTEND = "HZBLXX";
                rowExtend.EXTEND1 = "患者病历信息";
                rowExtend.EXTEND5 = "福建省上报平台";
                rowExtend.UPTIME = System.DateTime.Now;
                rowExtend.UPUSER = HemoApplicationContext.Current.CurrentUser.USER_ID;
                rowExtend.MAPIP = _currentPatientRow.HEMODIALYSIS_ID;
                dt.AddMED_PATIENT_DATAREPORTRow(rowExtend);
                var reINT = objDataReport.SavePatientIsUploadDt(dt);
                XtraMessageBox.Show("上传失败！\r\n" + msg);
            }
        }

        private void SetQuerBtnsVisit(bool isVisit)
        {
            this.labelControl33.Visible = isVisit;
            this.labelControl62.Visible = isVisit;
            this.cmbSTART_DATE.Visible = isVisit;
            this.cmbEND_DATE.Visible = isVisit;
            this.btnSearch.Visible = isVisit;
            this.checkAll.Visible = isVisit;
        }

        #endregion
    }
}
