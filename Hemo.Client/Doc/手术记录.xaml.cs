using System.Windows.Controls;
using Hemo.Model;
using Hemo.Utilities;

namespace Hemo.Client.Doc
{
    /// <summary>
    /// 枸橼酸抗凝同意书.xaml 的交互逻辑
    /// </summary>
    public partial class 手术记录 : UserControl
    {
        public 手术记录()
        {
            this.InitializeComponent();
            lblHospital.Content = Utilities.Utility.GetHospitalName();
        }

        public PatientModel.MED_PATIENTSRow PatientRow
        {
            set;
            get;
        }

        public void LoadDocumentInfo()
        {
            if (this.PatientRow != null)
            {
                this.txtName.Text = this.PatientRow.IsNAMENull() == true ? string.Empty : this.PatientRow.NAME;
                this.txtSex.Text = this.PatientRow.IsSEXNull() == true ? string.Empty : this.PatientRow.SEX;
                this.txtAge.Text = this.PatientRow.IsAGENull() == true ? string.Empty : Utility.CInt(this.PatientRow.AGE.ToString()).ToString();
                this.txtPATIENTID.Text = this.PatientRow.IsPATIENT_IDNull() == true ? string.Empty : this.PatientRow.PATIENT_ID;
            }
        }
    }
}
