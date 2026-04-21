using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.IService;
using Hemo.Service;

namespace Hemo.Client.UI.Config
{
    /// <summary>
    /// 添加人：吴兆娟
    /// 添加时间：2018-08-21
    /// </summary>
    public partial class ImportPatient : UserControl
    {
        //用户未登录，使用注释代码报错

        private List<String> standardColumn;

        private string logFileName = "Excel导入";

        private IPatient _patientService = ServiceManager.Instance.PatientService;

        public ImportPatient()
        {
            InitializeComponent();

            InitData();
        }

        #region <<初始化>>

        private void InitData()
        {
            standardColumn = new List<string>();
            standardColumn.Add("姓名");
            standardColumn.Add("性别");
            standardColumn.Add("患者来源");
            standardColumn.Add("住院（门诊）号");
            standardColumn.Add("出生日期");
            standardColumn.Add("籍贯");
            standardColumn.Add("婚姻");
            standardColumn.Add("工作");
            standardColumn.Add("学历");
            standardColumn.Add("民族");
            standardColumn.Add("姓名输入码");
            standardColumn.Add("医保号");
            standardColumn.Add("身份证");
            standardColumn.Add("年龄");
            standardColumn.Add("传染病");
            standardColumn.Add("诊断");
            standardColumn.Add("首次透析时间");
            //standardColumn.Add("透析次数");
            standardColumn.Add("联系电话");
            standardColumn.Add("家庭住址");
        }

        #endregion

        #region <<事件>>

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            DialogResult result = ofd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                string filename = ofd.FileName;
                tbFileName.Text = filename;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            string filename = tbFileName.Text;
            ValidateResult vr = Bind(filename);

            //格式不正确—不提交
            if (vr.IsSuccess == false)
            {
                this.tbMessage.Text = vr.ErrMessage;
                XtraMessageBox.Show(vr.ErrShortMessage, "导入错误", MessageBoxButtons.OK);
                WriteLogs(logFileName, null, vr.ErrMessage);
                return;
            }

            this.progressBar1.Visible = true;
            this.progressBar1.BringToFront();
            this.progressBar1.Show();

            //格式正确—提交
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                DataTable result = new DataTable();
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    PatientModel.MED_PATIENTSDataTable patientsDataTable = vr.Value as PatientModel.MED_PATIENTSDataTable;



                    result = _patientService.SaveImportedPatients(patientsDataTable);
                };

                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    #region <<信息，日志>>

                    string successCount = "-1";
                    string failCount = "0";
                    string failDetail = string.Empty;

                    if (result != null)
                    {
                        if (result.Rows.Count > 0)
                        {
                            successCount = result.Rows[0]["SuccessCount"].ToString();
                            failCount = result.Rows[0]["ExistCount"].ToString();
                            failDetail = result.Rows[0]["ExistDetail"].ToString();
                        }
                    }

                    string message;
                    if (int.Parse(successCount) < 0)
                    {
                        message = failDetail;
                    }
                    else
                    {
                        message = "1.成功导入患者数据" + successCount + "条。\r\n";
                        message += "2.失败导入患者数据" + failCount + "条。";
                        if (int.Parse(failCount) > 0)
                        {
                            message += "系统已存在下列患者，请勿重复导入。若需要修改患者信息，请于系统中修改！明细如下：\r\n";
                        }
                        message += failDetail;
                    }

                    vr.ErrMessage = message;
                    tbMessage.Text = vr.ErrMessage;
                    WriteLogs(logFileName, null, vr.ErrMessage);
                    //XtraMessageBox.Show("导入明细请见日志", "患者导入", MessageBoxButtons.OK);

                    #endregion

                    this.progressBar1.Visible = false;
                    this.progressBar1.Hide();
                };
                worker.RunWorkerAsync();
            }
        }

        private void btnOpenLog_Click(object sender, EventArgs e)
        {
            ofdLog.InitialDirectory = GetLogFileDirectory(logFileName);
            DialogResult result = ofdLog.ShowDialog(this);
            try
            {
                if (result == DialogResult.OK)
                {
                    string filename = ofdLog.FileName;

                    StringBuilder sb = Read(filename);
                    tbLogFile.Text = sb.ToString();
                    //使用默认程序打开
                    //System.Diagnostics.Process.Start(path);
                }
            }
            catch
            {
                XtraMessageBox.Show("日志打开错误，请联系系统管理员。", "日志打开错误", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region <<辅助方法>>

        #region <<数据绑定>>

        private ValidateResult Bind(string fileName)
        {
            string connstr2003 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
            string connstr2007 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;HDR=YES\"";
            OleDbConnection conn;
            if (fileName == "xls")
            {
                conn = new OleDbConnection(connstr2003);
            }
            else
            {
                conn = new OleDbConnection(connstr2007);
            }
            OleDbDataAdapter oledbda = null;
            DataSet ds = null;

            ValidateResult vrResult = new ValidateResult();
            try
            {
                conn.Open();
                string strselect = "SELECT * FROM [患者信息$]";
                oledbda = new OleDbDataAdapter(strselect, conn);
                ds = new DataSet();
                oledbda.Fill(ds);

                //模板校验
                vrResult = ValidateTemplate(ds.Tables[0]);
                if (vrResult.IsSuccess == false)
                {
                    return vrResult;
                }

                //数据转换
                vrResult = ConvertDataTable(ds.Tables[0]);
            }
            catch (Exception e)
            {
                vrResult.IsSuccess = false;
                vrResult.Value = null;
                vrResult.ErrMessage = e.Message;
                vrResult.ErrShortMessage = e.Message;
            }
            finally
            {
                conn.Close();
            }
            return vrResult;
        }

        private ValidateResult ValidateTemplate(DataTable dt)
        {
            ValidateResult vr = new ValidateResult();
            vr.IsSuccess = true;

            if (standardColumn != null)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    bool isExist = standardColumn.Contains(dc.ColumnName);
                    if (isExist == false)
                    {
                        vr.IsSuccess = false;
                        vr.ErrMessage = "Excel中列：" + dc.ColumnName + "不能被识别，请确认Excel模板是否正确，或者联系系统管理员。";
                        vr.ErrShortMessage = "Excel中存在列不能被识别，请确认Excel模板是否正确，或者联系系统管理员。";
                        break;
                    }
                }
            }

            return vr;
        }

        /// <summary>
        /// 校验是否存在重复数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private ValidateResult ValidateRepeatData(DataTable dt)
        {
            ValidateResult vr = new ValidateResult();
            vr.IsSuccess = true;

            List<string> list = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataRow drSub in dt.Rows)
                {
                    if (dr != drSub)
                    {
                        if (dr["身份证"].ToString() == drSub["身份证"].ToString())
                        {
                            if (!list.Contains(dr["身份证"].ToString()))
                            {
                                list.Add(dr["身份证"].ToString());
                            }
                            break;
                        }
                    }
                }
            }
            if (list.Count != 0)
            {
                vr.IsSuccess = false;
                string message = "Excel中存在关键列身份证数据重复，请核实。明细如下：\r\n";
                message += string.Join("\r\n", list);
                vr.ErrMessage = message;
                vr.ErrShortMessage = "Excel中存在关键列身份证数据重复，请核实。";
            }
            return vr;
        }

        /// <summary>
        /// 去除空白行
        /// 方式其他：加载sheet时使用where过滤，但是貌似耦合性有点强了
        /// </summary>
        /// <param name="dt"></param>
        private DataTable RemoveEmptyRow(DataTable dt)
        {
            bool IsNull = true;
            List<DataRow> removelist = new List<DataRow>();
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    if (!string.IsNullOrEmpty(dr[dc.ColumnName].ToString().Trim()))
                    {
                        IsNull = false;
                        break;
                    }
                }
                if (IsNull)
                {
                    removelist.Add(dr);
                }
            }
            foreach (DataRow dr in removelist)
            {
                dt.Rows.Remove(dr);
            }
            return dt;
        }

        private ValidateResult ConvertDataTable(DataTable dt)
        {
            PatientModel.MED_PATIENTSDataTable patientDataTable = new PatientModel.MED_PATIENTSDataTable();

            ValidateResult vrDataTable = new ValidateResult();
            vrDataTable.IsSuccess = true;
            List<string> listDataTable = new List<string>();

            //去除空白行
            dt = RemoveEmptyRow(dt);

            //空校验
            if (dt == null || dt.Rows.Count == 0)
            {
                vrDataTable.IsSuccess = false;
                vrDataTable.ErrMessage = "导入数据为空，请核实。";
                vrDataTable.ErrShortMessage = vrDataTable.ErrMessage;
                return vrDataTable;
            }

            //关键字段：身份证重复校验
            vrDataTable = ValidateRepeatData(dt);
            if (vrDataTable.IsSuccess == false)
            {
                return vrDataTable;
            }

            #region <<行处理>>

            foreach (DataRow dr in dt.Rows)
            {
                PatientModel.MED_PATIENTSRow drPatient = patientDataTable.NewMED_PATIENTSRow();

                //一次提供所有不合法信息，计入日志,提示导入操作人员
                ValidateResult vrRow = new ValidateResult();
                vrRow.IsSuccess = true;

                #region <<列处理>>

                List<string> listRow = new List<string>();

                //透析号
                drPatient.HEMODIALYSIS_ID = Guid.NewGuid().ToString();

                //姓名
                ValidateResult vrName = GetRowColumnValue(dr, "姓名", ValidateType.NotNULL);
                drPatient.NAME = vrName.Value.ToString();
                if (!vrName.IsSuccess)
                {
                    vrRow.IsSuccess = false;
                    listRow.Add(vrName.ErrMessage);
                }

                //性别
                ValidateResult vrSex = GetRowColumnValue(dr, "性别", ValidateType.NotNULL);
                drPatient.SEX = vrSex.Value.ToString();
                if (!vrSex.IsSuccess)
                {
                    vrRow.IsSuccess = false;
                    listRow.Add(vrSex.ErrMessage);
                }

                //患者来源
                ValidateResult vrPatientType = GetRowColumnValue(dr, "患者来源", ValidateType.NotNULL);
                drPatient.TIME_TYPE = vrPatientType.Value.ToString();
                if (!vrPatientType.IsSuccess)
                {
                    vrRow.IsSuccess = false;
                    listRow.Add(vrPatientType.ErrMessage);
                }

                //住院（门诊）号
                ValidateResult vrClinicOrHospitalNo = GetRowColumnValue(dr, "住院（门诊）号", ValidateType.NotNULL);
                drPatient.PATIENT_ID = vrClinicOrHospitalNo.Value.ToString();//门诊
                drPatient.ADMISSION_NUMBER = vrClinicOrHospitalNo.Value.ToString();//住院
                if (!vrClinicOrHospitalNo.IsSuccess)
                {
                    vrRow.IsSuccess = false;
                    listRow.Add(vrClinicOrHospitalNo.ErrMessage);
                }

                //医保号
                ValidateResult vrMedicalNo = GetRowColumnValue(dr, "医保号");
                drPatient.MEDICAL_TYPE = vrMedicalNo.Value.ToString();
                if (!vrMedicalNo.IsSuccess)
                {
                    vrRow.IsSuccess = false;
                    listRow.Add(vrMedicalNo.ErrMessage);
                }

                //身份证
                ValidateResult vrIdentityNo = GetRowColumnValue(dr, "身份证", ValidateType.NotNULL, ValidateType.NotIdentifyNo);
                drPatient.CREDENTIALS_NUMBER = vrIdentityNo.Value.ToString();
                if (!vrIdentityNo.IsSuccess)
                {
                    vrRow.IsSuccess = false;
                    listRow.Add(vrIdentityNo.ErrMessage);
                }

                //年龄
                ValidateResult vrAge = GetRowColumnValue(dr, "年龄", ValidateType.NotIntNullable);
                if (!vrAge.IsSuccess)
                {
                    vrRow.IsSuccess = false;
                    listRow.Add(vrAge.ErrMessage);
                }
                else
                {
                    if (vrAge.Value == null || string.IsNullOrEmpty(vrAge.Value.ToString()))
                    {
                        drPatient.SetAGENull();
                    }
                    else
                    {
                        drPatient.AGE = int.Parse(vrAge.Value.ToString());
                    }
                }

                //传染病
                ValidateResult vrInfectious = GetRowColumnValue(dr, "传染病", ValidateType.NotNULL);
                drPatient.INFECTIOUS_CHECK_RESULT = vrInfectious.Value.ToString();
                if (!vrInfectious.IsSuccess)
                {
                    vrRow.IsSuccess = false;
                    listRow.Add(vrInfectious.ErrMessage);
                }

                //诊断
                ValidateResult vrDiagnose = GetRowColumnValue(dr, "诊断", ValidateType.NotNULL);
                drPatient.DIAGNOSE = vrDiagnose.Value.ToString();
                if (!vrDiagnose.IsSuccess)
                {
                    vrRow.IsSuccess = false;
                    listRow.Add(vrDiagnose.ErrMessage);
                }

                //首次透析时间
                ValidateResult vrDateTime = GetRowColumnValue(dr, "首次透析时间", ValidateType.NotDateTimeNullable);
                if (!vrDateTime.IsSuccess)
                {
                    vrRow.IsSuccess = false;
                    listRow.Add(vrDateTime.ErrMessage);
                }
                else
                {
                    if (vrDateTime.Value == null || string.IsNullOrEmpty(vrDateTime.Value.ToString()))
                    {
                        drPatient.SetSPECIFIC_TIMENull();
                    }
                    else
                    {
                        drPatient.SPECIFIC_TIME = DateTime.Parse(vrDateTime.Value.ToString());
                    }
                }

                //透析次数
                //ValidateResult vrTouXiCount = GetRowColumnValue(dr, "透析次数");
                //drPatient.FIRST_COUNT = vrTouXiCount.Value.ToString();
                //if (!vrTouXiCount.IsSuccess)
                //{
                //    vrRow.IsSuccess = false;
                //    listRow.Add(vrTouXiCount.ErrMessage);
                //}

                //联系电话
                ValidateResult vrTelephone = GetRowColumnValue(dr, "联系电话");
                drPatient.TELEPHONE = vrTelephone.Value.ToString();
                if (!vrTelephone.IsSuccess)
                {
                    vrRow.IsSuccess = false;
                    listRow.Add(vrTelephone.ErrMessage);
                }

                //家庭住址
                ValidateResult vrAddress = GetRowColumnValue(dr, "家庭住址");
                drPatient.ADDRESS = vrAddress.Value.ToString();
                if (!vrAddress.IsSuccess)
                {
                    vrRow.IsSuccess = false;
                    listRow.Add(vrAddress.ErrMessage);
                }

                //出生日期
                ValidateResult vrBirthday = GetRowColumnValue(dr, "出生日期", ValidateType.NotDateTimeNullable);
                if (!vrBirthday.IsSuccess)
                {
                    vrRow.IsSuccess = false;
                    listRow.Add(vrBirthday.ErrMessage);
                }
                else
                {
                    if (vrBirthday.Value == null || string.IsNullOrEmpty(vrBirthday.Value.ToString()))
                    {
                        drPatient.SetBIRTHDAYNull();
                    }
                    else
                    {
                        drPatient.BIRTHDAY = DateTime.Parse(vrBirthday.Value.ToString());
                    }
                }
                //籍贯
                ValidateResult vrNativeplace = GetRowColumnValue(dr, "籍贯");
                drPatient.NATIVEPLACE = vrNativeplace.Value.ToString();
                if (!vrNativeplace.IsSuccess)
                {
                    vrRow.IsSuccess = false;
                    listRow.Add(vrNativeplace.ErrMessage);
                }
                //婚姻
                ValidateResult vrMaritals = GetRowColumnValue(dr, "婚姻");
                drPatient.MARITAL = vrMaritals.Value.ToString();
                if (!vrMaritals.IsSuccess)
                {
                    vrRow.IsSuccess = false;
                    listRow.Add(vrMaritals.ErrMessage);
                }
                //学历
                ValidateResult vrEducation = GetRowColumnValue(dr, "学历");
                drPatient.EDUCATION = vrEducation.Value.ToString();
                if (!vrEducation.IsSuccess)
                {
                    vrRow.IsSuccess = false;
                    listRow.Add(vrEducation.ErrMessage);
                }
                //民族
                ValidateResult vrNation = GetRowColumnValue(dr, "民族");
                drPatient.NATION = vrNation.Value.ToString();
                if (!vrNation.IsSuccess)
                {
                    vrRow.IsSuccess = false;
                    listRow.Add(vrNation.ErrMessage);
                }
                //姓名输入码
                ValidateResult vrInput_code = GetRowColumnValue(dr, "姓名输入码");
                drPatient.INPUT_CODE = vrInput_code.Value.ToString();
                if (!vrInput_code.IsSuccess)
                {
                    vrRow.IsSuccess = false;
                    listRow.Add(vrInput_code.ErrMessage);
                }
                //工作
                ValidateResult vrJob = GetRowColumnValue(dr, "工作");
                drPatient.JOB = vrJob.Value.ToString();
                if (!vrJob.IsSuccess)
                {
                    vrRow.IsSuccess = false;
                    listRow.Add(vrJob.ErrMessage);
                }
                #endregion

                if (!vrRow.IsSuccess)
                {
                    string excelPlace = "第" + (dt.Rows.IndexOf(dr) + 2) + "行";
                    string errMessage = excelPlace + "：\r\n" + String.Join("  ", listRow);
                    vrRow.ErrMessage = errMessage;

                    listDataTable.Add(vrRow.ErrMessage);
                    vrDataTable.IsSuccess = false;
                }
                if (vrRow.IsSuccess)
                {
                    patientDataTable.Rows.Add(drPatient);
                }
            }

            #endregion

            string resultMessage = String.Join("\r\n", listDataTable);
            vrDataTable.ErrMessage = resultMessage;
            vrDataTable.ErrShortMessage = "Excel中存在不合法数据格式，请核实。";
            vrDataTable.Value = patientDataTable;
            return vrDataTable;
        }

        #endregion

        #region <<各类数据验证>>

        private ValidateResult GetRowColumnValue(DataRow dr, string columnName, params ValidateType[] vtArray)
        {
            ValidateResult vr = new ValidateResult();


            if (dr.Table.Columns.Contains(columnName))
            {
                object value = dr[columnName];

                //获取值
                vr.Value = value;

                //验证值
                vr.IsSuccess = true;
                List<string> list = new List<string>();
                foreach (ValidateType vt in vtArray)
                {
                    ValidateResult vrSingle = new ValidateResult();

                    //优先
                    if (vt == ValidateType.NotNULL)
                    {
                        vrSingle = NotNULL(value);
                        if (!vrSingle.IsSuccess)
                        {
                            vr.IsSuccess = false;
                            list.Add(vrSingle.ErrMessage);
                            break;
                        }
                    }

                    switch (vt)
                    {
                        case ValidateType.NotDateTime:
                            vrSingle = NotDateTime(value);
                            if (!vrSingle.IsSuccess)
                            {
                                vr.IsSuccess = false;
                                list.Add(vrSingle.ErrMessage);
                            }
                            break;
                        case ValidateType.NotDateTimeNullable:
                            vrSingle = NotDateTimeNullable(value);
                            if (!vrSingle.IsSuccess)
                            {
                                vr.IsSuccess = false;
                                list.Add(vrSingle.ErrMessage);
                            }
                            break;
                        case ValidateType.NotIdentifyNo:
                            vrSingle = NotIdentifyNo(value);
                            if (!vrSingle.IsSuccess)
                            {
                                vr.IsSuccess = false;
                                list.Add(vrSingle.ErrMessage);
                            }
                            break;
                        case ValidateType.NotIntNullable:
                            vrSingle = NotIntNullable(value);
                            if (!vrSingle.IsSuccess)
                            {
                                vr.IsSuccess = false;
                                list.Add(vrSingle.ErrMessage);
                            }
                            break;
                    }
                }
                if (!vr.IsSuccess)
                {
                    string errMessage = columnName + "：" + String.Join("  ", list);
                    vr.ErrMessage = errMessage;
                }
            }
            return vr;
        }

        public enum ValidateType
        {
            NotNULL = 0,
            NotDateTime = 1,
            NotIdentifyNo = 2,
            NotIntNullable = 3,
            NotDateTimeNullable = 4
        }

        public class ValidateResult
        {
            public object Value { get; set; }
            public bool IsSuccess { get; set; }
            public string ErrMessage { get; set; }
            public string ErrShortMessage { get; set; }
        }

        private ValidateResult NotNULL(object value)
        {
            ValidateResult vr = new ValidateResult() { IsSuccess = true };
            vr.Value = value.ToString();
            if (String.IsNullOrEmpty(value.ToString()))
            {
                vr.IsSuccess = false;
                vr.ErrMessage = "不能为空";
            }
            return vr;
        }

        private ValidateResult NotDateTime(object value)
        {
            ValidateResult vr = new ValidateResult() { IsSuccess = true };
            try
            {
                vr.Value = DateTime.Parse(value.ToString());
            }
            catch
            {
                vr.IsSuccess = false;
                vr.ErrMessage = "不是合法的日期格式";
            }
            return vr;
        }

        private ValidateResult NotDateTimeNullable(object value)
        {
            ValidateResult vr = new ValidateResult() { IsSuccess = true, Value = null };
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return vr;
            }
            try
            {
                vr.Value = DateTime.Parse(value.ToString());
            }
            catch
            {
                vr.IsSuccess = false;
                vr.ErrMessage = "不是合法的日期格式";
            }
            return vr;
        }

        private ValidateResult NotIdentifyNo(object value)
        {
            string IsCardNumber = Utilities.IDCardHelper.Validate(value.ToString());
            ValidateResult vr = new ValidateResult() { IsSuccess = true, Value = value };
            if (!string.IsNullOrEmpty(IsCardNumber))
            {
                vr.IsSuccess = false;
                vr.ErrMessage = IsCardNumber;
                vr.Value = null;
            }
            return vr;
        }

        private ValidateResult NotIntNullable(object value)
        {
            ValidateResult vr = new ValidateResult() { IsSuccess = true, Value = null };
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return vr;
            }
            try
            {
                vr.Value = int.Parse(value.ToString());
            }
            catch (Exception e)
            {
                vr.IsSuccess = false;
                vr.ErrMessage = e.Message;
                vr.Value = null;
            }
            return vr;
        }

        #endregion

        #region <<日志>>

        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="type"></param>
        /// <param name="content"></param>
        public void WriteLogs(string fileName, string type, string content)
        {
            string path = GetLogFilePath(fileName);
            if (!string.IsNullOrEmpty(path))
            {
                if (File.Exists(path))
                {
                    StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default);
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + type + "\r\n" + content);
                    //sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + type + "-->\r\n" + content);
                    sw.WriteLine("-----------------------------------------------------------------------------");
                    sw.Close();
                }
            }
        }


        private string GetLogFilePath(string fileName)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            if (!string.IsNullOrEmpty(path))
            {
                path = AppDomain.CurrentDomain.BaseDirectory + fileName;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + "\\" + DateTime.Now.ToString("yyyyMMdd");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                if (!File.Exists(path))
                {
                    FileStream fs = File.Create(path);
                    fs.Close();
                }
            }
            return path;
        }

        private string GetLogFileDirectory(string fileName)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            if (!string.IsNullOrEmpty(path))
            {
                path = AppDomain.CurrentDomain.BaseDirectory + fileName;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            return path;
        }

        public StringBuilder Read(string path)
        {
            StringBuilder sb = new StringBuilder();

            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                sb.Append(line.ToString() + "\r\n");
                //Console.WriteLine(line.ToString());
            }
            return sb;
        }

        #endregion

        #endregion

    }
}
