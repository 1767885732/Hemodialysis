/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者病历业务功能类
// 创建时间：2016-05-18
// 创建者：吕志强
//  
// 修改时间：2017-02-22
// 修改人：刘配齐
// 修改描述：修改界面及部分业务逻辑
//
----------------------------------------------------------------*/

using System;
using System.Data;
using Hemo.Model;
using Hemo.Service;
using Hemo.IService;
using System.Windows.Forms;
using Hemo.IService.Config;
using Hemo.Utilities;
using DevExpress.XtraEditors;
using Hemo.Client.Print;
using Hemo.Client.UI.Machine;
using Hemo.Client.Print;
using Hemo.Client.UI.Hemodialysis;
using DevExpress.XtraReports.UI;
using System.Configuration;
using Hemo.HQCWebClient.Models;
using Hemo.HQCWebClient;
using Newtonsoft.Json;
using System.Linq;
using Hemo.Client.Core;

namespace Hemo.Client.UI.PatientFixUI
{
    public partial class PatientBaseRecordUI : ViewBase
    {
        #region 类变量

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        private IConfig configService = ServiceManager.Instance.ConfigService;

        private string hemoId = string.Empty;

        private PatientModel.MED_BASE_RECORDDataTable dtRecord = null;

        private HemodialysisModel.MED_BASE_RECORD_EVENTDataTable dtEvent = null;

        private HemodialysisModel.MED_BASE_RECORD_DIAGNOSEDataTable dtDiagnose = null;

        #endregion

        #region 属性

        /// <summary>
        /// 透析编号
        /// </summary>
        public string HemoId
        {
            get { return hemoId; }
            set { hemoId = value; }
        }


        /// <summary>
        /// 透析开始日期
        /// </summary>
        private DateTime hemoBeginDate;
        public DateTime HemoBeginDate {
            get {
                return hemoBeginDate;
            }
            set {
                hemoBeginDate = value;
            }            
        }

        #endregion

        #region 构造函数

        public PatientBaseRecordUI()
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
        private void PatientBaseRecordUI_Load(object sender, EventArgs e)
        {
            LoadInfo();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            if (this.tcRecord.SelectedTabPageIndex == 1)
            {
                this.ctlBaseRecordEvent.New();
            }
            else
            {
                this.ctlBaseRecordDiagnose.New();
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_Click(object sender, EventArgs e)
        {
            int result = 0;
            string text = this.tcRecord.SelectedTabPage.Text;
            if (this.tcRecord.SelectedTabPageIndex == 0)
            {
                dtRecord = this.ctlBaseRecord.GetBaseRecordDataTable();
                result = hemoService.SaveBaseRecord(dtRecord);
            }
            else if (this.tcRecord.SelectedTabPageIndex == 1)
            {
                dtEvent = this.ctlBaseRecordEvent.GetRecordEventDataTable();
                result = hemoService.SaveRecordEvent(dtEvent);
                LoadRecordEvent();
            }
            else
            {
                dtDiagnose = this.ctlBaseRecordDiagnose.GetRecordDiagnoseDataTable();
                result = hemoService.SaveRecordDiagnose(dtDiagnose);
                LoadRecordDiagnose();
            }

            if (result > 0)
            {
                AutoClosedMsgBox.ShowForm("保存" + text + "成功！", "提醒", 1000, MessageBoxIcon.Warning);
            }
            else
            {
                AutoClosedMsgBox.ShowForm("保存" + text + "失败！", "提醒", 1000, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var row = this.tcRecord.SelectedTabPageIndex == 1 ? this.ctlBaseRecordEvent.GetFocusedDataRow() : this.ctlBaseRecordDiagnose.GetFocusedDataRow();
            if (row != null)
            {
                if (XtraMessageBox.Show("确认要删除选中的记录吗？", "患者病历", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    int result = this.tcRecord.SelectedTabPageIndex == 1 ? hemoService.DeleteRecordEventById(row["ID"].ToString()) : hemoService.DeleteRecordDiagnoseById(row["ID"].ToString());
                    if (result > 0)
                    {
                        XtraMessageBox.Show("删除记录成功！", "患者病历");
                        if (this.tcRecord.SelectedTabPageIndex == 1)
                        {
                            LoadRecordEvent();
                        }
                        else
                        {
                            LoadRecordDiagnose();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 选项卡页面改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tcRecord_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            this.btnNew.Enabled = (this.tcRecord.SelectedTabPageIndex == 0) ? false : true;
            this.btnDelete.Enabled = (this.tcRecord.SelectedTabPageIndex == 0) ? false : true;
            this.btnUpToWeb.Enabled = (this.tcRecord.SelectedTabPageIndex == 0 ? true : false);


            if (tcRecord.SelectedTabPage == tpreport)
            {
                this.patientRecipeFrm1.HemoId = hemoId;

                this.patientRecipeFrm1.InzationDateControl();
                this.patientRecipeFrm1.InzationData();
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            PatientBaseRecordReport report = new PatientBaseRecordReport(hemoId);
            ReportPrintTool pt = new ReportPrintTool(report);
            pt.ShowPreviewDialog();
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpToWeb_Click(object sender, EventArgs e)
        {
            if (this.tcRecord.SelectedTabPageIndex == 0)
            {
                //上传之前先检查是否已经上传过患者病历
                if (this.ctlBaseRecord.chkIsUp.Checked)
                {
                    DialogResult dialog = XtraMessageBox.Show("患者病历已经上传过，是否继续？", "患者病历", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dialog == DialogResult.Cancel)
                    { return; }
                }

                string msg = string.Empty;
                bool success = true;
                ConfigModel.MED_COMMON_ITEMLISTDataTable dtConfig = configService.GetConfigList(string.Empty, string.Empty, "质控平台访问配置", "1");
                string saveApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("SaveMedBaseRecord")).ITEM_VALUE;
                string getUserApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("GetUserByName")).ITEM_VALUE;
                string getTokenApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("GetToken")).ITEM_VALUE;
                string userName = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("QCLoginName")).ITEM_VALUE;

                //获取用户信息
                ResultMsg<MedUserInfo> resultMsg = WebApiClient.GetUserByName(userName, getUserApi, getTokenApi);
                if (resultMsg.StatusCode == (int)StatusCodeEnum.Success)
                {
                    var userInfo = JsonConvert.DeserializeObject<MedUserInfo>(resultMsg.Data.ToString());
                    if (userInfo != null)
                    {
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
                            info.Anoia = (row.ANOIA == "1" ? "有" : "");
                            info.Cad = (row.CAD == "1" ? "有" : "");
                            info.Cgn = (row.CGN == "1" ? "有" : "");
                            info.Chf = (row.CHF == "1" ? "有" : "");
                            info.Cin = (row.CIN == "1" ? "有" : "");
                            info.Copd = (row.COPD == "1" ? "有" : "");
                            info.Cva = (row.CVA == "1" ? "有" : "");

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
                            info.Dm = (row.DM == "1" ? "有" : "");
                            info.Dn = (row.DN == "1" ? "有" : "");
                            info.DrugAllergy = row.DRUG_ALLERGY;
                            info.FamilyHistory = row.FAMILY_HISTORY;
                            info.FoodAllergy = row.FOOD_ALLERGY;
                            info.HemodialysisId = row.HEMODIALYSIS_ID;
                            info.Htn = (row.HTN == "1" ? "有" : "");

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
                            info.Paod = (row.PAOD == "1" ? "有" : "");
                            info.Pckd = (row.PCKD == "1" ? "有" : "");
                            info.PdExist = (row.PD_EXIST == "1" ? "曾经" : "从未");
                            if (!row.IsPD_YEARNull())
                            {
                                info.PdYear = Convert.ToInt32(row.PD_YEAR);
                            }

                            info.PROGRESSNODE = "";
                            info.RenalTransplantExist = (row.RENAL_TRANSPLANT_EXIST == "1" ? "曾经" : "从未");
                            if (!row.IsRENAL_TRANSPLANT_YEARNull())
                            {
                                info.RenalTransplantYear = Convert.ToInt32(row.RENAL_TRANSPLANT_YEAR);
                            }

                            info.RenalTumor = (row.RENAL_TUMOR == "1" ? "有" : "");
                            info.Smoke = (row.SMOKE == "1" ? "有" : "");
                            if (!row.IsSMOKE_NUMNull())
                            {
                                info.SmokeNum = Convert.ToInt32(row.SMOKE_NUM);
                            }
                            if (!row.IsSMOKE_YEARNull())
                            {
                                info.SmokeYear = Convert.ToInt32(row.SMOKE_YEAR);
                            }

                            info.Tfsb = (row.TFSB == "1" ? "有" : "");
                            info.Uuo = (row.UUO == "1" ? "有" : "");
                            info.Xujiu = (row.XUJIU == "1" ? "有" : "");
                            info.XujiuDesc = row.XUJIU_DESC;
                            ResultMsg<string> result = WebApiClient.SaveBaseRecordInfo(info, saveApi, getTokenApi);
                            if (result.StatusCode != (int)StatusCodeEnum.Success)
                            {
                                success = false;
                                msg = result.Info;
                            }
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
                        msg = "质控平台用户信息为空！";
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

                    //记录上传日志
                    var dtUploadLog = new HemodialysisModel.MED_UPLOAD_LOGDataTable();
                    var drUploadLog = dtUploadLog.NewMED_UPLOAD_LOGRow();
                    drUploadLog.ID = Guid.NewGuid().ToString();
                    drUploadLog.UPLOAD_ITEM_NAME = "患者病历";
                    drUploadLog.HEMODIALYSIS_ID = hemoId;
                    drUploadLog.BELONG_YEAR = DateTime.Now.ToShortDateString();
                    drUploadLog.UPLOADER = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                    drUploadLog.UPLOAD_DATE = DateTime.Now;
                    dtUploadLog.AddMED_UPLOAD_LOGRow(drUploadLog);
                    hemoService.SaveUploadLog(dtUploadLog);
                    XtraMessageBox.Show("上传成功！");
                }
                else
                {
                    XtraMessageBox.Show("上传失败！\r\n" + msg);
                }
            }
        }

        #endregion

        #region 方法

        public void LoadInfo()
        {
            this.Text = "患者病历";

            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountUI(this);
            this.ctlBaseRecordEvent.Load += (o, arg) => this.ctlBaseRecordEvent.LoadControl();
            this.ctlBaseRecordDiagnose.Load += (o, arg) => this.ctlBaseRecordDiagnose.LoadControl();
            LoadUserInfo();
            LoadBaseRecord();
            LoadRecordEvent();
            LoadRecordDiagnose();
        }

        /// <summary>
        /// 加载用户信息
        /// </summary>
        private void LoadUserInfo()
        {
          //  this.ctlBaseUserInfo.HemoId = hemoId;
         //   this.ctlBaseUserInfo.LoadUserInfo();
        }

        /// <summary>
        /// 加载患者病历基本资料
        /// </summary>
        private void LoadBaseRecord()
        {
            var dt = hemoService.GetBaseRecordByHemoId(hemoId);
            //dtRecord = hemoService.GetBaseRecordByHemoId(hemoId);
            dtRecord = dt;
            this.ctlBaseRecord.HemoId = hemoId;
            this.ctlBaseRecord.BeginDate = hemoBeginDate;//this.ctlBaseUserInfo.BeginDate;
            this.ctlBaseRecord.BaseRecord = dtRecord;
            this.ctlBaseRecord.LoadBaseRecord();
        }

        /// <summary>
        /// 加载患者病历基本资料事件
        /// </summary>
        private void LoadRecordEvent()
        {
            dtEvent = hemoService.GetRecordEventByHemoId(hemoId);
            this.ctlBaseRecordEvent.HemoId = hemoId;
            this.ctlBaseRecordEvent.RecordEvent = dtEvent;
            this.ctlBaseRecordEvent.LoadRecordEvent();
        }

        /// <summary>
        /// 加载患者病历基本资料诊断
        /// </summary>
        private void LoadRecordDiagnose()
        {
            dtDiagnose = hemoService.GetRecordDiagnoseByHemoId(hemoId);
            this.ctlBaseRecordDiagnose.HemoId = hemoId;
            this.ctlBaseRecordDiagnose.RecordDiagnose = dtDiagnose;
            this.ctlBaseRecordDiagnose.LoadRecordDiagnose();
        }

        public void SetBaseRecordSize()
        {
            this.ctlBaseRecord.Width = this.tpLeft.Width - 20;
        }

        #endregion
    }
}