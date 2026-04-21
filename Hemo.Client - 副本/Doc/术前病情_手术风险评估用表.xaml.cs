/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: 患者知情同意书
 * 创建标识:贺建操-2016年3月30日
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
    /// 术前病情_手术风险评估用表.xaml 的交互逻辑
    /// </summary>
    public partial class 术前病情_手术风险评估用表 : UserControl {
        public 术前病情_手术风险评估用表() {
            InitializeComponent();
        }
        #region 变量
        
        private PatientModel.MED_PATIENTSRow _patientRow;
        public PatientModel.MED_PATIENTSRow PatientRow {
            get {
                return _patientRow;
            }
            set {
                _patientRow = value;
            }
        }

        #endregion

    }
}
