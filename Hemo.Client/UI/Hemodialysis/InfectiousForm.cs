/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:传染病
 * 创建标识:吕志强-2013年7月30日
 * 
 * 修改时间:2013年11月7日
 * 修改人:贺建操
 * 修改描述:新增方法
 * 
 * 修改时间:2014年2月15日
 * 修改人:顾伟伟
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年5月26日
 * 修改人:顾伟伟
 * 修改描述:新增方法SQL
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.IService;
using Hemo.Service;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class InfectiousForm :HemoBaseFrm
    {
        #region 变量
        private PatientModel.MED_PATIENTSRow _current = null;

        public PatientModel.MED_PATIENTSRow Current
        {
            get { return _current; }
            set { _current = value; }
        }
        private PatientModel.MED_PATIENTSDataTable _patientDataTable;


        private IPatient objPatient = ServiceManager.Instance.PatientService;
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        public InfectiousForm()
        {
            InitializeComponent();
        }

        #region 事件
        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            _patientDataTable = new PatientModel.MED_PATIENTSDataTable();
            string strInfection = string.Empty;
            foreach (Control c in this.panelControl1.Controls)
            {
                if (c.GetType().ToString() == "DevExpress.XtraEditors.CheckEdit" && ((CheckEdit)c).Checked)
                {
                    if (c.Text.Trim() == "其它")
                    {
                        strInfection += this.txt_Other.Text + ",";
                    }
                    else
                    {
                        strInfection += c.Text + ",";
                    }

                }

            }

            if (strInfection.Length > 1)
            {

                strInfection = strInfection.Substring(0, strInfection.Length - 1);
            }
            _patientDataTable.LoadDataRow(_current.ItemArray, true);
            var row = _patientDataTable[0];
            row.INFECTIOUS_CHECK_RESULT = strInfection;
            objPatient.SavePatientInfo(_patientDataTable);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void InfectiousForm_Load(object sender, EventArgs e)
        {
            this.Text = "传染病标示";
            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountFrm(this);

            bool isHaved = false;
            if (_current != null && _current.INFECTIOUS_CHECK_RESULT.Length > 0)
            {
                string[] strl = _current.INFECTIOUS_CHECK_RESULT.Split(',');
                foreach (string s in strl)
                {
                    foreach (Control c in this.panelControl1.Controls)
                    {
                        if (s.Trim() == c.Text.Trim())
                        {
                            isHaved = true;
                            ((CheckEdit)c).Checked = true;
                        }
                    }
                    if (!isHaved)
                    {
                        this.checkEdit_Other.CheckState = CheckState.Checked;
                        this.txt_Other.Text = s.Trim();
                        isHaved = false;
                    }

                }

            }
        }

        private void checkEdit_Other_CheckedChanged(object sender, EventArgs e)
        {
            this.txt_Other.Enabled = this.checkEdit_Other.Checked;
        }
         #endregion
    }
}