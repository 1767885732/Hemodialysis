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

namespace Hemo.Client.Doc {
    /// <summary>
    /// 血液净化治疗知情同意书.xaml 的交互逻辑
    /// </summary>
    public partial class 血液净化治疗知情同意书 : UserControl {
        /// <summary>
        /// 病人姓名
        /// </summary>
        public string PatientName {
            get;
            set;
        }
        /// <summary>
        /// 住院号
        /// </summary>
        public string AdmissionNumber {
            get;
            set;
        }
        /// <summary>
        /// 诊断
        /// </summary>
        public string Diagnose {
            get;
            set;
        }

        /// <summary>
        /// 同意书签字
        /// </summary>
        public byte[] BookPicture {
            get;
            set;
        }

        public 血液净化治疗知情同意书() {
            InitializeComponent();
            lblHospital.Content = Utilities.Utility.GetHospitalName();
        }

        public PatientModel.MED_PATIENTSRow PatientRow {
            set;
            get;
        }

        /// <summary>
        /// 血管通路
        /// </summary>
        public string VASCULAR_ACCESS {
            set;
            get;
        }
        /// <summary>
        /// 加载 数据
        /// </summary>
        private void Grid_Loaded(object sender, RoutedEventArgs e) {
            txtName.Text = this.PatientRow.IsNAMENull() ? string.Empty : this.PatientRow.NAME;
            txtDIAGNOSE.Text = this.PatientRow.IsDIAGNOSENull() ? string.Empty : this.PatientRow.DIAGNOSE;
            txtSex.Text = this.PatientRow.IsSEXNull() ? string.Empty : this.PatientRow.SEX;
            txtAge.Text = this.PatientRow.IsAGENull() ? string.Empty : Utilities.Utility.CDecimal(this.PatientRow.AGE.ToString()).ToString();
            txtDeptCode.Text = this.PatientRow.IsPATIENT_IDNull() ? string.Empty : this.PatientRow.PATIENT_ID;
            //txtHemoID.Text = this.PatientRow.HEMODIALYSIS_ID;
            txtVASCULAR_ACCESS.Text = VASCULAR_ACCESS;
            if (BookPicture != null && BookPicture.Length>0)
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
            //  lblDate.Content = DateTime.Now.Year.ToString() + "  年  " + DateTime.Now.Month.ToString() + "  月  " + DateTime.Now.Day.ToString()+"  日";
        }
    }
}
