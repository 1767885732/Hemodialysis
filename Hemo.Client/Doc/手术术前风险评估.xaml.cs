/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: 患者知情同意书
 * 创建标识:贺建操-2016年2月30日
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
using Hemo.Utilities;

namespace Hemo.Client.Doc {
    /// <summary>
    /// 手术安全核查表1.xaml 的交互逻辑
    /// </summary>
    public partial class 手术术前风险评估 : UserControl {
        public 手术术前风险评估() {
            InitializeComponent();
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
        /// 加载 文档
        /// </summary>
        public void LoadDocumentInfo() {
            if (this.PatientRow != null) {
                this.txtName.Text = this.PatientRow.IsNAMENull() == true ? string.Empty : this.PatientRow.NAME;
                this.txtSex.Text = this.PatientRow.IsSEXNull() == true ? string.Empty : this.PatientRow.SEX;
                this.txtAge.Text = this.PatientRow.IsAGENull() == true ? string.Empty : Utility.CInt(this.PatientRow.AGE.ToString()).ToString();
                this.txtDeptCode.Text = this.PatientRow.IsWHAT_DEPARTMENT_INNull() == true ? string.Empty : this.PatientRow.WHAT_DEPARTMENT_IN;
                this.txtHemoID.Text = PatientRow.HEMODIALYSIS_ID;
            }
        }
    }
}
