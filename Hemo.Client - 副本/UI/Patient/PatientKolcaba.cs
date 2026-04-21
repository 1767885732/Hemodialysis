/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者Kolcaba查询维护类
// 创建时间：2016-07-5
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
using Hemo.Model;
using DevExpress.XtraEditors;
using Hemo.IService;
using Hemo.Service;

namespace Hemo.Client.UI.Patient
{
    public partial class PatientKolcaba : HemoBaseFrm
    {
        #region 类变量

        private string currentHemoId = string.Empty;
        private string currentHemoName = string.Empty;
        private IPatient patientService = ServiceManager.Instance.PatientService;

        #endregion

        #region 属性

        public string CurrentHemoId
        {
            get { return currentHemoId; }
            set { currentHemoId = value; }
        }
        public string CurrentHemoName
        {
            get { return currentHemoName; }
            set { currentHemoName = value; }
        }

        #endregion

        #region 构造函数

        public PatientKolcaba()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            foreach (PatientKolcabaModel.MED_PATIENT_KOLCABARow row in (this.gcRecord.DataSource as PatientKolcabaModel.MED_PATIENT_KOLCABADataTable))
            {
                if (row.CREATEDATE == DateTime.Now.Date)
                {
                    XtraMessageBox.Show("同一透析编号KOLCABA记录当天不能重复！");
                    return;
                }
            }
            PatientKolcabaDetail recordDetail = new PatientKolcabaDetail();
            recordDetail.CurrentHemoId = currentHemoId;
            recordDetail.CurrentRecordRow = null;
            DialogResult result = recordDetail.ShowDialog();
            if (result == DialogResult.OK)
            {
                Query();
            }
        }

        private void ctlMedicalUserInfo_Load(object sender, EventArgs e)
        {
            this.ctlMedicalUserInfo.HemoId = currentHemoId;
            this.ctlMedicalUserInfo.LoadUserInfo();

            this.txtFromDate.EditValue = DateTime.Parse(DateTime.Now.Year.ToString() + "-" + "01" + "-" + "01");
            this.txtToDate.EditValue = DateTime.Now.Date;

            Query();
        }

        private void gcRecord_DoubleClick(object sender, EventArgs e)
        {
            if (this.gvRecord.GetFocusedDataRow() != null)
            {
                PatientKolcabaDetail recordDetail = new PatientKolcabaDetail();
                recordDetail.CurrentHemoId = currentHemoId;
                recordDetail.CurrentHemoName = currentHemoName;
                recordDetail.CurrentRecordRow = this.gvRecord.GetFocusedDataRow() as PatientKolcabaModel.MED_PATIENT_KOLCABARow;

                DialogResult result = recordDetail.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.gcRecord.DataSource = patientService.QueryPatientKolcabaByParams(currentHemoId, Convert.ToDateTime(this.txtFromDate.EditValue), Convert.ToDateTime(this.txtToDate.EditValue));
                }
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        #endregion

        #region 方法

        private void Query()
        {
            this.gcRecord.DataSource = patientService.QueryPatientKolcabaByParams(currentHemoId, Convert.ToDateTime(this.txtFromDate.EditValue), Convert.ToDateTime(this.txtToDate.EditValue));
        }

        #endregion
    }
}
