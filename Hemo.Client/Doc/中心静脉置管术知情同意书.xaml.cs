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
using Hemo.Model;
using System.IO;

namespace Hemo.Client.Doc
{
    /// <summary>
    /// 中心静脉置管术知情同意书.xaml 的交互逻辑
    /// </summary>
    public partial class 中心静脉置管术知情同意书 : UserControl
    {
        public 中心静脉置管术知情同意书()
        {
            InitializeComponent();
            lblHospital.Content = Utilities.Utility.GetHospitalName();
        }

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
        /// <summary>
        /// 加载 文档
        /// </summary>
        public void LoadDocumentInfo() {
            if (PatientRow != null) {
                txtName.Text = PatientRow.IsNAMENull() == true ? string.Empty : PatientRow.NAME;
                txtBedNum.Text = PatientRow.IsBED_NONull() == true ? string.Empty : PatientRow.BED_NO;
                txtDeptCode.Text = PatientRow.IsWHAT_DEPARTMENT_INNull() == true ? string.Empty : PatientRow.WHAT_DEPARTMENT_IN;
                txtSex.Text = PatientRow.IsSEXNull() == true ? string.Empty : PatientRow.SEX;
                txtAge.Text = PatientRow.IsAGENull() == true ? string.Empty : Utilities.Utility.CInt(PatientRow.AGE.ToString()).ToString();
           //     txtAdmissionNumber.Text = PatientRow.IsADMISSION_NUMBERNull() == true ? string.Empty : PatientRow.ADMISSION_NUMBER;
                txtHemoID.Text = PatientRow.HEMODIALYSIS_ID;
                txtDIAGNOSE.Text = PatientRow.IsDIAGNOSENull() ? string.Empty : PatientRow.DIAGNOSE;
                // lblDIAGNOSE.Content = PatientRow.IsDIAGNOSENull() == true ? string.Empty : PatientRow.DIAGNOSE;
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
        }
    }
}
