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

namespace Hemo.Client.Doc
{
    /// <summary>
    /// 血液透析病历首页.xaml 的交互逻辑
    /// </summary>
    public partial class 血液透析病历首页 : UserControl
    {
        public 血液透析病历首页()
        {
            InitializeComponent();
            lblHospital.Content = Utilities.Utility.GetHospitalName();
        }

        private PatientModel.MED_PATIENTSRow _patientRow;
        public PatientModel.MED_PATIENTSRow PatientRow
        {
            get
            {
                return _patientRow;
            }
            set
            {
                _patientRow = value;
            }
        }
        /// <summary>
        /// 加载 文档
        /// </summary>
        public void LoadDocumentInfo()
        {
            if (PatientRow != null)
            {
                txtName.Text = PatientRow.IsNAMENull() == true ? string.Empty : PatientRow.NAME;
                var sexTxt = PatientRow.IsSEXNull() == true ? string.Empty : PatientRow.SEX;
                if (sexTxt.Equals("男"))
                {
                    chk0.IsChecked = true;
                    chk1.IsChecked = false;

                }
                else
                {
                    chk0.IsChecked = false;
                    chk1.IsChecked = true;
                }

                txtPatientID.Text = PatientRow.IsPATIENT_IDNull() == true ? string.Empty : PatientRow.PATIENT_ID;
                txtDIAGNOSE.Text = PatientRow.IsDIAGNOSENull() == true ? string.Empty : PatientRow.DIAGNOSE;
            }
        }
    }
}
