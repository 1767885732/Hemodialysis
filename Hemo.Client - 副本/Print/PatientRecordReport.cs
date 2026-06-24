/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：患者病历记录报表
// 创建时间：2016-05-16
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.Print {
    public partial class PatientRecordReport : XtraReport
    {
        #region 变量属性

        private string _name;

        public string Name
        {
            private get { return _name; }
            set
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    _name = value;
                    this.xrlab_name.Text = _name;
                }
            }
        }

        private string _age;

        public string Age
        {
            private get { return this._age; }
            set
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    _age = value;
                    this.xrLab_AGE.Text = _age;
                }
            }
        }

        private string _sex;

        public string Sex
        {
            private get { return _sex; }
            set
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    _sex = value;
                    this.xrLab_SEX.Text = _sex;
                }
            }
        }

        private string _hemoID;

        public string HemoID
        {
            private get { return _hemoID; }
            set
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    _hemoID = value;
                    this.xrLab_HemoId.Text = _hemoID;
                }
            }
        }

        private string _patientRecord;

        public string PatientRecord
        {
            private get { return _patientRecord; }
            set
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    _patientRecord = value;
                    this.xrRichText_PatientRecord.Text = _patientRecord;
                }

            }
        }

        #endregion

        #region 构造函数

        public PatientRecordReport()
        {
            InitializeComponent();
            xrLabel1.Text = Utilities.Utility.GetHospitalName() + "患者病历";
        }

        #endregion
    }
}
