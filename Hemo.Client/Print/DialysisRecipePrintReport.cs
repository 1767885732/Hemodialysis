using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using Hemo.IService;
using Hemo.Model;

namespace Hemo.Client.Print
{
    public partial class DialysisRecipePrintReport : XtraReport
    {
        //透析患者基本信息
        private IPatient objPatient = Hemo.Service.ServiceManager.Instance.PatientService;
        private PatientModel.MED_PATIENTSDataTable _patientDataTable;
        public class RecipeOrderItem
        {
            public string HEMODIALYSIS_ID { get; set; }
            public string NAME { get; set; }
            public string SEX { get; set; }
            public string AGE { get; set; }

            public string RECIPE_TYPE_NAME { get; set; }
            public string PURIFICATION_MODE_NAME { get; set; }
            public string STATUS_NAME { get; set; }
            public string RECIPE_DATE { get; set; }
            public string DOCTOR_NAME { get; set; }
            public string HEMO_FREQUENCY_TEXT { get; set; }
            public string PURIFIER_NAME { get; set; }
            public string DRY_WEIGHT { get; set; }
            public string EXECUTE_DATE { get; set; }
            public string NURSE_NAME { get; set; }

            public string LT_RECIPE_TYPE_NAME { get; set; }
            public string LT_RECIPE_DATE { get; set; }
            public string LT_HEMO_FREQUENCY_TEXT { get; set; }
            public string LT_PURIFICATION_MODE_NAME { get; set; }
            public string LT_PURIFIER_NAME { get; set; }
            public string LT_DRY_WEIGHT { get; set; }
            public string LT_REMARK { get; set; }
        }

        public DialysisRecipePrintReport(string pHemoID)
        {
            InitializeComponent();
            _patientDataTable = objPatient.GetPatientListByParams("", pHemoID);
            if (_patientDataTable != null && _patientDataTable.Rows.Count > 0)
            {
                lblHemoID.Text = pHemoID;
                lblName.Text = _patientDataTable.Rows[0]["NAME"].ToString();
                lblSex.Text = _patientDataTable.Rows[0]["SEX"].ToString();
                lblAge.Text = _patientDataTable.Rows[0]["AGE"].ToString();
            }
        }

        /// <summary>
        /// 设置打印数据
        /// </summary>
        /// <param name="tempRecipeRows"></param>
        /// <param name="longTermRecipeRow"></param>
        /// <param name="patientInfo"></param>
        public void SetData(List<DataRow> tempRecipeRows, DataRow longTermRecipeRow, DataRow patientInfo)
        {
            try
            {
                // 获取患者基本信息
                //string hemoId = GetVal(patientInfo, "HEMODIALYSIS_ID");
                //string name = GetVal(patientInfo, "NAME");
                //string sex = GetVal(patientInfo, "SEX");
                //string age = GetVal(patientInfo, "AGE");

                string ltRecipeTypeName = GetVal(longTermRecipeRow, "RECIPE_TYPE_NAME");
                string ltRecipeDate = GetVal(longTermRecipeRow, "RECIPE_DATE");
                string ltFrequency = GetVal(longTermRecipeRow, "Hemo_Times");
                string ltPurificationMode = GetVal(longTermRecipeRow, "PURIFICATION_MODE_NAME");
                string ltPurifier = GetVal(longTermRecipeRow, "purifier_model");
                string ltDryWeight = GetVal(longTermRecipeRow, "DRY_WEIGHT");
                string ltRemark = GetVal(longTermRecipeRow, "REMARK");

                // 构建数据列表，每条医嘱都携带患者信息和长期方案
                List<RecipeOrderItem> orderList = new List<RecipeOrderItem>();
                if (tempRecipeRows != null)
                {
                    foreach (DataRow row in tempRecipeRows)
                    {
                        orderList.Add(new RecipeOrderItem
                        {
                            // 患者信息
                            //HEMODIALYSIS_ID = hemoId,
                            //NAME = name,
                            //SEX = sex,
                            //AGE = age,
                            
                            RECIPE_TYPE_NAME = GetVal(row, "RECIPE_TYPE_NAME"),
                            PURIFICATION_MODE_NAME = GetVal(row, "PURIFICATION_MODE_NAME"),
                            STATUS_NAME = GetVal(row, "STATUS_NAME"),
                            RECIPE_DATE = GetVal(row, "RECIPE_DATE"),
                            DOCTOR_NAME = GetVal(row, "DOCTOR_NAME"),
                            HEMO_FREQUENCY_TEXT = GetVal(row, "Hemo_Times"),
                            PURIFIER_NAME = GetVal(row, "purifier_model"),
                            DRY_WEIGHT = GetVal(row, "DRY_WEIGHT"),
                            EXECUTE_DATE = GetVal(row, "EXECUTE_DATE"),
                            NURSE_NAME = GetVal(row, "NURSE_NAME"),
                            
                            LT_RECIPE_TYPE_NAME = ltRecipeTypeName,
                            LT_RECIPE_DATE = ltRecipeDate,
                            LT_HEMO_FREQUENCY_TEXT = ltFrequency,
                            LT_PURIFICATION_MODE_NAME = ltPurificationMode,
                            LT_PURIFIER_NAME = ltPurifier,
                            LT_DRY_WEIGHT = ltDryWeight,
                            LT_REMARK = ltRemark
                        });
                    }
                }

                // 设置报表数据源
                this.DataSource = orderList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置打印数据出错：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 安全获取DataRow中的字段值
        /// </summary>
        private string GetVal(DataRow dr, string columnName)
        {
            try
            {
                if (dr != null)
                {
                    // 大小写不敏感匹配列名
                    foreach (DataColumn col in dr.Table.Columns)
                    {
                        if (string.Equals(col.ColumnName, columnName, StringComparison.OrdinalIgnoreCase))
                        {
                            object val = dr[col];
                            return val == null || val == DBNull.Value ? "" : val.ToString().Trim();
                        }
                    }
                }
            }
            catch { }
            return "";
        }
    }
}
