/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：通用用户基本信息控件
// 创建时间：2013-03-14
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
//
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.Client.UI.Hemodialysis;
using Hemo.IService;
using DevExpress.XtraEditors.Controls;

namespace Hemo.Client.Controls
{
    public partial class CtlUserLongInfo : DevExpress.XtraEditors.XtraUserControl
    {
        public CtlUserLongInfo()
        {
            InitializeComponent();
        }
        #region 私有成员
        /// <summary>
        /// 实例化病人服务对象
        /// </summary>
        private IPatient objPatient = ServiceManager.Instance.PatientService;

        /// <summary>
        /// 病人数据表  
        /// </summary>
        private PatientModel.MED_PATIENTSDataTable _patientDataTable;
        #endregion

        #region 公共属性



        /// <summary>
        /// 父窗口容器
        /// </summary>
        public EditVascularAccess FormContainer { get; set; }

        /// <summary>
        /// 透析号
        /// </summary>
        private string _hemodialysisID = string.Empty;
        public string HEMODIALYSIS_ID
        {
            set
            {
                _hemodialysisID = value;
            }
            get
            {
                return _hemodialysisID;
            }
        }

        //修复只有住院号时Patient_ID为空的情况
        private string _patientID = string.Empty;
        public string PatientID
        {
            get
            {
                if (_patientDataTable.Rows[0]["PATIENT_ID"].ToString().Length > 0)
                {
                    _patientID = _patientDataTable.Rows[0]["PATIENT_ID"].ToString();
                }
                else
                {
                    _patientID = _patientDataTable.Rows[0]["ADMISSION_NUMBER"].ToString();
                }
                return _patientID;
            }
        }
        /// <summary>
        /// 面板宽度
        /// </summary>
        public int PanelWidth
        {
            set
            {
                panControl.Width = value;
            }
            get
            {
                return panControl.Width;
            }
        }


        public BorderStyles PanBorderStyle
        {
            get
            {
                return panControl.BorderStyle;
            }
            set
            {
                panControl.BorderStyle = value;
            }
        }

        /// <summary>
        /// 病人类型是否可用
        /// </summary>
        public bool PatientTypeEnabled
        {
            get
            {
                return cbxTIME_TYPE.Enabled;
            }
            set
            {
                cbxTIME_TYPE.Enabled = value;
            }
        }

        /// <summary>
        /// 病人类型名称
        /// </summary>
        public string PatientType
        {
            get
            {
                return cbxTIME_TYPE.Text;
            }
            set
            {
                cbxTIME_TYPE.Text = value;
            }
        }

        private string _diagose = string.Empty;
        public string DIAGNOSE
        {
            get
            {
                return _diagose;
            }
            set
            {
                _diagose = value;
            }
        }

        public PatientModel.MED_PATIENTSRow Patient
        {
            get
            {
                if (this._patientDataTable != null && this._patientDataTable.Rows.Count > 0)
                {
                    return this._patientDataTable.Rows[0] as PatientModel.MED_PATIENTSRow;
                }
                return null;
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 根据西透析号载入病人数据并给控件赋值
        /// </summary>
        public void LoadPatientInfo()
        {
            _patientDataTable = objPatient.GetPatientListByParams("", HEMODIALYSIS_ID);
            if (_patientDataTable != null && _patientDataTable.Rows.Count > 0)
            {
                BaseControlInfo.SetControlDataByDataTable(_patientDataTable, panControl);
                BaseControlInfo.SetControlEnabled(panControl, false);
                cbxTIME_TYPE.Enabled = PatientTypeEnabled;
                DIAGNOSE = _patientDataTable.Rows[0]["DIAGNOSE"].ToString();
                this.patientInfoCheck1.SetTxtHemoIdText(_patientDataTable[0].HEMODIALYSIS_ID);
            }
            else
            {
                txtNAME.Text = string.Empty;
                this.txtSEX.EditValue = null;
                this.txtSEX.Text = string.Empty;
                this.txtBIRTHDAY.EditValue = string.Empty;
                this.txtBIRTHDAY.Text = string.Empty;
                this.txtAge.Text = string.Empty;
                this.cbxTIME_TYPE.Text = string.Empty;
                this.patientInfoCheck1.SetTxtHemoIdText(string.Empty);
            }
        }

        /// <summary>
        /// 设置控件是否可用
        /// </summary>
        /// <param name="pValue"></param>
        public void SetControlsEnabled(bool pValue)
        {
            BaseControlInfo.SetControlEnabled(panControl, pValue);
        }
        #endregion
    }
}
