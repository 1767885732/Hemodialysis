/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: 患者知情同意书
 * 创建标识:贺建操-2016年5月13日
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
using Hemo.Model;
using Hemo.IService;
using Hemo.IService.Config;
using Hemo.Service;
using System.IO;


namespace Hemo.Client.Doc {

    /// <summary>
    /// 动静脉内瘘血管吻合术同意书.xaml 的交互逻辑
    /// </summary>
    public partial class 动静脉内瘘血管吻合术同意书 : UserControl {

        private IConfig _configService = ServiceManager.Instance.ConfigService;

        public 动静脉内瘘血管吻合术同意书() {
            InitializeComponent();
            lblHospital.Content = Utilities.Utility.GetHospitalName() ;
        }
        #region 变量
        private PatientModel.MED_PATIENTSRow _patientRow;
        public PatientModel.MED_PATIENTSRow PatientRow {
            get {
                return _patientRow;
            }
            set {
                _patientRow = value;
            }
        }

        /// <summary>
        /// 同意书签字
        /// </summary>
        public byte[] BookPicture
        {
            get;
            set;
        }


        #endregion
        #region 方法
        public void LoadDocumentInfo() {
            if (PatientRow != null) {
                txtWhatDepartmentID.Text = PatientRow.IsWHAT_DEPARTMENT_INNull() == true ? string.Empty : PatientRow.WHAT_DEPARTMENT_IN;
                txtName.Text = PatientRow.IsNAMENull() == true ? string.Empty : PatientRow.NAME;
                txtSex.Text = PatientRow.IsSEXNull() == true ? string.Empty : PatientRow.SEX;
                txtAge.Text = PatientRow.IsAGENull() == true ? string.Empty : Utilities.Utility.CInt(PatientRow.AGE.ToString()).ToString();
                txtAdmissionNumber.Text = PatientRow.IsADMISSION_NUMBERNull() == true ? string.Empty : PatientRow.ADMISSION_NUMBER;
            }

            if (BookPicture != null && BookPicture.Length > 0)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new MemoryStream(BookPicture);
                image.EndInit();
                if (image.Width <= imgPatientSign.Width && image.Height <= imgPatientSign.Height)
                {
                    imgPatientSign.Stretch = Stretch.None;
                }
                else
                {
                    imgPatientSign.Stretch = Stretch.Uniform;
                }
                imgPatientSign.Source = image;
            }
            //DataTable dtHospital = _configService.GetConfigList(string.Empty, string.Empty, "科室名称", "1");
            //if (dtHospital != null && dtHospital.Rows.Count > 0) {
            //    lblHospital.Content = dtHospital.Rows[0]["ITEM_NAME"].ToString();
            //}
        }

        private void txtOperationType_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("type"));
            DataRow dr = dt.NewRow();
            dr[0] = "手术方式1";
            dt.Rows.Add(dr);

            //dataGrid1.Visibility = System.Windows.Visibility.Visible;
            //dataGrid1.ItemsSource = dt.DefaultView;
        }


        #endregion
    }


}
