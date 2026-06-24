/*----------------------------------------------------------------
// Copyright (C) 2013 苏州医疗科技有限公司
// 描述：治疗单窗体 
// 创建时间：2013-04-02
// 创建者：刘超
//  
// 修改时间：2017-09-26
// 修改人：吕志强
// 修改描述：添加CRRT患者治疗单的业务逻辑
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
using System.Linq;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.IService.Config;
using DevExpress.XtraEditors.Controls;
using Hemo.IService;
using Hemo.IService.Dict;
using Hemo.IService.Machine;
using DevExpress.XtraGrid.Views.BandedGrid;
using Hemo.Client.Core;
using System.Diagnostics;
using Hemo.IService.PatientSchedule;
using Hemo.Client.UI.Machine;
using Hemo.Client.Controls;
using Hemo.IService.AuditLog;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class EditTreatment : ViewBase
    {
        #region 私有成员
        private bool isLoadDate = false;
        /// <summary>
        /// 治疗单主表 
        /// </summary>
        private HemodialysisModel.MED_CURE_MAINDataTable _CureMainDatatable;
        /// <summary>
        /// 透析参数记录表   
        /// </summary>
        private HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable _HemoialysisParamterDatatable;
        /// <summary>
        /// 给药表
        /// </summary>
        private HemodialysisModel.MED_CURE_DRUGDataTable _CureDrugDatatable;
        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IDrug objDrug = ServiceManager.Instance.DrugService;
        private IMachine objMachine = ServiceManager.Instance.MachineService;
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        private IPatientSchedule objPatientSchedule = ServiceManager.Instance.PatientSchedule;
        private IOperationLog _operationLogService = ServiceManager.Instance.OperationLogService;

        /// <summary>
        /// 并发症表
        /// </summary>
        private HemoModel.MED_COMPLICATION_OTHERDataTable complicatinTable;
        private int tabIndex = 0;
        public bool IsCahLoad = false;
        private bool isAdd = false;//是否为新增治疗单
        private bool isAddDrugParmars = false;
        private bool isAddHemoParmars = false;
        /// <summary>
        /// 页面加载完成后从UI控件拍的数据快照（用于审计日志差异对比的基准值）
        /// </summary>
        private Dictionary<string, string> _initialDataSnapshot = null;
        private bool isReplenishTreat = false;
        private string addHemoId = string.Empty;
        private string addPcureId = string.Empty;
        private int tabIndexTemp = 0;
        private PatientScheduleModel.MED_PATIENT_SCHEDULERow _patientScheduleRow = null;
        private DateTime cureDate = System.DateTime.Now;
        private MachineModel.MED_DIALYSIS_MACHINERow machineRow = null;
        private int loginType;
        private string banci = string.Empty;
        /// <summary>
        /// 是否为加透医嘱
        /// </summary>
        private bool _isOverOrder = false;
        public bool IsOverOrder
        {
            get { return _isOverOrder; }
            set { _isOverOrder = value; }
        }
        #endregion

        #region 审计日志辅助方法

        /// <summary>
        /// 获取当前登录用户ID
        /// </summary>
        private string GetCurrentUserId()
        {
            try
            {
                if (LoginUser.User != null)
                    return LoginUser.User.USER_ID;
            }
            catch { }
            return string.Empty;
        }

        /// <summary>
        /// 获取当前登录用户姓名
        /// </summary>
        private string GetCurrentUserName()
        {
            try
            {
                if (LoginUser.User != null)
                    return LoginUser.User.USER_NAME;
            }
            catch { }
            return string.Empty;
        }

        /// <summary>
        /// 获取当前登录用户登录名
        /// </summary>
        private string GetCurrentLoginName()
        {
            try
            {
                if (LoginUser.User != null)
                    return LoginUser.User.LOGIN_NAME;
            }
            catch { }
            return string.Empty;
        }

        /// <summary>
        /// 获取当前治疗单ID
        /// </summary>
        private string GetCurrentCureId()
        {
            return txtCURE_ID != null ? txtCURE_ID.Text.Trim() : string.Empty;
        }

        /// <summary>
        /// 获取当前血透编号
        /// </summary>
        private string GetCurrentHemodialysisId()
        {
            return ctlUserLongInfo1 != null ? ctlUserLongInfo1.HEMODIALYSIS_ID : string.Empty;
        }

        /// <summary>
        /// 记录操作审计日志（变化详情统一记录到 CHANGE_DETAIL 字段）
        /// </summary>
        /// <param name="operationType">操作类型（SAVE/DELETE/UPDATE等）</param>
        /// <param name="elementName">元素名称（如：透析参数、给药信息、血管通路等）</param>
        /// <param name="elementId">元素ID</param>
        /// <param name="changeDetail">变更详情（如：透前体重: 65 → 68; 实际脱水: 2 → 3）</param>
        /// <param name="remark">备注</param>
        private void WriteOperationLog(string operationType, string elementName, string elementId,
            string changeDetail, string remark)
        {
            try
            {
                _operationLogService.WriteLog(
                    GetCurrentUserId(),
                    GetCurrentUserName(),
                    GetCurrentLoginName(),
                    operationType,
                    "EditTreatment",
                    elementName,
                    elementId,
                    changeDetail,
                    GetCurrentCureId(),
                    GetCurrentHemodialysisId(),
                    remark
                );
            }
            catch (Exception ex)
            {
                // 审计日志写入失败不应影响主业务流程
                Hemo.Utilities.Logger.WriteErrorLog(ex);
            }
        }

        /// <summary>
        /// 将 DataTable 的行数据序列化为字符串（用于记录变更前后的值）
        /// </summary>
        private string SerializeDataRowToString(DataRow row)
        {
            if (row == null) return string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (DataColumn col in row.Table.Columns)
            {
                if (sb.Length > 0) sb.Append("; ");
                sb.AppendFormat("{0}={1}", col.ColumnName, row[col] == DBNull.Value ? "" : row[col].ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 数据库字段名 → 中文标签映射字典
        /// </summary>
        private static readonly Dictionary<string, string> FieldLabelMap = new Dictionary<string, string>
        {
            // 透前数据
            { "BEFORE_DRY_WEIGHT", "透前体重" },
            { "BEFORE_SYSTOLIC_PRESSURE", "透前收缩压" },
            { "BEFORE_DIASTOLIC_PRESSURE", "透前舒张压" },
            { "BEFORE_BP", "透前脉搏" },
            { "BEFORE_TEMPERATURE", "透前体温" },
            { "BR", "透前呼吸" },
            // 透后数据
            { "AFTER_DRY_WEIGHT", "透后体重" },
            { "AFTER_SYSTOLIC_PRESSURE", "透后收缩压" },
            { "AFTER_DIASTOLIC_PRESSURE", "透后舒张压" },
            { "AFTER_BP", "透后脉搏" },
            { "AFTER_TEMPERATURE", "透后体温" },
            { "AFTERBR", "透后呼吸" },
            // 基本治疗参数
            { "DRY_WEIGHT", "干体重" },
            { "DIALYSATE_FLOW", "透析液流量" },
            { "FREQUENCY_HOURS", "治疗时间(时)" },
            { "FREQUENCY_MINUTE", "治疗时间(分)" },
            { "CLEAN_UP_TIMES", "透析次数" },
            { "UFR", "预计脱水" },
            { "DRY_WATER_VALUE", "实际脱水" },
            // 滤过/置换相关
            { "FILTRATION_DISPLACEMENT_LIQUID", "透析中置换液总量" },
            { "FILTRATION_PERCOLATE", "滤过液总量" },
            { "DISPLACEMENT_LIQUID", "透析中血浆总量" },
            { "PERCOLATE", "滤过血浆总量" },
            // 抗凝相关
            { "FIRST_HEPARIN", "首量" },
            { "DOSAGE_SUSTENTATIVA", "追加" },
            { "HEPARIN_SPECIES", "抗凝方式" },
            { "FIRST_DRUG_UNIT", "首量单位" },
            // 净化/通路/设备
            { "PURIFICATION_MODE", "净化方式" },
            { "VASCULAR_ACCESS_ID", "血管通路" },
            { "MACHINE_TYPE", "透析器" },
            { "PURIFIER_NAME", "透析膜" },
            { "PURIFIER_M2", "膜面积" },
            { "MACHINE_ID", "透析机" },
            // 其他参数
            { "LastWeight", "上次透后体重" },
            { "DRY_WEIGHT_TAG", "衣物轮椅重" },
            { "IN_BED", "卧床" },
            // 人员
            { "PRIMARY_DOCTOR", "责任医生" },
            { "PRIMARY_NURSE", "责任护士" },
            { "PUNCTURE_NURSE", "穿刺护士" },
            { "CHECK_NURSE", "核对护士" },
            // 文本记录
            { "DOCTOR_ADVICE", "医生记录及医嘱" },
            { "SPECIAL_MATTER", "特殊事项" },
            { "SUMMARY", "透析小结" },
            { "SUMMARY2", "透析小结2" },
            { "SUMMARY3", "透析小结3" },
            // 评估
            { "SUBJECTIVE_COMFORT", "主观舒适度" },
            { "VEIN", "血管通路类型" },
            // 超滤相关
            { "UF", "超滤量" },
            { "UFR2", "超滤率" },
            { "SUM_UF", "总超滤量" },
            { "DISPLACEMENT_FLOW", "置换液流速" },
            // 导管评估
            { "VASCULAR_ACCESS_FIRM", "定牢固定" },
            { "VASCULAR_ACCESS_GLIDE", "滑脱" },
            { "VASCULAR_ACCESS_SWELLING", "穿刺点局部红肿" },
            { "VASCULAR_ACCESS_ERRHYISIS", "渗血" },
            { "VASCULAR_ACCESS_THROMBUS", "血栓" },
            { "VASCULAR_ACCESS_BLOOD", "血流情况" },
            { "VASCULAR_ACCESS_BLOOD_INFECT", "导管相关血流感染" },
            { "VASCULAR_ACCESS_PENDING", "下次评估待定" },
            { "VASCULAR_ACCESS_PENDING_DATE", "待定日期" },
            { "IN_BASKET_PLASTER_ALLERGY","渗液" },
            // CRRT相关
            { "CRRT_CLASS", "CRRT模式" },
            // 感染检查
            { "INFECTIOUS_CHECK_RESULT", "感染检查结果" },
        };

        /// <summary>
        /// 获取字段的中文名称，未找到则返回原始字段名
        /// </summary>
        private string GetFieldLabel(string fieldName)
        {
            if (FieldLabelMap.ContainsKey(fieldName))
                return FieldLabelMap[fieldName];
            return fieldName;
        }

        /// <summary>
        /// 将 DataRow 的列值深拷贝到字典中（用于后续差异对比）
        /// </summary>
        private Dictionary<string, string> CloneRowToDict(DataRow row)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (row == null) return dict;
            foreach (DataColumn col in row.Table.Columns)
            {
                string val = row[col] == DBNull.Value ? "" : row[col].ToString().Trim();
                dict[col.ColumnName] = val;
            }
            return dict;
        }

        /// <summary>
        /// 对比旧值字典和新行数据，返回发生变化的字段列表（使用中文标签）
        /// 格式：中文标签: 旧值 → 新值
        /// </summary>
        private string CompareDictWithRow(Dictionary<string, string> oldDict, DataRow newRow)
        {
            if (oldDict == null || oldDict.Count == 0 || newRow == null) return string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (var kv in oldDict)
            {
                if (!newRow.Table.Columns.Contains(kv.Key)) continue;
                string newVal = newRow[kv.Key] == DBNull.Value ? "" : newRow[kv.Key].ToString().Trim();
                if (kv.Value != newVal)
                {
                    if (sb.Length > 0) sb.Append("; ");
                    string label = GetFieldLabel(kv.Key);
                    sb.AppendFormat("{0}: {1} → {2}", label, kv.Value, newVal);
                }
            }
            return sb.ToString();
        }

        #endregion

        #region 属性

        public PatientScheduleModel.MED_PATIENT_SCHEDULERow PatientScheduleRow
        {
            get { return _patientScheduleRow; }
            set { this._patientScheduleRow = value; }
        }

        public DateTime CureDate
        {
            get { return cureDate; }
            set { this.cureDate = value; }
        }

        public MachineModel.MED_DIALYSIS_MACHINERow MachineRow
        {
            get { return machineRow; }
            set { machineRow = value; }
        }

        public string currentRecipeID { get; set; }

        public bool IsReplenishTreat
        {
            get { return isReplenishTreat; }
            set
            {
                isReplenishTreat = value;
                if (isReplenishTreat)
                {
                    this.panel_Top.Visible = true;
                    this.Text = "血透治疗记录单【补录】";
                    this.txtRECIPE_ID.Text = "2000000000";
                }
                else
                {
                    this.panel_Top.Visible = false;
                }
            }
        }

        public string Banci
        {
            get { return banci; }
            set { banci = value; }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pHemodialysisID"></param>
        /// <param name="pCureID"></param>
        /// <param name="pTabIndex"></param>
        /// <param name="loginType"></param>
        public EditTreatment(string pHemodialysisID, string pCureID, int pTabIndex, int loginType)
        {
            //Stopwatch watch = new Stopwatch();
            //watch.Start();
            InitializeComponent();

            //watch.Stop();
            //var a= watch.Elapsed;

            this.Text = "透析治疗";
            ProFunctionCount pfc = new ProFunctionCount();
            // pfc.SaveFunctionCountFrm(this);   很重要

            this.loginType = loginType;
            if (this.loginType != 0)
            {
                this.btnQJJL.Text = "护士抢救记录";
            }
            string pHemoID = string.Empty;
            this.Data_CureData.EditValue = System.DateTime.Now.ToString("yyyy-MM-dd");
            //刘超 2018-11-29  修复开始治疗时药品不刷新的问题
            addHemoId = txtHEMODIALYSIS_ID.Text = pHemodialysisID;
            addPcureId = pCureID;
            tabIndexTemp = pTabIndex;
            try
            {
                var patietDt = objPatient.GetPatientListByParams(string.Empty, pHemodialysisID);
                //    this.lupALLERGIC.Text = patietDt[0].BASEINFO;
            }
            catch { }
        }

        #endregion

        #region 载入数据

        public void LoadInfo(string pHemodialysisID, string pCureID, int pTabIndex, int loginType)
        {
            this.Text = "透析治疗";
            ProFunctionCount pfc = new ProFunctionCount();
            // pfc.SaveFunctionCountFrm(this);   很重要

            this.loginType = loginType;
            if (this.loginType != 0)
            {
                this.btnQJJL.Text = "护士抢救记录";
            }
            string pHemoID = string.Empty;
            this.Data_CureData.EditValue = System.DateTime.Now.ToString("yyyy-MM-dd");
            addHemoId = pHemodialysisID;
            addPcureId = pCureID;
            tabIndexTemp = pTabIndex;

            setDefaultValue(cmbVASCULAR_ACCESS_ID.Text.ToString());
            loadData(addHemoId, addPcureId, tabIndexTemp);
        }

        /// <summary>
        /// 设置默认初始值
        /// </summary>
        private void setDefaultValue(string pName)
        {
            if (pName.Contains("导管") || pName.Contains("置管"))
            {
                xtraTabControlPG.Visible = true;
                this.xtraTabControlPG.SelectedTabPage = xtraTabPageDGPG;
                if (this.groupControl4.Visible)
                {
                    this.groupBox1.Height = 613;
                }
                else
                {
                    this.groupBox1.Height = 519;
                }
                //if (this.groupBox1.Height == 164)
                //{
                //    this.groupControl4.Height = this.groupControl4.Height - 94;

                //}
            }
            else if (pName.Contains("内瘘"))
            {
                xtraTabControlPG.Visible = true;

                this.xtraTabControlPG.SelectedTabPage = xtraTabPageNLPG;
                if (this.groupControl4.Visible)
                {
                    this.groupBox1.Height = 613;
                }
                else
                {
                    this.groupBox1.Height = 519;
                }
                //if (this.groupControl4.Height == 94)
                //{
                //    this.groupControl4.Height = this.groupControl4.Height - 94;
                //}
            }
            else
            {

                xtraTabControlPG.Visible = false;

                if (this.groupControl4.Visible)
                {
                    this.groupBox1.Height = 543;
                }
                else
                {
                    this.groupBox1.Height = 449;


                }

                //groupBox1.Height = groupBox1.Height - 70;

                //if (this.groupControl4.Height == 70)
                //    this.groupControl4.Height = this.groupControl4.Height + 94;
            }
        }

        /// <summary>
        /// 载入数据 
        /// </summary>
        /// <param name="pHemodialysisID">血透编号</param>
        /// <param name="pCureID">治疗单号</param>
        private void loadData(string pHemodialysisID, string pCureID, int pTabIndex)
        {
            if (string.IsNullOrEmpty(pCureID))
            {
                isAdd = true;
                isAddDrugParmars = true;
            }
            else { isAdd = false; }
            ctlUserLongInfo1.HEMODIALYSIS_ID = pHemodialysisID;
            txtHEMODIALYSIS_ID.Text = pHemodialysisID;
            txtCURE_ID.Text = pCureID;
            ctlUserLongInfo1.LoadPatientInfo();
            ctlUserLongInfo1.PatientTypeEnabled = true;
            LoadLookUpEditList();
            lopDRUG_NURSE_ID.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
            lopDISPENSINGNURSE.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
            lopCHECKNURSE.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;

            lupNURSE_ID.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
            //   lupCHECK_NURSE.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
            //设置药品录入控件是否可用属性
            setDrugEnabled(false);
            setParamterEnabled(false);
            //控制选项卡显示
            switch (pTabIndex)
            {
                case 0:
                    tabControls.SelectedTabPage = tab1;
                    loadMainCureInfo(pCureID);
                    break;
                case 1:
                    tabControls.SelectedTabPage = tab2;
                    loadDrugGrid(pCureID, pHemodialysisID);
                    if (_CureMainDatatable == null)
                    {
                        loadMainCureInfo(pCureID);
                    }
                    break;
                case 2:
                    tabControls.SelectedTabPage = tab3;
                    loadParaneterGrid(pCureID, pHemodialysisID);
                    if (_CureMainDatatable == null)
                    {
                        loadMainCureInfo(pCureID);
                    }
                    break;
                case 3:
                    tabControls.SelectedTabPage = tab4;
                    if (_CureMainDatatable == null)
                    {
                        loadMainCureInfo(pCureID);
                    }
                    break;
                case 5:
                    tabControls.SelectedTabPage = xtraTabPage1;
                    //加载并发症数据
                    this.LoadComplication();
                    break;
                default:
                    tabControls.SelectedTabPage = tab1;
                    break;
            }
        }

        /// <summary>
        /// 方法需要优化三张表数据合并为一个DATASET
        /// </summary>
        /// <param name="pCureID">治疗单编号</param>
        private void loadParaneterGrid(string pCureID, string pHemoID)
        {
            this.gridControl1.DataSource = null;
            _HemoialysisParamterDatatable = objHemodialysisService.GetHemoParametersByCureID(pCureID);

            if (_HemoialysisParamterDatatable != null && _HemoialysisParamterDatatable.Rows.Count > 0)
            {
                this.gridControl1.DataSource = _HemoialysisParamterDatatable;
                if (_patientScheduleRow["AREANAME"].ToString().Equals("CRRT"))
                {
                    var dtResult = _HemoialysisParamterDatatable.Clone();
                    DateTime createDate = banci.Equals("3") ? cureDate.Date.AddDays(1) : cureDate.Date;
                    _HemoialysisParamterDatatable.Where(row => row.CREATE_DATE.Date.CompareTo(createDate) == 0 && row.CRRT_CLASS.Equals(banci)).CopyToDataTable(dtResult, LoadOption.OverwriteChanges);
                    this.gridControl1.DataSource = dtResult;
                }

                //有数据时更新排班表中的开始时间
                if (_HemoialysisParamterDatatable.Rows.Count == 1)
                {
                    var StartTime = _HemoialysisParamterDatatable[0].CREATE_DATE;
                    var RecipeID = _HemoialysisParamterDatatable[0].RECIPE_ID;
                    var ScheduleDt = objPatientSchedule.GetPatientScheduleByRecipeId(RecipeID);

                    if (ScheduleDt != null && ScheduleDt.Rows.Count > 0)
                    {
                        ScheduleDt[0].START_TIME = StartTime;
                        objPatientSchedule.SavePatientScheduleInfo(ScheduleDt);
                        this.PatientScheduleRow.START_TIME = StartTime;
                    }
                }
            }
            loadDrugGrid(pCureID, pHemoID);
        }

        private void loadDrugGrid(string pCureID, string pHemoID)
        {
            _CureDrugDatatable = new HemodialysisModel.MED_CURE_DRUGDataTable();
            //载入给药信息列表
            var drugDtTemp = string.IsNullOrEmpty(txtRECIPE_ID.Text) ? objHemodialysisService.GetValidCureDrugByHemoID(pHemoID, cureDate) : objHemodialysisService.GetValidCureDrugByHemoRecipeID(pHemoID, txtRECIPE_ID.Text);

            if (IsOverOrder)
            {
                drugDtTemp.Where(i => !i.IsEXECUTE_STATUSNull() && i.EXECUTE_STATUS == "1").CopyToDataTable<HemodialysisModel.MED_CURE_DRUGRow>(_CureDrugDatatable, LoadOption.PreserveChanges);
            }
            else
            {
                drugDtTemp.Where(i => i.IsEXECUTE_STATUSNull() || i.EXECUTE_STATUS != "1").CopyToDataTable<HemodialysisModel.MED_CURE_DRUGRow>(_CureDrugDatatable, LoadOption.PreserveChanges);
            }

            if (_CureDrugDatatable != null && _CureDrugDatatable.Rows.Count > 0)
            {
                gridControl4.DataSource = _CureDrugDatatable;
            }
            else
            {
                gridControl4.DataSource = null;
            }
        }

        /// <summary>
        /// 根据时间获取小时和分钟单位
        /// </summary>
        /// <param name="pHours"></param>
        private void getHourAndMinute(string pHours)
        {
            //spnFREQUENCY_MINUTE.Text = pHours;
            if (pHours.IndexOf(".") > 0)
            {
                string[] strArrHours = pHours.Split('.');
                if (strArrHours.Length > 0)
                {
                    spnFREQUENCY_HOURS.Text = strArrHours[0];
                    spnFREQUENCY_MINUTE.Text = Utility.GetMinuteByHours(strArrHours[1]);
                    spnFREQUENCY_MINUTE.EditValue = Utility.GetMinuteByHours(strArrHours[1]);
                }
            }
        }

        /// <summary>
        /// 根据血透ID，得到一条有效的长期处方数据
        /// </summary>
        /// <param name="pHemodialysisID">透析号</param>
        private void loadRecipeInfo(string pHemodialysisID)
        {
            //2014-03-05 刘超 修改将处方日期通过窗体传入，之前使用的为默认值sysdate，只传入透析号。
            //DataTable dtRecipt = objHemodialysisService.GetRecipeByHemodialysisID(pHemodialysisID);
            DataTable dtRecipt = objHemodialysisService.GetRecipeByHemodialysisIDAndDate(pHemodialysisID, CureDate);
            if (dtRecipt != null && dtRecipt.Rows.Count > 0)
            {
                if (dtRecipt != null && dtRecipt.Rows.Count > 0)
                {
                    getHourAndMinute(dtRecipt.Rows[0]["FREQUENCY_HOURS"].ToString());
                    cmbVASCULAR_ACCESS_ID.EditValue = dtRecipt.Rows[0]["VASCULAR_ACCESS_ID"].ToString();
                    lupMACHINE_TYPE.EditValue = dtRecipt.Rows[0]["FIRST_PURIFIER_MODEL"].ToString();
                    lupPURIFIER_NAME.EditValue = dtRecipt.Rows[0]["FIRST_PURIFIER_NAME"].ToString();
                    spnPURIFIER_M2.EditValue = Utility.CDecimal(dtRecipt.Rows[0]["FIRST_PURIFIER_M2"].ToString());
                    //     spnBIRCARBONATE.EditValue = Utility.CDecimal(dtRecipt.Rows[0]["BICARBONATE_RADICAL"].ToString());
                    txtRECIPE_ID.Text = dtRecipt.Rows[0]["RECIPE_ID"].ToString();
                }
            }
        }
        private void getDryWater()
        {
            //if (true)
            //{
            //    txtDRY_WATER_VALUE.Text = (Utility.CDecimal(spnBEFORE_DRY_WEIGHT.Text) - Utility.CDecimal(spnAFTER_DRY_WEIGHT.Text)).ToString();
            //}

        }
        /// <summary>
        /// 根据治疗单编号得到治疗单数据
        /// </summary>
        /// <param name="pCureID">治疗单编号</param>
        private void loadMainCureInfo(string pCureID)
        {
            this.groupBox1.Text = string.Format("{0}({1})  {2}床的治疗信息", this.ctlUserLongInfo1.Patient.NAME, this.ctlUserLongInfo1.Patient.SEX.ToString(), PatientScheduleRow.BEDNAME);
            BaseControlInfo.ClearControlText(xtraScrollableControl1);
            //foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in chkCOAGULATION_IN_DIALYSER.Items)
            //{
            //    item.CheckState = System.Windows.Forms.CheckState.Unchecked;
            //}

            _CureMainDatatable = objHemodialysisService.GetMainCureByCureID(pCureID);
            if (_CureMainDatatable != null && _CureMainDatatable.Rows.Count > 0)
            {
                BaseControlInfo.SetControlDataByDataTable(_CureMainDatatable, xtraScrollableControl1);
                spnFREQUENCY_HOURS.EditValue = _CureMainDatatable.Rows[0]["FREQUENCY_HOURS"].ToString();
                spnFREQUENCY_MINUTE.EditValue = _CureMainDatatable.Rows[0]["FREQUENCY_MINUTE"].ToString();
                lupHEPARIN_SPECIES.EditValue = _CureMainDatatable.Rows[0]["HEPARIN_SPECIES"].ToString();

                lupMACHINE_ID_TAG.EditValue = _CureMainDatatable.Rows[0]["MACHINE_ID_TAG"].ToString();
                //   spnBLOOW_FLOW.EditValue = _CureMainDatatable.Rows[0]["BLOOW_FLOW"].ToString();
                spnFILTRATION_DISPLACEMENT_LIQUID.EditValue = _CureMainDatatable.Rows[0]["FILTRATION_DISPLACEMENT_LIQUID"].ToString();
                isLoadDate = true;
                cmbPURIFICATION_MODE.EditValue = _CureMainDatatable.Rows[0]["PURIFICATION_MODE"].ToString();
                //   this.lupFOCUS_LEVEL.EditValue = _CureMainDatatable.Rows[0]["FOCUS_LEVEL"].ToString();
                //   this.lupSENSES.EditValue = _CureMainDatatable.Rows[0]["SENSES"].ToString();
                //   this.lupALLERGIC.Text = _CureMainDatatable.Rows[0]["ALLERGIC"].ToString();
                this.txtBEFORE_TEMPERATURE.Text = _CureMainDatatable.Rows[0]["BEFORE_TEMPERATURE"].ToString();
                this.txtBR.Text = _CureMainDatatable.Rows[0]["BR"].ToString();
                this.txtBEFORE_BP.Text = _CureMainDatatable.Rows[0]["BEFORE_BP"].ToString();
                this.txtAFTER_TEMPERATURE.Text = _CureMainDatatable.Rows[0]["AFTER_TEMPERATURE"].ToString();
                this.txtAFTER_BP.Text = _CureMainDatatable.Rows[0]["AFTER_BP"].ToString();
                this.txtAFTERBR.Text = _CureMainDatatable.Rows[0]["AFTERBR"].ToString();
                //   this.chkCOAGULATION_IN_DIALYSER.SelectedIndex = Convert.ToInt32(string.IsNullOrEmpty(_CureMainDatatable.Rows[0]["COAGULATION_IN_DIALYSER"].ToString()) ? "0" : _CureMainDatatable.Rows[0]["COAGULATION_IN_DIALYSER"].ToString());
                //   this.chkCOAGULATION_IN_DIALYSER.SetItemChecked(this.chkCOAGULATION_IN_DIALYSER.SelectedIndex, true);
                this.cmbVASCULAR_ACCESS_ID.EditValue = _CureMainDatatable.Rows[0]["VASCULAR_ACCESS_ID"].ToString();
                //衣物轮椅重
                spnDRY_WEIGHT_TAG.EditValue = _CureMainDatatable.Rows[0]["DRY_WEIGHT_TAG"].ToString();

                // 手动回显 RadioGroup 的值
                if (_CureMainDatatable.Rows[0]["VASCULAR_ACCESS_FIRM"] != DBNull.Value)
                    rdoVASCULAR_ACCESS_FIRM.EditValue = _CureMainDatatable.Rows[0]["VASCULAR_ACCESS_FIRM"].ToString();
                if (_CureMainDatatable.Rows[0]["VASCULAR_ACCESS_GLIDE"] != DBNull.Value)
                    rdoVASCULAR_ACCESS_GLIDE.EditValue = _CureMainDatatable.Rows[0]["VASCULAR_ACCESS_GLIDE"].ToString();
                if (_CureMainDatatable.Rows[0]["VASCULAR_ACCESS_SWELLING"] != DBNull.Value)
                    rdoVASCULAR_ACCESS_SWELLING.EditValue = _CureMainDatatable.Rows[0]["VASCULAR_ACCESS_SWELLING"].ToString();
                if (_CureMainDatatable.Rows[0]["VASCULAR_ACCESS_THROMBUS"] != DBNull.Value)
                    rdoVASCULAR_ACCESS_THROMBUS.EditValue = _CureMainDatatable.Rows[0]["VASCULAR_ACCESS_THROMBUS"].ToString();
                if (_CureMainDatatable.Rows[0]["VASCULAR_ACCESS_BLOOD"] != DBNull.Value)
                    rdoVASCULAR_ACCESS_BLOOD.EditValue = _CureMainDatatable.Rows[0]["VASCULAR_ACCESS_BLOOD"].ToString();
                if (_CureMainDatatable.Rows[0]["VASCULAR_ACCESS_ERRHYISIS"] != DBNull.Value)
                    rdoVASCULAR_ACCESS_ERRHYISIS.EditValue = _CureMainDatatable.Rows[0]["VASCULAR_ACCESS_ERRHYISIS"].ToString();
                if (_CureMainDatatable.Rows[0]["VASCULAR_ACCESS_BLOOD_INFECT"] != DBNull.Value)
                    rdoVASCULAR_ACCESS_BLOOD_INFECT.EditValue = _CureMainDatatable.Rows[0]["VASCULAR_ACCESS_BLOOD_INFECT"].ToString();
                if (_CureMainDatatable.Columns.Contains("VASCULAR_ACCESS_PENDING") && _CureMainDatatable.Rows[0]["VASCULAR_ACCESS_PENDING"] != DBNull.Value)
                    chkVASCULAR_ACCESS_PENDING.Checked = _CureMainDatatable.Rows[0]["VASCULAR_ACCESS_PENDING"].ToString() == "1";
                if (_CureMainDatatable.Columns.Contains("VASCULAR_ACCESS_PENDING_DATE") && _CureMainDatatable.Rows[0]["VASCULAR_ACCESS_PENDING_DATE"] != DBNull.Value)
                    txtVASCULAR_ACCESS_PENDING_DATE.EditValue = _CureMainDatatable.Rows[0]["VASCULAR_ACCESS_PENDING_DATE"];

                //透前体重卧床
                if (_CureMainDatatable.Rows[0]["VASCULAR_ACCESS_TYPE"].ToString().ToUpper().Equals("TRUE"))
                {
                    this.spnBEFORE_DRY_WEIGHT.Visible = false;
                    this.labelControlbefore.Visible = true;
                    labelControlbefo.Text = "透前血压";
                }
                else
                {
                    this.spnBEFORE_DRY_WEIGHT.Visible = true;
                    this.labelControlbefore.Visible = false;
                    labelControlbefo.Text = "Kg 透前血压";
                }

                //透后体重卧床
                if (_CureMainDatatable.Rows[0]["IN_BED"].ToString().Equals("1"))
                {
                    this.spnAFTER_DRY_WEIGHT.Visible = false;
                    this.labelControlAfter.Visible = true;
                    labelControlaftere.Text = "透后血压";
                }
                else
                {
                    this.spnAFTER_DRY_WEIGHT.Visible = true;
                    this.labelControlAfter.Visible = false;
                    labelControlaftere.Text = "Kg 透后血压";
                }

                if (_CureMainDatatable.Rows[0]["DOCTOR_ADVICE"].ToString().Contains("长期医嘱"))
                {
                    this.checkOrder.Checked = true;
                }
                else
                {
                    this.checkOrder.Checked = false;
                }

                if (_patientScheduleRow != null)
                {
                    if (_CureMainDatatable.Rows[0]["CLEAN_UP_TIMES"].ToString().Length <= 0)
                    {
                        spnCLEAN_UP_TIMES.Text = "1";
                    }

                    else if (!_patientScheduleRow.IsSTART_TIMENull() && !_patientScheduleRow.IsEND_TIMENull())
                    {
                        //spnCLEAN_UP_TIMES.Text = Convert.ToString(int.Parse(_CureMainDatatable.Rows[0]["CLEAN_UP_TIMES"].ToString()) + 1);
                    }
                }
                else { spnCLEAN_UP_TIMES.Text = _CureMainDatatable.Rows[0]["CLEAN_UP_TIMES"].ToString(); }

                HemodialysisModel.MED_CURE_MAIN_CRRTDataTable dtCRRTCure = objHemodialysisService.GetCRRTCureByCureIdAndBanci(pCureID, banci, banci.Equals("3") ? cureDate.Date.AddDays(1) : cureDate.Date);
                string areaName = _patientScheduleRow["AREANAME"].ToString();

                if (areaName.Equals("CRRT"))
                {
                    this.cmbPRIMARY_DOCTOR.EditValue = null;
                    this.cmbPRIMARY_NURSE.EditValue = null;
                    //  this.lupCHECK_NURSE.EditValue = null;
                    this.txtSUMMARY.Text = string.Empty;

                    if (dtCRRTCure != null && dtCRRTCure.Rows.Count > 0)
                    {
                        this.cmbPRIMARY_DOCTOR.EditValue = dtCRRTCure[0].PRIMARY_DOCTOR;
                        this.cmbPRIMARY_NURSE.EditValue = dtCRRTCure[0].PRIMARY_NURSE;
                        //      this.lupCHECK_NURSE.EditValue = dtCRRTCure[0].CHECK_NURSE;
                        this.txtSUMMARY.Text = dtCRRTCure[0].SUMMARY;
                    }
                    if (string.IsNullOrEmpty(this.txtSUMMARY.Text))
                    {
                        this.txtSUMMARY.Text = "患者于8:00开始行CRRT治疗，股静脉置管，无渗血、无滑脱。交换量ml/h，血流量ml/min，超滤率ml/h，遵医嘱给予首剂低分子肝素钙u，追加mg/h，8:00-16:00历时8小时，超滤ml。16:00-00:00历时8小时，超滤ml。00:00-08:00历时8小时，超滤ml。24小时总历时小时，总超滤ml。";
                    }
                }
                else
                {
                    this.txtSUMMARY.Text = _CureMainDatatable.Rows[0]["SUMMARY"].ToString();
                    if (_CureMainDatatable.Rows[0]["PRIMARY_NURSE"].ToString().Length == 0)
                    {
                        cmbPRIMARY_NURSE.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                    }
                    if (_CureMainDatatable.Rows[0]["CHECK_NURSE"].ToString().Length == 0)
                    {
                        //lupCHECK_NURSE.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                    }
                    if (_CureMainDatatable.Rows[0]["SUMMARY"].ToString().Length == 0)
                    {
                        this.txtSUMMARY.Text = "本次透析过程顺利，平安离室。";
                    }
                }

                string[] records = areaName.Equals("CRRT") ? (dtCRRTCure != null && dtCRRTCure.Rows.Count > 0 ? dtCRRTCure.Rows[0]["SUMMARY2"].ToString().Split("|".ToCharArray()) : null) : _CureMainDatatable.Rows[0]["SUMMARY2"].ToString().Split("|".ToCharArray());
                if (this.loginType == 0)
                {
                    txtSUMMARY2.Text = records != null && records.Length >= 1 ? records[0] : string.Empty;
                    txtSUMMARY3.Text = records != null && records.Length >= 2 ? records[1] : string.Empty;
                    txtSUMMARY4.Text = records != null && records.Length >= 3 ? records[2] : string.Empty;
                    txtSUMMARY5.Text = records != null && records.Length >= 4 ? records[3] : string.Empty;
                }
                else
                {
                    txtSUMMARY2.Text = areaName.Equals("CRRT") ? (dtCRRTCure != null && dtCRRTCure.Rows.Count > 0 ? dtCRRTCure.Rows[0]["SUMMARY3"].ToString() : string.Empty) : _CureMainDatatable.Rows[0]["SUMMARY3"].ToString();
                }

                if (_CureMainDatatable.Rows[0]["MACHINE_ID"].ToString().Length != 0)
                {
                    cmbMACHINE_ID.EditValue = _CureMainDatatable.Rows[0]["MACHINE_ID"].ToString();
                }
                else
                {
                    cmbMACHINE_ID.EditValue = _CureMainDatatable.Rows[0]["machine_name"].ToString();
                }
                if (!string.IsNullOrEmpty(_CureMainDatatable.Rows[0]["RECIPE_ID"].ToString()))
                {
                    this.txtRECIPE_ID.Text = _CureMainDatatable.Rows[0]["RECIPE_ID"].ToString();
                }
            }
            if (string.IsNullOrEmpty(this.txtRECIPE_ID.Text))
            {
                loadRecipeInfo(txtHEMODIALYSIS_ID.Text.Trim());
            }

            // 在所有控件数据加载完毕后，从UI控件拍一个数据快照作为审计日志的基准值
            // 这样保存时对比的是"用户实际看到的初始值"与"用户修改后的值"，避免数据库NULL与UI默认值的差异
            if (_CureMainDatatable != null && _CureMainDatatable.Rows.Count > 0)
            {
                DataTable snapshotDt = BaseControlInfo.GetDataTableByPanel(_CureMainDatatable, xtraScrollableControl1);
                if (snapshotDt != null && snapshotDt.Rows.Count > 0)
                {
                    // 手动补充 RadioGroup 的值（GetDataTableByPanel 可能无法获取 RadioGroup）
                    if (rdoVASCULAR_ACCESS_FIRM.EditValue != null)
                        snapshotDt.Rows[0]["VASCULAR_ACCESS_FIRM"] = rdoVASCULAR_ACCESS_FIRM.EditValue;
                    if (rdoVASCULAR_ACCESS_GLIDE.EditValue != null)
                        snapshotDt.Rows[0]["VASCULAR_ACCESS_GLIDE"] = rdoVASCULAR_ACCESS_GLIDE.EditValue;
                    if (rdoVASCULAR_ACCESS_SWELLING.EditValue != null)
                        snapshotDt.Rows[0]["VASCULAR_ACCESS_SWELLING"] = rdoVASCULAR_ACCESS_SWELLING.EditValue;
                    if (rdoVASCULAR_ACCESS_THROMBUS.EditValue != null)
                        snapshotDt.Rows[0]["VASCULAR_ACCESS_THROMBUS"] = rdoVASCULAR_ACCESS_THROMBUS.EditValue;
                    if (rdoVASCULAR_ACCESS_BLOOD.EditValue != null)
                        snapshotDt.Rows[0]["VASCULAR_ACCESS_BLOOD"] = rdoVASCULAR_ACCESS_BLOOD.EditValue;
                    if (rdoVASCULAR_ACCESS_ERRHYISIS.EditValue != null)
                        snapshotDt.Rows[0]["VASCULAR_ACCESS_ERRHYISIS"] = rdoVASCULAR_ACCESS_ERRHYISIS.EditValue;
                    if (rdoVASCULAR_ACCESS_BLOOD_INFECT.EditValue != null)
                        snapshotDt.Rows[0]["VASCULAR_ACCESS_BLOOD_INFECT"] = rdoVASCULAR_ACCESS_BLOOD_INFECT.EditValue;
                    if (rdoIN_BASKET_PLASTER_ALLERGY.EditValue != null)
                        snapshotDt.Rows[0]["IN_BASKET_PLASTER_ALLERGY"] = rdoIN_BASKET_PLASTER_ALLERGY.EditValue;
                    if (snapshotDt.Columns.Contains("VASCULAR_ACCESS_PENDING"))
                        snapshotDt.Rows[0]["VASCULAR_ACCESS_PENDING"] = chkVASCULAR_ACCESS_PENDING.Checked ? "1" : "0";
                    if (snapshotDt.Columns.Contains("VASCULAR_ACCESS_PENDING_DATE") && txtVASCULAR_ACCESS_PENDING_DATE.EditValue != null)
                        snapshotDt.Rows[0]["VASCULAR_ACCESS_PENDING_DATE"] = txtVASCULAR_ACCESS_PENDING_DATE.EditValue;
                    _initialDataSnapshot = CloneRowToDict(snapshotDt.Rows[0]);
                }
            }

            isAddDrugParmars = false;
            isLoadDate = false;
        }
        /// <summary>
        /// 设置CRRT相关控件显示方式
        /// </summary>
        /// <param name="IsVisible"></param>
        private void SetCrrtControlsVisible(bool IsVisible)
        {
            this.groupControl4.Visible = IsVisible;
            this.labelControlCureTypeMemo.Text = IsVisible ? "特殊交代事项" : "医生记录及医嘱";
            this.txtSPECIAL_MATTER.Visible = IsVisible;

            this.txtDOCTOR_ADVICE.Visible = !IsVisible;
            //checkOrder.Visible = !IsVisible;
            labelControl2.Visible = !IsVisible;
            lupPUNCTURE_NURSE.Visible = !IsVisible;
            if (!IsVisible)
            {
                if (this.groupBox1.Height == 613)
                {
                    this.groupBox1.Height = this.groupBox1.Height - 94;
                }
            }
            else
            {
                if (this.groupBox1.Height < 613)
                {
                    this.groupBox1.Height = this.groupBox1.Height + 94;
                }

            }
        }

        /// <summary>
        /// 载入下拉数据框 
        /// </summary>
        private void LoadLookUpEditList()
        {
            string type = _patientScheduleRow["AREANAME"].ToString().Equals("CRRT") ? "CRRT净化方式" : "净化方式";
            if (type.Equals("CRRT净化方式"))
            {
                SetCrrtControlsVisible(true);

            }
            else
            {
                SetCrrtControlsVisible(false);
            }

            //净化方式
            DataTable dtPurification = this._configService.GetConfigList(string.Empty, string.Empty, type, "1");
            if (dtPurification != null && dtPurification.Rows.Count > 0)
            {
                BaseControlInfo.BindLookUpEdit(cmbPURIFICATION_MODE, "ITEM_ID", "ITEM_NAME", dtPurification, "ITEM_NAME", "净化方式");
                cmbPURIFICATION_MODE.EditValue = dtPurification.Rows[0]["ITEM_ID"].ToString();
            }

            DataTable dtTempDrug = this._configService.GetConfigList(string.Empty, string.Empty, "护士转抄医嘱", "1");
            if (dtTempDrug != null && dtTempDrug.Rows.Count > 0)
            {
                BaseControlInfo.BindLookUpEdit(lupTempDrug, "ITEM_ID", "ITEM_NAME", dtTempDrug, "ITEM_NAME", "护士转抄医嘱");
                lupTempDrug.EditValue = dtTempDrug.Rows[0]["ITEM_NAME"].ToString();
            }

            DataTable dtMachine = this._configService.GetConfigList(string.Empty, string.Empty, "透析机", "1");
            if (dtMachine != null && dtMachine.Rows.Count > 0)
            {
                for (int z = 0; z < dtMachine.Rows.Count; z++)
                {
                    cmbMACHINE_ID.Properties.Items.Add(dtMachine.Rows[z]["ITEM_NAME"].ToString());
                }
            }

            //载入责任护士、责任医生下拉框数据
            DataTable dtStaffSict = _staffDictService.GetStaffDictList();
            if (dtStaffSict != null && dtStaffSict.Rows.Count > 0)
            {
                DataTable dtPunctureNurseList = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'", "name");
                if (dtPunctureNurseList != null && dtPunctureNurseList.Rows.Count > 0)
                {
                    BaseControlInfo.BindLookUpEdit(cmbPRIMARY_NURSE, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "责任护士");
                    BaseControlInfo.BindLookUpEdit(lupPUNCTURE_NURSE, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "穿刺护士");
                    //   BaseControlInfo.BindLookUpEdit(lupCHECK_NURSE, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "核对护士");
                    BaseControlInfo.BindLookUpEdit(lupNURSE_ID, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "记录护士");
                    BaseControlInfo.BindLookUpEdit(lopDRUG_NURSE_ID, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "执行护士");
                    BaseControlInfo.BindLookUpEdit(lopDISPENSINGNURSE, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "摆药护士");
                    BaseControlInfo.BindLookUpEdit(lopCHECKNURSE, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "核对护士");

                }
            }

            DataTable dtDoctorList = Utility.GetSubTable(dtStaffSict, "ZYNAME='医生'", "NAME");
            if (dtDoctorList != null && dtDoctorList.Rows.Count > 0)
            {
                BaseControlInfo.BindLookUpEdit(cmbPRIMARY_DOCTOR, "EMP_NO", "NAME", dtDoctorList, "NAME", "责任医生");
                //给药记录--开方医生
                BaseControlInfo.BindLookUpEdit(lopDOCTOR_ID, "EMP_NO", "NAME", dtDoctorList, "NAME", "开方医生");
            }

            //血管通路 
            //BaseControlInfo.BindLookUpEdit(cmbVASCULAR_ACCESS_ID, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "血管通路", "1"), "ITEM_NAME", "血管通路");
            var dtVasularAccess = _hemodialysisService.GetPatientVasular_AccessDt(txtHEMODIALYSIS_ID.Text.ToString());
            BaseControlInfo.BindLookUpEdit(this.cmbVASCULAR_ACCESS_ID, "VASCULAR_ACCESS_ID", "PATIENT_ID", dtVasularAccess, "PATIENT_ID", "血管通路");
            //膜材质
            BaseControlInfo.BindLookUpEdit(lupPURIFIER_NAME, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "透析膜", "1"), "ITEM_NAME", "透析膜");

            BaseControlInfo.BindLookUpEdit(lupIN_BASKET_WOUND_ALLERGY, "ITEM_NAME", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "灌流器", "1"), "ITEM_NAME", "灌流器");

            //净化器类型
            BaseControlInfo.BindLookUpEdit(lupMACHINE_TYPE, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "净化器类型", "1"), "ITEM_NAME", "净化器类型");
            BaseControlInfo.BindLookUpEdit(combFIRST_PURIFIER_MODEL, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "净化器类型", "1"), "ITEM_NAME", "净化器类型");
            //肝素种类 
            BaseControlInfo.BindLookUpEdit(lupHEPARIN_SPECIES, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "治疗方法", "1"), "ITEM_NAME", "治疗方法");
            //给药记录-药品单位
            BaseControlInfo.BindLookUpEdit(cmbDOSAGE_UNITS, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "药品单位", "1"), "ITEM_NAME", "药品单位");
            //给药记录-药品使用方式
            BaseControlInfo.BindLookUpEdit(cmbDRUG_MODE, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "注射方式", "1"), "ITEM_NAME", "注射方式");


            //药品名称
            txtDRUG_NAME.Properties.DataSource = objDrug.GetDrugMasterList();//绑定数据源
            txtDRUG_NAME.Properties.Columns.Add(new LookUpColumnInfo("DRUG_CODE", "编号"));   //大小写敏感
            txtDRUG_NAME.Properties.Columns.Add(new LookUpColumnInfo("DRUG_NAME", "名称"));
            txtDRUG_NAME.Properties.TextEditStyle = TextEditStyles.Standard;
            txtDRUG_NAME.Properties.DisplayMember = "DRUG_NAME";//要显示的字段,Text获得
            txtDRUG_NAME.Properties.ValueMember = "DRUG_CODE";//实际值的字段,EditValue获得 // DeptID
            txtDRUG_NAME.Properties.NullText = string.Empty;

            //   BaseControlInfo.BindLookUpEdit(lupFOCUS_LEVEL, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "病情", "1"), "ITEM_NAME", "病情");
            //   BaseControlInfo.BindLookUpEdit(lupSENSES, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "神志", "1"), "ITEM_NAME", "神志");
            //    this.lupFOCUS_LEVEL.EditValue = this._configService.GetConfigList(string.Empty, string.Empty, "病情", "1").First().ITEM_ID;
            //    this.lupSENSES.EditValue = this._configService.GetConfigList(string.Empty, string.Empty, "神志", "1").First().ITEM_ID;

            //    this.lupALLERGIC.Text = "无";

            BaseControlInfo.BindLookUpEdit(lupSUBJECTIVE_COMFORT, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "透析液品牌", "1"), "ITEM_NAME", "透析液品牌");

            BaseControlInfo.BindLookUpEdit(cmbFIRST_DRUG_UNIT, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "药品单位", "1"), "ITEM_NAME", "药品单位");
            BaseControlInfo.BindLookUpEdit(cmbSECOND_DRUG_UNIT, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "药品单位", "1"), "ITEM_NAME", "药品单位");
            //BaseControlInfo.BindLookUpEdit(this.lupDISPLACEMENT_MODE, "ITEM_ID", "ITEM_NAME", _configService.GetConfigList(string.Empty, string.Empty, "置换方式", "1"), "ITEM_NAME", "置换方式");
            //BaseControlInfo.BindLookUpEdit(this.lupDISPLACEMENT_RECIPE, "ITEM_ID", "ITEM_NAME", _configService.GetConfigList(string.Empty, string.Empty, "置换液配方", "1"), "ITEM_NAME", "置换液配方");
        }

        #endregion

        #region 数据处理

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        private bool IsDataValidate()
        {
            bool result = true;
            result = BaseControlInfo.CheckpLookUpEdit(cmbPURIFICATION_MODE, "请输入治疗方式。", "透析治疗单");
            if (result == false)
            {
                return result;
            }
            if ((string.IsNullOrEmpty(this.txtBEFORE_SYSTOLIC_PRESSURE.Text) && !string.IsNullOrEmpty(this.txtBEFORE_DIASTOLIC_PRESSURE.Text)) || (!string.IsNullOrEmpty(this.txtBEFORE_SYSTOLIC_PRESSURE.Text) && string.IsNullOrEmpty(this.txtBEFORE_DIASTOLIC_PRESSURE.Text)))
            {
                XtraMessageBox.Show("透前血压输入不正确，请核对后重新录入！", "患者治疗");
                return false;
            }
            decimal dBeforWeight = spnBEFORE_DRY_WEIGHT.Value;
            decimal dAfterWeigh = spnAFTER_DRY_WEIGHT.Value;
            if (dBeforWeight != 0 && dAfterWeigh != 0)
            {
                if (dAfterWeigh > dBeforWeight + 2)
                {
                    XtraMessageBox.Show("透后体重小于透前体重合理范围，请核对后重新录入！", "患者治疗");
                    return false;
                }
            }
            if ((string.IsNullOrEmpty(this.txtAFTER_SYSTOLIC_PRESSURE.Text) && !string.IsNullOrEmpty(this.txtAFTER_DIASTOLIC_PRESSURE.Text)) || (!string.IsNullOrEmpty(this.txtAFTER_SYSTOLIC_PRESSURE.Text) && string.IsNullOrEmpty(this.txtAFTER_DIASTOLIC_PRESSURE.Text)))
            {
                XtraMessageBox.Show("透后血压输入不正确，请核对后重新录入！", "患者治疗");
                return false;
            }
            ////钠	135~145
            //decimal dTemp = Utility.CDecimal(spnSODION.Text);
            //if (dTemp > 145 || dTemp < 135) {
            //    XtraMessageBox.Show("钠浓度的值正常范围在135~145之间。", "透析处方");
            //    spnSODION.Focus();
            //    return false;
            //}

            ////钾	0~4
            //dTemp = Utility.CDecimal(spnPOTASSIUM_ION.Text);
            //if (dTemp > 4 || dTemp < 0) {
            //    XtraMessageBox.Show("钾浓度的值正常范围在0~4之间。", "透析处方");
            //    spnPOTASSIUM_ION.Focus();
            //    return false;
            //}

            ////钙	1.25~1.75
            //dTemp = Utility.CDecimal(spnCALCIUM_ION.Text);
            //if (double.Parse(dTemp.ToString()) < 1.25 || double.Parse(dTemp.ToString()) > 1.75) {
            //    XtraMessageBox.Show("钙浓度的值正常范围在1.25~1.75之间。", "透析处方");
            //    spnCALCIUM_ION.Focus();
            //    return false;
            //}

            //葡萄糖	0~5.5
            //dTemp = Utility.CDecimal(spnAMYLACEUM.Text);
            //if (dTemp < 0 || double.Parse(dTemp.ToString()) > 5.5) {
            //    XtraMessageBox.Show("葡萄糖的值正常范围在0~5.5之间。", "透析处方");
            //    spnAMYLACEUM.Focus();
            //    return false;
            //}
            return result;
        }

        //验证给药信息 
        private bool IsDrugDataValidate()
        {
            bool result = true;
            result = BaseControlInfo.CheckTextEdit(txtCURE_ID, "请先保存处方信息，后添加给药记录", "透析治疗单");
            if (result == false)
            {
                return result;
            }
            result = BaseControlInfo.CheckpLookUpEdit(txtDRUG_NAME, "请输入药品名称", "透析治疗单");
            if (result == false)
            {
                return result;
            }
            return result;
        }

        //验证透析参数            
        private bool IsHemoParametersDataValidate()
        {
            bool result = true;
            result = BaseControlInfo.CheckTextEdit(txtCURE_ID, "请先保存处方信息，后添加透析参数记录", "透析治疗单");
            if (result == false)
            {
                return result;
            }
            result = BaseControlInfo.CheckDateEdit(cmbCREATE_DATE, "请输入录入时间", "透析治疗单");
            if (result == false)
            {
                return result;
            }
            return result;
        }

        /// <summary>
        /// 保存透析参数信息
        /// </summary>
        /// <returns></returns>
        private int SaveHemoParameters()
        {
            int result = 0;
            DataTable dt = _HemoialysisParamterDatatable;
            DataRow dr;
            DataTable dtTemp;

            // 记录修改前的旧值快照（深拷贝到字典，避免引用被后续操作覆盖）
            Dictionary<string, string> oldParamDict = null;
            string paramId = string.Empty;
            if (!(isAdd || isAddHemoParmars) && _HemoialysisParamterDatatable != null)
            {
                dr = gridView1.GetFocusedDataRow();
                if (dr != null)
                {
                    paramId = dr["HEMODIALYSIS_PARAMETERS_ID"].ToString();
                    DataTable dtOld = Utility.GetSubTable(_HemoialysisParamterDatatable as DataTable, "HEMODIALYSIS_PARAMETERS_ID = '" + paramId + "'");
                    if (dtOld != null && dtOld.Rows.Count > 0)
                    {
                        oldParamDict = CloneRowToDict(dtOld.Rows[0]);
                    }
                }
            }

            if (isAdd || isAddHemoParmars)
            {
                _HemoialysisParamterDatatable = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();
                dr = _HemoialysisParamterDatatable.NewRow();
                if (string.IsNullOrEmpty(txtCURE_ID.Text.Trim()))
                {
                    txtCURE_ID.Text = objHemodialysisService.GetNewCureID();
                }
                dr["CURE_ID"] = txtCURE_ID.Text;
                dr["RECIPE_ID"] = txtRECIPE_ID.Text;
                dr["HEMODIALYSIS_PARAMETERS_ID"] = System.Guid.NewGuid().ToString();
                dr["CRRT_CLASS"] = banci;
                _HemoialysisParamterDatatable.Rows.Add(dr);
                dtTemp = BaseControlInfo.GetDataTableByPanel(_HemoialysisParamterDatatable, panParamter);
            }
            else
            {
                dr = gridView1.GetFocusedDataRow(); //_HemoialysisParamterDatatable.Rows[0];
                dtTemp = Utility.GetSubTable(_HemoialysisParamterDatatable as DataTable, "HEMODIALYSIS_PARAMETERS_ID = '" + dr["HEMODIALYSIS_PARAMETERS_ID"].ToString() + "'");
                if (dtTemp.Rows.Count > 0)
                {
                    dtTemp = BaseControlInfo.GetDataTableByPanel(dtTemp, panParamter);
                }
            }
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                dtTemp.Rows[0]["CREATE_DATE"] = Utility.CDate(cmbCREATE_DATE.Text + " " + cmbCreate_Time.Text);
                if (chkVASCULAR_ACCESS_ERRHYISIS_1.Checked)
                {
                    dtTemp.Rows[0]["VASCULAR_ACCESS_ERRHYISIS"] = "1";
                }
                else if (chkVASCULAR_ACCESS_ERRHYISIS_0.Checked)
                {
                    dtTemp.Rows[0]["VASCULAR_ACCESS_ERRHYISIS"] = "0";
                }
                else
                {
                    dtTemp.Rows[0]["VASCULAR_ACCESS_ERRHYISIS"] = string.Empty;
                }

                if (chkVASCULAR_ACCESS_GLIDE_1.Checked)
                {
                    dtTemp.Rows[0]["VASCULAR_ACCESS_GLIDE"] = "1";
                }
                else if (chkVASCULAR_ACCESS_GLIDE_0.Checked)
                {
                    dtTemp.Rows[0]["VASCULAR_ACCESS_GLIDE"] = "0";
                }
                else
                {
                    dtTemp.Rows[0]["VASCULAR_ACCESS_GLIDE"] = string.Empty;
                }




                result = objHemodialysisService.SaveCureMainSaveHemoParameters((HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable)dtTemp);

                // 记录审计日志 - 透析参数保存
                string changeDetail = string.Empty;
                string opType = (isAdd || isAddHemoParmars) ? "SAVE" : "UPDATE";
                string logRemark = (isAdd || isAddHemoParmars) ? "新增透析参数" : "修改透析参数";
                if (opType == "SAVE")
                {
                    changeDetail = "新增透析参数";
                }
                else if (oldParamDict != null)
                {
                    changeDetail = CompareDictWithRow(oldParamDict, dtTemp.Rows[0]);
                    if (string.IsNullOrEmpty(changeDetail))
                        changeDetail = "无字段变化";
                }
                if (string.IsNullOrEmpty(paramId) && dtTemp.Rows[0]["HEMODIALYSIS_PARAMETERS_ID"] != DBNull.Value)
                {
                    paramId = dtTemp.Rows[0]["HEMODIALYSIS_PARAMETERS_ID"].ToString();
                }
                WriteOperationLog(opType, "透析参数记录", paramId, changeDetail, logRemark);
            }
            return result;
        }

        /// <summary>
        /// 保存给药信息
        /// </summary>
        /// <returns></returns>
        private int SaveDrug()
        {
            int result = 0;
            DataTable dt = _CureDrugDatatable;
            DataRow dr;
            char status = '0';

            dt = Utility.GetSubTable(_CureDrugDatatable as DataTable, "CURE_DRUG_ID='" + txtCURE_DRUG_ID.Text + "'");

            // 记录修改前的旧值快照（深拷贝到字典，避免引用被后续操作覆盖）
            Dictionary<string, string> oldDrugDict = null;
            string drugId = txtCURE_DRUG_ID.Text;
            if (dt != null && dt.Rows.Count > 0)
            {
                oldDrugDict = CloneRowToDict(dt.Rows[0]);
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                if (lopSTATUS.EditValue != null && lopSTATUS.EditValue.ToString() == "已执行")
                {
                    dt.Rows[0]["STATUS"] = '1';
                }
                else if (lopSTATUS.EditValue != null && lopSTATUS.EditValue.ToString() == "退回")
                {
                    dt.Rows[0]["STATUS"] = '2';
                }
                else
                {
                    dt.Rows[0]["STATUS"] = '0';
                }
                dt.Rows[0]["hemodialysis_id"] = this.ctlUserLongInfo1.HEMODIALYSIS_ID;
                dt.Rows[0]["patient_id"] = this.ctlUserLongInfo1.PatientID;
                dt.Rows[0]["EXEC_DATE"] = Utility.CDate(txtEXEC_DATE.Text + " " + txtEXEC_TIME.Text);
                if (string.IsNullOrEmpty(txtCURE_ID.Text.Trim()))
                {
                    txtCURE_ID.Text = objHemodialysisService.GetNewCureID();
                }
                dt.Rows[0]["DRUG_NURSE_ID"] = this.lopDRUG_NURSE_ID.EditValue.ToString();
                dt.Rows[0]["DISPENSINGNURSE"] = this.lopDISPENSINGNURSE.EditValue.ToString();
                dt.Rows[0]["CHECKNURSE"] = this.lopCHECKNURSE.EditValue.ToString();

                dt.Rows[0]["CURE_ID"] = txtCURE_ID.Text;
                dt.Rows[0]["RECIPE_ID"] = txtRECIPE_ID.Text;
                result = objHemodialysisService.SaveCureDrug((HemodialysisModel.MED_CURE_DRUGDataTable)dt);

                // 记录审计日志 - 给药信息保存
                string changeDetail = string.Empty;
                string statusText = lopSTATUS.EditValue != null ? lopSTATUS.EditValue.ToString() : "未执行";
                if (oldDrugDict != null)
                {
                    changeDetail = CompareDictWithRow(oldDrugDict, dt.Rows[0]);
                    if (string.IsNullOrEmpty(changeDetail))
                        changeDetail = "无字段变化";
                }
                WriteOperationLog("UPDATE", "给药信息", drugId, changeDetail,
                    "修改给药状态：" + statusText);

                if (lopSTATUS.EditValue != null && lopSTATUS.EditValue.ToString() == "已执行")
                {

                    var dataRow = gridView4.GetFocusedRow() as System.Data.DataRowView;
                    if (dataRow != null)
                    {
                        var dtTime = Hemo.Utilities.Utility.CDate(this.txtEXEC_DATE.Text.ToString() + " " + this.txtEXEC_TIME.Text);
                        var cureId = txtCURE_ID.Text;
                        var cureMainDt = objHemodialysisService.GetMainCureByCureID(cureId);
                        objHemodialysisService.UpdataCureDrugStateByParma("1", txtHEMODIALYSIS_ID.Text.Trim(), dataRow.Row["COM_NO"].ToString(), Hemo.Utilities.Utility.CDate(this.txtCREATE_DATE.EditValue.ToString()), cureId, cureMainDt[0].RECIPE_ID, dtTime, this.lopDRUG_NURSE_ID.EditValue.ToString());
                    }
                }
                else if (lopSTATUS.EditValue != null && lopSTATUS.EditValue.ToString() == "退回")
                {
                    var dataRow = gridView4.GetFocusedRow() as System.Data.DataRowView;

                    if (dataRow != null)
                    {
                        var dtTime = Hemo.Utilities.Utility.CDate(this.txtEXEC_DATE.Text.ToString() + this.txtEXEC_TIME.Text);

                        var cureId = txtCURE_ID.Text;
                        var cureMainDt = objHemodialysisService.GetMainCureByCureID(cureId);
                        objHemodialysisService.UpdataCureDrugStateByParma("2", txtHEMODIALYSIS_ID.Text.Trim(), dataRow.Row["COM_NO"].ToString(), dataRow.Row["COM_SUB_NO"].ToString(), Hemo.Utilities.Utility.CDate(this.txtCREATE_DATE.EditValue.ToString()), cureId, cureMainDt[0].RECIPE_ID, DateTime.Now, string.Empty);
                    }
                }


                //医嘱执行后，添加到医生记录及医嘱

                var dtCureMain = objHemodialysisService.GetMainCureByCureID(txtCURE_ID.Text);
                if (dtCureMain != null && dtCureMain.Rows.Count > 0)
                {
                    StringBuilder sbDrug = new StringBuilder();
                    //var dtComDrug = _CureDrugDatatable.Where(d => d.COM_NO.Equals(dt.Rows[0]["COM_NO"].ToString())).ToList();//不合理

                    var drugMain = objHemodialysisService.GetValidCureDrugByHemoID(addHemoId, cureDate);
                    var dtComDrug = drugMain.Where(d => d.STATUS2.Equals("已执行")).ToList();

                    foreach (HemodialysisModel.MED_CURE_DRUGRow d in dtComDrug)
                    {
                        if (!dtCureMain[0].IsDOCTOR_ADVICENull() && dtCureMain[0].DOCTOR_ADVICE.Length > 0)
                        {
                            StringBuilder sbDrugb = new StringBuilder();
                            sbDrugb.Append(d["DRUG_NAME"].ToString());
                            sbDrugb.Append(d["DOSAGE"].ToString());
                            sbDrugb.Append(d["UNIT_NAME"].ToString());
                            sbDrugb.Append(d["DRUG_MODE_NAME"].ToString());
                            sbDrugb.Append("；");
                            if (!dtCureMain[0].DOCTOR_ADVICE.Contains(sbDrugb.ToString()))
                            {
                                sbDrug.Append(sbDrugb);
                            }
                        }
                        else
                        {
                            sbDrug.Append(d["DRUG_NAME"].ToString());
                            sbDrug.Append(d["DOSAGE"].ToString());
                            sbDrug.Append(d["UNIT_NAME"].ToString());
                            sbDrug.Append(d["DRUG_MODE_NAME"].ToString());
                            sbDrug.Append("；");
                        }
                    }

                    var dtComDrug1 = drugMain.Where(d => d.STATUS2.Equals("返回")).ToList();

                    foreach (HemodialysisModel.MED_CURE_DRUGRow d in dtComDrug1)
                    {
                        if (!dtCureMain[0].IsDOCTOR_ADVICENull() && dtCureMain[0].DOCTOR_ADVICE.Length > 0)
                        {
                            StringBuilder sbDruga = new StringBuilder();

                            sbDruga.Append(d["DRUG_NAME"].ToString());
                            sbDruga.Append(d["DOSAGE"].ToString());
                            sbDruga.Append(d["UNIT_NAME"].ToString());
                            sbDruga.Append(d["DRUG_MODE_NAME"].ToString());
                            sbDruga.Append("；");
                            if (dtCureMain[0].DOCTOR_ADVICE.Contains(sbDruga.ToString()))
                            {
                                dtCureMain[0].DOCTOR_ADVICE = dtCureMain[0].DOCTOR_ADVICE.Replace(sbDruga.ToString(), string.Empty);
                            }
                        }
                    }



                    if (dtCureMain[0].IsDOCTOR_ADVICENull())
                        dtCureMain[0].DOCTOR_ADVICE = sbDrug.ToString();
                    else
                        dtCureMain[0].DOCTOR_ADVICE += sbDrug.ToString();

                    objHemodialysisService.SaveCureMain(dtCureMain);
                    this.txtDOCTOR_ADVICE.Text = dtCureMain[0].IsDOCTOR_ADVICENull() ? string.Empty : dtCureMain[0].DOCTOR_ADVICE;
                }


                //更新给药表后更新治疗参数表拼接已执行数据
                DataTable dtPamarsDrug = objHemodialysisService.GetPamarsDrugInfo(txtCURE_ID.Text, Utility.CDate(txtEXEC_DATE.Text + " " + txtEXEC_TIME.Text));

                if (dtPamarsDrug.Rows.Count <= 0) return result; //没有匹配数据，下次时进行数据匹配
                string strPamarsID = dtPamarsDrug.Rows[0]["HEMODIALYSIS_PARAMETERS_ID"].ToString();
                HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable dtParames = objHemodialysisService.GetHemoParametersByCureID(txtCURE_ID.Text);
                DataTable dtTempParams = Utility.GetSubTable((DataTable)dtParames, "HEMODIALYSIS_PARAMETERS_ID = '" + strPamarsID + "'");
            }
            return result;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public int SaveData()
        {
            int result = 0;
            DataTable dt = _CureMainDatatable;
            DataRow dr;
            // 使用页面加载时拍的UI快照作为对比基准（而非数据库原始行，避免NULL与UI默认值的差异）
            Dictionary<string, string> oldDataDict = null;
            if (!isAdd)
            {
                oldDataDict = _initialDataSnapshot;
            }
            //当治疗单编号为空时默认为新增数据 
            if (txtCURE_ID.Text.Length == 0)
            {
                isAdd = true;
            }
            if (isAdd)
            {
                if (string.IsNullOrEmpty(txtCURE_ID.Text.Trim()))
                {
                    txtCURE_ID.Text = objHemodialysisService.GetNewCureID();
                }
                _CureMainDatatable = new HemodialysisModel.MED_CURE_MAINDataTable();
                dr = _CureMainDatatable.NewRow();
                dr["CURE_ID"] = txtCURE_ID.Text;
                dr["HEMODIALYSIS_ID"] = ctlUserLongInfo1.HEMODIALYSIS_ID;
                dr["RECIPE_ID"] = txtRECIPE_ID.Text;
                dr["SUMMARY"] = txtSUMMARY.Text;
                dr["CURE_CREATE_DATE"] = Data_CureData.EditValue.ToString();
                _CureMainDatatable.Rows.Add(dr);
            }
            else
            {
                dr = _CureMainDatatable.Rows[0];
            }
            dt = BaseControlInfo.GetDataTableByPanel(_CureMainDatatable, xtraScrollableControl1);
            //手动保存 RadioGroup 的值
            dt.Rows[0]["VASCULAR_ACCESS_FIRM"] = rdoVASCULAR_ACCESS_FIRM.EditValue;
            dt.Rows[0]["VASCULAR_ACCESS_GLIDE"] = rdoVASCULAR_ACCESS_GLIDE.EditValue;
            dt.Rows[0]["VASCULAR_ACCESS_SWELLING"] = rdoVASCULAR_ACCESS_SWELLING.EditValue;
            dt.Rows[0]["VASCULAR_ACCESS_THROMBUS"] = rdoVASCULAR_ACCESS_THROMBUS.EditValue;
            dt.Rows[0]["VASCULAR_ACCESS_BLOOD"] = rdoVASCULAR_ACCESS_BLOOD.EditValue;
            dt.Rows[0]["VASCULAR_ACCESS_ERRHYISIS"] = rdoVASCULAR_ACCESS_ERRHYISIS.EditValue;
            dt.Rows[0]["VASCULAR_ACCESS_BLOOD_INFECT"] = rdoVASCULAR_ACCESS_BLOOD_INFECT.EditValue;
            dt.Rows[0]["IN_BASKET_PLASTER_ALLERGY"] = rdoIN_BASKET_PLASTER_ALLERGY.EditValue;
            if (dt.Columns.Contains("VASCULAR_ACCESS_PENDING"))
                dt.Rows[0]["VASCULAR_ACCESS_PENDING"] = chkVASCULAR_ACCESS_PENDING.Checked ? "1" : "0";
            if (dt.Columns.Contains("VASCULAR_ACCESS_PENDING_DATE"))
                dt.Rows[0]["VASCULAR_ACCESS_PENDING_DATE"] = txtVASCULAR_ACCESS_PENDING_DATE.EditValue;

            //透析小结 
            dt.Rows[0]["SUMMARY"] = txtSUMMARY.Text;
            if (this.loginType == 0)
            {
                dt.Rows[0]["SUMMARY2"] = txtSUMMARY2.Text.Trim() + "|" + txtSUMMARY3.Text.Trim() + "|" + txtSUMMARY4.Text.Trim() + "|" + txtSUMMARY5.Text.Trim();
            }
            else
            {
                dt.Rows[0]["SUMMARY3"] = txtSUMMARY2.Text.Trim() + txtSUMMARY3.Text.Trim() + txtSUMMARY4.Text.Trim() + txtSUMMARY5.Text.Trim();
            }
            // dt.Rows[0]["FOCUS_LEVEL"] = this.lupFOCUS_LEVEL.EditValue.ToString();
            // dt.Rows[0]["SENSES"] = this.lupSENSES.EditValue.ToString();
            // dt.Rows[0]["ALLERGIC"] = this.lupALLERGIC.Text.ToString();
            dt.Rows[0]["BEFORE_TEMPERATURE"] = this.txtBEFORE_TEMPERATURE.Value;
            dt.Rows[0]["BR"] = this.txtBR.Value.ToString();
            dt.Rows[0]["BEFORE_BP"] = this.txtBEFORE_BP.Value;
            dt.Rows[0]["AFTER_TEMPERATURE"] = this.txtAFTER_TEMPERATURE.Value;
            dt.Rows[0]["AFTER_BP"] = this.txtAFTER_BP.Value;
            dt.Rows[0]["AFTERBR"] = this.txtAFTERBR.Value.ToString();
            //  dt.Rows[0]["COAGULATION_IN_DIALYSER"] = this.chkCOAGULATION_IN_DIALYSER.SelectedIndex.ToString();

            DataTable dtCureCount = objHemodialysisService.GetCureCountByHemoID(ctlUserLongInfo1.HEMODIALYSIS_ID);
            if (dtCureCount != null && dtCureCount.Rows.Count > 0)
            {
                if (spnCLEAN_UP_TIMES.Text == dtCureCount.Rows[0][0].ToString() || spnCLEAN_UP_TIMES.Text == "0")
                {
                    dt.Rows[0]["CLEAN_UP_TIMES"] = dtCureCount.Rows[0][0].ToString();
                }
                else
                {
                    dt.Rows[0]["CLEAN_UP_TIMES"] = spnCLEAN_UP_TIMES.Text;
                }
            }

            dt.Rows[0]["FREQUENCY_HOURS"] = Utility.CDecimal(spnFREQUENCY_HOURS.Text);
            dt.Rows[0]["FREQUENCY_MINUTE"] = Utility.CDecimal(spnFREQUENCY_MINUTE.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                //保存非CRRT患者治疗单
                result = objHemodialysisService.SaveCureMain((HemodialysisModel.MED_CURE_MAINDataTable)dt);
                try
                {
                    //保存预计脱水
                    HemodialysisModel.MED_HEMO_RECIPEDataTable RecipeResult = objHemodialysisService.GetRecipeByRecipeID(dt.Rows[0]["RECIPE_ID"].ToString());
                    RecipeResult[0].UFR = Utility.CDecimal(dt.Rows[0]["UFR"].ToString());

                    objHemodialysisService.SaveRecipe(RecipeResult);
                }
                catch (Exception e)
                {
                }

                if (_patientScheduleRow["AREANAME"].ToString().Equals("CRRT"))
                {
                    //保存CRRT患者治疗单
                    string cureId = dt.Rows[0]["CURE_ID"].ToString();
                    HemodialysisModel.MED_CURE_MAIN_CRRTDataTable dtCRRTCure = objHemodialysisService.GetCRRTCureByCureIdAndBanci(cureId, banci, banci.Equals("3") ? cureDate.Date.AddDays(1) : cureDate.Date);
                    if (dtCRRTCure != null && dtCRRTCure.Rows.Count > 0)
                    {
                        //编辑
                        dtCRRTCure[0].PRIMARY_DOCTOR = this.cmbPRIMARY_DOCTOR.EditValue.ToString();
                        dtCRRTCure[0].PRIMARY_NURSE = this.cmbPRIMARY_NURSE.EditValue.ToString();
                        //     dtCRRTCure[0].CHECK_NURSE = this.lupCHECK_NURSE.EditValue.ToString();
                        dtCRRTCure[0].SUMMARY = this.txtSUMMARY.Text;
                        if (this.loginType == 0)
                        {
                            dtCRRTCure[0].SUMMARY2 = txtSUMMARY2.Text.Trim() + "|" + txtSUMMARY3.Text.Trim() + "|" + txtSUMMARY4.Text.Trim() + "|" + txtSUMMARY5.Text.Trim();
                        }
                        else
                        {
                            dtCRRTCure[0].SUMMARY3 = txtSUMMARY2.Text.Trim() + txtSUMMARY3.Text.Trim() + txtSUMMARY4.Text.Trim() + txtSUMMARY5.Text.Trim();
                        }
                    }
                    else
                    {
                        //新增
                        dtCRRTCure = new HemodialysisModel.MED_CURE_MAIN_CRRTDataTable();
                        var row = dtCRRTCure.NewMED_CURE_MAIN_CRRTRow();
                        row.ID = Guid.NewGuid().ToString();
                        row.CURE_ID = cureId;
                        row.RECIPE_ID = dt.Rows[0]["RECIPE_ID"].ToString();
                        row.HEMODIALYSIS_ID = dt.Rows[0]["HEMODIALYSIS_ID"].ToString();
                        row.CRRT_CLASS = banci;
                        row.PRIMARY_DOCTOR = this.cmbPRIMARY_DOCTOR.EditValue.ToString();
                        row.PRIMARY_NURSE = this.cmbPRIMARY_NURSE.EditValue.ToString();
                        //    row.CHECK_NURSE = this.lupCHECK_NURSE.EditValue.ToString();
                        row.SUMMARY = this.txtSUMMARY.Text;
                        //row.CREATE_DATE = DateTime.Now;
                        row.CREATE_DATE = banci.Equals("3") ? cureDate.Date.AddDays(1).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second) : cureDate.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
                        if (this.loginType == 0)
                        {
                            row.SUMMARY2 = txtSUMMARY2.Text.Trim() + "|" + txtSUMMARY3.Text.Trim() + "|" + txtSUMMARY4.Text.Trim() + "|" + txtSUMMARY5.Text.Trim();
                        }
                        else
                        {
                            row.SUMMARY3 = txtSUMMARY2.Text.Trim() + txtSUMMARY3.Text.Trim() + txtSUMMARY4.Text.Trim() + txtSUMMARY5.Text.Trim();
                        }
                        dtCRRTCure.AddMED_CURE_MAIN_CRRTRow(row);
                    }
                    result = objHemodialysisService.SaveCRRTCureMain(dtCRRTCure);

                    // 记录审计日志 - CRRT治疗单保存（只记录关键字段）
                    string crrtInfo = string.Empty;
                    if (dtCRRTCure.Rows.Count > 0)
                    {
                        DataRow crrtRow = dtCRRTCure.Rows[0];
                        StringBuilder sb = new StringBuilder();
                        string[] crrtFields = { "CURE_ID", "RECIPE_ID", "HEMODIALYSIS_ID", "CRRT_CLASS",
                            "PRIMARY_DOCTOR", "PRIMARY_NURSE", "SUMMARY", "CREATE_DATE" };
                        foreach (string field in crrtFields)
                        {
                            if (crrtRow.Table.Columns.Contains(field))
                            {
                                string val = crrtRow[field] == DBNull.Value ? "" : crrtRow[field].ToString().Trim();
                                if (!string.IsNullOrEmpty(val))
                                {
                                    if (sb.Length > 0) sb.Append("; ");
                                    sb.AppendFormat("{0}: {1}", field, val);
                                }
                            }
                        }
                        crrtInfo = sb.ToString();
                    }
                    WriteOperationLog("UPDATE", "CRRT治疗单", cureId, crrtInfo, "保存CRRT治疗单");
                }
            }

            //保存病人类型
            PatientModel.MED_PATIENTSDataTable _patientDataTable;
            _patientDataTable = objPatient.GetPatientListByPatientID(ctlUserLongInfo1.PatientID);
            _patientDataTable.Rows[0]["TIME_TYPE"] = ctlUserLongInfo1.PatientType;
            result = objPatient.SavePatientInfo(_patientDataTable);

            if (this.complicatinTable != null && this.complicatinTable.Rows.Count > 0)
            {
                bool isAddComp = false;
                foreach (DataColumn column in this.complicatinTable.Columns)
                {
                    if (column.ColumnName == "ID" || column.ColumnName == "NURSE_ID" || column.ColumnName == "MACHINE_ID" || column.ColumnName == "FIRST_PURIFIER_MODEL" || column.ColumnName == "HEMODIALYSIS_ID" || column.ColumnName == "CURE_ID" || column.ColumnName == "WORK_DATE")
                        continue;
                    if (this.complicatinTable.Rows[0][column] != DBNull.Value && this.complicatinTable.Rows[0][column] != null && this.complicatinTable.Rows[0][column].ToString().Length > 0)
                        isAddComp = true;
                }
                //保存并发症
                if (isAddComp)
                {
                    result = objHemodialysisService.SaveComplication(this.complicatinTable);
                    // 记录审计日志 - 并发症保存（只记录非空字段）
                    string compValue = string.Empty;
                    if (this.complicatinTable.Rows.Count > 0)
                    {
                        DataRow compRow = this.complicatinTable.Rows[0];
                        StringBuilder sb = new StringBuilder();
                        foreach (DataColumn col in compRow.Table.Columns)
                        {
                            if (col.ColumnName == "ID" || col.ColumnName == "NURSE_ID" || col.ColumnName == "MACHINE_ID"
                                || col.ColumnName == "FIRST_PURIFIER_MODEL" || col.ColumnName == "HEMODIALYSIS_ID"
                                || col.ColumnName == "CURE_ID" || col.ColumnName == "WORK_DATE")
                                continue;
                            string val = compRow[col] == DBNull.Value ? "" : compRow[col].ToString().Trim();
                            if (!string.IsNullOrEmpty(val))
                            {
                                if (sb.Length > 0) sb.Append("; ");
                                sb.AppendFormat("{0}: {1}", col.ColumnName, val);
                            }
                        }
                        compValue = sb.ToString();
                    }
                    WriteOperationLog("UPDATE", "并发症信息", GetCurrentCureId(), compValue, "保存并发症信息");
                }
            }

            // 记录审计日志 - 治疗单保存
            string changeDetail = string.Empty;
            if (isAdd)
            {
                // 新增时只记录关键字段
                changeDetail = "新增治疗单";
            }
            else if (oldDataDict != null && dt != null && dt.Rows.Count > 0)
            {
                // 修改时用字典快照与新行对比，只记录变化的字段
                changeDetail = CompareDictWithRow(oldDataDict, dt.Rows[0]);
                if (string.IsNullOrEmpty(changeDetail))
                    changeDetail = "无字段变化";
            }
            string operationType = isAdd ? "SAVE" : "UPDATE";
            string logRemark = isAdd ? "新增治疗单" : "修改治疗单";
            WriteOperationLog(operationType, "治疗单主表", GetCurrentCureId(), changeDetail, logRemark);

            if (dt != null && dt.Rows.Count > 0)
            {
                // 手动补充 RadioGroup 的值到快照
                if (rdoVASCULAR_ACCESS_FIRM.EditValue != null)
                    dt.Rows[0]["VASCULAR_ACCESS_FIRM"] = rdoVASCULAR_ACCESS_FIRM.EditValue;
                if (rdoVASCULAR_ACCESS_GLIDE.EditValue != null)
                    dt.Rows[0]["VASCULAR_ACCESS_GLIDE"] = rdoVASCULAR_ACCESS_GLIDE.EditValue;
                if (rdoVASCULAR_ACCESS_SWELLING.EditValue != null)
                    dt.Rows[0]["VASCULAR_ACCESS_SWELLING"] = rdoVASCULAR_ACCESS_SWELLING.EditValue;
                if (rdoVASCULAR_ACCESS_THROMBUS.EditValue != null)
                    dt.Rows[0]["VASCULAR_ACCESS_THROMBUS"] = rdoVASCULAR_ACCESS_THROMBUS.EditValue;
                if (rdoVASCULAR_ACCESS_BLOOD.EditValue != null)
                    dt.Rows[0]["VASCULAR_ACCESS_BLOOD"] = rdoVASCULAR_ACCESS_BLOOD.EditValue;
                if (rdoVASCULAR_ACCESS_ERRHYISIS.EditValue != null)
                    dt.Rows[0]["VASCULAR_ACCESS_ERRHYISIS"] = rdoVASCULAR_ACCESS_ERRHYISIS.EditValue;
                if (rdoVASCULAR_ACCESS_BLOOD_INFECT.EditValue != null)
                    dt.Rows[0]["VASCULAR_ACCESS_BLOOD_INFECT"] = rdoVASCULAR_ACCESS_BLOOD_INFECT.EditValue;
                if (rdoIN_BASKET_PLASTER_ALLERGY.EditValue != null)
                    dt.Rows[0]["IN_BASKET_PLASTER_ALLERGY"] = rdoIN_BASKET_PLASTER_ALLERGY.EditValue;
                if (dt.Columns.Contains("VASCULAR_ACCESS_PENDING"))
                    dt.Rows[0]["VASCULAR_ACCESS_PENDING"] = chkVASCULAR_ACCESS_PENDING.Checked ? "1" : "0";
                if (dt.Columns.Contains("VASCULAR_ACCESS_PENDING_DATE") && txtVASCULAR_ACCESS_PENDING_DATE.EditValue != null)
                    dt.Rows[0]["VASCULAR_ACCESS_PENDING_DATE"] = txtVASCULAR_ACCESS_PENDING_DATE.EditValue;
                _initialDataSnapshot = CloneRowToDict(dt.Rows[0]);
            }
            return result;
        }

        /// <summary>
        /// 设置药品录入控件是否可用属性
        /// </summary>
        /// <param name="pEnabled">控件状态</param>
        private void setDrugEnabled(bool pEnabled)
        {
            BaseControlInfo.SetControlEnabled(panDrug, pEnabled);
        }

        /// <summary>
        /// 设置透析参数录入控件是否可用
        /// </summary>
        /// <param name="pEnabled">控件状态</param>
        private void setParamterEnabled(bool pEnabled)
        {
            BaseControlInfo.SetControlEnabled(panParamter, pEnabled);
        }

        private string ConvertToString(object o)
        {
            if (o == null)
                return string.Empty;
            if (o == DBNull.Value || o is DBNull)
                return string.Empty;
            return o.ToString();
        }


        /// <summary>
        /// 患者卡片双击
        /// </summary>
        /// <param name="banci"></param>
        //public void Double_Click(string banci)
        //{
        //    ChangeCureAndParam(banci);
        //}

        /// <summary>
        /// 改变治疗单和透析参数
        /// </summary>
        /// <param name="banci"></param>
        //private void ChangeCureAndParam(string banci)
        //{
        //    //改变责任医生、责任护士、核对护士签名，改变透析小结
        //    HemodialysisModel.MED_CURE_MAIN_CRRTDataTable dtCRRTCure = objHemodialysisService.GetCRRTCureByCureIdAndBanci(this.addPcureId, banci);
        //    if (dtCRRTCure != null && dtCRRTCure.Rows.Count > 0)
        //    {
        //        this.cmbPRIMARY_DOCTOR.EditValue = dtCRRTCure[0].PRIMARY_DOCTOR;
        //        this.cmbPRIMARY_NURSE.EditValue = dtCRRTCure[0].PRIMARY_NURSE;
        //        this.lupCHECK_NURSE.EditValue = dtCRRTCure[0].CHECK_NURSE;
        //        this.txtSUMMARY.Text = dtCRRTCure[0].SUMMARY;
        //    }
        //    //改变透析参数列表数据
        //    if (this.tabControls.SelectedTabPageIndex == 2)
        //    {
        //        var dtResult = _HemoialysisParamterDatatable.Clone();
        //        _HemoialysisParamterDatatable.Where(row => row.CRRT_CLASS.Equals(banci)).CopyToDataTable(dtResult, LoadOption.OverwriteChanges);
        //        this.gridControl1.DataSource = dtResult;
        //    }
        //}

        #endregion

        #region 事件

        /// <summary>
        /// 遍历透析参数列表Grid并填充给对应的DataTable后保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnAFTER_DRY_WEIGHT_EditValueChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 保存治疗信息、透析信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tabControls.SelectedTabPageIndex != 2)
            {
                if (this.txtSUMMARY2.Text.Trim().Length > 1400)
                {
                    XtraMessageBox.Show("抢救记录长度不能超过1400个字符!", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    return;
                }

                bool checkResult = true;
                //CRRT患者检查是否提前保存治疗单
                if (_patientScheduleRow["AREANAME"].ToString().Equals("CRRT"))
                {
                    if (banci.Equals("1"))
                    {
                        if (XtraMessageBox.Show("确定保存为白天班次血透治疗单吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;
                        if (cureDate.Date.CompareTo(DateTime.Now.Date) == 0)
                        {
                            if (DateTime.Now.Hour < 8)
                            {
                                checkResult = false;
                            }
                        }
                        else if (cureDate.Date.CompareTo(DateTime.Now.Date) > 0)
                        {
                            checkResult = false;
                        }
                    }
                    else if (banci.Equals("2"))
                    {
                        if (XtraMessageBox.Show("确定保存为小夜班次血透治疗单吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;
                        if (cureDate.Date.CompareTo(DateTime.Now.Date) == 0)
                        {
                            if (DateTime.Now.Hour < 16)
                            {
                                checkResult = false;
                            }
                        }
                        else if (cureDate.Date.CompareTo(DateTime.Now.Date) > 0)
                        {
                            checkResult = false;
                        }
                    }
                    else if (banci.Equals("3"))
                    {
                        if (XtraMessageBox.Show("确定保存为大夜班次血透治疗单吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;
                        if (cureDate.Date.CompareTo(DateTime.Now.Date) >= 0)
                        {
                            checkResult = false;
                        }
                    }

                    if (checkResult == false)
                    {
                        AutoClosedMsgBox.ShowForm("不允许提前操作CRRT患者血透治疗单！", "患者治疗", 1000, MessageBoxIcon.Warning);
                        return;
                    }

                    if (cmbPRIMARY_DOCTOR.EditValue == null)
                    {
                        AutoClosedMsgBox.ShowForm("责任医生不能为空！", "患者治疗", 1000, MessageBoxIcon.Warning);
                        return;
                    }
                    if (cmbPRIMARY_NURSE.EditValue == null)
                    {
                        AutoClosedMsgBox.ShowForm("责任护士不能为空！", "患者治疗", 1000, MessageBoxIcon.Warning);
                        return;
                    }
                    //if (lupCHECK_NURSE.EditValue == null)
                    //{
                    //    AutoClosedMsgBox.ShowForm("核对护士不能为空！", "患者治疗", 1000, MessageBoxIcon.Warning);
                    //    return;
                    //}
                }

                //逻辑校验
                checkResult = IsDataValidate();
                if (checkResult == false)
                {
                    return;
                }

                if (!isAddDrugParmars)
                {
                    //if (txtCREATE_DATE.EditValue != null)
                    //{
                    //    SaveDrug();
                    //}
                }

                SaveData();
            }
            else
            {
                if (cmbCREATE_DATE.EditValue != null)
                {
                    if (_patientScheduleRow["AREANAME"].ToString().Equals("CRRT"))
                    {
                        string cureId = _CureMainDatatable[0].CURE_ID;
                        var dtCRRTCure = objHemodialysisService.GetCRRTCureByCureIdAndBanci(cureId, banci, banci.Equals("3") ? cureDate.Date.AddDays(1) : cureDate.Date);
                        if (dtCRRTCure == null || dtCRRTCure.Rows.Count == 0)
                        {
                            XtraMessageBox.Show("该班次对应治疗单不存在，请先保存治疗信息！", this.Text);
                            return;
                        }

                        if (banci.Equals("1"))
                        {
                            if (XtraMessageBox.Show("确定保存为白天班次透析参数记录吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                return;
                        }
                        else if (banci.Equals("2"))
                        {
                            if (XtraMessageBox.Show("确定保存为小夜班次透析参数记录吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                return;
                        }
                        else if (banci.Equals("3"))
                        {
                            if (XtraMessageBox.Show("确定保存为大夜班次透析参数记录吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                return;
                        }
                    }

                    var dtConfig = _configService.GetConfigList(string.Empty, string.Empty, "质控校验", "1");
                    DateTime start = Utility.CDate(this.cmbCREATE_DATE.DateTime.ToShortDateString() + " " + this.cmbCreate_Time.Text);
                    if (!Utility.CheckRecordTimeIsValid("透析参数记录", start, start, dtConfig))
                    {
                        XtraMessageBox.Show("透析参数不允许提前录入！", this.Text);
                        return;
                    }

                    if (_patientScheduleRow["AREANAME"].ToString().Equals("CRRT"))
                    {
                        //检查班次和时间段是否符合
                        bool checkResult = true;
                        DateTime time1 = Convert.ToDateTime("16:00");
                        DateTime time2 = Convert.ToDateTime("23:59");
                        DateTime time3 = Convert.ToDateTime("0:00");
                        DateTime time4 = Convert.ToDateTime("8:00");
                        //var date = banci.Equals("3") ? cureDate.Date.AddDays(1) : cureDate.Date;

                        if (banci.Equals("1"))
                        {
                            if (this.cmbCREATE_DATE.DateTime.Date.CompareTo(cureDate.Date) == 0)
                            {
                                if ((this.cmbCreate_Time.Time.CompareTo(time1) > 0 && this.cmbCreate_Time.Time.CompareTo(time2) < 0) || (this.cmbCreate_Time.Time.CompareTo(time3) >= 0 && this.cmbCreate_Time.Time.CompareTo(time4) < 0))
                                {
                                    checkResult = false;
                                }
                            }
                            else
                            {
                                checkResult = false;
                            }
                        }
                        else if (banci.Equals("2"))
                        {
                            if (this.cmbCREATE_DATE.DateTime.Date.CompareTo(cureDate.Date) == 0)
                            {
                                if ((this.cmbCreate_Time.Time.CompareTo(time4) >= 0 && this.cmbCreate_Time.Time.CompareTo(time1) < 0) || (this.cmbCreate_Time.Time.CompareTo(time3) > 0 && this.cmbCreate_Time.Time.CompareTo(time4) <= 0))
                                {
                                    checkResult = false;
                                }
                            }
                            else
                            {
                                checkResult = false;
                            }
                        }
                        else if (banci.Equals("3"))
                        {
                            if (this.cmbCREATE_DATE.DateTime.Date.CompareTo(cureDate.Date.AddDays(1)) == 0)
                            {
                                if ((this.cmbCreate_Time.Time.CompareTo(time4) > 0 && this.cmbCreate_Time.Time.CompareTo(time1) <= 0) || (this.cmbCreate_Time.Time.CompareTo(time1) >= 0 && this.cmbCreate_Time.Time.CompareTo(time2) < 0))
                                {
                                    checkResult = false;
                                }
                            }
                            else
                            {
                                checkResult = false;
                            }
                        }

                        if (!checkResult)
                        {
                            XtraMessageBox.Show("记录时间与签到日期、班次不符合，请检查！", this.Text);
                            return;
                        }
                    }

                    SaveHemoParameters();
                }
            }

            if (!this.panel_Top.Visible)
            {
                AutoClosedMsgBox.ShowForm("数据保存成功!", "患者治疗", 1000, MessageBoxIcon.Warning);
            }
            loadParaneterGrid(txtCURE_ID.Text, txtHEMODIALYSIS_ID.Text.Trim());
            setParamterEnabled(false);
            btnEdit.Text = "编辑";

            #region //以前的保存保留

            ////给药信息
            //if (tabIndex == 1)
            //{
            //    if (IsDrugDataValidate())
            //    {
            //        if (SaveDrug() > 0)
            //        {
            //            XtraMessageBox.Show("给药信息，保存成功。", "治疗信息");
            //            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            //            loadGrid(txtCURE_ID.Text);
            //        }
            //    }
            //}
            ////透析参数
            //else if (tabIndex == 2)
            //{
            //    if (IsHemoParametersDataValidate())
            //    {
            //        if (XtraMessageBox.Show("确定保存当前血透治疗单信息吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //            return;

            //        if (SaveHemoParameters() > 0)
            //        {
            //            XtraMessageBox.Show("透析参数，保存成功。", "治疗信息");
            //            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            //            loadGrid(txtCURE_ID.Text);
            //            setParamterEnabled(false);
            //            btnEdit.Text = "编辑";
            //        }
            //    }
            //}
            ////处方治疗单主信息与透析小结
            //else if (tabIndex == 0)
            //{

            //    if (IsDataValidate())
            //    {
            //        if (XtraMessageBox.Show("确定保存当前血透治疗单信息吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //            return;

            //        if (SaveData() > 0)
            //        {
            //            XtraMessageBox.Show("保存成功。", "治疗信息");
            //            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            //        }
            //    }
            //}
            //else if (tabIndex == 3 || tabIndex == 4)
            //{

            //    if (SaveData() > 0)
            //    {
            //        XtraMessageBox.Show("保存成功。", "治疗信息");
            //        this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            //    }
            //}

            #endregion
        }

        /// <summary>
        /// 添加一行新的透析参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataRow dr = _HemoialysisParamterDatatable.NewRow();
            dr["HEMODIALYSIS_PARAMETERS_ID"] = Guid.NewGuid().ToString();
            dr["CURE_ID"] = txtCURE_ID.Text;
            dr["RECIPE_ID"] = txtRECIPE_ID.Text;
            _HemoialysisParamterDatatable.Rows.Add(dr);
            gridControl1.DataSource = _HemoialysisParamterDatatable;
        }

        /// <summary>
        /// 添加一行新的给药记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow dr = _CureDrugDatatable.NewRow();
            dr["CURE_DRUG_ID"] = Guid.NewGuid().ToString();
            dr["CURE_ID"] = txtCURE_ID.Text;
            dr["RECIPE_ID"] = txtRECIPE_ID.Text;
            _CureDrugDatatable.Rows.Add(dr);
            gridControl4.DataSource = _CureDrugDatatable;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
        }

        private void cmbDRUG_MODE_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void tabControls_Selected(object sender, DevExpress.XtraTab.TabPageEventArgs e)
        {
        }

        private void tabControls_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            tabIndex = tabControls.SelectedTabPageIndex;
            if (tabIndex == 0 || tabIndex == 1 || tabIndex == 3)
            {
                btnAdd.Visible = false;
                btnEdit.Visible = false;
                btnShowSummary.Visible = false;
                if (tabIndex == 0)
                {
                    loadMainCureInfo(this.txtCURE_ID.Text.Trim());
                }
                if (tabIndex == 1)
                {
                    this.btnSave.Visible = false;
                    //刘超 2018-11-29  修复开始治疗时药品不刷新的问题
                    loadDrugGrid(txtCURE_ID.Text, addHemoId);
                }
                if (tabIndex == 3)
                {
                    txtSUMMARY.Focus();
                    btnShowSummary.Visible = true;
                }
            }
            else if (tabIndex == 2)
            {
                btnAdd.Visible = true;
                btnEdit.Visible = true;
                btnShowSummary.Visible = false;
                loadParaneterGrid(txtCURE_ID.Text, addHemoId);
            }
            if (this.tabControls.SelectedTabPageIndex != 1)
            {
                this.btn_exect.Visible = false;
                this.btn_Back.Visible = false;
                this.btnSave.Visible = true;
            }
            if (this.tabControls.SelectedTabPageIndex == 2)
            {
                this.btnCopy.Visible = true;
                //this.btnGatherData.Visible = true;
                btnDelete.Visible = true;
            }
            else
            {
                this.btnCopy.Visible = false;
                this.btnGatherData.Visible = false;
                btnDelete.Visible = false;
            }

            if (this.tabControls.SelectedTabPageIndex == 5)
            {
                LoadComplication();
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (tabControls.SelectedTabPageIndex == 1)
            {
                BaseControlInfo.ClearControlText(panDrug);
                setDrugEnabled(true);
                cmbCreate_Time.Focus();
                isAddDrugParmars = true;
            }
            else if (tabControls.SelectedTabPageIndex == 2)
            {
                BaseControlInfo.ClearControlText(panParamter);
                setDefaultValue(cmbVASCULAR_ACCESS_ID.EditValue.ToString());
                setParamterEnabled(true);
                cmbCREATE_DATE.Focus();
                cmbCREATE_DATE.EditValue = System.DateTime.Now.ToShortDateString();
                cmbCreate_Time.EditValue = System.DateTime.Now.ToShortTimeString();
                isAddHemoParmars = true;
                lupNURSE_ID.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                cmbCreate_Time.Focus();
                this.spnCONDUCTIVITY.EditValue = 14;
                this.spnDIALYSATE_RATE.EditValue = 500;
                this.txtCLINICAL_MANIFESTATION.EditValue = "无特殊";
                if (_HemoialysisParamterDatatable.Rows.Count > 0)
                    this.spnANTICOAGULANT.EditValue = 0;
                else if (this.spnFIRST_HEPARIN.Value != 0)
                {
                    this.spnANTICOAGULANT.EditValue = this.spnFIRST_HEPARIN.Value;
                }
                var QzValue = this.spnFREQUENCY_HOURS.Value + Utility.CDecimal(this.spnFREQUENCY_MINUTE.EditValue.ToString()) / 60;
                this.spnURF.EditValue = Math.Floor((this.spnUFR.Value * 1000 + 300) / QzValue);
            }
        }

        /// <summary>
        /// 给药记录单击行赋值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView4_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            btnEdit.Enabled = true;
            var dr = gridView4.GetFocusedDataRow() as HemodialysisModel.MED_CURE_DRUGRow;

            if (dr != null)
            {
                HemodialysisModel.MED_CURE_DRUGDataTable tmpDrugTable = new HemodialysisModel.MED_CURE_DRUGDataTable();
                tmpDrugTable.Rows.Add(dr.ItemArray);
                BaseControlInfo.SetControlDataByDataTable(tmpDrugTable, panDrug);
                setDrugEnabled(false);
                if (tmpDrugTable.Rows[0]["STATUS"].ToString() == "1")
                {
                    this.btn_exect.Visible = false;
                    this.btn_Back.Visible = true;
                    lopSTATUS.EditValue = "已执行";
                    this.btn_exect.Visible = false;
                    this.btn_Back.Visible = true;
                    txtEXEC_TIME.Time = Utility.CDate(tmpDrugTable.Rows[0]["EXEC_DATE"].ToString());
                    this.txtEXEC_DATE.EditValue = Utility.CDate(tmpDrugTable.Rows[0]["EXEC_DATE"].ToString());
                    this.txtEXEC_DATE.Enabled = false;
                    this.txtEXEC_TIME.Enabled = false;

                }
                else if (tmpDrugTable.Rows[0]["STATUS"].ToString() == "0" || tmpDrugTable.Rows[0]["STATUS"].ToString() == "2")
                {
                    this.btn_exect.Visible = true;
                    this.btn_Back.Visible = false;
                    lopSTATUS.EditValue = "未执行";
                    this.btn_exect.Visible = true;
                    this.btn_Back.Visible = false;
                    this.txtEXEC_DATE.EditValue = System.DateTime.Now.Date;
                    this.txtEXEC_TIME.EditValue = System.DateTime.Now;
                    this.txtEXEC_DATE.Enabled = true;
                    this.txtEXEC_TIME.Enabled = true;
                }
                else if (tmpDrugTable.Rows[0]["STATUS"].ToString() == "2")
                {
                    this.btn_exect.Visible = true;
                    this.btn_Back.Visible = false;
                    lopSTATUS.EditValue = "退回";
                    this.btn_exect.Visible = true;
                    this.btn_Back.Visible = false;
                    this.txtEXEC_DATE.EditValue = System.DateTime.Now.Date;
                    this.txtEXEC_TIME.EditValue = System.DateTime.Now;
                    this.txtEXEC_DATE.Enabled = true;
                    this.txtEXEC_TIME.Enabled = true;
                }

                lupDRUG_TIMETYPE.EditValue = tmpDrugTable.Rows[0]["DRUG_TIMETYPE"].ToString();
                this.txtDRUG_NAME.EditValue = dr.DRUG_CODE.ToString().Trim();

                try
                {
                    this.lopDRUG_NURSE_ID.EditValue = !string.IsNullOrEmpty(dr["DRUG_NURSE_ID"].ToString()) ? dr.DRUG_NURSE_ID.ToString() : HemoApplicationContext.Current.CurrentUser.EMP_NO; ;
                    this.lopDISPENSINGNURSE.EditValue = dr.DISPENSINGNURSE.ToString();
                    this.lopCHECKNURSE.EditValue = dr.CHECKNURSE.ToString();
                }
                catch { }

                this.lopDRUG_NURSE_ID.Enabled = true;
                this.lopDISPENSINGNURSE.Enabled = true;
                this.lopCHECKNURSE.Enabled = true;
            }



        }

        /// <summary>
        /// 透析参数单击行赋值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            btnEdit.Enabled = true;
            //根据选中行赋值
            DataRow dr = gridView1.GetFocusedDataRow();
            if (dr != null)
            {
                HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable tmpParamTable = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();
                tmpParamTable.Rows.Add(dr.ItemArray);
                txtHEMODIALYSIS_PARAMETERS_ID.Text = dr["HEMODIALYSIS_PARAMETERS_ID"].ToString();
                BaseControlInfo.SetControlDataByDataTable(tmpParamTable, panParamter);

                if (dr["VASCULAR_ACCESS_ERRHYISIS"].ToString() == "1")
                {
                    chkVASCULAR_ACCESS_ERRHYISIS_1.Checked = true;
                }
                else if (dr["VASCULAR_ACCESS_ERRHYISIS"].ToString() == "0")
                {
                    chkVASCULAR_ACCESS_ERRHYISIS_0.Checked = true;
                }

                if (dr["VASCULAR_ACCESS_GLIDE"].ToString() == "1")
                {
                    chkVASCULAR_ACCESS_GLIDE_1.Checked = true;
                }
                else if (dr["VASCULAR_ACCESS_GLIDE"].ToString() == "0")
                {
                    chkVASCULAR_ACCESS_GLIDE_0.Checked = true;
                }

                cmbCreate_Time.Time = Utility.CDate(dr["CREATE_DATE"].ToString());
            }
        }

        /// <summary>
        /// 点击编辑按钮，控制控件录入状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            //透析参数按钮状态控制 1=给药信息 2=透析参数
            if (tabIndex == 1 || tabIndex == 2)
            {
                if (btnEdit.Text == "取消")
                {
                    if (tabIndex == 2)
                    {
                        setParamterEnabled(false);
                        BaseControlInfo.ClearControlText(panParamter);
                    }
                    else if (tabIndex == 1)
                    {
                        setDrugEnabled(false);
                        BaseControlInfo.ClearControlText(panDrug);
                    }
                    btnEdit.Text = "编辑";
                }
                else
                {
                    if (tabIndex == 2)
                    {
                        setParamterEnabled(true);
                        cmbCREATE_DATE.Focus();
                    }
                    else if (tabIndex == 1)
                    {
                        setDrugEnabled(true);
                        cbmDRUG_DAYS.Enabled = spnDRUG_TIMES.Enabled = lupDRUG_TIMETYPE.Enabled = false;
                        lopDOCTOR_ID.Enabled = cmbDRUG_MODE.Enabled = cmbDOSAGE_UNITS.Enabled = false;
                        txtDOSAGE.Enabled = txtDRUG_NAME.Enabled = txtCREATE_DATE.Enabled = false;
                        txtEXEC_DATE.EditValue = System.DateTime.Now.ToShortDateString();
                        txtEXEC_TIME.EditValue = System.DateTime.Now.ToShortTimeString();
                        lopSTATUS.Focus();
                    }
                    btnEdit.Text = "取消(&E)";
                    isAddDrugParmars = false;
                    isAddHemoParmars = false;
                }
            }
        }

        private void spnUFR_Leave(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 复制参数记录内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (tabControls.SelectedTabPageIndex == 2)
            {
                isAddHemoParmars = true;
                BaseControlInfo.ClearControlText(panParamter);
                setParamterEnabled(true);
                DataRow dr = gridView1.GetFocusedDataRow();
                if (dr != null)
                {
                    cmbCREATE_DATE.EditValue = System.DateTime.Now.ToShortDateString();
                    cmbCreate_Time.EditValue = "0:00:00";
                    spnVENOUS_PRESSURE.EditValue = dr["VENOUS_PRESSURE"];
                    //  spnARTERY_PRESSURE.EditValue = dr["ARTERY_PRESSURE"];
                    spnTRANSMEMBRANE_PRESSURE.EditValue = dr["TRANSMEMBRANE_PRESSURE"];
                    spnSYSTOLIC_PRESSURE.EditValue = 0;
                    spnDIASTOLIC_PRESSURE.EditValue = 0;
                    spnDIALYSATE_RATE.EditValue = dr["DIALYSATE_RATE"];
                    spnANTICOAGULANT.EditValue = dr["ANTICOAGULANT"];
                    spnCONDUCTIVITY.EditValue = dr["CONDUCTIVITY"];
                    spnURF.EditValue = dr["URF"];
                    spnDISPLACEMENT.EditValue = dr["DISPLACEMENT"];
                    spnBLOOD_FLOW.EditValue = dr["BLOOD_FLOW"];

                    if (dr["VASCULAR_ACCESS_ERRHYISIS"].ToString() == "1")
                    {
                        chkVASCULAR_ACCESS_ERRHYISIS_1.Checked = true;
                    }
                    else if (dr["VASCULAR_ACCESS_ERRHYISIS"].ToString() == "0")
                    {
                        chkVASCULAR_ACCESS_ERRHYISIS_0.Checked = true;
                    }
                    if (dr["VASCULAR_ACCESS_GLIDE"].ToString() == "1")
                    {
                        chkVASCULAR_ACCESS_GLIDE_1.Checked = true;
                    }
                    else if (dr["VASCULAR_ACCESS_GLIDE"].ToString() == "0")
                    {
                        chkVASCULAR_ACCESS_GLIDE_0.Checked = true;
                    }
                    //txtCLINICAL_MANIFESTATION.Text = dr["CLINICAL_MANIFESTATION"].ToString();
                    spnBREATH.EditValue = dr["BREATH"];
                    //治疗
                    txtCLINICAL_MANIFESTATION.EditValue = dr["CURE_MODE"];
                    lupNURSE_ID.EditValue = dr["NURSE_ID"];
                    spnTEMPERATURE.EditValue = dr["TEMPERATURE"];
                    XtraMessageBox.Show("参数已复制，请输入收缩压和舒张压后保存数据。", "参数记录");
                }
                else
                {
                    XtraMessageBox.Show("请选择一条要复制的参数记录。", "参数记录");
                }
            }
        }

        private void Data_CureData_EditValueChanged(object sender, EventArgs e)
        {
            this.addPcureId = _hemodialysisService.GetCureID(this.Data_CureData.EditValue.ToString(), this.addHemoId);
        }

        private void spnBEFORE_DRY_WEIGHT_Leave(object sender, EventArgs e)
        {
            // getDryWater();
        }

        private void spnAFTER_DRY_WEIGHT_Leave(object sender, EventArgs e)
        {
            //   getDryWater();
        }

        private void gridView4_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (gridView4.GetRow(e.RowHandle) == null)
            {
                return;
            }
            else
            {
                //获取所在行指定列的值
                string status = gridView4.GetRowCellValue(e.RowHandle, "STATUS").ToString();

                //比较指定列的状态
                if (status == "0")
                {
                    //    e.Appearance.BackColor = Color.SkyBlue;//设置开立的背景颜色
                    e.Appearance.ForeColor = Color.Black;
                }
                else if (status == "1")
                {
                    e.Appearance.BackColor = Color.LightGoldenrodYellow;//设置执行的背景颜色
                    e.Appearance.ForeColor = Color.Green;
                }
                else if (status == "2")
                {
                    e.Appearance.BackColor = Color.Gray;//设置返回的背景颜色 
                    e.Appearance.ForeColor = Color.Green;
                }
                else if (status == "3")
                {
                    e.Appearance.BackColor = Color.Red;//设置停止的背景颜色 
                    e.Appearance.ForeColor = Color.Black;
                }
                else
                {
                    e.Appearance.BackColor = Color.DeepSkyBlue;
                }
            }
        }

        private void btn_exect_Click(object sender, EventArgs e)
        {
            this.lopSTATUS.EditValue = "已执行";
            if (XtraMessageBox.Show("确定保存当前血透治疗单信息吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            if (!isAddDrugParmars)
            {
                if (txtCREATE_DATE.EditValue != null)
                {
                    SaveDrug();


                    loadDrugGrid(txtCURE_ID.Text, txtHEMODIALYSIS_ID.Text.Trim());
                    this.btn_exect.Visible = false;
                    this.btn_Back.Visible = true;
                }
            }
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            this.lopSTATUS.EditValue = "退回";
            if (XtraMessageBox.Show("确定保存当前血透治疗单信息吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            if (!isAddDrugParmars)
            {
                if (txtCREATE_DATE.EditValue != null)
                {
                    SaveDrug();

                    loadDrugGrid(txtCURE_ID.Text, txtHEMODIALYSIS_ID.Text.Trim());
                    this.btn_exect.Visible = true;
                    this.btn_Back.Visible = false;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtHEMODIALYSIS_PARAMETERS_ID.Text.Length > 0)
            {
                if (XtraMessageBox.Show("确定删除当前透析参数信息吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                // 记录删除前的关键信息（用于审计日志）
                string deletedParamId = txtHEMODIALYSIS_PARAMETERS_ID.Text;
                string deletedInfo = string.Empty;
                if (_HemoialysisParamterDatatable != null)
                {
                    DataTable dtOld = Utility.GetSubTable(_HemoialysisParamterDatatable as DataTable,
                        "HEMODIALYSIS_PARAMETERS_ID = '" + deletedParamId + "'");
                    if (dtOld != null && dtOld.Rows.Count > 0)
                    {
                        DataRow delRow = dtOld.Rows[0];
                        // 只记录关键字段
                        StringBuilder sb = new StringBuilder();
                        string[] keyFields = { "CURE_ID", "HEMODIALYSIS_PARAMETERS_ID", "CREATE_DATE",
                            "VASCULAR_ACCESS_ERRHYISIS", "VASCULAR_ACCESS_GLIDE", "VENOUS_PRESSURE",
                            "TRANSMEMBRANE_PRESSURE", "BLOOD_FLOW", "DIALYSATE_FLOW", "UF" };
                        foreach (string field in keyFields)
                        {
                            if (delRow.Table.Columns.Contains(field))
                            {
                                string val = delRow[field] == DBNull.Value ? "" : delRow[field].ToString().Trim();
                                if (!string.IsNullOrEmpty(val))
                                {
                                    if (sb.Length > 0) sb.Append("; ");
                                    sb.AppendFormat("{0}: {1}", field, val);
                                }
                            }
                        }
                        deletedInfo = sb.ToString();
                    }
                }

                int result = objHemodialysisService.DeleteHemodialysisParametersByID(txtHEMODIALYSIS_PARAMETERS_ID.Text);

                if (result > 0)
                {
                    // 记录审计日志 - 删除透析参数
                    WriteOperationLog("DELETE", "透析参数记录", deletedParamId, deletedInfo, "删除透析参数");

                    loadParaneterGrid(txtCURE_ID.Text, txtHEMODIALYSIS_ID.Text.Trim());
                }
            }
            else
            {
                XtraMessageBox.Show("请选择一条要删除的数据。");
            }
        }

        private void cmbCreate_Time_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                spnVENOUS_PRESSURE.Focus();
            }
        }

        private void cmbVASCULAR_ACCESS_ID_EditValueChanged(object sender, EventArgs e)
        {
            setDefaultValue(cmbVASCULAR_ACCESS_ID.Text.ToString());
        }

        private void EditTreatment_Load(object sender, EventArgs e)
        {
            setDefaultValue(cmbVASCULAR_ACCESS_ID.Text.ToString());
            loadData(addHemoId, addPcureId, tabIndexTemp);
        }

        private void chkVASCULAR_ACCESS_ERRHYISIS_1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVASCULAR_ACCESS_ERRHYISIS_1.Checked)
            {
                chkVASCULAR_ACCESS_ERRHYISIS_0.Checked = false;
            }
        }

        private void chkVASCULAR_ACCESS_ERRHYISIS_0_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVASCULAR_ACCESS_ERRHYISIS_0.Checked)
            {
                chkVASCULAR_ACCESS_ERRHYISIS_1.Checked = false;
            }
        }

        private void chkVASCULAR_ACCESS_GLIDE_1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVASCULAR_ACCESS_GLIDE_1.Checked)
            {
                chkVASCULAR_ACCESS_GLIDE_0.Checked = false;
            }
        }

        private void chkpVASCULAR_ACCESS_GLIDE_0_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVASCULAR_ACCESS_GLIDE_0.Checked)
            {
                chkVASCULAR_ACCESS_GLIDE_1.Checked = false;
            }
        }

        private void gridView4_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            var rowCurrent = gridView4.GetDataRow(e.ListSourceRowIndex) as HemodialysisModel.MED_CURE_DRUGRow;
            if (rowCurrent == null)
            {
                return;
            }

            if (e.Column == gridColumn_COM)
            {
                var exitrows = _CureDrugDatatable.Count(wh => wh.COM_NO == rowCurrent.COM_NO);
                var smalCount = _CureDrugDatatable.Count(wh => wh.COM_NO == rowCurrent.COM_NO && Convert.ToInt32(wh.COM_SUB_NO) < Convert.ToInt32(rowCurrent.COM_SUB_NO));
                var bigCount = _CureDrugDatatable.Count(wh => wh.COM_NO == rowCurrent.COM_NO && Convert.ToInt32(wh.COM_SUB_NO) > Convert.ToInt32(rowCurrent.COM_SUB_NO));
                if (exitrows == 1)
                {
                    e.DisplayText = "";
                    return;
                }

                if (smalCount == 0)
                {
                    e.DisplayText = "┏";
                    return;
                }
                if (bigCount == 0)
                {
                    e.DisplayText = "┗";
                    return;
                }
                e.DisplayText = "┃";
                return;

            }
        }

        private void lupTempDrug_EditValueChanged(object sender, EventArgs e)
        {
            if (lupTempDrug.EditValue != null)
            {
                if (!string.IsNullOrEmpty(lupTempDrug.Text.ToString()))
                    txtCURE_MODE.Text += string.Format("{0};", lupTempDrug.Text.ToString());
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //txtSUMMARY
            ShowSummary frm = new ShowSummary();
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                string strSummary = string.Empty;
                strSummary = frm.Summary;
                txtSUMMARY.Text = strSummary;
            }
        }

        private void btnQJJL_Click(object sender, EventArgs e)
        {
            ShowRescueRecord frm = null;
            if (this.loginType == 0)
            {
                frm = new ShowRescueRecord(_CureMainDatatable.Rows[0]["SUMMARY3"].ToString());
            }
            else
            {
                frm = new ShowRescueRecord(_CureMainDatatable.Rows[0]["SUMMARY2"].ToString());
            }
            frm.ShowDialog();
        }

        /// <summary>
        /// 采集数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGatherData_Click(object sender, EventArgs e)
        {
            DataGatherList dataGatherList = new DataGatherList();
            dataGatherList.CreateDate = cureDate;
            dataGatherList.CureId = addPcureId;
            dataGatherList.RecipeId = _patientScheduleRow.RECIPE_ID;
            dataGatherList.ClassId = _patientScheduleRow.BANCI_ID;
            //dataGatherList.MachineLabel = machineRow.MACHINE_MODEL + "-" + machineRow.MACHINE_NAME;
            dataGatherList.MachineLabel = (machineRow as DataRow)["OTHER_THERAPEUTIC"].ToString();
            dataGatherList.SickArea = _patientScheduleRow.DIALYSIS_ROOM_ID;

            DialogResult result = dataGatherList.ShowDialog();
            if (result == DialogResult.OK)
            {
                loadData(addHemoId, addPcureId, tabIndexTemp);
                tabControls.SelectedTabPage = tab3;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rdoVASCULAR_ACCESS_FIRM.SelectedIndex = -1;
            rdoVASCULAR_ACCESS_GLIDE.SelectedIndex = -1;
            rdoVASCULAR_ACCESS_SWELLING.SelectedIndex = -1;
            rdoVASCULAR_ACCESS_THROMBUS.SelectedIndex = -1;
            rdoVASCULAR_ACCESS_BLOOD.SelectedIndex = -1;
            rdoVASCULAR_ACCESS_ERRHYISIS.SelectedIndex = -1;
            rdoVASCULAR_ACCESS_BLOOD_INFECT.SelectedIndex = -1;
            chkVASCULAR_ACCESS_PENDING.Checked = false;
            txtVASCULAR_ACCESS_PENDING_DATE.EditValue = null;
        }

        private void chkVASCULAR_ACCESS_PENDING_CheckedChanged(object sender, EventArgs e)
        {
            this.txtVASCULAR_ACCESS_PENDING_DATE.Enabled = this.chkVASCULAR_ACCESS_PENDING.Checked;
            if (!this.chkVASCULAR_ACCESS_PENDING.Checked)
            {
                this.txtVASCULAR_ACCESS_PENDING_DATE.EditValue = null;
            }
        }

        private void btnClear1_Click(object sender, EventArgs e)
        {
            rdoIN_BASKET_CLEAN.SelectedIndex = -1;
            rdoIN_BASKET_RED_HOT.SelectedIndex = -1;
            rdoIN_BASKET_ECCHYMOSIS.SelectedIndex = -1;
            rdoIN_BASKET_TREMOR.SelectedIndex = -1;
            rdoIN_BASKET_NOISE.SelectedIndex = -1;
            rdoIN_BASKET_VASCULAR_ELASTICITY.SelectedIndex = -1;
        }
        #endregion

        #region 并发症
        /// <summary>
        /// 加载并发症数据
        /// </summary>
        private void LoadComplication()
        {
            this.complicatinTable = this._hemodialysisService.GetComplicationByDialysisAndCure(this.addHemoId, this.addPcureId);

            if (this.complicatinTable == null)
            {
                this.complicatinTable = new HemoModel.MED_COMPLICATION_OTHERDataTable();
            }

            HemoModel.MED_COMPLICATION_OTHERRow complication = null;
            if (this.complicatinTable.Rows.Count < 1)
            {
                complication = this.complicatinTable.NewMED_COMPLICATION_OTHERRow();
                complication.ID = Guid.NewGuid().ToString();
                complication.HEMODIALYSIS_ID = this.addHemoId;
                complication.CURE_ID = this.addPcureId;
                complication.WORK_DATE = DateTime.Now;
                this.complicatinTable.AddMED_COMPLICATION_OTHERRow(complication);
            }
            else
            {
                complication = this.complicatinTable.Rows[0] as HemoModel.MED_COMPLICATION_OTHERRow;
            }

            foreach (var ctl in this.groupControl1.Controls)
            {
                if (ctl is BaseEdit)
                {
                    (ctl as BaseEdit).BindingDataRow(complication, "comb");
                }
            }

            //this.combFIRST_PURIFIER_MODEL.BindingDataRow(complication, "comb");
            if (this.combFIRST_PURIFIER_MODEL.EditValue == null)
            {
                this.combFIRST_PURIFIER_MODEL.EditValue = this.lupMACHINE_TYPE.EditValue;
            }
            //this.combMACHINE_ID.BindingDataRow(complication, "comb");
            if (this.combMACHINE_ID.EditValue == null)
            {
                this.combMACHINE_ID.EditValue = this.cmbMACHINE_ID.EditValue;
            }
            //this.combMACHINE_ID_TAG.BindingDataRow(complication, "comb");
            if (this.combMACHINE_ID_TAG.EditValue == null)
            {
                this.combMACHINE_ID_TAG.EditValue = this.lupMACHINE_ID_TAG.EditValue;
            }
            //this.combNURSE_ID.BindingDataRow(complication, "comb");
            if (this.combNURSE_ID.EditValue == null)
            {
                this.combNURSE_ID.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
            }
        }
        #endregion

        private void checkOrder_CheckedChanged(object sender, EventArgs e)
        {
            if (isLoadDate) return;
            //var drugDt = objHemodialysisService.GetLongCureDrugByHemoID(addHemoId);
            var drugDt = objHemodialysisService.GetCureDrugByHemoIDAndRecipeId(addHemoId, txtRECIPE_ID.Text);
            if (drugDt == null || drugDt.Rows.Count <= 0) return;
            StringBuilder sbDrug = new StringBuilder();

            for (int i = 0; i < drugDt.Rows.Count; i++)
            {
                sbDrug.Append(drugDt.Rows[i]["NEW_DRUG_NAME"].ToString());
                sbDrug.Append(drugDt.Rows[i]["DOSAGE"].ToString());
                sbDrug.Append(drugDt.Rows[i]["UNIT_NAME"].ToString());
                sbDrug.Append(drugDt.Rows[i]["DRUG_MODE_NAME"].ToString()).Append(";");
            }
            if (!checkOrder.Checked)
            {

                if (this.txtDOCTOR_ADVICE.Text.Trim().Equals("医嘱:" + sbDrug.ToString().Trim()))
                {
                    this.txtDOCTOR_ADVICE.Text = this.txtDOCTOR_ADVICE.Text.Replace(string.Format("医嘱:{0}", sbDrug.ToString().Trim()), "").Trim();
                }
                else
                {
                    this.txtDOCTOR_ADVICE.Text = this.txtDOCTOR_ADVICE.Text.Replace(string.Format(";医嘱:{0}", sbDrug.ToString().Trim()), "");

                }
            }
            else
            {
                if (string.IsNullOrEmpty(this.txtDOCTOR_ADVICE.Text.Trim()))
                {
                    this.txtDOCTOR_ADVICE.Text = string.Format("医嘱:{0}", sbDrug.ToString().Trim());
                }
                else
                {
                    this.txtDOCTOR_ADVICE.Text = string.Format("{0};医嘱:{1}", this.txtDOCTOR_ADVICE.Text, sbDrug.ToString().Trim());

                }

            }
        }

        private void cmbPURIFICATION_MODE_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbPURIFICATION_MODE.Text.Equals("HP") || cmbPURIFICATION_MODE.Text.Equals("HD+HP"))
            {
                this.lupIN_BASKET_WOUND_ALLERGY.Enabled = true;

            }
            else
            {
                this.lupIN_BASKET_WOUND_ALLERGY.Text = string.Empty;
                this.lupIN_BASKET_WOUND_ALLERGY.EditValue = string.Empty;

                this.lupIN_BASKET_WOUND_ALLERGY.Enabled = false;
            }
        }

        private void chkCOAGULATION_IN_DIALYSER_SelectedIndexChanged(object sender, EventArgs e)
        {
            //foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in chkCOAGULATION_IN_DIALYSER.Items)
            //{
            //    item.CheckState = System.Windows.Forms.CheckState.Unchecked;
            //}

            //chkCOAGULATION_IN_DIALYSER.SetItemCheckState(chkCOAGULATION_IN_DIALYSER.SelectedIndex, CheckState.Checked);
            //chkCOAGULATION_IN_DIALYSER.SetItemCheckState(chkCOAGULATION_IN_DIALYSER.SelectedIndex, CheckState.Checked);
            //chkCOAGULATION_IN_DIALYSER.Items[chkCOAGULATION_IN_DIALYSER.SelectedIndex].CheckState = CheckState.Checked;
        }

        private void chkCOAGULATION_IN_DIALYSER_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {

        }

        private void spnAFTER_DRY_WEIGHT_EditValueChanged_1(object sender, EventArgs e)
        {
            if (this.spnAFTER_DRY_WEIGHT.Value != 0)
            {
                //if (txtDRY_WATER_VALUE.Value != 0)
                //{
                txtDRY_WATER_VALUE.Value = spnBEFORE_DRY_WEIGHT.Value - spnAFTER_DRY_WEIGHT.Value;
                //}
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void checkBed_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkIN_BED.Checked)
            {
                this.spnAFTER_DRY_WEIGHT.Visible = false;
                this.labelControlAfter.Visible = true;
                this.labelControlaftere.Text = "透后血压";
            }
            else
            {
                this.spnAFTER_DRY_WEIGHT.Visible = true;
                this.labelControlAfter.Visible = false;
                labelControlaftere.Text = "Kg 透后血压";
            }
        }

        private void spnBEFORE_DRY_WEIGHT_EditValueChanged(object sender, EventArgs e)
        {
            this.spnUFR.EditValue = spnBEFORE_DRY_WEIGHT.Value - spnDRY_WEIGHT.Value - spnDRY_WEIGHT_TAG.Value;
            if (spnBEFORE_DRY_WEIGHT.Value - spnDRY_WEIGHT.Value - spnDRY_WEIGHT_TAG.Value < 0)
                this.spnUFR.Value = 0;
        }

        private void spnDISPLACEMENT_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void spnBREATH_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void spnUFR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.spnUFR.EditValue = spnBEFORE_DRY_WEIGHT.Value - spnDRY_WEIGHT.Value - spnDRY_WEIGHT_TAG.Value;
                if (spnBEFORE_DRY_WEIGHT.Value - spnDRY_WEIGHT.Value - spnDRY_WEIGHT_TAG.Value < 0)
                    this.spnUFR.Value = 0;
            }
        }

        private void txtDRY_WATER_VALUE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtDRY_WATER_VALUE.EditValue = spnBEFORE_DRY_WEIGHT.Value - spnAFTER_DRY_WEIGHT.Value;
                if (spnBEFORE_DRY_WEIGHT.Value - spnAFTER_DRY_WEIGHT.Value < 0)
                    this.txtDRY_WATER_VALUE.Value = 0;
            }
        }

        private void spnDRY_WEIGHT_EditValueChanged(object sender, EventArgs e)
        {
            this.spnUFR.EditValue = spnBEFORE_DRY_WEIGHT.Value - spnDRY_WEIGHT.Value - spnDRY_WEIGHT_TAG.Value;
            if (spnBEFORE_DRY_WEIGHT.Value - spnDRY_WEIGHT.Value - spnDRY_WEIGHT_TAG.Value < 0)
                this.spnUFR.Value = 0;
        }
    }
}