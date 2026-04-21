/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:血透机复用记录报表
 * 创建标识:刘超-2016年5月20日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Hemo.Model;
using Hemo.IService.Machine;
using Hemo.Service;

namespace Hemo.Client.Print
{
    public partial class MachineReUsableReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        public MachineReUsableReport(PatientModel.MED_PATIENTSRow _patientRow, DateTime begintime, DateTime endtime)
        {
            InitializeComponent();
            xrLabel1.Text = Utilities.Utility.GetHospitalName() + "透析器复用情况记录单";
            this.lb_Name.Text = string.Format("姓名：{0}", _patientRow.NAME);
            this.lb_sex.Text = string.Format("性别：{0}",_patientRow.SEX);
            this.lb_hemoid.Text = string.Format("病案号：{0}", _patientRow.HEMODIALYSIS_ID);
            this.lb_patientID.Text = string.Format("病人编号:{0}", _patientRow.PATIENT_ID);

            IMachine _machineService = ServiceManager.Instance.MachineService;

            this.DataSource = _machineService.GetReUsableData(_patientRow.HEMODIALYSIS_ID, begintime, endtime, string.Empty);
            this.DataMember = "";
        }

        #endregion
    }
}
