/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者血压趋势窗体类
// 创建时间：2016-07-14
// 创建者：刘超
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
using Hemo.IService.Config;
using Hemo.Service;

namespace Hemo.Client.UI.Patient
{
    public partial class PatientCurePressureFrm : HemoBaseFrm
    {
        #region 类变量

        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;

        #endregion

        #region 属性

        public string currentHemoId { get; set; }
        public string currentPatientName { get; set; }

        #endregion

        #region 构造函数

        public PatientCurePressureFrm()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void PatientCurePressureFrm_Load(object sender, EventArgs e)
        {
            this.Text = currentPatientName;// string.Format("{0}在透析过程血透变化图", currentPatientName);
            var dtParam = objHemodialysisService.GetHemoParamsByHemoID(currentHemoId);

            var frm = new PatientCurePressure();
            frm.MedHemoParameterDT = dtParam;
            frm.SetTitle(currentPatientName);
            frm.Dock = DockStyle.Fill;
            frm.SetDateTimeValue(DateTime.Now.AddDays(-1));
            frm.QueryPressureSigle();
            this.Controls.Add(frm);
        }

        #endregion 
    }
}
