/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：URR|Kt/V|TS评估列表窗体类
// 创建时间：2015-08-21
// 创建者：吕志强
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.IService;
using Hemo.Service;
using Hemo.Model;

namespace Hemo.Client.UI.Patient
{
    public partial class FrmSufficiencyURR : HemoBaseFrm
    {
        #region 类变量

        private string hemoId = string.Empty;

        private IPatient patientService = ServiceManager.Instance.PatientService;

        #endregion

        #region 属性

        /// <summary>
        /// 透析编号
        /// </summary>
        public string HemoId
        {
            get { return hemoId; }
            set { hemoId = value; }
        }

        #endregion

        #region 构造函数

        public FrmSufficiencyURR()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSufficiencyURR_Load(object sender, EventArgs e)
        {
            LoadPatientInfo();
            this.ctlPatientSufficiency.HemoId = hemoId;
        }

        private void ctlPatientSufficiency_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载病人信息
        /// </summary>
        private void LoadPatientInfo()
        {
            PatientModel.MED_PATIENTSDataTable dtPatient = patientService.GetPatientListByParams(string.Empty, hemoId);
            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                this.ctlUserLongInfo.HEMODIALYSIS_ID = dtPatient[0].HEMODIALYSIS_ID;
                this.ctlUserLongInfo.LoadPatientInfo();
                this.ctlPatientSufficiency.CurrentPatient = dtPatient[0];
            }
        }

        #endregion
    }
}
