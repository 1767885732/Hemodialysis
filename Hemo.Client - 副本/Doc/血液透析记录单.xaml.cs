/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: 患者知情同意书
 * 创建标识:贺建操-2015年1月14日
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace Hemo.Client.Doc
{
    /// <summary>
    /// 血液透析记录单.xaml 的交互逻辑
    /// </summary>
    public partial class 血液透析记录单 : UserControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public 血液透析记录单()
        {
            InitializeComponent();
            lblHospital.Content = Utilities.Utility.GetHospitalName();
        }
        #region 事件
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BindMedicalOrder();
            BindResult();
        }


        #endregion
        #region 方法
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindMedicalOrder()
        {
            DataTable dtMedicalOrder = new DataTable();
            dtMedicalOrder.Columns.Add(new DataColumn("Date"));
            dtMedicalOrder.Columns.Add(new DataColumn("Doctor"));
            dtMedicalOrder.Columns.Add(new DataColumn("OtherOrder"));
            dtMedicalOrder.Columns.Add(new DataColumn("ExecDate"));
            dtMedicalOrder.Columns.Add(new DataColumn("NurseName"));

            DataRow row = dtMedicalOrder.NewRow();
            row["Date"] = "2015-01-20";
            row["Doctor"] = "熊迎春";
            row["OtherOrder"] = "每天一支葡萄糖钙，静脉注射";
            row["ExecDate"] = "2015-01-20";
            row["NurseName"] = "高新庐";
            dtMedicalOrder.Rows.Add(row);

            row = dtMedicalOrder.NewRow();
            row["Date"] = "2015-01-20";
            row["Doctor"] = "熊迎春";
            row["OtherOrder"] = "每天一支葡萄糖钙，静脉注射";
            row["ExecDate"] = "2015-01-20";
            row["NurseName"] = "高新庐";
            dtMedicalOrder.Rows.Add(row);

            row = dtMedicalOrder.NewRow();
            row["Date"] = "2015-01-20";
            row["Doctor"] = "熊迎春";
            row["OtherOrder"] = "每天一支葡萄糖钙，静脉注射";
            row["ExecDate"] = "2015-01-20";
            row["NurseName"] = "高新庐";
            dtMedicalOrder.Rows.Add(row);

            row = dtMedicalOrder.NewRow();
            row["Date"] = "2015-01-20";
            row["Doctor"] = "熊迎春";
            row["OtherOrder"] = "每天一支葡萄糖钙，静脉注射";
            row["ExecDate"] = "2015-01-20";
            row["NurseName"] = "高新庐";
            dtMedicalOrder.Rows.Add(row);

            row = dtMedicalOrder.NewRow();
            row["Date"] = "2015-01-20";
            row["Doctor"] = "熊迎春";
            row["OtherOrder"] = "每天一支葡萄糖钙，静脉注射";
            row["ExecDate"] = "2015-01-20";
            row["NurseName"] = "高新庐";
            dtMedicalOrder.Rows.Add(row);

            List<MedicalOrder> list = new List<MedicalOrder>();
            list.Add(new MedicalOrder() { Date = "2015-01-20", Doctor = "熊迎春", ExecDate = "2015-01-20", NurseName = "高新庐", OtherOrder = "每天一支葡萄糖钙，静脉注射" });
            list.Add(new MedicalOrder() { Date = "2015-01-20", Doctor = "熊迎春", ExecDate = "2015-01-20", NurseName = "高新庐", OtherOrder = "每天一支葡萄糖钙，静脉注射" });
            list.Add(new MedicalOrder() { Date = "2015-01-20", Doctor = "熊迎春", ExecDate = "2015-01-20", NurseName = "高新庐", OtherOrder = "每天一支葡萄糖钙，静脉注射" });
            list.Add(new MedicalOrder() { Date = "2015-01-20", Doctor = "熊迎春", ExecDate = "2015-01-20", NurseName = "高新庐", OtherOrder = "每天一支葡萄糖钙，静脉注射" });
            list.Add(new MedicalOrder() { Date = "2015-01-20", Doctor = "熊迎春", ExecDate = "2015-01-20", NurseName = "高新庐", OtherOrder = "每天一支葡萄糖钙，静脉注射" });

            this.dgMedicalOrder.ItemsSource = dtMedicalOrder.DefaultView;
        }
        /// <summary>
        /// 绑定结果
        /// </summary>
        private void BindResult()
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add(new DataColumn("Date"));
            dtResult.Columns.Add(new DataColumn("BloodPressure"));
            dtResult.Columns.Add(new DataColumn("HeartRate"));
            dtResult.Columns.Add(new DataColumn("BloodRate"));
            dtResult.Columns.Add(new DataColumn("VenousPressure"));
            dtResult.Columns.Add(new DataColumn("TransPressure"));
            dtResult.Columns.Add(new DataColumn("Temperature"));
            dtResult.Columns.Add(new DataColumn("Dehydrate"));
            dtResult.Columns.Add(new DataColumn("DialyzerLevel"));
            dtResult.Columns.Add(new DataColumn("PrimaryNurse"));
            dtResult.Columns.Add(new DataColumn("AuditNurse"));

            DataRow row = dtResult.NewRow();
            row["Date"] = "2015-01-20";
            row["BloodPressure"] = "120";
            row["HeartRate"] = "75";
            row["BloodRate"] = "5";
            row["VenousPressure"] = "100";
            row["TransPressure"] = "80";
            row["Temperature"] = "36.5";
            row["Dehydrate"] = "1.5";
            row["DialyzerLevel"] = "2";
            row["PrimaryNurse"] = "高新庐";
            row["AuditNurse"] = "高新庐";
            dtResult.Rows.Add(row);

            row = dtResult.NewRow();
            row["Date"] = "2015-01-20";
            row["BloodPressure"] = "120";
            row["HeartRate"] = "75";
            row["BloodRate"] = "5";
            row["VenousPressure"] = "100";
            row["TransPressure"] = "80";
            row["Temperature"] = "36.5";
            row["Dehydrate"] = "1.5";
            row["DialyzerLevel"] = "2";
            row["PrimaryNurse"] = "高新庐";
            row["AuditNurse"] = "高新庐";
            dtResult.Rows.Add(row);

            row = dtResult.NewRow();
            row["Date"] = "2015-01-20";
            row["BloodPressure"] = "120";
            row["HeartRate"] = "75";
            row["BloodRate"] = "5";
            row["VenousPressure"] = "100";
            row["TransPressure"] = "80";
            row["Temperature"] = "36.5";
            row["Dehydrate"] = "1.5";
            row["DialyzerLevel"] = "2";
            row["PrimaryNurse"] = "高新庐";
            row["AuditNurse"] = "高新庐";
            dtResult.Rows.Add(row);

            row = dtResult.NewRow();
            row["Date"] = "2015-01-20";
            row["BloodPressure"] = "120";
            row["HeartRate"] = "75";
            row["BloodRate"] = "5";
            row["VenousPressure"] = "100";
            row["TransPressure"] = "80";
            row["Temperature"] = "36.5";
            row["Dehydrate"] = "1.5";
            row["DialyzerLevel"] = "2";
            row["PrimaryNurse"] = "高新庐";
            row["AuditNurse"] = "高新庐";
            dtResult.Rows.Add(row);

            row = dtResult.NewRow();
            row["Date"] = "2015-01-20";
            row["BloodPressure"] = "120";
            row["HeartRate"] = "75";
            row["BloodRate"] = "5";
            row["VenousPressure"] = "100";
            row["TransPressure"] = "80";
            row["Temperature"] = "36.5";
            row["Dehydrate"] = "1.5";
            row["DialyzerLevel"] = "2";
            row["PrimaryNurse"] = "高新庐";
            row["AuditNurse"] = "高新庐";
            dtResult.Rows.Add(row);

            List<Result> list = new List<Result>();
            list.Add(new Result() { AuditNurse = "高新庐", BloodPressure = "120", BloodRate = "5", Date = "2015-01-20", Dehydrate = "1.5", DialyzerLevel = "2", HeartRate = "75", PrimaryNurse = "高新庐", Temperature = "36.5", TransPressure = "80", VenousPressure = "100" });
            list.Add(new Result() { AuditNurse = "高新庐", BloodPressure = "120", BloodRate = "5", Date = "2015-01-20", Dehydrate = "1.5", DialyzerLevel = "2", HeartRate = "75", PrimaryNurse = "高新庐", Temperature = "36.5", TransPressure = "80", VenousPressure = "100" });
            list.Add(new Result() { AuditNurse = "高新庐", BloodPressure = "120", BloodRate = "5", Date = "2015-01-20", Dehydrate = "1.5", DialyzerLevel = "2", HeartRate = "75", PrimaryNurse = "高新庐", Temperature = "36.5", TransPressure = "80", VenousPressure = "100" });
            list.Add(new Result() { AuditNurse = "高新庐", BloodPressure = "120", BloodRate = "5", Date = "2015-01-20", Dehydrate = "1.5", DialyzerLevel = "2", HeartRate = "75", PrimaryNurse = "高新庐", Temperature = "36.5", TransPressure = "80", VenousPressure = "100" });
            list.Add(new Result() { AuditNurse = "高新庐", BloodPressure = "120", BloodRate = "5", Date = "2015-01-20", Dehydrate = "1.5", DialyzerLevel = "2", HeartRate = "75", PrimaryNurse = "高新庐", Temperature = "36.5", TransPressure = "80", VenousPressure = "100" });

            this.dgResult.ItemsSource = dtResult.DefaultView;
        }


        #endregion
    }
    #region 医嘱属性类
    
    /// <summary>
    /// 医嘱 类
    /// </summary>
    public class MedicalOrder
    {
        public string Date { get; set; }
        public string Doctor { get; set; }
        public string OtherOrder { get; set; }
        public string ExecDate { get; set; }
        public string NurseName { get; set; }
    }

    #endregion
    #region 结果类
    
    /// <summary>
    /// 结果类
    /// </summary>
    public class Result
    {
        public string Date { get; set; }
        public string BloodPressure { get; set; }
        public string HeartRate { get; set; }
        public string BloodRate { get; set; }
        public string VenousPressure { get; set; }
        public string TransPressure { get; set; }
        public string Temperature { get; set; }
        public string Dehydrate { get; set; }
        public string DialyzerLevel { get; set; }
        public string PrimaryNurse { get; set; }
        public string AuditNurse { get; set; }
    }
    #endregion

}
