/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: 患者知情同意书
 * 创建标识:贺建操-2016年4月13日
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
    /// 透析充分性评估.xaml 的交互逻辑
    /// </summary>
    public partial class 透析充分性评估 : UserControl
    {
        #region 变量
        
        private PatientKolcabaModel.MED_PATIENT_SUFFICIENCYRow _patientRow;
        private string _pName = string.Empty;
        public string Pname
        {
            get
            {
                return _pName;
            }
            set
            {
                _pName = value;
            }
        }
        public PatientKolcabaModel.MED_PATIENT_SUFFICIENCYRow PatientRow
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

        #endregion
        public 透析充分性评估()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 加载 文档
        /// </summary>
        public void LoadDocumentInfo()
        {
            if (PatientRow != null)
            {
                txtName.Text = _pName;
                txtDate.Text = PatientRow.IsLASTUPDATEDATENull() == true ? string.Empty : PatientRow.LASTUPDATEDATE.ToString("yyyy/MM/dd");
                txtID.Text = PatientRow.IsHEMODIALYSIS_IDNull() == true ? string.Empty : PatientRow.HEMODIALYSIS_ID.ToString();
                SUFFICIENCY.Text = PatientRow.IsSUFFICIENCYNull() == true ? string.Empty : PatientRow.SUFFICIENCY.ToString();
                PLAN.Text = PatientRow.IsPLANNull() == true ? string.Empty : PatientRow.PLAN.ToString();
                DRUG.Text = PatientRow.IsDRUGNull() == true ? string.Empty : PatientRow.DRUG.ToString();
                if (PatientRow.INDIALYSISBP == 0)
                {
                    INDIALYSISBP0.IsChecked = true;
                }
                else if (PatientRow.INDIALYSISBP == 1)
                {
                    INDIALYSISBP1.IsChecked = true;
                }
                else if (PatientRow.INDIALYSISBP == 2)
                {
                    INDIALYSISBP2.IsChecked = true;
                }


                if (PatientRow.DIALYSISBP == 0)
                {
                    DIALYSISBP0.IsChecked = true;
                }
                else if (PatientRow.DIALYSISBP == 1)
                {
                    DIALYSISBP1.IsChecked = true;
                }
                else if (PatientRow.DIALYSISBP == 2)
                {
                    DIALYSISBP2.IsChecked = true;
                }


                if (PatientRow.HEART == 0)
                {
                    HEART0.IsChecked = true;
                }
                else if (PatientRow.HEART == 1)
                {
                    HEART1.IsChecked = true;
                }
                else if (PatientRow.HEART == 2)
                {
                    HEART2.IsChecked = true;
                }
                else if (PatientRow.HEART == 3)
                {
                    HEART3.IsChecked = true;
                }


                if (PatientRow.EDEMA == 0)
                {
                    EDEMA0.IsChecked = true;
                }
                else if (PatientRow.EDEMA == 1)
                {
                    EDEMA1.IsChecked = true;
                }
                else if (PatientRow.EDEMA == 2)
                {
                    EDEMA2.IsChecked = true;
                }
                else if (PatientRow.EDEMA == 3)
                {
                    EDEMA3.IsChecked = true;
                }


                if (PatientRow.WEIGHTADD == 0)
                {
                    WEIGHTADD0.IsChecked = true;
                }
                else if (PatientRow.WEIGHTADD == 1)
                {
                    WEIGHTADD1.IsChecked = true;
                }


                if (PatientRow.NUTRITION == 0)
                {
                    NUTRITION0.IsChecked = true;
                }
                else if (PatientRow.NUTRITION == 1)
                {
                    NUTRITION1.IsChecked = true;
                }
                else if (PatientRow.NUTRITION == 2)
                {
                    NUTRITION2.IsChecked = true;
                }


                if (PatientRow.SKIN == 0)
                {
                    SKIN0.IsChecked = true;
                }
                else if (PatientRow.SKIN == 1)
                {
                    SKIN1.IsChecked = true;
                }


                if (PatientRow.BONEPAIN == 0)
                {
                    BONEPAIN0.IsChecked = true;
                }
                else if (PatientRow.BONEPAIN == 1)
                {
                    BONEPAIN1.IsChecked = true;
                }


                if (PatientRow.SLEEP == 0)
                {
                    SLEEP0.IsChecked = true;
                }
                else if (PatientRow.SLEEP == 1)
                {
                    SLEEP1.IsChecked = true;
                }
                //加下边这个判断，因为当用户输入的时候没有输入内容这边就会报错啦。
                if (PatientRow.IsCOMPLICATIONNull())
                {
                    COMPLICATION1.IsChecked = true;
                    txtCOMPLICATION.Text = string.Empty;
                }
                else
                {
                    if (PatientRow.COMPLICATION == "0")
                    {
                        COMPLICATION0.IsChecked = true;
                    }
                    else
                    {
                        COMPLICATION1.IsChecked = true;
                        txtCOMPLICATION.Text =  PatientRow.COMPLICATION;
                    }
                }


                if (PatientRow.ANEMIA == 0)
                {
                    ANEMIA0.IsChecked = true;
                }
                else if (PatientRow.ANEMIA == 1)
                {
                    ANEMIA1.IsChecked = true;
                }
                else if (PatientRow.ANEMIA == 2)
                {
                    ANEMIA2.IsChecked = true;
                }
                else if (PatientRow.ANEMIA == 3)
                {
                    ANEMIA3.IsChecked = true;
                }


                if (PatientRow.DISORDER == 0)
                {
                    DISORDER0.IsChecked = true;
                }
                else if (PatientRow.DISORDER == 1)
                {
                    DISORDER1.IsChecked = true;
                }


                if (PatientRow.P == 0)
                {
                    P0.IsChecked = true;
                }
                else if (PatientRow.P == 1)
                {
                    P1.IsChecked = true;
                }
                else if (PatientRow.P == 2)
                {
                    P2.IsChecked = true;
                }


                if (PatientRow.CA == 0)
                {
                    CA0.IsChecked = true;
                }
                else if (PatientRow.CA == 1)
                {
                    CA1.IsChecked = true;
                }
                else if (PatientRow.CA == 2)
                {
                    CA2.IsChecked = true;
                }


                if (PatientRow.IPTH == 0)
                {
                    IPTH0.IsChecked = true;
                }
                else if (PatientRow.IPTH == 1)
                {
                    IPTH1.IsChecked = true;
                }
                else if (PatientRow.IPTH == 2)
                {
                    IPTH2.IsChecked = true;
                }


                if (PatientRow.KTV == 0)
                {
                    KTV0.IsChecked = true;
                }
                else if (PatientRow.KTV == 1)
                {
                    KTV1.IsChecked = true;
                }
                else if (PatientRow.KTV == 2)
                {
                    KTV2.IsChecked = true;
                }


                if (PatientRow.URR == 0)
                {
                    URR0.IsChecked = true;
                }
                else if (PatientRow.URR == 1)
                {
                    URR1.IsChecked = true;
                }
                else if (PatientRow.URR == 2)
                {
                    URR2.IsChecked = true;
                }
            }
        }
    }
}
