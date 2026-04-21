/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: 患者知情同意书
 * 创建标识:贺建操-2016年5月1日
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
using Hemo.Client.Controls;

namespace Hemo.Client.Doc
{
    /// <summary>
    /// Kolcaba的舒适状况量表.xaml 的交互逻辑
    /// </summary>
    public partial class Kolcaba的舒适状况量表 : UserControl
    {
        public Kolcaba的舒适状况量表()
        {
            InitializeComponent();
        }
        #region 变量

        private CtlUserCureList ctlUserCureList = new CtlUserCureList();
        private PatientKolcabaModel.MED_PATIENT_KOLCABARow _patientRow;
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


        #endregion
        #region 方法
        public PatientKolcabaModel.MED_PATIENT_KOLCABARow PatientRow
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
        public void LoadDocumentInfo()
        {
            if (PatientRow != null)
            {
                if (PatientRow.ITEM1==1)
                {
                    checkBox1.IsChecked = true;
                }
                else if (PatientRow.ITEM1 == 2)
                {
                    checkBox2.IsChecked = true;
                }
                else if (PatientRow.ITEM1 == 3)
                {
                    checkBox3.IsChecked = true;
                }
                else if (PatientRow.ITEM1 == 4)
                {
                    checkBox4.IsChecked = true;
                }

                if (PatientRow.ITEM2 == 1)
                {
                    checkBox8.IsChecked = true;
                }
                else if (PatientRow.ITEM2 == 2)
                {
                    checkBox7.IsChecked = true;
                }
                else if (PatientRow.ITEM2 == 3)
                {
                    checkBox6.IsChecked = true;
                }
                else if (PatientRow.ITEM2 == 4)
                {
                    checkBox4.IsChecked = true;
                }

                if (PatientRow.ITEM3 == 1)
                {
                    checkBox12.IsChecked = true;
                }
                else if (PatientRow.ITEM3 == 2)
                {
                    checkBox11.IsChecked = true;
                }
                else if (PatientRow.ITEM3 == 3)
                {
                    checkBox10.IsChecked = true;
                }
                else if (PatientRow.ITEM3 == 4)
                {
                    checkBox9.IsChecked = true;
                }

                if (PatientRow.ITEM4 == 1)
                {
                    checkBox13.IsChecked = true;
                }
                else if (PatientRow.ITEM4 == 2)
                {
                    checkBox14.IsChecked = true;
                }
                else if (PatientRow.ITEM4 == 3)
                {
                    checkBox15.IsChecked = true;
                }
                else if (PatientRow.ITEM4 == 4)
                {
                    checkBox16.IsChecked = true;
                }

                if (PatientRow.ITEM5 == 1)
                {
                    checkBox17.IsChecked = true;
                }
                else if (PatientRow.ITEM5 == 2)
                {
                    checkBox18.IsChecked = true;
                }
                else if (PatientRow.ITEM5 == 3)
                {
                    checkBox19.IsChecked = true;
                }
                else if (PatientRow.ITEM5 == 4)
                {
                    checkBox20.IsChecked = true;
                }

                if (PatientRow.ITEM6 == 1)
                {
                    checkBox21.IsChecked = true;
                }
                else if (PatientRow.ITEM6 == 2)
                {
                    checkBox22.IsChecked = true;
                }
                else if (PatientRow.ITEM6 == 3)
                {
                    checkBox23.IsChecked = true;
                }
                else if (PatientRow.ITEM6 == 4)
                {
                    checkBox24.IsChecked = true;
                }

                if (PatientRow.ITEM7 == 1)
                {
                    checkBox28.IsChecked = true;
                }
                else if (PatientRow.ITEM7 == 2)
                {
                    checkBox27.IsChecked = true;
                }
                else if (PatientRow.ITEM7 == 3)
                {
                    checkBox26.IsChecked = true;
                }
                else if (PatientRow.ITEM7 == 4)
                {
                    checkBox25.IsChecked = true;
                }

                if (PatientRow.ITEM8 == 1)
                {
                    checkBox32.IsChecked = true;
                }
                else if (PatientRow.ITEM8 == 2)
                {
                    checkBox31.IsChecked = true;
                }
                else if (PatientRow.ITEM8 == 3)
                {
                    checkBox30.IsChecked = true;
                }
                else if (PatientRow.ITEM8 == 4)
                {
                    checkBox29.IsChecked = true;
                }

                if (PatientRow.ITEM9 == 1)
                {
                    checkBox36.IsChecked = true;
                }
                else if (PatientRow.ITEM9 == 2)
                {
                    checkBox35.IsChecked = true;
                }
                else if (PatientRow.ITEM9 == 3)
                {
                    checkBox34.IsChecked = true;
                }
                else if (PatientRow.ITEM9 == 4)
                {
                    checkBox33.IsChecked = true;
                }

                if (PatientRow.ITEM10 == 1)
                {
                    checkBox40.IsChecked = true;
                }
                else if (PatientRow.ITEM10 == 2)
                {
                    checkBox39.IsChecked = true;
                }
                else if (PatientRow.ITEM10 == 3)
                {
                    checkBox38.IsChecked = true;
                }
                else if (PatientRow.ITEM10 == 4)
                {
                    checkBox37.IsChecked = true;
                }

                if (PatientRow.ITEM11 == 1)
                {
                    checkBox44.IsChecked = true;
                }
                else if (PatientRow.ITEM11 == 2)
                {
                    checkBox43.IsChecked = true;
                }
                else if (PatientRow.ITEM11 == 3)
                {
                    checkBox42.IsChecked = true;
                }
                else if (PatientRow.ITEM11 == 4)
                {
                    checkBox41.IsChecked = true;
                }

                if (PatientRow.ITEM12 == 1)
                {
                    checkBox48.IsChecked = true;
                }
                else if (PatientRow.ITEM12 == 2)
                {
                    checkBox47.IsChecked = true;
                }
                else if (PatientRow.ITEM12 == 3)
                {
                    checkBox46.IsChecked = true;
                }
                else if (PatientRow.ITEM12 == 4)
                {
                    checkBox45.IsChecked = true;
                }

                if (PatientRow.ITEM13 == 1)
                {
                    checkBox52.IsChecked = true;
                }
                else if (PatientRow.ITEM13 == 2)
                {
                    checkBox51.IsChecked = true;
                }
                else if (PatientRow.ITEM13 == 3)
                {
                    checkBox50.IsChecked = true;
                }
                else if (PatientRow.ITEM13 == 4)
                {
                    checkBox49.IsChecked = true;
                }

                if (PatientRow.ITEM14 == 1)
                {
                    checkBox56.IsChecked = true;
                }
                else if (PatientRow.ITEM14 == 2)
                {
                    checkBox55.IsChecked = true;
                }
                else if (PatientRow.ITEM14 == 3)
                {
                    checkBox54.IsChecked = true;
                }
                else if (PatientRow.ITEM14 == 4)
                {
                    checkBox53.IsChecked = true;
                }

                if (PatientRow.ITEM15 == 1)
                {
                    checkBox60.IsChecked = true;
                }
                else if (PatientRow.ITEM15 == 2)
                {
                    checkBox59.IsChecked = true;
                }
                else if (PatientRow.ITEM15 == 3)
                {
                    checkBox58.IsChecked = true;
                }
                else if (PatientRow.ITEM15 == 4)
                {
                    checkBox57.IsChecked = true;
                }

                if (PatientRow.ITEM16 == 1)
                {
                    checkBox64.IsChecked = true;
                }
                else if (PatientRow.ITEM16 == 2)
                {
                    checkBox63.IsChecked = true;
                }
                else if (PatientRow.ITEM16 == 3)
                {
                    checkBox62.IsChecked = true;
                }
                else if (PatientRow.ITEM16 == 4)
                {
                    checkBox61.IsChecked = true;
                }

                if (PatientRow.ITEM17 == 1)
                {
                    checkBox65.IsChecked = true;
                }
                else if (PatientRow.ITEM17 == 2)
                {
                    checkBox66.IsChecked = true;
                }
                else if (PatientRow.ITEM17 == 3)
                {
                    checkBox67.IsChecked = true;
                }
                else if (PatientRow.ITEM17 == 4)
                {
                    checkBox68.IsChecked = true;
                }

                if (PatientRow.ITEM18 == 1)
                {
                    checkBox72.IsChecked = true;
                }
                else if (PatientRow.ITEM18 == 2)
                {
                    checkBox71.IsChecked = true;
                }
                else if (PatientRow.ITEM18 == 3)
                {
                    checkBox70.IsChecked = true;
                }
                else if (PatientRow.ITEM18 == 4)
                {
                    checkBox69.IsChecked = true;
                }

                if (PatientRow.ITEM19 == 1)
                {
                    checkBox73.IsChecked = true;
                }
                else if (PatientRow.ITEM19 == 2)
                {
                    checkBox74.IsChecked = true;
                }
                else if (PatientRow.ITEM19 == 3)
                {
                    checkBox75.IsChecked = true;
                }
                else if (PatientRow.ITEM19 == 4)
                {
                    checkBox76.IsChecked = true;
                }

                if (PatientRow.ITEM20 == 1)
                {
                    checkBox80.IsChecked = true;
                }
                else if (PatientRow.ITEM20 == 2)
                {
                    checkBox79.IsChecked = true;
                }
                else if (PatientRow.ITEM20 == 3)
                {
                    checkBox78.IsChecked = true;
                }
                else if (PatientRow.ITEM20 == 4)
                {
                    checkBox77.IsChecked = true;
                }

                if (PatientRow.ITEM21 == 1)
                {
                    checkBox84.IsChecked = true;
                }
                else if (PatientRow.ITEM21 == 2)
                {
                    checkBox83.IsChecked = true;
                }
                else if (PatientRow.ITEM21 == 3)
                {
                    checkBox82.IsChecked = true;
                }
                else if (PatientRow.ITEM21 == 4)
                {
                    checkBox81.IsChecked = true;
                }

                if (PatientRow.ITEM22 == 1)
                {
                    checkBox85.IsChecked = true;
                }
                else if (PatientRow.ITEM22 == 2)
                {
                    checkBox86.IsChecked = true;
                }
                else if (PatientRow.ITEM22 == 3)
                {
                    checkBox87.IsChecked = true;
                }
                else if (PatientRow.ITEM22 == 4)
                {
                    checkBox88.IsChecked = true;
                }

                if (PatientRow.ITEM23 == 1)
                {
                    checkBox89.IsChecked = true;
                }
                else if (PatientRow.ITEM23 == 2)
                {
                    checkBox90.IsChecked = true;
                }
                else if (PatientRow.ITEM23 == 3)
                {
                    checkBox91.IsChecked = true;
                }
                else if (PatientRow.ITEM23 == 4)
                {
                    checkBox92.IsChecked = true;
                }

                if (PatientRow.ITEM24 == 1)
                {
                    checkBox96.IsChecked = true;
                }
                else if (PatientRow.ITEM24 == 2)
                {
                    checkBox95.IsChecked = true;
                }
                else if (PatientRow.ITEM24 == 3)
                {
                    checkBox94.IsChecked = true;
                }
                else if (PatientRow.ITEM24 == 4)
                {
                    checkBox93.IsChecked = true;
                }

                if (PatientRow.ITEM25 == 1)
                {
                    checkBox100.IsChecked = true;
                }
                else if (PatientRow.ITEM25 == 2)
                {
                    checkBox99.IsChecked = true;
                }
                else if (PatientRow.ITEM25 == 3)
                {
                    checkBox98.IsChecked = true;
                }
                else if (PatientRow.ITEM25 == 4)
                {
                    checkBox97.IsChecked = true;
                }

                if (PatientRow.ITEM26 == 1)
                {
                    checkBox101.IsChecked = true;
                }
                else if (PatientRow.ITEM26 == 2)
                {
                    checkBox102.IsChecked = true;
                }
                else if (PatientRow.ITEM26 == 3)
                {
                    checkBox103.IsChecked = true;
                }
                else if (PatientRow.ITEM26 == 4)
                {
                    checkBox104.IsChecked = true;
                }

                if (PatientRow.ITEM27 == 1)
                {
                    checkBox108.IsChecked = true;
                }
                else if (PatientRow.ITEM27 == 2)
                {
                    checkBox107.IsChecked = true;
                }
                else if (PatientRow.ITEM27 == 3)
                {
                    checkBox106.IsChecked = true;
                }
                else if (PatientRow.ITEM27 == 4)
                {
                    checkBox105.IsChecked = true;
                }

                if (PatientRow.ITEM28 == 1)
                {
                    checkBox109.IsChecked = true;
                }
                else if (PatientRow.ITEM28 == 2)
                {
                    checkBox110.IsChecked = true;
                }
                else if (PatientRow.ITEM28 == 3)
                {
                    checkBox111.IsChecked = true;
                }
                else if (PatientRow.ITEM28 == 4)
                {
                    checkBox112.IsChecked = true;
                }
                txtName.Text = _pName;
                txtDate.Text = PatientRow.IsLASTUPDATEDATENull() == true ? string.Empty : PatientRow.LASTUPDATEDATE.ToString("yyyy/MM/dd");
                txtKolcaba.Text = PatientRow.IsLASTUPDATEDATENull() == true ? string.Empty : PatientRow.TOTALSCORE.ToString();
            }
        }

        #endregion
    }
}
