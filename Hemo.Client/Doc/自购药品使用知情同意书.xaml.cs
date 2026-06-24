/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: 患者知情同意书
 * 创建标识:贺建操-2015年1月14日
 * ----------------------------------------------------------------*/
using System.Windows.Controls;
using Hemo.Model;
using Hemo.Utilities;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media;

namespace Hemo.Client.Doc
{
    /// <summary>
    /// 自购药品使用知情同意书.xaml 的交互逻辑
    /// </summary>
    public partial class 自购药品使用知情同意书 : UserControl
    {
        public 自购药品使用知情同意书()
        {
            this.InitializeComponent();
            
        }
        /// <summary>
        /// 病人往上
        /// </summary>
        public PatientModel.MED_PATIENTSRow PatientRow
        {
            set;
            get;
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
        public void LoadDocumentInfo()
        {
            if (this.PatientRow != null)
            {
                //this.txtName.Content = this.PatientRow.IsNAMENull() == true ? string.Empty : this.PatientRow.NAME;
                this.txtPATIENTID.Content = this.PatientRow.IsPATIENT_IDNull() == true ? string.Empty : this.PatientRow.PATIENT_ID;
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
