/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: 患者知情同意书
 * 创建标识:贺建操-2017年2月23日
 * ----------------------------------------------------------------*/
using System.Windows.Controls;
using Hemo.Model;
using Hemo.Utilities;

namespace Hemo.Client.Doc
{
    /// <summary>
    /// 枸橼酸抗凝同意书.xaml 的交互逻辑
    /// </summary>
    public partial class 内瘘手术同意书 : UserControl
    {
        public 内瘘手术同意书()
        {
            this.InitializeComponent();
            lblHospital.Content = Utilities.Utility.GetHospitalName();
        }

        public PatientModel.MED_PATIENTSRow PatientRow
        {
            set;
            get;
        }
        /// <summary>
        /// 加载 文档
        /// </summary>
        public void LoadDocumentInfo()
        {
            if (this.PatientRow != null)
            {
                this.txtName.Text = this.PatientRow.IsNAMENull() == true ? string.Empty : this.PatientRow.NAME;
                this.txtSex.Text = this.PatientRow.IsSEXNull() == true ? string.Empty : this.PatientRow.SEX;
                this.txtAge.Text = this.PatientRow.IsAGENull() == true ? string.Empty : Utility.CInt(this.PatientRow.AGE.ToString()).ToString();
                this.txtDeptCode.Text = this.PatientRow.IsWHAT_DEPARTMENT_INNull() == true ? string.Empty : this.PatientRow.WHAT_DEPARTMENT_IN;
                this.txtBedNum.Text = this.PatientRow.IsBED_NONull() == true ? string.Empty : this.PatientRow.BED_NO;
                this.txtPATIENTID.Text = this.PatientRow.IsPATIENT_IDNull() == true ? string.Empty : this.PatientRow.PATIENT_ID;
                this.txtAdmissionNumber.Text = this.PatientRow.IsADMISSION_NUMBERNull() == true ? string.Empty : this.PatientRow.ADMISSION_NUMBER;
            }
        }
    }
}
