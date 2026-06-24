/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：测试患者数据窗体
// 创建时间：2015-07-16
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraTab;
using Hemo.Client.Doc;
using Hemo.Client.Properties;
using Hemo.IService;
using Hemo.IService.Config;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.Client.UI.Hemodialysis;
using Hemo.Client.UI.Erythropoietin;
using Hemo.Client.UI.Order;
using Hemo.Client.UI.Lab;
using System.Threading;
using DevExpress.XtraBars.Alerter;
namespace Hemo.Client {
    public partial class TestInterFace :HemoBaseFrm
    {
        #region 类变量

        private PatientModel.MED_PATIENTSDataTable _patientDataTable;
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private string strWardCode = ConfigurationManager.AppSettings["WardCode"].ToString();
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _purificationModeDataTable;
        private IConfig _configService = ServiceManager.Instance.ConfigService;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public TestInterFace()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                //this.picLoading.Visible = true;
                bool result = false;
                //同步全部

                //先导入病人到麻醉ICU库
                result = SynchronizeAllPatient(strWardCode);

                //同步全部病人到基础病人表
                if (result)
                {
                    result = UpdatePatientTable("", true);
                    if (result)
                    {
                        // this.picLoading.Visible = false;
                        XtraMessageBox.Show("同步病人信息成功！", "病人信息");
                        LoadPatientList("全部");
                    }
                }

            }
            catch (Exception ex)
            {
                //   this.picLoading.Visible = false;
                XtraMessageBox.Show(ex.Message, "病人信息");

            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            bool result = false;

            try
            {
                string error = InterfaceUtility.SynchronizeSingleOrder("1268958", 1);

                if (string.IsNullOrEmpty(error))
                    result = true;
                else
                    XtraMessageBox.Show(string.Format("新接口（3.0）报错：{0}", error));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }

            //if (result)
            //    this.LoadOrderData();

            //this.btnSelectAll.Visible = this.btnCancelSelected.Visible = this.btnExecOrder.Visible = this.btnSyncOrderInfo.Visible = true;
            //this.picLoading.Visible = false;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                bool result = false;
                string error = InterfaceUtility.SynchronizeSigleValidation("0399011", 3);

                if (string.IsNullOrEmpty(error))
                    result = true;
                else
                    XtraMessageBox.Show(string.Format("新接口（3.0）报错：{0}", error));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = _configService.GetConfigList("", "", "区域", "1");
            string result = Utilities.Utility.DataTableToJsonBySuccess(dt);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载病人信息
        /// </summary>
        private void LoadPatientList(string pType)
        {
            if (pType == "全部")
            {
                _patientDataTable = objPatient.GetPatientList();
            }
            else if (pType == "CRRT" || pType == "急诊" || pType == "门诊")
            {
                _patientDataTable = objPatient.GetPatientListByType(pType);
            }
            foreach (PatientModel.MED_PATIENTSRow patientRow in _patientDataTable)
            {
                if (patientRow.SEX == "男")
                {
                    patientRow.PATIENT_HEAD_PORTRAIT = Utility.BitmapToBytes(Resources.boy);
                }
                else
                {
                    patientRow.PATIENT_HEAD_PORTRAIT = Utility.BitmapToBytes(Resources.gril);
                }
            }
            //    grdPatient.DataSource = _patientDataTable;
        }

        /// 同步全部病人基本信息
        /// </summary>
        /// <returns></returns>
        public bool SynchronizeAllPatient(string pPerformedcode)
        {
            string error = InterfaceUtility.SynchronizePatient(pPerformedcode);
            if (error.Length > 0)
            {
                MessageBox.Show("新接口（3.0）报错：" + error);// + MessageBoxIcon.Error
                return false;
            }
            else
                return true;
        }

        private bool UpdatePatientTable(string patientID, bool isAll)
        {
            bool result = false;
            //取到数据后插入到病人表
            HemodialysisModel.MED_PAT_MASTER_INDEXDataTable pMaster;
            if (patientID.Length > 0 && isAll == false)
            {
                pMaster = _hemodialysisService.GetPatientMasterIndexByPatientID(patientID, strWardCode);
            }
            else
            {
                pMaster = _hemodialysisService.GetPatientMasterIndexList(strWardCode);
            }
            PatientModel.MED_PATIENTSDataTable patientDataTable = objPatient.GetPatientList();
            PatientModel.MED_PATIENTSRow patientRowData = null;
            PatientModel.MED_PATIENTSDataTable tmpPatient = new PatientModel.MED_PATIENTSDataTable();
            if (pMaster != null && pMaster.Rows.Count > 0)
            {
                foreach (HemodialysisModel.MED_PAT_MASTER_INDEXRow patIndexRow in pMaster.Rows)
                {

                    DataRow[] rows = patientDataTable.Select("PATIENT_ID = '" + patIndexRow.PATIENT_ID + "'");

                    if (rows.Length > 0)
                        patientRowData = rows[0] as PatientModel.MED_PATIENTSRow;

                    if (patientRowData == null) //new
                    {
                        patientRowData = patientDataTable.NewMED_PATIENTSRow();
                        patientRowData.PATIENT_ID = patIndexRow.PATIENT_ID;
                        patientRowData.HEMODIALYSIS_ID = objPatient.GetNewHemoID();
                        patientDataTable.AddMED_PATIENTSRow(patientRowData);
                    }

                    if (!patIndexRow.IsNAMENull())
                    {
                        patientRowData.NAME = patIndexRow.NAME;
                    }
                    if (!patIndexRow.IsNAME_PHONETICNull())
                    {
                        patientRowData.INPUT_CODE = patIndexRow.NAME_PHONETIC == string.Empty ? PinYinConverter.GetPYString(patIndexRow.NAME) : patIndexRow.NAME_PHONETIC;
                    }
                    if (!patIndexRow.IsSEXNull())
                    {
                        patientRowData.SEX = patIndexRow.SEX;
                    }
                    if (!patIndexRow.IsDATE_OF_BIRTHNull())
                    {
                        patientRowData.BIRTHDAY = patIndexRow.DATE_OF_BIRTH;
                        //根据出生日期计算年龄
                        patientRowData.AGE = Hemo.Utilities.Utility.GetAge(patientRowData.BIRTHDAY.ToShortDateString());
                    }
                    if (!patIndexRow.IsBIRTH_PLACENull())
                    {
                        patientRowData.NATIVEPLACE = patIndexRow.BIRTH_PLACE;
                    }
                    if (!patIndexRow.IsID_NONull())
                    {
                        patientRowData.CREDENTIALS_TYPE = "居民身份证";
                        patientRowData.CREDENTIALS_NUMBER = patIndexRow.ID_NO;
                    }
                    if (!patIndexRow.IsNATIONNull())
                    {
                        patientRowData.NATION = patIndexRow.NATION;
                    }
                    if (!patIndexRow.IsPHONE_NUMBER_HOMENull())
                    {
                        patientRowData.WORK_TELEPHONE = patIndexRow.PHONE_NUMBER_HOME;
                    }
                    if (!patIndexRow.IsMAILING_ADDRESSNull())
                    {
                        patientRowData.ADDRESS = patIndexRow.MAILING_ADDRESS;
                    }
                    if (!patIndexRow.IsCHARGE_TYPENull())
                    {
                        patientRowData.MEDICAL_TYPE = patIndexRow.CHARGE_TYPE;
                    }
                    if (!patIndexRow.IsNEXT_OF_KIN_PHONENull())
                    {
                        patientRowData.TELEPHONE = patIndexRow.NEXT_OF_KIN_PHONE;
                    }
                    if (!patIndexRow.IsVISIT_IDNull())
                    {
                        patientRowData.VISIT_ID = patIndexRow.VISIT_ID;
                    }
                    if (!patIndexRow.IsBED_NONull())
                    {
                        patientRowData.BED_NO = patIndexRow.BED_NO.ToString();
                    }
                    if (!patIndexRow.IsADMISSION_DATE_TIMENull())
                    {
                        patientRowData.SPECIFIC_TIME = Utility.CDate(patIndexRow.ADMISSION_DATE_TIME.ToString());
                    }
                    if (!patIndexRow.IsWARD_NAMENull())
                    {
                        patientRowData.WARD_CODE = patIndexRow.WARD_NAME;
                    }
                    if (!patIndexRow.IsDEPT_NAMENull())
                    {
                        patientRowData.WHAT_DEPARTMENT_IN = patIndexRow.DEPT_NAME;
                    }
                    if (!patIndexRow.IsDIAGNOSISNull())
                    {
                        patientRowData.DIAGNOSE = patIndexRow.DIAGNOSIS;
                    }
                    if (!patIndexRow.IsINP_NONull())
                    {
                        patientRowData.ADMISSION_NUMBER = patIndexRow.INP_NO;
                    }
                    patientRowData = null;
                    objPatient.SavePatientInfo(patientDataTable);
                }

                result = true;
            }
            return result;
        }

        #endregion
    }
}