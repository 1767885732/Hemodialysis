/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者血压趋势报表类
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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Client.UI.Machine;
using Hemo.IService.Config;
using Hemo.Service;

namespace Hemo.Client.UI.Patient
{
    public partial class PatientCurePressure : ViewBase
    {
        #region 类变量

        private string _patientName = string.Empty;

        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;

        private Model.HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable medHemoParameterDT = new Model.HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();

        #endregion

        #region 属性

        public Model.HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable MedHemoParameterDT
        {
            get { return medHemoParameterDT; }
            set { medHemoParameterDT = value; }
        }

        #endregion

        #region 构造函数

        public PatientCurePressure()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void btnQuery_Click(object sender, EventArgs e)
        {
            QueryPressureSigle();
        }

        private void QueryDate_EditValueChanged(object sender, EventArgs e)
        {
        }

        #endregion

        #region 方法

        public void SetDateTimeValue(DateTime dt)
        {
            this.QueryDate.DateTime = dt;
        }

        public void SetTitle(string patientName)
        {
            _patientName = patientName;
            this.lbTitle.Text = string.Format("{0}在{1}透析过程中的血压情况的趋势图", patientName, this.QueryDate.DateTime.Date.ToString("yyyy-MM-dd"));
        }

        public void QueryPressureSigle()
        {
            if (medHemoParameterDT != null && medHemoParameterDT.Rows.Count > 0)
            {
                var dayDtParam = new Model.HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();
                medHemoParameterDT.Where(i => i.CREATE_DATE.Date == this.QueryDate.DateTime.Date).CopyToDataTable<Model.HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSRow>(dayDtParam, LoadOption.PreserveChanges);
                if (dayDtParam.Rows.Count > 0)
                {
                    ctlSignChart2.DrawPressureChartDetail(dayDtParam);
                    SetTitle(_patientName);
                }
            }
        }

        #endregion
    }
}
