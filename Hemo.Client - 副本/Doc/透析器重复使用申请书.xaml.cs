/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: 患者知情同意书
 * 创建标识:贺建操-2016年2月21日
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

namespace Hemo.Client.Doc {
    /// <summary>
    /// 透析器重复使用申请书.xaml 的交互逻辑
    /// </summary>
    public partial class 透析器重复使用申请书 : UserControl {
        public 透析器重复使用申请书() {
            InitializeComponent();
            lblHospital.Content = Utilities.Utility.GetHospitalName();
        }
        /// <summary>
        /// 病人信息
        /// </summary>
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
        /// 加载 文档
        /// </summary>
        public void LoadDocumentInfo() {
            if (PatientRow != null) {
                txtName.Text = PatientRow.IsNAMENull() == true ? string.Empty : PatientRow.NAME;
                txtTelephone.Text = PatientRow.IsTELEPHONENull() == true ? string.Empty : PatientRow.TELEPHONE;
                txtName.Text = PatientRow.IsNAMENull() == true ? string.Empty : PatientRow.NAME;
                txtSex.Text = PatientRow.IsSEXNull() == true ? string.Empty : PatientRow.SEX;
                txtAge.Text = PatientRow.IsAGENull() == true ? string.Empty : Utilities.Utility.CInt(PatientRow.AGE.ToString()).ToString();
                txtAddress.Text = PatientRow.IsADDRESSNull() == true ? string.Empty : PatientRow.ADDRESS;
            }
        }
    }
}
