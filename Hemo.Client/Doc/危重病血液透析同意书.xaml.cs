/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: 患者知情同意书
 * 创建标识:贺建操-2016年2月25日
 * ----------------------------------------------------------------*/
using System.Windows.Controls;
using Hemo.Model;
using Hemo.Utilities;

namespace Hemo.Client.Doc
{
    /// <summary>
    /// 枸橼酸抗凝同意书.xaml 的交互逻辑
    /// </summary>
    public partial class 危重病血液透析同意书 : UserControl
    {
        public 危重病血液透析同意书()
        {
            this.InitializeComponent();
            lblHospital.Content = Utilities.Utility.GetHospitalName();
            lblHosptial1.Content = Utilities.Utility.GetHospitalName();
        }
        /// <summary>
        /// 病人信息
        /// </summary>
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
            //if (this.PatientRow != null)
            //{
            //    this.txtName.Text = this.PatientRow.IsNAMENull() == true ? string.Empty : this.PatientRow.NAME;
            //    this.txtSex.Text = this.PatientRow.IsSEXNull() == true ? string.Empty : this.PatientRow.SEX;
            //    this.txtAge.Text = this.PatientRow.IsAGENull() == true ? string.Empty : Utility.CInt(this.PatientRow.AGE.ToString()).ToString();
            //    this.txtAdmissionNumber.Text = this.PatientRow.IsADMISSION_NUMBERNull() == true ? string.Empty : this.PatientRow.ADMISSION_NUMBER;
            //}
        }
    }
}
