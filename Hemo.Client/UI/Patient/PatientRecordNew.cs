/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司
// 描述：患者病历维护窗体
// 创建时间：2016-6-20
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
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService;
using Hemo.Service;
using Hemo.Model;
using Hemo.Client.UI.Hemodialysis;
using Hemo.Utilities;

namespace Hemo.Client.UI.Patient
{
    public partial class PatientRecordNew :HemoBaseFrm
    {
        #region 成员变量

        private string currentHemoId = string.Empty;

        private IPatient patientService = ServiceManager.Instance.PatientService;

        #endregion

        #region 属性

        public string CurrentHemoId
        {
            get { return currentHemoId; }
            set { currentHemoId = value; }
        }

        #endregion

        #region 构造函数

        public PatientRecordNew()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientRecordNew_Load(object sender, EventArgs e)
        {
            this.ctlMedicalUserInfo.HemoId = currentHemoId;
            this.ctlMedicalUserInfo.LoadUserInfo();

            this.txtFromDate.EditValue = DateTime.Parse(DateTime.Now.Year.ToString() + "-" + "01" + "-" + "01");
            this.txtToDate.EditValue = DateTime.Now.Date;

            Query();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            foreach (PatientScheduleModel.MED_PATIENTRECORDRow row in (this.gcRecord.DataSource as PatientScheduleModel.MED_PATIENTRECORDDataTable))
            {
                if (row.CREATEDATE == DateTime.Now.Date)
                {
                    AutoClosedMsgBox.ShowForm("同一透析编号病历记录当天不能重复！", this.Text, 1000, MessageBoxIcon.Warning);
                    return;
                }
            }

            PatientRecordDetail recordDetail = new PatientRecordDetail();
            recordDetail.CurrentHemoId = currentHemoId;
            recordDetail.CurrentRecordRow = null;
            DialogResult result = recordDetail.ShowDialog();
            if (result == DialogResult.OK)
            {
                Query();
            }
        }

        /// <summary>
        /// 药品医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDrug_Click(object sender, EventArgs e)
        {
            var queryRecipeList = new QueryRecipeList(currentHemoId, 1);
            queryRecipeList.ShowDialog();
        }

        /// <summary>
        /// 双击病历列表行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcRecord_DoubleClick(object sender, EventArgs e)
        {
            if (this.gvRecord.GetFocusedDataRow() != null)
            {
                PatientRecordDetail recordDetail = new PatientRecordDetail();
                recordDetail.CurrentHemoId = currentHemoId;
                recordDetail.CurrentRecordRow = this.gvRecord.GetFocusedDataRow() as PatientScheduleModel.MED_PATIENTRECORDRow;
                DialogResult result = recordDetail.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.gcRecord.DataSource = patientService.QueryPatientRecordByParams(currentHemoId, Convert.ToDateTime(this.txtFromDate.EditValue), Convert.ToDateTime(this.txtToDate.EditValue));
                }
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 方法

        private void Query()
        {
            this.gcRecord.DataSource = patientService.QueryPatientRecordByParams(currentHemoId, Convert.ToDateTime(this.txtFromDate.EditValue), Convert.ToDateTime(this.txtToDate.EditValue));
        }

        #endregion
    }
}