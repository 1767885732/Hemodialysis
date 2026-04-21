/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:工作变更
 * 创建标识:贺建操-2013年8月3日
 * 
 * 修改时间:2018年10月16日
 * 修改人:刘超
 * 修改描述: 患者加透编辑窗体
 * 
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.IService.Config;
using Hemo.Service;
using DevExpress.XtraEditors.Repository;
using Hemo.IService.Dict;
using Hemo.IService.PatientSchedule;
using Hemo.Client.Core;
using Hemo.IService;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class ShowEditTreatment : HemoBaseFrm
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ShowEditTreatment(string HemodialysisID, string CureID, int TabIndex, int LoginType)
        {
            InitializeComponent();
        }

        #region 属性
        public bool isOverOrder { get; set; }
        private DateTime cureDate = System.DateTime.Now;
        public DateTime CureDate
        {
            get { return cureDate; }
            set { this.cureDate = value; }
        }

        private string _hemodialysisID = string.Empty;
        public string HemodialysisID
        {
            get
            {
                return _hemodialysisID;
            }
            set
            {
                _hemodialysisID = value;
            }
        }

        private int _tabIndex = 0;
        public int Tab_Index
        {
            get { return _tabIndex; }

            set { _tabIndex = value; }
        }

        private string _cureID = string.Empty;
        public string CureID
        {
            get
            {
                return _cureID;
            }
            set
            {
                _cureID = value;
            }
        }

        private int _loginType = 0;
        public int LoginType
        {
            get
            {
                return _loginType;
            }
            set
            {
                _loginType = value;
            }
        }

        private PatientScheduleModel.MED_PATIENT_SCHEDULERow _patientScheduleRow = null;
        public PatientScheduleModel.MED_PATIENT_SCHEDULERow PatientScheduleRow
        {
            get { return _patientScheduleRow; }
            set { this._patientScheduleRow = value; }
        }
        #endregion

        #region 事件
        private void ShowEditTreatment_Load(object sender, EventArgs e)
        {
            EditTreatment frm = new EditTreatment(HemodialysisID, CureID, Tab_Index, LoginType);
            frm.PatientScheduleRow = PatientScheduleRow;
            frm.CureDate = CureDate;
            frm.IsOverOrder = true;
            frm.IsReplenishTreat = false;
            frm.LoadInfo(HemodialysisID, CureID, Tab_Index, LoginType);
            panelControl1.Controls.Add(frm);
            frm.Dock = DockStyle.Fill;
        }
        #endregion


    }
}