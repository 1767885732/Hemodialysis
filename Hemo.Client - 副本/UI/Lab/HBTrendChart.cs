/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：血红蛋白趋势图查询窗体类
// 创建时间：2015-9-23
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Service;
using Hemo.IService.Lab;
using Hemo.Utilities;

namespace Hemo.Client.UI.Lab
{
    public partial class HBTrendChart : DevExpress.XtraEditors.XtraForm
    {
        #region 类变量

        private string pPatientId;
        private string pHemodialysisID;
        private ILab _labService = ServiceManager.Instance.LabService;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public HBTrendChart(string pPatientId, string pHemodialysisID)
        {
            InitializeComponent();

            this.cmbSTART_DATE.DateTime = DateTime.Now.AddDays(-DateTime.Now.Day + 1).AddMonths(-DateTime.Now.Month + 1);
            this.cmbEND_DATE.DateTime = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.Day);
            this.pPatientId = pPatientId;
            this.pHemodialysisID = pHemodialysisID;
        }

        #endregion

        #region 事件

        private void HBTrendChart_Load(object sender, EventArgs e)
        {
            this.ctlUserLongInfo1.HEMODIALYSIS_ID = this.pHemodialysisID;
            this.ctlUserLongInfo1.LoadPatientInfo();
            this.ctlUserLongInfo1.PatientTypeEnabled = true;

            this.ctlSignChart1.DrawHBTrendChart(this._labService.GetHBTrend(this.pPatientId,
                    Utility.CDate(this.cmbSTART_DATE.Text + " " + this.cmbSTART_Time.Text),
                    Utility.CDate(this.cmbEND_DATE.Text + " " + this.cmbEND_Time.Text)));
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.ctlSignChart1.DrawHBTrendChart(this._labService.GetHBTrend(this.pPatientId,
                    Utility.CDate(this.cmbSTART_DATE.Text + " " + this.cmbSTART_Time.Text),
                    Utility.CDate(this.cmbEND_DATE.Text + " " + this.cmbEND_Time.Text)));
        }

        #endregion
    }
}